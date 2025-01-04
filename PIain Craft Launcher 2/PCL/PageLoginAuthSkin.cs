using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020000F2 RID: 242
	[DesignerGenerated]
	public class PageLoginAuthSkin : Grid, IComponentConnector
	{
		// Token: 0x06000937 RID: 2359 RVA: 0x00006B29 File Offset: 0x00004D29
		public PageLoginAuthSkin()
		{
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.PageLoginLegacy_Loaded();
			};
			this.InitializeComponent();
			this.Skin.callbackMapper = PageLaunchLeft.m_FacadeMapper;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00006B5A File Offset: 0x00004D5A
		private void PageLoginLegacy_Loaded()
		{
			this.Skin.callbackMapper.Start(null, false);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00047E08 File Offset: 0x00046008
		public void Reload(bool KeepInput)
		{
			this.TextName.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheAuthName", null));
			this.TextEmail.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheAuthUsername", null));
			this.TextEmail.Visibility = (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiLauncherEmail", null)) ? Visibility.Collapsed : Visibility.Visible);
			this.PageLoginLegacy_Loaded();
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00047E84 File Offset: 0x00046084
		public static ModLaunch.McLoginServer GetLoginData()
		{
			string interpreterMap = Conversions.ToString(Operators.ConcatenateObject(Information.IsNothing(ModMinecraft.AddClient()) ? ModBase.m_IdentifierRepository.Get("CacheAuthServerServer", null) : ModBase.m_IdentifierRepository.Get("VersionServerAuthServer", ModMinecraft.AddClient()), "/authserver"));
			return new ModLaunch.McLoginServer(ModLaunch.McLoginType.Auth)
			{
				m_SerializerMap = "Auth",
				interpreterMap = interpreterMap,
				m_IssuerMap = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheAuthUsername", null)),
				_IndexerMap = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheAuthPass", null)),
				m_WatcherMap = "Authlib-Injector",
				Type = ModLaunch.McLoginType.Auth
			};
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00047F34 File Offset: 0x00046134
		private void PageLoginAuthSkin_MouseEnter(object sender, MouseEventArgs e)
		{
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaOpacity(this.BtnEdit, 1.0 - this.BtnEdit.Opacity, 80, 0, null, false),
				ModAnimation.AaHeight(this.BtnEdit, 25.5 - this.BtnEdit.Height, 140, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
				ModAnimation.AaHeight(this.BtnEdit, -1.5, 50, 140, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false),
				ModAnimation.AaOpacity(this.BtnExit, 1.0 - this.BtnExit.Opacity, 80, 0, null, false),
				ModAnimation.AaHeight(this.BtnExit, 25.5 - this.BtnExit.Height, 140, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
				ModAnimation.AaHeight(this.BtnExit, -1.5, 50, 140, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false)
			}, "PageLoginAuthSkin Button", false);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00048064 File Offset: 0x00046264
		private void PageLoginAuthSkin_MouseLeave(object sender, MouseEventArgs e)
		{
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaOpacity(this.BtnEdit, -this.BtnEdit.Opacity, 120, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
				ModAnimation.AaHeight(this.BtnEdit, 14.0 - this.BtnEdit.Height, 120, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false),
				ModAnimation.AaOpacity(this.BtnExit, -this.BtnExit.Opacity, 120, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
				ModAnimation.AaHeight(this.BtnExit, 14.0 - this.BtnExit.Height, 120, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false)
			}, "PageLoginAuthSkin Button", false);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00048134 File Offset: 0x00046334
		private void BtnEdit_Click(object sender, EventArgs e)
		{
			if (ModLaunch.interceptorTests.State != ModBase.LoadState.Loading)
			{
				ModMain.Hint("正在尝试更换，请稍候！", ModMain.HintType.Info, true);
				ModBase.m_IdentifierRepository.Set("CacheAuthUuid", "", false, null);
				ModBase.m_IdentifierRepository.Set("CacheAuthName", "", false, null);
				ModBase.RunInThread(delegate
				{
					try
					{
						ModLaunch.McLoginServer loginData = PageLoginAuthSkin.GetLoginData();
						loginData._IdentifierMap = true;
						ModLaunch.interceptorTests.WaitForExit(loginData, null, true);
						ModBase.RunInUi(delegate()
						{
							this.Reload(true);
						}, false);
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "更换角色失败", ModBase.LogLevel.Hint, "出现错误");
					}
				});
				return;
			}
			ModBase.Log("[Launch] 要求更换角色，但登录加载器繁忙", ModBase.LogLevel.Debug, "出现错误");
			if (((ModLaunch.McLoginServer)ModLaunch.interceptorTests.Input)._IdentifierMap)
			{
				ModMain.Hint("正在尝试更换，请稍候！", ModMain.HintType.Info, true);
				return;
			}
			ModMain.Hint("正在登录中，请稍后再更换角色！", ModMain.HintType.Critical, true);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000481D8 File Offset: 0x000463D8
		public static void ExitLogin()
		{
			ModBase.m_IdentifierRepository.Set("CacheAuthAccess", "", false, null);
			ModBase.m_IdentifierRepository.Set("CacheAuthUuid", "", false, null);
			ModBase.m_IdentifierRepository.Set("CacheAuthName", "", false, null);
			ModLaunch._ProcessTests.Input = null;
			ModMain.recordIterator.RefreshPage(false, true);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00048240 File Offset: 0x00046440
		private void Skin_Click(object sender, MouseButtonEventArgs e)
		{
			string text = Conversions.ToString((ModMinecraft.AddClient() != null) ? ModBase.m_IdentifierRepository.Get("VersionServerAuthRegister", ModMinecraft.AddClient()) : ModBase.m_IdentifierRepository.Get("CacheAuthServerRegister", null));
			if (string.IsNullOrEmpty(new ValidateHttp().Validate(text)))
			{
				ModBase.OpenWebsite(text.Replace("/auth/register", "/user/closet"));
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00006B6E File Offset: 0x00004D6E
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x000482A8 File Offset: 0x000464A8
		internal virtual Grid PanData
		{
			[CompilerGenerated]
			get
			{
				return this.methodClient;
			}
			[CompilerGenerated]
			set
			{
				MouseEventHandler value2 = new MouseEventHandler(this.PageLoginAuthSkin_MouseEnter);
				MouseEventHandler value3 = new MouseEventHandler(this.PageLoginAuthSkin_MouseLeave);
				Grid grid = this.methodClient;
				if (grid != null)
				{
					grid.MouseEnter -= value2;
					grid.MouseLeave -= value3;
				}
				this.methodClient = value;
				grid = this.methodClient;
				if (grid != null)
				{
					grid.MouseEnter += value2;
					grid.MouseLeave += value3;
				}
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00006B76 File Offset: 0x00004D76
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x00006B7E File Offset: 0x00004D7E
		internal virtual TextBlock TextName { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00006B87 File Offset: 0x00004D87
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x00006B8F File Offset: 0x00004D8F
		internal virtual TextBlock TextEmail { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00006B98 File Offset: 0x00004D98
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x00048308 File Offset: 0x00046508
		internal virtual MyIconButton BtnEdit
		{
			[CompilerGenerated]
			get
			{
				return this.configurationClient;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = new MyIconButton.ClickEventHandler(this.BtnEdit_Click);
				MyIconButton myIconButton = this.configurationClient;
				if (myIconButton != null)
				{
					myIconButton.Click -= value2;
				}
				this.configurationClient = value;
				myIconButton = this.configurationClient;
				if (myIconButton != null)
				{
					myIconButton.Click += value2;
				}
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x00006BA0 File Offset: 0x00004DA0
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x0004834C File Offset: 0x0004654C
		internal virtual MyIconButton BtnExit
		{
			[CompilerGenerated]
			get
			{
				return this.getterClient;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = (PageLoginAuthSkin._Closure$__.$IR27-2 == null) ? (PageLoginAuthSkin._Closure$__.$IR27-2 = delegate(object sender, EventArgs e)
				{
					PageLoginAuthSkin.ExitLogin();
				}) : PageLoginAuthSkin._Closure$__.$IR27-2;
				MyIconButton myIconButton = this.getterClient;
				if (myIconButton != null)
				{
					myIconButton.Click -= value2;
				}
				this.getterClient = value;
				myIconButton = this.getterClient;
				if (myIconButton != null)
				{
					myIconButton.Click += value2;
				}
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x00006BA8 File Offset: 0x00004DA8
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x000483A8 File Offset: 0x000465A8
		internal virtual MySkin Skin
		{
			[CompilerGenerated]
			get
			{
				return this.m_TokenClient;
			}
			[CompilerGenerated]
			set
			{
				MySkin.ClickEventHandler value2 = new MySkin.ClickEventHandler(this.Skin_Click);
				MySkin tokenClient = this.m_TokenClient;
				if (tokenClient != null)
				{
					tokenClient.Click -= value2;
				}
				this.m_TokenClient = value;
				tokenClient = this.m_TokenClient;
				if (tokenClient != null)
				{
					tokenClient.Click += value2;
				}
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x000483EC File Offset: 0x000465EC
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.expressionClient)
			{
				this.expressionClient = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/pageloginauthskin.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0004841C File Offset: 0x0004661C
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
				this.TextEmail = (TextBlock)target;
				return;
			}
			if (connectionId == 4)
			{
				this.BtnEdit = (MyIconButton)target;
				return;
			}
			if (connectionId == 5)
			{
				this.BtnExit = (MyIconButton)target;
				return;
			}
			if (connectionId == 6)
			{
				this.Skin = (MySkin)target;
				return;
			}
			this.expressionClient = true;
		}

		// Token: 0x040004E2 RID: 1250
		[CompilerGenerated]
		[AccessedThroughProperty("PanData")]
		private Grid methodClient;

		// Token: 0x040004E3 RID: 1251
		[AccessedThroughProperty("TextName")]
		[CompilerGenerated]
		private TextBlock taskClient;

		// Token: 0x040004E4 RID: 1252
		[AccessedThroughProperty("TextEmail")]
		[CompilerGenerated]
		private TextBlock consumerClient;

		// Token: 0x040004E5 RID: 1253
		[AccessedThroughProperty("BtnEdit")]
		[CompilerGenerated]
		private MyIconButton configurationClient;

		// Token: 0x040004E6 RID: 1254
		[AccessedThroughProperty("BtnExit")]
		[CompilerGenerated]
		private MyIconButton getterClient;

		// Token: 0x040004E7 RID: 1255
		[AccessedThroughProperty("Skin")]
		[CompilerGenerated]
		private MySkin m_TokenClient;

		// Token: 0x040004E8 RID: 1256
		private bool expressionClient;
	}
}
