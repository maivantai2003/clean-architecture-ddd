using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Features.Actions.Commands
{
    public record DeleteActionCommand(Guid ActionId) : IRequest<Unit>;
}
