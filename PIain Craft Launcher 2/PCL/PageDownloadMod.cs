using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020000E0 RID: 224
	[DesignerGenerated]
	public class PageDownloadMod : MyPageRight, IComponentConnector
	{
		// Token: 0x060006DB RID: 1755 RVA: 0x00005876 File Offset: 0x00003A76
		public PageDownloadMod()
		{
			base.Loaded += new RoutedEventHandler(this.PageDownloadMod_Inited);
			this.dispatcherField = false;
			this.InitializeComponent();
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0003E464 File Offset: 0x0003C664
		private void PageDownloadMod_Inited(object sender, EventArgs e)
		{
			if (PageDownloadMod.m_PageField != null)
			{
				this.ResetFilter();
				this.TextSearchVersion.Text = PageDownloadMod.m_PageField.Version._ConnectionMap;
				VB$AnonymousDelegate_5<string, MyComboBoxItem> vb$AnonymousDelegate_ = delegate(string Name)
				{
					try
					{
						foreach (object obj in ((IEnumerable)this.ComboSearchLoader.Items))
						{
							MyComboBoxItem myComboBoxItem = (MyComboBoxItem)obj;
							if (Operators.ConditionalCompareObjectEqual(myComboBoxItem.Content, Name, false))
							{
								return myComboBoxItem;
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
					return (MyComboBoxItem)this.ComboSearchLoader.Items[0];
				};
				if (PageDownloadMod.m_PageField.Version.m_StructMap)
				{
					this.ComboSearchLoader.SelectedItem = vb$AnonymousDelegate_("Forge");
				}
				else if (PageDownloadMod.m_PageField.Version._CandidateMap)
				{
					this.ComboSearchLoader.SelectedItem = vb$AnonymousDelegate_("Fabric");
				}
				else if (PageDownloadMod.m_PageField.Version._ValMap)
				{
					this.ComboSearchLoader.SelectedItem = vb$AnonymousDelegate_("NeoForge");
				}
				PageDownloadMod.m_PageField = null;
				if (this.dispatcherField)
				{
					this.StartNewSearch();
				}
				base.PanScroll.ScrollToHome();
			}
			if (!this.dispatcherField)
			{
				this.dispatcherField = true;
				base.PageLoaderInit(this.Load, this.PanLoad, this.PanContent, this.PanAlways, PageDownloadMod.interceptorField, delegate(ModLoader.LoaderBase a0)
				{
					this.Load_OnFinish();
				}, new Func<object>(PageDownloadMod.LoaderInput), true);
				if (ModDownloadLib.m_StructField == -1)
				{
					ModDownloadLib.m_StructField = Math.Max(ModDownloadLib.m_StructField, int.Parse(((MyComboBoxItem)this.TextSearchVersion.Items[1]).Content.ToString().Split(".")[1]));
				}
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0003E5D8 File Offset: 0x0003C7D8
		private static ModComp.CompProjectRequest LoaderInput()
		{
			checked
			{
				ModComp.CompProjectRequest compProjectRequest = new ModComp.CompProjectRequest(ModComp.CompType.Mod, PageDownloadMod.containerField, (PageDownloadMod.m_ParamsField + 1) * 40);
				if (ModMain.m_ExceptionIterator != null)
				{
					ModComp.CompModLoaderType compModLoaderType = (ModComp.CompModLoaderType)Math.Round(ModBase.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(ModMain.m_ExceptionIterator.ComboSearchLoader.SelectedItem, null, "Tag", new object[0], null, null, null))));
					string text = (Operators.CompareString(ModMain.m_ExceptionIterator.TextSearchVersion.Text, "全部 (也可自行输入)", false) == 0) ? null : ((ModMain.m_ExceptionIterator.TextSearchVersion.Text.Contains(".") || ModMain.m_ExceptionIterator.TextSearchVersion.Text.Contains("w")) ? ModMain.m_ExceptionIterator.TextSearchVersion.Text : null);
					if (text != null && text.Contains(".") && ModBase.Val(text.Split(".")[1]) < 14.0 && compModLoaderType == ModComp.CompModLoaderType.Forge)
					{
						compModLoaderType = ModComp.CompModLoaderType.Any;
					}
					ModComp.CompProjectRequest compProjectRequest2 = compProjectRequest;
					compProjectRequest2._RecordRepository = ModMain.m_ExceptionIterator.TextSearchName.Text;
					compProjectRequest2.m_ParameterRepository = text;
					compProjectRequest2.Tag = Conversions.ToString(NewLateBinding.LateGet(ModMain.m_ExceptionIterator.ComboSearchTag.SelectedItem, null, "Tag", new object[0], null, null, null));
					compProjectRequest2.processRepository = compModLoaderType;
					compProjectRequest2.Source = (ModComp.CompSourceType)Math.Round(ModBase.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(ModMain.m_ExceptionIterator.ComboSearchSource.SelectedItem, null, "Tag", new object[0], null, null, null))));
				}
				return compProjectRequest;
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0003E760 File Offset: 0x0003C960
		private void Load_OnFinish()
		{
			checked
			{
				try
				{
					ModBase.Log(string.Format("[Comp] 开始可视化 Mod 列表，已储藏 {0} 个结果，当前在第 {1} 页", PageDownloadMod.containerField._CreatorRepository.Count, PageDownloadMod.m_ParamsField + 1), ModBase.LogLevel.Normal, "出现错误");
					this.PanProjects.Children.Clear();
					int num = Math.Min(PageDownloadMod.m_ParamsField * 40, PageDownloadMod.containerField._CreatorRepository.Count - 1);
					int num2 = Math.Min((PageDownloadMod.m_ParamsField + 1) * 40 - 1, PageDownloadMod.containerField._CreatorRepository.Count - 1);
					for (int i = num; i <= num2; i++)
					{
						this.PanProjects.Children.Add(PageDownloadMod.containerField._CreatorRepository[i].ToCompItem(PageDownloadMod.interceptorField.Input.m_ParameterRepository == null, PageDownloadMod.interceptorField.Input.processRepository == ModComp.CompModLoaderType.Any));
					}
					this.CardPages.Visibility = ((PageDownloadMod.containerField._CreatorRepository.Count > 40 || PageDownloadMod.containerField.m_ServiceRepository < PageDownloadMod.containerField.m_InvocationRepository || PageDownloadMod.containerField.m_ProxyRepository < PageDownloadMod.containerField.m_MessageRepository) ? Visibility.Visible : Visibility.Collapsed);
					this.LabPage.Text = Conversions.ToString(PageDownloadMod.m_ParamsField + 1);
					this.BtnPageFirst.IsEnabled = (PageDownloadMod.m_ParamsField > 1);
					this.BtnPageFirst.Opacity = ((PageDownloadMod.m_ParamsField > 1) ? 1.0 : 0.2);
					this.BtnPageLeft.IsEnabled = (PageDownloadMod.m_ParamsField > 0);
					this.BtnPageLeft.Opacity = ((PageDownloadMod.m_ParamsField > 0) ? 1.0 : 0.2);
					bool flag = PageDownloadMod.containerField._CreatorRepository.Count > 40 * (PageDownloadMod.m_ParamsField + 1) || PageDownloadMod.containerField.m_ServiceRepository < PageDownloadMod.containerField.m_InvocationRepository || PageDownloadMod.containerField.m_ProxyRepository < PageDownloadMod.containerField.m_MessageRepository;
					this.BtnPageRight.IsEnabled = flag;
					this.BtnPageRight.Opacity = (flag ? 1.0 : 0.2);
					if (PageDownloadMod.containerField._InitializerRepository == null)
					{
						this.HintError.Visibility = Visibility.Collapsed;
					}
					else
					{
						this.HintError.Visibility = Visibility.Visible;
						this.HintError.Text = PageDownloadMod.containerField._InitializerRepository;
					}
					this.PanBack.ScrollToTop();
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "可视化 Mod 列表出错", ModBase.LogLevel.Feedback, "出现错误");
				}
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0003EA20 File Offset: 0x0003CC20
		private void Load_State(object sender, MyLoading.MyLoadingState state, MyLoading.MyLoadingState oldState)
		{
			ModBase.LoadState state2 = PageDownloadMod.interceptorField.State;
			if (state2 == ModBase.LoadState.Failed)
			{
				string text = "";
				if (PageDownloadMod.interceptorField.Error != null)
				{
					text = PageDownloadMod.interceptorField.Error.Message;
				}
				if (text.Contains("不是有效的 json 文件"))
				{
					ModBase.Log("[Download] 下载的 Mod 列表 json 文件损坏，已自动重试", ModBase.LogLevel.Debug, "出现错误");
					base.PageLoaderRestart(null, true);
				}
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0000589E File Offset: 0x00003A9E
		private void BtnPageFirst_Click(object sender, RoutedEventArgs e)
		{
			this.ChangePage(0);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000058A7 File Offset: 0x00003AA7
		private void BtnPageLeft_Click(object sender, RoutedEventArgs e)
		{
			this.ChangePage(checked(PageDownloadMod.m_ParamsField - 1));
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000058B6 File Offset: 0x00003AB6
		private void BtnPageRight_Click(object sender, RoutedEventArgs e)
		{
			this.ChangePage(checked(PageDownloadMod.m_ParamsField + 1));
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0003EA84 File Offset: 0x0003CC84
		private void ChangePage(int NewPage)
		{
			this.CardPages.IsEnabled = false;
			PageDownloadMod.m_ParamsField = NewPage;
			ModMain._ProcessIterator.BackToTop();
			ModBase.Log(string.Format("[Download] Mod 切换到第 {0} 页", checked(PageDownloadMod.m_ParamsField + 1)), ModBase.LogLevel.Normal, "出现错误");
			ModBase.RunInThread(delegate
			{
				Thread.Sleep(100);
				ModBase.RunInUi(delegate()
				{
					this.CardPages.IsEnabled = true;
				}, false);
				PageDownloadMod.interceptorField.Start(null, false);
			});
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0003EAE0 File Offset: 0x0003CCE0
		private void StartNewSearch()
		{
			PageDownloadMod.m_ParamsField = 0;
			ModLoader.LoaderTask<ModComp.CompProjectRequest, int> loaderTask = PageDownloadMod.interceptorField;
			object obj = PageDownloadMod.LoaderInput();
			if (loaderTask.ShouldStart(ref obj, false, false))
			{
				PageDownloadMod.containerField = new ModComp.CompProjectStorage();
			}
			PageDownloadMod.interceptorField.Start(null, false);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x000058C5 File Offset: 0x00003AC5
		private void EnterTrigger(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.StartNewSearch();
			}
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0003EB20 File Offset: 0x0003CD20
		private void ResetFilter()
		{
			this.TextSearchName.Text = "";
			this.TextSearchVersion.Text = "全部 (也可自行输入)";
			this.TextSearchVersion.SelectedIndex = 0;
			this.ComboSearchSource.SelectedIndex = 0;
			this.ComboSearchTag.SelectedIndex = 0;
			this.ComboSearchLoader.SelectedIndex = 0;
			PageDownloadMod.interceptorField.LastFinishedTime = 0L;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x000058D6 File Offset: 0x00003AD6
		private void TextSearchVersion_TextChanged()
		{
			if (!this.TextSearchVersion.IsDropDownOpen)
			{
				this.UpdateSearchLoaderVisibility();
			}
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0003EB90 File Offset: 0x0003CD90
		private void UpdateSearchLoaderVisibility()
		{
			if (!this.TextSearchVersion.Text.Contains(".") && !this.TextSearchVersion.Text.Contains("w"))
			{
				this.ComboSearchLoader.Visibility = Visibility.Collapsed;
				Grid.SetColumnSpan(this.TextSearchVersion, 2);
				this.ComboSearchLoader.SelectedIndex = 0;
				return;
			}
			this.ComboSearchLoader.Visibility = Visibility.Visible;
			Grid.SetColumnSpan(this.TextSearchVersion, 1);
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x000058EB File Offset: 0x00003AEB
		// (set) Token: 0x060006EA RID: 1770 RVA: 0x000058F3 File Offset: 0x00003AF3
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x000058FC File Offset: 0x00003AFC
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x00005904 File Offset: 0x00003B04
		internal virtual MyCard PanAlways { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0000590D File Offset: 0x00003B0D
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x0003EC08 File Offset: 0x0003CE08
		internal virtual MyTextBox TextSearchName
		{
			[CompilerGenerated]
			get
			{
				return this._RecordField;
			}
			[CompilerGenerated]
			set
			{
				KeyEventHandler value2 = new KeyEventHandler(this.EnterTrigger);
				MyTextBox recordField = this._RecordField;
				if (recordField != null)
				{
					recordField.KeyDown -= value2;
				}
				this._RecordField = value;
				recordField = this._RecordField;
				if (recordField != null)
				{
					recordField.KeyDown += value2;
				}
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x00005915 File Offset: 0x00003B15
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x0000591D File Offset: 0x00003B1D
		internal virtual MyComboBox ComboSearchSource { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00005926 File Offset: 0x00003B26
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x0003EC4C File Offset: 0x0003CE4C
		internal virtual MyComboBox TextSearchVersion
		{
			[CompilerGenerated]
			get
			{
				return this.m_InvocationField;
			}
			[CompilerGenerated]
			set
			{
				KeyEventHandler value2 = new KeyEventHandler(this.EnterTrigger);
				MyComboBox.TextChangedEventHandler obj = delegate(object sender, TextChangedEventArgs e)
				{
					this.TextSearchVersion_TextChanged();
				};
				EventHandler value3 = delegate(object sender, EventArgs e)
				{
					this.UpdateSearchLoaderVisibility();
				};
				MyComboBox invocationField = this.m_InvocationField;
				if (invocationField != null)
				{
					invocationField.KeyDown -= value2;
					invocationField.ValidateParser(obj);
					invocationField.DropDownClosed -= value3;
				}
				this.m_InvocationField = value;
				invocationField = this.m_InvocationField;
				if (invocationField != null)
				{
					invocationField.KeyDown += value2;
					invocationField.DeleteParser(obj);
					invocationField.DropDownClosed += value3;
				}
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0000592E File Offset: 0x00003B2E
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x00005936 File Offset: 0x00003B36
		internal virtual MyComboBox ComboSearchLoader { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0000593F File Offset: 0x00003B3F
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x00005947 File Offset: 0x00003B47
		internal virtual MyComboBox ComboSearchTag { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00005950 File Offset: 0x00003B50
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x0003ECC8 File Offset: 0x0003CEC8
		internal virtual MyButton BtnSearchRun
		{
			[CompilerGenerated]
			get
			{
				return this.m_CreatorField;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.StartNewSearch();
				};
				MyButton creatorField = this.m_CreatorField;
				if (creatorField != null)
				{
					creatorField.Click -= value2;
				}
				this.m_CreatorField = value;
				creatorField = this.m_CreatorField;
				if (creatorField != null)
				{
					creatorField.Click += value2;
				}
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00005958 File Offset: 0x00003B58
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x0003ED0C File Offset: 0x0003CF0C
		internal virtual MyButton BtnSearchReset
		{
			[CompilerGenerated]
			get
			{
				return this.initializerField;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.ResetFilter();
				};
				MyButton myButton = this.initializerField;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.initializerField = value;
				myButton = this.initializerField;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00005960 File Offset: 0x00003B60
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x00005968 File Offset: 0x00003B68
		internal virtual StackPanel PanContent { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00005971 File Offset: 0x00003B71
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x00005979 File Offset: 0x00003B79
		internal virtual MyHint HintError { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00005982 File Offset: 0x00003B82
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x0000598A File Offset: 0x00003B8A
		internal virtual MyCard CardProjects { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x00005993 File Offset: 0x00003B93
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x0000599B File Offset: 0x00003B9B
		internal virtual StackPanel PanProjects { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x000059A4 File Offset: 0x00003BA4
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x000059AC File Offset: 0x00003BAC
		internal virtual MyCard CardPages { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x000059B5 File Offset: 0x00003BB5
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x0003ED50 File Offset: 0x0003CF50
		internal virtual MyIconButton BtnPageFirst
		{
			[CompilerGenerated]
			get
			{
				return this._VisitorField;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = delegate(object sender, EventArgs e)
				{
					this.BtnPageFirst_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
				};
				MyIconButton visitorField = this._VisitorField;
				if (visitorField != null)
				{
					visitorField.Click -= value2;
				}
				this._VisitorField = value;
				visitorField = this._VisitorField;
				if (visitorField != null)
				{
					visitorField.Click += value2;
				}
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x000059BD File Offset: 0x00003BBD
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x0003ED94 File Offset: 0x0003CF94
		internal virtual MyIconButton BtnPageLeft
		{
			[CompilerGenerated]
			get
			{
				return this.valueField;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = delegate(object sender, EventArgs e)
				{
					this.BtnPageLeft_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
				};
				MyIconButton myIconButton = this.valueField;
				if (myIconButton != null)
				{
					myIconButton.Click -= value2;
				}
				this.valueField = value;
				myIconButton = this.valueField;
				if (myIconButton != null)
				{
					myIconButton.Click += value2;
				}
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x000059C5 File Offset: 0x00003BC5
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x000059CD File Offset: 0x00003BCD
		internal virtual TextBlock LabPage { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x000059D6 File Offset: 0x00003BD6
		// (set) Token: 0x0600070C RID: 1804 RVA: 0x0003EDD8 File Offset: 0x0003CFD8
		internal virtual MyIconButton BtnPageRight
		{
			[CompilerGenerated]
			get
			{
				return this.m_BridgeField;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = delegate(object sender, EventArgs e)
				{
					this.BtnPageRight_Click(RuntimeHelpers.GetObjectValue(sender), (RoutedEventArgs)e);
				};
				MyIconButton bridgeField = this.m_BridgeField;
				if (bridgeField != null)
				{
					bridgeField.Click -= value2;
				}
				this.m_BridgeField = value;
				bridgeField = this.m_BridgeField;
				if (bridgeField != null)
				{
					bridgeField.Click += value2;
				}
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x000059DE File Offset: 0x00003BDE
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x000059E6 File Offset: 0x00003BE6
		internal virtual MyCard PanLoad { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x000059EF File Offset: 0x00003BEF
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x0003EE1C File Offset: 0x0003D01C
		internal virtual MyLoading Load
		{
			[CompilerGenerated]
			get
			{
				return this.m_ReponseField;
			}
			[CompilerGenerated]
			set
			{
				MyLoading.StateChangedEventHandler obj = new MyLoading.StateChangedEventHandler(this.Load_State);
				MyLoading reponseField = this.m_ReponseField;
				if (reponseField != null)
				{
					reponseField.InterruptField(obj);
				}
				this.m_ReponseField = value;
				reponseField = this.m_ReponseField;
				if (reponseField != null)
				{
					reponseField.PrintField(obj);
				}
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0003EE60 File Offset: 0x0003D060
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.globalField)
			{
				this.globalField = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagedownload/pagedownloadmod.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0003EE90 File Offset: 0x0003D090
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
				this.PanAlways = (MyCard)target;
				return;
			}
			if (connectionId == 3)
			{
				this.TextSearchName = (MyTextBox)target;
				return;
			}
			if (connectionId == 4)
			{
				this.ComboSearchSource = (MyComboBox)target;
				return;
			}
			if (connectionId == 5)
			{
				this.TextSearchVersion = (MyComboBox)target;
				return;
			}
			if (connectionId == 6)
			{
				this.ComboSearchLoader = (MyComboBox)target;
				return;
			}
			if (connectionId == 7)
			{
				this.ComboSearchTag = (MyComboBox)target;
				return;
			}
			if (connectionId == 8)
			{
				this.BtnSearchRun = (MyButton)target;
				return;
			}
			if (connectionId == 9)
			{
				this.BtnSearchReset = (MyButton)target;
				return;
			}
			if (connectionId == 10)
			{
				this.PanContent = (StackPanel)target;
				return;
			}
			if (connectionId == 11)
			{
				this.HintError = (MyHint)target;
				return;
			}
			if (connectionId == 12)
			{
				this.CardProjects = (MyCard)target;
				return;
			}
			if (connectionId == 13)
			{
				this.PanProjects = (StackPanel)target;
				return;
			}
			if (connectionId == 14)
			{
				this.CardPages = (MyCard)target;
				return;
			}
			if (connectionId == 15)
			{
				this.BtnPageFirst = (MyIconButton)target;
				return;
			}
			if (connectionId == 16)
			{
				this.BtnPageLeft = (MyIconButton)target;
				return;
			}
			if (connectionId == 17)
			{
				this.LabPage = (TextBlock)target;
				return;
			}
			if (connectionId == 18)
			{
				this.BtnPageRight = (MyIconButton)target;
				return;
			}
			if (connectionId == 19)
			{
				this.PanLoad = (MyCard)target;
				return;
			}
			if (connectionId == 20)
			{
				this.Load = (MyLoading)target;
				return;
			}
			this.globalField = true;
		}

		// Token: 0x040003EF RID: 1007
		public static ModMinecraft.McVersion m_PageField = null;

		// Token: 0x040003F0 RID: 1008
		public static ModLoader.LoaderTask<ModComp.CompProjectRequest, int> interceptorField = new ModLoader.LoaderTask<ModComp.CompProjectRequest, int>("CompProject Mod", new Action<ModLoader.LoaderTask<ModComp.CompProjectRequest, int>>(ModComp.CompProjectsGet), new Func<ModComp.CompProjectRequest>(PageDownloadMod.LoaderInput), ThreadPriority.Normal)
		{
			ReloadTimeout = 60000
		};

		// Token: 0x040003F1 RID: 1009
		public static ModComp.CompProjectStorage containerField = new ModComp.CompProjectStorage();

		// Token: 0x040003F2 RID: 1010
		public static int m_ParamsField = 0;

		// Token: 0x040003F3 RID: 1011
		private bool dispatcherField;

		// Token: 0x040003F4 RID: 1012
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyScrollViewer m_ProcessField;

		// Token: 0x040003F5 RID: 1013
		[AccessedThroughProperty("PanAlways")]
		[CompilerGenerated]
		private MyCard m_ParameterField;

		// Token: 0x040003F6 RID: 1014
		[CompilerGenerated]
		[AccessedThroughProperty("TextSearchName")]
		private MyTextBox _RecordField;

		// Token: 0x040003F7 RID: 1015
		[CompilerGenerated]
		[AccessedThroughProperty("ComboSearchSource")]
		private MyComboBox m_ServiceField;

		// Token: 0x040003F8 RID: 1016
		[CompilerGenerated]
		[AccessedThroughProperty("TextSearchVersion")]
		private MyComboBox m_InvocationField;

		// Token: 0x040003F9 RID: 1017
		[AccessedThroughProperty("ComboSearchLoader")]
		[CompilerGenerated]
		private MyComboBox _ProxyField;

		// Token: 0x040003FA RID: 1018
		[CompilerGenerated]
		[AccessedThroughProperty("ComboSearchTag")]
		private MyComboBox m_MessageField;

		// Token: 0x040003FB RID: 1019
		[AccessedThroughProperty("BtnSearchRun")]
		[CompilerGenerated]
		private MyButton m_CreatorField;

		// Token: 0x040003FC RID: 1020
		[CompilerGenerated]
		[AccessedThroughProperty("BtnSearchReset")]
		private MyButton initializerField;

		// Token: 0x040003FD RID: 1021
		[CompilerGenerated]
		[AccessedThroughProperty("PanContent")]
		private StackPanel singletonField;

		// Token: 0x040003FE RID: 1022
		[AccessedThroughProperty("HintError")]
		[CompilerGenerated]
		private MyHint _RegField;

		// Token: 0x040003FF RID: 1023
		[AccessedThroughProperty("CardProjects")]
		[CompilerGenerated]
		private MyCard productField;

		// Token: 0x04000400 RID: 1024
		[AccessedThroughProperty("PanProjects")]
		[CompilerGenerated]
		private StackPanel _ListenerField;

		// Token: 0x04000401 RID: 1025
		[AccessedThroughProperty("CardPages")]
		[CompilerGenerated]
		private MyCard m_CollectionField;

		// Token: 0x04000402 RID: 1026
		[CompilerGenerated]
		[AccessedThroughProperty("BtnPageFirst")]
		private MyIconButton _VisitorField;

		// Token: 0x04000403 RID: 1027
		[CompilerGenerated]
		[AccessedThroughProperty("BtnPageLeft")]
		private MyIconButton valueField;

		// Token: 0x04000404 RID: 1028
		[AccessedThroughProperty("LabPage")]
		[CompilerGenerated]
		private TextBlock _ObjectField;

		// Token: 0x04000405 RID: 1029
		[CompilerGenerated]
		[AccessedThroughProperty("BtnPageRight")]
		private MyIconButton m_BridgeField;

		// Token: 0x04000406 RID: 1030
		[CompilerGenerated]
		[AccessedThroughProperty("PanLoad")]
		private MyCard m_ItemField;

		// Token: 0x04000407 RID: 1031
		[CompilerGenerated]
		[AccessedThroughProperty("Load")]
		private MyLoading m_ReponseField;

		// Token: 0x04000408 RID: 1032
		private bool globalField;
	}
}
