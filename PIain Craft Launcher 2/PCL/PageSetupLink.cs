using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020001B6 RID: 438
	[DesignerGenerated]
	public class PageSetupLink : MyPageRight, IComponentConnector
	{
		// Token: 0x0600131C RID: 4892 RVA: 0x0000B2FB File Offset: 0x000094FB
		public PageSetupLink()
		{
			base.Loaded += this.PageSetupLink_Loaded;
			this.issuerProperty = false;
			this.InitializeComponent();
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0000B323 File Offset: 0x00009523
		private void PageSetupLink_Loaded(object sender, RoutedEventArgs e)
		{
			this.PanBack.ScrollToHome();
			checked
			{
				if (!this.issuerProperty)
				{
					this.issuerProperty = true;
					ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
					this.Reload();
					ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
				}
			}
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00084FB4 File Offset: 0x000831B4
		public void Reload()
		{
			this.TextLinkName.Text = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LinkName", null));
			this.CheckHiperCertWarn.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LinkHiperCertWarn", null));
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00085004 File Offset: 0x00083204
		public void Reset()
		{
			try
			{
				ModBase.m_IdentifierRepository.Reset("LinkName", false, null);
				ModBase.m_IdentifierRepository.Reset("LinkHiperCertWarn", false, null);
				ModBase.Log("[Setup] 已初始化联机页设置", ModBase.LogLevel.Normal, "出现错误");
				ModMain.Hint("已初始化联机页设置！", ModMain.HintType.Finish, false);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "初始化联机页设置失败", ModBase.LogLevel.Msgbox, "出现错误");
			}
			this.Reload();
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0000ACF9 File Offset: 0x00008EF9
		private static void TextBoxChange(MyTextBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.Text, false, null);
			}
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0000AD75 File Offset: 0x00008F75
		private static void CheckBoxChange(MyCheckBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.Checked, false, null);
			}
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0000B35D File Offset: 0x0000955D
		private void BtnHiperLog_Click(object sender, EventArgs e)
		{
			if (File.Exists(PageLinkHiper._AttributeClient + "logs\\hiper.log"))
			{
				ModBase.OpenExplorer("/select,\"" + PageLinkHiper._AttributeClient + "logs\\hiper.log\"");
				return;
			}
			ModMain.Hint("没有找到 HiPer 联机模块的日志！", ModMain.HintType.Info, true);
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x0000B39B File Offset: 0x0000959B
		// (set) Token: 0x06001324 RID: 4900 RVA: 0x0000B3A3 File Offset: 0x000095A3
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x0000B3AC File Offset: 0x000095AC
		// (set) Token: 0x06001326 RID: 4902 RVA: 0x0000B3B4 File Offset: 0x000095B4
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x0000B3BD File Offset: 0x000095BD
		// (set) Token: 0x06001328 RID: 4904 RVA: 0x00085088 File Offset: 0x00083288
		internal virtual MyCheckBox CheckHiperCertWarn
		{
			[CompilerGenerated]
			get
			{
				return this.serializerProperty;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = (PageSetupLink._Closure$__.$IR18-1 == null) ? (PageSetupLink._Closure$__.$IR18-1 = delegate(object a0, bool a1)
				{
					PageSetupLink.CheckBoxChange((MyCheckBox)a0, a1);
				}) : PageSetupLink._Closure$__.$IR18-1;
				MyCheckBox myCheckBox = this.serializerProperty;
				if (myCheckBox != null)
				{
					myCheckBox.PublishConfig(obj);
				}
				this.serializerProperty = value;
				myCheckBox = this.serializerProperty;
				if (myCheckBox != null)
				{
					myCheckBox.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x0000B3C5 File Offset: 0x000095C5
		// (set) Token: 0x0600132A RID: 4906 RVA: 0x000850E4 File Offset: 0x000832E4
		internal virtual MyButton BtnHiperLog
		{
			[CompilerGenerated]
			get
			{
				return this.m_WatcherProperty;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnHiperLog_Click);
				MyButton watcherProperty = this.m_WatcherProperty;
				if (watcherProperty != null)
				{
					watcherProperty.Click -= value2;
				}
				this.m_WatcherProperty = value;
				watcherProperty = this.m_WatcherProperty;
				if (watcherProperty != null)
				{
					watcherProperty.Click += value2;
				}
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x0000B3CD File Offset: 0x000095CD
		// (set) Token: 0x0600132C RID: 4908 RVA: 0x00085128 File Offset: 0x00083328
		internal virtual MyTextBox TextLinkName
		{
			[CompilerGenerated]
			get
			{
				return this.identifierProperty;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = (PageSetupLink._Closure$__.$IR26-2 == null) ? (PageSetupLink._Closure$__.$IR26-2 = delegate(object sender, RoutedEventArgs e)
				{
					PageSetupLink.TextBoxChange((MyTextBox)sender, e);
				}) : PageSetupLink._Closure$__.$IR26-2;
				MyTextBox myTextBox = this.identifierProperty;
				if (myTextBox != null)
				{
					myTextBox.DestroyReader(value2);
				}
				this.identifierProperty = value;
				myTextBox = this.identifierProperty;
				if (myTextBox != null)
				{
					myTextBox.SetupReader(value2);
				}
			}
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00085184 File Offset: 0x00083384
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_SystemProperty)
			{
				this.m_SystemProperty = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagesetup/pagesetuplink.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x000851B4 File Offset: 0x000833B4
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
			if (connectionId == 3)
			{
				this.CheckHiperCertWarn = (MyCheckBox)target;
				return;
			}
			if (connectionId == 4)
			{
				this.BtnHiperLog = (MyButton)target;
				return;
			}
			if (connectionId == 5)
			{
				this.TextLinkName = (MyTextBox)target;
				return;
			}
			this.m_SystemProperty = true;
		}

		// Token: 0x040009D1 RID: 2513
		private bool issuerProperty;

		// Token: 0x040009D2 RID: 2514
		[CompilerGenerated]
		[AccessedThroughProperty("PanBack")]
		private MyScrollViewer indexerProperty;

		// Token: 0x040009D3 RID: 2515
		[CompilerGenerated]
		[AccessedThroughProperty("PanMain")]
		private StackPanel m_InterpreterProperty;

		// Token: 0x040009D4 RID: 2516
		[CompilerGenerated]
		[AccessedThroughProperty("CheckHiperCertWarn")]
		private MyCheckBox serializerProperty;

		// Token: 0x040009D5 RID: 2517
		[AccessedThroughProperty("BtnHiperLog")]
		[CompilerGenerated]
		private MyButton m_WatcherProperty;

		// Token: 0x040009D6 RID: 2518
		[AccessedThroughProperty("TextLinkName")]
		[CompilerGenerated]
		private MyTextBox identifierProperty;

		// Token: 0x040009D7 RID: 2519
		private bool m_SystemProperty;
	}
}
