using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
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
	// Token: 0x020000C8 RID: 200
	[StandardModule]
	public sealed class ModDownloadLib
	{
		// Token: 0x060005EC RID: 1516 RVA: 0x00034364 File Offset: 0x00032564
		public static ModLoader.LoaderCombo<string> McDownloadClient(ModNet.NetPreDownloadBehaviour Behaviour, string Id, string JsonUrl = null)
		{
			ModLoader.LoaderCombo<string> result;
			try
			{
				string text = ModMinecraft.m_ProxyTests + "versions\\" + Id + "\\";
				try
				{
					foreach (ModLoader.LoaderBase loaderBase in Enumerable.ToList<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar))
					{
						if (Operators.CompareString(loaderBase.Name, string.Format("Minecraft {0} 下载", Id), false) == 0)
						{
							if (Behaviour == ModNet.NetPreDownloadBehaviour.ExitWhileExistsOrDownloading)
							{
								return (ModLoader.LoaderCombo<string>)loaderBase;
							}
							ModMain.Hint("该版本正在下载中！", ModMain.HintType.Critical, true);
							return (ModLoader.LoaderCombo<string>)loaderBase;
						}
					}
				}
				finally
				{
					List<ModLoader.LoaderBase>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				if (Behaviour != ModNet.NetPreDownloadBehaviour.IgnoreCheck && File.Exists(text + Id + ".json") && File.Exists(text + Id + ".jar"))
				{
					if (Behaviour == ModNet.NetPreDownloadBehaviour.ExitWhileExistsOrDownloading)
					{
						return null;
					}
					if (ModMain.MyMsgBox("版本 " + Id + " 已存在，是否重新下载？\r\n这会覆盖版本的 json 与 jar 文件，但不会影响版本隔离的文件。", "版本已存在", "继续", "取消", "", false, true, false, null, null, null) != 1)
					{
						return null;
					}
					File.Delete(text + Id + ".jar");
					File.Delete(text + Id + ".json");
				}
				ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("Minecraft " + Id + " 下载", ModDownloadLib.McDownloadClientLoader(Id, JsonUrl, null));
				loaderCombo.OnStateChanged = new Action<ModLoader.LoaderBase>(ModDownloadLib.McInstallState);
				loaderCombo.Start(text, false);
				ModLoader.LoaderTaskbarAdd<string>(loaderCombo);
				ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
				ModMain._ProcessIterator.BtnExtraDownload.Ribble();
				result = loaderCombo;
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "开始 Minecraft 下载失败", ModBase.LogLevel.Feedback, "出现错误");
				result = null;
			}
			return result;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00034548 File Offset: 0x00032748
		private static List<ModLoader.LoaderBase> McDownloadClientLoader(string Id, string JsonUrl = null, string VersionName = null)
		{
			VersionName = (VersionName ?? Id);
			string VersionFolder = ModMinecraft.m_ProxyTests + "versions\\" + VersionName + "\\";
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			if (JsonUrl == null)
			{
				list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("获取原版 json 文件下载地址", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
				{
					string original = Conversions.ToString(ModDownload.DlClientListGet(Id));
					Task.Output = new List<ModNet.NetFile>
					{
						new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(original), VersionFolder + VersionName + ".json", null, false)
					};
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = 2.0,
					Show = false
				});
			}
			list.Add(new ModNet.LoaderDownload("下载原版 json 文件", new List<ModNet.NetFile>
			{
				new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(JsonUrl ?? ""), VersionFolder + VersionName + ".json", new ModBase.FileChecker(-1L, -1L, null, false, true), false)
			})
			{
				ProgressWeight = 3.0
			});
			list.Add(new ModLoader.LoaderCombo<string>("下载原版支持库文件", new List<ModLoader.LoaderBase>
			{
				new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析原版支持库文件（副加载器）", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
				{
					ModBase.Log("[Download] 开始分析原版支持库文件：" + VersionFolder, ModBase.LogLevel.Normal, "出现错误");
					Task.Output = ModMinecraft.McLibFix(new ModMinecraft.McVersion(VersionFolder));
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = 1.0,
					Show = false
				},
				new ModNet.LoaderDownload("下载原版支持库文件（副加载器）", new List<ModNet.NetFile>())
				{
					ProgressWeight = 13.0,
					Show = false
				}
			})
			{
				Block = false,
				ProgressWeight = 14.0
			});
			list.Add(new ModLoader.LoaderCombo<string>("下载原版资源文件", new List<ModLoader.LoaderBase>
			{
				new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析资源文件索引地址（副加载器）", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
				{
					try
					{
						ModMinecraft.McVersion version = new ModMinecraft.McVersion(VersionFolder);
						Task.Output = new List<ModNet.NetFile>
						{
							ModDownload.DlClientAssetIndexGet(version)
						};
					}
					catch (Exception innerException)
					{
						throw new Exception("分析资源文件索引地址失败", innerException);
					}
					try
					{
						JObject jobject = (JObject)ModBase.GetJson(ModBase.ReadFile(VersionFolder + VersionName + ".json", null));
						jobject.Add("clientVersion", Id);
						ModBase.WriteFile(VersionFolder + VersionName + ".json", jobject.ToString(), false, null);
					}
					catch (Exception innerException2)
					{
						throw new Exception("添加客户端版本失败", innerException2);
					}
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = 1.0,
					Show = false
				},
				new ModNet.LoaderDownload("下载资源文件索引（副加载器）", new List<ModNet.NetFile>())
				{
					ProgressWeight = 3.0,
					Show = false
				},
				new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析所需资源文件（副加载器）", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
				{
					ModLoader.LoaderTask<string, List<ModNet.NetFile>> loaderTask = Task;
					ModMinecraft.McVersion version = new ModMinecraft.McVersion(VersionFolder);
					bool checkHash = true;
					ModLoader.LoaderBase loaderBase = Task;
					List<ModNet.NetFile> output = ModMinecraft.McAssetsFixList(version, checkHash, ref loaderBase);
					Task = (ModLoader.LoaderTask<string, List<ModNet.NetFile>>)loaderBase;
					loaderTask.Output = output;
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = 3.0,
					Show = false
				},
				new ModNet.LoaderDownload("下载资源文件（副加载器）", new List<ModNet.NetFile>())
				{
					ProgressWeight = 14.0,
					Show = false
				}
			})
			{
				Block = false,
				ProgressWeight = 21.0
			});
			return list;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000347C4 File Offset: 0x000329C4
		public static MyListItem McDownloadListItem(JObject Entry, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
		{
			JToken jtoken = Entry["type"];
			string logo;
			if ((string)jtoken == "release")
			{
				logo = ModBase.m_SerializerRepository + "Blocks/Grass.png";
			}
			else if ((string)jtoken == "snapshot")
			{
				logo = ModBase.m_SerializerRepository + "Blocks/CommandBlock.png";
			}
			else if ((string)jtoken == "special")
			{
				logo = ModBase.m_SerializerRepository + "Blocks/GoldBlock.png";
			}
			else
			{
				logo = ModBase.m_SerializerRepository + "Blocks/CobbleStone.png";
			}
			MyListItem myListItem = new MyListItem
			{
				Logo = logo,
				SnapsToDevicePixels = true,
				Title = Entry["id"].ToString(),
				Height = 42.0,
				Type = MyListItem.CheckType.Clickable,
				Tag = Entry
			};
			if (Entry["lore"] == null)
			{
				myListItem.Info = Extensions.Value<DateTime>(Entry["releaseTime"]).ToString("yyyy'/'MM'/'dd HH':'mm");
			}
			else
			{
				myListItem.Info = Entry["lore"].ToString();
			}
			if (Entry["url"].ToString().Contains("pcl"))
			{
				myListItem.Info = "[PCL 特供下载] " + myListItem.Info;
			}
			myListItem.Click += OnClick;
			if (IsSaveOnly)
			{
				myListItem.tokenComposer = new Action<MyListItem, EventArgs>(ModDownloadLib.McDownloadSaveMenuBuild);
			}
			else
			{
				myListItem.tokenComposer = new Action<MyListItem, EventArgs>(ModDownloadLib.McDownloadMenuBuild);
			}
			return myListItem;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0003494C File Offset: 0x00032B4C
		private static void McDownloadSaveMenuBuild(object sender, EventArgs e)
		{
			MyIconButton myIconButton = new MyIconButton
			{
				LogoScale = 1.05,
				Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
				ToolTip = "更新日志"
			};
			ToolTipService.SetPlacement(myIconButton, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton, 2.0);
			myIconButton.Click += ((ModDownloadLib._Closure$__.$IR9-1 == null) ? (ModDownloadLib._Closure$__.$IR9-1 = delegate(object sender, EventArgs e)
			{
				ModDownloadLib.McDownloadMenuLog(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
			}) : ModDownloadLib._Closure$__.$IR9-1);
			MyIconButton myIconButton2 = new MyIconButton
			{
				LogoScale = 1.0,
				Logo = "M224 160a64 64 0 0 0-64 64v576a64 64 0 0 0 64 64h576a64 64 0 0 0 64-64V224a64 64 0 0 0-64-64H224z m0 384h576v256H224v-256z m192 96v64h320v-64H416z m-128 0v64h64v-64H288zM224 224h576v256H224V224z m192 96v64h320v-64H416z m-128 0v64h64v-64H288z",
				ToolTip = "下载服务端"
			};
			ToolTipService.SetPlacement(myIconButton2, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton2, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton2, 2.0);
			myIconButton2.Click += ((ModDownloadLib._Closure$__.$IR9-2 == null) ? (ModDownloadLib._Closure$__.$IR9-2 = delegate(object sender, EventArgs e)
			{
				ModDownloadLib.McDownloadMenuSaveServer(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
			}) : ModDownloadLib._Closure$__.$IR9-2);
			NewLateBinding.LateSet(sender2, null, "Buttons", new object[]
			{
				new MyIconButton[]
				{
					myIconButton2,
					myIconButton
				}
			}, null, null);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00034A74 File Offset: 0x00032C74
		private static void McDownloadMenuBuild(object sender, EventArgs e)
		{
			MyIconButton myIconButton = new MyIconButton
			{
				Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
				ToolTip = "另存为"
			};
			ToolTipService.SetPlacement(myIconButton, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton, 2.0);
			myIconButton.Click += ((ModDownloadLib._Closure$__.$IR10-3 == null) ? (ModDownloadLib._Closure$__.$IR10-3 = delegate(object sender, EventArgs e)
			{
				ModDownloadLib.McDownloadMenuSave(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
			}) : ModDownloadLib._Closure$__.$IR10-3);
			MyIconButton myIconButton2 = new MyIconButton
			{
				LogoScale = 1.05,
				Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
				ToolTip = "更新日志"
			};
			ToolTipService.SetPlacement(myIconButton2, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton2, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton2, 2.0);
			myIconButton2.Click += ((ModDownloadLib._Closure$__.$IR10-4 == null) ? (ModDownloadLib._Closure$__.$IR10-4 = delegate(object sender, EventArgs e)
			{
				ModDownloadLib.McDownloadMenuLog(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
			}) : ModDownloadLib._Closure$__.$IR10-4);
			MyIconButton myIconButton3 = new MyIconButton
			{
				LogoScale = 1.0,
				Logo = "M224 160a64 64 0 0 0-64 64v576a64 64 0 0 0 64 64h576a64 64 0 0 0 64-64V224a64 64 0 0 0-64-64H224z m0 384h576v256H224v-256z m192 96v64h320v-64H416z m-128 0v64h64v-64H288zM224 224h576v256H224V224z m192 96v64h320v-64H416z m-128 0v64h64v-64H288z",
				ToolTip = "下载服务端"
			};
			ToolTipService.SetPlacement(myIconButton3, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton3, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton3, 2.0);
			myIconButton3.Click += ((ModDownloadLib._Closure$__.$IR10-5 == null) ? (ModDownloadLib._Closure$__.$IR10-5 = delegate(object sender, EventArgs e)
			{
				ModDownloadLib.McDownloadMenuSaveServer(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
			}) : ModDownloadLib._Closure$__.$IR10-5);
			NewLateBinding.LateSet(sender2, null, "Buttons", new object[]
			{
				new MyIconButton[]
				{
					myIconButton,
					myIconButton2,
					myIconButton3
				}
			}, null, null);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00034C0C File Offset: 0x00032E0C
		private static void McDownloadMenuLog(object sender, RoutedEventArgs e)
		{
			JToken versionJson;
			if (NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null)
			{
				versionJson = (JToken)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
			}
			else if (NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) != null)
			{
				versionJson = (JToken)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
			}
			else
			{
				versionJson = (JToken)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
			}
			ModDownloadLib.McUpdateLogShow(versionJson);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00034CF8 File Offset: 0x00032EF8
		private static void McDownloadMenuSaveServer(MyListItem sender, RoutedEventArgs e)
		{
			MyListItem myListItem;
			if (sender is MyListItem)
			{
				myListItem = (MyListItem)sender;
			}
			else if (NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null) is MyListItem)
			{
				myListItem = (MyListItem)NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null);
			}
			else
			{
				myListItem = (MyListItem)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null);
			}
			try
			{
				ModDownloadLib._Closure$__12-0 CS$<>8__locals1 = new ModDownloadLib._Closure$__12-0(CS$<>8__locals1);
				CS$<>8__locals1.$VB$Local_Id = myListItem.Title;
				string original = NewLateBinding.LateIndexGet(myListItem.Tag, new object[]
				{
					"url"
				}, null).ToString();
				CS$<>8__locals1.$VB$Local_VersionFolder = ModBase.SelectFolder("选择文件夹");
				if (CS$<>8__locals1.$VB$Local_VersionFolder.Contains("\\"))
				{
					CS$<>8__locals1.$VB$Local_VersionFolder = CS$<>8__locals1.$VB$Local_VersionFolder + CS$<>8__locals1.$VB$Local_Id + "\\";
					try
					{
						List<ModLoader.LoaderBase>.Enumerator enumerator = Enumerable.ToList<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar).GetEnumerator();
						while (enumerator.MoveNext())
						{
							if (Operators.CompareString(enumerator.Current.Name, string.Format("Minecraft {0} 服务端下载", CS$<>8__locals1.$VB$Local_Id), false) == 0)
							{
								ModMain.Hint("该服务端正在下载中！", ModMain.HintType.Critical, true);
								return;
							}
						}
					}
					finally
					{
						List<ModLoader.LoaderBase>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
					list.Add(new ModNet.LoaderDownload("下载版本 JSON 文件", new List<ModNet.NetFile>
					{
						new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(original), CS$<>8__locals1.$VB$Local_VersionFolder + CS$<>8__locals1.$VB$Local_Id + ".json", new ModBase.FileChecker(-1L, -1L, null, false, true), false)
					})
					{
						ProgressWeight = 2.0
					});
					list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("构建服务端", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
					{
						ModMinecraft.McVersion mcVersion = new ModMinecraft.McVersion(CS$<>8__locals1.$VB$Local_VersionFolder);
						if (mcVersion.NewThread()["downloads"] != null && mcVersion.NewThread()["downloads"]["server"] != null && mcVersion.NewThread()["downloads"]["server"]["url"] != null)
						{
							string original2 = (string)mcVersion.NewThread()["downloads"]["server"]["url"];
							ModBase.FileChecker check = new ModBase.FileChecker(1024L, (long)(mcVersion.NewThread()["downloads"]["server"]["size"] ?? -1), (string)mcVersion.NewThread()["downloads"]["server"]["sha1"], true, false);
							Task.Output = new List<ModNet.NetFile>
							{
								new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(original2), CS$<>8__locals1.$VB$Local_VersionFolder + CS$<>8__locals1.$VB$Local_Id + "-server.jar", check, false)
							};
							string text = string.Format("@echo off\r\ntitle {0} 原版服务端\r\necho 如果服务端立即停止，请右键编辑该脚本，将下一行开头的 java 替换为适合该 Minecraft 版本的完整 java.exe 的路径。\r\necho 你可以在 PCL 的 [设置 → 启动选项] 中查看已安装的 java，所需的 java.exe 一般在其中的 bin 文件夹下。\r\necho ------------------------------\r\necho 如果提示 \"You need to agree to the EULA in order to run the server\"，请打开 eula.txt，按说明阅读并同意 Minecraft EULA 后，将该文件最后一行中的 eula=false 改为 eula=true。\r\necho ------------------------------\r\n\"java\" -server -XX:+UseG1GC -Xmx4096M -Xms1024M -XX:+UseCompressedOops -jar {1}-server.jar nogui\r\necho ----------------------\r\necho 服务端已停止。\r\npause", CS$<>8__locals1.$VB$Local_Id, CS$<>8__locals1.$VB$Local_Id);
							ModBase.WriteFile(CS$<>8__locals1.$VB$Local_VersionFolder + "Launch Server.bat", text, false, Encoding.Default.Equals(Encoding.UTF8) ? Encoding.UTF8 : Encoding.GetEncoding("GB18030"));
							File.Delete(CS$<>8__locals1.$VB$Local_VersionFolder + CS$<>8__locals1.$VB$Local_Id + ".json");
							return;
						}
						File.Delete(CS$<>8__locals1.$VB$Local_VersionFolder + CS$<>8__locals1.$VB$Local_Id + ".json");
						if (!Enumerable.Any<FileSystemInfo>(new DirectoryInfo(CS$<>8__locals1.$VB$Local_VersionFolder).GetFileSystemInfos()))
						{
							Directory.Delete(CS$<>8__locals1.$VB$Local_VersionFolder);
						}
						Task.Output = new List<ModNet.NetFile>();
						ModMain.Hint(string.Format("Mojang 没有给 Minecraft {0} 提供官方服务端下载，没法下，撤退！", CS$<>8__locals1.$VB$Local_Id), ModMain.HintType.Critical, true);
						Thread.Sleep(2000);
						Task.Abort();
					}, null, ThreadPriority.Normal)
					{
						ProgressWeight = 0.5,
						Show = false
					});
					list.Add(new ModNet.LoaderDownload("下载服务端文件", new List<ModNet.NetFile>())
					{
						ProgressWeight = 5.0
					});
					ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("Minecraft " + CS$<>8__locals1.$VB$Local_Id + " 服务端下载", list);
					loaderCombo.OnStateChanged = new Action<ModLoader.LoaderBase>(ModDownloadLib.DownloadStateSave);
					loaderCombo.Start(CS$<>8__locals1.$VB$Local_Id, false);
					ModLoader.LoaderTaskbarAdd<string>(loaderCombo);
					ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
					ModMain._ProcessIterator.BtnExtraDownload.Ribble();
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "开始 Minecraft 服务端下载失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00034FD0 File Offset: 0x000331D0
		public static void McDownloadMenuSave(MyListItem sender, RoutedEventArgs e)
		{
			MyListItem myListItem;
			if (sender is MyListItem)
			{
				myListItem = (MyListItem)sender;
			}
			else if (NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null) is MyListItem)
			{
				myListItem = (MyListItem)NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null);
			}
			else
			{
				myListItem = (MyListItem)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null);
			}
			try
			{
				ModDownloadLib._Closure$__13-0 CS$<>8__locals1 = new ModDownloadLib._Closure$__13-0(CS$<>8__locals1);
				string title = myListItem.Title;
				string original = NewLateBinding.LateIndexGet(myListItem.Tag, new object[]
				{
					"url"
				}, null).ToString();
				CS$<>8__locals1.$VB$Local_VersionFolder = ModBase.SelectFolder("选择文件夹");
				if (CS$<>8__locals1.$VB$Local_VersionFolder.Contains("\\"))
				{
					CS$<>8__locals1.$VB$Local_VersionFolder = CS$<>8__locals1.$VB$Local_VersionFolder + title + "\\";
					try
					{
						List<ModLoader.LoaderBase>.Enumerator enumerator = Enumerable.ToList<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar).GetEnumerator();
						while (enumerator.MoveNext())
						{
							if (Operators.CompareString(enumerator.Current.Name, string.Format("Minecraft {0} 下载", title), false) == 0)
							{
								ModMain.Hint("该版本正在下载中！", ModMain.HintType.Critical, true);
								return;
							}
						}
					}
					finally
					{
						List<ModLoader.LoaderBase>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
					list.Add(new ModNet.LoaderDownload("下载版本 JSON 文件", new List<ModNet.NetFile>
					{
						new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(original), CS$<>8__locals1.$VB$Local_VersionFolder + title + ".json", new ModBase.FileChecker(-1L, -1L, null, false, true), false)
					})
					{
						ProgressWeight = 2.0
					});
					list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析核心 JAR 文件下载地址", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
					{
						Task.Output = new List<ModNet.NetFile>
						{
							ModDownload.DlClientJarGet(new ModMinecraft.McVersion(CS$<>8__locals1.$VB$Local_VersionFolder), false)
						};
					}, null, ThreadPriority.Normal)
					{
						ProgressWeight = 0.5,
						Show = false
					});
					list.Add(new ModNet.LoaderDownload("下载核心 JAR 文件", new List<ModNet.NetFile>())
					{
						ProgressWeight = 5.0
					});
					ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("Minecraft " + title + " 下载", list);
					loaderCombo.OnStateChanged = new Action<ModLoader.LoaderBase>(ModDownloadLib.DownloadStateSave);
					loaderCombo.Start(title, false);
					ModLoader.LoaderTaskbarAdd<string>(loaderCombo);
					ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
					ModMain._ProcessIterator.BtnExtraDownload.Ribble();
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "开始 Minecraft 下载失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00035290 File Offset: 0x00033490
		public static void McUpdateLogShow(JToken VersionJson)
		{
			string text = VersionJson["id"].ToString().ToLower();
			string text2;
			if (Operators.CompareString(text, "3d shareware v1.34", false) == 0)
			{
				text2 = "3D_Shareware_v1.34";
			}
			else if (Operators.CompareString(text, "2.0", false) == 0)
			{
				text2 = "Java版2.0";
			}
			else if (Operators.CompareString(text, "1.rv-pre1", false) == 0)
			{
				text2 = "Java版1.RV-Pre1";
			}
			else if (Operators.CompareString(text, "combat test 1", false) != 0 && !text.Contains("combat-1") && !text.Contains("combat-212796"))
			{
				if (Operators.CompareString(text, "combat test 2", false) != 0 && !text.Contains("combat-2") && !text.Contains("combat-0"))
				{
					if (Operators.CompareString(text, "combat test 3", false) != 0 && Operators.CompareString(text, "1.14_combat-3", false) != 0)
					{
						if (Operators.CompareString(text, "combat test 4", false) != 0 && Operators.CompareString(text, "1.15_combat-1", false) != 0)
						{
							if (Operators.CompareString(text, "combat test 5", false) != 0 && Operators.CompareString(text, "1.15_combat-6", false) != 0)
							{
								if (Operators.CompareString(text, "combat test 6", false) != 0 && Operators.CompareString(text, "1.16_combat-0", false) != 0)
								{
									if (Operators.CompareString(text, "combat test 7c", false) != 0 && Operators.CompareString(text, "1.16_combat-3", false) != 0)
									{
										if (Operators.CompareString(text, "combat test 8b", false) != 0 && Operators.CompareString(text, "1.16_combat-5", false) != 0)
										{
											if (Operators.CompareString(text, "combat test 8c", false) != 0 && Operators.CompareString(text, "1.16_combat-6", false) != 0)
											{
												if (Operators.CompareString(text, "1.0.0-rc2-2", false) == 0)
												{
													text2 = "Java版RC2";
												}
												else if (!text.StartsWithF("1.19_deep_dark_experimental_snapshot-", false) && !text.StartsWithF("1_19_deep_dark_experimental_snapshot-", false))
												{
													if (Operators.CompareString(text, "b1.9-pre6", false) == 0)
													{
														text2 = "Java版Beta_1.9_Prerelease_6";
													}
													else if (text.Contains("b1.9"))
													{
														text2 = "Java版Beta_1.9_Prerelease";
													}
													else if (!((string)VersionJson["type"] == "release") && !((string)VersionJson["type"] == "snapshot") && !((string)VersionJson["type"] == "special"))
													{
														if (text.StartsWithF("b", false))
														{
															text2 = "Java版" + text.TrimEnd(new char[]
															{
																'a',
																'b',
																'c',
																'd',
																'e'
															}).Replace("b", "Beta_");
														}
														else if (text.StartsWithF("a", false))
														{
															text2 = "Java版" + text.TrimEnd(new char[]
															{
																'a',
																'b',
																'c',
																'd',
																'e'
															}).Replace("a", "Alpha_v");
														}
														else if (Operators.CompareString(text, "inf-20100618", false) == 0)
														{
															text2 = "Java版Infdev_20100618";
														}
														else if (Operators.CompareString(text, "c0.30_01c", false) != 0 && Operators.CompareString(text, "c0.30_survival", false) != 0 && !text.Contains("生存测试"))
														{
															if (text.StartsWithF("c0.31", false))
															{
																text2 = "Java版Indev_0.31_20100130";
															}
															else if (text.StartsWithF("c", false))
															{
																text2 = "Java版" + text.Replace("c", "Classic_");
															}
															else
															{
																if (!text.StartsWithF("rd-", false))
																{
																	ModBase.Log("[Error] 未知的版本格式：" + text + "。", ModBase.LogLevel.Feedback, "出现错误");
																	return;
																}
																text2 = "Java版Pre-classic_" + text;
															}
														}
														else
														{
															text2 = "Java版Classic_0.30（生存模式）";
														}
													}
													else
													{
														text2 = (text.Contains("w") ? "" : "Java版") + text.Replace(" Pre-Release ", "-pre");
													}
												}
												else
												{
													text2 = text.Replace("1_19", "1.19").Replace("1.19_deep_dark_experimental_snapshot-", "Java版Deep_Dark_Experimental_Snapshot_");
												}
											}
											else
											{
												text2 = "Java版Combat_Test_8c";
											}
										}
										else
										{
											text2 = "Java版Combat_Test_8b";
										}
									}
									else
									{
										text2 = "Java版Combat_Test_7c";
									}
								}
								else
								{
									text2 = "Java版Combat_Test_6";
								}
							}
							else
							{
								text2 = "Java版Combat_Test_5";
							}
						}
						else
						{
							text2 = "Java版Combat_Test_4";
						}
					}
					else
					{
						text2 = "Java版Combat_Test_3";
					}
				}
				else
				{
					text2 = "Java版Combat_Test_2";
				}
			}
			else
			{
				text2 = "Java版1.14.3_-_Combat_Test";
			}
			ModBase.OpenWebsite("https://zh.minecraft.wiki/w/" + text2.Replace("_experimental-snapshot-", "-exp"));
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00035730 File Offset: 0x00033930
		private static void McDownloadOptiFineSave(ModDownload.DlOptiFineListEntry DownloadInfo)
		{
			try
			{
				string text = ModBase.SelectAs("选择保存位置", DownloadInfo._DescriptorTest, "OptiFine Jar (*.jar)|*.jar", null);
				if (text.Contains("\\"))
				{
					try
					{
						List<ModLoader.LoaderBase>.Enumerator enumerator = Enumerable.ToList<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar).GetEnumerator();
						while (enumerator.MoveNext())
						{
							if (Operators.CompareString(enumerator.Current.Name, string.Format("OptiFine {0} 下载", DownloadInfo.m_SchemaTest), false) == 0)
							{
								ModMain.Hint("该版本正在下载中！", ModMain.HintType.Critical, true);
								return;
							}
						}
					}
					finally
					{
						List<ModLoader.LoaderBase>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					ModLoader.LoaderCombo<ModDownload.DlOptiFineListEntry> loaderCombo = new ModLoader.LoaderCombo<ModDownload.DlOptiFineListEntry>("OptiFine " + DownloadInfo.m_SchemaTest + " 下载", ModDownloadLib.McDownloadOptiFineSaveLoader(DownloadInfo, text));
					loaderCombo.OnStateChanged = new Action<ModLoader.LoaderBase>(ModDownloadLib.DownloadStateSave);
					loaderCombo.Start(DownloadInfo, false);
					ModLoader.LoaderTaskbarAdd<ModDownload.DlOptiFineListEntry>(loaderCombo);
					ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
					ModMain._ProcessIterator.BtnExtraDownload.Ribble();
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "开始 OptiFine 下载失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00035864 File Offset: 0x00033A64
		private static void McDownloadOptiFineInstall(string BaseMcFolderHome, string Target, ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task, bool UseJavaWrapper)
		{
			ModDownloadLib._Closure$__16-2 CS$<>8__locals1 = new ModDownloadLib._Closure$__16-2(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_Task = Task;
			object dispatcher = ModJava.m_Dispatcher;
			ObjectFlowControl.CheckForSyncLockOnValueType(dispatcher);
			ModJava.JavaEntry javaEntry;
			lock (dispatcher)
			{
				javaEntry = ModJava.JavaSelect("已取消安装。", new Version(1, 8, 0, 0), null, null);
				if (javaEntry == null)
				{
					if (!ModJava.JavaDownloadConfirm("Java 8 或更高版本", false))
					{
						throw new Exception("由于未找到 Java，已取消安装。");
					}
					ModLoader.LoaderCombo<int> loaderCombo = ModJava.JavaFixLoaders(17);
					try
					{
						loaderCombo.Start(17, true);
						while (loaderCombo.State == ModBase.LoadState.Loading && !CS$<>8__locals1.$VB$Local_Task.IsAborted)
						{
							Thread.Sleep(10);
						}
					}
					finally
					{
						loaderCombo.Abort();
					}
					javaEntry = ModJava.JavaSelect("已取消安装。", new Version(1, 8, 0, 0), null, null);
					if (CS$<>8__locals1.$VB$Local_Task.IsAborted)
					{
						return;
					}
					if (javaEntry == null)
					{
						throw new Exception("由于未找到 Java，已取消安装。");
					}
				}
			}
			string text;
			if (UseJavaWrapper)
			{
				text = string.Format("-Doolloo.jlw.tmpdir=\"{0}\" -Duser.home=\"{1}\" -cp \"{2}\" -jar \"{3}\" optifine.Installer", new object[]
				{
					ModBase._MethodRepository.TrimEnd(new char[]
					{
						'\\'
					}),
					BaseMcFolderHome,
					Target,
					ModLaunch.ExtractJavaWrapper()
				});
			}
			else
			{
				text = string.Format("-Duser.home=\"{0}\" -cp \"{1}\" optifine.Installer", BaseMcFolderHome, Target);
			}
			if (javaEntry.PostTests() >= 9)
			{
				text = "--add-exports cpw.mods.bootstraplauncher/cpw.mods.bootstraplauncher=ALL-UNNAMED " + text;
			}
			object statusField = ModDownloadLib._StatusField;
			ObjectFlowControl.CheckForSyncLockOnValueType(statusField);
			lock (statusField)
			{
				ModDownloadLib._Closure$__16-1 CS$<>8__locals2 = new ModDownloadLib._Closure$__16-1(CS$<>8__locals2);
				CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2 = CS$<>8__locals1;
				ProcessStartInfo processStartInfo = new ProcessStartInfo
				{
					FileName = javaEntry.PrintTests(),
					Arguments = text,
					UseShellExecute = false,
					CreateNoWindow = true,
					RedirectStandardError = true,
					RedirectStandardOutput = true,
					WorkingDirectory = BaseMcFolderHome
				};
				if (processStartInfo.EnvironmentVariables.ContainsKey("appdata"))
				{
					processStartInfo.EnvironmentVariables["appdata"] = BaseMcFolderHome;
				}
				else
				{
					processStartInfo.EnvironmentVariables.Add("appdata", BaseMcFolderHome);
				}
				ModBase.Log("[Download] 开始安装 OptiFine：" + Target, ModBase.LogLevel.Normal, "出现错误");
				CS$<>8__locals2.$VB$Local_TotalLength = 0;
				CS$<>8__locals2.$VB$Local_process = new Process
				{
					StartInfo = processStartInfo
				};
				CS$<>8__locals2.$VB$Local_LastResult = "";
				ModDownloadLib._Closure$__16-0 CS$<>8__locals3 = new ModDownloadLib._Closure$__16-0(CS$<>8__locals3);
				CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3 = CS$<>8__locals2;
				CS$<>8__locals3.$VB$Local_outputWaitHandle = new AutoResetEvent(false);
				try
				{
					ModDownloadLib._Closure$__16-3 CS$<>8__locals4 = new ModDownloadLib._Closure$__16-3(CS$<>8__locals4);
					CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4 = CS$<>8__locals3;
					CS$<>8__locals4.$VB$Local_errorWaitHandle = new AutoResetEvent(false);
					try
					{
						CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs e)
						{
							base._Lambda$__0(RuntimeHelpers.GetObjectValue(sender), e);
						};
						CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_process.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs e)
						{
							base._Lambda$__1(RuntimeHelpers.GetObjectValue(sender), e);
						};
						CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_process.Start();
						CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_process.BeginOutputReadLine();
						CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_process.BeginErrorReadLine();
						while (!CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_process.HasExited)
						{
							Thread.Sleep(10);
						}
						CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$Local_outputWaitHandle.WaitOne(10000);
						CS$<>8__locals4.$VB$Local_errorWaitHandle.WaitOne(10000);
						CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_process.Dispose();
						if (CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_TotalLength < 1000 || CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_LastResult.Contains("at "))
						{
							throw new Exception("安装器运行出错，末行为 " + CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_LastResult);
						}
					}
					finally
					{
						if (CS$<>8__locals4.$VB$Local_errorWaitHandle != null)
						{
							((IDisposable)CS$<>8__locals4.$VB$Local_errorWaitHandle).Dispose();
						}
					}
				}
				finally
				{
					if (CS$<>8__locals3.$VB$Local_outputWaitHandle != null)
					{
						((IDisposable)CS$<>8__locals3.$VB$Local_outputWaitHandle).Dispose();
					}
				}
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00035CCC File Offset: 0x00033ECC
		private static List<ModLoader.LoaderBase> McDownloadOptiFineLoader(ModDownload.DlOptiFineListEntry DownloadInfo, string McFolder = null, ModLoader.LoaderCombo<string> ClientDownloadLoader = null, string ClientFolder = null, bool FixLibrary = true)
		{
			ModDownloadLib._Closure$__17-0 CS$<>8__locals1 = new ModDownloadLib._Closure$__17-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_DownloadInfo = DownloadInfo;
			CS$<>8__locals1.$VB$Local_McFolder = McFolder;
			CS$<>8__locals1.$VB$Local_ClientDownloadLoader = ClientDownloadLoader;
			CS$<>8__locals1.$VB$Local_ClientFolder = ClientFolder;
			CS$<>8__locals1.$VB$Local_McFolder = (CS$<>8__locals1.$VB$Local_McFolder ?? ModMinecraft.m_ProxyTests);
			CS$<>8__locals1.$VB$Local_IsCustomFolder = (Operators.CompareString(CS$<>8__locals1.$VB$Local_McFolder, ModMinecraft.m_ProxyTests, false) != 0);
			CS$<>8__locals1.$VB$Local_Id = CS$<>8__locals1.$VB$Local_DownloadInfo.m_PublisherTest;
			CS$<>8__locals1.$VB$Local_VersionFolder = CS$<>8__locals1.$VB$Local_McFolder + "versions\\" + CS$<>8__locals1.$VB$Local_Id + "\\";
			bool flag = CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper().Contains("w") || ModBase.Val(CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper().Split(".")[1]) >= 14.0;
			CS$<>8__locals1.$VB$Local_Target = (flag ? string.Concat(new string[]
			{
				ModBase.m_DecoratorRepository,
				"Cache\\Code\\",
				CS$<>8__locals1.$VB$Local_DownloadInfo.m_PublisherTest,
				"_",
				Conversions.ToString(ModBase.GetUuid())
			}) : string.Concat(new string[]
			{
				CS$<>8__locals1.$VB$Local_McFolder,
				"libraries\\optifine\\OptiFine\\",
				CS$<>8__locals1.$VB$Local_DownloadInfo._DescriptorTest.Replace("OptiFine_", "").Replace(".jar", "").Replace("preview_", ""),
				"\\",
				CS$<>8__locals1.$VB$Local_DownloadInfo._DescriptorTest.Replace("OptiFine_", "OptiFine-").Replace("preview_", "")
			}));
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("获取 OptiFine 主文件下载地址", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
			{
				if (CS$<>8__locals1.$VB$Local_ClientDownloadLoader == null)
				{
					if (CS$<>8__locals1.$VB$Local_IsCustomFolder)
					{
						throw new Exception("如果没有指定原版下载器，则不能指定 MC 安装文件夹");
					}
					CS$<>8__locals1.$VB$Local_ClientDownloadLoader = ModDownloadLib.McDownloadClient(ModNet.NetPreDownloadBehaviour.ExitWhileExistsOrDownloading, CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(), null);
				}
				Task.Progress = 0.1;
				List<string> list2 = new List<string>();
				string text = CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper();
				if (Operators.CompareString(text, "1.8", false) == 0 || Operators.CompareString(text, "1.9", false) == 0)
				{
					text += ".0";
				}
				if (CS$<>8__locals1.$VB$Local_DownloadInfo._DefinitionTest)
				{
					list2.Add("https://bmclapi2.bangbang93.com/optifine/" + text + "/HD_U_" + CS$<>8__locals1.$VB$Local_DownloadInfo.m_SchemaTest.Replace(CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper() + " ", "").Replace(" ", "/"));
				}
				else
				{
					list2.Add("https://bmclapi2.bangbang93.com/optifine/" + text + "/HD_U/" + CS$<>8__locals1.$VB$Local_DownloadInfo.m_SchemaTest.Replace(CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper() + " ", ""));
				}
				try
				{
					string str = ModNet.NetGetCodeByClient("https://optifine.net/adloadx?f=" + CS$<>8__locals1.$VB$Local_DownloadInfo._DescriptorTest, new UTF8Encoding(false), 15000, "text/html", true);
					Task.Progress = 0.8;
					list2.Add("https://optifine.net/" + ModBase.RegexSearch(str, "downloadx\\?f=[^\"']+", 0)[0]);
					ModBase.Log("[Download] OptiFine " + CS$<>8__locals1.$VB$Local_DownloadInfo.m_SchemaTest + " 官方下载地址：" + Enumerable.Last<string>(list2), ModBase.LogLevel.Normal, "出现错误");
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "获取 OptiFine " + CS$<>8__locals1.$VB$Local_DownloadInfo.m_SchemaTest + " 官方下载地址失败", ModBase.LogLevel.Debug, "出现错误");
				}
				Task.Output = new List<ModNet.NetFile>
				{
					new ModNet.NetFile(list2.ToArray(), CS$<>8__locals1.$VB$Local_Target, new ModBase.FileChecker(307200L, -1L, null, true, false), false)
				};
			}, null, ThreadPriority.Normal)
			{
				ProgressWeight = 8.0
			});
			list.Add(new ModNet.LoaderDownload("下载 OptiFine 主文件", new List<ModNet.NetFile>())
			{
				ProgressWeight = 8.0
			});
			list.Add(new ModLoader.LoaderTask<List<ModNet.NetFile>, bool>("等待原版下载", delegate(ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task)
			{
				if (CS$<>8__locals1.$VB$Local_ClientDownloadLoader != null)
				{
					List<ModLoader.LoaderBase> list2 = Enumerable.ToList<ModLoader.LoaderBase>(Enumerable.Where<ModLoader.LoaderBase>(Enumerable.Where<ModLoader.LoaderBase>(CS$<>8__locals1.$VB$Local_ClientDownloadLoader.GetLoaderList(true), (ModDownloadLib._Closure$__.$I17-2 == null) ? (ModDownloadLib._Closure$__.$I17-2 = ((ModLoader.LoaderBase l) => Operators.CompareString(l.Name, "下载原版支持库文件", false) == 0 || Operators.CompareString(l.Name, "下载原版 json 文件", false) == 0)) : ModDownloadLib._Closure$__.$I17-2), (ModDownloadLib._Closure$__.$I17-3 == null) ? (ModDownloadLib._Closure$__.$I17-3 = ((ModLoader.LoaderBase l) => l.State != ModBase.LoadState.Finished)) : ModDownloadLib._Closure$__.$I17-3));
					if (Enumerable.Any<ModLoader.LoaderBase>(list2))
					{
						ModBase.Log("[Download] OptiFine 安装正在等待原版文件下载完成", ModBase.LogLevel.Normal, "出现错误");
					}
					while (Enumerable.Any<ModLoader.LoaderBase>(list2) && !Task.IsAborted)
					{
						list2 = Enumerable.ToList<ModLoader.LoaderBase>(Enumerable.Where<ModLoader.LoaderBase>(list2, (ModDownloadLib._Closure$__.$I17-4 == null) ? (ModDownloadLib._Closure$__.$I17-4 = ((ModLoader.LoaderBase l) => l.State != ModBase.LoadState.Finished)) : ModDownloadLib._Closure$__.$I17-4));
						Thread.Sleep(50);
					}
					if (!Task.IsAborted && CS$<>8__locals1.$VB$Local_IsCustomFolder)
					{
						object obj = ModDownloadLib.roleField;
						ObjectFlowControl.CheckForSyncLockOnValueType(obj);
						lock (obj)
						{
							string folderNameFromPath = ModBase.GetFolderNameFromPath(CS$<>8__locals1.$VB$Local_ClientFolder);
							Directory.CreateDirectory(CS$<>8__locals1.$VB$Local_McFolder + "versions\\" + CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper());
							if (!File.Exists(string.Concat(new string[]
							{
								CS$<>8__locals1.$VB$Local_McFolder,
								"versions\\",
								CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(),
								"\\",
								CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(),
								".json"
							})))
							{
								ModBase.CopyFile(string.Format("{0}{1}.json", CS$<>8__locals1.$VB$Local_ClientFolder, folderNameFromPath), string.Format("{0}versions\\{1}\\{2}.json", CS$<>8__locals1.$VB$Local_McFolder, CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(), CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper()));
							}
							if (!File.Exists(string.Concat(new string[]
							{
								CS$<>8__locals1.$VB$Local_McFolder,
								"versions\\",
								CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(),
								"\\",
								CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(),
								".jar"
							})))
							{
								ModBase.CopyFile(string.Format("{0}{1}.jar", CS$<>8__locals1.$VB$Local_ClientFolder, folderNameFromPath), string.Format("{0}versions\\{1}\\{2}.jar", CS$<>8__locals1.$VB$Local_McFolder, CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(), CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper()));
							}
						}
					}
				}
			}, null, ThreadPriority.Normal)
			{
				ProgressWeight = 0.1,
				Show = false
			});
			if (flag)
			{
				ModBase.Log("[Download] 检测为新版 OptiFine：" + CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(), ModBase.LogLevel.Normal, "出现错误");
				list.Add(new ModLoader.LoaderTask<List<ModNet.NetFile>, bool>("安装 OptiFine（方式 A）", delegate(ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task)
				{
					string text = ModBase.m_DecoratorRepository + "InstallOptiFine" + Conversions.ToString(ModBase.RandomInteger(0, 100000));
					string text2 = text + "\\.minecraft\\";
					try
					{
						if (Directory.Exists(text2 + "versions\\" + CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper()))
						{
							ModBase.DeleteDirectory(text2 + "versions\\" + CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(), false);
						}
						Directory.CreateDirectory(text2 + "versions\\" + CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper() + "\\");
						ModMinecraft.McFolderLauncherProfilesJsonCreate(text2);
						ModBase.CopyFile(string.Concat(new string[]
						{
							CS$<>8__locals1.$VB$Local_McFolder,
							"versions\\",
							CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(),
							"\\",
							CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(),
							".json"
						}), string.Concat(new string[]
						{
							text2,
							"versions\\",
							CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(),
							"\\",
							CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(),
							".json"
						}));
						ModBase.CopyFile(string.Concat(new string[]
						{
							CS$<>8__locals1.$VB$Local_McFolder,
							"versions\\",
							CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(),
							"\\",
							CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(),
							".jar"
						}), string.Concat(new string[]
						{
							text2,
							"versions\\",
							CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(),
							"\\",
							CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(),
							".jar"
						}));
						Task.Progress = 0.06;
						bool flag2 = true;
						for (;;)
						{
							try
							{
								ModDownloadLib.McDownloadOptiFineInstall(text, CS$<>8__locals1.$VB$Local_Target, Task, flag2);
								break;
							}
							catch (Exception ex)
							{
								if (!flag2)
								{
									throw new Exception("运行 OptiFine 安装器失败", ex);
								}
								ModBase.Log(ex, "使用 JavaWrapper 安装 OptiFine 失败，将不使用 JavaWrapper 并重试", ModBase.LogLevel.Debug, "出现错误");
								flag2 = false;
							}
						}
						Task.Progress = 0.96;
						File.Delete(text2 + "launcher_profiles.json");
						ModBase.CopyDirectory(text2, CS$<>8__locals1.$VB$Local_McFolder, null);
						Task.Progress = 0.98;
						File.Delete(CS$<>8__locals1.$VB$Local_Target);
						ModBase.DeleteDirectory(text, false);
					}
					catch (Exception innerException)
					{
						throw new Exception("安装 OptiFine（方式 A）失败", innerException);
					}
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = 8.0
				});
			}
			else
			{
				ModBase.Log("[Download] 检测为旧版 OptiFine：" + CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(), ModBase.LogLevel.Normal, "出现错误");
				list.Add(new ModLoader.LoaderTask<List<ModNet.NetFile>, bool>("安装 OptiFine（方式 B）", delegate(ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task)
				{
					try
					{
						Directory.CreateDirectory(CS$<>8__locals1.$VB$Local_VersionFolder);
						Task.Progress = 0.1;
						if (File.Exists(CS$<>8__locals1.$VB$Local_VersionFolder + CS$<>8__locals1.$VB$Local_Id + ".jar"))
						{
							File.Delete(CS$<>8__locals1.$VB$Local_VersionFolder + CS$<>8__locals1.$VB$Local_Id + ".jar");
						}
						ModBase.CopyFile(string.Concat(new string[]
						{
							CS$<>8__locals1.$VB$Local_McFolder,
							"versions\\",
							CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(),
							"\\",
							CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(),
							".jar"
						}), CS$<>8__locals1.$VB$Local_VersionFolder + CS$<>8__locals1.$VB$Local_Id + ".jar");
						Task.Progress = 0.7;
						ModMinecraft.McVersion mcVersion = new ModMinecraft.McVersion(CS$<>8__locals1.$VB$Local_McFolder + "versions\\" + CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper());
						string text = string.Concat(new string[]
						{
							"{\r\n    \"id\": \"",
							CS$<>8__locals1.$VB$Local_Id,
							"\",\r\n    \"inheritsFrom\": \"",
							CS$<>8__locals1.$VB$Local_DownloadInfo.CheckMapper(),
							"\",\r\n    \"time\": \"",
							(Operators.CompareString(CS$<>8__locals1.$VB$Local_DownloadInfo.m_ProcTest, "", false) == 0) ? mcVersion._RegistryMap.ToString("yyyy'-'MM'-'dd") : CS$<>8__locals1.$VB$Local_DownloadInfo.m_ProcTest.Replace("/", "-"),
							"T23:33:33+08:00\",\r\n    \"releaseTime\": \"",
							(Operators.CompareString(CS$<>8__locals1.$VB$Local_DownloadInfo.m_ProcTest, "", false) == 0) ? mcVersion._RegistryMap.ToString("yyyy'-'MM'-'dd") : CS$<>8__locals1.$VB$Local_DownloadInfo.m_ProcTest.Replace("/", "-"),
							"T23:33:33+08:00\",\r\n    \"type\": \"release\",\r\n    \"libraries\": [\r\n        {\"name\": \"optifine:OptiFine:",
							CS$<>8__locals1.$VB$Local_DownloadInfo._DescriptorTest.Replace("OptiFine_", "").Replace(".jar", "").Replace("preview_", ""),
							"\"},\r\n        {\"name\": \"net.minecraft:launchwrapper:1.12\"}\r\n    ],\r\n    \"mainClass\": \"net.minecraft.launchwrapper.Launch\","
						});
						Task.Progress = 0.8;
						if (mcVersion.CloneThread())
						{
							text = text + "\r\n    \"minimumLauncherVersion\": 18,\r\n    \"minecraftArguments\": \"" + mcVersion.NewThread()["minecraftArguments"].ToString() + "  --tweakClass optifine.OptiFineTweaker\"\r\n}";
						}
						else
						{
							text += "\r\n    \"minimumLauncherVersion\": \"21\",\r\n    \"arguments\": {\r\n        \"game\": [\r\n            \"--tweakClass\",\r\n            \"optifine.OptiFineTweaker\"\r\n        ]\r\n    }\r\n}";
						}
						ModBase.WriteFile(CS$<>8__locals1.$VB$Local_VersionFolder + CS$<>8__locals1.$VB$Local_Id + ".json", text, false, null);
					}
					catch (Exception innerException)
					{
						throw new Exception("安装 OptiFine（方式 B）失败", innerException);
					}
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = 1.0
				});
			}
			if (FixLibrary)
			{
				list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析 OptiFine 支持库文件", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
				{
					Task.Output = ModMinecraft.McLibFix(new ModMinecraft.McVersion(CS$<>8__locals1.$VB$Local_VersionFolder));
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = 1.0,
					Show = false
				});
				list.Add(new ModNet.LoaderDownload("下载 OptiFine 支持库文件", new List<ModNet.NetFile>())
				{
					ProgressWeight = 4.0
				});
			}
			return list;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00036000 File Offset: 0x00034200
		private static List<ModLoader.LoaderBase> McDownloadOptiFineSaveLoader(ModDownload.DlOptiFineListEntry DownloadInfo, string TargetFolder)
		{
			return new List<ModLoader.LoaderBase>
			{
				new ModLoader.LoaderTask<ModDownload.DlOptiFineListEntry, List<ModNet.NetFile>>("获取 OptiFine 下载地址", delegate(ModLoader.LoaderTask<ModDownload.DlOptiFineListEntry, List<ModNet.NetFile>> Task)
				{
					List<string> list = new List<string>();
					string text = DownloadInfo.CheckMapper();
					if (Operators.CompareString(text, "1.8", false) == 0 || Operators.CompareString(text, "1.9", false) == 0)
					{
						text += ".0";
					}
					if (DownloadInfo._DefinitionTest)
					{
						list.Add("https://bmclapi2.bangbang93.com/optifine/" + text + "/HD_U_" + DownloadInfo.m_SchemaTest.Replace(DownloadInfo.CheckMapper() + " ", "").Replace(" ", "/"));
					}
					else
					{
						list.Add("https://bmclapi2.bangbang93.com/optifine/" + text + "/HD_U/" + DownloadInfo.m_SchemaTest.Replace(DownloadInfo.CheckMapper() + " ", ""));
					}
					try
					{
						string str = ModNet.NetGetCodeByClient("https://optifine.net/adloadx?f=" + DownloadInfo._DescriptorTest, new UTF8Encoding(false), 15000, "text/html", true);
						Task.Progress = 0.8;
						list.Add("https://optifine.net/" + ModBase.RegexSearch(str, "downloadx\\?f=[^\"']+", 0)[0]);
						ModBase.Log("[Download] OptiFine " + DownloadInfo.m_SchemaTest + " 官方下载地址：" + Enumerable.Last<string>(list), ModBase.LogLevel.Normal, "出现错误");
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "获取 OptiFine " + DownloadInfo.m_SchemaTest + " 官方下载地址失败", ModBase.LogLevel.Debug, "出现错误");
					}
					Task.Progress = 0.9;
					Task.Output = new List<ModNet.NetFile>
					{
						new ModNet.NetFile(list.ToArray(), TargetFolder, new ModBase.FileChecker(65536L, -1L, null, true, false), false)
					};
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = 6.0
				},
				new ModNet.LoaderDownload("下载 OptiFine 主文件", new List<ModNet.NetFile>())
				{
					ProgressWeight = 10.0,
					Block = true
				}
			};
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00036080 File Offset: 0x00034280
		public static MyListItem OptiFineDownloadListItem(ModDownload.DlOptiFineListEntry Entry, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
		{
			MyListItem myListItem = new MyListItem
			{
				Title = Entry.m_SchemaTest,
				SnapsToDevicePixels = true,
				Height = 42.0,
				Type = MyListItem.CheckType.Clickable,
				Tag = Entry,
				Info = (Entry._DefinitionTest ? "测试版" : "正式版") + ((Operators.CompareString(Entry.m_ProcTest, "", false) == 0) ? "" : ("，发布于 " + Entry.m_ProcTest)) + ((Entry.parserMap == null) ? "，不兼容 Forge" : ((Operators.CompareString(Entry.parserMap, "", false) == 0) ? "" : ("，推荐 Forge 版本：" + Entry.parserMap))),
				Logo = ModBase.m_SerializerRepository + "Blocks/GrassPath.png"
			};
			myListItem.Click += OnClick;
			if (IsSaveOnly)
			{
				myListItem.tokenComposer = new Action<MyListItem, EventArgs>(ModDownloadLib.OptiFineSaveContMenuBuild);
			}
			else
			{
				myListItem.tokenComposer = new Action<MyListItem, EventArgs>(ModDownloadLib.OptiFineContMenuBuild);
			}
			return myListItem;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0003618C File Offset: 0x0003438C
		private static void OptiFineSaveContMenuBuild(object sender, EventArgs e)
		{
			MyIconButton myIconButton = new MyIconButton
			{
				LogoScale = 1.05,
				Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
				ToolTip = "更新日志"
			};
			ToolTipService.SetPlacement(myIconButton, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton, 2.0);
			myIconButton.Click += ((ModDownloadLib._Closure$__.$IR20-8 == null) ? (ModDownloadLib._Closure$__.$IR20-8 = delegate(object sender, EventArgs e)
			{
				ModDownloadLib.OptiFineLog_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
			}) : ModDownloadLib._Closure$__.$IR20-8);
			NewLateBinding.LateSet(sender2, null, "Buttons", new object[]
			{
				new MyIconButton[]
				{
					myIconButton
				}
			}, null, null);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00036234 File Offset: 0x00034434
		private static void OptiFineContMenuBuild(object sender, EventArgs e)
		{
			MyIconButton myIconButton = new MyIconButton
			{
				Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
				ToolTip = "另存为"
			};
			ToolTipService.SetPlacement(myIconButton, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton, 2.0);
			myIconButton.Click += ((ModDownloadLib._Closure$__.$IR21-9 == null) ? (ModDownloadLib._Closure$__.$IR21-9 = delegate(object sender, EventArgs e)
			{
				ModDownloadLib.OptiFineSave_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
			}) : ModDownloadLib._Closure$__.$IR21-9);
			MyIconButton myIconButton2 = new MyIconButton
			{
				LogoScale = 1.05,
				Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
				ToolTip = "更新日志"
			};
			ToolTipService.SetPlacement(myIconButton2, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton2, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton2, 2.0);
			myIconButton2.Click += ((ModDownloadLib._Closure$__.$IR21-10 == null) ? (ModDownloadLib._Closure$__.$IR21-10 = delegate(object sender, EventArgs e)
			{
				ModDownloadLib.OptiFineLog_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
			}) : ModDownloadLib._Closure$__.$IR21-10);
			NewLateBinding.LateSet(sender2, null, "Buttons", new object[]
			{
				new MyIconButton[]
				{
					myIconButton,
					myIconButton2
				}
			}, null, null);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0003634C File Offset: 0x0003454C
		private static void OptiFineLog_Click(object sender, RoutedEventArgs e)
		{
			ModDownload.DlOptiFineListEntry dlOptiFineListEntry;
			if (NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null)
			{
				dlOptiFineListEntry = (ModDownload.DlOptiFineListEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
			}
			else if (NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) != null)
			{
				dlOptiFineListEntry = (ModDownload.DlOptiFineListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
			}
			else
			{
				dlOptiFineListEntry = (ModDownload.DlOptiFineListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
			}
			ModBase.OpenWebsite("https://optifine.net/changelog?f=" + dlOptiFineListEntry._DescriptorTest);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00036444 File Offset: 0x00034644
		public static void OptiFineSave_Click(object sender, RoutedEventArgs e)
		{
			ModDownload.DlOptiFineListEntry downloadInfo;
			if (NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null)
			{
				downloadInfo = (ModDownload.DlOptiFineListEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
			}
			else if (NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) != null)
			{
				downloadInfo = (ModDownload.DlOptiFineListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
			}
			else
			{
				downloadInfo = (ModDownload.DlOptiFineListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
			}
			ModDownloadLib.McDownloadOptiFineSave(downloadInfo);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00036530 File Offset: 0x00034730
		public static void McDownloadLiteLoader(ModDownload.DlLiteLoaderListEntry DownloadInfo)
		{
			try
			{
				string inherit = DownloadInfo.Inherit;
				ModBase.m_DecoratorRepository + "Download\\" + inherit + "-Liteloader.jar";
				string text = DownloadInfo.Inherit + "-LiteLoader";
				string text2 = ModMinecraft.m_ProxyTests + "versions\\" + text + "\\";
				try
				{
					List<ModLoader.LoaderBase>.Enumerator enumerator = Enumerable.ToList<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar).GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (Operators.CompareString(enumerator.Current.Name, string.Format("LiteLoader {0} 下载", inherit), false) == 0)
						{
							ModMain.Hint("该版本正在下载中！", ModMain.HintType.Critical, true);
							return;
						}
					}
				}
				finally
				{
					List<ModLoader.LoaderBase>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				if (File.Exists(text2 + text + ".json"))
				{
					if (ModMain.MyMsgBox("版本 " + text + " 已存在，是否重新下载？\r\n这会覆盖版本的 json 和 jar 文件，但不会影响版本隔离的文件。", "版本已存在", "继续", "取消", "", false, true, false, null, null, null) != 1)
					{
						return;
					}
					File.Delete(text2 + text + ".jar");
					File.Delete(text2 + text + ".json");
				}
				ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("LiteLoader " + inherit + " 下载", ModDownloadLib.McDownloadLiteLoaderLoader(DownloadInfo, null, null, true));
				loaderCombo.OnStateChanged = new Action<ModLoader.LoaderBase>(ModDownloadLib.McInstallState);
				loaderCombo.Start(text2, false);
				ModLoader.LoaderTaskbarAdd<string>(loaderCombo);
				ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
				ModMain._ProcessIterator.BtnExtraDownload.Ribble();
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "开始 LiteLoader 下载失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000366FC File Offset: 0x000348FC
		private static void McDownloadLiteLoaderSave(ModDownload.DlLiteLoaderListEntry DownloadInfo)
		{
			try
			{
				string inherit = DownloadInfo.Inherit;
				string text = ModBase.SelectAs("选择保存位置", DownloadInfo.FileName.Replace("-SNAPSHOT", ""), "LiteLoader 安装器 (*.jar)|*.jar", null);
				if (text.Contains("\\"))
				{
					try
					{
						List<ModLoader.LoaderBase>.Enumerator enumerator = Enumerable.ToList<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar).GetEnumerator();
						while (enumerator.MoveNext())
						{
							if (Operators.CompareString(enumerator.Current.Name, string.Format("LiteLoader {0} 下载", inherit), false) == 0)
							{
								ModMain.Hint("该版本正在下载中！", ModMain.HintType.Critical, true);
								return;
							}
						}
					}
					finally
					{
						List<ModLoader.LoaderBase>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
					List<string> list2 = new List<string>();
					if (DownloadInfo.IsLegacy)
					{
						string inherit2 = DownloadInfo.Inherit;
						if (Operators.CompareString(inherit2, "1.7.10", false) != 0)
						{
							if (Operators.CompareString(inherit2, "1.7.2", false) != 0)
							{
								if (Operators.CompareString(inherit2, "1.6.4", false) != 0)
								{
									if (Operators.CompareString(inherit2, "1.6.2", false) != 0)
									{
										if (Operators.CompareString(inherit2, "1.5.2", false) != 0)
										{
											throw new NotSupportedException("未知的 Minecraft 版本（" + DownloadInfo.Inherit + "）");
										}
										list2.Add("https://dl.liteloader.com/redist/1.5.2/liteloader-installer-1.5.2-01.jar");
									}
									else
									{
										list2.Add("https://dl.liteloader.com/redist/1.6.2/liteloader-installer-1.6.2-04.jar");
									}
								}
								else
								{
									list2.Add("https://dl.liteloader.com/redist/1.6.4/liteloader-installer-1.6.4-01.jar");
								}
							}
							else
							{
								list2.Add("https://dl.liteloader.com/redist/1.7.2/liteloader-installer-1.7.2-04.jar");
							}
						}
						else
						{
							list2.Add("https://dl.liteloader.com/redist/1.7.10/liteloader-installer-1.7.10-04.jar");
						}
					}
					else
					{
						list2.Add(string.Concat(new string[]
						{
							"http://jenkins.liteloader.com/job/LiteLoaderInstaller%20",
							DownloadInfo.Inherit,
							"/lastSuccessfulBuild/artifact/",
							(Operators.CompareString(DownloadInfo.Inherit, "1.8", false) == 0) ? "ant/dist/" : "build/libs/",
							DownloadInfo.FileName
						}));
					}
					list.Add(new ModNet.LoaderDownload("下载主文件", new List<ModNet.NetFile>
					{
						new ModNet.NetFile(list2.ToArray(), text, new ModBase.FileChecker(1048576L, -1L, null, true, false), false)
					})
					{
						ProgressWeight = 15.0
					});
					ModLoader.LoaderCombo<ModDownload.DlLiteLoaderListEntry> loaderCombo = new ModLoader.LoaderCombo<ModDownload.DlLiteLoaderListEntry>("LiteLoader " + inherit + " 安装器下载", list);
					loaderCombo.OnStateChanged = new Action<ModLoader.LoaderBase>(ModDownloadLib.DownloadStateSave);
					loaderCombo.Start(DownloadInfo, false);
					ModLoader.LoaderTaskbarAdd<ModDownload.DlLiteLoaderListEntry>(loaderCombo);
					ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
					ModMain._ProcessIterator.BtnExtraDownload.Ribble();
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "开始 LiteLoader 安装器下载失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000369C4 File Offset: 0x00034BC4
		private static List<ModLoader.LoaderBase> McDownloadLiteLoaderLoader(ModDownload.DlLiteLoaderListEntry DownloadInfo, string McFolder = null, ModLoader.LoaderCombo<string> ClientDownloadLoader = null, bool FixLibrary = true)
		{
			McFolder = (McFolder ?? ModMinecraft.m_ProxyTests);
			bool IsCustomFolder = Operators.CompareString(McFolder, ModMinecraft.m_ProxyTests, false) != 0;
			string inherit = DownloadInfo.Inherit;
			ModBase.m_DecoratorRepository + "Download\\" + inherit + "-Liteloader.jar";
			string VersionName = DownloadInfo.Inherit + "-LiteLoader";
			string VersionFolder = McFolder + "versions\\" + VersionName + "\\";
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			if (ClientDownloadLoader == null)
			{
				list.Add(new ModLoader.LoaderTask<string, string>("启动 LiteLoader 依赖版本下载", delegate(ModLoader.LoaderTask<string, string> a0)
				{
					base._Lambda$__0();
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = 0.2,
					Show = false,
					Block = false
				});
			}
			list.Add(new ModLoader.LoaderTask<string, string>("安装 LiteLoader", delegate(ModLoader.LoaderTask<string, string> Task)
			{
				try
				{
					Directory.CreateDirectory(VersionFolder);
					JObject jobject = new JObject();
					jobject.Add("id", VersionName);
					jobject.Add("time", DateTime.ParseExact(DownloadInfo.ReleaseTime, "yyyy/MM/dd HH:mm", CultureInfo.CurrentCulture));
					jobject.Add("releaseTime", DateTime.ParseExact(DownloadInfo.ReleaseTime, "yyyy/MM/dd HH:mm", CultureInfo.CurrentCulture));
					jobject.Add("type", "release");
					jobject.Add("arguments", (JToken)ModBase.GetJson("{\"game\":[\"--tweakClass\",\"" + DownloadInfo.JsonToken["tweakClass"].ToString() + "\"]}"));
					jobject.Add("libraries", DownloadInfo.JsonToken["libraries"]);
					((JContainer)jobject["libraries"]).Add(RuntimeHelpers.GetObjectValue(ModBase.GetJson("{\"name\": \"com.mumfrey:liteloader:" + DownloadInfo.JsonToken["version"].ToString() + "\",\"url\": \"https://dl.liteloader.com/versions/\"}")));
					jobject.Add("mainClass", "net.minecraft.launchwrapper.Launch");
					jobject.Add("minimumLauncherVersion", 18);
					jobject.Add("inheritsFrom", DownloadInfo.Inherit);
					jobject.Add("jar", DownloadInfo.Inherit);
					ModBase.WriteFile(VersionFolder + VersionName + ".json", jobject.ToString(), false, null);
				}
				catch (Exception innerException)
				{
					throw new Exception("安装新 LiteLoader 版本失败", innerException);
				}
			}, null, ThreadPriority.Normal)
			{
				ProgressWeight = 1.0
			});
			if (FixLibrary)
			{
				list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析 LiteLoader 支持库文件", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
				{
					Task.Output = ModMinecraft.McLibFix(new ModMinecraft.McVersion(VersionFolder));
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = 1.0,
					Show = false
				});
				list.Add(new ModNet.LoaderDownload("下载 LiteLoader 支持库文件", new List<ModNet.NetFile>())
				{
					ProgressWeight = 6.0
				});
			}
			return list;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00036B34 File Offset: 0x00034D34
		public static MyListItem LiteLoaderDownloadListItem(ModDownload.DlLiteLoaderListEntry Entry, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
		{
			MyListItem myListItem = new MyListItem
			{
				Title = Entry.Inherit,
				SnapsToDevicePixels = true,
				Height = 42.0,
				Type = MyListItem.CheckType.Clickable,
				Tag = Entry,
				Info = (Entry.IsPreview ? "测试版" : "稳定版") + ((Operators.CompareString(Entry.ReleaseTime, "", false) == 0) ? "" : ("，发布于 " + Entry.ReleaseTime)),
				Logo = ModBase.m_SerializerRepository + "Blocks/Egg.png"
			};
			myListItem.Click += OnClick;
			if (IsSaveOnly)
			{
				myListItem.tokenComposer = new Action<MyListItem, EventArgs>(ModDownloadLib.LiteLoaderSaveContMenuBuild);
			}
			else
			{
				myListItem.tokenComposer = new Action<MyListItem, EventArgs>(ModDownloadLib.LiteLoaderContMenuBuild);
			}
			return myListItem;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00036C08 File Offset: 0x00034E08
		private static void LiteLoaderSaveContMenuBuild(MyListItem sender, EventArgs e)
		{
			if (Conversions.ToBoolean(NewLateBinding.LateGet(sender2.Tag, null, "IsLegacy", new object[0], null, null, null)))
			{
				sender2.Buttons = new MyIconButton[0];
				return;
			}
			MyIconButton myIconButton = new MyIconButton
			{
				Logo = "M384 128h640v128H384zM160 192m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0ZM384 448h640v128H384zM160 512m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0ZM384 768h640v128H384zM160 832m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0Z",
				ToolTip = "查看全部版本",
				Tag = sender2
			};
			ToolTipService.SetPlacement(myIconButton, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton, 2.0);
			myIconButton.Click += ((ModDownloadLib._Closure$__.$IR28-12 == null) ? (ModDownloadLib._Closure$__.$IR28-12 = delegate(object sender, EventArgs e)
			{
				ModDownloadLib.LiteLoaderAll_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
			}) : ModDownloadLib._Closure$__.$IR28-12);
			sender2.Buttons = new MyIconButton[]
			{
				myIconButton
			};
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00036CC8 File Offset: 0x00034EC8
		private static void LiteLoaderContMenuBuild(MyListItem sender, EventArgs e)
		{
			MyIconButton myIconButton = new MyIconButton
			{
				Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
				ToolTip = "保存安装器",
				Tag = sender2
			};
			ToolTipService.SetPlacement(myIconButton, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton, 2.0);
			myIconButton.Click += ((ModDownloadLib._Closure$__.$IR29-13 == null) ? (ModDownloadLib._Closure$__.$IR29-13 = delegate(object sender, EventArgs e)
			{
				ModDownloadLib.LiteLoaderSave_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
			}) : ModDownloadLib._Closure$__.$IR29-13);
			if (Conversions.ToBoolean(NewLateBinding.LateGet(sender2.Tag, null, "IsLegacy", new object[0], null, null, null)))
			{
				sender2.Buttons = new MyIconButton[]
				{
					myIconButton
				};
				return;
			}
			MyIconButton myIconButton2 = new MyIconButton
			{
				Logo = "M384 128h640v128H384zM160 192m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0ZM384 448h640v128H384zM160 512m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0ZM384 768h640v128H384zM160 832m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0Z",
				ToolTip = "查看全部版本",
				Tag = sender2
			};
			ToolTipService.SetPlacement(myIconButton2, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton2, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton2, 2.0);
			myIconButton2.Click += ((ModDownloadLib._Closure$__.$IR29-14 == null) ? (ModDownloadLib._Closure$__.$IR29-14 = delegate(object sender, EventArgs e)
			{
				ModDownloadLib.LiteLoaderAll_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
			}) : ModDownloadLib._Closure$__.$IR29-14);
			sender2.Buttons = new MyIconButton[]
			{
				myIconButton,
				myIconButton2
			};
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00036E00 File Offset: 0x00035000
		private static void LiteLoaderAll_Click(object sender, RoutedEventArgs e)
		{
			ModDownload.DlLiteLoaderListEntry dlLiteLoaderListEntry;
			if (NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) is ModDownload.DlLiteLoaderListEntry)
			{
				dlLiteLoaderListEntry = (ModDownload.DlLiteLoaderListEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
			}
			else
			{
				dlLiteLoaderListEntry = (ModDownload.DlLiteLoaderListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
			}
			ModBase.OpenWebsite("https://jenkins.liteloader.com/view/" + dlLiteLoaderListEntry.Inherit);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00036E8C File Offset: 0x0003508C
		public static void LiteLoaderSave_Click(object sender, RoutedEventArgs e)
		{
			ModDownload.DlLiteLoaderListEntry downloadInfo;
			if (NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) is ModDownload.DlLiteLoaderListEntry)
			{
				downloadInfo = (ModDownload.DlLiteLoaderListEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
			}
			else
			{
				downloadInfo = (ModDownload.DlLiteLoaderListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
			}
			ModDownloadLib.McDownloadLiteLoaderSave(downloadInfo);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00036F08 File Offset: 0x00035108
		public static void McDownloadForgelikeSave(ModDownload.DlForgelikeEntry Info)
		{
			try
			{
				string text = ModBase.SelectAs("选择保存位置", string.Format("{0}-{1}-{2}.{3}", new object[]
				{
					Info.CustomizeMapper(),
					Info._TestsMap,
					Info.m_ConfigMap,
					Info.AssetMapper()
				}), string.Format("{0} 安装器 (*.{1})|*.{2}", Info.CustomizeMapper(), Info.AssetMapper(), Info.AssetMapper()), null);
				string text2 = string.Format("{0} {1} - {2}", Info.CustomizeMapper(), Info._TestsMap, Info.m_ConfigMap);
				if (text.Contains("\\"))
				{
					try
					{
						List<ModLoader.LoaderBase>.Enumerator enumerator = Enumerable.ToList<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar).GetEnumerator();
						while (enumerator.MoveNext())
						{
							if (Operators.CompareString(enumerator.Current.Name, string.Format("{0} 下载", text2), false) == 0)
							{
								ModMain.Hint("该版本正在下载中！", ModMain.HintType.Critical, true);
								return;
							}
						}
					}
					finally
					{
						List<ModLoader.LoaderBase>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					List<ModNet.NetFile> list = new List<ModNet.NetFile>();
					if (Info.m_ReaderMap)
					{
						string text3 = ((ModDownload.DlNeoForgeListEntry)Info).RateMapper() + "-installer.jar";
						list.Add(new ModNet.NetFile(new string[]
						{
							text3.Replace("maven.neoforged.net/releases", "bmclapi2.bangbang93.com/maven"),
							text3
						}, text, new ModBase.FileChecker(65536L, -1L, null, true, false), false));
					}
					else
					{
						ModDownload.DlForgeVersionEntry dlForgeVersionEntry = (ModDownload.DlForgeVersionEntry)Info;
						list.Add(new ModNet.NetFile(new string[]
						{
							string.Format("https://bmclapi2.bangbang93.com/maven/net/minecraftforge/forge/{0}-{1}/forge-{2}-{3}-{4}.{5}", new object[]
							{
								dlForgeVersionEntry._TestsMap,
								dlForgeVersionEntry.iteratorMap,
								dlForgeVersionEntry._TestsMap,
								dlForgeVersionEntry.iteratorMap,
								dlForgeVersionEntry._ComposerMap,
								dlForgeVersionEntry.AssetMapper()
							}),
							string.Format("https://files.minecraftforge.net/maven/net/minecraftforge/forge/{0}-{1}/forge-{2}-{3}-{4}.{5}", new object[]
							{
								dlForgeVersionEntry._TestsMap,
								dlForgeVersionEntry.iteratorMap,
								dlForgeVersionEntry._TestsMap,
								dlForgeVersionEntry.iteratorMap,
								dlForgeVersionEntry._ComposerMap,
								dlForgeVersionEntry.AssetMapper()
							})
						}, text, new ModBase.FileChecker(65536L, -1L, dlForgeVersionEntry.threadMap, true, false), false));
					}
					List<ModLoader.LoaderBase> list2 = new List<ModLoader.LoaderBase>();
					list2.Add(new ModNet.LoaderDownload("下载主文件", list)
					{
						ProgressWeight = 6.0
					});
					ModLoader.LoaderCombo<ModDownload.DlForgelikeEntry> loaderCombo = new ModLoader.LoaderCombo<ModDownload.DlForgelikeEntry>(text2 + " 下载", list2);
					loaderCombo.OnStateChanged = new Action<ModLoader.LoaderBase>(ModDownloadLib.DownloadStateSave);
					loaderCombo.Start(Info, false);
					ModLoader.LoaderTaskbarAdd<ModDownload.DlForgelikeEntry>(loaderCombo);
					ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
					ModMain._ProcessIterator.BtnExtraDownload.Ribble();
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, string.Format("开始 {0} 安装器下载失败", Info.CustomizeMapper()), ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0003721C File Offset: 0x0003541C
		private static void ForgelikeInjector(string Target, ModLoader.LoaderTask<bool, bool> Task, string McFolder, bool UseJavaWrapper, bool IsNeoForge)
		{
			ModDownloadLib._Closure$__33-2 CS$<>8__locals1 = new ModDownloadLib._Closure$__33-2(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_Task = Task;
			object dispatcher = ModJava.m_Dispatcher;
			ObjectFlowControl.CheckForSyncLockOnValueType(dispatcher);
			ModJava.JavaEntry javaEntry;
			lock (dispatcher)
			{
				javaEntry = ModJava.JavaSelect("已取消安装。", new Version(1, 8, 0, 60), null, null);
				if (javaEntry == null)
				{
					if (!ModJava.JavaDownloadConfirm("Java 8 或更高版本", false))
					{
						throw new Exception("由于未找到 Java，已取消安装。");
					}
					ModLoader.LoaderCombo<int> loaderCombo = ModJava.JavaFixLoaders(17);
					try
					{
						loaderCombo.Start(17, true);
						while (loaderCombo.State == ModBase.LoadState.Loading && !CS$<>8__locals1.$VB$Local_Task.IsAborted)
						{
							Thread.Sleep(10);
						}
					}
					finally
					{
						loaderCombo.Abort();
					}
					javaEntry = ModJava.JavaSelect("已取消安装。", new Version(1, 8, 0, 60), null, null);
					if (CS$<>8__locals1.$VB$Local_Task.IsAborted)
					{
						return;
					}
					if (javaEntry == null)
					{
						throw new Exception("由于未找到 Java，已取消安装。");
					}
				}
			}
			string text;
			if (UseJavaWrapper)
			{
				text = string.Format("-Doolloo.jlw.tmpdir=\"{0}\" -cp \"{1}Cache\\forge_installer.jar;{2}\" -jar \"{3}\" com.bangbang93.ForgeInstaller \"{4}", new object[]
				{
					ModBase._MethodRepository.TrimEnd(new char[]
					{
						'\\'
					}),
					ModBase.m_DecoratorRepository,
					Target,
					ModLaunch.ExtractJavaWrapper(),
					McFolder
				});
			}
			else
			{
				text = string.Format("-cp \"{0}Cache\\forge_installer.jar;{1}\" com.bangbang93.ForgeInstaller \"{2}", ModBase.m_DecoratorRepository, Target, McFolder);
			}
			if (javaEntry.PostTests() >= 9)
			{
				text = "--add-exports cpw.mods.bootstraplauncher/cpw.mods.bootstraplauncher=ALL-UNNAMED " + text;
			}
			object statusField = ModDownloadLib._StatusField;
			ObjectFlowControl.CheckForSyncLockOnValueType(statusField);
			checked
			{
				lock (statusField)
				{
					ModDownloadLib._Closure$__33-1 CS$<>8__locals2 = new ModDownloadLib._Closure$__33-1(CS$<>8__locals2);
					CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2 = CS$<>8__locals1;
					ProcessStartInfo startInfo = new ProcessStartInfo
					{
						FileName = javaEntry.PrintTests(),
						Arguments = text,
						UseShellExecute = false,
						CreateNoWindow = true,
						RedirectStandardError = true,
						RedirectStandardOutput = true
					};
					CS$<>8__locals2.$VB$Local_LoaderName = (IsNeoForge ? "NeoForge" : "Forge");
					ModBase.Log(string.Format("[Download] 开始安装 {0}：", CS$<>8__locals2.$VB$Local_LoaderName) + text, ModBase.LogLevel.Normal, "出现错误");
					CS$<>8__locals2.$VB$Local_process = new Process
					{
						StartInfo = startInfo
					};
					CS$<>8__locals2.$VB$Local_LastResults = new Queue<string>();
					ModDownloadLib._Closure$__33-0 CS$<>8__locals3 = new ModDownloadLib._Closure$__33-0(CS$<>8__locals3);
					CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3 = CS$<>8__locals2;
					CS$<>8__locals3.$VB$Local_outputWaitHandle = new AutoResetEvent(false);
					try
					{
						ModDownloadLib._Closure$__33-3 CS$<>8__locals4 = new ModDownloadLib._Closure$__33-3(CS$<>8__locals4);
						CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4 = CS$<>8__locals3;
						CS$<>8__locals4.$VB$Local_errorWaitHandle = new AutoResetEvent(false);
						try
						{
							CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs e)
							{
								base._Lambda$__0(RuntimeHelpers.GetObjectValue(sender), e);
							};
							CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_process.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs e)
							{
								base._Lambda$__1(RuntimeHelpers.GetObjectValue(sender), e);
							};
							CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_process.Start();
							CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_process.BeginOutputReadLine();
							CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_process.BeginErrorReadLine();
							while (!CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_process.HasExited)
							{
								Thread.Sleep(10);
							}
							CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$Local_outputWaitHandle.WaitOne(10000);
							CS$<>8__locals4.$VB$Local_errorWaitHandle.WaitOne(10000);
							CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_process.Dispose();
							if (Operators.CompareString(Enumerable.Last<string>(CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_LastResults), "true", false) != 0 && (CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_LastResults.Count < 2 || Operators.CompareString(Enumerable.ElementAtOrDefault<string>(CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_LastResults, CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_LastResults.Count - 2), "true", false) != 0))
							{
								ModBase.Log(CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_LastResults.Join("\r\n"), ModBase.LogLevel.Normal, "出现错误");
								string text2 = "";
								int num = Math.Max(0, CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_LastResults.Count - 5);
								int num2 = CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_LastResults.Count - 1;
								for (int i = num; i <= num2; i++)
								{
									text2 = text2 + "\r\n" + Enumerable.ElementAtOrDefault<string>(CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_LastResults, i);
								}
								throw new Exception(string.Format("{0} 安装器出错，日志结束部分为：", CS$<>8__locals4.$VB$NonLocal_$VB$Closure_4.$VB$NonLocal_$VB$Closure_3.$VB$Local_LoaderName) + text2);
							}
						}
						finally
						{
							if (CS$<>8__locals4.$VB$Local_errorWaitHandle != null)
							{
								((IDisposable)CS$<>8__locals4.$VB$Local_errorWaitHandle).Dispose();
							}
						}
					}
					finally
					{
						if (CS$<>8__locals3.$VB$Local_outputWaitHandle != null)
						{
							((IDisposable)CS$<>8__locals3.$VB$Local_outputWaitHandle).Dispose();
						}
					}
				}
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00037758 File Offset: 0x00035958
		private static void ForgelikeInjectorLine(string Content, ModLoader.LoaderTask<bool, bool> Task)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(Content);
			if (num <= 2947477999U)
			{
				if (num <= 867564423U)
				{
					if (num != 214422873U)
					{
						if (num != 538128555U)
						{
							if (num == 867564423U)
							{
								if (Operators.CompareString(Content, "Remapping jar... 50%", false) == 0)
								{
									Task.Progress = 0.76;
									goto IL_36E;
								}
							}
						}
						else if (Operators.CompareString(Content, "Remapping final jar", false) == 0)
						{
							Task.Progress = 0.72;
							goto IL_36E;
						}
					}
					else if (Operators.CompareString(Content, "Splitting: ", false) == 0)
					{
						Task.Progress = 0.35;
						goto IL_36E;
					}
				}
				else if (num <= 2394603510U)
				{
					if (num != 1592277622U)
					{
						if (num == 2394603510U)
						{
							if (Operators.CompareString(Content, "Building Processors", false) == 0)
							{
								Task.Progress = 0.18;
								goto IL_36E;
							}
						}
					}
					else if (Operators.CompareString(Content, "log: null", false) == 0)
					{
						Task.Progress = 0.5;
						goto IL_36E;
					}
				}
				else if (num != 2846730644U)
				{
					if (num == 2947477999U)
					{
						if (Operators.CompareString(Content, "Sorting", false) == 0)
						{
							Task.Progress = 0.65;
							goto IL_36E;
						}
					}
				}
				else if (Operators.CompareString(Content, "Task: DOWNLOAD_MOJMAPS", false) == 0)
				{
					Task.Progress = 0.2;
					goto IL_36E;
				}
			}
			else if (num <= 3369270864U)
			{
				if (num <= 3198574777U)
				{
					if (num != 2971959160U)
					{
						if (num == 3198574777U)
						{
							if (Operators.CompareString(Content, "Injecting profile", false) == 0)
							{
								Task.Progress = 0.91;
								goto IL_36E;
							}
						}
					}
					else if (Operators.CompareString(Content, "  File exists: Checksum validated.", false) == 0)
					{
						if (ModBase._TokenRepository)
						{
							ModBase.Log("[Installer] " + Content, ModBase.LogLevel.Normal, "出现错误");
						}
						Task.Progress += 0.003;
						goto IL_36E;
					}
				}
				else if (num != 3296415987U)
				{
					if (num == 3369270864U)
					{
						if (Operators.CompareString(Content, "Extracting json", false) == 0)
						{
							ModBase.Log("[Installer] " + Content, ModBase.LogLevel.Normal, "出现错误");
							Task.Progress = 0.07;
							goto IL_36E;
						}
					}
				}
				else if (Operators.CompareString(Content, "Remapping jar... 100%", false) == 0)
				{
					Task.Progress = 0.81;
					goto IL_36E;
				}
			}
			else if (num <= 3905690050U)
			{
				if (num != 3587956665U)
				{
					if (num == 3905690050U)
					{
						if (Operators.CompareString(Content, "Parameter Annotations", false) == 0)
						{
							Task.Progress = 0.4;
							goto IL_36E;
						}
					}
				}
				else if (Operators.CompareString(Content, "Processing Complete", false) == 0)
				{
					Task.Progress = 0.5;
					goto IL_36E;
				}
			}
			else if (num != 3935946959U)
			{
				if (num == 4046473188U)
				{
					if (Operators.CompareString(Content, "Downloading libraries", false) == 0)
					{
						ModBase.Log("[Installer] " + Content, ModBase.LogLevel.Normal, "出现错误");
						Task.Progress = 0.08;
						goto IL_36E;
					}
				}
			}
			else if (Operators.CompareString(Content, "Task: MERGE_MAPPING", false) == 0)
			{
				Task.Progress = 0.3;
				goto IL_36E;
			}
			if (ModBase._TokenRepository)
			{
				ModBase.Log("[Installer] " + Content, ModBase.LogLevel.Normal, "出现错误");
				return;
			}
			return;
			IL_36E:
			ModBase.Log("[Installer] " + Content, ModBase.LogLevel.Normal, "出现错误");
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00037AEC File Offset: 0x00035CEC
		private static List<ModLoader.LoaderBase> McDownloadForgelikeLoader(bool IsNeoForge, string LoaderVersion, string TargetVersion, string Inherit, ModDownload.DlForgelikeEntry Info = null, string McFolder = null, ModLoader.LoaderCombo<string> ClientDownloadLoader = null, string ClientFolder = null)
		{
			ModDownloadLib._Closure$__35-0 CS$<>8__locals1 = new ModDownloadLib._Closure$__35-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_IsNeoForge = IsNeoForge;
			CS$<>8__locals1.$VB$Local_LoaderVersion = LoaderVersion;
			CS$<>8__locals1.$VB$Local_TargetVersion = TargetVersion;
			CS$<>8__locals1.$VB$Local_Inherit = Inherit;
			CS$<>8__locals1.$VB$Local_Info = Info;
			CS$<>8__locals1.$VB$Local_McFolder = McFolder;
			CS$<>8__locals1.$VB$Local_ClientDownloadLoader = ClientDownloadLoader;
			CS$<>8__locals1.$VB$Local_ClientFolder = ClientFolder;
			CS$<>8__locals1.$VB$Local_McFolder = (CS$<>8__locals1.$VB$Local_McFolder ?? ModMinecraft.m_ProxyTests);
			if (CS$<>8__locals1.$VB$Local_IsNeoForge && CS$<>8__locals1.$VB$Local_Info == null)
			{
				if (Operators.CompareString(CS$<>8__locals1.$VB$Local_Inherit, "1.20.1", false) == 0 && !CS$<>8__locals1.$VB$Local_LoaderVersion.StartsWithF("1.20.1-", false))
				{
					CS$<>8__locals1.$VB$Local_Info = new ModDownload.DlNeoForgeListEntry("1.20.1-" + CS$<>8__locals1.$VB$Local_LoaderVersion);
				}
				else
				{
					CS$<>8__locals1.$VB$Local_Info = new ModDownload.DlNeoForgeListEntry(CS$<>8__locals1.$VB$Local_LoaderVersion);
				}
			}
			if (!CS$<>8__locals1.$VB$Local_IsNeoForge && CS$<>8__locals1.$VB$Local_LoaderVersion.StartsWithF("1.", false) && CS$<>8__locals1.$VB$Local_LoaderVersion.Contains("-"))
			{
				CS$<>8__locals1.$VB$Local_Inherit = CS$<>8__locals1.$VB$Local_LoaderVersion.BeforeFirst("-", false);
				CS$<>8__locals1.$VB$Local_LoaderVersion = CS$<>8__locals1.$VB$Local_LoaderVersion.AfterLast("-", false);
			}
			CS$<>8__locals1.$VB$Local_LoaderName = (CS$<>8__locals1.$VB$Local_IsNeoForge ? "NeoForge" : "Forge");
			CS$<>8__locals1.$VB$Local_IsCustomFolder = (Operators.CompareString(CS$<>8__locals1.$VB$Local_McFolder, ModMinecraft.m_ProxyTests, false) != 0);
			CS$<>8__locals1.$VB$Local_InstallerAddress = string.Format("{0}Cache\\Code\\{1}Install-{2}_{3}", new object[]
			{
				ModBase.m_DecoratorRepository,
				CS$<>8__locals1.$VB$Local_LoaderName,
				CS$<>8__locals1.$VB$Local_LoaderVersion,
				ModBase.RandomInteger(0, 100000)
			});
			CS$<>8__locals1.$VB$Local_VersionFolder = string.Format("{0}versions\\{1}\\", CS$<>8__locals1.$VB$Local_McFolder, CS$<>8__locals1.$VB$Local_TargetVersion);
			string.Format("{0} {1} - {2}", CS$<>8__locals1.$VB$Local_LoaderName, CS$<>8__locals1.$VB$Local_Inherit, CS$<>8__locals1.$VB$Local_LoaderVersion);
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			string.Format("{0}versions\\{1}\\", ModMinecraft.m_ProxyTests, CS$<>8__locals1.$VB$Local_TargetVersion);
			if (CS$<>8__locals1.$VB$Local_Info == null)
			{
				list.Add(new ModLoader.LoaderTask<string, string>(string.Format("获取 {0} 详细信息", CS$<>8__locals1.$VB$Local_LoaderName), delegate(ModLoader.LoaderTask<string, string> Task)
				{
					ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> loaderTask = new ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>("McDownloadForgeLoader " + CS$<>8__locals1.$VB$Local_Inherit, new Action<ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>>(ModDownload.DlForgeVersionMain), null, ThreadPriority.Normal);
					loaderTask.WaitForExit(CS$<>8__locals1.$VB$Local_Inherit, null, false);
					Task.Progress = 0.8;
					try
					{
						foreach (ModDownload.DlForgeVersionEntry dlForgeVersionEntry in loaderTask.Output)
						{
							if (ModMinecraft.VersionSortInteger(dlForgeVersionEntry.clientMap.ToString(), CS$<>8__locals1.$VB$Local_LoaderVersion) == 0)
							{
								CS$<>8__locals1.$VB$Local_Info = dlForgeVersionEntry;
								return;
							}
						}
					}
					finally
					{
						List<ModDownload.DlForgeVersionEntry>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					throw new Exception(string.Concat(new string[]
					{
						string.Format("未能找到 {0} ", CS$<>8__locals1.$VB$Local_LoaderName),
						CS$<>8__locals1.$VB$Local_Inherit,
						"-",
						CS$<>8__locals1.$VB$Local_LoaderVersion,
						" 的详细信息！"
					}));
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = 3.0
				});
			}
			list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>(string.Format("准备下载 {0}", CS$<>8__locals1.$VB$Local_LoaderName), delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
			{
				if (CS$<>8__locals1.$VB$Local_ClientDownloadLoader == null)
				{
					if (CS$<>8__locals1.$VB$Local_IsCustomFolder)
					{
						throw new Exception("如果没有指定原版下载器，则不能指定 MC 安装文件夹");
					}
					CS$<>8__locals1.$VB$Local_ClientDownloadLoader = ModDownloadLib.McDownloadClient(ModNet.NetPreDownloadBehaviour.ExitWhileExistsOrDownloading, CS$<>8__locals1.$VB$Local_Inherit, null);
				}
				List<ModNet.NetFile> list2 = new List<ModNet.NetFile>();
				if (CS$<>8__locals1.$VB$Local_Info.m_ReaderMap)
				{
					string text = ((ModDownload.DlNeoForgeListEntry)CS$<>8__locals1.$VB$Local_Info).RateMapper() + "-installer.jar";
					list2.Add(new ModNet.NetFile(new string[]
					{
						text.Replace("maven.neoforged.net/releases", "bmclapi2.bangbang93.com/maven"),
						text
					}, CS$<>8__locals1.$VB$Local_InstallerAddress, new ModBase.FileChecker(65536L, -1L, null, true, false), false));
				}
				else
				{
					ModDownload.DlForgeVersionEntry dlForgeVersionEntry = (ModDownload.DlForgeVersionEntry)CS$<>8__locals1.$VB$Local_Info;
					string arg = string.Format("{0}-{1}/forge-{2}-{3}-{4}.{5}", new object[]
					{
						dlForgeVersionEntry._TestsMap.Replace("-", "_"),
						dlForgeVersionEntry.iteratorMap,
						dlForgeVersionEntry._TestsMap.Replace("-", "_"),
						dlForgeVersionEntry.iteratorMap,
						dlForgeVersionEntry._ComposerMap,
						dlForgeVersionEntry.AssetMapper()
					});
					list2.Add(new ModNet.NetFile(new string[]
					{
						string.Format("https://bmclapi2.bangbang93.com/maven/net/minecraftforge/forge/{0}", arg),
						string.Format("https://files.minecraftforge.net/maven/net/minecraftforge/forge/{0}", arg)
					}, CS$<>8__locals1.$VB$Local_InstallerAddress, new ModBase.FileChecker(65536L, -1L, dlForgeVersionEntry.threadMap, true, false), false));
				}
				Task.Output = list2;
			}, null, ThreadPriority.Normal)
			{
				ProgressWeight = 0.5,
				Show = false
			});
			list.Add(new ModNet.LoaderDownload(string.Format("下载 {0} 主文件", CS$<>8__locals1.$VB$Local_LoaderName), new List<ModNet.NetFile>())
			{
				ProgressWeight = 9.0
			});
			if (!CS$<>8__locals1.$VB$Local_IsNeoForge && Conversions.ToDouble(CS$<>8__locals1.$VB$Local_LoaderVersion.BeforeFirst(".", false)) < 20.0)
			{
				ModBase.Log("[Download] 检测为非新版 Forge：" + CS$<>8__locals1.$VB$Local_LoaderVersion, ModBase.LogLevel.Normal, "出现错误");
				list.Add(new ModLoader.LoaderTask<List<ModNet.NetFile>, bool>(string.Format("安装 {0}（方式 B）", CS$<>8__locals1.$VB$Local_LoaderName), delegate(ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task)
				{
					ZipArchive zipArchive = null;
					try
					{
						zipArchive = new ZipArchive(new FileStream(CS$<>8__locals1.$VB$Local_InstallerAddress, FileMode.Open));
						Task.Progress = 0.2;
						JObject jobject = (JObject)ModBase.GetJson(ModBase.ReadFile(zipArchive.GetEntry("install_profile.json").Open(), null));
						Task.Progress = 0.4;
						Directory.CreateDirectory(CS$<>8__locals1.$VB$Local_VersionFolder);
						Task.Progress = 0.5;
						if (jobject["install"] == null)
						{
							ModBase.Log("[Download] 开始进行 Forge 安装，Legacy 方式 1：" + CS$<>8__locals1.$VB$Local_InstallerAddress, ModBase.LogLevel.Normal, "出现错误");
							JObject jobject2 = (JObject)ModBase.GetJson(ModBase.ReadFile(zipArchive.GetEntry(jobject["json"].ToString().TrimStart(new char[]
							{
								'/'
							})).Open(), null));
							jobject2["id"] = CS$<>8__locals1.$VB$Local_TargetVersion;
							ModBase.WriteFile(CS$<>8__locals1.$VB$Local_VersionFolder + CS$<>8__locals1.$VB$Local_TargetVersion + ".json", jobject2.ToString(), false, null);
							Task.Progress = 0.6;
							zipArchive.Dispose();
							ModBase.ExtractFile(CS$<>8__locals1.$VB$Local_InstallerAddress, CS$<>8__locals1.$VB$Local_InstallerAddress + "_unrar\\", null, null);
							ModBase.CopyDirectory(CS$<>8__locals1.$VB$Local_InstallerAddress + "_unrar\\maven\\", CS$<>8__locals1.$VB$Local_McFolder + "libraries\\", null);
							ModBase.DeleteDirectory(CS$<>8__locals1.$VB$Local_InstallerAddress + "_unrar\\", false);
						}
						else
						{
							ModBase.Log("[Download] 开始进行 Forge 安装，Legacy 方式 2：" + CS$<>8__locals1.$VB$Local_InstallerAddress, ModBase.LogLevel.Normal, "出现错误");
							string text = ModMinecraft.McLibGet((string)jobject["install"]["path"], true, false, CS$<>8__locals1.$VB$Local_McFolder);
							if (File.Exists(text))
							{
								File.Delete(text);
							}
							ModBase.WriteFile(text, zipArchive.GetEntry((string)jobject["install"]["filePath"]).Open());
							Task.Progress = 0.9;
							jobject["versionInfo"]["id"] = CS$<>8__locals1.$VB$Local_TargetVersion;
							if (jobject["versionInfo"]["inheritsFrom"] == null)
							{
								((JObject)jobject["versionInfo"]).Add("inheritsFrom", CS$<>8__locals1.$VB$Local_Inherit);
							}
							ModBase.WriteFile(CS$<>8__locals1.$VB$Local_VersionFolder + CS$<>8__locals1.$VB$Local_TargetVersion + ".json", jobject["versionInfo"].ToString(), false, null);
						}
					}
					catch (Exception innerException)
					{
						throw new Exception("非新版方式安装 Forge 失败", innerException);
					}
					finally
					{
						try
						{
							if (zipArchive != null)
							{
								zipArchive.Dispose();
							}
							if (File.Exists(CS$<>8__locals1.$VB$Local_InstallerAddress))
							{
								File.Delete(CS$<>8__locals1.$VB$Local_InstallerAddress);
							}
							if (Directory.Exists(CS$<>8__locals1.$VB$Local_InstallerAddress + "_unrar\\"))
							{
								ModBase.DeleteDirectory(CS$<>8__locals1.$VB$Local_InstallerAddress + "_unrar\\", false);
							}
						}
						catch (Exception ex)
						{
							ModBase.Log(ex, "非新版方式安装 Forge 清理文件时出错", ModBase.LogLevel.Debug, "出现错误");
						}
					}
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = 1.0
				});
			}
			else
			{
				ModDownloadLib._Closure$__35-1 CS$<>8__locals2 = new ModDownloadLib._Closure$__35-1(CS$<>8__locals2);
				CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2 = CS$<>8__locals1;
				ModBase.Log(string.Format("[Download] 检测为{0}Forge：", CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_IsNeoForge ? " Neo" : "新版 ") + CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_LoaderVersion, ModBase.LogLevel.Normal, "出现错误");
				CS$<>8__locals2.$VB$Local_Libs = null;
				list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>(string.Format("分析 {0} 支持库文件", CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_LoaderName), checked(delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
				{
					Task.Output = new List<ModNet.NetFile>();
					ZipArchive zipArchive = null;
					try
					{
						zipArchive = new ZipArchive(new FileStream(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_InstallerAddress, FileMode.Open));
						Task.Progress = 0.2;
						JObject jobject = (JObject)ModBase.GetJson(ModBase.ReadFile(zipArchive.GetEntry("install_profile.json").Open(), null));
						JObject jobject2 = (JObject)ModBase.GetJson(ModBase.ReadFile(zipArchive.GetEntry("version.json").Open(), null));
						jobject.Merge(jobject2);
						CS$<>8__locals2.$VB$Local_Libs = ModMinecraft.McLibListGetWithJson(jobject, true, null, null);
						if (jobject["data"] != null && jobject["data"]["MOJMAPS"] != null)
						{
							Task.Progress = 0.4;
							JObject jobject3 = (JObject)ModBase.GetJson(ModNet.NetGetCodeByDownload(ModDownload.DlSourceLauncherOrMetaGet(Conversions.ToString(ModDownload.DlClientListGet(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Inherit))), 45000, true, false));
							string text = jobject["data"]["MOJMAPS"]["client"].ToString().Trim("[]".ToCharArray()).BeforeFirst("@", false);
							string wrapperMap = ModMinecraft.McLibGet(text, true, false, null).Replace(".jar", "-mappings." + jobject["data"]["MOJMAPS"]["client"].ToString().Trim("[]".ToCharArray()).Split("@")[1]);
							JToken jtoken = jobject3["downloads"]["client_mappings"];
							List<ModMinecraft.McLibToken> $VB$Local_Libs = CS$<>8__locals2.$VB$Local_Libs;
							ModMinecraft.McLibToken mcLibToken = new ModMinecraft.McLibToken();
							mcLibToken.m_InfoMap = false;
							mcLibToken.attributeMap = false;
							mcLibToken._WrapperMap = wrapperMap;
							mcLibToken._AnnotationMap = text;
							mcLibToken.ForgotThread((string)jtoken["url"]);
							mcLibToken.m_BaseMap = (long)jtoken["size"];
							mcLibToken.codeMap = (string)jtoken["sha1"];
							$VB$Local_Libs.Add(mcLibToken);
							ModBase.Log(string.Format("[Download] 需要下载 Mappings：{0} (SHA1: {1})", jtoken["url"], jtoken["sha1"]), ModBase.LogLevel.Normal, "出现错误");
						}
						Task.Progress = 0.8;
						int num = CS$<>8__locals2.$VB$Local_Libs.Count - 1;
						for (int i = 0; i <= num; i++)
						{
							if (CS$<>8__locals2.$VB$Local_Libs[i]._WrapperMap.EndsWithF(string.Format("{0}-{1}-{2}.jar", CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_LoaderName.ToLower(), CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Inherit, CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_LoaderVersion), false) || CS$<>8__locals2.$VB$Local_Libs[i]._WrapperMap.EndsWithF(string.Format("{0}-{1}-{2}-client.jar", CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_LoaderName.ToLower(), CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Inherit, CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_LoaderVersion), false))
							{
								ModBase.Log(string.Format("[Download] 已从待下载 {0} 支持库中移除：", CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_LoaderName) + CS$<>8__locals2.$VB$Local_Libs[i]._WrapperMap, ModBase.LogLevel.Debug, "出现错误");
								CS$<>8__locals2.$VB$Local_Libs.RemoveAt(i);
								IL_356:
								Task.Output = ModMinecraft.McLibFixFromLibToken(CS$<>8__locals2.$VB$Local_Libs, ModMinecraft.m_ProxyTests, null);
								return;
							}
						}
						goto IL_356;
					}
					catch (Exception innerException)
					{
						throw new Exception(string.Format("获取{0}Forge 支持库列表失败", CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_IsNeoForge ? " Neo" : "新版 "), innerException);
					}
					finally
					{
						if (zipArchive != null)
						{
							zipArchive.Dispose();
						}
					}
				}), null, ThreadPriority.Normal)
				{
					ProgressWeight = 2.0
				});
				list.Add(new ModNet.LoaderDownload(string.Format("下载 {0} 支持库文件", CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_LoaderName), new List<ModNet.NetFile>())
				{
					ProgressWeight = 12.0
				});
				list.Add(new ModLoader.LoaderTask<List<ModNet.NetFile>, bool>(string.Format("获取 {0} 支持库文件", CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_LoaderName), delegate(ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task)
				{
					if (CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_IsCustomFolder)
					{
						try
						{
							foreach (ModMinecraft.McLibToken mcLibToken in CS$<>8__locals2.$VB$Local_Libs)
							{
								string text = mcLibToken._WrapperMap.Replace(ModMinecraft.m_ProxyTests, CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_McFolder);
								if (!File.Exists(text))
								{
									Directory.CreateDirectory(Path.GetDirectoryName(text));
									ModBase.CopyFile(mcLibToken._WrapperMap, text);
								}
								if (ModBase._TokenRepository)
								{
									ModBase.Log(string.Format("[Download] 复制的 {0} 支持库文件：", CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_LoaderName) + mcLibToken._WrapperMap, ModBase.LogLevel.Normal, "出现错误");
								}
							}
						}
						finally
						{
							List<ModMinecraft.McLibToken>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
					if (CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_ClientDownloadLoader != null)
					{
						List<ModLoader.LoaderBase> list2 = Enumerable.ToList<ModLoader.LoaderBase>(Enumerable.Where<ModLoader.LoaderBase>(Enumerable.Where<ModLoader.LoaderBase>(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_ClientDownloadLoader.GetLoaderList(true), (ModDownloadLib._Closure$__.$I35-4 == null) ? (ModDownloadLib._Closure$__.$I35-4 = ((ModLoader.LoaderBase l) => Operators.CompareString(l.Name, "下载原版支持库文件", false) == 0 || Operators.CompareString(l.Name, "下载原版 json 文件", false) == 0)) : ModDownloadLib._Closure$__.$I35-4), (ModDownloadLib._Closure$__.$I35-5 == null) ? (ModDownloadLib._Closure$__.$I35-5 = ((ModLoader.LoaderBase l) => l.State != ModBase.LoadState.Finished)) : ModDownloadLib._Closure$__.$I35-5));
						if (Enumerable.Any<ModLoader.LoaderBase>(list2))
						{
							ModBase.Log(string.Format("[Download] {0} 安装正在等待原版文件下载完成", CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_LoaderName), ModBase.LogLevel.Normal, "出现错误");
						}
						while (Enumerable.Any<ModLoader.LoaderBase>(list2) && !Task.IsAborted)
						{
							list2 = Enumerable.ToList<ModLoader.LoaderBase>(Enumerable.Where<ModLoader.LoaderBase>(list2, (ModDownloadLib._Closure$__.$I35-6 == null) ? (ModDownloadLib._Closure$__.$I35-6 = ((ModLoader.LoaderBase l) => l.State != ModBase.LoadState.Finished)) : ModDownloadLib._Closure$__.$I35-6));
							Thread.Sleep(50);
						}
						if (!Task.IsAborted && CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_IsCustomFolder)
						{
							object obj = ModDownloadLib.roleField;
							ObjectFlowControl.CheckForSyncLockOnValueType(obj);
							lock (obj)
							{
								string folderNameFromPath = ModBase.GetFolderNameFromPath(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_ClientFolder);
								Directory.CreateDirectory(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_McFolder + "versions\\" + CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Inherit);
								if (!File.Exists(string.Concat(new string[]
								{
									CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_McFolder,
									"versions\\",
									CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Inherit,
									"\\",
									CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Inherit,
									".json"
								})))
								{
									ModBase.CopyFile(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_ClientFolder + folderNameFromPath + ".json", string.Concat(new string[]
									{
										CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_McFolder,
										"versions\\",
										CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Inherit,
										"\\",
										CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Inherit,
										".json"
									}));
								}
								if (!File.Exists(string.Concat(new string[]
								{
									CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_McFolder,
									"versions\\",
									CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Inherit,
									"\\",
									CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Inherit,
									".jar"
								})))
								{
									ModBase.CopyFile(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_ClientFolder + folderNameFromPath + ".jar", string.Concat(new string[]
									{
										CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_McFolder,
										"versions\\",
										CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Inherit,
										"\\",
										CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Inherit,
										".jar"
									}));
								}
							}
						}
					}
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = 0.1,
					Show = false
				});
				list.Add(new ModLoader.LoaderTask<bool, bool>(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_IsNeoForge ? "安装 NeoForge" : "安装 Forge（方式 A）", delegate(ModLoader.LoaderTask<bool, bool> Task)
				{
					ZipArchive zipArchive = null;
					try
					{
						ModDownloadLib._Closure$__35-2 CS$<>8__locals3 = new ModDownloadLib._Closure$__35-2(CS$<>8__locals3);
						ModBase.Log("[Download] 开始进行 Forgelike 安装：" + CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_InstallerAddress, ModBase.LogLevel.Normal, "出现错误");
						CS$<>8__locals3.$VB$Local_OldList = Enumerable.ToList<string>(Enumerable.Select<DirectoryInfo, string>(new DirectoryInfo(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_McFolder + "versions\\").EnumerateDirectories(), (ModDownloadLib._Closure$__.$I35-8 == null) ? (ModDownloadLib._Closure$__.$I35-8 = ((DirectoryInfo i) => i.FullName)) : ModDownloadLib._Closure$__.$I35-8));
						zipArchive = new ZipArchive(new FileStream(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_InstallerAddress, FileMode.Open));
						JObject jobject = (JObject)ModBase.GetJson(ModBase.ReadFile(zipArchive.GetEntry("install_profile.json").Open(), null));
						Directory.CreateDirectory(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_VersionFolder);
						Task.Progress = 0.04;
						ModMinecraft.McFolderLauncherProfilesJsonCreate(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_McFolder);
						Task.Progress = 0.05;
						bool flag = true;
						for (;;)
						{
							try
							{
								ModBase.WriteFile(ModBase.m_DecoratorRepository + "Cache\\forge_installer.jar", ModBase.GetResources("ForgeInstaller"), false);
								Task.Progress = 0.06;
								ModDownloadLib.ForgelikeInjector(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_InstallerAddress, Task, CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_McFolder, flag, CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_IsNeoForge);
								Task.Progress = 0.97;
								break;
							}
							catch (Exception ex)
							{
								if (!flag)
								{
									throw new Exception(string.Format("运行 {0} 安装器失败", CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_LoaderName), ex);
								}
								ModBase.Log(ex, string.Format("使用 JavaWrapper 安装 {0} 失败，将不使用 JavaWrapper 并重试", CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_LoaderName), ModBase.LogLevel.Debug, "出现错误");
								flag = false;
							}
						}
						List<DirectoryInfo> list2 = Enumerable.ToList<DirectoryInfo>(Enumerable.SkipWhile<DirectoryInfo>(new DirectoryInfo(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_McFolder + "versions\\").EnumerateDirectories(), (DirectoryInfo i) => CS$<>8__locals3.$VB$Local_OldList.Contains(i.FullName)));
						if (list2.Count > 1)
						{
							list2 = Enumerable.ToList<DirectoryInfo>(Enumerable.Where<DirectoryInfo>(list2, (ModDownloadLib._Closure$__.$I35-10 == null) ? (ModDownloadLib._Closure$__.$I35-10 = ((DirectoryInfo l) => l.Name.ContainsF("forge", true) && Enumerable.Any<FileInfo>(l.EnumerateFiles()))) : ModDownloadLib._Closure$__.$I35-10));
						}
						if (list2.Count == 1)
						{
							FileInfo fileInfo = Enumerable.First<FileInfo>(list2[0].EnumerateFiles());
							ModBase.WriteFile(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_VersionFolder + CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_TargetVersion + ".json", ModBase.ReadFile(fileInfo.FullName, null), false, null);
							ModBase.Log(string.Format("[Download] 已拷贝新增的版本 JSON 文件：{0} -> {1}{2}.json", fileInfo.FullName, CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_VersionFolder, CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_TargetVersion), ModBase.LogLevel.Normal, "出现错误");
						}
						else if (list2.Count > 1)
						{
							ModBase.Log(string.Format("[Download] 有多个疑似的新增版本，无法确定：{0}", Enumerable.Select<DirectoryInfo, string>(list2, (ModDownloadLib._Closure$__.$I35-11 == null) ? (ModDownloadLib._Closure$__.$I35-11 = ((DirectoryInfo d) => d.Name)) : ModDownloadLib._Closure$__.$I35-11).Join(";")), ModBase.LogLevel.Normal, "出现错误");
						}
						else
						{
							ModBase.Log("[Download] 未找到新增的版本文件夹", ModBase.LogLevel.Normal, "出现错误");
						}
					}
					catch (Exception innerException)
					{
						throw new Exception(string.Format("安装新 {0} 版本失败", CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_LoaderName), innerException);
					}
					finally
					{
						try
						{
							if (zipArchive != null)
							{
								zipArchive.Dispose();
							}
							if (File.Exists(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_InstallerAddress))
							{
								File.Delete(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_InstallerAddress);
							}
						}
						catch (Exception ex2)
						{
							ModBase.Log(ex2, string.Format("安装 {0} 清理文件时出错", CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_LoaderName), ModBase.LogLevel.Debug, "出现错误");
						}
					}
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = 10.0
				});
			}
			return list;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00037F5C File Offset: 0x0003615C
		public static void ForgeDownloadListItemPreload(StackPanel Stack, List<ModDownload.DlForgeVersionEntry> Entries, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
		{
			ModDownload.DlForgeVersionEntry dlForgeVersionEntry = null;
			if (Enumerable.Any<ModDownload.DlForgeVersionEntry>(Entries))
			{
				dlForgeVersionEntry = Entries[0];
			}
			else
			{
				ModBase.Log("[System] 未找到可用的 Forge 版本", ModBase.LogLevel.Debug, "出现错误");
			}
			ModDownload.DlForgeVersionEntry dlForgeVersionEntry2 = null;
			try
			{
				foreach (ModDownload.DlForgeVersionEntry dlForgeVersionEntry3 in Entries)
				{
					if (dlForgeVersionEntry3.propertyMap)
					{
						dlForgeVersionEntry2 = dlForgeVersionEntry3;
					}
				}
			}
			finally
			{
				List<ModDownload.DlForgeVersionEntry>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			if (dlForgeVersionEntry != null && dlForgeVersionEntry == dlForgeVersionEntry2)
			{
				dlForgeVersionEntry = null;
			}
			if (dlForgeVersionEntry2 != null)
			{
				MyListItem myListItem = ModDownloadLib.ForgeDownloadListItem(dlForgeVersionEntry2, OnClick, IsSaveOnly);
				myListItem.Info = "推荐版" + ((Operators.CompareString(myListItem.Info, "", false) == 0) ? "" : ("，" + myListItem.Info));
				Stack.Children.Add(myListItem);
			}
			if (dlForgeVersionEntry != null)
			{
				MyListItem myListItem2 = ModDownloadLib.ForgeDownloadListItem(dlForgeVersionEntry, OnClick, IsSaveOnly);
				myListItem2.Info = "最新版" + ((Operators.CompareString(myListItem2.Info, "", false) == 0) ? "" : ("，" + myListItem2.Info));
				Stack.Children.Add(myListItem2);
			}
			Stack.Children.Add(new TextBlock
			{
				Text = "全部版本 (" + Conversions.ToString(Entries.Count) + ")",
				HorizontalAlignment = HorizontalAlignment.Left,
				Margin = new Thickness(6.0, 13.0, 0.0, 4.0)
			});
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000380F4 File Offset: 0x000362F4
		public static MyListItem ForgeDownloadListItem(ModDownload.DlForgeVersionEntry Entry, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
		{
			MyListItem myListItem = new MyListItem
			{
				Title = Entry.m_ConfigMap,
				SnapsToDevicePixels = true,
				Height = 42.0,
				Type = MyListItem.CheckType.Clickable,
				Tag = Entry,
				Info = Enumerable.Where<string>(new string[]
				{
					(Operators.CompareString(Entry.mapperMap, "", false) == 0) ? "" : ("发布于 " + Entry.mapperMap),
					ModBase._TokenRepository ? ("种类：" + Entry._ComposerMap) : ""
				}, (ModDownloadLib._Closure$__.$I37-0 == null) ? (ModDownloadLib._Closure$__.$I37-0 = ((string d) => Operators.CompareString(d, "", false) != 0)) : ModDownloadLib._Closure$__.$I37-0).Join("，"),
				Logo = ModBase.m_SerializerRepository + "Blocks/Anvil.png"
			};
			myListItem.Click += OnClick;
			if (IsSaveOnly)
			{
				myListItem.tokenComposer = new Action<MyListItem, EventArgs>(ModDownloadLib.ForgeSaveContMenuBuild);
			}
			else
			{
				myListItem.tokenComposer = new Action<MyListItem, EventArgs>(ModDownloadLib.ForgeContMenuBuild);
			}
			return myListItem;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0003820C File Offset: 0x0003640C
		private static void ForgeContMenuBuild(MyListItem sender, EventArgs e)
		{
			MyIconButton myIconButton = new MyIconButton
			{
				Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
				ToolTip = "另存为"
			};
			ToolTipService.SetPlacement(myIconButton, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton, 2.0);
			myIconButton.Click += ((ModDownloadLib._Closure$__.$IR38-17 == null) ? (ModDownloadLib._Closure$__.$IR38-17 = delegate(object sender, EventArgs e)
			{
				ModDownloadLib.ForgeSave_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
			}) : ModDownloadLib._Closure$__.$IR38-17);
			MyIconButton myIconButton2 = new MyIconButton
			{
				LogoScale = 1.05,
				Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
				ToolTip = "更新日志"
			};
			ToolTipService.SetPlacement(myIconButton2, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton2, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton2, 2.0);
			myIconButton2.Click += ((ModDownloadLib._Closure$__.$IR38-18 == null) ? (ModDownloadLib._Closure$__.$IR38-18 = delegate(object sender, EventArgs e)
			{
				ModDownloadLib.ForgeLog_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
			}) : ModDownloadLib._Closure$__.$IR38-18);
			sender2.Buttons = new MyIconButton[]
			{
				myIconButton,
				myIconButton2
			};
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00038314 File Offset: 0x00036514
		private static void ForgeSaveContMenuBuild(MyListItem sender, EventArgs e)
		{
			MyIconButton myIconButton = new MyIconButton
			{
				LogoScale = 1.05,
				Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
				ToolTip = "更新日志"
			};
			ToolTipService.SetPlacement(myIconButton, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton, 2.0);
			myIconButton.Click += ((ModDownloadLib._Closure$__.$IR39-19 == null) ? (ModDownloadLib._Closure$__.$IR39-19 = delegate(object sender, EventArgs e)
			{
				ModDownloadLib.ForgeLog_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
			}) : ModDownloadLib._Closure$__.$IR39-19);
			sender2.Buttons = new MyIconButton[]
			{
				myIconButton
			};
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000383AC File Offset: 0x000365AC
		private static void ForgeLog_Click(object sender, RoutedEventArgs e)
		{
			ModDownload.DlForgeVersionEntry dlForgeVersionEntry;
			if (NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null)
			{
				dlForgeVersionEntry = (ModDownload.DlForgeVersionEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
			}
			else if (NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) != null)
			{
				dlForgeVersionEntry = (ModDownload.DlForgeVersionEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
			}
			else
			{
				dlForgeVersionEntry = (ModDownload.DlForgeVersionEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
			}
			ModBase.OpenWebsite(string.Format("https://files.minecraftforge.net/maven/net/minecraftforge/forge/{0}-{1}/forge-{2}-{3}-changelog.txt", new object[]
			{
				dlForgeVersionEntry._TestsMap,
				dlForgeVersionEntry.m_ConfigMap,
				dlForgeVersionEntry._TestsMap,
				dlForgeVersionEntry.m_ConfigMap
			}));
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x000384C8 File Offset: 0x000366C8
		public static void ForgeSave_Click(object sender, RoutedEventArgs e)
		{
			ModDownload.DlForgeVersionEntry info;
			if (NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null)
			{
				info = (ModDownload.DlForgeVersionEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
			}
			else if (NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) != null)
			{
				info = (ModDownload.DlForgeVersionEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
			}
			else
			{
				info = (ModDownload.DlForgeVersionEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
			}
			ModDownloadLib.McDownloadForgelikeSave(info);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00005191 File Offset: 0x00003391
		public static void McDownloadForgeRecommendedRefresh()
		{
			if (!ModDownloadLib.printerField)
			{
				ModDownloadLib.printerField = true;
				ModBase.RunInNewThread((ModDownloadLib._Closure$__.$I42-0 == null) ? (ModDownloadLib._Closure$__.$I42-0 = delegate()
				{
					try
					{
						ModBase.Log("[Download] 刷新 Forge 推荐版本缓存开始", ModBase.LogLevel.Normal, "出现错误");
						string text = ModNet.NetGetCodeByDownload("https://bmclapi2.bangbang93.com/forge/promos", 45000, false, false);
						if (text.Length < 1000)
						{
							throw new Exception("获取的结果过短（" + text + "）");
						}
						JContainer jcontainer = (JContainer)ModBase.GetJson(text);
						List<string> list = new List<string>();
						try
						{
							foreach (JToken jtoken in jcontainer)
							{
								JObject jobject = (JObject)jtoken;
								if (jobject["name"] != null && jobject["build"] != null)
								{
									string text2 = (string)jobject["name"];
									if (text2.EndsWithF("-recommended", false))
									{
										list.Add("\"" + text2.Replace("-recommended", "\":\"" + jobject["build"]["version"].ToString() + "\""));
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
						if (list.Count < 5)
						{
							throw new Exception("获取的推荐版本数过少（" + text + "）");
						}
						string text3 = "{" + list.Join(",") + "}";
						ModBase.WriteFile(ModBase.m_DecoratorRepository + "Cache\\ForgeRecommendedList.json", text3, false, null);
						ModBase.Log("[Download] 刷新 Forge 推荐版本缓存成功", ModBase.LogLevel.Normal, "出现错误");
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "刷新 Forge 推荐版本缓存失败", ModBase.LogLevel.Debug, "出现错误");
					}
				}) : ModDownloadLib._Closure$__.$I42-0, "ForgeRecommendedRefresh", ThreadPriority.Normal);
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x000385B4 File Offset: 0x000367B4
		public static string McDownloadForgeRecommendedGet(string McVersion)
		{
			string result;
			try
			{
				if (McVersion == null)
				{
					result = null;
				}
				else
				{
					string text = ModBase.ReadFile(ModBase.m_DecoratorRepository + "Cache\\ForgeRecommendedList.json", null);
					if (text != null && Operators.CompareString(text, "", false) != 0)
					{
						JObject jobject = (JObject)ModBase.GetJson(text);
						if (jobject != null && (McVersion ?? "null").Contains(".") && jobject.ContainsKey(McVersion))
						{
							result = (jobject[McVersion] ?? "").ToString();
						}
						else
						{
							result = null;
						}
					}
					else
					{
						ModBase.Log("[Download] 没有 Forge 推荐版本缓存文件", ModBase.LogLevel.Normal, "出现错误");
						result = null;
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "获取 Forge 推荐版本失败（" + (McVersion ?? "null") + "）", ModBase.LogLevel.Feedback, "出现错误");
				result = null;
			}
			return result;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0003869C File Offset: 0x0003689C
		public static void NeoForgeDownloadListItemPreload(StackPanel Stack, List<ModDownload.DlNeoForgeListEntry> Entries, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
		{
			ModDownload.DlNeoForgeListEntry dlNeoForgeListEntry = null;
			ModDownload.DlNeoForgeListEntry dlNeoForgeListEntry2 = null;
			if (Enumerable.Any<ModDownload.DlNeoForgeListEntry>(Entries))
			{
				try
				{
					foreach (ModDownload.DlNeoForgeListEntry dlNeoForgeListEntry3 in Enumerable.ToList<ModDownload.DlNeoForgeListEntry>(Entries))
					{
						if (!dlNeoForgeListEntry3._MapMap)
						{
							dlNeoForgeListEntry = dlNeoForgeListEntry3;
							break;
						}
						if (dlNeoForgeListEntry2 == null)
						{
							dlNeoForgeListEntry2 = dlNeoForgeListEntry3;
						}
					}
					goto IL_5C;
				}
				finally
				{
					List<ModDownload.DlNeoForgeListEntry>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
			ModBase.Log("[System] 未找到可用的 NeoForge 版本", ModBase.LogLevel.Debug, "出现错误");
			IL_5C:
			if (dlNeoForgeListEntry != null)
			{
				MyListItem myListItem = ModDownloadLib.NeoForgeDownloadListItem(dlNeoForgeListEntry, OnClick, IsSaveOnly);
				myListItem.Info = ((Operators.CompareString(myListItem.Info, "", false) == 0) ? "最新稳定版" : ("最新" + myListItem.Info));
				Stack.Children.Add(myListItem);
			}
			if (dlNeoForgeListEntry2 != null)
			{
				MyListItem myListItem2 = ModDownloadLib.NeoForgeDownloadListItem(dlNeoForgeListEntry2, OnClick, IsSaveOnly);
				myListItem2.Info = ((Operators.CompareString(myListItem2.Info, "", false) == 0) ? "最新测试版" : ("最新" + myListItem2.Info));
				Stack.Children.Add(myListItem2);
			}
			Stack.Children.Add(new TextBlock
			{
				Text = "全部版本 (" + Conversions.ToString(Entries.Count) + ")",
				HorizontalAlignment = HorizontalAlignment.Left,
				Margin = new Thickness(6.0, 13.0, 0.0, 4.0)
			});
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00038818 File Offset: 0x00036A18
		public static MyListItem NeoForgeDownloadListItem(ModDownload.DlNeoForgeListEntry Info, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
		{
			MyListItem myListItem = new MyListItem
			{
				Title = Info.m_ConfigMap,
				SnapsToDevicePixels = true,
				Height = 42.0,
				Type = MyListItem.CheckType.Clickable,
				Tag = Info,
				Info = (Info._MapMap ? "测试版" : "稳定版"),
				Logo = ModBase.m_SerializerRepository + "Blocks/NeoForge.png"
			};
			myListItem.Click += OnClick;
			if (IsSaveOnly)
			{
				myListItem.tokenComposer = new Action<MyListItem, EventArgs>(ModDownloadLib.NeoForgeSaveContMenuBuild);
			}
			else
			{
				myListItem.tokenComposer = new Action<MyListItem, EventArgs>(ModDownloadLib.NeoForgeContMenuBuild);
			}
			return myListItem;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000388BC File Offset: 0x00036ABC
		private static void NeoForgeContMenuBuild(MyListItem sender, EventArgs e)
		{
			MyIconButton myIconButton = new MyIconButton
			{
				Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
				ToolTip = "另存为"
			};
			ToolTipService.SetPlacement(myIconButton, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton, 2.0);
			myIconButton.Click += ((ModDownloadLib._Closure$__.$IR47-20 == null) ? (ModDownloadLib._Closure$__.$IR47-20 = delegate(object sender, EventArgs e)
			{
				ModDownloadLib.NeoForgeSave_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
			}) : ModDownloadLib._Closure$__.$IR47-20);
			MyIconButton myIconButton2 = new MyIconButton
			{
				LogoScale = 1.05,
				Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
				ToolTip = "更新日志"
			};
			ToolTipService.SetPlacement(myIconButton2, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton2, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton2, 2.0);
			myIconButton2.Click += ((ModDownloadLib._Closure$__.$IR47-21 == null) ? (ModDownloadLib._Closure$__.$IR47-21 = delegate(object sender, EventArgs e)
			{
				ModDownloadLib.NeoForgeLog_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
			}) : ModDownloadLib._Closure$__.$IR47-21);
			sender2.Buttons = new MyIconButton[]
			{
				myIconButton,
				myIconButton2
			};
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x000389C4 File Offset: 0x00036BC4
		private static void NeoForgeSaveContMenuBuild(MyListItem sender, EventArgs e)
		{
			MyIconButton myIconButton = new MyIconButton
			{
				LogoScale = 1.05,
				Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
				ToolTip = "更新日志"
			};
			ToolTipService.SetPlacement(myIconButton, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton, 2.0);
			myIconButton.Click += ((ModDownloadLib._Closure$__.$IR48-22 == null) ? (ModDownloadLib._Closure$__.$IR48-22 = delegate(object sender, EventArgs e)
			{
				ModDownloadLib.NeoForgeLog_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
			}) : ModDownloadLib._Closure$__.$IR48-22);
			sender2.Buttons = new MyIconButton[]
			{
				myIconButton
			};
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00038A5C File Offset: 0x00036C5C
		private static void NeoForgeLog_Click(object sender, RoutedEventArgs e)
		{
			ModDownload.DlNeoForgeListEntry dlNeoForgeListEntry;
			if (NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null)
			{
				dlNeoForgeListEntry = (ModDownload.DlNeoForgeListEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
			}
			else if (NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) != null)
			{
				dlNeoForgeListEntry = (ModDownload.DlNeoForgeListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
			}
			else
			{
				dlNeoForgeListEntry = (ModDownload.DlNeoForgeListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
			}
			ModBase.OpenWebsite(dlNeoForgeListEntry.RateMapper() + "-changelog.txt");
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00038B54 File Offset: 0x00036D54
		public static void NeoForgeSave_Click(object sender, RoutedEventArgs e)
		{
			ModDownload.DlNeoForgeListEntry info;
			if (NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null)
			{
				info = (ModDownload.DlNeoForgeListEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
			}
			else if (NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) != null)
			{
				info = (ModDownload.DlNeoForgeListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
			}
			else
			{
				info = (ModDownload.DlNeoForgeListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
			}
			ModDownloadLib.McDownloadForgelikeSave(info);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00038C40 File Offset: 0x00036E40
		public static void McDownloadFabricLoaderSave(JObject DownloadInfo)
		{
			try
			{
				string text = DownloadInfo["url"].ToString();
				string fileNameFromPath = ModBase.GetFileNameFromPath(text);
				string fileNameFromPath2 = ModBase.GetFileNameFromPath(DownloadInfo["version"].ToString());
				string text2 = ModBase.SelectAs("选择保存位置", fileNameFromPath, "Fabric 安装器 (*.jar)|*.jar", null);
				if (text2.Contains("\\"))
				{
					try
					{
						List<ModLoader.LoaderBase>.Enumerator enumerator = Enumerable.ToList<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar).GetEnumerator();
						while (enumerator.MoveNext())
						{
							if (Operators.CompareString(enumerator.Current.Name, string.Format("Fabric {0} 安装器下载", fileNameFromPath2), false) == 0)
							{
								ModMain.Hint("该版本正在下载中！", ModMain.HintType.Critical, true);
								return;
							}
						}
					}
					finally
					{
						List<ModLoader.LoaderBase>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
					List<string> list2 = new List<string>();
					list2.Add(text);
					list.Add(new ModNet.LoaderDownload("下载主文件", new List<ModNet.NetFile>
					{
						new ModNet.NetFile(list2.ToArray(), text2, new ModBase.FileChecker(65536L, -1L, null, true, false), false)
					})
					{
						ProgressWeight = 15.0
					});
					ModLoader.LoaderCombo<JObject> loaderCombo = new ModLoader.LoaderCombo<JObject>("Fabric " + fileNameFromPath2 + " 安装器下载", list);
					loaderCombo.OnStateChanged = new Action<ModLoader.LoaderBase>(ModDownloadLib.DownloadStateSave);
					loaderCombo.Start(DownloadInfo, false);
					ModLoader.LoaderTaskbarAdd<JObject>(loaderCombo);
					ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
					ModMain._ProcessIterator.BtnExtraDownload.Ribble();
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "开始 Fabric 安装器下载失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00038E10 File Offset: 0x00037010
		private static List<ModLoader.LoaderBase> McDownloadFabricLoader(string FabricVersion, string MinecraftName, string McFolder = null, bool FixLibrary = true)
		{
			McFolder = (McFolder ?? ModMinecraft.m_ProxyTests);
			Operators.CompareString(McFolder, ModMinecraft.m_ProxyTests, false);
			string Id = "fabric-loader-" + FabricVersion + "-" + MinecraftName;
			string VersionFolder = McFolder + "versions\\" + Id + "\\";
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			MinecraftName = MinecraftName.Replace("∞", "infinite");
			list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("获取 Fabric 主文件下载地址", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
			{
				if (FixLibrary)
				{
					ModDownloadLib.McDownloadClient(ModNet.NetPreDownloadBehaviour.ExitWhileExistsOrDownloading, MinecraftName, null);
				}
				Task.Progress = 0.5;
				Task.Output = new List<ModNet.NetFile>
				{
					new ModNet.NetFile(new string[]
					{
						string.Concat(new string[]
						{
							"https://bmclapi2.bangbang93.com/fabric-meta/v2/versions/loader/",
							MinecraftName,
							"/",
							FabricVersion,
							"/profile/json"
						}),
						string.Concat(new string[]
						{
							"https://meta.fabricmc.net/v2/versions/loader/",
							MinecraftName,
							"/",
							FabricVersion,
							"/profile/json"
						})
					}, VersionFolder + Id + ".json", new ModBase.FileChecker(-1L, -1L, null, true, true), false)
				};
			}, null, ThreadPriority.Normal)
			{
				ProgressWeight = 0.5
			});
			list.Add(new ModNet.LoaderDownload("下载 Fabric 主文件", new List<ModNet.NetFile>())
			{
				ProgressWeight = 2.5
			});
			if (FixLibrary)
			{
				list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析 Fabric 支持库文件", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
				{
					Task.Output = ModMinecraft.McLibFix(new ModMinecraft.McVersion(VersionFolder));
				}, null, ThreadPriority.Normal)
				{
					ProgressWeight = 1.0,
					Show = false
				});
				list.Add(new ModNet.LoaderDownload("下载 Fabric 支持库文件", new List<ModNet.NetFile>())
				{
					ProgressWeight = 8.0
				});
			}
			return list;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00038F64 File Offset: 0x00037164
		public static MyListItem FabricDownloadListItem(JObject Entry, MyListItem.ClickEventHandler OnClick)
		{
			MyListItem myListItem = new MyListItem();
			myListItem.Title = Entry["version"].ToString().Replace("+build", "");
			myListItem.SnapsToDevicePixels = true;
			myListItem.Height = 42.0;
			myListItem.Type = MyListItem.CheckType.Clickable;
			myListItem.Tag = Entry;
			myListItem.Info = (Entry["stable"].ToObject<bool>() ? "稳定版" : "测试版");
			myListItem.Logo = ModBase.m_SerializerRepository + "Blocks/Fabric.png";
			myListItem.Click += OnClick;
			return myListItem;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00039000 File Offset: 0x00037200
		public static MyListItem FabricApiDownloadListItem(ModComp.CompFile Entry, MyListItem.ClickEventHandler OnClick)
		{
			MyListItem myListItem = new MyListItem();
			myListItem.Title = Entry._ProductRepository.Split("]")[1].Replace("Fabric API ", "").Replace(" build ", ".").BeforeFirst("+", false).Trim();
			myListItem.SnapsToDevicePixels = true;
			myListItem.Height = 42.0;
			myListItem.Type = MyListItem.CheckType.Clickable;
			myListItem.Tag = Entry;
			myListItem.Info = Entry.CountTests() + "，发布于 " + Entry._ListenerRepository.ToString("yyyy'/'MM'/'dd HH':'mm");
			myListItem.Logo = ModBase.m_SerializerRepository + "Blocks/Fabric.png";
			myListItem.Click += OnClick;
			return myListItem;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000390C4 File Offset: 0x000372C4
		public static MyListItem OptiFabricDownloadListItem(ModComp.CompFile Entry, MyListItem.ClickEventHandler OnClick)
		{
			MyListItem myListItem = new MyListItem
			{
				Title = Entry._ProductRepository.ToLower().Replace("optifabric-", "").Replace(".jar", "").Trim().TrimStart(new char[]
				{
					'v'
				}),
				SnapsToDevicePixels = true,
				Height = 42.0,
				Type = MyListItem.CheckType.Clickable,
				Tag = Entry,
				Info = Entry.CountTests() + "，发布于 " + Entry._ListenerRepository.ToString("yyyy'/'MM'/'dd HH':'mm"),
				Logo = ModBase.m_SerializerRepository + "Blocks/OptiFabric.png"
			};
			myListItem.Click += OnClick;
			return myListItem;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00039188 File Offset: 0x00037388
		public static void McInstallState(object Loader)
		{
			object left = NewLateBinding.LateGet(Loader, null, "State", new object[0], null, null, null);
			if (Operators.ConditionalCompareObjectEqual(left, ModBase.LoadState.Finished, false))
			{
				ModBase.WriteIni(ModMinecraft.m_ProxyTests + "PCL.ini", "VersionCache", "");
				ModMain.Hint(Conversions.ToString(Operators.ConcatenateObject(NewLateBinding.LateGet(Loader, null, "Name", new object[0], null, null, null), "成功！")), ModMain.HintType.Finish, true);
			}
			else if (Operators.ConditionalCompareObjectEqual(left, ModBase.LoadState.Failed, false))
			{
				ModMain.Hint(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(NewLateBinding.LateGet(Loader, null, "Name", new object[0], null, null, null), "失败："), ModBase.GetExceptionSummary((Exception)NewLateBinding.LateGet(Loader, null, "Error", new object[0], null, null, null)))), ModMain.HintType.Critical, true);
			}
			else if (Operators.ConditionalCompareObjectEqual(left, ModBase.LoadState.Aborted, false))
			{
				ModMain.Hint(Conversions.ToString(Operators.ConcatenateObject(NewLateBinding.LateGet(Loader, null, "Name", new object[0], null, null, null), "已取消！")), ModMain.HintType.Info, true);
			}
			else if (Operators.ConditionalCompareObjectEqual(left, ModBase.LoadState.Loading, false))
			{
				return;
			}
			ModDownloadLib.McInstallFailedClearFolder(RuntimeHelpers.GetObjectValue(Loader));
			ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", false);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000392D8 File Offset: 0x000374D8
		public static void DownloadStateSave(object Loader)
		{
			object left = NewLateBinding.LateGet(Loader, null, "State", new object[0], null, null, null);
			if (Operators.ConditionalCompareObjectEqual(left, ModBase.LoadState.Finished, false))
			{
				ModMain.Hint(Conversions.ToString(Operators.ConcatenateObject(NewLateBinding.LateGet(Loader, null, "Name", new object[0], null, null, null), "成功！")), ModMain.HintType.Finish, true);
				return;
			}
			if (Operators.ConditionalCompareObjectEqual(left, ModBase.LoadState.Failed, false))
			{
				ModMain.Hint(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(NewLateBinding.LateGet(Loader, null, "Name", new object[0], null, null, null), "失败："), ModBase.GetExceptionSummary((Exception)NewLateBinding.LateGet(Loader, null, "Error", new object[0], null, null, null)))), ModMain.HintType.Critical, true);
				return;
			}
			if (Operators.ConditionalCompareObjectEqual(left, ModBase.LoadState.Aborted, false))
			{
				ModMain.Hint(Conversions.ToString(Operators.ConcatenateObject(NewLateBinding.LateGet(Loader, null, "Name", new object[0], null, null, null), "已取消！")), ModMain.HintType.Info, true);
				return;
			}
			Operators.ConditionalCompareObjectEqual(left, ModBase.LoadState.Loading, false);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000393E0 File Offset: 0x000375E0
		public static void McInstallFailedClearFolder(object Loader)
		{
			try
			{
				Thread.Sleep(1000);
				if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(Loader, null, "State", new object[0], null, null, null), ModBase.LoadState.Failed, false) || Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(Loader, null, "State", new object[0], null, null, null), ModBase.LoadState.Aborted, false))
				{
					if (!Directory.Exists(Conversions.ToString(Operators.ConcatenateObject(NewLateBinding.LateGet(Loader, null, "Input", new object[0], null, null, null), "saves\\"))) && !Directory.Exists(Conversions.ToString(Operators.ConcatenateObject(NewLateBinding.LateGet(Loader, null, "Input", new object[0], null, null, null), "versions\\"))))
					{
						ModBase.Log(Conversions.ToString(Operators.ConcatenateObject("[Download] 由于下载失败或取消，清理版本文件夹：", NewLateBinding.LateGet(Loader, null, "Input", new object[0], null, null, null))), ModBase.LogLevel.Developer, "出现错误");
						ModBase.DeleteDirectory(Conversions.ToString(NewLateBinding.LateGet(Loader, null, "Input", new object[0], null, null, null)), false);
					}
					else
					{
						ModBase.Log(Conversions.ToString(Operators.ConcatenateObject("[Download] 由于版本已被独立启动，不清理版本文件夹：", NewLateBinding.LateGet(Loader, null, "Input", new object[0], null, null, null))), ModBase.LogLevel.Developer, "出现错误");
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "下载失败或取消后清理版本文件夹失败", ModBase.LogLevel.Debug, "出现错误");
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00039554 File Offset: 0x00037754
		public static bool McInstall(ModDownloadLib.McInstallRequest Request)
		{
			bool result;
			try
			{
				List<ModLoader.LoaderBase> list = ModDownloadLib.McInstallLoader(Request, false);
				if (list == null)
				{
					result = false;
				}
				else
				{
					ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(Request.m_SystemTest + " 安装", list);
					loaderCombo.OnStateChanged = new Action<ModLoader.LoaderBase>(ModDownloadLib.McInstallState);
					loaderCombo.Start(Request._ParamTest, false);
					ModLoader.LoaderTaskbarAdd<string>(loaderCombo);
					ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
					ModMain._ProcessIterator.BtnExtraDownload.Ribble();
					result = true;
				}
			}
			catch (ModBase.CancelledException ex)
			{
				result = false;
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, "开始合并安装失败", ModBase.LogLevel.Feedback, "出现错误");
				result = false;
			}
			return result;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00039620 File Offset: 0x00037820
		public static List<ModLoader.LoaderBase> McInstallLoader(ModDownloadLib.McInstallRequest Request, bool DontFixLibraries = false)
		{
			string text;
			if (ModBase.m_DecoratorRepository.Contains(" ") && (Request.rulesTest != null || Request.decoratorTest != null || Request.stateTest != null))
			{
				text = ModBase._RefRepository + "ProgramData\\PCL\\Install\\";
			}
			else
			{
				text = ModBase.m_DecoratorRepository + "Install\\";
			}
			try
			{
				if (!ModDownloadLib.valField)
				{
					ModDownloadLib.valField = true;
					ModBase.DeleteDirectory(text, true);
					ModBase.Log("[Download] 已清理合并安装缓存", ModBase.LogLevel.Normal, "出现错误");
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "清理合并安装缓存失败", ModBase.LogLevel.Debug, "出现错误");
			}
			string OutputFolder = ModMinecraft.m_ProxyTests + "versions\\" + Request.m_SystemTest + "\\";
			string TempMcFolder = text + Conversions.ToString(Request.m_SystemTest.GetHashCode()) + "\\";
			if (Directory.Exists(TempMcFolder))
			{
				ModBase.DeleteDirectory(TempMcFolder, false);
			}
			string OptiFineFolder = null;
			if (Request._StubTest != null)
			{
				if (Request._StubTest.Contains("_HD_U_"))
				{
					Request._StubTest = "HD_U_" + Request._StubTest.AfterLast("_HD_U_", false);
				}
				ModDownloadLib.McInstallRequest $VB$Local_Request = Request;
				ModDownload.DlOptiFineListEntry dlOptiFineListEntry = new ModDownload.DlOptiFineListEntry();
				dlOptiFineListEntry.m_SchemaTest = Request._TagTest + " " + Request._StubTest.Replace("HD_U_", "").Replace("_", "").Replace("pre", " pre");
				dlOptiFineListEntry.CompareMapper(Request._TagTest);
				dlOptiFineListEntry._DefinitionTest = Request._StubTest.ContainsF("pre", true);
				dlOptiFineListEntry.m_PublisherTest = Request._TagTest + "-OptiFine_" + Request._StubTest;
				dlOptiFineListEntry._DescriptorTest = string.Concat(new string[]
				{
					Request._StubTest.ContainsF("pre", true) ? "preview_" : "",
					"OptiFine_",
					Request._TagTest,
					"_",
					Request._StubTest,
					".jar"
				});
				$VB$Local_Request.rulesTest = dlOptiFineListEntry;
			}
			if (Request.rulesTest != null)
			{
				OptiFineFolder = TempMcFolder + "versions\\" + Request.rulesTest.m_PublisherTest;
			}
			string ForgeFolder = null;
			if (Request.decoratorTest != null)
			{
				Request._RefTest = (Request._RefTest ?? Request.decoratorTest.m_ConfigMap);
			}
			if (Request._RefTest != null)
			{
				ForgeFolder = TempMcFolder + "versions\\forge-" + Request._RefTest;
			}
			string NeoForgeFolder = null;
			if (Request.stateTest != null)
			{
				Request.instanceTest = (Request.instanceTest ?? Request.stateTest.m_ConfigMap);
			}
			if (Request.instanceTest != null)
			{
				NeoForgeFolder = TempMcFolder + "versions\\neoforge-" + Request.instanceTest;
			}
			string FabricFolder = null;
			if (Request.m_CallbackTest != null)
			{
				FabricFolder = string.Concat(new string[]
				{
					TempMcFolder,
					"versions\\fabric-loader-",
					Request.m_CallbackTest,
					"-",
					Request._TagTest
				});
			}
			string LiteLoaderFolder = null;
			if (Request.methodTest != null)
			{
				LiteLoaderFolder = TempMcFolder + "versions\\" + Request._TagTest + "-LiteLoader";
			}
			int num = checked((int)Math.Round(Request._TagTest.Contains(".") ? ModBase.Val(Request._TagTest.Split(".")[1]) : 0.0));
			bool OptiFineAsMod = Request.rulesTest != null && (Request.m_CallbackTest != null || (Request.decoratorTest != null && num >= 14 && num <= 15));
			string ModsFolder = new ModMinecraft.McVersion(OutputFolder).GetPathIndie(true) + "mods\\";
			if (OptiFineAsMod)
			{
				ModBase.Log("[Download] OptiFine 将作为 Mod 进行下载", ModBase.LogLevel.Normal, "出现错误");
				OptiFineFolder = ModsFolder;
			}
			if (OptiFineFolder != null)
			{
				ModBase.Log("[Download] OptiFine 缓存：" + OptiFineFolder, ModBase.LogLevel.Normal, "出现错误");
			}
			if (ForgeFolder != null)
			{
				ModBase.Log("[Download] Forge 缓存：" + ForgeFolder, ModBase.LogLevel.Normal, "出现错误");
			}
			if (NeoForgeFolder != null)
			{
				ModBase.Log("[Download] NeoForge 缓存：" + NeoForgeFolder, ModBase.LogLevel.Normal, "出现错误");
			}
			if (FabricFolder != null)
			{
				ModBase.Log("[Download] Fabric 缓存：" + FabricFolder, ModBase.LogLevel.Normal, "出现错误");
			}
			if (LiteLoaderFolder != null)
			{
				ModBase.Log("[Download] LiteLoader 缓存：" + LiteLoaderFolder, ModBase.LogLevel.Normal, "出现错误");
			}
			ModBase.Log("[Download] 对应的原版版本：" + Request._TagTest, ModBase.LogLevel.Normal, "出现错误");
			if (File.Exists(OutputFolder + Request.m_SystemTest + ".json"))
			{
				ModMain.Hint("版本 " + Request.m_SystemTest + " 已经存在！", ModMain.HintType.Critical, true);
				throw new ModBase.CancelledException();
			}
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			list.Add(new ModLoader.LoaderTask<int, int>("添加忽略标识", delegate(ModLoader.LoaderTask<int, int> a0)
			{
				base._Lambda$__0();
			}, null, ThreadPriority.Normal)
			{
				Show = false,
				Block = false
			});
			if (Request._TemplateTest != null)
			{
				list.Add(new ModNet.LoaderDownload("下载 Fabric API", new List<ModNet.NetFile>
				{
					Request._TemplateTest.ToNetFile(ModsFolder)
				})
				{
					ProgressWeight = 3.0,
					Block = false
				});
			}
			if (Request.OptiFabric != null)
			{
				list.Add(new ModNet.LoaderDownload("下载 OptiFabric", new List<ModNet.NetFile>
				{
					Request.OptiFabric.ToNetFile(ModsFolder)
				})
				{
					ProgressWeight = 3.0,
					Block = false
				});
			}
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("下载原版 " + Request._TagTest, ModDownloadLib.McDownloadClientLoader(Request._TagTest, Request.m_ObserverTest, Request.m_SystemTest))
			{
				Show = false,
				ProgressWeight = 39.0,
				Block = (Request._RefTest == null && Request.rulesTest == null && Request.m_CallbackTest == null && Request.methodTest == null)
			};
			list.Add(loaderCombo);
			if (Request.rulesTest != null)
			{
				if (OptiFineAsMod)
				{
					list.Add(new ModLoader.LoaderCombo<string>("下载 OptiFine " + Request.rulesTest.m_SchemaTest, ModDownloadLib.McDownloadOptiFineSaveLoader(Request.rulesTest, OptiFineFolder + Request.rulesTest._DescriptorTest))
					{
						Show = false,
						ProgressWeight = 16.0,
						Block = (Request._RefTest == null && Request.m_CallbackTest == null && Request.methodTest == null)
					});
				}
				else
				{
					list.Add(new ModLoader.LoaderCombo<string>("下载 OptiFine " + Request.rulesTest.m_SchemaTest, ModDownloadLib.McDownloadOptiFineLoader(Request.rulesTest, TempMcFolder, loaderCombo, Request._ParamTest, false))
					{
						Show = false,
						ProgressWeight = 24.0,
						Block = (Request._RefTest == null && Request.m_CallbackTest == null && Request.methodTest == null)
					});
				}
			}
			if (Request._RefTest != null)
			{
				list.Add(new ModLoader.LoaderCombo<string>("下载 Forge " + Request._RefTest, ModDownloadLib.McDownloadForgelikeLoader(false, Request._RefTest, "forge-" + Request._RefTest, Request._TagTest, Request.decoratorTest, TempMcFolder, loaderCombo, Request._ParamTest))
				{
					Show = false,
					ProgressWeight = 25.0,
					Block = (Request.m_CallbackTest == null && Request.methodTest == null && Request.stateTest == null)
				});
			}
			if (Request.instanceTest != null)
			{
				list.Add(new ModLoader.LoaderCombo<string>("下载 NeoForge " + Request.instanceTest, ModDownloadLib.McDownloadForgelikeLoader(true, Request.instanceTest, "neoforge-" + Request.instanceTest, Request._TagTest, Request.stateTest, TempMcFolder, loaderCombo, Request._ParamTest))
				{
					Show = false,
					ProgressWeight = 25.0,
					Block = (Request.decoratorTest == null && Request.m_CallbackTest == null && Request.methodTest == null)
				});
			}
			if (Request.methodTest != null)
			{
				list.Add(new ModLoader.LoaderCombo<string>("下载 LiteLoader " + Request._TagTest, ModDownloadLib.McDownloadLiteLoaderLoader(Request.methodTest, TempMcFolder, loaderCombo, false))
				{
					Show = false,
					ProgressWeight = 1.0,
					Block = (Request.m_CallbackTest == null)
				});
			}
			if (Request.m_CallbackTest != null)
			{
				list.Add(new ModLoader.LoaderCombo<string>("下载 Fabric " + Request.m_CallbackTest, ModDownloadLib.McDownloadFabricLoader(Request.m_CallbackTest, Request._TagTest, TempMcFolder, false))
				{
					Show = false,
					ProgressWeight = 2.0,
					Block = true
				});
			}
			list.Add(new ModLoader.LoaderTask<string, string>("安装游戏", delegate(ModLoader.LoaderTask<string, string> Task)
			{
				ModDownloadLib.InstallMerge(OutputFolder, OutputFolder, OptiFineFolder, OptiFineAsMod, ForgeFolder, Request._RefTest, NeoForgeFolder, Request.instanceTest, FabricFolder, LiteLoaderFolder);
				Task.Progress = 0.3;
				if (Directory.Exists(TempMcFolder + "libraries"))
				{
					ModBase.CopyDirectory(TempMcFolder + "libraries", ModMinecraft.m_ProxyTests + "libraries", null);
				}
				if (Directory.Exists(TempMcFolder + "mods"))
				{
					ModBase.CopyDirectory(TempMcFolder + "mods", ModsFolder, null);
				}
				if (Request._RefTest != null || Request.m_CallbackTest != null || Request.stateTest != null || Request.methodTest != null)
				{
					Directory.CreateDirectory(ModsFolder);
					ModBase.Log("[Download] 自动创建 mods 文件夹：" + ModsFolder, ModBase.LogLevel.Normal, "出现错误");
				}
			}, null, ThreadPriority.Normal)
			{
				ProgressWeight = 2.0,
				Block = true
			});
			if (!DontFixLibraries && (Request.rulesTest != null || (Request._RefTest != null && Conversions.ToDouble(Request._RefTest.BeforeFirst(".", false)) >= 20.0) || Request.instanceTest != null || Request.m_CallbackTest != null || Request.methodTest != null))
			{
				list.Add(new ModLoader.LoaderCombo<string>("下载游戏支持库文件", new List<ModLoader.LoaderBase>
				{
					new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析游戏支持库文件（副加载器）", delegate(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
					{
						Task.Output = ModMinecraft.McLibFix(new ModMinecraft.McVersion(OutputFolder));
					}, null, ThreadPriority.Normal)
					{
						ProgressWeight = 1.0,
						Show = false
					},
					new ModNet.LoaderDownload("下载游戏支持库文件（副加载器）", new List<ModNet.NetFile>())
					{
						ProgressWeight = 7.0,
						Show = false
					}
				})
				{
					ProgressWeight = 8.0
				});
			}
			list.Add(new ModLoader.LoaderTask<int, int>("删除忽略标识", delegate(ModLoader.LoaderTask<int, int> a0)
			{
				base._Lambda$__3();
			}, null, ThreadPriority.Normal)
			{
				Show = false
			});
			return list;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0003A2D8 File Offset: 0x000384D8
		private static void InstallMerge(string OutputFolder, string MinecraftFolder, string OptiFineFolder = null, bool OptiFineAsMod = false, string ForgeFolder = null, string ForgeVersion = null, string NeoForgeFolder = null, string NeoForgeVersion = null, string FabricFolder = null, string LiteLoaderFolder = null)
		{
			ModBase.Log(string.Concat(new string[]
			{
				"[Download] 开始进行版本合并，输出：",
				OutputFolder,
				"，Minecraft：",
				MinecraftFolder,
				(OptiFineFolder != null) ? ("，OptiFine：" + OptiFineFolder) : "",
				(ForgeFolder != null) ? ("，Forge：" + ForgeFolder) : "",
				(NeoForgeFolder != null) ? ("，NeoForge：" + NeoForgeFolder) : "",
				(LiteLoaderFolder != null) ? ("，LiteLoader：" + LiteLoaderFolder) : "",
				(FabricFolder != null) ? ("，Fabric：" + FabricFolder) : ""
			}), ModBase.LogLevel.Normal, "出现错误");
			Directory.CreateDirectory(OutputFolder);
			bool flag = OptiFineFolder != null && !OptiFineAsMod;
			bool flag2 = ForgeFolder != null;
			bool flag3 = NeoForgeFolder != null;
			bool flag4 = LiteLoaderFolder != null;
			bool flag5 = FabricFolder != null;
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			if (!OutputFolder.EndsWithF("\\", false))
			{
				OutputFolder += "\\";
			}
			string folderNameFromPath = ModBase.GetFolderNameFromPath(OutputFolder);
			string filePath = OutputFolder + folderNameFromPath + ".json";
			string text6 = OutputFolder + folderNameFromPath + ".jar";
			if (!MinecraftFolder.EndsWithF("\\", false))
			{
				MinecraftFolder += "\\";
			}
			string folderNameFromPath2 = ModBase.GetFolderNameFromPath(MinecraftFolder);
			string text7 = MinecraftFolder + folderNameFromPath2 + ".json";
			string text8 = MinecraftFolder + folderNameFromPath2 + ".jar";
			if (flag)
			{
				if (!OptiFineFolder.EndsWithF("\\", false))
				{
					OptiFineFolder += "\\";
				}
				string folderNameFromPath3 = ModBase.GetFolderNameFromPath(OptiFineFolder);
				text = OptiFineFolder + folderNameFromPath3 + ".json";
			}
			if (flag2)
			{
				if (!ForgeFolder.EndsWithF("\\", false))
				{
					ForgeFolder += "\\";
				}
				string folderNameFromPath4 = ModBase.GetFolderNameFromPath(ForgeFolder);
				text2 = ForgeFolder + folderNameFromPath4 + ".json";
			}
			if (flag3)
			{
				if (!NeoForgeFolder.EndsWithF("\\", false))
				{
					NeoForgeFolder += "\\";
				}
				string folderNameFromPath5 = ModBase.GetFolderNameFromPath(NeoForgeFolder);
				text3 = NeoForgeFolder + folderNameFromPath5 + ".json";
			}
			if (flag4)
			{
				if (!LiteLoaderFolder.EndsWithF("\\", false))
				{
					LiteLoaderFolder += "\\";
				}
				string folderNameFromPath6 = ModBase.GetFolderNameFromPath(LiteLoaderFolder);
				text4 = LiteLoaderFolder + folderNameFromPath6 + ".json";
			}
			if (flag5)
			{
				if (!FabricFolder.EndsWithF("\\", false))
				{
					FabricFolder += "\\";
				}
				string folderNameFromPath7 = ModBase.GetFolderNameFromPath(FabricFolder);
				text5 = FabricFolder + folderNameFromPath7 + ".json";
			}
			JObject jobject = null;
			JObject jobject2 = null;
			JObject jobject3 = null;
			JObject jobject4 = null;
			JObject jobject5 = null;
			string text9 = ModBase.ReadFile(text7, null);
			if (!text9.StartsWithF("{", false))
			{
				throw new Exception("Minecraft json 有误，地址：" + text7 + "，前段内容：" + text9.Substring(0, Math.Min(text9.Length, 1000)));
			}
			JObject jobject6 = (JObject)ModBase.GetJson(text9);
			if (flag)
			{
				string text10 = ModBase.ReadFile(text, null);
				if (!text10.StartsWithF("{", false))
				{
					throw new Exception("OptiFine json 有误，地址：" + text + "，前段内容：" + text10.Substring(0, Math.Min(text10.Length, 1000)));
				}
				jobject = (JObject)ModBase.GetJson(text10);
			}
			if (flag2)
			{
				string text11 = ModBase.ReadFile(text2, null);
				if (!text11.StartsWithF("{", false))
				{
					throw new Exception("Forge json 有误，地址：" + text2 + "，前段内容：" + text11.Substring(0, Math.Min(text11.Length, 1000)));
				}
				jobject2 = (JObject)ModBase.GetJson(text11);
			}
			if (flag3)
			{
				string text12 = ModBase.ReadFile(text3, null);
				if (!text12.StartsWithF("{", false))
				{
					throw new Exception("NeoForge json 有误，地址：" + text3 + "，前段内容：" + text12.Substring(0, Math.Min(text12.Length, 1000)));
				}
				jobject3 = (JObject)ModBase.GetJson(text12);
			}
			if (flag4)
			{
				string text13 = ModBase.ReadFile(text4, null);
				if (!text13.StartsWithF("{", false))
				{
					throw new Exception("LiteLoader json 有误，地址：" + text4 + "，前段内容：" + text13.Substring(0, Math.Min(text13.Length, 1000)));
				}
				jobject4 = (JObject)ModBase.GetJson(text13);
			}
			if (flag5)
			{
				string text14 = ModBase.ReadFile(text5, null);
				if (!text14.StartsWithF("{", false))
				{
					throw new Exception("Fabric json 有误，地址：" + text5 + "，前段内容：" + text14.Substring(0, Math.Min(text14.Length, 1000)));
				}
				jobject5 = (JObject)ModBase.GetJson(text14);
			}
			List<string> list = Enumerable.ToList<string>(Enumerable.Select<string, string>(Enumerable.Where<string>(string.Concat(new string[]
			{
				(jobject6["minecraftArguments"] ?? " ").ToString(),
				" ",
				(jobject != null) ? (jobject["minecraftArguments"] ?? " ").ToString() : " ",
				" ",
				(jobject2 != null) ? (jobject2["minecraftArguments"] ?? " ").ToString() : " ",
				" ",
				(jobject3 != null) ? (jobject3["minecraftArguments"] ?? " ").ToString() : " ",
				" ",
				(jobject4 != null) ? (jobject4["minecraftArguments"] ?? " ").ToString() : " "
			}).Split(" "), (ModDownloadLib._Closure$__.$I63-0 == null) ? (ModDownloadLib._Closure$__.$I63-0 = ((string l) => Operators.CompareString(l, "", false) != 0)) : ModDownloadLib._Closure$__.$I63-0), (ModDownloadLib._Closure$__.$I63-1 == null) ? (ModDownloadLib._Closure$__.$I63-1 = ((string l) => l.Trim())) : ModDownloadLib._Closure$__.$I63-1));
			List<string> list2 = new List<string>();
			checked
			{
				int num = list.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					if (list[i].StartsWithF("-", false))
					{
						list2.Add(list[i]);
					}
					else if (Enumerable.Any<string>(list2) && Enumerable.Last<string>(list2).StartsWithF("-", false) && !Enumerable.Last<string>(list2).Contains(" "))
					{
						list2[list2.Count - 1] = Enumerable.Last<string>(list2) + " " + list[i];
					}
					else
					{
						list2.Add(list[i]);
					}
				}
				string text15 = Enumerable.ToList<string>(Enumerable.Distinct<string>(list2)).Join(" ");
				JObject jobject7 = jobject6;
				if (flag)
				{
					jobject.Remove("releaseTime");
					jobject.Remove("time");
					jobject7.Merge(jobject);
				}
				if (flag2)
				{
					jobject2.Remove("releaseTime");
					jobject2.Remove("time");
					jobject7.Merge(jobject2);
				}
				if (flag3)
				{
					jobject3.Remove("releaseTime");
					jobject3.Remove("time");
					jobject7.Merge(jobject3);
				}
				if (flag4)
				{
					jobject4.Remove("releaseTime");
					jobject4.Remove("time");
					jobject7.Merge(jobject4);
				}
				if (flag5)
				{
					jobject5.Remove("releaseTime");
					jobject5.Remove("time");
					jobject7.Merge(jobject5);
				}
				if (text15 != null && Operators.CompareString(text15.Replace(" ", ""), "", false) != 0)
				{
					jobject7["minecraftArguments"] = text15;
				}
				jobject7.Remove("_comment_");
				jobject7.Remove("inheritsFrom");
				jobject7.Remove("jar");
				jobject7["id"] = folderNameFromPath;
				ModBase.WriteFile(filePath, jobject7.ToString(), false, null);
				if (Operators.CompareString(text8, text6, false) != 0)
				{
					if (File.Exists(text6))
					{
						File.Delete(text6);
					}
					ModBase.CopyFile(text8, text6);
				}
				ModBase.Log("[Download] 版本合并 " + folderNameFromPath + " 完成", ModBase.LogLevel.Normal, "出现错误");
			}
		}

		// Token: 0x0400035C RID: 860
		private static object _StatusField = RuntimeHelpers.GetObjectValue(new object());

		// Token: 0x0400035D RID: 861
		private static object roleField = RuntimeHelpers.GetObjectValue(new object());

		// Token: 0x0400035E RID: 862
		public static int m_StructField = -1;

		// Token: 0x0400035F RID: 863
		private static bool printerField = false;

		// Token: 0x04000360 RID: 864
		private static bool valField = false;

		// Token: 0x020000C9 RID: 201
		public class McInstallRequest
		{
			// Token: 0x06000623 RID: 1571 RVA: 0x0003AB50 File Offset: 0x00038D50
			public McInstallRequest()
			{
				this._TagTest = null;
				this.m_ObserverTest = null;
				this._StubTest = null;
				this.rulesTest = null;
				this._RefTest = null;
				this.decoratorTest = null;
				this.instanceTest = null;
				this.stateTest = null;
				this.m_CallbackTest = null;
				this._TemplateTest = null;
				this.OptiFabric = null;
				this.methodTest = null;
			}

			// Token: 0x04000361 RID: 865
			public string m_SystemTest;

			// Token: 0x04000362 RID: 866
			public string _ParamTest;

			// Token: 0x04000363 RID: 867
			public string _TagTest;

			// Token: 0x04000364 RID: 868
			public string m_ObserverTest;

			// Token: 0x04000365 RID: 869
			public string _StubTest;

			// Token: 0x04000366 RID: 870
			public ModDownload.DlOptiFineListEntry rulesTest;

			// Token: 0x04000367 RID: 871
			public string _RefTest;

			// Token: 0x04000368 RID: 872
			public ModDownload.DlForgeVersionEntry decoratorTest;

			// Token: 0x04000369 RID: 873
			public string instanceTest;

			// Token: 0x0400036A RID: 874
			public ModDownload.DlNeoForgeListEntry stateTest;

			// Token: 0x0400036B RID: 875
			public string m_CallbackTest;

			// Token: 0x0400036C RID: 876
			public ModComp.CompFile _TemplateTest;

			// Token: 0x0400036D RID: 877
			public ModComp.CompFile OptiFabric;

			// Token: 0x0400036E RID: 878
			public ModDownload.DlLiteLoaderListEntry methodTest;
		}
	}
}
