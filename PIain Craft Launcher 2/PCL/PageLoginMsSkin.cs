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
	// Token: 0x020000F7 RID: 247
	[DesignerGenerated]
	public class PageLoginMsSkin : Grid, IComponentConnector
	{
		// Token: 0x06000970 RID: 2416 RVA: 0x00006CCB File Offset: 0x00004ECB
		public PageLoginMsSkin()
		{
			base.Loaded += this.PageLoginLegacy_Loaded;
			this.setterClient = false;
			this.InitializeComponent();
			this.Skin.callbackMapper = PageLaunchLeft.annotationMapper;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00006D03 File Offset: 0x00004F03
		private void PageLoginLegacy_Loaded(object sender, RoutedEventArgs e)
		{
			this.Skin.callbackMapper.Start(null, false);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00006D17 File Offset: 0x00004F17
		public void Reload(bool KeepInput)
		{
			this.TextName.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheMsV2Name", null));
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00048B20 File Offset: 0x00046D20
		public static ModLaunch.McLoginMs GetLoginData()
		{
			ModLaunch.McLoginMs result;
			if (ModLaunch.m_ContainerTests.State == ModBase.LoadState.Finished)
			{
				result = new ModLaunch.McLoginMs
				{
					m_SystemMap = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheMsV2OAuthRefresh", null)),
					observerMap = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheMsV2Name", null)),
					paramMap = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheMsV2Access", null)),
					_TagMap = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheMsV2Uuid", null)),
					stubMap = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheMsV2ProfileJson", null))
				};
			}
			else
			{
				result = new ModLaunch.McLoginMs
				{
					m_SystemMap = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheMsV2OAuthRefresh", null)),
					observerMap = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheMsV2Name", null))
				};
			}
			return result;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00006D39 File Offset: 0x00004F39
		private void ShowPanel(object sender, MouseEventArgs e)
		{
			ModAnimation.AniStart(ModAnimation.AaOpacity(this.PanButtons, 1.0 - this.PanButtons.Opacity, 120, 0, null, false), "PageLoginMsSkin Button", false);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00048C0C File Offset: 0x00046E0C
		public void HidePanel()
		{
			if (!this.BtnEdit.ContextMenu.IsOpen && !this.BtnSkin.ContextMenu.IsOpen && !this.PanData.IsMouseOver)
			{
				ModAnimation.AniStart(ModAnimation.AaOpacity(this.PanButtons, -this.PanButtons.Opacity, 120, 0, null, false), "PageLoginMsSkin Button", false);
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00006D6B File Offset: 0x00004F6B
		private void BtnEdit_Click(object sender, EventArgs e)
		{
			this.BtnEdit.ContextMenu.IsOpen = true;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00006D7E File Offset: 0x00004F7E
		public void BtnEditPassword_Click(object sender, RoutedEventArgs e)
		{
			ModBase.OpenWebsite("https://account.live.com/password/Change");
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00006D8A File Offset: 0x00004F8A
		public void BtnEditName_Click(object sender, RoutedEventArgs e)
		{
			ModBase.OpenWebsite("https://www.minecraft.net/zh-hans/msaprofile/mygames/editprofile");
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00048C74 File Offset: 0x00046E74
		private void BtnExit_Click()
		{
			ModBase.m_IdentifierRepository.Set("CacheMsV2OAuthRefresh", "", false, null);
			ModBase.m_IdentifierRepository.Set("CacheMsV2Access", "", false, null);
			ModBase.m_IdentifierRepository.Set("CacheMsV2ProfileJson", "", false, null);
			ModBase.m_IdentifierRepository.Set("CacheMsV2Uuid", "", false, null);
			ModBase.m_IdentifierRepository.Set("CacheMsV2Name", "", false, null);
			ModLaunch.m_ContainerTests.Abort();
			ModMain.recordIterator.RefreshPage(false, true);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00006D96 File Offset: 0x00004F96
		private void BtnSkin_Click(object sender, RoutedEventArgs e)
		{
			this.BtnSkin.ContextMenu.IsOpen = true;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00048D08 File Offset: 0x00046F08
		public void BtnSkinEdit_Click(object sender, RoutedEventArgs e)
		{
			PageLoginMsSkin._Closure$__12-0 CS$<>8__locals1 = new PageLoginMsSkin._Closure$__12-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Me = this;
			if (this.setterClient)
			{
				ModMain.Hint("正在更改皮肤中，请稍候！", ModMain.HintType.Info, true);
				return;
			}
			if (ModLaunch.interceptorTests.State == ModBase.LoadState.Failed)
			{
				ModMain.Hint("登录失败，无法更改皮肤！", ModMain.HintType.Critical, true);
				return;
			}
			CS$<>8__locals1.$VB$Local_SkinInfo = ModMinecraft.McSkinSelect();
			if (CS$<>8__locals1.$VB$Local_SkinInfo._ModelMap)
			{
				ModMain.Hint("正在更改皮肤……", ModMain.HintType.Info, true);
				this.setterClient = true;
				ModBase.RunInNewThread(delegate
				{
					PageLoginMsSkin._Closure$__12-0.VB$StateMachine___Lambda$__0 vb$StateMachine___Lambda$__ = default(PageLoginMsSkin._Closure$__12-0.VB$StateMachine___Lambda$__0);
					vb$StateMachine___Lambda$__.$VB$NonLocal__Closure$__12-0 = CS$<>8__locals1;
					vb$StateMachine___Lambda$__.$State = -1;
					vb$StateMachine___Lambda$__.$Builder = AsyncVoidMethodBuilder.Create();
					vb$StateMachine___Lambda$__.$Builder.Start<PageLoginMsSkin._Closure$__12-0.VB$StateMachine___Lambda$__0>(ref vb$StateMachine___Lambda$__);
				}, "Ms Skin Upload", ThreadPriority.Normal);
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00006DA9 File Offset: 0x00004FA9
		public void BtnSkinSave_Click(object sender, RoutedEventArgs e)
		{
			this.Skin.BtnSkinSave_Click();
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00006DB6 File Offset: 0x00004FB6
		public void BtnSkinRefresh_Click(object sender, RoutedEventArgs e)
		{
			this.Skin.RefreshClick();
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00006DC3 File Offset: 0x00004FC3
		public void BtnSkinCape_Click(object sender, RoutedEventArgs e)
		{
			this.Skin.BtnSkinCape_Click();
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00006DD0 File Offset: 0x00004FD0
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x00048D98 File Offset: 0x00046F98
		internal virtual Grid PanData
		{
			[CompilerGenerated]
			get
			{
				return this.factoryClient;
			}
			[CompilerGenerated]
			set
			{
				MouseEventHandler value2 = new MouseEventHandler(this.ShowPanel);
				MouseEventHandler value3 = delegate(object sender, MouseEventArgs e)
				{
					this.HidePanel();
				};
				Grid grid = this.factoryClient;
				if (grid != null)
				{
					grid.MouseEnter -= value2;
					grid.MouseLeave -= value3;
				}
				this.factoryClient = value;
				grid = this.factoryClient;
				if (grid != null)
				{
					grid.MouseEnter += value2;
					grid.MouseLeave += value3;
				}
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x00006DD8 File Offset: 0x00004FD8
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x00006DE0 File Offset: 0x00004FE0
		internal virtual TextBlock TextName { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x00006DE9 File Offset: 0x00004FE9
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x00006DF1 File Offset: 0x00004FF1
		internal virtual Border PanButtons { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00006DFA File Offset: 0x00004FFA
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x00048DF8 File Offset: 0x00046FF8
		internal virtual MyIconButton BtnSkin
		{
			[CompilerGenerated]
			get
			{
				return this._WorkerClient;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = delegate(object sender, EventArgs e)
				{
					this.BtnSkin_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
				};
				MyIconButton workerClient = this._WorkerClient;
				if (workerClient != null)
				{
					workerClient.Click -= value2;
				}
				this._WorkerClient = value;
				workerClient = this._WorkerClient;
				if (workerClient != null)
				{
					workerClient.Click += value2;
				}
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00006E02 File Offset: 0x00005002
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x00048E3C File Offset: 0x0004703C
		internal virtual MyIconButton BtnEdit
		{
			[CompilerGenerated]
			get
			{
				return this._ConnectionClient;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = new MyIconButton.ClickEventHandler(this.BtnEdit_Click);
				MyIconButton connectionClient = this._ConnectionClient;
				if (connectionClient != null)
				{
					connectionClient.Click -= value2;
				}
				this._ConnectionClient = value;
				connectionClient = this._ConnectionClient;
				if (connectionClient != null)
				{
					connectionClient.Click += value2;
				}
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00006E0A File Offset: 0x0000500A
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x00048E80 File Offset: 0x00047080
		internal virtual MyIconButton BtnExit
		{
			[CompilerGenerated]
			get
			{
				return this._ServerClient;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = delegate(object sender, EventArgs e)
				{
					this.BtnExit_Click();
				};
				MyIconButton serverClient = this._ServerClient;
				if (serverClient != null)
				{
					serverClient.Click -= value2;
				}
				this._ServerClient = value;
				serverClient = this._ServerClient;
				if (serverClient != null)
				{
					serverClient.Click += value2;
				}
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00006E12 File Offset: 0x00005012
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x00006E1A File Offset: 0x0000501A
		internal virtual MySkin Skin { get; set; }

		// Token: 0x0600098D RID: 2445 RVA: 0x00048EC4 File Offset: 0x000470C4
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.statusClient)
			{
				this.statusClient = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/pageloginmsskin.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00048EF4 File Offset: 0x000470F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanData = (Grid)target;
				return;
			}
			if (connectionId == 2)
			{
				this.TextName = (TextBlock)target;
				return;
			}
			if (connectionId == 3)
			{
				this.PanButtons = (Border)target;
				return;
			}
			if (connectionId == 4)
			{
				this.BtnSkin = (MyIconButton)target;
				return;
			}
			if (connectionId == 5)
			{
				((ContextMenu)target).Closed += delegate(object sender, RoutedEventArgs e)
				{
					this.HidePanel();
				};
				return;
			}
			if (connectionId == 6)
			{
				this.BtnEdit = (MyIconButton)target;
				return;
			}
			if (connectionId == 7)
			{
				((ContextMenu)target).Closed += delegate(object sender, RoutedEventArgs e)
				{
					this.HidePanel();
				};
				return;
			}
			if (connectionId == 8)
			{
				this.BtnExit = (MyIconButton)target;
				return;
			}
			if (connectionId == 9)
			{
				this.Skin = (MySkin)target;
				return;
			}
			this.statusClient = true;
		}

		// Token: 0x040004F2 RID: 1266
		private bool setterClient;

		// Token: 0x040004F3 RID: 1267
		[CompilerGenerated]
		[AccessedThroughProperty("PanData")]
		private Grid factoryClient;

		// Token: 0x040004F4 RID: 1268
		[AccessedThroughProperty("TextName")]
		[CompilerGenerated]
		private TextBlock m_ExporterClient;

		// Token: 0x040004F5 RID: 1269
		[AccessedThroughProperty("PanButtons")]
		[CompilerGenerated]
		private Border _ImporterClient;

		// Token: 0x040004F6 RID: 1270
		[CompilerGenerated]
		[AccessedThroughProperty("BtnSkin")]
		private MyIconButton _WorkerClient;

		// Token: 0x040004F7 RID: 1271
		[CompilerGenerated]
		[AccessedThroughProperty("BtnEdit")]
		private MyIconButton _ConnectionClient;

		// Token: 0x040004F8 RID: 1272
		[CompilerGenerated]
		[AccessedThroughProperty("BtnExit")]
		private MyIconButton _ServerClient;

		// Token: 0x040004F9 RID: 1273
		[CompilerGenerated]
		[AccessedThroughProperty("Skin")]
		private MySkin resolverClient;

		// Token: 0x040004FA RID: 1274
		private bool statusClient;
	}
}
