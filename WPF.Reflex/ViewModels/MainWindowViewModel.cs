using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Coom.IBaseService;
using System.Collections.ObjectModel;
using System.Windows;
using WPF.Reflex.Messenge;
using WPF.Reflex.Service;
using WPF.Reflex.Views;

namespace WPF.Reflex.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {

        [ObservableProperty]
        private ObservableCollection<CoomContainerViewModel>? coomContainers;
        [ObservableProperty]
        private InstancesTypeViewModel? current;
        private InstancesService _instancesService;
        private readonly DialogService _dialogService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="dialogService"></param>
        public MainWindowViewModel(InstancesService service, DialogService dialogService)
        {
            _instancesService = service;
            _dialogService = dialogService;
            Init();
            WeakReferenceMessenger.Default.Register<DllListChangeMessenger>(this, (o, m) =>
            {
                ReLoadDll();
            });
        }



        [RelayCommand]
        public void RegisterDll()
        {
            _dialogService.ShowDialog<RegisterView>();
        }

        [RelayCommand]
        public void ReLoadDll()
        {
            Init();
            MessageBox.Show("重新载入完成", "提示");
        }

        private void Init()
        {
            var list = _instancesService.GetCoomContainer();
            CoomContainers = new ObservableCollection<CoomContainerViewModel>();
            foreach (var dll in list)
            {
                CoomContainerViewModel model = new CoomContainerViewModel();
                model.DllName = dll.DllName;
                model.AllTypes = new ObservableCollection<InstancesTypeViewModel>();
                foreach (var type in dll.AllTypes)
                {
                    var d = new InstancesTypeViewModel
                    {
                        Alias = type.Alias,
                        FuncCode = type.FuncCode,
                        Instances = type.Instances,
                        IsRequest = type.IsRequest,
                        Propertys = type.Propertys,
                    };
                    d.AliasPropertyInfos = new ObservableCollection<AliasPropertyInfoViewModel>();
                    foreach (var info in type.AliasPropertyInfos)
                    {
                        d.AliasPropertyInfos.Add(new AliasPropertyInfoViewModel
                        {
                            PropertyName= info.PropertyName,
                            Alias = info.Alias,
                            MinValue = info.MinValue,
                            MaxValue = info.MaxValue,
                            Value = info.Value,
                        });
                    }
                    model.AllTypes.Add(d);
                }
                CoomContainers.Add(model);
            }
        }




        [RelayCommand]
        public void Exec(InstancesTypeViewModel instances)
        {
            Current = instances;
        }
    }
}
