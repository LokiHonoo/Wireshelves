using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Wireshelves
{
    /// <summary>
    ///
    /// </summary>
    public sealed class AppItemKindToVisibilityVisibleConverter : IValueConverter
    {
        public static AppItemKindToVisibilityVisibleConverter Instance { get; } = new AppItemKindToVisibilityVisibleConverter();

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (AppItemKind)value == AppItemKind.Application ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}