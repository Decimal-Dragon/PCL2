using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using PCL.My;

namespace PCL
{
	// Token: 0x020001E6 RID: 486
	[DesignerGenerated]
	public partial class FormMain : Window
	{
		// Token: 0x0600168C RID: 5772 RVA: 0x0009376C File Offset: 0x0009196C
		private void ShowUpdateLog(int LastVersion)
		{
			FormMain._Closure$__1-0 CS$<>8__locals1 = new FormMain._Closure$__1-0(CS$<>8__locals1);
			int num = 0;
			int num2 = 0;
			List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
			checked
			{
				if (LastVersion < 347)
				{
					list.Add(new KeyValuePair<int, string>(4, "Mod 管理页面添加下载 Mod、安装 Mod 选项"));
					list.Add(new KeyValuePair<int, string>(4, "Mod 详情页面支持按加载器、游戏版本进行分类和筛选"));
					list.Add(new KeyValuePair<int, string>(3, "支持安装同时包含 modpack 文件和启动器的懒人包"));
					list.Add(new KeyValuePair<int, string>(1, "优化整合包导入流程"));
					num += 43;
					num2 += 37;
				}
				if (LastVersion < 342)
				{
					list.Add(new KeyValuePair<int, string>(4, "支持下载原版服务端"));
					list.Add(new KeyValuePair<int, string>(3, "本地 Mod 的标题支持选择显示 Mod 原始文件名"));
					list.Add(new KeyValuePair<int, string>(1, "修复搜索后启用/禁用 Mod 时出错的 Bug"));
					num += 17;
					num2 += 13;
				}
				if (LastVersion < 340)
				{
					if (LastVersion == 338)
					{
						list.Add(new KeyValuePair<int, string>(1, "修复数个与新正版登录相关的严重 Bug"));
					}
					num += 3;
					num2 += 7;
				}
				if (LastVersion < 338)
				{
					list.Add(new KeyValuePair<int, string>(4, "使用新的正版登录方式，以提高安全性"));
					list.Add(new KeyValuePair<int, string>(2, "优化安装整合包、检索 Mod 的稳定性"));
					list.Add(new KeyValuePair<int, string>(1, "修复无法加载部分 Mod 的图标的 Bug"));
					list.Add(new KeyValuePair<int, string>(1, "修复在 Mod 管理页面删除 Mod 导致报错的 Bug"));
					num += 11;
					num2 += 21;
				}
				if (LastVersion < 336)
				{
					list.Add(new KeyValuePair<int, string>(4, "下载 Mod 时会使用 MCIM 国内镜像源"));
					list.Add(new KeyValuePair<int, string>(4, "Mod 管理页面允许筛选可更新/启用/禁用的 Mod"));
					list.Add(new KeyValuePair<int, string>(3, "打开 PCL 时会自动安装同目录下的 modpack.zip"));
					list.Add(new KeyValuePair<int, string>(3, "爱发电域名迁移至 afdian.com"));
					list.Add(new KeyValuePair<int, string>(1, "修复 1.20.1+ 离线登录使用正版皮肤时无法保存游戏的 Bug"));
					list.Add(new KeyValuePair<int, string>(1, "修复安装的 1.14~1.15 Forge+OptiFine 无法进入世界的 Bug"));
					num += 19;
					num2 += 24;
				}
				if (LastVersion < 332 && LastVersion == 330)
				{
					list.Add(new KeyValuePair<int, string>(2, "修复部分玩家无法启动 MC 的 Bug"));
				}
				if (LastVersion < 330)
				{
					list.Add(new KeyValuePair<int, string>(5, "NeoForge 兼容与自动安装"));
					list.Add(new KeyValuePair<int, string>(3, "支持编译、运行 PCL 开源代码"));
					num += 15;
					num2 += 22;
				}
				if (LastVersion < 326)
				{
					list.Add(new KeyValuePair<int, string>(2, "会自动隐藏明显不可用的自动安装选项"));
					list.Add(new KeyValuePair<int, string>(2, "优化正版登录流程和 MC 性能"));
					list.Add(new KeyValuePair<int, string>(1, "修复正版登录时弹出脚本错误提示的 Bug"));
					num += 17;
					num2 += 19;
				}
				if (LastVersion < 323)
				{
					list.Add(new KeyValuePair<int, string>(3, "添加 启动游戏前进行内存优化 设置"));
					list.Add(new KeyValuePair<int, string>(2, "优化 MC 性能"));
					list.Add(new KeyValuePair<int, string>(1, "修复安装 OptiFine 有概率失败的 Bug"));
					list.Add(new KeyValuePair<int, string>(1, "修复启动 Fabric 1.20.5+ 时无法正确选择 Java 的 Bug"));
					num += 22;
					num2 += 21;
				}
				if (LastVersion < 321)
				{
					list.Add(new KeyValuePair<int, string>(1, "修复启动部分整合包导致设置丢失的 Bug"));
					num2++;
				}
				if (LastVersion < 319)
				{
					list.Add(new KeyValuePair<int, string>(5, "支持更新 Mod"));
					list.Add(new KeyValuePair<int, string>(3, "支持查看可更新的 Mod 的更新日志"));
					list.Add(new KeyValuePair<int, string>(3, "支持滑动鼠标快速选中、取消选中多个 Mod"));
					list.Add(new KeyValuePair<int, string>(1, "修复无法启动 MC 24w14a+ 的 Bug"));
					num += 10;
					num2 += 10;
				}
				List<string> list2 = new List<string>();
				List<KeyValuePair<int, string>> list3 = list.Sort((FormMain._Closure$__.$I1-0 == null) ? (FormMain._Closure$__.$I1-0 = ((KeyValuePair<int, string> Left, KeyValuePair<int, string> Right) => Left.Key > Right.Key)) : FormMain._Closure$__.$I1-0);
				if (!Enumerable.Any<KeyValuePair<int, string>>(list3) && num == 0 && num2 == 0)
				{
					list2.Add("龙猫忘记写更新日志啦！可以去提醒他一下……");
				}
				int num3 = Math.Min(9, list3.Count - 1);
				for (int i = 0; i <= num3; i++)
				{
					list2.Add(list3[i].Value);
				}
				if (list3.Count > 10)
				{
					num += list3.Count - 10;
				}
				if (num > 0 || num2 > 0)
				{
					list2.Add(((num > 0) ? (Conversions.ToString(num) + " 项小调整与修改") : "") + ((num <= 0 || num2 <= 0) ? "" : "，") + ((num2 > 0) ? ("修复了 " + Conversions.ToString(num2) + " 个 Bug") : "") + "，详见完整更新日志");
				}
				CS$<>8__locals1.$VB$Local_Content = "· " + list2.Join("\r\n· ");
				ModBase.RunInNewThread(delegate
				{
					if (ModMain.MyMsgBox(CS$<>8__locals1.$VB$Local_Content, "PCL 已更新至 Release 2.8.12", "确定", "完整更新日志", "", false, true, false, null, null, null) == 2)
					{
						ModBase.OpenWebsite("https://afdian.com/a/LTCat?tab=feed");
					}
				}, "UpdateLog Output", ThreadPriority.Normal);
			}
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00093BC0 File Offset: 0x00091DC0
		public FormMain()
		{
			base.Loaded += this.FormMain_Loaded;
			base.Closing += new CancelEventHandler(this.FormMain_Closing);
			base.SizeChanged += delegate(object sender, SizeChangedEventArgs e)
			{
				this.FormMain_SizeChanged();
			};
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.FormMain_SizeChanged();
			};
			base.KeyDown += this.FormMain_KeyDown;
			base.MouseDown += this.FormMain_MouseDown;
			base.Activated += delegate(object sender, EventArgs e)
			{
				this.FormMain_Activated();
			};
			base.PreviewDragOver += this.FrmMain_PreviewDragOver;
			base.PreviewDrop += this.FrmMain_Drop;
			base.MouseMove += this.FormMain_MouseMove;
			this.m_DecoratorIterator = false;
			this.m_StateIterator = false;
			this.callbackIterator = false;
			this._TemplateIterator = false;
			this._MethodIterator = FormMain.PageType.Launch;
			this.taskIterator = FormMain.PageType.Launch;
			this._ConsumerIterator = new List<FormMain.PageStackData>();
			this.m_TokenIterator = false;
			this.expressionIterator = null;
			ModBase._SystemRepository = ModBase.GetTimeTick();
			ModMain._ProcessIterator = this;
			ModMain.recordIterator = new PageLaunchLeft();
			ModMain.m_ServiceIterator = new PageLaunchRight();
			int num = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("SystemLastVersionReg", null));
			if (num < 347)
			{
				this.UpgradeSub(num);
			}
			else if (num > 347)
			{
				this.DowngradeSub(num);
			}
			ModSecret.ResetReader(false);
			ModBase.m_IdentifierRepository.Load("UiLauncherTheme", false, null);
			this.InitializeComponent();
			base.Opacity = 0.0;
			if (!Information.IsNothing(ModMain.recordIterator.Parent))
			{
				ModMain.recordIterator.SetValue(ContentPresenter.ContentProperty, null);
			}
			if (!Information.IsNothing(ModMain.m_ServiceIterator.Parent))
			{
				ModMain.m_ServiceIterator.SetValue(ContentPresenter.ContentProperty, null);
			}
			this.PanMainLeft.Child = ModMain.recordIterator;
			this.configurationIterator = ModMain.recordIterator;
			this.PanMainRight.Child = ModMain.m_ServiceIterator;
			this._GetterIterator = ModMain.m_ServiceIterator;
			ModMain.m_ServiceIterator.ComputeBroadcaster(MyPageRight.PageStates.ContentStay);
			if (ModBase._TokenRepository)
			{
				ModMain.Hint("[调试模式] PCL 正以调试模式运行，这可能会导致性能下降，若无必要请不要开启！", ModMain.HintType.Info, true);
			}
			ModMinecraft.m_CreatorTests.Start(0, false);
			ModBase.Log("[Start] 第二阶段加载用时：" + Conversions.ToString(checked(ModBase.GetTimeTick() - ModBase._SystemRepository)) + " ms", ModBase.LogLevel.Normal, "出现错误");
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00093E30 File Offset: 0x00092030
		private void FormMain_Loaded(object sender, RoutedEventArgs e)
		{
			ModBase._SystemRepository = ModBase.GetTimeTick();
			ModBase.m_IndexerRepository = new WindowInteropHelper(this).Handle;
			ModBase.m_IdentifierRepository.Load("UiBackgroundOpacity", false, null);
			ModBase.m_IdentifierRepository.Load("UiBackgroundBlur", false, null);
			ModBase.m_IdentifierRepository.Load("UiLogoType", false, null);
			ModBase.m_IdentifierRepository.Load("UiHiddenPageDownload", false, null);
			PageSetupUI.BackgroundRefresh(false, true);
			ModMusic.MusicRefreshPlay(false, true);
			this.BtnExtraDownload._SpecificationBroadcaster = new MyExtraButton.ShowCheckDelegate(this.BtnExtraDownload_ShowCheck);
			this.BtnExtraBack._SpecificationBroadcaster = new MyExtraButton.ShowCheckDelegate(this.BtnExtraBack_ShowCheck);
			this.BtnExtraApril._SpecificationBroadcaster = new MyExtraButton.ShowCheckDelegate(this.BtnExtraApril_ShowCheck);
			this.BtnExtraShutdown._SpecificationBroadcaster = new MyExtraButton.ShowCheckDelegate(this.BtnExtraShutdown_ShowCheck);
			this.BtnExtraApril.ShowRefresh();
			MyResizer myResizer = new MyResizer(this);
			myResizer.addResizerDown(this.ResizerB);
			myResizer.addResizerLeft(this.ResizerL);
			myResizer.addResizerLeftDown(this.ResizerLB);
			myResizer.addResizerLeftUp(this.ResizerLT);
			myResizer.addResizerRight(this.ResizerR);
			myResizer.addResizerRightDown(this.ResizerRB);
			myResizer.addResizerRightUp(this.ResizerRT);
			myResizer.addResizerUp(this.ResizerT);
			if (ModBase.RandomInteger(1, 1000) == 233)
			{
				this.ShapeTitleLogo.Data = (Geometry)new GeometryConverter().ConvertFromString("M26,29 v-25 h5 a7,7 180 0 1 0,14 h-5 M80,6.5 a10,11.5 180 1 0 0,18   M47,2.5 v24.5 h12   M98,2 v27   M107,2 v27");
			}
			ModSecret.PushReader();
			try
			{
				base.Height = Conversions.ToDouble(ModBase.m_IdentifierRepository.Get("WindowHeight", null));
				base.Width = Conversions.ToDouble(ModBase.m_IdentifierRepository.Get("WindowWidth", null));
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "读取窗口默认大小失败", ModBase.LogLevel.Hint, "出现错误");
				base.Height = base.MinHeight + 100.0;
				base.Width = base.MinWidth + 100.0;
			}
			base.Topmost = false;
			if (ModMain.m_ParameterIterator != null)
			{
				ModMain.m_ParameterIterator.Close(new TimeSpan(0, 0, 0, 0, checked((int)Math.Round(400.0 / ModAnimation.m_Task))));
			}
			base.Top = (ModBase.smethod_4((double)MyWpfExtension.ManageParser().Screen.WorkingArea.Height) - base.Height) / 2.0;
			base.Left = (ModBase.smethod_4((double)MyWpfExtension.ManageParser().Screen.WorkingArea.Width) - base.Width) / 2.0;
			this.m_StateIterator = true;
			this.ShowWindowToTop();
			((HwndSource)PresentationSource.FromVisual(this)).AddHook(new HwndSourceHook(this.WndProc));
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaCode((FormMain._Closure$__.$I4-0 == null) ? (FormMain._Closure$__.$I4-0 = delegate()
				{
					ModAnimation.AssetParser(checked(ModAnimation.CalcParser() - 1));
				}) : FormMain._Closure$__.$I4-0, 50, false),
				ModAnimation.AaOpacity(this, Conversions.ToDouble(Operators.AddObject(Operators.DivideObject(ModBase.m_IdentifierRepository.Get("UiLauncherTransparent", null), 1000), 0.4)), 250, 100, null, false),
				ModAnimation.AaDouble(delegate(object i)
				{
					TranslateTransform transformPos;
					(transformPos = this.TransformPos).Y = Conversions.ToDouble(Operators.AddObject(transformPos.Y, i));
				}, -this.TransformPos.Y, 600, 100, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false),
				ModAnimation.AaDouble(delegate(object i)
				{
					RotateTransform transformRotate;
					(transformRotate = this.TransformRotate).Angle = Conversions.ToDouble(Operators.AddObject(transformRotate.Angle, i));
				}, -this.TransformRotate.Angle, 500, 100, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false),
				ModAnimation.AaCode(delegate
				{
					this.PanBack.RenderTransform = null;
					this.m_DecoratorIterator = true;
					ModBase.Log(string.Format("[System] DPI：{0}，系统版本：{1}，PCL 位置：{2}", ModBase._ConfigurationRepository, Environment.OSVersion.VersionString, ModBase.interpreterRepository), ModBase.LogLevel.Normal, "出现错误");
				}, 0, true)
			}, "Form Show", false);
			ModAnimation.AniStart();
			ModMain.TimerMainStart();
			ModBase.RunInNewThread(delegate
			{
				if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("SystemEula", null))))
				{
					int num = ModMain.MyMsgBox("在使用 PCL 前，请同意 PCL 的用户协议与免责声明。", "协议授权", "同意", "拒绝", "查看用户协议与免责声明", false, true, false, null, null, (FormMain._Closure$__.$I4-5 == null) ? (FormMain._Closure$__.$I4-5 = delegate()
					{
						ModBase.OpenWebsite("https://shimo.im/docs/rGrd8pY8xWkt6ryW");
					}) : FormMain._Closure$__.$I4-5);
					if (num != 1)
					{
						if (num == 2)
						{
							this.EndProgram(false);
						}
					}
					else
					{
						ModBase.m_IdentifierRepository.Set("SystemEula", true, false, null);
					}
				}
				try
				{
					ModJava.JavaListInit();
					Thread.Sleep(200);
					ModDownload.queueTests.Start(1, false);
					this.RunCountSub();
					ModSecret._ConnectionField.Start(1, false);
				}
				catch (Exception ex2)
				{
					ModBase.Log(ex2, "初始化加载池运行失败", ModBase.LogLevel.Feedback, "出现错误");
				}
				try
				{
					if (File.Exists(ModBase.Path + "PCL\\Plain Craft Launcher 2.exe"))
					{
						File.Delete(ModBase.Path + "PCL\\Plain Craft Launcher 2.exe");
					}
				}
				catch (Exception ex3)
				{
					ModBase.Log(ex3, "清理自动更新文件失败", ModBase.LogLevel.Debug, "出现错误");
				}
			}, "Start Loader", ThreadPriority.Lowest);
			ModBase.Log("[Start] 第三阶段加载用时：" + Conversions.ToString(checked(ModBase.GetTimeTick() - ModBase._SystemRepository)) + " ms", ModBase.LogLevel.Normal, "出现错误");
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0000CA3F File Offset: 0x0000AC3F
		private void RunCountSub()
		{
			ModBase.m_IdentifierRepository.Set("SystemCount", Operators.AddObject(ModBase.m_IdentifierRepository.Get("SystemCount", null), 1), false, null);
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00094264 File Offset: 0x00092464
		private void UpgradeSub(int LastVersionCode)
		{
			ModBase.Log("[Start] 版本号从 " + Conversions.ToString(LastVersionCode) + " 升高到 " + Conversions.ToString(347), ModBase.LogLevel.Normal, "出现错误");
			ModBase.m_IdentifierRepository.Set("SystemLastVersionReg", 347, false, null);
			int num = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("SystemHighestBetaVersionReg", null));
			if (num < 347)
			{
				ModBase.m_IdentifierRepository.Set("SystemHighestBetaVersionReg", 347, false, null);
				ModBase.Log("[Start] 最高版本号从 " + Conversions.ToString(num) + " 升高到 " + Conversions.ToString(347), ModBase.LogLevel.Normal, "出现错误");
			}
			if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LaunchArgumentWindowType", null), 5, false))
			{
				ModBase.m_IdentifierRepository.Set("LaunchArgumentWindowType", 1, false, null);
			}
			if (num <= 207)
			{
				List<string> list = new List<string>
				{
					"2"
				};
				list.AddRange(new List<string>(ModBase.m_IdentifierRepository.Get("UiLauncherThemeHide", null).ToString().Split("|")));
				list.AddRange(new List<string>(ModBase.m_IdentifierRepository.Get("UiLauncherThemeHide2", null).ToString().Split("|")));
				ModBase.m_IdentifierRepository.Set("UiLauncherThemeHide2", Enumerable.ToList<string>(Enumerable.Distinct<string>(list)).Join("|"), false, null);
			}
			if (LastVersionCode <= 115 && Enumerable.Contains<string>(ModBase.m_IdentifierRepository.Get("UiLauncherThemeHide2", null).ToString().Split("|"), "13"))
			{
				List<string> list2 = new List<string>(ModBase.m_IdentifierRepository.Get("UiLauncherThemeHide2", null).ToString().Split("|"));
				list2.Remove("13");
				ModBase.m_IdentifierRepository.Set("UiLauncherThemeHide2", list2.Join("|"), false, null);
				ModMain.MyMsgBox("由于新版 PCL 修改了欧皇彩的解锁方式，你需要重新解锁欧皇彩。\r\n多谢各位的理解啦！", "重新解锁提醒", "确定", "", "", false, true, false, null, null, null);
			}
			if (LastVersionCode <= 152 && Enumerable.Contains<string>(ModBase.m_IdentifierRepository.Get("UiLauncherThemeHide2", null).ToString().Split("|"), "12"))
			{
				List<string> list3 = new List<string>(ModBase.m_IdentifierRepository.Get("UiLauncherThemeHide2", null).ToString().Split("|"));
				list3.Remove("12");
				ModBase.m_IdentifierRepository.Set("UiLauncherThemeHide2", list3.Join("|"), false, null);
				ModMain.MyMsgBox("由于新版 PCL 修改了滑稽彩的解锁方式，你需要重新解锁滑稽彩。\r\n多谢各位的理解啦！", "重新解锁提醒", "确定", "", "", false, true, false, null, null, null);
			}
			if (LastVersionCode <= 161 && File.Exists(ModBase.Path + "PCL\\CustomSkin.png") && !File.Exists(ModBase.m_InstanceRepository + "CustomSkin.png"))
			{
				ModBase.CopyFile(ModBase.Path + "PCL\\CustomSkin.png", ModBase.m_InstanceRepository + "CustomSkin.png");
				ModBase.Log("[Start] 已移动离线自定义皮肤 (162)", ModBase.LogLevel.Normal, "出现错误");
			}
			if (LastVersionCode <= 263 && File.Exists(ModBase.m_DecoratorRepository + "CustomSkin.png") && !File.Exists(ModBase.m_InstanceRepository + "CustomSkin.png"))
			{
				ModBase.CopyFile(ModBase.m_DecoratorRepository + "CustomSkin.png", ModBase.m_InstanceRepository + "CustomSkin.png");
				ModBase.Log("[Start] 已移动离线自定义皮肤 (264)", ModBase.LogLevel.Normal, "出现错误");
			}
			if (LastVersionCode <= 205)
			{
				ModBase.m_IdentifierRepository.Set("UiHiddenOtherHelp", false, false, null);
				ModBase.Log("[Start] 已解除帮助页面的隐藏", ModBase.LogLevel.Normal, "出现错误");
			}
			if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("CacheMsV2Migrated", null))))
			{
				ModBase.m_IdentifierRepository.Set("CacheMsV2Migrated", true, false, null);
				ModBase.m_IdentifierRepository.Set("CacheMsV2OAuthRefresh", RuntimeHelpers.GetObjectValue(ModBase.m_IdentifierRepository.Get("CacheMsOAuthRefresh", null)), false, null);
				ModBase.m_IdentifierRepository.Set("CacheMsV2Access", RuntimeHelpers.GetObjectValue(ModBase.m_IdentifierRepository.Get("CacheMsAccess", null)), false, null);
				ModBase.m_IdentifierRepository.Set("CacheMsV2ProfileJson", RuntimeHelpers.GetObjectValue(ModBase.m_IdentifierRepository.Get("CacheMsProfileJson", null)), false, null);
				ModBase.m_IdentifierRepository.Set("CacheMsV2Uuid", RuntimeHelpers.GetObjectValue(ModBase.m_IdentifierRepository.Get("CacheMsUuid", null)), false, null);
				ModBase.m_IdentifierRepository.Set("CacheMsV2Name", RuntimeHelpers.GetObjectValue(ModBase.m_IdentifierRepository.Get("CacheMsName", null)), false, null);
				ModBase.Log("[Start] 已从老版本迁移微软登录结果", ModBase.LogLevel.Normal, "出现错误");
			}
			if (LastVersionCode != 0 && num < 347)
			{
				this.ShowUpdateLog(num);
			}
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00094744 File Offset: 0x00092944
		private void DowngradeSub(int LastVersionCode)
		{
			ModBase.Log("[Start] 版本号从 " + Conversions.ToString(LastVersionCode) + " 降低到 " + Conversions.ToString(347), ModBase.LogLevel.Normal, "出现错误");
			ModBase.m_IdentifierRepository.Set("SystemLastVersionReg", 347, false, null);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0000CA6D File Offset: 0x0000AC6D
		private void FormMain_Closing(object sender, CancelEventArgs e)
		{
			this.EndProgram(true);
			e.Cancel = true;
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00094798 File Offset: 0x00092998
		public void EndProgram(bool SendWarning)
		{
			if (SendWarning && ModNet.HasDownloadingTask(false))
			{
				if (ModMain.MyMsgBox("还有下载任务尚未完成，是否确定退出？", "提示", "确定", "取消", "", false, true, false, null, null, null) != 1)
				{
					return;
				}
				ModBase.RunInNewThread((FormMain._Closure$__.$I9-0 == null) ? (FormMain._Closure$__.$I9-0 = delegate()
				{
					ModBase.Log("[System] 正在强行停止任务", ModBase.LogLevel.Normal, "出现错误");
					try
					{
						foreach (ModLoader.LoaderBase loaderBase in Enumerable.ToList<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar))
						{
							loaderBase.Abort();
						}
					}
					finally
					{
						List<ModLoader.LoaderBase>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}) : FormMain._Closure$__.$I9-0, "强行停止下载任务", ThreadPriority.Normal);
			}
			ModBase.RunInUiWait(delegate()
			{
				base.IsHitTestVisible = false;
				if (this.PanBack.RenderTransform == null)
				{
					FormMain._Closure$__9-0 CS$<>8__locals1 = new FormMain._Closure$__9-0(CS$<>8__locals1);
					CS$<>8__locals1.$VB$Local_TransformPos = new TranslateTransform(0.0, 0.0);
					CS$<>8__locals1.$VB$Local_TransformRotate = new RotateTransform(0.0);
					CS$<>8__locals1.$VB$Local_TransformScale = new ScaleTransform(1.0, 1.0);
					this.PanBack.RenderTransform = new TransformGroup
					{
						Children = new TransformCollection(new Transform[]
						{
							CS$<>8__locals1.$VB$Local_TransformRotate,
							CS$<>8__locals1.$VB$Local_TransformPos,
							CS$<>8__locals1.$VB$Local_TransformScale
						})
					};
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaOpacity(this, -base.Opacity, 140, 40, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false),
						ModAnimation.AaDouble(delegate(object i)
						{
							ScaleTransform $VB$Local_TransformScale;
							($VB$Local_TransformScale = CS$<>8__locals1.$VB$Local_TransformScale).ScaleX = Conversions.ToDouble(Operators.AddObject($VB$Local_TransformScale.ScaleX, i));
							($VB$Local_TransformScale = CS$<>8__locals1.$VB$Local_TransformScale).ScaleY = Conversions.ToDouble(Operators.AddObject($VB$Local_TransformScale.ScaleY, i));
						}, 0.88 - CS$<>8__locals1.$VB$Local_TransformScale.ScaleX, 180, 0, null, false),
						ModAnimation.AaDouble(delegate(object i)
						{
							TranslateTransform $VB$Local_TransformPos;
							($VB$Local_TransformPos = CS$<>8__locals1.$VB$Local_TransformPos).Y = Conversions.ToDouble(Operators.AddObject($VB$Local_TransformPos.Y, i));
						}, 20.0 - CS$<>8__locals1.$VB$Local_TransformPos.Y, 180, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false),
						ModAnimation.AaDouble(delegate(object i)
						{
							RotateTransform $VB$Local_TransformRotate;
							($VB$Local_TransformRotate = CS$<>8__locals1.$VB$Local_TransformRotate).Angle = Conversions.ToDouble(Operators.AddObject($VB$Local_TransformRotate.Angle, i));
						}, 0.6 - CS$<>8__locals1.$VB$Local_TransformRotate.Angle, 180, 0, new ModAnimation.AniEaseInoutFluent(ModAnimation.AniEasePower.Weak, 0.5), false),
						ModAnimation.AaCode(delegate
						{
							base.IsHitTestVisible = false;
							base.Top = -10000.0;
							base.ShowInTaskbar = false;
						}, 210, false),
						ModAnimation.AaCode((FormMain._Closure$__.$IR9-4 == null) ? (FormMain._Closure$__.$IR9-4 = delegate()
						{
							FormMain.EndProgramForce(ModBase.Result.Success);
						}) : FormMain._Closure$__.$IR9-4, 230, false)
					}, "Form Close", false);
				}
				else
				{
					FormMain.EndProgramForce(ModBase.Result.Success);
				}
				ModBase.Log("[System] 收到关闭指令", ModBase.LogLevel.Normal, "出现错误");
			});
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00094818 File Offset: 0x00092A18
		public static void EndProgramForce(ModBase.Result ReturnCode = ModBase.Result.Success)
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
				ModBase._ObserverRepository = true;
				IL_0F:
				num2 = 3;
				ModAnimation.AssetParser(checked(ModAnimation.CalcParser() + 1));
				IL_1D:
				num2 = 4;
				PageLinkIoi.IoiStop(false);
				IL_26:
				num2 = 5;
				PageLinkHiper.HiperStop(false);
				IL_2F:
				num2 = 6;
				if (!ModSecret.m_ExporterField)
				{
					goto IL_40;
				}
				IL_38:
				num2 = 7;
				ModSecret.UpdateRestart(false);
				IL_40:
				num2 = 8;
				if (ReturnCode != ModBase.Result.Exception)
				{
					goto IL_9D;
				}
				IL_46:
				num2 = 9;
				if (FormMain.m_InstanceIterator)
				{
					goto IL_90;
				}
				IL_50:
				num2 = 10;
				ModBase.FeedbackInfo();
				IL_58:
				num2 = 11;
				ModBase.Log("请在 https://github.com/Hex-Dragon/PCL2/issues 提交错误报告，以便于作者解决此问题！", ModBase.LogLevel.Normal, "出现错误");
				IL_6B:
				num2 = 12;
				FormMain.m_InstanceIterator = true;
				IL_74:
				num2 = 13;
				ModBase.ShellOnly(ModBase.Path + "PCL\\Log1.txt", "");
				IL_90:
				num2 = 14;
				Thread.Sleep(500);
				IL_9D:
				num2 = 15;
				ModBase.Log("[System] 程序已退出，返回值：" + ModBase.GetStringFromEnum(ReturnCode), ModBase.LogLevel.Normal, "出现错误");
				IL_C0:
				num2 = 16;
				ModBase.LogFlush();
				IL_C8:
				num2 = 17;
				if (ReturnCode != ModBase.Result.Success)
				{
					goto IL_DD;
				}
				IL_CE:
				num2 = 18;
				Process.GetCurrentProcess().Kill();
				goto IL_F3;
				IL_DD:
				num2 = 20;
				Environment.Exit((int)ReturnCode);
				IL_E6:
				num2 = 21;
				Process.GetCurrentProcess().Kill();
				IL_F3:
				goto IL_19D;
				IL_F8:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_15E:
				goto IL_192;
				IL_160:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_170:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_160;
			}
			IL_192:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_19D:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0000CA7D File Offset: 0x0000AC7D
		private void BtnTitleClose_Click(object sender, RoutedEventArgs e)
		{
			this.EndProgram(true);
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x000949E8 File Offset: 0x00092BE8
		private void FormDragMove(object sender, MouseButtonEventArgs e)
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
				if (!Conversions.ToBoolean(NewLateBinding.LateGet(sender, null, "IsMouseDirectlyOver", new object[0], null, null, null)))
				{
					goto IL_2D;
				}
				IL_25:
				num2 = 3;
				base.DragMove();
				IL_2D:
				goto IL_8C;
				IL_2F:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_4D:
				goto IL_81;
				IL_4F:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_5F:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_4F;
			}
			IL_81:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_8C:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00094A9C File Offset: 0x00092C9C
		private void FormMain_SizeChanged()
		{
			if (this.m_StateIterator)
			{
				ModBase.m_IdentifierRepository.Set("WindowHeight", base.Height, false, null);
				ModBase.m_IdentifierRepository.Set("WindowWidth", base.Width, false, null);
			}
			this.RectForm.Rect = new Rect(0.0, 0.0, this.BorderForm.ActualWidth, this.BorderForm.ActualHeight);
			this.PanForm.Width = this.BorderForm.ActualWidth + 0.001;
			this.PanForm.Height = this.BorderForm.ActualHeight + 0.001;
			this.PanMain.Width = this.PanForm.Width;
			this.PanMain.Height = Math.Max(0.0, this.PanForm.Height - this.PanTitle.ActualHeight);
			if (base.WindowState == WindowState.Maximized)
			{
				base.WindowState = WindowState.Normal;
			}
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0000CA86 File Offset: 0x0000AC86
		private void BtnTitleMin_Click()
		{
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00094BB8 File Offset: 0x00092DB8
		private void FormMain_KeyDown(object sender, KeyEventArgs e)
		{
			if (!e.IsRepeat)
			{
				if (this.PanMsg.Children.Count > 0)
				{
					if (e.Key == Key.Return)
					{
						NewLateBinding.LateCall(this.PanMsg.Children[0], null, "Btn1_Click", new object[0], null, null, null, true);
						return;
					}
					if (e.Key == Key.Escape)
					{
						object obj = this.PanMsg.Children[0];
						if (!(obj is MyMsgInput) && !(obj is MyMsgSelect) && Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(NewLateBinding.LateGet(obj, null, "Btn3", new object[0], null, null, null), null, "Visibility", new object[0], null, null, null), Visibility.Visible, false))
						{
							NewLateBinding.LateCall(obj, null, "Btn3_Click", new object[0], null, null, null, true);
							return;
						}
						if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(NewLateBinding.LateGet(obj, null, "Btn2", new object[0], null, null, null), null, "Visibility", new object[0], null, null, null), Visibility.Visible, false))
						{
							NewLateBinding.LateCall(obj, null, "Btn2_Click", new object[0], null, null, null, true);
							return;
						}
						NewLateBinding.LateCall(obj, null, "Btn1_Click", new object[0], null, null, null, true);
						return;
					}
				}
				if (e.Key == Key.Escape)
				{
					this.TriggerPageBack();
				}
				if (e.Key == Key.F11 && this._MethodIterator == FormMain.PageType.VersionSelect)
				{
					ModMain.proxyIterator._StatusMapper = !ModMain.proxyIterator._StatusMapper;
					ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", false);
					return;
				}
				if (e.Key == Key.F12)
				{
					PageSetupUI.ExcludeClient(!PageSetupUI.CreateClient());
					if (PageSetupUI.CreateClient())
					{
						ModMain.Hint("功能隐藏设置已暂时关闭！", ModMain.HintType.Finish, true);
					}
					else
					{
						ModMain.Hint("功能隐藏设置已重新开启！", ModMain.HintType.Finish, true);
					}
					PageSetupUI.HiddenRefresh();
					return;
				}
				if (e.Key == Key.F5)
				{
					if (this.configurationIterator is IRefreshable)
					{
						((IRefreshable)this.configurationIterator).Refresh();
					}
					if (this._GetterIterator is IRefreshable)
					{
						((IRefreshable)this._GetterIterator).Refresh();
						return;
					}
				}
				else
				{
					if (e.Key == Key.Return && this._MethodIterator == FormMain.PageType.Launch)
					{
						if (ModMain.mapRepository && !ModMain.m_ErrorRepository)
						{
							ModMain.Hint("木大！", ModMain.HintType.Info, true);
						}
						else
						{
							ModMain.recordIterator.LaunchButtonClick();
						}
					}
					if (e.SystemKey == Key.LeftAlt || e.SystemKey == Key.RightAlt)
					{
						e.Handled = true;
					}
				}
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0000CA8F File Offset: 0x0000AC8F
		private void FormMain_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.XButton1 || e.ChangedButton == MouseButton.XButton2)
			{
				this.TriggerPageBack();
			}
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0000CAA9 File Offset: 0x0000ACA9
		private void TriggerPageBack()
		{
			if (this._MethodIterator == FormMain.PageType.Download && this.GetTests() == FormMain.PageSubType.DownloadInstall && ModMain.m_VisitorIterator.strategyField)
			{
				ModMain.m_VisitorIterator.ExitSelectPage();
				return;
			}
			this.PageBack();
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00094E3C File Offset: 0x0009303C
		private void FormMain_Activated()
		{
			try
			{
				if (this._MethodIterator == FormMain.PageType.VersionSetup && this.GetTests() == FormMain.PageSubType.SetupSystem)
				{
					ModMain._ThreadRepository.ReloadModList(false);
				}
				else if (this._MethodIterator == FormMain.PageType.VersionSelect)
				{
					ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.RunOnUpdated, 1, "versions\\", false);
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "切回窗口时出错", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0000CAE4 File Offset: 0x0000ACE4
		private void FrmMain_PreviewDragOver(object sender, DragEventArgs e)
		{
			if (Enumerable.Contains<string>(e.Data.GetFormats(), "FileDrop"))
			{
				e.Effects = DragDropEffects.Link;
				return;
			}
			e.Effects = DragDropEffects.None;
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00094ED0 File Offset: 0x000930D0
		private void FrmMain_Drop(object sender, DragEventArgs e)
		{
			try
			{
				if (e.Data.GetDataPresent(DataFormats.Text))
				{
					try
					{
						string text = Conversions.ToString(e.Data.GetData(DataFormats.Text));
						ModBase.Log("[System] 接受文本拖拽：" + text, ModBase.LogLevel.Normal, "出现错误");
						if (text.StartsWithF("authlib-injector:yggdrasil-server:", false))
						{
							e.Handled = true;
							string text2 = WebUtility.UrlDecode(text.Substring("authlib-injector:yggdrasil-server:".Length));
							ModBase.Log("[System] Authlib 拖拽：" + text2, ModBase.LogLevel.Normal, "出现错误");
							if (!string.IsNullOrEmpty(new ValidateHttp().Validate(text2)))
							{
								ModMain.Hint(string.Format("输入的 Authlib 验证服务器不符合网址格式（{0}）！", text2), ModMain.HintType.Critical, true);
							}
							else
							{
								ModMinecraft.McVersion mcVersion = (this._MethodIterator == FormMain.PageType.VersionSetup) ? PageVersionLeft._InstanceConfig : ModMinecraft.AddClient();
								if (mcVersion == null)
								{
									ModMain.Hint("请先下载游戏，再设置第三方登录！", ModMain.HintType.Critical, true);
								}
								else
								{
									if (Operators.CompareString(text2, "https://littleskin.cn/api/yggdrasil", false) == 0)
									{
										if (ModMain.MyMsgBox(string.Format("是否要在版本 {0} 中开启 LittleSkin 登录？", mcVersion.Name) + "\r\n你可以在 版本设置 → 设置 → 服务器选项 中修改登录方式。", "第三方登录开启确认", "确定", "取消", "", false, true, false, null, null, null) == 2)
										{
											return;
										}
										ModBase.m_IdentifierRepository.Set("VersionServerLogin", 4, false, mcVersion);
										ModBase.m_IdentifierRepository.Set("VersionServerAuthServer", "https://littleskin.cn/api/yggdrasil", false, mcVersion);
										ModBase.m_IdentifierRepository.Set("VersionServerAuthRegister", "https://littleskin.cn/auth/register", false, mcVersion);
										ModBase.m_IdentifierRepository.Set("VersionServerAuthName", "LittleSkin 登录", false, mcVersion);
									}
									else
									{
										if (ModMain.MyMsgBox(string.Format("是否要在版本 {0} 中开启第三方登录？", mcVersion.Name) + "\r\n" + string.Format("登录服务器：{0}", text2) + "\r\n\r\n你可以在 版本设置 → 设置 → 服务器选项 中修改登录方式。", "第三方登录开启确认", "确定", "取消", "", false, true, false, null, null, null) == 2)
										{
											return;
										}
										ModBase.m_IdentifierRepository.Set("VersionServerLogin", 4, false, mcVersion);
										ModBase.m_IdentifierRepository.Set("VersionServerAuthServer", text2, false, mcVersion);
										ModBase.m_IdentifierRepository.Set("VersionServerAuthRegister", text2.Replace("api/yggdrasil", "auth/register"), false, mcVersion);
										ModBase.m_IdentifierRepository.Set("VersionServerAuthName", "", false, mcVersion);
									}
									if (this._MethodIterator == FormMain.PageType.VersionSetup && this.GetTests() == FormMain.PageSubType.DownloadInstall)
									{
										ModMain.composerRepository.Reload();
									}
									else if (this._MethodIterator == FormMain.PageType.Launch)
									{
										ModMain.recordIterator.RefreshPage(true, false);
									}
								}
							}
						}
						else if (text.StartsWithF("file:///", false))
						{
							string item = WebUtility.UrlDecode(text).Substring("file:///".Length).Replace("/", "\\");
							e.Handled = true;
							this.FileDrag(new List<string>
							{
								item
							});
						}
						goto IL_34A;
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "无法接取文本拖拽事件", ModBase.LogLevel.Developer, "出现错误");
						return;
					}
				}
				if (e.Data.GetDataPresent(DataFormats.FileDrop))
				{
					object objectValue = RuntimeHelpers.GetObjectValue(e.Data.GetData(DataFormats.FileDrop));
					if (objectValue == null)
					{
						ModMain.Hint("请将文件解压后再拖入！", ModMain.HintType.Critical, true);
					}
					else
					{
						e.Handled = true;
						this.FileDrag((IEnumerable<string>)objectValue);
					}
				}
				IL_34A:;
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, "接取拖拽事件失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00095280 File Offset: 0x00093480
		private void FileDrag(IEnumerable<string> FilePathList)
		{
			FormMain._Closure$__23-0 CS$<>8__locals1 = new FormMain._Closure$__23-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_FilePathList = FilePathList;
			ModBase.RunInNewThread(delegate
			{
				FormMain._Closure$__23-1 CS$<>8__locals2 = new FormMain._Closure$__23-1(CS$<>8__locals2);
				string text = Enumerable.First<string>(CS$<>8__locals1.$VB$Local_FilePathList);
				ModBase.Log("[System] 接受文件拖拽：" + text + (Enumerable.Any<string>(CS$<>8__locals1.$VB$Local_FilePathList) ? string.Format(" 等 {0} 个文件", Enumerable.Count<string>(CS$<>8__locals1.$VB$Local_FilePathList)) : ""), ModBase.LogLevel.Developer, "出现错误");
				if (Directory.Exists(Enumerable.First<string>(CS$<>8__locals1.$VB$Local_FilePathList)) && !File.Exists(Enumerable.First<string>(CS$<>8__locals1.$VB$Local_FilePathList)))
				{
					ModMain.Hint("请拖入一个文件，而非文件夹！", ModMain.HintType.Critical, true);
					return;
				}
				if (!File.Exists(Enumerable.First<string>(CS$<>8__locals1.$VB$Local_FilePathList)))
				{
					ModMain.Hint("拖入的文件不存在：" + Enumerable.First<string>(CS$<>8__locals1.$VB$Local_FilePathList), ModMain.HintType.Critical, true);
					return;
				}
				if (Enumerable.Count<string>(CS$<>8__locals1.$VB$Local_FilePathList) > 1)
				{
					try
					{
						foreach (string str in CS$<>8__locals1.$VB$Local_FilePathList)
						{
							if (!Enumerable.Contains<string>(new string[]
							{
								"jar",
								"litemod",
								"disabled",
								"old"
							}, str.AfterLast(".", false).ToLower()))
							{
								ModMain.Hint("一次请只拖入一个文件！", ModMain.HintType.Critical, true);
								return;
							}
						}
					}
					finally
					{
						IEnumerator<string> enumerator;
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
				}
				CS$<>8__locals2.$VB$Local_Extension = text.AfterLast(".", false).ToLower();
				if (Operators.CompareString(CS$<>8__locals2.$VB$Local_Extension, "xaml", false) == 0)
				{
					ModBase.Log("[System] 文件后缀为 XAML，作为自定义主页加载", ModBase.LogLevel.Normal, "出现错误");
					if (!File.Exists(ModBase.Path + "PCL\\Custom.xaml") || ModMain.MyMsgBox("已存在一个自定义主页文件，是否要将它覆盖？", "覆盖确认", "覆盖", "取消", "", false, true, false, null, null, null) != 2)
					{
						ModBase.CopyFile(text, ModBase.Path + "PCL\\Custom.xaml");
						ModBase.RunInUi((FormMain._Closure$__.$I23-1 == null) ? (FormMain._Closure$__.$I23-1 = delegate()
						{
							ModBase.m_IdentifierRepository.Set("UiCustomType", 1, false, null);
							ModMain.m_ServiceIterator.ForceRefresh();
							ModMain.Hint("已加载主页自定义文件！", ModMain.HintType.Finish, true);
						}) : FormMain._Closure$__.$I23-1, false);
						return;
					}
				}
				else if (!PageVersionMod.InstallMods(CS$<>8__locals1.$VB$Local_FilePathList))
				{
					if (Enumerable.Any<string>(new string[]
					{
						"zip",
						"rar",
						"mrpack"
					}, (string t) => Operators.CompareString(t, CS$<>8__locals2.$VB$Local_Extension, false) == 0))
					{
						ModBase.Log("[System] 文件为压缩包，尝试作为整合包安装", ModBase.LogLevel.Normal, "出现错误");
						try
						{
							ModModpack.ModpackInstall(text, null, null);
							return;
						}
						catch (ModBase.CancelledException ex)
						{
							return;
						}
						catch (Exception ex2)
						{
						}
					}
					if (Operators.CompareString(CS$<>8__locals2.$VB$Local_Extension, "rar", false) == 0)
					{
						ModMain.Hint("PCL 无法处理 rar 格式的压缩包，请在解压后重新压缩为 zip 格式再试！", ModMain.HintType.Info, true);
						return;
					}
					try
					{
						ModBase.Log("[System] 尝试进行错误报告分析", ModBase.LogLevel.Normal, "出现错误");
						CrashAnalyzer crashAnalyzer = new CrashAnalyzer(ModBase.GetUuid());
						crashAnalyzer.Import(text);
						if (crashAnalyzer.Prepare() != 0)
						{
							crashAnalyzer.Analyze(null);
							crashAnalyzer.Output(true, new List<string>());
							return;
						}
					}
					catch (Exception ex3)
					{
						ModBase.Log(ex3, "自主错误报告分析失败", ModBase.LogLevel.Feedback, "出现错误");
					}
					ModMain.Hint("PCL 无法确定应当执行的文件拖拽操作……", ModMain.HintType.Info, true);
				}
			}, "文件拖拽", ThreadPriority.Normal);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x000952B4 File Offset: 0x000934B4
		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg == 30)
			{
				DateTime now = DateTime.Now;
				if (DateTime.Compare(now.Date, ModBase.paramRepository.Date) == 0)
				{
					ModBase.Log("[System] 系统时间微调为：" + now.ToLongDateString() + " " + now.ToLongTimeString(), ModBase.LogLevel.Normal, "出现错误");
					this.callbackIterator = false;
				}
				else
				{
					ModBase.Log("[System] 系统时间修改为：" + now.ToLongDateString() + " " + now.ToLongTimeString(), ModBase.LogLevel.Normal, "出现错误");
					this.callbackIterator = true;
				}
			}
			else if (msg == 6402)
			{
				ModBase.Log("[System] 收到置顶信息：" + Conversions.ToString(hwnd.ToInt64()), ModBase.LogLevel.Normal, "出现错误");
				if (!this.m_DecoratorIterator)
				{
					ModBase.Log("[System] 窗口尚未加载完成，忽略置顶请求", ModBase.LogLevel.Normal, "出现错误");
					return IntPtr.Zero;
				}
				this.ShowWindowToTop();
				handled = true;
			}
			return IntPtr.Zero;
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0000CB0C File Offset: 0x0000AD0C
		// (set) Token: 0x060016A2 RID: 5794 RVA: 0x000953A8 File Offset: 0x000935A8
		public bool Hidden
		{
			get
			{
				return this._TemplateIterator;
			}
			set
			{
				if (this._TemplateIterator != value)
				{
					this._TemplateIterator = value;
					if (value)
					{
						base.Left -= 10000.0;
						base.ShowInTaskbar = false;
						base.Visibility = Visibility.Hidden;
						ModBase.Log(string.Concat(new string[]
						{
							"[System] 窗口已隐藏，位置：(",
							Conversions.ToString(base.Left),
							",",
							Conversions.ToString(base.Top),
							")"
						}), ModBase.LogLevel.Normal, "出现错误");
						return;
					}
					if (base.Left < -2000.0)
					{
						base.Left += 10000.0;
					}
					this.ShowWindowToTop();
				}
			}
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0000CB14 File Offset: 0x0000AD14
		public void ShowWindowToTop()
		{
			ModBase.RunInUi(delegate()
			{
				base.Visibility = Visibility.Visible;
				base.ShowInTaskbar = true;
				base.WindowState = WindowState.Normal;
				this.Hidden = false;
				base.Topmost = true;
				base.Topmost = false;
				ModMain.SetForegroundWindow(ModBase.m_IndexerRepository);
				base.Focus();
				ModBase.Log(string.Format("[System] 窗口已置顶，位置：({0}, {1}), {2} x {3}", new object[]
				{
					base.Left,
					base.Top,
					base.Width,
					base.Height
				}), ModBase.LogLevel.Normal, "出现错误");
			}, false);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00095468 File Offset: 0x00093668
		private string PageNameGet(FormMain.PageStackData Stack)
		{
			string result;
			switch (Stack.initializerMap)
			{
			case FormMain.PageType.VersionSelect:
				result = "版本选择";
				break;
			case FormMain.PageType.DownloadManager:
				result = "下载管理";
				break;
			case FormMain.PageType.VersionSetup:
				result = "版本设置 - " + ((PageVersionLeft._InstanceConfig == null) ? "未知版本" : PageVersionLeft._InstanceConfig.Name);
				break;
			case FormMain.PageType.CompDetail:
			{
				ModComp.CompProject compProject = (ModComp.CompProject)NewLateBinding.LateIndexGet(Stack.m_SingletonMap, new object[]
				{
					0
				}, null);
				ModComp.CompType type = compProject.Type;
				if (type != ModComp.CompType.Mod)
				{
					if (type != ModComp.CompType.ModPack)
					{
						result = "资源包下载 - " + compProject.RemoveTests();
					}
					else
					{
						result = "整合包下载 - " + compProject.RemoveTests();
					}
				}
				else
				{
					result = "Mod 下载 - " + compProject.RemoveTests();
				}
				break;
			}
			case FormMain.PageType.HelpDetail:
				result = ((ModMain.HelpEntry)NewLateBinding.LateIndexGet(Stack.m_SingletonMap, new object[]
				{
					0
				}, null)).Title;
				break;
			default:
				result = "";
				break;
			}
			return result;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0000CB28 File Offset: 0x0000AD28
		public void PageNameRefresh(FormMain.PageStackData Type)
		{
			this.LabTitleInner.Text = this.PageNameGet(Type);
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0000CB3C File Offset: 0x0000AD3C
		public void PageNameRefresh()
		{
			this.PageNameRefresh(this._MethodIterator);
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00095570 File Offset: 0x00093770
		public FormMain.PageSubType GetTests()
		{
			FormMain.PageStackData methodIterator = this._MethodIterator;
			FormMain.PageSubType result;
			if (methodIterator == FormMain.PageType.Download)
			{
				if (ModMain.m_CollectionIterator == null)
				{
					ModMain.m_CollectionIterator = new PageDownloadLeft();
				}
				result = ModMain.m_CollectionIterator._InterpreterThread;
			}
			else if (methodIterator == FormMain.PageType.Setup)
			{
				if (ModMain._ClassIterator == null)
				{
					ModMain._ClassIterator = new PageSetupLeft();
				}
				result = ModMain._ClassIterator._ConnectionThread;
			}
			else if (methodIterator == FormMain.PageType.Other)
			{
				if (ModMain.descriptorIterator == null)
				{
					ModMain.descriptorIterator = new PageOtherLeft();
				}
				result = ModMain.descriptorIterator.expressionThread;
			}
			else if (methodIterator == FormMain.PageType.VersionSetup)
			{
				if (ModMain.m_TestsRepository == null)
				{
					ModMain.m_TestsRepository = new PageVersionLeft();
				}
				result = ModMain.m_TestsRepository._StateConfig;
			}
			else
			{
				result = FormMain.PageSubType.Default;
			}
			return result;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0009563C File Offset: 0x0009383C
		public void PageChange(FormMain.PageStackData Stack, FormMain.PageSubType SubType = FormMain.PageSubType.Default)
		{
			if (Operators.CompareString(this.PageNameGet(Stack), "", false) == 0)
			{
				this.PageChangeExit();
				this.m_TokenIterator = true;
				((MyRadioButton)this.PanTitleSelect.Children[(int)Stack]).SetChecked(true, true, Operators.CompareString(this.PageNameGet(this._MethodIterator), "", false) == 0);
				this.m_TokenIterator = false;
				switch (Stack.initializerMap)
				{
				case FormMain.PageType.Download:
					if (ModMain.m_CollectionIterator == null)
					{
						ModMain.m_CollectionIterator = new PageDownloadLeft();
					}
					((MyListItem)ModMain.m_CollectionIterator.PanItem.Children[(int)SubType]).SetChecked(true, true, Stack == this._MethodIterator);
					break;
				case FormMain.PageType.Setup:
					if (ModMain._ClassIterator == null)
					{
						ModMain._ClassIterator = new PageSetupLeft();
					}
					((MyListItem)ModMain._ClassIterator.PanItem.Children[(int)SubType]).SetChecked(true, true, Stack == this._MethodIterator);
					break;
				case FormMain.PageType.Other:
					if (ModMain.descriptorIterator == null)
					{
						ModMain.descriptorIterator = new PageOtherLeft();
					}
					((MyListItem)ModMain.descriptorIterator.PanItem.Children[(int)SubType]).SetChecked(true, true, Stack == this._MethodIterator);
					break;
				}
				this.PageChangeActual(Stack, SubType);
				return;
			}
			FormMain.PageType initializerMap = Stack.initializerMap;
			if (initializerMap == FormMain.PageType.VersionSetup)
			{
				if (ModMain.m_TestsRepository == null)
				{
					ModMain.m_TestsRepository = new PageVersionLeft();
				}
				((MyListItem)ModMain.m_TestsRepository.PanItem.Children[(int)SubType]).SetChecked(true, true, Stack == this._MethodIterator);
			}
			this.PageChangeActual(Stack, SubType);
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0000CB4A File Offset: 0x0000AD4A
		private void BtnTitleSelect_Click(MyRadioButton sender, bool raiseByMouse)
		{
			if (!this.m_TokenIterator)
			{
				this.PageChangeActual(checked((FormMain.PageType)Math.Round(ModBase.Val(RuntimeHelpers.GetObjectValue(sender.Tag)))), (FormMain.PageSubType)(-1));
			}
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0000CB76 File Offset: 0x0000AD76
		public void PageBack()
		{
			if (Enumerable.Any<FormMain.PageStackData>(this._ConsumerIterator))
			{
				this.PageChangeActual(this._ConsumerIterator[0], (FormMain.PageSubType)(-1));
				return;
			}
			this.PageChange(FormMain.PageType.Launch, FormMain.PageSubType.Default);
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x000957F0 File Offset: 0x000939F0
		private void PageChangeActual(FormMain.PageStackData Stack, FormMain.PageSubType SubType = (FormMain.PageSubType)(-1))
		{
			if (!(this._MethodIterator == Stack) || (this.GetTests() != SubType && SubType != (FormMain.PageSubType)(-1)))
			{
				ModAnimation.AssetParser(checked(ModAnimation.CalcParser() + 1));
				try
				{
					FormMain._Closure$__48-0 CS$<>8__locals1 = new FormMain._Closure$__48-0(CS$<>8__locals1);
					CS$<>8__locals1.$VB$Me = this;
					CS$<>8__locals1.$VB$Local_PageName = this.PageNameGet(Stack);
					if (Operators.CompareString(CS$<>8__locals1.$VB$Local_PageName, "", false) == 0)
					{
						this.PageChangeExit();
					}
					else if (Enumerable.Any<FormMain.PageStackData>(this._ConsumerIterator))
					{
						ModAnimation.AniStart(new ModAnimation.AniData[]
						{
							ModAnimation.AaOpacity(this.LabTitleInner, -this.LabTitleInner.Opacity, 130, 0, null, false),
							ModAnimation.AaCode(delegate
							{
								CS$<>8__locals1.$VB$Me.LabTitleInner.Text = CS$<>8__locals1.$VB$Local_PageName;
							}, 0, true),
							ModAnimation.AaOpacity(this.LabTitleInner, 1.0, 150, 30, null, false)
						}, "FrmMain Titlebar SubLayer", false);
						if (this._ConsumerIterator.Contains(Stack))
						{
							while (this._ConsumerIterator.Contains(Stack))
							{
								this._ConsumerIterator.RemoveAt(0);
							}
						}
						else
						{
							this._ConsumerIterator.Insert(0, this._MethodIterator);
						}
					}
					else
					{
						this.PanTitleInner.Visibility = Visibility.Visible;
						this.PanTitleMain.IsHitTestVisible = false;
						this.PanTitleInner.IsHitTestVisible = true;
						this.PageNameRefresh(Stack);
						ModAnimation.AniStart(new ModAnimation.AniData[]
						{
							ModAnimation.AaOpacity(this.PanTitleMain, -this.PanTitleMain.Opacity, 150, 0, null, false),
							ModAnimation.AaX(this.PanTitleMain, 12.0 - this.PanTitleMain.Margin.Left, 150, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak), false),
							ModAnimation.AaOpacity(this.PanTitleInner, 1.0 - this.PanTitleInner.Opacity, 150, 200, null, false),
							ModAnimation.AaX(this.PanTitleInner, -this.PanTitleInner.Margin.Left, 350, 200, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Middle), false),
							ModAnimation.AaCode(delegate
							{
								this.PanTitleMain.Visibility = Visibility.Collapsed;
							}, 0, true)
						}, "FrmMain Titlebar FirstLayer", false);
						this._ConsumerIterator.Insert(0, this._MethodIterator);
					}
					this.taskIterator = this._MethodIterator;
					this._MethodIterator = Stack;
					switch (Stack.initializerMap)
					{
					case FormMain.PageType.Launch:
						this.PageChangeAnim(ModMain.recordIterator, ModMain.m_ServiceIterator);
						break;
					case FormMain.PageType.Download:
						if (ModMain.m_CollectionIterator == null)
						{
							ModMain.m_CollectionIterator = new PageDownloadLeft();
						}
						this.PageChangeAnim(ModMain.m_CollectionIterator, (FrameworkElement)ModMain.m_CollectionIterator.PageGet(SubType));
						break;
					case FormMain.PageType.Link:
						if (ModMain.m_InitializerIterator == null)
						{
							ModMain.m_InitializerIterator = new PageLinkLeft();
						}
						this.PageChangeAnim(ModMain.m_InitializerIterator, (FrameworkElement)ModMain.m_InitializerIterator.PageGet(SubType));
						break;
					case FormMain.PageType.Setup:
						if (ModMain._ClassIterator == null)
						{
							ModMain._ClassIterator = new PageSetupLeft();
						}
						this.PageChangeAnim(ModMain._ClassIterator, (FrameworkElement)ModMain._ClassIterator.PageGet(SubType));
						break;
					case FormMain.PageType.Other:
						if (ModMain.descriptorIterator == null)
						{
							ModMain.descriptorIterator = new PageOtherLeft();
						}
						this.PageChangeAnim(ModMain.descriptorIterator, (FrameworkElement)ModMain.descriptorIterator.PageGet(SubType));
						break;
					case FormMain.PageType.VersionSelect:
						if (ModMain._InvocationIterator == null)
						{
							ModMain._InvocationIterator = new PageSelectLeft();
						}
						if (ModMain.proxyIterator == null)
						{
							ModMain.proxyIterator = new PageSelectRight();
						}
						this.PageChangeAnim(ModMain._InvocationIterator, ModMain.proxyIterator);
						break;
					case FormMain.PageType.DownloadManager:
						if (ModMain._MessageIterator == null)
						{
							ModMain._MessageIterator = new PageSpeedLeft();
						}
						if (ModMain.m_CreatorIterator == null)
						{
							ModMain.m_CreatorIterator = new PageSpeedRight();
						}
						this.PageChangeAnim(ModMain._MessageIterator, ModMain.m_CreatorIterator);
						break;
					case FormMain.PageType.VersionSetup:
						if (ModMain.m_TestsRepository == null)
						{
							ModMain.m_TestsRepository = new PageVersionLeft();
						}
						this.PageChangeAnim(ModMain.m_TestsRepository, (FrameworkElement)ModMain.m_TestsRepository.PageGet(SubType));
						break;
					case FormMain.PageType.CompDetail:
						if (ModMain.iteratorRepository == null)
						{
							ModMain.iteratorRepository = new PageDownloadCompDetail();
						}
						this.PageChangeAnim(new MyPageLeft(), ModMain.iteratorRepository);
						break;
					case FormMain.PageType.HelpDetail:
						this.PageChangeAnim(new MyPageLeft(), (FrameworkElement)NewLateBinding.LateIndexGet(Stack.m_SingletonMap, new object[]
						{
							1
						}, null));
						break;
					}
					this.BtnExtraDownload.ShowRefresh();
					this.BtnExtraApril.ShowRefresh();
					ModBase.Log("[Control] 切换主要页面：" + ModBase.GetStringFromEnum(Stack) + ", " + Conversions.ToString((int)SubType), ModBase.LogLevel.Normal, "出现错误");
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "切换主要页面失败（ID " + Conversions.ToString((int)this._MethodIterator.initializerMap) + "）", ModBase.LogLevel.Feedback, "出现错误");
				}
				finally
				{
					ModAnimation.AssetParser(checked(ModAnimation.CalcParser() - 1));
				}
			}
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00095D34 File Offset: 0x00093F34
		private void PageChangeAnim(FrameworkElement TargetLeft, FrameworkElement TargetRight)
		{
			ModAnimation.AniStop("FrmMain LeftChange");
			ModAnimation.AniStop("PageLeft PageChange");
			checked
			{
				ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
				if (!Information.IsNothing(TargetLeft.Parent))
				{
					TargetLeft.SetValue(ContentPresenter.ContentProperty, null);
				}
				if (!Information.IsNothing(TargetRight) && !Information.IsNothing(TargetRight.Parent))
				{
					TargetRight.SetValue(ContentPresenter.ContentProperty, null);
				}
				this.configurationIterator = (MyPageLeft)TargetLeft;
				this._GetterIterator = (MyPageRight)TargetRight;
				((MyPageLeft)this.PanMainLeft.Child).TriggerHideAnimation();
				((MyPageRight)this.PanMainRight.Child).PageOnExit();
				ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaCode(delegate
					{
						ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
						this.PanMainLeft.Child = this.configurationIterator;
						this.configurationIterator.Opacity = 0.0;
						this.PanMainLeft.Background = null;
						ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
						ModBase.RunInUi(delegate()
						{
							this.PanMainLeft_Resize(this.PanMainLeft.ActualWidth);
						}, true);
					}, 130, false),
					ModAnimation.AaCode(delegate
					{
						this.configurationIterator.Opacity = 1.0;
						this.configurationIterator.TriggerShowAnimation();
					}, 30, true)
				}, "FrmMain PageChangeLeft", false);
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaCode(delegate
					{
						ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
						((MyPageRight)this.PanMainRight.Child).PageOnForceExit();
						this.PanMainRight.Child = this._GetterIterator;
						this._GetterIterator.Opacity = 0.0;
						this.PanMainRight.Background = null;
						ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
						ModBase.RunInUi(delegate()
						{
							this.BtnExtraBack.ShowRefresh();
						}, true);
					}, 130, false),
					ModAnimation.AaCode(delegate
					{
						this._GetterIterator.Opacity = 1.0;
						this._GetterIterator.PageOnEnter();
					}, 30, true)
				}, "FrmMain PageChangeRight", false);
			}
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00095E80 File Offset: 0x00094080
		private void PageChangeExit()
		{
			if (Enumerable.Any<FormMain.PageStackData>(this._ConsumerIterator))
			{
				this.PanTitleMain.Visibility = Visibility.Visible;
				this.PanTitleMain.IsHitTestVisible = true;
				this.PanTitleInner.IsHitTestVisible = false;
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaOpacity(this.PanTitleInner, -this.PanTitleInner.Opacity, 150, 0, null, false),
					ModAnimation.AaX(this.PanTitleInner, -18.0 - this.PanTitleInner.Margin.Left, 150, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false),
					ModAnimation.AaOpacity(this.PanTitleMain, 1.0 - this.PanTitleMain.Opacity, 150, 200, null, false),
					ModAnimation.AaX(this.PanTitleMain, -this.PanTitleMain.Margin.Left, 350, 200, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false),
					ModAnimation.AaCode(delegate
					{
						this.PanTitleInner.Visibility = Visibility.Collapsed;
					}, 0, true)
				}, "FrmMain Titlebar FirstLayer", false);
				this._ConsumerIterator.Clear();
			}
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00095FC4 File Offset: 0x000941C4
		private void PanMainLeft_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (e.WidthChanged)
			{
				this.PanMainLeft_Resize(e.NewSize.Width);
			}
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00095FF0 File Offset: 0x000941F0
		private void PanMainLeft_Resize(double NewWidth)
		{
			if (Math.Abs(NewWidth - this.RectLeftBackground.Width) <= 0.1 || ModAnimation.CalcParser() != 0)
			{
				this.RectLeftBackground.Width = NewWidth;
				this.PanMainLeft.IsHitTestVisible = true;
				ModAnimation.AniStop("FrmMain LeftChange");
				return;
			}
			if (this.PanMain.Opacity < 0.1)
			{
				this.PanMainLeft.IsHitTestVisible = false;
			}
			if (NewWidth > 0.0)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaWidth(this.RectLeftBackground, NewWidth - this.RectLeftBackground.Width, 400, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.ExtraStrong), false),
					ModAnimation.AaOpacity(this.RectLeftShadow, 1.0 - this.RectLeftShadow.Opacity, 200, 0, null, false),
					ModAnimation.AaCode(delegate
					{
						this.PanMainLeft.IsHitTestVisible = true;
					}, 250, false)
				}, "FrmMain LeftChange", true);
				return;
			}
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaWidth(this.RectLeftBackground, -this.RectLeftBackground.Width, 200, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
				ModAnimation.AaOpacity(this.RectLeftShadow, -this.RectLeftShadow.Opacity, 200, 0, null, false),
				ModAnimation.AaCode(delegate
				{
					this.PanMainLeft.IsHitTestVisible = true;
				}, 170, false)
			}, "FrmMain LeftChange", true);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0000CBA6 File Offset: 0x0000ADA6
		public void DragTick()
		{
			if (ModMain._DicRepository != null && Mouse.LeftButton != MouseButtonState.Pressed)
			{
				this.DragStop();
			}
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00096188 File Offset: 0x00094388
		public void DragDoing()
		{
			if (ModMain._DicRepository != null)
			{
				if (Mouse.LeftButton == MouseButtonState.Pressed)
				{
					NewLateBinding.LateCall(ModMain._DicRepository, null, "DragDoing", new object[0], null, null, null, true);
					return;
				}
				this.DragStop();
			}
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0000CBBD File Offset: 0x0000ADBD
		public void DragStop()
		{
			ModBase.RunInUi((FormMain._Closure$__.$I55-0 == null) ? (FormMain._Closure$__.$I55-0 = delegate()
			{
				if (ModMain._DicRepository != null)
				{
					object objectValue = RuntimeHelpers.GetObjectValue(ModMain._DicRepository);
					ModMain._DicRepository = null;
					NewLateBinding.LateCall(objectValue, null, "DragStop", new object[0], null, null, null, true);
				}
			}) : FormMain._Closure$__.$I55-0, false);
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0000CBE9 File Offset: 0x0000ADE9
		private void BtnExtraMusic_Click(object sender, EventArgs e)
		{
			ModMusic.MusicControlPause();
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0000CBF0 File Offset: 0x0000ADF0
		private void BtnExtraMusic_RightClick(object sender, EventArgs e)
		{
			ModMusic.MusicControlNext();
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0000CBF7 File Offset: 0x0000ADF7
		private void BtnExtraDownload_Click(object sender, EventArgs e)
		{
			this.PageChange(FormMain.PageType.DownloadManager, FormMain.PageSubType.Default);
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0000CC06 File Offset: 0x0000AE06
		private bool BtnExtraDownload_ShowCheck()
		{
			return ModNet.HasDownloadingTask(false) && !(this._MethodIterator == FormMain.PageType.DownloadManager);
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x000961C8 File Offset: 0x000943C8
		public void AprilGiveup()
		{
			if (ModMain.mapRepository && !ModMain.m_ErrorRepository)
			{
				ModMain.Hint("=D", ModMain.HintType.Finish, true);
				ModMain.m_ErrorRepository = true;
				ModMain.recordIterator.AprilScaleTrans.ScaleX = 1.0;
				ModMain.recordIterator.AprilScaleTrans.ScaleY = 1.0;
				this.BtnExtraApril.ShowRefresh();
			}
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x0000CC26 File Offset: 0x0000AE26
		public bool BtnExtraApril_ShowCheck()
		{
			return ModMain.mapRepository && !ModMain.m_ErrorRepository && this._MethodIterator == FormMain.PageType.Launch;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x00096230 File Offset: 0x00094430
		public void BtnExtraShutdown_Click()
		{
			try
			{
				if (ModLaunch.m_PredicateTests != null)
				{
					ModLaunch.m_PredicateTests.Abort();
				}
				try
				{
					foreach (ModWatcher.Watcher watcher in ModWatcher.dicField)
					{
						watcher.Kill();
					}
				}
				finally
				{
					List<ModWatcher.Watcher>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				ModMain.Hint("已关闭运行中的 Minecraft！", ModMain.HintType.Finish, true);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "强制关闭所有 Minecraft 失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0000CC49 File Offset: 0x0000AE49
		public bool BtnExtraShutdown_ShowCheck()
		{
			return ModWatcher._IssuerField;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x000962CC File Offset: 0x000944CC
		public void BackToTop()
		{
			MyScrollViewer myScrollViewer = this.BtnExtraBack_GetRealChild();
			if (myScrollViewer != null)
			{
				myScrollViewer.PerformVerticalOffsetDelta(-myScrollViewer.VerticalOffset);
				return;
			}
			ModBase.Log("[UI] 无法返回顶部，未找到合适的 RealScroll", ModBase.LogLevel.Hint, "出现错误");
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00096304 File Offset: 0x00094504
		private bool BtnExtraBack_ShowCheck()
		{
			MyScrollViewer myScrollViewer = this.BtnExtraBack_GetRealChild();
			return myScrollViewer != null && myScrollViewer.Visibility == Visibility.Visible && myScrollViewer.VerticalOffset > base.Height + (double)(this.BtnExtraBack.Show ? 0 : 700);
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0009634C File Offset: 0x0009454C
		private MyScrollViewer BtnExtraBack_GetRealChild()
		{
			MyScrollViewer result;
			if (this.PanMainRight.Child != null && this.PanMainRight.Child is MyPageRight)
			{
				result = ((MyPageRight)this.PanMainRight.Child).PanScroll;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0000CC50 File Offset: 0x0000AE50
		private void FormMain_MouseMove(object sender, MouseEventArgs e)
		{
			this.expressionIterator = e;
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x0000CC59 File Offset: 0x0000AE59
		// (set) Token: 0x060016C0 RID: 5824 RVA: 0x0000CC61 File Offset: 0x0000AE61
		internal virtual FormMain WindMain { get; set; }

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x0000CC6A File Offset: 0x0000AE6A
		// (set) Token: 0x060016C2 RID: 5826 RVA: 0x00096394 File Offset: 0x00094594
		internal virtual Grid PanBack
		{
			[CompilerGenerated]
			get
			{
				return this.registryIterator;
			}
			[CompilerGenerated]
			set
			{
				MouseEventHandler value2 = delegate(object sender, MouseEventArgs e)
				{
					this.DragDoing();
				};
				Grid grid = this.registryIterator;
				if (grid != null)
				{
					grid.MouseMove -= value2;
				}
				this.registryIterator = value;
				grid = this.registryIterator;
				if (grid != null)
				{
					grid.MouseMove += value2;
				}
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x0000CC72 File Offset: 0x0000AE72
		// (set) Token: 0x060016C4 RID: 5828 RVA: 0x0000CC7A File Offset: 0x0000AE7A
		internal virtual RotateTransform TransformRotate { get; set; }

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x0000CC83 File Offset: 0x0000AE83
		// (set) Token: 0x060016C6 RID: 5830 RVA: 0x0000CC8B File Offset: 0x0000AE8B
		internal virtual TranslateTransform TransformPos { get; set; }

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0000CC94 File Offset: 0x0000AE94
		// (set) Token: 0x060016C8 RID: 5832 RVA: 0x0000CC9C File Offset: 0x0000AE9C
		internal virtual Rectangle ResizerT { get; set; }

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0000CCA5 File Offset: 0x0000AEA5
		// (set) Token: 0x060016CA RID: 5834 RVA: 0x0000CCAD File Offset: 0x0000AEAD
		internal virtual Rectangle ResizerB { get; set; }

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x0000CCB6 File Offset: 0x0000AEB6
		// (set) Token: 0x060016CC RID: 5836 RVA: 0x0000CCBE File Offset: 0x0000AEBE
		internal virtual Rectangle ResizerR { get; set; }

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x0000CCC7 File Offset: 0x0000AEC7
		// (set) Token: 0x060016CE RID: 5838 RVA: 0x0000CCCF File Offset: 0x0000AECF
		internal virtual Rectangle ResizerL { get; set; }

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x0000CCD8 File Offset: 0x0000AED8
		// (set) Token: 0x060016D0 RID: 5840 RVA: 0x0000CCE0 File Offset: 0x0000AEE0
		internal virtual Rectangle ResizerLT { get; set; }

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x0000CCE9 File Offset: 0x0000AEE9
		// (set) Token: 0x060016D2 RID: 5842 RVA: 0x0000CCF1 File Offset: 0x0000AEF1
		internal virtual Rectangle ResizerLB { get; set; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x0000CCFA File Offset: 0x0000AEFA
		// (set) Token: 0x060016D4 RID: 5844 RVA: 0x0000CD02 File Offset: 0x0000AF02
		internal virtual Rectangle ResizerRB { get; set; }

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0000CD0B File Offset: 0x0000AF0B
		// (set) Token: 0x060016D6 RID: 5846 RVA: 0x0000CD13 File Offset: 0x0000AF13
		internal virtual Rectangle ResizerRT { get; set; }

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x0000CD1C File Offset: 0x0000AF1C
		// (set) Token: 0x060016D8 RID: 5848 RVA: 0x0000CD24 File Offset: 0x0000AF24
		internal virtual Border BorderForm { get; set; }

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x0000CD2D File Offset: 0x0000AF2D
		// (set) Token: 0x060016DA RID: 5850 RVA: 0x0000CD35 File Offset: 0x0000AF35
		internal virtual RectangleGeometry RectForm { get; set; }

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x0000CD3E File Offset: 0x0000AF3E
		// (set) Token: 0x060016DC RID: 5852 RVA: 0x0000CD46 File Offset: 0x0000AF46
		internal virtual Grid PanForm { get; set; }

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x0000CD4F File Offset: 0x0000AF4F
		// (set) Token: 0x060016DE RID: 5854 RVA: 0x0000CD57 File Offset: 0x0000AF57
		internal virtual Canvas ImgBack { get; set; }

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x0000CD60 File Offset: 0x0000AF60
		// (set) Token: 0x060016E0 RID: 5856 RVA: 0x000963D8 File Offset: 0x000945D8
		internal virtual Grid PanTitle
		{
			[CompilerGenerated]
			get
			{
				return this.valIterator;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.FormDragMove);
				Grid grid = this.valIterator;
				if (grid != null)
				{
					grid.MouseLeftButtonDown -= value2;
				}
				this.valIterator = value;
				grid = this.valIterator;
				if (grid != null)
				{
					grid.MouseLeftButtonDown += value2;
				}
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x0000CD68 File Offset: 0x0000AF68
		// (set) Token: 0x060016E2 RID: 5858 RVA: 0x0000CD70 File Offset: 0x0000AF70
		internal virtual MyImage ImgTitle { get; set; }

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x0000CD79 File Offset: 0x0000AF79
		// (set) Token: 0x060016E4 RID: 5860 RVA: 0x0009641C File Offset: 0x0009461C
		internal virtual MyIconButton BtnTitleClose
		{
			[CompilerGenerated]
			get
			{
				return this.candidateIterator;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = delegate(object sender, EventArgs e)
				{
					this.BtnTitleClose_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
				};
				MyIconButton myIconButton = this.candidateIterator;
				if (myIconButton != null)
				{
					myIconButton.Click -= value2;
				}
				this.candidateIterator = value;
				myIconButton = this.candidateIterator;
				if (myIconButton != null)
				{
					myIconButton.Click += value2;
				}
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x0000CD81 File Offset: 0x0000AF81
		// (set) Token: 0x060016E6 RID: 5862 RVA: 0x00096460 File Offset: 0x00094660
		internal virtual MyIconButton BtnTitleMin
		{
			[CompilerGenerated]
			get
			{
				return this.advisorIterator;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = delegate(object sender, EventArgs e)
				{
					this.BtnTitleMin_Click();
				};
				MyIconButton myIconButton = this.advisorIterator;
				if (myIconButton != null)
				{
					myIconButton.Click -= value2;
				}
				this.advisorIterator = value;
				myIconButton = this.advisorIterator;
				if (myIconButton != null)
				{
					myIconButton.Click += value2;
				}
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x0000CD89 File Offset: 0x0000AF89
		// (set) Token: 0x060016E8 RID: 5864 RVA: 0x0000CD91 File Offset: 0x0000AF91
		internal virtual Grid PanTitleMain { get; set; }

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x0000CD9A File Offset: 0x0000AF9A
		// (set) Token: 0x060016EA RID: 5866 RVA: 0x0000CDA2 File Offset: 0x0000AFA2
		internal virtual Path ShapeTitleLogo { get; set; }

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x0000CDAB File Offset: 0x0000AFAB
		// (set) Token: 0x060016EC RID: 5868 RVA: 0x0000CDB3 File Offset: 0x0000AFB3
		internal virtual TextBlock LabTitleLogo { get; set; }

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x0000CDBC File Offset: 0x0000AFBC
		// (set) Token: 0x060016EE RID: 5870 RVA: 0x0000CDC4 File Offset: 0x0000AFC4
		internal virtual MyImage ImageTitleLogo { get; set; }

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0000CDCD File Offset: 0x0000AFCD
		// (set) Token: 0x060016F0 RID: 5872 RVA: 0x0000CDD5 File Offset: 0x0000AFD5
		internal virtual StackPanel PanTitleSelect { get; set; }

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0000CDDE File Offset: 0x0000AFDE
		// (set) Token: 0x060016F2 RID: 5874 RVA: 0x000964A4 File Offset: 0x000946A4
		internal virtual MyRadioButton BtnTitleSelect0
		{
			[CompilerGenerated]
			get
			{
				return this.m_WrapperIterator;
			}
			[CompilerGenerated]
			set
			{
				MyRadioButton.CheckEventHandler obj = delegate(object a0, bool a1)
				{
					this.BtnTitleSelect_Click((MyRadioButton)a0, a1);
				};
				MyRadioButton wrapperIterator = this.m_WrapperIterator;
				if (wrapperIterator != null)
				{
					wrapperIterator.ResolveTests(obj);
				}
				this.m_WrapperIterator = value;
				wrapperIterator = this.m_WrapperIterator;
				if (wrapperIterator != null)
				{
					wrapperIterator.LogoutTests(obj);
				}
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x0000CDE6 File Offset: 0x0000AFE6
		// (set) Token: 0x060016F4 RID: 5876 RVA: 0x000964E8 File Offset: 0x000946E8
		internal virtual MyRadioButton BtnTitleSelect1
		{
			[CompilerGenerated]
			get
			{
				return this._BaseIterator;
			}
			[CompilerGenerated]
			set
			{
				MyRadioButton.CheckEventHandler obj = delegate(object a0, bool a1)
				{
					this.BtnTitleSelect_Click((MyRadioButton)a0, a1);
				};
				MyRadioButton baseIterator = this._BaseIterator;
				if (baseIterator != null)
				{
					baseIterator.ResolveTests(obj);
				}
				this._BaseIterator = value;
				baseIterator = this._BaseIterator;
				if (baseIterator != null)
				{
					baseIterator.LogoutTests(obj);
				}
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x0000CDEE File Offset: 0x0000AFEE
		// (set) Token: 0x060016F6 RID: 5878 RVA: 0x0009652C File Offset: 0x0009472C
		internal virtual MyRadioButton BtnTitleSelect2
		{
			[CompilerGenerated]
			get
			{
				return this._AttributeIterator;
			}
			[CompilerGenerated]
			set
			{
				MyRadioButton.CheckEventHandler obj = delegate(object a0, bool a1)
				{
					this.BtnTitleSelect_Click((MyRadioButton)a0, a1);
				};
				MyRadioButton attributeIterator = this._AttributeIterator;
				if (attributeIterator != null)
				{
					attributeIterator.ResolveTests(obj);
				}
				this._AttributeIterator = value;
				attributeIterator = this._AttributeIterator;
				if (attributeIterator != null)
				{
					attributeIterator.LogoutTests(obj);
				}
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x0000CDF6 File Offset: 0x0000AFF6
		// (set) Token: 0x060016F8 RID: 5880 RVA: 0x00096570 File Offset: 0x00094770
		internal virtual MyRadioButton BtnTitleSelect3
		{
			[CompilerGenerated]
			get
			{
				return this._CodeIterator;
			}
			[CompilerGenerated]
			set
			{
				MyRadioButton.CheckEventHandler obj = delegate(object a0, bool a1)
				{
					this.BtnTitleSelect_Click((MyRadioButton)a0, a1);
				};
				MyRadioButton codeIterator = this._CodeIterator;
				if (codeIterator != null)
				{
					codeIterator.ResolveTests(obj);
				}
				this._CodeIterator = value;
				codeIterator = this._CodeIterator;
				if (codeIterator != null)
				{
					codeIterator.LogoutTests(obj);
				}
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x0000CDFE File Offset: 0x0000AFFE
		// (set) Token: 0x060016FA RID: 5882 RVA: 0x000965B4 File Offset: 0x000947B4
		internal virtual MyRadioButton BtnTitleSelect4
		{
			[CompilerGenerated]
			get
			{
				return this._PrototypeIterator;
			}
			[CompilerGenerated]
			set
			{
				MyRadioButton.CheckEventHandler obj = delegate(object a0, bool a1)
				{
					this.BtnTitleSelect_Click((MyRadioButton)a0, a1);
				};
				MyRadioButton prototypeIterator = this._PrototypeIterator;
				if (prototypeIterator != null)
				{
					prototypeIterator.ResolveTests(obj);
				}
				this._PrototypeIterator = value;
				prototypeIterator = this._PrototypeIterator;
				if (prototypeIterator != null)
				{
					prototypeIterator.LogoutTests(obj);
				}
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x0000CE06 File Offset: 0x0000B006
		// (set) Token: 0x060016FC RID: 5884 RVA: 0x0000CE0E File Offset: 0x0000B00E
		internal virtual Grid PanTitleInner { get; set; }

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x0000CE17 File Offset: 0x0000B017
		// (set) Token: 0x060016FE RID: 5886 RVA: 0x000965F8 File Offset: 0x000947F8
		internal virtual MyIconButton BtnTitleInner
		{
			[CompilerGenerated]
			get
			{
				return this.m_InfoIterator;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = delegate(object sender, EventArgs e)
				{
					this.PageBack();
				};
				MyIconButton infoIterator = this.m_InfoIterator;
				if (infoIterator != null)
				{
					infoIterator.Click -= value2;
				}
				this.m_InfoIterator = value;
				infoIterator = this.m_InfoIterator;
				if (infoIterator != null)
				{
					infoIterator.Click += value2;
				}
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x0000CE1F File Offset: 0x0000B01F
		// (set) Token: 0x06001700 RID: 5888 RVA: 0x0000CE27 File Offset: 0x0000B027
		internal virtual TextBlock LabTitleInner { get; set; }

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x0000CE30 File Offset: 0x0000B030
		// (set) Token: 0x06001702 RID: 5890 RVA: 0x0000CE38 File Offset: 0x0000B038
		internal virtual Grid PanLeft { get; set; }

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x0000CE41 File Offset: 0x0000B041
		// (set) Token: 0x06001704 RID: 5892 RVA: 0x0000CE49 File Offset: 0x0000B049
		internal virtual Rectangle RectLeftBackground { get; set; }

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x0000CE52 File Offset: 0x0000B052
		// (set) Token: 0x06001706 RID: 5894 RVA: 0x0000CE5A File Offset: 0x0000B05A
		internal virtual Rectangle RectLeftShadow { get; set; }

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x0000CE63 File Offset: 0x0000B063
		// (set) Token: 0x06001708 RID: 5896 RVA: 0x0000CE6B File Offset: 0x0000B06B
		internal virtual Grid PanMain { get; set; }

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x0000CE74 File Offset: 0x0000B074
		// (set) Token: 0x0600170A RID: 5898 RVA: 0x0000CE7C File Offset: 0x0000B07C
		internal virtual Border PanMainRight { get; set; }

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x0000CE85 File Offset: 0x0000B085
		// (set) Token: 0x0600170C RID: 5900 RVA: 0x0009663C File Offset: 0x0009483C
		internal virtual Border PanMainLeft
		{
			[CompilerGenerated]
			get
			{
				return this.comparatorIterator;
			}
			[CompilerGenerated]
			set
			{
				SizeChangedEventHandler value2 = new SizeChangedEventHandler(this.PanMainLeft_SizeChanged);
				Border border = this.comparatorIterator;
				if (border != null)
				{
					border.SizeChanged -= value2;
				}
				this.comparatorIterator = value;
				border = this.comparatorIterator;
				if (border != null)
				{
					border.SizeChanged += value2;
				}
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x0000CE8D File Offset: 0x0000B08D
		// (set) Token: 0x0600170E RID: 5902 RVA: 0x0000CE95 File Offset: 0x0000B095
		internal virtual StackPanel PanHint { get; set; }

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x0000CE9E File Offset: 0x0000B09E
		// (set) Token: 0x06001710 RID: 5904 RVA: 0x00096680 File Offset: 0x00094880
		internal virtual MyExtraButton BtnExtraBack
		{
			[CompilerGenerated]
			get
			{
				return this._TokenizerIterator;
			}
			[CompilerGenerated]
			set
			{
				MyExtraButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.BackToTop();
				};
				MyExtraButton tokenizerIterator = this._TokenizerIterator;
				if (tokenizerIterator != null)
				{
					tokenizerIterator.Click -= value2;
				}
				this._TokenizerIterator = value;
				tokenizerIterator = this._TokenizerIterator;
				if (tokenizerIterator != null)
				{
					tokenizerIterator.Click += value2;
				}
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x0000CEA6 File Offset: 0x0000B0A6
		// (set) Token: 0x06001712 RID: 5906 RVA: 0x000966C4 File Offset: 0x000948C4
		internal virtual MyExtraButton BtnExtraDownload
		{
			[CompilerGenerated]
			get
			{
				return this._FilterIterator;
			}
			[CompilerGenerated]
			set
			{
				MyExtraButton.ClickEventHandler value2 = new MyExtraButton.ClickEventHandler(this.BtnExtraDownload_Click);
				MyExtraButton filterIterator = this._FilterIterator;
				if (filterIterator != null)
				{
					filterIterator.Click -= value2;
				}
				this._FilterIterator = value;
				filterIterator = this._FilterIterator;
				if (filterIterator != null)
				{
					filterIterator.Click += value2;
				}
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x0000CEAE File Offset: 0x0000B0AE
		// (set) Token: 0x06001714 RID: 5908 RVA: 0x00096708 File Offset: 0x00094908
		internal virtual MyExtraButton BtnExtraApril
		{
			[CompilerGenerated]
			get
			{
				return this.databaseIterator;
			}
			[CompilerGenerated]
			set
			{
				MyExtraButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.AprilGiveup();
				};
				MyExtraButton myExtraButton = this.databaseIterator;
				if (myExtraButton != null)
				{
					myExtraButton.Click -= value2;
				}
				this.databaseIterator = value;
				myExtraButton = this.databaseIterator;
				if (myExtraButton != null)
				{
					myExtraButton.Click += value2;
				}
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x0000CEB6 File Offset: 0x0000B0B6
		// (set) Token: 0x06001716 RID: 5910 RVA: 0x0009674C File Offset: 0x0009494C
		internal virtual MyExtraButton BtnExtraShutdown
		{
			[CompilerGenerated]
			get
			{
				return this._PredicateIterator;
			}
			[CompilerGenerated]
			set
			{
				MyExtraButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.BtnExtraShutdown_Click();
				};
				MyExtraButton predicateIterator = this._PredicateIterator;
				if (predicateIterator != null)
				{
					predicateIterator.Click -= value2;
				}
				this._PredicateIterator = value;
				predicateIterator = this._PredicateIterator;
				if (predicateIterator != null)
				{
					predicateIterator.Click += value2;
				}
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x0000CEBE File Offset: 0x0000B0BE
		// (set) Token: 0x06001718 RID: 5912 RVA: 0x00096790 File Offset: 0x00094990
		internal virtual MyExtraButton BtnExtraMusic
		{
			[CompilerGenerated]
			get
			{
				return this.poolIterator;
			}
			[CompilerGenerated]
			set
			{
				MyExtraButton.ClickEventHandler value2 = new MyExtraButton.ClickEventHandler(this.BtnExtraMusic_Click);
				MyExtraButton.RightClickEventHandler obj = new MyExtraButton.RightClickEventHandler(this.BtnExtraMusic_RightClick);
				MyExtraButton myExtraButton = this.poolIterator;
				if (myExtraButton != null)
				{
					myExtraButton.Click -= value2;
					myExtraButton.CloneField(obj);
				}
				this.poolIterator = value;
				myExtraButton = this.poolIterator;
				if (myExtraButton != null)
				{
					myExtraButton.Click += value2;
					myExtraButton.IncludeField(obj);
				}
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x0000CEC6 File Offset: 0x0000B0C6
		// (set) Token: 0x0600171A RID: 5914 RVA: 0x000967F0 File Offset: 0x000949F0
		internal virtual Grid PanMsg
		{
			[CompilerGenerated]
			get
			{
				return this.m_CustomerIterator;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.FormDragMove);
				Grid customerIterator = this.m_CustomerIterator;
				if (customerIterator != null)
				{
					customerIterator.MouseLeftButtonDown -= value2;
				}
				this.m_CustomerIterator = value;
				customerIterator = this.m_CustomerIterator;
				if (customerIterator != null)
				{
					customerIterator.MouseLeftButtonDown += value2;
				}
			}
		}

		// Token: 0x04000B66 RID: 2918
		private bool m_DecoratorIterator;

		// Token: 0x04000B67 RID: 2919
		private static bool m_InstanceIterator = false;

		// Token: 0x04000B68 RID: 2920
		public bool m_StateIterator;

		// Token: 0x04000B69 RID: 2921
		public bool callbackIterator;

		// Token: 0x04000B6A RID: 2922
		private bool _TemplateIterator;

		// Token: 0x04000B6B RID: 2923
		public FormMain.PageStackData _MethodIterator;

		// Token: 0x04000B6C RID: 2924
		public FormMain.PageStackData taskIterator;

		// Token: 0x04000B6D RID: 2925
		public List<FormMain.PageStackData> _ConsumerIterator;

		// Token: 0x04000B6E RID: 2926
		public MyPageLeft configurationIterator;

		// Token: 0x04000B6F RID: 2927
		public MyPageRight _GetterIterator;

		// Token: 0x04000B70 RID: 2928
		private bool m_TokenIterator;

		// Token: 0x04000B71 RID: 2929
		public MouseEventArgs expressionIterator;

		// Token: 0x04000B72 RID: 2930
		[AccessedThroughProperty("WindMain")]
		[CompilerGenerated]
		private FormMain m_WriterIterator;

		// Token: 0x04000B73 RID: 2931
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private Grid registryIterator;

		// Token: 0x04000B74 RID: 2932
		[AccessedThroughProperty("TransformRotate")]
		[CompilerGenerated]
		private RotateTransform ruleIterator;

		// Token: 0x04000B75 RID: 2933
		[AccessedThroughProperty("TransformPos")]
		[CompilerGenerated]
		private TranslateTransform m_ProccesorIterator;

		// Token: 0x04000B76 RID: 2934
		[CompilerGenerated]
		[AccessedThroughProperty("ResizerT")]
		private Rectangle m_SetterIterator;

		// Token: 0x04000B77 RID: 2935
		[AccessedThroughProperty("ResizerB")]
		[CompilerGenerated]
		private Rectangle _FactoryIterator;

		// Token: 0x04000B78 RID: 2936
		[AccessedThroughProperty("ResizerR")]
		[CompilerGenerated]
		private Rectangle _ExporterIterator;

		// Token: 0x04000B79 RID: 2937
		[AccessedThroughProperty("ResizerL")]
		[CompilerGenerated]
		private Rectangle importerIterator;

		// Token: 0x04000B7A RID: 2938
		[AccessedThroughProperty("ResizerLT")]
		[CompilerGenerated]
		private Rectangle workerIterator;

		// Token: 0x04000B7B RID: 2939
		[CompilerGenerated]
		[AccessedThroughProperty("ResizerLB")]
		private Rectangle m_ConnectionIterator;

		// Token: 0x04000B7C RID: 2940
		[CompilerGenerated]
		[AccessedThroughProperty("ResizerRB")]
		private Rectangle _ServerIterator;

		// Token: 0x04000B7D RID: 2941
		[CompilerGenerated]
		[AccessedThroughProperty("ResizerRT")]
		private Rectangle m_ResolverIterator;

		// Token: 0x04000B7E RID: 2942
		[CompilerGenerated]
		[AccessedThroughProperty("BorderForm")]
		private Border _StatusIterator;

		// Token: 0x04000B7F RID: 2943
		[AccessedThroughProperty("RectForm")]
		[CompilerGenerated]
		private RectangleGeometry _RoleIterator;

		// Token: 0x04000B80 RID: 2944
		[CompilerGenerated]
		[AccessedThroughProperty("PanForm")]
		private Grid m_StructIterator;

		// Token: 0x04000B81 RID: 2945
		[AccessedThroughProperty("ImgBack")]
		[CompilerGenerated]
		private Canvas m_PrinterIterator;

		// Token: 0x04000B82 RID: 2946
		[AccessedThroughProperty("PanTitle")]
		[CompilerGenerated]
		private Grid valIterator;

		// Token: 0x04000B83 RID: 2947
		[CompilerGenerated]
		[AccessedThroughProperty("ImgTitle")]
		private MyImage attrIterator;

		// Token: 0x04000B84 RID: 2948
		[CompilerGenerated]
		[AccessedThroughProperty("BtnTitleClose")]
		private MyIconButton candidateIterator;

		// Token: 0x04000B85 RID: 2949
		[AccessedThroughProperty("BtnTitleMin")]
		[CompilerGenerated]
		private MyIconButton advisorIterator;

		// Token: 0x04000B86 RID: 2950
		[CompilerGenerated]
		[AccessedThroughProperty("PanTitleMain")]
		private Grid m_AccountIterator;

		// Token: 0x04000B87 RID: 2951
		[AccessedThroughProperty("ShapeTitleLogo")]
		[CompilerGenerated]
		private Path queueIterator;

		// Token: 0x04000B88 RID: 2952
		[AccessedThroughProperty("LabTitleLogo")]
		[CompilerGenerated]
		private TextBlock _EventIterator;

		// Token: 0x04000B89 RID: 2953
		[CompilerGenerated]
		[AccessedThroughProperty("ImageTitleLogo")]
		private MyImage _ManagerIterator;

		// Token: 0x04000B8A RID: 2954
		[CompilerGenerated]
		[AccessedThroughProperty("PanTitleSelect")]
		private StackPanel _ModelIterator;

		// Token: 0x04000B8B RID: 2955
		[CompilerGenerated]
		[AccessedThroughProperty("BtnTitleSelect0")]
		private MyRadioButton m_WrapperIterator;

		// Token: 0x04000B8C RID: 2956
		[AccessedThroughProperty("BtnTitleSelect1")]
		[CompilerGenerated]
		private MyRadioButton _BaseIterator;

		// Token: 0x04000B8D RID: 2957
		[CompilerGenerated]
		[AccessedThroughProperty("BtnTitleSelect2")]
		private MyRadioButton _AttributeIterator;

		// Token: 0x04000B8E RID: 2958
		[AccessedThroughProperty("BtnTitleSelect3")]
		[CompilerGenerated]
		private MyRadioButton _CodeIterator;

		// Token: 0x04000B8F RID: 2959
		[CompilerGenerated]
		[AccessedThroughProperty("BtnTitleSelect4")]
		private MyRadioButton _PrototypeIterator;

		// Token: 0x04000B90 RID: 2960
		[CompilerGenerated]
		[AccessedThroughProperty("PanTitleInner")]
		private Grid m_AnnotationIterator;

		// Token: 0x04000B91 RID: 2961
		[CompilerGenerated]
		[AccessedThroughProperty("BtnTitleInner")]
		private MyIconButton m_InfoIterator;

		// Token: 0x04000B92 RID: 2962
		[CompilerGenerated]
		[AccessedThroughProperty("LabTitleInner")]
		private TextBlock m_AdapterIterator;

		// Token: 0x04000B93 RID: 2963
		[CompilerGenerated]
		[AccessedThroughProperty("PanLeft")]
		private Grid facadeIterator;

		// Token: 0x04000B94 RID: 2964
		[CompilerGenerated]
		[AccessedThroughProperty("RectLeftBackground")]
		private Rectangle listIterator;

		// Token: 0x04000B95 RID: 2965
		[CompilerGenerated]
		[AccessedThroughProperty("RectLeftShadow")]
		private Rectangle m_MerchantIterator;

		// Token: 0x04000B96 RID: 2966
		[CompilerGenerated]
		[AccessedThroughProperty("PanMain")]
		private Grid authenticationIterator;

		// Token: 0x04000B97 RID: 2967
		[AccessedThroughProperty("PanMainRight")]
		[CompilerGenerated]
		private Border algoIterator;

		// Token: 0x04000B98 RID: 2968
		[CompilerGenerated]
		[AccessedThroughProperty("PanMainLeft")]
		private Border comparatorIterator;

		// Token: 0x04000B99 RID: 2969
		[AccessedThroughProperty("PanHint")]
		[CompilerGenerated]
		private StackPanel _MappingIterator;

		// Token: 0x04000B9A RID: 2970
		[AccessedThroughProperty("BtnExtraBack")]
		[CompilerGenerated]
		private MyExtraButton _TokenizerIterator;

		// Token: 0x04000B9B RID: 2971
		[AccessedThroughProperty("BtnExtraDownload")]
		[CompilerGenerated]
		private MyExtraButton _FilterIterator;

		// Token: 0x04000B9C RID: 2972
		[CompilerGenerated]
		[AccessedThroughProperty("BtnExtraApril")]
		private MyExtraButton databaseIterator;

		// Token: 0x04000B9D RID: 2973
		[CompilerGenerated]
		[AccessedThroughProperty("BtnExtraShutdown")]
		private MyExtraButton _PredicateIterator;

		// Token: 0x04000B9E RID: 2974
		[AccessedThroughProperty("BtnExtraMusic")]
		[CompilerGenerated]
		private MyExtraButton poolIterator;

		// Token: 0x04000B9F RID: 2975
		[CompilerGenerated]
		[AccessedThroughProperty("PanMsg")]
		private Grid m_CustomerIterator;

		// Token: 0x020001E7 RID: 487
		public enum PageType
		{
			// Token: 0x04000BA2 RID: 2978
			Launch,
			// Token: 0x04000BA3 RID: 2979
			Download,
			// Token: 0x04000BA4 RID: 2980
			Link,
			// Token: 0x04000BA5 RID: 2981
			Setup,
			// Token: 0x04000BA6 RID: 2982
			Other,
			// Token: 0x04000BA7 RID: 2983
			VersionSelect,
			// Token: 0x04000BA8 RID: 2984
			DownloadManager,
			// Token: 0x04000BA9 RID: 2985
			VersionSetup,
			// Token: 0x04000BAA RID: 2986
			CompDetail,
			// Token: 0x04000BAB RID: 2987
			HelpDetail
		}

		// Token: 0x020001E8 RID: 488
		public enum PageSubType
		{
			// Token: 0x04000BAD RID: 2989
			Default,
			// Token: 0x04000BAE RID: 2990
			DownloadInstall,
			// Token: 0x04000BAF RID: 2991
			DownloadClient = 4,
			// Token: 0x04000BB0 RID: 2992
			DownloadOptiFine,
			// Token: 0x04000BB1 RID: 2993
			DownloadForge,
			// Token: 0x04000BB2 RID: 2994
			DownloadNeoForge,
			// Token: 0x04000BB3 RID: 2995
			DownloadFabric,
			// Token: 0x04000BB4 RID: 2996
			DownloadLiteLoader,
			// Token: 0x04000BB5 RID: 2997
			DownloadMod = 11,
			// Token: 0x04000BB6 RID: 2998
			DownloadPack,
			// Token: 0x04000BB7 RID: 2999
			SetupLaunch = 0,
			// Token: 0x04000BB8 RID: 3000
			SetupUI,
			// Token: 0x04000BB9 RID: 3001
			SetupSystem,
			// Token: 0x04000BBA RID: 3002
			SetupLink,
			// Token: 0x04000BBB RID: 3003
			LinkHiper = 1,
			// Token: 0x04000BBC RID: 3004
			LinkIoi,
			// Token: 0x04000BBD RID: 3005
			LinkSetup = 4,
			// Token: 0x04000BBE RID: 3006
			LinkHelp,
			// Token: 0x04000BBF RID: 3007
			LinkFeedback,
			// Token: 0x04000BC0 RID: 3008
			OtherHelp = 0,
			// Token: 0x04000BC1 RID: 3009
			OtherAbout,
			// Token: 0x04000BC2 RID: 3010
			OtherTest,
			// Token: 0x04000BC3 RID: 3011
			VersionOverall = 0,
			// Token: 0x04000BC4 RID: 3012
			VersionSetup,
			// Token: 0x04000BC5 RID: 3013
			VersionMod,
			// Token: 0x04000BC6 RID: 3014
			VersionModDisabled
		}

		// Token: 0x020001E9 RID: 489
		public class PageStackData
		{
			// Token: 0x0600173F RID: 5951 RVA: 0x00097148 File Offset: 0x00095348
			public override bool Equals(object other)
			{
				bool result;
				if (other == null)
				{
					result = false;
				}
				else if (other is FormMain.PageStackData)
				{
					FormMain.PageStackData pageStackData = (FormMain.PageStackData)other;
					if (this.initializerMap != pageStackData.initializerMap)
					{
						result = false;
					}
					else if (this.m_SingletonMap == null)
					{
						result = (pageStackData.m_SingletonMap == null);
					}
					else
					{
						result = (pageStackData.m_SingletonMap != null && this.m_SingletonMap.Equals(RuntimeHelpers.GetObjectValue(pageStackData.m_SingletonMap)));
					}
				}
				else
				{
					result = (other is int && !Operators.ConditionalCompareObjectNotEqual(this.initializerMap, other, false) && this.m_SingletonMap == null);
				}
				return result;
			}

			// Token: 0x06001740 RID: 5952 RVA: 0x0000CFDC File Offset: 0x0000B1DC
			public static bool operator ==(FormMain.PageStackData left, FormMain.PageStackData right)
			{
				return EqualityComparer<FormMain.PageStackData>.Default.Equals(left, right);
			}

			// Token: 0x06001741 RID: 5953 RVA: 0x0000CFEA File Offset: 0x0000B1EA
			public static bool operator !=(FormMain.PageStackData left, FormMain.PageStackData right)
			{
				return !(left == right);
			}

			// Token: 0x06001742 RID: 5954 RVA: 0x0000CFF6 File Offset: 0x0000B1F6
			public static implicit operator FormMain.PageStackData(FormMain.PageType Value)
			{
				return new FormMain.PageStackData
				{
					initializerMap = Value
				};
			}

			// Token: 0x06001743 RID: 5955 RVA: 0x0000D004 File Offset: 0x0000B204
			public static implicit operator FormMain.PageType(FormMain.PageStackData Value)
			{
				return Value.initializerMap;
			}

			// Token: 0x04000BC7 RID: 3015
			public FormMain.PageType initializerMap;

			// Token: 0x04000BC8 RID: 3016
			public object[] m_SingletonMap;
		}
	}
}
