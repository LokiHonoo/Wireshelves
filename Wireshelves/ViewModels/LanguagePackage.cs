using Honoo.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Wireshelves.ViewModels
{
    /// <summary>
    /// Language package class. Changed-notify interface implemented [.NET 6.0 later With Project setting field <![CDATA[<Nullable>enable</Nullable>]]>].
    /// <br />Reference in viewmodel to visit or used single instance <see cref="Instance"/> to visit.
    /// <br />e.g.
    /// <code>
    /// <![CDATA[public LanguagePackage LanguagePackageReference { get; } = LanguagePackage.Instance;]]>
    /// <br />
    /// <![CDATA[<TextBlock Text="{Binding LanguagePackageReference.Main.HasNewVersion}" />]]>
    /// </code>
    /// <br />Or:
    /// <code>
    /// <![CDATA[<TextBlock Text="{Binding Main.HasNewVersion, Source={x:Static vm:LanguagePackage.Instance}}" />]]>
    /// </code>
    /// <br />Install nuget packages:
    /// <br /><see href="https://www.nuget.org/packages/Honoo.Configuration.ConfigurationManager"/>.
    /// </summary>
    public sealed class LanguagePackage
    {
        #region Instance

        /// <summary>
        /// Language package instance.
        /// </summary>
        public static LanguagePackage Instance { get; } = new LanguagePackage();

        #endregion Instance

        #region Members

        /// <summary>
        /// Managed translation entries count.
        /// </summary>
        public int Count { get; } = 40;

        /// <summary>
        /// Language package creator.
        /// </summary>
        public const string CREATOR = "Honoo Language Localisation Converter 1.0.5";

        /// <summary>
        /// Creator website.
        /// </summary>
        public const string WEBSITE = "https://github.com/LokiHonoo/Honoo-Language-Localisation-Converter";

        #region Sections

        /// <summary>
        /// Information section.
        /// </summary>
        public __Information Information { get; } = new __Information();

        /// <summary>
        /// DialogButton section.
        /// </summary>
        public __DialogButton DialogButton { get; } = new __DialogButton();

        /// <summary>
        /// Main section.
        /// </summary>
        public __Main Main { get; } = new __Main();

        /// <summary>
        /// Settings section.
        /// </summary>
        public __Settings Settings { get; } = new __Settings();

        /// <summary>
        /// AppItem section.
        /// </summary>
        public __AppItem AppItem { get; } = new __AppItem();

        /// <summary>
        /// Messages section.
        /// </summary>
        public __Messages Messages { get; } = new __Messages();

        #endregion Sections

        #endregion Members

        /// <summary>
        /// Initialize new instance of LanguagePackage class.
        /// </summary>
        internal LanguagePackage()
        {
        }

        /// <summary>
        /// Gets information pairs from language file.
        /// </summary>
        /// <param name="stream">Language file name.</param>
        /// <returns>Information pairs.</returns>
        public static Dictionary<string, string> GetInformation(string fileName)
        {
            var pairs = new Dictionary<string, string>();
            using (var manager = new XConfigManager(fileName))
            {
                if (manager.Sections.TryGetValue("Information", out XSection section))
                {
                    foreach (KeyValuePair<string, XProperty> property in section.Properties)
                    {
                        pairs.Add(property.Key, ((XString)property.Value).GetStringValue());
                    }
                }
            }
            return pairs;
        }

        /// <summary>
        /// Gets information pairs from language stream.
        /// </summary>
        /// <param name="stream">Language stream.</param>
        /// <returns>Information pairs.</returns>
        public static Dictionary<string, string> GetInformation(Stream stream)
        {
            var pairs = new Dictionary<string, string>();
            using (var manager = new XConfigManager(stream))
            {
                if (manager.Sections.TryGetValue("Information", out XSection section))
                {
                    foreach (KeyValuePair<string, XProperty> property in section.Properties)
                    {
                        pairs.Add(property.Key, ((XString)property.Value).GetStringValue());
                    }
                }
            }
            return pairs;
        }

        /// <summary>
        /// Load language file and return loaded translation entries count.
        /// </summary>
        /// <param name="fileName">Language file name.</param>
        /// <returns>Loaded translation entries count.</returns>
        public int Load(string fileName)
        {
            return Load(fileName, out _);
        }

        /// <summary>
        /// Load language file and return loaded translation entries count.
        /// </summary>
        /// <param name="fileName">Language file name.</param>
        /// <param name="missingNames">Missing translation entry names when loaded.</param>
        /// <returns>Loaded translation entries count.</returns>
        public int Load(string fileName, out List<string> missingNames)
        {
            var missing = new List<string>();
            int loaded = 0;
            using (var manager = new XConfigManager(fileName))
            {
                loaded += __Information.__LoadInternal(this, manager, missing);
                loaded += __DialogButton.__LoadInternal(this, manager, missing);
                loaded += __Main.__LoadInternal(this, manager, missing);
                loaded += __Settings.__LoadInternal(this, manager, missing);
                loaded += __AppItem.__LoadInternal(this, manager, missing);
                loaded += __Messages.__LoadInternal(this, manager, missing);
            }
            missingNames = missing;
            return loaded;
        }

        /// <summary>
        /// Load language stream and return loaded translation entries count.
        /// </summary>
        /// <param name="stream">Language stream.</param>
        /// <returns>Loaded translation entries count.</returns>
        public int Load(Stream stream)
        {
            return Load(stream, out _);
        }

        /// <summary>
        /// Load language stream and return loaded translation entries count.
        /// </summary>
        /// <param name="stream">Language stream.</param>
        /// <param name="missingNames">Missing translation entry names when loaded.</param>
        /// <returns>Loaded translation entries count.</returns>
        public int Load(Stream stream, out List<string> missingNames)
        {
            var missing = new List<string>();
            int loaded = 0;
            using (var manager = new XConfigManager(stream))
            {
                loaded += __Information.__LoadInternal(this, manager, missing);
                loaded += __DialogButton.__LoadInternal(this, manager, missing);
                loaded += __Main.__LoadInternal(this, manager, missing);
                loaded += __Settings.__LoadInternal(this, manager, missing);
                loaded += __AppItem.__LoadInternal(this, manager, missing);
                loaded += __Messages.__LoadInternal(this, manager, missing);
            }
            missingNames = missing;
            return loaded;
        }

        /// <summary>
        /// Reset all translation entries to default values.
        /// </summary>
        public void ResetDefault()
        {
            __Information.__ResetDefaultInternal(this);
            __DialogButton.__ResetDefaultInternal(this);
            __Main.__ResetDefaultInternal(this);
            __Settings.__ResetDefaultInternal(this);
            __AppItem.__ResetDefaultInternal(this);
            __Messages.__ResetDefaultInternal(this);
        }

        /// <summary>
        /// Save to language file.
        /// </summary>
        /// <param name="defaultField">Select current fields or default fields.</param>
        /// <param name="fileName">Language file name.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>")]
        public void Save(bool defaultField, string fileName)
        {
            using (var manager = new XConfigManager())
            {
                manager.Default.Properties.AddString("Creator", CREATOR);
                manager.Default.Properties.AddString("Website", WEBSITE);
                manager.Default.Properties.AddString("CreatedTime", DateTime.Now.ToString("R"));
                __Information.__SaveInternal(this, defaultField, manager);
                __DialogButton.__SaveInternal(this, defaultField, manager);
                __Main.__SaveInternal(this, defaultField, manager);
                __Settings.__SaveInternal(this, defaultField, manager);
                __AppItem.__SaveInternal(this, defaultField, manager);
                __Messages.__SaveInternal(this, defaultField, manager);
                manager.Save(fileName);
            }
        }

        /// <summary>
        /// Save to language stream.
        /// </summary>
        /// <param name="defaultField">Select current fields or default fields.</param>
        /// <param name="stream">Language stream.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>")]
        public void Save(bool defaultField, Stream stream)
        {
            using (var manager = new XConfigManager())
            {
                manager.Default.Properties.AddString("Creator", CREATOR);
                manager.Default.Properties.AddString("Website", WEBSITE);
                manager.Default.Properties.AddString("CreatedTime", DateTime.Now.ToString("R"));
                __Information.__SaveInternal(this, defaultField, manager);
                __DialogButton.__SaveInternal(this, defaultField, manager);
                __Main.__SaveInternal(this, defaultField, manager);
                __Settings.__SaveInternal(this, defaultField, manager);
                __AppItem.__SaveInternal(this, defaultField, manager);
                __Messages.__SaveInternal(this, defaultField, manager);
                manager.Save(stream);
            }
        }

        #region Information

        /// <summary>
        /// Information section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public sealed class __Information : INotifyPropertyChanging, INotifyPropertyChanged
        {
            #region Events

            /// <summary>
            /// Property changed event handler.
            /// </summary>
            public event PropertyChangedEventHandler? PropertyChanged;

            /// <summary>
            /// Property changing event handler.
            /// </summary>
            public event PropertyChangingEventHandler? PropertyChanging;

            private void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

            private void OnPropertyChanging(string name)
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
            }

            #endregion Events

            #region Comments

            private const string _appName_c = "";
            private const string _appVer_c = "";
            private const string _langName_c = "";
            private const string _langVer_c = "";
            private const string _author_c = "";
            private const string _email_c = "";
            private const string _website_c = "";
            private const string _remarks_c = "";

            #endregion Comments

            #region Default

            private const string _appName_d = "Wireshelves";
            private const string _appVer_d = "1.x";
            private const string _langName_d = "en-US";
            private const string _langVer_d = "00";
            private const string _author_d = "Honoo Language Localisation Converter";
            private const string _email_d = "";
            private const string _website_d = "https://github.com/LokiHonoo/Honoo-Language-Localisation-Converter";
            private const string _remarks_d = "";

            #endregion Default

            #region Members

            private string _appName = _appName_d;
            private string _appVer = _appVer_d;
            private string _langName = _langName_d;
            private string _langVer = _langVer_d;
            private string _author = _author_d;
            private string _email = _email_d;
            private string _website = _website_d;
            private string _remarks = _remarks_d;

            /// <summary>
            /// 
            /// </summary>
            public string AppName { get { return _appName; } set { OnPropertyChanging(nameof(this.AppName)); _appName = value; OnPropertyChanged(nameof(this.AppName)); } }

            /// <summary>
            /// 
            /// </summary>
            public string AppVer { get { return _appVer; } set { OnPropertyChanging(nameof(this.AppVer)); _appVer = value; OnPropertyChanged(nameof(this.AppVer)); } }

            /// <summary>
            /// 
            /// </summary>
            public string LangName { get { return _langName; } set { OnPropertyChanging(nameof(this.LangName)); _langName = value; OnPropertyChanged(nameof(this.LangName)); } }

            /// <summary>
            /// 
            /// </summary>
            public string LangVer { get { return _langVer; } set { OnPropertyChanging(nameof(this.LangVer)); _langVer = value; OnPropertyChanged(nameof(this.LangVer)); } }

            /// <summary>
            /// 
            /// </summary>
            public string Author { get { return _author; } set { OnPropertyChanging(nameof(this.Author)); _author = value; OnPropertyChanged(nameof(this.Author)); } }

            /// <summary>
            /// 
            /// </summary>
            public string Email { get { return _email; } set { OnPropertyChanging(nameof(this.Email)); _email = value; OnPropertyChanged(nameof(this.Email)); } }

            /// <summary>
            /// 
            /// </summary>
            public string Website { get { return _website; } set { OnPropertyChanging(nameof(this.Website)); _website = value; OnPropertyChanged(nameof(this.Website)); } }

            /// <summary>
            /// 
            /// </summary>
            public string Remarks { get { return _remarks; } set { OnPropertyChanging(nameof(this.Remarks)); _remarks = value; OnPropertyChanged(nameof(this.Remarks)); } }

            #endregion Members

            internal __Information()
            {
            }

            internal static int __LoadInternal(LanguagePackage instance, XConfigManager manager, List<string> missing)
            {
                return instance.Information.__LoadInternal(manager, missing);
            }

            internal static void __ResetDefaultInternal(LanguagePackage instance)
            {
                instance.Information.__ResetDefaultInternal();
            }

            internal static void __SaveInternal(LanguagePackage instance, bool defaultField, XConfigManager manager)
            {
                instance.Information.__SaveInternal(defaultField, manager);
            }

            private static string __GetTranslationEntryInternal(XSection section, string name, string defaultValue, List<string> missing, ref int loaded)
            {
                if (section.Properties.TryGetStringValue(name, out string value))
                {
                    loaded++;
                    return value;
                }
                else
                {
                    missing.Add(name);
                    return defaultValue;
                }
            }

            private int __LoadInternal(XConfigManager manager, List<string> missing)
            {
                if (manager.Sections.TryGetValue("Information", out XSection section))
                {
                    int loaded = 0;
                    this.AppName = __GetTranslationEntryInternal(section, "AppName", _appName_d, missing, ref loaded);
                    this.AppVer = __GetTranslationEntryInternal(section, "AppVer", _appVer_d, missing, ref loaded);
                    this.LangName = __GetTranslationEntryInternal(section, "LangName", _langName_d, missing, ref loaded);
                    this.LangVer = __GetTranslationEntryInternal(section, "LangVer", _langVer_d, missing, ref loaded);
                    this.Author = __GetTranslationEntryInternal(section, "Author", _author_d, missing, ref loaded);
                    this.Email = __GetTranslationEntryInternal(section, "Email", _email_d, missing, ref loaded);
                    this.Website = __GetTranslationEntryInternal(section, "Website", _website_d, missing, ref loaded);
                    this.Remarks = __GetTranslationEntryInternal(section, "Remarks", _remarks_d, missing, ref loaded);
                    return loaded;
                }
                else
                {
                    missing.Add("AppName");
                    missing.Add("AppVer");
                    missing.Add("LangName");
                    missing.Add("LangVer");
                    missing.Add("Author");
                    missing.Add("Email");
                    missing.Add("Website");
                    missing.Add("Remarks");
                    return 0;
                }
            }

            private void __ResetDefaultInternal()
            {
                this.AppName = _appName_d;
                this.AppVer = _appVer_d;
                this.LangName = _langName_d;
                this.LangVer = _langVer_d;
                this.Author = _author_d;
                this.Email = _email_d;
                this.Website = _website_d;
                this.Remarks = _remarks_d;
            }

            private void __SaveInternal(bool defaultField, XConfigManager manager)
            {
                XSection section = manager.Sections.Add("Information");
                if (defaultField)
                {
                    section.Properties.AddString("AppName", _appName_d).Comment.SetValue(_appName_c, true);
                    section.Properties.AddString("AppVer", _appVer_d).Comment.SetValue(_appVer_c, true);
                    section.Properties.AddString("LangName", _langName_d).Comment.SetValue(_langName_c, true);
                    section.Properties.AddString("LangVer", _langVer_d).Comment.SetValue(_langVer_c, true);
                    section.Properties.AddString("Author", _author_d).Comment.SetValue(_author_c, true);
                    section.Properties.AddString("Email", _email_d).Comment.SetValue(_email_c, true);
                    section.Properties.AddString("Website", _website_d).Comment.SetValue(_website_c, true);
                    section.Properties.AddString("Remarks", _remarks_d).Comment.SetValue(_remarks_c, true);
                }
                else
                {
                    section.Properties.AddString("AppName", this.AppName).Comment.SetValue(_appName_c, true);
                    section.Properties.AddString("AppVer", this.AppVer).Comment.SetValue(_appVer_c, true);
                    section.Properties.AddString("LangName", this.LangName).Comment.SetValue(_langName_c, true);
                    section.Properties.AddString("LangVer", this.LangVer).Comment.SetValue(_langVer_c, true);
                    section.Properties.AddString("Author", this.Author).Comment.SetValue(_author_c, true);
                    section.Properties.AddString("Email", this.Email).Comment.SetValue(_email_c, true);
                    section.Properties.AddString("Website", this.Website).Comment.SetValue(_website_c, true);
                    section.Properties.AddString("Remarks", this.Remarks).Comment.SetValue(_remarks_c, true);
                }
            }

        }

        #endregion Information

        #region DialogButton

        /// <summary>
        /// DialogButton section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public sealed class __DialogButton : INotifyPropertyChanging, INotifyPropertyChanged
        {
            #region Events

            /// <summary>
            /// Property changed event handler.
            /// </summary>
            public event PropertyChangedEventHandler? PropertyChanged;

            /// <summary>
            /// Property changing event handler.
            /// </summary>
            public event PropertyChangingEventHandler? PropertyChanging;

            private void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

            private void OnPropertyChanging(string name)
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
            }

            #endregion Events

            #region Comments

            private const string _cancelText_c = "";
            private const string _noText_c = "";
            private const string _okText_c = "";
            private const string _yesText_c = "";

            #endregion Comments

            #region Default

            private const string _cancelText_d = "Cancel";
            private const string _noText_d = "No";
            private const string _okText_d = "OK";
            private const string _yesText_d = "Yes";

            #endregion Default

            #region Members

            private string _cancelText = _cancelText_d;
            private string _noText = _noText_d;
            private string _okText = _okText_d;
            private string _yesText = _yesText_d;

            /// <summary>
            /// 
            /// </summary>
            public string CancelText { get { return _cancelText; } set { OnPropertyChanging(nameof(this.CancelText)); _cancelText = value; OnPropertyChanged(nameof(this.CancelText)); } }

            /// <summary>
            /// 
            /// </summary>
            public string NoText { get { return _noText; } set { OnPropertyChanging(nameof(this.NoText)); _noText = value; OnPropertyChanged(nameof(this.NoText)); } }

            /// <summary>
            /// 
            /// </summary>
            public string OkText { get { return _okText; } set { OnPropertyChanging(nameof(this.OkText)); _okText = value; OnPropertyChanged(nameof(this.OkText)); } }

            /// <summary>
            /// 
            /// </summary>
            public string YesText { get { return _yesText; } set { OnPropertyChanging(nameof(this.YesText)); _yesText = value; OnPropertyChanged(nameof(this.YesText)); } }

            #endregion Members

            internal __DialogButton()
            {
            }

            internal static int __LoadInternal(LanguagePackage instance, XConfigManager manager, List<string> missing)
            {
                return instance.DialogButton.__LoadInternal(manager, missing);
            }

            internal static void __ResetDefaultInternal(LanguagePackage instance)
            {
                instance.DialogButton.__ResetDefaultInternal();
            }

            internal static void __SaveInternal(LanguagePackage instance, bool defaultField, XConfigManager manager)
            {
                instance.DialogButton.__SaveInternal(defaultField, manager);
            }

            private static string __GetTranslationEntryInternal(XSection section, string name, string defaultValue, List<string> missing, ref int loaded)
            {
                if (section.Properties.TryGetStringValue(name, out string value))
                {
                    loaded++;
                    return value;
                }
                else
                {
                    missing.Add(name);
                    return defaultValue;
                }
            }

            private int __LoadInternal(XConfigManager manager, List<string> missing)
            {
                if (manager.Sections.TryGetValue("DialogButton", out XSection section))
                {
                    int loaded = 0;
                    this.CancelText = __GetTranslationEntryInternal(section, "CancelText", _cancelText_d, missing, ref loaded);
                    this.NoText = __GetTranslationEntryInternal(section, "NoText", _noText_d, missing, ref loaded);
                    this.OkText = __GetTranslationEntryInternal(section, "OkText", _okText_d, missing, ref loaded);
                    this.YesText = __GetTranslationEntryInternal(section, "YesText", _yesText_d, missing, ref loaded);
                    return loaded;
                }
                else
                {
                    missing.Add("CancelText");
                    missing.Add("NoText");
                    missing.Add("OkText");
                    missing.Add("YesText");
                    return 0;
                }
            }

            private void __ResetDefaultInternal()
            {
                this.CancelText = _cancelText_d;
                this.NoText = _noText_d;
                this.OkText = _okText_d;
                this.YesText = _yesText_d;
            }

            private void __SaveInternal(bool defaultField, XConfigManager manager)
            {
                XSection section = manager.Sections.Add("DialogButton");
                if (defaultField)
                {
                    section.Properties.AddString("CancelText", _cancelText_d).Comment.SetValue(_cancelText_c, true);
                    section.Properties.AddString("NoText", _noText_d).Comment.SetValue(_noText_c, true);
                    section.Properties.AddString("OkText", _okText_d).Comment.SetValue(_okText_c, true);
                    section.Properties.AddString("YesText", _yesText_d).Comment.SetValue(_yesText_c, true);
                }
                else
                {
                    section.Properties.AddString("CancelText", this.CancelText).Comment.SetValue(_cancelText_c, true);
                    section.Properties.AddString("NoText", this.NoText).Comment.SetValue(_noText_c, true);
                    section.Properties.AddString("OkText", this.OkText).Comment.SetValue(_okText_c, true);
                    section.Properties.AddString("YesText", this.YesText).Comment.SetValue(_yesText_c, true);
                }
            }

        }

        #endregion DialogButton

        #region Main

        /// <summary>
        /// Main section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public sealed class __Main : INotifyPropertyChanging, INotifyPropertyChanged
        {
            #region Events

            /// <summary>
            /// Property changed event handler.
            /// </summary>
            public event PropertyChangedEventHandler? PropertyChanged;

            /// <summary>
            /// Property changing event handler.
            /// </summary>
            public event PropertyChangingEventHandler? PropertyChanging;

            private void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

            private void OnPropertyChanging(string name)
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
            }

            #endregion Events

            #region Comments

            private const string _addNextPage_c = "";
            private const string _addPageTip_c = "";
            private const string _addPreviousPage_c = "";
            private const string _appInfoButtonTip_c = "";
            private const string _emptyPageText_c = "";
            private const string _exitButtonTip_c = "";
            private const string _locateButtonTip_c = "";
            private const string _moveOutGroup_c = "";
            private const string _moveToNextPage_c = "";
            private const string _moveToPreviousPage_c = "";
            private const string _pinTip_c = "";
            private const string _removeGroup_c = "";

            #endregion Comments

            #region Default

            private const string _addNextPage_d = "Add Next Page";
            private const string _addPageTip_d = "Add new page";
            private const string _addPreviousPage_d = "Add Previous Page";
            private const string _appInfoButtonTip_d = "Application information";
            private const string _emptyPageText_d = "Pin the application to stay for drop file into here.\r\n\r\nEmpty page will be removed at next starts.";
            private const string _exitButtonTip_d = "Exit";
            private const string _locateButtonTip_d = "Locate Window to bottom";
            private const string _moveOutGroup_d = "Move out from Group";
            private const string _moveToNextPage_d = "Move to Next Page";
            private const string _moveToPreviousPage_d = "Move to Previous Page";
            private const string _pinTip_d = "Pin the application to stay";
            private const string _removeGroup_d = "Remove Group...";

            #endregion Default

            #region Members

            private string _addNextPage = _addNextPage_d;
            private string _addPageTip = _addPageTip_d;
            private string _addPreviousPage = _addPreviousPage_d;
            private string _appInfoButtonTip = _appInfoButtonTip_d;
            private string _emptyPageText = _emptyPageText_d;
            private string _exitButtonTip = _exitButtonTip_d;
            private string _locateButtonTip = _locateButtonTip_d;
            private string _moveOutGroup = _moveOutGroup_d;
            private string _moveToNextPage = _moveToNextPage_d;
            private string _moveToPreviousPage = _moveToPreviousPage_d;
            private string _pinTip = _pinTip_d;
            private string _removeGroup = _removeGroup_d;

            /// <summary>
            /// 
            /// </summary>
            public string AddNextPage { get { return _addNextPage; } set { OnPropertyChanging(nameof(this.AddNextPage)); _addNextPage = value; OnPropertyChanged(nameof(this.AddNextPage)); } }

            /// <summary>
            /// 
            /// </summary>
            public string AddPageTip { get { return _addPageTip; } set { OnPropertyChanging(nameof(this.AddPageTip)); _addPageTip = value; OnPropertyChanged(nameof(this.AddPageTip)); } }

            /// <summary>
            /// 
            /// </summary>
            public string AddPreviousPage { get { return _addPreviousPage; } set { OnPropertyChanging(nameof(this.AddPreviousPage)); _addPreviousPage = value; OnPropertyChanged(nameof(this.AddPreviousPage)); } }

            /// <summary>
            /// 
            /// </summary>
            public string AppInfoButtonTip { get { return _appInfoButtonTip; } set { OnPropertyChanging(nameof(this.AppInfoButtonTip)); _appInfoButtonTip = value; OnPropertyChanged(nameof(this.AppInfoButtonTip)); } }

            /// <summary>
            /// 
            /// </summary>
            public string EmptyPageText { get { return _emptyPageText; } set { OnPropertyChanging(nameof(this.EmptyPageText)); _emptyPageText = value; OnPropertyChanged(nameof(this.EmptyPageText)); } }

            /// <summary>
            /// 
            /// </summary>
            public string ExitButtonTip { get { return _exitButtonTip; } set { OnPropertyChanging(nameof(this.ExitButtonTip)); _exitButtonTip = value; OnPropertyChanged(nameof(this.ExitButtonTip)); } }

            /// <summary>
            /// 
            /// </summary>
            public string LocateButtonTip { get { return _locateButtonTip; } set { OnPropertyChanging(nameof(this.LocateButtonTip)); _locateButtonTip = value; OnPropertyChanged(nameof(this.LocateButtonTip)); } }

            /// <summary>
            /// 
            /// </summary>
            public string MoveOutGroup { get { return _moveOutGroup; } set { OnPropertyChanging(nameof(this.MoveOutGroup)); _moveOutGroup = value; OnPropertyChanged(nameof(this.MoveOutGroup)); } }

            /// <summary>
            /// 
            /// </summary>
            public string MoveToNextPage { get { return _moveToNextPage; } set { OnPropertyChanging(nameof(this.MoveToNextPage)); _moveToNextPage = value; OnPropertyChanged(nameof(this.MoveToNextPage)); } }

            /// <summary>
            /// 
            /// </summary>
            public string MoveToPreviousPage { get { return _moveToPreviousPage; } set { OnPropertyChanging(nameof(this.MoveToPreviousPage)); _moveToPreviousPage = value; OnPropertyChanged(nameof(this.MoveToPreviousPage)); } }

            /// <summary>
            /// 
            /// </summary>
            public string PinTip { get { return _pinTip; } set { OnPropertyChanging(nameof(this.PinTip)); _pinTip = value; OnPropertyChanged(nameof(this.PinTip)); } }

            /// <summary>
            /// 
            /// </summary>
            public string RemoveGroup { get { return _removeGroup; } set { OnPropertyChanging(nameof(this.RemoveGroup)); _removeGroup = value; OnPropertyChanged(nameof(this.RemoveGroup)); } }

            #endregion Members

            internal __Main()
            {
            }

            internal static int __LoadInternal(LanguagePackage instance, XConfigManager manager, List<string> missing)
            {
                return instance.Main.__LoadInternal(manager, missing);
            }

            internal static void __ResetDefaultInternal(LanguagePackage instance)
            {
                instance.Main.__ResetDefaultInternal();
            }

            internal static void __SaveInternal(LanguagePackage instance, bool defaultField, XConfigManager manager)
            {
                instance.Main.__SaveInternal(defaultField, manager);
            }

            private static string __GetTranslationEntryInternal(XSection section, string name, string defaultValue, List<string> missing, ref int loaded)
            {
                if (section.Properties.TryGetStringValue(name, out string value))
                {
                    loaded++;
                    return value;
                }
                else
                {
                    missing.Add(name);
                    return defaultValue;
                }
            }

            private int __LoadInternal(XConfigManager manager, List<string> missing)
            {
                if (manager.Sections.TryGetValue("Main", out XSection section))
                {
                    int loaded = 0;
                    this.AddNextPage = __GetTranslationEntryInternal(section, "AddNextPage", _addNextPage_d, missing, ref loaded);
                    this.AddPageTip = __GetTranslationEntryInternal(section, "AddPageTip", _addPageTip_d, missing, ref loaded);
                    this.AddPreviousPage = __GetTranslationEntryInternal(section, "AddPreviousPage", _addPreviousPage_d, missing, ref loaded);
                    this.AppInfoButtonTip = __GetTranslationEntryInternal(section, "AppInfoButtonTip", _appInfoButtonTip_d, missing, ref loaded);
                    this.EmptyPageText = __GetTranslationEntryInternal(section, "EmptyPageText", _emptyPageText_d, missing, ref loaded);
                    this.ExitButtonTip = __GetTranslationEntryInternal(section, "ExitButtonTip", _exitButtonTip_d, missing, ref loaded);
                    this.LocateButtonTip = __GetTranslationEntryInternal(section, "LocateButtonTip", _locateButtonTip_d, missing, ref loaded);
                    this.MoveOutGroup = __GetTranslationEntryInternal(section, "MoveOutGroup", _moveOutGroup_d, missing, ref loaded);
                    this.MoveToNextPage = __GetTranslationEntryInternal(section, "MoveToNextPage", _moveToNextPage_d, missing, ref loaded);
                    this.MoveToPreviousPage = __GetTranslationEntryInternal(section, "MoveToPreviousPage", _moveToPreviousPage_d, missing, ref loaded);
                    this.PinTip = __GetTranslationEntryInternal(section, "PinTip", _pinTip_d, missing, ref loaded);
                    this.RemoveGroup = __GetTranslationEntryInternal(section, "RemoveGroup", _removeGroup_d, missing, ref loaded);
                    return loaded;
                }
                else
                {
                    missing.Add("AddNextPage");
                    missing.Add("AddPageTip");
                    missing.Add("AddPreviousPage");
                    missing.Add("AppInfoButtonTip");
                    missing.Add("EmptyPageText");
                    missing.Add("ExitButtonTip");
                    missing.Add("LocateButtonTip");
                    missing.Add("MoveOutGroup");
                    missing.Add("MoveToNextPage");
                    missing.Add("MoveToPreviousPage");
                    missing.Add("PinTip");
                    missing.Add("RemoveGroup");
                    return 0;
                }
            }

            private void __ResetDefaultInternal()
            {
                this.AddNextPage = _addNextPage_d;
                this.AddPageTip = _addPageTip_d;
                this.AddPreviousPage = _addPreviousPage_d;
                this.AppInfoButtonTip = _appInfoButtonTip_d;
                this.EmptyPageText = _emptyPageText_d;
                this.ExitButtonTip = _exitButtonTip_d;
                this.LocateButtonTip = _locateButtonTip_d;
                this.MoveOutGroup = _moveOutGroup_d;
                this.MoveToNextPage = _moveToNextPage_d;
                this.MoveToPreviousPage = _moveToPreviousPage_d;
                this.PinTip = _pinTip_d;
                this.RemoveGroup = _removeGroup_d;
            }

            private void __SaveInternal(bool defaultField, XConfigManager manager)
            {
                XSection section = manager.Sections.Add("Main");
                if (defaultField)
                {
                    section.Properties.AddString("AddNextPage", _addNextPage_d).Comment.SetValue(_addNextPage_c, true);
                    section.Properties.AddString("AddPageTip", _addPageTip_d).Comment.SetValue(_addPageTip_c, true);
                    section.Properties.AddString("AddPreviousPage", _addPreviousPage_d).Comment.SetValue(_addPreviousPage_c, true);
                    section.Properties.AddString("AppInfoButtonTip", _appInfoButtonTip_d).Comment.SetValue(_appInfoButtonTip_c, true);
                    section.Properties.AddString("EmptyPageText", _emptyPageText_d).Comment.SetValue(_emptyPageText_c, true);
                    section.Properties.AddString("ExitButtonTip", _exitButtonTip_d).Comment.SetValue(_exitButtonTip_c, true);
                    section.Properties.AddString("LocateButtonTip", _locateButtonTip_d).Comment.SetValue(_locateButtonTip_c, true);
                    section.Properties.AddString("MoveOutGroup", _moveOutGroup_d).Comment.SetValue(_moveOutGroup_c, true);
                    section.Properties.AddString("MoveToNextPage", _moveToNextPage_d).Comment.SetValue(_moveToNextPage_c, true);
                    section.Properties.AddString("MoveToPreviousPage", _moveToPreviousPage_d).Comment.SetValue(_moveToPreviousPage_c, true);
                    section.Properties.AddString("PinTip", _pinTip_d).Comment.SetValue(_pinTip_c, true);
                    section.Properties.AddString("RemoveGroup", _removeGroup_d).Comment.SetValue(_removeGroup_c, true);
                }
                else
                {
                    section.Properties.AddString("AddNextPage", this.AddNextPage).Comment.SetValue(_addNextPage_c, true);
                    section.Properties.AddString("AddPageTip", this.AddPageTip).Comment.SetValue(_addPageTip_c, true);
                    section.Properties.AddString("AddPreviousPage", this.AddPreviousPage).Comment.SetValue(_addPreviousPage_c, true);
                    section.Properties.AddString("AppInfoButtonTip", this.AppInfoButtonTip).Comment.SetValue(_appInfoButtonTip_c, true);
                    section.Properties.AddString("EmptyPageText", this.EmptyPageText).Comment.SetValue(_emptyPageText_c, true);
                    section.Properties.AddString("ExitButtonTip", this.ExitButtonTip).Comment.SetValue(_exitButtonTip_c, true);
                    section.Properties.AddString("LocateButtonTip", this.LocateButtonTip).Comment.SetValue(_locateButtonTip_c, true);
                    section.Properties.AddString("MoveOutGroup", this.MoveOutGroup).Comment.SetValue(_moveOutGroup_c, true);
                    section.Properties.AddString("MoveToNextPage", this.MoveToNextPage).Comment.SetValue(_moveToNextPage_c, true);
                    section.Properties.AddString("MoveToPreviousPage", this.MoveToPreviousPage).Comment.SetValue(_moveToPreviousPage_c, true);
                    section.Properties.AddString("PinTip", this.PinTip).Comment.SetValue(_pinTip_c, true);
                    section.Properties.AddString("RemoveGroup", this.RemoveGroup).Comment.SetValue(_removeGroup_c, true);
                }
            }

        }

        #endregion Main

        #region Settings

        /// <summary>
        /// Settings section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public sealed class __Settings : INotifyPropertyChanging, INotifyPropertyChanged
        {
            #region Events

            /// <summary>
            /// Property changed event handler.
            /// </summary>
            public event PropertyChangedEventHandler? PropertyChanged;

            /// <summary>
            /// Property changing event handler.
            /// </summary>
            public event PropertyChangingEventHandler? PropertyChanging;

            private void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

            private void OnPropertyChanging(string name)
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
            }

            #endregion Events

            #region Comments

            private const string _settings_c = "Settings title.";
            private const string _shelfSize_c = "Settings title shelf size.";
            private const string _themeStyle_c = "Settings title theme style.";

            #endregion Comments

            #region Default

            private const string _settings_d = "Settings";
            private const string _shelfSize_d = "Shelf size";
            private const string _themeStyle_d = "Theme Style";

            #endregion Default

            #region Members

            private string _settings = _settings_d;
            private string _shelfSize = _shelfSize_d;
            private string _themeStyle = _themeStyle_d;

            /// <summary>
            /// Settings title.
            /// </summary>
            public string Settings { get { return _settings; } set { OnPropertyChanging(nameof(this.Settings)); _settings = value; OnPropertyChanged(nameof(this.Settings)); } }

            /// <summary>
            /// Settings title shelf size.
            /// </summary>
            public string ShelfSize { get { return _shelfSize; } set { OnPropertyChanging(nameof(this.ShelfSize)); _shelfSize = value; OnPropertyChanged(nameof(this.ShelfSize)); } }

            /// <summary>
            /// Settings title theme style.
            /// </summary>
            public string ThemeStyle { get { return _themeStyle; } set { OnPropertyChanging(nameof(this.ThemeStyle)); _themeStyle = value; OnPropertyChanged(nameof(this.ThemeStyle)); } }

            #endregion Members

            internal __Settings()
            {
            }

            internal static int __LoadInternal(LanguagePackage instance, XConfigManager manager, List<string> missing)
            {
                return instance.Settings.__LoadInternal(manager, missing);
            }

            internal static void __ResetDefaultInternal(LanguagePackage instance)
            {
                instance.Settings.__ResetDefaultInternal();
            }

            internal static void __SaveInternal(LanguagePackage instance, bool defaultField, XConfigManager manager)
            {
                instance.Settings.__SaveInternal(defaultField, manager);
            }

            private static string __GetTranslationEntryInternal(XSection section, string name, string defaultValue, List<string> missing, ref int loaded)
            {
                if (section.Properties.TryGetStringValue(name, out string value))
                {
                    loaded++;
                    return value;
                }
                else
                {
                    missing.Add(name);
                    return defaultValue;
                }
            }

            private int __LoadInternal(XConfigManager manager, List<string> missing)
            {
                if (manager.Sections.TryGetValue("Settings", out XSection section))
                {
                    int loaded = 0;
                    this.Settings = __GetTranslationEntryInternal(section, "Settings", _settings_d, missing, ref loaded);
                    this.ShelfSize = __GetTranslationEntryInternal(section, "ShelfSize", _shelfSize_d, missing, ref loaded);
                    this.ThemeStyle = __GetTranslationEntryInternal(section, "ThemeStyle", _themeStyle_d, missing, ref loaded);
                    return loaded;
                }
                else
                {
                    missing.Add("Settings");
                    missing.Add("ShelfSize");
                    missing.Add("ThemeStyle");
                    return 0;
                }
            }

            private void __ResetDefaultInternal()
            {
                this.Settings = _settings_d;
                this.ShelfSize = _shelfSize_d;
                this.ThemeStyle = _themeStyle_d;
            }

            private void __SaveInternal(bool defaultField, XConfigManager manager)
            {
                XSection section = manager.Sections.Add("Settings");
                if (defaultField)
                {
                    section.Properties.AddString("Settings", _settings_d).Comment.SetValue(_settings_c, true);
                    section.Properties.AddString("ShelfSize", _shelfSize_d).Comment.SetValue(_shelfSize_c, true);
                    section.Properties.AddString("ThemeStyle", _themeStyle_d).Comment.SetValue(_themeStyle_c, true);
                }
                else
                {
                    section.Properties.AddString("Settings", this.Settings).Comment.SetValue(_settings_c, true);
                    section.Properties.AddString("ShelfSize", this.ShelfSize).Comment.SetValue(_shelfSize_c, true);
                    section.Properties.AddString("ThemeStyle", this.ThemeStyle).Comment.SetValue(_themeStyle_c, true);
                }
            }

        }

        #endregion Settings

        #region AppItem

        /// <summary>
        /// AppItem section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public sealed class __AppItem : INotifyPropertyChanging, INotifyPropertyChanged
        {
            #region Events

            /// <summary>
            /// Property changed event handler.
            /// </summary>
            public event PropertyChangedEventHandler? PropertyChanged;

            /// <summary>
            /// Property changing event handler.
            /// </summary>
            public event PropertyChangingEventHandler? PropertyChanging;

            private void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

            private void OnPropertyChanging(string name)
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
            }

            #endregion Events

            #region Comments

            private const string _allowsRemove_c = "";
            private const string _arguments_c = "";
            private const string _defaultRunAsAdministrator_c = "";
            private const string _openFileLocation_c = "";
            private const string _remove_c = "";
            private const string _run_c = "";
            private const string _runAsAdministrator_c = "";

            #endregion Comments

            #region Default

            private const string _allowsRemove_d = "Allows";
            private const string _arguments_d = "Arguments";
            private const string _defaultRunAsAdministrator_d = "Default";
            private const string _openFileLocation_d = "Open File Location";
            private const string _remove_d = "Remove";
            private const string _run_d = "Run";
            private const string _runAsAdministrator_d = "Run As Administrator";

            #endregion Default

            #region Members

            private string _allowsRemove = _allowsRemove_d;
            private string _arguments = _arguments_d;
            private string _defaultRunAsAdministrator = _defaultRunAsAdministrator_d;
            private string _openFileLocation = _openFileLocation_d;
            private string _remove = _remove_d;
            private string _run = _run_d;
            private string _runAsAdministrator = _runAsAdministrator_d;

            /// <summary>
            /// 
            /// </summary>
            public string AllowsRemove { get { return _allowsRemove; } set { OnPropertyChanging(nameof(this.AllowsRemove)); _allowsRemove = value; OnPropertyChanged(nameof(this.AllowsRemove)); } }

            /// <summary>
            /// 
            /// </summary>
            public string Arguments { get { return _arguments; } set { OnPropertyChanging(nameof(this.Arguments)); _arguments = value; OnPropertyChanged(nameof(this.Arguments)); } }

            /// <summary>
            /// 
            /// </summary>
            public string DefaultRunAsAdministrator { get { return _defaultRunAsAdministrator; } set { OnPropertyChanging(nameof(this.DefaultRunAsAdministrator)); _defaultRunAsAdministrator = value; OnPropertyChanged(nameof(this.DefaultRunAsAdministrator)); } }

            /// <summary>
            /// 
            /// </summary>
            public string OpenFileLocation { get { return _openFileLocation; } set { OnPropertyChanging(nameof(this.OpenFileLocation)); _openFileLocation = value; OnPropertyChanged(nameof(this.OpenFileLocation)); } }

            /// <summary>
            /// 
            /// </summary>
            public string Remove { get { return _remove; } set { OnPropertyChanging(nameof(this.Remove)); _remove = value; OnPropertyChanged(nameof(this.Remove)); } }

            /// <summary>
            /// 
            /// </summary>
            public string Run { get { return _run; } set { OnPropertyChanging(nameof(this.Run)); _run = value; OnPropertyChanged(nameof(this.Run)); } }

            /// <summary>
            /// 
            /// </summary>
            public string RunAsAdministrator { get { return _runAsAdministrator; } set { OnPropertyChanging(nameof(this.RunAsAdministrator)); _runAsAdministrator = value; OnPropertyChanged(nameof(this.RunAsAdministrator)); } }

            #endregion Members

            internal __AppItem()
            {
            }

            internal static int __LoadInternal(LanguagePackage instance, XConfigManager manager, List<string> missing)
            {
                return instance.AppItem.__LoadInternal(manager, missing);
            }

            internal static void __ResetDefaultInternal(LanguagePackage instance)
            {
                instance.AppItem.__ResetDefaultInternal();
            }

            internal static void __SaveInternal(LanguagePackage instance, bool defaultField, XConfigManager manager)
            {
                instance.AppItem.__SaveInternal(defaultField, manager);
            }

            private static string __GetTranslationEntryInternal(XSection section, string name, string defaultValue, List<string> missing, ref int loaded)
            {
                if (section.Properties.TryGetStringValue(name, out string value))
                {
                    loaded++;
                    return value;
                }
                else
                {
                    missing.Add(name);
                    return defaultValue;
                }
            }

            private int __LoadInternal(XConfigManager manager, List<string> missing)
            {
                if (manager.Sections.TryGetValue("AppItem", out XSection section))
                {
                    int loaded = 0;
                    this.AllowsRemove = __GetTranslationEntryInternal(section, "AllowsRemove", _allowsRemove_d, missing, ref loaded);
                    this.Arguments = __GetTranslationEntryInternal(section, "Arguments", _arguments_d, missing, ref loaded);
                    this.DefaultRunAsAdministrator = __GetTranslationEntryInternal(section, "DefaultRunAsAdministrator", _defaultRunAsAdministrator_d, missing, ref loaded);
                    this.OpenFileLocation = __GetTranslationEntryInternal(section, "OpenFileLocation", _openFileLocation_d, missing, ref loaded);
                    this.Remove = __GetTranslationEntryInternal(section, "Remove", _remove_d, missing, ref loaded);
                    this.Run = __GetTranslationEntryInternal(section, "Run", _run_d, missing, ref loaded);
                    this.RunAsAdministrator = __GetTranslationEntryInternal(section, "RunAsAdministrator", _runAsAdministrator_d, missing, ref loaded);
                    return loaded;
                }
                else
                {
                    missing.Add("AllowsRemove");
                    missing.Add("Arguments");
                    missing.Add("DefaultRunAsAdministrator");
                    missing.Add("OpenFileLocation");
                    missing.Add("Remove");
                    missing.Add("Run");
                    missing.Add("RunAsAdministrator");
                    return 0;
                }
            }

            private void __ResetDefaultInternal()
            {
                this.AllowsRemove = _allowsRemove_d;
                this.Arguments = _arguments_d;
                this.DefaultRunAsAdministrator = _defaultRunAsAdministrator_d;
                this.OpenFileLocation = _openFileLocation_d;
                this.Remove = _remove_d;
                this.Run = _run_d;
                this.RunAsAdministrator = _runAsAdministrator_d;
            }

            private void __SaveInternal(bool defaultField, XConfigManager manager)
            {
                XSection section = manager.Sections.Add("AppItem");
                if (defaultField)
                {
                    section.Properties.AddString("AllowsRemove", _allowsRemove_d).Comment.SetValue(_allowsRemove_c, true);
                    section.Properties.AddString("Arguments", _arguments_d).Comment.SetValue(_arguments_c, true);
                    section.Properties.AddString("DefaultRunAsAdministrator", _defaultRunAsAdministrator_d).Comment.SetValue(_defaultRunAsAdministrator_c, true);
                    section.Properties.AddString("OpenFileLocation", _openFileLocation_d).Comment.SetValue(_openFileLocation_c, true);
                    section.Properties.AddString("Remove", _remove_d).Comment.SetValue(_remove_c, true);
                    section.Properties.AddString("Run", _run_d).Comment.SetValue(_run_c, true);
                    section.Properties.AddString("RunAsAdministrator", _runAsAdministrator_d).Comment.SetValue(_runAsAdministrator_c, true);
                }
                else
                {
                    section.Properties.AddString("AllowsRemove", this.AllowsRemove).Comment.SetValue(_allowsRemove_c, true);
                    section.Properties.AddString("Arguments", this.Arguments).Comment.SetValue(_arguments_c, true);
                    section.Properties.AddString("DefaultRunAsAdministrator", this.DefaultRunAsAdministrator).Comment.SetValue(_defaultRunAsAdministrator_c, true);
                    section.Properties.AddString("OpenFileLocation", this.OpenFileLocation).Comment.SetValue(_openFileLocation_c, true);
                    section.Properties.AddString("Remove", this.Remove).Comment.SetValue(_remove_c, true);
                    section.Properties.AddString("Run", this.Run).Comment.SetValue(_run_c, true);
                    section.Properties.AddString("RunAsAdministrator", this.RunAsAdministrator).Comment.SetValue(_runAsAdministrator_c, true);
                }
            }

        }

        #endregion AppItem

        #region Messages

        /// <summary>
        /// Messages section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public sealed class __Messages : INotifyPropertyChanging, INotifyPropertyChanged
        {
            #region Events

            /// <summary>
            /// Property changed event handler.
            /// </summary>
            public event PropertyChangedEventHandler? PropertyChanged;

            /// <summary>
            /// Property changing event handler.
            /// </summary>
            public event PropertyChangingEventHandler? PropertyChanging;

            private void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

            private void OnPropertyChanging(string name)
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
            }

            #endregion Events

            #region Comments

            private const string _appItemNotExists_c = "";
            private const string _folderNotExists_c = "";
            private const string _removeGroupConfirm_c = "";
            private const string _removeItemConfirm_c = "";
            private const string _tooManyItems_c = "";
            private const string _unknownItem_c = "";

            #endregion Comments

            #region Default

            private const string _appItemNotExists_d = "Item not exists.";
            private const string _folderNotExists_d = "Folder not exists.";
            private const string _removeGroupConfirm_d = "\"{0}\" has {1} items.\r\n\r\nRemove it?";
            private const string _removeItemConfirm_d = "Remove item \"{0}\"?.";
            private const string _tooManyItems_d = "Page has too many items.";
            private const string _unknownItem_d = "Unknown item. IO error.";

            #endregion Default

            #region Members

            private string _appItemNotExists = _appItemNotExists_d;
            private string _folderNotExists = _folderNotExists_d;
            private string _removeGroupConfirm = _removeGroupConfirm_d;
            private string _removeItemConfirm = _removeItemConfirm_d;
            private string _tooManyItems = _tooManyItems_d;
            private string _unknownItem = _unknownItem_d;

            /// <summary>
            /// 
            /// </summary>
            public string AppItemNotExists { get { return _appItemNotExists; } set { OnPropertyChanging(nameof(this.AppItemNotExists)); _appItemNotExists = value; OnPropertyChanged(nameof(this.AppItemNotExists)); } }

            /// <summary>
            /// 
            /// </summary>
            public string FolderNotExists { get { return _folderNotExists; } set { OnPropertyChanging(nameof(this.FolderNotExists)); _folderNotExists = value; OnPropertyChanged(nameof(this.FolderNotExists)); } }

            /// <summary>
            /// 
            /// </summary>
            public string RemoveGroupConfirm { get { return _removeGroupConfirm; } set { OnPropertyChanging(nameof(this.RemoveGroupConfirm)); _removeGroupConfirm = value; OnPropertyChanged(nameof(this.RemoveGroupConfirm)); } }

            /// <summary>
            /// 
            /// </summary>
            public string RemoveItemConfirm { get { return _removeItemConfirm; } set { OnPropertyChanging(nameof(this.RemoveItemConfirm)); _removeItemConfirm = value; OnPropertyChanged(nameof(this.RemoveItemConfirm)); } }

            /// <summary>
            /// 
            /// </summary>
            public string TooManyItems { get { return _tooManyItems; } set { OnPropertyChanging(nameof(this.TooManyItems)); _tooManyItems = value; OnPropertyChanged(nameof(this.TooManyItems)); } }

            /// <summary>
            /// 
            /// </summary>
            public string UnknownItem { get { return _unknownItem; } set { OnPropertyChanging(nameof(this.UnknownItem)); _unknownItem = value; OnPropertyChanged(nameof(this.UnknownItem)); } }

            #endregion Members

            internal __Messages()
            {
            }

            internal static int __LoadInternal(LanguagePackage instance, XConfigManager manager, List<string> missing)
            {
                return instance.Messages.__LoadInternal(manager, missing);
            }

            internal static void __ResetDefaultInternal(LanguagePackage instance)
            {
                instance.Messages.__ResetDefaultInternal();
            }

            internal static void __SaveInternal(LanguagePackage instance, bool defaultField, XConfigManager manager)
            {
                instance.Messages.__SaveInternal(defaultField, manager);
            }

            private static string __GetTranslationEntryInternal(XSection section, string name, string defaultValue, List<string> missing, ref int loaded)
            {
                if (section.Properties.TryGetStringValue(name, out string value))
                {
                    loaded++;
                    return value;
                }
                else
                {
                    missing.Add(name);
                    return defaultValue;
                }
            }

            private int __LoadInternal(XConfigManager manager, List<string> missing)
            {
                if (manager.Sections.TryGetValue("Messages", out XSection section))
                {
                    int loaded = 0;
                    this.AppItemNotExists = __GetTranslationEntryInternal(section, "AppItemNotExists", _appItemNotExists_d, missing, ref loaded);
                    this.FolderNotExists = __GetTranslationEntryInternal(section, "FolderNotExists", _folderNotExists_d, missing, ref loaded);
                    this.RemoveGroupConfirm = __GetTranslationEntryInternal(section, "RemoveGroupConfirm", _removeGroupConfirm_d, missing, ref loaded);
                    this.RemoveItemConfirm = __GetTranslationEntryInternal(section, "RemoveItemConfirm", _removeItemConfirm_d, missing, ref loaded);
                    this.TooManyItems = __GetTranslationEntryInternal(section, "TooManyItems", _tooManyItems_d, missing, ref loaded);
                    this.UnknownItem = __GetTranslationEntryInternal(section, "UnknownItem", _unknownItem_d, missing, ref loaded);
                    return loaded;
                }
                else
                {
                    missing.Add("AppItemNotExists");
                    missing.Add("FolderNotExists");
                    missing.Add("RemoveGroupConfirm");
                    missing.Add("RemoveItemConfirm");
                    missing.Add("TooManyItems");
                    missing.Add("UnknownItem");
                    return 0;
                }
            }

            private void __ResetDefaultInternal()
            {
                this.AppItemNotExists = _appItemNotExists_d;
                this.FolderNotExists = _folderNotExists_d;
                this.RemoveGroupConfirm = _removeGroupConfirm_d;
                this.RemoveItemConfirm = _removeItemConfirm_d;
                this.TooManyItems = _tooManyItems_d;
                this.UnknownItem = _unknownItem_d;
            }

            private void __SaveInternal(bool defaultField, XConfigManager manager)
            {
                XSection section = manager.Sections.Add("Messages");
                if (defaultField)
                {
                    section.Properties.AddString("AppItemNotExists", _appItemNotExists_d).Comment.SetValue(_appItemNotExists_c, true);
                    section.Properties.AddString("FolderNotExists", _folderNotExists_d).Comment.SetValue(_folderNotExists_c, true);
                    section.Properties.AddString("RemoveGroupConfirm", _removeGroupConfirm_d).Comment.SetValue(_removeGroupConfirm_c, true);
                    section.Properties.AddString("RemoveItemConfirm", _removeItemConfirm_d).Comment.SetValue(_removeItemConfirm_c, true);
                    section.Properties.AddString("TooManyItems", _tooManyItems_d).Comment.SetValue(_tooManyItems_c, true);
                    section.Properties.AddString("UnknownItem", _unknownItem_d).Comment.SetValue(_unknownItem_c, true);
                }
                else
                {
                    section.Properties.AddString("AppItemNotExists", this.AppItemNotExists).Comment.SetValue(_appItemNotExists_c, true);
                    section.Properties.AddString("FolderNotExists", this.FolderNotExists).Comment.SetValue(_folderNotExists_c, true);
                    section.Properties.AddString("RemoveGroupConfirm", this.RemoveGroupConfirm).Comment.SetValue(_removeGroupConfirm_c, true);
                    section.Properties.AddString("RemoveItemConfirm", this.RemoveItemConfirm).Comment.SetValue(_removeItemConfirm_c, true);
                    section.Properties.AddString("TooManyItems", this.TooManyItems).Comment.SetValue(_tooManyItems_c, true);
                    section.Properties.AddString("UnknownItem", this.UnknownItem).Comment.SetValue(_unknownItem_c, true);
                }
            }

        }

        #endregion Messages

    }
}
