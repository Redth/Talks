using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.Content;
using MyGarishApp;
using MyGarishApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ColorPickerView), typeof(ColorPickerViewRendererAndroid))]

namespace MyGarishApp.Droid.Renderers
{
    public class ColorPickerViewRendererAndroid : ViewRenderer<ColorPickerView, Com.Jaredrummler.Android.Colorpicker.ColorPickerView>, Com.Jaredrummler.Android.Colorpicker.ColorPickerView.IOnColorChangedListener
    {
        Context context;

        public ColorPickerViewRendererAndroid(Context context) : base(context)
        {
            this.context = context;
        }

        Com.Jaredrummler.Android.Colorpicker.ColorPickerView nativeColorPickerView;

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
                    nativeColorPickerView = new Com.Jaredrummler.Android.Colorpicker.ColorPickerView(context);
                    nativeColorPickerView.SetOnColorChangedListener(this);
                    SetNativeControl(nativeColorPickerView);
                }
            }
        }

        public void OnColorChanged(int p0)
        {
            if (nativeColorPickerView != null)
            {
                var c = new Android.Graphics.Color(p0);

                Element.RaiseColorChanged(c.ToColor());
            }
        }
    }
}
