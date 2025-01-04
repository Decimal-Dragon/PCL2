using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x0200004B RID: 75
	public class MyPageLeft : Grid
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00002FCB File Offset: 0x000011CB
		public MyPageLeft()
		{
			this.m_Manager = ModBase.GetUuid();
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00002FDF File Offset: 0x000011DF
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00002FF1 File Offset: 0x000011F1
		public FrameworkElement AnimatedControl
		{
			get
			{
				return (FrameworkElement)base.GetValue(MyPageLeft.m_Wrapper);
			}
			set
			{
				base.SetValue(MyPageLeft.m_Wrapper, value);
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00014138 File Offset: 0x00012338
		public void TriggerShowAnimation()
		{
			if (this.AnimatedControl == null)
			{
				if (!(base.RenderTransform is ScaleTransform))
				{
					base.RenderTransform = new ScaleTransform(0.96, 0.96);
					base.RenderTransformOrigin = new Point(0.5, 0.5);
				}
				base.Opacity = 0.0;
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaScaleTransform(this, 1.0 - ((ScaleTransform)base.RenderTransform).ScaleX, 400, 0, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false),
					ModAnimation.AaOpacity(this, 1.0, 100, 0, null, false)
				}, "PageLeft PageChange " + Conversions.ToString(this.m_Manager), false);
				return;
			}
			List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
			int num = 0;
			int num2 = 0;
			checked
			{
				try
				{
					List<FrameworkElement>.Enumerator enumerator = this.GetAllAnimControls(true).GetEnumerator();
					while (enumerator.MoveNext())
					{
						MyPageLeft._Closure$__7-0 CS$<>8__locals1 = new MyPageLeft._Closure$__7-0(CS$<>8__locals1);
						CS$<>8__locals1.$VB$Me = this;
						CS$<>8__locals1.$VB$Local_Element = enumerator.Current;
						if (CS$<>8__locals1.$VB$Local_Element.Visibility == Visibility.Collapsed)
						{
							CS$<>8__locals1.$VB$Local_Element.Opacity = 1.0;
							CS$<>8__locals1.$VB$Local_Element.RenderTransform = new TranslateTransform(0.0, 0.0);
							if (CS$<>8__locals1.$VB$Local_Element is MyListItem)
							{
								((MyListItem)CS$<>8__locals1.$VB$Local_Element)._ProccesorComposer = true;
							}
						}
						else
						{
							CS$<>8__locals1.$VB$Local_Element.Opacity = 0.0;
							CS$<>8__locals1.$VB$Local_Element.RenderTransform = new TranslateTransform(-25.0, 0.0);
							if (CS$<>8__locals1.$VB$Local_Element is MyListItem)
							{
								((MyListItem)CS$<>8__locals1.$VB$Local_Element)._ProccesorComposer = false;
							}
							list.Add(ModAnimation.AaOpacity(CS$<>8__locals1.$VB$Local_Element, (CS$<>8__locals1.$VB$Local_Element is TextBlock) ? 0.6 : 1.0, 200, num2, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false));
							list.Add(ModAnimation.AaTranslateX(CS$<>8__locals1.$VB$Local_Element, 5.0, 200, num2, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false));
							list.Add(ModAnimation.AaTranslateX(CS$<>8__locals1.$VB$Local_Element, 20.0, 300, num2, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false));
							if (CS$<>8__locals1.$VB$Local_Element is MyListItem)
							{
								list.Add(ModAnimation.AaCode(delegate
								{
									((MyListItem)CS$<>8__locals1.$VB$Local_Element)._ProccesorComposer = true;
									((MyListItem)CS$<>8__locals1.$VB$Local_Element).RefreshColor(CS$<>8__locals1.$VB$Me, new EventArgs());
								}, num2 + 280, false));
							}
							num2 = (int)Math.Round(unchecked((double)num2 + (double)Math.Max(8, checked(20 - num)) * 2.5));
							num++;
						}
					}
				}
				finally
				{
					List<FrameworkElement>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				ModAnimation.AniStart(list, "PageLeft PageChange " + Conversions.ToString(this.m_Manager), false);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00014450 File Offset: 0x00012650
		public void TriggerHideAnimation()
		{
			if (this.AnimatedControl == null)
			{
				if (!(base.RenderTransform is ScaleTransform))
				{
					base.RenderTransform = new ScaleTransform(1.0, 1.0);
					base.RenderTransformOrigin = new Point(0.5, 0.5);
				}
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaScaleTransform(this, 0.95 - ((ScaleTransform)base.RenderTransform).ScaleX, 130, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak), false),
					ModAnimation.AaOpacity(this, -base.Opacity, 100, 30, null, false)
				}, "PageLeft PageChange " + Conversions.ToString(this.m_Manager), false);
				return;
			}
			List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
			int num = 0;
			List<FrameworkElement> allAnimControls = this.GetAllAnimControls(false);
			checked
			{
				try
				{
					foreach (FrameworkElement frameworkElement in allAnimControls)
					{
						list.Add(ModAnimation.AaOpacity(frameworkElement, unchecked(-frameworkElement.Opacity), 60, (int)Math.Round(unchecked(80.0 / (double)allAnimControls.Count * (double)num)), null, false));
						list.Add(ModAnimation.AaTranslateX(frameworkElement, -6.0, 60, (int)Math.Round(unchecked(80.0 / (double)allAnimControls.Count * (double)num)), null, false));
						num++;
					}
				}
				finally
				{
					List<FrameworkElement>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				ModAnimation.AniStart(list, "PageLeft PageChange " + Conversions.ToString(this.m_Manager), false);
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000145F4 File Offset: 0x000127F4
		private List<FrameworkElement> GetAllAnimControls(bool IgnoreInvisibility = false)
		{
			List<FrameworkElement> result = new List<FrameworkElement>();
			this.GetAllAnimControls(this.AnimatedControl, ref result, IgnoreInvisibility);
			return result;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00014618 File Offset: 0x00012818
		private void GetAllAnimControls(FrameworkElement Element, ref List<FrameworkElement> AllControls, bool IgnoreInvisibility)
		{
			if (IgnoreInvisibility || Element.Visibility != Visibility.Collapsed)
			{
				if (Element is MyTextButton)
				{
					AllControls.Add(Element);
					return;
				}
				if (Element is MyListItem)
				{
					AllControls.Add(Element);
					return;
				}
				if (Element is ContentControl)
				{
					this.GetAllAnimControls((FrameworkElement)((ContentControl)Element).Content, ref AllControls, IgnoreInvisibility);
					return;
				}
				if (Element is Panel)
				{
					try
					{
						foreach (object obj in ((Panel)Element).Children)
						{
							FrameworkElement element = (FrameworkElement)obj;
							this.GetAllAnimControls(element, ref AllControls, IgnoreInvisibility);
						}
						return;
					}
					finally
					{
						IEnumerator enumerator;
						if (enumerator is IDisposable)
						{
							(enumerator as IDisposable).Dispose();
						}
					}
				}
				AllControls.Add(Element);
			}
		}

		// Token: 0x040000BB RID: 187
		private int m_Manager;

		// Token: 0x040000BC RID: 188
		public static readonly DependencyProperty m_Wrapper = DependencyProperty.Register("AnimatedControl", typeof(FrameworkElement), typeof(MyPageLeft), new PropertyMetadata(null));
	}
}
