using Gmfctn.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gmfctn.Repos
{
    public class MockAchievementRepo : IAchievementRepo
    {
        List<Achievement> repo = new List<Achievement>(){
            new Achievement{ Name = "First",Description = "First achievement", Xp = 10 ,IconId = 12,Id=1 },
            new Achievement{ Name = "Second",Description = "Second achievement", Xp = 15 ,IconId = 11,Id=2 },
            new Achievement{ Name = "Third",Description = "Third achievement", Xp = 100 ,IconId = 123,Id=3 },
            new Achievement{ Name = "Fourth",Description = "Fourth achievement", Xp = 34 ,IconId = 134,Id=4 },
            new Achievement{ Name = "Fifth",Description = "Fifth achievement", Xp = 11 ,IconId = 112,Id=5 },
        };
        public Achievement GetAchievementById(int id)
        {
            return repo.FirstOrDefault(obj=>obj.Id ==id);
        }

        public IEnumerable<Achievement> GetAllAchievements()
        {
            return repo;
        }
    }
}
