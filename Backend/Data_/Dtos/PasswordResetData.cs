using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Data_.Dtos
{
    public class PasswordResetData
    {
        public string NewPassword { get; set; }
        public string Hash { get; set; }
    }
}
