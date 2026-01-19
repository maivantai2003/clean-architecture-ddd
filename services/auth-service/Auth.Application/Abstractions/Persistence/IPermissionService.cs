using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Abstractions.Persistence
{
    public interface IPermissionService
    {
        Task<IReadOnlyList<string>> GetPermissionsAsync(Guid userId);
        Task ClearAsync(Guid userId);
    }
}
