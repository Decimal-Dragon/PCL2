using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using PCL.My;

namespace PCL
{
	// Token: 0x0200016E RID: 366
	[StandardModule]
	public sealed class ModMinecraft
	{
		// Token: 0x06000E57 RID: 3671 RVA: 0x00067A1C File Offset: 0x00065C1C
		private static void McFolderListLoadSub()
		{
			try
			{
				List<ModMinecraft.McFolder> list = new List<ModMinecraft.McFolder>();
				try
				{
					if (Directory.Exists(ModBase.Path + "versions\\"))
					{
						list.Add(new ModMinecraft.McFolder
						{
							Name = "当前文件夹",
							Path = ModBase.Path,
							Type = ModMinecraft.McFolderType.Original
						});
					}
					foreach (DirectoryInfo directoryInfo in new DirectoryInfo(ModBase.Path).GetDirectories())
					{
						if (Directory.Exists(directoryInfo.FullName + "versions\\") || Operators.CompareString(directoryInfo.Name, ".minecraft", false) == 0)
						{
							list.Add(new ModMinecraft.McFolder
							{
								Name = "当前文件夹",
								Path = directoryInfo.FullName + "\\",
								Type = ModMinecraft.McFolderType.Original
							});
						}
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "扫描 PCL 所在文件夹中是否有 MC 文件夹失败", ModBase.LogLevel.Debug, "出现错误");
				}
				string text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft\\";
				if ((!Enumerable.Any<ModMinecraft.McFolder>(list) || Operators.CompareString(text, list[0].Path, false) != 0) && Directory.Exists(text + "versions\\"))
				{
					list.Add(new ModMinecraft.McFolder
					{
						Name = "官方启动器文件夹",
						Path = text,
						Type = ModMinecraft.McFolderType.Original
					});
				}
				try
				{
					foreach (object value in ((IEnumerable)NewLateBinding.LateGet(ModBase.m_IdentifierRepository.Get("LaunchFolders", null), null, "Split", new object[]
					{
						"|"
					}, null, null, null)))
					{
						string text2 = Conversions.ToString(value);
						if (Operators.CompareString(text2, "", false) != 0)
						{
							if (text2.Contains(">") && text2.EndsWithF("\\", false))
							{
								string name = text2.Split(">")[0];
								string text3 = text2.Split(">")[1];
								if (ModBase.CheckPermission(text3))
								{
									bool flag = false;
									try
									{
										foreach (ModMinecraft.McFolder mcFolder in list)
										{
											if (Operators.CompareString(mcFolder.Path, text3, false) == 0)
											{
												mcFolder.Name = name;
												mcFolder.Type = ModMinecraft.McFolderType.RenamedOriginal;
												flag = true;
											}
										}
									}
									finally
									{
										List<ModMinecraft.McFolder>.Enumerator enumerator2;
										((IDisposable)enumerator2).Dispose();
									}
									if (!flag)
									{
										list.Add(new ModMinecraft.McFolder
										{
											Name = name,
											Path = text3,
											Type = ModMinecraft.McFolderType.Custom
										});
									}
								}
								else
								{
									ModMain.Hint("无法访问 Minecraft 文件夹：" + text3, ModMain.HintType.Critical, true);
								}
							}
							else
							{
								ModMain.Hint("无效的 Minecraft 文件夹：" + text2, ModMain.HintType.Critical, true);
							}
						}
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
				List<string> list2 = new List<string>();
				try
				{
					foreach (ModMinecraft.McFolder mcFolder2 in list)
					{
						if (mcFolder2.Type != ModMinecraft.McFolderType.Original)
						{
							list2.Add(mcFolder2.Name + ">" + mcFolder2.Path);
						}
					}
				}
				finally
				{
					List<ModMinecraft.McFolder>.Enumerator enumerator3;
					((IDisposable)enumerator3).Dispose();
				}
				if (!Enumerable.Any<string>(list2))
				{
					list2.Add("");
				}
				ModBase.m_IdentifierRepository.Set("LaunchFolders", list2.Join("|"), false, null);
				if (!Enumerable.Any<ModMinecraft.McFolder>(list))
				{
					Directory.CreateDirectory(ModBase.Path + ".minecraft\\versions\\");
					list.Add(new ModMinecraft.McFolder
					{
						Name = "当前文件夹",
						Path = ModBase.Path + ".minecraft\\",
						Type = ModMinecraft.McFolderType.Original
					});
				}
				try
				{
					foreach (ModMinecraft.McFolder mcFolder3 in list)
					{
						ModMinecraft.McFolderLauncherProfilesJsonCreate(mcFolder3.Path);
					}
				}
				finally
				{
					List<ModMinecraft.McFolder>.Enumerator enumerator4;
					((IDisposable)enumerator4).Dispose();
				}
				if (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("SystemDebugDelay", null)))
				{
					Thread.Sleep(ModBase.RandomInteger(200, 2000));
				}
				ModMinecraft.messageTests = list;
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, "加载 Minecraft 文件夹列表失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00067EE4 File Offset: 0x000660E4
		public static void McFolderLauncherProfilesJsonCreate(string Folder)
		{
			try
			{
				if (!File.Exists(Folder + "launcher_profiles.json"))
				{
					string text = string.Concat(new string[]
					{
						"{\r\n    \"profiles\":  {\r\n        \"PCL\": {\r\n            \"icon\": \"Grass\",\r\n            \"name\": \"PCL\",\r\n            \"lastVersionId\": \"latest-release\",\r\n            \"type\": \"latest-release\",\r\n            \"lastUsed\": \"",
						DateTime.Now.ToString("yyyy'-'MM'-'dd"),
						"T",
						DateTime.Now.ToString("HH':'mm':'ss"),
						".0000Z\"\r\n        }\r\n    },\r\n    \"selectedProfile\": \"PCL\",\r\n    \"clientToken\": \"23323323323323323323323323323333\"\r\n}"
					});
					ModBase.WriteFile(Folder + "launcher_profiles.json", text, false, Encoding.GetEncoding("GB18030"));
					ModBase.Log("[Minecraft] 已创建 launcher_profiles.json：" + Folder, ModBase.LogLevel.Normal, "出现错误");
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "创建 launcher_profiles.json 失败（" + Folder + "）", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00008F58 File Offset: 0x00007158
		public static ModMinecraft.McVersion AddClient()
		{
			return ModMinecraft.initializerTests;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00067FC4 File Offset: 0x000661C4
		public static void InstantiateClient(ModMinecraft.McVersion value)
		{
			if (!object.ReferenceEquals(RuntimeHelpers.GetObjectValue(ModMinecraft._SingletonTests), value))
			{
				ModMinecraft.initializerTests = value;
				ModMinecraft._SingletonTests = value;
				if (value != null)
				{
					PageDownloadCompDetail.parameterReader = null;
					if (ModAnimation.CalcParser() == 0 && Operators.ConditionalCompareObjectNotEqual(ModBase.m_IdentifierRepository.Get("VersionServerNide", value), ModBase.m_IdentifierRepository.Get("CacheNideServer", null), false) && Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("VersionServerLogin", value), 3, false))
					{
						ModBase.m_IdentifierRepository.Set("CacheNideAccess", "", false, null);
						ModBase.Log("[Launch] 服务器改变，要求重新登录统一通行证", ModBase.LogLevel.Normal, "出现错误");
					}
					if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("VersionServerLogin", value), 3, false))
					{
						ModBase.m_IdentifierRepository.Set("CacheNideServer", RuntimeHelpers.GetObjectValue(ModBase.m_IdentifierRepository.Get("VersionServerNide", value)), false, null);
					}
					if (ModAnimation.CalcParser() == 0 && Operators.ConditionalCompareObjectNotEqual(ModBase.m_IdentifierRepository.Get("VersionServerAuthServer", value), ModBase.m_IdentifierRepository.Get("CacheAuthServerServer", null), false) && Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("VersionServerLogin", value), 4, false))
					{
						ModBase.m_IdentifierRepository.Set("CacheAuthAccess", "", false, null);
						ModBase.Log("[Launch] 服务器改变，要求重新登录 Authlib-Injector", ModBase.LogLevel.Normal, "出现错误");
					}
					if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("VersionServerLogin", value), 4, false))
					{
						ModBase.m_IdentifierRepository.Set("CacheAuthServerServer", RuntimeHelpers.GetObjectValue(ModBase.m_IdentifierRepository.Get("VersionServerAuthServer", value)), false, null);
						ModBase.m_IdentifierRepository.Set("CacheAuthServerName", RuntimeHelpers.GetObjectValue(ModBase.m_IdentifierRepository.Get("VersionServerAuthName", value)), false, null);
						ModBase.m_IdentifierRepository.Set("CacheAuthServerRegister", RuntimeHelpers.GetObjectValue(ModBase.m_IdentifierRepository.Get("VersionServerAuthRegister", value)), false, null);
					}
				}
			}
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x000681B8 File Offset: 0x000663B8
		public static string GetMcFoolName(string Name)
		{
			Name = Name.ToLower();
			string result;
			if (Name.StartsWithF("2.0", false))
			{
				result = "2013 | 这个秘密计划了两年的更新将游戏推向了一个新高度！";
			}
			else if (Operators.CompareString(Name, "15w14a", false) == 0)
			{
				result = "2015 | 作为一款全年龄向的游戏，我们需要和平，需要爱与拥抱。";
			}
			else if (Operators.CompareString(Name, "1.rv-pre1", false) == 0)
			{
				result = "2016 | 是时候将现代科技带入 Minecraft 了！";
			}
			else if (Operators.CompareString(Name, "3d shareware v1.34", false) == 0)
			{
				result = "2019 | 我们从地下室的废墟里找到了这个开发于 1994 年的杰作！";
			}
			else if (!Name.StartsWithF("20w14inf", false) && Operators.CompareString(Name, "20w14∞", false) != 0)
			{
				if (Operators.CompareString(Name, "22w13oneblockatatime", false) == 0)
				{
					result = "2022 | 一次一个方块更新！迎接全新的挖掘、合成与骑乘玩法吧！";
				}
				else if (Operators.CompareString(Name, "23w13a_or_b", false) == 0)
				{
					result = "2023 | 研究表明：玩家喜欢作出选择——越多越好！";
				}
				else if (Operators.CompareString(Name, "24w14potato", false) == 0)
				{
					result = "2024 | 毒马铃薯一直都被大家忽视和低估，于是我们超级加强了它！";
				}
				else
				{
					result = "";
				}
			}
			else
			{
				result = "2020 | 我们加入了 20 亿个新的维度，让无限的想象变成了现实！";
			}
			return result;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0006829C File Offset: 0x0006649C
		private static void McVersionListLoad(ModLoader.LoaderTask<string, int> Loader)
		{
			string input = Loader.Input;
			try
			{
				ModMinecraft._RegTests = new Dictionary<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>>();
				List<string> list = new List<string>();
				if (Directory.Exists(input + "versions"))
				{
					try
					{
						foreach (DirectoryInfo directoryInfo in new DirectoryInfo(input + "versions").GetDirectories())
						{
							list.Add(directoryInfo.Name);
						}
					}
					catch (Exception innerException)
					{
						throw new Exception("无法读取版本文件夹，可能是由于没有权限（" + input + "versions）", innerException);
					}
				}
				if (!Enumerable.Any<string>(list))
				{
					ModBase.WriteIni(input + "PCL.ini", "VersionCache", "");
				}
				else
				{
					int num = Convert.ToInt32(decimal.Remainder(new decimal(ModBase.GetHash(Conversions.ToString(30) + "#" + list.ToArray().Join("#"))), 2147483646m));
					if (!ModMinecraft.m_ProductTests && ModBase.Val(ModBase.ReadIni(input + "PCL.ini", "VersionCache", "")) == (double)num)
					{
						Dictionary<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>> dictionary = ModMinecraft.McVersionListLoadCache(input);
						if (dictionary != null)
						{
							ModMinecraft._RegTests = dictionary;
							goto IL_16C;
						}
					}
					ModMinecraft.m_ProductTests = false;
					ModBase.Log("[Minecraft] 文件夹列表变更，重载所有版本", ModBase.LogLevel.Normal, "出现错误");
					ModBase.WriteIni(input + "PCL.ini", "VersionCache", Conversions.ToString(num));
					ModMinecraft._RegTests = ModMinecraft.McVersionListLoadNoCache(input);
					IL_16C:
					ModMinecraft.collectionTests = false;
					if (Loader.IsAborted)
					{
						return;
					}
				}
				if (Enumerable.Any<KeyValuePair<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>>>(ModMinecraft._RegTests, (ModMinecraft._Closure$__.$I22-0 == null) ? (ModMinecraft._Closure$__.$I22-0 = ((KeyValuePair<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>> v) => v.Key != ModMinecraft.McVersionCardType.Error)) : ModMinecraft._Closure$__.$I22-0))
				{
					string text = ModBase.ReadIni(input + "PCL.ini", "Version", "");
					if (Operators.CompareString(text, "", false) != 0)
					{
						try
						{
							foreach (KeyValuePair<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>> keyValuePair in ModMinecraft._RegTests)
							{
								try
								{
									foreach (ModMinecraft.McVersion mcVersion in keyValuePair.Value)
									{
										if (Operators.CompareString(mcVersion.Name, text, false) == 0 && mcVersion._ConfigurationMap != ModMinecraft.McVersionState.Error)
										{
											ModMinecraft.InstantiateClient(mcVersion);
											ModBase.m_IdentifierRepository.Set("LaunchVersionSelect", ModMinecraft.initializerTests.Name, false, null);
											ModBase.Log("[Minecraft] 选择该文件夹储存的 Minecraft 版本：" + ModMinecraft.initializerTests.Path, ModBase.LogLevel.Normal, "出现错误");
											return;
										}
									}
								}
								finally
								{
									List<ModMinecraft.McVersion>.Enumerator enumerator2;
									((IDisposable)enumerator2).Dispose();
								}
							}
						}
						finally
						{
							Dictionary<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
					if (Enumerable.First<KeyValuePair<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>>>(ModMinecraft._RegTests).Value[0]._ConfigurationMap != ModMinecraft.McVersionState.Error)
					{
						ModMinecraft.InstantiateClient(Enumerable.First<KeyValuePair<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>>>(ModMinecraft._RegTests).Value[0]);
						ModBase.m_IdentifierRepository.Set("LaunchVersionSelect", ModMinecraft.initializerTests.Name, false, null);
						ModBase.Log("[Launch] 自动选择 Minecraft 版本：" + ModMinecraft.initializerTests.Path, ModBase.LogLevel.Normal, "出现错误");
					}
				}
				else
				{
					ModMinecraft.InstantiateClient(null);
					ModBase.m_IdentifierRepository.Set("LaunchVersionSelect", "", false, null);
					ModBase.Log("[Minecraft] 未找到可用 Minecraft 版本", ModBase.LogLevel.Normal, "出现错误");
				}
				if (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("SystemDebugDelay", null)))
				{
					Thread.Sleep(ModBase.RandomInteger(200, 3000));
				}
			}
			catch (ThreadInterruptedException ex)
			{
			}
			catch (Exception ex2)
			{
				ModBase.WriteIni(input + "PCL.ini", "VersionCache", "");
				ModBase.Log(ex2, "加载 .minecraft 版本列表失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x000686EC File Offset: 0x000668EC
		private static Dictionary<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>> McVersionListLoadCache(string Path)
		{
			Dictionary<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>> dictionary = new Dictionary<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>>();
			checked
			{
				Dictionary<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>> result;
				try
				{
					int num = Conversions.ToInteger(ModBase.ReadIni(Path + "PCL.ini", "CardCount", Conversions.ToString(-1)));
					if (num == -1)
					{
						result = null;
					}
					else
					{
						int num2 = num - 1;
						for (int i = 0; i <= num2; i++)
						{
							ModMinecraft.McVersionCardType key = (ModMinecraft.McVersionCardType)Conversions.ToInteger(ModBase.ReadIni(Path + "PCL.ini", "CardKey" + Conversions.ToString(i + 1), ":"));
							List<ModMinecraft.McVersion> list = new List<ModMinecraft.McVersion>();
							foreach (string text in ModBase.ReadIni(Path + "PCL.ini", "CardValue" + Conversions.ToString(i + 1), ":").Split(":"))
							{
								if (Operators.CompareString(text, "", false) != 0)
								{
									string text2 = string.Format("{0}versions\\{1}\\", Path, text);
									if (File.Exists(text2 + ".pclignore"))
									{
										if (!ModMinecraft.collectionTests)
										{
											ModBase.Log("[Minecraft] 跳过要求忽略的项目：" + text2, ModBase.LogLevel.Normal, "出现错误");
											goto IL_5AC;
										}
										ModBase.Log("[Minecraft] 清理残留的忽略项目：" + text2, ModBase.LogLevel.Normal, "出现错误");
										File.Delete(text2 + ".pclignore");
									}
									try
									{
										ModMinecraft.McVersion mcVersion = new ModMinecraft.McVersion(text2);
										list.Add(mcVersion);
										mcVersion._ConsumerMap = ModBase.ReadIni(mcVersion.Path + "PCL\\Setup.ini", "CustomInfo", "");
										if (Operators.CompareString(mcVersion._ConsumerMap, "", false) == 0)
										{
											mcVersion._ConsumerMap = ModBase.ReadIni(mcVersion.Path + "PCL\\Setup.ini", "Info", mcVersion._ConsumerMap);
										}
										mcVersion.getterMap = ModBase.ReadIni(mcVersion.Path + "PCL\\Setup.ini", "Logo", mcVersion.getterMap);
										mcVersion._RegistryMap = Conversions.ToDate(ModBase.ReadIni(mcVersion.Path + "PCL\\Setup.ini", "ReleaseTime", Conversions.ToString(mcVersion._RegistryMap)));
										mcVersion._ConfigurationMap = (ModMinecraft.McVersionState)Conversions.ToInteger(ModBase.ReadIni(mcVersion.Path + "PCL\\Setup.ini", "State", Conversions.ToString((int)mcVersion._ConfigurationMap)));
										mcVersion._TokenMap = Conversions.ToBoolean(ModBase.ReadIni(mcVersion.Path + "PCL\\Setup.ini", "IsStar", Conversions.ToString(false)));
										mcVersion.expressionMap = (ModMinecraft.McVersionCardType)Conversions.ToInteger(ModBase.ReadIni(Path + "PCL\\Setup.ini", "DisplayType", Conversions.ToString(0)));
										if (mcVersion._ConfigurationMap != ModMinecraft.McVersionState.Error && Operators.CompareString(ModBase.ReadIni(mcVersion.Path + "PCL\\Setup.ini", "VersionOriginal", "Unknown"), "Unknown", false) != 0)
										{
											ModMinecraft.McVersionInfo mcVersionInfo = new ModMinecraft.McVersionInfo();
											mcVersionInfo._AdvisorMap = ModBase.ReadIni(mcVersion.Path + "PCL\\Setup.ini", "VersionFabric", "");
											mcVersionInfo.printerMap = ModBase.ReadIni(mcVersion.Path + "PCL\\Setup.ini", "VersionForge", "");
											mcVersionInfo.m_AttrMap = ModBase.ReadIni(mcVersion.Path + "PCL\\Setup.ini", "VersionNeoForge", "");
											mcVersionInfo._RoleMap = ModBase.ReadIni(mcVersion.Path + "PCL\\Setup.ini", "VersionOptiFine", "");
											mcVersionInfo._AccountMap = Conversions.ToBoolean(ModBase.ReadIni(mcVersion.Path + "PCL\\Setup.ini", "VersionLiteLoader", Conversions.ToString(false)));
											mcVersionInfo.LogoutThread(Conversions.ToInteger(ModBase.ReadIni(mcVersion.Path + "PCL\\Setup.ini", "VersionApiCode", Conversions.ToString(-1))));
											mcVersionInfo._ConnectionMap = ModBase.ReadIni(mcVersion.Path + "PCL\\Setup.ini", "VersionOriginal", "Unknown");
											mcVersionInfo._ServerMap = Conversions.ToInteger(ModBase.ReadIni(mcVersion.Path + "PCL\\Setup.ini", "VersionOriginalMain", Conversions.ToString(-1)));
											mcVersionInfo.m_ResolverMap = Conversions.ToInteger(ModBase.ReadIni(mcVersion.Path + "PCL\\Setup.ini", "VersionOriginalSub", Conversions.ToString(-1)));
											mcVersionInfo.workerMap = true;
											ModMinecraft.McVersionInfo mcVersionInfo2 = mcVersionInfo;
											mcVersionInfo2._CandidateMap = Enumerable.Any<char>(mcVersionInfo2._AdvisorMap);
											mcVersionInfo2.m_StructMap = Enumerable.Any<char>(mcVersionInfo2.printerMap);
											mcVersionInfo2._ValMap = Enumerable.Any<char>(mcVersionInfo2.m_AttrMap);
											mcVersionInfo2._StatusMap = Enumerable.Any<char>(mcVersionInfo2._RoleMap);
											mcVersion.Version = mcVersionInfo2;
										}
										if (mcVersion._ConfigurationMap == ModMinecraft.McVersionState.Error)
										{
											string consumerMap = mcVersion._ConsumerMap;
											mcVersion._ConfigurationMap = ModMinecraft.McVersionState.Original;
											mcVersion.Check();
											string left = ModBase.ReadIni(mcVersion.Path + "PCL\\Setup.ini", "CustomInfo", "");
											if (mcVersion._ConfigurationMap == ModMinecraft.McVersionState.Original || (Operators.CompareString(left, "", false) == 0 && Operators.CompareString(consumerMap, mcVersion._ConsumerMap, false) != 0))
											{
												ModBase.Log("[Minecraft] 版本 " + mcVersion.Name + " 的错误状态已变更，新的状态为：" + mcVersion._ConsumerMap, ModBase.LogLevel.Normal, "出现错误");
												return null;
											}
										}
										if (Operators.CompareString(mcVersion.getterMap, "", false) == 0)
										{
											ModBase.Log("[Minecraft] 版本 " + mcVersion.Name + " 未被加载", ModBase.LogLevel.Normal, "出现错误");
											return null;
										}
									}
									catch (Exception ex)
									{
										ModBase.Log(ex, "读取版本加载缓存失败（" + text + "）", ModBase.LogLevel.Debug, "出现错误");
										return null;
									}
								}
								IL_5AC:;
							}
							if (Enumerable.Any<ModMinecraft.McVersion>(list))
							{
								dictionary.Add(key, list);
							}
						}
						result = dictionary;
					}
				}
				catch (Exception ex2)
				{
					ModBase.Log(ex2, "读取版本缓存失败", ModBase.LogLevel.Debug, "出现错误");
					result = null;
				}
				return result;
			}
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00068D34 File Offset: 0x00066F34
		private static Dictionary<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>> McVersionListLoadNoCache(string Path)
		{
			List<ModMinecraft.McVersion> list = new List<ModMinecraft.McVersion>();
			foreach (DirectoryInfo directoryInfo in new DirectoryInfo(Path + "versions").GetDirectories())
			{
				if (directoryInfo.Exists && Enumerable.Any<FileInfo>(directoryInfo.EnumerateFiles()))
				{
					if ((Operators.CompareString(directoryInfo.Name, "cache", false) == 0 || Operators.CompareString(directoryInfo.Name, "BLClient", false) == 0 || Operators.CompareString(directoryInfo.Name, "PCL", false) == 0) && !File.Exists(directoryInfo.FullName + "\\" + directoryInfo.Name + ".json"))
					{
						ModBase.Log("[Minecraft] 跳过可能不是版本文件夹的项目：" + directoryInfo.FullName, ModBase.LogLevel.Normal, "出现错误");
					}
					else
					{
						string text = directoryInfo.FullName + "\\";
						if (File.Exists(text + ".pclignore"))
						{
							if (!ModMinecraft.collectionTests)
							{
								ModBase.Log("[Minecraft] 跳过要求忽略的项目：" + text, ModBase.LogLevel.Normal, "出现错误");
								goto IL_16F;
							}
							ModBase.Log("[Minecraft] 清理残留的忽略项目：" + text, ModBase.LogLevel.Normal, "出现错误");
							File.Delete(text + ".pclignore");
						}
						ModMinecraft.McVersion mcVersion = new ModMinecraft.McVersion(text);
						list.Add(mcVersion);
						mcVersion.Load();
					}
				}
				else
				{
					ModBase.Log("[Minecraft] 跳过空文件夹：" + directoryInfo.FullName, ModBase.LogLevel.Normal, "出现错误");
				}
				IL_16F:;
			}
			Dictionary<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>> dictionary = new Dictionary<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>>();
			try
			{
				Dictionary<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>> dictionary2 = new Dictionary<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>>();
				List<ModMinecraft.McVersion> list2 = new List<ModMinecraft.McVersion>();
				try
				{
					foreach (ModMinecraft.McVersion mcVersion2 in Enumerable.ToList<ModMinecraft.McVersion>(list))
					{
						if (mcVersion2._TokenMap && mcVersion2.expressionMap != ModMinecraft.McVersionCardType.Hidden)
						{
							list2.Add(mcVersion2);
							list.Remove(mcVersion2);
						}
					}
				}
				finally
				{
					List<ModMinecraft.McVersion>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				if (Enumerable.Any<ModMinecraft.McVersion>(list2))
				{
					dictionary2.Add(ModMinecraft.McVersionCardType.Star, list2);
				}
				ModMinecraft.McVersionFilter(ref list, ref dictionary2, new ModMinecraft.McVersionState[1], ModMinecraft.McVersionCardType.Error);
				ModMinecraft.McVersionFilter(ref list, ref dictionary2, new ModMinecraft.McVersionState[]
				{
					ModMinecraft.McVersionState.Fool
				}, ModMinecraft.McVersionCardType.Fool);
				ModMinecraft.McVersionFilter(ref list, ref dictionary2, new ModMinecraft.McVersionState[]
				{
					ModMinecraft.McVersionState.Forge,
					ModMinecraft.McVersionState.NeoForge,
					ModMinecraft.McVersionState.LiteLoader,
					ModMinecraft.McVersionState.Fabric
				}, ModMinecraft.McVersionCardType.API);
				List<ModMinecraft.McVersion> list3 = new List<ModMinecraft.McVersion>();
				List<ModMinecraft.McVersion> list4 = new List<ModMinecraft.McVersion>();
				ModMinecraft.McVersionFilter(ref list, new ModMinecraft.McVersionState[]
				{
					ModMinecraft.McVersionState.Old
				}, ref list4);
				ModMinecraft.McVersion mcVersion3 = null;
				try
				{
					foreach (ModMinecraft.McVersion mcVersion4 in list)
					{
						if ((mcVersion4._ConfigurationMap == ModMinecraft.McVersionState.Original || mcVersion4._ConfigurationMap == ModMinecraft.McVersionState.Snapshot) && (mcVersion3 == null || DateTime.Compare(mcVersion4._RegistryMap, mcVersion3._RegistryMap) > 0))
						{
							mcVersion3 = mcVersion4;
						}
					}
				}
				finally
				{
					List<ModMinecraft.McVersion>.Enumerator enumerator2;
					((IDisposable)enumerator2).Dispose();
				}
				if (mcVersion3 != null && mcVersion3._ConfigurationMap == ModMinecraft.McVersionState.Snapshot)
				{
					list3.Add(mcVersion3);
					list.Remove(mcVersion3);
				}
				ModMinecraft.McVersionFilter(ref list, new ModMinecraft.McVersionState[]
				{
					ModMinecraft.McVersionState.Snapshot
				}, ref list4);
				Dictionary<string, ModMinecraft.McVersion> dictionary3 = new Dictionary<string, ModMinecraft.McVersion>();
				List<int> list5 = new List<int>();
				try
				{
					foreach (ModMinecraft.McVersion mcVersion5 in list)
					{
						if (mcVersion5.Version._ServerMap >= 2)
						{
							if (!list5.Contains(mcVersion5.Version._ServerMap))
							{
								list5.Add(mcVersion5.Version._ServerMap);
							}
							if (dictionary3.ContainsKey(Conversions.ToString(mcVersion5.Version._ServerMap) + "-" + Conversions.ToString((int)mcVersion5._ConfigurationMap)))
							{
								if (mcVersion5.Version._StatusMap)
								{
									if (mcVersion5.Version.ReadThread() > dictionary3[Conversions.ToString(mcVersion5.Version._ServerMap) + "-" + Conversions.ToString((int)mcVersion5._ConfigurationMap)].Version.ReadThread())
									{
										dictionary3[Conversions.ToString(mcVersion5.Version._ServerMap) + "-" + Conversions.ToString((int)mcVersion5._ConfigurationMap)] = mcVersion5;
									}
								}
								else if (DateTime.Compare(mcVersion5._RegistryMap, dictionary3[Conversions.ToString(mcVersion5.Version._ServerMap) + "-" + Conversions.ToString((int)mcVersion5._ConfigurationMap)]._RegistryMap) > 0)
								{
									dictionary3[Conversions.ToString(mcVersion5.Version._ServerMap) + "-" + Conversions.ToString((int)mcVersion5._ConfigurationMap)] = mcVersion5;
								}
							}
							else
							{
								dictionary3.Add(Conversions.ToString(mcVersion5.Version._ServerMap) + "-" + Conversions.ToString((int)mcVersion5._ConfigurationMap), mcVersion5);
							}
						}
					}
				}
				finally
				{
					List<ModMinecraft.McVersion>.Enumerator enumerator3;
					((IDisposable)enumerator3).Dispose();
				}
				try
				{
					foreach (int value in list5)
					{
						if (dictionary3.ContainsKey(Conversions.ToString(value) + "-" + Conversions.ToString(4)) && dictionary3.ContainsKey(Conversions.ToString(value) + "-" + Conversions.ToString(1)))
						{
							ModMinecraft.McVersion mcVersion6 = dictionary3[Conversions.ToString(value) + "-" + Conversions.ToString(1)];
							ModMinecraft.McVersion mcVersion7 = dictionary3[Conversions.ToString(value) + "-" + Conversions.ToString(4)];
							if (mcVersion6.Version.m_ResolverMap > mcVersion7.Version.m_ResolverMap)
							{
								list3.Add(mcVersion6);
								list.Remove(mcVersion6);
							}
							list3.Add(mcVersion7);
							list.Remove(mcVersion7);
						}
						else if (dictionary3.ContainsKey(Conversions.ToString(value) + "-" + Conversions.ToString(4)))
						{
							list3.Add(dictionary3[Conversions.ToString(value) + "-" + Conversions.ToString(4)]);
							list.Remove(dictionary3[Conversions.ToString(value) + "-" + Conversions.ToString(4)]);
						}
						else if (dictionary3.ContainsKey(Conversions.ToString(value) + "-" + Conversions.ToString(1)))
						{
							list3.Add(dictionary3[Conversions.ToString(value) + "-" + Conversions.ToString(1)]);
							list.Remove(dictionary3[Conversions.ToString(value) + "-" + Conversions.ToString(1)]);
						}
					}
				}
				finally
				{
					List<int>.Enumerator enumerator4;
					((IDisposable)enumerator4).Dispose();
				}
				list4.AddRange(list);
				if (Enumerable.Any<ModMinecraft.McVersion>(list3))
				{
					dictionary2.Add(ModMinecraft.McVersionCardType.OriginalLike, list3);
				}
				if (Enumerable.Any<ModMinecraft.McVersion>(list4))
				{
					dictionary2.Add(ModMinecraft.McVersionCardType.Rubbish, list4);
				}
				try
				{
					foreach (KeyValuePair<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>> keyValuePair in dictionary2)
					{
						try
						{
							foreach (ModMinecraft.McVersion mcVersion8 in keyValuePair.Value)
							{
								ModMinecraft.McVersionCardType key = (mcVersion8.expressionMap == ModMinecraft.McVersionCardType.Auto || keyValuePair.Key == ModMinecraft.McVersionCardType.Star) ? keyValuePair.Key : mcVersion8.expressionMap;
								if (!dictionary.ContainsKey(key))
								{
									dictionary.Add(key, new List<ModMinecraft.McVersion>());
								}
								dictionary[key].Add(mcVersion8);
							}
						}
						finally
						{
							List<ModMinecraft.McVersion>.Enumerator enumerator6;
							((IDisposable)enumerator6).Dispose();
						}
					}
				}
				finally
				{
					Dictionary<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>>.Enumerator enumerator5;
					((IDisposable)enumerator5).Dispose();
				}
			}
			catch (Exception ex)
			{
				dictionary.Clear();
				ModBase.Log(ex, "分类版本列表失败", ModBase.LogLevel.Feedback, "出现错误");
			}
			Dictionary<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>> dictionary4 = new Dictionary<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>>();
			ModMinecraft.McVersionCardType[] array = new ModMinecraft.McVersionCardType[]
			{
				ModMinecraft.McVersionCardType.Star,
				ModMinecraft.McVersionCardType.API,
				ModMinecraft.McVersionCardType.OriginalLike,
				ModMinecraft.McVersionCardType.Rubbish,
				ModMinecraft.McVersionCardType.Fool,
				ModMinecraft.McVersionCardType.Error,
				ModMinecraft.McVersionCardType.Hidden
			};
			checked
			{
				for (int j = 0; j < array.Length; j++)
				{
					string value2 = Conversions.ToString((int)array[j]);
					if (dictionary.ContainsKey((ModMinecraft.McVersionCardType)Conversions.ToInteger(value2)))
					{
						dictionary4.Add((ModMinecraft.McVersionCardType)Conversions.ToInteger(value2), dictionary[(ModMinecraft.McVersionCardType)Conversions.ToInteger(value2)]);
					}
				}
				dictionary = dictionary4;
				if (dictionary.ContainsKey(ModMinecraft.McVersionCardType.OriginalLike))
				{
					List<ModMinecraft.McVersion> list6 = dictionary[ModMinecraft.McVersionCardType.OriginalLike];
					ModMinecraft.McVersion mcVersion9 = null;
					try
					{
						foreach (ModMinecraft.McVersion mcVersion10 in list6)
						{
							if (mcVersion10._ConfigurationMap == ModMinecraft.McVersionState.Snapshot)
							{
								mcVersion9 = mcVersion10;
								break;
							}
						}
					}
					finally
					{
						List<ModMinecraft.McVersion>.Enumerator enumerator7;
						((IDisposable)enumerator7).Dispose();
					}
					if (!Information.IsNothing(mcVersion9))
					{
						list6.Remove(mcVersion9);
					}
					List<ModMinecraft.McVersion> list7 = list6.Sort((ModMinecraft._Closure$__.$I24-0 == null) ? (ModMinecraft._Closure$__.$I24-0 = ((ModMinecraft.McVersion Left, ModMinecraft.McVersion Right) => Left.Version._ServerMap > Right.Version._ServerMap)) : ModMinecraft._Closure$__.$I24-0);
					if (!Information.IsNothing(mcVersion9))
					{
						list7.Insert(0, mcVersion9);
					}
					dictionary[ModMinecraft.McVersionCardType.OriginalLike] = list7;
				}
				if (dictionary.ContainsKey(ModMinecraft.McVersionCardType.Rubbish))
				{
					dictionary[ModMinecraft.McVersionCardType.Rubbish] = dictionary[ModMinecraft.McVersionCardType.Rubbish].Sort((ModMinecraft._Closure$__.$I24-1 == null) ? (ModMinecraft._Closure$__.$I24-1 = delegate(ModMinecraft.McVersion Left, ModMinecraft.McVersion Right)
					{
						int year = Left._RegistryMap.Year;
						int year2 = Right._RegistryMap.Year;
						bool result;
						if (year > 2000 && year2 > 2000)
						{
							if (year != year2)
							{
								result = (year > year2);
							}
							else
							{
								result = (DateTime.Compare(Left._RegistryMap, Right._RegistryMap) > 0);
							}
						}
						else
						{
							result = ((year > 2000 && year2 < 2000) || ((year >= 2000 || year2 <= 2000) && Operators.CompareString(Left.Name, Right.Name, false) > 0));
						}
						return result;
					}) : ModMinecraft._Closure$__.$I24-1);
				}
				if (dictionary.ContainsKey(ModMinecraft.McVersionCardType.API))
				{
					dictionary[ModMinecraft.McVersionCardType.API] = dictionary[ModMinecraft.McVersionCardType.API].Sort((ModMinecraft._Closure$__.$I24-2 == null) ? (ModMinecraft._Closure$__.$I24-2 = delegate(ModMinecraft.McVersion Left, ModMinecraft.McVersion Right)
					{
						int num2 = ModMinecraft.VersionSortInteger(Left.Version._ConnectionMap, Right.Version._ConnectionMap);
						bool result;
						if (num2 != 0)
						{
							result = (num2 > 0);
						}
						else if (Left.Version._CandidateMap ^ Right.Version._CandidateMap)
						{
							result = Left.Version._CandidateMap;
						}
						else if (Left.Version._ValMap ^ Right.Version._ValMap)
						{
							result = Left.Version._ValMap;
						}
						else if (Left.Version.m_StructMap ^ Right.Version.m_StructMap)
						{
							result = Left.Version.m_StructMap;
						}
						else if (Left.Version.ReadThread() == Right.Version.ReadThread())
						{
							result = (Left.Version.ReadThread() > Right.Version.ReadThread());
						}
						else
						{
							result = (Operators.CompareString(Left.Name, Right.Name, false) > 0);
						}
						return result;
					}) : ModMinecraft._Closure$__.$I24-2);
				}
				ModBase.WriteIni(Path + "PCL.ini", "CardCount", Conversions.ToString(dictionary.Count));
				int num = dictionary.Count - 1;
				for (int k = 0; k <= num; k++)
				{
					ModBase.WriteIni(Path + "PCL.ini", "CardKey" + Conversions.ToString(k + 1), Conversions.ToString((int)Enumerable.ElementAtOrDefault<ModMinecraft.McVersionCardType>(dictionary.Keys, k)));
					string text2 = "";
					try
					{
						foreach (ModMinecraft.McVersion mcVersion11 in Enumerable.ElementAtOrDefault<List<ModMinecraft.McVersion>>(dictionary.Values, k))
						{
							text2 = text2 + mcVersion11.Name + ":";
						}
					}
					finally
					{
						List<ModMinecraft.McVersion>.Enumerator enumerator8;
						((IDisposable)enumerator8).Dispose();
					}
					ModBase.WriteIni(Path + "PCL.ini", "CardValue" + Conversions.ToString(k + 1), text2);
				}
				return dictionary;
			}
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0006984C File Offset: 0x00067A4C
		private static void McVersionFilter(ref List<ModMinecraft.McVersion> VersionList, ref Dictionary<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>> Target, ModMinecraft.McVersionState[] Formula, ModMinecraft.McVersionCardType CardType)
		{
			ModMinecraft._Closure$__25-0 CS$<>8__locals1 = new ModMinecraft._Closure$__25-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_Formula = Formula;
			List<ModMinecraft.McVersion> list = Enumerable.ToList<ModMinecraft.McVersion>(Enumerable.Where<ModMinecraft.McVersion>(VersionList, (ModMinecraft.McVersion v) => Enumerable.Contains<ModMinecraft.McVersionState>(CS$<>8__locals1.$VB$Local_Formula, v._ConfigurationMap)));
			if (Enumerable.Any<ModMinecraft.McVersion>(list))
			{
				Target.Add(CardType, list);
				try
				{
					foreach (ModMinecraft.McVersion item in list)
					{
						VersionList.Remove(item);
					}
				}
				finally
				{
					List<ModMinecraft.McVersion>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x000698D4 File Offset: 0x00067AD4
		private static void McVersionFilter(ref List<ModMinecraft.McVersion> VersionList, ModMinecraft.McVersionState[] Formula, ref List<ModMinecraft.McVersion> KeepList)
		{
			ModMinecraft._Closure$__26-0 CS$<>8__locals1 = new ModMinecraft._Closure$__26-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_Formula = Formula;
			KeepList.AddRange(Enumerable.Where<ModMinecraft.McVersion>(VersionList, (ModMinecraft.McVersion v) => Enumerable.Contains<ModMinecraft.McVersionState>(CS$<>8__locals1.$VB$Local_Formula, v._ConfigurationMap)));
			if (Enumerable.Any<ModMinecraft.McVersion>(KeepList))
			{
				try
				{
					foreach (ModMinecraft.McVersion item in KeepList)
					{
						VersionList.Remove(item);
					}
				}
				finally
				{
					List<ModMinecraft.McVersion>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00069958 File Offset: 0x00067B58
		public static ModMinecraft.McSkinInfo McSkinSelect()
		{
			string text = ModBase.SelectFile("皮肤文件(*.png;*.jpg;*.webp)|*.png;*.jpg;*.webp", "选择皮肤文件");
			ModMinecraft.McSkinInfo result;
			if (Operators.CompareString(text, "", false) == 0)
			{
				result = new ModMinecraft.McSkinInfo
				{
					_ModelMap = false
				};
			}
			else
			{
				try
				{
					MyBitmap myBitmap = new MyBitmap(text);
					if (myBitmap._ContainerIterator.Width == 64)
					{
						if (myBitmap._ContainerIterator.Height == 32 || myBitmap._ContainerIterator.Height == 64)
						{
							FileInfo fileInfo = new FileInfo(text);
							if (fileInfo.Length > 24576L)
							{
								ModMain.Hint("皮肤文件大小需小于 24 KB，而所选文件大小为 " + Conversions.ToString(Math.Round((double)fileInfo.Length / 1024.0, 2)) + " KB", ModMain.HintType.Critical, true);
								return new ModMinecraft.McSkinInfo
								{
									_ModelMap = false
								};
							}
							goto IL_130;
						}
					}
					ModMain.Hint("皮肤图片大小应为 64x32 像素或 64x64 像素！", ModMain.HintType.Critical, true);
					return new ModMinecraft.McSkinInfo
					{
						_ModelMap = false
					};
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "皮肤文件存在错误", ModBase.LogLevel.Hint, "出现错误");
					return new ModMinecraft.McSkinInfo
					{
						_ModelMap = false
					};
				}
				IL_130:
				int num = ModMain.MyMsgBox("此皮肤为 Steve 模型（粗手臂）还是 Alex 模型（细手臂）？", "选择皮肤种类", "Steve 模型", "Alex 模型", "我不知道", false, false, false, null, null, null);
				if (num == 3)
				{
					ModMain.Hint("请在皮肤下载页面确认皮肤种类后再使用此皮肤！", ModMain.HintType.Info, true);
					result = new ModMinecraft.McSkinInfo
					{
						_ModelMap = false
					};
				}
				else
				{
					result = new ModMinecraft.McSkinInfo
					{
						_ModelMap = true,
						eventMap = (num == 2),
						managerMap = text
					};
				}
			}
			return result;
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00069B14 File Offset: 0x00067D14
		public static string McSkinGetAddress(string Uuid, string Type)
		{
			if (Operators.CompareString(Uuid, "", false) == 0)
			{
				throw new Exception("Uuid 为空。");
			}
			if (Uuid.StartsWithF("00000", false))
			{
				throw new Exception("离线 Uuid 无正版皮肤文件。");
			}
			string text = ModBase.ReadIni(ModBase.m_DecoratorRepository + "Cache\\Skin\\Index" + Type + ".ini", Uuid, "");
			string result;
			if (Operators.CompareString(text, "", false) != 0)
			{
				result = text;
			}
			else
			{
				string str;
				if (Operators.CompareString(Type, "Mojang", false) != 0 && Operators.CompareString(Type, "Ms", false) != 0)
				{
					if (Operators.CompareString(Type, "Nide", false) != 0)
					{
						if (Operators.CompareString(Type, "Auth", false) != 0)
						{
							throw new ArgumentException("皮肤地址种类无效：" + (Type ?? "null"));
						}
						str = Conversions.ToString(Operators.ConcatenateObject((ModMinecraft.initializerTests == null) ? ModBase.m_IdentifierRepository.Get("CacheAuthServerServer", null) : ModBase.m_IdentifierRepository.Get("VersionServerAuthServer", ModMinecraft.initializerTests), "/sessionserver/session/minecraft/profile/"));
					}
					else
					{
						str = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("https://auth.mc-user.com:233/", (ModMinecraft.initializerTests == null) ? ModBase.m_IdentifierRepository.Get("CacheNideServer", null) : ModBase.m_IdentifierRepository.Get("VersionServerNide", ModMinecraft.initializerTests)), "/sessionserver/session/minecraft/profile/"));
					}
				}
				else
				{
					str = "https://sessionserver.mojang.com/session/minecraft/profile/";
				}
				object obj = RuntimeHelpers.GetObjectValue(ModNet.NetGetCodeByRequestRetry(str + Uuid, null, "", false, null, false));
				if (Operators.ConditionalCompareObjectEqual(obj, "", false))
				{
					throw new Exception("皮肤返回值为空，可能是未设置自定义皮肤的用户");
				}
				string text2;
				try
				{
					try
					{
						foreach (object obj2 in ((IEnumerable)NewLateBinding.LateIndexGet(ModBase.GetJson(Conversions.ToString(obj)), new object[]
						{
							"properties"
						}, null)))
						{
							object objectValue = RuntimeHelpers.GetObjectValue(obj2);
							if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateIndexGet(objectValue, new object[]
							{
								"name"
							}, null), "textures", false))
							{
								text2 = Conversions.ToString(NewLateBinding.LateIndexGet(objectValue, new object[]
								{
									"value"
								}, null));
								goto IL_265;
							}
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
					throw new Exception("未从皮肤返回值中找到符合条件的 Property");
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, Conversions.ToString(Operators.ConcatenateObject("无法完成解析的皮肤返回值，可能是未设置自定义皮肤的用户：", obj)), ModBase.LogLevel.Developer, "出现错误");
					throw new Exception("皮肤返回值中不包含皮肤数据项，可能是未设置自定义皮肤的用户", ex);
				}
				IL_265:
				obj = Encoding.GetEncoding("utf-8").GetString(Convert.FromBase64String(text2));
				JObject jobject = (JObject)ModBase.GetJson(Conversions.ToString(NewLateBinding.LateGet(obj, null, "ToLower", new object[0], null, null, null)));
				if (jobject["textures"] == null || jobject["textures"]["skin"] == null || jobject["textures"]["skin"]["url"] == null)
				{
					throw new Exception("用户未设置自定义皮肤");
				}
				text2 = jobject["textures"]["skin"]["url"].ToString();
				ModBase.WriteIni(ModBase.m_DecoratorRepository + "Cache\\Skin\\Index" + Type + ".ini", Uuid, text2);
				ModBase.Log("[Skin] UUID " + Uuid + " 对应的皮肤文件为 " + text2, ModBase.LogLevel.Normal, "出现错误");
				result = text2;
			}
			return result;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00069EA0 File Offset: 0x000680A0
		public static string McSkinDownload(string Address)
		{
			ModBase.GetFileNameFromPath(Address);
			string text = ModBase.m_DecoratorRepository + "Cache\\Skin\\" + Conversions.ToString(ModBase.GetHash(Address)) + ".png";
			object obj = ModMinecraft.visitorTests;
			ObjectFlowControl.CheckForSyncLockOnValueType(obj);
			string result;
			lock (obj)
			{
				if (!File.Exists(text))
				{
					ModNet.NetDownload(Address, text + ".PCLDownloading", false);
					File.Delete(text);
					FileSystem.Rename(text + ".PCLDownloading", text);
					ModBase.Log("[Minecraft] 皮肤下载成功：" + text, ModBase.LogLevel.Normal, "出现错误");
				}
				result = text;
			}
			return result;
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00069F50 File Offset: 0x00068150
		public static string McSkinSex(string Uuid)
		{
			string result;
			if (Uuid.Length != 32)
			{
				result = "Steve";
			}
			else
			{
				int num = int.Parse(Conversions.ToString(Uuid[7]), NumberStyles.AllowHexSpecifier);
				int num2 = int.Parse(Conversions.ToString(Uuid[15]), NumberStyles.AllowHexSpecifier);
				int num3 = int.Parse(Conversions.ToString(Uuid[23]), NumberStyles.AllowHexSpecifier);
				int num4 = int.Parse(Conversions.ToString(Uuid[31]), NumberStyles.AllowHexSpecifier);
				result = (((num ^ num2 ^ num3 ^ num4) % 2 != 0) ? "Alex" : "Steve");
			}
			return result;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00069FE8 File Offset: 0x000681E8
		public static bool McJsonRuleCheck(JToken RuleToken)
		{
			bool result;
			if (RuleToken == null)
			{
				result = true;
			}
			else
			{
				bool flag = false;
				try
				{
					foreach (JToken jtoken in RuleToken)
					{
						bool flag2 = true;
						if (jtoken["os"] != null)
						{
							if (jtoken["os"]["name"] != null)
							{
								string left = jtoken["os"]["name"].ToString();
								if (Operators.CompareString(left, "unknown", false) != 0)
								{
									if (Operators.CompareString(left, "windows", false) == 0)
									{
										if (jtoken["os"]["version"] != null)
										{
											string regex = jtoken["os"]["version"].ToString();
											flag2 = (flag2 && ModBase.RegexCheck(ModMinecraft.valueTests, regex, 0));
										}
									}
									else
									{
										flag2 = false;
									}
								}
							}
							if (jtoken["os"]["arch"] != null)
							{
								flag2 = (flag2 && Operators.CompareString(jtoken["os"]["arch"].ToString(), "x86", false) == 0 == ModBase.m_StubRepository);
							}
						}
						if (!Information.IsNothing(jtoken["features"]))
						{
							flag2 = (flag2 && Information.IsNothing(jtoken["features"]["is_demo_user"]));
							if (Enumerable.Any<JToken>((IEnumerable<JToken>)((JObject)jtoken["features"]).Children(), (ModMinecraft._Closure$__.$IR35-2 == null) ? (ModMinecraft._Closure$__.$IR35-2 = ((JToken a0) => ((ModMinecraft._Closure$__.$I35-0 == null) ? (ModMinecraft._Closure$__.$I35-0 = ((JProperty j) => j.Name.Contains("quick_play"))) : ModMinecraft._Closure$__.$I35-0)((JProperty)a0))) : ModMinecraft._Closure$__.$IR35-2))
							{
								flag2 = false;
							}
						}
						if (Operators.CompareString(jtoken["action"].ToString(), "allow", false) == 0)
						{
							if (flag2)
							{
								flag = true;
							}
						}
						else if (flag2)
						{
							flag = false;
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
				result = flag;
			}
			return result;
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0006A1FC File Offset: 0x000683FC
		public static List<ModMinecraft.McLibToken> McLibListGet(ModMinecraft.McVersion Version, bool IncludeVersionJar)
		{
			ModBase.Log("[Minecraft] 获取支持库列表：" + Version.Name, ModBase.LogLevel.Normal, "出现错误");
			List<ModMinecraft.McLibToken> list = ModMinecraft.McLibListGetWithJson(Version.NewThread(), false, null, Version.ChangeMapper() + ".jumploader\\");
			if (IncludeVersionJar)
			{
				JToken jtoken = Version.NewThread()["jar"];
				string text = (jtoken != null) ? jtoken.ToString() : null;
				ModMinecraft.McVersion mcVersion;
				if (!Version.InvokeThread() && text != null)
				{
					mcVersion = new ModMinecraft.McVersion(text);
				}
				else
				{
					ModMinecraft.McVersion mcVersion2 = Version;
					if (Version.Version.m_StructMap || Version.Version._ValMap)
					{
						if (Version.Version._ServerMap >= 17)
						{
							goto IL_EE;
						}
					}
					while (Operators.CompareString(mcVersion2.CallThread(), "", false) != 0 && Operators.CompareString(mcVersion2.CallThread(), mcVersion2.Name, false) != 0)
					{
						mcVersion2 = new ModMinecraft.McVersion(ModMinecraft.m_ProxyTests + "versions\\" + mcVersion2.CallThread() + "\\");
					}
					IL_EE:
					mcVersion = new ModMinecraft.McVersion(mcVersion2.Path);
				}
				if (!File.Exists(mcVersion.Path + mcVersion.Name + ".json"))
				{
					mcVersion = Version;
					ModBase.Log("[Minecraft] 可能缺少前置版本 " + mcVersion.Name + "，找不到对应的 json 文件", ModBase.LogLevel.Debug, "出现错误");
				}
				string value;
				string codeMap;
				if (mcVersion.NewThread()["downloads"] != null && mcVersion.NewThread()["downloads"]["client"] != null)
				{
					value = (string)mcVersion.NewThread()["downloads"]["client"]["url"];
					codeMap = (string)mcVersion.NewThread()["downloads"]["client"]["sha1"];
				}
				else
				{
					value = null;
					codeMap = null;
				}
				List<ModMinecraft.McLibToken> list2 = list;
				ModMinecraft.McLibToken mcLibToken = new ModMinecraft.McLibToken();
				mcLibToken._WrapperMap = mcVersion.Path + mcVersion.Name + ".jar";
				mcLibToken.m_BaseMap = 0L;
				mcLibToken.attributeMap = false;
				mcLibToken.ForgotThread(value);
				mcLibToken.codeMap = codeMap;
				list2.Add(mcLibToken);
			}
			return list;
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0006A41C File Offset: 0x0006861C
		public static List<ModMinecraft.McLibToken> McLibListGetWithJson(JObject JsonObject, bool KeepSameNameDifferentVersionResult = false, string CustomMcFolder = null, string JumpLoaderFolder = null)
		{
			CustomMcFolder = (CustomMcFolder ?? ModMinecraft.m_ProxyTests);
			List<ModMinecraft.McLibToken> list = new List<ModMinecraft.McLibToken>();
			JArray jarray = (JArray)JsonObject["libraries"];
			if (JsonObject["jumploader"] != null && JsonObject["jumploader"]["jars"] != null && JsonObject["jumploader"]["jars"]["maven"] != null)
			{
				try
				{
					foreach (JToken jtoken in JsonObject["jumploader"]["jars"]["maven"])
					{
						jarray.Add(jtoken);
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
			}
			checked
			{
				try
				{
					foreach (JToken jtoken2 in jarray.Children())
					{
						JObject jobject = (JObject)jtoken2;
						for (int i = Enumerable.Count<JProperty>(jobject.Properties()) - 1; i >= 0; i += -1)
						{
							if (Enumerable.ElementAtOrDefault<JProperty>(jobject.Properties(), i).Value.Type == 10)
							{
								jobject.Remove(Enumerable.ElementAtOrDefault<JProperty>(jobject.Properties(), i).Name);
							}
						}
						if (ModMinecraft.McJsonRuleCheck(jobject["rules"]))
						{
							bool flag = false;
							if (jobject["mavenPath"] != null)
							{
								flag = true;
								if (jobject["name"] == null)
								{
									jobject.Add("name", jobject["mavenPath"]);
								}
								if (jobject["repoUrl"] != null && jobject["url"] == null)
								{
									jobject.Add("url", jobject["repoUrl"]);
								}
							}
							string text = (string)jobject["url"];
							if (text != null)
							{
								text += ModMinecraft.McLibGet((string)jobject["name"], false, true, CustomMcFolder).Replace("\\", "/");
							}
							if (jobject["natives"] == null)
							{
								string wrapperMap;
								if (flag)
								{
									wrapperMap = ModMinecraft.McLibGet((string)jobject["name"], true, false, JumpLoaderFolder ?? CustomMcFolder);
								}
								else
								{
									wrapperMap = ModMinecraft.McLibGet((string)jobject["name"], true, false, CustomMcFolder);
								}
								try
								{
									if (jobject["downloads"] != null && jobject["downloads"]["artifact"] != null)
									{
										List<ModMinecraft.McLibToken> list2 = list;
										ModMinecraft.McLibToken mcLibToken = new ModMinecraft.McLibToken();
										mcLibToken.m_InfoMap = flag;
										mcLibToken._AnnotationMap = (string)jobject["name"];
										mcLibToken.ForgotThread((string)((text != null) ? text : jobject["downloads"]["artifact"]["url"]));
										mcLibToken._WrapperMap = ((jobject["downloads"]["artifact"]["path"] == null) ? ModMinecraft.McLibGet((string)jobject["name"], true, false, CustomMcFolder) : (CustomMcFolder + "libraries\\" + jobject["downloads"]["artifact"]["path"].ToString().Replace("/", "\\")));
										mcLibToken.m_BaseMap = (long)Math.Round(ModBase.Val(jobject["downloads"]["artifact"]["size"].ToString()));
										mcLibToken.attributeMap = false;
										ModMinecraft.McLibToken mcLibToken2 = mcLibToken;
										JToken jtoken3 = jobject["downloads"]["artifact"]["sha1"];
										mcLibToken2.codeMap = ((jtoken3 != null) ? jtoken3.ToString() : null);
										list2.Add(mcLibToken);
									}
									else
									{
										List<ModMinecraft.McLibToken> list3 = list;
										ModMinecraft.McLibToken mcLibToken3 = new ModMinecraft.McLibToken();
										mcLibToken3.m_InfoMap = flag;
										mcLibToken3._AnnotationMap = (string)jobject["name"];
										mcLibToken3.ForgotThread(text);
										mcLibToken3._WrapperMap = wrapperMap;
										mcLibToken3.m_BaseMap = 0L;
										mcLibToken3.attributeMap = false;
										mcLibToken3.codeMap = null;
										list3.Add(mcLibToken3);
									}
									continue;
								}
								catch (Exception ex)
								{
									ModBase.Log(ex, "处理实际支持库列表失败（无 Natives，" + (jobject["name"] ?? "Nothing").ToString() + "）", ModBase.LogLevel.Debug, "出现错误");
									List<ModMinecraft.McLibToken> list4 = list;
									ModMinecraft.McLibToken mcLibToken4 = new ModMinecraft.McLibToken();
									mcLibToken4.m_InfoMap = flag;
									mcLibToken4._AnnotationMap = (string)jobject["name"];
									mcLibToken4.ForgotThread(text);
									mcLibToken4._WrapperMap = wrapperMap;
									mcLibToken4.m_BaseMap = 0L;
									mcLibToken4.attributeMap = false;
									mcLibToken4.codeMap = null;
									list4.Add(mcLibToken4);
									continue;
								}
							}
							if (jobject["natives"]["windows"] != null)
							{
								try
								{
									if (jobject["downloads"] != null && jobject["downloads"]["classifiers"] != null && jobject["downloads"]["classifiers"]["natives-windows"] != null)
									{
										List<ModMinecraft.McLibToken> list5 = list;
										ModMinecraft.McLibToken mcLibToken5 = new ModMinecraft.McLibToken();
										mcLibToken5.m_InfoMap = flag;
										mcLibToken5._AnnotationMap = (string)jobject["name"];
										mcLibToken5.ForgotThread((string)((text != null) ? text : jobject["downloads"]["classifiers"]["natives-windows"]["url"]));
										mcLibToken5._WrapperMap = ((jobject["downloads"]["classifiers"]["natives-windows"]["path"] == null) ? ModMinecraft.McLibGet((string)jobject["name"], true, false, CustomMcFolder).Replace(".jar", "-" + jobject["natives"]["windows"].ToString() + ".jar").Replace("${arch}", Environment.Is64BitOperatingSystem ? "64" : "32") : (CustomMcFolder + "libraries\\" + jobject["downloads"]["classifiers"]["natives-windows"]["path"].ToString().Replace("/", "\\")));
										mcLibToken5.m_BaseMap = (long)Math.Round(ModBase.Val(jobject["downloads"]["classifiers"]["natives-windows"]["size"].ToString()));
										mcLibToken5.attributeMap = true;
										mcLibToken5.codeMap = jobject["downloads"]["classifiers"]["natives-windows"]["sha1"].ToString();
										list5.Add(mcLibToken5);
									}
									else
									{
										List<ModMinecraft.McLibToken> list6 = list;
										ModMinecraft.McLibToken mcLibToken6 = new ModMinecraft.McLibToken();
										mcLibToken6.m_InfoMap = flag;
										mcLibToken6._AnnotationMap = (string)jobject["name"];
										mcLibToken6.ForgotThread(text);
										mcLibToken6._WrapperMap = ModMinecraft.McLibGet((string)jobject["name"], true, false, CustomMcFolder).Replace(".jar", "-" + jobject["natives"]["windows"].ToString() + ".jar").Replace("${arch}", Environment.Is64BitOperatingSystem ? "64" : "32");
										mcLibToken6.m_BaseMap = 0L;
										mcLibToken6.attributeMap = true;
										mcLibToken6.codeMap = null;
										list6.Add(mcLibToken6);
									}
								}
								catch (Exception ex2)
								{
									ModBase.Log(ex2, "处理实际支持库列表失败（有 Natives，" + (jobject["name"] ?? "Nothing").ToString() + "）", ModBase.LogLevel.Debug, "出现错误");
									List<ModMinecraft.McLibToken> list7 = list;
									ModMinecraft.McLibToken mcLibToken7 = new ModMinecraft.McLibToken();
									mcLibToken7.m_InfoMap = flag;
									mcLibToken7._AnnotationMap = (string)jobject["name"];
									mcLibToken7.ForgotThread(text);
									mcLibToken7._WrapperMap = ModMinecraft.McLibGet((string)jobject["name"], true, false, CustomMcFolder).Replace(".jar", "-" + jobject["natives"]["windows"].ToString() + ".jar").Replace("${arch}", Environment.Is64BitOperatingSystem ? "64" : "32");
									mcLibToken7.m_BaseMap = 0L;
									mcLibToken7.attributeMap = true;
									mcLibToken7.codeMap = null;
									list7.Add(mcLibToken7);
								}
							}
						}
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
				Dictionary<string, ModMinecraft.McLibToken> dictionary = new Dictionary<string, ModMinecraft.McLibToken>();
				VB$AnonymousDelegate_10<ModMinecraft.McLibToken, string> vb$AnonymousDelegate_ = (ModMinecraft._Closure$__.$I38-0 == null) ? (ModMinecraft._Closure$__.$I38-0 = ((ModMinecraft.McLibToken Token) => ModBase.GetFolderNameFromPath(ModBase.GetPathFromFullPath(Token._WrapperMap)))) : ModMinecraft._Closure$__.$I38-0;
				int num = list.Count - 1;
				for (int j = 0; j <= num; j++)
				{
					string text2 = list[j].Name + list[j].attributeMap.ToString() + list[j].m_InfoMap.ToString();
					if (dictionary.ContainsKey(text2))
					{
						string text3 = vb$AnonymousDelegate_(list[j]);
						string text4 = vb$AnonymousDelegate_(dictionary[text2]);
						if (Operators.CompareString(text3, text4, false) != 0 && KeepSameNameDifferentVersionResult)
						{
							ModBase.Log(string.Format("[Minecraft] 发现疑似重复的支持库：{0} ({1}) 与 {2} ({3})", new object[]
							{
								list[j],
								text3,
								dictionary[text2],
								text4
							}), ModBase.LogLevel.Normal, "出现错误");
							dictionary.Add(text2 + Conversions.ToString(ModBase.GetUuid()), list[j]);
						}
						else
						{
							ModBase.Log(string.Format("[Minecraft] 发现重复的支持库：{0} ({1}) 与 {2} ({3})，已忽略其中之一", new object[]
							{
								list[j],
								text3,
								dictionary[text2],
								text4
							}), ModBase.LogLevel.Normal, "出现错误");
							if (ModMinecraft.VersionSortBoolean(text3, text4))
							{
								dictionary[text2] = list[j];
							}
						}
					}
					else
					{
						dictionary.Add(text2, list[j]);
					}
				}
				return Enumerable.ToList<ModMinecraft.McLibToken>(dictionary.Values);
			}
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0006AF24 File Offset: 0x00069124
		public static List<ModNet.NetFile> McLibFix(ModMinecraft.McVersion Version)
		{
			if (!Version.importerMap)
			{
				Version.Load();
			}
			List<ModNet.NetFile> list = new List<ModNet.NetFile>();
			try
			{
				ModNet.NetFile netFile = ModDownload.DlClientJarGet(Version, true);
				if (netFile != null)
				{
					list.Add(netFile);
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "版本缺失主 jar 文件所必须的信息", ModBase.LogLevel.Developer, "出现错误");
			}
			list.AddRange(ModMinecraft.McLibFixFromLibToken(ModMinecraft.McLibListGet(Version, false), null, Version.ChangeMapper() + ".jumploader\\"));
			if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("VersionServerLogin", Version), 3, false))
			{
				string localPath = ModBase.m_InstanceRepository + "nide8auth.jar";
				JObject jobject = null;
				try
				{
					ModBase.Log("[Minecraft] 开始获取统一通行证下载信息", ModBase.LogLevel.Normal, "出现错误");
					jobject = (JObject)ModBase.GetJson(ModNet.NetGetCodeByDownload(new string[]
					{
						Conversions.ToString(Operators.ConcatenateObject("https://auth.mc-user.com:233/", ModBase.m_IdentifierRepository.Get("VersionServerNide", Version)))
					}, 45000, true, false));
				}
				catch (Exception ex2)
				{
					ModBase.Log(ex2, "获取统一通行证下载信息失败", ModBase.LogLevel.Debug, "出现错误");
				}
				if (jobject != null)
				{
					ModBase.FileChecker fileChecker = new ModBase.FileChecker(-1L, -1L, jobject["jarHash"].ToString(), true, false);
					if (fileChecker.Check(localPath) != null)
					{
						ModBase.Log("[Minecraft] 统一通行证需要更新：Hash - " + fileChecker.specificationError, ModBase.LogLevel.Developer, "出现错误");
						list.Add(new ModNet.NetFile(new string[]
						{
							"https://login.mc-user.com:233/index/jar"
						}, localPath, fileChecker, false));
					}
				}
			}
			if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("VersionServerLogin", Version), 4, false))
			{
				string localPath2 = ModBase._MethodRepository + "\\authlib-injector.jar";
				JObject jobject2 = null;
				try
				{
					ModBase.Log("[Minecraft] 开始获取 Authlib-Injector 下载信息", ModBase.LogLevel.Normal, "出现错误");
					jobject2 = (JObject)ModBase.GetJson(ModNet.NetGetCodeByDownload(new string[]
					{
						"https://authlib-injector.yushi.moe/artifact/latest.json",
						"https://bmclapi2.bangbang93.com/mirrors/authlib-injector/artifact/latest.json"
					}, 45000, true, false));
				}
				catch (Exception ex3)
				{
					ModBase.Log(ex3, "获取 Authlib-Injector 下载信息失败", ModBase.LogLevel.Debug, "出现错误");
				}
				if (jobject2 != null && new ModBase.FileChecker(-1L, -1L, jobject2["checksums"]["sha256"].ToString(), true, false).Check(localPath2) != null)
				{
					string text = jobject2["download_url"].ToString().Replace("bmclapi2.bangbang93.com/mirrors/authlib-injector", "authlib-injector.yushi.moe");
					ModBase.Log("[Minecraft] Authlib-Injector 需要更新：" + text, ModBase.LogLevel.Developer, "出现错误");
					list.Add(new ModNet.NetFile(new string[]
					{
						text,
						text.Replace("authlib-injector.yushi.moe", "bmclapi2.bangbang93.com/mirrors/authlib-injector")
					}, localPath2, new ModBase.FileChecker(-1L, -1L, jobject2["checksums"]["sha256"].ToString(), true, false), false));
				}
			}
			if (Conversions.ToBoolean(ModMinecraft.ShouldIgnoreFileCheck(Version)))
			{
				ModBase.Log("[Minecraft] 用户要求尽量忽略文件检查，这可能会保留有误的文件", ModBase.LogLevel.Normal, "出现错误");
				list = Enumerable.ToList<ModNet.NetFile>(Enumerable.Where<ModNet.NetFile>(list, (ModMinecraft._Closure$__.$I39-0 == null) ? (ModMinecraft._Closure$__.$I39-0 = delegate(ModNet.NetFile f)
				{
					bool result;
					if (File.Exists(f.m_AuthenticationTest))
					{
						ModBase.Log("[Minecraft] 跳过下载的支持库文件：" + f.m_AuthenticationTest, ModBase.LogLevel.Debug, "出现错误");
						result = false;
					}
					else
					{
						result = true;
					}
					return result;
				}) : ModMinecraft._Closure$__.$I39-0));
			}
			return list;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0006B2A4 File Offset: 0x000694A4
		public static List<ModNet.NetFile> McLibFixFromLibToken(List<ModMinecraft.McLibToken> Libs, string CustomMcFolder = null, string JumpLoaderFolder = null)
		{
			CustomMcFolder = (CustomMcFolder ?? ModMinecraft.m_ProxyTests);
			List<ModNet.NetFile> list = new List<ModNet.NetFile>();
			try
			{
				foreach (ModMinecraft.McLibToken mcLibToken in Libs)
				{
					ModBase.FileChecker fileChecker = new ModBase.FileChecker(-1L, (mcLibToken.m_BaseMap == 0L) ? -1L : mcLibToken.m_BaseMap, mcLibToken.codeMap, true, false);
					if (fileChecker.Check(mcLibToken._WrapperMap) != null)
					{
						List<string> list2 = new List<string>();
						if (mcLibToken.ReflectThread() != null)
						{
							list2.Add(mcLibToken.ReflectThread());
							if (mcLibToken.ReflectThread().Contains("launcher.mojang.com/v1/objects") || mcLibToken.ReflectThread().Contains("client.txt") || mcLibToken.ReflectThread().Contains(".tsrg"))
							{
								list2.AddRange(Enumerable.ToList<string>(ModDownload.DlSourceLauncherOrMetaGet(mcLibToken.ReflectThread())));
							}
							if (mcLibToken.ReflectThread().Contains("maven"))
							{
								list2.Insert(0, mcLibToken.ReflectThread().Replace(Strings.Mid(mcLibToken.ReflectThread(), 1, mcLibToken.ReflectThread().IndexOfF("maven", false)), "https://bmclapi2.bangbang93.com/").Replace("maven.fabricmc.net", "maven").Replace("maven.minecraftforge.net", "maven").Replace("maven.neoforged.net/releases", "maven"));
							}
						}
						if (mcLibToken._WrapperMap.Contains("transformer-discovery-service"))
						{
							if (!File.Exists(mcLibToken._WrapperMap))
							{
								ModBase.WriteFile(mcLibToken._WrapperMap, ModBase.GetResources("Transformer"), false);
							}
							ModBase.Log("[Download] 已自动释放 Transformer Discovery Service", ModBase.LogLevel.Developer, "出现错误");
						}
						else
						{
							if (mcLibToken._WrapperMap.Contains("optifine\\OptiFine"))
							{
								string text = mcLibToken._WrapperMap.Replace((mcLibToken.m_InfoMap ? JumpLoaderFolder : CustomMcFolder) + "libraries\\optifine\\OptiFine\\", "").Split("_")[0] + "/" + ModBase.GetFileNameFromPath(mcLibToken._WrapperMap).Replace("-", "_");
								text = "/maven/com/optifine/" + text;
								if (text.Contains("_pre"))
								{
									text = text.Replace("com/optifine/", "com/optifine/preview_");
								}
								list2.Add("https://bmclapi2.bangbang93.com" + text);
							}
							else if (list2.Count <= 2)
							{
								list2.AddRange(ModDownload.DlSourceLibraryGet("https://libraries.minecraft.net" + mcLibToken._WrapperMap.Replace((mcLibToken.m_InfoMap ? JumpLoaderFolder : CustomMcFolder) + "libraries", "").Replace("\\", "/")));
							}
							list.Add(new ModNet.NetFile(Enumerable.Distinct<string>(list2), mcLibToken._WrapperMap, fileChecker, false));
						}
					}
				}
			}
			finally
			{
				List<ModMinecraft.McLibToken>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			return list.Distinct((ModMinecraft._Closure$__.$I40-0 == null) ? (ModMinecraft._Closure$__.$I40-0 = ((ModNet.NetFile a, ModNet.NetFile b) => Operators.CompareString(a.m_AuthenticationTest, b.m_AuthenticationTest, false) == 0)) : ModMinecraft._Closure$__.$I40-0);
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0006B5C4 File Offset: 0x000697C4
		public static string McLibGet(string Original, bool WithHead = true, bool IgnoreLiteLoader = false, string CustomMcFolder = null)
		{
			CustomMcFolder = (CustomMcFolder ?? ModMinecraft.m_ProxyTests);
			string[] array = Original.Split(":");
			string text = string.Concat(new string[]
			{
				WithHead ? (CustomMcFolder + "libraries\\") : "",
				array[0].Replace(".", "\\"),
				"\\",
				array[1],
				"\\",
				array[2],
				"\\",
				array[1],
				"-",
				array[2],
				".jar"
			});
			if (text.Contains("optifine\\OptiFine\\1.12") && File.Exists(string.Concat(new string[]
			{
				CustomMcFolder,
				"libraries\\",
				array[0].Replace(".", "\\"),
				"\\",
				array[1],
				"\\",
				array[2],
				"\\",
				array[1],
				"-",
				array[2],
				"-installer.jar"
			})))
			{
				ModBase.Log("[Launch] 已将 " + Original + " 特判替换为对应的 Installer 文件", ModBase.LogLevel.Debug, "出现错误");
				text = text.Replace(".jar", "-installer.jar");
			}
			return text;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0006B71C File Offset: 0x0006991C
		public static object ShouldIgnoreFileCheck(ModMinecraft.McVersion Version)
		{
			return Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LaunchAdvanceAssets", null)) || Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("VersionAdvanceAssetsV2", Version)) || Conversions.ToBoolean(Operators.CompareObjectEqual(ModBase.m_IdentifierRepository.Get("VersionAdvanceAssets", Version), 2, false));
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0006B780 File Offset: 0x00069980
		public static JToken McAssetsGetIndex(ModMinecraft.McVersion Version, bool ReturnLegacyOnError = false, bool CheckURLEmpty = false)
		{
			try
			{
				JToken jtoken;
				for (;;)
				{
					jtoken = Version.NewThread()["assetIndex"];
					if (jtoken != null && jtoken["id"] != null)
					{
						break;
					}
					if (Version.NewThread()["assets"] != null)
					{
						Version.NewThread()["assets"].ToString();
					}
					if (CheckURLEmpty && jtoken["url"] != null)
					{
						goto IL_8E;
					}
					if (Operators.CompareString(Version.CallThread(), "", false) == 0)
					{
						goto IL_92;
					}
					Version = new ModMinecraft.McVersion(ModMinecraft.m_ProxyTests + "versions\\" + Version.CallThread());
				}
				return jtoken;
				IL_8E:
				return jtoken;
				IL_92:;
			}
			catch (Exception ex)
			{
			}
			if (!ReturnLegacyOnError)
			{
				throw new Exception("该版本不存在资源文件索引信息");
			}
			ModBase.Log("[Minecraft] 无法获取资源文件索引下载地址，使用默认的 legacy 下载地址", ModBase.LogLevel.Normal, "出现错误");
			return (JToken)ModBase.GetJson("{\r\n                \"id\": \"legacy\",\r\n                \"sha1\": \"c0fd82e8ce9fbc93119e40d96d5a4e62cfa3f729\",\r\n                \"size\": 134284,\r\n                \"url\": \"https://launchermeta.mojang.com/mc-staging/assets/legacy/c0fd82e8ce9fbc93119e40d96d5a4e62cfa3f729/legacy.json\",\r\n                \"totalSize\": 111220701\r\n            }");
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0006B870 File Offset: 0x00069A70
		public static string McAssetsGetIndexName(ModMinecraft.McVersion Version)
		{
			try
			{
				while (Version.NewThread()["assetIndex"] == null || Version.NewThread()["assetIndex"]["id"] == null)
				{
					if (Version.NewThread()["assets"] != null)
					{
						return Version.NewThread()["assets"].ToString();
					}
					if (Operators.CompareString(Version.CallThread(), "", false) == 0)
					{
						goto IL_CC;
					}
					Version = new ModMinecraft.McVersion(ModMinecraft.m_ProxyTests + "versions\\" + Version.CallThread());
				}
				return Version.NewThread()["assetIndex"]["id"].ToString();
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "获取资源文件索引名失败", ModBase.LogLevel.Debug, "出现错误");
			}
			IL_CC:
			return "legacy";
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0006B960 File Offset: 0x00069B60
		private static List<ModMinecraft.McAssetsToken> McAssetsListGet(ModMinecraft.McVersion Version)
		{
			string text = ModMinecraft.McAssetsGetIndexName(Version);
			List<ModMinecraft.McAssetsToken> result;
			try
			{
				if (!File.Exists(string.Format("{0}assets\\indexes\\{1}.json", ModMinecraft.m_ProxyTests, text)))
				{
					throw new FileNotFoundException("未找到 Asset Index", ModMinecraft.m_ProxyTests + "assets\\indexes\\" + text + ".json");
				}
				List<ModMinecraft.McAssetsToken> list = new List<ModMinecraft.McAssetsToken>();
				JObject jobject = (JObject)ModBase.GetJson(ModBase.ReadFile(string.Format("{0}assets\\indexes\\{1}.json", ModMinecraft.m_ProxyTests, text), null));
				try
				{
					foreach (JToken jtoken in jobject["objects"].Children())
					{
						JProperty jproperty = (JProperty)jtoken;
						string adapterMap;
						if (jobject["map_to_resources"] != null && jobject["map_to_resources"].ToObject<bool>())
						{
							adapterMap = Version.ChangeMapper() + "resources\\" + jproperty.Name.Replace("/", "\\");
						}
						else if (jobject["virtual"] != null && jobject["virtual"].ToObject<bool>())
						{
							adapterMap = ModMinecraft.m_ProxyTests + "assets\\virtual\\legacy\\" + jproperty.Name.Replace("/", "\\");
						}
						else
						{
							adapterMap = string.Concat(new string[]
							{
								ModMinecraft.m_ProxyTests,
								"assets\\objects\\",
								Strings.Left(jproperty.Value["hash"].ToString(), 2),
								"\\",
								jproperty.Value["hash"].ToString()
							});
						}
						list.Add(new ModMinecraft.McAssetsToken
						{
							m_AdapterMap = adapterMap,
							_FacadeMap = jproperty.Name,
							merchantMap = jproperty.Value["hash"].ToString(),
							listMap = Conversions.ToLong(jproperty.Value["size"].ToString())
						});
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
				result = list;
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "获取资源文件列表失败：" + text, ModBase.LogLevel.Debug, "出现错误");
				throw;
			}
			return result;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0006BBCC File Offset: 0x00069DCC
		public static List<ModNet.NetFile> McAssetsFixList(ModMinecraft.McVersion Version, bool CheckHash, ref ModLoader.LoaderBase ProgressFeed = null)
		{
			List<ModNet.NetFile> list = new List<ModNet.NetFile>();
			checked
			{
				try
				{
					List<ModMinecraft.McAssetsToken> list2 = ModMinecraft.McAssetsListGet(Version);
					if (ProgressFeed != null)
					{
						ProgressFeed.Progress = 0.04;
					}
					int num = list2.Count - 1;
					for (int i = 0; i <= num; i++)
					{
						ModMinecraft.McAssetsToken mcAssetsToken = list2[i];
						if (ProgressFeed != null)
						{
							ProgressFeed.Progress = unchecked(0.05 + 0.94 * (double)i / (double)list2.Count);
						}
						FileInfo fileInfo = new FileInfo(mcAssetsToken.m_AdapterMap);
						if (!fileInfo.Exists || (mcAssetsToken.listMap != 0L && mcAssetsToken.listMap != fileInfo.Length) || (CheckHash && mcAssetsToken.merchantMap != null && Operators.CompareString(mcAssetsToken.merchantMap, ModBase.smethod_1(mcAssetsToken.m_AdapterMap), false) != 0))
						{
							list.Add(new ModNet.NetFile(ModDownload.DlSourceResourceGet("https://resources.download.minecraft.net/" + Strings.Left(mcAssetsToken.merchantMap, 2) + "/" + mcAssetsToken.merchantMap), mcAssetsToken.m_AdapterMap, new ModBase.FileChecker(-1L, (mcAssetsToken.listMap == 0L) ? -1L : mcAssetsToken.listMap, mcAssetsToken.merchantMap, true, false), false));
						}
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "获取版本缺失的资源文件下载列表失败", ModBase.LogLevel.Debug, "出现错误");
				}
				if (ProgressFeed != null)
				{
					ProgressFeed.Progress = 0.99;
				}
				return list;
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0006BD60 File Offset: 0x00069F60
		public static void McDownloadClientUpdateHint(string VersionName, JObject Json)
		{
			ModMinecraft._Closure$__48-1 CS$<>8__locals1 = new ModMinecraft._Closure$__48-1(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_VersionName = VersionName;
			try
			{
				ModMinecraft._Closure$__48-0 CS$<>8__locals2 = new ModMinecraft._Closure$__48-0(CS$<>8__locals2);
				CS$<>8__locals2.$VB$Local_Version = null;
				try
				{
					foreach (JToken jtoken in Json["versions"])
					{
						if (jtoken["id"] != null && Operators.CompareString(jtoken["id"].ToString(), CS$<>8__locals1.$VB$Local_VersionName, false) == 0)
						{
							CS$<>8__locals2.$VB$Local_Version = jtoken;
							break;
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
				if (CS$<>8__locals2.$VB$Local_Version != null)
				{
					DateTime dateTime = (DateTime)CS$<>8__locals2.$VB$Local_Version["releaseTime"];
					if (ModMain.MyMsgBox(string.Format("新版本：{0}{1}", CS$<>8__locals1.$VB$Local_VersionName, "\r\n") + (((DateTime.Now - dateTime).TotalDays > 1.0) ? ("更新时间：" + dateTime.ToString()) : ("更新于：" + ModBase.GetTimeSpanString(dateTime - DateTime.Now, false))), "Minecraft 更新提示", "确定", "下载", ((DateTime.Now - dateTime).TotalHours > 3.0) ? "更新日志" : "", false, true, false, null, null, delegate
					{
						ModDownloadLib.McUpdateLogShow(CS$<>8__locals2.$VB$Local_Version);
					}) == 2)
					{
						ModBase.RunInUi(delegate()
						{
							PageDownloadInstall._ErrorReader = CS$<>8__locals1.$VB$Local_VersionName;
							ModMain._ProcessIterator.PageChange(FormMain.PageType.Download, FormMain.PageSubType.DownloadInstall);
						}, false);
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "Minecraft 更新提示发送失败（" + (CS$<>8__locals1.$VB$Local_VersionName ?? "Nothing") + "）", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00008F5F File Offset: 0x0000715F
		public static bool VersionSortBoolean(string Left, string Right)
		{
			return ModMinecraft.VersionSortInteger(Left, Right) >= 0;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0006BF50 File Offset: 0x0006A150
		public static int VersionSortInteger(string Left, string Right)
		{
			if (Operators.CompareString(Left, "未知版本", false) == 0 || Operators.CompareString(Right, "未知版本", false) == 0)
			{
				if (Operators.CompareString(Left, "未知版本", false) == 0 && Operators.CompareString(Right, "未知版本", false) != 0)
				{
					return 1;
				}
				if (Operators.CompareString(Left, "未知版本", false) == 0 && Operators.CompareString(Right, "未知版本", false) == 0)
				{
					return 0;
				}
				if (Operators.CompareString(Left, "未知版本", false) != 0 && Operators.CompareString(Right, "未知版本", false) == 0)
				{
					return -1;
				}
			}
			Left = Left.ToLowerInvariant();
			Right = Right.ToLowerInvariant();
			List<string> list = ModBase.RegexSearch(Left.Replace("快照", "snapshot").Replace("预览版", "pre"), "[a-z]+|[0-9]+", 0);
			List<string> list2 = ModBase.RegexSearch(Right.Replace("快照", "snapshot").Replace("预览版", "pre"), "[a-z]+|[0-9]+", 0);
			int num = 0;
			checked
			{
				while (list.Count - 1 >= num || list2.Count - 1 >= num)
				{
					string text = (list.Count - 1 < num) ? "-1" : list[num];
					string text2 = (list2.Count - 1 < num) ? "-1" : list2[num];
					if (Operators.CompareString(text, text2, false) != 0)
					{
						if (Operators.CompareString(text, "pre", false) == 0 || Operators.CompareString(text, "snapshot", false) == 0)
						{
							text = "-3";
						}
						if (Operators.CompareString(text, "rc", false) == 0)
						{
							text = "-2";
						}
						if (Operators.CompareString(text, "experimental", false) == 0)
						{
							text = "-4";
						}
						double num2 = ModBase.Val(text);
						if (Operators.CompareString(text2, "pre", false) == 0 || Operators.CompareString(text2, "snapshot", false) == 0)
						{
							text2 = "-3";
						}
						if (Operators.CompareString(text2, "rc", false) == 0)
						{
							text2 = "-2";
						}
						if (Operators.CompareString(text2, "experimental", false) == 0)
						{
							text2 = "-4";
						}
						double num3 = ModBase.Val(text2);
						if (num2 == 0.0 && num3 == 0.0)
						{
							if (Operators.CompareString(text, text2, false) > 0)
							{
								return 1;
							}
							if (Operators.CompareString(text, text2, false) < 0)
							{
								return -1;
							}
						}
						else
						{
							if (num2 > num3)
							{
								return 1;
							}
							if (num2 < num3)
							{
								return -1;
							}
						}
					}
					num++;
				}
				int result;
				if (Operators.CompareString(Left, Right, false) > 0)
				{
					result = 1;
				}
				else if (Operators.CompareString(Left, Right, false) < 0)
				{
					result = -1;
				}
				else
				{
					result = 0;
				}
				return result;
			}
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0006C1E4 File Offset: 0x0006A3E4
		public static string AccountFilter(string Account)
		{
			string result;
			if (Account.Contains("@"))
			{
				string[] array = Account.Split("@");
				result = "".PadLeft(Enumerable.Count<char>(array[0]), '*') + "@" + array[1];
			}
			else if (Enumerable.Count<char>(Account) >= 6)
			{
				result = Strings.Mid(Account, 1, checked(Enumerable.Count<char>(Account) - 4)) + "****";
			}
			else
			{
				result = "".PadLeft(Enumerable.Count<char>(Account), '*');
			}
			return result;
		}

		// Token: 0x040007B0 RID: 1968
		public static string m_ProxyTests;

		// Token: 0x040007B1 RID: 1969
		public static List<ModMinecraft.McFolder> messageTests = new List<ModMinecraft.McFolder>();

		// Token: 0x040007B2 RID: 1970
		public static ModLoader.LoaderTask<int, int> m_CreatorTests = new ModLoader.LoaderTask<int, int>("Minecraft Folder List", delegate(ModLoader.LoaderTask<int, int> a0)
		{
			ModMinecraft.McFolderListLoadSub();
		}, null, ThreadPriority.AboveNormal);

		// Token: 0x040007B3 RID: 1971
		private static ModMinecraft.McVersion initializerTests;

		// Token: 0x040007B4 RID: 1972
		private static ModMinecraft.McVersion _SingletonTests = 0;

		// Token: 0x040007B5 RID: 1973
		public static Dictionary<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>> _RegTests = new Dictionary<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>>();

		// Token: 0x040007B6 RID: 1974
		public static bool m_ProductTests = false;

		// Token: 0x040007B7 RID: 1975
		public static ModLoader.LoaderTask<string, int> _ListenerTests = new ModLoader.LoaderTask<string, int>("Minecraft Version List", new Action<ModLoader.LoaderTask<string, int>>(ModMinecraft.McVersionListLoad), null, ThreadPriority.Normal)
		{
			ReloadTimeout = 1
		};

		// Token: 0x040007B8 RID: 1976
		private static bool collectionTests = true;

		// Token: 0x040007B9 RID: 1977
		private static readonly object visitorTests = RuntimeHelpers.GetObjectValue(new object());

		// Token: 0x040007BA RID: 1978
		private static string valueTests = MyWpfExtension.ManageParser().Info.OSVersion;

		// Token: 0x0200016F RID: 367
		public class McFolder
		{
			// Token: 0x06000E75 RID: 3701 RVA: 0x0006C268 File Offset: 0x0006A468
			public override bool Equals(object obj)
			{
				bool result;
				if (!(obj is ModMinecraft.McFolder))
				{
					result = false;
				}
				else
				{
					ModMinecraft.McFolder mcFolder = (ModMinecraft.McFolder)obj;
					result = (Operators.CompareString(this.Name, mcFolder.Name, false) == 0 && Operators.CompareString(this.Path, mcFolder.Path, false) == 0 && this.Type == mcFolder.Type);
				}
				return result;
			}

			// Token: 0x06000E76 RID: 3702 RVA: 0x00008F6E File Offset: 0x0000716E
			public override string ToString()
			{
				return this.Path;
			}

			// Token: 0x040007BB RID: 1979
			public string Name;

			// Token: 0x040007BC RID: 1980
			public string Path;

			// Token: 0x040007BD RID: 1981
			public ModMinecraft.McFolderType Type;
		}

		// Token: 0x02000170 RID: 368
		public enum McFolderType
		{
			// Token: 0x040007BF RID: 1983
			Original,
			// Token: 0x040007C0 RID: 1984
			RenamedOriginal,
			// Token: 0x040007C1 RID: 1985
			Custom
		}

		// Token: 0x02000171 RID: 369
		public class McVersion
		{
			// Token: 0x17000219 RID: 537
			// (get) Token: 0x06000E78 RID: 3704 RVA: 0x00008F76 File Offset: 0x00007176
			public string Path { get; }

			// Token: 0x06000E79 RID: 3705 RVA: 0x00008F7E File Offset: 0x0000717E
			public string ChangeMapper()
			{
				return this.GetPathIndie(this.RunThread());
			}

			// Token: 0x06000E7A RID: 3706 RVA: 0x0006C2C4 File Offset: 0x0006A4C4
			public string GetPathIndie(bool Modable)
			{
				int num = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("LaunchArgumentIndie", null));
				object left = ModBase.m_IdentifierRepository.Get("VersionArgumentIndie", this);
				if (Operators.ConditionalCompareObjectEqual(left, -1, false))
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(this.Path + "mods\\");
					DirectoryInfo directoryInfo2 = new DirectoryInfo(this.Path + "saves\\");
					if ((directoryInfo.Exists && Enumerable.Any<FileInfo>(directoryInfo.EnumerateFiles())) || (directoryInfo2.Exists && Enumerable.Any<FileInfo>(directoryInfo2.EnumerateFiles())))
					{
						ModBase.m_IdentifierRepository.Set("VersionArgumentIndie", 1, false, this);
						ModBase.Log("[Setup] 已自动开启单版本隔离：" + this.Name, ModBase.LogLevel.Normal, "出现错误");
						goto IL_1BF;
					}
					ModBase.m_IdentifierRepository.Set("VersionArgumentIndie", 0, false, this);
					ModBase.Log("[Setup] 版本隔离使用全局设置：" + this.Name, ModBase.LogLevel.Normal, "出现错误");
				}
				else if (!Operators.ConditionalCompareObjectEqual(left, 0, false))
				{
					if (Operators.ConditionalCompareObjectEqual(left, 1, false))
					{
						goto IL_1BF;
					}
					if (Operators.ConditionalCompareObjectEqual(left, 2, false))
					{
						goto IL_1AE;
					}
				}
				switch (num)
				{
				case 1:
					if (Modable)
					{
						return this.Path;
					}
					break;
				case 2:
					if (this._ConfigurationMap == ModMinecraft.McVersionState.Fool || this._ConfigurationMap == ModMinecraft.McVersionState.Old || this._ConfigurationMap == ModMinecraft.McVersionState.Snapshot)
					{
						return this.Path;
					}
					break;
				case 3:
					if (Modable)
					{
						return this.Path;
					}
					if (this._ConfigurationMap != ModMinecraft.McVersionState.Fool && this._ConfigurationMap != ModMinecraft.McVersionState.Old)
					{
						if (this._ConfigurationMap != ModMinecraft.McVersionState.Snapshot)
						{
							break;
						}
					}
					return this.Path;
				case 4:
					IL_1BF:
					return this.Path;
				}
				IL_1AE:
				return ModMinecraft.m_ProxyTests;
			}

			// Token: 0x1700021A RID: 538
			// (get) Token: 0x06000E7B RID: 3707 RVA: 0x00008F8C File Offset: 0x0000718C
			public string Name
			{
				get
				{
					if (this.taskMap == null && Operators.CompareString(this.Path, "", false) != 0)
					{
						this.taskMap = ModBase.GetFolderNameFromPath(this.Path);
					}
					return this.taskMap;
				}
			}

			// Token: 0x06000E7C RID: 3708 RVA: 0x0006C498 File Offset: 0x0006A698
			public bool RunThread()
			{
				if (!this.importerMap)
				{
					this.Load();
				}
				return this.Version._CandidateMap || this.Version.m_StructMap || this.Version._AccountMap || this.Version._ValMap || this.expressionMap == ModMinecraft.McVersionCardType.API;
			}

			// Token: 0x1700021B RID: 539
			// (get) Token: 0x06000E7D RID: 3709 RVA: 0x0006C4F4 File Offset: 0x0006A6F4
			// (set) Token: 0x06000E7E RID: 3710 RVA: 0x00008FC0 File Offset: 0x000071C0
			public ModMinecraft.McVersionInfo Version
			{
				get
				{
					if (this.m_WriterMap == null)
					{
						this.m_WriterMap = new ModMinecraft.McVersionInfo();
						try
						{
							try
							{
								if (this.NewThread()["releaseTime"] == null)
								{
									this._RegistryMap = new DateTime(1970, 1, 1, 15, 0, 0);
								}
								else
								{
									this._RegistryMap = this.NewThread()["releaseTime"].ToObject<DateTime>();
								}
								if (this._RegistryMap.Year > 2000 && this._RegistryMap.Year < 2013)
								{
									this.m_WriterMap._ConnectionMap = "Old";
									goto IL_648;
								}
							}
							catch (Exception ex)
							{
								this._RegistryMap = new DateTime(1970, 1, 1, 15, 0, 0);
							}
							if ((string)(this.NewThread()["type"] ?? "") == "pending")
							{
								this.m_WriterMap._ConnectionMap = "pending";
							}
							else
							{
								if (this.PrepareThread())
								{
									try
									{
										this.m_WriterMap._ConnectionMap = (string)this.NewThread()["jumploader"]["jars"]["minecraft"][0]["gameVersion"];
										goto IL_648;
									}
									catch (Exception ex2)
									{
									}
								}
								if (this.NewThread()["clientVersion"] != null)
								{
									this.m_WriterMap._ConnectionMap = (string)this.NewThread()["clientVersion"];
								}
								else
								{
									if (this.NewThread()["patches"] != null)
									{
										try
										{
											foreach (JToken jtoken in this.NewThread()["patches"])
											{
												JObject jobject = (JObject)jtoken;
												if (Operators.CompareString((jobject["id"] ?? "").ToString(), "game", false) == 0 && jobject["version"] != null)
												{
													this.m_WriterMap._ConnectionMap = jobject["version"].ToString();
													goto IL_648;
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
									}
									if (this.NewThread()["arguments"] != null && this.NewThread()["arguments"]["game"] != null)
									{
										bool flag = false;
										try
										{
											foreach (JToken jtoken2 in this.NewThread()["arguments"]["game"])
											{
												if (flag)
												{
													this.m_WriterMap._ConnectionMap = jtoken2.ToString();
													goto IL_648;
												}
												if (Operators.CompareString(jtoken2.ToString(), "--fml.mcVersion", false) == 0)
												{
													flag = true;
												}
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
									if (Operators.CompareString(this.CallThread(), "", false) != 0)
									{
										this.m_WriterMap._ConnectionMap = (this.NewThread()["jar"] ?? "").ToString();
										if (Operators.CompareString(this.m_WriterMap._ConnectionMap, "", false) == 0)
										{
											this.m_WriterMap._ConnectionMap = this.CallThread();
										}
									}
									else
									{
										string text = ModBase.RegexSeek((this.NewThread()["downloads"] ?? "").ToString(), "(?<=launcher.mojang.com/mc/game/)[^/]*", 0);
										if (text != null)
										{
											this.m_WriterMap._ConnectionMap = text;
										}
										else
										{
											string str = this.NewThread()["libraries"].ToString();
											text = (ModBase.RegexSeek(str, "(?<=net.minecraftforge:forge:)1.[0-9+.]+", 0) ?? ModBase.RegexSeek(str, "(?<=net.minecraftforge:fmlloader:)1.[0-9+.]+", 0));
											if (text != null)
											{
												this.m_WriterMap._ConnectionMap = text;
											}
											else
											{
												text = ModBase.RegexSeek(str, "(?<=optifine:OptiFine:)1.[0-9+.]+", 0);
												if (text != null)
												{
													this.m_WriterMap._ConnectionMap = text;
												}
												else
												{
													text = ModBase.RegexSeek(str, "(?<=((fabricmc)|(quiltmc)):intermediary:)[^\"]*", 0);
													if (text != null)
													{
														this.m_WriterMap._ConnectionMap = text;
													}
													else if (this.NewThread()["jar"] != null)
													{
														this.m_WriterMap._ConnectionMap = this.NewThread()["jar"].ToString();
													}
													else
													{
														if (File.Exists(this.Path + this.Name + ".jar"))
														{
															try
															{
																using (ZipArchive zipArchive = new ZipArchive(new FileStream(this.Path + this.Name + ".jar", FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
																{
																	ZipArchiveEntry entry = zipArchive.GetEntry("version.json");
																	if (entry != null)
																	{
																		using (StreamReader streamReader = new StreamReader(entry.Open()))
																		{
																			JObject jobject2 = (JObject)ModBase.GetJson(streamReader.ReadToEnd());
																			if (jobject2["id"] != null)
																			{
																				string text2 = jobject2["id"].ToString();
																				if (text2.Length < 32)
																				{
																					this.m_WriterMap._ConnectionMap = text2;
																					ModBase.Log("[Minecraft] 从版本 jar 中的 version.json 获取到版本号：" + text2, ModBase.LogLevel.Normal, "出现错误");
																					goto IL_648;
																				}
																			}
																		}
																	}
																}
															}
															catch (Exception ex3)
															{
																ModBase.Log(ex3, "从版本 jar 中的 version.json 获取版本号失败", ModBase.LogLevel.Debug, "出现错误");
															}
														}
														ModBase.Log("[Minecraft] 无法完全确认 MC 版本号的版本：" + this.Name, ModBase.LogLevel.Normal, "出现错误");
														text = ModBase.RegexSeek(this.Name, "([0-9w]{5}[a-z]{1})|(1\\.[0-9]+(\\.[0-9]+)?(-(pre|rc)[1-9]?| Pre-Release( [1-9]{1})?)?)", 1);
														if (text != null)
														{
															this.m_WriterMap._ConnectionMap = text;
														}
														else
														{
															JObject jobject3 = (JObject)this.NewThread().DeepClone();
															jobject3.Remove("libraries");
															text = ModBase.RegexSeek(jobject3.ToString(), "([0-9w]{5}[a-z]{1})|(1\\.[0-9]+(\\.[0-9]+)?(-(pre|rc)[1-9]?| Pre-Release( [1-9]{1})?)?)", 1);
															if (text != null)
															{
																this.m_WriterMap._ConnectionMap = text;
															}
															else
															{
																this.m_WriterMap._ConnectionMap = "Unknown";
																this._ConsumerMap = "PCL 无法识别该版本的 MC 版本号";
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						catch (Exception ex4)
						{
							ModBase.Log(ex4, "识别 Minecraft 版本时出错", ModBase.LogLevel.Debug, "出现错误");
							this.m_WriterMap._ConnectionMap = "Unknown";
							this._ConsumerMap = "无法识别：" + ex4.Message;
						}
						IL_648:
						if (this.m_WriterMap._ConnectionMap.StartsWithF("1.", false))
						{
							string[] array = this.m_WriterMap._ConnectionMap.Split(new char[]
							{
								' ',
								'_',
								'-',
								'.'
							});
							string text3 = (Enumerable.Count<string>(array) >= 2) ? array[1] : "0";
							this.m_WriterMap._ServerMap = Conversions.ToInteger((text3.Length <= 2) ? ModBase.Val(text3) : "0");
							text3 = ((Enumerable.Count<string>(array) >= 3) ? array[2] : "0");
							this.m_WriterMap.m_ResolverMap = Conversions.ToInteger((text3.Length <= 2) ? ModBase.Val(text3) : "0");
						}
						else if (this.m_WriterMap._ConnectionMap.Contains("w") || Operators.CompareString(this.m_WriterMap._ConnectionMap, "pending", false) == 0)
						{
							this.m_WriterMap._ServerMap = 99;
							this.m_WriterMap.m_ResolverMap = 99;
						}
					}
					return this.m_WriterMap;
				}
				set
				{
					this.m_WriterMap = value;
				}
			}

			// Token: 0x06000E7F RID: 3711 RVA: 0x0006CD24 File Offset: 0x0006AF24
			public string AwakeThread()
			{
				VB$AnonymousDelegate_8<string, bool> vb$AnonymousDelegate_ = (ModMinecraft.McVersion._Closure$__.$I22-0 == null) ? (ModMinecraft.McVersion._Closure$__.$I22-0 = delegate(string Json)
				{
					string str = Json.Trim();
					return str.StartsWithF("{", false) && str.EndsWithF("}", false);
				}) : ModMinecraft.McVersion._Closure$__.$I22-0;
				if (this.m_RuleMap == null)
				{
					if (!File.Exists(this.Path + this.Name + ".json"))
					{
						throw new Exception(string.Format("未找到版本 JSON 文件：{0}{1}.json", this.Path, this.Name));
					}
					this.m_RuleMap = ModBase.ReadFile(this.Path + this.Name + ".json", null);
					if (!vb$AnonymousDelegate_(this.m_RuleMap))
					{
						if (ModBase.RunInUi())
						{
							ModBase.Log("[Minecraft] 版本 JSON 文件为空或有误，由于代码在主线程运行，将不再进行重试", ModBase.LogLevel.Debug, "出现错误");
							ModBase.GetJson(this.m_RuleMap);
						}
						else
						{
							ModBase.Log("[Minecraft] 版本 JSON 文件为空或有误，将在 2s 后重试读取（" + this.Path + this.Name + ".json）", ModBase.LogLevel.Debug, "出现错误");
							Thread.Sleep(2000);
							this.m_RuleMap = ModBase.ReadFile(this.Path + this.Name + ".json", null);
							if (!vb$AnonymousDelegate_(this.m_RuleMap))
							{
								ModBase.GetJson(this.m_RuleMap);
							}
						}
					}
				}
				return this.m_RuleMap;
			}

			// Token: 0x06000E80 RID: 3712 RVA: 0x00008FC9 File Offset: 0x000071C9
			public void FillThread(string value)
			{
				this.m_RuleMap = value;
			}

			// Token: 0x06000E81 RID: 3713 RVA: 0x0006CE64 File Offset: 0x0006B064
			public JObject NewThread()
			{
				if (this.proccesorMap == null)
				{
					string text = this.AwakeThread();
					try
					{
						this.proccesorMap = (JObject)ModBase.GetJson(text);
						if (this.proccesorMap.ContainsKey("patches") && !this.proccesorMap.ContainsKey("time"))
						{
							this.SetupThread(true);
							JObject jobject = null;
							List<JObject> list = new List<JObject>();
							try
							{
								foreach (JToken jtoken in this.proccesorMap["patches"])
								{
									JObject item = (JObject)jtoken;
									list.Add(item);
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
							list = list.Sort((ModMinecraft.McVersion._Closure$__.$I26-0 == null) ? (ModMinecraft.McVersion._Closure$__.$I26-0 = ((JObject Left, JObject Right) => ModBase.Val((Left["priority"] ?? "0").ToString()) < ModBase.Val((Right["priority"] ?? "0").ToString()))) : ModMinecraft.McVersion._Closure$__.$I26-0);
							try
							{
								foreach (JObject jobject2 in list)
								{
									string text2 = (string)jobject2["id"];
									if (text2 != null)
									{
										ModBase.Log("[Minecraft] 合并 HMCL 分支项：" + text2, ModBase.LogLevel.Normal, "出现错误");
										if (jobject != null)
										{
											jobject.Merge(jobject2);
										}
										else
										{
											jobject = jobject2;
										}
									}
									else
									{
										ModBase.Log("[Minecraft] 存在为空的 HMCL 分支项", ModBase.LogLevel.Normal, "出现错误");
									}
								}
							}
							finally
							{
								List<JObject>.Enumerator enumerator2;
								((IDisposable)enumerator2).Dispose();
							}
							this.proccesorMap = jobject;
							this.proccesorMap["id"] = this.Name;
							if (this.proccesorMap.ContainsKey("inheritsFrom"))
							{
								this.proccesorMap.Remove("inheritsFrom");
							}
						}
						object obj = null;
						try
						{
							obj = ((this.proccesorMap["inheritsFrom"] == null) ? "" : this.proccesorMap["inheritsFrom"].ToString());
							if (Operators.ConditionalCompareObjectEqual(obj, this.Name, false))
							{
								ModBase.Log("[Minecraft] 自引用的继承版本：" + this.Name, ModBase.LogLevel.Debug, "出现错误");
								obj = "";
							}
							else
							{
								while (Operators.ConditionalCompareObjectNotEqual(obj, "", false))
								{
									ModMinecraft.McVersion mcVersion = new ModMinecraft.McVersion(Conversions.ToString(obj));
									if (Operators.ConditionalCompareObjectEqual(mcVersion.CallThread(), obj, false))
									{
										throw new Exception(Conversions.ToString(Operators.ConcatenateObject("版本依赖项出现嵌套：", obj)));
									}
									obj = mcVersion.CallThread();
									mcVersion.NewThread().Merge(this.proccesorMap);
									this.proccesorMap = mcVersion.NewThread();
								}
							}
						}
						catch (Exception ex)
						{
							ModBase.Log(ex, "合并版本依赖项 JSON 失败（" + (obj ?? "null").ToString() + "）", ModBase.LogLevel.Debug, "出现错误");
						}
					}
					catch (Exception innerException)
					{
						throw new Exception("初始化版本 JSON 时失败（" + (this.Name ?? "null") + "）", innerException);
					}
					try
					{
						if (text.Contains("minecraftforge") && File.Exists(this.ChangeMapper() + "config\\jumploader.json"))
						{
							try
							{
								foreach (string filePath in Directory.EnumerateFiles(this.ChangeMapper() + "mods"))
								{
									string fileNameFromPath = ModBase.GetFileNameFromPath(filePath);
									if (fileNameFromPath.EndsWithF(".jar", true) && fileNameFromPath.ContainsF("jumploader", true))
									{
										ModBase.Log("[Minecraft] 发现 JumpLoader 分支项：" + fileNameFromPath, ModBase.LogLevel.Normal, "出现错误");
										this.CalculateThread(true);
										break;
									}
								}
							}
							finally
							{
								IEnumerator<string> enumerator3;
								if (enumerator3 != null)
								{
									enumerator3.Dispose();
								}
							}
						}
						if (this.PrepareThread())
						{
							this.proccesorMap.Remove("jumploader");
							this.proccesorMap.Add("jumploader", (JToken)ModBase.GetJson(ModBase.ReadFile(this.ChangeMapper() + "config\\jumploader.json", null)));
						}
					}
					catch (Exception ex2)
					{
						ModBase.Log(ex2, "处理 JumpLoader 失败", ModBase.LogLevel.Debug, "出现错误");
					}
				}
				return this.proccesorMap;
			}

			// Token: 0x06000E82 RID: 3714 RVA: 0x00008FD2 File Offset: 0x000071D2
			public void ComputeThread(JObject value)
			{
				this.proccesorMap = value;
			}

			// Token: 0x06000E83 RID: 3715 RVA: 0x00008FDB File Offset: 0x000071DB
			public bool CloneThread()
			{
				return this.NewThread()["minecraftArguments"] != null && (string)this.NewThread()["minecraftArguments"] != "";
			}

			// Token: 0x06000E84 RID: 3716 RVA: 0x00009010 File Offset: 0x00007210
			[CompilerGenerated]
			public bool InvokeThread()
			{
				return this.m_SetterMap;
			}

			// Token: 0x06000E85 RID: 3717 RVA: 0x00009018 File Offset: 0x00007218
			[CompilerGenerated]
			public void SetupThread(bool AutoPropertyValue)
			{
				this.m_SetterMap = AutoPropertyValue;
			}

			// Token: 0x06000E86 RID: 3718 RVA: 0x00009021 File Offset: 0x00007221
			[CompilerGenerated]
			public bool PrepareThread()
			{
				return this._FactoryMap;
			}

			// Token: 0x06000E87 RID: 3719 RVA: 0x00009029 File Offset: 0x00007229
			[CompilerGenerated]
			public void CalculateThread(bool AutoPropertyValue)
			{
				this._FactoryMap = AutoPropertyValue;
			}

			// Token: 0x06000E88 RID: 3720 RVA: 0x0006D2F0 File Offset: 0x0006B4F0
			public string CallThread()
			{
				if (this.m_ExporterMap == null)
				{
					this.m_ExporterMap = (this.NewThread()["inheritsFrom"] ?? "").ToString();
					if (this.AwakeThread().Contains("liteloader") && Operators.CompareString(this.Version._ConnectionMap, this.Name, false) != 0 && !this.AwakeThread().Contains("logging") && Operators.CompareString((this.NewThread()["jar"] ?? this.Version._ConnectionMap).ToString(), this.Version._ConnectionMap, false) == 0)
					{
						this.m_ExporterMap = this.Version._ConnectionMap;
					}
					if (this.InvokeThread())
					{
						this.m_ExporterMap = "";
					}
				}
				return this.m_ExporterMap;
			}

			// Token: 0x06000E89 RID: 3721 RVA: 0x0006D3D4 File Offset: 0x0006B5D4
			public McVersion(string Path)
			{
				this.taskMap = null;
				this._ConsumerMap = "该版本未被加载，请向作者反馈此问题";
				this._ConfigurationMap = ModMinecraft.McVersionState.Error;
				this._TokenMap = false;
				this.expressionMap = ModMinecraft.McVersionCardType.Auto;
				this.m_WriterMap = null;
				this._RegistryMap = new DateTime(1970, 1, 1, 15, 0, 0);
				this.m_RuleMap = null;
				this.proccesorMap = null;
				this.SetupThread(false);
				this.CalculateThread(false);
				this.m_ExporterMap = null;
				this.importerMap = false;
				this.Path = (Path.Contains(":") ? "" : (ModMinecraft.m_ProxyTests + "versions\\")) + Path + (Path.EndsWithF("\\", false) ? "" : "\\");
			}

			// Token: 0x06000E8A RID: 3722 RVA: 0x0006D4A0 File Offset: 0x0006B6A0
			public bool Check()
			{
				bool result;
				if (!Directory.Exists(this.Path))
				{
					this._ConfigurationMap = ModMinecraft.McVersionState.Error;
					this._ConsumerMap = "未找到版本 " + this.Name;
					result = false;
				}
				else
				{
					try
					{
						Directory.CreateDirectory(this.Path + "PCL\\");
						ModBase.CheckPermissionWithException(this.Path + "PCL\\");
					}
					catch (Exception ex)
					{
						this._ConfigurationMap = ModMinecraft.McVersionState.Error;
						this._ConsumerMap = "PCL 没有对该文件夹的访问权限，请右键以管理员身份运行 PCL";
						ModBase.Log(ex, "没有访问版本文件夹的权限", ModBase.LogLevel.Debug, "出现错误");
						return false;
					}
					try
					{
						this.NewThread();
					}
					catch (Exception ex2)
					{
						ModBase.Log(ex2, "版本 JSON 可用性检查失败（" + this.Path + "）", ModBase.LogLevel.Debug, "出现错误");
						this.FillThread("");
						this.ComputeThread(null);
						this._ConsumerMap = ex2.Message;
						this._ConfigurationMap = ModMinecraft.McVersionState.Error;
						return false;
					}
					try
					{
						if (Operators.CompareString(this.CallThread(), "", false) != 0 && !File.Exists(string.Concat(new string[]
						{
							ModBase.GetPathFromFullPath(this.Path),
							this.CallThread(),
							"\\",
							this.CallThread(),
							".json"
						})))
						{
							this._ConfigurationMap = ModMinecraft.McVersionState.Error;
							this._ConsumerMap = "需要安装 " + this.CallThread() + " 作为前置版本";
							return false;
						}
					}
					catch (Exception ex3)
					{
						ModBase.Log(ex3, "依赖版本检查出错（" + this.Name + "）", ModBase.LogLevel.Debug, "出现错误");
						this._ConfigurationMap = ModMinecraft.McVersionState.Error;
						this._ConsumerMap = "未知错误：" + ModBase.GetExceptionSummary(ex3);
						return false;
					}
					this._ConfigurationMap = ModMinecraft.McVersionState.Original;
					result = true;
				}
				return result;
			}

			// Token: 0x06000E8B RID: 3723 RVA: 0x0006D6A4 File Offset: 0x0006B8A4
			public ModMinecraft.McVersion Load()
			{
				try
				{
					if (this.Check())
					{
						string connectionMap = this.Version._ConnectionMap;
						if (Operators.CompareString(connectionMap, "Unknown", false) != 0)
						{
							if (Operators.CompareString(connectionMap, "Old", false) != 0)
							{
								string text = (this.NewThread() ?? this.AwakeThread()).ToString();
								if (Operators.CompareString((this.NewThread()["type"] ?? "").ToString(), "fool", false) != 0 && Operators.CompareString(ModMinecraft.GetMcFoolName(this.Version._ConnectionMap), "", false) == 0)
								{
									if (this.Version._ConnectionMap.ContainsF("w", true) || this.Name.ContainsF("combat", true) || this.Version._ConnectionMap.ContainsF("rc", true) || this.Version._ConnectionMap.ContainsF("pre", true) || this.Version._ConnectionMap.ContainsF("experimental", true) || Operators.CompareString((this.NewThread()["type"] ?? "").ToString(), "snapshot", false) == 0 || Operators.CompareString((this.NewThread()["type"] ?? "").ToString(), "pending", false) == 0)
									{
										this._ConfigurationMap = ModMinecraft.McVersionState.Snapshot;
									}
								}
								else
								{
									this._ConfigurationMap = ModMinecraft.McVersionState.Fool;
								}
								if (text.Contains("optifine"))
								{
									this._ConfigurationMap = ModMinecraft.McVersionState.OptiFine;
									this.Version._StatusMap = true;
									this.Version._RoleMap = (ModBase.RegexSeek(text, "(?<=HD_U_)[^\":/]+", 0) ?? "未知版本");
								}
								if (text.Contains("liteloader"))
								{
									this._ConfigurationMap = ModMinecraft.McVersionState.LiteLoader;
									this.Version._AccountMap = true;
								}
								if (!text.Contains("net.fabricmc:fabric-loader") && !text.Contains("org.quiltmc:quilt-loader"))
								{
									if (text.Contains("minecraftforge") && !text.Contains("net.neoforge"))
									{
										this._ConfigurationMap = ModMinecraft.McVersionState.Forge;
										this.Version.m_StructMap = true;
										this.Version.printerMap = ModBase.RegexSeek(text, "(?<=forge:[0-9\\.]+(_pre[0-9]*)?\\-)[0-9\\.]+", 0);
										if (this.Version.printerMap == null)
										{
											this.Version.printerMap = ModBase.RegexSeek(text, "(?<=net\\.minecraftforge:minecraftforge:)[0-9\\.]+", 0);
										}
										if (this.Version.printerMap == null)
										{
											this.Version.printerMap = (ModBase.RegexSeek(text, "(?<=net\\.minecraftforge:fmlloader:[0-9\\.]+-)[0-9\\.]+", 0) ?? "未知版本");
										}
									}
									else if (text.Contains("net.neoforge"))
									{
										this._ConfigurationMap = ModMinecraft.McVersionState.NeoForge;
										this.Version._ValMap = true;
										this.Version.m_AttrMap = (ModBase.RegexSeek(text, "(?<=orgeVersion\",[^\"]*?\")[^\"]+(?=\",)", 0) ?? "未知版本");
									}
								}
								else
								{
									this._ConfigurationMap = ModMinecraft.McVersionState.Fabric;
									this.Version._CandidateMap = true;
									this.Version._AdvisorMap = (ModBase.RegexSeek(text, "(?<=(net.fabricmc:fabric-loader:)|(org.quiltmc:quilt-loader:))[0-9\\.]+(\\+build.[0-9]+)?", 0) ?? "未知版本").Replace("+build", "");
								}
								this.Version.workerMap = true;
							}
							else
							{
								this._ConfigurationMap = ModMinecraft.McVersionState.Old;
							}
						}
						else
						{
							this._ConfigurationMap = ModMinecraft.McVersionState.Error;
						}
					}
					this.getterMap = ModBase.ReadIni(this.Path + "PCL\\Setup.ini", "Logo", "");
					if (Operators.CompareString(this.getterMap, "", false) == 0 || !Conversions.ToBoolean(ModBase.ReadIni(this.Path + "PCL\\Setup.ini", "LogoCustom", Conversions.ToString(false))))
					{
						switch (this._ConfigurationMap)
						{
						case ModMinecraft.McVersionState.Original:
							this.getterMap = ModBase.m_SerializerRepository + "Blocks/Grass.png";
							break;
						case ModMinecraft.McVersionState.Snapshot:
							this.getterMap = ModBase.m_SerializerRepository + "Blocks/CommandBlock.png";
							break;
						case ModMinecraft.McVersionState.Fool:
							this.getterMap = ModBase.m_SerializerRepository + "Blocks/GoldBlock.png";
							break;
						case ModMinecraft.McVersionState.OptiFine:
							this.getterMap = ModBase.m_SerializerRepository + "Blocks/GrassPath.png";
							break;
						case ModMinecraft.McVersionState.Old:
							this.getterMap = ModBase.m_SerializerRepository + "Blocks/CobbleStone.png";
							break;
						case ModMinecraft.McVersionState.Forge:
							this.getterMap = ModBase.m_SerializerRepository + "Blocks/Anvil.png";
							break;
						case ModMinecraft.McVersionState.NeoForge:
							this.getterMap = ModBase.m_SerializerRepository + "Blocks/NeoForge.png";
							break;
						case ModMinecraft.McVersionState.LiteLoader:
							this.getterMap = ModBase.m_SerializerRepository + "Blocks/Egg.png";
							break;
						case ModMinecraft.McVersionState.Fabric:
							this.getterMap = ModBase.m_SerializerRepository + "Blocks/Fabric.png";
							break;
						default:
							this.getterMap = ModBase.m_SerializerRepository + "Blocks/RedstoneBlock.png";
							break;
						}
					}
					string text2 = ModBase.ReadIni(this.Path + "PCL\\Setup.ini", "CustomInfo", "");
					if (Operators.CompareString(text2, "", false) == 0)
					{
						switch (this._ConfigurationMap)
						{
						case ModMinecraft.McVersionState.Error:
							break;
						case ModMinecraft.McVersionState.Original:
						case ModMinecraft.McVersionState.OptiFine:
						case ModMinecraft.McVersionState.Forge:
						case ModMinecraft.McVersionState.NeoForge:
						case ModMinecraft.McVersionState.LiteLoader:
						case ModMinecraft.McVersionState.Fabric:
							this._ConsumerMap = this.Version.ToString();
							break;
						case ModMinecraft.McVersionState.Snapshot:
							if (this.Version._ConnectionMap.ContainsF("pre", true))
							{
								this._ConsumerMap = "预发布版 " + this.Version._ConnectionMap;
							}
							else if (this.Version._ConnectionMap.ContainsF("rc", true))
							{
								this._ConsumerMap = "发布候选 " + this.Version._ConnectionMap;
							}
							else if (!this.Version._ConnectionMap.Contains("experimental") && Operators.CompareString(this.Version._ConnectionMap, "pending", false) != 0)
							{
								this._ConsumerMap = "快照 " + this.Version._ConnectionMap;
							}
							else
							{
								this._ConsumerMap = "实验性快照";
							}
							break;
						case ModMinecraft.McVersionState.Fool:
							this._ConsumerMap = ModMinecraft.GetMcFoolName(this.Version._ConnectionMap);
							break;
						case ModMinecraft.McVersionState.Old:
							this._ConsumerMap = "远古版本";
							break;
						default:
							this._ConsumerMap = "发生了未知错误，请向作者反馈此问题";
							break;
						}
						if (this._ConfigurationMap != ModMinecraft.McVersionState.Error)
						{
							if (this.PrepareThread())
							{
								ref string ptr = ref this._ConsumerMap;
								this._ConsumerMap = ptr + ", JumpLoader";
							}
							if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("VersionServerLogin", this), 3, false))
							{
								ref string ptr = ref this._ConsumerMap;
								this._ConsumerMap = ptr + ", 统一通行证验证";
							}
							if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("VersionServerLogin", this), 4, false))
							{
								ref string ptr = ref this._ConsumerMap;
								this._ConsumerMap = ptr + ", Authlib 验证";
							}
						}
					}
					else
					{
						this._ConsumerMap = text2;
					}
					this._TokenMap = Conversions.ToBoolean(ModBase.ReadIni(this.Path + "PCL\\Setup.ini", "IsStar", Conversions.ToString(false)));
					this.expressionMap = (ModMinecraft.McVersionCardType)Conversions.ToInteger(ModBase.ReadIni(this.Path + "PCL\\Setup.ini", "DisplayType", Conversions.ToString(0)));
					if (Directory.Exists(this.Path))
					{
						ModBase.WriteIni(this.Path + "PCL\\Setup.ini", "State", Conversions.ToString((int)this._ConfigurationMap));
						ModBase.WriteIni(this.Path + "PCL\\Setup.ini", "Info", this._ConsumerMap);
						ModBase.WriteIni(this.Path + "PCL\\Setup.ini", "Logo", this.getterMap);
					}
					if (this._ConfigurationMap != ModMinecraft.McVersionState.Error)
					{
						ModBase.WriteIni(this.Path + "PCL\\Setup.ini", "ReleaseTime", this._RegistryMap.ToString("yyyy'-'MM'-'dd HH':'mm"));
						ModBase.WriteIni(this.Path + "PCL\\Setup.ini", "VersionFabric", this.Version._AdvisorMap);
						ModBase.WriteIni(this.Path + "PCL\\Setup.ini", "VersionOptiFine", this.Version._RoleMap);
						ModBase.WriteIni(this.Path + "PCL\\Setup.ini", "VersionLiteLoader", Conversions.ToString(this.Version._AccountMap));
						ModBase.WriteIni(this.Path + "PCL\\Setup.ini", "VersionForge", this.Version.printerMap);
						ModBase.WriteIni(this.Path + "PCL\\Setup.ini", "VersionNeoForge", this.Version.m_AttrMap);
						ModBase.WriteIni(this.Path + "PCL\\Setup.ini", "VersionApiCode", Conversions.ToString(this.Version.ReadThread()));
						ModBase.WriteIni(this.Path + "PCL\\Setup.ini", "VersionOriginal", this.Version._ConnectionMap);
						ModBase.WriteIni(this.Path + "PCL\\Setup.ini", "VersionOriginalMain", Conversions.ToString(this.Version._ServerMap));
						ModBase.WriteIni(this.Path + "PCL\\Setup.ini", "VersionOriginalSub", Conversions.ToString(this.Version.m_ResolverMap));
					}
				}
				catch (Exception ex)
				{
					this._ConsumerMap = "未知错误：" + ModBase.GetExceptionSummary(ex);
					this.getterMap = ModBase.m_SerializerRepository + "Blocks/RedstoneBlock.png";
					this._ConfigurationMap = ModMinecraft.McVersionState.Error;
					ModBase.Log(ex, "加载版本失败（" + this.Name + "）", ModBase.LogLevel.Feedback, "出现错误");
				}
				finally
				{
					this.importerMap = true;
				}
				return this;
			}

			// Token: 0x06000E8C RID: 3724 RVA: 0x0006E0A0 File Offset: 0x0006C2A0
			public override bool Equals(object obj)
			{
				ModMinecraft.McVersion mcVersion = obj as ModMinecraft.McVersion;
				return mcVersion != null && Operators.CompareString(this.Path, mcVersion.Path, false) == 0;
			}

			// Token: 0x06000E8D RID: 3725 RVA: 0x0006E0D0 File Offset: 0x0006C2D0
			public static bool operator ==(ModMinecraft.McVersion a, ModMinecraft.McVersion b)
			{
				return (a == null && b == null) || (a != null && b != null && Operators.CompareString(a.Path, b.Path, false) == 0);
			}

			// Token: 0x06000E8E RID: 3726 RVA: 0x00009032 File Offset: 0x00007232
			public static bool operator !=(ModMinecraft.McVersion a, ModMinecraft.McVersion b)
			{
				return !(a == b);
			}

			// Token: 0x040007C2 RID: 1986
			[CompilerGenerated]
			private string _MethodMap;

			// Token: 0x040007C3 RID: 1987
			private string taskMap;

			// Token: 0x040007C4 RID: 1988
			public string _ConsumerMap;

			// Token: 0x040007C5 RID: 1989
			public ModMinecraft.McVersionState _ConfigurationMap;

			// Token: 0x040007C6 RID: 1990
			public string getterMap;

			// Token: 0x040007C7 RID: 1991
			public bool _TokenMap;

			// Token: 0x040007C8 RID: 1992
			public ModMinecraft.McVersionCardType expressionMap;

			// Token: 0x040007C9 RID: 1993
			private ModMinecraft.McVersionInfo m_WriterMap;

			// Token: 0x040007CA RID: 1994
			public DateTime _RegistryMap;

			// Token: 0x040007CB RID: 1995
			private string m_RuleMap;

			// Token: 0x040007CC RID: 1996
			private JObject proccesorMap;

			// Token: 0x040007CD RID: 1997
			[CompilerGenerated]
			private bool m_SetterMap;

			// Token: 0x040007CE RID: 1998
			[CompilerGenerated]
			private bool _FactoryMap;

			// Token: 0x040007CF RID: 1999
			private string m_ExporterMap;

			// Token: 0x040007D0 RID: 2000
			public bool importerMap;
		}

		// Token: 0x02000173 RID: 371
		public enum McVersionState
		{
			// Token: 0x040007D5 RID: 2005
			Error,
			// Token: 0x040007D6 RID: 2006
			Original,
			// Token: 0x040007D7 RID: 2007
			Snapshot,
			// Token: 0x040007D8 RID: 2008
			Fool,
			// Token: 0x040007D9 RID: 2009
			OptiFine,
			// Token: 0x040007DA RID: 2010
			Old,
			// Token: 0x040007DB RID: 2011
			Forge,
			// Token: 0x040007DC RID: 2012
			NeoForge,
			// Token: 0x040007DD RID: 2013
			LiteLoader,
			// Token: 0x040007DE RID: 2014
			Fabric
		}

		// Token: 0x02000174 RID: 372
		public class McVersionInfo
		{
			// Token: 0x06000E94 RID: 3732 RVA: 0x0006E190 File Offset: 0x0006C390
			public McVersionInfo()
			{
				this.workerMap = false;
				this._ServerMap = -1;
				this.m_ResolverMap = -1;
				this._StatusMap = false;
				this._RoleMap = "";
				this.m_StructMap = false;
				this.printerMap = "";
				this._ValMap = false;
				this.m_AttrMap = "";
				this._CandidateMap = false;
				this._AdvisorMap = "";
				this._AccountMap = false;
				this.queueMap = -2;
			}

			// Token: 0x06000E95 RID: 3733 RVA: 0x0006E210 File Offset: 0x0006C410
			public override string ToString()
			{
				string text = "";
				if (this.m_StructMap)
				{
					text = text + ", Forge" + ((Operators.CompareString(this.printerMap, "未知版本", false) == 0) ? "" : (" " + this.printerMap));
				}
				if (this._ValMap)
				{
					text = text + ", NeoForge" + ((Operators.CompareString(this.m_AttrMap, "未知版本", false) == 0) ? "" : (" " + this.m_AttrMap));
				}
				if (this._CandidateMap)
				{
					text = text + ", Fabric" + ((Operators.CompareString(this._AdvisorMap, "未知版本", false) == 0) ? "" : (" " + this._AdvisorMap));
				}
				if (this._StatusMap)
				{
					text = text + ", OptiFine" + ((Operators.CompareString(this._RoleMap, "未知版本", false) == 0) ? "" : (" " + this._RoleMap));
				}
				if (this._AccountMap)
				{
					text += ", LiteLoader";
				}
				if (Operators.CompareString(text, "", false) == 0)
				{
					text = "原版 " + this._ConnectionMap;
				}
				else
				{
					text = this._ConnectionMap + text + (ModBase._TokenRepository ? (" (" + Conversions.ToString(this.ReadThread()) + "#)") : "");
				}
				return text;
			}

			// Token: 0x06000E96 RID: 3734 RVA: 0x0006E388 File Offset: 0x0006C588
			public int ReadThread()
			{
				checked
				{
					if (this.queueMap == -2)
					{
						try
						{
							if (this._CandidateMap)
							{
								if (Operators.CompareString(this._AdvisorMap, "未知版本", false) == 0)
								{
									return 0;
								}
								string[] array = this._AdvisorMap.Split(".");
								if (array.Length < 3)
								{
									throw new Exception("无效的 Fabric 版本：" + this.printerMap);
								}
								this.queueMap = (int)Math.Round(unchecked(ModBase.Val(array[0]) * 10000.0 + ModBase.Val(array[1]) * 100.0 + ModBase.Val(array[2])));
							}
							else if (!this.m_StructMap && !this._ValMap)
							{
								if (this._StatusMap)
								{
									if (Operators.CompareString(this._RoleMap, "未知版本", false) == 0)
									{
										return 0;
									}
									this.queueMap = (int)Math.Round(unchecked((double)(checked(((this.m_ResolverMap >= 0) ? this.m_ResolverMap : 0) * 1000000 + (Strings.Asc(Conversions.ToChar(Strings.Left(this._RoleMap.ToUpper(), 1))) - 65 + 1) * 10000)) + ModBase.Val(ModBase.RegexSeek(Strings.Right(this._RoleMap, checked(this._RoleMap.Length - 1)), "[0-9]+", 0)) * 100.0));
									if (this._RoleMap.ContainsF("pre", true))
									{
										ref int ptr = ref this.queueMap;
										this.queueMap = ptr + 50;
									}
									if (!this._RoleMap.ContainsF("pre", true) && !this._RoleMap.ContainsF("beta", true))
									{
										ref int ptr = ref this.queueMap;
										this.queueMap = ptr + 99;
									}
									else if (ModBase.Val(Strings.Right(this._RoleMap, 1)) == 0.0 && Operators.CompareString(Strings.Right(this._RoleMap, 1), "0", false) != 0)
									{
										ref int ptr = ref this.queueMap;
										this.queueMap = ptr + 1;
									}
									else
									{
										ref int ptr = ref this.queueMap;
										this.queueMap = (int)Math.Round(unchecked((double)ptr + ModBase.Val(ModBase.RegexSeek(this._RoleMap.ToLower(), "(?<=((pre)|(beta)))[0-9]+", 0))));
									}
								}
								else
								{
									this.queueMap = -1;
								}
							}
							else
							{
								if (Operators.CompareString(this.printerMap, "未知版本", false) == 0 && Operators.CompareString(this.m_AttrMap, "未知版本", false) == 0)
								{
									return 0;
								}
								string[] array2 = this.m_StructMap ? this.printerMap.Split(".") : this.m_AttrMap.Split(".");
								if (array2.Length == 4)
								{
									this.queueMap = (int)Math.Round(unchecked(ModBase.Val(array2[0]) * 1000000.0 + ModBase.Val(array2[1]) * 10000.0 + ModBase.Val(array2[3])));
								}
								else
								{
									if (array2.Length != 3)
									{
										throw new Exception("无效的 Neo/Forge 版本：" + this.printerMap);
									}
									this.queueMap = (int)Math.Round(unchecked(ModBase.Val(array2[0]) * 1000000.0 + ModBase.Val(array2[1]) * 10000.0 + ModBase.Val(array2[2])));
								}
							}
						}
						catch (Exception ex)
						{
							this.queueMap = -1;
							ModBase.Log(ex, "获取 API 版本信息失败：" + this.ToString(), ModBase.LogLevel.Debug, "出现错误");
						}
					}
					return this.queueMap;
				}
			}

			// Token: 0x06000E97 RID: 3735 RVA: 0x0000904D File Offset: 0x0000724D
			public void LogoutThread(int value)
			{
				this.queueMap = value;
			}

			// Token: 0x040007DF RID: 2015
			public bool workerMap;

			// Token: 0x040007E0 RID: 2016
			public string _ConnectionMap;

			// Token: 0x040007E1 RID: 2017
			public int _ServerMap;

			// Token: 0x040007E2 RID: 2018
			public int m_ResolverMap;

			// Token: 0x040007E3 RID: 2019
			public bool _StatusMap;

			// Token: 0x040007E4 RID: 2020
			public string _RoleMap;

			// Token: 0x040007E5 RID: 2021
			public bool m_StructMap;

			// Token: 0x040007E6 RID: 2022
			public string printerMap;

			// Token: 0x040007E7 RID: 2023
			public bool _ValMap;

			// Token: 0x040007E8 RID: 2024
			public string m_AttrMap;

			// Token: 0x040007E9 RID: 2025
			public bool _CandidateMap;

			// Token: 0x040007EA RID: 2026
			public string _AdvisorMap;

			// Token: 0x040007EB RID: 2027
			public bool _AccountMap;

			// Token: 0x040007EC RID: 2028
			private int queueMap;
		}

		// Token: 0x02000175 RID: 373
		public enum McVersionCardType
		{
			// Token: 0x040007EE RID: 2030
			Star = -1,
			// Token: 0x040007EF RID: 2031
			Auto,
			// Token: 0x040007F0 RID: 2032
			Hidden,
			// Token: 0x040007F1 RID: 2033
			API,
			// Token: 0x040007F2 RID: 2034
			OriginalLike,
			// Token: 0x040007F3 RID: 2035
			Rubbish,
			// Token: 0x040007F4 RID: 2036
			Fool,
			// Token: 0x040007F5 RID: 2037
			Error
		}

		// Token: 0x02000176 RID: 374
		public struct McSkinInfo
		{
			// Token: 0x040007F6 RID: 2038
			public bool eventMap;

			// Token: 0x040007F7 RID: 2039
			public string managerMap;

			// Token: 0x040007F8 RID: 2040
			public bool _ModelMap;
		}

		// Token: 0x02000177 RID: 375
		public class McLibToken
		{
			// Token: 0x06000E99 RID: 3737 RVA: 0x00009056 File Offset: 0x00007256
			public McLibToken()
			{
				this.m_BaseMap = 0L;
				this.attributeMap = false;
				this.codeMap = null;
				this.m_InfoMap = false;
			}

			// Token: 0x06000E9A RID: 3738 RVA: 0x00009083 File Offset: 0x00007283
			public string ReflectThread()
			{
				return this._PrototypeMap;
			}

			// Token: 0x06000E9B RID: 3739 RVA: 0x0000908B File Offset: 0x0000728B
			public void ForgotThread(string value)
			{
				this._PrototypeMap = (string.IsNullOrWhiteSpace(value) ? null : value);
			}

			// Token: 0x1700021C RID: 540
			// (get) Token: 0x06000E9C RID: 3740 RVA: 0x0006E718 File Offset: 0x0006C918
			public string Name
			{
				get
				{
					string result;
					if (this._AnnotationMap == null)
					{
						result = null;
					}
					else
					{
						List<string> list = new List<string>(this._AnnotationMap.Split(":"));
						list.RemoveAt(2);
						result = list.Join(":");
					}
					return result;
				}
			}

			// Token: 0x06000E9D RID: 3741 RVA: 0x0000909F File Offset: 0x0000729F
			public override string ToString()
			{
				return (this.attributeMap ? "[Native] " : "") + ModBase.GetString(this.m_BaseMap) + " | " + this._WrapperMap;
			}

			// Token: 0x040007F9 RID: 2041
			public string _WrapperMap;

			// Token: 0x040007FA RID: 2042
			public long m_BaseMap;

			// Token: 0x040007FB RID: 2043
			public bool attributeMap;

			// Token: 0x040007FC RID: 2044
			public string codeMap;

			// Token: 0x040007FD RID: 2045
			private string _PrototypeMap;

			// Token: 0x040007FE RID: 2046
			public string _AnnotationMap;

			// Token: 0x040007FF RID: 2047
			public bool m_InfoMap;
		}

		// Token: 0x02000178 RID: 376
		private struct McAssetsToken
		{
			// Token: 0x06000E9F RID: 3743 RVA: 0x000090D0 File Offset: 0x000072D0
			public override string ToString()
			{
				return ModBase.GetString(this.listMap) + " | " + this.m_AdapterMap;
			}

			// Token: 0x04000800 RID: 2048
			public string m_AdapterMap;

			// Token: 0x04000801 RID: 2049
			public string _FacadeMap;

			// Token: 0x04000802 RID: 2050
			public long listMap;

			// Token: 0x04000803 RID: 2051
			public string merchantMap;
		}

		// Token: 0x02000179 RID: 377
		public class VersionComparer : IComparer<string>
		{
			// Token: 0x06000EA2 RID: 3746 RVA: 0x000090ED File Offset: 0x000072ED
			public int Compare(string x, string y)
			{
				return ModMinecraft.VersionSortInteger(x, y);
			}
		}
	}
}
