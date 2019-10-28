using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoursAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace CoursAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [Route("toto")]
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] Personne p)
        {
            return Ok(new { nom = p.Nom, prenom = p.Prenom});
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Personne p)
        {
            return Ok(new { id= id, nom = p.Nom, prenom = p.Prenom });
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return NotFound();
        }
    }
}
