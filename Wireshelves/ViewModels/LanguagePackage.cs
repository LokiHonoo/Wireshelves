using Honoo.Configuration;
using System.ComponentModel;
using System.IO;

namespace Wireshelves.ViewModels
{
    /// <summary>
    /// Language package class. Binding-notify basic class [.NET 6.0+][With Project field <Nullable>enable</Nullable>].
    /// Using single instance <see cref="Instance"/> to visit.
    /// <br />Install nuget package: <see href="https://www.nuget.org/packages/Honoo.Configuration.ConfigurationManager"/>.
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
        /// AppItem section.
        /// </summary>
        public __AppItem AppItem { get; } = new __AppItem();

        /// <summary>
        /// DialogButton section.
        /// </summary>
        public __DialogButton DialogButton { get; } = new __DialogButton();

        /// <summary>
        /// Informartion section.
        /// </summary>
        public __Informartion Informartion { get; } = new __Informartion();

        /// <summary>
        /// Main section.
        /// </summary>
        public __Main Main { get; } = new __Main();

        /// <summary>
        /// Messages section.
        /// </summary>
        public __Messages Messages { get; } = new __Messages();

        /// <summary>
        /// Settings section.
        /// </summary>
        public __Settings Settings { get; } = new __Settings();

        #endregion Members

        /// <summary>
        /// Initialize new instance of LanguagePackage class.
        /// </summary>
        internal LanguagePackage()
        {
        }

        /// <summary>
        /// Load language file.
        /// </summary>
        /// <param name="fileName">Language file name.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>")]
        public void Load(string fileName)
        {
            using (var manager = new XConfigManager(fileName, true))
            {
                this.Informartion.LoadInternal(manager);
                this.DialogButton.LoadInternal(manager);
                this.Main.LoadInternal(manager);
                this.Settings.LoadInternal(manager);
                this.AppItem.LoadInternal(manager);
                this.Messages.LoadInternal(manager);
            }
        }

        /// <summary>
        /// Load language stream.
        /// </summary>
        /// <param name="stream">Language stream.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>")]
        public void Load(Stream stream)
        {
            using (var manager = new XConfigManager(stream))
            {
                this.Informartion.LoadInternal(manager);
                this.DialogButton.LoadInternal(manager);
                this.Main.LoadInternal(manager);
                this.Settings.LoadInternal(manager);
                this.AppItem.LoadInternal(manager);
                this.Messages.LoadInternal(manager);
            }
        }

        /// <summary>
        /// Reset all properties to default values.
        /// </summary>
        public void ResetDefault()
        {
            this.Informartion.ResetDefaultInternal();
            this.DialogButton.ResetDefaultInternal();
            this.Main.ResetDefaultInternal();
            this.Settings.ResetDefaultInternal();
            this.AppItem.ResetDefaultInternal();
            this.Messages.ResetDefaultInternal();
        }

        /// <summary>
        /// Save to language file.
        /// </summary>
        /// <param name="defaultField">Select current field or default field.</param>
        /// <param name="fileName">Language file name.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>")]
        public void Save(bool defaultField, string fileName)
        {
            using (var manager = new XConfigManager())
            {
                this.Informartion.SaveInternal(defaultField, manager);
                this.DialogButton.SaveInternal(defaultField, manager);
                this.Main.SaveInternal(defaultField, manager);
                this.Settings.SaveInternal(defaultField, manager);
                this.AppItem.SaveInternal(defaultField, manager);
                this.Messages.SaveInternal(defaultField, manager);
                manager.Save(fileName);
            }
        }

        /// <summary>
        /// Save to language stream.
        /// </summary>
        /// <param name="defaultField">Select current field or default field.</param>
        /// <param name="stream">Language stream.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>")]
        public void Save(bool defaultField, Stream stream)
        {
            using (var manager = new XConfigManager())
            {
                this.Informartion.SaveInternal(defaultField, manager);
                this.DialogButton.SaveInternal(defaultField, manager);
                this.Main.SaveInternal(defaultField, manager);
                this.Settings.SaveInternal(defaultField, manager);
                this.AppItem.SaveInternal(defaultField, manager);
                this.Messages.SaveInternal(defaultField, manager);
                manager.Save(stream);
            }
        }

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

            private const string _arguments_c = "";
            private const string _defaultRunAsAdministrator_c = "";
            private const string _openFileLocation_c = "";
            private const string _remove_c = "";
            private const string _runAsAdministrator_c = "";

            #endregion Comments

            #region Default

            private const string _arguments_d = "Arguments";
            private const string _defaultRunAsAdministrator_d = "Default";
            private const string _openFileLocation_d = "Open File Location";
            private const string _remove_d = "Remove";
            private const string _runAsAdministrator_d = "Run As Administrator";

            #endregion Default

            #region Members

            private string _arguments = _arguments_d;
            private string _defaultRunAsAdministrator = _defaultRunAsAdministrator_d;
            private string _openFileLocation = _openFileLocation_d;
            private string _remove = _remove_d;
            private string _runAsAdministrator = _runAsAdministrator_d;

            /// <summary>
            ///
            /// </summary>
            public string Arguments
            { get { return _arguments; } set { OnPropertyChanging(nameof(this.Arguments)); _arguments = value; OnPropertyChanged(nameof(this.Arguments)); } }

            /// <summary>
            ///
            /// </summary>
            public string DefaultRunAsAdministrator
            { get { return _defaultRunAsAdministrator; } set { OnPropertyChanging(nameof(this.DefaultRunAsAdministrator)); _defaultRunAsAdministrator = value; OnPropertyChanged(nameof(this.DefaultRunAsAdministrator)); } }

            /// <summary>
            ///
            /// </summary>
            public string OpenFileLocation
            { get { return _openFileLocation; } set { OnPropertyChanging(nameof(this.OpenFileLocation)); _openFileLocation = value; OnPropertyChanged(nameof(this.OpenFileLocation)); } }

            /// <summary>
            ///
            /// </summary>
            public string Remove
            { get { return _remove; } set { OnPropertyChanging(nameof(this.Remove)); _remove = value; OnPropertyChanged(nameof(this.Remove)); } }

            /// <summary>
            ///
            /// </summary>
            public string RunAsAdministrator
            { get { return _runAsAdministrator; } set { OnPropertyChanging(nameof(this.RunAsAdministrator)); _runAsAdministrator = value; OnPropertyChanged(nameof(this.RunAsAdministrator)); } }

            #endregion Members

            internal __AppItem()
            {
            }

            internal void LoadInternal(XConfigManager manager)
            {
                if (manager.Sections.TryGetValue("AppItem", out XSection section))
                {
                    this.Arguments = section.Properties.GetStringValue("Arguments", _arguments_d);
                    this.DefaultRunAsAdministrator = section.Properties.GetStringValue("DefaultRunAsAdministrator", _defaultRunAsAdministrator_d);
                    this.OpenFileLocation = section.Properties.GetStringValue("OpenFileLocation", _openFileLocation_d);
                    this.Remove = section.Properties.GetStringValue("Remove", _remove_d);
                    this.RunAsAdministrator = section.Properties.GetStringValue("RunAsAdministrator", _runAsAdministrator_d);
                }
            }

            internal void ResetDefaultInternal()
            {
                this.Arguments = _arguments_d;
                this.DefaultRunAsAdministrator = _defaultRunAsAdministrator_d;
                this.OpenFileLocation = _openFileLocation_d;
                this.Remove = _remove_d;
                this.RunAsAdministrator = _runAsAdministrator_d;
            }

            internal void SaveInternal(bool defaultField, XConfigManager manager)
            {
                XSection section = manager.Sections.Add("AppItem");
                if (defaultField)
                {
                    section.Properties.AddString("Arguments", _arguments_d).Comment.SetValue(_arguments_c, true);
                    section.Properties.AddString("DefaultRunAsAdministrator", _defaultRunAsAdministrator_d).Comment.SetValue(_defaultRunAsAdministrator_c, true);
                    section.Properties.AddString("OpenFileLocation", _openFileLocation_d).Comment.SetValue(_openFileLocation_c, true);
                    section.Properties.AddString("Remove", _remove_d).Comment.SetValue(_remove_c, true);
                    section.Properties.AddString("RunAsAdministrator", _runAsAdministrator_d).Comment.SetValue(_runAsAdministrator_c, true);
                }
                else
                {
                    section.Properties.AddString("Arguments", this.Arguments).Comment.SetValue(_arguments_c, true);
                    section.Properties.AddString("DefaultRunAsAdministrator", this.DefaultRunAsAdministrator).Comment.SetValue(_defaultRunAsAdministrator_c, true);
                    section.Properties.AddString("OpenFileLocation", this.OpenFileLocation).Comment.SetValue(_openFileLocation_c, true);
                    section.Properties.AddString("Remove", this.Remove).Comment.SetValue(_remove_c, true);
                    section.Properties.AddString("RunAsAdministrator", this.RunAsAdministrator).Comment.SetValue(_runAsAdministrator_c, true);
                }
            }
        }

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
            public string CancelText
            { get { return _cancelText; } set { OnPropertyChanging(nameof(this.CancelText)); _cancelText = value; OnPropertyChanged(nameof(this.CancelText)); } }

            /// <summary>
            ///
            /// </summary>
            public string NoText
            { get { return _noText; } set { OnPropertyChanging(nameof(this.NoText)); _noText = value; OnPropertyChanged(nameof(this.NoText)); } }

            /// <summary>
            ///
            /// </summary>
            public string OkText
            { get { return _okText; } set { OnPropertyChanging(nameof(this.OkText)); _okText = value; OnPropertyChanged(nameof(this.OkText)); } }

            /// <summary>
            ///
            /// </summary>
            public string YesText
            { get { return _yesText; } set { OnPropertyChanging(nameof(this.YesText)); _yesText = value; OnPropertyChanged(nameof(this.YesText)); } }

            #endregion Members

            internal __DialogButton()
            {
            }

            internal void LoadInternal(XConfigManager manager)
            {
                if (manager.Sections.TryGetValue("DialogButton", out XSection section))
                {
                    this.CancelText = section.Properties.GetStringValue("CancelText", _cancelText_d);
                    this.NoText = section.Properties.GetStringValue("NoText", _noText_d);
                    this.OkText = section.Properties.GetStringValue("OkText", _okText_d);
                    this.YesText = section.Properties.GetStringValue("YesText", _yesText_d);
                }
            }

            internal void ResetDefaultInternal()
            {
                this.CancelText = _cancelText_d;
                this.NoText = _noText_d;
                this.OkText = _okText_d;
                this.YesText = _yesText_d;
            }

            internal void SaveInternal(bool defaultField, XConfigManager manager)
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

        /// <summary>
        /// Informartion section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public sealed class __Informartion : INotifyPropertyChanging, INotifyPropertyChanged
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

            #region Default

            private const string _appName_d = "Wireshelves";
            private const string _appVer_d = "1.x";
            private const string _author_d = "Honoo Language Localisation Converter";
            private const string _langName_d = "en-US";
            private const string _langVer_d = "00";
            private const string _remarks_d = "";
            private const string _url_d = "https://github.com/LokiHonoo/Honoo-Language-Localisation-Converter";

            #endregion Default

            #region Members

            private string _appName = _appName_d;
            private string _appVer = _appVer_d;
            private string _author = _author_d;
            private string _langName = _langName_d;
            private string _langVer = _langVer_d;
            private string _remarks = _remarks_d;
            private string _url = _url_d;

            /// <summary>
            /// Application name.
            /// </summary>
            public string AppName
            { get { return _appName; } set { OnPropertyChanging(nameof(this.AppName)); _appName = value; OnPropertyChanged(nameof(this.AppName)); } }

            /// <summary>
            /// Application version.
            /// </summary>
            public string AppVer
            { get { return _appVer; } set { OnPropertyChanging(nameof(this.AppVer)); _appVer = value; OnPropertyChanged(nameof(this.AppVer)); } }

            /// <summary>
            /// Author name.
            /// </summary>
            public string Author
            { get { return _author; } set { OnPropertyChanging(nameof(this.Author)); _author = value; OnPropertyChanged(nameof(this.Author)); } }

            /// <summary>
            /// Language name as "en-US".
            /// </summary>
            public string LangName
            { get { return _langName; } set { OnPropertyChanging(nameof(this.LangName)); _langName = value; OnPropertyChanged(nameof(this.LangName)); } }

            /// <summary>
            /// Language file version.
            /// </summary>
            public string LangVer
            { get { return _langVer; } set { OnPropertyChanging(nameof(this.LangVer)); _langVer = value; OnPropertyChanged(nameof(this.LangVer)); } }

            /// <summary>
            /// Remarks.
            /// </summary>
            public string Remarks
            { get { return _remarks; } set { OnPropertyChanging(nameof(this.Remarks)); _remarks = value; OnPropertyChanged(nameof(this.Remarks)); } }

            /// <summary>
            /// Author / file related url.
            /// </summary>
            public string Url
            { get { return _url; } set { OnPropertyChanging(nameof(this.Url)); _url = value; OnPropertyChanged(nameof(this.Url)); } }

            #endregion Members

            internal __Informartion()
            {
            }

            internal void LoadInternal(XConfigManager manager)
            {
                this.AppName = manager.Default.Properties.GetStringValue("AppName", _appName_d);
                this.AppVer = manager.Default.Properties.GetStringValue("AppVer", _appVer_d);
                this.LangName = manager.Default.Properties.GetStringValue("LangName", _langName_d);
                this.LangVer = manager.Default.Properties.GetStringValue("LangVer", _langVer_d);
                this.Author = manager.Default.Properties.GetStringValue("Author", _author_d);
                this.Url = manager.Default.Properties.GetStringValue("Url", _url_d);
                this.Remarks = manager.Default.Properties.GetStringValue("Remarks", _remarks_d);
            }

            internal void ResetDefaultInternal()
            {
                this.AppName = _appName_d;
                this.AppVer = _appVer_d;
                this.LangName = _langName_d;
                this.LangVer = _langVer_d;
                this.Author = _author_d;
                this.Url = _url_d;
                this.Remarks = _remarks_d;
            }

            internal void SaveInternal(bool defaultField, XConfigManager manager)
            {
                if (defaultField)
                {
                    manager.Default.Properties.AddString("AppName", _appName_d);
                    manager.Default.Properties.AddString("AppVer", _appVer_d);
                    manager.Default.Properties.AddString("LangName", _langName_d);
                    manager.Default.Properties.AddString("LangVer", _langVer_d);
                    manager.Default.Properties.AddString("Author", _author_d);
                    manager.Default.Properties.AddString("Url", _url_d);
                    manager.Default.Properties.AddString("Remarks", _remarks_d);
                }
                else
                {
                    manager.Default.Properties.AddString("AppName", this.AppName);
                    manager.Default.Properties.AddString("AppVer", this.AppVer);
                    manager.Default.Properties.AddString("LangName", this.LangName);
                    manager.Default.Properties.AddString("LangVer", this.LangVer);
                    manager.Default.Properties.AddString("Author", this.Author);
                    manager.Default.Properties.AddString("Url", this.Url);
                    manager.Default.Properties.AddString("Remarks", this.Remarks);
                }
            }
        }

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

            private const string _addPageTip_c = "";
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

            private const string _addPageTip_d = "Add new page";
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

            private string _addPageTip = _addPageTip_d;
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
            public string AddPageTip
            { get { return _addPageTip; } set { OnPropertyChanging(nameof(this.AddPageTip)); _addPageTip = value; OnPropertyChanged(nameof(this.AddPageTip)); } }

            /// <summary>
            ///
            /// </summary>
            public string AppInfoButtonTip
            { get { return _appInfoButtonTip; } set { OnPropertyChanging(nameof(this.AppInfoButtonTip)); _appInfoButtonTip = value; OnPropertyChanged(nameof(this.AppInfoButtonTip)); } }

            /// <summary>
            ///
            /// </summary>
            public string EmptyPageText
            { get { return _emptyPageText; } set { OnPropertyChanging(nameof(this.EmptyPageText)); _emptyPageText = value; OnPropertyChanged(nameof(this.EmptyPageText)); } }

            /// <summary>
            ///
            /// </summary>
            public string ExitButtonTip
            { get { return _exitButtonTip; } set { OnPropertyChanging(nameof(this.ExitButtonTip)); _exitButtonTip = value; OnPropertyChanged(nameof(this.ExitButtonTip)); } }

            /// <summary>
            ///
            /// </summary>
            public string LocateButtonTip
            { get { return _locateButtonTip; } set { OnPropertyChanging(nameof(this.LocateButtonTip)); _locateButtonTip = value; OnPropertyChanged(nameof(this.LocateButtonTip)); } }

            /// <summary>
            ///
            /// </summary>
            public string MoveOutGroup
            { get { return _moveOutGroup; } set { OnPropertyChanging(nameof(this.MoveOutGroup)); _moveOutGroup = value; OnPropertyChanged(nameof(this.MoveOutGroup)); } }

            /// <summary>
            ///
            /// </summary>
            public string MoveToNextPage
            { get { return _moveToNextPage; } set { OnPropertyChanging(nameof(this.MoveToNextPage)); _moveToNextPage = value; OnPropertyChanged(nameof(this.MoveToNextPage)); } }

            /// <summary>
            ///
            /// </summary>
            public string MoveToPreviousPage
            { get { return _moveToPreviousPage; } set { OnPropertyChanging(nameof(this.MoveToPreviousPage)); _moveToPreviousPage = value; OnPropertyChanged(nameof(this.MoveToPreviousPage)); } }

            /// <summary>
            ///
            /// </summary>
            public string PinTip
            { get { return _pinTip; } set { OnPropertyChanging(nameof(this.PinTip)); _pinTip = value; OnPropertyChanged(nameof(this.PinTip)); } }

            /// <summary>
            ///
            /// </summary>
            public string RemoveGroup
            { get { return _removeGroup; } set { OnPropertyChanging(nameof(this.RemoveGroup)); _removeGroup = value; OnPropertyChanged(nameof(this.RemoveGroup)); } }

            #endregion Members

            internal __Main()
            {
            }

            internal void LoadInternal(XConfigManager manager)
            {
                if (manager.Sections.TryGetValue("Main", out XSection section))
                {
                    this.AddPageTip = section.Properties.GetStringValue("AddPageTip", _addPageTip_d);
                    this.AppInfoButtonTip = section.Properties.GetStringValue("AppInfoButtonTip", _appInfoButtonTip_d);
                    this.EmptyPageText = section.Properties.GetStringValue("EmptyPageText", _emptyPageText_d);
                    this.ExitButtonTip = section.Properties.GetStringValue("ExitButtonTip", _exitButtonTip_d);
                    this.LocateButtonTip = section.Properties.GetStringValue("LocateButtonTip", _locateButtonTip_d);
                    this.MoveOutGroup = section.Properties.GetStringValue("MoveOutGroup", _moveOutGroup_d);
                    this.MoveToNextPage = section.Properties.GetStringValue("MoveToNextPage", _moveToNextPage_d);
                    this.MoveToPreviousPage = section.Properties.GetStringValue("MoveToPreviousPage", _moveToPreviousPage_d);
                    this.PinTip = section.Properties.GetStringValue("PinTip", _pinTip_d);
                    this.RemoveGroup = section.Properties.GetStringValue("RemoveGroup", _removeGroup_d);
                }
            }

            internal void ResetDefaultInternal()
            {
                this.AddPageTip = _addPageTip_d;
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

            internal void SaveInternal(bool defaultField, XConfigManager manager)
            {
                XSection section = manager.Sections.Add("Main");
                if (defaultField)
                {
                    section.Properties.AddString("AddPageTip", _addPageTip_d).Comment.SetValue(_addPageTip_c, true);
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
                    section.Properties.AddString("AddPageTip", this.AddPageTip).Comment.SetValue(_addPageTip_c, true);
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
            public string AppItemNotExists
            { get { return _appItemNotExists; } set { OnPropertyChanging(nameof(this.AppItemNotExists)); _appItemNotExists = value; OnPropertyChanged(nameof(this.AppItemNotExists)); } }

            /// <summary>
            ///
            /// </summary>
            public string FolderNotExists
            { get { return _folderNotExists; } set { OnPropertyChanging(nameof(this.FolderNotExists)); _folderNotExists = value; OnPropertyChanged(nameof(this.FolderNotExists)); } }

            /// <summary>
            ///
            /// </summary>
            public string RemoveGroupConfirm
            { get { return _removeGroupConfirm; } set { OnPropertyChanging(nameof(this.RemoveGroupConfirm)); _removeGroupConfirm = value; OnPropertyChanged(nameof(this.RemoveGroupConfirm)); } }

            /// <summary>
            ///
            /// </summary>
            public string RemoveItemConfirm
            { get { return _removeItemConfirm; } set { OnPropertyChanging(nameof(this.RemoveItemConfirm)); _removeItemConfirm = value; OnPropertyChanged(nameof(this.RemoveItemConfirm)); } }

            /// <summary>
            ///
            /// </summary>
            public string TooManyItems
            { get { return _tooManyItems; } set { OnPropertyChanging(nameof(this.TooManyItems)); _tooManyItems = value; OnPropertyChanged(nameof(this.TooManyItems)); } }

            /// <summary>
            ///
            /// </summary>
            public string UnknownItem
            { get { return _unknownItem; } set { OnPropertyChanging(nameof(this.UnknownItem)); _unknownItem = value; OnPropertyChanged(nameof(this.UnknownItem)); } }

            #endregion Members

            internal __Messages()
            {
            }

            internal void LoadInternal(XConfigManager manager)
            {
                if (manager.Sections.TryGetValue("Messages", out XSection section))
                {
                    this.AppItemNotExists = section.Properties.GetStringValue("AppItemNotExists", _appItemNotExists_d);
                    this.FolderNotExists = section.Properties.GetStringValue("FolderNotExists", _folderNotExists_d);
                    this.RemoveGroupConfirm = section.Properties.GetStringValue("RemoveGroupConfirm", _removeGroupConfirm_d);
                    this.RemoveItemConfirm = section.Properties.GetStringValue("RemoveItemConfirm", _removeItemConfirm_d);
                    this.TooManyItems = section.Properties.GetStringValue("TooManyItems", _tooManyItems_d);
                    this.UnknownItem = section.Properties.GetStringValue("UnknownItem", _unknownItem_d);
                }
            }

            internal void ResetDefaultInternal()
            {
                this.AppItemNotExists = _appItemNotExists_d;
                this.FolderNotExists = _folderNotExists_d;
                this.RemoveGroupConfirm = _removeGroupConfirm_d;
                this.RemoveItemConfirm = _removeItemConfirm_d;
                this.TooManyItems = _tooManyItems_d;
                this.UnknownItem = _unknownItem_d;
            }

            internal void SaveInternal(bool defaultField, XConfigManager manager)
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
            public string Settings
            { get { return _settings; } set { OnPropertyChanging(nameof(this.Settings)); _settings = value; OnPropertyChanged(nameof(this.Settings)); } }

            /// <summary>
            /// Settings title shelf size.
            /// </summary>
            public string ShelfSize
            { get { return _shelfSize; } set { OnPropertyChanging(nameof(this.ShelfSize)); _shelfSize = value; OnPropertyChanged(nameof(this.ShelfSize)); } }

            /// <summary>
            /// Settings title theme style.
            /// </summary>
            public string ThemeStyle
            { get { return _themeStyle; } set { OnPropertyChanging(nameof(this.ThemeStyle)); _themeStyle = value; OnPropertyChanged(nameof(this.ThemeStyle)); } }

            #endregion Members

            internal __Settings()
            {
            }

            internal void LoadInternal(XConfigManager manager)
            {
                if (manager.Sections.TryGetValue("Settings", out XSection section))
                {
                    this.Settings = section.Properties.GetStringValue("Settings", _settings_d);
                    this.ShelfSize = section.Properties.GetStringValue("ShelfSize", _shelfSize_d);
                    this.ThemeStyle = section.Properties.GetStringValue("ThemeStyle", _themeStyle_d);
                }
            }

            internal void ResetDefaultInternal()
            {
                this.Settings = _settings_d;
                this.ShelfSize = _shelfSize_d;
                this.ThemeStyle = _themeStyle_d;
            }

            internal void SaveInternal(bool defaultField, XConfigManager manager)
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
    }
}