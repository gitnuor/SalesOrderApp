using Microsoft.AspNetCore.Mvc;
using SalesOrder.Application.Interfaces;

namespace SalesOrder.Views.Shared.Components.TopMenuWidget
{
    public class TopMenuWidgetViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public TopMenuWidgetViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("_TopNavigation");
        }
    }
}
