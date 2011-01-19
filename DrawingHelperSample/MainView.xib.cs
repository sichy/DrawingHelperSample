
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using System.Drawing;

namespace DrawingHelperSample
{
	public partial class MainView : UIViewController
	{
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public MainView (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public MainView (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public MainView () : base("MainView", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}
		
		#endregion

		UIImageView backgroundView;
		UIImageView webView;
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			backgroundView = new UIImageView (new RectangleF (0, 0, 320, 480));
			backgroundView.Image = UIImage.FromFile ("Images/Background.png");
			
			webView = new UIImageView (new RectangleF (0, 0, 320, 480));
			webView.Image = UIImage.FromFile ("Images/Web.png");
			
			this.View.AddSubview (backgroundView);
			this.View.AddSubview (webView);
		}
		
		private void EraseTouch (NSSet touches)
		{
			var touch = touches.AnyObject as UITouch;
			
			var helper = new DrawingHelper (webView.Image, webView);
			
			helper.EraseStart ();
			helper.ContextErasePoint (touch.LocationInView (webView), 75);
			helper.EraseEnd ();
		}
		
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
			
			EraseTouch (touches);
		}
		
		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			base.TouchesEnded (touches, evt);
			
			EraseTouch (touches);
		}
		
		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			base.TouchesMoved (touches, evt);
			
			EraseTouch (touches);
		}
	}
}

