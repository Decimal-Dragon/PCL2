using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x0200019D RID: 413
	[DesignerGenerated]
	public class PageLaunchRight : MyPageRight, IRefreshable, IComponentConnector
	{
		// Token: 0x060010C0 RID: 4288 RVA: 0x0007AAD8 File Offset: 0x00078CD8
		public PageLaunchRight()
		{
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.Init();
			};
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.Refresh();
			};
			this.classMapper = RuntimeHelpers.GetObjectValue(new object());
			this._PolicyMapper = new ModLoader.LoaderTask<string, int>("自定义主页获取", new Action<ModLoader.LoaderTask<string, int>>(this.OnlineLoaderSub), null, ThreadPriority.Normal)
			{
				ReloadTimeout = 600000
			};
			this.m_OrderMapper = -1;
			this._ProducerMapper = RuntimeHelpers.GetObjectValue(new object());
			this.InitializeComponent();
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0000A3B2 File Offset: 0x000085B2
		private void Init()
		{
			this.PanBack.ScrollToHome();
			base.PanScroll = this.PanBack;
			this.PanLog.Visibility = (ModBase._TokenRepository ? Visibility.Visible : Visibility.Collapsed);
			this.PanHint.Visibility = Visibility.Collapsed;
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0000A3ED File Offset: 0x000085ED
		private void Refresh()
		{
			ModBase.RunInNewThread(delegate
			{
				try
				{
					object obj = this.classMapper;
					ObjectFlowControl.CheckForSyncLockOnValueType(obj);
					lock (obj)
					{
						this.RefreshReal();
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "加载 PCL 主页自定义信息失败", ModBase.LogLevel.Msgbox, "出现错误");
				}
			}, string.Format("刷新自定义主页 #{0}", ModBase.GetUuid()), ThreadPriority.Normal);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0007AB68 File Offset: 0x00078D68
		private void RefreshReal()
		{
			PageLaunchRight._Closure$__3-0 CS$<>8__locals1 = new PageLaunchRight._Closure$__3-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Me = this;
			CS$<>8__locals1.$VB$Local_Content = "";
			object left = ModBase.m_IdentifierRepository.Get("UiCustomType", null);
			if (Operators.ConditionalCompareObjectEqual(left, 1, false))
			{
				ModBase.Log("[Page] 主页自定义数据来源：本地文件", ModBase.LogLevel.Normal, "出现错误");
				CS$<>8__locals1.$VB$Local_Content = ModBase.ReadFile(ModBase.Path + "PCL\\Custom.xaml", null);
			}
			else
			{
				string text;
				if (Operators.ConditionalCompareObjectEqual(left, 2, false))
				{
					text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("UiCustomNet", null));
				}
				else
				{
					if (!Operators.ConditionalCompareObjectEqual(left, 3, false))
					{
						goto IL_375;
					}
					object left2 = ModBase.m_IdentifierRepository.Get("UiCustomPreset", null);
					if (Operators.ConditionalCompareObjectEqual(left2, 0, false))
					{
						ModBase.Log("[Page] 主页预设：你知道吗", ModBase.LogLevel.Normal, "出现错误");
						CS$<>8__locals1.$VB$Local_Content = "\r\n                            <local:MyCard Title=\"你知道吗？\" Margin=\"0,0,0,15\">\r\n                                <TextBlock Margin=\"25,38,23,15\" FontSize=\"13.5\" IsHitTestVisible=\"False\" Text=\"{hint}\" TextWrapping=\"Wrap\" Foreground=\"{DynamicResource ColorBrush1}\" />\r\n                                <local:MyIconButton Height=\"22\" Width=\"22\" Margin=\"9\" VerticalAlignment=\"Top\" HorizontalAlignment=\"Right\" \r\n                                    EventType=\"刷新主页\" EventData=\"/\"\r\n                                    Logo=\"M875.52 148.48C783.36 56.32 655.36 0 512 0 291.84 0 107.52 138.24 30.72 332.8l122.88 46.08C204.8 230.4 348.16 128 512 128c107.52 0 199.68 40.96 271.36 112.64L640 384h384V0L875.52 148.48zM512 896c-107.52 0-199.68-40.96-271.36-112.64L384 640H0v384l148.48-148.48C240.64 967.68 368.64 1024 512 1024c220.16 0 404.48-138.24 481.28-332.8L870.4 645.12C819.2 793.6 675.84 896 512 896z\" />\r\n                            </local:MyCard>";
						goto IL_375;
					}
					if (Operators.ConditionalCompareObjectEqual(left2, 1, false))
					{
						ModBase.Log("[Page] 主页预设：回声洞", ModBase.LogLevel.Normal, "出现错误");
						CS$<>8__locals1.$VB$Local_Content = "\r\n                            <local:MyCard Title=\"回声洞\" Margin=\"0,0,0,15\">\r\n                                <TextBlock Margin=\"25,38,23,15\" FontSize=\"13.5\" IsHitTestVisible=\"False\" Text=\"{cave}\" TextWrapping=\"Wrap\" Foreground=\"{DynamicResource ColorBrush1}\" />\r\n                                <local:MyIconButton Height=\"22\" Width=\"22\" Margin=\"9\" VerticalAlignment=\"Top\" HorizontalAlignment=\"Right\" \r\n                                    EventType=\"刷新主页\" EventData=\"/\"\r\n                                    Logo=\"M875.52 148.48C783.36 56.32 655.36 0 512 0 291.84 0 107.52 138.24 30.72 332.8l122.88 46.08C204.8 230.4 348.16 128 512 128c107.52 0 199.68 40.96 271.36 112.64L640 384h384V0L875.52 148.48zM512 896c-107.52 0-199.68-40.96-271.36-112.64L384 640H0v384l148.48-148.48C240.64 967.68 368.64 1024 512 1024c220.16 0 404.48-138.24 481.28-332.8L870.4 645.12C819.2 793.6 675.84 896 512 896z\" />\r\n                            </local:MyCard>";
						goto IL_375;
					}
					if (Operators.ConditionalCompareObjectEqual(left2, 2, false))
					{
						ModBase.Log("[Page] 主页预设：Minecraft 新闻", ModBase.LogLevel.Normal, "出现错误");
						text = "http://pcl.mcnews.thestack.top";
					}
					else if (Operators.ConditionalCompareObjectEqual(left2, 3, false))
					{
						ModBase.Log("[Page] 主页预设：简单主页", ModBase.LogLevel.Normal, "出现错误");
						text = "https://raw.gitcode.com/mfn233/PCL-Mainpage/raw/main/Custom.xaml";
					}
					else if (Operators.ConditionalCompareObjectEqual(left2, 4, false))
					{
						ModBase.Log("[Page] 主页预设：每日整合包推荐", ModBase.LogLevel.Normal, "出现错误");
						text = "https://pclsub.sodamc.com/";
					}
					else if (Operators.ConditionalCompareObjectEqual(left2, 5, false))
					{
						ModBase.Log("[Page] 主页预设：Minecraft 皮肤推荐", ModBase.LogLevel.Normal, "出现错误");
						text = "https://forgepixel.com/pcl_sub_file";
					}
					else if (Operators.ConditionalCompareObjectEqual(left2, 6, false))
					{
						ModBase.Log("[Page] 主页预设：OpenBMCLAPI 仪表盘 Lite", ModBase.LogLevel.Normal, "出现错误");
						text = "https://pcl-bmcl.milu.ink/";
					}
					else if (Operators.ConditionalCompareObjectEqual(left2, 7, false))
					{
						ModBase.Log("[Page] 主页预设：主页市场", ModBase.LogLevel.Normal, "出现错误");
						text = "https://homepage-market.pages.dev/Custom.xaml";
					}
					else if (Operators.ConditionalCompareObjectEqual(left2, 8, false))
					{
						ModBase.Log("[Page] 主页预设：更新日志", ModBase.LogLevel.Normal, "出现错误");
						text = "https://updatehomepage.pages.dev/UpdateHomepage.xaml";
					}
					else if (Operators.ConditionalCompareObjectEqual(left2, 9, false))
					{
						ModBase.Log("[Page] 主页预设：PCL 新功能说明书", ModBase.LogLevel.Normal, "出现错误");
						text = "https://raw.gitcode.com/WForst-Breeze/WhatsNewPCL/raw/main/Custom.xaml";
					}
					else if (Operators.ConditionalCompareObjectEqual(left2, 10, false))
					{
						ModBase.Log("[Page] 主页预设：OpenMCIM Dashboard", ModBase.LogLevel.Normal, "出现错误");
						text = "https://files.mcimirror.top/PCL";
					}
					else
					{
						if (!Operators.ConditionalCompareObjectEqual(left2, 11, false))
						{
							goto IL_375;
						}
						ModBase.Log("[Page] 主页预设：杂志主页", ModBase.LogLevel.Normal, "出现错误");
						text = "http://pclhomeplazaoss.lingyunawa.top:26994/d/Homepages/Ext1nguisher/Custom.xaml";
					}
				}
				if (!string.IsNullOrWhiteSpace(text))
				{
					if (!Operators.ConditionalCompareObjectEqual(text, ModBase.m_IdentifierRepository.Get("CacheSavedPageUrl", null), false) || !File.Exists(ModBase.m_DecoratorRepository + "Cache\\Custom.xaml"))
					{
						ModBase.Log("[Page] 主页自定义数据来源：联网全新下载", ModBase.LogLevel.Normal, "出现错误");
						ModMain.Hint("正在加载主页……", ModMain.HintType.Info, true);
						ModBase.RunInUiWait(delegate()
						{
							this.LoadContent("");
						});
						ModBase.m_IdentifierRepository.Set("CacheSavedPageVersion", "", false, null);
						this._PolicyMapper.Start(text, false);
						return;
					}
					ModBase.Log("[Page] 主页自定义数据来源：联网缓存文件", ModBase.LogLevel.Normal, "出现错误");
					CS$<>8__locals1.$VB$Local_Content = ModBase.ReadFile(ModBase.m_DecoratorRepository + "Cache\\Custom.xaml", null);
					this._PolicyMapper.Start(text, false);
				}
			}
			IL_375:
			ModBase.RunInUi(delegate()
			{
				CS$<>8__locals1.$VB$Me.LoadContent(CS$<>8__locals1.$VB$Local_Content);
			}, false);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0007AEFC File Offset: 0x000790FC
		private void OnlineLoaderSub(ModLoader.LoaderTask<string, int> Task)
		{
			string input = Task.Input;
			try
			{
				string text;
				if (input.Contains(".xaml"))
				{
					text = input.Replace(".xaml", ".xaml.ini");
				}
				else
				{
					text = input.BeforeFirst("?", false);
					if (!text.EndsWith("/"))
					{
						text += "/";
					}
					text += "version";
					if (input.Contains("?"))
					{
						text += input.AfterLast("?", false);
					}
				}
				string text2 = "";
				bool flag = true;
				try
				{
					text2 = Conversions.ToString(ModNet.NetGetCodeByRequestOnce(text, null, 10000, false, "", false));
					if (text2.Length > 1000)
					{
						throw new Exception(string.Format("获取的自定义主页版本过长（{0} 字符）", text2.Length));
					}
					string text3 = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheSavedPageVersion", null));
					if (Operators.CompareString(text2, "", false) != 0 && Operators.CompareString(text3, "", false) != 0 && Operators.CompareString(text2, text3, false) == 0)
					{
						ModBase.Log(string.Format("[Page] 当前缓存的自定义主页已为最新，当前版本：{0}，检查源：{1}", text2, text), ModBase.LogLevel.Normal, "出现错误");
						flag = false;
					}
					else
					{
						ModBase.Log(string.Format("[Page] 需要下载联网自定义主页，当前版本：{0}，检查源：{1}", text2, text), ModBase.LogLevel.Normal, "出现错误");
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "联网获取自定义主页版本失败", ModBase.LogLevel.Developer, "出现错误");
					ModBase.Log(string.Format("[Page] 无法检查联网自定义主页版本，将直接下载，检查源：{0}", text), ModBase.LogLevel.Normal, "出现错误");
				}
				if (flag)
				{
					string text4 = Conversions.ToString(ModNet.NetGetCodeByRequestRetry(input, null, "", false, null, false));
					ModBase.Log(string.Format("[Page] 已联网下载自定义主页，内容长度：{0}，来源：{1}", text4.Length, input), ModBase.LogLevel.Normal, "出现错误");
					ModBase.m_IdentifierRepository.Set("CacheSavedPageUrl", input, false, null);
					ModBase.m_IdentifierRepository.Set("CacheSavedPageVersion", text2, false, null);
					ModBase.WriteFile(ModBase.m_DecoratorRepository + "Cache\\Custom.xaml", text4, false, null);
				}
				this.Refresh();
			}
			catch (Exception ex2)
			{
				if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("CacheSavedPageVersion", null), "", false))
				{
					ModBase.Log(ex2, string.Format("联网下载自定义主页失败（{0}）", input), ModBase.LogLevel.Msgbox, "出现错误");
				}
				else
				{
					ModBase.Log(ex2, string.Format("联网下载自定义主页失败（{0}）", input), ModBase.LogLevel.Debug, "出现错误");
				}
			}
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0007B188 File Offset: 0x00079388
		public void ForceRefresh()
		{
			ModBase.Log("[Page] 要求强制刷新自定义主页", ModBase.LogLevel.Normal, "出现错误");
			this.ClearCache();
			if (ModMain._ProcessIterator._MethodIterator.initializerMap == FormMain.PageType.Launch)
			{
				this.PanBack.ScrollToHome();
				this.Refresh();
				return;
			}
			ModMain._ProcessIterator.PageChange(FormMain.PageType.Launch, FormMain.PageSubType.Default);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0007B1E0 File Offset: 0x000793E0
		private void ClearCache()
		{
			this.m_OrderMapper = -1;
			this._PolicyMapper.Input = "";
			ModBase.m_IdentifierRepository.Set("CacheSavedPageUrl", "", false, null);
			ModBase.m_IdentifierRepository.Set("CacheSavedPageVersion", "", false, null);
			ModBase.Log("[Page] 已清空自定义主页缓存", ModBase.LogLevel.Normal, "出现错误");
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0007B240 File Offset: 0x00079440
		private void LoadContent(string Content)
		{
			object producerMapper = this._ProducerMapper;
			ObjectFlowControl.CheckForSyncLockOnValueType(producerMapper);
			lock (producerMapper)
			{
				int hashCode = Content.GetHashCode();
				if (hashCode == this.m_OrderMapper)
				{
					return;
				}
				this.m_OrderMapper = hashCode;
				this.PanCustom.Children.Clear();
				if (string.IsNullOrWhiteSpace(Content))
				{
					ModBase.Log("[Page] 实例化：清空自定义主页 UI，来源为空", ModBase.LogLevel.Normal, "出现错误");
					return;
				}
				Content = "<StackPanel xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:local=\"clr-namespace:PCL;assembly=Plain Craft Launcher 2\">" + Content + "</StackPanel>";
				Content = ModMain.HelpArgumentReplace(Content);
				ModBase.Log(string.Format("[Page] 实例化：加载自定义主页 UI 开始，最终内容长度：{0}", Enumerable.Count<char>(Content)), ModBase.LogLevel.Normal, "出现错误");
				try
				{
					this.PanCustom.Children.Add((UIElement)ModBase.GetObjectFromXML(Content));
				}
				catch (Exception ex)
				{
					ModBase.Log("[Page] 加载失败的自定义主页内容：\r\n" + Content, ModBase.LogLevel.Normal, "出现错误");
					if (ModMain.MyMsgBox(string.Format("自定义主页内容编写有误，请根据下列错误信息进行检查：{0}{1}", "\r\n", ex.Message), "加载自定义主页失败", "重试", "取消", "", false, true, false, null, null, null) == 1)
					{
						goto IL_136;
					}
				}
				ModBase.Log("[Page] 实例化：加载自定义主页 UI 完成", ModBase.LogLevel.Normal, "出现错误");
				return;
			}
			IL_136:
			this.ForceRefresh();
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x0000A416 File Offset: 0x00008616
		// (set) Token: 0x060010C9 RID: 4297 RVA: 0x0000A41E File Offset: 0x0000861E
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x0000A427 File Offset: 0x00008627
		// (set) Token: 0x060010CB RID: 4299 RVA: 0x0000A42F File Offset: 0x0000862F
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x0000A438 File Offset: 0x00008638
		// (set) Token: 0x060010CD RID: 4301 RVA: 0x0000A440 File Offset: 0x00008640
		internal virtual StackPanel PanCustom { get; set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x0000A449 File Offset: 0x00008649
		// (set) Token: 0x060010CF RID: 4303 RVA: 0x0000A451 File Offset: 0x00008651
		internal virtual MyCard PanHint { get; set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x0000A45A File Offset: 0x0000865A
		// (set) Token: 0x060010D1 RID: 4305 RVA: 0x0000A462 File Offset: 0x00008662
		internal virtual MyIconButton BtnHintClose { get; set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x0000A46B File Offset: 0x0000866B
		// (set) Token: 0x060010D3 RID: 4307 RVA: 0x0000A473 File Offset: 0x00008673
		internal virtual TextBlock LabHint1 { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x0000A47C File Offset: 0x0000867C
		// (set) Token: 0x060010D5 RID: 4309 RVA: 0x0000A484 File Offset: 0x00008684
		internal virtual TextBlock LabHint2 { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x0000A48D File Offset: 0x0000868D
		// (set) Token: 0x060010D7 RID: 4311 RVA: 0x0000A495 File Offset: 0x00008695
		internal virtual MyCard PanLog { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x0000A49E File Offset: 0x0000869E
		// (set) Token: 0x060010D9 RID: 4313 RVA: 0x0000A4A6 File Offset: 0x000086A6
		internal virtual TextBlock LabLog { get; set; }

		// Token: 0x060010DA RID: 4314 RVA: 0x0007B3C0 File Offset: 0x000795C0
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._ReaderThread)
			{
				this._ReaderThread = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/pagelaunchright.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0007B3F0 File Offset: 0x000795F0
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
				this.PanMain = (StackPanel)target;
				return;
			}
			if (connectionId == 3)
			{
				this.PanCustom = (StackPanel)target;
				return;
			}
			if (connectionId == 4)
			{
				this.PanHint = (MyCard)target;
				return;
			}
			if (connectionId == 5)
			{
				this.BtnHintClose = (MyIconButton)target;
				return;
			}
			if (connectionId == 6)
			{
				this.LabHint1 = (TextBlock)target;
				return;
			}
			if (connectionId == 7)
			{
				this.LabHint2 = (TextBlock)target;
				return;
			}
			if (connectionId == 8)
			{
				this.PanLog = (MyCard)target;
				return;
			}
			if (connectionId == 9)
			{
				this.LabLog = (TextBlock)target;
				return;
			}
			this._ReaderThread = true;
		}

		// Token: 0x040008DB RID: 2267
		private object classMapper;

		// Token: 0x040008DC RID: 2268
		private ModLoader.LoaderTask<string, int> _PolicyMapper;

		// Token: 0x040008DD RID: 2269
		private int m_OrderMapper;

		// Token: 0x040008DE RID: 2270
		private object _ProducerMapper;

		// Token: 0x040008DF RID: 2271
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyScrollViewer m_SchemaMapper;

		// Token: 0x040008E0 RID: 2272
		[AccessedThroughProperty("PanMain")]
		[CompilerGenerated]
		private StackPanel _DescriptorMapper;

		// Token: 0x040008E1 RID: 2273
		[AccessedThroughProperty("PanCustom")]
		[CompilerGenerated]
		private StackPanel publisherMapper;

		// Token: 0x040008E2 RID: 2274
		[AccessedThroughProperty("PanHint")]
		[CompilerGenerated]
		private MyCard definitionMapper;

		// Token: 0x040008E3 RID: 2275
		[AccessedThroughProperty("BtnHintClose")]
		[CompilerGenerated]
		private MyIconButton strategyMapper;

		// Token: 0x040008E4 RID: 2276
		[CompilerGenerated]
		[AccessedThroughProperty("LabHint1")]
		private TextBlock m_ProcMapper;

		// Token: 0x040008E5 RID: 2277
		[CompilerGenerated]
		[AccessedThroughProperty("LabHint2")]
		private TextBlock m_ParserThread;

		// Token: 0x040008E6 RID: 2278
		[CompilerGenerated]
		[AccessedThroughProperty("PanLog")]
		private MyCard _BroadcasterThread;

		// Token: 0x040008E7 RID: 2279
		[CompilerGenerated]
		[AccessedThroughProperty("LabLog")]
		private TextBlock m_FieldThread;

		// Token: 0x040008E8 RID: 2280
		private bool _ReaderThread;
	}
}
