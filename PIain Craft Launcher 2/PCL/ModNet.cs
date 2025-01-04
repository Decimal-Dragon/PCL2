using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x0200012F RID: 303
	[StandardModule]
	public sealed class ModNet
	{
		// Token: 0x06000CF1 RID: 3313 RVA: 0x00055F84 File Offset: 0x00054184
		public static int Ping(string Ip, int Timeout = 10000, bool MakeLog = true)
		{
			PingReply pingReply;
			try
			{
				pingReply = new Ping().Send(Ip);
			}
			catch (Exception ex)
			{
				if (MakeLog)
				{
					ModBase.Log("[Net] Ping " + Ip + " 失败：" + ex.Message, ModBase.LogLevel.Normal, "出现错误");
				}
				return -1;
			}
			int result;
			if (pingReply.Status == IPStatus.Success)
			{
				if (MakeLog)
				{
					ModBase.Log(string.Concat(new string[]
					{
						"[Net] Ping ",
						Ip,
						" 结束：",
						Conversions.ToString(pingReply.RoundtripTime),
						"ms"
					}), ModBase.LogLevel.Normal, "出现错误");
				}
				result = checked((int)pingReply.RoundtripTime);
			}
			else
			{
				if (MakeLog)
				{
					ModBase.Log("[Net] Ping " + Ip + " 失败", ModBase.LogLevel.Normal, "出现错误");
				}
				result = -1;
			}
			return result;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0005605C File Offset: 0x0005425C
		public static string NetGetCodeByClient(string Url, Encoding Encoding, string Accept = "application/json, text/javascript, */*; q=0.01", bool UseBrowserUserAgent = false)
		{
			int num = 0;
			Exception ex = null;
			long timeTick = ModBase.GetTimeTick();
			checked
			{
				string result;
				try
				{
					IL_0A:
					if (num != 0)
					{
						if (num != 1)
						{
							if (ModBase.GetTimeTick() - timeTick <= 5500L)
							{
								throw ex;
							}
							Thread.Sleep(500);
							result = ModNet.NetGetCodeByClient(Url, Encoding, 4000, Accept, UseBrowserUserAgent);
						}
						else
						{
							Thread.Sleep(500);
							result = ModNet.NetGetCodeByClient(Url, Encoding, 30000, Accept, UseBrowserUserAgent);
						}
					}
					else
					{
						result = ModNet.NetGetCodeByClient(Url, Encoding, 10000, Accept, UseBrowserUserAgent);
					}
				}
				catch (Exception ex2)
				{
					if (num == 0)
					{
						ex = ex2;
						num++;
						goto IL_0A;
					}
					if (num != 1)
					{
						throw;
					}
					num++;
					goto IL_0A;
				}
				return result;
			}
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00056114 File Offset: 0x00054314
		public static string NetGetCodeByClient(string Url, Encoding Encoding, int Timeout, string Accept, bool UseBrowserUserAgent = false)
		{
			Url = Conversions.ToString(ModSecret.SecretCdnSign(Url));
			ModBase.Log("[Net] 获取客户端网络结果：" + Url + "，最大超时 " + Conversions.ToString(Timeout), ModBase.LogLevel.Normal, "出现错误");
			HttpWebResponse httpWebResponse = null;
			Stream stream = null;
			string result;
			try
			{
				CookieWebClient cookieWebClient = new CookieWebClient
				{
					Encoding = Encoding,
					resolverField = Timeout
				};
				cookieWebClient.Headers["Accept"] = Accept;
				cookieWebClient.Headers["Accept-Language"] = "en-US,en;q=0.5";
				cookieWebClient.Headers["X-Requested-With"] = "XMLHttpRequest";
				string url = Url;
				WebClient webClient = cookieWebClient;
				ModSecret.SecretHeadersSign(url, ref webClient, UseBrowserUserAgent);
				cookieWebClient = (CookieWebClient)webClient;
				result = cookieWebClient.DownloadString(Url);
			}
			catch (Exception ex)
			{
				if (ex.GetType().Equals(typeof(WebException)) && ((WebException)ex).Status == 14)
				{
					throw new TimeoutException("连接服务器超时（" + Url + "）", ex);
				}
				throw new WebException(string.Concat(new string[]
				{
					"获取结果失败，",
					ex.Message,
					"（",
					Url,
					"）"
				}), ex);
			}
			finally
			{
				if (!Information.IsNothing(stream))
				{
					stream.Dispose();
				}
				if (!Information.IsNothing(httpWebResponse))
				{
					httpWebResponse.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00056280 File Offset: 0x00054480
		public static object NetGetCodeByRequestRetry(string Url, Encoding Encode = null, string Accept = "", bool IsJson = false, string BackupUrl = null, bool UseBrowserUserAgent = false)
		{
			int num = 0;
			Exception ex = null;
			long timeTick = ModBase.GetTimeTick();
			checked
			{
				object result;
				try
				{
					IL_0A:
					if (num != 0)
					{
						if (num != 1)
						{
							if (ModBase.GetTimeTick() - timeTick <= 5500L)
							{
								throw ex;
							}
							Thread.Sleep(500);
							result = ModNet.NetGetCodeByRequestOnce(BackupUrl ?? Url, Encode, 4000, IsJson, Accept, UseBrowserUserAgent);
						}
						else
						{
							Thread.Sleep(500);
							result = ModNet.NetGetCodeByRequestOnce(BackupUrl ?? Url, Encode, 30000, IsJson, Accept, UseBrowserUserAgent);
						}
					}
					else
					{
						result = ModNet.NetGetCodeByRequestOnce(Url, Encode, 10000, IsJson, Accept, UseBrowserUserAgent);
					}
				}
				catch (ThreadInterruptedException ex2)
				{
					throw;
				}
				catch (Exception ex3)
				{
					if (num == 0)
					{
						ex = ex3;
						num++;
						goto IL_0A;
					}
					if (num != 1)
					{
						throw;
					}
					num++;
					goto IL_0A;
				}
				return result;
			}
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00056364 File Offset: 0x00054564
		public static object NetGetCodeByRequestMultiple(string Url, Encoding Encode = null, string Accept = "", bool IsJson = false)
		{
			ModNet._Closure$__6-0 CS$<>8__locals1 = new ModNet._Closure$__6-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_Url = Url;
			CS$<>8__locals1.$VB$Local_Encode = Encode;
			CS$<>8__locals1.$VB$Local_Accept = Accept;
			CS$<>8__locals1.$VB$Local_IsJson = IsJson;
			List<Thread> list = new List<Thread>();
			CS$<>8__locals1.$VB$Local_RequestResult = null;
			CS$<>8__locals1.$VB$Local_RequestEx = null;
			CS$<>8__locals1.$VB$Local_FailCount = 0;
			int num = 1;
			checked
			{
				do
				{
					Thread thread = new Thread((CS$<>8__locals1.$I0 == null) ? (CS$<>8__locals1.$I0 = delegate()
					{
						try
						{
							CS$<>8__locals1.$VB$Local_RequestResult = RuntimeHelpers.GetObjectValue(ModNet.NetGetCodeByRequestOnce(CS$<>8__locals1.$VB$Local_Url, CS$<>8__locals1.$VB$Local_Encode, 30000, CS$<>8__locals1.$VB$Local_IsJson, CS$<>8__locals1.$VB$Local_Accept, false));
						}
						catch (Exception $VB$Local_RequestEx)
						{
							CS$<>8__locals1.$VB$Local_FailCount++;
							CS$<>8__locals1.$VB$Local_RequestEx = $VB$Local_RequestEx;
						}
					}) : CS$<>8__locals1.$I0);
					thread.Start();
					list.Add(thread);
					Thread.Sleep(num * 250);
					if (CS$<>8__locals1.$VB$Local_RequestResult != null)
					{
						goto IL_114;
					}
					num++;
				}
				while (num <= 4);
				while (CS$<>8__locals1.$VB$Local_RequestResult == null)
				{
					if (CS$<>8__locals1.$VB$Local_FailCount == 4)
					{
						try
						{
							try
							{
								foreach (Thread thread2 in list)
								{
									if (thread2.IsAlive)
									{
										thread2.Interrupt();
									}
								}
							}
							finally
							{
								List<Thread>.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
						}
						catch (Exception ex)
						{
						}
						throw CS$<>8__locals1.$VB$Local_RequestEx;
					}
					Thread.Sleep(20);
				}
				IL_114:
				try
				{
					try
					{
						foreach (Thread thread3 in list)
						{
							if (thread3.IsAlive)
							{
								thread3.Interrupt();
							}
						}
					}
					finally
					{
						List<Thread>.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
				}
				catch (Exception ex2)
				{
				}
				return CS$<>8__locals1.$VB$Local_RequestResult;
			}
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0005650C File Offset: 0x0005470C
		public static object NetGetCodeByRequestOnce(string Url, Encoding Encode = null, int Timeout = 30000, bool IsJson = false, string Accept = "", bool UseBrowserUserAgent = false)
		{
			if (ModBase.RunInUi() && !Url.Contains("//127."))
			{
				throw new Exception("在 UI 线程执行了网络请求");
			}
			Url = Conversions.ToString(ModSecret.SecretCdnSign(Url));
			ModBase.Log(string.Format("[Net] 获取网络结果：{0}，超时 {1}ms{2}", Url, Timeout, IsJson ? "，要求 json" : ""), ModBase.LogLevel.Normal, "出现错误");
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
			new List<byte>();
			object result;
			try
			{
				if (Url.StartsWithF("https", true))
				{
					httpWebRequest.ProtocolVersion = HttpVersion.Version11;
				}
				httpWebRequest.Timeout = Timeout;
				httpWebRequest.Accept = Accept;
				ModSecret.SecretHeadersSign(Url, ref httpWebRequest, UseBrowserUserAgent);
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
				{
					using (Stream responseStream = httpWebResponse.GetResponseStream())
					{
						responseStream.ReadTimeout = Timeout;
						using (StreamReader streamReader = new StreamReader(responseStream, Encode ?? Encoding.UTF8))
						{
							string text = streamReader.ReadToEnd();
							result = (IsJson ? ModBase.GetJson(text) : text);
						}
					}
				}
			}
			catch (ThreadInterruptedException ex)
			{
				throw;
			}
			catch (Exception ex2)
			{
				if (ex2 is WebException && ((WebException)ex2).Status == 14)
				{
					throw new TimeoutException(string.Format("获取结果失败（{0}，{1}，{2}）", ((WebException)ex2).Status, ex2.Message, Url), ex2);
				}
				throw new WebException(string.Format("获取结果失败（{0}{1}，{2}）", (ex2 is WebException) ? (Conversions.ToString(((WebException)ex2).Status) + "，") : "", ex2.Message, Url), ex2);
			}
			finally
			{
				httpWebRequest.Abort();
			}
			return result;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00056754 File Offset: 0x00054954
		public static string NetGetCodeByDownload(string Url, int Timeout = 45000, bool IsJson = false, bool UseBrowserUserAgent = false)
		{
			string text = string.Concat(new string[]
			{
				ModBase.m_DecoratorRepository,
				"Cache\\Code\\",
				Conversions.ToString(Url.GetHashCode()),
				"_",
				Conversions.ToString(ModBase.GetUuid())
			});
			ModNet.LoaderDownload loaderDownload = new ModNet.LoaderDownload("源码获取 " + Conversions.ToString(ModBase.GetUuid()) + "#", new List<ModNet.NetFile>
			{
				new ModNet.NetFile(new string[]
				{
					Url
				}, text, new ModBase.FileChecker(-1L, -1L, null, true, false)
				{
					m_RequestError = IsJson
				}, UseBrowserUserAgent)
			});
			string result;
			try
			{
				loaderDownload.WaitForExitTime(Timeout, null, "连接服务器超时（" + Url + "）", null, false);
				result = ModBase.ReadFile(text, null);
				File.Delete(text);
			}
			finally
			{
				loaderDownload.Abort();
			}
			return result;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00056840 File Offset: 0x00054A40
		public static string NetGetCodeByDownload(IEnumerable<string> Urls, int Timeout = 45000, bool IsJson = false, bool UseBrowserUserAgent = false)
		{
			string text = string.Concat(new string[]
			{
				ModBase.m_DecoratorRepository,
				"Cache\\Code\\",
				Conversions.ToString(Enumerable.First<string>(Urls).GetHashCode()),
				"_",
				Conversions.ToString(ModBase.GetUuid())
			});
			ModNet.LoaderDownload loaderDownload = new ModNet.LoaderDownload("源码获取 " + Conversions.ToString(ModBase.GetUuid()) + "#", new List<ModNet.NetFile>
			{
				new ModNet.NetFile(Urls, text, new ModBase.FileChecker(-1L, -1L, null, true, false)
				{
					m_RequestError = IsJson
				}, UseBrowserUserAgent)
			});
			string result;
			try
			{
				loaderDownload.WaitForExitTime(Timeout, null, "连接服务器超时（第一下载源：" + Enumerable.First<string>(Urls) + "）", null, false);
				result = ModBase.ReadFile(text, null);
				File.Delete(text);
			}
			finally
			{
				loaderDownload.Abort();
			}
			return result;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0005692C File Offset: 0x00054B2C
		public static void NetDownload(string Url, string LocalFile, bool UseBrowserUserAgent = false)
		{
			ModBase.Log("[Net] 直接下载文件：" + Url, ModBase.LogLevel.Normal, "出现错误");
			try
			{
				Directory.CreateDirectory(ModBase.GetPathFromFullPath(LocalFile));
				File.Delete(LocalFile);
			}
			catch (Exception ex)
			{
				throw new WebException("预处理下载文件路径失败（" + LocalFile + "）。", ex);
			}
			using (WebClient webClient = new WebClient())
			{
				try
				{
					WebClient webClient2 = webClient;
					ModSecret.SecretHeadersSign(Url, ref webClient2, UseBrowserUserAgent);
					webClient.DownloadFile(Url, LocalFile);
				}
				catch (Exception ex2)
				{
					File.Delete(LocalFile);
					throw new WebException("直接下载文件失败（" + Url + "）。", ex2);
				}
			}
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000569F4 File Offset: 0x00054BF4
		public static string NetRequestRetry(string Url, string Method, object Data, string ContentType, bool DontRetryOnRefused = true, Dictionary<string, string> Headers = null)
		{
			int num = 0;
			Exception ex = null;
			long timeTick = ModBase.GetTimeTick();
			checked
			{
				string result;
				try
				{
					IL_0A:
					if (num != 0)
					{
						if (num != 1)
						{
							if (ModBase.GetTimeTick() - timeTick <= 5500L)
							{
								throw ex;
							}
							Thread.Sleep(500);
							result = ModNet.NetRequestOnce(Url, Method, RuntimeHelpers.GetObjectValue(Data), ContentType, 4000, Headers, true, false);
						}
						else
						{
							Thread.Sleep(500);
							result = ModNet.NetRequestOnce(Url, Method, RuntimeHelpers.GetObjectValue(Data), ContentType, 25000, Headers, true, false);
						}
					}
					else
					{
						result = ModNet.NetRequestOnce(Url, Method, RuntimeHelpers.GetObjectValue(Data), ContentType, 15000, Headers, true, false);
					}
				}
				catch (ThreadInterruptedException ex2)
				{
					throw;
				}
				catch (Exception ex3)
				{
					if (ex3.InnerException != null && ex3.InnerException.Message.Contains("(40") && DontRetryOnRefused)
					{
						throw;
					}
					if (num == 0)
					{
						if (ModBase._TokenRepository)
						{
							ModBase.Log(ex3, "[Net] 网络请求第一次失败（" + Url + "）", ModBase.LogLevel.Debug, "出现错误");
						}
						ex = ex3;
						num++;
						goto IL_0A;
					}
					if (num != 1)
					{
						throw;
					}
					if (ModBase._TokenRepository)
					{
						ModBase.Log(ex3, "[Net] 网络请求第二次失败（" + Url + "）", ModBase.LogLevel.Debug, "出现错误");
					}
					num++;
					goto IL_0A;
				}
				return result;
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00056B58 File Offset: 0x00054D58
		public static object NetRequestMultiple(string Url, string Method, object Data, string ContentType, int RequestCount = 4, Dictionary<string, string> Headers = null, bool MakeLog = true)
		{
			ModNet._Closure$__12-0 CS$<>8__locals1 = new ModNet._Closure$__12-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_Url = Url;
			CS$<>8__locals1.$VB$Local_Method = Method;
			CS$<>8__locals1.$VB$Local_Data = Data;
			CS$<>8__locals1.$VB$Local_ContentType = ContentType;
			CS$<>8__locals1.$VB$Local_Headers = Headers;
			CS$<>8__locals1.$VB$Local_MakeLog = MakeLog;
			List<Thread> list = new List<Thread>();
			CS$<>8__locals1.$VB$Local_RequestResult = null;
			CS$<>8__locals1.$VB$Local_RequestEx = null;
			CS$<>8__locals1.$VB$Local_FailCount = 0;
			checked
			{
				for (int i = 1; i <= RequestCount; i++)
				{
					Thread thread = new Thread((CS$<>8__locals1.$I0 == null) ? (CS$<>8__locals1.$I0 = delegate()
					{
						try
						{
							CS$<>8__locals1.$VB$Local_RequestResult = ModNet.NetRequestOnce(CS$<>8__locals1.$VB$Local_Url, CS$<>8__locals1.$VB$Local_Method, RuntimeHelpers.GetObjectValue(CS$<>8__locals1.$VB$Local_Data), CS$<>8__locals1.$VB$Local_ContentType, 30000, CS$<>8__locals1.$VB$Local_Headers, CS$<>8__locals1.$VB$Local_MakeLog, false);
						}
						catch (Exception $VB$Local_RequestEx)
						{
							CS$<>8__locals1.$VB$Local_FailCount++;
							CS$<>8__locals1.$VB$Local_RequestEx = $VB$Local_RequestEx;
						}
					}) : CS$<>8__locals1.$I0);
					thread.Start();
					list.Add(thread);
					Thread.Sleep(i * 250);
					if (CS$<>8__locals1.$VB$Local_RequestResult != null)
					{
						IL_119:
						try
						{
							foreach (Thread thread2 in list)
							{
								if (thread2.IsAlive)
								{
									thread2.Interrupt();
								}
							}
						}
						finally
						{
							List<Thread>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
						return CS$<>8__locals1.$VB$Local_RequestResult;
					}
				}
				while (CS$<>8__locals1.$VB$Local_RequestResult == null)
				{
					if (CS$<>8__locals1.$VB$Local_FailCount == RequestCount)
					{
						try
						{
							foreach (Thread thread3 in list)
							{
								if (thread3.IsAlive)
								{
									thread3.Interrupt();
								}
							}
						}
						finally
						{
							List<Thread>.Enumerator enumerator2;
							((IDisposable)enumerator2).Dispose();
						}
						throw CS$<>8__locals1.$VB$Local_RequestEx;
					}
					Thread.Sleep(20);
				}
				goto IL_119;
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00056CE0 File Offset: 0x00054EE0
		public static string NetRequestOnce(string Url, string Method, byte[] Data, string ContentType, int Timeout = 25000, Dictionary<string, string> Headers = null, bool MakeLog = true, bool UseBrowserUserAgent = false)
		{
			if (ModBase.RunInUi() && !Url.Contains("//127."))
			{
				throw new Exception("在 UI 线程执行了网络请求");
			}
			Url = Conversions.ToString(ModSecret.SecretCdnSign(Url));
			if (MakeLog)
			{
				ModBase.Log(string.Concat(new string[]
				{
					"[Net] 发起网络请求（",
					Method,
					"，",
					Url,
					"），最大超时 ",
					Conversions.ToString(Timeout)
				}), ModBase.LogLevel.Normal, "出现错误");
			}
			Stream stream = null;
			WebResponse webResponse = null;
			string result;
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
				httpWebRequest.Method = Method;
				byte[] array;
				if (Data is byte[])
				{
					array = (byte[])Data;
				}
				else
				{
					array = new UTF8Encoding(false).GetBytes(Data.ToString());
				}
				if (Headers != null)
				{
					try
					{
						foreach (KeyValuePair<string, string> keyValuePair in Headers)
						{
							httpWebRequest.Headers.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
					finally
					{
						Dictionary<string, string>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
				httpWebRequest.ContentType = ContentType;
				httpWebRequest.Timeout = Timeout;
				ModSecret.SecretHeadersSign(Url, ref httpWebRequest, UseBrowserUserAgent);
				if (Url.StartsWithF("https", true))
				{
					httpWebRequest.ProtocolVersion = HttpVersion.Version11;
				}
				if (Operators.CompareString(Method, "POST", false) == 0 || Operators.CompareString(Method, "PUT", false) == 0)
				{
					httpWebRequest.ContentLength = (long)array.Length;
					stream = httpWebRequest.GetRequestStream();
					stream.WriteTimeout = Timeout;
					stream.ReadTimeout = Timeout;
					stream.Write(array, 0, array.Length);
					stream.Close();
				}
				webResponse = httpWebRequest.GetResponse();
				stream = webResponse.GetResponseStream();
				stream.WriteTimeout = Timeout;
				stream.ReadTimeout = Timeout;
				using (StreamReader streamReader = new StreamReader(stream))
				{
					result = streamReader.ReadToEnd();
				}
			}
			catch (ThreadInterruptedException ex)
			{
				throw;
			}
			catch (WebException ex2)
			{
				if (ex2.Status == 14)
				{
					ex2 = new WebException(string.Format("连接服务器超时，请检查你的网络环境是否良好（{0}，{1}）", ex2.Message, Url), ex2);
				}
				else
				{
					string text = "";
					try
					{
						if (ex2.Response == null)
						{
							ProjectData.ClearProjectError();
						}
						else
						{
							stream = ex2.Response.GetResponseStream();
							stream.WriteTimeout = Timeout;
							stream.ReadTimeout = Timeout;
							using (StreamReader streamReader2 = new StreamReader(stream))
							{
								text = streamReader2.ReadToEnd();
							}
						}
					}
					catch (Exception ex3)
					{
					}
					if (Operators.CompareString(text, "", false) == 0)
					{
						ex2 = new WebException(string.Format("网络请求失败（{0}，{1}，{2}）", ex2.Status, ex2.Message, Url), ex2);
					}
					else
					{
						ex2 = new ModNet.ResponsedWebException(string.Format("服务器返回错误（{0}，{1}，{2}）{3}{4}", new object[]
						{
							ex2.Status,
							ex2.Message,
							Url,
							"\r\n",
							text
						}), text, ex2);
					}
				}
				if (MakeLog)
				{
					ModBase.Log(ex2, "NetRequestOnce 失败", ModBase.LogLevel.Developer, "出现错误");
				}
				throw ex2;
			}
			catch (Exception ex4)
			{
				ex4 = new WebException("网络请求失败（" + Url + "）", ex4);
				if (MakeLog)
				{
					ModBase.Log(ex4, "NetRequestOnce 失败", ModBase.LogLevel.Developer, "出现错误");
				}
				throw ex4;
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
				}
				if (webResponse != null)
				{
					webResponse.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x000570EC File Offset: 0x000552EC
		public static bool HasDownloadingTask(bool IgnoreCustomDownload = false)
		{
			try
			{
				foreach (ModLoader.LoaderBase loaderBase in Enumerable.ToList<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar))
				{
					if (loaderBase.Show && loaderBase.State == ModBase.LoadState.Loading && (!IgnoreCustomDownload || !loaderBase.Name.ToString().Contains("自定义下载")))
					{
						return true;
					}
				}
			}
			finally
			{
				List<ModLoader.LoaderBase>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			return false;
		}

		// Token: 0x0400068A RID: 1674
		public static int m_StateTests;

		// Token: 0x0400068B RID: 1675
		public static long callbackTests = 262144L;

		// Token: 0x0400068C RID: 1676
		public static long m_TemplateTests = -1L;

		// Token: 0x0400068D RID: 1677
		public static long m_MethodTests = -1L;

		// Token: 0x0400068E RID: 1678
		private static readonly object taskTests = RuntimeHelpers.GetObjectValue(new object());

		// Token: 0x0400068F RID: 1679
		private static long m_ConsumerTests;

		// Token: 0x04000690 RID: 1680
		public static int _ConfigurationTests = 0;

		// Token: 0x04000691 RID: 1681
		private static readonly object _GetterTests = RuntimeHelpers.GetObjectValue(new object());

		// Token: 0x04000692 RID: 1682
		public static ModNet.NetManagerClass tokenTests = new ModNet.NetManagerClass();

		// Token: 0x02000130 RID: 304
		public class ResponsedWebException : WebException
		{
			// Token: 0x06000CFE RID: 3326 RVA: 0x000086F0 File Offset: 0x000068F0
			[CompilerGenerated]
			public string GetMapper()
			{
				return this._StatusTest;
			}

			// Token: 0x06000CFF RID: 3327 RVA: 0x000086F8 File Offset: 0x000068F8
			[CompilerGenerated]
			public void FindMapper(string AutoPropertyValue)
			{
				this._StatusTest = AutoPropertyValue;
			}

			// Token: 0x06000D00 RID: 3328 RVA: 0x00008701 File Offset: 0x00006901
			public ResponsedWebException(string Message, string Response, Exception InnerException) : base(Message, InnerException)
			{
				this.FindMapper(Response);
			}

			// Token: 0x04000693 RID: 1683
			[CompilerGenerated]
			private string _StatusTest;
		}

		// Token: 0x02000131 RID: 305
		public class NetSource
		{
			// Token: 0x06000D03 RID: 3331 RVA: 0x00008713 File Offset: 0x00006913
			public override string ToString()
			{
				return this.m_StructTest;
			}

			// Token: 0x04000694 RID: 1684
			public int roleTest;

			// Token: 0x04000695 RID: 1685
			public string m_StructTest;

			// Token: 0x04000696 RID: 1686
			public int _PrinterTest;

			// Token: 0x04000697 RID: 1687
			public Exception _ValTest;

			// Token: 0x04000698 RID: 1688
			public ModNet.NetThread m_AttrTest;

			// Token: 0x04000699 RID: 1689
			public bool candidateTest;
		}

		// Token: 0x02000132 RID: 306
		public enum NetState
		{
			// Token: 0x0400069B RID: 1691
			WaitForCheck = -1,
			// Token: 0x0400069C RID: 1692
			WaitForDownload,
			// Token: 0x0400069D RID: 1693
			Connect,
			// Token: 0x0400069E RID: 1694
			Get,
			// Token: 0x0400069F RID: 1695
			Download,
			// Token: 0x040006A0 RID: 1696
			Merge,
			// Token: 0x040006A1 RID: 1697
			WaitForCopy,
			// Token: 0x040006A2 RID: 1698
			Finish,
			// Token: 0x040006A3 RID: 1699
			Error
		}

		// Token: 0x02000133 RID: 307
		public enum NetPreDownloadBehaviour
		{
			// Token: 0x040006A5 RID: 1701
			HintWhileExists,
			// Token: 0x040006A6 RID: 1702
			ExitWhileExistsOrDownloading,
			// Token: 0x040006A7 RID: 1703
			IgnoreCheck
		}

		// Token: 0x02000134 RID: 308
		public class NetThread : IEnumerable<ModNet.NetThread>
		{
			// Token: 0x06000D05 RID: 3333 RVA: 0x00057170 File Offset: 0x00055370
			public NetThread()
			{
				this.wrapperTest = 0L;
				this.m_BaseTest = ModBase.GetTimeTick();
				this.attributeTest = 0L;
				this._CodeTest = 0L;
				this._PrototypeTest = ModBase.GetTimeTick();
				this._AnnotationTest = -1L;
				this.infoTest = ModNet.NetState.WaitForDownload;
			}

			// Token: 0x06000D06 RID: 3334 RVA: 0x0000871B File Offset: 0x0000691B
			private IEnumerable<ModNet.NetThread> RestartMapper()
			{
				ModNet.NetThread.VB$StateMachine_5_get_Next vb$StateMachine_5_get_Next = new ModNet.NetThread.VB$StateMachine_5_get_Next(-2);
				vb$StateMachine_5_get_Next.$VB$Me = this;
				return vb$StateMachine_5_get_Next;
			}

			// Token: 0x06000D07 RID: 3335 RVA: 0x0000872B File Offset: 0x0000692B
			public IEnumerator<ModNet.NetThread> GetEnumerator()
			{
				return this.RestartMapper().GetEnumerator();
			}

			// Token: 0x06000D08 RID: 3336 RVA: 0x0000872B File Offset: 0x0000692B
			IEnumerator IEnumerable.IEnumerable_GetEnumerator()
			{
				return this.RestartMapper().GetEnumerator();
			}

			// Token: 0x06000D09 RID: 3337 RVA: 0x00008738 File Offset: 0x00006938
			public bool InitMapper()
			{
				return this._ModelTest == 0L && this._AdvisorTest.filterTest == -2L;
			}

			// Token: 0x06000D0A RID: 3338 RVA: 0x000571E0 File Offset: 0x000553E0
			public long AddMapper()
			{
				object proxyTest = this._AdvisorTest._ProxyTest;
				ObjectFlowControl.CheckForSyncLockOnValueType(proxyTest);
				checked
				{
					long result;
					lock (proxyTest)
					{
						if (this.m_QueueTest == null)
						{
							if (this._AdvisorTest.databaseTest)
							{
								result = 5368709120L;
							}
							else
							{
								result = this._AdvisorTest.filterTest - 1L;
							}
						}
						else
						{
							result = this.m_QueueTest._ModelTest - 1L;
						}
					}
					return result;
				}
			}

			// Token: 0x06000D0B RID: 3339 RVA: 0x0000875A File Offset: 0x0000695A
			public long PublishMapper()
			{
				return checked(this.AddMapper() - (this._ModelTest + this.wrapperTest) + 1L);
			}

			// Token: 0x06000D0C RID: 3340 RVA: 0x00057278 File Offset: 0x00055478
			public long PatchMapper()
			{
				checked
				{
					if (ModBase.GetTimeTick() - this.m_BaseTest > 200L)
					{
						long num = ModBase.GetTimeTick() - this.m_BaseTest;
						this._CodeTest = (long)Math.Round((double)(this.wrapperTest - this.attributeTest) / ((double)num / 1000.0));
						this.attributeTest = this.wrapperTest;
						ref long ptr = ref this.m_BaseTest;
						this.m_BaseTest = ptr + num;
					}
					return this._CodeTest;
				}
			}

			// Token: 0x06000D0D RID: 3341 RVA: 0x0000877A File Offset: 0x0000697A
			public bool ConnectMapper()
			{
				return this.infoTest == ModNet.NetState.Finish || this.infoTest == ModNet.NetState.Error;
			}

			// Token: 0x040006A8 RID: 1704
			public ModNet.NetFile _AdvisorTest;

			// Token: 0x040006A9 RID: 1705
			public Thread m_AccountTest;

			// Token: 0x040006AA RID: 1706
			public ModNet.NetThread m_QueueTest;

			// Token: 0x040006AB RID: 1707
			public int m_EventTest;

			// Token: 0x040006AC RID: 1708
			public string _ManagerTest;

			// Token: 0x040006AD RID: 1709
			public long _ModelTest;

			// Token: 0x040006AE RID: 1710
			public long wrapperTest;

			// Token: 0x040006AF RID: 1711
			private long m_BaseTest;

			// Token: 0x040006B0 RID: 1712
			private long attributeTest;

			// Token: 0x040006B1 RID: 1713
			private long _CodeTest;

			// Token: 0x040006B2 RID: 1714
			public long _PrototypeTest;

			// Token: 0x040006B3 RID: 1715
			public long _AnnotationTest;

			// Token: 0x040006B4 RID: 1716
			public ModNet.NetState infoTest;

			// Token: 0x040006B5 RID: 1717
			public ModNet.NetSource Source;
		}

		// Token: 0x02000136 RID: 310
		public class NetFile
		{
			// Token: 0x06000D18 RID: 3352 RVA: 0x000573A4 File Offset: 0x000555A4
			private ModNet.NetSource GetSource(int Id = 0)
			{
				if (Id >= Enumerable.Count<ModNet.NetSource>(this.facadeTest) || Id < 0)
				{
					Id = 0;
				}
				object messageTest = this._MessageTest;
				ObjectFlowControl.CheckForSyncLockOnValueType(messageTest);
				checked
				{
					ModNet.NetSource result;
					lock (messageTest)
					{
						if (!this.IsSourceFailed(false))
						{
							ModNet.NetSource netSource = this.facadeTest[Id];
							while (netSource.candidateTest)
							{
								Id++;
								if (Id >= Enumerable.Count<ModNet.NetSource>(this.facadeTest))
								{
									Id = 0;
								}
								netSource = this.facadeTest[Id];
							}
							result = netSource;
						}
						else if (Enumerable.Any<ModNet.NetSource>(this._MerchantTest))
						{
							result = this._MerchantTest[0];
						}
						else
						{
							result = null;
						}
					}
					return result;
				}
			}

			// Token: 0x06000D19 RID: 3353 RVA: 0x00057458 File Offset: 0x00055658
			public bool IsSourceFailed(bool AllowOnceSource = true)
			{
				checked
				{
					bool result;
					if (AllowOnceSource && Enumerable.Any<ModNet.NetSource>(this._MerchantTest))
					{
						result = false;
					}
					else
					{
						object messageTest = this._MessageTest;
						ObjectFlowControl.CheckForSyncLockOnValueType(messageTest);
						lock (messageTest)
						{
							ModNet.NetSource[] array = this.facadeTest;
							for (int i = 0; i < array.Length; i++)
							{
								if (!array[i].candidateTest)
								{
									return false;
								}
							}
						}
						result = true;
					}
					return result;
				}
			}

			// Token: 0x06000D1A RID: 3354 RVA: 0x000087C2 File Offset: 0x000069C2
			public bool CollectMapper()
			{
				return this.databaseTest || this.filterTest < 262144L;
			}

			// Token: 0x06000D1B RID: 3355 RVA: 0x000574D8 File Offset: 0x000556D8
			public long CreateMapper()
			{
				checked
				{
					if (ModBase.GetTimeTick() - this.m_ContainerTest > 200L)
					{
						long num = ModBase.GetTimeTick() - this.m_ContainerTest;
						this.m_DispatcherTest = (long)Math.Round((double)(this._PoolTest - this.paramsTest) / ((double)num / 1000.0));
						this.paramsTest = this._PoolTest;
						ref long ptr = ref this.m_ContainerTest;
						this.m_ContainerTest = ptr + num;
					}
					return this.m_DispatcherTest;
				}
			}

			// Token: 0x1700020A RID: 522
			// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00057550 File Offset: 0x00055750
			public double Progress
			{
				get
				{
					double result;
					switch (this._ComparatorTest)
					{
					case ModNet.NetState.WaitForCheck:
						result = 0.0;
						break;
					case ModNet.NetState.WaitForDownload:
						result = 0.01;
						break;
					case ModNet.NetState.Connect:
						result = 0.02;
						break;
					case ModNet.NetState.Get:
						result = 0.04;
						break;
					case ModNet.NetState.Download:
					{
						double num = this.databaseTest ? 0.5 : ((double)this._PoolTest / (double)Math.Max(this.filterTest, 1L));
						num = 1.0 - Math.Pow(1.0 - num, 0.9);
						result = num * 0.93 + 0.05;
						break;
					}
					case ModNet.NetState.Merge:
						result = 0.99;
						break;
					case ModNet.NetState.WaitForCopy:
						result = 0.2;
						break;
					case ModNet.NetState.Finish:
					case ModNet.NetState.Error:
						result = 1.0;
						break;
					default:
						throw new ArgumentOutOfRangeException("文件状态未知：" + Conversions.ToString((int)this._ComparatorTest));
					}
					return result;
				}
			}

			// Token: 0x06000D1D RID: 3357 RVA: 0x00057678 File Offset: 0x00055878
			private int StartMapper()
			{
				object obj = this.serviceTest;
				ObjectFlowControl.CheckForSyncLockOnValueType(obj);
				int result;
				lock (obj)
				{
					result = checked((int)Math.Round((this.parameterTest == 0) ? -1.0 : ((double)this.recordTest / (double)this.parameterTest)));
				}
				return result;
			}

			// Token: 0x06000D1E RID: 3358 RVA: 0x000576E4 File Offset: 0x000558E4
			public override bool Equals(object obj)
			{
				ModNet.NetFile netFile = obj as ModNet.NetFile;
				return netFile != null && this.creatorTest == netFile.creatorTest;
			}

			// Token: 0x06000D1F RID: 3359 RVA: 0x0005770C File Offset: 0x0005590C
			public NetFile(IEnumerable<string> Urls, string LocalPath, ModBase.FileChecker Check = null, bool UseBrowserUserAgent = false)
			{
				this._AdapterTest = new ModBase.SafeList<ModNet.LoaderDownload>();
				this.listTest = 0;
				this._MerchantTest = new List<ModNet.NetSource>();
				this.m_AuthenticationTest = null;
				this._AlgoTest = null;
				this._ComparatorTest = ModNet.NetState.WaitForCheck;
				this._MappingTest = new List<Exception>();
				this.filterTest = -2L;
				this.databaseTest = false;
				this._PoolTest = 0L;
				this.customerTest = RuntimeHelpers.GetObjectValue(new object());
				this.m_ContainerTest = ModBase.GetTimeTick();
				this.paramsTest = 0L;
				this.m_DispatcherTest = 0L;
				this.m_ProcessTest = false;
				this.parameterTest = 0;
				this.recordTest = 0L;
				this.serviceTest = RuntimeHelpers.GetObjectValue(new object());
				this.m_InvocationTest = RuntimeHelpers.GetObjectValue(new object());
				this._ProxyTest = RuntimeHelpers.GetObjectValue(new object());
				this._MessageTest = RuntimeHelpers.GetObjectValue(new object());
				this.creatorTest = ModBase.GetUuid();
				List<ModNet.NetSource> list = new List<ModNet.NetSource>();
				int num = 0;
				Urls = Enumerable.ToArray<string>(Enumerable.Distinct<string>(Urls));
				checked
				{
					try
					{
						foreach (string text in Urls)
						{
							list.Add(new ModNet.NetSource
							{
								_PrinterTest = 0,
								m_StructTest = Conversions.ToString(ModSecret.SecretCdnSign(text.Replace("\r", "").Replace("\n", "").Trim())),
								roleTest = num,
								candidateTest = false,
								_ValTest = null
							});
							num++;
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
					this.facadeTest = list.ToArray();
					this.m_AuthenticationTest = LocalPath;
					this.m_PageTest = Check;
					this._InterceptorTest = UseBrowserUserAgent;
					this._AlgoTest = ModBase.GetFileNameFromPath(LocalPath);
				}
			}

			// Token: 0x06000D20 RID: 3360 RVA: 0x000578FC File Offset: 0x00055AFC
			public ModNet.NetThread TryBeginThread()
			{
				checked
				{
					ModNet.NetThread result;
					try
					{
						if (ModNet._ConfigurationTests < ModNet.m_StateTests && !this.IsSourceFailed(true))
						{
							if (!this.CollectMapper() || this.m_TokenizerTest == null || this.m_TokenizerTest.infoTest == ModNet.NetState.Error)
							{
								if (this._ComparatorTest < ModNet.NetState.Merge)
								{
									if (this._ComparatorTest != ModNet.NetState.WaitForCheck)
									{
										object invocationTest = this.m_InvocationTest;
										ObjectFlowControl.CheckForSyncLockOnValueType(invocationTest);
										lock (invocationTest)
										{
											if (this._ComparatorTest < ModNet.NetState.Connect)
											{
												this._ComparatorTest = ModNet.NetState.Connect;
											}
										}
										ModNet.NetSource netSource = null;
										object proxyTest = this._ProxyTest;
										ObjectFlowControl.CheckForSyncLockOnValueType(proxyTest);
										Thread thread;
										ModNet.NetThread netThread4;
										lock (proxyTest)
										{
											if (!this.CollectMapper())
											{
												if (!this.IsSourceFailed(false))
												{
													goto IL_16E;
												}
												if (this._MerchantTest[0].m_AttrTest != null && this._MerchantTest[0].m_AttrTest.infoTest != ModNet.NetState.Error)
												{
													return null;
												}
											}
											this.predicateTest = null;
											this.m_TokenizerTest = null;
											ModNet.NetManagerClass tokenTests;
											(tokenTests = ModNet.tokenTests).InterruptMapper(tokenTests.PrintMapper() - this._PoolTest);
											object obj = this.customerTest;
											ObjectFlowControl.CheckForSyncLockOnValueType(obj);
											lock (obj)
											{
												this._PoolTest = 0L;
											}
											this.paramsTest = 0L;
											this._ComparatorTest = ModNet.NetState.Get;
											IL_16E:
											long num;
											if (this.m_TokenizerTest == null)
											{
												num = 0L;
												netSource = this.GetSource(this.listTest);
												this.listTest = netSource.roleTest + 1;
											}
											else
											{
												try
												{
													foreach (ModNet.NetThread netThread in this.m_TokenizerTest)
													{
														if (netThread.infoTest == ModNet.NetState.Error && netThread.PublishMapper() > 0L)
														{
															num = netThread._ModelTest + netThread.wrapperTest;
															netSource = this.GetSource(netThread.Source.roleTest + 1);
															goto IL_30A;
														}
													}
												}
												finally
												{
													IEnumerator<ModNet.NetThread> enumerator;
													if (enumerator != null)
													{
														enumerator.Dispose();
													}
												}
												string structTest = this.GetSource(0).m_StructTest;
												if (structTest.Contains("pcl2-server") || structTest.Contains("mcimirror") || structTest.Contains("github.com") || structTest.Contains("optifine.net") || structTest.Contains("modrinth"))
												{
													return null;
												}
												ModNet.NetThread netThread2 = this.m_TokenizerTest;
												try
												{
													foreach (ModNet.NetThread netThread3 in this.m_TokenizerTest)
													{
														if (netThread3.PublishMapper() > netThread2.PublishMapper())
														{
															netThread2 = netThread3;
														}
													}
												}
												finally
												{
													IEnumerator<ModNet.NetThread> enumerator2;
													if (enumerator2 != null)
													{
														enumerator2.Dispose();
													}
												}
												if (netThread2 == null || netThread2.PublishMapper() < 262144L)
												{
													return null;
												}
												num = (long)Math.Round(unchecked((double)netThread2.AddMapper() - (double)netThread2.PublishMapper() * 0.4));
												netSource = this.GetSource(0);
											}
											IL_30A:
											if ((num > this.filterTest && this.filterTest >= 0L && !this.databaseTest) || num < 0L || Information.IsNothing(netSource))
											{
												return null;
											}
											int uuid = ModBase.GetUuid();
											if (!Enumerable.Any<ModNet.LoaderDownload>(this._AdapterTest))
											{
												return null;
											}
											thread = new Thread(delegate(object a0)
											{
												this.Thread((ModNet.NetThread)a0);
											})
											{
												Name = string.Format("NetTask {0}/{1} Download {2}#", this._AdapterTest[0].Uuid, this.creatorTest, uuid),
												Priority = ThreadPriority.BelowNormal
											};
											netThread4 = new ModNet.NetThread
											{
												m_EventTest = uuid,
												_ModelTest = num,
												m_AccountTest = thread,
												Source = netSource,
												_AdvisorTest = this,
												infoTest = ModNet.NetState.WaitForDownload
											};
											if (!netThread4.InitMapper() && this.m_TokenizerTest != null)
											{
												ModNet.NetThread netThread5 = this.m_TokenizerTest;
												while (netThread5.AddMapper() <= num)
												{
													netThread5 = netThread5.m_QueueTest;
												}
												netThread4.m_QueueTest = netThread5.m_QueueTest;
												netThread5.m_QueueTest = netThread4;
											}
											else
											{
												this.m_TokenizerTest = netThread4;
											}
										}
										object getterTests = ModNet._GetterTests;
										ObjectFlowControl.CheckForSyncLockOnValueType(getterTests);
										lock (getterTests)
										{
											ModNet._ConfigurationTests++;
										}
										object messageTest = this._MessageTest;
										ObjectFlowControl.CheckForSyncLockOnValueType(messageTest);
										lock (messageTest)
										{
											if (this.IsSourceFailed(false))
											{
												this._MerchantTest[0].m_AttrTest = netThread4;
											}
										}
										thread.Start(netThread4);
										return netThread4;
									}
								}
								return null;
							}
						}
						result = null;
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "尝试开始下载线程失败（" + (this._AlgoTest ?? "Nothing") + "）", ModBase.LogLevel.Hint, "出现错误");
						result = null;
					}
					return result;
				}
			}

			// Token: 0x06000D21 RID: 3361 RVA: 0x00057EF4 File Offset: 0x000560F4
			private void Thread(ModNet.NetThread Info)
			{
				if (ModBase._TokenRepository || Info._ModelTest == 0L)
				{
					ModBase.Log(string.Concat(new string[]
					{
						"[Download] ",
						this._AlgoTest,
						" ",
						Conversions.ToString(Info.m_EventTest),
						"#：开始，起始点 ",
						Conversions.ToString(Info._ModelTest),
						"，",
						Info.Source.m_StructTest
					}), ModBase.LogLevel.Normal, "出现错误");
				}
				Stream stream = null;
				checked
				{
					int num = Math.Min(Math.Max(this.StartMapper(), 6000) * (1 + Info.Source._PrinterTest), 30000);
					Info.infoTest = ModNet.NetState.Connect;
					try
					{
						int num2 = 0;
						long num3;
						if (!this._MerchantTest.Contains(Info.Source) || Info.Equals(Info.Source.m_AttrTest))
						{
							HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Info.Source.m_StructTest);
							if (Info.Source.m_StructTest.StartsWithF("https", true))
							{
								httpWebRequest.ProtocolVersion = HttpVersion.Version11;
							}
							httpWebRequest.Timeout = num;
							httpWebRequest.AddRange(Info._ModelTest);
							ModSecret.SecretHeadersSign(Info.Source.m_StructTest, ref httpWebRequest, this._InterceptorTest);
							num3 = 0L;
							using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
							{
								if (this._ComparatorTest != ModNet.NetState.Error)
								{
									if (ModBase._TokenRepository && Operators.CompareString(httpWebResponse.ResponseUri.OriginalString, Info.Source.m_StructTest, false) != 0)
									{
										ModBase.Log(string.Format("[Download] {0} {1}#：重定向至 {2}", this._AlgoTest, Info.m_EventTest, httpWebResponse.ResponseUri.OriginalString), ModBase.LogLevel.Normal, "出现错误");
									}
									num3 = httpWebResponse.ContentLength;
									if (num3 == -1L)
									{
										if (this.filterTest <= 1L)
										{
											this.filterTest = -1L;
											this.databaseTest = true;
											ModBase.Log(string.Format("[Download] {0} {1}#：文件大小未知", this._AlgoTest, Info.m_EventTest), ModBase.LogLevel.Normal, "出现错误");
											goto IL_60B;
										}
										if (Info._ModelTest == 0L)
										{
											ModBase.Log(string.Format("[Download] {0} {1}#：文件大小未知，但已从其他下载源获取，不作处理", this._AlgoTest, Info.m_EventTest), ModBase.LogLevel.Normal, "出现错误");
											goto IL_60B;
										}
										ModBase.Log(string.Format("[Download] {0} {1}#：ContentLength 返回了 -1，无法确定是否支持分段下载，视作不支持", this._AlgoTest, Info.m_EventTest), ModBase.LogLevel.Normal, "出现错误");
									}
									else
									{
										if (num3 < 0L)
										{
											throw new Exception("获取片大小失败，结果为 " + Conversions.ToString(num3) + "。");
										}
										if (Info.InitMapper())
										{
											if (this.m_PageTest != null)
											{
												if (num3 < this.m_PageTest.m_ContextError && this.m_PageTest.m_ContextError > 0L)
												{
													throw new Exception(string.Format("文件大小不足，获取结果为 {0}，要求至少为 {1}。", num3, this.m_PageTest.m_ContextError));
												}
												if (num3 != this.m_PageTest._ErrorError && this.m_PageTest._ErrorError > 0L)
												{
													throw new Exception(string.Format("文件大小不一致，获取结果为 {0}，要求必须为 {1}。", num3, this.m_PageTest._ErrorError));
												}
											}
											this.filterTest = num3;
											this.databaseTest = false;
											ModBase.Log(string.Format("[Download] {0} {1}#：文件大小 {2}（{3}）", new object[]
											{
												this._AlgoTest,
												Info.m_EventTest,
												num3,
												ModBase.GetString(num3)
											}), ModBase.LogLevel.Normal, "出现错误");
											if (num3 > 52428800L)
											{
												foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
												{
													string text = Enumerable.First<char>(driveInfo.Name).ToString();
													double num4 = unchecked((ModBase.m_DecoratorRepository.StartsWithF(text, false) ? ((double)num3 * 1.1) : 0.0) + (double)(this.m_AuthenticationTest.StartsWithF(text, false) ? checked(num3 + 5242880L) : 0L));
													if ((double)driveInfo.TotalFreeSpace < num4)
													{
														throw new Exception(string.Concat(new string[]
														{
															text,
															" 盘空间不足，无法进行下载。\r\n需要至少 ",
															ModBase.GetString((long)Math.Round(num4)),
															" 空间，但当前仅剩余 ",
															ModBase.GetString(driveInfo.TotalFreeSpace),
															"。",
															ModBase.m_DecoratorRepository.StartsWithF(text, false) ? "\r\n\r\n下载时需要与文件同等大小的空间存放缓存，你可以在设置中调整缓存文件夹的位置。" : ""
														}));
													}
												}
												goto IL_60B;
											}
											goto IL_60B;
										}
										else
										{
											if (this.filterTest < 0L)
											{
												throw new Exception("非首线程运行时，尚未获取文件大小");
											}
											if (Info._ModelTest <= 0L || num3 != this.filterTest)
											{
												if (this.filterTest - Info._ModelTest != num3)
												{
													throw new WebException(string.Format("获取到的分段大小不一致：Range 起始于 {0}，预期 ContentLength 为 {1}，返回 ContentLength 为 {2}，总文件大小 {3}", new object[]
													{
														Info._ModelTest,
														this.filterTest - Info._ModelTest,
														num3,
														this.filterTest
													}));
												}
												goto IL_60B;
											}
										}
									}
									object messageTest = this._MessageTest;
									ObjectFlowControl.CheckForSyncLockOnValueType(messageTest);
									lock (messageTest)
									{
										if (this._MerchantTest.Contains(Info.Source))
										{
											goto IL_AF7;
										}
										this._MerchantTest.Add(Info.Source);
									}
									throw new WebException(string.Format("该下载源不支持分段下载：Range 起始于 {0}，预期 ContentLength 为 {1}，返回 ContentLength 为 {2}，总文件大小 {3}", new object[]
									{
										Info._ModelTest,
										this.filterTest - Info._ModelTest,
										num3,
										this.filterTest
									}));
									IL_60B:
									Info.infoTest = ModNet.NetState.Get;
									object invocationTest = this.m_InvocationTest;
									ObjectFlowControl.CheckForSyncLockOnValueType(invocationTest);
									lock (invocationTest)
									{
										if (this._ComparatorTest < ModNet.NetState.Get)
										{
											this._ComparatorTest = ModNet.NetState.Get;
										}
									}
									if (this.CollectMapper())
									{
										Info._ManagerTest = null;
										this.predicateTest = new Queue<byte>();
									}
									else
									{
										Info._ManagerTest = string.Concat(new string[]
										{
											ModBase.m_DecoratorRepository,
											"Download\\",
											Conversions.ToString(this.creatorTest),
											"_",
											Conversions.ToString(Info.m_EventTest),
											"_",
											Conversions.ToString(ModBase.RandomInteger(0, 999999)),
											".tmp"
										});
										stream = new FileStream(Info._ManagerTest, FileMode.Create, FileAccess.Write, FileShare.Read);
									}
									using (Stream responseStream = httpWebResponse.GetResponseStream())
									{
										responseStream.ReadTimeout = num;
										if (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("SystemDebugDelay", null)))
										{
											System.Threading.Thread.Sleep(ModBase.RandomInteger(50, 3000));
										}
										byte[] array = new byte[16385];
										num2 = responseStream.Read(array, 0, 16384);
										int num5;
										long num6;
										for (;;)
										{
											if (!this.databaseTest)
											{
												if (Info.PublishMapper() <= 0L)
												{
													break;
												}
											}
											if (num2 <= 0 || ModBase._ObserverRepository || this._ComparatorTest >= ModNet.NetState.Merge)
											{
												break;
											}
											if (Info.Source.candidateTest && !Info.Equals(Info.Source.m_AttrTest))
											{
												break;
											}
											while (ModNet.m_TemplateTests > 0L && ModNet.m_MethodTests <= 0L)
											{
												System.Threading.Thread.Sleep(16);
											}
											num5 = (int)(unchecked(this.databaseTest ? ((long)num2) : Math.Min((long)num2, Info.PublishMapper())));
											object taskTests = ModNet.taskTests;
											ObjectFlowControl.CheckForSyncLockOnValueType(taskTests);
											lock (taskTests)
											{
												if (ModNet.m_TemplateTests > 0L)
												{
													ModNet.m_MethodTests -= unchecked((long)num5);
												}
											}
											num6 = ModBase.GetTimeTick() - Info._AnnotationTest;
											if (num6 > 1000000L)
											{
												num6 = 1L;
											}
											if (num5 > 0)
											{
												long ptr2;
												if (Info.wrapperTest == 0L)
												{
													Info.infoTest = ModNet.NetState.Download;
													object invocationTest2 = this.m_InvocationTest;
													ObjectFlowControl.CheckForSyncLockOnValueType(invocationTest2);
													lock (invocationTest2)
													{
														if (this._ComparatorTest < ModNet.NetState.Download)
														{
															this._ComparatorTest = ModNet.NetState.Download;
														}
													}
													object obj = this.serviceTest;
													ObjectFlowControl.CheckForSyncLockOnValueType(obj);
													lock (obj)
													{
														ref int ptr = ref this.parameterTest;
														this.parameterTest = ptr + 1;
														ptr2 = ref this.recordTest;
														this.recordTest = ptr2 + (ModBase.GetTimeTick() - Info._PrototypeTest);
													}
												}
												object obj2 = this.serviceTest;
												ObjectFlowControl.CheckForSyncLockOnValueType(obj2);
												lock (obj2)
												{
													Info.Source._PrinterTest = 0;
													try
													{
														foreach (ModNet.LoaderDownload loaderDownload in this._AdapterTest)
														{
															loaderDownload.FailCount = 0;
														}
													}
													finally
													{
														IEnumerator<ModNet.LoaderDownload> enumerator;
														if (enumerator != null)
														{
															enumerator.Dispose();
														}
													}
												}
												ModNet.NetManagerClass tokenTests;
												(tokenTests = ModNet.tokenTests).InterruptMapper(tokenTests.PrintMapper() + unchecked((long)num5));
												object obj3 = this.customerTest;
												ObjectFlowControl.CheckForSyncLockOnValueType(obj3);
												lock (obj3)
												{
													ptr2 = ref this._PoolTest;
													this._PoolTest = ptr2 + unchecked((long)num5);
												}
												ptr2 = ref Info.wrapperTest;
												Info.wrapperTest = ptr2 + unchecked((long)num5);
												if (this.CollectMapper())
												{
													if (Enumerable.Count<byte>(array) == num5)
													{
														foreach (byte b in array)
														{
															this.predicateTest.Enqueue(b);
														}
													}
													else
													{
														int num7 = num5 - 1;
														for (int k = 0; k <= num7; k++)
														{
															this.predicateTest.Enqueue(array[k]);
														}
													}
												}
												else
												{
													stream.Write(array, 0, num5);
												}
												if (num6 > 1500L && num6 > unchecked((long)num5))
												{
													goto IL_A93;
												}
												Info._AnnotationTest = ModBase.GetTimeTick();
												if (Info.PublishMapper() == 0L && !this.databaseTest)
												{
													break;
												}
											}
											else if (Info._AnnotationTest > 0L && num6 > unchecked((long)num))
											{
												goto IL_AD0;
											}
											num2 = responseStream.Read(array, 0, 16384);
										}
										goto IL_ADB;
										IL_A93:
										throw new TimeoutException(string.Concat(new string[]
										{
											"由于速度过慢断开链接，下载 ",
											Conversions.ToString(num5),
											" B，消耗 ",
											Conversions.ToString(num6),
											" ms。"
										}));
										IL_AD0:
										throw new TimeoutException("操作超时，无数据。");
										IL_ADB:;
									}
								}
							}
						}
						IL_AF7:
						if (this._ComparatorTest != ModNet.NetState.Error && !Info.Source.candidateTest && (Info.PublishMapper() <= 0L || this.databaseTest))
						{
							if (num2 == 0 && Info.PublishMapper() > 0L && !this.databaseTest)
							{
								throw new Exception(string.Format("返回的 ContentLength 过多：ContentLength 为 {0}，但获取到的总数据量仅为 {1}（全文件总数据量 {2}）", num3, Info.wrapperTest, this._PoolTest));
							}
							Info.infoTest = ModNet.NetState.Finish;
							if (ModBase._TokenRepository)
							{
								ModBase.Log(string.Format("[Download] {0} {1}#：完成，已下载 {2}", this._AlgoTest, Info.m_EventTest, Info.wrapperTest), ModBase.LogLevel.Normal, "出现错误");
							}
						}
						else
						{
							Info.infoTest = ModNet.NetState.Error;
							ModBase.Log(string.Format("[Download] {0} {1}#：中断", this._AlgoTest, Info.m_EventTest), ModBase.LogLevel.Normal, "出现错误");
						}
					}
					catch (Exception ex)
					{
						object obj4 = this.serviceTest;
						ObjectFlowControl.CheckForSyncLockOnValueType(obj4);
						lock (obj4)
						{
							ModNet.NetSource source = Info.Source;
							ref int ptr = ref source._PrinterTest;
							source._PrinterTest = ptr + 1;
							try
							{
								foreach (ModNet.LoaderDownload loaderDownload2 in this._AdapterTest)
								{
									ModNet.LoaderDownload loaderDownload3;
									(loaderDownload3 = loaderDownload2).FailCount = loaderDownload3.FailCount + 1;
								}
							}
							finally
							{
								IEnumerator<ModNet.LoaderDownload> enumerator2;
								if (enumerator2 != null)
								{
									enumerator2.Dispose();
								}
							}
						}
						string text2 = ModBase.GetExceptionSummary(ex).ToLower().Replace(" ", "");
						bool flag9 = text2.Contains("由于连接方在一段时间后没有正确答复或连接的主机没有反应") || text2.Contains("超时") || text2.Contains("timeout") || text2.Contains("timedout");
						ModBase.Log(string.Concat(new string[]
						{
							"[Download] ",
							this._AlgoTest,
							" ",
							Conversions.ToString(Info.m_EventTest),
							flag9 ? ("#：超时（" + Conversions.ToString(unchecked((double)num * 0.001)) + "s）") : ("#：出错，" + ModBase.GetExceptionDetail(ex, false))
						}), ModBase.LogLevel.Normal, "出现错误");
						Info.infoTest = ModNet.NetState.Error;
						Info.Source._ValTest = ex;
						if (ex.Message.Contains("该下载源不支持") || ex.Message.Contains("未能解析") || ex.Message.Contains("(404)") || ex.Message.Contains("(502)") || ex.Message.Contains("无返回数据") || ex.Message.Contains("空间不足") || ex.Message.Contains("获取到的分段大小不一致") || (ex.Message.Contains("(403)") && !Info.Source.m_StructTest.ContainsF("bmclapi", false)) || ((double)Info.Source._PrinterTest >= ModBase.MathClamp((double)ModNet.m_StateTests, 5.0, 30.0) && this._PoolTest < 1L) || Info.Source._PrinterTest > ModNet.m_StateTests + 2)
						{
							bool flag10 = false;
							object messageTest2 = this._MessageTest;
							ObjectFlowControl.CheckForSyncLockOnValueType(messageTest2);
							lock (messageTest2)
							{
								if (Info.Source.m_AttrTest != null && Info.Source.m_AttrTest.Equals(Info))
								{
									this._MerchantTest.RemoveAt(0);
								}
								else if (Info.Source.candidateTest)
								{
									goto IL_ECF;
								}
								Info.Source.candidateTest = true;
								flag10 = true;
								IL_ECF:;
							}
							if (flag10)
							{
								ModBase.Log(string.Format("[Download] {0} {1}#：下载源被禁用（{2}）：{3}", new object[]
								{
									this._AlgoTest,
									Info.m_EventTest,
									Info.Source.roleTest,
									Info.Source.m_StructTest
								}), ModBase.LogLevel.Normal, "出现错误");
								ModBase.Log(ex, "下载源 " + Conversions.ToString(Info.Source.roleTest) + " 已被禁用", (ex.Message.Contains("不支持分段下载") || ex.Message.Contains("(404)") || ex.Message.Contains("(416)")) ? ModBase.LogLevel.Developer : ModBase.LogLevel.Debug, "出现错误");
								if (this.IsSourceFailed(true))
								{
									ModBase.Log("[Download] 文件 " + this._AlgoTest + " 已无可用下载源", ModBase.LogLevel.Normal, "出现错误");
									Exception raiseEx = null;
									object messageTest3 = this._MessageTest;
									ObjectFlowControl.CheckForSyncLockOnValueType(messageTest3);
									lock (messageTest3)
									{
										foreach (ModNet.NetSource netSource in this.facadeTest)
										{
											ModBase.Log("[Download] 已禁用的下载源：" + netSource.m_StructTest, ModBase.LogLevel.Normal, "出现错误");
											if (netSource._ValTest != null)
											{
												raiseEx = netSource._ValTest;
												ModBase.Log(netSource._ValTest, "下载源禁用原因", ModBase.LogLevel.Developer, "出现错误");
											}
										}
									}
									this.Fail(raiseEx);
								}
								else if (ex.Message.Contains("空间不足"))
								{
									this.Fail(ex);
								}
							}
						}
						if (this.filterTest == -2L)
						{
							object proxyTest = this._ProxyTest;
							ObjectFlowControl.CheckForSyncLockOnValueType(proxyTest);
							lock (proxyTest)
							{
								this.m_TokenizerTest = null;
							}
						}
					}
					finally
					{
						if (stream != null)
						{
							stream.Dispose();
						}
						object getterTests = ModNet._GetterTests;
						ObjectFlowControl.CheckForSyncLockOnValueType(getterTests);
						lock (getterTests)
						{
							ModNet._ConfigurationTests--;
						}
						if (((this.filterTest >= 0L && this._PoolTest >= this.filterTest) || (this.filterTest == -1L && this._PoolTest > 0L)) && this._ComparatorTest < ModNet.NetState.Merge)
						{
							this.Merge();
						}
					}
				}
			}

			// Token: 0x06000D22 RID: 3362 RVA: 0x00059214 File Offset: 0x00057414
			private string GetFileNameFromResponse(HttpWebResponse response)
			{
				string text = response.Headers["Content-Disposition"];
				string result;
				if (string.IsNullOrEmpty(text))
				{
					result = null;
				}
				else if (!text.Contains("filename="))
				{
					result = null;
				}
				else
				{
					result = text.AfterLast("filename=", false).Trim(new char[]
					{
						'"',
						' '
					}).BeforeFirst(";", false);
				}
				return result;
			}

			// Token: 0x06000D23 RID: 3363 RVA: 0x00059280 File Offset: 0x00057480
			private void Merge()
			{
				object invocationTest = this.m_InvocationTest;
				ObjectFlowControl.CheckForSyncLockOnValueType(invocationTest);
				lock (invocationTest)
				{
					if (this._ComparatorTest >= ModNet.NetState.Merge)
					{
						return;
					}
					this._ComparatorTest = ModNet.NetState.Merge;
				}
				int num = 0;
				Stream stream = null;
				BinaryWriter binaryWriter = null;
				checked
				{
					try
					{
						IL_40:
						object proxyTest = this._ProxyTest;
						ObjectFlowControl.CheckForSyncLockOnValueType(proxyTest);
						lock (proxyTest)
						{
							if (File.Exists(this.m_AuthenticationTest))
							{
								File.Delete(this.m_AuthenticationTest);
							}
							new FileInfo(this.m_AuthenticationTest).Directory.Create();
							if (this.CollectMapper())
							{
								if (ModBase._TokenRepository)
								{
									ModBase.Log(string.Format("[Download] {0}：下载结束，从缓存输出文件，长度：", this._AlgoTest) + Conversions.ToString(this.predicateTest.Count), ModBase.LogLevel.Normal, "出现错误");
								}
								stream = new FileStream(this.m_AuthenticationTest, FileMode.Create);
								binaryWriter = new BinaryWriter(stream);
								binaryWriter.Write(this.predicateTest.ToArray());
								binaryWriter.Dispose();
								binaryWriter = null;
								stream.Dispose();
								stream = null;
							}
							else if (this.m_TokenizerTest.wrapperTest == this._PoolTest && this.m_TokenizerTest._ManagerTest != null)
							{
								if (ModBase._TokenRepository)
								{
									ModBase.Log(string.Format("[Download] {0}：下载结束，仅有一个文件，无需合并", this._AlgoTest), ModBase.LogLevel.Normal, "出现错误");
								}
								ModBase.CopyFile(this.m_TokenizerTest._ManagerTest, this.m_AuthenticationTest);
							}
							else
							{
								if (ModBase._TokenRepository)
								{
									ModBase.Log(string.Format("[Download] {0}：下载结束，开始合并文件", this._AlgoTest), ModBase.LogLevel.Normal, "出现错误");
								}
								stream = new FileStream(this.m_AuthenticationTest, FileMode.Create);
								binaryWriter = new BinaryWriter(stream);
								try
								{
									foreach (ModNet.NetThread netThread in this.m_TokenizerTest)
									{
										if (netThread.wrapperTest != 0L && netThread._ManagerTest != null)
										{
											using (FileStream fileStream = new FileStream(netThread._ManagerTest, FileMode.Open, FileAccess.Read, FileShare.Read))
											{
												using (BinaryReader binaryReader = new BinaryReader(fileStream))
												{
													binaryWriter.Write(binaryReader.ReadBytes((int)netThread.wrapperTest));
												}
											}
										}
									}
								}
								finally
								{
									IEnumerator<ModNet.NetThread> enumerator;
									if (enumerator != null)
									{
										enumerator.Dispose();
									}
								}
								binaryWriter.Dispose();
								binaryWriter = null;
								stream.Dispose();
								stream = null;
							}
							if (!this.databaseTest && this.m_PageTest != null)
							{
								if (this.m_PageTest._ErrorError == -1L)
								{
									this.m_PageTest._ErrorError = this.filterTest;
								}
								else if (this.m_PageTest._ErrorError != this.filterTest)
								{
									throw new Exception(string.Format("文件大小不一致：任务要求为 {0} B，网络获取结果为 {1}B", this.m_PageTest._ErrorError, this.filterTest));
								}
							}
							ModBase.FileChecker pageTest = this.m_PageTest;
							string text = (pageTest != null) ? pageTest.Check(this.m_AuthenticationTest) : null;
							if (text != null)
							{
								ModBase.Log(string.Format("[Download] {0} 文件校验失败，下载线程细节：", this._AlgoTest), ModBase.LogLevel.Normal, "出现错误");
								try
								{
									foreach (ModNet.NetThread netThread2 in this.m_TokenizerTest)
									{
										ModBase.Log(string.Format("[Download]     {0}#，状态 {1}，范围 {2}~{3}，完成 {4}，剩余 {5}", new object[]
										{
											netThread2.m_EventTest,
											ModBase.GetStringFromEnum(netThread2.infoTest),
											netThread2._ModelTest,
											netThread2._ModelTest + netThread2.wrapperTest,
											netThread2.wrapperTest,
											netThread2.PublishMapper()
										}), ModBase.LogLevel.Normal, "出现错误");
									}
								}
								finally
								{
									IEnumerator<ModNet.NetThread> enumerator2;
									if (enumerator2 != null)
									{
										enumerator2.Dispose();
									}
								}
								throw new Exception(text);
							}
							if (this.CollectMapper())
							{
								this.predicateTest = null;
							}
							else
							{
								try
								{
									foreach (ModNet.NetThread netThread3 in this.m_TokenizerTest)
									{
										if (netThread3._ManagerTest != null)
										{
											File.Delete(netThread3._ManagerTest);
										}
									}
								}
								finally
								{
									IEnumerator<ModNet.NetThread> enumerator3;
									if (enumerator3 != null)
									{
										enumerator3.Dispose();
									}
								}
							}
							this.Finish(true);
						}
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "合并文件出错（" + this._AlgoTest + "）", ModBase.LogLevel.Debug, "出现错误");
						if (stream != null)
						{
							stream.Dispose();
							stream = null;
						}
						if (binaryWriter != null)
						{
							binaryWriter.Dispose();
							binaryWriter = null;
						}
						if (num <= 3)
						{
							System.Threading.Thread.Sleep(ModBase.RandomInteger(500, 1000));
							num++;
							goto IL_40;
						}
						this.Fail(ex);
					}
				}
			}

			// Token: 0x06000D24 RID: 3364 RVA: 0x000597D8 File Offset: 0x000579D8
			private void Fail(Exception RaiseEx = null)
			{
				object invocationTest = this.m_InvocationTest;
				ObjectFlowControl.CheckForSyncLockOnValueType(invocationTest);
				lock (invocationTest)
				{
					if (this._ComparatorTest >= ModNet.NetState.Finish)
					{
						return;
					}
					if (RaiseEx != null)
					{
						this._MappingTest.Add(RaiseEx);
					}
					this._ComparatorTest = ModNet.NetState.Error;
				}
				this.InterruptAndDelete();
				try
				{
					foreach (ModNet.LoaderDownload loaderDownload in this._AdapterTest)
					{
						loaderDownload.OnFileFail(this);
					}
				}
				finally
				{
					IEnumerator<ModNet.LoaderDownload> enumerator;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
			}

			// Token: 0x06000D25 RID: 3365 RVA: 0x0005987C File Offset: 0x00057A7C
			public void Abort(ModNet.LoaderDownload CausedByTask)
			{
				this._AdapterTest.Remove(CausedByTask);
				if (!Enumerable.Any<ModNet.LoaderDownload>(this._AdapterTest))
				{
					object invocationTest = this.m_InvocationTest;
					ObjectFlowControl.CheckForSyncLockOnValueType(invocationTest);
					lock (invocationTest)
					{
						if (this._ComparatorTest >= ModNet.NetState.Finish)
						{
							return;
						}
						this._ComparatorTest = ModNet.NetState.Error;
					}
					this.InterruptAndDelete();
				}
			}

			// Token: 0x06000D26 RID: 3366 RVA: 0x000598F0 File Offset: 0x00057AF0
			private void InterruptAndDelete()
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
					if (!File.Exists(this.m_AuthenticationTest))
					{
						goto IL_23;
					}
					IL_16:
					num2 = 3;
					File.Delete(this.m_AuthenticationTest);
					IL_23:
					num2 = 4;
					object visitorTest = ModNet.tokenTests.visitorTest;
					ObjectFlowControl.CheckForSyncLockOnValueType(visitorTest);
					lock (visitorTest)
					{
						ModNet.NetManagerClass tokenTests = ModNet.tokenTests;
						ref int ptr = ref tokenTests._CollectionTest;
						tokenTests._CollectionTest = checked(ptr - 1);
						ModBase.Log(string.Format("[Download] {0}：状态 {1}，剩余文件 {2}", this._AlgoTest, this._ComparatorTest, ModNet.tokenTests._CollectionTest), ModBase.LogLevel.Normal, "出现错误");
					}
					IL_96:
					goto IL_F9;
					IL_98:
					int num3 = num4 + 1;
					num4 = 0;
					@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
					IL_BA:
					goto IL_EE;
					IL_BC:
					num4 = num2;
					@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
					IL_CC:;
				}
				catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
				{
					Exception ex = (Exception)obj2;
					goto IL_BC;
				}
				IL_EE:
				throw ProjectData.CreateProjectError(-2146828237);
				IL_F9:
				if (num4 != 0)
				{
					ProjectData.ClearProjectError();
				}
			}

			// Token: 0x06000D27 RID: 3367 RVA: 0x00059A1C File Offset: 0x00057C1C
			public void Finish(bool PrintLog = true)
			{
				object invocationTest = this.m_InvocationTest;
				ObjectFlowControl.CheckForSyncLockOnValueType(invocationTest);
				lock (invocationTest)
				{
					if (this._ComparatorTest >= ModNet.NetState.Finish)
					{
						return;
					}
					this._ComparatorTest = ModNet.NetState.Finish;
				}
				object visitorTest = ModNet.tokenTests.visitorTest;
				ObjectFlowControl.CheckForSyncLockOnValueType(visitorTest);
				lock (visitorTest)
				{
					ModNet.NetManagerClass tokenTests = ModNet.tokenTests;
					ref int ptr = ref tokenTests._CollectionTest;
					tokenTests._CollectionTest = checked(ptr - 1);
					if (PrintLog)
					{
						ModBase.Log("[Download] " + this._AlgoTest + "：已完成，剩余文件 " + Conversions.ToString(ModNet.tokenTests._CollectionTest), ModBase.LogLevel.Normal, "出现错误");
					}
				}
				try
				{
					foreach (ModNet.LoaderDownload loaderDownload in this._AdapterTest)
					{
						loaderDownload.OnFileFinish(this);
					}
				}
				finally
				{
					IEnumerator<ModNet.LoaderDownload> enumerator;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
			}

			// Token: 0x040006BB RID: 1723
			public ModBase.SafeList<ModNet.LoaderDownload> _AdapterTest;

			// Token: 0x040006BC RID: 1724
			public ModNet.NetSource[] facadeTest;

			// Token: 0x040006BD RID: 1725
			private int listTest;

			// Token: 0x040006BE RID: 1726
			public List<ModNet.NetSource> _MerchantTest;

			// Token: 0x040006BF RID: 1727
			public string m_AuthenticationTest;

			// Token: 0x040006C0 RID: 1728
			public string _AlgoTest;

			// Token: 0x040006C1 RID: 1729
			public ModNet.NetState _ComparatorTest;

			// Token: 0x040006C2 RID: 1730
			public List<Exception> _MappingTest;

			// Token: 0x040006C3 RID: 1731
			public ModNet.NetThread m_TokenizerTest;

			// Token: 0x040006C4 RID: 1732
			public long filterTest;

			// Token: 0x040006C5 RID: 1733
			public bool databaseTest;

			// Token: 0x040006C6 RID: 1734
			private Queue<byte> predicateTest;

			// Token: 0x040006C7 RID: 1735
			public long _PoolTest;

			// Token: 0x040006C8 RID: 1736
			private readonly object customerTest;

			// Token: 0x040006C9 RID: 1737
			public ModBase.FileChecker m_PageTest;

			// Token: 0x040006CA RID: 1738
			public bool _InterceptorTest;

			// Token: 0x040006CB RID: 1739
			private long m_ContainerTest;

			// Token: 0x040006CC RID: 1740
			private long paramsTest;

			// Token: 0x040006CD RID: 1741
			private long m_DispatcherTest;

			// Token: 0x040006CE RID: 1742
			public bool m_ProcessTest;

			// Token: 0x040006CF RID: 1743
			private int parameterTest;

			// Token: 0x040006D0 RID: 1744
			private long recordTest;

			// Token: 0x040006D1 RID: 1745
			public readonly object serviceTest;

			// Token: 0x040006D2 RID: 1746
			public readonly object m_InvocationTest;

			// Token: 0x040006D3 RID: 1747
			public readonly object _ProxyTest;

			// Token: 0x040006D4 RID: 1748
			public readonly object _MessageTest;

			// Token: 0x040006D5 RID: 1749
			public readonly int creatorTest;
		}

		// Token: 0x02000137 RID: 311
		public class LoaderDownload : ModLoader.LoaderBase
		{
			// Token: 0x1700020B RID: 523
			// (get) Token: 0x06000D2A RID: 3370 RVA: 0x00059B2C File Offset: 0x00057D2C
			// (set) Token: 0x06000D2B RID: 3371 RVA: 0x000087ED File Offset: 0x000069ED
			public override double Progress
			{
				get
				{
					double result;
					if (base.State >= ModBase.LoadState.Finished)
					{
						result = 1.0;
					}
					else if (!Enumerable.Any<ModNet.NetFile>(this.Files))
					{
						result = 0.0;
					}
					else
					{
						result = this._Progress;
					}
					return result;
				}
				set
				{
					throw new Exception("文件下载不允许指定进度");
				}
			}

			// Token: 0x1700020C RID: 524
			// (get) Token: 0x06000D2C RID: 3372 RVA: 0x000087F9 File Offset: 0x000069F9
			// (set) Token: 0x06000D2D RID: 3373 RVA: 0x00059B70 File Offset: 0x00057D70
			public int FailCount
			{
				get
				{
					return this._FailCount;
				}
				set
				{
					int num2;
					int num4;
					object obj;
					try
					{
						IL_00:
						int num = 1;
						this._FailCount = value;
						IL_09:
						num = 2;
						if (base.State != ModBase.LoadState.Loading || (double)value < Math.Min(10000.0, Math.Max((double)this.FileRemain * 5.5, (double)ModNet.m_StateTests * 5.5 + 3.0)))
						{
							goto IL_11B;
						}
						IL_5C:
						num = 3;
						ModBase.Log("[Download] 由于同加载器中失败次数过多引发强制失败：连续失败了 " + Conversions.ToString(value) + " 次", ModBase.LogLevel.Debug, "出现错误");
						IL_7E:
						ProjectData.ClearProjectError();
						num2 = 1;
						IL_85:
						num = 5;
						List<Exception> list = new List<Exception>();
						IL_8D:
						num = 6;
						IEnumerator<ModNet.NetFile> enumerator = this.Files.GetEnumerator();
						while (enumerator.MoveNext())
						{
							ModNet.NetFile netFile = enumerator.Current;
							IL_A7:
							num = 7;
							foreach (ModNet.NetSource netSource in netFile.facadeTest)
							{
								IL_BE:
								num = 8;
								if (netSource._ValTest != null)
								{
									IL_C9:
									num = 9;
									list.Add(netSource._ValTest);
									IL_D9:
									num = 10;
									if (list.Count > 10)
									{
										goto IL_111;
									}
								}
								IL_E6:
								num = 12;
							}
							IL_F7:
							num = 13;
							continue;
							IL_111:
							num = 15;
							this.OnFail(list);
							goto IL_11B;
						}
						IL_103:
						num = 14;
						if (enumerator != null)
						{
							enumerator.Dispose();
							goto IL_111;
						}
						goto IL_111;
						IL_11B:
						goto IL_1AD;
						IL_120:
						int num3 = num4 + 1;
						num4 = 0;
						@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
						IL_16E:
						goto IL_1A2;
						IL_170:
						num4 = num;
						@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num2);
						IL_180:;
					}
					catch when (endfilter(obj is Exception & num2 != 0 & num4 == 0))
					{
						Exception ex = (Exception)obj2;
						goto IL_170;
					}
					IL_1A2:
					throw ProjectData.CreateProjectError(-2146828237);
					IL_1AD:
					if (num4 != 0)
					{
						ProjectData.ClearProjectError();
					}
				}
			}

			// Token: 0x06000D2E RID: 3374 RVA: 0x00059D50 File Offset: 0x00057F50
			public void RefreshStat()
			{
				double num = 0.0;
				double num2 = 0.0;
				try
				{
					foreach (ModNet.NetFile netFile in this.Files)
					{
						if (netFile.m_ProcessTest)
						{
							num += netFile.Progress * 0.2;
							num2 += 0.2;
						}
						else
						{
							num += netFile.Progress;
							num2 += 1.0;
						}
					}
				}
				finally
				{
					IEnumerator<ModNet.NetFile> enumerator;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
				if (num2 > 0.0)
				{
					num /= num2;
				}
				if (num < 1.0 && num > 0.0)
				{
					num = 2.0 * Math.Pow(num, 3.0) - 3.0 * Math.Pow(num, 2.0) + 2.0 * num;
				}
				this._Progress = num;
			}

			// Token: 0x06000D2F RID: 3375 RVA: 0x00059E58 File Offset: 0x00058058
			public LoaderDownload(string Name, List<ModNet.NetFile> FileTasks)
			{
				this.FileRemainLock = RuntimeHelpers.GetObjectValue(new object());
				this._Progress = 0.0;
				this._FailCount = 0;
				this.Name = Name;
				this.Files = new ModBase.SafeList<ModNet.NetFile>(FileTasks);
			}

			// Token: 0x06000D30 RID: 3376 RVA: 0x00059EA8 File Offset: 0x000580A8
			public override void Start(object Input = null, bool IsForceRestart = false)
			{
				if (Input != null)
				{
					this.Files = new ModBase.SafeList<ModNet.NetFile>((IEnumerable<ModNet.NetFile>)Input);
				}
				ModBase.SafeList<ModNet.NetFile> safeList = new ModBase.SafeList<ModNet.NetFile>();
				checked
				{
					int num = this.Files.Count - 1;
					int i = 0;
					IL_8D:
					while (i <= num)
					{
						int num2 = i + 1;
						int num3 = this.Files.Count - 1;
						for (int j = num2; j <= num3; j++)
						{
							if (Operators.CompareString(this.Files[i].m_AuthenticationTest, this.Files[j].m_AuthenticationTest, false) == 0)
							{
								IL_89:
								i++;
								goto IL_8D;
							}
						}
						safeList.Add(this.Files[i]);
						goto IL_89;
					}
					this.Files = safeList;
					object fileRemainLock = this.FileRemainLock;
					ObjectFlowControl.CheckForSyncLockOnValueType(fileRemainLock);
					lock (fileRemainLock)
					{
						try
						{
							IEnumerator<ModNet.NetFile> enumerator = this.Files.GetEnumerator();
							while (enumerator.MoveNext())
							{
								if (enumerator.Current._ComparatorTest != ModNet.NetState.Finish)
								{
									ref int ptr = ref this.FileRemain;
									this.FileRemain = ptr + 1;
								}
							}
						}
						finally
						{
							IEnumerator<ModNet.NetFile> enumerator;
							if (enumerator != null)
							{
								enumerator.Dispose();
							}
						}
					}
					base.State = ModBase.LoadState.Loading;
					ModBase.RunInNewThread(delegate
					{
						try
						{
							ModNet.LoaderDownload._Closure$__13-1 CS$<>8__locals1 = new ModNet.LoaderDownload._Closure$__13-1(CS$<>8__locals1);
							CS$<>8__locals1.$VB$Me = this;
							if (!Enumerable.Any<ModNet.NetFile>(this.Files))
							{
								this.OnFinish();
							}
							else
							{
								try
								{
									foreach (ModNet.NetFile netFile in this.Files)
									{
										if (netFile == null)
										{
											throw new ArgumentException("存在空文件请求！");
										}
										foreach (ModNet.NetSource netSource in netFile.facadeTest)
										{
											if (!netSource.m_StructTest.StartsWithF("https://", true) && !netSource.m_StructTest.StartsWithF("http://", true))
											{
												netSource._ValTest = new ArgumentException("输入的下载链接不正确！");
												netSource.candidateTest = true;
											}
										}
										if (netFile.IsSourceFailed(true))
										{
											throw new ArgumentException("输入的下载链接不正确！");
										}
										if (!netFile.m_AuthenticationTest.ToLower().Contains(":\\"))
										{
											throw new ArgumentException("输入的本地文件地址不正确！");
										}
										if (netFile.m_AuthenticationTest.EndsWithF("\\", false))
										{
											throw new ArgumentException("请输入含文件名的完整文件路径！");
										}
										string fullName = new FileInfo(netFile.m_AuthenticationTest).Directory.FullName;
										if (!Directory.Exists(fullName))
										{
											Directory.CreateDirectory(fullName);
										}
									}
								}
								finally
								{
									IEnumerator<ModNet.NetFile> enumerator2;
									if (enumerator2 != null)
									{
										enumerator2.Dispose();
									}
								}
								ModNet.tokenTests.Start(this);
								List<string> list = new List<string>();
								CS$<>8__locals1.$VB$Local_FoldersFinal = new List<string>();
								if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("SystemDebugSkipCopy", null))))
								{
									list.Add(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft\\");
									try
									{
										foreach (ModMinecraft.McFolder mcFolder in ModMinecraft.messageTests)
										{
											list.Add(mcFolder.Path);
										}
									}
									finally
									{
										List<ModMinecraft.McFolder>.Enumerator enumerator3;
										((IDisposable)enumerator3).Dispose();
									}
									list = Enumerable.ToList<string>(Enumerable.Distinct<string>(list));
									try
									{
										foreach (string text in list)
										{
											if (Operators.CompareString(text, ModMinecraft.m_ProxyTests, false) != 0 && Directory.Exists(text))
											{
												CS$<>8__locals1.$VB$Local_FoldersFinal.Add(text);
											}
										}
									}
									finally
									{
										List<string>.Enumerator enumerator4;
										((IDisposable)enumerator4).Dispose();
									}
								}
								int num4 = (int)Math.Round(Math.Max(5.0, unchecked((double)this.Files.Count / 10.0 + 1.0)));
								List<ModNet.NetFile> list2 = new List<ModNet.NetFile>();
								try
								{
									foreach (ModNet.NetFile item in this.Files)
									{
										list2.Add(item);
										if (list2.Count == num4)
										{
											ModNet.LoaderDownload._Closure$__13-0 CS$<>8__locals2 = new ModNet.LoaderDownload._Closure$__13-0(CS$<>8__locals2);
											CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2 = CS$<>8__locals1;
											CS$<>8__locals2.$VB$Local_FilesToRun = new List<ModNet.NetFile>();
											CS$<>8__locals2.$VB$Local_FilesToRun.AddRange(list2);
											ModBase.RunInNewThread(delegate
											{
												CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Me.StartCopy(CS$<>8__locals2.$VB$Local_FilesToRun, CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_FoldersFinal);
											}, "NetTask FileCopy " + Conversions.ToString(this.Uuid), ThreadPriority.Normal);
											list2.Clear();
										}
									}
								}
								finally
								{
									IEnumerator<ModNet.NetFile> enumerator5;
									if (enumerator5 != null)
									{
										enumerator5.Dispose();
									}
								}
								if (Enumerable.Any<ModNet.NetFile>(list2))
								{
									ModNet.LoaderDownload._Closure$__13-2 CS$<>8__locals3 = new ModNet.LoaderDownload._Closure$__13-2(CS$<>8__locals3);
									CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3 = CS$<>8__locals1;
									CS$<>8__locals3.$VB$Local_FilesToRun = new List<ModNet.NetFile>();
									CS$<>8__locals3.$VB$Local_FilesToRun.AddRange(list2);
									ModBase.RunInNewThread(delegate
									{
										CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3.$VB$Me.StartCopy(CS$<>8__locals3.$VB$Local_FilesToRun, CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3.$VB$Local_FoldersFinal);
									}, "NetTask FileCopy " + Conversions.ToString(this.Uuid), ThreadPriority.Normal);
									list2.Clear();
								}
							}
						}
						catch (Exception item2)
						{
							this.OnFail(new List<Exception>
							{
								item2
							});
						}
					}, "NetTask " + Conversions.ToString(this.Uuid) + " Main", ThreadPriority.Normal);
				}
			}

			// Token: 0x06000D31 RID: 3377 RVA: 0x0005A008 File Offset: 0x00058208
			private void StartCopy(List<ModNet.NetFile> Files, List<string> FolderList)
			{
				checked
				{
					try
					{
						if (ModBase._TokenRepository)
						{
							ModBase.Log("[Download] 检查线程分配文件数：" + Conversions.ToString(Files.Count) + "，线程名：" + Thread.CurrentThread.Name, ModBase.LogLevel.Normal, "出现错误");
						}
						List<KeyValuePair<ModNet.NetFile, string>> list = new List<KeyValuePair<ModNet.NetFile, string>>();
						try
						{
							foreach (ModNet.NetFile netFile in Files)
							{
								string text = null;
								if (netFile.m_PageTest != null && ModMinecraft.messageTests != null && ModMinecraft.m_ProxyTests != null && netFile.m_PageTest.m_MockError && netFile.m_AuthenticationTest.StartsWithF(ModMinecraft.m_ProxyTests, false))
								{
									string str = netFile.m_AuthenticationTest.Replace(ModMinecraft.m_ProxyTests, "");
									try
									{
										foreach (string str2 in FolderList)
										{
											string text2 = str2 + str;
											if (netFile.m_PageTest.Check(text2) == null)
											{
												text = text2;
												break;
											}
										}
									}
									finally
									{
										List<string>.Enumerator enumerator2;
										((IDisposable)enumerator2).Dispose();
									}
								}
								object lockState = this.LockState;
								ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
								lock (lockState)
								{
									if (text != null)
									{
										netFile._ComparatorTest = ModNet.NetState.WaitForCopy;
										netFile.m_ProcessTest = true;
										list.Add(new KeyValuePair<ModNet.NetFile, string>(netFile, text));
									}
									else
									{
										netFile._ComparatorTest = ModNet.NetState.WaitForDownload;
										netFile.m_ProcessTest = false;
									}
								}
							}
						}
						finally
						{
							List<ModNet.NetFile>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
						try
						{
							foreach (KeyValuePair<ModNet.NetFile, string> keyValuePair in list)
							{
								ModNet.NetFile key = keyValuePair.Key;
								object lockState2 = this.LockState;
								ObjectFlowControl.CheckForSyncLockOnValueType(lockState2);
								lock (lockState2)
								{
									if (key._ComparatorTest > ModNet.NetState.WaitForCopy)
									{
										break;
									}
								}
								string value = keyValuePair.Value;
								int num = 0;
								for (;;)
								{
									try
									{
										ModBase.Log("[Download] 复制已存在的文件（" + value + "）", ModBase.LogLevel.Normal, "出现错误");
										ModBase.CopyFile(value, key.m_AuthenticationTest);
										key.Finish(false);
										break;
									}
									catch (Exception ex)
									{
										num++;
										ModBase.Log(ex, string.Format("复制已存在的文件失败，重试第 {2} 次（{0} -> {1}）", value, key.m_AuthenticationTest, num), ModBase.LogLevel.Debug, "出现错误");
										if (num >= 3)
										{
											key._ComparatorTest = ModNet.NetState.WaitForDownload;
											key.m_ProcessTest = false;
											break;
										}
										Thread.Sleep(200);
									}
								}
							}
						}
						finally
						{
							List<KeyValuePair<ModNet.NetFile, string>>.Enumerator enumerator3;
							((IDisposable)enumerator3).Dispose();
						}
					}
					catch (Exception ex2)
					{
						ModBase.Log(ex2, "下载已存在文件查找失败", ModBase.LogLevel.Feedback, "出现错误");
					}
				}
			}

			// Token: 0x06000D32 RID: 3378 RVA: 0x0005A368 File Offset: 0x00058568
			public void OnFileFinish(ModNet.NetFile File)
			{
				object fileRemainLock = this.FileRemainLock;
				ObjectFlowControl.CheckForSyncLockOnValueType(fileRemainLock);
				lock (fileRemainLock)
				{
					ref int ptr = ref this.FileRemain;
					this.FileRemain = checked(ptr - 1);
					if (this.FileRemain > 0)
					{
						return;
					}
				}
				this.OnFinish();
			}

			// Token: 0x06000D33 RID: 3379 RVA: 0x0005A3C8 File Offset: 0x000585C8
			public void OnFinish()
			{
				base.RaisePreviewFinish();
				object lockState = this.LockState;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
				lock (lockState)
				{
					if (base.State <= ModBase.LoadState.Loading)
					{
						base.State = ModBase.LoadState.Finished;
					}
				}
			}

			// Token: 0x06000D34 RID: 3380 RVA: 0x0005A420 File Offset: 0x00058620
			public void OnFileFail(ModNet.NetFile File)
			{
				foreach (ModNet.NetSource netSource in File.facadeTest)
				{
					if (!Information.IsNothing(netSource._ValTest))
					{
						File._MappingTest.Add(netSource._ValTest);
					}
				}
				this.OnFail(File._MappingTest);
			}

			// Token: 0x06000D35 RID: 3381 RVA: 0x0005A470 File Offset: 0x00058670
			public void OnFail(List<Exception> ExList)
			{
				object lockState = this.LockState;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
				lock (lockState)
				{
					if (base.State > ModBase.LoadState.Loading)
					{
						return;
					}
					if (ExList == null || !Enumerable.Any<Exception>(ExList))
					{
						ExList = new List<Exception>
						{
							new Exception("未知错误！")
						};
					}
					List<Exception> list = Enumerable.ToList<Exception>(Enumerable.Where<Exception>(ExList, (ModNet.LoaderDownload._Closure$__.$I18-0 == null) ? (ModNet.LoaderDownload._Closure$__.$I18-0 = ((Exception e) => !e.Message.Contains("(404)"))) : ModNet.LoaderDownload._Closure$__.$I18-0));
					base.Error = (Enumerable.Any<Exception>(list) ? list[0] : ExList[0]);
					try
					{
						foreach (ModNet.NetFile netFile in this.Files)
						{
							if (netFile._ComparatorTest == ModNet.NetState.Error)
							{
								base.Error = new Exception("文件下载失败：" + netFile.m_AuthenticationTest + "\r\n" + Enumerable.Select<ModNet.NetSource, string>(netFile.facadeTest, (ModNet.LoaderDownload._Closure$__.$I18-1 == null) ? (ModNet.LoaderDownload._Closure$__.$I18-1 = delegate(ModNet.NetSource s)
								{
									if (s._ValTest != null)
									{
										return s._ValTest.Message + "（" + s.m_StructTest + "）";
									}
									return s.m_StructTest;
								}) : ModNet.LoaderDownload._Closure$__.$I18-1).Join("\r\n"), base.Error);
								break;
							}
						}
					}
					finally
					{
						IEnumerator<ModNet.NetFile> enumerator;
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
					base.State = ModBase.LoadState.Failed;
				}
				try
				{
					foreach (ModNet.NetFile netFile2 in this.Files)
					{
						if (netFile2._ComparatorTest < ModNet.NetState.Merge)
						{
							netFile2._ComparatorTest = ModNet.NetState.Error;
						}
					}
				}
				finally
				{
					IEnumerator<ModNet.NetFile> enumerator2;
					if (enumerator2 != null)
					{
						enumerator2.Dispose();
					}
				}
				List<string> list2 = new List<string>();
				try
				{
					foreach (Exception ex in ExList)
					{
						list2.Add(ModBase.GetExceptionDetail(ex, false));
					}
				}
				finally
				{
					List<Exception>.Enumerator enumerator3;
					((IDisposable)enumerator3).Dispose();
				}
				ModBase.Log("[Download] " + Enumerable.ToArray<string>(Enumerable.Distinct<string>(list2)).Join("\r\n"), ModBase.LogLevel.Normal, "出现错误");
			}

			// Token: 0x06000D36 RID: 3382 RVA: 0x0005A6D4 File Offset: 0x000588D4
			public override void Abort()
			{
				object lockState = this.LockState;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
				lock (lockState)
				{
					if (base.State >= ModBase.LoadState.Finished)
					{
						return;
					}
					base.State = ModBase.LoadState.Aborted;
				}
				ModBase.Log("[Download] " + this.Name + " 已取消！", ModBase.LogLevel.Normal, "出现错误");
				try
				{
					foreach (ModNet.NetFile netFile in this.Files)
					{
						netFile.Abort(this);
					}
				}
				finally
				{
					IEnumerator<ModNet.NetFile> enumerator;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
			}

			// Token: 0x040006D6 RID: 1750
			public ModBase.SafeList<ModNet.NetFile> Files;

			// Token: 0x040006D7 RID: 1751
			private int FileRemain;

			// Token: 0x040006D8 RID: 1752
			private readonly object FileRemainLock;

			// Token: 0x040006D9 RID: 1753
			private double _Progress;

			// Token: 0x040006DA RID: 1754
			private int _FailCount;
		}

		// Token: 0x0200013C RID: 316
		public class NetManagerClass
		{
			// Token: 0x06000D45 RID: 3397 RVA: 0x0005ABA4 File Offset: 0x00058DA4
			public NetManagerClass()
			{
				this.initializerTest = new Dictionary<string, ModNet.NetFile>();
				this._SingletonTest = RuntimeHelpers.GetObjectValue(new object());
				this.regTest = new ModBase.SafeList<ModNet.LoaderDownload>();
				this.productTest = 0L;
				this.m_ListenerTest = RuntimeHelpers.GetObjectValue(new object());
				this._CollectionTest = 0;
				this.visitorTest = RuntimeHelpers.GetObjectValue(new object());
				this._ValueTest = 0L;
				this._ObjectTest = new List<long>();
				this.m_BridgeTest = 0L;
				this.reponseTest = ModBase.GetUuid();
				this.m_ExceptionTest = false;
				this.m_UtilsTest = false;
			}

			// Token: 0x06000D46 RID: 3398 RVA: 0x000088E4 File Offset: 0x00006AE4
			public long PrintMapper()
			{
				return this.productTest;
			}

			// Token: 0x06000D47 RID: 3399 RVA: 0x0005AC58 File Offset: 0x00058E58
			public void InterruptMapper(long value)
			{
				object listenerTest = this.m_ListenerTest;
				ObjectFlowControl.CheckForSyncLockOnValueType(listenerTest);
				lock (listenerTest)
				{
					this.productTest = value;
				}
			}

			// Token: 0x06000D48 RID: 3400 RVA: 0x0005ACA0 File Offset: 0x00058EA0
			private void RefreshStat()
			{
				checked
				{
					try
					{
						long num = ModBase.GetTimeTick() - this._GlobalTest;
						ref long ptr = ref this._GlobalTest;
						this._GlobalTest = ptr + num;
						double a = Math.Max(0.0, (double)(this.PrintMapper() - this._ValueTest) / ((double)num / 1000.0));
						this._ObjectTest.Insert(0, (long)Math.Round(a));
						if (this._ObjectTest.Count >= 31)
						{
							this._ObjectTest.RemoveAt(30);
						}
						this._ValueTest = this.PrintMapper();
						long num2 = 0L;
						long num3 = 0L;
						int num4 = this._ObjectTest.Count;
						try
						{
							foreach (long num5 in this._ObjectTest)
							{
								num2 += num5 * unchecked((long)num4);
								num3 += unchecked((long)num4);
								num4--;
							}
						}
						finally
						{
							List<long>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
						this.m_BridgeTest = (long)Math.Round((num3 > 0L) ? ((double)num2 / (double)num3) : 0.0);
						long num6 = 0L;
						if (this._ObjectTest.Count >= 10)
						{
							num6 = (long)Math.Round(unchecked(Enumerable.Average(Enumerable.Take<long>(this._ObjectTest, 10)) * 0.85));
						}
						if (num6 > ModNet.callbackTests)
						{
							ModNet.callbackTests = num6;
							ModBase.Log("[Download] 速度下限已提升到 " + ModBase.GetString(num6), ModBase.LogLevel.Normal, "出现错误");
						}
						try
						{
							foreach (ModNet.LoaderDownload loaderDownload in this.regTest)
							{
								loaderDownload.RefreshStat();
							}
						}
						finally
						{
							IEnumerator<ModNet.LoaderDownload> enumerator2;
							if (enumerator2 != null)
							{
								enumerator2.Dispose();
							}
						}
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "刷新下载公开属性失败", ModBase.LogLevel.Debug, "出现错误");
					}
				}
			}

			// Token: 0x06000D49 RID: 3401 RVA: 0x0005AECC File Offset: 0x000590CC
			private void StartManager()
			{
				ModNet.NetManagerClass._Closure$__17-0 CS$<>8__locals1 = new ModNet.NetManagerClass._Closure$__17-0(CS$<>8__locals1);
				checked
				{
					if (!this.m_ExceptionTest)
					{
						this.m_ExceptionTest = true;
						CS$<>8__locals1.$VB$Local_ThreadStarter = delegate(int Id)
						{
							try
							{
								for (;;)
								{
									IL_00:
									Thread.Sleep(20);
									object singletonTest = this._SingletonTest;
									ObjectFlowControl.CheckForSyncLockOnValueType(singletonTest);
									List<ModNet.NetFile> list;
									lock (singletonTest)
									{
										if (Id == 0 && this._CollectionTest == 0 && Enumerable.Any<KeyValuePair<string, ModNet.NetFile>>(this.initializerTest))
										{
											this.initializerTest.Clear();
										}
										list = Enumerable.ToList<ModNet.NetFile>(this.initializerTest.Values);
									}
									List<ModNet.NetFile> list2 = new List<ModNet.NetFile>();
									List<ModNet.NetFile> list3 = new List<ModNet.NetFile>();
									try
									{
										foreach (ModNet.NetFile netFile in list)
										{
											if (netFile.creatorTest % 2 != Id)
											{
												if (netFile._ComparatorTest == ModNet.NetState.WaitForDownload)
												{
													list2.Add(netFile);
												}
												else if (netFile._ComparatorTest < ModNet.NetState.Merge)
												{
													list3.Add(netFile);
												}
											}
										}
									}
									finally
									{
										List<ModNet.NetFile>.Enumerator enumerator;
										((IDisposable)enumerator).Dispose();
									}
									try
									{
										foreach (ModNet.NetFile netFile2 in list2)
										{
											if (ModNet._ConfigurationTests >= ModNet.m_StateTests)
											{
												goto IL_00;
											}
											ModNet.NetThread netThread = netFile2.TryBeginThread();
											if (netThread != null && netThread.Source.m_StructTest.Contains("bmclapi"))
											{
												Thread.Sleep(70);
											}
										}
									}
									finally
									{
										List<ModNet.NetFile>.Enumerator enumerator2;
										((IDisposable)enumerator2).Dispose();
									}
									if (this.m_BridgeTest < ModNet.callbackTests)
									{
										try
										{
											foreach (ModNet.NetFile netFile3 in list3)
											{
												if (ModNet._ConfigurationTests >= ModNet.m_StateTests)
												{
													break;
												}
												int num = 0;
												int num2 = 0;
												if (netFile3.m_TokenizerTest != null)
												{
													try
													{
														foreach (ModNet.NetThread netThread2 in Enumerable.ToList<ModNet.NetThread>(netFile3.m_TokenizerTest))
														{
															if (netThread2.infoTest < ModNet.NetState.Download)
															{
																num++;
															}
															else if (netThread2.infoTest == ModNet.NetState.Download)
															{
																num2++;
															}
														}
													}
													finally
													{
														List<ModNet.NetThread>.Enumerator enumerator4;
														((IDisposable)enumerator4).Dispose();
													}
												}
												if (num <= num2)
												{
													ModNet.NetThread netThread3 = netFile3.TryBeginThread();
													if (netThread3 != null && netThread3.Source.m_StructTest.Contains("bmclapi"))
													{
														Thread.Sleep(70);
													}
												}
											}
										}
										finally
										{
											List<ModNet.NetFile>.Enumerator enumerator3;
											((IDisposable)enumerator3).Dispose();
										}
									}
								}
							}
							catch (Exception ex)
							{
								ModBase.Log(ex, string.Format("下载管理启动线程 {0} 出错", Id), ModBase.LogLevel.Assert, "出现错误");
							}
						};
						ModBase.RunInNewThread(delegate
						{
							CS$<>8__locals1.$VB$Local_ThreadStarter(0);
						}, "NetManager ThreadStarter 0", ThreadPriority.Normal);
						ModBase.RunInNewThread(delegate
						{
							CS$<>8__locals1.$VB$Local_ThreadStarter(1);
						}, "NetManager ThreadStarter 1", ThreadPriority.Normal);
						ModBase.RunInNewThread(delegate
						{
							try
							{
								ModNet.m_ConsumerTests = ModBase.GetTimeTick();
								for (;;)
								{
									long timeTick = ModBase.GetTimeTick();
									long num = timeTick;
									if (ModNet.m_TemplateTests > 0L)
									{
										ModNet.m_MethodTests = (long)Math.Round(unchecked((double)ModNet.m_TemplateTests / 1000.0 * (double)(checked(timeTick - ModNet.m_ConsumerTests))));
									}
									ModNet.m_ConsumerTests = timeTick;
									this.RefreshStat();
									while (ModBase.GetTimeTick() - num < 80L)
									{
										Thread.Sleep(10);
									}
								}
							}
							catch (Exception ex)
							{
								ModBase.Log(ex, "下载管理刷新线程出错", ModBase.LogLevel.Assert, "出现错误");
							}
						}, "NetManager StatRefresher", ThreadPriority.Normal);
					}
				}
			}

			// Token: 0x06000D4A RID: 3402 RVA: 0x0005AF4C File Offset: 0x0005914C
			public void Start(ModNet.LoaderDownload Task)
			{
				this.StartManager();
				if (!this.m_UtilsTest)
				{
					try
					{
						ModBase.DeleteDirectory(ModBase.m_DecoratorRepository + "Download", false);
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "清理下载缓存失败", ModBase.LogLevel.Debug, "出现错误");
					}
					this.m_UtilsTest = true;
				}
				Directory.CreateDirectory(ModBase.m_DecoratorRepository + "Download");
				object singletonTest = this._SingletonTest;
				ObjectFlowControl.CheckForSyncLockOnValueType(singletonTest);
				checked
				{
					lock (singletonTest)
					{
						int num = Task.Files.Count - 1;
						for (int i = 0; i <= num; i++)
						{
							ModNet.NetFile netFile = Task.Files[i];
							if (this.initializerTest.ContainsKey(netFile.m_AuthenticationTest))
							{
								if (this.initializerTest[netFile.m_AuthenticationTest]._ComparatorTest >= ModNet.NetState.Finish)
								{
									netFile._AdapterTest.Add(Task);
									this.initializerTest[netFile.m_AuthenticationTest] = netFile;
									object obj = this.visitorTest;
									ObjectFlowControl.CheckForSyncLockOnValueType(obj);
									lock (obj)
									{
										ref int ptr = ref this._CollectionTest;
										this._CollectionTest = ptr + 1;
										if (ModBase._TokenRepository)
										{
											ModBase.Log("[Download] " + netFile._AlgoTest + "：已替换列表，剩余文件 " + Conversions.ToString(this._CollectionTest), ModBase.LogLevel.Normal, "出现错误");
										}
										goto IL_215;
									}
								}
								netFile = this.initializerTest[netFile.m_AuthenticationTest];
								netFile._AdapterTest.Add(Task);
							}
							else
							{
								netFile._AdapterTest.Add(Task);
								this.initializerTest.Add(netFile.m_AuthenticationTest, netFile);
								object obj2 = this.visitorTest;
								ObjectFlowControl.CheckForSyncLockOnValueType(obj2);
								lock (obj2)
								{
									ref int ptr = ref this._CollectionTest;
									this._CollectionTest = ptr + 1;
									if (ModBase._TokenRepository)
									{
										ModBase.Log("[Download] " + netFile._AlgoTest + "：已加入列表，剩余文件 " + Conversions.ToString(this._CollectionTest), ModBase.LogLevel.Normal, "出现错误");
									}
								}
							}
							IL_215:
							Task.Files[i] = netFile;
						}
					}
					this.regTest.Add(Task);
				}
			}

			// Token: 0x040006E4 RID: 1764
			public Dictionary<string, ModNet.NetFile> initializerTest;

			// Token: 0x040006E5 RID: 1765
			public readonly object _SingletonTest;

			// Token: 0x040006E6 RID: 1766
			public ModBase.SafeList<ModNet.LoaderDownload> regTest;

			// Token: 0x040006E7 RID: 1767
			private long productTest;

			// Token: 0x040006E8 RID: 1768
			private readonly object m_ListenerTest;

			// Token: 0x040006E9 RID: 1769
			public int _CollectionTest;

			// Token: 0x040006EA RID: 1770
			public readonly object visitorTest;

			// Token: 0x040006EB RID: 1771
			private long _ValueTest;

			// Token: 0x040006EC RID: 1772
			private List<long> _ObjectTest;

			// Token: 0x040006ED RID: 1773
			public long m_BridgeTest;

			// Token: 0x040006EE RID: 1774
			public readonly int reponseTest;

			// Token: 0x040006EF RID: 1775
			private long _GlobalTest;

			// Token: 0x040006F0 RID: 1776
			private bool m_ExceptionTest;

			// Token: 0x040006F1 RID: 1777
			private bool m_UtilsTest;
		}
	}
}
