using HonooUI.WPF;
using System;
using System.Globalization;
using System.Windows;
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
        public object DarkValue { get; set; } = DependencyProperty.UnsetValue;

        /// <summary>
        ///
        /// </summary>
        public object LightValue { get; set; } = DependencyProperty.UnsetValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ThemeStyle style)
            {
                return style == ThemeStyle.Dark ? this.DarkValue : this.LightValue;
            }
            return this.DarkValue;
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