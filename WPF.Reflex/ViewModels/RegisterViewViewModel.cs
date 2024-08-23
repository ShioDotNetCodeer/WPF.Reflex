using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace WPF.Reflex.ViewModels
{
    public partial class RegisterViewViewModel:ObservableObject
    {
        public string Title { get; set; } = "注册DLL";

        [ObservableProperty]
        private string path;

        public void Register()
        {

        }

        [RelayCommand]
        public void SelectDlL()
        {
            FileDialog dialog = new FileDialog();
        }
    }
}
