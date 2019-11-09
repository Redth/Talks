using System;
using System.ComponentModel;
using System.Threading.Tasks;
using MSColorPicker;
using MyGarishApp;
using MyGarishApp.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ColorPickerView), typeof(ColorPickerViewRendereriOS))]

namespace MyGarishApp.iOS.Renderers
{
    public class ColorPickerViewRendereriOS : ViewRenderer<ColorPickerView, MSColorPicker.MSColorView>, MSColorPicker.IMSColorViewDelegate
    {
        MSColorPicker.MSColorView nativeColorPickerView;

        protected override void OnElementChanged(ElementChangedEventArgs<ColorPickerView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {

            }

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    nativeColorPickerView = new MSColorPicker.MSColorView();

                    nativeColorPickerView.Delegate = this;
                    
                    SetNativeControl(nativeColorPickerView);
                }
            }
        }

        public void DidChangeColor(MSColorPicker.MSColorView colorView, UIColor color)
        {
            if (nativeColorPickerView != null)                
                Element.RaiseColorChanged(color.ToColor());
        }
    }
}
