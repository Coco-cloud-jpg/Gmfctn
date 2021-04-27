using System;
using System.Collections.Generic;
using System.Text;

namespace Data_.Entities
{
    public class PasswordResetRequest: BaseEntity
    {
        public string Email { get; set; }
        public string Hash { get; set; }
    }
}
