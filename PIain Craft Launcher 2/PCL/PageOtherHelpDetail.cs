using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x02000103 RID: 259
	[DesignerGenerated]
	public class PageOtherHelpDetail : MyPageRight, IRefreshable, IComponentConnector
	{
		// Token: 0x06000A46 RID: 2630 RVA: 0x00007453 File Offset: 0x00005653
		public PageOtherHelpDetail()
		{
			base.Loaded += this.PageOtherHelpDetail_Loaded;
			this.InitializeComponent();
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00007474 File Offset: 0x00005674
		public void Refresh()
		{
			this.Init(new ModMain.HelpEntry(this._DefinitionClient._DescriptorMap));
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0000748D File Offset: 0x0000568D
		private void PageOtherHelpDetail_Loaded(object sender, RoutedEventArgs e)
		{
			this.PanBack.ScrollToTop();
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0004AB24 File Offset: 0x00048D24
		public bool Init(ModMain.HelpEntry Entry)
		{
			string text = "<StackPanel xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:local=\"clr-namespace:PCL;assembly=Plain Craft Launcher 2\">" + Entry.testsError + "</StackPanel>";
			bool result;
			try
			{
				if (string.IsNullOrEmpty(Entry.testsError))
				{
					throw new Exception("帮助 xaml 文件为空");
				}
				this._DefinitionClient = Entry;
				this.PanCustom.Children.Clear();
				text = ModMain.HelpArgumentReplace(text);
				this.PanCustom.Children.Add((UIElement)ModBase.GetObjectFromXML(text));
				result = true;
			}
			catch (Exception ex)
			{
				ModBase.Log("[System] 自定义信息内容：\r\n" + text, ModBase.LogLevel.Normal, "出现错误");
				ModBase.Log(ex, "加载帮助 xaml 文件失败", ModBase.LogLevel.Msgbox, "出现错误");
				result = false;
			}
			return result;
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0000749A File Offset: 0x0000569A
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x000074A2 File Offset: 0x000056A2
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x000074AB File Offset: 0x000056AB
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x000074B3 File Offset: 0x000056B3
		internal virtual StackPanel PanCustom { get; set; }

		// Token: 0x06000A4E RID: 2638 RVA: 0x0004ABE8 File Offset: 0x00048DE8
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_ParserConfig)
			{
				this.m_ParserConfig = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageother/pageotherhelpdetail.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000074BC File Offset: 0x000056BC
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
				this.PanCustom = (StackPanel)target;
				return;
			}
			this.m_ParserConfig = true;
		}

		// Token: 0x04000550 RID: 1360
		public ModMain.HelpEntry _DefinitionClient;

		// Token: 0x04000551 RID: 1361
		[CompilerGenerated]
		[AccessedThroughProperty("PanBack")]
		private MyScrollViewer _StrategyClient;

		// Token: 0x04000552 RID: 1362
		[CompilerGenerated]
		[AccessedThroughProperty("PanCustom")]
		private StackPanel procClient;

		// Token: 0x04000553 RID: 1363
		private bool m_ParserConfig;
	}
}
