using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x0200019F RID: 415
	[DesignerGenerated]
	public class PageOtherAbout : MyPageRight, IComponentConnector
	{
		// Token: 0x060010E5 RID: 4325 RVA: 0x0000A4F7 File Offset: 0x000086F7
		public PageOtherAbout()
		{
			base.Loaded += this.PageOtherAbout_Loaded;
			this.clientThread = false;
			this.InitializeComponent();
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0007B514 File Offset: 0x00079714
		private void PageOtherAbout_Loaded(object sender, RoutedEventArgs e)
		{
			this.PanBack.ScrollToHome();
			if (!this.clientThread)
			{
				this.clientThread = true;
				this.ItemAboutPcl.Info = this.ItemAboutPcl.Info.Replace("%VERSION%", "Release 2.8.12").Replace("%VERSIONCODE%", Conversions.ToString(347)).Replace("%BRANCH%", "50");
			}
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0000A51F File Offset: 0x0000871F
		private void BtnAboutBmclapi_Click(object sender, EventArgs e)
		{
			ModBase.OpenWebsite("https://afdian.com/a/bangbang93");
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0000A52B File Offset: 0x0000872B
		private void BtnAboutWiki_Click(object sender, EventArgs e)
		{
			ModBase.OpenWebsite("https://www.mcmod.cn");
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0000A537 File Offset: 0x00008737
		public static void CopyUniqueAddress()
		{
			ModBase.ClipboardSet(ModBase._TagRepository, true);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0000A544 File Offset: 0x00008744
		private void BtnDonateCodeInput_Click()
		{
			ModSecret.DonateCodeInput();
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x0000A54C File Offset: 0x0000874C
		// (set) Token: 0x060010EC RID: 4332 RVA: 0x0000A554 File Offset: 0x00008754
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x0000A55D File Offset: 0x0000875D
		// (set) Token: 0x060010EE RID: 4334 RVA: 0x0000A565 File Offset: 0x00008765
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x0000A56E File Offset: 0x0000876E
		// (set) Token: 0x060010F0 RID: 4336 RVA: 0x0000A576 File Offset: 0x00008776
		internal virtual MyListItem ItemAboutPcl { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0000A57F File Offset: 0x0000877F
		// (set) Token: 0x060010F2 RID: 4338 RVA: 0x0007B584 File Offset: 0x00079784
		internal virtual MyButton BtnAboutBmclapi
		{
			[CompilerGenerated]
			get
			{
				return this._ThreadThread;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnAboutBmclapi_Click);
				MyButton threadThread = this._ThreadThread;
				if (threadThread != null)
				{
					threadThread.Click -= value2;
				}
				this._ThreadThread = value;
				threadThread = this._ThreadThread;
				if (threadThread != null)
				{
					threadThread.Click += value2;
				}
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0000A587 File Offset: 0x00008787
		// (set) Token: 0x060010F4 RID: 4340 RVA: 0x0007B5C8 File Offset: 0x000797C8
		internal virtual MyButton BtnAboutWiki
		{
			[CompilerGenerated]
			get
			{
				return this.propertyThread;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnAboutWiki_Click);
				MyButton myButton = this.propertyThread;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.propertyThread = value;
				myButton = this.propertyThread;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x0000A58F File Offset: 0x0000878F
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x0000A597 File Offset: 0x00008797
		internal virtual MyButton BtnDonateOutput { get; set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x0000A5A0 File Offset: 0x000087A0
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x0000A5A8 File Offset: 0x000087A8
		internal virtual MyButton BtnDonateDonate { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x0000A5B1 File Offset: 0x000087B1
		// (set) Token: 0x060010FA RID: 4346 RVA: 0x0007B60C File Offset: 0x0007980C
		internal virtual MyButton BtnDonateCopy
		{
			[CompilerGenerated]
			get
			{
				return this.repositoryThread;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = (PageOtherAbout._Closure$__.$IR37-1 == null) ? (PageOtherAbout._Closure$__.$IR37-1 = delegate(object sender, MouseButtonEventArgs e)
				{
					PageOtherAbout.CopyUniqueAddress();
				}) : PageOtherAbout._Closure$__.$IR37-1;
				MyButton myButton = this.repositoryThread;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.repositoryThread = value;
				myButton = this.repositoryThread;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x0000A5B9 File Offset: 0x000087B9
		// (set) Token: 0x060010FC RID: 4348 RVA: 0x0007B668 File Offset: 0x00079868
		internal virtual MyButton BtnDonateInput
		{
			[CompilerGenerated]
			get
			{
				return this.m_TestThread;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.BtnDonateCodeInput_Click();
				};
				MyButton testThread = this.m_TestThread;
				if (testThread != null)
				{
					testThread.Click -= value2;
				}
				this.m_TestThread = value;
				testThread = this.m_TestThread;
				if (testThread != null)
				{
					testThread.Click += value2;
				}
			}
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0007B6AC File Offset: 0x000798AC
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._MapThread)
			{
				this._MapThread = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageother/pageotherabout.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0007B6DC File Offset: 0x000798DC
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
				this.ItemAboutPcl = (MyListItem)target;
				return;
			}
			if (connectionId == 4)
			{
				this.BtnAboutBmclapi = (MyButton)target;
				return;
			}
			if (connectionId == 5)
			{
				this.BtnAboutWiki = (MyButton)target;
				return;
			}
			if (connectionId == 6)
			{
				this.BtnDonateOutput = (MyButton)target;
				return;
			}
			if (connectionId == 7)
			{
				this.BtnDonateDonate = (MyButton)target;
				return;
			}
			if (connectionId == 8)
			{
				this.BtnDonateCopy = (MyButton)target;
				return;
			}
			if (connectionId == 9)
			{
				this.BtnDonateInput = (MyButton)target;
				return;
			}
			this._MapThread = true;
		}

		// Token: 0x040008EB RID: 2283
		private bool clientThread;

		// Token: 0x040008EC RID: 2284
		[CompilerGenerated]
		[AccessedThroughProperty("PanBack")]
		private MyScrollViewer m_ConfigThread;

		// Token: 0x040008ED RID: 2285
		[CompilerGenerated]
		[AccessedThroughProperty("PanMain")]
		private StackPanel m_TestsThread;

		// Token: 0x040008EE RID: 2286
		[CompilerGenerated]
		[AccessedThroughProperty("ItemAboutPcl")]
		private MyListItem m_MapperThread;

		// Token: 0x040008EF RID: 2287
		[CompilerGenerated]
		[AccessedThroughProperty("BtnAboutBmclapi")]
		private MyButton _ThreadThread;

		// Token: 0x040008F0 RID: 2288
		[CompilerGenerated]
		[AccessedThroughProperty("BtnAboutWiki")]
		private MyButton propertyThread;

		// Token: 0x040008F1 RID: 2289
		[CompilerGenerated]
		[AccessedThroughProperty("BtnDonateOutput")]
		private MyButton m_ComposerThread;

		// Token: 0x040008F2 RID: 2290
		[AccessedThroughProperty("BtnDonateDonate")]
		[CompilerGenerated]
		private MyButton _IteratorThread;

		// Token: 0x040008F3 RID: 2291
		[CompilerGenerated]
		[AccessedThroughProperty("BtnDonateCopy")]
		private MyButton repositoryThread;

		// Token: 0x040008F4 RID: 2292
		[AccessedThroughProperty("BtnDonateInput")]
		[CompilerGenerated]
		private MyButton m_TestThread;

		// Token: 0x040008F5 RID: 2293
		private bool _MapThread;
	}
}
