using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020000A2 RID: 162
	public class MyScrollBar : ScrollBar
	{
		// Token: 0x060004D8 RID: 1240 RVA: 0x00029AF4 File Offset: 0x00027CF4
		public MyScrollBar()
		{
			base.IsEnabledChanged += delegate(object sender, DependencyPropertyChangedEventArgs e)
			{
				this.RefreshColor();
			};
			base.GotMouseCapture += delegate(object sender, MouseEventArgs e)
			{
				this.RefreshColor();
			};
			base.LostMouseCapture += delegate(object sender, MouseEventArgs e)
			{
				this.RefreshColor();
			};
			base.MouseEnter += delegate(object sender, MouseEventArgs e)
			{
				this.RefreshColor();
			};
			base.MouseLeave += delegate(object sender, MouseEventArgs e)
			{
				this.RefreshColor();
			};
			base.IsVisibleChanged += delegate(object sender, DependencyPropertyChangedEventArgs e)
			{
				this.RefreshColor();
			};
			this._CreatorBroadcaster = ModBase.GetUuid();
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00029B80 File Offset: 0x00027D80
		private void RefreshColor()
		{
			try
			{
				double num;
				int time;
				string text;
				if (!base.IsVisible)
				{
					num = 0.0;
					time = 20;
					text = "ColorBrush4";
				}
				else if (base.IsMouseCaptureWithin)
				{
					num = 1.0;
					text = "ColorBrush4";
					time = 50;
				}
				else if (base.IsMouseOver)
				{
					num = 0.9;
					text = "ColorBrush3";
					time = 130;
				}
				else
				{
					num = 0.5;
					text = "ColorBrush4";
					time = 180;
				}
				if (base.IsLoaded && ModAnimation.CalcParser() == 0)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaColor(this, Control.ForegroundProperty, text, time, 0, null, false),
						ModAnimation.AaOpacity(this, num - base.Opacity, time, 0, null, false)
					}, "MyScrollBar Color " + Conversions.ToString(this._CreatorBroadcaster), false);
				}
				else
				{
					ModAnimation.AniStop("MyScrollBar Color " + Conversions.ToString(this._CreatorBroadcaster));
					base.SetResourceReference(Control.ForegroundProperty, text);
					base.Opacity = num;
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "滚动条颜色改变出错", ModBase.LogLevel.Debug, "出现错误");
			}
		}

		// Token: 0x0400028C RID: 652
		public int _CreatorBroadcaster;
	}
}
