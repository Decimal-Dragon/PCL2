using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace PCL
{
	// Token: 0x02000104 RID: 260
	[DesignerGenerated]
	public class PageLinkIoi : MyPageRight, IComponentConnector
	{
		// Token: 0x06000A53 RID: 2643 RVA: 0x000074E7 File Offset: 0x000056E7
		public PageLinkIoi()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0000507A File Offset: 0x0000327A
		public static bool IoiStop(bool SleepWhenKilled)
		{
			return false;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0000507A File Offset: 0x0000327A
		public static bool IoiStart()
		{
			return false;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x000063A1 File Offset: 0x000045A1
		private static void IoiLogLine(string Content)
		{
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000063A1 File Offset: 0x000045A1
		public void RefreshUi()
		{
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000063A1 File Offset: 0x000045A1
		public void RefreshWorker()
		{
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000063A1 File Offset: 0x000045A1
		private static void SendPortsubRequest(PageLinkIoi.LinkUserIoi User)
		{
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0004AC88 File Offset: 0x00048E88
		private static void SendConnectRequest(PageLinkIoi.LinkUserIoi User)
		{
			JObject jobject = new JObject();
			jobject["version"] = 4;
			jobject["name"] = PageLinkIoi.GetPlayerName();
			jobject["id"] = PageLinkIoi.m_FieldConfig;
			jobject["type"] = "connect";
			User.Send(jobject);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0004ACF4 File Offset: 0x00048EF4
		private static void SendUpdateRequest(PageLinkIoi.LinkUserIoi User, int Stage, long Unique = -1L)
		{
			if (Unique == -1L)
			{
				Unique = ModBase.GetTimeTick();
			}
			JObject jobject = new JObject();
			jobject["name"] = PageLinkIoi.GetPlayerName();
			jobject["id"] = PageLinkIoi.m_FieldConfig;
			jobject["type"] = "update";
			jobject["stage"] = Stage;
			jobject["unique"] = Unique;
			if (Stage < 3)
			{
				JArray jarray = new JArray();
				try
				{
					foreach (PageLinkIoi.RoomEntry roomEntry in PageLinkIoi.repositoryConfig)
					{
						JObject jobject2 = new JObject();
						jobject2["name"] = roomEntry.connectionTest;
						jobject2["port"] = roomEntry.workerTest;
						jarray.Add(jobject2);
					}
				}
				finally
				{
					List<PageLinkIoi.RoomEntry>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				jobject["rooms"] = jarray;
				User._ExpressionTest[Unique] = DateTime.Now;
			}
			User.Send(jobject);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x000063A1 File Offset: 0x000045A1
		private static void SendDisconnectRequest(PageLinkIoi.LinkUserIoi User, string Message = null, bool IsError = false)
		{
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000063A1 File Offset: 0x000045A1
		private static void BtnListRefresh_Click(MyIconButton sender, EventArgs e)
		{
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x000063A1 File Offset: 0x000045A1
		private static void BtnListDisconnect_Click(MyIconButton sender, EventArgs e)
		{
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000063A1 File Offset: 0x000045A1
		public static void BtnLeftCopy_Click()
		{
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0004AE28 File Offset: 0x00049028
		public static string GetPlayerName()
		{
			if (PageLinkIoi._ComposerConfig == null)
			{
				ModLaunch.McLoginName();
				PageLinkIoi._ComposerConfig = ModLaunch.McLoginName();
			}
			string text = ModBase.m_IdentifierRepository.Get("LinkName", null).ToString().Trim();
			string result;
			if (Operators.CompareString(text, "", false) != 0)
			{
				result = text.Trim();
			}
			else
			{
				result = PageLinkIoi._ComposerConfig;
			}
			return result;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000074F6 File Offset: 0x000056F6
		private static bool IsPlayerNameValid(string Name)
		{
			return true;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000063A1 File Offset: 0x000045A1
		public static void StartSocketListener()
		{
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0004AE88 File Offset: 0x00049088
		private List<PageLinkIoi.RoomEntry> GetRoomList()
		{
			List<PageLinkIoi.RoomEntry> list = new List<PageLinkIoi.RoomEntry>(PageLinkIoi.repositoryConfig);
			checked
			{
				int num = PageLinkIoi._IteratorConfig.Count - 1;
				int num2 = 0;
				while (num2 <= num && num2 <= PageLinkIoi._IteratorConfig.Count - 1)
				{
					list.AddRange(Enumerable.ElementAtOrDefault<PageLinkIoi.LinkUserIoi>(PageLinkIoi._IteratorConfig.Values, num2)._FactoryTest);
					num2++;
				}
				return list;
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x000063A1 File Offset: 0x000045A1
		public static void BtnLeftCreate_Click()
		{
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x000063A1 File Offset: 0x000045A1
		private static void SendPortsubBack(PageLinkIoi.LinkUserIoi User, int TargetVersion)
		{
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x000063A1 File Offset: 0x000045A1
		private void LinkCreate()
		{
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0004AEE4 File Offset: 0x000490E4
		private static void SendUpdateRequestToAllUsers()
		{
			checked
			{
				int num = PageLinkIoi._IteratorConfig.Count - 1;
				int num2 = 0;
				while (num2 <= num && num2 <= PageLinkIoi._IteratorConfig.Count - 1)
				{
					PageLinkIoi.LinkUserIoi linkUserIoi = Enumerable.ElementAtOrDefault<PageLinkIoi.LinkUserIoi>(PageLinkIoi._IteratorConfig.Values, num2);
					if (linkUserIoi._ExporterTest >= 1.0)
					{
						try
						{
							PageLinkIoi.SendUpdateRequest(linkUserIoi, 1, -1L);
						}
						catch (Exception ex)
						{
							ModBase.Log(ex, "发送全局刷新请求失败（" + linkUserIoi._ConfigurationTest + "）", ModBase.LogLevel.Debug, "出现错误");
						}
					}
					num2++;
				}
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000063A1 File Offset: 0x000045A1
		private static void BtnRoomEdit_Click(MyIconButton sender, EventArgs e)
		{
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0004AF90 File Offset: 0x00049190
		private static void BtnRoom_Click(MyListItem sender, EventArgs e)
		{
			PageLinkIoi.RoomEntry roomEntry = (PageLinkIoi.RoomEntry)sender.Tag;
			if (ModMain.MyMsgBox("请在多人游戏页面点击直接连接，输入 " + roomEntry.ValidateMapper() + " 以进入服务器！", "加入房间", "复制地址", "确定", "", false, true, false, null, null, null) == 1)
			{
				ModBase.ClipboardSet(roomEntry.ValidateMapper(), true);
			}
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x000063A1 File Offset: 0x000045A1
		private static void BtnRoomClose_Click(MyIconButton sender, EventArgs e)
		{
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000063A1 File Offset: 0x000045A1
		public static void ReceiveJson(JObject JsonData, Socket NewSocket = null)
		{
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x000063A1 File Offset: 0x000045A1
		public static void UserRemove(PageLinkIoi.LinkUserIoi User, bool ShowLeaveMessage)
		{
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000063A1 File Offset: 0x000045A1
		public static void ModuleStopManually()
		{
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x000074F9 File Offset: 0x000056F9
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x00007501 File Offset: 0x00005701
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x0000750A File Offset: 0x0000570A
		// (set) Token: 0x06000A71 RID: 2673 RVA: 0x00007512 File Offset: 0x00005712
		internal virtual MyCard CardUser { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0000751B File Offset: 0x0000571B
		// (set) Token: 0x06000A73 RID: 2675 RVA: 0x00007523 File Offset: 0x00005723
		internal virtual MyHint LabHint { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0000752C File Offset: 0x0000572C
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x00007534 File Offset: 0x00005734
		internal virtual StackPanel PanUserList { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0000753D File Offset: 0x0000573D
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x0004AFEC File Offset: 0x000491EC
		internal virtual MyButton BtnLeftCreate
		{
			[CompilerGenerated]
			get
			{
				return this.specificationConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = (PageLinkIoi._Closure$__.$IR63-1 == null) ? (PageLinkIoi._Closure$__.$IR63-1 = delegate(object sender, MouseButtonEventArgs e)
				{
					PageLinkIoi.BtnLeftCreate_Click();
				}) : PageLinkIoi._Closure$__.$IR63-1;
				MyButton myButton = this.specificationConfig;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.specificationConfig = value;
				myButton = this.specificationConfig;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00007545 File Offset: 0x00005745
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x0004B048 File Offset: 0x00049248
		internal virtual MyButton BtnLeftCopy
		{
			[CompilerGenerated]
			get
			{
				return this.mockConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = (PageLinkIoi._Closure$__.$IR67-2 == null) ? (PageLinkIoi._Closure$__.$IR67-2 = delegate(object sender, MouseButtonEventArgs e)
				{
					PageLinkIoi.BtnLeftCopy_Click();
				}) : PageLinkIoi._Closure$__.$IR67-2;
				MyButton myButton = this.mockConfig;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.mockConfig = value;
				myButton = this.mockConfig;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0000754D File Offset: 0x0000574D
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x00007555 File Offset: 0x00005755
		internal virtual MyCard CardRoom { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0000755E File Offset: 0x0000575E
		// (set) Token: 0x06000A7D RID: 2685 RVA: 0x00007566 File Offset: 0x00005766
		internal virtual StackPanel PanRoom { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0000756F File Offset: 0x0000576F
		// (set) Token: 0x06000A7F RID: 2687 RVA: 0x0004B0A4 File Offset: 0x000492A4
		internal virtual MyListItem BtnCreate
		{
			[CompilerGenerated]
			get
			{
				return this.m_HelperConfig;
			}
			[CompilerGenerated]
			set
			{
				MyListItem.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.LinkCreate();
				};
				MyListItem helperConfig = this.m_HelperConfig;
				if (helperConfig != null)
				{
					helperConfig.Click -= value2;
				}
				this.m_HelperConfig = value;
				helperConfig = this.m_HelperConfig;
				if (helperConfig != null)
				{
					helperConfig.Click += value2;
				}
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00007577 File Offset: 0x00005777
		// (set) Token: 0x06000A81 RID: 2689 RVA: 0x0000757F File Offset: 0x0000577F
		internal virtual MyCard PanLoad { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00007588 File Offset: 0x00005788
		// (set) Token: 0x06000A83 RID: 2691 RVA: 0x00007590 File Offset: 0x00005790
		internal virtual MyLoading Load { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00007599 File Offset: 0x00005799
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x000075A1 File Offset: 0x000057A1
		internal virtual Grid PanAlways { get; set; }

		// Token: 0x06000A86 RID: 2694 RVA: 0x0004B0E8 File Offset: 0x000492E8
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._SerializerConfig)
			{
				this._SerializerConfig = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelink/pagelinkioi.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0004B118 File Offset: 0x00049318
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
				this.CardUser = (MyCard)target;
				return;
			}
			if (connectionId == 3)
			{
				this.LabHint = (MyHint)target;
				return;
			}
			if (connectionId == 4)
			{
				this.PanUserList = (StackPanel)target;
				return;
			}
			if (connectionId == 5)
			{
				this.BtnLeftCreate = (MyButton)target;
				return;
			}
			if (connectionId == 6)
			{
				this.BtnLeftCopy = (MyButton)target;
				return;
			}
			if (connectionId == 7)
			{
				this.CardRoom = (MyCard)target;
				return;
			}
			if (connectionId == 8)
			{
				this.PanRoom = (StackPanel)target;
				return;
			}
			if (connectionId == 9)
			{
				this.BtnCreate = (MyListItem)target;
				return;
			}
			if (connectionId == 10)
			{
				this.PanLoad = (MyCard)target;
				return;
			}
			if (connectionId == 11)
			{
				this.Load = (MyLoading)target;
				return;
			}
			if (connectionId == 12)
			{
				this.PanAlways = (Grid)target;
				return;
			}
			this._SerializerConfig = true;
		}

		// Token: 0x04000554 RID: 1364
		public static string m_BroadcasterConfig = ModBase.m_InstanceRepository + "联机模块\\IOI 联机模块.exe";

		// Token: 0x04000555 RID: 1365
		private static string m_FieldConfig;

		// Token: 0x04000556 RID: 1366
		private static string _ReaderConfig;

		// Token: 0x04000557 RID: 1367
		private static Process m_ClientConfig = null;

		// Token: 0x04000558 RID: 1368
		private static ModBase.LoadState _ConfigConfig = ModBase.LoadState.Waiting;

		// Token: 0x04000559 RID: 1369
		private static int m_TestsConfig = 0;

		// Token: 0x0400055A RID: 1370
		private static string mapperConfig = "";

		// Token: 0x0400055B RID: 1371
		private static string threadConfig = "";

		// Token: 0x0400055C RID: 1372
		private static string _PropertyConfig = "";

		// Token: 0x0400055D RID: 1373
		private static string _ComposerConfig = null;

		// Token: 0x0400055E RID: 1374
		public static Dictionary<string, PageLinkIoi.LinkUserIoi> _IteratorConfig = new Dictionary<string, PageLinkIoi.LinkUserIoi>();

		// Token: 0x0400055F RID: 1375
		private static List<PageLinkIoi.RoomEntry> repositoryConfig = new List<PageLinkIoi.RoomEntry>();

		// Token: 0x04000560 RID: 1376
		[CompilerGenerated]
		[AccessedThroughProperty("PanBack")]
		private MyScrollViewer _TestConfig;

		// Token: 0x04000561 RID: 1377
		[AccessedThroughProperty("CardUser")]
		[CompilerGenerated]
		private MyCard mapConfig;

		// Token: 0x04000562 RID: 1378
		[AccessedThroughProperty("LabHint")]
		[CompilerGenerated]
		private MyHint errorConfig;

		// Token: 0x04000563 RID: 1379
		[AccessedThroughProperty("PanUserList")]
		[CompilerGenerated]
		private StackPanel m_ContextConfig;

		// Token: 0x04000564 RID: 1380
		[AccessedThroughProperty("BtnLeftCreate")]
		[CompilerGenerated]
		private MyButton specificationConfig;

		// Token: 0x04000565 RID: 1381
		[AccessedThroughProperty("BtnLeftCopy")]
		[CompilerGenerated]
		private MyButton mockConfig;

		// Token: 0x04000566 RID: 1382
		[AccessedThroughProperty("CardRoom")]
		[CompilerGenerated]
		private MyCard m_RequestConfig;

		// Token: 0x04000567 RID: 1383
		[AccessedThroughProperty("PanRoom")]
		[CompilerGenerated]
		private StackPanel _DicConfig;

		// Token: 0x04000568 RID: 1384
		[CompilerGenerated]
		[AccessedThroughProperty("BtnCreate")]
		private MyListItem m_HelperConfig;

		// Token: 0x04000569 RID: 1385
		[AccessedThroughProperty("PanLoad")]
		[CompilerGenerated]
		private MyCard m_IssuerConfig;

		// Token: 0x0400056A RID: 1386
		[AccessedThroughProperty("Load")]
		[CompilerGenerated]
		private MyLoading m_IndexerConfig;

		// Token: 0x0400056B RID: 1387
		[CompilerGenerated]
		[AccessedThroughProperty("PanAlways")]
		private Grid interpreterConfig;

		// Token: 0x0400056C RID: 1388
		private bool _SerializerConfig;

		// Token: 0x02000105 RID: 261
		public abstract class LinkUserBase : IDisposable
		{
			// Token: 0x06000A8A RID: 2698 RVA: 0x000063A1 File Offset: 0x000045A1
			public void Send(JObject Request)
			{
			}

			// Token: 0x06000A8B RID: 2699 RVA: 0x000063A1 File Offset: 0x000045A1
			public void StartListener()
			{
			}

			// Token: 0x06000A8C RID: 2700 RVA: 0x000075B2 File Offset: 0x000057B2
			public void BindSocket(Socket Socket)
			{
				if (this.getterTest != null)
				{
					throw new Exception("该用户已经绑定了 Socket");
				}
				this.getterTest = Socket;
				this.StartListener();
			}

			// Token: 0x06000A8D RID: 2701 RVA: 0x0004B1FC File Offset: 0x000493FC
			public LinkUserBase(string Id, string DisplayName)
			{
				this.taskTest = ModBase.GetUuid();
				this.getterTest = null;
				this.m_TokenTest = null;
				this._ExpressionTest = new Dictionary<long, DateTime>();
				this.m_WriterTest = new Queue<int>();
				this.m_RegistryTest = DateTime.Now;
				this.ruleTest = DateTime.Now;
				this.m_ProccesorTest = false;
				this._ConsumerTest = Id;
				this._ConfigurationTest = DisplayName;
				ModBase.Log("[IOI] 无通信包的新用户对象：" + this.ToString(), ModBase.LogLevel.Normal, "出现错误");
			}

			// Token: 0x06000A8E RID: 2702 RVA: 0x0004B288 File Offset: 0x00049488
			public LinkUserBase(string Id, string DisplayName, Socket Socket)
			{
				this.taskTest = ModBase.GetUuid();
				this.getterTest = null;
				this.m_TokenTest = null;
				this._ExpressionTest = new Dictionary<long, DateTime>();
				this.m_WriterTest = new Queue<int>();
				this.m_RegistryTest = DateTime.Now;
				this.ruleTest = DateTime.Now;
				this.m_ProccesorTest = false;
				this._ConsumerTest = Id;
				this._ConfigurationTest = DisplayName;
				this.getterTest = Socket;
				ModBase.Log("[IOI] 新用户对象：" + this.ToString(), ModBase.LogLevel.Normal, "出现错误");
				this.StartListener();
			}

			// Token: 0x06000A8F RID: 2703 RVA: 0x000075D4 File Offset: 0x000057D4
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					this._ConfigurationTest,
					" @ ",
					this._ConsumerTest,
					" #",
					Conversions.ToString(this.taskTest)
				});
			}

			// Token: 0x06000A90 RID: 2704 RVA: 0x00007611 File Offset: 0x00005811
			public static implicit operator string(PageLinkIoi.LinkUserBase User)
			{
				return User.ToString();
			}

			// Token: 0x06000A91 RID: 2705 RVA: 0x00007619 File Offset: 0x00005819
			protected virtual void Dispose(bool IsDisposing)
			{
				if (this.getterTest != null)
				{
					this.getterTest.Dispose();
				}
				if (this.m_TokenTest != null && this.m_TokenTest.IsAlive)
				{
					this.m_TokenTest.Interrupt();
				}
			}

			// Token: 0x06000A92 RID: 2706 RVA: 0x0000764E File Offset: 0x0000584E
			public void Dispose()
			{
				if (!this.m_ProccesorTest)
				{
					this.m_ProccesorTest = true;
					this.Dispose(true);
				}
				GC.SuppressFinalize(this);
			}

			// Token: 0x0400056D RID: 1389
			public int taskTest;

			// Token: 0x0400056E RID: 1390
			public string _ConsumerTest;

			// Token: 0x0400056F RID: 1391
			public string _ConfigurationTest;

			// Token: 0x04000570 RID: 1392
			public Socket getterTest;

			// Token: 0x04000571 RID: 1393
			public Thread m_TokenTest;

			// Token: 0x04000572 RID: 1394
			public Dictionary<long, DateTime> _ExpressionTest;

			// Token: 0x04000573 RID: 1395
			public Queue<int> m_WriterTest;

			// Token: 0x04000574 RID: 1396
			public DateTime m_RegistryTest;

			// Token: 0x04000575 RID: 1397
			public DateTime ruleTest;

			// Token: 0x04000576 RID: 1398
			public bool m_ProccesorTest;
		}

		// Token: 0x02000106 RID: 262
		public class LinkUserIoi : PageLinkIoi.LinkUserBase
		{
			// Token: 0x06000A94 RID: 2708 RVA: 0x0000766C File Offset: 0x0000586C
			public LinkUserIoi(string Id, string DisplayName, Socket Socket) : base(Id, DisplayName, Socket)
			{
				this.m_SetterTest = new Dictionary<int, string>();
				this._FactoryTest = new List<PageLinkIoi.RoomEntry>();
				this._ExporterTest = 0.0;
				this.importerTest = null;
			}

			// Token: 0x06000A95 RID: 2709 RVA: 0x000076A4 File Offset: 0x000058A4
			public LinkUserIoi(string Id, string DisplayName) : base(Id, DisplayName)
			{
				this.m_SetterTest = new Dictionary<int, string>();
				this._FactoryTest = new List<PageLinkIoi.RoomEntry>();
				this._ExporterTest = 0.0;
				this.importerTest = null;
			}

			// Token: 0x06000A96 RID: 2710 RVA: 0x0004B320 File Offset: 0x00049520
			public string GetDescription()
			{
				if (this._ExporterTest >= 1.0)
				{
					return "已连接，" + ((!Enumerable.Any<int>(this.m_WriterTest)) ? "检查延迟中" : (Conversions.ToString(Math.Round(Enumerable.Average(this.m_WriterTest))) + "ms"));
				}
				return "正在连接，" + Conversions.ToString(Math.Round(this._ExporterTest * 100.0)) + "%";
			}

			// Token: 0x06000A97 RID: 2711 RVA: 0x0004B3A8 File Offset: 0x000495A8
			public MyListItem ToListItem()
			{
				MyListItem myListItem = new MyListItem
				{
					Title = this._ConfigurationTest,
					Height = 42.0,
					Tag = this,
					Type = MyListItem.CheckType.None,
					Logo = "pack://application:,,,/images/Blocks/Grass.png"
				};
				MyIconButton myIconButton = new MyIconButton
				{
					Logo = "M875.52 148.48C783.36 56.32 655.36 0 512 0 291.84 0 107.52 138.24 30.72 332.8l122.88 46.08C204.8 230.4 348.16 128 512 128c107.52 0 199.68 40.96 271.36 112.64L640 384h384V0L875.52 148.48zM512 896c-107.52 0-199.68-40.96-271.36-112.64L384 640H0v384l148.48-148.48C240.64 967.68 368.64 1024 512 1024c220.16 0 404.48-138.24 481.28-332.8L870.4 645.12C819.2 793.6 675.84 896 512 896z",
					LogoScale = 0.85,
					ToolTip = "刷新",
					Tag = this
				};
				myIconButton.Click += ((PageLinkIoi.LinkUserIoi._Closure$__.$IR7-1 == null) ? (PageLinkIoi.LinkUserIoi._Closure$__.$IR7-1 = delegate(object sender, EventArgs e)
				{
					PageLinkIoi.BtnListRefresh_Click((MyIconButton)sender, e);
				}) : PageLinkIoi.LinkUserIoi._Closure$__.$IR7-1);
				ToolTipService.SetPlacement(myIconButton, PlacementMode.Bottom);
				ToolTipService.SetHorizontalOffset(myIconButton, -10.0);
				ToolTipService.SetVerticalOffset(myIconButton, 5.0);
				ToolTipService.SetInitialShowDelay(myIconButton, 200);
				MyIconButton myIconButton2 = new MyIconButton
				{
					Logo = "F1 M 26.9166,22.1667L 37.9999,33.25L 49.0832,22.1668L 53.8332,26.9168L 42.7499,38L 53.8332,49.0834L 49.0833,53.8334L 37.9999,42.75L 26.9166,53.8334L 22.1666,49.0833L 33.25,38L 22.1667,26.9167L 26.9166,22.1667 Z",
					LogoScale = 0.85,
					ToolTip = "断开",
					Tag = this
				};
				myIconButton2.Click += ((PageLinkIoi.LinkUserIoi._Closure$__.$IR7-2 == null) ? (PageLinkIoi.LinkUserIoi._Closure$__.$IR7-2 = delegate(object sender, EventArgs e)
				{
					PageLinkIoi.BtnListDisconnect_Click((MyIconButton)sender, e);
				}) : PageLinkIoi.LinkUserIoi._Closure$__.$IR7-2);
				ToolTipService.SetPlacement(myIconButton2, PlacementMode.Bottom);
				ToolTipService.SetHorizontalOffset(myIconButton2, -10.0);
				ToolTipService.SetVerticalOffset(myIconButton2, 5.0);
				ToolTipService.SetInitialShowDelay(myIconButton2, 200);
				myListItem.Buttons = new MyIconButton[]
				{
					myIconButton,
					myIconButton2
				};
				this.RefreshUi(myListItem);
				return myListItem;
			}

			// Token: 0x06000A98 RID: 2712 RVA: 0x000076DB File Offset: 0x000058DB
			public void RefreshUi(MyListItem RelatedListItem)
			{
				RelatedListItem.Title = this._ConfigurationTest;
				RelatedListItem.Info = this.GetDescription();
				Enumerable.ElementAtOrDefault<MyIconButton>(RelatedListItem.Buttons, 0).Visibility = ((this._ExporterTest == 1.0) ? Visibility.Visible : Visibility.Collapsed);
			}

			// Token: 0x06000A99 RID: 2713 RVA: 0x0004B524 File Offset: 0x00049724
			protected override void Dispose(bool IsDisposing)
			{
				ModBase.Log("[IOI] 用户资源释放（IOI, " + this._ConfigurationTest + "）", ModBase.LogLevel.Normal, "出现错误");
				if (this.importerTest != null && this.importerTest.IsAlive)
				{
					this.importerTest.Interrupt();
				}
				PageLinkIoi._IteratorConfig.Remove(this._ConsumerTest);
				base.Dispose(IsDisposing);
			}

			// Token: 0x04000577 RID: 1399
			public Dictionary<int, string> m_SetterTest;

			// Token: 0x04000578 RID: 1400
			public List<PageLinkIoi.RoomEntry> _FactoryTest;

			// Token: 0x04000579 RID: 1401
			public double _ExporterTest;

			// Token: 0x0400057A RID: 1402
			public Thread importerTest;
		}

		// Token: 0x02000108 RID: 264
		public class RoomEntry
		{
			// Token: 0x06000A9F RID: 2719 RVA: 0x0004B58C File Offset: 0x0004978C
			public string ValidateMapper()
			{
				string result;
				if (this.resolverTest)
				{
					result = "localhost:" + Conversions.ToString(this.workerTest);
				}
				else
				{
					result = this.serverTest.m_SetterTest[this.workerTest] + ":" + Conversions.ToString(this.workerTest);
				}
				return result;
			}

			// Token: 0x06000AA0 RID: 2720 RVA: 0x00007746 File Offset: 0x00005946
			public RoomEntry(int Port, string DisplayName, PageLinkIoi.LinkUserIoi User = null)
			{
				this.serverTest = null;
				this.resolverTest = (User == null);
				this.serverTest = User;
				this.connectionTest = DisplayName;
				this.workerTest = Port;
			}

			// Token: 0x06000AA1 RID: 2721 RVA: 0x0004B5E8 File Offset: 0x000497E8
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					this.connectionTest,
					" - ",
					Conversions.ToString(this.workerTest),
					" - ",
					Conversions.ToString(this.resolverTest)
				});
			}

			// Token: 0x06000AA2 RID: 2722 RVA: 0x00007775 File Offset: 0x00005975
			public static implicit operator string(PageLinkIoi.RoomEntry Room)
			{
				return Room.ToString();
			}

			// Token: 0x06000AA3 RID: 2723 RVA: 0x0000777D File Offset: 0x0000597D
			public static int SelectPort(PageLinkIoi.RoomEntry Room)
			{
				return Room.workerTest;
			}

			// Token: 0x06000AA4 RID: 2724 RVA: 0x0004B638 File Offset: 0x00049838
			public string GetDescription()
			{
				string result;
				if (this.resolverTest)
				{
					result = "由我创建，端口 " + Conversions.ToString(this.workerTest);
				}
				else
				{
					result = "由 " + this.serverTest._ConfigurationTest + " 创建，端口 " + Conversions.ToString(this.workerTest);
				}
				return result;
			}

			// Token: 0x06000AA5 RID: 2725 RVA: 0x0004B68C File Offset: 0x0004988C
			public MyListItem ToListItem()
			{
				MyListItem myListItem = new MyListItem
				{
					Title = this.connectionTest,
					Height = 42.0,
					Info = this.GetDescription(),
					Tag = this,
					Type = (this.resolverTest ? MyListItem.CheckType.None : MyListItem.CheckType.Clickable),
					Logo = "pack://application:,,,/images/Blocks/" + (this.resolverTest ? "GrassPath" : "Grass") + ".png"
				};
				if (this.resolverTest)
				{
					MyIconButton myIconButton = new MyIconButton
					{
						Logo = "M732.64 64.32C688.576 21.216 613.696 21.216 569.6 64.32L120.128 499.52c-17.6 12.896-26.432 30.144-30.848 51.68L32 870.048c0 25.856 8.8 56 26.432 73.248 17.632 17.216 17.632 48.704 88.64 48.704h13.248l326.08-56c22.016-4.32 39.68-12.928 52.864-30.176l449.472-435.2c22.048-21.536 35.264-47.36 35.264-77.536 0-30.176-13.216-56-35.264-77.568l-256.096-251.2zM139.712 903.776l56-326.912 311.04-295.136 267.104 269.44-310.976 295.168-323.168 57.44zM844.576 467.84l-273.984-260.672 61.856-59.84c8.832-8.512 26.528-8.512 39.776 0l234.24 226.496c4.384 4.288 8.832 12.8 8.832 17.088s-4.416 8.544-8.864 12.8l-61.856 64.128z",
						LogoScale = 1.0,
						ToolTip = "修改名称",
						Tag = this
					};
					myIconButton.Click += ((PageLinkIoi.RoomEntry._Closure$__.$IR11-1 == null) ? (PageLinkIoi.RoomEntry._Closure$__.$IR11-1 = delegate(object sender, EventArgs e)
					{
						PageLinkIoi.BtnRoomEdit_Click((MyIconButton)sender, e);
					}) : PageLinkIoi.RoomEntry._Closure$__.$IR11-1);
					ToolTipService.SetPlacement(myIconButton, PlacementMode.Bottom);
					ToolTipService.SetHorizontalOffset(myIconButton, -22.0);
					ToolTipService.SetVerticalOffset(myIconButton, 5.0);
					ToolTipService.SetInitialShowDelay(myIconButton, 200);
					MyIconButton myIconButton2 = new MyIconButton
					{
						Logo = "F1 M 26.9166,22.1667L 37.9999,33.25L 49.0832,22.1668L 53.8332,26.9168L 42.7499,38L 53.8332,49.0834L 49.0833,53.8334L 37.9999,42.75L 26.9166,53.8334L 22.1666,49.0833L 33.25,38L 22.1667,26.9167L 26.9166,22.1667 Z",
						LogoScale = 0.85,
						ToolTip = "关闭",
						Tag = this
					};
					myIconButton2.Click += ((PageLinkIoi.RoomEntry._Closure$__.$IR11-2 == null) ? (PageLinkIoi.RoomEntry._Closure$__.$IR11-2 = delegate(object sender, EventArgs e)
					{
						PageLinkIoi.BtnRoomClose_Click((MyIconButton)sender, e);
					}) : PageLinkIoi.RoomEntry._Closure$__.$IR11-2);
					ToolTipService.SetPlacement(myIconButton2, PlacementMode.Bottom);
					ToolTipService.SetHorizontalOffset(myIconButton2, -10.0);
					ToolTipService.SetVerticalOffset(myIconButton2, 5.0);
					ToolTipService.SetInitialShowDelay(myIconButton2, 200);
					myListItem.Buttons = new MyIconButton[]
					{
						myIconButton,
						myIconButton2
					};
				}
				else
				{
					myListItem.Click += ((PageLinkIoi.RoomEntry._Closure$__.$IR11-3 == null) ? (PageLinkIoi.RoomEntry._Closure$__.$IR11-3 = delegate(object sender, MouseButtonEventArgs e)
					{
						PageLinkIoi.BtnRoom_Click((MyListItem)sender, e);
					}) : PageLinkIoi.RoomEntry._Closure$__.$IR11-3);
				}
				return myListItem;
			}

			// Token: 0x06000AA6 RID: 2726 RVA: 0x00007785 File Offset: 0x00005985
			public void RefreshUi(MyListItem RelatedListItem)
			{
				RelatedListItem.Title = this.connectionTest;
				RelatedListItem.Info = this.GetDescription();
			}

			// Token: 0x0400057E RID: 1406
			public int workerTest;

			// Token: 0x0400057F RID: 1407
			public string connectionTest;

			// Token: 0x04000580 RID: 1408
			public PageLinkIoi.LinkUserIoi serverTest;

			// Token: 0x04000581 RID: 1409
			public bool resolverTest;
		}
	}
}
