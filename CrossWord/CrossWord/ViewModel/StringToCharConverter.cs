using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CrossWord.ViewModel
{
    public class StringToCharConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Debug.WriteLine("Convert + value"+ value.ToString()[0]);
            //Debug.WriteLine("value " + value);
            //Debug.WriteLine("value.ToString()[0] " + value.ToString()[0]);
            if (value.ToString()[0] == char.MinValue)
            {
                return "";
            }
            else
            {
                return value.ToString()[0];
            }

            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //Debug.WriteLine("ConvertBack + value.ToString() "+ value.ToString());
            //Debug.WriteLine("value.ToString()[0] " + value.ToString()[0]);
            if (value.ToString().Length == 0)
            {
                //if (value.ToString()[0] == char.MinValue)
                //{

                //}
                return char.MinValue;
            }
            else
            {
                return value.ToString();
            }


            
        }

    }
}
