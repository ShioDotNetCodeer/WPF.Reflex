using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPF.Reflex.ViewModels;

namespace WPF.Reflex.Views
{
    /// <summary>
    /// RegisterView.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterView : Window
    {
        public RegisterView()
        {
            InitializeComponent();
            this.DataContext = App.Current.Services.GetService<RegisterViewViewModel>();
        }
    }
}
