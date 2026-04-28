using MediatR;

namespace UserDevice.Application.Features.UserDevices.Commands
{
    public record UpdateUserDeviceCommand(Guid userId,string deviceId,string? firebaseToken,string? location):IRequest<Guid>;
}
