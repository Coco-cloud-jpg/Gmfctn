using System;
using System.Collections.Generic;
using System.Text;

namespace Data_.Dtos
{
    public class RequestBody
    {
        public string Message { get; set; }
        public Guid AchievementId { get; set; }
        public object Headers { get; set; }
    }
}
