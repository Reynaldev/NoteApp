using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApp.ViewModels
{
    internal class AboutViewModel
    {
        public string Title => AppInfo.Name;
        public string Version => AppInfo.VersionString;
        public string MoreInfoUrl => "https://aka.ms/maui";
        public string Message => "This app is written in XAML and C# with .NET MAUI.";
        public ICommand ShowMoreInfoCmd { get; }

        public AboutViewModel()
        {
            ShowMoreInfoCmd = new AsyncRelayCommand(ShowMoreInfo);
        }

        private async Task ShowMoreInfo() => await Launcher.Default.OpenAsync(MoreInfoUrl);
    }
}
