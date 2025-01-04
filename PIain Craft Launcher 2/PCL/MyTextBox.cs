using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020000A6 RID: 166
	public class MyTextBox : TextBox
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x0002A0F0 File Offset: 0x000282F0
		public MyTextBox()
		{
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.Validate();
			};
			base.TextChanged += delegate(object sender, TextChangedEventArgs e)
			{
				this.MyTextBox_TextChanged((MyTextBox)sender, e);
			};
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
			base.GotFocus += delegate(object sender, RoutedEventArgs e)
			{
				this.RefreshColor();
			};
			base.LostFocus += delegate(object sender, RoutedEventArgs e)
			{
				this.RefreshColor();
			};
			base.IsEnabledChanged += delegate(object sender, DependencyPropertyChangedEventArgs e)
			{
				this.RefreshTextColor();
			};
			this.HasAnimation = true;
			this.ShowValidateResult = true;
			this.bridgeBroadcaster = null;
			this.itemBroadcaster = null;
			this.reponseBroadcaster = ModBase.GetUuid();
			this._ExceptionBroadcaster = new List<RoutedEventHandler>();
			this.classBroadcaster = new Collection<Validate>();
			this.policyBroadcaster = MyTextBox.ValidateState.NotInited;
			this.producerBroadcaster = false;
			this.schemaBroadcaster = "";
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00004B9E File Offset: 0x00002D9E
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x00004BA6 File Offset: 0x00002DA6
		public bool HasAnimation { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00004BAF File Offset: 0x00002DAF
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x00004BB7 File Offset: 0x00002DB7
		public bool ShowValidateResult { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0002A1EC File Offset: 0x000283EC
		private TextBlock labWrong
		{
			get
			{
				TextBlock result;
				if (base.Template == null)
				{
					result = null;
				}
				else
				{
					if (this.bridgeBroadcaster == null)
					{
						this.bridgeBroadcaster = (TextBlock)base.Template.FindName("labWrong", this);
					}
					result = this.bridgeBroadcaster;
				}
				return result;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0002A234 File Offset: 0x00028434
		private TextBlock labHint
		{
			get
			{
				TextBlock result;
				if (base.Template == null)
				{
					result = null;
				}
				else
				{
					if (this.itemBroadcaster == null)
					{
						this.itemBroadcaster = (TextBlock)base.Template.FindName("labHint", this);
					}
					result = this.itemBroadcaster;
				}
				return result;
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0002A27C File Offset: 0x0002847C
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			if (Operators.CompareString(this.HintText, "", false) != 0 && Operators.CompareString(this.labHint.Text, "", false) == 0)
			{
				this.labHint.Text = ((Operators.CompareString(base.Text, "", false) == 0) ? this.HintText : "");
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0002A2E8 File Offset: 0x000284E8
		[CompilerGenerated]
		public static void SortReader(MyTextBox.ValidateChangedEventHandler obj)
		{
			MyTextBox.ValidateChangedEventHandler validateChangedEventHandler = MyTextBox.m_GlobalBroadcaster;
			MyTextBox.ValidateChangedEventHandler validateChangedEventHandler2;
			do
			{
				validateChangedEventHandler2 = validateChangedEventHandler;
				MyTextBox.ValidateChangedEventHandler value = (MyTextBox.ValidateChangedEventHandler)Delegate.Combine(validateChangedEventHandler2, obj);
				validateChangedEventHandler = Interlocked.CompareExchange<MyTextBox.ValidateChangedEventHandler>(ref MyTextBox.m_GlobalBroadcaster, value, validateChangedEventHandler2);
			}
			while (validateChangedEventHandler != validateChangedEventHandler2);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0002A31C File Offset: 0x0002851C
		[CompilerGenerated]
		public static void MapReader(MyTextBox.ValidateChangedEventHandler obj)
		{
			MyTextBox.ValidateChangedEventHandler validateChangedEventHandler = MyTextBox.m_GlobalBroadcaster;
			MyTextBox.ValidateChangedEventHandler validateChangedEventHandler2;
			do
			{
				validateChangedEventHandler2 = validateChangedEventHandler;
				MyTextBox.ValidateChangedEventHandler value = (MyTextBox.ValidateChangedEventHandler)Delegate.Remove(validateChangedEventHandler2, obj);
				validateChangedEventHandler = Interlocked.CompareExchange<MyTextBox.ValidateChangedEventHandler>(ref MyTextBox.m_GlobalBroadcaster, value, validateChangedEventHandler2);
			}
			while (validateChangedEventHandler != validateChangedEventHandler2);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00004BC0 File Offset: 0x00002DC0
		public void SetupReader(RoutedEventHandler value)
		{
			this._ExceptionBroadcaster.Add(value);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00004BCE File Offset: 0x00002DCE
		public void DestroyReader(RoutedEventHandler value)
		{
			this._ExceptionBroadcaster.Remove(value);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0002A350 File Offset: 0x00028550
		private void raise_ValidatedTextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				foreach (RoutedEventHandler routedEventHandler in this._ExceptionBroadcaster)
				{
					if (!Information.IsNothing(routedEventHandler))
					{
						routedEventHandler(RuntimeHelpers.GetObjectValue(sender), e);
					}
				}
			}
			finally
			{
				List<RoutedEventHandler>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00004BDD File Offset: 0x00002DDD
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x00004BEF File Offset: 0x00002DEF
		public string ValidateResult
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyTextBox.m_UtilsBroadcaster));
			}
			set
			{
				base.SetValue(MyTextBox.m_UtilsBroadcaster, value);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00004BFD File Offset: 0x00002DFD
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x00004C05 File Offset: 0x00002E05
		public Collection<Validate> ValidateRules
		{
			get
			{
				return this.classBroadcaster;
			}
			set
			{
				this.classBroadcaster = value;
				this.Validate();
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0002A3B4 File Offset: 0x000285B4
		public void Validate()
		{
			MyTextBox._Closure$__36-0 CS$<>8__locals1 = new MyTextBox._Closure$__36-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Me = this;
			this.ValidateResult = "";
			try
			{
				foreach (Validate validate in this.ValidateRules)
				{
					this.ValidateResult = validate.Validate(base.Text);
					if (Information.IsNothing(this.ValidateResult))
					{
						break;
					}
					if (Operators.CompareString(this.ValidateResult, "", false) != 0)
					{
						break;
					}
				}
			}
			finally
			{
				IEnumerator<Validate> enumerator;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
			CS$<>8__locals1.$VB$Local_IsSuccessful = (Operators.CompareString(this.ValidateResult, "", false) == 0);
			if (this.policyBroadcaster != (CS$<>8__locals1.$VB$Local_IsSuccessful ? MyTextBox.ValidateState.Success : MyTextBox.ValidateState.FailedAndShowDetail))
			{
				if (base.IsLoaded && this.labWrong != null)
				{
					this.ChangeValidateResult(CS$<>8__locals1.$VB$Local_IsSuccessful, true);
				}
				else
				{
					ModBase.RunInNewThread(delegate
					{
						Thread.Sleep(30);
						ModBase.RunInUi((CS$<>8__locals1.$I1 == null) ? (CS$<>8__locals1.$I1 = delegate()
						{
							CS$<>8__locals1.$VB$Me.ChangeValidateResult(CS$<>8__locals1.$VB$Local_IsSuccessful, false);
						}) : CS$<>8__locals1.$I1, false);
					}, "DelayedValidate Change", ThreadPriority.Normal);
				}
			}
			if (this.ShowValidateResult && Operators.CompareString(this.ValidateResult, "", false) != 0)
			{
				if (base.IsLoaded && this.labWrong != null)
				{
					this.labWrong.Text = this.ValidateResult;
					return;
				}
				ModBase.RunInNewThread(delegate
				{
					MyTextBox._Closure$__36-1 CS$<>8__locals2 = new MyTextBox._Closure$__36-1(CS$<>8__locals2);
					CS$<>8__locals2.$VB$Me = this;
					CS$<>8__locals2.$VB$Local_IsFinished = false;
					while (!CS$<>8__locals2.$VB$Local_IsFinished)
					{
						Thread.Sleep(20);
						ModBase.RunInUiWait(delegate()
						{
							if (CS$<>8__locals2.$VB$Me.labWrong != null)
							{
								CS$<>8__locals2.$VB$Me.labWrong.Text = CS$<>8__locals2.$VB$Me.ValidateResult;
								CS$<>8__locals2.$VB$Local_IsFinished = true;
							}
							if (!CS$<>8__locals2.$VB$Me.IsLoaded)
							{
								CS$<>8__locals2.$VB$Local_IsFinished = true;
							}
						});
					}
				}, "DelayedValidate Text", ThreadPriority.Normal);
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00004C14 File Offset: 0x00002E14
		public void ForceShowAsSuccess()
		{
			this.producerBroadcaster = false;
			this.ChangeValidateResult(Operators.CompareString(this.ValidateResult, "", false) == 0, true);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0002A504 File Offset: 0x00028704
		private void ChangeValidateResult(bool IsSuccessful, bool IsLoaded)
		{
			if (IsLoaded && ModAnimation.CalcParser() == 0 && this.labWrong != null)
			{
				if (!IsSuccessful && this.producerBroadcaster)
				{
					if (this.ShowValidateResult)
					{
						this.policyBroadcaster = MyTextBox.ValidateState.FailedAndShowDetail;
						this.labWrong.Visibility = Visibility.Visible;
						ModAnimation.AniStart(new ModAnimation.AniData[]
						{
							ModAnimation.AaOpacity(this.labWrong, 1.0 - this.labWrong.Opacity, 170, 0, null, false),
							ModAnimation.AaHeight(this.labWrong, 21.0 - this.labWrong.Height, 300, 0, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false)
						}, "MyTextBox Validate " + Conversions.ToString(this.reponseBroadcaster), false);
					}
					else
					{
						this.policyBroadcaster = MyTextBox.ValidateState.FailedAndHideDetail;
					}
				}
				else
				{
					this.policyBroadcaster = (IsSuccessful ? MyTextBox.ValidateState.Success : MyTextBox.ValidateState.FailedButTextNotChanged);
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaOpacity(this.labWrong, -this.labWrong.Opacity, 150, 0, null, false),
						ModAnimation.AaHeight(this.labWrong, -this.labWrong.Height, 150, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
						ModAnimation.AaCode(delegate
						{
							this.labWrong.Visibility = Visibility.Collapsed;
						}, 0, true)
					}, "MyTextBox Validate " + Conversions.ToString(this.reponseBroadcaster), false);
				}
			}
			else
			{
				this.policyBroadcaster = MyTextBox.ValidateState.NotLoaded;
			}
			this.RefreshColor();
			MyTextBox.ValidateChangedEventHandler globalBroadcaster = MyTextBox.m_GlobalBroadcaster;
			if (globalBroadcaster != null)
			{
				globalBroadcaster(this, new EventArgs());
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00004C38 File Offset: 0x00002E38
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x00004C40 File Offset: 0x00002E40
		public string HintText
		{
			get
			{
				return this.schemaBroadcaster;
			}
			set
			{
				this.schemaBroadcaster = value;
				if (this.labHint != null)
				{
					this.labHint.Text = ((Operators.CompareString(base.Text, "", false) == 0) ? this.HintText : "");
				}
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0002A6B0 File Offset: 0x000288B0
		private void MyTextBox_TextChanged(MyTextBox sender, TextChangedEventArgs e)
		{
			try
			{
				if (this.labHint != null)
				{
					this.labHint.Text = ((Operators.CompareString(base.Text, "", false) == 0) ? this.HintText : "");
				}
				this.producerBroadcaster = base.IsLoaded;
				this.Validate();
				if (Operators.CompareString(this.ValidateResult, "", false) == 0)
				{
					this.raise_ValidatedTextChanged(sender, e);
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "进行输入验证时出错", ModBase.LogLevel.Assert, "出现错误");
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0002A750 File Offset: 0x00028950
		private void RefreshColor()
		{
			try
			{
				if (this.HasAnimation)
				{
					if (base.TemplatedParent == null || !(base.TemplatedParent is MyComboBox))
					{
						string text;
						string text2;
						int time;
						if (base.IsEnabled)
						{
							if (Operators.CompareString(this.ValidateResult, "", false) != 0 && this.producerBroadcaster)
							{
								text = "ColorBrushRedLight";
								text2 = "ColorBrushRedBack";
								time = 200;
							}
							else if (base.IsFocused)
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
								ModAnimation.AaColor(this, Control.BorderBrushProperty, text, time, 0, null, false),
								ModAnimation.AaColor(this, Control.BackgroundProperty, text2, time, 0, null, false)
							}, "MyTextBox Color " + Conversions.ToString(this.reponseBroadcaster), false);
						}
						else
						{
							ModAnimation.AniStop("MyTextBox Color " + Conversions.ToString(this.reponseBroadcaster));
							base.SetResourceReference(Control.BorderBrushProperty, text);
							base.SetResourceReference(Control.BackgroundProperty, text2);
						}
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "文本框颜色改变出错", ModBase.LogLevel.Debug, "出现错误");
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0002A8E0 File Offset: 0x00028AE0
		private void RefreshTextColor()
		{
			ModBase.MyColor myColor = base.IsEnabled ? ModSecret.m_RefField : ModSecret.m_StateField;
			if ((double)((SolidColorBrush)base.Foreground).Color.R != myColor.m_ThreadError)
			{
				if (base.IsLoaded && ModAnimation.CalcParser() == 0 && Operators.CompareString(base.Text, "", false) != 0)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaColor(this, Control.ForegroundProperty, base.IsEnabled ? "ColorBrushGray1" : "ColorBrushGray4", 200, 0, null, false)
					}, "MyTextBox TextColor " + Conversions.ToString(this.reponseBroadcaster), false);
					return;
				}
				ModAnimation.AniStop("MyTextBox TextColor " + Conversions.ToString(this.reponseBroadcaster));
				base.Foreground = myColor;
			}
		}

		// Token: 0x04000294 RID: 660
		[CompilerGenerated]
		private bool m_ValueBroadcaster;

		// Token: 0x04000295 RID: 661
		[CompilerGenerated]
		private bool _ObjectBroadcaster;

		// Token: 0x04000296 RID: 662
		private TextBlock bridgeBroadcaster;

		// Token: 0x04000297 RID: 663
		private TextBlock itemBroadcaster;

		// Token: 0x04000298 RID: 664
		public int reponseBroadcaster;

		// Token: 0x04000299 RID: 665
		[CompilerGenerated]
		private static MyTextBox.ValidateChangedEventHandler m_GlobalBroadcaster;

		// Token: 0x0400029A RID: 666
		public List<RoutedEventHandler> _ExceptionBroadcaster;

		// Token: 0x0400029B RID: 667
		public static readonly DependencyProperty m_UtilsBroadcaster = DependencyProperty.Register("ValidateResult", typeof(string), typeof(MyTextBox), new PropertyMetadata(""));

		// Token: 0x0400029C RID: 668
		private Collection<Validate> classBroadcaster;

		// Token: 0x0400029D RID: 669
		private MyTextBox.ValidateState policyBroadcaster;

		// Token: 0x0400029E RID: 670
		private bool producerBroadcaster;

		// Token: 0x0400029F RID: 671
		private string schemaBroadcaster;

		// Token: 0x020000A7 RID: 167
		// (Invoke) Token: 0x0600052C RID: 1324
		public delegate void ValidateChangedEventHandler(object sender, EventArgs e);

		// Token: 0x020000A8 RID: 168
		private enum ValidateState
		{
			// Token: 0x040002A1 RID: 673
			NotInited,
			// Token: 0x040002A2 RID: 674
			Success,
			// Token: 0x040002A3 RID: 675
			FailedButTextNotChanged,
			// Token: 0x040002A4 RID: 676
			FailedAndShowDetail,
			// Token: 0x040002A5 RID: 677
			FailedAndHideDetail,
			// Token: 0x040002A6 RID: 678
			NotLoaded
		}
	}
}
