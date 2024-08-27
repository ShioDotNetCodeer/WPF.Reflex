using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Coom.IBaseService;
using System.Collections.ObjectModel;
using System.Reflection;

namespace WPF.Reflex.ViewModels
{
    public partial class CoomContainerViewModel : ObservableObject
    {
        [ObservableProperty]
        private string dllName = "";

        [ObservableProperty]
        private ObservableCollection<InstancesTypeViewModel>? allTypes;
    }

    public partial class InstancesTypeViewModel : ObservableObject
    {
        public IBaseProtocolService? Instances { get; set; }

        public string? Alias { get; set; }

        public int FuncCode { get; set; }

        public bool IsRequest { get; set; }

        public List<PropertyInfo>? Propertys { get; set; }

        [ObservableProperty]
        private ObservableCollection<AliasPropertyInfoViewModel>? aliasPropertyInfos;

        /// <summary>
        /// 协议字符串
        /// </summary>
        [ObservableProperty]
        private string protocolStr;

        [RelayCommand]
        public void Exec()
        {
            var list = AliasPropertyInfos.Select(x => new AliasPropertyInfo
            {
                Alias = x.Alias,
                MaxValue = x.MaxValue,
                MinValue = x.MinValue,
                PropertyName = x.PropertyName,
                Value = x.Value
            }).ToList();
            Instances = CoomContainerHelper.SetValues(Instances, list);
            var array = Instances.Transform2Protocol();
            ProtocolStr = string.Join(" ", array.Select(x => x.ToString("X2")));
        }
    }
    public partial class AliasPropertyInfoViewModel : ObservableObject
    {
        [ObservableProperty]
        private string propertyName;
        /// <summary>
        /// 别名
        /// </summary>
        [ObservableProperty]
        private string? alias;

        /// <summary>
        /// 值
        /// </summary>
        [ObservableProperty]
        private ulong value;

        /// <summary>
        /// 最小值
        /// 为自动化测试随机使用
        /// </summary>
        [ObservableProperty]
        private long minValue;

        /// <summary>
        /// 最大值
        /// 为自动化测试随机使用
        /// </summary>
        [ObservableProperty]
        private long maxValue;
    }
}
