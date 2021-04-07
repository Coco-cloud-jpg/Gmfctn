using Data_.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_.Dtos
{
    public class UserUpdateDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
    }
}
