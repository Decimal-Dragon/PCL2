using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x02000181 RID: 385
	[DesignerGenerated]
	public class PageDownloadOptiFine : MyPageRight, IComponentConnector
	{
		// Token: 0x06000EF3 RID: 3827 RVA: 0x0000942A File Offset: 0x0000762A
		public PageDownloadOptiFine()
		{
			base.Initialized += delegate(object sender, EventArgs e)
			{
				this.LoaderInit();
			};
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.Init();
			};
			this.InitializeComponent();
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0006F07C File Offset: 0x0006D27C
		private void LoaderInit()
		{
			base.PageLoaderInit(this.Load, this.PanLoad, this.PanMain, this.CardTip, ModDownload.m_ModelTests, delegate(ModLoader.LoaderBase a0)
			{
				this.Load_OnFinish();
			}, null, true);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0000945D File Offset: 0x0000765D
		private void Init()
		{
			this.PanBack.ScrollToHome();
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0006F0BC File Offset: 0x0006D2BC
		private void Load_OnFinish()
		{
			checked
			{
				try
				{
					Dictionary<string, List<ModDownload.DlOptiFineListEntry>> dictionary = new Dictionary<string, List<ModDownload.DlOptiFineListEntry>>();
					dictionary.Add("快照版本", new List<ModDownload.DlOptiFineListEntry>());
					int num = 50;
					do
					{
						dictionary.Add("1." + Conversions.ToString(num), new List<ModDownload.DlOptiFineListEntry>());
						num += -1;
					}
					while (num >= 0);
					try
					{
						foreach (ModDownload.DlOptiFineListEntry dlOptiFineListEntry in ModDownload.m_ModelTests.Output.Value)
						{
							if (dlOptiFineListEntry.CheckMapper().StartsWith("1."))
							{
								string key = "1." + dlOptiFineListEntry.m_SchemaTest.Split(".")[1].Split(" ")[0];
								if (dictionary.ContainsKey(key))
								{
									dictionary[key].Add(dlOptiFineListEntry);
								}
								else
								{
									dictionary["快照版本"].Add(dlOptiFineListEntry);
								}
							}
							else
							{
								dictionary["快照版本"].Add(dlOptiFineListEntry);
							}
						}
					}
					finally
					{
						List<ModDownload.DlOptiFineListEntry>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					this.PanMain.Children.Clear();
					try
					{
						foreach (KeyValuePair<string, List<ModDownload.DlOptiFineListEntry>> keyValuePair in dictionary)
						{
							if (Enumerable.Any<ModDownload.DlOptiFineListEntry>(keyValuePair.Value))
							{
								MyCard myCard = new MyCard();
								myCard.Title = keyValuePair.Key + " (" + Conversions.ToString(keyValuePair.Value.Count) + ")";
								myCard.Margin = new Thickness(0.0, 0.0, 0.0, 15.0);
								myCard.CreateParser(3);
								MyCard myCard2 = myCard;
								StackPanel stackPanel = new StackPanel
								{
									Margin = new Thickness(20.0, 40.0, 18.0, 0.0),
									VerticalAlignment = VerticalAlignment.Top,
									RenderTransform = new TranslateTransform(0.0, 0.0),
									Tag = keyValuePair.Value
								};
								myCard2.Children.Add(stackPanel);
								myCard2._Stub = stackPanel;
								myCard2.IsSwaped = true;
								this.PanMain.Children.Add(myCard2);
							}
						}
					}
					finally
					{
						Dictionary<string, List<ModDownload.DlOptiFineListEntry>>.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "可视化 OptiFine 版本列表出错", ModBase.LogLevel.Feedback, "出现错误");
				}
			}
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0000946A File Offset: 0x0000766A
		private void BtnWeb_Click(object sender, EventArgs e)
		{
			ModBase.OpenWebsite("https://www.optifine.net/");
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00009476 File Offset: 0x00007676
		// (set) Token: 0x06000EF9 RID: 3833 RVA: 0x0000947E File Offset: 0x0000767E
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00009487 File Offset: 0x00007687
		// (set) Token: 0x06000EFB RID: 3835 RVA: 0x0000948F File Offset: 0x0000768F
		internal virtual MyCard CardTip { get; set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x00009498 File Offset: 0x00007698
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x000094A0 File Offset: 0x000076A0
		internal virtual TextBlock LabConnect { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x000094A9 File Offset: 0x000076A9
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x0006F384 File Offset: 0x0006D584
		internal virtual MyButton BtnWeb
		{
			[CompilerGenerated]
			get
			{
				return this.m_SchemaTests;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnWeb_Click);
				MyButton schemaTests = this.m_SchemaTests;
				if (schemaTests != null)
				{
					schemaTests.Click -= value2;
				}
				this.m_SchemaTests = value;
				schemaTests = this.m_SchemaTests;
				if (schemaTests != null)
				{
					schemaTests.Click += value2;
				}
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x000094B1 File Offset: 0x000076B1
		// (set) Token: 0x06000F01 RID: 3841 RVA: 0x000094B9 File Offset: 0x000076B9
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x000094C2 File Offset: 0x000076C2
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x000094CA File Offset: 0x000076CA
		internal virtual MyCard PanLoad { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x000094D3 File Offset: 0x000076D3
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x000094DB File Offset: 0x000076DB
		internal virtual MyLoading Load { get; set; }

		// Token: 0x06000F06 RID: 3846 RVA: 0x0006F3C8 File Offset: 0x0006D5C8
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._StrategyTests)
			{
				this._StrategyTests = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagedownload/pagedownloadoptifine.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0006F3F8 File Offset: 0x0006D5F8
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
				this.CardTip = (MyCard)target;
				return;
			}
			if (connectionId == 3)
			{
				this.LabConnect = (TextBlock)target;
				return;
			}
			if (connectionId == 4)
			{
				this.BtnWeb = (MyButton)target;
				return;
			}
			if (connectionId == 5)
			{
				this.PanMain = (StackPanel)target;
				return;
			}
			if (connectionId == 6)
			{
				this.PanLoad = (MyCard)target;
				return;
			}
			if (connectionId == 7)
			{
				this.Load = (MyLoading)target;
				return;
			}
			this._StrategyTests = true;
		}

		// Token: 0x04000822 RID: 2082
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyScrollViewer m_PolicyTests;

		// Token: 0x04000823 RID: 2083
		[CompilerGenerated]
		[AccessedThroughProperty("CardTip")]
		private MyCard m_OrderTests;

		// Token: 0x04000824 RID: 2084
		[CompilerGenerated]
		[AccessedThroughProperty("LabConnect")]
		private TextBlock producerTests;

		// Token: 0x04000825 RID: 2085
		[CompilerGenerated]
		[AccessedThroughProperty("BtnWeb")]
		private MyButton m_SchemaTests;

		// Token: 0x04000826 RID: 2086
		[CompilerGenerated]
		[AccessedThroughProperty("PanMain")]
		private StackPanel descriptorTests;

		// Token: 0x04000827 RID: 2087
		[AccessedThroughProperty("PanLoad")]
		[CompilerGenerated]
		private MyCard _PublisherTests;

		// Token: 0x04000828 RID: 2088
		[AccessedThroughProperty("Load")]
		[CompilerGenerated]
		private MyLoading _DefinitionTests;

		// Token: 0x04000829 RID: 2089
		private bool _StrategyTests;
	}
}
