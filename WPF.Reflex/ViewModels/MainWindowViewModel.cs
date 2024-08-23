using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Coom.IBaseService;
using System.IO;
using System.Reflection;
using System.Windows;
using WPF.Reflex.Service;
using WPF.Reflex.Views;

namespace WPF.Reflex.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private InstancesService service;
        public List<CoomContainer>? CoomContainers;
        private InstancesService _instancesService;
        private readonly DialogService _dialogService;
        public MainWindowViewModel(InstancesService service, DialogService dialogService)
        {
            _instancesService = service;
            CoomContainers = service.GetCoomContainer();
            _dialogService = dialogService;
        }



        [RelayCommand]
        public void RegisterDll()
        {
            _dialogService.ShowDialog<RegisterView>();
        }

        [RelayCommand]
        public void ReLoadDll()
        {
            CoomContainers = service.ReLoad();
            MessageBox.Show("重新载入完成","提示");
        }


    }
}
