using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Wireshelves.ViewModels
{
    public partial class AppItemPage : ObservableObject
    {
        #region Members

        public ObservableCollection<AppItemGroup> AppItemGroups { get; } = [];
        public string Id { get; }

        #endregion Members

        public AppItemPage(string id)
        {
            this.Id = id;
            this.AppItemGroups.CollectionChanged += OnAppItemGroupsChanged;
        }

        internal bool RemoveAppItem(AppItem item)
        {
            foreach (var group in this.AppItemGroups)
            {
                if (group.RemoveAppItem(item))
                {
                    if (group.AppItems.Count == 0)
                    {
                        this.AppItemGroups.Remove(group);
                    }
                    return true;
                }
            }
            return false;
        }

        private void OnAppItemGroupsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            General.Instance.Modified = true;
        }
    }
}