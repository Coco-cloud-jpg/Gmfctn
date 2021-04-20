using Data_.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_.Dtos
{
    public class UserCreateDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid? AvatarId { get; set; }
        public string[] Roles { get; set; }
    }
}
