using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Wireshelves.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private DateTime _dropOverTimestamp;
        private int _wheelTimestamp;

        [ObservableProperty]
        private ImageSource? titleBg;

        public MainWindowViewModel()
        {
            this.WindowDeactivatedCommand = new RelayCommand<Window?>(WindowDeactivatedCommandExecute);
            this.MouseWheelCommand = new RelayCommand<MouseWheelEventArgs?>(MouseWheelCommandExecute);
            this.PreviousCommand = new RelayCommand(PreviousCommandExecute, PreviousCommandCanExecute);
            this.NextCommand = new RelayCommand(NextCommandExecute, NextCommandCanExecute);
            this.InsertPageBeforeCommand = new RelayCommand(InsertPageBeforeCommandExecute);
            this.InsertPageAfterCommand = new RelayCommand(InsertPageAfterCommandExecute, InsertPageAfterCommandCanExecute);
            this.DropSideEnterCommand = new RelayCommand<DragEventArgs?>(DropSideEnterCommandExecute);
            this.DropLeftOverCommand = new RelayCommand<DragEventArgs?>(DropLeftOverCommandExecute, DropLeftOverCommandCanExecute);
            this.DropRightOverCommand = new RelayCommand<DragEventArgs?>(DropRightOverCommandExecute, DropRightOverCommandCanExecute);
            //
            WeakReferenceMessenger.Default.Register<MainWindowViewModel, object, string>(this, "ShelfIndexChanged", (r, m) =>
             {
                 this.PreviousCommand.NotifyCanExecuteChanged();
                 this.NextCommand.NotifyCanExecuteChanged();
                 this.InsertPageAfterCommand.NotifyCanExecuteChanged();
                 this.DropLeftOverCommand.NotifyCanExecuteChanged();
                 this.DropRightOverCommand.NotifyCanExecuteChanged();
             });
        }

        public ICommand CloseCommand { get; } = new RelayCommand<Window?>((window) =>
        {
            if (window != null)
            {
                SystemCommands.CloseWindow(window);
            }
        });

        public GlobalViewModel GlobalViewModel { get; } = Locator.GlobalViewModel;
        public IRelayCommand DropLeftOverCommand { get; }
        public IRelayCommand DropRightOverCommand { get; }
        public IRelayCommand DropSideEnterCommand { get; }
        public IRelayCommand InsertPageAfterCommand { get; }
        public IRelayCommand InsertPageBeforeCommand { get; }

        public Models.Localization Localization { get; } = Locator.Localization;

        public IRelayCommand MouseWheelCommand { get; }

        public ICommand NavigateUriCommand { get; } = new RelayCommand<Uri?>((uri) =>
        {
            if (uri != null)
            {
                Process.Start(new ProcessStartInfo(uri.ToString()) { UseShellExecute = true });
            }
        });

        public ICommand NavigationViewItemClickedCommand { get; } = new RelayCommand<ShelfViewModel?>(NavigationViewItemClickedCommandExecute);

        public IRelayCommand NextCommand { get; }

        public IRelayCommand PreviousCommand { get; }

        public ICommand WindowDeactivatedCommand { get; }

        private static void NavigationViewItemClickedCommandExecute(ShelfViewModel? shelf)
        {
            if (shelf != null)
            {
                // FrameManager.Default.Navigate(obj);
            }
        }

        private bool DropLeftOverCommandCanExecute(DragEventArgs? e)
        {
            return this.GlobalViewModel.ShelfIndex > 0;
        }

        private void DropLeftOverCommandExecute(DragEventArgs? e)
        {
            if (e != null)
            {
                if ((DateTime.Now - _dropOverTimestamp).TotalMilliseconds > 1000)
                {
                    this.GlobalViewModel.ShelfIndex--;
                    _dropOverTimestamp = DateTime.Now;
                }
            }
        }

        private bool DropRightOverCommandCanExecute(DragEventArgs? e)
        {
            return this.GlobalViewModel.ShelfIndex < this.GlobalViewModel.Shelves.Count - 1;
        }

        private void DropRightOverCommandExecute(DragEventArgs? e)
        {
            if (e != null)
            {
                if ((DateTime.Now - _dropOverTimestamp).TotalMilliseconds > 1000)
                {
                    this.GlobalViewModel.ShelfIndex++;
                    _dropOverTimestamp = DateTime.Now;
                }
            }
        }

        private void DropSideEnterCommandExecute(DragEventArgs? e)
        {
            if (e != null)
            {
                _dropOverTimestamp = DateTime.Now;
            }
        }

        private bool InsertPageAfterCommandCanExecute()
        {
            return this.GlobalViewModel.ShelfIndex < this.GlobalViewModel.Shelves.Count - 1;
        }

        private void InsertPageAfterCommandExecute()
        {
            this.GlobalViewModel.Shelves.Insert(this.GlobalViewModel.ShelfIndex + 1, new ShelfViewModel([]));
            this.GlobalViewModel.ShelfIndex++;
        }

        private void InsertPageBeforeCommandExecute()
        {
            this.GlobalViewModel.Shelves.Insert(this.GlobalViewModel.ShelfIndex, new ShelfViewModel([]));
            this.GlobalViewModel.ShelfIndex--;
        }

        private void MouseWheelCommandExecute(MouseWheelEventArgs? e)
        {
            if (e != null)
            {
                if (e.Timestamp - _wheelTimestamp > 200)
                {
                    if ((e.Delta > 0) && (this.GlobalViewModel.ShelfIndex > 0))
                    {
                        this.GlobalViewModel.ShelfIndex--;
                    }
                    else if ((e.Delta < 0) && (this.GlobalViewModel.ShelfIndex < this.GlobalViewModel.Shelves.Count - 1))
                    {
                        this.GlobalViewModel.ShelfIndex++;
                    }
                    _wheelTimestamp = _wheelTimestamp > int.MaxValue - 2000 ? 0 : e.Timestamp;
                }
            }
        }

        private bool NextCommandCanExecute()
        {
            return this.GlobalViewModel.ShelfIndex < this.GlobalViewModel.Shelves.Count - 1;
        }

        private void NextCommandExecute()
        {
            this.GlobalViewModel.ShelfIndex++;
        }

        private bool PreviousCommandCanExecute()
        {
            return this.GlobalViewModel.ShelfIndex > 0;
        }

        private void PreviousCommandExecute()
        {
            this.GlobalViewModel.ShelfIndex--;
        }

        private void WindowDeactivatedCommandExecute(Window? window)
        {
            if (window != null && !this.GlobalViewModel.Pin)
            {
                window.WindowState = WindowState.Minimized;
            }
        }
    }
}