using System;
using System.Collections.Generic;
using System.IO;
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
    [Route("[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {

        private DataDbContext data;

        public TrackController(DataDbContext _data)
        {
            data = _data;
        }
        [Route("/tracks/{nb?}")]
        [HttpGet]
        public ActionResult GetAll(int? nb)
        {
            int nbre = (nb == null) ? 25 : (int)nb;
            return Ok(data.Tracks.OrderByDescending(d => d.Id).Take(nbre));
        }

        [Route("/track/{id}")]
        [HttpGet]
        public ActionResult Get(int id)
        {
            return Ok(data.Tracks.Find(id));
        }

        [Route("/Search-tracks/{search}")]
        [HttpGet]
        public ActionResult Search(string search)
        {
            return Ok(data.Tracks.Where(t => t.Title.Contains(search) || t.Artist.Contains(search)));
        }

        [Route("/Delete-track/{id}")]
        [HttpDelete]
        [Authorize]
        public ActionResult Delete(int id)
        {
            int roleId;
            Int32.TryParse((User.Claims.ToList()[2]).Value, out roleId);
            if (roleId > 1)
            {
                Track t = data.Tracks.Find(id);
                if (t != null)
                {
                    data.Tracks.Remove(t);
                    data.SaveChanges();
                    return Ok(new { error = false });
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return StatusCode(401);
            }
        }

        [Route("/track/add")]
        [HttpPost]
        [Authorize]
        public ActionResult Post()
        {
            int roleId;
            Int32.TryParse((User.Claims.ToList()[2]).Value, out roleId);
            if(roleId > 1)
            {
                IFormFile audio = Request.Form.Files[0];
                if (audio.Length > 0)
                {
                    Track track = new Track();
                    string pathFolder = Path.Combine("wwwroot", "track", Path.GetFileName(audio.FileName));
                    FileStream s = new FileStream(pathFolder, FileMode.Create);
                    track.TrackUrl = "http://" + Request.Host + "/track/" + Path.GetFileName(audio.FileName);
                    audio.CopyTo(s);
                    s.Close();
                    IFormFile cover = Request.Form.Files[1];
                    if (cover.Length > 0)
                    {
                        string pathFolderCover = Path.Combine("wwwroot", "cover", Path.GetFileName(cover.FileName));
                        s = new FileStream(pathFolderCover, FileMode.Create);
                        track.Cover = "http://" + Request.Host + "/cover/" + Path.GetFileName(cover.FileName);
                        cover.CopyTo(s);
                        s.Close();
                    }
                    track.Title = Request.Form["title"].ToString();
                    track.Artist = Request.Form["artist"].ToString();
                    data.Tracks.Add(track);
                    if (data.SaveChanges() > 0)
                    {
                        return Ok(new { error = false, trackId = track.Id });
                    }
                    else
                    {
                        return StatusCode(500);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return StatusCode(401);
            }
        }

        [Route("/tracks/playlist/{id}")]
        [HttpGet]
        [Authorize]
        public ActionResult GetTrackByPlayList(int id)
        {
            //var claims = User.Claims;
            int userId;
            Int32.TryParse((User.Claims.ToList()[1]).Value, out userId);
            PlayList pl = data.PlayLists.SingleOrDefault(x => x.UserId == userId && x.Id == id);
            if (pl != null)
                //Retourner la liste des tracks par playList
                return Ok(data.PlayLists.Include("Tracks").SingleOrDefault(x => x.Id == id).Tracks.Select(tr => data.Tracks.Find(tr.TrackId)));
            else
                return StatusCode(403);
        }
    }
}