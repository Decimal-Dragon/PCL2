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
	// Token: 0x020001AB RID: 427
	[DesignerGenerated]
	public class PageSetupLeft : MyPageLeft, IComponentConnector
	{
		// Token: 0x060011A6 RID: 4518 RVA: 0x0007DC94 File Offset: 0x0007BE94
		private void PageSetupLeft_Loaded(object sender, RoutedEventArgs e)
		{
			bool flag = false;
			if (Conversions.ToBoolean(this.ItemLaunch.Checked && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupLaunch", null))))
			{
				flag = true;
			}
			if (Conversions.ToBoolean(this.ItemUI.Checked && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupUi", null))))
			{
				flag = true;
			}
			if (Conversions.ToBoolean(this.ItemSystem.Checked && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupSystem", null))))
			{
				flag = true;
			}
			if (Conversions.ToBoolean(this.ItemLink.Checked && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupLink", null))))
			{
				flag = true;
			}
			if (PageSetupUI.CreateClient())
			{
				flag = false;
			}
			if (!this._ImporterThread || flag)
			{
				this._ImporterThread = true;
				PageSetupUI.HiddenRefresh();
				if (!this._WorkerThread)
				{
					if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenSetupLaunch", null))))
					{
						this.ItemLaunch.SetChecked(true, false, false);
						return;
					}
					if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenSetupUi", null))))
					{
						this.ItemUI.SetChecked(true, false, false);
						return;
					}
					if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenSetupSystem", null))))
					{
						this.ItemSystem.SetChecked(true, false, false);
						return;
					}
					if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenSetupLink", null))))
					{
						this.ItemLink.SetChecked(true, false, false);
						return;
					}
					this.ItemLaunch.SetChecked(true, false, false);
				}
			}
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0000AA83 File Offset: 0x00008C83
		private void PageOtherLeft_Unloaded(object sender, RoutedEventArgs e)
		{
			this._WorkerThread = false;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0007DE58 File Offset: 0x0007C058
		public PageSetupLeft()
		{
			base.Loaded += this.PageSetupLeft_Loaded;
			base.Unloaded += this.PageOtherLeft_Unloaded;
			this._ImporterThread = false;
			this._WorkerThread = false;
			this.InitializeComponent();
			if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenSetupLaunch", null))))
			{
				this._ConnectionThread = FormMain.PageSubType.Default;
				return;
			}
			if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenSetupUi", null))))
			{
				this._ConnectionThread = FormMain.PageSubType.DownloadInstall;
				return;
			}
			if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenSetupSystem", null))))
			{
				this._ConnectionThread = FormMain.PageSubType.SetupSystem;
				return;
			}
			if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenSetupLink", null))))
			{
				this._ConnectionThread = FormMain.PageSubType.SetupLink;
				return;
			}
			this._ConnectionThread = FormMain.PageSubType.Default;
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0000AA8C File Offset: 0x00008C8C
		private void PageCheck(MyListItem sender, EventArgs e)
		{
			if (sender.Tag != null)
			{
				this.PageChange(checked((FormMain.PageSubType)Math.Round(ModBase.Val(RuntimeHelpers.GetObjectValue(sender.Tag)))));
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0007DF3C File Offset: 0x0007C13C
		public object PageGet(FormMain.PageSubType ID = (FormMain.PageSubType)(-1))
		{
			if (ID == (FormMain.PageSubType)(-1))
			{
				ID = this._ConnectionThread;
			}
			object result;
			switch (ID)
			{
			case FormMain.PageSubType.Default:
				if (ModMain._PolicyIterator == null)
				{
					ModMain._PolicyIterator = new PageSetupLaunch();
				}
				result = ModMain._PolicyIterator;
				break;
			case FormMain.PageSubType.DownloadInstall:
				if (ModMain.m_OrderIterator == null)
				{
					ModMain.m_OrderIterator = new PageSetupUI();
				}
				result = ModMain.m_OrderIterator;
				break;
			case FormMain.PageSubType.SetupSystem:
				if (ModMain.producerIterator == null)
				{
					ModMain.producerIterator = new PageSetupSystem();
				}
				result = ModMain.producerIterator;
				break;
			case FormMain.PageSubType.SetupLink:
				if (ModMain.m_SchemaIterator == null)
				{
					ModMain.m_SchemaIterator = new PageSetupLink();
				}
				result = ModMain.m_SchemaIterator;
				break;
			default:
				throw new Exception("未知的设置子页面种类：" + Conversions.ToString((int)ID));
			}
			return result;
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0007DFE8 File Offset: 0x0007C1E8
		public void PageChange(FormMain.PageSubType ID)
		{
			checked
			{
				if (this._ConnectionThread != ID)
				{
					ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
					this._WorkerThread = true;
					try
					{
						switch (ID)
						{
						case FormMain.PageSubType.Default:
							if (Information.IsNothing(ModMain._PolicyIterator))
							{
								ModMain._PolicyIterator = new PageSetupLaunch();
							}
							PageSetupLeft.PageChangeRun(ModMain._PolicyIterator);
							break;
						case FormMain.PageSubType.DownloadInstall:
							if (Information.IsNothing(ModMain.m_OrderIterator))
							{
								ModMain.m_OrderIterator = new PageSetupUI();
							}
							PageSetupLeft.PageChangeRun(ModMain.m_OrderIterator);
							break;
						case FormMain.PageSubType.SetupSystem:
							if (Information.IsNothing(ModMain.producerIterator))
							{
								ModMain.producerIterator = new PageSetupSystem();
							}
							PageSetupLeft.PageChangeRun(ModMain.producerIterator);
							break;
						case FormMain.PageSubType.SetupLink:
							if (Information.IsNothing(ModMain.m_SchemaIterator))
							{
								ModMain.m_SchemaIterator = new PageSetupLink();
							}
							PageSetupLeft.PageChangeRun(ModMain.m_SchemaIterator);
							break;
						default:
							throw new Exception("未知的设置子页面种类：" + Conversions.ToString((int)ID));
						}
						this._ConnectionThread = ID;
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

		// Token: 0x060011AC RID: 4524 RVA: 0x0007E12C File Offset: 0x0007C32C
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
				ModAnimation.AaCode((PageSetupLeft._Closure$__.$I9-0 == null) ? (PageSetupLeft._Closure$__.$I9-0 = delegate()
				{
					((MyPageRight)ModMain._ProcessIterator.PanMainRight.Child).PageOnForceExit();
					ModMain._ProcessIterator.PanMainRight.Child = ModMain._ProcessIterator._GetterIterator;
					ModMain._ProcessIterator._GetterIterator.Opacity = 0.0;
				}) : PageSetupLeft._Closure$__.$I9-0, 130, false),
				ModAnimation.AaCode((PageSetupLeft._Closure$__.$I9-1 == null) ? (PageSetupLeft._Closure$__.$I9-1 = delegate()
				{
					ModMain._ProcessIterator._GetterIterator.Opacity = 1.0;
					ModMain._ProcessIterator._GetterIterator.PageOnEnter();
				}) : PageSetupLeft._Closure$__.$I9-1, 30, true)
			}, "PageLeft PageChange", false);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0007E1F8 File Offset: 0x0007C3F8
		public void Reset(object sender, EventArgs e)
		{
			double num = ModBase.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)));
			if (num == 0.0)
			{
				if (ModMain.MyMsgBox("是否要初始化启动页的所有设置？该操作不可撤销。", "初始化确认", "确定", "取消", "", true, true, false, null, null, null) == 1)
				{
					if (Information.IsNothing(ModMain._PolicyIterator))
					{
						ModMain._PolicyIterator = new PageSetupLaunch();
					}
					ModMain._PolicyIterator.Reset();
					this.ItemLaunch.Checked = true;
					return;
				}
			}
			else if (num == 1.0)
			{
				if (ModMain.MyMsgBox("是否要初始化个性化页的所有设置？该操作不可撤销。\r\n（背景图片与音乐、自定义主页等外部文件不会被删除）", "初始化确认", "确定", "取消", "", true, true, false, null, null, null) == 1)
				{
					if (Information.IsNothing(ModMain.m_OrderIterator))
					{
						ModMain.m_OrderIterator = new PageSetupUI();
					}
					ModMain.m_OrderIterator.Reset();
					this.ItemUI.Checked = true;
					return;
				}
			}
			else if (num == 2.0)
			{
				if (ModMain.MyMsgBox("是否要初始化启动器页的所有设置？该操作不可撤销。", "初始化确认", "确定", "取消", "", true, true, false, null, null, null) == 1)
				{
					if (Information.IsNothing(ModMain.producerIterator))
					{
						ModMain.producerIterator = new PageSetupSystem();
					}
					ModMain.producerIterator.Reset();
					this.ItemSystem.Checked = true;
					return;
				}
			}
			else if (num == 3.0 && ModMain.MyMsgBox("是否要初始化联机页的所有设置？该操作不可撤销。", "初始化确认", "确定", "取消", "", true, true, false, null, null, null) == 1)
			{
				if (Information.IsNothing(ModMain.m_SchemaIterator))
				{
					ModMain.m_SchemaIterator = new PageSetupLink();
				}
				ModMain.m_SchemaIterator.Reset();
				this.ItemLink.Checked = true;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x0000AAB2 File Offset: 0x00008CB2
		// (set) Token: 0x060011AF RID: 4527 RVA: 0x0000AABA File Offset: 0x00008CBA
		internal virtual StackPanel PanItem { get; set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0000AAC3 File Offset: 0x00008CC3
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x0007E3B0 File Offset: 0x0007C5B0
		internal virtual MyListItem ItemLaunch
		{
			[CompilerGenerated]
			get
			{
				return this.resolverThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem myListItem = this.resolverThread;
				if (myListItem != null)
				{
					myListItem.Check -= value2;
				}
				this.resolverThread = value;
				myListItem = this.resolverThread;
				if (myListItem != null)
				{
					myListItem.Check += value2;
				}
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0000AACB File Offset: 0x00008CCB
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x0007E3F4 File Offset: 0x0007C5F4
		internal virtual MyListItem ItemUI
		{
			[CompilerGenerated]
			get
			{
				return this.statusThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem myListItem = this.statusThread;
				if (myListItem != null)
				{
					myListItem.Check -= value2;
				}
				this.statusThread = value;
				myListItem = this.statusThread;
				if (myListItem != null)
				{
					myListItem.Check += value2;
				}
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0000AAD3 File Offset: 0x00008CD3
		// (set) Token: 0x060011B5 RID: 4533 RVA: 0x0007E438 File Offset: 0x0007C638
		internal virtual MyListItem ItemSystem
		{
			[CompilerGenerated]
			get
			{
				return this.roleThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem myListItem = this.roleThread;
				if (myListItem != null)
				{
					myListItem.Check -= value2;
				}
				this.roleThread = value;
				myListItem = this.roleThread;
				if (myListItem != null)
				{
					myListItem.Check += value2;
				}
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0000AADB File Offset: 0x00008CDB
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x0007E47C File Offset: 0x0007C67C
		internal virtual MyListItem ItemLink
		{
			[CompilerGenerated]
			get
			{
				return this._StructThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem structThread = this._StructThread;
				if (structThread != null)
				{
					structThread.Check -= value2;
				}
				this._StructThread = value;
				structThread = this._StructThread;
				if (structThread != null)
				{
					structThread.Check += value2;
				}
			}
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0007E4C0 File Offset: 0x0007C6C0
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._PrinterThread)
			{
				this._PrinterThread = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagesetup/pagesetupleft.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0007E4F0 File Offset: 0x0007C6F0
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
				this.ItemLaunch = (MyListItem)target;
				return;
			}
			if (connectionId == 3)
			{
				this.ItemUI = (MyListItem)target;
				return;
			}
			if (connectionId == 4)
			{
				this.ItemSystem = (MyListItem)target;
				return;
			}
			if (connectionId == 5)
			{
				this.ItemLink = (MyListItem)target;
				return;
			}
			this._PrinterThread = true;
		}

		// Token: 0x0400092B RID: 2347
		private bool _ImporterThread;

		// Token: 0x0400092C RID: 2348
		private bool _WorkerThread;

		// Token: 0x0400092D RID: 2349
		public FormMain.PageSubType _ConnectionThread;

		// Token: 0x0400092E RID: 2350
		[CompilerGenerated]
		[AccessedThroughProperty("PanItem")]
		private StackPanel _ServerThread;

		// Token: 0x0400092F RID: 2351
		[AccessedThroughProperty("ItemLaunch")]
		[CompilerGenerated]
		private MyListItem resolverThread;

		// Token: 0x04000930 RID: 2352
		[AccessedThroughProperty("ItemUI")]
		[CompilerGenerated]
		private MyListItem statusThread;

		// Token: 0x04000931 RID: 2353
		[AccessedThroughProperty("ItemSystem")]
		[CompilerGenerated]
		private MyListItem roleThread;

		// Token: 0x04000932 RID: 2354
		[CompilerGenerated]
		[AccessedThroughProperty("ItemLink")]
		private MyListItem _StructThread;

		// Token: 0x04000933 RID: 2355
		private bool _PrinterThread;
	}
}
