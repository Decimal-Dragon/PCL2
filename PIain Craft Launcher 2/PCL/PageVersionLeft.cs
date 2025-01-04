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
	// Token: 0x0200010D RID: 269
	[DesignerGenerated]
	public class PageVersionLeft : MyPageLeft, IComponentConnector
	{
		// Token: 0x06000AD7 RID: 2775 RVA: 0x000078E4 File Offset: 0x00005AE4
		public PageVersionLeft()
		{
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.RefreshModDisabled();
			};
			this._StateConfig = FormMain.PageSubType.Default;
			this.InitializeComponent();
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0004BE04 File Offset: 0x0004A004
		public void RefreshModDisabled()
		{
			if (PageVersionLeft._InstanceConfig != null && PageVersionLeft._InstanceConfig.RunThread())
			{
				this.ItemMod.Visibility = Visibility.Visible;
				this.ItemModDisabled.Visibility = Visibility.Collapsed;
				return;
			}
			this.ItemMod.Visibility = Visibility.Collapsed;
			this.ItemModDisabled.Visibility = Visibility.Visible;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0000790C File Offset: 0x00005B0C
		private void PageCheck(MyListItem sender, ModBase.RouteEventArgs e)
		{
			if (sender.Tag != null)
			{
				this.PageChange(checked((FormMain.PageSubType)Math.Round(ModBase.Val(RuntimeHelpers.GetObjectValue(sender.Tag)))));
			}
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0004BE58 File Offset: 0x0004A058
		public object PageGet(FormMain.PageSubType ID = (FormMain.PageSubType)(-1))
		{
			if (ID == (FormMain.PageSubType)(-1))
			{
				ID = this._StateConfig;
			}
			object result;
			switch (ID)
			{
			case FormMain.PageSubType.Default:
				if (ModMain.m_MapperRepository == null)
				{
					ModMain.m_MapperRepository = new PageVersionOverall();
				}
				result = ModMain.m_MapperRepository;
				break;
			case FormMain.PageSubType.DownloadInstall:
				if (Information.IsNothing(ModMain.composerRepository))
				{
					ModMain.composerRepository = new PageVersionSetup();
				}
				result = ModMain.composerRepository;
				break;
			case FormMain.PageSubType.SetupSystem:
				if (ModMain._ThreadRepository == null)
				{
					ModMain._ThreadRepository = new PageVersionMod();
				}
				result = ModMain._ThreadRepository;
				break;
			case FormMain.PageSubType.SetupLink:
				if (ModMain._PropertyRepository == null)
				{
					ModMain._PropertyRepository = new PageVersionModDisabled();
				}
				result = ModMain._PropertyRepository;
				break;
			default:
				throw new Exception("未知的版本设置子页面种类：" + Conversions.ToString((int)ID));
			}
			return result;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0004BF08 File Offset: 0x0004A108
		public void PageChange(FormMain.PageSubType ID)
		{
			checked
			{
				if (this._StateConfig != ID)
				{
					ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
					try
					{
						PageVersionLeft.PageChangeRun((MyPageRight)this.PageGet(ID));
						this._StateConfig = ID;
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

		// Token: 0x06000ADC RID: 2780 RVA: 0x0004BF9C File Offset: 0x0004A19C
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
				ModAnimation.AaCode((PageVersionLeft._Closure$__.$I8-0 == null) ? (PageVersionLeft._Closure$__.$I8-0 = delegate()
				{
					((MyPageRight)ModMain._ProcessIterator.PanMainRight.Child).PageOnForceExit();
					ModMain._ProcessIterator.PanMainRight.Child = ModMain._ProcessIterator._GetterIterator;
					ModMain._ProcessIterator._GetterIterator.Opacity = 0.0;
				}) : PageVersionLeft._Closure$__.$I8-0, 130, false),
				ModAnimation.AaCode((PageVersionLeft._Closure$__.$I8-1 == null) ? (PageVersionLeft._Closure$__.$I8-1 = delegate()
				{
					ModMain._ProcessIterator._GetterIterator.Opacity = 1.0;
					ModMain._ProcessIterator._GetterIterator.PageOnEnter();
				}) : PageVersionLeft._Closure$__.$I8-1, 30, true)
			}, "PageLeft PageChange", false);
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00007932 File Offset: 0x00005B32
		public void Refresh(object sender, EventArgs e)
		{
			PageVersionMod.Refresh();
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0004C068 File Offset: 0x0004A268
		public void Reset(object sender, EventArgs e)
		{
			if (ModMain.MyMsgBox("是否要初始化该版本的版本独立设置？该操作不可撤销。", "初始化确认", "确定", "取消", "", true, true, false, null, null, null) == 1)
			{
				if (Information.IsNothing(ModMain.composerRepository))
				{
					ModMain.composerRepository = new PageVersionSetup();
				}
				ModMain.composerRepository.Reset();
				this.ItemSetup.Checked = true;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x00007939 File Offset: 0x00005B39
		// (set) Token: 0x06000AE0 RID: 2784 RVA: 0x00007941 File Offset: 0x00005B41
		internal virtual StackPanel PanItem { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0000794A File Offset: 0x00005B4A
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x0004C0C8 File Offset: 0x0004A2C8
		internal virtual MyListItem ItemOverall
		{
			[CompilerGenerated]
			get
			{
				return this.templateConfig;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem myListItem = this.templateConfig;
				if (myListItem != null)
				{
					myListItem.Check -= value2;
				}
				this.templateConfig = value;
				myListItem = this.templateConfig;
				if (myListItem != null)
				{
					myListItem.Check += value2;
				}
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x00007952 File Offset: 0x00005B52
		// (set) Token: 0x06000AE4 RID: 2788 RVA: 0x0004C10C File Offset: 0x0004A30C
		internal virtual MyListItem ItemSetup
		{
			[CompilerGenerated]
			get
			{
				return this.methodConfig;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem myListItem = this.methodConfig;
				if (myListItem != null)
				{
					myListItem.Check -= value2;
				}
				this.methodConfig = value;
				myListItem = this.methodConfig;
				if (myListItem != null)
				{
					myListItem.Check += value2;
				}
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0000795A File Offset: 0x00005B5A
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x0004C150 File Offset: 0x0004A350
		internal virtual MyListItem ItemMod
		{
			[CompilerGenerated]
			get
			{
				return this._TaskConfig;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem taskConfig = this._TaskConfig;
				if (taskConfig != null)
				{
					taskConfig.Check -= value2;
				}
				this._TaskConfig = value;
				taskConfig = this._TaskConfig;
				if (taskConfig != null)
				{
					taskConfig.Check += value2;
				}
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00007962 File Offset: 0x00005B62
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x0004C194 File Offset: 0x0004A394
		internal virtual MyListItem ItemModDisabled
		{
			[CompilerGenerated]
			get
			{
				return this.consumerConfig;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.PageCheck((MyListItem)sender, e);
				};
				MyListItem myListItem = this.consumerConfig;
				if (myListItem != null)
				{
					myListItem.Check -= value2;
				}
				this.consumerConfig = value;
				myListItem = this.consumerConfig;
				if (myListItem != null)
				{
					myListItem.Check += value2;
				}
			}
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0004C1D8 File Offset: 0x0004A3D8
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_ConfigurationConfig)
			{
				this.m_ConfigurationConfig = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageversion/pageversionleft.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0004C208 File Offset: 0x0004A408
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
				this.ItemOverall = (MyListItem)target;
				return;
			}
			if (connectionId == 3)
			{
				this.ItemSetup = (MyListItem)target;
				return;
			}
			if (connectionId == 4)
			{
				this.ItemMod = (MyListItem)target;
				return;
			}
			if (connectionId == 5)
			{
				this.ItemModDisabled = (MyListItem)target;
				return;
			}
			this.m_ConfigurationConfig = true;
		}

		// Token: 0x04000596 RID: 1430
		public static ModMinecraft.McVersion _InstanceConfig = null;

		// Token: 0x04000597 RID: 1431
		public FormMain.PageSubType _StateConfig;

		// Token: 0x04000598 RID: 1432
		[AccessedThroughProperty("PanItem")]
		[CompilerGenerated]
		private StackPanel callbackConfig;

		// Token: 0x04000599 RID: 1433
		[AccessedThroughProperty("ItemOverall")]
		[CompilerGenerated]
		private MyListItem templateConfig;

		// Token: 0x0400059A RID: 1434
		[AccessedThroughProperty("ItemSetup")]
		[CompilerGenerated]
		private MyListItem methodConfig;

		// Token: 0x0400059B RID: 1435
		[AccessedThroughProperty("ItemMod")]
		[CompilerGenerated]
		private MyListItem _TaskConfig;

		// Token: 0x0400059C RID: 1436
		[AccessedThroughProperty("ItemModDisabled")]
		[CompilerGenerated]
		private MyListItem consumerConfig;

		// Token: 0x0400059D RID: 1437
		private bool m_ConfigurationConfig;
	}
}
