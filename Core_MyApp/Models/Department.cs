using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core_MyApp.Models
{
    public partial class Department
    {
        public Department()
        {
            Employee = new HashSet<Employee>();
        }

        [Required(ErrorMessage ="DeptNo is Must")]
        public int DeptNo { get; set; }
        [Required(ErrorMessage = "DeptName is Must")]
        public string DeptName { get; set; }
        [Required(ErrorMessage = "Location is Must")]
        public string Location { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
