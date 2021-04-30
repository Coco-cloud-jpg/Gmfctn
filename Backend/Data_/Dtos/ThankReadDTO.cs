using Data_.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_.Dtos
{
    public class ThankReadDTO
    {
        public Guid ToUserId { get; set; }
        public UserReadShortDTO FromUser { get; set; }
        public string Text { get; set; }
        public string AddedTime { get; set; }
    }
}
