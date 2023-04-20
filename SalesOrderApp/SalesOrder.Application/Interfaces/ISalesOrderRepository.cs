
using SalesOrder.Domain.Entities;

namespace SalesOrder.Application.Interfaces
{
    public interface  ISalesOrderRepository
    {
        Task<IReadOnlyList<Order>> GetListAsync();
        Task<int> SaveOrderAsync(Order order, string actionType);
        Task<IReadOnlyList<Order>> GetAllOrderAsync();
        Task<IReadOnlyList<Order>> GetAllWindowAsync();
        Task<int> SaveWindowAsync(Order order, string actionType);
        Task<IReadOnlyList<WindowElement>> GetAllElementAsync();
        Task<int> SaveElementAsync(WindowElement order, string actionType);
        Task<IReadOnlyList<OrderDetails>> GetDetalsListAsync(string orderId);
        Task<int> UpdateOrderAsync(OrderDetails order, string actionType);
    }
}
