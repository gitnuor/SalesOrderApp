using Microsoft.AspNetCore.Mvc;
using SalesOrder.Application.Interfaces;
using SalesOrder.Domain.Entities;
using SalesOrder.Domain.Enums;
using System.Security.Claims;

namespace SalesOrder.Controllers
{
    public class WindowController : BaseController
    {

        private readonly IUnitOfWork _unitOfWork;
        public WindowController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Elements()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWindowList()
        {
            try
            {
                string message = "Sorry!!! No Data Found!!!";
                var data = await _unitOfWork.SalesOrderRepository.GetAllWindowAsync();

                return Json(new { data = data, status = "success", message = message, result = CommonAjaxResponse("Success", "Success", "200") });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetElementList()
        {
            try
            {
                string message = "Sorry!!! No Data Found!!!";
                var data = await _unitOfWork.SalesOrderRepository.GetAllElementAsync();

                return Json(new { data = data, status = "success", message = message, result = CommonAjaxResponse("Success", "Success", "200") });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveWindow(Order type)
        {
            try
            {
                string message = "";
                bool saved = false;

                if (string.IsNullOrEmpty(type.WindowName))
                {
                    message = "Window Name Can't be empty.";
                }
                else
                {
                    if (string.IsNullOrEmpty(type.WindowId))
                    {
                        await _unitOfWork.SalesOrderRepository.SaveWindowAsync(type, ActionType.InsertWndow.ToString());
                        saved = true;
                        message = "Successfully Inserted!!!";
                    }
                    else
                    {
                        await _unitOfWork.SalesOrderRepository.SaveWindowAsync(type, ActionType.UpdateWindow.ToString());
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
        public async Task<IActionResult> SaveElement(WindowElement element)
        {
            //string userName = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;
            try
            {
                string message = "";
                bool saved = false;

                if (string.IsNullOrEmpty(element.Type))
                {
                    message = "Window Name Can't be empty.";
                }
                else
                {
                    if (string.IsNullOrEmpty(element.SubElementsId))
                    {
                        await _unitOfWork.SalesOrderRepository.SaveElementAsync(element, ActionType.InsertElement.ToString());
                        saved = true;
                        message = "Successfully Inserted!!!";
                    }
                    else
                    {
                        await _unitOfWork.SalesOrderRepository.SaveElementAsync(element, ActionType.UpdateElement.ToString());
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

        [HttpGet]
        public async Task<IActionResult> WindowFilter(string term, int pageNumber, int pageSize)
        {
            try
            {
                var data = await _unitOfWork.SalesOrderRepository.GetAllWindowAsync();
                if (!string.IsNullOrEmpty(term))
                {
                    data = (from p in data
                            where (p.WindowName.ToLower().Contains(term.ToLower()) || p.WindowId.Contains(term))
                            select p).OrderBy(c => c.WindowName).ToList();
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


