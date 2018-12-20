using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using ViewModel.Enums;

namespace ProjektTPA.WPF.Converters
{
    public class TreeTypeEnumToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case TreeTypeEnum.Type:
                    return new SolidColorBrush(Colors.Brown);
                case TreeTypeEnum.Assembly:
                    return new SolidColorBrush(Colors.Black);
                case TreeTypeEnum.Attribute:
                    return new SolidColorBrush(Colors.Aqua);
                case TreeTypeEnum.Constructor:
                    return new SolidColorBrush(Colors.Chocolate);
                case TreeTypeEnum.Field:
                    return new SolidColorBrush(Colors.DarkKhaki);
                case TreeTypeEnum.Method:
                    return new SolidColorBrush(Colors.YellowGreen);
                case TreeTypeEnum.Namespace:
                    return new SolidColorBrush(Colors.Navy);
                case TreeTypeEnum.Parameter:
                    return new SolidColorBrush(Colors.SlateGray);
                case TreeTypeEnum.Property:
                    return new SolidColorBrush(Colors.Olive);

                default: return new SolidColorBrush(Colors.DeepPink);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}