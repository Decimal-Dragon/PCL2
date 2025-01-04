using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020001C1 RID: 449
	[DesignerGenerated]
	public class MyListItem : Grid, IMyRadio, IComponentConnector
	{
		// Token: 0x060014CB RID: 5323 RVA: 0x0008BBE0 File Offset: 0x00089DE0
		public MyListItem()
		{
			base.SizeChanged += delegate(object sender, SizeChangedEventArgs e)
			{
				this.OnSizeChanged();
			};
			base.PreviewMouseLeftButtonUp += this.Button_MouseUp;
			base.PreviewMouseLeftButtonDown += this.Button_MouseDown;
			base.MouseLeave += new MouseEventHandler(this.Button_MouseLeave);
			base.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(this.Button_MouseLeave);
			base.MouseEnter += new MouseEventHandler(this.RefreshColor);
			base.MouseLeave += new MouseEventHandler(this.RefreshColor);
			base.MouseLeftButtonDown += new MouseButtonEventHandler(this.RefreshColor);
			base.MouseLeftButtonUp += new MouseButtonEventHandler(this.RefreshColor);
			base.Loaded += this.MyListItem_Loaded;
			this.m_IdentifierComposer = null;
			this.tagComposer = null;
			this._ObserverComposer = ModBase.GetUuid();
			this.stubComposer = true;
			this.MinPaddingRight = 4;
			this.stateComposer = "";
			this.callbackComposer = "";
			this.m_TemplateComposer = 1.0;
			this.SetConfig(false);
			this.taskComposer = false;
			this._ConsumerComposer = MyListItem.CheckType.None;
			this.configurationComposer = false;
			this.expressionComposer = false;
			this._ProccesorComposer = true;
			this.InitializeComponent();
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060014CC RID: 5324 RVA: 0x0008BD24 File Offset: 0x00089F24
		// (remove) Token: 0x060014CD RID: 5325 RVA: 0x0008BD5C File Offset: 0x00089F5C
		public event MyListItem.ClickEventHandler Click
		{
			[CompilerGenerated]
			add
			{
				MyListItem.ClickEventHandler clickEventHandler = this.m_IndexerComposer;
				MyListItem.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MyListItem.ClickEventHandler value2 = (MyListItem.ClickEventHandler)Delegate.Combine(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MyListItem.ClickEventHandler>(ref this.m_IndexerComposer, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				MyListItem.ClickEventHandler clickEventHandler = this.m_IndexerComposer;
				MyListItem.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MyListItem.ClickEventHandler value2 = (MyListItem.ClickEventHandler)Delegate.Remove(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MyListItem.ClickEventHandler>(ref this.m_IndexerComposer, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0008BD94 File Offset: 0x00089F94
		[CompilerGenerated]
		public void VisitConfig(MyListItem.LogoClickEventHandler obj)
		{
			MyListItem.LogoClickEventHandler logoClickEventHandler = this.interpreterComposer;
			MyListItem.LogoClickEventHandler logoClickEventHandler2;
			do
			{
				logoClickEventHandler2 = logoClickEventHandler;
				MyListItem.LogoClickEventHandler value = (MyListItem.LogoClickEventHandler)Delegate.Combine(logoClickEventHandler2, obj);
				logoClickEventHandler = Interlocked.CompareExchange<MyListItem.LogoClickEventHandler>(ref this.interpreterComposer, value, logoClickEventHandler2);
			}
			while (logoClickEventHandler != logoClickEventHandler2);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0008BDCC File Offset: 0x00089FCC
		[CompilerGenerated]
		public void CallConfig(MyListItem.LogoClickEventHandler obj)
		{
			MyListItem.LogoClickEventHandler logoClickEventHandler = this.interpreterComposer;
			MyListItem.LogoClickEventHandler logoClickEventHandler2;
			do
			{
				logoClickEventHandler2 = logoClickEventHandler;
				MyListItem.LogoClickEventHandler value = (MyListItem.LogoClickEventHandler)Delegate.Remove(logoClickEventHandler2, obj);
				logoClickEventHandler = Interlocked.CompareExchange<MyListItem.LogoClickEventHandler>(ref this.interpreterComposer, value, logoClickEventHandler2);
			}
			while (logoClickEventHandler != logoClickEventHandler2);
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060014D0 RID: 5328 RVA: 0x0008BE04 File Offset: 0x0008A004
		// (remove) Token: 0x060014D1 RID: 5329 RVA: 0x0008BE3C File Offset: 0x0008A03C
		public event IMyRadio.CheckEventHandler Check
		{
			[CompilerGenerated]
			add
			{
				IMyRadio.CheckEventHandler checkEventHandler = this.m_SerializerComposer;
				IMyRadio.CheckEventHandler checkEventHandler2;
				do
				{
					checkEventHandler2 = checkEventHandler;
					IMyRadio.CheckEventHandler value2 = (IMyRadio.CheckEventHandler)Delegate.Combine(checkEventHandler2, value);
					checkEventHandler = Interlocked.CompareExchange<IMyRadio.CheckEventHandler>(ref this.m_SerializerComposer, value2, checkEventHandler2);
				}
				while (checkEventHandler != checkEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				IMyRadio.CheckEventHandler checkEventHandler = this.m_SerializerComposer;
				IMyRadio.CheckEventHandler checkEventHandler2;
				do
				{
					checkEventHandler2 = checkEventHandler;
					IMyRadio.CheckEventHandler value2 = (IMyRadio.CheckEventHandler)Delegate.Remove(checkEventHandler2, value);
					checkEventHandler = Interlocked.CompareExchange<IMyRadio.CheckEventHandler>(ref this.m_SerializerComposer, value2, checkEventHandler2);
				}
				while (checkEventHandler != checkEventHandler2);
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060014D2 RID: 5330 RVA: 0x0008BE74 File Offset: 0x0008A074
		// (remove) Token: 0x060014D3 RID: 5331 RVA: 0x0008BEAC File Offset: 0x0008A0AC
		public event IMyRadio.ChangedEventHandler Changed
		{
			[CompilerGenerated]
			add
			{
				IMyRadio.ChangedEventHandler changedEventHandler = this._WatcherComposer;
				IMyRadio.ChangedEventHandler changedEventHandler2;
				do
				{
					changedEventHandler2 = changedEventHandler;
					IMyRadio.ChangedEventHandler value2 = (IMyRadio.ChangedEventHandler)Delegate.Combine(changedEventHandler2, value);
					changedEventHandler = Interlocked.CompareExchange<IMyRadio.ChangedEventHandler>(ref this._WatcherComposer, value2, changedEventHandler2);
				}
				while (changedEventHandler != changedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				IMyRadio.ChangedEventHandler changedEventHandler = this._WatcherComposer;
				IMyRadio.ChangedEventHandler changedEventHandler2;
				do
				{
					changedEventHandler2 = changedEventHandler;
					IMyRadio.ChangedEventHandler value2 = (IMyRadio.ChangedEventHandler)Delegate.Remove(changedEventHandler2, value);
					changedEventHandler = Interlocked.CompareExchange<IMyRadio.ChangedEventHandler>(ref this._WatcherComposer, value2, changedEventHandler2);
				}
				while (changedEventHandler != changedEventHandler2);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x0008BEE4 File Offset: 0x0008A0E4
		public Border RectBack
		{
			get
			{
				if (this.m_IdentifierComposer == null)
				{
					Border border = new Border
					{
						Name = "RectBack",
						CornerRadius = new CornerRadius((double)((this.IsScaleAnimationEnabled || base.Height > 40.0) ? 6 : 0)),
						RenderTransform = (this.IsScaleAnimationEnabled ? new ScaleTransform(0.8, 0.8) : null),
						RenderTransformOrigin = new Point(0.5, 0.5),
						BorderThickness = new Thickness(ModBase.smethod_4(1.0)),
						SnapsToDevicePixels = true,
						IsHitTestVisible = false,
						Opacity = 0.0
					};
					border.SetResourceReference(Border.BackgroundProperty, "ColorBrush7");
					border.SetResourceReference(Border.BorderBrushProperty, "ColorBrush6");
					Grid.SetColumnSpan(border, 999);
					Grid.SetRowSpan(border, 999);
					base.Children.Insert(0, border);
					this.m_IdentifierComposer = border;
				}
				return this.m_IdentifierComposer;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x0008C004 File Offset: 0x0008A204
		public TextBlock LabInfo
		{
			get
			{
				if (this.tagComposer == null)
				{
					TextBlock element = new TextBlock
					{
						Name = "LabInfo",
						SnapsToDevicePixels = false,
						UseLayoutRounding = false,
						HorizontalAlignment = HorizontalAlignment.Left,
						IsHitTestVisible = false,
						TextTrimming = TextTrimming.CharacterEllipsis,
						Visibility = Visibility.Collapsed,
						FontSize = 12.0,
						Margin = new Thickness(4.0, 0.0, 0.0, 0.0),
						Opacity = 0.6
					};
					Grid.SetColumn(element, 3);
					Grid.SetRow(element, 2);
					base.Children.Add(element);
					this.tagComposer = element;
				}
				return this.tagComposer;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x0000BC9C File Offset: 0x00009E9C
		// (set) Token: 0x060014D7 RID: 5335 RVA: 0x0000BCA4 File Offset: 0x00009EA4
		public bool IsScaleAnimationEnabled
		{
			get
			{
				return this.stubComposer;
			}
			set
			{
				this.stubComposer = value;
				if (this.m_IdentifierComposer != null)
				{
					this.RectBack.CornerRadius = new CornerRadius((double)(value ? 6 : 0));
				}
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x0008C0CC File Offset: 0x0008A2CC
		// (set) Token: 0x060014D9 RID: 5337 RVA: 0x0000BCCD File Offset: 0x00009ECD
		public int PaddingLeft
		{
			get
			{
				return checked((int)Math.Round(this.ColumnPaddingLeft.Width.Value));
			}
			set
			{
				this.ColumnPaddingLeft.Width = new GridLength((double)value);
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x0000BCE1 File Offset: 0x00009EE1
		// (set) Token: 0x060014DB RID: 5339 RVA: 0x0000BCE9 File Offset: 0x00009EE9
		public int MinPaddingRight { get; set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x0000BCF2 File Offset: 0x00009EF2
		// (set) Token: 0x060014DD RID: 5341 RVA: 0x0008C0F4 File Offset: 0x0008A2F4
		public IEnumerable<MyIconButton> Buttons
		{
			get
			{
				return this.m_RefComposer;
			}
			set
			{
				this.m_RefComposer = value;
				if (this.systemComposer != null)
				{
					base.Children.Remove(this.systemComposer);
					this.systemComposer = null;
				}
				int num = Enumerable.Count<MyIconButton>(value);
				if (num != 0)
				{
					if (num == 1)
					{
						try
						{
							foreach (MyIconButton myIconButton in value)
							{
								if (myIconButton.Height.Equals(double.NaN))
								{
									myIconButton.Height = 25.0;
								}
								if (myIconButton.Width.Equals(double.NaN))
								{
									myIconButton.Width = 25.0;
								}
								MyIconButton myIconButton2 = myIconButton;
								myIconButton2.Opacity = 0.0;
								myIconButton2.Margin = new Thickness(0.0, 0.0, 5.0, 0.0);
								myIconButton2.SnapsToDevicePixels = false;
								myIconButton2.HorizontalAlignment = HorizontalAlignment.Right;
								myIconButton2.VerticalAlignment = VerticalAlignment.Center;
								myIconButton2.SnapsToDevicePixels = false;
								myIconButton2.UseLayoutRounding = false;
								Grid.SetColumnSpan(myIconButton, 10);
								Grid.SetRowSpan(myIconButton, 10);
								base.Children.Add(myIconButton);
								this.systemComposer = myIconButton;
							}
							return;
						}
						finally
						{
							IEnumerator<MyIconButton> enumerator;
							if (enumerator != null)
							{
								enumerator.Dispose();
							}
						}
					}
					this.systemComposer = new StackPanel
					{
						Opacity = 0.0,
						Margin = new Thickness(0.0, 0.0, 5.0, 0.0),
						SnapsToDevicePixels = false,
						Orientation = Orientation.Horizontal,
						HorizontalAlignment = HorizontalAlignment.Right,
						VerticalAlignment = VerticalAlignment.Center,
						UseLayoutRounding = false
					};
					Grid.SetColumnSpan(this.systemComposer, 10);
					Grid.SetRowSpan(this.systemComposer, 10);
					try
					{
						foreach (MyIconButton myIconButton3 in value)
						{
							if (myIconButton3.Height.Equals(double.NaN))
							{
								myIconButton3.Height = 25.0;
							}
							if (myIconButton3.Width.Equals(double.NaN))
							{
								myIconButton3.Width = 25.0;
							}
							((StackPanel)this.systemComposer).Children.Add(myIconButton3);
						}
					}
					finally
					{
						IEnumerator<MyIconButton> enumerator2;
						if (enumerator2 != null)
						{
							enumerator2.Dispose();
						}
					}
					base.Children.Add(this.systemComposer);
				}
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x0000BCFA File Offset: 0x00009EFA
		// (set) Token: 0x060014DF RID: 5343 RVA: 0x0000BD0C File Offset: 0x00009F0C
		public string Title
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyListItem._DecoratorComposer));
			}
			set
			{
				base.SetValue(MyListItem._DecoratorComposer, value);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x0000BD1A File Offset: 0x00009F1A
		// (set) Token: 0x060014E1 RID: 5345 RVA: 0x0000BD2C File Offset: 0x00009F2C
		public double FontSize
		{
			get
			{
				return Conversions.ToDouble(base.GetValue(MyListItem.instanceComposer));
			}
			set
			{
				base.SetValue(MyListItem.instanceComposer, value);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x0000BD3F File Offset: 0x00009F3F
		// (set) Token: 0x060014E3 RID: 5347 RVA: 0x0008C38C File Offset: 0x0008A58C
		public string Info
		{
			get
			{
				return this.stateComposer;
			}
			set
			{
				if (Operators.CompareString(this.stateComposer, value, false) != 0)
				{
					value = value.Replace("\r", "").Replace("\n", "");
					this.stateComposer = value;
					this.LabInfo.Text = value;
					this.LabInfo.Visibility = ((Operators.CompareString(value, "", false) == 0) ? Visibility.Collapsed : Visibility.Visible);
				}
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x0000BD47 File Offset: 0x00009F47
		// (set) Token: 0x060014E5 RID: 5349 RVA: 0x0008C3FC File Offset: 0x0008A5FC
		public string Logo
		{
			get
			{
				return this.callbackComposer;
			}
			set
			{
				if (Operators.CompareString(this.callbackComposer, value, false) != 0)
				{
					this.callbackComposer = value;
					if (!Information.IsNothing(this.PathLogo))
					{
						base.Children.Remove(this.PathLogo);
					}
					if (Operators.CompareString(this.callbackComposer, "", false) != 0)
					{
						if (this.callbackComposer.StartsWithF("http", true))
						{
							this.PathLogo = new MyImage
							{
								Tag = this,
								IsHitTestVisible = this.ManageConfig(),
								Source = this.callbackComposer,
								RenderTransformOrigin = new Point(0.5, 0.5),
								RenderTransform = new ScaleTransform
								{
									ScaleX = this.LogoScale,
									ScaleY = this.LogoScale
								},
								SnapsToDevicePixels = true,
								UseLayoutRounding = false
							};
							RenderOptions.SetBitmapScalingMode(this.PathLogo, BitmapScalingMode.LowQuality);
						}
						else if (!this.callbackComposer.EndsWithF(".png", true) && !this.callbackComposer.EndsWithF(".jpg", true) && !this.callbackComposer.EndsWithF(".webp", true))
						{
							this.PathLogo = new Path
							{
								Tag = this,
								IsHitTestVisible = this.ManageConfig(),
								HorizontalAlignment = HorizontalAlignment.Center,
								VerticalAlignment = VerticalAlignment.Center,
								Stretch = Stretch.Uniform,
								Data = (Geometry)new GeometryConverter().ConvertFromString(this.callbackComposer),
								RenderTransformOrigin = new Point(0.5, 0.5),
								RenderTransform = new ScaleTransform
								{
									ScaleX = this.LogoScale,
									ScaleY = this.LogoScale
								},
								SnapsToDevicePixels = false,
								UseLayoutRounding = false
							};
							this.PathLogo.SetBinding(Shape.FillProperty, new Binding("Foreground")
							{
								Source = this
							});
						}
						else
						{
							this.PathLogo = new Canvas
							{
								Tag = this,
								IsHitTestVisible = this.ManageConfig(),
								Background = new MyBitmap(this.callbackComposer),
								RenderTransformOrigin = new Point(0.5, 0.5),
								RenderTransform = new ScaleTransform
								{
									ScaleX = this.LogoScale,
									ScaleY = this.LogoScale
								},
								SnapsToDevicePixels = true,
								UseLayoutRounding = false,
								HorizontalAlignment = HorizontalAlignment.Stretch,
								VerticalAlignment = VerticalAlignment.Stretch
							};
							RenderOptions.SetBitmapScalingMode(this.PathLogo, BitmapScalingMode.LowQuality);
						}
						Grid.SetColumn(this.PathLogo, 2);
						Grid.SetRowSpan(this.PathLogo, 4);
						this.OnSizeChanged();
						base.Children.Add(this.PathLogo);
						if (this.ManageConfig())
						{
							this.PathLogo.MouseLeave += delegate(object sender, MouseEventArgs e)
							{
								this.taskComposer = false;
							};
							this.PathLogo.MouseLeftButtonDown += delegate(object sender, MouseButtonEventArgs e)
							{
								this.taskComposer = true;
							};
							this.PathLogo.MouseLeftButtonUp += delegate(object sender, MouseButtonEventArgs e)
							{
								if (this.taskComposer)
								{
									this.taskComposer = false;
									MyListItem.LogoClickEventHandler logoClickEventHandler = this.interpreterComposer;
									if (logoClickEventHandler != null)
									{
										logoClickEventHandler(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)), e);
									}
								}
							};
						}
					}
					this.ColumnLogo.Width = new GridLength((double)(checked(((Operators.CompareString(this.callbackComposer, "", false) == 0) ? 0 : 34) + ((base.Height < 40.0) ? 0 : 4))));
				}
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x0000BD4F File Offset: 0x00009F4F
		// (set) Token: 0x060014E7 RID: 5351 RVA: 0x0000BD57 File Offset: 0x00009F57
		public double LogoScale
		{
			get
			{
				return this.m_TemplateComposer;
			}
			set
			{
				this.m_TemplateComposer = value;
				if (!Information.IsNothing(this.PathLogo))
				{
					this.PathLogo.RenderTransform = new ScaleTransform
					{
						ScaleX = this.LogoScale,
						ScaleY = this.LogoScale
					};
				}
			}
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0000BD95 File Offset: 0x00009F95
		[CompilerGenerated]
		public bool ManageConfig()
		{
			return this._MethodComposer;
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0000BD9D File Offset: 0x00009F9D
		[CompilerGenerated]
		public void SetConfig(bool AutoPropertyValue)
		{
			this._MethodComposer = AutoPropertyValue;
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x0000BDA6 File Offset: 0x00009FA6
		// (set) Token: 0x060014EB RID: 5355 RVA: 0x0008C754 File Offset: 0x0008A954
		public MyListItem.CheckType Type
		{
			get
			{
				return this._ConsumerComposer;
			}
			set
			{
				if (this._ConsumerComposer != value)
				{
					this._ConsumerComposer = value;
					this.ColumnCheck.Width = new GridLength((double)((this._ConsumerComposer == MyListItem.CheckType.None || this._ConsumerComposer == MyListItem.CheckType.Clickable) ? ((base.Height < 40.0) ? 4 : 2) : 6));
					if (this._ConsumerComposer != MyListItem.CheckType.None)
					{
						if (this._ConsumerComposer != MyListItem.CheckType.Clickable)
						{
							if (Information.IsNothing(this.m_ParamComposer))
							{
								this.m_ParamComposer = new Border
								{
									Width = 5.0,
									Height = (this.Checked ? double.NaN : 0.0),
									CornerRadius = new CornerRadius(2.0, 2.0, 2.0, 2.0),
									VerticalAlignment = (this.Checked ? VerticalAlignment.Stretch : VerticalAlignment.Center),
									HorizontalAlignment = HorizontalAlignment.Left,
									UseLayoutRounding = false,
									SnapsToDevicePixels = false,
									Margin = (this.Checked ? new Thickness(-1.0, 6.0, 0.0, 6.0) : new Thickness(-1.0, 0.0, 0.0, 0.0))
								};
								this.m_ParamComposer.SetResourceReference(Border.BackgroundProperty, "ColorBrush3");
								Grid.SetRowSpan(this.m_ParamComposer, 4);
								base.Children.Add(this.m_ParamComposer);
								return;
							}
							return;
						}
					}
					if (!Information.IsNothing(this.m_ParamComposer))
					{
						base.Children.Remove(this.m_ParamComposer);
						this.m_ParamComposer = null;
					}
					this.SetChecked(false, false, false);
					return;
				}
			}
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0008C930 File Offset: 0x0008AB30
		private void OnSizeChanged()
		{
			this.ColumnCheck.Width = new GridLength((double)((this._ConsumerComposer == MyListItem.CheckType.None || this._ConsumerComposer == MyListItem.CheckType.Clickable) ? ((base.Height < 40.0) ? 4 : 2) : 6));
			this.ColumnLogo.Width = new GridLength((double)(checked(((Operators.CompareString(this.callbackComposer, "", false) == 0) ? 0 : 34) + ((base.Height < 40.0) ? 0 : 4))));
			if (this.PathLogo != null)
			{
				if (!this.callbackComposer.EndsWithF(".png", true) && !this.callbackComposer.EndsWithF(".jpg", true) && !this.callbackComposer.EndsWithF(".webp", true))
				{
					this.PathLogo.Margin = new Thickness((double)((base.Height < 40.0) ? 6 : 8), 8.0, (double)((base.Height < 40.0) ? 4 : 6), 8.0);
				}
				else
				{
					this.PathLogo.Margin = new Thickness(4.0, 5.0, 3.0, 5.0);
				}
			}
			this.LabTitle.Margin = new Thickness(4.0, 0.0, 0.0, (double)((base.Height < 40.0) ? 0 : 2));
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x0000BDAE File Offset: 0x00009FAE
		// (set) Token: 0x060014EE RID: 5358 RVA: 0x0000BDB6 File Offset: 0x00009FB6
		public bool Checked
		{
			get
			{
				return this.configurationComposer;
			}
			set
			{
				this.SetChecked(value, false, true);
			}
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0008CAC0 File Offset: 0x0008ACC0
		public void SetChecked(bool value, bool user, bool anime)
		{
			try
			{
				ModBase.RouteEventArgs routeEventArgs = new ModBase.RouteEventArgs(user);
				bool flag = this.configurationComposer;
				if (this.Type == MyListItem.CheckType.RadioBox)
				{
					if (base.IsInitialized && value != this.configurationComposer)
					{
						this.configurationComposer = value;
						IMyRadio.ChangedEventHandler watcherComposer = this._WatcherComposer;
						if (watcherComposer != null)
						{
							watcherComposer(this, routeEventArgs);
						}
						if (routeEventArgs.m_SerializerError)
						{
							this.configurationComposer = flag;
							return;
						}
					}
					this.configurationComposer = value;
				}
				else
				{
					if (value == this.configurationComposer)
					{
						return;
					}
					this.configurationComposer = value;
					if (base.IsInitialized)
					{
						IMyRadio.ChangedEventHandler watcherComposer2 = this._WatcherComposer;
						if (watcherComposer2 != null)
						{
							watcherComposer2(this, routeEventArgs);
						}
						if (routeEventArgs.m_SerializerError)
						{
							this.configurationComposer = flag;
							return;
						}
					}
				}
				if (value)
				{
					ModBase.RouteEventArgs routeEventArgs2 = new ModBase.RouteEventArgs(user);
					IMyRadio.CheckEventHandler serializerComposer = this.m_SerializerComposer;
					if (serializerComposer != null)
					{
						serializerComposer(this, routeEventArgs2);
					}
					if (routeEventArgs2.m_SerializerError)
					{
						return;
					}
				}
				checked
				{
					if (this.Type == MyListItem.CheckType.RadioBox)
					{
						if (Information.IsNothing(base.Parent))
						{
							return;
						}
						List<MyListItem> list = new List<MyListItem>();
						int num = 0;
						try
						{
							foreach (object obj in ((IEnumerable)NewLateBinding.LateGet(base.Parent, null, "Children", new object[0], null, null, null)))
							{
								object objectValue = RuntimeHelpers.GetObjectValue(obj);
								if (objectValue is MyListItem && ((MyListItem)objectValue).Type == MyListItem.CheckType.RadioBox)
								{
									list.Add((MyListItem)objectValue);
									if (Conversions.ToBoolean(NewLateBinding.LateGet(objectValue, null, "Checked", new object[0], null, null, null)))
									{
										num++;
									}
								}
							}
						}
						finally
						{
							IEnumerator enumerator;
							if (enumerator is IDisposable)
							{
								(enumerator as IDisposable).Dispose();
							}
						}
						int num2 = num;
						if (num2 == 0)
						{
							list[0].Checked = true;
						}
						else if (num2 > 1)
						{
							if (this.Checked)
							{
								try
								{
									foreach (MyListItem myListItem in list)
									{
										if (myListItem.Checked && !myListItem.Equals(this))
										{
											myListItem.Checked = false;
										}
									}
									goto IL_255;
								}
								finally
								{
									List<MyListItem>.Enumerator enumerator2;
									((IDisposable)enumerator2).Dispose();
								}
							}
							bool flag2 = false;
							try
							{
								foreach (MyListItem myListItem2 in list)
								{
									if (myListItem2.Checked)
									{
										if (flag2)
										{
											myListItem2.Checked = false;
										}
										else
										{
											flag2 = true;
										}
									}
								}
							}
							finally
							{
								List<MyListItem>.Enumerator enumerator3;
								((IDisposable)enumerator3).Dispose();
							}
						}
					}
					IL_255:;
				}
				if (base.IsLoaded && ModAnimation.CalcParser() == 0 && anime)
				{
					List<ModAnimation.AniData> list2 = new List<ModAnimation.AniData>();
					if (this.Checked)
					{
						if (!Information.IsNothing(this.m_ParamComposer))
						{
							double num3 = base.ActualHeight - this.m_ParamComposer.ActualHeight - 12.0;
							list2.Add(ModAnimation.AaHeight(this.m_ParamComposer, num3 * 0.4, 200, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false));
							list2.Add(ModAnimation.AaHeight(this.m_ParamComposer, num3 * 0.6, 300, 0, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false));
							list2.Add(ModAnimation.AaOpacity(this.m_ParamComposer, 1.0 - this.m_ParamComposer.Opacity, 30, 0, null, false));
							this.m_ParamComposer.VerticalAlignment = VerticalAlignment.Center;
							this.m_ParamComposer.Margin = new Thickness(-1.0, 0.0, 0.0, 0.0);
						}
						list2.Add(ModAnimation.AaColor(this, MyListItem.m_GetterComposer, (base.Height < 40.0) ? "ColorBrush3" : "ColorBrush2", 200, 0, null, false));
					}
					else
					{
						if (!Information.IsNothing(this.m_ParamComposer))
						{
							list2.Add(ModAnimation.AaHeight(this.m_ParamComposer, -this.m_ParamComposer.ActualHeight, 120, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak), false));
							list2.Add(ModAnimation.AaOpacity(this.m_ParamComposer, -this.m_ParamComposer.Opacity, 70, 40, null, false));
							this.m_ParamComposer.VerticalAlignment = VerticalAlignment.Center;
						}
						list2.Add(ModAnimation.AaColor(this, MyListItem.m_GetterComposer, "ColorBrush1", 120, 0, null, false));
					}
					ModAnimation.AniStart(list2, "MyListItem Checked " + Conversions.ToString(this._ObserverComposer), false);
				}
				else
				{
					ModAnimation.AniStop("MyListItem Checked " + Conversions.ToString(this._ObserverComposer));
					if (this.Checked)
					{
						if (!Information.IsNothing(this.m_ParamComposer))
						{
							this.m_ParamComposer.Height = double.NaN;
							this.m_ParamComposer.Margin = new Thickness(-1.0, 6.0, 0.0, 6.0);
							this.m_ParamComposer.Opacity = 1.0;
							this.m_ParamComposer.VerticalAlignment = VerticalAlignment.Stretch;
						}
						base.SetResourceReference(MyListItem.m_GetterComposer, (base.Height < 40.0) ? "ColorBrush3" : "ColorBrush2");
					}
					else
					{
						if (!Information.IsNothing(this.m_ParamComposer))
						{
							this.m_ParamComposer.Height = 0.0;
							this.m_ParamComposer.Margin = new Thickness(-1.0, 0.0, 0.0, 0.0);
							this.m_ParamComposer.Opacity = 0.0;
							this.m_ParamComposer.VerticalAlignment = VerticalAlignment.Center;
						}
						base.SetResourceReference(MyListItem.m_GetterComposer, "ColorBrush1");
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "设置 Checked 失败", ModBase.LogLevel.Debug, "出现错误");
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x0000BDC1 File Offset: 0x00009FC1
		// (set) Token: 0x060014F1 RID: 5361 RVA: 0x0000BDD3 File Offset: 0x00009FD3
		public Brush Foreground
		{
			get
			{
				return (Brush)base.GetValue(MyListItem.m_GetterComposer);
			}
			set
			{
				base.SetValue(MyListItem.m_GetterComposer, value);
			}
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0008D0F4 File Offset: 0x0008B2F4
		private void Button_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (this.expressionComposer)
			{
				MyListItem.ClickEventHandler indexerComposer = this.m_IndexerComposer;
				if (indexerComposer != null)
				{
					indexerComposer(RuntimeHelpers.GetObjectValue(sender), e);
				}
				if (!e.Handled)
				{
					if (!string.IsNullOrEmpty(this.EventType))
					{
						ModEvent.TryStartEvent(this.EventType, this.EventData);
						e.Handled = true;
					}
					if (!e.Handled)
					{
						switch (this.Type)
						{
						case MyListItem.CheckType.Clickable:
							ModBase.Log("[Control] 按下单击列表项：" + this.Title, ModBase.LogLevel.Normal, "出现错误");
							return;
						case MyListItem.CheckType.RadioBox:
							ModBase.Log("[Control] 按下单选列表项：" + this.Title, ModBase.LogLevel.Normal, "出现错误");
							if (!this.Checked)
							{
								this.SetChecked(true, true, true);
								return;
							}
							break;
						case MyListItem.CheckType.CheckBox:
							ModBase.Log("[Control] 按下复选列表项（" + (!this.Checked).ToString() + "）：" + this.Title, ModBase.LogLevel.Normal, "出现错误");
							this.SetChecked(!this.Checked, true, true);
							break;
						default:
							return;
						}
					}
				}
			}
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0000BDE1 File Offset: 0x00009FE1
		private void Button_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (base.IsMouseDirectlyOver && this.Type != MyListItem.CheckType.None)
			{
				this.expressionComposer = true;
				if (this.systemComposer != null)
				{
					this.systemComposer.IsHitTestVisible = false;
				}
			}
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0000BE0E File Offset: 0x0000A00E
		private void Button_MouseLeave(object sender, object e)
		{
			this.expressionComposer = false;
			if (this.systemComposer != null)
			{
				this.systemComposer.IsHitTestVisible = true;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x0000BE2B File Offset: 0x0000A02B
		// (set) Token: 0x060014F6 RID: 5366 RVA: 0x0000BE3D File Offset: 0x0000A03D
		public string EventType
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyListItem._WriterComposer));
			}
			set
			{
				base.SetValue(MyListItem._WriterComposer, value);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x0000BE4B File Offset: 0x0000A04B
		// (set) Token: 0x060014F8 RID: 5368 RVA: 0x0000BE5D File Offset: 0x0000A05D
		public string EventData
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyListItem._RegistryComposer));
			}
			set
			{
				base.SetValue(MyListItem._RegistryComposer, value);
			}
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0008D208 File Offset: 0x0008B408
		public void RefreshColor(object sender, EventArgs e)
		{
			if (this.tokenComposer != null)
			{
				this.tokenComposer((MyListItem)sender, e);
				this.tokenComposer = null;
			}
			string text;
			int num;
			if (this.expressionComposer && (this.Type != MyListItem.CheckType.RadioBox || !this.Checked))
			{
				text = "MouseDown";
				num = 120;
			}
			else if (base.IsMouseOver && this._ProccesorComposer)
			{
				text = "MouseOver";
				num = 120;
			}
			else
			{
				text = "Idle";
				num = 180;
			}
			if (Operators.CompareString(this._RuleComposer, text, false) != 0)
			{
				this._RuleComposer = text;
				if (base.IsLoaded && ModAnimation.CalcParser() == 0)
				{
					List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
					if (base.IsMouseOver && this._ProccesorComposer)
					{
						checked
						{
							if (this.systemComposer != null)
							{
								list.Add(ModAnimation.AaOpacity(this.systemComposer, unchecked(1.0 - this.systemComposer.Opacity), (int)Math.Round(unchecked((double)num * 0.7)), (int)Math.Round(unchecked((double)num * 0.3)), null, false));
								list.Add(ModAnimation.AaDouble(delegate(object i)
								{
									this.ColumnPaddingRight.Width = new GridLength(Conversions.ToDouble(NewLateBinding.LateGet(null, typeof(Math), "Max", new object[]
									{
										0,
										Operators.AddObject(this.ColumnPaddingRight.Width.Value, i)
									}, null, null, null)));
								}, unchecked((double)Math.Max(this.MinPaddingRight, checked(5 + Enumerable.Count<MyIconButton>(this.Buttons) * 25)) - this.ColumnPaddingRight.Width.Value), (int)Math.Round(unchecked((double)num * 0.3)), (int)Math.Round(unchecked((double)num * 0.7)), null, false));
							}
						}
						list.AddRange(new ModAnimation.AniData[]
						{
							ModAnimation.AaColor(this.RectBack, Border.BackgroundProperty, this.expressionComposer ? "ColorBrush6" : "ColorBrushBg1", num, 0, null, false),
							ModAnimation.AaOpacity(this.RectBack, 1.0 - this.RectBack.Opacity, num, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false)
						});
						if (this.IsScaleAnimationEnabled)
						{
							list.Add(ModAnimation.AaScaleTransform(this.RectBack, 1.0 - ((ScaleTransform)this.RectBack.RenderTransform).ScaleX, checked((int)Math.Round(unchecked((double)num * 1.6))), 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false));
							if (this.expressionComposer)
							{
								list.Add(ModAnimation.AaScaleTransform(this, 0.98 - ((ScaleTransform)base.RenderTransform).ScaleX, checked((int)Math.Round(unchecked((double)num * 0.9))), 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false));
							}
							else
							{
								list.Add(ModAnimation.AaScaleTransform(this, 1.0 - ((ScaleTransform)base.RenderTransform).ScaleX, checked((int)Math.Round(unchecked((double)num * 1.2))), 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false));
							}
						}
					}
					else
					{
						if (this.systemComposer != null)
						{
							list.Add(ModAnimation.AaOpacity(this.systemComposer, -this.systemComposer.Opacity, checked((int)Math.Round(unchecked((double)num * 0.4))), 0, null, false));
							list.Add(ModAnimation.AaDouble(delegate(object i)
							{
								this.ColumnPaddingRight.Width = new GridLength(Conversions.ToDouble(NewLateBinding.LateGet(null, typeof(Math), "Max", new object[]
								{
									0,
									Operators.AddObject(this.ColumnPaddingRight.Width.Value, i)
								}, null, null, null)));
							}, (double)this.MinPaddingRight - this.ColumnPaddingRight.Width.Value, checked((int)Math.Round(unchecked((double)num * 0.4))), 0, null, false));
						}
						list.Add(ModAnimation.AaOpacity(this.RectBack, -this.RectBack.Opacity, num, 0, null, false));
						if (this.IsScaleAnimationEnabled)
						{
							list.AddRange(new ModAnimation.AniData[]
							{
								ModAnimation.AaColor(this.RectBack, Border.BackgroundProperty, this.expressionComposer ? "ColorBrush6" : "ColorBrush7", num, 0, null, false),
								ModAnimation.AaScaleTransform(this, 1.0 - ((ScaleTransform)base.RenderTransform).ScaleX, checked(num * 3), 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
								ModAnimation.AaScaleTransform(this.RectBack, 0.996 - ((ScaleTransform)this.RectBack.RenderTransform).ScaleX, num, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
								ModAnimation.AaScaleTransform(this.RectBack, -0.246, 1, 0, null, true)
							});
						}
					}
					ModAnimation.AniStart(list, "ListItem Color " + Conversions.ToString(this._ObserverComposer), false);
					return;
				}
				if (base.IsMouseOver && this._ProccesorComposer)
				{
					if (this.systemComposer != null)
					{
						this.systemComposer.Opacity = 1.0;
						this.ColumnPaddingRight.Width = new GridLength((double)Math.Max(this.MinPaddingRight, checked(5 + Enumerable.Count<MyIconButton>(this.Buttons) * 25)));
					}
					this.RectBack.Background = ModSecret.m_RulesField;
					this.RectBack.Opacity = 1.0;
					this.RectBack.RenderTransform = new ScaleTransform(1.0, 1.0);
					base.RenderTransform = new ScaleTransform(1.0, 1.0);
				}
				else
				{
					if (this.systemComposer != null)
					{
						this.systemComposer.Opacity = 0.0;
						this.ColumnPaddingRight.Width = new GridLength((double)this.MinPaddingRight);
					}
					base.RenderTransform = new ScaleTransform(1.0, 1.0);
					if (this.m_IdentifierComposer != null)
					{
						if (this.IsScaleAnimationEnabled)
						{
							this.RectBack.RenderTransform = new ScaleTransform(0.75, 0.75);
						}
						this.RectBack.Background = ModSecret.m_TagField;
						this.RectBack.Opacity = 0.0;
					}
				}
				ModAnimation.AniStop("ListItem Color " + Conversions.ToString(this._ObserverComposer));
			}
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x0008D804 File Offset: 0x0008BA04
		private void MyListItem_Loaded(object sender, RoutedEventArgs e)
		{
			if (this.Checked)
			{
				base.SetResourceReference(MyListItem.m_GetterComposer, (base.Height < 40.0) ? "ColorBrush3" : "ColorBrush2");
			}
			else
			{
				base.SetResourceReference(MyListItem.m_GetterComposer, "ColorBrush1");
			}
			this.ColumnPaddingRight.Width = new GridLength((double)this.MinPaddingRight);
			if (Operators.CompareString(this.EventType, "打开帮助", false) == 0 && (Operators.CompareString(this.Title, "", false) == 0 || Operators.CompareString(this.Info, "", false) == 0))
			{
				try
				{
					new ModMain.HelpEntry(ModEvent.GetEventAbsoluteUrls(this.EventData, this.EventType)[0]).SetToListItem(this);
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "设置帮助 MyListItem 失败", ModBase.LogLevel.Msgbox, "出现错误");
					this.EventType = null;
					this.EventData = null;
				}
			}
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0000BE6B File Offset: 0x0000A06B
		public override string ToString()
		{
			return this.Title;
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x0000BE73 File Offset: 0x0000A073
		// (set) Token: 0x060014FD RID: 5373 RVA: 0x0000BE7B File Offset: 0x0000A07B
		internal virtual MyListItem PanBack { get; set; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x0000BE84 File Offset: 0x0000A084
		// (set) Token: 0x060014FF RID: 5375 RVA: 0x0000BE8C File Offset: 0x0000A08C
		internal virtual ColumnDefinition ColumnCheck { get; set; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0000BE95 File Offset: 0x0000A095
		// (set) Token: 0x06001501 RID: 5377 RVA: 0x0000BE9D File Offset: 0x0000A09D
		internal virtual ColumnDefinition ColumnPaddingLeft { get; set; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x0000BEA6 File Offset: 0x0000A0A6
		// (set) Token: 0x06001503 RID: 5379 RVA: 0x0000BEAE File Offset: 0x0000A0AE
		internal virtual ColumnDefinition ColumnLogo { get; set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0000BEB7 File Offset: 0x0000A0B7
		// (set) Token: 0x06001505 RID: 5381 RVA: 0x0000BEBF File Offset: 0x0000A0BF
		internal virtual ColumnDefinition ColumnPaddingRight { get; set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x0000BEC8 File Offset: 0x0000A0C8
		// (set) Token: 0x06001507 RID: 5383 RVA: 0x0000BED0 File Offset: 0x0000A0D0
		internal virtual TextBlock LabTitle { get; set; }

		// Token: 0x06001508 RID: 5384 RVA: 0x0008D904 File Offset: 0x0008BB04
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.serverComposer)
			{
				this.serverComposer = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/mylistitem.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0008D934 File Offset: 0x0008BB34
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (MyListItem)target;
				return;
			}
			if (connectionId == 2)
			{
				this.ColumnCheck = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 3)
			{
				this.ColumnPaddingLeft = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 4)
			{
				this.ColumnLogo = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 5)
			{
				this.ColumnPaddingRight = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 6)
			{
				this.LabTitle = (TextBlock)target;
				return;
			}
			this.serverComposer = true;
		}

		// Token: 0x04000AAB RID: 2731
		[CompilerGenerated]
		private MyListItem.ClickEventHandler m_IndexerComposer;

		// Token: 0x04000AAC RID: 2732
		[CompilerGenerated]
		private MyListItem.LogoClickEventHandler interpreterComposer;

		// Token: 0x04000AAD RID: 2733
		[CompilerGenerated]
		private IMyRadio.CheckEventHandler m_SerializerComposer;

		// Token: 0x04000AAE RID: 2734
		[CompilerGenerated]
		private IMyRadio.ChangedEventHandler _WatcherComposer;

		// Token: 0x04000AAF RID: 2735
		private Border m_IdentifierComposer;

		// Token: 0x04000AB0 RID: 2736
		public FrameworkElement systemComposer;

		// Token: 0x04000AB1 RID: 2737
		public FrameworkElement PathLogo;

		// Token: 0x04000AB2 RID: 2738
		public Border m_ParamComposer;

		// Token: 0x04000AB3 RID: 2739
		private TextBlock tagComposer;

		// Token: 0x04000AB4 RID: 2740
		public int _ObserverComposer;

		// Token: 0x04000AB5 RID: 2741
		private bool stubComposer;

		// Token: 0x04000AB6 RID: 2742
		[CompilerGenerated]
		private int m_RulesComposer;

		// Token: 0x04000AB7 RID: 2743
		private IEnumerable<MyIconButton> m_RefComposer;

		// Token: 0x04000AB8 RID: 2744
		public static readonly DependencyProperty _DecoratorComposer = DependencyProperty.Register("Title", typeof(string), typeof(MyListItem));

		// Token: 0x04000AB9 RID: 2745
		public static readonly DependencyProperty instanceComposer = DependencyProperty.Register("FontSize", typeof(double), typeof(MyListItem), new PropertyMetadata(14.0));

		// Token: 0x04000ABA RID: 2746
		private string stateComposer;

		// Token: 0x04000ABB RID: 2747
		private string callbackComposer;

		// Token: 0x04000ABC RID: 2748
		private double m_TemplateComposer;

		// Token: 0x04000ABD RID: 2749
		[CompilerGenerated]
		private bool _MethodComposer;

		// Token: 0x04000ABE RID: 2750
		private bool taskComposer;

		// Token: 0x04000ABF RID: 2751
		private MyListItem.CheckType _ConsumerComposer;

		// Token: 0x04000AC0 RID: 2752
		private bool configurationComposer;

		// Token: 0x04000AC1 RID: 2753
		public static readonly DependencyProperty m_GetterComposer = DependencyProperty.Register("Foreground", typeof(Brush), typeof(MyListItem), new PropertyMetadata(ModSecret._InterpreterField));

		// Token: 0x04000AC2 RID: 2754
		public Action<MyListItem, EventArgs> tokenComposer;

		// Token: 0x04000AC3 RID: 2755
		private bool expressionComposer;

		// Token: 0x04000AC4 RID: 2756
		public static readonly DependencyProperty _WriterComposer = DependencyProperty.Register("EventType", typeof(string), typeof(MyListItem), new PropertyMetadata(null));

		// Token: 0x04000AC5 RID: 2757
		public static readonly DependencyProperty _RegistryComposer = DependencyProperty.Register("EventData", typeof(string), typeof(MyListItem), new PropertyMetadata(null));

		// Token: 0x04000AC6 RID: 2758
		private string _RuleComposer;

		// Token: 0x04000AC7 RID: 2759
		public bool _ProccesorComposer;

		// Token: 0x04000AC8 RID: 2760
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyListItem setterComposer;

		// Token: 0x04000AC9 RID: 2761
		[CompilerGenerated]
		[AccessedThroughProperty("ColumnCheck")]
		private ColumnDefinition m_FactoryComposer;

		// Token: 0x04000ACA RID: 2762
		[CompilerGenerated]
		[AccessedThroughProperty("ColumnPaddingLeft")]
		private ColumnDefinition m_ExporterComposer;

		// Token: 0x04000ACB RID: 2763
		[AccessedThroughProperty("ColumnLogo")]
		[CompilerGenerated]
		private ColumnDefinition _ImporterComposer;

		// Token: 0x04000ACC RID: 2764
		[AccessedThroughProperty("ColumnPaddingRight")]
		[CompilerGenerated]
		private ColumnDefinition _WorkerComposer;

		// Token: 0x04000ACD RID: 2765
		[AccessedThroughProperty("LabTitle")]
		[CompilerGenerated]
		private TextBlock m_ConnectionComposer;

		// Token: 0x04000ACE RID: 2766
		private bool serverComposer;

		// Token: 0x020001C2 RID: 450
		// (Invoke) Token: 0x06001513 RID: 5395
		public delegate void ClickEventHandler(object sender, MouseButtonEventArgs e);

		// Token: 0x020001C3 RID: 451
		// (Invoke) Token: 0x06001518 RID: 5400
		public delegate void LogoClickEventHandler(object sender, MouseButtonEventArgs e);

		// Token: 0x020001C4 RID: 452
		public enum CheckType
		{
			// Token: 0x04000AD0 RID: 2768
			None,
			// Token: 0x04000AD1 RID: 2769
			Clickable,
			// Token: 0x04000AD2 RID: 2770
			RadioBox,
			// Token: 0x04000AD3 RID: 2771
			CheckBox
		}
	}
}
