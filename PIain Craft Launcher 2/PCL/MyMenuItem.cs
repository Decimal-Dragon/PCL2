using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x0200009D RID: 157
	public class MyMenuItem : MenuItem
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x00028728 File Offset: 0x00026928
		public MyMenuItem()
		{
			base.Loaded += this.MyMenuItem_Loaded;
			base.MouseEnter += delegate(object sender, MouseEventArgs e)
			{
				this.RefreshColor();
			};
			base.MouseLeave += delegate(object sender, MouseEventArgs e)
			{
				this.RefreshColor();
			};
			base.IsEnabledChanged += delegate(object sender, DependencyPropertyChangedEventArgs e)
			{
				this.RefreshColor();
			};
			this.managerBroadcaster = ModBase.GetUuid();
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00028790 File Offset: 0x00026990
		private void MyMenuItem_Loaded(object sender, RoutedEventArgs e)
		{
			if (base.Icon != null)
			{
				Path path = (Path)base.GetTemplateChild("Icon");
				if (path != null)
				{
					path.Data = (Geometry)new GeometryConverter().ConvertFromString(Conversions.ToString(base.Icon));
				}
			}
			((ContextMenu)base.Parent).Opacity = Conversions.ToDouble(Operators.AddObject(Operators.DivideObject(ModBase.m_IdentifierRepository.Get("UiLauncherTransparent", null), 1000), 0.4));
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00028820 File Offset: 0x00026A20
		private void RefreshColor()
		{
			string text;
			string text2;
			int time;
			if (!base.IsEnabled)
			{
				text = "ColorBrushTransparent";
				text2 = "ColorBrushGray5";
				time = 200;
			}
			else if (base.IsMouseOver)
			{
				text = "ColorBrush6";
				text2 = "ColorBrush2";
				time = 100;
			}
			else
			{
				text = "ColorBrushTransparent";
				text2 = "ColorBrush1";
				time = 200;
			}
			if (Operators.CompareString(this.m_ModelBroadcaster, text, false) != 0)
			{
				this.m_ModelBroadcaster = text;
				if (base.IsLoaded && ModAnimation.CalcParser() == 0)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaColor(this, Control.BackgroundProperty, text, time, 0, null, false),
						ModAnimation.AaColor(this, Control.ForegroundProperty, text2, time, 0, null, false)
					}, "MyMenuItem Color " + Conversions.ToString(this.managerBroadcaster), false);
					return;
				}
				ModAnimation.AniStop("MyMenuItem Color " + Conversions.ToString(this.managerBroadcaster));
				base.SetResourceReference(Control.BackgroundProperty, text);
				base.SetResourceReference(Control.ForegroundProperty, text2);
			}
		}

		// Token: 0x04000266 RID: 614
		public int managerBroadcaster;

		// Token: 0x04000267 RID: 615
		private string m_ModelBroadcaster;
	}
}
