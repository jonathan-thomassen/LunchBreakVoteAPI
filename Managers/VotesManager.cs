using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchBreakVoteAPI.Models;

namespace LunchBreakVoteAPI.Managers
{
    public class VotesManager
    {
        private static int _nextId = 1;
        private static readonly List<Vote> Data = new List<Vote>();

        private static readonly List<string> StudentMailAddresses = new List<string>
        {
            "mail@mail.dk",
            "mail@mail2.dk",
            "abc@abc.com",
            "dk@dk.dk"
        };

        public List<Vote> GetAll()
        {
            return new List<Vote>(Data);
        }

        public List<Vote> GetAllByMail(string substring)
        {
            if (string.IsNullOrEmpty(substring))
                return new List<Vote>(Data);

            return Data.FindAll(vote => vote.Mail.ToLower().Contains(substring.ToLower()));
        }

        public List<Vote> GetAllByVote(int? choice)
        {
            if (!choice.HasValue)
                return new List<Vote>(Data);

            return Data.FindAll(vote => (int)vote.VoteChoice == choice);
        }

        public Vote GetById(int id)
        {
            return Data.Find(item => item.Id == id);
        }

        public Vote Add(Vote newVote)
        {
            if (Data.Exists(vote => vote.Mail == newVote.Mail)) return null;
            if (!StudentMailAddresses.Exists(mail => mail == newVote.Mail)) return null;

            newVote.Id = _nextId++;
            Data.Add(newVote);
            return newVote;
        }

        public Vote Delete(int id)
        {
            Vote vote = Data.Find(vote1 => vote1.Id == id);
            if (vote == null) return null;
            Data.Remove(vote);
            return vote;
        }

        public Vote Update(int id, Vote updates)
        {
            Vote vote = Data.Find(v => v.Id.Equals(id));
            if (vote == null)
                return null;

            vote.Mail = updates.Mail;
            vote.VoteChoice = updates.VoteChoice;

            return vote;
        }

        public void Wipe()
        {
            Data.Clear();
            _nextId = 1;
        }
    }
}
