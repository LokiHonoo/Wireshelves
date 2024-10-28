using HonooUI.WPF;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Wireshelves
{
    /// <summary>
    ///
    /// </summary>
    public sealed class ThemeStyleToCutomConverter : IValueConverter
    {
        /// <summary>
        ///
        /// </summary>
        public object? DarkValue { get; set; }

        /// <summary>
        ///
        /// </summary>
        public object? OtherValue { get; set; }

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
            return (ThemeStyle)value == ThemeStyle.Dark ? DarkValue : OtherValue;
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