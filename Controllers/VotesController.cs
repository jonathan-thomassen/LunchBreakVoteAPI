using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LunchBreakVoteAPI.Managers;
using LunchBreakVoteAPI.Models;

namespace LunchBreakVoteAPI.Controllers
{
    [Route("api/[controller]")]
    // the controller is available on ..../api/items
    // [controller] means the name of the controller minus "Controller"
    [ApiController]
    public class VotesController : Controller
    {
        private readonly IVotesManager _manager;

        public VotesController(ItemContext context)
        {
            _manager = new VotesManagerSQL(context);
        }

        // GET: api/<ItemsController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<Vote>> Get()
        {
            List<Vote> votes = _manager.GetAll();
            if (votes.Count == 0)
                return NotFound("No votes found.");
            return Ok(votes);
        }

        // GET api/<ItemsController>/Id/
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Vote> Get(int id)
        {
            Vote vote = _manager.GetById(id);
            if (vote == null)
                return NotFound("No such vote, id: " + id);
            return Ok(vote);
        }

        // GET: api/<ItemsController>/Mail/
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("Mail/{substring}")]
        public ActionResult<IEnumerable<Vote>> GetMail(string substring)
        {
            List<Vote> foundVotes = _manager.GetAllByMail(substring);
            if (foundVotes != null)
                return Ok(foundVotes);
            return NotFound("No votes found.");
        }

        // GET: api/<ItemsController>/Vote/
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("Choice/{choice}")]
        public ActionResult<IEnumerable<Vote>> GetVote(int? choice)
        {
            List<Vote> foundVotes = _manager.GetAllByVote(choice);
            if (foundVotes != null)
                return Ok(foundVotes);

            return NotFound("No votes found.");
        }

        // POST api/<ItemsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Vote> Post([FromBody] Vote vote)
        {
            Vote postedVote = _manager.Add(vote);
            if (postedVote != null)
            {
                return Created($"/{vote.Id}", postedVote);
            }

            return BadRequest();
        }

        // PUT api/<ItemsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<Vote> Put(int id, [FromBody] Vote vote)
        {
            Vote updatedVote = _manager.Update(id, vote);
            if (updatedVote != null)
                return Ok(updatedVote);

            return NotFound();
        }

        // DELETE api/<ItemsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Vote> Delete(int id)
        {
            Vote deletedVote = _manager.Delete(id);
            if (deletedVote != null)
                return Ok(deletedVote);

            return NotFound();
        }
    }
}