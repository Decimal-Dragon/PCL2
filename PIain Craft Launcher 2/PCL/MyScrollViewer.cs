using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020000A3 RID: 163
	public class MyScrollViewer : ScrollViewer
	{
		// Token: 0x060004E1 RID: 1249 RVA: 0x00029CCC File Offset: 0x00027ECC
		public MyScrollViewer()
		{
			base.PreviewMouseWheel += this.MyScrollViewer_PreviewMouseWheel;
			base.ScrollChanged += this.MyScrollViewer_ScrollChanged;
			base.IsVisibleChanged += this.MyScrollViewer_IsVisibleChanged;
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.Load();
			};
			base.PreviewGotKeyboardFocus += this.MyScrollViewer_PreviewGotKeyboardFocus;
			this.DeltaMult = 1.0;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x000049D4 File Offset: 0x00002BD4
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x000049DC File Offset: 0x00002BDC
		public double DeltaMult { get; set; }

		// Token: 0x060004E4 RID: 1252 RVA: 0x00029D4C File Offset: 0x00027F4C
		private void MyScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			if (e.Delta != 0 && base.ActualHeight != 0.0 && base.ScrollableHeight != 0.0)
			{
				Type type = e.Source.GetType();
				if (NewLateBinding.LateGet(base.Content, null, "TemplatedParent", new object[0], null, null, null) != null || ((!typeof(ComboBox).IsAssignableFrom(type) || !((ComboBox)e.Source).IsDropDownOpen) && (!typeof(TextBox).IsAssignableFrom(type) || !((TextBox)e.Source).AcceptsReturn) && !typeof(ComboBoxItem).IsAssignableFrom(type) && !(e.Source is CheckBox)))
				{
					e.Handled = true;
					this.PerformVerticalOffsetDelta((double)(checked(0 - e.Delta)));
					try
					{
						foreach (Border obj in Application.m_HelperComposer)
						{
							ModAnimation.AniStart(ModAnimation.AaOpacity(obj, -1.0, 100, 0, null, false), string.Format("Hide Tooltip {0}", ModBase.GetUuid()), false);
						}
					}
					finally
					{
						List<Border>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000049E5 File Offset: 0x00002BE5
		public void PerformVerticalOffsetDelta(double Delta)
		{
			ModAnimation.AniStart(ModAnimation.AaDouble(delegate(object a0)
			{
				this._Lambda$__7-0(Conversions.ToDouble(a0));
			}, Delta * this.DeltaMult, 300, 0, new ModAnimation.AniEaseOutFluent((ModAnimation.AniEasePower)6), false), "", false);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00029EA4 File Offset: 0x000280A4
		private void MyScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			this.singletonBroadcaster = base.VerticalOffset;
			if (ModMain._ProcessIterator != null && (e.VerticalChange != 0.0 || e.ViewportHeightChange != 0.0))
			{
				ModMain._ProcessIterator.BtnExtraBack.ShowRefresh();
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00004A18 File Offset: 0x00002C18
		private void MyScrollViewer_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			ModMain._ProcessIterator.BtnExtraBack.ShowRefresh();
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00004A29 File Offset: 0x00002C29
		private void Load()
		{
			this.m_RegBroadcaster = (MyScrollBar)base.GetTemplateChild("PART_VerticalScrollBar");
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00004A41 File Offset: 0x00002C41
		private void MyScrollViewer_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			if (e.NewFocus != null && e.NewFocus is MySlider)
			{
				e.Handled = true;
			}
		}

		// Token: 0x0400028D RID: 653
		[CompilerGenerated]
		private double initializerBroadcaster;

		// Token: 0x0400028E RID: 654
		private double singletonBroadcaster;

		// Token: 0x0400028F RID: 655
		public MyScrollBar m_RegBroadcaster;
	}
}
