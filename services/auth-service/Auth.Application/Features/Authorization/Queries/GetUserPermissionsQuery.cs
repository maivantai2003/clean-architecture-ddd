using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Features.Authorization.Queries
{
    public record GetUserPermissionsQuery(Guid UserId): IRequest<IReadOnlyList<string>>;
}
