namespace SalesOrder.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ISalesOrderRepository SalesOrderRepository { get; }

    }
}
