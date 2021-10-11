using System.Collections.Generic;
using LunchBreakVoteAPI.Models;

namespace LunchBreakVoteAPI.Managers
{
    public interface IVotesManager
    {
        public List<Vote> GetAll();

        public List<Vote> GetAllByMail(string substring);

        public List<Vote> GetAllByVote(int? choice);

        public Vote GetById(int id);

        public Vote Add(Vote newVote);

        public Vote Delete(int id);

        public Vote Update(int id, Vote voteUpdate);

        public void Wipe();
    }
}
