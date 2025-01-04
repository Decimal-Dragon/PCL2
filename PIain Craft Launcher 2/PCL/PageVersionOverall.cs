using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json.Linq;
using PCL.My;

namespace PCL
{
	// Token: 0x0200011B RID: 283
	[DesignerGenerated]
	public class PageVersionOverall : MyPageRight, IComponentConnector
	{
		// Token: 0x06000BAA RID: 2986 RVA: 0x00007F87 File Offset: 0x00006187
		public PageVersionOverall()
		{
			base.Loaded += this.PageSetupLaunch_Loaded;
			this.filterConfig = false;
			this.InitializeComponent();
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00007FAF File Offset: 0x000061AF
		private void PageSetupLaunch_Loaded(object sender, RoutedEventArgs e)
		{
			this.PanBack.ScrollToHome();
			this.ItemDisplayLogoCustom.Tag = "PCL\\Logo.png";
			this.Reload();
			if (!this.filterConfig)
			{
				this.filterConfig = true;
				this.PanDisplay.TriggerForceResize();
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0004F63C File Offset: 0x0004D83C
		private void Reload()
		{
			checked
			{
				ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
				this.ComboDisplayType.SelectedIndex = Conversions.ToInteger(ModBase.ReadIni(PageVersionLeft._InstanceConfig.Path + "PCL\\Setup.ini", "DisplayType", Conversions.ToString(0)));
				this.BtnDisplayStar.Text = (PageVersionLeft._InstanceConfig._TokenMap ? "从收藏夹中移除" : "加入收藏夹");
				this.BtnFolderMods.Visibility = (PageVersionLeft._InstanceConfig.RunThread() ? Visibility.Visible : Visibility.Collapsed);
				this.PanDisplayItem.Children.Clear();
				this.m_DatabaseConfig = PageSelectRight.McVersionListItem(PageVersionLeft._InstanceConfig);
				this.m_DatabaseConfig.IsHitTestVisible = false;
				this.PanDisplayItem.Children.Add(this.m_DatabaseConfig);
				ModMain._ProcessIterator.PageNameRefresh();
				this.ComboDisplayLogo.SelectedIndex = 0;
				string text = ModBase.ReadIni(PageVersionLeft._InstanceConfig.Path + "PCL\\Setup.ini", "Logo", "");
				if (Conversions.ToBoolean(ModBase.ReadIni(PageVersionLeft._InstanceConfig.Path + "PCL\\Setup.ini", "LogoCustom", "False")))
				{
					try
					{
						foreach (object obj in ((IEnumerable)this.ComboDisplayLogo.Items))
						{
							MyComboBoxItem myComboBoxItem = (MyComboBoxItem)obj;
							if (Operators.ConditionalCompareObjectEqual(myComboBoxItem.Tag, text, false) || (Operators.ConditionalCompareObjectEqual(myComboBoxItem.Tag, "PCL\\Logo.png", false) && text.EndsWith("PCL\\Logo.png")))
							{
								this.ComboDisplayLogo.SelectedItem = myComboBoxItem;
								break;
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
				ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
			}
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0004F804 File Offset: 0x0004DA04
		private void ComboDisplayType_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.filterConfig && ModAnimation.CalcParser() == 0)
			{
				if (this.ComboDisplayType.SelectedIndex != 1)
				{
					try
					{
						ModBase.WriteIni(PageVersionLeft._InstanceConfig.Path + "PCL\\Setup.ini", "DisplayType", Conversions.ToString(this.ComboDisplayType.SelectedIndex));
						PageVersionLeft._InstanceConfig.expressionMap = (ModMinecraft.McVersionCardType)Conversions.ToInteger(ModBase.ReadIni(PageVersionLeft._InstanceConfig.Path + "PCL\\Setup.ini", "DisplayType", Conversions.ToString(0)));
						ModMain.m_TestsRepository.RefreshModDisabled();
						ModBase.WriteIni(ModMinecraft.m_ProxyTests + "PCL.ini", "VersionCache", "");
						ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", false);
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "修改版本分类失败（" + PageVersionLeft._InstanceConfig.Name + "）", ModBase.LogLevel.Feedback, "出现错误");
					}
					this.Reload();
					return;
				}
				try
				{
					if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("HintHide", null))))
					{
						if (ModMain.MyMsgBox("确认要从版本列表中隐藏该版本吗？隐藏该版本后，它将不再出现于 PCL 显示的版本列表中。\r\n此后，在版本列表页面按下 F11 才可以查看被隐藏的版本。", "隐藏版本提示", "确定", "取消", "", false, true, false, null, null, null) != 1)
						{
							this.ComboDisplayType.SelectedIndex = 0;
							return;
						}
						ModBase.m_IdentifierRepository.Set("HintHide", true, false, null);
					}
					ModBase.WriteIni(PageVersionLeft._InstanceConfig.Path + "PCL\\Setup.ini", "DisplayType", Conversions.ToString(1));
					ModBase.WriteIni(ModMinecraft.m_ProxyTests + "PCL.ini", "VersionCache", "");
					ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", false);
				}
				catch (Exception ex2)
				{
					ModBase.Log(ex2, "隐藏版本 " + PageVersionLeft._InstanceConfig.Name + " 失败", ModBase.LogLevel.Feedback, "出现错误");
				}
			}
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0004FA2C File Offset: 0x0004DC2C
		private void BtnDisplayDesc_Click(object sender, EventArgs e)
		{
			try
			{
				string text = ModBase.ReadIni(PageVersionLeft._InstanceConfig.Path + "PCL\\Setup.ini", "CustomInfo", "");
				string text2 = ModMain.MyMsgBoxInput("更改描述", "修改版本的描述文本，留空则使用 PCL 的默认描述。", text, new Collection<Validate>(), "默认描述", "确定", "取消", false);
				if (text2 != null && Operators.CompareString(text, text2, false) != 0)
				{
					ModBase.WriteIni(PageVersionLeft._InstanceConfig.Path + "PCL\\Setup.ini", "CustomInfo", text2);
				}
				PageVersionLeft._InstanceConfig = new ModMinecraft.McVersion(PageVersionLeft._InstanceConfig.Name).Load();
				this.Reload();
				ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", false);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "版本 " + PageVersionLeft._InstanceConfig.Name + " 描述更改失败", ModBase.LogLevel.Msgbox, "出现错误");
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0004FB2C File Offset: 0x0004DD2C
		private void BtnDisplayRename_Click(object sender, EventArgs e)
		{
			try
			{
				string name = PageVersionLeft._InstanceConfig.Name;
				string path = PageVersionLeft._InstanceConfig.Path;
				string text = ModMain.MyMsgBoxInput("重命名版本", "", name, new Collection<Validate>
				{
					new ValidateFolderName(ModMinecraft.m_ProxyTests + "versions", true, false)
				}, "", "确定", "取消", false);
				if (!string.IsNullOrWhiteSpace(text))
				{
					string text2 = ModMinecraft.m_ProxyTests + "versions\\" + text + "\\";
					string text3 = text + "_temp";
					string directory = ModMinecraft.m_ProxyTests + "versions\\" + text3 + "\\";
					bool flag = Operators.CompareString(text.ToLower(), name.ToLower(), false) == 0;
					JObject jobject;
					try
					{
						jobject = (JObject)ModBase.GetJson(ModBase.ReadFile(PageVersionLeft._InstanceConfig.Path + PageVersionLeft._InstanceConfig.Name + ".json", null));
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "重命名读取 json 时失败", ModBase.LogLevel.Debug, "出现错误");
						jobject = PageVersionLeft._InstanceConfig.NewThread();
					}
					MyWpfExtension.ManageParser().FileSystem.RenameDirectory(path, text3);
					MyWpfExtension.ManageParser().FileSystem.RenameDirectory(directory, text);
					ModBase.IniClearCache(PageVersionLeft._InstanceConfig.Path + "PCL\\Setup.ini");
					try
					{
						foreach (DirectoryInfo directoryInfo in new DirectoryInfo(text2).EnumerateDirectories())
						{
							if (directoryInfo.Name.Contains(name))
							{
								if (flag)
								{
									MyWpfExtension.ManageParser().FileSystem.RenameDirectory(directoryInfo.FullName, directoryInfo.Name + "_temp");
									MyWpfExtension.ManageParser().FileSystem.RenameDirectory(directoryInfo.FullName + "_temp", directoryInfo.Name.Replace(name, text));
								}
								else
								{
									ModBase.DeleteDirectory(text2 + directoryInfo.Name.Replace(name, text), false);
									MyWpfExtension.ManageParser().FileSystem.RenameDirectory(directoryInfo.FullName, directoryInfo.Name.Replace(name, text));
								}
							}
						}
					}
					finally
					{
						IEnumerator<DirectoryInfo> enumerator;
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
					try
					{
						foreach (FileInfo fileInfo in new DirectoryInfo(text2).EnumerateFiles())
						{
							if (fileInfo.Name.Contains(name))
							{
								if (flag)
								{
									MyWpfExtension.ManageParser().FileSystem.RenameFile(fileInfo.FullName, fileInfo.Name + "_temp");
									MyWpfExtension.ManageParser().FileSystem.RenameFile(fileInfo.FullName + "_temp", fileInfo.Name.Replace(name, text));
								}
								else
								{
									if (File.Exists(text2 + fileInfo.Name.Replace(name, text)))
									{
										File.Delete(text2 + fileInfo.Name.Replace(name, text));
									}
									MyWpfExtension.ManageParser().FileSystem.RenameFile(fileInfo.FullName, fileInfo.Name.Replace(name, text));
								}
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
					if (File.Exists(text2 + "PCL\\Setup.ini"))
					{
						ModBase.WriteFile(text2 + "PCL\\Setup.ini", ModBase.ReadFile(text2 + "PCL\\Setup.ini", null).Replace(path, text2), false, null);
					}
					if (Operators.CompareString(ModBase.ReadIni(ModMinecraft.m_ProxyTests + "PCL.ini", "Version", ""), name, false) == 0)
					{
						ModBase.WriteIni(ModMinecraft.m_ProxyTests + "PCL.ini", "Version", text);
					}
					if (File.Exists(text2 + text + ".json"))
					{
						try
						{
							jobject["id"] = text;
							ModBase.WriteFile(text2 + text + ".json", jobject.ToString(), false, null);
						}
						catch (Exception ex2)
						{
							ModBase.Log(ex2, "重命名版本 json 失败", ModBase.LogLevel.Debug, "出现错误");
						}
					}
					ModMain.Hint("重命名成功！", ModMain.HintType.Finish, true);
					PageVersionLeft._InstanceConfig = new ModMinecraft.McVersion(text).Load();
					if (!Information.IsNothing(ModMinecraft.AddClient()) && ModMinecraft.AddClient().Equals(PageVersionLeft._InstanceConfig))
					{
						ModBase.WriteIni(ModMinecraft.m_ProxyTests + "PCL.ini", "Version", text);
					}
					this.Reload();
					ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", false);
				}
			}
			catch (Exception ex3)
			{
				ModBase.Log(ex3, "重命名版本失败", ModBase.LogLevel.Msgbox, "出现错误");
			}
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00050064 File Offset: 0x0004E264
		private void ComboDisplayLogo_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.filterConfig && ModAnimation.CalcParser() == 0)
			{
				try
				{
					if (this.ComboDisplayLogo.SelectedItem == this.ItemDisplayLogoCustom)
					{
						string text = ModBase.SelectFile("常用图片文件(*.png;*.jpg;*.gif)|*.png;*.jpg;*.gif", "选择图片");
						if (Operators.CompareString(text, "", false) == 0)
						{
							this.Reload();
							return;
						}
						File.Delete(PageVersionLeft._InstanceConfig.Path + "PCL\\Logo.png");
						Directory.CreateDirectory(PageVersionLeft._InstanceConfig.Path + "PCL");
						ModBase.CopyFile(text, PageVersionLeft._InstanceConfig.Path + "PCL\\Logo.png");
					}
					else
					{
						File.Delete(PageVersionLeft._InstanceConfig.Path + "PCL\\Logo.png");
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "更改自定义版本图标失败（" + PageVersionLeft._InstanceConfig.Name + "）", ModBase.LogLevel.Feedback, "出现错误");
				}
				try
				{
					string text2 = Conversions.ToString(NewLateBinding.LateGet(this.ComboDisplayLogo.SelectedItem, null, "Tag", new object[0], null, null, null));
					ModBase.WriteIni(PageVersionLeft._InstanceConfig.Path + "PCL\\Setup.ini", "Logo", text2);
					ModBase.WriteIni(PageVersionLeft._InstanceConfig.Path + "PCL\\Setup.ini", "LogoCustom", Conversions.ToString(Operators.CompareString(text2, "", false) != 0));
					ModBase.WriteIni(ModMinecraft.m_ProxyTests + "PCL.ini", "VersionCache", "");
					PageVersionLeft._InstanceConfig = new ModMinecraft.McVersion(PageVersionLeft._InstanceConfig.Name).Load();
					this.Reload();
					ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", false);
				}
				catch (Exception ex2)
				{
					ModBase.Log(ex2, "更改版本图标失败（" + PageVersionLeft._InstanceConfig.Name + "）", ModBase.LogLevel.Feedback, "出现错误");
				}
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00050280 File Offset: 0x0004E480
		private void BtnDisplayStar_Click(object sender, EventArgs e)
		{
			try
			{
				ModBase.WriteIni(PageVersionLeft._InstanceConfig.Path + "PCL\\Setup.ini", "IsStar", Conversions.ToString(!PageVersionLeft._InstanceConfig._TokenMap));
				PageVersionLeft._InstanceConfig = new ModMinecraft.McVersion(PageVersionLeft._InstanceConfig.Name).Load();
				this.Reload();
				ModMinecraft.m_ProductTests = true;
				ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", false);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "版本 " + PageVersionLeft._InstanceConfig.Name + " 收藏状态更改失败", ModBase.LogLevel.Msgbox, "出现错误");
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00007FEC File Offset: 0x000061EC
		private void BtnFolderVersion_Click()
		{
			PageVersionOverall.OpenVersionFolder(PageVersionLeft._InstanceConfig);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00007FF8 File Offset: 0x000061F8
		public static void OpenVersionFolder(ModMinecraft.McVersion Version)
		{
			ModBase.OpenExplorer("\"" + Version.Path + "\"");
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00050340 File Offset: 0x0004E540
		private void BtnFolderSaves_Click()
		{
			string text = PageVersionLeft._InstanceConfig.ChangeMapper() + "saves\\";
			Directory.CreateDirectory(text);
			ModBase.OpenExplorer("\"" + text + "\"");
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00050380 File Offset: 0x0004E580
		private void BtnFolderMods_Click()
		{
			string text = PageVersionLeft._InstanceConfig.ChangeMapper() + "mods\\";
			Directory.CreateDirectory(text);
			ModBase.OpenExplorer("\"" + text + "\"");
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x000503C0 File Offset: 0x0004E5C0
		private void BtnManageScript_Click()
		{
			try
			{
				string text = ModBase.SelectAs("选择脚本保存位置", "启动 " + PageVersionLeft._InstanceConfig.Name + ".bat", "批处理文件(*.bat)|*.bat", null);
				if (Operators.CompareString(text, "", false) != 0)
				{
					if (ModLaunch.databaseTests.State == ModBase.LoadState.Loading)
					{
						ModMain.Hint("请在当前启动任务结束后再试！", ModMain.HintType.Critical, true);
					}
					else if (ModLaunch.McLaunchStart(new ModLaunch.McLaunchOptions
					{
						requestMap = text,
						_DicMap = PageVersionLeft._InstanceConfig
					}))
					{
						if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LoginType", null), ModLaunch.McLoginType.Legacy, false))
						{
							ModMain.Hint("正在导出启动脚本……", ModMain.HintType.Info, true);
						}
						else
						{
							ModMain.Hint("正在导出启动脚本……（注意，使用脚本启动可能会导致登录失效！）", ModMain.HintType.Info, true);
						}
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "导出启动脚本失败（" + PageVersionLeft._InstanceConfig.Name + "）", ModBase.LogLevel.Msgbox, "出现错误");
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000504C4 File Offset: 0x0004E6C4
		private void BtnManageCheck_Click(object sender, EventArgs e)
		{
			try
			{
				PageVersionOverall._Closure$__15-0 CS$<>8__locals1 = new PageVersionOverall._Closure$__15-0(CS$<>8__locals1);
				if (Conversions.ToBoolean(ModMinecraft.ShouldIgnoreFileCheck(PageVersionLeft._InstanceConfig)))
				{
					if (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LaunchAdvanceAssets", null)))
					{
						ModMain.Hint("请先关闭 [设置 → 高级启动选项 → 关闭文件校验]，然后再尝试补全文件！", ModMain.HintType.Info, true);
					}
					else
					{
						ModMain.Hint("请先关闭 [版本设置 → 设置 → 高级启动选项 → 关闭文件校验]，然后再尝试补全文件！", ModMain.HintType.Info, true);
					}
				}
				else
				{
					try
					{
						IEnumerator<ModLoader.LoaderBase> enumerator = ModLoader.LoaderTaskbar.GetEnumerator();
						while (enumerator.MoveNext())
						{
							if (Operators.CompareString(enumerator.Current.Name, PageVersionLeft._InstanceConfig.Name + " 文件补全", false) == 0)
							{
								ModMain.Hint("正在处理中，请稍候！", ModMain.HintType.Critical, true);
								return;
							}
						}
					}
					finally
					{
						IEnumerator<ModLoader.LoaderBase> enumerator;
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
					CS$<>8__locals1.$VB$Local_Loader = new ModLoader.LoaderCombo<string>(PageVersionLeft._InstanceConfig.Name + " 文件补全", ModDownload.DlClientFix(PageVersionLeft._InstanceConfig, true, ModDownload.AssetsIndexExistsBehaviour.AlwaysDownload));
					CS$<>8__locals1.$VB$Local_Loader.OnStateChanged = delegate(ModLoader.LoaderBase a0)
					{
						base._Lambda$__0();
					};
					CS$<>8__locals1.$VB$Local_Loader.Start(PageVersionLeft._InstanceConfig.Name, false);
					ModLoader.LoaderTaskbarAdd<string>(CS$<>8__locals1.$VB$Local_Loader);
					ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
					ModMain._ProcessIterator.BtnExtraDownload.Ribble();
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "尝试补全文件失败（" + PageVersionLeft._InstanceConfig.Name + "）", ModBase.LogLevel.Msgbox, "出现错误");
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00050664 File Offset: 0x0004E864
		private void BtnManageDelete_Click(object sender, EventArgs e)
		{
			try
			{
				bool shiftKeyDown = MyWpfExtension.ManageParser().Keyboard.ShiftKeyDown;
				bool flag = PageVersionLeft._InstanceConfig._ConfigurationMap != ModMinecraft.McVersionState.Error && Operators.CompareString(PageVersionLeft._InstanceConfig.ChangeMapper(), ModMinecraft.m_ProxyTests, false) != 0;
				int num = ModMain.MyMsgBox(string.Format("你确定要{0}删除版本 {1} 吗？", shiftKeyDown ? "永久" : "", PageVersionLeft._InstanceConfig.Name) + (flag ? "\r\n由于该版本开启了版本隔离，删除版本时该版本对应的存档、资源包、Mod 等文件也将被一并删除！" : ""), "版本删除确认", "确定", "取消", "", flag || shiftKeyDown, true, false, null, null, null);
				if (num != 1)
				{
					if (num == 2)
					{
						return;
					}
				}
				else
				{
					ModBase.IniClearCache(PageVersionLeft._InstanceConfig.Path + "PCL\\Setup.ini");
					if (shiftKeyDown)
					{
						ModBase.DeleteDirectory(PageVersionLeft._InstanceConfig.Path, false);
						ModMain.Hint("版本 " + PageVersionLeft._InstanceConfig.Name + " 已永久删除！", ModMain.HintType.Finish, true);
					}
					else
					{
						Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(PageVersionLeft._InstanceConfig.Path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
						ModMain.Hint("版本 " + PageVersionLeft._InstanceConfig.Name + " 已删除到回收站！", ModMain.HintType.Finish, true);
					}
				}
				ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", false);
				ModMain._ProcessIterator.PageBack();
			}
			catch (OperationCanceledException ex)
			{
				ModBase.Log(ex, "删除版本 " + PageVersionLeft._InstanceConfig.Name + " 被主动取消", ModBase.LogLevel.Debug, "出现错误");
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, "删除版本 " + PageVersionLeft._InstanceConfig.Name + " 失败", ModBase.LogLevel.Msgbox, "出现错误");
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x00008014 File Offset: 0x00006214
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x0000801C File Offset: 0x0000621C
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x00008025 File Offset: 0x00006225
		// (set) Token: 0x06000BBC RID: 3004 RVA: 0x0000802D File Offset: 0x0000622D
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x00008036 File Offset: 0x00006236
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x0000803E File Offset: 0x0000623E
		internal virtual Grid PanDisplayItem { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x00008047 File Offset: 0x00006247
		// (set) Token: 0x06000BC0 RID: 3008 RVA: 0x0000804F File Offset: 0x0000624F
		internal virtual MyCard PanDisplay { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00008058 File Offset: 0x00006258
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x00008060 File Offset: 0x00006260
		internal virtual Grid PanDisplayIcon { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x00008069 File Offset: 0x00006269
		// (set) Token: 0x06000BC4 RID: 3012 RVA: 0x0005085C File Offset: 0x0004EA5C
		internal virtual MyComboBox ComboDisplayLogo
		{
			[CompilerGenerated]
			get
			{
				return this.m_ContainerConfig;
			}
			[CompilerGenerated]
			set
			{
				SelectionChangedEventHandler value2 = new SelectionChangedEventHandler(this.ComboDisplayLogo_SelectionChanged);
				MyComboBox containerConfig = this.m_ContainerConfig;
				if (containerConfig != null)
				{
					containerConfig.SelectionChanged -= value2;
				}
				this.m_ContainerConfig = value;
				containerConfig = this.m_ContainerConfig;
				if (containerConfig != null)
				{
					containerConfig.SelectionChanged += value2;
				}
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x00008071 File Offset: 0x00006271
		// (set) Token: 0x06000BC6 RID: 3014 RVA: 0x00008079 File Offset: 0x00006279
		internal virtual MyComboBoxItem ItemDisplayLogoCustom { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x00008082 File Offset: 0x00006282
		// (set) Token: 0x06000BC8 RID: 3016 RVA: 0x000508A0 File Offset: 0x0004EAA0
		internal virtual MyComboBox ComboDisplayType
		{
			[CompilerGenerated]
			get
			{
				return this._DispatcherConfig;
			}
			[CompilerGenerated]
			set
			{
				SelectionChangedEventHandler value2 = new SelectionChangedEventHandler(this.ComboDisplayType_SelectionChanged);
				MyComboBox dispatcherConfig = this._DispatcherConfig;
				if (dispatcherConfig != null)
				{
					dispatcherConfig.SelectionChanged -= value2;
				}
				this._DispatcherConfig = value;
				dispatcherConfig = this._DispatcherConfig;
				if (dispatcherConfig != null)
				{
					dispatcherConfig.SelectionChanged += value2;
				}
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0000808A File Offset: 0x0000628A
		// (set) Token: 0x06000BCA RID: 3018 RVA: 0x000508E4 File Offset: 0x0004EAE4
		internal virtual MyButton BtnDisplayRename
		{
			[CompilerGenerated]
			get
			{
				return this.processConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnDisplayRename_Click);
				MyButton myButton = this.processConfig;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.processConfig = value;
				myButton = this.processConfig;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x00008092 File Offset: 0x00006292
		// (set) Token: 0x06000BCC RID: 3020 RVA: 0x00050928 File Offset: 0x0004EB28
		internal virtual MyButton BtnDisplayDesc
		{
			[CompilerGenerated]
			get
			{
				return this.m_ParameterConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnDisplayDesc_Click);
				MyButton parameterConfig = this.m_ParameterConfig;
				if (parameterConfig != null)
				{
					parameterConfig.Click -= value2;
				}
				this.m_ParameterConfig = value;
				parameterConfig = this.m_ParameterConfig;
				if (parameterConfig != null)
				{
					parameterConfig.Click += value2;
				}
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x0000809A File Offset: 0x0000629A
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x0005096C File Offset: 0x0004EB6C
		internal virtual MyButton BtnDisplayStar
		{
			[CompilerGenerated]
			get
			{
				return this._RecordConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnDisplayStar_Click);
				MyButton recordConfig = this._RecordConfig;
				if (recordConfig != null)
				{
					recordConfig.Click -= value2;
				}
				this._RecordConfig = value;
				recordConfig = this._RecordConfig;
				if (recordConfig != null)
				{
					recordConfig.Click += value2;
				}
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x000080A2 File Offset: 0x000062A2
		// (set) Token: 0x06000BD0 RID: 3024 RVA: 0x000080AA File Offset: 0x000062AA
		internal virtual MyCard PanFolder { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x000080B3 File Offset: 0x000062B3
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x000509B0 File Offset: 0x0004EBB0
		internal virtual MyButton BtnFolderVersion
		{
			[CompilerGenerated]
			get
			{
				return this.m_InvocationConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.BtnFolderVersion_Click();
				};
				MyButton invocationConfig = this.m_InvocationConfig;
				if (invocationConfig != null)
				{
					invocationConfig.Click -= value2;
				}
				this.m_InvocationConfig = value;
				invocationConfig = this.m_InvocationConfig;
				if (invocationConfig != null)
				{
					invocationConfig.Click += value2;
				}
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x000080BB File Offset: 0x000062BB
		// (set) Token: 0x06000BD4 RID: 3028 RVA: 0x000509F4 File Offset: 0x0004EBF4
		internal virtual MyButton BtnFolderSaves
		{
			[CompilerGenerated]
			get
			{
				return this.proxyConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.BtnFolderSaves_Click();
				};
				MyButton myButton = this.proxyConfig;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.proxyConfig = value;
				myButton = this.proxyConfig;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x000080C3 File Offset: 0x000062C3
		// (set) Token: 0x06000BD6 RID: 3030 RVA: 0x00050A38 File Offset: 0x0004EC38
		internal virtual MyButton BtnFolderMods
		{
			[CompilerGenerated]
			get
			{
				return this.messageConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.BtnFolderMods_Click();
				};
				MyButton myButton = this.messageConfig;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.messageConfig = value;
				myButton = this.messageConfig;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x000080CB File Offset: 0x000062CB
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x000080D3 File Offset: 0x000062D3
		internal virtual MyCard PanManage { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x000080DC File Offset: 0x000062DC
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x00050A7C File Offset: 0x0004EC7C
		internal virtual MyButton BtnManageScript
		{
			[CompilerGenerated]
			get
			{
				return this.m_InitializerConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.BtnManageScript_Click();
				};
				MyButton initializerConfig = this.m_InitializerConfig;
				if (initializerConfig != null)
				{
					initializerConfig.Click -= value2;
				}
				this.m_InitializerConfig = value;
				initializerConfig = this.m_InitializerConfig;
				if (initializerConfig != null)
				{
					initializerConfig.Click += value2;
				}
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x000080E4 File Offset: 0x000062E4
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x00050AC0 File Offset: 0x0004ECC0
		internal virtual MyButton BtnManageCheck
		{
			[CompilerGenerated]
			get
			{
				return this.singletonConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnManageCheck_Click);
				MyButton myButton = this.singletonConfig;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.singletonConfig = value;
				myButton = this.singletonConfig;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x000080EC File Offset: 0x000062EC
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x00050B04 File Offset: 0x0004ED04
		internal virtual MyButton BtnManageDelete
		{
			[CompilerGenerated]
			get
			{
				return this._RegConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnManageDelete_Click);
				MyButton regConfig = this._RegConfig;
				if (regConfig != null)
				{
					regConfig.Click -= value2;
				}
				this._RegConfig = value;
				regConfig = this._RegConfig;
				if (regConfig != null)
				{
					regConfig.Click += value2;
				}
			}
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00050B48 File Offset: 0x0004ED48
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.productConfig)
			{
				this.productConfig = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageversion/pageversionoverall.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00050B78 File Offset: 0x0004ED78
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (MyScrollViewer)target;
				return;
			}
			if (connectionId == 2)
			{
				this.PanMain = (StackPanel)target;
				return;
			}
			if (connectionId == 3)
			{
				this.PanDisplayItem = (Grid)target;
				return;
			}
			if (connectionId == 4)
			{
				this.PanDisplay = (MyCard)target;
				return;
			}
			if (connectionId == 5)
			{
				this.PanDisplayIcon = (Grid)target;
				return;
			}
			if (connectionId == 6)
			{
				this.ComboDisplayLogo = (MyComboBox)target;
				return;
			}
			if (connectionId == 7)
			{
				this.ItemDisplayLogoCustom = (MyComboBoxItem)target;
				return;
			}
			if (connectionId == 8)
			{
				this.ComboDisplayType = (MyComboBox)target;
				return;
			}
			if (connectionId == 9)
			{
				this.BtnDisplayRename = (MyButton)target;
				return;
			}
			if (connectionId == 10)
			{
				this.BtnDisplayDesc = (MyButton)target;
				return;
			}
			if (connectionId == 11)
			{
				this.BtnDisplayStar = (MyButton)target;
				return;
			}
			if (connectionId == 12)
			{
				this.PanFolder = (MyCard)target;
				return;
			}
			if (connectionId == 13)
			{
				this.BtnFolderVersion = (MyButton)target;
				return;
			}
			if (connectionId == 14)
			{
				this.BtnFolderSaves = (MyButton)target;
				return;
			}
			if (connectionId == 15)
			{
				this.BtnFolderMods = (MyButton)target;
				return;
			}
			if (connectionId == 16)
			{
				this.PanManage = (MyCard)target;
				return;
			}
			if (connectionId == 17)
			{
				this.BtnManageScript = (MyButton)target;
				return;
			}
			if (connectionId == 18)
			{
				this.BtnManageCheck = (MyButton)target;
				return;
			}
			if (connectionId == 19)
			{
				this.BtnManageDelete = (MyButton)target;
				return;
			}
			this.productConfig = true;
		}

		// Token: 0x040005E7 RID: 1511
		private bool filterConfig;

		// Token: 0x040005E8 RID: 1512
		public MyListItem m_DatabaseConfig;

		// Token: 0x040005E9 RID: 1513
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyScrollViewer _PredicateConfig;

		// Token: 0x040005EA RID: 1514
		[AccessedThroughProperty("PanMain")]
		[CompilerGenerated]
		private StackPanel poolConfig;

		// Token: 0x040005EB RID: 1515
		[AccessedThroughProperty("PanDisplayItem")]
		[CompilerGenerated]
		private Grid customerConfig;

		// Token: 0x040005EC RID: 1516
		[AccessedThroughProperty("PanDisplay")]
		[CompilerGenerated]
		private MyCard m_PageConfig;

		// Token: 0x040005ED RID: 1517
		[CompilerGenerated]
		[AccessedThroughProperty("PanDisplayIcon")]
		private Grid m_InterceptorConfig;

		// Token: 0x040005EE RID: 1518
		[CompilerGenerated]
		[AccessedThroughProperty("ComboDisplayLogo")]
		private MyComboBox m_ContainerConfig;

		// Token: 0x040005EF RID: 1519
		[CompilerGenerated]
		[AccessedThroughProperty("ItemDisplayLogoCustom")]
		private MyComboBoxItem paramsConfig;

		// Token: 0x040005F0 RID: 1520
		[CompilerGenerated]
		[AccessedThroughProperty("ComboDisplayType")]
		private MyComboBox _DispatcherConfig;

		// Token: 0x040005F1 RID: 1521
		[CompilerGenerated]
		[AccessedThroughProperty("BtnDisplayRename")]
		private MyButton processConfig;

		// Token: 0x040005F2 RID: 1522
		[CompilerGenerated]
		[AccessedThroughProperty("BtnDisplayDesc")]
		private MyButton m_ParameterConfig;

		// Token: 0x040005F3 RID: 1523
		[AccessedThroughProperty("BtnDisplayStar")]
		[CompilerGenerated]
		private MyButton _RecordConfig;

		// Token: 0x040005F4 RID: 1524
		[AccessedThroughProperty("PanFolder")]
		[CompilerGenerated]
		private MyCard m_ServiceConfig;

		// Token: 0x040005F5 RID: 1525
		[CompilerGenerated]
		[AccessedThroughProperty("BtnFolderVersion")]
		private MyButton m_InvocationConfig;

		// Token: 0x040005F6 RID: 1526
		[CompilerGenerated]
		[AccessedThroughProperty("BtnFolderSaves")]
		private MyButton proxyConfig;

		// Token: 0x040005F7 RID: 1527
		[AccessedThroughProperty("BtnFolderMods")]
		[CompilerGenerated]
		private MyButton messageConfig;

		// Token: 0x040005F8 RID: 1528
		[CompilerGenerated]
		[AccessedThroughProperty("PanManage")]
		private MyCard m_CreatorConfig;

		// Token: 0x040005F9 RID: 1529
		[CompilerGenerated]
		[AccessedThroughProperty("BtnManageScript")]
		private MyButton m_InitializerConfig;

		// Token: 0x040005FA RID: 1530
		[CompilerGenerated]
		[AccessedThroughProperty("BtnManageCheck")]
		private MyButton singletonConfig;

		// Token: 0x040005FB RID: 1531
		[CompilerGenerated]
		[AccessedThroughProperty("BtnManageDelete")]
		private MyButton _RegConfig;

		// Token: 0x040005FC RID: 1532
		private bool productConfig;
	}
}
