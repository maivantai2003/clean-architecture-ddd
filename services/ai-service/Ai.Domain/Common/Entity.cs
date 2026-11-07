namespace Ai.Domain.Common
{
    public class Entity
    {
        public Guid Id { get; protected set; }

        protected Entity()
        {
        }

        protected Entity(Guid id)
        {
            if (id == Guid.Empty)
                throw new DomainException("Entity ID không hợp lệ.");

            Id = id;
        }
    }
}
