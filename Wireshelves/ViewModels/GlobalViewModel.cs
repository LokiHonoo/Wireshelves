using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using HonooUI.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using Wireshelves.Models;

namespace Wireshelves.ViewModels
{
    public partial class GlobalViewModel : ObservableObject
    {
        private object? _oldValue1;
        private object? _oldValue2;

        [ObservableProperty]
        private AppItemViewModel? appItemEditing;

        [ObservableProperty]
        private int appLocation;

        [ObservableProperty]
        private double areaHeight;

        [ObservableProperty]
        private double areaWidth;

        [ObservableProperty]
        private int deactivatedWork;

        [ObservableProperty]
        private bool moreControllers;

        [ObservableProperty]
        private bool pin;

        [ObservableProperty]
        private int shelfCol;

        [ObservableProperty]
        private double shelfHeight;

        [ObservableProperty]
        private int shelfIndex;

        [ObservableProperty]
        private int shelfRow;

        [ObservableProperty]
        private double shelfWidth;

        [ObservableProperty]
        private ThemeStyle themeStyle;

        [ObservableProperty]
        private double windowLeft;

        [ObservableProperty]
        private double windowTop;

        public Language Language { get; set; } = new Language("Default", string.Empty);
        public IList<Language> Languages { get; } = [new Language("Default", string.Empty)];
        public Shelves Shelves { get; } = [];
        public Version? Version { get; } = Assembly.GetExecutingAssembly().GetName().Version;

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e != null)
            {
                if (e.PropertyName == nameof(this.ThemeStyle))
                {
                    Theme.SetThemeStyle(this.ThemeStyle);
                }
                else if (e.PropertyName == nameof(this.ShelfCol) || e.PropertyName == nameof(this.ShelfRow))
                {
                    int max = this.ShelfCol * this.ShelfRow;
                    bool overflow = false;
                    foreach (var shelf in this.Shelves)
                    {
                        if (shelf.Appitems.Count > max)
                        {
                            overflow = true;
                            break;
                        }
                    }
                    if (overflow)
                    {
                        DialogManager.Default.Show(Locator.Localization.ResizeMessage,
                                                   null,
                                                   DialogButtons.OKCancel,
                                                   DialogImage.Information,
                                                   new DialogOptions() { DialogSize = new DialogSize(320, double.NaN) },
                                                   (args) =>
                                                   {
                                                       if (args.DialogResult == DialogResult.OK)
                                                       {
                                                           ResetUI();
                                                           ResetAppItems();
                                                       }
                                                       else
                                                       {
                                                           this.ShelfCol = (int)_oldValue1!;
                                                           this.ShelfRow = (int)_oldValue2!;
                                                       }
                                                   },
                                                   null);
                    }
                    else
                    {
                        ResetUI();
                    }
                }
                else if (e.PropertyName == nameof(this.AppLocation))
                {
                    ResetLocation();
                }
                else if (e.PropertyName == nameof(this.ShelfIndex))
                {
                    WeakReferenceMessenger.Default.Send((object)this.ShelfIndex, "ShelfIndexChanged");
                }
            }
        }

        protected override void OnPropertyChanging(PropertyChangingEventArgs e)
        {
            base.OnPropertyChanging(e);
            if (e != null)
            {
                if (e.PropertyName == nameof(this.ShelfCol) || e.PropertyName == nameof(this.ShelfRow))

                {
                    _oldValue1 = this.ShelfCol;
                    _oldValue2 = this.ShelfRow;
                }
            }
        }

        private void ResetAppItems()
        {
            int max = this.ShelfCol * this.ShelfRow;
            var items = new List<AppItemViewModel>();
            foreach (var shelf in this.Shelves)
            {
                if (shelf.Appitems.Count > max)
                {
                    for (int i = shelf.Appitems.Count - 1; i >= max; i--)
                    {
                        items.Add(shelf.Appitems[i]);
                        shelf.Appitems.RemoveAt(i);
                    }
                }
            }
            while (items.Count > 0)
            {
                var shelf = this.Shelves[^1];
                while (shelf.Appitems.Count < max && items.Count > 0)
                {
                    shelf.Appitems.Add(items[0]);
                    items.RemoveAt(0);
                }
                this.Shelves.Add(new ShelfViewModel([]));
            }
        }

        private void ResetLocation()
        {
            double width = this.ShelfWidth + 48;
            double height = this.ShelfHeight + 203;
            var area = SystemParameters.WorkArea;
            switch (this.AppLocation)
            {
                case 0:
                    this.WindowTop = area.Height - height;
                    this.WindowLeft = (area.Width - width) / 2;
                    break;

                case 1:
                    this.WindowTop = area.Height - height;
                    this.WindowLeft = 10;
                    break;

                case 2:
                    this.WindowTop = area.Height - height;
                    this.WindowLeft = area.Width - width - 10;
                    break;

                case 3: break;

                default: this.AppLocation = 0; break;
            }
        }

        private void ResetUI()
        {
            this.ShelfWidth = this.ShelfCol * 95;
            this.ShelfHeight = this.ShelfRow * 85;
            this.AreaWidth = this.ShelfWidth + 40;
            this.AreaHeight = this.ShelfHeight + 20 + 20 + 36;
            ResetLocation();
        }
    }
}