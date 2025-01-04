using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020001CD RID: 461
	[DesignerGenerated]
	public class MyIconButton : Border, IComponentConnector
	{
		// Token: 0x0600157A RID: 5498 RVA: 0x0008F0B0 File Offset: 0x0008D2B0
		public MyIconButton()
		{
			base.MouseLeftButtonUp += this.Button_MouseUp;
			base.MouseLeftButtonDown += this.Button_MouseDown;
			base.MouseLeftButtonUp += delegate(object sender, MouseButtonEventArgs e)
			{
				this.Button_MouseUp();
			};
			base.MouseLeave += delegate(object sender, MouseEventArgs e)
			{
				this.Button_MouseLeave();
			};
			base.MouseEnter += delegate(object sender, MouseEventArgs e)
			{
				this.RefreshAnim();
			};
			base.MouseLeave += delegate(object sender, MouseEventArgs e)
			{
				this.RefreshAnim();
			};
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.RefreshAnim();
			};
			this.authenticationComposer = ModBase.GetUuid();
			this.algoComposer = 1.0;
			this.Theme = MyIconButton.Themes.Color;
			this.m_MappingComposer = new SolidColorBrush(Color.FromRgb(128, 128, 128));
			this._DatabaseComposer = false;
			this.InitializeComponent();
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600157B RID: 5499 RVA: 0x0008F190 File Offset: 0x0008D390
		// (remove) Token: 0x0600157C RID: 5500 RVA: 0x0008F1C8 File Offset: 0x0008D3C8
		public event MyIconButton.ClickEventHandler Click
		{
			[CompilerGenerated]
			add
			{
				MyIconButton.ClickEventHandler clickEventHandler = this._MerchantComposer;
				MyIconButton.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MyIconButton.ClickEventHandler value2 = (MyIconButton.ClickEventHandler)Delegate.Combine(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MyIconButton.ClickEventHandler>(ref this._MerchantComposer, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				MyIconButton.ClickEventHandler clickEventHandler = this._MerchantComposer;
				MyIconButton.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MyIconButton.ClickEventHandler value2 = (MyIconButton.ClickEventHandler)Delegate.Remove(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MyIconButton.ClickEventHandler>(ref this._MerchantComposer, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x0000C1DE File Offset: 0x0000A3DE
		// (set) Token: 0x0600157E RID: 5502 RVA: 0x0000C1F0 File Offset: 0x0000A3F0
		public string Logo
		{
			get
			{
				return this.Path.Data.ToString();
			}
			set
			{
				this.Path.Data = (Geometry)new GeometryConverter().ConvertFromString(value);
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x0000C20D File Offset: 0x0000A40D
		// (set) Token: 0x06001580 RID: 5504 RVA: 0x0000C215 File Offset: 0x0000A415
		public double LogoScale
		{
			get
			{
				return this.algoComposer;
			}
			set
			{
				this.algoComposer = value;
				if (!Information.IsNothing(this.Path))
				{
					this.Path.RenderTransform = new ScaleTransform
					{
						ScaleX = this.LogoScale,
						ScaleY = this.LogoScale
					};
				}
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x0000C253 File Offset: 0x0000A453
		// (set) Token: 0x06001582 RID: 5506 RVA: 0x0000C25B File Offset: 0x0000A45B
		public MyIconButton.Themes Theme { get; set; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x0000C264 File Offset: 0x0000A464
		// (set) Token: 0x06001584 RID: 5508 RVA: 0x0000C26C File Offset: 0x0000A46C
		public SolidColorBrush Foreground
		{
			get
			{
				return this.m_MappingComposer;
			}
			set
			{
				this.m_MappingComposer = value;
				checked
				{
					ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
					this.RefreshAnim();
					ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
				}
			}
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0008F200 File Offset: 0x0008D400
		private void Button_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (this._DatabaseComposer)
			{
				ModBase.Log("[Control] 按下图标按钮" + (string.IsNullOrEmpty(base.Name) ? "" : ("：" + base.Name)), ModBase.LogLevel.Normal, "出现错误");
				MyIconButton.ClickEventHandler merchantComposer = this._MerchantComposer;
				if (merchantComposer != null)
				{
					merchantComposer(RuntimeHelpers.GetObjectValue(sender), e);
				}
				e.Handled = true;
				this.Button_MouseUp();
				ModEvent.TryStartEvent(this.EventType, this.EventData);
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x0000C293 File Offset: 0x0000A493
		// (set) Token: 0x06001587 RID: 5511 RVA: 0x0000C2A5 File Offset: 0x0000A4A5
		public string EventType
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyIconButton.tokenizerComposer));
			}
			set
			{
				base.SetValue(MyIconButton.tokenizerComposer, value);
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x0000C2B3 File Offset: 0x0000A4B3
		// (set) Token: 0x06001589 RID: 5513 RVA: 0x0000C2C5 File Offset: 0x0000A4C5
		public string EventData
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyIconButton.m_FilterComposer));
			}
			set
			{
				base.SetValue(MyIconButton.m_FilterComposer, value);
			}
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0008F284 File Offset: 0x0008D484
		private void Button_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this._DatabaseComposer = true;
			base.Focus();
			ModAnimation.AniStart(ModAnimation.AaScaleTransform(this.PanBack, 0.8 - ((ScaleTransform)this.PanBack.RenderTransform).ScaleX, 400, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false), "MyIconButton Scale " + Conversions.ToString(this.authenticationComposer), false);
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0008F2F4 File Offset: 0x0008D4F4
		private void Button_MouseUp()
		{
			if (this._DatabaseComposer)
			{
				this._DatabaseComposer = false;
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaScaleTransform(this.PanBack, 1.05 - ((ScaleTransform)this.PanBack.RenderTransform).ScaleX, 250, 0, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false),
					ModAnimation.AaScaleTransform(this.PanBack, -0.05, 250, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false)
				}, "MyIconButton Scale " + Conversions.ToString(this.authenticationComposer), false);
			}
			this.RefreshAnim();
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0008F3A0 File Offset: 0x0008D5A0
		private void Button_MouseLeave()
		{
			this._DatabaseComposer = false;
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaScaleTransform(this.PanBack, 1.0 - ((ScaleTransform)this.PanBack.RenderTransform).ScaleX, 250, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false)
			}, "MyIconButton Scale " + Conversions.ToString(this.authenticationComposer), false);
			this.RefreshAnim();
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0008F41C File Offset: 0x0008D61C
		public void RefreshAnim()
		{
			try
			{
				if (base.IsLoaded && ModAnimation.CalcParser() == 0)
				{
					if (this.PanBack.Background == null)
					{
						this.PanBack.Background = new ModBase.MyColor(0.0, 255.0, 255.0, 255.0);
					}
					if (this.Path.Fill == null)
					{
						switch (this.Theme)
						{
						case MyIconButton.Themes.Black:
							this.Path.Fill = new ModBase.MyColor(160.0, 0.0, 0.0, 0.0);
							break;
						case MyIconButton.Themes.Red:
							this.Path.Fill = new ModBase.MyColor(160.0, 255.0, 76.0, 76.0);
							break;
						case MyIconButton.Themes.Custom:
							this.Path.Fill = new ModBase.MyColor(160.0, this.Foreground);
							break;
						}
					}
					if (base.IsMouseOver)
					{
						List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
						switch (this.Theme)
						{
						case MyIconButton.Themes.Color:
							list.Add(ModAnimation.AaColor(this.Path, Shape.FillProperty, "ColorBrush2", 120, 0, null, false));
							break;
						case MyIconButton.Themes.White:
							list.Add(ModAnimation.AaColor(this.PanBack, Border.BackgroundProperty, new ModBase.MyColor(50.0, 255.0, 255.0, 255.0) - this.PanBack.Background, 120, 0, null, false));
							break;
						case MyIconButton.Themes.Black:
							list.Add(ModAnimation.AaColor(this.Path, Shape.FillProperty, new ModBase.MyColor(230.0, 0.0, 0.0, 0.0) - this.Path.Fill, 120, 0, null, false));
							break;
						case MyIconButton.Themes.Red:
							list.Add(ModAnimation.AaColor(this.Path, Shape.FillProperty, new ModBase.MyColor(255.0, 76.0, 76.0) - this.Path.Fill, 120, 0, null, false));
							break;
						case MyIconButton.Themes.Custom:
							list.Add(ModAnimation.AaColor(this.Path, Shape.FillProperty, new ModBase.MyColor(255.0, this.Foreground) - this.Path.Fill, 120, 0, null, false));
							break;
						}
						ModAnimation.AniStart(list, "MyIconButton Color " + Conversions.ToString(this.authenticationComposer), false);
					}
					else
					{
						List<ModAnimation.AniData> list2 = new List<ModAnimation.AniData>();
						switch (this.Theme)
						{
						case MyIconButton.Themes.Color:
							list2.Add(ModAnimation.AaColor(this.Path, Shape.FillProperty, "ColorBrush4", 150, 0, null, false));
							this.PanBack.Background = new ModBase.MyColor(0.0, 255.0, 255.0, 255.0);
							break;
						case MyIconButton.Themes.White:
							list2.Add(ModAnimation.AaColor(this.Path, Shape.FillProperty, "ColorBrush8", 150, 0, null, false));
							list2.Add(ModAnimation.AaColor(this.PanBack, Border.BackgroundProperty, new ModBase.MyColor(0.0, 255.0, 255.0, 255.0) - this.PanBack.Background, 150, 0, null, false));
							break;
						case MyIconButton.Themes.Black:
							list2.Add(ModAnimation.AaColor(this.Path, Shape.FillProperty, new ModBase.MyColor(160.0, 0.0, 0.0, 0.0) - this.Path.Fill, 150, 0, null, false));
							this.PanBack.Background = new ModBase.MyColor(0.0, 255.0, 255.0, 255.0);
							break;
						case MyIconButton.Themes.Red:
							list2.Add(ModAnimation.AaColor(this.Path, Shape.FillProperty, new ModBase.MyColor(160.0, 255.0, 76.0, 76.0) - this.Path.Fill, 150, 0, null, false));
							this.PanBack.Background = new ModBase.MyColor(0.0, 255.0, 255.0, 255.0);
							break;
						case MyIconButton.Themes.Custom:
							list2.Add(ModAnimation.AaColor(this.Path, Shape.FillProperty, new ModBase.MyColor(160.0, this.Foreground) - this.Path.Fill, 150, 0, null, false));
							this.PanBack.Background = new ModBase.MyColor(0.0, 255.0, 255.0, 255.0);
							break;
						}
						ModAnimation.AniStart(list2, "MyIconButton Color " + Conversions.ToString(this.authenticationComposer), false);
					}
				}
				else
				{
					ModAnimation.AniStop("MyIconButton Color " + Conversions.ToString(this.authenticationComposer));
					switch (this.Theme)
					{
					case MyIconButton.Themes.Color:
						this.Path.SetResourceReference(Shape.FillProperty, "ColorBrush5");
						break;
					case MyIconButton.Themes.White:
						this.Path.SetResourceReference(Shape.FillProperty, "ColorBrush8");
						break;
					case MyIconButton.Themes.Black:
						this.Path.Fill = new ModBase.MyColor(160.0, 0.0, 0.0, 0.0);
						break;
					case MyIconButton.Themes.Red:
						this.Path.Fill = new ModBase.MyColor(160.0, 255.0, 76.0, 76.0);
						break;
					case MyIconButton.Themes.Custom:
						this.Path.Fill = new ModBase.MyColor(160.0, this.Foreground);
						break;
					}
					this.PanBack.Background = new ModBase.MyColor(0.0, 255.0, 255.0, 255.0);
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "刷新图标按钮动画状态出错", ModBase.LogLevel.Debug, "出现错误");
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x0000C2D3 File Offset: 0x0000A4D3
		// (set) Token: 0x0600158F RID: 5519 RVA: 0x0000C2DB File Offset: 0x0000A4DB
		internal virtual Border PanBack { get; set; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		// (set) Token: 0x06001591 RID: 5521 RVA: 0x0000C2EC File Offset: 0x0000A4EC
		internal virtual Path Path { get; set; }

		// Token: 0x06001592 RID: 5522 RVA: 0x0008FBA8 File Offset: 0x0008DDA8
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.customerComposer)
			{
				this.customerComposer = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/myiconbutton.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0000C2F5 File Offset: 0x0000A4F5
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (Border)target;
				return;
			}
			if (connectionId == 2)
			{
				this.Path = (Path)target;
				return;
			}
			this.customerComposer = true;
		}

		// Token: 0x04000AF3 RID: 2803
		[CompilerGenerated]
		private MyIconButton.ClickEventHandler _MerchantComposer;

		// Token: 0x04000AF4 RID: 2804
		public int authenticationComposer;

		// Token: 0x04000AF5 RID: 2805
		private double algoComposer;

		// Token: 0x04000AF6 RID: 2806
		[CompilerGenerated]
		private MyIconButton.Themes m_ComparatorComposer;

		// Token: 0x04000AF7 RID: 2807
		private SolidColorBrush m_MappingComposer;

		// Token: 0x04000AF8 RID: 2808
		public static readonly DependencyProperty tokenizerComposer = DependencyProperty.Register("EventType", typeof(string), typeof(MyIconButton), new PropertyMetadata(null));

		// Token: 0x04000AF9 RID: 2809
		public static readonly DependencyProperty m_FilterComposer = DependencyProperty.Register("EventData", typeof(string), typeof(MyIconButton), new PropertyMetadata(null));

		// Token: 0x04000AFA RID: 2810
		private bool _DatabaseComposer;

		// Token: 0x04000AFB RID: 2811
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private Border _PredicateComposer;

		// Token: 0x04000AFC RID: 2812
		[AccessedThroughProperty("Path")]
		[CompilerGenerated]
		private Path poolComposer;

		// Token: 0x04000AFD RID: 2813
		private bool customerComposer;

		// Token: 0x020001CE RID: 462
		// (Invoke) Token: 0x0600159C RID: 5532
		public delegate void ClickEventHandler(object sender, EventArgs e);

		// Token: 0x020001CF RID: 463
		public enum Themes
		{
			// Token: 0x04000AFF RID: 2815
			Color,
			// Token: 0x04000B00 RID: 2816
			White,
			// Token: 0x04000B01 RID: 2817
			Black,
			// Token: 0x04000B02 RID: 2818
			Red,
			// Token: 0x04000B03 RID: 2819
			Custom
		}
	}
}
