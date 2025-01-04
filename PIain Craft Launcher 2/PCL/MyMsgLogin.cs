using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace PCL
{
	// Token: 0x020000EF RID: 239
	[DesignerGenerated]
	public class MyMsgLogin : Grid, IComponentConnector
	{
		// Token: 0x060008EA RID: 2282 RVA: 0x00046CE8 File Offset: 0x00044EE8
		public MyMsgLogin(ModMain.MyMsgBoxConverter Converter)
		{
			base.Loaded += new RoutedEventHandler(this.Load);
			this._RequestClient = ModBase.GetUuid();
			try
			{
				this.InitializeComponent();
				this.Btn1.Name = this.Btn1.Name + Conversions.ToString(ModBase.GetUuid());
				this.Btn2.Name = this.Btn2.Name + Conversions.ToString(ModBase.GetUuid());
				this.Btn3.Name = this.Btn3.Name + Conversions.ToString(ModBase.GetUuid());
				this.m_MockClient = Converter;
				this.ShapeLine.StrokeThickness = ModBase.smethod_4(1.0);
				this.m_MapClient = (JObject)Converter.m_CollectionMap;
				this.Init();
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "登录弹窗初始化失败", ModBase.LogLevel.Hint, "出现错误");
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00046DF8 File Offset: 0x00044FF8
		private void Load(object sender, EventArgs e)
		{
			try
			{
				base.Opacity = 0.0;
				ModAnimation.AniStart(ModAnimation.AaColor(ModMain._ProcessIterator.PanMsg, Panel.BackgroundProperty, (this.m_MockClient._ClassMap ? new ModBase.MyColor(140.0, 80.0, 0.0, 0.0) : new ModBase.MyColor(90.0, 0.0, 0.0, 0.0)) - ModMain._ProcessIterator.PanMsg.Background, 200, 0, null, false), "PanMsg Background", false);
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaOpacity(this, 1.0, 120, 60, null, false),
					ModAnimation.AaDouble(delegate(object i)
					{
						TranslateTransform transformPos;
						(transformPos = this.TransformPos).Y = Conversions.ToDouble(Operators.AddObject(transformPos.Y, i));
					}, -this.TransformPos.Y, 300, 60, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false),
					ModAnimation.AaDouble(delegate(object i)
					{
						RotateTransform transformRotate;
						(transformRotate = this.TransformRotate).Angle = Conversions.ToDouble(Operators.AddObject(transformRotate.Angle, i));
					}, -this.TransformRotate.Angle, 300, 60, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false)
				}, "MyMsgBox " + Conversions.ToString(this._RequestClient), false);
				ModBase.Log("[Control] 登录弹窗：" + this.LabTitle.Text + "\r\n" + this.LabCaption.Text, ModBase.LogLevel.Normal, "出现错误");
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "登录弹窗加载失败", ModBase.LogLevel.Hint, "出现错误");
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00046FC4 File Offset: 0x000451C4
		private void Close()
		{
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaCode((MyMsgLogin._Closure$__.$I8-0 == null) ? (MyMsgLogin._Closure$__.$I8-0 = delegate()
				{
					if (!Enumerable.Any<ModMain.MyMsgBoxConverter>(ModMain.m_DispatcherIterator))
					{
						ModAnimation.AniStart(ModAnimation.AaColor(ModMain._ProcessIterator.PanMsg, Panel.BackgroundProperty, new ModBase.MyColor(0.0, 0.0, 0.0, 0.0) - ModMain._ProcessIterator.PanMsg.Background, 200, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false), "", false);
					}
				}) : MyMsgLogin._Closure$__.$I8-0, 30, false),
				ModAnimation.AaOpacity(this, -base.Opacity, 80, 20, null, false),
				ModAnimation.AaDouble(delegate(object i)
				{
					TranslateTransform transformPos;
					(transformPos = this.TransformPos).Y = Conversions.ToDouble(Operators.AddObject(transformPos.Y, i));
				}, 20.0 - this.TransformPos.Y, 150, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
				ModAnimation.AaDouble(delegate(object i)
				{
					RotateTransform transformRotate;
					(transformRotate = this.TransformRotate).Angle = Conversions.ToDouble(Operators.AddObject(transformRotate.Angle, i));
				}, 6.0 - this.TransformRotate.Angle, 150, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak), false),
				ModAnimation.AaCode(delegate
				{
					((Grid)base.Parent).Children.Remove(this);
				}, 0, true)
			}, "MyMsgBox " + Conversions.ToString(this._RequestClient), false);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x000063A1 File Offset: 0x000045A1
		public void Btn1_Click()
		{
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00006942 File Offset: 0x00004B42
		public void Btn3_Click()
		{
			this.Finished(new ThreadInterruptedException());
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x000470D0 File Offset: 0x000452D0
		private void Drag(object sender, MouseButtonEventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				if (e.GetPosition(this.ShapeLine).Y > 2.0)
				{
					goto IL_34;
				}
				IL_28:
				num2 = 3;
				ModMain._ProcessIterator.DragMove();
				IL_34:
				goto IL_93;
				IL_36:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_54:
				goto IL_88;
				IL_56:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_66:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_56;
			}
			IL_88:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_93:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00047188 File Offset: 0x00045388
		private void Finished(object Result)
		{
			if (!this.m_MockClient.m_ProducerMap)
			{
				this.m_MockClient.m_ProducerMap = true;
				this.m_MockClient.schemaMap = RuntimeHelpers.GetObjectValue(Result);
				ModBase.RunInUi(new Action(this.Close), false);
				Thread.Sleep(200);
				ModMain._ProcessIterator.ShowWindowToTop();
			}
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x000471E8 File Offset: 0x000453E8
		private void Init()
		{
			this.errorClient = (string)this.m_MapClient["user_code"];
			this.m_ContextClient = (string)this.m_MapClient["device_code"];
			this._SpecificationClient = (string)this.m_MapClient["verification_uri"];
			this.LabTitle.Text = "登录 Minecraft";
			this.LabCaption.Text = string.Format("登录网页将自动开启，请在网页中输入 {0}（已自动复制）。", this.errorClient) + "\r\n\r\n如果网络环境不佳，网页可能一直加载不出来，届时请使用 VPN 并重试。\r\n" + string.Format("你也可以用其他设备打开 {0} 并输入上述代码。", this._SpecificationClient);
			this.Btn1.EventData = this._SpecificationClient;
			this.Btn2.EventData = this.errorClient;
			ModBase.RunInNewThread(new Action(this.WorkThread), "MyMsgLogin", ThreadPriority.Normal);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x000472C8 File Offset: 0x000454C8
		private void WorkThread()
		{
			Thread.Sleep(3000);
			checked
			{
				if (!this.m_MockClient.m_ProducerMap)
				{
					ModBase.OpenWebsite(this._SpecificationClient);
					ModBase.ClipboardSet(this.errorClient, true);
					Thread.Sleep((this.m_MapClient["interval"].ToObject<int>() - 1) * 1000);
					int num = 0;
					while (!this.m_MockClient.m_ProducerMap)
					{
						try
						{
							JObject jobject = (JObject)ModBase.GetJson(ModNet.NetRequestOnce("https://login.microsoftonline.com/consumers/oauth2/v2.0/token", "POST", "grant_type=urn:ietf:params:oauth:grant-type:device_code&client_id=fe72edc2-3a6f-4280-90e8-e2beb64ce7e1&device_code=" + this.m_ContextClient + "&scope=XboxLive.signin%20offline_access", "application/x-www-form-urlencoded", 5000 + num * 5000, null, false, false));
							ModLaunch.McLaunchLog(string.Format("令牌过期时间：{0} 秒", jobject["expires_in"]));
							ModMain.Hint("网页登录成功！", ModMain.HintType.Finish, true);
							this.Finished(new string[]
							{
								jobject["access_token"].ToString(),
								jobject["refresh_token"].ToString()
							});
							break;
						}
						catch (Exception ex)
						{
							if (ex.Message.Contains("authorization_declined"))
							{
								this.Finished(new Exception("$你拒绝了 PCL 申请的权限……"));
								break;
							}
							if (ex.Message.Contains("expired_token"))
							{
								this.Finished(new Exception("$登录用时太长啦，重新试试吧！"));
								break;
							}
							if (ex.Message.Contains("AADSTS70000"))
							{
								this.Finished(new ModBase.RestartException());
								break;
							}
							if (ex.Message.Contains("authorization_pending"))
							{
								Thread.Sleep(2000);
							}
							else
							{
								if (num > 2)
								{
									this.Finished(new Exception("登录轮询失败", ex));
									break;
								}
								num++;
								ModBase.Log(ex, string.Format("登录轮询第 {0} 次失败", num), ModBase.LogLevel.Debug, "出现错误");
								Thread.Sleep(2000);
							}
						}
					}
				}
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0000694F File Offset: 0x00004B4F
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x00006957 File Offset: 0x00004B57
		internal virtual RotateTransform TransformRotate { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00006960 File Offset: 0x00004B60
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x00006968 File Offset: 0x00004B68
		internal virtual TranslateTransform TransformPos { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00006971 File Offset: 0x00004B71
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x000474E0 File Offset: 0x000456E0
		internal virtual Border PanBorder
		{
			[CompilerGenerated]
			get
			{
				return this.issuerClient;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.Drag);
				Border border = this.issuerClient;
				if (border != null)
				{
					border.MouseLeftButtonDown -= value2;
				}
				this.issuerClient = value;
				border = this.issuerClient;
				if (border != null)
				{
					border.MouseLeftButtonDown += value2;
				}
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00006979 File Offset: 0x00004B79
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x00006981 File Offset: 0x00004B81
		internal virtual DropShadowEffect EffectShadow { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0000698A File Offset: 0x00004B8A
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x00006992 File Offset: 0x00004B92
		internal virtual Grid PanMain { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0000699B File Offset: 0x00004B9B
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x00047524 File Offset: 0x00045724
		internal virtual TextBlock LabTitle
		{
			[CompilerGenerated]
			get
			{
				return this.m_SerializerClient;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.Drag);
				TextBlock serializerClient = this.m_SerializerClient;
				if (serializerClient != null)
				{
					serializerClient.MouseLeftButtonDown -= value2;
				}
				this.m_SerializerClient = value;
				serializerClient = this.m_SerializerClient;
				if (serializerClient != null)
				{
					serializerClient.MouseLeftButtonDown += value2;
				}
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x000069A3 File Offset: 0x00004BA3
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x000069AB File Offset: 0x00004BAB
		internal virtual Rectangle ShapeLine { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x000069B4 File Offset: 0x00004BB4
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x000069BC File Offset: 0x00004BBC
		internal virtual MyScrollViewer PanCaption { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x000069C5 File Offset: 0x00004BC5
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x000069CD File Offset: 0x00004BCD
		internal virtual TextBlock LabCaption { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x000069D6 File Offset: 0x00004BD6
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x000069DE File Offset: 0x00004BDE
		internal virtual StackPanel PanBtn { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x000069E7 File Offset: 0x00004BE7
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x00047568 File Offset: 0x00045768
		public virtual MyButton Btn1
		{
			[CompilerGenerated]
			get
			{
				return this.tagClient;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.Btn1_Click();
				};
				MyButton myButton = this.tagClient;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.tagClient = value;
				myButton = this.tagClient;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x000069EF File Offset: 0x00004BEF
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x000069F7 File Offset: 0x00004BF7
		public virtual MyButton Btn2 { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00006A00 File Offset: 0x00004C00
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x000475AC File Offset: 0x000457AC
		public virtual MyButton Btn3
		{
			[CompilerGenerated]
			get
			{
				return this._StubClient;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.Btn3_Click();
				};
				MyButton stubClient = this._StubClient;
				if (stubClient != null)
				{
					stubClient.Click -= value2;
				}
				this._StubClient = value;
				stubClient = this._StubClient;
				if (stubClient != null)
				{
					stubClient.Click += value2;
				}
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x000475F0 File Offset: 0x000457F0
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.rulesClient)
			{
				this.rulesClient = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/mymsglogin.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00047620 File Offset: 0x00045820
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.TransformRotate = (RotateTransform)target;
				return;
			}
			if (connectionId == 2)
			{
				this.TransformPos = (TranslateTransform)target;
				return;
			}
			if (connectionId == 3)
			{
				this.PanBorder = (Border)target;
				return;
			}
			if (connectionId == 4)
			{
				this.EffectShadow = (DropShadowEffect)target;
				return;
			}
			if (connectionId == 5)
			{
				this.PanMain = (Grid)target;
				return;
			}
			if (connectionId == 6)
			{
				this.LabTitle = (TextBlock)target;
				return;
			}
			if (connectionId == 7)
			{
				this.ShapeLine = (Rectangle)target;
				return;
			}
			if (connectionId == 8)
			{
				this.PanCaption = (MyScrollViewer)target;
				return;
			}
			if (connectionId == 9)
			{
				this.LabCaption = (TextBlock)target;
				return;
			}
			if (connectionId == 10)
			{
				this.PanBtn = (StackPanel)target;
				return;
			}
			if (connectionId == 11)
			{
				this.Btn1 = (MyButton)target;
				return;
			}
			if (connectionId == 12)
			{
				this.Btn2 = (MyButton)target;
				return;
			}
			if (connectionId == 13)
			{
				this.Btn3 = (MyButton)target;
				return;
			}
			this.rulesClient = true;
		}

		// Token: 0x040004C6 RID: 1222
		private JObject m_MapClient;

		// Token: 0x040004C7 RID: 1223
		private string errorClient;

		// Token: 0x040004C8 RID: 1224
		private string m_ContextClient;

		// Token: 0x040004C9 RID: 1225
		private string _SpecificationClient;

		// Token: 0x040004CA RID: 1226
		private readonly ModMain.MyMsgBoxConverter m_MockClient;

		// Token: 0x040004CB RID: 1227
		private readonly int _RequestClient;

		// Token: 0x040004CC RID: 1228
		[AccessedThroughProperty("TransformRotate")]
		[CompilerGenerated]
		private RotateTransform _DicClient;

		// Token: 0x040004CD RID: 1229
		[CompilerGenerated]
		[AccessedThroughProperty("TransformPos")]
		private TranslateTransform m_HelperClient;

		// Token: 0x040004CE RID: 1230
		[AccessedThroughProperty("PanBorder")]
		[CompilerGenerated]
		private Border issuerClient;

		// Token: 0x040004CF RID: 1231
		[AccessedThroughProperty("EffectShadow")]
		[CompilerGenerated]
		private DropShadowEffect _IndexerClient;

		// Token: 0x040004D0 RID: 1232
		[CompilerGenerated]
		[AccessedThroughProperty("PanMain")]
		private Grid m_InterpreterClient;

		// Token: 0x040004D1 RID: 1233
		[AccessedThroughProperty("LabTitle")]
		[CompilerGenerated]
		private TextBlock m_SerializerClient;

		// Token: 0x040004D2 RID: 1234
		[CompilerGenerated]
		[AccessedThroughProperty("ShapeLine")]
		private Rectangle _WatcherClient;

		// Token: 0x040004D3 RID: 1235
		[AccessedThroughProperty("PanCaption")]
		[CompilerGenerated]
		private MyScrollViewer _IdentifierClient;

		// Token: 0x040004D4 RID: 1236
		[CompilerGenerated]
		[AccessedThroughProperty("LabCaption")]
		private TextBlock systemClient;

		// Token: 0x040004D5 RID: 1237
		[CompilerGenerated]
		[AccessedThroughProperty("PanBtn")]
		private StackPanel m_ParamClient;

		// Token: 0x040004D6 RID: 1238
		[AccessedThroughProperty("Btn1")]
		[CompilerGenerated]
		private MyButton tagClient;

		// Token: 0x040004D7 RID: 1239
		[AccessedThroughProperty("Btn2")]
		[CompilerGenerated]
		private MyButton _ObserverClient;

		// Token: 0x040004D8 RID: 1240
		[AccessedThroughProperty("Btn3")]
		[CompilerGenerated]
		private MyButton _StubClient;

		// Token: 0x040004D9 RID: 1241
		private bool rulesClient;
	}
}
