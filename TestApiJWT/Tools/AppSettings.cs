using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiJWT.Tools
{
    public class AppSettings
    {
        public string Secrect { get; set; }

        public AppSettings()
        {
            Secrect = "this is my custom Secret key for authnetication";
        }
    }
}
