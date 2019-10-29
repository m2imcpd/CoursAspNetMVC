using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoursAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        [Route("/upload")]
       
        public ActionResult Upload()
        {
            var file = Request.Form.Files[0];
            if(file.Length > 0)
            {
                var pathFolder = Path.Combine("images", Path.GetFileName(file.FileName));
                FileStream s = new FileStream(pathFolder, FileMode.Create);
                file.CopyTo(s);
                return Ok(new { message = pathFolder });
            }
            else
            {
                return Ok(new { message = "upload error" });
            }
            
        }
    }
}