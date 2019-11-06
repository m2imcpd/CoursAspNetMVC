using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorrectionApiRestDeezer.Models
{
    public class UserApi
    {
        private int id;
        private string firstName;
        private string lastName;
        private string email;
        private int role;
        private string password;
        private string token;
        private string avatarUrl;

        public int Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public int Role { get => role; set => role = value; }
        public string Password { get => password; set => password = value; }
        public string Token { get => token; set => token = value; }
        public string AvatarUrl { get => avatarUrl; set => avatarUrl = value; }

        [JsonIgnore]
        public virtual ICollection<Like> Likes { get; set; }

        [JsonIgnore]
        public virtual ICollection<PlayList> PlayLists { get; set; }

        public UserApi()
        {
            Likes = new List<Like>();
            PlayLists = new List<PlayList>();
        }
    }
}
