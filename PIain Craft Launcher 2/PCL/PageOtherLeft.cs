using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020001A9 RID: 425
	[DesignerGenerated]
	public class PageOtherLeft : MyPageLeft, IComponentConnector
	{
		// Token: 0x06001182 RID: 4482 RVA: 0x0007D5E0 File Offset: 0x0007B7E0
		private void PageOtherLeft_Loaded(object sender, RoutedEventArgs e)
		{
			bool flag = false;
			if (Conversions.ToBoolean(this.ItemHelp.Checked && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherHelp", null))))
			{
				flag = true;
			}
			if (Conversions.ToBoolean(this.ItemAbout.Checked && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherAbout", null))))
			{
				flag = true;
			}
			if (Conversions.ToBoolean(this.ItemTest.Checked && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherTest", null))))
			{
				flag = true;
			}
			if (PageSetupUI.CreateClient())
			{
				flag = false;
			}
			if (!this._GetterThread || flag)
			{
				this._GetterThread = true;
				PageSetupUI.HiddenRefresh();
				if (!this.tokenThread)
				{
					if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenOtherHelp", null))))
					{
						this.ItemHelp.SetChecked(true, false, false);
						return;
					}
					if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenOtherAbout", null))))
					{
						this.ItemAbout.SetChecked(true, false, false);
						return;
					}
					this.ItemTest.SetChecked(true, false, false);
				}
			}
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0000A98E File Offset: 0x00008B8E
		private void PageOtherLeft_Unloaded(object sender, RoutedEventArgs e)
		{
			this.tokenThread = false;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0007D714 File Offset: 0x0007B914
		public PageOtherLeft()
		{
			base.Loaded += this.PageOtherLeft_Loaded;
			base.Unloaded += this.PageOtherLeft_Unloaded;
			this._GetterThread = false;
			this.tokenThread = false;
			this.InitializeComponent();
			if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenOtherHelp", null))))
			{
				this.expressionThread = FormMain.PageSubType.Default;
				return;
			}
			if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenOtherAbout", null))))
			{
				this.expressionThread = FormMain.PageSubType.DownloadInstall;
				return;
			}
			this.expressionThread = FormMain.PageSubType.SetupSystem;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0000A997 File Offset: 0x00008B97
		private void PageCheck(MyListItem sender, ModBase.RouteEventArgs e)
		{
			if (sender.Tag != null)
			{
				this.PageChange(checked((FormMain.PageSubType)Math.Round(ModBase.Val(RuntimeHelpers.GetObjectValue(sender.Tag)))));
			}
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0007D7B0 File Offset: 0x0007B9B0
		public object PageGet(FormMain.PageSubType ID = (FormMain.PageSubType)(-1))
		{
			if (ID == (FormMain.PageSubType)(-1))
			{
				ID = this.expressionThread;
			}
			object result;
			switch (ID)
			{
			case FormMain.PageSubType.Default:
				if (ModMain.m_PublisherIterator == null)
				{
					ModMain.m_PublisherIterator = new PageOtherHelp();
				}
				result = ModMain.m_PublisherIterator;
				break;
			case FormMain.PageSubType.DownloadInstall:
				if (ModMain._DefinitionIterator == null)
				{
					ModMain._DefinitionIterator = new PageOtherAbout();
				}
				result = ModMain._DefinitionIterator;
				break;
			case FormMain.PageSubType.SetupSystem:
				if (ModMain._StrategyIterator == null)
				{
					ModMain._StrategyIterator = new PageOtherTest();
				}
				result = ModMain._StrategyIterator;
				break;
			default:
				throw new Exception("未知的更多子页面种类：" + Conversions.ToString((int)ID));
			}
			return result;
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0007D840 File Offset: 0x0007BA40
		public void PageChange(FormMain.PageSubType ID)
		{
			checked
			{
				if (this.expressionThread != ID)
				{
					ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
					this.tokenThread = true;
					try
					{
						PageOtherLeft.PageChangeRun((MyPageRight)this.PageGet(ID));
						this.expressionThread = ID;
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

		// Token: 0x06001188 RID: 4488 RVA: 0x0007D8DC File Offset: 0x0007BADC
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
				ModAnimation.AaCode((PageOtherLeft._Closure$__.$I9-0 == null) ? (PageOtherLeft._Closure$__.$I9-0 = delegate()
				{
					((MyPageRight)ModMain._ProcessIterator.PanMainRight.Child).PageOnForceExit();
					ModMain._ProcessIterator.PanMainRight.Child = ModMain._ProcessIterator._GetterIterator;
					ModMain._ProcessIterator._GetterIterator.Opacity = 0.0;
				}) : PageOtherLeft._Closure$__.$I9-0, 130, false),
				ModAnimation.AaCode((PageOtherLeft._Closure$__.$I9-1 == null) ? (PageOtherLeft._Closure$__.$I9-1 = delegate()
				{
					ModMain._ProcessIterator._GetterIterator.Opacity = 1.0;
					ModMain._ProcessIterator._GetterIterator.PageOnEnter();
				}) : PageOtherLeft._Closure$__.$I9-1, 30, true)
			}, "PageLeft PageChange", false);
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0007D9A8 File Offset: 0x0007BBA8
		public void Refresh(object sender, EventArgs e)
		{
			if (ModBase.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null))) == 0.0)
			{
				PageOtherLeft.RefreshHelp();
				this.ItemHelp.Checked = true;
			}
			ModMain.Hint("正在刷新……", ModMain.HintType.Info, false);
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0000A9BD File Offset: 0x00008BBD
		public static void RefreshHelp()
		{
			ModBase.m_IdentifierRepository.Set("SystemHelpVersion", 0, false, null);
			ModMain.m_PublisherIterator.PageLoaderRestart(null, true);
			ModMain.m_PublisherIterator.SearchBox.Text = "";
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0000A9F6 File Offset: 0x00008BF6
		private void TryFeedback(object sender, ModBase.RouteEventArgs e)
		{
			if (this.ItemFeedback.Checked)
			{
				PageOtherLeft.TryFeedback();
				e.m_SerializerError = true;
			}
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0007D9FC File Offset: 0x0007BBFC
		public static void TryFeedback()
		{
			if (ModBase.CanFeedback(true))
			{
				int num = ModMain.MyMsgBox("在提交新反馈前，建议先搜索反馈列表，以避免重复提交。\r\n如果无法打开该网页，请尝试使用加速器或 VPN。", "反馈", "提交新反馈", "查看反馈列表", "取消", false, true, false, null, null, null);
				if (num == 1)
				{
					ModBase.Feedback(true, false);
					return;
				}
				if (num != 2)
				{
					return;
				}
				ModBase.OpenWebsite("https://github.com/Hex-Dragon/PCL2/issues/");
			}
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0000AA11 File Offset: 0x00008C11
		private void TryVote(object sender, ModBase.RouteEventArgs e)
		{
			if (this.ItemVote.Checked)
			{
				PageOtherLeft.TryVote();
				e.m_SerializerError = true;
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0007DA54 File Offset: 0x0007BC54
		public static void TryVote()
		{
			if (ModMain.MyMsgBox("是否要打开新功能投票网页？\r\n如果无法打开该网页，请尝试使用加速器或 VPN。", "新功能投票", "打开", "取消", "", false, true, false, null, null, null) != 2)
			{
				ModBase.OpenWebsite("https://github.com/Hex-Dragon/PCL2/discussions/categories/%E5%8A%9F%E8%83%BD%E6%8A%95%E7%A5%A8?discussions_q=category%3A%E5%8A%9F%E8%83%BD%E6%8A%95%E7%A5%A8+sort%3Adate_created");
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x0000AA2C File Offset: 0x00008C2C
		// (set) Token: 0x06001190 RID: 4496 RVA: 0x0000AA34 File Offset: 0x00008C34
		internal virtual StackPanel PanItem { get; set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x0000AA3D File Offset: 0x00008C3D
		// (set) Token: 0x06001192 RID: 4498 RVA: 0x0007DA94 File Offset: 0x0007BC94
		internal virtual MyListItem ItemHelp
		{
			[CompilerGenerated]
			get
			{
				return this._RegistryThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem registryThread = this._RegistryThread;
				if (registryThread != null)
				{
					registryThread.Check -= value2;
				}
				this._RegistryThread = value;
				registryThread = this._RegistryThread;
				if (registryThread != null)
				{
					registryThread.Check += value2;
				}
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x0000AA45 File Offset: 0x00008C45
		// (set) Token: 0x06001194 RID: 4500 RVA: 0x0007DAD8 File Offset: 0x0007BCD8
		internal virtual MyListItem ItemAbout
		{
			[CompilerGenerated]
			get
			{
				return this.ruleThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem myListItem = this.ruleThread;
				if (myListItem != null)
				{
					myListItem.Check -= value2;
				}
				this.ruleThread = value;
				myListItem = this.ruleThread;
				if (myListItem != null)
				{
					myListItem.Check += value2;
				}
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x0000AA4D File Offset: 0x00008C4D
		// (set) Token: 0x06001196 RID: 4502 RVA: 0x0007DB1C File Offset: 0x0007BD1C
		internal virtual MyListItem ItemTest
		{
			[CompilerGenerated]
			get
			{
				return this.proccesorThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem myListItem = this.proccesorThread;
				if (myListItem != null)
				{
					myListItem.Check -= value2;
				}
				this.proccesorThread = value;
				myListItem = this.proccesorThread;
				if (myListItem != null)
				{
					myListItem.Check += value2;
				}
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x0000AA55 File Offset: 0x00008C55
		// (set) Token: 0x06001198 RID: 4504 RVA: 0x0007DB60 File Offset: 0x0007BD60
		internal virtual MyListItem ItemFeedback
		{
			[CompilerGenerated]
			get
			{
				return this.setterThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.ChangedEventHandler value2 = new IMyRadio.ChangedEventHandler(this.TryFeedback);
				MyListItem myListItem = this.setterThread;
				if (myListItem != null)
				{
					myListItem.Changed -= value2;
				}
				this.setterThread = value;
				myListItem = this.setterThread;
				if (myListItem != null)
				{
					myListItem.Changed += value2;
				}
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x0000AA5D File Offset: 0x00008C5D
		// (set) Token: 0x0600119A RID: 4506 RVA: 0x0007DBA4 File Offset: 0x0007BDA4
		internal virtual MyListItem ItemVote
		{
			[CompilerGenerated]
			get
			{
				return this.factoryThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.ChangedEventHandler value2 = new IMyRadio.ChangedEventHandler(this.TryVote);
				MyListItem myListItem = this.factoryThread;
				if (myListItem != null)
				{
					myListItem.Changed -= value2;
				}
				this.factoryThread = value;
				myListItem = this.factoryThread;
				if (myListItem != null)
				{
					myListItem.Changed += value2;
				}
			}
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0007DBE8 File Offset: 0x0007BDE8
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.exporterThread)
			{
				this.exporterThread = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageother/pageotherleft.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0007DC18 File Offset: 0x0007BE18
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanItem = (StackPanel)target;
				return;
			}
			if (connectionId == 2)
			{
				this.ItemHelp = (MyListItem)target;
				return;
			}
			if (connectionId == 3)
			{
				this.ItemAbout = (MyListItem)target;
				return;
			}
			if (connectionId == 4)
			{
				this.ItemTest = (MyListItem)target;
				return;
			}
			if (connectionId == 5)
			{
				this.ItemFeedback = (MyListItem)target;
				return;
			}
			if (connectionId == 6)
			{
				this.ItemVote = (MyListItem)target;
				return;
			}
			this.exporterThread = true;
		}

		// Token: 0x0400091E RID: 2334
		private bool _GetterThread;

		// Token: 0x0400091F RID: 2335
		private bool tokenThread;

		// Token: 0x04000920 RID: 2336
		public FormMain.PageSubType expressionThread;

		// Token: 0x04000921 RID: 2337
		[AccessedThroughProperty("PanItem")]
		[CompilerGenerated]
		private StackPanel _WriterThread;

		// Token: 0x04000922 RID: 2338
		[AccessedThroughProperty("ItemHelp")]
		[CompilerGenerated]
		private MyListItem _RegistryThread;

		// Token: 0x04000923 RID: 2339
		[CompilerGenerated]
		[AccessedThroughProperty("ItemAbout")]
		private MyListItem ruleThread;

		// Token: 0x04000924 RID: 2340
		[AccessedThroughProperty("ItemTest")]
		[CompilerGenerated]
		private MyListItem proccesorThread;

		// Token: 0x04000925 RID: 2341
		[AccessedThroughProperty("ItemFeedback")]
		[CompilerGenerated]
		private MyListItem setterThread;

		// Token: 0x04000926 RID: 2342
		[AccessedThroughProperty("ItemVote")]
		[CompilerGenerated]
		private MyListItem factoryThread;

		// Token: 0x04000927 RID: 2343
		private bool exporterThread;
	}
}
