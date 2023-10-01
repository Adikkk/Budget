using Budget.WebApp.Models.Income;
using Microsoft.AspNetCore.Mvc;

namespace Budget.WebApp.Controllers
{
    public class IncomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details([FromRoute] Guid id)
        {
            var vm = new IncomeDetailsViewModel(id);

            return View(vm);
        }

        public IActionResult Edit([FromRoute] Guid id)
        {
            var vm = new IncomeEditViewModel(id);

            return View(vm);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
