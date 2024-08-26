using Coom.IBaseService;
using System.Reflection;

namespace WPF.Reflex.Service
{
    public class CheckDllService
    {
        public bool CheckDll(string dllName)
        {
            var typelist = Assembly.LoadFile(dllName)
                .GetTypes()
                .Where(x => x.IsAbstract == false)
                .ToList();
            bool hasProtocol = false;
            foreach (var type in typelist)
            {
                var has = type.GetInterfaces().Contains(typeof(IBaseProtocolService));
                if (!has)
                    continue;
                hasProtocol= true;
                break;
            }
            return hasProtocol;
        }
    }
}
