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
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x0200008D RID: 141
	[DesignerGenerated]
	public class MyExtraButton : Grid, IComponentConnector
	{
		// Token: 0x0600039C RID: 924 RVA: 0x00026B7C File Offset: 0x00024D7C
		public MyExtraButton()
		{
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.RefreshColor();
			};
			this._ComposerBroadcaster = 0.0;
			this.m_IteratorBroadcaster = ModBase.GetUuid();
			this.repositoryBroadcaster = "";
			this.errorBroadcaster = 1.0;
			this.m_ContextBroadcaster = false;
			this._SpecificationBroadcaster = null;
			this.mockBroadcaster = false;
			this.requestBroadcaster = false;
			this.m_DicBroadcaster = false;
			this.InitializeComponent();
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600039D RID: 925 RVA: 0x00026C00 File Offset: 0x00024E00
		// (remove) Token: 0x0600039E RID: 926 RVA: 0x00026C38 File Offset: 0x00024E38
		public event MyExtraButton.ClickEventHandler Click
		{
			[CompilerGenerated]
			add
			{
				MyExtraButton.ClickEventHandler clickEventHandler = this.threadBroadcaster;
				MyExtraButton.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MyExtraButton.ClickEventHandler value2 = (MyExtraButton.ClickEventHandler)Delegate.Combine(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MyExtraButton.ClickEventHandler>(ref this.threadBroadcaster, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				MyExtraButton.ClickEventHandler clickEventHandler = this.threadBroadcaster;
				MyExtraButton.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MyExtraButton.ClickEventHandler value2 = (MyExtraButton.ClickEventHandler)Delegate.Remove(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MyExtraButton.ClickEventHandler>(ref this.threadBroadcaster, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00026C70 File Offset: 0x00024E70
		[CompilerGenerated]
		public void IncludeField(MyExtraButton.RightClickEventHandler obj)
		{
			MyExtraButton.RightClickEventHandler rightClickEventHandler = this._PropertyBroadcaster;
			MyExtraButton.RightClickEventHandler rightClickEventHandler2;
			do
			{
				rightClickEventHandler2 = rightClickEventHandler;
				MyExtraButton.RightClickEventHandler value = (MyExtraButton.RightClickEventHandler)Delegate.Combine(rightClickEventHandler2, obj);
				rightClickEventHandler = Interlocked.CompareExchange<MyExtraButton.RightClickEventHandler>(ref this._PropertyBroadcaster, value, rightClickEventHandler2);
			}
			while (rightClickEventHandler != rightClickEventHandler2);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00026CA8 File Offset: 0x00024EA8
		[CompilerGenerated]
		public void CloneField(MyExtraButton.RightClickEventHandler obj)
		{
			MyExtraButton.RightClickEventHandler rightClickEventHandler = this._PropertyBroadcaster;
			MyExtraButton.RightClickEventHandler rightClickEventHandler2;
			do
			{
				rightClickEventHandler2 = rightClickEventHandler;
				MyExtraButton.RightClickEventHandler value = (MyExtraButton.RightClickEventHandler)Delegate.Remove(rightClickEventHandler2, obj);
				rightClickEventHandler = Interlocked.CompareExchange<MyExtraButton.RightClickEventHandler>(ref this._PropertyBroadcaster, value, rightClickEventHandler2);
			}
			while (rightClickEventHandler != rightClickEventHandler2);
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x000041B6 File Offset: 0x000023B6
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x00026CE0 File Offset: 0x00024EE0
		public double Progress
		{
			get
			{
				return this._ComposerBroadcaster;
			}
			set
			{
				if (this._ComposerBroadcaster != value)
				{
					this._ComposerBroadcaster = value;
					if (value < 0.0001)
					{
						this.PanProgress.Visibility = Visibility.Collapsed;
						return;
					}
					this.PanProgress.Visibility = Visibility.Visible;
					this.RectProgress.Rect = new Rect(0.0, 40.0 * (1.0 - value), 40.0, 40.0 * value);
				}
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x000041BE File Offset: 0x000023BE
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x000041C6 File Offset: 0x000023C6
		public string Logo
		{
			get
			{
				return this.repositoryBroadcaster;
			}
			set
			{
				if (Operators.CompareString(value, this.repositoryBroadcaster, false) != 0)
				{
					this.repositoryBroadcaster = value;
					this.Path.Data = (Geometry)new GeometryConverter().ConvertFromString(value);
				}
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000041F9 File Offset: 0x000023F9
		public double LoginField()
		{
			return this.errorBroadcaster;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00004201 File Offset: 0x00002401
		public void ManageField(double value)
		{
			this.errorBroadcaster = value;
			if (!Information.IsNothing(this.Path))
			{
				this.Path.RenderTransform = new ScaleTransform
				{
					ScaleX = this.LoginField(),
					ScaleY = this.LoginField()
				};
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000423F File Offset: 0x0000243F
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x00026D64 File Offset: 0x00024F64
		public bool Show
		{
			get
			{
				return this.m_ContextBroadcaster;
			}
			set
			{
				if (this.m_ContextBroadcaster != value)
				{
					this.m_ContextBroadcaster = value;
					ModBase.RunInUi(delegate()
					{
						if (value)
						{
							this.Visibility = Visibility.Visible;
							ModAnimation.AniStart(new ModAnimation.AniData[]
							{
								ModAnimation.AaScaleTransform(this, 0.3 - ((ScaleTransform)this.RenderTransform).ScaleX, 500, 60, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false),
								ModAnimation.AaScaleTransform(this, 0.7, 500, 60, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false),
								ModAnimation.AaHeight(this, 50.0 - this.Height, 200, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false)
							}, "MyExtraButton MainScale " + Conversions.ToString(this.m_IteratorBroadcaster), false);
						}
						else
						{
							ModAnimation.AniStart(new ModAnimation.AniData[]
							{
								ModAnimation.AaScaleTransform(this, -((ScaleTransform)this.RenderTransform).ScaleX, 100, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak), false),
								ModAnimation.AaHeight(this, -this.Height, 400, 100, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
								ModAnimation.AaCode(delegate
								{
									base.Visibility = Visibility.Collapsed;
								}, 0, true)
							}, "MyExtraButton MainScale " + Conversions.ToString(this.m_IteratorBroadcaster), false);
						}
						this.IsHitTestVisible = value;
					}, false);
				}
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00004247 File Offset: 0x00002447
		public void ShowRefresh()
		{
			if (this._SpecificationBroadcaster != null)
			{
				this.Show = this._SpecificationBroadcaster();
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00026DB4 File Offset: 0x00024FB4
		private void Button_LeftMouseUp(object sender, MouseButtonEventArgs e)
		{
			if (this.requestBroadcaster)
			{
				ModBase.Log("[Control] 按下附加按钮" + (Operators.ConditionalCompareObjectEqual(base.ToolTip, "", false) ? "" : ("：" + base.ToolTip.ToString())), ModBase.LogLevel.Normal, "出现错误");
				MyExtraButton.ClickEventHandler clickEventHandler = this.threadBroadcaster;
				if (clickEventHandler != null)
				{
					clickEventHandler(RuntimeHelpers.GetObjectValue(sender), e);
				}
				e.Handled = true;
				this.Button_LeftMouseUp();
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00026E34 File Offset: 0x00025034
		private void Button_RightMouseUp(object sender, MouseButtonEventArgs e)
		{
			if (this.m_DicBroadcaster)
			{
				ModBase.Log("[Control] 右键按下附加按钮" + (Operators.ConditionalCompareObjectEqual(base.ToolTip, "", false) ? "" : ("：" + base.ToolTip.ToString())), ModBase.LogLevel.Normal, "出现错误");
				MyExtraButton.RightClickEventHandler propertyBroadcaster = this._PropertyBroadcaster;
				if (propertyBroadcaster != null)
				{
					propertyBroadcaster(RuntimeHelpers.GetObjectValue(sender), e);
				}
				e.Handled = true;
				this.Button_RightMouseUp();
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00004262 File Offset: 0x00002462
		// (set) Token: 0x060003AD RID: 941 RVA: 0x0000426A File Offset: 0x0000246A
		public bool CanRightClick
		{
			get
			{
				return this.mockBroadcaster;
			}
			set
			{
				this.mockBroadcaster = value;
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00026EB4 File Offset: 0x000250B4
		private void Button_LeftMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (!this.requestBroadcaster && !this.m_DicBroadcaster)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaScaleTransform(this.PanScale, 0.85 - ((ScaleTransform)this.PanScale.RenderTransform).ScaleX, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false),
					ModAnimation.AaScaleTransform(this.PanScale, -0.05, 60, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false)
				}, "MyExtraButton Scale " + Conversions.ToString(this.m_IteratorBroadcaster), false);
			}
			this.requestBroadcaster = true;
			base.Focus();
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00026F6C File Offset: 0x0002516C
		private void Button_RightMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (this.CanRightClick)
			{
				if (!this.requestBroadcaster && !this.m_DicBroadcaster)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaScaleTransform(this.PanScale, 0.85 - ((ScaleTransform)this.PanScale.RenderTransform).ScaleX, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false),
						ModAnimation.AaScaleTransform(this.PanScale, -0.05, 60, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false)
					}, "MyExtraButton Scale " + Conversions.ToString(this.m_IteratorBroadcaster), false);
				}
				this.m_DicBroadcaster = true;
				base.Focus();
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0002702C File Offset: 0x0002522C
		private void Button_LeftMouseUp()
		{
			if (!this.m_DicBroadcaster)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaScaleTransform(this.PanScale, 1.0 - ((ScaleTransform)this.PanScale.RenderTransform).ScaleX, 300, 0, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Middle), false)
				}, "MyExtraButton Scale " + Conversions.ToString(this.m_IteratorBroadcaster), false);
			}
			this.requestBroadcaster = false;
			this.RefreshColor();
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000270B0 File Offset: 0x000252B0
		private void Button_RightMouseUp()
		{
			if (this.CanRightClick)
			{
				if (!this.requestBroadcaster)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaScaleTransform(this.PanScale, 1.0 - ((ScaleTransform)this.PanScale.RenderTransform).ScaleX, 300, 0, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Middle), false)
					}, "MyExtraButton Scale " + Conversions.ToString(this.m_IteratorBroadcaster), false);
				}
				this.m_DicBroadcaster = false;
				this.RefreshColor();
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0002713C File Offset: 0x0002533C
		private void Button_MouseLeave()
		{
			this.requestBroadcaster = false;
			this.m_DicBroadcaster = false;
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaScaleTransform(this.PanScale, 1.0 - ((ScaleTransform)this.PanScale.RenderTransform).ScaleX, 500, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false)
			}, "MyExtraButton Scale " + Conversions.ToString(this.m_IteratorBroadcaster), false);
			this.RefreshColor();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000271BC File Offset: 0x000253BC
		public void RefreshColor()
		{
			try
			{
				if (base.IsLoaded && ModAnimation.CalcParser() == 0)
				{
					if (base.IsMouseOver)
					{
						ModAnimation.AniStart(ModAnimation.AaColor(this.PanColor, Panel.BackgroundProperty, "ColorBrush4", 120, 0, null, false), "MyExtraButton Color " + Conversions.ToString(this.m_IteratorBroadcaster), false);
					}
					else
					{
						ModAnimation.AniStart(ModAnimation.AaColor(this.PanColor, Panel.BackgroundProperty, "ColorBrush3", 150, 0, null, false), "MyExtraButton Color " + Conversions.ToString(this.m_IteratorBroadcaster), false);
					}
				}
				else
				{
					ModAnimation.AniStop("MyExtraButton Color " + Conversions.ToString(this.m_IteratorBroadcaster));
					if (base.IsMouseOver)
					{
						this.PanColor.SetResourceReference(Panel.BackgroundProperty, "ColorBrush4");
					}
					else
					{
						this.PanColor.SetResourceReference(Panel.BackgroundProperty, "ColorBrush3");
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "刷新图标按钮颜色出错", ModBase.LogLevel.Debug, "出现错误");
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00004273 File Offset: 0x00002473
		public void Ribble()
		{
			ModBase.RunInUi(delegate()
			{
				Border Shape = new Border
				{
					CornerRadius = new CornerRadius(1000.0),
					BorderThickness = new Thickness(0.001),
					Opacity = 0.5,
					RenderTransformOrigin = new Point(0.5, 0.5),
					RenderTransform = new ScaleTransform()
				};
				Shape.SetResourceReference(Border.BackgroundProperty, "ColorBrush5");
				this.PanScale.Children.Insert(0, Shape);
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaScaleTransform(Shape, 13.0, 1000, 0, new ModAnimation.AniEaseInoutFluent(ModAnimation.AniEasePower.Strong, 0.3), false),
					ModAnimation.AaOpacity(Shape, -Shape.Opacity, 1000, 0, null, false),
					ModAnimation.AaCode(delegate
					{
						this.PanScale.Children.Remove(Shape);
					}, 0, true)
				}, "ExtraButton Ribble " + Conversions.ToString(ModBase.GetUuid()), false);
			}, false);
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00004287 File Offset: 0x00002487
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000428F File Offset: 0x0000248F
		internal virtual MyExtraButton PanBack { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00004298 File Offset: 0x00002498
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x000272D8 File Offset: 0x000254D8
		internal virtual Border PanClick
		{
			[CompilerGenerated]
			get
			{
				return this.m_IssuerBroadcaster;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.Button_LeftMouseUp);
				MouseButtonEventHandler value3 = new MouseButtonEventHandler(this.Button_RightMouseUp);
				MouseButtonEventHandler value4 = new MouseButtonEventHandler(this.Button_LeftMouseDown);
				MouseButtonEventHandler value5 = new MouseButtonEventHandler(this.Button_RightMouseDown);
				MouseButtonEventHandler value6 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.Button_LeftMouseUp();
				};
				MouseButtonEventHandler value7 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.Button_RightMouseUp();
				};
				MouseEventHandler value8 = delegate(object sender, MouseEventArgs e)
				{
					this.Button_MouseLeave();
				};
				MouseEventHandler value9 = delegate(object sender, MouseEventArgs e)
				{
					this.RefreshColor();
				};
				MouseEventHandler value10 = delegate(object sender, MouseEventArgs e)
				{
					this.RefreshColor();
				};
				Border issuerBroadcaster = this.m_IssuerBroadcaster;
				if (issuerBroadcaster != null)
				{
					issuerBroadcaster.MouseLeftButtonUp -= value2;
					issuerBroadcaster.MouseRightButtonUp -= value3;
					issuerBroadcaster.MouseLeftButtonDown -= value4;
					issuerBroadcaster.MouseRightButtonDown -= value5;
					issuerBroadcaster.MouseLeftButtonUp -= value6;
					issuerBroadcaster.MouseRightButtonUp -= value7;
					issuerBroadcaster.MouseLeave -= value8;
					issuerBroadcaster.MouseEnter -= value9;
					issuerBroadcaster.MouseLeave -= value10;
				}
				this.m_IssuerBroadcaster = value;
				issuerBroadcaster = this.m_IssuerBroadcaster;
				if (issuerBroadcaster != null)
				{
					issuerBroadcaster.MouseLeftButtonUp += value2;
					issuerBroadcaster.MouseRightButtonUp += value3;
					issuerBroadcaster.MouseLeftButtonDown += value4;
					issuerBroadcaster.MouseRightButtonDown += value5;
					issuerBroadcaster.MouseLeftButtonUp += value6;
					issuerBroadcaster.MouseRightButtonUp += value7;
					issuerBroadcaster.MouseLeave += value8;
					issuerBroadcaster.MouseEnter += value9;
					issuerBroadcaster.MouseLeave += value10;
				}
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x000042A0 File Offset: 0x000024A0
		// (set) Token: 0x060003BA RID: 954 RVA: 0x000042A8 File Offset: 0x000024A8
		internal virtual Grid PanScale { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060003BB RID: 955 RVA: 0x000042B1 File Offset: 0x000024B1
		// (set) Token: 0x060003BC RID: 956 RVA: 0x000042B9 File Offset: 0x000024B9
		internal virtual Border PanColor { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060003BD RID: 957 RVA: 0x000042C2 File Offset: 0x000024C2
		// (set) Token: 0x060003BE RID: 958 RVA: 0x000042CA File Offset: 0x000024CA
		internal virtual Border PanProgress { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060003BF RID: 959 RVA: 0x000042D3 File Offset: 0x000024D3
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x000042DB File Offset: 0x000024DB
		internal virtual RectangleGeometry RectProgress { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x000042E4 File Offset: 0x000024E4
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x000042EC File Offset: 0x000024EC
		internal virtual Path Path { get; set; }

		// Token: 0x060003C3 RID: 963 RVA: 0x00027418 File Offset: 0x00025618
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.systemBroadcaster)
			{
				this.systemBroadcaster = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/myextrabutton.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00027448 File Offset: 0x00025648
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (MyExtraButton)target;
				return;
			}
			if (connectionId == 2)
			{
				this.PanClick = (Border)target;
				return;
			}
			if (connectionId == 3)
			{
				this.PanScale = (Grid)target;
				return;
			}
			if (connectionId == 4)
			{
				this.PanColor = (Border)target;
				return;
			}
			if (connectionId == 5)
			{
				this.PanProgress = (Border)target;
				return;
			}
			if (connectionId == 6)
			{
				this.RectProgress = (RectangleGeometry)target;
				return;
			}
			if (connectionId == 7)
			{
				this.Path = (Path)target;
				return;
			}
			this.systemBroadcaster = true;
		}

		// Token: 0x04000221 RID: 545
		[CompilerGenerated]
		private MyExtraButton.ClickEventHandler threadBroadcaster;

		// Token: 0x04000222 RID: 546
		[CompilerGenerated]
		private MyExtraButton.RightClickEventHandler _PropertyBroadcaster;

		// Token: 0x04000223 RID: 547
		private double _ComposerBroadcaster;

		// Token: 0x04000224 RID: 548
		public int m_IteratorBroadcaster;

		// Token: 0x04000225 RID: 549
		private string repositoryBroadcaster;

		// Token: 0x04000226 RID: 550
		private double errorBroadcaster;

		// Token: 0x04000227 RID: 551
		private bool m_ContextBroadcaster;

		// Token: 0x04000228 RID: 552
		public MyExtraButton.ShowCheckDelegate _SpecificationBroadcaster;

		// Token: 0x04000229 RID: 553
		private bool mockBroadcaster;

		// Token: 0x0400022A RID: 554
		private bool requestBroadcaster;

		// Token: 0x0400022B RID: 555
		private bool m_DicBroadcaster;

		// Token: 0x0400022C RID: 556
		[CompilerGenerated]
		[AccessedThroughProperty("PanBack")]
		private MyExtraButton _HelperBroadcaster;

		// Token: 0x0400022D RID: 557
		[AccessedThroughProperty("PanClick")]
		[CompilerGenerated]
		private Border m_IssuerBroadcaster;

		// Token: 0x0400022E RID: 558
		[AccessedThroughProperty("PanScale")]
		[CompilerGenerated]
		private Grid m_IndexerBroadcaster;

		// Token: 0x0400022F RID: 559
		[CompilerGenerated]
		[AccessedThroughProperty("PanColor")]
		private Border m_InterpreterBroadcaster;

		// Token: 0x04000230 RID: 560
		[AccessedThroughProperty("PanProgress")]
		[CompilerGenerated]
		private Border serializerBroadcaster;

		// Token: 0x04000231 RID: 561
		[CompilerGenerated]
		[AccessedThroughProperty("RectProgress")]
		private RectangleGeometry _WatcherBroadcaster;

		// Token: 0x04000232 RID: 562
		[AccessedThroughProperty("Path")]
		[CompilerGenerated]
		private Path _IdentifierBroadcaster;

		// Token: 0x04000233 RID: 563
		private bool systemBroadcaster;

		// Token: 0x0200008E RID: 142
		// (Invoke) Token: 0x060003D1 RID: 977
		public delegate void ClickEventHandler(object sender, MouseButtonEventArgs e);

		// Token: 0x0200008F RID: 143
		// (Invoke) Token: 0x060003D6 RID: 982
		public delegate void RightClickEventHandler(object sender, MouseButtonEventArgs e);

		// Token: 0x02000090 RID: 144
		// (Invoke) Token: 0x060003DB RID: 987
		public delegate bool ShowCheckDelegate();
	}
}
