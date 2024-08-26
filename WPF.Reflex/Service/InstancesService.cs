using Coom.IBaseService;
using System.IO;
using System.Reflection;

namespace WPF.Reflex.Service
{
    public class InstancesService
    {
        private List<CoomContainer>? CoomContainers;
        private readonly string _dllPath;
        public InstancesService()
        {
            _dllPath = App.Current.DllPath;
            Init();
        }


        public List<CoomContainer>? GetCoomContainer()
        {
            return CoomContainers;
        }

        public List<CoomContainer>? ReLoad()
        {
            Init();
            return CoomContainers;
        }

        #region private
        private void Init()
        {
            CoomContainers = new List<CoomContainer>();
            var files = Directory.GetFiles(_dllPath);
            foreach (var file in files)
            {
                var coomContainer = new CoomContainer();
                coomContainer.AllTypes = new List<InstancesType>();
                coomContainer.DllName = Path.GetFileNameWithoutExtension(file).Replace("Coom.", "").Replace("Service", "");
                var typelist = Assembly.LoadFile(file).GetTypes().Where(x => x.IsAbstract == false).ToList();
                foreach (var type in typelist)
                {
                    var has = type.GetInterfaces().Contains(typeof(IBaseProtocolService));
                    if (!has)
                        continue;

                    var fi = type.GetCustomAttribute<FuncInfoAttribute>();
                    if (fi == null)
                        continue;

                    InstancesType dllType = new InstancesType();
                    dllType.Alias = fi.FuncName;
                    dllType.IsRequest = fi.IsRequest;
                    dllType.FuncCode = fi.FuncCode;
                    dllType.Instances = Activator.CreateInstance(type) as IBaseProtocolService; ;
                    dllType.Propertys = GetPropertyAndSort(type);
                    coomContainer.AllTypes.Add(dllType);
                }
                CoomContainers.Add(coomContainer);
            }
        }

        private List<PropertyInfo>? GetPropertyAndSort(Type type)
        {

            var allProperty = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (allProperty == null)
                return null;
            List<SortPropertyInfo> propertyInfos = new List<SortPropertyInfo>(allProperty.Length);
            var arr = new PropertyInfo[allProperty.Length];
            foreach (var property in allProperty)
            {
                var att = property.GetCustomAttribute<PropertyControlAttribute>();
                if (att == null)
                    continue;
                SortPropertyInfo sortPropertyInfo = new SortPropertyInfo();
                sortPropertyInfo.Property = property;
                sortPropertyInfo.Index = att.Index;
                propertyInfos.Add(sortPropertyInfo);
            }
            return propertyInfos.OrderBy(x => x.Index)
                .Select(x => x.Property)
                .ToList();
        }
        #endregion
    }
}
