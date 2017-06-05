//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LeaderBoardMVC.EntityDataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Achievement
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public int Achieve { get; set; }
        public System.DateTime DateTime { get; set; }
    
        public virtual Game Games { get; set; }
        public virtual Player Players { get; set; }
    }

    public class AchievementViewItem
    {
        private string dateTimeString;
        public string Player { get; set; }
        public int Achieve { get; set; }
        public DateTime DateTime {
            set { dateTimeString = value.ToString("MM/dd/yy H:mm:ss"); }
        }
        public string DateTimeString {
            get { return dateTimeString; }
        }
    }
    
}
