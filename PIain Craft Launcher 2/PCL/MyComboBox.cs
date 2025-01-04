using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x02000019 RID: 25
	public class MyComboBox : ComboBox
	{
		// Token: 0x0600007B RID: 123 RVA: 0x0000E404 File Offset: 0x0000C604
		public MyComboBox()
		{
			base.PreviewMouseLeftButtonDown += this.MyComboBox_PreviewMouseLeftButtonDown;
			base.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(this.MyComboBox_PreviewMouseLeftButtonUp);
			base.MouseLeave += new MouseEventHandler(this.MyComboBox_PreviewMouseLeftButtonUp);
			base.IsEnabledChanged += delegate(object sender, DependencyPropertyChangedEventArgs e)
			{
				this.RefreshColor();
			};
			base.MouseEnter += delegate(object sender, MouseEventArgs e)
			{
				this.RefreshColor();
			};
			base.MouseLeave += delegate(object sender, MouseEventArgs e)
			{
				this.RefreshColor();
			};
			base.PreviewMouseLeftButtonDown += delegate(object sender, MouseButtonEventArgs e)
			{
				this.RefreshColor();
			};
			base.PreviewMouseLeftButtonUp += delegate(object sender, MouseButtonEventArgs e)
			{
				this.RefreshColor();
			};
			base.GotKeyboardFocus += delegate(object sender, KeyboardFocusChangedEventArgs e)
			{
				this.RefreshColor();
			};
			base.DropDownOpened += this.MyComboBox_DropDownOpened;
			base.DropDownClosed += this.MyComboBox_DropDownClosed;
			this.DeleteParser(new MyComboBox.TextChangedEventHandler(this.MyComboBox_TextChanged));
			this.m_Mapper = ModBase.GetUuid();
			this.HintText = "";
			this.m_Composer = Conversions.ToString(base.SelectedItem);
			this._Iterator = false;
			this.m_Test = false;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000E528 File Offset: 0x0000C728
		[CompilerGenerated]
		public void DeleteParser(MyComboBox.TextChangedEventHandler obj)
		{
			MyComboBox.TextChangedEventHandler textChangedEventHandler = this._Tests;
			MyComboBox.TextChangedEventHandler textChangedEventHandler2;
			do
			{
				textChangedEventHandler2 = textChangedEventHandler;
				MyComboBox.TextChangedEventHandler value = (MyComboBox.TextChangedEventHandler)Delegate.Combine(textChangedEventHandler2, obj);
				textChangedEventHandler = Interlocked.CompareExchange<MyComboBox.TextChangedEventHandler>(ref this._Tests, value, textChangedEventHandler2);
			}
			while (textChangedEventHandler != textChangedEventHandler2);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000E560 File Offset: 0x0000C760
		[CompilerGenerated]
		public void ValidateParser(MyComboBox.TextChangedEventHandler obj)
		{
			MyComboBox.TextChangedEventHandler textChangedEventHandler = this._Tests;
			MyComboBox.TextChangedEventHandler textChangedEventHandler2;
			do
			{
				textChangedEventHandler2 = textChangedEventHandler;
				MyComboBox.TextChangedEventHandler value = (MyComboBox.TextChangedEventHandler)Delegate.Remove(textChangedEventHandler2, obj);
				textChangedEventHandler = Interlocked.CompareExchange<MyComboBox.TextChangedEventHandler>(ref this._Tests, value, textChangedEventHandler2);
			}
			while (textChangedEventHandler != textChangedEventHandler2);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000269E File Offset: 0x0000089E
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000026A6 File Offset: 0x000008A6
		public string HintText { get; set; }

		// Token: 0x06000080 RID: 128 RVA: 0x0000E598 File Offset: 0x0000C798
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			if (base.IsEditable)
			{
				try
				{
					this.TextBox = (MyTextBox)base.Template.FindName("PART_EditableTextBox", this);
					this.TextBox.AddHandler(UIElement.LostFocusEvent, new RoutedEventHandler(delegate(object sender, RoutedEventArgs e)
					{
						this.RefreshColor();
					}));
					this.TextBox._ExceptionBroadcaster.Add(delegate(object sender, RoutedEventArgs e)
					{
						MyComboBox.TextChangedEventHandler tests2 = this._Tests;
						if (tests2 != null)
						{
							tests2(RuntimeHelpers.GetObjectValue(sender), (TextChangedEventArgs)e);
						}
					});
					this.TextBox.Tag = RuntimeHelpers.GetObjectValue(base.Tag);
					if (Operators.CompareString(this.Text, "", false) == 0)
					{
						this.TextBox.Text = this.m_Composer;
					}
					else
					{
						MyComboBox.TextChangedEventHandler tests = this._Tests;
						if (tests != null)
						{
							tests(this, null);
						}
					}
					if (this.HintText.Length > 0)
					{
						this.TextBox.HintText = this.HintText;
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "初始化可编辑文本框失败（" + base.Name + "）", ModBase.LogLevel.Feedback, "出现错误");
				}
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000E6BC File Offset: 0x0000C8BC
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000026AF File Offset: 0x000008AF
		public new string Text
		{
			get
			{
				string result;
				if (base.IsEditable)
				{
					if (this.TextBox == null)
					{
						result = (this.m_Composer ?? "");
					}
					else
					{
						result = (this.TextBox.Text ?? "");
					}
				}
				else
				{
					result = (base.SelectedItem ?? "").ToString();
				}
				return result;
			}
			set
			{
				if (!base.IsEditable)
				{
					throw new NotSupportedException("该 ComboBox 不支持修改文本。");
				}
				if (Information.IsNothing(this.TextBox))
				{
					this.m_Composer = value;
					return;
				}
				this.TextBox.Text = value;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000026E5 File Offset: 0x000008E5
		private void MyComboBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this._Iterator = true;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000026EE File Offset: 0x000008EE
		private void MyComboBox_PreviewMouseLeftButtonUp(object sender, EventArgs e)
		{
			this._Iterator = false;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000E718 File Offset: 0x0000C918
		public void RefreshColor()
		{
			string text;
			string text2;
			int time;
			if (base.IsEnabled)
			{
				if (Conversions.ToBoolean(this._Iterator || base.IsDropDownOpen || (base.IsEditable && Conversions.ToBoolean(NewLateBinding.LateGet(base.Template.FindName("PART_EditableTextBox", this), null, "IsFocused", new object[0], null, null, null)))))
				{
					text = "ColorBrush3";
					text2 = "ColorBrush7";
					time = 10;
				}
				else if (base.IsMouseOver)
				{
					text = "ColorBrush4";
					text2 = "ColorBrush7";
					time = 100;
				}
				else
				{
					text = "ColorBrushBg0";
					text2 = "ColorBrushHalfWhite";
					time = 100;
				}
			}
			else
			{
				text = "ColorBrushGray5";
				text2 = "ColorBrushGray6";
				time = 200;
			}
			if (base.IsLoaded && ModAnimation.CalcParser() == 0)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaColor(this, Control.ForegroundProperty, text, time, 0, null, false),
					ModAnimation.AaColor(this, Control.BackgroundProperty, text2, time, 0, null, false)
				}, "MyComboBox Color " + Conversions.ToString(this.m_Mapper), false);
				return;
			}
			ModAnimation.AniStop("MyComboBox Color " + Conversions.ToString(this.m_Mapper));
			base.SetResourceReference(Control.ForegroundProperty, text);
			base.SetResourceReference(Control.BackgroundProperty, text2);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000E864 File Offset: 0x0000CA64
		private void MyComboBox_DropDownOpened(object sender, EventArgs e)
		{
			this._Repository = base.Width;
			base.Width = base.ActualWidth;
			try
			{
				((Grid)base.Template.FindName("PanPopup", this)).Opacity = ModMain._ProcessIterator.Opacity;
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "设置下拉框透明度失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000026F7 File Offset: 0x000008F7
		private void MyComboBox_DropDownClosed(object sender, EventArgs e)
		{
			base.Width = this._Repository;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000E8E0 File Offset: 0x0000CAE0
		private void MyComboBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!this.m_Test && base.IsEditable && base.SelectedItem != null && Operators.CompareString(this.Text, base.SelectedItem.ToString(), false) != 0)
			{
				string text = this.Text;
				int selectionStart = this.TextBox.SelectionStart;
				this.m_Test = true;
				base.SelectedItem = null;
				this.Text = text;
				this.TextBox.SelectionStart = selectionStart;
				this.m_Test = false;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002705 File Offset: 0x00000905
		public ContentPresenter ResetParser()
		{
			return (ContentPresenter)base.Template.FindName("PART_Content", this);
		}

		// Token: 0x0400000A RID: 10
		[CompilerGenerated]
		private MyComboBox.TextChangedEventHandler _Tests;

		// Token: 0x0400000B RID: 11
		public int m_Mapper;

		// Token: 0x0400000C RID: 12
		private MyTextBox TextBox;

		// Token: 0x0400000D RID: 13
		[CompilerGenerated]
		private string _Thread;

		// Token: 0x0400000E RID: 14
		private string m_Composer;

		// Token: 0x0400000F RID: 15
		private bool _Iterator;

		// Token: 0x04000010 RID: 16
		private double _Repository;

		// Token: 0x04000011 RID: 17
		private bool m_Test;

		// Token: 0x0200001A RID: 26
		// (Invoke) Token: 0x06000096 RID: 150
		public delegate void TextChangedEventHandler(object sender, TextChangedEventArgs e);
	}
}
