using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x02000094 RID: 148
	[DesignerGenerated]
	public class MyLoading : Grid, IComponentConnector
	{
		// Token: 0x06000405 RID: 1029 RVA: 0x00027BA0 File Offset: 0x00025DA0
		[CompilerGenerated]
		public void PopField(MyLoading.IsErrorChangedEventHandler obj)
		{
			MyLoading.IsErrorChangedEventHandler isErrorChangedEventHandler = this.m_ConsumerBroadcaster;
			MyLoading.IsErrorChangedEventHandler isErrorChangedEventHandler2;
			do
			{
				isErrorChangedEventHandler2 = isErrorChangedEventHandler;
				MyLoading.IsErrorChangedEventHandler value = (MyLoading.IsErrorChangedEventHandler)Delegate.Combine(isErrorChangedEventHandler2, obj);
				isErrorChangedEventHandler = Interlocked.CompareExchange<MyLoading.IsErrorChangedEventHandler>(ref this.m_ConsumerBroadcaster, value, isErrorChangedEventHandler2);
			}
			while (isErrorChangedEventHandler != isErrorChangedEventHandler2);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00027BD8 File Offset: 0x00025DD8
		[CompilerGenerated]
		public void StartField(MyLoading.IsErrorChangedEventHandler obj)
		{
			MyLoading.IsErrorChangedEventHandler isErrorChangedEventHandler = this.m_ConsumerBroadcaster;
			MyLoading.IsErrorChangedEventHandler isErrorChangedEventHandler2;
			do
			{
				isErrorChangedEventHandler2 = isErrorChangedEventHandler;
				MyLoading.IsErrorChangedEventHandler value = (MyLoading.IsErrorChangedEventHandler)Delegate.Remove(isErrorChangedEventHandler2, obj);
				isErrorChangedEventHandler = Interlocked.CompareExchange<MyLoading.IsErrorChangedEventHandler>(ref this.m_ConsumerBroadcaster, value, isErrorChangedEventHandler2);
			}
			while (isErrorChangedEventHandler != isErrorChangedEventHandler2);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00027C10 File Offset: 0x00025E10
		[CompilerGenerated]
		public void PrintField(MyLoading.StateChangedEventHandler obj)
		{
			MyLoading.StateChangedEventHandler stateChangedEventHandler = this.m_ConfigurationBroadcaster;
			MyLoading.StateChangedEventHandler stateChangedEventHandler2;
			do
			{
				stateChangedEventHandler2 = stateChangedEventHandler;
				MyLoading.StateChangedEventHandler value = (MyLoading.StateChangedEventHandler)Delegate.Combine(stateChangedEventHandler2, obj);
				stateChangedEventHandler = Interlocked.CompareExchange<MyLoading.StateChangedEventHandler>(ref this.m_ConfigurationBroadcaster, value, stateChangedEventHandler2);
			}
			while (stateChangedEventHandler != stateChangedEventHandler2);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00027C48 File Offset: 0x00025E48
		[CompilerGenerated]
		public void InterruptField(MyLoading.StateChangedEventHandler obj)
		{
			MyLoading.StateChangedEventHandler stateChangedEventHandler = this.m_ConfigurationBroadcaster;
			MyLoading.StateChangedEventHandler stateChangedEventHandler2;
			do
			{
				stateChangedEventHandler2 = stateChangedEventHandler;
				MyLoading.StateChangedEventHandler value = (MyLoading.StateChangedEventHandler)Delegate.Remove(stateChangedEventHandler2, obj);
				stateChangedEventHandler = Interlocked.CompareExchange<MyLoading.StateChangedEventHandler>(ref this.m_ConfigurationBroadcaster, value, stateChangedEventHandler2);
			}
			while (stateChangedEventHandler != stateChangedEventHandler2);
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000409 RID: 1033 RVA: 0x00027C80 File Offset: 0x00025E80
		// (remove) Token: 0x0600040A RID: 1034 RVA: 0x00027CB8 File Offset: 0x00025EB8
		public event MyLoading.ClickEventHandler Click
		{
			[CompilerGenerated]
			add
			{
				MyLoading.ClickEventHandler clickEventHandler = this.getterBroadcaster;
				MyLoading.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MyLoading.ClickEventHandler value2 = (MyLoading.ClickEventHandler)Delegate.Combine(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MyLoading.ClickEventHandler>(ref this.getterBroadcaster, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				MyLoading.ClickEventHandler clickEventHandler = this.getterBroadcaster;
				MyLoading.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MyLoading.ClickEventHandler value2 = (MyLoading.ClickEventHandler)Delegate.Remove(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MyLoading.ClickEventHandler>(ref this.getterBroadcaster, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x000044BB File Offset: 0x000026BB
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x000044C3 File Offset: 0x000026C3
		public bool AutoRun { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x000044CC File Offset: 0x000026CC
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x000044DE File Offset: 0x000026DE
		public SolidColorBrush Foreground
		{
			get
			{
				return (SolidColorBrush)base.GetValue(MyLoading.m_WriterBroadcaster);
			}
			set
			{
				base.SetValue(MyLoading.m_WriterBroadcaster, value);
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00027CF0 File Offset: 0x00025EF0
		public MyLoading()
		{
			this.PopField(delegate(object a0, bool a1)
			{
				this.RefreshText();
			});
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.RefreshText();
			};
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.InitState();
			};
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.RefreshState();
			};
			base.Unloaded += delegate(object sender, RoutedEventArgs e)
			{
				this.RefreshState();
			};
			base.MouseLeftButtonUp += this.Button_MouseUp;
			base.MouseLeftButtonDown += this.Button_MouseDown;
			base.MouseLeave += new MouseEventHandler(this.Button_MouseLeave);
			base.MouseLeftButtonUp += new MouseButtonEventHandler(this.Button_MouseLeave);
			this.AutoRun = true;
			this._ExpressionBroadcaster = ModBase.GetUuid();
			this.PushField(false);
			this.ruleBroadcaster = "加载中";
			this.proccesorBroadcaster = "加载失败";
			this.TextErrorInherit = true;
			this.AddField(MyLoading.MyLoadingState.Unloaded);
			this.RemoveField(MyLoading.MyLoadingState.Unloaded);
			this.HasAnimation = true;
			this._ConnectionBroadcaster = false;
			this.serverBroadcaster = false;
			this._ResolverBroadcaster = false;
			this.InitializeComponent();
			base.SetResourceReference(MyLoading.m_WriterBroadcaster, "ColorBrush3");
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000044EC File Offset: 0x000026EC
		[CompilerGenerated]
		private bool ViewField()
		{
			return this._RegistryBroadcaster;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000044F4 File Offset: 0x000026F4
		[CompilerGenerated]
		private void PushField(bool AutoPropertyValue)
		{
			this._RegistryBroadcaster = AutoPropertyValue;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x000044FD File Offset: 0x000026FD
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x00004505 File Offset: 0x00002705
		public bool ShowProgress
		{
			get
			{
				return this.ViewField();
			}
			set
			{
				if (this.ViewField() != value)
				{
					this.PushField(value);
					this.RefreshText();
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000451D File Offset: 0x0000271D
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x00004525 File Offset: 0x00002725
		public string Text
		{
			get
			{
				return this.ruleBroadcaster;
			}
			set
			{
				this.ruleBroadcaster = value;
				this.RefreshText();
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00004534 File Offset: 0x00002734
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x0000453C File Offset: 0x0000273C
		public string TextError
		{
			get
			{
				return this.proccesorBroadcaster;
			}
			set
			{
				this.proccesorBroadcaster = value;
				this.RefreshText();
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000454B File Offset: 0x0000274B
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x00004553 File Offset: 0x00002753
		public bool TextErrorInherit { get; set; }

		// Token: 0x0600041A RID: 1050 RVA: 0x0000455C File Offset: 0x0000275C
		private void RefreshText()
		{
			ModBase.RunInUi(delegate()
			{
				if (this.EnableField() == MyLoading.MyLoadingState.Error)
				{
					if (!this.TextErrorInherit || !this.State.IsLoader)
					{
						this.LabText.Text = this.TextError;
						return;
					}
					Exception ex = (Exception)NewLateBinding.LateGet(this.State, null, "Error", new object[0], null, null, null);
					if (ex == null)
					{
						this.LabText.Text = "未知错误";
						return;
					}
					while (ex.InnerException != null)
					{
						ex = ex.InnerException;
					}
					this.LabText.Text = Conversions.ToString(ModBase.StrTrim(ex.Message, true));
					if (Enumerable.Any<string>(new string[]
					{
						"远程主机强迫关闭了",
						"远程方已关闭传输流",
						"未能解析此远程名称",
						"由于目标计算机积极拒绝",
						"操作已超时",
						"操作超时",
						"服务器超时",
						"连接超时"
					}, (string s) => this.LabText.Text.Contains(s)))
					{
						this.LabText.Text = "网络环境不佳，请重试或尝试使用 VPN";
						return;
					}
				}
				else
				{
					if (this.ShowProgress && this.State.IsLoader)
					{
						this.LabText.Text = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(this.Text + " - ", NewLateBinding.LateGet(null, typeof(Math), "Floor", new object[]
						{
							Operators.MultiplyObject(NewLateBinding.LateGet(this.State, null, "Progress", new object[0], null, null, null), 100)
						}, null, null, null)), "%"));
						return;
					}
					this.LabText.Text = this.Text;
				}
			}, false);
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00004570 File Offset: 0x00002770
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x00027E1C File Offset: 0x0002601C
		private virtual ILoadingTrigger _State
		{
			[CompilerGenerated]
			get
			{
				return this.factoryBroadcaster;
			}
			[CompilerGenerated]
			set
			{
				ILoadingTrigger.ProgressChangedEventHandler value2 = delegate(double a0, double a1)
				{
					this.RefreshText();
				};
				ILoadingTrigger.LoadingStateChangedEventHandler value3 = delegate(MyLoading.MyLoadingState a0, MyLoading.MyLoadingState a1)
				{
					this.RefreshState();
				};
				ILoadingTrigger loadingTrigger = this.factoryBroadcaster;
				if (loadingTrigger != null)
				{
					loadingTrigger.ProgressChanged -= value2;
					loadingTrigger.LoadingStateChanged -= value3;
				}
				this.factoryBroadcaster = value;
				loadingTrigger = this.factoryBroadcaster;
				if (loadingTrigger != null)
				{
					loadingTrigger.ProgressChanged += value2;
					loadingTrigger.LoadingStateChanged += value3;
				}
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00004578 File Offset: 0x00002778
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00004586 File Offset: 0x00002786
		public ILoadingTrigger State
		{
			get
			{
				this.InitState();
				return this._State;
			}
			set
			{
				this._State = value;
				this.RefreshState();
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00004595 File Offset: 0x00002795
		private void InitState()
		{
			if (this._State == null)
			{
				this._State = new MyLoadingStateSimulator();
				if (this.AutoRun)
				{
					this._State.LoadingState = MyLoading.MyLoadingState.Run;
				}
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00027E7C File Offset: 0x0002607C
		private void RefreshState()
		{
			if (this._State.LoadingState == MyLoading.MyLoadingState.Run && !base.IsLoaded)
			{
				this.CollectField(MyLoading.MyLoadingState.Stop);
			}
			this.CollectField(this._State.LoadingState);
			this.FlushField(this._State.LoadingState);
			this.AniLoop();
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000045BE File Offset: 0x000027BE
		[CompilerGenerated]
		private MyLoading.MyLoadingState SearchField()
		{
			return this._ExporterBroadcaster;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000045C6 File Offset: 0x000027C6
		[CompilerGenerated]
		private void AddField(MyLoading.MyLoadingState AutoPropertyValue)
		{
			this._ExporterBroadcaster = AutoPropertyValue;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000045CF File Offset: 0x000027CF
		private MyLoading.MyLoadingState PublishField()
		{
			return this.SearchField();
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00027ED0 File Offset: 0x000260D0
		private void FlushField(MyLoading.MyLoadingState value)
		{
			if (this.SearchField() != value)
			{
				MyLoading.MyLoadingState myLoadingState = this.SearchField();
				this.AddField(value);
				MyLoading.StateChangedEventHandler configurationBroadcaster = this.m_ConfigurationBroadcaster;
				if (configurationBroadcaster != null)
				{
					configurationBroadcaster(this, value, myLoadingState);
				}
				if (myLoadingState == MyLoading.MyLoadingState.Error != (value == MyLoading.MyLoadingState.Error))
				{
					MyLoading.IsErrorChangedEventHandler consumerBroadcaster = this.m_ConsumerBroadcaster;
					if (consumerBroadcaster != null)
					{
						consumerBroadcaster(this, value == MyLoading.MyLoadingState.Error);
					}
				}
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000045D7 File Offset: 0x000027D7
		[CompilerGenerated]
		private MyLoading.MyLoadingState ListField()
		{
			return this.importerBroadcaster;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000045DF File Offset: 0x000027DF
		[CompilerGenerated]
		private void RemoveField(MyLoading.MyLoadingState AutoPropertyValue)
		{
			this.importerBroadcaster = AutoPropertyValue;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000045E8 File Offset: 0x000027E8
		private MyLoading.MyLoadingState EnableField()
		{
			return this.ListField();
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000045F0 File Offset: 0x000027F0
		private void CollectField(MyLoading.MyLoadingState value)
		{
			if (this.ListField() != value)
			{
				int num = (int)this.ListField();
				this.RemoveField(value);
				this.AniLoop();
				if (num == 2 != (value == MyLoading.MyLoadingState.Error))
				{
					this.ErrorAnimation(this, value == MyLoading.MyLoadingState.Error);
				}
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00004622 File Offset: 0x00002822
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x0000462A File Offset: 0x0000282A
		public bool HasAnimation { get; set; }

		// Token: 0x0600042B RID: 1067 RVA: 0x00027F28 File Offset: 0x00026128
		private void AniLoop()
		{
			if (this.HasAnimation && !this._ConnectionBroadcaster && this.EnableField() == MyLoading.MyLoadingState.Run && ModAnimation.m_Task <= 10.0 && base.IsLoaded)
			{
				this._ConnectionBroadcaster = true;
				this.serverBroadcaster = true;
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaRotateTransform(this.PathPickaxe, -20.0 - ((RotateTransform)this.PathPickaxe.RenderTransform).Angle, 350, 250, new ModAnimation.AniEaseInBack(ModAnimation.AniEasePower.Weak), false),
					ModAnimation.AaRotateTransform(this.PathPickaxe, 50.0, 900, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), true),
					ModAnimation.AaRotateTransform(this.PathPickaxe, 25.0, 900, 0, new ModAnimation.AniEaseOutElastic(ModAnimation.AniEasePower.Weak), false),
					ModAnimation.AaCode(delegate
					{
						this.PathLeft.Opacity = 1.0;
						this.PathLeft.Margin = new Thickness(7.0, 41.0, 0.0, 0.0);
						this.PathRight.Opacity = 1.0;
						this.PathRight.Margin = new Thickness(14.0, 41.0, 0.0, 0.0);
						this.serverBroadcaster = false;
					}, 0, false),
					ModAnimation.AaOpacity(this.PathLeft, -1.0, 100, 50, null, false),
					ModAnimation.AaX(this.PathLeft, -5.0, 180, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
					ModAnimation.AaY(this.PathLeft, -6.0, 180, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
					ModAnimation.AaOpacity(this.PathRight, -1.0, 100, 50, null, false),
					ModAnimation.AaX(this.PathRight, 5.0, 180, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
					ModAnimation.AaY(this.PathRight, -6.0, 180, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
					ModAnimation.AaCode(delegate
					{
						this._ConnectionBroadcaster = false;
						this.AniLoop();
					}, 0, true)
				}, "MyLoader Loop " + Conversions.ToString(this._ExpressionBroadcaster) + "/" + Conversions.ToString(ModBase.GetUuid()), false);
				bool showProgress = this.ShowProgress;
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00028164 File Offset: 0x00026364
		private void ErrorAnimation(object sender, bool isError)
		{
			if (isError)
			{
				int num = this.serverBroadcaster ? 400 : 0;
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaColor(this.PanBack, MyLoading.m_WriterBroadcaster, "ColorBrushRedLight", 300, 0, null, false),
					ModAnimation.AaOpacity(this.PathError, 1.0 - this.PathError.Opacity, 100, checked(300 + num), null, false),
					ModAnimation.AaScaleTransform(this.PathError, 1.0 - ((ScaleTransform)this.PathError.RenderTransform).ScaleX, 400, checked(300 + num), new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Middle), false)
				}, "MyLoader Error " + Conversions.ToString(this._ExpressionBroadcaster), false);
				return;
			}
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaOpacity(this.PathError, -this.PathError.Opacity, 100, 0, null, false),
				ModAnimation.AaScaleTransform(this.PathError, 0.5 - ((ScaleTransform)this.PathError.RenderTransform).ScaleX, 200, 0, null, false),
				ModAnimation.AaColor(this.PanBack, MyLoading.m_WriterBroadcaster, "ColorBrush3", 300, 0, null, false)
			}, "MyLoader Error " + Conversions.ToString(this._ExpressionBroadcaster), false);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000282E8 File Offset: 0x000264E8
		private void Button_MouseUp(object sender, MouseButtonEventArgs e)
		{
			MyLoading.ClickEventHandler clickEventHandler = this.getterBroadcaster;
			if (clickEventHandler != null)
			{
				clickEventHandler(RuntimeHelpers.GetObjectValue(sender), e);
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00004633 File Offset: 0x00002833
		private void Button_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this._ResolverBroadcaster = true;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000463C File Offset: 0x0000283C
		private void Button_MouseLeave(object sender, object e)
		{
			this._ResolverBroadcaster = false;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00004645 File Offset: 0x00002845
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x0000464D File Offset: 0x0000284D
		internal virtual MyLoading PanBack { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00004656 File Offset: 0x00002856
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x0000465E File Offset: 0x0000285E
		internal virtual Path PathPickaxe { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00004667 File Offset: 0x00002867
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x0000466F File Offset: 0x0000286F
		internal virtual Path PathLeft { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00004678 File Offset: 0x00002878
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x00004680 File Offset: 0x00002880
		internal virtual Path PathRight { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00004689 File Offset: 0x00002889
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x00004691 File Offset: 0x00002891
		internal virtual Path PathError { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000469A File Offset: 0x0000289A
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x000046A2 File Offset: 0x000028A2
		internal virtual TextBlock LabText { get; set; }

		// Token: 0x0600043C RID: 1084 RVA: 0x0002830C File Offset: 0x0002650C
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_CandidateBroadcaster)
			{
				this.m_CandidateBroadcaster = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/myloading.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0002833C File Offset: 0x0002653C
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (MyLoading)target;
				return;
			}
			if (connectionId == 2)
			{
				this.PathPickaxe = (Path)target;
				return;
			}
			if (connectionId == 3)
			{
				this.PathLeft = (Path)target;
				return;
			}
			if (connectionId == 4)
			{
				this.PathRight = (Path)target;
				return;
			}
			if (connectionId == 5)
			{
				this.PathError = (Path)target;
				return;
			}
			if (connectionId == 6)
			{
				this.LabText = (TextBlock)target;
				return;
			}
			this.m_CandidateBroadcaster = true;
		}

		// Token: 0x04000245 RID: 581
		[CompilerGenerated]
		private MyLoading.IsErrorChangedEventHandler m_ConsumerBroadcaster;

		// Token: 0x04000246 RID: 582
		[CompilerGenerated]
		private MyLoading.StateChangedEventHandler m_ConfigurationBroadcaster;

		// Token: 0x04000247 RID: 583
		[CompilerGenerated]
		private MyLoading.ClickEventHandler getterBroadcaster;

		// Token: 0x04000248 RID: 584
		[CompilerGenerated]
		private bool _TokenBroadcaster;

		// Token: 0x04000249 RID: 585
		private int _ExpressionBroadcaster;

		// Token: 0x0400024A RID: 586
		public static readonly DependencyProperty m_WriterBroadcaster = DependencyProperty.Register("Foreground", typeof(SolidColorBrush), typeof(MyLoading));

		// Token: 0x0400024B RID: 587
		[CompilerGenerated]
		private bool _RegistryBroadcaster;

		// Token: 0x0400024C RID: 588
		private string ruleBroadcaster;

		// Token: 0x0400024D RID: 589
		private string proccesorBroadcaster;

		// Token: 0x0400024E RID: 590
		[CompilerGenerated]
		private bool m_SetterBroadcaster;

		// Token: 0x0400024F RID: 591
		[AccessedThroughProperty("_State")]
		[CompilerGenerated]
		private ILoadingTrigger factoryBroadcaster;

		// Token: 0x04000250 RID: 592
		[CompilerGenerated]
		private MyLoading.MyLoadingState _ExporterBroadcaster;

		// Token: 0x04000251 RID: 593
		[CompilerGenerated]
		private MyLoading.MyLoadingState importerBroadcaster;

		// Token: 0x04000252 RID: 594
		[CompilerGenerated]
		private bool m_WorkerBroadcaster;

		// Token: 0x04000253 RID: 595
		private bool _ConnectionBroadcaster;

		// Token: 0x04000254 RID: 596
		private bool serverBroadcaster;

		// Token: 0x04000255 RID: 597
		private bool _ResolverBroadcaster;

		// Token: 0x04000256 RID: 598
		[CompilerGenerated]
		[AccessedThroughProperty("PanBack")]
		private MyLoading _StatusBroadcaster;

		// Token: 0x04000257 RID: 599
		[AccessedThroughProperty("PathPickaxe")]
		[CompilerGenerated]
		private Path roleBroadcaster;

		// Token: 0x04000258 RID: 600
		[CompilerGenerated]
		[AccessedThroughProperty("PathLeft")]
		private Path structBroadcaster;

		// Token: 0x04000259 RID: 601
		[CompilerGenerated]
		[AccessedThroughProperty("PathRight")]
		private Path printerBroadcaster;

		// Token: 0x0400025A RID: 602
		[CompilerGenerated]
		[AccessedThroughProperty("PathError")]
		private Path valBroadcaster;

		// Token: 0x0400025B RID: 603
		[AccessedThroughProperty("LabText")]
		[CompilerGenerated]
		private TextBlock _AttrBroadcaster;

		// Token: 0x0400025C RID: 604
		private bool m_CandidateBroadcaster;

		// Token: 0x02000095 RID: 149
		// (Invoke) Token: 0x0600044C RID: 1100
		public delegate void IsErrorChangedEventHandler(object sender, bool isError);

		// Token: 0x02000096 RID: 150
		// (Invoke) Token: 0x06000451 RID: 1105
		public delegate void StateChangedEventHandler(object sender, MyLoading.MyLoadingState newState, MyLoading.MyLoadingState oldState);

		// Token: 0x02000097 RID: 151
		// (Invoke) Token: 0x06000456 RID: 1110
		public delegate void ClickEventHandler(object sender, MouseButtonEventArgs e);

		// Token: 0x02000098 RID: 152
		public enum MyLoadingState
		{
			// Token: 0x0400025E RID: 606
			Unloaded = -1,
			// Token: 0x0400025F RID: 607
			Run,
			// Token: 0x04000260 RID: 608
			Stop,
			// Token: 0x04000261 RID: 609
			Error
		}
	}
}
