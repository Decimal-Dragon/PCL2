using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace PCL
{
	// Token: 0x0200014E RID: 334
	[StandardModule]
	public sealed class ModDownload
	{
		// Token: 0x06000DB5 RID: 3509 RVA: 0x0005C8AC File Offset: 0x0005AAAC
		public static ModNet.NetFile DlClientJarGet(ModMinecraft.McVersion Version, bool ReturnNothingOnFileUseable)
		{
			try
			{
				while (!string.IsNullOrEmpty(Version.CallThread()))
				{
					Version = new ModMinecraft.McVersion(Version.CallThread());
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "获取底层继承版本失败", ModBase.LogLevel.Debug, "出现错误");
			}
			if (Version.NewThread()["downloads"] != null && Version.NewThread()["downloads"]["client"] != null && Version.NewThread()["downloads"]["client"]["url"] != null)
			{
				ModBase.FileChecker fileChecker = new ModBase.FileChecker(1024L, (long)(Version.NewThread()["downloads"]["client"]["size"] ?? -1), (string)Version.NewThread()["downloads"]["client"]["sha1"], true, false);
				ModNet.NetFile result;
				if (ReturnNothingOnFileUseable && fileChecker.Check(Version.Path + Version.Name + ".jar") == null)
				{
					result = null;
				}
				else
				{
					string original = (string)Version.NewThread()["downloads"]["client"]["url"];
					result = new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(original), Version.Path + Version.Name + ".jar", fileChecker, false);
				}
				return result;
			}
			throw new Exception("底层版本 " + Version.Name + " 中无 jar 文件下载信息");
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0005CA64 File Offset: 0x0005AC64
		public static ModNet.NetFile DlClientAssetIndexGet(ModMinecraft.McVersion Version)
		{
			while (!string.IsNullOrEmpty(Version.CallThread()))
			{
				Version = new ModMinecraft.McVersion(Version.CallThread());
			}
			JToken jtoken = ModMinecraft.McAssetsGetIndex(Version, true, true);
			string localPath = ModMinecraft.m_ProxyTests + "assets\\indexes\\" + jtoken["id"].ToString() + ".json";
			ModBase.Log("[Download] 版本 " + Version.Name + " 对应的资源文件索引为 " + jtoken["id"].ToString(), ModBase.LogLevel.Normal, "出现错误");
			string text = (string)(jtoken["url"] ?? "");
			ModNet.NetFile result;
			if (Operators.CompareString(text, "", false) == 0)
			{
				result = null;
			}
			else
			{
				result = new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(text), localPath, new ModBase.FileChecker(-1L, -1L, null, false, true), false);
			}
			return result;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0005CB48 File Offset: 0x0005AD48
		public static List<ModLoader.LoaderBase> DlClientFix(ModMinecraft.McVersion Version, bool CheckAssetsHash, ModDownload.AssetsIndexExistsBehaviour AssetsIndexBehaviour)
		{
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			List<ModLoader.LoaderBase> loaders = new List<ModLoader.LoaderBase>
			{
				new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析缺失支持库文件", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
				{
					Task.Output = ModMinecraft.McLibFix(Version);
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = 1.0
				},
				new ModNet.LoaderDownload("下载支持库文件", new List<ModNet.NetFile>())
				{
					ProgressWeight = 15.0
				}
			};
			list.Add(new ModLoader.LoaderCombo<string>("下载支持库文件（主加载器）", loaders)
			{
				Block = false,
				Show = false,
				ProgressWeight = 16.0
			});
			if (Conversions.ToBoolean(ModMinecraft.ShouldIgnoreFileCheck(Version)))
			{
				ModBase.Log("[Download] 已跳过所有 Assets 检查", ModBase.LogLevel.Normal, "出现错误");
			}
			else
			{
				List<ModLoader.LoaderBase> list2 = new List<ModLoader.LoaderBase>();
				list2.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析资源文件索引地址", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
				{
					try
					{
						ModNet.NetFile netFile = ModDownload.DlClientAssetIndexGet(Version);
						new FileInfo(netFile.m_AuthenticationTest);
						if (AssetsIndexBehaviour != ModDownload.AssetsIndexExistsBehaviour.AlwaysDownload && netFile.m_PageTest.Check(netFile.m_AuthenticationTest) == null)
						{
							Task.Output = new List<ModNet.NetFile>();
						}
						else
						{
							Task.Output = new List<ModNet.NetFile>
							{
								netFile
							};
						}
					}
					catch (Exception innerException)
					{
						throw new Exception("分析资源文件索引地址失败", innerException);
					}
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = 0.5,
					Show = false
				});
				list2.Add(new ModNet.LoaderDownload("下载资源文件索引", new List<ModNet.NetFile>())
				{
					ProgressWeight = 2.0
				});
				if (AssetsIndexBehaviour == ModDownload.AssetsIndexExistsBehaviour.DownloadInBackground)
				{
					List<ModLoader.LoaderBase> list3 = new List<ModLoader.LoaderBase>();
					string TempAddress = null;
					string RealAddress = null;
					list3.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("后台分析资源文件索引地址", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
					{
						ModNet.NetFile netFile = ModDownload.DlClientAssetIndexGet(Version);
						RealAddress = netFile.m_AuthenticationTest;
						TempAddress = ModBase.m_DecoratorRepository + "Cache\\" + netFile._AlgoTest;
						netFile.m_AuthenticationTest = TempAddress;
						Task.Output = new List<ModNet.NetFile>
						{
							netFile
						};
						if (File.Exists(RealAddress) && Math.Abs((File.GetLastWriteTime(RealAddress).Date - DateAndTime.Now.Date).TotalDays) < 1.0)
						{
							ModBase.Log("[Download] 无需更新资源文件索引，取消", ModBase.LogLevel.Normal, "出现错误");
							Task.Abort();
						}
					}, null, ThreadPriority.Normal));
					list3.Add(new ModNet.LoaderDownload("后台下载资源文件索引", new List<ModNet.NetFile>()));
					list3.Add(new ModLoader.LoaderTask<List<ModNet.NetFile>, string>("后台复制资源文件索引", delegate(ModLoader.LoaderTask<List<ModNet.NetFile>, string> Task)
					{
						ModBase.CopyFile(TempAddress, RealAddress);
						ModLaunch.McLaunchLog("后台更新资源文件索引成功：" + TempAddress);
					}, null, ThreadPriority.Normal));
					ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("后台更新资源文件索引", list3);
					ModBase.Log("[Download] 开始后台检查资源文件索引", ModBase.LogLevel.Normal, "出现错误");
					loaderCombo.Start(null, false);
				}
				list2.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析缺失资源文件", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
				{
					ModLoader.LoaderTask<string, List<ModNet.NetFile>> loaderTask = Task;
					ModMinecraft.McVersion $VB$Local_Version = Version;
					bool $VB$Local_CheckAssetsHash = CheckAssetsHash;
					ModLoader.LoaderBase loaderBase = Task;
					List<ModNet.NetFile> output = ModMinecraft.McAssetsFixList($VB$Local_Version, $VB$Local_CheckAssetsHash, ref loaderBase);
					Task = (ModLoader.LoaderTask<string, List<ModNet.NetFile>>)loaderBase;
					loaderTask.Output = output;
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = 3.0
				});
				list2.Add(new ModNet.LoaderDownload("下载资源文件", new List<ModNet.NetFile>())
				{
					ProgressWeight = 25.0
				});
				list.Add(new ModLoader.LoaderCombo<string>("下载资源文件（主加载器）", list2)
				{
					Block = false,
					Show = false,
					ProgressWeight = 30.5
				});
			}
			return list;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0005CDAC File Offset: 0x0005AFAC
		private static void DlClientListMain(ModLoader.LoaderTask<string, ModDownload.DlClientListResult> Loader)
		{
			object left = ModBase.m_IdentifierRepository.Get("ToolDownloadVersion", null);
			if (Operators.ConditionalCompareObjectEqual(left, 0, false))
			{
				ModDownload.DlSourceLoader<string, ModDownload.DlClientListResult>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<string, ModDownload.DlClientListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<string, ModDownload.DlClientListResult>, int>(ModDownload._ManagerTests, 30),
					new KeyValuePair<ModLoader.LoaderTask<string, ModDownload.DlClientListResult>, int>(ModDownload.queueTests, 90)
				}, Loader.IsForceRestarting);
				return;
			}
			if (Operators.ConditionalCompareObjectEqual(left, 1, false))
			{
				ModDownload.DlSourceLoader<string, ModDownload.DlClientListResult>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<string, ModDownload.DlClientListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<string, ModDownload.DlClientListResult>, int>(ModDownload.queueTests, 5),
					new KeyValuePair<ModLoader.LoaderTask<string, ModDownload.DlClientListResult>, int>(ModDownload._ManagerTests, 35)
				}, Loader.IsForceRestarting);
				return;
			}
			ModDownload.DlSourceLoader<string, ModDownload.DlClientListResult>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<string, ModDownload.DlClientListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<string, ModDownload.DlClientListResult>, int>(ModDownload.queueTests, 60),
				new KeyValuePair<ModLoader.LoaderTask<string, ModDownload.DlClientListResult>, int>(ModDownload._ManagerTests, 120)
			}, Loader.IsForceRestarting);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0005CE88 File Offset: 0x0005B088
		private static void DlClientListMojangMain(ModLoader.LoaderTask<string, ModDownload.DlClientListResult> Loader)
		{
			JObject jobject = (JObject)ModNet.NetGetCodeByRequestRetry("https://launchermeta.mojang.com/mc/game/version_manifest.json", null, "", true, null, false);
			try
			{
				JArray jarray = (JArray)jobject["versions"];
				if (jarray.Count < 200)
				{
					throw new Exception("获取到的版本列表长度不足（" + jobject.ToString() + "）");
				}
				if (File.Exists(ModBase.m_DecoratorRepository + "Cache\\download.json"))
				{
					jarray.Merge(RuntimeHelpers.GetObjectValue(ModBase.GetJson(ModBase.ReadFile(ModBase.m_DecoratorRepository + "Cache\\download.json", null))));
				}
				Loader.Output = new ModDownload.DlClientListResult
				{
					policyTest = true,
					classTest = "Mojang 官方源",
					Value = jobject
				};
				string text = (string)jobject["latest"]["release"];
				if (Conversions.ToBoolean(Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("ToolUpdateRelease", null)) && Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(ModBase.m_IdentifierRepository.Get("ToolUpdateReleaseLast", null), "", false))) && text != null && Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(ModBase.m_IdentifierRepository.Get("ToolUpdateReleaseLast", null), text, false)))))
				{
					ModMinecraft.McDownloadClientUpdateHint(text, jobject);
					ModDownload.m_EventTests = true;
				}
				ModDownloadLib.m_StructField = Conversions.ToInteger(text.Split(".")[1]);
				ModBase.m_IdentifierRepository.Set("ToolUpdateReleaseLast", text, false, null);
				text = (string)jobject["latest"]["snapshot"];
				if (Conversions.ToBoolean(Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("ToolUpdateSnapshot", null)) && Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(ModBase.m_IdentifierRepository.Get("ToolUpdateSnapshotLast", null), "", false))) && text != null && Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(ModBase.m_IdentifierRepository.Get("ToolUpdateSnapshotLast", null), text, false))) && !ModDownload.m_EventTests))
				{
					ModMinecraft.McDownloadClientUpdateHint(text, jobject);
				}
				ModBase.m_IdentifierRepository.Set("ToolUpdateSnapshotLast", text ?? "Nothing", false, null);
			}
			catch (Exception innerException)
			{
				throw new Exception("Minecraft 官方源版本列表解析失败", innerException);
			}
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0005D0FC File Offset: 0x0005B2FC
		private static void DlClientListBmclapiMain(ModLoader.LoaderTask<string, ModDownload.DlClientListResult> Loader)
		{
			JObject jobject = (JObject)ModNet.NetGetCodeByRequestRetry("https://bmclapi2.bangbang93.com/mc/game/version_manifest.json", null, "", true, null, false);
			try
			{
				JArray jarray = (JArray)jobject["versions"];
				if (jarray.Count < 200)
				{
					throw new Exception("获取到的版本列表长度不足（" + jobject.ToString() + "）");
				}
				if (File.Exists(ModBase.m_DecoratorRepository + "Cache\\download.json"))
				{
					jarray.Merge(RuntimeHelpers.GetObjectValue(ModBase.GetJson(ModBase.ReadFile(ModBase.m_DecoratorRepository + "Cache\\download.json", null))));
				}
				if (!string.IsNullOrEmpty(Loader.Input))
				{
					string Id = Loader.Input;
					if (!Enumerable.Any<JToken>(ModDownload.accountTests.Output.Value["versions"], (JToken v) => (string)v["id"] == Id))
					{
						throw new Exception("BMCLAPI 源未包含目标版本 " + Id);
					}
				}
				Loader.Output = new ModDownload.DlClientListResult
				{
					policyTest = false,
					classTest = "BMCLAPI",
					Value = jobject
				};
			}
			catch (Exception innerException)
			{
				throw new Exception("Minecraft BMCLAPI 版本列表解析失败（" + jobject.ToString() + "）", innerException);
			}
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0005D268 File Offset: 0x0005B468
		public static object DlClientListGet(string Id)
		{
			object result;
			try
			{
				Id = Id.Replace("_", "-");
				if (Operators.CompareString(Id, "1.0", false) != 0 && Id.EndsWithF(".0", false))
				{
					Id = Strings.Left(Id, checked(Id.Length - 2));
				}
				switch (ModDownload.accountTests.State)
				{
				case ModBase.LoadState.Waiting:
				case ModBase.LoadState.Failed:
				case ModBase.LoadState.Aborted:
					ModDownload.accountTests.WaitForExit(Id, null, true);
					break;
				case ModBase.LoadState.Loading:
					ModDownload.accountTests.WaitForExit(Id, null, false);
					break;
				case ModBase.LoadState.Finished:
					try
					{
						foreach (JToken jtoken in ModDownload.accountTests.Output.Value["versions"])
						{
							JObject jobject = (JObject)jtoken;
							if ((string)jobject["id"] == Id)
							{
								return jobject["url"].ToString();
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
					ModDownload.accountTests.WaitForExit(Id, null, true);
					break;
				}
				try
				{
					foreach (JToken jtoken2 in ModDownload.accountTests.Output.Value["versions"])
					{
						JObject jobject2 = (JObject)jtoken2;
						if ((string)jobject2["id"] == Id)
						{
							return jobject2["url"].ToString();
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
				ModBase.Log(string.Format("未发现版本 {0} 的 json 下载地址，版本列表返回为：{1}{2}", Id, "\r\n", ModDownload.accountTests.Output.Value.ToString()), ModBase.LogLevel.Debug, "出现错误");
				result = null;
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, string.Format("获取版本 {0} 的 json 下载地址失败", Id), ModBase.LogLevel.Debug, "出现错误");
				result = null;
			}
			return result;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0005D49C File Offset: 0x0005B69C
		private static void DlOptiFineListMain(ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult> Loader)
		{
			object left = ModBase.m_IdentifierRepository.Get("ToolDownloadVersion", null);
			if (Operators.ConditionalCompareObjectEqual(left, 0, false))
			{
				ModDownload.DlSourceLoader<int, ModDownload.DlOptiFineListResult>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult>, int>(ModDownload._BaseTests, 30),
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult>, int>(ModDownload._WrapperTests, 90)
				}, Loader.IsForceRestarting);
				return;
			}
			if (Operators.ConditionalCompareObjectEqual(left, 1, false))
			{
				ModDownload.DlSourceLoader<int, ModDownload.DlOptiFineListResult>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult>, int>(ModDownload._WrapperTests, 5),
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult>, int>(ModDownload._BaseTests, 35)
				}, Loader.IsForceRestarting);
				return;
			}
			ModDownload.DlSourceLoader<int, ModDownload.DlOptiFineListResult>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult>, int>(ModDownload._WrapperTests, 60),
				new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult>, int>(ModDownload._BaseTests, 120)
			}, Loader.IsForceRestarting);
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0005D578 File Offset: 0x0005B778
		private static void DlOptiFineListOfficialMain(ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult> Loader)
		{
			string text = ModNet.NetGetCodeByClient("https://optifine.net/downloads", Encoding.Default, "application/json, text/javascript, */*; q=0.01", false);
			if (text.Length < 200)
			{
				throw new Exception("获取到的版本列表长度不足（" + text + "）");
			}
			checked
			{
				try
				{
					List<string> list = ModBase.RegexSearch(text, "(?<=colForge'>)[^<]*", 0);
					List<string> list2 = ModBase.RegexSearch(text, "(?<=colDate'>)[^<]+", 0);
					List<string> list3 = ModBase.RegexSearch(text, "(?<=OptiFine_)[0-9A-Za-z_.]+(?=.jar\")", 0);
					if (list2.Count != list3.Count)
					{
						throw new Exception("版本与发布时间数据无法对应");
					}
					if (list.Count != list3.Count)
					{
						throw new Exception("版本与 Forge 兼容数据无法对应");
					}
					if (list2.Count < 10)
					{
						throw new Exception("获取到的版本数量不足（" + text + "）");
					}
					List<ModDownload.DlOptiFineListEntry> list4 = new List<ModDownload.DlOptiFineListEntry>();
					int num = list2.Count - 1;
					for (int i = 0; i <= num; i++)
					{
						list3[i] = list3[i].Replace("_", " ");
						ModDownload.DlOptiFineListEntry dlOptiFineListEntry = new ModDownload.DlOptiFineListEntry();
						dlOptiFineListEntry.m_SchemaTest = list3[i].Replace("HD U ", "").Replace(".0 ", " ");
						dlOptiFineListEntry.m_ProcTest = new string[]
						{
							list2[i].Split(".")[2],
							list2[i].Split(".")[1],
							list2[i].Split(".")[0]
						}.Join("/");
						dlOptiFineListEntry._DefinitionTest = list3[i].ContainsF("pre", true);
						dlOptiFineListEntry.CompareMapper(list3[i].ToString().Split(" ")[0]);
						dlOptiFineListEntry._DescriptorTest = (list3[i].ContainsF("pre", true) ? "preview_" : "") + "OptiFine_" + list3[i].Replace(" ", "_") + ".jar";
						dlOptiFineListEntry.parserMap = list[i].Replace("Forge ", "").Replace("#", "");
						ModDownload.DlOptiFineListEntry dlOptiFineListEntry2 = dlOptiFineListEntry;
						if (dlOptiFineListEntry2.parserMap.Contains("N/A"))
						{
							dlOptiFineListEntry2.parserMap = null;
						}
						dlOptiFineListEntry2.m_PublisherTest = dlOptiFineListEntry2.CheckMapper() + "-OptiFine_" + list3[i].ToString().Replace(" ", "_").Replace(dlOptiFineListEntry2.CheckMapper() + "_", "");
						list4.Add(dlOptiFineListEntry2);
					}
					Loader.Output = new ModDownload.DlOptiFineListResult
					{
						producerTest = true,
						orderTest = "OptiFine 官方源",
						Value = list4
					};
				}
				catch (Exception innerException)
				{
					throw new Exception("OptiFine 官方源版本列表解析失败（" + text + "）", innerException);
				}
			}
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0005D8A8 File Offset: 0x0005BAA8
		private static void DlOptiFineListBmclapiMain(ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult> Loader)
		{
			JArray jarray = (JArray)ModNet.NetGetCodeByRequestRetry("https://bmclapi2.bangbang93.com/optifine/versionList", null, "", true, null, false);
			try
			{
				List<ModDownload.DlOptiFineListEntry> list = new List<ModDownload.DlOptiFineListEntry>();
				try
				{
					foreach (JToken jtoken in jarray)
					{
						JObject jobject = (JObject)jtoken;
						ModDownload.DlOptiFineListEntry dlOptiFineListEntry = new ModDownload.DlOptiFineListEntry();
						dlOptiFineListEntry.m_SchemaTest = (jobject["mcversion"].ToString() + jobject["type"].ToString().Replace("HD_U", "").Replace("_", " ") + " " + jobject["patch"].ToString()).Replace(".0 ", " ");
						dlOptiFineListEntry.m_ProcTest = "";
						dlOptiFineListEntry._DefinitionTest = jobject["patch"].ToString().ContainsF("pre", true);
						dlOptiFineListEntry.CompareMapper(jobject["mcversion"].ToString());
						dlOptiFineListEntry._DescriptorTest = jobject["filename"].ToString();
						dlOptiFineListEntry.parserMap = (jobject["forge"] ?? "").ToString().Replace("Forge ", "").Replace("#", "");
						ModDownload.DlOptiFineListEntry dlOptiFineListEntry2 = dlOptiFineListEntry;
						if (dlOptiFineListEntry2.parserMap.Contains("N/A"))
						{
							dlOptiFineListEntry2.parserMap = null;
						}
						dlOptiFineListEntry2.m_PublisherTest = dlOptiFineListEntry2.CheckMapper() + "-OptiFine_" + (jobject["type"].ToString() + " " + jobject["patch"].ToString()).Replace(".0 ", " ").Replace(" ", "_").Replace(dlOptiFineListEntry2.CheckMapper() + "_", "");
						list.Add(dlOptiFineListEntry2);
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
				Loader.Output = new ModDownload.DlOptiFineListResult
				{
					producerTest = false,
					orderTest = "BMCLAPI",
					Value = list
				};
			}
			catch (Exception innerException)
			{
				throw new Exception("OptiFine BMCLAPI 版本列表解析失败（" + jarray.ToString() + "）", innerException);
			}
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0005DB38 File Offset: 0x0005BD38
		private static void DlForgeListMain(ModLoader.LoaderTask<int, ModDownload.DlForgeListResult> Loader)
		{
			object left = ModBase.m_IdentifierRepository.Get("ToolDownloadVersion", null);
			if (Operators.ConditionalCompareObjectEqual(left, 0, false))
			{
				ModDownload.DlSourceLoader<int, ModDownload.DlForgeListResult>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlForgeListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlForgeListResult>, int>(ModDownload.m_PrototypeTests, 30),
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlForgeListResult>, int>(ModDownload.m_CodeTests, 90)
				}, Loader.IsForceRestarting);
				return;
			}
			if (Operators.ConditionalCompareObjectEqual(left, 1, false))
			{
				ModDownload.DlSourceLoader<int, ModDownload.DlForgeListResult>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlForgeListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlForgeListResult>, int>(ModDownload.m_CodeTests, 5),
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlForgeListResult>, int>(ModDownload.m_PrototypeTests, 35)
				}, Loader.IsForceRestarting);
				return;
			}
			ModDownload.DlSourceLoader<int, ModDownload.DlForgeListResult>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlForgeListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlForgeListResult>, int>(ModDownload.m_CodeTests, 60),
				new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlForgeListResult>, int>(ModDownload.m_PrototypeTests, 120)
			}, Loader.IsForceRestarting);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0005DC14 File Offset: 0x0005BE14
		private static void DlForgeListOfficialMain(ModLoader.LoaderTask<int, ModDownload.DlForgeListResult> Loader)
		{
			string text = Conversions.ToString(ModNet.NetGetCodeByRequestRetry("https://files.minecraftforge.net/maven/net/minecraftforge/forge/index_1.2.4.html", Encoding.Default, "text/html", false, null, true));
			if (text.Length < 200)
			{
				throw new Exception("获取到的版本列表长度不足（" + text + "）");
			}
			List<string> list = ModBase.RegexSearch(text, "(?<=a href=\"index_)[0-9.]+(_pre[0-9]?)?(?=.html)", 0);
			list.Add("1.2.4");
			if (list.Count < 10)
			{
				throw new Exception("获取到的版本数量不足（" + text + "）");
			}
			Loader.Output = new ModDownload.DlForgeListResult
			{
				m_FieldMap = true,
				m_BroadcasterMap = "Forge 官方源",
				Value = list
			};
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0005DCC4 File Offset: 0x0005BEC4
		private static void DlForgeListBmclapiMain(ModLoader.LoaderTask<int, ModDownload.DlForgeListResult> Loader)
		{
			string text = Conversions.ToString(ModNet.NetGetCodeByRequestRetry("https://bmclapi2.bangbang93.com/forge/minecraft", Encoding.Default, "", false, null, false));
			if (text.Length < 200)
			{
				throw new Exception("获取到的版本列表长度不足（" + text + "）");
			}
			List<string> list = ModBase.RegexSearch(text, "[0-9.]+(_pre[0-9]?)?", 0);
			if (list.Count < 10)
			{
				throw new Exception("获取到的版本数量不足（" + text + "）");
			}
			Loader.Output = new ModDownload.DlForgeListResult
			{
				m_FieldMap = false,
				m_BroadcasterMap = "BMCLAPI",
				Value = list
			};
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0005DD6C File Offset: 0x0005BF6C
		public static void DlForgeVersionMain(ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> Loader)
		{
			ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> key = new ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>("DlForgeVersion Official", new Action<ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>>(ModDownload.DlForgeVersionOfficialMain), null, ThreadPriority.Normal);
			ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> key2 = new ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>("DlForgeVersion Bmclapi", new Action<ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>>(ModDownload.DlForgeVersionBmclapiMain), null, ThreadPriority.Normal);
			object left = ModBase.m_IdentifierRepository.Get("ToolDownloadVersion", null);
			if (Operators.ConditionalCompareObjectEqual(left, 0, false))
			{
				ModDownload.DlSourceLoader<string, List<ModDownload.DlForgeVersionEntry>>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>, int>(key2, 30),
					new KeyValuePair<ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>, int>(key, 90)
				}, Loader.IsForceRestarting);
				return;
			}
			if (Operators.ConditionalCompareObjectEqual(left, 1, false))
			{
				ModDownload.DlSourceLoader<string, List<ModDownload.DlForgeVersionEntry>>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>, int>(key, 5),
					new KeyValuePair<ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>, int>(key2, 35)
				}, Loader.IsForceRestarting);
				return;
			}
			ModDownload.DlSourceLoader<string, List<ModDownload.DlForgeVersionEntry>>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>, int>(key, 60),
				new KeyValuePair<ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>, int>(key2, 120)
			}, Loader.IsForceRestarting);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0005DE64 File Offset: 0x0005C064
		public static void DlForgeVersionOfficialMain(ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> Loader)
		{
			string text;
			try
			{
				text = ModNet.NetGetCodeByDownload("https://files.minecraftforge.net/maven/net/minecraftforge/forge/index_" + Loader.Input.Replace("-", "_") + ".html", 45000, false, true);
			}
			catch (Exception ex)
			{
				if (ModBase.GetExceptionSummary(ex).Contains("(404)"))
				{
					throw new Exception("没有可用版本");
				}
				throw;
			}
			if (text.Length < 1000)
			{
				throw new Exception("获取到的版本列表长度不足（" + text + "）");
			}
			List<ModDownload.DlForgeVersionEntry> list = new List<ModDownload.DlForgeVersionEntry>();
			checked
			{
				try
				{
					string[] array = Strings.Mid(text, 1, text.LastIndexOfF("</table>", false)).Split("<td class=\"download-version");
					int num = Enumerable.Count<string>(array) - 1;
					for (int i = 1; i <= num; i++)
					{
						string text2 = array[i];
						try
						{
							string text3 = ModBase.RegexSeek(text2, "(?<=[^(0-9)]+)[0-9\\.]+", 0);
							bool propertyMap = text2.Contains("fa promo-recommended");
							string input = Loader.Input;
							string text4 = ModBase.RegexSeek(text2, string.Format("(?<=-{0}-)[^-\"]+(?=-[a-z]+.[a-z]{{3}})", text3), 0);
							if (string.IsNullOrWhiteSpace(text4))
							{
								text4 = null;
							}
							string[] array2 = ModBase.RegexSeek(text2, "(?<=\"download-time\" title=\")[^\"]+", 0).Split(" -:".ToCharArray());
							string mapperMap = new DateTime(Conversions.ToInteger(array2[0]), Conversions.ToInteger(array2[1]), Conversions.ToInteger(array2[2]), Conversions.ToInteger(array2[3]), Conversions.ToInteger(array2[4]), Conversions.ToInteger(array2[5]), 0, DateTimeKind.Utc).ToLocalTime().ToString("yyyy'/'MM'/'dd HH':'mm");
							string text5;
							string composerMap;
							if (text2.Contains("classifier-installer\""))
							{
								text2 = text2.Substring(text2.IndexOfF("installer.jar", false));
								text5 = ModBase.RegexSeek(text2, "(?<=MD5:</strong> )[^<]+", 0);
								composerMap = "installer";
							}
							else if (text2.Contains("classifier-universal\""))
							{
								text2 = text2.Substring(text2.IndexOfF("universal.zip", false));
								text5 = ModBase.RegexSeek(text2, "(?<=MD5:</strong> )[^<]+", 0);
								composerMap = "universal";
							}
							else
							{
								if (!text2.Contains("client.zip"))
								{
									goto IL_29C;
								}
								text2 = text2.Substring(text2.IndexOfF("client.zip", false));
								text5 = ModBase.RegexSeek(text2, "(?<=MD5:</strong> )[^<]+", 0);
								composerMap = "client";
							}
							list.Add(new ModDownload.DlForgeVersionEntry(text3, text4, input)
							{
								_ComposerMap = composerMap,
								propertyMap = propertyMap,
								threadMap = text5.Trim(new char[]
								{
									'\r',
									'\n'
								}),
								mapperMap = mapperMap
							});
						}
						catch (Exception innerException)
						{
							throw new Exception("Forge 官方源版本信息提取失败（" + text2 + "）", innerException);
						}
						IL_29C:;
					}
				}
				catch (Exception innerException2)
				{
					throw new Exception("Forge 官方源版本列表解析失败（" + text + "）", innerException2);
				}
				if (!Enumerable.Any<ModDownload.DlForgeVersionEntry>(list))
				{
					throw new Exception("没有可用版本");
				}
				Loader.Output = list;
			}
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0005E1A0 File Offset: 0x0005C3A0
		public static void DlForgeVersionBmclapiMain(ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> Loader)
		{
			JArray jarray = (JArray)ModNet.NetGetCodeByRequestRetry("https://bmclapi2.bangbang93.com/forge/minecraft/" + Loader.Input.Replace("-", "_"), null, "", true, null, false);
			List<ModDownload.DlForgeVersionEntry> list = new List<ModDownload.DlForgeVersionEntry>();
			try
			{
				string left = ModDownloadLib.McDownloadForgeRecommendedGet(Loader.Input);
				try
				{
					foreach (JToken jtoken in jarray)
					{
						JObject jobject = (JObject)jtoken;
						string threadMap = null;
						string composerMap = "unknown";
						int num = -1;
						try
						{
							foreach (JToken jtoken2 in jobject["files"])
							{
								JObject jobject2 = (JObject)jtoken2;
								string left2 = jobject2["category"].ToString();
								if (Operators.CompareString(left2, "installer", false) != 0)
								{
									if (Operators.CompareString(left2, "universal", false) != 0)
									{
										if (Operators.CompareString(left2, "client", false) == 0 && num <= 0 && Operators.CompareString(jobject2["format"].ToString(), "zip", false) == 0)
										{
											threadMap = (string)jobject2["hash"];
											composerMap = "client";
											num = 0;
										}
									}
									else if (num <= 1 && Operators.CompareString(jobject2["format"].ToString(), "zip", false) == 0)
									{
										threadMap = (string)jobject2["hash"];
										composerMap = "universal";
										num = 1;
									}
								}
								else if (Operators.CompareString(jobject2["format"].ToString(), "jar", false) == 0)
								{
									threadMap = (string)jobject2["hash"];
									composerMap = "installer";
									num = 2;
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
						string branch = (string)jobject["branch"];
						string text = (string)jobject["version"];
						ModDownload.DlForgeVersionEntry dlForgeVersionEntry = new ModDownload.DlForgeVersionEntry(text, branch, Loader.Input)
						{
							threadMap = threadMap,
							_ComposerMap = composerMap,
							propertyMap = (Operators.CompareString(left, text, false) == 0)
						};
						jobject["modified"].ToString().Split(new char[]
						{
							'-',
							'T',
							':',
							'.',
							' ',
							'/'
						});
						dlForgeVersionEntry.mapperMap = jobject["modified"].ToObject<DateTime>().ToLocalTime().ToString("yyyy'/'MM'/'dd HH':'mm");
						list.Add(dlForgeVersionEntry);
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
			catch (Exception innerException)
			{
				throw new Exception("Forge BMCLAPI 版本列表解析失败（" + jarray.ToString() + "）", innerException);
			}
			if (!Enumerable.Any<ModDownload.DlForgeVersionEntry>(list))
			{
				throw new Exception("没有可用版本");
			}
			Loader.Output = list;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0005E4C0 File Offset: 0x0005C6C0
		private static void DlNeoForgeListMain(ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult> Loader)
		{
			object left = ModBase.m_IdentifierRepository.Get("ToolDownloadVersion", null);
			if (Operators.ConditionalCompareObjectEqual(left, 0, false))
			{
				ModDownload.DlSourceLoader<int, ModDownload.DlNeoForgeListResult>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult>, int>(ModDownload.m_AdapterTests, 30),
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult>, int>(ModDownload.infoTests, 90)
				}, Loader.IsForceRestarting);
				return;
			}
			if (Operators.ConditionalCompareObjectEqual(left, 1, false))
			{
				ModDownload.DlSourceLoader<int, ModDownload.DlNeoForgeListResult>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult>, int>(ModDownload.infoTests, 5),
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult>, int>(ModDownload.m_AdapterTests, 35)
				}, Loader.IsForceRestarting);
				return;
			}
			ModDownload.DlSourceLoader<int, ModDownload.DlNeoForgeListResult>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult>, int>(ModDownload.infoTests, 60),
				new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult>, int>(ModDownload.m_AdapterTests, 120)
			}, Loader.IsForceRestarting);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0005E59C File Offset: 0x0005C79C
		private static void DlNeoForgeListOfficialMain(ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult> Loader)
		{
			string text = ModNet.NetGetCodeByDownload("https://maven.neoforged.net/api/maven/versions/releases/net/neoforged/neoforge", 45000, true, true);
			string text2 = ModNet.NetGetCodeByDownload("https://maven.neoforged.net/api/maven/versions/releases/net/neoforged/forge", 45000, true, true);
			if (text.Length >= 100 && text2.Length >= 100)
			{
				try
				{
					Loader.Output = new ModDownload.DlNeoForgeListResult
					{
						testMap = true,
						_RepositoryMap = "NeoForge 官方源",
						Value = ModDownload.GetNeoForgeEntries(text, text2)
					};
					return;
				}
				catch (Exception innerException)
				{
					throw new Exception(string.Concat(new string[]
					{
						"NeoForge 官方源版本列表解析失败（",
						text,
						"\r\n\r\n",
						text2,
						"）"
					}), innerException);
				}
			}
			throw new Exception("获取到的版本列表长度不足（" + text + "）");
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0005E674 File Offset: 0x0005C874
		public static void DlNeoForgeListBmclapiMain(ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult> Loader)
		{
			string text = ModNet.NetGetCodeByDownload("https://bmclapi2.bangbang93.com/neoforge/meta/api/maven/details/releases/net/neoforged/neoforge", 45000, true, true);
			string text2 = ModNet.NetGetCodeByDownload("https://bmclapi2.bangbang93.com/neoforge/meta/api/maven/details/releases/net/neoforged/forge", 45000, true, true);
			if (text.Length >= 100 && text2.Length >= 100)
			{
				try
				{
					Loader.Output = new ModDownload.DlNeoForgeListResult
					{
						testMap = true,
						_RepositoryMap = "BMCLAPI",
						Value = ModDownload.GetNeoForgeEntries(text, text2)
					};
					return;
				}
				catch (Exception innerException)
				{
					throw new Exception(string.Concat(new string[]
					{
						"NeoForge BMCLAPI 版本列表解析失败（",
						text,
						"\r\n\r\n",
						text2,
						"）"
					}), innerException);
				}
			}
			throw new Exception("获取到的版本列表长度不足（" + text + "）");
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0005E74C File Offset: 0x0005C94C
		private static List<ModDownload.DlNeoForgeListEntry> GetNeoForgeEntries(string LatestJson, string LatestLegacyJson)
		{
			List<ModDownload.DlNeoForgeListEntry> list = Enumerable.ToList<ModDownload.DlNeoForgeListEntry>(Enumerable.Select<string, ModDownload.DlNeoForgeListEntry>(Enumerable.Where<string>(ModBase.RegexSearch(LatestLegacyJson + LatestJson, "(?<=\")(1\\.20\\.1-)?\\d+\\.\\d+\\.\\d+(-beta)?(?=\")", 0), (ModDownload._Closure$__.$I42-0 == null) ? (ModDownload._Closure$__.$I42-0 = ((string name) => Operators.CompareString(name, "47.1.82", false) != 0)) : ModDownload._Closure$__.$I42-0), (ModDownload._Closure$__.$I42-1 == null) ? (ModDownload._Closure$__.$I42-1 = ((string name) => new ModDownload.DlNeoForgeListEntry(name))) : ModDownload._Closure$__.$I42-1));
			if (!Enumerable.Any<ModDownload.DlNeoForgeListEntry>(list))
			{
				throw new Exception("没有可用版本");
			}
			return Enumerable.ToList<ModDownload.DlNeoForgeListEntry>(Enumerable.OrderByDescending<ModDownload.DlNeoForgeListEntry, Version>(list, (ModDownload._Closure$__.$I42-2 == null) ? (ModDownload._Closure$__.$I42-2 = ((ModDownload.DlNeoForgeListEntry a) => a.clientMap)) : ModDownload._Closure$__.$I42-2));
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0005E804 File Offset: 0x0005CA04
		private static void DlLiteLoaderListMain(ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult> Loader)
		{
			object left = ModBase.m_IdentifierRepository.Get("ToolDownloadVersion", null);
			if (Operators.ConditionalCompareObjectEqual(left, 0, false))
			{
				ModDownload.DlSourceLoader<int, ModDownload.DlLiteLoaderListResult>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult>, int>(ModDownload.m_MerchantTests, 30),
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult>, int>(ModDownload.listTests, 90)
				}, Loader.IsForceRestarting);
				return;
			}
			if (Operators.ConditionalCompareObjectEqual(left, 1, false))
			{
				ModDownload.DlSourceLoader<int, ModDownload.DlLiteLoaderListResult>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult>, int>(ModDownload.listTests, 5),
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult>, int>(ModDownload.m_MerchantTests, 35)
				}, Loader.IsForceRestarting);
				return;
			}
			ModDownload.DlSourceLoader<int, ModDownload.DlLiteLoaderListResult>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult>, int>(ModDownload.listTests, 60),
				new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult>, int>(ModDownload.m_MerchantTests, 120)
			}, Loader.IsForceRestarting);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0005E8E0 File Offset: 0x0005CAE0
		private static void DlLiteLoaderListOfficialMain(ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult> Loader)
		{
			JObject jobject = (JObject)ModNet.NetGetCodeByRequestRetry("https://dl.liteloader.com/versions/versions.json", null, "", true, null, false);
			try
			{
				JObject jobject2 = (JObject)jobject["versions"];
				List<ModDownload.DlLiteLoaderListEntry> list = new List<ModDownload.DlLiteLoaderListEntry>();
				try
				{
					foreach (KeyValuePair<string, JToken> keyValuePair in jobject2)
					{
						if (!keyValuePair.Key.StartsWithF("1.6", false) && !keyValuePair.Key.StartsWithF("1.5", false))
						{
							JToken jtoken = (keyValuePair.Value["artefacts"] ?? keyValuePair.Value["snapshots"])["com.mumfrey:liteloader"]["latest"];
							list.Add(new ModDownload.DlLiteLoaderListEntry
							{
								Inherit = keyValuePair.Key,
								IsLegacy = (Conversions.ToDouble(keyValuePair.Key.Split(".")[1]) < 8.0),
								IsPreview = (Operators.CompareString(jtoken["stream"].ToString().ToLower(), "snapshot", false) == 0),
								FileName = "liteloader-installer-" + keyValuePair.Key + ((Operators.CompareString(keyValuePair.Key, "1.8", false) == 0 || Operators.CompareString(keyValuePair.Key, "1.9", false) == 0) ? ".0" : "") + "-00-SNAPSHOT.jar",
								MD5 = (string)jtoken["md5"],
								ReleaseTime = ModBase.GetLocalTime(ModBase.GetDate((int)jtoken["timestamp"])).ToString("yyyy'/'MM'/'dd HH':'mm"),
								JsonToken = jtoken
							});
						}
					}
				}
				finally
				{
					IEnumerator<KeyValuePair<string, JToken>> enumerator;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
				Loader.Output = new ModDownload.DlLiteLoaderListResult
				{
					IsOfficial = true,
					SourceName = "LiteLoader 官方源",
					Value = list
				};
			}
			catch (Exception innerException)
			{
				throw new Exception("LiteLoader 官方源版本列表解析失败（" + jobject.ToString() + "）", innerException);
			}
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0005EB44 File Offset: 0x0005CD44
		private static void DlLiteLoaderListBmclapiMain(ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult> Loader)
		{
			JObject jobject = (JObject)ModNet.NetGetCodeByRequestRetry("https://bmclapi2.bangbang93.com/maven/com/mumfrey/liteloader/versions.json", null, "", true, null, false);
			try
			{
				JObject jobject2 = (JObject)jobject["versions"];
				List<ModDownload.DlLiteLoaderListEntry> list = new List<ModDownload.DlLiteLoaderListEntry>();
				try
				{
					foreach (KeyValuePair<string, JToken> keyValuePair in jobject2)
					{
						if (!keyValuePair.Key.StartsWithF("1.6", false) && !keyValuePair.Key.StartsWithF("1.5", false))
						{
							JToken jtoken = (keyValuePair.Value["artefacts"] ?? keyValuePair.Value["snapshots"])["com.mumfrey:liteloader"]["latest"];
							list.Add(new ModDownload.DlLiteLoaderListEntry
							{
								Inherit = keyValuePair.Key,
								IsLegacy = (Conversions.ToDouble(keyValuePair.Key.Split(".")[1]) < 8.0),
								IsPreview = (Operators.CompareString(jtoken["stream"].ToString().ToLower(), "snapshot", false) == 0),
								FileName = "liteloader-installer-" + keyValuePair.Key + ((Operators.CompareString(keyValuePair.Key, "1.8", false) == 0 || Operators.CompareString(keyValuePair.Key, "1.9", false) == 0) ? ".0" : "") + "-00-SNAPSHOT.jar",
								MD5 = (string)jtoken["md5"],
								ReleaseTime = ModBase.GetLocalTime(ModBase.GetDate((int)jtoken["timestamp"])).ToString("yyyy'/'MM'/'dd HH':'mm"),
								JsonToken = jtoken
							});
						}
					}
				}
				finally
				{
					IEnumerator<KeyValuePair<string, JToken>> enumerator;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
				Loader.Output = new ModDownload.DlLiteLoaderListResult
				{
					IsOfficial = false,
					SourceName = "BMCLAPI",
					Value = list
				};
			}
			catch (Exception innerException)
			{
				throw new Exception("LiteLoader BMCLAPI 版本列表解析失败（" + jobject.ToString() + "）", innerException);
			}
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0005EDA8 File Offset: 0x0005CFA8
		private static void DlFabricListMain(ModLoader.LoaderTask<int, ModDownload.DlFabricListResult> Loader)
		{
			object left = ModBase.m_IdentifierRepository.Get("ToolDownloadVersion", null);
			if (Operators.ConditionalCompareObjectEqual(left, 0, false))
			{
				ModDownload.DlSourceLoader<int, ModDownload.DlFabricListResult>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlFabricListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlFabricListResult>, int>(ModDownload._ComparatorTests, 30),
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlFabricListResult>, int>(ModDownload.algoTests, 90)
				}, Loader.IsForceRestarting);
				return;
			}
			if (Operators.ConditionalCompareObjectEqual(left, 1, false))
			{
				ModDownload.DlSourceLoader<int, ModDownload.DlFabricListResult>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlFabricListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlFabricListResult>, int>(ModDownload.algoTests, 5),
					new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlFabricListResult>, int>(ModDownload._ComparatorTests, 35)
				}, Loader.IsForceRestarting);
				return;
			}
			ModDownload.DlSourceLoader<int, ModDownload.DlFabricListResult>(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlFabricListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlFabricListResult>, int>(ModDownload.algoTests, 60),
				new KeyValuePair<ModLoader.LoaderTask<int, ModDownload.DlFabricListResult>, int>(ModDownload._ComparatorTests, 120)
			}, Loader.IsForceRestarting);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0005EE84 File Offset: 0x0005D084
		private static void DlFabricListOfficialMain(ModLoader.LoaderTask<int, ModDownload.DlFabricListResult> Loader)
		{
			JObject jobject = (JObject)ModNet.NetGetCodeByRequestRetry("https://meta.fabricmc.net/v2/versions", null, "", true, null, false);
			try
			{
				ModDownload.DlFabricListResult dlFabricListResult = new ModDownload.DlFabricListResult
				{
					specificationMap = true,
					m_ContextMap = "Fabric 官方源",
					Value = jobject
				};
				if (dlFabricListResult.Value["game"] == null || dlFabricListResult.Value["loader"] == null || dlFabricListResult.Value["installer"] == null)
				{
					throw new Exception("获取到的列表缺乏必要项");
				}
				Loader.Output = dlFabricListResult;
			}
			catch (Exception innerException)
			{
				throw new Exception("Fabric 官方源版本列表解析失败（" + jobject.ToString() + "）", innerException);
			}
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0005EF50 File Offset: 0x0005D150
		private static void DlFabricListBmclapiMain(ModLoader.LoaderTask<int, ModDownload.DlFabricListResult> Loader)
		{
			JObject jobject = (JObject)ModNet.NetGetCodeByRequestRetry("https://bmclapi2.bangbang93.com/fabric-meta/v2/versions", null, "", true, null, false);
			try
			{
				ModDownload.DlFabricListResult dlFabricListResult = new ModDownload.DlFabricListResult
				{
					specificationMap = false,
					m_ContextMap = "BMCLAPI",
					Value = jobject
				};
				if (dlFabricListResult.Value["game"] == null || dlFabricListResult.Value["loader"] == null || dlFabricListResult.Value["installer"] == null)
				{
					throw new Exception("获取到的列表缺乏必要项");
				}
				Loader.Output = dlFabricListResult;
			}
			catch (Exception innerException)
			{
				throw new Exception("Fabric BMCLAPI 版本列表解析失败（" + jobject.ToString() + "）", innerException);
			}
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0005F01C File Offset: 0x0005D21C
		public static object DlModRequest(string Url, bool IsJson = false)
		{
			string text = ModDownload.DlSourceModGet(Url);
			List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
			if (Operators.CompareString(text, Url, false) != 0)
			{
				object left = ModBase.m_IdentifierRepository.Get("ToolDownloadMod", null);
				if (Operators.ConditionalCompareObjectEqual(left, 0, false))
				{
					if (ModBase._TokenRepository)
					{
						list.Add(new KeyValuePair<string, int>(text, 10));
						list.Add(new KeyValuePair<string, int>(text, 20));
						list.Add(new KeyValuePair<string, int>(Url, 30));
						list.Add(new KeyValuePair<string, int>(text, 60));
						list.Add(new KeyValuePair<string, int>(Url, 60));
					}
					else
					{
						list.Add(new KeyValuePair<string, int>(Url, 5));
						list.Add(new KeyValuePair<string, int>(Url, 20));
						list.Add(new KeyValuePair<string, int>(text, 30));
						list.Add(new KeyValuePair<string, int>(Url, 60));
						list.Add(new KeyValuePair<string, int>(text, 60));
					}
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 1, false))
				{
					list.Add(new KeyValuePair<string, int>(Url, 5));
					list.Add(new KeyValuePair<string, int>(Url, 20));
					list.Add(new KeyValuePair<string, int>(text, 30));
					list.Add(new KeyValuePair<string, int>(Url, 60));
					list.Add(new KeyValuePair<string, int>(text, 60));
				}
				else
				{
					list.Add(new KeyValuePair<string, int>(Url, 5));
					list.Add(new KeyValuePair<string, int>(Url, 30));
					list.Add(new KeyValuePair<string, int>(Url, 60));
					list.Add(new KeyValuePair<string, int>(text, 60));
					list.Add(new KeyValuePair<string, int>(text, 60));
				}
			}
			string text2 = "";
			try
			{
				foreach (KeyValuePair<string, int> keyValuePair in list)
				{
					try
					{
						return ModNet.NetGetCodeByRequestOnce(keyValuePair.Key, Encoding.UTF8, checked(keyValuePair.Value * 1000), IsJson, "", true);
					}
					catch (Exception ex)
					{
						text2 = text2 + ex.Message + "\r\n";
					}
				}
			}
			finally
			{
				List<KeyValuePair<string, int>>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			throw new Exception(text2);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0005F240 File Offset: 0x0005D440
		public static string DlModRequest(string Url, string Method, string Data, string ContentType)
		{
			string text = ModDownload.DlSourceModGet(Url);
			List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
			if (Operators.CompareString(text, Url, false) != 0)
			{
				object left = ModBase.m_IdentifierRepository.Get("ToolDownloadMod", null);
				if (Operators.ConditionalCompareObjectEqual(left, 0, false))
				{
					if (ModBase._TokenRepository)
					{
						list.Add(new KeyValuePair<string, int>(text, 10));
						list.Add(new KeyValuePair<string, int>(text, 20));
						list.Add(new KeyValuePair<string, int>(Url, 30));
						list.Add(new KeyValuePair<string, int>(text, 60));
						list.Add(new KeyValuePair<string, int>(Url, 60));
					}
					else
					{
						list.Add(new KeyValuePair<string, int>(Url, 5));
						list.Add(new KeyValuePair<string, int>(Url, 20));
						list.Add(new KeyValuePair<string, int>(text, 30));
						list.Add(new KeyValuePair<string, int>(Url, 60));
						list.Add(new KeyValuePair<string, int>(text, 60));
					}
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 1, false))
				{
					list.Add(new KeyValuePair<string, int>(Url, 5));
					list.Add(new KeyValuePair<string, int>(Url, 20));
					list.Add(new KeyValuePair<string, int>(text, 30));
					list.Add(new KeyValuePair<string, int>(Url, 60));
					list.Add(new KeyValuePair<string, int>(text, 60));
				}
				else
				{
					list.Add(new KeyValuePair<string, int>(Url, 5));
					list.Add(new KeyValuePair<string, int>(Url, 30));
					list.Add(new KeyValuePair<string, int>(Url, 60));
					list.Add(new KeyValuePair<string, int>(text, 60));
					list.Add(new KeyValuePair<string, int>(text, 60));
				}
			}
			string text2 = "";
			try
			{
				foreach (KeyValuePair<string, int> keyValuePair in list)
				{
					try
					{
						return ModNet.NetRequestOnce(keyValuePair.Key, Method, Data, ContentType, checked(keyValuePair.Value * 1000), null, true, false);
					}
					catch (Exception ex)
					{
						text2 = text2 + ex.Message + "\r\n";
					}
				}
			}
			finally
			{
				List<KeyValuePair<string, int>>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			throw new Exception(text2);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0005F460 File Offset: 0x0005D660
		public static string[] DlSourceResourceGet(string Original)
		{
			Original = Original.Replace("http://resources.download.minecraft.net", "https://resources.download.minecraft.net");
			return new string[]
			{
				Original.Replace("https://piston-data.mojang.com", "https://bmclapi2.bangbang93.com/assets").Replace("https://piston-meta.mojang.com", "https://bmclapi2.bangbang93.com/assets").Replace("https://resources.download.minecraft.net", "https://bmclapi2.bangbang93.com/assets"),
				Original
			};
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0005F4BC File Offset: 0x0005D6BC
		public static string[] DlSourceLibraryGet(string Original)
		{
			return new string[]
			{
				Original.Replace("https://piston-data.mojang.com", "https://bmclapi2.bangbang93.com/maven").Replace("https://piston-meta.mojang.com", "https://bmclapi2.bangbang93.com/maven").Replace("https://libraries.minecraft.net", "https://bmclapi2.bangbang93.com/maven"),
				Original.Replace("https://piston-data.mojang.com", "https://bmclapi2.bangbang93.com/libraries").Replace("https://piston-meta.mojang.com", "https://bmclapi2.bangbang93.com/libraries").Replace("https://libraries.minecraft.net", "https://bmclapi2.bangbang93.com/libraries"),
				Original
			};
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0005F538 File Offset: 0x0005D738
		public static string DlSourceModGet(string Original)
		{
			return Original.Replace("api.modrinth.com", "mod.mcimirror.top/modrinth").Replace("staging-api.modrinth.com", "mod.mcimirror.top/modrinth").Replace("cdn.modrinth.com", "mod.mcimirror.top").Replace("api.curseforge.com", "mod.mcimirror.top/curseforge").Replace("edge.forgecdn.net", "mod.mcimirror.top").Replace("mediafilez.forgecdn.net", "mod.mcimirror.top").Replace("media.forgecdn.net", "mod.mcimirror.top");
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0005F5B0 File Offset: 0x0005D7B0
		public static string[] DlSourceLauncherOrMetaGet(string Original)
		{
			if (Original == null)
			{
				throw new Exception("无对应的 json 下载地址");
			}
			return new string[]
			{
				Original.Replace("https://piston-data.mojang.com", "https://bmclapi2.bangbang93.com").Replace("https://piston-meta.mojang.com", "https://bmclapi2.bangbang93.com").Replace("https://launcher.mojang.com", "https://bmclapi2.bangbang93.com").Replace("https://launchermeta.mojang.com", "https://bmclapi2.bangbang93.com"),
				Original
			};
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0005F618 File Offset: 0x0005D818
		private static void DlSourceLoader<InputType, OutputType>(ModLoader.LoaderTask<InputType, OutputType> MainLoader, List<KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int>> LoaderList, bool IsForceRestart = false)
		{
			int num = 0;
			checked
			{
				for (;;)
				{
					IL_21B:
					bool flag = true;
					try
					{
						foreach (KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int> keyValuePair in LoaderList)
						{
							if (num == 0)
							{
								if (IsForceRestart || (keyValuePair.Key.Input == null ^ MainLoader.Input == null))
								{
									continue;
								}
								if (keyValuePair.Key.Input != null)
								{
									ModLoader.LoaderTask<!!0, !!1> key = keyValuePair.Key;
									ref !!0 ptr = ref key.Input;
									if (default(!!0) == null)
									{
										InputType input = key.Input;
										ptr = ref input;
									}
									if (!ptr.Equals(MainLoader.Input))
									{
										continue;
									}
								}
							}
							if (keyValuePair.Key.State != ModBase.LoadState.Failed)
							{
								flag = false;
							}
							if (keyValuePair.Key.State == ModBase.LoadState.Finished)
							{
								MainLoader.Output = keyValuePair.Key.Output;
								ModDownload.DlSourceLoaderAbort<InputType, OutputType>(LoaderList);
								return;
							}
							if (flag && num < keyValuePair.Value * 100)
							{
								num = keyValuePair.Value * 100;
							}
						}
					}
					finally
					{
						List<KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int>>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					if (num == 0)
					{
						Enumerable.First<KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int>>(LoaderList).Key.Start(MainLoader.Input, IsForceRestart);
						try
						{
							foreach (KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int> keyValuePair2 in Enumerable.Skip<KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int>>(LoaderList, 1))
							{
								keyValuePair2.Key.State = ModBase.LoadState.Waiting;
							}
						}
						finally
						{
							IEnumerator<KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int>> enumerator2;
							if (enumerator2 != null)
							{
								enumerator2.Dispose();
							}
						}
					}
					int num2 = LoaderList.Count - 1;
					int i = 0;
					while (i <= num2)
					{
						if (num == LoaderList[i].Value * 100)
						{
							if (i >= LoaderList.Count - 1 || Enumerable.All<KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int>>(LoaderList, (ModDownload._Closure$__66<!!0, !!1>.$I66-0 == null) ? (ModDownload._Closure$__66<!!0, !!1>.$I66-0 = ((KeyValuePair<ModLoader.LoaderTask<$CLS0, $CLS1>, int> l) => l.Key.State == ModBase.LoadState.Failed)) : ModDownload._Closure$__66<!!0, !!1>.$I66-0))
							{
								goto IL_22D;
							}
							LoaderList[i + 1].Key.Start(MainLoader.Input, IsForceRestart);
							IL_208:
							Thread.Sleep(10);
							num++;
							if (!MainLoader.IsAborted)
							{
								goto IL_21B;
							}
							goto IL_222;
						}
						else
						{
							i++;
						}
					}
					goto IL_208;
				}
				IL_222:
				ModDownload.DlSourceLoaderAbort<InputType, OutputType>(LoaderList);
				return;
				IL_22D:
				Exception ex = null;
				int num3 = LoaderList.Count - 1;
				for (int j = 0; j <= num3; j++)
				{
					LoaderList[j].Key.Input = default(!!0);
					if (LoaderList[j].Key.Error != null && (ex == null || LoaderList[j].Key.Error.Message.Contains("没有可用版本")))
					{
						ex = LoaderList[j].Key.Error;
					}
				}
				if (ex == null)
				{
					ex = new TimeoutException("下载源连接超时");
				}
				ModDownload.DlSourceLoaderAbort<InputType, OutputType>(LoaderList);
				throw ex;
			}
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0005F920 File Offset: 0x0005DB20
		private static void DlSourceLoaderAbort<InputType, OutputType>(List<KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int>> LoaderList)
		{
			try
			{
				foreach (KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int> keyValuePair in LoaderList)
				{
					if (keyValuePair.Key.State == ModBase.LoadState.Loading)
					{
						keyValuePair.Key.Abort();
					}
				}
			}
			finally
			{
				List<KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int>>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
		}

		// Token: 0x0400071C RID: 1820
		public static ModLoader.LoaderTask<string, ModDownload.DlClientListResult> accountTests = new ModLoader.LoaderTask<string, ModDownload.DlClientListResult>("DlClientList Main", new Action<ModLoader.LoaderTask<string, ModDownload.DlClientListResult>>(ModDownload.DlClientListMain), null, ThreadPriority.Normal);

		// Token: 0x0400071D RID: 1821
		public static ModLoader.LoaderTask<string, ModDownload.DlClientListResult> queueTests = new ModLoader.LoaderTask<string, ModDownload.DlClientListResult>("DlClientList Mojang", new Action<ModLoader.LoaderTask<string, ModDownload.DlClientListResult>>(ModDownload.DlClientListMojangMain), null, ThreadPriority.Normal);

		// Token: 0x0400071E RID: 1822
		private static bool m_EventTests = false;

		// Token: 0x0400071F RID: 1823
		public static ModLoader.LoaderTask<string, ModDownload.DlClientListResult> _ManagerTests = new ModLoader.LoaderTask<string, ModDownload.DlClientListResult>("DlClientList Bmclapi", new Action<ModLoader.LoaderTask<string, ModDownload.DlClientListResult>>(ModDownload.DlClientListBmclapiMain), null, ThreadPriority.Normal);

		// Token: 0x04000720 RID: 1824
		public static ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult> m_ModelTests = new ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult>("DlOptiFineList Main", new Action<ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult>>(ModDownload.DlOptiFineListMain), null, ThreadPriority.Normal);

		// Token: 0x04000721 RID: 1825
		public static ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult> _WrapperTests = new ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult>("DlOptiFineList Official", new Action<ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult>>(ModDownload.DlOptiFineListOfficialMain), null, ThreadPriority.Normal);

		// Token: 0x04000722 RID: 1826
		public static ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult> _BaseTests = new ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult>("DlOptiFineList Bmclapi", new Action<ModLoader.LoaderTask<int, ModDownload.DlOptiFineListResult>>(ModDownload.DlOptiFineListBmclapiMain), null, ThreadPriority.Normal);

		// Token: 0x04000723 RID: 1827
		public static ModLoader.LoaderTask<int, ModDownload.DlForgeListResult> m_AttributeTests = new ModLoader.LoaderTask<int, ModDownload.DlForgeListResult>("DlForgeList Main", new Action<ModLoader.LoaderTask<int, ModDownload.DlForgeListResult>>(ModDownload.DlForgeListMain), null, ThreadPriority.Normal);

		// Token: 0x04000724 RID: 1828
		public static ModLoader.LoaderTask<int, ModDownload.DlForgeListResult> m_CodeTests = new ModLoader.LoaderTask<int, ModDownload.DlForgeListResult>("DlForgeList Official", new Action<ModLoader.LoaderTask<int, ModDownload.DlForgeListResult>>(ModDownload.DlForgeListOfficialMain), null, ThreadPriority.Normal);

		// Token: 0x04000725 RID: 1829
		public static ModLoader.LoaderTask<int, ModDownload.DlForgeListResult> m_PrototypeTests = new ModLoader.LoaderTask<int, ModDownload.DlForgeListResult>("DlForgeList Bmclapi", new Action<ModLoader.LoaderTask<int, ModDownload.DlForgeListResult>>(ModDownload.DlForgeListBmclapiMain), null, ThreadPriority.Normal);

		// Token: 0x04000726 RID: 1830
		public static ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult> m_AnnotationTests = new ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult>("DlNeoForgeList Main", new Action<ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult>>(ModDownload.DlNeoForgeListMain), null, ThreadPriority.Normal);

		// Token: 0x04000727 RID: 1831
		public static ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult> infoTests = new ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult>("DlNeoForgeList Official", new Action<ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult>>(ModDownload.DlNeoForgeListOfficialMain), null, ThreadPriority.Normal);

		// Token: 0x04000728 RID: 1832
		public static ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult> m_AdapterTests = new ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult>("DlNeoForgeList Bmclapi", new Action<ModLoader.LoaderTask<int, ModDownload.DlNeoForgeListResult>>(ModDownload.DlNeoForgeListBmclapiMain), null, ThreadPriority.Normal);

		// Token: 0x04000729 RID: 1833
		public static ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult> _FacadeTests = new ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult>("DlLiteLoaderList Main", new Action<ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult>>(ModDownload.DlLiteLoaderListMain), null, ThreadPriority.Normal);

		// Token: 0x0400072A RID: 1834
		public static ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult> listTests = new ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult>("DlLiteLoaderList Official", new Action<ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult>>(ModDownload.DlLiteLoaderListOfficialMain), null, ThreadPriority.Normal);

		// Token: 0x0400072B RID: 1835
		public static ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult> m_MerchantTests = new ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult>("DlLiteLoaderList Bmclapi", new Action<ModLoader.LoaderTask<int, ModDownload.DlLiteLoaderListResult>>(ModDownload.DlLiteLoaderListBmclapiMain), null, ThreadPriority.Normal);

		// Token: 0x0400072C RID: 1836
		public static ModLoader.LoaderTask<int, ModDownload.DlFabricListResult> authenticationTests = new ModLoader.LoaderTask<int, ModDownload.DlFabricListResult>("DlFabricList Main", new Action<ModLoader.LoaderTask<int, ModDownload.DlFabricListResult>>(ModDownload.DlFabricListMain), null, ThreadPriority.Normal);

		// Token: 0x0400072D RID: 1837
		public static ModLoader.LoaderTask<int, ModDownload.DlFabricListResult> algoTests = new ModLoader.LoaderTask<int, ModDownload.DlFabricListResult>("DlFabricList Official", new Action<ModLoader.LoaderTask<int, ModDownload.DlFabricListResult>>(ModDownload.DlFabricListOfficialMain), null, ThreadPriority.Normal);

		// Token: 0x0400072E RID: 1838
		public static ModLoader.LoaderTask<int, ModDownload.DlFabricListResult> _ComparatorTests = new ModLoader.LoaderTask<int, ModDownload.DlFabricListResult>("DlFabricList Bmclapi", new Action<ModLoader.LoaderTask<int, ModDownload.DlFabricListResult>>(ModDownload.DlFabricListBmclapiMain), null, ThreadPriority.Normal);

		// Token: 0x0400072F RID: 1839
		public static ModLoader.LoaderTask<int, List<ModComp.CompFile>> mappingTests = new ModLoader.LoaderTask<int, List<ModComp.CompFile>>("Fabric API List Loader", delegate(ModLoader.LoaderTask<int, List<ModComp.CompFile>> Task)
		{
			Task.Output = ModComp.CompFilesGet("fabric-api", false);
		}, null, ThreadPriority.Normal);

		// Token: 0x04000730 RID: 1840
		public static ModLoader.LoaderTask<int, List<ModComp.CompFile>> tokenizerTests = new ModLoader.LoaderTask<int, List<ModComp.CompFile>>("OptiFabric List Loader", delegate(ModLoader.LoaderTask<int, List<ModComp.CompFile>> Task)
		{
			Task.Output = ModComp.CompFilesGet("322385", true);
		}, null, ThreadPriority.Normal);

		// Token: 0x0200014F RID: 335
		public enum AssetsIndexExistsBehaviour
		{
			// Token: 0x04000732 RID: 1842
			DontDownload,
			// Token: 0x04000733 RID: 1843
			DownloadInBackground,
			// Token: 0x04000734 RID: 1844
			AlwaysDownload
		}

		// Token: 0x02000150 RID: 336
		public struct DlClientListResult
		{
			// Token: 0x04000735 RID: 1845
			public string classTest;

			// Token: 0x04000736 RID: 1846
			public bool policyTest;

			// Token: 0x04000737 RID: 1847
			public JObject Value;
		}

		// Token: 0x02000151 RID: 337
		public struct DlOptiFineListResult
		{
			// Token: 0x04000738 RID: 1848
			public string orderTest;

			// Token: 0x04000739 RID: 1849
			public bool producerTest;

			// Token: 0x0400073A RID: 1850
			public List<ModDownload.DlOptiFineListEntry> Value;
		}

		// Token: 0x02000152 RID: 338
		public class DlOptiFineListEntry
		{
			// Token: 0x06000DD8 RID: 3544 RVA: 0x00008C7C File Offset: 0x00006E7C
			public string CheckMapper()
			{
				return this.strategyTest;
			}

			// Token: 0x06000DD9 RID: 3545 RVA: 0x00008C84 File Offset: 0x00006E84
			public void CompareMapper(string value)
			{
				if (value.EndsWithF(".0", false))
				{
					value = Strings.Left(value, checked(value.Length - 2));
				}
				this.strategyTest = value;
			}

			// Token: 0x0400073B RID: 1851
			public string m_SchemaTest;

			// Token: 0x0400073C RID: 1852
			public string _DescriptorTest;

			// Token: 0x0400073D RID: 1853
			public string m_PublisherTest;

			// Token: 0x0400073E RID: 1854
			public bool _DefinitionTest;

			// Token: 0x0400073F RID: 1855
			private string strategyTest;

			// Token: 0x04000740 RID: 1856
			public string m_ProcTest;

			// Token: 0x04000741 RID: 1857
			public string parserMap;
		}

		// Token: 0x02000153 RID: 339
		public struct DlForgeListResult
		{
			// Token: 0x04000742 RID: 1858
			public string m_BroadcasterMap;

			// Token: 0x04000743 RID: 1859
			public bool m_FieldMap;

			// Token: 0x04000744 RID: 1860
			public List<string> Value;
		}

		// Token: 0x02000154 RID: 340
		public abstract class DlForgelikeEntry
		{
			// Token: 0x06000DDC RID: 3548 RVA: 0x00008CAB File Offset: 0x00006EAB
			public string CustomizeMapper()
			{
				if (!this.m_ReaderMap)
				{
					return "Forge";
				}
				return "NeoForge";
			}

			// Token: 0x06000DDD RID: 3549 RVA: 0x0005F984 File Offset: 0x0005DB84
			public string AssetMapper()
			{
				string result;
				if (this.m_ReaderMap)
				{
					result = "jar";
				}
				else
				{
					result = ((Operators.CompareString(((ModDownload.DlForgeVersionEntry)this)._ComposerMap, "installer", false) == 0) ? "jar" : "zip");
				}
				return result;
			}

			// Token: 0x17000218 RID: 536
			// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00008CC0 File Offset: 0x00006EC0
			public bool IsLegacy
			{
				get
				{
					return this.clientMap.Major < 20;
				}
			}

			// Token: 0x04000745 RID: 1861
			public bool m_ReaderMap;

			// Token: 0x04000746 RID: 1862
			public Version clientMap;

			// Token: 0x04000747 RID: 1863
			public string m_ConfigMap;

			// Token: 0x04000748 RID: 1864
			public string _TestsMap;
		}

		// Token: 0x02000155 RID: 341
		public class DlForgeVersionEntry : ModDownload.DlForgelikeEntry
		{
			// Token: 0x06000DE0 RID: 3552 RVA: 0x0005F9C8 File Offset: 0x0005DBC8
			public DlForgeVersionEntry(string Version, string Branch, string Inherit)
			{
				this.threadMap = null;
				if (Operators.CompareString(Version, "11.15.1.2318", false) == 0 || Operators.CompareString(Version, "11.15.1.1902", false) == 0 || Operators.CompareString(Version, "11.15.1.1890", false) == 0)
				{
					Branch = "1.8.9";
				}
				if (Branch == null && Operators.CompareString(Inherit, "1.7.10", false) == 0 && Conversions.ToDouble(Version.Split(".")[3]) >= 1300.0)
				{
					Branch = "1.7.10";
				}
				this.m_ReaderMap = false;
				this.m_ConfigMap = Version;
				this.clientMap = new Version(Version);
				this._TestsMap = Inherit;
				this.iteratorMap = Version + ((Branch == null) ? "" : ("-" + Branch));
			}

			// Token: 0x04000749 RID: 1865
			public string mapperMap;

			// Token: 0x0400074A RID: 1866
			public string threadMap;

			// Token: 0x0400074B RID: 1867
			public bool propertyMap;

			// Token: 0x0400074C RID: 1868
			public string _ComposerMap;

			// Token: 0x0400074D RID: 1869
			public string iteratorMap;
		}

		// Token: 0x02000156 RID: 342
		public struct DlNeoForgeListResult
		{
			// Token: 0x0400074E RID: 1870
			public string _RepositoryMap;

			// Token: 0x0400074F RID: 1871
			public bool testMap;

			// Token: 0x04000750 RID: 1872
			public List<ModDownload.DlNeoForgeListEntry> Value;
		}

		// Token: 0x02000157 RID: 343
		public class DlNeoForgeListEntry : ModDownload.DlForgelikeEntry
		{
			// Token: 0x06000DE2 RID: 3554 RVA: 0x0005FA8C File Offset: 0x0005DC8C
			public string RateMapper()
			{
				string text = base.IsLegacy ? "forge" : "neoforge";
				return string.Format("https://maven.neoforged.net/releases/net/neoforged/{0}/{1}/{2}-{3}", new object[]
				{
					text,
					this._ErrorMap,
					text,
					this._ErrorMap
				});
			}

			// Token: 0x06000DE3 RID: 3555 RVA: 0x0005FAD8 File Offset: 0x0005DCD8
			public DlNeoForgeListEntry(string ApiName)
			{
				this.m_ReaderMap = true;
				this._ErrorMap = ApiName;
				this._MapMap = ApiName.Contains("beta");
				if (ApiName.Contains("1.20.1"))
				{
					this.m_ConfigMap = ApiName.Replace("1.20.1-", "");
					this.clientMap = new Version("19." + this.m_ConfigMap);
					this._TestsMap = "1.20.1";
					return;
				}
				this.m_ConfigMap = ApiName;
				this.clientMap = new Version(ApiName.BeforeFirst("-", false));
				this._TestsMap = string.Format("1.{0}", this.clientMap.Major) + ((this.clientMap.Minor == 0) ? "" : ("." + Conversions.ToString(this.clientMap.Minor)));
			}

			// Token: 0x04000751 RID: 1873
			public bool _MapMap;

			// Token: 0x04000752 RID: 1874
			public string _ErrorMap;
		}

		// Token: 0x02000158 RID: 344
		public struct DlLiteLoaderListResult
		{
			// Token: 0x04000753 RID: 1875
			public string SourceName;

			// Token: 0x04000754 RID: 1876
			public bool IsOfficial;

			// Token: 0x04000755 RID: 1877
			public List<ModDownload.DlLiteLoaderListEntry> Value;

			// Token: 0x04000756 RID: 1878
			public Exception OfficialError;
		}

		// Token: 0x02000159 RID: 345
		public class DlLiteLoaderListEntry
		{
			// Token: 0x04000757 RID: 1879
			public string FileName;

			// Token: 0x04000758 RID: 1880
			public bool IsPreview;

			// Token: 0x04000759 RID: 1881
			public string Inherit;

			// Token: 0x0400075A RID: 1882
			public bool IsLegacy;

			// Token: 0x0400075B RID: 1883
			public string ReleaseTime;

			// Token: 0x0400075C RID: 1884
			public string MD5;

			// Token: 0x0400075D RID: 1885
			public JToken JsonToken;
		}

		// Token: 0x0200015A RID: 346
		public struct DlFabricListResult
		{
			// Token: 0x0400075E RID: 1886
			public string m_ContextMap;

			// Token: 0x0400075F RID: 1887
			public bool specificationMap;

			// Token: 0x04000760 RID: 1888
			public JObject Value;
		}
	}
}
