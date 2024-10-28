using Honoo.Configuration;
using HonooUI.WPF;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Wireshelves.ViewModels;

namespace Wireshelves.Models
{
    internal static class Config
    {
        private static readonly string _configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Wireshelves.xml");
        private static readonly string _languageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Languages");

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:不捕获常规异常类型", Justification = "<挂起>")]
        internal static void LoadSettings()
        {
            var gvm = Locator.GlobalViewModel;
            //
            if (Directory.Exists(_languageFolder))
            {
                foreach (FileInfo file in new DirectoryInfo(_languageFolder).EnumerateFiles())
                {
                    if (file.Extension == ".lng" && file.Length < 1024 * 100)
                    {
                        try
                        {
                            string fileName = file.FullName;
                            using (var manager = new XConfigManager(file.FullName))
                            {
                                string name = manager.Default.Properties.GetValue<XString>("ShortName").GetStringValue();
                                gvm.Languages.Add(new Language(name, fileName));
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            //
            using (var manager = File.Exists(_configFile) ? new XConfigManager(_configFile) : new XConfigManager())
            {
                gvm.AppLocation = manager.Default.Properties.TryGetValue("AppLocation", out XString value) ? value.GetInt32Value() : 0;
                gvm.Pin = manager.Default.Properties.TryGetValue("Pin", out value) ? value.GetBooleanValue() : false;
                gvm.ThemeStyle = manager.Default.Properties.TryGetValue("ThemeStyle", out value) ? value.GetEnumValue<ThemeStyle>() : ThemeStyle.Dark;
                int col = manager.Default.Properties.TryGetValue("ShelfCol", out value) ? value.GetInt32Value() : 4;
                int row = manager.Default.Properties.TryGetValue("ShelfRow", out value) ? value.GetInt32Value() : 6;
                gvm.ShelfCol = col < 4 || col > 12 ? 4 : col;
                gvm.ShelfRow = row < 3 || row > 7 ? 6 : row;
                gvm.MoreControllers = manager.Default.Properties.TryGetValue("MoreControllers", out value) ? value.GetBooleanValue() : false;
                gvm.DeactivatedWork = manager.Default.Properties.TryGetValue("DeactivatedWork", out value) ? value.GetInt32Value() : 0;
                int max = col * row;
                var list = new ObservableCollection<AppItemViewModel>();
                foreach (var section in manager.Sections)
                {
                    var shelf = new ShelfViewModel([]);
                    gvm.Shelves.Add(shelf);
                    foreach (var item in section.Properties)
                    {
                        try
                        {
                            var info = (XDictionary)item.Value;
                            string path = info.Properties.GetValue("Path").GetStringValue();
                            var vm = new AppItemViewModel(item.Key, info.Properties.GetValue("Title").GetStringValue(), path)
                            {
                                RunAsAdministrator = info.Properties.TryGetValue("RunAs", out value) ? value.GetBooleanValue() : false,
                            };
                            if (File.Exists(path))
                            {
                                string ext = Path.GetExtension(path).ToUpperInvariant();
                                if (ext == ".EXE")
                                {
                                    vm.Kind = AppItemKind.File;
                                    vm.Icon = Honoo.Drawing.Icon.GetIcon(path);
                                }
                                else if (ext == ".LNK")
                                {
                                    var shell = new IWshRuntimeLibrary.WshShell();
                                    var shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(path);
                                    vm.Kind = AppItemKind.Shortcut;
                                    vm.Icon = Honoo.Drawing.Icon.GetIcon(shortcut.TargetPath);
                                }
                            }
                            else if (Directory.Exists(path))
                            {
                                vm.Kind = AppItemKind.Folder;
                                vm.Icon = (Geometry)Application.Current.FindResource("FolderFillGeometry");
                                vm.IconColorBrush = new SolidColorBrush(Color.FromRgb(255, 207, 75));
                            }
                            if (shelf.Appitems.Count >= max)
                            {
                                list.Add(vm);
                            }
                            else
                            {
                                shelf.Appitems.Add(vm);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                if (list.Count > 0)
                {
                    var shelf = new ShelfViewModel([]);
                    gvm.Shelves.Add(shelf);
                    foreach (var vm in list)
                    {
                        if (shelf.Appitems.Count >= max)
                        {
                            shelf = new ShelfViewModel([]);
                            gvm.Shelves.Add(shelf);
                        }
                        else
                        {
                            shelf.Appitems.Add(vm);
                        }
                    }
                }
                if (gvm.Shelves.Count == 0 || gvm.Shelves[^1].Appitems.Count > 0)
                {
                    gvm.Shelves.Add(new ShelfViewModel([]));
                }
                int si = manager.Default.Properties.TryGetValue("ShelfIndex", out value) ? value.GetInt32Value() : 0;
                gvm.ShelfIndex = si < 0 || si >= gvm.Shelves.Count ? 0 : si;
                // Set moveable location once.
                if (gvm.AppLocation == 3)
                {
                    double width = gvm.ShelfWidth + 48;
                    double height = gvm.ShelfHeight + 203;
                    var area = SystemParameters.WorkArea;
                    gvm.WindowTop = manager.Default.Properties.TryGetValue("WindowTop", out value) ? value.GetDoubleValue() : (area.Height - height) / 2;
                    gvm.WindowLeft = manager.Default.Properties.TryGetValue("WindowLeft", out value) ? value.GetDoubleValue() : (area.Width - width) / 2;
                    if (gvm.WindowTop < 0)
                    {
                        gvm.WindowTop = 0;
                    }
                    if (gvm.WindowLeft < 0)
                    {
                        gvm.WindowLeft = 0;
                    }
                }
                //
                if (manager.Default.Properties.TryGetValue("LanguageName", out XString languageName))
                {
                    foreach (var lang in gvm.Languages)
                    {
                        if (languageName.GetStringValue() == lang.Name)
                        {
                            gvm.Language = lang;
                            break;
                        }
                    }
                    gvm.Language ??= gvm.Languages[0];
                }
                else
                {
                    gvm.Language = gvm.Languages[0];
                }
                if (gvm.Language.Name == "Default")
                {
                    Locator.Localization.Reset(Localization.Template);
                }
                else if (File.Exists(gvm.Language.FileName))
                {
                    Locator.Localization.Reset(gvm.Language.FileName);
                }
            }
        }

        internal static void SaveSettings()
        {
            var GlobalViewModel = Locator.GlobalViewModel;
            using (XConfigManager manager = File.Exists(_configFile) ? new XConfigManager(_configFile) : new XConfigManager())
            {
                manager.Default.Properties.AddOrUpdate("AppLocation", new XString(GlobalViewModel.AppLocation.ToString(CultureInfo.InvariantCulture)));
                manager.Default.Properties.AddOrUpdate("Pin", new XString(GlobalViewModel.Pin.ToString(CultureInfo.InvariantCulture)));
                manager.Default.Properties.AddOrUpdate("WindowTop", new XString(GlobalViewModel.WindowTop.ToString(CultureInfo.InvariantCulture)));
                manager.Default.Properties.AddOrUpdate("WindowLeft", new XString(GlobalViewModel.WindowLeft.ToString(CultureInfo.InvariantCulture)));
                manager.Default.Properties.AddOrUpdate("ThemeStyle", new XString(GlobalViewModel.ThemeStyle.ToString()));
                manager.Default.Properties.AddOrUpdate("ShelfCol", new XString(GlobalViewModel.ShelfCol.ToString(CultureInfo.InvariantCulture)));
                manager.Default.Properties.AddOrUpdate("ShelfRow", new XString(GlobalViewModel.ShelfRow.ToString(CultureInfo.InvariantCulture)));
                manager.Default.Properties.AddOrUpdate("MoreControllers", new XString(GlobalViewModel.MoreControllers.ToString(CultureInfo.InvariantCulture)));
                manager.Default.Properties.AddOrUpdate("DeactivatedWork", new XString(GlobalViewModel.DeactivatedWork.ToString(CultureInfo.InvariantCulture)));
                if (GlobalViewModel.Language != null)
                {
                    manager.Default.Properties.AddOrUpdate("LanguageName", new XString(GlobalViewModel.Language.Name.ToString(CultureInfo.InvariantCulture)));
                }
                manager.Sections.Clear();
                int index = 0;
                foreach (var shelf in GlobalViewModel.Shelves)
                {
                    if (shelf.Appitems.Count > 0)
                    {
                        var section = manager.Sections.Add("Shelf" + index);
                        foreach (var app in shelf.Appitems)
                        {
                            XDictionary dictionary = section.Properties.Add(app.ID, new XDictionary());
                            dictionary.Properties.Add("Title", new XString(app.Title));
                            dictionary.Properties.Add("Path", new XString(app.Path));
                            dictionary.Properties.Add("RunAs", new XString(app.RunAsAdministrator.ToString()));
                        }
                        index++;
                    }
                }
                manager.Save(_configFile);
            }
        }
    }
}