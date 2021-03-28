using Gmfctn.Entities;
using System.Collections.Generic;
namespace Gmfctn
{
    public interface IAchievementRepo
    {
        public IEnumerable<Achievement> GetAllAchievements();
        public Achievement GetAchievementById(int id);
    }
}
