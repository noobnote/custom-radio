using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace WpfApp10.Converter
{
    public class CreateCommadParameter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            CommandParameter defaultCommandParameter = default(CommandParameter);
            if (!(values != null && values.Length >= 2)) return defaultCommandParameter;
            if (!(values[0] !=null && values[1] != null)) return defaultCommandParameter;
            if (!(values[0] is ToggleButton && values[1] is Person))return defaultCommandParameter;


            return new CommandParameter() { ToggleButton = (ToggleButton)values[0], Person = (Person)values[1] };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
