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
	// Token: 0x020000FC RID: 252
	[DesignerGenerated]
	public class PageLinkFeedback : MyPageRight, IComponentConnector
	{
		// Token: 0x060009B8 RID: 2488 RVA: 0x00006F45 File Offset: 0x00005145
		public PageLinkFeedback()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x00006F54 File Offset: 0x00005154
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x00006F5C File Offset: 0x0000515C
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x00006F65 File Offset: 0x00005165
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x00006F6D File Offset: 0x0000516D
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x060009BD RID: 2493 RVA: 0x00049A08 File Offset: 0x00047C08
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_EventClient)
			{
				this.m_EventClient = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelink/pagelinkfeedback.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00006F76 File Offset: 0x00005176
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
				this.PanMain = (StackPanel)target;
				return;
			}
			this.m_EventClient = true;
		}

		// Token: 0x0400050C RID: 1292
		[CompilerGenerated]
		[AccessedThroughProperty("PanBack")]
		private MyScrollViewer m_AccountClient;

		// Token: 0x0400050D RID: 1293
		[AccessedThroughProperty("PanMain")]
		[CompilerGenerated]
		private StackPanel m_QueueClient;

		// Token: 0x0400050E RID: 1294
		private bool m_EventClient;
	}
}
