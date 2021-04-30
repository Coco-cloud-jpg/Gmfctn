using System;
using System.Collections.Generic;
using System.Text;
using Data_.Constants;

namespace Data_.Dtos
{
    public class EventReadDTO
    {
        public string Description { get; set; }
        public string CreatedTime { get; set; }
        public EventType Type { get; set; }
        public Guid UserId { get; set; }
        public UserReadDTO User { get; set; }
    }
}
