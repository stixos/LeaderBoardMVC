using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LeaderBoardMVC.EntityDataModel;
using LeaderBoardMVC.Models;

namespace LeaderBoardMVC.Controllers
{
    public class AchievementController : ApiController
    {
        // GET api/Achievement/5/1/string
        [Route("api/Achievement/{gameId}/{timeIntervalId}/{sessionId}")]
        public IEnumerable<AchievementViewItem> Get(int gameId, int timeIntervalId, string sessionId)
        {
            return AchievementRepository.GetAchievementViewItems(gameId, timeIntervalId);
        }
    }
}
