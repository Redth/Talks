using System;
using Android.Content;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Eww.ColorPickerView), typeof(Eww.Droid.ColorPickerViewRendererAndroid))]

namespace Eww.Droid
{
    public class ColorPickerViewRendererAndroid : ViewRenderer<ColorPickerView, JaredRummler.Android.ColorPicker.ColorPickerView>, JaredRummler.Android.ColorPicker.ColorPickerView.IOnColorChangedListener
    {
        Context context;

        public ColorPickerViewRendererAndroid(Context context) : base(context)
        {
            this.context = context;
        }

        JaredRummler.Android.ColorPicker.ColorPickerView nativeColorPickerView;

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
                    nativeColorPickerView = new JaredRummler.Android.ColorPicker.ColorPickerView(context);
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
