using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursAspNet.Tools
{
    public class ServicePanier : IServicePanier
    {
        private IHttpContextAccessor accessor;
        //private ISession session { get => accessor.HttpContext.Session; }
        private ISession session => accessor.HttpContext.Session;
        public ServicePanier(IHttpContextAccessor _httpContextAccessor)
        {
            accessor = _httpContextAccessor;
            //accessor.HttpContext.Response
        }
        public void AjouterProduit()
        {

        }
    }
}
