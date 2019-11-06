using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorrectionApiRestDeezer.Models;
using CorrectionApiRestDeezer.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CorrectionApiRestDeezer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PlayListController : ControllerBase
    {
        private DataDbContext data;

        public PlayListController(DataDbContext _data)
        {
            data = _data;
        }

        [Route("/user/playlists")]
        [HttpGet]

        public ActionResult GetPlayListByUser()
        {
            int userId;
            Int32.TryParse((User.Claims.ToList()[1]).Value, out userId);
            return Ok(data.PlayLists.Where(x => x.UserId == userId));
        }

        [Route("/playlist/add")]
        [HttpPost]

        public ActionResult AddPlayList([FromBody] PlayList playList)
        {
            int userId;
            Int32.TryParse((User.Claims.ToList()[1]).Value, out userId);
            playList.UserId = userId;
            data.PlayLists.Add(playList);
            if (data.SaveChanges() > 0)
            {
                return Ok(new { error = false, playListId = playList.Id });
            }
            else
            {
                return StatusCode(500);
            }
        }


        [Route("/playlist/{playListId}/add/track/{trackId}")]
        [HttpGet]
        public ActionResult AddTrackToPlayList(int playListId, int trackId)
        {
            int userId;
            Int32.TryParse((User.Claims.ToList()[1]).Value, out userId);
            PlayList pl = data.PlayLists.SingleOrDefault(x => x.UserId == userId && x.Id == playListId);
            if (pl == null)
                return StatusCode(403);
            else
            {
                pl.Tracks.Add(new TrackPlayList { PlayListId = playListId, TrackId = trackId });
                if(data.SaveChanges() > 0)
                {
                    return Ok(new { error = false });
                }
                else
                {
                    return StatusCode(500);
                }
            }
        }

        [Route("/playlist/{playListId}/remove/track/{trackId}")]
        [HttpDelete]
        public ActionResult DeleteTrackFromPlayList(int playListId, int trackId)
        {
            int userId;
            Int32.TryParse((User.Claims.ToList()[1]).Value, out userId);
            PlayList pl = data.PlayLists.SingleOrDefault(x => x.UserId == userId && x.Id == playListId);
            if (pl == null)
                return StatusCode(403);
            else
            {
                pl.Tracks.Remove(data.TracksPlayLists.FirstOrDefault(x=>x.PlayListId == pl.Id && x.TrackId == trackId));
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
}