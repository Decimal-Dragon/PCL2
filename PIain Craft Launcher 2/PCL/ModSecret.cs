using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using PCL.My;

namespace PCL
{
	// Token: 0x020000C2 RID: 194
	[StandardModule]
	internal sealed class ModSecret
	{
		// Token: 0x060005B8 RID: 1464 RVA: 0x00030B30 File Offset: 0x0002ED30
		internal static void SecretOnApplicationStart()
		{
			Thread.CurrentThread.Priority = ThreadPriority.Highest;
			try
			{
				new FormattedText("", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, Enumerable.First<Typeface>(Fonts.SystemTypefaces), 96.0, new ModBase.MyColor(), (double)ModBase._ConfigurationRepository);
			}
			catch (UriFormatException ex)
			{
				Environment.SetEnvironmentVariable("windir", Environment.GetEnvironmentVariable("SystemRoot"), EnvironmentVariableTarget.User);
				new FormattedText("", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, Enumerable.First<Typeface>(Fonts.SystemTypefaces), 96.0, new ModBase.MyColor(), (double)ModBase._ConfigurationRepository);
			}
			try
			{
				Directory.CreateDirectory(ModBase.Path + "PCL");
			}
			catch (Exception ex2)
			{
				Interaction.MsgBox(string.Format("PCL 无法创建 PCL 文件夹（{0}），请尝试：", ModBase.Path + "PCL") + "\r\n1. 将 PCL 移动到其他文件夹" + (ModBase.Path.StartsWithF("C:", true) ? "，例如 C 盘和桌面以外的其他位置。" : "。") + "\r\n2. 删除当前目录中的 PCL 文件夹，然后再试。\r\n3. 右键 PCL 选择属性，打开 兼容性 中的 以管理员身份运行此程序。", MsgBoxStyle.Critical, "运行环境错误");
				Environment.Exit(4);
			}
			if (!ModBase.CheckPermission(ModBase.Path + "PCL"))
			{
				Interaction.MsgBox("PCL 没有对当前文件夹的写入权限，请尝试：\r\n1. 将 PCL 移动到其他文件夹" + (ModBase.Path.StartsWithF("C:", true) ? "，例如 C 盘和桌面以外的其他位置。" : "。") + "\r\n2. 删除当前目录中的 PCL 文件夹，然后再试。\r\n3. 右键 PCL 选择属性，打开 兼容性 中的 以管理员身份运行此程序。", MsgBoxStyle.Critical, "运行环境错误");
				Environment.Exit(4);
			}
			ModSecret.indexerField = ModSecret.indexerField + "qgaq".ToUpper() + "1S";
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00030CE4 File Offset: 0x0002EEE4
		internal static string SecretGetUniqueAddress()
		{
			string result;
			try
			{
				if (ModBase._SystemRepository < 1L)
				{
					result = "0000-0000-0000-0000";
				}
				else
				{
					string str;
					try
					{
						str = MyWpfExtension.ManageParser().Registry.GetValue(string.Format("HKEY_LOCAL_MACHINE\\SYSTEM\\Har{0}wareConfig", "D".ToLower()), "LastConfig", "Unknown").ToString().ToUpper().Trim(new char[]
						{
							'{',
							'}'
						});
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "获取主板标识码失败", ModBase.LogLevel.Debug, "出现错误");
						str = "Unknown";
					}
					string text;
					try
					{
						text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("Identify", null));
					}
					catch (Exception ex2)
					{
						text = "";
					}
					if (text.Length < 3)
					{
						text = Conversions.ToString(ModBase.GetTimeTick()) + Conversions.ToString(MyWpfExtension.ManageParser().Info.AvailablePhysicalMemory);
						ModBase.m_IdentifierRepository.Set("Identify", text, false, null);
					}
					string str2 = ModBase.StrFill(ModBase.GetHash(str + text).ToString("X"), "7", 16);
					result = string.Format("{0}-{1}-{2}-{3}", new object[]
					{
						Strings.Mid(str2, 5, 4),
						Strings.Mid(str2, 13, 4),
						Strings.Mid(str2, 1, 4),
						Strings.Mid(str2, 9, 4)
					});
				}
			}
			catch (Exception ex3)
			{
				ModBase.Log(ex3, "PCL 无法获取设备标识码，这可能会导致部分设置无法正常存储。\r\n\r\n详细的错误信息", ModBase.LogLevel.Feedback, "获取标识码失败");
				result = "0000-0000-0000-0000";
			}
			return result;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00030ECC File Offset: 0x0002F0CC
		internal static void SecretLaunchJvmArgs(ref List<string> DataList)
		{
			string text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionAdvanceJvm", ModMinecraft.AddClient()));
			DataList.Insert(0, Conversions.ToString((Operators.CompareString(text, "", false) == 0) ? ModBase.m_IdentifierRepository.Get("LaunchAdvanceJvm", null) : text));
			ModLaunch.McLaunchLog("当前剩余内存：" + Conversions.ToString(Math.Round(MyWpfExtension.ManageParser().Info.AvailablePhysicalMemory / 1024.0 / 1024.0 / 1024.0 * 10.0) / 10.0) + "G");
			DataList.Add("-Xmn" + Conversions.ToString(Math.Floor(PageVersionSetup.GetRam(ModMinecraft.AddClient(), null) * 1024.0 * 0.15)) + "m");
			DataList.Add("-Xmx" + Conversions.ToString(Math.Floor(PageVersionSetup.GetRam(ModMinecraft.AddClient(), null) * 1024.0)) + "m");
			if (!Enumerable.Any<string>(DataList, (ModSecret._Closure$__.$I6-0 == null) ? (ModSecret._Closure$__.$I6-0 = ((string d) => d.Contains("-Dlog4j2.formatMsgNoLookups=true"))) : ModSecret._Closure$__.$I6-0))
			{
				DataList.Add("-Dlog4j2.formatMsgNoLookups=true");
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00031040 File Offset: 0x0002F240
		internal static string SecretFilter(string Raw, char FilterChar)
		{
			if (Raw.Contains("accessToken "))
			{
				try
				{
					foreach (string text in ModBase.RegexSearch(Raw, "(?<=accessToken ([^ ]{5}))[^ ]+(?=[^ ]{5})", 0))
					{
						Raw = Raw.Replace(text, new string(FilterChar, Enumerable.Count<char>(text)));
					}
				}
				finally
				{
					List<string>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
			string stateMap = ModLaunch.interceptorTests.Output._StateMap;
			string result;
			if (stateMap != null && stateMap.Length >= 10 && Raw.ContainsF(stateMap, true) && Operators.CompareString(ModLaunch.interceptorTests.Output.m_InstanceMap, ModLaunch.interceptorTests.Output._StateMap, false) != 0)
			{
				result = Raw.Replace(stateMap, Strings.Left(stateMap, 5) + new string(FilterChar, checked(stateMap.Length - 10)) + Strings.Right(stateMap, 5));
			}
			else
			{
				result = Raw;
			}
			return result;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00031130 File Offset: 0x0002F330
		internal static object SecretCdnSign(string UrlWithMark)
		{
			object result;
			if (ModBase._SystemRepository < 1L)
			{
				result = UrlWithMark;
			}
			else if (!UrlWithMark.EndsWithF("{CDN}", false))
			{
				result = UrlWithMark;
			}
			else
			{
				string text = UrlWithMark.Replace("{CDN}", "").Replace(" ", "%20");
				string text2 = ModBase.StrFill(ModBase.RandomInteger(0, 2147483645).ToString("x"), "0", 8);
				string text3 = ModSecret.SecretDecrypt("VwHB1je1uabAr0gKijpFaQ==", "CDN");
				string text4 = Conversions.ToString(ModBase.GetUnixTimestamp());
				string text5 = text.Substring(checked(text.IndexOfF("://", false) + 3));
				text5 = text5.Substring(text5.IndexOfF("/", false));
				string stringMD = ModBase.GetStringMD5(new string[]
				{
					text5,
					text4,
					text2,
					"0",
					text3
				}.Join("-"));
				result = text + "?sign=" + new string[]
				{
					text4,
					text2,
					"0",
					stringMD
				}.Join("-");
			}
			return result;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0003125C File Offset: 0x0002F45C
		internal static void SecretHeadersSign(string Url, ref WebClient Client, bool UseBrowserUserAgent = false)
		{
			if (ModBase._SystemRepository >= 1L)
			{
				if (!Url.Contains("baidupcs.com") && !Url.Contains("baidu.com"))
				{
					if (Url.Contains("modrinth.com"))
					{
						switch (ModBase.RandomInteger(1, 10))
						{
						case 1:
							Client.Headers["User-Agent"] = "Huawei/14.0.2.311";
							break;
						case 2:
						case 3:
							Client.Headers["User-Agent"] = "Edge/109.0.1474.0 Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.6045.160 Safari/537.36";
							break;
						case 4:
							Client.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:130.0) Gecko/20100101 Firefox/130";
							break;
						case 5:
						case 6:
							Client.Headers["User-Agent"] = "Mozilla/5.0 (Linux; Android 10; K) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/128.0.0.0 Mobile Safari/537.3";
							break;
						default:
							Client.Headers["User-Agent"] = "PCL2/2.8.12.50 Mozilla/5.0 AppleWebKit/537.36 Chrome/63.0.3239.132 Safari/537.36";
							break;
						}
					}
					else if (UseBrowserUserAgent)
					{
						Client.Headers["User-Agent"] = "PCL2/2.8.12.50 Mozilla/5.0 AppleWebKit/537.36 Chrome/63.0.3239.132 Safari/537.36";
					}
					else
					{
						Client.Headers["User-Agent"] = "PCL2/2.8.12.50";
					}
				}
				else
				{
					Client.Headers["User-Agent"] = "LogStatistic";
				}
				Client.Headers["Referer"] = "http://" + Conversions.ToString(347) + ".pcl2.server/";
				if (Url.Contains(ModSecret.SecretDecrypt("kSHbgKrsiOuHY81i63QtJevaX2+IWvVT", "")))
				{
					Client.Headers.Add(ModSecret.SecretDecrypt("hlBzlKCNcyEYEaSOd0Vx0w==", ""), ModSecret.SecretDecrypt("SA+taZSvSkfRoq0fW5pnIpFzDGDck82KzMPbuglfAqrAbQAOwcZXBXVQVzenSNP14fnexB+G1bV/ufvjrtpiUA==", ""));
				}
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00031408 File Offset: 0x0002F608
		internal static void SecretHeadersSign(string Url, ref HttpWebRequest Request, bool UseBrowserUserAgent = false)
		{
			if (ModBase._SystemRepository >= 1L)
			{
				if (!Url.Contains("baidupcs.com") && !Url.Contains("baidu.com"))
				{
					if (Url.Contains("modrinth.com"))
					{
						switch (ModBase.RandomInteger(1, 11))
						{
						case 1:
							Request.UserAgent = "Mozilla/5.0 AppleWebKit/537.36 Chrome/63.0.3239.132 Safari/537.36";
							break;
						case 2:
						case 11:
							Request.UserAgent = "Edge/109.0.1474.0 Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.6045.160 Safari/537.36";
							break;
						case 3:
						case 6:
						case 7:
						case 8:
							Request.UserAgent = "Huawei/14.0.2.311";
							break;
						case 4:
						case 9:
						case 10:
							Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:130.0) Gecko/20100101 Firefox/130";
							break;
						case 5:
							Request.UserAgent = "Mozilla/5.0 (Linux; Android 10; K) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/128.0.0.0 Mobile Safari/537.3";
							break;
						}
					}
					else if (UseBrowserUserAgent)
					{
						Request.UserAgent = "PCL2/2.8.12.50 Mozilla/5.0 AppleWebKit/537.36 Chrome/63.0.3239.132 Safari/537.36";
					}
					else
					{
						Request.UserAgent = "PCL2/2.8.12.50";
					}
				}
				else
				{
					Request.UserAgent = "LogStatistic";
				}
				Request.Referer = "http://" + Conversions.ToString(347) + ".pcl2.server/";
				if (Url.Contains(ModSecret.SecretDecrypt("kSHbgKrsiOuHY81i63QtJevaX2+IWvVT", "")))
				{
					Request.Headers.Add(ModSecret.SecretDecrypt("hlBzlKCNcyEYEaSOd0Vx0w==", ""), ModSecret.SecretDecrypt("SA+taZSvSkfRoq0fW5pnIpFzDGDck82KzMPbuglfAqrAbQAOwcZXBXVQVzenSNP14fnexB+G1bV/ufvjrtpiUA==", ""));
				}
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0003156C File Offset: 0x0002F76C
		private static string SecretKeyGet(string Key)
		{
			string result;
			if (ModBase._SystemRepository < 1L)
			{
				result = "00000000";
			}
			else if (string.IsNullOrEmpty(Key))
			{
				result = "@;$ Abv2";
			}
			else
			{
				result = Strings.Mid(ModBase.StrFill(Conversions.ToString(ModBase.GetHash(Key)), "X", 8), 1, 8);
			}
			return result;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000315C0 File Offset: 0x0002F7C0
		internal static string SecretEncrypt(string SourceString, string Key = "")
		{
			Key = ModSecret.SecretKeyGet(Key);
			byte[] bytes = Encoding.UTF8.GetBytes(Key);
			byte[] bytes2 = Encoding.UTF8.GetBytes("95168702");
			DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] bytes3 = Encoding.UTF8.GetBytes(SourceString);
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateEncryptor(bytes, bytes2), CryptoStreamMode.Write))
				{
					cryptoStream.Write(bytes3, 0, bytes3.Length);
					cryptoStream.FlushFinalBlock();
					result = Convert.ToBase64String(memoryStream.ToArray());
				}
			}
			return result;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00031678 File Offset: 0x0002F878
		internal static string SecretDecrypt(string SourceString, string Key = "")
		{
			Key = ModSecret.SecretKeyGet(Key);
			byte[] bytes = Encoding.UTF8.GetBytes(Key);
			byte[] bytes2 = Encoding.UTF8.GetBytes("95168702");
			DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] array = Convert.FromBase64String(SourceString);
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateDecryptor(bytes, bytes2), CryptoStreamMode.Write))
				{
					cryptoStream.Write(array, 0, array.Length);
					cryptoStream.FlushFinalBlock();
					@string = Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			return @string;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00031730 File Offset: 0x0002F930
		internal static bool SecretRsaVerify(string SourceString, string Sign)
		{
			bool result;
			try
			{
				if (ModBase._SystemRepository < 1L)
				{
					result = false;
				}
				else
				{
					RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(512);
					rsacryptoServiceProvider.FromXmlString(ModSecret.indexerField.Replace("!", "").Replace("$", "+") + "/R1Frckd3/Sn+Zsx9aD6U2f" + Conversions.ToString(Math.Round(84m)) + "SdWMDlrRY9DfhQ==</Modulus><Exponent>AQAB<\\Exponent><\\RSAKeyValue>".Replace("\\", "/"));
					result = rsacryptoServiceProvider.VerifyData(Encoding.Default.GetBytes(SourceString), typeof(SHA256), Convert.FromBase64String(Sign));
				}
			}
			catch (Exception ex)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00031800 File Offset: 0x0002FA00
		public static void ViewReader(int config = -1)
		{
			try
			{
				if (ModSecret.configurationField != config || config < 0)
				{
					if (ModSecret.InsertReader((config >= 0) ? config : ModSecret.configurationField))
					{
						if (config >= 0)
						{
							ModSecret.configurationField = config;
						}
						switch (ModSecret.registryField)
						{
						case 1:
							ModSecret.m_ExpressionField = 999;
							break;
						case 2:
							ModSecret.m_GetterField = ModBase.RandomInteger(0, 359);
							ModSecret.m_TokenField = ModBase.RandomInteger(40, 70);
							ModSecret.m_ExpressionField = ModBase.RandomInteger(-20, 20);
							break;
						}
						ModSecret._InterpreterField = new ModBase.MyColor().FromHSL2((double)ModSecret.m_GetterField, (double)ModSecret.m_TokenField * 0.2, 25.0 + (double)ModSecret.m_ExpressionField * 0.3);
						checked
						{
							ModSecret.serializerField = new ModBase.MyColor().FromHSL2((double)ModSecret.m_GetterField, (double)ModSecret.m_TokenField, (double)(45 + ModSecret.m_ExpressionField));
							ModSecret.watcherField = new ModBase.MyColor().FromHSL2((double)ModSecret.m_GetterField, (double)ModSecret.m_TokenField, (double)(55 + ModSecret.m_ExpressionField));
							ModSecret.m_IdentifierField = new ModBase.MyColor().FromHSL2((double)ModSecret.m_GetterField, (double)ModSecret.m_TokenField, (double)(65 + ModSecret.m_ExpressionField));
						}
						ModSecret._SystemField = new ModBase.MyColor().FromHSL2((double)ModSecret.m_GetterField, (double)ModSecret.m_TokenField, 80.0 + (double)ModSecret.m_ExpressionField * 0.4);
						ModSecret.m_ParamField = new ModBase.MyColor().FromHSL2((double)ModSecret.m_GetterField, (double)ModSecret.m_TokenField, 91.0 + (double)ModSecret.m_ExpressionField * 0.1);
						ModSecret.m_TagField = new ModBase.MyColor().FromHSL2((double)ModSecret.m_GetterField, (double)ModSecret.m_TokenField, 94.0);
						ModSecret.m_ObserverField = new ModBase.MyColor().FromHSL2((double)ModSecret.m_GetterField, (double)ModSecret.m_TokenField, 97.0);
						ModSecret._StubField = ModSecret.m_IdentifierField * 0.3 + ModSecret._SystemField * 0.3 + ModSecret.m_StateField * 0.4;
						ModSecret.m_RulesField = new ModBase.MyColor(190.0, ModSecret.m_TagField);
						ModSecret._ConsumerField = new ModBase.MyColor(1.0, ModSecret.m_ObserverField);
						Application.Current.Resources["ColorBrush1"] = new SolidColorBrush(ModSecret._InterpreterField);
						Application.Current.Resources["ColorBrush2"] = new SolidColorBrush(ModSecret.serializerField);
						Application.Current.Resources["ColorBrush3"] = new SolidColorBrush(ModSecret.watcherField);
						Application.Current.Resources["ColorBrush4"] = new SolidColorBrush(ModSecret.m_IdentifierField);
						Application.Current.Resources["ColorBrush5"] = new SolidColorBrush(ModSecret._SystemField);
						Application.Current.Resources["ColorBrush6"] = new SolidColorBrush(ModSecret.m_ParamField);
						Application.Current.Resources["ColorBrush7"] = new SolidColorBrush(ModSecret.m_TagField);
						Application.Current.Resources["ColorBrush8"] = new SolidColorBrush(ModSecret.m_ObserverField);
						Application.Current.Resources["ColorBrushBg0"] = new SolidColorBrush(ModSecret._StubField);
						Application.Current.Resources["ColorBrushBg1"] = new SolidColorBrush(ModSecret.m_RulesField);
						Application.Current.Resources["ColorObject1"] = ModSecret._InterpreterField;
						Application.Current.Resources["ColorObject2"] = ModSecret.serializerField;
						Application.Current.Resources["ColorObject3"] = ModSecret.watcherField;
						Application.Current.Resources["ColorObject4"] = ModSecret.m_IdentifierField;
						Application.Current.Resources["ColorObject5"] = ModSecret._SystemField;
						Application.Current.Resources["ColorObject6"] = ModSecret.m_ParamField;
						Application.Current.Resources["ColorObject7"] = ModSecret.m_TagField;
						Application.Current.Resources["ColorObject8"] = ModSecret.m_ObserverField;
						Application.Current.Resources["ColorObjectBg0"] = ModSecret._StubField;
						Application.Current.Resources["ColorObjectBg1"] = ModSecret.m_RulesField;
						ModSecret.PushReader();
						if (ModSecret.configurationField != 12 && ModSecret.configurationField != 14 && ModSecret.registryField != 2)
						{
							ModBase.Log("[UI] 刷新主题：" + Conversions.ToString(ModSecret.configurationField), ModBase.LogLevel.Normal, "出现错误");
						}
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "刷新主题颜色失败", ModBase.LogLevel.Hint, "出现错误");
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0000504E File Offset: 0x0000324E
		public static void PushReader()
		{
			ModBase.RunInUi((ModSecret._Closure$__.$I44-0 == null) ? (ModSecret._Closure$__.$I44-0 = delegate()
			{
				if (ModMain._ProcessIterator.IsLoaded)
				{
					LinearGradientBrush linearGradientBrush = new LinearGradientBrush
					{
						EndPoint = new Point(1.0, 0.0),
						StartPoint = new Point(0.0, 0.0)
					};
					checked
					{
						if (ModSecret.configurationField == 5)
						{
							linearGradientBrush.GradientStops.Add(new GradientStop
							{
								Offset = 0.0,
								Color = new ModBase.MyColor().FromHSL2((double)ModSecret.m_GetterField, (double)ModSecret.m_TokenField, 25.0)
							});
							linearGradientBrush.GradientStops.Add(new GradientStop
							{
								Offset = 0.5,
								Color = new ModBase.MyColor().FromHSL2((double)ModSecret.m_GetterField, (double)ModSecret.m_TokenField, 15.0)
							});
							linearGradientBrush.GradientStops.Add(new GradientStop
							{
								Offset = 1.0,
								Color = new ModBase.MyColor().FromHSL2((double)ModSecret.m_GetterField, (double)ModSecret.m_TokenField, 25.0)
							});
							ModMain._ProcessIterator.PanTitle.Background = linearGradientBrush;
							ModMain._ProcessIterator.PanTitle.Background.Freeze();
						}
						else if (ModSecret.configurationField != 12 && ModSecret.registryField != 2)
						{
							if (ModSecret._WriterField is int)
							{
								linearGradientBrush.GradientStops.Add(new GradientStop
								{
									Offset = 0.0,
									Color = new ModBase.MyColor().FromHSL2(Conversions.ToDouble(Operators.SubtractObject(ModSecret.m_GetterField, ModSecret._WriterField)), (double)ModSecret.m_TokenField, (double)(48 + ModSecret.m_ExpressionField))
								});
								linearGradientBrush.GradientStops.Add(new GradientStop
								{
									Offset = 0.5,
									Color = new ModBase.MyColor().FromHSL2((double)ModSecret.m_GetterField, (double)ModSecret.m_TokenField, (double)(54 + ModSecret.m_ExpressionField))
								});
								linearGradientBrush.GradientStops.Add(new GradientStop
								{
									Offset = 1.0,
									Color = new ModBase.MyColor().FromHSL2(Conversions.ToDouble(Operators.AddObject(ModSecret.m_GetterField, ModSecret._WriterField)), (double)ModSecret.m_TokenField, (double)(48 + ModSecret.m_ExpressionField))
								});
							}
							else
							{
								linearGradientBrush.GradientStops.Add(new GradientStop
								{
									Offset = 0.0,
									Color = new ModBase.MyColor().FromHSL2(Conversions.ToDouble(Operators.AddObject(ModSecret.m_GetterField, NewLateBinding.LateIndexGet(ModSecret._WriterField, new object[]
									{
										0
									}, null))), (double)ModSecret.m_TokenField, (double)(48 + ModSecret.m_ExpressionField))
								});
								linearGradientBrush.GradientStops.Add(new GradientStop
								{
									Offset = 0.5,
									Color = new ModBase.MyColor().FromHSL2(Conversions.ToDouble(Operators.AddObject(ModSecret.m_GetterField, NewLateBinding.LateIndexGet(ModSecret._WriterField, new object[]
									{
										1
									}, null))), (double)ModSecret.m_TokenField, (double)(54 + ModSecret.m_ExpressionField))
								});
								linearGradientBrush.GradientStops.Add(new GradientStop
								{
									Offset = 1.0,
									Color = new ModBase.MyColor().FromHSL2(Conversions.ToDouble(Operators.AddObject(ModSecret.m_GetterField, NewLateBinding.LateIndexGet(ModSecret._WriterField, new object[]
									{
										2
									}, null))), (double)ModSecret.m_TokenField, (double)(48 + ModSecret.m_ExpressionField))
								});
							}
							ModMain._ProcessIterator.PanTitle.Background = linearGradientBrush;
							ModMain._ProcessIterator.PanTitle.Background.Freeze();
						}
						else
						{
							linearGradientBrush.GradientStops.Add(new GradientStop
							{
								Offset = 0.0,
								Color = new ModBase.MyColor().FromHSL2((double)(ModSecret.m_GetterField - 21), (double)ModSecret.m_TokenField, (double)(53 + ModSecret.m_ExpressionField))
							});
							linearGradientBrush.GradientStops.Add(new GradientStop
							{
								Offset = 0.33,
								Color = new ModBase.MyColor().FromHSL2((double)(ModSecret.m_GetterField - 7), (double)ModSecret.m_TokenField, (double)(47 + ModSecret.m_ExpressionField))
							});
							linearGradientBrush.GradientStops.Add(new GradientStop
							{
								Offset = 0.67,
								Color = new ModBase.MyColor().FromHSL2((double)(ModSecret.m_GetterField + 7), (double)ModSecret.m_TokenField, (double)(47 + ModSecret.m_ExpressionField))
							});
							linearGradientBrush.GradientStops.Add(new GradientStop
							{
								Offset = 1.0,
								Color = new ModBase.MyColor().FromHSL2((double)(ModSecret.m_GetterField + 21), (double)ModSecret.m_TokenField, (double)(53 + ModSecret.m_ExpressionField))
							});
							ModMain._ProcessIterator.PanTitle.Background = linearGradientBrush;
						}
						if (ModSecret.configurationField >= 5 && ModSecret.configurationField != 14)
						{
							ModMain._ProcessIterator.ImgTitle.Source = string.Format("{0}Themes/{1}.png", ModBase.m_SerializerRepository, ModSecret.configurationField);
						}
						else
						{
							ModMain._ProcessIterator.ImgTitle.Source = null;
						}
						ModMain._ProcessIterator.ImgTitle.Opacity = ((ModSecret.configurationField == 13) ? 0.25 : 0.5);
					}
					if (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiBackgroundColorful", null)))
					{
						linearGradientBrush = new LinearGradientBrush
						{
							EndPoint = new Point(0.1, 1.0),
							StartPoint = new Point(0.9, 0.0)
						};
						linearGradientBrush.GradientStops.Add(new GradientStop
						{
							Offset = -0.1,
							Color = new ModBase.MyColor().FromHSL2((double)(checked(ModSecret.m_GetterField - 20)), (double)Math.Min(60, ModSecret.m_TokenField) * 0.5, 80.0)
						});
						linearGradientBrush.GradientStops.Add(new GradientStop
						{
							Offset = 0.4,
							Color = new ModBase.MyColor().FromHSL2((double)ModSecret.m_GetterField, (double)ModSecret.m_TokenField * 0.9, 90.0)
						});
						linearGradientBrush.GradientStops.Add(new GradientStop
						{
							Offset = 1.1,
							Color = new ModBase.MyColor().FromHSL2((double)(checked(ModSecret.m_GetterField + 20)), (double)Math.Min(60, ModSecret.m_TokenField) * 0.5, 80.0)
						});
						ModMain._ProcessIterator.PanForm.Background = linearGradientBrush;
					}
					else
					{
						ModMain._ProcessIterator.PanForm.Background = new ModBase.MyColor(245.0, 245.0, 245.0);
					}
					ModMain._ProcessIterator.PanForm.Background.Freeze();
				}
			}) : ModSecret._Closure$__.$I44-0, false);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00031DA0 File Offset: 0x0002FFA0
		public static void ResetReader(bool isres)
		{
			ModSecret._Closure$__45-0 CS$<>8__locals1 = new ModSecret._Closure$__45-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_EffectSetup = isres;
			ModBase.RunInUi(checked(delegate()
			{
				try
				{
					string text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("UiLauncherThemeHide2", null));
					if (Operators.CompareString(text, "", false) == 0)
					{
						text = "3";
					}
					if (ModBase.m_IdentifierRepository.Get("UiLauncherThemeHide", null).ToString().Contains("7"))
					{
						text += "|7";
					}
					if (ModBase.m_IdentifierRepository.Get("UiLauncherThemeHide", null).ToString().Contains("6"))
					{
						text += "|6";
					}
					text = Enumerable.ToList<string>(Enumerable.SkipWhile<string>(Enumerable.Distinct<string>(text.Split("|")), (ModSecret._Closure$__.$I45-1 == null) ? (ModSecret._Closure$__.$I45-1 = ((string Data) => string.IsNullOrEmpty(Data))) : ModSecret._Closure$__.$I45-1)).Join("|");
					ModBase.m_IdentifierRepository.Set("UiLauncherThemeHide2", text, false, null);
					List<string> list = new List<string>(text.Split("|"));
					int num = 0;
					try
					{
						foreach (string value in list)
						{
							int num2 = Conversions.ToInteger(value);
							if (num2 >= 5 && num2 <= 14 && num2 != 8)
							{
								num++;
							}
						}
					}
					finally
					{
						List<string>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					if (ModSecret.DeleteReader(null))
					{
						num++;
					}
					string text2 = Conversions.ToString(ModBase.m_IdentifierRepository.Get("UiLauncherTheme", null));
					if (!ModSecret.InsertReader(Conversions.ToInteger(text2)))
					{
						ModBase.Log("[UI] 检测到尚未解锁的主题：" + text2 + "，已重置为默认主题", ModBase.LogLevel.Normal, "出现错误");
						ModBase.m_IdentifierRepository.Set("UiLauncherTheme", 0, false, null);
					}
					if (CS$<>8__locals1.$VB$Local_EffectSetup)
					{
					}
				}
				catch (Exception ex)
				{
					if ((ex.InnerException ?? ex) is FormatException)
					{
						ModBase.Log(ex, "解锁的主题列表存档已损坏，主题解锁已被重置", ModBase.LogLevel.Msgbox, "出现错误");
						ModBase.m_IdentifierRepository.Set("UiLauncherThemeHide2", "", false, null);
					}
					else
					{
						ModBase.Log(ex, "检查主题失败", ModBase.LogLevel.Feedback, "出现错误");
					}
				}
			}), false);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00031DD0 File Offset: 0x0002FFD0
		public static bool InsertReader(int reference_High)
		{
			checked
			{
				bool result;
				if (reference_High == 8)
				{
					result = ModSecret.DeleteReader(null);
				}
				else if (reference_High == 14)
				{
					int num = 0;
					string[] array = ModBase.m_IdentifierRepository.Get("UiLauncherThemeHide2", null).ToString().Split("|");
					for (int i = 0; i < array.Length; i++)
					{
						int num2 = Conversions.ToInteger(array[i]);
						if (num2 >= 5 && num2 <= 14 && num2 != 8)
						{
							num++;
						}
					}
					if (ModSecret.DeleteReader(null))
					{
						num++;
					}
					result = (num >= 5);
				}
				else
				{
					result = (Enumerable.Contains<int>(new int[]
					{
						0,
						1,
						2,
						3,
						4,
						42
					}, reference_High) || Enumerable.Contains<string>(ModBase.m_IdentifierRepository.Get("UiLauncherThemeHide2", null).ToString().Split("|"), reference_High.ToString()));
				}
				return result;
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000507A File Offset: 0x0000327A
		internal static bool ThemeUnlock(int Id, bool ShowDoubleHint = true, string UnlockHint = null)
		{
			return false;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00031EA0 File Offset: 0x000300A0
		public static bool DeleteReader(string value = null)
		{
			bool result;
			try
			{
				string sign = (value ?? ModBase.m_IdentifierRepository.Get("UiLauncherThemeGold", null)).ToString().Replace("#", "");
				result = ModSecret.SecretRsaVerify("Gold|0|" + ModBase._TagRepository.Replace("-", ""), sign);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "检查秋仪金失败", ModBase.LogLevel.Feedback, "出现错误");
				result = false;
			}
			return result;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00031F30 File Offset: 0x00030130
		public static bool? DonateCodeInput()
		{
			ModMain.Hint("正式版无法使用主题功能，若你获得了解锁码，请在快照版中使用！", ModMain.HintType.Info, true);
			return null;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00031F54 File Offset: 0x00030154
		public static void UpdateCheckByButton()
		{
			if (ModSecret.m_SetterField)
			{
				ModMain.Hint("正在检查更新，请稍候！", ModMain.HintType.Info, true);
				return;
			}
			ModSecret.m_SetterField = true;
			ModBase.RunInThread((ModSecret._Closure$__.$I51-0 == null) ? (ModSecret._Closure$__.$I51-0 = delegate()
			{
				try
				{
					ModMain.Hint("正在检查更新，请稍候！", ModMain.HintType.Info, true);
					ModSecret._ConnectionField.WaitForExit(null, null, false);
					bool? flag = PageSetupSystem.IsLauncherNewest();
					if (flag == null)
					{
						ModMain.Hint("连接 PCL 服务器失败，请确认系统时间是否准确，尝试将 PCL 加入杀毒软件或防火墙白名单，然后重启 PCL！", ModMain.HintType.Critical, true);
					}
					else if (flag.GetValueOrDefault())
					{
						ModMain.Hint(string.Format("已经是最新{0} {1}，不需要更新啦，可以直接使用！", "正式版", "2.8.12"), ModMain.HintType.Finish, true);
					}
					else if (ModMain.MyMsgBox("发现启动器更新，是否立即下载？", "发现更新！", "更新！冲！", "取消", "", false, true, false, null, null, null) != 2)
					{
						ModSecret.UpdateStart("https://pcl2-server-1253424809.file.myqcloud.com/update/{KEY}.zip{CDN}", false, null, false);
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "尝试手动开始更新失败", ModBase.LogLevel.Feedback, "出现错误");
				}
				finally
				{
					ModSecret.m_SetterField = false;
				}
			}) : ModSecret._Closure$__.$I51-0);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00031FA4 File Offset: 0x000301A4
		public static void UpdateStart(string BaseUrl, bool Slient, string ReceivedKey = null, bool ForceValidated = false)
		{
			ModSecret._Closure$__55-0 CS$<>8__locals1 = new ModSecret._Closure$__55-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_BaseUrl = BaseUrl;
			if (ModSecret._FactoryField)
			{
				if (!Slient)
				{
					ModMain.Hint("PCL 正在下载更新，更新结束时将自动重启，请稍候！", ModMain.HintType.Info, true);
					ModSecret._ImporterField = false;
				}
				if (ModSecret.m_ExporterField)
				{
					ModSecret.UpdateRestart(true);
					return;
				}
			}
			else
			{
				ModSecret._FactoryField = true;
				ModSecret._ImporterField = Slient;
				ModBase.Log("[System] 更新已开始，静默：" + Conversions.ToString(ModSecret._ImporterField), ModBase.LogLevel.Normal, "出现错误");
				CS$<>8__locals1.$VB$Local_UpdateKey = "Publi3";
				if (!ModSecret._ImporterField)
				{
					ModMain.Hint("PCL 正在下载更新，更新结束时将自动重启，请稍候！", ModMain.HintType.Info, true);
				}
				ModBase.RunInUi(delegate()
				{
					try
					{
						ModSecret._Closure$__55-1 CS$<>8__locals2 = new ModSecret._Closure$__55-1(CS$<>8__locals2);
						CS$<>8__locals1.$VB$Local_BaseUrl = CS$<>8__locals1.$VB$Local_BaseUrl.Replace("{KEY}", CS$<>8__locals1.$VB$Local_UpdateKey);
						string[] urls;
						if (CS$<>8__locals1.$VB$Local_BaseUrl.EndsWithF(string.Format("pcl2-server-1253424809.file.myqcloud.com/update/{0}.zip{{CDN}}", "Publi3"), true))
						{
							urls = new string[]
							{
								"https://github.com/Hex-Dragon/PCL2/raw/main/%E6%9C%80%E6%96%B0%E6%AD%A3%E5%BC%8F%E7%89%88.zip",
								CS$<>8__locals1.$VB$Local_BaseUrl
							};
						}
						else
						{
							urls = new string[]
							{
								CS$<>8__locals1.$VB$Local_BaseUrl
							};
						}
						CS$<>8__locals2.$VB$Local_Loader = new ModLoader.LoaderCombo<string>("启动器更新", new ModLoader.LoaderBase[]
						{
							new ModNet.LoaderDownload("下载更新文件", new List<ModNet.NetFile>
							{
								new ModNet.NetFile(urls, ModBase.m_DecoratorRepository + "Update.zip", new ModBase.FileChecker(1048576L, -1L, null, false, false), false)
							})
							{
								ProgressWeight = 1.0
							},
							new ModLoader.LoaderTask<string, int>("安装更新", (ModSecret._Closure$__.$IR55-1 == null) ? (ModSecret._Closure$__.$IR55-1 = delegate(ModLoader.LoaderTask<string, int> a0)
							{
								((ModSecret._Closure$__.$I55-1 == null) ? (ModSecret._Closure$__.$I55-1 = delegate()
								{
									string text = ModBase.Path + "PCL\\Plain Craft Launcher 2.exe";
									if (File.Exists(text))
									{
										File.Delete(text);
										ModBase.Log("[System] 已清理存在的更新文件：" + text, ModBase.LogLevel.Normal, "出现错误");
									}
									else
									{
										ModBase.Log("[System] 无需清理目标更新文件：" + text, ModBase.LogLevel.Normal, "出现错误");
									}
									ModBase.ExtractFile(ModBase.m_DecoratorRepository + "Update.zip", ModBase.Path + "PCL\\", null, null);
									File.Delete(ModBase.m_DecoratorRepository + "Update.zip");
									ModBase.Log("[System] 更新文件解压完成", ModBase.LogLevel.Normal, "出现错误");
									if (ModSecret._ImporterField)
									{
										ModSecret.m_ExporterField = true;
										return;
									}
									if (ModLaunch.databaseTests.State == ModBase.LoadState.Loading)
									{
										ModMain.Hint("更新已准备就绪，PCL 将在游戏启动完成后重启！", ModMain.HintType.Finish, true);
										while (ModLaunch.databaseTests.State == ModBase.LoadState.Loading)
										{
											Thread.Sleep(10);
										}
									}
									ModSecret.UpdateRestart(true);
								}) : ModSecret._Closure$__.$I55-1)();
							}) : ModSecret._Closure$__.$IR55-1, null, ThreadPriority.Normal)
							{
								ProgressWeight = 0.1
							}
						});
						CS$<>8__locals2.$VB$Local_Loader.OnStateChanged = delegate(ModLoader.LoaderBase a0)
						{
							base._Lambda$__2();
						};
						CS$<>8__locals2.$VB$Local_Loader.Start(null, false);
						if (!ModSecret._ImporterField)
						{
							ModLoader.LoaderTaskbarAdd<string>(CS$<>8__locals2.$VB$Local_Loader);
							ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
						}
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "开始启动器更新失败", ModBase.LogLevel.Feedback, "出现错误");
					}
				}, false);
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00032044 File Offset: 0x00030244
		public static void UpdateRestart(bool TriggerRestartAndByEnd)
		{
			try
			{
				ModSecret.m_ExporterField = false;
				string fileName = ModBase.Path + "PCL\\Plain Craft Launcher 2.exe";
				string text = string.Concat(new string[]
				{
					"--update ",
					Conversions.ToString(Process.GetCurrentProcess().Id),
					" \"",
					AppDomain.CurrentDomain.SetupInformation.ApplicationName,
					"\" \"",
					AppDomain.CurrentDomain.SetupInformation.ApplicationName,
					"\" ",
					Conversions.ToString(TriggerRestartAndByEnd)
				});
				ModBase.Log("[System] 更新程序启动，参数：" + text, ModBase.LogLevel.Normal, "出现错误");
				Process.Start(new ProcessStartInfo(fileName)
				{
					WindowStyle = ProcessWindowStyle.Hidden,
					CreateNoWindow = true,
					Arguments = text
				});
				if (TriggerRestartAndByEnd)
				{
					ModMain._ProcessIterator.EndProgram(false);
					ModBase.Log("[System] 已由于更新强制结束程序", ModBase.LogLevel.Normal, "出现错误");
				}
			}
			catch (Win32Exception ex)
			{
				ModBase.Log(ex, "自动更新时触发 Win32 错误，疑似被拦截", ModBase.LogLevel.Debug, "出现错误");
				if (ModMain.MyMsgBox(string.Format("由于被 Windows 安全中心拦截，或者存在权限问题，导致 PCL 无法更新。{0}请将 PCL 所在文件夹加入白名单，或者手动用 {1}PCL\\Plain Craft Launcher 2.exe 替换当前文件！", "\r\n", ModBase.Path), "更新失败", "查看帮助", "确定", "", true, true, false, null, null, null) == 1)
				{
					ModEvent.TryStartEvent("打开帮助", "启动器/Microsoft Defender 添加排除项.json");
				}
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000321A0 File Offset: 0x000303A0
		public static void UpdateReplace(int ProcessId, string OldFileName, string NewFileName, bool TriggerRestart)
		{
			try
			{
				Process.GetProcessById(ProcessId).Kill();
			}
			catch (Exception ex)
			{
			}
			checked
			{
				string path = Strings.Mid(ModBase.Path, 1, ModBase.Path.Length - 4) + ModBase.GetFileNameFromPath(OldFileName);
				string text = Strings.Mid(ModBase.Path, 1, ModBase.Path.Length - 4) + ModBase.GetFileNameFromPath(NewFileName);
				Exception ex2 = null;
				int num = 0;
				do
				{
					try
					{
						if (File.Exists(path))
						{
							File.Delete(path);
						}
						if (File.Exists(text))
						{
							File.Delete(text);
						}
						if (!File.Exists(path) && !File.Exists(text))
						{
							break;
						}
						Thread.Sleep(2000);
					}
					catch (Exception ex3)
					{
						ex2 = ex3;
					}
					num++;
				}
				while (num <= 4);
				if (!File.Exists(path) && !File.Exists(text))
				{
					try
					{
						ModBase.CopyFile(ModBase.interpreterRepository, text);
					}
					catch (UnauthorizedAccessException ex4)
					{
						Interaction.MsgBox("PCL 更新失败：权限不足。请手动复制 PCL 文件夹下的新版本程序。\r\n若 PCL 位于桌面或 C 盘，你可以尝试将其挪到其他文件夹，这可能可以解决权限问题。\r\n" + ModBase.GetExceptionSummary(ex4), MsgBoxStyle.Critical, "更新失败");
					}
					catch (Exception ex5)
					{
						Interaction.MsgBox("PCL 更新失败：无法复制新文件。请手动复制 PCL 文件夹下的新版本程序。\r\n" + ModBase.GetExceptionSummary(ex5), MsgBoxStyle.Critical, "更新失败");
						return;
					}
					if (TriggerRestart)
					{
						try
						{
							Process.Start(text);
						}
						catch (Exception ex6)
						{
							Interaction.MsgBox("PCL 更新失败：无法重新启动。\r\n" + ModBase.GetExceptionSummary(ex6), MsgBoxStyle.Critical, "更新失败");
						}
					}
					return;
				}
				if (ex2 is UnauthorizedAccessException)
				{
					Interaction.MsgBox(string.Concat(new string[]
					{
						"由于权限不足，PCL 无法完成更新。请尝试：\r\n",
						(text.StartsWithF(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), false) || text.StartsWithF(Environment.GetFolderPath(Environment.SpecialFolder.Personal), false)) ? " - 将 PCL 文件移动到桌面、文档以外的文件夹（这或许可以一劳永逸地解决权限问题）\r\n" : "",
						text.StartsWithF("C", true) ? " - 将 PCL 文件移动到 C 盘以外的文件夹（这或许可以一劳永逸地解决权限问题）\r\n" : "",
						" - 右键以管理员身份运行 PCL\r\n - 手动复制已下载到 PCL 文件夹下的新版本程序，覆盖原程序\r\n\r\n",
						ModBase.GetExceptionSummary(ex2)
					}), MsgBoxStyle.Critical, "更新失败");
					return;
				}
				Interaction.MsgBox("PCL 更新失败：无法删除原文件。请手动复制已下载到 PCL 文件夹下的新版本程序覆盖原程序。\r\n" + ModBase.GetExceptionSummary(ex2), MsgBoxStyle.Critical, "更新失败");
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00032400 File Offset: 0x00030600
		private static void ServerSub(ModLoader.LoaderTask<int, int> Loader)
		{
			try
			{
				if (File.Exists(ModBase.Path + "PCL\\update.exe"))
				{
					File.Delete(ModBase.Path + "PCL\\update.exe");
					ModBase.Log("[Server] 已清理更新缓存", ModBase.LogLevel.Normal, "出现错误");
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "清理更新缓存失败", ModBase.LogLevel.Debug, "出现错误");
			}
			ModBase.Log("[Server] 正在连接到 PCL 服务器", ModBase.LogLevel.Normal, "出现错误");
			try
			{
				ModSecret.ServerSubReal();
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, "连接到 PCL 服务器失败", ModBase.LogLevel.Debug, "出现错误");
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000324BC File Offset: 0x000306BC
		private static void ServerSubReal()
		{
			int num = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("HintNotice", null));
			int num2 = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("HintDownload", null));
			checked
			{
				int num3;
				int num4;
				try
				{
					string text = Conversions.ToString(ModNet.NetGetCodeByRequestRetry("https://pcl2-server-1253424809.file.myqcloud.com/notice.cfg{CDN}", null, "", false, null, false));
					num3 = (int)Math.Round(ModBase.Val(text.Split("|")[0]));
					num4 = (int)Math.Round(ModBase.Val(text.Split("|")[3]));
					if (num3 == 0)
					{
						throw new Exception("获取到的内容有误！（" + text + "）");
					}
					if (num3 > num)
					{
						ModBase.Log(string.Concat(new string[]
						{
							"[Server] 服务器公告：",
							text,
							"，本地公告编号：",
							Conversions.ToString(num),
							"，需更新"
						}), ModBase.LogLevel.Normal, "出现错误");
					}
					else
					{
						ModBase.Log("[Server] 服务器公告：" + text + "，无需更新", ModBase.LogLevel.Normal, "出现错误");
					}
					ModBase.WriteFile(ModBase.m_DecoratorRepository + "Cache\\Notice.cfg", text, false, null);
				}
				catch (Exception ex)
				{
					if (!(ex is InvalidOperationException) || !ex.Message.Contains("FIPS"))
					{
						ModBase.Log(ex, "获取 PCL 服务器状态失败", ModBase.LogLevel.Debug, "出现错误");
						return;
					}
					if (ModMain.MyMsgBox("由于系统未启用 FIPS 兼容算法，PCL 可能无法正常运行。\r\n请按照教程启用该功能，然后重启 PCL。", "兼容性警告", "打开教程", "取消", "", false, true, false, null, null, null) == 1)
					{
						ModBase.OpenWebsite("https://blog.csdn.net/qq_37608398/article/details/81209922");
					}
				}
				try
				{
					if (num4 > num2 || !File.Exists(ModBase.m_DecoratorRepository + "Cache\\download.json"))
					{
						string text2 = ModNet.NetGetCodeByDownload("https://pcl2-server-1253424809.file.myqcloud.com/minecraft/download.json{CDN}", 45000, true, false);
						Directory.CreateDirectory(ModBase.m_DecoratorRepository + "Cache\\");
						ModBase.WriteFile(ModBase.m_DecoratorRepository + "Cache\\download.json", text2, false, null);
					}
					ModBase.m_IdentifierRepository.Set("HintDownload", num4, false, null);
				}
				catch (Exception ex2)
				{
					ModBase.Log(ex2, "下载 PCL 特供版信息失败", ModBase.LogLevel.Debug, "出现错误");
					File.Delete(ModBase.m_DecoratorRepository + "Cache\\download.json");
				}
				string text3;
				try
				{
					if (num3 <= num && File.Exists(ModBase.m_DecoratorRepository + "Cache\\Notice.json"))
					{
						text3 = ModBase.ReadFile(ModBase.m_DecoratorRepository + "Cache\\Notice.json", null);
					}
					else
					{
						text3 = Conversions.ToString(ModNet.NetGetCodeByRequestRetry("https://pcl2-server-1253424809.file.myqcloud.com/notice.json{CDN}", null, "", false, null, false));
						ModBase.WriteFile(ModBase.m_DecoratorRepository + "Cache\\Notice.json", text3, false, null);
					}
					ModBase.m_IdentifierRepository.Set("HintNotice", num3, false, null);
				}
				catch (Exception ex3)
				{
					ModBase.Log(ex3, "下载 PCL 服务器公告失败", ModBase.LogLevel.Debug, "出现错误");
					File.Delete(ModBase.m_DecoratorRepository + "Cache\\Notice.json");
					return;
				}
				try
				{
					try
					{
						foreach (object obj in ((IEnumerable)ModBase.GetJson(text3.Replace("{UNIQUE}", ModBase._TagRepository))))
						{
							JObject jobject = (JObject)obj;
							int num5 = (int)Math.Round(ModBase.Val(jobject["id"] ?? 0));
							if (num5 > num)
							{
								bool flag = true;
								try
								{
									foreach (JToken jtoken in (jobject["requirements"] ?? new byte[0]))
									{
										JProperty jproperty = (JProperty)jtoken;
										if (!ModSecret.ServerRequirement(jproperty.Name, jproperty.Value))
										{
											flag = false;
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
								if (flag)
								{
									int num6 = (int)(jobject["importantLevel"] ?? 2);
									bool flag2 = (bool)(jobject["isUpdate"] ?? false);
									ModBase.Log("[Server] 重要等级 " + Conversions.ToString(num6) + "，更新公告 " + Conversions.ToString(flag2), ModBase.LogLevel.Normal, "出现错误");
									if (!Operators.ConditionalCompareObjectGreater(ModBase.m_IdentifierRepository.Get(flag2 ? "SystemSystemUpdate" : "SystemSystemActivity", null), num6, false) && (!flag2 || !ModSecret._FactoryField))
									{
										string title = (jobject["title"] ?? "公告").ToString();
										string text4 = (jobject["description"] ?? "").ToString().Replace("\\n", "\r\n");
										JContainer jcontainer = (JContainer)(jobject["buttons"] ?? ModBase.GetJson("[]"));
										int count = jcontainer.Count;
										object obj2 = new object[0];
										if (count > 0)
										{
											ModBase.Log("[Server] 显示公告 " + Conversions.ToString(num5) + "：" + text4, ModBase.LogLevel.Normal, "出现错误");
											int num7;
											switch (count)
											{
											case 1:
												num7 = ModMain.MyMsgBox(text4, title, (jcontainer[0]["text"] ?? "确定").ToString(), "", "", false, true, true, null, null, null);
												break;
											case 2:
												num7 = ModMain.MyMsgBox(text4, title, (jcontainer[0]["text"] ?? "确定").ToString(), (jcontainer[1]["text"] ?? "确定").ToString(), "", false, true, false, null, null, null);
												break;
											case 3:
												num7 = ModMain.MyMsgBox(text4, title, (jcontainer[0]["text"] ?? "确定").ToString(), (jcontainer[1]["text"] ?? "确定").ToString(), (jcontainer[2]["text"] ?? "确定").ToString(), false, true, false, null, null, null);
												break;
											default:
												ModBase.Log(string.Concat(new string[]
												{
													"[Server] 公告 ",
													Conversions.ToString(num5),
													" 的弹窗有 ",
													Conversions.ToString(count),
													" 个按钮，无法显示"
												}), ModBase.LogLevel.Debug, "出现错误");
												continue;
											}
											num7 = (int)Math.Round(ModBase.MathClamp((double)num7, 1.0, (double)count));
											obj2 = (jcontainer[num7 - 1]["actions"] ?? new byte[0]);
										}
										else
										{
											obj2 = (jobject["actions"] ?? new byte[0]);
										}
										try
										{
											foreach (object obj3 in ((IEnumerable)obj2))
											{
												JProperty jproperty2 = (JProperty)obj3;
												try
												{
													string name = jproperty2.Name;
													if (Operators.CompareString(name, "copy", false) != 0)
													{
														if (Operators.CompareString(name, "website", false) != 0)
														{
															if (Operators.CompareString(name, "stop", false) != 0)
															{
																if (Operators.CompareString(name, "setup", false) != 0)
																{
																	if (Operators.CompareString(name, "slientupdate", false) != 0)
																	{
																		if (Operators.CompareString(name, "update", false) != 0)
																		{
																			throw new Exception("未知的行动支：" + jproperty2.Name + ", " + jproperty2.Value.ToString());
																		}
																		ModSecret.UpdateStart((((JObject)jproperty2.Value)["url"] ?? "https://pcl2-server-1253424809.file.myqcloud.com/update/{KEY}.zip{CDN}").ToString(), false, null, true);
																	}
																	else
																	{
																		ModSecret.UpdateStart((((JObject)jproperty2.Value)["url"] ?? "https://pcl2-server-1253424809.file.myqcloud.com/update/{KEY}.zip{CDN}").ToString(), true, null, true);
																	}
																}
																else
																{
																	ModBase.m_IdentifierRepository.Set(((JProperty)jproperty2.Value).Name, ((JProperty)jproperty2.Value).Value.ToString(), false, null);
																}
															}
															else
															{
																ModMain._ProcessIterator.EndProgram(false);
															}
														}
														else
														{
															ModBase.OpenWebsite(jproperty2.Value.ToString());
														}
													}
													else
													{
														ModBase.ClipboardSet(jproperty2.Value.ToString(), true);
													}
												}
												catch (Exception ex4)
												{
													ModBase.Log(ex4, string.Concat(new string[]
													{
														"执行 PCL 服务器公告动作失败（",
														jproperty2.Name,
														", ",
														jproperty2.Value.ToString(),
														"）"
													}), ModBase.LogLevel.Hint, "出现错误");
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
				}
				catch (Exception ex5)
				{
					ModBase.Log(text3, ModBase.LogLevel.Normal, "出现错误");
					ModBase.Log(ex5, "读取 PCL 服务器公告失败（" + Conversions.ToString(text3.Length) + "）", ModBase.LogLevel.Hint, "出现错误");
					File.Delete(ModBase.m_DecoratorRepository + "Cache\\Notice.json");
				}
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00032F18 File Offset: 0x00031118
		private static bool ServerRequirement(string Name, JToken Value)
		{
			checked
			{
				bool result;
				try
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(Name);
					if (num <= 2200040294U)
					{
						if (num <= 1143669093U)
						{
							if (num != 127006290U)
							{
								if (num != 233440242U)
								{
									if (num == 1143669093U)
									{
										if (Operators.CompareString(Name, "setupboolean =", false) == 0)
										{
											return Operators.CompareString(ModBase.m_IdentifierRepository.Get(((JProperty)Value).Name, null).ToString().ToLower(), ((JProperty)Value).Value.ToString().ToLower(), false) == 0;
										}
									}
								}
								else if (Operators.CompareString(Name, "unlockedtheme =", false) == 0)
								{
									return ModSecret.InsertReader((int)Math.Round(ModBase.Val(Value)));
								}
							}
							else if (Operators.CompareString(Name, "versionbranch <=", false) == 0)
							{
								return ModBase.Val("50") <= ModBase.Val(Value);
							}
						}
						else if (num <= 1834020325U)
						{
							if (num != 1699755989U)
							{
								if (num == 1834020325U)
								{
									if (Operators.CompareString(Name, "setupinteger >=", false) == 0)
									{
										return Operators.ConditionalCompareObjectGreaterEqual(ModBase.m_IdentifierRepository.Get(((JProperty)Value).Name, null), ModBase.Val(((JProperty)Value).Value), false);
									}
								}
							}
							else if (Operators.CompareString(Name, "opencount >=", false) == 0)
							{
								return Operators.ConditionalCompareObjectGreaterEqual(ModBase.m_IdentifierRepository.Get("SystemCount", null), ModBase.Val(Value), false);
							}
						}
						else if (num != 1931416207U)
						{
							if (num == 2200040294U)
							{
								if (Operators.CompareString(Name, "keyhash !=", false) == 0)
								{
									return ModBase.GetHash(ModSecret.indexerField.ToLower()) != ModBase.GetHash(Value.ToString().ToLower());
								}
							}
						}
						else if (Operators.CompareString(Name, "debug =", false) == 0)
						{
							return Operators.CompareString(ModBase._TokenRepository.ToString().ToLower(), Value.ToString().ToLower(), false) == 0;
						}
					}
					else if (num <= 3063258025U)
					{
						if (num <= 2410018677U)
						{
							if (num != 2207725236U)
							{
								if (num == 2410018677U)
								{
									if (Operators.CompareString(Name, "d10000 <=", false) == 0)
									{
										return (double)ModBase.RandomInteger(1, 10000) <= ModBase.Val(Value);
									}
								}
							}
							else if (Operators.CompareString(Name, "versionbranch >=", false) == 0)
							{
								return ModBase.Val("50") >= ModBase.Val(Value);
							}
						}
						else if (num != 2561636181U)
						{
							if (num == 3063258025U)
							{
								if (Operators.CompareString(Name, "unlockedtheme !=", false) == 0)
								{
									return !ModSecret.InsertReader((int)Math.Round(ModBase.Val(Value)));
								}
							}
						}
						else if (Operators.CompareString(Name, "uniqueaddress =", false) == 0)
						{
							return Operators.CompareString(ModBase._TagRepository, Value.ToString(), false) == 0;
						}
					}
					else if (num <= 3317911237U)
					{
						if (num != 3250506571U)
						{
							if (num == 3317911237U)
							{
								if (Operators.CompareString(Name, "versioncode <=", false) == 0)
								{
									return 347.0 <= ModBase.Val(Value);
								}
							}
						}
						else if (Operators.CompareString(Name, "versioncode >=", false) == 0)
						{
							return 347.0 >= ModBase.Val(Value);
						}
					}
					else if (num != 3779783387U)
					{
						if (num == 3914047723U)
						{
							if (Operators.CompareString(Name, "setupinteger <=", false) == 0)
							{
								return Operators.ConditionalCompareObjectLessEqual(ModBase.m_IdentifierRepository.Get(((JProperty)Value).Name, null), ModBase.Val(((JProperty)Value).Value), false);
							}
						}
					}
					else if (Operators.CompareString(Name, "opencount <=", false) == 0)
					{
						return Operators.ConditionalCompareObjectLessEqual(ModBase.m_IdentifierRepository.Get("SystemCount", null), ModBase.Val(Value), false);
					}
					ModBase.Log("[Server] 未知的条件支：" + Name + ", " + Value.ToString(), ModBase.LogLevel.Debug, "出现错误");
					result = false;
				}
				catch (Exception ex)
				{
					ModBase.Log("[Server] 判断分支条件失败：" + Name + ", " + Value.ToString(), ModBase.LogLevel.Debug, "出现错误");
					result = false;
				}
				return result;
			}
		}

		// Token: 0x0400032D RID: 813
		internal static string indexerField = "<RS!AKeyValu!e><Mo!dul!us>0L/cZoJUyBRAIE8OKiG8$qYOytXD$azFCBsmOuQra";

		// Token: 0x0400032E RID: 814
		public static ModBase.MyColor _InterpreterField = new ModBase.MyColor(52.0, 61.0, 74.0);

		// Token: 0x0400032F RID: 815
		public static ModBase.MyColor serializerField = new ModBase.MyColor(11.0, 91.0, 203.0);

		// Token: 0x04000330 RID: 816
		public static ModBase.MyColor watcherField = new ModBase.MyColor(19.0, 112.0, 243.0);

		// Token: 0x04000331 RID: 817
		public static ModBase.MyColor m_IdentifierField = new ModBase.MyColor(72.0, 144.0, 245.0);

		// Token: 0x04000332 RID: 818
		public static ModBase.MyColor _SystemField = new ModBase.MyColor(150.0, 192.0, 249.0);

		// Token: 0x04000333 RID: 819
		public static ModBase.MyColor m_ParamField = new ModBase.MyColor(213.0, 230.0, 253.0);

		// Token: 0x04000334 RID: 820
		public static ModBase.MyColor m_TagField = new ModBase.MyColor(222.0, 236.0, 253.0);

		// Token: 0x04000335 RID: 821
		public static ModBase.MyColor m_ObserverField = new ModBase.MyColor(234.0, 242.0, 254.0);

		// Token: 0x04000336 RID: 822
		public static ModBase.MyColor _StubField = new ModBase.MyColor(150.0, 192.0, 249.0);

		// Token: 0x04000337 RID: 823
		public static ModBase.MyColor m_RulesField = new ModBase.MyColor(190.0, ModSecret.m_TagField);

		// Token: 0x04000338 RID: 824
		public static ModBase.MyColor m_RefField = new ModBase.MyColor(64.0, 64.0, 64.0);

		// Token: 0x04000339 RID: 825
		public static ModBase.MyColor m_DecoratorField = new ModBase.MyColor(115.0, 115.0, 115.0);

		// Token: 0x0400033A RID: 826
		public static ModBase.MyColor instanceField = new ModBase.MyColor(140.0, 140.0, 140.0);

		// Token: 0x0400033B RID: 827
		public static ModBase.MyColor m_StateField = new ModBase.MyColor(166.0, 166.0, 166.0);

		// Token: 0x0400033C RID: 828
		public static ModBase.MyColor callbackField = new ModBase.MyColor(204.0, 204.0, 204.0);

		// Token: 0x0400033D RID: 829
		public static ModBase.MyColor m_TemplateField = new ModBase.MyColor(235.0, 235.0, 235.0);

		// Token: 0x0400033E RID: 830
		public static ModBase.MyColor _MethodField = new ModBase.MyColor(240.0, 240.0, 240.0);

		// Token: 0x0400033F RID: 831
		public static ModBase.MyColor taskField = new ModBase.MyColor(245.0, 245.0, 245.0);

		// Token: 0x04000340 RID: 832
		public static ModBase.MyColor _ConsumerField = new ModBase.MyColor(1.0, ModSecret.m_ObserverField);

		// Token: 0x04000341 RID: 833
		public static int configurationField = -1;

		// Token: 0x04000342 RID: 834
		public static int m_GetterField = 210;

		// Token: 0x04000343 RID: 835
		public static int m_TokenField = 85;

		// Token: 0x04000344 RID: 836
		public static int m_ExpressionField = 0;

		// Token: 0x04000345 RID: 837
		public static object _WriterField = 0;

		// Token: 0x04000346 RID: 838
		public static int registryField = 0;

		// Token: 0x04000347 RID: 839
		private static bool _RuleField = false;

		// Token: 0x04000348 RID: 840
		private static int m_ProccesorField = 1;

		// Token: 0x04000349 RID: 841
		private static bool m_SetterField = false;

		// Token: 0x0400034A RID: 842
		public static bool _FactoryField = false;

		// Token: 0x0400034B RID: 843
		public static bool m_ExporterField = false;

		// Token: 0x0400034C RID: 844
		private static bool _ImporterField;

		// Token: 0x0400034D RID: 845
		private static bool _WorkerField = false;

		// Token: 0x0400034E RID: 846
		public static ModLoader.LoaderTask<int, int> _ConnectionField = new ModLoader.LoaderTask<int, int>("PCL 服务", new Action<ModLoader.LoaderTask<int, int>>(ModSecret.ServerSub), null, ThreadPriority.BelowNormal)
		{
			ReloadTimeout = 60000
		};
	}
}
