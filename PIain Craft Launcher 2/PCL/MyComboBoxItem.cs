using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x0200001B RID: 27
	public class MyComboBoxItem : ComboBoxItem
	{
		// Token: 0x06000098 RID: 152 RVA: 0x0000E988 File Offset: 0x0000CB88
		public MyComboBoxItem()
		{
			base.Unselected += delegate(object sender, RoutedEventArgs e)
			{
				this.RefreshColor();
			};
			base.MouseMove += delegate(object sender, MouseEventArgs e)
			{
				this.RefreshColor();
			};
			base.MouseLeave += delegate(object sender, MouseEventArgs e)
			{
				this.RefreshColor();
			};
			base.Selected += delegate(object sender, RoutedEventArgs e)
			{
				this.RefreshColor();
			};
			base.IsEnabledChanged += delegate(object sender, DependencyPropertyChangedEventArgs e)
			{
				this.RefreshColor();
			};
			base.MouseLeftButtonUp += this.MyComboBoxItem_MouseLeftButtonUp;
			this.m_Map = ModBase.GetUuid();
			base.Style = (Style)base.FindResource("MyComboBoxItem");
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000EA2C File Offset: 0x0000CC2C
		private void RefreshColor()
		{
			string text;
			double num;
			int time;
			if (base.IsSelected)
			{
				text = "ColorBrush6";
				num = 1.0;
				time = 100;
			}
			else if (base.IsMouseOver)
			{
				text = "ColorBrush8";
				num = 1.0;
				time = 100;
			}
			else if (base.IsEnabled)
			{
				text = "ColorBrushTransparent";
				num = 1.0;
				time = 300;
			}
			else
			{
				text = "ColorBrushTransparent";
				num = 0.4;
				time = 300;
			}
			if (Operators.CompareString(this._Context, text, false) != 0 || this.m_Specification != num)
			{
				this._Context = text;
				this.m_Specification = num;
				if (base.IsLoaded && ModAnimation.CalcParser() == 0)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaColor(this, Control.BackgroundProperty, this._Context, time, 0, null, false),
						ModAnimation.AaOpacity(this, this.m_Specification - base.Opacity, time, 0, null, false)
					}, "ComboBoxItem Color " + Conversions.ToString(this.m_Map), false);
					return;
				}
				ModAnimation.AniStop("ComboBoxItem Color " + Conversions.ToString(this.m_Map));
				base.SetResourceReference(Control.BackgroundProperty, this._Context);
				base.Opacity = this.m_Specification;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002725 File Offset: 0x00000925
		public override string ToString()
		{
			return base.Content.ToString();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002732 File Offset: 0x00000932
		public static implicit operator string(MyComboBoxItem Value)
		{
			return Value.Content.ToString();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000273F File Offset: 0x0000093F
		private void MyComboBoxItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			ModBase.Log("[Control] 选择下拉列表项：" + this.ToString(), ModBase.LogLevel.Normal, "出现错误");
		}

		// Token: 0x04000012 RID: 18
		public int m_Map;

		// Token: 0x04000013 RID: 19
		private string _Context;

		// Token: 0x04000014 RID: 20
		private double m_Specification;
	}
}
