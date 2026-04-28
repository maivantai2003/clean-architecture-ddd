using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search.Application.Features.Search.Commands
{
    public record IndexUserCommand(string UserId, string Username, string Email):IRequest<Unit>;
}
