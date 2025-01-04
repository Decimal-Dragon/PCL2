using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020000A4 RID: 164
	[DesignerGenerated]
	public class MySearchBox : MyCard, IComponentConnector
	{
		// Token: 0x060004EE RID: 1262 RVA: 0x00004AAC File Offset: 0x00002CAC
		public MySearchBox()
		{
			base.Loaded += this.MySearchBox_Loaded;
			this.InitializeComponent();
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00029F00 File Offset: 0x00028100
		[CompilerGenerated]
		public void ChangeField(MySearchBox.TextChangedEventHandler obj)
		{
			MySearchBox.TextChangedEventHandler textChangedEventHandler = this.productBroadcaster;
			MySearchBox.TextChangedEventHandler textChangedEventHandler2;
			do
			{
				textChangedEventHandler2 = textChangedEventHandler;
				MySearchBox.TextChangedEventHandler value = (MySearchBox.TextChangedEventHandler)Delegate.Combine(textChangedEventHandler2, obj);
				textChangedEventHandler = Interlocked.CompareExchange<MySearchBox.TextChangedEventHandler>(ref this.productBroadcaster, value, textChangedEventHandler2);
			}
			while (textChangedEventHandler != textChangedEventHandler2);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00029F38 File Offset: 0x00028138
		[CompilerGenerated]
		public void RegisterField(MySearchBox.TextChangedEventHandler obj)
		{
			MySearchBox.TextChangedEventHandler textChangedEventHandler = this.productBroadcaster;
			MySearchBox.TextChangedEventHandler textChangedEventHandler2;
			do
			{
				textChangedEventHandler2 = textChangedEventHandler;
				MySearchBox.TextChangedEventHandler value = (MySearchBox.TextChangedEventHandler)Delegate.Remove(textChangedEventHandler2, obj);
				textChangedEventHandler = Interlocked.CompareExchange<MySearchBox.TextChangedEventHandler>(ref this.productBroadcaster, value, textChangedEventHandler2);
			}
			while (textChangedEventHandler != textChangedEventHandler2);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00004ACD File Offset: 0x00002CCD
		private void MySearchBox_Loaded(object sender, RoutedEventArgs e)
		{
			this.TextBox.Focus();
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00004ADB File Offset: 0x00002CDB
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x00004AE8 File Offset: 0x00002CE8
		public string HintText
		{
			get
			{
				return this.TextBox.HintText;
			}
			set
			{
				this.TextBox.HintText = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00004AF6 File Offset: 0x00002CF6
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x00004B03 File Offset: 0x00002D03
		public string Text
		{
			get
			{
				return this.TextBox.Text;
			}
			set
			{
				this.TextBox.Text = value;
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00029F70 File Offset: 0x00028170
		private void Text_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (string.IsNullOrEmpty(this.TextBox.Text))
			{
				ModAnimation.AniStart(ModAnimation.AaOpacity(this.BtnClear, -this.BtnClear.Opacity, 90, 0, null, false), "MySearchBox ClearBtn " + Conversions.ToString(this.indexer), false);
				this.BtnClear.IsHitTestVisible = false;
			}
			else
			{
				ModAnimation.AniStart(ModAnimation.AaOpacity(this.BtnClear, 1.0 - this.BtnClear.Opacity, 90, 0, null, false), "MySearchBox ClearBtn " + Conversions.ToString(this.indexer), false);
				this.BtnClear.IsHitTestVisible = true;
			}
			MySearchBox.TextChangedEventHandler textChangedEventHandler = this.productBroadcaster;
			if (textChangedEventHandler != null)
			{
				textChangedEventHandler(RuntimeHelpers.GetObjectValue(sender), e);
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00004B11 File Offset: 0x00002D11
		private void BtnClear_Click(object sender, EventArgs e)
		{
			this.TextBox.Text = "";
			this.TextBox.Focus();
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00004B2F File Offset: 0x00002D2F
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x0002A038 File Offset: 0x00028238
		internal virtual MyTextBox TextBox
		{
			[CompilerGenerated]
			get
			{
				return this.m_ListenerBroadcaster;
			}
			[CompilerGenerated]
			set
			{
				System.Windows.Controls.TextChangedEventHandler value2 = new System.Windows.Controls.TextChangedEventHandler(this.Text_TextChanged);
				MyTextBox listenerBroadcaster = this.m_ListenerBroadcaster;
				if (listenerBroadcaster != null)
				{
					listenerBroadcaster.TextChanged -= value2;
				}
				this.m_ListenerBroadcaster = value;
				listenerBroadcaster = this.m_ListenerBroadcaster;
				if (listenerBroadcaster != null)
				{
					listenerBroadcaster.TextChanged += value2;
				}
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00004B37 File Offset: 0x00002D37
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x0002A07C File Offset: 0x0002827C
		internal virtual MyIconButton BtnClear
		{
			[CompilerGenerated]
			get
			{
				return this._CollectionBroadcaster;
			}
			[CompilerGenerated]
			set
			{
				MyIconButton.ClickEventHandler value2 = new MyIconButton.ClickEventHandler(this.BtnClear_Click);
				MyIconButton collectionBroadcaster = this._CollectionBroadcaster;
				if (collectionBroadcaster != null)
				{
					collectionBroadcaster.Click -= value2;
				}
				this._CollectionBroadcaster = value;
				collectionBroadcaster = this._CollectionBroadcaster;
				if (collectionBroadcaster != null)
				{
					collectionBroadcaster.Click += value2;
				}
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0002A0C0 File Offset: 0x000282C0
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.visitorBroadcaster)
			{
				this.visitorBroadcaster = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/mysearchbox.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00004B3F File Offset: 0x00002D3F
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.TextBox = (MyTextBox)target;
				return;
			}
			if (connectionId == 2)
			{
				this.BtnClear = (MyIconButton)target;
				return;
			}
			this.visitorBroadcaster = true;
		}

		// Token: 0x04000290 RID: 656
		[CompilerGenerated]
		private MySearchBox.TextChangedEventHandler productBroadcaster;

		// Token: 0x04000291 RID: 657
		[CompilerGenerated]
		[AccessedThroughProperty("TextBox")]
		private MyTextBox m_ListenerBroadcaster;

		// Token: 0x04000292 RID: 658
		[AccessedThroughProperty("BtnClear")]
		[CompilerGenerated]
		private MyIconButton _CollectionBroadcaster;

		// Token: 0x04000293 RID: 659
		private bool visitorBroadcaster;

		// Token: 0x020000A5 RID: 165
		// (Invoke) Token: 0x06000503 RID: 1283
		public delegate void TextChangedEventHandler(object sender, EventArgs e);
	}
}
