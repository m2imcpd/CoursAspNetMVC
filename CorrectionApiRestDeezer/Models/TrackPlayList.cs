using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CorrectionApiRestDeezer.Models
{
    public class TrackPlayList
    {
        private int id;
        private int trackId;
        private int playListId;
        public int Id { get => id; set => id = value; }

        [ForeignKey("TrackId")]
        public virtual Track Track { get; set; }

        [ForeignKey("PlayListId")]
        public virtual PlayList PlayList { get; set; }
        public int TrackId { get => trackId; set => trackId = value; }
        public int PlayListId { get => playListId; set => playListId = value; }
    }
}
