using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_MyApp.Models;
using Core_MyApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Core_MyApp.Controllers
{
    /// <summary>
    /// Inject the Repository class using Constructor Injection
    /// </summary>
    public class DepartmentController : Controller
    {
        private readonly IRepository<Department, int> _repositoryDept;
        public DepartmentController(IRepository<Department, int> repositoryDept)
        {
            _repositoryDept = repositoryDept;
        }
        public async Task<IActionResult> Index()
        {
            var depts = await _repositoryDept.GetAsync();
            return View(depts);
        }

        public IActionResult Create()
        {
            var dept = new Department();
            return View(dept);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department dept)
        {
            if (ModelState.IsValid)
            {
                dept = await _repositoryDept.CreateAsync(dept);
                return RedirectToAction("Index");
            }
            return View(dept);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dept = await _repositoryDept.GetAsync(id);
            return View(dept);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Department dept)
        {
            if (ModelState.IsValid)
            {
                dept = await _repositoryDept.UpdateAsync(id,dept);
                return RedirectToAction("Index");
            }
            return View(dept);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _repositoryDept.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
