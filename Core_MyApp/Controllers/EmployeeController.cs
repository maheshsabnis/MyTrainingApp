using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Core_MyApp.CustomFilters;
using Core_MyApp.Models;
using Core_MyApp.Repositories;
using Core_MyApp.SessionExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core_MyApp.Controllers
{
    /// <summary>
    /// Action Filter applied at controller level
    /// </summary>
    // [MyLoggingFilter]
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
        public  IActionResult Index()
        {
            List<Employee> emps = new List<Employee>();
            // var id = HttpContext.Session.GetInt32("DeptNo");
            // read department object from session
            var dept = HttpContext.Session.GetSessionObject<Department>("Dept");
            if (dept.DeptNo > 0)
            {
                emps = (from e in _empRepository.GetAsync().Result.ToList()
                        where e.DeptNo == dept.DeptNo
                        select e
                        ).ToList();
            }
            else
            { 
                 emps =   _empRepository.GetAsync().Result.ToList();
            }
             
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
            //try
            //{
                if (ModelState.IsValid)
                {
                    if (emp.Salary < 0)
                    {
                        throw new Exception($"Salary Cannot be -ve {emp.Salary}");
                    }
                    emp = await _empRepository.CreateAsync(emp);
                    return RedirectToAction("Index");
                }
                ViewData["DeptNo"] = await _deptRepository.GetAsync();
                return View(emp);
            //}
            //catch (Exception ex)
            //{
            //    return View("Error", new ErrorViewModel() {
            //        ControllerName = this.RouteData.Values["controller"].ToString(),
            //        ActionName = this.RouteData.Values["action"].ToString(),
            //        ErrorMessage = ex.Message
            //    });
              
            //}
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
