using HonooUI.WPF;

using Wireshelves.Models;
using Wireshelves.ViewModels;

namespace Wireshelves
{
    public static class Locator
    {
        public static GlobalViewModel GlobalViewModel { get; } = new();

        public static Localization Localization { get; } = new();

        public static DialogOptions Options { get; } = new DialogOptions();
    }
}