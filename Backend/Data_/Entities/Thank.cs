using Data_.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data_.Entities
{
    public class Thank: BaseEntity
    {

        [ForeignKey(nameof(ToUserId))]
        public Guid ToUserId { get; set; }
        public User FromUser { get; set; }
        public string Text { get; set; }
        public DateTime AddedTime { get; set; }
    }
}
