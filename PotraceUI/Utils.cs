using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PotraceUI
{
    static public class BindingHelper
    {

        static public void Binding(this TextBox control, object obj, string property)
        {
            var _binding = new Binding
            {
                Source = obj,
                Path = new PropertyPath(property)
            };
            control.SetBinding(TextBox.TextProperty, _binding);
        }

        static public void Binding(this Slider control, object obj, string property)
        {
            var _binding = new Binding
            {
                Source = obj,
                Path = new PropertyPath(property)
            };
            control.SetBinding(Slider.ValueProperty, _binding);
        }

        static public void Binding(this CheckBox control, object obj, string property)
        {
            var _binding = new Binding
            {
                Source = obj,
                Path = new PropertyPath(property)
            };
            control.SetBinding(CheckBox.IsCheckedProperty, _binding);
        }

        static public void Binding(this ComboBox control, object obj, string property)
        {
            var _binding = new Binding
            {
                Source = obj,
                Path = new PropertyPath(property)
            };
            control.SetBinding(ComboBox.SelectedValueProperty, _binding);
        }

        static public void BindingList<T>(this ComboBox control, IEnumerable<T> list, string display, string selected)
        {
            control.ItemsSource = list;
            control.DisplayMemberPath = display;
            control.SelectedValuePath = selected;
        }
    }

    static class ConvertHelper
    {
        static public void String2IntCheck(string value, ref int number, int? min = null, int? max = null)
        {
            if (int.TryParse(value, out int _i))
            {
                if (min != null && _i < min) number = (int)min;
                else if (max != null && _i > max) number = (int)max;
                else number = _i;
            }
        }

        static public void String2DoubleCheck(string value, ref double number)
        {
            if (double.TryParse(value, out double _i))
            {
                number = _i;
            }
        }
    }

    static class ImageHelper
    {
        static public BitmapImage ColorBitmap(string path)
        {
            var _img = new BitmapImage();
            _img.BeginInit();
            _img.StreamSource = System.IO.File.OpenRead(path);
            _img.EndInit();
            return _img;
        }
    }
}