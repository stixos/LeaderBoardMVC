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
    public class GameController : ApiController
    {
        // GET api/Game/5/1/string
        [Route("api/Game/{sessionId}")]
        public IEnumerable<GameViewItem> Get(string sessionId)
        {
            return AchievementRepository.GetGameViewItems();
        }

    }
}
