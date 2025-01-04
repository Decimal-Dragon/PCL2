using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Markup;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x0200011A RID: 282
	[DesignerGenerated]
	public class PageVersionModDisabled : MyPageRight, IComponentConnector
	{
		// Token: 0x06000B9B RID: 2971 RVA: 0x00007EDC File Offset: 0x000060DC
		public PageVersionModDisabled()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00007EEB File Offset: 0x000060EB
		private void BtnDownload_Click(object sender, EventArgs e)
		{
			ModMain._ProcessIterator.PageChange(FormMain.PageType.Download, FormMain.PageSubType.DownloadInstall);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00007EFE File Offset: 0x000060FE
		private void BtnVersion_Click(object sender, EventArgs e)
		{
			ModMain._ProcessIterator.PageChange(FormMain.PageType.Launch, FormMain.PageSubType.Default);
			ModMain._ProcessIterator.PageChange(FormMain.PageType.VersionSelect, FormMain.PageSubType.Default);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0004F4F0 File Offset: 0x0004D6F0
		public void BtnDownload_Loaded()
		{
			Visibility visibility = Conversions.ToBoolean((Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenPageDownload", null)) && !PageSetupUI.CreateClient()) || (ModMain.proxyIterator != null && ModMain.proxyIterator._StatusMapper)) ? Visibility.Collapsed : Visibility.Visible;
			if (this.BtnDownload.Visibility != visibility)
			{
				this.BtnDownload.Visibility = visibility;
				this.PanMain.TriggerForceResize();
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00007F22 File Offset: 0x00006122
		// (set) Token: 0x06000BA0 RID: 2976 RVA: 0x00007F2A File Offset: 0x0000612A
		internal virtual MyCard PanMain { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x00007F33 File Offset: 0x00006133
		// (set) Token: 0x06000BA2 RID: 2978 RVA: 0x0004F568 File Offset: 0x0004D768
		internal virtual MyButton BtnDownload
		{
			[CompilerGenerated]
			get
			{
				return this._ComparatorConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnDownload_Click);
				RoutedEventHandler value3 = delegate(object sender, RoutedEventArgs e)
				{
					this.BtnDownload_Loaded();
				};
				MyButton comparatorConfig = this._ComparatorConfig;
				if (comparatorConfig != null)
				{
					comparatorConfig.Click -= value2;
					comparatorConfig.Loaded -= value3;
				}
				this._ComparatorConfig = value;
				comparatorConfig = this._ComparatorConfig;
				if (comparatorConfig != null)
				{
					comparatorConfig.Click += value2;
					comparatorConfig.Loaded += value3;
				}
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00007F3B File Offset: 0x0000613B
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x0004F5C8 File Offset: 0x0004D7C8
		internal virtual MyButton BtnVersion
		{
			[CompilerGenerated]
			get
			{
				return this.m_MappingConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnVersion_Click);
				MyButton mappingConfig = this.m_MappingConfig;
				if (mappingConfig != null)
				{
					mappingConfig.Click -= value2;
				}
				this.m_MappingConfig = value;
				mappingConfig = this.m_MappingConfig;
				if (mappingConfig != null)
				{
					mappingConfig.Click += value2;
				}
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0004F60C File Offset: 0x0004D80C
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_TokenizerConfig)
			{
				this.m_TokenizerConfig = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageversion/pageversionmoddisabled.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00007F43 File Offset: 0x00006143
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanMain = (MyCard)target;
				return;
			}
			if (connectionId == 2)
			{
				this.BtnDownload = (MyButton)target;
				return;
			}
			if (connectionId == 3)
			{
				this.BtnVersion = (MyButton)target;
				return;
			}
			this.m_TokenizerConfig = true;
		}

		// Token: 0x040005E3 RID: 1507
		[AccessedThroughProperty("PanMain")]
		[CompilerGenerated]
		private MyCard m_AlgoConfig;

		// Token: 0x040005E4 RID: 1508
		[AccessedThroughProperty("BtnDownload")]
		[CompilerGenerated]
		private MyButton _ComparatorConfig;

		// Token: 0x040005E5 RID: 1509
		[AccessedThroughProperty("BtnVersion")]
		[CompilerGenerated]
		private MyButton m_MappingConfig;

		// Token: 0x040005E6 RID: 1510
		private bool m_TokenizerConfig;
	}
}
