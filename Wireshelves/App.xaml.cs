using System.Windows;
using Wireshelves.Models;

namespace Wireshelves
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            Config.SaveSettings();
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Config.LoadSettings();
            base.OnStartup(e);
        }
    }
}