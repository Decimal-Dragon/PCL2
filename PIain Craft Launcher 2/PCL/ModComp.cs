using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace PCL
{
	// Token: 0x02000054 RID: 84
	[StandardModule]
	public sealed class ModComp
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x00015074 File Offset: 0x00013274
		private static List<ModComp.CompDatabaseEntry> RestartBroadcaster()
		{
			checked
			{
				List<ModComp.CompDatabaseEntry> predicate;
				if (ModComp.m_Predicate != null)
				{
					predicate = ModComp.m_Predicate;
				}
				else
				{
					ModComp.m_Predicate = new List<ModComp.CompDatabaseEntry>();
					int num = 0;
					foreach (string text in ModBase.DecodeBytes(ModBase.GetResources("ModData")).Replace("\r\n", "\n").Replace("\r", "").Split("\n"))
					{
						num++;
						if (Operators.CompareString(text, "", false) != 0)
						{
							foreach (string fullStr in text.Split("¨"))
							{
								ModComp.CompDatabaseEntry compDatabaseEntry = new ModComp.CompDatabaseEntry();
								string[] array3 = fullStr.Split("|");
								if (array3[0].StartsWithF("@", false))
								{
									compDatabaseEntry.adapterRepository = null;
									compDatabaseEntry.m_FacadeRepository = array3[0].Replace("@", "");
								}
								else if (array3[0].EndsWithF("@", false))
								{
									compDatabaseEntry.adapterRepository = array3[0].TrimEnd(new char[]
									{
										'@'
									});
									compDatabaseEntry.m_FacadeRepository = compDatabaseEntry.adapterRepository;
								}
								else if (array3[0].Contains("@"))
								{
									compDatabaseEntry.adapterRepository = array3[0].Split("@")[0];
									compDatabaseEntry.m_FacadeRepository = array3[0].Split("@")[1];
								}
								else
								{
									compDatabaseEntry.adapterRepository = array3[0];
									compDatabaseEntry.m_FacadeRepository = null;
								}
								compDatabaseEntry.annotationRepository = num;
								if (Enumerable.Count<string>(array3) >= 2)
								{
									compDatabaseEntry.infoRepository = array3[1];
									if (compDatabaseEntry.infoRepository.Contains("*"))
									{
										compDatabaseEntry.infoRepository = compDatabaseEntry.infoRepository.Replace("*", " (" + string.Join(" ", Enumerable.Select<string, string>((compDatabaseEntry.adapterRepository ?? compDatabaseEntry.m_FacadeRepository).Split("-"), (ModComp._Closure$__.$I6-0 == null) ? (ModComp._Closure$__.$I6-0 = ((string w) => w.Substring(0, 1).ToUpper() + w.Substring(1, w.Length - 1))) : ModComp._Closure$__.$I6-0)) + ")");
									}
								}
								ModComp.m_Predicate.Add(compDatabaseEntry);
							}
						}
					}
					predicate = ModComp.m_Predicate;
				}
				return predicate;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000152C8 File Offset: 0x000134C8
		public static void CompProjectsGet(ModLoader.LoaderTask<ModComp.CompProjectRequest, int> Task)
		{
			ModComp._Closure$__13-1 CS$<>8__locals1 = new ModComp._Closure$__13-1(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_Task = Task;
			CS$<>8__locals1.$VB$Local_Storage = CS$<>8__locals1.$VB$Local_Task.Input.m_ParamsRepository;
			if (CS$<>8__locals1.$VB$Local_Task.Input.m_ParamsRepository._CreatorRepository.Count >= CS$<>8__locals1.$VB$Local_Task.Input.m_DispatcherRepository)
			{
				ModBase.Log(string.Format("[Comp] 已有 {0} 个结果，多于所需的 {1} 个结果，结束处理", CS$<>8__locals1.$VB$Local_Task.Input.m_ParamsRepository._CreatorRepository.Count, CS$<>8__locals1.$VB$Local_Task.Input.m_DispatcherRepository), ModBase.LogLevel.Normal, "出现错误");
				return;
			}
			if (!CS$<>8__locals1.$VB$Local_Task.Input.EnableTests())
			{
				if (!Enumerable.Any<ModComp.CompProject>(CS$<>8__locals1.$VB$Local_Task.Input.m_ParamsRepository._CreatorRepository))
				{
					throw new Exception("没有符合条件的结果");
				}
				ModBase.Log(string.Format("[Comp] 已有 {0} 个结果，少于所需的 {1} 个结果，但无法继续获取，结束处理", CS$<>8__locals1.$VB$Local_Task.Input.m_ParamsRepository._CreatorRepository.Count, CS$<>8__locals1.$VB$Local_Task.Input.m_DispatcherRepository), ModBase.LogLevel.Normal, "出现错误");
				return;
			}
			else
			{
				if (CS$<>8__locals1.$VB$Local_Task.Input.processRepository == ModComp.CompModLoaderType.Quilt && ModMinecraft.VersionSortInteger(CS$<>8__locals1.$VB$Local_Task.Input.m_ParameterRepository ?? "1.15", "1.14") == -1)
				{
					throw new Exception("Quilt 不支持 Minecraft " + CS$<>8__locals1.$VB$Local_Task.Input.m_ParameterRepository);
				}
				string text = (CS$<>8__locals1.$VB$Local_Task.Input._RecordRepository ?? "").Trim();
				CS$<>8__locals1.$VB$Local_Task.Input._RecordRepository = text;
				text = text.ToLower();
				ModBase.Log("[Comp] 工程列表搜索原始文本：" + text, ModBase.LogLevel.Normal, "出现错误");
				bool flag;
				Dictionary<ModComp.CompProject, double> dictionary;
				List<ModBase.SearchEntry<ModComp.CompProject>> list4;
				checked
				{
					if ((flag = (ModBase.RegexCheck(text, "[\\u4e00-\\u9fbb]", 0) && !string.IsNullOrEmpty(text))) && CS$<>8__locals1.$VB$Local_Task.Input.Type == ModComp.CompType.Mod)
					{
						List<ModBase.SearchEntry<ModComp.CompDatabaseEntry>> list = new List<ModBase.SearchEntry<ModComp.CompDatabaseEntry>>();
						try
						{
							foreach (ModComp.CompDatabaseEntry compDatabaseEntry in ModComp.RestartBroadcaster())
							{
								if (!compDatabaseEntry.infoRepository.Contains("动态的树"))
								{
									list.Add(new ModBase.SearchEntry<ModComp.CompDatabaseEntry>
									{
										m_DicError = compDatabaseEntry,
										helperError = new List<KeyValuePair<string, double>>
										{
											new KeyValuePair<string, double>(compDatabaseEntry.infoRepository + compDatabaseEntry.adapterRepository + compDatabaseEntry.m_FacadeRepository, 1.0)
										}
									});
								}
							}
						}
						finally
						{
							List<ModComp.CompDatabaseEntry>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
						List<ModBase.SearchEntry<ModComp.CompDatabaseEntry>> list2 = ModBase.Search<ModComp.CompDatabaseEntry>(list, CS$<>8__locals1.$VB$Local_Task.Input._RecordRepository, 3, 0.1);
						if (!Enumerable.Any<ModBase.SearchEntry<ModComp.CompDatabaseEntry>>(list2))
						{
							throw new Exception("无搜索结果，请尝试搜索英文名称");
						}
						string text2 = "";
						int num = Math.Min(4, list2.Count - 1);
						int num2 = 0;
						while (num2 <= num && (list2[num2].indexerError || num2 < Math.Min(2, list2.Count - 1)))
						{
							if (list2[num2].m_DicError.adapterRepository != null)
							{
								text2 = text2 + list2[num2].m_DicError.adapterRepository.Replace("-", " ").Replace("/", " ") + " ";
							}
							if (list2[num2].m_DicError.m_FacadeRepository != null)
							{
								text2 = text2 + list2[num2].m_DicError.m_FacadeRepository.Replace("-", " ").Replace("/", " ") + " ";
							}
							text2 = text2 + list2[num2].m_DicError.infoRepository.AfterLast(" (", false).TrimEnd(new char[]
							{
								')'
							}).BeforeFirst(" - ", false).Replace(":", "").Replace("(", "").Replace(")", "").ToLower().Replace("/", " ") + " ";
							num2++;
						}
						ModBase.Log("[Comp] 中文搜索原始关键词：" + text2, ModBase.LogLevel.Developer, "出现错误");
						string text3 = "";
						foreach (string text4 in text2.Split(" "))
						{
							if (!Enumerable.Contains<string>(new string[]
							{
								"the",
								"of",
								"a",
								"mod",
								"and"
							}, text4.ToLowerInvariant()) && ModBase.Val(text4) <= 0.0 && (Enumerable.Count<string>(text2.Split(" ")) <= 3 || !Enumerable.Contains<string>(new string[]
							{
								"ftb"
							}, text4.ToLower())))
							{
								text3 = text3 + text4.TrimStart(new char[]
								{
									'{'
								}).TrimEnd(new char[]
								{
									'}'
								}) + " ";
							}
						}
						CS$<>8__locals1.$VB$Local_Task.Input._RecordRepository = text3;
						ModBase.Log("[Comp] 中文搜索最终关键词：" + text3, ModBase.LogLevel.Developer, "出现错误");
					}
					string str = ModBase.RegexReplace(CS$<>8__locals1.$VB$Local_Task.Input._RecordRepository, "$& ", "([A-Z]+|[a-z]+?)(?=[A-Z]+[a-z]+[a-z ]*)", 0);
					string str2 = CS$<>8__locals1.$VB$Local_Task.Input._RecordRepository.Replace(" ", "");
					string fullStr = (str + " " + (flag ? CS$<>8__locals1.$VB$Local_Task.Input._RecordRepository : (str2 + " " + text))).ToLower();
					List<string> list3 = new List<string>();
					foreach (string text5 in fullStr.Split(" "))
					{
						text5 = text5.Trim(new char[]
						{
							'[',
							']'
						});
						if (Operators.CompareString(text5, "", false) != 0)
						{
							if (Enumerable.Contains<string>(new string[]
							{
								"forge",
								"fabric",
								"for",
								"mod",
								"quilt"
							}, text5))
							{
								ModBase.Log("[Comp] 已跳过搜索关键词：" + text5, ModBase.LogLevel.Developer, "出现错误");
							}
							else
							{
								list3.Add(text5);
							}
						}
					}
					if (text.Length > 0 && !Enumerable.Any<string>(list3))
					{
						CS$<>8__locals1.$VB$Local_Task.Input._RecordRepository = text;
					}
					else
					{
						CS$<>8__locals1.$VB$Local_Task.Input._RecordRepository = Enumerable.ToList<string>(Enumerable.Distinct<string>(list3)).Join(" ").ToLower();
					}
					if (text.Replace(" ", "").ContainsF("optiforge", true))
					{
						CS$<>8__locals1.$VB$Local_Task.Input._RecordRepository = "optiforge";
					}
					if (text.Replace(" ", "").ContainsF("optifabric", true))
					{
						CS$<>8__locals1.$VB$Local_Task.Input._RecordRepository = "optifabric";
					}
					ModBase.Log("[Comp] 工程列表搜索最终文本：" + CS$<>8__locals1.$VB$Local_Task.Input._RecordRepository, ModBase.LogLevel.Debug, "出现错误");
					CS$<>8__locals1.$VB$Local_Task.Progress = 0.1;
					CS$<>8__locals1.$VB$Local_RealResults = new List<ModComp.CompProject>();
					for (;;)
					{
						CS$<>8__locals1.$VB$Local_RawResults = new List<ModComp.CompProject>();
						CS$<>8__locals1.$VB$Local_Error = null;
						Thread thread = null;
						Thread thread2 = null;
						CS$<>8__locals1.$VB$Local_ResultsLock = RuntimeHelpers.GetObjectValue(new object());
						try
						{
							ModComp._Closure$__13-0 CS$<>8__locals2 = new ModComp._Closure$__13-0(CS$<>8__locals2);
							CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2 = CS$<>8__locals1;
							CS$<>8__locals2.$VB$Local_CurseForgeUrl = CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Task.Input.GetCurseForgeAddress();
							CS$<>8__locals2.$VB$Local_CurseForgeFailed = false;
							if (CS$<>8__locals2.$VB$Local_CurseForgeUrl != null)
							{
								thread = ModBase.RunInNewThread(delegate
								{
									try
									{
										ModBase.Log("[Comp] 开始从 CurseForge 获取工程列表：" + CS$<>8__locals2.$VB$Local_CurseForgeUrl, ModBase.LogLevel.Normal, "出现错误");
										JObject jobject = (JObject)ModDownload.DlModRequest(CS$<>8__locals2.$VB$Local_CurseForgeUrl, true);
										ModLoader.LoaderTask<ModComp.CompProjectRequest, int> $VB$Local_Task;
										($VB$Local_Task = CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Task).Progress = unchecked($VB$Local_Task.Progress + 0.2);
										List<ModComp.CompProject> list6 = new List<ModComp.CompProject>();
										try
										{
											foreach (JToken jtoken in jobject["data"])
											{
												JObject data = (JObject)jtoken;
												list6.Add(new ModComp.CompProject(data));
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
										object $VB$Local_ResultsLock = CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_ResultsLock;
										ObjectFlowControl.CheckForSyncLockOnValueType($VB$Local_ResultsLock);
										lock ($VB$Local_ResultsLock)
										{
											CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_RawResults.AddRange(list6);
										}
										ModComp.CompProjectStorage $VB$Local_Storage = CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Storage;
										ref int ptr = ref $VB$Local_Storage.m_ServiceRepository;
										$VB$Local_Storage.m_ServiceRepository = ptr + list6.Count;
										CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Storage.m_InvocationRepository = jobject["pagination"]["totalCount"].ToObject<int>();
										ModBase.Log(string.Format("[Comp] 从 CurseForge 获取到了 {0} 个工程（已获取 {1} 个，共 {2} 个）", list6.Count, CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Storage.m_ServiceRepository, CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Storage.m_InvocationRepository), ModBase.LogLevel.Normal, "出现错误");
									}
									catch (Exception ex)
									{
										ModBase.Log(ex, "从 CurseForge 获取工程列表失败", ModBase.LogLevel.Debug, "出现错误");
										CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Storage.m_InvocationRepository = -1;
										CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Error = ex;
										CS$<>8__locals2.$VB$Local_CurseForgeFailed = true;
									}
								}, "CurseForge Project Request", ThreadPriority.Normal);
							}
							CS$<>8__locals2.$VB$Local_ModrinthUrl = CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Task.Input.GetModrinthAddress();
							CS$<>8__locals2.$VB$Local_ModrinthFailed = false;
							if (CS$<>8__locals2.$VB$Local_ModrinthUrl != null)
							{
								thread2 = ModBase.RunInNewThread(delegate
								{
									try
									{
										ModBase.Log("[Comp] 开始从 Modrinth 获取工程列表：" + CS$<>8__locals2.$VB$Local_ModrinthUrl, ModBase.LogLevel.Normal, "出现错误");
										JObject jobject = (JObject)ModDownload.DlModRequest(CS$<>8__locals2.$VB$Local_ModrinthUrl, true);
										ModLoader.LoaderTask<ModComp.CompProjectRequest, int> $VB$Local_Task;
										($VB$Local_Task = CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Task).Progress = unchecked($VB$Local_Task.Progress + 0.2);
										List<ModComp.CompProject> list6 = new List<ModComp.CompProject>();
										try
										{
											foreach (JToken jtoken in jobject["hits"])
											{
												JObject data = (JObject)jtoken;
												list6.Add(new ModComp.CompProject(data));
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
										object $VB$Local_ResultsLock = CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_ResultsLock;
										ObjectFlowControl.CheckForSyncLockOnValueType($VB$Local_ResultsLock);
										lock ($VB$Local_ResultsLock)
										{
											try
											{
												foreach (ModComp.CompProject compProject3 in list6)
												{
													if (CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Task.Input.Type != ModComp.CompType.Mod || Enumerable.Any<ModComp.CompModLoaderType>(compProject3.predicateRepository))
													{
														CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_RawResults.Add(compProject3);
													}
												}
											}
											finally
											{
												List<ModComp.CompProject>.Enumerator enumerator6;
												((IDisposable)enumerator6).Dispose();
											}
										}
										ModComp.CompProjectStorage $VB$Local_Storage = CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Storage;
										ref int ptr = ref $VB$Local_Storage.m_ProxyRepository;
										$VB$Local_Storage.m_ProxyRepository = ptr + list6.Count;
										CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Storage.m_MessageRepository = jobject["total_hits"].ToObject<int>();
										ModBase.Log(string.Format("[Comp] 从 Modrinth 获取到了 {0} 个工程（已获取 {1} 个，共 {2} 个）", list6.Count, CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Storage.m_ProxyRepository, CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Storage.m_MessageRepository), ModBase.LogLevel.Normal, "出现错误");
									}
									catch (Exception ex)
									{
										ModBase.Log(ex, "从 Modrinth 获取工程列表失败", ModBase.LogLevel.Debug, "出现错误");
										CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Storage.m_MessageRepository = -1;
										CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Error = ex;
										CS$<>8__locals2.$VB$Local_ModrinthFailed = true;
									}
								}, "Modrinth Project Request", ThreadPriority.Normal);
							}
							if (thread != null)
							{
								thread.Join();
							}
							if (CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Task.IsAborted)
							{
								return;
							}
							if (thread2 != null)
							{
								thread2.Join();
							}
							if (CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Task.IsAborted)
							{
								return;
							}
							CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Storage._InitializerRepository = null;
							if (!Enumerable.Any<ModComp.CompProject>(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_RawResults))
							{
								if (CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Error != null)
								{
									throw CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Error;
								}
								if (flag && CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Task.Input.Type != ModComp.CompType.Mod)
								{
									throw new Exception(string.Format("{0}搜索仅支持英文", (CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Task.Input.Type == ModComp.CompType.ModPack) ? "整合包" : "资源包"));
								}
								if (CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Task.Input.Source == ModComp.CompSourceType.CurseForge && CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Task.Input.Tag.StartsWithF("/", false))
								{
									throw new Exception("CurseForge 不兼容所选的类型");
								}
								if (CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Task.Input.Source == ModComp.CompSourceType.Modrinth && CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Task.Input.Tag.EndsWithF("/", false))
								{
									throw new Exception("Modrinth 不兼容所选的类型");
								}
								throw new Exception("没有搜索结果");
							}
							else if (CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Error != null)
							{
								if (CS$<>8__locals2.$VB$Local_CurseForgeFailed)
								{
									CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Storage._InitializerRepository = string.Format("无法连接到 CurseForge，所以目前仅显示了来自 Modrinth 的内容，结果可能不全。{0}请尝试使用 VPN 或加速器以改善网络。", "\r\n");
								}
								else
								{
									CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Storage._InitializerRepository = string.Format("无法连接到 Modrinth，所以目前仅显示了来自 CurseForge 的内容，结果可能不全。{0}请尝试使用 VPN 或加速器以改善网络。", "\r\n");
								}
							}
						}
						finally
						{
							if (thread != null)
							{
								thread.Interrupt();
							}
							if (thread2 != null)
							{
								thread2.Interrupt();
							}
						}
						CS$<>8__locals1.$VB$Local_RawResults = Enumerable.ToList<ModComp.CompProject>(Enumerable.Concat<ModComp.CompProject>(Enumerable.Where<ModComp.CompProject>(CS$<>8__locals1.$VB$Local_RawResults, (ModComp._Closure$__.$I13-2 == null) ? (ModComp._Closure$__.$I13-2 = ((ModComp.CompProject x) => !x.listRepository)) : ModComp._Closure$__.$I13-2), Enumerable.Where<ModComp.CompProject>(CS$<>8__locals1.$VB$Local_RawResults, (ModComp._Closure$__.$I13-3 == null) ? (ModComp._Closure$__.$I13-3 = ((ModComp.CompProject x) => x.listRepository)) : ModComp._Closure$__.$I13-3)));
						CS$<>8__locals1.$VB$Local_RawResults = CS$<>8__locals1.$VB$Local_RawResults.Distinct((ModComp._Closure$__.$I13-4 == null) ? (ModComp._Closure$__.$I13-4 = ((ModComp.CompProject a, ModComp.CompProject b) => a.IsLike(b))) : ModComp._Closure$__.$I13-4);
						CS$<>8__locals1.$VB$Local_RawResults = Enumerable.ToList<ModComp.CompProject>(Enumerable.Where<ModComp.CompProject>(CS$<>8__locals1.$VB$Local_RawResults, delegate(ModComp.CompProject r)
						{
							ModComp._Closure$__13-2 CS$<>8__locals3 = new ModComp._Closure$__13-2(CS$<>8__locals3);
							CS$<>8__locals3.$VB$Local_r = r;
							return !Enumerable.Any<ModComp.CompProject>(CS$<>8__locals1.$VB$Local_RealResults, (ModComp.CompProject b) => CS$<>8__locals3.$VB$Local_r.IsLike(b)) && !Enumerable.Any<ModComp.CompProject>(CS$<>8__locals1.$VB$Local_Storage._CreatorRepository, (ModComp.CompProject b) => CS$<>8__locals3.$VB$Local_r.IsLike(b));
						}));
						CS$<>8__locals1.$VB$Local_RealResults.AddRange(CS$<>8__locals1.$VB$Local_RawResults);
						ModBase.Log(string.Format("[Comp] 去重、筛选后累计新增结果 {0} 个", CS$<>8__locals1.$VB$Local_RealResults.Count), ModBase.LogLevel.Normal, "出现错误");
						if (CS$<>8__locals1.$VB$Local_RealResults.Count + CS$<>8__locals1.$VB$Local_Storage._CreatorRepository.Count >= CS$<>8__locals1.$VB$Local_Task.Input.m_DispatcherRepository)
						{
							goto IL_BE6;
						}
						ModBase.Log(string.Format("[Comp] 总结果数需求最少 {0} 个，仅获得了 {1} 个", CS$<>8__locals1.$VB$Local_Task.Input.m_DispatcherRepository, CS$<>8__locals1.$VB$Local_RealResults.Count + CS$<>8__locals1.$VB$Local_Storage._CreatorRepository.Count), ModBase.LogLevel.Normal, "出现错误");
						if (!CS$<>8__locals1.$VB$Local_Task.Input.EnableTests())
						{
							break;
						}
						ModBase.Log("[Comp] 将继续尝试加载下一页", ModBase.LogLevel.Normal, "出现错误");
					}
					ModBase.Log("[Comp] 无法继续加载，将强制结束", ModBase.LogLevel.Normal, "出现错误");
					IL_BE6:
					dictionary = new Dictionary<ModComp.CompProject, double>();
					if (string.IsNullOrEmpty(CS$<>8__locals1.$VB$Local_Task.Input._RecordRepository))
					{
						try
						{
							foreach (ModComp.CompProject compProject in CS$<>8__locals1.$VB$Local_RealResults)
							{
								dictionary.Add(compProject, (double)(compProject.databaseRepository * (compProject.listRepository ? 1 : 10)));
							}
							goto IL_DC0;
						}
						finally
						{
							List<ModComp.CompProject>.Enumerator enumerator2;
							((IDisposable)enumerator2).Dispose();
						}
					}
					list4 = new List<ModBase.SearchEntry<ModComp.CompProject>>();
				}
				try
				{
					foreach (ModComp.CompProject compProject2 in CS$<>8__locals1.$VB$Local_RealResults)
					{
						dictionary.Add(compProject2, ((compProject2.FlushTests() > 0) ? 0.2 : 0.0) + Math.Log10((double)(checked(Math.Max(compProject2.databaseRepository, 1) * (compProject2.listRepository ? 1 : 10)))) / 9.0);
						list4.Add(new ModBase.SearchEntry<ModComp.CompProject>
						{
							m_DicError = compProject2,
							helperError = new List<KeyValuePair<string, double>>
							{
								new KeyValuePair<string, double>(flag ? compProject2.RemoveTests() : compProject2.comparatorRepository, 1.0),
								new KeyValuePair<string, double>(compProject2.mappingRepository, 0.05)
							}
						});
					}
				}
				finally
				{
					List<ModComp.CompProject>.Enumerator enumerator3;
					((IDisposable)enumerator3).Dispose();
				}
				List<ModBase.SearchEntry<ModComp.CompProject>> list5 = ModBase.Search<ModComp.CompProject>(list4, text, 101, -1.0);
				try
				{
					foreach (ModBase.SearchEntry<ModComp.CompProject> searchEntry in list5)
					{
						Dictionary<ModComp.CompProject, double> dictionary2;
						ModComp.CompProject dicError;
						(dictionary2 = dictionary)[dicError = searchEntry.m_DicError] = dictionary2[dicError] + searchEntry.m_IssuerError / list5[0].m_IssuerError;
					}
				}
				finally
				{
					List<ModBase.SearchEntry<ModComp.CompProject>>.Enumerator enumerator4;
					((IDisposable)enumerator4).Dispose();
				}
				IL_DC0:
				CS$<>8__locals1.$VB$Local_Storage._CreatorRepository.AddRange(Enumerable.ToList<ModComp.CompProject>(Enumerable.Select<KeyValuePair<ModComp.CompProject, double>, ModComp.CompProject>(Enumerable.ToList<KeyValuePair<ModComp.CompProject, double>>(dictionary).Sort((ModComp._Closure$__.$I13-8 == null) ? (ModComp._Closure$__.$I13-8 = ((KeyValuePair<ModComp.CompProject, double> a, KeyValuePair<ModComp.CompProject, double> b) => a.Value > b.Value)) : ModComp._Closure$__.$I13-8), (ModComp._Closure$__.$I13-9 == null) ? (ModComp._Closure$__.$I13-9 = ((KeyValuePair<ModComp.CompProject, double> r) => r.Key)) : ModComp._Closure$__.$I13-9)));
				return;
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00016180 File Offset: 0x00014380
		public static List<ModComp.CompFile> CompFilesGet(string ProjectId, bool FromCurseForge)
		{
			ModComp._Closure$__17-0 CS$<>8__locals1 = new ModComp._Closure$__17-0(CS$<>8__locals1);
			if (ModComp.pool.ContainsKey(ProjectId))
			{
				CS$<>8__locals1.$VB$Local_TargetProject = ModComp.pool[ProjectId];
			}
			else if (FromCurseForge)
			{
				CS$<>8__locals1.$VB$Local_TargetProject = new ModComp.CompProject((JObject)NewLateBinding.LateIndexGet(ModDownload.DlModRequest("https://api.curseforge.com/v1/mods/" + ProjectId, true), new object[]
				{
					"data"
				}, null));
			}
			else
			{
				CS$<>8__locals1.$VB$Local_TargetProject = new ModComp.CompProject((JObject)ModDownload.DlModRequest("https://api.modrinth.com/v2/project/" + ProjectId, true));
			}
			if (!ModComp.customer.ContainsKey(ProjectId))
			{
				ModBase.Log("[Comp] 开始获取文件列表：" + ProjectId, ModBase.LogLevel.Normal, "出现错误");
				JArray jarray;
				if (FromCurseForge)
				{
					if (CS$<>8__locals1.$VB$Local_TargetProject.Type == ModComp.CompType.Mod)
					{
						jarray = (JArray)NewLateBinding.LateIndexGet(ModBase.GetJson(ModDownload.DlModRequest("https://api.curseforge.com/v1/mods/files", "POST", "{\"fileIds\": [" + CS$<>8__locals1.$VB$Local_TargetProject.m_AlgoRepository.Join(",") + "]}", "application/json")), new object[]
						{
							"data"
						}, null);
					}
					else
					{
						jarray = (JArray)NewLateBinding.LateIndexGet(ModDownload.DlModRequest(string.Format("https://api.curseforge.com/v1/mods/{0}/files?pageSize=999", ProjectId), true), new object[]
						{
							"data"
						}, null);
					}
				}
				else
				{
					jarray = (JArray)ModDownload.DlModRequest(string.Format("https://api.modrinth.com/v2/project/{0}/version", ProjectId), true);
				}
				ModComp.customer[ProjectId] = Enumerable.ToList<ModComp.CompFile>(Enumerable.Where<ModComp.CompFile>(Enumerable.Select<JToken, ModComp.CompFile>(jarray, (JToken a) => new ModComp.CompFile((JObject)a, CS$<>8__locals1.$VB$Local_TargetProject.Type)), (ModComp._Closure$__.$I17-1 == null) ? (ModComp._Closure$__.$I17-1 = ((ModComp.CompFile a) => a.ExcludeTests())) : ModComp._Closure$__.$I17-1)).Distinct((ModComp._Closure$__.$I17-2 == null) ? (ModComp._Closure$__.$I17-2 = ((ModComp.CompFile a, ModComp.CompFile b) => Operators.CompareString(a.m_RegRepository, b.m_RegRepository, false) == 0)) : ModComp._Closure$__.$I17-2);
			}
			List<ModComp.CompFile> result;
			if (CS$<>8__locals1.$VB$Local_TargetProject.Type != ModComp.CompType.Mod)
			{
				result = ModComp.customer[ProjectId];
			}
			else
			{
				List<string> list = Enumerable.ToList<string>(Enumerable.Distinct<string>(Enumerable.SelectMany<ModComp.CompFile, string>(ModComp.customer[ProjectId], (ModComp._Closure$__.$I17-3 == null) ? (ModComp._Closure$__.$I17-3 = ((ModComp.CompFile f) => f.m_GlobalRepository)) : ModComp._Closure$__.$I17-3)));
				List<string> list2 = Enumerable.ToList<string>(Enumerable.Where<string>(list, (ModComp._Closure$__.$I17-4 == null) ? (ModComp._Closure$__.$I17-4 = ((string f) => !ModComp.pool.ContainsKey(f))) : ModComp._Closure$__.$I17-4));
				if (Enumerable.Any<string>(list2))
				{
					ModBase.Log(string.Format("[Comp] {0} 文件列表中还需要获取信息的前置 Mod：{1}", ProjectId, list2.Join("，")), ModBase.LogLevel.Normal, "出现错误");
					JArray jarray2;
					if (CS$<>8__locals1.$VB$Local_TargetProject.listRepository)
					{
						jarray2 = (JArray)NewLateBinding.LateIndexGet(ModBase.GetJson(ModDownload.DlModRequest("https://api.curseforge.com/v1/mods", "POST", "{\"modIds\": [" + list2.Join(",") + "]}", "application/json")), new object[]
						{
							"data"
						}, null);
					}
					else
					{
						jarray2 = (JArray)ModDownload.DlModRequest(string.Format("https://api.modrinth.com/v2/projects?ids=[\"{0}\"]", list2.Join("\",\"")), true);
					}
					try
					{
						foreach (JToken jtoken in jarray2)
						{
							new ModComp.CompProject((JObject)jtoken);
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
				if (Enumerable.Any<string>(list))
				{
					try
					{
						foreach (ModComp.CompProject compProject in Enumerable.Select<string, ModComp.CompProject>(Enumerable.Where<string>(list, (ModComp._Closure$__.$I17-5 == null) ? (ModComp._Closure$__.$I17-5 = ((string id) => ModComp.pool.ContainsKey(id))) : ModComp._Closure$__.$I17-5), (ModComp._Closure$__.$I17-6 == null) ? (ModComp._Closure$__.$I17-6 = ((string id) => ModComp.pool[id])) : ModComp._Closure$__.$I17-6))
						{
							try
							{
								foreach (ModComp.CompFile compFile in ModComp.customer[ProjectId])
								{
									if (compFile.m_GlobalRepository.Contains(compProject._AuthenticationRepository) && Operators.CompareString(compProject._AuthenticationRepository, ProjectId, false) != 0)
									{
										compFile.exceptionRepository.Add(compProject._AuthenticationRepository);
									}
								}
							}
							finally
							{
								List<ModComp.CompFile>.Enumerator enumerator3;
								((IDisposable)enumerator3).Dispose();
							}
						}
					}
					finally
					{
						IEnumerator<ModComp.CompProject> enumerator2;
						if (enumerator2 != null)
						{
							enumerator2.Dispose();
						}
					}
				}
				result = ModComp.customer[ProjectId];
			}
			return result;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000165F8 File Offset: 0x000147F8
		public static void CompFilesCardPreload(StackPanel Stack, List<ModComp.CompFile> Files)
		{
			List<string> list = Enumerable.ToList<string>(Enumerable.Distinct<string>(Enumerable.SelectMany<ModComp.CompFile, string>(Files, (ModComp._Closure$__.$I18-0 == null) ? (ModComp._Closure$__.$I18-0 = ((ModComp.CompFile f) => f.exceptionRepository)) : ModComp._Closure$__.$I18-0)));
			list.Sort();
			if (Enumerable.Any<string>(list))
			{
				list = Enumerable.ToList<string>(Enumerable.Where<string>(list, (ModComp._Closure$__.$I18-1 == null) ? (ModComp._Closure$__.$I18-1 = delegate(string dep)
				{
					if (!ModComp.pool.ContainsKey(dep))
					{
						ModBase.Log(string.Format("[Comp] 未找到 ID {0} 的前置 Mod 信息", dep), ModBase.LogLevel.Debug, "出现错误");
					}
					return ModComp.pool.ContainsKey(dep);
				}) : ModComp._Closure$__.$I18-1));
				Stack.Children.Add(new TextBlock
				{
					Text = "前置 Mod",
					FontSize = 14.0,
					HorizontalAlignment = HorizontalAlignment.Left,
					Margin = new Thickness(6.0, 2.0, 0.0, 5.0)
				});
				try
				{
					foreach (string key in list)
					{
						MyCompItem element = ModComp.pool[key].ToCompItem(false, false);
						Stack.Children.Add(element);
					}
				}
				finally
				{
					List<string>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				Stack.Children.Add(new TextBlock
				{
					Text = "可选版本",
					FontSize = 14.0,
					HorizontalAlignment = HorizontalAlignment.Left,
					Margin = new Thickness(6.0, 12.0, 0.0, 5.0)
				});
			}
		}

		// Token: 0x040000D1 RID: 209
		private static List<ModComp.CompDatabaseEntry> m_Predicate = null;

		// Token: 0x040000D2 RID: 210
		public static Dictionary<string, ModComp.CompProject> pool = new Dictionary<string, ModComp.CompProject>();

		// Token: 0x040000D3 RID: 211
		public static Dictionary<string, List<ModComp.CompFile>> customer = new Dictionary<string, List<ModComp.CompFile>>();

		// Token: 0x02000055 RID: 85
		public enum CompType
		{
			// Token: 0x040000D5 RID: 213
			Mod,
			// Token: 0x040000D6 RID: 214
			ModPack,
			// Token: 0x040000D7 RID: 215
			ResourcePack
		}

		// Token: 0x02000056 RID: 86
		public enum CompModLoaderType
		{
			// Token: 0x040000D9 RID: 217
			Any,
			// Token: 0x040000DA RID: 218
			Forge,
			// Token: 0x040000DB RID: 219
			LiteLoader = 3,
			// Token: 0x040000DC RID: 220
			Fabric,
			// Token: 0x040000DD RID: 221
			Quilt,
			// Token: 0x040000DE RID: 222
			NeoForge
		}

		// Token: 0x02000057 RID: 87
		[Flags]
		public enum CompSourceType
		{
			// Token: 0x040000E0 RID: 224
			CurseForge = 1,
			// Token: 0x040000E1 RID: 225
			Modrinth = 2,
			// Token: 0x040000E2 RID: 226
			Any = 3
		}

		// Token: 0x02000058 RID: 88
		private class CompDatabaseEntry
		{
			// Token: 0x060001EA RID: 490 RVA: 0x00003261 File Offset: 0x00001461
			public CompDatabaseEntry()
			{
				this.infoRepository = "";
				this.adapterRepository = null;
				this.m_FacadeRepository = null;
			}

			// Token: 0x060001EB RID: 491 RVA: 0x00016798 File Offset: 0x00014998
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					this.adapterRepository,
					"&",
					this.m_FacadeRepository,
					"|",
					Conversions.ToString(this.annotationRepository),
					"|",
					this.infoRepository
				});
			}

			// Token: 0x040000E3 RID: 227
			public int annotationRepository;

			// Token: 0x040000E4 RID: 228
			public string infoRepository;

			// Token: 0x040000E5 RID: 229
			public string adapterRepository;

			// Token: 0x040000E6 RID: 230
			public string m_FacadeRepository;
		}

		// Token: 0x02000059 RID: 89
		public class CompProject
		{
			// Token: 0x060001ED RID: 493 RVA: 0x00003283 File Offset: 0x00001483
			private ModComp.CompDatabaseEntry AddTests()
			{
				if (!this.m_InterceptorRepository)
				{
					this.m_InterceptorRepository = true;
					if (this.Type == ModComp.CompType.Mod)
					{
						this._ContainerRepository = Enumerable.FirstOrDefault<ModComp.CompDatabaseEntry>(ModComp.RestartBroadcaster(), (ModComp.CompDatabaseEntry c) => Operators.CompareString(this.listRepository ? c.adapterRepository : c.m_FacadeRepository, this.m_MerchantRepository, false) == 0);
					}
				}
				return this._ContainerRepository;
			}

			// Token: 0x060001EE RID: 494 RVA: 0x000032BE File Offset: 0x000014BE
			private void InstantiateTests(ModComp.CompDatabaseEntry value)
			{
				this.m_InterceptorRepository = true;
				this._ContainerRepository = value;
			}

			// Token: 0x060001EF RID: 495 RVA: 0x000032CE File Offset: 0x000014CE
			public int FlushTests()
			{
				if (this.AddTests() != null)
				{
					return this.AddTests().annotationRepository;
				}
				return 0;
			}

			// Token: 0x060001F0 RID: 496 RVA: 0x000032E5 File Offset: 0x000014E5
			public string RemoveTests()
			{
				if (this.AddTests() != null && Operators.CompareString(this.AddTests().infoRepository, "", false) != 0)
				{
					return this.AddTests().infoRepository;
				}
				return this.comparatorRepository;
			}

			// Token: 0x060001F1 RID: 497 RVA: 0x000167F4 File Offset: 0x000149F4
			public CompProject(JObject Data)
			{
				this._FilterRepository = null;
				this._CustomerRepository = null;
				this.m_InterceptorRepository = false;
				this._ContainerRepository = null;
				checked
				{
					if (Data.ContainsKey("Tags"))
					{
						this.listRepository = ((string)Data["DataSource"] == "CurseForge");
						this.Type = (ModComp.CompType)Data["Type"].ToObject<int>();
						this.m_MerchantRepository = (string)Data["Slug"];
						this._AuthenticationRepository = (string)Data["Id"];
						if (Data.ContainsKey("CurseForgeFileIds"))
						{
							this.m_AlgoRepository = Enumerable.ToList<int>(Enumerable.Select<JToken, int>((JArray)Data["CurseForgeFileIds"], (ModComp.CompProject._Closure$__.$I23-0 == null) ? (ModComp.CompProject._Closure$__.$I23-0 = ((JToken t) => t.ToObject<int>())) : ModComp.CompProject._Closure$__.$I23-0));
						}
						this.comparatorRepository = (string)Data["RawName"];
						this.mappingRepository = (string)Data["Description"];
						this.m_TokenizerRepository = (string)Data["Website"];
						if (Data.ContainsKey("LastUpdate"))
						{
							this._FilterRepository = (DateTime?)Data["LastUpdate"];
						}
						this.databaseRepository = (int)Data["DownloadCount"];
						if (Data.ContainsKey("ModLoaders"))
						{
							this.predicateRepository = Enumerable.ToList<ModComp.CompModLoaderType>(Enumerable.Select<JToken, ModComp.CompModLoaderType>((JArray)Data["ModLoaders"], (ModComp.CompProject._Closure$__.$I23-1 == null) ? (ModComp.CompProject._Closure$__.$I23-1 = ((JToken t) => (ModComp.CompModLoaderType)t.ToObject<int>())) : ModComp.CompProject._Closure$__.$I23-1));
						}
						else
						{
							this.predicateRepository = new List<ModComp.CompModLoaderType>();
						}
						this.m_PoolRepository = Enumerable.ToList<string>(Enumerable.Select<JToken, string>((JArray)Data["Tags"], (ModComp.CompProject._Closure$__.$I23-2 == null) ? (ModComp.CompProject._Closure$__.$I23-2 = ((JToken t) => t.ToString())) : ModComp.CompProject._Closure$__.$I23-2));
						if (Data.ContainsKey("LogoUrl"))
						{
							this._CustomerRepository = (string)Data["LogoUrl"];
						}
						if (Data.ContainsKey("GameVersions"))
						{
							this.pageRepository = Enumerable.ToList<int>(Enumerable.Select<JToken, int>((JArray)Data["GameVersions"], (ModComp.CompProject._Closure$__.$I23-3 == null) ? (ModComp.CompProject._Closure$__.$I23-3 = ((JToken t) => t.ToObject<int>())) : ModComp.CompProject._Closure$__.$I23-3));
						}
						else
						{
							this.pageRepository = new List<int>();
						}
					}
					else
					{
						this.listRepository = Data.ContainsKey("summary");
						if (this.listRepository)
						{
							this._AuthenticationRepository = (string)Data["id"];
							this.m_MerchantRepository = (string)Data["slug"];
							this.comparatorRepository = (string)Data["name"];
							this.mappingRepository = (string)Data["summary"];
							this.m_TokenizerRepository = Data["links"]["websiteUrl"].ToString().TrimEnd(new char[]
							{
								'/'
							});
							this._FilterRepository = (DateTime?)Data["dateReleased"];
							this.databaseRepository = (int)Data["downloadCount"];
							if (Enumerable.Count<JToken>(Data["logo"]) > 0)
							{
								if (Data["logo"]["thumbnailUrl"] != null && !((string)Data["logo"]["thumbnailUrl"] == ""))
								{
									this._CustomerRepository = (string)Data["logo"]["thumbnailUrl"];
								}
								else
								{
									this._CustomerRepository = (string)Data["logo"]["url"];
								}
							}
							this.predicateRepository = new List<ModComp.CompModLoaderType>();
							List<KeyValuePair<int, List<string>>> list = new List<KeyValuePair<int, List<string>>>();
							try
							{
								foreach (JToken jtoken in (Data["latestFiles"] ?? new JArray()))
								{
									ModComp.CompFile compFile = new ModComp.CompFile((JObject)jtoken, this.Type);
									if (compFile.ExcludeTests())
									{
										this.predicateRepository.AddRange(compFile._VisitorRepository);
										List<string> list2 = jtoken["gameVersions"].ToObject<List<string>>();
										if (Enumerable.Any<string>(list2, (ModComp.CompProject._Closure$__.$I23-4 == null) ? (ModComp.CompProject._Closure$__.$I23-4 = ((string v) => v.StartsWithF("1.", false))) : ModComp.CompProject._Closure$__.$I23-4))
										{
											list.Add(new KeyValuePair<int, List<string>>((int)jtoken["id"], list2));
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
							try
							{
								foreach (JToken jtoken2 in (Data["latestFilesIndexes"] ?? new JArray()))
								{
									if (jtoken2["gameVersion"].ToString().StartsWithF("1.", false))
									{
										list.Add(new KeyValuePair<int, List<string>>((int)jtoken2["fileId"], Enumerable.ToList<string>(new string[]
										{
											jtoken2["gameVersion"].ToString()
										})));
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
							this.m_AlgoRepository = Enumerable.ToList<int>(Enumerable.Distinct<int>(Enumerable.Select<KeyValuePair<int, List<string>>, int>(list, (ModComp.CompProject._Closure$__.$I23-5 == null) ? (ModComp.CompProject._Closure$__.$I23-5 = ((KeyValuePair<int, List<string>> f) => f.Key)) : ModComp.CompProject._Closure$__.$I23-5)));
							this.pageRepository = Enumerable.ToList<int>(Enumerable.OrderByDescending<int, int>(Enumerable.Distinct<int>(Enumerable.Where<int>(Enumerable.Select<string, int>(Enumerable.Where<string>(Enumerable.SelectMany<KeyValuePair<int, List<string>>, string>(list, (ModComp.CompProject._Closure$__.$I23-6 == null) ? (ModComp.CompProject._Closure$__.$I23-6 = ((KeyValuePair<int, List<string>> f) => f.Value)) : ModComp.CompProject._Closure$__.$I23-6), (ModComp.CompProject._Closure$__.$I23-7 == null) ? (ModComp.CompProject._Closure$__.$I23-7 = ((string v) => v.StartsWithF("1.", false))) : ModComp.CompProject._Closure$__.$I23-7), (ModComp.CompProject._Closure$__.$I23-8 == null) ? (ModComp.CompProject._Closure$__.$I23-8 = ((string v) => (int)Math.Round(ModBase.Val(v.Split(".")[1].BeforeFirst("-", false))))) : ModComp.CompProject._Closure$__.$I23-8), (ModComp.CompProject._Closure$__.$I23-9 == null) ? (ModComp.CompProject._Closure$__.$I23-9 = ((int v) => v > 0)) : ModComp.CompProject._Closure$__.$I23-9)), (ModComp.CompProject._Closure$__.$I23-10 == null) ? (ModComp.CompProject._Closure$__.$I23-10 = ((int v) => v)) : ModComp.CompProject._Closure$__.$I23-10));
							this.predicateRepository = Enumerable.ToList<ModComp.CompModLoaderType>(Enumerable.OrderBy<ModComp.CompModLoaderType, int>(Enumerable.Distinct<ModComp.CompModLoaderType>(this.predicateRepository), (ModComp.CompProject._Closure$__.$I23-11 == null) ? (ModComp.CompProject._Closure$__.$I23-11 = ((ModComp.CompModLoaderType t) => (int)t)) : ModComp.CompProject._Closure$__.$I23-11));
							if (!this.m_TokenizerRepository.Contains("/mc-mods/") && !this.m_TokenizerRepository.Contains("/mod/"))
							{
								if (this.m_TokenizerRepository.Contains("/modpacks/"))
								{
									this.Type = ModComp.CompType.ModPack;
								}
								else
								{
									this.Type = ModComp.CompType.ResourcePack;
								}
							}
							else
							{
								this.Type = ModComp.CompType.Mod;
							}
							this.m_PoolRepository = new List<string>();
							try
							{
								foreach (int num in Enumerable.OrderByDescending<int, int>(Enumerable.Distinct<int>(Enumerable.Select<JToken, int>(Data["categories"] ?? new JArray(), (ModComp.CompProject._Closure$__.$I23-12 == null) ? (ModComp.CompProject._Closure$__.$I23-12 = ((JToken t) => (int)t["id"])) : ModComp.CompProject._Closure$__.$I23-12)), (ModComp.CompProject._Closure$__.$I23-13 == null) ? (ModComp.CompProject._Closure$__.$I23-13 = ((int c) => c)) : ModComp.CompProject._Closure$__.$I23-13))
								{
									if (num <= 4558)
									{
										switch (num)
										{
										case 406:
											this.m_PoolRepository.Add("世界元素");
											break;
										case 407:
											this.m_PoolRepository.Add("生物群系");
											break;
										case 408:
											this.m_PoolRepository.Add("矿物/资源");
											break;
										case 409:
											this.m_PoolRepository.Add("天然结构");
											break;
										case 410:
											this.m_PoolRepository.Add("维度");
											break;
										case 411:
											this.m_PoolRepository.Add("生物");
											break;
										case 412:
											this.m_PoolRepository.Add("科技");
											break;
										case 413:
										case 418:
										case 425:
										case 426:
										case 427:
										case 428:
										case 429:
										case 430:
										case 431:
										case 432:
										case 433:
											break;
										case 414:
											this.m_PoolRepository.Add("运输");
											break;
										case 415:
											this.m_PoolRepository.Add("管道/物流");
											break;
										case 416:
											this.m_PoolRepository.Add("农业");
											break;
										case 417:
											this.m_PoolRepository.Add("能源");
											break;
										case 419:
											this.m_PoolRepository.Add("魔法");
											break;
										case 420:
											this.m_PoolRepository.Add("仓储");
											break;
										case 421:
											this.m_PoolRepository.Add("支持库");
											break;
										case 422:
											this.m_PoolRepository.Add("冒险");
											break;
										case 423:
											this.m_PoolRepository.Add("信息显示");
											break;
										case 424:
											this.m_PoolRepository.Add("装饰");
											break;
										case 434:
											this.m_PoolRepository.Add("装备");
											break;
										case 435:
											this.m_PoolRepository.Add("服务器");
											break;
										case 436:
											this.m_PoolRepository.Add("食物/烹饪");
											break;
										default:
											switch (num)
											{
											case 4471:
												this.m_PoolRepository.Add("科幻");
												break;
											case 4472:
												this.m_PoolRepository.Add("科技");
												break;
											case 4473:
												this.m_PoolRepository.Add("魔法");
												break;
											case 4474:
											case 4485:
											case 4486:
												break;
											case 4475:
												this.m_PoolRepository.Add("冒险");
												break;
											case 4476:
												this.m_PoolRepository.Add("探索");
												break;
											case 4477:
												this.m_PoolRepository.Add("小游戏");
												break;
											case 4478:
												this.m_PoolRepository.Add("任务");
												break;
											case 4479:
												this.m_PoolRepository.Add("硬核");
												break;
											case 4480:
												this.m_PoolRepository.Add("基于地图");
												break;
											case 4481:
												this.m_PoolRepository.Add("轻量");
												break;
											case 4482:
												this.m_PoolRepository.Add("大型");
												break;
											case 4483:
												this.m_PoolRepository.Add("战斗");
												break;
											case 4484:
												this.m_PoolRepository.Add("多人");
												break;
											case 4487:
												this.m_PoolRepository.Add("FTB");
												break;
											default:
												if (num == 4558)
												{
													this.m_PoolRepository.Add("红石");
												}
												break;
											}
											break;
										}
									}
									else if (num <= 4843)
									{
										if (num != 4736)
										{
											if (num == 4843)
											{
												this.m_PoolRepository.Add("自动化");
											}
										}
										else
										{
											this.m_PoolRepository.Add("空岛");
										}
									}
									else if (num != 5128)
									{
										if (num == 5191)
										{
											this.m_PoolRepository.Add("改良");
										}
									}
									else
									{
										this.m_PoolRepository.Add("原版改良");
									}
								}
							}
							finally
							{
								IEnumerator<int> enumerator3;
								if (enumerator3 != null)
								{
									enumerator3.Dispose();
								}
							}
							if (!Enumerable.Any<string>(this.m_PoolRepository))
							{
								this.m_PoolRepository.Add("杂项");
							}
						}
						else
						{
							this._AuthenticationRepository = (string)(Data["project_id"] ?? Data["id"]);
							this.m_MerchantRepository = (string)Data["slug"];
							this.comparatorRepository = (string)Data["title"];
							this.mappingRepository = (string)Data["description"];
							this._FilterRepository = (DateTime?)Data["date_modified"];
							this.databaseRepository = (int)Data["downloads"];
							this._CustomerRepository = (string)Data["icon_url"];
							if (Operators.CompareString(this._CustomerRepository, "", false) == 0)
							{
								this._CustomerRepository = null;
							}
							this.m_TokenizerRepository = string.Format("https://modrinth.com/{0}/{1}", Data["project_type"], this.m_MerchantRepository);
							this.pageRepository = Enumerable.ToList<int>(Enumerable.OrderByDescending<int, int>(Enumerable.Distinct<int>(Enumerable.Where<int>(Enumerable.Select<string, int>(Enumerable.Where<string>(Enumerable.Select<JToken, string>(((JArray)(Data["game_versions"] ?? Data["versions"])) ?? new JArray(), (ModComp.CompProject._Closure$__.$I23-14 == null) ? (ModComp.CompProject._Closure$__.$I23-14 = ((JToken v) => v.ToString())) : ModComp.CompProject._Closure$__.$I23-14), (ModComp.CompProject._Closure$__.$I23-15 == null) ? (ModComp.CompProject._Closure$__.$I23-15 = ((string v) => v.StartsWithF("1.", false))) : ModComp.CompProject._Closure$__.$I23-15), (ModComp.CompProject._Closure$__.$I23-16 == null) ? (ModComp.CompProject._Closure$__.$I23-16 = ((string v) => (int)Math.Round(ModBase.Val(v.Split(".")[1].BeforeFirst("-", false))))) : ModComp.CompProject._Closure$__.$I23-16), (ModComp.CompProject._Closure$__.$I23-17 == null) ? (ModComp.CompProject._Closure$__.$I23-17 = ((int v) => v > 0)) : ModComp.CompProject._Closure$__.$I23-17)), (ModComp.CompProject._Closure$__.$I23-18 == null) ? (ModComp.CompProject._Closure$__.$I23-18 = ((int v) => v)) : ModComp.CompProject._Closure$__.$I23-18));
							string left = Data["project_type"].ToString();
							if (Operators.CompareString(left, "mod", false) != 0)
							{
								if (Operators.CompareString(left, "modpack", false) != 0)
								{
									if (Operators.CompareString(left, "resourcepack", false) == 0)
									{
										this.Type = ModComp.CompType.ResourcePack;
									}
								}
								else
								{
									this.Type = ModComp.CompType.ModPack;
								}
							}
							else
							{
								this.Type = ModComp.CompType.Mod;
							}
							this.m_PoolRepository = new List<string>();
							this.predicateRepository = new List<ModComp.CompModLoaderType>();
							try
							{
								foreach (string text in Enumerable.Select<JToken, string>(Data["categories"], (ModComp.CompProject._Closure$__.$I23-19 == null) ? (ModComp.CompProject._Closure$__.$I23-19 = ((JToken t) => t.ToString())) : ModComp.CompProject._Closure$__.$I23-19))
								{
									uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text);
									if (num2 <= 2310183952U)
									{
										if (num2 <= 889035115U)
										{
											if (num2 <= 484666913U)
											{
												if (num2 != 30055014U)
												{
													if (num2 != 126402435U)
													{
														if (num2 == 484666913U)
														{
															if (Operators.CompareString(text, "combat", false) == 0)
															{
																this.m_PoolRepository.Add("战斗");
															}
														}
													}
													else if (Operators.CompareString(text, "decoration", false) == 0)
													{
														this.m_PoolRepository.Add("装饰");
													}
												}
												else if (Operators.CompareString(text, "storage", false) == 0)
												{
													this.m_PoolRepository.Add("仓储");
												}
											}
											else if (num2 != 564424733U)
											{
												if (num2 != 659651964U)
												{
													if (num2 == 889035115U)
													{
														if (Operators.CompareString(text, "lightweight", false) == 0)
														{
															this.m_PoolRepository.Add("轻量");
														}
													}
												}
												else if (Operators.CompareString(text, "neoforge", false) == 0)
												{
													this.predicateRepository.Add(ModComp.CompModLoaderType.NeoForge);
												}
											}
											else if (Operators.CompareString(text, "game-mechanics", false) == 0)
											{
												this.m_PoolRepository.Add("游戏机制");
											}
										}
										else if (num2 <= 1446847267U)
										{
											if (num2 != 1028682697U)
											{
												if (num2 != 1418100143U)
												{
													if (num2 == 1446847267U)
													{
														if (Operators.CompareString(text, "transportation", false) == 0)
														{
															this.m_PoolRepository.Add("运输");
														}
													}
												}
												else if (Operators.CompareString(text, "technology", false) == 0)
												{
													this.m_PoolRepository.Add("科技");
												}
											}
											else if (Operators.CompareString(text, "food", false) == 0)
											{
												this.m_PoolRepository.Add("食物/烹饪");
											}
										}
										else if (num2 != 1743038524U)
										{
											if (num2 != 1955283044U)
											{
												if (num2 == 2310183952U)
												{
													if (Operators.CompareString(text, "forge", false) == 0)
													{
														this.predicateRepository.Add(ModComp.CompModLoaderType.Forge);
													}
												}
											}
											else if (Operators.CompareString(text, "optimization", false) == 0)
											{
												this.m_PoolRepository.Add("性能优化");
											}
										}
										else if (Operators.CompareString(text, "fabric", false) == 0)
										{
											this.predicateRepository.Add(ModComp.CompModLoaderType.Fabric);
										}
									}
									else if (num2 <= 3249952714U)
									{
										if (num2 <= 3001865938U)
										{
											if (num2 != 2432105424U)
											{
												if (num2 != 2898550409U)
												{
													if (num2 == 3001865938U)
													{
														if (Operators.CompareString(text, "social", false) == 0)
														{
															this.m_PoolRepository.Add("服务器");
														}
													}
												}
												else if (Operators.CompareString(text, "challenging", false) == 0)
												{
													this.m_PoolRepository.Add("硬核");
												}
											}
											else if (Operators.CompareString(text, "library", false) == 0)
											{
												this.m_PoolRepository.Add("支持库");
											}
										}
										else if (num2 != 3037049615U)
										{
											if (num2 != 3217471839U)
											{
												if (num2 == 3249952714U)
												{
													if (Operators.CompareString(text, "quests", false) == 0)
													{
														this.m_PoolRepository.Add("任务");
													}
												}
											}
											else if (Operators.CompareString(text, "utility", false) == 0)
											{
												this.m_PoolRepository.Add("改良");
											}
										}
										else if (Operators.CompareString(text, "worldgen", false) == 0)
										{
											this.m_PoolRepository.Add("世界元素");
										}
									}
									else if (num2 <= 3463588087U)
									{
										if (num2 != 3302598349U)
										{
											if (num2 != 3306152395U)
											{
												if (num2 == 3463588087U)
												{
													if (Operators.CompareString(text, "equipment", false) == 0)
													{
														this.m_PoolRepository.Add("装备");
													}
												}
											}
											else if (Operators.CompareString(text, "adventure", false) == 0)
											{
												this.m_PoolRepository.Add("冒险");
											}
										}
										else if (Operators.CompareString(text, "kitchen-sink", false) == 0)
										{
											this.m_PoolRepository.Add("大杂烩");
										}
									}
									else if (num2 <= 3964042846U)
									{
										if (num2 != 3840615820U)
										{
											if (num2 == 3964042846U)
											{
												if (Operators.CompareString(text, "quilt", false) == 0)
												{
													this.predicateRepository.Add(ModComp.CompModLoaderType.Quilt);
												}
											}
										}
										else if (Operators.CompareString(text, "magic", false) == 0)
										{
											this.m_PoolRepository.Add("魔法");
										}
									}
									else if (num2 != 4053708971U)
									{
										if (num2 == 4269569010U)
										{
											if (Operators.CompareString(text, "mobs", false) == 0)
											{
												this.m_PoolRepository.Add("生物");
											}
										}
									}
									else if (Operators.CompareString(text, "multiplayer", false) == 0)
									{
										this.m_PoolRepository.Add("多人");
									}
								}
							}
							finally
							{
								IEnumerator<string> enumerator4;
								if (enumerator4 != null)
								{
									enumerator4.Dispose();
								}
							}
							if (!Enumerable.Any<string>(this.m_PoolRepository))
							{
								this.m_PoolRepository.Add("杂项");
							}
							this.m_PoolRepository.Sort();
							this.predicateRepository.Sort();
						}
					}
					ModComp.pool[this._AuthenticationRepository] = this;
				}
			}

			// Token: 0x060001F2 RID: 498 RVA: 0x00017CA0 File Offset: 0x00015EA0
			public JObject ToJson()
			{
				JObject jobject = new JObject();
				jobject["DataSource"] = (this.listRepository ? "CurseForge" : "Modrinth");
				jobject["Type"] = (int)this.Type;
				jobject["Slug"] = this.m_MerchantRepository;
				jobject["Id"] = this._AuthenticationRepository;
				if (this.m_AlgoRepository != null)
				{
					jobject["CurseForgeFileIds"] = new JArray(this.m_AlgoRepository);
				}
				jobject["RawName"] = this.comparatorRepository;
				jobject["Description"] = this.mappingRepository;
				jobject["Website"] = this.m_TokenizerRepository;
				if (this._FilterRepository != null)
				{
					jobject["LastUpdate"] = this._FilterRepository;
				}
				jobject["DownloadCount"] = this.databaseRepository;
				if (this.predicateRepository != null && Enumerable.Any<ModComp.CompModLoaderType>(this.predicateRepository))
				{
					jobject["ModLoaders"] = new JArray(Enumerable.Select<ModComp.CompModLoaderType, int>(this.predicateRepository, (ModComp.CompProject._Closure$__.$I24-0 == null) ? (ModComp.CompProject._Closure$__.$I24-0 = ((ModComp.CompModLoaderType m) => (int)m)) : ModComp.CompProject._Closure$__.$I24-0));
				}
				jobject["Tags"] = new JArray(this.m_PoolRepository);
				if (!string.IsNullOrEmpty(this._CustomerRepository))
				{
					jobject["LogoUrl"] = this._CustomerRepository;
				}
				if (Enumerable.Any<int>(this.pageRepository))
				{
					jobject["GameVersions"] = new JArray(this.pageRepository);
				}
				jobject["CacheTime"] = DateTime.Now;
				return jobject;
			}

			// Token: 0x060001F3 RID: 499 RVA: 0x00017E7C File Offset: 0x0001607C
			public MyCompItem ToCompItem(bool ShowMcVersionDesc, bool ShowLoaderDesc)
			{
				checked
				{
					string text;
					if (this.pageRepository != null && Enumerable.Any<int>(this.pageRepository))
					{
						List<string> list = new List<string>();
						bool flag = false;
						int num = this.pageRepository.Count - 1;
						int i = 0;
						while (i <= num)
						{
							int num2 = this.pageRepository[i];
							int num3 = this.pageRepository[i];
							if (num2 < 10)
							{
								if (Enumerable.Any<string>(list) && !flag)
								{
									break;
								}
								flag = true;
							}
							int num4 = i + 1;
							int num5 = this.pageRepository.Count - 1;
							int num6 = num4;
							while (num6 <= num5 && this.pageRepository[num6] == num3 - 1)
							{
								num3 = this.pageRepository[num6];
								i = num6;
								num6++;
							}
							if (num2 == num3)
							{
								list.Add("1." + Conversions.ToString(num2));
							}
							else if (ModDownloadLib.m_StructField > -1 && num2 >= ModDownloadLib.m_StructField)
							{
								if (num3 < 10)
								{
									list.Clear();
									list.Add("全版本");
									break;
								}
								list.Add("1." + Conversions.ToString(num3) + "+");
							}
							else
							{
								if (num3 < 10)
								{
									list.Add("1." + Conversions.ToString(num2) + "-");
									break;
								}
								if (num2 - num3 == 1)
								{
									list.Add("1." + Conversions.ToString(num2) + ", 1." + Conversions.ToString(num3));
								}
								else
								{
									list.Add("1." + Conversions.ToString(num2) + "~1." + Conversions.ToString(num3));
								}
							}
							i++;
							continue;
							IL_1B4:
							text = list.Join(", ");
							goto IL_1C9;
						}
						goto IL_1B4;
					}
					text = "未知";
					IL_1C9:
					List<ModComp.CompModLoaderType> list2 = new List<ModComp.CompModLoaderType>(this.predicateRepository);
					if (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("ToolDownloadIgnoreQuilt", null)))
					{
						list2.Remove(ModComp.CompModLoaderType.Quilt);
					}
					int count = list2.Count;
					string text2;
					string text3;
					if (count != 0)
					{
						if (count != 1)
						{
							if (Conversions.ToBoolean(this.predicateRepository.Contains(ModComp.CompModLoaderType.Forge) && (Enumerable.Max(this.pageRepository) < 14 || this.predicateRepository.Contains(ModComp.CompModLoaderType.Fabric)) && (Enumerable.Max(this.pageRepository) < 20 || this.predicateRepository.Contains(ModComp.CompModLoaderType.NeoForge)) && (Enumerable.Max(this.pageRepository) < 14 || this.predicateRepository.Contains(ModComp.CompModLoaderType.Quilt) || Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("ToolDownloadIgnoreQuilt", null)))))
							{
								text2 = "任意";
								text3 = "";
							}
							else
							{
								text2 = list2.Join(" / ");
								text3 = list2.Join(" / ");
							}
						}
						else
						{
							text2 = "仅 " + Enumerable.Single<ModComp.CompModLoaderType>(list2).ToString();
							text3 = Enumerable.Single<ModComp.CompModLoaderType>(list2).ToString();
						}
					}
					else if (this.predicateRepository.Count == 1)
					{
						text2 = "仅 " + Enumerable.Single<ModComp.CompModLoaderType>(this.predicateRepository).ToString();
						text3 = Enumerable.Single<ModComp.CompModLoaderType>(this.predicateRepository).ToString();
					}
					else
					{
						text2 = "未知";
						text3 = "";
					}
					MyCompItem myCompItem = new MyCompItem
					{
						Tag = this,
						Logo = this.GetControlLogo()
					};
					KeyValuePair<string, string> controlTitle = this.GetControlTitle(true);
					myCompItem.Title = controlTitle.Key;
					if (Operators.CompareString(controlTitle.Value, "", false) == 0)
					{
						((StackPanel)myCompItem.LabTitleRaw.Parent).Children.Remove(myCompItem.LabTitleRaw);
					}
					else
					{
						myCompItem.RestartReader(controlTitle.Value);
					}
					myCompItem.Tags = this.m_PoolRepository;
					myCompItem.Description = this.mappingRepository.Replace("\r", "").Replace("\n", "");
					if (!ShowMcVersionDesc && !ShowLoaderDesc)
					{
						((Grid)myCompItem.PathVersion.Parent).Children.Remove(myCompItem.PathVersion);
						((Grid)myCompItem.LabVersion.Parent).Children.Remove(myCompItem.LabVersion);
						myCompItem.ColumnVersion1.Width = new GridLength(0.0);
						myCompItem.ColumnVersion2.MaxWidth = 0.0;
						myCompItem.ColumnVersion3.Width = new GridLength(0.0);
					}
					else if (ShowMcVersionDesc && ShowMcVersionDesc)
					{
						myCompItem.LabVersion.Text = ((Operators.CompareString(text3, "", false) == 0) ? "" : (text3 + " ")) + text;
					}
					else if (ShowMcVersionDesc)
					{
						myCompItem.LabVersion.Text = text;
					}
					else
					{
						myCompItem.LabVersion.Text = text2;
					}
					myCompItem.LabSource.Text = (this.listRepository ? "CurseForge" : "Modrinth");
					if (this._FilterRepository != null)
					{
						myCompItem.LabTime.Text = ModBase.GetTimeSpanString((this._FilterRepository - DateTime.Now).Value, true);
					}
					else
					{
						myCompItem.LabTime.Visibility = Visibility.Collapsed;
						myCompItem.ColumnTime1.Width = new GridLength(0.0);
						myCompItem.ColumnTime2.Width = new GridLength(0.0);
						myCompItem.ColumnTime3.Width = new GridLength(0.0);
					}
					myCompItem.LabDownload.Text = Conversions.ToString((this.databaseRepository > 100000000) ? (Conversions.ToString(Math.Round((double)this.databaseRepository / 100000000.0, 2)) + " 亿") : ((this.databaseRepository > 100000) ? (Conversions.ToString(Math.Floor((double)this.databaseRepository / 10000.0)) + " 万") : this.databaseRepository));
					return myCompItem;
				}
			}

			// Token: 0x060001F4 RID: 500 RVA: 0x000184EC File Offset: 0x000166EC
			public string GetControlLogo()
			{
				string result;
				if (string.IsNullOrEmpty(this._CustomerRepository))
				{
					result = ModBase.m_SerializerRepository + "Icons/NoIcon.png";
				}
				else
				{
					result = this._CustomerRepository;
				}
				return result;
			}

			// Token: 0x060001F5 RID: 501 RVA: 0x00018520 File Offset: 0x00016720
			public KeyValuePair<string, string> GetControlTitle(bool HasModLoaderDescription)
			{
				string text = this.comparatorRepository;
				List<string> list2;
				if (Operators.CompareString(this.RemoveTests(), this.comparatorRepository, false) == 0)
				{
					List<string> list = Enumerable.ToList<string>(Enumerable.Where<string>(Enumerable.Select<string, string>(this.RemoveTests().Split(new string[]
					{
						" | ",
						" - ",
						"(",
						")",
						"[",
						"]",
						"{",
						"}"
					}, StringSplitOptions.RemoveEmptyEntries), (ModComp.CompProject._Closure$__.$I27-0 == null) ? (ModComp.CompProject._Closure$__.$I27-0 = ((string s) => s.Trim(" /\\".ToCharArray()))) : ModComp.CompProject._Closure$__.$I27-0), (ModComp.CompProject._Closure$__.$I27-1 == null) ? (ModComp.CompProject._Closure$__.$I27-1 = ((string w) => !string.IsNullOrEmpty(w))) : ModComp.CompProject._Closure$__.$I27-1));
					if (list.Count == 1)
					{
						goto IL_571;
					}
					list2 = new List<string>();
					List<string> list3 = new List<string>();
					try
					{
						foreach (string text2 in list)
						{
							string text3 = text2.ToLower();
							if (Operators.CompareString(text2.ToUpper(), text2, false) == 0 && Operators.CompareString(text2, "FPS", false) != 0 && Operators.CompareString(text2, "HUD", false) != 0)
							{
								list2.Add(text2);
							}
							else if ((text3.Contains("forge") || text3.Contains("fabric") || text3.Contains("quilt")) && !ModBase.RegexCheck(text3.Replace("forge", "").Replace("fabric", "").Replace("quilt", ""), "[a-z]+", 0))
							{
								list2.Add(text2);
							}
							else
							{
								list3.Add(text2);
							}
						}
					}
					finally
					{
						List<string>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					if (!Enumerable.Any<string>(list3) || !Enumerable.Any<string>(list2))
					{
						goto IL_571;
					}
					text = list3.Join(" - ");
				}
				else
				{
					text = this.RemoveTests().BeforeFirst(" (", false).BeforeFirst(" - ", false);
					string text4 = "";
					if (this.RemoveTests().AfterLast(")", false).Contains(" - "))
					{
						text4 = this.RemoveTests().AfterLast(")", false).AfterLast(" - ", false);
					}
					string text5 = this.RemoveTests();
					if (Operators.CompareString(text4, "", false) != 0)
					{
						text5 = text5.Replace(" - " + text4, "");
					}
					text5 = text5.Replace(text, "").Trim(new char[]
					{
						'(',
						')',
						' '
					});
					list2 = Enumerable.ToList<string>(Enumerable.Where<string>(Enumerable.Select<string, string>(text5.Split(new string[]
					{
						" | ",
						" - ",
						"(",
						")",
						"[",
						"]",
						"{",
						"}"
					}, StringSplitOptions.RemoveEmptyEntries), (ModComp.CompProject._Closure$__.$I27-2 == null) ? (ModComp.CompProject._Closure$__.$I27-2 = ((string s) => s.Trim(" /".ToCharArray()))) : ModComp.CompProject._Closure$__.$I27-2), (ModComp.CompProject._Closure$__.$I27-3 == null) ? (ModComp.CompProject._Closure$__.$I27-3 = ((string w) => !string.IsNullOrEmpty(w))) : ModComp.CompProject._Closure$__.$I27-3));
					if (list2.Count > 1 && !Enumerable.Any<string>(list2, (ModComp.CompProject._Closure$__.$I27-4 == null) ? (ModComp.CompProject._Closure$__.$I27-4 = ((string s) => s.ToLower().Contains("forge") || s.ToLower().Contains("fabric") || s.ToLower().Contains("quilt"))) : ModComp.CompProject._Closure$__.$I27-4) && (list2.Count != 2 || Operators.CompareString(Enumerable.Last<string>(list2).ToUpper(), Enumerable.Last<string>(list2), false) != 0))
					{
						list2 = new List<string>
						{
							text5
						};
					}
					if (Operators.CompareString(text4, "", false) != 0)
					{
						list2.Add(text4);
					}
				}
				list2 = Enumerable.ToList<string>(Enumerable.Distinct<string>(list2));
				string text6 = "";
				if (Enumerable.Any<string>(list2))
				{
					try
					{
						foreach (string text7 in list2)
						{
							bool flag = text7.ToLower().Contains("forge") || text7.ToLower().Contains("fabric") || text7.ToLower().Contains("quilt");
							if ((HasModLoaderDescription || !flag) && (text7.Length >= 16 || !text7.ToLower().Contains("fabric") || !text7.ToLower().Contains("forge")))
							{
								if (flag && !text7.Contains("版") && text7.ToLower().Replace("forge", "").Replace("fabric", "").Replace("quilt", "").Length <= 3)
								{
									text7 = text7.Replace("Edition", "").Replace("edition", "").Trim().Capitalize() + " 版";
								}
								text7 = text7.Replace("forge", "Forge").Replace("neo", "Neo").Replace("fabric", "Fabric").Replace("quilt", "Quilt");
								text6 = text6 + "  |  " + text7.Trim();
							}
						}
						goto IL_577;
					}
					finally
					{
						List<string>.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
				}
				IL_571:
				text6 = "";
				IL_577:
				return new KeyValuePair<string, string>(text, text6);
			}

			// Token: 0x060001F6 RID: 502 RVA: 0x00018AE0 File Offset: 0x00016CE0
			public bool IsLike(ModComp.CompProject Project)
			{
				bool result;
				if (Operators.CompareString(this._AuthenticationRepository, Project._AuthenticationRepository, false) == 0)
				{
					result = true;
				}
				else
				{
					VB$AnonymousDelegate_0<string, string> vb$AnonymousDelegate_ = (ModComp.CompProject._Closure$__.$I28-0 == null) ? (ModComp.CompProject._Closure$__.$I28-0 = delegate(string Data)
					{
						StringBuilder stringBuilder = new StringBuilder();
						try
						{
							foreach (char value in Enumerable.Where<char>(Data, (ModComp.CompProject._Closure$__.$I28-1 == null) ? (ModComp.CompProject._Closure$__.$I28-1 = ((char c) => char.IsLetterOrDigit(c))) : ModComp.CompProject._Closure$__.$I28-1))
							{
								stringBuilder.Append(value);
							}
						}
						finally
						{
							IEnumerator<char> enumerator;
							if (enumerator != null)
							{
								enumerator.Dispose();
							}
						}
						return stringBuilder.ToString().ToLower();
					}) : ModComp.CompProject._Closure$__.$I28-0;
					if (this.listRepository == Project.listRepository)
					{
						result = false;
					}
					else if (this.predicateRepository.Count == Project.predicateRepository.Count && !Enumerable.Any<ModComp.CompModLoaderType>(Enumerable.Except<ModComp.CompModLoaderType>(this.predicateRepository, Project.predicateRepository)))
					{
						if (this.pageRepository.Count == Project.pageRepository.Count && !Enumerable.Any<int>(Enumerable.Except<int>(this.pageRepository, Project.pageRepository)))
						{
							if (Operators.CompareString(this.RemoveTests(), Project.RemoveTests(), false) != 0 && Operators.CompareString(this.comparatorRepository, Project.comparatorRepository, false) != 0 && Operators.CompareString(this.mappingRepository, Project.mappingRepository, false) != 0 && Operators.CompareString(vb$AnonymousDelegate_(this.m_MerchantRepository), vb$AnonymousDelegate_(Project.m_MerchantRepository), false) != 0)
							{
								result = false;
							}
							else
							{
								ModBase.Log(string.Format("[Comp] 将 {0} ({1}) 与 {2} ({3}) 认定为相似工程", new object[]
								{
									this.comparatorRepository,
									this.m_MerchantRepository,
									Project.comparatorRepository,
									Project.m_MerchantRepository
								}), ModBase.LogLevel.Normal, "出现错误");
								if (this.AddTests() == null && Project.AddTests() != null)
								{
									this.InstantiateTests(Project.AddTests());
								}
								if (this.AddTests() != null && Project.AddTests() == null)
								{
									Project.InstantiateTests(this.AddTests());
								}
								result = true;
							}
						}
						else
						{
							result = false;
						}
					}
					else
					{
						result = false;
					}
				}
				return result;
			}

			// Token: 0x060001F7 RID: 503 RVA: 0x00003319 File Offset: 0x00001519
			public override string ToString()
			{
				return string.Format("{0} ({1}): {2}", this._AuthenticationRepository, this.m_MerchantRepository, this.comparatorRepository);
			}

			// Token: 0x060001F8 RID: 504 RVA: 0x00018C94 File Offset: 0x00016E94
			public override bool Equals(object obj)
			{
				ModComp.CompProject compProject = obj as ModComp.CompProject;
				return compProject != null && Operators.CompareString(this._AuthenticationRepository, compProject._AuthenticationRepository, false) == 0;
			}

			// Token: 0x060001F9 RID: 505 RVA: 0x00003337 File Offset: 0x00001537
			public static bool operator ==(ModComp.CompProject left, ModComp.CompProject right)
			{
				return EqualityComparer<ModComp.CompProject>.Default.Equals(left, right);
			}

			// Token: 0x060001FA RID: 506 RVA: 0x00003345 File Offset: 0x00001545
			public static bool operator !=(ModComp.CompProject left, ModComp.CompProject right)
			{
				return !(left == right);
			}

			// Token: 0x040000E7 RID: 231
			public readonly bool listRepository;

			// Token: 0x040000E8 RID: 232
			public readonly ModComp.CompType Type;

			// Token: 0x040000E9 RID: 233
			public readonly string m_MerchantRepository;

			// Token: 0x040000EA RID: 234
			public readonly string _AuthenticationRepository;

			// Token: 0x040000EB RID: 235
			public readonly List<int> m_AlgoRepository;

			// Token: 0x040000EC RID: 236
			public readonly string comparatorRepository;

			// Token: 0x040000ED RID: 237
			public readonly string mappingRepository;

			// Token: 0x040000EE RID: 238
			public readonly string m_TokenizerRepository;

			// Token: 0x040000EF RID: 239
			public readonly DateTime? _FilterRepository;

			// Token: 0x040000F0 RID: 240
			public readonly int databaseRepository;

			// Token: 0x040000F1 RID: 241
			public readonly List<ModComp.CompModLoaderType> predicateRepository;

			// Token: 0x040000F2 RID: 242
			public readonly List<string> m_PoolRepository;

			// Token: 0x040000F3 RID: 243
			public string _CustomerRepository;

			// Token: 0x040000F4 RID: 244
			public readonly List<int> pageRepository;

			// Token: 0x040000F5 RID: 245
			private bool m_InterceptorRepository;

			// Token: 0x040000F6 RID: 246
			private ModComp.CompDatabaseEntry _ContainerRepository;
		}

		// Token: 0x0200005B RID: 91
		public class CompProjectRequest
		{
			// Token: 0x0600021B RID: 539 RVA: 0x00018D48 File Offset: 0x00016F48
			public bool EnableTests()
			{
				if (this.Tag.StartsWithF("/", false) || !this.Source.HasFlag(ModComp.CompSourceType.CurseForge))
				{
					this.m_ParamsRepository.m_InvocationRepository = 0;
				}
				if (this.Tag.EndsWithF("/", false) || !this.Source.HasFlag(ModComp.CompSourceType.Modrinth))
				{
					this.m_ParamsRepository.m_MessageRepository = 0;
				}
				if (this.m_ParamsRepository.m_InvocationRepository != -1)
				{
					if (this.m_ParamsRepository.m_MessageRepository != -1)
					{
						return this.m_ParamsRepository.m_ServiceRepository < this.m_ParamsRepository.m_InvocationRepository || this.m_ParamsRepository.m_ProxyRepository < this.m_ParamsRepository.m_MessageRepository;
					}
				}
				return true;
			}

			// Token: 0x0600021C RID: 540 RVA: 0x00018E1C File Offset: 0x0001701C
			public CompProjectRequest(ModComp.CompType Type, ModComp.CompProjectStorage Storage, int TargetResultCount)
			{
				this.Tag = "";
				this.processRepository = ModComp.CompModLoaderType.Any;
				this.m_ParameterRepository = null;
				this._RecordRepository = null;
				this.Source = ModComp.CompSourceType.Any;
				this.Type = Type;
				this.m_ParamsRepository = Storage;
				this.m_DispatcherRepository = TargetResultCount;
			}

			// Token: 0x0600021D RID: 541 RVA: 0x00018E6C File Offset: 0x0001706C
			public string GetCurseForgeAddress()
			{
				string result;
				if (!this.Source.HasFlag(ModComp.CompSourceType.CurseForge))
				{
					result = null;
				}
				else
				{
					if (this.Tag.StartsWithF("/", false))
					{
						this.m_ParamsRepository.m_InvocationRepository = 0;
					}
					if (this.m_ParamsRepository.m_InvocationRepository > -1 && this.m_ParamsRepository.m_InvocationRepository <= this.m_ParamsRepository.m_ServiceRepository)
					{
						result = null;
					}
					else
					{
						string text = string.Format("https://api.curseforge.com/v1/mods/search?gameId=432&sortField=2&sortOrder=desc&pageSize={0}", 40);
						switch (this.Type)
						{
						case ModComp.CompType.Mod:
							text += "&classId=6";
							break;
						case ModComp.CompType.ModPack:
							text += "&classId=4471";
							break;
						}
						text = text + "&categoryId=" + ((Operators.CompareString(this.Tag, "", false) == 0) ? "0" : this.Tag.BeforeFirst("/", false));
						if (this.processRepository != ModComp.CompModLoaderType.Any)
						{
							text = text + "&modLoaderType=" + Conversions.ToString((int)this.processRepository);
						}
						if (!string.IsNullOrEmpty(this.m_ParameterRepository))
						{
							text = text + "&gameVersion=" + this.m_ParameterRepository;
						}
						if (!string.IsNullOrEmpty(this._RecordRepository))
						{
							text = text + "&searchFilter=" + WebUtility.UrlEncode(this._RecordRepository);
						}
						if (this.m_ParamsRepository.m_ServiceRepository > 0)
						{
							text = text + "&index=" + Conversions.ToString(this.m_ParamsRepository.m_ServiceRepository);
						}
						result = text;
					}
				}
				return result;
			}

			// Token: 0x0600021E RID: 542 RVA: 0x00018FF4 File Offset: 0x000171F4
			public string GetModrinthAddress()
			{
				string result;
				if (!this.Source.HasFlag(ModComp.CompSourceType.Modrinth))
				{
					result = null;
				}
				else
				{
					if (this.Tag.EndsWithF("/", false))
					{
						this.m_ParamsRepository.m_MessageRepository = 0;
					}
					if (this.m_ParamsRepository.m_MessageRepository > -1 && this.m_ParamsRepository.m_MessageRepository <= this.m_ParamsRepository.m_ProxyRepository)
					{
						result = null;
					}
					else
					{
						string text = string.Format("https://api.modrinth.com/v2/search?limit={0}&index=relevance", 40);
						if (!string.IsNullOrEmpty(this._RecordRepository))
						{
							text = text + "&query=" + WebUtility.UrlEncode(this._RecordRepository);
						}
						if (this.m_ParamsRepository.m_ProxyRepository > 0)
						{
							text = text + "&offset=" + Conversions.ToString(this.m_ParamsRepository.m_ProxyRepository);
						}
						List<string> list = new List<string>();
						list.Add(string.Format("[\"project_type:{0}\"]", ModBase.GetStringFromEnum(this.Type).ToLower()));
						if (!string.IsNullOrEmpty(this.Tag))
						{
							list.Add(string.Format("[\"categories:'{0}'\"]", this.Tag.AfterLast("/", false)));
						}
						if (this.processRepository != ModComp.CompModLoaderType.Any)
						{
							list.Add(string.Format("[\"categories:'{0}'\"]", ModBase.GetStringFromEnum(this.processRepository).ToLower()));
						}
						if (!string.IsNullOrEmpty(this.m_ParameterRepository))
						{
							list.Add(string.Format("[\"versions:'{0}'\"]", this.m_ParameterRepository));
						}
						text = text + "&facets=[" + string.Join(",", list) + "]";
						result = text;
					}
				}
				return result;
			}

			// Token: 0x0600021F RID: 543 RVA: 0x00019194 File Offset: 0x00017394
			public override bool Equals(object obj)
			{
				ModComp.CompProjectRequest compProjectRequest = obj as ModComp.CompProjectRequest;
				return compProjectRequest != null && this.Type == compProjectRequest.Type && this.m_DispatcherRepository == compProjectRequest.m_DispatcherRepository && Operators.CompareString(this.Tag, compProjectRequest.Tag, false) == 0 && this.processRepository == compProjectRequest.processRepository && this.Source == compProjectRequest.Source && Operators.CompareString(this.m_ParameterRepository, compProjectRequest.m_ParameterRepository, false) == 0 && Operators.CompareString(this._RecordRepository, compProjectRequest._RecordRepository, false) == 0;
			}

			// Token: 0x06000220 RID: 544 RVA: 0x00003466 File Offset: 0x00001666
			public static bool operator ==(ModComp.CompProjectRequest left, ModComp.CompProjectRequest right)
			{
				return EqualityComparer<ModComp.CompProjectRequest>.Default.Equals(left, right);
			}

			// Token: 0x06000221 RID: 545 RVA: 0x00003474 File Offset: 0x00001674
			public static bool operator !=(ModComp.CompProjectRequest left, ModComp.CompProjectRequest right)
			{
				return !(left == right);
			}

			// Token: 0x04000114 RID: 276
			public ModComp.CompProjectStorage m_ParamsRepository;

			// Token: 0x04000115 RID: 277
			public int m_DispatcherRepository;

			// Token: 0x04000116 RID: 278
			public ModComp.CompType Type;

			// Token: 0x04000117 RID: 279
			public string Tag;

			// Token: 0x04000118 RID: 280
			public ModComp.CompModLoaderType processRepository;

			// Token: 0x04000119 RID: 281
			public string m_ParameterRepository;

			// Token: 0x0400011A RID: 282
			public string _RecordRepository;

			// Token: 0x0400011B RID: 283
			public ModComp.CompSourceType Source;
		}

		// Token: 0x0200005C RID: 92
		public class CompProjectStorage
		{
			// Token: 0x06000223 RID: 547 RVA: 0x00003480 File Offset: 0x00001680
			public CompProjectStorage()
			{
				this.m_ServiceRepository = 0;
				this.m_InvocationRepository = -1;
				this.m_ProxyRepository = 0;
				this.m_MessageRepository = -1;
				this._CreatorRepository = new List<ModComp.CompProject>();
				this._InitializerRepository = null;
			}

			// Token: 0x0400011C RID: 284
			public int m_ServiceRepository;

			// Token: 0x0400011D RID: 285
			public int m_InvocationRepository;

			// Token: 0x0400011E RID: 286
			public int m_ProxyRepository;

			// Token: 0x0400011F RID: 287
			public int m_MessageRepository;

			// Token: 0x04000120 RID: 288
			public List<ModComp.CompProject> _CreatorRepository;

			// Token: 0x04000121 RID: 289
			public string _InitializerRepository;
		}

		// Token: 0x0200005D RID: 93
		public enum CompFileStatus
		{
			// Token: 0x04000123 RID: 291
			Release = 1,
			// Token: 0x04000124 RID: 292
			Beta,
			// Token: 0x04000125 RID: 293
			Alpha
		}

		// Token: 0x0200005E RID: 94
		public class CompFile
		{
			// Token: 0x06000225 RID: 549 RVA: 0x00019224 File Offset: 0x00017424
			public string CountTests()
			{
				ModComp.CompFileStatus objectRepository = this.m_ObjectRepository;
				string result;
				if (objectRepository != ModComp.CompFileStatus.Release)
				{
					if (objectRepository != ModComp.CompFileStatus.Beta)
					{
						result = (ModBase._TokenRepository ? "Alpha 版" : "测试版");
					}
					else
					{
						result = (ModBase._TokenRepository ? "Beta 版" : "测试版");
					}
				}
				else
				{
					result = "正式版";
				}
				return result;
			}

			// Token: 0x06000226 RID: 550 RVA: 0x000034B7 File Offset: 0x000016B7
			public bool ExcludeTests()
			{
				return this._BridgeRepository != null && this._ItemRepository != null;
			}

			// Token: 0x06000227 RID: 551 RVA: 0x00019274 File Offset: 0x00017474
			public ModNet.NetFile ToNetFile(string LocalAddress)
			{
				return new ModNet.NetFile(this._ItemRepository, LocalAddress + (LocalAddress.EndsWithF("\\", false) ? this._BridgeRepository : ""), new ModBase.FileChecker(-1L, -1L, this.m_ReponseRepository, true, false), true);
			}

			// Token: 0x06000228 RID: 552 RVA: 0x000192D0 File Offset: 0x000174D0
			public CompFile(JObject Data, ModComp.CompType Type)
			{
				this._BridgeRepository = null;
				this.m_ReponseRepository = null;
				this.m_GlobalRepository = new List<string>();
				this.exceptionRepository = new List<string>();
				this.Type = Type;
				if (Data.ContainsKey("FromCurseForge"))
				{
					this.singletonRepository = Data["FromCurseForge"].ToObject<bool>();
					this.m_RegRepository = Data["Id"].ToString();
					this._ProductRepository = Data["DisplayName"].ToString();
					this._ListenerRepository = Data["ReleaseDate"].ToObject<DateTime>();
					this._CollectionRepository = Data["DownloadCount"].ToObject<int>();
					this.m_ObjectRepository = (ModComp.CompFileStatus)Data["Status"].ToObject<int>();
					if (Data.ContainsKey("FileName"))
					{
						this._BridgeRepository = Data["FileName"].ToString();
					}
					if (Data.ContainsKey("DownloadUrls"))
					{
						this._ItemRepository = Data["DownloadUrls"].ToObject<List<string>>();
					}
					if (Data.ContainsKey("ModLoaders"))
					{
						this._VisitorRepository = Data["ModLoaders"].ToObject<List<ModComp.CompModLoaderType>>();
					}
					if (Data.ContainsKey("Hash"))
					{
						this.m_ReponseRepository = Data["Hash"].ToString();
					}
					if (Data.ContainsKey("GameVersions"))
					{
						this.m_ValueRepository = Data["GameVersions"].ToObject<List<string>>();
					}
					if (Data.ContainsKey("RawDependencies"))
					{
						this.m_GlobalRepository = Data["RawDependencies"].ToObject<List<string>>();
					}
					if (Data.ContainsKey("Dependencies"))
					{
						this.exceptionRepository = Data["Dependencies"].ToObject<List<string>>();
						return;
					}
				}
				else
				{
					this.singletonRepository = Data.ContainsKey("gameId");
					if (this.singletonRepository)
					{
						this.m_RegRepository = (string)Data["id"];
						this._ProductRepository = Data["displayName"].ToString().Replace("\t", "").Trim(new char[]
						{
							' '
						});
						this._ListenerRepository = (DateTime)Data["fileDate"];
						this.m_ObjectRepository = (ModComp.CompFileStatus)Data["releaseType"].ToObject<int>();
						this._CollectionRepository = (int)Data["downloadCount"];
						this._BridgeRepository = (string)Data["fileName"];
						JToken jtoken = Enumerable.FirstOrDefault<JToken>(Enumerable.ToList<JToken>((JArray)Data["hashes"]), (ModComp.CompFile._Closure$__.$I19-0 == null) ? (ModComp.CompFile._Closure$__.$I19-0 = ((JToken s) => s["algo"].ToObject<int>() == 1)) : ModComp.CompFile._Closure$__.$I19-0);
						this.m_ReponseRepository = (string)((jtoken != null) ? jtoken["value"] : null);
						if (this.m_ReponseRepository == null)
						{
							JToken jtoken2 = Enumerable.FirstOrDefault<JToken>(Enumerable.ToList<JToken>((JArray)Data["hashes"]), (ModComp.CompFile._Closure$__.$I19-1 == null) ? (ModComp.CompFile._Closure$__.$I19-1 = ((JToken s) => s["algo"].ToObject<int>() == 2)) : ModComp.CompFile._Closure$__.$I19-1);
							this.m_ReponseRepository = (string)((jtoken2 != null) ? jtoken2["value"] : null);
						}
						string text = Data["downloadUrl"].ToString();
						if (Operators.CompareString(text, "", false) == 0)
						{
							text = string.Format("https://media.forgecdn.net/files/{0}/{1}/{2}", Conversions.ToInteger(this.m_RegRepository.ToString().Substring(0, 4)), Conversions.ToInteger(this.m_RegRepository.ToString().Substring(4)), this._BridgeRepository);
						}
						text = text.Replace(this._BridgeRepository, WebUtility.UrlEncode(this._BridgeRepository));
						this._ItemRepository = Enumerable.ToList<string>(Enumerable.Distinct<string>(new List<string>
						{
							text.Replace("-service.overwolf.wtf", ".forgecdn.net").Replace("://edge", "://media"),
							text.Replace("-service.overwolf.wtf", ".forgecdn.net"),
							text.Replace("://edge", "://media"),
							text
						}));
						this._ItemRepository.AddRange(Enumerable.ToList<string>(Enumerable.Select<string, string>(this._ItemRepository, (ModComp.CompFile._Closure$__.$I19-2 == null) ? (ModComp.CompFile._Closure$__.$I19-2 = ((string u) => ModDownload.DlSourceModGet(u))) : ModComp.CompFile._Closure$__.$I19-2)));
						this._ItemRepository = Enumerable.ToList<string>(Enumerable.Distinct<string>(this._ItemRepository));
						if (Type == ModComp.CompType.Mod)
						{
							this.m_GlobalRepository = Enumerable.ToList<string>(Enumerable.Select<JToken, string>(Enumerable.Where<JToken>(Data["dependencies"], (ModComp.CompFile._Closure$__.$I19-3 == null) ? (ModComp.CompFile._Closure$__.$I19-3 = ((JToken d) => d["relationType"].ToObject<int>() == 3 && d["modId"].ToObject<int>() != 306612 && d["modId"].ToObject<int>() != 634179)) : ModComp.CompFile._Closure$__.$I19-3), (ModComp.CompFile._Closure$__.$I19-4 == null) ? (ModComp.CompFile._Closure$__.$I19-4 = ((JToken d) => d["modId"].ToString())) : ModComp.CompFile._Closure$__.$I19-4));
						}
						List<string> list = Enumerable.ToList<string>(Enumerable.Select<JToken, string>(Data["gameVersions"], (ModComp.CompFile._Closure$__.$I19-5 == null) ? (ModComp.CompFile._Closure$__.$I19-5 = ((JToken t) => t.ToString().Trim().ToLower())) : ModComp.CompFile._Closure$__.$I19-5));
						this.m_ValueRepository = Enumerable.ToList<string>(Enumerable.Select<string, string>(Enumerable.Where<string>(list, (ModComp.CompFile._Closure$__.$I19-6 == null) ? (ModComp.CompFile._Closure$__.$I19-6 = ((string v) => v.StartsWithF("1.", false))) : ModComp.CompFile._Closure$__.$I19-6), (ModComp.CompFile._Closure$__.$I19-7 == null) ? (ModComp.CompFile._Closure$__.$I19-7 = ((string v) => v.Replace("-snapshot", " 预览版"))) : ModComp.CompFile._Closure$__.$I19-7));
						if (this.m_ValueRepository.Count > 1)
						{
							this.m_ValueRepository = Enumerable.ToList<string>(this.m_ValueRepository.Sort(new ModBase.CompareThreadStart<string>(ModMinecraft.VersionSortBoolean)));
							if (Type == ModComp.CompType.ModPack)
							{
								this.m_ValueRepository = new List<string>
								{
									this.m_ValueRepository[0]
								};
							}
						}
						else if (this.m_ValueRepository.Count == 1)
						{
							this.m_ValueRepository = Enumerable.ToList<string>(this.m_ValueRepository);
						}
						else
						{
							this.m_ValueRepository = new List<string>
							{
								"未知版本"
							};
						}
						this._VisitorRepository = new List<ModComp.CompModLoaderType>();
						if (list.Contains("forge"))
						{
							this._VisitorRepository.Add(ModComp.CompModLoaderType.Forge);
						}
						if (list.Contains("fabric"))
						{
							this._VisitorRepository.Add(ModComp.CompModLoaderType.Fabric);
						}
						if (list.Contains("quilt"))
						{
							this._VisitorRepository.Add(ModComp.CompModLoaderType.Quilt);
						}
						if (list.Contains("neoforge"))
						{
							this._VisitorRepository.Add(ModComp.CompModLoaderType.NeoForge);
							return;
						}
					}
					else
					{
						this.m_RegRepository = (string)Data["id"];
						this._ProductRepository = Data["name"].ToString().Replace("\t", "").Trim(new char[]
						{
							' '
						});
						this._ListenerRepository = (DateTime)Data["date_published"];
						this.m_ObjectRepository = ((Operators.CompareString(Data["version_type"].ToString(), "release", false) == 0) ? ModComp.CompFileStatus.Release : ((Operators.CompareString(Data["version_type"].ToString(), "beta", false) == 0) ? ModComp.CompFileStatus.Beta : ModComp.CompFileStatus.Alpha));
						this._CollectionRepository = (int)Data["downloads"];
						if (Enumerable.Any<JToken>((JArray)Data["files"]))
						{
							JToken jtoken3 = Data["files"][0];
							this._BridgeRepository = (string)jtoken3["filename"];
							this._ItemRepository = Enumerable.ToList<string>(Enumerable.Distinct<string>(new List<string>
							{
								(string)jtoken3["url"],
								ModDownload.DlSourceModGet((string)jtoken3["url"])
							}));
							this.m_ReponseRepository = (string)jtoken3["hashes"]["sha1"];
						}
						if (Type == ModComp.CompType.Mod)
						{
							this.m_GlobalRepository = Enumerable.ToList<string>(Enumerable.Select<JToken, string>(Enumerable.Where<JToken>(Data["dependencies"], (ModComp.CompFile._Closure$__.$I19-8 == null) ? (ModComp.CompFile._Closure$__.$I19-8 = ((JToken d) => (string)d["dependency_type"] == "required" && (string)d["project_id"] != "P7dR8mSH" && (string)d["project_id"] != "qvIfYCYJ" && d["project_id"].ToString().Length > 0)) : ModComp.CompFile._Closure$__.$I19-8), (ModComp.CompFile._Closure$__.$I19-9 == null) ? (ModComp.CompFile._Closure$__.$I19-9 = ((JToken d) => d["project_id"].ToString())) : ModComp.CompFile._Closure$__.$I19-9));
						}
						List<string> list2 = Enumerable.ToList<string>(Enumerable.Select<JToken, string>(Data["game_versions"], (ModComp.CompFile._Closure$__.$I19-10 == null) ? (ModComp.CompFile._Closure$__.$I19-10 = ((JToken t) => t.ToString().Trim().ToLower())) : ModComp.CompFile._Closure$__.$I19-10));
						this.m_ValueRepository = Enumerable.ToList<string>(Enumerable.Select<string, string>(Enumerable.Where<string>(list2, (ModComp.CompFile._Closure$__.$I19-11 == null) ? (ModComp.CompFile._Closure$__.$I19-11 = ((string v) => v.StartsWithF("1.", false) || v.StartsWithF("b1.", false))) : ModComp.CompFile._Closure$__.$I19-11), (ModComp.CompFile._Closure$__.$I19-12 == null) ? (ModComp.CompFile._Closure$__.$I19-12 = delegate(string v)
						{
							if (v.Contains("-"))
							{
								return v.BeforeFirst("-", false) + " 预览版";
							}
							if (!v.StartsWithF("b1.", false))
							{
								return v;
							}
							return "远古版本";
						}) : ModComp.CompFile._Closure$__.$I19-12));
						if (this.m_ValueRepository.Count > 1)
						{
							this.m_ValueRepository = Enumerable.ToList<string>(this.m_ValueRepository.Sort(new ModBase.CompareThreadStart<string>(ModMinecraft.VersionSortBoolean)));
							if (Type == ModComp.CompType.ModPack)
							{
								this.m_ValueRepository = new List<string>
								{
									this.m_ValueRepository[0]
								};
							}
						}
						else if (this.m_ValueRepository.Count != 1)
						{
							if (Enumerable.Any<string>(list2, (ModComp.CompFile._Closure$__.$I19-13 == null) ? (ModComp.CompFile._Closure$__.$I19-13 = ((string v) => ModBase.RegexCheck(v, "[0-9]{2}w[0-9]{2}[a-z]{1}", 0))) : ModComp.CompFile._Closure$__.$I19-13))
							{
								this.m_ValueRepository = Enumerable.ToList<string>(Enumerable.Where<string>(list2, (ModComp.CompFile._Closure$__.$I19-14 == null) ? (ModComp.CompFile._Closure$__.$I19-14 = ((string v) => ModBase.RegexCheck(v, "[0-9]{2}w[0-9]{2}[a-z]{1}", 0))) : ModComp.CompFile._Closure$__.$I19-14));
							}
							else
							{
								this.m_ValueRepository = new List<string>
								{
									"未知版本"
								};
							}
						}
						List<string> list3 = Enumerable.ToList<string>(Enumerable.Select<JToken, string>(Data["loaders"], (ModComp.CompFile._Closure$__.$I19-15 == null) ? (ModComp.CompFile._Closure$__.$I19-15 = ((JToken v) => v.ToString())) : ModComp.CompFile._Closure$__.$I19-15));
						this._VisitorRepository = new List<ModComp.CompModLoaderType>();
						if (list3.Contains("forge"))
						{
							this._VisitorRepository.Add(ModComp.CompModLoaderType.Forge);
						}
						if (list3.Contains("neoforge"))
						{
							this._VisitorRepository.Add(ModComp.CompModLoaderType.NeoForge);
						}
						if (list3.Contains("fabric"))
						{
							this._VisitorRepository.Add(ModComp.CompModLoaderType.Fabric);
						}
						if (list3.Contains("quilt"))
						{
							this._VisitorRepository.Add(ModComp.CompModLoaderType.Quilt);
						}
					}
				}
			}

			// Token: 0x06000229 RID: 553 RVA: 0x00019D50 File Offset: 0x00017F50
			public JObject ToJson()
			{
				JObject jobject = new JObject();
				jobject.Add("FromCurseForge", this.singletonRepository);
				jobject.Add("Id", this.m_RegRepository);
				jobject.Add("DisplayName", this._ProductRepository);
				jobject.Add("ReleaseDate", this._ListenerRepository);
				jobject.Add("DownloadCount", this._CollectionRepository);
				jobject.Add("ModLoaders", new JArray(Enumerable.Select<ModComp.CompModLoaderType, int>(this._VisitorRepository, (ModComp.CompFile._Closure$__.$I20-0 == null) ? (ModComp.CompFile._Closure$__.$I20-0 = ((ModComp.CompModLoaderType m) => (int)m)) : ModComp.CompFile._Closure$__.$I20-0)));
				jobject.Add("GameVersions", new JArray(this.m_ValueRepository));
				jobject.Add("Status", (int)this.m_ObjectRepository);
				if (this._BridgeRepository != null)
				{
					jobject.Add("FileName", this._BridgeRepository);
				}
				if (this._ItemRepository != null)
				{
					jobject.Add("DownloadUrls", new JArray(this._ItemRepository));
				}
				if (this.m_ReponseRepository != null)
				{
					jobject.Add("Hash", this.m_ReponseRepository);
				}
				jobject.Add("RawDependencies", new JArray(this.m_GlobalRepository));
				jobject.Add("Dependencies", new JArray(this.exceptionRepository));
				return jobject;
			}

			// Token: 0x0600022A RID: 554 RVA: 0x00019EC4 File Offset: 0x000180C4
			public MyListItem ToListItem(MyListItem.ClickEventHandler OnClick, MyIconButton.ClickEventHandler OnSaveClick = null, bool BadDisplayName = false)
			{
				string text = BadDisplayName ? this._BridgeRepository : this._ProductRepository;
				List<string> list = new List<string>();
				if (Operators.CompareString(text, this._BridgeRepository.BeforeLast(".", false), false) != 0)
				{
					list.Add(this._BridgeRepository.BeforeLast(".", false));
				}
				ModComp.CompType type = this.Type;
				if (type != ModComp.CompType.Mod)
				{
					if (type == ModComp.CompType.ModPack)
					{
						if (Enumerable.All<string>(this.m_ValueRepository, (ModComp.CompFile._Closure$__.$I21-0 == null) ? (ModComp.CompFile._Closure$__.$I21-0 = ((string v) => v.Contains("w"))) : ModComp.CompFile._Closure$__.$I21-0))
						{
							list.Add(string.Format("游戏版本 {0}", this.m_ValueRepository.Join("、")));
						}
					}
				}
				else if (Enumerable.Any<string>(this.exceptionRepository))
				{
					list.Add(Conversions.ToString(this.exceptionRepository.Count) + " 个前置 Mod");
				}
				if (this._CollectionRepository > 0)
				{
					list.Add("下载 " + ((this._CollectionRepository > 100000) ? (Conversions.ToString(Math.Round((double)this._CollectionRepository / 10000.0)) + " 万次") : (Conversions.ToString(this._CollectionRepository) + " 次")));
				}
				list.Add("更新于 " + ModBase.GetTimeSpanString(this._ListenerRepository - DateTime.Now, false));
				if (this.m_ObjectRepository != ModComp.CompFileStatus.Release)
				{
					list.Add(this.CountTests());
				}
				MyListItem myListItem = new MyListItem
				{
					Title = text,
					SnapsToDevicePixels = true,
					Height = 42.0,
					Type = MyListItem.CheckType.Clickable,
					Tag = this,
					Info = list.Join("，")
				};
				ModComp.CompFileStatus objectRepository = this.m_ObjectRepository;
				if (objectRepository != ModComp.CompFileStatus.Release)
				{
					if (objectRepository != ModComp.CompFileStatus.Beta)
					{
						myListItem.Logo = ModBase.m_SerializerRepository + "Icons/A.png";
					}
					else
					{
						myListItem.Logo = ModBase.m_SerializerRepository + "Icons/B.png";
					}
				}
				else
				{
					myListItem.Logo = ModBase.m_SerializerRepository + "Icons/R.png";
				}
				myListItem.Click += OnClick;
				if (OnSaveClick != null)
				{
					MyIconButton myIconButton = new MyIconButton
					{
						Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
						ToolTip = "另存为"
					};
					ToolTipService.SetPlacement(myIconButton, PlacementMode.Center);
					ToolTipService.SetVerticalOffset(myIconButton, 30.0);
					ToolTipService.SetHorizontalOffset(myIconButton, 2.0);
					myIconButton.Click += OnSaveClick;
					myListItem.Buttons = new MyIconButton[]
					{
						myIconButton
					};
				}
				return myListItem;
			}

			// Token: 0x0600022B RID: 555 RVA: 0x000034CC File Offset: 0x000016CC
			public override string ToString()
			{
				return string.Format("{0}: {1}", this.m_RegRepository, this._BridgeRepository);
			}

			// Token: 0x04000126 RID: 294
			public readonly ModComp.CompType Type;

			// Token: 0x04000127 RID: 295
			public readonly bool singletonRepository;

			// Token: 0x04000128 RID: 296
			public readonly string m_RegRepository;

			// Token: 0x04000129 RID: 297
			public string _ProductRepository;

			// Token: 0x0400012A RID: 298
			public readonly DateTime _ListenerRepository;

			// Token: 0x0400012B RID: 299
			public readonly int _CollectionRepository;

			// Token: 0x0400012C RID: 300
			public readonly List<ModComp.CompModLoaderType> _VisitorRepository;

			// Token: 0x0400012D RID: 301
			public readonly List<string> m_ValueRepository;

			// Token: 0x0400012E RID: 302
			public readonly ModComp.CompFileStatus m_ObjectRepository;

			// Token: 0x0400012F RID: 303
			public readonly string _BridgeRepository;

			// Token: 0x04000130 RID: 304
			public List<string> _ItemRepository;

			// Token: 0x04000131 RID: 305
			public readonly string m_ReponseRepository;

			// Token: 0x04000132 RID: 306
			public readonly List<string> m_GlobalRepository;

			// Token: 0x04000133 RID: 307
			public readonly List<string> exceptionRepository;
		}
	}
}
