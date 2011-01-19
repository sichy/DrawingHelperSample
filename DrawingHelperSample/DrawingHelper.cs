using System;
using System.Drawing;

using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;

namespace DrawingHelperSample
{
	public class DrawingHelper
	{
		CGContext ctx;
		UIImage drawingBoard;
		UIImageView drawingView;
		
		public DrawingHelper (UIImage board, UIImageView view)
		{
			drawingView = view;
			drawingBoard = board;
		}
		
		public void ContextEraseLine (PointF startPoint, PointF endPoint, int thickness)
		{
			if (ctx == null)
			{
				// call EraseStart first
				return;
			}
			
			int x, cx, deltax, xstep, y, cy, deltay, ystep, error, st,
			dupe;
			int x0, y0, x1, y1, temp;
			
			x0 = (int)startPoint.X;
			y0 = (int)startPoint.Y;
			x1 = (int)endPoint.X;
			y1 = (int)endPoint.Y;
			
			// find largest delta for pixel steps
			st = Math.Abs (y1 - y0) - Math.Abs (x1 - x0);
			
			// if deltay > deltax then swap x,y
			if (st > 0) 
			{
				temp = x0;
				x0 = y0;
				y0 = temp;
				// swap(x0, y0);
				temp = x1;
				x1 = y1;
				y1 = temp;
				// swap(x1, y1);
			}
			
			deltax = Math.Abs (x1 - x0);
			deltay = Math.Abs (y1 - y0);
			error = deltax / 2;
			y = y0;
			
			if (x0 > x1) 
			{
				xstep = -1;
			} 
			else 
			{
				xstep = 1;
			}
			
			if (y0 > y1) 
			{
				ystep = -1;
			} 
			else 
			{
				ystep = 1;
			}
			
			for (x = x0; x != x1 + xstep; x += xstep) 
			{
				cx = x;
				cy = y;
				// copy of x, copy of y
				// if x,y swapped above, swap them back now
				if (st > 0) 
				{
					temp = cx;
					cx ^= cy;
					cy ^= temp;
				}
				
				ctx.ClearRect (new RectangleF (cx, cy, thickness, thickness));
				
				error -= deltay;
				
				// converge toward end of line
				if (error < 0) 
				{
					// not done yet
					y += ystep;
					error += deltax;
				}
			}
		}
		
		public void ContextErasePoint (PointF point, int thickness)
		{
			if (drawingBoard == null)
				return;
			
			RectangleF circleRect = new RectangleF (point.X - thickness / 2, 
				drawingBoard.Size.Height - point.Y - thickness / 2, thickness, thickness);
			
			ctx.AddArc (point.X, drawingBoard.Size.Height - point.Y, thickness / 2, 0f, 2f * (float)Math.PI, false);
			ctx.Clip();
			
			ctx.ClearRect (circleRect);
		}
		
		public void EraseStart ()
		{
			if (drawingBoard == null)
				return;
			
			UIGraphics.BeginImageContext (drawingBoard.Size);
			
			// erase lines
			ctx = UIGraphics.GetCurrentContext ();
			
			// Convert co-ordinate system to Cocoa's (origin in UL, not LL)
			ctx.TranslateCTM (0, drawingBoard.Size.Height);
			ctx.ConcatCTM (CGAffineTransform.MakeScale (1, -1));
			
			ctx.DrawImage (new RectangleF (0, 0, drawingBoard.Size.Width, drawingBoard.Size.Height),
                        drawingBoard.CGImage);
		}
		
		public void EraseEnd ()
		{
			drawingBoard = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			
			drawingView.Image = drawingBoard;
		}

	}
}

