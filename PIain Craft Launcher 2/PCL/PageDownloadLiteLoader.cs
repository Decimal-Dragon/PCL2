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
	// Token: 0x02000180 RID: 384
	[DesignerGenerated]
	public class PageDownloadLiteLoader : MyPageRight, IComponentConnector
	{
		// Token: 0x06000ED8 RID: 3800 RVA: 0x00009346 File Offset: 0x00007546
		public PageDownloadLiteLoader()
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

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0006ECA8 File Offset: 0x0006CEA8
		private void LoaderInit()
		{
			base.PageLoaderInit(this.Load, this.PanLoad, this.PanMain, this.CardTip, ModDownload._FacadeTests, delegate(ModLoader.LoaderBase a0)
			{
				this.Load_OnFinish();
			}, null, true);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00009379 File Offset: 0x00007579
		private void Init()
		{
			this.PanBack.ScrollToHome();
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0006ECE8 File Offset: 0x0006CEE8
		private void Load_OnFinish()
		{
			checked
			{
				try
				{
					Dictionary<string, List<ModDownload.DlLiteLoaderListEntry>> dictionary = new Dictionary<string, List<ModDownload.DlLiteLoaderListEntry>>();
					int num = 30;
					do
					{
						dictionary.Add("1." + Conversions.ToString(num), new List<ModDownload.DlLiteLoaderListEntry>());
						num += -1;
					}
					while (num >= 0);
					dictionary.Add("未知版本", new List<ModDownload.DlLiteLoaderListEntry>());
					try
					{
						foreach (ModDownload.DlLiteLoaderListEntry dlLiteLoaderListEntry in ModDownload._FacadeTests.Output.Value)
						{
							string key = "1." + dlLiteLoaderListEntry.Inherit.Split(".")[1];
							if (dictionary.ContainsKey(key))
							{
								dictionary[key].Add(dlLiteLoaderListEntry);
							}
							else
							{
								dictionary["未知版本"].Add(dlLiteLoaderListEntry);
							}
						}
					}
					finally
					{
						List<ModDownload.DlLiteLoaderListEntry>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					this.PanMain.Children.Clear();
					try
					{
						foreach (KeyValuePair<string, List<ModDownload.DlLiteLoaderListEntry>> keyValuePair in dictionary)
						{
							if (Enumerable.Any<ModDownload.DlLiteLoaderListEntry>(keyValuePair.Value))
							{
								MyCard myCard = new MyCard();
								myCard.Title = keyValuePair.Key + " (" + Conversions.ToString(keyValuePair.Value.Count) + ")";
								myCard.Margin = new Thickness(0.0, 0.0, 0.0, 15.0);
								myCard.CreateParser(10);
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
						Dictionary<string, List<ModDownload.DlLiteLoaderListEntry>>.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "可视化 LiteLoader 版本列表出错", ModBase.LogLevel.Feedback, "出现错误");
				}
			}
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00009386 File Offset: 0x00007586
		public void DownloadStart(MyListItem sender, object e)
		{
			ModDownloadLib.McDownloadLiteLoader((ModDownload.DlLiteLoaderListEntry)sender.Tag);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x00009398 File Offset: 0x00007598
		private void BtnWeb_Click(object sender, EventArgs e)
		{
			ModBase.OpenWebsite("https://www.liteloader.com");
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x000093A4 File Offset: 0x000075A4
		// (set) Token: 0x06000EDF RID: 3807 RVA: 0x000093AC File Offset: 0x000075AC
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x000093B5 File Offset: 0x000075B5
		// (set) Token: 0x06000EE1 RID: 3809 RVA: 0x000093BD File Offset: 0x000075BD
		internal virtual MyCard CardTip { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x000093C6 File Offset: 0x000075C6
		// (set) Token: 0x06000EE3 RID: 3811 RVA: 0x000093CE File Offset: 0x000075CE
		internal virtual TextBlock LabConnect { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x000093D7 File Offset: 0x000075D7
		// (set) Token: 0x06000EE5 RID: 3813 RVA: 0x0006EF7C File Offset: 0x0006D17C
		internal virtual MyButton BtnWeb
		{
			[CompilerGenerated]
			get
			{
				return this._BtnWeb;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnWeb_Click);
				MyButton btnWeb = this._BtnWeb;
				if (btnWeb != null)
				{
					btnWeb.Click -= value2;
				}
				this._BtnWeb = value;
				btnWeb = this._BtnWeb;
				if (btnWeb != null)
				{
					btnWeb.Click += value2;
				}
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x000093DF File Offset: 0x000075DF
		// (set) Token: 0x06000EE7 RID: 3815 RVA: 0x000093E7 File Offset: 0x000075E7
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x000093F0 File Offset: 0x000075F0
		// (set) Token: 0x06000EE9 RID: 3817 RVA: 0x000093F8 File Offset: 0x000075F8
		internal virtual MyCard PanLoad { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x00009401 File Offset: 0x00007601
		// (set) Token: 0x06000EEB RID: 3819 RVA: 0x00009409 File Offset: 0x00007609
		internal virtual MyLoading Load { get; set; }

		// Token: 0x06000EEC RID: 3820 RVA: 0x0006EFC0 File Offset: 0x0006D1C0
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagedownload/pagedownloadliteloader.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0006EFF0 File Offset: 0x0006D1F0
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
			this._contentLoaded = true;
		}

		// Token: 0x04000821 RID: 2081
		private bool _contentLoaded;
	}
}
