using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gmfctn.Entities
{
    public class Achievement: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Xp { get; set; }
        public int IconId { get; set; }

    }
}
