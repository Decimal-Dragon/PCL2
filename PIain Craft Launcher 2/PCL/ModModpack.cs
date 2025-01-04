using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace PCL
{
	// Token: 0x02000075 RID: 117
	[StandardModule]
	public sealed class ModModpack
	{
		// Token: 0x060002DD RID: 733 RVA: 0x00020A24 File Offset: 0x0001EC24
		public static void ModpackInstall()
		{
			string File = ModBase.SelectFile("整合包文件(*.rar;*.zip;*.mrpack)|*.rar;*.zip;*.mrpack", "选择整合包压缩文件");
			if (!string.IsNullOrEmpty(File))
			{
				ModBase.RunInThread(delegate
				{
					try
					{
						ModModpack.ModpackInstall(File, null, null);
					}
					catch (ModBase.CancelledException ex)
					{
					}
					catch (Exception ex2)
					{
						ModBase.Log(ex2, "手动安装整合包失败", ModBase.LogLevel.Msgbox, "出现错误");
					}
				});
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00020A6C File Offset: 0x0001EC6C
		public static ModLoader.LoaderCombo<string> ModpackInstall(string File, string VersionName = null, string Logo = null)
		{
			ModBase.Log("[ModPack] 整合包安装请求：" + (File ?? "null"), ModBase.LogLevel.Normal, "出现错误");
			ZipArchive zipArchive = null;
			string archiveBaseFolder = "";
			ModLoader.LoaderCombo<string> result;
			try
			{
				int num = -1;
				try
				{
					zipArchive = new ZipArchive(new FileStream(File, FileMode.Open, FileAccess.Read, FileShare.Read));
					if (zipArchive.GetEntry("mcbbs.packmeta") != null)
					{
						num = 3;
					}
					else if (zipArchive.GetEntry("mmc-pack.json") != null)
					{
						num = 2;
					}
					else if (zipArchive.GetEntry("modrinth.index.json") != null)
					{
						num = 4;
					}
					else if (zipArchive.GetEntry("manifest.json") != null)
					{
						if (((JObject)ModBase.GetJson(ModBase.ReadFile(zipArchive.GetEntry("manifest.json").Open(), Encoding.UTF8)))["addons"] == null)
						{
							num = 0;
						}
						else
						{
							num = 3;
						}
					}
					else if (zipArchive.GetEntry("modpack.json") != null)
					{
						num = 1;
					}
					else if (zipArchive.GetEntry("modpack.zip") == null && zipArchive.GetEntry("modpack.mrpack") == null)
					{
						try
						{
							foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
							{
								string[] array = zipArchiveEntry.FullName.Split("/");
								archiveBaseFolder = array[0] + "/";
								if (Enumerable.Count<string>(array) == 2)
								{
									if (Operators.CompareString(array[1], "mcbbs.packmeta", false) == 0)
									{
										num = 3;
										break;
									}
									if (Operators.CompareString(array[1], "mmc-pack.json", false) == 0)
									{
										num = 2;
										break;
									}
									if (Operators.CompareString(array[1], "modrinth.index.json", false) == 0)
									{
										num = 4;
										break;
									}
									if (Operators.CompareString(array[1], "manifest.json", false) != 0)
									{
										if (Operators.CompareString(array[1], "modpack.json", false) == 0)
										{
											num = 1;
											break;
										}
										if (Operators.CompareString(array[1], "modpack.zip", false) == 0 || Operators.CompareString(array[1], "modpack.mrpack", false) == 0)
										{
											num = 9;
											break;
										}
									}
									else
									{
										if (((JObject)ModBase.GetJson(ModBase.ReadFile(zipArchiveEntry.Open(), Encoding.UTF8)))["addons"] == null)
										{
											num = 0;
											break;
										}
										num = 3;
										archiveBaseFolder = "overrides/";
										break;
									}
								}
							}
						}
						finally
						{
							IEnumerator<ZipArchiveEntry> enumerator;
							if (enumerator != null)
							{
								enumerator.Dispose();
							}
						}
					}
					else
					{
						num = 9;
					}
				}
				catch (Exception ex)
				{
					if (ModBase.GetExceptionDetail(ex, true).Contains("Error.WinIOError"))
					{
						throw new Exception("打开整合包文件失败", ex);
					}
					if (File.EndsWithF(".rar", true))
					{
						throw new Exception("PCL 无法处理 rar 格式的压缩包，请在解压后重新压缩为 zip 格式再试", ex);
					}
					throw new Exception("打开整合包文件失败，文件可能损坏或为不支持的压缩包格式", ex);
				}
				switch (num)
				{
				case 0:
					ModBase.Log("[ModPack] 整合包种类：CurseForge", ModBase.LogLevel.Normal, "出现错误");
					return ModModpack.InstallPackCurseForge(File, zipArchive, archiveBaseFolder, VersionName, Logo);
				case 1:
					ModBase.Log("[ModPack] 整合包种类：HMCL", ModBase.LogLevel.Normal, "出现错误");
					return ModModpack.InstallPackHMCL(File, zipArchive, archiveBaseFolder);
				case 2:
					ModBase.Log("[ModPack] 整合包种类：MMC", ModBase.LogLevel.Normal, "出现错误");
					return ModModpack.InstallPackMMC(File, zipArchive, archiveBaseFolder);
				case 3:
					ModBase.Log("[ModPack] 整合包种类：MCBBS", ModBase.LogLevel.Normal, "出现错误");
					return ModModpack.InstallPackMCBBS(File, zipArchive, archiveBaseFolder, VersionName);
				case 4:
					ModBase.Log("[ModPack] 整合包种类：Modrinth", ModBase.LogLevel.Normal, "出现错误");
					return ModModpack.InstallPackModrinth(File, zipArchive, archiveBaseFolder, VersionName, Logo);
				case 9:
					ModBase.Log("[ModPack] 整合包种类：带启动器的压缩包", ModBase.LogLevel.Normal, "出现错误");
					return ModModpack.InstallPackLauncherPack(File, zipArchive, archiveBaseFolder);
				}
				ModBase.Log("[ModPack] 整合包种类：未能识别，假定为压缩包", ModBase.LogLevel.Normal, "出现错误");
				result = ModModpack.InstallPackCompress(File, zipArchive);
			}
			finally
			{
				if (zipArchive != null)
				{
					zipArchive.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00020E4C File Offset: 0x0001F04C
		private static void ExtractModpackFiles(string InstallTemp, string FileAddress, ModLoader.LoaderBase Loader, double LoaderProgressDelta)
		{
			ModModpack._Closure$__5-0 CS$<>8__locals1 = new ModModpack._Closure$__5-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_Loader = Loader;
			CS$<>8__locals1.$VB$Local_LoaderProgressDelta = LoaderProgressDelta;
			if (!ModModpack.m_Invocation)
			{
				ModModpack.m_Invocation = true;
				ModModpack.proxy = true;
				try
				{
					ModBase.Log("[ModPack] 开始清理整合包安装缓存", ModBase.LogLevel.Normal, "出现错误");
					ModBase.DeleteDirectory(ModBase.m_DecoratorRepository + "PackInstall\\", false);
					ModBase.Log("[ModPack] 已清理整合包安装缓存", ModBase.LogLevel.Normal, "出现错误");
					goto IL_9D;
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "清理整合包安装缓存失败", ModBase.LogLevel.Debug, "出现错误");
					goto IL_9D;
				}
				finally
				{
					ModModpack.proxy = false;
				}
			}
			if (ModModpack.proxy)
			{
				while (ModModpack.proxy)
				{
					Thread.Sleep(1);
				}
			}
			IL_9D:
			int num = 1;
			Encoding encode = Encoding.GetEncoding("GB18030");
			try
			{
				IL_AA:
				ModBase.DeleteDirectory(InstallTemp, false);
				ModBase.ExtractFile(FileAddress, InstallTemp, encode, delegate(double Delta)
				{
					ModLoader.LoaderBase $VB$Local_Loader;
					($VB$Local_Loader = CS$<>8__locals1.$VB$Local_Loader).Progress = $VB$Local_Loader.Progress + Delta * CS$<>8__locals1.$VB$Local_LoaderProgressDelta;
				});
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, "第 " + Conversions.ToString(num) + " 次解压尝试失败", ModBase.LogLevel.Debug, "出现错误");
				if (ex2 is ArgumentException)
				{
					encode = Encoding.UTF8;
					ModBase.Log("[ModPack] 已切换压缩包解压编码为 UTF8", ModBase.LogLevel.Normal, "出现错误");
				}
				if (num >= 5)
				{
					throw new Exception("解压整合包文件失败", ex2);
				}
				checked
				{
					Thread.Sleep(num * 2000);
					if (CS$<>8__locals1.$VB$Local_Loader == null || CS$<>8__locals1.$VB$Local_Loader.LoadingState == MyLoading.MyLoadingState.Run)
					{
						num++;
						goto IL_AA;
					}
				}
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00020FDC File Offset: 0x0001F1DC
		private static ModLoader.LoaderCombo<string> InstallPackCurseForge(string FileAddress, ZipArchive Archive, string ArchiveBaseFolder, string VersionName = null, string Logo = null)
		{
			ModModpack._Closure$__6-0 CS$<>8__locals1 = new ModModpack._Closure$__6-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_FileAddress = FileAddress;
			CS$<>8__locals1.$VB$Local_ArchiveBaseFolder = ArchiveBaseFolder;
			CS$<>8__locals1.$VB$Local_VersionName = VersionName;
			CS$<>8__locals1.$VB$Local_Logo = Logo;
			JObject jobject;
			try
			{
				jobject = (JObject)ModBase.GetJson(ModBase.ReadFile(Archive.GetEntry(CS$<>8__locals1.$VB$Local_ArchiveBaseFolder + "manifest.json").Open(), null));
			}
			catch (Exception innerException)
			{
				throw new Exception("CurseForge 整合包安装信息存在问题", innerException);
			}
			if (jobject["minecraft"] == null || jobject["minecraft"]["version"] == null)
			{
				throw new Exception("CurseForge 整合包未提供 Minecraft 版本信息");
			}
			if (CS$<>8__locals1.$VB$Local_VersionName == null)
			{
				CS$<>8__locals1.$VB$Local_VersionName = (string)(jobject["name"] ?? "");
				ValidateFolderName validateFolderName = new ValidateFolderName(ModMinecraft.m_ProxyTests + "versions", true, true);
				if (Operators.CompareString(validateFolderName.Validate(CS$<>8__locals1.$VB$Local_VersionName), "", false) != 0)
				{
					CS$<>8__locals1.$VB$Local_VersionName = "";
				}
				if (Operators.CompareString(CS$<>8__locals1.$VB$Local_VersionName, "", false) == 0)
				{
					CS$<>8__locals1.$VB$Local_VersionName = ModMain.MyMsgBoxInput("输入版本名称", "", "", new Collection<Validate>
					{
						validateFolderName
					}, "", "确定", "取消", false);
				}
				if (string.IsNullOrEmpty(CS$<>8__locals1.$VB$Local_VersionName))
				{
					throw new ModBase.CancelledException();
				}
			}
			string refTest = null;
			string instanceTest = null;
			string callbackTest = null;
			try
			{
				foreach (JToken jtoken in (jobject["minecraft"]["modLoaders"] ?? new byte[0]))
				{
					string text = (jtoken["id"] ?? "").ToString().ToLower();
					if (text.StartsWithF("forge-", false))
					{
						if (text.Contains("recommended"))
						{
							throw new Exception("该整合包版本过老，已不支持进行安装！");
						}
						ModBase.Log("[ModPack] 整合包 Forge 版本：" + text, ModBase.LogLevel.Normal, "出现错误");
						refTest = text.Replace("forge-", "");
					}
					else if (text.StartsWithF("neoforge-", false))
					{
						ModBase.Log("[ModPack] 整合包 NeoForge 版本：" + text, ModBase.LogLevel.Normal, "出现错误");
						instanceTest = text.Replace("neoforge-", "");
					}
					else if (text.StartsWithF("fabric-", false))
					{
						ModBase.Log("[ModPack] 整合包 Fabric 版本：" + text, ModBase.LogLevel.Normal, "出现错误");
						callbackTest = text.Replace("fabric-", "");
					}
					else
					{
						ModBase.Log("[ModPack] 未知 Mod 加载器：" + text, ModBase.LogLevel.Normal, "出现错误");
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
			CS$<>8__locals1.$VB$Local_InstallTemp = ModBase.m_DecoratorRepository + "PackInstall\\" + Conversions.ToString(ModBase.RandomInteger(0, 100000)) + "\\";
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			CS$<>8__locals1.$VB$Local_OverrideHome = (string)(jobject["overrides"] ?? "");
			if (Operators.CompareString(CS$<>8__locals1.$VB$Local_OverrideHome, "", false) != 0)
			{
				list.Add(new ModLoader.LoaderTask<string, int>("解压整合包文件", delegate(ModLoader.LoaderTask<string, int> Task)
				{
					ModModpack._Closure$__6-1 CS$<>8__locals2 = new ModModpack._Closure$__6-1(CS$<>8__locals2);
					CS$<>8__locals2.$VB$Local_Task = Task;
					ModModpack.ExtractModpackFiles(CS$<>8__locals1.$VB$Local_InstallTemp, CS$<>8__locals1.$VB$Local_FileAddress, CS$<>8__locals2.$VB$Local_Task, 0.6);
					CS$<>8__locals2.$VB$Local_Task.Progress = 0.6;
					string text2 = CS$<>8__locals1.$VB$Local_InstallTemp + CS$<>8__locals1.$VB$Local_ArchiveBaseFolder + CS$<>8__locals1.$VB$Local_OverrideHome;
					if (Directory.Exists(text2))
					{
						ModBase.CopyDirectory(text2, ModMinecraft.m_ProxyTests + "versions\\" + CS$<>8__locals1.$VB$Local_VersionName, delegate(double Delta)
						{
							ModLoader.LoaderTask<string, int> $VB$Local_Task;
							($VB$Local_Task = CS$<>8__locals2.$VB$Local_Task).Progress = $VB$Local_Task.Progress + Delta * 0.35;
						});
						ModBase.Log(string.Format("[ModPack] 整合包 override 复制：{0} -> {1}", text2, ModMinecraft.m_ProxyTests + "versions\\" + CS$<>8__locals1.$VB$Local_VersionName), ModBase.LogLevel.Normal, "出现错误");
					}
					else
					{
						ModBase.Log(string.Format("[ModPack] 整合包中未找到 override 文件夹：{0}", text2), ModBase.LogLevel.Normal, "出现错误");
					}
					CS$<>8__locals2.$VB$Local_Task.Progress = 0.95;
					ModBase.WriteIni(ModMinecraft.m_ProxyTests + "versions\\" + CS$<>8__locals1.$VB$Local_VersionName + "\\PCL\\Setup.ini", "VersionArgumentIndie", Conversions.ToString(1));
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = (double)new FileInfo(CS$<>8__locals1.$VB$Local_FileAddress).Length / 1024.0 / 1024.0 / 6.0,
					Block = false
				});
			}
			CS$<>8__locals1.$VB$Local_ModList = new List<int>();
			CS$<>8__locals1.$VB$Local_ModOptionalList = new List<int>();
			try
			{
				foreach (JToken jtoken2 in (jobject["files"] ?? new byte[0]))
				{
					if (jtoken2["projectID"] != null && jtoken2["fileID"] != null)
					{
						CS$<>8__locals1.$VB$Local_ModList.Add((int)jtoken2["fileID"]);
						if (jtoken2["required"] != null && !jtoken2["required"].ToObject<bool>())
						{
							CS$<>8__locals1.$VB$Local_ModOptionalList.Add((int)jtoken2["fileID"]);
						}
					}
					else
					{
						ModMain.Hint("某项 Mod 缺少必要信息，已跳过：" + jtoken2.ToString(), ModMain.HintType.Info, true);
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
			if (Enumerable.Any<int>(CS$<>8__locals1.$VB$Local_ModList))
			{
				List<ModLoader.LoaderBase> list2 = new List<ModLoader.LoaderBase>();
				list2.Add(new ModLoader.LoaderTask<int, JArray>("获取 Mod 下载信息", delegate(ModLoader.LoaderTask<int, JArray> Task)
				{
					Task.Output = (JArray)NewLateBinding.LateIndexGet(ModBase.GetJson(ModNet.NetRequestRetry("https://api.curseforge.com/v1/mods/files", "POST", "{\"fileIds\": [" + CS$<>8__locals1.$VB$Local_ModList.Join(",") + "]}", "application/json", true, null)), new object[]
					{
						"data"
					}, null);
					if (CS$<>8__locals1.$VB$Local_ModList.Count > Task.Output.Count)
					{
						throw new Exception("整合包中的部分 Mod 版本已被 Mod 作者删除，所以没法继续安装了，请向整合包作者反馈该问题");
					}
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = (double)CS$<>8__locals1.$VB$Local_ModList.Count / 10.0
				});
				list2.Add(new ModLoader.LoaderTask<JArray, List<ModNet.NetFile>>("构造 Mod 下载信息", delegate(ModLoader.LoaderTask<JArray, List<ModNet.NetFile>> Task)
				{
					Dictionary<int, ModNet.NetFile> dictionary = new Dictionary<int, ModNet.NetFile>();
					try
					{
						foreach (JToken jtoken3 in Task.Input)
						{
							int num = jtoken3["id"].ToObject<int>();
							if (!dictionary.ContainsKey(num) && (!CS$<>8__locals1.$VB$Local_ModOptionalList.Contains(num) || ModMain.MyMsgBox("是否要下载整合包中的可选文件 " + jtoken3["displayName"].ToString() + "？", "下载可选文件", "是", "否", "", false, true, false, null, null, null) != 2))
							{
								ModComp.CompFile compFile = new ModComp.CompFile((JObject)jtoken3, ModComp.CompType.Mod);
								if (compFile.ExcludeTests())
								{
									string arg;
									if (Enumerable.Any<JToken>(jtoken3["modules"]))
									{
										List<string> list6 = Enumerable.ToList<string>(Enumerable.Select<JToken, string>((JArray)jtoken3["modules"], (ModModpack._Closure$__.$I6-4 == null) ? (ModModpack._Closure$__.$I6-4 = ((JToken l) => l["name"].ToString())) : ModModpack._Closure$__.$I6-4));
										if (!list6.Contains("META-INF") && !list6.Contains("mcmod.info") && !compFile._BridgeRepository.EndsWithF(".jar", true))
										{
											if (list6.Contains("pack.mcmeta"))
											{
												arg = "resourcepacks";
											}
											else
											{
												arg = "shaderpacks";
											}
										}
										else
										{
											arg = "mods";
										}
									}
									else
									{
										arg = "mods";
									}
									dictionary.Add(num, compFile.ToNetFile(string.Format("{0}versions\\{1}\\{2}\\", ModMinecraft.m_ProxyTests, CS$<>8__locals1.$VB$Local_VersionName, arg)));
									Task.Progress += 1.0 / (double)(checked(1 + CS$<>8__locals1.$VB$Local_ModList.Count));
								}
							}
						}
					}
					finally
					{
						IEnumerator<JToken> enumerator3;
						if (enumerator3 != null)
						{
							enumerator3.Dispose();
						}
					}
					Task.Output = Enumerable.ToList<ModNet.NetFile>(dictionary.Values);
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = (double)CS$<>8__locals1.$VB$Local_ModList.Count / 200.0,
					Show = false
				});
				list2.Add(new ModNet.LoaderDownload("下载 Mod", new List<ModNet.NetFile>())
				{
					ProgressWeight = (double)CS$<>8__locals1.$VB$Local_ModList.Count * 1.5
				});
				list.Add(new ModLoader.LoaderCombo<int>("下载 Mod（主加载器）", list2)
				{
					Show = false,
					ProgressWeight = Enumerable.Sum<ModLoader.LoaderBase>(list2, (ModModpack._Closure$__.$I6-5 == null) ? (ModModpack._Closure$__.$I6-5 = ((ModLoader.LoaderBase l) => l.ProgressWeight)) : ModModpack._Closure$__.$I6-5)
				});
			}
			ModDownloadLib.McInstallRequest mcInstallRequest = new ModDownloadLib.McInstallRequest
			{
				m_SystemTest = CS$<>8__locals1.$VB$Local_VersionName,
				_ParamTest = string.Format("{0}versions\\{1}\\", ModMinecraft.m_ProxyTests, CS$<>8__locals1.$VB$Local_VersionName),
				_TagTest = jobject["minecraft"]["version"].ToString(),
				_RefTest = refTest,
				instanceTest = instanceTest,
				m_CallbackTest = callbackTest
			};
			List<ModLoader.LoaderBase> list3 = ModDownloadLib.McInstallLoader(mcInstallRequest, true);
			List<ModLoader.LoaderBase> list4 = new List<ModLoader.LoaderBase>();
			list4.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析游戏支持库文件（副加载器）", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
			{
				Task.Output = ModMinecraft.McLibFix(new ModMinecraft.McVersion(CS$<>8__locals1.$VB$Local_VersionName));
			}, null, ThreadPriority.Normal)
			{
				ProgressWeight = 1.0,
				Show = false
			});
			list4.Add(new ModNet.LoaderDownload("下载游戏支持库文件（副加载器）", new List<ModNet.NetFile>())
			{
				ProgressWeight = 7.0,
				Show = false
			});
			List<ModLoader.LoaderBase> list5 = new List<ModLoader.LoaderBase>();
			list5.Add(new ModLoader.LoaderCombo<string>("整合包安装", list)
			{
				Show = false,
				Block = false,
				ProgressWeight = Enumerable.Sum<ModLoader.LoaderBase>(list, (ModModpack._Closure$__.$I6-7 == null) ? (ModModpack._Closure$__.$I6-7 = ((ModLoader.LoaderBase l) => l.ProgressWeight)) : ModModpack._Closure$__.$I6-7)
			});
			list5.Add(new ModLoader.LoaderCombo<string>("游戏安装", list3)
			{
				Show = false,
				ProgressWeight = Enumerable.Sum<ModLoader.LoaderBase>(list3, (ModModpack._Closure$__.$I6-8 == null) ? (ModModpack._Closure$__.$I6-8 = ((ModLoader.LoaderBase l) => l.ProgressWeight)) : ModModpack._Closure$__.$I6-8)
			});
			list5.Add(new ModLoader.LoaderCombo<string>("下载游戏支持库文件", list4)
			{
				ProgressWeight = 8.0
			});
			list5.Add(new ModLoader.LoaderTask<string, string>("最终整理文件", delegate(ModLoader.LoaderTask<string, string> Task)
			{
				string str = ModMinecraft.m_ProxyTests + "versions\\" + CS$<>8__locals1.$VB$Local_VersionName + "\\";
				if (CS$<>8__locals1.$VB$Local_Logo != null && File.Exists(CS$<>8__locals1.$VB$Local_Logo))
				{
					File.Copy(CS$<>8__locals1.$VB$Local_Logo, str + "PCL\\Logo.png", true);
					ModBase.WriteIni(str + "PCL\\Setup.ini", "Logo", "PCL\\Logo.png");
					ModBase.WriteIni(str + "PCL\\Setup.ini", "LogoCustom", "True");
					ModBase.Log("[ModPack] 已设置整合包 Logo：" + CS$<>8__locals1.$VB$Local_Logo, ModBase.LogLevel.Normal, "出现错误");
				}
				foreach (string text2 in new string[]
				{
					str + "原始整合包.zip",
					str + "原始整合包.mrpack"
				})
				{
					if (File.Exists(text2))
					{
						ModBase.Log("[ModPack] 删除原始整合包文件：" + text2, ModBase.LogLevel.Normal, "出现错误");
						File.Delete(text2);
					}
				}
			}, null, ThreadPriority.Normal)
			{
				ProgressWeight = 0.1,
				Show = false
			});
			CS$<>8__locals1.$VB$Local_LoaderName = "CurseForge 整合包安装：" + CS$<>8__locals1.$VB$Local_VersionName + " ";
			if (Enumerable.Any<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar, (ModLoader.LoaderBase l) => Operators.CompareString(l.Name, CS$<>8__locals1.$VB$Local_LoaderName, false) == 0))
			{
				ModMain.Hint("该整合包正在安装中！", ModMain.HintType.Critical, true);
				throw new ModBase.CancelledException();
			}
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(CS$<>8__locals1.$VB$Local_LoaderName, list5);
			loaderCombo.OnStateChanged = new Action<ModLoader.LoaderBase>(ModDownloadLib.McInstallState);
			loaderCombo.Start(mcInstallRequest._ParamTest, false);
			ModLoader.LoaderTaskbarAdd<string>(loaderCombo);
			ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
			ModBase.RunInUi((ModModpack._Closure$__.$I6-11 == null) ? (ModModpack._Closure$__.$I6-11 = delegate()
			{
				ModMain._ProcessIterator.PageChange(FormMain.PageType.DownloadManager, FormMain.PageSubType.Default);
			}) : ModModpack._Closure$__.$I6-11, false);
			return loaderCombo;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00021844 File Offset: 0x0001FA44
		private static ModLoader.LoaderCombo<string> InstallPackModrinth(string FileAddress, ZipArchive Archive, string ArchiveBaseFolder, string VersionName = null, string Logo = null)
		{
			ModModpack._Closure$__7-0 CS$<>8__locals1 = new ModModpack._Closure$__7-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_FileAddress = FileAddress;
			CS$<>8__locals1.$VB$Local_ArchiveBaseFolder = ArchiveBaseFolder;
			CS$<>8__locals1.$VB$Local_VersionName = VersionName;
			CS$<>8__locals1.$VB$Local_Logo = Logo;
			JObject jobject;
			try
			{
				jobject = (JObject)ModBase.GetJson(ModBase.ReadFile(Archive.GetEntry(CS$<>8__locals1.$VB$Local_ArchiveBaseFolder + "modrinth.index.json").Open(), null));
			}
			catch (Exception innerException)
			{
				throw new Exception("Modrinth 整合包安装信息存在问题", innerException);
			}
			if (jobject["dependencies"] == null || jobject["dependencies"]["minecraft"] == null)
			{
				throw new Exception("Modrinth 整合包未提供 Minecraft 版本信息");
			}
			string tagTest = null;
			string text = null;
			string text2 = null;
			string text3 = null;
			try
			{
				foreach (JToken jtoken in (jobject["dependencies"] ?? new byte[0]))
				{
					JProperty jproperty = (JProperty)jtoken;
					string left = jproperty.Name.ToLower();
					if (Operators.CompareString(left, "minecraft", false) != 0)
					{
						if (Operators.CompareString(left, "forge", false) != 0)
						{
							if (Operators.CompareString(left, "neoforge", false) != 0 && Operators.CompareString(left, "neo-forge", false) != 0)
							{
								if (Operators.CompareString(left, "fabric-loader", false) == 0)
								{
									text3 = jproperty.Value.ToString();
									ModBase.Log("[ModPack] 整合包 Fabric 版本：" + text3, ModBase.LogLevel.Normal, "出现错误");
								}
								else
								{
									if (Operators.CompareString(left, "quilt-loader", false) != 0)
									{
										ModMain.Hint(string.Format("无法安装整合包，其中出现了未知的 Mod 加载器 {0}！", jproperty.Value), ModMain.HintType.Critical, true);
										throw new ModBase.CancelledException();
									}
									ModMain.Hint("PCL 暂不支持安装需要 Quilt 的整合包！", ModMain.HintType.Critical, true);
									throw new ModBase.CancelledException();
								}
							}
							else
							{
								text2 = jproperty.Value.ToString();
								ModBase.Log("[ModPack] 整合包 NeoForge 版本：" + text2, ModBase.LogLevel.Normal, "出现错误");
							}
						}
						else
						{
							text = jproperty.Value.ToString();
							ModBase.Log("[ModPack] 整合包 Forge 版本：" + text, ModBase.LogLevel.Normal, "出现错误");
						}
					}
					else
					{
						tagTest = jproperty.Value.ToString();
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
			if (CS$<>8__locals1.$VB$Local_VersionName == null)
			{
				CS$<>8__locals1.$VB$Local_VersionName = (string)(jobject["name"] ?? "");
				ValidateFolderName validateFolderName = new ValidateFolderName(ModMinecraft.m_ProxyTests + "versions", true, true);
				if (Operators.CompareString(validateFolderName.Validate(CS$<>8__locals1.$VB$Local_VersionName), "", false) != 0)
				{
					CS$<>8__locals1.$VB$Local_VersionName = "";
				}
				if (Operators.CompareString(CS$<>8__locals1.$VB$Local_VersionName, "", false) == 0)
				{
					CS$<>8__locals1.$VB$Local_VersionName = ModMain.MyMsgBoxInput("输入版本名称", "", "", new Collection<Validate>
					{
						validateFolderName
					}, "", "确定", "取消", false);
				}
				if (string.IsNullOrEmpty(CS$<>8__locals1.$VB$Local_VersionName))
				{
					throw new ModBase.CancelledException();
				}
			}
			CS$<>8__locals1.$VB$Local_InstallTemp = ModBase.m_DecoratorRepository + "PackInstall\\" + Conversions.ToString(ModBase.RandomInteger(0, 100000)) + "\\";
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			list.Add(new ModLoader.LoaderTask<string, int>("解压整合包文件", delegate(ModLoader.LoaderTask<string, int> Task)
			{
				ModModpack._Closure$__7-1 CS$<>8__locals2 = new ModModpack._Closure$__7-1(CS$<>8__locals2);
				CS$<>8__locals2.$VB$Local_Task = Task;
				ModModpack.ExtractModpackFiles(CS$<>8__locals1.$VB$Local_InstallTemp, CS$<>8__locals1.$VB$Local_FileAddress, CS$<>8__locals2.$VB$Local_Task, 0.6);
				CS$<>8__locals2.$VB$Local_Task.Progress = 0.6;
				if (Directory.Exists(CS$<>8__locals1.$VB$Local_InstallTemp + CS$<>8__locals1.$VB$Local_ArchiveBaseFolder + "overrides"))
				{
					ModBase.CopyDirectory(CS$<>8__locals1.$VB$Local_InstallTemp + CS$<>8__locals1.$VB$Local_ArchiveBaseFolder + "overrides", ModMinecraft.m_ProxyTests + "versions\\" + CS$<>8__locals1.$VB$Local_VersionName, delegate(double Delta)
					{
						ModLoader.LoaderTask<string, int> $VB$Local_Task;
						($VB$Local_Task = CS$<>8__locals2.$VB$Local_Task).Progress = $VB$Local_Task.Progress + Delta * 0.25;
					});
				}
				else
				{
					ModBase.Log("[ModPack] 整合包中未找到 override 目录，已跳过", ModBase.LogLevel.Normal, "出现错误");
				}
				CS$<>8__locals2.$VB$Local_Task.Progress = 0.85;
				if (Directory.Exists(CS$<>8__locals1.$VB$Local_InstallTemp + CS$<>8__locals1.$VB$Local_ArchiveBaseFolder + "client-overrides"))
				{
					ModBase.CopyDirectory(CS$<>8__locals1.$VB$Local_InstallTemp + CS$<>8__locals1.$VB$Local_ArchiveBaseFolder + "client-overrides", ModMinecraft.m_ProxyTests + "versions\\" + CS$<>8__locals1.$VB$Local_VersionName, delegate(double Delta)
					{
						ModLoader.LoaderTask<string, int> $VB$Local_Task;
						($VB$Local_Task = CS$<>8__locals2.$VB$Local_Task).Progress = $VB$Local_Task.Progress + Delta * 0.1;
					});
				}
				CS$<>8__locals2.$VB$Local_Task.Progress = 0.95;
				ModBase.WriteIni(ModMinecraft.m_ProxyTests + "versions\\" + CS$<>8__locals1.$VB$Local_VersionName + "\\PCL\\Setup.ini", "VersionArgumentIndie", Conversions.ToString(1));
			}, null, ThreadPriority.Normal)
			{
				ProgressWeight = (double)new FileInfo(CS$<>8__locals1.$VB$Local_FileAddress).Length / 1024.0 / 1024.0 / 6.0,
				Block = false
			});
			List<ModNet.NetFile> list2 = new List<ModNet.NetFile>();
			try
			{
				foreach (JToken jtoken2 in (jobject["files"] ?? new byte[0]))
				{
					if (jtoken2["env"] != null)
					{
						string left2 = jtoken2["env"]["client"].ToString();
						if (Operators.CompareString(left2, "optional", false) != 0)
						{
							if (Operators.CompareString(left2, "unsupported", false) == 0)
							{
								continue;
							}
						}
						else if (ModMain.MyMsgBox("是否要下载整合包中的可选文件 " + ModBase.GetFileNameFromPath(jtoken2["path"].ToString()) + "？", "下载可选文件", "是", "否", "", false, true, false, null, null, null) == 2)
						{
							continue;
						}
					}
					List<string> list3 = Enumerable.ToList<string>(Enumerable.Select<JToken, string>(jtoken2["downloads"], (ModModpack._Closure$__.$I7-3 == null) ? (ModModpack._Closure$__.$I7-3 = ((JToken t) => t.ToString().Replace("://edge.forgecdn", "://media.forgecdn"))) : ModModpack._Closure$__.$I7-3));
					list3.AddRange(Enumerable.ToList<string>(Enumerable.Select<string, string>(list3, (ModModpack._Closure$__.$I7-4 == null) ? (ModModpack._Closure$__.$I7-4 = ((string u) => ModDownload.DlSourceModGet(u))) : ModModpack._Closure$__.$I7-4)));
					list3 = Enumerable.ToList<string>(Enumerable.Distinct<string>(list3));
					list2.Add(new ModNet.NetFile(list3, string.Concat(new string[]
					{
						ModMinecraft.m_ProxyTests,
						"versions\\",
						CS$<>8__locals1.$VB$Local_VersionName,
						"\\",
						jtoken2["path"].ToString()
					}), new ModBase.FileChecker(-1L, jtoken2["fileSize"].ToObject<long>(), jtoken2["hashes"]["sha1"].ToString(), true, false), true));
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
			if (Enumerable.Any<ModNet.NetFile>(list2))
			{
				list.Add(new ModNet.LoaderDownload("下载额外文件", list2)
				{
					ProgressWeight = (double)list2.Count * 1.5
				});
			}
			ModDownloadLib.McInstallRequest mcInstallRequest = new ModDownloadLib.McInstallRequest
			{
				m_SystemTest = CS$<>8__locals1.$VB$Local_VersionName,
				_ParamTest = string.Format("{0}versions\\{1}\\", ModMinecraft.m_ProxyTests, CS$<>8__locals1.$VB$Local_VersionName),
				_TagTest = tagTest,
				_RefTest = text,
				instanceTest = text2,
				m_CallbackTest = text3
			};
			List<ModLoader.LoaderBase> list4 = ModDownloadLib.McInstallLoader(mcInstallRequest, true);
			List<ModLoader.LoaderBase> list5 = new List<ModLoader.LoaderBase>();
			list5.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析游戏支持库文件（副加载器）", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
			{
				Task.Output = ModMinecraft.McLibFix(new ModMinecraft.McVersion(CS$<>8__locals1.$VB$Local_VersionName));
			}, null, ThreadPriority.Normal)
			{
				ProgressWeight = 1.0,
				Show = false
			});
			list5.Add(new ModNet.LoaderDownload("下载游戏支持库文件（副加载器）", new List<ModNet.NetFile>())
			{
				ProgressWeight = 7.0,
				Show = false
			});
			List<ModLoader.LoaderBase> list6 = new List<ModLoader.LoaderBase>();
			list6.Add(new ModLoader.LoaderCombo<string>("整合包安装", list)
			{
				Show = false,
				Block = false,
				ProgressWeight = Enumerable.Sum<ModLoader.LoaderBase>(list, (ModModpack._Closure$__.$I7-6 == null) ? (ModModpack._Closure$__.$I7-6 = ((ModLoader.LoaderBase l) => l.ProgressWeight)) : ModModpack._Closure$__.$I7-6)
			});
			list6.Add(new ModLoader.LoaderCombo<string>("游戏安装", list4)
			{
				Show = false,
				ProgressWeight = Enumerable.Sum<ModLoader.LoaderBase>(list4, (ModModpack._Closure$__.$I7-7 == null) ? (ModModpack._Closure$__.$I7-7 = ((ModLoader.LoaderBase l) => l.ProgressWeight)) : ModModpack._Closure$__.$I7-7)
			});
			list6.Add(new ModLoader.LoaderCombo<string>("下载游戏支持库文件", list5)
			{
				ProgressWeight = 8.0
			});
			list6.Add(new ModLoader.LoaderTask<string, string>("最终整理文件", delegate(ModLoader.LoaderTask<string, string> Task)
			{
				string str = ModMinecraft.m_ProxyTests + "versions\\" + CS$<>8__locals1.$VB$Local_VersionName + "\\";
				if (CS$<>8__locals1.$VB$Local_Logo != null && File.Exists(CS$<>8__locals1.$VB$Local_Logo))
				{
					File.Copy(CS$<>8__locals1.$VB$Local_Logo, str + "PCL\\Logo.png", true);
					ModBase.WriteIni(str + "PCL\\Setup.ini", "Logo", "PCL\\Logo.png");
					ModBase.WriteIni(str + "PCL\\Setup.ini", "LogoCustom", "True");
					ModBase.Log("[ModPack] 已设置整合包 Logo：" + CS$<>8__locals1.$VB$Local_Logo, ModBase.LogLevel.Normal, "出现错误");
				}
				foreach (string text4 in new string[]
				{
					str + "原始整合包.zip",
					str + "原始整合包.mrpack"
				})
				{
					if (File.Exists(text4))
					{
						ModBase.Log("[ModPack] 删除原始整合包文件：" + text4, ModBase.LogLevel.Normal, "出现错误");
						File.Delete(text4);
					}
				}
			}, null, ThreadPriority.Normal)
			{
				ProgressWeight = 0.1,
				Show = false
			});
			CS$<>8__locals1.$VB$Local_LoaderName = "Modrinth 整合包安装：" + CS$<>8__locals1.$VB$Local_VersionName + " ";
			if (Enumerable.Any<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar, (ModLoader.LoaderBase l) => Operators.CompareString(l.Name, CS$<>8__locals1.$VB$Local_LoaderName, false) == 0))
			{
				ModMain.Hint("该整合包正在安装中！", ModMain.HintType.Critical, true);
				throw new ModBase.CancelledException();
			}
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(CS$<>8__locals1.$VB$Local_LoaderName, list6);
			loaderCombo.OnStateChanged = new Action<ModLoader.LoaderBase>(ModDownloadLib.McInstallState);
			loaderCombo.Start(mcInstallRequest._ParamTest, false);
			ModLoader.LoaderTaskbarAdd<string>(loaderCombo);
			ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
			ModBase.RunInUi((ModModpack._Closure$__.$I7-10 == null) ? (ModModpack._Closure$__.$I7-10 = delegate()
			{
				ModMain._ProcessIterator.PageChange(FormMain.PageType.DownloadManager, FormMain.PageSubType.Default);
			}) : ModModpack._Closure$__.$I7-10, false);
			return loaderCombo;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000220A0 File Offset: 0x000202A0
		private static ModLoader.LoaderCombo<string> InstallPackHMCL(string FileAddress, ZipArchive Archive, string ArchiveBaseFolder)
		{
			JObject jobject;
			try
			{
				jobject = (JObject)ModBase.GetJson(ModBase.ReadFile(Archive.GetEntry(ArchiveBaseFolder + "modpack.json").Open(), Encoding.UTF8));
			}
			catch (Exception innerException)
			{
				throw new Exception("HMCL 整合包安装信息存在问题", innerException);
			}
			string VersionName = (string)(jobject["name"] ?? "");
			ValidateFolderName validateFolderName = new ValidateFolderName(ModMinecraft.m_ProxyTests + "versions", true, true);
			if (Operators.CompareString(validateFolderName.Validate(VersionName), "", false) != 0)
			{
				VersionName = "";
			}
			if (Operators.CompareString(VersionName, "", false) == 0)
			{
				VersionName = ModMain.MyMsgBoxInput("输入版本名称", "", "", new Collection<Validate>
				{
					validateFolderName
				}, "", "确定", "取消", false);
			}
			if (string.IsNullOrEmpty(VersionName))
			{
				throw new ModBase.CancelledException();
			}
			string InstallTemp = ModBase.m_DecoratorRepository + "PackInstall\\" + Conversions.ToString(ModBase.RandomInteger(0, 100000)) + "\\";
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			list.Add(new ModLoader.LoaderTask<string, int>("解压整合包文件", delegate(ModLoader.LoaderTask<string, int> Task)
			{
				ModModpack.ExtractModpackFiles(InstallTemp, FileAddress, Task, 0.6);
				Task.Progress = 0.6;
				if (Directory.Exists(InstallTemp + ArchiveBaseFolder + "minecraft"))
				{
					ModBase.CopyDirectory(InstallTemp + ArchiveBaseFolder + "minecraft", ModMinecraft.m_ProxyTests + "versions\\" + VersionName, delegate(double Delta)
					{
						ModLoader.LoaderTask<string, int> $VB$Local_Task;
						($VB$Local_Task = Task).Progress = $VB$Local_Task.Progress + Delta * 0.35;
					});
				}
				else
				{
					ModBase.Log("[ModPack] 整合包中未找到 minecraft override 目录，已跳过", ModBase.LogLevel.Normal, "出现错误");
				}
				Task.Progress = 0.95;
				ModBase.WriteIni(ModMinecraft.m_ProxyTests + "versions\\" + VersionName + "\\PCL\\Setup.ini", "VersionArgumentIndie", Conversions.ToString(1));
			}, null, ThreadPriority.Normal)
			{
				ProgressWeight = (double)new FileInfo(FileAddress).Length / 1024.0 / 1024.0 / 6.0,
				Block = false
			});
			if (jobject["gameVersion"] == null)
			{
				throw new Exception("该 HMCL 整合包未提供游戏版本信息，无法安装！");
			}
			ModDownloadLib.McInstallRequest mcInstallRequest = new ModDownloadLib.McInstallRequest
			{
				m_SystemTest = VersionName,
				_ParamTest = string.Format("{0}versions\\{1}\\", ModMinecraft.m_ProxyTests, VersionName),
				_TagTest = jobject["gameVersion"].ToString()
			};
			List<ModLoader.LoaderBase> list2 = ModDownloadLib.McInstallLoader(mcInstallRequest, true);
			List<ModLoader.LoaderBase> list3 = new List<ModLoader.LoaderBase>();
			list3.Add(new ModLoader.LoaderTask<string, string>("重命名版本 Json（副加载器）", delegate(ModLoader.LoaderTask<string, string> a0)
			{
				base._Lambda$__2();
			}, null, ThreadPriority.Normal)
			{
				ProgressWeight = 0.1,
				Show = false
			});
			list3.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析游戏支持库文件（副加载器）", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
			{
				Task.Output = ModMinecraft.McLibFix(new ModMinecraft.McVersion(VersionName));
			}, null, ThreadPriority.Normal)
			{
				ProgressWeight = 1.0,
				Show = false
			});
			list3.Add(new ModNet.LoaderDownload("下载游戏支持库文件（副加载器）", new List<ModNet.NetFile>())
			{
				ProgressWeight = 7.0,
				Show = false
			});
			List<ModLoader.LoaderBase> loaders = new List<ModLoader.LoaderBase>
			{
				new ModLoader.LoaderCombo<string>("游戏安装", list2)
				{
					Show = false,
					Block = false,
					ProgressWeight = Enumerable.Sum<ModLoader.LoaderBase>(list2, (ModModpack._Closure$__.$I8-4 == null) ? (ModModpack._Closure$__.$I8-4 = ((ModLoader.LoaderBase l) => l.ProgressWeight)) : ModModpack._Closure$__.$I8-4)
				},
				new ModLoader.LoaderCombo<string>("整合包安装", list)
				{
					Show = false,
					ProgressWeight = Enumerable.Sum<ModLoader.LoaderBase>(list, (ModModpack._Closure$__.$I8-5 == null) ? (ModModpack._Closure$__.$I8-5 = ((ModLoader.LoaderBase l) => l.ProgressWeight)) : ModModpack._Closure$__.$I8-5)
				},
				new ModLoader.LoaderCombo<string>("下载游戏支持库文件", list3)
				{
					ProgressWeight = 8.0
				}
			};
			string LoaderName = "HMCL 整合包安装：" + VersionName + " ";
			if (Enumerable.Any<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar, (ModLoader.LoaderBase l) => Operators.CompareString(l.Name, LoaderName, false) == 0))
			{
				ModMain.Hint("该整合包正在安装中！", ModMain.HintType.Critical, true);
				throw new ModBase.CancelledException();
			}
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(LoaderName, loaders);
			loaderCombo.OnStateChanged = new Action<ModLoader.LoaderBase>(ModDownloadLib.McInstallState);
			loaderCombo.Start(mcInstallRequest._ParamTest, false);
			ModLoader.LoaderTaskbarAdd<string>(loaderCombo);
			ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
			ModBase.RunInUi((ModModpack._Closure$__.$I8-7 == null) ? (ModModpack._Closure$__.$I8-7 = delegate()
			{
				ModMain._ProcessIterator.PageChange(FormMain.PageType.DownloadManager, FormMain.PageSubType.Default);
			}) : ModModpack._Closure$__.$I8-7, false);
			return loaderCombo;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000224D4 File Offset: 0x000206D4
		private static ModLoader.LoaderCombo<string> InstallPackMMC(string FileAddress, ZipArchive Archive, string ArchiveBaseFolder)
		{
			ModModpack._Closure$__9-0 CS$<>8__locals1 = new ModModpack._Closure$__9-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_FileAddress = FileAddress;
			CS$<>8__locals1.$VB$Local_ArchiveBaseFolder = ArchiveBaseFolder;
			JObject jobject;
			string str;
			try
			{
				jobject = (JObject)ModBase.GetJson(ModBase.ReadFile(Archive.GetEntry(CS$<>8__locals1.$VB$Local_ArchiveBaseFolder + "mmc-pack.json").Open(), Encoding.UTF8));
				str = ModBase.ReadFile(Archive.GetEntry(CS$<>8__locals1.$VB$Local_ArchiveBaseFolder + "instance.cfg").Open(), Encoding.UTF8);
			}
			catch (Exception innerException)
			{
				throw new Exception("MMC 整合包安装信息存在问题", innerException);
			}
			CS$<>8__locals1.$VB$Local_VersionName = (ModBase.RegexSeek(str, "(?<=\\nname\\=)[^\\n]+", 0) ?? "");
			ValidateFolderName validateFolderName = new ValidateFolderName(ModMinecraft.m_ProxyTests + "versions", true, true);
			if (Operators.CompareString(validateFolderName.Validate(CS$<>8__locals1.$VB$Local_VersionName), "", false) != 0)
			{
				CS$<>8__locals1.$VB$Local_VersionName = "";
			}
			if (Operators.CompareString(CS$<>8__locals1.$VB$Local_VersionName, "", false) == 0)
			{
				CS$<>8__locals1.$VB$Local_VersionName = ModMain.MyMsgBoxInput("输入版本名称", "", "", new Collection<Validate>
				{
					validateFolderName
				}, "", "确定", "取消", false);
			}
			if (string.IsNullOrEmpty(CS$<>8__locals1.$VB$Local_VersionName))
			{
				throw new ModBase.CancelledException();
			}
			CS$<>8__locals1.$VB$Local_InstallTemp = string.Format("{0}PackInstall\\{1}\\", ModBase.m_DecoratorRepository, ModBase.RandomInteger(0, 100000));
			CS$<>8__locals1.$VB$Local_SetupFile = string.Format("{0}versions\\{1}\\PCL\\Setup.ini", ModMinecraft.m_ProxyTests, CS$<>8__locals1.$VB$Local_VersionName);
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			list.Add(new ModLoader.LoaderTask<string, int>("解压整合包文件", delegate(ModLoader.LoaderTask<string, int> Task)
			{
				ModModpack._Closure$__9-1 CS$<>8__locals2 = new ModModpack._Closure$__9-1(CS$<>8__locals2);
				CS$<>8__locals2.$VB$Local_Task = Task;
				ModModpack.ExtractModpackFiles(CS$<>8__locals1.$VB$Local_InstallTemp, CS$<>8__locals1.$VB$Local_FileAddress, CS$<>8__locals2.$VB$Local_Task, 0.6);
				CS$<>8__locals2.$VB$Local_Task.Progress = 0.6;
				if (Directory.Exists(CS$<>8__locals1.$VB$Local_InstallTemp + CS$<>8__locals1.$VB$Local_ArchiveBaseFolder + ".minecraft"))
				{
					ModBase.CopyDirectory(CS$<>8__locals1.$VB$Local_InstallTemp + CS$<>8__locals1.$VB$Local_ArchiveBaseFolder + ".minecraft", ModMinecraft.m_ProxyTests + "versions\\" + CS$<>8__locals1.$VB$Local_VersionName, delegate(double Delta)
					{
						ModLoader.LoaderTask<string, int> $VB$Local_Task;
						($VB$Local_Task = CS$<>8__locals2.$VB$Local_Task).Progress = $VB$Local_Task.Progress + Delta * 0.35;
					});
				}
				else
				{
					ModBase.Log("[ModPack] 整合包中未找到 override .minecraft 目录，已跳过", ModBase.LogLevel.Normal, "出现错误");
				}
				CS$<>8__locals2.$VB$Local_Task.Progress = 0.95;
				ModBase.WriteIni(CS$<>8__locals1.$VB$Local_SetupFile, "VersionArgumentIndie", Conversions.ToString(1));
				try
				{
					string text = CS$<>8__locals1.$VB$Local_InstallTemp + CS$<>8__locals1.$VB$Local_ArchiveBaseFolder + "instance.cfg";
					if (File.Exists(text))
					{
						ModBase.WriteFile(text, ModBase.ReadFile(text, null).Replace("=", ":"), false, null);
						if (Conversions.ToBoolean(ModBase.ReadIni(text, "OverrideCommands", Conversions.ToString(false))))
						{
							string text2 = ModBase.ReadIni(text, "PreLaunchCommand", "");
							if (Operators.CompareString(text2, "", false) != 0)
							{
								text2 = text2.Replace("\\\"", "\"").Replace("$INST_JAVA", "{java}javaw.exe").Replace("$INST_MC_DIR\\", "{minecraft}").Replace("$INST_MC_DIR", "{minecraft}").Replace("$INST_DIR\\", "{verpath}").Replace("$INST_DIR", "{verpath}").Replace("$INST_ID", "{name}").Replace("$INST_NAME", "{name}");
								ModBase.WriteIni(CS$<>8__locals1.$VB$Local_SetupFile, "VersionAdvanceRun", text2);
								ModBase.Log("[ModPack] 迁移 MultiMC 版本独立设置：启动前执行命令：" + text2, ModBase.LogLevel.Normal, "出现错误");
							}
						}
						if (Conversions.ToBoolean(ModBase.ReadIni(text, "JoinServerOnLaunch", Conversions.ToString(false))))
						{
							string text3 = ModBase.ReadIni(text, "JoinServerOnLaunchAddress", "").Replace("\\\"", "\"");
							ModBase.WriteIni(CS$<>8__locals1.$VB$Local_SetupFile, "VersionServerEnter", text3);
							ModBase.Log("[ModPack] 迁移 MultiMC 版本独立设置：自动进入服务器：" + text3, ModBase.LogLevel.Normal, "出现错误");
						}
						if (Conversions.ToBoolean(ModBase.ReadIni(text, "IgnoreJavaCompatibility", Conversions.ToString(false))))
						{
							ModBase.WriteIni(CS$<>8__locals1.$VB$Local_SetupFile, "VersionAdvanceJava", Conversions.ToString(true));
							ModBase.Log("[ModPack] 迁移 MultiMC 版本独立设置：忽略 Java 兼容性警告", ModBase.LogLevel.Normal, "出现错误");
						}
						string text4 = ModBase.ReadIni(text, "iconKey", "");
						if (Operators.CompareString(text4, "", false) != 0 && File.Exists(string.Format("{0}{1}{2}.png", CS$<>8__locals1.$VB$Local_InstallTemp, CS$<>8__locals1.$VB$Local_ArchiveBaseFolder, text4)))
						{
							ModBase.WriteIni(CS$<>8__locals1.$VB$Local_SetupFile, "LogoCustom", Conversions.ToString(true));
							ModBase.WriteIni(CS$<>8__locals1.$VB$Local_SetupFile, "Logo", "PCL\\Logo.png");
							ModBase.CopyFile(string.Format("{0}{1}{2}.png", CS$<>8__locals1.$VB$Local_InstallTemp, CS$<>8__locals1.$VB$Local_ArchiveBaseFolder, text4), string.Format("{0}versions\\{1}\\PCL\\Logo.png", ModMinecraft.m_ProxyTests, CS$<>8__locals1.$VB$Local_VersionName));
							ModBase.Log(string.Format("[ModPack] 迁移 MultiMC 版本独立设置：版本图标（{0}.png）", text4), ModBase.LogLevel.Normal, "出现错误");
						}
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, string.Format("读取 MMC 配置文件失败（{0}{1}instance.cfg）", CS$<>8__locals1.$VB$Local_InstallTemp, CS$<>8__locals1.$VB$Local_ArchiveBaseFolder), ModBase.LogLevel.Debug, "出现错误");
				}
			}, null, ThreadPriority.Normal)
			{
				ProgressWeight = (double)new FileInfo(CS$<>8__locals1.$VB$Local_FileAddress).Length / 1024.0 / 1024.0 / 6.0,
				Block = false
			});
			if (jobject["components"] == null)
			{
				throw new Exception("该 MMC 整合包未提供游戏版本信息，无法安装！");
			}
			ModDownloadLib.McInstallRequest mcInstallRequest = new ModDownloadLib.McInstallRequest
			{
				m_SystemTest = CS$<>8__locals1.$VB$Local_VersionName,
				_ParamTest = string.Format("{0}versions\\{1}\\", ModMinecraft.m_ProxyTests, CS$<>8__locals1.$VB$Local_VersionName)
			};
			try
			{
				foreach (JToken jtoken in jobject["components"])
				{
					string left = (jtoken["uid"] ?? "").ToString();
					if (Operators.CompareString(left, "org.lwjgl", false) != 0)
					{
						if (Operators.CompareString(left, "net.minecraft", false) != 0)
						{
							if (Operators.CompareString(left, "net.minecraftforge", false) != 0)
							{
								if (Operators.CompareString(left, "net.neoforged", false) != 0)
								{
									if (Operators.CompareString(left, "net.fabricmc.fabric-loader", false) != 0)
									{
										if (Operators.CompareString(left, "org.quiltmc.quilt-loader", false) == 0)
										{
											ModMain.Hint("PCL 暂不支持安装需要 Quilt 的整合包！", ModMain.HintType.Critical, true);
											throw new ModBase.CancelledException();
										}
									}
									else
									{
										mcInstallRequest.m_CallbackTest = (string)jtoken["version"];
									}
								}
								else
								{
									mcInstallRequest.instanceTest = (string)jtoken["version"];
								}
							}
							else
							{
								mcInstallRequest._RefTest = (string)jtoken["version"];
							}
						}
						else
						{
							mcInstallRequest._TagTest = (string)jtoken["version"];
						}
					}
					else
					{
						ModBase.Log("[ModPack] 已跳过 LWJGL 项", ModBase.LogLevel.Normal, "出现错误");
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
			List<ModLoader.LoaderBase> list2 = ModDownloadLib.McInstallLoader(mcInstallRequest, true);
			List<ModLoader.LoaderBase> list3 = new List<ModLoader.LoaderBase>();
			list3.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析游戏支持库文件（副加载器）", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
			{
				Task.Output = ModMinecraft.McLibFix(new ModMinecraft.McVersion(CS$<>8__locals1.$VB$Local_VersionName));
			}, null, ThreadPriority.Normal)
			{
				ProgressWeight = 1.0,
				Show = false
			});
			list3.Add(new ModNet.LoaderDownload("下载游戏支持库文件（副加载器）", new List<ModNet.NetFile>())
			{
				ProgressWeight = 7.0,
				Show = false
			});
			List<ModLoader.LoaderBase> list4 = new List<ModLoader.LoaderBase>();
			list4.Add(new ModLoader.LoaderCombo<string>("游戏安装", list2)
			{
				Show = false,
				Block = false,
				ProgressWeight = Enumerable.Sum<ModLoader.LoaderBase>(list2, (ModModpack._Closure$__.$I9-3 == null) ? (ModModpack._Closure$__.$I9-3 = ((ModLoader.LoaderBase l) => l.ProgressWeight)) : ModModpack._Closure$__.$I9-3)
			});
			list4.Add(new ModLoader.LoaderCombo<string>("整合包安装", list)
			{
				Show = false,
				ProgressWeight = Enumerable.Sum<ModLoader.LoaderBase>(list, (ModModpack._Closure$__.$I9-4 == null) ? (ModModpack._Closure$__.$I9-4 = ((ModLoader.LoaderBase l) => l.ProgressWeight)) : ModModpack._Closure$__.$I9-4)
			});
			list4.Add(new ModLoader.LoaderCombo<string>("下载游戏支持库文件", list3)
			{
				ProgressWeight = 8.0
			});
			CS$<>8__locals1.$VB$Local_LoaderName = "MMC 整合包安装：" + CS$<>8__locals1.$VB$Local_VersionName + " ";
			if (Enumerable.Any<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar, (ModLoader.LoaderBase l) => Operators.CompareString(l.Name, CS$<>8__locals1.$VB$Local_LoaderName, false) == 0))
			{
				ModMain.Hint("该整合包正在安装中！", ModMain.HintType.Critical, true);
				throw new ModBase.CancelledException();
			}
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(CS$<>8__locals1.$VB$Local_LoaderName, list4);
			loaderCombo.OnStateChanged = new Action<ModLoader.LoaderBase>(ModDownloadLib.McInstallState);
			loaderCombo.Start(mcInstallRequest._ParamTest, false);
			ModLoader.LoaderTaskbarAdd<string>(loaderCombo);
			ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
			ModBase.RunInUi((ModModpack._Closure$__.$I9-6 == null) ? (ModModpack._Closure$__.$I9-6 = delegate()
			{
				ModMain._ProcessIterator.PageChange(FormMain.PageType.DownloadManager, FormMain.PageSubType.Default);
			}) : ModModpack._Closure$__.$I9-6, false);
			return loaderCombo;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00022A68 File Offset: 0x00020C68
		private static ModLoader.LoaderCombo<string> InstallPackMCBBS(string FileAddress, ZipArchive Archive, string ArchiveBaseFolder, string VersionName = null)
		{
			ModModpack._Closure$__10-0 CS$<>8__locals1 = new ModModpack._Closure$__10-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_FileAddress = FileAddress;
			CS$<>8__locals1.$VB$Local_ArchiveBaseFolder = ArchiveBaseFolder;
			CS$<>8__locals1.$VB$Local_VersionName = VersionName;
			JObject jobject;
			try
			{
				jobject = (JObject)ModBase.GetJson(ModBase.ReadFile((Archive.GetEntry(CS$<>8__locals1.$VB$Local_ArchiveBaseFolder + "mcbbs.packmeta") ?? Archive.GetEntry(CS$<>8__locals1.$VB$Local_ArchiveBaseFolder + "manifest.json")).Open(), Encoding.UTF8));
			}
			catch (Exception innerException)
			{
				throw new Exception("MCBBS 整合包安装信息存在问题", innerException);
			}
			if (CS$<>8__locals1.$VB$Local_VersionName == null)
			{
				CS$<>8__locals1.$VB$Local_VersionName = (string)(jobject["name"] ?? "");
				ValidateFolderName validateFolderName = new ValidateFolderName(ModMinecraft.m_ProxyTests + "versions", true, true);
				if (Operators.CompareString(validateFolderName.Validate(CS$<>8__locals1.$VB$Local_VersionName), "", false) != 0)
				{
					CS$<>8__locals1.$VB$Local_VersionName = "";
				}
				if (Operators.CompareString(CS$<>8__locals1.$VB$Local_VersionName, "", false) == 0)
				{
					CS$<>8__locals1.$VB$Local_VersionName = ModMain.MyMsgBoxInput("输入版本名称", "", "", new Collection<Validate>
					{
						validateFolderName
					}, "", "确定", "取消", false);
				}
				if (string.IsNullOrEmpty(CS$<>8__locals1.$VB$Local_VersionName))
				{
					throw new ModBase.CancelledException();
				}
			}
			CS$<>8__locals1.$VB$Local_InstallTemp = ModBase.m_DecoratorRepository + "PackInstall\\" + Conversions.ToString(ModBase.RandomInteger(0, 100000)) + "\\";
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			list.Add(new ModLoader.LoaderTask<string, int>("解压整合包文件", delegate(ModLoader.LoaderTask<string, int> Task)
			{
				ModModpack._Closure$__10-1 CS$<>8__locals2 = new ModModpack._Closure$__10-1(CS$<>8__locals2);
				CS$<>8__locals2.$VB$Local_Task = Task;
				ModModpack.ExtractModpackFiles(CS$<>8__locals1.$VB$Local_InstallTemp, CS$<>8__locals1.$VB$Local_FileAddress, CS$<>8__locals2.$VB$Local_Task, 0.6);
				CS$<>8__locals2.$VB$Local_Task.Progress = 0.6;
				if (Directory.Exists(CS$<>8__locals1.$VB$Local_InstallTemp + CS$<>8__locals1.$VB$Local_ArchiveBaseFolder + "overrides"))
				{
					ModBase.CopyDirectory(CS$<>8__locals1.$VB$Local_InstallTemp + CS$<>8__locals1.$VB$Local_ArchiveBaseFolder + "overrides", ModMinecraft.m_ProxyTests + "versions\\" + CS$<>8__locals1.$VB$Local_VersionName, delegate(double Delta)
					{
						ModLoader.LoaderTask<string, int> $VB$Local_Task;
						($VB$Local_Task = CS$<>8__locals2.$VB$Local_Task).Progress = $VB$Local_Task.Progress + 0.35 * Delta;
					});
				}
				else
				{
					ModBase.Log("[ModPack] 整合包中未找到 overrides 目录，已跳过", ModBase.LogLevel.Normal, "出现错误");
				}
				CS$<>8__locals2.$VB$Local_Task.Progress = 0.95;
				ModBase.WriteIni(ModMinecraft.m_ProxyTests + "versions\\" + CS$<>8__locals1.$VB$Local_VersionName + "\\PCL\\Setup.ini", "VersionArgumentIndie", Conversions.ToString(1));
			}, null, ThreadPriority.Normal)
			{
				ProgressWeight = (double)new FileInfo(CS$<>8__locals1.$VB$Local_FileAddress).Length / 1024.0 / 1024.0 / 6.0,
				Block = false
			});
			if (jobject["addons"] == null)
			{
				throw new Exception("该 MCBBS 整合包未提供游戏版本附加信息，无法安装！");
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			try
			{
				foreach (JToken jtoken in jobject["addons"])
				{
					dictionary.Add((string)jtoken["id"], (string)jtoken["version"]);
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
			if (!dictionary.ContainsKey("game"))
			{
				throw new Exception("该 MCBBS 整合包未提供游戏版本信息，无法安装！");
			}
			if (dictionary.ContainsKey("quilt"))
			{
				ModMain.Hint("PCL 暂不支持安装需要 Quilt 的整合包！", ModMain.HintType.Critical, true);
				throw new ModBase.CancelledException();
			}
			ModDownloadLib.McInstallRequest mcInstallRequest = new ModDownloadLib.McInstallRequest
			{
				m_SystemTest = CS$<>8__locals1.$VB$Local_VersionName,
				_ParamTest = string.Format("{0}versions\\{1}\\", ModMinecraft.m_ProxyTests, CS$<>8__locals1.$VB$Local_VersionName),
				_TagTest = dictionary["game"],
				_StubTest = (dictionary.ContainsKey("optifine") ? dictionary["optifine"] : null),
				_RefTest = (dictionary.ContainsKey("forge") ? dictionary["forge"] : null),
				instanceTest = (dictionary.ContainsKey("neoforge") ? dictionary["neoforge"] : null),
				m_CallbackTest = (dictionary.ContainsKey("fabric") ? dictionary["fabric"] : null)
			};
			List<ModLoader.LoaderBase> list2 = ModDownloadLib.McInstallLoader(mcInstallRequest, true);
			List<ModLoader.LoaderBase> list3 = new List<ModLoader.LoaderBase>();
			list3.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析游戏支持库文件（副加载器）", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
			{
				Task.Output = ModMinecraft.McLibFix(new ModMinecraft.McVersion(CS$<>8__locals1.$VB$Local_VersionName));
			}, null, ThreadPriority.Normal)
			{
				ProgressWeight = 1.0,
				Show = false
			});
			list3.Add(new ModNet.LoaderDownload("下载游戏支持库文件（副加载器）", new List<ModNet.NetFile>())
			{
				ProgressWeight = 7.0,
				Show = false
			});
			List<ModLoader.LoaderBase> list4 = new List<ModLoader.LoaderBase>();
			list4.Add(new ModLoader.LoaderCombo<string>("游戏安装", list2)
			{
				Show = false,
				Block = false,
				ProgressWeight = Enumerable.Sum<ModLoader.LoaderBase>(list2, (ModModpack._Closure$__.$I10-3 == null) ? (ModModpack._Closure$__.$I10-3 = ((ModLoader.LoaderBase l) => l.ProgressWeight)) : ModModpack._Closure$__.$I10-3)
			});
			list4.Add(new ModLoader.LoaderCombo<string>("整合包安装", list)
			{
				Show = false,
				ProgressWeight = Enumerable.Sum<ModLoader.LoaderBase>(list, (ModModpack._Closure$__.$I10-4 == null) ? (ModModpack._Closure$__.$I10-4 = ((ModLoader.LoaderBase l) => l.ProgressWeight)) : ModModpack._Closure$__.$I10-4)
			});
			list4.Add(new ModLoader.LoaderCombo<string>("下载游戏支持库文件", list3)
			{
				ProgressWeight = 8.0
			});
			CS$<>8__locals1.$VB$Local_LoaderName = "MCBBS 整合包安装：" + CS$<>8__locals1.$VB$Local_VersionName + " ";
			if (Enumerable.Any<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar, (ModLoader.LoaderBase l) => Operators.CompareString(l.Name, CS$<>8__locals1.$VB$Local_LoaderName, false) == 0))
			{
				ModMain.Hint("该整合包正在安装中！", ModMain.HintType.Critical, true);
				throw new ModBase.CancelledException();
			}
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(CS$<>8__locals1.$VB$Local_LoaderName, list4);
			loaderCombo.OnStateChanged = new Action<ModLoader.LoaderBase>(ModDownloadLib.McInstallState);
			loaderCombo.Start(mcInstallRequest._ParamTest, false);
			ModLoader.LoaderTaskbarAdd<string>(loaderCombo);
			ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
			ModBase.RunInUi((ModModpack._Closure$__.$I10-6 == null) ? (ModModpack._Closure$__.$I10-6 = delegate()
			{
				ModMain._ProcessIterator.PageChange(FormMain.PageType.DownloadManager, FormMain.PageSubType.Default);
			}) : ModModpack._Closure$__.$I10-6, false);
			return loaderCombo;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00022FBC File Offset: 0x000211BC
		private static ModLoader.LoaderCombo<string> InstallPackLauncherPack(string FileAddress, ZipArchive Archive, string ArchiveBaseFolder)
		{
			ModModpack._Closure$__11-0 CS$<>8__locals1 = new ModModpack._Closure$__11-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_FileAddress = FileAddress;
			CS$<>8__locals1.$VB$Local_ArchiveBaseFolder = ArchiveBaseFolder;
			ModMain.MyMsgBox("接下来请选择一个空文件夹，它会被安装到这个文件夹里。", "安装", "继续", "", "", false, true, true, null, null, null);
			CS$<>8__locals1.$VB$Local_TargetFolder = ModBase.SelectFolder("选择安装目标（必须是一个空文件夹）");
			if (string.IsNullOrEmpty(CS$<>8__locals1.$VB$Local_TargetFolder))
			{
				throw new ModBase.CancelledException();
			}
			if (CS$<>8__locals1.$VB$Local_TargetFolder.Contains("!") || CS$<>8__locals1.$VB$Local_TargetFolder.Contains(";"))
			{
				ModMain.Hint("Minecraft 文件夹路径中不能含有感叹号或分号！", ModMain.HintType.Critical, true);
				throw new ModBase.CancelledException();
			}
			if (Directory.GetFileSystemEntries(CS$<>8__locals1.$VB$Local_TargetFolder).Length > 0)
			{
				ModMain.Hint("请选择一个空文件夹作为安装目标！", ModMain.HintType.Critical, true);
				throw new ModBase.CancelledException();
			}
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("解压压缩包", new ModLoader.LoaderBase[]
			{
				new ModLoader.LoaderTask<string, int>("解压压缩包", delegate(ModLoader.LoaderTask<string, int> Task)
				{
					ModModpack.ExtractModpackFiles(CS$<>8__locals1.$VB$Local_TargetFolder, CS$<>8__locals1.$VB$Local_FileAddress, Task, 0.9);
					Thread.Sleep(400);
					string text = null;
					foreach (string text2 in Directory.GetFiles(CS$<>8__locals1.$VB$Local_TargetFolder, "*.exe", SearchOption.AllDirectories))
					{
						FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(text2);
						ModBase.Log(string.Format("[Modpack] 文件 {0} 的产品名标识为 {1}", text2, versionInfo.ProductName), ModBase.LogLevel.Normal, "出现错误");
						if (Operators.CompareString(versionInfo.ProductName, "Plain Craft Launcher", false) == 0)
						{
							text = text2;
						}
						else if ((versionInfo.ProductName.ContainsF("Launcher", true) || versionInfo.ProductName.ContainsF("启动器", true)) && Operators.CompareString(versionInfo.ProductName, "Plain Craft Launcher Admin Manager", false) != 0 && text == null)
						{
							text = text2;
						}
					}
					Task.Progress = 0.95;
					if (text != null)
					{
						ModBase.Log("[Modpack] 找到压缩包中附带的启动器：" + text, ModBase.LogLevel.Normal, "出现错误");
						if (ModMain.MyMsgBox(string.Format("整合包中似乎自带了启动器，是否换用它继续安装？{0}通常推荐这样做，以获得最佳体验。{1}即将打开：{2}", "\r\n", "\r\n", text), "换用整合包启动器？", "继续", "取消", "", false, true, false, null, null, null) == 1)
						{
							ModBase.ShellOnly(text, "--wait");
							ModBase.Log("[Modpack] 为换用整合包中的启动器启动，强制结束程序", ModBase.LogLevel.Normal, "出现错误");
							ModMain._ProcessIterator.EndProgram(false);
							return;
						}
					}
					else
					{
						ModBase.Log("[Modpack] 未找到压缩包中附带的启动器", ModBase.LogLevel.Normal, "出现错误");
					}
					string folderNameFromPath = ModBase.GetFolderNameFromPath(CS$<>8__locals1.$VB$Local_TargetFolder);
					PageSelectLeft.AddFolder(CS$<>8__locals1.$VB$Local_TargetFolder + CS$<>8__locals1.$VB$Local_ArchiveBaseFolder.Replace("/", "\\").TrimStart(new char[]
					{
						'\\'
					}), folderNameFromPath, false);
					string text3 = Enumerable.First<string>(Directory.GetFiles(CS$<>8__locals1.$VB$Local_TargetFolder, "modpack.*", SearchOption.AllDirectories));
					ModBase.Log("[Modpack] 调用 modpack 文件继续安装：" + text3, ModBase.LogLevel.Normal, "出现错误");
					ModModpack.ModpackInstall(text3, folderNameFromPath, null);
				}, null, ThreadPriority.Normal)
			});
			loaderCombo.Start(CS$<>8__locals1.$VB$Local_TargetFolder, false);
			ModLoader.LoaderTaskbarAdd<string>(loaderCombo);
			ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
			ModMain._ProcessIterator.BtnExtraDownload.Ribble();
			return loaderCombo;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000230DC File Offset: 0x000212DC
		private static ModLoader.LoaderCombo<string> InstallPackCompress(string FileAddress, ZipArchive Archive)
		{
			ModModpack._Closure$__12-0 CS$<>8__locals1 = new ModModpack._Closure$__12-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_FileAddress = FileAddress;
			Match match = null;
			Regex regex = new Regex("^.*\\/(?=versions\\/(?<ver>[^\\/]+)\\/(\\k<ver>)\\.json$)", 1);
			try
			{
				foreach (ZipArchiveEntry zipArchiveEntry in Archive.Entries)
				{
					Match match2 = regex.Match("/" + zipArchiveEntry.FullName);
					if (match2.Success)
					{
						match = match2;
						break;
					}
				}
			}
			finally
			{
				IEnumerator<ZipArchiveEntry> enumerator;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
			if (match == null)
			{
				throw new Exception("未能找到适合的文件结构，这可能不是一个 MC 压缩包");
			}
			CS$<>8__locals1.$VB$Local_ArchiveBaseFolder = match.Value.Replace("/", "\\").TrimStart(new char[]
			{
				'\\'
			});
			string value = match.Groups[1].Value;
			ModBase.Log("[ModPack] 检测到压缩包的 .minecraft 根目录：" + CS$<>8__locals1.$VB$Local_ArchiveBaseFolder + "，命中的版本名：" + value, ModBase.LogLevel.Normal, "出现错误");
			ModMain.MyMsgBox("接下来请选择一个空文件夹，它会被安装到这个文件夹里。", "安装", "继续", "", "", false, true, true, null, null, null);
			CS$<>8__locals1.$VB$Local_TargetFolder = ModBase.SelectFolder("选择安装目标（必须是一个空文件夹）");
			if (string.IsNullOrEmpty(CS$<>8__locals1.$VB$Local_TargetFolder))
			{
				throw new ModBase.CancelledException();
			}
			if (CS$<>8__locals1.$VB$Local_TargetFolder.Contains("!") || CS$<>8__locals1.$VB$Local_TargetFolder.Contains(";"))
			{
				ModMain.Hint("Minecraft 文件夹路径中不能含有感叹号或分号！", ModMain.HintType.Critical, true);
				throw new ModBase.CancelledException();
			}
			if (Directory.GetFileSystemEntries(CS$<>8__locals1.$VB$Local_TargetFolder).Length > 0)
			{
				ModMain.Hint("请选择一个空文件夹作为安装目标！", ModMain.HintType.Critical, true);
				throw new ModBase.CancelledException();
			}
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("解压压缩包", new ModLoader.LoaderBase[]
			{
				new ModLoader.LoaderTask<string, int>("解压压缩包", delegate(ModLoader.LoaderTask<string, int> Task)
				{
					ModModpack.ExtractModpackFiles(CS$<>8__locals1.$VB$Local_TargetFolder, CS$<>8__locals1.$VB$Local_FileAddress, Task, 0.95);
					PageSelectLeft.AddFolder(CS$<>8__locals1.$VB$Local_TargetFolder + CS$<>8__locals1.$VB$Local_ArchiveBaseFolder, ModBase.GetFolderNameFromPath(CS$<>8__locals1.$VB$Local_TargetFolder), false);
					Thread.Sleep(400);
					ModBase.RunInUi((ModModpack._Closure$__.$I12-1 == null) ? (ModModpack._Closure$__.$I12-1 = delegate()
					{
						ModMain._ProcessIterator.PageChange(FormMain.PageType.VersionSelect, FormMain.PageSubType.Default);
					}) : ModModpack._Closure$__.$I12-1, false);
				}, null, ThreadPriority.Normal)
			});
			loaderCombo.OnStateChanged = new Action<ModLoader.LoaderBase>(ModDownloadLib.McInstallState);
			loaderCombo.Start(CS$<>8__locals1.$VB$Local_TargetFolder, false);
			ModLoader.LoaderTaskbarAdd<string>(loaderCombo);
			ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
			ModMain._ProcessIterator.BtnExtraDownload.Ribble();
			return loaderCombo;
		}

		// Token: 0x040001B6 RID: 438
		private static bool m_Invocation = false;

		// Token: 0x040001B7 RID: 439
		private static bool proxy = false;
	}
}
