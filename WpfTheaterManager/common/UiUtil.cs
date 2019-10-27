using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfTheaterManager.common
{
    public class IntCollection : ObservableCollection<int> { }

    public class RowColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value.ToString();
            //Debug.Print("strValue" + strValue);
            if (strValue.Equals("exist"))
                return new LinearGradientBrush(Colors.Cornsilk, Colors.Cornsilk, 45);
            if (strValue.Equals("not exist"))
                return new LinearGradientBrush(Colors.Honeydew, Colors.Honeydew, 45);

            return new LinearGradientBrush(Colors.Pink, Colors.White, 45);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
