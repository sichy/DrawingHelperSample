
using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;

namespace DrawingHelperSample
{
	public class Application
	{
		static void Main (string[] args)
		{
			UIApplication.Main (args);
		}
	}

	// The name AppDelegate is referenced in the MainWindow.xib file.
	public partial class AppDelegate : UIApplicationDelegate
	{
		MainView mainView;
		
		// This method is invoked when the application has loaded its UI and its ready to run
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			if (UIApplication.SharedApplication.RespondsToSelector (new Selector ("setStatusBarHidden: withAnimation:")))
				UIApplication.SharedApplication.SetStatusBarHidden (true, UIStatusBarAnimation.Fade);
			else
				UIApplication.SharedApplication.SetStatusBarHidden (true, true);
			
			// If you have defined a view, add it here:
			// window.AddSubview (navigationController.View);
			mainView = new MainView ();
			window.AddSubview (mainView.View);
			
			window.MakeKeyAndVisible ();
			
			return true;
		}

		// This method is required in iPhoneOS 3.0
		public override void OnActivated (UIApplication application)
		{
		}
	}
}

