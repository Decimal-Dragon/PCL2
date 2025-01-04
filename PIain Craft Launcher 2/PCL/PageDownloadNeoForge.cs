using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020000EC RID: 236
	[DesignerGenerated]
	public class PageDownloadNeoForge : MyPageRight, IComponentConnector
	{
		// Token: 0x0600088D RID: 2189 RVA: 0x00006680 File Offset: 0x00004880
		public PageDownloadNeoForge()
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

		// Token: 0x0600088E RID: 2190 RVA: 0x00045F80 File Offset: 0x00044180
		private void LoaderInit()
		{
			base.PageLoaderInit(this.Load, this.PanLoad, this.PanMain, this.CardTip, ModDownload.m_AnnotationTests, delegate(ModLoader.LoaderBase a0)
			{
				this.Load_OnFinish();
			}, null, true);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000066B3 File Offset: 0x000048B3
		private void Init()
		{
			this.PanBack.ScrollToHome();
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00045FC0 File Offset: 0x000441C0
		private void Load_OnFinish()
		{
			try
			{
				Dictionary<string, List<ModDownload.DlNeoForgeListEntry>> dictionary = Enumerable.ToDictionary<IGrouping<string, ModDownload.DlNeoForgeListEntry>, string, List<ModDownload.DlNeoForgeListEntry>>(Enumerable.OrderByDescending<IGrouping<string, ModDownload.DlNeoForgeListEntry>, string>(Enumerable.GroupBy<ModDownload.DlNeoForgeListEntry, string>(ModDownload.m_AnnotationTests.Output.Value, (PageDownloadNeoForge._Closure$__.$I3-0 == null) ? (PageDownloadNeoForge._Closure$__.$I3-0 = ((ModDownload.DlNeoForgeListEntry d) => d._TestsMap)) : PageDownloadNeoForge._Closure$__.$I3-0), (PageDownloadNeoForge._Closure$__.$I3-1 == null) ? (PageDownloadNeoForge._Closure$__.$I3-1 = ((IGrouping<string, ModDownload.DlNeoForgeListEntry> g) => g.Key)) : PageDownloadNeoForge._Closure$__.$I3-1), (PageDownloadNeoForge._Closure$__.$I3-2 == null) ? (PageDownloadNeoForge._Closure$__.$I3-2 = ((IGrouping<string, ModDownload.DlNeoForgeListEntry> g) => g.Key)) : PageDownloadNeoForge._Closure$__.$I3-2, (PageDownloadNeoForge._Closure$__.$I3-3 == null) ? (PageDownloadNeoForge._Closure$__.$I3-3 = ((IGrouping<string, ModDownload.DlNeoForgeListEntry> g) => Enumerable.ToList<ModDownload.DlNeoForgeListEntry>(g))) : PageDownloadNeoForge._Closure$__.$I3-3);
				this.PanMain.Children.Clear();
				try
				{
					foreach (KeyValuePair<string, List<ModDownload.DlNeoForgeListEntry>> keyValuePair in dictionary)
					{
						if (Enumerable.Any<ModDownload.DlNeoForgeListEntry>(keyValuePair.Value))
						{
							MyCard myCard = new MyCard();
							myCard.Title = keyValuePair.Key + " (" + Conversions.ToString(keyValuePair.Value.Count) + ")";
							myCard.Margin = new Thickness(0.0, 0.0, 0.0, 15.0);
							myCard.CreateParser(13);
							MyCard myCard2 = myCard;
							StackPanel stackPanel = new StackPanel
							{
								Margin = new Thickness(20.0, 40.0, 18.0, 0.0),
								VerticalAlignment = VerticalAlignment.Top,
								RenderTransform = new TranslateTransform(0.0, 0.0),
								Tag = keyValuePair.Value
							};
							myCard2.Children.Add(stackPanel);
							myCard2._Stub = stackPanel;
							myCard2.IsSwaped = true;
							this.PanMain.Children.Add(myCard2);
						}
					}
				}
				finally
				{
					Dictionary<string, List<ModDownload.DlNeoForgeListEntry>>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "可视化 NeoForge 版本列表出错", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x000066C0 File Offset: 0x000048C0
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x000066C8 File Offset: 0x000048C8
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x000066D1 File Offset: 0x000048D1
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x000066D9 File Offset: 0x000048D9
		internal virtual MyCard CardTip { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x000066E2 File Offset: 0x000048E2
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x000066EA File Offset: 0x000048EA
		internal virtual MyButton BtnWeb { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x000066F3 File Offset: 0x000048F3
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x000066FB File Offset: 0x000048FB
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00006704 File Offset: 0x00004904
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x0000670C File Offset: 0x0000490C
		internal virtual MyCard PanLoad { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00006715 File Offset: 0x00004915
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x0000671D File Offset: 0x0000491D
		internal virtual MyLoading Load { get; set; }

		// Token: 0x0600089D RID: 2205 RVA: 0x00046224 File Offset: 0x00044424
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_ExceptionReader)
			{
				this.m_ExceptionReader = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagedownload/pagedownloadneoforge.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00046254 File Offset: 0x00044454
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
				this.BtnWeb = (MyButton)target;
				return;
			}
			if (connectionId == 4)
			{
				this.PanMain = (StackPanel)target;
				return;
			}
			if (connectionId == 5)
			{
				this.PanLoad = (MyCard)target;
				return;
			}
			if (connectionId == 6)
			{
				this.Load = (MyLoading)target;
				return;
			}
			this.m_ExceptionReader = true;
		}

		// Token: 0x040004A2 RID: 1186
		[CompilerGenerated]
		[AccessedThroughProperty("PanBack")]
		private MyScrollViewer valueReader;

		// Token: 0x040004A3 RID: 1187
		[AccessedThroughProperty("CardTip")]
		[CompilerGenerated]
		private MyCard m_ObjectReader;

		// Token: 0x040004A4 RID: 1188
		[AccessedThroughProperty("BtnWeb")]
		[CompilerGenerated]
		private MyButton m_BridgeReader;

		// Token: 0x040004A5 RID: 1189
		[CompilerGenerated]
		[AccessedThroughProperty("PanMain")]
		private StackPanel itemReader;

		// Token: 0x040004A6 RID: 1190
		[CompilerGenerated]
		[AccessedThroughProperty("PanLoad")]
		private MyCard m_ReponseReader;

		// Token: 0x040004A7 RID: 1191
		[CompilerGenerated]
		[AccessedThroughProperty("Load")]
		private MyLoading m_GlobalReader;

		// Token: 0x040004A8 RID: 1192
		private bool m_ExceptionReader;
	}
}
