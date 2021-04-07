using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data_.Validators
{
    public class GreaterThanZeroAttribute: ValidationAttribute
    {
        public GreaterThanZeroAttribute() {
        }
        public override bool IsValid(object value)
        {
            
            /*if (value is uint) 
            {
                var temp = (uint)value;

                return temp <= 0 ? false : true;
            }*/
            return false;
        }
    }
}
