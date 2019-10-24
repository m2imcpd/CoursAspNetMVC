using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursAspNet.Tools
{
    public class AdminRequirement : IAuthorizationRequirement
    {
        public string TypeProfil { get; set; }

        public AdminRequirement(string type)
        {
            TypeProfil = type;
        }
    }
}
