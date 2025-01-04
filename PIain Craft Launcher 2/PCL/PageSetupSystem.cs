using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020001B4 RID: 436
	[DesignerGenerated]
	public class PageSetupSystem : MyPageRight, IComponentConnector
	{
		// Token: 0x060012B3 RID: 4787 RVA: 0x0000B124 File Offset: 0x00009324
		public PageSetupSystem()
		{
			base.Loaded += this.PageSetupSystem_Loaded;
			this.policyThread = false;
			this.InitializeComponent();
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00083D5C File Offset: 0x00081F5C
		private void PageSetupSystem_Loaded(object sender, RoutedEventArgs e)
		{
			this.PanBack.ScrollToHome();
			this.PanDonate.Visibility = Visibility.Collapsed;
			checked
			{
				if (!this.policyThread)
				{
					this.policyThread = true;
					ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
					this.Reload();
					this.SliderLoad();
					ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
				}
			}
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00083DB4 File Offset: 0x00081FB4
		public void Reload()
		{
			this.SliderDownloadThread.Value = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("ToolDownloadThread", null));
			this.SliderDownloadSpeed.Value = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("ToolDownloadSpeed", null));
			this.ComboDownloadVersion.SelectedIndex = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("ToolDownloadVersion", null));
			this.CheckDownloadCert.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("ToolDownloadCert", null));
			this.ComboDownloadTranslate.SelectedIndex = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("ToolDownloadTranslate", null));
			this.ComboDownloadMod.SelectedIndex = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("ToolDownloadMod", null));
			this.ComboModLocalNameStyle.SelectedIndex = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("ToolModLocalNameStyle", null));
			this.CheckDownloadIgnoreQuilt.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("ToolDownloadIgnoreQuilt", null));
			this.CheckUpdateRelease.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("ToolUpdateRelease", null));
			this.CheckUpdateSnapshot.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("ToolUpdateSnapshot", null));
			this.CheckHelpChinese.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("ToolHelpChinese", null));
			this.ComboSystemUpdate.SelectedIndex = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("SystemSystemUpdate", null));
			this.ComboSystemActivity.SelectedIndex = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("SystemSystemActivity", null));
			this.TextSystemCache.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("SystemSystemCache", null));
			this.CheckDebugMode.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("SystemDebugMode", null));
			this.SliderDebugAnim.Value = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("SystemDebugAnim", null));
			this.CheckDebugDelay.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("SystemDebugDelay", null));
			this.CheckDebugSkipCopy.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("SystemDebugSkipCopy", null));
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00084004 File Offset: 0x00082204
		public void Reset()
		{
			try
			{
				ModBase.m_IdentifierRepository.Reset("ToolDownloadThread", false, null);
				ModBase.m_IdentifierRepository.Reset("ToolDownloadSpeed", false, null);
				ModBase.m_IdentifierRepository.Reset("ToolDownloadVersion", false, null);
				ModBase.m_IdentifierRepository.Reset("ToolDownloadTranslate", false, null);
				ModBase.m_IdentifierRepository.Reset("ToolDownloadIgnoreQuilt", false, null);
				ModBase.m_IdentifierRepository.Reset("ToolDownloadCert", false, null);
				ModBase.m_IdentifierRepository.Reset("ToolDownloadMod", false, null);
				ModBase.m_IdentifierRepository.Reset("ToolModLocalNameStyle", false, null);
				ModBase.m_IdentifierRepository.Reset("ToolUpdateRelease", false, null);
				ModBase.m_IdentifierRepository.Reset("ToolUpdateSnapshot", false, null);
				ModBase.m_IdentifierRepository.Reset("ToolHelpChinese", false, null);
				ModBase.m_IdentifierRepository.Reset("SystemDebugMode", false, null);
				ModBase.m_IdentifierRepository.Reset("SystemDebugAnim", false, null);
				ModBase.m_IdentifierRepository.Reset("SystemDebugDelay", false, null);
				ModBase.m_IdentifierRepository.Reset("SystemDebugSkipCopy", false, null);
				ModBase.m_IdentifierRepository.Reset("SystemSystemCache", false, null);
				ModBase.m_IdentifierRepository.Reset("SystemSystemUpdate", false, null);
				ModBase.m_IdentifierRepository.Reset("SystemSystemActivity", false, null);
				ModBase.Log("[Setup] 已初始化启动器页设置", ModBase.LogLevel.Normal, "出现错误");
				ModMain.Hint("已初始化启动器页设置！", ModMain.HintType.Finish, false);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "初始化启动器页设置失败", ModBase.LogLevel.Msgbox, "出现错误");
			}
			this.Reload();
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0000AD75 File Offset: 0x00008F75
		private static void CheckBoxChange(MyCheckBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.Checked, false, null);
			}
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0000AD1F File Offset: 0x00008F1F
		private static void SliderChange(MySlider sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.Value, false, null);
			}
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0000AD4A File Offset: 0x00008F4A
		private static void ComboChange(MyComboBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.SelectedIndex, false, null);
			}
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0000ACF9 File Offset: 0x00008EF9
		private static void TextBoxChange(MyTextBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.Text, false, null);
			}
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x000841A4 File Offset: 0x000823A4
		private void SliderLoad()
		{
			this.SliderDownloadThread.m_RepositoryIterator = ((PageSetupSystem._Closure$__.$I9-0 == null) ? (PageSetupSystem._Closure$__.$I9-0 = ((object v) => Operators.AddObject(v, 1))) : PageSetupSystem._Closure$__.$I9-0);
			this.SliderDownloadSpeed.m_RepositoryIterator = ((PageSetupSystem._Closure$__.$I9-1 == null) ? (PageSetupSystem._Closure$__.$I9-1 = delegate(object v)
			{
				object result;
				if (Operators.ConditionalCompareObjectLessEqual(v, 14, false))
				{
					result = Operators.ConcatenateObject(Operators.MultiplyObject(Operators.AddObject(v, 1), 0.1), " M/s");
				}
				else if (Operators.ConditionalCompareObjectLessEqual(v, 31, false))
				{
					result = Operators.ConcatenateObject(Operators.MultiplyObject(Operators.SubtractObject(v, 11), 0.5), " M/s");
				}
				else if (Operators.ConditionalCompareObjectLessEqual(v, 41, false))
				{
					result = Operators.ConcatenateObject(Operators.SubtractObject(v, 21), " M/s");
				}
				else
				{
					result = "无限制";
				}
				return result;
			}) : PageSetupSystem._Closure$__.$I9-1);
			this.SliderDebugAnim.m_RepositoryIterator = ((PageSetupSystem._Closure$__.$I9-2 == null) ? (PageSetupSystem._Closure$__.$I9-2 = delegate(object v)
			{
				if (!Operators.ConditionalCompareObjectGreater(v, 29, false))
				{
					return Operators.ConcatenateObject(Operators.AddObject(Operators.DivideObject(v, 10), 0.1), "x");
				}
				return "关闭";
			}) : PageSetupSystem._Closure$__.$I9-2);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00084240 File Offset: 0x00082440
		private void SliderDownloadThread_PreviewChange(object sender, ModBase.RouteEventArgs e)
		{
			if (this.SliderDownloadThread.Value >= 100 && Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("HintDownloadThread", null))))
			{
				ModBase.m_IdentifierRepository.Set("HintDownloadThread", true, false, null);
				ModMain.MyMsgBox("如果设置过多的下载线程，可能会导致下载时出现非常严重的卡顿。\r\n一般设置 64 线程即可满足大多数下载需求，除非你知道你在干什么，否则不建议设置更多的线程数！", "警告", "我知道了", "", "", true, true, false, null, null, null);
			}
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0000A5D8 File Offset: 0x000087D8
		private void BtnSystemIdentify_Click(object sender, MouseButtonEventArgs e)
		{
			PageOtherAbout.CopyUniqueAddress();
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0000A544 File Offset: 0x00008744
		private void BtnSystemUnlock_Click(object sender, MouseButtonEventArgs e)
		{
			ModSecret.DonateCodeInput();
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0000B14C File Offset: 0x0000934C
		private void CheckDebugMode_Change()
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModMain.Hint("部分调试信息将在刷新或启动器重启后切换显示！", ModMain.HintType.Info, false);
			}
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x000842B4 File Offset: 0x000824B4
		private void ComboSystemActivity_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ModAnimation.CalcParser() == 0 && this.ComboSystemActivity.SelectedIndex == 2 && ModMain.MyMsgBox("若选择此项，即使在将来出现严重问题时，你也无法获取相关通知。\r\n例如，如果发现某个版本游戏存在严重 Bug，你可能就会因为无法得到通知而导致无法预知的后果。\r\n\r\n一般选择 仅在有重要通知时显示公告 就可以让你尽量不受打扰了。\r\n除非你在制作服务器整合包，或时常手动更新启动器，否则极度不推荐选择此项！", "警告", "我知道我在做什么", "取消", "", true, true, false, null, null, null) == 2)
			{
				this.ComboSystemActivity.SelectedItem = RuntimeHelpers.GetObjectValue(e.RemovedItems[0]);
			}
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0008431C File Offset: 0x0008251C
		private void ComboSystemUpdate_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ModAnimation.CalcParser() == 0 && this.ComboSystemUpdate.SelectedIndex == 3 && ModMain.MyMsgBox("若选择此项，即使在启动器将来出现严重问题时，你也无法获取更新并获得修复。\r\n例如，如果官方修改了登录方式，从而导致现有启动器无法登录，你可能就会因为无法更新而无法开始游戏。\r\n\r\n一般选择 仅在有重大漏洞更新时显示提示 就可以让你尽量不受打扰了。\r\n除非你在制作服务器整合包，或时常手动更新启动器，否则极度不推荐选择此项！", "警告", "我知道我在做什么", "取消", "", true, true, false, null, null, null) == 2)
			{
				this.ComboSystemUpdate.SelectedItem = RuntimeHelpers.GetObjectValue(e.RemovedItems[0]);
			}
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0000B161 File Offset: 0x00009361
		private void BtnSystemUpdate_Click(object sender, EventArgs e)
		{
			ModSecret.UpdateCheckByButton();
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00084384 File Offset: 0x00082584
		public static bool? IsLauncherNewest()
		{
			bool? result;
			try
			{
				string fullStr = ModBase.ReadFile(ModBase.m_DecoratorRepository + "Cache\\Notice.cfg", null);
				if (Enumerable.Count<string>(fullStr.Split("|")) < 3)
				{
					result = null;
				}
				else
				{
					int num = Conversions.ToInteger(fullStr.Split("|")[2]);
					result = new bool?(num <= 347);
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "确认启动器更新失败", ModBase.LogLevel.Feedback, "出现错误");
				result = null;
			}
			return result;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0000B168 File Offset: 0x00009368
		private void BtnSystemSettingExp_Click(object sender, MouseButtonEventArgs e)
		{
			ModMain.Hint("该功能尚在开发中！", ModMain.HintType.Info, true);
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0000B168 File Offset: 0x00009368
		private void BtnSystemSettingImp_Click(object sender, MouseButtonEventArgs e)
		{
			ModMain.Hint("该功能尚在开发中！", ModMain.HintType.Info, true);
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x0000B176 File Offset: 0x00009376
		// (set) Token: 0x060012C7 RID: 4807 RVA: 0x0000B17E File Offset: 0x0000937E
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x0000B187 File Offset: 0x00009387
		// (set) Token: 0x060012C9 RID: 4809 RVA: 0x0000B18F File Offset: 0x0000938F
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x0000B198 File Offset: 0x00009398
		// (set) Token: 0x060012CB RID: 4811 RVA: 0x00084424 File Offset: 0x00082624
		internal virtual MyComboBox ComboDownloadVersion
		{
			[CompilerGenerated]
			get
			{
				return this.schemaThread;
			}
			[CompilerGenerated]
			set
			{
				SelectionChangedEventHandler value2 = (PageSetupSystem._Closure$__.$IR30-1 == null) ? (PageSetupSystem._Closure$__.$IR30-1 = delegate(object sender, SelectionChangedEventArgs e)
				{
					PageSetupSystem.ComboChange((MyComboBox)sender, e);
				}) : PageSetupSystem._Closure$__.$IR30-1;
				MyComboBox myComboBox = this.schemaThread;
				if (myComboBox != null)
				{
					myComboBox.SelectionChanged -= value2;
				}
				this.schemaThread = value;
				myComboBox = this.schemaThread;
				if (myComboBox != null)
				{
					myComboBox.SelectionChanged += value2;
				}
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x0000B1A0 File Offset: 0x000093A0
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x00084480 File Offset: 0x00082680
		internal virtual MySlider SliderDownloadThread
		{
			[CompilerGenerated]
			get
			{
				return this.descriptorThread;
			}
			[CompilerGenerated]
			set
			{
				MySlider.ChangeEventHandler obj = (PageSetupSystem._Closure$__.$IR34-2 == null) ? (PageSetupSystem._Closure$__.$IR34-2 = delegate(object a0, bool a1)
				{
					PageSetupSystem.SliderChange((MySlider)a0, a1);
				}) : PageSetupSystem._Closure$__.$IR34-2;
				MySlider.PreviewChangeEventHandler obj2 = new MySlider.PreviewChangeEventHandler(this.SliderDownloadThread_PreviewChange);
				MySlider mySlider = this.descriptorThread;
				if (mySlider != null)
				{
					mySlider.WriteTests(obj);
					mySlider.IncludeTests(obj2);
				}
				this.descriptorThread = value;
				mySlider = this.descriptorThread;
				if (mySlider != null)
				{
					mySlider.FillTests(obj);
					mySlider.ComputeTests(obj2);
				}
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x0000B1A8 File Offset: 0x000093A8
		// (set) Token: 0x060012CF RID: 4815 RVA: 0x000844F8 File Offset: 0x000826F8
		internal virtual MySlider SliderDownloadSpeed
		{
			[CompilerGenerated]
			get
			{
				return this._PublisherThread;
			}
			[CompilerGenerated]
			set
			{
				MySlider.ChangeEventHandler obj = (PageSetupSystem._Closure$__.$IR38-3 == null) ? (PageSetupSystem._Closure$__.$IR38-3 = delegate(object a0, bool a1)
				{
					PageSetupSystem.SliderChange((MySlider)a0, a1);
				}) : PageSetupSystem._Closure$__.$IR38-3;
				MySlider publisherThread = this._PublisherThread;
				if (publisherThread != null)
				{
					publisherThread.WriteTests(obj);
				}
				this._PublisherThread = value;
				publisherThread = this._PublisherThread;
				if (publisherThread != null)
				{
					publisherThread.FillTests(obj);
				}
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x0000B1B0 File Offset: 0x000093B0
		// (set) Token: 0x060012D1 RID: 4817 RVA: 0x00084554 File Offset: 0x00082754
		internal virtual MyCheckBox CheckDownloadCert
		{
			[CompilerGenerated]
			get
			{
				return this.definitionThread;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupSystem._Closure$__.$IR42-4 == null) ? (PageSetupSystem._Closure$__.$IR42-4 = delegate(object a0, bool a1)
				{
					PageSetupSystem.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupSystem._Closure$__.$IR42-4;
				MyCheckBox myCheckBox = this.definitionThread;
				if (myCheckBox != null)
				{
					myCheckBox.PublishConfig(obj);
				}
				this.definitionThread = value;
				myCheckBox = this.definitionThread;
				if (myCheckBox != null)
				{
					myCheckBox.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x0000B1B8 File Offset: 0x000093B8
		// (set) Token: 0x060012D3 RID: 4819 RVA: 0x000845B0 File Offset: 0x000827B0
		internal virtual MyComboBox ComboDownloadMod
		{
			[CompilerGenerated]
			get
			{
				return this._StrategyThread;
			}
			[CompilerGenerated]
			set
			{
				SelectionChangedEventHandler value2 = (PageSetupSystem._Closure$__.$IR46-5 == null) ? (PageSetupSystem._Closure$__.$IR46-5 = delegate(object sender, SelectionChangedEventArgs e)
				{
					PageSetupSystem.ComboChange((MyComboBox)sender, e);
				}) : PageSetupSystem._Closure$__.$IR46-5;
				MyComboBox strategyThread = this._StrategyThread;
				if (strategyThread != null)
				{
					strategyThread.SelectionChanged -= value2;
				}
				this._StrategyThread = value;
				strategyThread = this._StrategyThread;
				if (strategyThread != null)
				{
					strategyThread.SelectionChanged += value2;
				}
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x0000B1C0 File Offset: 0x000093C0
		// (set) Token: 0x060012D5 RID: 4821 RVA: 0x0008460C File Offset: 0x0008280C
		internal virtual MyComboBox ComboDownloadTranslate
		{
			[CompilerGenerated]
			get
			{
				return this.procThread;
			}
			[CompilerGenerated]
			set
			{
				SelectionChangedEventHandler value2 = (PageSetupSystem._Closure$__.$IR50-6 == null) ? (PageSetupSystem._Closure$__.$IR50-6 = delegate(object sender, SelectionChangedEventArgs e)
				{
					PageSetupSystem.ComboChange((MyComboBox)sender, e);
				}) : PageSetupSystem._Closure$__.$IR50-6;
				MyComboBox myComboBox = this.procThread;
				if (myComboBox != null)
				{
					myComboBox.SelectionChanged -= value2;
				}
				this.procThread = value;
				myComboBox = this.procThread;
				if (myComboBox != null)
				{
					myComboBox.SelectionChanged += value2;
				}
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x0000B1C8 File Offset: 0x000093C8
		// (set) Token: 0x060012D7 RID: 4823 RVA: 0x00084668 File Offset: 0x00082868
		internal virtual MyComboBox ComboModLocalNameStyle
		{
			[CompilerGenerated]
			get
			{
				return this.parserProperty;
			}
			[CompilerGenerated]
			set
			{
				SelectionChangedEventHandler value2 = (PageSetupSystem._Closure$__.$IR54-7 == null) ? (PageSetupSystem._Closure$__.$IR54-7 = delegate(object sender, SelectionChangedEventArgs e)
				{
					PageSetupSystem.ComboChange((MyComboBox)sender, e);
				}) : PageSetupSystem._Closure$__.$IR54-7;
				MyComboBox myComboBox = this.parserProperty;
				if (myComboBox != null)
				{
					myComboBox.SelectionChanged -= value2;
				}
				this.parserProperty = value;
				myComboBox = this.parserProperty;
				if (myComboBox != null)
				{
					myComboBox.SelectionChanged += value2;
				}
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x0000B1D0 File Offset: 0x000093D0
		// (set) Token: 0x060012D9 RID: 4825 RVA: 0x000846C4 File Offset: 0x000828C4
		internal virtual MyCheckBox CheckDownloadIgnoreQuilt
		{
			[CompilerGenerated]
			get
			{
				return this._BroadcasterProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupSystem._Closure$__.$IR58-8 == null) ? (PageSetupSystem._Closure$__.$IR58-8 = delegate(object a0, bool a1)
				{
					PageSetupSystem.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupSystem._Closure$__.$IR58-8;
				MyCheckBox broadcasterProperty = this._BroadcasterProperty;
				if (broadcasterProperty != null)
				{
					broadcasterProperty.PublishConfig(obj);
				}
				this._BroadcasterProperty = value;
				broadcasterProperty = this._BroadcasterProperty;
				if (broadcasterProperty != null)
				{
					broadcasterProperty.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x0000B1D8 File Offset: 0x000093D8
		// (set) Token: 0x060012DB RID: 4827 RVA: 0x00084720 File Offset: 0x00082920
		internal virtual MyCheckBox CheckUpdateRelease
		{
			[CompilerGenerated]
			get
			{
				return this._FieldProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupSystem._Closure$__.$IR62-9 == null) ? (PageSetupSystem._Closure$__.$IR62-9 = delegate(object a0, bool a1)
				{
					PageSetupSystem.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupSystem._Closure$__.$IR62-9;
				MyCheckBox fieldProperty = this._FieldProperty;
				if (fieldProperty != null)
				{
					fieldProperty.PublishConfig(obj);
				}
				this._FieldProperty = value;
				fieldProperty = this._FieldProperty;
				if (fieldProperty != null)
				{
					fieldProperty.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x0000B1E0 File Offset: 0x000093E0
		// (set) Token: 0x060012DD RID: 4829 RVA: 0x0008477C File Offset: 0x0008297C
		internal virtual MyCheckBox CheckUpdateSnapshot
		{
			[CompilerGenerated]
			get
			{
				return this._ReaderProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupSystem._Closure$__.$IR66-10 == null) ? (PageSetupSystem._Closure$__.$IR66-10 = delegate(object a0, bool a1)
				{
					PageSetupSystem.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupSystem._Closure$__.$IR66-10;
				MyCheckBox readerProperty = this._ReaderProperty;
				if (readerProperty != null)
				{
					readerProperty.PublishConfig(obj);
				}
				this._ReaderProperty = value;
				readerProperty = this._ReaderProperty;
				if (readerProperty != null)
				{
					readerProperty.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x0000B1E8 File Offset: 0x000093E8
		// (set) Token: 0x060012DF RID: 4831 RVA: 0x000847D8 File Offset: 0x000829D8
		internal virtual MyCheckBox CheckHelpChinese
		{
			[CompilerGenerated]
			get
			{
				return this.m_ClientProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupSystem._Closure$__.$IR70-11 == null) ? (PageSetupSystem._Closure$__.$IR70-11 = delegate(object a0, bool a1)
				{
					PageSetupSystem.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupSystem._Closure$__.$IR70-11;
				MyCheckBox clientProperty = this.m_ClientProperty;
				if (clientProperty != null)
				{
					clientProperty.PublishConfig(obj);
				}
				this.m_ClientProperty = value;
				clientProperty = this.m_ClientProperty;
				if (clientProperty != null)
				{
					clientProperty.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x0000B1F0 File Offset: 0x000093F0
		// (set) Token: 0x060012E1 RID: 4833 RVA: 0x00084834 File Offset: 0x00082A34
		internal virtual MyComboBox ComboSystemUpdate
		{
			[CompilerGenerated]
			get
			{
				return this.m_ConfigProperty;
			}
			[CompilerGenerated]
			set
			{
				SelectionChangedEventHandler value2 = (PageSetupSystem._Closure$__.$IR74-12 == null) ? (PageSetupSystem._Closure$__.$IR74-12 = delegate(object sender, SelectionChangedEventArgs e)
				{
					PageSetupSystem.ComboChange((MyComboBox)sender, e);
				}) : PageSetupSystem._Closure$__.$IR74-12;
				SelectionChangedEventHandler value3 = new SelectionChangedEventHandler(this.ComboSystemUpdate_SelectionChanged);
				MyComboBox configProperty = this.m_ConfigProperty;
				if (configProperty != null)
				{
					configProperty.SelectionChanged -= value2;
					configProperty.SelectionChanged -= value3;
				}
				this.m_ConfigProperty = value;
				configProperty = this.m_ConfigProperty;
				if (configProperty != null)
				{
					configProperty.SelectionChanged += value2;
					configProperty.SelectionChanged += value3;
				}
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x0000B1F8 File Offset: 0x000093F8
		// (set) Token: 0x060012E3 RID: 4835 RVA: 0x0000B200 File Offset: 0x00009400
		internal virtual MyComboBoxItem ItemSystemUpdateDownload { get; set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x0000B209 File Offset: 0x00009409
		// (set) Token: 0x060012E5 RID: 4837 RVA: 0x000848AC File Offset: 0x00082AAC
		internal virtual MyComboBox ComboSystemActivity
		{
			[CompilerGenerated]
			get
			{
				return this.m_MapperProperty;
			}
			[CompilerGenerated]
			set
			{
				SelectionChangedEventHandler value2 = (PageSetupSystem._Closure$__.$IR82-13 == null) ? (PageSetupSystem._Closure$__.$IR82-13 = delegate(object sender, SelectionChangedEventArgs e)
				{
					PageSetupSystem.ComboChange((MyComboBox)sender, e);
				}) : PageSetupSystem._Closure$__.$IR82-13;
				SelectionChangedEventHandler value3 = new SelectionChangedEventHandler(this.ComboSystemActivity_SelectionChanged);
				MyComboBox mapperProperty = this.m_MapperProperty;
				if (mapperProperty != null)
				{
					mapperProperty.SelectionChanged -= value2;
					mapperProperty.SelectionChanged -= value3;
				}
				this.m_MapperProperty = value;
				mapperProperty = this.m_MapperProperty;
				if (mapperProperty != null)
				{
					mapperProperty.SelectionChanged += value2;
					mapperProperty.SelectionChanged += value3;
				}
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x0000B211 File Offset: 0x00009411
		// (set) Token: 0x060012E7 RID: 4839 RVA: 0x00084924 File Offset: 0x00082B24
		internal virtual MyTextBox TextSystemCache
		{
			[CompilerGenerated]
			get
			{
				return this.threadProperty;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageSetupSystem._Closure$__.$IR86-14 == null) ? (PageSetupSystem._Closure$__.$IR86-14 = delegate(object sender, RoutedEventArgs e)
				{
					PageSetupSystem.TextBoxChange((MyTextBox)sender, e);
				}) : PageSetupSystem._Closure$__.$IR86-14;
				MyTextBox myTextBox = this.threadProperty;
				if (myTextBox != null)
				{
					myTextBox.DestroyReader(value2);
				}
				this.threadProperty = value;
				myTextBox = this.threadProperty;
				if (myTextBox != null)
				{
					myTextBox.SetupReader(value2);
				}
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x0000B219 File Offset: 0x00009419
		// (set) Token: 0x060012E9 RID: 4841 RVA: 0x00084980 File Offset: 0x00082B80
		internal virtual MyButton BtnSystemUpdate
		{
			[CompilerGenerated]
			get
			{
				return this._PropertyProperty;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnSystemUpdate_Click);
				MyButton propertyProperty = this._PropertyProperty;
				if (propertyProperty != null)
				{
					propertyProperty.Click -= value2;
				}
				this._PropertyProperty = value;
				propertyProperty = this._PropertyProperty;
				if (propertyProperty != null)
				{
					propertyProperty.Click += value2;
				}
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x0000B221 File Offset: 0x00009421
		// (set) Token: 0x060012EB RID: 4843 RVA: 0x000849C4 File Offset: 0x00082BC4
		internal virtual MyButton BtnSystemSettingExp
		{
			[CompilerGenerated]
			get
			{
				return this.m_ComposerProperty;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnSystemSettingExp_Click);
				MyButton composerProperty = this.m_ComposerProperty;
				if (composerProperty != null)
				{
					composerProperty.Click -= value2;
				}
				this.m_ComposerProperty = value;
				composerProperty = this.m_ComposerProperty;
				if (composerProperty != null)
				{
					composerProperty.Click += value2;
				}
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x0000B229 File Offset: 0x00009429
		// (set) Token: 0x060012ED RID: 4845 RVA: 0x00084A08 File Offset: 0x00082C08
		internal virtual MyButton BtnSystemSettingImp
		{
			[CompilerGenerated]
			get
			{
				return this._IteratorProperty;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnSystemSettingImp_Click);
				MyButton iteratorProperty = this._IteratorProperty;
				if (iteratorProperty != null)
				{
					iteratorProperty.Click -= value2;
				}
				this._IteratorProperty = value;
				iteratorProperty = this._IteratorProperty;
				if (iteratorProperty != null)
				{
					iteratorProperty.Click += value2;
				}
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x0000B231 File Offset: 0x00009431
		// (set) Token: 0x060012EF RID: 4847 RVA: 0x0000B239 File Offset: 0x00009439
		internal virtual Grid PanDonate { get; set; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x0000B242 File Offset: 0x00009442
		// (set) Token: 0x060012F1 RID: 4849 RVA: 0x00084A4C File Offset: 0x00082C4C
		internal virtual MyButton BtnSystemIdentify
		{
			[CompilerGenerated]
			get
			{
				return this._TestProperty;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnSystemIdentify_Click);
				MyButton testProperty = this._TestProperty;
				if (testProperty != null)
				{
					testProperty.Click -= value2;
				}
				this._TestProperty = value;
				testProperty = this._TestProperty;
				if (testProperty != null)
				{
					testProperty.Click += value2;
				}
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x0000B24A File Offset: 0x0000944A
		// (set) Token: 0x060012F3 RID: 4851 RVA: 0x00084A90 File Offset: 0x00082C90
		internal virtual MyButton BtnSystemUnlock
		{
			[CompilerGenerated]
			get
			{
				return this.mapProperty;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnSystemUnlock_Click);
				MyButton myButton = this.mapProperty;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.mapProperty = value;
				myButton = this.mapProperty;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x0000B252 File Offset: 0x00009452
		// (set) Token: 0x060012F5 RID: 4853 RVA: 0x0000B25A File Offset: 0x0000945A
		internal virtual MyCard CardDebug { get; set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x0000B263 File Offset: 0x00009463
		// (set) Token: 0x060012F7 RID: 4855 RVA: 0x0000B26B File Offset: 0x0000946B
		internal virtual Grid PanDebugAnim { get; set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x0000B274 File Offset: 0x00009474
		// (set) Token: 0x060012F9 RID: 4857 RVA: 0x00084AD4 File Offset: 0x00082CD4
		internal virtual MySlider SliderDebugAnim
		{
			[CompilerGenerated]
			get
			{
				return this._SpecificationProperty;
			}
			[CompilerGenerated]
			set
			{
				MySlider.ChangeEventHandler obj = (PageSetupSystem._Closure$__.$IR122-15 == null) ? (PageSetupSystem._Closure$__.$IR122-15 = delegate(object a0, bool a1)
				{
					PageSetupSystem.SliderChange((MySlider)a0, a1);
				}) : PageSetupSystem._Closure$__.$IR122-15;
				MySlider specificationProperty = this._SpecificationProperty;
				if (specificationProperty != null)
				{
					specificationProperty.WriteTests(obj);
				}
				this._SpecificationProperty = value;
				specificationProperty = this._SpecificationProperty;
				if (specificationProperty != null)
				{
					specificationProperty.FillTests(obj);
				}
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x0000B27C File Offset: 0x0000947C
		// (set) Token: 0x060012FB RID: 4859 RVA: 0x00084B30 File Offset: 0x00082D30
		internal virtual MyCheckBox CheckDebugSkipCopy
		{
			[CompilerGenerated]
			get
			{
				return this._MockProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupSystem._Closure$__.$IR126-16 == null) ? (PageSetupSystem._Closure$__.$IR126-16 = delegate(object a0, bool a1)
				{
					PageSetupSystem.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupSystem._Closure$__.$IR126-16;
				MyCheckBox mockProperty = this._MockProperty;
				if (mockProperty != null)
				{
					mockProperty.PublishConfig(obj);
				}
				this._MockProperty = value;
				mockProperty = this._MockProperty;
				if (mockProperty != null)
				{
					mockProperty.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x0000B284 File Offset: 0x00009484
		// (set) Token: 0x060012FD RID: 4861 RVA: 0x00084B8C File Offset: 0x00082D8C
		internal virtual MyCheckBox CheckDebugMode
		{
			[CompilerGenerated]
			get
			{
				return this.requestProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupSystem._Closure$__.$IR130-17 == null) ? (PageSetupSystem._Closure$__.$IR130-17 = delegate(object a0, bool a1)
				{
					PageSetupSystem.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupSystem._Closure$__.$IR130-17;
				MyCheckBox.ChangeEventHandler obj2 = delegate(object a0, bool a1)
				{
					this.CheckDebugMode_Change();
				};
				MyCheckBox myCheckBox = this.requestProperty;
				if (myCheckBox != null)
				{
					myCheckBox.PublishConfig(obj);
					myCheckBox.PublishConfig(obj2);
				}
				this.requestProperty = value;
				myCheckBox = this.requestProperty;
				if (myCheckBox != null)
				{
					myCheckBox.InstantiateConfig(obj);
					myCheckBox.InstantiateConfig(obj2);
				}
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x0000B28C File Offset: 0x0000948C
		// (set) Token: 0x060012FF RID: 4863 RVA: 0x00084C04 File Offset: 0x00082E04
		internal virtual MyCheckBox CheckDebugDelay
		{
			[CompilerGenerated]
			get
			{
				return this._DicProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupSystem._Closure$__.$IR134-19 == null) ? (PageSetupSystem._Closure$__.$IR134-19 = delegate(object a0, bool a1)
				{
					PageSetupSystem.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupSystem._Closure$__.$IR134-19;
				MyCheckBox dicProperty = this._DicProperty;
				if (dicProperty != null)
				{
					dicProperty.PublishConfig(obj);
				}
				this._DicProperty = value;
				dicProperty = this._DicProperty;
				if (dicProperty != null)
				{
					dicProperty.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00084C60 File Offset: 0x00082E60
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_HelperProperty)
			{
				this.m_HelperProperty = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagesetup/pagesetupsystem.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00084C90 File Offset: 0x00082E90
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
				this.ComboDownloadVersion = (MyComboBox)target;
				return;
			}
			if (connectionId == 4)
			{
				this.SliderDownloadThread = (MySlider)target;
				return;
			}
			if (connectionId == 5)
			{
				this.SliderDownloadSpeed = (MySlider)target;
				return;
			}
			if (connectionId == 6)
			{
				this.CheckDownloadCert = (MyCheckBox)target;
				return;
			}
			if (connectionId == 7)
			{
				this.ComboDownloadMod = (MyComboBox)target;
				return;
			}
			if (connectionId == 8)
			{
				this.ComboDownloadTranslate = (MyComboBox)target;
				return;
			}
			if (connectionId == 9)
			{
				this.ComboModLocalNameStyle = (MyComboBox)target;
				return;
			}
			if (connectionId == 10)
			{
				this.CheckDownloadIgnoreQuilt = (MyCheckBox)target;
				return;
			}
			if (connectionId == 11)
			{
				this.CheckUpdateRelease = (MyCheckBox)target;
				return;
			}
			if (connectionId == 12)
			{
				this.CheckUpdateSnapshot = (MyCheckBox)target;
				return;
			}
			if (connectionId == 13)
			{
				this.CheckHelpChinese = (MyCheckBox)target;
				return;
			}
			if (connectionId == 14)
			{
				this.ComboSystemUpdate = (MyComboBox)target;
				return;
			}
			if (connectionId == 15)
			{
				this.ItemSystemUpdateDownload = (MyComboBoxItem)target;
				return;
			}
			if (connectionId == 16)
			{
				this.ComboSystemActivity = (MyComboBox)target;
				return;
			}
			if (connectionId == 17)
			{
				this.TextSystemCache = (MyTextBox)target;
				return;
			}
			if (connectionId == 18)
			{
				this.BtnSystemUpdate = (MyButton)target;
				return;
			}
			if (connectionId == 19)
			{
				this.BtnSystemSettingExp = (MyButton)target;
				return;
			}
			if (connectionId == 20)
			{
				this.BtnSystemSettingImp = (MyButton)target;
				return;
			}
			if (connectionId == 21)
			{
				this.PanDonate = (Grid)target;
				return;
			}
			if (connectionId == 22)
			{
				this.BtnSystemIdentify = (MyButton)target;
				return;
			}
			if (connectionId == 23)
			{
				this.BtnSystemUnlock = (MyButton)target;
				return;
			}
			if (connectionId == 24)
			{
				this.CardDebug = (MyCard)target;
				return;
			}
			if (connectionId == 25)
			{
				this.PanDebugAnim = (Grid)target;
				return;
			}
			if (connectionId == 26)
			{
				this.SliderDebugAnim = (MySlider)target;
				return;
			}
			if (connectionId == 27)
			{
				this.CheckDebugSkipCopy = (MyCheckBox)target;
				return;
			}
			if (connectionId == 28)
			{
				this.CheckDebugMode = (MyCheckBox)target;
				return;
			}
			if (connectionId == 29)
			{
				this.CheckDebugDelay = (MyCheckBox)target;
				return;
			}
			this.m_HelperProperty = true;
		}

		// Token: 0x0400099C RID: 2460
		private bool policyThread;

		// Token: 0x0400099D RID: 2461
		[CompilerGenerated]
		[AccessedThroughProperty("PanBack")]
		private MyScrollViewer orderThread;

		// Token: 0x0400099E RID: 2462
		[CompilerGenerated]
		[AccessedThroughProperty("PanMain")]
		private StackPanel m_ProducerThread;

		// Token: 0x0400099F RID: 2463
		[AccessedThroughProperty("ComboDownloadVersion")]
		[CompilerGenerated]
		private MyComboBox schemaThread;

		// Token: 0x040009A0 RID: 2464
		[AccessedThroughProperty("SliderDownloadThread")]
		[CompilerGenerated]
		private MySlider descriptorThread;

		// Token: 0x040009A1 RID: 2465
		[AccessedThroughProperty("SliderDownloadSpeed")]
		[CompilerGenerated]
		private MySlider _PublisherThread;

		// Token: 0x040009A2 RID: 2466
		[AccessedThroughProperty("CheckDownloadCert")]
		[CompilerGenerated]
		private MyCheckBox definitionThread;

		// Token: 0x040009A3 RID: 2467
		[AccessedThroughProperty("ComboDownloadMod")]
		[CompilerGenerated]
		private MyComboBox _StrategyThread;

		// Token: 0x040009A4 RID: 2468
		[AccessedThroughProperty("ComboDownloadTranslate")]
		[CompilerGenerated]
		private MyComboBox procThread;

		// Token: 0x040009A5 RID: 2469
		[CompilerGenerated]
		[AccessedThroughProperty("ComboModLocalNameStyle")]
		private MyComboBox parserProperty;

		// Token: 0x040009A6 RID: 2470
		[CompilerGenerated]
		[AccessedThroughProperty("CheckDownloadIgnoreQuilt")]
		private MyCheckBox _BroadcasterProperty;

		// Token: 0x040009A7 RID: 2471
		[CompilerGenerated]
		[AccessedThroughProperty("CheckUpdateRelease")]
		private MyCheckBox _FieldProperty;

		// Token: 0x040009A8 RID: 2472
		[CompilerGenerated]
		[AccessedThroughProperty("CheckUpdateSnapshot")]
		private MyCheckBox _ReaderProperty;

		// Token: 0x040009A9 RID: 2473
		[AccessedThroughProperty("CheckHelpChinese")]
		[CompilerGenerated]
		private MyCheckBox m_ClientProperty;

		// Token: 0x040009AA RID: 2474
		[AccessedThroughProperty("ComboSystemUpdate")]
		[CompilerGenerated]
		private MyComboBox m_ConfigProperty;

		// Token: 0x040009AB RID: 2475
		[CompilerGenerated]
		[AccessedThroughProperty("ItemSystemUpdateDownload")]
		private MyComboBoxItem m_TestsProperty;

		// Token: 0x040009AC RID: 2476
		[AccessedThroughProperty("ComboSystemActivity")]
		[CompilerGenerated]
		private MyComboBox m_MapperProperty;

		// Token: 0x040009AD RID: 2477
		[AccessedThroughProperty("TextSystemCache")]
		[CompilerGenerated]
		private MyTextBox threadProperty;

		// Token: 0x040009AE RID: 2478
		[AccessedThroughProperty("BtnSystemUpdate")]
		[CompilerGenerated]
		private MyButton _PropertyProperty;

		// Token: 0x040009AF RID: 2479
		[CompilerGenerated]
		[AccessedThroughProperty("BtnSystemSettingExp")]
		private MyButton m_ComposerProperty;

		// Token: 0x040009B0 RID: 2480
		[CompilerGenerated]
		[AccessedThroughProperty("BtnSystemSettingImp")]
		private MyButton _IteratorProperty;

		// Token: 0x040009B1 RID: 2481
		[CompilerGenerated]
		[AccessedThroughProperty("PanDonate")]
		private Grid m_RepositoryProperty;

		// Token: 0x040009B2 RID: 2482
		[CompilerGenerated]
		[AccessedThroughProperty("BtnSystemIdentify")]
		private MyButton _TestProperty;

		// Token: 0x040009B3 RID: 2483
		[AccessedThroughProperty("BtnSystemUnlock")]
		[CompilerGenerated]
		private MyButton mapProperty;

		// Token: 0x040009B4 RID: 2484
		[AccessedThroughProperty("CardDebug")]
		[CompilerGenerated]
		private MyCard _ErrorProperty;

		// Token: 0x040009B5 RID: 2485
		[AccessedThroughProperty("PanDebugAnim")]
		[CompilerGenerated]
		private Grid contextProperty;

		// Token: 0x040009B6 RID: 2486
		[AccessedThroughProperty("SliderDebugAnim")]
		[CompilerGenerated]
		private MySlider _SpecificationProperty;

		// Token: 0x040009B7 RID: 2487
		[CompilerGenerated]
		[AccessedThroughProperty("CheckDebugSkipCopy")]
		private MyCheckBox _MockProperty;

		// Token: 0x040009B8 RID: 2488
		[CompilerGenerated]
		[AccessedThroughProperty("CheckDebugMode")]
		private MyCheckBox requestProperty;

		// Token: 0x040009B9 RID: 2489
		[CompilerGenerated]
		[AccessedThroughProperty("CheckDebugDelay")]
		private MyCheckBox _DicProperty;

		// Token: 0x040009BA RID: 2490
		private bool m_HelperProperty;
	}
}
