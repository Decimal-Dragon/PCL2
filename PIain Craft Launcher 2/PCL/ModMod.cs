using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PCL
{
	// Token: 0x0200006E RID: 110
	[StandardModule]
	public sealed class ModMod
	{
		// Token: 0x06000297 RID: 663 RVA: 0x0001D2B8 File Offset: 0x0001B4B8
		private static void McModLoad(ModLoader.LoaderTask<string, List<ModMod.McMod>> Loader)
		{
			try
			{
				ModBase.RunInUiWait((ModMod._Closure$__.$I4-0 == null) ? (ModMod._Closure$__.$I4-0 = delegate()
				{
					if (ModMain._ThreadRepository != null)
					{
						ModMain._ThreadRepository.Load.ShowProgress = false;
					}
				}) : ModMod._Closure$__.$I4-0);
				if (PageVersionMod._RuleConfig.Contains(Loader.Input))
				{
					ModBase.Log("[Mod] 等待 Mod 更新完成后才能继续加载 Mod 列表：" + Loader.Input, ModBase.LogLevel.Normal, "出现错误");
					try
					{
						ModBase.RunInUiWait((ModMod._Closure$__.$I4-1 == null) ? (ModMod._Closure$__.$I4-1 = delegate()
						{
							if (ModMain._ThreadRepository != null)
							{
								ModMain._ThreadRepository.Load.Text = "正在更新 Mod";
							}
						}) : ModMod._Closure$__.$I4-1);
						while (PageVersionMod._RuleConfig.Contains(Loader.Input))
						{
							if (Loader.IsAborted)
							{
								return;
							}
							Thread.Sleep(100);
						}
					}
					finally
					{
						ModBase.RunInUiWait((ModMod._Closure$__.$I4-2 == null) ? (ModMod._Closure$__.$I4-2 = delegate()
						{
							if (ModMain._ThreadRepository != null)
							{
								ModMain._ThreadRepository.Load.Text = "正在加载 Mod 列表";
							}
						}) : ModMod._Closure$__.$I4-2);
					}
					ModMain._ThreadRepository.LoaderRun(ModLoader.LoaderFolderRunType.UpdateOnly);
				}
				List<FileInfo> list = new List<FileInfo>();
				if (Directory.Exists(Loader.Input))
				{
					string right = Loader.Input.ToLower();
					try
					{
						foreach (FileInfo fileInfo in ModBase.EnumerateFiles(Loader.Input))
						{
							if ((Operators.CompareString(fileInfo.DirectoryName.ToLower() + "\\", right, false) == 0 || (PageVersionLeft._InstanceConfig != null && PageVersionLeft._InstanceConfig.Version.m_StructMap && PageVersionLeft._InstanceConfig.Version._ServerMap < 13 && Operators.CompareString(fileInfo.Directory.Name, "1." + Conversions.ToString(PageVersionLeft._InstanceConfig.Version._ServerMap) + "." + Conversions.ToString(PageVersionLeft._InstanceConfig.Version.m_ResolverMap), false) == 0)) && Conversions.ToBoolean(ModMod.McMod.IsModFile(fileInfo.FullName)))
							{
								list.Add(fileInfo);
							}
						}
					}
					finally
					{
						IEnumerator<FileInfo> enumerator;
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
				}
				Loader.Progress = 0.05;
				if (list.Count > 50)
				{
					ModBase.RunInUi((ModMod._Closure$__.$I4-3 == null) ? (ModMod._Closure$__.$I4-3 = delegate()
					{
						if (ModMain._ThreadRepository != null)
						{
							ModMain._ThreadRepository.Load.ShowProgress = true;
						}
					}) : ModMod._Closure$__.$I4-3, false);
				}
				string filePath = ModBase.m_DecoratorRepository + "Cache\\LocalMod.json";
				JObject jobject = new JObject();
				try
				{
					string text = ModBase.ReadFile(filePath, null);
					if (!string.IsNullOrWhiteSpace(text))
					{
						jobject = (JObject)ModBase.GetJson(text);
						if (!jobject.ContainsKey("version") || jobject["version"].ToObject<int>() != 7)
						{
							ModBase.Log("[Mod] 本地 Mod 信息缓存版本已过期，将弃用这些缓存信息", ModBase.LogLevel.Debug, "出现错误");
							jobject = new JObject();
						}
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "读取本地 Mod 信息缓存失败，已重置", ModBase.LogLevel.Debug, "出现错误");
					jobject = new JObject();
				}
				jobject["version"] = 7;
				List<ModMod.McMod> list2 = new List<ModMod.McMod>();
				List<ModMod.McMod> list3 = new List<ModMod.McMod>();
				try
				{
					foreach (FileInfo fileInfo2 in list)
					{
						ModMod._Closure$__4-0 CS$<>8__locals1 = new ModMod._Closure$__4-0(CS$<>8__locals1);
						Loader.Progress += 0.94 / (double)list.Count;
						if (Loader.IsAborted)
						{
							return;
						}
						CS$<>8__locals1.$VB$Local_ModEntry = new ModMod.McMod(fileInfo2.FullName);
						CS$<>8__locals1.$VB$Local_ModEntry.Load(false);
						ModMod.McMod mcMod = Enumerable.FirstOrDefault<ModMod.McMod>(list2, (ModMod.McMod m) => Operators.CompareString(m.ConcatTests(), CS$<>8__locals1.$VB$Local_ModEntry.ConcatTests(), false) == 0);
						if (mcMod != null)
						{
							ModMod.McMod mcMod2 = (mcMod.State == ModMod.McMod.McModState.Disabled) ? mcMod : CS$<>8__locals1.$VB$Local_ModEntry;
							ModBase.Log(string.Format("[Mod] 重复的 Mod 文件：{0} 与 {1}，已忽略 {2}", mcMod.FileName, CS$<>8__locals1.$VB$Local_ModEntry.FileName, mcMod2.FileName), ModBase.LogLevel.Debug, "出现错误");
							if (mcMod2 == CS$<>8__locals1.$VB$Local_ModEntry)
							{
								continue;
							}
							list2.Remove(mcMod2);
							list3.Remove(mcMod2);
						}
						list2.Add(CS$<>8__locals1.$VB$Local_ModEntry);
						if (CS$<>8__locals1.$VB$Local_ModEntry.State != ModMod.McMod.McModState.Unavailable)
						{
							string text2 = CS$<>8__locals1.$VB$Local_ModEntry.ReflectMapper() + PageVersionLeft._InstanceConfig.Version._ConnectionMap + ModMod.GetTargetModLoaders().Join("");
							if (jobject.ContainsKey(text2))
							{
								CS$<>8__locals1.$VB$Local_ModEntry.FromJson((JObject)jobject[text2]);
								if (CS$<>8__locals1.$VB$Local_ModEntry.m_RepositoryTest && DateTime.Now - jobject[text2]["Comp"]["CacheTime"].ToObject<DateTime>() < new TimeSpan(6, 0, 0))
								{
									continue;
								}
							}
							list3.Add(CS$<>8__locals1.$VB$Local_ModEntry);
						}
					}
				}
				finally
				{
					List<FileInfo>.Enumerator enumerator2;
					((IDisposable)enumerator2).Dispose();
				}
				Loader.Progress = 0.99;
				ModBase.Log(string.Format("[Mod] 共有 {0} 个 Mod，其中 {1} 个需要联网获取信息，{2} 个需要更新信息", list2.Count, Enumerable.Count<ModMod.McMod>(Enumerable.Where<ModMod.McMod>(list3, (ModMod._Closure$__.$I4-5 == null) ? (ModMod._Closure$__.$I4-5 = ((ModMod.McMod m) => m.Comp == null)) : ModMod._Closure$__.$I4-5)), Enumerable.Count<ModMod.McMod>(Enumerable.Where<ModMod.McMod>(list3, (ModMod._Closure$__.$I4-6 == null) ? (ModMod._Closure$__.$I4-6 = ((ModMod.McMod m) => m.Comp != null)) : ModMod._Closure$__.$I4-6))), ModBase.LogLevel.Normal, "出现错误");
				list2 = list2.Sort((ModMod._Closure$__.$I4-7 == null) ? (ModMod._Closure$__.$I4-7 = delegate(ModMod.McMod Left, ModMod.McMod Right)
				{
					bool result;
					if (Left.State == ModMod.McMod.McModState.Unavailable != (Right.State == ModMod.McMod.McModState.Unavailable))
					{
						result = (Left.State == ModMod.McMod.McModState.Unavailable);
					}
					else
					{
						result = (~Right.FileName.CompareTo(Left.FileName) != 0);
					}
					return result;
				}) : ModMod._Closure$__.$I4-7);
				if (!Loader.IsAborted)
				{
					Loader.Output = list2;
					if (Enumerable.Any<ModMod.McMod>(list3))
					{
						ModMod.m_Service.Start(new KeyValuePair<List<ModMod.McMod>, JObject>(list3, jobject), true);
					}
				}
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, "Mod 列表加载失败", ModBase.LogLevel.Debug, "出现错误");
				throw;
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0001D914 File Offset: 0x0001BB14
		private static void McModDetailLoad(ModLoader.LoaderTask<KeyValuePair<List<ModMod.McMod>, JObject>, int> Loader)
		{
			ModMod._Closure$__6-0 CS$<>8__locals1 = new ModMod._Closure$__6-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_Loader = Loader;
			CS$<>8__locals1.$VB$Local_Mods = CS$<>8__locals1.$VB$Local_Loader.Input.Key;
			JObject value = CS$<>8__locals1.$VB$Local_Loader.Input.Value;
			ModMinecraft.McVersionInfo version = PageVersionLeft._InstanceConfig.Version;
			CS$<>8__locals1.$VB$Local_ModLoaders = ModMod.GetTargetModLoaders();
			CS$<>8__locals1.$VB$Local_McVersion = version._ConnectionMap;
			ModBase.Log(string.Format("[Mod] 目标加载器：{0}，版本：{1}", CS$<>8__locals1.$VB$Local_ModLoaders.Join("/"), CS$<>8__locals1.$VB$Local_McVersion), ModBase.LogLevel.Normal, "出现错误");
			CS$<>8__locals1.$VB$Local_EndedThreadCount = 0;
			CS$<>8__locals1.$VB$Local_IsFailed = false;
			CS$<>8__locals1.$VB$Local_MainThread = Thread.CurrentThread;
			checked
			{
				ModBase.RunInNewThread(delegate
				{
					try
					{
						List<string> list = Enumerable.ToList<string>(Enumerable.Select<ModMod.McMod, string>(CS$<>8__locals1.$VB$Local_Mods, (ModMod._Closure$__.$I6-1 == null) ? (ModMod._Closure$__.$I6-1 = ((ModMod.McMod m) => m.ReflectMapper())) : ModMod._Closure$__.$I6-1));
						JObject jobject = (JObject)ModBase.GetJson(ModDownload.DlModRequest("https://api.modrinth.com/v2/version_files", "POST", string.Format("{{\"hashes\": [\"{0}\"], \"algorithm\": \"sha1\"}}", list.Join("\",\"")), "application/json"));
						ModBase.Log(string.Format("[Mod] 从 Modrinth 获取到 {0} 个本地 Mod 的对应信息", jobject.Count), ModBase.LogLevel.Normal, "出现错误");
						if (jobject.Count != 0)
						{
							Dictionary<string, List<ModMod.McMod>> dictionary = new Dictionary<string, List<ModMod.McMod>>();
							try
							{
								foreach (ModMod.McMod mcMod2 in CS$<>8__locals1.$VB$Local_Mods)
								{
									if (jobject.ContainsKey(mcMod2.ReflectMapper()) && !((string)jobject[mcMod2.ReflectMapper()]["files"][0]["hashes"]["sha1"] != mcMod2.ReflectMapper()))
									{
										string key = jobject[mcMod2.ReflectMapper()]["project_id"].ToString();
										if (ModComp.pool.ContainsKey(key) && mcMod2.Comp == null)
										{
											mcMod2.Comp = ModComp.pool[key];
										}
										if (!dictionary.ContainsKey(key))
										{
											dictionary[key] = new List<ModMod.McMod>();
										}
										dictionary[key].Add(mcMod2);
										ModComp.CompFile compFile = new ModComp.CompFile((JObject)jobject[mcMod2.ReflectMapper()], ModComp.CompType.Mod);
										if (mcMod2._PropertyTest == null || DateTime.Compare(mcMod2._PropertyTest._ListenerRepository, compFile._ListenerRepository) < 0)
										{
											mcMod2._PropertyTest = compFile;
										}
									}
								}
							}
							finally
							{
								List<ModMod.McMod>.Enumerator enumerator2;
								((IDisposable)enumerator2).Dispose();
							}
							if (!CS$<>8__locals1.$VB$Local_Loader.IsAbortedWithThread(CS$<>8__locals1.$VB$Local_MainThread))
							{
								ModBase.Log(string.Format("[Mod] 需要从 Modrinth 获取 {0} 个本地 Mod 的工程信息", dictionary.Count), ModBase.LogLevel.Normal, "出现错误");
								if (Enumerable.Any<KeyValuePair<string, List<ModMod.McMod>>>(dictionary))
								{
									JArray jarray = (JArray)ModBase.GetJson(ModDownload.DlModRequest(string.Format("https://api.modrinth.com/v2/projects?ids=[\"{0}\"]", dictionary.Keys.Join("\",\"")), "GET", "", "application/json"));
									try
									{
										foreach (JToken jtoken in jarray)
										{
											ModComp.CompProject compProject = new ModComp.CompProject((JObject)jtoken);
											try
											{
												foreach (ModMod.McMod mcMod3 in dictionary[compProject._AuthenticationRepository])
												{
													mcMod3.Comp = compProject;
												}
											}
											finally
											{
												List<ModMod.McMod>.Enumerator enumerator4;
												((IDisposable)enumerator4).Dispose();
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
									ModBase.Log("[Mod] 已从 Modrinth 获取本地 Mod 信息，继续获取更新信息", ModBase.LogLevel.Normal, "出现错误");
									JObject jobject2 = (JObject)ModBase.GetJson(ModDownload.DlModRequest("https://api.modrinth.com/v2/version_files/update", "POST", string.Format("{{\"hashes\": [\"{0}\"], \"algorithm\": \"sha1\", \r\n                    \"loaders\": [\"{1}\"],\"game_versions\": [\"{2}\"]}}", Enumerable.SelectMany<KeyValuePair<string, List<ModMod.McMod>>, string>(dictionary, (ModMod._Closure$__.$I6-2 == null) ? (ModMod._Closure$__.$I6-2 = ((KeyValuePair<string, List<ModMod.McMod>> l) => Enumerable.Select<ModMod.McMod, string>(l.Value, (ModMod._Closure$__.$I6-3 == null) ? (ModMod._Closure$__.$I6-3 = ((ModMod.McMod m) => m.ReflectMapper())) : ModMod._Closure$__.$I6-3))) : ModMod._Closure$__.$I6-2).Join("\",\""), CS$<>8__locals1.$VB$Local_ModLoaders.Join("\",\"").ToLower(), CS$<>8__locals1.$VB$Local_McVersion), "application/json"));
									try
									{
										foreach (ModMod.McMod mcMod4 in CS$<>8__locals1.$VB$Local_Mods)
										{
											if (jobject2.ContainsKey(mcMod4.ReflectMapper()) && mcMod4._PropertyTest != null)
											{
												ModComp.CompFile compFile2 = new ModComp.CompFile((JObject)jobject2[mcMod4.ReflectMapper()], ModComp.CompType.Mod);
												if (compFile2.ExcludeTests())
												{
													if (ModBase._TokenRepository)
													{
														ModBase.Log(string.Format("[Mod] 本地文件 {0} 在 Modrinth 上的最新版为 {1}", mcMod4._PropertyTest._BridgeRepository, compFile2._BridgeRepository), ModBase.LogLevel.Normal, "出现错误");
													}
													if (DateTime.Compare(mcMod4._PropertyTest._ListenerRepository, compFile2._ListenerRepository) < 0 && Operators.CompareString(mcMod4._PropertyTest.m_ReponseRepository, compFile2.m_ReponseRepository, false) != 0)
													{
														if (mcMod4.UpdateFile != null && Operators.CompareString(compFile2.m_ReponseRepository, mcMod4.UpdateFile.m_ReponseRepository, false) == 0)
														{
															mcMod4.m_IteratorTest.Add(string.Format("https://modrinth.com/mod/{0}/changelog?g={1}", jobject2[mcMod4.ReflectMapper()]["project_id"], CS$<>8__locals1.$VB$Local_McVersion));
															compFile2._ItemRepository.AddRange(mcMod4.UpdateFile._ItemRepository);
															mcMod4.UpdateFile = compFile2;
														}
														else if (mcMod4.UpdateFile == null || DateTime.Compare(compFile2._ListenerRepository, mcMod4.UpdateFile._ListenerRepository) >= 0)
														{
															mcMod4.m_IteratorTest = new List<string>
															{
																string.Format("https://modrinth.com/mod/{0}/changelog?g={1}", jobject2[mcMod4.ReflectMapper()]["project_id"], CS$<>8__locals1.$VB$Local_McVersion)
															};
															mcMod4.UpdateFile = compFile2;
														}
													}
												}
											}
										}
									}
									finally
									{
										List<ModMod.McMod>.Enumerator enumerator5;
										((IDisposable)enumerator5).Dispose();
									}
									ModBase.Log("[Mod] 从 Modrinth 获取本地 Mod 信息结束", ModBase.LogLevel.Normal, "出现错误");
								}
							}
						}
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "从 Modrinth 获取本地 Mod 信息失败", ModBase.LogLevel.Debug, "出现错误");
						CS$<>8__locals1.$VB$Local_IsFailed = true;
					}
					finally
					{
						CS$<>8__locals1.$VB$Local_EndedThreadCount++;
					}
				}, "Mod List Detail Loader Modrinth", ThreadPriority.Normal);
				ModBase.RunInNewThread(delegate
				{
					try
					{
						List<uint> list = new List<uint>();
						try
						{
							foreach (ModMod.McMod mcMod2 in CS$<>8__locals1.$VB$Local_Mods)
							{
								list.Add(mcMod2.LogoutMapper());
								if (CS$<>8__locals1.$VB$Local_Loader.IsAbortedWithThread(CS$<>8__locals1.$VB$Local_MainThread))
								{
									return;
								}
							}
						}
						finally
						{
							List<ModMod.McMod>.Enumerator enumerator2;
							((IDisposable)enumerator2).Dispose();
						}
						JContainer jcontainer = (JContainer)((JObject)ModBase.GetJson(ModDownload.DlModRequest("https://api.curseforge.com/v1/fingerprints/432", "POST", string.Format("{{\"fingerprints\": [{0}]}}", list.Join(",")), "application/json")))["data"]["exactMatches"];
						ModBase.Log(string.Format("[Mod] 从 CurseForge 获取到 {0} 个本地 Mod 的对应信息", jcontainer.Count), ModBase.LogLevel.Normal, "出现错误");
						if (Enumerable.Any<JToken>(jcontainer))
						{
							Dictionary<int, List<ModMod.McMod>> dictionary = new Dictionary<int, List<ModMod.McMod>>();
							try
							{
								foreach (JToken jtoken in jcontainer)
								{
									string text = jtoken["id"].ToString();
									uint num = (uint)jtoken["file"]["fileFingerprint"];
									try
									{
										foreach (ModMod.McMod mcMod3 in CS$<>8__locals1.$VB$Local_Mods)
										{
											if (mcMod3.LogoutMapper() == num)
											{
												if (ModComp.pool.ContainsKey(text) && mcMod3.Comp == null)
												{
													mcMod3.Comp = ModComp.pool[text];
												}
												if (!dictionary.ContainsKey(Conversions.ToInteger(text)))
												{
													dictionary[Conversions.ToInteger(text)] = new List<ModMod.McMod>();
												}
												dictionary[Conversions.ToInteger(text)].Add(mcMod3);
												ModComp.CompFile compFile = new ModComp.CompFile((JObject)jtoken["file"], ModComp.CompType.Mod);
												if (mcMod3._PropertyTest == null || DateTime.Compare(mcMod3._PropertyTest._ListenerRepository, compFile._ListenerRepository) < 0)
												{
													mcMod3._PropertyTest = compFile;
												}
											}
										}
									}
									finally
									{
										List<ModMod.McMod>.Enumerator enumerator4;
										((IDisposable)enumerator4).Dispose();
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
							if (!CS$<>8__locals1.$VB$Local_Loader.IsAbortedWithThread(CS$<>8__locals1.$VB$Local_MainThread))
							{
								ModBase.Log(string.Format("[Mod] 需要从 CurseForge 获取 {0} 个本地 Mod 的工程信息", dictionary.Count), ModBase.LogLevel.Normal, "出现错误");
								if (Enumerable.Any<KeyValuePair<int, List<ModMod.McMod>>>(dictionary))
								{
									JToken jtoken2 = ((JObject)ModBase.GetJson(ModDownload.DlModRequest("https://api.curseforge.com/v1/mods", "POST", string.Format("{{\"modIds\": [{0}]}}", dictionary.Keys.Join(",")), "application/json")))["data"];
									Dictionary<int, List<ModMod.McMod>> dictionary2 = new Dictionary<int, List<ModMod.McMod>>();
									Dictionary<int, string> dictionary3 = new Dictionary<int, string>();
									try
									{
										foreach (JToken jtoken3 in jtoken2)
										{
											if (jtoken3["isAvailable"] == null || jtoken3["isAvailable"].ToObject<bool>())
											{
												ModComp.CompProject compProject = new ModComp.CompProject((JObject)jtoken3);
												try
												{
													foreach (ModMod.McMod mcMod4 in dictionary[Conversions.ToInteger(compProject._AuthenticationRepository)])
													{
														if (mcMod4.Comp != null && !mcMod4.Comp.listRepository)
														{
															mcMod4.Comp = mcMod4.Comp;
														}
														else
														{
															mcMod4.Comp = compProject;
														}
													}
												}
												finally
												{
													List<ModMod.McMod>.Enumerator enumerator6;
													((IDisposable)enumerator6).Dispose();
												}
												if (CS$<>8__locals1.$VB$Local_ModLoaders.Count == 1)
												{
													string text2 = null;
													List<int> list2 = new List<int>();
													try
													{
														foreach (JToken jtoken4 in jtoken3["latestFilesIndexes"])
														{
															if (jtoken4["modLoader"] != null && Enumerable.Single<ModComp.CompModLoaderType>(CS$<>8__locals1.$VB$Local_ModLoaders) == (ModComp.CompModLoaderType)jtoken4["modLoader"].ToObject<int>())
															{
																string text3 = (string)jtoken4["gameVersion"];
																if (Operators.CompareString(text3, CS$<>8__locals1.$VB$Local_McVersion, false) == 0 && (text2 == null || ModMinecraft.VersionSortInteger(text2, text3) <= -1))
																{
																	if (Operators.CompareString(text2, text3, false) != 0)
																	{
																		text2 = text3;
																		list2.Clear();
																	}
																	list2.Add(jtoken4["fileId"].ToObject<int>());
																}
															}
														}
													}
													finally
													{
														IEnumerator<JToken> enumerator7;
														if (enumerator7 != null)
														{
															enumerator7.Dispose();
														}
													}
													try
													{
														foreach (int key in list2)
														{
															if (!dictionary2.ContainsKey(key))
															{
																dictionary2[key] = new List<ModMod.McMod>();
															}
															dictionary2[key].AddRange(dictionary[Conversions.ToInteger(compProject._AuthenticationRepository)]);
															dictionary3[key] = compProject.m_MerchantRepository;
														}
													}
													finally
													{
														List<int>.Enumerator enumerator8;
														((IDisposable)enumerator8).Dispose();
													}
												}
											}
										}
									}
									finally
									{
										IEnumerator<JToken> enumerator5;
										if (enumerator5 != null)
										{
											enumerator5.Dispose();
										}
									}
									ModBase.Log(string.Format("[Mod] 已从 CurseForge 获取本地 Mod 信息，需要获取 {0} 个用于检查更新的文件信息", dictionary2.Count), ModBase.LogLevel.Normal, "出现错误");
									if (Enumerable.Any<KeyValuePair<int, List<ModMod.McMod>>>(dictionary2))
									{
										JToken jtoken5 = ((JObject)ModBase.GetJson(ModDownload.DlModRequest("https://api.curseforge.com/v1/mods/files", "POST", string.Format("{{\"fileIds\": [{0}]}}", dictionary2.Keys.Join(",")), "application/json")))["data"];
										Dictionary<ModMod.McMod, ModComp.CompFile> dictionary4 = new Dictionary<ModMod.McMod, ModComp.CompFile>();
										try
										{
											foreach (JToken jtoken6 in jtoken5)
											{
												ModComp.CompFile compFile2 = new ModComp.CompFile((JObject)jtoken6, ModComp.CompType.Mod);
												if (compFile2.ExcludeTests())
												{
													try
													{
														foreach (ModMod.McMod key2 in dictionary2[Conversions.ToInteger(compFile2.m_RegRepository)])
														{
															if (!dictionary4.ContainsKey(key2) || DateTime.Compare(dictionary4[key2]._ListenerRepository, compFile2._ListenerRepository) < 0)
															{
																dictionary4[key2] = compFile2;
															}
														}
													}
													finally
													{
														List<ModMod.McMod>.Enumerator enumerator10;
														((IDisposable)enumerator10).Dispose();
													}
												}
											}
										}
										finally
										{
											IEnumerator<JToken> enumerator9;
											if (enumerator9 != null)
											{
												enumerator9.Dispose();
											}
										}
										try
										{
											foreach (KeyValuePair<ModMod.McMod, ModComp.CompFile> keyValuePair in dictionary4)
											{
												ModMod.McMod key3 = keyValuePair.Key;
												ModComp.CompFile value2 = keyValuePair.Value;
												if (ModBase._TokenRepository)
												{
													ModBase.Log(string.Format("[Mod] 本地文件 {0} 在 CurseForge 上的最新版为 {1}", key3._PropertyTest._BridgeRepository, value2._BridgeRepository), ModBase.LogLevel.Normal, "出现错误");
												}
												if (DateTime.Compare(key3._PropertyTest._ListenerRepository, value2._ListenerRepository) < 0 && Operators.CompareString(key3._PropertyTest.m_ReponseRepository, value2.m_ReponseRepository, false) != 0)
												{
													if (key3.UpdateFile != null && Operators.CompareString(value2.m_ReponseRepository, key3.UpdateFile.m_ReponseRepository, false) == 0)
													{
														key3.m_IteratorTest.Add(string.Format("https://www.curseforge.com/minecraft/mc-mods/{0}/files/{1}", dictionary3[Conversions.ToInteger(value2.m_RegRepository)], value2.m_RegRepository));
														key3.UpdateFile._ItemRepository.AddRange(value2._ItemRepository);
													}
													else if (key3.UpdateFile == null || DateTime.Compare(value2._ListenerRepository, key3.UpdateFile._ListenerRepository) > 0)
													{
														key3.m_IteratorTest = new List<string>
														{
															string.Format("https://www.curseforge.com/minecraft/mc-mods/{0}/files/{1}", dictionary3[Conversions.ToInteger(value2.m_RegRepository)], value2.m_RegRepository)
														};
														key3.UpdateFile = value2;
													}
												}
											}
										}
										finally
										{
											Dictionary<ModMod.McMod, ModComp.CompFile>.Enumerator enumerator11;
											((IDisposable)enumerator11).Dispose();
										}
										ModBase.Log("[Mod] 从 CurseForge 获取 Mod 更新信息结束", ModBase.LogLevel.Normal, "出现错误");
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "从 CurseForge 获取本地 Mod 信息失败", ModBase.LogLevel.Debug, "出现错误");
						CS$<>8__locals1.$VB$Local_IsFailed = true;
					}
					finally
					{
						CS$<>8__locals1.$VB$Local_EndedThreadCount++;
					}
				}, "Mod List Detail Loader CurseForge", ThreadPriority.Normal);
				while (CS$<>8__locals1.$VB$Local_EndedThreadCount != 2)
				{
					if (CS$<>8__locals1.$VB$Local_Loader.IsAborted)
					{
						return;
					}
					Thread.Sleep(10);
				}
				CS$<>8__locals1.$VB$Local_Mods = Enumerable.ToList<ModMod.McMod>(Enumerable.Where<ModMod.McMod>(CS$<>8__locals1.$VB$Local_Mods, (ModMod._Closure$__.$I6-5 == null) ? (ModMod._Closure$__.$I6-5 = ((ModMod.McMod m) => m.Comp != null)) : ModMod._Closure$__.$I6-5));
				ModBase.Log(string.Format("[Mod] 联网获取本地 Mod 信息完成，为 {0} 个 Mod 更新缓存", CS$<>8__locals1.$VB$Local_Mods.Count), ModBase.LogLevel.Normal, "出现错误");
				if (Enumerable.Any<ModMod.McMod>(CS$<>8__locals1.$VB$Local_Mods))
				{
					try
					{
						foreach (ModMod.McMod mcMod in CS$<>8__locals1.$VB$Local_Mods)
						{
							mcMod.m_RepositoryTest = !CS$<>8__locals1.$VB$Local_IsFailed;
							value[mcMod.ReflectMapper() + CS$<>8__locals1.$VB$Local_McVersion + CS$<>8__locals1.$VB$Local_ModLoaders.Join("")] = mcMod.ToJson();
						}
					}
					finally
					{
						List<ModMod.McMod>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					ModBase.WriteFile(ModBase.m_DecoratorRepository + "Cache\\LocalMod.json", value.ToString(ModBase._TokenRepository ? 1 : 0, new JsonConverter[0]), false, null);
					ModBase.RunInUi((ModMod._Closure$__.$I6-6 == null) ? (ModMod._Closure$__.$I6-6 = delegate()
					{
						PageVersionMod threadRepository = ModMain._ThreadRepository;
						if (threadRepository == null)
						{
							return;
						}
						threadRepository.RefreshBars();
					}) : ModMod._Closure$__.$I6-6, false);
					return;
				}
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0001DB54 File Offset: 0x0001BD54
		private static List<ModComp.CompModLoaderType> GetTargetModLoaders()
		{
			List<ModComp.CompModLoaderType> list = new List<ModComp.CompModLoaderType>();
			if (PageVersionLeft._InstanceConfig.Version.m_StructMap)
			{
				list.Add(ModComp.CompModLoaderType.Forge);
			}
			if (PageVersionLeft._InstanceConfig.Version._ValMap)
			{
				list.Add(ModComp.CompModLoaderType.NeoForge);
			}
			if (PageVersionLeft._InstanceConfig.Version._CandidateMap)
			{
				list.Add(ModComp.CompModLoaderType.Fabric);
			}
			if (PageVersionLeft._InstanceConfig.Version._AccountMap)
			{
				list.Add(ModComp.CompModLoaderType.LiteLoader);
			}
			if (!Enumerable.Any<ModComp.CompModLoaderType>(list))
			{
				list.AddRange(new ModComp.CompModLoaderType[]
				{
					ModComp.CompModLoaderType.Forge,
					ModComp.CompModLoaderType.NeoForge,
					ModComp.CompModLoaderType.Fabric,
					ModComp.CompModLoaderType.LiteLoader,
					ModComp.CompModLoaderType.Quilt
				});
			}
			return list;
		}

		// Token: 0x04000185 RID: 389
		public static ModLoader.LoaderTask<string, List<ModMod.McMod>> m_Record = new ModLoader.LoaderTask<string, List<ModMod.McMod>>("Mod List Loader", new Action<ModLoader.LoaderTask<string, List<ModMod.McMod>>>(ModMod.McModLoad), null, ThreadPriority.Normal);

		// Token: 0x04000186 RID: 390
		public static ModLoader.LoaderTask<KeyValuePair<List<ModMod.McMod>, JObject>, int> m_Service = new ModLoader.LoaderTask<KeyValuePair<List<ModMod.McMod>, JObject>, int>("Mod List Detail Loader", new Action<ModLoader.LoaderTask<KeyValuePair<List<ModMod.McMod>, JObject>, int>>(ModMod.McModDetailLoad), null, ThreadPriority.Normal);

		// Token: 0x0200006F RID: 111
		public class McMod
		{
			// Token: 0x0600029A RID: 666 RVA: 0x0001DBE8 File Offset: 0x0001BDE8
			public McMod(string Path)
			{
				this.m_SchemaRepository = null;
				this.m_DescriptorRepository = null;
				this._PublisherRepository = null;
				this._DefinitionRepository = null;
				this._StrategyRepository = new List<string>();
				this.m_ProcRepository = null;
				this._ParserTest = null;
				this.broadcasterTest = new Dictionary<string, string>();
				this.m_FieldTest = false;
				this._ReaderTest = null;
				this._ClientTest = false;
				this._ConfigTest = false;
				this._TestsTest = false;
				this.m_IteratorTest = new List<string>();
				this.m_RepositoryTest = false;
				this.Path = (Path ?? "");
			}

			// Token: 0x0600029B RID: 667 RVA: 0x000039F2 File Offset: 0x00001BF2
			public string CustomizeTests()
			{
				return ModBase.GetPathFromFullPath(this.Path) + this.ConcatTests();
			}

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x0600029C RID: 668 RVA: 0x00003A0A File Offset: 0x00001C0A
			public string FileName
			{
				get
				{
					return ModBase.GetFileNameFromPath(this.Path);
				}
			}

			// Token: 0x0600029D RID: 669 RVA: 0x00003A17 File Offset: 0x00001C17
			public string ConcatTests()
			{
				return this.FileName.Replace(".disabled", "").Replace(".old", "");
			}

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x0600029E RID: 670 RVA: 0x0001DC84 File Offset: 0x0001BE84
			public ModMod.McMod.McModState State
			{
				get
				{
					this.Load(false);
					ModMod.McMod.McModState result;
					if (!this.SortMapper())
					{
						result = ModMod.McMod.McModState.Unavailable;
					}
					else if (!this.Path.EndsWithF(".disabled", true) && !this.Path.EndsWithF(".old", true))
					{
						result = ModMod.McMod.McModState.Fine;
					}
					else
					{
						result = ModMod.McMod.McModState.Disabled;
					}
					return result;
				}
			}

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x0600029F RID: 671 RVA: 0x0001DCD4 File Offset: 0x0001BED4
			// (set) Token: 0x060002A0 RID: 672 RVA: 0x0001DD24 File Offset: 0x0001BF24
			public string Name
			{
				get
				{
					if (this.m_SchemaRepository == null)
					{
						this.Load(false);
					}
					if (this.m_SchemaRepository == null)
					{
						this.m_SchemaRepository = this._DefinitionRepository;
					}
					if (this.m_SchemaRepository == null)
					{
						this.m_SchemaRepository = ModBase.GetFileNameWithoutExtentionFromPath(this.Path);
					}
					return this.m_SchemaRepository;
				}
				set
				{
					if (this.m_SchemaRepository == null && value != null && !value.Contains("modname") && Operators.CompareString(value.ToLower(), "name", false) != 0 && Enumerable.Count<char>(value) > 1 && Operators.CompareString(ModBase.Val(value).ToString(), value, false) != 0)
					{
						this.m_SchemaRepository = value;
					}
				}
			}

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060002A1 RID: 673 RVA: 0x00003A3D File Offset: 0x00001C3D
			// (set) Token: 0x060002A2 RID: 674 RVA: 0x0001DD84 File Offset: 0x0001BF84
			public string Description
			{
				get
				{
					if (this.m_DescriptorRepository == null)
					{
						this.Load(false);
					}
					if (this.m_DescriptorRepository == null && this.SetupMapper() != null)
					{
						this.m_DescriptorRepository = this.SetupMapper().Message;
					}
					return this.m_DescriptorRepository;
				}
				set
				{
					if (this.m_DescriptorRepository == null && value != null && Enumerable.Count<char>(value) > 2)
					{
						this.m_DescriptorRepository = value.ToString().Trim(new char[]
						{
							'\n'
						});
						if (this.m_DescriptorRepository.ToLower().LastIndexOfAny(Conversions.ToCharArrayRankOne("qwertyuiopasdfghjklzxcvbnm0123456789")) == checked(Enumerable.Count<char>(this.m_DescriptorRepository) - 1))
						{
							ref string ptr = ref this.m_DescriptorRepository;
							this.m_DescriptorRepository = ptr + ".";
						}
					}
				}
			}

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060002A3 RID: 675 RVA: 0x00003A75 File Offset: 0x00001C75
			// (set) Token: 0x060002A4 RID: 676 RVA: 0x00003A8C File Offset: 0x00001C8C
			public string Version
			{
				get
				{
					if (this._PublisherRepository == null)
					{
						this.Load(false);
					}
					return this._PublisherRepository;
				}
				set
				{
					if (this._PublisherRepository == null || !ModBase.RegexCheck(this._PublisherRepository, "[0-9.\\-]+", 0))
					{
						if (value != null && value.ContainsF("version", true))
						{
							value = "version";
						}
						this._PublisherRepository = value;
					}
				}
			}

			// Token: 0x060002A5 RID: 677 RVA: 0x00003ACB File Offset: 0x00001CCB
			public string LoginMapper()
			{
				if (this._DefinitionRepository == null)
				{
					this.Load(false);
				}
				return this._DefinitionRepository;
			}

			// Token: 0x060002A6 RID: 678 RVA: 0x0001DE00 File Offset: 0x0001C000
			public void ManageMapper(string value)
			{
				if (value != null)
				{
					value = ModBase.RegexSeek(value, "[0-9a-zA-Z_-]+", 0);
					if (value != null && Enumerable.Count<char>(value) > 1 && Operators.CompareString(ModBase.Val(value).ToString(), value, false) != 0 && !value.ContainsF("name", true) && !value.ContainsF("modid", true))
					{
						if (!this._StrategyRepository.Contains(value))
						{
							this._StrategyRepository.Add(value);
						}
						if (this._DefinitionRepository == null)
						{
							this._DefinitionRepository = value;
						}
					}
				}
			}

			// Token: 0x060002A7 RID: 679 RVA: 0x00003AE2 File Offset: 0x00001CE2
			public string AwakeMapper()
			{
				if (this.m_ProcRepository == null)
				{
					this.Load(false);
				}
				return this.m_ProcRepository;
			}

			// Token: 0x060002A8 RID: 680 RVA: 0x00003AF9 File Offset: 0x00001CF9
			public void FillMapper(string value)
			{
				if (this.m_ProcRepository == null && value != null && value.StartsWithF("http", false))
				{
					this.m_ProcRepository = value;
				}
			}

			// Token: 0x060002A9 RID: 681 RVA: 0x00003B1B File Offset: 0x00001D1B
			public string NewMapper()
			{
				if (this._ParserTest == null)
				{
					this.Load(false);
				}
				return this._ParserTest;
			}

			// Token: 0x060002AA RID: 682 RVA: 0x00003B32 File Offset: 0x00001D32
			public void ComputeMapper(string value)
			{
				if (this._ParserTest == null && !string.IsNullOrWhiteSpace(value))
				{
					this._ParserTest = value;
				}
			}

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060002AB RID: 683 RVA: 0x00003B4B File Offset: 0x00001D4B
			public Dictionary<string, string> Dependencies
			{
				get
				{
					this.Load(false);
					return this.broadcasterTest;
				}
			}

			// Token: 0x060002AC RID: 684 RVA: 0x0001DE88 File Offset: 0x0001C088
			private void AddDependency(string ModID, string VersionRequirement = null)
			{
				if (ModID != null && Enumerable.Count<char>(ModID) >= 2)
				{
					ModID = ModID.ToLower();
					if (Operators.CompareString(ModID, "name", false) != 0 && Operators.CompareString(ModBase.Val(ModID).ToString(), ModID, false) != 0)
					{
						if (VersionRequirement != null && (VersionRequirement.Contains(".") || VersionRequirement.Contains("-")) && !VersionRequirement.Contains("$"))
						{
							if (!VersionRequirement.StartsWithF("[", false) && !VersionRequirement.StartsWithF("(", false) && !VersionRequirement.EndsWithF("]", false) && !VersionRequirement.EndsWithF(")", false))
							{
								VersionRequirement = "[" + VersionRequirement + ",)";
							}
						}
						else
						{
							VersionRequirement = null;
						}
						if (this.broadcasterTest.ContainsKey(ModID))
						{
							if (this.broadcasterTest[ModID] == null)
							{
								this.broadcasterTest[ModID] = VersionRequirement;
								return;
							}
						}
						else
						{
							this.broadcasterTest.Add(ModID, VersionRequirement);
						}
					}
				}
			}

			// Token: 0x060002AD RID: 685 RVA: 0x00003B5A File Offset: 0x00001D5A
			public bool SortMapper()
			{
				this.Load(false);
				return this.SetupMapper() == null;
			}

			// Token: 0x060002AE RID: 686 RVA: 0x00003B6C File Offset: 0x00001D6C
			public Exception SetupMapper()
			{
				this.Load(false);
				return this._ReaderTest;
			}

			// Token: 0x060002AF RID: 687 RVA: 0x0001DF8C File Offset: 0x0001C18C
			private void Init()
			{
				this.m_SchemaRepository = null;
				this.m_DescriptorRepository = null;
				this._PublisherRepository = null;
				this._DefinitionRepository = null;
				this._StrategyRepository = new List<string>();
				this.broadcasterTest = new Dictionary<string, string>();
				this.m_FieldTest = false;
				this._ReaderTest = null;
				this._ConfigTest = false;
				this._TestsTest = false;
			}

			// Token: 0x060002B0 RID: 688 RVA: 0x0001DFE8 File Offset: 0x0001C1E8
			public void Load(bool ForceReload = false)
			{
				if (!this.m_FieldTest || ForceReload)
				{
					this.Init();
					ZipArchive zipArchive = null;
					try
					{
						if (this.Path.Length < 2)
						{
							throw new FileNotFoundException("错误的 Mod 文件路径（" + (this.Path ?? "null") + "）");
						}
						if (!File.Exists(this.Path))
						{
							throw new FileNotFoundException("未找到 Mod 文件（" + this.Path + "）");
						}
						zipArchive = new ZipArchive(new FileStream(this.Path, FileMode.Open));
						this.LookupMetadata(zipArchive);
					}
					catch (UnauthorizedAccessException ex)
					{
						ModBase.Log(ex, "Mod 文件由于无权限无法打开（" + this.Path + "）", ModBase.LogLevel.Developer, "出现错误");
						this._ReaderTest = new UnauthorizedAccessException("没有读取此文件的权限，请尝试右键以管理员身份运行 PCL", ex);
					}
					catch (Exception ex2)
					{
						ModBase.Log(ex2, "Mod 文件无法打开（" + this.Path + "）", ModBase.LogLevel.Developer, "出现错误");
						this._ReaderTest = ex2;
					}
					finally
					{
						if (zipArchive != null)
						{
							zipArchive.Dispose();
						}
					}
					this.m_FieldTest = true;
				}
			}

			// Token: 0x060002B1 RID: 689 RVA: 0x0001E134 File Offset: 0x0001C334
			private void LookupMetadata(ZipArchive Jar)
			{
				checked
				{
					try
					{
						ZipArchiveEntry entry = Jar.GetEntry("mcmod.info");
						string text = null;
						if (entry != null)
						{
							text = ModBase.ReadFile(entry.Open(), null);
							if (text.Length < 15)
							{
								text = null;
							}
						}
						if (text != null)
						{
							object objectValue = RuntimeHelpers.GetObjectValue(ModBase.GetJson(text));
							JObject jobject;
							if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(objectValue, null, "Type", new object[0], null, null, null), 2, false))
							{
								jobject = (JObject)NewLateBinding.LateIndexGet(objectValue, new object[]
								{
									0
								}, null);
							}
							else
							{
								jobject = (JObject)NewLateBinding.LateIndexGet(NewLateBinding.LateIndexGet(objectValue, new object[]
								{
									"modList"
								}, null), new object[]
								{
									0
								}, null);
							}
							this.Name = (string)jobject["name"];
							this.Description = (string)jobject["description"];
							this.Version = (string)jobject["version"];
							this.FillMapper((string)jobject["url"]);
							this.ManageMapper((string)jobject["modid"]);
							JArray jarray = (JArray)jobject["authorList"];
							if (jarray != null)
							{
								List<string> list = new List<string>();
								try
								{
									foreach (JToken jtoken in jarray)
									{
										list.Add(jtoken.ToString());
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
								if (Enumerable.Any<string>(list))
								{
									this.ComputeMapper(list.Join(", "));
								}
							}
							JArray jarray2 = (JArray)jobject["requiredMods"];
							if (jarray2 != null)
							{
								try
								{
									foreach (JToken jtoken2 in jarray2)
									{
										string text2 = (string)jtoken2;
										if (!string.IsNullOrEmpty(text2))
										{
											text2 = text2.Substring(text2.IndexOfF(":", false) + 1);
											if (text2.Contains("@"))
											{
												this.AddDependency(text2.Split("@")[0], text2.Split("@")[1]);
											}
											else
											{
												this.AddDependency(text2, null);
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
							}
							jarray2 = (JArray)jobject["dependancies"];
							if (jarray2 != null)
							{
								try
								{
									foreach (JToken jtoken3 in jarray2)
									{
										string text3 = (string)jtoken3;
										if (!string.IsNullOrEmpty(text3))
										{
											text3 = text3.Substring(text3.IndexOfF(":", false) + 1);
											if (text3.Contains("@"))
											{
												this.AddDependency(text3.Split("@")[0], text3.Split("@")[1]);
											}
											else
											{
												this.AddDependency(text3, null);
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
							}
						}
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "读取 mcmod.info 时出现未知错误（" + this.Path + "）", ModBase.LogLevel.Developer, "出现错误");
					}
					try
					{
						ZipArchiveEntry entry2 = Jar.GetEntry("fabric.mod.json");
						string text4 = null;
						if (entry2 != null)
						{
							text4 = ModBase.ReadFile(entry2.Open(), Encoding.UTF8);
							if (!text4.Contains("schemaVersion"))
							{
								text4 = null;
							}
						}
						if (text4 != null)
						{
							JObject jobject2 = (JObject)ModBase.GetJson(text4);
							if (jobject2.ContainsKey("name"))
							{
								this.Name = (string)jobject2["name"];
							}
							if (jobject2.ContainsKey("version"))
							{
								this.Version = (string)jobject2["version"];
							}
							if (jobject2.ContainsKey("description"))
							{
								this.Description = (string)jobject2["description"];
							}
							if (jobject2.ContainsKey("id"))
							{
								this.ManageMapper((string)jobject2["id"]);
							}
							if (jobject2.ContainsKey("contact"))
							{
								this.FillMapper((string)(jobject2["contact"]["homepage"] ?? ""));
							}
							JArray jarray3 = (JArray)jobject2["authors"];
							if (jarray3 != null)
							{
								List<string> list2 = new List<string>();
								try
								{
									foreach (JToken jtoken4 in jarray3)
									{
										list2.Add(jtoken4.ToString());
									}
								}
								finally
								{
									IEnumerator<JToken> enumerator4;
									if (enumerator4 != null)
									{
										enumerator4.Dispose();
									}
								}
								if (Enumerable.Any<string>(list2))
								{
									this.ComputeMapper(list2.Join(", "));
								}
							}
							goto IL_F7F;
						}
					}
					catch (Exception ex2)
					{
						ModBase.Log(ex2, "读取 fabric.mod.json 时出现未知错误（" + this.Path + "）", ModBase.LogLevel.Developer, "出现错误");
					}
					try
					{
						ZipArchiveEntry entry3 = Jar.GetEntry("META-INF/mods.toml");
						string text5 = null;
						if (entry3 != null)
						{
							text5 = ModBase.ReadFile(entry3.Open(), null);
							if (text5.Length < 15)
							{
								text5 = null;
							}
						}
						if (text5 != null)
						{
							List<string> list3 = new List<string>();
							foreach (string text6 in text5.Replace("\r\n", "\n").Replace("\r", "\n").Split("\n"))
							{
								if (!text6.StartsWithF("#", false))
								{
									if (text6.Contains("#"))
									{
										text6 = text6.Substring(0, text6.IndexOfF("#", false));
									}
									text6 = text6.Trim(new char[]
									{
										' ',
										'\t',
										'\u3000'
									});
									if (Enumerable.Any<char>(text6))
									{
										list3.Add(text6);
									}
								}
							}
							List<KeyValuePair<string, Dictionary<string, object>>> list4 = new List<KeyValuePair<string, Dictionary<string, object>>>
							{
								new KeyValuePair<string, Dictionary<string, object>>("", new Dictionary<string, object>())
							};
							int num = list3.Count - 1;
							for (int j = 0; j <= num; j++)
							{
								string text7 = list3[j];
								if (text7.StartsWithF("[", false) && text7.EndsWithF("]", false))
								{
									string key = text7.Trim("[]".ToCharArray());
									list4.Add(new KeyValuePair<string, Dictionary<string, object>>(key, new Dictionary<string, object>()));
								}
								else
								{
									if (!text7.Contains("="))
									{
										goto IL_B3E;
									}
									string key2 = text7.Substring(0, text7.IndexOfF("=", false)).TrimEnd(new char[]
									{
										' ',
										'\t',
										'\u3000'
									});
									string text8 = text7.Substring(text7.IndexOfF("=", false) + 1).TrimStart(new char[]
									{
										' ',
										'\t',
										'\u3000'
									});
									object obj;
									if (text8.StartsWithF("\"", false) && text8.EndsWithF("\"", false))
									{
										obj = text8.Trim(new char[]
										{
											'"'
										});
									}
									else if (text8.StartsWithF("'''", false))
									{
										List<string> list5 = new List<string>
										{
											text8.TrimStart(new char[]
											{
												'\''
											})
										};
										if (list5[0].EndsWithF("'''", false))
										{
											list5[0] = list5[0].TrimEnd(new char[]
											{
												'\''
											});
										}
										else
										{
											while (j < list3.Count - 1)
											{
												j++;
												string text9 = list3[j];
												if (text9.EndsWithF("'''", false))
												{
													list5.Add(text9.TrimEnd(new char[]
													{
														'\''
													}));
													break;
												}
												list5.Add(text9);
											}
										}
										obj = list5.Join("\n").Trim(new char[]
										{
											'\n'
										}).Replace("\n", "\r\n");
									}
									else if (Operators.CompareString(text8.ToLower(), "true", false) != 0 && Operators.CompareString(text8.ToLower(), "false", false) != 0)
									{
										if (Operators.CompareString(ModBase.Val(text8).ToString(), text8, false) == 0)
										{
											obj = ModBase.Val(text8);
										}
										else
										{
											obj = text8;
										}
									}
									else
									{
										obj = (Operators.CompareString(text8.ToLower(), "true", false) == 0);
									}
									Enumerable.Last<KeyValuePair<string, Dictionary<string, object>>>(list4).Value[key2] = RuntimeHelpers.GetObjectValue(obj);
								}
							}
							Dictionary<string, object> dictionary = null;
							try
							{
								foreach (KeyValuePair<string, Dictionary<string, object>> keyValuePair in list4)
								{
									if (Operators.CompareString(keyValuePair.Key, "mods", false) == 0)
									{
										dictionary = keyValuePair.Value;
										break;
									}
								}
							}
							finally
							{
								List<KeyValuePair<string, Dictionary<string, object>>>.Enumerator enumerator5;
								((IDisposable)enumerator5).Dispose();
							}
							if (dictionary != null && dictionary.ContainsKey("modId"))
							{
								this.ManageMapper(Conversions.ToString(dictionary["modId"]));
								if (this._DefinitionRepository != null)
								{
									if (dictionary.ContainsKey("displayName"))
									{
										this.Name = Conversions.ToString(dictionary["displayName"]);
									}
									if (dictionary.ContainsKey("description"))
									{
										this.Description = Conversions.ToString(dictionary["description"]);
									}
									if (dictionary.ContainsKey("version"))
									{
										this.Version = Conversions.ToString(dictionary["version"]);
									}
									if (list4[0].Value.ContainsKey("displayURL"))
									{
										this.FillMapper(Conversions.ToString(list4[0].Value["displayURL"]));
									}
									if (list4[0].Value.ContainsKey("authors"))
									{
										this.ComputeMapper(Conversions.ToString(list4[0].Value["authors"]));
									}
									try
									{
										foreach (KeyValuePair<string, Dictionary<string, object>> keyValuePair2 in list4)
										{
											if (Operators.CompareString(keyValuePair2.Key.ToLower(), string.Format("dependencies.{0}", this.LoginMapper().ToLower()), false) == 0)
											{
												Dictionary<string, object> value = keyValuePair2.Value;
												if (Conversions.ToBoolean(value.ContainsKey("modId") && value.ContainsKey("mandatory") && Conversions.ToBoolean(value["mandatory"]) && value.ContainsKey("side") && Operators.CompareString(value["side"].ToString().ToLower(), "server", false) != 0))
												{
													this.AddDependency(Conversions.ToString(value["modId"]), Conversions.ToString(value.ContainsKey("versionRange") ? value["versionRange"] : null));
												}
											}
										}
									}
									finally
									{
										List<KeyValuePair<string, Dictionary<string, object>>>.Enumerator enumerator6;
										((IDisposable)enumerator6).Dispose();
									}
									goto IL_F7F;
								}
							}
						}
					}
					catch (Exception ex3)
					{
						ModBase.Log(ex3, "读取 mods.toml 时出现未知错误（" + this.Path + "）", ModBase.LogLevel.Developer, "出现错误");
					}
					IL_B3E:
					try
					{
						ZipArchiveEntry entry4 = Jar.GetEntry("META-INF/fml_cache_annotation.json");
						string text10 = null;
						if (entry4 != null)
						{
							text10 = ModBase.ReadFile(entry4.Open(), Encoding.UTF8);
							if (!text10.Contains("Lnet/minecraftforge/fml/common/Mod;"))
							{
								text10 = null;
							}
						}
						if (text10 != null)
						{
							JObject jobject3 = (JObject)ModBase.GetJson(text10);
							JObject jobject4 = null;
							try
							{
								foreach (KeyValuePair<string, JToken> keyValuePair3 in jobject3)
								{
									JArray jarray4 = (JArray)keyValuePair3.Value["annotations"];
									if (jarray4 != null)
									{
										try
										{
											foreach (JToken jtoken5 in jarray4)
											{
												if (Operators.CompareString((string)(jtoken5["name"] ?? ""), "Lnet/minecraftforge/fml/common/Mod;", false) == 0)
												{
													jobject4 = (JObject)jtoken5["values"];
													goto IL_C4F;
												}
											}
										}
										finally
										{
											IEnumerator<JToken> enumerator8;
											if (enumerator8 != null)
											{
												enumerator8.Dispose();
											}
										}
									}
								}
							}
							finally
							{
								IEnumerator<KeyValuePair<string, JToken>> enumerator7;
								if (enumerator7 != null)
								{
									enumerator7.Dispose();
								}
							}
							goto IL_F7F;
							IL_C4F:
							if (jobject4.ContainsKey("useMetadata") && Operators.CompareString((jobject4["useMetadata"]["value"] ?? "").ToString().ToLower(), "true", false) == 0)
							{
								string text11 = (string)jobject4["modid"]["value"];
								if (text11 != null)
								{
									text11 = ModBase.RegexSeek(text11.ToLower(), "[0-9a-z_]+", 0);
									if (text11 != null && Operators.CompareString(text11.ToLower(), "name", false) != 0 && Enumerable.Count<char>(text11) > 1 && Operators.CompareString(ModBase.Val(text11).ToString(), text11, false) != 0 && !this._StrategyRepository.Contains(text11))
									{
										this._StrategyRepository.Add(text11);
									}
								}
							}
							else
							{
								if (jobject4.ContainsKey("name"))
								{
									this.Name = (string)jobject4["name"]["value"];
								}
								if (jobject4.ContainsKey("version"))
								{
									this.Version = (string)jobject4["version"]["value"];
								}
								if (jobject4.ContainsKey("modid"))
								{
									this.ManageMapper((string)jobject4["modid"]["value"]);
								}
								if (!jobject4.ContainsKey("serverSideOnly") || !jobject4["serverSideOnly"]["value"].ToObject<bool>())
								{
									JToken jtoken6;
									if (jobject4["acceptedMinecraftVersions"] == null)
									{
										if ((jtoken6 = "") != null)
										{
											goto IL_E32;
										}
									}
									else if ((jtoken6 = jobject4["acceptedMinecraftVersions"]["value"]) != null)
									{
										goto IL_E32;
									}
									jtoken6 = "";
									IL_E32:
									string text12 = (string)jtoken6;
									if (Operators.CompareString(text12, "", false) != 0)
									{
										this.AddDependency("minecraft", text12);
									}
									JToken jtoken7;
									if (jobject4["dependencies"] == null)
									{
										if ((jtoken7 = "") != null)
										{
											goto IL_E97;
										}
									}
									else if ((jtoken7 = jobject4["dependencies"]["value"]) != null)
									{
										goto IL_E97;
									}
									jtoken7 = "";
									IL_E97:
									string text13 = (string)jtoken7;
									if (Operators.CompareString(text13, "", false) != 0)
									{
										foreach (string text14 in text13.Split(";"))
										{
											if (Operators.CompareString(text14, "", false) != 0 && text14.StartsWithF("required-", false))
											{
												text14 = text14.Substring(text14.IndexOfF(":", false) + 1);
												if (text14.Contains("@"))
												{
													this.AddDependency(text14.Split("@")[0], text14.Split("@")[1]);
												}
												else
												{
													this.AddDependency(text14, null);
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
						ModBase.Log(ex4, "读取 fml_cache_annotation.json 时出现未知错误（" + this.Path + "）", ModBase.LogLevel.Developer, "出现错误");
					}
					IL_F7F:
					if (Operators.CompareString(this._PublisherRepository, "version", false) == 0)
					{
						try
						{
							ZipArchiveEntry entry5 = Jar.GetEntry("META-INF/MANIFEST.MF");
							if (entry5 != null)
							{
								string text15 = ModBase.ReadFile(entry5.Open(), null).Replace(" :", ":").Replace(": ", ":");
								if (text15.Contains("Implementation-Version:"))
								{
									text15 = text15.Substring(text15.IndexOfF("Implementation-Version:", false) + Enumerable.Count<char>("Implementation-Version:"));
									text15 = text15.Substring(0, text15.IndexOfAny("\r\n".ToCharArray())).Trim();
									this.Version = text15;
								}
							}
						}
						catch (Exception ex5)
						{
							ModBase.Log("获取 META-INF 中的版本信息失败（" + this.Path + "）", ModBase.LogLevel.Developer, "出现错误");
							this.Version = null;
						}
					}
					if (this._PublisherRepository != null && !this._PublisherRepository.Contains(".") && !this._PublisherRepository.Contains("-"))
					{
						this.Version = null;
					}
				}
			}

			// Token: 0x060002B2 RID: 690 RVA: 0x0001F314 File Offset: 0x0001D514
			[CompilerGenerated]
			public void ViewMapper(ModMod.McMod.OnCompUpdateEventHandler obj)
			{
				ModMod.McMod.OnCompUpdateEventHandler onCompUpdateEventHandler = this.mapperTest;
				ModMod.McMod.OnCompUpdateEventHandler onCompUpdateEventHandler2;
				do
				{
					onCompUpdateEventHandler2 = onCompUpdateEventHandler;
					ModMod.McMod.OnCompUpdateEventHandler value = (ModMod.McMod.OnCompUpdateEventHandler)Delegate.Combine(onCompUpdateEventHandler2, obj);
					onCompUpdateEventHandler = Interlocked.CompareExchange<ModMod.McMod.OnCompUpdateEventHandler>(ref this.mapperTest, value, onCompUpdateEventHandler2);
				}
				while (onCompUpdateEventHandler != onCompUpdateEventHandler2);
			}

			// Token: 0x060002B3 RID: 691 RVA: 0x0001F34C File Offset: 0x0001D54C
			[CompilerGenerated]
			public void PushMapper(ModMod.McMod.OnCompUpdateEventHandler obj)
			{
				ModMod.McMod.OnCompUpdateEventHandler onCompUpdateEventHandler = this.mapperTest;
				ModMod.McMod.OnCompUpdateEventHandler onCompUpdateEventHandler2;
				do
				{
					onCompUpdateEventHandler2 = onCompUpdateEventHandler;
					ModMod.McMod.OnCompUpdateEventHandler value = (ModMod.McMod.OnCompUpdateEventHandler)Delegate.Remove(onCompUpdateEventHandler2, obj);
					onCompUpdateEventHandler = Interlocked.CompareExchange<ModMod.McMod.OnCompUpdateEventHandler>(ref this.mapperTest, value, onCompUpdateEventHandler2);
				}
				while (onCompUpdateEventHandler != onCompUpdateEventHandler2);
			}

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060002B4 RID: 692 RVA: 0x00003B7B File Offset: 0x00001D7B
			// (set) Token: 0x060002B5 RID: 693 RVA: 0x0001F384 File Offset: 0x0001D584
			public ModComp.CompProject Comp
			{
				get
				{
					return this._ThreadTest;
				}
				set
				{
					this._ThreadTest = value;
					ModMod.McMod.OnCompUpdateEventHandler onCompUpdateEventHandler = this.mapperTest;
					if (onCompUpdateEventHandler != null)
					{
						onCompUpdateEventHandler(this);
					}
				}
			}

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x060002B6 RID: 694 RVA: 0x00003B83 File Offset: 0x00001D83
			// (set) Token: 0x060002B7 RID: 695 RVA: 0x0001F3AC File Offset: 0x0001D5AC
			public ModComp.CompFile UpdateFile
			{
				get
				{
					return this.m_ComposerTest;
				}
				set
				{
					this.m_ComposerTest = value;
					ModMod.McMod.OnCompUpdateEventHandler onCompUpdateEventHandler = this.mapperTest;
					if (onCompUpdateEventHandler != null)
					{
						onCompUpdateEventHandler(this);
					}
				}
			}

			// Token: 0x060002B8 RID: 696 RVA: 0x0001F3D4 File Offset: 0x0001D5D4
			public JObject ToJson()
			{
				JObject jobject = new JObject();
				if (this.Comp != null)
				{
					jobject.Add("Comp", this.Comp.ToJson());
				}
				jobject.Add("ChangelogUrls", new JArray(this.m_IteratorTest));
				jobject.Add("CompLoaded", this.m_RepositoryTest);
				if (this._PropertyTest != null)
				{
					jobject.Add("CompFile", this._PropertyTest.ToJson());
				}
				if (this.UpdateFile != null)
				{
					jobject.Add("UpdateFile", this.UpdateFile.ToJson());
				}
				return jobject;
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x0001F470 File Offset: 0x0001D670
			public void FromJson(JObject Json)
			{
				this.m_RepositoryTest = (bool)Json["CompLoaded"];
				if (Json.ContainsKey("Comp"))
				{
					this.Comp = new ModComp.CompProject((JObject)Json["Comp"]);
				}
				if (Json.ContainsKey("ChangelogUrls"))
				{
					this.m_IteratorTest = Json["ChangelogUrls"].ToObject<List<string>>();
				}
				if (Json.ContainsKey("CompFile"))
				{
					this._PropertyTest = new ModComp.CompFile((JObject)Json["CompFile"], ModComp.CompType.Mod);
				}
				if (Json.ContainsKey("UpdateFile"))
				{
					this.UpdateFile = new ModComp.CompFile((JObject)Json["UpdateFile"], ModComp.CompType.Mod);
				}
			}

			// Token: 0x060002BA RID: 698 RVA: 0x00003B8B File Offset: 0x00001D8B
			public bool SelectMapper()
			{
				return Conversions.ToBoolean(Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenFunctionModUpdate", null))) && Enumerable.Any<string>(this.m_IteratorTest));
			}

			// Token: 0x060002BB RID: 699 RVA: 0x0001F530 File Offset: 0x0001D730
			public uint LogoutMapper()
			{
				checked
				{
					if (this.m_TestTest == null)
					{
						FileInfo fileInfo = new FileInfo(this.Path);
						string key = Conversions.ToString(ModBase.GetHash(string.Format("{0}-{1}-{2}-C", this.CustomizeTests(), fileInfo.LastWriteTime.ToLongTimeString(), fileInfo.Length)));
						string text = ModBase.ReadIni(ModBase.m_DecoratorRepository + "Cache\\ModHash.ini", key, "");
						if (Operators.CompareString(text, "", false) != 0 && ModBase.RegexCheck(text, "^\\d+$", 0))
						{
							this.m_TestTest = new uint?(Conversions.ToUInteger(text));
							return this.m_TestTest.Value;
						}
						List<byte> list = new List<byte>();
						foreach (byte b in ModBase.ReadFileBytes(this.Path, null))
						{
							if (b != 9 && b != 10 && b != 13 && b != 32)
							{
								list.Add(b);
							}
						}
						int count = list.Count;
						uint num = (uint)(1 ^ count);
						int num2 = count - 4;
						int j;
						for (j = 0; j <= num2; j += 4)
						{
							uint num3 = (uint)((int)list[j] | (int)list[j + 1] << 8 | (int)list[j + 2] << 16 | (int)list[j + 3] << 24);
							num3 = (uint)(unchecked((ulong)num3) * 1540483477UL & 4294967295UL);
							num3 ^= num3 >> 24;
							num3 = (uint)(unchecked((ulong)num3) * 1540483477UL & 4294967295UL);
							num = (uint)(unchecked((ulong)num) * 1540483477UL & 4294967295UL);
							num ^= num3;
						}
						switch (count - j)
						{
						case 1:
							num ^= (uint)list[j];
							num = (uint)(unchecked((ulong)num) * 1540483477UL & 4294967295UL);
							break;
						case 2:
							num ^= (uint)((int)list[j] | (int)list[j + 1] << 8);
							num = (uint)(unchecked((ulong)num) * 1540483477UL & 4294967295UL);
							break;
						case 3:
							num ^= (uint)((int)list[j] | (int)list[j + 1] << 8);
							num ^= (uint)((uint)list[j + 2] << 16);
							num = (uint)(unchecked((ulong)num) * 1540483477UL & 4294967295UL);
							break;
						}
						num ^= num >> 13;
						num = (uint)(unchecked((ulong)num) * 1540483477UL & 4294967295UL);
						num ^= num >> 15;
						this.m_TestTest = new uint?(num);
						ModBase.WriteIni(ModBase.m_DecoratorRepository + "Cache\\ModHash.ini", key, num.ToString());
					}
					return this.m_TestTest.Value;
				}
			}

			// Token: 0x060002BC RID: 700 RVA: 0x0001F830 File Offset: 0x0001DA30
			public string ReflectMapper()
			{
				if (this.m_MapTest == null)
				{
					FileInfo fileInfo = new FileInfo(this.Path);
					string key = Conversions.ToString(ModBase.GetHash(string.Format("{0}-{1}-{2}-M", this.CustomizeTests(), fileInfo.LastWriteTime.ToLongTimeString(), fileInfo.Length)));
					string text = ModBase.ReadIni(ModBase.m_DecoratorRepository + "Cache\\ModHash.ini", key, "");
					if (Operators.CompareString(text, "", false) != 0)
					{
						this.m_MapTest = text;
						return this.m_MapTest;
					}
					this.m_MapTest = ModBase.smethod_1(this.Path);
					ModBase.WriteIni(ModBase.m_DecoratorRepository + "Cache\\ModHash.ini", key, this.m_MapTest);
				}
				return this.m_MapTest;
			}

			// Token: 0x060002BD RID: 701 RVA: 0x00003BC1 File Offset: 0x00001DC1
			public override string ToString()
			{
				return string.Format("{0} - {1}", this.State, this.Path);
			}

			// Token: 0x060002BE RID: 702 RVA: 0x0001F8F8 File Offset: 0x0001DAF8
			public override bool Equals(object obj)
			{
				ModMod.McMod mcMod = obj as ModMod.McMod;
				return mcMod != null && Operators.CompareString(this.Path, mcMod.Path, false) == 0;
			}

			// Token: 0x060002BF RID: 703 RVA: 0x0001F928 File Offset: 0x0001DB28
			public bool IsPresetMod()
			{
				return !Enumerable.Any<KeyValuePair<string, string>>(this.Dependencies) && this.Name != null && (this.Name.ToLower().Contains("core") || this.Name.ToLower().Contains("lib"));
			}

			// Token: 0x060002C0 RID: 704 RVA: 0x0001F97C File Offset: 0x0001DB7C
			public static object IsModFile(string Path)
			{
				object result;
				if (Path != null && Path.Contains("."))
				{
					Path = Path.ToLower();
					if (!Path.EndsWithF(".jar", true) && !Path.EndsWithF(".zip", true) && !Path.EndsWithF(".litemod", true) && !Path.EndsWithF(".jar.disabled", true) && !Path.EndsWithF(".zip.disabled", true) && !Path.EndsWithF(".litemod.disabled", true) && !Path.EndsWithF(".jar.old", true) && !Path.EndsWithF(".zip.old", true) && !Path.EndsWithF(".litemod.old", true))
					{
						result = false;
					}
					else
					{
						result = true;
					}
				}
				else
				{
					result = false;
				}
				return result;
			}

			// Token: 0x04000187 RID: 391
			public readonly string Path;

			// Token: 0x04000188 RID: 392
			private string m_SchemaRepository;

			// Token: 0x04000189 RID: 393
			private string m_DescriptorRepository;

			// Token: 0x0400018A RID: 394
			public string _PublisherRepository;

			// Token: 0x0400018B RID: 395
			private string _DefinitionRepository;

			// Token: 0x0400018C RID: 396
			public List<string> _StrategyRepository;

			// Token: 0x0400018D RID: 397
			private string m_ProcRepository;

			// Token: 0x0400018E RID: 398
			private string _ParserTest;

			// Token: 0x0400018F RID: 399
			private Dictionary<string, string> broadcasterTest;

			// Token: 0x04000190 RID: 400
			private bool m_FieldTest;

			// Token: 0x04000191 RID: 401
			private Exception _ReaderTest;

			// Token: 0x04000192 RID: 402
			private bool _ClientTest;

			// Token: 0x04000193 RID: 403
			private bool _ConfigTest;

			// Token: 0x04000194 RID: 404
			private bool _TestsTest;

			// Token: 0x04000195 RID: 405
			[CompilerGenerated]
			private ModMod.McMod.OnCompUpdateEventHandler mapperTest;

			// Token: 0x04000196 RID: 406
			private ModComp.CompProject _ThreadTest;

			// Token: 0x04000197 RID: 407
			public ModComp.CompFile _PropertyTest;

			// Token: 0x04000198 RID: 408
			private ModComp.CompFile m_ComposerTest;

			// Token: 0x04000199 RID: 409
			public List<string> m_IteratorTest;

			// Token: 0x0400019A RID: 410
			public bool m_RepositoryTest;

			// Token: 0x0400019B RID: 411
			private uint? m_TestTest;

			// Token: 0x0400019C RID: 412
			private string m_MapTest;

			// Token: 0x02000070 RID: 112
			public enum McModState
			{
				// Token: 0x0400019E RID: 414
				Fine,
				// Token: 0x0400019F RID: 415
				Disabled,
				// Token: 0x040001A0 RID: 416
				Unavailable
			}

			// Token: 0x02000071 RID: 113
			// (Invoke) Token: 0x060002C5 RID: 709
			public delegate void OnCompUpdateEventHandler(ModMod.McMod sender);
		}
	}
}
