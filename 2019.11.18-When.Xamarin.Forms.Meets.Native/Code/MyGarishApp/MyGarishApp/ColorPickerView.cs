using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyGarishApp
{
    public class ColorPickerView : Xamarin.Forms.View
    {

        public event EventHandler<Color> ColorChanged;


        public void RaiseColorChanged(Color color)
            => ColorChanged?.Invoke(this, color);
    }
}
