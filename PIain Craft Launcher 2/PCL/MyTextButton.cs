using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020000AB RID: 171
	public class MyTextButton : Label
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000536 RID: 1334 RVA: 0x0002AB3C File Offset: 0x00028D3C
		// (remove) Token: 0x06000537 RID: 1335 RVA: 0x0002AB74 File Offset: 0x00028D74
		public event MyTextButton.ClickEventHandler Click
		{
			[CompilerGenerated]
			add
			{
				MyTextButton.ClickEventHandler clickEventHandler = this._DescriptorBroadcaster;
				MyTextButton.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MyTextButton.ClickEventHandler value2 = (MyTextButton.ClickEventHandler)Delegate.Combine(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MyTextButton.ClickEventHandler>(ref this._DescriptorBroadcaster, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				MyTextButton.ClickEventHandler clickEventHandler = this._DescriptorBroadcaster;
				MyTextButton.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MyTextButton.ClickEventHandler value2 = (MyTextButton.ClickEventHandler)Delegate.Remove(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MyTextButton.ClickEventHandler>(ref this._DescriptorBroadcaster, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0002ABAC File Offset: 0x00028DAC
		public MyTextButton()
		{
			base.PreviewMouseLeftButtonDown += this.MyTextButton_MouseLeftButtonDown;
			base.MouseLeave += delegate(object sender, MouseEventArgs e)
			{
				this.MyTextButton_MouseLeave();
			};
			base.PreviewMouseLeftButtonUp += this.MyTextButton_MouseLeftButtonUp;
			base.MouseEnter += delegate(object sender, MouseEventArgs e)
			{
				this.RefreshColor();
			};
			base.MouseLeave += delegate(object sender, MouseEventArgs e)
			{
				this.RefreshColor();
			};
			base.IsEnabledChanged += delegate(object sender, DependencyPropertyChangedEventArgs e)
			{
				this.RefreshColor();
			};
			base.MouseLeftButtonDown += delegate(object sender, MouseButtonEventArgs e)
			{
				this.RefreshColor();
			};
			base.MouseLeftButtonUp += delegate(object sender, MouseButtonEventArgs e)
			{
				this.RefreshColor();
			};
			this.m_PublisherBroadcaster = ModBase.GetUuid();
			this.m_StrategyBroadcaster = false;
			base.SetResourceReference(Control.ForegroundProperty, "ColorBrush1");
			base.Background = ModSecret._ConsumerField;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00004CF5 File Offset: 0x00002EF5
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x00004D07 File Offset: 0x00002F07
		public string Text
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyTextButton._DefinitionBroadcaster));
			}
			set
			{
				base.SetValue(MyTextButton._DefinitionBroadcaster, value);
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00004D15 File Offset: 0x00002F15
		private void MyTextButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.m_StrategyBroadcaster = true;
			e.Handled = true;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00004D25 File Offset: 0x00002F25
		private void MyTextButton_MouseLeave()
		{
			this.m_StrategyBroadcaster = false;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0002AC84 File Offset: 0x00028E84
		private void MyTextButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (this.m_StrategyBroadcaster)
			{
				this.m_StrategyBroadcaster = false;
				ModBase.Log("[Control] 按下文本按钮：" + this.Text, ModBase.LogLevel.Normal, "出现错误");
				MyTextButton.ClickEventHandler descriptorBroadcaster = this._DescriptorBroadcaster;
				if (descriptorBroadcaster != null)
				{
					descriptorBroadcaster(this, null);
				}
				ModEvent.TryStartEvent(this.EventType, this.EventData);
				e.Handled = true;
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0002ACE8 File Offset: 0x00028EE8
		private void RefreshColor()
		{
			string text;
			int time;
			if (this.m_StrategyBroadcaster)
			{
				text = "ColorBrush4";
				time = 30;
			}
			else if (base.IsMouseOver)
			{
				text = "ColorBrush3";
				time = 100;
			}
			else
			{
				text = "ColorBrush1";
				time = 200;
			}
			if (Operators.CompareString(this.procBroadcaster, text, false) != 0)
			{
				this.procBroadcaster = text;
				if (base.IsLoaded && ModAnimation.CalcParser() == 0)
				{
					ModAnimation.AniStart(ModAnimation.AaColor(this, Control.ForegroundProperty, text, time, 0, null, false), "MyTextButton Color " + Conversions.ToString(this.m_PublisherBroadcaster), false);
					return;
				}
				ModAnimation.AniStop("MyTextButton Color " + Conversions.ToString(this.m_PublisherBroadcaster));
				base.SetResourceReference(Control.ForegroundProperty, text);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00004D2E File Offset: 0x00002F2E
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x00004D40 File Offset: 0x00002F40
		public string EventType
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyTextButton._ParserField));
			}
			set
			{
				base.SetValue(MyTextButton._ParserField, value);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x00004D4E File Offset: 0x00002F4E
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x00004D60 File Offset: 0x00002F60
		public string EventData
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyTextButton.broadcasterField));
			}
			set
			{
				base.SetValue(MyTextButton.broadcasterField, value);
			}
		}

		// Token: 0x040002AC RID: 684
		[CompilerGenerated]
		private MyTextButton.ClickEventHandler _DescriptorBroadcaster;

		// Token: 0x040002AD RID: 685
		public int m_PublisherBroadcaster;

		// Token: 0x040002AE RID: 686
		public static readonly DependencyProperty _DefinitionBroadcaster = DependencyProperty.Register("Text", typeof(string), typeof(MyTextButton), new PropertyMetadata("", delegate(DependencyObject a0, DependencyPropertyChangedEventArgs a1)
		{
			((MyTextButton._Closure$__.$I0-0 == null) ? (MyTextButton._Closure$__.$I0-0 = delegate(MyTextButton sender, DependencyPropertyChangedEventArgs e)
			{
				if (Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(e.OldValue, e.NewValue, false))))
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaOpacity(sender, -sender.Opacity, 50, 0, null, false),
						ModAnimation.AaCode(delegate
						{
							sender.Content = RuntimeHelpers.GetObjectValue(e.NewValue);
						}, 0, true),
						ModAnimation.AaOpacity(sender, 1.0, 170, 0, null, false)
					}, "MyTextButton Text " + Conversions.ToString(sender.m_PublisherBroadcaster), false);
				}
			}) : MyTextButton._Closure$__.$I0-0)((MyTextButton)a0, a1);
		}));

		// Token: 0x040002AF RID: 687
		public bool m_StrategyBroadcaster;

		// Token: 0x040002B0 RID: 688
		private string procBroadcaster;

		// Token: 0x040002B1 RID: 689
		public static readonly DependencyProperty _ParserField = DependencyProperty.Register("EventType", typeof(string), typeof(MyTextButton), new PropertyMetadata(null));

		// Token: 0x040002B2 RID: 690
		public static readonly DependencyProperty broadcasterField = DependencyProperty.Register("EventData", typeof(string), typeof(MyTextButton), new PropertyMetadata(null));

		// Token: 0x020000AC RID: 172
		// (Invoke) Token: 0x0600054C RID: 1356
		public delegate void ClickEventHandler(object sender, EventArgs e);
	}
}
