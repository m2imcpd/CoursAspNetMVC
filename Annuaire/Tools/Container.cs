using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Annuaire.Tools
{   
    //Simple container for IC
    public class Container
    {
        private Dictionary<Type, Type> dicoContainer;

        private static Container instance = null;
        private static object _lock = new object();

        public static Container Instance
        {
            get
            {
                lock(_lock)
                {
                    if (instance == null)
                        instance = new Container();
                    return instance;
                }

            }
        }
        private Container()
        {
            dicoContainer = new Dictionary<Type, Type>();
        }

        public  void Register<T, TypeClass>() where  TypeClass : T
        {
            var typeExist = dicoContainer.FirstOrDefault((element) => element.Key == typeof(T));
            if(typeExist.Key == default(Type))
            {
                dicoContainer.Add(typeof(T), typeof(TypeClass));
            }
            else
            {
                dicoContainer[typeof(T)] = typeof(TypeClass);
            }
        }

        public object Resolver<T>()
        {
            Type typefound;
            if(dicoContainer.TryGetValue(typeof(T), out typefound)){
                return Activator.CreateInstance(typefound); 
            }
            else
            {
                throw new Exception("No Interface in container");
            }
        }
    }
}
