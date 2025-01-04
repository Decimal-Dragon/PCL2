using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PCL
{
	// Token: 0x02000065 RID: 101
	[StandardModule]
	public sealed class ModJava
	{
		// Token: 0x06000260 RID: 608 RVA: 0x00003765 File Offset: 0x00001965
		private static string InitBroadcaster()
		{
			if (ModJava.m_Container == null)
			{
				ModJava.m_Container = (Environment.GetEnvironmentVariable("Path") ?? "");
			}
			return ModJava.m_Container;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000378B File Offset: 0x0000198B
		private static string AddBroadcaster()
		{
			if (ModJava.@params == null)
			{
				ModJava.@params = (Environment.GetEnvironmentVariable("JAVA_HOME") ?? "");
			}
			return ModJava.@params;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0001A7B4 File Offset: 0x000189B4
		public static void JavaListInit()
		{
			ModJava.interceptor = new List<ModJava.JavaEntry>();
			try
			{
				if (Operators.ConditionalCompareObjectLess(ModBase.m_IdentifierRepository.Get("CacheJavaListVersion", null), ModJava.m_Page, false))
				{
					ModBase.Log("[Java] 要求 Java 列表缓存更新", ModBase.LogLevel.Normal, "出现错误");
					ModBase.m_IdentifierRepository.Set("CacheJavaListVersion", ModJava.m_Page, false, null);
				}
				else
				{
					try
					{
						foreach (object obj in ((IEnumerable)ModBase.GetJson(Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchArgumentJavaAll", null)))))
						{
							object objectValue = RuntimeHelpers.GetObjectValue(obj);
							ModJava.interceptor.Add(ModJava.JavaEntry.FromJson((JObject)objectValue));
						}
					}
					finally
					{
						IEnumerator enumerator;
						if (enumerator is IDisposable)
						{
							(enumerator as IDisposable).Dispose();
						}
					}
				}
				if (!Enumerable.Any<ModJava.JavaEntry>(ModJava.interceptor))
				{
					ModBase.Log("[Java] 初始化未找到可用的 Java，将自动触发搜索", ModBase.LogLevel.Developer, "出现错误");
					ModJava._Process.Start(0, false);
				}
				else
				{
					ModBase.Log("[Java] 缓存中有 " + Conversions.ToString(ModJava.interceptor.Count) + " 个可用的 Java：", ModBase.LogLevel.Normal, "出现错误");
					ModJava.interceptor.ForEach((ModJava._Closure$__.$I10-0 == null) ? (ModJava._Closure$__.$I10-0 = delegate(ModJava.JavaEntry j)
					{
						ModBase.Log(string.Format("[Java]  - {0}", j), ModBase.LogLevel.Normal, "出现错误");
					}) : ModJava._Closure$__.$I10-0);
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "初始化 Java 列表失败", ModBase.LogLevel.Feedback, "出现错误");
				ModBase.m_IdentifierRepository.Set("LaunchArgumentJavaAll", "[]", false, null);
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0001A974 File Offset: 0x00018B74
		public static ModJava.JavaEntry JavaSelect(string CancelException, Version MinVersion = null, Version MaxVersion = null, ModMinecraft.McVersion RelatedVersion = null)
		{
			ModJava.JavaEntry result;
			try
			{
				ModJava._Closure$__12-0 CS$<>8__locals1 = new ModJava._Closure$__12-0(CS$<>8__locals1);
				List<ModJava.JavaEntry> list = new List<ModJava.JavaEntry>();
				Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
				if (Enumerable.Count<string>(ModMinecraft.m_ProxyTests.Split("\\")) > 3 && !ModMinecraft.m_ProxyTests.Contains("AppData\\Roaming"))
				{
					ModJava.JavaSearchFolder(ModBase.GetPathFromFullPath(ModMinecraft.m_ProxyTests), ref dictionary, false, true);
				}
				ModJava.JavaSearchFolder(ModMinecraft.m_ProxyTests, ref dictionary, false, true);
				dictionary = Enumerable.ToDictionary<KeyValuePair<string, bool>, string, bool>(Enumerable.Where<KeyValuePair<string, bool>>(dictionary, (ModJava._Closure$__.$I12-0 == null) ? (ModJava._Closure$__.$I12-0 = ((KeyValuePair<string, bool> j) => !j.Key.Contains(".minecraft\\runtime"))) : ModJava._Closure$__.$I12-0), (ModJava._Closure$__.$I12-1 == null) ? (ModJava._Closure$__.$I12-1 = ((KeyValuePair<string, bool> j) => j.Key)) : ModJava._Closure$__.$I12-1, (ModJava._Closure$__.$I12-2 == null) ? (ModJava._Closure$__.$I12-2 = ((KeyValuePair<string, bool> j) => j.Value)) : ModJava._Closure$__.$I12-2);
				if (RelatedVersion != null)
				{
					ModJava.JavaSearchFolder(RelatedVersion.Path, ref dictionary, false, true);
				}
				List<ModJava.JavaEntry> list2 = new List<ModJava.JavaEntry>();
				try
				{
					foreach (KeyValuePair<string, bool> keyValuePair in dictionary)
					{
						list2.Add(new ModJava.JavaEntry(keyValuePair.Key, keyValuePair.Value));
					}
				}
				finally
				{
					Dictionary<string, bool>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				if (Enumerable.Any<ModJava.JavaEntry>(list2))
				{
					list2 = ModJava.JavaCheckList(list2);
					ModBase.Log("[Java] 检查后找到 " + Conversions.ToString(list2.Count) + " 个特定路径下的 Java：", ModBase.LogLevel.Normal, "出现错误");
					try
					{
						foreach (ModJava.JavaEntry arg in list2)
						{
							ModBase.Log(string.Format("[Java]  - {0}", arg), ModBase.LogLevel.Normal, "出现错误");
						}
					}
					finally
					{
						List<ModJava.JavaEntry>.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
				}
				CS$<>8__locals1.$VB$Local_UserJava = null;
				string text = "";
				if (RelatedVersion != null)
				{
					text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionArgumentJavaSelect", RelatedVersion));
					if (text.StartsWithF("{", false))
					{
						try
						{
							CS$<>8__locals1.$VB$Local_UserJava = ModJava.JavaEntry.FromJson((JObject)ModBase.GetJson(text));
							CS$<>8__locals1.$VB$Local_UserJava.Check();
						}
						catch (ThreadInterruptedException ex)
						{
							throw;
						}
						catch (Exception ex2)
						{
							CS$<>8__locals1.$VB$Local_UserJava = null;
							ModBase.m_IdentifierRepository.Reset("VersionArgumentJavaSelect", false, RelatedVersion);
							ModBase.Log(ex2, "版本独立设置中指定的 Java 已无法使用，此设置已重置", ModBase.LogLevel.Hint, "出现错误");
						}
					}
				}
				if (CS$<>8__locals1.$VB$Local_UserJava == null && Operators.CompareString(text, "", false) != 0 && Operators.ConditionalCompareObjectNotEqual(ModBase.m_IdentifierRepository.Get("LaunchArgumentJavaSelect", null), "", false))
				{
					try
					{
						CS$<>8__locals1.$VB$Local_UserJava = ModJava.JavaEntry.FromJson((JObject)ModBase.GetJson(Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchArgumentJavaSelect", null))));
						CS$<>8__locals1.$VB$Local_UserJava.Check();
					}
					catch (ThreadInterruptedException ex3)
					{
						throw;
					}
					catch (Exception ex4)
					{
						CS$<>8__locals1.$VB$Local_UserJava = null;
						ModBase.m_IdentifierRepository.Reset("LaunchArgumentJavaSelect", false, null);
						ModBase.Log(ex4, "全局设置中指定的 Java 已无法使用，此设置已重置", ModBase.LogLevel.Hint, "出现错误");
					}
				}
				if (CS$<>8__locals1.$VB$Local_UserJava != null)
				{
					ModBase.Log(string.Format("[Java] 用户指定的 Java：{0}", CS$<>8__locals1.$VB$Local_UserJava), ModBase.LogLevel.Normal, "出现错误");
					list2.Add(CS$<>8__locals1.$VB$Local_UserJava);
				}
				ModJava.JavaEntry javaEntry2;
				for (;;)
				{
					if (ModJava._Process.State != ModBase.LoadState.Finished)
					{
						goto IL_9AA;
					}
					IL_33A:
					ModBase.LoadState state = ModJava._Process.State;
					if (state == ModBase.LoadState.Failed)
					{
						goto IL_A15;
					}
					if (state == ModBase.LoadState.Aborted)
					{
						goto IL_A0A;
					}
					List<ModJava.JavaEntry> list3 = new List<ModJava.JavaEntry>();
					list3.AddRange(list2);
					list3.AddRange(ModJava.interceptor);
					try
					{
						foreach (ModJava.JavaEntry javaEntry in list3)
						{
							if ((MinVersion == null || !(javaEntry.policyRepository < MinVersion)) && (MaxVersion == null || !(javaEntry.policyRepository > MaxVersion)) && (!javaEntry.producerRepository || !ModBase.m_StubRepository))
							{
								list.Add(javaEntry);
							}
						}
					}
					finally
					{
						List<ModJava.JavaEntry>.Enumerator enumerator3;
						((IDisposable)enumerator3).Dispose();
					}
					if (!Enumerable.Any<ModJava.JavaEntry>(list) && ModJava._Process.State == ModBase.LoadState.Waiting)
					{
						ModBase.Log("[Java] 未找到满足条件的 Java，尝试进行搜索", ModBase.LogLevel.Normal, "出现错误");
						ModJava._Process.Start(null, true);
						continue;
					}
					if (CS$<>8__locals1.$VB$Local_UserJava == null)
					{
						goto IL_853;
					}
					if (Enumerable.Any<ModJava.JavaEntry>(list, (ModJava.JavaEntry j) => Operators.CompareString(j._UtilsRepository, CS$<>8__locals1.$VB$Local_UserJava._UtilsRepository, false) == 0))
					{
						ModBase.Log("[Java] 使用用户指定的 Java：" + CS$<>8__locals1.$VB$Local_UserJava._UtilsRepository, ModBase.LogLevel.Normal, "出现错误");
						list = new List<ModJava.JavaEntry>
						{
							CS$<>8__locals1.$VB$Local_UserJava
						};
					}
					else
					{
						ModBase.Log("[Java] 发现用户指定的不兼容 Java：" + CS$<>8__locals1.$VB$Local_UserJava.ToString(), ModBase.LogLevel.Normal, "出现错误");
						ModBase.Log("[Java] 目前实际可用的 Java 列表：", ModBase.LogLevel.Normal, "出现错误");
						try
						{
							foreach (ModJava.JavaEntry arg2 in list)
							{
								ModBase.Log(string.Format("[Java]  - {0}", arg2), ModBase.LogLevel.Normal, "出现错误");
							}
						}
						finally
						{
							List<ModJava.JavaEntry>.Enumerator enumerator4;
							((IDisposable)enumerator4).Dispose();
						}
						string text2 = "";
						bool flag = false;
						if ((MinVersion == null || MinVersion.Minor == 0) && MaxVersion != null && MaxVersion.Minor < 999)
						{
							flag = (MaxVersion.MinorRevision < 999);
							text2 = "最高兼容到 Java " + Conversions.ToString(MaxVersion.Minor) + (flag ? ("." + Conversions.ToString((int)MaxVersion.MajorRevision) + "." + Conversions.ToString((int)MaxVersion.MinorRevision)) : "");
						}
						else if (MinVersion != null && MinVersion.Minor > 0 && (MaxVersion == null || MaxVersion.Minor >= 999))
						{
							flag = (MinVersion.MinorRevision > 0 || MinVersion.MajorRevision > 0);
							text2 = "至少需要 Java " + Conversions.ToString(MinVersion.Minor) + (flag ? ("." + Conversions.ToString((int)MinVersion.MajorRevision) + "." + Conversions.ToString((int)MinVersion.MinorRevision)) : "");
						}
						else if (MinVersion != null && MinVersion.Minor > 0 && MaxVersion != null && MaxVersion.Minor < 999)
						{
							flag = (MinVersion.MinorRevision > 0 || MinVersion.MajorRevision > 0 || MaxVersion.MinorRevision < 999);
							string text3 = Conversions.ToString(MinVersion.Minor) + (flag ? ("." + Conversions.ToString((int)MinVersion.MajorRevision) + "." + Conversions.ToString((int)MinVersion.MinorRevision)) : "");
							string text4 = Conversions.ToString(MaxVersion.Minor) + (flag ? ("." + Conversions.ToString((int)MaxVersion.MajorRevision) + "." + Conversions.ToString((int)MaxVersion.MinorRevision)) : "");
							text2 = "需要 Java " + ((Operators.CompareString(text3, text4, false) == 0) ? text3 : (text3 + " ~ " + text4));
						}
						string text5 = Conversions.ToString(CS$<>8__locals1.$VB$Local_UserJava.PostTests()) + (flag ? ("." + Conversions.ToString((int)CS$<>8__locals1.$VB$Local_UserJava.policyRepository.MajorRevision) + "." + Conversions.ToString((int)CS$<>8__locals1.$VB$Local_UserJava.policyRepository.MinorRevision)) : "");
						if (Conversions.ToBoolean(Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LaunchAdvanceJava", null)) || (RelatedVersion != null && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("VersionAdvanceJava", RelatedVersion)))))
						{
							ModBase.Log(string.Concat(new string[]
							{
								"[Java] 设置中指定了使用 Java ",
								text5,
								"，但当前版本",
								text2,
								"，这可能会导致游戏崩溃！"
							}), ModBase.LogLevel.Debug, "出现错误");
							list = new List<ModJava.JavaEntry>
							{
								CS$<>8__locals1.$VB$Local_UserJava
							};
							goto IL_853;
						}
						switch (ModMain.MyMsgBox(string.Concat(new string[]
						{
							"你在设置中手动指定了使用 Java ",
							text5,
							"，但当前",
							text2,
							"。\r\n如果强制使用该 Java，可能导致游戏崩溃。\r\n你也可以将 游戏 Java 设置修改为 自动选择合适的 Java。\r\n\r\n - 指定的 Java：",
							CS$<>8__locals1.$VB$Local_UserJava.ToString()
						}), "Java 兼容性警告", "让 PCL 自动选择", "强制使用该 Java", "取消", false, true, false, null, null, null))
						{
						case 1:
							goto IL_853;
						case 2:
							ModBase.Log("[Java] 已强制使用用户指定的不兼容 Java", ModBase.LogLevel.Normal, "出现错误");
							list = new List<ModJava.JavaEntry>
							{
								CS$<>8__locals1.$VB$Local_UserJava
							};
							goto IL_853;
						case 3:
							goto IL_9DE;
						default:
							goto IL_853;
						}
					}
					IL_8D5:
					list = list.Sort(new ModBase.CompareThreadStart<ModJava.JavaEntry>(ModJava.JavaSorter));
					ModBase.Log("[Java] 排序后的 Java 优先顺序：", ModBase.LogLevel.Normal, "出现错误");
					try
					{
						foreach (ModJava.JavaEntry arg3 in list)
						{
							ModBase.Log(string.Format("[Java]  - {0}", arg3), ModBase.LogLevel.Normal, "出现错误");
						}
					}
					finally
					{
						List<ModJava.JavaEntry>.Enumerator enumerator5;
						((IDisposable)enumerator5).Dispose();
					}
					javaEntry2 = Enumerable.First<ModJava.JavaEntry>(list);
					try
					{
						javaEntry2.Check();
						goto IL_9E5;
					}
					catch (ThreadInterruptedException ex5)
					{
						throw;
					}
					catch (Exception ex6)
					{
						if (ex6.InnerException != null && ex6.InnerException is ThreadInterruptedException)
						{
							throw ex6.InnerException;
						}
						ModBase.Log(ex6, "最终选定的 Java 已无法使用，尝试进行搜索", ModBase.LogLevel.Debug, "出现错误");
						list = new List<ModJava.JavaEntry>();
						ModJava._Process.Start(null, true);
						continue;
					}
					goto IL_9AA;
					IL_853:
					if (Enumerable.Any<ModJava.JavaEntry>(list))
					{
						try
						{
							foreach (ModJava.JavaEntry javaEntry3 in list)
							{
								if (!javaEntry3._UtilsRepository.Contains(".minecraft\\cache\\java") && list2.Contains(javaEntry3))
								{
									list = new List<ModJava.JavaEntry>
									{
										javaEntry3
									};
									ModBase.Log("[Java] 优先使用特定路径下的 Java：" + javaEntry3.ToString(), ModBase.LogLevel.Normal, "出现错误");
									break;
								}
							}
						}
						finally
						{
							List<ModJava.JavaEntry>.Enumerator enumerator6;
							((IDisposable)enumerator6).Dispose();
						}
						goto IL_8D5;
					}
					goto IL_A20;
					IL_9AA:
					if (ModJava._Process.State != ModBase.LoadState.Waiting)
					{
						ModJava._Process.WaitForExit(null, null, false);
						goto IL_33A;
					}
					goto IL_33A;
				}
				IL_9DE:
				throw new Exception(CancelException);
				IL_9E5:
				ModBase.Log("[Java] 最终选定的 Java：" + Enumerable.First<ModJava.JavaEntry>(list).ToString(), ModBase.LogLevel.Normal, "出现错误");
				return javaEntry2;
				IL_A0A:
				throw new ThreadInterruptedException("Java 搜索加载器已中断");
				IL_A15:
				throw ModJava._Process.Error;
				IL_A20:
				result = null;
			}
			catch (ThreadInterruptedException ex7)
			{
				ModBase.Log(ex7, "查找符合条件的 Java 时出现加载器中断", ModBase.LogLevel.Debug, "出现错误");
				result = null;
			}
			catch (Exception ex8)
			{
				if (Operators.CompareString(ex8.Message, "$$", false) == 0)
				{
					throw ex8;
				}
				ModBase.Log(ex8, "查找符合条件的 Java 失败", ModBase.LogLevel.Feedback, "出现错误");
				result = null;
			}
			return result;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0001B558 File Offset: 0x00019758
		public static bool smethod_0(ModMinecraft.McVersion RelatedVersion = null)
		{
			bool result;
			try
			{
				string text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchArgumentJavaSelect", null));
				if (RelatedVersion != null)
				{
					string text2 = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionArgumentJavaSelect", RelatedVersion));
					if (Operators.CompareString(text2, "使用全局设置", false) != 0)
					{
						text = text2;
					}
				}
				if (Operators.CompareString(text, "", false) != 0)
				{
					ModJava.JavaEntry javaEntry = null;
					try
					{
						javaEntry = ModJava.JavaEntry.FromJson((JObject)ModBase.GetJson(text));
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "版本指定的 Java 信息已损坏，已重置版本设置中指定的 Java", ModBase.LogLevel.Debug, "出现错误");
						ModBase.m_IdentifierRepository.Set("VersionArgumentJavaSelect", "使用全局设置", false, RelatedVersion);
						goto IL_EA;
					}
					try
					{
						List<ModJava.JavaEntry>.Enumerator enumerator = ModJava.interceptor.GetEnumerator();
						while (enumerator.MoveNext())
						{
							if (Operators.CompareString(enumerator.Current._UtilsRepository, javaEntry._UtilsRepository, false) == 0)
							{
								return javaEntry.producerRepository;
							}
						}
					}
					finally
					{
						List<ModJava.JavaEntry>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
				IL_EA:
				try
				{
					List<ModJava.JavaEntry>.Enumerator enumerator2 = ModJava.interceptor.GetEnumerator();
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.producerRepository)
						{
							return true;
						}
					}
				}
				finally
				{
					List<ModJava.JavaEntry>.Enumerator enumerator2;
					((IDisposable)enumerator2).Dispose();
				}
				result = false;
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, "检查 Java 类别时出错", ModBase.LogLevel.Feedback, "出现错误");
				ModBase.m_IdentifierRepository.Set("LaunchArgumentJavaSelect", "", false, null);
				result = true;
			}
			return result;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0001B72C File Offset: 0x0001992C
		public static bool JavaSorter(ModJava.JavaEntry Left, ModJava.JavaEntry Right)
		{
			string prefix = "";
			string fullName = (new DirectoryInfo(ModBase.Path).Parent ?? new DirectoryInfo(ModBase.Path)).FullName;
			if (Operators.CompareString(ModMinecraft.m_ProxyTests, "", false) != 0)
			{
				prefix = (new DirectoryInfo(ModMinecraft.m_ProxyTests).Parent ?? new DirectoryInfo(ModMinecraft.m_ProxyTests)).FullName;
			}
			bool result;
			if (Left._UtilsRepository.StartsWithF(fullName, false) && !Right._UtilsRepository.StartsWithF(fullName, false))
			{
				result = true;
			}
			else if (!Left._UtilsRepository.StartsWithF(fullName, false) && Right._UtilsRepository.StartsWithF(fullName, false))
			{
				result = false;
			}
			else
			{
				if (Operators.CompareString(ModMinecraft.m_ProxyTests, "", false) != 0)
				{
					if (Left._UtilsRepository.StartsWithF(prefix, false) && !Right._UtilsRepository.StartsWithF(prefix, false))
					{
						return true;
					}
					if (!Left._UtilsRepository.StartsWithF(prefix, false) && Right._UtilsRepository.StartsWithF(prefix, false))
					{
						return false;
					}
				}
				if (Left.producerRepository && !Right.producerRepository)
				{
					result = true;
				}
				else if (!Left.producerRepository && Right.producerRepository)
				{
					result = false;
				}
				else if (Left.orderRepository && !Right.orderRepository)
				{
					result = true;
				}
				else if (!Left.orderRepository && Right.orderRepository)
				{
					result = false;
				}
				else if (Left.PostTests() != Right.PostTests())
				{
					int[] array = new int[]
					{
						0,
						1,
						2,
						3,
						4,
						5,
						6,
						14,
						30,
						10,
						12,
						15,
						13,
						9,
						8,
						7,
						11,
						31,
						29,
						16,
						17,
						28,
						27,
						26,
						25,
						24,
						23,
						22,
						21,
						20,
						19,
						18
					};
					result = (Enumerable.ElementAtOrDefault<int>(array, Left.PostTests()) >= Enumerable.ElementAtOrDefault<int>(array, Right.PostTests()));
				}
				else
				{
					result = checked(Math.Abs(Left.policyRepository.Revision - 51) <= Math.Abs(Right.policyRepository.Revision - 51));
				}
			}
			return result;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0001B904 File Offset: 0x00019B04
		private static void JavaSearchLoaderSub(ModLoader.LoaderTask<int, int> Loader)
		{
			if (ModMain._PolicyIterator != null)
			{
				ModBase.RunInUiWait((ModJava._Closure$__.$I16-0 == null) ? (ModJava._Closure$__.$I16-0 = delegate()
				{
					ModMain._PolicyIterator.ComboArgumentJava.Items.Clear();
					ModMain._PolicyIterator.ComboArgumentJava.Items.Add(new ComboBoxItem
					{
						Content = "加载中……",
						IsSelected = true
					});
				}) : ModJava._Closure$__.$I16-0);
			}
			if (ModMain.composerRepository != null)
			{
				ModBase.RunInUiWait((ModJava._Closure$__.$I16-1 == null) ? (ModJava._Closure$__.$I16-1 = delegate()
				{
					ModMain.composerRepository.ComboArgumentJava.Items.Clear();
					ModMain.composerRepository.ComboArgumentJava.Items.Add(new ComboBoxItem
					{
						Content = "加载中……",
						IsSelected = true
					});
				}) : ModJava._Closure$__.$I16-1);
			}
			try
			{
				Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
				foreach (string text in (ModJava.InitBroadcaster() + ";" + ModJava.AddBroadcaster()).Replace("\\\\", "\\").Replace("/", "\\").Split(";"))
				{
					text = text.Trim(" \"".ToCharArray());
					if (Operators.CompareString(text, "", false) != 0)
					{
						if (!text.EndsWithF("\\", false))
						{
							text += "\\";
						}
						if (File.Exists(text + "javaw.exe"))
						{
							dictionary[text] = false;
						}
					}
				}
				foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
				{
					if (driveInfo.DriveType != DriveType.Network)
					{
						ModJava.JavaSearchFolder(driveInfo.Name, ref dictionary, false, false);
					}
				}
				ModJava.JavaSearchFolder(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\", ref dictionary, false, false);
				ModJava.JavaSearchFolder(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\", ref dictionary, false, false);
				ModJava.JavaSearchFolder(ModBase.Path, ref dictionary, false, true);
				if (!string.IsNullOrWhiteSpace(ModMinecraft.m_ProxyTests) && Operators.CompareString(ModBase.Path, ModMinecraft.m_ProxyTests, false) != 0)
				{
					ModJava.JavaSearchFolder(ModMinecraft.m_ProxyTests, ref dictionary, false, true);
				}
				Dictionary<string, bool> dictionary2 = new Dictionary<string, bool>();
				try
				{
					Dictionary<string, bool>.Enumerator enumerator = dictionary.GetEnumerator();
					IL_2AF:
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, bool> keyValuePair = enumerator.Current;
						string text2 = keyValuePair.Key.Replace("\\\\", "\\").Replace("/", "\\");
						FileSystemInfo fileSystemInfo = new FileInfo(text2 + "javaw.exe");
						while (!fileSystemInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
						{
							fileSystemInfo = ((fileSystemInfo is FileInfo) ? ((FileInfo)fileSystemInfo).Directory : ((DirectoryInfo)fileSystemInfo).Parent);
							if (fileSystemInfo == null)
							{
								ModBase.Log("[Java] 位于 " + text2 + " 的 Java 不含符号链接", ModBase.LogLevel.Normal, "出现错误");
								dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
								goto IL_2AF;
							}
						}
						ModBase.Log("[Java] 位于 " + text2 + " 的 Java 包含符号链接", ModBase.LogLevel.Normal, "出现错误");
					}
				}
				finally
				{
					Dictionary<string, bool>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				if (Enumerable.Any<KeyValuePair<string, bool>>(dictionary2))
				{
					dictionary = dictionary2;
				}
				Dictionary<string, bool> dictionary3 = new Dictionary<string, bool>();
				try
				{
					foreach (KeyValuePair<string, bool> keyValuePair2 in dictionary)
					{
						if (!keyValuePair2.Key.Contains("java8path_target_") && !keyValuePair2.Key.Contains("javapath_target_") && !keyValuePair2.Key.Contains("javatmp"))
						{
							ModBase.Log("[Java] 位于 " + keyValuePair2.Key + " 的 Java 不含特殊引用", ModBase.LogLevel.Normal, "出现错误");
							dictionary3.Add(keyValuePair2.Key, keyValuePair2.Value);
						}
						else
						{
							ModBase.Log("[Java] 位于 " + keyValuePair2.Key + " 的 Java 包含特殊引用", ModBase.LogLevel.Normal, "出现错误");
						}
					}
				}
				finally
				{
					Dictionary<string, bool>.Enumerator enumerator2;
					((IDisposable)enumerator2).Dispose();
				}
				if (Enumerable.Any<KeyValuePair<string, bool>>(dictionary3))
				{
					dictionary = dictionary3;
				}
				string data = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchArgumentJavaAll", null));
				try
				{
					try
					{
						foreach (object obj in ((IEnumerable)ModBase.GetJson(data)))
						{
							ModJava.JavaEntry javaEntry = ModJava.JavaEntry.FromJson((JObject)RuntimeHelpers.GetObjectValue(obj));
							if (javaEntry._ClassRepository)
							{
								dictionary[javaEntry._UtilsRepository] = true;
							}
						}
					}
					finally
					{
						IEnumerator enumerator3;
						if (enumerator3 is IDisposable)
						{
							(enumerator3 as IDisposable).Dispose();
						}
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "Java 列表已损坏", ModBase.LogLevel.Feedback, "出现错误");
					ModBase.m_IdentifierRepository.Set("LaunchArgumentJavaAll", "[]", false, null);
				}
				List<ModJava.JavaEntry> list = new List<ModJava.JavaEntry>();
				try
				{
					foreach (KeyValuePair<string, bool> keyValuePair3 in dictionary.Distinct((ModJava._Closure$__.$I16-2 == null) ? (ModJava._Closure$__.$I16-2 = ((KeyValuePair<string, bool> a, KeyValuePair<string, bool> b) => Operators.CompareString(a.Key.ToLower(), b.Key.ToLower(), false) == 0)) : ModJava._Closure$__.$I16-2))
					{
						list.Add(new ModJava.JavaEntry(keyValuePair3.Key, keyValuePair3.Value));
					}
				}
				finally
				{
					List<KeyValuePair<string, bool>>.Enumerator enumerator4;
					((IDisposable)enumerator4).Dispose();
				}
				list = ModJava.JavaCheckList(list).Sort(new ModBase.CompareThreadStart<ModJava.JavaEntry>(ModJava.JavaSorter));
				JArray jarray = new JArray();
				try
				{
					foreach (ModJava.JavaEntry javaEntry2 in list)
					{
						jarray.Add(javaEntry2.ToJson());
					}
				}
				finally
				{
					List<ModJava.JavaEntry>.Enumerator enumerator5;
					((IDisposable)enumerator5).Dispose();
				}
				ModBase.m_IdentifierRepository.Set("LaunchArgumentJavaAll", jarray.ToString(0, new JsonConverter[0]), false, null);
				ModJava.interceptor = list;
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, "搜索 Java 时出错", ModBase.LogLevel.Feedback, "出现错误");
				ModJava.interceptor = new List<ModJava.JavaEntry>();
			}
			ModBase.Log("[Java] Java 搜索完成，发现 " + Conversions.ToString(ModJava.interceptor.Count) + " 个 Java", ModBase.LogLevel.Normal, "出现错误");
			if (ModMain._PolicyIterator != null)
			{
				ModBase.RunInUi((ModJava._Closure$__.$I16-3 == null) ? (ModJava._Closure$__.$I16-3 = delegate()
				{
					ModMain._PolicyIterator.RefreshJavaComboBox();
				}) : ModJava._Closure$__.$I16-3, false);
			}
			if (ModMain.composerRepository != null)
			{
				ModBase.RunInUi((ModJava._Closure$__.$I16-4 == null) ? (ModJava._Closure$__.$I16-4 = delegate()
				{
					ModMain.composerRepository.RefreshJavaComboBox();
				}) : ModJava._Closure$__.$I16-4, false);
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0001BFC8 File Offset: 0x0001A1C8
		private static List<ModJava.JavaEntry> JavaCheckList(List<ModJava.JavaEntry> JavaEntries)
		{
			ModJava._Closure$__17-1 CS$<>8__locals1 = new ModJava._Closure$__17-1(CS$<>8__locals1);
			ModBase.Log("[Java] 开始确认列表 Java 状态，共 " + Conversions.ToString(JavaEntries.Count) + " 项", ModBase.LogLevel.Normal, "出现错误");
			CS$<>8__locals1.$VB$Local_JavaCheckList = new List<ModJava.JavaEntry>();
			CS$<>8__locals1.$VB$Local_ListLock = RuntimeHelpers.GetObjectValue(new object());
			List<Thread> list = new List<Thread>();
			try
			{
				List<ModJava.JavaEntry>.Enumerator enumerator = JavaEntries.GetEnumerator();
				while (enumerator.MoveNext())
				{
					ModJava._Closure$__17-0 CS$<>8__locals2 = new ModJava._Closure$__17-0(CS$<>8__locals2);
					CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2 = CS$<>8__locals1;
					CS$<>8__locals2.$VB$Local_Entry = enumerator.Current;
					Thread thread = new Thread(delegate()
					{
						try
						{
							CS$<>8__locals2.$VB$Local_Entry.Check();
							if (ModBase._TokenRepository)
							{
								ModBase.Log("[Java]  - " + CS$<>8__locals2.$VB$Local_Entry.ToString(), ModBase.LogLevel.Normal, "出现错误");
							}
							object $VB$Local_ListLock = CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_ListLock;
							ObjectFlowControl.CheckForSyncLockOnValueType($VB$Local_ListLock);
							lock ($VB$Local_ListLock)
							{
								CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_JavaCheckList.Add(CS$<>8__locals2.$VB$Local_Entry);
							}
						}
						catch (ThreadInterruptedException ex)
						{
						}
						catch (Exception ex2)
						{
							if (CS$<>8__locals2.$VB$Local_Entry._ClassRepository)
							{
								ModBase.Log(ex2, "位于 " + CS$<>8__locals2.$VB$Local_Entry._UtilsRepository + " 的 Java 存在异常，将被自动移除", ModBase.LogLevel.Hint, "出现错误");
							}
							else
							{
								ModBase.Log(ex2, "位于 " + CS$<>8__locals2.$VB$Local_Entry._UtilsRepository + " 的 Java 存在异常", ModBase.LogLevel.Debug, "出现错误");
							}
						}
					});
					list.Add(thread);
					thread.Start();
				}
				goto IL_DF;
			}
			finally
			{
				List<ModJava.JavaEntry>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			try
			{
				IL_AC:
				List<Thread>.Enumerator enumerator2 = list.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.IsAlive)
					{
						goto IL_DF;
					}
				}
				goto IL_E8;
			}
			finally
			{
				List<Thread>.Enumerator enumerator2;
				((IDisposable)enumerator2).Dispose();
			}
			goto IL_DF;
			IL_E8:
			return CS$<>8__locals1.$VB$Local_JavaCheckList;
			IL_DF:
			Thread.Sleep(10);
			goto IL_AC;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0001C0E0 File Offset: 0x0001A2E0
		private static void JavaSearchFolder(string OriginalPath, ref Dictionary<string, bool> Results, bool Source, bool IsFullSearch = false)
		{
			try
			{
				ModBase.Log("[Java] 开始" + (IsFullSearch ? "完全" : "部分") + "遍历查找：" + OriginalPath, ModBase.LogLevel.Normal, "出现错误");
				ModJava.JavaSearchFolder(new DirectoryInfo(OriginalPath), ref Results, Source, IsFullSearch);
			}
			catch (UnauthorizedAccessException ex)
			{
				ModBase.Log("[Java] 遍历查找 Java 时遭遇无权限的文件夹：" + OriginalPath, ModBase.LogLevel.Normal, "出现错误");
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, "遍历查找 Java 时出错（" + OriginalPath + "）", ModBase.LogLevel.Debug, "出现错误");
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0001C194 File Offset: 0x0001A394
		private static void JavaSearchFolder(DirectoryInfo OriginalPath, ref Dictionary<string, bool> Results, bool Source, bool IsFullSearch = false)
		{
			try
			{
				if (OriginalPath.Exists)
				{
					string text = OriginalPath.FullName.Replace("\\\\", "\\");
					if (!text.EndsWithF("\\", false))
					{
						text += "\\";
					}
					if (File.Exists(text + "javaw.exe"))
					{
						Results[text] = Source;
					}
					string[] array = new string[]
					{
						"java",
						"jdk",
						"env",
						"环境",
						"run",
						"软件",
						"jre",
						"mc",
						"dragon",
						"soft",
						"cache",
						"temp",
						"corretto",
						"roaming",
						"users",
						"craft",
						"program",
						"世界",
						"net",
						"游戏",
						"oracle",
						"game",
						"file",
						"data",
						"jvm",
						"服务",
						"server",
						"客户",
						"client",
						"整合",
						"应用",
						"运行",
						"前置",
						"mojang",
						"官启",
						"新建文件夹",
						"eclipse",
						"microsoft",
						"hotspot",
						"runtime",
						"x86",
						"x64",
						"forge",
						"原版",
						"optifine",
						"官方",
						"启动",
						"hmcl",
						"mod",
						"高清",
						"download",
						"launch",
						"程序",
						"path",
						"version",
						"baka",
						"pcl",
						"zulu",
						"local",
						"packages",
						"4297127d64ec6",
						"国服",
						"网易",
						"ext",
						"netease",
						"1.",
						"启动"
					};
					try
					{
						foreach (DirectoryInfo directoryInfo in OriginalPath.EnumerateDirectories())
						{
							ModJava._Closure$__19-0 CS$<>8__locals1 = new ModJava._Closure$__19-0(CS$<>8__locals1);
							if (!directoryInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
							{
								CS$<>8__locals1.$VB$Local_SearchEntry = ModBase.GetFolderNameFromPath(directoryInfo.Name).ToLower();
								if (IsFullSearch || Operators.CompareString(directoryInfo.Parent.Name.ToLower(), "users", false) == 0 || ModBase.Val(CS$<>8__locals1.$VB$Local_SearchEntry) > 0.0 || Enumerable.Any<string>(array, (string w) => CS$<>8__locals1.$VB$Local_SearchEntry.Contains(w)) || Operators.CompareString(CS$<>8__locals1.$VB$Local_SearchEntry, "bin", false) == 0)
								{
									ModJava.JavaSearchFolder(directoryInfo, ref Results, Source, false);
								}
							}
						}
					}
					finally
					{
						IEnumerator<DirectoryInfo> enumerator;
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
				}
			}
			catch (UnauthorizedAccessException ex)
			{
				ModBase.Log("[Java] 遍历查找 Java 时遭遇无权限的文件夹：" + OriginalPath.FullName, ModBase.LogLevel.Normal, "出现错误");
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, "遍历查找 Java 时出错（" + OriginalPath.FullName + "）", ModBase.LogLevel.Debug, "出现错误");
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0001C5D4 File Offset: 0x0001A7D4
		public static bool JavaDownloadConfirm(string VersionDescription, bool ForcedManualDownload = false)
		{
			bool result;
			if (ForcedManualDownload)
			{
				ModMain.MyMsgBox(string.Format("PCL 未找到 {0}。", VersionDescription) + "\r\n" + string.Format("请自行搜索并安装 {0}，安装后在 设置 → 启动选项 → 游戏 Java 中重新搜索或导入。", VersionDescription), "未找到 Java", "确定", "", "", false, true, false, null, null, null);
				result = false;
			}
			else
			{
				result = (ModMain.MyMsgBox(string.Format("PCL 未找到 {0}，是否需要 PCL 自动下载？", VersionDescription) + "\r\n" + string.Format("如果你已经安装了 {0}，请在 设置 → 启动选项 → 游戏 Java 中手动导入。", VersionDescription), "未找到 Java", "自动下载", "取消", "", false, true, false, null, null, null) == 1);
			}
			return result;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0001C66C File Offset: 0x0001A86C
		public static ModLoader.LoaderCombo<int> JavaFixLoaders(int Version)
		{
			ModNet.LoaderDownload loaderDownload = new ModNet.LoaderDownload("下载 Java 文件", new List<ModNet.NetFile>())
			{
				ProgressWeight = 10.0
			};
			ModLoader.LoaderCombo<int> result = new ModLoader.LoaderCombo<int>(string.Format("下载 Java {0}", Version), new ModLoader.LoaderBase[]
			{
				new ModLoader.LoaderTask<int, List<ModNet.NetFile>>("获取 Java 下载信息", new Action<ModLoader.LoaderTask<int, List<ModNet.NetFile>>>(ModJava.JavaFileList), null, ThreadPriority.Normal)
				{
					ProgressWeight = 2.0
				},
				loaderDownload,
				ModJava._Process
			});
			loaderDownload.OnStateChangedThread += ((ModJava._Closure$__.$I21-0 == null) ? (ModJava._Closure$__.$I21-0 = delegate(ModLoader.LoaderBase Raw, ModBase.LoadState NewState, ModBase.LoadState OldState)
			{
				if ((NewState == ModBase.LoadState.Failed || NewState == ModBase.LoadState.Aborted) && ModJava._Parameter != null)
				{
					ModBase.Log(string.Format("[Java] 由于下载未完成，清理未下载完成的 Java 文件：{0}", ModJava._Parameter), ModBase.LogLevel.Debug, "出现错误");
					ModBase.DeleteDirectory(ModJava._Parameter, false);
					return;
				}
				if (NewState == ModBase.LoadState.Finished)
				{
					ModJava._Parameter = null;
				}
			}) : ModJava._Closure$__.$I21-0);
			loaderDownload.HasOnStateChangedThread = true;
			return result;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0001C71C File Offset: 0x0001A91C
		private static void JavaFileList(ModLoader.LoaderTask<int, List<ModNet.NetFile>> Loader)
		{
			ModJava._Closure$__23-1 CS$<>8__locals1 = new ModJava._Closure$__23-1(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_Loader = Loader;
			ModBase.Log("[Java] 开始获取 Java 下载信息", ModBase.LogLevel.Normal, "出现错误");
			KeyValuePair<string, JObject> keyValuePair = Enumerable.First<KeyValuePair<string, JObject>>(Enumerable.SelectMany<JToken, KeyValuePair<string, JObject>>(Enumerable.Reverse<JToken>((IEnumerable<JToken>)((JObject)((JObject)ModBase.GetJson(ModNet.NetGetCodeByDownload(new string[]
			{
				"https://bmclapi2.bangbang93.com/v1/products/java-runtime/2ec0cc96c44e5a76b9c8b7c39df7210883d12871/all.json",
				"https://piston-meta.mojang.com/v1/products/java-runtime/2ec0cc96c44e5a76b9c8b7c39df7210883d12871/all.json"
			}, 45000, true, false)))[string.Format("windows-x{0}", ModBase.m_StubRepository ? "86" : "64")]).Children()), (ModJava._Closure$__.$IR23-1 == null) ? (ModJava._Closure$__.$IR23-1 = ((JToken a0) => ((ModJava._Closure$__.$I23-0 == null) ? (ModJava._Closure$__.$I23-0 = delegate(JProperty e)
			{
				ModJava._Closure$__23-0 CS$<>8__locals2 = new ModJava._Closure$__23-0(CS$<>8__locals2);
				CS$<>8__locals2.$VB$Local_e = e;
				return Enumerable.Select<JToken, KeyValuePair<string, JObject>>((JArray)CS$<>8__locals2.$VB$Local_e.Value, (JToken v) => new KeyValuePair<string, JObject>(CS$<>8__locals2.$VB$Local_e.Name, (JObject)v));
			}) : ModJava._Closure$__.$I23-0)((JProperty)a0))) : ModJava._Closure$__.$IR23-1), (KeyValuePair<string, JObject> t) => t.Value["version"]["name"].ToString().StartsWithF(Conversions.ToString(CS$<>8__locals1.$VB$Local_Loader.Input), false));
			string text = (string)keyValuePair.Value["manifest"]["url"];
			ModBase.Log(string.Format("[Java] 准备下载 Java {0}（{1}）：{2}", keyValuePair.Value["version"]["name"], keyValuePair.Key, text), ModBase.LogLevel.Normal, "出现错误");
			string data = ModNet.NetGetCodeByDownload(new string[]
			{
				text.Replace("piston-meta.mojang.com", "bmclapi2.bangbang93.com"),
				text
			}, 45000, true, false);
			ModJava._Parameter = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft\\runtime\\" + keyValuePair.Key + "\\";
			List<ModNet.NetFile> list = new List<ModNet.NetFile>();
			try
			{
				foreach (JToken jtoken in ((JObject)ModBase.GetJson(data))["files"])
				{
					JProperty jproperty = (JProperty)jtoken;
					JToken jtoken2 = ((JObject)jproperty.Value)["downloads"];
					if (((jtoken2 != null) ? jtoken2["raw"] : null) != null)
					{
						JObject jobject = (JObject)((JObject)jproperty.Value)["downloads"]["raw"];
						ModBase.FileChecker fileChecker = new ModBase.FileChecker(-1L, (long)jobject["size"], (string)jobject["sha1"], true, false);
						if (Operators.CompareString(fileChecker.specificationError, "12976a6c2b227cbac58969c1455444596c894656", false) != 0 && Operators.CompareString(fileChecker.specificationError, "c80e4bab46e34d02826eab226a4441d0970f2aba", false) != 0 && Operators.CompareString(fileChecker.specificationError, "84d2102ad171863db04e7ee22a259d1f6c5de4a5", false) != 0 && fileChecker.Check(ModJava._Parameter + jproperty.Name) != null)
						{
							string text2 = (string)jobject["url"];
							list.Add(new ModNet.NetFile(new string[]
							{
								text2.Replace("piston-data.mojang.com", "bmclapi2.bangbang93.com"),
								text2
							}, ModJava._Parameter + jproperty.Name, fileChecker, false));
						}
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
			CS$<>8__locals1.$VB$Local_Loader.Output = list;
			ModBase.Log(string.Format("[Java] 需要下载 {0} 个文件，目标文件夹：{1}", list.Count, ModJava._Parameter), ModBase.LogLevel.Normal, "出现错误");
		}

		// Token: 0x04000163 RID: 355
		public static int m_Page = 7;

		// Token: 0x04000164 RID: 356
		public static List<ModJava.JavaEntry> interceptor = new List<ModJava.JavaEntry>();

		// Token: 0x04000165 RID: 357
		private static string m_Container = null;

		// Token: 0x04000166 RID: 358
		private static string @params = null;

		// Token: 0x04000167 RID: 359
		public static object m_Dispatcher = RuntimeHelpers.GetObjectValue(new object());

		// Token: 0x04000168 RID: 360
		public static ModLoader.LoaderTask<int, int> _Process = new ModLoader.LoaderTask<int, int>("查找 Java", new Action<ModLoader.LoaderTask<int, int>>(ModJava.JavaSearchLoaderSub), null, ThreadPriority.Normal)
		{
			ProgressWeight = 2.0
		};

		// Token: 0x04000169 RID: 361
		private static string _Parameter = null;

		// Token: 0x02000066 RID: 102
		public class JavaEntry
		{
			// Token: 0x0600026D RID: 621 RVA: 0x000037B1 File Offset: 0x000019B1
			public string StartTests()
			{
				return this._UtilsRepository + "java.exe";
			}

			// Token: 0x0600026E RID: 622 RVA: 0x000037C3 File Offset: 0x000019C3
			public string PrintTests()
			{
				return this._UtilsRepository + "javaw.exe";
			}

			// Token: 0x0600026F RID: 623 RVA: 0x000037D5 File Offset: 0x000019D5
			public int PostTests()
			{
				return this.policyRepository.Minor;
			}

			// Token: 0x06000270 RID: 624 RVA: 0x0001CA64 File Offset: 0x0001AC64
			public bool CompareTests()
			{
				return this._UtilsRepository != null && ModJava.InitBroadcaster() != null && ModJava.InitBroadcaster().Replace("\\", "").Replace("/", "").ContainsF(this._UtilsRepository.Replace("\\", ""), true);
			}

			// Token: 0x06000271 RID: 625 RVA: 0x0001CAC4 File Offset: 0x0001ACC4
			public JObject ToJson()
			{
				return new JObject(new object[]
				{
					new JProperty("Path", this._UtilsRepository),
					new JProperty("VersionString", this.policyRepository.ToString()),
					new JProperty("IsJre", this.orderRepository),
					new JProperty("Is64Bit", this.producerRepository),
					new JProperty("IsUserImport", this._ClassRepository)
				});
			}

			// Token: 0x06000272 RID: 626 RVA: 0x0001CB50 File Offset: 0x0001AD50
			public static ModJava.JavaEntry FromJson(JObject Data)
			{
				return new ModJava.JavaEntry((string)Data["Path"], (bool)Data["IsUserImport"])
				{
					policyRepository = new Version((string)Data["VersionString"]),
					orderRepository = (bool)Data["IsJre"],
					producerRepository = (bool)Data["Is64Bit"]
				};
			}

			// Token: 0x06000273 RID: 627 RVA: 0x0001CBCC File Offset: 0x0001ADCC
			public override string ToString()
			{
				string text = this.policyRepository.ToString();
				if (text.StartsWithF("1.", false))
				{
					text = Strings.Mid(text, 3);
				}
				return string.Concat(new string[]
				{
					this.orderRepository ? "JRE " : "JDK ",
					Conversions.ToString(this.PostTests()),
					" (",
					text,
					")",
					this.producerRepository ? "" : "，32 位",
					this._ClassRepository ? "，手动导入" : "",
					"：",
					this._UtilsRepository
				});
			}

			// Token: 0x06000274 RID: 628 RVA: 0x0001CC80 File Offset: 0x0001AE80
			public JavaEntry(string Folder, bool IsUserImport)
			{
				this.IsChecked = false;
				if (!Folder.EndsWithF("\\", false))
				{
					Folder += "\\";
				}
				this._UtilsRepository = Folder.Replace("/", "\\");
				this._ClassRepository = IsUserImport;
			}

			// Token: 0x06000275 RID: 629 RVA: 0x0001CCD4 File Offset: 0x0001AED4
			public void Check()
			{
				if (!this.IsChecked)
				{
					string text = null;
					try
					{
						if (!File.Exists(this.PrintTests()))
						{
							throw new FileNotFoundException("未找到 javaw.exe 文件", this.PrintTests());
						}
						if (!File.Exists(this._UtilsRepository + "java.exe"))
						{
							throw new FileNotFoundException("未找到 java.exe 文件", this._UtilsRepository + "java.exe");
						}
						this.orderRepository = !File.Exists(this._UtilsRepository + "javac.exe");
						text = ModBase.ShellAndGetOutput(this._UtilsRepository + "java.exe", "-version", 15000, null).ToLower();
						if (Operators.CompareString(text, "", false) == 0)
						{
							throw new ApplicationException("尝试运行该 Java 失败");
						}
						if (ModBase._TokenRepository)
						{
							ModBase.Log("[Java] Java 检查输出：" + this._UtilsRepository + "java.exe\r\n" + text, ModBase.LogLevel.Normal, "出现错误");
						}
						if (text.Contains("/lib/ext exists"))
						{
							throw new ApplicationException("无法运行该 Java，请在删除 Java 文件夹中的 /lib/ext 文件夹后再试");
						}
						string text2;
						if ((text2 = ModBase.RegexSeek(text, "(?<=version \")[^\"]+", 0)) == null)
						{
							text2 = (ModBase.RegexSeek(text, "(?<=openjdk )[0-9]+", 0) ?? "");
						}
						string text3 = Enumerable.First<string>(text2.Replace("_", ".").Split("-"));
						if (Enumerable.Count<string>(text3.Split(".")) > 4)
						{
							text3 = text3.Replace(".0.", ".");
						}
						while (Enumerable.Count<string>(text3.Split(".")) < 4)
						{
							if (text3.StartsWithF("1.", false))
							{
								text3 += ".0";
							}
							else
							{
								text3 = "1." + text3;
							}
						}
						if (Operators.CompareString(text3, "", false) == 0)
						{
							throw new ApplicationException(string.Format("未找到该 Java 的版本号{0}", (text.Length < 500) ? string.Format("{0}输出为：{1}{2}", "\r\n", "\r\n", text) : ""));
						}
						this.policyRepository = new Version(text3);
						if (this.policyRepository.Minor == 0)
						{
							ModBase.Log("[Java] 疑似 X.0.X.X 格式版本号：" + this.policyRepository.ToString(), ModBase.LogLevel.Normal, "出现错误");
							this.policyRepository = new Version(1, this.policyRepository.Major, this.policyRepository.Build, this.policyRepository.Revision);
						}
						this.producerRepository = text.Contains("64-bit");
						if (this.policyRepository.Minor <= 4 || this.policyRepository.Minor >= 25)
						{
							throw new ApplicationException("分析详细信息失败，获取的版本为 " + this.policyRepository.ToString());
						}
						if (!this.producerRepository && !ModBase.m_StubRepository)
						{
							throw new Exception("该 Java 为 32 位版本，请安装 64 位的 Java");
						}
						if (this.orderRepository && this.PostTests() >= 16)
						{
							throw new Exception("由于高版本 JRE 对游戏的兼容性很差，因此不再允许使用。你可以使用对应版本的 JDK，而非 JRE！");
						}
					}
					catch (ApplicationException ex)
					{
						throw ex;
					}
					catch (ThreadInterruptedException ex2)
					{
						throw ex2;
					}
					catch (Exception innerException)
					{
						ModBase.Log("[Java] 检查失败的 Java 输出：" + this._UtilsRepository + "java.exe\r\n" + (text ?? "无程序输出"), ModBase.LogLevel.Normal, "出现错误");
						throw new Exception("检查 Java 失败（" + (this.PrintTests() ?? "Nothing") + "）", innerException);
					}
					this.IsChecked = true;
				}
			}

			// Token: 0x0400016A RID: 362
			public string _UtilsRepository;

			// Token: 0x0400016B RID: 363
			public bool _ClassRepository;

			// Token: 0x0400016C RID: 364
			public Version policyRepository;

			// Token: 0x0400016D RID: 365
			public bool orderRepository;

			// Token: 0x0400016E RID: 366
			public bool producerRepository;

			// Token: 0x0400016F RID: 367
			private bool IsChecked;
		}
	}
}
