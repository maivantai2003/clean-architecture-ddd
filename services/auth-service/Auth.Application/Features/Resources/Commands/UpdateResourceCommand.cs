using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Features.Resources.Commands
{
    public record UpdateResourceCommand(Guid ResourceId,string Name,string Description) : IRequest<Unit>;
}
