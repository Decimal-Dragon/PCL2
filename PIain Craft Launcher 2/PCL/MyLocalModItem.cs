using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x02000085 RID: 133
	[DesignerGenerated]
	public class MyLocalModItem : Grid, IComponentConnector
	{
		// Token: 0x06000339 RID: 825 RVA: 0x000245F0 File Offset: 0x000227F0
		public MyLocalModItem()
		{
			base.PreviewMouseLeftButtonUp += this.Button_MouseUp;
			base.PreviewMouseLeftButtonDown += this.Button_MouseDown;
			base.MouseLeave += new MouseEventHandler(this.Button_MouseLeave);
			base.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(this.Button_MouseLeave);
			base.MouseLeftButtonDown += new MouseButtonEventHandler(this.Button_MouseSwipeStart);
			base.MouseEnter += new MouseEventHandler(this.Button_MouseSwipe);
			base.MouseLeave += new MouseEventHandler(this.Button_MouseSwipe);
			base.MouseLeftButtonUp += new MouseButtonEventHandler(this.Button_MouseSwipe);
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.Refresh();
			};
			base.MouseEnter += new MouseEventHandler(this.RefreshColor);
			base.MouseLeave += new MouseEventHandler(this.RefreshColor);
			base.MouseLeftButtonDown += new MouseButtonEventHandler(this.RefreshColor);
			base.MouseLeftButtonUp += new MouseButtonEventHandler(this.RefreshColor);
			this.RateBroadcaster(new MyLocalModItem.ChangedEventHandler(this.RefreshColor));
			this.m_Message = ModBase.GetUuid();
			this._Singleton = false;
			this.global = false;
			this.m_Class = null;
			this.InitializeComponent();
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00003F47 File Offset: 0x00002147
		// (set) Token: 0x0600033B RID: 827 RVA: 0x00003F54 File Offset: 0x00002154
		public string Logo
		{
			get
			{
				return this.PathLogo.Source;
			}
			set
			{
				this.PathLogo.Source = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00003F62 File Offset: 0x00002162
		// (set) Token: 0x0600033D RID: 829 RVA: 0x00024728 File Offset: 0x00022928
		public string Title
		{
			get
			{
				return this._Creator;
			}
			set
			{
				string creator = value;
				switch (this.ExcludeBroadcaster().State)
				{
				case ModMod.McMod.McModState.Fine:
					this.LabTitle.TextDecorations = null;
					break;
				case ModMod.McMod.McModState.Disabled:
					this.LabTitle.TextDecorations = TextDecorations.Strikethrough;
					break;
				case ModMod.McMod.McModState.Unavailable:
					this.LabTitle.TextDecorations = TextDecorations.Strikethrough;
					value += " [错误]";
					break;
				}
				if (Operators.CompareString(this.LabTitle.Text, value, false) != 0)
				{
					this.LabTitle.Text = value;
					this._Creator = creator;
				}
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00003F6A File Offset: 0x0000216A
		public string RemoveBroadcaster()
		{
			TextBlock labSubtitle = this.LabSubtitle;
			string result;
			if (labSubtitle != null)
			{
				if ((result = labSubtitle.Text) != null)
				{
					return result;
				}
			}
			result = "";
			return result;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00003F87 File Offset: 0x00002187
		public void ConnectBroadcaster(string value)
		{
			if (Operators.CompareString(this.LabSubtitle.Text, value, false) != 0)
			{
				this.LabSubtitle.Text = value;
				this.LabSubtitle.Visibility = ((Operators.CompareString(value, "", false) == 0) ? Visibility.Collapsed : Visibility.Visible);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00003FC6 File Offset: 0x000021C6
		// (set) Token: 0x06000341 RID: 833 RVA: 0x00003FD3 File Offset: 0x000021D3
		public string Description
		{
			get
			{
				return this.LabInfo.Text;
			}
			set
			{
				if (Operators.CompareString(this.LabInfo.Text, value, false) != 0)
				{
					this.LabInfo.Text = value;
				}
			}
		}

		// Token: 0x17000030 RID: 48
		// (set) Token: 0x06000342 RID: 834 RVA: 0x000247BC File Offset: 0x000229BC
		public List<string> Tags
		{
			set
			{
				this.PanTags.Children.Clear();
				this.PanTags.Visibility = (Enumerable.Any<string>(value) ? Visibility.Visible : Visibility.Collapsed);
				try
				{
					foreach (string str in value)
					{
						object objectValue = RuntimeHelpers.GetObjectValue(ModBase.GetObjectFromXML("<Border xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\r\n                         Background=\"#0C000000\" Padding=\"3,1\" CornerRadius=\"3\" Margin=\"0,0,3,0\" \r\n                         SnapsToDevicePixels=\"True\" UseLayoutRounding=\"False\">\r\n                   <TextBlock Text=\"" + str + "\" Foreground=\"#88000000\" FontSize=\"11\" />\r\n                </Border>"));
						this.PanTags.Children.Add((UIElement)objectValue);
					}
				}
				finally
				{
					List<string>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00003FF5 File Offset: 0x000021F5
		public ModMod.McMod ExcludeBroadcaster()
		{
			return (ModMod.McMod)base.Tag;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00004002 File Offset: 0x00002202
		public void PopBroadcaster(ModMod.McMod value)
		{
			base.Tag = value;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000345 RID: 837 RVA: 0x0002485C File Offset: 0x00022A5C
		// (remove) Token: 0x06000346 RID: 838 RVA: 0x00024894 File Offset: 0x00022A94
		public event MyLocalModItem.ClickEventHandler Click
		{
			[CompilerGenerated]
			add
			{
				MyLocalModItem.ClickEventHandler clickEventHandler = this.initializer;
				MyLocalModItem.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MyLocalModItem.ClickEventHandler value2 = (MyLocalModItem.ClickEventHandler)Delegate.Combine(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MyLocalModItem.ClickEventHandler>(ref this.initializer, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				MyLocalModItem.ClickEventHandler clickEventHandler = this.initializer;
				MyLocalModItem.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MyLocalModItem.ClickEventHandler value2 = (MyLocalModItem.ClickEventHandler)Delegate.Remove(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MyLocalModItem.ClickEventHandler>(ref this.initializer, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000248CC File Offset: 0x00022ACC
		private void Button_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (this._Singleton)
			{
				MyLocalModItem.ClickEventHandler clickEventHandler = this.initializer;
				if (clickEventHandler != null)
				{
					clickEventHandler(RuntimeHelpers.GetObjectValue(sender), e);
				}
				if (!e.Handled)
				{
					ModBase.Log("[Control] 按下本地 Mod 列表项：" + this.LabTitle.Text, ModBase.LogLevel.Normal, "出现错误");
				}
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000400B File Offset: 0x0000220B
		private void Button_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (base.IsMouseDirectlyOver)
			{
				this._Singleton = true;
				if (this._Order != null)
				{
					this._Order.IsHitTestVisible = false;
				}
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00004030 File Offset: 0x00002230
		private void Button_MouseLeave(object sender, object e)
		{
			this._Singleton = false;
			if (this._Order != null)
			{
				this._Order.IsHitTestVisible = true;
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00024920 File Offset: 0x00022B20
		private void Button_MouseSwipeStart(object sender, object e)
		{
			if (base.Parent != null)
			{
				MyLocalModItem.listener = (MyLocalModItem.product = ((StackPanel)base.Parent).Children.IndexOf(this));
				MyLocalModItem._Collection = true;
				MyLocalModItem._Object = !this.Checked;
				ModMain._ThreadRepository.CardSelect.IsHitTestVisible = false;
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0002497C File Offset: 0x00022B7C
		private void Button_MouseSwipe(object sender, object e)
		{
			checked
			{
				if (base.Parent != null)
				{
					if (Mouse.LeftButton != MouseButtonState.Pressed || !MyLocalModItem._Collection)
					{
						MyLocalModItem._Collection = false;
						ModMain._ThreadRepository.CardSelect.IsHitTestVisible = true;
						return;
					}
					int val = ((StackPanel)base.Parent).Children.IndexOf(this);
					MyLocalModItem.product = Math.Min(MyLocalModItem.product, val);
					MyLocalModItem.listener = Math.Max(MyLocalModItem.listener, val);
					if (MyLocalModItem.product != MyLocalModItem.listener)
					{
						int num = MyLocalModItem.product;
						int num2 = MyLocalModItem.listener;
						for (int i = num; i <= num2; i++)
						{
							MyLocalModItem myLocalModItem = (MyLocalModItem)((StackPanel)base.Parent).Children[i];
							myLocalModItem.InitLate(myLocalModItem, (EventArgs)e);
							myLocalModItem.Checked = MyLocalModItem._Object;
						}
					}
				}
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00024A50 File Offset: 0x00022C50
		[CompilerGenerated]
		public void AssetBroadcaster(MyLocalModItem.CheckEventHandler obj)
		{
			MyLocalModItem.CheckEventHandler checkEventHandler = this.m_Bridge;
			MyLocalModItem.CheckEventHandler checkEventHandler2;
			do
			{
				checkEventHandler2 = checkEventHandler;
				MyLocalModItem.CheckEventHandler value = (MyLocalModItem.CheckEventHandler)Delegate.Combine(checkEventHandler2, obj);
				checkEventHandler = Interlocked.CompareExchange<MyLocalModItem.CheckEventHandler>(ref this.m_Bridge, value, checkEventHandler2);
			}
			while (checkEventHandler != checkEventHandler2);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00024A88 File Offset: 0x00022C88
		[CompilerGenerated]
		public void ConcatBroadcaster(MyLocalModItem.CheckEventHandler obj)
		{
			MyLocalModItem.CheckEventHandler checkEventHandler = this.m_Bridge;
			MyLocalModItem.CheckEventHandler checkEventHandler2;
			do
			{
				checkEventHandler2 = checkEventHandler;
				MyLocalModItem.CheckEventHandler value = (MyLocalModItem.CheckEventHandler)Delegate.Remove(checkEventHandler2, obj);
				checkEventHandler = Interlocked.CompareExchange<MyLocalModItem.CheckEventHandler>(ref this.m_Bridge, value, checkEventHandler2);
			}
			while (checkEventHandler != checkEventHandler2);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00024AC0 File Offset: 0x00022CC0
		[CompilerGenerated]
		public void RateBroadcaster(MyLocalModItem.ChangedEventHandler obj)
		{
			MyLocalModItem.ChangedEventHandler changedEventHandler = this._Reponse;
			MyLocalModItem.ChangedEventHandler changedEventHandler2;
			do
			{
				changedEventHandler2 = changedEventHandler;
				MyLocalModItem.ChangedEventHandler value = (MyLocalModItem.ChangedEventHandler)Delegate.Combine(changedEventHandler2, obj);
				changedEventHandler = Interlocked.CompareExchange<MyLocalModItem.ChangedEventHandler>(ref this._Reponse, value, changedEventHandler2);
			}
			while (changedEventHandler != changedEventHandler2);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00024AF8 File Offset: 0x00022CF8
		[CompilerGenerated]
		public void CancelBroadcaster(MyLocalModItem.ChangedEventHandler obj)
		{
			MyLocalModItem.ChangedEventHandler changedEventHandler = this._Reponse;
			MyLocalModItem.ChangedEventHandler changedEventHandler2;
			do
			{
				changedEventHandler2 = changedEventHandler;
				MyLocalModItem.ChangedEventHandler value = (MyLocalModItem.ChangedEventHandler)Delegate.Remove(changedEventHandler2, obj);
				changedEventHandler = Interlocked.CompareExchange<MyLocalModItem.ChangedEventHandler>(ref this._Reponse, value, changedEventHandler2);
			}
			while (changedEventHandler != changedEventHandler2);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000404D File Offset: 0x0000224D
		// (set) Token: 0x06000351 RID: 849 RVA: 0x00024B30 File Offset: 0x00022D30
		public bool Checked
		{
			get
			{
				return this.global;
			}
			set
			{
				try
				{
					bool flag = this.global;
					if (value != this.global)
					{
						this.global = value;
						ModBase.RouteEventArgs routeEventArgs = new ModBase.RouteEventArgs(false);
						if (base.IsInitialized)
						{
							MyLocalModItem.ChangedEventHandler reponse = this._Reponse;
							if (reponse != null)
							{
								reponse(this, routeEventArgs);
							}
							if (routeEventArgs.m_SerializerError)
							{
								this.global = flag;
								return;
							}
						}
						if (value)
						{
							ModBase.RouteEventArgs routeEventArgs2 = new ModBase.RouteEventArgs(false);
							MyLocalModItem.CheckEventHandler bridge = this.m_Bridge;
							if (bridge != null)
							{
								bridge(this, routeEventArgs2);
							}
							if (routeEventArgs2.m_SerializerError)
							{
								return;
							}
						}
						if (this.IsVisibleInForm())
						{
							List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
							if (this.Checked)
							{
								double num = 32.0 - this.CompareBroadcaster().ActualHeight;
								list.Add(ModAnimation.AaHeight(this.CompareBroadcaster(), num * 0.4, 200, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false));
								list.Add(ModAnimation.AaHeight(this.CompareBroadcaster(), num * 0.6, 300, 0, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false));
								list.Add(ModAnimation.AaOpacity(this.CompareBroadcaster(), 1.0 - this.CompareBroadcaster().Opacity, 30, 0, null, false));
								this.CompareBroadcaster().VerticalAlignment = VerticalAlignment.Center;
								this.CompareBroadcaster().Margin = new Thickness(-3.0, 0.0, 0.0, 0.0);
								list.Add(ModAnimation.AaColor(this.LabTitle, TextBlock.ForegroundProperty, (this.ExcludeBroadcaster().State == ModMod.McMod.McModState.Fine) ? "ColorBrush2" : "ColorBrush5", 200, 0, null, false));
							}
							else
							{
								list.Add(ModAnimation.AaHeight(this.CompareBroadcaster(), -this.CompareBroadcaster().ActualHeight, 120, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak), false));
								list.Add(ModAnimation.AaOpacity(this.CompareBroadcaster(), -this.CompareBroadcaster().Opacity, 70, 40, null, false));
								this.CompareBroadcaster().VerticalAlignment = VerticalAlignment.Center;
								list.Add(ModAnimation.AaColor(this.LabTitle, TextBlock.ForegroundProperty, (this.LabTitle.TextDecorations == null) ? "ColorBrush1" : "ColorBrushGray4", 120, 0, null, false));
							}
							ModAnimation.AniStart(list, "MyLocalModItem Checked " + Conversions.ToString(this.m_Message), false);
						}
						else
						{
							this.CompareBroadcaster().VerticalAlignment = VerticalAlignment.Center;
							this.CompareBroadcaster().Margin = new Thickness(-3.0, 0.0, 0.0, 0.0);
							if (this.Checked)
							{
								this.CompareBroadcaster().Height = 32.0;
								this.CompareBroadcaster().Opacity = 1.0;
								this.LabTitle.SetResourceReference(TextBlock.ForegroundProperty, (this.ExcludeBroadcaster().State == ModMod.McMod.McModState.Fine) ? "ColorBrush2" : "ColorBrush5");
							}
							else
							{
								this.CompareBroadcaster().Height = 0.0;
								this.CompareBroadcaster().Opacity = 0.0;
								this.LabTitle.SetResourceReference(TextBlock.ForegroundProperty, (this.ExcludeBroadcaster().State == ModMod.McMod.McModState.Fine) ? "ColorBrush1" : "ColorBrushGray4");
							}
							ModAnimation.AniStop("MyLocalModItem Checked " + Conversions.ToString(this.m_Message));
						}
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "设置 Checked 失败", ModBase.LogLevel.Debug, "出现错误");
				}
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00024EE4 File Offset: 0x000230E4
		public Border RectBack
		{
			get
			{
				if (this.m_Class == null)
				{
					Border border = new Border
					{
						Name = "RectBack",
						CornerRadius = new CornerRadius(3.0),
						RenderTransform = new ScaleTransform(0.8, 0.8),
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
					this.m_Class = border;
				}
				return this.m_Class;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00004055 File Offset: 0x00002255
		// (set) Token: 0x06000354 RID: 852 RVA: 0x00024FE4 File Offset: 0x000231E4
		public IEnumerable<MyIconButton> Buttons
		{
			get
			{
				return this.m_Producer;
			}
			set
			{
				this.m_Producer = value;
				if (this._Order != null)
				{
					base.Children.Remove(this._Order);
					this._Order = null;
				}
				if (Enumerable.Any<MyIconButton>(value))
				{
					this._Order = new StackPanel
					{
						Opacity = 0.0,
						Margin = new Thickness(0.0, 0.0, 5.0, 0.0),
						SnapsToDevicePixels = false,
						Orientation = Orientation.Horizontal,
						HorizontalAlignment = HorizontalAlignment.Right,
						VerticalAlignment = VerticalAlignment.Center,
						UseLayoutRounding = false
					};
					Grid.SetColumnSpan(this._Order, 10);
					Grid.SetRowSpan(this._Order, 10);
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
							((StackPanel)this._Order).Children.Add(myIconButton);
						}
					}
					finally
					{
						IEnumerator<MyIconButton> enumerator;
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
					base.Children.Add(this._Order);
				}
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00025158 File Offset: 0x00023358
		public Border CompareBroadcaster()
		{
			if (this._Schema == null)
			{
				this._Schema = new Border
				{
					Width = 5.0,
					Height = (this.Checked ? double.NaN : 0.0),
					CornerRadius = new CornerRadius(2.0, 2.0, 2.0, 2.0),
					VerticalAlignment = (this.Checked ? VerticalAlignment.Stretch : VerticalAlignment.Center),
					HorizontalAlignment = HorizontalAlignment.Left,
					UseLayoutRounding = false,
					SnapsToDevicePixels = false,
					Margin = (this.Checked ? new Thickness(-3.0, 6.0, 0.0, 6.0) : new Thickness(-3.0, 0.0, 0.0, 0.0))
				};
				this._Schema.SetResourceReference(Border.BackgroundProperty, "ColorBrush3");
				Grid.SetRowSpan(this._Schema, 10);
				base.Children.Add(this._Schema);
			}
			return this._Schema;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000252A0 File Offset: 0x000234A0
		private string GetUpdateCompareDescription()
		{
			string text = this.ExcludeBroadcaster()._PropertyTest._BridgeRepository.Replace(".jar", "");
			string text2 = this.ExcludeBroadcaster().UpdateFile._BridgeRepository.Replace(".jar", "");
			List<string> list = Enumerable.ToList<string>(text.Split(new char[]
			{
				'-'
			}));
			List<string> list2 = Enumerable.ToList<string>(text2.Split(new char[]
			{
				'-'
			}));
			bool flag = false;
			try
			{
				foreach (string item in Enumerable.ToList<string>(list))
				{
					if (list2.Contains(item))
					{
						list.Remove(item);
						list2.Remove(item);
						flag = true;
					}
				}
			}
			finally
			{
				List<string>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			if (flag && Enumerable.Any<string>(list) && Enumerable.Any<string>(list2))
			{
				text = list.Join("-");
				text2 = list2.Join("-");
				this.ExcludeBroadcaster()._PublisherRepository = text;
			}
			return string.Format("当前版本：{0}（{1}）{2}最新版本：{3}（{4}）", new object[]
			{
				text,
				ModBase.GetTimeSpanString(this.ExcludeBroadcaster()._PropertyTest._ListenerRepository - DateTime.Now, false),
				"\r\n",
				text2,
				ModBase.GetTimeSpanString(this.ExcludeBroadcaster().UpdateFile._ListenerRepository - DateTime.Now, false)
			});
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000405D File Offset: 0x0000225D
		public void Refresh()
		{
			ModBase.RunInUi(delegate()
			{
				if (this.ExcludeBroadcaster().SelectMapper())
				{
					this.BtnUpdate.Visibility = Visibility.Visible;
					this.BtnUpdate.ToolTip = string.Format("{0}{1}点击以更新，右键查看更新日志。", this.GetUpdateCompareDescription(), "\r\n");
				}
				else
				{
					this.BtnUpdate.Visibility = Visibility.Collapsed;
				}
				ModMod.McMod.McModState state = this.ExcludeBroadcaster().State;
				string text;
				if (state != ModMod.McMod.McModState.Fine)
				{
					if (state != ModMod.McMod.McModState.Disabled)
					{
						text = ModBase.GetFileNameFromPath(this.ExcludeBroadcaster().Path);
					}
					else
					{
						text = ModBase.GetFileNameWithoutExtentionFromPath(this.ExcludeBroadcaster().Path.Replace(".disabled", "").Replace(".old", ""));
					}
				}
				else
				{
					text = ModBase.GetFileNameWithoutExtentionFromPath(this.ExcludeBroadcaster().Path);
				}
				string text2;
				if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("ToolModLocalNameStyle", null), 1, false))
				{
					this.Title = text;
					this.ConnectBroadcaster("");
					if (this.ExcludeBroadcaster().Comp == null)
					{
						text2 = this.ExcludeBroadcaster().Name;
					}
					else
					{
						KeyValuePair<string, string> controlTitle = this.ExcludeBroadcaster().Comp.GetControlTitle(false);
						text2 = controlTitle.Key + controlTitle.Value;
					}
					text2 = text2.Replace("  |  ", " / ");
					if (this.ExcludeBroadcaster().Version != null)
					{
						text2 += string.Format(" ({0})", this.ExcludeBroadcaster().Version);
					}
				}
				else
				{
					if (this.ExcludeBroadcaster().Comp == null)
					{
						this.Title = this.ExcludeBroadcaster().Name;
						this.ConnectBroadcaster((this.ExcludeBroadcaster().Version == null) ? "" : ("  |  " + this.ExcludeBroadcaster().Version));
					}
					else
					{
						KeyValuePair<string, string> controlTitle2 = this.ExcludeBroadcaster().Comp.GetControlTitle(false);
						this.Title = controlTitle2.Key;
						this.ConnectBroadcaster(controlTitle2.Value + ((this.ExcludeBroadcaster().Version == null) ? "" : ("  |  " + this.ExcludeBroadcaster().Version)));
					}
					text2 = text;
				}
				if (this.ExcludeBroadcaster().Comp != null)
				{
					text2 = text2 + ": " + this.ExcludeBroadcaster().Comp.mappingRepository.Replace("\r", "").Replace("\n", "");
				}
				else if (this.ExcludeBroadcaster().Description != null)
				{
					text2 = text2 + ": " + this.ExcludeBroadcaster().Description.Replace("\r", "").Replace("\n", "");
				}
				else if (!this.ExcludeBroadcaster().SortMapper())
				{
					text2 += ": 存在错误，无法获取信息";
				}
				this.Description = text2;
				if (this.Checked)
				{
					this.LabTitle.SetResourceReference(TextBlock.ForegroundProperty, (this.ExcludeBroadcaster().State == ModMod.McMod.McModState.Fine) ? "ColorBrush2" : "ColorBrush5");
				}
				else
				{
					this.LabTitle.SetResourceReference(TextBlock.ForegroundProperty, (this.ExcludeBroadcaster().State == ModMod.McMod.McModState.Fine) ? "ColorBrush1" : "ColorBrushGray4");
				}
				this.Logo = ((this.ExcludeBroadcaster().Comp == null) ? (ModBase.m_SerializerRepository + "Icons/NoIcon.png") : this.ExcludeBroadcaster().Comp.GetControlLogo());
				if (this.ExcludeBroadcaster().State == ModMod.McMod.McModState.Fine)
				{
					if (this._Utils != null)
					{
						base.Children.Remove(this._Utils);
						this._Utils = null;
					}
				}
				else
				{
					if (this._Utils == null)
					{
						this._Utils = new Image
						{
							Width = 20.0,
							Height = 20.0,
							Margin = new Thickness(0.0, 0.0, -5.0, -3.0),
							IsHitTestVisible = false,
							HorizontalAlignment = HorizontalAlignment.Right,
							VerticalAlignment = VerticalAlignment.Bottom
						};
						RenderOptions.SetBitmapScalingMode(this._Utils, BitmapScalingMode.HighQuality);
						Grid.SetColumn(this._Utils, 1);
						Grid.SetRow(this._Utils, 1);
						Grid.SetRowSpan(this._Utils, 2);
						base.Children.Add(this._Utils);
					}
					this._Utils.Source = new MyBitmap(ModBase.m_SerializerRepository + string.Format("Icons/{0}.png", this.ExcludeBroadcaster().State));
				}
				if (this.ExcludeBroadcaster().Comp != null)
				{
					this.Tags = this.ExcludeBroadcaster().Comp.m_PoolRepository;
				}
			}, false);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0002541C File Offset: 0x0002361C
		public void RefreshColor(object sender, EventArgs e)
		{
			this.InitLate(RuntimeHelpers.GetObjectValue(sender), e);
			int num = base.IsMouseOver ? 120 : 180;
			List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
			checked
			{
				if (this._Order != null)
				{
					if (base.IsMouseOver)
					{
						list.Add(ModAnimation.AaOpacity(this._Order, unchecked(1.0 - this._Order.Opacity), (int)Math.Round(unchecked((double)num * 0.7)), (int)Math.Round(unchecked((double)num * 0.3)), null, false));
						list.Add(ModAnimation.AaDouble(delegate(object i)
						{
							this.ColumnPaddingRight.Width = new GridLength(Conversions.ToDouble(NewLateBinding.LateGet(null, typeof(Math), "Max", new object[]
							{
								0,
								Operators.AddObject(this.ColumnPaddingRight.Width.Value, i)
							}, null, null, null)));
						}, unchecked((double)(checked(5 + Enumerable.Count<MyIconButton>(this.Buttons) * 25)) - this.ColumnPaddingRight.Width.Value), (int)Math.Round(unchecked((double)num * 0.3)), (int)Math.Round(unchecked((double)num * 0.7)), null, false));
					}
					else
					{
						list.Add(ModAnimation.AaOpacity(this._Order, unchecked(-this._Order.Opacity), (int)Math.Round(unchecked((double)num * 0.4)), 0, null, false));
						list.Add(ModAnimation.AaDouble(delegate(object i)
						{
							this.ColumnPaddingRight.Width = new GridLength(Conversions.ToDouble(NewLateBinding.LateGet(null, typeof(Math), "Max", new object[]
							{
								0,
								Operators.AddObject(this.ColumnPaddingRight.Width.Value, i)
							}, null, null, null)));
						}, unchecked(4.0 - this.ColumnPaddingRight.Width.Value), (int)Math.Round(unchecked((double)num * 0.4)), 0, null, false));
					}
				}
			}
			if (!base.IsMouseOver && !this.Checked)
			{
				list.AddRange(new ModAnimation.AniData[]
				{
					ModAnimation.AaOpacity(this.RectBack, -this.RectBack.Opacity, num, 0, null, false),
					ModAnimation.AaScaleTransform(this.RectBack, 0.996 - ((ScaleTransform)this.RectBack.RenderTransform).ScaleX, num, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
					ModAnimation.AaScaleTransform(this.RectBack, -0.196, 1, 0, null, true)
				});
			}
			else
			{
				list.AddRange(new ModAnimation.AniData[]
				{
					ModAnimation.AaColor(this.RectBack, Border.BackgroundProperty, this._Singleton ? "ColorBrush6" : "ColorBrushBg1", num, 0, null, false),
					ModAnimation.AaOpacity(this.RectBack, 1.0 - this.RectBack.Opacity, num, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false)
				});
				if (this._Singleton)
				{
					list.Add(ModAnimation.AaScaleTransform(this.RectBack, 0.996 - ((ScaleTransform)this.RectBack.RenderTransform).ScaleX, checked((int)Math.Round(unchecked((double)num * 1.2))), 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false));
				}
				else
				{
					list.Add(ModAnimation.AaScaleTransform(this.RectBack, 1.0 - ((ScaleTransform)this.RectBack.RenderTransform).ScaleX, checked((int)Math.Round(unchecked((double)num * 1.2))), 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false));
				}
			}
			ModAnimation.AniStart(list, "LocalModItem Color " + Conversions.ToString(this.m_Message), false);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00004071 File Offset: 0x00002271
		private void InitLate(object sender, EventArgs e)
		{
			if (this._Policy != null)
			{
				this._Policy((MyLocalModItem)sender, e);
				this._Policy = null;
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00025758 File Offset: 0x00023958
		private void ShowUpdateLog(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			string text = Enumerable.FirstOrDefault<string>(this.ExcludeBroadcaster().m_IteratorTest, (MyLocalModItem._Closure$__.$I67-0 == null) ? (MyLocalModItem._Closure$__.$I67-0 = ((string x) => x.Contains("curseforge.com"))) : MyLocalModItem._Closure$__.$I67-0);
			string text2 = Enumerable.FirstOrDefault<string>(this.ExcludeBroadcaster().m_IteratorTest, (MyLocalModItem._Closure$__.$I67-1 == null) ? (MyLocalModItem._Closure$__.$I67-1 = ((string x) => x.Contains("modrinth.com"))) : MyLocalModItem._Closure$__.$I67-1);
			if (text == null || text2 == null)
			{
				ModBase.OpenWebsite(Enumerable.First<string>(this.ExcludeBroadcaster().m_IteratorTest));
				return;
			}
			int num = ModMain.MyMsgBox("要在哪个网站上查看更新日志？", "查看更新日志", "Modrinth", "CurseForge", "取消", false, true, false, null, null, null);
			if (num == 1)
			{
				ModBase.OpenWebsite(text2);
				return;
			}
			if (num != 2)
			{
				return;
			}
			ModBase.OpenWebsite(text);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00025830 File Offset: 0x00023A30
		private void BtnUpdate_Click(object sender, EventArgs e)
		{
			if (ModMain.MyMsgBox(string.Format("是否要更新 {0}？{1}{2}{3}", new object[]
			{
				this.ExcludeBroadcaster().Name,
				"\r\n",
				"\r\n",
				this.GetUpdateCompareDescription()
			}), "Mod 更新确认", "更新", "取消", "", false, true, false, null, null, null) != 2)
			{
				ModMain._ThreadRepository.UpdateMods(new ModMod.McMod[]
				{
					this.ExcludeBroadcaster()
				});
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x000258B0 File Offset: 0x00023AB0
		private void PanTitle_SizeChanged()
		{
			if (!this.ColumnExtend.Width.IsStar)
			{
				if (!this.ColumnTitle.Width.IsStar)
				{
					if (this.ColumnSubtitle.ActualWidth < 0.5)
					{
						goto IL_13E;
					}
					if (this.LabSubtitle.IsTextTrimmed())
					{
						return;
					}
				}
				else
				{
					if (this.LabTitle.IsTextTrimmed())
					{
						return;
					}
					switch ((this.LabSubtitle.Visibility != Visibility.Collapsed) ? 1 : 0)
					{
					case 0:
						break;
					case 1:
						goto IL_F7;
					case 2:
						goto IL_13E;
					default:
						return;
					}
				}
				this.ColumnTitle.Width = GridLength.Auto;
				this.ColumnSubtitle.Width = GridLength.Auto;
				this.ColumnExtend.Width = new GridLength(1.0, GridUnitType.Star);
				return;
			}
			if (this.ColumnExtend.ActualWidth >= 0.5)
			{
				return;
			}
			if (this.LabSubtitle.Visibility == Visibility.Collapsed)
			{
				goto IL_13E;
			}
			IL_F7:
			this.ColumnTitle.Width = GridLength.Auto;
			this.ColumnSubtitle.Width = new GridLength(1.0, GridUnitType.Star);
			this.ColumnExtend.Width = new GridLength(0.0, GridUnitType.Pixel);
			return;
			IL_13E:
			this.ColumnTitle.Width = new GridLength(1.0, GridUnitType.Star);
			this.ColumnSubtitle.Width = new GridLength(0.0, GridUnitType.Pixel);
			this.ColumnExtend.Width = new GridLength(0.0, GridUnitType.Pixel);
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00004094 File Offset: 0x00002294
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000409C File Offset: 0x0000229C
		internal virtual MyLocalModItem PanBack { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600035F RID: 863 RVA: 0x000040A5 File Offset: 0x000022A5
		// (set) Token: 0x06000360 RID: 864 RVA: 0x000040AD File Offset: 0x000022AD
		internal virtual ColumnDefinition ColumnPaddingRight { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000361 RID: 865 RVA: 0x000040B6 File Offset: 0x000022B6
		// (set) Token: 0x06000362 RID: 866 RVA: 0x000040BE File Offset: 0x000022BE
		internal virtual MyImage PathLogo { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000363 RID: 867 RVA: 0x000040C7 File Offset: 0x000022C7
		// (set) Token: 0x06000364 RID: 868 RVA: 0x00025A4C File Offset: 0x00023C4C
		internal virtual Grid PanTitle
		{
			[CompilerGenerated]
			get
			{
				return this.strategy;
			}
			[CompilerGenerated]
			set
			{
				SizeChangedEventHandler value2 = delegate(object sender, SizeChangedEventArgs e)
				{
					this.PanTitle_SizeChanged();
				};
				Grid grid = this.strategy;
				if (grid != null)
				{
					grid.SizeChanged -= value2;
				}
				this.strategy = value;
				grid = this.strategy;
				if (grid != null)
				{
					grid.SizeChanged += value2;
				}
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000365 RID: 869 RVA: 0x000040CF File Offset: 0x000022CF
		// (set) Token: 0x06000366 RID: 870 RVA: 0x000040D7 File Offset: 0x000022D7
		internal virtual ColumnDefinition ColumnTitle { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000040E0 File Offset: 0x000022E0
		// (set) Token: 0x06000368 RID: 872 RVA: 0x000040E8 File Offset: 0x000022E8
		internal virtual ColumnDefinition ColumnSubtitle { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000369 RID: 873 RVA: 0x000040F1 File Offset: 0x000022F1
		// (set) Token: 0x0600036A RID: 874 RVA: 0x000040F9 File Offset: 0x000022F9
		internal virtual ColumnDefinition ColumnExtend { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00004102 File Offset: 0x00002302
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000410A File Offset: 0x0000230A
		internal virtual TextBlock LabTitle { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00004113 File Offset: 0x00002313
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000411B File Offset: 0x0000231B
		internal virtual TextBlock LabSubtitle { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00004124 File Offset: 0x00002324
		// (set) Token: 0x06000370 RID: 880 RVA: 0x00025A90 File Offset: 0x00023C90
		internal virtual MyIconButton BtnUpdate
		{
			[CompilerGenerated]
			get
			{
				return this.m_ClientBroadcaster;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.ShowUpdateLog);
				MyIconButton.ClickEventHandler value3 = new MyIconButton.ClickEventHandler(this.BtnUpdate_Click);
				MyIconButton clientBroadcaster = this.m_ClientBroadcaster;
				if (clientBroadcaster != null)
				{
					clientBroadcaster.PreviewMouseRightButtonUp -= value2;
					clientBroadcaster.Click -= value3;
				}
				this.m_ClientBroadcaster = value;
				clientBroadcaster = this.m_ClientBroadcaster;
				if (clientBroadcaster != null)
				{
					clientBroadcaster.PreviewMouseRightButtonUp += value2;
					clientBroadcaster.Click += value3;
				}
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000412C File Offset: 0x0000232C
		// (set) Token: 0x06000372 RID: 882 RVA: 0x00004134 File Offset: 0x00002334
		internal virtual StackPanel PanTags { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000413D File Offset: 0x0000233D
		// (set) Token: 0x06000374 RID: 884 RVA: 0x00004145 File Offset: 0x00002345
		internal virtual TextBlock LabInfo { get; set; }

		// Token: 0x06000375 RID: 885 RVA: 0x00025AF0 File Offset: 0x00023CF0
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._MapperBroadcaster)
			{
				this._MapperBroadcaster = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/modules/minecraft/mylocalmoditem.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00025B20 File Offset: 0x00023D20
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (MyLocalModItem)target;
				return;
			}
			if (connectionId == 2)
			{
				this.ColumnPaddingRight = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 3)
			{
				this.PathLogo = (MyImage)target;
				return;
			}
			if (connectionId == 4)
			{
				this.PanTitle = (Grid)target;
				return;
			}
			if (connectionId == 5)
			{
				this.ColumnTitle = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 6)
			{
				this.ColumnSubtitle = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 7)
			{
				this.ColumnExtend = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 8)
			{
				this.LabTitle = (TextBlock)target;
				return;
			}
			if (connectionId == 9)
			{
				this.LabSubtitle = (TextBlock)target;
				return;
			}
			if (connectionId == 10)
			{
				this.BtnUpdate = (MyIconButton)target;
				return;
			}
			if (connectionId == 11)
			{
				this.PanTags = (StackPanel)target;
				return;
			}
			if (connectionId == 12)
			{
				this.LabInfo = (TextBlock)target;
				return;
			}
			this._MapperBroadcaster = true;
		}

		// Token: 0x040001FA RID: 506
		public int m_Message;

		// Token: 0x040001FB RID: 507
		private string _Creator;

		// Token: 0x040001FC RID: 508
		[CompilerGenerated]
		private MyLocalModItem.ClickEventHandler initializer;

		// Token: 0x040001FD RID: 509
		private bool _Singleton;

		// Token: 0x040001FE RID: 510
		private static int product;

		// Token: 0x040001FF RID: 511
		private static int listener;

		// Token: 0x04000200 RID: 512
		private static bool _Collection = false;

		// Token: 0x04000201 RID: 513
		private static bool _Object;

		// Token: 0x04000202 RID: 514
		[CompilerGenerated]
		private MyLocalModItem.CheckEventHandler m_Bridge;

		// Token: 0x04000203 RID: 515
		[CompilerGenerated]
		private MyLocalModItem.ChangedEventHandler _Reponse;

		// Token: 0x04000204 RID: 516
		private bool global;

		// Token: 0x04000205 RID: 517
		private Image _Utils;

		// Token: 0x04000206 RID: 518
		private Border m_Class;

		// Token: 0x04000207 RID: 519
		public Action<MyLocalModItem, EventArgs> _Policy;

		// Token: 0x04000208 RID: 520
		public FrameworkElement _Order;

		// Token: 0x04000209 RID: 521
		private IEnumerable<MyIconButton> m_Producer;

		// Token: 0x0400020A RID: 522
		private Border _Schema;

		// Token: 0x0400020B RID: 523
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyLocalModItem m_Descriptor;

		// Token: 0x0400020C RID: 524
		[AccessedThroughProperty("ColumnPaddingRight")]
		[CompilerGenerated]
		private ColumnDefinition _Publisher;

		// Token: 0x0400020D RID: 525
		[AccessedThroughProperty("PathLogo")]
		[CompilerGenerated]
		private MyImage _Definition;

		// Token: 0x0400020E RID: 526
		[AccessedThroughProperty("PanTitle")]
		[CompilerGenerated]
		private Grid strategy;

		// Token: 0x0400020F RID: 527
		[CompilerGenerated]
		[AccessedThroughProperty("ColumnTitle")]
		private ColumnDefinition m_Proc;

		// Token: 0x04000210 RID: 528
		[CompilerGenerated]
		[AccessedThroughProperty("ColumnSubtitle")]
		private ColumnDefinition _ParserBroadcaster;

		// Token: 0x04000211 RID: 529
		[CompilerGenerated]
		[AccessedThroughProperty("ColumnExtend")]
		private ColumnDefinition _BroadcasterBroadcaster;

		// Token: 0x04000212 RID: 530
		[AccessedThroughProperty("LabTitle")]
		[CompilerGenerated]
		private TextBlock fieldBroadcaster;

		// Token: 0x04000213 RID: 531
		[AccessedThroughProperty("LabSubtitle")]
		[CompilerGenerated]
		private TextBlock _ReaderBroadcaster;

		// Token: 0x04000214 RID: 532
		[CompilerGenerated]
		[AccessedThroughProperty("BtnUpdate")]
		private MyIconButton m_ClientBroadcaster;

		// Token: 0x04000215 RID: 533
		[AccessedThroughProperty("PanTags")]
		[CompilerGenerated]
		private StackPanel _ConfigBroadcaster;

		// Token: 0x04000216 RID: 534
		[AccessedThroughProperty("LabInfo")]
		[CompilerGenerated]
		private TextBlock _TestsBroadcaster;

		// Token: 0x04000217 RID: 535
		private bool _MapperBroadcaster;

		// Token: 0x02000086 RID: 134
		// (Invoke) Token: 0x06000380 RID: 896
		public delegate void ClickEventHandler(object sender, MouseButtonEventArgs e);

		// Token: 0x02000087 RID: 135
		// (Invoke) Token: 0x06000385 RID: 901
		public delegate void CheckEventHandler(object sender, ModBase.RouteEventArgs e);

		// Token: 0x02000088 RID: 136
		// (Invoke) Token: 0x0600038A RID: 906
		public delegate void ChangedEventHandler(object sender, ModBase.RouteEventArgs e);
	}
}
