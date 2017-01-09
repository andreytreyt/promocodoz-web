namespace Promocodoz.Domain.Interfaces
{
    public interface IIdentifiableEntity<out TKey>
    {
        TKey Id { get; }
    }
}
