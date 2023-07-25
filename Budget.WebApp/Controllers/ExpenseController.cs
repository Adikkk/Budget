using Budget.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Budget.WebApp.Controllers
{
    public class ExpenseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details([FromRoute]Guid id)
        {
            var vm = new ExpenseDetailsViewModel(id);

            return View(vm);
        }
    }
}
