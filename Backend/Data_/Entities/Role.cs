using Data_.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_.Entities
{
    public class Role: BaseEntity
    {
        public RoleName RoleName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
