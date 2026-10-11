using Auth.Application.Abstractions.Persistence;
using Auth.Application.Abstractions.Security;
using Auth.Application.Common.Errors;
using Auth.Application.DTOs;
using Auth.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Auth.Application.Features.Authorization.Commands;

public sealed class LoginCommandHandler: IRequestHandler<LoginCommand, Result<TokenResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenLifetimeProvider _jwtOptions;
    private readonly ILogger<LoginCommandHandler> _logger;
    public LoginCommandHandler(IUserRepository userRepository,IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork,
        ITokenLifetimeProvider jwtOptions,
        ILogger<LoginCommandHandler> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _jwtOptions = jwtOptions;
    }

    public async Task<Result<TokenResponse>> Handle(LoginCommand request,CancellationToken cancellationToken)
    {
        // 1️⃣ Find user
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            return Result<TokenResponse>.Failure(AuthErrors.InvalidCredentials);
        }
        // 2️⃣ Verify password
        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            return Result<TokenResponse>.Failure(AuthErrors.InvalidCredentials);
        }
        // 3️⃣ Generate tokens
        var claims = MinimalJwtClaims.Create(user);
        var accessToken = _jwtTokenService.GenerateAccessToken(claims);
        var refreshTokenValue = _jwtTokenService.GenerateRefreshToken();
        var refreshTokenHash = RefreshToken.Hash(refreshTokenValue);
        var refreshToken = RefreshToken.Create(
            user.UserId,
            refreshTokenHash,
            DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenDays)
        );
        // 4️⃣ Persist refresh token
        await _refreshTokenRepository.AddAsync(refreshToken);
        // 5️⃣ Commit
        await _unitOfWork.SaveInternalAsync(cancellationToken);
        _logger.LogInformation("User {UserId} logged in successfully", user.UserId);
        return Result<TokenResponse>.Success(new TokenResponse(accessToken,_jwtOptions.AccessTokenMinutes * 60,
            refreshTokenValue,_jwtOptions.RefreshTokenDays * 24 * 60 * 60
        ));
    }
}
