using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data_.Entities
{
    public class RequestAchievement: BaseEntity
    {
        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(AchievementId))]
        public Guid AchievementId { get; set; }
        public Achievement Achievement { get; set; }
        public string Message { get; set; }
    }
}
