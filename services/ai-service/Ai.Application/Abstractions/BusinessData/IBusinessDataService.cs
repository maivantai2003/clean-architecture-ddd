using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai.Application.Abstractions.BusinessData
{
    public sealed record BusinessQueryRequest(string Query,IReadOnlyDictionary<string, string> Filters);

    public sealed record BusinessRecord(
        string Type,
        string Id,
        string Summary);

    public interface IBusinessDataService
    {
        Task<IReadOnlyList<BusinessRecord>> QueryAsync(
            BusinessQueryRequest request,
            CancellationToken cancellationToken);
    }
}
