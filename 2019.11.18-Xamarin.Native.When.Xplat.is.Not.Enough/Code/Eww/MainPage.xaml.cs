using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eww
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var darkModeService = DependencyService.Get<IDarkModeService>();

            if (darkModeService?.IsDarkMode ?? false)
            {
                this.BackgroundColor = Color.DarkGray;

                button.StartColor = Color.Aquamarine;
                button.EndColor = Color.Black;
            }
            else
            {
                this.BackgroundColor = Color.White;

                button.StartColor = Color.HotPink;
                button.EndColor = Color.Teal;
            }

            colourPicker.ColorChanged += ColourPicker_ColorChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            colourPicker.ColorChanged -= ColourPicker_ColorChanged;
        }

        private void ColourPicker_ColorChanged(object sender, Color e)
        {
            button.StartColor = e;
        }
    }
}
