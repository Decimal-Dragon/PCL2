using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using PCL.My;

namespace PCL
{
	// Token: 0x02000182 RID: 386
	[DesignerGenerated]
	public class PageOtherTest : MyPageRight, IComponentConnector
	{
		// Token: 0x06000F0E RID: 3854 RVA: 0x00009507 File Offset: 0x00007707
		public PageOtherTest()
		{
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.MeLoaded();
			};
			this.procTests = false;
			this._BroadcasterMapper = true;
			this.InitializeComponent();
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0006F484 File Offset: 0x0006D684
		private void MeLoaded()
		{
			if (!this.procTests)
			{
				this.procTests = true;
				this.TextDownloadFolder.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheDownloadFolder", null));
				this.TextDownloadFolder.Validate();
				if (Operators.CompareString(this.TextDownloadFolder.ValidateResult, "", false) != 0 || Operators.CompareString(this.TextDownloadFolder.Text, "", false) == 0)
				{
					this.TextDownloadFolder.Text = ModBase.Path + "PCL\\MyDownload\\";
				}
				this.TextDownloadFolder.Validate();
				this.TextDownloadName.Validate();
			}
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0006F530 File Offset: 0x0006D730
		private void SaveCacheDownloadFolder()
		{
			ModBase.m_IdentifierRepository.Set("CacheDownloadFolder", this.TextDownloadFolder.Text, false, null);
			((ValidateFileName)Enumerable.First<Validate>(this.TextDownloadName.ValidateRules)).FindClient(this.TextDownloadFolder.Text);
			this.TextDownloadName.Validate();
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0006F58C File Offset: 0x0006D78C
		private void StartButtonRefresh()
		{
			this.BtnDownloadStart.IsEnabled = (Operators.CompareString(this.TextDownloadFolder.ValidateResult, "", false) == 0 && Operators.CompareString(this.TextDownloadUrl.ValidateResult, "", false) == 0 && Operators.CompareString(this.TextDownloadName.ValidateResult, "", false) == 0);
			this.BtnDownloadOpen.IsEnabled = (Operators.CompareString(this.TextDownloadFolder.ValidateResult, "", false) == 0);
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00009536 File Offset: 0x00007736
		private void TextDownloadUrl_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && this.BtnDownloadStart.IsEnabled)
			{
				this.BtnDownloadStart_Click();
			}
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0006F614 File Offset: 0x0006D814
		private void TextDownloadUrl_TextChanged(object sender, TextChangedEventArgs e)
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
				if (Operators.CompareString(this.TextDownloadName.Text, "", false) != 0 || Operators.CompareString(this.TextDownloadUrl.Text, "", false) == 0)
				{
					goto IL_5B;
				}
				IL_39:
				num2 = 3;
				this.TextDownloadName.Text = ModBase.GetFileNameFromPath(WebUtility.UrlDecode(this.TextDownloadUrl.Text));
				IL_5B:
				goto IL_BA;
				IL_5D:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_7B:
				goto IL_AF;
				IL_7D:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_8D:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_7D;
			}
			IL_AF:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_BA:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0006F6F4 File Offset: 0x0006D8F4
		private void BtnDownloadStart_Click()
		{
			PageOtherTest.StartCustomDownload(this.TextDownloadUrl.Text, this.TextDownloadName.Text, this.TextDownloadFolder.Text);
			this.TextDownloadUrl.Text = "";
			this.TextDownloadUrl.Validate();
			this.TextDownloadUrl.ForceShowAsSuccess();
			this.TextDownloadName.Text = "";
			this.TextDownloadName.Validate();
			this.TextDownloadName.ForceShowAsSuccess();
			this.StartButtonRefresh();
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0006F77C File Offset: 0x0006D97C
		public static void StartCustomDownload(string Url, string FileName, string Folder = null)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(Folder))
				{
					Folder = ModBase.SelectAs("选择文件保存位置", FileName, null, null);
					if (!Folder.Contains("\\"))
					{
						return;
					}
					if (Folder.EndsWith(FileName))
					{
						Folder = Strings.Mid(Folder, 1, checked(Folder.Length - FileName.Length));
					}
				}
				Folder = Folder.Replace("/", "\\").TrimEnd(new char[]
				{
					'\\'
				}) + "\\";
				try
				{
					Directory.CreateDirectory(Folder);
					ModBase.CheckPermissionWithException(Folder);
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "访问文件夹失败（" + Folder + "）", ModBase.LogLevel.Hint, "出现错误");
					return;
				}
				ModBase.Log("[Download] 自定义下载文件名：" + FileName, ModBase.LogLevel.Normal, "出现错误");
				ModBase.Log("[Download] 自定义下载文件目标：" + Folder, ModBase.LogLevel.Normal, "出现错误");
				int uuid = ModBase.GetUuid();
				ModNet.LoaderDownload loaderDownload = new ModNet.LoaderDownload("自定义下载文件：" + FileName + " ", new List<ModNet.NetFile>
				{
					new ModNet.NetFile(new string[]
					{
						Url
					}, Folder + FileName, null, true)
				});
				ModLoader.LoaderCombo<int> loaderCombo = new ModLoader.LoaderCombo<int>("自定义下载 (" + Conversions.ToString(uuid) + ") ", new ModLoader.LoaderBase[]
				{
					loaderDownload
				});
				loaderCombo.OnStateChanged = ((PageOtherTest._Closure$__.$IR9-2 == null) ? (PageOtherTest._Closure$__.$IR9-2 = delegate(ModLoader.LoaderBase a0)
				{
					PageOtherTest.DownloadState((ModLoader.LoaderCombo<int>)a0);
				}) : PageOtherTest._Closure$__.$IR9-2);
				loaderCombo.Start(null, false);
				ModLoader.LoaderTaskbarAdd<int>(loaderCombo);
				ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
				ModMain._ProcessIterator.BtnExtraDownload.Ribble();
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, "开始自定义下载失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0006F978 File Offset: 0x0006DB78
		private void BtnDownloadSelect_Click(object sender, EventArgs e)
		{
			string text = ModBase.SelectFolder("选择文件夹");
			if (Operators.CompareString(text, "", false) != 0)
			{
				this.TextDownloadFolder.Text = text;
			}
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0006F9AC File Offset: 0x0006DBAC
		private void BtnDownloadOpen_Click()
		{
			try
			{
				string text = this.TextDownloadFolder.Text;
				Directory.CreateDirectory(text);
				Process.Start(text);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "打开下载文件夹失败", ModBase.LogLevel.Debug, "出现错误");
			}
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0006FA04 File Offset: 0x0006DC04
		private static void DownloadState(ModLoader.LoaderCombo<int> Loader)
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
				switch (Loader.State)
				{
				case ModBase.LoadState.Finished:
					IL_27:
					num2 = 4;
					ModMain.Hint(Loader.Name + "完成！", ModMain.HintType.Finish, true);
					IL_40:
					num2 = 5;
					Interaction.Beep();
					break;
				case ModBase.LoadState.Failed:
					IL_49:
					num2 = 7;
					ModBase.Log(Loader.Error, Loader.Name + "失败", ModBase.LogLevel.Msgbox, "出现错误");
					IL_6C:
					num2 = 8;
					Interaction.Beep();
					break;
				case ModBase.LoadState.Aborted:
					IL_75:
					num2 = 10;
					ModMain.Hint(Loader.Name + "已取消！", ModMain.HintType.Info, true);
					break;
				}
				IL_8F:
				goto IL_10E;
				IL_91:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_CF:
				goto IL_103;
				IL_D1:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_E1:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_D1;
			}
			IL_103:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_10E:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0006FB38 File Offset: 0x0006DD38
		private void BtnClick_Click(object sender, EventArgs e)
		{
			int num;
			int num5;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				List<int> list = new List<int>
				{
					ModBase.RandomInteger(0, 7)
				};
				for (;;)
				{
					IL_2D:
					num2 = 4;
					if (ModBase.RandomInteger(0, 1) != 1)
					{
						break;
					}
					IL_1E:
					num2 = 5;
					list.Add(ModBase.RandomInteger(1, 6));
				}
				IL_39:
				num2 = 7;
				if (DateTime.Now.Month != 4 || DateTime.Now.Day != 1)
				{
					goto IL_6C;
				}
				IL_5D:
				num2 = 8;
				list = new List<int>
				{
					7
				};
				IL_6C:
				num2 = 9;
				if (!list.Contains(1))
				{
					goto IL_B7;
				}
				IL_78:
				num2 = 10;
				if (ModMain.MyMsgBox("当暴露在点击确定后的场景时，有极小部分人群会引发癲痫。这种情形可能是由于某些未查出的癫病症状引起，即使该人员并没有患癫痫病史也有可能造成此类病症。如果您的家人或任何家庭成员曾有过类似症状，请在点击确定前咨询您的医生或医师。如果您在稍后出现任何症状，包括头晕、目眩、眼部或肌肉抽搐、失去意识、失去方向感、抽搐或出现任何自己无法控制的动作，请立即关闭 PCL 并咨询您的医生或医师。\\n这是最后的警告，是否继续操作？".Replace("\\n", "\r\n"), "警告", "确定", "取消", "", true, true, false, null, null, null) == 2)
				{
					goto IL_4D8;
				}
				goto IL_EE;
				IL_B7:
				num2 = 13;
				ModMain.MyMsgBox("PCL 作者不会受理由于点击千万别点造成的任何 Bug。\\n这是最后的警告，是否继续操作？".Replace("\\n", "\r\n"), "警告", "确定", "确定", "确定", true, true, false, null, null, null);
				IL_EE:
				num2 = 14;
				List<int>.Enumerator enumerator = list.GetEnumerator();
				checked
				{
					while (enumerator.MoveNext())
					{
						int num3 = enumerator.Current;
						IL_10E:
						num2 = 15;
						switch (num3)
						{
						case 0:
							IL_13E:
							num2 = 17;
							ModSecret.registryField = 1;
							IL_147:
							num2 = 18;
							ModSecret.ViewReader(-1);
							break;
						case 1:
							IL_155:
							num2 = 20;
							ModSecret.registryField = 2;
							IL_15E:
							num2 = 21;
							ModSecret.ViewReader(-1);
							break;
						case 2:
							IL_16C:
							num2 = 23;
							if (ModMain._ProcessIterator.PanBack.LayoutTransform != null)
							{
								IL_180:
								num2 = 24;
								ModMain._ProcessIterator.PanBack.RenderTransformOrigin = new Point(1.25, 1.25);
							}
							IL_1A9:
							num2 = 25;
							switch (ModBase.RandomInteger(0, 2))
							{
							case 0:
								IL_1CE:
								num2 = 27;
								ModMain._ProcessIterator.PanBack.RenderTransform = new ScaleTransform(1.0, -1.0);
								break;
							case 1:
								IL_1FC:
								num2 = 29;
								ModMain._ProcessIterator.PanBack.RenderTransform = new ScaleTransform(-1.0, -1.0);
								break;
							case 2:
								IL_22A:
								num2 = 31;
								ModMain._ProcessIterator.PanBack.RenderTransform = new ScaleTransform(-1.0, 1.0);
								break;
							}
							break;
						case 3:
							IL_258:
							num2 = 34;
							ModMain._ProcessIterator.m_StateIterator = false;
							IL_266:
							num2 = 35;
							ModMain._ProcessIterator.PanBack.LayoutTransform = new ScaleTransform(2.5, 2.5);
							IL_28F:
							num2 = 36;
							ModMain._ProcessIterator.Height = (double)(MyWpfExtension.ManageParser().Screen.WorkingArea.Height - 200);
							IL_2BB:
							num2 = 37;
							ModMain._ProcessIterator.Width = (double)(MyWpfExtension.ManageParser().Screen.WorkingArea.Width - 200);
							IL_2E7:
							num2 = 38;
							ModMain._ProcessIterator.Left = 0.0;
							IL_2FD:
							num2 = 39;
							ModMain._ProcessIterator.Top = 0.0;
							IL_313:
							num2 = 40;
							ModAnimation.AniStop("Don't Click Scale");
							break;
						case 4:
							IL_325:
							num2 = 42;
							ModMain._ProcessIterator.m_StateIterator = false;
							IL_333:
							num2 = 43;
							ModMain._ProcessIterator.RenderTransform = new ScaleTransform();
							IL_345:
							num2 = 44;
							ModAnimation.AniStart(ModAnimation.AaScaleTransform(ModMain._ProcessIterator, -0.85, 5000, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.ExtraStrong), false), "Don't Click Scale", false);
							IL_373:
							num2 = 45;
							ModAnimation.AniStop("Don't Click Rotate");
							break;
						case 5:
							IL_385:
							num2 = 47;
							ModMain._ProcessIterator.RenderTransform = new RotateTransform();
							IL_397:
							num2 = 48;
							ModAnimation.AniStart(ModAnimation.AaRotateTransform(ModMain._ProcessIterator, (double)(1000000 * ModBase.RandomOne<int>(new int[]
							{
								1,
								-1
							})), 10000000, 0, null, false), "Don't Click Rotate", false);
							IL_3D1:
							num2 = 49;
							ModAnimation.AniStop("Don't Click Scale");
							break;
						case 6:
							IL_3E3:
							num2 = 51;
							ModMain._ProcessIterator.m_StateIterator = false;
							IL_3F1:
							num2 = 52;
							if (ModBase.RandomInteger(0, 1) != 0)
							{
								IL_400:
								num2 = 53;
								ModAnimation.AniStart(ModAnimation.AaX(ModMain._ProcessIterator, (double)(10000000 * ModBase.RandomOne<int>(new int[]
								{
									1,
									-1
								})), 30000000, 0, null, false), "Don't Click Move X", false);
							}
							else
							{
								IL_43C:
								num2 = 55;
								ModAnimation.AniStart(ModAnimation.AaY(ModMain._ProcessIterator, (double)(10000000 * ModBase.RandomOne<int>(new int[]
								{
									1,
									-1
								})), 50000000, 0, null, false), "Don't Click Move Y", false);
							}
							IL_476:
							num2 = 56;
							ModBase.RunInThread((PageOtherTest._Closure$__.$I13-0 == null) ? (PageOtherTest._Closure$__.$I13-0 = delegate()
							{
								for (;;)
								{
									ModBase.RunInUi((PageOtherTest._Closure$__.$I13-1 != null) ? PageOtherTest._Closure$__.$I13-1 : (PageOtherTest._Closure$__.$I13-1 = delegate()
									{
										if (ModMain._ProcessIterator.Top < unchecked(-ModMain._ProcessIterator.Height))
										{
											ModMain._ProcessIterator.Top = (double)(MyWpfExtension.ManageParser().Screen.Bounds.Height + 49);
										}
										if (ModMain._ProcessIterator.Top > (double)(MyWpfExtension.ManageParser().Screen.Bounds.Height + 50))
										{
											ModMain._ProcessIterator.Top = unchecked(-ModMain._ProcessIterator.Height + 1.0);
										}
										if (ModMain._ProcessIterator.Left < unchecked(-ModMain._ProcessIterator.Width))
										{
											ModMain._ProcessIterator.Left = (double)(MyWpfExtension.ManageParser().Screen.Bounds.Width + 49);
										}
										if (ModMain._ProcessIterator.Left > (double)(MyWpfExtension.ManageParser().Screen.Bounds.Width + 50))
										{
											ModMain._ProcessIterator.Left = unchecked(-ModMain._ProcessIterator.Width + 1.0);
										}
									}), false);
									Thread.Sleep(10);
								}
							}) : PageOtherTest._Closure$__.$I13-0);
							break;
						case 7:
							IL_4A4:
							num2 = 58;
							ModBase.OpenWebsite("https://www.bilibili.com/video/BV1GJ411x7h7");
							break;
						}
						IL_4B1:
						num2 = 60;
					}
					IL_4B9:
					num2 = 61;
					((IDisposable)enumerator).Dispose();
					IL_4C9:
					num2 = 62;
					this.BtnClick.Visibility = Visibility.Collapsed;
					IL_4D8:
					goto IL_626;
					IL_4DD:;
				}
				int num4 = num5 + 1;
				num5 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num4);
				IL_5E7:
				goto IL_61B;
				IL_5E9:
				num5 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_5F9:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num5 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_5E9;
			}
			IL_61B:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_626:
			if (num5 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00070190 File Offset: 0x0006E390
		public static void Jrrp()
		{
			checked
			{
				int num = (int)Math.Round(Math.Abs(unchecked(ModBase.GetHash(string.Concat(new string[]
				{
					"asdfgbn",
					Conversions.ToString(DateTime.Now.DayOfYear),
					"12#3$45",
					Conversions.ToString(DateTime.Now.Year),
					"IUY"
				}).ToString()) / 3.0 + ModBase.GetHash(string.Concat(new string[]
				{
					"QWERTY",
					ModBase._TagRepository,
					"0*8&6",
					Conversions.ToString(DateTime.Now.Day),
					"kjhg"
				})) / 3.0) / 527.0) % 1001.0);
				int num2;
				if (num >= 970)
				{
					num2 = 100;
				}
				else
				{
					num2 = (int)Math.Round(unchecked((double)num / 969.0 * 99.0));
				}
				string str;
				if (num2 == 100)
				{
					str = "！100！100！！！！！" + (ModSecret.ThemeUnlock(13, false, null) ? "\r\n隐藏主题 欧皇彩 已解锁！" : "");
				}
				else if (num2 >= 98)
				{
					str = "！差点就到 100 了呢……";
				}
				else if (num2 >= 90)
				{
					str = "！好评如潮！";
				}
				else if (num2 >= 65)
				{
					str = "！今天运气不错呢！";
				}
				else if (num2 > 50)
				{
					str = "！还行啦，还行啦。";
				}
				else if (num2 == 50)
				{
					str = "！五五开……";
				}
				else if (num2 >= 40)
				{
					str = "！勉强还行吧……？";
				}
				else if (num2 >= 20)
				{
					str = "！呜……";
				}
				else if (num2 >= 11)
				{
					str = "？！不会吧……";
				}
				else if (num2 >= 1)
				{
					str = "……（是百分制哦）";
				}
				else
				{
					str = "？！";
					if (ModMain.MyMsgBox("在查看结果前，请先同意以下附加使用条款：\r\n\r\n1. 我知晓并了解 PCL 的今日人品功能完全没有出 Bug。\r\n2. PCL 不对使用本软件所间接造成的一切财产损失（如砸电脑等）等负责。", "今日人品 - 附加使用条款", "同意并查看结果", "再见", "", true, true, false, null, null, null) == 2)
					{
						return;
					}
				}
				ModMain.MyMsgBox((ModMain._ProcessIterator.callbackIterator ? "在这个时候，你的人品值会是：" : "你今天的人品值是：") + Conversions.ToString(num2) + str, (ModMain._ProcessIterator.callbackIterator ? "今日人品? - " : "今日人品 - ") + DateTime.Now.ToString("yyyy/M/d"), "我知道了", "", "", num2 < 20, true, false, null, null, null);
			}
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x000703E4 File Offset: 0x0006E5E4
		public static void RubbishClear()
		{
			ModBase.RunInUi((PageOtherTest._Closure$__.$I15-0 == null) ? (PageOtherTest._Closure$__.$I15-0 = delegate()
			{
				if (ModMain._StrategyIterator != null && ModMain._StrategyIterator.BtnClear != null)
				{
					ModMain._StrategyIterator.BtnClear.IsEnabled = false;
				}
			}) : PageOtherTest._Closure$__.$I15-0, false);
			ModBase.RunInNewThread((PageOtherTest._Closure$__.$I15-1 == null) ? (PageOtherTest._Closure$__.$I15-1 = checked(delegate()
			{
				try
				{
					if (!ModWatcher._IssuerField)
					{
						if (ModLaunch.databaseTests.State != ModBase.LoadState.Loading)
						{
							if (ModNet.HasDownloadingTask(false))
							{
								ModMain.Hint("请在所有下载任务完成后再来清理吧……", ModMain.HintType.Info, true);
								return;
							}
							if (!Enumerable.Any<ModMinecraft.McFolder>(ModMinecraft.messageTests))
							{
								ModMinecraft.m_CreatorTests.Start(null, false);
							}
							if (Operators.CompareString(ModBase.m_DecoratorRepository, Path.GetTempPath() + "PCL\\", false) == 0)
							{
								if (Operators.ConditionalCompareObjectLessEqual(ModBase.m_IdentifierRepository.Get("HintClearRubbish", null), 2, false))
								{
									if (ModMain.MyMsgBox("即将清理游戏日志、错误报告、缓存等文件。\r\n虽然应该没人往这些地方放重要文件，但还是问一下，是否确认继续？\r\n\r\n在完成清理后，PCL 将自动重启。", "清理确认", "确定", "取消", "", false, true, false, null, null, null) == 2)
									{
										return;
									}
									ModBase.m_IdentifierRepository.Set("HintClearRubbish", Operators.AddObject(ModBase.m_IdentifierRepository.Get("HintClearRubbish", null), 1), false, null);
								}
							}
							else if (ModMain.MyMsgBox("即将清理游戏日志、错误报告、缓存等文件。\r\n\r\n你已将缓存文件夹手动修改为：" + ModBase.m_DecoratorRepository + "\r\n清理过程中，将删除该文件夹中的所有内容，且无法恢复。请确认其中没有除了 PCL 缓存以外的重要文件！\r\n\r\n在完成清理后，PCL 将自动重启。", "清理确认", "确定", "取消", "", false, true, false, null, null, null) == 2)
							{
								return;
							}
							int num = 0;
							List<DirectoryInfo> list = new List<DirectoryInfo>();
							if (!Enumerable.Any<ModMinecraft.McFolder>(ModMinecraft.messageTests))
							{
								ModMinecraft.m_CreatorTests.WaitForExit(null, null, false);
							}
							try
							{
								foreach (ModMinecraft.McFolder mcFolder in ModMinecraft.messageTests)
								{
									list.Add(new DirectoryInfo(mcFolder.Path));
									DirectoryInfo directoryInfo = new DirectoryInfo(mcFolder.Path + "versions");
									if (directoryInfo.Exists)
									{
										try
										{
											foreach (DirectoryInfo item in directoryInfo.EnumerateDirectories())
											{
												list.Add(item);
											}
										}
										finally
										{
											IEnumerator<DirectoryInfo> enumerator2;
											if (enumerator2 != null)
											{
												enumerator2.Dispose();
											}
										}
									}
								}
							}
							finally
							{
								List<ModMinecraft.McFolder>.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
							try
							{
								foreach (DirectoryInfo directoryInfo2 in list)
								{
									num += ModBase.DeleteDirectory(directoryInfo2.FullName + (directoryInfo2.FullName.EndsWith("\\") ? "" : "\\") + "crash-reports\\", true);
									num += ModBase.DeleteDirectory(directoryInfo2.FullName + (directoryInfo2.FullName.EndsWith("\\") ? "" : "\\") + "logs\\", true);
									try
									{
										foreach (FileInfo fileInfo in directoryInfo2.EnumerateFiles("*"))
										{
											if (fileInfo.Name.StartsWith("hs_err_pid") || fileInfo.Name.EndsWith(".log") || Operators.CompareString(fileInfo.Name, "WailaErrorOutput.txt", false) == 0)
											{
												fileInfo.Delete();
												num++;
											}
										}
									}
									finally
									{
										IEnumerator<FileInfo> enumerator4;
										if (enumerator4 != null)
										{
											enumerator4.Dispose();
										}
									}
									try
									{
										foreach (DirectoryInfo directoryInfo3 in directoryInfo2.EnumerateDirectories("*"))
										{
											if (Operators.CompareString(directoryInfo3.Name, directoryInfo2.Name + "-natives", false) == 0 || Operators.CompareString(directoryInfo3.Name, "natives-windows-x86_64", false) == 0)
											{
												num += ModBase.DeleteDirectory(directoryInfo3.FullName, true);
											}
										}
									}
									finally
									{
										IEnumerator<DirectoryInfo> enumerator5;
										if (enumerator5 != null)
										{
											enumerator5.Dispose();
										}
									}
								}
							}
							finally
							{
								List<DirectoryInfo>.Enumerator enumerator3;
								((IDisposable)enumerator3).Dispose();
							}
							num += ModBase.DeleteDirectory(ModBase.m_DecoratorRepository, true);
							num += ModBase.DeleteDirectory(ModBase._RefRepository + "ProgramData\\PCL\\", true);
							ModMain.MyMsgBox(string.Format("清理了 {0} 个文件！", num) + "\r\nPCL 即将自动重启……", "缓存已清理", "确定", "", "", false, true, true, null, null, null);
							Process.Start(new ProcessStartInfo(ModBase.interpreterRepository));
							FormMain.EndProgramForce(ModBase.Result.Success);
							return;
						}
					}
					ModMain.Hint("请先关闭所有运行中的游戏……", ModMain.HintType.Info, true);
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "清理垃圾失败", ModBase.LogLevel.Hint, "出现错误");
				}
				finally
				{
					ModBase.RunInUiWait((PageOtherTest._Closure$__.$I15-2 == null) ? (PageOtherTest._Closure$__.$I15-2 = delegate()
					{
						if (ModMain._StrategyIterator != null && ModMain._StrategyIterator.BtnClear != null)
						{
							ModMain._StrategyIterator.BtnClear.IsEnabled = true;
						}
					}) : PageOtherTest._Closure$__.$I15-2);
				}
			})) : PageOtherTest._Closure$__.$I15-1, "Rubbish Clear", ThreadPriority.Normal);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x00009554 File Offset: 0x00007754
		private void BtnMemory_Click()
		{
			ModBase.RunInThread((PageOtherTest._Closure$__.$I16-0 == null) ? (PageOtherTest._Closure$__.$I16-0 = delegate()
			{
				PageOtherTest.MemoryOptimize(true);
			}) : PageOtherTest._Closure$__.$I16-0);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0007044C File Offset: 0x0006E64C
		public static void MemoryOptimize(bool ShowHint)
		{
			checked
			{
				if (PageOtherTest._ParserMapper)
				{
					if (ShowHint)
					{
						ModMain.Hint("内存优化尚未结束，请稍等！", ModMain.HintType.Info, true);
						return;
					}
				}
				else
				{
					PageOtherTest._ParserMapper = true;
					long num;
					if (ModBase.IsAdmin())
					{
						num = (long)MyWpfExtension.ManageParser().Info.AvailablePhysicalMemory;
						try
						{
							PageOtherTest.MemoryOptimizeInternal(ShowHint);
						}
						catch (Exception ex)
						{
							ModBase.Log(ex, "内存优化失败", ShowHint ? ModBase.LogLevel.Hint : ModBase.LogLevel.Debug, "出现错误");
							return;
						}
						finally
						{
							PageOtherTest._ParserMapper = false;
						}
						num = Convert.ToInt64(decimal.Subtract(new decimal(MyWpfExtension.ManageParser().Info.AvailablePhysicalMemory), new decimal(num)));
					}
					else
					{
						ModBase.Log("[Test] 没有管理员权限，将以命令行方式进行内存优化", ModBase.LogLevel.Normal, "出现错误");
						try
						{
							num = unchecked((long)ModBase.RunAsAdmin("--memory")) * 1024L;
						}
						catch (Exception ex2)
						{
							ModBase.Log(ex2, "命令行形式内存优化失败", ModBase.LogLevel.Debug, "出现错误");
							if (ShowHint)
							{
								ModMain.Hint(string.Concat(new string[]
								{
									"获取管理员权限失败，请尝试右键 PCL，选择 ",
									Conversions.ToString(ModBase.callbackRepository),
									"以管理员身份运行",
									Conversions.ToString(ModBase.m_TemplateRepository),
									"！"
								}), ModMain.HintType.Critical, true);
							}
							return;
						}
						finally
						{
							PageOtherTest._ParserMapper = false;
						}
						if (num < 0L)
						{
							return;
						}
					}
					string @string = ModBase.GetString((long)MyWpfExtension.ManageParser().Info.AvailablePhysicalMemory);
					ModBase.Log(string.Format("[Test] 内存优化完成，可用内存改变量：{0}，大致剩余内存：{1}", ModBase.GetString(num), @string), ModBase.LogLevel.Normal, "出现错误");
					if (num > 0L)
					{
						if (ShowHint)
						{
							ModMain.Hint(string.Format("内存优化完成，可用内存增加了 {0}，目前剩余内存 {1}！", ModBase.GetString((long)Math.Round(unchecked((double)num * 0.8))), @string), ModMain.HintType.Finish, true);
							return;
						}
					}
					else if (ShowHint)
					{
						ModMain.Hint(string.Format("内存优化完成，已经优化到了最佳状态，目前剩余内存 {0}！", @string), ModMain.HintType.Info, true);
					}
				}
			}
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00070650 File Offset: 0x0006E850
		public static void MemoryOptimizeInternal(bool ShowHint)
		{
			if (!ModBase.IsAdmin())
			{
				throw new Exception("内存优化功能需要管理员权限！\r\n如果需要自动以管理员身份启动 PCL，可以右键 PCL，打开 属性 → 兼容性 → 以管理员身份运行此程序。");
			}
			ModBase.Log("[Test] 获取内存优化权限", ModBase.LogLevel.Normal, "出现错误");
			using (WindowsIdentity current = WindowsIdentity.GetCurrent(TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges))
			{
				PageOtherTest.PrivilegeToken structure = default(PageOtherTest.PrivilegeToken);
				structure.authenticationMap = 1;
				structure.m_AlgoMap = 0L;
				structure._ComparatorMap = 2;
				string text = null;
				string text2 = "SeProfileSingleProcessPrivilege";
				if (PageOtherTest.LookupPrivilegeValueA(ref text, ref text2, ref structure.m_AlgoMap))
				{
					IntPtr token = current.Token;
					bool disableAllPrivileges = false;
					int bufferLength = Marshal.SizeOf<PageOtherTest.PrivilegeToken>(structure);
					IntPtr zero = IntPtr.Zero;
					int num = 0;
					if (PageOtherTest.AdjustTokenPrivileges(token, disableAllPrivileges, ref structure, bufferLength, ref zero, ref num) && Marshal.GetLastWin32Error() == 0)
					{
						goto IL_BD;
					}
				}
				throw new Exception(string.Format("获取内存优化权限失败（错误代码：{0}）", Marshal.GetLastWin32Error()));
			}
			IL_BD:
			if (ShowHint)
			{
				ModMain.Hint("正在进行内存优化……", ModMain.HintType.Info, true);
			}
			int num2 = 2;
			checked
			{
				int num3;
				for (;;)
				{
					ModBase.Log(string.Format("[Test] 内存优化操作 {0} 开始", num2), ModBase.LogLevel.Normal, "出现错误");
					GCHandle gchandle = GCHandle.Alloc(num2, GCHandleType.Pinned);
					num3 = (int)PageOtherTest.NtSetSystemInformation(80, gchandle.AddrOfPinnedObject(), Marshal.SizeOf<int>(num2));
					gchandle.Free();
					if (num3 != 0)
					{
						break;
					}
					num2++;
					if (num2 > 4)
					{
						return;
					}
				}
				throw new Exception(string.Format("内存优化操作 {0} 失败（错误代码：{1}）", num2, num3));
			}
		}

		// Token: 0x06000F1F RID: 3871
		[DllImport("ntdll.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern uint NtSetSystemInformation(int SystemInformationClass, IntPtr SystemInformation, int SystemInformationLength);

		// Token: 0x06000F20 RID: 3872
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern bool OpenProcessToken(IntPtr ProcessHandle, int DesiredAccess, ref IntPtr TokenHandle);

		// Token: 0x06000F21 RID: 3873
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern bool LookupPrivilegeValueA([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpSystemName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpName, ref long lpLuid);

		// Token: 0x06000F22 RID: 3874
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, bool DisableAllPrivileges, ref PageOtherTest.PrivilegeToken NewState, int BufferLength, ref IntPtr PreviousState, ref int ReturnLength);

		// Token: 0x06000F23 RID: 3875 RVA: 0x0000957F File Offset: 0x0000777F
		private void BtnCave_Click(object sender, EventArgs e)
		{
			ModBase.OpenWebsite("https://jinshuju.net/f/esXHQF");
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0000958B File Offset: 0x0000778B
		private void CaveHand()
		{
			this.BtnCave.Visibility = (ModSecret.DeleteReader(null) ? Visibility.Visible : Visibility.Collapsed);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x000707B8 File Offset: 0x0006E9B8
		private void CardCave_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (this._BroadcasterMapper)
			{
				string randomCave = PageOtherTest.GetRandomCave();
				string text = this.LabCave.Text;
				this.LabCave.Text = randomCave;
				this._BroadcasterMapper = false;
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaOpacity(this.LabCave, -1.0, 5, 0, null, false),
					ModAnimation.AaOpacity(this.LabCave, 1.0, 5, 30, null, false),
					ModAnimation.AaOpacity(this.LabCave, -1.0, 5, 70, null, false),
					ModAnimation.AaOpacity(this.LabCave, 0.9, 5, 85, null, false),
					ModAnimation.AaOpacity(this.LabCave, -1.0, 5, 125, null, false),
					ModAnimation.AaOpacity(this.LabCave, 0.8, 5, 145, null, false),
					ModAnimation.AaOpacity(this.LabCave, -1.0, 5, 165, null, false),
					ModAnimation.AaOpacity(this.LabCave, 0.7, 5, 195, null, false),
					ModAnimation.AaOpacity(this.LabCave, -1.0, 5, 210, null, false),
					ModAnimation.AaOpacity(this.LabCave, 0.5, 5, 235, null, false),
					ModAnimation.AaOpacity(this.LabCave, -1.0, 5, 250, null, false),
					ModAnimation.AaOpacity(this.LabCave, 1.0, 1, 400, null, true),
					ModAnimation.AaTextAppear(this.LabCave, false, true, 60, 400, null, false),
					ModAnimation.AaCode(delegate
					{
						this._BroadcasterMapper = true;
					}, 0, true)
				}, "Cave", false);
				this.LabCave.Text = text;
			}
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x000709E4 File Offset: 0x0006EBE4
		public static string GetRandomCave()
		{
			return ModBase.RandomOne<string>(new string[]
			{
				ModBase.RandomOne<string>(new string[]
				{
					"来硬的！",
					"钻石！",
					"我们需要再深入些！",
					"结束了？",
					"见鬼去吧！",
					"君临天下！",
					"与火共舞！",
					"本地的酿造厂！",
					"为什么会变成这样呢？",
					"信标工程师！",
					"不稳定的同盟！",
					"天空即为极限！",
					"甜蜜的梦！",
					"探索的时光！",
					"狙击手的对决！",
					"这是？工作台！",
					"永恒的伙伴！",
					"腥味十足的生意！",
					"结束了。",
					"开始了？",
					"这交易不错！",
					"你的世界！",
					"/summon Creeper ~ ~ ~ {Fuse:0}",
					"MC-98587!",
					"紫黑格子波纹疾走！",
					"命令方块不适合作为武器！",
					"Don't try Minecraft Legend!",
					"新增了一堆 Bug！",
					"Also try Create!",
					"这是刻意的游戏设计！",
					"附魔神圣橡胶树树苗！",
					"有频道的 AE 不是好 AE！",
					"你好中国！",
					"/give @a hugs 64",
					"这是特性！",
					"Minecraft Legend!",
					"Creeper?",
					"Minecraft 2.0!",
					"Hello, Herobrine!",
					"Herobrine...xia?",
					"It's a FEATURE! Not a BUG!",
					"我 Mojang 绝不跳票！",
					"苦力怕！不是爬行者！",
					"蠢虫？毒虫？蠹虫？",
					"粉色羊是隐性纯合子！",
					"Can't keep up! Did the system time change or is the server overloaded?",
					"比钻石更强！",
					"BUGJANG!",
					"猪灵劲曲！",
					"床里面藏着 TNT！",
					"午时已到！",
					"钓鱼有风险！",
					"Deadline 是第一生产力！",
					"一刀 999，装备全靠爆！是兄弟就来砍我！",
					"巧手能绘千秋业，雄心可创万代春！",
					"NVIDIA，fuck you!",
					"荒野更新，但没有荒野。",
					"Nothing is true, everything is feature!",
					"Also try 新闻主页！",
					"新名单！新名单！",
					"夹子！",
					"这个游戏没有 SSR！",
					"光敏性癫痫警告！",
					"CO~ CO~ DA~ YO~",
					"众所周知，炉石传说是免费游戏！",
					"你号没了！",
					"王八坨子号！",
					"A 级记忆删除（物理）！",
					"Also try SCP-CN-660-J!",
					"Command Block Logic!",
					"Also try SCP-CN-048-J!",
					"原神！启动！",
					"任天堂就是世界的主宰！",
					"不要停下来啊！",
					"To be continued.",
					"脑子放假去了！",
					"抱抱是第一生产力！",
					"总会有地上的生灵，敢于直面雷霆的威光！",
					"当你觉得我咕了，但是我没咕，这也是一种咕！",
					"没有钻石块的可以用下界合金块代替！",
					"心脏停跳文学社！",
					"我 Forge 打开这么慢一定是你启动器出 Bug 了！",
					"50382 警告！",
					"Priority Crash Launcher!",
					"破喉咙！破喉咙！破喉咙！",
					"你知道吗：燕双鹰的手枪永远保持机枪的射速，并且不会过热！",
					"那就当做没有 Bug 好了！",
					"这句话真的有十个字（吗",
					"向骰子低头！",
					"隐藏主题解锁给点提示行不行啊 QAQ！",
					"破产了启动器！",
					"泡菜龙启动器！",
					"劈柴驴启动器！",
					"碰瓷狼启动器！",
					"你这 MC 几块钱一斤？\\n89 块钱一斤。\\n你这地图是金子做的还是玩法是金子做的？",
					"Keep your DETERMINATION.",
					"我东方永不过气！",
					"PPCCLL!",
					"Let The Bass Kick...",
					"数据包是一个高级且优雅的东西，学这个优雅的东西些许掉发，但是我一定能接受！"
				}),
				ModBase.RandomOne<string>(new string[]
				{
					"Also try 探索起源！",
					"Also try Celeste!",
					"Also try The Witness!",
					"Also try gamejam!",
					"RTX ON!",
					"要恰饭的嘛！",
					"爆炸就是艺术，当量就是正义！",
					"帅得体面，猫得明白！",
					"王泪天下第一！",
					"FLAG IS WIN!",
					"凌波微步，快乐的舞步！",
					"这个叫 TAS 的人是不是开挂了？",
					"蜜雪冰城甜蜜蜜！",
					"倒一杯卡布奇诺！",
					"59 区废墟的红刀哥！",
					"Hello, world!",
					"把龙猫拖去祭天！",
					"还真有傻子用 23.33 块钱去买回声洞……",
					"五光十色的白！",
					"请为坠落的龙猫取名……",
					"一脸玄素！",
					"最强的法术是掉线法术！",
					"我能怎么办啊，我也很绝望啊！",
					"保护我的敌人，痛击我的队友！",
					"掉帧是道具吗！",
					"你不是挂怎么和我们打！",
					"叮叮叮叮！",
					"无阻力滚轮！",
					"发出死了的声音！",
					"打不过就丢人，骂人！",
					"要致富，先撸树！",
					"众所周知，我的世界的开发商是 BugJump。",
					"别杀怪物，你这个海豚！",
					"据说咕咕叫的鸽子最适合炖，炖起来非常美味。",
					"千万得点！",
					"是的，这是一条用来凑数的信息。",
					"你说的对但是你说的对所以你说的对！",
					"你必封印在众人梦中散布瘟疫的障目之光。",
					"卡其脱离太！",
					"不要停下来啊！",
					"Professional Crush Launcher!",
					"战神与他的文盲老父亲！",
					"锟斤拷锟斤拷烫烫烫",
					"玄素黑 = 黑白黑 = 奥利奥！",
					"我跟你讲你这样是要被夹的！",
					"中国移动联通电信联合提醒：警惕 Never Gonna Give You Up 诈骗！一旦中招后果惨重！",
					"Missing No.",
					"管管孩子，救救游戏！",
					"Ori2 天下无敌！",
					"气人安卓，在线谈崩！",
					"希望人有事.jpg",
					"卧槽，不是吧，这……",
					"FAKE NEWS!",
					"非法吟诗 ×",
					"禁止套娃 禁止套娃 禁止套娃 awa",
					"这是想上骰子的 SS！",
					"打开浏览器 - 狗屁不通文章生成器 - 随便复制一段",
					"这里可以写广告嘛？",
					"PCL＝PCL1＋PCL1",
					"我差一血打死怪，怪差一血不打死我！！",
					"不打钱就削土豆！",
					"众所周知，1+1=王！",
					"登录镜像世界，同调生物 阿尔法超活性化，连接！",
					"回声洞草。",
					"PCL 服务器第一朵蘑菇云始于 Candy_Pink！",
					"The shadow?（疑惑地）",
					"欢迎使用忘却的旋律启动器！",
					"Minecraft 2.0 正在启动中！",
					"Link Start!",
					"Completely shocked!",
					"林肯死大头！",
					"FBI! OPEN THE DOOR!",
					"保卫萝卜最新作公布！与伯特们并肩作战吧！",
					"你所做的一切，都是在重复昨天。",
					"不是每个东北人都会二人转！",
					"但是天津人真的会相声！",
					"有地域歧视的游戏不是好游戏！",
					"麻辣鸡翅真好吃！",
					"MCD 被网易收走了呢。",
					"薅 羊 毛",
					"后浪们已经涌入 PiliPili，你还不入海吗？",
					"玩我的世界地下城吗？",
					"麻辣鸡腿也不错，但是太多肉了。",
					"Wake up.",
					"It's time to go to school.",
					"Did you finished your homework?",
					"凋 零 残 响",
					"有的人毕业了，有的人还没开学。",
					"在 38 度的太阳底下放寒假。",
					"一条有先见之明的消息对你说了先见之明这四个字。",
					"红鲤鱼与绿鲤鱼与旅驴与铝绿与氯绿……",
					"优质回答：我不知道",
					"大人，食大便了。",
					"不挖坑，毋宁死！",
					"人生无味，夜空无明，原野无火。",
					"高锰酸钾滴眼睛！",
					"丢骰子 2.0！",
					"AMD YES!",
					"啊这……",
					"请不要在给我识别码的时候混进去空格！",
					"oreoreoreorerererereoooooorereoreo",
					"龙猫都写了些什么 233333",
					"《龙猫观察日记》！",
					"过个桥还要上次天！"
				}),
				ModBase.RandomOne<string>(new string[]
				{
					"基岩真好吃！",
					"弱小和无知不是生存的障碍，傲慢才是。——《三体》",
					"在没有错误日志的情况下诊断任何问题无异于闭眼开车。",
					"PCL 咕测群！",
					"希望可爱的龙猫猫能更容易看懂自己写的代码。",
					"PCL 的代码只有龙猫和上帝知道，然而龙猫忘记了。",
					"挖三填一：刚进入 MC 的玩家可能都不知道这个游戏存在危险，只知道东看看西瞅瞅，所以到了晚上的时候才知道 MC 的可怕。",
					"Also try Terraria.",
					"龙猫画的饼很大！",
					"事实证明，乱打序顺会不影响的人阅读。",
					"你知道龙猫 MC 的 ID 是 LTCat 吗？",
					"据我所知，这些材料至少得用一背包或者一大箱子的石头，都有这么多石头了，干嘛还要去做刷石机？",
					"考古模块……加载失败\\n更多生物模块……失去链接\\n收纳袋……已失联\\n更好的战斗系统……未响应",
					"龙腾猫跃 > 龙猫 > lm",
					"你知道吗：2022 年 3 月 10 日 Mojang 账号是最后自愿迁移至 Microsoft 账号的期限。",
					"日常踩雷！",
					"当你看到这条回声洞，你就看到了这条回声洞。",
					"《关于解锁档位为了投稿回声洞，然后发现不知道说啥这件事》",
					"不要尝试在岩浆里喝热水！",
					"博士，您还有许多事情需要处理，现在还不能休息哦。",
					"我就是叫紫妈怎么了，有本事从我背后出现然后把我的脸按在键盘aoikxznwgp",
					"我的人品值很差，只有 1 的 100 倍（bushi）",
					"哎嘿红军战士骑马飞奔向前！",
					"Also try Minecraft with RTX!",
					"public static void main(String[] args) {}",
					"Ctrl + S!",
					"九头蛇万岁！",
					"反手一个超级加倍，闷声发大财。",
					"回声洞好好玩啊 2333",
					"↑↑↓↓←→←→BABA",
					"兽人永不为奴！（除非包吃包住）",
					"少前药丸",
					"咕嚕靈波~（｡•ω•｡)つ━☆.. ・*",
					"建筑，我所欲也，红石，亦我所欲也；二者不可得兼……喊俩大佬就可以得兼了。",
					"我的梦想是天天咕咕咕（逃",
					"锟斤拷锟斤拷䵣笓靹攮濄魊！",
					"* It fills you with determination.",
					"点击千万别点，可以获得所有主题。",
					"I want to play a game.",
					"十七张牌你能秒我？你能秒杀我？",
					"温馨提示：你可点击红色按钮，剩余要靠你的智慧啦~",
					"韩信带净化，虽然不是同一时间，但是是同一厕所。",
					"使用左键以和铁傀儡友好交流！",
					"熠熠生辉——眼前一亮！",
					"THE END IS NEVER THE END IS NEVER THE END IS NEVER THE END",
					"雄火龙又双叒叕被绿了。",
					"净他妈扯淡！",
					"乌拉！！！！！",
					"不要尝试带着猫挑衅苦力怕！",
					"Welcome to……O…PCL II！",
					"快来试试 PCL 下载器！",
					"Technoblade Never Died!",
					"新的 Bug 删除了，旧的 Bug 增加了。",
					"我知道你想睡觉了，但是，不熬夜游戏不会健康。",
					"Death is not an escape.",
					"你知道吗？你不知道。",
					"欸我 10000000 Mods 的整合包打不开了，这启动器不行啊。",
					"你知道吗？人会对看到的文字进行自动排序。别读了，你再读一遍也是这样。",
					"回声~回声~回声~",
					"360：我 TM 觉得你很可疑",
					"你知道吗？现在 PCL II 支持将 Mod 拖动到窗口添加了！",
					"爷爷，你关注的龙猫终于更新啦！",
					"rm -rf /*",
					"半命无出三，故吾曰 G 胖不三。PCL 不出三，是龙猫不三乎？曰：非然也。",
					"让人类永远保持理智是一种奢望！",
					"点千万别点会发生好玩的事！",
					"PCL 是我的世界启动器，不是下载器！",
					"你知道吗？Pot Player 比 Windows Media Player 好用多了！",
					"* 你赞助了龙猫，这使你充满了决心",
					"希望开发者的权益不会受到侵害！awa……",
					"KO NO DIO DA!",
					"我可是要成为 Bug 王的游戏！",
					"* 还剩 17 个。=)",
					"哇！金色传说！",
					"* 你认为即使最坏的人都有变好的机会吗？",
					"我绝对不会因为回声洞给龙腾猫跃发电的！",
					"我绝对不会发回声洞的！",
					"爷想被夹！",
					"淡黄的长裙~蓬松的头发~",
					"奇怪的知识增加了！",
					"小朋友，你是否有很多问号？",
					"小问号，你是否有很多朋友？",
					"在？不在。",
					"阿 Sir，不会吧？",
					"PCL = Perfect C++ Library（雾）",
					"我单杀 42 奶奶，Boss 做得到吗？\\n—— 某闪避率高达 70% 的大闪避海嗣",
					"Get Over Here!",
					"天青色等烟雨，而我在等龙猫更新！",
					"震惊龙猫一整天！",
					"你看这个光影多棒啊，开一下试试，反正电脑好，诶我主机怎么烧",
					"PCL 高速下载器！",
					"右键开始游戏按钮是没有彩蛋的！",
					"> 点此启动内置小游戏 <",
					"衬衫的价格是九镑十五便士。",
					"「胜败乃兵家常事，但是下一次我们会赢回来的！」",
					"建议用脑壳想想再干事，脑壳别那么铁。——龙腾猫跃",
					"然则天下之事，但知其一，不知其二者多矣，可据理臆断欤？",
					"Java (TM) SE binary 未响应\\n· 尝试恢复此程序\\n· 等待程序响应\\n· 关闭程序",
					"我这里有负荆请罪 IV、弹射物吸引 V、经验腐蚀的钻石甲，要吗？（",
					"Plain musiC pLayer 2!"
				}),
				ModBase.RandomOne<string>(new string[]
				{
					"犹豫，就会白给！",
					"PCL YES!",
					"龙猫是龙还是猫？",
					"你吼辣么大声干什么嘛！",
					"我爱吃薯条 awa",
					"我不做人啦！JOJO！！！",
					"哇，这就是回声洞里面的感 juo 吗？",
					"你这什么游戏啊？手机能玩吗？要不要钱啊？",
					"Press F to enter the tank!",
					"多行不义必自毙。",
					"活跃橙好难拿！",
					"GOLDEN AGE WILL RETURN AGAIN",
					"自从玩了我的世界，牛顿的棺材板就再也合不上了！",
					"不要停下来啊！（指咕咕咕）",
					"在我没想到好的留言之前请不要把我放到回声洞中！",
					"咕咕咕鸽鸽鸽嗝！",
					"3T3B",
					"因为匆忙制作的 PCL 启动器，我的身体已经菠萝菠萝哒了！\\n回：这条是投稿，不是我写的.jpg",
					"在游戏尽头的城市！",
					"幻翼感受到了动能……",
					"你还在用 PCL II？ 我已经用 PCL Max Pro 了！",
					"Nobody knows PCL better than me!",
					"当你残血时，小白就变成了神射手！",
					"/kill @e[type=creeper]",
					"/gamemode 1",
					"古代三百勇士可砍下一座城池，现代三百勇士砍不下一部手机。",
					"杀手皇后，第三炸弹，败者食尘！姨妈大！",
					"Mojang 作为英语或瑞典语时的读法不一样哦。",
					"主页自定义看起来很难，但是你仔细研究发现你可以套娃。",
					"众所周知，别人的世界和我的世界不是同一款游戏。",
					"PCL 盒子！",
					"你的好友 xxx 正在游戏\\nWallpaper Engine",
					"ya ri ma su ne~",
					"呐呐呐。银家正在艾特你捏，发起了自个儿跟自个儿唠嗑儿的唠嗑儿。",
					"破产了 2 启动器，你值得拥有。",
					"Wryyyyyyyyyyyyyy",
					"您，人？神！砰砰砰！",
					"龙腾猫跃最棒了！トトロが一番跳ねる！",
					"欧皇！！！",
					"* 看到这么多沙雕网友的留言，使你充满了决心",
					"DUANG~",
					"天呐，那是鸡还是鸭！",
					"Mojang 在瑞典语中意思是东西！",
					"所以……我不知道有啥好说的，反正氵一个留言就行了 qwq",
					"众所周知，地狱有水。",
					"Also try 看云模拟器（bushi）！",
					"游戏一小时，看云 59min。",
					"我们需要再深入些。抱歉，今天不行。",
					"阿伟你又在点回声洞了哦，休息一下吧，去看看书好不好？",
					"TIPS: 大量的红键会使屏幕起火！",
					"喵呜~你好可爱~",
					"如果游戏没有声音可以尝试按下 F3+S！",
					"今早雾霾蔽日，但是不要害怕，太阳依旧在云端！",
					"犹豫就会白给！",
					"你知道吗？PCL 的前身是 PCL1！",
					"Mojang AB = Mojang And Bug",
					"Point Cloud Library 启动器！",
					"SCP 基金会已介入调查！",
					"很快就到你家门口挖矿！",
					"10 岁以下的儿童不宜食用小块块！（PS：我的世界 ESRB 分级为 10+）",
					"Removed Herobrine!",
					"PCL 是个我的世界启动器吗？不，是音乐播放器，Mod 下载器和下载软件。",
					"龙猫的解密也太难了点吧！",
					"一定要点上面那个千万别点！！！",
					"手持两把锟斤拷，口中疾呼烫烫烫。脚踏千朵屯屯屯，笑看万物锘锘锘。",
					"白帝圣剑！御剑跟着我！",
					"今晚，深渊结算。",
					"(let ([s \"(let ([s ~s]) (printf s s)\"]) (printf s s)",
					"花开花落，再灿烂的星光也终将消失。",
					"DNF IS TRUE!!!",
					"欢迎使用史上最复杂的解密启动器！（doge）",
					"原始人，起洞！",
					"某黑客：你会编程吗？龙猫：我不会，PCL 不是我编的。",
					"* (通过对着语音转换系统乱叫，这只龙猫偶然编出了这个软件。)",
					"宇宙多么浩瀚，偏偏我们在此相遇！",
					"UGxhaW4gQ3JhZnQgTGF1bmNoZXIgMu+8gQ==",
					"人品测试，每天一次！",
					"彭翠兰启动器！",
					"生而为人，我很抱歉！（看到这条留言的人一定很温柔吧！）",
					"没错，是百分制√",
					"ckya blyat!",
					"好快の刀！",
					"freedom d↓ve！",
					"这是一条回声！",
					"泷泽萝拉哒！！！",
					"今天你猫车了吗？",
					"我国有一套完整的未成年人保护法！",
					"在内群有个人经常会把龙猫称为游戏《黎明杀机》里面的夹子杀手……",
					"回声洞\u3000\u3000\u3000\u3000\u3000\u3000\u3000投稿",
					"初音未来，我好喜欢你啊（",
					"典明粥的制作配方：花京院典明的爱心宝宝粥*1 + 替身使者 死神13 的便便*1",
					"二次元，金发，吸血鬼，可爱……没错，这些都是在形容迪奥·布兰度。",
					"我在想为什么没有人一开始就用最强攻击呢？",
					"花儿在绽放，小鸟在歌唱……",
					"就 该 在 地 狱 中 燃 烧",
					"腾讯** 百度网盘**",
					"我的世界一直都不只是一款马赛克游戏！",
					"聆听怒海潮生，长空雷震，獠牙在耳，万籁黄昏，最动人，莫过喧嚣红尘。",
					"这次一定！",
					"繚乱！虹ヶ咲！"
				}),
				ModBase.RandomOne<string>(new string[]
				{
					"PCL 中还有两种隐藏主题：真·滑稽彩和星空蓝。要怎么获得他们呢？提示：你猜！",
					"秒了，有什么好说的。",
					"塔塔开！塔塔开！",
					"平角裤，平角裤！",
					"白日放鸽须纵酒，龙猫作伴好还乡。",
					"龙猫说快改完 Bug 了，他一定没改完，就像老婆饼里没有老婆，康师傅牛肉面里没有牛肉。",
					"当你觉得臭鸽子龙猫又要咕咕咕的时候他真的咕咕咕了，这亦是一种不咕。",
					"December=Dec",
					"左手画圆，右手画方！",
					"咕咕咕咕？咕咕咕咕咕咕！咕咕咕咕咕咕咕？？？咕咕咕！（咕咕咕咕咕咕咕咕咕",
					".. -....- .-.. --- ...- . -....- .--. -.-. .-.. ..---",
					"芜湖，起飞！",
					"努力不一定会成功，但一定会有结果，无论好坏输赢，都不会后悔，因为，你也曾为此奋斗劳累拼命，人生千姿百态，努力一次，谁知怎样呢。",
					"I see the player you mean.",
					"催更催更，我是急急国王！",
					"精 神 小 伙",
					"总有些话，是反的，是倒的！",
					"大图？高清？",
					"我的世界是好游戏啊，再差的电脑也能带起来，再好的电脑也带不动……",
					"龙猫：太复杂了，不修了，这是特性，特性！",
					"Why do you see this? Shouldn't you play your game?",
					"你给路达哟！不说了开游戏了，林肯死大头！",
					"龙猫的今日人品我们谁也不知道！",
					"我们是如何走到这一地步的？",
					"有个按钮可以让启动器缩小，旋转……",
					"众所周知，电子游戏不需要视力！",
					"We're no strangers to love~",
					"还记得第一次玩我的世界的时候吗？那曾是很美好的回忆……",
					"该说什么好呢 说什么好呢 什么好呢 么好呢 好呢 呢 （回声真大",
					"如果你找不到第三方登录的话不要到版本选择点那三个点，更不要点那个设置，就算你点了那千万不要往下翻。",
					"千万别点千万别点！",
					"El Primo!!!",
					"在时间的流逝中，没有什么事是一成不变的。",
					"The Escapists 2？彳亍，开始逃狱！",
					"熬夜对身体不好，所以我建议你们……玩通宵。",
					"啊wee改哈鞥嫦娥我刚不疤痕处哈维楚王嗡阿格王朔！！！",
					"对于一个像我一样身高两米一的巨人来说，挖三填一是不可取的。",
					"众所周知，柯南和基德才是真爱！",
					"雷石东直放站！",
					"现在玩的嗨，待会被夹更嗨！",
					"GrandpaBr!",
					"人群当中钻出来一个光！头！",
					"啊呜呜呜~~",
					"伊莉雅：美游来过这里吗？",
					"买不起正版的穷鬼举个爪。",
					"17 张牌你能秒我？你能秒杀我？",
					"有——回——声——吗—— 有-回-声-吗- 有回声吗",
					"Notch is coming back!",
					"夹子启动器！",
					"我不会告诉你，鼠标悬浮在隐藏主题上，会显示出什么不可告人的秘密！",
					"国足 NB！",
					"结束了？不，你还有很多事要做，现在还不能休息哦……",
					"勇士总冠军，库里 FMVP！",
					"握~着~我~的~抱~枕~",
					"我爱吃滑稽果！",
					"跟你们讲一个笑话：龙猫",
					"你每天要忘记成千上万件事，为什么不把这件事也忘了。",
					"迫害群友需谨慎，不然夹子就离你不远了 awa",
					"Minecraft: Dungeons 又名 我的裤子动了！",
					"100 年清朝老兵，申请出战！",
					"你现在不能睡觉，你的朋友在开派对！",
					"木叶飞舞之处，火亦生生不息！",
					"我爱学习，学习使我妈快乐，我妈快乐，全家快乐！",
					"人被逼急了啥都能做出来，除了数学题！",
					"M to the C to the V！",
					"* 保持你的决心，FUCK！",
					"向鸽者文明致敬！",
					"* 今天是多么美好的一天啊。小鸟在歌唱，花朵在绽放。在这样的一天里，像你这样的孩子……应当被龙猫夹起来扔垃圾桶里。",
					"关掉，关掉，一定要关掉！",
					"远古残骸真的存在吗？",
					"啊呦 EVERYBODY 在你头上暴扣！",
					"Plain Craft Launcher→PCL→PC L→电脑 L→电脑垃圾\\n思考.jpg",
					"月色与雪色之间，你是第三种绝色。",
					"群服务器，时不时来群里 Can't keep up！",
					"或许你并不是不想睡觉，而是周围有怪物在游荡！",
					"我们的 LTW 真是太好玩了！",
					"阿瓦达啃大瓜！",
					"非酋该怎么在这个世界上生存（小声）……",
					"妇 科 圣 手",
					"少年没有乌托邦！",
					"嗨，同志，您知道列宁格勒和斯大林格勒在哪吗？我在地图上找不到它。",
					"生活就像打电话，不是你先挂就是我先挂！",
					"好好睡一觉， 就是人生的重启方式呀。",
					"为什么披萨会考糊？",
					"老鼠偷了大米，人们说它狡猾；人类偷了蜂蜜，却说蜜蜂勤劳。",
					"千万别点千万别点千万别点！",
					"CHINA!!! CHINA!!! CHINA!!!",
					"看着龙猫的秀发，我不禁陷入沉思……哦！原来龙猫没有头发！",
					"休息区里有一个特殊的休息室，通往整个后室里最棒的派对喔！=)",
					"咖啡党永不为奴！",
					"再点一下吧！",
					"这个回声洞莫得 CD，定个小目标，点它一亿下！",
					"Tip: 小鸽子们不要挑食哦，不管是烤鸭还是欧芹都要吃~",
					"到点了，Visual Studio 上号！",
					"人间，处处是仙境，何欲而求天？已有大于未有，莫要失去而追悔莫及！",
					"芜湖~咕咕咕起飞~",
					"死机蓝！",
					"Tip: 热知识：这是一条…烫烫烫烫烫！的热知识！",
					"再次点击查看下一位沙雕网友乱七八糟的留言！",
					"咕咕咕——\\n翻译：鸽，下次也不一定。"
				}),
				ModBase.RandomOne<string>(new string[]
				{
					"在 MC 中，沙子可以下落，说明 MC 还是很科学的！（确信",
					"唔咕，要饭大失败……（眼神死",
					"你蓝了，你白了，你没了！",
					"这是回声洞~是回声洞~回声洞~声洞~洞~",
					"往往结束才是开始！",
					"检测到 Minecraft 进程意外退出，错误分析已开始……",
					"你不能游荡，周围有怪物在休息！",
					"玻璃，放错，退游，一气呵成！",
					"你已被服务器封禁！理由：请自证 1145 CPS！",
					"当你打开这条回声洞的时候……还不快去想想你的作业写完了没！",
					"看到这条留言，请为疫情中献身的人们默哀一分钟，并献出至高的敬意。",
					"投稿回声洞的人数为 n，你看到这条的概率是 n 分之一，所以能看到这条的都是欧皇！",
					"汀！汀！莱万汀！汀！汀！莱万汀！",
					"直到我的钱包中了一箭！",
					"阿能我老婆！阿噗噜派！",
					"游艺街✘ 戒赌街✔",
					"忠告：请不要在喝水的时候看回声洞，否则所造成的一切后果与 PCL II 无关。",
					"生活枯燥无味，龙猫模仿人类！",
					"龙猫不是鸽子，只是太忙了（确信）！",
					"在这个时候，你的人品值会是：100！100！100！！！！！",
					"问：龙猫今天的人品值是多少？\\n答：我连宇宙尽头在哪里都不知道，怎么会知道这个。",
					"愿世界永葆和平！",
					"ENDERMAN PENTA KILL! ACE!",
					"众里寻他千百度。蓦然回首，那人却在，灯火阑珊处。",
					"前进，然后变得更好！",
					"小丑竟是你自己！",
					"敲传统木鱼，见观音如来。\\n敲电子木鱼，见初音未来，法号弥苦。",
					"The cake is a lie.",
					"别戳了，这里没有你想找的东西！",
					"lm：蓝猫",
					"主不在乎。",
					"抱歉，今天不行？不吃这套，谢谢。",
					"做工程是不可能不咕的啦，这辈子都不可能不咕的啦。",
					"挖三填一！",
					"Make Minecraft Great Again!",
					"众所周知，阳光菇不爱阳光。",
					"要是哪一天我电脑打 MC 炸了我都不稀奇！",
					"你笨拙的表现犹如黏糊的麦片粥，继续努力吧！",
					"恭喜你，你的鼠标左键没坏！",
					"炮造毕，何不置珍珠？",
					"祝我下个池子出水大叔！大叔，为了你，对蓝色恶魔使用石头吧！",
					"Minecraft 1.7.10 - 单人游戏（未响应）",
					"我趣，是吴奇隆！",
					"悲しみの...向こうへと...",
					"* 移除了 Herobrine\\n* 修复了一个 Bug\\n* 增加了一个Bug",
					"长官，我们双脚着地，率先踏入地狱！",
					"Plain Craft Launcher 的中文翻译是：普通飞行器发射器！",
					"特别是其搭载 690 战术核显卡的改进版本，一发就可以摧毁一个航母战斗群。",
					"天上的卡兹不说话，地上的刀哥想妈妈（doge）！",
					"要用咕咕对抗咕咕。——鲁迅",
					"We are the universe. We are everything you think isn't you.\\n——终末之诗",
					"众所周知，在服务器中按 Alt+F4 可以开启飞行！",
					"下降率大点没事！",
					"快门一按，行车中断，造成事故，移交法办！",
					"中国联通提醒您：警惕移动电信诈骗！",
					"就我个人来说，PCL 很好用对我的意义，不能不说非常重大。这样看来，就我个人来说，PCL 很好用对我的意义，不能不说非常重大。而这些并不是完全重要，更加重要的问题是，莎士比亚曾经说过，意志命运往往背道而驰，决心到最后会全部推倒。这句话语虽然很短, 但令我浮想联翩。",
					"夹子，夹子，更多的夹子，夹子在蔓延……",
					"谁言别后终无悔，寒月清宵绮梦回；深知身在情长在，前尘不共彩云飞。",
					"Bugjump 自古特性多，可与育碧争霸王！",
					"众所周知，Bug 修掉一个还会有第二个 Bug 伴随着修掉的 Bug 出现！",
					"芜湖，我直接成为懒狗起飞！",
					"邪王真眼是最强的！",
					"反馈 Bug 前……先想一想这是不是特性！",
					"这是回声洞还是回字洞？",
					"歪比巴卜？",
					"为什么不试试在调试选项中把动画速度调成 0.1x 或是 3x 呢？",
					"神社倒闭之日。",
					"爱丽丝做的布朗尼果然好吃呢！面团口感湿润但却不发黏，有种清爽的甜味。可可粉是用万豪顿牌的吗？",
					"大大大~大工ong~，你的铠铠甲怎怎么漏漏漏 漏~电啊~\\n——面对二阶段深海骑士的格林",
					"错误代码：-118\\n无法载入网页（未知错误）",
					"获得成就：别人的世界！",
					"你这圣遗物怎么不强化（",
					"你知道吗，当你看了这条信息会发现看了跟不看一个样！",
					"你知道吗？其实你什么都不知道！",
					"冷知识：这其实是一条热知识！",
					"回声洞里面没有米勒星球！",
					"你说对，但原神，米自研，冒险游，提瓦特，神选中，授神眼，引元素。扮角色，邂同伴，击强敌，找亲人，掘真相。",
					"孤独是山峰给予征服者的礼物。",
					"你知道吗：千万别惹玄素，否则会被夹得很惨！",
					"她牵着对立的手。因为她们将会继续前进。\\n因此，就像这样，命运的齿轮在这里继续运转……而远方不会再有等候着的命运。",
					"众所周知，塔科夫是一款恐怖游戏。",
					"Click Circle!",
					"问君能有几多愁？恰似一缸龙猫向东流。",
					"这游戏真凡尔赛！",
					"以声之色，塑花之形，将你之名，刻于我心！",
					"Make Minecraft great again!",
					"任何罪恶终将绳之以法！",
					"看到我了吗？你没有！ヽ(•̀ω•́ )ゝ",
					"Non terrae plus ultra!",
					"中继器是直放站！",
					"Hex Dragon！",
					"什么？Java 版不支持光追？显卡白买了……",
					"育碧就是一颗大土豆！",
					"夹了！都给我夹了！",
					"自从接触了 CraftTweaker，GPT3 人工智能都被折磨疯了。",
					"我起了，一枪秒了，能怎样？",
					"结束了？开始了？不，还没开始呢，咕咕咕！",
					"今日大无语事件：外卖点了个黑椒牛排套餐，结果商家忘了放牛排……",
					"手握两把锟斤拷，口中疾呼烫烫烫，脚踏千朵屯屯屯，笑看万物锘锘锘！",
					"虽然上面那个按钮叫千万别点，但是还有好多人去点它！"
				}),
				ModBase.RandomOne<string>(new string[]
				{
					"damedane, dameyo……",
					"你说得对，但是，后面忘了。",
					"哇！白色普通！",
					"PCL 的蓝色图标是用硫酸铜染色的！",
					"记得使用 Steam 启动 PCL 哦！",
					"果断，就会白给！",
					"各位同学们，作业写完了吗？",
					"I am the storm that is approoooooooooooooooaching!!!!!!!",
					"你不崩谁崩？",
					"天云轻轨交通二号线！开！通！啦！",
					"前有 IDM，后有 XDM，今有 PDM！",
					"玩了红石以后腰不酸了腿不痛了。就是脑袋有点凉。",
					"野蜂飞过，经过了平凡与伟大。却追随着无悔！",
					"竜神の剣を喰らえ！",
					"你有没有听见孩子们的悲鸣！",
					"您这辈子都别想进入 Grand Theft Auto V 在线模式！",
					"我刚太认真写反馈，结果把笔头含嘴里了！",
					"友谊是魔法！",
					"不懂就问，我要问什么？",
					"年轻人不讲武德！",
					"打五把 CSGO！",
					"* 没人知道龙猫收到多少回声洞投稿。\\n* 没人知道有人投了什么投稿。\\n* 没人知道龙猫的心态。\\n* 因为你根本不在乎龙猫。",
					"巨硬公司™ Hugehard™ Huge hardsoft！",
					"建议您 50 包邮并往里面塞 200 元，更容易卖出！",
					"建议您白送我，更容易卖出！",
					"怎样才能让龙猫选择你的投稿？当然是多发几遍！（bushi",
					"什么？这不是饼干，这是我生产的压缩毛巾……",
					"You should try our sister game, Minceraft!",
					"番茄条+土豆酱！",
					"* 你没有看见什么留言，这里只有几根鸽子留下的羽毛。",
					"龙猫什么时候才能整点大活呢（小声",
					"奇变偶不变，_________！",
					"巴山楚水凄凉地，Q 得 cm△t",
					"井底之蛙，不曾见过大海之辽阔，却知晓天空之蓝！",
					"0 errors, 0 warnings!",
					"这个好诶！",
					"我们都是阴沟里的虫子，但总还是得有人仰望星空。",
					"开学愉快！",
					"祂从天空陨落，于是人们看到了神明……",
					"蛋白不会做蛋糕，但他会做糕蛋~",
					"皇帝家是干什么的呢……人人都在叫这个叫皇帝的人，想必干活也一定是用的……额……金锄头？不不不，可能是钴锄头……",
					"温馨提示：按 Alt+F4 有惊喜！",
					"天不生我键盘侠，键道万古如长夜，键来！",
					"E！S！M！跑！！！",
					"你的无畏来源于无知！",
					"我的人品必不可能是 0！系统有 Bug！",
					"二次元，金发，吸血鬼，可爱……没错，这次真的是芙兰朵露了！",
					"祝各位音游人们在今后的打歌过程中好运连连，后宫成群（",
					"如果今天是你的生日，那我祝你生日快乐，如果不是，那我祝你早上中午下午晚上好！",
					"你 要 被 夹",
					"我保留了千万别点，这样你才知道你用的是 PCL。",
					"海皮咳嗽是一个……高 Ping 战士快乐基地。",
					"龙咕（",
					"4 月 1 日打开 PCL 有惊喜！",
					"鸡汤来咯~~~~",
					"猜猜你要点多少次才能再次看到我！",
					"天苍苍，地茫茫，龙猫走路像牛羊。",
					"你充 Minecoin 吗？",
					"今天是个看人品值的好日子啊~",
					"虽然 Java 版不支持光追，但是现在基岩版白送了！哈哈哈哈！",
					"啥时候出 PCL III？\\n回：不可能的……",
					"这里没有人~我们都是鬼~",
					"Long may the sunshine!",
					"If you can.",
					"Tell me your secret.",
					"近朱者赤，近墨者黑。近网易者，就是个寄吧！",
					"【新华字典】里一共有几个字？",
					"寻找远古城市，可以在高海拔地区例如冰封山峰、尖峭山峰、草旬等生物群系往下挖！",
					"我的化学老师说没有 PCl2 这种化学物质！",
					"真的会有傻子买 23.33 来玩回声洞吗？",
					"这解密主题到底怎么解啊啊啊啊啊啊啊！",
					"或许有意义的人生，才是完美的人生！",
					"（阴暗的爬行）（蠕动）（尖叫）（同化）（不分对象攻击）",
					"在吗？明天 DDL 了。",
					"V 我 50 吃个 KFC 谢谢喵！",
					"《我 们 拥 有 最 真 实 的 物 理 引 擎》",
					"爱上一个人是快乐的，但是爱下去让你痛苦了，就要学着放弃，对吧。",
					"我有 20 铁嗷，你怕不怕！（不是",
					"《关于升级后不知道干啥……》",
					"Grove Street, home At least before I fu**ed up these things.",
					"想来把昆特牌吗？我可是村子里最厉害的！",
					"我曾背井离乡，后来全村的人都渴死了。",
					"为 PCL 和伟大的咕咕咕事业而欢呼！",
					"胜利之风，正从我 DIO 背后吹来！",
					"今日人品？",
					"你每天都会忘掉很多事，为什么不把这件事也忘掉呢？",
					"点击千万别点，会送正版哦！",
					"我超！冰！",
					"再次点击这里以查看更多的 PCL 作者和各位沙雕网友乱七八糟的留言！",
					"glvE Me l0m c0iNs 0r Rep0rTinG U",
					"不要再打羊驼了啊！！！",
					"温馨提示：当你拿打火石右键苦力怕时，苦力怕将会消失。",
					"嗨嗨嗨，我的世界好玩吗，不说了，喝鸡汤去了。",
					"你打开内群，试图在群文件找些有用的东西，却发现里面都是错误报告……",
					"沙雕解谜：整个 PCL II 将为你闪烁",
					"TECHNOBLADE YOU NERDSSSSSSS!",
					"你知道吗？龙腾猫跃这个名字实际上是从龙腾虎跃这个名字为基础改过来的哦~",
					"Bug 变 Feature，妙啊！",
					"你在狗叫什么？",
					"听我说谢谢你~因为有你~温暖了四季~"
				}),
				ModBase.RandomOne<string>(new string[]
				{
					ModBase.RandomOne<string>(new string[]
					{
						"不要相信灰色，直接上！",
						"点一下不够就点两下！",
						"今日人品：100！",
						"鱼人节快乐！",
						"滑稽节是一个节日呢！",
						"ASCII 总是三位数！",
						"帮助的英文是 Help！",
						"砸反馈就完事了！",
						"属于宇宙的数字！",
						"众所周知，十大主题一定有十一个！",
						"回声洞能带来灵感！",
						"！读着倒要话候时有",
						"从罗马开始！",
						"MCBBS 的本体是箱子！",
						"化学，文档，网格！",
						"网址就是来路！",
						"越过屏障！",
						"卢恩与去路！",
						"地下埋藏着宝藏！",
						"从老线索中发现新东西！",
						"重组碎片……",
						"橙色线，藏着线和点！",
						"于历史中发掘秘密！",
						"线索在游戏之外！",
						"穷举不能让你变得更强！",
						"OBSIDIAN！",
						"深蓝色的极客！",
						"不要忽视背景！",
						"结束了？开始了。",
						"开始了？结束了。"
					}),
					PageOtherTest.GetRandomPresetHint()
				})
			}).ToString().Replace("\\n", "\r\n");
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x00072424 File Offset: 0x00070624
		public static string GetRandomHint()
		{
			try
			{
				if (File.Exists(ModBase.Path + "PCL\\hints.txt"))
				{
					List<string> list = Enumerable.ToList<string>(Enumerable.Where<string>(ModBase.ReadFile(ModBase.Path + "PCL\\hints.txt", null).Split("\r\n".ToCharArray()), (PageOtherTest._Closure$__.$I30-0 == null) ? (PageOtherTest._Closure$__.$I30-0 = ((string s) => !string.IsNullOrWhiteSpace(s))) : PageOtherTest._Closure$__.$I30-0));
					if (Enumerable.Any<string>(list))
					{
						return ModBase.RandomOne<string>(list);
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "获取自定义 你知道吗 提示失败", ModBase.LogLevel.Hint, "出现错误");
			}
			return PageOtherTest.GetRandomPresetHint();
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x000724E8 File Offset: 0x000706E8
		public static string GetRandomPresetHint()
		{
			return ModBase.RandomOne<string>(new string[]
			{
				"在版本选择页面，右键某个版本也能进入版本设置页面。",
				"你可以在版本设置中调整分类，以将特定版本隐藏。",
				"使用 --memory 参数启动 PCL 可以静默进行内存优化！",
				"在第一次启动游戏时，PCL 会自动将语言设置为中文。",
				"在版本设置中可以开启第三方登录验证，例如 Little Skin 登录！",
				"自动配置游戏内存时，PCL 将会根据剩余内存与 Mod 数量动态决定分配的内存！",
				"主页可以使用特定的预设，在设置中看看吧！",
				"在高级启动选项中，可以设置在游戏启动前运行特定程序！",
				"将鼠标悬浮在设置页的左边栏，可以找到重置设置按钮。",
				"你可以使用自定义主页来自定义快捷方式！",
				"版本设置只对当前版本生效，而设置页面的设置对所有版本生效。",
				"要将已有的 MC 文件夹加入 PCL，可以在版本选择页的左侧选择添加文件夹。",
				"如果同时安装了 OptiFine 与对应的原版，PCL 会展示 OptiFine 版本，折叠原版。",
				"版本选择的 常规版本 分类中，只会列出最新的一个快照或预发布版。",
				"点击版本右侧的心形就能将该版本加入收藏夹，便于查找。",
				"在版本选择页面，右键游戏文件夹可以进行打开、重命名、删除等操作！",
				string.Format("如果你在其他地方修改了皮肤，需要手动选择 {0}刷新头像{1} 才能更新登录页面的头像……", ModBase.callbackRepository, ModBase.m_TemplateRepository),
				"PCL 会自动选择最合适的 Java，不用自己操心……",
				"下载 Mod 时，PCL 会自动定位对应版本的 Mod 文件夹！",
				"如果缺少 Java，PCL 也能自动下载，不必自己安装啦！",
				"如果你打开了调试模式，启动页右侧就会显示启动日志。",
				"将鼠标悬浮在下载页的左边栏，可以找到刷新按钮！",
				string.Format("将鼠标指向下载页的 MC 版本，可以在右侧找到 {0}查看更新日志{1} 选项！", ModBase.callbackRepository, ModBase.m_TemplateRepository),
				"直接把 Mod 或整合包拖进 PCL 窗口就能安装了！",
				string.Format("在 PCL 文件夹下新建 hints.txt，可以自定义 {0}你知道吗{1} 的内容，注意编码需要为 UTF-8！", ModBase.callbackRepository, ModBase.m_TemplateRepository),
				"设置中可以自定义离线皮肤，但这只对单人游戏有效。",
				"如果不想用某项功能，可以在个性化设置中把它隐藏掉！",
				string.Format("如果打开了 {0}游戏更新提示{1} 功能，当 MC 更新时 PCL 会弹窗进行提醒！", ModBase.callbackRepository, ModBase.m_TemplateRepository),
				"如果解锁了赞助主题，就能打开回声洞的投稿入口！",
				"拖拽 PCL 的窗口边缘就能调整窗口大小！",
				"PCL 的第一个内部版本制作于 2018 年 8 月 13 日。",
				"PCL 的开发者只有龙腾猫跃一个人，并不存在什么开发团队。",
				"据调查，有 90.3% 的用户点击了百宝箱中的千万别点按钮。",
				"PCL 的绝大多数代码都在 GitHub 开源了！",
				"PCL 的开发者龙腾猫跃经常被简称为龙猫，但和那只龙猫没有任何关系。"
			});
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x000095A4 File Offset: 0x000077A4
		// (set) Token: 0x06000F2A RID: 3882 RVA: 0x000095AC File Offset: 0x000077AC
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x000095B5 File Offset: 0x000077B5
		// (set) Token: 0x06000F2C RID: 3884 RVA: 0x00072698 File Offset: 0x00070898
		internal virtual MyButton BtnJrrp
		{
			[CompilerGenerated]
			get
			{
				return this._ReaderMapper;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = (PageOtherTest._Closure$__.$IR38-3 == null) ? (PageOtherTest._Closure$__.$IR38-3 = delegate(object sender, MouseButtonEventArgs e)
				{
					PageOtherTest.Jrrp();
				}) : PageOtherTest._Closure$__.$IR38-3;
				MyButton readerMapper = this._ReaderMapper;
				if (readerMapper != null)
				{
					readerMapper.Click -= value2;
				}
				this._ReaderMapper = value;
				readerMapper = this._ReaderMapper;
				if (readerMapper != null)
				{
					readerMapper.Click += value2;
				}
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x000095BD File Offset: 0x000077BD
		// (set) Token: 0x06000F2E RID: 3886 RVA: 0x000726F4 File Offset: 0x000708F4
		internal virtual MyButton BtnMemory
		{
			[CompilerGenerated]
			get
			{
				return this.clientMapper;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.BtnMemory_Click();
				};
				MyButton myButton = this.clientMapper;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.clientMapper = value;
				myButton = this.clientMapper;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x000095C5 File Offset: 0x000077C5
		// (set) Token: 0x06000F30 RID: 3888 RVA: 0x00072738 File Offset: 0x00070938
		internal virtual MyButton BtnClear
		{
			[CompilerGenerated]
			get
			{
				return this.m_ConfigMapper;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = (PageOtherTest._Closure$__.$IR46-5 == null) ? (PageOtherTest._Closure$__.$IR46-5 = delegate(object sender, MouseButtonEventArgs e)
				{
					PageOtherTest.RubbishClear();
				}) : PageOtherTest._Closure$__.$IR46-5;
				MyButton configMapper = this.m_ConfigMapper;
				if (configMapper != null)
				{
					configMapper.Click -= value2;
				}
				this.m_ConfigMapper = value;
				configMapper = this.m_ConfigMapper;
				if (configMapper != null)
				{
					configMapper.Click += value2;
				}
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x000095CD File Offset: 0x000077CD
		// (set) Token: 0x06000F32 RID: 3890 RVA: 0x000095D5 File Offset: 0x000077D5
		internal virtual MyButton BtnUpdate { get; set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x000095DE File Offset: 0x000077DE
		// (set) Token: 0x06000F34 RID: 3892 RVA: 0x00072794 File Offset: 0x00070994
		internal virtual MyButton BtnClick
		{
			[CompilerGenerated]
			get
			{
				return this._MapperMapper;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnClick_Click);
				MyButton mapperMapper = this._MapperMapper;
				if (mapperMapper != null)
				{
					mapperMapper.Click -= value2;
				}
				this._MapperMapper = value;
				mapperMapper = this._MapperMapper;
				if (mapperMapper != null)
				{
					mapperMapper.Click += value2;
				}
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x000095E6 File Offset: 0x000077E6
		// (set) Token: 0x06000F36 RID: 3894 RVA: 0x000095EE File Offset: 0x000077EE
		internal virtual MyCard PanPriority { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x000095F7 File Offset: 0x000077F7
		// (set) Token: 0x06000F38 RID: 3896 RVA: 0x000095FF File Offset: 0x000077FF
		internal virtual TextBlock TextPrioritySum1 { get; set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x00009608 File Offset: 0x00007808
		// (set) Token: 0x06000F3A RID: 3898 RVA: 0x00009610 File Offset: 0x00007810
		internal virtual TextBlock TextPrioritySum2 { get; set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x00009619 File Offset: 0x00007819
		// (set) Token: 0x06000F3C RID: 3900 RVA: 0x00009621 File Offset: 0x00007821
		internal virtual TextBlock TextPrioritySum3 { get; set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x0000962A File Offset: 0x0000782A
		// (set) Token: 0x06000F3E RID: 3902 RVA: 0x00009632 File Offset: 0x00007832
		internal virtual TextBlock TextPrioritySum4 { get; set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x0000963B File Offset: 0x0000783B
		// (set) Token: 0x06000F40 RID: 3904 RVA: 0x00009643 File Offset: 0x00007843
		internal virtual TextBlock TextPriorityLevel { get; set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x0000964C File Offset: 0x0000784C
		// (set) Token: 0x06000F42 RID: 3906 RVA: 0x00009654 File Offset: 0x00007854
		internal virtual MyComboBox ComboPriority1 { get; set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x0000965D File Offset: 0x0000785D
		// (set) Token: 0x06000F44 RID: 3908 RVA: 0x00009665 File Offset: 0x00007865
		internal virtual MyComboBox ComboPriority2 { get; set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x0000966E File Offset: 0x0000786E
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x00009676 File Offset: 0x00007876
		internal virtual MyComboBox ComboPriority3 { get; set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x0000967F File Offset: 0x0000787F
		// (set) Token: 0x06000F48 RID: 3912 RVA: 0x00009687 File Offset: 0x00007887
		internal virtual MyComboBox ComboPriority4 { get; set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x00009690 File Offset: 0x00007890
		// (set) Token: 0x06000F4A RID: 3914 RVA: 0x00009698 File Offset: 0x00007898
		internal virtual MyComboBox ComboPriority5 { get; set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x000096A1 File Offset: 0x000078A1
		// (set) Token: 0x06000F4C RID: 3916 RVA: 0x000096A9 File Offset: 0x000078A9
		internal virtual MyButton BtnPriorityReset { get; set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x000096B2 File Offset: 0x000078B2
		// (set) Token: 0x06000F4E RID: 3918 RVA: 0x000727D8 File Offset: 0x000709D8
		internal virtual MyCard CardCave
		{
			[CompilerGenerated]
			get
			{
				return this.dicMapper;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.CardCave_MouseLeftButtonUp);
				MyCard myCard = this.dicMapper;
				if (myCard != null)
				{
					myCard.MouseLeftButtonUp -= value2;
				}
				this.dicMapper = value;
				myCard = this.dicMapper;
				if (myCard != null)
				{
					myCard.MouseLeftButtonUp += value2;
				}
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x000096BA File Offset: 0x000078BA
		// (set) Token: 0x06000F50 RID: 3920 RVA: 0x000096C2 File Offset: 0x000078C2
		internal virtual TextBlock LabCave { get; set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x000096CB File Offset: 0x000078CB
		// (set) Token: 0x06000F52 RID: 3922 RVA: 0x0007281C File Offset: 0x00070A1C
		internal virtual MyTextButton BtnCave
		{
			[CompilerGenerated]
			get
			{
				return this.m_IssuerMapper;
			}
			[CompilerGenerated]
			set
			{
				MyTextButton.ClickEventHandler value2 = new MyTextButton.ClickEventHandler(this.BtnCave_Click);
				RoutedEventHandler value3 = delegate(object sender, RoutedEventArgs e)
				{
					this.CaveHand();
				};
				MyTextButton issuerMapper = this.m_IssuerMapper;
				if (issuerMapper != null)
				{
					issuerMapper.Click -= value2;
					issuerMapper.Loaded -= value3;
				}
				this.m_IssuerMapper = value;
				issuerMapper = this.m_IssuerMapper;
				if (issuerMapper != null)
				{
					issuerMapper.Click += value2;
					issuerMapper.Loaded += value3;
				}
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x000096D3 File Offset: 0x000078D3
		// (set) Token: 0x06000F54 RID: 3924 RVA: 0x0007287C File Offset: 0x00070A7C
		internal virtual MyTextBox TextDownloadUrl
		{
			[CompilerGenerated]
			get
			{
				return this._IndexerMapper;
			}
			[CompilerGenerated]
			set
			{
				MyTextBox.ValidateChangedEventHandler obj = delegate(object sender, EventArgs e)
				{
					this.StartButtonRefresh();
				};
				KeyEventHandler value2 = new KeyEventHandler(this.TextDownloadUrl_KeyDown);
				RoutedEventHandler value3 = delegate(object sender, RoutedEventArgs e)
				{
					this.TextDownloadUrl_TextChanged(RuntimeHelpers.GetObjectValue(sender), (TextChangedEventArgs)e);
				};
				MyTextBox indexerMapper = this._IndexerMapper;
				if (indexerMapper != null)
				{
					MyTextBox.MapReader(obj);
					indexerMapper.KeyDown -= value2;
					indexerMapper.DestroyReader(value3);
				}
				this._IndexerMapper = value;
				indexerMapper = this._IndexerMapper;
				if (indexerMapper != null)
				{
					MyTextBox.SortReader(obj);
					indexerMapper.KeyDown += value2;
					indexerMapper.SetupReader(value3);
				}
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x000096DB File Offset: 0x000078DB
		// (set) Token: 0x06000F56 RID: 3926 RVA: 0x000728F4 File Offset: 0x00070AF4
		internal virtual MyTextBox TextDownloadFolder
		{
			[CompilerGenerated]
			get
			{
				return this._InterpreterMapper;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = delegate(object sender, RoutedEventArgs e)
				{
					this.SaveCacheDownloadFolder();
				};
				MyTextBox.ValidateChangedEventHandler obj = delegate(object sender, EventArgs e)
				{
					this.StartButtonRefresh();
				};
				KeyEventHandler value3 = new KeyEventHandler(this.TextDownloadUrl_KeyDown);
				MyTextBox interpreterMapper = this._InterpreterMapper;
				if (interpreterMapper != null)
				{
					interpreterMapper.DestroyReader(value2);
					MyTextBox.MapReader(obj);
					interpreterMapper.KeyDown -= value3;
				}
				this._InterpreterMapper = value;
				interpreterMapper = this._InterpreterMapper;
				if (interpreterMapper != null)
				{
					interpreterMapper.SetupReader(value2);
					MyTextBox.SortReader(obj);
					interpreterMapper.KeyDown += value3;
				}
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x000096E3 File Offset: 0x000078E3
		// (set) Token: 0x06000F58 RID: 3928 RVA: 0x0007296C File Offset: 0x00070B6C
		internal virtual MyTextBox TextDownloadName
		{
			[CompilerGenerated]
			get
			{
				return this._SerializerMapper;
			}
			[CompilerGenerated]
			set
			{
				MyTextBox.ValidateChangedEventHandler obj = delegate(object sender, EventArgs e)
				{
					this.StartButtonRefresh();
				};
				KeyEventHandler value2 = new KeyEventHandler(this.TextDownloadUrl_KeyDown);
				MyTextBox serializerMapper = this._SerializerMapper;
				if (serializerMapper != null)
				{
					MyTextBox.MapReader(obj);
					serializerMapper.KeyDown -= value2;
				}
				this._SerializerMapper = value;
				serializerMapper = this._SerializerMapper;
				if (serializerMapper != null)
				{
					MyTextBox.SortReader(obj);
					serializerMapper.KeyDown += value2;
				}
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x000096EB File Offset: 0x000078EB
		// (set) Token: 0x06000F5A RID: 3930 RVA: 0x000729C8 File Offset: 0x00070BC8
		internal virtual MyTextButton BtnDownloadSelect
		{
			[CompilerGenerated]
			get
			{
				return this.watcherMapper;
			}
			[CompilerGenerated]
			set
			{
				MyTextButton.ClickEventHandler value2 = new MyTextButton.ClickEventHandler(this.BtnDownloadSelect_Click);
				MyTextButton myTextButton = this.watcherMapper;
				if (myTextButton != null)
				{
					myTextButton.Click -= value2;
				}
				this.watcherMapper = value;
				myTextButton = this.watcherMapper;
				if (myTextButton != null)
				{
					myTextButton.Click += value2;
				}
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x000096F3 File Offset: 0x000078F3
		// (set) Token: 0x06000F5C RID: 3932 RVA: 0x00072A0C File Offset: 0x00070C0C
		internal virtual MyButton BtnDownloadStart
		{
			[CompilerGenerated]
			get
			{
				return this._IdentifierMapper;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.BtnDownloadStart_Click();
				};
				MyButton identifierMapper = this._IdentifierMapper;
				if (identifierMapper != null)
				{
					identifierMapper.Click -= value2;
				}
				this._IdentifierMapper = value;
				identifierMapper = this._IdentifierMapper;
				if (identifierMapper != null)
				{
					identifierMapper.Click += value2;
				}
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x000096FB File Offset: 0x000078FB
		// (set) Token: 0x06000F5E RID: 3934 RVA: 0x00072A50 File Offset: 0x00070C50
		internal virtual MyButton BtnDownloadOpen
		{
			[CompilerGenerated]
			get
			{
				return this._SystemMapper;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.BtnDownloadOpen_Click();
				};
				MyButton systemMapper = this._SystemMapper;
				if (systemMapper != null)
				{
					systemMapper.Click -= value2;
				}
				this._SystemMapper = value;
				systemMapper = this._SystemMapper;
				if (systemMapper != null)
				{
					systemMapper.Click += value2;
				}
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x00009703 File Offset: 0x00007903
		// (set) Token: 0x06000F60 RID: 3936 RVA: 0x0000970B File Offset: 0x0000790B
		internal virtual MyCard PanEncode { get; set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x00009714 File Offset: 0x00007914
		// (set) Token: 0x06000F62 RID: 3938 RVA: 0x0000971C File Offset: 0x0000791C
		internal virtual MyTextBox txStr { get; set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x00009725 File Offset: 0x00007925
		// (set) Token: 0x06000F64 RID: 3940 RVA: 0x0000972D File Offset: 0x0000792D
		internal virtual MyTextBox txKey { get; set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x00009736 File Offset: 0x00007936
		// (set) Token: 0x06000F66 RID: 3942 RVA: 0x0000973E File Offset: 0x0000793E
		internal virtual MyButton btnSerAdd { get; set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x00009747 File Offset: 0x00007947
		// (set) Token: 0x06000F68 RID: 3944 RVA: 0x0000974F File Offset: 0x0000794F
		internal virtual MyButton btnSerRemove { get; set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x00009758 File Offset: 0x00007958
		// (set) Token: 0x06000F6A RID: 3946 RVA: 0x00009760 File Offset: 0x00007960
		internal virtual MyTextBox txResult { get; set; }

		// Token: 0x06000F6B RID: 3947 RVA: 0x00072A94 File Offset: 0x00070C94
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_DecoratorMapper)
			{
				this.m_DecoratorMapper = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageother/pageothertest.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00072AC4 File Offset: 0x00070CC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (MyScrollViewer)target;
				return;
			}
			if (connectionId == 2)
			{
				this.BtnJrrp = (MyButton)target;
				return;
			}
			if (connectionId == 3)
			{
				this.BtnMemory = (MyButton)target;
				return;
			}
			if (connectionId == 4)
			{
				this.BtnClear = (MyButton)target;
				return;
			}
			if (connectionId == 5)
			{
				this.BtnUpdate = (MyButton)target;
				return;
			}
			if (connectionId == 6)
			{
				this.BtnClick = (MyButton)target;
				return;
			}
			if (connectionId == 7)
			{
				this.PanPriority = (MyCard)target;
				return;
			}
			if (connectionId == 8)
			{
				this.TextPrioritySum1 = (TextBlock)target;
				return;
			}
			if (connectionId == 9)
			{
				this.TextPrioritySum2 = (TextBlock)target;
				return;
			}
			if (connectionId == 10)
			{
				this.TextPrioritySum3 = (TextBlock)target;
				return;
			}
			if (connectionId == 11)
			{
				this.TextPrioritySum4 = (TextBlock)target;
				return;
			}
			if (connectionId == 12)
			{
				this.TextPriorityLevel = (TextBlock)target;
				return;
			}
			if (connectionId == 13)
			{
				this.ComboPriority1 = (MyComboBox)target;
				return;
			}
			if (connectionId == 14)
			{
				this.ComboPriority2 = (MyComboBox)target;
				return;
			}
			if (connectionId == 15)
			{
				this.ComboPriority3 = (MyComboBox)target;
				return;
			}
			if (connectionId == 16)
			{
				this.ComboPriority4 = (MyComboBox)target;
				return;
			}
			if (connectionId == 17)
			{
				this.ComboPriority5 = (MyComboBox)target;
				return;
			}
			if (connectionId == 18)
			{
				this.BtnPriorityReset = (MyButton)target;
				return;
			}
			if (connectionId == 19)
			{
				this.CardCave = (MyCard)target;
				return;
			}
			if (connectionId == 20)
			{
				this.LabCave = (TextBlock)target;
				return;
			}
			if (connectionId == 21)
			{
				this.BtnCave = (MyTextButton)target;
				return;
			}
			if (connectionId == 22)
			{
				this.TextDownloadUrl = (MyTextBox)target;
				return;
			}
			if (connectionId == 23)
			{
				this.TextDownloadFolder = (MyTextBox)target;
				return;
			}
			if (connectionId == 24)
			{
				this.TextDownloadName = (MyTextBox)target;
				return;
			}
			if (connectionId == 25)
			{
				this.BtnDownloadSelect = (MyTextButton)target;
				return;
			}
			if (connectionId == 26)
			{
				this.BtnDownloadStart = (MyButton)target;
				return;
			}
			if (connectionId == 27)
			{
				this.BtnDownloadOpen = (MyButton)target;
				return;
			}
			if (connectionId == 28)
			{
				this.PanEncode = (MyCard)target;
				return;
			}
			if (connectionId == 29)
			{
				this.txStr = (MyTextBox)target;
				return;
			}
			if (connectionId == 30)
			{
				this.txKey = (MyTextBox)target;
				return;
			}
			if (connectionId == 31)
			{
				this.btnSerAdd = (MyButton)target;
				return;
			}
			if (connectionId == 32)
			{
				this.btnSerRemove = (MyButton)target;
				return;
			}
			if (connectionId == 33)
			{
				this.txResult = (MyTextBox)target;
				return;
			}
			this.m_DecoratorMapper = true;
		}

		// Token: 0x0400082A RID: 2090
		private bool procTests;

		// Token: 0x0400082B RID: 2091
		private static bool _ParserMapper = false;

		// Token: 0x0400082C RID: 2092
		private bool _BroadcasterMapper;

		// Token: 0x0400082D RID: 2093
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyScrollViewer _FieldMapper;

		// Token: 0x0400082E RID: 2094
		[CompilerGenerated]
		[AccessedThroughProperty("BtnJrrp")]
		private MyButton _ReaderMapper;

		// Token: 0x0400082F RID: 2095
		[AccessedThroughProperty("BtnMemory")]
		[CompilerGenerated]
		private MyButton clientMapper;

		// Token: 0x04000830 RID: 2096
		[CompilerGenerated]
		[AccessedThroughProperty("BtnClear")]
		private MyButton m_ConfigMapper;

		// Token: 0x04000831 RID: 2097
		[AccessedThroughProperty("BtnUpdate")]
		[CompilerGenerated]
		private MyButton _TestsMapper;

		// Token: 0x04000832 RID: 2098
		[AccessedThroughProperty("BtnClick")]
		[CompilerGenerated]
		private MyButton _MapperMapper;

		// Token: 0x04000833 RID: 2099
		[AccessedThroughProperty("PanPriority")]
		[CompilerGenerated]
		private MyCard _ThreadMapper;

		// Token: 0x04000834 RID: 2100
		[AccessedThroughProperty("TextPrioritySum1")]
		[CompilerGenerated]
		private TextBlock propertyMapper;

		// Token: 0x04000835 RID: 2101
		[AccessedThroughProperty("TextPrioritySum2")]
		[CompilerGenerated]
		private TextBlock m_ComposerMapper;

		// Token: 0x04000836 RID: 2102
		[AccessedThroughProperty("TextPrioritySum3")]
		[CompilerGenerated]
		private TextBlock _IteratorMapper;

		// Token: 0x04000837 RID: 2103
		[AccessedThroughProperty("TextPrioritySum4")]
		[CompilerGenerated]
		private TextBlock m_RepositoryMapper;

		// Token: 0x04000838 RID: 2104
		[AccessedThroughProperty("TextPriorityLevel")]
		[CompilerGenerated]
		private TextBlock _TestMapper;

		// Token: 0x04000839 RID: 2105
		[AccessedThroughProperty("ComboPriority1")]
		[CompilerGenerated]
		private MyComboBox m_MapMapper;

		// Token: 0x0400083A RID: 2106
		[AccessedThroughProperty("ComboPriority2")]
		[CompilerGenerated]
		private MyComboBox _ErrorMapper;

		// Token: 0x0400083B RID: 2107
		[AccessedThroughProperty("ComboPriority3")]
		[CompilerGenerated]
		private MyComboBox m_ContextMapper;

		// Token: 0x0400083C RID: 2108
		[AccessedThroughProperty("ComboPriority4")]
		[CompilerGenerated]
		private MyComboBox m_SpecificationMapper;

		// Token: 0x0400083D RID: 2109
		[CompilerGenerated]
		[AccessedThroughProperty("ComboPriority5")]
		private MyComboBox _MockMapper;

		// Token: 0x0400083E RID: 2110
		[AccessedThroughProperty("BtnPriorityReset")]
		[CompilerGenerated]
		private MyButton requestMapper;

		// Token: 0x0400083F RID: 2111
		[CompilerGenerated]
		[AccessedThroughProperty("CardCave")]
		private MyCard dicMapper;

		// Token: 0x04000840 RID: 2112
		[AccessedThroughProperty("LabCave")]
		[CompilerGenerated]
		private TextBlock helperMapper;

		// Token: 0x04000841 RID: 2113
		[CompilerGenerated]
		[AccessedThroughProperty("BtnCave")]
		private MyTextButton m_IssuerMapper;

		// Token: 0x04000842 RID: 2114
		[CompilerGenerated]
		[AccessedThroughProperty("TextDownloadUrl")]
		private MyTextBox _IndexerMapper;

		// Token: 0x04000843 RID: 2115
		[CompilerGenerated]
		[AccessedThroughProperty("TextDownloadFolder")]
		private MyTextBox _InterpreterMapper;

		// Token: 0x04000844 RID: 2116
		[AccessedThroughProperty("TextDownloadName")]
		[CompilerGenerated]
		private MyTextBox _SerializerMapper;

		// Token: 0x04000845 RID: 2117
		[AccessedThroughProperty("BtnDownloadSelect")]
		[CompilerGenerated]
		private MyTextButton watcherMapper;

		// Token: 0x04000846 RID: 2118
		[CompilerGenerated]
		[AccessedThroughProperty("BtnDownloadStart")]
		private MyButton _IdentifierMapper;

		// Token: 0x04000847 RID: 2119
		[CompilerGenerated]
		[AccessedThroughProperty("BtnDownloadOpen")]
		private MyButton _SystemMapper;

		// Token: 0x04000848 RID: 2120
		[AccessedThroughProperty("PanEncode")]
		[CompilerGenerated]
		private MyCard m_ParamMapper;

		// Token: 0x04000849 RID: 2121
		[CompilerGenerated]
		[AccessedThroughProperty("txStr")]
		private MyTextBox tagMapper;

		// Token: 0x0400084A RID: 2122
		[CompilerGenerated]
		[AccessedThroughProperty("txKey")]
		private MyTextBox _ObserverMapper;

		// Token: 0x0400084B RID: 2123
		[CompilerGenerated]
		[AccessedThroughProperty("btnSerAdd")]
		private MyButton _StubMapper;

		// Token: 0x0400084C RID: 2124
		[AccessedThroughProperty("btnSerRemove")]
		[CompilerGenerated]
		private MyButton _RulesMapper;

		// Token: 0x0400084D RID: 2125
		[AccessedThroughProperty("txResult")]
		[CompilerGenerated]
		private MyTextBox m_RefMapper;

		// Token: 0x0400084E RID: 2126
		private bool m_DecoratorMapper;

		// Token: 0x02000183 RID: 387
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct PrivilegeToken
		{
			// Token: 0x0400084F RID: 2127
			public int authenticationMap;

			// Token: 0x04000850 RID: 2128
			public long m_AlgoMap;

			// Token: 0x04000851 RID: 2129
			public int _ComparatorMap;
		}
	}
}
