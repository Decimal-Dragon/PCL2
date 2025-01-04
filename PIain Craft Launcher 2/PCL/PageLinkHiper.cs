using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020000FD RID: 253
	[DesignerGenerated]
	public class PageLinkHiper : MyPageRight, IComponentConnector
	{
		// Token: 0x060009C1 RID: 2497 RVA: 0x00049A38 File Offset: 0x00047C38
		static PageLinkHiper()
		{
			PageLinkHiper.RemoveReader(new ModLoader.LoaderCombo<int>("HiPer 初始化", new ModLoader.LoaderBase[]
			{
				new ModLoader.LoaderTask<int, int>("网络环境：连通检测", new Action<ModLoader.LoaderTask<int, int>>(PageLinkHiper.InitPingCheck), null, ThreadPriority.Normal)
				{
					Block = false,
					ProgressWeight = 0.5
				},
				new ModLoader.LoaderTask<int, int>("网络环境：IP 检测", delegate(ModLoader.LoaderTask<int, int> a0)
				{
					PageLinkHiper.InitIpCheck();
				}, null, ThreadPriority.Normal)
				{
					Block = false,
					ProgressWeight = 1.0
				},
				new ModLoader.LoaderTask<int, int>("检查网络环境", new Action<ModLoader.LoaderTask<int, int>>(PageLinkHiper.InitCheck), null, ThreadPriority.Normal)
				{
					ProgressWeight = 0.5
				},
				new ModLoader.LoaderTask<int, List<ModNet.NetFile>>("获取所需文件", new Action<ModLoader.LoaderTask<int, List<ModNet.NetFile>>>(PageLinkHiper.InitGetFile), null, ThreadPriority.Normal)
				{
					ProgressWeight = 4.0
				},
				new ModNet.LoaderDownload("下载所需文件", new List<ModNet.NetFile>())
				{
					ProgressWeight = 4.0
				},
				new ModLoader.LoaderTask<int, int>("启动联机模块", new Action<ModLoader.LoaderTask<int, int>>(PageLinkHiper.InitLaunch), null, ThreadPriority.Normal)
				{
					ProgressWeight = 7.0
				}
			}));
			PageLinkHiper.annotationClient = ModBase.LoadState.Loading;
			PageLinkHiper.m_ListClient = ModBase.LoadState.Waiting;
			PageLinkHiper._MerchantClient = null;
			PageLinkHiper._AuthenticationClient = -1;
			PageLinkHiper.m_AlgoClient = -1;
			PageLinkHiper.comparatorClient = DateTime.Now;
			PageLinkHiper._MappingClient = null;
			PageLinkHiper.m_FilterClient = -1;
			PageLinkHiper._DatabaseClient = "准备初始化";
			PageLinkHiper.m_PredicateClient = new ModLoader.LoaderTask<bool, int>("HiPer Ping Host", delegate(ModLoader.LoaderTask<bool, int> Task)
			{
				PageLinkHiper.m_FilterClient = -1;
				PageLinkHiper.m_FilterClient = ModNet.Ping(PageLinkHiper._ModelClient, 5000, Task.Input);
			}, null, ThreadPriority.Normal);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00049BD8 File Offset: 0x00047DD8
		public PageLinkHiper()
		{
			base.Initialized += delegate(object sender, EventArgs e)
			{
				this.LoaderInit();
			};
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.OnLoaded();
			};
			base.IncludeBroadcaster(new MyPageRight.PageEnterEventHandler(this.PageLinkHiper_OnPageEnter));
			this.m_BaseClient = false;
			this.tokenizerClient = false;
			this.poolClient = PageLinkHiper.Subpages.PanCert;
			this.InitializeComponent();
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00049C40 File Offset: 0x00047E40
		private void LoaderInit()
		{
			base.PageLoaderInit(this.Load, this.PanLoad, this.PanContent, this.PanAlways, PageLinkHiper._CodeClient, null, null, false);
			PageLinkHiper._CodeClient.OnStateChangedUi += this.OnLoadStateChanged;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00049C8C File Offset: 0x00047E8C
		private void OnLoaded()
		{
			FormMain.EndProgramForce(ModBase.Result.Aborted);
			if (!this.m_BaseClient)
			{
				this.m_BaseClient = true;
				if (!this.tokenizerClient)
				{
					ModBase.RunInNewThread(new Action(this.WatcherThread), "Hiper Watcher", ThreadPriority.Normal);
				}
				try
				{
					string text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LinkHiperCertTime", null));
					if (Operators.CompareString(text, "", false) == 0)
					{
						ModBase.Log("[HiPer] 没有缓存凭证", ModBase.LogLevel.Normal, "出现错误");
					}
					else if (DateTime.Compare(DateTime.Parse(text), DateTime.Now) > 0)
					{
						this.TextCert.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LinkHiperCertLast", null));
						ModBase.Log("[HiPer] 缓存凭证尚未过期：" + text, ModBase.LogLevel.Normal, "出现错误");
						this.ExcludeReader(PageLinkHiper.Subpages.PanSelect);
					}
					else
					{
						ModBase.Log("[HiPer] 缓存凭证已过期：" + text, ModBase.LogLevel.Normal, "出现错误");
						this.LabCertTitle.Text = "输入索引码";
						this.LabCertDesc.Text = "你的 HiPer 索引码已经过期，请输入新的索引码。\r\n如果实在没有索引码，可以在左侧选择 IOI 方式联机。";
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "读取缓存凭证失败", ModBase.LogLevel.Debug, "出现错误");
					ModBase.m_IdentifierRepository.Set("LinkHiperCertTime", "", false, null);
				}
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00006FA1 File Offset: 0x000051A1
		[CompilerGenerated]
		public static ModLoader.LoaderCombo<int> PatchReader()
		{
			return PageLinkHiper._CodeClient;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00006FA8 File Offset: 0x000051A8
		[CompilerGenerated]
		public static void RemoveReader(ModLoader.LoaderCombo<int> WithEventsValue)
		{
			PageLinkHiper._CodeClient = WithEventsValue;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x000063A1 File Offset: 0x000045A1
		private static void InitPingCheck(ModLoader.LoaderTask<int, int> Task)
		{
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x000063A1 File Offset: 0x000045A1
		private static void InitIpCheck()
		{
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x000063A1 File Offset: 0x000045A1
		private static void InitCheck(ModLoader.LoaderTask<int, int> Task)
		{
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x000063A1 File Offset: 0x000045A1
		private static void InitGetFile(ModLoader.LoaderTask<int, List<ModNet.NetFile>> Task)
		{
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x000063A1 File Offset: 0x000045A1
		private static void InitLaunch(ModLoader.LoaderTask<int, int> Task)
		{
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00006FB0 File Offset: 0x000051B0
		public static ModBase.LoadState EnableReader()
		{
			return PageLinkHiper.m_ListClient;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00006FB7 File Offset: 0x000051B7
		public static void CollectReader(ModBase.LoadState value)
		{
			PageLinkHiper.m_ListClient = value;
			ModBase.RunInUi((PageLinkHiper._Closure$__.$I28-0 == null) ? (PageLinkHiper._Closure$__.$I28-0 = delegate()
			{
				if (ModMain.m_InitializerIterator != null)
				{
					Enumerable.ElementAtOrDefault<MyIconButton>(ModMain.m_InitializerIterator.ItemHiper.Buttons, 0).Visibility = ((PageLinkHiper.EnableReader() == ModBase.LoadState.Finished || PageLinkHiper.EnableReader() == ModBase.LoadState.Loading) ? Visibility.Visible : Visibility.Collapsed);
				}
			}) : PageLinkHiper._Closure$__.$I28-0, false);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00006FE9 File Offset: 0x000051E9
		public static void ModuleStopManually()
		{
			PageLinkHiper.HiperExit(false);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0000507A File Offset: 0x0000327A
		public static bool HiperStop(bool SleepWhenKilled)
		{
			return false;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x000063A1 File Offset: 0x000045A1
		public static void HiperStart(ModLoader.LoaderTask<int, int> Task)
		{
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x000063A1 File Offset: 0x000045A1
		private static void HiperLogLine(string Content, ModLoader.LoaderTask<int, int> Task)
		{
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00049DDC File Offset: 0x00047FDC
		private void WatcherThread()
		{
			int num = 0;
			checked
			{
				for (;;)
				{
					try
					{
						int num2 = 1;
						do
						{
							Thread.Sleep(200);
							if (PageLinkHiper._CodeClient.State == ModBase.LoadState.Loading)
							{
								ModBase.RunInUi(delegate()
								{
									this.UpdateProgress(-1.0);
								}, false);
							}
							num2++;
						}
						while (num2 <= 5);
						Thread.Sleep(1000);
						num++;
						this.WatcherTimer1();
						if (num == 15)
						{
							num = 0;
							this.WatcherTimer15();
						}
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "联机模块主时钟出错", ModBase.LogLevel.Feedback, "出现错误");
						Thread.Sleep(20000);
					}
				}
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00006FF1 File Offset: 0x000051F1
		private void WatcherTimer1()
		{
			if (PageLinkHiper.m_ListClient == ModBase.LoadState.Finished)
			{
				ModBase.RunInUi(delegate()
				{
					TimeSpan timeSpan = PageLinkHiper.comparatorClient - DateTime.Now;
					if (timeSpan.TotalDays >= 30.0)
					{
						this.LabFinishTime.Text = "> 30 天";
					}
					else if (timeSpan.TotalDays >= 4.0)
					{
						this.LabFinishTime.Text = Conversions.ToString(timeSpan.Days) + " 天";
					}
					else if (timeSpan.TotalDays >= 1.0)
					{
						this.LabFinishTime.Text = Conversions.ToString(timeSpan.Days) + " 天" + ((timeSpan.Hours > 0) ? (" " + Conversions.ToString(timeSpan.Hours) + " 小时") : "");
					}
					else if (timeSpan.TotalMinutes >= 10.0)
					{
						this.LabFinishTime.Text = Conversions.ToString(timeSpan.Hours) + ":" + timeSpan.Minutes.ToString().PadLeft(2, '0') + "'";
					}
					else
					{
						this.LabFinishTime.Text = Conversions.ToString(timeSpan.Minutes) + "'" + timeSpan.Seconds.ToString().PadLeft(2, '0') + "\"";
					}
					if (Conversions.ToBoolean(timeSpan.TotalSeconds <= 300.0 && timeSpan.TotalSeconds > 299.0 && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LinkHiperCertWarn", null))))
					{
						ModMain.MyMsgBox("你的索引码还有不到 5 分钟就要过期了！\r\n你可以在设置中关闭这个提示……", "索引码即将过期", "我知道了……", "", "", false, true, false, null, null, null);
						ModMain.ShowWindowToTop(ModBase.m_IndexerRepository);
						Interaction.Beep();
					}
					if (timeSpan.TotalSeconds < 2.0)
					{
						this.LabCertTitle.Text = "索引码已过期";
						this.LabCertDesc.Text = "你的 HiPer 索引码已经过期，请输入新的索引码。\r\n如果实在没有索引码，可以在左侧选择 IOI 方式联机。";
						this.TextCert.Text = "";
						PageLinkHiper.HiperExit(true);
						ModMain.ShowWindowToTop(ModBase.m_IndexerRepository);
						Interaction.Beep();
						return;
					}
					int num = checked((int)Math.Round(unchecked((double)(PageLinkHiper.infoClient ? 0 : -2) - Math.Ceiling((double)(checked(Math.Min(PageLinkHiper._PrototypeClient, 600) + Math.Min(PageLinkHiper._AdapterClient, 600))) / 80.0))));
					if (num >= -1)
					{
						this.LabFinishQuality.Text = "优秀";
					}
					else if (num >= -2)
					{
						this.LabFinishQuality.Text = "优良";
					}
					else if (num >= -3)
					{
						this.LabFinishQuality.Text = "良好";
					}
					else if (num >= -5)
					{
						this.LabFinishQuality.Text = "一般";
					}
					else if (num >= -7)
					{
						this.LabFinishQuality.Text = "较差";
					}
					else
					{
						this.LabFinishQuality.Text = "很差";
					}
					if (PageLinkHiper.m_FilterClient != -1 && ModMain.m_RegIterator != null && ModMain.m_RegIterator.LabFinishPing.IsLoaded)
					{
						ModMain.m_RegIterator.LabFinishPing.Text = Conversions.ToString(PageLinkHiper.m_FilterClient) + "ms";
					}
				}, false);
			}
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x000063A1 File Offset: 0x000045A1
		private void WatcherTimer15()
		{
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0000700D File Offset: 0x0000520D
		private void TextCert_ValidateChanged(object sender, EventArgs e)
		{
			this.BtnCertDone.IsEnabled = (Operators.CompareString(this.TextCert.ValidateResult, "", false) == 0);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00007033 File Offset: 0x00005233
		private void TextCert_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && this.BtnCertDone.IsEnabled)
			{
				this.BtnCertDone_Click();
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00007051 File Offset: 0x00005251
		private void BtnCertDone_Click()
		{
			this.ExcludeReader(PageLinkHiper.Subpages.PanSelect);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0000705A File Offset: 0x0000525A
		private void BtnSelectReturn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			this.LabCertTitle.Text = "输入索引码";
			this.LabCertDesc.Text = "你需要获取索引码才能使用 HiPer。\r\n如果实在没有索引码，可以在左侧选择 IOI 方式联机。";
			this.ExcludeReader(PageLinkHiper.Subpages.PanCert);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000063A1 File Offset: 0x000045A1
		private void BtnSelectCreate_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00007083 File Offset: 0x00005283
		private void RoomCreate(int Port)
		{
			PageLinkHiper._ModelClient = null;
			PageLinkHiper.m_WrapperClient = Port;
			PageLinkHiper.m_ManagerClient = true;
			PageLinkHiper._CodeClient.Start(null, true);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x000063A1 File Offset: 0x000045A1
		private void BtnSelectJoin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x000070A3 File Offset: 0x000052A3
		private void RoomJoin(string Ip, int Port)
		{
			PageLinkHiper._ModelClient = Ip;
			PageLinkHiper.m_WrapperClient = Port;
			PageLinkHiper.m_ManagerClient = false;
			PageLinkHiper._CodeClient.Start(null, true);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x000063A1 File Offset: 0x000045A1
		private void OnLoadStateChanged(ModLoader.LoaderBase Loader, ModBase.LoadState NewState, ModBase.LoadState OldState)
		{
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00049E90 File Offset: 0x00048090
		private static void SetLoadDesc(string Intro, string Step)
		{
			ModBase.Log("[Hiper] 连接步骤：" + Intro, ModBase.LogLevel.Normal, "出现错误");
			PageLinkHiper._DatabaseClient = Step;
			ModBase.RunInUiWait(delegate()
			{
				if (ModMain.m_RegIterator != null && ModMain.m_RegIterator.LabLoadDesc.IsLoaded)
				{
					ModMain.m_RegIterator.LabLoadDesc.Text = Intro;
					ModMain.m_RegIterator.UpdateProgress(-1.0);
				}
			});
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x000070C3 File Offset: 0x000052C3
		private void CardLoad_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (PageLinkHiper._CodeClient.State == ModBase.LoadState.Failed)
			{
				PageLinkHiper._CodeClient.Start(null, true);
			}
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000070DE File Offset: 0x000052DE
		private void CancelLoad()
		{
			if (PageLinkHiper._CodeClient.State == ModBase.LoadState.Loading)
			{
				this.ExcludeReader(PageLinkHiper.Subpages.PanSelect);
				PageLinkHiper._CodeClient.Abort();
			}
			else
			{
				PageLinkHiper._CodeClient.State = ModBase.LoadState.Waiting;
			}
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00049EDC File Offset: 0x000480DC
		private void UpdateProgress(double Value = -1.0)
		{
			if (Value == -1.0)
			{
				Value = PageLinkHiper._CodeClient.Progress;
			}
			double value = this.ColumnProgressA.Width.Value;
			if (Math.Round(Value - value, 3) != 0.0)
			{
				if (value > Value)
				{
					this.ColumnProgressA.Width = new GridLength(Value, GridUnitType.Star);
					this.ColumnProgressB.Width = new GridLength(1.0 - Value, GridUnitType.Star);
					ModAnimation.AniStop("Hiper Progress");
					return;
				}
				double num = (Value == 1.0) ? 1.0 : ((Value - value) * 0.2 + value);
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaGridLengthWidth(this.ColumnProgressA, num - this.ColumnProgressA.Width.Value, 300, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
					ModAnimation.AaGridLengthWidth(this.ColumnProgressB, 1.0 - num - this.ColumnProgressB.Width.Value, 300, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false)
				}, "Hiper Progress", false);
			}
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0000710B File Offset: 0x0000530B
		private void CardResized()
		{
			this.RectProgressClip.Rect = new Rect(0.0, 0.0, this.CardLoad.ActualWidth, 12.0);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00007143 File Offset: 0x00005343
		private void BtnFinishIp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			ModBase.ClipboardSet(this.LabFinishIp.Text, true);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0004A014 File Offset: 0x00048214
		private void BtnFinishExit_Click(object sender, EventArgs e)
		{
			if (!PageLinkHiper.m_ManagerClient || ModMain.MyMsgBox("你确定要关闭联机房间吗？", "确认退出", "确定", "取消", "", true, true, false, null, null, null) != 2)
			{
				PageLinkHiper.HiperExit(false);
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x000063A1 File Offset: 0x000045A1
		private void BtnFinishCopy_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00007156 File Offset: 0x00005356
		private void BtnFinishPing_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			this.LabFinishPing.Text = "检测中";
			if (PageLinkHiper.m_PredicateClient.State != ModBase.LoadState.Loading)
			{
				PageLinkHiper.m_PredicateClient.Start(true, true);
			}
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00007186 File Offset: 0x00005386
		public PageLinkHiper.Subpages CreateReader()
		{
			return this.poolClient;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0004A058 File Offset: 0x00048258
		public void ExcludeReader(PageLinkHiper.Subpages value)
		{
			if (this.poolClient != value)
			{
				this.poolClient = value;
				ModBase.Log("[Hiper] 子页面更改为 " + ModBase.GetStringFromEnum(value), ModBase.LogLevel.Normal, "出现错误");
				base.PageOnContentExit();
				if (value == PageLinkHiper.Subpages.PanSelect)
				{
					this.LabSelectCode.Text = "(" + this.TextCert.Text.Substring(0, Math.Min(this.TextCert.Text.Length, 3)) + "…)";
				}
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0004A0E0 File Offset: 0x000482E0
		private void PageLinkHiper_OnPageEnter()
		{
			ModMain.m_RegIterator.PanCert.Visibility = ((this.CreateReader() == PageLinkHiper.Subpages.PanCert) ? Visibility.Visible : Visibility.Collapsed);
			ModMain.m_RegIterator.PanSelect.Visibility = ((this.CreateReader() == PageLinkHiper.Subpages.PanSelect) ? Visibility.Visible : Visibility.Collapsed);
			ModMain.m_RegIterator.PanFinish.Visibility = ((this.CreateReader() == PageLinkHiper.Subpages.PanFinish) ? Visibility.Visible : Visibility.Collapsed);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0004A140 File Offset: 0x00048340
		private static void HiperExit(bool ExitToCertPage)
		{
			ModBase.Log("[Hiper] 要求退出 Hiper（当前加载器状态为 " + ModBase.GetStringFromEnum(PageLinkHiper._CodeClient.State) + "）", ModBase.LogLevel.Normal, "出现错误");
			if (PageLinkHiper._CodeClient.State == ModBase.LoadState.Loading)
			{
				PageLinkHiper._CodeClient.Abort();
			}
			if (PageLinkHiper._CodeClient.State == ModBase.LoadState.Failed)
			{
				PageLinkHiper._CodeClient.State = ModBase.LoadState.Waiting;
			}
			ModBase.RunInUi(delegate()
			{
				if (ModMain.m_RegIterator != null && ModMain.m_RegIterator.IsLoaded)
				{
					ModMain.m_RegIterator.ExcludeReader(ExitToCertPage ? PageLinkHiper.Subpages.PanCert : PageLinkHiper.Subpages.PanSelect);
					ModMain.m_RegIterator.PageOnContentExit();
				}
			}, false);
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x0000718E File Offset: 0x0000538E
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x00007196 File Offset: 0x00005396
		internal virtual Grid PanLoad { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0000719F File Offset: 0x0000539F
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x0004A1C8 File Offset: 0x000483C8
		internal virtual MyCard CardLoad
		{
			[CompilerGenerated]
			get
			{
				return this.m_PageClient;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.CardLoad_MouseLeftButtonUp);
				SizeChangedEventHandler value3 = delegate(object sender, SizeChangedEventArgs e)
				{
					this.CardResized();
				};
				MyCard pageClient = this.m_PageClient;
				if (pageClient != null)
				{
					pageClient.MouseLeftButtonUp -= value2;
					pageClient.SizeChanged -= value3;
				}
				this.m_PageClient = value;
				pageClient = this.m_PageClient;
				if (pageClient != null)
				{
					pageClient.MouseLeftButtonUp += value2;
					pageClient.SizeChanged += value3;
				}
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x000071A7 File Offset: 0x000053A7
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x000071AF File Offset: 0x000053AF
		internal virtual MyLoading Load { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x000071B8 File Offset: 0x000053B8
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x000071C0 File Offset: 0x000053C0
		internal virtual TextBlock LabLoadTitle { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x000071C9 File Offset: 0x000053C9
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x000071D1 File Offset: 0x000053D1
		internal virtual TextBlock LabLoadDesc { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x000071DA File Offset: 0x000053DA
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x0004A228 File Offset: 0x00048428
		internal virtual MyIconButton BtnLoadCancel
		{
			[CompilerGenerated]
			get
			{
				return this._DispatcherClient;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = delegate(object sender, EventArgs e)
				{
					this.CancelLoad();
				};
				MyIconButton dispatcherClient = this._DispatcherClient;
				if (dispatcherClient != null)
				{
					dispatcherClient.Click -= value2;
				}
				this._DispatcherClient = value;
				dispatcherClient = this._DispatcherClient;
				if (dispatcherClient != null)
				{
					dispatcherClient.Click += value2;
				}
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x000071E2 File Offset: 0x000053E2
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x000071EA File Offset: 0x000053EA
		internal virtual ColumnDefinition ColumnProgressA { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x000071F3 File Offset: 0x000053F3
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x000071FB File Offset: 0x000053FB
		internal virtual ColumnDefinition ColumnProgressB { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x00007204 File Offset: 0x00005404
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x0000720C File Offset: 0x0000540C
		internal virtual RectangleGeometry RectProgressClip { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00007215 File Offset: 0x00005415
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x0000721D File Offset: 0x0000541D
		internal virtual Grid PanContent { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00007226 File Offset: 0x00005426
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0000722E File Offset: 0x0000542E
		internal virtual Grid PanCert { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00007237 File Offset: 0x00005437
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x0000723F File Offset: 0x0000543F
		internal virtual TextBlock LabCertTitle { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00007248 File Offset: 0x00005448
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x00007250 File Offset: 0x00005450
		internal virtual TextBlock LabCertDesc { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00007259 File Offset: 0x00005459
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x0004A26C File Offset: 0x0004846C
		internal virtual MyTextBox TextCert
		{
			[CompilerGenerated]
			get
			{
				return this.m_CreatorClient;
			}
			[CompilerGenerated]
			set
			{
				MyTextBox.ValidateChangedEventHandler obj = new MyTextBox.ValidateChangedEventHandler(this.TextCert_ValidateChanged);
				KeyEventHandler value2 = new KeyEventHandler(this.TextCert_KeyDown);
				MyTextBox creatorClient = this.m_CreatorClient;
				if (creatorClient != null)
				{
					MyTextBox.MapReader(obj);
					creatorClient.KeyDown -= value2;
				}
				this.m_CreatorClient = value;
				creatorClient = this.m_CreatorClient;
				if (creatorClient != null)
				{
					MyTextBox.SortReader(obj);
					creatorClient.KeyDown += value2;
				}
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00007261 File Offset: 0x00005461
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x0004A2C8 File Offset: 0x000484C8
		internal virtual MyButton BtnCertDone
		{
			[CompilerGenerated]
			get
			{
				return this.m_InitializerClient;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.BtnCertDone_Click();
				};
				MyButton initializerClient = this.m_InitializerClient;
				if (initializerClient != null)
				{
					initializerClient.Click -= value2;
				}
				this.m_InitializerClient = value;
				initializerClient = this.m_InitializerClient;
				if (initializerClient != null)
				{
					initializerClient.Click += value2;
				}
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00007269 File Offset: 0x00005469
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x00007271 File Offset: 0x00005471
		internal virtual Grid PanSelect { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0000727A File Offset: 0x0000547A
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x0004A30C File Offset: 0x0004850C
		internal virtual MyCard BtnSelectCreate
		{
			[CompilerGenerated]
			get
			{
				return this._RegClient;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.BtnSelectCreate_MouseLeftButtonUp);
				MyCard regClient = this._RegClient;
				if (regClient != null)
				{
					regClient.MouseLeftButtonUp -= value2;
				}
				this._RegClient = value;
				regClient = this._RegClient;
				if (regClient != null)
				{
					regClient.MouseLeftButtonUp += value2;
				}
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00007282 File Offset: 0x00005482
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x0004A350 File Offset: 0x00048550
		internal virtual MyCard BtnSelectJoin
		{
			[CompilerGenerated]
			get
			{
				return this._ProductClient;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.BtnSelectJoin_MouseLeftButtonUp);
				MyCard productClient = this._ProductClient;
				if (productClient != null)
				{
					productClient.MouseLeftButtonUp -= value2;
				}
				this._ProductClient = value;
				productClient = this._ProductClient;
				if (productClient != null)
				{
					productClient.MouseLeftButtonUp += value2;
				}
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0000728A File Offset: 0x0000548A
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x0004A394 File Offset: 0x00048594
		internal virtual MyCard BtnSelectReturn
		{
			[CompilerGenerated]
			get
			{
				return this.m_ListenerClient;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.BtnSelectReturn_MouseLeftButtonUp);
				MyCard listenerClient = this.m_ListenerClient;
				if (listenerClient != null)
				{
					listenerClient.MouseLeftButtonUp -= value2;
				}
				this.m_ListenerClient = value;
				listenerClient = this.m_ListenerClient;
				if (listenerClient != null)
				{
					listenerClient.MouseLeftButtonUp += value2;
				}
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00007292 File Offset: 0x00005492
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x0000729A File Offset: 0x0000549A
		internal virtual TextBlock LabSelectCode { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x000072A3 File Offset: 0x000054A3
		// (set) Token: 0x06000A14 RID: 2580 RVA: 0x000072AB File Offset: 0x000054AB
		internal virtual Grid PanFinish { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x000072B4 File Offset: 0x000054B4
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x000072BC File Offset: 0x000054BC
		internal virtual StackPanel BtnFinishQuality { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x000072C5 File Offset: 0x000054C5
		// (set) Token: 0x06000A18 RID: 2584 RVA: 0x000072CD File Offset: 0x000054CD
		internal virtual TextBlock LabFinishQuality { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x000072D6 File Offset: 0x000054D6
		// (set) Token: 0x06000A1A RID: 2586 RVA: 0x0004A3D8 File Offset: 0x000485D8
		internal virtual StackPanel BtnFinishPing
		{
			[CompilerGenerated]
			get
			{
				return this._BridgeClient;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.BtnFinishPing_MouseLeftButtonUp);
				StackPanel bridgeClient = this._BridgeClient;
				if (bridgeClient != null)
				{
					bridgeClient.MouseLeftButtonUp -= value2;
				}
				this._BridgeClient = value;
				bridgeClient = this._BridgeClient;
				if (bridgeClient != null)
				{
					bridgeClient.MouseLeftButtonUp += value2;
				}
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x000072DE File Offset: 0x000054DE
		// (set) Token: 0x06000A1C RID: 2588 RVA: 0x000072E6 File Offset: 0x000054E6
		internal virtual TextBlock LabFinishPing { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x000072EF File Offset: 0x000054EF
		// (set) Token: 0x06000A1E RID: 2590 RVA: 0x000072F7 File Offset: 0x000054F7
		internal virtual TextBlock LineFinishPing { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00007300 File Offset: 0x00005500
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x0004A41C File Offset: 0x0004861C
		internal virtual StackPanel BtnFinishIp
		{
			[CompilerGenerated]
			get
			{
				return this.exceptionClient;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.BtnFinishIp_MouseLeftButtonUp);
				StackPanel stackPanel = this.exceptionClient;
				if (stackPanel != null)
				{
					stackPanel.MouseLeftButtonUp -= value2;
				}
				this.exceptionClient = value;
				stackPanel = this.exceptionClient;
				if (stackPanel != null)
				{
					stackPanel.MouseLeftButtonUp += value2;
				}
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x00007308 File Offset: 0x00005508
		// (set) Token: 0x06000A22 RID: 2594 RVA: 0x00007310 File Offset: 0x00005510
		internal virtual TextBlock LabFinishIp { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x00007319 File Offset: 0x00005519
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x00007321 File Offset: 0x00005521
		internal virtual TextBlock LabFinishTime { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0000732A File Offset: 0x0000552A
		// (set) Token: 0x06000A26 RID: 2598 RVA: 0x00007332 File Offset: 0x00005532
		internal virtual TextBlock LabFinishTitle { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0000733B File Offset: 0x0000553B
		// (set) Token: 0x06000A28 RID: 2600 RVA: 0x00007343 File Offset: 0x00005543
		internal virtual TextBlock LabFinishDesc { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0000734C File Offset: 0x0000554C
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x0004A460 File Offset: 0x00048660
		internal virtual MyButton BtnFinishCopy
		{
			[CompilerGenerated]
			get
			{
				return this._ProducerClient;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnFinishCopy_Click);
				MyButton producerClient = this._ProducerClient;
				if (producerClient != null)
				{
					producerClient.Click -= value2;
				}
				this._ProducerClient = value;
				producerClient = this._ProducerClient;
				if (producerClient != null)
				{
					producerClient.Click += value2;
				}
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x00007354 File Offset: 0x00005554
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x0004A4A4 File Offset: 0x000486A4
		internal virtual MyButton BtnFinishExit
		{
			[CompilerGenerated]
			get
			{
				return this.schemaClient;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnFinishExit_Click);
				MyButton myButton = this.schemaClient;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.schemaClient = value;
				myButton = this.schemaClient;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x0000735C File Offset: 0x0000555C
		// (set) Token: 0x06000A2E RID: 2606 RVA: 0x00007364 File Offset: 0x00005564
		internal virtual Grid PanAlways { get; set; }

		// Token: 0x06000A2F RID: 2607 RVA: 0x0004A4E8 File Offset: 0x000486E8
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._PublisherClient)
			{
				this._PublisherClient = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelink/pagelinkhiper.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0004A518 File Offset: 0x00048718
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanLoad = (Grid)target;
				return;
			}
			if (connectionId == 2)
			{
				this.CardLoad = (MyCard)target;
				return;
			}
			if (connectionId == 3)
			{
				this.Load = (MyLoading)target;
				return;
			}
			if (connectionId == 4)
			{
				this.LabLoadTitle = (TextBlock)target;
				return;
			}
			if (connectionId == 5)
			{
				this.LabLoadDesc = (TextBlock)target;
				return;
			}
			if (connectionId == 6)
			{
				this.BtnLoadCancel = (MyIconButton)target;
				return;
			}
			if (connectionId == 7)
			{
				this.ColumnProgressA = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 8)
			{
				this.ColumnProgressB = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 9)
			{
				this.RectProgressClip = (RectangleGeometry)target;
				return;
			}
			if (connectionId == 10)
			{
				this.PanContent = (Grid)target;
				return;
			}
			if (connectionId == 11)
			{
				this.PanCert = (Grid)target;
				return;
			}
			if (connectionId == 12)
			{
				this.LabCertTitle = (TextBlock)target;
				return;
			}
			if (connectionId == 13)
			{
				this.LabCertDesc = (TextBlock)target;
				return;
			}
			if (connectionId == 14)
			{
				this.TextCert = (MyTextBox)target;
				return;
			}
			if (connectionId == 15)
			{
				this.BtnCertDone = (MyButton)target;
				return;
			}
			if (connectionId == 16)
			{
				this.PanSelect = (Grid)target;
				return;
			}
			if (connectionId == 17)
			{
				this.BtnSelectCreate = (MyCard)target;
				return;
			}
			if (connectionId == 18)
			{
				this.BtnSelectJoin = (MyCard)target;
				return;
			}
			if (connectionId == 19)
			{
				this.BtnSelectReturn = (MyCard)target;
				return;
			}
			if (connectionId == 20)
			{
				this.LabSelectCode = (TextBlock)target;
				return;
			}
			if (connectionId == 21)
			{
				this.PanFinish = (Grid)target;
				return;
			}
			if (connectionId == 22)
			{
				this.BtnFinishQuality = (StackPanel)target;
				return;
			}
			if (connectionId == 23)
			{
				this.LabFinishQuality = (TextBlock)target;
				return;
			}
			if (connectionId == 24)
			{
				this.BtnFinishPing = (StackPanel)target;
				return;
			}
			if (connectionId == 25)
			{
				this.LabFinishPing = (TextBlock)target;
				return;
			}
			if (connectionId == 26)
			{
				this.LineFinishPing = (TextBlock)target;
				return;
			}
			if (connectionId == 27)
			{
				this.BtnFinishIp = (StackPanel)target;
				return;
			}
			if (connectionId == 28)
			{
				this.LabFinishIp = (TextBlock)target;
				return;
			}
			if (connectionId == 29)
			{
				this.LabFinishTime = (TextBlock)target;
				return;
			}
			if (connectionId == 30)
			{
				this.LabFinishTitle = (TextBlock)target;
				return;
			}
			if (connectionId == 31)
			{
				this.LabFinishDesc = (TextBlock)target;
				return;
			}
			if (connectionId == 32)
			{
				this.BtnFinishCopy = (MyButton)target;
				return;
			}
			if (connectionId == 33)
			{
				this.BtnFinishExit = (MyButton)target;
				return;
			}
			if (connectionId == 34)
			{
				this.PanAlways = (Grid)target;
				return;
			}
			this._PublisherClient = true;
		}

		// Token: 0x0400050F RID: 1295
		public static bool m_ManagerClient;

		// Token: 0x04000510 RID: 1296
		private static string _ModelClient;

		// Token: 0x04000511 RID: 1297
		private static int m_WrapperClient;

		// Token: 0x04000512 RID: 1298
		private bool m_BaseClient;

		// Token: 0x04000513 RID: 1299
		public static string _AttributeClient = ModBase.m_InstanceRepository + "联机模块\\";

		// Token: 0x04000514 RID: 1300
		[CompilerGenerated]
		[AccessedThroughProperty("InitLoader")]
		private static ModLoader.LoaderCombo<int> _CodeClient;

		// Token: 0x04000515 RID: 1301
		private static int _PrototypeClient;

		// Token: 0x04000516 RID: 1302
		private static ModBase.LoadState annotationClient;

		// Token: 0x04000517 RID: 1303
		private static bool infoClient;

		// Token: 0x04000518 RID: 1304
		private static int _AdapterClient;

		// Token: 0x04000519 RID: 1305
		private static List<string> facadeClient;

		// Token: 0x0400051A RID: 1306
		private static ModBase.LoadState m_ListClient;

		// Token: 0x0400051B RID: 1307
		private static string _MerchantClient;

		// Token: 0x0400051C RID: 1308
		private static int _AuthenticationClient;

		// Token: 0x0400051D RID: 1309
		private static int m_AlgoClient;

		// Token: 0x0400051E RID: 1310
		private static DateTime comparatorClient;

		// Token: 0x0400051F RID: 1311
		private static string _MappingClient;

		// Token: 0x04000520 RID: 1312
		private bool tokenizerClient;

		// Token: 0x04000521 RID: 1313
		private static int m_FilterClient;

		// Token: 0x04000522 RID: 1314
		private static string _DatabaseClient;

		// Token: 0x04000523 RID: 1315
		private static ModLoader.LoaderTask<bool, int> m_PredicateClient;

		// Token: 0x04000524 RID: 1316
		private PageLinkHiper.Subpages poolClient;

		// Token: 0x04000525 RID: 1317
		[AccessedThroughProperty("PanLoad")]
		[CompilerGenerated]
		private Grid _CustomerClient;

		// Token: 0x04000526 RID: 1318
		[AccessedThroughProperty("CardLoad")]
		[CompilerGenerated]
		private MyCard m_PageClient;

		// Token: 0x04000527 RID: 1319
		[AccessedThroughProperty("Load")]
		[CompilerGenerated]
		private MyLoading interceptorClient;

		// Token: 0x04000528 RID: 1320
		[CompilerGenerated]
		[AccessedThroughProperty("LabLoadTitle")]
		private TextBlock m_ContainerClient;

		// Token: 0x04000529 RID: 1321
		[AccessedThroughProperty("LabLoadDesc")]
		[CompilerGenerated]
		private TextBlock m_ParamsClient;

		// Token: 0x0400052A RID: 1322
		[AccessedThroughProperty("BtnLoadCancel")]
		[CompilerGenerated]
		private MyIconButton _DispatcherClient;

		// Token: 0x0400052B RID: 1323
		[CompilerGenerated]
		[AccessedThroughProperty("ColumnProgressA")]
		private ColumnDefinition m_ProcessClient;

		// Token: 0x0400052C RID: 1324
		[CompilerGenerated]
		[AccessedThroughProperty("ColumnProgressB")]
		private ColumnDefinition m_ParameterClient;

		// Token: 0x0400052D RID: 1325
		[CompilerGenerated]
		[AccessedThroughProperty("RectProgressClip")]
		private RectangleGeometry m_RecordClient;

		// Token: 0x0400052E RID: 1326
		[CompilerGenerated]
		[AccessedThroughProperty("PanContent")]
		private Grid serviceClient;

		// Token: 0x0400052F RID: 1327
		[AccessedThroughProperty("PanCert")]
		[CompilerGenerated]
		private Grid invocationClient;

		// Token: 0x04000530 RID: 1328
		[AccessedThroughProperty("LabCertTitle")]
		[CompilerGenerated]
		private TextBlock m_ProxyClient;

		// Token: 0x04000531 RID: 1329
		[CompilerGenerated]
		[AccessedThroughProperty("LabCertDesc")]
		private TextBlock m_MessageClient;

		// Token: 0x04000532 RID: 1330
		[AccessedThroughProperty("TextCert")]
		[CompilerGenerated]
		private MyTextBox m_CreatorClient;

		// Token: 0x04000533 RID: 1331
		[AccessedThroughProperty("BtnCertDone")]
		[CompilerGenerated]
		private MyButton m_InitializerClient;

		// Token: 0x04000534 RID: 1332
		[AccessedThroughProperty("PanSelect")]
		[CompilerGenerated]
		private Grid m_SingletonClient;

		// Token: 0x04000535 RID: 1333
		[AccessedThroughProperty("BtnSelectCreate")]
		[CompilerGenerated]
		private MyCard _RegClient;

		// Token: 0x04000536 RID: 1334
		[AccessedThroughProperty("BtnSelectJoin")]
		[CompilerGenerated]
		private MyCard _ProductClient;

		// Token: 0x04000537 RID: 1335
		[CompilerGenerated]
		[AccessedThroughProperty("BtnSelectReturn")]
		private MyCard m_ListenerClient;

		// Token: 0x04000538 RID: 1336
		[CompilerGenerated]
		[AccessedThroughProperty("LabSelectCode")]
		private TextBlock collectionClient;

		// Token: 0x04000539 RID: 1337
		[CompilerGenerated]
		[AccessedThroughProperty("PanFinish")]
		private Grid _VisitorClient;

		// Token: 0x0400053A RID: 1338
		[CompilerGenerated]
		[AccessedThroughProperty("BtnFinishQuality")]
		private StackPanel m_ValueClient;

		// Token: 0x0400053B RID: 1339
		[AccessedThroughProperty("LabFinishQuality")]
		[CompilerGenerated]
		private TextBlock m_ObjectClient;

		// Token: 0x0400053C RID: 1340
		[CompilerGenerated]
		[AccessedThroughProperty("BtnFinishPing")]
		private StackPanel _BridgeClient;

		// Token: 0x0400053D RID: 1341
		[AccessedThroughProperty("LabFinishPing")]
		[CompilerGenerated]
		private TextBlock reponseClient;

		// Token: 0x0400053E RID: 1342
		[AccessedThroughProperty("LineFinishPing")]
		[CompilerGenerated]
		private TextBlock m_GlobalClient;

		// Token: 0x0400053F RID: 1343
		[AccessedThroughProperty("BtnFinishIp")]
		[CompilerGenerated]
		private StackPanel exceptionClient;

		// Token: 0x04000540 RID: 1344
		[AccessedThroughProperty("LabFinishIp")]
		[CompilerGenerated]
		private TextBlock _UtilsClient;

		// Token: 0x04000541 RID: 1345
		[AccessedThroughProperty("LabFinishTime")]
		[CompilerGenerated]
		private TextBlock m_ClassClient;

		// Token: 0x04000542 RID: 1346
		[AccessedThroughProperty("LabFinishTitle")]
		[CompilerGenerated]
		private TextBlock policyClient;

		// Token: 0x04000543 RID: 1347
		[CompilerGenerated]
		[AccessedThroughProperty("LabFinishDesc")]
		private TextBlock orderClient;

		// Token: 0x04000544 RID: 1348
		[AccessedThroughProperty("BtnFinishCopy")]
		[CompilerGenerated]
		private MyButton _ProducerClient;

		// Token: 0x04000545 RID: 1349
		[CompilerGenerated]
		[AccessedThroughProperty("BtnFinishExit")]
		private MyButton schemaClient;

		// Token: 0x04000546 RID: 1350
		[AccessedThroughProperty("PanAlways")]
		[CompilerGenerated]
		private Grid m_DescriptorClient;

		// Token: 0x04000547 RID: 1351
		private bool _PublisherClient;

		// Token: 0x020000FE RID: 254
		public class CertOutdatedException : Exception
		{
		}

		// Token: 0x020000FF RID: 255
		public enum Subpages
		{
			// Token: 0x04000549 RID: 1353
			PanCert,
			// Token: 0x0400054A RID: 1354
			PanSelect,
			// Token: 0x0400054B RID: 1355
			PanFinish
		}
	}
}
