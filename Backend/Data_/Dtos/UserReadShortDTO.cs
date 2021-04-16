using System;
using System.Collections.Generic;
using System.Text;

namespace Data_.Dtos
{
    public class UserReadShortDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Xp { get; set; }
        public Guid? AvatarId { get; set; }
    }
}
