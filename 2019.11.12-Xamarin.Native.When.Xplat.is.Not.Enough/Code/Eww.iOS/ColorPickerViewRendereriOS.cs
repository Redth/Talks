using System;
using MSColorPicker;
using UIKit;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Eww.ColorPickerView), typeof(Eww.iOS.ColorPickerViewRendereriOS))]

namespace Eww.iOS
{
    public class ColorPickerViewRendereriOS : ViewRenderer<ColorPickerView, MSColorSelectionView>
    {
        MSColorSelectionView nativeColorPickerView;
        ColorPickerDelegate colorPickerDelegate;

        protected override void OnElementChanged(ElementChangedEventArgs<ColorPickerView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Clean up
            }

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    nativeColorPickerView = new MSColorPicker.MSColorSelectionView();

                    colorPickerDelegate = new ColorPickerDelegate
                    {
                        ColorChangedHandler = c => Element?.RaiseColorChanged(c.ToColor())
                    };

                    nativeColorPickerView.Delegate = colorPickerDelegate;
                    nativeColorPickerView.SetSelectedIndex(1, false);

                    SetNativeControl(nativeColorPickerView);

                    nativeColorPickerView.SetNeedsLayout();
                }
            }
        }

        class ColorPickerDelegate : MSColorViewDelegate
        {
            public Action<UIColor> ColorChangedHandler { get; set; }

            public override void DidChangeColor(MSColorView colorView, UIColor color)
                => ColorChangedHandler?.Invoke(color);
        }
    }
}
