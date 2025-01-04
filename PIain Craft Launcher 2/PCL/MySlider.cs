using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020001DD RID: 477
	[DesignerGenerated]
	public class MySlider : Border, IComponentConnector
	{
		// Token: 0x06001623 RID: 5667 RVA: 0x00091E74 File Offset: 0x00090074
		public MySlider()
		{
			base.SizeChanged += this.RefreshWidth;
			base.MouseLeftButtonDown += this.DragStart;
			base.IsEnabledChanged += delegate(object sender, DependencyPropertyChangedEventArgs e)
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
			base.MouseEnter += delegate(object sender, MouseEventArgs e)
			{
				this.MySlider_MouseEnter();
			};
			base.KeyDown += this.MySlider_KeyDown;
			this.testsIterator = ModBase.GetUuid();
			this._PropertyIterator = 100;
			this.composerIterator = false;
			this.iteratorIterator = 0;
			this.ValueByKey = 1U;
			this.InitializeComponent();
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x00091F34 File Offset: 0x00090134
		[CompilerGenerated]
		public void FillTests(MySlider.ChangeEventHandler obj)
		{
			MySlider.ChangeEventHandler changeEventHandler = this.mapperIterator;
			MySlider.ChangeEventHandler changeEventHandler2;
			do
			{
				changeEventHandler2 = changeEventHandler;
				MySlider.ChangeEventHandler value = (MySlider.ChangeEventHandler)Delegate.Combine(changeEventHandler2, obj);
				changeEventHandler = Interlocked.CompareExchange<MySlider.ChangeEventHandler>(ref this.mapperIterator, value, changeEventHandler2);
			}
			while (changeEventHandler != changeEventHandler2);
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00091F6C File Offset: 0x0009016C
		[CompilerGenerated]
		public void WriteTests(MySlider.ChangeEventHandler obj)
		{
			MySlider.ChangeEventHandler changeEventHandler = this.mapperIterator;
			MySlider.ChangeEventHandler changeEventHandler2;
			do
			{
				changeEventHandler2 = changeEventHandler;
				MySlider.ChangeEventHandler value = (MySlider.ChangeEventHandler)Delegate.Remove(changeEventHandler2, obj);
				changeEventHandler = Interlocked.CompareExchange<MySlider.ChangeEventHandler>(ref this.mapperIterator, value, changeEventHandler2);
			}
			while (changeEventHandler != changeEventHandler2);
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x00091FA4 File Offset: 0x000901A4
		[CompilerGenerated]
		public void ComputeTests(MySlider.PreviewChangeEventHandler obj)
		{
			MySlider.PreviewChangeEventHandler previewChangeEventHandler = this.threadIterator;
			MySlider.PreviewChangeEventHandler previewChangeEventHandler2;
			do
			{
				previewChangeEventHandler2 = previewChangeEventHandler;
				MySlider.PreviewChangeEventHandler value = (MySlider.PreviewChangeEventHandler)Delegate.Combine(previewChangeEventHandler2, obj);
				previewChangeEventHandler = Interlocked.CompareExchange<MySlider.PreviewChangeEventHandler>(ref this.threadIterator, value, previewChangeEventHandler2);
			}
			while (previewChangeEventHandler != previewChangeEventHandler2);
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00091FDC File Offset: 0x000901DC
		[CompilerGenerated]
		public void IncludeTests(MySlider.PreviewChangeEventHandler obj)
		{
			MySlider.PreviewChangeEventHandler previewChangeEventHandler = this.threadIterator;
			MySlider.PreviewChangeEventHandler previewChangeEventHandler2;
			do
			{
				previewChangeEventHandler2 = previewChangeEventHandler;
				MySlider.PreviewChangeEventHandler value = (MySlider.PreviewChangeEventHandler)Delegate.Remove(previewChangeEventHandler2, obj);
				previewChangeEventHandler = Interlocked.CompareExchange<MySlider.PreviewChangeEventHandler>(ref this.threadIterator, value, previewChangeEventHandler2);
			}
			while (previewChangeEventHandler != previewChangeEventHandler2);
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x0000C782 File Offset: 0x0000A982
		// (set) Token: 0x06001629 RID: 5673 RVA: 0x0000C78A File Offset: 0x0000A98A
		public int MaxValue
		{
			get
			{
				return this._PropertyIterator;
			}
			set
			{
				if (value != this._PropertyIterator)
				{
					this._PropertyIterator = value;
					this.RefreshWidth(null, null);
				}
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x0000C7A4 File Offset: 0x0000A9A4
		// (set) Token: 0x0600162B RID: 5675 RVA: 0x00092014 File Offset: 0x00090214
		public int Value
		{
			get
			{
				return this.iteratorIterator;
			}
			set
			{
				try
				{
					value = checked((int)Math.Round(ModBase.MathClamp((double)value, 0.0, (double)this.MaxValue)));
					if (this.iteratorIterator != value)
					{
						int num = this.iteratorIterator;
						this.iteratorIterator = value;
						if (ModAnimation.CalcParser() == 0)
						{
							ModBase.RouteEventArgs routeEventArgs = new ModBase.RouteEventArgs(false);
							MySlider.PreviewChangeEventHandler previewChangeEventHandler = this.threadIterator;
							if (previewChangeEventHandler != null)
							{
								previewChangeEventHandler(this, routeEventArgs);
							}
							if (routeEventArgs.m_SerializerError)
							{
								this.iteratorIterator = num;
								this.DragStop();
								return;
							}
						}
						if (base.IsLoaded && ModAnimation.CalcParser() == 0)
						{
							if (base.ActualWidth < this.ShapeDot.Width)
							{
								return;
							}
							double num2 = (double)this.iteratorIterator / (double)this.MaxValue * (base.ActualWidth - this.ShapeDot.Width);
							double num3 = Math.Abs(this.LineFore.Width / (base.ActualWidth - this.ShapeDot.Width) - (double)this.iteratorIterator / (double)this.MaxValue);
							double num4 = (1.0 - Math.Pow(1.0 - num3, 3.0)) * 300.0 + (double)(this.composerIterator ? 100 : 0);
							ModAnimation.AniStart(new ModAnimation.AniData[]
							{
								ModAnimation.AaWidth(this.LineFore, Math.Max(0.0, num2 + ((num2 < 0.5) ? 0.0 : 0.5)) - this.LineFore.Width, checked((int)Math.Round(num4)), 0, (ModAnimation.AniEase)((num4 > 50.0) ? new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle) : new ModAnimation.AniEaseLinear()), false),
								ModAnimation.AaWidth(this.LineBack, Math.Max(0.0, base.ActualWidth - this.ShapeDot.Width - num2 + ((base.ActualWidth - this.ShapeDot.Width - num2 < 0.5) ? 0.0 : 0.5)) - this.LineBack.Width, checked((int)Math.Round(num4)), 0, (ModAnimation.AniEase)((num4 > 50.0) ? new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle) : new ModAnimation.AniEaseLinear()), false),
								ModAnimation.AaX(this.ShapeDot, num2 - this.ShapeDot.Margin.Left, checked((int)Math.Round(num4)), 0, (ModAnimation.AniEase)((num4 > 50.0) ? new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle) : new ModAnimation.AniEaseLinear()), false)
							}, "MySlider Progress " + Conversions.ToString(this.testsIterator), false);
						}
						else
						{
							this.RefreshWidth(null, null);
						}
						if (ModAnimation.CalcParser() == 0)
						{
							MySlider.ChangeEventHandler changeEventHandler = this.mapperIterator;
							if (changeEventHandler != null)
							{
								changeEventHandler(this, false);
							}
						}
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "滑动条进度改变出错", ModBase.LogLevel.Hint, "出现错误");
				}
			}
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x00092348 File Offset: 0x00090548
		private void RefreshWidth(object sender, SizeChangedEventArgs e)
		{
			if (!Information.IsNothing(e))
			{
				this.PanMain.Width = e.NewSize.Width;
			}
			ModAnimation.AniStop("MySlider Progress " + Conversions.ToString(this.testsIterator));
			double num = (double)this.iteratorIterator / (double)this.MaxValue * (base.ActualWidth - this.ShapeDot.Width);
			this.LineFore.Width = Math.Max(0.0, num + ((num < 0.5) ? 0.0 : 0.5));
			this.LineBack.Width = Math.Max(0.0, base.ActualWidth - this.ShapeDot.Width - num + ((base.ActualWidth - this.ShapeDot.Width - num < 0.5) ? 0.0 : 0.5));
			ModBase.SetLeft(this.ShapeDot, num);
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x0009245C File Offset: 0x0009065C
		private void DragStart(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			ModMain._DicRepository = this;
			this.RefreshColor();
			ModMain._ProcessIterator.DragDoing();
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaScaleTransform(this.ShapeDot, 1.3 - ((ScaleTransform)this.ShapeDot.RenderTransform).ScaleX, 40, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false)
			}, "MySlider Scale " + Conversions.ToString(this.testsIterator), false);
			this.RefreshPopup();
			ModAnimation.AniStop("MySlider KeyPopup " + Conversions.ToString(this.testsIterator));
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x00092504 File Offset: 0x00090704
		public void DragDoing()
		{
			int num = checked((int)Math.Round(unchecked(ModBase.MathClamp((Mouse.GetPosition(this.PanMain).X - this.ShapeDot.Width / 2.0) / (base.ActualWidth - this.ShapeDot.Width), 0.0, 1.0) * (double)this.MaxValue)));
			if (num != this.Value)
			{
				this.Value = num;
			}
			this.RefreshPopup();
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x0009258C File Offset: 0x0009078C
		public void DragStop()
		{
			this.RefreshColor();
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaScaleTransform(this.ShapeDot, 1.0 - ((ScaleTransform)this.ShapeDot.RenderTransform).ScaleX, 200, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false)
			}, "MySlider Scale " + Conversions.ToString(this.testsIterator), false);
			this.Popup.IsOpen = false;
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x0009260C File Offset: 0x0009080C
		public void RefreshPopup()
		{
			if (this.m_RepositoryIterator != null)
			{
				this.Popup.IsOpen = true;
				this.TextHint.Text = Conversions.ToString(this.m_RepositoryIterator.DynamicInvoke(new object[]
				{
					this.Value
				}));
				Typeface typeface = new Typeface(this.TextHint.FontFamily, this.TextHint.FontStyle, this.TextHint.FontWeight, this.TextHint.FontStretch);
				FormattedText formattedText = new FormattedText(this.TextHint.Text, Thread.CurrentThread.CurrentCulture, this.TextHint.FlowDirection, typeface, this.TextHint.FontSize, this.TextHint.Foreground, (double)ModBase._ConfigurationRepository);
				this.TextHint.Width = formattedText.Width;
			}
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x000926E8 File Offset: 0x000908E8
		private void RefreshColor()
		{
			try
			{
				string text;
				string text2;
				int time;
				if (base.IsEnabled)
				{
					if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(ModMain._DicRepository)) && ModMain._DicRepository.Equals(this))
					{
						text = "ColorBrush3";
						text2 = "ColorBrush3";
						time = 40;
					}
					else if (base.IsMouseOver)
					{
						text = "ColorBrush3";
						text2 = "ColorBrush3";
						time = 40;
					}
					else
					{
						text = "ColorBrushBg0";
						text2 = "ColorBrushBg0";
						time = 100;
					}
				}
				else
				{
					text = "ColorBrushGray5";
					text2 = "ColorBrushGray5";
					time = 200;
				}
				if (base.IsLoaded && ModAnimation.CalcParser() == 0)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaColor(this, Border.BorderBrushProperty, text, time, 0, null, false),
						ModAnimation.AaColor(this.ShapeDot, Shape.FillProperty, text2, time, 0, null, false)
					}, "MySlider Color " + Conversions.ToString(this.testsIterator), false);
				}
				else
				{
					ModAnimation.AniStop("MySlider Color " + Conversions.ToString(this.testsIterator));
					base.SetResourceReference(Border.BorderBrushProperty, text);
					this.ShapeDot.SetResourceReference(Shape.FillProperty, text2);
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "滑动条颜色改变出错", ModBase.LogLevel.Debug, "出现错误");
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x0000C7AC File Offset: 0x0000A9AC
		// (set) Token: 0x06001633 RID: 5683 RVA: 0x0000C7B4 File Offset: 0x0000A9B4
		public uint ValueByKey { get; set; }

		// Token: 0x06001634 RID: 5684 RVA: 0x0000C7BD File Offset: 0x0000A9BD
		private void MySlider_MouseEnter()
		{
			base.Focus();
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x00092844 File Offset: 0x00090A44
		private void MySlider_KeyDown(object sender, KeyEventArgs e)
		{
			checked
			{
				if (!object.ReferenceEquals(this, RuntimeHelpers.GetObjectValue(ModMain._DicRepository)))
				{
					if (e.Key == Key.Left)
					{
						this.composerIterator = true;
						this.Value = (int)(unchecked((long)this.Value) - (long)(unchecked((ulong)this.ValueByKey)));
						this.composerIterator = false;
						e.Handled = true;
					}
					else
					{
						if (e.Key != Key.Right)
						{
							return;
						}
						this.composerIterator = true;
						this.Value = (int)(unchecked((long)this.Value) + (long)(unchecked((ulong)this.ValueByKey)));
						this.composerIterator = false;
						e.Handled = true;
					}
					if (this.m_RepositoryIterator != null)
					{
						this.RefreshPopup();
						ModAnimation.AniStop("MySlider KeyPopup " + Conversions.ToString(this.testsIterator));
						ModAnimation.AniStart(ModAnimation.AaCode(delegate
						{
							this.Popup.IsOpen = false;
						}, (int)Math.Round(unchecked(700.0 * ModAnimation.m_Task)), false), "MySlider KeyPopup " + Conversions.ToString(this.testsIterator), false);
					}
				}
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x0000C7C6 File Offset: 0x0000A9C6
		// (set) Token: 0x06001637 RID: 5687 RVA: 0x0000C7CE File Offset: 0x0000A9CE
		internal virtual MySlider PanBack { get; set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x0000C7D7 File Offset: 0x0000A9D7
		// (set) Token: 0x06001639 RID: 5689 RVA: 0x0000C7DF File Offset: 0x0000A9DF
		internal virtual Grid PanMain { get; set; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		// (set) Token: 0x0600163B RID: 5691 RVA: 0x0000C7F0 File Offset: 0x0000A9F0
		internal virtual Line LineBack { get; set; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x0000C7F9 File Offset: 0x0000A9F9
		// (set) Token: 0x0600163D RID: 5693 RVA: 0x0000C801 File Offset: 0x0000AA01
		internal virtual Line LineFore { get; set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x0000C80A File Offset: 0x0000AA0A
		// (set) Token: 0x0600163F RID: 5695 RVA: 0x0000C812 File Offset: 0x0000AA12
		internal virtual Ellipse ShapeDot { get; set; }

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x0000C81B File Offset: 0x0000AA1B
		// (set) Token: 0x06001641 RID: 5697 RVA: 0x0000C823 File Offset: 0x0000AA23
		internal virtual Popup Popup { get; set; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x0000C82C File Offset: 0x0000AA2C
		// (set) Token: 0x06001643 RID: 5699 RVA: 0x0000C834 File Offset: 0x0000AA34
		internal virtual TextBlock TextHint { get; set; }

		// Token: 0x06001644 RID: 5700 RVA: 0x00092940 File Offset: 0x00090B40
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_HelperIterator)
			{
				this.m_HelperIterator = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/myslider.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x00092970 File Offset: 0x00090B70
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (MySlider)target;
				return;
			}
			if (connectionId == 2)
			{
				this.PanMain = (Grid)target;
				return;
			}
			if (connectionId == 3)
			{
				this.LineBack = (Line)target;
				return;
			}
			if (connectionId == 4)
			{
				this.LineFore = (Line)target;
				return;
			}
			if (connectionId == 5)
			{
				this.ShapeDot = (Ellipse)target;
				return;
			}
			if (connectionId == 6)
			{
				this.Popup = (Popup)target;
				return;
			}
			if (connectionId == 7)
			{
				this.TextHint = (TextBlock)target;
				return;
			}
			this.m_HelperIterator = true;
		}

		// Token: 0x04000B45 RID: 2885
		public int testsIterator;

		// Token: 0x04000B46 RID: 2886
		[CompilerGenerated]
		private MySlider.ChangeEventHandler mapperIterator;

		// Token: 0x04000B47 RID: 2887
		[CompilerGenerated]
		private MySlider.PreviewChangeEventHandler threadIterator;

		// Token: 0x04000B48 RID: 2888
		private int _PropertyIterator;

		// Token: 0x04000B49 RID: 2889
		private bool composerIterator;

		// Token: 0x04000B4A RID: 2890
		private int iteratorIterator;

		// Token: 0x04000B4B RID: 2891
		public Delegate m_RepositoryIterator;

		// Token: 0x04000B4C RID: 2892
		[CompilerGenerated]
		private uint m_TestIterator;

		// Token: 0x04000B4D RID: 2893
		[CompilerGenerated]
		[AccessedThroughProperty("PanBack")]
		private MySlider _MapIterator;

		// Token: 0x04000B4E RID: 2894
		[AccessedThroughProperty("PanMain")]
		[CompilerGenerated]
		private Grid errorIterator;

		// Token: 0x04000B4F RID: 2895
		[CompilerGenerated]
		[AccessedThroughProperty("LineBack")]
		private Line m_ContextIterator;

		// Token: 0x04000B50 RID: 2896
		[CompilerGenerated]
		[AccessedThroughProperty("LineFore")]
		private Line m_SpecificationIterator;

		// Token: 0x04000B51 RID: 2897
		[CompilerGenerated]
		[AccessedThroughProperty("ShapeDot")]
		private Ellipse _MockIterator;

		// Token: 0x04000B52 RID: 2898
		[AccessedThroughProperty("Popup")]
		[CompilerGenerated]
		private Popup requestIterator;

		// Token: 0x04000B53 RID: 2899
		[AccessedThroughProperty("TextHint")]
		[CompilerGenerated]
		private TextBlock m_DicIterator;

		// Token: 0x04000B54 RID: 2900
		private bool m_HelperIterator;

		// Token: 0x020001DE RID: 478
		// (Invoke) Token: 0x0600164F RID: 5711
		public delegate void ChangeEventHandler(object sender, bool user);

		// Token: 0x020001DF RID: 479
		// (Invoke) Token: 0x06001654 RID: 5716
		public delegate void PreviewChangeEventHandler(object sender, ModBase.RouteEventArgs e);
	}
}
