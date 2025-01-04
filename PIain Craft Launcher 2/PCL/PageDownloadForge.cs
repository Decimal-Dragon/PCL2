using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x0200017F RID: 383
	[DesignerGenerated]
	public class PageDownloadForge : MyPageRight, IComponentConnector
	{
		// Token: 0x06000EBC RID: 3772 RVA: 0x00009252 File Offset: 0x00007452
		public PageDownloadForge()
		{
			base.Initialized += delegate(object sender, EventArgs e)
			{
				this.LoaderInit();
			};
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.Init();
			};
			this.InitializeComponent();
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0006E928 File Offset: 0x0006CB28
		private void LoaderInit()
		{
			base.PageLoaderInit(this.Load, this.PanLoad, this.PanMain, this.CardTip, ModDownload.m_AttributeTests, delegate(ModLoader.LoaderBase a0)
			{
				this.Load_OnFinish();
			}, null, true);
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x00009285 File Offset: 0x00007485
		private void Init()
		{
			this.PanBack.ScrollToHome();
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0006E968 File Offset: 0x0006CB68
		private void Load_OnFinish()
		{
			try
			{
				List<string> list = ModDownload.m_AttributeTests.Output.Value.Sort(new ModBase.CompareThreadStart<string>(ModMinecraft.VersionSortBoolean));
				this.PanMain.Children.Clear();
				try
				{
					foreach (string text in list)
					{
						MyCard myCard = new MyCard();
						myCard.Title = text.Replace("_p", " P");
						myCard.Margin = new Thickness(0.0, 0.0, 0.0, 15.0);
						myCard.CreateParser(5);
						MyCard myCard2 = myCard;
						StackPanel stackPanel = new StackPanel
						{
							Margin = new Thickness(20.0, 40.0, 18.0, 0.0),
							VerticalAlignment = VerticalAlignment.Top,
							RenderTransform = new TranslateTransform(0.0, 0.0),
							Tag = text
						};
						myCard2.Children.Add(stackPanel);
						myCard2._Stub = stackPanel;
						myCard2.IsSwaped = true;
						this.PanMain.Children.Add(myCard2);
					}
				}
				finally
				{
					List<string>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "可视化 Forge 版本列表出错", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00009292 File Offset: 0x00007492
		public void Forge_Click(MyLoading sender, MouseButtonEventArgs e)
		{
			if (sender.State.LoadingState == MyLoading.MyLoadingState.Error)
			{
				((ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>)sender.State).Start(null, true);
			}
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0006EB10 File Offset: 0x0006CD10
		public void Forge_StateChanged(MyLoading sender, MyLoading.MyLoadingState newState, MyLoading.MyLoadingState oldState)
		{
			if (newState == MyLoading.MyLoadingState.Stop)
			{
				MyCard myCard = (MyCard)((FrameworkElement)sender.Parent).Parent;
				ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> loaderTask = (ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>)sender.State;
				NewLateBinding.LateCall(NewLateBinding.LateGet(myCard._Stub, null, "Children", new object[0], null, null, null), null, "Clear", new object[0], null, null, null, true);
				NewLateBinding.LateSet(myCard._Stub, null, "Tag", new object[]
				{
					loaderTask.Output
				}, null, null);
				myCard.CreateParser(6);
				myCard.StackInstall();
			}
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x000092B4 File Offset: 0x000074B4
		private void BtnWeb_Click(object sender, EventArgs e)
		{
			ModBase.OpenWebsite("https://files.minecraftforge.net");
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x000092C0 File Offset: 0x000074C0
		// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x000092C8 File Offset: 0x000074C8
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x000092D1 File Offset: 0x000074D1
		// (set) Token: 0x06000EC6 RID: 3782 RVA: 0x000092D9 File Offset: 0x000074D9
		internal virtual MyCard CardTip { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x000092E2 File Offset: 0x000074E2
		// (set) Token: 0x06000EC8 RID: 3784 RVA: 0x000092EA File Offset: 0x000074EA
		internal virtual TextBlock LabConnect { get; set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x000092F3 File Offset: 0x000074F3
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x0006EBA8 File Offset: 0x0006CDA8
		internal virtual MyButton BtnWeb
		{
			[CompilerGenerated]
			get
			{
				return this.m_ReponseTests;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnWeb_Click);
				MyButton reponseTests = this.m_ReponseTests;
				if (reponseTests != null)
				{
					reponseTests.Click -= value2;
				}
				this.m_ReponseTests = value;
				reponseTests = this.m_ReponseTests;
				if (reponseTests != null)
				{
					reponseTests.Click += value2;
				}
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x000092FB File Offset: 0x000074FB
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x00009303 File Offset: 0x00007503
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0000930C File Offset: 0x0000750C
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x00009314 File Offset: 0x00007514
		internal virtual MyCard PanLoad { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0000931D File Offset: 0x0000751D
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x00009325 File Offset: 0x00007525
		internal virtual MyLoading Load { get; set; }

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0006EBEC File Offset: 0x0006CDEC
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_ClassTests)
			{
				this.m_ClassTests = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagedownload/pagedownloadforge.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0006EC1C File Offset: 0x0006CE1C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (MyScrollViewer)target;
				return;
			}
			if (connectionId == 2)
			{
				this.CardTip = (MyCard)target;
				return;
			}
			if (connectionId == 3)
			{
				this.LabConnect = (TextBlock)target;
				return;
			}
			if (connectionId == 4)
			{
				this.BtnWeb = (MyButton)target;
				return;
			}
			if (connectionId == 5)
			{
				this.PanMain = (StackPanel)target;
				return;
			}
			if (connectionId == 6)
			{
				this.PanLoad = (MyCard)target;
				return;
			}
			if (connectionId == 7)
			{
				this.Load = (MyLoading)target;
				return;
			}
			this.m_ClassTests = true;
		}

		// Token: 0x04000812 RID: 2066
		[CompilerGenerated]
		[AccessedThroughProperty("PanBack")]
		private MyScrollViewer objectTests;

		// Token: 0x04000813 RID: 2067
		[AccessedThroughProperty("CardTip")]
		[CompilerGenerated]
		private MyCard _BridgeTests;

		// Token: 0x04000814 RID: 2068
		[AccessedThroughProperty("LabConnect")]
		[CompilerGenerated]
		private TextBlock _ItemTests;

		// Token: 0x04000815 RID: 2069
		[CompilerGenerated]
		[AccessedThroughProperty("BtnWeb")]
		private MyButton m_ReponseTests;

		// Token: 0x04000816 RID: 2070
		[AccessedThroughProperty("PanMain")]
		[CompilerGenerated]
		private StackPanel m_GlobalTests;

		// Token: 0x04000817 RID: 2071
		[CompilerGenerated]
		[AccessedThroughProperty("PanLoad")]
		private MyCard _ExceptionTests;

		// Token: 0x04000818 RID: 2072
		[AccessedThroughProperty("Load")]
		[CompilerGenerated]
		private MyLoading utilsTests;

		// Token: 0x04000819 RID: 2073
		private bool m_ClassTests;
	}
}
