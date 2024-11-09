using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Wireshelves.ViewModels
{
    public partial class AppItemGroup : ObservableObject
    {
        #region Members

        [ObservableProperty]
        private string _groupTitle;

        [ObservableProperty]
        private object _icon;

        [ObservableProperty]
        private string _title;

        // internal IInputElement IconData { get; set; } = new XList();
        public ObservableCollection<AppItem> AppItems { get; } = [];

        #endregion Members

        public AppItemGroup(string title)
        {
            this.GroupTitle = title;
            this.Title = title;
            this.Icon = "";
            this.AppItems.CollectionChanged += OnAppItemsChanged;
            this.PropertyChanged += OnPropertyChanged;
        }

        internal bool RemoveAppItem(AppItem item)
        {
            return this.AppItems.Remove(item);
        }

        private void OnAppItemsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
                        foreach (AppItem item in e.NewItems)
                        {
                            item.PropertyChanged += OnItemPropertyChanged;
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                    {
                        foreach (AppItem item in e.OldItems)
                        {
                            item.PropertyChanged -= OnItemPropertyChanged;
                        }
                    }
                    break;

                default: break;
            }
            if (this.AppItems.Count >= 4)
            {
                var grid = new Grid();
                grid.Children.Add(new Image() { Source = this.AppItems[0].Icon.Source, Width = 14, Height = 14, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top });
                grid.Children.Add(new Image() { Source = this.AppItems[1].Icon.Source, Width = 14, Height = 14, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Top });
                grid.Children.Add(new Image() { Source = this.AppItems[2].Icon.Source, Width = 14, Height = 14, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Bottom });
                grid.Children.Add(new Image() { Source = this.AppItems[3].Icon.Source, Width = 14, Height = 14, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Bottom });
                this.Icon = grid;
                this.Title = this.GroupTitle;
            }
            else if (this.AppItems.Count == 3)
            {
                var grid = new Grid();
                grid.Children.Add(new Image() { Source = this.AppItems[0].Icon.Source, Width = 14, Height = 14, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top });
                grid.Children.Add(new Image() { Source = this.AppItems[1].Icon.Source, Width = 14, Height = 14, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Top });
                grid.Children.Add(new Image() { Source = this.AppItems[2].Icon.Source, Width = 14, Height = 14, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Bottom });
                this.Icon = grid;
                this.Title = this.GroupTitle;
            }
            else if (this.AppItems.Count == 2)
            {
                var grid = new Grid();
                grid.Children.Add(new Image() { Source = this.AppItems[0].Icon.Source, Width = 14, Height = 14, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top });
                grid.Children.Add(new Image() { Source = this.AppItems[1].Icon.Source, Width = 14, Height = 14, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Top });
                this.Icon = grid;
                this.Title = this.GroupTitle;
            }
            else if (this.AppItems.Count == 1)
            {
                this.Icon = new Image() { Source = this.AppItems[0].Icon.Source, Width = 32, Height = 32, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };
                this.Title = this.AppItems[0].Title;
            }
            General.Instance.Modified = true;
        }

        private void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Title":
                    if (this.AppItems.Count == 1)
                    {
                        this.Title = this.AppItems[0].Title;
                    }
                    break;

                default: break;
            }
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.GroupTitle):
                    if (this.AppItems.Count > 1)
                    {
                        this.Title = this.GroupTitle;
                    }
                    break;

                default: break;
            }
            General.Instance.Modified = true;
        }
    }
}