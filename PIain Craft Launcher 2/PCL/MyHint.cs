using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x02000093 RID: 147
	[DesignerGenerated]
	public class MyHint : Border, IComponentConnector
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x00027820 File Offset: 0x00025A20
		public MyHint()
		{
			base.Loaded += this.MyHint_Loaded;
			base.MouseLeftButtonUp += this.MyHint_MouseUp;
			base.MouseLeftButtonDown += this.MyHint_MouseDown;
			base.MouseLeave += delegate(object sender, MouseEventArgs e)
			{
				this.MyHint_MouseLeave();
			};
			this.m_ParamBroadcaster = ModBase.GetUuid();
			this.tagBroadcaster = true;
			this.observerBroadcaster = "";
			this.m_StubBroadcaster = false;
			this.InitializeComponent();
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000433B File Offset: 0x0000253B
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x000278A8 File Offset: 0x00025AA8
		public bool IsWarn
		{
			get
			{
				return this.tagBroadcaster;
			}
			set
			{
				if (this.tagBroadcaster != value)
				{
					this.tagBroadcaster = value;
					if (this.tagBroadcaster)
					{
						base.BorderBrush = new ModBase.MyColor("#CCFF4444");
						this.Gradient1.Color = new ModBase.MyColor("#BBFFBBBB");
						this.Gradient2.Color = new ModBase.MyColor("#BBFF8888");
						this.Path.Fill = new ModBase.MyColor("#BF0000");
						this.LabText.Foreground = new ModBase.MyColor("#BF0000");
						this.BtnClose.Foreground = new ModBase.MyColor("#BF0000");
						this.Path.Data = (Geometry)new GeometryConverter().ConvertFromString("F1 M 58.5832,55.4172L 17.4169,55.4171C 15.5619,53.5621 15.5619,50.5546 17.4168,48.6996L 35.201,15.8402C 37.056,13.9852 40.0635,13.9852 41.9185,15.8402L 58.5832,48.6997C 60.4382,50.5546 60.4382,53.5622 58.5832,55.4172 Z M 34.0417,25.7292L 36.0208,41.9584L 39.9791,41.9583L 41.9583,25.7292L 34.0417,25.7292 Z M 38,44.3333C 36.2511,44.3333 34.8333,45.7511 34.8333,47.5C 34.8333,49.2489 36.2511,50.6667 38,50.6667C 39.7489,50.6667 41.1666,49.2489 41.1666,47.5C 41.1666,45.7511 39.7489,44.3333 38,44.3333 Z ");
						return;
					}
					base.BorderBrush = new ModBase.MyColor("#CC4D76FF");
					this.Gradient1.Color = new ModBase.MyColor("#BBB0D0FF");
					this.Gradient2.Color = new ModBase.MyColor("#BB9EBAFF");
					this.Path.Fill = new ModBase.MyColor("#0062BF");
					this.LabText.Foreground = new ModBase.MyColor("#0062BF");
					this.BtnClose.Foreground = new ModBase.MyColor("#0062BF");
					this.Path.Data = (Geometry)new GeometryConverter().ConvertFromString("F1M38,19C48.4934,19 57,27.5066 57,38 57,48.4934 48.4934,57 38,57 27.5066,57 19,48.4934 19,38 19,27.5066 27.5066,19 38,19z M33.25,33.25L33.25,36.4167 36.4166,36.4167 36.4166,47.5 33.25,47.5 33.25,50.6667 44.3333,50.6667 44.3333,47.5 41.1666,47.5 41.1666,36.4167 41.1666,33.25 33.25,33.25z M38.7917,25.3333C37.48,25.3333 36.4167,26.3967 36.4167,27.7083 36.4167,29.02 37.48,30.0833 38.7917,30.0833 40.1033,30.0833 41.1667,29.02 41.1667,27.7083 41.1667,26.3967 40.1033,25.3333 38.7917,25.3333z");
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00004343 File Offset: 0x00002543
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00004350 File Offset: 0x00002550
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

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000435E File Offset: 0x0000255E
		public bool PrepareField()
		{
			return this.BtnClose.Visibility == Visibility.Visible;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000436E File Offset: 0x0000256E
		public void TestField(bool value)
		{
			this.BtnClose.Visibility = (value ? Visibility.Visible : Visibility.Collapsed);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00004382 File Offset: 0x00002582
		private void MyHint_Loaded(object sender, RoutedEventArgs e)
		{
			if (Conversions.ToBoolean(this.PrepareField() && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get(this.observerBroadcaster, null))))
			{
				base.Visibility = Visibility.Collapsed;
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000043B8 File Offset: 0x000025B8
		private void BtnClose_Click(object sender, EventArgs e)
		{
			ModBase.m_IdentifierRepository.Set(this.observerBroadcaster, true, false, null);
			ModAnimation.AniDispose(this, false, null);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00027A40 File Offset: 0x00025C40
		private void MyHint_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (this.m_StubBroadcaster)
			{
				this.m_StubBroadcaster = false;
				ModBase.Log("[Control] 按下提示条" + (string.IsNullOrEmpty(base.Name) ? "" : ("：" + base.Name)), ModBase.LogLevel.Normal, "出现错误");
				e.Handled = true;
				ModEvent.TryStartEvent(this.EventType, this.EventData);
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000043DA File Offset: 0x000025DA
		private void MyHint_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this.m_StubBroadcaster = true;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000043E3 File Offset: 0x000025E3
		private void MyHint_MouseLeave()
		{
			this.m_StubBroadcaster = false;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x000043EC File Offset: 0x000025EC
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x000043FE File Offset: 0x000025FE
		public string EventType
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyHint.rulesBroadcaster));
			}
			set
			{
				base.SetValue(MyHint.rulesBroadcaster, value);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000440C File Offset: 0x0000260C
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0000441E File Offset: 0x0000261E
		public string EventData
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyHint._RefBroadcaster));
			}
			set
			{
				base.SetValue(MyHint._RefBroadcaster, value);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000442C File Offset: 0x0000262C
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00004434 File Offset: 0x00002634
		internal virtual MyHint PanBack { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000443D File Offset: 0x0000263D
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00004445 File Offset: 0x00002645
		internal virtual GradientStop Gradient1 { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000444E File Offset: 0x0000264E
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00004456 File Offset: 0x00002656
		internal virtual GradientStop Gradient2 { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000445F File Offset: 0x0000265F
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x00004467 File Offset: 0x00002667
		internal virtual Path Path { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00004470 File Offset: 0x00002670
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00004478 File Offset: 0x00002678
		internal virtual TextBlock LabText { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00004481 File Offset: 0x00002681
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x00027AB0 File Offset: 0x00025CB0
		internal virtual MyIconButton BtnClose
		{
			[CompilerGenerated]
			get
			{
				return this.m_MethodBroadcaster;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = new MyIconButton.ClickEventHandler(this.BtnClose_Click);
				MyIconButton methodBroadcaster = this.m_MethodBroadcaster;
				if (methodBroadcaster != null)
				{
					methodBroadcaster.Click -= value2;
				}
				this.m_MethodBroadcaster = value;
				methodBroadcaster = this.m_MethodBroadcaster;
				if (methodBroadcaster != null)
				{
					methodBroadcaster.Click += value2;
				}
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00027AF4 File Offset: 0x00025CF4
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.taskBroadcaster)
			{
				this.taskBroadcaster = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/myhint.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00027B24 File Offset: 0x00025D24
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (MyHint)target;
				return;
			}
			if (connectionId == 2)
			{
				this.Gradient1 = (GradientStop)target;
				return;
			}
			if (connectionId == 3)
			{
				this.Gradient2 = (GradientStop)target;
				return;
			}
			if (connectionId == 4)
			{
				this.Path = (Path)target;
				return;
			}
			if (connectionId == 5)
			{
				this.LabText = (TextBlock)target;
				return;
			}
			if (connectionId == 6)
			{
				this.BtnClose = (MyIconButton)target;
				return;
			}
			this.taskBroadcaster = true;
		}

		// Token: 0x04000238 RID: 568
		public int m_ParamBroadcaster;

		// Token: 0x04000239 RID: 569
		private bool tagBroadcaster;

		// Token: 0x0400023A RID: 570
		public string observerBroadcaster;

		// Token: 0x0400023B RID: 571
		private bool m_StubBroadcaster;

		// Token: 0x0400023C RID: 572
		public static readonly DependencyProperty rulesBroadcaster = DependencyProperty.Register("EventType", typeof(string), typeof(MyHint), new PropertyMetadata(null));

		// Token: 0x0400023D RID: 573
		public static readonly DependencyProperty _RefBroadcaster = DependencyProperty.Register("EventData", typeof(string), typeof(MyHint), new PropertyMetadata(null));

		// Token: 0x0400023E RID: 574
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyHint _DecoratorBroadcaster;

		// Token: 0x0400023F RID: 575
		[AccessedThroughProperty("Gradient1")]
		[CompilerGenerated]
		private GradientStop m_InstanceBroadcaster;

		// Token: 0x04000240 RID: 576
		[CompilerGenerated]
		[AccessedThroughProperty("Gradient2")]
		private GradientStop stateBroadcaster;

		// Token: 0x04000241 RID: 577
		[AccessedThroughProperty("Path")]
		[CompilerGenerated]
		private Path m_CallbackBroadcaster;

		// Token: 0x04000242 RID: 578
		[AccessedThroughProperty("LabText")]
		[CompilerGenerated]
		private TextBlock templateBroadcaster;

		// Token: 0x04000243 RID: 579
		[AccessedThroughProperty("BtnClose")]
		[CompilerGenerated]
		private MyIconButton m_MethodBroadcaster;

		// Token: 0x04000244 RID: 580
		private bool taskBroadcaster;
	}
}
