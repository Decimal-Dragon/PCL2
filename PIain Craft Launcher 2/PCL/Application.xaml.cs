using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Threading;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PCL.My;

namespace PCL
{
	// Token: 0x020001C0 RID: 448
	public partial class Application : Application, IStyleConnector
	{
		// Token: 0x060014B9 RID: 5305 RVA: 0x0008AADC File Offset: 0x00088CDC
		public Application()
		{
			base.Startup += this.Application_Startup;
			base.SessionEnding += this.Application_SessionEnding;
			base.DispatcherUnhandledException += this.Application_DispatcherUnhandledException;
			this.m_RepositoryComposer = false;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0008AB2C File Offset: 0x00088D2C
		private void Application_Startup(object sender, System.Windows.StartupEventArgs e)
		{
			checked
			{
				try
				{
					ModSecret.SecretOnApplicationStart();
					if (e.Args.Length > 0)
					{
						if (Operators.CompareString(e.Args[0], "--update", false) == 0)
						{
							ModSecret.UpdateReplace(Conversions.ToInteger(e.Args[1]), e.Args[2].Trim(new char[]
							{
								'"'
							}), e.Args[3].Trim(new char[]
							{
								'"'
							}), Conversions.ToBoolean(e.Args[4]));
							Environment.Exit(4);
						}
						else if (e.Args[0].StartsWithF("--memory", false))
						{
							ulong availablePhysicalMemory = MyWpfExtension.ManageParser().Info.AvailablePhysicalMemory;
							try
							{
								PageOtherTest.MemoryOptimizeInternal(false);
							}
							catch (Exception ex)
							{
								Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, "内存优化失败");
								Environment.Exit(-1);
							}
							if (MyWpfExtension.ManageParser().Info.AvailablePhysicalMemory < availablePhysicalMemory)
							{
								Environment.Exit(0);
							}
							else
							{
								Environment.Exit((int)Math.Round((MyWpfExtension.ManageParser().Info.AvailablePhysicalMemory - availablePhysicalMemory) / 1024.0));
							}
						}
					}
					Directory.CreateDirectory(ModBase.Path + "PCL\\Pictures");
					Directory.CreateDirectory(ModBase.Path + "PCL\\Musics");
					try
					{
						Directory.CreateDirectory(ModBase.m_DecoratorRepository);
						if (!ModBase.CheckPermission(ModBase.m_DecoratorRepository))
						{
							throw new Exception("PCL 没有对 " + ModBase.m_DecoratorRepository + " 的访问权限");
						}
					}
					catch (Exception ex2)
					{
						if (Operators.CompareString(ModBase.m_DecoratorRepository, Path.GetTempPath() + "PCL\\", false) == 0)
						{
							ModMain.MyMsgBox("PCL 无法访问缓存文件夹，可能导致程序出错或无法正常使用！\r\n错误原因：" + ModBase.GetExceptionDetail(ex2, false), "缓存文件夹不可用", "确定", "", "", false, true, false, null, null, null);
						}
						else
						{
							ModMain.MyMsgBox("手动设置的缓存文件夹不可用，PCL 将使用默认缓存文件夹。\r\n错误原因：" + ModBase.GetExceptionDetail(ex2, false), "缓存文件夹不可用", "确定", "", "", false, true, false, null, null, null);
							ModBase.m_IdentifierRepository.Set("SystemSystemCache", "", false, null);
							ModBase.m_DecoratorRepository = Path.GetTempPath() + "PCL\\";
						}
					}
					Directory.CreateDirectory(ModBase.m_DecoratorRepository + "Cache");
					Directory.CreateDirectory(ModBase.m_DecoratorRepository + "Download");
					Directory.CreateDirectory(ModBase.m_InstanceRepository);
					bool flag = e.Args.Length > 0 && Operators.CompareString(e.Args[0], "--wait", false) == 0;
					int num = 0;
					IntPtr intPtr;
					for (;;)
					{
						string text = null;
						string text2 = "Plain Craft Launcher\u3000";
						intPtr = ModMain.FindWindowA(ref text, ref text2);
						if (intPtr == IntPtr.Zero)
						{
							text2 = null;
							text = "Plain Craft Launcher 2\u3000";
							ModMain.FindWindowA(ref text2, ref text);
						}
						if (!(intPtr != IntPtr.Zero))
						{
							goto IL_306;
						}
						if (!flag || num >= 20)
						{
							break;
						}
						num++;
						Thread.Sleep(500);
					}
					ModMain.ShowWindowToTop(intPtr);
					Interaction.Beep();
					Environment.Exit(4);
					IL_306:
					ToolTipService.InitialShowDelayProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(300));
					ToolTipService.BetweenShowDelayProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(400));
					ToolTipService.ShowDurationProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(9999999));
					ToolTipService.PlacementProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(PlacementMode.Bottom));
					ToolTipService.HorizontalOffsetProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(8.0));
					ToolTipService.VerticalOffsetProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(4.0));
					if (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiLauncherLogo", null)))
					{
						ModMain.m_ParameterIterator = new SplashScreen("Images\\icon.ico");
						ModMain.m_ParameterIterator.Show(false, true);
					}
					AppDomain.CurrentDomain.AssemblyResolve += Application.AssemblyResolve;
					ModBase.LogStart();
					ModBase.Log(string.Format("[Start] 程序版本：{0} ({1}{2})", "Release 2.8.12", 347, ""), ModBase.LogLevel.Normal, "出现错误");
					ModBase.Log(string.Format("[Start] 识别码：{0}{1}", ModBase._TagRepository, ModSecret.InsertReader(9) ? "，已解锁反馈主题" : ""), ModBase.LogLevel.Normal, "出现错误");
					ModBase.Log(string.Format("[Start] 程序路径：{0}", ModBase.interpreterRepository), ModBase.LogLevel.Normal, "出现错误");
					ModBase.Log(string.Format("[Start] 系统编码：{0} ({1}, GBK={2})", Encoding.Default.HeaderName, Encoding.Default.CodePage, ModBase.rulesRepository), ModBase.LogLevel.Normal, "出现错误");
					ModBase.Log(string.Format("[Start] 管理员权限：{0}", ModBase.IsAdmin()), ModBase.LogLevel.Normal, "出现错误");
					if (ModBase.Path.Contains(Path.GetTempPath()) || ModBase.Path.Contains("AppData\\Local\\Temp\\"))
					{
						ModMain.MyMsgBox("请将 PCL 从压缩包中解压之后再使用！\r\n在当前环境下运行可能会导致丢失游戏存档或设置，部分功能也可能无法使用！", "环境警告", "我知道了", "", "", true, true, false, null, null, null);
					}
					if (ModBase.m_StubRepository)
					{
						ModMain.MyMsgBox("PCL 和新版 Minecraft 均不再支持 32 位系统，部分功能将无法使用。\r\n非常建议重装为 64 位系统后再进行游戏！", "环境警告", "我知道了", "", "", true, true, false, null, null, null);
					}
					ModBase.m_IdentifierRepository.Load("SystemDebugMode", false, null);
					ModBase.m_IdentifierRepository.Load("SystemDebugAnim", false, null);
					ModBase.m_IdentifierRepository.Load("ToolDownloadThread", false, null);
					ModBase.m_IdentifierRepository.Load("ToolDownloadCert", false, null);
					ServicePointManager.Expect100Continue = true;
					ServicePointManager.SecurityProtocol = (SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12);
					ServicePointManager.DefaultConnectionLimit = 1024;
					ModBase.Log("[Start] 第一阶段加载用时：" + Conversions.ToString(ModBase.GetTimeTick() - ModBase._SystemRepository) + " ms", ModBase.LogLevel.Normal, "出现错误");
					ModBase._SystemRepository = ModBase.GetTimeTick();
					ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
				}
				catch (Exception ex3)
				{
					string text3 = null;
					try
					{
						text3 = ModBase.interpreterRepository;
					}
					catch (Exception ex4)
					{
					}
					Interaction.MsgBox(ModBase.GetExceptionDetail(ex3, true) + "\r\nPCL 所在路径：" + (string.IsNullOrEmpty(text3) ? "获取失败" : text3), MsgBoxStyle.Critical, "PCL 初始化错误");
					FormMain.EndProgramForce(ModBase.Result.Exception);
				}
			}
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00005034 File Offset: 0x00003234
		private void Application_SessionEnding(object sender, SessionEndingCancelEventArgs e)
		{
			ModMain._ProcessIterator.EndProgram(false);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0008B20C File Offset: 0x0008940C
		private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
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
				e.Handled = true;
				IL_10:
				num2 = 3;
				if (ModBase._ObserverRepository)
				{
					goto IL_D1;
				}
				IL_1C:
				num2 = 5;
				if (!this.m_RepositoryComposer)
				{
					goto IL_33;
				}
				IL_26:
				num2 = 6;
				FormMain.EndProgramForce(ModBase.Result.Exception);
				goto IL_D1;
				IL_33:
				num2 = 8;
				this.m_RepositoryComposer = true;
				IL_3C:
				num2 = 9;
				string exceptionDetail = ModBase.GetExceptionDetail(e.Exception, true);
				IL_4C:
				num2 = 10;
				if (exceptionDetail.Contains("System.Windows.Threading.Dispatcher.Invoke") || exceptionDetail.Contains("MS.Internal.AppModel.ITaskbarList.HrInit") || exceptionDetail.Contains(".NET Framework") || exceptionDetail.Contains("未能加载文件或程序集"))
				{
					goto IL_A6;
				}
				IL_83:
				num2 = 15;
				ModBase.FeedbackInfo();
				IL_8B:
				num2 = 16;
				ModBase.Log(e.Exception, "程序出现未知错误", ModBase.LogLevel.Assert, "锟斤拷烫烫烫");
				goto IL_D1;
				IL_A6:
				num2 = 11;
				ModBase.OpenWebsite("https://dotnet.microsoft.com/zh-cn/download/dotnet-framework/thank-you/net462-offline-installer");
				IL_B3:
				num2 = 12;
				Interaction.MsgBox("你的 .NET Framework 版本过低或损坏，请下载并重新安装 .NET Framework 4.6.2！", MsgBoxStyle.Information, "运行环境错误");
				IL_C8:
				num2 = 13;
				FormMain.EndProgramForce(ModBase.Result.Cancel);
				IL_D1:
				goto IL_167;
				IL_D6:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_128:
				goto IL_15C;
				IL_12A:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_13A:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_12A;
			}
			IL_15C:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_167:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x060014BD RID: 5309
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern bool SetDllDirectoryA([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpPathName);

		// Token: 0x060014BE RID: 5310 RVA: 0x0008B3A4 File Offset: 0x000895A4
		public static Assembly AssemblyResolve(object sender, ResolveEventArgs args)
		{
			if (args.Name.StartsWithF("NAudio", false))
			{
				object obj = Application.specificationComposer;
				ObjectFlowControl.CheckForSyncLockOnValueType(obj);
				lock (obj)
				{
					if (Application._TestComposer == null)
					{
						ModBase.Log("[Start] 加载 DLL：NAudio", ModBase.LogLevel.Normal, "出现错误");
						Application._TestComposer = Assembly.Load(ModBase.GetResources("NAudio"));
					}
					return Application._TestComposer;
				}
			}
			if (args.Name.StartsWithF("Newtonsoft.Json", false))
			{
				object mockComposer = Application.m_MockComposer;
				ObjectFlowControl.CheckForSyncLockOnValueType(mockComposer);
				lock (mockComposer)
				{
					if (Application.mapComposer == null)
					{
						ModBase.Log("[Start] 加载 DLL：Json", ModBase.LogLevel.Normal, "出现错误");
						Application.mapComposer = Assembly.Load(ModBase.GetResources("Json"));
					}
					return Application.mapComposer;
				}
			}
			if (args.Name.StartsWithF("Ookii.Dialogs.Wpf", false))
			{
				object requestComposer = Application._RequestComposer;
				ObjectFlowControl.CheckForSyncLockOnValueType(requestComposer);
				lock (requestComposer)
				{
					if (Application.errorComposer == null)
					{
						ModBase.Log("[Start] 加载 DLL：Dialogs", ModBase.LogLevel.Normal, "出现错误");
						Application.errorComposer = Assembly.Load(ModBase.GetResources("Dialogs"));
					}
					return Application.errorComposer;
				}
			}
			if (args.Name.StartsWithF("Imazen.WebP", false))
			{
				object obj2 = Application.dicComposer;
				ObjectFlowControl.CheckForSyncLockOnValueType(obj2);
				lock (obj2)
				{
					if (Application._ContextComposer == null)
					{
						ModBase.Log("[Start] 加载 DLL：Imazen.WebP", ModBase.LogLevel.Normal, "出现错误");
						Application._ContextComposer = Assembly.Load(ModBase.GetResources("Imazen_WebP"));
						string text = ModBase._MethodRepository.TrimEnd(new char[]
						{
							'\\'
						});
						Application.SetDllDirectoryA(ref text);
						ModBase.WriteFile(ModBase._MethodRepository + "libwebp.dll", ModBase.GetResources("libwebp64"), false);
					}
					return Application._ContextComposer;
				}
			}
			return null;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0008B5D8 File Offset: 0x000897D8
		private void MyIconButton_Click(object sender, EventArgs e)
		{
			object left = ModBase.m_IdentifierRepository.Get("LoginType", null);
			if (Operators.ConditionalCompareObjectEqual(left, ModLaunch.McLoginType.Ms, false))
			{
				JObject jobject = (JObject)ModBase.GetJson(Conversions.ToString(ModBase.m_IdentifierRepository.Get("LoginMsJson", null)));
				jobject.Remove(Conversions.ToString(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)));
				ModBase.m_IdentifierRepository.Set("LoginMsJson", jobject.ToString(0, new JsonConverter[0]), false, null);
				if (ModMain.m_ClientRepository.ComboAccounts.SelectedItem == NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null))
				{
					ModMain.m_ClientRepository.ComboAccounts.SelectedIndex = 0;
				}
				ModMain.m_ClientRepository.ComboAccounts.Items.Remove(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null)));
				return;
			}
			if (Operators.ConditionalCompareObjectEqual(left, ModLaunch.McLoginType.Legacy, false))
			{
				List<string> list = new List<string>();
				list.AddRange(ModBase.m_IdentifierRepository.Get("LoginLegacyName", null).ToString().Split("¨"));
				list.Remove(Conversions.ToString(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)));
				ModBase.m_IdentifierRepository.Set("LoginLegacyName", list.Join("¨"), false, null);
				ModMain.m_ProcIterator.ComboName.ItemsSource = list;
				ModMain.m_ProcIterator.ComboName.Text = (Enumerable.Any<string>(list) ? list[0] : "");
				return;
			}
			string stringFromEnum = ModBase.GetStringFromEnum((Enum)ModBase.m_IdentifierRepository.Get("LoginType", null));
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			List<string> list2 = new List<string>();
			List<string> list3 = new List<string>();
			if (Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(ModBase.m_IdentifierRepository.Get("Login" + stringFromEnum + "Email", null), "", false))))
			{
				list2.AddRange(ModBase.m_IdentifierRepository.Get("Login" + stringFromEnum + "Email", null).ToString().Split("¨"));
			}
			if (Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(ModBase.m_IdentifierRepository.Get("Login" + stringFromEnum + "Pass", null), "", false))))
			{
				list3.AddRange(ModBase.m_IdentifierRepository.Get("Login" + stringFromEnum + "Pass", null).ToString().Split("¨"));
			}
			checked
			{
				int num = list2.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					dictionary.Add(list2[i], list3[i]);
				}
				dictionary.Remove(Conversions.ToString(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)));
				ModBase.m_IdentifierRepository.Set("Login" + stringFromEnum + "Email", dictionary.Keys.Join("¨"), false, null);
				ModBase.m_IdentifierRepository.Set("Login" + stringFromEnum + "Pass", dictionary.Values.Join("¨"), false, null);
				if (Operators.CompareString(stringFromEnum, "Nide", false) == 0)
				{
					ModMain.parserRepository.ComboName.ItemsSource = dictionary.Keys;
					ModMain.parserRepository.ComboName.Text = (Enumerable.Any<string>(dictionary.Keys) ? Enumerable.ElementAtOrDefault<string>(dictionary.Keys, 0) : "");
					ModMain.parserRepository.TextPass.Password = (Enumerable.Any<string>(dictionary.Values) ? Enumerable.ElementAtOrDefault<string>(dictionary.Values, 0) : "");
					return;
				}
				if (Operators.CompareString(stringFromEnum, "Auth", false) != 0)
				{
					return;
				}
				ModMain.fieldRepository.ComboName.ItemsSource = dictionary.Keys;
				ModMain.fieldRepository.ComboName.Text = (Enumerable.Any<string>(dictionary.Keys) ? Enumerable.ElementAtOrDefault<string>(dictionary.Keys, 0) : "");
				ModMain.fieldRepository.TextPass.Password = (Enumerable.Any<string>(dictionary.Values) ? Enumerable.ElementAtOrDefault<string>(dictionary.Values, 0) : "");
			}
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0000BC43 File Offset: 0x00009E43
		private void TooltipLoaded(Border sender, EventArgs e)
		{
			Application.m_HelperComposer.Add(sender);
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0000BC50 File Offset: 0x00009E50
		private void TooltipUnloaded(Border sender, RoutedEventArgs e)
		{
			Application.m_HelperComposer.Remove(sender);
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x0000BC5E File Offset: 0x00009E5E
		internal AssemblyInfo Info
		{
			get
			{
				return new AssemblyInfo(Assembly.GetExecutingAssembly());
			}
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0008BA74 File Offset: 0x00089C74
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IStyleConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				((Border)target).Loaded += delegate(object sender, RoutedEventArgs e)
				{
					this.TooltipLoaded((Border)sender, e);
				};
				((Border)target).Unloaded += delegate(object sender, RoutedEventArgs e)
				{
					this.TooltipUnloaded((Border)sender, e);
				};
			}
			if (connectionId == 2)
			{
				((Border)target).Loaded += delegate(object sender, RoutedEventArgs e)
				{
					this.TooltipLoaded((Border)sender, e);
				};
				((Border)target).Unloaded += delegate(object sender, RoutedEventArgs e)
				{
					this.TooltipUnloaded((Border)sender, e);
				};
			}
		}

		// Token: 0x04000AA0 RID: 2720
		private bool m_RepositoryComposer;

		// Token: 0x04000AA1 RID: 2721
		private static Assembly _TestComposer;

		// Token: 0x04000AA2 RID: 2722
		private static Assembly mapComposer;

		// Token: 0x04000AA3 RID: 2723
		private static Assembly errorComposer;

		// Token: 0x04000AA4 RID: 2724
		private static Assembly _ContextComposer;

		// Token: 0x04000AA5 RID: 2725
		private static readonly object specificationComposer = RuntimeHelpers.GetObjectValue(new object());

		// Token: 0x04000AA6 RID: 2726
		private static readonly object m_MockComposer = RuntimeHelpers.GetObjectValue(new object());

		// Token: 0x04000AA7 RID: 2727
		private static readonly object _RequestComposer = RuntimeHelpers.GetObjectValue(new object());

		// Token: 0x04000AA8 RID: 2728
		private static readonly object dicComposer = RuntimeHelpers.GetObjectValue(new object());

		// Token: 0x04000AA9 RID: 2729
		public static List<Border> m_HelperComposer = new List<Border>();
	}
}
