using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace WPF.Reflex.Service
{
    public class DialogService
    {
        private readonly IServiceProvider _serviceProvider;

        public DialogService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowDialog<T>() where T : Window
        {
            var dialog = _serviceProvider.GetService<T>();
            dialog?.ShowDialog();
        }
    }
}
