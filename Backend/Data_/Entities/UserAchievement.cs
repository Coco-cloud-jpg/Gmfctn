using System;
using System.Collections.Generic;
using System.Text;

namespace Data_.Entities
{
    public class UserAchievement: BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid AchievementId { get; set; }
        public Achievement Achievement { get; set; }
        public DateTime AddedTime { get; set; }
    }
}
