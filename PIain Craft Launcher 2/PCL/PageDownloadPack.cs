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
	// Token: 0x020000EE RID: 238
	[DesignerGenerated]
	public class PageDownloadPack : MyPageRight, IComponentConnector
	{
		// Token: 0x060008AB RID: 2219 RVA: 0x00006765 File Offset: 0x00004965
		public PageDownloadPack()
		{
			base.Initialized += this.PageDownloadPack_Inited;
			this.InitializeComponent();
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00046324 File Offset: 0x00044524
		private void PageDownloadPack_Inited(object sender, EventArgs e)
		{
			base.PageLoaderInit(this.Load, this.PanLoad, this.PanContent, this.PanAlways, PageDownloadPack._UtilsReader, delegate(ModLoader.LoaderBase a0)
			{
				this.Load_OnFinish();
			}, new Func<object>(PageDownloadPack.LoaderInput), true);
			if (ModDownloadLib.m_StructField == -1)
			{
				ModDownloadLib.m_StructField = Math.Max(ModDownloadLib.m_StructField, int.Parse(((MyComboBoxItem)this.TextSearchVersion.Items[1]).Content.ToString().Split(".")[1]));
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x000463B8 File Offset: 0x000445B8
		private static ModComp.CompProjectRequest LoaderInput()
		{
			checked
			{
				ModComp.CompProjectRequest compProjectRequest = new ModComp.CompProjectRequest(ModComp.CompType.ModPack, PageDownloadPack._ClassReader, (PageDownloadPack._PolicyReader + 1) * 40);
				if (ModMain.m_UtilsIterator != null)
				{
					ModComp.CompProjectRequest compProjectRequest2 = compProjectRequest;
					compProjectRequest2._RecordRepository = ModMain.m_UtilsIterator.TextSearchName.Text;
					compProjectRequest2.m_ParameterRepository = ((Operators.CompareString(ModMain.m_UtilsIterator.TextSearchVersion.Text, "全部 (也可自行输入)", false) == 0) ? null : ((ModMain.m_UtilsIterator.TextSearchVersion.Text.Contains(".") || ModMain.m_UtilsIterator.TextSearchVersion.Text.Contains("w")) ? ModMain.m_UtilsIterator.TextSearchVersion.Text : null));
					compProjectRequest2.Tag = Conversions.ToString(NewLateBinding.LateGet(ModMain.m_UtilsIterator.ComboSearchTag.SelectedItem, null, "Tag", new object[0], null, null, null));
					compProjectRequest2.Source = (ModComp.CompSourceType)Math.Round(ModBase.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(ModMain.m_UtilsIterator.ComboSearchSource.SelectedItem, null, "Tag", new object[0], null, null, null))));
				}
				return compProjectRequest;
			}
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x000464D0 File Offset: 0x000446D0
		private void Load_OnFinish()
		{
			checked
			{
				try
				{
					ModBase.Log(string.Format("[Comp] 开始可视化整合包列表，已储藏 {0} 个结果，当前在第 {1} 页", PageDownloadPack._ClassReader._CreatorRepository.Count, PageDownloadPack._PolicyReader + 1), ModBase.LogLevel.Normal, "出现错误");
					this.PanProjects.Children.Clear();
					int num = Math.Min(PageDownloadPack._PolicyReader * 40, PageDownloadPack._ClassReader._CreatorRepository.Count - 1);
					int num2 = Math.Min((PageDownloadPack._PolicyReader + 1) * 40 - 1, PageDownloadPack._ClassReader._CreatorRepository.Count - 1);
					for (int i = num; i <= num2; i++)
					{
						this.PanProjects.Children.Add(PageDownloadPack._ClassReader._CreatorRepository[i].ToCompItem(PageDownloadPack._UtilsReader.Input.m_ParameterRepository == null, true));
					}
					this.CardPages.Visibility = ((PageDownloadPack._ClassReader._CreatorRepository.Count > 40 || PageDownloadPack._ClassReader.m_ServiceRepository < PageDownloadPack._ClassReader.m_InvocationRepository || PageDownloadPack._ClassReader.m_ProxyRepository < PageDownloadPack._ClassReader.m_MessageRepository) ? Visibility.Visible : Visibility.Collapsed);
					this.LabPage.Text = Conversions.ToString(PageDownloadPack._PolicyReader + 1);
					this.BtnPageFirst.IsEnabled = (PageDownloadPack._PolicyReader > 1);
					this.BtnPageFirst.Opacity = ((PageDownloadPack._PolicyReader > 1) ? 1.0 : 0.2);
					this.BtnPageLeft.IsEnabled = (PageDownloadPack._PolicyReader > 0);
					this.BtnPageLeft.Opacity = ((PageDownloadPack._PolicyReader > 0) ? 1.0 : 0.2);
					bool flag = PageDownloadPack._ClassReader._CreatorRepository.Count > 40 * (PageDownloadPack._PolicyReader + 1) || PageDownloadPack._ClassReader.m_ServiceRepository < PageDownloadPack._ClassReader.m_InvocationRepository || PageDownloadPack._ClassReader.m_ProxyRepository < PageDownloadPack._ClassReader.m_MessageRepository;
					this.BtnPageRight.IsEnabled = flag;
					this.BtnPageRight.Opacity = (flag ? 1.0 : 0.2);
					if (PageDownloadPack._ClassReader._InitializerRepository == null)
					{
						this.HintError.Visibility = Visibility.Collapsed;
					}
					else
					{
						this.HintError.Visibility = Visibility.Visible;
						this.HintError.Text = PageDownloadPack._ClassReader._InitializerRepository;
					}
					this.PanBack.ScrollToTop();
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "可视化整合包列表出错", ModBase.LogLevel.Feedback, "出现错误");
				}
			}
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0004677C File Offset: 0x0004497C
		private void Load_State(object sender, MyLoading.MyLoadingState state, MyLoading.MyLoadingState oldState)
		{
			ModBase.LoadState state2 = PageDownloadPack._UtilsReader.State;
			if (state2 == ModBase.LoadState.Failed)
			{
				string text = "";
				if (PageDownloadPack._UtilsReader.Error != null)
				{
					text = PageDownloadPack._UtilsReader.Error.Message;
				}
				if (text.Contains("不是有效的 json 文件"))
				{
					ModBase.Log("[Download] 下载的整合包列表 json 文件损坏，已自动重试", ModBase.LogLevel.Debug, "出现错误");
					base.PageLoaderRestart(null, true);
				}
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00006786 File Offset: 0x00004986
		private void BtnPageFirst_Click(object sender, RoutedEventArgs e)
		{
			this.ChangePage(0);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0000678F File Offset: 0x0000498F
		private void BtnPageLeft_Click(object sender, RoutedEventArgs e)
		{
			this.ChangePage(checked(PageDownloadPack._PolicyReader - 1));
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0000679E File Offset: 0x0000499E
		private void BtnPageRight_Click(object sender, RoutedEventArgs e)
		{
			this.ChangePage(checked(PageDownloadPack._PolicyReader + 1));
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x000467E0 File Offset: 0x000449E0
		private void ChangePage(int NewPage)
		{
			this.CardPages.IsEnabled = false;
			PageDownloadPack._PolicyReader = NewPage;
			ModMain._ProcessIterator.BackToTop();
			ModBase.Log(string.Format("[Download] 整合包切换到第 {0} 页", checked(PageDownloadPack._PolicyReader + 1)), ModBase.LogLevel.Normal, "出现错误");
			ModBase.RunInThread(delegate
			{
				Thread.Sleep(100);
				ModBase.RunInUi(delegate()
				{
					this.CardPages.IsEnabled = true;
				}, false);
				PageDownloadPack._UtilsReader.Start(null, false);
			});
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000041AF File Offset: 0x000023AF
		private void BtnSearchInstall_Click(object sender, EventArgs e)
		{
			ModModpack.ModpackInstall();
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0004683C File Offset: 0x00044A3C
		private void StartNewSearch()
		{
			PageDownloadPack._PolicyReader = 0;
			ModLoader.LoaderTask<ModComp.CompProjectRequest, int> utilsReader = PageDownloadPack._UtilsReader;
			object obj = PageDownloadPack.LoaderInput();
			if (utilsReader.ShouldStart(ref obj, false, false))
			{
				PageDownloadPack._ClassReader = new ModComp.CompProjectStorage();
			}
			PageDownloadPack._UtilsReader.Start(null, false);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x000067AD File Offset: 0x000049AD
		private void EnterTrigger(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.StartNewSearch();
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0004687C File Offset: 0x00044A7C
		private void BtnSearchReset_Click(object sender, EventArgs e)
		{
			this.TextSearchName.Text = "";
			this.TextSearchVersion.Text = "全部 (也可自行输入)";
			this.TextSearchVersion.SelectedIndex = 0;
			this.ComboSearchSource.SelectedIndex = 0;
			this.ComboSearchTag.SelectedIndex = 0;
			PageDownloadPack._UtilsReader.LastFinishedTime = 0L;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x000067BE File Offset: 0x000049BE
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x000067C6 File Offset: 0x000049C6
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x000067CF File Offset: 0x000049CF
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x000067D7 File Offset: 0x000049D7
		internal virtual MyCard PanAlways { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x000067E0 File Offset: 0x000049E0
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x000468E0 File Offset: 0x00044AE0
		internal virtual MyTextBox TextSearchName
		{
			[CompilerGenerated]
			get
			{
				return this.descriptorReader;
			}
			[CompilerGenerated]
			set
			{
				KeyEventHandler value2 = new KeyEventHandler(this.EnterTrigger);
				MyTextBox myTextBox = this.descriptorReader;
				if (myTextBox != null)
				{
					myTextBox.KeyDown -= value2;
				}
				this.descriptorReader = value;
				myTextBox = this.descriptorReader;
				if (myTextBox != null)
				{
					myTextBox.KeyDown += value2;
				}
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x000067E8 File Offset: 0x000049E8
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x000067F0 File Offset: 0x000049F0
		internal virtual MyComboBox ComboSearchSource { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x000067F9 File Offset: 0x000049F9
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x00046924 File Offset: 0x00044B24
		internal virtual MyComboBox TextSearchVersion
		{
			[CompilerGenerated]
			get
			{
				return this.m_DefinitionReader;
			}
			[CompilerGenerated]
			set
			{
				KeyEventHandler value2 = new KeyEventHandler(this.EnterTrigger);
				MyComboBox definitionReader = this.m_DefinitionReader;
				if (definitionReader != null)
				{
					definitionReader.KeyDown -= value2;
				}
				this.m_DefinitionReader = value;
				definitionReader = this.m_DefinitionReader;
				if (definitionReader != null)
				{
					definitionReader.KeyDown += value2;
				}
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00006801 File Offset: 0x00004A01
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x00006809 File Offset: 0x00004A09
		internal virtual MyComboBox ComboSearchTag { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00006812 File Offset: 0x00004A12
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x00046968 File Offset: 0x00044B68
		internal virtual MyButton BtnSearchRun
		{
			[CompilerGenerated]
			get
			{
				return this._ProcReader;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.StartNewSearch();
				};
				MyButton procReader = this._ProcReader;
				if (procReader != null)
				{
					procReader.Click -= value2;
				}
				this._ProcReader = value;
				procReader = this._ProcReader;
				if (procReader != null)
				{
					procReader.Click += value2;
				}
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0000681A File Offset: 0x00004A1A
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x000469AC File Offset: 0x00044BAC
		internal virtual MyButton BtnSearchReset
		{
			[CompilerGenerated]
			get
			{
				return this._ParserClient;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnSearchReset_Click);
				MyButton parserClient = this._ParserClient;
				if (parserClient != null)
				{
					parserClient.Click -= value2;
				}
				this._ParserClient = value;
				parserClient = this._ParserClient;
				if (parserClient != null)
				{
					parserClient.Click += value2;
				}
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00006822 File Offset: 0x00004A22
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x000469F0 File Offset: 0x00044BF0
		internal virtual MyButton BtnSearchInstall
		{
			[CompilerGenerated]
			get
			{
				return this.broadcasterClient;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnSearchInstall_Click);
				MyButton myButton = this.broadcasterClient;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.broadcasterClient = value;
				myButton = this.broadcasterClient;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0000682A File Offset: 0x00004A2A
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x00006832 File Offset: 0x00004A32
		internal virtual StackPanel PanContent { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0000683B File Offset: 0x00004A3B
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x00006843 File Offset: 0x00004A43
		internal virtual MyHint HintError { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0000684C File Offset: 0x00004A4C
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x00006854 File Offset: 0x00004A54
		internal virtual MyCard CardProjects { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0000685D File Offset: 0x00004A5D
		// (set) Token: 0x060008D1 RID: 2257 RVA: 0x00006865 File Offset: 0x00004A65
		internal virtual StackPanel PanProjects { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x0000686E File Offset: 0x00004A6E
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x00006876 File Offset: 0x00004A76
		internal virtual MyCard CardPages { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0000687F File Offset: 0x00004A7F
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x00046A34 File Offset: 0x00044C34
		internal virtual MyIconButton BtnPageFirst
		{
			[CompilerGenerated]
			get
			{
				return this.m_MapperClient;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = delegate(object sender, EventArgs e)
				{
					this.BtnPageFirst_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
				};
				MyIconButton mapperClient = this.m_MapperClient;
				if (mapperClient != null)
				{
					mapperClient.Click -= value2;
				}
				this.m_MapperClient = value;
				mapperClient = this.m_MapperClient;
				if (mapperClient != null)
				{
					mapperClient.Click += value2;
				}
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00006887 File Offset: 0x00004A87
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x00046A78 File Offset: 0x00044C78
		internal virtual MyIconButton BtnPageLeft
		{
			[CompilerGenerated]
			get
			{
				return this.threadClient;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = delegate(object sender, EventArgs e)
				{
					this.BtnPageLeft_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
				};
				MyIconButton myIconButton = this.threadClient;
				if (myIconButton != null)
				{
					myIconButton.Click -= value2;
				}
				this.threadClient = value;
				myIconButton = this.threadClient;
				if (myIconButton != null)
				{
					myIconButton.Click += value2;
				}
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x0000688F File Offset: 0x00004A8F
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x00006897 File Offset: 0x00004A97
		internal virtual TextBlock LabPage { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x000068A0 File Offset: 0x00004AA0
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x00046ABC File Offset: 0x00044CBC
		internal virtual MyIconButton BtnPageRight
		{
			[CompilerGenerated]
			get
			{
				return this.composerClient;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = delegate(object sender, EventArgs e)
				{
					this.BtnPageRight_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
				};
				MyIconButton myIconButton = this.composerClient;
				if (myIconButton != null)
				{
					myIconButton.Click -= value2;
				}
				this.composerClient = value;
				myIconButton = this.composerClient;
				if (myIconButton != null)
				{
					myIconButton.Click += value2;
				}
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x000068A8 File Offset: 0x00004AA8
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x000068B0 File Offset: 0x00004AB0
		internal virtual MyCard PanLoad { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x000068B9 File Offset: 0x00004AB9
		// (set) Token: 0x060008DF RID: 2271 RVA: 0x00046B00 File Offset: 0x00044D00
		internal virtual MyLoading Load
		{
			[CompilerGenerated]
			get
			{
				return this._RepositoryClient;
			}
			[CompilerGenerated]
			set
			{
				MyLoading.StateChangedEventHandler obj = new MyLoading.StateChangedEventHandler(this.Load_State);
				MyLoading repositoryClient = this._RepositoryClient;
				if (repositoryClient != null)
				{
					repositoryClient.InterruptField(obj);
				}
				this._RepositoryClient = value;
				repositoryClient = this._RepositoryClient;
				if (repositoryClient != null)
				{
					repositoryClient.PrintField(obj);
				}
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00046B44 File Offset: 0x00044D44
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_TestClient)
			{
				this.m_TestClient = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagedownload/pagedownloadpack.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00046B74 File Offset: 0x00044D74
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (MyScrollViewer)target;
				return;
			}
			if (connectionId == 2)
			{
				this.PanAlways = (MyCard)target;
				return;
			}
			if (connectionId == 3)
			{
				this.TextSearchName = (MyTextBox)target;
				return;
			}
			if (connectionId == 4)
			{
				this.ComboSearchSource = (MyComboBox)target;
				return;
			}
			if (connectionId == 5)
			{
				this.TextSearchVersion = (MyComboBox)target;
				return;
			}
			if (connectionId == 6)
			{
				this.ComboSearchTag = (MyComboBox)target;
				return;
			}
			if (connectionId == 7)
			{
				this.BtnSearchRun = (MyButton)target;
				return;
			}
			if (connectionId == 8)
			{
				this.BtnSearchReset = (MyButton)target;
				return;
			}
			if (connectionId == 9)
			{
				this.BtnSearchInstall = (MyButton)target;
				return;
			}
			if (connectionId == 10)
			{
				this.PanContent = (StackPanel)target;
				return;
			}
			if (connectionId == 11)
			{
				this.HintError = (MyHint)target;
				return;
			}
			if (connectionId == 12)
			{
				this.CardProjects = (MyCard)target;
				return;
			}
			if (connectionId == 13)
			{
				this.PanProjects = (StackPanel)target;
				return;
			}
			if (connectionId == 14)
			{
				this.CardPages = (MyCard)target;
				return;
			}
			if (connectionId == 15)
			{
				this.BtnPageFirst = (MyIconButton)target;
				return;
			}
			if (connectionId == 16)
			{
				this.BtnPageLeft = (MyIconButton)target;
				return;
			}
			if (connectionId == 17)
			{
				this.LabPage = (TextBlock)target;
				return;
			}
			if (connectionId == 18)
			{
				this.BtnPageRight = (MyIconButton)target;
				return;
			}
			if (connectionId == 19)
			{
				this.PanLoad = (MyCard)target;
				return;
			}
			if (connectionId == 20)
			{
				this.Load = (MyLoading)target;
				return;
			}
			this.m_TestClient = true;
		}

		// Token: 0x040004AE RID: 1198
		public static ModLoader.LoaderTask<ModComp.CompProjectRequest, int> _UtilsReader = new ModLoader.LoaderTask<ModComp.CompProjectRequest, int>("CompProject ModPack", new Action<ModLoader.LoaderTask<ModComp.CompProjectRequest, int>>(ModComp.CompProjectsGet), new Func<ModComp.CompProjectRequest>(PageDownloadPack.LoaderInput), ThreadPriority.Normal)
		{
			ReloadTimeout = 60000
		};

		// Token: 0x040004AF RID: 1199
		public static ModComp.CompProjectStorage _ClassReader = new ModComp.CompProjectStorage();

		// Token: 0x040004B0 RID: 1200
		public static int _PolicyReader = 0;

		// Token: 0x040004B1 RID: 1201
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyScrollViewer m_ProducerReader;

		// Token: 0x040004B2 RID: 1202
		[AccessedThroughProperty("PanAlways")]
		[CompilerGenerated]
		private MyCard m_SchemaReader;

		// Token: 0x040004B3 RID: 1203
		[AccessedThroughProperty("TextSearchName")]
		[CompilerGenerated]
		private MyTextBox descriptorReader;

		// Token: 0x040004B4 RID: 1204
		[CompilerGenerated]
		[AccessedThroughProperty("ComboSearchSource")]
		private MyComboBox publisherReader;

		// Token: 0x040004B5 RID: 1205
		[CompilerGenerated]
		[AccessedThroughProperty("TextSearchVersion")]
		private MyComboBox m_DefinitionReader;

		// Token: 0x040004B6 RID: 1206
		[CompilerGenerated]
		[AccessedThroughProperty("ComboSearchTag")]
		private MyComboBox strategyReader;

		// Token: 0x040004B7 RID: 1207
		[CompilerGenerated]
		[AccessedThroughProperty("BtnSearchRun")]
		private MyButton _ProcReader;

		// Token: 0x040004B8 RID: 1208
		[AccessedThroughProperty("BtnSearchReset")]
		[CompilerGenerated]
		private MyButton _ParserClient;

		// Token: 0x040004B9 RID: 1209
		[CompilerGenerated]
		[AccessedThroughProperty("BtnSearchInstall")]
		private MyButton broadcasterClient;

		// Token: 0x040004BA RID: 1210
		[CompilerGenerated]
		[AccessedThroughProperty("PanContent")]
		private StackPanel _FieldClient;

		// Token: 0x040004BB RID: 1211
		[CompilerGenerated]
		[AccessedThroughProperty("HintError")]
		private MyHint _ReaderClient;

		// Token: 0x040004BC RID: 1212
		[AccessedThroughProperty("CardProjects")]
		[CompilerGenerated]
		private MyCard clientClient;

		// Token: 0x040004BD RID: 1213
		[CompilerGenerated]
		[AccessedThroughProperty("PanProjects")]
		private StackPanel m_ConfigClient;

		// Token: 0x040004BE RID: 1214
		[CompilerGenerated]
		[AccessedThroughProperty("CardPages")]
		private MyCard testsClient;

		// Token: 0x040004BF RID: 1215
		[CompilerGenerated]
		[AccessedThroughProperty("BtnPageFirst")]
		private MyIconButton m_MapperClient;

		// Token: 0x040004C0 RID: 1216
		[AccessedThroughProperty("BtnPageLeft")]
		[CompilerGenerated]
		private MyIconButton threadClient;

		// Token: 0x040004C1 RID: 1217
		[CompilerGenerated]
		[AccessedThroughProperty("LabPage")]
		private TextBlock m_PropertyClient;

		// Token: 0x040004C2 RID: 1218
		[AccessedThroughProperty("BtnPageRight")]
		[CompilerGenerated]
		private MyIconButton composerClient;

		// Token: 0x040004C3 RID: 1219
		[CompilerGenerated]
		[AccessedThroughProperty("PanLoad")]
		private MyCard m_IteratorClient;

		// Token: 0x040004C4 RID: 1220
		[CompilerGenerated]
		[AccessedThroughProperty("Load")]
		private MyLoading _RepositoryClient;

		// Token: 0x040004C5 RID: 1221
		private bool m_TestClient;
	}
}
