using Honoo.Configuration;

namespace Wireshelves.Models
{
    public class Localization
    {
        internal static Localization Template { get; } = new Localization();

        #region Info

        public string Author { get; set; } = "Wireshelves";
        public string Name { get; set; } = "Template en lng";
        public string ShortName { get; set; } = "Default";
        public string Version { get; set; } = Locator.GlobalViewModel.Version == null ? "1.0" : Locator.GlobalViewModel.Version.ToString();

        #endregion Info

        #region Main

        public string EmptyPageTip { get; set; } = "Empty page will be removed at next starts.";

        public string InsertPageAfter { get; set; } = "Insert page after";

        public string InsertPageBefore { get; set; } = "Insert page before";

        public string LackSpace { get; set; } = "Lack of space.";

        public string NewPageTip { get; set; } = "Pin the application to stay for drop file into here.";

        public string NextPage { get; set; } = "Next page";

        public string PreviousPage { get; set; } = "Previous page";

        public string ResizeMessage { get; set; } = "Lack of space. Someone will move to last page.";

        #endregion Main

        #region Settings

        public string AppLocation { get; set; } = "Application Location";
        public string AppLocationCenter { get; set; } = "Desktop Center";
        public string AppLocationLeft { get; set; } = "Desktop Left";
        public string AppLocationMoveable { get; set; } = "Moveable";
        public string AppLocationRight { get; set; } = "Desktop Right";
        public string CloseDelay { get; set; } = "Close delay {0}";
        public string CloseImmediately { get; set; } = "Close immediately";
        public string DeactivatedWork { get; set; } = "When deactivated";
        public string MoreControllers { get; set; } = "More controllers";
        public string NothingToDo { get; set; } = "Nothing to do";
        public string ShelfSize { get; set; } = "Shelf size";
        public string System { get; set; } = "System";
        public string ThemeStyle { get; set; } = "Theme style";
        public string Ui { get; set; } = "UI";

        #endregion Settings

        #region AppItem

        public string Always { get; set; } = "Always";
        public string Confirm { get; set; } = "Confirm";
        public string Delete { get; set; } = "Delete";
        public string FileNotExists { get; set; } = "File not exists.";
        public string FolderNotExists { get; set; } = "Folder not exists.";
        public string OpenFileLocation { get; set; } = "Open file location";
        public string Run { get; set; } = "Run";
        public string RunAsAdministrator { get; set; } = "Run as administrator";
        public string TypeUnsupported { get; set; } = "Type \"{0}\" unsupported.";

        #endregion AppItem

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:不捕获常规异常类型", Justification = "<挂起>")]
        internal bool Reset(string fileName)
        {
            try
            {
                using (var manager = new XConfigManager(fileName))
                {
                    ResetInfo(manager);
                    ResetMain(manager);
                    ResetSettings(manager);
                    ResetAppItem(manager);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal void Reset(Localization other)
        {
            ResetInfo(other);
            ResetMain(other);
            ResetSettings(other);
            ResetAppItem(other);
        }

        internal void Save(string fileName)
        {
            using (var manager = new XConfigManager())
            {
                SaveInfo(manager);
                SaveMain(manager);
                SaveSettings(manager);
                SaveAppItem(manager);
                manager.Save(fileName);
            }
        }

        private void ResetAppItem(XConfigManager manager)
        {
            if (manager.Sections.TryGetValue("AppItem", out XDictionary section))
            {
                this.Always = section.Properties.GetValue("Always", new XString(Template.Always)).GetStringValue();
                this.Confirm = section.Properties.GetValue("Confirm", new XString(Template.Confirm)).GetStringValue();
                this.Delete = section.Properties.GetValue("Delete", new XString(Template.Delete)).GetStringValue();
                this.FileNotExists = section.Properties.GetValue("FileNotExists", new XString(Template.FileNotExists)).GetStringValue();
                this.FolderNotExists = section.Properties.GetValue("FolderNotExists", new XString(Template.FolderNotExists)).GetStringValue();
                this.OpenFileLocation = section.Properties.GetValue("OpenFileLocation", new XString(Template.OpenFileLocation)).GetStringValue();
                this.Run = section.Properties.GetValue("Run", new XString(Template.Run)).GetStringValue();
                this.RunAsAdministrator = section.Properties.GetValue("RunAsAdministrator", new XString(Template.RunAsAdministrator)).GetStringValue();
                this.TypeUnsupported = section.Properties.GetValue("TypeUnsupported", new XString(Template.TypeUnsupported)).GetStringValue();
            }
        }

        private void ResetAppItem(Localization other)
        {
            this.Always = other.Always;
            this.Confirm = other.Confirm;
            this.Delete = other.Delete;
            this.FileNotExists = other.FileNotExists;
            this.FolderNotExists = other.FolderNotExists;
            this.OpenFileLocation = other.OpenFileLocation;
            this.Run = other.Run;
            this.RunAsAdministrator = other.RunAsAdministrator;
            this.TypeUnsupported = other.TypeUnsupported;
        }

        private void ResetInfo(XConfigManager manager)
        {
            this.Name = manager.Default.Properties.GetValue("Name", new XString("Lng Name")).GetStringValue();
            this.ShortName = manager.Default.Properties.GetValue("ShortName", new XString("unknow")).GetStringValue();
            this.Author = manager.Default.Properties.GetValue("Author", new XString("unknow")).GetStringValue();
            this.Version = manager.Default.Properties.GetValue("Version", new XString("unknow")).GetStringValue();
        }

        private void ResetInfo(Localization other)
        {
            this.Name = other.Name;
            this.ShortName = other.ShortName;
            this.Author = other.Author;
            this.Version = other.Version;
        }

        private void ResetMain(XConfigManager manager)
        {
            if (manager.Sections.TryGetValue("Main", out XDictionary section))
            {
                this.EmptyPageTip = section.Properties.GetValue("EmptyPageTip", new XString(Template.EmptyPageTip)).GetStringValue();
                this.InsertPageAfter = section.Properties.GetValue("InsertPageAfter", new XString(Template.InsertPageAfter)).GetStringValue();
                this.InsertPageBefore = section.Properties.GetValue("InsertPageBefore", new XString(Template.InsertPageBefore)).GetStringValue();
                this.LackSpace = section.Properties.GetValue("LackSpace", new XString(Template.LackSpace)).GetStringValue();
                this.NewPageTip = section.Properties.GetValue("NewPageTip", new XString(Template.NewPageTip)).GetStringValue();
                this.NextPage = section.Properties.GetValue("NextPage", new XString(Template.NextPage)).GetStringValue();
                this.PreviousPage = section.Properties.GetValue("PreviousPage", new XString(Template.PreviousPage)).GetStringValue();
                this.ResizeMessage = section.Properties.GetValue("ResizeMessage", new XString(Template.ResizeMessage)).GetStringValue();
            }
        }

        private void ResetMain(Localization other)
        {
            this.EmptyPageTip = other.EmptyPageTip;
            this.InsertPageAfter = other.InsertPageAfter;
            this.InsertPageBefore = other.InsertPageBefore;
            this.LackSpace = other.LackSpace;
            this.NewPageTip = other.NewPageTip;
            this.NextPage = other.NextPage;
            this.PreviousPage = other.PreviousPage;
            this.ResizeMessage = other.ResizeMessage;
        }

        private void ResetSettings(XConfigManager manager)
        {
            if (manager.Sections.TryGetValue("Settings", out XDictionary section))
            {
                this.AppLocation = section.Properties.GetValue("AppLocation", new XString(Template.AppLocation)).GetStringValue();
                this.AppLocationCenter = section.Properties.GetValue("AppLocationCenter", new XString(Template.AppLocationCenter)).GetStringValue();
                this.AppLocationLeft = section.Properties.GetValue("AppLocationLeft", new XString(Template.AppLocationLeft)).GetStringValue();
                this.AppLocationMoveable = section.Properties.GetValue("AppLocationMoveable", new XString(Template.AppLocationMoveable)).GetStringValue();
                this.AppLocationRight = section.Properties.GetValue("AppLocationRight", new XString(Template.AppLocationRight)).GetStringValue();
                this.CloseDelay = section.Properties.GetValue("CloseDelay", new XString(Template.CloseDelay)).GetStringValue();
                this.CloseImmediately = section.Properties.GetValue("CloseImmediately", new XString(Template.CloseImmediately)).GetStringValue();
                this.DeactivatedWork = section.Properties.GetValue("DeactivatedWork", new XString(Template.DeactivatedWork)).GetStringValue();
                this.MoreControllers = section.Properties.GetValue("MoreControllers", new XString(Template.MoreControllers)).GetStringValue();
                this.NothingToDo = section.Properties.GetValue("NothingToDo", new XString(Template.NothingToDo)).GetStringValue();
                this.ShelfSize = section.Properties.GetValue("ShelfSize", new XString(Template.ShelfSize)).GetStringValue();
                this.System = section.Properties.GetValue("System", new XString(Template.System)).GetStringValue();
                this.ThemeStyle = section.Properties.GetValue("ThemeStyle", new XString(Template.ThemeStyle)).GetStringValue();
                this.Ui = section.Properties.GetValue("Ui", new XString(Template.Ui)).GetStringValue();
            }
        }

        private void ResetSettings(Localization other)
        {
            this.AppLocation = other.AppLocation;
            this.AppLocationCenter = other.AppLocationCenter;
            this.AppLocationLeft = other.AppLocationLeft;
            this.AppLocationMoveable = other.AppLocationMoveable;
            this.AppLocationRight = other.AppLocationRight;
            this.CloseDelay = other.CloseDelay;
            this.CloseImmediately = other.CloseImmediately;
            this.DeactivatedWork = other.DeactivatedWork;
            this.MoreControllers = other.MoreControllers;
            this.NothingToDo = other.NothingToDo;
            this.ShelfSize = other.ShelfSize;
            this.System = other.System;
            this.ThemeStyle = other.ThemeStyle;
            this.Ui = other.Ui;
        }

        private void SaveAppItem(XConfigManager manager)
        {
            XDictionary section = manager.Sections.GetOrAdd("AppItem");
            section.Properties.AddOrUpdate("Always", new XString(this.Always));
            section.Properties.AddOrUpdate("Confirm", new XString(this.Confirm));
            section.Properties.AddOrUpdate("Delete", new XString(this.Delete));
            section.Properties.AddOrUpdate("FileNotExists", new XString(this.FileNotExists));
            section.Properties.AddOrUpdate("FolderNotExists", new XString(this.FolderNotExists));
            section.Properties.AddOrUpdate("OpenFileLocation", new XString(this.OpenFileLocation));
            section.Properties.AddOrUpdate("Run", new XString(this.Run));
            section.Properties.AddOrUpdate("RunAsAdministrator", new XString(this.RunAsAdministrator));
            section.Properties.AddOrUpdate("TypeUnsupported", new XString(this.TypeUnsupported));
        }

        private void SaveInfo(XConfigManager manager)
        {
            manager.Default.Properties.AddOrUpdate("Name", new XString(this.Name)).Comment.SetValue("Move the \"*.lng\" file to \\{application directory}\\Languages\\*.lng .");
            manager.Default.Properties.AddOrUpdate("ShortName", new XString(this.ShortName)).Comment.SetValue("Display \"ShortName\" for application selecttion."); ;
            manager.Default.Properties.AddOrUpdate("Author", new XString(this.Author));
            manager.Default.Properties.AddOrUpdate("Version", new XString(this.Version));
        }

        private void SaveMain(XConfigManager manager)
        {
            XDictionary section = manager.Sections.GetOrAdd("Main");
            section.Properties.AddOrUpdate("EmptyPageTip", new XString(this.EmptyPageTip));
            section.Properties.AddOrUpdate("InsertPageAfter", new XString(this.InsertPageAfter));
            section.Properties.AddOrUpdate("InsertPageBefore", new XString(this.InsertPageBefore));
            section.Properties.AddOrUpdate("LackSpace", new XString(this.LackSpace));
            section.Properties.AddOrUpdate("NewPageTip", new XString(this.NewPageTip));
            section.Properties.AddOrUpdate("NextPage", new XString(this.NextPage));
            section.Properties.AddOrUpdate("PreviousPage", new XString(this.PreviousPage));
            section.Properties.AddOrUpdate("ResizeMessage", new XString(this.ResizeMessage));
        }

        private void SaveSettings(XConfigManager manager)
        {
            XDictionary section = manager.Sections.GetOrAdd("Settings");
            section.Properties.AddOrUpdate("AppLocation", new XString(this.AppLocation));
            section.Properties.AddOrUpdate("AppLocationCenter", new XString(this.AppLocationCenter));
            section.Properties.AddOrUpdate("AppLocationLeft", new XString(this.AppLocationLeft));
            section.Properties.AddOrUpdate("AppLocationMoveable", new XString(this.AppLocationMoveable));
            section.Properties.AddOrUpdate("AppLocationRight", new XString(this.AppLocationRight));
            section.Properties.AddOrUpdate("CloseDelay", new XString(this.CloseDelay));
            section.Properties.AddOrUpdate("CloseImmediately", new XString(this.CloseImmediately));
            section.Properties.AddOrUpdate("DeactivatedWork", new XString(this.DeactivatedWork));
            section.Properties.AddOrUpdate("MoreControllers", new XString(this.MoreControllers));
            section.Properties.AddOrUpdate("NothingToDo", new XString(this.NothingToDo));
            section.Properties.AddOrUpdate("ShelfSize", new XString(this.ShelfSize));
            section.Properties.AddOrUpdate("System", new XString(this.System));
            section.Properties.AddOrUpdate("ThemeStyle", new XString(this.ThemeStyle));
            section.Properties.AddOrUpdate("Ui", new XString(this.Ui));
        }
    }
}