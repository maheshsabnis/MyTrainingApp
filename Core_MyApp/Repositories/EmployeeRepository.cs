using Core_MyApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_MyApp.Repositories
{
    public class EmployeeRepository : IRepository<Employee, int>
    {
        private readonly CompanyContext _context;
        public EmployeeRepository(CompanyContext context)
        {
            _context = context;
        }
        public async Task<Employee> CreateAsync(Employee entity)
        {
            var result = await _context.Employee.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var emp = await _context.Employee.FindAsync(id);
            if (emp != null)
            {
                _context.Employee.Remove(emp);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<Employee>> GetAsync()
        {
            var emps = await _context.Employee.ToListAsync();
            return emps;
        }

        public async Task<Employee> GetAsync(int id)
        {
            var emp = await _context.Employee.FindAsync(id);
            return emp;
        }

        public async Task<Employee> UpdateAsync(int id, Employee entity)
        {
            var emp = await _context.Employee.FindAsync(id);
            if (emp != null)
            {
                emp.EmpName = entity.EmpName;
                emp.Salary = entity.Salary;
                emp.Designation = entity.Designation;
                emp.DeptNo = entity.DeptNo;
                await _context.SaveChangesAsync();
                return emp;
            }
            return entity;
        }
    }
}
