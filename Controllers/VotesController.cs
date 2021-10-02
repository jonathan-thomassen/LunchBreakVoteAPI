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
        private readonly VotesManager _manager = new VotesManager();

        // GET: api/<ItemsController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<Vote>> Get()
        {
            List<Vote> votes = _manager.GetAll();

            if (votes.Count == 0) return NotFound("No votes found.");

            return Ok(votes);
        }

        // GET api/<ItemsController>/Id/
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Vote> Get(int id)
        {
            Vote vote = _manager.GetById(id);

            if (vote == null) return NotFound("No such vote, id: " + id);

            return Ok(vote);
        }

        // GET: api/<ItemsController>/Mail/
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("Mail/{substring}")]
        public ActionResult<IEnumerable<Vote>> GetMail(string substring)
        {
            return _manager.GetAllByMail(substring);
        }

        // GET: api/<ItemsController>/Vote/
        [HttpGet("Choice/{choice}")]
        public IEnumerable<Vote> GetVote(int? choice)
        {
            return _manager.GetAllByVote(choice);
        }

        // POST api/<ItemsController>
        [HttpPost]
        public Vote Post([FromBody] Vote value)
        {
            return _manager.Add(value);
        }

        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        public Vote Put(int id, [FromBody] Vote value)
        {
            return _manager.Update(id, value);
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public Vote Delete(int id)
        {
            return _manager.Delete(id);
        }
    }
}