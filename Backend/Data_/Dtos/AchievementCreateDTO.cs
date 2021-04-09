using System;

namespace Data_.Dtos
{
    public class AchievementCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public uint Xp { get; set; }
        public Guid IconId { get; set; }
    }
}
