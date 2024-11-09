using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Honoo.Configuration;
using HonooUI.WPF;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Wireshelves.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        #region Instance

        public static MainWindowViewModel Instance { get; } = new MainWindowViewModel();

        #endregion Instance

        #region Members

        private readonly string _appListFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app-list.xml");

        [ObservableProperty]
        private AppItem? _currentAppItem;

        [ObservableProperty]
        private AppItemGroup? _currentAppItemGroup;

        [ObservableProperty]
        private AppItemPage? _currentAppItemPage;

        [ObservableProperty]
        private object? _editing;

        [ObservableProperty]
        private Visibility _moveOutGroupVisibility;

        [ObservableProperty]
        private Visibility _moveToPageVisibility;

        [ObservableProperty]
        private Visibility _settingsMenuVisibility = Visibility.Collapsed;

        private FrameworkElement? _targetElement = null;
        private int _wheelTimestamp;
        public ICommand AddPageCommand { get; }
        public ICommand AppItemClickCommand { get; }
        public ICommand AppItemDropCommand { get; }
        public ICommand AppItemGroupClickCommand { get; }
        public ICommand AppItemGroupDragEnterCommand { get; }
        public ICommand AppItemGroupDragLeaveCommand { get; }
        public ICommand AppItemGroupDropCommand { get; }
        public ICommand AppItemGroupMouseRightUpCommand { get; }
        public ICommand AppItemGroupPadDropCommand { get; }
        public ICommand AppItemGroupSidePadDropCommand { get; }
        public ICommand AppItemMouseRightUpCommand { get; }
        public ICommand AppitemPagePadDropCommand { get; }
        public ObservableCollection<AppItemPage> AppItemPages { get; } = [];
        public ICommand CloseGroupMenuCommand { get; }
        public ICommand CloseItemMenuCommand { get; }
        public ICommand ExitCommand { get; } = new RelayCommand(() => { SystemCommands.CloseWindow(Application.Current.MainWindow); });
        public ICommand ExportLanguageCommand { get; }
        public ICommand MouseWheelCommand { get; }
        public ICommand MoveOutGroupCommand { get; }
        public ICommand MoveToNextPageCommand { get; }
        public ICommand MoveToPreviousPageCommand { get; }
        public ICommand OpenUriCommand { get; }
        public ICommand RemoveGroupCommand { get; }
        public ICommand SetLocationCommand { get; }
        public ICommand ToggleSettingsMenuCommand { get; }
        public ICommand WindowClosingCommand { get; }
        public ICommand WindowDeactivatedCommand { get; }
        public ICommand WindowLoadedCommand { get; }
        private int _shelfCol;
        private int _shelfRow;

        #endregion Members

        #region Construction

        public MainWindowViewModel()
        {
            this.WindowLoadedCommand = new RelayCommand(WindowLoadedCommandExecute);
            this.WindowClosingCommand = new RelayCommand<CancelEventArgs>(WindowClosingCommandExecute);
            this.WindowDeactivatedCommand = new RelayCommand<HonooUI.WPF.Controls.Window?>(WindowDeactivatedCommandExecute);
            this.MouseWheelCommand = new RelayCommand<MouseWheelEventArgs?>(MouseWheelCommandExecute);
            this.AddPageCommand = new RelayCommand<string>(AddPageCommandExecute);
            this.AppitemPagePadDropCommand = new RelayCommand<DragEventArgs>(AppitemPagePadDropCommandExecute);
            this.CloseItemMenuCommand = new RelayCommand<string>(CloseItemMenuCommandExecute);
            this.CloseGroupMenuCommand = new RelayCommand(CloseGroupMenuCommandExecute);
            this.SetLocationCommand = new RelayCommand<HonooUI.WPF.Controls.Window>(SetLocationCommandExecute);
            this.ExportLanguageCommand = new RelayCommand<string>(ExportLanguageCommandExecute);
            this.AppItemGroupPadDropCommand = new RelayCommand<DragEventArgs>(AppItemGroupPadDropCommandExecute);
            this.AppItemGroupClickCommand = new RelayCommand<AppItemGroup>(AppItemGroupClickCommandExecute);
            this.AppItemGroupMouseRightUpCommand = new RelayCommand<MouseButtonEventArgs>(AppItemGroupMouseRightUpCommandExecute);
            this.AppItemGroupDropCommand = new RelayCommand<DragEventArgs>(AppItemGroupDropCommandExecute);
            this.AppItemGroupSidePadDropCommand = new RelayCommand<DragEventArgs>(AppItemGroupSidePadDropCommandExecute);
            this.AppItemGroupDragEnterCommand = new RelayCommand<DragEventArgs>(AppItemGroupDragEnterCommandExecute);
            this.AppItemGroupDragLeaveCommand = new RelayCommand<DragEventArgs>(AppItemGroupDragLeaveCommandExecute);
            this.AppItemClickCommand = new RelayCommand<AppItem>(AppItemClickCommandExecute);
            this.AppItemMouseRightUpCommand = new RelayCommand<AppItem>(AppItemMouseRightUpCommandExecute);
            this.AppItemDropCommand = new RelayCommand<DragEventArgs>(AppItemDropCommandExecute);
            this.MoveOutGroupCommand = new RelayCommand(MoveOutGroupCommandExecute);
            this.MoveToPreviousPageCommand = new RelayCommand(MoveToPreviousPageCommandExecute);
            this.MoveToNextPageCommand = new RelayCommand(MoveToNextPageCommandExecute);
            this.RemoveGroupCommand = new RelayCommand(RemoveGroupCommandExecute);
            this.ToggleSettingsMenuCommand = new RelayCommand<DragEventArgs>(ToggleSettingsMenuCommandExecute);
            this.OpenUriCommand = new RelayCommand<Uri>(OpenUriCommandExecute);

            this.AppItemPages.CollectionChanged += OnAppItemPagesChanged;
            Settings.Instance.PropertyChanging += OnSettingsPropertyChanging; ;
            Settings.Instance.PropertyChanged += OnSettingsPropertyChanged;
        }

        private void OnSettingsPropertyChanging(object? sender, PropertyChangingEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Settings.Instance.ShelfCol): _shelfCol = Settings.Instance.ShelfCol; break;
                case nameof(Settings.Instance.ShelfRow): _shelfRow = Settings.Instance.ShelfRow; break;
                default: break;
            }
        }

        private void OnSettingsPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Settings.Instance.ShelfCol):
                    foreach (var page in this.AppItemPages)
                    {
                        if (page.AppItemGroups.Count > Settings.Instance.ShelfCol * Settings.Instance.ShelfRow)
                        {
                            Settings.Instance.ShelfCol = _shelfCol;
                            ToastManager.Default.Show(LanguagePackage.Instance.Messages.TooManyItems, 5000, ToastOptions.Exclamation, null);
                            break;
                        }
                    }
                    break;

                case nameof(Settings.Instance.ShelfRow):
                    foreach (var page in this.AppItemPages)
                    {
                        if (page.AppItemGroups.Count > Settings.Instance.ShelfCol * Settings.Instance.ShelfRow)
                        {
                            Settings.Instance.ShelfRow = _shelfRow;
                            ToastManager.Default.Show(LanguagePackage.Instance.Messages.TooManyItems, 5000, ToastOptions.Exclamation, null);
                            break;
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        private void OnAppItemPagesChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            General.Instance.Modified = true;
        }

        #endregion Construction

        private void AddPageCommandExecute(string? obj)
        {
            if (obj != null && this.CurrentAppItemPage != null)
            {
                switch (obj)
                {
                    case "Previous":
                        for (int i = 0; i < this.AppItemPages.Count; i++)
                        {
                            if (this.AppItemPages[i] == this.CurrentAppItemPage)
                            {
                                var page = new AppItemPage(Guid.NewGuid().ToString());
                                this.AppItemPages.Insert(i, page);
                                this.CurrentAppItemPage = page;
                                break;
                            }
                        }
                        break;

                    case "Next":
                    default:
                        for (int i = 0; i < this.AppItemPages.Count; i++)
                        {
                            if (this.AppItemPages[i] == this.CurrentAppItemPage)
                            {
                                var page = new AppItemPage(Guid.NewGuid().ToString());
                                if (i == this.AppItemPages.Count - 1)
                                {
                                    this.AppItemPages.Add(page);
                                }
                                else
                                {
                                    this.AppItemPages.Insert(i + 1, page);
                                }
                                this.CurrentAppItemPage = page;
                                break;
                            }
                        }
                        break;
                }
            }
        }

        private void AppItemClickCommandExecute(AppItem? item)
        {
            if (item != null)
            {
                if (File.Exists(item.FullName) || Directory.Exists(item.FullName))
                {
                    var info = new ProcessStartInfo(item.FullName) { UseShellExecute = true };
                    if (item.Kind == AppItemKind.Application)
                    {
                        info.Arguments = item.Arguments;
                        if (item.Privilege)
                        {
                            info.Verb = "runas";
                        }
                    }
                    Process.Start(info);
                }
                else
                {
                    ToastManager.Default.Show(LanguagePackage.Instance.Messages.AppItemNotExists, 5000, ToastOptions.Exclamation, null);
                }
            }
        }

        private void AppItemDropCommandExecute(DragEventArgs? e)
        {
            if (e != null && this.CurrentAppItemPage != null && this.CurrentAppItemGroup != null)
            {
                var target = (Control)e.Source;
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] entries = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (entries.Length > 0)
                    {
                        DropFileInGroup(entries);
                    }
                }
                else if (e.Data.GetDataPresent(typeof(AppItem)))
                {
                    var droppedItem = (AppItem)e.Data.GetData(typeof(AppItem));

                    AppItem targetItem = (AppItem)target.DataContext;
                    int sIndex = this.CurrentAppItemGroup.AppItems.IndexOf(droppedItem);
                    int tIndex = this.CurrentAppItemGroup.AppItems.IndexOf(targetItem);
                    if (sIndex != tIndex)
                    {
                        this.CurrentAppItemGroup.AppItems.RemoveAt(sIndex);
                        this.CurrentAppItemGroup.AppItems.Insert(tIndex, droppedItem);
                    }
                    General.Instance.Modified = true;
                }
                else if (e.Data.GetDataPresent(typeof(AppItemGroup)))
                {
                    var droppedGroup = (AppItemGroup)e.Data.GetData(typeof(AppItemGroup));
                    if (droppedGroup.AppItems.Count == 1)
                    {
                        if (this.CurrentAppItemGroup.AppItems.Count < Settings.Instance.GroupCol * Settings.Instance.GroupRow)
                        {
                            this.CurrentAppItemPage.AppItemGroups.Remove(droppedGroup);
                            this.CurrentAppItemGroup.AppItems.Add(droppedGroup.AppItems[0]);
                            General.Instance.Modified = true;
                        }
                        else
                        {
                            ToastManager.Default.Show(LanguagePackage.Instance.Messages.TooManyItems, 5000, ToastOptions.Exclamation, null);
                        }
                        General.Instance.Modified = true;
                    }
                }
                target.Background = new SolidColorBrush(Colors.Transparent);
                _targetElement = null;
                e.Handled = true;
            }
        }

        private void AppItemGroupClickCommandExecute(AppItemGroup? group)
        {
            if (group != null)
            {
                if (group.AppItems.Count > 1)
                {
                    this.CurrentAppItemGroup = group;
                }
                else
                {
                    AppItem item = group.AppItems[0];
                    if (File.Exists(item.FullName) || Directory.Exists(item.FullName))
                    {
                        var info = new ProcessStartInfo(item.FullName) { UseShellExecute = true };
                        if (item.Kind == AppItemKind.Application)
                        {
                            info.Arguments = item.Arguments;
                            if (item.Privilege)
                            {
                                info.Verb = "runas";
                            }
                        }
                        Process.Start(info);
                    }
                    else
                    {
                        ToastManager.Default.Show(LanguagePackage.Instance.Messages.AppItemNotExists, 5000, ToastOptions.Exclamation, null);
                    }
                }
            }
        }

        private void AppItemGroupDragEnterCommandExecute(DragEventArgs? e)
        {
            if (e != null)
            {
                if (e.Data.GetDataPresent(typeof(AppItemGroup)))
                {
                    var droppedGroup = (AppItemGroup)e.Data.GetData(typeof(AppItemGroup));
                    var target = (FrameworkElement)e.Source;
                    if (droppedGroup.AppItems.Count == 1)
                    {
                        _targetElement = (FrameworkElement)e.Source;
                        Task.Run(() =>
                        {
                            Thread.Sleep(2000);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                if ((FrameworkElement)e.Source == _targetElement)
                                {
                                    this.CurrentAppItemGroup = (AppItemGroup)target.DataContext;
                                }
                            });
                        });
                    }
                }
                e.Handled = true;
            }
        }

        private void AppItemGroupDragLeaveCommandExecute(DragEventArgs? e)
        {
            if (e != null)
            {
                _targetElement = null;
            }
        }

        private void AppItemGroupDropCommandExecute(DragEventArgs? e)
        {
            if (e != null && this.CurrentAppItemPage != null)
            {
                var target = (Control)e.Source;
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] entries = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (entries.Length > 0)
                    {
                        DropFileInPage(entries);
                    }
                }
                else if (e.Data.GetDataPresent(typeof(AppItemGroup)))
                {
                    var droppedGroup = (AppItemGroup)e.Data.GetData(typeof(AppItemGroup));
                    AppItemGroup targetGroup = (AppItemGroup)target.DataContext;
                    int sIndex = this.CurrentAppItemPage.AppItemGroups.IndexOf(droppedGroup);
                    int tIndex = this.CurrentAppItemPage.AppItemGroups.IndexOf(targetGroup);
                    if (sIndex != tIndex)
                    {
                        this.CurrentAppItemPage.AppItemGroups.RemoveAt(sIndex);
                        this.CurrentAppItemPage.AppItemGroups.Insert(tIndex, droppedGroup);
                    }
                    General.Instance.Modified = true;
                }
                else if (e.Data.GetDataPresent(typeof(AppItem)))
                {
                    var droppedItem = (AppItem)e.Data.GetData(typeof(AppItem));
                    if (this.CurrentAppItemPage.AppItemGroups.Count < Settings.Instance.ShelfCol * Settings.Instance.ShelfRow)
                    {
                        foreach (var page in this.AppItemPages)
                        {
                            page.RemoveAppItem(droppedItem);
                        }
                        var group = new AppItemGroup("Group");
                        group.AppItems.Add(droppedItem);
                        this.CurrentAppItemPage.AppItemGroups.Add(group);
                        General.Instance.Modified = true;
                    }
                    else
                    {
                        ToastManager.Default.Show(LanguagePackage.Instance.Messages.TooManyItems, 5000, ToastOptions.Exclamation, null);
                    }
                }
                target.Background = new SolidColorBrush(Colors.Transparent);
                _targetElement = null;
                e.Handled = true;
            }
        }

        private void AppItemGroupMouseRightUpCommandExecute(MouseButtonEventArgs? e)
        {
            if (e != null)
            {
                if (e.Source is FrameworkElement ctl)
                {
                    if (ctl.DataContext is AppItemGroup group)
                    {
                        if (group.AppItems.Count == 1)
                        {
                            this.CurrentAppItem = group.AppItems[0];
                            this.MoveOutGroupVisibility = Visibility.Collapsed;
                            this.MoveToPageVisibility = Visibility.Visible;
                            this.Editing = group;
                        }
                        else
                        {
                            Point mousePos = e.GetPosition(ctl);
                            HitTestResult result = VisualTreeHelper.HitTest(ctl, mousePos);
                            if (result != null)
                            {
                                if (result.VisualHit is FrameworkElement element)
                                {
                                    this.Editing = group;
                                    var menu = (ContextMenu)element.FindResource("AppGroupContextMenu");
                                    menu.PlacementTarget = element;
                                    menu.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
                                    menu.IsOpen = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AppItemGroupPadDropCommandExecute(DragEventArgs? e)
        {
            if (e != null && this.CurrentAppItemPage != null && this.CurrentAppItemGroup != null)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] entries = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (entries.Length > 0)
                    {
                        DropFileInGroup(entries);
                    }
                }
                else if (e.Data.GetDataPresent(typeof(AppItemGroup)))
                {
                    var droppedGroup = (AppItemGroup)e.Data.GetData(typeof(AppItemGroup));
                    if (droppedGroup.AppItems.Count == 1)
                    {
                        if (this.CurrentAppItemGroup.AppItems.Count < Settings.Instance.GroupCol * Settings.Instance.GroupRow)
                        {
                            this.CurrentAppItemPage.AppItemGroups.Remove(droppedGroup);
                            this.CurrentAppItemGroup.AppItems.Add(droppedGroup.AppItems[0]);
                            General.Instance.Modified = true;
                        }
                        else
                        {
                            ToastManager.Default.Show(LanguagePackage.Instance.Messages.TooManyItems, 5000, ToastOptions.Exclamation, null);
                        }
                    }
                }
                e.Handled = true;
            }
        }

        private void AppItemGroupSidePadDropCommandExecute(DragEventArgs? e)
        {
            if (e != null && this.CurrentAppItemPage != null)
            {
                if (e.Data.GetDataPresent(typeof(AppItem)))
                {
                    var droppedItem = (AppItem)e.Data.GetData(typeof(AppItem));
                    if (this.CurrentAppItemPage.AppItemGroups.Count < Settings.Instance.ShelfCol * Settings.Instance.ShelfRow)
                    {
                        foreach (var page in this.AppItemPages)
                        {
                            page.RemoveAppItem(droppedItem);
                        }
                        var group = new AppItemGroup("Group");
                        group.AppItems.Add(droppedItem);
                        this.CurrentAppItemPage.AppItemGroups.Add(group);
                        General.Instance.Modified = true;
                        this.CurrentAppItemGroup = null;
                    }
                    else
                    {
                        ToastManager.Default.Show(LanguagePackage.Instance.Messages.TooManyItems, 5000, ToastOptions.Exclamation, null);
                    }
                }
                e.Handled = true;
            }
        }

        private void AppItemMouseRightUpCommandExecute(AppItem? item)
        {
            if (item != null)
            {
                int count = this.CurrentAppItemGroup!.AppItems.Count;
                this.CurrentAppItemGroup = null;
                this.CurrentAppItem = item;
                this.MoveOutGroupVisibility = count > 1 ? Visibility.Visible : Visibility.Collapsed;
                this.MoveToPageVisibility = Visibility.Collapsed;
                this.Editing = item;
            }
        }

        private void AppitemPagePadDropCommandExecute(DragEventArgs? e)
        {
            if (e != null && this.CurrentAppItemPage != null)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] entries = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (entries.Length > 0)
                    {
                        DropFileInPage(entries);
                    }
                }
                e.Handled = true;
            }
        }

        private void CloseGroupMenuCommandExecute()
        {
            this.CurrentAppItemGroup = null;
        }

        private void CloseItemMenuCommandExecute(string? obj)
        {
            if (obj != null && this.CurrentAppItem != null)
            {
                switch (obj)
                {
                    case "Remove":
                        foreach (var page in this.AppItemPages)
                        {
                            if (page.RemoveAppItem(this.CurrentAppItem))
                            {
                                break;
                            }
                        }
                        break;

                    case "OpenFileLocation":
                        if (Directory.Exists(this.CurrentAppItem.Folder))
                        {
                            Process.Start(new ProcessStartInfo(this.CurrentAppItem.Folder) { UseShellExecute = true });
                        }
                        else
                        {
                            ToastManager.Default.Show(LanguagePackage.Instance.Messages.FolderNotExists, 5000, ToastOptions.Exclamation, null);
                        }
                        break;

                    case "RunAsAdministrator":
                        if (File.Exists(this.CurrentAppItem.FullName) || Directory.Exists(this.CurrentAppItem.FullName))
                        {
                            var info = new ProcessStartInfo(this.CurrentAppItem.FullName) { UseShellExecute = true };
                            if (this.CurrentAppItem.Kind == AppItemKind.Application)
                            {
                                info.Arguments = this.CurrentAppItem.Arguments;
                                info.Verb = "runas";
                            }
                            Process.Start(info);
                        }
                        else
                        {
                            ToastManager.Default.Show(LanguagePackage.Instance.Messages.AppItemNotExists, 5000, ToastOptions.Exclamation, null);
                        }
                        break;

                    case "Close": default: break;
                }
                this.CurrentAppItem = null;
            }
        }

        private void DropFileInGroup(string[] entries)
        {
            if (entries.Length + this.CurrentAppItemGroup!.AppItems.Count <= Settings.Instance.GroupCol * Settings.Instance.GroupRow)
            {
                foreach (var entry in entries)
                {
                    if (File.Exists(entry))
                    {
                        try
                        {
                            var vm = new AppItem(new FileInfo(entry));
                            this.CurrentAppItemGroup.AppItems.Add(vm);
                        }
                        catch
                        {
                        }
                    }
                    else if (Directory.Exists(entry))
                    {
                        try
                        {
                            var vm = new AppItem(new DirectoryInfo(entry));
                            this.CurrentAppItemGroup.AppItems.Add(vm);
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        ToastManager.Default.Show(LanguagePackage.Instance.Messages.UnknownItem, 5000, ToastOptions.Exclamation, null);
                    }
                }
            }
            else
            {
                ToastManager.Default.Show(LanguagePackage.Instance.Messages.TooManyItems, 5000, ToastOptions.Exclamation, null);
            }
        }

        private void DropFileInPage(string[] entries)
        {
            if (entries.Length + this.CurrentAppItemPage!.AppItemGroups.Count <= Settings.Instance.ShelfCol * Settings.Instance.ShelfRow)
            {
                foreach (var entry in entries)
                {
                    if (File.Exists(entry))
                    {
                        try
                        {
                            var vm = new AppItem(new FileInfo(entry));
                            var vmg = new AppItemGroup("Group");
                            vmg.AppItems.Add(vm);
                            this.CurrentAppItemPage.AppItemGroups.Add(vmg);
                        }
                        catch
                        {
                        }
                    }
                    else if (Directory.Exists(entry))
                    {
                        try
                        {
                            var vm = new AppItem(new DirectoryInfo(entry));
                            var vmg = new AppItemGroup("Group");
                            vmg.AppItems.Add(vm);
                            this.CurrentAppItemPage.AppItemGroups.Add(vmg);
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        ToastManager.Default.Show(LanguagePackage.Instance.Messages.UnknownItem, 5000, ToastOptions.Exclamation, null);
                    }
                }
            }
            else
            {
                ToastManager.Default.Show(LanguagePackage.Instance.Messages.TooManyItems, 5000, ToastOptions.Exclamation, null);
            }
        }

        private void ExportLanguageCommandExecute(string? obj)
        {
            if (obj != null)
            {
                var dig = new SaveFileDialog
                {
                    Filter = "All Files (*.*)|*.*",
                    FileName = "Wireshelves.Language_template.lang",
                    DefaultExt = ".lang",
                    AddExtension = true
                };
                if (dig.ShowDialog() == true)
                {
                    LanguagePackage.Instance.Save(obj != "Currently", dig.FileName);
                }
            }
        }

        private void MouseWheelCommandExecute(MouseWheelEventArgs? e)
        {
            if (e != null && this.AppItemPages.Count > 1)
            {
                if (e.Timestamp - _wheelTimestamp > 200)
                {
                    if (e.Delta > 0)
                    {
                        for (int i = 0; i < this.AppItemPages.Count; i++)
                        {
                            if (i > 0 && this.AppItemPages[i] == this.CurrentAppItemPage)
                            {
                                this.CurrentAppItemPage = this.AppItemPages[i - 1];
                            }
                        }
                    }
                    else if (e.Delta < 0)
                    {
                        for (int i = this.AppItemPages.Count - 1; i >= 0; i--)
                        {
                            if (i < this.AppItemPages.Count - 1 && this.AppItemPages[i] == this.CurrentAppItemPage)
                            {
                                this.CurrentAppItemPage = this.AppItemPages[i + 1];
                            }
                        }
                    }
                    _wheelTimestamp = e.Timestamp;
                }
            }
        }

        private void MoveOutGroupCommandExecute()
        {
            if (this.Editing is AppItem item && this.CurrentAppItemPage != null)
            {
                if (this.CurrentAppItemPage.AppItemGroups.Count < Settings.Instance.ShelfCol * Settings.Instance.ShelfRow)
                {
                    foreach (var page in this.AppItemPages)
                    {
                        page.RemoveAppItem(item);
                    }
                    var group = new AppItemGroup("Group");
                    group.AppItems.Add(item);
                    this.CurrentAppItemPage.AppItemGroups.Add(group);
                    General.Instance.Modified = true;
                }
                else
                {
                    ToastManager.Default.Show(LanguagePackage.Instance.Messages.TooManyItems, 5000, ToastOptions.Exclamation, null);
                }
            }
            this.CurrentAppItemGroup = null;
            this.CurrentAppItem = null;
            this.Editing = null;
        }

        private void MoveToNextPageCommandExecute()
        {
            if (this.Editing is AppItemGroup group && this.CurrentAppItemPage != null)
            {
                var index = this.AppItemPages.IndexOf(this.CurrentAppItemPage) + 1;
                while (index <= this.AppItemPages.Count - 1)
                {
                    if (this.AppItemPages[index].AppItemGroups.Count < Settings.Instance.ShelfCol * Settings.Instance.ShelfRow)
                    {
                        break;
                    }
                    index++;
                }
                if (index > this.AppItemPages.Count - 1)
                {
                    var newPage = new AppItemPage(Guid.NewGuid().ToString());
                    this.CurrentAppItemPage.AppItemGroups.Remove(group);
                    newPage.AppItemGroups.Add(group);
                    this.AppItemPages.Add(newPage);
                    this.CurrentAppItemPage = newPage;
                }
                else
                {
                    this.CurrentAppItemPage.AppItemGroups.Remove(group);
                    this.CurrentAppItemPage = this.AppItemPages[index];
                    this.CurrentAppItemPage.AppItemGroups.Add(group);
                }
            }
            this.CurrentAppItemGroup = null;
            this.CurrentAppItem = null;
            this.Editing = null;
        }

        private void MoveToPreviousPageCommandExecute()
        {
            if (this.Editing is AppItemGroup group && this.CurrentAppItemPage != null)
            {
                var index = this.AppItemPages.IndexOf(this.CurrentAppItemPage) - 1;
                while (index >= 0)
                {
                    if (this.AppItemPages[index].AppItemGroups.Count < Settings.Instance.ShelfCol * Settings.Instance.ShelfRow)
                    {
                        break;
                    }
                    index--;
                }
                if (index < 0)
                {
                    var newPage = new AppItemPage(Guid.NewGuid().ToString());
                    this.CurrentAppItemPage.AppItemGroups.Remove(group);
                    newPage.AppItemGroups.Add(group);
                    this.AppItemPages.Insert(0, newPage);
                    this.CurrentAppItemPage = newPage;
                }
                else
                {
                    this.CurrentAppItemPage.AppItemGroups.Remove(group);
                    this.CurrentAppItemPage = this.AppItemPages[index];
                    this.CurrentAppItemPage.AppItemGroups.Add(group);
                }
            }
            this.CurrentAppItemGroup = null;
            this.CurrentAppItem = null;
            this.Editing = null;
        }

        private void OpenUriCommandExecute(Uri? uri)
        {
            if (uri != null)
            {
                Process.Start(new ProcessStartInfo(uri.ToString()) { UseShellExecute = true });
            }
        }

        private void RemoveGroupCommandExecute()
        {
            if (this.Editing is AppItemGroup group && this.CurrentAppItemPage != null)
            {
                DialogManager.Default.Show(string.Format(LanguagePackage.Instance.Messages.RemoveGroupConfirm, group.Title, group.AppItems.Count),
                                           string.Empty,
                                           DialogButtons.OKCancel,
                                           DialogDefaultButton.Cancel,
                                           DialogCloseButton.Ordinary,
                                           DialogImage.None,
                                           DialogSize.Default,
                                           false,
                                           DialogLocalization.Default,
                                           null,
                                           (e) =>
                                           {
                                               if (e.DialogResult == DialogResult.OK)
                                               {
                                                   foreach (var page in this.AppItemPages)
                                                   {
                                                       if (page.AppItemGroups.Remove(group))
                                                       {
                                                           break;
                                                       }
                                                   }
                                               }
                                           },
                                           null);
            }
            this.CurrentAppItemGroup = null;
            this.CurrentAppItem = null;
            this.Editing = null;
        }

        private void SetLocationCommandExecute(HonooUI.WPF.Controls.Window? window)
        {
            if (window != null)
            {
                Size area = SystemParameters.WorkArea.Size;
                window.Left = (area.Width - window.ActualWidth) / 2;
                window.Top = area.Height - window.ActualHeight - 8;
            }
        }

        private void ToggleSettingsMenuCommandExecute(DragEventArgs? args)
        {
            this.CurrentAppItemGroup = null;
            this.CurrentAppItem = null;
            this.SettingsMenuVisibility = this.SettingsMenuVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void WindowClosingCommandExecute(CancelEventArgs? args)
        {
            if (General.Instance.Modified)
            {
                using (var manager = new XConfigManager())
                {
                    foreach (var page in this.AppItemPages)
                    {
                        if (page.AppItemGroups.Count > 0)
                        {
                            XList appPage = manager.Default.Properties.Add(page.Id, new XList());
                            appPage.Attributes.AddString("Kind", "Page");
                            foreach (var group in page.AppItemGroups)
                            {
                                if (group.AppItems.Count > 0)
                                {
                                    XList appGroup = appPage.Properties.Add(new XList());
                                    appGroup.Attributes.AddString("Kind", "Group");
                                    appGroup.Attributes.AddString("Title", group.GroupTitle);
                                    foreach (var item in group.AppItems)
                                    {
                                        XDictionary appItem = appGroup.Properties.Add(new XDictionary());
                                        appItem.Attributes.AddString("Kind", item.Kind.ToString());
                                        appItem.Properties.AddString("Title", item.Title);
                                        appItem.Properties.AddString("FullName", item.FullName);
                                        appItem.Properties.AddString("Arguments", item.Arguments);
                                        appItem.Properties.AddString("Privilege", item.Privilege.ToString());
                                        appItem.Properties.AddString("Folder", item.Folder);
                                        appItem.Properties.AddString("Description", item.Description);

                                        var bitmap = item.Icon.Source as BitmapSource;
                                        using (var ms = new MemoryStream())
                                        {
                                            BitmapEncoder encoder = new PngBitmapEncoder();
                                            encoder.Frames.Add(BitmapFrame.Create(bitmap));
                                            encoder.Save(ms);
                                            ms.Seek(0, SeekOrigin.Begin);
                                            byte[] data = ms.ToArray();
                                            string hex = Honoo.IO.Binaries.GetHex(data, 0, data.Length, true, string.Empty, 60, string.Empty);

                                            appItem.Properties.AddString("IconCache", "\r\n" + hex + "\r\n");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    manager.Save(_appListFile);
                }
            }
        }

        private void WindowDeactivatedCommandExecute(HonooUI.WPF.Controls.Window? window)
        {
            if (window != null && !Settings.Instance.Pinned)
            {
                window.WindowState = WindowState.Minimized;
            }
        }

        private void WindowLoadedCommandExecute()
        {
            int pageCapacity = Settings.Instance.ShelfCol * Settings.Instance.ShelfRow;
            int groupCapacity = Settings.Instance.GroupCol * Settings.Instance.GroupRow;
            try
            {
                using (var manager = new XConfigManager(_appListFile, true))
                {
                    foreach (var appPage in manager.Default.Properties)
                    {
                        try
                        {
                            var page = new AppItemPage(appPage.Key);
                            foreach (XList appGroup in ((XList)appPage.Value).Properties.Cast<XList>())
                            {
                                try
                                {
                                    var group = new AppItemGroup(appGroup.Attributes.GetStringValue("Title"));
                                    foreach (XDictionary item in appGroup.Properties.Cast<XDictionary>())
                                    {
                                        try
                                        {
                                            var appItem = new AppItem(item);
                                            if (group.AppItems.Count < groupCapacity)
                                            {
                                                group.AppItems.Add(new AppItem(item));
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                    if (page.AppItemGroups.Count < pageCapacity)
                                    {
                                        page.AppItemGroups.Add(group);
                                    }
                                }
                                catch
                                {
                                }
                            }
                            this.AppItemPages.Add(page);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
            }
            for (int i = this.AppItemPages.Count - 1; i >= 0; i--)
            {
                var page = this.AppItemPages[i];
                if (page.AppItemGroups.Count > 0)
                {
                    for (int j = page.AppItemGroups.Count - 1; j >= 0; j--)
                    {
                        if (page.AppItemGroups[j].AppItems.Count == 0)
                        {
                            page.AppItemGroups.RemoveAt(j);
                        }
                    }
                }
                else
                {
                    this.AppItemPages.RemoveAt(i);
                }
            }
            if (this.AppItemPages.Count == 0)
            {
                this.AppItemPages.Add(new AppItemPage(Guid.NewGuid().ToString()));
            }
            this.CurrentAppItemPage = this.AppItemPages[0];
            General.Instance.Modified = false;
        }
    }
}