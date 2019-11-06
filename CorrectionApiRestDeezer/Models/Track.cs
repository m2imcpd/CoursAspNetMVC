using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorrectionApiRestDeezer.Models
{
    public class Track
    {
        private int id;
        private string title;
        private string artist;
        private string cover;
        private string trackUrl;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Artist { get => artist; set => artist = value; }
        public string Cover { get => cover; set => cover = value; }
        public string TrackUrl { get => trackUrl; set => trackUrl = value; }

        [JsonIgnore]
        public virtual ICollection<TrackPlayList> PlayLists { get; set; }

        public Track()
        {
            PlayLists = new List<TrackPlayList>();
        }
    }
}
