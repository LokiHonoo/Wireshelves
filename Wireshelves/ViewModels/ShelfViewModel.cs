using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HonooUI.WPF;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Wireshelves.ViewModels
{
    public partial class ShelfViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<AppItemViewModel> appitems;

        [ObservableProperty]
        private bool isLastPage;

        public ShelfViewModel(ObservableCollection<AppItemViewModel> appitems)
        {
            this.Appitems = appitems;
            this.DropCommand = new RelayCommand<DragEventArgs?>(DropCommandExecute);
        }

        public ICommand DropCommand { get; }
        public GlobalViewModel GlobalViewModel { get; } = Locator.GlobalViewModel;
        public Models.Localization Localization { get; } = Locator.Localization;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:不捕获常规异常类型", Justification = "<挂起>")]
        private void DropCommandExecute(DragEventArgs? e)
        {
            if (e != null)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    if (this.Appitems.Count < this.GlobalViewModel.ShelfCol * this.GlobalViewModel.ShelfRow)
                    {
                        string[] entries = (string[])e.Data.GetData(DataFormats.FileDrop);
                        if (entries.Length > 0)
                        {
                            foreach (var entry in entries)
                            {
                                try
                                {
                                    if (File.Exists(entry))
                                    {
                                        string ext = Path.GetExtension(entry).ToUpperInvariant();
                                        if (ext == ".EXE")
                                        {
                                            var vm = new AppItemViewModel(Guid.NewGuid().ToString(), Path.GetFileNameWithoutExtension(entry), entry)
                                            {
                                                Kind = AppItemKind.File,
                                                Icon = Honoo.Drawing.Icon.GetIcon(entry)
                                            };
                                            this.Appitems.Add(vm);
                                            if (this == this.GlobalViewModel.Shelves[^1])
                                            {
                                                this.GlobalViewModel.Shelves.Add(new ShelfViewModel([]));
                                            }
                                        }
                                        else if (ext == ".LNK")
                                        {
                                            var shell = new IWshRuntimeLibrary.WshShell();
                                            var shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(entry);
                                            var vm = new AppItemViewModel(Guid.NewGuid().ToString(), Path.GetFileNameWithoutExtension(entry), entry)
                                            {
                                                Kind = AppItemKind.Shortcut,
                                                Icon = Honoo.Drawing.Icon.GetIcon(shortcut.TargetPath)
                                            };
                                            this.Appitems.Add(vm);
                                            if (this == this.GlobalViewModel.Shelves[^1])
                                            {
                                                this.GlobalViewModel.Shelves.Add(new ShelfViewModel([]));
                                            }
                                        }
                                    }
                                    else if (Directory.Exists(entry))
                                    {
                                        var vm = new AppItemViewModel(Guid.NewGuid().ToString(), Path.GetFileName(entry), entry)
                                        {
                                            Kind = AppItemKind.Folder,
                                            Icon = (Geometry)Application.Current.FindResource("FolderFillGeometry"),
                                            IconColorBrush = new SolidColorBrush(Color.FromRgb(255, 207, 75))
                                        };
                                        this.Appitems.Add(vm);
                                        if (this == this.GlobalViewModel.Shelves[^1])
                                        {
                                            this.GlobalViewModel.Shelves.Add(new ShelfViewModel([]));
                                        }
                                    }
                                    else
                                    {
                                        ToastManager.Default.Show(this.Localization.TypeUnsupported, 5000, ToastOptions.Exclamation);
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                    else
                    {
                        ToastManager.Default.Show(this.Localization.LackSpace, 5000, ToastOptions.Exclamation);
                    }
                }
                else if (e.Data.GetDataPresent(DataFormats.Xaml))
                {
                }
                e.Handled = true;
            }
        }
    }
}