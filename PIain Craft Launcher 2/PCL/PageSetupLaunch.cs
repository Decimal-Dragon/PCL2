using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
	// Token: 0x020001B2 RID: 434
	[DesignerGenerated]
	public class PageSetupLaunch : MyPageRight, IComponentConnector
	{
		// Token: 0x060011FE RID: 4606 RVA: 0x0000ACBC File Offset: 0x00008EBC
		public PageSetupLaunch()
		{
			base.Loaded += this.PageSetupLaunch_Loaded;
			this.attrThread = false;
			this.candidateThread = 2;
			this._AdvisorThread = 1;
			this._AccountThread = false;
			this.InitializeComponent();
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0008106C File Offset: 0x0007F26C
		private void PageSetupLaunch_Loaded(object sender, RoutedEventArgs e)
		{
			this.PanBack.ScrollToHome();
			this.RefreshRam(false);
			checked
			{
				if (!this.attrThread)
				{
					this.attrThread = true;
					ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
					this.Reload();
					ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
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

		// Token: 0x06001200 RID: 4608 RVA: 0x000810E4 File Offset: 0x0007F2E4
		public void Reload()
		{
			try
			{
				((MyRadioBox)base.FindName(Conversions.ToString(Operators.ConcatenateObject("RadioSkinType", ModBase.m_IdentifierRepository.Load("LaunchSkinType", false, null))))).Checked = true;
				this.TextSkinID.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchSkinID", null));
				this.TextArgumentTitle.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchArgumentTitle", null));
				this.TextArgumentInfo.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchArgumentInfo", null));
				this.ComboArgumentIndie.SelectedIndex = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("LaunchArgumentIndie", null));
				this.ComboArgumentVisibie.SelectedIndex = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("LaunchArgumentVisible", null));
				this.ComboArgumentPriority.SelectedIndex = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("LaunchArgumentPriority", null));
				this.ComboArgumentWindowType.SelectedIndex = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("LaunchArgumentWindowType", null));
				this.TextArgumentWindowWidth.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchArgumentWindowWidth", null));
				this.TextArgumentWindowHeight.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchArgumentWindowHeight", null));
				this.CheckArgumentRam.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LaunchArgumentRam", null));
				this.RefreshJavaComboBox();
				((MyRadioBox)base.FindName(Conversions.ToString(Operators.ConcatenateObject("RadioRamType", ModBase.m_IdentifierRepository.Load("LaunchRamType", false, null))))).Checked = true;
				this.SliderRamCustom.Value = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("LaunchRamCustom", null));
				this.TextAdvanceJvm.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchAdvanceJvm", null));
				this.TextAdvanceGame.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchAdvanceGame", null));
				this.TextAdvanceRun.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchAdvanceRun", null));
				this.CheckAdvanceRunWait.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LaunchAdvanceRunWait", null));
				this.CheckAdvanceAssets.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LaunchAdvanceAssets", null));
				this.CheckAdvanceJava.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LaunchAdvanceJava", null));
			}
			catch (NullReferenceException ex)
			{
				ModBase.Log(ex, "启动设置项存在异常，已被自动重置", ModBase.LogLevel.Msgbox, "出现错误");
				this.Reset();
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, "重载启动设置时出错", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x000813F4 File Offset: 0x0007F5F4
		public void Reset()
		{
			try
			{
				ModBase.m_IdentifierRepository.Reset("LaunchArgumentTitle", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchArgumentInfo", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchArgumentIndie", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchArgumentVisible", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchArgumentWindowType", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchArgumentWindowWidth", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchArgumentWindowHeight", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchArgumentPriority", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchArgumentRam", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchRamType", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchRamCustom", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchSkinType", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchSkinID", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchAdvanceJvm", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchAdvanceGame", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchAdvanceJava", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchAdvanceAssets", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchAdvanceRun", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchAdvanceRunWait", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchArgumentJavaAll", false, null);
				ModBase.m_IdentifierRepository.Reset("LaunchArgumentJavaSelect", false, null);
				ModJava._Process.Start(null, true);
				ModBase.Log("[Setup] 已初始化启动设置", ModBase.LogLevel.Normal, "出现错误");
				ModMain.Hint("已初始化启动设置！", ModMain.HintType.Finish, false);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "初始化启动设置失败", ModBase.LogLevel.Msgbox, "出现错误");
			}
			this.Reload();
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x000815D4 File Offset: 0x0007F7D4
		private static void RadioBoxChange(MyRadioBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(sender.Tag.ToString().Split("/")[0], ModBase.Val(sender.Tag.ToString().Split("/")[1]), false, null);
			}
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0000ACF9 File Offset: 0x00008EF9
		private static void TextBoxChange(MyTextBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.Text, false, null);
			}
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0000AD1F File Offset: 0x00008F1F
		private static void SliderChange(MySlider sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.Value, false, null);
			}
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0000AD4A File Offset: 0x00008F4A
		private static void ComboChange(MyComboBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.SelectedIndex, false, null);
			}
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0000AD75 File Offset: 0x00008F75
		private static void CheckBoxChange(MyCheckBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.Checked, false, null);
			}
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0008162C File Offset: 0x0007F82C
		private void BtnSkinChange_Click(object sender, EventArgs e)
		{
			ModMinecraft.McSkinInfo mcSkinInfo = ModMinecraft.McSkinSelect();
			if (mcSkinInfo._ModelMap)
			{
				this.ChangeSkin(mcSkinInfo);
			}
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00081650 File Offset: 0x0007F850
		private void RadioSkinType3_Check(object sender, ModBase.RouteEventArgs e)
		{
			if (ModAnimation.CalcParser() == 0 && e.interpreterError && !File.Exists(ModBase.m_InstanceRepository + "CustomSkin.png"))
			{
				ModMinecraft.McSkinInfo mcSkinInfo = ModMinecraft.McSkinSelect();
				if (!mcSkinInfo._ModelMap)
				{
					e.m_SerializerError = true;
					return;
				}
				if (!this.ChangeSkin(mcSkinInfo))
				{
					e.m_SerializerError = true;
				}
			}
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x000816A8 File Offset: 0x0007F8A8
		private bool ChangeSkin(ModMinecraft.McSkinInfo SkinInfo)
		{
			bool result;
			try
			{
				File.Delete(ModBase.m_InstanceRepository + "CustomSkin.png");
				ModBase.CopyFile(SkinInfo.managerMap, ModBase.m_InstanceRepository + "CustomSkin.png");
				MyBitmap myBitmap = new MyBitmap(ModBase.m_InstanceRepository + "CustomSkin.png");
				if (myBitmap._ContainerIterator.Width == 64 && myBitmap._ContainerIterator.Height == 32)
				{
					System.Drawing.Image image = myBitmap;
					Bitmap bitmap = new Bitmap(64, 64);
					using (Graphics graphics = Graphics.FromImage(bitmap))
					{
						graphics.DrawImageUnscaled(image, new System.Drawing.Point(0, 0));
					}
					File.Delete(ModBase.m_InstanceRepository + "CustomSkin.png");
					bitmap.Save(ModBase.m_InstanceRepository + "CustomSkin.png");
				}
				ModBase.m_IdentifierRepository.Set("LaunchSkinSlim", SkinInfo.eventMap, false, null);
				result = true;
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "改变离线皮肤失败", ModBase.LogLevel.Msgbox, "出现错误");
				result = false;
			}
			finally
			{
				PageLaunchLeft.infoMapper.Start(null, true);
			}
			return result;
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00081814 File Offset: 0x0007FA14
		private void BtnSkinDelete_Click(object sender, EventArgs e)
		{
			try
			{
				File.Delete(ModBase.m_InstanceRepository + "CustomSkin.png");
				this.RadioSkinType0.SetChecked(true, true, true);
				ModMain.Hint("离线皮肤已清空！", ModMain.HintType.Finish, true);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "清空离线皮肤失败", ModBase.LogLevel.Msgbox, "出现错误");
			}
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x0000ADA0 File Offset: 0x00008FA0
		private void BtnSkinSave_Click(object sender, EventArgs e)
		{
			MySkin.Save(PageLaunchLeft.infoMapper);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0000ADAC File Offset: 0x00008FAC
		private void BtnSkinCache_Click(object sender, EventArgs e)
		{
			MySkin.RefreshCache(null);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0000ADB4 File Offset: 0x00008FB4
		public void RamType(int Type)
		{
			if (this.SliderRamCustom != null)
			{
				this.SliderRamCustom.IsEnabled = (Type == 1);
			}
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00081880 File Offset: 0x0007FA80
		public void RefreshRam(bool ShowAnim)
		{
			if (this.LabRamGame != null && this.LabRamUsed != null && !(ModMain._ProcessIterator._MethodIterator != FormMain.PageType.Setup) && ModMain._ClassIterator._ConnectionThread == FormMain.PageSubType.Default)
			{
				double ram = PageSetupLaunch.GetRam(ModMinecraft.AddClient(), false, null);
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
					this.LabRamWarn.Visibility = ((ram != 1.0 || ModJava.smethod_0(null) || ModBase.m_StubRepository || !Enumerable.Any<ModJava.JavaEntry>(ModJava.interceptor)) ? Visibility.Collapsed : Visibility.Visible);
				}
				if (ShowAnim)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaGridLengthWidth(this.ColumnRamUsed, num4 - this.ColumnRamUsed.Width.Value, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false),
						ModAnimation.AaGridLengthWidth(this.ColumnRamGame, num3 - this.ColumnRamGame.Width.Value, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false),
						ModAnimation.AaGridLengthWidth(this.ColumnRamEmpty, num5 - this.ColumnRamEmpty.Width.Value, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false)
					}, "SetupLaunch Ram Grid", false);
					return;
				}
				this.ColumnRamUsed.Width = new GridLength(num4, GridUnitType.Star);
				this.ColumnRamGame.Width = new GridLength(num3, GridUnitType.Star);
				this.ColumnRamEmpty.Width = new GridLength(num5, GridUnitType.Star);
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0000ADCD File Offset: 0x00008FCD
		private void RefreshRam()
		{
			this.RefreshRam(true);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00081CFC File Offset: 0x0007FEFC
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
			if (this.candidateThread != num)
			{
				this.candidateThread = num;
				switch (num)
				{
				case 0:
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaOpacity(this.LabRamUsed, -this.LabRamUsed.Opacity, 100, 0, null, false),
						ModAnimation.AaOpacity(this.LabRamTotal, -this.LabRamTotal.Opacity, 100, 0, null, false),
						ModAnimation.AaOpacity(this.LabRamUsedTitle, -this.LabRamUsedTitle.Opacity, 100, 0, null, false)
					}, "SetupLaunch Ram TextLeft", false);
					break;
				case 1:
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaOpacity(this.LabRamUsed, 1.0 - this.LabRamUsed.Opacity, 100, 0, null, false),
						ModAnimation.AaOpacity(this.LabRamTotal, -this.LabRamTotal.Opacity, 100, 0, null, false),
						ModAnimation.AaOpacity(this.LabRamUsedTitle, 0.7 - this.LabRamUsedTitle.Opacity, 100, 0, null, false)
					}, "SetupLaunch Ram TextLeft", false);
					break;
				case 2:
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaOpacity(this.LabRamUsed, 1.0 - this.LabRamUsed.Opacity, 100, 0, null, false),
						ModAnimation.AaOpacity(this.LabRamTotal, 1.0 - this.LabRamTotal.Opacity, 100, 0, null, false),
						ModAnimation.AaOpacity(this.LabRamUsedTitle, 0.7 - this.LabRamUsedTitle.Opacity, 100, 0, null, false)
					}, "SetupLaunch Ram TextLeft", false);
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
				if (ModAnimation.CalcParser() == 0 && (this._AdvisorThread != num2 || ModAnimation.AniIsRun("SetupLaunch Ram TextRight")))
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaX(this.LabRamGame, actualWidth2 - actualWidth3 - this.LabRamGame.Margin.Left, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false),
						ModAnimation.AaX(this.LabRamGameTitle, actualWidth2 - actualWidth6 - this.LabRamGameTitle.Margin.Left, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false)
					}, "SetupLaunch Ram TextRight", false);
				}
				else
				{
					ModAnimation.AniStop("SetupLaunch Ram TextRight");
					this.LabRamGame.Margin = new Thickness(actualWidth2 - actualWidth3, 3.0, 0.0, 0.0);
					this.LabRamGameTitle.Margin = new Thickness(actualWidth2 - actualWidth6, 0.0, 0.0, 5.0);
				}
			}
			else if (ModAnimation.CalcParser() == 0 && (this._AdvisorThread != num2 || ModAnimation.AniIsRun("SetupLaunch Ram TextRight")))
			{
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaX(this.LabRamGame, 2.0 + actualWidth - this.LabRamGame.Margin.Left, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false),
					ModAnimation.AaX(this.LabRamGameTitle, 2.0 + actualWidth - this.LabRamGameTitle.Margin.Left, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false)
				}, "SetupLaunch Ram TextRight", false);
			}
			else
			{
				ModAnimation.AniStop("SetupLaunch Ram TextRight");
				this.LabRamGame.Margin = new Thickness(2.0 + actualWidth, 3.0, 0.0, 0.0);
				this.LabRamGameTitle.Margin = new Thickness(2.0 + actualWidth, 0.0, 0.0, 5.0);
			}
			this._AdvisorThread = num2;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x000821D8 File Offset: 0x000803D8
		public static double GetRam(ModMinecraft.McVersion Version, bool UseVersionJavaSetup, bool? nullable_0 = null)
		{
			double num7;
			if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LaunchRamType", null), 0, false))
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
				int num8 = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("LaunchRamCustom", null));
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
			if (nullable_0 ?? (!ModJava.smethod_0(UseVersionJavaSetup ? Version : null)))
			{
				num7 = Math.Min(1.0, num7);
			}
			return Math.Min(32.0, num7);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x000824EC File Offset: 0x000806EC
		public void RefreshJavaComboBox()
		{
			if (this.ComboArgumentJava != null)
			{
				this.ComboArgumentJava.Items.Clear();
				this.ComboArgumentJava.Items.Add(new MyComboBoxItem
				{
					Content = "自动选择合适的 Java",
					Tag = "自动选择"
				});
				MyComboBoxItem myComboBoxItem = null;
				string text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchArgumentJavaSelect", null));
				try
				{
					try
					{
						foreach (ModJava.JavaEntry javaEntry in ModJava.interceptor.Clone<ModJava.JavaEntry>().Sort((PageSetupLaunch._Closure$__.$I23-0 == null) ? (PageSetupLaunch._Closure$__.$I23-0 = ((ModJava.JavaEntry l, ModJava.JavaEntry r) => l.PostTests() < r.PostTests())) : PageSetupLaunch._Closure$__.$I23-0))
						{
							MyComboBoxItem myComboBoxItem2 = new MyComboBoxItem
							{
								Content = javaEntry.ToString(),
								ToolTip = javaEntry._UtilsRepository,
								Tag = javaEntry
							};
							ToolTipService.SetHorizontalOffset(myComboBoxItem2, 400.0);
							this.ComboArgumentJava.Items.Add(myComboBoxItem2);
							if (Operators.CompareString(text, "", false) != 0 && Operators.CompareString(ModJava.JavaEntry.FromJson((JObject)ModBase.GetJson(text))._UtilsRepository, javaEntry._UtilsRepository, false) == 0)
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
					ModBase.m_IdentifierRepository.Set("LaunchArgumentJavaSelect", "", false, null);
					ModBase.Log(ex, "更新设置 Java 下拉框失败", ModBase.LogLevel.Feedback, "出现错误");
				}
				if (myComboBoxItem == null && Enumerable.Any<ModJava.JavaEntry>(ModJava.interceptor))
				{
					myComboBoxItem = (MyComboBoxItem)this.ComboArgumentJava.Items[0];
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

		// Token: 0x06001213 RID: 4627 RVA: 0x00082700 File Offset: 0x00080900
		private void ComboArgumentJava_DropDownOpened(object sender, EventArgs e)
		{
			if (this.ComboArgumentJava.SelectedItem == null || Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(this.ComboArgumentJava.Items[0], null, "Content", new object[0], null, null, null), "未找到可用的 Java", false) || Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(this.ComboArgumentJava.Items[0], null, "Content", new object[0], null, null, null), "加载中……", false))
			{
				this.ComboArgumentJava.IsDropDownOpen = false;
			}
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0008278C File Offset: 0x0008098C
		private void JavaSelectionUpdate()
		{
			if (ModAnimation.CalcParser() == 0 && this.ComboArgumentJava.SelectedItem != null && NewLateBinding.LateGet(this.ComboArgumentJava.SelectedItem, null, "Tag", new object[0], null, null, null) != null)
			{
				object objectValue = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(this.ComboArgumentJava.SelectedItem, null, "Tag", new object[0], null, null, null));
				if ("自动选择".Equals(RuntimeHelpers.GetObjectValue(objectValue)))
				{
					ModBase.m_IdentifierRepository.Set("LaunchArgumentJavaSelect", "", false, null);
					ModBase.Log("[Java] 修改 Java 选择设置：自动选择", ModBase.LogLevel.Normal, "出现错误");
				}
				else
				{
					ModBase.m_IdentifierRepository.Set("LaunchArgumentJavaSelect", ((JObject)NewLateBinding.LateGet(objectValue, null, "ToJson", new object[0], null, null, null)).ToString(0, new JsonConverter[0]), false, null);
					ModBase.Log("[Java] 修改 Java 选择设置：" + objectValue.ToString(), ModBase.LogLevel.Normal, "出现错误");
				}
				this.RefreshRam(true);
			}
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00082890 File Offset: 0x00080A90
		private void BtnArgumentJavaSelect_Click(object sender, EventArgs e)
		{
			if (ModJava._Process.State == ModBase.LoadState.Loading)
			{
				ModMain.Hint("正在搜索 Java，请稍候！", ModMain.HintType.Critical, true);
				return;
			}
			string text = ModBase.SelectFile("javaw.exe|javaw.exe", "选择 bin 文件夹中的 javaw.exe 文件");
			if (Operators.CompareString(text, "", false) != 0)
			{
				text = ModBase.GetPathFromFullPath(text);
				try
				{
					ModJava.JavaEntry javaEntry = new ModJava.JavaEntry(text, true);
					javaEntry.Check();
					JArray jarray = new JArray();
					jarray.Add(javaEntry.ToJson());
					JArray jarray2 = jarray;
					try
					{
						foreach (object obj in ((IEnumerable)ModBase.GetJson(Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchArgumentJavaAll", null)))))
						{
							object objectValue = RuntimeHelpers.GetObjectValue(obj);
							if (Operators.CompareString(ModJava.JavaEntry.FromJson((JObject)objectValue)._UtilsRepository, javaEntry._UtilsRepository, false) != 0)
							{
								jarray2.Add(RuntimeHelpers.GetObjectValue(objectValue));
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
					ModBase.m_IdentifierRepository.Set("LaunchArgumentJavaAll", jarray2.ToString(0, new JsonConverter[0]), false, null);
					ModJava._Process.Start(null, true);
					ModMain.Hint("已将该 Java 加入 Java 列表！", ModMain.HintType.Finish, true);
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "该 Java 存在异常，无法使用", ModBase.LogLevel.Msgbox, "异常的 Java");
				}
			}
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x000829F4 File Offset: 0x00080BF4
		private void BtnArgumentJavaSearch_Click(object sender, EventArgs e)
		{
			if (ModJava._Process.State == ModBase.LoadState.Loading)
			{
				ModMain.Hint("正在搜索 Java，请稍候！", ModMain.HintType.Critical, true);
				return;
			}
			ModBase.RunInThread((PageSetupLaunch._Closure$__.$I27-0 == null) ? (PageSetupLaunch._Closure$__.$I27-0 = delegate()
			{
				ModMain.Hint("正在搜索 Java！", ModMain.HintType.Info, true);
				ModJava._Process.WaitForExit(null, null, true);
				if (!Enumerable.Any<ModJava.JavaEntry>(ModJava.interceptor))
				{
					ModMain.Hint("未找到可用的 Java！", ModMain.HintType.Critical, true);
					return;
				}
				ModMain.Hint("已找到 " + Conversions.ToString(ModJava.interceptor.Count) + " 个 Java，请检查下拉框查看列表！", ModMain.HintType.Finish, true);
			}) : PageSetupLaunch._Closure$__.$I27-0);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00082A44 File Offset: 0x00080C44
		private void method_0()
		{
			if (this.ComboArgumentWindowType != null)
			{
				if (this.ComboArgumentWindowType.SelectedIndex == 3 && this.LabArgumentWindowMiddle != null && this.LabArgumentWindowMiddle.Visibility == Visibility.Collapsed)
				{
					this.LabArgumentWindowMiddle.Visibility = Visibility.Visible;
					this.TextArgumentWindowHeight.Visibility = Visibility.Visible;
					this.TextArgumentWindowWidth.Visibility = Visibility.Visible;
					return;
				}
				if (this.ComboArgumentWindowType.SelectedIndex != 3 && this.LabArgumentWindowMiddle != null && this.LabArgumentWindowMiddle.Visibility == Visibility.Visible)
				{
					this.LabArgumentWindowMiddle.Visibility = Visibility.Collapsed;
					this.TextArgumentWindowHeight.Visibility = Visibility.Collapsed;
					this.TextArgumentWindowWidth.Visibility = Visibility.Collapsed;
				}
			}
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00082AEC File Offset: 0x00080CEC
		private void ComboArgumentVisibie_SizeChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ModAnimation.CalcParser() == 0 && this.ComboArgumentVisibie.SelectedIndex == 0 && ModMain.MyMsgBox("若在游戏启动后立即关闭启动器，崩溃检测、更改游戏标题等功能将失效。\r\n如果想保留这些功能，可以选择让启动器在游戏启动后隐藏，游戏退出后自动关闭。", "提醒", "继续", "取消", "", false, true, false, null, null, null) == 2)
			{
				this.ComboArgumentVisibie.SelectedItem = RuntimeHelpers.GetObjectValue(e.RemovedItems[0]);
			}
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00082B50 File Offset: 0x00080D50
		private void CheckArgumentRam_Change()
		{
			if (ModAnimation.CalcParser() == 0 && this.CheckArgumentRam.Checked && ModMain.MyMsgBox("内存优化会显著延长启动耗时，建议仅在内存不足时开启。\r\n如果你在使用机械硬盘，这还可能导致一小段时间的严重卡顿。" + (ModBase.IsAdmin() ? "" : string.Format("{0}{1}每次启动游戏，PCL 都需要申请管理员权限以进行内存优化。{2}若想自动授予权限，可以右键 PCL，打开 属性 → 兼容性 → 以管理员身份运行此程序。", "\r\n", "\r\n", "\r\n")), "提醒", "确定", "取消", "", false, true, false, null, null, null) == 2)
			{
				this.CheckArgumentRam.Checked = false;
			}
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00082BD0 File Offset: 0x00080DD0
		private void ComboArgumentIndie_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ModAnimation.CalcParser() == 0 && !this._AccountThread && ModMain.MyMsgBox("调整版本隔离后，你可能得把游戏存档、Mod 等文件手动迁移到新的游戏文件夹中。\r\n如果修改后发现存档消失，把这项设置改回来就能恢复。\r\n如果你不会迁移存档，不建议修改这项设置！", "警告", "我知道我在做什么", "取消", "", true, true, false, null, null, null) == 2)
			{
				this._AccountThread = true;
				this.ComboArgumentIndie.SelectedItem = RuntimeHelpers.GetObjectValue(e.RemovedItems[0]);
				this._AccountThread = false;
			}
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0000ADD6 File Offset: 0x00008FD6
		private void TextAdvanceRun_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.CheckAdvanceRunWait.Visibility = ((Operators.CompareString(this.TextAdvanceRun.Text, "", false) == 0) ? Visibility.Collapsed : Visibility.Visible);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0000ADFF File Offset: 0x00008FFF
		private void TextAdvanceJvm_TextChanged()
		{
			this.BtnAdvanceJvmReset.Visibility = ((Operators.CompareString(this.TextAdvanceJvm.Text, ModBase.m_IdentifierRepository.GetDefault("LaunchAdvanceJvm"), false) == 0) ? Visibility.Hidden : Visibility.Visible);
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x0000AE32 File Offset: 0x00009032
		private void BtnAdvanceJvmReset_Click(object sender, EventArgs e)
		{
			ModBase.m_IdentifierRepository.Reset("LaunchAdvanceJvm", false, null);
			this.Reload();
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x0000AE4B File Offset: 0x0000904B
		// (set) Token: 0x0600121F RID: 4639 RVA: 0x0000AE53 File Offset: 0x00009053
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x0000AE5C File Offset: 0x0000905C
		// (set) Token: 0x06001221 RID: 4641 RVA: 0x0000AE64 File Offset: 0x00009064
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x0000AE6D File Offset: 0x0000906D
		// (set) Token: 0x06001223 RID: 4643 RVA: 0x0000AE75 File Offset: 0x00009075
		internal virtual MyCard CardArgument { get; set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x0000AE7E File Offset: 0x0000907E
		// (set) Token: 0x06001225 RID: 4645 RVA: 0x00082C40 File Offset: 0x00080E40
		internal virtual MyTextBox TextArgumentTitle
		{
			[CompilerGenerated]
			get
			{
				return this.m_ModelThread;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageSetupLaunch._Closure$__.$IR50-2 == null) ? (PageSetupLaunch._Closure$__.$IR50-2 = delegate(object sender, RoutedEventArgs e)
				{
					PageSetupLaunch.TextBoxChange((MyTextBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR50-2;
				MyTextBox modelThread = this.m_ModelThread;
				if (modelThread != null)
				{
					modelThread.DestroyReader(value2);
				}
				this.m_ModelThread = value;
				modelThread = this.m_ModelThread;
				if (modelThread != null)
				{
					modelThread.SetupReader(value2);
				}
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x0000AE86 File Offset: 0x00009086
		// (set) Token: 0x06001227 RID: 4647 RVA: 0x00082C9C File Offset: 0x00080E9C
		internal virtual MyTextBox TextArgumentInfo
		{
			[CompilerGenerated]
			get
			{
				return this.m_WrapperThread;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageSetupLaunch._Closure$__.$IR54-3 == null) ? (PageSetupLaunch._Closure$__.$IR54-3 = delegate(object sender, RoutedEventArgs e)
				{
					PageSetupLaunch.TextBoxChange((MyTextBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR54-3;
				MyTextBox wrapperThread = this.m_WrapperThread;
				if (wrapperThread != null)
				{
					wrapperThread.DestroyReader(value2);
				}
				this.m_WrapperThread = value;
				wrapperThread = this.m_WrapperThread;
				if (wrapperThread != null)
				{
					wrapperThread.SetupReader(value2);
				}
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x0000AE8E File Offset: 0x0000908E
		// (set) Token: 0x06001229 RID: 4649 RVA: 0x00082CF8 File Offset: 0x00080EF8
		internal virtual MyComboBox ComboArgumentIndie
		{
			[CompilerGenerated]
			get
			{
				return this.baseThread;
			}
			[CompilerGenerated]
			set
			{
				SelectionChangedEventHandler value2 = (PageSetupLaunch._Closure$__.$IR58-4 == null) ? (PageSetupLaunch._Closure$__.$IR58-4 = delegate(object sender, SelectionChangedEventArgs e)
				{
					PageSetupLaunch.ComboChange((MyComboBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR58-4;
				SelectionChangedEventHandler value3 = new SelectionChangedEventHandler(this.ComboArgumentIndie_SelectionChanged);
				MyComboBox myComboBox = this.baseThread;
				if (myComboBox != null)
				{
					myComboBox.SelectionChanged -= value2;
					myComboBox.SelectionChanged -= value3;
				}
				this.baseThread = value;
				myComboBox = this.baseThread;
				if (myComboBox != null)
				{
					myComboBox.SelectionChanged += value2;
					myComboBox.SelectionChanged += value3;
				}
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x0000AE96 File Offset: 0x00009096
		// (set) Token: 0x0600122B RID: 4651 RVA: 0x00082D70 File Offset: 0x00080F70
		internal virtual MyComboBox ComboArgumentVisibie
		{
			[CompilerGenerated]
			get
			{
				return this.attributeThread;
			}
			[CompilerGenerated]
			set
			{
				SelectionChangedEventHandler value2 = (PageSetupLaunch._Closure$__.$IR62-5 == null) ? (PageSetupLaunch._Closure$__.$IR62-5 = delegate(object sender, SelectionChangedEventArgs e)
				{
					PageSetupLaunch.ComboChange((MyComboBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR62-5;
				SelectionChangedEventHandler value3 = new SelectionChangedEventHandler(this.ComboArgumentVisibie_SizeChanged);
				MyComboBox myComboBox = this.attributeThread;
				if (myComboBox != null)
				{
					myComboBox.SelectionChanged -= value2;
					myComboBox.SelectionChanged -= value3;
				}
				this.attributeThread = value;
				myComboBox = this.attributeThread;
				if (myComboBox != null)
				{
					myComboBox.SelectionChanged += value2;
					myComboBox.SelectionChanged += value3;
				}
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x0000AE9E File Offset: 0x0000909E
		// (set) Token: 0x0600122D RID: 4653 RVA: 0x00082DE8 File Offset: 0x00080FE8
		internal virtual MyComboBox ComboArgumentPriority
		{
			[CompilerGenerated]
			get
			{
				return this.codeThread;
			}
			[CompilerGenerated]
			set
			{
				SelectionChangedEventHandler value2 = (PageSetupLaunch._Closure$__.$IR66-6 == null) ? (PageSetupLaunch._Closure$__.$IR66-6 = delegate(object sender, SelectionChangedEventArgs e)
				{
					PageSetupLaunch.ComboChange((MyComboBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR66-6;
				MyComboBox myComboBox = this.codeThread;
				if (myComboBox != null)
				{
					myComboBox.SelectionChanged -= value2;
				}
				this.codeThread = value;
				myComboBox = this.codeThread;
				if (myComboBox != null)
				{
					myComboBox.SelectionChanged += value2;
				}
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x0000AEA6 File Offset: 0x000090A6
		// (set) Token: 0x0600122F RID: 4655 RVA: 0x0000AEAE File Offset: 0x000090AE
		internal virtual Grid PanArgumentWindow { get; set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x0000AEB7 File Offset: 0x000090B7
		// (set) Token: 0x06001231 RID: 4657 RVA: 0x00082E44 File Offset: 0x00081044
		internal virtual MyComboBox ComboArgumentWindowType
		{
			[CompilerGenerated]
			get
			{
				return this.m_AnnotationThread;
			}
			[CompilerGenerated]
			set
			{
				SelectionChangedEventHandler value2 = (PageSetupLaunch._Closure$__.$IR74-7 == null) ? (PageSetupLaunch._Closure$__.$IR74-7 = delegate(object sender, SelectionChangedEventArgs e)
				{
					PageSetupLaunch.ComboChange((MyComboBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR74-7;
				SelectionChangedEventHandler value3 = delegate(object sender, SelectionChangedEventArgs e)
				{
					this.method_0();
				};
				MyComboBox annotationThread = this.m_AnnotationThread;
				if (annotationThread != null)
				{
					annotationThread.SelectionChanged -= value2;
					annotationThread.SelectionChanged -= value3;
				}
				this.m_AnnotationThread = value;
				annotationThread = this.m_AnnotationThread;
				if (annotationThread != null)
				{
					annotationThread.SelectionChanged += value2;
					annotationThread.SelectionChanged += value3;
				}
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x0000AEBF File Offset: 0x000090BF
		// (set) Token: 0x06001233 RID: 4659 RVA: 0x00082EBC File Offset: 0x000810BC
		internal virtual MyTextBox TextArgumentWindowWidth
		{
			[CompilerGenerated]
			get
			{
				return this._InfoThread;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageSetupLaunch._Closure$__.$IR78-9 == null) ? (PageSetupLaunch._Closure$__.$IR78-9 = delegate(object sender, RoutedEventArgs e)
				{
					PageSetupLaunch.TextBoxChange((MyTextBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR78-9;
				MyTextBox infoThread = this._InfoThread;
				if (infoThread != null)
				{
					infoThread.DestroyReader(value2);
				}
				this._InfoThread = value;
				infoThread = this._InfoThread;
				if (infoThread != null)
				{
					infoThread.SetupReader(value2);
				}
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x0000AEC7 File Offset: 0x000090C7
		// (set) Token: 0x06001235 RID: 4661 RVA: 0x0000AECF File Offset: 0x000090CF
		internal virtual TextBlock LabArgumentWindowMiddle { get; set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x0000AED8 File Offset: 0x000090D8
		// (set) Token: 0x06001237 RID: 4663 RVA: 0x00082F18 File Offset: 0x00081118
		internal virtual MyTextBox TextArgumentWindowHeight
		{
			[CompilerGenerated]
			get
			{
				return this.facadeThread;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageSetupLaunch._Closure$__.$IR86-10 == null) ? (PageSetupLaunch._Closure$__.$IR86-10 = delegate(object sender, RoutedEventArgs e)
				{
					PageSetupLaunch.TextBoxChange((MyTextBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR86-10;
				MyTextBox myTextBox = this.facadeThread;
				if (myTextBox != null)
				{
					myTextBox.DestroyReader(value2);
				}
				this.facadeThread = value;
				myTextBox = this.facadeThread;
				if (myTextBox != null)
				{
					myTextBox.SetupReader(value2);
				}
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x0000AEE0 File Offset: 0x000090E0
		// (set) Token: 0x06001239 RID: 4665 RVA: 0x00082F74 File Offset: 0x00081174
		internal virtual MyComboBox ComboArgumentJava
		{
			[CompilerGenerated]
			get
			{
				return this.m_ListThread;
			}
			[CompilerGenerated]
			set
			{
				EventHandler value2 = new EventHandler(this.ComboArgumentJava_DropDownOpened);
				SelectionChangedEventHandler value3 = delegate(object sender, SelectionChangedEventArgs e)
				{
					this.JavaSelectionUpdate();
				};
				MyComboBox listThread = this.m_ListThread;
				if (listThread != null)
				{
					listThread.DropDownOpened -= value2;
					listThread.SelectionChanged -= value3;
				}
				this.m_ListThread = value;
				listThread = this.m_ListThread;
				if (listThread != null)
				{
					listThread.DropDownOpened += value2;
					listThread.SelectionChanged += value3;
				}
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x0000AEE8 File Offset: 0x000090E8
		// (set) Token: 0x0600123B RID: 4667 RVA: 0x00082FD4 File Offset: 0x000811D4
		internal virtual MyTextButton BtnArgumentJavaSearch
		{
			[CompilerGenerated]
			get
			{
				return this._MerchantThread;
			}
			[CompilerGenerated]
			set
			{
				MyTextButton.ClickEventHandler value2 = new MyTextButton.ClickEventHandler(this.BtnArgumentJavaSearch_Click);
				MyTextButton merchantThread = this._MerchantThread;
				if (merchantThread != null)
				{
					merchantThread.Click -= value2;
				}
				this._MerchantThread = value;
				merchantThread = this._MerchantThread;
				if (merchantThread != null)
				{
					merchantThread.Click += value2;
				}
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x0000AEF0 File Offset: 0x000090F0
		// (set) Token: 0x0600123D RID: 4669 RVA: 0x00083018 File Offset: 0x00081218
		internal virtual MyTextButton BtnArgumentJavaSelect
		{
			[CompilerGenerated]
			get
			{
				return this._AuthenticationThread;
			}
			[CompilerGenerated]
			set
			{
				MyTextButton.ClickEventHandler value2 = new MyTextButton.ClickEventHandler(this.BtnArgumentJavaSelect_Click);
				MyTextButton authenticationThread = this._AuthenticationThread;
				if (authenticationThread != null)
				{
					authenticationThread.Click -= value2;
				}
				this._AuthenticationThread = value;
				authenticationThread = this._AuthenticationThread;
				if (authenticationThread != null)
				{
					authenticationThread.Click += value2;
				}
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x0000AEF8 File Offset: 0x000090F8
		// (set) Token: 0x0600123F RID: 4671 RVA: 0x0000AF00 File Offset: 0x00009100
		internal virtual MyHint LabRamWarn { get; set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x0000AF09 File Offset: 0x00009109
		// (set) Token: 0x06001241 RID: 4673 RVA: 0x0008305C File Offset: 0x0008125C
		internal virtual MyRadioBox RadioRamType0
		{
			[CompilerGenerated]
			get
			{
				return this.m_ComparatorThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupLaunch._Closure$__.$IR106-12 == null) ? (PageSetupLaunch._Closure$__.$IR106-12 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupLaunch.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR106-12;
				IMyRadio.CheckEventHandler value3 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.RefreshRam();
				};
				MyRadioBox comparatorThread = this.m_ComparatorThread;
				if (comparatorThread != null)
				{
					comparatorThread.Check -= value2;
					comparatorThread.Check -= value3;
				}
				this.m_ComparatorThread = value;
				comparatorThread = this.m_ComparatorThread;
				if (comparatorThread != null)
				{
					comparatorThread.Check += value2;
					comparatorThread.Check += value3;
				}
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x0000AF11 File Offset: 0x00009111
		// (set) Token: 0x06001243 RID: 4675 RVA: 0x000830D4 File Offset: 0x000812D4
		internal virtual MyRadioBox RadioRamType1
		{
			[CompilerGenerated]
			get
			{
				return this._MappingThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupLaunch._Closure$__.$IR110-14 == null) ? (PageSetupLaunch._Closure$__.$IR110-14 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupLaunch.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR110-14;
				IMyRadio.CheckEventHandler value3 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.RefreshRam();
				};
				MyRadioBox mappingThread = this._MappingThread;
				if (mappingThread != null)
				{
					mappingThread.Check -= value2;
					mappingThread.Check -= value3;
				}
				this._MappingThread = value;
				mappingThread = this._MappingThread;
				if (mappingThread != null)
				{
					mappingThread.Check += value2;
					mappingThread.Check += value3;
				}
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x0000AF19 File Offset: 0x00009119
		// (set) Token: 0x06001245 RID: 4677 RVA: 0x0008314C File Offset: 0x0008134C
		internal virtual MySlider SliderRamCustom
		{
			[CompilerGenerated]
			get
			{
				return this.tokenizerThread;
			}
			[CompilerGenerated]
			set
			{
				MySlider.ChangeEventHandler obj = (PageSetupLaunch._Closure$__.$IR114-16 == null) ? (PageSetupLaunch._Closure$__.$IR114-16 = delegate(object a0, bool a1)
				{
					PageSetupLaunch.SliderChange((MySlider)a0, a1);
				}) : PageSetupLaunch._Closure$__.$IR114-16;
				MySlider.ChangeEventHandler obj2 = delegate(object a0, bool a1)
				{
					this.RefreshRam();
				};
				MySlider mySlider = this.tokenizerThread;
				if (mySlider != null)
				{
					mySlider.WriteTests(obj);
					mySlider.WriteTests(obj2);
				}
				this.tokenizerThread = value;
				mySlider = this.tokenizerThread;
				if (mySlider != null)
				{
					mySlider.FillTests(obj);
					mySlider.FillTests(obj2);
				}
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x0000AF21 File Offset: 0x00009121
		// (set) Token: 0x06001247 RID: 4679 RVA: 0x000831C4 File Offset: 0x000813C4
		internal virtual MyCheckBox CheckArgumentRam
		{
			[CompilerGenerated]
			get
			{
				return this.m_FilterThread;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupLaunch._Closure$__.$IR118-18 == null) ? (PageSetupLaunch._Closure$__.$IR118-18 = delegate(object a0, bool a1)
				{
					PageSetupLaunch.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupLaunch._Closure$__.$IR118-18;
				MyCheckBox.ChangeEventHandler obj2 = delegate(object a0, bool a1)
				{
					this.CheckArgumentRam_Change();
				};
				MyCheckBox filterThread = this.m_FilterThread;
				if (filterThread != null)
				{
					filterThread.PublishConfig(obj);
					filterThread.PublishConfig(obj2);
				}
				this.m_FilterThread = value;
				filterThread = this.m_FilterThread;
				if (filterThread != null)
				{
					filterThread.InstantiateConfig(obj);
					filterThread.InstantiateConfig(obj2);
				}
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x0000AF29 File Offset: 0x00009129
		// (set) Token: 0x06001249 RID: 4681 RVA: 0x0000AF31 File Offset: 0x00009131
		internal virtual Grid PanRamDisplay { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x0000AF3A File Offset: 0x0000913A
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x0000AF42 File Offset: 0x00009142
		internal virtual ColumnDefinition ColumnRamUsed { get; set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0000AF4B File Offset: 0x0000914B
		// (set) Token: 0x0600124D RID: 4685 RVA: 0x0000AF53 File Offset: 0x00009153
		internal virtual ColumnDefinition ColumnRamGame { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x0000AF5C File Offset: 0x0000915C
		// (set) Token: 0x0600124F RID: 4687 RVA: 0x0000AF64 File Offset: 0x00009164
		internal virtual ColumnDefinition ColumnRamEmpty { get; set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x0000AF6D File Offset: 0x0000916D
		// (set) Token: 0x06001251 RID: 4689 RVA: 0x0000AF75 File Offset: 0x00009175
		internal virtual System.Windows.Shapes.Rectangle RectRamUsed { get; set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x0000AF7E File Offset: 0x0000917E
		// (set) Token: 0x06001253 RID: 4691 RVA: 0x0008323C File Offset: 0x0008143C
		internal virtual System.Windows.Shapes.Rectangle RectRamGame
		{
			[CompilerGenerated]
			get
			{
				return this._InterceptorThread;
			}
			[CompilerGenerated]
			set
			{
				SizeChangedEventHandler value2 = delegate(object sender, SizeChangedEventArgs e)
				{
					this.RefreshRamText();
				};
				System.Windows.Shapes.Rectangle interceptorThread = this._InterceptorThread;
				if (interceptorThread != null)
				{
					interceptorThread.SizeChanged -= value2;
				}
				this._InterceptorThread = value;
				interceptorThread = this._InterceptorThread;
				if (interceptorThread != null)
				{
					interceptorThread.SizeChanged += value2;
				}
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x0000AF86 File Offset: 0x00009186
		// (set) Token: 0x06001255 RID: 4693 RVA: 0x00083280 File Offset: 0x00081480
		internal virtual System.Windows.Shapes.Rectangle RectRamEmpty
		{
			[CompilerGenerated]
			get
			{
				return this._ContainerThread;
			}
			[CompilerGenerated]
			set
			{
				SizeChangedEventHandler value2 = delegate(object sender, SizeChangedEventArgs e)
				{
					this.RefreshRamText();
				};
				System.Windows.Shapes.Rectangle containerThread = this._ContainerThread;
				if (containerThread != null)
				{
					containerThread.SizeChanged -= value2;
				}
				this._ContainerThread = value;
				containerThread = this._ContainerThread;
				if (containerThread != null)
				{
					containerThread.SizeChanged += value2;
				}
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x0000AF8E File Offset: 0x0000918E
		// (set) Token: 0x06001257 RID: 4695 RVA: 0x0000AF96 File Offset: 0x00009196
		internal virtual TextBlock LabRamUsedTitle { get; set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x0000AF9F File Offset: 0x0000919F
		// (set) Token: 0x06001259 RID: 4697 RVA: 0x0000AFA7 File Offset: 0x000091A7
		internal virtual TextBlock LabRamGameTitle { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x0000AFB0 File Offset: 0x000091B0
		// (set) Token: 0x0600125B RID: 4699 RVA: 0x0000AFB8 File Offset: 0x000091B8
		internal virtual TextBlock LabRamUsed { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x0000AFC1 File Offset: 0x000091C1
		// (set) Token: 0x0600125D RID: 4701 RVA: 0x0000AFC9 File Offset: 0x000091C9
		internal virtual TextBlock LabRamTotal { get; set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x0000AFD2 File Offset: 0x000091D2
		// (set) Token: 0x0600125F RID: 4703 RVA: 0x000832C4 File Offset: 0x000814C4
		internal virtual TextBlock LabRamGame
		{
			[CompilerGenerated]
			get
			{
				return this._RecordThread;
			}
			[CompilerGenerated]
			set
			{
				SizeChangedEventHandler value2 = delegate(object sender, SizeChangedEventArgs e)
				{
					this.RefreshRamText();
				};
				TextBlock recordThread = this._RecordThread;
				if (recordThread != null)
				{
					recordThread.SizeChanged -= value2;
				}
				this._RecordThread = value;
				recordThread = this._RecordThread;
				if (recordThread != null)
				{
					recordThread.SizeChanged += value2;
				}
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x0000AFDA File Offset: 0x000091DA
		// (set) Token: 0x06001261 RID: 4705 RVA: 0x0000AFE2 File Offset: 0x000091E2
		internal virtual MyCard CardSkin { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x0000AFEB File Offset: 0x000091EB
		// (set) Token: 0x06001263 RID: 4707 RVA: 0x00083308 File Offset: 0x00081508
		internal virtual MyRadioBox RadioSkinType0
		{
			[CompilerGenerated]
			get
			{
				return this.m_InvocationThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupLaunch._Closure$__.$IR174-23 == null) ? (PageSetupLaunch._Closure$__.$IR174-23 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupLaunch.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR174-23;
				MyRadioBox invocationThread = this.m_InvocationThread;
				if (invocationThread != null)
				{
					invocationThread.Check -= value2;
				}
				this.m_InvocationThread = value;
				invocationThread = this.m_InvocationThread;
				if (invocationThread != null)
				{
					invocationThread.Check += value2;
				}
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0000AFF3 File Offset: 0x000091F3
		// (set) Token: 0x06001265 RID: 4709 RVA: 0x00083364 File Offset: 0x00081564
		internal virtual MyRadioBox RadioSkinType1
		{
			[CompilerGenerated]
			get
			{
				return this.m_ProxyThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupLaunch._Closure$__.$IR178-24 == null) ? (PageSetupLaunch._Closure$__.$IR178-24 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupLaunch.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR178-24;
				MyRadioBox proxyThread = this.m_ProxyThread;
				if (proxyThread != null)
				{
					proxyThread.Check -= value2;
				}
				this.m_ProxyThread = value;
				proxyThread = this.m_ProxyThread;
				if (proxyThread != null)
				{
					proxyThread.Check += value2;
				}
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x0000AFFB File Offset: 0x000091FB
		// (set) Token: 0x06001267 RID: 4711 RVA: 0x000833C0 File Offset: 0x000815C0
		internal virtual MyRadioBox RadioSkinType2
		{
			[CompilerGenerated]
			get
			{
				return this.messageThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupLaunch._Closure$__.$IR182-25 == null) ? (PageSetupLaunch._Closure$__.$IR182-25 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupLaunch.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR182-25;
				MyRadioBox myRadioBox = this.messageThread;
				if (myRadioBox != null)
				{
					myRadioBox.Check -= value2;
				}
				this.messageThread = value;
				myRadioBox = this.messageThread;
				if (myRadioBox != null)
				{
					myRadioBox.Check += value2;
				}
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x0000B003 File Offset: 0x00009203
		// (set) Token: 0x06001269 RID: 4713 RVA: 0x0008341C File Offset: 0x0008161C
		internal virtual MyRadioBox RadioSkinType3
		{
			[CompilerGenerated]
			get
			{
				return this.creatorThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupLaunch._Closure$__.$IR186-26 == null) ? (PageSetupLaunch._Closure$__.$IR186-26 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupLaunch.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR186-26;
				MyRadioBox myRadioBox = this.creatorThread;
				if (myRadioBox != null)
				{
					myRadioBox.Check -= value2;
				}
				this.creatorThread = value;
				myRadioBox = this.creatorThread;
				if (myRadioBox != null)
				{
					myRadioBox.Check += value2;
				}
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x0000B00B File Offset: 0x0000920B
		// (set) Token: 0x0600126B RID: 4715 RVA: 0x00083478 File Offset: 0x00081678
		internal virtual MyRadioBox RadioSkinType4
		{
			[CompilerGenerated]
			get
			{
				return this._InitializerThread;
			}
			[CompilerGenerated]
			set
			{
				IMyRadio.CheckEventHandler value2 = (PageSetupLaunch._Closure$__.$IR190-27 == null) ? (PageSetupLaunch._Closure$__.$IR190-27 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					PageSetupLaunch.RadioBoxChange((MyRadioBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR190-27;
				MyRadioBox.PreviewCheckEventHandler obj = new MyRadioBox.PreviewCheckEventHandler(this.RadioSkinType3_Check);
				MyRadioBox initializerThread = this._InitializerThread;
				if (initializerThread != null)
				{
					initializerThread.Check -= value2;
					initializerThread.RateConfig(obj);
				}
				this._InitializerThread = value;
				initializerThread = this._InitializerThread;
				if (initializerThread != null)
				{
					initializerThread.Check += value2;
					initializerThread.UpdateConfig(obj);
				}
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x0000B013 File Offset: 0x00009213
		// (set) Token: 0x0600126D RID: 4717 RVA: 0x0000B01B File Offset: 0x0000921B
		internal virtual Grid PanSkinID { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x0000B024 File Offset: 0x00009224
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x000834F0 File Offset: 0x000816F0
		internal virtual MyTextBox TextSkinID
		{
			[CompilerGenerated]
			get
			{
				return this.regThread;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageSetupLaunch._Closure$__.$IR198-28 == null) ? (PageSetupLaunch._Closure$__.$IR198-28 = delegate(object sender, RoutedEventArgs e)
				{
					PageSetupLaunch.TextBoxChange((MyTextBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR198-28;
				MyTextBox myTextBox = this.regThread;
				if (myTextBox != null)
				{
					myTextBox.DestroyReader(value2);
				}
				this.regThread = value;
				myTextBox = this.regThread;
				if (myTextBox != null)
				{
					myTextBox.SetupReader(value2);
				}
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x0000B02C File Offset: 0x0000922C
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x0008354C File Offset: 0x0008174C
		internal virtual MyButton BtnSkinSave
		{
			[CompilerGenerated]
			get
			{
				return this.m_ProductThread;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnSkinSave_Click);
				MyButton productThread = this.m_ProductThread;
				if (productThread != null)
				{
					productThread.Click -= value2;
				}
				this.m_ProductThread = value;
				productThread = this.m_ProductThread;
				if (productThread != null)
				{
					productThread.Click += value2;
				}
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x0000B034 File Offset: 0x00009234
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x00083590 File Offset: 0x00081790
		internal virtual MyButton BtnSkinCache
		{
			[CompilerGenerated]
			get
			{
				return this.m_ListenerThread;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnSkinCache_Click);
				MyButton listenerThread = this.m_ListenerThread;
				if (listenerThread != null)
				{
					listenerThread.Click -= value2;
				}
				this.m_ListenerThread = value;
				listenerThread = this.m_ListenerThread;
				if (listenerThread != null)
				{
					listenerThread.Click += value2;
				}
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x0000B03C File Offset: 0x0000923C
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x0000B044 File Offset: 0x00009244
		internal virtual Grid PanSkinChange { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x0000B04D File Offset: 0x0000924D
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x000835D4 File Offset: 0x000817D4
		internal virtual MyButton BtnSkinChange
		{
			[CompilerGenerated]
			get
			{
				return this.visitorThread;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnSkinChange_Click);
				MyButton myButton = this.visitorThread;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.visitorThread = value;
				myButton = this.visitorThread;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x0000B055 File Offset: 0x00009255
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x00083618 File Offset: 0x00081818
		internal virtual MyButton BtnSkinDelete
		{
			[CompilerGenerated]
			get
			{
				return this.m_ValueThread;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnSkinDelete_Click);
				MyButton valueThread = this.m_ValueThread;
				if (valueThread != null)
				{
					valueThread.Click -= value2;
				}
				this.m_ValueThread = value;
				valueThread = this.m_ValueThread;
				if (valueThread != null)
				{
					valueThread.Click += value2;
				}
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x0000B05D File Offset: 0x0000925D
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x0008365C File Offset: 0x0008185C
		internal virtual MyIconButton BtnAdvanceJvmReset
		{
			[CompilerGenerated]
			get
			{
				return this.objectThread;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = new MyIconButton.ClickEventHandler(this.BtnAdvanceJvmReset_Click);
				MyIconButton myIconButton = this.objectThread;
				if (myIconButton != null)
				{
					myIconButton.Click -= value2;
				}
				this.objectThread = value;
				myIconButton = this.objectThread;
				if (myIconButton != null)
				{
					myIconButton.Click += value2;
				}
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x0000B065 File Offset: 0x00009265
		// (set) Token: 0x0600127D RID: 4733 RVA: 0x000836A0 File Offset: 0x000818A0
		internal virtual MyTextBox TextAdvanceJvm
		{
			[CompilerGenerated]
			get
			{
				return this.m_BridgeThread;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageSetupLaunch._Closure$__.$IR226-29 == null) ? (PageSetupLaunch._Closure$__.$IR226-29 = delegate(object sender, RoutedEventArgs e)
				{
					PageSetupLaunch.TextBoxChange((MyTextBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR226-29;
				RoutedEventHandler value3 = delegate(object sender, RoutedEventArgs e)
				{
					this.TextAdvanceJvm_TextChanged();
				};
				MyTextBox bridgeThread = this.m_BridgeThread;
				if (bridgeThread != null)
				{
					bridgeThread.DestroyReader(value2);
					bridgeThread.DestroyReader(value3);
				}
				this.m_BridgeThread = value;
				bridgeThread = this.m_BridgeThread;
				if (bridgeThread != null)
				{
					bridgeThread.SetupReader(value2);
					bridgeThread.SetupReader(value3);
				}
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x0000B06D File Offset: 0x0000926D
		// (set) Token: 0x0600127F RID: 4735 RVA: 0x00083718 File Offset: 0x00081918
		internal virtual MyTextBox TextAdvanceGame
		{
			[CompilerGenerated]
			get
			{
				return this.itemThread;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageSetupLaunch._Closure$__.$IR230-31 == null) ? (PageSetupLaunch._Closure$__.$IR230-31 = delegate(object sender, RoutedEventArgs e)
				{
					PageSetupLaunch.TextBoxChange((MyTextBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR230-31;
				MyTextBox myTextBox = this.itemThread;
				if (myTextBox != null)
				{
					myTextBox.DestroyReader(value2);
				}
				this.itemThread = value;
				myTextBox = this.itemThread;
				if (myTextBox != null)
				{
					myTextBox.SetupReader(value2);
				}
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0000B075 File Offset: 0x00009275
		// (set) Token: 0x06001281 RID: 4737 RVA: 0x00083774 File Offset: 0x00081974
		internal virtual MyTextBox TextAdvanceRun
		{
			[CompilerGenerated]
			get
			{
				return this.reponseThread;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageSetupLaunch._Closure$__.$IR234-32 == null) ? (PageSetupLaunch._Closure$__.$IR234-32 = delegate(object sender, RoutedEventArgs e)
				{
					PageSetupLaunch.TextBoxChange((MyTextBox)sender, e);
				}) : PageSetupLaunch._Closure$__.$IR234-32;
				TextChangedEventHandler value3 = new TextChangedEventHandler(this.TextAdvanceRun_TextChanged);
				MyTextBox myTextBox = this.reponseThread;
				if (myTextBox != null)
				{
					myTextBox.DestroyReader(value2);
					myTextBox.TextChanged -= value3;
				}
				this.reponseThread = value;
				myTextBox = this.reponseThread;
				if (myTextBox != null)
				{
					myTextBox.SetupReader(value2);
					myTextBox.TextChanged += value3;
				}
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x0000B07D File Offset: 0x0000927D
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x000837EC File Offset: 0x000819EC
		internal virtual MyCheckBox CheckAdvanceRunWait
		{
			[CompilerGenerated]
			get
			{
				return this.globalThread;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupLaunch._Closure$__.$IR238-33 == null) ? (PageSetupLaunch._Closure$__.$IR238-33 = delegate(object a0, bool a1)
				{
					PageSetupLaunch.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupLaunch._Closure$__.$IR238-33;
				MyCheckBox myCheckBox = this.globalThread;
				if (myCheckBox != null)
				{
					myCheckBox.PublishConfig(obj);
				}
				this.globalThread = value;
				myCheckBox = this.globalThread;
				if (myCheckBox != null)
				{
					myCheckBox.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x0000B085 File Offset: 0x00009285
		// (set) Token: 0x06001285 RID: 4741 RVA: 0x00083848 File Offset: 0x00081A48
		internal virtual MyCheckBox CheckAdvanceJava
		{
			[CompilerGenerated]
			get
			{
				return this.exceptionThread;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupLaunch._Closure$__.$IR242-34 == null) ? (PageSetupLaunch._Closure$__.$IR242-34 = delegate(object a0, bool a1)
				{
					PageSetupLaunch.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupLaunch._Closure$__.$IR242-34;
				MyCheckBox myCheckBox = this.exceptionThread;
				if (myCheckBox != null)
				{
					myCheckBox.PublishConfig(obj);
				}
				this.exceptionThread = value;
				myCheckBox = this.exceptionThread;
				if (myCheckBox != null)
				{
					myCheckBox.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x0000B08D File Offset: 0x0000928D
		// (set) Token: 0x06001287 RID: 4743 RVA: 0x000838A4 File Offset: 0x00081AA4
		internal virtual MyCheckBox CheckAdvanceAssets
		{
			[CompilerGenerated]
			get
			{
				return this._UtilsThread;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupLaunch._Closure$__.$IR246-35 == null) ? (PageSetupLaunch._Closure$__.$IR246-35 = delegate(object a0, bool a1)
				{
					PageSetupLaunch.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupLaunch._Closure$__.$IR246-35;
				MyCheckBox utilsThread = this._UtilsThread;
				if (utilsThread != null)
				{
					utilsThread.PublishConfig(obj);
				}
				this._UtilsThread = value;
				utilsThread = this._UtilsThread;
				if (utilsThread != null)
				{
					utilsThread.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x00083900 File Offset: 0x00081B00
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._ClassThread)
			{
				this._ClassThread = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagesetup/pagesetuplaunch.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00083930 File Offset: 0x00081B30
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
				this.ComboArgumentVisibie = (MyComboBox)target;
				return;
			}
			if (connectionId == 8)
			{
				this.ComboArgumentPriority = (MyComboBox)target;
				return;
			}
			if (connectionId == 9)
			{
				this.PanArgumentWindow = (Grid)target;
				return;
			}
			if (connectionId == 10)
			{
				this.ComboArgumentWindowType = (MyComboBox)target;
				return;
			}
			if (connectionId == 11)
			{
				this.TextArgumentWindowWidth = (MyTextBox)target;
				return;
			}
			if (connectionId == 12)
			{
				this.LabArgumentWindowMiddle = (TextBlock)target;
				return;
			}
			if (connectionId == 13)
			{
				this.TextArgumentWindowHeight = (MyTextBox)target;
				return;
			}
			if (connectionId == 14)
			{
				this.ComboArgumentJava = (MyComboBox)target;
				return;
			}
			if (connectionId == 15)
			{
				this.BtnArgumentJavaSearch = (MyTextButton)target;
				return;
			}
			if (connectionId == 16)
			{
				this.BtnArgumentJavaSelect = (MyTextButton)target;
				return;
			}
			if (connectionId == 17)
			{
				this.LabRamWarn = (MyHint)target;
				return;
			}
			if (connectionId == 18)
			{
				this.RadioRamType0 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 19)
			{
				this.RadioRamType1 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 20)
			{
				this.SliderRamCustom = (MySlider)target;
				return;
			}
			if (connectionId == 21)
			{
				this.CheckArgumentRam = (MyCheckBox)target;
				return;
			}
			if (connectionId == 22)
			{
				this.PanRamDisplay = (Grid)target;
				return;
			}
			if (connectionId == 23)
			{
				this.ColumnRamUsed = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 24)
			{
				this.ColumnRamGame = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 25)
			{
				this.ColumnRamEmpty = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 26)
			{
				this.RectRamUsed = (System.Windows.Shapes.Rectangle)target;
				return;
			}
			if (connectionId == 27)
			{
				this.RectRamGame = (System.Windows.Shapes.Rectangle)target;
				return;
			}
			if (connectionId == 28)
			{
				this.RectRamEmpty = (System.Windows.Shapes.Rectangle)target;
				return;
			}
			if (connectionId == 29)
			{
				this.LabRamUsedTitle = (TextBlock)target;
				return;
			}
			if (connectionId == 30)
			{
				this.LabRamGameTitle = (TextBlock)target;
				return;
			}
			if (connectionId == 31)
			{
				this.LabRamUsed = (TextBlock)target;
				return;
			}
			if (connectionId == 32)
			{
				this.LabRamTotal = (TextBlock)target;
				return;
			}
			if (connectionId == 33)
			{
				this.LabRamGame = (TextBlock)target;
				return;
			}
			if (connectionId == 34)
			{
				this.CardSkin = (MyCard)target;
				return;
			}
			if (connectionId == 35)
			{
				this.RadioSkinType0 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 36)
			{
				this.RadioSkinType1 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 37)
			{
				this.RadioSkinType2 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 38)
			{
				this.RadioSkinType3 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 39)
			{
				this.RadioSkinType4 = (MyRadioBox)target;
				return;
			}
			if (connectionId == 40)
			{
				this.PanSkinID = (Grid)target;
				return;
			}
			if (connectionId == 41)
			{
				this.TextSkinID = (MyTextBox)target;
				return;
			}
			if (connectionId == 42)
			{
				this.BtnSkinSave = (MyButton)target;
				return;
			}
			if (connectionId == 43)
			{
				this.BtnSkinCache = (MyButton)target;
				return;
			}
			if (connectionId == 44)
			{
				this.PanSkinChange = (Grid)target;
				return;
			}
			if (connectionId == 45)
			{
				this.BtnSkinChange = (MyButton)target;
				return;
			}
			if (connectionId == 46)
			{
				this.BtnSkinDelete = (MyButton)target;
				return;
			}
			if (connectionId == 47)
			{
				this.BtnAdvanceJvmReset = (MyIconButton)target;
				return;
			}
			if (connectionId == 48)
			{
				this.TextAdvanceJvm = (MyTextBox)target;
				return;
			}
			if (connectionId == 49)
			{
				this.TextAdvanceGame = (MyTextBox)target;
				return;
			}
			if (connectionId == 50)
			{
				this.TextAdvanceRun = (MyTextBox)target;
				return;
			}
			if (connectionId == 51)
			{
				this.CheckAdvanceRunWait = (MyCheckBox)target;
				return;
			}
			if (connectionId == 52)
			{
				this.CheckAdvanceJava = (MyCheckBox)target;
				return;
			}
			if (connectionId == 53)
			{
				this.CheckAdvanceAssets = (MyCheckBox)target;
				return;
			}
			this._ClassThread = true;
		}

		// Token: 0x04000947 RID: 2375
		private bool attrThread;

		// Token: 0x04000948 RID: 2376
		private int candidateThread;

		// Token: 0x04000949 RID: 2377
		private int _AdvisorThread;

		// Token: 0x0400094A RID: 2378
		private bool _AccountThread;

		// Token: 0x0400094B RID: 2379
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyScrollViewer queueThread;

		// Token: 0x0400094C RID: 2380
		[CompilerGenerated]
		[AccessedThroughProperty("PanMain")]
		private StackPanel _EventThread;

		// Token: 0x0400094D RID: 2381
		[CompilerGenerated]
		[AccessedThroughProperty("CardArgument")]
		private MyCard m_ManagerThread;

		// Token: 0x0400094E RID: 2382
		[CompilerGenerated]
		[AccessedThroughProperty("TextArgumentTitle")]
		private MyTextBox m_ModelThread;

		// Token: 0x0400094F RID: 2383
		[AccessedThroughProperty("TextArgumentInfo")]
		[CompilerGenerated]
		private MyTextBox m_WrapperThread;

		// Token: 0x04000950 RID: 2384
		[AccessedThroughProperty("ComboArgumentIndie")]
		[CompilerGenerated]
		private MyComboBox baseThread;

		// Token: 0x04000951 RID: 2385
		[CompilerGenerated]
		[AccessedThroughProperty("ComboArgumentVisibie")]
		private MyComboBox attributeThread;

		// Token: 0x04000952 RID: 2386
		[CompilerGenerated]
		[AccessedThroughProperty("ComboArgumentPriority")]
		private MyComboBox codeThread;

		// Token: 0x04000953 RID: 2387
		[CompilerGenerated]
		[AccessedThroughProperty("PanArgumentWindow")]
		private Grid m_PrototypeThread;

		// Token: 0x04000954 RID: 2388
		[CompilerGenerated]
		[AccessedThroughProperty("ComboArgumentWindowType")]
		private MyComboBox m_AnnotationThread;

		// Token: 0x04000955 RID: 2389
		[AccessedThroughProperty("TextArgumentWindowWidth")]
		[CompilerGenerated]
		private MyTextBox _InfoThread;

		// Token: 0x04000956 RID: 2390
		[AccessedThroughProperty("LabArgumentWindowMiddle")]
		[CompilerGenerated]
		private TextBlock m_AdapterThread;

		// Token: 0x04000957 RID: 2391
		[CompilerGenerated]
		[AccessedThroughProperty("TextArgumentWindowHeight")]
		private MyTextBox facadeThread;

		// Token: 0x04000958 RID: 2392
		[AccessedThroughProperty("ComboArgumentJava")]
		[CompilerGenerated]
		private MyComboBox m_ListThread;

		// Token: 0x04000959 RID: 2393
		[AccessedThroughProperty("BtnArgumentJavaSearch")]
		[CompilerGenerated]
		private MyTextButton _MerchantThread;

		// Token: 0x0400095A RID: 2394
		[AccessedThroughProperty("BtnArgumentJavaSelect")]
		[CompilerGenerated]
		private MyTextButton _AuthenticationThread;

		// Token: 0x0400095B RID: 2395
		[AccessedThroughProperty("LabRamWarn")]
		[CompilerGenerated]
		private MyHint m_AlgoThread;

		// Token: 0x0400095C RID: 2396
		[AccessedThroughProperty("RadioRamType0")]
		[CompilerGenerated]
		private MyRadioBox m_ComparatorThread;

		// Token: 0x0400095D RID: 2397
		[AccessedThroughProperty("RadioRamType1")]
		[CompilerGenerated]
		private MyRadioBox _MappingThread;

		// Token: 0x0400095E RID: 2398
		[AccessedThroughProperty("SliderRamCustom")]
		[CompilerGenerated]
		private MySlider tokenizerThread;

		// Token: 0x0400095F RID: 2399
		[AccessedThroughProperty("CheckArgumentRam")]
		[CompilerGenerated]
		private MyCheckBox m_FilterThread;

		// Token: 0x04000960 RID: 2400
		[AccessedThroughProperty("PanRamDisplay")]
		[CompilerGenerated]
		private Grid _DatabaseThread;

		// Token: 0x04000961 RID: 2401
		[CompilerGenerated]
		[AccessedThroughProperty("ColumnRamUsed")]
		private ColumnDefinition predicateThread;

		// Token: 0x04000962 RID: 2402
		[CompilerGenerated]
		[AccessedThroughProperty("ColumnRamGame")]
		private ColumnDefinition m_PoolThread;

		// Token: 0x04000963 RID: 2403
		[AccessedThroughProperty("ColumnRamEmpty")]
		[CompilerGenerated]
		private ColumnDefinition customerThread;

		// Token: 0x04000964 RID: 2404
		[CompilerGenerated]
		[AccessedThroughProperty("RectRamUsed")]
		private System.Windows.Shapes.Rectangle m_PageThread;

		// Token: 0x04000965 RID: 2405
		[AccessedThroughProperty("RectRamGame")]
		[CompilerGenerated]
		private System.Windows.Shapes.Rectangle _InterceptorThread;

		// Token: 0x04000966 RID: 2406
		[AccessedThroughProperty("RectRamEmpty")]
		[CompilerGenerated]
		private System.Windows.Shapes.Rectangle _ContainerThread;

		// Token: 0x04000967 RID: 2407
		[AccessedThroughProperty("LabRamUsedTitle")]
		[CompilerGenerated]
		private TextBlock paramsThread;

		// Token: 0x04000968 RID: 2408
		[CompilerGenerated]
		[AccessedThroughProperty("LabRamGameTitle")]
		private TextBlock dispatcherThread;

		// Token: 0x04000969 RID: 2409
		[AccessedThroughProperty("LabRamUsed")]
		[CompilerGenerated]
		private TextBlock m_ProcessThread;

		// Token: 0x0400096A RID: 2410
		[CompilerGenerated]
		[AccessedThroughProperty("LabRamTotal")]
		private TextBlock _ParameterThread;

		// Token: 0x0400096B RID: 2411
		[CompilerGenerated]
		[AccessedThroughProperty("LabRamGame")]
		private TextBlock _RecordThread;

		// Token: 0x0400096C RID: 2412
		[AccessedThroughProperty("CardSkin")]
		[CompilerGenerated]
		private MyCard _ServiceThread;

		// Token: 0x0400096D RID: 2413
		[CompilerGenerated]
		[AccessedThroughProperty("RadioSkinType0")]
		private MyRadioBox m_InvocationThread;

		// Token: 0x0400096E RID: 2414
		[CompilerGenerated]
		[AccessedThroughProperty("RadioSkinType1")]
		private MyRadioBox m_ProxyThread;

		// Token: 0x0400096F RID: 2415
		[AccessedThroughProperty("RadioSkinType2")]
		[CompilerGenerated]
		private MyRadioBox messageThread;

		// Token: 0x04000970 RID: 2416
		[AccessedThroughProperty("RadioSkinType3")]
		[CompilerGenerated]
		private MyRadioBox creatorThread;

		// Token: 0x04000971 RID: 2417
		[CompilerGenerated]
		[AccessedThroughProperty("RadioSkinType4")]
		private MyRadioBox _InitializerThread;

		// Token: 0x04000972 RID: 2418
		[CompilerGenerated]
		[AccessedThroughProperty("PanSkinID")]
		private Grid singletonThread;

		// Token: 0x04000973 RID: 2419
		[CompilerGenerated]
		[AccessedThroughProperty("TextSkinID")]
		private MyTextBox regThread;

		// Token: 0x04000974 RID: 2420
		[CompilerGenerated]
		[AccessedThroughProperty("BtnSkinSave")]
		private MyButton m_ProductThread;

		// Token: 0x04000975 RID: 2421
		[CompilerGenerated]
		[AccessedThroughProperty("BtnSkinCache")]
		private MyButton m_ListenerThread;

		// Token: 0x04000976 RID: 2422
		[AccessedThroughProperty("PanSkinChange")]
		[CompilerGenerated]
		private Grid m_CollectionThread;

		// Token: 0x04000977 RID: 2423
		[AccessedThroughProperty("BtnSkinChange")]
		[CompilerGenerated]
		private MyButton visitorThread;

		// Token: 0x04000978 RID: 2424
		[AccessedThroughProperty("BtnSkinDelete")]
		[CompilerGenerated]
		private MyButton m_ValueThread;

		// Token: 0x04000979 RID: 2425
		[CompilerGenerated]
		[AccessedThroughProperty("BtnAdvanceJvmReset")]
		private MyIconButton objectThread;

		// Token: 0x0400097A RID: 2426
		[AccessedThroughProperty("TextAdvanceJvm")]
		[CompilerGenerated]
		private MyTextBox m_BridgeThread;

		// Token: 0x0400097B RID: 2427
		[CompilerGenerated]
		[AccessedThroughProperty("TextAdvanceGame")]
		private MyTextBox itemThread;

		// Token: 0x0400097C RID: 2428
		[CompilerGenerated]
		[AccessedThroughProperty("TextAdvanceRun")]
		private MyTextBox reponseThread;

		// Token: 0x0400097D RID: 2429
		[AccessedThroughProperty("CheckAdvanceRunWait")]
		[CompilerGenerated]
		private MyCheckBox globalThread;

		// Token: 0x0400097E RID: 2430
		[AccessedThroughProperty("CheckAdvanceJava")]
		[CompilerGenerated]
		private MyCheckBox exceptionThread;

		// Token: 0x0400097F RID: 2431
		[AccessedThroughProperty("CheckAdvanceAssets")]
		[CompilerGenerated]
		private MyCheckBox _UtilsThread;

		// Token: 0x04000980 RID: 2432
		private bool _ClassThread;
	}
}
