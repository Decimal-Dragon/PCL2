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
	// Token: 0x020000FA RID: 250
	[DesignerGenerated]
	public class PageLoginNideSkin : Grid, IComponentConnector
	{
		// Token: 0x0600099C RID: 2460 RVA: 0x00006E6D File Offset: 0x0000506D
		public PageLoginNideSkin()
		{
			base.Loaded += this.PageLoginLegacy_Loaded;
			this.InitializeComponent();
			this.Skin.callbackMapper = PageLaunchLeft.m_AdapterMapper;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00006E9E File Offset: 0x0000509E
		private void PageLoginLegacy_Loaded(object sender, RoutedEventArgs e)
		{
			this.Skin.callbackMapper.Start(null, false);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00049490 File Offset: 0x00047690
		public void Reload(bool KeepInput)
		{
			this.TextName.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheNideName", null));
			this.TextEmail.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheNideUsername", null));
			this.TextEmail.Visibility = (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiLauncherEmail", null)) ? Visibility.Collapsed : Visibility.Visible);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00049504 File Offset: 0x00047704
		public static ModLaunch.McLoginServer GetLoginData()
		{
			string str = Conversions.ToString(Information.IsNothing(ModMinecraft.AddClient()) ? ModBase.m_IdentifierRepository.Get("CacheNideServer", null) : ModBase.m_IdentifierRepository.Get("VersionServerNide", ModMinecraft.AddClient()));
			return new ModLaunch.McLoginServer(ModLaunch.McLoginType.Nide)
			{
				m_SerializerMap = "Nide",
				m_IssuerMap = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheNideUsername", null)),
				_IndexerMap = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheNidePass", null)),
				m_WatcherMap = "统一通行证",
				Type = ModLaunch.McLoginType.Nide,
				interpreterMap = "https://auth.mc-user.com:233/" + str + "/authserver"
			};
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000495B8 File Offset: 0x000477B8
		private void PageLoginNideSkin_MouseEnter(object sender, MouseEventArgs e)
		{
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaOpacity(this.BtnEdit, 1.0 - this.BtnEdit.Opacity, 80, 0, null, false),
				ModAnimation.AaHeight(this.BtnEdit, 25.5 - this.BtnEdit.Height, 140, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
				ModAnimation.AaHeight(this.BtnEdit, -1.5, 50, 140, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false),
				ModAnimation.AaOpacity(this.BtnExit, 1.0 - this.BtnExit.Opacity, 80, 0, null, false),
				ModAnimation.AaHeight(this.BtnExit, 25.5 - this.BtnExit.Height, 140, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
				ModAnimation.AaHeight(this.BtnExit, -1.5, 50, 140, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false)
			}, "PageLoginNideSkin Button", false);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x000496E8 File Offset: 0x000478E8
		private void PageLoginNideSkin_MouseLeave(object sender, MouseEventArgs e)
		{
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaOpacity(this.BtnEdit, -this.BtnEdit.Opacity, 120, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
				ModAnimation.AaHeight(this.BtnEdit, 14.0 - this.BtnEdit.Height, 120, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false),
				ModAnimation.AaOpacity(this.BtnExit, -this.BtnExit.Opacity, 120, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
				ModAnimation.AaHeight(this.BtnExit, 14.0 - this.BtnExit.Height, 120, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false)
			}, "PageLoginNideSkin Button", false);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00006EB2 File Offset: 0x000050B2
		private void BtnEdit_Click(object sender, EventArgs e)
		{
			ModBase.OpenWebsite("https://login.mc-user.com:233/account/changepw");
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00006EBE File Offset: 0x000050BE
		public static void ExitLogin()
		{
			ModBase.m_IdentifierRepository.Set("CacheNideAccess", "", false, null);
			ModLaunch.dispatcherTests.Input = null;
			ModMain.recordIterator.RefreshPage(false, true);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x000497B8 File Offset: 0x000479B8
		private void Skin_Click(object sender, MouseButtonEventArgs e)
		{
			ModBase.OpenWebsite(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("https://login.mc-user.com:233/", Information.IsNothing(ModMinecraft.AddClient()) ? ModBase.m_IdentifierRepository.Get("CacheNideServer", null) : ModBase.m_IdentifierRepository.Get("VersionServerNide", ModMinecraft.AddClient())), "/skin")));
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x00006EED File Offset: 0x000050ED
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x00049818 File Offset: 0x00047A18
		internal virtual Grid PanData
		{
			[CompilerGenerated]
			get
			{
				return this._RoleClient;
			}
			[CompilerGenerated]
			set
			{
				MouseEventHandler value2 = new MouseEventHandler(this.PageLoginNideSkin_MouseEnter);
				MouseEventHandler value3 = new MouseEventHandler(this.PageLoginNideSkin_MouseLeave);
				Grid roleClient = this._RoleClient;
				if (roleClient != null)
				{
					roleClient.MouseEnter -= value2;
					roleClient.MouseLeave -= value3;
				}
				this._RoleClient = value;
				roleClient = this._RoleClient;
				if (roleClient != null)
				{
					roleClient.MouseEnter += value2;
					roleClient.MouseLeave += value3;
				}
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00006EF5 File Offset: 0x000050F5
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x00006EFD File Offset: 0x000050FD
		internal virtual TextBlock TextName { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00006F06 File Offset: 0x00005106
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x00006F0E File Offset: 0x0000510E
		internal virtual TextBlock TextEmail { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00006F17 File Offset: 0x00005117
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x00049878 File Offset: 0x00047A78
		internal virtual MyIconButton BtnEdit
		{
			[CompilerGenerated]
			get
			{
				return this.m_ValClient;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = new MyIconButton.ClickEventHandler(this.BtnEdit_Click);
				MyIconButton valClient = this.m_ValClient;
				if (valClient != null)
				{
					valClient.Click -= value2;
				}
				this.m_ValClient = value;
				valClient = this.m_ValClient;
				if (valClient != null)
				{
					valClient.Click += value2;
				}
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00006F1F File Offset: 0x0000511F
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x000498BC File Offset: 0x00047ABC
		internal virtual MyIconButton BtnExit
		{
			[CompilerGenerated]
			get
			{
				return this.m_AttrClient;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = (PageLoginNideSkin._Closure$__.$IR27-1 == null) ? (PageLoginNideSkin._Closure$__.$IR27-1 = delegate(object sender, EventArgs e)
				{
					PageLoginNideSkin.ExitLogin();
				}) : PageLoginNideSkin._Closure$__.$IR27-1;
				MyIconButton attrClient = this.m_AttrClient;
				if (attrClient != null)
				{
					attrClient.Click -= value2;
				}
				this.m_AttrClient = value;
				attrClient = this.m_AttrClient;
				if (attrClient != null)
				{
					attrClient.Click += value2;
				}
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00006F27 File Offset: 0x00005127
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x00049918 File Offset: 0x00047B18
		internal virtual MySkin Skin
		{
			[CompilerGenerated]
			get
			{
				return this.m_CandidateClient;
			}
			[CompilerGenerated]
			set
			{
				MySkin.ClickEventHandler value2 = new MySkin.ClickEventHandler(this.Skin_Click);
				MySkin candidateClient = this.m_CandidateClient;
				if (candidateClient != null)
				{
					candidateClient.Click -= value2;
				}
				this.m_CandidateClient = value;
				candidateClient = this.m_CandidateClient;
				if (candidateClient != null)
				{
					candidateClient.Click += value2;
				}
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0004995C File Offset: 0x00047B5C
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_AdvisorClient)
			{
				this.m_AdvisorClient = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/pageloginnideskin.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0004998C File Offset: 0x00047B8C
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
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
			this.m_AdvisorClient = true;
		}

		// Token: 0x04000503 RID: 1283
		[AccessedThroughProperty("PanData")]
		[CompilerGenerated]
		private Grid _RoleClient;

		// Token: 0x04000504 RID: 1284
		[AccessedThroughProperty("TextName")]
		[CompilerGenerated]
		private TextBlock m_StructClient;

		// Token: 0x04000505 RID: 1285
		[CompilerGenerated]
		[AccessedThroughProperty("TextEmail")]
		private TextBlock _PrinterClient;

		// Token: 0x04000506 RID: 1286
		[AccessedThroughProperty("BtnEdit")]
		[CompilerGenerated]
		private MyIconButton m_ValClient;

		// Token: 0x04000507 RID: 1287
		[AccessedThroughProperty("BtnExit")]
		[CompilerGenerated]
		private MyIconButton m_AttrClient;

		// Token: 0x04000508 RID: 1288
		[CompilerGenerated]
		[AccessedThroughProperty("Skin")]
		private MySkin m_CandidateClient;

		// Token: 0x04000509 RID: 1289
		private bool m_AdvisorClient;
	}
}
