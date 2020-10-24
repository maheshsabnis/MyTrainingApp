using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Core_MyApp.Models;
using Core_MyApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Core_MyApp.Controllers
{
    /// <summary>
    /// Route ---> Map with the Route Expression and load the controller
    /// </summary>
    [Route("api/[controller]")]
     [ApiController]
    public class DepartmentAPIController : ControllerBase
    {
        private readonly IRepository<Department, int> deptRepository;
        public DepartmentAPIController(IRepository<Department, int> deptRepository)
        {
            this.deptRepository = deptRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var depts = await deptRepository.GetAsync();
            return Ok(depts);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var dept = await deptRepository.GetAsync(id);
            if (dept != null)
            { 
                return Ok(dept);
            }
            return NotFound($"Record Not Found based on Id as {id} probably it is deleted");
            
        }

        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]Department dept)
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromQuery] Department dept)

        //[HttpPost("{DeptNo}/{DeptName}/{Location}")]
        //public async Task<IActionResult> PostAsync([FromRoute] Department dept)
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromForm] Department dept)
        [HttpPost]
        public async Task<IActionResult> PostAsync(Department dept)
        {
            
            if (ModelState.IsValid)
            { 
                dept = await deptRepository.CreateAsync(dept);
                return Ok(dept);
            }
            return BadRequest(ModelState);
             
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Department dept)
        {
            if (ModelState.IsValid)
            {
                dept = await deptRepository.UpdateAsync(id, dept);
                return Ok(dept);
            }
            return BadRequest(ModelState);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
             var result = await deptRepository.DeleteAsync(id);
            if(result)
            {
                return Ok("Record Deleted Succesfully...");
            }
            return NotFound($"Delete Operation Filed for id {id} either record is not found or server refuse to delete");
           
        }

    }
}
