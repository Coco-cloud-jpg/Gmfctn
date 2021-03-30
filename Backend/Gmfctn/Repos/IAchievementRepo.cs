using Gmfctn.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gmfctn
{
    public interface IAchievementRepo
    {
        public IEnumerable<Achievement> GetAllAchievements();//
        public Achievement GetAchievementById(int id);//
    }
}
