using SqlLiteSimple.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace SqlLiteSimple.Converter
{
   public sealed class ListViewConverter : IValueConverter
    {
        public int count = 0;
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            // Products item = (Products)value;
            //IEnumerable<Products> total =new IEnumerable<Products> (parameter) ;
            //var data=  parameter;
            //for(int i=0;i<=total.;i++)
            //ListView listView =
            //    ItemsControl.ItemsControlFromItemContainer(item) as ListView;
            //// Get the index of a ListViewItem
            //int index =
            //    listView.ItemContainerGenerator.IndexFromContainer(item);
            if (count == 100)
            {
                count = 1;
            }
            if (count % 2 == 0)
            {
                count++;
                return  new SolidColorBrush(Colors.White);
            }
            else
            {
                count++;
                return  new SolidColorBrush(Colors.SkyBlue);
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
