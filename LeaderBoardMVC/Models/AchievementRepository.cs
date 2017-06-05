using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LeaderBoardMVC.EntityDataModel;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace LeaderBoardMVC.Models
{
    public class AchievementRepository
    {
        private static Object lockObject = new Object();
        private static LeaderBoardDataEntities _achieveDb;
        private static LeaderBoardDataEntities AchieveDb
        {
            get
            {
                lock (lockObject)
                { 
                    return _achieveDb ?? (_achieveDb = new LeaderBoardDataEntities());
                }
            }
        }

        public static IEnumerable<AchievementViewItem> GetAchievementViewItems(int gameId, int timeIntervalId)
        {
            lock (lockObject)
            {
                DateTime dt;
                switch (timeIntervalId)
                {
                    case 1:
                        dt = DateTime.Today.Date.AddDays(-1);
                        break;
                    case 2:
                        dt = DateTime.Today.Date.AddDays(-7);
                        break;
                    default:
                        dt = DateTime.MinValue;
                        break;
                }

                var list = (from a in AchieveDb.Achievements
                            where ((a.GameId == gameId) && (a.DateTime > dt))
                            select new AchievementViewItem {
                                Player = a.Players.Name,
                                Achieve = a.Achieve,
                                DateTime = a.DateTime }).ToList();

                list.Sort((emp1, emp2) => emp2.Achieve.CompareTo(emp1.Achieve));
                return list;
            }
        }

        public static IEnumerable<GameViewItem> GetGameViewItems()
        {
            lock (lockObject)
            {
                var query = AchieveDb.Games.Select(a => new GameViewItem { Id = a.Id, Name = a.Name });
                var list = query.ToList();
                list.Sort((emp1, emp2) => emp1.Name.CompareTo(emp2.Name));
                return list;
            }
        }
    }
}