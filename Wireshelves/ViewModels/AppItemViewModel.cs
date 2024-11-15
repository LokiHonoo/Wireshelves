﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HonooUI.WPF;
using Microsoft.Xaml.Behaviors;
using Microsoft.Xaml.Behaviors.Layout;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Wireshelves.ViewModels
{
    public partial class AppItemViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool deleteConfirm;

        [ObservableProperty]
        private object? icon = (Geometry)Application.Current.FindResource("WindowsGeometry");

        [ObservableProperty]
        private Brush? iconColorBrush = (Brush)Application.Current.FindResource("SecondaryForegroundBrush");

        [ObservableProperty]
        private AppItemKind kind;

        [ObservableProperty]
        private bool runAsAdministrator;

        [ObservableProperty]
        private string? title;

        public AppItemViewModel(string id, string title, string path)
        {
            this.ID = id;
            this.Title = title;
            this.Path = path;
            this.MouseRightButtonUpCommand = new RelayCommand(MouseRightButtonUpCommandExecute);
            this.ProcessCommand = new RelayCommand(ProcessCommandExecute);
            this.ProcessAsCommand = new RelayCommand(ProcessAsCommandExecute);
            this.ItemBoxLoadedCommand = new RelayCommand<Border?>(ItemBoxLoadedCommandExecute);
            this.OpenLocationCommand = new RelayCommand(OpenLocationCommandExecute);
            this.DeleteCommand = new RelayCommand(DeleteCommandExecute);
            this.CloseMenuCommand = new RelayCommand(CloseMenuCommandExecute);
        }

        public ICommand CloseMenuCommand { get; }
        public ICommand DeleteCommand { get; }
        public GlobalViewModel GlobalViewModel { get; } = Locator.GlobalViewModel;
        public string? ID { get; }
        public ICommand ItemBoxLoadedCommand { get; }
        public Models.Localization Localization { get; } = Locator.Localization;

        public ICommand MouseRightButtonUpCommand { get; }

        public ICommand OpenLocationCommand { get; }

        public string? Path { get; }

        public ICommand ProcessAsCommand { get; }

        public ICommand ProcessCommand { get; }

        private void CloseMenuCommandExecute()
        {
            this.GlobalViewModel.AppItemEditing = null;
        }

        private void DeleteCommandExecute()
        {
            bool del = false;
            foreach (var shelf in this.GlobalViewModel.Shelves)
            {
                for (int i = shelf.Appitems.Count - 1; i >= 0; i--)
                {
                    if (shelf.Appitems[i] == this)
                    {
                        shelf.Appitems.RemoveAt(i);
                        del = true;
                        break;
                    }
                }
                if (del)
                {
                    break;
                }
            }
            this.GlobalViewModel.AppItemEditing = null;
        }

        private void DragBegun(object sender, MouseEventArgs e)
        {
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:不捕获常规异常类型", Justification = "<挂起>")]
        private void DragFinished(object sender, MouseEventArgs e)
        {
            var element = (FrameworkElement)e.Source;
            if (element.RenderTransform == null || (element.RenderTransform.Value.OffsetX < 1 && element.RenderTransform.Value.OffsetY < 1))
            {
                if (!string.IsNullOrWhiteSpace(this.Path))
                {
                    if (this.Kind == AppItemKind.File || this.Kind == AppItemKind.Shortcut)
                    {
                        if (File.Exists(this.Path))
                        {
                            if (this.RunAsAdministrator)
                            {
                                try
                                {
                                    Process.Start(new ProcessStartInfo(this.Path) { UseShellExecute = true, Verb = "runas" });
                                }
                                catch
                                {
                                }
                            }
                            else
                            {
                                try
                                {
                                    Process.Start(new ProcessStartInfo(this.Path) { UseShellExecute = true });
                                }
                                catch
                                {
                                }
                            }
                        }
                        else
                        {
                            ToastManager.Default.Show(this.Localization.FileNotExists, 5000, ToastOptions.Exclamation);
                        }
                    }
                    else
                    {
                        if (Directory.Exists(this.Path))
                        {
                            try
                            {
                                Process.Start(new ProcessStartInfo(this.Path) { UseShellExecute = true });
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            ToastManager.Default.Show(this.Localization.FolderNotExists, 5000, ToastOptions.Exclamation);
                        }
                    }
                }
                else
                {
                    ToastManager.Default.Show(this.Localization.FileNotExists, 5000, ToastOptions.Exclamation);
                }
            }
            else if (element.RenderTransform != null)
            {
            }
            element.RenderTransform = null;
        }

        private void ItemBoxLoadedCommandExecute(Border? box)
        {
            if (box != null)
            {
                var behavior = new MouseDragElementBehavior();
                Interaction.GetBehaviors(box).Add(behavior);
                behavior.DragBegun += DragBegun;
                behavior.DragFinished += DragFinished;
            }
        }

        private void MouseRightButtonUpCommandExecute()
        {
            this.DeleteConfirm = false;
            this.GlobalViewModel.AppItemEditing = this;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:不捕获常规异常类型", Justification = "<挂起>")]
        private void OpenLocationCommandExecute()
        {
            if (!string.IsNullOrWhiteSpace(this.Path))
            {
                if (this.Kind == AppItemKind.File || this.Kind == AppItemKind.Shortcut)
                {
                    string? dir = System.IO.Path.GetDirectoryName(this.Path);
                    if (Directory.Exists(dir))
                    {
                        try
                        {
                            Process.Start(new ProcessStartInfo(dir) { UseShellExecute = true });
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        ToastManager.Default.Show(this.Localization.FolderNotExists, 5000, ToastOptions.Exclamation);
                    }
                }
            }
            else
            {
                ToastManager.Default.Show(this.Localization.FolderNotExists, 5000, ToastOptions.Exclamation);
            }
            this.GlobalViewModel.AppItemEditing = null;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:不捕获常规异常类型", Justification = "<挂起>")]
        private void ProcessAsCommandExecute()
        {
            if (!string.IsNullOrWhiteSpace(this.Path))
            {
                if (this.Kind == AppItemKind.File || this.Kind == AppItemKind.Shortcut)
                {
                    if (File.Exists(this.Path))
                    {
                        if (this.RunAsAdministrator)
                        {
                            try
                            {
                                Process.Start(new ProcessStartInfo(this.Path) { UseShellExecute = true, Verb = "runas" });
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            try
                            {
                                Process.Start(new ProcessStartInfo(this.Path) { UseShellExecute = true });
                            }
                            catch
                            {
                            }
                        }
                    }
                    else
                    {
                        ToastManager.Default.Show(this.Localization.FileNotExists, 5000, ToastOptions.Exclamation);
                    }
                }
                else
                {
                    if (Directory.Exists(this.Path))
                    {
                        try
                        {
                            Process.Start(new ProcessStartInfo(this.Path) { UseShellExecute = true });
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        ToastManager.Default.Show(this.Localization.FolderNotExists, 5000, ToastOptions.Exclamation);
                    }
                }
            }
            else
            {
                ToastManager.Default.Show(this.Localization.FileNotExists, 5000, ToastOptions.Exclamation);
            }
            this.GlobalViewModel.AppItemEditing = null;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:不捕获常规异常类型", Justification = "<挂起>")]
        private void ProcessCommandExecute()
        {
            if (!string.IsNullOrWhiteSpace(this.Path))
            {
                if (this.Kind == AppItemKind.File || this.Kind == AppItemKind.Shortcut)
                {
                    if (File.Exists(this.Path))
                    {
                        try
                        {
                            Process.Start(new ProcessStartInfo(this.Path) { UseShellExecute = true });
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        ToastManager.Default.Show(this.Localization.FileNotExists, 5000, ToastOptions.Exclamation);
                    }
                }
                else
                {
                    if (Directory.Exists(this.Path))
                    {
                        try
                        {
                            Process.Start(new ProcessStartInfo(this.Path) { UseShellExecute = true });
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        ToastManager.Default.Show(this.Localization.FolderNotExists, 5000, ToastOptions.Exclamation);
                    }
                }
            }
            else
            {
                ToastManager.Default.Show(this.Localization.FileNotExists, 5000, ToastOptions.Exclamation);
            }
            this.GlobalViewModel.AppItemEditing = null;
        }
    }
}