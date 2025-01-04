using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PCL.My;

namespace PCL
{
	// Token: 0x0200011D RID: 285
	[DesignerGenerated]
	public class PageVersionSetup : MyPageRight, IComponentConnector
	{
		// Token: 0x06000BEB RID: 3051 RVA: 0x00008134 File Offset: 0x00006334
		public PageVersionSetup()
		{
			base.Loaded += this.PageSetupSystem_Loaded;
			this.listenerConfig = false;
			this.collectionConfig = 2;
			this.m_VisitorConfig = 1;
			this._ObjectConfig = false;
			this.InitializeComponent();
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00050D70 File Offset: 0x0004EF70
		private void PageSetupSystem_Loaded(object sender, RoutedEventArgs e)
		{
			this.PanBack.ScrollToHome();
			this.RefreshRam(false);
			checked
			{
				ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
				this.Reload();
				ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
				if (!this.listenerConfig)
				{
					this.listenerConfig = true;
					DispatcherTimer dispatcherTimer = new DispatcherTimer();
					dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
					dispatcherTimer.Tick += delegate(object sender, EventArgs e)
					{
						this.RefreshRam();
					};
					dispatcherTimer.Start();
				}
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00050DE8 File Offset: 0x0004EFE8
		public void Reload()
		{
			try
			{
				this.TextArgumentTitle.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionArgumentTitle", PageVersionLeft._InstanceConfig));
				this.TextArgumentInfo.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionArgumentInfo", PageVersionLeft._InstanceConfig));
				int num = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("VersionArgumentIndie", PageVersionLeft._InstanceConfig));
				if (num == -1)
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(PageVersionLeft._InstanceConfig.Path + "mods\\");
					DirectoryInfo directoryInfo2 = new DirectoryInfo(PageVersionLeft._InstanceConfig.Path + "saves\\");
					if ((directoryInfo.Exists && Enumerable.Any<FileInfo>(directoryInfo.EnumerateFiles())) || (directoryInfo2.Exists && Enumerable.Any<FileInfo>(directoryInfo2.EnumerateFiles())))
					{
						ModBase.m_IdentifierRepository.Set("VersionArgumentIndie", 1, false, PageVersionLeft._InstanceConfig);
						ModBase.Log("[Setup] 已自动开启单版本隔离：" + PageVersionLeft._InstanceConfig.Name, ModBase.LogLevel.Normal, "出现错误");
						num = 1;
					}
					else
					{
						ModBase.m_IdentifierRepository.Set("VersionArgumentIndie", 0, false, PageVersionLeft._InstanceConfig);
						ModBase.Log("[Setup] 版本隔离使用全局设置：" + PageVersionLeft._InstanceConfig.Name, ModBase.LogLevel.Normal, "出现错误");
						num = 0;
					}
				}
				this.ComboArgumentIndie.SelectedIndex = num;
				this.RefreshJavaComboBox();
				((MyRadioBox)base.FindName(Conversions.ToString(Operators.ConcatenateObject("RadioRamType", ModBase.m_IdentifierRepository.Load("VersionRamType", false, PageVersionLeft._InstanceConfig))))).Checked = true;
				this.SliderRamCustom.Value = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("VersionRamCustom", PageVersionLeft._InstanceConfig));
				this.ComboRamOptimize.SelectedIndex = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("VersionRamOptimize", PageVersionLeft._InstanceConfig));
				this.TextServerEnter.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionServerEnter", PageVersionLeft._InstanceConfig));
				this.ComboServerLogin.SelectedIndex = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("VersionServerLogin", PageVersionLeft._InstanceConfig));
				this.valueConfig = this.ComboServerLogin.SelectedIndex;
				this.ServerLogin(this.ComboServerLogin.SelectedIndex);
				this.TextServerNide.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionServerNide", PageVersionLeft._InstanceConfig));
				this.TextServerAuthServer.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionServerAuthServer", PageVersionLeft._InstanceConfig));
				this.TextServerAuthName.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionServerAuthName", PageVersionLeft._InstanceConfig));
				this.TextServerAuthRegister.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionServerAuthRegister", PageVersionLeft._InstanceConfig));
				this.TextAdvanceJvm.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionAdvanceJvm", PageVersionLeft._InstanceConfig));
				this.TextAdvanceGame.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionAdvanceGame", PageVersionLeft._InstanceConfig));
				this.TextAdvanceRun.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionAdvanceRun", PageVersionLeft._InstanceConfig));
				this.CheckAdvanceRunWait.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("VersionAdvanceRunWait", PageVersionLeft._InstanceConfig));
				if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("VersionAdvanceAssets", PageVersionLeft._InstanceConfig), 2, false))
				{
					ModBase.Log("[Setup] 已迁移老版本的关闭文件校验设置", ModBase.LogLevel.Normal, "出现错误");
					ModBase.m_IdentifierRepository.Reset("VersionAdvanceAssets", false, PageVersionLeft._InstanceConfig);
					ModBase.m_IdentifierRepository.Set("VersionAdvanceAssetsV2", true, false, PageVersionLeft._InstanceConfig);
				}
				this.MyCheckBox_0.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("VersionAdvanceAssetsV2", PageVersionLeft._InstanceConfig));
				this.CheckAdvanceJava.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("VersionAdvanceJava", PageVersionLeft._InstanceConfig));
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "重载版本独立设置时出错", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00051238 File Offset: 0x0004F438
		public void Reset()
		{
			try
			{
				ModBase.m_IdentifierRepository.Reset("VersionServerEnter", false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Reset("VersionServerLogin", false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Reset("VersionServerNide", false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Reset("VersionServerAuthServer", false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Reset("VersionServerAuthRegister", false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Reset("VersionServerAuthName", false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Reset("VersionArgumentTitle", false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Reset("VersionArgumentInfo", false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Set("VersionArgumentIndie", 0, false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Reset("VersionRamType", false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Reset("VersionRamCustom", false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Reset("VersionRamOptimize", false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Reset("VersionAdvanceJvm", false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Reset("VersionAdvanceGame", false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Reset("VersionAdvanceAssets", false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Reset("VersionAdvanceAssetsV2", false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Reset("VersionAdvanceJava", false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Reset("VersionAdvanceRun", false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Reset("VersionAdvanceRunWait", false, PageVersionLeft._InstanceConfig);
				ModBase.m_IdentifierRepository.Reset("VersionArgumentJavaSelect", false, PageVersionLeft._InstanceConfig);
				ModJava._Process.Start(null, true);
				ModBase.Log("[Setup] 已初始化版本独立设置", ModBase.LogLevel.Normal, "出现错误");
				ModMain.Hint("已初始化版本独立设置！", ModMain.HintType.Finish, false);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "初始化版本独立设置失败", ModBase.LogLevel.Msgbox, "出现错误");
			}
			this.Reload();
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0005145C File Offset: 0x0004F65C
		private static void RadioBoxChange(MyRadioBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(sender.Tag.ToString().Split("/")[0], ModBase.Val(sender.Tag.ToString().Split("/")[1]), false, PageVersionLeft._InstanceConfig);
			}
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00008171 File Offset: 0x00006371
		private static void TextBoxChange(MyTextBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.Text, false, PageVersionLeft._InstanceConfig);
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0000819B File Offset: 0x0000639B
		private static void SliderChange(MySlider sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.Value, false, PageVersionLeft._InstanceConfig);
			}
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000081CA File Offset: 0x000063CA
		private static void ComboChange(MyComboBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.SelectedIndex, false, PageVersionLeft._InstanceConfig);
			}
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x000081F9 File Offset: 0x000063F9
		private static void CheckBoxChange(MyCheckBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.Checked, false, PageVersionLeft._InstanceConfig);
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00008228 File Offset: 0x00006428
		public void RamType(int Type)
		{
			if (this.SliderRamCustom != null)
			{
				this.SliderRamCustom.IsEnabled = (Type == 1);
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x000514B8 File Offset: 0x0004F6B8
		public void RefreshRam(bool ShowAnim)
		{
			if (this.LabRamGame != null && this.LabRamUsed != null && !(ModMain._ProcessIterator._MethodIterator != FormMain.PageType.VersionSetup) && ModMain.m_TestsRepository._StateConfig == FormMain.PageSubType.DownloadInstall)
			{
				double ram = PageVersionSetup.GetRam(PageVersionLeft._InstanceConfig, null);
				double num = Math.Round(MyWpfExtension.ManageParser().Info.TotalPhysicalMemory / 1024.0 / 1024.0 / 1024.0 * 10.0) / 10.0;
				double num2 = Math.Round(MyWpfExtension.ManageParser().Info.AvailablePhysicalMemory / 1024.0 / 1024.0 / 1024.0 * 10.0) / 10.0;
				double num3 = Math.Min(ram, num2);
				double num4 = num - num2;
				double num5 = Math.Round(ModBase.MathClamp(num - num4 - ram, 0.0, 1000.0) * 10.0) / 10.0;
				checked
				{
					if (num <= 1.5)
					{
						this.SliderRamCustom.MaxValue = (int)Math.Round(Math.Max(Math.Floor(unchecked(num - 0.3) / 0.1), 1.0));
					}
					else if (num <= 8.0)
					{
						this.SliderRamCustom.MaxValue = (int)Math.Round(unchecked(Math.Floor((num - 1.5) / 0.5) + 12.0));
					}
					else if (num <= 16.0)
					{
						this.SliderRamCustom.MaxValue = (int)Math.Round(unchecked(Math.Floor((num - 8.0) / 1.0) + 25.0));
					}
					else
					{
						this.SliderRamCustom.MaxValue = (int)Math.Round(Math.Min(unchecked(Math.Floor((num - 16.0) / 2.0) + 33.0), 41.0));
					}
					this.LabRamGame.Text = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject((ram == Math.Floor(ram)) ? (Conversions.ToString(ram) + ".0") : ram, " GB"), (ram != num3) ? Operators.ConcatenateObject(Operators.ConcatenateObject(" (可用 ", (num3 == Math.Floor(num3)) ? (Conversions.ToString(num3) + ".0") : num3), " GB)") : ""));
					this.LabRamUsed.Text = Conversions.ToString(Operators.ConcatenateObject((num4 == Math.Floor(num4)) ? (Conversions.ToString(num4) + ".0") : num4, " GB"));
					this.LabRamTotal.Text = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(" / ", (num == Math.Floor(num)) ? (Conversions.ToString(num) + ".0") : num), " GB"));
					this.LabRamWarn.Visibility = ((ram != 1.0 || ModJava.smethod_0(PageVersionLeft._InstanceConfig) || ModBase.m_StubRepository || !Enumerable.Any<ModJava.JavaEntry>(ModJava.interceptor)) ? Visibility.Collapsed : Visibility.Visible);
				}
				if (ShowAnim)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaGridLengthWidth(this.ColumnRamUsed, num4 - this.ColumnRamUsed.Width.Value, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false),
						ModAnimation.AaGridLengthWidth(this.ColumnRamGame, num3 - this.ColumnRamGame.Width.Value, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false),
						ModAnimation.AaGridLengthWidth(this.ColumnRamEmpty, num5 - this.ColumnRamEmpty.Width.Value, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false)
					}, "VersionSetup Ram Grid", false);
					return;
				}
				this.ColumnRamUsed.Width = new GridLength(num4, GridUnitType.Star);
				this.ColumnRamGame.Width = new GridLength(num3, GridUnitType.Star);
				this.ColumnRamEmpty.Width = new GridLength(num5, GridUnitType.Star);
			}
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00008241 File Offset: 0x00006441
		private void RefreshRam()
		{
			this.RefreshRam(true);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00051938 File Offset: 0x0004FB38
		private void RefreshRamText()
		{
			double actualWidth = this.RectRamUsed.ActualWidth;
			double actualWidth2 = this.PanRamDisplay.ActualWidth;
			double actualWidth3 = this.LabRamGame.ActualWidth;
			double actualWidth4 = this.LabRamUsed.ActualWidth;
			double actualWidth5 = this.LabRamTotal.ActualWidth;
			double actualWidth6 = this.LabRamGameTitle.ActualWidth;
			double actualWidth7 = this.LabRamUsedTitle.ActualWidth;
			int num;
			if (actualWidth - 30.0 >= actualWidth4 && actualWidth - 30.0 >= actualWidth7)
			{
				if (actualWidth - 25.0 < actualWidth4 + actualWidth5)
				{
					num = 1;
				}
				else
				{
					num = 2;
				}
			}
			else
			{
				num = 0;
			}
			if (this.collectionConfig != num)
			{
				this.collectionConfig = num;
				switch (num)
				{
				case 0:
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaOpacity(this.LabRamUsed, -this.LabRamUsed.Opacity, 100, 0, null, false),
						ModAnimation.AaOpacity(this.LabRamTotal, -this.LabRamTotal.Opacity, 100, 0, null, false),
						ModAnimation.AaOpacity(this.LabRamUsedTitle, -this.LabRamUsedTitle.Opacity, 100, 0, null, false)
					}, "VersionSetup Ram TextLeft", false);
					break;
				case 1:
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaOpacity(this.LabRamUsed, 1.0 - this.LabRamUsed.Opacity, 100, 0, null, false),
						ModAnimation.AaOpacity(this.LabRamTotal, -this.LabRamTotal.Opacity, 100, 0, null, false),
						ModAnimation.AaOpacity(this.LabRamUsedTitle, 0.7 - this.LabRamUsedTitle.Opacity, 100, 0, null, false)
					}, "VersionSetup Ram TextLeft", false);
					break;
				case 2:
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaOpacity(this.LabRamUsed, 1.0 - this.LabRamUsed.Opacity, 100, 0, null, false),
						ModAnimation.AaOpacity(this.LabRamTotal, 1.0 - this.LabRamTotal.Opacity, 100, 0, null, false),
						ModAnimation.AaOpacity(this.LabRamUsedTitle, 0.7 - this.LabRamUsedTitle.Opacity, 100, 0, null, false)
					}, "VersionSetup Ram TextLeft", false);
					break;
				}
			}
			int num2;
			if (actualWidth2 >= actualWidth3 + 2.0 + actualWidth && actualWidth2 >= actualWidth6 + 2.0 + actualWidth)
			{
				num2 = 1;
			}
			else
			{
				num2 = 0;
			}
			if (num2 == 0)
			{
				if (ModAnimation.CalcParser() == 0 && (this.m_VisitorConfig != num2 || ModAnimation.AniIsRun("VersionSetup Ram TextRight")))
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaX(this.LabRamGame, actualWidth2 - actualWidth3 - this.LabRamGame.Margin.Left, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false),
						ModAnimation.AaX(this.LabRamGameTitle, actualWidth2 - actualWidth6 - this.LabRamGameTitle.Margin.Left, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false)
					}, "VersionSetup Ram TextRight", false);
				}
				else
				{
					this.LabRamGame.Margin = new Thickness(actualWidth2 - actualWidth3, 3.0, 0.0, 0.0);
					this.LabRamGameTitle.Margin = new Thickness(actualWidth2 - actualWidth6, 0.0, 0.0, 5.0);
				}
			}
			else if (ModAnimation.CalcParser() == 0 && (this.m_VisitorConfig != num2 || ModAnimation.AniIsRun("VersionSetup Ram TextRight")))
			{
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaX(this.LabRamGame, 2.0 + actualWidth - this.LabRamGame.Margin.Left, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false),
					ModAnimation.AaX(this.LabRamGameTitle, 2.0 + actualWidth - this.LabRamGameTitle.Margin.Left, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false)
				}, "VersionSetup Ram TextRight", false);
			}
			else
			{
				this.LabRamGame.Margin = new Thickness(2.0 + actualWidth, 3.0, 0.0, 0.0);
				this.LabRamGameTitle.Margin = new Thickness(2.0 + actualWidth, 0.0, 0.0, 5.0);
			}
			this.m_VisitorConfig = num2;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00051E00 File Offset: 0x00050000
		public static double GetRam(ModMinecraft.McVersion Version, bool? nullable_0 = null)
		{
			double result;
			if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("VersionRamType", Version), 2, false))
			{
				result = PageSetupLaunch.GetRam(Version, true, nullable_0);
			}
			else
			{
				double num7;
				if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("VersionRamType", Version), 0, false))
				{
					double num = Math.Round(MyWpfExtension.ManageParser().Info.AvailablePhysicalMemory / 1024.0 / 1024.0 / 1024.0 * 10.0) / 10.0;
					if (Version != null && !Version.importerMap)
					{
						Version.Load();
					}
					double val;
					double num3;
					double num4;
					double num5;
					if (Version != null && Version.RunThread())
					{
						DirectoryInfo directoryInfo = new DirectoryInfo(Version.ChangeMapper() + "mods\\");
						int num2 = directoryInfo.Exists ? directoryInfo.GetFiles().Length : 0;
						val = 0.5 + (double)num2 / 150.0;
						num3 = 1.5 + (double)num2 / 90.0;
						num4 = 2.7 + (double)num2 / 50.0;
						num5 = 4.5 + (double)num2 / 25.0;
					}
					else if (Version != null && Version.Version._StatusMap)
					{
						val = 0.5;
						num3 = 1.5;
						num4 = 3.0;
						num5 = 5.0;
					}
					else
					{
						val = 0.5;
						num3 = 1.5;
						num4 = 2.5;
						num5 = 4.0;
					}
					double num6 = num3;
					num7 += Math.Min(num, num6);
					num -= num6;
					if (num >= 0.1)
					{
						num6 = num4 - num3;
						num7 += Math.Min(num * 0.7, num6);
						num -= num6 / 0.7;
						if (num >= 0.1)
						{
							num6 = num5 - num4;
							num7 += Math.Min(num * 0.4, num6);
							num -= num6 / 0.4;
							if (num >= 0.1)
							{
								num6 = num5;
								num7 += Math.Min(num * 0.15, num6);
								num -= num6 / 0.15;
							}
						}
					}
					num7 = Math.Round(Math.Max(num7, val), 1);
				}
				else
				{
					int num8 = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("VersionRamCustom", Version));
					if (num8 <= 12)
					{
						num7 = (double)num8 * 0.1 + 0.3;
					}
					else if (num8 <= 25)
					{
						num7 = (double)(checked(num8 - 12)) * 0.5 + 1.5;
					}
					else if (num8 <= 33)
					{
						num7 = (double)(checked((num8 - 25) * 1 + 8));
					}
					else
					{
						num7 = (double)(checked((num8 - 33) * 2 + 16));
					}
				}
				if (nullable_0 ?? (!ModJava.smethod_0(PageVersionLeft._InstanceConfig)))
				{
					num7 = Math.Min(1.0, num7);
				}
				result = Math.Min(32.0, num7);
			}
			return result;
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00052148 File Offset: 0x00050348
		private void ComboServerLogin_Changed()
		{
			if (ModAnimation.CalcParser() == 0)
			{
				this.ServerLogin(this.ComboServerLogin.SelectedIndex);
				if ((this.ComboServerLogin.SelectedIndex != 3 || Operators.CompareString(this.TextServerNide.ValidateResult, "", false) == 0) && (this.ComboServerLogin.SelectedIndex != 4 || Operators.CompareString(this.TextServerAuthServer.ValidateResult, "", false) == 0) && this.valueConfig != this.ComboServerLogin.SelectedIndex)
				{
					this.valueConfig = this.ComboServerLogin.SelectedIndex;
					PageVersionSetup.ComboChange(this.ComboServerLogin, null);
				}
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x000521EC File Offset: 0x000503EC
		public void ServerLogin(int Type)
		{
			if (this.LabServerNide != null)
			{
				this.LabServerNide.Visibility = ((Type == 3) ? Visibility.Visible : Visibility.Collapsed);
				this.TextServerNide.Visibility = ((Type == 3) ? Visibility.Visible : Visibility.Collapsed);
				this.PanServerNide.Visibility = ((Type == 3) ? Visibility.Visible : Visibility.Collapsed);
				this.LabServerAuthName.Visibility = ((Type == 4) ? Visibility.Visible : Visibility.Collapsed);
				this.TextServerAuthName.Visibility = ((Type == 4) ? Visibility.Visible : Visibility.Collapsed);
				this.LabServerAuthRegister.Visibility = ((Type == 4) ? Visibility.Visible : Visibility.Collapsed);
				this.TextServerAuthRegister.Visibility = ((Type == 4) ? Visibility.Visible : Visibility.Collapsed);
				this.LabServerAuthServer.Visibility = ((Type == 4) ? Visibility.Visible : Visibility.Collapsed);
				this.TextServerAuthServer.Visibility = ((Type == 4) ? Visibility.Visible : Visibility.Collapsed);
				this.BtnServerAuthLittle.Visibility = ((Type == 4) ? Visibility.Visible : Visibility.Collapsed);
				this.CardServer.TriggerForceResize();
			}
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0000824A File Offset: 0x0000644A
		private void BtnServerNideWeb_Click(object sender, EventArgs e)
		{
			ModBase.OpenWebsite("https://login.mc-user.com:233/server/intro");
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x000522D0 File Offset: 0x000504D0
		private void BtnServerAuthLittle_Click(object sender, EventArgs e)
		{
			if (Operators.CompareString(this.TextServerAuthServer.Text, "", false) == 0 || Operators.CompareString(this.TextServerAuthServer.Text, "https://littleskin.cn/api/yggdrasil", false) == 0 || ModMain.MyMsgBox("即将把第三方登录设置覆盖为 LittleSkin 登录。\r\n除非你是服主，或者服主要求你这样做，否则请不要继续。\r\n\r\n是否确实需要覆盖当前设置？", "设置覆盖确认", "继续", "取消", "", false, true, false, null, null, null) != 2)
			{
				this.TextServerAuthServer.Text = "https://littleskin.cn/api/yggdrasil";
				this.TextServerAuthRegister.Text = "https://littleskin.cn/auth/register";
				this.TextServerAuthName.Text = "LittleSkin 登录";
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00052364 File Offset: 0x00050564
		public void RefreshJavaComboBox()
		{
			if (this.ComboArgumentJava != null)
			{
				this.ComboArgumentJava.Items.Clear();
				this.ComboArgumentJava.Items.Add(new MyComboBoxItem
				{
					Content = "使用全局设置",
					Tag = "使用全局设置"
				});
				this.ComboArgumentJava.Items.Add(new MyComboBoxItem
				{
					Content = "自动选择合适的 Java",
					Tag = "自动选择"
				});
				MyComboBoxItem myComboBoxItem = null;
				string text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("VersionArgumentJavaSelect", PageVersionLeft._InstanceConfig));
				try
				{
					try
					{
						foreach (ModJava.JavaEntry javaEntry in ModJava.interceptor.Clone<ModJava.JavaEntry>().Sort((PageVersionSetup._Closure$__.$I22-0 == null) ? (PageVersionSetup._Closure$__.$I22-0 = ((ModJava.JavaEntry l, ModJava.JavaEntry r) => l.PostTests() < r.PostTests())) : PageVersionSetup._Closure$__.$I22-0))
						{
							MyComboBoxItem myComboBoxItem2 = new MyComboBoxItem
							{
								Content = javaEntry.ToString(),
								ToolTip = javaEntry._UtilsRepository,
								Tag = javaEntry
							};
							ToolTipService.SetHorizontalOffset(myComboBoxItem2, 400.0);
							this.ComboArgumentJava.Items.Add(myComboBoxItem2);
							if (Operators.CompareString(text, "", false) != 0 && Operators.CompareString(text, "使用全局设置", false) != 0 && Operators.CompareString(ModJava.JavaEntry.FromJson((JObject)ModBase.GetJson(text))._UtilsRepository, javaEntry._UtilsRepository, false) == 0)
							{
								myComboBoxItem = myComboBoxItem2;
							}
						}
					}
					finally
					{
						List<ModJava.JavaEntry>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
				catch (Exception ex)
				{
					ModBase.m_IdentifierRepository.Set("VersionArgumentJavaSelect", "使用全局设置", false, PageVersionLeft._InstanceConfig);
					ModBase.Log(ex, "更新版本设置 Java 下拉框失败", ModBase.LogLevel.Feedback, "出现错误");
				}
				if (myComboBoxItem == null && Enumerable.Any<ModJava.JavaEntry>(ModJava.interceptor))
				{
					if (Operators.CompareString(text, "", false) == 0)
					{
						myComboBoxItem = (MyComboBoxItem)this.ComboArgumentJava.Items[1];
					}
					else
					{
						myComboBoxItem = (MyComboBoxItem)this.ComboArgumentJava.Items[0];
					}
				}
				this.ComboArgumentJava.SelectedItem = myComboBoxItem;
				if (myComboBoxItem == null)
				{
					this.ComboArgumentJava.Items.Clear();
					this.ComboArgumentJava.Items.Add(new ComboBoxItem
					{
						Content = "未找到可用的 Java",
						IsSelected = true
					});
				}
				this.RefreshRam(true);
			}
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x000525E4 File Offset: 0x000507E4
		private void ComboArgumentJava_DropDownOpened(object sender, EventArgs e)
		{
			if (this.ComboArgumentJava.SelectedItem == null || Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(this.ComboArgumentJava.Items[0], null, "Content", new object[0], null, null, null), "未找到可用的 Java", false) || Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(this.ComboArgumentJava.Items[0], null, "Content", new object[0], null, null, null), "加载中……", false))
			{
				this.ComboArgumentJava.IsDropDownOpen = false;
			}
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00052670 File Offset: 0x00050870
		private void JavaSelectionUpdate()
		{
			if (ModAnimation.CalcParser() == 0 && this.ComboArgumentJava.SelectedItem != null && NewLateBinding.LateGet(this.ComboArgumentJava.SelectedItem, null, "Tag", new object[0], null, null, null) != null)
			{
				object objectValue = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(this.ComboArgumentJava.SelectedItem, null, "Tag", new object[0], null, null, null));
				if ("使用全局设置".Equals(RuntimeHelpers.GetObjectValue(objectValue)))
				{
					ModBase.m_IdentifierRepository.Set("VersionArgumentJavaSelect", "使用全局设置", false, PageVersionLeft._InstanceConfig);
					ModBase.Log("[Java] 修改版本 Java 选择设置：使用全局设置", ModBase.LogLevel.Normal, "出现错误");
				}
				else if ("自动选择".Equals(RuntimeHelpers.GetObjectValue(objectValue)))
				{
					ModBase.m_IdentifierRepository.Set("VersionArgumentJavaSelect", "", false, PageVersionLeft._InstanceConfig);
					ModBase.Log("[Java] 修改版本 Java 选择设置：自动选择", ModBase.LogLevel.Normal, "出现错误");
				}
				else
				{
					ModBase.m_IdentifierRepository.Set("VersionArgumentJavaSelect", ((JObject)NewLateBinding.LateGet(objectValue, null, "ToJson", new object[0], null, null, null)).ToString(0, new JsonConverter[0]), false, PageVersionLeft._InstanceConfig);
					ModBase.Log("[Java] 修改版本 Java 选择设置：" + objectValue.ToString(), ModBase.LogLevel.Normal, "出现错误");
				}
				this.RefreshRam(true);
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x000527BC File Offset: 0x000509BC
		private void ComboArgumentIndie_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ModAnimation.CalcParser() == 0 && !this._ObjectConfig && ModMain.MyMsgBox("调整版本隔离后，你可能得把游戏存档、Mod 等文件手动迁移到新的游戏文件夹中。\r\n如果修改后发现存档消失，把这项设置改回来就能恢复。\r\n如果你不会迁移存档，不建议修改这项设置！", "警告", "我知道我在做什么", "取消", "", true, true, false, null, null, null) == 2)
			{
				this._ObjectConfig = true;
				this.ComboArgumentIndie.SelectedItem = RuntimeHelpers.GetObjectValue(e.RemovedItems[0]);
				this._ObjectConfig = false;
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00008256 File Offset: 0x00006456
		private void TextAdvanceRun_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.CheckAdvanceRunWait.Visibility = ((Operators.CompareString(this.TextAdvanceRun.Text, "", false) == 0) ? Visibility.Collapsed : Visibility.Visible);
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0000827F File Offset: 0x0000647F
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00008287 File Offset: 0x00006487
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00008290 File Offset: 0x00006490
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x00008298 File Offset: 0x00006498
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x000082A1 File Offset: 0x000064A1
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x000082A9 File Offset: 0x000064A9
		internal virtual MyCard CardArgument { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x000082B2 File Offset: 0x000064B2
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x0005282C File Offset: 0x00050A2C
		internal virtual MyTextBox TextArgumentTitle
		{
			[CompilerGenerated]
			get
			{
				return this.globalConfig;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageVersionSetup._Closure$__.$IR42-2 == null) ? (PageVersionSetup._Closure$__.$IR42-2 = delegate(object sender, RoutedEventArgs e)
				{
					PageVersionSetup.TextBoxChange((MyTextBox)sender, e);
				}) : PageVersionSetup._Closure$__.$IR42-2;
				MyTextBox myTextBox = this.globalConfig;
				if (myTextBox != null)
				{
					myTextBox.DestroyReader(value2);
				}
				this.globalConfig = value;
				myTextBox = this.globalConfig;
				if (myTextBox != null)
				{
					myTextBox.SetupReader(value2);
				}
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x000082BA File Offset: 0x000064BA
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x00052888 File Offset: 0x00050A88
		internal virtual MyTextBox TextArgumentInfo
		{
			[CompilerGenerated]
			get
			{
				return this._ExceptionConfig;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageVersionSetup._Closure$__.$IR46-3 == null) ? (PageVersionSetup._Closure$__.$IR46-3 = delegate(object sender, RoutedEventArgs e)
				{
					PageVersionSetup.TextBoxChange((MyTextBox)sender, e);
				}) : PageVersionSetup._Closure$__.$IR46-3;
				MyTextBox exceptionConfig = this._ExceptionConfig;
				if (exceptionConfig != null)
				{
					exceptionConfig.DestroyReader(value2);
				}
				this._ExceptionConfig = value;
				exceptionConfig = this._ExceptionConfig;
				if (exceptionConfig != null)
				{
					exceptionConfig.SetupReader(value2);
				}
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x000082C2 File Offset: 0x000064C2
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x000528E4 File Offset: 0x00050AE4
		internal virtual MyComboBox ComboArgumentIndie
		{
			[CompilerGenerated]
			get
			{
				return this._UtilsConfig;
			}
			[CompilerGenerated]
			set
			{
				SelectionChangedEventHandler value2 = (PageVersionSetup._Closure$__.$IR50-4 == null) ? (PageVersionSetup._Closure$__.$IR50-4 = delegate(object sender, SelectionChangedEventArgs e)
				{
					PageVersionSetup.ComboChange((MyComboBox)sender, e);
				}) : PageVersionSetup._Closure$__.$IR50-4;
				SelectionChangedEventHandler value3 = new SelectionChangedEventHandler(this.ComboArgumentIndie_SelectionChanged);
				MyComboBox utilsConfig = this._UtilsConfig;
				if (utilsConfig != null)
				{
					utilsConfig.SelectionChanged -= value2;
					utilsConfig.SelectionChanged -= value3;
				}
				this._UtilsConfig = value;
				utilsConfig = this._UtilsConfig;
				if (utilsConfig != null)
				{
					utilsConfig.SelectionChanged += value2;
					utilsConfig.SelectionChanged += value3;
				}
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x000082CA File Offset: 0x000064CA
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x0005295C File Offset: 0x00050B5C
		internal virtual MyComboBox ComboArgumentJava
		{
			[CompilerGenerated]
			get
			{
				return this._ClassConfig;
			}
			[CompilerGenerated]
			set
			{
				EventHandler value2 = new EventHandler(this.ComboArgumentJava_DropDownOpened);
				SelectionChangedEventHandler value3 = delegate(object sender, SelectionChangedEventArgs e)
				{
					this.JavaSelectionUpdate();
				};
				MyComboBox classConfig = this._ClassConfig;
				if (classConfig != null)
				{
					classConfig.DropDownOpened -= value2;
					classConfig.SelectionChanged -= value3;
				}
				this._ClassConfig = value;
				classConfig = this._ClassConfig;
				if (classConfig != null)
				{
					classConfig.DropDownOpened += value2;
					classConfig.SelectionChanged += value3;
				}
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x000082D2 File Offset: 0x000064D2
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x000082DA File Offset: 0x000064DA
		internal virtual MyHint LabRamWarn { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x000082E3 File Offset: 0x000064E3
		// (set) Token: 0x06000C13 RID: 3091 RVA: 0x000529BC File Offset: 0x00050BBC
		internal virtual MyRadioBox RadioRamType2
		{
			[CompilerGenerated]
			get
			{
				return this.m_OrderConfig;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageVersionSetup._Closure$__.$IR62-6 == null) ? (PageVersionSetup._Closure$__.$IR62-6 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageVersionSetup.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageVersionSetup._Closure$__.$IR62-6;
				IMyRadio.CheckEventHandler value3 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.RefreshRam();
				};
				MyRadioBox orderConfig = this.m_OrderConfig;
				if (orderConfig != null)
				{
					orderConfig.Check -= value2;
					orderConfig.Check -= value3;
				}
				this.m_OrderConfig = value;
				orderConfig = this.m_OrderConfig;
				if (orderConfig != null)
				{
					orderConfig.Check += value2;
					orderConfig.Check += value3;
				}
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x000082EB File Offset: 0x000064EB
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x00052A34 File Offset: 0x00050C34
		internal virtual MyRadioBox RadioRamType0
		{
			[CompilerGenerated]
			get
			{
				return this.m_ProducerConfig;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageVersionSetup._Closure$__.$IR66-8 == null) ? (PageVersionSetup._Closure$__.$IR66-8 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageVersionSetup.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageVersionSetup._Closure$__.$IR66-8;
				IMyRadio.CheckEventHandler value3 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.RefreshRam();
				};
				MyRadioBox producerConfig = this.m_ProducerConfig;
				if (producerConfig != null)
				{
					producerConfig.Check -= value2;
					producerConfig.Check -= value3;
				}
				this.m_ProducerConfig = value;
				producerConfig = this.m_ProducerConfig;
				if (producerConfig != null)
				{
					producerConfig.Check += value2;
					producerConfig.Check += value3;
				}
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x000082F3 File Offset: 0x000064F3
		// (set) Token: 0x06000C17 RID: 3095 RVA: 0x00052AAC File Offset: 0x00050CAC
		internal virtual MyRadioBox RadioRamType1
		{
			[CompilerGenerated]
			get
			{
				return this._SchemaConfig;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageVersionSetup._Closure$__.$IR70-10 == null) ? (PageVersionSetup._Closure$__.$IR70-10 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageVersionSetup.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageVersionSetup._Closure$__.$IR70-10;
				IMyRadio.CheckEventHandler value3 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.RefreshRam();
				};
				MyRadioBox schemaConfig = this._SchemaConfig;
				if (schemaConfig != null)
				{
					schemaConfig.Check -= value2;
					schemaConfig.Check -= value3;
				}
				this._SchemaConfig = value;
				schemaConfig = this._SchemaConfig;
				if (schemaConfig != null)
				{
					schemaConfig.Check += value2;
					schemaConfig.Check += value3;
				}
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x000082FB File Offset: 0x000064FB
		// (set) Token: 0x06000C19 RID: 3097 RVA: 0x00052B24 File Offset: 0x00050D24
		internal virtual MySlider SliderRamCustom
		{
			[CompilerGenerated]
			get
			{
				return this.descriptorConfig;
			}
			[CompilerGenerated]
			set
			{
				MySlider.ChangeEventHandler obj = (PageVersionSetup._Closure$__.$IR74-12 == null) ? (PageVersionSetup._Closure$__.$IR74-12 = delegate(object a0, bool a1)
				{
					PageVersionSetup.SliderChange((MySlider)a0, a1);
				}) : PageVersionSetup._Closure$__.$IR74-12;
				MySlider.ChangeEventHandler obj2 = delegate(object a0, bool a1)
				{
					this.RefreshRam();
				};
				MySlider mySlider = this.descriptorConfig;
				if (mySlider != null)
				{
					mySlider.WriteTests(obj);
					mySlider.WriteTests(obj2);
				}
				this.descriptorConfig = value;
				mySlider = this.descriptorConfig;
				if (mySlider != null)
				{
					mySlider.FillTests(obj);
					mySlider.FillTests(obj2);
				}
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x00008303 File Offset: 0x00006503
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x00052B9C File Offset: 0x00050D9C
		internal virtual MyComboBox ComboRamOptimize
		{
			[CompilerGenerated]
			get
			{
				return this.m_PublisherConfig;
			}
			[CompilerGenerated]
			set
			{
				SelectionChangedEventHandler value2 = (PageVersionSetup._Closure$__.$IR78-14 == null) ? (PageVersionSetup._Closure$__.$IR78-14 = delegate(object sender, SelectionChangedEventArgs e)
				{
					PageVersionSetup.ComboChange((MyComboBox)sender, e);
				}) : PageVersionSetup._Closure$__.$IR78-14;
				MyComboBox publisherConfig = this.m_PublisherConfig;
				if (publisherConfig != null)
				{
					publisherConfig.SelectionChanged -= value2;
				}
				this.m_PublisherConfig = value;
				publisherConfig = this.m_PublisherConfig;
				if (publisherConfig != null)
				{
					publisherConfig.SelectionChanged += value2;
				}
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0000830B File Offset: 0x0000650B
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x00008313 File Offset: 0x00006513
		internal virtual Grid PanRamDisplay { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x0000831C File Offset: 0x0000651C
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x00008324 File Offset: 0x00006524
		internal virtual ColumnDefinition ColumnRamUsed { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0000832D File Offset: 0x0000652D
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x00008335 File Offset: 0x00006535
		internal virtual ColumnDefinition ColumnRamGame { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x0000833E File Offset: 0x0000653E
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x00008346 File Offset: 0x00006546
		internal virtual ColumnDefinition ColumnRamEmpty { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0000834F File Offset: 0x0000654F
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x00008357 File Offset: 0x00006557
		internal virtual Rectangle RectRamUsed { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x00008360 File Offset: 0x00006560
		// (set) Token: 0x06000C27 RID: 3111 RVA: 0x00052BF8 File Offset: 0x00050DF8
		internal virtual Rectangle RectRamGame
		{
			[CompilerGenerated]
			get
			{
				return this.fieldTests;
			}
			[CompilerGenerated]
			set
			{
				SizeChangedEventHandler value2 = delegate(object sender, SizeChangedEventArgs e)
				{
					this.RefreshRamText();
				};
				Rectangle rectangle = this.fieldTests;
				if (rectangle != null)
				{
					rectangle.SizeChanged -= value2;
				}
				this.fieldTests = value;
				rectangle = this.fieldTests;
				if (rectangle != null)
				{
					rectangle.SizeChanged += value2;
				}
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00008368 File Offset: 0x00006568
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x00052C3C File Offset: 0x00050E3C
		internal virtual Rectangle RectRamEmpty
		{
			[CompilerGenerated]
			get
			{
				return this.m_ReaderTests;
			}
			[CompilerGenerated]
			set
			{
				SizeChangedEventHandler value2 = delegate(object sender, SizeChangedEventArgs e)
				{
					this.RefreshRamText();
				};
				Rectangle readerTests = this.m_ReaderTests;
				if (readerTests != null)
				{
					readerTests.SizeChanged -= value2;
				}
				this.m_ReaderTests = value;
				readerTests = this.m_ReaderTests;
				if (readerTests != null)
				{
					readerTests.SizeChanged += value2;
				}
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00008370 File Offset: 0x00006570
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x00008378 File Offset: 0x00006578
		internal virtual TextBlock LabRamUsedTitle { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x00008381 File Offset: 0x00006581
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x00008389 File Offset: 0x00006589
		internal virtual TextBlock LabRamGameTitle { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x00008392 File Offset: 0x00006592
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x0000839A File Offset: 0x0000659A
		internal virtual TextBlock LabRamUsed { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x000083A3 File Offset: 0x000065A3
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x000083AB File Offset: 0x000065AB
		internal virtual TextBlock LabRamTotal { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x000083B4 File Offset: 0x000065B4
		// (set) Token: 0x06000C33 RID: 3123 RVA: 0x00052C80 File Offset: 0x00050E80
		internal virtual TextBlock LabRamGame
		{
			[CompilerGenerated]
			get
			{
				return this.threadTests;
			}
			[CompilerGenerated]
			set
			{
				SizeChangedEventHandler value2 = delegate(object sender, SizeChangedEventArgs e)
				{
					this.RefreshRamText();
				};
				TextBlock textBlock = this.threadTests;
				if (textBlock != null)
				{
					textBlock.SizeChanged -= value2;
				}
				this.threadTests = value;
				textBlock = this.threadTests;
				if (textBlock != null)
				{
					textBlock.SizeChanged += value2;
				}
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x000083BC File Offset: 0x000065BC
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x000083C4 File Offset: 0x000065C4
		internal virtual MyCard CardServer { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x000083CD File Offset: 0x000065CD
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x00052CC4 File Offset: 0x00050EC4
		internal virtual MyComboBox ComboServerLogin
		{
			[CompilerGenerated]
			get
			{
				return this.composerTests;
			}
			[CompilerGenerated]
			set
			{
				SelectionChangedEventHandler value2 = delegate(object sender, SelectionChangedEventArgs e)
				{
					this.ComboServerLogin_Changed();
				};
				MyComboBox myComboBox = this.composerTests;
				if (myComboBox != null)
				{
					myComboBox.SelectionChanged -= value2;
				}
				this.composerTests = value;
				myComboBox = this.composerTests;
				if (myComboBox != null)
				{
					myComboBox.SelectionChanged += value2;
				}
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x000083D5 File Offset: 0x000065D5
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x000083DD File Offset: 0x000065DD
		internal virtual TextBlock LabServerNide { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x000083E6 File Offset: 0x000065E6
		// (set) Token: 0x06000C3B RID: 3131 RVA: 0x00052D08 File Offset: 0x00050F08
		internal virtual MyTextBox TextServerNide
		{
			[CompilerGenerated]
			get
			{
				return this._RepositoryTests;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageVersionSetup._Closure$__.$IR142-19 == null) ? (PageVersionSetup._Closure$__.$IR142-19 = delegate(object sender, RoutedEventArgs e)
				{
					PageVersionSetup.TextBoxChange((MyTextBox)sender, e);
				}) : PageVersionSetup._Closure$__.$IR142-19;
				RoutedEventHandler value3 = delegate(object sender, RoutedEventArgs e)
				{
					this.ComboServerLogin_Changed();
				};
				MyTextBox repositoryTests = this._RepositoryTests;
				if (repositoryTests != null)
				{
					repositoryTests.DestroyReader(value2);
					repositoryTests.DestroyReader(value3);
				}
				this._RepositoryTests = value;
				repositoryTests = this._RepositoryTests;
				if (repositoryTests != null)
				{
					repositoryTests.SetupReader(value2);
					repositoryTests.SetupReader(value3);
				}
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x000083EE File Offset: 0x000065EE
		// (set) Token: 0x06000C3D RID: 3133 RVA: 0x000083F6 File Offset: 0x000065F6
		internal virtual TextBlock LabServerAuthServer { get; set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x000083FF File Offset: 0x000065FF
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x00052D80 File Offset: 0x00050F80
		internal virtual MyTextBox TextServerAuthServer
		{
			[CompilerGenerated]
			get
			{
				return this.mapTests;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageVersionSetup._Closure$__.$IR150-21 == null) ? (PageVersionSetup._Closure$__.$IR150-21 = delegate(object sender, RoutedEventArgs e)
				{
					PageVersionSetup.TextBoxChange((MyTextBox)sender, e);
				}) : PageVersionSetup._Closure$__.$IR150-21;
				RoutedEventHandler value3 = delegate(object sender, RoutedEventArgs e)
				{
					this.ComboServerLogin_Changed();
				};
				MyTextBox myTextBox = this.mapTests;
				if (myTextBox != null)
				{
					myTextBox.DestroyReader(value2);
					myTextBox.DestroyReader(value3);
				}
				this.mapTests = value;
				myTextBox = this.mapTests;
				if (myTextBox != null)
				{
					myTextBox.SetupReader(value2);
					myTextBox.SetupReader(value3);
				}
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00008407 File Offset: 0x00006607
		// (set) Token: 0x06000C41 RID: 3137 RVA: 0x0000840F File Offset: 0x0000660F
		internal virtual TextBlock LabServerAuthRegister { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00008418 File Offset: 0x00006618
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x00052DF8 File Offset: 0x00050FF8
		internal virtual MyTextBox TextServerAuthRegister
		{
			[CompilerGenerated]
			get
			{
				return this._ContextTests;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageVersionSetup._Closure$__.$IR158-23 == null) ? (PageVersionSetup._Closure$__.$IR158-23 = delegate(object sender, RoutedEventArgs e)
				{
					PageVersionSetup.TextBoxChange((MyTextBox)sender, e);
				}) : PageVersionSetup._Closure$__.$IR158-23;
				RoutedEventHandler value3 = delegate(object sender, RoutedEventArgs e)
				{
					this.ComboServerLogin_Changed();
				};
				MyTextBox contextTests = this._ContextTests;
				if (contextTests != null)
				{
					contextTests.DestroyReader(value2);
					contextTests.DestroyReader(value3);
				}
				this._ContextTests = value;
				contextTests = this._ContextTests;
				if (contextTests != null)
				{
					contextTests.SetupReader(value2);
					contextTests.SetupReader(value3);
				}
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00008420 File Offset: 0x00006620
		// (set) Token: 0x06000C45 RID: 3141 RVA: 0x00008428 File Offset: 0x00006628
		internal virtual TextBlock LabServerAuthName { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00008431 File Offset: 0x00006631
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x00052E70 File Offset: 0x00051070
		internal virtual MyTextBox TextServerAuthName
		{
			[CompilerGenerated]
			get
			{
				return this._MockTests;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageVersionSetup._Closure$__.$IR166-25 == null) ? (PageVersionSetup._Closure$__.$IR166-25 = delegate(object sender, RoutedEventArgs e)
				{
					PageVersionSetup.TextBoxChange((MyTextBox)sender, e);
				}) : PageVersionSetup._Closure$__.$IR166-25;
				MyTextBox mockTests = this._MockTests;
				if (mockTests != null)
				{
					mockTests.DestroyReader(value2);
				}
				this._MockTests = value;
				mockTests = this._MockTests;
				if (mockTests != null)
				{
					mockTests.SetupReader(value2);
				}
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x00008439 File Offset: 0x00006639
		// (set) Token: 0x06000C49 RID: 3145 RVA: 0x00052ECC File Offset: 0x000510CC
		internal virtual MyTextBox TextServerEnter
		{
			[CompilerGenerated]
			get
			{
				return this.m_RequestTests;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageVersionSetup._Closure$__.$IR170-26 == null) ? (PageVersionSetup._Closure$__.$IR170-26 = delegate(object sender, RoutedEventArgs e)
				{
					PageVersionSetup.TextBoxChange((MyTextBox)sender, e);
				}) : PageVersionSetup._Closure$__.$IR170-26;
				MyTextBox requestTests = this.m_RequestTests;
				if (requestTests != null)
				{
					requestTests.DestroyReader(value2);
				}
				this.m_RequestTests = value;
				requestTests = this.m_RequestTests;
				if (requestTests != null)
				{
					requestTests.SetupReader(value2);
				}
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x00008441 File Offset: 0x00006641
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x00008449 File Offset: 0x00006649
		internal virtual Grid PanServerNide { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x00008452 File Offset: 0x00006652
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x00052F28 File Offset: 0x00051128
		internal virtual MyButton BtnServerNideWeb
		{
			[CompilerGenerated]
			get
			{
				return this._HelperTests;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnServerNideWeb_Click);
				MyButton helperTests = this._HelperTests;
				if (helperTests != null)
				{
					helperTests.Click -= value2;
				}
				this._HelperTests = value;
				helperTests = this._HelperTests;
				if (helperTests != null)
				{
					helperTests.Click += value2;
				}
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0000845A File Offset: 0x0000665A
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x00052F6C File Offset: 0x0005116C
		internal virtual MyButton BtnServerAuthLittle
		{
			[CompilerGenerated]
			get
			{
				return this.m_IssuerTests;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnServerAuthLittle_Click);
				MyButton issuerTests = this.m_IssuerTests;
				if (issuerTests != null)
				{
					issuerTests.Click -= value2;
				}
				this.m_IssuerTests = value;
				issuerTests = this.m_IssuerTests;
				if (issuerTests != null)
				{
					issuerTests.Click += value2;
				}
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x00008462 File Offset: 0x00006662
		// (set) Token: 0x06000C51 RID: 3153 RVA: 0x0000846A File Offset: 0x0000666A
		internal virtual MyCard CardAdvance { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x00008473 File Offset: 0x00006673
		// (set) Token: 0x06000C53 RID: 3155 RVA: 0x00052FB0 File Offset: 0x000511B0
		internal virtual MyTextBox TextAdvanceJvm
		{
			[CompilerGenerated]
			get
			{
				return this.interpreterTests;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageVersionSetup._Closure$__.$IR190-27 == null) ? (PageVersionSetup._Closure$__.$IR190-27 = delegate(object sender, RoutedEventArgs e)
				{
					PageVersionSetup.TextBoxChange((MyTextBox)sender, e);
				}) : PageVersionSetup._Closure$__.$IR190-27;
				MyTextBox myTextBox = this.interpreterTests;
				if (myTextBox != null)
				{
					myTextBox.DestroyReader(value2);
				}
				this.interpreterTests = value;
				myTextBox = this.interpreterTests;
				if (myTextBox != null)
				{
					myTextBox.SetupReader(value2);
				}
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x0000847B File Offset: 0x0000667B
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x0005300C File Offset: 0x0005120C
		internal virtual MyTextBox TextAdvanceGame
		{
			[CompilerGenerated]
			get
			{
				return this.m_SerializerTests;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageVersionSetup._Closure$__.$IR194-28 == null) ? (PageVersionSetup._Closure$__.$IR194-28 = delegate(object sender, RoutedEventArgs e)
				{
					PageVersionSetup.TextBoxChange((MyTextBox)sender, e);
				}) : PageVersionSetup._Closure$__.$IR194-28;
				MyTextBox serializerTests = this.m_SerializerTests;
				if (serializerTests != null)
				{
					serializerTests.DestroyReader(value2);
				}
				this.m_SerializerTests = value;
				serializerTests = this.m_SerializerTests;
				if (serializerTests != null)
				{
					serializerTests.SetupReader(value2);
				}
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00008483 File Offset: 0x00006683
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x00053068 File Offset: 0x00051268
		internal virtual MyTextBox TextAdvanceRun
		{
			[CompilerGenerated]
			get
			{
				return this._WatcherTests;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageVersionSetup._Closure$__.$IR198-29 == null) ? (PageVersionSetup._Closure$__.$IR198-29 = delegate(object sender, RoutedEventArgs e)
				{
					PageVersionSetup.TextBoxChange((MyTextBox)sender, e);
				}) : PageVersionSetup._Closure$__.$IR198-29;
				TextChangedEventHandler value3 = new TextChangedEventHandler(this.TextAdvanceRun_TextChanged);
				MyTextBox watcherTests = this._WatcherTests;
				if (watcherTests != null)
				{
					watcherTests.DestroyReader(value2);
					watcherTests.TextChanged -= value3;
				}
				this._WatcherTests = value;
				watcherTests = this._WatcherTests;
				if (watcherTests != null)
				{
					watcherTests.SetupReader(value2);
					watcherTests.TextChanged += value3;
				}
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x0000848B File Offset: 0x0000668B
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x000530E0 File Offset: 0x000512E0
		internal virtual MyCheckBox CheckAdvanceRunWait
		{
			[CompilerGenerated]
			get
			{
				return this._IdentifierTests;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageVersionSetup._Closure$__.$IR202-30 == null) ? (PageVersionSetup._Closure$__.$IR202-30 = delegate(object a0, bool a1)
				{
					PageVersionSetup.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageVersionSetup._Closure$__.$IR202-30;
				MyCheckBox identifierTests = this._IdentifierTests;
				if (identifierTests != null)
				{
					identifierTests.PublishConfig(obj);
				}
				this._IdentifierTests = value;
				identifierTests = this._IdentifierTests;
				if (identifierTests != null)
				{
					identifierTests.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00008493 File Offset: 0x00006693
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x0005313C File Offset: 0x0005133C
		internal virtual MyCheckBox CheckAdvanceJava
		{
			[CompilerGenerated]
			get
			{
				return this.systemTests;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageVersionSetup._Closure$__.$IR206-31 == null) ? (PageVersionSetup._Closure$__.$IR206-31 = delegate(object a0, bool a1)
				{
					PageVersionSetup.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageVersionSetup._Closure$__.$IR206-31;
				MyCheckBox myCheckBox = this.systemTests;
				if (myCheckBox != null)
				{
					myCheckBox.PublishConfig(obj);
				}
				this.systemTests = value;
				myCheckBox = this.systemTests;
				if (myCheckBox != null)
				{
					myCheckBox.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x0000849B File Offset: 0x0000669B
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x00053198 File Offset: 0x00051398
		internal virtual MyCheckBox MyCheckBox_0
		{
			[CompilerGenerated]
			get
			{
				return this.paramTests;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageVersionSetup._Closure$__.$IR210-32 == null) ? (PageVersionSetup._Closure$__.$IR210-32 = delegate(object a0, bool a1)
				{
					PageVersionSetup.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageVersionSetup._Closure$__.$IR210-32;
				MyCheckBox myCheckBox = this.paramTests;
				if (myCheckBox != null)
				{
					myCheckBox.PublishConfig(obj);
				}
				this.paramTests = value;
				myCheckBox = this.paramTests;
				if (myCheckBox != null)
				{
					myCheckBox.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x000531F4 File Offset: 0x000513F4
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._TagTests)
			{
				this._TagTests = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageversion/pageversionsetup.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00053224 File Offset: 0x00051424
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
				this.CardArgument = (MyCard)target;
				return;
			}
			if (connectionId == 4)
			{
				this.TextArgumentTitle = (MyTextBox)target;
				return;
			}
			if (connectionId == 5)
			{
				this.TextArgumentInfo = (MyTextBox)target;
				return;
			}
			if (connectionId == 6)
			{
				this.ComboArgumentIndie = (MyComboBox)target;
				return;
			}
			if (connectionId == 7)
			{
				this.ComboArgumentJava = (MyComboBox)target;
				return;
			}
			if (connectionId == 8)
			{
				this.LabRamWarn = (MyHint)target;
				return;
			}
			if (connectionId == 9)
			{
				this.RadioRamType2 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 10)
			{
				this.RadioRamType0 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 11)
			{
				this.RadioRamType1 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 12)
			{
				this.SliderRamCustom = (MySlider)target;
				return;
			}
			if (connectionId == 13)
			{
				this.ComboRamOptimize = (MyComboBox)target;
				return;
			}
			if (connectionId == 14)
			{
				this.PanRamDisplay = (Grid)target;
				return;
			}
			if (connectionId == 15)
			{
				this.ColumnRamUsed = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 16)
			{
				this.ColumnRamGame = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 17)
			{
				this.ColumnRamEmpty = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 18)
			{
				this.RectRamUsed = (Rectangle)target;
				return;
			}
			if (connectionId == 19)
			{
				this.RectRamGame = (Rectangle)target;
				return;
			}
			if (connectionId == 20)
			{
				this.RectRamEmpty = (Rectangle)target;
				return;
			}
			if (connectionId == 21)
			{
				this.LabRamUsedTitle = (TextBlock)target;
				return;
			}
			if (connectionId == 22)
			{
				this.LabRamGameTitle = (TextBlock)target;
				return;
			}
			if (connectionId == 23)
			{
				this.LabRamUsed = (TextBlock)target;
				return;
			}
			if (connectionId == 24)
			{
				this.LabRamTotal = (TextBlock)target;
				return;
			}
			if (connectionId == 25)
			{
				this.LabRamGame = (TextBlock)target;
				return;
			}
			if (connectionId == 26)
			{
				this.CardServer = (MyCard)target;
				return;
			}
			if (connectionId == 27)
			{
				this.ComboServerLogin = (MyComboBox)target;
				return;
			}
			if (connectionId == 28)
			{
				this.LabServerNide = (TextBlock)target;
				return;
			}
			if (connectionId == 29)
			{
				this.TextServerNide = (MyTextBox)target;
				return;
			}
			if (connectionId == 30)
			{
				this.LabServerAuthServer = (TextBlock)target;
				return;
			}
			if (connectionId == 31)
			{
				this.TextServerAuthServer = (MyTextBox)target;
				return;
			}
			if (connectionId == 32)
			{
				this.LabServerAuthRegister = (TextBlock)target;
				return;
			}
			if (connectionId == 33)
			{
				this.TextServerAuthRegister = (MyTextBox)target;
				return;
			}
			if (connectionId == 34)
			{
				this.LabServerAuthName = (TextBlock)target;
				return;
			}
			if (connectionId == 35)
			{
				this.TextServerAuthName = (MyTextBox)target;
				return;
			}
			if (connectionId == 36)
			{
				this.TextServerEnter = (MyTextBox)target;
				return;
			}
			if (connectionId == 37)
			{
				this.PanServerNide = (Grid)target;
				return;
			}
			if (connectionId == 38)
			{
				this.BtnServerNideWeb = (MyButton)target;
				return;
			}
			if (connectionId == 39)
			{
				this.BtnServerAuthLittle = (MyButton)target;
				return;
			}
			if (connectionId == 40)
			{
				this.CardAdvance = (MyCard)target;
				return;
			}
			if (connectionId == 41)
			{
				this.TextAdvanceJvm = (MyTextBox)target;
				return;
			}
			if (connectionId == 42)
			{
				this.TextAdvanceGame = (MyTextBox)target;
				return;
			}
			if (connectionId == 43)
			{
				this.TextAdvanceRun = (MyTextBox)target;
				return;
			}
			if (connectionId == 44)
			{
				this.CheckAdvanceRunWait = (MyCheckBox)target;
				return;
			}
			if (connectionId == 45)
			{
				this.CheckAdvanceJava = (MyCheckBox)target;
				return;
			}
			if (connectionId == 46)
			{
				this.MyCheckBox_0 = (MyCheckBox)target;
				return;
			}
			this._TagTests = true;
		}

		// Token: 0x040005FE RID: 1534
		private bool listenerConfig;

		// Token: 0x040005FF RID: 1535
		private int collectionConfig;

		// Token: 0x04000600 RID: 1536
		private int m_VisitorConfig;

		// Token: 0x04000601 RID: 1537
		private int valueConfig;

		// Token: 0x04000602 RID: 1538
		private bool _ObjectConfig;

		// Token: 0x04000603 RID: 1539
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyScrollViewer bridgeConfig;

		// Token: 0x04000604 RID: 1540
		[CompilerGenerated]
		[AccessedThroughProperty("PanMain")]
		private StackPanel m_ItemConfig;

		// Token: 0x04000605 RID: 1541
		[CompilerGenerated]
		[AccessedThroughProperty("CardArgument")]
		private MyCard m_ReponseConfig;

		// Token: 0x04000606 RID: 1542
		[AccessedThroughProperty("TextArgumentTitle")]
		[CompilerGenerated]
		private MyTextBox globalConfig;

		// Token: 0x04000607 RID: 1543
		[AccessedThroughProperty("TextArgumentInfo")]
		[CompilerGenerated]
		private MyTextBox _ExceptionConfig;

		// Token: 0x04000608 RID: 1544
		[AccessedThroughProperty("ComboArgumentIndie")]
		[CompilerGenerated]
		private MyComboBox _UtilsConfig;

		// Token: 0x04000609 RID: 1545
		[AccessedThroughProperty("ComboArgumentJava")]
		[CompilerGenerated]
		private MyComboBox _ClassConfig;

		// Token: 0x0400060A RID: 1546
		[CompilerGenerated]
		[AccessedThroughProperty("LabRamWarn")]
		private MyHint m_PolicyConfig;

		// Token: 0x0400060B RID: 1547
		[CompilerGenerated]
		[AccessedThroughProperty("RadioRamType2")]
		private MyRadioBox m_OrderConfig;

		// Token: 0x0400060C RID: 1548
		[CompilerGenerated]
		[AccessedThroughProperty("RadioRamType0")]
		private MyRadioBox m_ProducerConfig;

		// Token: 0x0400060D RID: 1549
		[AccessedThroughProperty("RadioRamType1")]
		[CompilerGenerated]
		private MyRadioBox _SchemaConfig;

		// Token: 0x0400060E RID: 1550
		[AccessedThroughProperty("SliderRamCustom")]
		[CompilerGenerated]
		private MySlider descriptorConfig;

		// Token: 0x0400060F RID: 1551
		[CompilerGenerated]
		[AccessedThroughProperty("ComboRamOptimize")]
		private MyComboBox m_PublisherConfig;

		// Token: 0x04000610 RID: 1552
		[AccessedThroughProperty("PanRamDisplay")]
		[CompilerGenerated]
		private Grid m_DefinitionConfig;

		// Token: 0x04000611 RID: 1553
		[AccessedThroughProperty("ColumnRamUsed")]
		[CompilerGenerated]
		private ColumnDefinition m_StrategyConfig;

		// Token: 0x04000612 RID: 1554
		[CompilerGenerated]
		[AccessedThroughProperty("ColumnRamGame")]
		private ColumnDefinition procConfig;

		// Token: 0x04000613 RID: 1555
		[CompilerGenerated]
		[AccessedThroughProperty("ColumnRamEmpty")]
		private ColumnDefinition m_ParserTests;

		// Token: 0x04000614 RID: 1556
		[AccessedThroughProperty("RectRamUsed")]
		[CompilerGenerated]
		private Rectangle _BroadcasterTests;

		// Token: 0x04000615 RID: 1557
		[AccessedThroughProperty("RectRamGame")]
		[CompilerGenerated]
		private Rectangle fieldTests;

		// Token: 0x04000616 RID: 1558
		[AccessedThroughProperty("RectRamEmpty")]
		[CompilerGenerated]
		private Rectangle m_ReaderTests;

		// Token: 0x04000617 RID: 1559
		[CompilerGenerated]
		[AccessedThroughProperty("LabRamUsedTitle")]
		private TextBlock clientTests;

		// Token: 0x04000618 RID: 1560
		[AccessedThroughProperty("LabRamGameTitle")]
		[CompilerGenerated]
		private TextBlock m_ConfigTests;

		// Token: 0x04000619 RID: 1561
		[AccessedThroughProperty("LabRamUsed")]
		[CompilerGenerated]
		private TextBlock _TestsTests;

		// Token: 0x0400061A RID: 1562
		[CompilerGenerated]
		[AccessedThroughProperty("LabRamTotal")]
		private TextBlock mapperTests;

		// Token: 0x0400061B RID: 1563
		[AccessedThroughProperty("LabRamGame")]
		[CompilerGenerated]
		private TextBlock threadTests;

		// Token: 0x0400061C RID: 1564
		[AccessedThroughProperty("CardServer")]
		[CompilerGenerated]
		private MyCard propertyTests;

		// Token: 0x0400061D RID: 1565
		[AccessedThroughProperty("ComboServerLogin")]
		[CompilerGenerated]
		private MyComboBox composerTests;

		// Token: 0x0400061E RID: 1566
		[AccessedThroughProperty("LabServerNide")]
		[CompilerGenerated]
		private TextBlock iteratorTests;

		// Token: 0x0400061F RID: 1567
		[CompilerGenerated]
		[AccessedThroughProperty("TextServerNide")]
		private MyTextBox _RepositoryTests;

		// Token: 0x04000620 RID: 1568
		[AccessedThroughProperty("LabServerAuthServer")]
		[CompilerGenerated]
		private TextBlock _TestTests;

		// Token: 0x04000621 RID: 1569
		[AccessedThroughProperty("TextServerAuthServer")]
		[CompilerGenerated]
		private MyTextBox mapTests;

		// Token: 0x04000622 RID: 1570
		[AccessedThroughProperty("LabServerAuthRegister")]
		[CompilerGenerated]
		private TextBlock _ErrorTests;

		// Token: 0x04000623 RID: 1571
		[AccessedThroughProperty("TextServerAuthRegister")]
		[CompilerGenerated]
		private MyTextBox _ContextTests;

		// Token: 0x04000624 RID: 1572
		[CompilerGenerated]
		[AccessedThroughProperty("LabServerAuthName")]
		private TextBlock specificationTests;

		// Token: 0x04000625 RID: 1573
		[CompilerGenerated]
		[AccessedThroughProperty("TextServerAuthName")]
		private MyTextBox _MockTests;

		// Token: 0x04000626 RID: 1574
		[CompilerGenerated]
		[AccessedThroughProperty("TextServerEnter")]
		private MyTextBox m_RequestTests;

		// Token: 0x04000627 RID: 1575
		[AccessedThroughProperty("PanServerNide")]
		[CompilerGenerated]
		private Grid _DicTests;

		// Token: 0x04000628 RID: 1576
		[AccessedThroughProperty("BtnServerNideWeb")]
		[CompilerGenerated]
		private MyButton _HelperTests;

		// Token: 0x04000629 RID: 1577
		[AccessedThroughProperty("BtnServerAuthLittle")]
		[CompilerGenerated]
		private MyButton m_IssuerTests;

		// Token: 0x0400062A RID: 1578
		[CompilerGenerated]
		[AccessedThroughProperty("CardAdvance")]
		private MyCard indexerTests;

		// Token: 0x0400062B RID: 1579
		[AccessedThroughProperty("TextAdvanceJvm")]
		[CompilerGenerated]
		private MyTextBox interpreterTests;

		// Token: 0x0400062C RID: 1580
		[CompilerGenerated]
		[AccessedThroughProperty("TextAdvanceGame")]
		private MyTextBox m_SerializerTests;

		// Token: 0x0400062D RID: 1581
		[AccessedThroughProperty("TextAdvanceRun")]
		[CompilerGenerated]
		private MyTextBox _WatcherTests;

		// Token: 0x0400062E RID: 1582
		[CompilerGenerated]
		[AccessedThroughProperty("CheckAdvanceRunWait")]
		private MyCheckBox _IdentifierTests;

		// Token: 0x0400062F RID: 1583
		[AccessedThroughProperty("CheckAdvanceJava")]
		[CompilerGenerated]
		private MyCheckBox systemTests;

		// Token: 0x04000630 RID: 1584
		[AccessedThroughProperty("CheckAdvanceAssetsV2")]
		[CompilerGenerated]
		private MyCheckBox paramTests;

		// Token: 0x04000631 RID: 1585
		private bool _TagTests;
	}
}
