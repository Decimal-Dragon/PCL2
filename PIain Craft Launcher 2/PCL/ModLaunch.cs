using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PCL
{
	// Token: 0x02000160 RID: 352
	[StandardModule]
	public sealed class ModLaunch
	{
		// Token: 0x06000DFE RID: 3582 RVA: 0x0005FE8C File Offset: 0x0005E08C
		public static bool McLaunchStart(ModLaunch.McLaunchOptions Options = null)
		{
			ModLaunch.filterTests = (Options ?? new ModLaunch.McLaunchOptions());
			if (!ModBase.RunInUi())
			{
				throw new Exception("McLaunchStart 必须在 UI 线程调用！");
			}
			bool result;
			if (ModLaunch.databaseTests.State == ModBase.LoadState.Loading)
			{
				ModMain.Hint("已有游戏正在启动中！", ModMain.HintType.Critical, true);
				result = false;
			}
			else
			{
				if (ModLaunch.filterTests._DicMap != null && ModMinecraft.AddClient() != ModLaunch.filterTests._DicMap)
				{
					ModLaunch.McLaunchLog("在启动前切换到版本 " + ModLaunch.filterTests._DicMap.Name);
					ModLaunch.filterTests._DicMap.Load();
					if (ModLaunch.filterTests._DicMap._ConfigurationMap == ModMinecraft.McVersionState.Error)
					{
						ModMain.Hint("无法启动 Minecraft：" + ModLaunch.filterTests._DicMap._ConsumerMap, ModMain.HintType.Critical, true);
						return false;
					}
					ModMinecraft.InstantiateClient(ModLaunch.filterTests._DicMap);
					ModBase.m_IdentifierRepository.Set("LaunchVersionSelect", ModMinecraft.AddClient().Name, false, null);
					ModMain.recordIterator.RefreshButtonsUI();
					ModMain.recordIterator.RefreshPage(false, false);
				}
				ModMain._ProcessIterator.AprilGiveup();
				ModMain._ProcessIterator._ConsumerIterator = Enumerable.ToList<FormMain.PageStackData>(Enumerable.Where<FormMain.PageStackData>(ModMain._ProcessIterator._ConsumerIterator, (ModLaunch._Closure$__.$I3-0 == null) ? (ModLaunch._Closure$__.$I3-0 = ((FormMain.PageStackData p) => p.initializerMap != FormMain.PageType.VersionSelect)) : ModLaunch._Closure$__.$I3-0));
				ModLaunch.databaseTests.Start(Options, true);
				result = true;
			}
			return result;
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00060004 File Offset: 0x0005E204
		public static void McLaunchLog(string Text)
		{
			Text = ModSecret.SecretFilter(Text, '*');
			ModBase.RunInUi(delegate()
			{
				TextBlock labLog;
				(labLog = ModMain.m_ServiceIterator.LabLog).Text = string.Concat(new string[]
				{
					labLog.Text,
					"\r\n[",
					ModBase.GetTimeNow(),
					"] ",
					Text
				});
			}, false);
			ModBase.Log("[Launch] " + Text, ModBase.LogLevel.Normal, "出现错误");
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00060060 File Offset: 0x0005E260
		private static void McLaunchState(ModLoader.LoaderTask<ModLaunch.McLaunchOptions, object> Loader)
		{
			switch (ModLaunch.databaseTests.State)
			{
			case ModBase.LoadState.Waiting:
			case ModBase.LoadState.Finished:
			case ModBase.LoadState.Failed:
			case ModBase.LoadState.Aborted:
				ModMain.recordIterator.PageChangeToLogin();
				return;
			case ModBase.LoadState.Loading:
				ModMain.m_ServiceIterator.LabLog.Text = "";
				return;
			default:
				return;
			}
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x000600B4 File Offset: 0x0005E2B4
		private static void McLaunchStart(ModLoader.LoaderTask<ModLaunch.McLaunchOptions, object> Loader)
		{
			ModBase.RunInUiWait(new Action(ModMain.recordIterator.PageChangeToLaunching));
			try
			{
				ModLaunch.McLaunchPrecheck();
				ModLaunch.McLaunchLog("预检测已通过");
			}
			catch (Exception ex)
			{
				if (!ex.Message.StartsWithF("$$", false))
				{
					ModMain.Hint(ex.Message, ModMain.HintType.Critical, true);
				}
				throw;
			}
			try
			{
				List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>
				{
					new ModLoader.LoaderTask<int, int>("获取 Java", new Action<ModLoader.LoaderTask<int, int>>(ModLaunch.McLaunchJava), null, ThreadPriority.Normal)
					{
						ProgressWeight = 4.0,
						Block = false
					},
					ModLaunch.interceptorTests,
					new ModLoader.LoaderCombo<string>("补全文件", ModDownload.DlClientFix(ModMinecraft.AddClient(), false, ModDownload.AssetsIndexExistsBehaviour.DownloadInBackground))
					{
						ProgressWeight = 15.0,
						Show = false
					},
					new ModLoader.LoaderTask<string, List<ModMinecraft.McLibToken>>("获取启动参数", new Action<ModLoader.LoaderTask<string, List<ModMinecraft.McLibToken>>>(ModLaunch.McLaunchArgumentMain), null, ThreadPriority.Normal)
					{
						ProgressWeight = 2.0
					},
					new ModLoader.LoaderTask<List<ModMinecraft.McLibToken>, int>("解压文件", new Action<ModLoader.LoaderTask<List<ModMinecraft.McLibToken>, int>>(ModLaunch.McLaunchNatives), null, ThreadPriority.Normal)
					{
						ProgressWeight = 2.0
					},
					new ModLoader.LoaderTask<int, int>("预启动处理", (ModLaunch._Closure$__.$IR11-2 == null) ? (ModLaunch._Closure$__.$IR11-2 = delegate(ModLoader.LoaderTask<int, int> a0)
					{
						ModLaunch.McLaunchPrerun();
					}) : ModLaunch._Closure$__.$IR11-2, null, ThreadPriority.Normal)
					{
						ProgressWeight = 1.0
					},
					new ModLoader.LoaderTask<int, int>("执行自定义命令", new Action<ModLoader.LoaderTask<int, int>>(ModLaunch.McLaunchCustom), null, ThreadPriority.Normal)
					{
						ProgressWeight = 1.0
					},
					new ModLoader.LoaderTask<int, Process>("启动进程", new Action<ModLoader.LoaderTask<int, Process>>(ModLaunch.McLaunchRun), null, ThreadPriority.Normal)
					{
						ProgressWeight = 2.0
					},
					new ModLoader.LoaderTask<Process, int>("等待游戏窗口出现", new Action<ModLoader.LoaderTask<Process, int>>(ModLaunch.McLaunchWait), null, ThreadPriority.Normal)
					{
						ProgressWeight = 1.0
					},
					new ModLoader.LoaderTask<int, int>("结束处理", (ModLaunch._Closure$__.$IR11-3 == null) ? (ModLaunch._Closure$__.$IR11-3 = delegate(ModLoader.LoaderTask<int, int> a0)
					{
						ModLaunch.McLaunchEnd();
					}) : ModLaunch._Closure$__.$IR11-3, null, ThreadPriority.Normal)
					{
						ProgressWeight = 1.0
					}
				};
				object left = ModBase.m_IdentifierRepository.Get("VersionRamOptimize", ModMinecraft.AddClient());
				if (Operators.ConditionalCompareObjectEqual(left, 0, false))
				{
					if (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LaunchArgumentRam", null)))
					{
						((ModLoader.LoaderCombo<string>)list[2]).Block = false;
						list.Insert(3, new ModLoader.LoaderTask<int, int>("内存优化", new Action<ModLoader.LoaderTask<int, int>>(ModLaunch.McLaunchMemoryOptimize), null, ThreadPriority.Normal)
						{
							ProgressWeight = 30.0
						});
					}
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 1, false))
				{
					((ModLoader.LoaderCombo<string>)list[2]).Block = false;
					list.Insert(3, new ModLoader.LoaderTask<int, int>("内存优化", new Action<ModLoader.LoaderTask<int, int>>(ModLaunch.McLaunchMemoryOptimize), null, ThreadPriority.Normal)
					{
						ProgressWeight = 30.0
					});
				}
				else
				{
					Operators.ConditionalCompareObjectEqual(left, 2, false);
				}
				ModLoader.LoaderCombo<object> loaderCombo = new ModLoader.LoaderCombo<object>("Minecraft 启动", list)
				{
					Show = false
				};
				if (ModLaunch.interceptorTests.State == ModBase.LoadState.Finished)
				{
					ModLaunch.interceptorTests.State = ModBase.LoadState.Waiting;
				}
				ModLaunch.m_PredicateTests = loaderCombo;
				ModLaunch._PageTests = null;
				loaderCombo.Start(null, false);
				ModLoader.LoaderTaskbarAdd<object>(loaderCombo);
				while (loaderCombo.State == ModBase.LoadState.Loading)
				{
					ModMain.recordIterator.Dispatcher.Invoke(new Action(ModMain.recordIterator.LaunchingRefresh));
					Thread.Sleep(200);
				}
				ModMain.recordIterator.Dispatcher.Invoke(new Action(ModMain.recordIterator.LaunchingRefresh));
				switch (loaderCombo.State)
				{
				case ModBase.LoadState.Finished:
					ModMain.Hint(ModMinecraft.AddClient().Name + " 启动成功！", ModMain.HintType.Finish, true);
					break;
				case ModBase.LoadState.Failed:
					throw loaderCombo.Error;
				case ModBase.LoadState.Aborted:
					if (ModLaunch._PageTests == null)
					{
						ModLaunch.McLaunchOptions mcLaunchOptions = ModLaunch.filterTests;
						ModMain.Hint((((mcLaunchOptions != null) ? mcLaunchOptions.requestMap : null) == null) ? "已取消启动！" : "已取消导出启动脚本！", ModMain.HintType.Info, true);
					}
					else
					{
						ModMain.Hint(ModLaunch._PageTests, ModMain.HintType.Finish, true);
					}
					break;
				default:
					throw new Exception("错误的状态改变：" + ModBase.GetStringFromEnum(loaderCombo.State));
				}
			}
			catch (Exception ex2)
			{
				Exception ex3 = ex2;
				while (!ex3.Message.StartsWithF("$", false))
				{
					if (ex3.InnerException == null)
					{
						ModLaunch.McLaunchLog("错误：" + ModBase.GetExceptionDetail(ex2, false));
						Exception ex4 = ex2;
						ModLaunch.McLaunchOptions mcLaunchOptions2 = ModLaunch.filterTests;
						string desc = (((mcLaunchOptions2 != null) ? mcLaunchOptions2.requestMap : null) == null) ? "Minecraft 启动失败" : "导出启动脚本失败";
						ModBase.LogLevel level = ModBase.LogLevel.Msgbox;
						ModLaunch.McLaunchOptions mcLaunchOptions3 = ModLaunch.filterTests;
						ModBase.Log(ex4, desc, level, (((mcLaunchOptions3 != null) ? mcLaunchOptions3.requestMap : null) == null) ? "启动失败" : "导出启动脚本失败");
						throw;
					}
					ex3 = ex3.InnerException;
				}
				if (Operators.CompareString(ex3.Message, "$$", false) != 0)
				{
					string caption = ex3.Message.TrimStart(new char[]
					{
						'$'
					});
					ModLaunch.McLaunchOptions mcLaunchOptions4 = ModLaunch.filterTests;
					ModMain.MyMsgBox(caption, (((mcLaunchOptions4 != null) ? mcLaunchOptions4.requestMap : null) == null) ? "启动失败" : "导出启动脚本失败", "确定", "", "", false, true, false, null, null, null);
				}
				throw;
			}
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0006063C File Offset: 0x0005E83C
		private static void McLaunchMemoryOptimize(ModLoader.LoaderTask<int, int> Loader)
		{
			ModLaunch._Closure$__12-0 CS$<>8__locals1 = new ModLaunch._Closure$__12-0(CS$<>8__locals1);
			ModLaunch.McLaunchLog("内存优化开始");
			CS$<>8__locals1.$VB$Local_Finished = false;
			ModBase.RunInNewThread(delegate
			{
				PageOtherTest.MemoryOptimize(false);
				CS$<>8__locals1.$VB$Local_Finished = true;
			}, "Launch Memory Optimize", ThreadPriority.Normal);
			while (!CS$<>8__locals1.$VB$Local_Finished && !Loader.IsAborted)
			{
				if (Loader.Progress < 0.7)
				{
					Loader.Progress += 0.007;
				}
				else
				{
					Loader.Progress += (0.95 - Loader.Progress) * 0.02;
				}
				Thread.Sleep(100);
			}
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x000606E8 File Offset: 0x0005E8E8
		private static void McLaunchPrecheck()
		{
			if (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("SystemDebugDelay", null)))
			{
				Thread.Sleep(ModBase.RandomInteger(100, 2000));
			}
			if (ModMinecraft.AddClient().ChangeMapper().Contains("!") || ModMinecraft.AddClient().ChangeMapper().Contains(";"))
			{
				throw new Exception("游戏路径中不可包含 ! 或 ;（" + ModMinecraft.AddClient().ChangeMapper() + "）");
			}
			if (ModMinecraft.AddClient().Path.Contains("!") || ModMinecraft.AddClient().Path.Contains(";"))
			{
				throw new Exception("游戏路径中不可包含 ! 或 ;（" + ModMinecraft.AddClient().Path + "）");
			}
			if (ModMinecraft.AddClient() == null)
			{
				throw new Exception("未选择 Minecraft 版本！");
			}
			ModMinecraft.AddClient().Load();
			if (ModMinecraft.AddClient()._ConfigurationMap == ModMinecraft.McVersionState.Error)
			{
				throw new Exception("Minecraft 存在问题：" + ModMinecraft.AddClient()._ConsumerMap);
			}
			string CheckResult = "";
			ModBase.RunInUiWait(delegate()
			{
				CheckResult = ModLaunch.McLoginAble(ModLaunch.McLoginInput());
			});
			if (Operators.CompareString(CheckResult, "", false) != 0)
			{
				throw new ArgumentException(CheckResult);
			}
			ModBase.RunInNewThread((ModLaunch._Closure$__.$I13-1 == null) ? (ModLaunch._Closure$__.$I13-1 = delegate()
			{
				object left2 = ModBase.m_IdentifierRepository.Get("SystemLaunchCount", null);
				if (Conversions.ToBoolean(Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 10, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 20, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 40, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 60, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 80, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 100, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 120, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 150, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 200, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 250, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 300, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 350, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 400, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 500, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 600, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 700, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 800, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 900, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 1000, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 1200, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 1400, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 1600, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 1800, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, 2000, false))) && ModMain.MyMsgBox(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("PCL 已经为你启动了 ", ModBase.m_IdentifierRepository.Get("SystemLaunchCount", null)), " 次游戏啦！"), "\r\n"), "如果 PCL 还算好用的话，能不能考虑赞助一下 PCL……"), "\r\n"), "如果没有大家的支持，PCL 很难在免费、无任何广告的情况下维持数年的更新（磕头）……！")), Conversions.ToString(Operators.ConcatenateObject(ModBase.m_IdentifierRepository.Get("SystemLaunchCount", null), " 次启动！")), "支持 PCL！", "但是我拒绝", "", false, true, false, null, null, null) == 1)
				{
					ModBase.OpenWebsite("https://afdian.com/a/LTCat");
				}
			}) : ModLaunch._Closure$__.$I13-1, "Donate", ThreadPriority.Normal);
			if (Conversions.ToBoolean(Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("HintBuy", null))) && Operators.ConditionalCompareObjectNotEqual(ModBase.m_IdentifierRepository.Get("LoginType", null), ModLaunch.McLoginType.Ms, false)))
			{
				if (ModBase.IsSystemLanguageChinese())
				{
					object left = ModBase.m_IdentifierRepository.Get("SystemLaunchCount", null);
					if (Conversions.ToBoolean(Conversions.ToBoolean(Operators.CompareObjectEqual(left, 3, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 8, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 15, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 30, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 50, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 70, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 90, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 110, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 130, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 180, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 220, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 280, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 330, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 380, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 450, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 550, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 660, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 750, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 880, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 950, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 1100, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 1300, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 1500, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 1700, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 1900, false))) && ModMain.MyMsgBox(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("你已经启动了 ", ModBase.m_IdentifierRepository.Get("SystemLaunchCount", null)), " 次 Minecraft 啦！"), "\r\n"), "如果觉得 Minecraft 还不错，可以购买正版支持一下，毕竟开发游戏也真的很不容易……不要一直白嫖啦。"), "\r\n"), "\r\n"), "在登录一次正版账号后，就不会再出现这个提示了！")), "考虑一下正版？", "支持正版游戏！", "下次一定", "", false, true, false, null, null, null) == 1)
					{
						ModBase.OpenWebsite("https://www.xbox.com/zh-cn/games/store/minecraft-java-bedrock-edition-for-pc/9nxp44l49shj");
						return;
					}
				}
				else if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LoginType", null), ModLaunch.McLoginType.Legacy, false))
				{
					int num = ModMain.MyMsgBox("你必须先登录正版账号，才能进行离线登录！", "正版验证", "购买正版", "试玩", "返回", false, true, false, (ModLaunch._Closure$__.$I13-2 == null) ? (ModLaunch._Closure$__.$I13-2 = delegate()
					{
						ModBase.OpenWebsite("https://www.xbox.com/zh-cn/games/store/minecraft-java-bedrock-edition-for-pc/9nxp44l49shj");
					}) : ModLaunch._Closure$__.$I13-2, null, null);
					if (num == 2)
					{
						ModMain.Hint("游戏将以试玩模式启动！", ModMain.HintType.Critical, true);
						ModLaunch.filterTests._HelperMap.Add("--demo");
						return;
					}
					if (num != 3)
					{
						return;
					}
					throw new Exception("$$");
				}
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00060C88 File Offset: 0x0005EE88
		public static string McLoginName()
		{
			object left = ModBase.m_IdentifierRepository.Get("LoginType", null);
			if (Operators.ConditionalCompareObjectEqual(left, ModLaunch.McLoginType.Ms, false))
			{
				if (Operators.ConditionalCompareObjectNotEqual(ModBase.m_IdentifierRepository.Get("CacheMsV2Name", null), "", false))
				{
					return Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheMsV2Name", null));
				}
			}
			else if (Operators.ConditionalCompareObjectEqual(left, ModLaunch.McLoginType.Legacy, false))
			{
				if (Operators.ConditionalCompareObjectNotEqual(ModBase.m_IdentifierRepository.Get("LoginLegacyName", null), "", false))
				{
					return ModBase.m_IdentifierRepository.Get("LoginLegacyName", null).ToString().BeforeFirst("¨", false);
				}
			}
			else if (Operators.ConditionalCompareObjectEqual(left, ModLaunch.McLoginType.Nide, false))
			{
				if (Operators.ConditionalCompareObjectNotEqual(ModBase.m_IdentifierRepository.Get("CacheNideName", null), "", false))
				{
					return Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheNideName", null));
				}
			}
			else if (Operators.ConditionalCompareObjectEqual(left, ModLaunch.McLoginType.Auth, false) && Operators.ConditionalCompareObjectNotEqual(ModBase.m_IdentifierRepository.Get("CacheAuthName", null), "", false))
			{
				return Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheAuthName", null));
			}
			string result;
			if (Operators.ConditionalCompareObjectNotEqual(ModBase.m_IdentifierRepository.Get("CacheMsV2Name", null), "", false))
			{
				result = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheMsV2Name", null));
			}
			else if (Operators.ConditionalCompareObjectNotEqual(ModBase.m_IdentifierRepository.Get("CacheNideName", null), "", false))
			{
				result = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheNideName", null));
			}
			else if (Operators.ConditionalCompareObjectNotEqual(ModBase.m_IdentifierRepository.Get("CacheAuthName", null), "", false))
			{
				result = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheAuthName", null));
			}
			else if (Operators.ConditionalCompareObjectNotEqual(ModBase.m_IdentifierRepository.Get("LoginLegacyName", null), "", false))
			{
				result = ModBase.m_IdentifierRepository.Get("LoginLegacyName", null).ToString().BeforeFirst("¨", false);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00060EB8 File Offset: 0x0005F0B8
		public static string McLoginAble()
		{
			object left = ModBase.m_IdentifierRepository.Get("LoginType", null);
			string result;
			if (Operators.ConditionalCompareObjectEqual(left, ModLaunch.McLoginType.Ms, false))
			{
				if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("CacheMsV2OAuthRefresh", null), "", false))
				{
					result = ModMain.m_ClientRepository.IsVaild();
				}
				else
				{
					result = "";
				}
			}
			else if (Operators.ConditionalCompareObjectEqual(left, ModLaunch.McLoginType.Legacy, false))
			{
				result = ModMain.m_ProcIterator.IsVaild();
			}
			else if (Operators.ConditionalCompareObjectEqual(left, ModLaunch.McLoginType.Nide, false))
			{
				if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("CacheNideAccess", null), "", false))
				{
					result = ModMain.parserRepository.IsVaild();
				}
				else
				{
					result = "";
				}
			}
			else if (Operators.ConditionalCompareObjectEqual(left, ModLaunch.McLoginType.Auth, false))
			{
				if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("CacheAuthAccess", null), "", false))
				{
					result = ModMain.fieldRepository.IsVaild();
				}
				else
				{
					result = "";
				}
			}
			else
			{
				result = "未知的登录方式";
			}
			return result;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00060FC8 File Offset: 0x0005F1C8
		public static string McLoginAble(ModLaunch.McLoginData LoginData)
		{
			switch (LoginData.Type)
			{
			case ModLaunch.McLoginType.Legacy:
				return PageLoginLegacy.IsVaild((ModLaunch.McLoginLegacy)LoginData);
			case ModLaunch.McLoginType.Nide:
				return PageLoginNide.IsVaild((ModLaunch.McLoginServer)LoginData);
			case ModLaunch.McLoginType.Auth:
				return PageLoginAuth.IsVaild((ModLaunch.McLoginServer)LoginData);
			case ModLaunch.McLoginType.Ms:
				return PageLoginMs.IsVaild((ModLaunch.McLoginMs)LoginData);
			}
			return "未知的登录方式";
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0006103C File Offset: 0x0005F23C
		public static ModLaunch.McLoginData McLoginInput()
		{
			ModLaunch.McLoginData result = null;
			ModLaunch.McLoginType mcLoginType = (ModLaunch.McLoginType)Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("LoginType", null));
			try
			{
				switch (mcLoginType)
				{
				case ModLaunch.McLoginType.Legacy:
					result = PageLoginLegacy.GetLoginData();
					break;
				case ModLaunch.McLoginType.Nide:
					if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("CacheNideAccess", null), "", false))
					{
						result = PageLoginNide.GetLoginData();
					}
					else
					{
						result = PageLoginNideSkin.GetLoginData();
					}
					break;
				case ModLaunch.McLoginType.Auth:
					if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("CacheAuthAccess", null), "", false))
					{
						result = PageLoginAuth.GetLoginData();
					}
					else
					{
						result = PageLoginAuthSkin.GetLoginData();
					}
					break;
				case ModLaunch.McLoginType.Ms:
					if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("CacheMsV2OAuthRefresh", null), "", false))
					{
						result = PageLoginMs.GetLoginData();
					}
					else
					{
						result = PageLoginMsSkin.GetLoginData();
					}
					break;
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "获取登录输入信息失败（" + ModBase.GetStringFromEnum(mcLoginType) + "）", ModBase.LogLevel.Feedback, "出现错误");
			}
			return result;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0006115C File Offset: 0x0005F35C
		private static void McLoginStart(ModLoader.LoaderTask<ModLaunch.McLoginData, ModLaunch.McLoginResult> Data)
		{
			ModLaunch.McLaunchLog("登录加载已开始");
			string text = ModLaunch.McLoginAble(Data.Input);
			if (Operators.CompareString(text, "", false) != 0)
			{
				throw new ArgumentException(text);
			}
			ModLoader.LoaderBase loaderBase = null;
			switch (Data.Input.Type)
			{
			case ModLaunch.McLoginType.Legacy:
				loaderBase = ModLaunch._ParamsTests;
				break;
			case ModLaunch.McLoginType.Nide:
				loaderBase = ModLaunch.dispatcherTests;
				break;
			case ModLaunch.McLoginType.Auth:
				loaderBase = ModLaunch._ProcessTests;
				break;
			case ModLaunch.McLoginType.Ms:
				loaderBase = ModLaunch.m_ContainerTests;
				break;
			}
			loaderBase.WaitForExit(Data.Input, ModLaunch.interceptorTests, Data.IsForceRestarting);
			object obj = NewLateBinding.LateGet(loaderBase, null, "Output", new object[0], null, null, null);
			Data.Output = ((obj != null) ? ((ModLaunch.McLoginResult)obj) : default(ModLaunch.McLoginResult));
			ModBase.RunInUi((ModLaunch._Closure$__.$I25-0 == null) ? (ModLaunch._Closure$__.$I25-0 = delegate()
			{
				ModMain.recordIterator.RefreshPage(true, false);
			}) : ModLaunch._Closure$__.$I25-0, false);
			ModLaunch.McLaunchLog("登录加载已结束");
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0006125C File Offset: 0x0005F45C
		private static void McLoginMsStart(ModLoader.LoaderTask<ModLaunch.McLoginMs, ModLaunch.McLoginResult> Data)
		{
			ModLaunch.McLoginMs input = Data.Input;
			string observerMap = input.observerMap;
			ModLaunch.McLaunchLog("登录方式：正版（" + ((Operators.CompareString(observerMap, "", false) == 0) ? "尚未登录" : observerMap) + "）");
			Data.Progress = 0.05;
			if (!Data.IsForceRestarting && Operators.CompareString(input.paramMap, "", false) != 0 && ModLaunch.m_ParameterTests > 0L && checked(ModBase.GetTimeTick() - ModLaunch.m_ParameterTests) < 600000L)
			{
				Data.Output = new ModLaunch.McLoginResult
				{
					_StateMap = input.paramMap,
					Name = input.observerMap,
					m_InstanceMap = input._TagMap,
					Type = "Microsoft",
					_CallbackMap = input._TagMap,
					_TemplateMap = input.stubMap
				};
			}
			else
			{
				string[] array;
				if (Operators.CompareString(input.m_SystemMap, "", false) != 0)
				{
					array = ModLaunch.smethod_0(input.m_SystemMap);
					if (Operators.CompareString(array[0], "Relogin", false) != 0)
					{
						goto IL_126;
					}
				}
				array = ModLaunch.MsLoginStep1New(Data);
				IL_126:
				if (Data.IsAborted)
				{
					throw new ThreadInterruptedException();
				}
				Data.Progress = 0.25;
				if (Data.IsAborted)
				{
					throw new ThreadInterruptedException();
				}
				string accessToken = array[0];
				string text = array[1];
				string xbltoken = ModLaunch.MsLoginStep2(accessToken);
				Data.Progress = 0.4;
				if (Data.IsAborted)
				{
					throw new ThreadInterruptedException();
				}
				string[] tokens = ModLaunch.MsLoginStep3(xbltoken);
				Data.Progress = 0.55;
				if (Data.IsAborted)
				{
					throw new ThreadInterruptedException();
				}
				string text2 = ModLaunch.MsLoginStep4(tokens);
				Data.Progress = 0.7;
				if (Data.IsAborted)
				{
					throw new ThreadInterruptedException();
				}
				ModLaunch.MsLoginStep5(text2);
				Data.Progress = 0.85;
				if (Data.IsAborted)
				{
					throw new ThreadInterruptedException();
				}
				string[] array2 = ModLaunch.MsLoginStep6(text2);
				Data.Progress = 0.98;
				ModBase.m_IdentifierRepository.Set("CacheMsV2OAuthRefresh", text, false, null);
				ModBase.m_IdentifierRepository.Set("CacheMsV2Access", text2, false, null);
				ModBase.m_IdentifierRepository.Set("CacheMsV2Uuid", array2[0], false, null);
				ModBase.m_IdentifierRepository.Set("CacheMsV2Name", array2[1], false, null);
				ModBase.m_IdentifierRepository.Set("CacheMsV2ProfileJson", array2[2], false, null);
				JObject jobject = (JObject)ModBase.GetJson(Conversions.ToString(ModBase.m_IdentifierRepository.Get("LoginMsJson", null)));
				jobject.Remove(input.observerMap);
				jobject[array2[1]] = text;
				ModBase.m_IdentifierRepository.Set("LoginMsJson", jobject.ToString(0, new JsonConverter[0]), false, null);
				Data.Output = new ModLaunch.McLoginResult
				{
					_StateMap = text2,
					Name = array2[1],
					m_InstanceMap = array2[0],
					Type = "Microsoft",
					_CallbackMap = array2[0],
					_TemplateMap = array2[2]
				};
				ModLaunch.m_ParameterTests = ModBase.GetTimeTick();
				ModLaunch.McLaunchLog("微软登录完成");
			}
			ModBase.m_IdentifierRepository.Set("HintBuy", true, false, null);
			if (ModSecret.ThemeUnlock(10, false, null))
			{
				ModMain.MyMsgBox("感谢你对正版游戏的支持！\r\n隐藏主题 跳票红 已解锁！", "提示", "确定", "", "", false, true, false, null, null, null);
			}
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x000615D4 File Offset: 0x0005F7D4
		private static void McLoginServerStart(ModLoader.LoaderTask<ModLaunch.McLoginServer, ModLaunch.McLoginResult> Data)
		{
			ModLaunch.McLoginServer input = Data.Input;
			bool flag = false;
			string text = input.m_IssuerMap;
			if (Conversions.ToBoolean(text.Contains("@") && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiLauncherEmail", null))))
			{
				text = ModMinecraft.AccountFilter(text);
			}
			ModLaunch.McLaunchLog(string.Concat(new string[]
			{
				"登录方式：",
				input.m_WatcherMap,
				"（",
				text,
				"）"
			}));
			Data.Progress = 0.05;
			if (Data.Input._IdentifierMap || !Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("Cache" + input.m_SerializerMap + "Username", null), Data.Input.m_IssuerMap, false) || !Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("Cache" + input.m_SerializerMap + "Pass", null), Data.Input._IndexerMap, false) || !Operators.ConditionalCompareObjectNotEqual(ModBase.m_IdentifierRepository.Get("Cache" + input.m_SerializerMap + "Access", null), "", false) || !Operators.ConditionalCompareObjectNotEqual(ModBase.m_IdentifierRepository.Get("Cache" + input.m_SerializerMap + "Client", null), "", false) || !Operators.ConditionalCompareObjectNotEqual(ModBase.m_IdentifierRepository.Get("Cache" + input.m_SerializerMap + "Uuid", null), "", false) || !Operators.ConditionalCompareObjectNotEqual(ModBase.m_IdentifierRepository.Get("Cache" + input.m_SerializerMap + "Name", null), "", false))
			{
				goto IL_300;
			}
			try
			{
				if (Data.IsAborted)
				{
					throw new ThreadInterruptedException();
				}
				ModLaunch.McLoginRequestValidate(ref Data);
				goto IL_306;
			}
			catch (Exception ex)
			{
				string exceptionSummary = ModBase.GetExceptionSummary(ex);
				ModLaunch.McLaunchLog("验证登录失败：" + exceptionSummary);
				if ((exceptionSummary.Contains("超时") || exceptionSummary.Contains("imeout")) && !exceptionSummary.Contains("403"))
				{
					ModLaunch.McLaunchLog("已触发超时登录失败");
					throw new Exception("$登录失败：连接登录服务器超时。\r\n请检查你的网络状况是否良好，或尝试使用 VPN！");
				}
			}
			Data.Progress = 0.25;
			IL_2A5:
			try
			{
				if (Data.IsAborted)
				{
					throw new ThreadInterruptedException();
				}
				ModLaunch.McLoginRequestRefresh(ref Data, flag);
				goto IL_306;
			}
			catch (Exception ex2)
			{
				ModLaunch.McLaunchLog("刷新登录失败：" + ModBase.GetExceptionSummary(ex2));
			}
			Data.Progress = (flag ? 0.85 : 0.45);
			IL_300:
			try
			{
				if (Data.IsAborted)
				{
					throw new ThreadInterruptedException();
				}
				flag = ModLaunch.McLoginRequestLogin(ref Data);
			}
			catch (Exception ex3)
			{
				ModLaunch.McLaunchLog("登录失败：" + ModBase.GetExceptionSummary(ex3));
				throw;
			}
			if (flag)
			{
				Data.Progress = 0.65;
				goto IL_2A5;
			}
			IL_306:
			Data.Progress = 0.95;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			checked
			{
				try
				{
					if (Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(ModBase.m_IdentifierRepository.Get("Login" + input.m_SerializerMap + "Email", null), "", false))))
					{
						list.AddRange(ModBase.m_IdentifierRepository.Get("Login" + input.m_SerializerMap + "Email", null).ToString().Split("¨"));
					}
					if (Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(ModBase.m_IdentifierRepository.Get("Login" + input.m_SerializerMap + "Pass", null), "", false))))
					{
						list2.AddRange(ModBase.m_IdentifierRepository.Get("Login" + input.m_SerializerMap + "Pass", null).ToString().Split("¨"));
					}
					int num = list.Count - 1;
					for (int i = 0; i <= num; i++)
					{
						dictionary.Add(list[i], list2[i]);
					}
					dictionary.Remove(input.m_IssuerMap);
					list = new List<string>(dictionary.Keys);
					list.Insert(0, input.m_IssuerMap);
					list2 = new List<string>(dictionary.Values);
					list2.Insert(0, input._IndexerMap);
					ModBase.m_IdentifierRepository.Set("Login" + input.m_SerializerMap + "Email", list.Join("¨"), false, null);
					ModBase.m_IdentifierRepository.Set("Login" + input.m_SerializerMap + "Pass", list2.Join("¨"), false, null);
				}
				catch (Exception ex4)
				{
					ModBase.Log(ex4, "保存启动记录失败", ModBase.LogLevel.Hint, "出现错误");
					ModBase.m_IdentifierRepository.Set("Login" + input.m_SerializerMap + "Email", "", false, null);
					ModBase.m_IdentifierRepository.Set("Login" + input.m_SerializerMap + "Pass", "", false, null);
				}
			}
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00061B88 File Offset: 0x0005FD88
		private static void McLoginLegacyStart(ModLoader.LoaderTask<ModLaunch.McLoginLegacy, ModLaunch.McLoginResult> Data)
		{
			ModLaunch.McLoginLegacy input = Data.Input;
			ModLaunch.McLaunchLog("登录方式：离线（" + input.m_RulesMap + "）");
			Data.Progress = 0.1;
			Data.Output.Name = input.m_RulesMap;
			Data.Output.m_InstanceMap = ModLaunch.McLoginLegacyUuidWithCustomSkin(input.m_RulesMap, input.m_RefMap, input._DecoratorMap);
			Data.Output.Type = "Legacy";
			Data.Output._StateMap = Data.Output.m_InstanceMap;
			Data.Output._CallbackMap = Data.Output.m_InstanceMap;
			List<string> list = new List<string>();
			if (Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(ModBase.m_IdentifierRepository.Get("LoginLegacyName", null), "", false))))
			{
				list.AddRange(ModBase.m_IdentifierRepository.Get("LoginLegacyName", null).ToString().Split("¨"));
			}
			list.Remove(input.m_RulesMap);
			list.Insert(0, input.m_RulesMap);
			ModBase.m_IdentifierRepository.Set("LoginLegacyName", list.ToArray().Join("¨"), false, null);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00061CB8 File Offset: 0x0005FEB8
		private static void McLoginRequestValidate(ref ModLoader.LoaderTask<ModLaunch.McLoginServer, ModLaunch.McLoginResult> Data)
		{
			ModLaunch.McLaunchLog("验证登录开始（Validate, " + Data.Input.m_SerializerMap + "）");
			string text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("Cache" + Data.Input.m_SerializerMap + "Access", null));
			string text2 = Conversions.ToString(ModBase.m_IdentifierRepository.Get("Cache" + Data.Input.m_SerializerMap + "Client", null));
			string instanceMap = Conversions.ToString(ModBase.m_IdentifierRepository.Get("Cache" + Data.Input.m_SerializerMap + "Uuid", null));
			string name = Conversions.ToString(ModBase.m_IdentifierRepository.Get("Cache" + Data.Input.m_SerializerMap + "Name", null));
			JObject jobject = new JObject(new object[]
			{
				new JProperty("accessToken", text),
				new JProperty("clientToken", text2),
				new JProperty("requestUser", true)
			});
			ModNet.NetRequestRetry(Data.Input.interpreterMap + "/validate", "POST", jobject.ToString(0, new JsonConverter[0]), "application/json; charset=utf-8", true, new Dictionary<string, string>
			{
				{
					"Accept-Language",
					"zh_CN"
				}
			});
			Data.Output._StateMap = text;
			Data.Output._CallbackMap = text2;
			Data.Output.m_InstanceMap = instanceMap;
			Data.Output.Name = name;
			Data.Output.Type = Data.Input.m_SerializerMap;
			ModLaunch.McLaunchLog("验证登录成功（Validate, " + Data.Input.m_SerializerMap + "）");
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00061E88 File Offset: 0x00060088
		private static void McLoginRequestRefresh(ref ModLoader.LoaderTask<ModLaunch.McLoginServer, ModLaunch.McLoginResult> Data, bool RequestUser)
		{
			ModLaunch.McLaunchLog("刷新登录开始（Refresh, " + Data.Input.m_SerializerMap + "）");
			JObject jobject = (JObject)ModBase.GetJson(ModNet.NetRequestRetry(Data.Input.interpreterMap + "/refresh", "POST", Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("{", RequestUser ? Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("\r\n               \"requestUser\": true,\r\n               \"selectedProfile\": {\r\n                   \"id\":\"", ModBase.m_IdentifierRepository.Get("Cache" + Data.Input.m_SerializerMap + "Uuid", null)), "\",\r\n                   \"name\":\""), ModBase.m_IdentifierRepository.Get("Cache" + Data.Input.m_SerializerMap + "Name", null)), "\"},") : ""), "\r\n               \"accessToken\":\""), ModBase.m_IdentifierRepository.Get("Cache" + Data.Input.m_SerializerMap + "Access", null)), "\",\r\n               \"clientToken\":\""), ModBase.m_IdentifierRepository.Get("Cache" + Data.Input.m_SerializerMap + "Client", null)), "\"}"), "application/json; charset=utf-8", true, new Dictionary<string, string>
			{
				{
					"Accept-Language",
					"zh_CN"
				}
			}));
			if (jobject["selectedProfile"] == null)
			{
				throw new Exception(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("选择的角色 ", ModBase.m_IdentifierRepository.Get("Cache" + Data.Input.m_SerializerMap + "Name", null)), " 无效！")));
			}
			Data.Output._StateMap = jobject["accessToken"].ToString();
			Data.Output._CallbackMap = jobject["clientToken"].ToString();
			Data.Output.m_InstanceMap = jobject["selectedProfile"]["id"].ToString();
			Data.Output.Name = jobject["selectedProfile"]["name"].ToString();
			Data.Output.Type = Data.Input.m_SerializerMap;
			ModBase.m_IdentifierRepository.Set("Cache" + Data.Input.m_SerializerMap + "Access", Data.Output._StateMap, false, null);
			ModBase.m_IdentifierRepository.Set("Cache" + Data.Input.m_SerializerMap + "Client", Data.Output._CallbackMap, false, null);
			ModBase.m_IdentifierRepository.Set("Cache" + Data.Input.m_SerializerMap + "Uuid", Data.Output.m_InstanceMap, false, null);
			ModBase.m_IdentifierRepository.Set("Cache" + Data.Input.m_SerializerMap + "Name", Data.Output.Name, false, null);
			ModBase.m_IdentifierRepository.Set("Cache" + Data.Input.m_SerializerMap + "Username", Data.Input.m_IssuerMap, false, null);
			ModBase.m_IdentifierRepository.Set("Cache" + Data.Input.m_SerializerMap + "Pass", Data.Input._IndexerMap, false, null);
			ModLaunch.McLaunchLog("刷新登录成功（Refresh, " + Data.Input.m_SerializerMap + "）");
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00062234 File Offset: 0x00060434
		private static bool McLoginRequestLogin(ref ModLoader.LoaderTask<ModLaunch.McLoginServer, ModLaunch.McLoginResult> Data)
		{
			bool result;
			try
			{
				ModLaunch._Closure$__36-0 CS$<>8__locals1 = new ModLaunch._Closure$__36-0(CS$<>8__locals1);
				bool flag = false;
				ModLaunch.McLaunchLog("登录开始（Login, " + Data.Input.m_SerializerMap + "）");
				JObject jobject = new JObject(new object[]
				{
					new JProperty("agent", new JObject(new object[]
					{
						new JProperty("name", "Minecraft"),
						new JProperty("version", 1)
					})),
					new JProperty("username", Data.Input.m_IssuerMap),
					new JProperty("password", Data.Input._IndexerMap),
					new JProperty("requestUser", true)
				});
				CS$<>8__locals1.$VB$Local_LoginJson = (JObject)ModBase.GetJson(ModNet.NetRequestRetry(Data.Input.interpreterMap + "/authenticate", "POST", jobject.ToString(0, new JsonConverter[0]), "application/json; charset=utf-8", true, new Dictionary<string, string>
				{
					{
						"Accept-Language",
						"zh_CN"
					}
				}));
				if (Enumerable.Count<JToken>(CS$<>8__locals1.$VB$Local_LoginJson["availableProfiles"]) == 0)
				{
					if (Data.Input._IdentifierMap)
					{
						ModMain.Hint("你还没有创建角色，无法更换！", ModMain.HintType.Critical, true);
					}
					throw new Exception("$你还没有创建角色，请在创建角色后再试！");
				}
				if (Data.Input._IdentifierMap && Enumerable.Count<JToken>(CS$<>8__locals1.$VB$Local_LoginJson["availableProfiles"]) == 1)
				{
					ModMain.Hint("你的账户中只有一个角色，无法更换！", ModMain.HintType.Critical, true);
				}
				CS$<>8__locals1.$VB$Local_SelectedName = null;
				CS$<>8__locals1.$VB$Local_SelectedId = null;
				if ((CS$<>8__locals1.$VB$Local_LoginJson["selectedProfile"] == null || Data.Input._IdentifierMap) && Enumerable.Count<JToken>(CS$<>8__locals1.$VB$Local_LoginJson["availableProfiles"]) > 1)
				{
					flag = true;
					string right = Conversions.ToString(ModBase.m_IdentifierRepository.Get("Cache" + Data.Input.m_SerializerMap + "Uuid", null));
					try
					{
						foreach (JToken jtoken in CS$<>8__locals1.$VB$Local_LoginJson["availableProfiles"])
						{
							if (Operators.CompareString(jtoken["id"].ToString(), right, false) == 0)
							{
								CS$<>8__locals1.$VB$Local_SelectedName = jtoken["name"].ToString();
								CS$<>8__locals1.$VB$Local_SelectedId = jtoken["id"].ToString();
								ModLaunch.McLaunchLog("根据缓存选择的角色：" + CS$<>8__locals1.$VB$Local_SelectedName);
							}
						}
					}
					finally
					{
						IEnumerator<JToken> enumerator;
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
					if (CS$<>8__locals1.$VB$Local_SelectedName == null)
					{
						ModLaunch.McLaunchLog("要求玩家选择角色");
						ModBase.RunInUiWait(delegate()
						{
							List<IMyRadio> list = new List<IMyRadio>();
							List<JToken> list2 = new List<JToken>();
							try
							{
								foreach (JToken jtoken2 in CS$<>8__locals1.$VB$Local_LoginJson["availableProfiles"])
								{
									list.Add(new MyRadioBox
									{
										Text = jtoken2["name"].ToString()
									});
									list2.Add(jtoken2);
								}
							}
							finally
							{
								IEnumerator<JToken> enumerator2;
								if (enumerator2 != null)
								{
									enumerator2.Dispose();
								}
							}
							int value = ModMain.MyMsgBoxSelect(list, "选择使用的角色", "确定", "", false).Value;
							CS$<>8__locals1.$VB$Local_SelectedName = list2[value]["name"].ToString();
							CS$<>8__locals1.$VB$Local_SelectedId = list2[value]["id"].ToString();
						});
						ModLaunch.McLaunchLog("玩家选择的角色：" + CS$<>8__locals1.$VB$Local_SelectedName);
					}
				}
				else
				{
					CS$<>8__locals1.$VB$Local_SelectedName = CS$<>8__locals1.$VB$Local_LoginJson["selectedProfile"]["name"].ToString();
					CS$<>8__locals1.$VB$Local_SelectedId = CS$<>8__locals1.$VB$Local_LoginJson["selectedProfile"]["id"].ToString();
				}
				Data.Output._StateMap = CS$<>8__locals1.$VB$Local_LoginJson["accessToken"].ToString();
				Data.Output._CallbackMap = CS$<>8__locals1.$VB$Local_LoginJson["clientToken"].ToString();
				Data.Output.Name = CS$<>8__locals1.$VB$Local_SelectedName;
				Data.Output.m_InstanceMap = CS$<>8__locals1.$VB$Local_SelectedId;
				Data.Output.Type = Data.Input.m_SerializerMap;
				ModBase.m_IdentifierRepository.Set("Cache" + Data.Input.m_SerializerMap + "Access", Data.Output._StateMap, false, null);
				ModBase.m_IdentifierRepository.Set("Cache" + Data.Input.m_SerializerMap + "Client", Data.Output._CallbackMap, false, null);
				ModBase.m_IdentifierRepository.Set("Cache" + Data.Input.m_SerializerMap + "Uuid", Data.Output.m_InstanceMap, false, null);
				ModBase.m_IdentifierRepository.Set("Cache" + Data.Input.m_SerializerMap + "Name", Data.Output.Name, false, null);
				ModBase.m_IdentifierRepository.Set("Cache" + Data.Input.m_SerializerMap + "Username", Data.Input.m_IssuerMap, false, null);
				ModBase.m_IdentifierRepository.Set("Cache" + Data.Input.m_SerializerMap + "Pass", Data.Input._IndexerMap, false, null);
				ModLaunch.McLaunchLog("登录成功（Login, " + Data.Input.m_SerializerMap + "）");
				result = flag;
			}
			catch (Exception ex)
			{
				string exceptionSummary = ModBase.GetExceptionSummary(ex);
				ModBase.Log(ex, "登录失败原始错误信息", ModBase.LogLevel.Normal, "出现错误");
				if (ex is ModNet.ResponsedWebException)
				{
					string text = null;
					try
					{
						text = Conversions.ToString(NewLateBinding.LateIndexGet(ModBase.GetJson(((ModNet.ResponsedWebException)ex).GetMapper()), new object[]
						{
							"errorMessage"
						}, null));
					}
					catch (Exception ex2)
					{
					}
					if (!string.IsNullOrWhiteSpace(text))
					{
						if (text.Contains("密码错误") || text.ContainsF("Incorrect username or password", true))
						{
							ModLaunch.McLaunchLog("密码错误，退出登录");
							ModLaunch.McLoginType type = Data.Input.Type;
							if (type != ModLaunch.McLoginType.Nide)
							{
								if (type == ModLaunch.McLoginType.Auth)
								{
									ModBase.RunInUi(new Action(PageLoginAuthSkin.ExitLogin), false);
								}
							}
							else
							{
								ModBase.RunInUi(new Action(PageLoginNideSkin.ExitLogin), false);
							}
						}
						throw new Exception("$登录失败：" + text);
					}
				}
				if (exceptionSummary.Contains("403"))
				{
					ModLaunch.McLoginType type2 = Data.Input.Type;
					if (type2 == ModLaunch.McLoginType.Nide)
					{
						throw new Exception("$登录失败，以下为可能的原因：\r\n - 输入的账号或密码错误。\r\n - 密码错误次数过多，导致被暂时屏蔽。请不要操作，等待 10 分钟后再试。\r\n" + (Data.Input.m_IssuerMap.Contains("@") ? "" : " - 登录账号应为邮箱或统一通行证账号，而非游戏角色 ID。\r\n") + " - 只注册了账号，但没有加入对应服务器。");
					}
					if (type2 == ModLaunch.McLoginType.Auth)
					{
						throw new Exception("$登录失败，以下为可能的原因：\r\n - 输入的账号或密码错误。\r\n - 登录尝试过于频繁，导致被暂时屏蔽。请不要操作，等待 10 分钟后再试。\r\n - 只注册了账号，但没有在皮肤站新建角色。");
					}
					result = false;
				}
				else
				{
					if (exceptionSummary.Contains("超时") || exceptionSummary.Contains("imeout") || exceptionSummary.Contains("网络请求失败"))
					{
						throw new Exception("$登录失败：连接登录服务器超时。\r\n请检查你的网络状况是否良好，或尝试使用 VPN！");
					}
					if (ex.Message.StartsWithF("$", false))
					{
						throw;
					}
					throw new Exception("登录失败：" + ex.Message, ex);
				}
			}
			return result;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00062940 File Offset: 0x00060B40
		private static string[] MsLoginStep1New(ModLoader.LoaderTask<ModLaunch.McLoginMs, ModLaunch.McLoginResult> Data)
		{
			ModMain.MyMsgBoxConverter myMsgBoxConverter;
			do
			{
				ModLaunch.McLaunchLog("开始微软登录步骤 1/6（原始登录）");
				JObject jobject = (JObject)ModBase.GetJson(ModNet.NetRequestRetry("https://login.microsoftonline.com/consumers/oauth2/v2.0/devicecode", "POST", string.Format("client_id={0}&tenant=/consumers&scope=XboxLive.signin%20offline_access", "fe72edc2-3a6f-4280-90e8-e2beb64ce7e1"), "application/x-www-form-urlencoded", true, null));
				ModLaunch.McLaunchLog("网页登录地址：" + jobject["verification_uri"].ToString());
				myMsgBoxConverter = new ModMain.MyMsgBoxConverter
				{
					m_CollectionMap = jobject,
					m_PolicyMap = true,
					Type = ModMain.MyMsgBoxType.Login
				};
				ModMain.m_DispatcherIterator.Add(myMsgBoxConverter);
				while (myMsgBoxConverter.schemaMap == null)
				{
					Thread.Sleep(100);
				}
				if (!(myMsgBoxConverter.schemaMap is ModBase.RestartException))
				{
					goto IL_159;
				}
			}
			while (ModMain.MyMsgBox(string.Format("请在登录时选择 {0}其他登录方法{1}，然后选择 {2}使用我的密码{3}。{4}如果没有该选项，请选择 {5}设置密码{6}，设置完毕后再登录。", new object[]
			{
				ModBase.callbackRepository,
				ModBase.m_TemplateRepository,
				ModBase.callbackRepository,
				ModBase.m_TemplateRepository,
				"\r\n",
				ModBase.callbackRepository,
				ModBase.m_TemplateRepository
			}), "需要使用密码登录", "重新登录", "设置密码", "取消", false, true, false, null, (ModLaunch._Closure$__.$I37-0 == null) ? (ModLaunch._Closure$__.$I37-0 = delegate()
			{
				ModBase.OpenWebsite("https://account.live.com/password/Change");
			}) : ModLaunch._Closure$__.$I37-0, null) == 1);
			throw new Exception("$$");
			IL_159:
			if (myMsgBoxConverter.schemaMap is Exception)
			{
				throw (Exception)myMsgBoxConverter.schemaMap;
			}
			return (string[])myMsgBoxConverter.schemaMap;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00062ACC File Offset: 0x00060CCC
		private static string[] smethod_0(string Code)
		{
			ModLaunch.McLaunchLog("开始微软登录步骤 1/6（刷新登录）");
			string data;
			try
			{
				data = Conversions.ToString(ModNet.NetRequestMultiple("https://login.live.com/oauth20_token.srf", "POST", string.Format("client_id={0}&refresh_token={1}&grant_type=refresh_token&scope=XboxLive.signin%20offline_access", "fe72edc2-3a6f-4280-90e8-e2beb64ce7e1", Uri.EscapeDataString(Code)), "application/x-www-form-urlencoded", 2, null, true));
			}
			catch (Exception ex)
			{
				if (!ex.Message.Contains("must sign in again") && (!ex.Message.Contains("refresh_token") || !ex.Message.Contains("is not valid")))
				{
					throw;
				}
				return new string[]
				{
					"Relogin",
					""
				};
			}
			JObject jobject = (JObject)ModBase.GetJson(data);
			string text = jobject["access_token"].ToString();
			string text2 = jobject["refresh_token"].ToString();
			return new string[]
			{
				text,
				text2
			};
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00062BC4 File Offset: 0x00060DC4
		private static string MsLoginStep2(string AccessToken)
		{
			ModLaunch.McLaunchLog("开始微软登录步骤 2/6");
			string data = "{\r\n           \"Properties\": {\r\n               \"AuthMethod\": \"RPS\",\r\n               \"SiteName\": \"user.auth.xboxlive.com\",\r\n               \"RpsTicket\": \"" + (AccessToken.StartsWithF("d=", false) ? "" : "d=") + AccessToken + "\"\r\n           },\r\n           \"RelyingParty\": \"http://auth.xboxlive.com\",\r\n           \"TokenType\": \"JWT\"\r\n        }";
			return ((JObject)ModBase.GetJson(Conversions.ToString(ModNet.NetRequestMultiple("https://user.auth.xboxlive.com/user/authenticate", "POST", data, "application/json", 3, null, true))))["Token"].ToString();
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00062C3C File Offset: 0x00060E3C
		private static string[] MsLoginStep3(string XBLToken)
		{
			ModLaunch.McLaunchLog("开始微软登录步骤 3/6");
			string data = "{\r\n                                    \"Properties\": {\r\n                                        \"SandboxId\": \"RETAIL\",\r\n                                        \"UserTokens\": [\r\n                                            \"" + XBLToken + "\"\r\n                                        ]\r\n                                    },\r\n                                    \"RelyingParty\": \"rp://api.minecraftservices.com/\",\r\n                                    \"TokenType\": \"JWT\"\r\n                                 }";
			string data2;
			try
			{
				data2 = Conversions.ToString(ModNet.NetRequestMultiple("https://xsts.auth.xboxlive.com/xsts/authorize", "POST", data, "application/json", 3, null, true));
			}
			catch (WebException ex)
			{
				if (ex.Message.Contains("2148916227"))
				{
					ModMain.MyMsgBox("该账号似乎已被微软封禁，无法登录。", "登录失败", "我知道了", "", "", true, true, false, null, null, null);
					throw new Exception("$$");
				}
				if (ex.Message.Contains("2148916233"))
				{
					if (ModMain.MyMsgBox("你尚未注册 Xbox 账户，请在注册后再登录。", "登录提示", "注册", "取消", "", false, true, false, null, null, null) == 1)
					{
						ModBase.OpenWebsite("https://signup.live.com/signup");
					}
					throw new Exception("$$");
				}
				if (ex.Message.Contains("2148916235"))
				{
					ModMain.MyMsgBox(string.Format("你的网络所在的国家或地区无法登录微软账号。{0}请尝试使用加速器或 VPN。", "\r\n"), "登录失败", "我知道了", "", "", false, true, false, null, null, null);
					throw new Exception("$$");
				}
				if (ex.Message.Contains("2148916238"))
				{
					if (ModMain.MyMsgBox("该账号年龄不足，你需要先修改出生日期，然后才能登录。\r\n该账号目前填写的年龄是否在 13 岁以上？", "登录提示", "13 岁以上", "12 岁以下", "我不知道", false, true, false, null, null, null) == 1)
					{
						ModBase.OpenWebsite("https://account.live.com/editprof.aspx");
						ModMain.MyMsgBox("请在打开的网页中修改账号的出生日期（至少改为 18 岁以上）。\r\n在修改成功后等待一分钟，然后再回到 PCL，就可以正常登录了！", "登录提示", "确定", "", "", false, true, false, null, null, null);
					}
					else
					{
						ModBase.OpenWebsite("https://support.microsoft.com/zh-cn/account-billing/如何更改-microsoft-帐户上的出生日期-837badbc-999e-54d2-2617-d19206b9540a");
						ModMain.MyMsgBox("请根据打开的网页的说明，修改账号的出生日期（至少改为 18 岁以上）。\r\n在修改成功后等待一分钟，然后再回到 PCL，就可以正常登录了！", "登录提示", "确定", "", "", false, true, false, null, null, null);
					}
					throw new Exception("$$");
				}
				throw;
			}
			JObject jobject = (JObject)ModBase.GetJson(data2);
			string text = jobject["Token"].ToString();
			string text2 = jobject["DisplayClaims"]["xui"][0]["uhs"].ToString();
			return new string[]
			{
				text,
				text2
			};
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00062E90 File Offset: 0x00061090
		private static string MsLoginStep4(string[] Tokens)
		{
			ModLaunch.McLaunchLog("开始微软登录步骤 4/6");
			string data = new JObject(new JProperty("identityToken", string.Format("XBL3.0 x={0};{1}", Tokens[1], Tokens[0]))).ToString(0, new JsonConverter[0]);
			string data2;
			try
			{
				data2 = ModNet.NetRequestRetry("https://api.minecraftservices.com/authentication/login_with_xbox", "POST", data, "application/json", true, null);
			}
			catch (WebException ex)
			{
				string exceptionSummary = ModBase.GetExceptionSummary(ex);
				if (exceptionSummary.Contains("(429)"))
				{
					ModBase.Log(ex, "微软登录第 5 步汇报 429", ModBase.LogLevel.Debug, "出现错误");
					throw new Exception("$登录尝试太过频繁，请等待几分钟后再试！");
				}
				if (exceptionSummary.Contains("(403)"))
				{
					ModBase.Log(ex, "微软登录第 5 步汇报 403", ModBase.LogLevel.Debug, "出现错误");
					throw new Exception("$当前 IP 的登录尝试异常。\r\n如果你使用了 VPN 或加速器，请把它们关掉或更换节点后再试！");
				}
				throw;
			}
			return ((JObject)ModBase.GetJson(data2))["access_token"].ToString();
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00062F7C File Offset: 0x0006117C
		private static void MsLoginStep5(string AccessToken)
		{
			ModLaunch.McLaunchLog("开始微软登录步骤 5/6");
			string text = Conversions.ToString(ModNet.NetRequestMultiple("https://api.minecraftservices.com/entitlements/mcstore", "GET", "", "application/json", 2, new Dictionary<string, string>
			{
				{
					"Authorization",
					"Bearer " + AccessToken
				}
			}, true));
			try
			{
				JObject jobject = (JObject)ModBase.GetJson(text);
				if (!jobject.ContainsKey("items") || !Enumerable.Any<JToken>(jobject["items"]))
				{
					int num = ModMain.MyMsgBox("你尚未购买正版 Minecraft，或者 Xbox Game Pass 已到期。", "登录失败", "购买 Minecraft", "取消", "", false, true, false, null, null, null);
					if (num == 1)
					{
						ModBase.OpenWebsite("https://www.xbox.com/zh-cn/games/store/minecraft-java-bedrock-edition-for-pc/9nxp44l49shj");
					}
					throw new Exception("$$");
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "微软登录第 6 步异常：" + text, ModBase.LogLevel.Debug, "出现错误");
				throw;
			}
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0006306C File Offset: 0x0006126C
		private static string[] MsLoginStep6(string AccessToken)
		{
			ModLaunch.McLaunchLog("开始微软登录步骤 6/6");
			string text;
			try
			{
				text = Conversions.ToString(ModNet.NetRequestMultiple("https://api.minecraftservices.com/minecraft/profile", "GET", "", "application/json", 2, new Dictionary<string, string>
				{
					{
						"Authorization",
						"Bearer " + AccessToken
					}
				}, true));
			}
			catch (WebException ex)
			{
				string exceptionSummary = ModBase.GetExceptionSummary(ex);
				if (exceptionSummary.Contains("(429)"))
				{
					ModBase.Log(ex, "微软登录第 7 步汇报 429", ModBase.LogLevel.Debug, "出现错误");
					throw new Exception("$登录尝试太过频繁，请等待几分钟后再试！");
				}
				if (exceptionSummary.Contains("(404)"))
				{
					ModBase.Log(ex, "微软登录第 7 步汇报 404", ModBase.LogLevel.Debug, "出现错误");
					ModBase.RunInNewThread((ModLaunch._Closure$__.$I43-0 == null) ? (ModLaunch._Closure$__.$I43-0 = delegate()
					{
						int num = ModMain.MyMsgBox("请先创建 Minecraft 玩家档案，然后再重新登录。", "登录失败", "创建档案", "取消", "", false, true, false, null, null, null);
						if (num == 1)
						{
							ModBase.OpenWebsite("https://www.minecraft.net/zh-hans/msaprofile/mygames/editprofile");
						}
					}) : ModLaunch._Closure$__.$I43-0, "Login Failed: Create Profile", ThreadPriority.Normal);
					throw new Exception("$$");
				}
				throw;
			}
			JObject jobject = (JObject)ModBase.GetJson(text);
			string text2 = jobject["id"].ToString();
			string text3 = jobject["name"].ToString();
			return new string[]
			{
				text2,
				text3,
				text
			};
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x000631A8 File Offset: 0x000613A8
		private static string McLoginLegacyUuidWithCustomSkin(string UserName, int SkinType, string SkinName)
		{
			string text = Conversions.ToString(ModLaunch.McLoginLegacyUuid(UserName));
			checked
			{
				switch (SkinType)
				{
				case 0:
					return text;
				case 1:
					while (Operators.CompareString(ModMinecraft.McSkinSex(text), "Steve", false) != 0)
					{
						if (text.EndsWithF("FFFFF", false))
						{
							text = text.Substring(0, 27) + "00000";
						}
						text = text.Substring(0, 27) + (long.Parse(text.Substring(27), NumberStyles.AllowHexSpecifier) + 1L).ToString("X").PadLeft(5, '0');
					}
					return text;
				case 2:
					while (Operators.CompareString(ModMinecraft.McSkinSex(text), "Alex", false) != 0)
					{
						if (text.EndsWithF("FFFFF", false))
						{
							text = text.Substring(0, 27) + "00000";
						}
						text = text.Substring(0, 27) + (long.Parse(text.Substring(27), NumberStyles.AllowHexSpecifier) + 1L).ToString("X").PadLeft(5, '0');
					}
					return text;
				case 3:
					try
					{
						if (Operators.CompareString(SkinName, "", false) != 0 && ModMinecraft.AddClient().Version._ServerMap < 20)
						{
							ModBase.Log("[Skin] 由于离线皮肤设置，使用正版 UUID：" + SkinName, ModBase.LogLevel.Normal, "出现错误");
							text = Conversions.ToString(ModLaunch.McLoginMojangUuid(SkinName, false));
						}
						return text;
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "离线启动时使用的正版皮肤获取失败", ModBase.LogLevel.Debug, "出现错误");
						ModMain.MyMsgBox("由于设置的离线启动时使用的正版皮肤获取失败，游戏将以无皮肤的方式启动。\r\n请检查你的网络是否通畅，或尝试使用 VPN！\r\n\r\n详细的错误信息：" + ex.Message, "皮肤获取失败", "确定", "", "", false, true, false, null, null, null);
						return text;
					}
					break;
				case 4:
					break;
				default:
					return text;
				}
				while (Operators.CompareString(ModMinecraft.McSkinSex(text), (!Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LaunchSkinSlim", null))) ? "Steve" : "Alex", false) != 0)
				{
					if (text.EndsWithF("FFFFF", false))
					{
						text = text.Substring(0, 27) + "00000";
					}
					text = text.Substring(0, 27) + (long.Parse(text.Substring(27), NumberStyles.AllowHexSpecifier) + 1L).ToString("X").PadLeft(5, '0');
				}
				return text;
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00063420 File Offset: 0x00061620
		public static object McLoginMojangUuid(string Name, bool ThrowOnNotFound)
		{
			object result;
			if (Name.Trim().Length == 0)
			{
				result = ModBase.StrFill("", "0", 32);
			}
			else
			{
				string text = ModBase.ReadIni(ModBase.m_DecoratorRepository + "Cache\\Uuid\\Mojang.ini", Name, "");
				if (Strings.Len(text) == 32)
				{
					result = text;
				}
				else
				{
					try
					{
						JObject jobject = (JObject)ModNet.NetGetCodeByRequestRetry("https://api.mojang.com/users/profiles/minecraft/" + Name, null, "", true, null, false);
						if (jobject == null)
						{
							throw new FileNotFoundException("正版玩家档案不存在（" + Name + "）");
						}
						text = (string)(jobject["id"] ?? "");
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "从官网获取正版 Uuid 失败（" + Name + "）", ModBase.LogLevel.Debug, "出现错误");
						if (ThrowOnNotFound || Operators.CompareString(ex.GetType().Name, "FileNotFoundException", false) != 0)
						{
							throw new Exception("从官网获取正版 Uuid 失败", ex);
						}
						text = Conversions.ToString(ModLaunch.McLoginLegacyUuid(Name));
					}
					if (Strings.Len(text) != 32)
					{
						throw new Exception("获取的正版 Uuid 长度不足（" + text + "）");
					}
					ModBase.WriteIni(ModBase.m_DecoratorRepository + "Cache\\Uuid\\Mojang.ini", Name, text);
					result = text;
				}
			}
			return result;
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0006357C File Offset: 0x0006177C
		public static object McLoginLegacyUuid(string Name)
		{
			string text = ModBase.StrFill(Name.Length.ToString("X"), "0", 16) + ModBase.StrFill(ModBase.GetHash(Name).ToString("X"), "0", 16);
			return string.Concat(new string[]
			{
				text.Substring(0, 12),
				"3",
				text.Substring(13, 3),
				"9",
				text.Substring(17, 15)
			});
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00063610 File Offset: 0x00061810
		private static void McLaunchJava(ModLoader.LoaderTask<int, int> Task)
		{
			Version version = new Version(0, 0, 0, 0);
			Version version2 = new Version(999, 999, 999, 999);
			if ((DateTime.Compare(ModMinecraft.AddClient()._RegistryMap, new DateTime(2024, 4, 2)) >= 0 && ModMinecraft.AddClient().Version._ServerMap == 99) || (ModMinecraft.AddClient().Version._ServerMap > 20 && ModMinecraft.AddClient().Version._ServerMap != 99) || (ModMinecraft.AddClient().Version._ServerMap == 20 && ModMinecraft.AddClient().Version.m_ResolverMap >= 5))
			{
				version = new Version(1, 21, 0, 0);
			}
			else if ((DateTime.Compare(ModMinecraft.AddClient()._RegistryMap, new DateTime(2021, 11, 16)) >= 0 && ModMinecraft.AddClient().Version._ServerMap == 99) || (ModMinecraft.AddClient().Version._ServerMap >= 18 && ModMinecraft.AddClient().Version._ServerMap != 99))
			{
				version = new Version(1, 17, 0, 0);
			}
			else if ((DateTime.Compare(ModMinecraft.AddClient()._RegistryMap, new DateTime(2021, 5, 11)) >= 0 && ModMinecraft.AddClient().Version._ServerMap == 99) || (ModMinecraft.AddClient().Version._ServerMap >= 17 && ModMinecraft.AddClient().Version._ServerMap != 99))
			{
				version = new Version(1, 16, 0, 0);
			}
			else if (ModMinecraft.AddClient()._RegistryMap.Year >= 2017)
			{
				version = new Version(1, 8, 0, 0);
			}
			else if (DateTime.Compare(ModMinecraft.AddClient()._RegistryMap, new DateTime(2013, 5, 1)) <= 0 && ModMinecraft.AddClient()._RegistryMap.Year >= 2001)
			{
				version2 = new Version(1, 12, 999, 999);
			}
			if (ModMinecraft.AddClient().Version._StatusMap)
			{
				if (ModMinecraft.AddClient().Version._ServerMap <= 7 && ModMinecraft.AddClient().Version._ServerMap > 0)
				{
					version2 = new Version(1, 8, 999, 999);
				}
				else if (ModMinecraft.AddClient().Version._ServerMap >= 8 && ModMinecraft.AddClient().Version._ServerMap <= 11)
				{
					version = new Version(1, 8, 0, 0);
					version2 = new Version(1, 8, 999, 999);
				}
				else if (ModMinecraft.AddClient().Version._ServerMap == 12)
				{
					version2 = new Version(1, 8, 999, 999);
				}
			}
			if (ModMinecraft.AddClient().Version.m_StructMap)
			{
				if (Operators.CompareString(ModMinecraft.AddClient().Version._ConnectionMap, "1.7.2", false) == 0)
				{
					version = ((new Version(1, 7, 0, 0) > version) ? new Version(1, 7, 0, 0) : version);
					version2 = ((new Version(1, 7, 999, 999) < version2) ? new Version(1, 7, 999, 999) : version2);
				}
				else if (ModMinecraft.AddClient().Version._ServerMap <= 12 && ModMinecraft.AddClient().Version._ServerMap > 0)
				{
					version2 = new Version(1, 8, 999, 999);
				}
				else if (ModMinecraft.AddClient().Version._ServerMap <= 14 && ModMinecraft.AddClient().Version._ServerMap >= 13)
				{
					version = ((new Version(1, 8, 0, 0) > version) ? new Version(1, 8, 0, 0) : version);
					version2 = ((new Version(1, 10, 999, 999) < version2) ? new Version(1, 10, 999, 999) : version2);
				}
				else if (ModMinecraft.AddClient().Version._ServerMap == 15)
				{
					version = ((new Version(1, 8, 0, 0) > version) ? new Version(1, 8, 0, 0) : version);
					version2 = ((new Version(1, 15, 999, 999) < version2) ? new Version(1, 15, 999, 999) : version2);
				}
				else if (ModMinecraft.VersionSortBoolean(ModMinecraft.AddClient().Version.printerMap, "34.0.0") && ModMinecraft.VersionSortBoolean("36.2.25", ModMinecraft.AddClient().Version.printerMap))
				{
					version2 = ((new Version(1, 8, 0, 320) < version2) ? new Version(1, 8, 0, 320) : version2);
				}
				else if (ModMinecraft.AddClient().Version._ServerMap >= 18 && ModMinecraft.AddClient().Version._ServerMap < 19 && ModMinecraft.AddClient().Version._StatusMap)
				{
					version2 = ((new Version(1, 18, 999, 999) < version2) ? new Version(1, 18, 999, 999) : version2);
				}
			}
			if (ModMinecraft.AddClient().Version._CandidateMap)
			{
				if (ModMinecraft.AddClient().Version._ServerMap >= 15 && ModMinecraft.AddClient().Version._ServerMap <= 16 && ModMinecraft.AddClient().Version._ServerMap != -1)
				{
					version = ((new Version(1, 8, 0, 0) > version) ? new Version(1, 8, 0, 0) : version);
				}
				else if (ModMinecraft.AddClient().Version._ServerMap >= 18 && ModMinecraft.AddClient().Version._ServerMap < 99)
				{
					version = ((new Version(1, 17, 0, 0) > version) ? new Version(1, 17, 0, 0) : version);
				}
			}
			if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LoginType", null), ModLaunch.McLoginType.Nide, false))
			{
				version = ((new Version(1, 8, 0, 141) > version) ? new Version(1, 8, 0, 141) : version);
			}
			object dispatcher = ModJava.m_Dispatcher;
			ObjectFlowControl.CheckForSyncLockOnValueType(dispatcher);
			lock (dispatcher)
			{
				ModLaunch.McLaunchLog("Java 版本需求：最低 " + version.ToString() + "，最高 " + version2.ToString());
				ModLaunch.m_RecordTests = ModJava.JavaSelect("$$", version, version2, ModMinecraft.AddClient());
				if (!Task.IsAborted)
				{
					if (ModLaunch.m_RecordTests != null)
					{
						ModLaunch.McLaunchLog("选择的 Java：" + ModLaunch.m_RecordTests.ToString());
					}
					else if (!Task.IsAborted)
					{
						ModLaunch.McLaunchLog("无合适的 Java，需要确认是否自动下载");
						string text;
						if (version >= new Version(1, 21, 0, 0))
						{
							text = Conversions.ToString(21);
							if (!ModJava.JavaDownloadConfirm("Java 21", false))
							{
								throw new Exception("$$");
							}
						}
						else if (version >= new Version(1, 9, 0, 0))
						{
							text = Conversions.ToString(17);
							if (!ModJava.JavaDownloadConfirm("Java 17", false))
							{
								throw new Exception("$$");
							}
						}
						else if (version2 < new Version(1, 8, 0, 0))
						{
							text = Conversions.ToString(7);
							if (!ModJava.JavaDownloadConfirm("Java 7", true))
							{
								throw new Exception("$$");
							}
						}
						else if (version > new Version(1, 8, 0, 140) && version2 < new Version(1, 8, 0, 321))
						{
							text = "8u141";
							if (!ModJava.JavaDownloadConfirm("Java 8.0.141 ~ 8.0.320", true))
							{
								throw new Exception("$$");
							}
						}
						else if (version > new Version(1, 8, 0, 140))
						{
							text = "8u141";
							if (!ModJava.JavaDownloadConfirm("Java 8.0.141 或更高版本的 Java 8", true))
							{
								throw new Exception("$$");
							}
						}
						else if (version2 < new Version(1, 8, 0, 321))
						{
							text = Conversions.ToString(8);
							if (!ModJava.JavaDownloadConfirm("Java 8.0.320 或更低版本的 Java 8", false))
							{
								throw new Exception("$$");
							}
						}
						else
						{
							text = Conversions.ToString(8);
							if (!ModJava.JavaDownloadConfirm("Java 8", false))
							{
								throw new Exception("$$");
							}
						}
						ModLoader.LoaderCombo<int> loaderCombo = ModJava.JavaFixLoaders(Conversions.ToInteger(text));
						try
						{
							loaderCombo.Start(text, true);
							while (loaderCombo.State == ModBase.LoadState.Loading && !Task.IsAborted)
							{
								Task.Progress = loaderCombo.Progress;
								Thread.Sleep(10);
							}
						}
						finally
						{
							loaderCombo.Abort();
						}
						if (ModJava._Process.State != ModBase.LoadState.Loading)
						{
							ModJava._Process.State = ModBase.LoadState.Waiting;
						}
						ModLaunch.m_RecordTests = ModJava.JavaSelect("$$", version, version2, ModMinecraft.AddClient());
						if (!Task.IsAborted)
						{
							if (ModLaunch.m_RecordTests == null)
							{
								ModMain.Hint("没有可用的 Java，已取消启动！", ModMain.HintType.Critical, true);
								throw new Exception("$$");
							}
							ModLaunch.McLaunchLog("选择的 Java：" + ModLaunch.m_RecordTests.ToString());
						}
					}
				}
			}
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00063F18 File Offset: 0x00062118
		public static string ExtractJavaWrapper()
		{
			string text = ModBase._MethodRepository + "JavaWrapper.jar";
			ModBase.Log("[Java] 选定的 Java Wrapper 路径：" + text, ModBase.LogLevel.Normal, "出现错误");
			object obj = ModLaunch.invocationTests;
			ObjectFlowControl.CheckForSyncLockOnValueType(obj);
			lock (obj)
			{
				try
				{
					ModBase.WriteFile(text, ModBase.GetResources("JavaWrapper"), false);
				}
				catch (Exception ex)
				{
					if (File.Exists(text))
					{
						ModBase.Log(ex, "Java Wrapper 文件释放失败，但文件已存在，将在删除后尝试重新生成", ModBase.LogLevel.Developer, "出现错误");
						try
						{
							File.Delete(text);
							ModBase.WriteFile(text, ModBase.GetResources("JavaWrapper"), false);
							goto IL_F3;
						}
						catch (Exception ex2)
						{
							ModBase.Log(ex2, "Java Wrapper 文件重新释放失败，将尝试更换文件名重新生成", ModBase.LogLevel.Developer, "出现错误");
							text = ModBase._MethodRepository + "JavaWrapper2.jar";
							try
							{
								ModBase.WriteFile(text, ModBase.GetResources("JavaWrapper"), false);
							}
							catch (Exception innerException)
							{
								throw new FileNotFoundException("释放 Java Wrapper 最终尝试失败", innerException);
							}
							goto IL_F3;
						}
						goto IL_E7;
						IL_F3:
						return text;
					}
					IL_E7:
					throw new FileNotFoundException("释放 Java Wrapper 失败", ex);
				}
			}
			return text;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00064060 File Offset: 0x00062260
		private static void McLaunchArgumentMain(ModLoader.LoaderTask<string, List<ModMinecraft.McLibToken>> Loader)
		{
			ModLaunch.McLaunchLog("开始获取 Minecraft 启动参数");
			string text;
			if (ModMinecraft.AddClient().NewThread()["arguments"] != null && ModMinecraft.AddClient().NewThread()["arguments"]["jvm"] != null)
			{
				ModLaunch.McLaunchLog("获取新版 JVM 参数");
				text = ModLaunch.McLaunchArgumentsJvmNew(ModMinecraft.AddClient());
				ModLaunch.McLaunchLog("新版 JVM 参数获取成功：");
				ModLaunch.McLaunchLog(text);
			}
			else
			{
				ModLaunch.McLaunchLog("获取旧版 JVM 参数");
				text = ModLaunch.McLaunchArgumentsJvmOld(ModMinecraft.AddClient());
				ModLaunch.McLaunchLog("旧版 JVM 参数获取成功：");
				ModLaunch.McLaunchLog(text);
			}
			if (!string.IsNullOrEmpty((string)ModMinecraft.AddClient().NewThread()["minecraftArguments"]))
			{
				ModLaunch.McLaunchLog("获取旧版 Game 参数");
				text = text + " " + ModLaunch.McLaunchArgumentsGameOld(ModMinecraft.AddClient());
				ModLaunch.McLaunchLog("旧版 Game 参数获取成功");
			}
			if (ModMinecraft.AddClient().NewThread()["arguments"] != null && ModMinecraft.AddClient().NewThread()["arguments"]["game"] != null)
			{
				ModLaunch.McLaunchLog("获取新版 Game 参数");
				text = text + " " + ModLaunch.McLaunchArgumentsGameNew(ModMinecraft.AddClient());
				ModLaunch.McLaunchLog("新版 Game 参数获取成功");
			}
			Dictionary<string, string> dictionary = ModLaunch.McLaunchArgumentsReplace(ModMinecraft.AddClient(), ref Loader);
			if (string.IsNullOrWhiteSpace(dictionary["${version_type}"]))
			{
				text = text.Replace(" --versionType ${version_type}", "");
				dictionary["${version_type}"] = "\"\"";
			}
			try
			{
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					text = text.Replace(keyValuePair.Key, (keyValuePair.Value.Contains(" ") || keyValuePair.Value.Contains(":\\")) ? ("\"" + keyValuePair.Value + "\"") : keyValuePair.Value);
				}
			}
			finally
			{
				Dictionary<string, string>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			text = text.Replace(" -Dos.name=Windows 10", " -Dos.name=\"Windows 10\"");
			if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LaunchArgumentWindowType", null), 0, false))
			{
				text += " --fullscreen";
			}
			try
			{
				foreach (string text2 in ModLaunch.filterTests._HelperMap)
				{
					text = text + " " + text2.Trim();
				}
			}
			finally
			{
				List<string>.Enumerator enumerator2;
				((IDisposable)enumerator2).Dispose();
			}
			string text3 = Conversions.ToString(string.IsNullOrEmpty(ModLaunch.filterTests.m_MockMap) ? ModBase.m_IdentifierRepository.Get("VersionServerEnter", ModMinecraft.AddClient()) : ModLaunch.filterTests.m_MockMap);
			if (text3.Length > 0)
			{
				if (DateTime.Compare(ModMinecraft.AddClient()._RegistryMap, new DateTime(2023, 4, 4)) > 0)
				{
					text += string.Format(" --quickPlayMultiplayer \"{0}\"", text3);
				}
				else
				{
					if (text3.Contains(":"))
					{
						text = string.Concat(new string[]
						{
							text,
							" --server ",
							text3.Split(":")[0],
							" --port ",
							text3.Split(":")[1]
						});
					}
					else
					{
						text = text + " --server " + text3 + " --port 25565";
					}
					if (ModMinecraft.AddClient().Version._StatusMap)
					{
						ModMain.Hint("OptiFine 与自动进入服务器可能不兼容，有概率导致材质丢失甚至游戏崩溃！", ModMain.HintType.Critical, true);
					}
				}
			}
			string text4 = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionAdvanceGame", ModMinecraft.AddClient()));
			text = Conversions.ToString(Operators.AddObject(text, Operators.ConcatenateObject(" ", (Operators.CompareString(text4, "", false) == 0) ? ModBase.m_IdentifierRepository.Get("LaunchAdvanceGame", null) : text4)));
			ModLaunch.McLaunchLog("Minecraft 启动参数：");
			ModLaunch.McLaunchLog(text);
			ModLaunch._ServiceTests = text;
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00064460 File Offset: 0x00062660
		private static string McLaunchArgumentsJvmOld(ModMinecraft.McVersion Version)
		{
			List<string> list = new List<string>();
			list.Add("-XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump");
			string text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionAdvanceJvm", ModMinecraft.AddClient()));
			if (Operators.CompareString(text, "", false) == 0)
			{
				text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchAdvanceJvm", null));
			}
			if (!text.Contains("-Dlog4j2.formatMsgNoLookups=true"))
			{
				text += " -Dlog4j2.formatMsgNoLookups=true";
			}
			text = text.Replace(" -XX:MaxDirectMemorySize=256M", "");
			list.Insert(0, text);
			list.Add("-Xmn" + Conversions.ToString(Math.Floor(PageVersionSetup.GetRam(ModMinecraft.AddClient(), new bool?(!ModLaunch.m_RecordTests.producerRepository)) * 1024.0 * 0.15)) + "m");
			list.Add("-Xmx" + Conversions.ToString(Math.Floor(PageVersionSetup.GetRam(ModMinecraft.AddClient(), new bool?(!ModLaunch.m_RecordTests.producerRepository)) * 1024.0)) + "m");
			list.Add("\"-Djava.library.path=" + ModLaunch.GetNativesFolder() + "\"");
			list.Add("-cp ${classpath}");
			if (Operators.CompareString(ModLaunch.interceptorTests.Output.Type, "Nide", false) == 0)
			{
				list.Insert(0, Conversions.ToString(Operators.ConcatenateObject("-Dnide8auth.client=true -javaagent:\"" + ModBase.m_InstanceRepository + "nide8auth.jar\"=", ModBase.m_IdentifierRepository.Get("VersionServerNide", ModMinecraft.AddClient()))));
			}
			if (Operators.CompareString(ModLaunch.interceptorTests.Output.Type, "Auth", false) == 0)
			{
				string text2 = Conversions.ToString((ModLaunch.interceptorTests.Input.Type == ModLaunch.McLoginType.Legacy) ? "http://hiperauth.tech/api/yggdrasil-hiper/" : ModBase.m_IdentifierRepository.Get("VersionServerAuthServer", ModMinecraft.AddClient()));
				try
				{
					string s = Conversions.ToString(ModNet.NetGetCodeByRequestRetry(text2, Encoding.UTF8, "", false, null, false));
					list.Insert(0, string.Concat(new string[]
					{
						"-javaagent:\"",
						ModBase._MethodRepository,
						"authlib-injector.jar\"=",
						text2,
						" -Dauthlibinjector.side=client -Dauthlibinjector.yggdrasil.prefetched=",
						Convert.ToBase64String(Encoding.UTF8.GetBytes(s))
					}));
				}
				catch (Exception innerException)
				{
					throw new Exception("无法连接到第三方登录服务器（" + text2 + "）", innerException);
				}
			}
			if (ModLaunch.m_RecordTests.PostTests() >= 9)
			{
				list.Add("--add-exports cpw.mods.bootstraplauncher/cpw.mods.bootstraplauncher=ALL-UNNAMED");
			}
			list.Add("-Doolloo.jlw.tmpdir=\"" + ModBase._MethodRepository.TrimEnd(new char[]
			{
				'\\'
			}) + "\"");
			list.Add("-jar \"" + ModLaunch.ExtractJavaWrapper() + "\"");
			if (Version.NewThread()["mainClass"] == null)
			{
				throw new Exception("版本 json 中没有 mainClass 项！");
			}
			list.Add((string)Version.NewThread()["mainClass"]);
			return list.Join(" ");
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00064784 File Offset: 0x00062984
		private static string McLaunchArgumentsJvmNew(ModMinecraft.McVersion Version)
		{
			List<string> list = new List<string>();
			ModMinecraft.McVersion mcVersion = Version;
			for (;;)
			{
				if (mcVersion.NewThread()["arguments"] != null && mcVersion.NewThread()["arguments"]["jvm"] != null)
				{
					try
					{
						foreach (JToken jtoken in mcVersion.NewThread()["arguments"]["jvm"])
						{
							if (jtoken.Type == 8)
							{
								list.Add(jtoken.ToString());
							}
							else if (ModMinecraft.McJsonRuleCheck(jtoken["rules"]))
							{
								if (jtoken["value"].Type == 8)
								{
									list.Add(jtoken["value"].ToString());
								}
								else
								{
									try
									{
										foreach (JToken jtoken2 in jtoken["value"])
										{
											list.Add(jtoken2.ToString());
										}
									}
									finally
									{
										IEnumerator<JToken> enumerator2;
										if (enumerator2 != null)
										{
											enumerator2.Dispose();
										}
									}
								}
							}
						}
						goto IL_0D;
					}
					finally
					{
						IEnumerator<JToken> enumerator;
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
					continue;
				}
				IL_0D:
				if (Operators.CompareString(mcVersion.CallThread(), "", false) == 0)
				{
					break;
				}
				mcVersion = new ModMinecraft.McVersion(mcVersion.CallThread());
			}
			ModSecret.SecretLaunchJvmArgs(ref list);
			if (Operators.CompareString(ModLaunch.interceptorTests.Output.Type, "Nide", false) == 0)
			{
				list.Insert(0, Conversions.ToString(Operators.ConcatenateObject("-javaagent:\"" + ModBase.m_InstanceRepository + "nide8auth.jar\"=", ModBase.m_IdentifierRepository.Get("VersionServerNide", ModMinecraft.AddClient()))));
			}
			if (Operators.CompareString(ModLaunch.interceptorTests.Output.Type, "Auth", false) == 0)
			{
				string text = Conversions.ToString((ModLaunch.interceptorTests.Input.Type == ModLaunch.McLoginType.Legacy) ? "http://hiperauth.tech/api/yggdrasil-hiper/" : ModBase.m_IdentifierRepository.Get("VersionServerAuthServer", ModMinecraft.AddClient()));
				try
				{
					string s = Conversions.ToString(ModNet.NetGetCodeByRequestRetry(text, Encoding.UTF8, "", false, null, false));
					list.Insert(0, string.Concat(new string[]
					{
						"-javaagent:\"",
						ModBase._MethodRepository,
						"authlib-injector.jar\"=",
						text,
						" -Dauthlibinjector.side=client -Dauthlibinjector.yggdrasil.prefetched=",
						Convert.ToBase64String(Encoding.UTF8.GetBytes(s))
					}));
				}
				catch (Exception innerException)
				{
					throw new Exception("无法连接到第三方登录服务器（" + text + "）", innerException);
				}
			}
			if (ModLaunch.m_RecordTests.PostTests() >= 9)
			{
				list.Add("--add-exports cpw.mods.bootstraplauncher/cpw.mods.bootstraplauncher=ALL-UNNAMED");
			}
			list.Add("-Doolloo.jlw.tmpdir=\"" + ModBase._MethodRepository.TrimEnd(new char[]
			{
				'\\'
			}) + "\"");
			list.Add("-jar \"" + ModLaunch.ExtractJavaWrapper() + "\"");
			List<string> list2 = new List<string>();
			checked
			{
				int num = list.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					string text2 = list[i];
					if (list[i].StartsWithF("-", false))
					{
						while (i < list.Count - 1 && !list[i + 1].StartsWithF("-", false))
						{
							i++;
							text2 = text2 + " " + list[i];
						}
					}
					list2.Add(text2.Trim().Replace("McEmu= ", "McEmu="));
				}
				list2.Remove("-XX:MaxDirectMemorySize=256M");
				string str = Enumerable.ToList<string>(Enumerable.Distinct<string>(list2)).Join(" ");
				if (Version.NewThread()["mainClass"] == null)
				{
					throw new Exception("版本 json 中没有 mainClass 项！");
				}
				return str + " " + Version.NewThread()["mainClass"].ToString();
			}
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00064BA8 File Offset: 0x00062DA8
		private static string McLaunchArgumentsGameOld(ModMinecraft.McVersion Version)
		{
			List<string> list = new List<string>();
			string text = Version.NewThread()["minecraftArguments"].ToString();
			if (!text.Contains("--height"))
			{
				text += " --height ${resolution_height} --width ${resolution_width}";
			}
			list.Add(text);
			string text2 = list.Join(" ");
			if ((Version.Version.m_StructMap || Version.Version._AccountMap) && Version.Version._StatusMap)
			{
				if (text2.Contains("--tweakClass optifine.OptiFineForgeTweaker"))
				{
					ModBase.Log("[Launch] 发现正确的 OptiFineForge TweakClass，目前参数：" + text2, ModBase.LogLevel.Normal, "出现错误");
					text2 = text2.Replace(" --tweakClass optifine.OptiFineForgeTweaker", "").Replace("--tweakClass optifine.OptiFineForgeTweaker ", "") + " --tweakClass optifine.OptiFineForgeTweaker";
				}
				if (text2.Contains("--tweakClass optifine.OptiFineTweaker"))
				{
					ModBase.Log("[Launch] 发现错误的 OptiFineForge TweakClass，目前参数：" + text2, ModBase.LogLevel.Normal, "出现错误");
					text2 = text2.Replace(" --tweakClass optifine.OptiFineTweaker", "").Replace("--tweakClass optifine.OptiFineTweaker ", "") + " --tweakClass optifine.OptiFineForgeTweaker";
					try
					{
						ModBase.WriteFile(Version.Path + Version.Name + ".json", ModBase.ReadFile(Version.Path + Version.Name + ".json", null).Replace("optifine.OptiFineTweaker", "optifine.OptiFineForgeTweaker"), false, null);
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "替换 OptiFineForge TweakClass 失败", ModBase.LogLevel.Debug, "出现错误");
					}
				}
			}
			return text2;
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00064D40 File Offset: 0x00062F40
		private static string McLaunchArgumentsGameNew(ModMinecraft.McVersion Version)
		{
			List<string> list = new List<string>();
			ModMinecraft.McVersion mcVersion = Version;
			for (;;)
			{
				if (mcVersion.NewThread()["arguments"] != null && mcVersion.NewThread()["arguments"]["game"] != null)
				{
					try
					{
						foreach (JToken jtoken in mcVersion.NewThread()["arguments"]["game"])
						{
							if (jtoken.Type == 8)
							{
								list.Add(jtoken.ToString());
							}
							else if (ModMinecraft.McJsonRuleCheck(jtoken["rules"]))
							{
								if (jtoken["value"].Type == 8)
								{
									list.Add(jtoken["value"].ToString());
								}
								else
								{
									try
									{
										foreach (JToken jtoken2 in jtoken["value"])
										{
											list.Add(jtoken2.ToString());
										}
									}
									finally
									{
										IEnumerator<JToken> enumerator2;
										if (enumerator2 != null)
										{
											enumerator2.Dispose();
										}
									}
								}
							}
						}
						goto IL_0D;
					}
					finally
					{
						IEnumerator<JToken> enumerator;
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
					continue;
				}
				IL_0D:
				if (Operators.CompareString(mcVersion.CallThread(), "", false) == 0)
				{
					break;
				}
				mcVersion = new ModMinecraft.McVersion(mcVersion.CallThread());
			}
			List<string> list2 = new List<string>();
			checked
			{
				int num = list.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					string text = list[i];
					if (list[i].StartsWithF("-", false))
					{
						while (i < list.Count - 1 && !list[i + 1].StartsWithF("-", false))
						{
							i++;
							text = text + " " + list[i];
						}
					}
					list2.Add(text);
				}
				string text2 = Enumerable.ToList<string>(Enumerable.Distinct<string>(list2)).Join(" ");
				if ((Version.Version.m_StructMap || Version.Version._AccountMap) && Version.Version._StatusMap)
				{
					if (text2.Contains("--tweakClass optifine.OptiFineForgeTweaker"))
					{
						ModBase.Log("[Launch] 发现正确的 OptiFineForge TweakClass，目前参数：" + text2, ModBase.LogLevel.Normal, "出现错误");
						text2 = text2.Replace(" --tweakClass optifine.OptiFineForgeTweaker", "").Replace("--tweakClass optifine.OptiFineForgeTweaker ", "") + " --tweakClass optifine.OptiFineForgeTweaker";
					}
					if (text2.Contains("--tweakClass optifine.OptiFineTweaker"))
					{
						ModBase.Log("[Launch] 发现错误的 OptiFineForge TweakClass，目前参数：" + text2, ModBase.LogLevel.Normal, "出现错误");
						text2 = text2.Replace(" --tweakClass optifine.OptiFineTweaker", "").Replace("--tweakClass optifine.OptiFineTweaker ", "") + " --tweakClass optifine.OptiFineForgeTweaker";
						try
						{
							ModBase.WriteFile(Version.Path + Version.Name + ".json", ModBase.ReadFile(Version.Path + Version.Name + ".json", null).Replace("optifine.OptiFineTweaker", "optifine.OptiFineForgeTweaker"), false, null);
						}
						catch (Exception ex)
						{
							ModBase.Log(ex, "替换 OptiFineForge TweakClass 失败", ModBase.LogLevel.Debug, "出现错误");
						}
					}
				}
				return text2;
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x000650A4 File Offset: 0x000632A4
		private static Dictionary<string, string> McLaunchArgumentsReplace(ModMinecraft.McVersion Version, ref ModLoader.LoaderTask<string, List<ModMinecraft.McLibToken>> Loader)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("${classpath_separator}", ";");
			dictionary.Add("${natives_directory}", ModLaunch.GetNativesFolder());
			dictionary.Add("${library_directory}", ModMinecraft.m_ProxyTests + "libraries");
			dictionary.Add("${libraries_directory}", ModMinecraft.m_ProxyTests + "libraries");
			dictionary.Add("${launcher_name}", "PCL");
			dictionary.Add("${launcher_version}", Conversions.ToString(347));
			dictionary.Add("${version_name}", Version.Name);
			string text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionArgumentInfo", ModMinecraft.AddClient()));
			dictionary.Add("${version_type}", Conversions.ToString((Operators.CompareString(text, "", false) == 0) ? ModBase.m_IdentifierRepository.Get("LaunchArgumentInfo", null) : text));
			dictionary.Add("${game_directory}", Strings.Left(ModMinecraft.AddClient().ChangeMapper(), checked(Enumerable.Count<char>(ModMinecraft.AddClient().ChangeMapper()) - 1)));
			dictionary.Add("${assets_root}", ModMinecraft.m_ProxyTests + "assets");
			dictionary.Add("${user_properties}", "{}");
			dictionary.Add("${auth_player_name}", ModLaunch.interceptorTests.Output.Name);
			dictionary.Add("${auth_uuid}", ModLaunch.interceptorTests.Output.m_InstanceMap);
			dictionary.Add("${auth_access_token}", ModLaunch.interceptorTests.Output._StateMap);
			dictionary.Add("${access_token}", ModLaunch.interceptorTests.Output._StateMap);
			dictionary.Add("${auth_session}", ModLaunch.interceptorTests.Output._StateMap);
			dictionary.Add("${user_type}", "msa");
			object left = ModBase.m_IdentifierRepository.Get("LaunchArgumentWindowType", null);
			Size $VB$Local_Result;
			if (Operators.ConditionalCompareObjectEqual(left, 2, false))
			{
				ModLaunch._Closure$__57-0 CS$<>8__locals1 = new ModLaunch._Closure$__57-0(CS$<>8__locals1);
				ModBase.RunInUiWait(delegate()
				{
					CS$<>8__locals1.$VB$Local_Result = new Size(ModBase.GetPixelSize(ModMain._ProcessIterator.PanForm.ActualWidth), ModBase.GetPixelSize(ModMain._ProcessIterator.PanForm.ActualHeight));
				});
				$VB$Local_Result = CS$<>8__locals1.$VB$Local_Result;
			}
			else if (Operators.ConditionalCompareObjectEqual(left, 3, false))
			{
				$VB$Local_Result = new Size(Conversions.ToDouble(NewLateBinding.LateGet(null, typeof(Math), "Max", new object[]
				{
					100,
					Operators.SubtractObject(ModBase.m_IdentifierRepository.Get("LaunchArgumentWindowWidth", null), 2)
				}, null, null, null)), Conversions.ToDouble(NewLateBinding.LateGet(null, typeof(Math), "Max", new object[]
				{
					100,
					Operators.SubtractObject(ModBase.m_IdentifierRepository.Get("LaunchArgumentWindowHeight", null), 2)
				}, null, null, null)));
			}
			else
			{
				$VB$Local_Result = new Size(873.0, 538.0);
			}
			$VB$Local_Result.Height -= 29.5 * (double)ModBase._ConfigurationRepository / 96.0;
			if (ModMinecraft.AddClient().Version._ServerMap <= 12 && ModLaunch.m_RecordTests.PostTests() <= 8 && ModLaunch.m_RecordTests.policyRepository.Revision >= 200 && ModLaunch.m_RecordTests.policyRepository.Revision <= 321 && !ModMinecraft.AddClient().Version._StatusMap && !ModMinecraft.AddClient().Version.m_StructMap)
			{
				ModLaunch.McLaunchLog(string.Format("已应用窗口大小过大修复（{0}）", ModLaunch.m_RecordTests.policyRepository.Revision));
				$VB$Local_Result.Width /= (double)ModBase._ConfigurationRepository / 96.0;
				$VB$Local_Result.Height /= (double)ModBase._ConfigurationRepository / 96.0;
			}
			dictionary.Add("${resolution_width}", Conversions.ToString(Math.Round($VB$Local_Result.Width)));
			dictionary.Add("${resolution_height}", Conversions.ToString(Math.Round($VB$Local_Result.Height)));
			dictionary.Add("${game_assets}", ModMinecraft.m_ProxyTests + "assets\\virtual\\legacy");
			dictionary.Add("${assets_index_name}", ModMinecraft.McAssetsGetIndexName(Version));
			List<ModMinecraft.McLibToken> list = ModMinecraft.McLibListGet(Version, true);
			Loader.Output = list;
			List<string> list2 = new List<string>();
			string text2 = null;
			try
			{
				foreach (ModMinecraft.McLibToken mcLibToken in list)
				{
					if (!mcLibToken.attributeMap)
					{
						if (mcLibToken.Name != null && Operators.CompareString(mcLibToken.Name, "optifine:OptiFine", false) == 0)
						{
							text2 = mcLibToken._WrapperMap;
						}
						else
						{
							list2.Add(mcLibToken._WrapperMap);
						}
					}
				}
			}
			finally
			{
				List<ModMinecraft.McLibToken>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			if (text2 != null)
			{
				list2.Insert(checked(list2.Count - 2), text2);
			}
			dictionary.Add("${classpath}", list2.Join(";"));
			return dictionary;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x000655B4 File Offset: 0x000637B4
		private static void McLaunchNatives(ModLoader.LoaderTask<List<ModMinecraft.McLibToken>, int> Loader)
		{
			string text = ModLaunch.GetNativesFolder() + "\\";
			Directory.CreateDirectory(text);
			ModLaunch.McLaunchLog("正在解压 Natives 文件");
			List<string> list = new List<string>();
			try
			{
				foreach (ModMinecraft.McLibToken mcLibToken in Loader.Input)
				{
					if (mcLibToken.attributeMap)
					{
						ZipArchive zipArchive;
						try
						{
							zipArchive = new ZipArchive(new FileStream(mcLibToken._WrapperMap, FileMode.Open));
						}
						catch (InvalidDataException ex)
						{
							ModBase.Log(ex, "打开 Natives 文件失败（" + mcLibToken._WrapperMap + "）", ModBase.LogLevel.Debug, "出现错误");
							File.Delete(mcLibToken._WrapperMap);
							throw new Exception("无法打开 Natives 文件（" + mcLibToken._WrapperMap + "），该文件可能已损坏，请重新尝试启动游戏");
						}
						try
						{
							foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
							{
								string fullName = zipArchiveEntry.FullName;
								if (fullName.EndsWithF(".dll", true))
								{
									string text2 = text + fullName;
									list.Add(text2);
									FileInfo fileInfo = new FileInfo(text2);
									if (fileInfo.Exists)
									{
										if (fileInfo.Length == zipArchiveEntry.Length)
										{
											if (ModBase._TokenRepository)
											{
												ModLaunch.McLaunchLog("无需解压：" + text2);
												continue;
											}
											continue;
										}
										else
										{
											try
											{
												File.Delete(text2);
											}
											catch (UnauthorizedAccessException ex2)
											{
												ModLaunch.McLaunchLog("删除原 dll 访问被拒绝，这通常代表有一个 MC 正在运行，跳过解压：" + text2);
												ModLaunch.McLaunchLog("实际的错误信息：" + ModBase.GetExceptionSummary(ex2));
												break;
											}
										}
									}
									ModBase.WriteFile(text2, zipArchiveEntry.Open());
									ModLaunch.McLaunchLog("已解压：" + text2);
								}
							}
							goto IL_1C1;
						}
						finally
						{
							IEnumerator<ZipArchiveEntry> enumerator2;
							if (enumerator2 != null)
							{
								enumerator2.Dispose();
							}
						}
						IL_1B5:
						zipArchive.Dispose();
						continue;
						IL_1C1:
						if (zipArchive != null)
						{
							goto IL_1B5;
						}
					}
				}
			}
			finally
			{
				List<ModMinecraft.McLibToken>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			foreach (string text3 in Directory.GetFiles(text))
			{
				if (!list.Contains(text3))
				{
					try
					{
						ModLaunch.McLaunchLog("删除：" + text3);
						File.Delete(text3);
					}
					catch (UnauthorizedAccessException ex3)
					{
						ModLaunch.McLaunchLog("删除多余文件访问被拒绝，跳过删除步骤");
						ModLaunch.McLaunchLog("实际的错误信息：" + ModBase.GetExceptionSummary(ex3));
						break;
					}
				}
			}
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00065890 File Offset: 0x00063A90
		private static string GetNativesFolder()
		{
			string text = ModMinecraft.AddClient().Path + ModMinecraft.AddClient().Name + "-natives";
			string result;
			if (!ModBase.rulesRepository && !text.IsASCII())
			{
				text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft\\bin\\natives";
				if (text.IsASCII())
				{
					result = text;
				}
				else
				{
					result = ModBase._RefRepository + "ProgramData\\PCL\\natives";
				}
			}
			else
			{
				result = text;
			}
			return result;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00065900 File Offset: 0x00063B00
		private static void McLaunchPrerun()
		{
			try
			{
				if (Operators.CompareString(ModLaunch.interceptorTests.Output.Type, "Microsoft", false) == 0)
				{
					ModMinecraft.McFolderLauncherProfilesJsonCreate(ModMinecraft.m_ProxyTests);
					JObject jobject = (JObject)ModBase.GetJson(string.Concat(new string[]
					{
						"\r\n            {\r\n              \"authenticationDatabase\": {\r\n                \"00000111112222233333444445555566\": {\r\n                  \"username\": \"",
						ModLaunch.interceptorTests.Output.Name.Replace("\"", "-"),
						"\",\r\n                  \"profiles\": {\r\n                    \"66666555554444433333222221111100\": {\r\n                        \"displayName\": \"",
						ModLaunch.interceptorTests.Output.Name,
						"\"\r\n                    }\r\n                  }\r\n                }\r\n              },\r\n              \"clientToken\": \"",
						ModLaunch.interceptorTests.Output._CallbackMap,
						"\",\r\n              \"selectedUser\": {\r\n                \"account\": \"00000111112222233333444445555566\", \r\n                \"profile\": \"66666555554444433333222221111100\"\r\n              }\r\n            }"
					}));
					JObject jobject2 = (JObject)ModBase.GetJson(ModBase.ReadFile(ModMinecraft.m_ProxyTests + "launcher_profiles.json", null));
					jobject2.Merge(jobject);
					ModBase.WriteFile(ModMinecraft.m_ProxyTests + "launcher_profiles.json", jobject2.ToString(), false, Encoding.GetEncoding("GB18030"));
					ModLaunch.McLaunchLog("已更新 launcher_profiles.json");
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "更新 launcher_profiles.json 失败，将在删除文件后重试", ModBase.LogLevel.Debug, "出现错误");
				try
				{
					File.Delete(ModMinecraft.m_ProxyTests + "launcher_profiles.json");
					ModMinecraft.McFolderLauncherProfilesJsonCreate(ModMinecraft.m_ProxyTests);
					JObject jobject3 = (JObject)ModBase.GetJson(string.Concat(new string[]
					{
						"\r\n                    {\r\n                      \"authenticationDatabase\": {\r\n                        \"00000111112222233333444445555566\": {\r\n                          \"username\": \"",
						ModLaunch.interceptorTests.Output.Name.Replace("\"", "-"),
						"\",\r\n                          \"profiles\": {\r\n                            \"66666555554444433333222221111100\": {\r\n                                \"displayName\": \"",
						ModLaunch.interceptorTests.Output.Name,
						"\"\r\n                            }\r\n                          }\r\n                        }\r\n                      },\r\n                      \"clientToken\": \"",
						ModLaunch.interceptorTests.Output._CallbackMap,
						"\",\r\n                      \"selectedUser\": {\r\n                        \"account\": \"00000111112222233333444445555566\", \r\n                        \"profile\": \"66666555554444433333222221111100\"\r\n                      }\r\n                    }"
					}));
					JObject jobject4 = (JObject)ModBase.GetJson(ModBase.ReadFile(ModMinecraft.m_ProxyTests + "launcher_profiles.json", null));
					jobject4.Merge(jobject3);
					ModBase.WriteFile(ModMinecraft.m_ProxyTests + "launcher_profiles.json", jobject4.ToString(), false, Encoding.GetEncoding("GB18030"));
					ModLaunch.McLaunchLog("已在删除后更新 launcher_profiles.json");
				}
				catch (Exception ex2)
				{
					ModBase.Log(ex2, "更新 launcher_profiles.json 失败", ModBase.LogLevel.Feedback, "出现错误");
				}
			}
			string text = ModMinecraft.AddClient().ChangeMapper() + "options.txt";
			if (!File.Exists(text))
			{
				string text2 = ModMinecraft.AddClient().ChangeMapper() + "config\\yosbr\\options.txt";
				if (File.Exists(text2))
				{
					ModLaunch.McLaunchLog("将修改 Yosbr Mod 中的 options.txt");
					text = text2;
					ModBase.WriteIni(text, "lang", "none");
				}
			}
			try
			{
				string text3 = ModBase.ReadIni(text, "lang", "none");
				string text4 = (Operators.CompareString(text3, "none", false) == 0 || !Directory.Exists(ModMinecraft.AddClient().ChangeMapper() + "saves")) ? (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("ToolHelpChinese", null)) ? "zh_cn" : "en_us") : text3.ToLower();
				if (ModMinecraft.AddClient().Version._ServerMap < 12)
				{
					text4 = checked(text4.Substring(0, text4.Length - 2) + text4.Substring(text4.Length - 2).ToUpper());
				}
				if (Operators.CompareString(text3, text4, false) == 0)
				{
					ModLaunch.McLaunchLog(string.Format("需要的语言为 {0}，当前语言为 {1}，无需修改", text4, text3));
				}
				else
				{
					ModBase.WriteIni(text, "lang", "-");
					ModBase.WriteIni(text, "lang", text4);
					ModLaunch.McLaunchLog(string.Format("已将语言从 {0} 修改为 {1}", text3, text4));
				}
				object left = ModBase.m_IdentifierRepository.Get("LaunchArgumentWindowType", null);
				if (Operators.ConditionalCompareObjectEqual(left, 0, false))
				{
					ModBase.WriteIni(text, "fullscreen", "true");
				}
				else if (!Operators.ConditionalCompareObjectEqual(left, 1, false))
				{
					ModBase.WriteIni(text, "fullscreen", "false");
				}
			}
			catch (Exception ex3)
			{
				ModBase.Log(ex3, "更新 options.txt 失败", ModBase.LogLevel.Hint, "出现错误");
			}
			if (Conversions.ToBoolean(ModMinecraft.AddClient().Version._ServerMap <= 7 && ModMinecraft.AddClient().Version._ServerMap >= 2 && ModLaunch.interceptorTests.Input.Type == ModLaunch.McLoginType.Legacy && (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LaunchSkinType", null), 2, false) || (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LaunchSkinType", null), 4, false) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LaunchSkinSlim", null))))))
			{
				ModMain.Hint("此 Minecraft 版本尚不支持 Alex 皮肤，你的皮肤可能会显示为 Steve！", ModMain.HintType.Critical, true);
			}
			try
			{
				Directory.CreateDirectory(ModMinecraft.AddClient().ChangeMapper() + "resourcepacks\\");
				string path = ModMinecraft.AddClient().ChangeMapper() + "resourcepacks\\PCL2 Skin.zip";
				bool flag = ModMinecraft.AddClient().Version._ServerMap >= 13 || ModMinecraft.AddClient().Version._ServerMap < 6;
				if (ModLaunch.interceptorTests.Input.Type == ModLaunch.McLoginType.Legacy && Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LaunchSkinType", null), 4, false) && File.Exists(ModBase.m_InstanceRepository + "CustomSkin.png"))
				{
					Directory.CreateDirectory(ModBase.m_DecoratorRepository);
					string text5 = ModBase.m_DecoratorRepository + "pack.mcmeta";
					string text6 = ModBase.m_DecoratorRepository + "pack.png";
					int value;
					switch (ModMinecraft.AddClient().Version._ServerMap)
					{
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
						ModLaunch.McLaunchLog("Minecraft 版本过老，尚不支持自定义离线皮肤");
						goto IL_B91;
					case 6:
					case 7:
					case 8:
						value = 1;
						break;
					case 9:
					case 10:
						value = 2;
						break;
					case 11:
					case 12:
						value = 3;
						break;
					case 13:
					case 14:
						value = 4;
						break;
					case 15:
						value = 5;
						break;
					case 16:
						value = 6;
						break;
					case 17:
						value = 7;
						break;
					case 18:
						if (ModMinecraft.AddClient().Version.m_ResolverMap <= 2)
						{
							value = 8;
						}
						else
						{
							value = 9;
						}
						break;
					case 19:
						if (ModMinecraft.AddClient().Version.m_ResolverMap <= 3)
						{
							value = 9;
						}
						else
						{
							value = 12;
						}
						break;
					case 20:
						if (ModMinecraft.AddClient().Version.m_ResolverMap <= 1)
						{
							value = 15;
						}
						else
						{
							value = 17;
						}
						break;
					default:
						value = 17;
						break;
					}
					ModLaunch.McLaunchLog("正在构建自定义皮肤资源包，格式为：" + Conversions.ToString(value));
					new MyBitmap(ModBase.m_SerializerRepository + "Heads/Logo.png").Save(text6);
					ModBase.WriteFile(text5, "{\"pack\":{\"pack_format\":" + Conversions.ToString(value) + ",\"description\":\"PCL 自定义离线皮肤资源包\"}}", false, null);
					MyBitmap myBitmap = new MyBitmap(ModBase.m_InstanceRepository + "CustomSkin.png");
					if ((ModMinecraft.AddClient().Version._ServerMap == 6 || ModMinecraft.AddClient().Version._ServerMap == 7) && myBitmap._ContainerIterator.Height == 64)
					{
						ModLaunch.McLaunchLog("该 Minecraft 版本不支持双层皮肤，已进行裁剪");
						myBitmap = myBitmap.Clip(0, 0, 64, 32);
					}
					myBitmap.Save(ModBase.Path + "PCL\\CustomSkin_Cliped.png");
					using (FileStream fileStream = new FileStream(path, FileMode.Create))
					{
						using (ZipArchive zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create))
						{
							zipArchive.CreateEntryFromFile(text5, "pack.mcmeta");
							zipArchive.CreateEntryFromFile(text6, "pack.png");
							int serverMap = ModMinecraft.AddClient().Version._ServerMap;
							bool flag2 = serverMap < 19 || (serverMap == 19 && ModMinecraft.AddClient().Version.m_ResolverMap <= 2);
							if (flag2)
							{
								zipArchive.CreateEntryFromFile(ModBase.Path + "PCL\\CustomSkin_Cliped.png", "assets/minecraft/textures/entity/" + (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LaunchSkinSlim", null)) ? "alex.png" : "steve.png"));
							}
							else
							{
								foreach (string arg in new string[]
								{
									"alex",
									"ari",
									"efe",
									"kai",
									"makena",
									"noor",
									"steve",
									"sunny",
									"zuri"
								})
								{
									zipArchive.CreateEntryFromFile(ModBase.Path + "PCL\\CustomSkin_Cliped.png", string.Format("assets/minecraft/textures/entity/player/{0}/{1}.png", Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LaunchSkinSlim", null)) ? "slim" : "wide", arg));
								}
							}
						}
					}
					File.Delete(ModBase.Path + "PCL\\CustomSkin_Cliped.png");
					ModBase.IniClearCache(text);
					string text7 = ModBase.ReadIni(text, "resourcePacks", "[]").TrimStart(new char[]
					{
						'['
					}).TrimEnd(new char[]
					{
						']'
					});
					if (flag)
					{
						if (Operators.CompareString(text7, "", false) == 0)
						{
							text7 = "\"vanilla\"";
						}
						List<string> list = new List<string>(text7.Split(","));
						List<string> list2 = new List<string>();
						try
						{
							foreach (string text8 in list)
							{
								if (Operators.CompareString(text8, "\"file/PCL2 Skin.zip\"", false) != 0 && Operators.CompareString(text8, "", false) != 0)
								{
									list2.Add(text8);
								}
							}
						}
						finally
						{
							List<string>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
						list2.Add("\"file/PCL2 Skin.zip\"");
						string value2 = "[" + list2.Join(",") + "]";
						ModBase.WriteIni(text, "resourcePacks", value2);
					}
					else
					{
						List<string> list3 = new List<string>(text7.Split(","));
						List<string> list4 = new List<string>();
						try
						{
							foreach (string text9 in list3)
							{
								if (Operators.CompareString(text9, "\"PCL2 Skin.zip\"", false) != 0 && Operators.CompareString(text9, "", false) != 0)
								{
									list4.Add(text9);
								}
							}
						}
						finally
						{
							List<string>.Enumerator enumerator2;
							((IDisposable)enumerator2).Dispose();
						}
						list4.Add("\"PCL2 Skin.zip\"");
						string value3 = "[" + list4.Join(",") + "]";
						ModBase.WriteIni(text, "resourcePacks", value3);
					}
				}
				else if (File.Exists(path))
				{
					ModLaunch.McLaunchLog("正在清空自定义皮肤资源包");
					File.Delete(path);
					ModBase.IniClearCache(text);
					string text10 = ModBase.ReadIni(text, "resourcePacks", "[]").TrimStart(new char[]
					{
						'['
					}).TrimEnd(new char[]
					{
						']'
					});
					if (flag)
					{
						if (Operators.CompareString(text10, "", false) == 0)
						{
							text10 = "\"vanilla\"";
						}
						List<string> list5 = new List<string>(text10.Split(","));
						list5.Remove("\"file/PCL2 Skin.zip\"");
						string value4 = "[" + list5.Join(",") + "]";
						ModBase.WriteIni(text, "resourcePacks", value4);
					}
					else
					{
						List<string> list6 = new List<string>(text10.Split(","));
						list6.Remove("\"PCL2 Skin.zip\"");
						string value5 = "[" + list6.Join(",") + "]";
						ModBase.WriteIni(text, "resourcePacks", value5);
					}
				}
				IL_B91:;
			}
			catch (Exception ex4)
			{
				ModBase.Log(ex4, "离线皮肤资源包设置失败", ModBase.LogLevel.Hint, "出现错误");
			}
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00066588 File Offset: 0x00064788
		private static void McLaunchCustom(ModLoader.LoaderTask<int, int> Loader)
		{
			string text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchAdvanceRun", null));
			if (Operators.CompareString(text, "", false) != 0)
			{
				text = ModLaunch.ArgumentReplace(text, true);
			}
			string text2 = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionAdvanceRun", ModMinecraft.AddClient()));
			if (Operators.CompareString(text2, "", false) != 0)
			{
				text2 = ModLaunch.ArgumentReplace(text2, true);
			}
			try
			{
				string raw = string.Concat(new string[]
				{
					"@echo off\r\ntitle 启动 - ",
					ModMinecraft.AddClient().Name,
					"\r\necho 游戏正在启动，请稍候。\r\nset APPDATA=\"",
					ModMinecraft.m_ProxyTests,
					"\"\r\ncd /D \"",
					ModMinecraft.m_ProxyTests,
					"\"\r\n",
					text,
					"\r\n",
					text2,
					"\r\n\"",
					ModLaunch.m_RecordTests.StartTests(),
					"\" ",
					ModLaunch._ServiceTests,
					"\r\necho 游戏已退出。\r\npause"
				});
				ModBase.WriteFile(ModLaunch.filterTests.requestMap ?? (ModBase.Path + "PCL\\LatestLaunch.bat"), ModSecret.SecretFilter(raw, 'F'), false, Encoding.Default.Equals(Encoding.UTF8) ? Encoding.UTF8 : Encoding.GetEncoding("GB18030"));
				if (ModLaunch.filterTests.requestMap != null)
				{
					ModLaunch.McLaunchLog("导出启动脚本完成，强制结束启动过程");
					ModLaunch._PageTests = "导出启动脚本成功！";
					ModBase.OpenExplorer("/select,\"" + ModLaunch.filterTests.requestMap + "\"");
					Loader.Parent.Abort();
					return;
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "输出启动脚本失败", ModBase.LogLevel.Debug, "出现错误");
				if (ModLaunch.filterTests.requestMap != null)
				{
					throw ex;
				}
			}
			if (Operators.CompareString(text, "", false) != 0)
			{
				ModLaunch.McLaunchLog("正在执行全局自定义命令：" + text);
				Process process = new Process();
				try
				{
					process.StartInfo.FileName = "cmd.exe";
					process.StartInfo.Arguments = "/c \"" + text + "\"";
					process.StartInfo.WorkingDirectory = ModMinecraft.m_ProxyTests;
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.CreateNoWindow = true;
					process.Start();
					if (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LaunchAdvanceRunWait", null)))
					{
						while (!process.HasExited && !Loader.IsAborted)
						{
							Thread.Sleep(10);
						}
					}
				}
				catch (Exception ex2)
				{
					ModBase.Log(ex2, "执行全局自定义命令失败", ModBase.LogLevel.Hint, "出现错误");
				}
				finally
				{
					if (!process.HasExited && Loader.IsAborted)
					{
						ModLaunch.McLaunchLog("由于取消启动，已强制结束自定义命令 CMD 进程");
						process.Kill();
					}
				}
			}
			if (Operators.CompareString(text2, "", false) != 0)
			{
				ModLaunch.McLaunchLog("正在执行版本自定义命令：" + text2);
				Process process2 = new Process();
				try
				{
					process2.StartInfo.FileName = "cmd.exe";
					process2.StartInfo.Arguments = "/c \"" + text2 + "\"";
					process2.StartInfo.WorkingDirectory = ModMinecraft.m_ProxyTests;
					process2.StartInfo.UseShellExecute = false;
					process2.StartInfo.CreateNoWindow = true;
					process2.Start();
					if (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("VersionAdvanceRunWait", ModMinecraft.AddClient())))
					{
						while (!process2.HasExited && !Loader.IsAborted)
						{
							Thread.Sleep(10);
						}
					}
				}
				catch (Exception ex3)
				{
					ModBase.Log(ex3, "执行版本自定义命令失败", ModBase.LogLevel.Hint, "出现错误");
				}
				finally
				{
					if (!process2.HasExited && Loader.IsAborted)
					{
						ModLaunch.McLaunchLog("由于取消启动，已强制结束自定义命令 CMD 进程");
						process2.Kill();
					}
				}
			}
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x000669CC File Offset: 0x00064BCC
		private static void McLaunchRun(ModLoader.LoaderTask<int, Process> Loader)
		{
			Process process = new Process();
			ProcessStartInfo processStartInfo = new ProcessStartInfo(ModLaunch.m_RecordTests.PrintTests());
			if (processStartInfo.EnvironmentVariables.ContainsKey("appdata"))
			{
				processStartInfo.EnvironmentVariables["appdata"] = ModMinecraft.m_ProxyTests;
			}
			else
			{
				processStartInfo.EnvironmentVariables.Add("appdata", ModMinecraft.m_ProxyTests);
			}
			List<string> list = new List<string>(processStartInfo.EnvironmentVariables["Path"].Split(";"));
			list.Add(ModLaunch.m_RecordTests._UtilsRepository);
			processStartInfo.EnvironmentVariables["Path"] = Enumerable.ToList<string>(Enumerable.Distinct<string>(list)).Join(";");
			processStartInfo.StandardErrorEncoding = Encoding.UTF8;
			processStartInfo.StandardOutputEncoding = Encoding.UTF8;
			processStartInfo.WorkingDirectory = ModMinecraft.AddClient().ChangeMapper();
			processStartInfo.UseShellExecute = false;
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.CreateNoWindow = false;
			processStartInfo.Arguments = ModLaunch._ServiceTests;
			process.StartInfo = processStartInfo;
			process.Start();
			ModLaunch.McLaunchLog("已启动游戏进程：" + ModLaunch.m_RecordTests.PrintTests());
			if (Loader.IsAborted)
			{
				ModLaunch.McLaunchLog("由于取消启动，已强制结束游戏进程");
				process.Kill();
				return;
			}
			Loader.Output = process;
			ModLaunch._PoolTests = process;
			try
			{
				process.PriorityBoostEnabled = true;
				object left = ModBase.m_IdentifierRepository.Get("LaunchArgumentPriority", null);
				if (Operators.ConditionalCompareObjectEqual(left, 0, false))
				{
					process.PriorityClass = ProcessPriorityClass.AboveNormal;
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 2, false))
				{
					process.PriorityClass = ProcessPriorityClass.BelowNormal;
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "设置进程优先级失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00066BA0 File Offset: 0x00064DA0
		private static void McLaunchWait(ModLoader.LoaderTask<Process, int> Loader)
		{
			ModLaunch.McLaunchLog("");
			ModLaunch.McLaunchLog("~ 基础参数 ~");
			ModLaunch.McLaunchLog("PCL 版本：Release 2.8.12 (" + Conversions.ToString(347) + ")");
			ModLaunch.McLaunchLog(string.Concat(new string[]
			{
				"游戏版本：",
				ModMinecraft.AddClient().Version.ToString(),
				"（识别为 1.",
				Conversions.ToString(ModMinecraft.AddClient().Version._ServerMap),
				".",
				Conversions.ToString(ModMinecraft.AddClient().Version.m_ResolverMap),
				"）"
			}));
			ModLaunch.McLaunchLog("资源版本：" + ModMinecraft.McAssetsGetIndexName(ModMinecraft.AddClient()));
			ModLaunch.McLaunchLog("版本继承：" + ((Operators.CompareString(ModMinecraft.AddClient().CallThread(), "", false) == 0) ? "无" : ModMinecraft.AddClient().CallThread()));
			ModLaunch.McLaunchLog(string.Concat(new string[]
			{
				"分配的内存：",
				Conversions.ToString(PageVersionSetup.GetRam(ModMinecraft.AddClient(), new bool?(!ModLaunch.m_RecordTests.producerRepository))),
				" GB（",
				Conversions.ToString(Math.Round(PageVersionSetup.GetRam(ModMinecraft.AddClient(), new bool?(!ModLaunch.m_RecordTests.producerRepository)) * 1024.0)),
				" MB）"
			}));
			ModLaunch.McLaunchLog("MC 文件夹：" + ModMinecraft.m_ProxyTests);
			ModLaunch.McLaunchLog("版本文件夹：" + ModMinecraft.AddClient().Path);
			ModLaunch.McLaunchLog("版本隔离：" + Conversions.ToString(Operators.CompareString(ModMinecraft.AddClient().ChangeMapper(), ModMinecraft.AddClient().Path, false) == 0));
			ModLaunch.McLaunchLog("HMCL 格式：" + Conversions.ToString(ModMinecraft.AddClient().InvokeThread()));
			ModLaunch.McLaunchLog("Java 信息：" + ((ModLaunch.m_RecordTests != null) ? ModLaunch.m_RecordTests.ToString() : "无可用 Java"));
			ModLaunch.McLaunchLog("环境变量：" + ((ModLaunch.m_RecordTests != null) ? (ModLaunch.m_RecordTests.CompareTests() ? "已设置" : "未设置") : "未设置"));
			ModLaunch.McLaunchLog("Natives 文件夹：" + ModLaunch.GetNativesFolder());
			ModLaunch.McLaunchLog("");
			ModLaunch.McLaunchLog("~ 登录参数 ~");
			ModLaunch.McLaunchLog("玩家用户名：" + ModLaunch.interceptorTests.Output.Name);
			ModLaunch.McLaunchLog("AccessToken：" + ModLaunch.interceptorTests.Output._StateMap);
			ModLaunch.McLaunchLog("ClientToken：" + ModLaunch.interceptorTests.Output._CallbackMap);
			ModLaunch.McLaunchLog("UUID：" + ModLaunch.interceptorTests.Output.m_InstanceMap);
			ModLaunch.McLaunchLog("登录方式：" + ModLaunch.interceptorTests.Output.Type);
			ModLaunch.McLaunchLog("");
			string text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionArgumentTitle", ModMinecraft.AddClient()));
			if (Operators.CompareString(text, "", false) == 0)
			{
				text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchArgumentTitle", null));
			}
			text = ModLaunch.ArgumentReplace(text, false);
			ModWatcher.Watcher watcher = new ModWatcher.Watcher(Loader, ModMinecraft.AddClient(), text);
			ModLaunch.customerTests = watcher;
			while (watcher.State == ModWatcher.Watcher.MinecraftState.Loading)
			{
				Thread.Sleep(100);
			}
			if (watcher.State == ModWatcher.Watcher.MinecraftState.Crashed)
			{
				throw new Exception("$$");
			}
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00066F3C File Offset: 0x0006513C
		private static void McLaunchEnd()
		{
			ModLaunch.McLaunchLog("开始启动结束处理");
			if (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiMusicStop", null)))
			{
				ModBase.RunInUi((ModLaunch._Closure$__.$I64-0 == null) ? (ModLaunch._Closure$__.$I64-0 = delegate()
				{
					if (ModMusic.MusicPause())
					{
						ModBase.Log("[Music] 已根据设置，在启动后暂停音乐播放", ModBase.LogLevel.Normal, "出现错误");
					}
				}) : ModLaunch._Closure$__.$I64-0, false);
			}
			else if (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiMusicStart", null)))
			{
				ModBase.RunInUi((ModLaunch._Closure$__.$I64-1 == null) ? (ModLaunch._Closure$__.$I64-1 = delegate()
				{
					if (ModMusic.MusicResume())
					{
						ModBase.Log("[Music] 已根据设置，在启动后开始音乐播放", ModBase.LogLevel.Normal, "出现错误");
					}
				}) : ModLaunch._Closure$__.$I64-1, false);
			}
			ModLaunch.McLaunchLog(Conversions.ToString(Operators.ConcatenateObject("启动器可见性：", ModBase.m_IdentifierRepository.Get("LaunchArgumentVisible", null))));
			object left = ModBase.m_IdentifierRepository.Get("LaunchArgumentVisible", null);
			if (Operators.ConditionalCompareObjectEqual(left, 0, false))
			{
				ModLaunch.McLaunchLog("已根据设置，在启动后关闭启动器");
				ModBase.RunInUi((ModLaunch._Closure$__.$I64-2 == null) ? (ModLaunch._Closure$__.$I64-2 = delegate()
				{
					ModMain._ProcessIterator.EndProgram(false);
				}) : ModLaunch._Closure$__.$I64-2, false);
			}
			else if (Conversions.ToBoolean(Conversions.ToBoolean(Operators.CompareObjectEqual(left, 2, false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left, 3, false))))
			{
				ModLaunch.McLaunchLog("已根据设置，在启动后隐藏启动器");
				ModBase.RunInUi((ModLaunch._Closure$__.$I64-3 == null) ? (ModLaunch._Closure$__.$I64-3 = delegate()
				{
					ModMain._ProcessIterator.Hidden = true;
				}) : ModLaunch._Closure$__.$I64-3, false);
			}
			else if (Operators.ConditionalCompareObjectEqual(left, 4, false))
			{
				ModLaunch.McLaunchLog("已根据设置，在启动后最小化启动器");
				ModBase.RunInUi((ModLaunch._Closure$__.$I64-4 == null) ? (ModLaunch._Closure$__.$I64-4 = delegate()
				{
					ModMain._ProcessIterator.WindowState = WindowState.Minimized;
				}) : ModLaunch._Closure$__.$I64-4, false);
			}
			else
			{
				Operators.ConditionalCompareObjectEqual(left, 5, false);
			}
			ModBase.m_IdentifierRepository.Set("SystemLaunchCount", Operators.AddObject(ModBase.m_IdentifierRepository.Get("SystemLaunchCount", null), 1), false, null);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00067140 File Offset: 0x00065340
		private static string ArgumentReplace(string Raw, bool ReplaceTimeAndDate)
		{
			string result;
			if (Raw == null)
			{
				result = null;
			}
			else
			{
				Raw = Raw.Replace("{minecraft}", ModMinecraft.m_ProxyTests);
				Raw = Raw.Replace("{verpath}", ModMinecraft.AddClient().Path);
				Raw = Raw.Replace("{verindie}", ModMinecraft.AddClient().ChangeMapper());
				Raw = Raw.Replace("{java}", ModLaunch.m_RecordTests._UtilsRepository);
				Raw = Raw.Replace("{user}", ModLaunch.interceptorTests.Output.Name);
				Raw = Raw.Replace("{uuid}", ModLaunch.interceptorTests.Output.m_InstanceMap);
				if (ReplaceTimeAndDate)
				{
					Raw = Raw.Replace("{date}", DateTime.Now.ToString("yyyy/M/d"));
					Raw = Raw.Replace("{time}", DateTime.Now.ToString("HH:mm:ss"));
				}
				switch (ModLaunch.interceptorTests.Input.Type)
				{
				case ModLaunch.McLoginType.Legacy:
					if (PageLinkHiper.EnableReader() == ModBase.LoadState.Finished)
					{
						Raw = Raw.Replace("{login}", "联机离线");
					}
					else
					{
						Raw = Raw.Replace("{login}", "离线");
					}
					break;
				case ModLaunch.McLoginType.Nide:
					Raw = Raw.Replace("{login}", "统一通行证");
					break;
				case ModLaunch.McLoginType.Auth:
					Raw = Raw.Replace("{login}", "Authlib-Injector");
					break;
				case ModLaunch.McLoginType.Ms:
					Raw = Raw.Replace("{login}", "正版");
					break;
				}
				Raw = Raw.Replace("{name}", ModMinecraft.AddClient().Name);
				if (Enumerable.Contains<string>(new string[]
				{
					"unknown",
					"old",
					"pending"
				}, ModMinecraft.AddClient().Version._ConnectionMap.ToLower()))
				{
					Raw = Raw.Replace("{version}", ModMinecraft.AddClient().Name);
				}
				else
				{
					Raw = Raw.Replace("{version}", ModMinecraft.AddClient().Version._ConnectionMap);
				}
				Raw = Raw.Replace("{path}", ModBase.Path);
				result = Raw;
			}
			return result;
		}

		// Token: 0x0400076E RID: 1902
		public static ModLaunch.McLaunchOptions filterTests = null;

		// Token: 0x0400076F RID: 1903
		public static ModLoader.LoaderTask<ModLaunch.McLaunchOptions, object> databaseTests = new ModLoader.LoaderTask<ModLaunch.McLaunchOptions, object>("Loader Launch", new Action<ModLoader.LoaderTask<ModLaunch.McLaunchOptions, object>>(ModLaunch.McLaunchStart), null, ThreadPriority.Normal)
		{
			OnStateChanged = delegate(ModLoader.LoaderBase a0)
			{
				ModLaunch.McLaunchState((ModLoader.LoaderTask<ModLaunch.McLaunchOptions, object>)a0);
			}
		};

		// Token: 0x04000770 RID: 1904
		public static ModLoader.LoaderCombo<object> m_PredicateTests;

		// Token: 0x04000771 RID: 1905
		public static Process _PoolTests;

		// Token: 0x04000772 RID: 1906
		public static ModWatcher.Watcher customerTests;

		// Token: 0x04000773 RID: 1907
		private static string _PageTests = null;

		// Token: 0x04000774 RID: 1908
		public static ModLoader.LoaderTask<ModLaunch.McLoginData, ModLaunch.McLoginResult> interceptorTests = new ModLoader.LoaderTask<ModLaunch.McLoginData, ModLaunch.McLoginResult>("登录", new Action<ModLoader.LoaderTask<ModLaunch.McLoginData, ModLaunch.McLoginResult>>(ModLaunch.McLoginStart), new Func<ModLaunch.McLoginData>(ModLaunch.McLoginInput), ThreadPriority.BelowNormal)
		{
			ReloadTimeout = 1,
			ProgressWeight = 15.0,
			Block = false
		};

		// Token: 0x04000775 RID: 1909
		public static ModLoader.LoaderTask<ModLaunch.McLoginMs, ModLaunch.McLoginResult> m_ContainerTests = new ModLoader.LoaderTask<ModLaunch.McLoginMs, ModLaunch.McLoginResult>("Loader Login Ms", new Action<ModLoader.LoaderTask<ModLaunch.McLoginMs, ModLaunch.McLoginResult>>(ModLaunch.McLoginMsStart), null, ThreadPriority.Normal)
		{
			ReloadTimeout = 1
		};

		// Token: 0x04000776 RID: 1910
		public static ModLoader.LoaderTask<ModLaunch.McLoginLegacy, ModLaunch.McLoginResult> _ParamsTests = new ModLoader.LoaderTask<ModLaunch.McLoginLegacy, ModLaunch.McLoginResult>("Loader Login Legacy", new Action<ModLoader.LoaderTask<ModLaunch.McLoginLegacy, ModLaunch.McLoginResult>>(ModLaunch.McLoginLegacyStart), null, ThreadPriority.Normal);

		// Token: 0x04000777 RID: 1911
		public static ModLoader.LoaderTask<ModLaunch.McLoginServer, ModLaunch.McLoginResult> dispatcherTests = new ModLoader.LoaderTask<ModLaunch.McLoginServer, ModLaunch.McLoginResult>("Loader Login Nide", new Action<ModLoader.LoaderTask<ModLaunch.McLoginServer, ModLaunch.McLoginResult>>(ModLaunch.McLoginServerStart), null, ThreadPriority.Normal)
		{
			ReloadTimeout = 600000
		};

		// Token: 0x04000778 RID: 1912
		public static ModLoader.LoaderTask<ModLaunch.McLoginServer, ModLaunch.McLoginResult> _ProcessTests = new ModLoader.LoaderTask<ModLaunch.McLoginServer, ModLaunch.McLoginResult>("Loader Login Auth", new Action<ModLoader.LoaderTask<ModLaunch.McLoginServer, ModLaunch.McLoginResult>>(ModLaunch.McLoginServerStart), null, ThreadPriority.Normal)
		{
			ReloadTimeout = 600000
		};

		// Token: 0x04000779 RID: 1913
		private static long m_ParameterTests = 0L;

		// Token: 0x0400077A RID: 1914
		public static ModJava.JavaEntry m_RecordTests = null;

		// Token: 0x0400077B RID: 1915
		private static string _ServiceTests;

		// Token: 0x0400077C RID: 1916
		private static object invocationTests = RuntimeHelpers.GetObjectValue(new object());

		// Token: 0x02000161 RID: 353
		public class McLaunchOptions
		{
			// Token: 0x06000E29 RID: 3625 RVA: 0x00008D9F File Offset: 0x00006F9F
			public McLaunchOptions()
			{
				this.m_MockMap = null;
				this.requestMap = null;
				this._DicMap = null;
				this._HelperMap = new List<string>();
			}

			// Token: 0x0400077D RID: 1917
			public string m_MockMap;

			// Token: 0x0400077E RID: 1918
			public string requestMap;

			// Token: 0x0400077F RID: 1919
			public ModMinecraft.McVersion _DicMap;

			// Token: 0x04000780 RID: 1920
			public List<string> _HelperMap;
		}

		// Token: 0x02000162 RID: 354
		public enum McLoginType
		{
			// Token: 0x04000782 RID: 1922
			Legacy,
			// Token: 0x04000783 RID: 1923
			Nide = 2,
			// Token: 0x04000784 RID: 1924
			Auth,
			// Token: 0x04000785 RID: 1925
			Ms = 5
		}

		// Token: 0x02000163 RID: 355
		public abstract class McLoginData
		{
			// Token: 0x06000E2C RID: 3628 RVA: 0x00008DC8 File Offset: 0x00006FC8
			public override bool Equals(object obj)
			{
				return obj != null && obj.GetHashCode() == this.GetHashCode();
			}

			// Token: 0x04000786 RID: 1926
			public ModLaunch.McLoginType Type;
		}

		// Token: 0x02000164 RID: 356
		public class McLoginServer : ModLaunch.McLoginData
		{
			// Token: 0x06000E2E RID: 3630 RVA: 0x00008DDD File Offset: 0x00006FDD
			public McLoginServer(ModLaunch.McLoginType Type)
			{
				this._IdentifierMap = false;
				this.Type = Type;
			}

			// Token: 0x06000E2F RID: 3631 RVA: 0x0006735C File Offset: 0x0006555C
			public override int GetHashCode()
			{
				return Convert.ToInt32(decimal.Remainder(new decimal(ModBase.GetHash(string.Concat(new string[]
				{
					this.m_IssuerMap,
					this._IndexerMap,
					this.interpreterMap,
					this.m_SerializerMap,
					Conversions.ToString((int)this.Type)
				}))), 2147483647m));
			}

			// Token: 0x04000787 RID: 1927
			public string m_IssuerMap;

			// Token: 0x04000788 RID: 1928
			public string _IndexerMap;

			// Token: 0x04000789 RID: 1929
			public string interpreterMap;

			// Token: 0x0400078A RID: 1930
			public string m_SerializerMap;

			// Token: 0x0400078B RID: 1931
			public string m_WatcherMap;

			// Token: 0x0400078C RID: 1932
			public bool _IdentifierMap;
		}

		// Token: 0x02000165 RID: 357
		public class McLoginMs : ModLaunch.McLoginData
		{
			// Token: 0x06000E31 RID: 3633 RVA: 0x000673C8 File Offset: 0x000655C8
			public McLoginMs()
			{
				this.m_SystemMap = "";
				this.paramMap = "";
				this._TagMap = "";
				this.observerMap = "";
				this.stubMap = "";
				this.Type = ModLaunch.McLoginType.Ms;
			}

			// Token: 0x06000E32 RID: 3634 RVA: 0x0006741C File Offset: 0x0006561C
			public override int GetHashCode()
			{
				return Convert.ToInt32(decimal.Remainder(new decimal(ModBase.GetHash(string.Concat(new string[]
				{
					this.m_SystemMap,
					this.paramMap,
					this._TagMap,
					this.observerMap,
					this.stubMap
				}))), 2147483647m));
			}

			// Token: 0x0400078D RID: 1933
			public string m_SystemMap;

			// Token: 0x0400078E RID: 1934
			public string paramMap;

			// Token: 0x0400078F RID: 1935
			public string _TagMap;

			// Token: 0x04000790 RID: 1936
			public string observerMap;

			// Token: 0x04000791 RID: 1937
			public string stubMap;
		}

		// Token: 0x02000166 RID: 358
		public class McLoginLegacy : ModLaunch.McLoginData
		{
			// Token: 0x06000E34 RID: 3636 RVA: 0x00008DF4 File Offset: 0x00006FF4
			public McLoginLegacy()
			{
				this.Type = ModLaunch.McLoginType.Legacy;
			}

			// Token: 0x06000E35 RID: 3637 RVA: 0x00067484 File Offset: 0x00065684
			public override int GetHashCode()
			{
				return Convert.ToInt32(decimal.Remainder(new decimal(ModBase.GetHash(this.m_RulesMap + Conversions.ToString(this.m_RefMap) + this._DecoratorMap + Conversions.ToString((int)this.Type))), 2147483647m));
			}

			// Token: 0x04000792 RID: 1938
			public string m_RulesMap;

			// Token: 0x04000793 RID: 1939
			public int m_RefMap;

			// Token: 0x04000794 RID: 1940
			public string _DecoratorMap;
		}

		// Token: 0x02000167 RID: 359
		public struct McLoginResult
		{
			// Token: 0x04000795 RID: 1941
			public string Name;

			// Token: 0x04000796 RID: 1942
			public string m_InstanceMap;

			// Token: 0x04000797 RID: 1943
			public string _StateMap;

			// Token: 0x04000798 RID: 1944
			public string Type;

			// Token: 0x04000799 RID: 1945
			public string _CallbackMap;

			// Token: 0x0400079A RID: 1946
			public string _TemplateMap;
		}
	}
}
