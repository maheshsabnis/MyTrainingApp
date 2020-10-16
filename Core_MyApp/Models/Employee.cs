using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core_MyApp.Models
{
    public partial class Employee
    {
        [Required(ErrorMessage ="EmpNo is required")]
        public int EmpNo { get; set; }
        [Required(ErrorMessage = "EmpName is required")]
        public string EmpName { get; set; }
        [Required(ErrorMessage = "Designation is required")]
        public string Designation { get; set; }
        [Required(ErrorMessage = "Salary is required")]
        // [NumericPositiveValidation(ErrorMessage = "Salary Cannot be -ve")]
        public int Salary { get; set; }
      //  [Required(ErrorMessage = "DeptNo is required")]
        public int DeptNo { get; set; }

        public virtual Department DeptNoNavigation { get; set; }
    }
}
