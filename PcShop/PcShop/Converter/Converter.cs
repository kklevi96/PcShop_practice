using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace PcShop.Converter
{
    public class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value.ToString() == "MOTHERBOARD")
            {
                return Brushes.LightPink;
            }
            else if (value.ToString() == "CPU")
            {
                return Brushes.LightGreen;
            }
            else if (value.ToString() == "STORAGE")
            {
                return Brushes.LightBlue;
            }
            else if (value.ToString() == "RAM")
            {
                return Brushes.Yellow;
            }
            else
            {
                return Brushes.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
