using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using User.Application.Abstractions.Persistence;
using User.Domain.Common;
using User.Infrastructure.Outbox;

namespace User.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserDbContext _context;
        private readonly IMediator _mediator;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(UserDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
                _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await SaveChangesAndStoreOutboxEventsAsync();

                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
        }
        public async Task CommitInternalAsync()
        {
            try
            {
                await SaveInternalAsync();

                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
        }
        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await SaveChangesAndStoreOutboxEventsAsync(cancellationToken);
        }
        private async Task<int> SaveChangesAndStoreOutboxEventsAsync(CancellationToken cancellationToken = default)
        {
            var result = await _context.SaveChangesAsync(cancellationToken);
            var domainEntities = _context.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(x => x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();
            domainEntities.ForEach(e => e.Entity.ClearDomainEvents());
            foreach (var domainEvent in domainEvents)
            {
                var outboxMessage = new OutboxMessage(domainEvent);
                _context.OutboxMessages.Add(outboxMessage);
            }
            if (_context.OutboxMessages.Local.Any())
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            //await _context.SaveChangesAsync(cancellationToken);

            return result;
        }
        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }

        public async Task<int> SaveInternalAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
