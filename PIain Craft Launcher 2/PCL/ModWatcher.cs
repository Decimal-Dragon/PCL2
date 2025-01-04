using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020000BA RID: 186
	[StandardModule]
	public sealed class ModWatcher
	{
		// Token: 0x0600058B RID: 1419 RVA: 0x0002F888 File Offset: 0x0002DA88
		private static void WatcherStateChanged()
		{
			bool flag = false;
			bool triggerLauncherShutdown = true;
			try
			{
				foreach (ModWatcher.Watcher watcher in ModWatcher.dicField)
				{
					if (watcher.State != ModWatcher.Watcher.MinecraftState.Loading)
					{
						if (watcher.State != ModWatcher.Watcher.MinecraftState.Running)
						{
							if (watcher.State == ModWatcher.Watcher.MinecraftState.Crashed || watcher.State == ModWatcher.Watcher.MinecraftState.Canceled)
							{
								triggerLauncherShutdown = false;
								continue;
							}
							continue;
						}
					}
					flag = true;
					break;
				}
			}
			finally
			{
				List<ModWatcher.Watcher>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			if (ModWatcher.m_HelperField != flag)
			{
				ModWatcher.m_HelperField = flag;
				if (ModWatcher.m_HelperField)
				{
					ModWatcher.MinecraftStart();
					return;
				}
				ModWatcher.MinecraftStop(triggerLauncherShutdown);
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00004F2F File Offset: 0x0000312F
		private static void MinecraftStart()
		{
			ModLaunch.McLaunchLog("[全局] 出现运行中的 Minecraft");
			ModWatcher._IssuerField = true;
			ModMain._ProcessIterator.BtnExtraShutdown.ShowRefresh();
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0002F924 File Offset: 0x0002DB24
		private static void MinecraftStop(bool TriggerLauncherShutdown)
		{
			ModLaunch.McLaunchLog("[全局] 已无运行中的 Minecraft");
			ModWatcher._IssuerField = false;
			ModMain._ProcessIterator.BtnExtraShutdown.ShowRefresh();
			if (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiMusicStop", null)))
			{
				ModBase.RunInUi((ModWatcher._Closure$__.$I6-0 == null) ? (ModWatcher._Closure$__.$I6-0 = delegate()
				{
					if (ModMusic.MusicResume())
					{
						ModBase.Log("[Music] 已根据设置，在结束后开始音乐播放", ModBase.LogLevel.Normal, "出现错误");
					}
				}) : ModWatcher._Closure$__.$I6-0, false);
			}
			else if (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiMusicStart", null)))
			{
				ModBase.RunInUi((ModWatcher._Closure$__.$I6-1 == null) ? (ModWatcher._Closure$__.$I6-1 = delegate()
				{
					if (ModMusic.MusicPause())
					{
						ModBase.Log("[Music] 已根据设置，在结束后暂停音乐播放", ModBase.LogLevel.Normal, "出现错误");
					}
				}) : ModWatcher._Closure$__.$I6-1, false);
			}
			object left = ModBase.m_IdentifierRepository.Get("LaunchArgumentVisible", null);
			if (!Operators.ConditionalCompareObjectEqual(left, 2, false))
			{
				if (Operators.ConditionalCompareObjectEqual(left, 3, false))
				{
					ModBase.RunInUi((ModWatcher._Closure$__.$I6-4 == null) ? (ModWatcher._Closure$__.$I6-4 = delegate()
					{
						ModMain._ProcessIterator.Hidden = false;
					}) : ModWatcher._Closure$__.$I6-4, false);
				}
				return;
			}
			if (TriggerLauncherShutdown)
			{
				ModBase.RunInUi((ModWatcher._Closure$__.$I6-2 == null) ? (ModWatcher._Closure$__.$I6-2 = delegate()
				{
					ModMain._ProcessIterator.EndProgram(false);
				}) : ModWatcher._Closure$__.$I6-2, false);
				return;
			}
			ModBase.RunInUi((ModWatcher._Closure$__.$I6-3 == null) ? (ModWatcher._Closure$__.$I6-3 = delegate()
			{
				ModMain._ProcessIterator.Hidden = false;
			}) : ModWatcher._Closure$__.$I6-3, false);
		}

		// Token: 0x0400030A RID: 778
		public static List<ModWatcher.Watcher> dicField = new List<ModWatcher.Watcher>();

		// Token: 0x0400030B RID: 779
		private static bool m_HelperField = false;

		// Token: 0x0400030C RID: 780
		public static bool _IssuerField = false;

		// Token: 0x020000BB RID: 187
		public class Watcher
		{
			// Token: 0x0600058E RID: 1422 RVA: 0x0002FA88 File Offset: 0x0002DC88
			public Watcher(ModLoader.LoaderTask<Process, int> Loader, ModMinecraft.McVersion Version, string WindowTitle)
			{
				ModWatcher.Watcher._Closure$__5-0 CS$<>8__locals1 = new ModWatcher.Watcher._Closure$__5-0(CS$<>8__locals1);
				CS$<>8__locals1.$VB$Local_Loader = Loader;
				CS$<>8__locals1.$VB$Local_WindowTitle = WindowTitle;
				base..ctor();
				CS$<>8__locals1.$VB$Me = this;
				this._SpecificationTest = "";
				this.m_DicTest = ModWatcher.Watcher.MinecraftState.Loading;
				this._HelperTest = new List<string>(1000);
				this.m_IssuerTest = RuntimeHelpers.GetObjectValue(new object());
				this.indexerTest = new Queue<string>();
				this.m_InterpreterTest = 0;
				this.m_SerializerTest = false;
				this.watcherTest = false;
				this._RequestTest = CS$<>8__locals1.$VB$Local_Loader;
				this._ContextTest = Version;
				this._SpecificationTest = CS$<>8__locals1.$VB$Local_WindowTitle;
				this.m_MockTest = CS$<>8__locals1.$VB$Local_Loader.Input.Id;
				this.WatcherLog("开始 Minecraft 日志监控");
				if (Operators.CompareString(this._SpecificationTest, "", false) != 0)
				{
					this.WatcherLog("要求窗口标题：" + CS$<>8__locals1.$VB$Local_WindowTitle);
				}
				List<ModWatcher.Watcher> list = new List<ModWatcher.Watcher>();
				try
				{
					foreach (ModWatcher.Watcher watcher in ModWatcher.dicField)
					{
						if (watcher.State != ModWatcher.Watcher.MinecraftState.Crashed && watcher.State != ModWatcher.Watcher.MinecraftState.Ended && watcher.State != ModWatcher.Watcher.MinecraftState.Canceled)
						{
							list.Add(watcher);
						}
					}
				}
				finally
				{
					List<ModWatcher.Watcher>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				list.Add(this);
				ModWatcher.dicField = list;
				ModWatcher.WatcherStateChanged();
				this.m_ErrorTest = CS$<>8__locals1.$VB$Local_Loader.Input;
				this.m_ErrorTest.BeginOutputReadLine();
				this.m_ErrorTest.BeginErrorReadLine();
				this.m_ErrorTest.OutputDataReceived += this.LogReceived;
				this.m_ErrorTest.ErrorDataReceived += this.LogReceived;
				ModBase.RunInNewThread(checked(delegate
				{
					try
					{
						while (CS$<>8__locals1.$VB$Me.State != ModWatcher.Watcher.MinecraftState.Ended && CS$<>8__locals1.$VB$Me.State != ModWatcher.Watcher.MinecraftState.Crashed && CS$<>8__locals1.$VB$Me.State != ModWatcher.Watcher.MinecraftState.Canceled)
						{
							if (CS$<>8__locals1.$VB$Local_Loader.State == ModBase.LoadState.Aborted)
							{
								break;
							}
							CS$<>8__locals1.$VB$Me.TimerWindow();
							CS$<>8__locals1.$VB$Me.TimerLog();
							int num = 1;
							do
							{
								if (CS$<>8__locals1.$VB$Me.watcherTest && CS$<>8__locals1.$VB$Me.m_SerializerTest && Operators.CompareString(CS$<>8__locals1.$VB$Local_WindowTitle, "", false) != 0 && CS$<>8__locals1.$VB$Me.State == ModWatcher.Watcher.MinecraftState.Running && !CS$<>8__locals1.$VB$Me.m_ErrorTest.HasExited)
								{
									string text = CS$<>8__locals1.$VB$Local_WindowTitle.Replace("{date}", DateTime.Now.ToString("yyyy/M/d")).Replace("{time}", DateTime.Now.ToString("HH:mm:ss"));
									int hWnd = (int)CS$<>8__locals1.$VB$Me._IdentifierTest;
									string text2 = new string(text.ToCharArray());
									ModWatcher.Watcher.SetWindowTextA(hWnd, ref text2);
								}
								Thread.Sleep(64);
								num++;
							}
							while (num <= 3);
						}
						CS$<>8__locals1.$VB$Me.WatcherLog("Minecraft 日志监控已退出");
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "Minecraft 日志监控主循环出错", ModBase.LogLevel.Feedback, "出现错误");
						CS$<>8__locals1.$VB$Me.State = ModWatcher.Watcher.MinecraftState.Ended;
					}
				}), "Minecraft Watcher PID " + Conversions.ToString(this.m_MockTest), ThreadPriority.Normal);
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x0600058F RID: 1423 RVA: 0x00004F50 File Offset: 0x00003150
			// (set) Token: 0x06000590 RID: 1424 RVA: 0x00004F58 File Offset: 0x00003158
			public ModWatcher.Watcher.MinecraftState State
			{
				get
				{
					return this.m_DicTest;
				}
				set
				{
					if (this.m_DicTest != value)
					{
						this.m_DicTest = value;
						ModWatcher.WatcherStateChanged();
					}
				}
			}

			// Token: 0x06000591 RID: 1425 RVA: 0x0002FC68 File Offset: 0x0002DE68
			private void LogReceived(object sender, DataReceivedEventArgs e)
			{
				object issuerTest = this.m_IssuerTest;
				ObjectFlowControl.CheckForSyncLockOnValueType(issuerTest);
				lock (issuerTest)
				{
					this._HelperTest.Add(e.Data);
				}
			}

			// Token: 0x06000592 RID: 1426 RVA: 0x0002FCBC File Offset: 0x0002DEBC
			private void TimerLog()
			{
				try
				{
					List<string> list = new List<string>();
					object issuerTest = this.m_IssuerTest;
					ObjectFlowControl.CheckForSyncLockOnValueType(issuerTest);
					lock (issuerTest)
					{
						if (!Enumerable.Any<string>(this._HelperTest))
						{
							return;
						}
						list = this._HelperTest;
						this._HelperTest = new List<string>(1000);
					}
					try
					{
						foreach (string text in list)
						{
							this.GameLog(text);
						}
					}
					finally
					{
						List<string>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					if (this.State == ModWatcher.Watcher.MinecraftState.Loading)
					{
						this.ProgressUpdate();
					}
					if (this.m_ErrorTest.HasExited)
					{
						this.WatcherLog("Minecraft 已退出，返回值：" + Conversions.ToString(this.m_ErrorTest.ExitCode));
						if (this.State == ModWatcher.Watcher.MinecraftState.Loading)
						{
							this.WatcherLog("Minecraft 尚未加载完成，可能已崩溃");
							this.Crashed();
						}
						else if (this.m_ErrorTest.ExitCode != 0 && this.State == ModWatcher.Watcher.MinecraftState.Running && this._ContextTest._RegistryMap.Year >= 2012)
						{
							this.WatcherLog("Minecraft 返回值异常，可能已崩溃");
							this.Crashed();
						}
						else if (this.State != ModWatcher.Watcher.MinecraftState.Crashed)
						{
							this.State = ModWatcher.Watcher.MinecraftState.Ended;
						}
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "输出 Minecraft 日志失败", ModBase.LogLevel.Feedback, "出现错误");
				}
			}

			// Token: 0x06000593 RID: 1427 RVA: 0x0002FE68 File Offset: 0x0002E068
			private void GameLog(string Text)
			{
				if (Text != null)
				{
					Text = Text.Replace("\r\n", "\r").Replace("\n", "\r").Replace("\r", "\r\n");
					this.indexerTest.Enqueue(Text);
					if (this.indexerTest.Count >= 501)
					{
						this.indexerTest.Dequeue();
					}
					if (this.m_InterpreterTest < 1)
					{
						this.WatcherLog("日志 1/5：已出现日志输出");
						this.m_InterpreterTest = 1;
					}
					if (this.m_InterpreterTest < 2 && Text.Contains("Setting user:"))
					{
						this.WatcherLog("日志 2/5：游戏用户已设置");
						this.m_InterpreterTest = 2;
					}
					else if (this.m_InterpreterTest < 3 && Text.ContainsF("lwjgl version", true))
					{
						this.WatcherLog("日志 3/5：LWJGL 版本已确认");
						this.m_InterpreterTest = 3;
					}
					else if (this.m_InterpreterTest < 4 && (Text.Contains("OpenAL initialized") || Text.Contains("Starting up SoundSystem")))
					{
						this.WatcherLog("日志 4/5：OpenAL 已加载");
						this.m_InterpreterTest = 4;
					}
					else if (this.m_InterpreterTest < 5 && ((Text.Contains("Created") && Text.Contains("textures") && Text.Contains("-atlas")) || Text.Contains("Found animation info")))
					{
						this.WatcherLog("日志 5/5：材质已加载");
						this.m_InterpreterTest = 5;
					}
					if (!Text.Contains("[CHAT]"))
					{
						if (Text.Contains("Someone is closing me!") || Text.Contains("Restarting Minecraft with command"))
						{
							this.WatcherLog("识别为关闭的 Log：" + Text);
							this.State = ModWatcher.Watcher.MinecraftState.Ended;
							return;
						}
						if (Text.Contains("Crash report saved to") || Text.Contains("This crash report has been saved to:"))
						{
							this.WatcherLog("识别为崩溃的 Log：" + Text);
							this.Crashed();
							return;
						}
						if (Text.Contains("Could not save crash report to"))
						{
							this.WatcherLog("识别为崩溃的 Log：" + Text);
							this.Crashed();
							return;
						}
						if (Text.Contains("/ERROR]: Unable to launch") || Text.Contains("An exception was thrown, the game will display an error screen and halt."))
						{
							this.WatcherLog("识别为崩溃的 Log：" + Text);
							this.Crashed();
						}
					}
				}
			}

			// Token: 0x06000594 RID: 1428 RVA: 0x00004F6F File Offset: 0x0000316F
			private void WatcherLog(string Text)
			{
				ModLaunch.McLaunchLog("[" + Conversions.ToString(this.m_MockTest) + "] " + Text);
			}

			// Token: 0x06000595 RID: 1429 RVA: 0x000300A4 File Offset: 0x0002E2A4
			private void ProgressUpdate()
			{
				double progress;
				if (!this.m_SerializerTest)
				{
					if (this.m_InterpreterTest != 5)
					{
						progress = (double)Math.Min(this.m_InterpreterTest, 3) / 3.0 * 0.9;
						goto IL_54;
					}
				}
				progress = 0.95;
				this.WatcherLog("Minecraft 加载已完成");
				this.State = ModWatcher.Watcher.MinecraftState.Running;
				IL_54:
				this._RequestTest.Progress = progress;
			}

			// Token: 0x06000596 RID: 1430 RVA: 0x00030114 File Offset: 0x0002E314
			private void TimerWindow()
			{
				try
				{
					if (!this.m_ErrorTest.HasExited)
					{
						if (!this.watcherTest)
						{
							KeyValuePair<IntPtr, string>? keyValuePair = null;
							try
							{
								keyValuePair = this.TryGetMinecraftWindow();
							}
							catch (Win32Exception ex)
							{
								ModBase.Log(ex, "由于反作弊或安全软件拦截，PCL 无法操作游戏窗口", ModBase.LogLevel.Hint, "出现错误");
								this.watcherTest = true;
							}
							if (keyValuePair != null)
							{
								string value = keyValuePair.Value.Value;
								IntPtr MinecraftWindowHandle = keyValuePair.Value.Key;
								if (!value.StartsWithF("FML", false))
								{
									this._IdentifierTest = MinecraftWindowHandle;
									this.WatcherLog(string.Format("Minecraft 窗口已加载：{0}（{1}）", value, MinecraftWindowHandle.ToInt64()));
									this.watcherTest = true;
									if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LaunchArgumentWindowType", null), 4, false))
									{
										ModBase.RunInNewThread(delegate
										{
											try
											{
												Thread.Sleep(2000);
												ModWatcher.Watcher.ShowWindow(this._IdentifierTest, 3U);
												this.WatcherLog(string.Format("已最大化 Minecraft 窗口：{0}", MinecraftWindowHandle.ToInt64()));
											}
											catch (Exception ex3)
											{
												ModBase.Log(ex3, "最大化 Minecraft 窗口时出现错误", ModBase.LogLevel.Debug, "出现错误");
											}
										}, "MinecraftWindowMaximize", ThreadPriority.Normal);
									}
								}
								else if (!this.m_SerializerTest)
								{
									this.WatcherLog(string.Concat(new string[]
									{
										"FML 窗口已加载：",
										value,
										"（",
										Conversions.ToString(MinecraftWindowHandle.ToInt64()),
										"）"
									}));
								}
								this.m_SerializerTest = true;
							}
						}
					}
				}
				catch (Exception ex2)
				{
					ModBase.Log(ex2, "检查 Minecraft 窗口失败", ModBase.LogLevel.Feedback, "出现错误");
				}
			}

			// Token: 0x06000597 RID: 1431 RVA: 0x00004F91 File Offset: 0x00003191
			private KeyValuePair<IntPtr, string>? TryGetMinecraftWindow()
			{
				KeyValuePair<IntPtr, string>? TryGetMinecraftWindow = null;
				ModWatcher.Watcher.EnumWindows(delegate(IntPtr hwnd, int lParam)
				{
					if (TryGetMinecraftWindow == null)
					{
						StringBuilder stringBuilder = new StringBuilder(512);
						ModWatcher.Watcher.GetClassNameA((int)hwnd, stringBuilder, stringBuilder.Capacity);
						string left = stringBuilder.ToString();
						if (Operators.CompareString(left, "GLFW30", false) == 0 || Operators.CompareString(left, "LWJGL", false) == 0 || Operators.CompareString(left, "SunAwtFrame", false) == 0)
						{
							stringBuilder = new StringBuilder(512);
							ModWatcher.Watcher.GetWindowTextA((int)hwnd, stringBuilder, stringBuilder.Capacity);
							string text = stringBuilder.ToString();
							if (text.StartsWithF("FML", false) || (Operators.CompareString(text, "PopupMessageWindow", false) != 0 && !text.StartsWithF("GLFW", false)))
							{
								int processId;
								ModWatcher.Watcher.GetWindowThreadProcessId(hwnd, ref processId);
								try
								{
									if (DateTime.Compare(Process.GetProcessById(processId).StartTime, this.m_ErrorTest.StartTime) < 0)
									{
										return;
									}
								}
								catch (Exception ex)
								{
									ModBase.Log(ex, "枚举 Minecraft 窗口进程失败", ModBase.LogLevel.Debug, "出现错误");
									return;
								}
								TryGetMinecraftWindow = new KeyValuePair<IntPtr, string>?(new KeyValuePair<IntPtr, string>(hwnd, text));
							}
						}
					}
				}, 0);
				TryGetMinecraftWindow = TryGetMinecraftWindow;
				return TryGetMinecraftWindow;
			}

			// Token: 0x06000598 RID: 1432
			[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
			private static extern bool EnumWindows(ModWatcher.Watcher.EnumWindowsSub hWnd, int lParam);

			// Token: 0x06000599 RID: 1433
			[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
			private static extern int GetClassNameA(int hWnd, StringBuilder str, int maxCount);

			// Token: 0x0600059A RID: 1434
			[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
			private static extern int GetWindowTextA(int hWnd, StringBuilder str, int maxCount);

			// Token: 0x0600059B RID: 1435
			[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
			private static extern bool SetWindowTextA(int hWnd, [MarshalAs(UnmanagedType.VBByRefStr)] ref string str);

			// Token: 0x0600059C RID: 1436
			[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
			private static extern bool ShowWindow(IntPtr hWnd, uint cmdWindow);

			// Token: 0x0600059D RID: 1437
			[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
			private static extern int GetWindowThreadProcessId(IntPtr hWnd, ref int lpdwProcessId);

			// Token: 0x0600059E RID: 1438 RVA: 0x000302DC File Offset: 0x0002E4DC
			private void Crashed()
			{
				if (this.State != ModWatcher.Watcher.MinecraftState.Crashed && this.State != ModWatcher.Watcher.MinecraftState.Ended)
				{
					this.State = ModWatcher.Watcher.MinecraftState.Crashed;
					this.WatcherLog("Minecraft 已崩溃，将在 2 秒后开始崩溃分析");
					ModMain.Hint("检测到 Minecraft 出现错误，错误分析已开始……", ModMain.HintType.Info, true);
					ModBase.FeedbackInfo();
					ModBase.RunInNewThread(delegate
					{
						try
						{
							Thread.Sleep(2000);
							this.WatcherLog("崩溃分析开始");
							CrashAnalyzer crashAnalyzer = new CrashAnalyzer(this.m_MockTest);
							crashAnalyzer.Collect(this._ContextTest.ChangeMapper(), Enumerable.ToList<string>(this.indexerTest));
							crashAnalyzer.Prepare();
							crashAnalyzer.Analyze(this._ContextTest);
							crashAnalyzer.Output(false, new List<string>
							{
								this._ContextTest.Path + this._ContextTest.Name + ".json",
								ModBase.Path + "PCL\\Log1.txt",
								ModBase.Path + "PCL\\LatestLaunch.bat"
							});
						}
						catch (Exception ex)
						{
							ModBase.Log(ex, "崩溃分析失败", ModBase.LogLevel.Feedback, "出现错误");
						}
					}, "Crash Analyzer", ThreadPriority.Normal);
				}
			}

			// Token: 0x0600059F RID: 1439 RVA: 0x00030338 File Offset: 0x0002E538
			public void Kill()
			{
				this.State = ModWatcher.Watcher.MinecraftState.Canceled;
				this.WatcherLog("尝试强制结束 Minecraft 进程");
				try
				{
					if (!this.m_ErrorTest.HasExited)
					{
						this.m_ErrorTest.Kill();
					}
					this.WatcherLog("已强制结束 Minecraft 进程");
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "强制结束 Minecraft 进程失败", ModBase.LogLevel.Hint, "出现错误");
				}
			}

			// Token: 0x0400030D RID: 781
			public Process m_ErrorTest;

			// Token: 0x0400030E RID: 782
			public ModMinecraft.McVersion _ContextTest;

			// Token: 0x0400030F RID: 783
			private string _SpecificationTest;

			// Token: 0x04000310 RID: 784
			private int m_MockTest;

			// Token: 0x04000311 RID: 785
			public ModLoader.LoaderTask<Process, int> _RequestTest;

			// Token: 0x04000312 RID: 786
			private ModWatcher.Watcher.MinecraftState m_DicTest;

			// Token: 0x04000313 RID: 787
			public List<string> _HelperTest;

			// Token: 0x04000314 RID: 788
			private readonly object m_IssuerTest;

			// Token: 0x04000315 RID: 789
			public Queue<string> indexerTest;

			// Token: 0x04000316 RID: 790
			private int m_InterpreterTest;

			// Token: 0x04000317 RID: 791
			private bool m_SerializerTest;

			// Token: 0x04000318 RID: 792
			private bool watcherTest;

			// Token: 0x04000319 RID: 793
			private IntPtr _IdentifierTest;

			// Token: 0x020000BC RID: 188
			public enum MinecraftState
			{
				// Token: 0x0400031B RID: 795
				Loading,
				// Token: 0x0400031C RID: 796
				Running,
				// Token: 0x0400031D RID: 797
				Crashed,
				// Token: 0x0400031E RID: 798
				Ended,
				// Token: 0x0400031F RID: 799
				Canceled
			}

			// Token: 0x020000BD RID: 189
			// (Invoke) Token: 0x060005A5 RID: 1445
			private delegate void EnumWindowsSub(IntPtr hwnd, int lParam);
		}
	}
}
