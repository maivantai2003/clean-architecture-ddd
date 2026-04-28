using MediatR;
namespace UserDevice.Application.Features.DeviceOtps.Commands
{
    public record VerifyDeviceOtpCommand(Guid UserId,string DeviceId,string OtpCode) : IRequest<Unit>;

}
