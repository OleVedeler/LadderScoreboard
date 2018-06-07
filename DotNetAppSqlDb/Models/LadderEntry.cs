using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetAppSqlDb.Models
{
    public class LadderEntry
    {
        public int ID { get; set; }

        public string ArmyList { get; set; }

        public string Army { get; set; }

        public string PlayerName { get; set; }

        public string Rank { get; set; }
    }
}