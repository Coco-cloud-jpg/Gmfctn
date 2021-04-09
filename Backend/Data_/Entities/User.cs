using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data_.Entities
{
    public class User: BaseEntity
    {
        [Required]
        [MaxLength(250)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(32)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(32)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(32)]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z]{8,32}$")]
        public string Password { get; set; }
        [MaxLength(250)]
        public string Status { get; set; }
        public int Xp { get; set; }
        public Guid? AvatarId { get; set; }
        [Required]
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserAchievement> UserAchievements { get; set; }
    }
}
