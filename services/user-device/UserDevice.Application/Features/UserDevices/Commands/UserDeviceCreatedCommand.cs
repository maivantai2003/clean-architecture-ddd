using MediatR;

namespace UserDevice.Application.Features.UserDevices.Commands
{
    public record UserDeviceCreatedCommand(Guid userId,string deviceId, string deviceType,string deviceToken,string location):IRequest<Guid>;
}