using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020001B8 RID: 440
	[DesignerGenerated]
	public class PageSetupUI : MyPageRight, IComponentConnector
	{
		// Token: 0x06001336 RID: 4918 RVA: 0x00085220 File Offset: 0x00083420
		public PageSetupUI()
		{
			base.Loaded += this.PageSetupUI_Loaded;
			base.Loaded += ((PageSetupUI._Closure$__.$IR1-1 == null) ? (PageSetupUI._Closure$__.$IR1-1 = delegate(object sender, RoutedEventArgs e)
			{
				PageSetupUI.HiddenRefresh();
			}) : PageSetupUI._Closure$__.$IR1-1);
			this._ParamProperty = false;
			this.InitializeComponent();
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00085280 File Offset: 0x00083480
		private void PageSetupUI_Loaded(object sender, RoutedEventArgs e)
		{
			this.PanBack.ScrollToHome();
			ModSecret.ResetReader(true);
			if (ModSecret.registryField != 0)
			{
				int registryField = ModSecret.registryField;
				string text;
				if (registryField != 1)
				{
					if (registryField != 2)
					{
						text = "？？？";
					}
					else
					{
						text = "真·滑稽彩";
					}
				}
				else
				{
					text = "眼瞎白";
				}
				try
				{
					foreach (object obj in this.PanLauncherTheme.Children)
					{
						object objectValue = RuntimeHelpers.GetObjectValue(obj);
						if (objectValue is MyRadioBox && ((MyRadioBox)objectValue).IsEnabled)
						{
							((MyRadioBox)objectValue).Text = text;
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
			checked
			{
				ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
				this.Reload();
				ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
				if (!this._ParamProperty)
				{
					this._ParamProperty = true;
					this.SliderLoad();
					this.PanLauncherHide.Visibility = Visibility.Visible;
					if (!this.RadioLauncherTheme8.IsEnabled)
					{
						this.LabLauncherTheme8Copy.ToolTip = "累积赞助达到 ¥23.33 后，在爱发电私信发送【解锁码】以解锁。\r\n右键打开赞助页面，如果觉得 PCL 做得还不错就支持一下吧 =w=！";
					}
					this.RadioLauncherTheme8.ToolTip = "累积赞助达到 ¥23.33 后，在爱发电私信发送【解锁码】以解锁";
					if (!this.RadioLauncherTheme9.IsEnabled)
					{
						this.LabLauncherTheme9Copy.ToolTip = "· 反馈一个 Bug，在标记为 [完成] 后回复识别码要求解锁（右键打开反馈页面）\r\n· 提交一个 Pull Request，在合并后回复识别码要求解锁";
					}
					this.RadioLauncherTheme9.ToolTip = "· 反馈一个 Bug，在标记为 [完成] 后回复识别码要求解锁\r\n· 提交一个 Pull Request，在合并后回复识别码要求解锁";
				}
			}
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000853D4 File Offset: 0x000835D4
		public void Reload()
		{
			try
			{
				this.SliderLauncherOpacity.Value = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("UiLauncherTransparent", null));
				this.SliderLauncherHue.Value = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("UiLauncherHue", null));
				this.SliderLauncherSat.Value = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("UiLauncherSat", null));
				this.SliderLauncherDelta.Value = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("UiLauncherDelta", null));
				this.SliderLauncherLight.Value = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("UiLauncherLight", null));
				if (Operators.ConditionalCompareObjectLessEqual(ModBase.m_IdentifierRepository.Get("UiLauncherTheme", null), 14, false))
				{
					((MyRadioBox)base.FindName(Conversions.ToString(Operators.ConcatenateObject("RadioLauncherTheme", ModBase.m_IdentifierRepository.Get("UiLauncherTheme", null))))).Checked = true;
				}
				this.CheckLauncherLogo.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiLauncherLogo", null));
				this.CheckLauncherEmail.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiLauncherEmail", null));
				this.SliderBackgroundOpacity.Value = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("UiBackgroundOpacity", null));
				this.SliderBackgroundBlur.Value = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("UiBackgroundBlur", null));
				this.ComboBackgroundSuit.SelectedIndex = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("UiBackgroundSuit", null));
				this.CheckBackgroundColorful.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiBackgroundColorful", null));
				PageSetupUI.BackgroundRefresh(false, false);
				((MyRadioBox)base.FindName(Conversions.ToString(Operators.ConcatenateObject("RadioLogoType", ModBase.m_IdentifierRepository.Get("UiLogoType", null))))).Checked = true;
				this.CheckLogoLeft.Visibility = (this.RadioLogoType0.Checked ? Visibility.Visible : Visibility.Collapsed);
				this.PanLogoText.Visibility = (this.RadioLogoType2.Checked ? Visibility.Visible : Visibility.Collapsed);
				this.PanLogoChange.Visibility = (this.RadioLogoType3.Checked ? Visibility.Visible : Visibility.Collapsed);
				this.TextLogoText.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("UiLogoText", null));
				this.CheckLogoLeft.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiLogoLeft", null));
				this.CheckMusicRandom.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiMusicRandom", null));
				this.CheckMusicAuto.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiMusicAuto", null));
				this.CheckMusicStop.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiMusicStop", null));
				this.CheckMusicStart.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiMusicStart", null));
				this.SliderMusicVolume.Value = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("UiMusicVolume", null));
				this.MusicRefreshUI();
				try
				{
					this.ComboCustomPreset.SelectedIndex = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("UiCustomPreset", null));
				}
				catch (Exception ex)
				{
					ModBase.m_IdentifierRepository.Reset("UiCustomPreset", false, null);
				}
				((MyRadioBox)base.FindName(Conversions.ToString(Operators.ConcatenateObject("RadioCustomType", ModBase.m_IdentifierRepository.Load("UiCustomType", false, null))))).Checked = true;
				this.TextCustomNet.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("UiCustomNet", null));
				this.CheckHiddenPageDownload.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenPageDownload", null));
				this.CheckHiddenPageLink.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenPageLink", null));
				this.CheckHiddenPageSetup.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenPageSetup", null));
				this.CheckHiddenPageOther.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenPageOther", null));
				this.CheckHiddenFunctionSelect.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenFunctionSelect", null));
				this.CheckHiddenFunctionModUpdate.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenFunctionModUpdate", null));
				this.CheckHiddenFunctionHidden.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenFunctionHidden", null));
				this.CheckHiddenSetupLaunch.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupLaunch", null));
				this.CheckHiddenSetupUI.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupUi", null));
				this.CheckHiddenSetupLink.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupLink", null));
				this.CheckHiddenSetupSystem.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupSystem", null));
				this.CheckHiddenOtherAbout.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherAbout", null));
				this.CheckHiddenOtherFeedback.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherFeedback", null));
				this.CheckHiddenOtherVote.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherVote", null));
				this.CheckHiddenOtherHelp.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherHelp", null));
				this.CheckHiddenOtherTest.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherTest", null));
			}
			catch (NullReferenceException ex2)
			{
				ModBase.Log(ex2, "个性化设置项存在异常，已被自动重置", ModBase.LogLevel.Msgbox, "出现错误");
				this.Reset();
			}
			catch (Exception ex3)
			{
				ModBase.Log(ex3, "重载个性化设置时出错", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00085A24 File Offset: 0x00083C24
		public void Reset()
		{
			try
			{
				ModBase.m_IdentifierRepository.Reset("UiLauncherTransparent", false, null);
				ModBase.m_IdentifierRepository.Reset("UiLauncherTheme", false, null);
				ModBase.m_IdentifierRepository.Reset("UiLauncherLogo", false, null);
				ModBase.m_IdentifierRepository.Reset("UiLauncherHue", false, null);
				ModBase.m_IdentifierRepository.Reset("UiLauncherSat", false, null);
				ModBase.m_IdentifierRepository.Reset("UiLauncherDelta", false, null);
				ModBase.m_IdentifierRepository.Reset("UiLauncherLight", false, null);
				ModBase.m_IdentifierRepository.Reset("UiLauncherEmail", false, null);
				ModBase.m_IdentifierRepository.Reset("UiBackgroundColorful", false, null);
				ModBase.m_IdentifierRepository.Reset("UiBackgroundOpacity", false, null);
				ModBase.m_IdentifierRepository.Reset("UiBackgroundBlur", false, null);
				ModBase.m_IdentifierRepository.Reset("UiBackgroundSuit", false, null);
				ModBase.m_IdentifierRepository.Reset("UiLogoType", false, null);
				ModBase.m_IdentifierRepository.Reset("UiLogoText", false, null);
				ModBase.m_IdentifierRepository.Reset("UiLogoLeft", false, null);
				ModBase.m_IdentifierRepository.Reset("UiMusicVolume", false, null);
				ModBase.m_IdentifierRepository.Reset("UiMusicStop", false, null);
				ModBase.m_IdentifierRepository.Reset("UiMusicStart", false, null);
				ModBase.m_IdentifierRepository.Reset("UiMusicRandom", false, null);
				ModBase.m_IdentifierRepository.Reset("UiMusicAuto", false, null);
				ModBase.m_IdentifierRepository.Reset("UiCustomType", false, null);
				ModBase.m_IdentifierRepository.Reset("UiCustomPreset", false, null);
				ModBase.m_IdentifierRepository.Reset("UiCustomNet", false, null);
				ModBase.m_IdentifierRepository.Reset("UiHiddenPageDownload", false, null);
				ModBase.m_IdentifierRepository.Reset("UiHiddenPageLink", false, null);
				ModBase.m_IdentifierRepository.Reset("UiHiddenPageSetup", false, null);
				ModBase.m_IdentifierRepository.Reset("UiHiddenPageOther", false, null);
				ModBase.m_IdentifierRepository.Reset("UiHiddenFunctionSelect", false, null);
				ModBase.m_IdentifierRepository.Reset("UiHiddenFunctionModUpdate", false, null);
				ModBase.m_IdentifierRepository.Reset("UiHiddenFunctionHidden", false, null);
				ModBase.m_IdentifierRepository.Reset("UiHiddenSetupLaunch", false, null);
				ModBase.m_IdentifierRepository.Reset("UiHiddenSetupUi", false, null);
				ModBase.m_IdentifierRepository.Reset("UiHiddenSetupLink", false, null);
				ModBase.m_IdentifierRepository.Reset("UiHiddenSetupSystem", false, null);
				ModBase.m_IdentifierRepository.Reset("UiHiddenOtherAbout", false, null);
				ModBase.m_IdentifierRepository.Reset("UiHiddenOtherFeedback", false, null);
				ModBase.m_IdentifierRepository.Reset("UiHiddenOtherVote", false, null);
				ModBase.m_IdentifierRepository.Reset("UiHiddenOtherHelp", false, null);
				ModBase.m_IdentifierRepository.Reset("UiHiddenOtherTest", false, null);
				ModBase.Log("[Setup] 已初始化个性化设置！", ModBase.LogLevel.Normal, "出现错误");
				ModMain.Hint("已初始化个性化设置", ModMain.HintType.Finish, false);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "初始化个性化设置失败", ModBase.LogLevel.Msgbox, "出现错误");
			}
			this.Reload();
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0000AD1F File Offset: 0x00008F1F
		private static void SliderChange(MySlider sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.Value, false, null);
			}
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0000AD4A File Offset: 0x00008F4A
		private static void ComboChange(MyComboBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.SelectedIndex, false, null);
			}
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0000AD75 File Offset: 0x00008F75
		private static void CheckBoxChange(MyCheckBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.Checked, false, null);
			}
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0000ACF9 File Offset: 0x00008EF9
		private static void TextBoxChange(MyTextBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.Text, false, null);
			}
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x000815D4 File Offset: 0x0007F7D4
		private static void RadioBoxChange(MyRadioBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(sender.Tag.ToString().Split("/")[0], ModBase.Val(sender.Tag.ToString().Split("/")[1]), false, null);
			}
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0000B410 File Offset: 0x00009610
		private void BtnUIBgOpen_Click(object sender, EventArgs e)
		{
			ModBase.OpenExplorer("\"" + ModBase.Path + "PCL\\Pictures\"");
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0000B42B File Offset: 0x0000962B
		private void BtnBackgroundRefresh_Click(object sender, EventArgs e)
		{
			PageSetupUI.BackgroundRefresh(true, true);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00085D28 File Offset: 0x00083F28
		public void BackgroundRefreshUI(bool Show, int Count)
		{
			if (!Information.IsNothing(this.PanBackgroundOpacity))
			{
				if (Show)
				{
					this.PanBackgroundOpacity.Visibility = Visibility.Visible;
					this.PanBackgroundBlur.Visibility = Visibility.Visible;
					this.PanBackgroundSuit.Visibility = Visibility.Visible;
					this.BtnBackgroundClear.Visibility = Visibility.Visible;
					this.CardBackground.Title = "背景图片（" + Conversions.ToString(Count) + " 张）";
				}
				else
				{
					this.PanBackgroundOpacity.Visibility = Visibility.Collapsed;
					this.PanBackgroundBlur.Visibility = Visibility.Collapsed;
					this.PanBackgroundSuit.Visibility = Visibility.Collapsed;
					this.BtnBackgroundClear.Visibility = Visibility.Collapsed;
					this.CardBackground.Title = "背景图片";
				}
				this.CardBackground.TriggerForceResize();
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00085DE8 File Offset: 0x00083FE8
		private void BtnBackgroundClear_Click(object sender, EventArgs e)
		{
			if (ModMain.MyMsgBox("即将删除背景图片文件夹中的所有文件。\r\n此操作不可撤销，是否确定？", "警告", "确定", "取消", "", true, true, false, null, null, null) == 1)
			{
				ModBase.DeleteDirectory(ModBase.Path + "PCL\\Pictures", false);
				PageSetupUI.BackgroundRefresh(false, true);
				ModMain.Hint("背景图片已清空！", ModMain.HintType.Finish, true);
			}
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00085E48 File Offset: 0x00084048
		public static void BackgroundRefresh(bool IsHint, bool Refresh)
		{
			try
			{
				Directory.CreateDirectory(ModBase.Path + "PCL\\Pictures\\");
				List<string> list = new List<string>();
				try
				{
					foreach (FileInfo fileInfo in ModBase.EnumerateFiles(ModBase.Path + "PCL\\Pictures\\"))
					{
						if (Operators.CompareString(fileInfo.Extension.ToLower(), ".ini", false) != 0 && Operators.CompareString(fileInfo.Extension.ToLower(), ".db", false) != 0)
						{
							list.Add(fileInfo.FullName);
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
				if (!Enumerable.Any<string>(list))
				{
					if (Refresh)
					{
						if (ModMain._ProcessIterator.ImgBack.Visibility == Visibility.Collapsed)
						{
							if (IsHint)
							{
								ModMain.Hint("未检测到可用背景图片！", ModMain.HintType.Critical, true);
							}
						}
						else
						{
							ModMain._ProcessIterator.ImgBack.Visibility = Visibility.Collapsed;
							if (IsHint)
							{
								ModMain.Hint("背景图片已清除！", ModMain.HintType.Finish, true);
							}
						}
					}
					if (!Information.IsNothing(ModMain.m_OrderIterator))
					{
						ModMain.m_OrderIterator.BackgroundRefreshUI(false, 0);
					}
				}
				else
				{
					if (Refresh)
					{
						string text = ModBase.RandomOne<string>(list);
						try
						{
							ModBase.Log("[UI] 加载背景图片：" + text, ModBase.LogLevel.Normal, "出现错误");
							ModMain._ProcessIterator.ImgBack.Background = new MyBitmap(text);
							ModBase.m_IdentifierRepository.Load("UiBackgroundSuit", true, null);
							ModMain._ProcessIterator.ImgBack.Visibility = Visibility.Visible;
							if (IsHint)
							{
								ModMain.Hint("背景图片已刷新：" + ModBase.GetFileNameFromPath(text), ModMain.HintType.Finish, false);
							}
						}
						catch (Exception ex)
						{
							if (ex.Message.Contains("参数无效"))
							{
								ModBase.Log("刷新背景图片失败，该图片文件可能并非标准格式。\r\n你可以尝试使用画图打开该文件并重新保存，这会让图片变为标准格式。\r\n文件：" + text, ModBase.LogLevel.Msgbox, "出现错误");
							}
							else
							{
								ModBase.Log(ex, "刷新背景图片失败（" + text + "）", ModBase.LogLevel.Msgbox, "出现错误");
							}
						}
					}
					if (!Information.IsNothing(ModMain.m_OrderIterator))
					{
						ModMain.m_OrderIterator.BackgroundRefreshUI(true, list.Count);
					}
				}
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, "刷新背景图片时出现未知错误", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000860B0 File Offset: 0x000842B0
		private void BtnLogoChange_Click(object sender, EventArgs e)
		{
			string text = ModBase.SelectFile("常用图片文件(*.png;*.jpg;*.gif;*.webp)|*.png;*.jpg;*.gif;*.webp", "选择图片");
			if (Operators.CompareString(text, "", false) != 0)
			{
				try
				{
					File.Delete(ModBase.Path + "PCL\\Logo.png");
					ModBase.CopyFile(text, ModBase.Path + "PCL\\Logo.png");
					ModMain._ProcessIterator.ImageTitleLogo.Source = ModBase.Path + "PCL\\Logo.png";
				}
				catch (Exception ex)
				{
					if (ex.Message.Contains("参数无效"))
					{
						ModBase.Log("改变标题栏图片失败，该图片文件可能并非标准格式。\r\n你可以尝试使用画图打开该文件并重新保存，这会让图片变为标准格式。", ModBase.LogLevel.Msgbox, "出现错误");
					}
					else
					{
						ModBase.Log(ex, "设置标题栏图片失败", ModBase.LogLevel.Msgbox, "出现错误");
					}
					ModMain._ProcessIterator.ImageTitleLogo.Source = null;
				}
			}
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0008618C File Offset: 0x0008438C
		private void RadioLogoType3_Check(object sender, ModBase.RouteEventArgs e)
		{
			if (ModAnimation.CalcParser() == 0 && e.interpreterError)
			{
				while (!File.Exists(ModBase.Path + "PCL\\Logo.png"))
				{
					string text = ModBase.SelectFile("常用图片文件(*.png;*.jpg;*.gif;*.webp)|*.png;*.jpg;*.gif;*.webp", "选择图片");
					if (Operators.CompareString(text, "", false) == 0)
					{
						IL_152:
						ModMain._ProcessIterator.ImageTitleLogo.Source = null;
						e.m_SerializerError = true;
						return;
					}
					try
					{
						File.Delete(ModBase.Path + "PCL\\Logo.png");
						ModBase.CopyFile(text, ModBase.Path + "PCL\\Logo.png");
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "复制标题栏图片失败", ModBase.LogLevel.Msgbox, "出现错误");
						return;
					}
				}
				try
				{
					ModMain._ProcessIterator.ImageTitleLogo.Source = ModBase.Path + "PCL\\Logo.png";
					return;
				}
				catch (Exception ex2)
				{
					if (ex2.Message.Contains("参数无效"))
					{
						ModBase.Log("调整标题栏图片失败，该图片文件可能并非标准格式。\r\n你可以尝试使用画图打开该文件并重新保存，这会让图片变为标准格式。", ModBase.LogLevel.Msgbox, "出现错误");
					}
					else
					{
						ModBase.Log(ex2, "调整标题栏图片失败", ModBase.LogLevel.Msgbox, "出现错误");
					}
					ModMain._ProcessIterator.ImageTitleLogo.Source = null;
					e.m_SerializerError = true;
					try
					{
						File.Delete(ModBase.Path + "PCL\\Logo.png");
					}
					catch (Exception ex3)
					{
						ModBase.Log(ex3, "清理错误的标题栏图片失败", ModBase.LogLevel.Msgbox, "出现错误");
					}
					return;
				}
				goto IL_152;
			}
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0008632C File Offset: 0x0008452C
		private void BtnLogoDelete_Click(object sender, EventArgs e)
		{
			try
			{
				File.Delete(ModBase.Path + "PCL\\Logo.png");
				this.RadioLogoType1.SetChecked(true, true, true);
				ModMain.Hint("标题栏图片已清空！", ModMain.HintType.Finish, true);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "清空标题栏图片失败", ModBase.LogLevel.Msgbox, "出现错误");
			}
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0000B434 File Offset: 0x00009634
		private void BtnMusicOpen_Click(object sender, EventArgs e)
		{
			ModBase.OpenExplorer("\"" + ModBase.Path + "PCL\\Musics\"");
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0000B44F File Offset: 0x0000964F
		private void BtnMusicRefresh_Click(object sender, EventArgs e)
		{
			ModMusic.MusicRefreshPlay(true, false);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00086398 File Offset: 0x00084598
		public void MusicRefreshUI()
		{
			if (this.PanBackgroundOpacity != null)
			{
				if (Enumerable.Any<string>(ModMusic.m_SpecificationField))
				{
					this.PanMusicVolume.Visibility = Visibility.Visible;
					this.PanMusicDetail.Visibility = Visibility.Visible;
					this.BtnMusicClear.Visibility = Visibility.Visible;
					this.CardMusic.Title = "背景音乐（" + Conversions.ToString(Enumerable.Count<FileInfo>(ModBase.EnumerateFiles(ModBase.Path + "PCL\\Musics\\"))) + " 首）";
				}
				else
				{
					this.PanMusicVolume.Visibility = Visibility.Collapsed;
					this.PanMusicDetail.Visibility = Visibility.Collapsed;
					this.BtnMusicClear.Visibility = Visibility.Collapsed;
					this.CardMusic.Title = "背景音乐";
				}
				this.CardMusic.TriggerForceResize();
			}
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0008645C File Offset: 0x0008465C
		private void BtnMusicClear_Click(object sender, EventArgs e)
		{
			if (ModMain.MyMsgBox("即将删除背景音乐文件夹中的所有文件。\r\n此操作不可撤销，是否确定？", "警告", "确定", "取消", "", true, true, false, null, null, null) == 1)
			{
				ModBase.RunInThread((PageSetupUI._Closure$__.$I22-0 == null) ? (PageSetupUI._Closure$__.$I22-0 = delegate()
				{
					ModMain.Hint("正在删除背景音乐……", ModMain.HintType.Info, true);
					ModMusic.mockField = null;
					ModMusic._ContextField = new List<string>();
					ModMusic.m_SpecificationField = new List<string>();
					Thread.Sleep(200);
					try
					{
						ModBase.DeleteDirectory(ModBase.Path + "PCL\\Musics", false);
						ModMain.Hint("背景音乐已删除！", ModMain.HintType.Finish, true);
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "删除背景音乐失败", ModBase.LogLevel.Msgbox, "出现错误");
					}
					try
					{
						Directory.CreateDirectory(ModBase.Path + "PCL\\Musics");
						ModBase.RunInUi((PageSetupUI._Closure$__.$I22-1 == null) ? (PageSetupUI._Closure$__.$I22-1 = delegate()
						{
							ModMusic.MusicRefreshPlay(false, false);
						}) : PageSetupUI._Closure$__.$I22-1, false);
					}
					catch (Exception ex2)
					{
						ModBase.Log(ex2, "重建背景音乐文件夹失败", ModBase.LogLevel.Msgbox, "出现错误");
					}
				}) : PageSetupUI._Closure$__.$I22-0);
			}
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0000B458 File Offset: 0x00009658
		private void CheckMusicStart_Change()
		{
			if (ModAnimation.CalcParser() == 0 && this.CheckMusicStart.Checked)
			{
				this.CheckMusicStop.Checked = false;
			}
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0000B47A File Offset: 0x0000967A
		private void CheckMusicStop_Change()
		{
			if (ModAnimation.CalcParser() == 0 && this.CheckMusicStop.Checked)
			{
				this.CheckMusicStart.Checked = false;
			}
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x000864BC File Offset: 0x000846BC
		private void BtnCustomFile_Click(object sender, EventArgs e)
		{
			try
			{
				if (!File.Exists(ModBase.Path + "PCL\\Custom.xaml") || ModMain.MyMsgBox("当前已存在布局文件，继续生成教学文件将会覆盖现有布局文件！", "覆盖确认", "继续", "取消", "", true, true, false, null, null, null) != 2)
				{
					ModBase.WriteFile(ModBase.Path + "PCL\\Custom.xaml", ModBase.GetResources("Custom"), false);
					ModMain.Hint("教学文件已生成！", ModMain.HintType.Finish, true);
					ModBase.OpenExplorer("/select,\"" + ModBase.Path + "PCL\\Custom.xaml\"");
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "生成教学文件失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0000B49C File Offset: 0x0000969C
		private void BtnCustomRefresh_Click()
		{
			ModMain.m_ServiceIterator.ForceRefresh();
			ModMain.Hint("已刷新主页！", ModMain.HintType.Finish, true);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00086580 File Offset: 0x00084780
		private void BtnCustomTutorial_Click(object sender, EventArgs e)
		{
			ModMain.MyMsgBox("1. 点击 生成教学文件 按钮，这会在 PCL 文件夹下生成 Custom.xaml 布局文件。\r\n2. 使用记事本等工具打开这个文件并进行修改，修改完记得保存。\r\n3. 点击 刷新主页 按钮，查看主页现在长啥样了。\r\n\r\n你可以在生成教学文件后直接刷新主页，对照着进行修改，更有助于理解。\r\n直接将自定义主页文件拖进 PCL 窗口也可以快捷加载。", "主页自定义教程", "确定", "", "", false, true, false, null, null, null);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x000865B4 File Offset: 0x000847B4
		private void LabLauncherTheme5Unlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			MyRadioBox myRadioBox;
			(myRadioBox = this.RadioLauncherTheme5Gray).Opacity = myRadioBox.Opacity - 0.23;
			(myRadioBox = this.RadioLauncherTheme5).Opacity = myRadioBox.Opacity + 0.23;
			ModAnimation.AniStart(checked(new ModAnimation.AniData[]
			{
				ModAnimation.AaOpacity(this.RadioLauncherTheme5Gray, 1.0, (int)Math.Round(unchecked(1000.0 * ModAnimation.m_Task)), 0, null, false),
				ModAnimation.AaOpacity(this.RadioLauncherTheme5, -1.0, (int)Math.Round(unchecked(1000.0 * ModAnimation.m_Task)), 0, null, false)
			}), "ThemeUnlock", false);
			if (this.RadioLauncherTheme5Gray.Opacity < 0.08)
			{
				ModSecret.ThemeUnlock(5, true, "隐藏主题 玄素黑 已解锁！");
				ModAnimation.AniStop("ThemeUnlock");
				this.RadioLauncherTheme5.Checked = true;
			}
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x000866AC File Offset: 0x000848AC
		private void LabLauncherTheme11Click_MouseLeftButtonUp()
		{
			if ((this.LabLauncherTheme11Click.Visibility == Visibility.Collapsed || (this.LabLauncherTheme11Click.ToolTip ?? "").ToString().Contains("点击")) && ModMain.MyMsgBox("1. 不爬取或攻击相关服务或网站，不盗取相关账号，没有谜题可以或需要以此来解决。\r\n2. 不得篡改或损毁相关公开信息，请尽量让它们保持原状。\r\n3. 在你感到迷茫的时候，看看回声洞可能会给你带来惊喜。\r\n\r\n若违规，可能会被从任意相关群中踢出！", "解密游戏的基本规则", "我知道了", "恕我拒绝", "", false, true, false, null, null, null) == 1)
			{
				ModMain.MyMsgBox("你需要用自己的智慧来找到下一步的线索……\r\n初始线索：gnp.dorC61\\60\\20\\0202\\moc.x1xa.2s\\\\:sp" + "T".ToLower() + "th", "解密游戏", "确定", "", "", false, true, false, null, null, null);
			}
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0000B4B4 File Offset: 0x000096B4
		private void LabLauncherTheme8Copy_MouseRightButtonUp()
		{
			ModBase.OpenWebsite("https://afdian.com/a/LTCat");
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0000B4C0 File Offset: 0x000096C0
		private void LabLauncherTheme9Copy_MouseRightButtonUp()
		{
			PageOtherLeft.TryFeedback();
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0008674C File Offset: 0x0008494C
		private void RadioLauncherTheme14_Change(object sender, ModBase.RouteEventArgs e)
		{
			if (this.MyRadioBox_4.Checked)
			{
				if (this.LabLauncherHue.Visibility == Visibility.Visible)
				{
					return;
				}
				this.LabLauncherHue.Visibility = Visibility.Visible;
				this.SliderLauncherHue.Visibility = Visibility.Visible;
				this.LabLauncherSat.Visibility = Visibility.Visible;
				this.SliderLauncherSat.Visibility = Visibility.Visible;
				this.LabLauncherDelta.Visibility = Visibility.Visible;
				this.SliderLauncherDelta.Visibility = Visibility.Visible;
				this.LabLauncherLight.Visibility = Visibility.Visible;
				this.SliderLauncherLight.Visibility = Visibility.Visible;
			}
			else
			{
				if (this.LabLauncherHue.Visibility == Visibility.Collapsed)
				{
					return;
				}
				this.LabLauncherHue.Visibility = Visibility.Collapsed;
				this.SliderLauncherHue.Visibility = Visibility.Collapsed;
				this.LabLauncherSat.Visibility = Visibility.Collapsed;
				this.SliderLauncherSat.Visibility = Visibility.Collapsed;
				this.LabLauncherDelta.Visibility = Visibility.Collapsed;
				this.SliderLauncherDelta.Visibility = Visibility.Collapsed;
				this.LabLauncherLight.Visibility = Visibility.Collapsed;
				this.SliderLauncherLight.Visibility = Visibility.Collapsed;
			}
			this.CardLauncher.TriggerForceResize();
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0000B4C7 File Offset: 0x000096C7
		private void HSL_Change()
		{
			if (ModAnimation.CalcParser() == 0 && this.SliderLauncherSat != null && this.SliderLauncherSat.IsLoaded)
			{
				ModSecret.ViewReader(-1);
			}
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0000B4EB File Offset: 0x000096EB
		public static bool CreateClient()
		{
			return PageSetupUI.m_TagProperty;
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0000B4F2 File Offset: 0x000096F2
		public static void ExcludeClient(bool value)
		{
			PageSetupUI.m_TagProperty = value;
			PageSetupUI.HiddenRefresh();
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00086854 File Offset: 0x00084A54
		public static void HiddenRefresh()
		{
			checked
			{
				if (ModMain._ProcessIterator.PanTitleSelect != null && ModMain._ProcessIterator.PanTitleSelect.IsLoaded)
				{
					try
					{
						if (Conversions.ToBoolean(!PageSetupUI.m_TagProperty && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenPageDownload", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenPageLink", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenPageSetup", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenPageOther", null))))
						{
							ModMain._ProcessIterator.PanTitleSelect.Visibility = Visibility.Collapsed;
						}
						else
						{
							ModMain._ProcessIterator.PanTitleSelect.Visibility = Visibility.Visible;
							ModMain._ProcessIterator.BtnTitleSelect1.Visibility = (Conversions.ToBoolean(!PageSetupUI.m_TagProperty && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenPageDownload", null))) ? Visibility.Collapsed : Visibility.Visible);
							ModMain._ProcessIterator.BtnTitleSelect2.Visibility = Visibility.Collapsed;
							ModMain._ProcessIterator.BtnTitleSelect3.Visibility = (Conversions.ToBoolean(!PageSetupUI.m_TagProperty && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenPageSetup", null))) ? Visibility.Collapsed : Visibility.Visible);
							ModMain._ProcessIterator.BtnTitleSelect4.Visibility = (Conversions.ToBoolean(!PageSetupUI.m_TagProperty && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenPageOther", null))) ? Visibility.Collapsed : Visibility.Visible);
						}
						ModMain.recordIterator.RefreshButtonsUI();
						if (ModMain.m_OrderIterator != null)
						{
							ModMain.m_OrderIterator.CardSwitch.Visibility = (Conversions.ToBoolean(!PageSetupUI.m_TagProperty && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenFunctionHidden", null))) ? Visibility.Collapsed : Visibility.Visible);
						}
						if (ModMain._ClassIterator != null)
						{
							ModMain._ClassIterator.ItemLaunch.Visibility = (Conversions.ToBoolean(!PageSetupUI.m_TagProperty && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupLaunch", null))) ? Visibility.Collapsed : Visibility.Visible);
							ModMain._ClassIterator.ItemUI.Visibility = (Conversions.ToBoolean(!PageSetupUI.m_TagProperty && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupUi", null))) ? Visibility.Collapsed : Visibility.Visible);
							ModMain._ClassIterator.ItemLink.Visibility = Visibility.Collapsed;
							ModMain._ClassIterator.ItemSystem.Visibility = (Conversions.ToBoolean(!PageSetupUI.m_TagProperty && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupSystem", null))) ? Visibility.Collapsed : Visibility.Visible);
							int num = 0;
							if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenSetupLaunch", null))))
							{
								num++;
							}
							if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenSetupUi", null))))
							{
								num++;
							}
							if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenSetupLink", null))))
							{
								num++;
							}
							if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenSetupSystem", null))))
							{
								num++;
							}
							ModMain._ClassIterator.PanItem.Visibility = ((num >= 2 || PageSetupUI.m_TagProperty) ? Visibility.Visible : Visibility.Collapsed);
						}
						int num2 = 0;
						if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenOtherHelp", null))))
						{
							num2++;
						}
						if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenOtherAbout", null))))
						{
							num2++;
						}
						if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenOtherTest", null))))
						{
							num2++;
						}
						if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenOtherFeedback", null))))
						{
							num2++;
						}
						if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenOtherVote", null))))
						{
							num2++;
						}
						if (ModMain.descriptorIterator != null)
						{
							ModMain.descriptorIterator.ItemHelp.Visibility = (Conversions.ToBoolean(!PageSetupUI.m_TagProperty && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherHelp", null))) ? Visibility.Collapsed : Visibility.Visible);
							ModMain.descriptorIterator.ItemFeedback.Visibility = (Conversions.ToBoolean(!PageSetupUI.m_TagProperty && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherFeedback", null))) ? Visibility.Collapsed : Visibility.Visible);
							ModMain.descriptorIterator.ItemVote.Visibility = (Conversions.ToBoolean(!PageSetupUI.m_TagProperty && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherVote", null))) ? Visibility.Collapsed : Visibility.Visible);
							ModMain.descriptorIterator.ItemAbout.Visibility = (Conversions.ToBoolean(!PageSetupUI.m_TagProperty && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherAbout", null))) ? Visibility.Collapsed : Visibility.Visible);
							ModMain.descriptorIterator.ItemTest.Visibility = (Conversions.ToBoolean(!PageSetupUI.m_TagProperty && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherTest", null))) ? Visibility.Collapsed : Visibility.Visible);
							ModMain.descriptorIterator.PanItem.Visibility = ((num2 >= 2 || PageSetupUI.m_TagProperty) ? Visibility.Visible : Visibility.Collapsed);
						}
						if (num2 == 1 && !PageSetupUI.m_TagProperty)
						{
							if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenOtherHelp", null))))
							{
								ModMain._ProcessIterator.BtnTitleSelect4.Text = "帮助";
							}
							else if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenOtherAbout", null))))
							{
								ModMain._ProcessIterator.BtnTitleSelect4.Text = "关于";
							}
							else
							{
								ModMain._ProcessIterator.BtnTitleSelect4.Text = "百宝箱";
							}
						}
						else
						{
							ModMain._ProcessIterator.BtnTitleSelect4.Text = "更多";
						}
						if (ModMain._ProcessIterator._MethodIterator == FormMain.PageType.VersionSelect)
						{
							ModMain.proxyIterator.BtnEmptyDownload_Loaded();
						}
						if (ModMain._ProcessIterator._MethodIterator == FormMain.PageType.Launch)
						{
							ModMain.recordIterator.RefreshButtonsUI();
						}
						if (ModMain._ProcessIterator._MethodIterator == FormMain.PageType.VersionSetup && ModMain._PropertyRepository != null)
						{
							ModMain._PropertyRepository.BtnDownload_Loaded();
						}
						if (ModMain.m_OrderIterator != null)
						{
							ModMain.m_OrderIterator.CardSwitch.Title = (PageSetupUI.m_TagProperty ? "功能隐藏（已暂时关闭，按 F12 以重新启用）" : "功能隐藏");
						}
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "刷新功能隐藏项目失败", ModBase.LogLevel.Feedback, "出现错误");
					}
				}
			}
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00086F14 File Offset: 0x00085114
		private void HiddenSetupMain()
		{
			if (this.CheckHiddenPageSetup.Checked)
			{
				this.CheckHiddenSetupLaunch.Checked = true;
				this.CheckHiddenSetupSystem.Checked = true;
				this.CheckHiddenSetupLink.Checked = true;
				this.CheckHiddenSetupUI.Checked = true;
				return;
			}
			if (Conversions.ToBoolean(Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupLaunch", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupUi", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupSystem", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupLink", null))))
			{
				this.CheckHiddenSetupLaunch.Checked = false;
				this.CheckHiddenSetupSystem.Checked = false;
				this.CheckHiddenSetupLink.Checked = false;
				this.CheckHiddenSetupUI.Checked = false;
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00086FF8 File Offset: 0x000851F8
		private void HiddenSetupSub()
		{
			if (Conversions.ToBoolean(Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupLaunch", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupUi", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupSystem", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupLink", null))))
			{
				this.CheckHiddenPageSetup.Checked = true;
				return;
			}
			this.CheckHiddenPageSetup.Checked = false;
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00087088 File Offset: 0x00085288
		private void HiddenOtherMain()
		{
			if (this.CheckHiddenPageOther.Checked)
			{
				this.CheckHiddenOtherAbout.Checked = true;
				this.CheckHiddenOtherTest.Checked = true;
				this.CheckHiddenOtherFeedback.Checked = true;
				this.CheckHiddenOtherVote.Checked = true;
				this.CheckHiddenOtherHelp.Checked = true;
				return;
			}
			if (Conversions.ToBoolean(Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherHelp", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherAbout", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherTest", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherVote", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherFeedback", null))))
			{
				this.CheckHiddenOtherAbout.Checked = false;
				this.CheckHiddenOtherTest.Checked = false;
				this.CheckHiddenOtherFeedback.Checked = false;
				this.CheckHiddenOtherVote.Checked = false;
				this.CheckHiddenOtherHelp.Checked = false;
			}
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0008719C File Offset: 0x0008539C
		private void HiddenOtherSub(object sender, bool user)
		{
			if (Conversions.ToBoolean(Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherHelp", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherAbout", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherTest", null))))
			{
				this.CheckHiddenPageOther.Checked = true;
			}
			else
			{
				this.CheckHiddenPageOther.Checked = false;
			}
			if (user && Conversions.ToBoolean(Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherHelp", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherAbout", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherTest", null))))
			{
				this.CheckHiddenOtherFeedback.Checked = true;
				this.CheckHiddenOtherVote.Checked = true;
			}
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00087284 File Offset: 0x00085484
		private void HiddenOtherNet(object sender, bool user)
		{
			if (user && Conversions.ToBoolean(Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherHelp", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherAbout", null)) && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenOtherTest", null)) && (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenOtherFeedback", null))) || Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiHiddenOtherVote", null))))))
			{
				this.CheckHiddenOtherAbout.Checked = false;
				this.CheckHiddenOtherTest.Checked = false;
				this.CheckHiddenOtherHelp.Checked = false;
			}
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0000B4FF File Offset: 0x000096FF
		private void HiddenHint(object sender, bool user)
		{
			if (Conversions.ToBoolean(ModAnimation.CalcParser() == 0 && Conversions.ToBoolean(NewLateBinding.LateGet(sender, null, "Checked", new object[0], null, null, null))))
			{
				ModMain.Hint("按 F12 即可暂时关闭功能隐藏设置。千万别忘了，要不然设置就改不回来了……", ModMain.HintType.Info, true);
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0000B4B4 File Offset: 0x000096B4
		private void BtnLauncherDonate_Click(object sender, EventArgs e)
		{
			ModBase.OpenWebsite("https://afdian.com/a/LTCat");
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00087348 File Offset: 0x00085548
		private void SliderLoad()
		{
			this.SliderMusicVolume.m_RepositoryIterator = ((PageSetupUI._Closure$__.$I46-0 == null) ? (PageSetupUI._Closure$__.$I46-0 = ((object v) => Operators.ConcatenateObject(NewLateBinding.LateGet(null, typeof(Math), "Ceiling", new object[]
			{
				Operators.MultiplyObject(v, 0.1)
			}, null, null, null), "%"))) : PageSetupUI._Closure$__.$I46-0);
			this.SliderLauncherOpacity.m_RepositoryIterator = ((PageSetupUI._Closure$__.$I46-1 == null) ? (PageSetupUI._Closure$__.$I46-1 = ((object v) => Operators.ConcatenateObject(NewLateBinding.LateGet(null, typeof(Math), "Round", new object[]
			{
				Operators.AddObject(40, Operators.MultiplyObject(v, 0.1))
			}, null, null, null), "%"))) : PageSetupUI._Closure$__.$I46-1);
			this.SliderLauncherHue.m_RepositoryIterator = ((PageSetupUI._Closure$__.$I46-2 == null) ? (PageSetupUI._Closure$__.$I46-2 = ((object v) => Operators.ConcatenateObject(v, "°"))) : PageSetupUI._Closure$__.$I46-2);
			this.SliderLauncherSat.m_RepositoryIterator = ((PageSetupUI._Closure$__.$I46-3 == null) ? (PageSetupUI._Closure$__.$I46-3 = ((object v) => Operators.ConcatenateObject(v, "%"))) : PageSetupUI._Closure$__.$I46-3);
			checked
			{
				this.SliderLauncherDelta.m_RepositoryIterator = ((PageSetupUI._Closure$__.$I46-4 == null) ? (PageSetupUI._Closure$__.$I46-4 = delegate(int Value)
				{
					object result;
					if (Value > 90)
					{
						result = "+" + Conversions.ToString(Value - 90);
					}
					else if (Value == 90)
					{
						result = 0;
					}
					else
					{
						result = Value - 90;
					}
					return result;
				}) : PageSetupUI._Closure$__.$I46-4);
				this.SliderLauncherLight.m_RepositoryIterator = ((PageSetupUI._Closure$__.$I46-5 == null) ? (PageSetupUI._Closure$__.$I46-5 = delegate(int Value)
				{
					object result;
					if (Value > 20)
					{
						result = "+" + Conversions.ToString(Value - 20);
					}
					else if (Value == 20)
					{
						result = 0;
					}
					else
					{
						result = Value - 20;
					}
					return result;
				}) : PageSetupUI._Closure$__.$I46-5);
				this.SliderBackgroundOpacity.m_RepositoryIterator = ((PageSetupUI._Closure$__.$I46-6 == null) ? (PageSetupUI._Closure$__.$I46-6 = ((object v) => Operators.ConcatenateObject(NewLateBinding.LateGet(null, typeof(Math), "Round", new object[]
				{
					Operators.MultiplyObject(v, 0.1)
				}, null, null, null), "%"))) : PageSetupUI._Closure$__.$I46-6);
				this.SliderBackgroundBlur.m_RepositoryIterator = ((PageSetupUI._Closure$__.$I46-7 == null) ? (PageSetupUI._Closure$__.$I46-7 = ((object v) => Operators.ConcatenateObject(v, " 像素"))) : PageSetupUI._Closure$__.$I46-7);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x0000B53D File Offset: 0x0000973D
		// (set) Token: 0x06001362 RID: 4962 RVA: 0x0000B545 File Offset: 0x00009745
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x0000B54E File Offset: 0x0000974E
		// (set) Token: 0x06001364 RID: 4964 RVA: 0x0000B556 File Offset: 0x00009756
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x0000B55F File Offset: 0x0000975F
		// (set) Token: 0x06001366 RID: 4966 RVA: 0x0000B567 File Offset: 0x00009767
		internal virtual MyCard CardLauncher { get; set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x0000B570 File Offset: 0x00009770
		// (set) Token: 0x06001368 RID: 4968 RVA: 0x000874D0 File Offset: 0x000856D0
		internal virtual MySlider SliderLauncherOpacity
		{
			[CompilerGenerated]
			get
			{
				return this.refProperty;
			}
			[CompilerGenerated]
			set
			{
				MySlider.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR61-2 == null) ? (PageSetupUI._Closure$__.$IR61-2 = delegate(object a0, bool a1)
				{
					PageSetupUI.SliderChange((MySlider)a0, a1);
				}) : PageSetupUI._Closure$__.$IR61-2;
				MySlider mySlider = this.refProperty;
				if (mySlider != null)
				{
					mySlider.WriteTests(obj);
				}
				this.refProperty = value;
				mySlider = this.refProperty;
				if (mySlider != null)
				{
					mySlider.FillTests(obj);
				}
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x0000B578 File Offset: 0x00009778
		// (set) Token: 0x0600136A RID: 4970 RVA: 0x0000B580 File Offset: 0x00009780
		internal virtual TextBlock LabLauncherHue { get; set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x0000B589 File Offset: 0x00009789
		// (set) Token: 0x0600136C RID: 4972 RVA: 0x0008752C File Offset: 0x0008572C
		internal virtual MySlider SliderLauncherHue
		{
			[CompilerGenerated]
			get
			{
				return this.m_InstanceProperty;
			}
			[CompilerGenerated]
			set
			{
				MySlider.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR69-3 == null) ? (PageSetupUI._Closure$__.$IR69-3 = delegate(object a0, bool a1)
				{
					PageSetupUI.SliderChange((MySlider)a0, a1);
				}) : PageSetupUI._Closure$__.$IR69-3;
				MySlider.ChangeEventHandler obj2 = delegate(object a0, bool a1)
				{
					this.HSL_Change();
				};
				MySlider instanceProperty = this.m_InstanceProperty;
				if (instanceProperty != null)
				{
					instanceProperty.WriteTests(obj);
					instanceProperty.WriteTests(obj2);
				}
				this.m_InstanceProperty = value;
				instanceProperty = this.m_InstanceProperty;
				if (instanceProperty != null)
				{
					instanceProperty.FillTests(obj);
					instanceProperty.FillTests(obj2);
				}
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x0000B591 File Offset: 0x00009791
		// (set) Token: 0x0600136E RID: 4974 RVA: 0x0000B599 File Offset: 0x00009799
		internal virtual TextBlock LabLauncherDelta { get; set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x0000B5A2 File Offset: 0x000097A2
		// (set) Token: 0x06001370 RID: 4976 RVA: 0x000875A4 File Offset: 0x000857A4
		internal virtual MySlider SliderLauncherDelta
		{
			[CompilerGenerated]
			get
			{
				return this._CallbackProperty;
			}
			[CompilerGenerated]
			set
			{
				MySlider.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR77-5 == null) ? (PageSetupUI._Closure$__.$IR77-5 = delegate(object a0, bool a1)
				{
					PageSetupUI.SliderChange((MySlider)a0, a1);
				}) : PageSetupUI._Closure$__.$IR77-5;
				MySlider.ChangeEventHandler obj2 = delegate(object a0, bool a1)
				{
					this.HSL_Change();
				};
				MySlider callbackProperty = this._CallbackProperty;
				if (callbackProperty != null)
				{
					callbackProperty.WriteTests(obj);
					callbackProperty.WriteTests(obj2);
				}
				this._CallbackProperty = value;
				callbackProperty = this._CallbackProperty;
				if (callbackProperty != null)
				{
					callbackProperty.FillTests(obj);
					callbackProperty.FillTests(obj2);
				}
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x0000B5AA File Offset: 0x000097AA
		// (set) Token: 0x06001372 RID: 4978 RVA: 0x0000B5B2 File Offset: 0x000097B2
		internal virtual TextBlock LabLauncherSat { get; set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x0000B5BB File Offset: 0x000097BB
		// (set) Token: 0x06001374 RID: 4980 RVA: 0x0008761C File Offset: 0x0008581C
		internal virtual MySlider SliderLauncherSat
		{
			[CompilerGenerated]
			get
			{
				return this.m_MethodProperty;
			}
			[CompilerGenerated]
			set
			{
				MySlider.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR85-7 == null) ? (PageSetupUI._Closure$__.$IR85-7 = delegate(object a0, bool a1)
				{
					PageSetupUI.SliderChange((MySlider)a0, a1);
				}) : PageSetupUI._Closure$__.$IR85-7;
				MySlider.ChangeEventHandler obj2 = delegate(object a0, bool a1)
				{
					this.HSL_Change();
				};
				MySlider methodProperty = this.m_MethodProperty;
				if (methodProperty != null)
				{
					methodProperty.WriteTests(obj);
					methodProperty.WriteTests(obj2);
				}
				this.m_MethodProperty = value;
				methodProperty = this.m_MethodProperty;
				if (methodProperty != null)
				{
					methodProperty.FillTests(obj);
					methodProperty.FillTests(obj2);
				}
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x0000B5C3 File Offset: 0x000097C3
		// (set) Token: 0x06001376 RID: 4982 RVA: 0x0000B5CB File Offset: 0x000097CB
		internal virtual TextBlock LabLauncherLight { get; set; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x0000B5D4 File Offset: 0x000097D4
		// (set) Token: 0x06001378 RID: 4984 RVA: 0x00087694 File Offset: 0x00085894
		internal virtual MySlider SliderLauncherLight
		{
			[CompilerGenerated]
			get
			{
				return this._ConsumerProperty;
			}
			[CompilerGenerated]
			set
			{
				MySlider.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR93-9 == null) ? (PageSetupUI._Closure$__.$IR93-9 = delegate(object a0, bool a1)
				{
					PageSetupUI.SliderChange((MySlider)a0, a1);
				}) : PageSetupUI._Closure$__.$IR93-9;
				MySlider.ChangeEventHandler obj2 = delegate(object a0, bool a1)
				{
					this.HSL_Change();
				};
				MySlider consumerProperty = this._ConsumerProperty;
				if (consumerProperty != null)
				{
					consumerProperty.WriteTests(obj);
					consumerProperty.WriteTests(obj2);
				}
				this._ConsumerProperty = value;
				consumerProperty = this._ConsumerProperty;
				if (consumerProperty != null)
				{
					consumerProperty.FillTests(obj);
					consumerProperty.FillTests(obj2);
				}
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x0000B5DC File Offset: 0x000097DC
		// (set) Token: 0x0600137A RID: 4986 RVA: 0x0000B5E4 File Offset: 0x000097E4
		internal virtual Grid PanLauncherTheme { get; set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x0000B5ED File Offset: 0x000097ED
		// (set) Token: 0x0600137C RID: 4988 RVA: 0x0008770C File Offset: 0x0008590C
		internal virtual MyRadioBox RadioLauncherTheme0
		{
			[CompilerGenerated]
			get
			{
				return this._GetterProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR101-11 == null) ? (PageSetupUI._Closure$__.$IR101-11 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR101-11;
				MyRadioBox getterProperty = this._GetterProperty;
				if (getterProperty != null)
				{
					getterProperty.Check -= value2;
				}
				this._GetterProperty = value;
				getterProperty = this._GetterProperty;
				if (getterProperty != null)
				{
					getterProperty.Check += value2;
				}
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x0000B5F5 File Offset: 0x000097F5
		// (set) Token: 0x0600137E RID: 4990 RVA: 0x00087768 File Offset: 0x00085968
		internal virtual MyRadioBox RadioLauncherTheme1
		{
			[CompilerGenerated]
			get
			{
				return this.tokenProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR105-12 == null) ? (PageSetupUI._Closure$__.$IR105-12 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR105-12;
				MyRadioBox myRadioBox = this.tokenProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check -= value2;
				}
				this.tokenProperty = value;
				myRadioBox = this.tokenProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check += value2;
				}
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x0000B5FD File Offset: 0x000097FD
		// (set) Token: 0x06001380 RID: 4992 RVA: 0x000877C4 File Offset: 0x000859C4
		internal virtual MyRadioBox RadioLauncherTheme2
		{
			[CompilerGenerated]
			get
			{
				return this.m_ExpressionProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR109-13 == null) ? (PageSetupUI._Closure$__.$IR109-13 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR109-13;
				MyRadioBox expressionProperty = this.m_ExpressionProperty;
				if (expressionProperty != null)
				{
					expressionProperty.Check -= value2;
				}
				this.m_ExpressionProperty = value;
				expressionProperty = this.m_ExpressionProperty;
				if (expressionProperty != null)
				{
					expressionProperty.Check += value2;
				}
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x0000B605 File Offset: 0x00009805
		// (set) Token: 0x06001382 RID: 4994 RVA: 0x00087820 File Offset: 0x00085A20
		internal virtual MyRadioBox RadioLauncherTheme3
		{
			[CompilerGenerated]
			get
			{
				return this._WriterProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR113-14 == null) ? (PageSetupUI._Closure$__.$IR113-14 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR113-14;
				MyRadioBox writerProperty = this._WriterProperty;
				if (writerProperty != null)
				{
					writerProperty.Check -= value2;
				}
				this._WriterProperty = value;
				writerProperty = this._WriterProperty;
				if (writerProperty != null)
				{
					writerProperty.Check += value2;
				}
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x0000B60D File Offset: 0x0000980D
		// (set) Token: 0x06001384 RID: 4996 RVA: 0x0008787C File Offset: 0x00085A7C
		internal virtual MyRadioBox RadioLauncherTheme4
		{
			[CompilerGenerated]
			get
			{
				return this.m_RegistryProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR117-15 == null) ? (PageSetupUI._Closure$__.$IR117-15 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR117-15;
				MyRadioBox registryProperty = this.m_RegistryProperty;
				if (registryProperty != null)
				{
					registryProperty.Check -= value2;
				}
				this.m_RegistryProperty = value;
				registryProperty = this.m_RegistryProperty;
				if (registryProperty != null)
				{
					registryProperty.Check += value2;
				}
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x0000B615 File Offset: 0x00009815
		// (set) Token: 0x06001386 RID: 4998 RVA: 0x000878D8 File Offset: 0x00085AD8
		internal virtual MyRadioBox RadioLauncherTheme5
		{
			[CompilerGenerated]
			get
			{
				return this.ruleProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR121-16 == null) ? (PageSetupUI._Closure$__.$IR121-16 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR121-16;
				MyRadioBox myRadioBox = this.ruleProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check -= value2;
				}
				this.ruleProperty = value;
				myRadioBox = this.ruleProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check += value2;
				}
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x0000B61D File Offset: 0x0000981D
		// (set) Token: 0x06001388 RID: 5000 RVA: 0x0000B625 File Offset: 0x00009825
		internal virtual MyRadioBox RadioLauncherTheme5Gray { get; set; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x0000B62E File Offset: 0x0000982E
		// (set) Token: 0x0600138A RID: 5002 RVA: 0x00087934 File Offset: 0x00085B34
		internal virtual TextBlock LabLauncherTheme5Unlock
		{
			[CompilerGenerated]
			get
			{
				return this.setterProperty;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.LabLauncherTheme5Unlock_MouseLeftButtonUp);
				TextBlock textBlock = this.setterProperty;
				if (textBlock != null)
				{
					textBlock.MouseLeftButtonUp -= value2;
				}
				this.setterProperty = value;
				textBlock = this.setterProperty;
				if (textBlock != null)
				{
					textBlock.MouseLeftButtonUp += value2;
				}
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x0000B636 File Offset: 0x00009836
		// (set) Token: 0x0600138C RID: 5004 RVA: 0x00087978 File Offset: 0x00085B78
		internal virtual MyRadioBox MyRadioBox_0
		{
			[CompilerGenerated]
			get
			{
				return this.m_FactoryProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR133-17 == null) ? (PageSetupUI._Closure$__.$IR133-17 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR133-17;
				MyRadioBox factoryProperty = this.m_FactoryProperty;
				if (factoryProperty != null)
				{
					factoryProperty.Check -= value2;
				}
				this.m_FactoryProperty = value;
				factoryProperty = this.m_FactoryProperty;
				if (factoryProperty != null)
				{
					factoryProperty.Check += value2;
				}
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x0000B63E File Offset: 0x0000983E
		// (set) Token: 0x0600138E RID: 5006 RVA: 0x000879D4 File Offset: 0x00085BD4
		internal virtual MyRadioBox RadioLauncherTheme6
		{
			[CompilerGenerated]
			get
			{
				return this.exporterProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR137-18 == null) ? (PageSetupUI._Closure$__.$IR137-18 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR137-18;
				MyRadioBox myRadioBox = this.exporterProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check -= value2;
				}
				this.exporterProperty = value;
				myRadioBox = this.exporterProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check += value2;
				}
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x0000B646 File Offset: 0x00009846
		// (set) Token: 0x06001390 RID: 5008 RVA: 0x00087A30 File Offset: 0x00085C30
		internal virtual MyRadioBox RadioLauncherTheme7
		{
			[CompilerGenerated]
			get
			{
				return this.importerProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR141-19 == null) ? (PageSetupUI._Closure$__.$IR141-19 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR141-19;
				MyRadioBox myRadioBox = this.importerProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check -= value2;
				}
				this.importerProperty = value;
				myRadioBox = this.importerProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check += value2;
				}
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x0000B64E File Offset: 0x0000984E
		// (set) Token: 0x06001392 RID: 5010 RVA: 0x00087A8C File Offset: 0x00085C8C
		internal virtual MyRadioBox MyRadioBox_1
		{
			[CompilerGenerated]
			get
			{
				return this.workerProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR145-20 == null) ? (PageSetupUI._Closure$__.$IR145-20 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR145-20;
				MyRadioBox myRadioBox = this.workerProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check -= value2;
				}
				this.workerProperty = value;
				myRadioBox = this.workerProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check += value2;
				}
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x0000B656 File Offset: 0x00009856
		// (set) Token: 0x06001394 RID: 5012 RVA: 0x00087AE8 File Offset: 0x00085CE8
		internal virtual MyRadioBox RadioLauncherTheme8
		{
			[CompilerGenerated]
			get
			{
				return this._ConnectionProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR149-21 == null) ? (PageSetupUI._Closure$__.$IR149-21 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR149-21;
				MouseButtonEventHandler value3 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.LabLauncherTheme8Copy_MouseRightButtonUp();
				};
				MyRadioBox connectionProperty = this._ConnectionProperty;
				if (connectionProperty != null)
				{
					connectionProperty.Check -= value2;
					connectionProperty.MouseRightButtonUp -= value3;
				}
				this._ConnectionProperty = value;
				connectionProperty = this._ConnectionProperty;
				if (connectionProperty != null)
				{
					connectionProperty.Check += value2;
					connectionProperty.MouseRightButtonUp += value3;
				}
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x0000B65E File Offset: 0x0000985E
		// (set) Token: 0x06001396 RID: 5014 RVA: 0x00087B60 File Offset: 0x00085D60
		internal virtual TextBlock LabLauncherTheme8Copy
		{
			[CompilerGenerated]
			get
			{
				return this.m_ServerProperty;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.LabLauncherTheme8Copy_MouseRightButtonUp();
				};
				TextBlock serverProperty = this.m_ServerProperty;
				if (serverProperty != null)
				{
					serverProperty.MouseRightButtonUp -= value2;
				}
				this.m_ServerProperty = value;
				serverProperty = this.m_ServerProperty;
				if (serverProperty != null)
				{
					serverProperty.MouseRightButtonUp += value2;
				}
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x0000B666 File Offset: 0x00009866
		// (set) Token: 0x06001398 RID: 5016 RVA: 0x00087BA4 File Offset: 0x00085DA4
		internal virtual MyRadioBox RadioLauncherTheme9
		{
			[CompilerGenerated]
			get
			{
				return this.resolverProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR157-24 == null) ? (PageSetupUI._Closure$__.$IR157-24 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR157-24;
				MouseButtonEventHandler value3 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.LabLauncherTheme9Copy_MouseRightButtonUp();
				};
				MyRadioBox myRadioBox = this.resolverProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check -= value2;
					myRadioBox.MouseRightButtonUp -= value3;
				}
				this.resolverProperty = value;
				myRadioBox = this.resolverProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check += value2;
					myRadioBox.MouseRightButtonUp += value3;
				}
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x0000B66E File Offset: 0x0000986E
		// (set) Token: 0x0600139A RID: 5018 RVA: 0x00087C1C File Offset: 0x00085E1C
		internal virtual TextBlock LabLauncherTheme9Copy
		{
			[CompilerGenerated]
			get
			{
				return this.statusProperty;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.LabLauncherTheme9Copy_MouseRightButtonUp();
				};
				TextBlock textBlock = this.statusProperty;
				if (textBlock != null)
				{
					textBlock.MouseRightButtonUp -= value2;
				}
				this.statusProperty = value;
				textBlock = this.statusProperty;
				if (textBlock != null)
				{
					textBlock.MouseRightButtonUp += value2;
				}
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x0000B676 File Offset: 0x00009876
		// (set) Token: 0x0600139C RID: 5020 RVA: 0x00087C60 File Offset: 0x00085E60
		internal virtual MyRadioBox MyRadioBox_2
		{
			[CompilerGenerated]
			get
			{
				return this.m_RoleProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR165-27 == null) ? (PageSetupUI._Closure$__.$IR165-27 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR165-27;
				MyRadioBox roleProperty = this.m_RoleProperty;
				if (roleProperty != null)
				{
					roleProperty.Check -= value2;
				}
				this.m_RoleProperty = value;
				roleProperty = this.m_RoleProperty;
				if (roleProperty != null)
				{
					roleProperty.Check += value2;
				}
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x0000B67E File Offset: 0x0000987E
		// (set) Token: 0x0600139E RID: 5022 RVA: 0x00087CBC File Offset: 0x00085EBC
		internal virtual MyRadioBox MyRadioBox_3
		{
			[CompilerGenerated]
			get
			{
				return this._StructProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR169-28 == null) ? (PageSetupUI._Closure$__.$IR169-28 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR169-28;
				MouseButtonEventHandler value3 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.LabLauncherTheme11Click_MouseLeftButtonUp();
				};
				MyRadioBox structProperty = this._StructProperty;
				if (structProperty != null)
				{
					structProperty.Check -= value2;
					structProperty.MouseRightButtonUp -= value3;
				}
				this._StructProperty = value;
				structProperty = this._StructProperty;
				if (structProperty != null)
				{
					structProperty.Check += value2;
					structProperty.MouseRightButtonUp += value3;
				}
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x0000B686 File Offset: 0x00009886
		// (set) Token: 0x060013A0 RID: 5024 RVA: 0x00087D34 File Offset: 0x00085F34
		internal virtual TextBlock LabLauncherTheme11Click
		{
			[CompilerGenerated]
			get
			{
				return this.printerProperty;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.LabLauncherTheme11Click_MouseLeftButtonUp();
				};
				TextBlock textBlock = this.printerProperty;
				if (textBlock != null)
				{
					textBlock.MouseLeftButtonUp -= value2;
				}
				this.printerProperty = value;
				textBlock = this.printerProperty;
				if (textBlock != null)
				{
					textBlock.MouseLeftButtonUp += value2;
				}
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x0000B68E File Offset: 0x0000988E
		// (set) Token: 0x060013A2 RID: 5026 RVA: 0x00087D78 File Offset: 0x00085F78
		internal virtual MyRadioBox MyRadioBox_4
		{
			[CompilerGenerated]
			get
			{
				return this.valProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR177-31 == null) ? (PageSetupUI._Closure$__.$IR177-31 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR177-31;
				IMyRadio.ChangedEventHandler value3 = new IMyRadio.ChangedEventHandler(this.RadioLauncherTheme14_Change);
				MyRadioBox myRadioBox = this.valProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check -= value2;
					myRadioBox.Changed -= value3;
				}
				this.valProperty = value;
				myRadioBox = this.valProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check += value2;
					myRadioBox.Changed += value3;
				}
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060013A3 RID: 5027 RVA: 0x0000B696 File Offset: 0x00009896
		// (set) Token: 0x060013A4 RID: 5028 RVA: 0x00087DF0 File Offset: 0x00085FF0
		internal virtual MyCheckBox CheckLauncherLogo
		{
			[CompilerGenerated]
			get
			{
				return this.attrProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR181-32 == null) ? (PageSetupUI._Closure$__.$IR181-32 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR181-32;
				MyCheckBox myCheckBox = this.attrProperty;
				if (myCheckBox != null)
				{
					myCheckBox.PublishConfig(obj);
				}
				this.attrProperty = value;
				myCheckBox = this.attrProperty;
				if (myCheckBox != null)
				{
					myCheckBox.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x0000B69E File Offset: 0x0000989E
		// (set) Token: 0x060013A6 RID: 5030 RVA: 0x0000B6A6 File Offset: 0x000098A6
		internal virtual Border PanLauncherHide { get; set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x0000B6AF File Offset: 0x000098AF
		// (set) Token: 0x060013A8 RID: 5032 RVA: 0x00087E4C File Offset: 0x0008604C
		internal virtual MyButton BtnLauncherDonate
		{
			[CompilerGenerated]
			get
			{
				return this.m_AdvisorProperty;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnLauncherDonate_Click);
				MyButton advisorProperty = this.m_AdvisorProperty;
				if (advisorProperty != null)
				{
					advisorProperty.Click -= value2;
				}
				this.m_AdvisorProperty = value;
				advisorProperty = this.m_AdvisorProperty;
				if (advisorProperty != null)
				{
					advisorProperty.Click += value2;
				}
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x0000B6B7 File Offset: 0x000098B7
		// (set) Token: 0x060013AA RID: 5034 RVA: 0x0000B6BF File Offset: 0x000098BF
		internal virtual MyCard CardBackground { get; set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x0000B6C8 File Offset: 0x000098C8
		// (set) Token: 0x060013AC RID: 5036 RVA: 0x0000B6D0 File Offset: 0x000098D0
		internal virtual Grid PanBackgroundSuit { get; set; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x0000B6D9 File Offset: 0x000098D9
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x00087E90 File Offset: 0x00086090
		internal virtual MyComboBox ComboBackgroundSuit
		{
			[CompilerGenerated]
			get
			{
				return this._EventProperty;
			}
			[CompilerGenerated]
			set
			{
				SelectionChangedEventHandler value2 = (PageSetupUI._Closure$__.$IR201-33 == null) ? (PageSetupUI._Closure$__.$IR201-33 = delegate(object sender, SelectionChangedEventArgs e)
				{
					PageSetupUI.ComboChange((MyComboBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR201-33;
				MyComboBox eventProperty = this._EventProperty;
				if (eventProperty != null)
				{
					eventProperty.SelectionChanged -= value2;
				}
				this._EventProperty = value;
				eventProperty = this._EventProperty;
				if (eventProperty != null)
				{
					eventProperty.SelectionChanged += value2;
				}
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x0000B6E1 File Offset: 0x000098E1
		// (set) Token: 0x060013B0 RID: 5040 RVA: 0x0000B6E9 File Offset: 0x000098E9
		internal virtual Grid PanBackgroundOpacity { get; set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x0000B6F2 File Offset: 0x000098F2
		// (set) Token: 0x060013B2 RID: 5042 RVA: 0x00087EEC File Offset: 0x000860EC
		internal virtual MySlider SliderBackgroundOpacity
		{
			[CompilerGenerated]
			get
			{
				return this.m_ModelProperty;
			}
			[CompilerGenerated]
			set
			{
				MySlider.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR209-34 == null) ? (PageSetupUI._Closure$__.$IR209-34 = delegate(object a0, bool a1)
				{
					PageSetupUI.SliderChange((MySlider)a0, a1);
				}) : PageSetupUI._Closure$__.$IR209-34;
				MySlider modelProperty = this.m_ModelProperty;
				if (modelProperty != null)
				{
					modelProperty.WriteTests(obj);
				}
				this.m_ModelProperty = value;
				modelProperty = this.m_ModelProperty;
				if (modelProperty != null)
				{
					modelProperty.FillTests(obj);
				}
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x0000B6FA File Offset: 0x000098FA
		// (set) Token: 0x060013B4 RID: 5044 RVA: 0x0000B702 File Offset: 0x00009902
		internal virtual Grid PanBackgroundBlur { get; set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x0000B70B File Offset: 0x0000990B
		// (set) Token: 0x060013B6 RID: 5046 RVA: 0x00087F48 File Offset: 0x00086148
		internal virtual MySlider SliderBackgroundBlur
		{
			[CompilerGenerated]
			get
			{
				return this._BaseProperty;
			}
			[CompilerGenerated]
			set
			{
				MySlider.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR217-35 == null) ? (PageSetupUI._Closure$__.$IR217-35 = delegate(object a0, bool a1)
				{
					PageSetupUI.SliderChange((MySlider)a0, a1);
				}) : PageSetupUI._Closure$__.$IR217-35;
				MySlider baseProperty = this._BaseProperty;
				if (baseProperty != null)
				{
					baseProperty.WriteTests(obj);
				}
				this._BaseProperty = value;
				baseProperty = this._BaseProperty;
				if (baseProperty != null)
				{
					baseProperty.FillTests(obj);
				}
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x0000B713 File Offset: 0x00009913
		// (set) Token: 0x060013B8 RID: 5048 RVA: 0x00087FA4 File Offset: 0x000861A4
		internal virtual MyCheckBox CheckBackgroundColorful
		{
			[CompilerGenerated]
			get
			{
				return this.m_AttributeProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR221-36 == null) ? (PageSetupUI._Closure$__.$IR221-36 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR221-36;
				MyCheckBox attributeProperty = this.m_AttributeProperty;
				if (attributeProperty != null)
				{
					attributeProperty.PublishConfig(obj);
				}
				this.m_AttributeProperty = value;
				attributeProperty = this.m_AttributeProperty;
				if (attributeProperty != null)
				{
					attributeProperty.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x0000B71B File Offset: 0x0000991B
		// (set) Token: 0x060013BA RID: 5050 RVA: 0x00088000 File Offset: 0x00086200
		internal virtual MyButton BtnBackgroundOpen
		{
			[CompilerGenerated]
			get
			{
				return this.m_CodeProperty;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnUIBgOpen_Click);
				MyButton codeProperty = this.m_CodeProperty;
				if (codeProperty != null)
				{
					codeProperty.Click -= value2;
				}
				this.m_CodeProperty = value;
				codeProperty = this.m_CodeProperty;
				if (codeProperty != null)
				{
					codeProperty.Click += value2;
				}
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x0000B723 File Offset: 0x00009923
		// (set) Token: 0x060013BC RID: 5052 RVA: 0x00088044 File Offset: 0x00086244
		internal virtual MyButton BtnBackgroundRefresh
		{
			[CompilerGenerated]
			get
			{
				return this.m_PrototypeProperty;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnBackgroundRefresh_Click);
				MyButton prototypeProperty = this.m_PrototypeProperty;
				if (prototypeProperty != null)
				{
					prototypeProperty.Click -= value2;
				}
				this.m_PrototypeProperty = value;
				prototypeProperty = this.m_PrototypeProperty;
				if (prototypeProperty != null)
				{
					prototypeProperty.Click += value2;
				}
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x0000B72B File Offset: 0x0000992B
		// (set) Token: 0x060013BE RID: 5054 RVA: 0x00088088 File Offset: 0x00086288
		internal virtual MyButton BtnBackgroundClear
		{
			[CompilerGenerated]
			get
			{
				return this.annotationProperty;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnBackgroundClear_Click);
				MyButton myButton = this.annotationProperty;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.annotationProperty = value;
				myButton = this.annotationProperty;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x0000B733 File Offset: 0x00009933
		// (set) Token: 0x060013C0 RID: 5056 RVA: 0x0000B73B File Offset: 0x0000993B
		internal virtual MyCard CardMusic { get; set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x0000B744 File Offset: 0x00009944
		// (set) Token: 0x060013C2 RID: 5058 RVA: 0x0000B74C File Offset: 0x0000994C
		internal virtual Grid PanMusicVolume { get; set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x0000B755 File Offset: 0x00009955
		// (set) Token: 0x060013C4 RID: 5060 RVA: 0x000880CC File Offset: 0x000862CC
		internal virtual MySlider SliderMusicVolume
		{
			[CompilerGenerated]
			get
			{
				return this.m_FacadeProperty;
			}
			[CompilerGenerated]
			set
			{
				MySlider.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR245-37 == null) ? (PageSetupUI._Closure$__.$IR245-37 = delegate(object a0, bool a1)
				{
					PageSetupUI.SliderChange((MySlider)a0, a1);
				}) : PageSetupUI._Closure$__.$IR245-37;
				MySlider facadeProperty = this.m_FacadeProperty;
				if (facadeProperty != null)
				{
					facadeProperty.WriteTests(obj);
				}
				this.m_FacadeProperty = value;
				facadeProperty = this.m_FacadeProperty;
				if (facadeProperty != null)
				{
					facadeProperty.FillTests(obj);
				}
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x0000B75D File Offset: 0x0000995D
		// (set) Token: 0x060013C6 RID: 5062 RVA: 0x0000B765 File Offset: 0x00009965
		internal virtual StackPanel PanMusicDetail { get; set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x0000B76E File Offset: 0x0000996E
		// (set) Token: 0x060013C8 RID: 5064 RVA: 0x00088128 File Offset: 0x00086328
		internal virtual MyCheckBox CheckMusicRandom
		{
			[CompilerGenerated]
			get
			{
				return this.m_MerchantProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR253-38 == null) ? (PageSetupUI._Closure$__.$IR253-38 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR253-38;
				MyCheckBox merchantProperty = this.m_MerchantProperty;
				if (merchantProperty != null)
				{
					merchantProperty.PublishConfig(obj);
				}
				this.m_MerchantProperty = value;
				merchantProperty = this.m_MerchantProperty;
				if (merchantProperty != null)
				{
					merchantProperty.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x0000B776 File Offset: 0x00009976
		// (set) Token: 0x060013CA RID: 5066 RVA: 0x00088184 File Offset: 0x00086384
		internal virtual MyCheckBox CheckMusicAuto
		{
			[CompilerGenerated]
			get
			{
				return this._AuthenticationProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR257-39 == null) ? (PageSetupUI._Closure$__.$IR257-39 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR257-39;
				MyCheckBox authenticationProperty = this._AuthenticationProperty;
				if (authenticationProperty != null)
				{
					authenticationProperty.PublishConfig(obj);
				}
				this._AuthenticationProperty = value;
				authenticationProperty = this._AuthenticationProperty;
				if (authenticationProperty != null)
				{
					authenticationProperty.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x0000B77E File Offset: 0x0000997E
		// (set) Token: 0x060013CC RID: 5068 RVA: 0x000881E0 File Offset: 0x000863E0
		internal virtual MyCheckBox CheckMusicStart
		{
			[CompilerGenerated]
			get
			{
				return this.m_AlgoProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR261-40 == null) ? (PageSetupUI._Closure$__.$IR261-40 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR261-40;
				MyCheckBox.ChangeEventHandler obj2 = delegate(object a0, bool a1)
				{
					this.CheckMusicStart_Change();
				};
				MyCheckBox algoProperty = this.m_AlgoProperty;
				if (algoProperty != null)
				{
					algoProperty.PublishConfig(obj);
					algoProperty.PublishConfig(obj2);
				}
				this.m_AlgoProperty = value;
				algoProperty = this.m_AlgoProperty;
				if (algoProperty != null)
				{
					algoProperty.InstantiateConfig(obj);
					algoProperty.InstantiateConfig(obj2);
				}
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x0000B786 File Offset: 0x00009986
		// (set) Token: 0x060013CE RID: 5070 RVA: 0x00088258 File Offset: 0x00086458
		internal virtual MyCheckBox CheckMusicStop
		{
			[CompilerGenerated]
			get
			{
				return this.m_ComparatorProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR265-42 == null) ? (PageSetupUI._Closure$__.$IR265-42 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR265-42;
				MyCheckBox.ChangeEventHandler obj2 = delegate(object a0, bool a1)
				{
					this.CheckMusicStop_Change();
				};
				MyCheckBox comparatorProperty = this.m_ComparatorProperty;
				if (comparatorProperty != null)
				{
					comparatorProperty.PublishConfig(obj);
					comparatorProperty.PublishConfig(obj2);
				}
				this.m_ComparatorProperty = value;
				comparatorProperty = this.m_ComparatorProperty;
				if (comparatorProperty != null)
				{
					comparatorProperty.InstantiateConfig(obj);
					comparatorProperty.InstantiateConfig(obj2);
				}
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x0000B78E File Offset: 0x0000998E
		// (set) Token: 0x060013D0 RID: 5072 RVA: 0x000882D0 File Offset: 0x000864D0
		internal virtual MyButton BtnMusicOpen
		{
			[CompilerGenerated]
			get
			{
				return this.m_MappingProperty;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnMusicOpen_Click);
				MyButton mappingProperty = this.m_MappingProperty;
				if (mappingProperty != null)
				{
					mappingProperty.Click -= value2;
				}
				this.m_MappingProperty = value;
				mappingProperty = this.m_MappingProperty;
				if (mappingProperty != null)
				{
					mappingProperty.Click += value2;
				}
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x0000B796 File Offset: 0x00009996
		// (set) Token: 0x060013D2 RID: 5074 RVA: 0x00088314 File Offset: 0x00086514
		internal virtual MyButton BtnMusicRefresh
		{
			[CompilerGenerated]
			get
			{
				return this.tokenizerProperty;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnMusicRefresh_Click);
				MyButton myButton = this.tokenizerProperty;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.tokenizerProperty = value;
				myButton = this.tokenizerProperty;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x0000B79E File Offset: 0x0000999E
		// (set) Token: 0x060013D4 RID: 5076 RVA: 0x00088358 File Offset: 0x00086558
		internal virtual MyButton BtnMusicClear
		{
			[CompilerGenerated]
			get
			{
				return this._FilterProperty;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnMusicClear_Click);
				MyButton filterProperty = this._FilterProperty;
				if (filterProperty != null)
				{
					filterProperty.Click -= value2;
				}
				this._FilterProperty = value;
				filterProperty = this._FilterProperty;
				if (filterProperty != null)
				{
					filterProperty.Click += value2;
				}
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x0000B7A6 File Offset: 0x000099A6
		// (set) Token: 0x060013D6 RID: 5078 RVA: 0x0000B7AE File Offset: 0x000099AE
		internal virtual MyCard CardLogo { get; set; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0000B7B7 File Offset: 0x000099B7
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x0008839C File Offset: 0x0008659C
		internal virtual MyRadioBox RadioLogoType0
		{
			[CompilerGenerated]
			get
			{
				return this.predicateProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR285-44 == null) ? (PageSetupUI._Closure$__.$IR285-44 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR285-44;
				MyRadioBox myRadioBox = this.predicateProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check -= value2;
				}
				this.predicateProperty = value;
				myRadioBox = this.predicateProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check += value2;
				}
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x0000B7BF File Offset: 0x000099BF
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x000883F8 File Offset: 0x000865F8
		internal virtual MyRadioBox RadioLogoType1
		{
			[CompilerGenerated]
			get
			{
				return this._PoolProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR289-45 == null) ? (PageSetupUI._Closure$__.$IR289-45 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR289-45;
				MyRadioBox poolProperty = this._PoolProperty;
				if (poolProperty != null)
				{
					poolProperty.Check -= value2;
				}
				this._PoolProperty = value;
				poolProperty = this._PoolProperty;
				if (poolProperty != null)
				{
					poolProperty.Check += value2;
				}
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x0000B7C7 File Offset: 0x000099C7
		// (set) Token: 0x060013DC RID: 5084 RVA: 0x00088454 File Offset: 0x00086654
		internal virtual MyRadioBox RadioLogoType2
		{
			[CompilerGenerated]
			get
			{
				return this.customerProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR293-46 == null) ? (PageSetupUI._Closure$__.$IR293-46 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR293-46;
				MyRadioBox myRadioBox = this.customerProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check -= value2;
				}
				this.customerProperty = value;
				myRadioBox = this.customerProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check += value2;
				}
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x0000B7CF File Offset: 0x000099CF
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x000884B0 File Offset: 0x000866B0
		internal virtual MyRadioBox RadioLogoType3
		{
			[CompilerGenerated]
			get
			{
				return this._PageProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR297-47 == null) ? (PageSetupUI._Closure$__.$IR297-47 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR297-47;
				MyRadioBox.PreviewCheckEventHandler obj = new MyRadioBox.PreviewCheckEventHandler(this.RadioLogoType3_Check);
				MyRadioBox pageProperty = this._PageProperty;
				if (pageProperty != null)
				{
					pageProperty.Check -= value2;
					pageProperty.RateConfig(obj);
				}
				this._PageProperty = value;
				pageProperty = this._PageProperty;
				if (pageProperty != null)
				{
					pageProperty.Check += value2;
					pageProperty.UpdateConfig(obj);
				}
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0000B7D7 File Offset: 0x000099D7
		// (set) Token: 0x060013E0 RID: 5088 RVA: 0x00088528 File Offset: 0x00086728
		internal virtual MyCheckBox CheckLogoLeft
		{
			[CompilerGenerated]
			get
			{
				return this._InterceptorProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR301-48 == null) ? (PageSetupUI._Closure$__.$IR301-48 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR301-48;
				MyCheckBox interceptorProperty = this._InterceptorProperty;
				if (interceptorProperty != null)
				{
					interceptorProperty.PublishConfig(obj);
				}
				this._InterceptorProperty = value;
				interceptorProperty = this._InterceptorProperty;
				if (interceptorProperty != null)
				{
					interceptorProperty.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0000B7DF File Offset: 0x000099DF
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x0000B7E7 File Offset: 0x000099E7
		internal virtual Grid PanLogoText { get; set; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x0000B7F0 File Offset: 0x000099F0
		// (set) Token: 0x060013E4 RID: 5092 RVA: 0x00088584 File Offset: 0x00086784
		internal virtual MyTextBox TextLogoText
		{
			[CompilerGenerated]
			get
			{
				return this.paramsProperty;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageSetupUI._Closure$__.$IR309-49 == null) ? (PageSetupUI._Closure$__.$IR309-49 = delegate(object sender, RoutedEventArgs e)
				{
					PageSetupUI.TextBoxChange((MyTextBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR309-49;
				MyTextBox myTextBox = this.paramsProperty;
				if (myTextBox != null)
				{
					myTextBox.DestroyReader(value2);
				}
				this.paramsProperty = value;
				myTextBox = this.paramsProperty;
				if (myTextBox != null)
				{
					myTextBox.SetupReader(value2);
				}
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0000B7F8 File Offset: 0x000099F8
		// (set) Token: 0x060013E6 RID: 5094 RVA: 0x0000B800 File Offset: 0x00009A00
		internal virtual Grid PanLogoChange { get; set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x0000B809 File Offset: 0x00009A09
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x000885E0 File Offset: 0x000867E0
		internal virtual MyButton BtnLogoChange
		{
			[CompilerGenerated]
			get
			{
				return this.processProperty;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnLogoChange_Click);
				MyButton myButton = this.processProperty;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.processProperty = value;
				myButton = this.processProperty;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x0000B811 File Offset: 0x00009A11
		// (set) Token: 0x060013EA RID: 5098 RVA: 0x00088624 File Offset: 0x00086824
		internal virtual MyButton BtnLogoDelete
		{
			[CompilerGenerated]
			get
			{
				return this.m_ParameterProperty;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnLogoDelete_Click);
				MyButton parameterProperty = this.m_ParameterProperty;
				if (parameterProperty != null)
				{
					parameterProperty.Click -= value2;
				}
				this.m_ParameterProperty = value;
				parameterProperty = this.m_ParameterProperty;
				if (parameterProperty != null)
				{
					parameterProperty.Click += value2;
				}
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x0000B819 File Offset: 0x00009A19
		// (set) Token: 0x060013EC RID: 5100 RVA: 0x0000B821 File Offset: 0x00009A21
		internal virtual MyCard CardCustom { get; set; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x0000B82A File Offset: 0x00009A2A
		// (set) Token: 0x060013EE RID: 5102 RVA: 0x00088668 File Offset: 0x00086868
		internal virtual MyRadioBox RadioCustomType0
		{
			[CompilerGenerated]
			get
			{
				return this._ServiceProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR329-50 == null) ? (PageSetupUI._Closure$__.$IR329-50 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR329-50;
				MyRadioBox serviceProperty = this._ServiceProperty;
				if (serviceProperty != null)
				{
					serviceProperty.Check -= value2;
				}
				this._ServiceProperty = value;
				serviceProperty = this._ServiceProperty;
				if (serviceProperty != null)
				{
					serviceProperty.Check += value2;
				}
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x0000B832 File Offset: 0x00009A32
		// (set) Token: 0x060013F0 RID: 5104 RVA: 0x000886C4 File Offset: 0x000868C4
		internal virtual MyRadioBox RadioCustomType3
		{
			[CompilerGenerated]
			get
			{
				return this.invocationProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR333-51 == null) ? (PageSetupUI._Closure$__.$IR333-51 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR333-51;
				MyRadioBox myRadioBox = this.invocationProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check -= value2;
				}
				this.invocationProperty = value;
				myRadioBox = this.invocationProperty;
				if (myRadioBox != null)
				{
					myRadioBox.Check += value2;
				}
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x0000B83A File Offset: 0x00009A3A
		// (set) Token: 0x060013F2 RID: 5106 RVA: 0x00088720 File Offset: 0x00086920
		internal virtual MyRadioBox RadioCustomType1
		{
			[CompilerGenerated]
			get
			{
				return this.m_ProxyProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR337-52 == null) ? (PageSetupUI._Closure$__.$IR337-52 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR337-52;
				MyRadioBox proxyProperty = this.m_ProxyProperty;
				if (proxyProperty != null)
				{
					proxyProperty.Check -= value2;
				}
				this.m_ProxyProperty = value;
				proxyProperty = this.m_ProxyProperty;
				if (proxyProperty != null)
				{
					proxyProperty.Check += value2;
				}
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0000B842 File Offset: 0x00009A42
		// (set) Token: 0x060013F4 RID: 5108 RVA: 0x0008877C File Offset: 0x0008697C
		internal virtual MyRadioBox RadioCustomType2
		{
			[CompilerGenerated]
			get
			{
				return this._MessageProperty;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupUI._Closure$__.$IR341-53 == null) ? (PageSetupUI._Closure$__.$IR341-53 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupUI.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR341-53;
				MyRadioBox messageProperty = this._MessageProperty;
				if (messageProperty != null)
				{
					messageProperty.Check -= value2;
				}
				this._MessageProperty = value;
				messageProperty = this._MessageProperty;
				if (messageProperty != null)
				{
					messageProperty.Check += value2;
				}
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0000B84A File Offset: 0x00009A4A
		// (set) Token: 0x060013F6 RID: 5110 RVA: 0x0000B852 File Offset: 0x00009A52
		internal virtual MyHint HintCustom { get; set; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x0000B85B File Offset: 0x00009A5B
		// (set) Token: 0x060013F8 RID: 5112 RVA: 0x0000B863 File Offset: 0x00009A63
		internal virtual Grid PanCustomLocal { get; set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x0000B86C File Offset: 0x00009A6C
		// (set) Token: 0x060013FA RID: 5114 RVA: 0x000887D8 File Offset: 0x000869D8
		internal virtual MyButton BtnCustomRefresh
		{
			[CompilerGenerated]
			get
			{
				return this.singletonProperty;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.BtnCustomRefresh_Click();
				};
				MyButton myButton = this.singletonProperty;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.singletonProperty = value;
				myButton = this.singletonProperty;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x0000B874 File Offset: 0x00009A74
		// (set) Token: 0x060013FC RID: 5116 RVA: 0x0008881C File Offset: 0x00086A1C
		internal virtual MyButton BtnCustomFile
		{
			[CompilerGenerated]
			get
			{
				return this._RegProperty;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnCustomFile_Click);
				MyButton regProperty = this._RegProperty;
				if (regProperty != null)
				{
					regProperty.Click -= value2;
				}
				this._RegProperty = value;
				regProperty = this._RegProperty;
				if (regProperty != null)
				{
					regProperty.Click += value2;
				}
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x0000B87C File Offset: 0x00009A7C
		// (set) Token: 0x060013FE RID: 5118 RVA: 0x00088860 File Offset: 0x00086A60
		internal virtual MyButton BtnCustomTutorial
		{
			[CompilerGenerated]
			get
			{
				return this._ProductProperty;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnCustomTutorial_Click);
				MyButton productProperty = this._ProductProperty;
				if (productProperty != null)
				{
					productProperty.Click -= value2;
				}
				this._ProductProperty = value;
				productProperty = this._ProductProperty;
				if (productProperty != null)
				{
					productProperty.Click += value2;
				}
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x0000B884 File Offset: 0x00009A84
		// (set) Token: 0x06001400 RID: 5120 RVA: 0x0000B88C File Offset: 0x00009A8C
		internal virtual Grid PanCustomNet { get; set; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x0000B895 File Offset: 0x00009A95
		// (set) Token: 0x06001402 RID: 5122 RVA: 0x000888A4 File Offset: 0x00086AA4
		internal virtual MyTextBox TextCustomNet
		{
			[CompilerGenerated]
			get
			{
				return this.m_CollectionProperty;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageSetupUI._Closure$__.$IR369-55 == null) ? (PageSetupUI._Closure$__.$IR369-55 = delegate(object sender, RoutedEventArgs e)
				{
					PageSetupUI.TextBoxChange((MyTextBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR369-55;
				MyTextBox collectionProperty = this.m_CollectionProperty;
				if (collectionProperty != null)
				{
					collectionProperty.DestroyReader(value2);
				}
				this.m_CollectionProperty = value;
				collectionProperty = this.m_CollectionProperty;
				if (collectionProperty != null)
				{
					collectionProperty.SetupReader(value2);
				}
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x0000B89D File Offset: 0x00009A9D
		// (set) Token: 0x06001404 RID: 5124 RVA: 0x0000B8A5 File Offset: 0x00009AA5
		internal virtual Grid PanCustomPreset { get; set; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x0000B8AE File Offset: 0x00009AAE
		// (set) Token: 0x06001406 RID: 5126 RVA: 0x00088900 File Offset: 0x00086B00
		internal virtual MyComboBox ComboCustomPreset
		{
			[CompilerGenerated]
			get
			{
				return this.m_ValueProperty;
			}
			[CompilerGenerated]
			set
			{
				SelectionChangedEventHandler value2 = (PageSetupUI._Closure$__.$IR377-56 == null) ? (PageSetupUI._Closure$__.$IR377-56 = delegate(object sender, SelectionChangedEventArgs e)
				{
					PageSetupUI.ComboChange((MyComboBox)sender, e);
				}) : PageSetupUI._Closure$__.$IR377-56;
				MyComboBox valueProperty = this.m_ValueProperty;
				if (valueProperty != null)
				{
					valueProperty.SelectionChanged -= value2;
				}
				this.m_ValueProperty = value;
				valueProperty = this.m_ValueProperty;
				if (valueProperty != null)
				{
					valueProperty.SelectionChanged += value2;
				}
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x0000B8B6 File Offset: 0x00009AB6
		// (set) Token: 0x06001408 RID: 5128 RVA: 0x0000B8BE File Offset: 0x00009ABE
		internal virtual MyCard CardSwitch { get; set; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0000B8C7 File Offset: 0x00009AC7
		// (set) Token: 0x0600140A RID: 5130 RVA: 0x0008895C File Offset: 0x00086B5C
		internal virtual MyCheckBox CheckHiddenPageDownload
		{
			[CompilerGenerated]
			get
			{
				return this.bridgeProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR385-57 == null) ? (PageSetupUI._Closure$__.$IR385-57 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR385-57;
				MyCheckBox myCheckBox = this.bridgeProperty;
				if (myCheckBox != null)
				{
					myCheckBox.PublishConfig(obj);
				}
				this.bridgeProperty = value;
				myCheckBox = this.bridgeProperty;
				if (myCheckBox != null)
				{
					myCheckBox.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x0000B8CF File Offset: 0x00009ACF
		// (set) Token: 0x0600140C RID: 5132 RVA: 0x000889B8 File Offset: 0x00086BB8
		internal virtual MyCheckBox CheckHiddenPageLink
		{
			[CompilerGenerated]
			get
			{
				return this._ItemProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR389-58 == null) ? (PageSetupUI._Closure$__.$IR389-58 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR389-58;
				MyCheckBox itemProperty = this._ItemProperty;
				if (itemProperty != null)
				{
					itemProperty.PublishConfig(obj);
				}
				this._ItemProperty = value;
				itemProperty = this._ItemProperty;
				if (itemProperty != null)
				{
					itemProperty.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x0000B8D7 File Offset: 0x00009AD7
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x00088A14 File Offset: 0x00086C14
		internal virtual MyCheckBox CheckHiddenPageSetup
		{
			[CompilerGenerated]
			get
			{
				return this.reponseProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR393-59 == null) ? (PageSetupUI._Closure$__.$IR393-59 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR393-59;
				MyCheckBox.ChangeEventHandler obj2 = delegate(object a0, bool a1)
				{
					this.HiddenSetupMain();
				};
				MyCheckBox.ChangeEventHandler obj3 = new MyCheckBox.ChangeEventHandler(this.HiddenHint);
				MyCheckBox myCheckBox = this.reponseProperty;
				if (myCheckBox != null)
				{
					myCheckBox.PublishConfig(obj);
					myCheckBox.PublishConfig(obj2);
					myCheckBox.PublishConfig(obj3);
				}
				this.reponseProperty = value;
				myCheckBox = this.reponseProperty;
				if (myCheckBox != null)
				{
					myCheckBox.InstantiateConfig(obj);
					myCheckBox.InstantiateConfig(obj2);
					myCheckBox.InstantiateConfig(obj3);
				}
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x0000B8DF File Offset: 0x00009ADF
		// (set) Token: 0x06001410 RID: 5136 RVA: 0x00088AA8 File Offset: 0x00086CA8
		internal virtual MyCheckBox CheckHiddenPageOther
		{
			[CompilerGenerated]
			get
			{
				return this.globalProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR397-61 == null) ? (PageSetupUI._Closure$__.$IR397-61 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR397-61;
				MyCheckBox.ChangeEventHandler obj2 = delegate(object a0, bool a1)
				{
					this.HiddenOtherMain();
				};
				MyCheckBox myCheckBox = this.globalProperty;
				if (myCheckBox != null)
				{
					myCheckBox.PublishConfig(obj);
					myCheckBox.PublishConfig(obj2);
				}
				this.globalProperty = value;
				myCheckBox = this.globalProperty;
				if (myCheckBox != null)
				{
					myCheckBox.InstantiateConfig(obj);
					myCheckBox.InstantiateConfig(obj2);
				}
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x0000B8E7 File Offset: 0x00009AE7
		// (set) Token: 0x06001412 RID: 5138 RVA: 0x00088B20 File Offset: 0x00086D20
		internal virtual MyCheckBox CheckHiddenSetupLaunch
		{
			[CompilerGenerated]
			get
			{
				return this.m_ExceptionProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR401-63 == null) ? (PageSetupUI._Closure$__.$IR401-63 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR401-63;
				MyCheckBox.ChangeEventHandler obj2 = delegate(object a0, bool a1)
				{
					this.HiddenSetupSub();
				};
				MyCheckBox exceptionProperty = this.m_ExceptionProperty;
				if (exceptionProperty != null)
				{
					exceptionProperty.PublishConfig(obj);
					exceptionProperty.PublishConfig(obj2);
				}
				this.m_ExceptionProperty = value;
				exceptionProperty = this.m_ExceptionProperty;
				if (exceptionProperty != null)
				{
					exceptionProperty.InstantiateConfig(obj);
					exceptionProperty.InstantiateConfig(obj2);
				}
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x0000B8EF File Offset: 0x00009AEF
		// (set) Token: 0x06001414 RID: 5140 RVA: 0x00088B98 File Offset: 0x00086D98
		internal virtual MyCheckBox CheckHiddenSetupUI
		{
			[CompilerGenerated]
			get
			{
				return this._UtilsProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR405-65 == null) ? (PageSetupUI._Closure$__.$IR405-65 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR405-65;
				MyCheckBox.ChangeEventHandler obj2 = delegate(object a0, bool a1)
				{
					this.HiddenSetupSub();
				};
				MyCheckBox.ChangeEventHandler obj3 = new MyCheckBox.ChangeEventHandler(this.HiddenHint);
				MyCheckBox utilsProperty = this._UtilsProperty;
				if (utilsProperty != null)
				{
					utilsProperty.PublishConfig(obj);
					utilsProperty.PublishConfig(obj2);
					utilsProperty.PublishConfig(obj3);
				}
				this._UtilsProperty = value;
				utilsProperty = this._UtilsProperty;
				if (utilsProperty != null)
				{
					utilsProperty.InstantiateConfig(obj);
					utilsProperty.InstantiateConfig(obj2);
					utilsProperty.InstantiateConfig(obj3);
				}
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x0000B8F7 File Offset: 0x00009AF7
		// (set) Token: 0x06001416 RID: 5142 RVA: 0x00088C2C File Offset: 0x00086E2C
		internal virtual MyCheckBox CheckHiddenSetupSystem
		{
			[CompilerGenerated]
			get
			{
				return this._ClassProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR409-67 == null) ? (PageSetupUI._Closure$__.$IR409-67 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR409-67;
				MyCheckBox.ChangeEventHandler obj2 = delegate(object a0, bool a1)
				{
					this.HiddenSetupSub();
				};
				MyCheckBox classProperty = this._ClassProperty;
				if (classProperty != null)
				{
					classProperty.PublishConfig(obj);
					classProperty.PublishConfig(obj2);
				}
				this._ClassProperty = value;
				classProperty = this._ClassProperty;
				if (classProperty != null)
				{
					classProperty.InstantiateConfig(obj);
					classProperty.InstantiateConfig(obj2);
				}
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x0000B8FF File Offset: 0x00009AFF
		// (set) Token: 0x06001418 RID: 5144 RVA: 0x00088CA4 File Offset: 0x00086EA4
		internal virtual MyCheckBox CheckHiddenSetupLink
		{
			[CompilerGenerated]
			get
			{
				return this._PolicyProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR413-69 == null) ? (PageSetupUI._Closure$__.$IR413-69 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR413-69;
				MyCheckBox.ChangeEventHandler obj2 = delegate(object a0, bool a1)
				{
					this.HiddenSetupSub();
				};
				MyCheckBox policyProperty = this._PolicyProperty;
				if (policyProperty != null)
				{
					policyProperty.PublishConfig(obj);
					policyProperty.PublishConfig(obj2);
				}
				this._PolicyProperty = value;
				policyProperty = this._PolicyProperty;
				if (policyProperty != null)
				{
					policyProperty.InstantiateConfig(obj);
					policyProperty.InstantiateConfig(obj2);
				}
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x0000B907 File Offset: 0x00009B07
		// (set) Token: 0x0600141A RID: 5146 RVA: 0x00088D1C File Offset: 0x00086F1C
		internal virtual MyCheckBox CheckHiddenOtherHelp
		{
			[CompilerGenerated]
			get
			{
				return this.orderProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR417-71 == null) ? (PageSetupUI._Closure$__.$IR417-71 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR417-71;
				MyCheckBox.ChangeEventHandler obj2 = new MyCheckBox.ChangeEventHandler(this.HiddenOtherSub);
				MyCheckBox myCheckBox = this.orderProperty;
				if (myCheckBox != null)
				{
					myCheckBox.PublishConfig(obj);
					myCheckBox.PublishConfig(obj2);
				}
				this.orderProperty = value;
				myCheckBox = this.orderProperty;
				if (myCheckBox != null)
				{
					myCheckBox.InstantiateConfig(obj);
					myCheckBox.InstantiateConfig(obj2);
				}
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x0000B90F File Offset: 0x00009B0F
		// (set) Token: 0x0600141C RID: 5148 RVA: 0x00088D94 File Offset: 0x00086F94
		internal virtual MyCheckBox CheckHiddenOtherAbout
		{
			[CompilerGenerated]
			get
			{
				return this.m_ProducerProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR421-72 == null) ? (PageSetupUI._Closure$__.$IR421-72 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR421-72;
				MyCheckBox.ChangeEventHandler obj2 = new MyCheckBox.ChangeEventHandler(this.HiddenOtherSub);
				MyCheckBox producerProperty = this.m_ProducerProperty;
				if (producerProperty != null)
				{
					producerProperty.PublishConfig(obj);
					producerProperty.PublishConfig(obj2);
				}
				this.m_ProducerProperty = value;
				producerProperty = this.m_ProducerProperty;
				if (producerProperty != null)
				{
					producerProperty.InstantiateConfig(obj);
					producerProperty.InstantiateConfig(obj2);
				}
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x0000B917 File Offset: 0x00009B17
		// (set) Token: 0x0600141E RID: 5150 RVA: 0x00088E0C File Offset: 0x0008700C
		internal virtual MyCheckBox CheckHiddenOtherTest
		{
			[CompilerGenerated]
			get
			{
				return this.schemaProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR425-73 == null) ? (PageSetupUI._Closure$__.$IR425-73 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR425-73;
				MyCheckBox.ChangeEventHandler obj2 = new MyCheckBox.ChangeEventHandler(this.HiddenOtherSub);
				MyCheckBox myCheckBox = this.schemaProperty;
				if (myCheckBox != null)
				{
					myCheckBox.PublishConfig(obj);
					myCheckBox.PublishConfig(obj2);
				}
				this.schemaProperty = value;
				myCheckBox = this.schemaProperty;
				if (myCheckBox != null)
				{
					myCheckBox.InstantiateConfig(obj);
					myCheckBox.InstantiateConfig(obj2);
				}
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x0000B91F File Offset: 0x00009B1F
		// (set) Token: 0x06001420 RID: 5152 RVA: 0x00088E84 File Offset: 0x00087084
		internal virtual MyCheckBox CheckHiddenOtherFeedback
		{
			[CompilerGenerated]
			get
			{
				return this.m_DescriptorProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR429-74 == null) ? (PageSetupUI._Closure$__.$IR429-74 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR429-74;
				MyCheckBox.ChangeEventHandler obj2 = new MyCheckBox.ChangeEventHandler(this.HiddenOtherNet);
				MyCheckBox descriptorProperty = this.m_DescriptorProperty;
				if (descriptorProperty != null)
				{
					descriptorProperty.PublishConfig(obj);
					descriptorProperty.PublishConfig(obj2);
				}
				this.m_DescriptorProperty = value;
				descriptorProperty = this.m_DescriptorProperty;
				if (descriptorProperty != null)
				{
					descriptorProperty.InstantiateConfig(obj);
					descriptorProperty.InstantiateConfig(obj2);
				}
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x0000B927 File Offset: 0x00009B27
		// (set) Token: 0x06001422 RID: 5154 RVA: 0x00088EFC File Offset: 0x000870FC
		internal virtual MyCheckBox CheckHiddenOtherVote
		{
			[CompilerGenerated]
			get
			{
				return this.m_PublisherProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR433-75 == null) ? (PageSetupUI._Closure$__.$IR433-75 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR433-75;
				MyCheckBox.ChangeEventHandler obj2 = new MyCheckBox.ChangeEventHandler(this.HiddenOtherNet);
				MyCheckBox publisherProperty = this.m_PublisherProperty;
				if (publisherProperty != null)
				{
					publisherProperty.PublishConfig(obj);
					publisherProperty.PublishConfig(obj2);
				}
				this.m_PublisherProperty = value;
				publisherProperty = this.m_PublisherProperty;
				if (publisherProperty != null)
				{
					publisherProperty.InstantiateConfig(obj);
					publisherProperty.InstantiateConfig(obj2);
				}
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x0000B92F File Offset: 0x00009B2F
		// (set) Token: 0x06001424 RID: 5156 RVA: 0x00088F74 File Offset: 0x00087174
		internal virtual MyCheckBox CheckLauncherEmail
		{
			[CompilerGenerated]
			get
			{
				return this.definitionProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR437-76 == null) ? (PageSetupUI._Closure$__.$IR437-76 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR437-76;
				MyCheckBox myCheckBox = this.definitionProperty;
				if (myCheckBox != null)
				{
					myCheckBox.PublishConfig(obj);
				}
				this.definitionProperty = value;
				myCheckBox = this.definitionProperty;
				if (myCheckBox != null)
				{
					myCheckBox.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x0000B937 File Offset: 0x00009B37
		// (set) Token: 0x06001426 RID: 5158 RVA: 0x00088FD0 File Offset: 0x000871D0
		internal virtual MyCheckBox CheckHiddenFunctionSelect
		{
			[CompilerGenerated]
			get
			{
				return this._StrategyProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR441-77 == null) ? (PageSetupUI._Closure$__.$IR441-77 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR441-77;
				MyCheckBox strategyProperty = this._StrategyProperty;
				if (strategyProperty != null)
				{
					strategyProperty.PublishConfig(obj);
				}
				this._StrategyProperty = value;
				strategyProperty = this._StrategyProperty;
				if (strategyProperty != null)
				{
					strategyProperty.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x0000B93F File Offset: 0x00009B3F
		// (set) Token: 0x06001428 RID: 5160 RVA: 0x0008902C File Offset: 0x0008722C
		internal virtual MyCheckBox CheckHiddenFunctionModUpdate
		{
			[CompilerGenerated]
			get
			{
				return this.m_ProcProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR445-78 == null) ? (PageSetupUI._Closure$__.$IR445-78 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR445-78;
				MyCheckBox procProperty = this.m_ProcProperty;
				if (procProperty != null)
				{
					procProperty.PublishConfig(obj);
				}
				this.m_ProcProperty = value;
				procProperty = this.m_ProcProperty;
				if (procProperty != null)
				{
					procProperty.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0000B947 File Offset: 0x00009B47
		// (set) Token: 0x0600142A RID: 5162 RVA: 0x00089088 File Offset: 0x00087288
		internal virtual MyCheckBox CheckHiddenFunctionHidden
		{
			[CompilerGenerated]
			get
			{
				return this.parserComposer;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupUI._Closure$__.$IR449-79 == null) ? (PageSetupUI._Closure$__.$IR449-79 = delegate(object a0, bool a1)
				{
					PageSetupUI.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupUI._Closure$__.$IR449-79;
				MyCheckBox.ChangeEventHandler obj2 = new MyCheckBox.ChangeEventHandler(this.HiddenHint);
				MyCheckBox myCheckBox = this.parserComposer;
				if (myCheckBox != null)
				{
					myCheckBox.PublishConfig(obj);
					myCheckBox.PublishConfig(obj2);
				}
				this.parserComposer = value;
				myCheckBox = this.parserComposer;
				if (myCheckBox != null)
				{
					myCheckBox.InstantiateConfig(obj);
					myCheckBox.InstantiateConfig(obj2);
				}
			}
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x00089100 File Offset: 0x00087300
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_BroadcasterComposer)
			{
				this.m_BroadcasterComposer = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagesetup/pagesetupui.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x00089130 File Offset: 0x00087330
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
				this.PanMain = (StackPanel)target;
				return;
			}
			if (connectionId == 3)
			{
				this.CardLauncher = (MyCard)target;
				return;
			}
			if (connectionId == 4)
			{
				this.SliderLauncherOpacity = (MySlider)target;
				return;
			}
			if (connectionId == 5)
			{
				this.LabLauncherHue = (TextBlock)target;
				return;
			}
			if (connectionId == 6)
			{
				this.SliderLauncherHue = (MySlider)target;
				return;
			}
			if (connectionId == 7)
			{
				this.LabLauncherDelta = (TextBlock)target;
				return;
			}
			if (connectionId == 8)
			{
				this.SliderLauncherDelta = (MySlider)target;
				return;
			}
			if (connectionId == 9)
			{
				this.LabLauncherSat = (TextBlock)target;
				return;
			}
			if (connectionId == 10)
			{
				this.SliderLauncherSat = (MySlider)target;
				return;
			}
			if (connectionId == 11)
			{
				this.LabLauncherLight = (TextBlock)target;
				return;
			}
			if (connectionId == 12)
			{
				this.SliderLauncherLight = (MySlider)target;
				return;
			}
			if (connectionId == 13)
			{
				this.PanLauncherTheme = (Grid)target;
				return;
			}
			if (connectionId == 14)
			{
				this.RadioLauncherTheme0 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 15)
			{
				this.RadioLauncherTheme1 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 16)
			{
				this.RadioLauncherTheme2 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 17)
			{
				this.RadioLauncherTheme3 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 18)
			{
				this.RadioLauncherTheme4 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 19)
			{
				this.RadioLauncherTheme5 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 20)
			{
				this.RadioLauncherTheme5Gray = (MyRadioBox)target;
				return;
			}
			if (connectionId == 21)
			{
				this.LabLauncherTheme5Unlock = (TextBlock)target;
				return;
			}
			if (connectionId == 22)
			{
				this.MyRadioBox_0 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 23)
			{
				this.RadioLauncherTheme6 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 24)
			{
				this.RadioLauncherTheme7 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 25)
			{
				this.MyRadioBox_1 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 26)
			{
				this.RadioLauncherTheme8 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 27)
			{
				this.LabLauncherTheme8Copy = (TextBlock)target;
				return;
			}
			if (connectionId == 28)
			{
				this.RadioLauncherTheme9 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 29)
			{
				this.LabLauncherTheme9Copy = (TextBlock)target;
				return;
			}
			if (connectionId == 30)
			{
				this.MyRadioBox_2 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 31)
			{
				this.MyRadioBox_3 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 32)
			{
				this.LabLauncherTheme11Click = (TextBlock)target;
				return;
			}
			if (connectionId == 33)
			{
				this.MyRadioBox_4 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 34)
			{
				this.CheckLauncherLogo = (MyCheckBox)target;
				return;
			}
			if (connectionId == 35)
			{
				this.PanLauncherHide = (Border)target;
				return;
			}
			if (connectionId == 36)
			{
				this.BtnLauncherDonate = (MyButton)target;
				return;
			}
			if (connectionId == 37)
			{
				this.CardBackground = (MyCard)target;
				return;
			}
			if (connectionId == 38)
			{
				this.PanBackgroundSuit = (Grid)target;
				return;
			}
			if (connectionId == 39)
			{
				this.ComboBackgroundSuit = (MyComboBox)target;
				return;
			}
			if (connectionId == 40)
			{
				this.PanBackgroundOpacity = (Grid)target;
				return;
			}
			if (connectionId == 41)
			{
				this.SliderBackgroundOpacity = (MySlider)target;
				return;
			}
			if (connectionId == 42)
			{
				this.PanBackgroundBlur = (Grid)target;
				return;
			}
			if (connectionId == 43)
			{
				this.SliderBackgroundBlur = (MySlider)target;
				return;
			}
			if (connectionId == 44)
			{
				this.CheckBackgroundColorful = (MyCheckBox)target;
				return;
			}
			if (connectionId == 45)
			{
				this.BtnBackgroundOpen = (MyButton)target;
				return;
			}
			if (connectionId == 46)
			{
				this.BtnBackgroundRefresh = (MyButton)target;
				return;
			}
			if (connectionId == 47)
			{
				this.BtnBackgroundClear = (MyButton)target;
				return;
			}
			if (connectionId == 48)
			{
				this.CardMusic = (MyCard)target;
				return;
			}
			if (connectionId == 49)
			{
				this.PanMusicVolume = (Grid)target;
				return;
			}
			if (connectionId == 50)
			{
				this.SliderMusicVolume = (MySlider)target;
				return;
			}
			if (connectionId == 51)
			{
				this.PanMusicDetail = (StackPanel)target;
				return;
			}
			if (connectionId == 52)
			{
				this.CheckMusicRandom = (MyCheckBox)target;
				return;
			}
			if (connectionId == 53)
			{
				this.CheckMusicAuto = (MyCheckBox)target;
				return;
			}
			if (connectionId == 54)
			{
				this.CheckMusicStart = (MyCheckBox)target;
				return;
			}
			if (connectionId == 55)
			{
				this.CheckMusicStop = (MyCheckBox)target;
				return;
			}
			if (connectionId == 56)
			{
				this.BtnMusicOpen = (MyButton)target;
				return;
			}
			if (connectionId == 57)
			{
				this.BtnMusicRefresh = (MyButton)target;
				return;
			}
			if (connectionId == 58)
			{
				this.BtnMusicClear = (MyButton)target;
				return;
			}
			if (connectionId == 59)
			{
				this.CardLogo = (MyCard)target;
				return;
			}
			if (connectionId == 60)
			{
				this.RadioLogoType0 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 61)
			{
				this.RadioLogoType1 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 62)
			{
				this.RadioLogoType2 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 63)
			{
				this.RadioLogoType3 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 64)
			{
				this.CheckLogoLeft = (MyCheckBox)target;
				return;
			}
			if (connectionId == 65)
			{
				this.PanLogoText = (Grid)target;
				return;
			}
			if (connectionId == 66)
			{
				this.TextLogoText = (MyTextBox)target;
				return;
			}
			if (connectionId == 67)
			{
				this.PanLogoChange = (Grid)target;
				return;
			}
			if (connectionId == 68)
			{
				this.BtnLogoChange = (MyButton)target;
				return;
			}
			if (connectionId == 69)
			{
				this.BtnLogoDelete = (MyButton)target;
				return;
			}
			if (connectionId == 70)
			{
				this.CardCustom = (MyCard)target;
				return;
			}
			if (connectionId == 71)
			{
				this.RadioCustomType0 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 72)
			{
				this.RadioCustomType3 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 73)
			{
				this.RadioCustomType1 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 74)
			{
				this.RadioCustomType2 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 75)
			{
				this.HintCustom = (MyHint)target;
				return;
			}
			if (connectionId == 76)
			{
				this.PanCustomLocal = (Grid)target;
				return;
			}
			if (connectionId == 77)
			{
				this.BtnCustomRefresh = (MyButton)target;
				return;
			}
			if (connectionId == 78)
			{
				this.BtnCustomFile = (MyButton)target;
				return;
			}
			if (connectionId == 79)
			{
				this.BtnCustomTutorial = (MyButton)target;
				return;
			}
			if (connectionId == 80)
			{
				this.PanCustomNet = (Grid)target;
				return;
			}
			if (connectionId == 81)
			{
				this.TextCustomNet = (MyTextBox)target;
				return;
			}
			if (connectionId == 82)
			{
				this.PanCustomPreset = (Grid)target;
				return;
			}
			if (connectionId == 83)
			{
				this.ComboCustomPreset = (MyComboBox)target;
				return;
			}
			if (connectionId == 84)
			{
				this.CardSwitch = (MyCard)target;
				return;
			}
			if (connectionId == 85)
			{
				this.CheckHiddenPageDownload = (MyCheckBox)target;
				return;
			}
			if (connectionId == 86)
			{
				this.CheckHiddenPageLink = (MyCheckBox)target;
				return;
			}
			if (connectionId == 87)
			{
				this.CheckHiddenPageSetup = (MyCheckBox)target;
				return;
			}
			if (connectionId == 88)
			{
				this.CheckHiddenPageOther = (MyCheckBox)target;
				return;
			}
			if (connectionId == 89)
			{
				this.CheckHiddenSetupLaunch = (MyCheckBox)target;
				return;
			}
			if (connectionId == 90)
			{
				this.CheckHiddenSetupUI = (MyCheckBox)target;
				return;
			}
			if (connectionId == 91)
			{
				this.CheckHiddenSetupSystem = (MyCheckBox)target;
				return;
			}
			if (connectionId == 92)
			{
				this.CheckHiddenSetupLink = (MyCheckBox)target;
				return;
			}
			if (connectionId == 93)
			{
				this.CheckHiddenOtherHelp = (MyCheckBox)target;
				return;
			}
			if (connectionId == 94)
			{
				this.CheckHiddenOtherAbout = (MyCheckBox)target;
				return;
			}
			if (connectionId == 95)
			{
				this.CheckHiddenOtherTest = (MyCheckBox)target;
				return;
			}
			if (connectionId == 96)
			{
				this.CheckHiddenOtherFeedback = (MyCheckBox)target;
				return;
			}
			if (connectionId == 97)
			{
				this.CheckHiddenOtherVote = (MyCheckBox)target;
				return;
			}
			if (connectionId == 98)
			{
				this.CheckLauncherEmail = (MyCheckBox)target;
				return;
			}
			if (connectionId == 99)
			{
				this.CheckHiddenFunctionSelect = (MyCheckBox)target;
				return;
			}
			if (connectionId == 100)
			{
				this.CheckHiddenFunctionModUpdate = (MyCheckBox)target;
				return;
			}
			if (connectionId == 101)
			{
				this.CheckHiddenFunctionHidden = (MyCheckBox)target;
				return;
			}
			this.m_BroadcasterComposer = true;
		}

		// Token: 0x040009DB RID: 2523
		public bool _ParamProperty;

		// Token: 0x040009DC RID: 2524
		private static bool m_TagProperty = false;

		// Token: 0x040009DD RID: 2525
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyScrollViewer observerProperty;

		// Token: 0x040009DE RID: 2526
		[CompilerGenerated]
		[AccessedThroughProperty("PanMain")]
		private StackPanel stubProperty;

		// Token: 0x040009DF RID: 2527
		[CompilerGenerated]
		[AccessedThroughProperty("CardLauncher")]
		private MyCard m_RulesProperty;

		// Token: 0x040009E0 RID: 2528
		[AccessedThroughProperty("SliderLauncherOpacity")]
		[CompilerGenerated]
		private MySlider refProperty;

		// Token: 0x040009E1 RID: 2529
		[CompilerGenerated]
		[AccessedThroughProperty("LabLauncherHue")]
		private TextBlock decoratorProperty;

		// Token: 0x040009E2 RID: 2530
		[CompilerGenerated]
		[AccessedThroughProperty("SliderLauncherHue")]
		private MySlider m_InstanceProperty;

		// Token: 0x040009E3 RID: 2531
		[CompilerGenerated]
		[AccessedThroughProperty("LabLauncherDelta")]
		private TextBlock m_StateProperty;

		// Token: 0x040009E4 RID: 2532
		[AccessedThroughProperty("SliderLauncherDelta")]
		[CompilerGenerated]
		private MySlider _CallbackProperty;

		// Token: 0x040009E5 RID: 2533
		[CompilerGenerated]
		[AccessedThroughProperty("LabLauncherSat")]
		private TextBlock m_TemplateProperty;

		// Token: 0x040009E6 RID: 2534
		[CompilerGenerated]
		[AccessedThroughProperty("SliderLauncherSat")]
		private MySlider m_MethodProperty;

		// Token: 0x040009E7 RID: 2535
		[CompilerGenerated]
		[AccessedThroughProperty("LabLauncherLight")]
		private TextBlock _TaskProperty;

		// Token: 0x040009E8 RID: 2536
		[AccessedThroughProperty("SliderLauncherLight")]
		[CompilerGenerated]
		private MySlider _ConsumerProperty;

		// Token: 0x040009E9 RID: 2537
		[CompilerGenerated]
		[AccessedThroughProperty("PanLauncherTheme")]
		private Grid configurationProperty;

		// Token: 0x040009EA RID: 2538
		[CompilerGenerated]
		[AccessedThroughProperty("RadioLauncherTheme0")]
		private MyRadioBox _GetterProperty;

		// Token: 0x040009EB RID: 2539
		[AccessedThroughProperty("RadioLauncherTheme1")]
		[CompilerGenerated]
		private MyRadioBox tokenProperty;

		// Token: 0x040009EC RID: 2540
		[CompilerGenerated]
		[AccessedThroughProperty("RadioLauncherTheme2")]
		private MyRadioBox m_ExpressionProperty;

		// Token: 0x040009ED RID: 2541
		[CompilerGenerated]
		[AccessedThroughProperty("RadioLauncherTheme3")]
		private MyRadioBox _WriterProperty;

		// Token: 0x040009EE RID: 2542
		[AccessedThroughProperty("RadioLauncherTheme4")]
		[CompilerGenerated]
		private MyRadioBox m_RegistryProperty;

		// Token: 0x040009EF RID: 2543
		[CompilerGenerated]
		[AccessedThroughProperty("RadioLauncherTheme5")]
		private MyRadioBox ruleProperty;

		// Token: 0x040009F0 RID: 2544
		[AccessedThroughProperty("RadioLauncherTheme5Gray")]
		[CompilerGenerated]
		private MyRadioBox proccesorProperty;

		// Token: 0x040009F1 RID: 2545
		[CompilerGenerated]
		[AccessedThroughProperty("LabLauncherTheme5Unlock")]
		private TextBlock setterProperty;

		// Token: 0x040009F2 RID: 2546
		[CompilerGenerated]
		[AccessedThroughProperty("RadioLauncherTheme12")]
		private MyRadioBox m_FactoryProperty;

		// Token: 0x040009F3 RID: 2547
		[CompilerGenerated]
		[AccessedThroughProperty("RadioLauncherTheme6")]
		private MyRadioBox exporterProperty;

		// Token: 0x040009F4 RID: 2548
		[CompilerGenerated]
		[AccessedThroughProperty("RadioLauncherTheme7")]
		private MyRadioBox importerProperty;

		// Token: 0x040009F5 RID: 2549
		[CompilerGenerated]
		[AccessedThroughProperty("RadioLauncherTheme13")]
		private MyRadioBox workerProperty;

		// Token: 0x040009F6 RID: 2550
		[AccessedThroughProperty("RadioLauncherTheme8")]
		[CompilerGenerated]
		private MyRadioBox _ConnectionProperty;

		// Token: 0x040009F7 RID: 2551
		[CompilerGenerated]
		[AccessedThroughProperty("LabLauncherTheme8Copy")]
		private TextBlock m_ServerProperty;

		// Token: 0x040009F8 RID: 2552
		[CompilerGenerated]
		[AccessedThroughProperty("RadioLauncherTheme9")]
		private MyRadioBox resolverProperty;

		// Token: 0x040009F9 RID: 2553
		[CompilerGenerated]
		[AccessedThroughProperty("LabLauncherTheme9Copy")]
		private TextBlock statusProperty;

		// Token: 0x040009FA RID: 2554
		[CompilerGenerated]
		[AccessedThroughProperty("RadioLauncherTheme10")]
		private MyRadioBox m_RoleProperty;

		// Token: 0x040009FB RID: 2555
		[AccessedThroughProperty("RadioLauncherTheme11")]
		[CompilerGenerated]
		private MyRadioBox _StructProperty;

		// Token: 0x040009FC RID: 2556
		[AccessedThroughProperty("LabLauncherTheme11Click")]
		[CompilerGenerated]
		private TextBlock printerProperty;

		// Token: 0x040009FD RID: 2557
		[AccessedThroughProperty("RadioLauncherTheme14")]
		[CompilerGenerated]
		private MyRadioBox valProperty;

		// Token: 0x040009FE RID: 2558
		[AccessedThroughProperty("CheckLauncherLogo")]
		[CompilerGenerated]
		private MyCheckBox attrProperty;

		// Token: 0x040009FF RID: 2559
		[CompilerGenerated]
		[AccessedThroughProperty("PanLauncherHide")]
		private Border m_CandidateProperty;

		// Token: 0x04000A00 RID: 2560
		[CompilerGenerated]
		[AccessedThroughProperty("BtnLauncherDonate")]
		private MyButton m_AdvisorProperty;

		// Token: 0x04000A01 RID: 2561
		[AccessedThroughProperty("CardBackground")]
		[CompilerGenerated]
		private MyCard _AccountProperty;

		// Token: 0x04000A02 RID: 2562
		[AccessedThroughProperty("PanBackgroundSuit")]
		[CompilerGenerated]
		private Grid _QueueProperty;

		// Token: 0x04000A03 RID: 2563
		[CompilerGenerated]
		[AccessedThroughProperty("ComboBackgroundSuit")]
		private MyComboBox _EventProperty;

		// Token: 0x04000A04 RID: 2564
		[AccessedThroughProperty("PanBackgroundOpacity")]
		[CompilerGenerated]
		private Grid _ManagerProperty;

		// Token: 0x04000A05 RID: 2565
		[AccessedThroughProperty("SliderBackgroundOpacity")]
		[CompilerGenerated]
		private MySlider m_ModelProperty;

		// Token: 0x04000A06 RID: 2566
		[AccessedThroughProperty("PanBackgroundBlur")]
		[CompilerGenerated]
		private Grid _WrapperProperty;

		// Token: 0x04000A07 RID: 2567
		[CompilerGenerated]
		[AccessedThroughProperty("SliderBackgroundBlur")]
		private MySlider _BaseProperty;

		// Token: 0x04000A08 RID: 2568
		[AccessedThroughProperty("CheckBackgroundColorful")]
		[CompilerGenerated]
		private MyCheckBox m_AttributeProperty;

		// Token: 0x04000A09 RID: 2569
		[AccessedThroughProperty("BtnBackgroundOpen")]
		[CompilerGenerated]
		private MyButton m_CodeProperty;

		// Token: 0x04000A0A RID: 2570
		[CompilerGenerated]
		[AccessedThroughProperty("BtnBackgroundRefresh")]
		private MyButton m_PrototypeProperty;

		// Token: 0x04000A0B RID: 2571
		[CompilerGenerated]
		[AccessedThroughProperty("BtnBackgroundClear")]
		private MyButton annotationProperty;

		// Token: 0x04000A0C RID: 2572
		[AccessedThroughProperty("CardMusic")]
		[CompilerGenerated]
		private MyCard m_InfoProperty;

		// Token: 0x04000A0D RID: 2573
		[CompilerGenerated]
		[AccessedThroughProperty("PanMusicVolume")]
		private Grid m_AdapterProperty;

		// Token: 0x04000A0E RID: 2574
		[CompilerGenerated]
		[AccessedThroughProperty("SliderMusicVolume")]
		private MySlider m_FacadeProperty;

		// Token: 0x04000A0F RID: 2575
		[AccessedThroughProperty("PanMusicDetail")]
		[CompilerGenerated]
		private StackPanel m_ListProperty;

		// Token: 0x04000A10 RID: 2576
		[AccessedThroughProperty("CheckMusicRandom")]
		[CompilerGenerated]
		private MyCheckBox m_MerchantProperty;

		// Token: 0x04000A11 RID: 2577
		[AccessedThroughProperty("CheckMusicAuto")]
		[CompilerGenerated]
		private MyCheckBox _AuthenticationProperty;

		// Token: 0x04000A12 RID: 2578
		[AccessedThroughProperty("CheckMusicStart")]
		[CompilerGenerated]
		private MyCheckBox m_AlgoProperty;

		// Token: 0x04000A13 RID: 2579
		[CompilerGenerated]
		[AccessedThroughProperty("CheckMusicStop")]
		private MyCheckBox m_ComparatorProperty;

		// Token: 0x04000A14 RID: 2580
		[AccessedThroughProperty("BtnMusicOpen")]
		[CompilerGenerated]
		private MyButton m_MappingProperty;

		// Token: 0x04000A15 RID: 2581
		[AccessedThroughProperty("BtnMusicRefresh")]
		[CompilerGenerated]
		private MyButton tokenizerProperty;

		// Token: 0x04000A16 RID: 2582
		[CompilerGenerated]
		[AccessedThroughProperty("BtnMusicClear")]
		private MyButton _FilterProperty;

		// Token: 0x04000A17 RID: 2583
		[CompilerGenerated]
		[AccessedThroughProperty("CardLogo")]
		private MyCard m_DatabaseProperty;

		// Token: 0x04000A18 RID: 2584
		[AccessedThroughProperty("RadioLogoType0")]
		[CompilerGenerated]
		private MyRadioBox predicateProperty;

		// Token: 0x04000A19 RID: 2585
		[AccessedThroughProperty("RadioLogoType1")]
		[CompilerGenerated]
		private MyRadioBox _PoolProperty;

		// Token: 0x04000A1A RID: 2586
		[CompilerGenerated]
		[AccessedThroughProperty("RadioLogoType2")]
		private MyRadioBox customerProperty;

		// Token: 0x04000A1B RID: 2587
		[AccessedThroughProperty("RadioLogoType3")]
		[CompilerGenerated]
		private MyRadioBox _PageProperty;

		// Token: 0x04000A1C RID: 2588
		[CompilerGenerated]
		[AccessedThroughProperty("CheckLogoLeft")]
		private MyCheckBox _InterceptorProperty;

		// Token: 0x04000A1D RID: 2589
		[AccessedThroughProperty("PanLogoText")]
		[CompilerGenerated]
		private Grid _ContainerProperty;

		// Token: 0x04000A1E RID: 2590
		[AccessedThroughProperty("TextLogoText")]
		[CompilerGenerated]
		private MyTextBox paramsProperty;

		// Token: 0x04000A1F RID: 2591
		[CompilerGenerated]
		[AccessedThroughProperty("PanLogoChange")]
		private Grid _DispatcherProperty;

		// Token: 0x04000A20 RID: 2592
		[CompilerGenerated]
		[AccessedThroughProperty("BtnLogoChange")]
		private MyButton processProperty;

		// Token: 0x04000A21 RID: 2593
		[CompilerGenerated]
		[AccessedThroughProperty("BtnLogoDelete")]
		private MyButton m_ParameterProperty;

		// Token: 0x04000A22 RID: 2594
		[AccessedThroughProperty("CardCustom")]
		[CompilerGenerated]
		private MyCard _RecordProperty;

		// Token: 0x04000A23 RID: 2595
		[AccessedThroughProperty("RadioCustomType0")]
		[CompilerGenerated]
		private MyRadioBox _ServiceProperty;

		// Token: 0x04000A24 RID: 2596
		[CompilerGenerated]
		[AccessedThroughProperty("RadioCustomType3")]
		private MyRadioBox invocationProperty;

		// Token: 0x04000A25 RID: 2597
		[CompilerGenerated]
		[AccessedThroughProperty("RadioCustomType1")]
		private MyRadioBox m_ProxyProperty;

		// Token: 0x04000A26 RID: 2598
		[AccessedThroughProperty("RadioCustomType2")]
		[CompilerGenerated]
		private MyRadioBox _MessageProperty;

		// Token: 0x04000A27 RID: 2599
		[AccessedThroughProperty("HintCustom")]
		[CompilerGenerated]
		private MyHint m_CreatorProperty;

		// Token: 0x04000A28 RID: 2600
		[CompilerGenerated]
		[AccessedThroughProperty("PanCustomLocal")]
		private Grid m_InitializerProperty;

		// Token: 0x04000A29 RID: 2601
		[CompilerGenerated]
		[AccessedThroughProperty("BtnCustomRefresh")]
		private MyButton singletonProperty;

		// Token: 0x04000A2A RID: 2602
		[AccessedThroughProperty("BtnCustomFile")]
		[CompilerGenerated]
		private MyButton _RegProperty;

		// Token: 0x04000A2B RID: 2603
		[AccessedThroughProperty("BtnCustomTutorial")]
		[CompilerGenerated]
		private MyButton _ProductProperty;

		// Token: 0x04000A2C RID: 2604
		[AccessedThroughProperty("PanCustomNet")]
		[CompilerGenerated]
		private Grid m_ListenerProperty;

		// Token: 0x04000A2D RID: 2605
		[CompilerGenerated]
		[AccessedThroughProperty("TextCustomNet")]
		private MyTextBox m_CollectionProperty;

		// Token: 0x04000A2E RID: 2606
		[AccessedThroughProperty("PanCustomPreset")]
		[CompilerGenerated]
		private Grid m_VisitorProperty;

		// Token: 0x04000A2F RID: 2607
		[CompilerGenerated]
		[AccessedThroughProperty("ComboCustomPreset")]
		private MyComboBox m_ValueProperty;

		// Token: 0x04000A30 RID: 2608
		[CompilerGenerated]
		[AccessedThroughProperty("CardSwitch")]
		private MyCard _ObjectProperty;

		// Token: 0x04000A31 RID: 2609
		[AccessedThroughProperty("CheckHiddenPageDownload")]
		[CompilerGenerated]
		private MyCheckBox bridgeProperty;

		// Token: 0x04000A32 RID: 2610
		[AccessedThroughProperty("CheckHiddenPageLink")]
		[CompilerGenerated]
		private MyCheckBox _ItemProperty;

		// Token: 0x04000A33 RID: 2611
		[AccessedThroughProperty("CheckHiddenPageSetup")]
		[CompilerGenerated]
		private MyCheckBox reponseProperty;

		// Token: 0x04000A34 RID: 2612
		[CompilerGenerated]
		[AccessedThroughProperty("CheckHiddenPageOther")]
		private MyCheckBox globalProperty;

		// Token: 0x04000A35 RID: 2613
		[AccessedThroughProperty("CheckHiddenSetupLaunch")]
		[CompilerGenerated]
		private MyCheckBox m_ExceptionProperty;

		// Token: 0x04000A36 RID: 2614
		[CompilerGenerated]
		[AccessedThroughProperty("CheckHiddenSetupUI")]
		private MyCheckBox _UtilsProperty;

		// Token: 0x04000A37 RID: 2615
		[AccessedThroughProperty("CheckHiddenSetupSystem")]
		[CompilerGenerated]
		private MyCheckBox _ClassProperty;

		// Token: 0x04000A38 RID: 2616
		[CompilerGenerated]
		[AccessedThroughProperty("CheckHiddenSetupLink")]
		private MyCheckBox _PolicyProperty;

		// Token: 0x04000A39 RID: 2617
		[CompilerGenerated]
		[AccessedThroughProperty("CheckHiddenOtherHelp")]
		private MyCheckBox orderProperty;

		// Token: 0x04000A3A RID: 2618
		[AccessedThroughProperty("CheckHiddenOtherAbout")]
		[CompilerGenerated]
		private MyCheckBox m_ProducerProperty;

		// Token: 0x04000A3B RID: 2619
		[CompilerGenerated]
		[AccessedThroughProperty("CheckHiddenOtherTest")]
		private MyCheckBox schemaProperty;

		// Token: 0x04000A3C RID: 2620
		[AccessedThroughProperty("CheckHiddenOtherFeedback")]
		[CompilerGenerated]
		private MyCheckBox m_DescriptorProperty;

		// Token: 0x04000A3D RID: 2621
		[CompilerGenerated]
		[AccessedThroughProperty("CheckHiddenOtherVote")]
		private MyCheckBox m_PublisherProperty;

		// Token: 0x04000A3E RID: 2622
		[CompilerGenerated]
		[AccessedThroughProperty("CheckLauncherEmail")]
		private MyCheckBox definitionProperty;

		// Token: 0x04000A3F RID: 2623
		[CompilerGenerated]
		[AccessedThroughProperty("CheckHiddenFunctionSelect")]
		private MyCheckBox _StrategyProperty;

		// Token: 0x04000A40 RID: 2624
		[AccessedThroughProperty("CheckHiddenFunctionModUpdate")]
		[CompilerGenerated]
		private MyCheckBox m_ProcProperty;

		// Token: 0x04000A41 RID: 2625
		[CompilerGenerated]
		[AccessedThroughProperty("CheckHiddenFunctionHidden")]
		private MyCheckBox parserComposer;

		// Token: 0x04000A42 RID: 2626
		private bool m_BroadcasterComposer;
	}
}
