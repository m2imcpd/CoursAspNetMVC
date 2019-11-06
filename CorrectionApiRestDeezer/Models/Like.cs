using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CorrectionApiRestDeezer.Models
{
    public class Like
    {
        private int id;

        private int trackId;

        private int userId;
        public int Id { get => id; set => id = value; }
       
        [ForeignKey("TrackId")]
        public virtual Track Track { get; set; }

        [JsonIgnore]
        [ForeignKey("UserId")]
        public virtual UserApi User { get; set; }
        public int TrackId { get => trackId; set => trackId = value; }
        public int UserId { get => userId; set => userId = value; }
    }
}
