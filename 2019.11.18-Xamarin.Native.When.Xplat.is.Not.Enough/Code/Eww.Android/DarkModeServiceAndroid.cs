using System;

[assembly: Xamarin.Forms.Dependency(typeof(Eww.Droid.DarkModeServiceAndroid))]

namespace Eww.Droid
{
    public class DarkModeServiceAndroid : IDarkModeService
    {
        public bool IsDarkMode
            => (Android.App.Application.Context.Resources.Configuration.UiMode
                & Android.Content.Res.UiMode.NightMask) == Android.Content.Res.UiMode.NightMask;
    }
}
