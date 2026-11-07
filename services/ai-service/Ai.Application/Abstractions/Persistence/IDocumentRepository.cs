using Ai.Domain.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai.Application.Abstractions.Persistence
{
    public interface IDocumentRepository
    {
        Task<Document?> GetByIdAsync(
            Guid documentId,
            CancellationToken cancellationToken);

        Task AddAsync(
            Document document,
            CancellationToken cancellationToken);
    }
}
