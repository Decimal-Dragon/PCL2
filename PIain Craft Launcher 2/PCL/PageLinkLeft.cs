using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x0200010B RID: 267
	[DesignerGenerated]
	public class PageLinkLeft : MyPageLeft, IComponentConnector
	{
		// Token: 0x06000AB1 RID: 2737 RVA: 0x0004B86C File Offset: 0x00049A6C
		public PageLinkLeft()
		{
			base.Loaded += this.PageLinkLeft_Loaded;
			base.Unloaded += this.PageOtherLeft_Unloaded;
			this.watcherConfig = false;
			this._IdentifierConfig = false;
			this._SystemConfig = FormMain.PageSubType.DownloadInstall;
			this.InitializeComponent();
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x000077F5 File Offset: 0x000059F5
		private void PageLinkLeft_Loaded(object sender, RoutedEventArgs e)
		{
			if (!this.watcherConfig)
			{
				this.watcherConfig = true;
				if (!this._IdentifierConfig)
				{
					this.ItemHiper.SetChecked(true, false, false);
				}
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0000781C File Offset: 0x00005A1C
		private void PageOtherLeft_Unloaded(object sender, RoutedEventArgs e)
		{
			this._IdentifierConfig = false;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00007825 File Offset: 0x00005A25
		private void PageCheck(MyListItem sender, ModBase.RouteEventArgs e)
		{
			if (sender.Tag != null)
			{
				this.PageChange(checked((FormMain.PageSubType)Math.Round(ModBase.Val(RuntimeHelpers.GetObjectValue(sender.Tag)))));
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0004B8C0 File Offset: 0x00049AC0
		public object PageGet(FormMain.PageSubType ID = (FormMain.PageSubType)(-1))
		{
			if (ID == (FormMain.PageSubType)(-1))
			{
				ID = this._SystemConfig;
			}
			switch (ID)
			{
			case FormMain.PageSubType.Default:
			case FormMain.PageSubType.DownloadInstall:
				if (ModMain.m_RegIterator == null)
				{
					ModMain.m_RegIterator = new PageLinkHiper();
				}
				return ModMain.m_RegIterator;
			case FormMain.PageSubType.SetupSystem:
				if (ModMain.m_SingletonIterator == null)
				{
					ModMain.m_SingletonIterator = new PageLinkIoi();
				}
				return ModMain.m_SingletonIterator;
			case FormMain.PageSubType.DownloadClient:
				if (ModMain.m_SchemaIterator == null)
				{
					ModMain.m_SchemaIterator = new PageSetupLink();
				}
				return ModMain.m_SchemaIterator;
			case FormMain.PageSubType.DownloadOptiFine:
				if (ModMain._ProductIterator == null)
				{
					ModMain._ProductIterator = PageOtherHelp.GetHelpPage(ModBase.m_DecoratorRepository + "Help\\启动器\\联机.json");
				}
				return ModMain._ProductIterator;
			case FormMain.PageSubType.DownloadForge:
				if (ModMain.m_ListenerIterator == null)
				{
					ModMain.m_ListenerIterator = new PageLinkFeedback();
				}
				return ModMain.m_ListenerIterator;
			}
			throw new Exception("未知的更多子页面种类：" + Conversions.ToString((int)ID));
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0004B9A4 File Offset: 0x00049BA4
		public void PageChange(FormMain.PageSubType ID)
		{
			checked
			{
				if (this._SystemConfig != ID)
				{
					ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
					this._IdentifierConfig = true;
					try
					{
						PageLinkLeft.PageChangeRun((MyPageRight)this.PageGet(ID));
						this._SystemConfig = ID;
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "切换分页面失败（ID " + Conversions.ToString((int)ID) + "）", ModBase.LogLevel.Feedback, "出现错误");
					}
					finally
					{
						ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
					}
				}
			}
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0004BA40 File Offset: 0x00049C40
		private static void PageChangeRun(MyPageRight Target)
		{
			ModAnimation.AniStop("FrmMain PageChangeRight");
			if (Target.Parent != null)
			{
				Target.SetValue(ContentPresenter.ContentProperty, null);
			}
			ModMain._ProcessIterator._GetterIterator = Target;
			((MyPageRight)ModMain._ProcessIterator.PanMainRight.Child).PageOnExit();
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaCode((PageLinkLeft._Closure$__.$I9-0 == null) ? (PageLinkLeft._Closure$__.$I9-0 = delegate()
				{
					((MyPageRight)ModMain._ProcessIterator.PanMainRight.Child).PageOnForceExit();
					ModMain._ProcessIterator.PanMainRight.Child = ModMain._ProcessIterator._GetterIterator;
					ModMain._ProcessIterator._GetterIterator.Opacity = 0.0;
				}) : PageLinkLeft._Closure$__.$I9-0, 130, false),
				ModAnimation.AaCode((PageLinkLeft._Closure$__.$I9-1 == null) ? (PageLinkLeft._Closure$__.$I9-1 = delegate()
				{
					ModMain._ProcessIterator._GetterIterator.Opacity = 1.0;
					ModMain._ProcessIterator._GetterIterator.PageOnEnter();
				}) : PageLinkLeft._Closure$__.$I9-1, 30, true)
			}, "PageLeft PageChange", false);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0004BB0C File Offset: 0x00049D0C
		public void Reset(object sender, EventArgs e)
		{
			if (ModMain.MyMsgBox("是否要初始化联机页的所有设置？该操作不可撤销。", "初始化确认", "确定", "取消", "", true, true, false, null, null, null) == 1)
			{
				if (Information.IsNothing(ModMain.m_SchemaIterator))
				{
					ModMain.m_SchemaIterator = new PageSetupLink();
				}
				ModMain.m_SchemaIterator.Reset();
				this.ItemSetup.Checked = true;
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0004BB6C File Offset: 0x00049D6C
		private void BtnHiperStop_Loaded(object sender, RoutedEventArgs e)
		{
			NewLateBinding.LateSet(sender, null, "Visibility", new object[]
			{
				(PageLinkHiper.EnableReader() == ModBase.LoadState.Finished || PageLinkHiper.EnableReader() == ModBase.LoadState.Loading) ? Visibility.Visible : Visibility.Collapsed
			}, null, null);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0000784B File Offset: 0x00005A4B
		private void BtnHiperStop_Click(object sender, EventArgs e)
		{
			PageLinkHiper.ModuleStopManually();
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000063A1 File Offset: 0x000045A1
		private void BtnIoiStop_Loaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00007852 File Offset: 0x00005A52
		private void BtnIoiStop_Click(object sender, EventArgs e)
		{
			PageLinkIoi.ModuleStopManually();
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00007859 File Offset: 0x00005A59
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x00007861 File Offset: 0x00005A61
		internal virtual StackPanel PanItem { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0000786A File Offset: 0x00005A6A
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x0004BBAC File Offset: 0x00049DAC
		internal virtual MyListItem ItemHiper
		{
			[CompilerGenerated]
			get
			{
				return this._TagConfig;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem tagConfig = this._TagConfig;
				if (tagConfig != null)
				{
					tagConfig.Check -= value2;
				}
				this._TagConfig = value;
				tagConfig = this._TagConfig;
				if (tagConfig != null)
				{
					tagConfig.Check += value2;
				}
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00007872 File Offset: 0x00005A72
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x0004BBF0 File Offset: 0x00049DF0
		internal virtual MyListItem ItemIoi
		{
			[CompilerGenerated]
			get
			{
				return this._ObserverConfig;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem observerConfig = this._ObserverConfig;
				if (observerConfig != null)
				{
					observerConfig.Check -= value2;
				}
				this._ObserverConfig = value;
				observerConfig = this._ObserverConfig;
				if (observerConfig != null)
				{
					observerConfig.Check += value2;
				}
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0000787A File Offset: 0x00005A7A
		// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x0004BC34 File Offset: 0x00049E34
		internal virtual MyListItem ItemSetup
		{
			[CompilerGenerated]
			get
			{
				return this.m_StubConfig;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem stubConfig = this.m_StubConfig;
				if (stubConfig != null)
				{
					stubConfig.Check -= value2;
				}
				this.m_StubConfig = value;
				stubConfig = this.m_StubConfig;
				if (stubConfig != null)
				{
					stubConfig.Check += value2;
				}
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x00007882 File Offset: 0x00005A82
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x0004BC78 File Offset: 0x00049E78
		internal virtual MyListItem ItemHelp
		{
			[CompilerGenerated]
			get
			{
				return this._RulesConfig;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem rulesConfig = this._RulesConfig;
				if (rulesConfig != null)
				{
					rulesConfig.Check -= value2;
				}
				this._RulesConfig = value;
				rulesConfig = this._RulesConfig;
				if (rulesConfig != null)
				{
					rulesConfig.Check += value2;
				}
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0000788A File Offset: 0x00005A8A
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x0004BCBC File Offset: 0x00049EBC
		internal virtual MyListItem ItemFeedback
		{
			[CompilerGenerated]
			get
			{
				return this.m_RefConfig;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem refConfig = this.m_RefConfig;
				if (refConfig != null)
				{
					refConfig.Check -= value2;
				}
				this.m_RefConfig = value;
				refConfig = this.m_RefConfig;
				if (refConfig != null)
				{
					refConfig.Check += value2;
				}
			}
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0004BD00 File Offset: 0x00049F00
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_DecoratorConfig)
			{
				this.m_DecoratorConfig = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelink/pagelinkleft.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0004BD30 File Offset: 0x00049F30
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanItem = (StackPanel)target;
				return;
			}
			if (connectionId == 2)
			{
				this.ItemHiper = (MyListItem)target;
				return;
			}
			if (connectionId == 3)
			{
				this.ItemIoi = (MyListItem)target;
				return;
			}
			if (connectionId == 4)
			{
				this.ItemSetup = (MyListItem)target;
				return;
			}
			if (connectionId == 5)
			{
				this.ItemHelp = (MyListItem)target;
				return;
			}
			if (connectionId == 6)
			{
				this.ItemFeedback = (MyListItem)target;
				return;
			}
			this.m_DecoratorConfig = true;
		}

		// Token: 0x04000589 RID: 1417
		private bool watcherConfig;

		// Token: 0x0400058A RID: 1418
		private bool _IdentifierConfig;

		// Token: 0x0400058B RID: 1419
		public FormMain.PageSubType _SystemConfig;

		// Token: 0x0400058C RID: 1420
		[AccessedThroughProperty("PanItem")]
		[CompilerGenerated]
		private StackPanel _ParamConfig;

		// Token: 0x0400058D RID: 1421
		[CompilerGenerated]
		[AccessedThroughProperty("ItemHiper")]
		private MyListItem _TagConfig;

		// Token: 0x0400058E RID: 1422
		[CompilerGenerated]
		[AccessedThroughProperty("ItemIoi")]
		private MyListItem _ObserverConfig;

		// Token: 0x0400058F RID: 1423
		[CompilerGenerated]
		[AccessedThroughProperty("ItemSetup")]
		private MyListItem m_StubConfig;

		// Token: 0x04000590 RID: 1424
		[AccessedThroughProperty("ItemHelp")]
		[CompilerGenerated]
		private MyListItem _RulesConfig;

		// Token: 0x04000591 RID: 1425
		[CompilerGenerated]
		[AccessedThroughProperty("ItemFeedback")]
		private MyListItem m_RefConfig;

		// Token: 0x04000592 RID: 1426
		private bool m_DecoratorConfig;
	}
}
