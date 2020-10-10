using Core_MyApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_MyApp.Repositories
{
    /// <summary>
    /// This class will be Constructor Injected  using CompanyContext class
    /// because the CompanyContext class is already Registered in  DI Container
    /// of the application using AddDbContext(); in ConfigureServices() method of
    /// StartUp class
    /// </summary>
    public class DepartmentRepository : IRepository<Department, int>
    {
        private readonly CompanyContext _context;
        public DepartmentRepository(CompanyContext context)
        {
            _context = context;
        }
        public async Task<Department> CreateAsync(Department entity)
        {
           var result =  await _context.Department.AddAsync(entity);
           await _context.SaveChangesAsync();
           return result.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var dept = await _context.Department.FindAsync(id);
            if (dept != null)
            {
                _context.Department.Remove(dept);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<Department>> GetAsync()
        {
            var depts = await _context.Department.ToListAsync();
            return depts;
        }

        public async Task<Department> GetAsync(int id)
        {
            var dept = await _context.Department.FindAsync(id);
            return dept;
        }

        public async Task<Department> UpdateAsync(int id, Department entity)
        {
            var dept = await _context.Department.FindAsync(id);
            if (dept != null)
            {
                dept.DeptName = entity.DeptName;
                dept.Location = entity.Location;
                await _context.SaveChangesAsync();
                return dept;
            }
            return entity;
        }
    }
}
