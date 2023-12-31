﻿using Budget.WebApp.Models.Expense;
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

        public IActionResult Edit([FromRoute] Guid id)
        {
            var vm = new ExpenseEditViewModel(id);

            return View(vm);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
