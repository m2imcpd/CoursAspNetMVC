using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoursAngularApiRest.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoursAngularApiRest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private DataDbContext data;

        public ContactController(DataDbContext _data)
        {
            data = _data;
        }
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(data.Contacts.ToList());
        }
    }
}