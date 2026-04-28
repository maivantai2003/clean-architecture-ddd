using MediatR;
using UserDevice.Application.DTOs;

namespace UserDevice.Application.Features.UserDevices.Queries
{
    public record GetByUserDeviceQuery(Guid userId,string deviceId):IRequest<UserDeviceDTO?>;
}
