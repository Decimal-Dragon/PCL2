using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020000AF RID: 175
	public class CrashAnalyzer
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x0002AE74 File Offset: 0x00029074
		public CrashAnalyzer(int UUID)
		{
			this._ConfigField = new List<KeyValuePair<string, string[]>>();
			this.testsField = null;
			this.m_MapperField = null;
			this._ThreadField = null;
			this.propertyField = null;
			this._ComposerField = null;
			this.m_RepositoryField = new Dictionary<CrashAnalyzer.CrashReason, List<string>>();
			this.m_ErrorField = new List<string>();
			object readerField = CrashAnalyzer.m_ReaderField;
			ObjectFlowControl.CheckForSyncLockOnValueType(readerField);
			lock (readerField)
			{
				if (!CrashAnalyzer.fieldField)
				{
					try
					{
						ModBase.DeleteDirectory(ModBase.m_DecoratorRepository + "CrashAnalyzer\\", false);
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "清理崩溃分析缓存失败", ModBase.LogLevel.Debug, "出现错误");
					}
					CrashAnalyzer.fieldField = true;
				}
			}
			this.clientField = string.Concat(new string[]
			{
				ModBase.m_DecoratorRepository,
				"CrashAnalyzer\\",
				Conversions.ToString(UUID),
				Conversions.ToString(ModBase.RandomInteger(0, 99999999)),
				"\\"
			});
			ModBase.DeleteDirectory(this.clientField, false);
			Directory.CreateDirectory(this.clientField + "Temp\\");
			Directory.CreateDirectory(this.clientField + "Report\\");
			ModBase.Log("[Crash] 崩溃分析暂存文件夹：" + this.clientField, ModBase.LogLevel.Normal, "出现错误");
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0002AFEC File Offset: 0x000291EC
		public void Collect(string VersionPathIndie, IList<string> LatestLog = null)
		{
			ModBase.Log("[Crash] 步骤 1：收集日志文件", ModBase.LogLevel.Normal, "出现错误");
			List<string> list = new List<string>();
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(VersionPathIndie + "crash-reports\\");
				if (directoryInfo.Exists)
				{
					try
					{
						foreach (FileInfo fileInfo in directoryInfo.EnumerateFiles())
						{
							list.Add(fileInfo.FullName);
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
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "收集 Minecraft 崩溃日志文件夹下的日志失败", ModBase.LogLevel.Debug, "出现错误");
			}
			try
			{
				try
				{
					foreach (FileInfo fileInfo2 in new DirectoryInfo(VersionPathIndie).Parent.Parent.EnumerateFiles())
					{
						if (Operators.CompareString(fileInfo2.Extension ?? "", ".log", false) == 0)
						{
							list.Add(fileInfo2.FullName);
						}
					}
				}
				finally
				{
					IEnumerator<FileInfo> enumerator2;
					if (enumerator2 != null)
					{
						enumerator2.Dispose();
					}
				}
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, "收集 Minecraft 主文件夹下的日志失败", ModBase.LogLevel.Debug, "出现错误");
			}
			try
			{
				try
				{
					foreach (FileInfo fileInfo3 in new DirectoryInfo(VersionPathIndie).EnumerateFiles())
					{
						if (Operators.CompareString(fileInfo3.Extension ?? "", ".log", false) == 0)
						{
							list.Add(fileInfo3.FullName);
						}
					}
				}
				finally
				{
					IEnumerator<FileInfo> enumerator3;
					if (enumerator3 != null)
					{
						enumerator3.Dispose();
					}
				}
			}
			catch (Exception ex3)
			{
				ModBase.Log(ex3, "收集 Minecraft 隔离文件夹下的日志失败", ModBase.LogLevel.Debug, "出现错误");
			}
			list.Add(VersionPathIndie + "logs\\latest.log");
			list.Add(VersionPathIndie + "logs\\debug.log");
			list = Enumerable.ToList<string>(Enumerable.Distinct<string>(list));
			List<string> list2 = new List<string>();
			try
			{
				foreach (string text in list)
				{
					try
					{
						FileInfo fileInfo4 = new FileInfo(text);
						if (fileInfo4.Exists)
						{
							double num = Math.Abs((fileInfo4.LastWriteTime - DateTime.Now).TotalMinutes);
							if (num < 3.0 && fileInfo4.Length > 0L)
							{
								list2.Add(text);
								ModBase.Log(string.Concat(new string[]
								{
									"[Crash] 可能可用的日志文件：",
									text,
									"（",
									Conversions.ToString(Math.Round(num, 1)),
									" 分钟）"
								}), ModBase.LogLevel.Normal, "出现错误");
							}
						}
					}
					catch (Exception ex4)
					{
						ModBase.Log(ex4, "确认崩溃日志时间失败（" + text + "）", ModBase.LogLevel.Debug, "出现错误");
					}
				}
			}
			finally
			{
				List<string>.Enumerator enumerator4;
				((IDisposable)enumerator4).Dispose();
			}
			if (!Enumerable.Any<string>(list2))
			{
				ModBase.Log("[Crash] 未发现可能可用的日志文件", ModBase.LogLevel.Normal, "出现错误");
			}
			try
			{
				foreach (string text2 in list2)
				{
					try
					{
						if (text2.Contains("crash-"))
						{
							this._ConfigField.Add(new KeyValuePair<string, string[]>(text2, ModBase.ReadFile(text2, null).Split("\r\n".ToCharArray())));
						}
						else
						{
							this._ConfigField.Add(new KeyValuePair<string, string[]>(text2, ModBase.ReadFile(text2, Encoding.UTF8).Split("\r\n".ToCharArray())));
						}
					}
					catch (Exception ex5)
					{
						ModBase.Log(ex5, "读取可能的崩溃日志文件失败（" + text2 + "）", ModBase.LogLevel.Debug, "出现错误");
					}
				}
			}
			finally
			{
				List<string>.Enumerator enumerator5;
				((IDisposable)enumerator5).Dispose();
			}
			if (LatestLog != null && Enumerable.Any<string>(LatestLog))
			{
				string text3 = LatestLog.Join("\r\n");
				ModBase.Log("[Crash] 以下为游戏输出的最后一段内容：\r\n" + text3, ModBase.LogLevel.Normal, "出现错误");
				ModBase.WriteFile(this.clientField + "RawOutput.log", text3, false, null);
				this._ConfigField.Add(new KeyValuePair<string, string[]>(this.clientField + "RawOutput.log", Enumerable.ToArray<string>(LatestLog)));
				LatestLog.Clear();
			}
			ModBase.Log("[Crash] 步骤 1：收集日志文件完成，收集到 " + Conversions.ToString(this._ConfigField.Count) + " 个文件", ModBase.LogLevel.Normal, "出现错误");
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0002B4C4 File Offset: 0x000296C4
		public void Import(string FilePath)
		{
			ModBase.Log("[Crash] 步骤 1：自主导入日志文件", ModBase.LogLevel.Normal, "出现错误");
			try
			{
				FileInfo fileInfo = new FileInfo(FilePath);
				if (fileInfo.Exists && fileInfo.Length > 0L && !FilePath.EndsWithF(".jar", true))
				{
					ModBase.ExtractFile(FilePath, this.clientField + "Temp\\", null, null);
					ModBase.Log("[Crash] 已解压导入的日志文件：" + FilePath, ModBase.LogLevel.Normal, "出现错误");
					goto IL_AE;
				}
			}
			catch (Exception ex)
			{
			}
			ModBase.CopyFile(FilePath, this.clientField + "Temp\\" + ModBase.GetFileNameFromPath(FilePath));
			ModBase.Log("[Crash] 已复制导入的日志文件：" + FilePath, ModBase.LogLevel.Normal, "出现错误");
			IL_AE:
			try
			{
				foreach (FileInfo fileInfo2 in Enumerable.ToList<FileInfo>(new DirectoryInfo(this.clientField + "Temp\\").EnumerateFiles()))
				{
					try
					{
						if (fileInfo2.Exists && fileInfo2.Length != 0L)
						{
							string left = fileInfo2.Extension.ToLower();
							if (Operators.CompareString(left, ".log", false) != 0 && Operators.CompareString(left, ".txt", false) != 0)
							{
								File.Delete(fileInfo2.FullName);
							}
							else if (fileInfo2.Name.StartsWithF("crash-", false))
							{
								this._ConfigField.Add(new KeyValuePair<string, string[]>(fileInfo2.FullName, ModBase.ReadFile(fileInfo2.FullName, null).Split("\r\n".ToCharArray())));
							}
							else
							{
								this._ConfigField.Add(new KeyValuePair<string, string[]>(fileInfo2.FullName, ModBase.ReadFile(fileInfo2.FullName, Encoding.UTF8).Split("\r\n".ToCharArray())));
							}
						}
					}
					catch (Exception ex2)
					{
						ModBase.Log(ex2, "导入单个日志文件失败", ModBase.LogLevel.Debug, "出现错误");
					}
				}
			}
			finally
			{
				List<FileInfo>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			ModBase.Log("[Crash] 步骤 1：自主导入日志文件，收集到 " + Conversions.ToString(this._ConfigField.Count) + " 个文件", ModBase.LogLevel.Normal, "出现错误");
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0002B730 File Offset: 0x00029930
		public int Prepare()
		{
			ModBase.Log("[Crash] 步骤 2：准备日志文本", ModBase.LogLevel.Normal, "出现错误");
			this.testsField = null;
			List<KeyValuePair<CrashAnalyzer.AnalyzeFileType, KeyValuePair<string, string[]>>> list = new List<KeyValuePair<CrashAnalyzer.AnalyzeFileType, KeyValuePair<string, string[]>>>();
			try
			{
				foreach (KeyValuePair<string, string[]> value in this._ConfigField)
				{
					string text = ModBase.GetFileNameFromPath(value.Key).ToLower();
					CrashAnalyzer.AnalyzeFileType analyzeFileType;
					if (text.StartsWithF("hs_err", false))
					{
						analyzeFileType = CrashAnalyzer.AnalyzeFileType.HsErr;
						this.testsField = new KeyValuePair<string, string[]>?(value);
					}
					else if (text.StartsWithF("crash-", false))
					{
						analyzeFileType = CrashAnalyzer.AnalyzeFileType.CrashReport;
						this.testsField = new KeyValuePair<string, string[]>?(value);
					}
					else if (Operators.CompareString(text, "latest.log", false) != 0 && Operators.CompareString(text, "latest log.txt", false) != 0 && Operators.CompareString(text, "debug.log", false) != 0 && Operators.CompareString(text, "debug log.txt", false) != 0 && Operators.CompareString(text, "游戏崩溃前的输出.txt", false) != 0 && Operators.CompareString(text, "rawoutput.log", false) != 0)
					{
						if (Operators.CompareString(text, "启动器日志.txt", false) != 0 && Operators.CompareString(text, "PCL2 启动器日志.txt", false) != 0 && Operators.CompareString(text, "PCL 启动器日志.txt", false) != 0 && Operators.CompareString(text, "log1.txt", false) != 0)
						{
							if (!text.EndsWithF(".log", true) && !text.EndsWithF(".txt", true))
							{
								ModBase.Log("[Crash] " + text + " 分类为 Ignore", ModBase.LogLevel.Normal, "出现错误");
								continue;
							}
							analyzeFileType = CrashAnalyzer.AnalyzeFileType.ExtraLog;
						}
						else if (Enumerable.Any<string>(value.Value, (CrashAnalyzer._Closure$__.$I10-0 == null) ? (CrashAnalyzer._Closure$__.$I10-0 = ((string s) => s.Contains("以下为游戏输出的最后一段内容"))) : CrashAnalyzer._Closure$__.$I10-0))
						{
							analyzeFileType = CrashAnalyzer.AnalyzeFileType.MinecraftLog;
							if (this.testsField == null)
							{
								this.testsField = new KeyValuePair<string, string[]>?(value);
							}
						}
						else
						{
							analyzeFileType = CrashAnalyzer.AnalyzeFileType.ExtraLog;
						}
					}
					else
					{
						analyzeFileType = CrashAnalyzer.AnalyzeFileType.MinecraftLog;
						if (this.testsField == null)
						{
							this.testsField = new KeyValuePair<string, string[]>?(value);
						}
					}
					if (!Enumerable.Any<string>(value.Value))
					{
						ModBase.Log("[Crash] " + text + " 由于内容为空跳过", ModBase.LogLevel.Normal, "出现错误");
					}
					else
					{
						list.Add(new KeyValuePair<CrashAnalyzer.AnalyzeFileType, KeyValuePair<string, string[]>>(analyzeFileType, value));
						ModBase.Log("[Crash] " + text + " 分类为 " + ModBase.GetStringFromEnum(analyzeFileType), ModBase.LogLevel.Normal, "出现错误");
					}
				}
			}
			finally
			{
				List<KeyValuePair<string, string[]>>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			checked
			{
				foreach (CrashAnalyzer.AnalyzeFileType analyzeFileType2 in new CrashAnalyzer.AnalyzeFileType[]
				{
					CrashAnalyzer.AnalyzeFileType.MinecraftLog,
					CrashAnalyzer.AnalyzeFileType.HsErr,
					CrashAnalyzer.AnalyzeFileType.ExtraLog,
					CrashAnalyzer.AnalyzeFileType.CrashReport
				})
				{
					List<KeyValuePair<string, string[]>> list2 = new List<KeyValuePair<string, string[]>>();
					try
					{
						foreach (KeyValuePair<CrashAnalyzer.AnalyzeFileType, KeyValuePair<string, string[]>> keyValuePair in list)
						{
							if (analyzeFileType2 == keyValuePair.Key)
							{
								list2.Add(keyValuePair.Value);
							}
						}
					}
					finally
					{
						List<KeyValuePair<CrashAnalyzer.AnalyzeFileType, KeyValuePair<string, string[]>>>.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
					if (Enumerable.Any<KeyValuePair<string, string[]>>(list2))
					{
						try
						{
							switch (analyzeFileType2)
							{
							case CrashAnalyzer.AnalyzeFileType.HsErr:
							case CrashAnalyzer.AnalyzeFileType.CrashReport:
								break;
							case CrashAnalyzer.AnalyzeFileType.MinecraftLog:
							{
								this.m_MapperField = "";
								this._ThreadField = "";
								Dictionary<string, KeyValuePair<string, string[]>> dictionary = new Dictionary<string, KeyValuePair<string, string[]>>();
								try
								{
									foreach (KeyValuePair<string, string[]> value2 in list2)
									{
										dictionary[ModBase.GetFileNameFromPath(value2.Key).ToLower()] = value2;
										this.m_ErrorField.Add(value2.Key);
										ModBase.Log("[Crash] 输出报告：" + value2.Key + "，作为 Minecraft 或启动器日志", ModBase.LogLevel.Normal, "出现错误");
									}
								}
								finally
								{
									List<KeyValuePair<string, string[]>>.Enumerator enumerator3;
									((IDisposable)enumerator3).Dispose();
								}
								foreach (string key in new string[]
								{
									"rawoutput.log",
									"启动器日志.txt",
									"log1.txt",
									"游戏崩溃前的输出.txt",
									"PCL2 启动器日志.txt",
									"PCL 启动器日志.txt"
								})
								{
									if (dictionary.ContainsKey(key))
									{
										KeyValuePair<string, string[]> keyValuePair2 = dictionary[key];
										bool flag = false;
										foreach (string text2 in keyValuePair2.Value)
										{
											if (flag)
											{
												ref string ptr = ref this.m_MapperField;
												this.m_MapperField = ptr + text2 + "\n";
											}
											else if (text2.Contains("以下为游戏输出的最后一段内容"))
											{
												flag = true;
												ModBase.Log("[Crash] 找到 PCL 输出的游戏实时日志头", ModBase.LogLevel.Normal, "出现错误");
											}
										}
										if (!flag)
										{
											ref string ptr = ref this.m_MapperField;
											this.m_MapperField = ptr + this.GetHeadTailLines(keyValuePair2.Value, 0, 500);
										}
										this.m_MapperField = this.m_MapperField.TrimEnd("\r\n".ToCharArray());
										ModBase.Log("[Crash] 导入分析：" + keyValuePair2.Key + "，作为启动器日志", ModBase.LogLevel.Normal, "出现错误");
										IL_4DD:
										foreach (string key2 in new string[]
										{
											"latest.log",
											"latest log.txt",
											"debug.log",
											"debug log.txt"
										})
										{
											if (dictionary.ContainsKey(key2))
											{
												KeyValuePair<string, string[]> keyValuePair3 = dictionary[key2];
												ref string ptr = ref this.m_MapperField;
												this.m_MapperField = ptr + this.GetHeadTailLines(keyValuePair3.Value, 250, 500);
												ModBase.Log("[Crash] 导入分析：" + keyValuePair3.Key + "，作为 Minecraft 日志", ModBase.LogLevel.Normal, "出现错误");
												IL_582:
												string[] array4 = new string[]
												{
													"debug.log",
													"debug log.txt"
												};
												int m = 0;
												while (m < array4.Length)
												{
													string key3 = array4[m];
													if (!dictionary.ContainsKey(key3))
													{
														m++;
													}
													else
													{
														KeyValuePair<string, string[]> keyValuePair4 = dictionary[key3];
														ptr = ref this._ThreadField;
														this._ThreadField = ptr + this.GetHeadTailLines(keyValuePair4.Value, 1000, 0);
														ModBase.Log("[Crash] 导入分析：" + keyValuePair4.Key + "，作为 Minecraft Debug 日志", ModBase.LogLevel.Normal, "出现错误");
														IL_613:
														if (Operators.CompareString(this.m_MapperField, "", false) == 0)
														{
															this.m_MapperField = null;
															throw new Exception("无法找到匹配的 Minecraft Log");
														}
														if (Operators.CompareString(this._ThreadField, "", false) == 0)
														{
															this._ThreadField = null;
															goto IL_829;
														}
														goto IL_829;
													}
												}
												goto IL_613;
											}
										}
										goto IL_582;
									}
								}
								goto IL_4DD;
							}
							case CrashAnalyzer.AnalyzeFileType.ExtraLog:
								try
								{
									foreach (KeyValuePair<string, string[]> keyValuePair5 in list2)
									{
										this.m_ErrorField.Add(keyValuePair5.Key);
										ModBase.Log("[Crash] 输出报告：" + keyValuePair5.Key + "，作为额外日志", ModBase.LogLevel.Normal, "出现错误");
									}
									goto IL_829;
								}
								finally
								{
									List<KeyValuePair<string, string[]>>.Enumerator enumerator4;
									((IDisposable)enumerator4).Dispose();
								}
								break;
							default:
								goto IL_829;
							}
							SortedList<DateTime, KeyValuePair<string, string[]>> sortedList = new SortedList<DateTime, KeyValuePair<string, string[]>>();
							try
							{
								foreach (KeyValuePair<string, string[]> keyValuePair6 in list2)
								{
									try
									{
										sortedList.Add(new FileInfo(keyValuePair6.Key).LastWriteTime, keyValuePair6);
									}
									catch (Exception ex)
									{
										ModBase.Log(ex, "获取日志文件修改时间失败", ModBase.LogLevel.Debug, "出现错误");
										sortedList.Add(new DateTime(1900, 1, 1), keyValuePair6);
									}
								}
							}
							finally
							{
								List<KeyValuePair<string, string[]>>.Enumerator enumerator5;
								((IDisposable)enumerator5).Dispose();
							}
							KeyValuePair<string, string[]> value4 = Enumerable.Last<KeyValuePair<DateTime, KeyValuePair<string, string[]>>>(sortedList).Value;
							this.m_ErrorField.Add(value4.Key);
							if (analyzeFileType2 == CrashAnalyzer.AnalyzeFileType.HsErr)
							{
								this.propertyField = this.GetHeadTailLines(value4.Value, 200, 100);
								ModBase.Log("[Crash] 输出报告：" + value4.Key + "，作为虚拟机错误信息", ModBase.LogLevel.Normal, "出现错误");
								ModBase.Log("[Crash] 导入分析：" + value4.Key + "，作为虚拟机错误信息", ModBase.LogLevel.Normal, "出现错误");
							}
							else
							{
								this._ComposerField = this.GetHeadTailLines(value4.Value, 300, 700);
								ModBase.Log("[Crash] 输出报告：" + value4.Key + "，作为 Minecraft 崩溃报告", ModBase.LogLevel.Normal, "出现错误");
								ModBase.Log("[Crash] 导入分析：" + value4.Key + "，作为 Minecraft 崩溃报告", ModBase.LogLevel.Normal, "出现错误");
							}
							IL_829:;
						}
						catch (Exception ex2)
						{
							ModBase.Log(ex2, "分类处理日志文件时出错", ModBase.LogLevel.Debug, "出现错误");
						}
					}
				}
				int num = ((this.m_MapperField != null) + (this.propertyField != null) + (this._ComposerField != null)) ? 1 : 0;
				if (num == 0)
				{
					ModBase.Log("[Crash] 步骤 2：准备日志文本完成，没有任何可供分析的日志", ModBase.LogLevel.Normal, "出现错误");
				}
				else
				{
					ModBase.Log(string.Concat(new string[]
					{
						"[Crash] 步骤 2：准备日志文本完成，找到",
						(this.m_MapperField == null) ? "" : "游戏日志、",
						(this._ThreadField == null) ? "" : "游戏 Debug 日志、",
						(this.propertyField == null) ? "" : "虚拟机日志、",
						(this._ComposerField == null) ? "" : "崩溃日志、"
					}).TrimEnd(new char[]
					{
						'、'
					}) + "用作分析", ModBase.LogLevel.Normal, "出现错误");
				}
				return num;
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0002C110 File Offset: 0x0002A310
		private string GetHeadTailLines(string[] Raw, int HeadLines, int TailLines)
		{
			checked
			{
				string result;
				if (Raw.Length <= HeadLines + TailLines)
				{
					result = Enumerable.Distinct<string>(Raw).Join("\n");
				}
				else
				{
					List<string> list = new List<string>();
					int num = 0;
					int num2 = Raw.Length - 1;
					int i;
					for (i = 0; i <= num2; i++)
					{
						if (!list.Contains(Raw[i]))
						{
							num++;
							list.Add(Raw[i]);
							if (num >= HeadLines)
							{
								break;
							}
						}
					}
					int num3 = 0;
					int num4 = Raw.Length - 1;
					int num5 = i;
					for (int j = num4; j >= num5; j += -1)
					{
						if (!list.Contains(Raw[j]))
						{
							num3++;
							list.Insert(num, Raw[j]);
							if (num3 >= TailLines)
							{
								break;
							}
						}
					}
					StringBuilder stringBuilder = new StringBuilder();
					try
					{
						foreach (string text in list)
						{
							if (Operators.CompareString(text, "", false) != 0)
							{
								stringBuilder.Append(text);
								stringBuilder.Append("\n");
							}
						}
					}
					finally
					{
						List<string>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					result = stringBuilder.ToString();
				}
				return result;
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0002C224 File Offset: 0x0002A424
		public void Analyze(ModMinecraft.McVersion Version = null)
		{
			ModBase.Log("[Crash] 步骤 3：分析崩溃原因", ModBase.LogLevel.Normal, "出现错误");
			this._IteratorField = this.m_MapperField + this._ThreadField + this.propertyField + this._ComposerField;
			this.AnalyzeCrit1();
			if (!Enumerable.Any<KeyValuePair<CrashAnalyzer.CrashReason, List<string>>>(this.m_RepositoryField))
			{
				this.AnalyzeCrit2();
				if (!Enumerable.Any<KeyValuePair<CrashAnalyzer.CrashReason, List<string>>>(this.m_RepositoryField))
				{
					if (!this._IteratorField.Contains("orge") && !this._IteratorField.Contains("abric") && !this._IteratorField.Contains("uilt") && !this._IteratorField.Contains("iteloader"))
					{
						ModBase.Log("[Crash] 可能并未安装 Mod，不进行堆栈分析", ModBase.LogLevel.Normal, "出现错误");
					}
					else
					{
						List<string> list = new List<string>();
						if (this._ComposerField != null)
						{
							ModBase.Log("[Crash] 开始进行崩溃日志堆栈分析", ModBase.LogLevel.Normal, "出现错误");
							list.AddRange(this.AnalyzeStackKeyword(this._ComposerField.BeforeFirst("System Details", false)));
						}
						if (this.m_MapperField != null)
						{
							List<string> list2 = ModBase.RegexSearch(this.m_MapperField, "/FATAL] .+?(?=[\\n]+\\[)", 0);
							if (this.m_MapperField.Contains("Unreported exception thrown!"))
							{
								list2.Add(this.m_MapperField.Between("Unreported exception thrown!", "at oolloo.jlw.Wrapper", false));
							}
							ModBase.Log("[Crash] 开始进行 Minecraft 日志堆栈分析，发现 " + Conversions.ToString(list2.Count) + " 个报错项", ModBase.LogLevel.Normal, "出现错误");
							try
							{
								foreach (string errorStack in list2)
								{
									list.AddRange(this.AnalyzeStackKeyword(errorStack));
								}
							}
							finally
							{
								List<string>.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
						}
						if (this.propertyField != null)
						{
							ModBase.Log("[Crash] 开始进行虚拟机堆栈分析", ModBase.LogLevel.Normal, "出现错误");
							string errorStack2 = this.propertyField.Between("T H R E A D", "Registers:", false);
							list.AddRange(this.AnalyzeStackKeyword(errorStack2));
						}
						if (Enumerable.Any<string>(list))
						{
							List<string> list3 = this.AnalyzeModName(list);
							if (list3 == null)
							{
								this.AppendReason(CrashAnalyzer.CrashReason.堆栈分析发现关键字, list);
								goto IL_20E;
							}
							this.AppendReason(CrashAnalyzer.CrashReason.堆栈分析发现Mod名称, list3);
							goto IL_20E;
						}
					}
					this.AnalyzeCrit3();
				}
			}
			IL_20E:
			if (!Enumerable.Any<KeyValuePair<CrashAnalyzer.CrashReason, List<string>>>(this.m_RepositoryField))
			{
				ModBase.Log("[Crash] 步骤 3：分析崩溃原因完成，未找到可能的原因", ModBase.LogLevel.Normal, "出现错误");
				return;
			}
			ModBase.Log("[Crash] 步骤 3：分析崩溃原因完成，找到 " + Conversions.ToString(this.m_RepositoryField.Count) + " 条可能的原因", ModBase.LogLevel.Normal, "出现错误");
			try
			{
				foreach (KeyValuePair<CrashAnalyzer.CrashReason, List<string>> keyValuePair in this.m_RepositoryField)
				{
					ModBase.Log("[Crash]  - " + ModBase.GetStringFromEnum(keyValuePair.Key) + (Enumerable.Any<string>(keyValuePair.Value) ? ("（" + keyValuePair.Value.Join("；") + "）") : ""), ModBase.LogLevel.Normal, "出现错误");
				}
			}
			finally
			{
				Dictionary<CrashAnalyzer.CrashReason, List<string>>.Enumerator enumerator2;
				((IDisposable)enumerator2).Dispose();
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0002C530 File Offset: 0x0002A730
		private void AppendReason(CrashAnalyzer.CrashReason Reason, ICollection<string> Additional = null)
		{
			if (this.m_RepositoryField.ContainsKey(Reason))
			{
				if (Additional != null)
				{
					this.m_RepositoryField[Reason].AddRange(Additional);
					this.m_RepositoryField[Reason] = Enumerable.ToList<string>(Enumerable.Distinct<string>(this.m_RepositoryField[Reason]));
				}
			}
			else
			{
				this.m_RepositoryField.Add(Reason, new List<string>(Additional ?? new string[0]));
			}
			ModBase.Log("[Crash] 可能的崩溃原因：" + ModBase.GetStringFromEnum(Reason) + ((Additional == null || !Enumerable.Any<string>(Additional)) ? "" : ("（" + Additional.Join("；") + "）")), ModBase.LogLevel.Normal, "出现错误");
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00004DF6 File Offset: 0x00002FF6
		private void AppendReason(CrashAnalyzer.CrashReason Reason, string Additional)
		{
			List<string> additional;
			if (!string.IsNullOrEmpty(Additional))
			{
				(additional = new List<string>()).Add(Additional);
			}
			else
			{
				additional = null;
			}
			this.AppendReason(Reason, additional);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0002C5EC File Offset: 0x0002A7EC
		private void AnalyzeCrit1()
		{
			if (this.m_MapperField == null && this.propertyField == null && this._ComposerField == null)
			{
				this.AppendReason(CrashAnalyzer.CrashReason.没有可用的分析文件, null);
				return;
			}
			if (this._ComposerField != null && this._ComposerField.Contains("Unable to make protected final java.lang.Class java.lang.ClassLoader.defineClass"))
			{
				this.AppendReason(CrashAnalyzer.CrashReason.Java版本过高, null);
			}
			if (this.m_MapperField != null)
			{
				if (this.m_MapperField.Contains("Found multiple arguments for option fml.forgeVersion, but you asked for only one"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.版本Json中存在多个Forge, null);
				}
				if (this.m_MapperField.Contains("The driver does not appear to support OpenGL"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.显卡不支持OpenGL, null);
				}
				if (this.m_MapperField.Contains("java.lang.ClassCastException: java.base/jdk"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.使用JDK, null);
				}
				if (this.m_MapperField.Contains("java.lang.ClassCastException: class jdk."))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.使用JDK, null);
				}
				if (this.m_MapperField.Contains("TRANSFORMER/net.optifine/net.optifine.reflect.Reflector.<clinit>(Reflector.java"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.OptiFine与Forge不兼容, null);
				}
				if (this.m_MapperField.Contains("java.lang.NoSuchMethodError: 'void net.minecraft.client.renderer.texture.SpriteContents.<init>"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.OptiFine与Forge不兼容, null);
				}
				if (this.m_MapperField.Contains("java.lang.NoSuchMethodError: 'java.lang.String com.mojang.blaze3d.systems.RenderSystem.getBackendDescription"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.OptiFine与Forge不兼容, null);
				}
				if (this.m_MapperField.Contains("java.lang.NoSuchMethodError: 'void net.minecraft.client.renderer.block.model.BakedQuad.<init>"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.OptiFine与Forge不兼容, null);
				}
				if (this.m_MapperField.Contains("java.lang.NoSuchMethodError: 'void net.minecraftforge.client.gui.overlay.ForgeGui.renderSelectedItemName"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.OptiFine与Forge不兼容, null);
				}
				if (this.m_MapperField.Contains("java.lang.NoSuchMethodError: 'void net.minecraft.server.level.DistanceManager"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.OptiFine与Forge不兼容, null);
				}
				if (this.m_MapperField.Contains("java.lang.NoSuchMethodError: 'net.minecraft.network.chat.FormattedText net.minecraft.client.gui.Font.ellipsize"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.OptiFine与Forge不兼容, null);
				}
				if (this.m_MapperField.Contains("Open J9 is not supported") || this.m_MapperField.Contains("OpenJ9 is incompatible") || this.m_MapperField.Contains(".J9VMInternals."))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.使用OpenJ9, null);
				}
				if (this.m_MapperField.Contains("java.lang.NoSuchFieldException: ucp"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Java版本过高, null);
				}
				if (this.m_MapperField.Contains("because module java.base does not export"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Java版本过高, null);
				}
				if (this.m_MapperField.Contains("java.lang.ClassNotFoundException: jdk.nashorn.api.scripting.NashornScriptEngineFactory"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Java版本过高, null);
				}
				if (this.m_MapperField.Contains("java.lang.ClassNotFoundException: java.lang.invoke.LambdaMetafactory"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Java版本过高, null);
				}
				if (this.m_MapperField.Contains("The directories below appear to be extracted jar files. Fix this before you continue."))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Mod文件被解压, null);
				}
				if (this.m_MapperField.Contains("Extracted mod jars found, loading will NOT continue"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Mod文件被解压, null);
				}
				if (this.m_MapperField.Contains("java.lang.ClassNotFoundException: org.spongepowered.asm.launch.MixinTweaker"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.MixinBootstrap缺失, null);
				}
				if (this.m_MapperField.Contains("Couldn't set pixel format"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.显卡驱动不支持导致无法设置像素格式, null);
				}
				if (this.m_MapperField.Contains("java.lang.OutOfMemoryError") || this.m_MapperField.Contains("an out of memory error"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.内存不足, null);
				}
				if (this.m_MapperField.Contains("java.lang.RuntimeException: Shaders Mod detected. Please remove it, OptiFine has built-in support for shaders."))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.ShadersMod与OptiFine同时安装, null);
				}
				if (this.m_MapperField.Contains("java.lang.NoSuchMethodError: sun.security.util.ManifestEntryVerifier"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.低版本Forge与高版本Java不兼容, null);
				}
				if (this.m_MapperField.Contains("1282: Invalid operation"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.光影或资源包导致OpenGL1282错误, null);
				}
				if (this.m_MapperField.Contains("signer information does not match signer information of other classes in the same package"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.文件或内容校验失败, (ModBase.RegexSeek(this.m_MapperField, "(?<=class \")[^']+(?=\"'s signer information)", 0) ?? "").TrimEnd(new char[]
					{
						'\r'
					}));
				}
				if (this.m_MapperField.Contains("Maybe try a lower resolution resourcepack?"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.材质过大或显卡配置不足, null);
				}
				if (this.m_MapperField.Contains("java.lang.NoSuchMethodError: net.minecraft.world.server.ChunkManager$ProxyTicketManager.shouldForceTicks(J)Z") && this.m_MapperField.Contains("OptiFine"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.OptiFine导致无法加载世界, null);
				}
				if (this.m_MapperField.Contains("Unsupported class file major version"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Java版本不兼容, null);
				}
				if (this.m_MapperField.Contains("com.electronwill.nightconfig.core.io.ParsingException: Not enough data available"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.NightConfig的Bug, null);
				}
				if (this.m_MapperField.Contains("Cannot find launch target fmlclient, unable to launch"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Forge安装不完整, null);
				}
				if (this.m_MapperField.Contains("Invalid paths argument, contained no existing paths") && this.m_MapperField.Contains("libraries\\net\\minecraftforge\\fmlcore"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Forge安装不完整, null);
				}
				if (this.m_MapperField.Contains("Invalid module name: '' is not a Java identifier"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Mod名称包含特殊字符, null);
				}
				if (this.m_MapperField.Contains("has been compiled by a more recent version of the Java Runtime (class file version 55.0), this version of the Java Runtime only recognizes class file versions up to"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Mod需要Java11, null);
				}
				if (this.m_MapperField.Contains("java.lang.RuntimeException: java.lang.NoSuchMethodException: no such method: sun.misc.Unsafe.defineAnonymousClass(Class,byte[],Object[])Class/invokeVirtual"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Mod需要Java11, null);
				}
				if (this.m_MapperField.Contains("java.lang.IllegalArgumentException: The requested compatibility level JAVA_11 could not be set. Level is not supported by the active JRE or ASM version"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Mod需要Java11, null);
				}
				if (this.m_MapperField.Contains("Unsupported major.minor version"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Java版本不兼容, null);
				}
				if (this.m_MapperField.Contains("Invalid maximum heap size"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.使用32位Java导致JVM无法分配足够多的内存, null);
				}
				if (this.m_MapperField.Contains("Could not reserve enough space"))
				{
					if (this.m_MapperField.Contains("for 1048576KB object heap"))
					{
						this.AppendReason(CrashAnalyzer.CrashReason.使用32位Java导致JVM无法分配足够多的内存, null);
					}
					else
					{
						this.AppendReason(CrashAnalyzer.CrashReason.内存不足, null);
					}
				}
				if (this.m_MapperField.Contains("Caught exception from "))
				{
					CrashAnalyzer.CrashReason reason = CrashAnalyzer.CrashReason.确定Mod导致游戏崩溃;
					string text = ModBase.RegexSeek(this.m_MapperField, "(?<=Caught exception from )[^\\n]+?", 0);
					this.AppendReason(reason, this.TryAnalyzeModName((text != null) ? text.TrimEnd("\r\n ".ToCharArray()) : null));
				}
				if (this.m_MapperField.Contains("DuplicateModsFoundException"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Mod重复安装, ModBase.RegexSearch(this.m_MapperField, "(?<=\\n\\t[\\w]+ : [A-Z]{1}:[^\\n]+(/|\\\\))[^/\\\\\\n]+?.jar", 1));
				}
				if (this.m_MapperField.Contains("Found a duplicate mod"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Mod重复安装, ModBase.RegexSearch(ModBase.RegexSeek(this.m_MapperField, "Found a duplicate mod[^\\n]+", 0) ?? "", "[^\\\\/]+.jar", 1));
				}
				if (this.m_MapperField.Contains("Found duplicate mods"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Mod重复安装, Enumerable.ToList<string>(Enumerable.Distinct<string>(ModBase.RegexSearch(this.m_MapperField, "(?<=Mod ID: ')\\w+?(?=' from mod files:)", 0))));
				}
				if (this.m_MapperField.Contains("ModResolutionException: Duplicate"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Mod重复安装, ModBase.RegexSearch(ModBase.RegexSeek(this.m_MapperField, "ModResolutionException: Duplicate[^\\n]+", 0) ?? "", "[^\\\\/]+.jar", 1));
				}
				if (this.m_MapperField.Contains("Incompatible mods found!"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Mod互不兼容, ModBase.RegexSeek(this.m_MapperField, "(?<=Incompatible mods found![\\s\\S]+: )[\\s\\S]+?(?=\\tat )", 0) ?? "");
				}
				if (this.m_MapperField.Contains("Missing or unsupported mandatory dependencies:"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Mod缺少前置或MC版本错误, Enumerable.ToList<string>(Enumerable.Distinct<string>(Enumerable.Select<string, string>(ModBase.RegexSearch(this.m_MapperField, "(?<=Missing or unsupported mandatory dependencies:)([\\n\\r]+\\t(.*))+", 1), (CrashAnalyzer._Closure$__.$I22-0 == null) ? (CrashAnalyzer._Closure$__.$I22-0 = ((string s) => s.Trim("\r\n\t ".ToCharArray()))) : CrashAnalyzer._Closure$__.$I22-0))));
				}
			}
			if (this.propertyField != null)
			{
				if (this.propertyField.Contains("The system is out of physical RAM or swap space"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.内存不足, null);
				}
				if (this.propertyField.Contains("Out of Memory Error"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.内存不足, null);
				}
				if (this.propertyField.Contains("EXCEPTION_ACCESS_VIOLATION"))
				{
					if (this.propertyField.Contains("# C  [ig"))
					{
						this.AppendReason(CrashAnalyzer.CrashReason.Intel驱动不兼容导致EXCEPTION_ACCESS_VIOLATION, null);
					}
					if (this.propertyField.Contains("# C  [atio"))
					{
						this.AppendReason(CrashAnalyzer.CrashReason.AMD驱动不兼容导致EXCEPTION_ACCESS_VIOLATION, null);
					}
					if (this.propertyField.Contains("# C  [nvoglv"))
					{
						this.AppendReason(CrashAnalyzer.CrashReason.Nvidia驱动不兼容导致EXCEPTION_ACCESS_VIOLATION, null);
					}
				}
			}
			if (this._ComposerField != null)
			{
				if (this._ComposerField.Contains("maximum id range exceeded"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Mod过多导致超出ID限制, null);
				}
				if (this._ComposerField.Contains("java.lang.OutOfMemoryError"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.内存不足, null);
				}
				if (this._ComposerField.Contains("Pixel format not accelerated"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.显卡驱动不支持导致无法设置像素格式, null);
				}
				if (this._ComposerField.Contains("Manually triggered debug crash"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.玩家手动触发调试崩溃, null);
				}
				if (this._ComposerField.Contains("has mods that were not found") && ModBase.RegexCheck(this._ComposerField, "The Mod File [^\\n]+optifine\\\\OptiFine[^\\n]+ has mods that were not found", 0))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.OptiFine与Forge不兼容, null);
				}
				if (this._ComposerField.Contains("-- MOD "))
				{
					if (this._ComposerField.Between("-- MOD ", "Failure message:", false).ContainsF(".jar", true))
					{
						this.AppendReason(CrashAnalyzer.CrashReason.确定Mod导致游戏崩溃, (ModBase.RegexSeek(this._ComposerField, "(?<=Mod File: ).+", 0) ?? "").TrimEnd("\r\n ".ToCharArray()));
					}
					else
					{
						this.AppendReason(CrashAnalyzer.CrashReason.Mod加载器报错, (ModBase.RegexSeek(this._ComposerField, "(?<=Failure message: )[\\w\\W]+?(?=\\tMod)", 0) ?? "").Replace("\t", " ").TrimEnd("\r\n ".ToCharArray()));
					}
				}
				if (this._ComposerField.Contains("Multiple entries with same key: "))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.确定Mod导致游戏崩溃, this.TryAnalyzeModName((ModBase.RegexSeek(this._ComposerField, "(?<=Multiple entries with same key: )[^=]+", 0) ?? "").TrimEnd("\r\n ".ToCharArray())));
				}
				if (this._ComposerField.Contains("LoaderExceptionModCrash: Caught exception from "))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.确定Mod导致游戏崩溃, this.TryAnalyzeModName((ModBase.RegexSeek(this._ComposerField, "(?<=LoaderExceptionModCrash: Caught exception from )[^\\n]+", 0) ?? "").TrimEnd("\r\n ".ToCharArray())));
				}
				if (this._ComposerField.Contains("Failed loading config file "))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Mod配置文件导致游戏崩溃, new string[]
					{
						Enumerable.First<string>(this.TryAnalyzeModName((ModBase.RegexSeek(this._ComposerField, "(?<=Failed loading config file .+ for modid )[^\\n]+", 0) ?? "").TrimEnd(new char[]
						{
							'\r'
						}))),
						(ModBase.RegexSeek(this._ComposerField, "(?<=Failed loading config file ).+(?= of type)", 0) ?? "").TrimEnd(new char[]
						{
							'\r'
						})
					});
				}
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0002CF9C File Offset: 0x0002B19C
		private void AnalyzeCrit2()
		{
			VB$AnonymousDelegate_3<string, bool> vb$AnonymousDelegate_ = delegate(string LogText)
			{
				bool result;
				if (!LogText.Contains("Mixin prepare failed ") && !LogText.Contains("Mixin apply failed ") && !LogText.Contains("MixinApplyError") && !LogText.Contains("MixinTransformerError") && !LogText.Contains("mixin.injection.throwables.") && !LogText.Contains(".json] FAILED during )"))
				{
					result = false;
				}
				else
				{
					string text2 = ModBase.RegexSeek(LogText, "(?<=from mod )[^.\\/ ]+(?=\\] from)", 0);
					if (text2 == null)
					{
						text2 = ModBase.RegexSeek(LogText, "(?<=for mod )[^.\\/ ]+(?= failed)", 0);
					}
					if (text2 != null)
					{
						this.AppendReason(CrashAnalyzer.CrashReason.ModMixin失败, this.TryAnalyzeModName(text2.TrimEnd("\r\n ".ToCharArray())));
						result = true;
					}
					else
					{
						try
						{
							List<string>.Enumerator enumerator = ModBase.RegexSearch(LogText, "(?<=^[^\\t]+[ \\[{(]{1})[^ \\[{(]+\\.[^ ]+(?=\\.json)", 2).GetEnumerator();
							if (enumerator.MoveNext())
							{
								string text3 = enumerator.Current;
								this.AppendReason(CrashAnalyzer.CrashReason.ModMixin失败, this.TryAnalyzeModName(text3.Replace("mixins", "mixin").Replace(".mixin", "").Replace("mixin.", "")));
								return true;
							}
						}
						finally
						{
							List<string>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
						this.AppendReason(CrashAnalyzer.CrashReason.ModMixin失败, null);
						result = true;
					}
				}
				return result;
			};
			if (this.m_MapperField != null)
			{
				bool flag = vb$AnonymousDelegate_(this.m_MapperField);
				if (this.m_MapperField.Contains("An exception was thrown, the game will display an error screen and halt."))
				{
					CrashAnalyzer.CrashReason reason = CrashAnalyzer.CrashReason.Forge报错;
					string text = ModBase.RegexSeek(this.m_MapperField, "(?<=the game will display an error screen and halt.[\\n\\r]+[^\\n]+?Exception: )[\\s\\S]+?(?=\\n\\tat)", 0);
					this.AppendReason(reason, (text != null) ? text.Trim(new char[]
					{
						'\r'
					}) : null);
				}
				if (this.m_MapperField.Contains("A potential solution has been determined:"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Fabric报错并给出解决方案, ModBase.RegexSearch(ModBase.RegexSeek(this.m_MapperField, "(?<=A potential solution has been determined:\\n)((\\t)+ - [^\\n]+\\n)+", 0) ?? "", "(?<=(\\t)+)[^\\n]+", 0).Join("\n"));
				}
				if (this.m_MapperField.Contains("A potential solution has been determined, this may resolve your problem:"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Fabric报错并给出解决方案, ModBase.RegexSearch(ModBase.RegexSeek(this.m_MapperField, "(?<=A potential solution has been determined, this may resolve your problem:\\n)((\\t)+ - [^\\n]+\\n)+", 0) ?? "", "(?<=(\\t)+)[^\\n]+", 0).Join("\n"));
				}
				if (this.m_MapperField.Contains("确定了一种可能的解决方法，这样做可能会解决你的问题："))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Fabric报错并给出解决方案, ModBase.RegexSearch(ModBase.RegexSeek(this.m_MapperField, "(?<=确定了一种可能的解决方法，这样做可能会解决你的问题：\\n)((\\t)+ - [^\\n]+\\n)+", 0) ?? "", "(?<=(\\t)+)[^\\n]+", 0).Join("\n"));
				}
				if (!flag && this.m_MapperField.Contains("due to errors, provided by "))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.确定Mod导致游戏崩溃, this.TryAnalyzeModName((ModBase.RegexSeek(this.m_MapperField, "(?<=due to errors, provided by ')[^']+", 0) ?? "").TrimEnd("\r\n ".ToCharArray())));
				}
			}
			if (this._ComposerField != null)
			{
				vb$AnonymousDelegate_(this._ComposerField);
				if (this._ComposerField.Contains("Suspected Mod"))
				{
					string str = this._ComposerField.Between("Suspected Mod", "Stacktrace", false);
					if (!str.StartsWithF("s: None", false))
					{
						List<string> list = ModBase.RegexSearch(str, "(?<=\\n\\t[^(\\t]+\\()[^)\\n]+", 0);
						if (Enumerable.Any<string>(list))
						{
							this.AppendReason(CrashAnalyzer.CrashReason.怀疑Mod导致游戏崩溃, this.TryAnalyzeModName(list));
						}
					}
				}
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0002D1A8 File Offset: 0x0002B3A8
		private void AnalyzeCrit3()
		{
			if (this.m_MapperField != null)
			{
				if (!this.m_MapperField.Contains("at net.") && !this.m_MapperField.Contains("INFO]") && this.propertyField == null && this._ComposerField == null && this.m_MapperField.Length < 100)
				{
					this.AppendReason(CrashAnalyzer.CrashReason.极短的程序输出, this.m_MapperField);
				}
				if (this.m_MapperField.Contains("Mod resolution failed"))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.Mod加载器报错, null);
				}
				if (this.m_MapperField.Contains("Failed to create mod instance."))
				{
					CrashAnalyzer.CrashReason reason = CrashAnalyzer.CrashReason.Mod初始化失败;
					string text;
					if ((text = ModBase.RegexSeek(this.m_MapperField, "(?<=Failed to create mod instance. ModID: )[^,]+", 0)) == null)
					{
						text = (ModBase.RegexSeek(this.m_MapperField, "(?<=Failed to create mod instance. ModId )[^\\n]+(?= for )", 0) ?? "");
					}
					this.AppendReason(reason, this.TryAnalyzeModName(text.TrimEnd(new char[]
					{
						'\r'
					})));
				}
			}
			if (this._ComposerField != null)
			{
				if (this._ComposerField.Contains("\tBlock location: World: "))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.特定方块导致崩溃, ModBase.RegexSeek(this._ComposerField, "(?<=\\tBlock: Block\\{)[^\\}]+", 0) + " " + ModBase.RegexSeek(this._ComposerField, "(?<=\\tBlock location: World: )\\([^\\)]+\\)", 0));
				}
				if (this._ComposerField.Contains("\tEntity's Exact location: "))
				{
					this.AppendReason(CrashAnalyzer.CrashReason.特定实体导致崩溃, ModBase.RegexSeek(this._ComposerField, "(?<=\\tEntity Type: )[^\\n]+(?= \\()", 0) + " (" + (ModBase.RegexSeek(this._ComposerField, "(?<=\\tEntity's Exact location: )[^\\n]+", 0) ?? "").TrimEnd("\r\n".ToCharArray()) + ")");
				}
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0002D340 File Offset: 0x0002B540
		private List<string> AnalyzeStackKeyword(string ErrorStack)
		{
			ErrorStack = "\n" + ErrorStack + "\n";
			List<string> list = new List<string>();
			list.AddRange(ModBase.RegexSearch(ErrorStack, "(?<=\\n[^{]+)[a-zA-Z_]+\\w+\\.[a-zA-Z_]+[\\w\\.]+(?=\\.[\\w\\.$]+\\.)", 0));
			list.AddRange(Enumerable.Select<string, string>(ModBase.RegexSearch(ErrorStack, "(?<=at [^(]+?\\.\\w+\\$\\w+\\$)[\\w\\$]+?(?=\\$\\w+\\()", 0), (CrashAnalyzer._Closure$__.$I25-0 == null) ? (CrashAnalyzer._Closure$__.$I25-0 = ((string s) => s.Replace("$", "."))) : CrashAnalyzer._Closure$__.$I25-0));
			list = Enumerable.ToList<string>(Enumerable.Distinct<string>(list));
			List<string> list2 = new List<string>();
			try
			{
				List<string>.Enumerator enumerator = list.GetEnumerator();
				IL_174:
				while (enumerator.MoveNext())
				{
					string text = enumerator.Current;
					foreach (string prefix in new string[]
					{
						"java",
						"sun",
						"javax",
						"jdk",
						"oolloo",
						"org.lwjgl",
						"com.sun",
						"net.minecraftforge",
						"paulscode.sound",
						"com.mojang",
						"net.minecraft",
						"cpw.mods",
						"com.google",
						"org.apache",
						"org.spongepowered",
						"net.fabricmc",
						"com.mumfrey",
						"com.electronwill.nightconfig",
						"it.unimi.dsi",
						"MojangTricksIntelDriversForPerformance_javaw"
					})
					{
						if (text.StartsWithF(prefix, false))
						{
							goto IL_174;
						}
					}
					list2.Add(text.Trim());
				}
			}
			finally
			{
				List<string>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			list2 = Enumerable.ToList<string>(Enumerable.Distinct<string>(list2));
			ModBase.Log("[Crash] 找到 " + Conversions.ToString(list2.Count) + " 条可能的堆栈信息", ModBase.LogLevel.Normal, "出现错误");
			checked
			{
				List<string> result;
				if (!Enumerable.Any<string>(list2))
				{
					result = new List<string>();
				}
				else
				{
					try
					{
						foreach (string str in list2)
						{
							ModBase.Log("[Crash]  - " + str, ModBase.LogLevel.Normal, "出现错误");
						}
					}
					finally
					{
						List<string>.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
					List<string> list3 = new List<string>();
					try
					{
						foreach (string fullStr in list2)
						{
							string[] array2 = fullStr.Split(".");
							int num = Math.Min(3, Enumerable.Count<string>(array2) - 1);
							for (int j = 0; j <= num; j++)
							{
								string text2 = array2[j];
								if (text2.Length > 2 && !text2.StartsWithF("func_", false) && !Enumerable.Contains<string>(new string[]
								{
									"com",
									"org",
									"net",
									"asm",
									"fml",
									"mod",
									"jar",
									"sun",
									"lib",
									"map",
									"gui",
									"dev",
									"nio",
									"api",
									"dsi",
									"top",
									"mcp",
									"core",
									"init",
									"mods",
									"main",
									"file",
									"game",
									"load",
									"read",
									"done",
									"util",
									"tile",
									"item",
									"base",
									"oshi",
									"impl",
									"data",
									"pool",
									"task",
									"forge",
									"setup",
									"block",
									"model",
									"mixin",
									"event",
									"unimi",
									"netty",
									"world",
									"gitlab",
									"common",
									"server",
									"config",
									"mixins",
									"compat",
									"loader",
									"launch",
									"entity",
									"assist",
									"client",
									"plugin",
									"modapi",
									"mojang",
									"shader",
									"events",
									"github",
									"recipe",
									"render",
									"packet",
									"events",
									"preinit",
									"preload",
									"machine",
									"reflect",
									"channel",
									"general",
									"handler",
									"content",
									"systems",
									"modules",
									"service",
									"fastutil",
									"optifine",
									"internal",
									"platform",
									"override",
									"fabricmc",
									"neoforge",
									"injection",
									"listeners",
									"scheduler",
									"minecraft",
									"transformer",
									"transformers",
									"neoforged",
									"universal",
									"multipart",
									"minecraftforge",
									"blockentity",
									"spongepowered",
									"electronwill"
								}, text2.ToLower()))
								{
									list3.Add(text2.Trim());
								}
							}
						}
					}
					finally
					{
						List<string>.Enumerator enumerator3;
						((IDisposable)enumerator3).Dispose();
					}
					list3 = Enumerable.ToList<string>(Enumerable.Distinct<string>(list3));
					ModBase.Log("[Crash] 从堆栈信息中找到 " + Conversions.ToString(list3.Count) + " 个可能的 Mod ID 关键词", ModBase.LogLevel.Normal, "出现错误");
					if (Enumerable.Any<string>(list3))
					{
						ModBase.Log("[Crash]  - " + list3.Join(", "), ModBase.LogLevel.Normal, "出现错误");
					}
					if (list3.Count > 10)
					{
						ModBase.Log("[Crash] 关键词过多，考虑匹配出错，不纳入考虑", ModBase.LogLevel.Normal, "出现错误");
						result = new List<string>();
					}
					else
					{
						result = list3;
					}
				}
				return result;
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0002DA38 File Offset: 0x0002BC38
		private List<string> AnalyzeModName(List<string> Keywords)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			try
			{
				foreach (string fullStr in Keywords)
				{
					foreach (string text in fullStr.Split("("))
					{
						list2.Add(text.Trim(" )".ToCharArray()));
					}
				}
			}
			finally
			{
				List<string>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			Keywords = list2;
			if (this._ComposerField != null && this._ComposerField.Contains("A detailed walkthrough of the error"))
			{
				string text2 = this._ComposerField.Replace("A detailed walkthrough of the error", "¨");
				bool flag;
				if (flag = text2.Contains("Fabric Mods"))
				{
					text2 = text2.Replace("Fabric Mods", "¨");
					ModBase.Log("[Crash] 崩溃报告中检测到 Fabric Mod 信息格式", ModBase.LogLevel.Normal, "出现错误");
				}
				text2 = text2.AfterLast("¨", false);
				List<string> list3 = new List<string>();
				foreach (string text3 in text2.Split("\n"))
				{
					if ((text3.ContainsF(".jar", true) && checked(text3.Length - text3.Replace(".jar", "").Length) == 4) || (flag && text3.StartsWithF("\t\t", false) && !ModBase.RegexCheck(text3, "\\t\\tfabric[\\w-]*: Fabric", 0)))
					{
						list3.Add(text3);
					}
				}
				ModBase.Log("[Crash] 崩溃报告中找到 " + Conversions.ToString(list3.Count) + " 个可能的 Mod 项目行", ModBase.LogLevel.Normal, "出现错误");
				List<string> list4 = new List<string>();
				try
				{
					foreach (string text4 in Keywords)
					{
						try
						{
							foreach (string text5 in list3)
							{
								string text6 = text5.ToLower().Replace("_", "");
								if (text6.Contains(text4.ToLower().Replace("_", "")) && !text6.Contains("minecraft.jar") && !text6.Contains(" forge-") && !text6.Contains(" mixin-"))
								{
									list4.Add(text5.Trim("\r\n".ToCharArray()));
									break;
								}
							}
						}
						finally
						{
							List<string>.Enumerator enumerator3;
							((IDisposable)enumerator3).Dispose();
						}
					}
				}
				finally
				{
					List<string>.Enumerator enumerator2;
					((IDisposable)enumerator2).Dispose();
				}
				list4 = Enumerable.ToList<string>(Enumerable.Distinct<string>(list4));
				ModBase.Log("[Crash] 崩溃报告中找到 " + Conversions.ToString(list4.Count) + " 个可能的崩溃 Mod 匹配行", ModBase.LogLevel.Normal, "出现错误");
				try
				{
					foreach (string str in list4)
					{
						ModBase.Log("[Crash]  - " + str, ModBase.LogLevel.Normal, "出现错误");
					}
				}
				finally
				{
					List<string>.Enumerator enumerator4;
					((IDisposable)enumerator4).Dispose();
				}
				try
				{
					foreach (string str2 in list4)
					{
						string text7;
						if (flag)
						{
							text7 = ModBase.RegexSeek(str2, "(?<=: )[^\\n]+(?= [^\\n]+)", 0);
						}
						else
						{
							text7 = ModBase.RegexSeek(str2, "(?<=\\()[^\\t]+.jar(?=\\))|(?<=(\\t\\t)|(\\| ))[^\\t\\|]+.jar", 1);
						}
						if (text7 != null)
						{
							list.Add(text7);
						}
					}
				}
				finally
				{
					List<string>.Enumerator enumerator5;
					((IDisposable)enumerator5).Dispose();
				}
			}
			if (this._ThreadField != null)
			{
				List<string> list5 = ModBase.RegexSearch(this._ThreadField, "(?<=valid mod file ).*", 2);
				ModBase.Log("[Crash] Debug 信息中找到 " + Conversions.ToString(list5.Count) + " 个可能的 Mod 项目行", ModBase.LogLevel.Normal, "出现错误");
				List<string> list6 = new List<string>();
				try
				{
					foreach (string arg in Keywords)
					{
						try
						{
							foreach (string text8 in list5)
							{
								if (text8.Contains(string.Format("{{{0}}}", arg)))
								{
									list6.Add(text8);
								}
							}
						}
						finally
						{
							List<string>.Enumerator enumerator7;
							((IDisposable)enumerator7).Dispose();
						}
					}
				}
				finally
				{
					List<string>.Enumerator enumerator6;
					((IDisposable)enumerator6).Dispose();
				}
				list6 = Enumerable.ToList<string>(Enumerable.Distinct<string>(list6));
				ModBase.Log("[Crash] Debug 信息中找到 " + Conversions.ToString(list6.Count) + " 个可能的崩溃 Mod 匹配行", ModBase.LogLevel.Normal, "出现错误");
				try
				{
					foreach (string str3 in list6)
					{
						ModBase.Log("[Crash]  - " + str3, ModBase.LogLevel.Normal, "出现错误");
					}
				}
				finally
				{
					List<string>.Enumerator enumerator8;
					((IDisposable)enumerator8).Dispose();
				}
				try
				{
					foreach (string str4 in list6)
					{
						string text9 = ModBase.RegexSeek(str4, ".*(?= with)", 0);
						if (text9 != null)
						{
							list.Add(text9);
						}
					}
				}
				finally
				{
					List<string>.Enumerator enumerator9;
					((IDisposable)enumerator9).Dispose();
				}
			}
			list = Enumerable.ToList<string>(Enumerable.Distinct<string>(list));
			List<string> result;
			if (!Enumerable.Any<string>(list))
			{
				result = null;
			}
			else
			{
				ModBase.Log("[Crash] 找到 " + Conversions.ToString(list.Count) + " 个可能的崩溃 Mod 文件名", ModBase.LogLevel.Normal, "出现错误");
				try
				{
					foreach (string str5 in list)
					{
						ModBase.Log("[Crash]  - " + str5, ModBase.LogLevel.Normal, "出现错误");
					}
				}
				finally
				{
					List<string>.Enumerator enumerator10;
					((IDisposable)enumerator10).Dispose();
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0002E01C File Offset: 0x0002C21C
		private List<string> TryAnalyzeModName(string Keyword)
		{
			List<string> list = new List<string>
			{
				Keyword ?? ""
			};
			List<string> result;
			if (string.IsNullOrEmpty(Keyword))
			{
				result = list;
			}
			else
			{
				result = (this.AnalyzeModName(list) ?? list);
			}
			return result;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0002E05C File Offset: 0x0002C25C
		private List<string> TryAnalyzeModName(List<string> Keywords)
		{
			List<string> result;
			if (!Enumerable.Any<string>(Keywords))
			{
				result = Keywords;
			}
			else
			{
				result = (this.AnalyzeModName(Keywords) ?? Keywords);
			}
			return result;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0002E084 File Offset: 0x0002C284
		public void Output(bool IsHandAnalyze, List<string> ExtraFiles = null)
		{
			ModMain._ProcessIterator.ShowWindowToTop();
			string analyzeResult = this.GetAnalyzeResult(IsHandAnalyze);
			string title = IsHandAnalyze ? "错误报告分析结果" : "Minecraft 出现错误";
			string button = "确定";
			string button2 = (IsHandAnalyze || this.testsField == null) ? "" : "查看日志";
			string button3 = IsHandAnalyze ? "" : "导出错误报告";
			bool isWarn = false;
			bool highLight = true;
			bool forceWait = false;
			Action button1Action = null;
			VB$AnonymousDelegate_4 vb$AnonymousDelegate_ = (IsHandAnalyze || this.testsField == null) ? null : new VB$AnonymousDelegate_4(delegate()
			{
				if (File.Exists(this.testsField.Value.Key))
				{
					ModBase.ShellOnly("notepad", this.testsField.Value.Key);
					return;
				}
				string text3 = ModBase.m_DecoratorRepository + "Crash.txt";
				ModBase.WriteFile(text3, this.testsField.Value.Value.Join("\r\n"), false, null);
				ModBase.ShellOnly(text3, "");
			});
			int num = ModMain.MyMsgBox(analyzeResult, title, button, button2, button3, isWarn, highLight, forceWait, button1Action, (vb$AnonymousDelegate_ == null) ? null : new Action(vb$AnonymousDelegate_.Invoke), null);
			if (num == 3)
			{
				CrashAnalyzer._Closure$__30-0 CS$<>8__locals1 = new CrashAnalyzer._Closure$__30-0(CS$<>8__locals1);
				CS$<>8__locals1.$VB$Local_FileAddress = null;
				try
				{
					ModBase.RunInUiWait(delegate()
					{
						CS$<>8__locals1.$VB$Local_FileAddress = ModBase.SelectAs("选择保存位置", "错误报告-" + DateTime.Now.ToString("G").Replace("/", "-").Replace(":", ".").Replace(" ", "_") + ".zip", "Minecraft 错误报告(*.zip)|*.zip", null);
					});
					if (string.IsNullOrEmpty(CS$<>8__locals1.$VB$Local_FileAddress))
					{
						return;
					}
					Directory.CreateDirectory(ModBase.GetPathFromFullPath(CS$<>8__locals1.$VB$Local_FileAddress));
					if (File.Exists(CS$<>8__locals1.$VB$Local_FileAddress))
					{
						File.Delete(CS$<>8__locals1.$VB$Local_FileAddress);
					}
					ModBase.FeedbackInfo();
					ModBase.LogFlush();
					if (ExtraFiles != null)
					{
						this.m_ErrorField.AddRange(ExtraFiles);
					}
					try
					{
						foreach (string text in this.m_ErrorField)
						{
							string text2 = ModBase.GetFileNameFromPath(text);
							Encoding encoding = null;
							if (Operators.CompareString(text2, "LatestLaunch.bat", false) != 0)
							{
								if (Operators.CompareString(text2, "Log1.txt", false) != 0)
								{
									if (Operators.CompareString(text2, "RawOutput.log", false) == 0)
									{
										text2 = "游戏崩溃前的输出.txt";
										encoding = Encoding.UTF8;
									}
								}
								else
								{
									text2 = "PCL 启动器日志.txt";
									encoding = Encoding.UTF8;
								}
							}
							else
							{
								text2 = "启动脚本.bat";
							}
							if (File.Exists(text))
							{
								if (encoding == null)
								{
									encoding = ModBase.GetEncoding(ModBase.ReadFileBytes(text, null));
								}
								ModBase.WriteFile(this.clientField + "Report\\" + text2, ModSecret.SecretFilter(ModBase.ReadFile(text, encoding), Conversions.ToChar((Operators.CompareString(text2, "启动脚本.bat", false) == 0) ? "F" : "*")), false, encoding);
								ModBase.Log(string.Format("[Crash] 导出文件：{0}，编码：{1}", text2, encoding.HeaderName), ModBase.LogLevel.Normal, "出现错误");
							}
						}
					}
					finally
					{
						List<string>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					ZipFile.CreateFromDirectory(this.clientField + "Report\\", CS$<>8__locals1.$VB$Local_FileAddress);
					ModBase.DeleteDirectory(this.clientField + "Report\\", false);
					ModMain.Hint("错误报告已导出！", ModMain.HintType.Finish, true);
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "导出错误报告失败", ModBase.LogLevel.Feedback, "出现错误");
					return;
				}
				ModBase.OpenExplorer("/select,\"" + CS$<>8__locals1.$VB$Local_FileAddress + "\"");
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0002E360 File Offset: 0x0002C560
		private string GetAnalyzeResult(bool IsHandAnalyze)
		{
			string result;
			if (!Enumerable.Any<KeyValuePair<CrashAnalyzer.CrashReason, List<string>>>(this.m_RepositoryField))
			{
				if (IsHandAnalyze)
				{
					result = "很抱歉，PCL 无法确定错误原因。";
				}
				else
				{
					result = string.Format("很抱歉，你的游戏出现了一些问题……{0}如果要寻求帮助，请把错误报告文件发给对方，而不是发送这个窗口的照片或者截图。", "\r\n");
				}
			}
			else
			{
				List<string> list = new List<string>();
				try
				{
					foreach (KeyValuePair<CrashAnalyzer.CrashReason, List<string>> keyValuePair in this.m_RepositoryField)
					{
						List<string> value = keyValuePair.Value;
						switch (keyValuePair.Key)
						{
						case CrashAnalyzer.CrashReason.Mod文件被解压:
							list.Add("由于 Mod 文件被解压了，导致游戏无法继续运行。\\n直接把整个 Mod 文件放进 Mod 文件夹中即可，若解压就会导致游戏出错。\\n\\n请删除 Mod 文件夹中已被解压的 Mod，然后再启动游戏。");
							break;
						case CrashAnalyzer.CrashReason.MixinBootstrap缺失:
							list.Add("由于缺失 MixinBootstrap，导致游戏崩溃。\\n请尝试安装 MixinBootstrap。若安装后依然崩溃，可以尝试在文件名前添加英文感叹号。");
							break;
						case CrashAnalyzer.CrashReason.内存不足:
							list.Add("Minecraft 内存不足，导致其无法继续运行。\\n这很可能是因为电脑内存不足、游戏分配的内存不足，或是配置要求过高。\\n\\n你可以尝试在 更多 → 百宝箱 中选择 内存优化，然后再启动游戏。\\n如果还是不行，请在启动设置中增加为游戏分配的内存，并删除配置要求较高的材质、Mod、光影。\\n如果依然不奏效，请在开始游戏前尽量关闭其他软件，或者……换台电脑？\\h");
							break;
						case CrashAnalyzer.CrashReason.使用JDK:
							list.Add("游戏似乎因为使用 JDK，或 Java 版本过高而崩溃了。\\n请在启动设置的 Java 选择一项中改用 JRE 8（Java 8），然后再启动游戏。\\n如果你没有安装 JRE 8，你可以从网络中下载、安装一个。");
							break;
						case CrashAnalyzer.CrashReason.显卡不支持OpenGL:
						case CrashAnalyzer.CrashReason.显卡驱动不支持导致无法设置像素格式:
						case CrashAnalyzer.CrashReason.Intel驱动不兼容导致EXCEPTION_ACCESS_VIOLATION:
						case CrashAnalyzer.CrashReason.AMD驱动不兼容导致EXCEPTION_ACCESS_VIOLATION:
						case CrashAnalyzer.CrashReason.Nvidia驱动不兼容导致EXCEPTION_ACCESS_VIOLATION:
							if (this._IteratorField.Contains("hd graphics "))
							{
								list.Add("你的显卡驱动存在问题，或未使用独立显卡，导致游戏无法正常运行。\\n\\n如果你的电脑存在独立显卡，请使用独立显卡而非 Intel 核显启动 PCL 与 Minecraft。\\n如果问题依然存在，请尝试升级你的显卡驱动到最新版本，或回退到出厂版本。\\n如果还是不行，还可以尝试使用 8.0.51 或更低版本的 Java。\\h");
							}
							else
							{
								list.Add("你的显卡驱动存在问题，导致游戏无法正常运行。\\n\\n请尝试升级你的显卡驱动到最新版本，或回退到出厂版本，然后再启动游戏。\\n如果还是不行，可以尝试使用 8.0.51 或更低版本的 Java。\\n如果问题依然存在，那么你可能需要换个更好的显卡……\\h");
							}
							break;
						case CrashAnalyzer.CrashReason.使用OpenJ9:
							list.Add("游戏因为使用 Open J9 而崩溃了。\\n请在启动设置的 Java 选择一项中改用非 OpenJ9 的 Java，然后再启动游戏。");
							break;
						case CrashAnalyzer.CrashReason.Java版本过高:
							list.Add("游戏似乎因为你所使用的 Java 版本过高而崩溃了。\\n请在启动设置的 Java 选择一项中改用较低版本的 Java，然后再启动游戏。\\n如果没有，可以从网络中下载、安装一个。");
							break;
						case CrashAnalyzer.CrashReason.Java版本不兼容:
							list.Add("游戏不兼容你当前使用的 Java。\\n如果没有合适的 Java，可以从网络中下载、安装一个。");
							break;
						case CrashAnalyzer.CrashReason.Mod名称包含特殊字符:
							list.Add("由于有 Mod 的名称包含特殊字符，导致游戏崩溃。\\n请尝试修改 Mod 文件名，让它只包含英文字母、数字、减号（-）、下划线（_）和小数点，然后再启动游戏。");
							break;
						case CrashAnalyzer.CrashReason.极短的程序输出:
							list.Add(string.Format("程序返回了以下信息：\\n{0}\\n\\h", Enumerable.First<string>(value)));
							break;
						case CrashAnalyzer.CrashReason.玩家手动触发调试崩溃:
							list.Add("* 事实上，你的游戏没有任何问题，这是你自己触发的崩溃。\\n* 你难道没有更重要的事要做吗？");
							break;
						case CrashAnalyzer.CrashReason.光影或资源包导致OpenGL1282错误:
							list.Add("你所使用的光影或材质导致游戏出现了一些问题……\\n\\n请尝试删除你所添加的这些额外资源。\\h");
							break;
						case CrashAnalyzer.CrashReason.文件或内容校验失败:
							list.Add("部分文件或内容校验失败，导致游戏出现了问题。\\n\\n请尝试删除游戏（包括 Mod）并重新下载，或尝试在重新下载时使用 VPN。\\h");
							break;
						case CrashAnalyzer.CrashReason.确定Mod导致游戏崩溃:
							if (value.Count == 1)
							{
								list.Add("名为 " + Enumerable.First<string>(value) + " 的 Mod 导致了游戏出错。\\n你可以尝试禁用此 Mod，然后观察游戏是否还会崩溃。\\n\\e\\h");
							}
							else
							{
								list.Add("以下 Mod 导致了游戏出错：\\n - " + value.Join("\\n - ") + "\\n\\n你可以尝试依次禁用上述 Mod，然后观察游戏是否还会崩溃。\\n\\e\\h");
							}
							break;
						case CrashAnalyzer.CrashReason.怀疑Mod导致游戏崩溃:
						case CrashAnalyzer.CrashReason.堆栈分析发现Mod名称:
							if (value.Count == 1)
							{
								list.Add("PCL 怀疑名为 " + Enumerable.First<string>(value) + " 的 Mod 导致了游戏出错，但不能完全确定。\\n你可以尝试禁用此 Mod，然后观察游戏是否还会崩溃。\\n\\e\\h");
							}
							else
							{
								list.Add("PCL 怀疑以下 Mod 导致了游戏出错，但不能完全确定：\\n - " + value.Join("\\n - ") + "\\n\\n你可以尝试依次禁用上述 Mod，然后观察游戏是否还会崩溃。\\n\\e\\h");
							}
							break;
						case CrashAnalyzer.CrashReason.Mod配置文件导致游戏崩溃:
							if (value[1] == null)
							{
								list.Add("名为 " + Enumerable.First<string>(value) + " 的 Mod 导致了游戏出错。\\n\\e\\h");
							}
							else
							{
								list.Add(string.Concat(new string[]
								{
									"名为 ",
									Enumerable.First<string>(value),
									" 的 Mod 导致了游戏出错：\\n其配置文件 ",
									value[1],
									" 存在异常，无法读取。"
								}));
							}
							break;
						case CrashAnalyzer.CrashReason.ModMixin失败:
							if (value.Count == 0)
							{
								list.Add("部分 Mod 注入失败，导致游戏出错。\\n这一般代表着部分 Mod 与其他 Mod 或当前环境不兼容，或是它存在 Bug。\\n你可以尝试逐步禁用 Mod，然后观察游戏是否还会崩溃，以此定位导致崩溃的 Mod。\\n\\e\\h");
							}
							else if (value.Count == 1)
							{
								list.Add("名为 " + Enumerable.First<string>(value) + " 的 Mod 注入失败，导致游戏出错。\\n这一般代表着它与其他 Mod 或当前环境不兼容，或是它存在 Bug。\\n你可以尝试禁用此 Mod，然后观察游戏是否还会崩溃。\\n\\e\\h");
							}
							else
							{
								list.Add("以下 Mod 导致了游戏出错：\\n - " + value.Join("\\n - ") + "\\n这一般代表着它们与其他 Mod 或当前环境不兼容，或是它存在 Bug。\\n你可以尝试依次禁用上述 Mod，然后观察游戏是否还会崩溃。\\n\\e\\h");
							}
							break;
						case CrashAnalyzer.CrashReason.Mod加载器报错:
							if (value.Count == 1)
							{
								list.Add("Mod 加载器提供了以下错误信息：\\n" + Enumerable.First<string>(value) + "\\n\\n请根据上述信息进行对应处理，如果看不懂英文可以使用翻译软件。");
							}
							else
							{
								list.Add("Mod 加载器可能已经提供了错误信息，请根据错误报告中的日志信息进行对应处理，如果看不懂英文可以使用翻译软件。\\h");
							}
							break;
						case CrashAnalyzer.CrashReason.Mod初始化失败:
							if (value.Count == 1)
							{
								list.Add("名为 " + Enumerable.First<string>(value) + " 的 Mod 初始化失败，导致游戏无法继续加载。\\n你可以尝试禁用此 Mod，然后观察游戏是否还会崩溃。\\n\\e\\h");
							}
							else
							{
								list.Add("以下 Mod 初始化失败，导致游戏出错：\\n - " + value.Join("\\n - ") + "\\n\\n你可以尝试依次禁用上述 Mod，然后观察游戏是否还会崩溃。\\n\\e\\h");
							}
							break;
						case CrashAnalyzer.CrashReason.堆栈分析发现关键字:
							if (value.Count == 1)
							{
								list.Add("你的游戏遇到了一些问题，PCL 为此找到了一个可疑的关键词：" + Enumerable.First<string>(value) + "。\\n\\n如果你知道某个关键词对应的 Mod，那么有可能就是它引起的错误，你也可以查看错误报告获取详情。\\h");
							}
							else
							{
								list.Add("你的游戏遇到了一些问题，PCL 为此找到了以下可疑的关键词：\\n - " + value.Join(", ") + "\\n\\n如果你知道某个关键词对应的 Mod，那么有可能就是它引起的错误，你也可以查看错误报告获取详情。\\h");
							}
							break;
						case CrashAnalyzer.CrashReason.OptiFine导致无法加载世界:
							list.Add("你所使用的 OptiFine 可能导致了你的游戏出现问题。\\n\\n该问题只在特定 OptiFine 版本中出现，你可以尝试更换 OptiFine 的版本。\\h");
							break;
						case CrashAnalyzer.CrashReason.特定方块导致崩溃:
							if (value.Count == 1)
							{
								list.Add("游戏似乎因为方块 " + Enumerable.First<string>(value) + " 出现了问题。\\n\\n你可以创建一个新世界，并观察游戏的运行情况：\\n - 若正常运行，则是该方块导致出错，你或许需要使用一些方式删除此方块。\\n - 若仍然出错，问题就可能来自其他原因……\\h");
							}
							else
							{
								list.Add("游戏似乎因为世界中的某些方块出现了问题。\\n\\n你可以创建一个新世界，并观察游戏的运行情况：\\n - 若正常运行，则是某些方块导致出错，你或许需要删除该世界。\\n - 若仍然出错，问题就可能来自其他原因……\\h");
							}
							break;
						case CrashAnalyzer.CrashReason.特定实体导致崩溃:
							if (value.Count == 1)
							{
								list.Add("游戏似乎因为实体 " + Enumerable.First<string>(value) + " 出现了问题。\\n\\n你可以创建一个新世界，并生成一个该实体，然后观察游戏的运行情况：\\n - 若正常运行，则是该实体导致出错，你或许需要使用一些方式删除此实体。\\n - 若仍然出错，问题就可能来自其他原因……\\h");
							}
							else
							{
								list.Add("游戏似乎因为世界中的某些实体出现了问题。\\n\\n你可以创建一个新世界，并生成各种实体，观察游戏的运行情况：\\n - 若正常运行，则是某些实体导致出错，你或许需要删除该世界。\\n - 若仍然出错，问题就可能来自其他原因……\\h");
							}
							break;
						case CrashAnalyzer.CrashReason.材质过大或显卡配置不足:
							list.Add("你所使用的材质分辨率过高，或显卡配置不足，导致游戏无法继续运行。\\n\\n如果你正在使用高清材质，请将它移除。\\n如果你没有使用材质，那么你可能需要更新显卡驱动，或者换个更好的显卡……\\h");
							break;
						case CrashAnalyzer.CrashReason.没有可用的分析文件:
							list.Add("你的游戏出现了一些问题，但 PCL 未能找到相关记录文件，因此无法进行分析。\\h");
							break;
						case CrashAnalyzer.CrashReason.使用32位Java导致JVM无法分配足够多的内存:
							if (Environment.Is64BitOperatingSystem)
							{
								list.Add("你似乎正在使用 32 位 Java，这会导致 Minecraft 无法使用所需的内存，进而造成崩溃。\\n\\n请在启动设置的 Java 选择一项中改用 64 位的 Java 再启动游戏，然后再启动游戏。\\n如果你没有安装 64 位的 Java，你可以从网络中下载、安装一个。");
							}
							else
							{
								list.Add("你正在使用 32 位的操作系统，这会导致 Minecraft 无法使用所需的内存，进而造成崩溃。\\n\\n你或许只能重装 64 位的操作系统来解决此问题。\\n如果你的电脑内存在 2GB 以内，那或许只能换台电脑了……\\h");
							}
							break;
						case CrashAnalyzer.CrashReason.Mod重复安装:
							if (value.Count >= 2)
							{
								list.Add("你重复安装了多个相同的 Mod：\\n - " + value.Join("\\n - ") + "\\n\\n每个 Mod 只能出现一次，请删除重复的 Mod，然后再启动游戏。");
							}
							else
							{
								list.Add("你可能重复安装了多个相同的 Mod，导致游戏出错。\\n\\n每个 Mod 只能出现一次，请删除重复的 Mod，然后再启动游戏。\\e\\h");
							}
							break;
						case CrashAnalyzer.CrashReason.Mod互不兼容:
							if (value.Count == 1)
							{
								list.Add("你所安装的 Mod 不兼容：\\n" + Enumerable.First<string>(value) + "\\n\\n请根据上述信息进行对应处理，如果看不懂英文可以使用翻译软件。");
							}
							else
							{
								list.Add("你所安装的 Mod 不兼容，Mod 加载器可能已经提供了错误信息，请根据错误报告中的日志信息进行对应处理，如果看不懂英文可以使用翻译软件。\\h");
							}
							break;
						case CrashAnalyzer.CrashReason.OptiFine与Forge不兼容:
							list.Add("由于 OptiFine 与当前版本的 Forge 不兼容，导致了游戏崩溃。\\n\\n请前往 OptiFine 官网（https://optifine.net/downloads）查看 OptiFine 所兼容的 Forge 版本，并严格按照对应版本重新安装游戏。");
							break;
						case CrashAnalyzer.CrashReason.Fabric报错:
							if (value.Count == 1)
							{
								list.Add("Fabric 提供了以下错误信息：\\n" + Enumerable.First<string>(value) + "\\n\\n请根据上述信息进行对应处理，如果看不懂英文可以使用翻译软件。");
							}
							else
							{
								list.Add("Fabric 可能已经提供了错误信息，请根据错误报告中的日志信息进行对应处理，如果看不懂英文可以使用翻译软件。\\h");
							}
							break;
						case CrashAnalyzer.CrashReason.Fabric报错并给出解决方案:
							if (value.Count == 1)
							{
								list.Add("Fabric 提供了以下解决方案：\\n" + Enumerable.First<string>(value) + "\\n\\n请根据上述信息进行对应处理，如果看不懂英文可以使用翻译软件。");
							}
							else
							{
								list.Add("Fabric 可能已经提供了解决方案，请根据错误报告中的日志信息进行对应处理，如果看不懂英文可以使用翻译软件。\\h");
							}
							break;
						case CrashAnalyzer.CrashReason.Forge报错:
							if (value.Count == 1)
							{
								list.Add("Forge 提供了以下错误信息：\\n" + Enumerable.First<string>(value) + "\\n\\n请根据上述信息进行对应处理，如果看不懂英文可以使用翻译软件。");
							}
							else
							{
								list.Add("Forge 可能已经提供了错误信息，请根据错误报告中的日志信息进行对应处理，如果看不懂英文可以使用翻译软件。\\h");
							}
							break;
						case CrashAnalyzer.CrashReason.低版本Forge与高版本Java不兼容:
							list.Add("由于低版本 Forge 与当前 Java 不兼容，导致了游戏崩溃。\\n\\n请尝试以下解决方案：\\n - 更新 Forge 到 36.2.26 或更高版本\\n - 换用版本低于 1.8.0.320 的 Java");
							break;
						case CrashAnalyzer.CrashReason.版本Json中存在多个Forge:
							list.Add("可能由于其他启动器修改了 Forge 版本，当前版本的文件存在异常，导致了游戏崩溃。\\n请尝试重新全新安装 Forge，而非使用其他启动器修改 Forge 版本。");
							break;
						case CrashAnalyzer.CrashReason.Mod过多导致超出ID限制:
							list.Add("你所安装的 Mod 过多，超出了游戏的 ID 限制，导致了游戏崩溃。\\n请尝试安装 JEID 等修复 Mod，或删除部分大型 Mod。");
							break;
						case CrashAnalyzer.CrashReason.NightConfig的Bug:
							list.Add("由于 Night Config 存在问题，导致了游戏崩溃。\\n你可以尝试安装 Night Config Fixes 模组，这或许能解决此问题。\\h");
							break;
						case CrashAnalyzer.CrashReason.ShadersMod与OptiFine同时安装:
							list.Add("无需同时安装 Optifine 和 Shaders Mod，OptiFine 已经集成了 Shaders Mod 的功能。\\n在删除 Shaders Mod 后，游戏即可正常运行。");
							break;
						case CrashAnalyzer.CrashReason.Forge安装不完整:
							list.Add("由于安装的 Forge 文件丢失，导致游戏无法正常运行。\\n请重新安装一次相同版本的 Forge，然后再启动游戏。\\n在打包游戏时删除 libraries 文件夹可能导致此错误。\\h");
							break;
						case CrashAnalyzer.CrashReason.Mod需要Java11:
							list.Add("你所安装的部分 Mod 似乎需要使用 Java 11 启动。\\n请在启动设置的 Java 选择一项中改用 Java 11，然后再启动游戏。\\n如果你没有安装 Java 11，你可以从网络中下载、安装一个。");
							break;
						case CrashAnalyzer.CrashReason.Mod缺少前置或MC版本错误:
							if (Enumerable.Any<string>(value))
							{
								list.Add("由于未安装正确的前置 Mod，导致游戏退出。\\n缺失的依赖项：\\n - " + value.Join("\\n - ") + "\\n\\n请根据上述信息进行对应处理，如果看不懂英文可以使用翻译软件。");
							}
							else
							{
								list.Add("由于未安装正确的前置 Mod，导致游戏退出。\\n请根据错误报告中的日志信息进行对应处理，如果看不懂英文可以使用翻译软件。\\h");
							}
							break;
						default:
							list.Add("PCL 获取到了没有详细信息的错误原因（" + Conversions.ToString((int)Enumerable.First<KeyValuePair<CrashAnalyzer.CrashReason, List<string>>>(this.m_RepositoryField).Key) + "），请向 PCL 作者提交反馈以获取详情。\\h");
							break;
						}
					}
				}
				finally
				{
					Dictionary<CrashAnalyzer.CrashReason, List<string>>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				result = list.Join("\\n\\n此外，").Replace("\\n", "\r\n").Replace("\\h", "").Replace("\\e", IsHandAnalyze ? "" : "\r\n你可以查看错误报告了解错误具体是如何发生的。").Replace("\r\n", "\r").Replace("\n", "\r").Replace("\r", "\r\n").Trim("\r\n".ToCharArray()) + ((!Enumerable.Any<string>(list, (CrashAnalyzer._Closure$__.$I31-0 == null) ? (CrashAnalyzer._Closure$__.$I31-0 = ((string r) => r.EndsWithF("\\h", false))) : CrashAnalyzer._Closure$__.$I31-0) || IsHandAnalyze) ? "" : ("\r\n如果要寻求帮助，请把错误报告文件发给对方，而不是发送这个窗口的照片或者截图。" + (PageSetupSystem.IsLauncherNewest().GetValueOrDefault(true) ? "" : "\r\n\r\n此外，你正在使用老版本 PCL，更新 PCL 或许也能解决这个问题。\r\n你可以点击 设置 → 启动器 → 检查更新 来更新 PCL。")));
			}
			return result;
		}

		// Token: 0x040002B7 RID: 695
		private static bool fieldField = false;

		// Token: 0x040002B8 RID: 696
		private static object m_ReaderField = RuntimeHelpers.GetObjectValue(new object());

		// Token: 0x040002B9 RID: 697
		private string clientField;

		// Token: 0x040002BA RID: 698
		private List<KeyValuePair<string, string[]>> _ConfigField;

		// Token: 0x040002BB RID: 699
		private KeyValuePair<string, string[]>? testsField;

		// Token: 0x040002BC RID: 700
		private string m_MapperField;

		// Token: 0x040002BD RID: 701
		private string _ThreadField;

		// Token: 0x040002BE RID: 702
		private string propertyField;

		// Token: 0x040002BF RID: 703
		private string _ComposerField;

		// Token: 0x040002C0 RID: 704
		private string _IteratorField;

		// Token: 0x040002C1 RID: 705
		private Dictionary<CrashAnalyzer.CrashReason, List<string>> m_RepositoryField;

		// Token: 0x040002C2 RID: 706
		private List<string> m_ErrorField;

		// Token: 0x020000B0 RID: 176
		private enum AnalyzeFileType
		{
			// Token: 0x040002C4 RID: 708
			HsErr,
			// Token: 0x040002C5 RID: 709
			MinecraftLog,
			// Token: 0x040002C6 RID: 710
			ExtraLog,
			// Token: 0x040002C7 RID: 711
			CrashReport
		}

		// Token: 0x020000B1 RID: 177
		private enum CrashReason
		{
			// Token: 0x040002C9 RID: 713
			Mod文件被解压,
			// Token: 0x040002CA RID: 714
			MixinBootstrap缺失,
			// Token: 0x040002CB RID: 715
			内存不足,
			// Token: 0x040002CC RID: 716
			使用JDK,
			// Token: 0x040002CD RID: 717
			显卡不支持OpenGL,
			// Token: 0x040002CE RID: 718
			使用OpenJ9,
			// Token: 0x040002CF RID: 719
			Java版本过高,
			// Token: 0x040002D0 RID: 720
			Java版本不兼容,
			// Token: 0x040002D1 RID: 721
			Mod名称包含特殊字符,
			// Token: 0x040002D2 RID: 722
			显卡驱动不支持导致无法设置像素格式,
			// Token: 0x040002D3 RID: 723
			极短的程序输出,
			// Token: 0x040002D4 RID: 724
			Intel驱动不兼容导致EXCEPTION_ACCESS_VIOLATION,
			// Token: 0x040002D5 RID: 725
			AMD驱动不兼容导致EXCEPTION_ACCESS_VIOLATION,
			// Token: 0x040002D6 RID: 726
			Nvidia驱动不兼容导致EXCEPTION_ACCESS_VIOLATION,
			// Token: 0x040002D7 RID: 727
			玩家手动触发调试崩溃,
			// Token: 0x040002D8 RID: 728
			光影或资源包导致OpenGL1282错误,
			// Token: 0x040002D9 RID: 729
			文件或内容校验失败,
			// Token: 0x040002DA RID: 730
			确定Mod导致游戏崩溃,
			// Token: 0x040002DB RID: 731
			怀疑Mod导致游戏崩溃,
			// Token: 0x040002DC RID: 732
			Mod配置文件导致游戏崩溃,
			// Token: 0x040002DD RID: 733
			ModMixin失败,
			// Token: 0x040002DE RID: 734
			Mod加载器报错,
			// Token: 0x040002DF RID: 735
			Mod初始化失败,
			// Token: 0x040002E0 RID: 736
			堆栈分析发现关键字,
			// Token: 0x040002E1 RID: 737
			堆栈分析发现Mod名称,
			// Token: 0x040002E2 RID: 738
			OptiFine导致无法加载世界,
			// Token: 0x040002E3 RID: 739
			特定方块导致崩溃,
			// Token: 0x040002E4 RID: 740
			特定实体导致崩溃,
			// Token: 0x040002E5 RID: 741
			材质过大或显卡配置不足,
			// Token: 0x040002E6 RID: 742
			没有可用的分析文件,
			// Token: 0x040002E7 RID: 743
			使用32位Java导致JVM无法分配足够多的内存,
			// Token: 0x040002E8 RID: 744
			Mod重复安装,
			// Token: 0x040002E9 RID: 745
			Mod互不兼容,
			// Token: 0x040002EA RID: 746
			OptiFine与Forge不兼容,
			// Token: 0x040002EB RID: 747
			Fabric报错,
			// Token: 0x040002EC RID: 748
			Fabric报错并给出解决方案,
			// Token: 0x040002ED RID: 749
			Forge报错,
			// Token: 0x040002EE RID: 750
			低版本Forge与高版本Java不兼容,
			// Token: 0x040002EF RID: 751
			版本Json中存在多个Forge,
			// Token: 0x040002F0 RID: 752
			Mod过多导致超出ID限制,
			// Token: 0x040002F1 RID: 753
			NightConfig的Bug,
			// Token: 0x040002F2 RID: 754
			ShadersMod与OptiFine同时安装,
			// Token: 0x040002F3 RID: 755
			Forge安装不完整,
			// Token: 0x040002F4 RID: 756
			Mod需要Java11,
			// Token: 0x040002F5 RID: 757
			Mod缺少前置或MC版本错误
		}
	}
}
