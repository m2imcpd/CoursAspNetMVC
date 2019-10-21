using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Annuaire.Tools
{
    public class ClassB
    {
        public IInterfaceA pA;

        public ClassB()
        {
            pA = (IInterfaceA)Container.Instance.Resolver<IInterfaceA>();
        }
    }
}
