using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HonooUI.WPF;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows.Input;
using Wireshelves.Models;

namespace Wireshelves.ViewModels
{
    public class SettingsUserControlViewModel : ObservableObject
    {
        public SettingsUserControlViewModel()
        {
            this.AppLocations = new Collection<string>(
                [
                this.Localization.AppLocationCenter,
                this.Localization.AppLocationLeft,
                this.Localization.AppLocationRight,
                this.Localization.AppLocationMoveable
                ]);

            this.DeactivatedWorks = new Collection<string>(
                [
                this.Localization.NothingToDo,
                this.Localization.CloseImmediately,
                string.Format(CultureInfo.InvariantCulture, this.Localization.CloseDelay, "5m"),
                string.Format(CultureInfo.InvariantCulture, this.Localization.CloseDelay, "30m"),
                string.Format(CultureInfo.InvariantCulture, this.Localization.CloseDelay, "1h")
                ]);
            this.ExportLngCommand = new RelayCommand(ExportLngCommandExecute);
        }

        public IReadOnlyCollection<string> AppLocations { get; }

        public GlobalViewModel GlobalViewModel { get; } = Locator.GlobalViewModel;

        public IReadOnlyCollection<string> DeactivatedWorks { get; }

        public ICommand ExportLngCommand { get; }

        public Localization Localization { get; } = Locator.Localization;

        public IReadOnlyCollection<int> ShelfCols { get; } = [4, 5, 6, 7, 8, 9, 10, 11, 12];

        public IReadOnlyCollection<int> ShelfRows { get; } = [3, 4, 5, 6, 7];

        public IReadOnlyCollection<ThemeStyle> ThemeStyles { get; } = [ThemeStyle.Light, ThemeStyle.Dark];

        private void ExportLngCommandExecute()
        {
            var dialog = new SaveFileDialog
            {
                Filter = "lng|XML Language File",
                AddExtension = true,
                DefaultExt = ".lng",
                FileName = this.Localization.ShortName + ".lng",
                OverwritePrompt = true
            };
            bool pin = this.GlobalViewModel.Pin;
            this.GlobalViewModel.Pin = true;
            if (dialog.ShowDialog() is bool ok)
            {
                if (ok)
                {
                    File.Delete(dialog.FileName);
                    this.Localization.Save(dialog.FileName);
                }
            }
            this.GlobalViewModel.Pin = pin;
        }
    }
}