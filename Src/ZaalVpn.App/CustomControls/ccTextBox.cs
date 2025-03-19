using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ZaalVpn.App.CustomControls
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility visibility && visibility == Visibility.Visible;
        }
    }
    public class ccTextBox : TextBox
    {
        static ccTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ccTextBox), new FrameworkPropertyMetadata(typeof(ccTextBox)));
        }

        public ccTextBox()
        {
            GotFocus += (s, e) => UpdatePlaceholderVisibility();
            LostFocus += (s, e) => UpdatePlaceholderVisibility();
            TextChanged += (s, e) => UpdatePlaceholderVisibility();
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(ccTextBox), new PropertyMetadata(string.Empty));

        private static readonly DependencyPropertyKey IsPlaceholderVisiblePropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(IsPlaceholderVisible), typeof(bool), typeof(ccTextBox), new PropertyMetadata(true));

        public static readonly DependencyProperty IsPlaceholderVisibleProperty = IsPlaceholderVisiblePropertyKey.DependencyProperty;

        // CLR Property for Placeholder
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        // CLR Property for Placeholder Visibility
        public bool IsPlaceholderVisible
        {
            get => (bool)GetValue(IsPlaceholderVisibleProperty);
            private set => SetValue(IsPlaceholderVisiblePropertyKey, value);
        }

        private void UpdatePlaceholderVisibility()
        {
            IsPlaceholderVisible = string.IsNullOrEmpty(Text);
        }
    }
}