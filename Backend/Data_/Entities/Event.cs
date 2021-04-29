using Data_.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data_.Entities
{
    public class Event : BaseEntity
    {
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public EventType Type { get; set; }
        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
