using Gmfctn.Entities;
using Gmfctn.Repos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gmfctn.Controllers
{
    [Route("api/achievement")]
    [ApiController]
    public class AchievementController : ControllerBase
    {
        private IAchievementRepo repo;

        public AchievementController() {
            repo = new MockAchievementRepo();
        }
        [HttpGet]
        public ActionResult<IEnumerable<Achievement>> GetAllAchievement()
        {
            var Items = repo.GetAllAchievements();
            return Ok(Items);
        }
        [HttpGet("{id}")]
        public ActionResult<Achievement> GetAchivementById(int id)
        {
            var Item = repo.GetAchievementById(id);
            if (Item != null)
            {
                return Ok(Item);
            }
            return NotFound();
        }
    }
}
