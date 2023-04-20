using Microsoft.AspNetCore.Mvc;
using SalesOrder.Application.Interfaces;
using SalesOrder.Domain.Entities;
using SalesOrder.Domain.Enums;
using System.Security.Claims;

namespace SalesOrder.Controllers
{
    public class OrderController : BaseController
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        

        public OrderController(ILogger<OrderController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        //[Authorize(Roles = (AppRole.ADMIN))]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrderWiseDetail()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderList()
        {
            try
            {
                string message = "Sorry!!! No Data Found!!!";
                var data = await _unitOfWork.SalesOrderRepository.GetListAsync();

                return Json(new { data = data, status = "success", message = message, result = CommonAjaxResponse("Success", "Success", "200") });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetDetailList(string orderId)
        {
            try
            {
                string message = "Sorry!!! No Data Found!!!";
                var data = await _unitOfWork.SalesOrderRepository.GetDetalsListAsync(orderId);

                return Json(new { data = data, status = "success", message = message, result = CommonAjaxResponse("Success", "Success", "200") });
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> SaveOrder(Order order)
        {
            try
            {
                string message = "";
                bool saved = false;

                if (string.IsNullOrEmpty(order.OrderName))
                {
                    message = "Order Name Can't be empty.";
                }
                else
                {
                    if (string.IsNullOrEmpty(order.OrderId))
                    {
                        await _unitOfWork.SalesOrderRepository.SaveOrderAsync(order, ActionType.Insert.ToString());
                        saved = true;
                        message = "Successfully Inserted!!!";
                    }
                    else
                    {
                        await _unitOfWork.SalesOrderRepository.SaveOrderAsync(order, ActionType.Update.ToString());
                        saved = true;
                        message = "Successfully Updated!!!";
                    }
                }

                return Json(new { status = saved, message = message });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder(OrderDetails orderDetail)
        {
            try
            {
                string message = "";
                bool saved = false;
    
                if (!string.IsNullOrEmpty(orderDetail.OrderId))
                {
                    await _unitOfWork.SalesOrderRepository.UpdateOrderAsync(orderDetail, ActionType.GetDetailsInfoUpdate.ToString());
                    saved = true;
                    message = "Successfully Updated!!!";
                }  
                return Json(new { status = saved, message = message });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> OrderFilter(string term, int pageNumber, int pageSize)
        {
            try
            {
                var data = await _unitOfWork.SalesOrderRepository.GetAllOrderAsync();
                if (!string.IsNullOrEmpty(term))
                {
                    data = (from p in data
                            where (p.OrderName.ToLower().Contains(term.ToLower()) || p.OrderId.Contains(term))
                            select p).OrderBy(c => c.OrderName).ToList();
                }
                var total = data.Count();
                data = data.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                return Json(new { data = data, status = "success", result = CommonAjaxResponse("Success", "Success", "200") });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
