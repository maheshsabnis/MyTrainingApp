using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Core_MyApp.Models;
using Core_MyApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core_MyApp.Controllers
{
    public class EmployeeController : Controller
    {
        IRepository<Employee, int> _empRepository;
        IRepository<Department, int> _deptRepository;
        public EmployeeController(IRepository<Employee, int> empRepository,
            IRepository<Department, int> deptRepository)
        {
            _empRepository = empRepository;
            _deptRepository = deptRepository;
        }
        // GET: EmployeeController
        public async Task<ActionResult> Index()
        {
            var emps = await _empRepository.GetAsync();
            return View(emps);
        }

        

        // GET: EmployeeController/Create
        public async Task<ActionResult> Create()
        {
            var emp = new Employee();
            // define a ViewData foe List of Departments
            ViewData["DeptNo"] = await _deptRepository.GetAsync();
            return View(emp);
        }

        // POST: EmployeeController/Create
        [HttpPost]
         
        public async Task<ActionResult> Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                emp = await _empRepository.CreateAsync(emp);
                return RedirectToAction("Index");
            }
            ViewData["DeptNo"] = await _deptRepository.GetAsync();
            return View(emp);
        }

        // GET: EmployeeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var emp = await _empRepository.GetAsync(id);
            return View(emp);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Employee emp)
        {
            if (ModelState.IsValid)
            {
                emp = await _empRepository.UpdateAsync(id, emp);
                return RedirectToAction("Index");
            }
            return View(emp);
        }

        // GET: EmployeeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
           var res =  await  _empRepository.DeleteAsync(id);

            return RedirectToAction("Index");
        }

       
    }
}
