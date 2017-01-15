using Promocodoz.Domain.Interfaces;

namespace Promocodoz.Domain.Core.Entities
{
    public class IdentifiableEntity<TKey> : IIdentifiableEntity<TKey>
    {
        public virtual TKey Id { get; protected set; }
    }
}
