using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x0200008A RID: 138
	[StandardModule]
	public sealed class ModEvent
	{
		// Token: 0x06000390 RID: 912 RVA: 0x00026118 File Offset: 0x00024318
		public static void TryStartEvent(string Type, string Data)
		{
			if (!string.IsNullOrWhiteSpace(Type))
			{
				string[] data = new string[]
				{
					""
				};
				if (Data != null)
				{
					data = Data.Split("|");
				}
				ModEvent.StartEvent(Type, data);
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00026154 File Offset: 0x00024354
		public static void StartEvent(string Type, string[] Data)
		{
			checked
			{
				try
				{
					ModBase.Log("[Control] 执行自定义事件：" + Type + ", " + Data.Join(", "), ModBase.LogLevel.Normal, "出现错误");
					string $VB$Local_Type = Type;
					uint num = <PrivateImplementationDetails>.ComputeStringHash($VB$Local_Type);
					if (num <= 1214012690U)
					{
						if (num <= 213330431U)
						{
							if (num != 46857845U)
							{
								if (num != 87465761U)
								{
									if (num != 213330431U)
									{
										goto IL_5DB;
									}
									if (Operators.CompareString($VB$Local_Type, "打开文件", false) != 0)
									{
										goto IL_5DB;
									}
									goto IL_612;
								}
								else
								{
									if (Operators.CompareString($VB$Local_Type, "复制文本", false) == 0)
									{
										ModBase.ClipboardSet(Data.Join("|"), true);
										goto IL_623;
									}
									goto IL_5DB;
								}
							}
							else
							{
								if (Operators.CompareString($VB$Local_Type, "今日人品", false) == 0)
								{
									PageOtherTest.Jrrp();
									goto IL_623;
								}
								goto IL_5DB;
							}
						}
						else if (num <= 790387239U)
						{
							if (num != 425440694U)
							{
								if (num != 790387239U)
								{
									goto IL_5DB;
								}
								if (Operators.CompareString($VB$Local_Type, "清理垃圾", false) == 0)
								{
									ModBase.RunInThread((ModEvent._Closure$__.$I1-2 == null) ? (ModEvent._Closure$__.$I1-2 = delegate()
									{
										PageOtherTest.RubbishClear();
									}) : ModEvent._Closure$__.$I1-2);
									goto IL_623;
								}
								goto IL_5DB;
							}
							else if (Operators.CompareString($VB$Local_Type, "安装整合包", false) != 0)
							{
								goto IL_5DB;
							}
						}
						else if (num != 1018565114U)
						{
							if (num != 1214012690U)
							{
								goto IL_5DB;
							}
							if (Operators.CompareString($VB$Local_Type, "内存优化", false) == 0)
							{
								ModBase.RunInThread((ModEvent._Closure$__.$I1-1 == null) ? (ModEvent._Closure$__.$I1-1 = delegate()
								{
									PageOtherTest.MemoryOptimize(true);
								}) : ModEvent._Closure$__.$I1-1);
								goto IL_623;
							}
							goto IL_5DB;
						}
						else
						{
							if (Operators.CompareString($VB$Local_Type, "刷新主页", false) != 0)
							{
								goto IL_5DB;
							}
							ModMain.m_ServiceIterator.ForceRefresh();
							if (Operators.CompareString(Data[0], "", false) == 0)
							{
								ModMain.Hint("已刷新主页！", ModMain.HintType.Finish, true);
								goto IL_623;
							}
							goto IL_623;
						}
					}
					else if (num <= 2917375879U)
					{
						if (num <= 1629407123U)
						{
							if (num != 1534644891U)
							{
								if (num != 1629407123U)
								{
									goto IL_5DB;
								}
								if (Operators.CompareString($VB$Local_Type, "启动游戏", false) != 0)
								{
									goto IL_5DB;
								}
								if (Operators.CompareString(Data[0], "\\current", false) == 0)
								{
									if (ModMinecraft.AddClient() == null)
									{
										ModMain.Hint("请先选择一个 Minecraft 版本！", ModMain.HintType.Critical, true);
										return;
									}
									Data[0] = ModMinecraft.AddClient().Name;
								}
								if (ModLaunch.McLaunchStart(new ModLaunch.McLaunchOptions
								{
									m_MockMap = ((Data.Length >= 2) ? Data[1] : null),
									_DicMap = new ModMinecraft.McVersion(Data[0])
								}))
								{
									ModMain.Hint("正在启动 " + Data[0] + "……", ModMain.HintType.Info, true);
									goto IL_623;
								}
								goto IL_623;
							}
							else
							{
								if (Operators.CompareString($VB$Local_Type, "切换页面", false) == 0)
								{
									ModMain._ProcessIterator.PageChange((FormMain.PageType)Math.Round(ModBase.Val(Data[0])), (FormMain.PageSubType)Math.Round(ModBase.Val(Data[1])));
									goto IL_623;
								}
								goto IL_5DB;
							}
						}
						else if (num != 1804229068U)
						{
							if (num != 2917375879U)
							{
								goto IL_5DB;
							}
							if (Operators.CompareString($VB$Local_Type, "刷新帮助", false) == 0)
							{
								PageOtherLeft.RefreshHelp();
								goto IL_623;
							}
							goto IL_5DB;
						}
						else
						{
							if (Operators.CompareString($VB$Local_Type, "弹出窗口", false) == 0)
							{
								ModMain.MyMsgBox(Data[1].Replace("\\n", "\r\n"), Data[0].Replace("\\n", "\r\n"), "确定", "", "", false, true, false, null, null, null);
								goto IL_623;
							}
							goto IL_5DB;
						}
					}
					else if (num <= 3824712473U)
					{
						if (num != 3277022720U)
						{
							if (num != 3824712473U)
							{
								goto IL_5DB;
							}
							if (Operators.CompareString($VB$Local_Type, "导入整合包", false) != 0)
							{
								goto IL_5DB;
							}
						}
						else
						{
							if (Operators.CompareString($VB$Local_Type, "打开网页", false) != 0)
							{
								goto IL_5DB;
							}
							Data[0] = Data[0].Replace("\\", "/");
							if (Data[0].Contains("://") && !Data[0].StartsWithF("file", true))
							{
								ModMain.Hint("正在开启中，请稍候……", ModMain.HintType.Info, true);
								ModBase.OpenWebsite(Data[0]);
								goto IL_623;
							}
							ModMain.MyMsgBox("EventData 必须为一个网址。\r\n如果想要启动程序，请将 EventType 改为 打开文件。", "事件执行失败", "确定", "", "", false, true, false, null, null, null);
							return;
						}
					}
					else
					{
						if (num != 3900623875U)
						{
							if (num != 4108293856U)
							{
								goto IL_5DB;
							}
							if (Operators.CompareString($VB$Local_Type, "下载文件", false) != 0)
							{
								goto IL_5DB;
							}
							Data[0] = Data[0].Replace("\\", "/");
							if (!Data[0].StartsWithF("http://", true) && !Data[0].StartsWithF("https://", true))
							{
								ModMain.MyMsgBox("EventData 必须为以 http:// 或 https:// 开头的网址。\r\nPCL 不支持其他乱七八糟的下载协议。", "事件执行失败", "确定", "", "", false, true, false, null, null, null);
								return;
							}
							try
							{
								int num2 = Data.Length;
								if (num2 != 1)
								{
									if (num2 != 2)
									{
										PageOtherTest.StartCustomDownload(Data[0], Data[1], Data[2]);
									}
									else
									{
										PageOtherTest.StartCustomDownload(Data[0], Data[1], null);
									}
								}
								else
								{
									PageOtherTest.StartCustomDownload(Data[0], ModBase.GetFileNameFromPath(Data[0]), null);
								}
								goto IL_623;
							}
							catch (Exception ex)
							{
								PageOtherTest.StartCustomDownload(Data[0], "未知", null);
								goto IL_623;
							}
						}
						if (Operators.CompareString($VB$Local_Type, "打开帮助", false) != 0)
						{
							goto IL_5DB;
						}
						goto IL_612;
					}
					ModBase.RunInUi((ModEvent._Closure$__.$I1-3 == null) ? (ModEvent._Closure$__.$I1-3 = delegate()
					{
						ModModpack.ModpackInstall();
					}) : ModEvent._Closure$__.$I1-3, false);
					goto IL_623;
					IL_5DB:
					ModMain.MyMsgBox("未知的事件类型：" + Type + "\r\n请检查事件类型填写是否正确，或者 PCL 是否为最新版本。", "事件执行失败", "确定", "", "", false, true, false, null, null, null);
					goto IL_623;
					IL_612:
					ModBase.RunInThread(delegate
					{
						try
						{
							string[] eventAbsoluteUrls = ModEvent.GetEventAbsoluteUrls(Data[0], Type);
							string text = eventAbsoluteUrls[0];
							string text2 = eventAbsoluteUrls[1];
							ModBase.Log(string.Format("[Control] 打开类自定义事件实际路径：{0}，工作目录：{1}", text, text2), ModBase.LogLevel.Normal, "出现错误");
							if (Operators.CompareString(Type, "打开文件", false) == 0)
							{
								Process.Start(new ProcessStartInfo
								{
									Arguments = ((Data.Length >= 2) ? Data[1] : ""),
									FileName = text,
									WorkingDirectory = text2
								});
							}
							else
							{
								PageOtherHelp.EnterHelpPage(text);
							}
						}
						catch (Exception ex3)
						{
							ModBase.Log(ex3, "执行打开类自定义事件失败", ModBase.LogLevel.Msgbox, "出现错误");
						}
					});
					IL_623:;
				}
				catch (Exception ex2)
				{
					ModBase.Log(ex2, "事件执行失败", ModBase.LogLevel.Msgbox, "出现错误");
				}
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000267DC File Offset: 0x000249DC
		public static string[] GetEventAbsoluteUrls(string RelativeUrl, string EventType)
		{
			if (RelativeUrl.StartsWithF("http", true))
			{
				if (ModBase.RunInUi())
				{
					throw new Exception("能打开联网帮助页面的 MyListItem 必须手动设置 Title、Info 属性！");
				}
				string fileNameFromPath;
				try
				{
					fileNameFromPath = ModBase.GetFileNameFromPath(RelativeUrl);
					if (!fileNameFromPath.EndsWithF(".json", true))
					{
						throw new Exception("未指向 .json 后缀的文件");
					}
				}
				catch (Exception innerException)
				{
					throw new Exception("联网帮助页面须指向一个帮助 JSON 文件，并在同路径下包含相应 XAML 文件！\r\n例如：\r\n - https://www.baidu.com/test.json（填写这个路径）\r\n - https://www.baidu.com/test.xaml（同时也需要包含这个文件）", innerException);
				}
				string text = ModBase.m_DecoratorRepository + "CustomEvent\\" + fileNameFromPath;
				ModBase.m_DecoratorRepository + "CustomEvent\\" + fileNameFromPath.Replace(".json", ".xaml");
				ModBase.Log("[Event] 转换网络资源：" + RelativeUrl + " -> " + text, ModBase.LogLevel.Normal, "出现错误");
				try
				{
					ModNet.NetDownload(RelativeUrl, text, false);
					ModNet.NetDownload(RelativeUrl.Replace(".json", ".xaml"), text.Replace(".json", ".xaml"), false);
				}
				catch (Exception innerException2)
				{
					throw new Exception("下载指定的文件失败！\r\n注意，联网帮助页面须指向一个帮助 JSON 文件，并在同路径下包含相应 XAML 文件！\r\n例如：\r\n - https://www.baidu.com/test.json（填写这个路径）\r\n - https://www.baidu.com/test.xaml（同时也需要包含这个文件）", innerException2);
				}
				RelativeUrl = text;
			}
			RelativeUrl = RelativeUrl.Replace("/", "\\").ToLower().TrimStart(new char[]
			{
				'\\'
			});
			string text2 = ModBase.Path + "PCL";
			ModMain.HelpTryExtract();
			string text3;
			if (RelativeUrl.Contains(":\\"))
			{
				text3 = RelativeUrl;
				ModBase.Log("[Control] 自定义事件中由绝对路径" + EventType + "：" + text3, ModBase.LogLevel.Normal, "出现错误");
			}
			else if (File.Exists(ModBase.Path + "PCL\\" + RelativeUrl))
			{
				text3 = ModBase.Path + "PCL\\" + RelativeUrl;
				ModBase.Log("[Control] 自定义事件中由相对 PCL 文件夹的路径" + EventType + "：" + text3, ModBase.LogLevel.Normal, "出现错误");
			}
			else if (File.Exists(ModBase.Path + "PCL\\Help\\" + RelativeUrl))
			{
				text3 = ModBase.Path + "PCL\\Help\\" + RelativeUrl;
				text2 = ModBase.Path + "PCL\\Help\\";
				ModBase.Log("[Control] 自定义事件中由相对 PCL 本地帮助文件夹的路径" + EventType + "：" + text3, ModBase.LogLevel.Normal, "出现错误");
			}
			else if (Operators.CompareString(EventType, "打开帮助", false) == 0 && File.Exists(ModBase.m_DecoratorRepository + "Help\\" + RelativeUrl))
			{
				text3 = ModBase.m_DecoratorRepository + "Help\\" + RelativeUrl;
				text2 = ModBase.m_DecoratorRepository + "Help\\";
				ModBase.Log("[Control] 自定义事件中由相对 PCL 自带帮助文件夹的路径" + EventType + "：" + text3, ModBase.LogLevel.Normal, "出现错误");
			}
			else
			{
				if (Operators.CompareString(EventType, "打开文件", false) != 0)
				{
					throw new FileNotFoundException("未找到 EventData 指向的本地 xaml 文件：" + RelativeUrl, RelativeUrl);
				}
				text3 = RelativeUrl;
				ModBase.Log("[Control] 自定义事件中直接" + EventType + "：" + text3, ModBase.LogLevel.Normal, "出现错误");
			}
			return new string[]
			{
				text3,
				text2
			};
		}
	}
}
