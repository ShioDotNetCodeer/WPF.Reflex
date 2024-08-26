using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using WPF.Reflex.Messenge;
using WPF.Reflex.Service;

namespace WPF.Reflex.ViewModels
{
    public partial class RegisterViewViewModel : ObservableObject
    {
        private readonly CheckDllService checkDllService;

        public RegisterViewViewModel(CheckDllService checkDllService)
        {
            this.checkDllService = checkDllService;

        }

        public string Title { get; set; } = "注册DLL";


        [ObservableProperty]
        private ObservableCollection<string> dllPaths;

        [ObservableProperty]
        private ObservableCollection<string> logs;


        [RelayCommand]
        public void Register(Window window)
        {
            if (DllPaths.Count == 0)
            {
                MessageBox.Show("无可用数据", "提示", MessageBoxButton.OK);
                window.Close();
                return;
            }

            var dir = Path.GetDirectoryName(DllPaths[0]);
            if (dir != App.Current.DllPath)
            {
                foreach (var dllpath in DllPaths)
                {
                    string fileName = Path.GetFileName(dllpath);
                    string newPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                    if (!File.Exists(newPath))
                    {
                        File.Copy(dllpath, newPath);
                        Logs.Add($"{fileName}已添加");
                    }
                    else
                    {
                        if(GetFileHash(dllpath) != GetFileHash(newPath))
                        {
                            File.Copy(dllpath, newPath,true);
                        }
                    }
                }
                window.Close();
                WeakReferenceMessenger.Default.Send(new DllListChangeMessenger());
            }
        }


        [RelayCommand]
        public void SelectDLL()
        {
            Logs = new ObservableCollection<string>();
            DllPaths = new ObservableCollection<string>();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "动态链接库|*.dll";
            if (ofd.ShowDialog() == false)
            {
                return;
            }
            var dllpaths = ofd.FileNames;
            Logs.Add($"共{dllpaths.Length}个文件");
            foreach (var dllpath in dllpaths)
            {
                if (checkDllService.CheckDll(dllpath))
                {
                    DllPaths.Add(dllpath);
                }
            }
            Logs.Add($"{DllPaths.Count}个文件可用");
            
        }

        private string GetFileHash(string filePath)
        {
            using (MD5 hashAlgorithm = MD5.Create())
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] hashBytes = hashAlgorithm.ComputeHash(fileStream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}
