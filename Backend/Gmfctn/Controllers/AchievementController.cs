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
        private readonly IAchievementRepo repo;

        public AchievementController() {
            repo = new MockAchievementRepo();
        }
        [HttpGet]
        public ActionResult<IEnumerable<Achievement>> GetAllAchievement()
        {
            IEnumerable<Achievement> Items = null;
            try
            {
                Items = repo.GetAllAchievements();
                if (Items == null)
                    return NotFound();
                else
                    return Ok(Items);
            }
            catch(Exception exc) {
                throw exc;
            }
            
        }
        [HttpGet("{id}")]
        public ActionResult<Achievement> GetAchivementById(int id)
        {
            try {
                var Item = repo.GetAchievementById(id);
                if (Item != null)
                {
                    return Ok(Item);
                }
                return NotFound();
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }
    }
}
