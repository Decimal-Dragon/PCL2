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
	// Token: 0x0200004E RID: 78
	[DesignerGenerated]
	public class MyIconTextButton : Border, IComponentConnector
	{
		// Token: 0x060001AF RID: 431 RVA: 0x000147B0 File Offset: 0x000129B0
		public MyIconTextButton()
		{
			base.MouseLeftButtonUp += delegate(object sender, MouseButtonEventArgs e)
			{
				this.MyIconTextButton_MouseUp();
			};
			base.MouseLeftButtonDown += delegate(object sender, MouseButtonEventArgs e)
			{
				this.MyIconTextButton_MouseDown();
			};
			base.MouseLeave += delegate(object sender, MouseEventArgs e)
			{
				this.MyIconTextButton_MouseLeave();
			};
			base.MouseEnter += new MouseEventHandler(this.RefreshColor);
			base.Loaded += new RoutedEventHandler(this.RefreshColor);
			base.IsEnabledChanged += delegate(object sender, DependencyPropertyChangedEventArgs e)
			{
				this.RefreshColor(RuntimeHelpers.GetObjectValue(sender), e);
			};
			this._Attribute = ModBase.GetUuid();
			this.annotation = 1.0;
			this._Merchant = false;
			this.InitializeComponent();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00014858 File Offset: 0x00012A58
		[CompilerGenerated]
		public void ResetBroadcaster(MyIconTextButton.CheckEventHandler obj)
		{
			MyIconTextButton.CheckEventHandler checkEventHandler = this._Code;
			MyIconTextButton.CheckEventHandler checkEventHandler2;
			do
			{
				checkEventHandler2 = checkEventHandler;
				MyIconTextButton.CheckEventHandler value = (MyIconTextButton.CheckEventHandler)Delegate.Combine(checkEventHandler2, obj);
				checkEventHandler = Interlocked.CompareExchange<MyIconTextButton.CheckEventHandler>(ref this._Code, value, checkEventHandler2);
			}
			while (checkEventHandler != checkEventHandler2);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00014890 File Offset: 0x00012A90
		[CompilerGenerated]
		public void InsertBroadcaster(MyIconTextButton.CheckEventHandler obj)
		{
			MyIconTextButton.CheckEventHandler checkEventHandler = this._Code;
			MyIconTextButton.CheckEventHandler checkEventHandler2;
			do
			{
				checkEventHandler2 = checkEventHandler;
				MyIconTextButton.CheckEventHandler value = (MyIconTextButton.CheckEventHandler)Delegate.Remove(checkEventHandler2, obj);
				checkEventHandler = Interlocked.CompareExchange<MyIconTextButton.CheckEventHandler>(ref this._Code, value, checkEventHandler2);
			}
			while (checkEventHandler != checkEventHandler2);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000148C8 File Offset: 0x00012AC8
		[CompilerGenerated]
		public void ValidateBroadcaster(MyIconTextButton.ChangeEventHandler obj)
		{
			MyIconTextButton.ChangeEventHandler changeEventHandler = this.m_Prototype;
			MyIconTextButton.ChangeEventHandler changeEventHandler2;
			do
			{
				changeEventHandler2 = changeEventHandler;
				MyIconTextButton.ChangeEventHandler value = (MyIconTextButton.ChangeEventHandler)Delegate.Combine(changeEventHandler2, obj);
				changeEventHandler = Interlocked.CompareExchange<MyIconTextButton.ChangeEventHandler>(ref this.m_Prototype, value, changeEventHandler2);
			}
			while (changeEventHandler != changeEventHandler2);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00014900 File Offset: 0x00012B00
		[CompilerGenerated]
		public void DefineBroadcaster(MyIconTextButton.ChangeEventHandler obj)
		{
			MyIconTextButton.ChangeEventHandler changeEventHandler = this.m_Prototype;
			MyIconTextButton.ChangeEventHandler changeEventHandler2;
			do
			{
				changeEventHandler2 = changeEventHandler;
				MyIconTextButton.ChangeEventHandler value = (MyIconTextButton.ChangeEventHandler)Delegate.Remove(changeEventHandler2, obj);
				changeEventHandler = Interlocked.CompareExchange<MyIconTextButton.ChangeEventHandler>(ref this.m_Prototype, value, changeEventHandler2);
			}
			while (changeEventHandler != changeEventHandler2);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00014938 File Offset: 0x00012B38
		public void RaiseChange()
		{
			MyIconTextButton.ChangeEventHandler prototype = this.m_Prototype;
			if (prototype != null)
			{
				prototype(this, false);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00003045 File Offset: 0x00001245
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00003057 File Offset: 0x00001257
		public string Logo
		{
			get
			{
				return this.ShapeLogo.Data.ToString();
			}
			set
			{
				this.ShapeLogo.Data = (Geometry)new GeometryConverter().ConvertFromString(value);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00003074 File Offset: 0x00001274
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x0000307C File Offset: 0x0000127C
		public double LogoScale
		{
			get
			{
				return this.annotation;
			}
			set
			{
				this.annotation = value;
				if (!Information.IsNothing(this.ShapeLogo))
				{
					this.ShapeLogo.RenderTransform = new ScaleTransform
					{
						ScaleX = this.LogoScale,
						ScaleY = this.LogoScale
					};
				}
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000030BA File Offset: 0x000012BA
		// (set) Token: 0x060001BA RID: 442 RVA: 0x000030CC File Offset: 0x000012CC
		public string Text
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyIconTextButton.m_Adapter));
			}
			set
			{
				base.SetValue(MyIconTextButton.m_Adapter, value);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000030DA File Offset: 0x000012DA
		// (set) Token: 0x060001BC RID: 444 RVA: 0x000030EC File Offset: 0x000012EC
		public MyIconTextButton.ColorState ColorType
		{
			get
			{
				return (MyIconTextButton.ColorState)Conversions.ToInteger(base.GetValue(MyIconTextButton.facade));
			}
			set
			{
				if (this.ColorType != value)
				{
					base.SetValue(MyIconTextButton.facade, value);
					this.RefreshColor(null, null);
				}
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060001BD RID: 445 RVA: 0x00014958 File Offset: 0x00012B58
		// (remove) Token: 0x060001BE RID: 446 RVA: 0x00014990 File Offset: 0x00012B90
		public event MyIconTextButton.ClickEventHandler Click
		{
			[CompilerGenerated]
			add
			{
				MyIconTextButton.ClickEventHandler clickEventHandler = this.list;
				MyIconTextButton.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MyIconTextButton.ClickEventHandler value2 = (MyIconTextButton.ClickEventHandler)Delegate.Combine(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MyIconTextButton.ClickEventHandler>(ref this.list, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				MyIconTextButton.ClickEventHandler clickEventHandler = this.list;
				MyIconTextButton.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MyIconTextButton.ClickEventHandler value2 = (MyIconTextButton.ClickEventHandler)Delegate.Remove(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MyIconTextButton.ClickEventHandler>(ref this.list, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000149C8 File Offset: 0x00012BC8
		private void MyIconTextButton_MouseUp()
		{
			if (this._Merchant)
			{
				ModBase.Log("[Control] 按下带图标按钮：" + this.Text, ModBase.LogLevel.Normal, "出现错误");
				this._Merchant = false;
				MyIconTextButton.ClickEventHandler clickEventHandler = this.list;
				if (clickEventHandler != null)
				{
					clickEventHandler(this, new ModBase.RouteEventArgs(true));
				}
				ModEvent.TryStartEvent(this.EventType, this.EventData);
				this.RefreshColor(null, null);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00003110 File Offset: 0x00001310
		private void MyIconTextButton_MouseDown()
		{
			this._Merchant = true;
			this.RefreshColor(null, null);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00003121 File Offset: 0x00001321
		private void MyIconTextButton_MouseLeave()
		{
			this._Merchant = false;
			this.RefreshColor(null, null);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00003132 File Offset: 0x00001332
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00003144 File Offset: 0x00001344
		public string EventType
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyIconTextButton.m_Authentication));
			}
			set
			{
				base.SetValue(MyIconTextButton.m_Authentication, value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00003152 File Offset: 0x00001352
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00003164 File Offset: 0x00001364
		public string EventData
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyIconTextButton.algo));
			}
			set
			{
				base.SetValue(MyIconTextButton.algo, value);
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00014A30 File Offset: 0x00012C30
		private void RefreshColor(object obj = null, object e = null)
		{
			try
			{
				if (base.IsLoaded && ModAnimation.CalcParser() == 0 && !false.Equals(RuntimeHelpers.GetObjectValue(e)))
				{
					MyIconTextButton.ColorState colorType = this.ColorType;
					if (colorType != MyIconTextButton.ColorState.Black)
					{
						if (colorType == MyIconTextButton.ColorState.Highlight)
						{
							if (this._Merchant)
							{
								ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, "ColorBrush6", 70, 0, null, false), "MyIconTextButton Color " + Conversions.ToString(this._Attribute), false);
							}
							else if (base.IsMouseOver)
							{
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaColor(this.ShapeLogo, Shape.FillProperty, "ColorBrush3", 100, 0, null, false),
									ModAnimation.AaColor(this.LabText, TextBlock.ForegroundProperty, "ColorBrush3", 100, 0, null, false)
								}, "MyIconTextButton Checked " + Conversions.ToString(this._Attribute), false);
								ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, "ColorBrushBg1", 100, 0, null, false), "MyIconTextButton Color " + Conversions.ToString(this._Attribute), false);
							}
							else if (base.IsEnabled)
							{
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaColor(this.ShapeLogo, Shape.FillProperty, "ColorBrush3", 150, 0, null, false),
									ModAnimation.AaColor(this.LabText, TextBlock.ForegroundProperty, "ColorBrush3", 150, 0, null, false)
								}, "MyIconTextButton Checked " + Conversions.ToString(this._Attribute), false);
								ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, ModSecret._ConsumerField - base.Background, 150, 0, null, false), "MyIconTextButton Color " + Conversions.ToString(this._Attribute), false);
							}
							else
							{
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaColor(this.ShapeLogo, Shape.FillProperty, "ColorBrushGray5", 100, 0, null, false),
									ModAnimation.AaColor(this.LabText, TextBlock.ForegroundProperty, "ColorBrushGray5", 100, 0, null, false)
								}, "MyIconTextButton Checked " + Conversions.ToString(this._Attribute), false);
								ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, ModSecret._ConsumerField - base.Background, 150, 0, null, false), "MyIconTextButton Color " + Conversions.ToString(this._Attribute), false);
							}
						}
					}
					else if (this._Merchant)
					{
						ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, "ColorBrush6", 70, 0, null, false), "MyIconTextButton Color " + Conversions.ToString(this._Attribute), false);
					}
					else if (base.IsMouseOver)
					{
						ModAnimation.AniStart(new ModAnimation.AniData[]
						{
							ModAnimation.AaColor(this.ShapeLogo, Shape.FillProperty, "ColorBrush3", 100, 0, null, false),
							ModAnimation.AaColor(this.LabText, TextBlock.ForegroundProperty, "ColorBrush3", 100, 0, null, false)
						}, "MyIconTextButton Checked " + Conversions.ToString(this._Attribute), false);
						ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, "ColorBrushBg1", 100, 0, null, false), "MyIconTextButton Color " + Conversions.ToString(this._Attribute), false);
					}
					else if (base.IsEnabled)
					{
						ModAnimation.AniStart(new ModAnimation.AniData[]
						{
							ModAnimation.AaColor(this.ShapeLogo, Shape.FillProperty, "ColorBrush1", 150, 0, null, false),
							ModAnimation.AaColor(this.LabText, TextBlock.ForegroundProperty, "ColorBrush1", 150, 0, null, false)
						}, "MyIconTextButton Checked " + Conversions.ToString(this._Attribute), false);
						ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, ModSecret._ConsumerField - base.Background, 150, 0, null, false), "MyIconTextButton Color " + Conversions.ToString(this._Attribute), false);
					}
					else
					{
						ModAnimation.AniStart(new ModAnimation.AniData[]
						{
							ModAnimation.AaColor(this.ShapeLogo, Shape.FillProperty, "ColorBrushGray5", 100, 0, null, false),
							ModAnimation.AaColor(this.LabText, TextBlock.ForegroundProperty, "ColorBrushGray5", 100, 0, null, false)
						}, "MyIconTextButton Checked " + Conversions.ToString(this._Attribute), false);
						ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, ModSecret._ConsumerField - base.Background, 150, 0, null, false), "MyIconTextButton Color " + Conversions.ToString(this._Attribute), false);
					}
				}
				else
				{
					ModAnimation.AniStop("MyIconTextButton Checked " + Conversions.ToString(this._Attribute));
					ModAnimation.AniStop("MyIconTextButton Color " + Conversions.ToString(this._Attribute));
					MyIconTextButton.ColorState colorType2 = this.ColorType;
					if (colorType2 != MyIconTextButton.ColorState.Black)
					{
						if (colorType2 == MyIconTextButton.ColorState.Highlight)
						{
							base.Background = ModSecret._ConsumerField;
							this.ShapeLogo.SetResourceReference(Shape.FillProperty, base.IsEnabled ? "ColorBrush3" : "ColorBrushGray5");
							this.LabText.SetResourceReference(TextBlock.ForegroundProperty, base.IsEnabled ? "ColorBrush3" : "ColorBrushGray5");
						}
					}
					else
					{
						base.Background = ModSecret._ConsumerField;
						this.ShapeLogo.SetResourceReference(Shape.FillProperty, base.IsEnabled ? "ColorBrush1" : "ColorBrushGray5");
						this.LabText.SetResourceReference(TextBlock.ForegroundProperty, base.IsEnabled ? "ColorBrush1" : "ColorBrushGray5");
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "刷新带图标按钮颜色出错", ModBase.LogLevel.Debug, "出现错误");
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00003172 File Offset: 0x00001372
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000317A File Offset: 0x0000137A
		internal virtual MyIconTextButton PanBack { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00003183 File Offset: 0x00001383
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000318B File Offset: 0x0000138B
		internal virtual Path ShapeLogo { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00003194 File Offset: 0x00001394
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000319C File Offset: 0x0000139C
		internal virtual TextBlock LabText { get; set; }

		// Token: 0x060001CD RID: 461 RVA: 0x00015044 File Offset: 0x00013244
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_Database)
			{
				this.m_Database = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/myicontextbutton.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000031A5 File Offset: 0x000013A5
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (MyIconTextButton)target;
				return;
			}
			if (connectionId == 2)
			{
				this.ShapeLogo = (Path)target;
				return;
			}
			if (connectionId == 3)
			{
				this.LabText = (TextBlock)target;
				return;
			}
			this.m_Database = true;
		}

		// Token: 0x040000BF RID: 191
		public int _Attribute;

		// Token: 0x040000C0 RID: 192
		[CompilerGenerated]
		private MyIconTextButton.CheckEventHandler _Code;

		// Token: 0x040000C1 RID: 193
		[CompilerGenerated]
		private MyIconTextButton.ChangeEventHandler m_Prototype;

		// Token: 0x040000C2 RID: 194
		private double annotation;

		// Token: 0x040000C3 RID: 195
		public static readonly DependencyProperty m_Adapter = DependencyProperty.Register("Text", typeof(string), typeof(MyIconTextButton), new PropertyMetadata(delegate(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			if (!Information.IsNothing(sender))
			{
				((MyIconTextButton)sender).LabText.Text = Conversions.ToString(e.NewValue);
			}
		}));

		// Token: 0x040000C4 RID: 196
		public static readonly DependencyProperty facade = DependencyProperty.Register("ColorType", typeof(MyIconTextButton.ColorState), typeof(MyIconTextButton), new PropertyMetadata(MyIconTextButton.ColorState.Black));

		// Token: 0x040000C5 RID: 197
		[CompilerGenerated]
		private MyIconTextButton.ClickEventHandler list;

		// Token: 0x040000C6 RID: 198
		private bool _Merchant;

		// Token: 0x040000C7 RID: 199
		public static readonly DependencyProperty m_Authentication = DependencyProperty.Register("EventType", typeof(string), typeof(MyIconTextButton), new PropertyMetadata(null));

		// Token: 0x040000C8 RID: 200
		public static readonly DependencyProperty algo = DependencyProperty.Register("EventData", typeof(string), typeof(MyIconTextButton), new PropertyMetadata(null));

		// Token: 0x040000C9 RID: 201
		[CompilerGenerated]
		[AccessedThroughProperty("PanBack")]
		private MyIconTextButton _Comparator;

		// Token: 0x040000CA RID: 202
		[CompilerGenerated]
		[AccessedThroughProperty("ShapeLogo")]
		private Path _Mapping;

		// Token: 0x040000CB RID: 203
		[CompilerGenerated]
		[AccessedThroughProperty("LabText")]
		private TextBlock m_Tokenizer;

		// Token: 0x040000CC RID: 204
		private bool m_Database;

		// Token: 0x0200004F RID: 79
		// (Invoke) Token: 0x060001D6 RID: 470
		public delegate void CheckEventHandler(object sender, bool raiseByMouse);

		// Token: 0x02000050 RID: 80
		// (Invoke) Token: 0x060001DB RID: 475
		public delegate void ChangeEventHandler(object sender, bool raiseByMouse);

		// Token: 0x02000051 RID: 81
		public enum ColorState
		{
			// Token: 0x040000CE RID: 206
			Black,
			// Token: 0x040000CF RID: 207
			Highlight
		}

		// Token: 0x02000052 RID: 82
		// (Invoke) Token: 0x060001E0 RID: 480
		public delegate void ClickEventHandler(object sender, ModBase.RouteEventArgs e);
	}
}
