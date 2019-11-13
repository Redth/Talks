using System;
using Xamarin.Forms;

namespace Eww
{
    public class GradientButton : Button
    {
        Color startColor = Color.Transparent;
        Color endColor = Color.Transparent;

        public Color StartColor
        {
            get => startColor;
            set
            {
                startColor = value;
                OnPropertyChanged(nameof(StartColor));
            }
        }

        public Color EndColor
        {
            get => endColor;
            set
            {
                endColor = value;
                OnPropertyChanged(nameof(EndColor));
            }
        }
    }
}
