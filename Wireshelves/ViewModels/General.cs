using CommunityToolkit.Mvvm.ComponentModel;

namespace Wireshelves.ViewModels
{
    public partial class General : ObservableObject
    {
        #region Instance

        public static General Instance { get; } = new General();

        #endregion Instance

        [ObservableProperty]
        private bool _modified;
    }
}