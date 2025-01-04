using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020001C5 RID: 453
	[DesignerGenerated]
	public class MyButton : Border, IComponentConnector
	{
		// Token: 0x0600151B RID: 5403 RVA: 0x0008DB40 File Offset: 0x0008BD40
		public MyButton()
		{
			base.MouseEnter += new MouseEventHandler(this.RefreshColor);
			base.MouseLeave += new MouseEventHandler(this.RefreshColor);
			base.Loaded += new RoutedEventHandler(this.RefreshColor);
			base.IsEnabledChanged += delegate(object sender, DependencyPropertyChangedEventArgs e)
			{
				this.RefreshColor(RuntimeHelpers.GetObjectValue(sender), e);
			};
			base.MouseLeftButtonUp += this.Button_MouseUp;
			base.MouseLeftButtonDown += this.Button_MouseDown;
			base.MouseEnter += delegate(object sender, MouseEventArgs e)
			{
				this.Button_MouseEnter();
			};
			base.MouseLeftButtonUp += delegate(object sender, MouseButtonEventArgs e)
			{
				this.Button_MouseUp();
			};
			base.MouseLeave += delegate(object sender, MouseEventArgs e)
			{
				this.Button_MouseLeave();
			};
			this.m_StatusComposer = ModBase.GetUuid();
			this._StructComposer = MyButton.ColorState.Normal;
			this.m_CandidateComposer = false;
			this.InitializeComponent();
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600151C RID: 5404 RVA: 0x0008DC18 File Offset: 0x0008BE18
		// (remove) Token: 0x0600151D RID: 5405 RVA: 0x0008DC50 File Offset: 0x0008BE50
		public event MyButton.ClickEventHandler Click
		{
			[CompilerGenerated]
			add
			{
				MyButton.ClickEventHandler clickEventHandler = this.resolverComposer;
				MyButton.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MyButton.ClickEventHandler value2 = (MyButton.ClickEventHandler)Delegate.Combine(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MyButton.ClickEventHandler>(ref this.resolverComposer, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				MyButton.ClickEventHandler clickEventHandler = this.resolverComposer;
				MyButton.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MyButton.ClickEventHandler value2 = (MyButton.ClickEventHandler)Delegate.Remove(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MyButton.ClickEventHandler>(ref this.resolverComposer, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x0000BEF3 File Offset: 0x0000A0F3
		// (set) Token: 0x0600151F RID: 5407 RVA: 0x0000BF00 File Offset: 0x0000A100
		public string Text
		{
			get
			{
				return this.LabText.Text;
			}
			set
			{
				this.LabText.Text = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x0000BF0E File Offset: 0x0000A10E
		// (set) Token: 0x06001521 RID: 5409 RVA: 0x0000BF1B File Offset: 0x0000A11B
		public Thickness TextPadding
		{
			get
			{
				return this.LabText.Padding;
			}
			set
			{
				this.LabText.Padding = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x0000BF29 File Offset: 0x0000A129
		// (set) Token: 0x06001523 RID: 5411 RVA: 0x0000BF31 File Offset: 0x0000A131
		public MyButton.ColorState ColorType
		{
			get
			{
				return this._StructComposer;
			}
			set
			{
				this._StructComposer = value;
				this.RefreshColor(null, null);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x0000BF42 File Offset: 0x0000A142
		// (set) Token: 0x06001525 RID: 5413 RVA: 0x0000BF4F File Offset: 0x0000A14F
		public new Thickness Padding
		{
			get
			{
				return this.PanFore.Padding;
			}
			set
			{
				this.PanFore.Padding = value;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x0000BF5D File Offset: 0x0000A15D
		// (set) Token: 0x06001527 RID: 5415 RVA: 0x0000BF6A File Offset: 0x0000A16A
		public Transform RealRenderTransform
		{
			get
			{
				return this.PanFore.RenderTransform;
			}
			set
			{
				this.PanFore.RenderTransform = value;
			}
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0008DC88 File Offset: 0x0008BE88
		private void RefreshColor(object obj = null, object e = null)
		{
			try
			{
				if (base.IsLoaded && ModAnimation.CalcParser() == 0)
				{
					if (base.IsEnabled)
					{
						switch (this.ColorType)
						{
						case MyButton.ColorState.Normal:
							if (base.IsMouseOver)
							{
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaColor(this.PanFore, Border.BorderBrushProperty, "ColorBrush3", 100, 0, null, false)
								}, "MyButton Color " + Conversions.ToString(this.m_StatusComposer), false);
							}
							else
							{
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaColor(this.PanFore, Border.BorderBrushProperty, "ColorBrush1", 200, 0, null, false)
								}, "MyButton Color " + Conversions.ToString(this.m_StatusComposer), false);
							}
							break;
						case MyButton.ColorState.Highlight:
							if (base.IsMouseOver)
							{
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaColor(this.PanFore, Border.BorderBrushProperty, "ColorBrush3", 100, 0, null, false)
								}, "MyButton Color " + Conversions.ToString(this.m_StatusComposer), false);
							}
							else
							{
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaColor(this.PanFore, Border.BorderBrushProperty, "ColorBrush2", 200, 0, null, false)
								}, "MyButton Color " + Conversions.ToString(this.m_StatusComposer), false);
							}
							break;
						case MyButton.ColorState.Red:
							if (base.IsMouseOver)
							{
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaColor(this.PanFore, Border.BorderBrushProperty, "ColorBrushRedLight", 100, 0, null, false)
								}, "MyButton Color " + Conversions.ToString(this.m_StatusComposer), false);
							}
							else
							{
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaColor(this.PanFore, Border.BorderBrushProperty, "ColorBrushRedDark", 200, 0, null, false)
								}, "MyButton Color " + Conversions.ToString(this.m_StatusComposer), false);
							}
							break;
						}
					}
					else
					{
						ModAnimation.AniStart(new ModAnimation.AniData[]
						{
							ModAnimation.AaColor(this.PanFore, Border.BorderBrushProperty, ModSecret.m_StateField - this.PanFore.BorderBrush, 200, 0, null, false)
						}, "MyButton Color " + Conversions.ToString(this.m_StatusComposer), false);
					}
				}
				else
				{
					ModAnimation.AniStop("MyButton Color " + Conversions.ToString(this.m_StatusComposer));
					if (base.IsEnabled)
					{
						switch (this.ColorType)
						{
						case MyButton.ColorState.Normal:
							if (base.IsMouseOver)
							{
								this.PanFore.SetResourceReference(Border.BorderBrushProperty, "ColorBrush3");
							}
							else
							{
								this.PanFore.SetResourceReference(Border.BorderBrushProperty, "ColorBrush1");
							}
							break;
						case MyButton.ColorState.Highlight:
							if (base.IsMouseOver)
							{
								this.PanFore.SetResourceReference(Border.BorderBrushProperty, "ColorBrush3");
							}
							else
							{
								this.PanFore.SetResourceReference(Border.BorderBrushProperty, "ColorBrush2");
							}
							break;
						case MyButton.ColorState.Red:
							if (base.IsMouseOver)
							{
								this.PanFore.SetResourceReference(Border.BorderBrushProperty, "ColorBrushRedLight");
							}
							else
							{
								this.PanFore.SetResourceReference(Border.BorderBrushProperty, "ColorBrushRedDark");
							}
							break;
						}
					}
					else
					{
						this.PanFore.BorderBrush = ModSecret.m_StateField;
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "刷新按钮颜色出错", ModBase.LogLevel.Debug, "出现错误");
			}
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0008E03C File Offset: 0x0008C23C
		private void Button_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (this.m_CandidateComposer)
			{
				ModBase.Log("[Control] 按下按钮：" + this.Text, ModBase.LogLevel.Normal, "出现错误");
				MyButton.ClickEventHandler clickEventHandler = this.resolverComposer;
				if (clickEventHandler != null)
				{
					clickEventHandler(RuntimeHelpers.GetObjectValue(sender), e);
				}
				if (!string.IsNullOrEmpty(Conversions.ToString(base.Tag)) && (base.Tag.ToString().StartsWithF("链接-", false) || base.Tag.ToString().StartsWithF("启动-", false)))
				{
					ModMain.Hint("主页自定义按钮语法已更新，且不再兼容老版本语法，请查看新的自定义示例！", ModMain.HintType.Info, true);
				}
				ModEvent.TryStartEvent(this.EventType, this.EventData);
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x0000BF78 File Offset: 0x0000A178
		// (set) Token: 0x0600152B RID: 5419 RVA: 0x0000BF8A File Offset: 0x0000A18A
		public string EventType
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyButton.valComposer));
			}
			set
			{
				base.SetValue(MyButton.valComposer, value);
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x0000BF98 File Offset: 0x0000A198
		// (set) Token: 0x0600152D RID: 5421 RVA: 0x0000BFAA File Offset: 0x0000A1AA
		public string EventData
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyButton.m_AttrComposer));
			}
			set
			{
				base.SetValue(MyButton.m_AttrComposer, value);
			}
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0008E0E8 File Offset: 0x0008C2E8
		private void Button_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this.m_CandidateComposer = true;
			base.Focus();
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaScaleTransform(this.PanFore, 0.955 - ((ScaleTransform)this.PanFore.RenderTransform).ScaleX, 80, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.ExtraStrong), false),
				ModAnimation.AaScaleTransform(this.PanFore, -0.01, 700, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false)
			}, "MyButton Scale " + Conversions.ToString(this.m_StatusComposer), false);
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0008E188 File Offset: 0x0008C388
		private void Button_MouseEnter()
		{
			ModAnimation.AniStart(ModAnimation.AaColor(this.PanFore, Border.BackgroundProperty, (this._StructComposer == MyButton.ColorState.Red) ? "ColorBrushRedBack" : "ColorBrush7", 100, 0, null, false), "MyButton Background " + Conversions.ToString(this.m_StatusComposer), false);
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0008E1DC File Offset: 0x0008C3DC
		private void Button_MouseUp()
		{
			if (this.m_CandidateComposer)
			{
				this.m_CandidateComposer = false;
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaScaleTransform(this.PanFore, 1.0 - ((ScaleTransform)this.PanFore.RenderTransform).ScaleX, 300, 10, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false)
				}, "MyButton Scale " + Conversions.ToString(this.m_StatusComposer), false);
			}
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0008E258 File Offset: 0x0008C458
		private void Button_MouseLeave()
		{
			ModAnimation.AniStart(ModAnimation.AaColor(this.PanFore, Border.BackgroundProperty, "ColorBrushHalfWhite", 200, 0, null, false), "MyButton Background " + Conversions.ToString(this.m_StatusComposer), false);
			if (this.m_CandidateComposer)
			{
				this.m_CandidateComposer = false;
				ModAnimation.AniStart(ModAnimation.AaScaleTransform(this.PanFore, 1.0 - ((ScaleTransform)this.PanFore.RenderTransform).ScaleX, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false), "MyButton Scale " + Conversions.ToString(this.m_StatusComposer), false);
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x0000BFB8 File Offset: 0x0000A1B8
		// (set) Token: 0x06001533 RID: 5427 RVA: 0x0000BFC0 File Offset: 0x0000A1C0
		internal virtual MyButton PanBack { get; set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x0000BFC9 File Offset: 0x0000A1C9
		// (set) Token: 0x06001535 RID: 5429 RVA: 0x0000BFD1 File Offset: 0x0000A1D1
		internal virtual Border PanFore { get; set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x0000BFDA File Offset: 0x0000A1DA
		// (set) Token: 0x06001537 RID: 5431 RVA: 0x0000BFE2 File Offset: 0x0000A1E2
		internal virtual TextBlock LabText { get; set; }

		// Token: 0x06001538 RID: 5432 RVA: 0x0008E300 File Offset: 0x0008C500
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_EventComposer)
			{
				this.m_EventComposer = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/mybutton.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0000BFEB File Offset: 0x0000A1EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (MyButton)target;
				return;
			}
			if (connectionId == 2)
			{
				this.PanFore = (Border)target;
				return;
			}
			if (connectionId == 3)
			{
				this.LabText = (TextBlock)target;
				return;
			}
			this.m_EventComposer = true;
		}

		// Token: 0x04000AD4 RID: 2772
		[CompilerGenerated]
		private MyButton.ClickEventHandler resolverComposer;

		// Token: 0x04000AD5 RID: 2773
		public int m_StatusComposer;

		// Token: 0x04000AD6 RID: 2774
		public static readonly DependencyProperty m_RoleComposer = DependencyProperty.Register("Text", typeof(string), typeof(MyButton), new PropertyMetadata(delegate(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			if (!Information.IsNothing(sender))
			{
				((MyButton)sender).LabText.Text = Conversions.ToString(e.NewValue);
			}
		}));

		// Token: 0x04000AD7 RID: 2775
		private MyButton.ColorState _StructComposer;

		// Token: 0x04000AD8 RID: 2776
		public static readonly DependencyProperty printerComposer = DependencyProperty.Register("Padding", typeof(Thickness), typeof(MyButton), new PropertyMetadata(delegate(DependencyObject a0, DependencyPropertyChangedEventArgs a1)
		{
			((MyButton._Closure$__.$I0-1 == null) ? (MyButton._Closure$__.$I0-1 = delegate(MyButton sender, DependencyPropertyChangedEventArgs e)
			{
				if (sender != null)
				{
					Border panFore = sender.PanFore;
					object newValue = e.NewValue;
					panFore.Padding = ((newValue != null) ? ((Thickness)newValue) : default(Thickness));
				}
			}) : MyButton._Closure$__.$I0-1)((MyButton)a0, a1);
		}));

		// Token: 0x04000AD9 RID: 2777
		public static readonly DependencyProperty valComposer = DependencyProperty.Register("EventType", typeof(string), typeof(MyButton), new PropertyMetadata(null));

		// Token: 0x04000ADA RID: 2778
		public static readonly DependencyProperty m_AttrComposer = DependencyProperty.Register("EventData", typeof(string), typeof(MyButton), new PropertyMetadata(null));

		// Token: 0x04000ADB RID: 2779
		private bool m_CandidateComposer;

		// Token: 0x04000ADC RID: 2780
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyButton m_AdvisorComposer;

		// Token: 0x04000ADD RID: 2781
		[CompilerGenerated]
		[AccessedThroughProperty("PanFore")]
		private Border accountComposer;

		// Token: 0x04000ADE RID: 2782
		[AccessedThroughProperty("LabText")]
		[CompilerGenerated]
		private TextBlock queueComposer;

		// Token: 0x04000ADF RID: 2783
		private bool m_EventComposer;

		// Token: 0x020001C6 RID: 454
		// (Invoke) Token: 0x06001541 RID: 5441
		public delegate void ClickEventHandler(object sender, MouseButtonEventArgs e);

		// Token: 0x020001C7 RID: 455
		public enum ColorState
		{
			// Token: 0x04000AE1 RID: 2785
			Normal,
			// Token: 0x04000AE2 RID: 2786
			Highlight,
			// Token: 0x04000AE3 RID: 2787
			Red
		}
	}
}
