using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CorrectionApiRestDeezer.Models
{
    public class PlayList
    {
        private int id;
        private string title;
        private int userId;
        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }

        public virtual ICollection<TrackPlayList> Tracks { get; set; }

        [ForeignKey("UserId")]
        public virtual UserApi User { get; set; }
        public int UserId { get => userId; set => userId = value; }

        public PlayList()
        {
            Tracks = new List<TrackPlayList>();
        }
    }
}
