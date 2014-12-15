using System;
using Android.Support.Wearable.Watchface;
using Android.App;
using Android.Runtime;

namespace WatchFace
{
    public class MyWatchFaceService : CanvasWatchFaceService
    {
        public override CanvasWatchFaceService.Engine OnCreateEngine ()
        {
            return new MyWatchFaceEngine (this);
        }

        public class MyWatchFaceEngine : CanvasWatchFaceService.Engine
        {
            public MyWatchFaceEngine (CanvasWatchFaceService svc) : base(svc)
            {
                Service = svc.JavaCast<Service> ();
            }

            public Service Service { get; private set; }

            Android.Graphics.Paint hoursPaint;

            public override void OnCreate (Android.Views.ISurfaceHolder surfaceHolder)
            {          
                var b = new WatchFaceStyle.Builder (Service)
                    .SetCardPeekMode (WatchFaceStyle.PeekModeShort)
                    .SetBackgroundVisibility (WatchFaceStyle.BackgroundVisibilityInterruptive)
                    .SetShowSystemUiTime (false)
                    .Build ();

                SetWatchFaceStyle(b);

                hoursPaint = new Android.Graphics.Paint ();
                hoursPaint.Color = Android.Graphics.Color.White;
                hoursPaint.TextSize = 48f;
            }

            public override void OnPropertiesChanged (Android.OS.Bundle props)
            {

            }

            public override void OnTimeTick ()
            {
                Invalidate ();
            }

            public override void OnAmbientModeChanged (bool ambient)
            {

            }

            public override void OnVisibilityChanged (bool visible)
            {

            }

            public override void OnDraw (Android.Graphics.Canvas canvas, Android.Graphics.Rect frame)
            {
                var str = DateTime.Now.ToString ("h:mm tt");

                canvas.DrawText (str, (float)(frame.Left + 70), (float)(frame.Top + 80), hoursPaint);
            }
        }
    }
}
