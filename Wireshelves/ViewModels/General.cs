using CommunityToolkit.Mvvm.ComponentModel;
using System.Reflection;

namespace Wireshelves.ViewModels
{
    public partial class General : ObservableObject
    {
        #region Instance

        public static General Instance { get; } = new General();

        #endregion Instance

        #region Members

        [ObservableProperty]
        private bool _modified;

        public string Version { get; }

        #endregion Members

        public General()
        {
            this.Version = Assembly.GetExecutingAssembly().GetName().Version!.ToString(3);
        }
    }
}