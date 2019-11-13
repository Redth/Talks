using System;
using Xamarin.Forms;

namespace Eww
{
    public class ColorPickerView : View
    {
        public event EventHandler<Color> ColorChanged;

        public void RaiseColorChanged(Color color)
            => ColorChanged?.Invoke(this, color);
    }
}
