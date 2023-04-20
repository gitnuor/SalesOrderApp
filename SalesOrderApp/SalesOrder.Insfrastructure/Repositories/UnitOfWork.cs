using SalesOrder.Application.Interfaces;

namespace CallBookSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ISalesOrderRepository salesOrderRepository)
        {
            this.SalesOrderRepository = salesOrderRepository;
        }
        public ISalesOrderRepository SalesOrderRepository { get; }
    }
}
