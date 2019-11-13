using System;
using System.Linq;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(Eww.iOS.DarkModeServiceiOS))]

namespace Eww.iOS
{
    public class DarkModeServiceiOS : IDarkModeService
    {
        public bool IsDarkMode =>
            (GetCurrentViewController()?.TraitCollection?.UserInterfaceStyle ?? UIUserInterfaceStyle.Unspecified) == UIUserInterfaceStyle.Dark;

        internal static UIViewController GetCurrentViewController()
        {
            UIViewController viewController = null;

            var window = UIApplication.SharedApplication.KeyWindow;

            if (window.WindowLevel == UIWindowLevel.Normal)
                viewController = window.RootViewController;

            if (viewController == null)
            {
                window = UIApplication.SharedApplication
                    .Windows
                    .OrderByDescending(w => w.WindowLevel)
                    .FirstOrDefault(w => w.RootViewController != null && w.WindowLevel == UIWindowLevel.Normal);

                if (window == null)
                    throw new InvalidOperationException("Could not find current view controller.");
                else
                    viewController = window.RootViewController;
            }

            while (viewController.PresentedViewController != null)
                viewController = viewController.PresentedViewController;

            return viewController;
        }
    }
}
