using Dapper;
using Microsoft.Extensions.Configuration;
using SalesOrder.Application.Interfaces;
using SalesOrder.Domain.Entities;
using SalesOrder.Domain.Enums;
using SalesOrder.Infrastructure.DbHelper;
using System.Data;
using System.Data.SqlClient;

namespace SalesOrder.Infrastructure.Repositories
{
    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly string _connectionString;
        public SalesOrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(DatabaseConnection.DBConnection);
        }

        public async Task<IReadOnlyList<Order>> GetListAsync()
        {
            try
            {
                var sql = "select o.OrderId,o.OrderName,o.State from [dbo].[Orders] o";
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Order>(sql, commandType: CommandType.Text);
                    connection.Close();
                    return result.OrderBy(x => x.OrderName).ToList();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> SaveOrderAsync(Order order, string actionType)
        {
            int result = 0;
            var sql = DatabaseProcedure.CallBookProcedure.Sp_Order;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@OrderId", order.OrderId);
                parameters.Add("@OrderName", order.OrderName);
                parameters.Add("@State", order.State);
                parameters.Add("@actionType", actionType);
                result = await connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
                connection.Close();
            }
            return result;
        }

        public async Task<IReadOnlyList<Order>> GetAllOrderAsync()
        {
            var sql = DatabaseProcedure.CallBookProcedure.Sp_Order;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@actionType", ActionType.GetAll.ToString());
                var result = (await connection.QueryAsync<Order>(sql, parameters, commandType: CommandType.StoredProcedure)).ToList();
                connection.Close();
                return result;
            }
        }

        public async Task<IReadOnlyList<Order>> GetAllWindowAsync()
        {
            try
            {
                var sql = DatabaseProcedure.CallBookProcedure.Sp_Order;
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("@actionType", ActionType.GetAllWindow.ToString());
                    var result = (await connection.QueryAsync<Order>(sql, parameters, commandType: CommandType.StoredProcedure)).ToList();
                    connection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> SaveWindowAsync(Order order, string actionType)
        {
            int result = 0;
            var sql = DatabaseProcedure.CallBookProcedure.Sp_Order;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("@OrderId", order.OrderId);
                    parameters.Add("@WindowName", order.WindowName);
                    parameters.Add("@WndowId", order.WindowId);
                    parameters.Add("@actionType", actionType);
                    result = await connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
                    connection.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<int> UpdateOrderAsync(OrderDetails order, string actionType)
        {
            int result = 0;
            var sql = DatabaseProcedure.CallBookProcedure.Sp_Order;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("@OrderId", order.OrderId);
                    parameters.Add("@State", order.state);
                    parameters.Add("@WindowName", order.WindowName);
                    parameters.Add("@Width", order.Width);
                    parameters.Add("@Height", order.Height);
                    parameters.Add("@actionType", actionType);
                    result = await connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
                    connection.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<IReadOnlyList<WindowElement>> GetAllElementAsync()
        {
            try
            {
                var sql = DatabaseProcedure.CallBookProcedure.Sp_Order;
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("@actionType", ActionType.GetAllElement.ToString());
                    var result = (await connection.QueryAsync<WindowElement>(sql, parameters, commandType: CommandType.StoredProcedure)).ToList();
                    connection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> SaveElementAsync(WindowElement element, string actionType)
        {
            int result = 0;
            var sql = DatabaseProcedure.CallBookProcedure.Sp_Order;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("@WndowId", element.WindowId);
                    parameters.Add("@SubId", element.SubElementsId);
                    parameters.Add("@Type", element.Type);
                    parameters.Add("@Width", element.Width);
                    parameters.Add("@Height", element.Height);
                    parameters.Add("@actionType", actionType);
                    result = await connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
                    connection.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<IReadOnlyList<OrderDetails>> GetDetalsListAsync(string orderId)
        {
            try
            {
                var sql = DatabaseProcedure.CallBookProcedure.Sp_Order;
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("@OrderId", orderId);
                    parameters.Add("@actionType", ActionType.GetDetailsInfo.ToString());
                    var result = (await connection.QueryAsync<OrderDetails>(sql, parameters, commandType: CommandType.StoredProcedure)).ToList();
                    connection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
