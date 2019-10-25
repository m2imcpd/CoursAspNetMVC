using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Tools
{
    public class AccessRequirement : IAuthorizationRequirement
    {
        private int typeProfil;

        public AccessRequirement(int t)
        {
            TypeProfil = t;
        }

        public int TypeProfil { get => typeProfil; set => typeProfil = value; }
    }
}
