using CommunityToolkit.Mvvm.ComponentModel;
using Honoo.Configuration;
using IWshRuntimeLibrary;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Wireshelves.ViewModels
{
    public partial class AppItem : ObservableObject
    {
        #region Members

        [ObservableProperty]
        private string _arguments;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private string _folder;

        [ObservableProperty]
        private string _fullName;

        [ObservableProperty]
        private Image _icon;

        [ObservableProperty]
        private AppItemKind _kind;

        [ObservableProperty]
        private bool _privilege;

        [ObservableProperty]
        private string _title;

        #endregion Members

        public AppItem(FileInfo newFile)
        {
            string ext = Path.GetExtension(newFile.FullName);
            if (ext.Equals(".exe", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Kind = AppItemKind.Application;
                this.Title = newFile.Name.Replace(newFile.Extension, string.Empty);
                this.FullName = newFile.FullName;
                this.Arguments = string.Empty;
                this.Privilege = false;
                this.Folder = newFile.DirectoryName ?? string.Empty;
                this.Description = string.Empty;
            }
            else if (ext.Equals(".lnk", StringComparison.InvariantCultureIgnoreCase))
            {
                IWshShell3 shell = new WshShell();
                var shortcut = (IWshShortcut)shell.CreateShortcut(newFile.FullName);
                this.Kind = Path.GetExtension(shortcut.TargetPath).Equals(".exe", StringComparison.InvariantCultureIgnoreCase) ? AppItemKind.Application : AppItemKind.File;
                this.Title = newFile.Name.Replace(newFile.Extension, string.Empty);
                this.FullName = shortcut.TargetPath;
                this.Arguments = shortcut.Arguments;
                this.Privilege = false;
                this.Folder = newFile.DirectoryName ?? string.Empty;
                this.Description = shortcut.Description;
            }
            else
            {
                this.Kind = AppItemKind.File;
                this.Title = newFile.Name;
                this.FullName = newFile.FullName;
                this.Arguments = string.Empty;
                this.Privilege = false;
                this.Folder = newFile.DirectoryName ?? string.Empty;
                this.Description = string.Empty;
            }
            this.Icon = GetIcon(this.FullName);
            this.PropertyChanged += OnPropertyChanged;
        }

        public AppItem(DirectoryInfo newFolder)
        {
            this.Kind = AppItemKind.Folder;
            this.Title = newFolder.Name;
            this.FullName = newFolder.FullName;
            this.Arguments = string.Empty;
            this.Privilege = false;
            this.Folder = newFolder.FullName;
            this.Description = string.Empty;
            this.Icon = GetFolderIcon();
            this.PropertyChanged += OnPropertyChanged;
        }

        public AppItem(XDictionary entry)
        {
            this.Kind = entry.Attributes.GetValue("Kind", new XConfigAttribute(AppItemKind.File.ToString())).GetEnumValue<AppItemKind>();
            this.Title = entry.Properties.GetStringValue("Title", "Title");
            this.FullName = entry.Properties.GetStringValue("FullName", string.Empty);
            this.Arguments = entry.Properties.GetStringValue("Arguments", string.Empty);
            this.Privilege = entry.Properties.GetValue("Privilege", new XString(false.ToString())).GetBooleanValue();
            this.Folder = this.Kind == AppItemKind.Folder ? this.FullName : Path.GetDirectoryName(this.FullName) ?? string.Empty;
            this.Description = entry.Properties.GetStringValue("Description", string.Empty);
            XString iconCache = entry.Properties.GetValue("IconCache", new XString(string.Empty));
            if (iconCache.Value.Length > 0)
            {
                byte[] data = iconCache.GetBytesValue(XStringFormat.Hex, "\r", "\n", " ");
                this.Icon = GetIcon(data);
            }
            else if (this.Kind == AppItemKind.Folder)
            {
                this.Icon = GetFolderIcon();
            }
            else
            {
                this.Icon = GetBreakIcon();
            }
            this.PropertyChanged += OnPropertyChanged;
        }

        private static Image GetBreakIcon()
        {
            return new Image()
            {
                Height = 32,
                Width = 32,
                Source = new BitmapImage(new Uri("/Resources/imageres-2.ico", UriKind.Relative))
            };
        }

        private static Image GetFolderIcon()
        {
            return new Image()
            {
                Height = 32,
                Width = 32,
                Source = new BitmapImage(new Uri("/Resources/imageres-3.ico", UriKind.Relative))
            };
        }

        private static Image GetIcon(byte[] blob)
        {
            try
            {
                var bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(blob);
                bmp.EndInit();
                bmp.Freeze();
                return new Image() { Height = 32, Width = 32, Source = bmp };
            }
            catch
            {
                return GetBreakIcon();
            }
        }

        private static Image GetIcon(string path)
        {
            BitmapSource? icon = Honoo.Drawing.ImagingExtensions.CreateFromHIcon(path);
            return icon == null ? GetBreakIcon() : new Image() { Height = 32, Width = 32, Source = icon };
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            General.Instance.Modified = true;
        }
    }
}