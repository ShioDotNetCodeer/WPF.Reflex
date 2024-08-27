using Coom.IBaseService;
using System.Collections.Generic;
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
            var files = Directory.GetFiles(_dllPath);
            CoomContainers = CoomContainerHelper.GetTypes(files);
        }
        #endregion
    }
}
