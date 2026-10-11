using Auth.Application.Abstractions.Persistence;
using Auth.Application.Abstractions.Security;
using Auth.Application.DTOs;
using Auth.Domain.Entities;
using MediatR;

namespace Auth.Application.Features.Authorization.Commands;

public sealed class RefreshTokenCommandHandler
    : IRequestHandler<RefreshTokenCommand, TokenResponse>
{
    private readonly IRefreshTokenRepository _refreshTokenRepo;
    private readonly IUserRepository _userRepo;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenLifetimeProvider _jwtOptions;

    public RefreshTokenCommandHandler(
        IRefreshTokenRepository refreshTokenRepo,
        IUserRepository userRepo,
        IJwtTokenService jwtTokenService,
        IUnitOfWork unitOfWork, ITokenLifetimeProvider jwtOptions)
    {
        _refreshTokenRepo = refreshTokenRepo;
        _userRepo = userRepo;
        _jwtTokenService = jwtTokenService;
        _unitOfWork = unitOfWork;
        _jwtOptions = jwtOptions;
    }

    public async Task<TokenResponse> Handle(RefreshTokenCommand request,CancellationToken cancellationToken)
    {
        // 1️⃣ Hash refresh token
        var tokenHash = RefreshToken.Hash(request.RefreshToken);

        // 2️⃣ Load token
        var oldToken = await _refreshTokenRepo.GetByHashAsync(tokenHash)
            ?? throw new UnauthorizedAccessException("Invalid refresh token");
        var user = await _userRepo.GetByIdAsync(oldToken.UserId)
             ?? throw new UnauthorizedAccessException("User not found");
        // 3️⃣ Validate
        if (oldToken.IsExpired)
            throw new UnauthorizedAccessException("Refresh token expired");

        if (oldToken.IsRevoked)
        {
            // Refresh token reuse attack
            await _refreshTokenRepo.RevokeAllByUserAsync(oldToken.UserId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            throw new UnauthorizedAccessException("Refresh token reused");
        }

        // 4️⃣ Rotate refresh token
        var newRefreshTokenValue = _jwtTokenService.GenerateRefreshToken();
        var newRefreshTokenHash = RefreshToken.Hash(newRefreshTokenValue);

        var newRefreshToken = RefreshToken.Create(
            oldToken.UserId,
            newRefreshTokenHash,
            DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenDays)
        );

        oldToken.Revoke(newRefreshTokenHash);
        await _refreshTokenRepo.AddAsync(newRefreshToken);

        // 5️⃣ Generate access token
        var claims = MinimalJwtClaims.Create(user);
        var accessToken = _jwtTokenService.GenerateAccessToken(claims);

        // 6️⃣ Commit
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new TokenResponse(
            accessToken,
            _jwtOptions.AccessTokenMinutes * 60,
            newRefreshTokenValue,
            _jwtOptions.RefreshTokenDays * 24 * 60 * 60
        );
    }
}
