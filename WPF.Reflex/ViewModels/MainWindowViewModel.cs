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
        private ObservableCollection<CoomContainer>? coomContainers;
        private InstancesService _instancesService;
        private readonly DialogService _dialogService;
        public MainWindowViewModel(InstancesService service, DialogService dialogService)
        {
            _instancesService = service;
            _dialogService = dialogService;
            CoomContainers = new ObservableCollection<CoomContainer>(_instancesService.GetCoomContainer());
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
            CoomContainers = new ObservableCollection<CoomContainer>(_instancesService.ReLoad());
            MessageBox.Show("重新载入完成", "提示");
        }


        [ObservableProperty]
        private InstancesType current;


        [RelayCommand]
        public void Exec(InstancesType instances)
        {
            Current = instances;
        }
    }
}
