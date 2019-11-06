using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorrectionApiRestDeezer.Models;
using CorrectionApiRestDeezer.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CorrectionApiRestDeezer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LikeController : ControllerBase
    {
        private DataDbContext data;

        public LikeController(DataDbContext _data)
        {
            data = _data;
        }

        [Route("/user/likes")]
        [HttpGet]
        public ActionResult GetLikeByUser()
        {
            int userId;
            Int32.TryParse((User.Claims.ToList()[1]).Value, out userId);
            return Ok(data.Likes.Include("Track").Where(l => l.UserId ==userId));
        }

        [Route("/like/track/add/{trackId}")]
        [HttpGet]
        public ActionResult AddLikedTrack(int trackId)
        {
            int userId;
            Int32.TryParse((User.Claims.ToList()[1]).Value, out userId);
            Like l = new Like { TrackId = trackId, UserId = userId };
            data.Likes.Add(l);
            if(data.SaveChanges() > 0)
            {
                return Ok(new { error = false, likeId = l.Id });
            }
            else
            {
                return StatusCode(500);
            }
        }
        [Route("/like/track/remove/{trackId}")]
        [HttpDelete]
        public ActionResult RemoveLikedTrack(int trackId)
        {
            int userId;
            Int32.TryParse((User.Claims.ToList()[1]).Value, out userId);
            data.Likes.Remove(data.Likes.FirstOrDefault(x => x.UserId == userId && x.TrackId == trackId));
            if (data.SaveChanges() > 0)
            {
                return Ok(new { error = false });
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}