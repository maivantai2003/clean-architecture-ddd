using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai.Domain.Documents
{
    public enum DocumentStatus
    {
        Pending = 1,
        Processing = 2,
        Indexed = 3,
        Failed = 4,
        Deleted = 5
    }
}
