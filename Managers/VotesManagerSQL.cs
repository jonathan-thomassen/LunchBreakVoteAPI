using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchBreakVoteAPI.Models;
using OpenQA.Selenium.Support.UI;

namespace LunchBreakVoteAPI.Managers
{
    public class VotesManagerSQL : IVotesManager
    {
        private static ItemContext _context;

        private static readonly List<string> StudentMailAddresses = new List<string>
        {
            "mail@mail.dk",
            "mail@mail2.dk",
            "abc@abc.com",
            "dk@dk.dk"
        };

        public VotesManagerSQL(ItemContext context)
        {
            _context = context;
        }

        public List<Vote> GetAll()
        {
            if (_context != null)
                return _context.Votes.ToList();

            return null;
        }

        public List<Vote> GetAllByMail(string substring)
        {
            IEnumerable<Vote> votes = from vote in _context.Votes where vote.Mail.ToLower().Contains(substring.ToLower()) select vote;
            return votes.ToList();
        }

        public List<Vote> GetAllByVote(int? choice)
        {
            IEnumerable<Vote> votes = from vote in _context.Votes where vote.VoteChoice.Equals(choice) select vote;
            return votes.ToList();
        }

        public Vote GetById(int id)
        {
            return _context.Votes.Find(id);
        }

        public Vote Add(Vote newVote)
        {
            Vote addedVote = _context.Votes.Add(newVote).Entity;
            _context.SaveChanges();
            return addedVote;
        }

        public Vote Delete(int id)
        {
            if (_context.Votes.Find(id) != null)
            {
                Vote deletedVote = _context.Votes.Remove(_context.Votes.Find(id)).Entity;
                _context.SaveChanges();
                return deletedVote;
            }

            return null;
        }

        public Vote Update(int id, Vote voteUpdate)
        {
            if (_context.Votes.Find(id) != null)
            {
                Vote oldVote = _context.Votes.Find(id);
                oldVote.Mail = voteUpdate.Mail;
                oldVote.VoteChoice = voteUpdate.VoteChoice;

                Vote updatedVote = _context.Votes.Update(oldVote).Entity;
                _context.SaveChanges();
                return updatedVote;
            }

            return null;
        }

        public void Wipe()
        {
            _context.RemoveRange(_context.Votes);
            _context.SaveChanges();
        }
    }
}
