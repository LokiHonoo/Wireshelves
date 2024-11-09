using CommunityToolkit.Mvvm.ComponentModel;
using Honoo.Configuration;
using HonooUI.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace Wireshelves.ViewModels
{
    public partial class Settings : ObservableObject
    {
        #region Instance

        public static Settings Instance { get; } = new Settings();

        #endregion Instance

        #region Members

        private readonly string _configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml");
        private readonly string _languageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "languages");

        [ObservableProperty]
        private bool _allowRemove;

        [ObservableProperty]
        private int _groupCol = 4;

        [ObservableProperty]
        private int _groupRow = 4;

        [ObservableProperty]
        private KeyValuePair<string, string> _language;

        [ObservableProperty]
        private bool _pinned;

        [ObservableProperty]
        private int _shelfCol;

        [ObservableProperty]
        private int _shelfRow;

        [ObservableProperty]
        private ThemeStyle? _themeStyle;

        [ObservableProperty]
        private double _windowLeft;

        [ObservableProperty]
        private double _windowTop;

        public Dictionary<string, string> Languages { get; } = new([new("Default", string.Empty)]);
        public ObservableCollection<int> ShelfCols { get; } = [5, 6, 7, 8, 9, 10, 11, 12];
        public ObservableCollection<int> ShelfRows { get; } = [5, 6, 7, 8];
        public ThemeStyle[] ThemeStyles { get; } = [HonooUI.WPF.ThemeStyle.Light, HonooUI.WPF.ThemeStyle.Dark];

        #endregion Members

        #region Construction

        public Settings()
        {
            this.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.ThemeStyle):
                    if (this.ThemeStyle != null)
                    {
                        Theme.Instance.ThemeStyle = this.ThemeStyle.Value;
                    }
                    break;

                case nameof(this.Language):
                    if (this.Language.Key == "Default")
                    {
                        LanguagePackage.Instance.ResetDefault();
                    }
                    else if (File.Exists(this.Language.Value))
                    {
                        try
                        {
                            LanguagePackage.Instance.Load(this.Language.Value);
                        }
                        catch
                        {
                            LanguagePackage.Instance.ResetDefault();
                        }
                    }
                    DialogLocalization.Default = new DialogLocalization(LanguagePackage.Instance.DialogButton.OkText,
                                                                        LanguagePackage.Instance.DialogButton.CancelText,
                                                                        LanguagePackage.Instance.DialogButton.YesText,
                                                                        LanguagePackage.Instance.DialogButton.NoText);
                    break;

                default: break;
            }
        }

        #endregion Construction

        internal void Load()
        {
            var folder = new DirectoryInfo(_languageFolder);
            if (folder.Exists)
            {
                foreach (FileInfo file in folder.EnumerateFiles())
                {
                    if (file.Extension.Equals(".lang", StringComparison.InvariantCultureIgnoreCase) && file.Length < 1024 * 100)
                    {
                        try
                        {
                            using (var manager = new XConfigManager(file.FullName))
                            {
                                string langName = manager.Default.Properties.GetStringValue("LangName");
                                if (langName != "Default")
                                {
                                    this.Languages.Add(langName, file.FullName);
                                }
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
                Directory.CreateDirectory(_languageFolder);
            }
            using (var manager = new XConfigManager(_configFile, true))
            {
                this.WindowLeft = manager.Default.Properties.GetValue("WindowLeft", new XString("200")).GetDoubleValue();
                this.WindowTop = manager.Default.Properties.GetValue("WindowTop", new XString("600")).GetDoubleValue();
                this.ThemeStyle = manager.Default.Properties.GetValue("ThemeStyle", new XString(HonooUI.WPF.ThemeStyle.Dark.ToString())).GetEnumValue<ThemeStyle>();
                this.Pinned = manager.Default.Properties.GetValue("Pinned", new XString("False")).GetBooleanValue();
                int col = manager.Default.Properties.GetValue("ShelfCol", new XString("5")).GetInt32Value();
                int row = manager.Default.Properties.GetValue("ShelfRow", new XString("5")).GetInt32Value();
                this.ShelfCol = col < 5 || col > 12 ? 5 : col;
                this.ShelfRow = row < 4 || row > 8 ? 5 : row;
                string langName = manager.Default.Properties.GetStringValue("LangName", string.Empty);
                if (this.Languages.TryGetValue(langName, out string? fileName))
                {
                    this.Language = new KeyValuePair<string, string>(langName, fileName);
                }
                else
                {
                    this.Language = new KeyValuePair<string, string>("Default", string.Empty);
                }
            }
        }

        internal void Save()
        {
            using (var manager = new XConfigManager(_configFile, true))
            {
                manager.Default.Properties.AddOrUpdateString("WindowLeft", this.WindowLeft.ToString());
                manager.Default.Properties.AddOrUpdateString("WindowTop", this.WindowTop.ToString());
                manager.Default.Properties.AddOrUpdateString("ThemeStyle", this.ThemeStyle!.ToString());
                manager.Default.Properties.AddOrUpdateString("Pinned", this.Pinned.ToString());
                manager.Default.Properties.AddOrUpdateString("ShelfCol", this.ShelfCol.ToString());
                manager.Default.Properties.AddOrUpdateString("ShelfRow", this.ShelfRow.ToString());
                manager.Default.Properties.AddOrUpdateString("LangName", this.Language.Key);

                manager.Save(_configFile);
            }
        }
    }
}