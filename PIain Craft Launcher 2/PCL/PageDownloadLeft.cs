using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020001A5 RID: 421
	[DesignerGenerated]
	public class PageDownloadLeft : MyPageLeft, IRefreshable, IComponentConnector
	{
		// Token: 0x06001131 RID: 4401 RVA: 0x0000A74A File Offset: 0x0000894A
		public PageDownloadLeft()
		{
			this._InterpreterThread = FormMain.PageSubType.DownloadInstall;
			this.InitializeComponent();
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0000A760 File Offset: 0x00008960
		private void PageCheck(MyListItem sender, ModBase.RouteEventArgs e)
		{
			if (sender.Tag != null)
			{
				this.PageChange(checked((FormMain.PageSubType)Math.Round(ModBase.Val(RuntimeHelpers.GetObjectValue(sender.Tag)))));
			}
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0007C1C8 File Offset: 0x0007A3C8
		public object PageGet(FormMain.PageSubType ID = (FormMain.PageSubType)(-1))
		{
			if (ID == (FormMain.PageSubType)(-1))
			{
				ID = this._InterpreterThread;
			}
			switch (ID)
			{
			case FormMain.PageSubType.DownloadInstall:
				if (ModMain.m_VisitorIterator == null)
				{
					ModMain.m_VisitorIterator = new PageDownloadInstall();
				}
				return ModMain.m_VisitorIterator;
			case FormMain.PageSubType.DownloadClient:
				if (ModMain._ValueIterator == null)
				{
					ModMain._ValueIterator = new PageDownloadClient();
				}
				return ModMain._ValueIterator;
			case FormMain.PageSubType.DownloadOptiFine:
				if (ModMain.objectIterator == null)
				{
					ModMain.objectIterator = new PageDownloadOptiFine();
				}
				return ModMain.objectIterator;
			case FormMain.PageSubType.DownloadForge:
				if (ModMain.m_ItemIterator == null)
				{
					ModMain.m_ItemIterator = new PageDownloadForge();
				}
				return ModMain.m_ItemIterator;
			case FormMain.PageSubType.DownloadNeoForge:
				if (ModMain._ReponseIterator == null)
				{
					ModMain._ReponseIterator = new PageDownloadNeoForge();
				}
				return ModMain._ReponseIterator;
			case FormMain.PageSubType.DownloadFabric:
				if (ModMain._GlobalIterator == null)
				{
					ModMain._GlobalIterator = new PageDownloadFabric();
				}
				return ModMain._GlobalIterator;
			case FormMain.PageSubType.DownloadLiteLoader:
				if (ModMain.bridgeIterator == null)
				{
					ModMain.bridgeIterator = new PageDownloadLiteLoader();
				}
				return ModMain.bridgeIterator;
			case FormMain.PageSubType.DownloadMod:
				if (ModMain.m_ExceptionIterator == null)
				{
					ModMain.m_ExceptionIterator = new PageDownloadMod();
				}
				return ModMain.m_ExceptionIterator;
			case FormMain.PageSubType.DownloadPack:
				if (ModMain.m_UtilsIterator == null)
				{
					ModMain.m_UtilsIterator = new PageDownloadPack();
				}
				return ModMain.m_UtilsIterator;
			}
			throw new Exception("未知的下载子页面种类：" + Conversions.ToString((int)ID));
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0007C324 File Offset: 0x0007A524
		public void PageChange(FormMain.PageSubType ID)
		{
			checked
			{
				if (this._InterpreterThread != ID)
				{
					ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
					try
					{
						PageDownloadLeft.PageChangeRun((MyPageRight)this.PageGet(ID));
						this._InterpreterThread = ID;
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

		// Token: 0x06001135 RID: 4405 RVA: 0x0007C3B8 File Offset: 0x0007A5B8
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
				ModAnimation.AaCode((PageDownloadLeft._Closure$__.$I5-0 == null) ? (PageDownloadLeft._Closure$__.$I5-0 = delegate()
				{
					((MyPageRight)ModMain._ProcessIterator.PanMainRight.Child).PageOnForceExit();
					ModMain._ProcessIterator.PanMainRight.Child = ModMain._ProcessIterator._GetterIterator;
					ModMain._ProcessIterator._GetterIterator.Opacity = 0.0;
				}) : PageDownloadLeft._Closure$__.$I5-0, 130, false),
				ModAnimation.AaCode((PageDownloadLeft._Closure$__.$I5-1 == null) ? (PageDownloadLeft._Closure$__.$I5-1 = delegate()
				{
					ModMain._ProcessIterator._GetterIterator.Opacity = 1.0;
					ModMain._ProcessIterator._GetterIterator.PageOnEnter();
				}) : PageDownloadLeft._Closure$__.$I5-1, 30, true)
			}, "PageLeft PageChange", false);
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0007C484 File Offset: 0x0007A684
		public void Refresh(object sender, EventArgs e)
		{
			this.Refresh(checked((FormMain.PageSubType)Math.Round(ModBase.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null))))));
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0000A786 File Offset: 0x00008986
		public void Refresh()
		{
			this.Refresh(ModMain._ProcessIterator.GetTests());
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0007C4BC File Offset: 0x0007A6BC
		public void Refresh(FormMain.PageSubType SubType)
		{
			switch (SubType)
			{
			case FormMain.PageSubType.DownloadInstall:
				ModDownload.accountTests.Start(null, true);
				ModDownload.m_ModelTests.Start(null, true);
				ModDownload.m_AttributeTests.Start(null, true);
				ModDownload.m_AnnotationTests.Start(null, true);
				ModDownload._FacadeTests.Start(null, true);
				ModDownload.authenticationTests.Start(null, true);
				ModDownload.mappingTests.Start(null, true);
				ModDownload.tokenizerTests.Start(null, true);
				this.ItemInstall.Checked = true;
				break;
			case FormMain.PageSubType.DownloadClient:
				ModDownload.accountTests.Start(null, true);
				this.ItemClient.Checked = true;
				break;
			case FormMain.PageSubType.DownloadOptiFine:
				ModDownload.m_ModelTests.Start(null, true);
				this.ItemOptiFine.Checked = true;
				break;
			case FormMain.PageSubType.DownloadForge:
				ModDownload.m_AttributeTests.Start(null, true);
				this.ItemForge.Checked = true;
				break;
			case FormMain.PageSubType.DownloadNeoForge:
				ModDownload.m_AnnotationTests.Start(null, true);
				this.ItemNeoForge.Checked = true;
				break;
			case FormMain.PageSubType.DownloadFabric:
				ModDownload.authenticationTests.Start(null, true);
				this.ItemFabric.Checked = true;
				break;
			case FormMain.PageSubType.DownloadLiteLoader:
				ModDownload._FacadeTests.Start(null, true);
				this.ItemLiteLoader.Checked = true;
				break;
			case FormMain.PageSubType.DownloadMod:
				PageDownloadMod.containerField = new ModComp.CompProjectStorage();
				PageDownloadMod.m_ParamsField = 0;
				ModComp.pool.Clear();
				ModComp.customer.Clear();
				if (ModMain.m_ExceptionIterator != null)
				{
					ModMain.m_ExceptionIterator.PageLoaderRestart(null, true);
				}
				this.ItemMod.Checked = true;
				break;
			case FormMain.PageSubType.DownloadPack:
				PageDownloadPack._ClassReader = new ModComp.CompProjectStorage();
				PageDownloadPack._PolicyReader = 0;
				ModComp.pool.Clear();
				ModComp.customer.Clear();
				if (ModMain.m_UtilsIterator != null)
				{
					ModMain.m_UtilsIterator.PageLoaderRestart(null, true);
				}
				this.ItemPack.Checked = true;
				break;
			}
			ModMain.Hint("正在刷新……", ModMain.HintType.Info, false);
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0000A798 File Offset: 0x00008998
		private void ItemInstall_Click(object sender, MouseButtonEventArgs e)
		{
			if (this.ItemInstall.Checked)
			{
				ModMain.m_VisitorIterator.ExitSelectPage();
			}
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0007C6BC File Offset: 0x0007A8BC
		private void ItemHand_Click(object sender, ModBase.RouteEventArgs e)
		{
			checked
			{
				if (this.ItemHand.Checked)
				{
					e.m_SerializerError = true;
					ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
					if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("HintHandInstall", null))))
					{
						ModBase.m_IdentifierRepository.Set("HintHandInstall", true, false, null);
						if (ModMain.MyMsgBox("手动安装包功能提供了 OptiFine、Forge 等组件的 .jar 安装文件下载，但无法自动安装。\r\n在自动安装页面先选择 MC 版本，然后就可以选择 OptiFine、Forge 等组件，让 PCL 自动进行安装了。", "自动安装提示", "返回自动安装", "继续下载手动安装包", "", false, true, false, null, null, null) == 1)
						{
							ModMain._ProcessIterator.PageChange(new FormMain.PageStackData
							{
								initializerMap = FormMain.PageType.Download
							}, FormMain.PageSubType.DownloadInstall);
							ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
							return;
						}
					}
					this.ItemHand.Visibility = Visibility.Collapsed;
					this.LabGame.Visibility = Visibility.Collapsed;
					this.LabHand.Visibility = Visibility.Visible;
					this.ItemClient.Visibility = Visibility.Visible;
					this.ItemOptiFine.Visibility = Visibility.Visible;
					this.ItemFabric.Visibility = Visibility.Visible;
					this.ItemForge.Visibility = Visibility.Visible;
					this.ItemNeoForge.Visibility = Visibility.Visible;
					this.ItemLiteLoader.Visibility = Visibility.Visible;
					ModBase.RunInThread(delegate
					{
						Thread.Sleep(20);
						ModBase.RunInUiWait(delegate()
						{
							this.ItemClient.SetChecked(true, true, true);
						});
						ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
					});
				}
			}
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0007C7E8 File Offset: 0x0007A9E8
		private void LabHand_Click(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			checked
			{
				ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
				this.ItemHand.Visibility = Visibility.Visible;
				this.LabGame.Visibility = Visibility.Visible;
				this.LabHand.Visibility = Visibility.Collapsed;
				this.ItemClient.Visibility = Visibility.Collapsed;
				this.ItemOptiFine.Visibility = Visibility.Collapsed;
				this.ItemNeoForge.Visibility = Visibility.Collapsed;
				this.ItemFabric.Visibility = Visibility.Collapsed;
				this.ItemForge.Visibility = Visibility.Collapsed;
				this.ItemLiteLoader.Visibility = Visibility.Collapsed;
				ModBase.RunInThread(delegate
				{
					Thread.Sleep(20);
					ModBase.RunInUiWait(delegate()
					{
						this.ItemInstall.SetChecked(true, true, true);
					});
					ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
				});
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x0000A7B1 File Offset: 0x000089B1
		// (set) Token: 0x0600113D RID: 4413 RVA: 0x0000A7B9 File Offset: 0x000089B9
		internal virtual StackPanel PanItem { get; set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x0000A7C2 File Offset: 0x000089C2
		// (set) Token: 0x0600113F RID: 4415 RVA: 0x0000A7CA File Offset: 0x000089CA
		internal virtual TextBlock LabGame { get; set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x0000A7D3 File Offset: 0x000089D3
		// (set) Token: 0x06001141 RID: 4417 RVA: 0x0007C888 File Offset: 0x0007AA88
		internal virtual MyListItem ItemInstall
		{
			[CompilerGenerated]
			get
			{
				return this._IdentifierThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem.ClickEventHandler value3 = new MyListItem.ClickEventHandler(this.ItemInstall_Click);
				MyListItem identifierThread = this._IdentifierThread;
				if (identifierThread != null)
				{
					identifierThread.Check -= value2;
					identifierThread.Click -= value3;
				}
				this._IdentifierThread = value;
				identifierThread = this._IdentifierThread;
				if (identifierThread != null)
				{
					identifierThread.Check += value2;
					identifierThread.Click += value3;
				}
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x0000A7DB File Offset: 0x000089DB
		// (set) Token: 0x06001143 RID: 4419 RVA: 0x0007C8E8 File Offset: 0x0007AAE8
		internal virtual MyListItem ItemHand
		{
			[CompilerGenerated]
			get
			{
				return this.m_SystemThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.ChangedEventHandler value2 = new IMyRadio.ChangedEventHandler(this.ItemHand_Click);
				MyListItem systemThread = this.m_SystemThread;
				if (systemThread != null)
				{
					systemThread.Changed -= value2;
				}
				this.m_SystemThread = value;
				systemThread = this.m_SystemThread;
				if (systemThread != null)
				{
					systemThread.Changed += value2;
				}
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0000A7E3 File Offset: 0x000089E3
		// (set) Token: 0x06001145 RID: 4421 RVA: 0x0007C92C File Offset: 0x0007AB2C
		internal virtual TextBlock LabHand
		{
			[CompilerGenerated]
			get
			{
				return this._ParamThread;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.LabHand_Click);
				TextBlock paramThread = this._ParamThread;
				if (paramThread != null)
				{
					paramThread.MouseLeftButtonUp -= value2;
				}
				this._ParamThread = value;
				paramThread = this._ParamThread;
				if (paramThread != null)
				{
					paramThread.MouseLeftButtonUp += value2;
				}
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x0000A7EB File Offset: 0x000089EB
		// (set) Token: 0x06001147 RID: 4423 RVA: 0x0007C970 File Offset: 0x0007AB70
		internal virtual MyListItem ItemClient
		{
			[CompilerGenerated]
			get
			{
				return this.tagThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem myListItem = this.tagThread;
				if (myListItem != null)
				{
					myListItem.Check -= value2;
				}
				this.tagThread = value;
				myListItem = this.tagThread;
				if (myListItem != null)
				{
					myListItem.Check += value2;
				}
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x0000A7F3 File Offset: 0x000089F3
		// (set) Token: 0x06001149 RID: 4425 RVA: 0x0007C9B4 File Offset: 0x0007ABB4
		internal virtual MyListItem ItemOptiFine
		{
			[CompilerGenerated]
			get
			{
				return this.observerThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem myListItem = this.observerThread;
				if (myListItem != null)
				{
					myListItem.Check -= value2;
				}
				this.observerThread = value;
				myListItem = this.observerThread;
				if (myListItem != null)
				{
					myListItem.Check += value2;
				}
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0000A7FB File Offset: 0x000089FB
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x0007C9F8 File Offset: 0x0007ABF8
		internal virtual MyListItem ItemForge
		{
			[CompilerGenerated]
			get
			{
				return this._StubThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem stubThread = this._StubThread;
				if (stubThread != null)
				{
					stubThread.Check -= value2;
				}
				this._StubThread = value;
				stubThread = this._StubThread;
				if (stubThread != null)
				{
					stubThread.Check += value2;
				}
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x0000A803 File Offset: 0x00008A03
		// (set) Token: 0x0600114D RID: 4429 RVA: 0x0007CA3C File Offset: 0x0007AC3C
		internal virtual MyListItem ItemNeoForge
		{
			[CompilerGenerated]
			get
			{
				return this._RulesThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem rulesThread = this._RulesThread;
				if (rulesThread != null)
				{
					rulesThread.Check -= value2;
				}
				this._RulesThread = value;
				rulesThread = this._RulesThread;
				if (rulesThread != null)
				{
					rulesThread.Check += value2;
				}
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x0000A80B File Offset: 0x00008A0B
		// (set) Token: 0x0600114F RID: 4431 RVA: 0x0007CA80 File Offset: 0x0007AC80
		internal virtual MyListItem ItemFabric
		{
			[CompilerGenerated]
			get
			{
				return this._RefThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem refThread = this._RefThread;
				if (refThread != null)
				{
					refThread.Check -= value2;
				}
				this._RefThread = value;
				refThread = this._RefThread;
				if (refThread != null)
				{
					refThread.Check += value2;
				}
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x0000A813 File Offset: 0x00008A13
		// (set) Token: 0x06001151 RID: 4433 RVA: 0x0007CAC4 File Offset: 0x0007ACC4
		internal virtual MyListItem ItemLiteLoader
		{
			[CompilerGenerated]
			get
			{
				return this.m_DecoratorThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem decoratorThread = this.m_DecoratorThread;
				if (decoratorThread != null)
				{
					decoratorThread.Check -= value2;
				}
				this.m_DecoratorThread = value;
				decoratorThread = this.m_DecoratorThread;
				if (decoratorThread != null)
				{
					decoratorThread.Check += value2;
				}
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x0000A81B File Offset: 0x00008A1B
		// (set) Token: 0x06001153 RID: 4435 RVA: 0x0007CB08 File Offset: 0x0007AD08
		internal virtual MyListItem ItemMod
		{
			[CompilerGenerated]
			get
			{
				return this.m_InstanceThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem instanceThread = this.m_InstanceThread;
				if (instanceThread != null)
				{
					instanceThread.Check -= value2;
				}
				this.m_InstanceThread = value;
				instanceThread = this.m_InstanceThread;
				if (instanceThread != null)
				{
					instanceThread.Check += value2;
				}
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x0000A823 File Offset: 0x00008A23
		// (set) Token: 0x06001155 RID: 4437 RVA: 0x0007CB4C File Offset: 0x0007AD4C
		internal virtual MyListItem ItemPack
		{
			[CompilerGenerated]
			get
			{
				return this.m_StateThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem stateThread = this.m_StateThread;
				if (stateThread != null)
				{
					stateThread.Check -= value2;
				}
				this.m_StateThread = value;
				stateThread = this.m_StateThread;
				if (stateThread != null)
				{
					stateThread.Check += value2;
				}
			}
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0007CB90 File Offset: 0x0007AD90
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_CallbackThread)
			{
				this.m_CallbackThread = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagedownload/pagedownloadleft.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0007CBC0 File Offset: 0x0007ADC0
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
				this.LabGame = (TextBlock)target;
				return;
			}
			if (connectionId == 3)
			{
				this.ItemInstall = (MyListItem)target;
				return;
			}
			if (connectionId == 4)
			{
				this.ItemHand = (MyListItem)target;
				return;
			}
			if (connectionId == 5)
			{
				this.LabHand = (TextBlock)target;
				return;
			}
			if (connectionId == 6)
			{
				this.ItemClient = (MyListItem)target;
				return;
			}
			if (connectionId == 7)
			{
				this.ItemOptiFine = (MyListItem)target;
				return;
			}
			if (connectionId == 8)
			{
				this.ItemForge = (MyListItem)target;
				return;
			}
			if (connectionId == 9)
			{
				this.ItemNeoForge = (MyListItem)target;
				return;
			}
			if (connectionId == 10)
			{
				this.ItemFabric = (MyListItem)target;
				return;
			}
			if (connectionId == 11)
			{
				this.ItemLiteLoader = (MyListItem)target;
				return;
			}
			if (connectionId == 12)
			{
				this.ItemMod = (MyListItem)target;
				return;
			}
			if (connectionId == 13)
			{
				this.ItemPack = (MyListItem)target;
				return;
			}
			this.m_CallbackThread = true;
		}

		// Token: 0x04000905 RID: 2309
		public FormMain.PageSubType _InterpreterThread;

		// Token: 0x04000906 RID: 2310
		[AccessedThroughProperty("PanItem")]
		[CompilerGenerated]
		private StackPanel m_SerializerThread;

		// Token: 0x04000907 RID: 2311
		[CompilerGenerated]
		[AccessedThroughProperty("LabGame")]
		private TextBlock watcherThread;

		// Token: 0x04000908 RID: 2312
		[CompilerGenerated]
		[AccessedThroughProperty("ItemInstall")]
		private MyListItem _IdentifierThread;

		// Token: 0x04000909 RID: 2313
		[CompilerGenerated]
		[AccessedThroughProperty("ItemHand")]
		private MyListItem m_SystemThread;

		// Token: 0x0400090A RID: 2314
		[AccessedThroughProperty("LabHand")]
		[CompilerGenerated]
		private TextBlock _ParamThread;

		// Token: 0x0400090B RID: 2315
		[CompilerGenerated]
		[AccessedThroughProperty("ItemClient")]
		private MyListItem tagThread;

		// Token: 0x0400090C RID: 2316
		[AccessedThroughProperty("ItemOptiFine")]
		[CompilerGenerated]
		private MyListItem observerThread;

		// Token: 0x0400090D RID: 2317
		[CompilerGenerated]
		[AccessedThroughProperty("ItemForge")]
		private MyListItem _StubThread;

		// Token: 0x0400090E RID: 2318
		[AccessedThroughProperty("ItemNeoForge")]
		[CompilerGenerated]
		private MyListItem _RulesThread;

		// Token: 0x0400090F RID: 2319
		[CompilerGenerated]
		[AccessedThroughProperty("ItemFabric")]
		private MyListItem _RefThread;

		// Token: 0x04000910 RID: 2320
		[CompilerGenerated]
		[AccessedThroughProperty("ItemLiteLoader")]
		private MyListItem m_DecoratorThread;

		// Token: 0x04000911 RID: 2321
		[CompilerGenerated]
		[AccessedThroughProperty("ItemMod")]
		private MyListItem m_InstanceThread;

		// Token: 0x04000912 RID: 2322
		[AccessedThroughProperty("ItemPack")]
		[CompilerGenerated]
		private MyListItem m_StateThread;

		// Token: 0x04000913 RID: 2323
		private bool m_CallbackThread;
	}
}
