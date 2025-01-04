using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace PCL
{
	// Token: 0x020000E1 RID: 225
	[DesignerGenerated]
	public class PageDownloadFabric : MyPageRight, IComponentConnector
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x00005A90 File Offset: 0x00003C90
		public PageDownloadFabric()
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

		// Token: 0x06000720 RID: 1824 RVA: 0x0003F08C File Offset: 0x0003D28C
		private void LoaderInit()
		{
			base.PageLoaderInit(this.Load, this.PanLoad, this.CardVersions, this.CardTip, ModDownload.authenticationTests, delegate(ModLoader.LoaderBase a0)
			{
				this.Load_OnFinish();
			}, null, true);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00005AC3 File Offset: 0x00003CC3
		private void Init()
		{
			this.PanBack.ScrollToHome();
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0003F0CC File Offset: 0x0003D2CC
		private void Load_OnFinish()
		{
			try
			{
				JArray jarray = (JArray)ModDownload.authenticationTests.Output.Value["installer"];
				this.PanVersions.Children.Clear();
				try
				{
					foreach (JToken jtoken in jarray)
					{
						this.PanVersions.Children.Add(ModDownloadLib.FabricDownloadListItem((JObject)jtoken, delegate(object sender, MouseButtonEventArgs e)
						{
							this.Fabric_Selected((MyListItem)sender, e);
						}));
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
				this.CardVersions.Title = "版本列表 (" + Conversions.ToString(jarray.Count) + ")";
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "可视化 Fabric 版本列表出错", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00005AD0 File Offset: 0x00003CD0
		private void Fabric_Selected(MyListItem sender, EventArgs e)
		{
			ModDownloadLib.McDownloadFabricLoaderSave((JObject)sender.Tag);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00005AE2 File Offset: 0x00003CE2
		private void BtnWeb_Click(object sender, EventArgs e)
		{
			ModBase.OpenWebsite("https://www.fabricmc.net");
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00005AEE File Offset: 0x00003CEE
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x00005AF6 File Offset: 0x00003CF6
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x00005AFF File Offset: 0x00003CFF
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x00005B07 File Offset: 0x00003D07
		internal virtual MyCard CardTip { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00005B10 File Offset: 0x00003D10
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x0003F1B8 File Offset: 0x0003D3B8
		internal virtual MyButton BtnWeb
		{
			[CompilerGenerated]
			get
			{
				return this.m_ClassField;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnWeb_Click);
				MyButton classField = this.m_ClassField;
				if (classField != null)
				{
					classField.Click -= value2;
				}
				this.m_ClassField = value;
				classField = this.m_ClassField;
				if (classField != null)
				{
					classField.Click += value2;
				}
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x00005B18 File Offset: 0x00003D18
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x00005B20 File Offset: 0x00003D20
		internal virtual MyCard CardVersions { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x00005B29 File Offset: 0x00003D29
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x00005B31 File Offset: 0x00003D31
		internal virtual StackPanel PanVersions { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00005B3A File Offset: 0x00003D3A
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x00005B42 File Offset: 0x00003D42
		internal virtual MyCard PanLoad { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x00005B4B File Offset: 0x00003D4B
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x00005B53 File Offset: 0x00003D53
		internal virtual MyLoading Load { get; set; }

		// Token: 0x06000733 RID: 1843 RVA: 0x0003F1FC File Offset: 0x0003D3FC
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.publisherField)
			{
				this.publisherField = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagedownload/pagedownloadfabric.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0003F22C File Offset: 0x0003D42C
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
				this.BtnWeb = (MyButton)target;
				return;
			}
			if (connectionId == 4)
			{
				this.CardVersions = (MyCard)target;
				return;
			}
			if (connectionId == 5)
			{
				this.PanVersions = (StackPanel)target;
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
			this.publisherField = true;
		}

		// Token: 0x04000409 RID: 1033
		[CompilerGenerated]
		[AccessedThroughProperty("PanBack")]
		private MyScrollViewer m_ExceptionField;

		// Token: 0x0400040A RID: 1034
		[CompilerGenerated]
		[AccessedThroughProperty("CardTip")]
		private MyCard utilsField;

		// Token: 0x0400040B RID: 1035
		[CompilerGenerated]
		[AccessedThroughProperty("BtnWeb")]
		private MyButton m_ClassField;

		// Token: 0x0400040C RID: 1036
		[CompilerGenerated]
		[AccessedThroughProperty("CardVersions")]
		private MyCard _PolicyField;

		// Token: 0x0400040D RID: 1037
		[AccessedThroughProperty("PanVersions")]
		[CompilerGenerated]
		private StackPanel m_ProducerField;

		// Token: 0x0400040E RID: 1038
		[AccessedThroughProperty("PanLoad")]
		[CompilerGenerated]
		private MyCard m_SchemaField;

		// Token: 0x0400040F RID: 1039
		[CompilerGenerated]
		[AccessedThroughProperty("Load")]
		private MyLoading _DescriptorField;

		// Token: 0x04000410 RID: 1040
		private bool publisherField;
	}
}
