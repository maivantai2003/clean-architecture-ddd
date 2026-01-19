using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.Features.Users.Commands
{
    public record UserRegisterCommand(Guid UserId,string Name, string Email) :IRequest<Guid>;
}
