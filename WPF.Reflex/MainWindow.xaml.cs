using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPF.Reflex.ViewModels;
namespace WPF.Reflex
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = App.Current.Services.GetService<MainWindowViewModel>(); 
        }
        private bool isDarkMode = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isDarkMode)
            {
                SwitchToLightMode();
            }
            else
            {
                SwitchToDarkMode();
            }
            isDarkMode = !isDarkMode;
        }

        public void SetTheme(Uri themeUri)
        {
            // 清空当前资源字典
            ResourceDictionary newTheme = new ResourceDictionary { Source = themeUri };
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(newTheme);
        }

        private void SwitchToDarkMode()
        {
            SetTheme(new Uri("Theme/DarkTheme.xaml", UriKind.Relative));
        }
        private void SwitchToLightMode()
        {
            SetTheme(new Uri("Theme/LightTheme.xaml", UriKind.Relative));
        }
    }
}