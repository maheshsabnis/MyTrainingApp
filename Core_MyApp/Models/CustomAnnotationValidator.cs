using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core_MyApp.Models
{
    public class NumericPositiveValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int v = Convert.ToInt32(value);
            if (v < 0) return false;
            return true;
        }
    }
}
