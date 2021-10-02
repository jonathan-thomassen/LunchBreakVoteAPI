using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchBreakVoteAPI.Models
{
    public enum VoteChoices
    {
        Yay,
        Nay,
        Blank
    }

    public class Vote
    {   
        public int Id { get; set; }
        public string Mail { get; set; }
        public VoteChoices VoteChoice { get; set; }
    }
}
