using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorrectionApiRestDeezer.Tools
{
    public interface ILoginService
    {
        string Login(string email, string password);
    }
}
