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
	// Token: 0x020001BF RID: 447
	[DesignerGenerated]
	public class PageSpeedRight : MyPageRight, IComponentConnector
	{
		// Token: 0x060014AD RID: 5293 RVA: 0x0000BBC0 File Offset: 0x00009DC0
		public PageSpeedRight()
		{
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.Init();
			};
			this.InitializeComponent();
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0000BBE1 File Offset: 0x00009DE1
		private void Init()
		{
			this.PanBack.ScrollToHome();
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x0000BBEE File Offset: 0x00009DEE
		// (set) Token: 0x060014B0 RID: 5296 RVA: 0x0000BBF6 File Offset: 0x00009DF6
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x0000BBFF File Offset: 0x00009DFF
		// (set) Token: 0x060014B2 RID: 5298 RVA: 0x0000BC07 File Offset: 0x00009E07
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x060014B3 RID: 5299 RVA: 0x0008AA54 File Offset: 0x00088C54
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._IteratorComposer)
			{
				this._IteratorComposer = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagespeedright.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0000BC10 File Offset: 0x00009E10
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
			this._IteratorComposer = true;
		}

		// Token: 0x04000A9D RID: 2717
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyScrollViewer _PropertyComposer;

		// Token: 0x04000A9E RID: 2718
		[CompilerGenerated]
		[AccessedThroughProperty("PanMain")]
		private StackPanel _ComposerComposer;

		// Token: 0x04000A9F RID: 2719
		private bool _IteratorComposer;
	}
}
