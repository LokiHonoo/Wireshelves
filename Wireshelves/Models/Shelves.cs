using System;
using System.Collections.ObjectModel;
using Wireshelves.ViewModels;

namespace Wireshelves.Models
{
    public class Shelves : ObservableCollection<ShelfViewModel>
    {
        protected override void InsertItem(int index, ShelfViewModel item)
        {
            ArgumentNullException.ThrowIfNull(item);
            base.InsertItem(index, item);
            if (index == this.Count - 1)
            {
                item.IsLastPage = true;
                if (index - 1 >= 0)
                {
                    this[index - 1].IsLastPage = false;
                }
            }
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            base.MoveItem(oldIndex, newIndex);
            if (newIndex == this.Count - 1)
            {
                this[newIndex].IsLastPage = true;
                if (newIndex - 1 >= 0)
                {
                    this[newIndex - 1].IsLastPage = false;
                }
            }
        }

        protected override void RemoveItem(int index)
        {
            if (index == this.Count - 1)
            {
                if (index - 1 >= 0)
                {
                    this[index - 1].IsLastPage = true;
                }
            }
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, ShelfViewModel item)
        {
            ArgumentNullException.ThrowIfNull(item);
            base.SetItem(index, item);
            if (index == this.Count - 1)
            {
                this[index].IsLastPage = true;
            }
        }
    }
}