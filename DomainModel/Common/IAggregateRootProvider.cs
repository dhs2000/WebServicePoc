namespace DomainModel.Common
{
    public interface IAggregateRootProvider
    {
        IAggregateRoot AggregateRoot { get; }
    }
}