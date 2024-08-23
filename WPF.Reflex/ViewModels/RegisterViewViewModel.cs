using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace WPF.Reflex.ViewModels
{
    public partial class RegisterViewViewModel : ObservableObject
    {
        public string Title { get; set; } = "注册DLL";

        [ObservableProperty]
        private string path;

        [ObservableProperty]
        private string selectBtnText="请选择";

        public void Register()
        {

        }

        [RelayCommand]
        public void SelectDLL()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "动态链接库|*.dll";
            if (ofd.ShowDialog() == true)
            {
                var dllpaths = ofd.FileNames;
                SelectBtnText = $"共{dllpaths.Length}个文件";
            }
        }
    }
}
