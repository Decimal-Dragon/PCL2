using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020001E0 RID: 480
	[DesignerGenerated]
	public class MyRadioButton : Border, IComponentConnector
	{
		// Token: 0x06001657 RID: 5719 RVA: 0x000929FC File Offset: 0x00090BFC
		public MyRadioButton()
		{
			base.MouseLeftButtonUp += delegate(object sender, MouseButtonEventArgs e)
			{
				this.Radiobox_MouseUp();
			};
			base.MouseLeftButtonDown += delegate(object sender, MouseButtonEventArgs e)
			{
				this.Radiobox_MouseDown();
			};
			base.MouseLeave += delegate(object sender, MouseEventArgs e)
			{
				this.Radiobox_MouseLeave();
			};
			base.MouseEnter += new MouseEventHandler(this.RefreshColor);
			base.MouseLeave += new MouseEventHandler(this.RefreshColor);
			base.Loaded += new RoutedEventHandler(this.RefreshColor);
			this.issuerIterator = ModBase.GetUuid();
			this.serializerIterator = 1.0;
			this.watcherIterator = false;
			this.m_SystemIterator = MyRadioButton.ColorState.White;
			this.m_TagIterator = false;
			this.InitializeComponent();
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00092AB4 File Offset: 0x00090CB4
		[CompilerGenerated]
		public void LogoutTests(MyRadioButton.CheckEventHandler obj)
		{
			MyRadioButton.CheckEventHandler checkEventHandler = this._IndexerIterator;
			MyRadioButton.CheckEventHandler checkEventHandler2;
			do
			{
				checkEventHandler2 = checkEventHandler;
				MyRadioButton.CheckEventHandler value = (MyRadioButton.CheckEventHandler)Delegate.Combine(checkEventHandler2, obj);
				checkEventHandler = Interlocked.CompareExchange<MyRadioButton.CheckEventHandler>(ref this._IndexerIterator, value, checkEventHandler2);
			}
			while (checkEventHandler != checkEventHandler2);
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x00092AEC File Offset: 0x00090CEC
		[CompilerGenerated]
		public void ResolveTests(MyRadioButton.CheckEventHandler obj)
		{
			MyRadioButton.CheckEventHandler checkEventHandler = this._IndexerIterator;
			MyRadioButton.CheckEventHandler checkEventHandler2;
			do
			{
				checkEventHandler2 = checkEventHandler;
				MyRadioButton.CheckEventHandler value = (MyRadioButton.CheckEventHandler)Delegate.Remove(checkEventHandler2, obj);
				checkEventHandler = Interlocked.CompareExchange<MyRadioButton.CheckEventHandler>(ref this._IndexerIterator, value, checkEventHandler2);
			}
			while (checkEventHandler != checkEventHandler2);
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x00092B24 File Offset: 0x00090D24
		[CompilerGenerated]
		public void ForgotTests(MyRadioButton.ChangeEventHandler obj)
		{
			MyRadioButton.ChangeEventHandler changeEventHandler = this._InterpreterIterator;
			MyRadioButton.ChangeEventHandler changeEventHandler2;
			do
			{
				changeEventHandler2 = changeEventHandler;
				MyRadioButton.ChangeEventHandler value = (MyRadioButton.ChangeEventHandler)Delegate.Combine(changeEventHandler2, obj);
				changeEventHandler = Interlocked.CompareExchange<MyRadioButton.ChangeEventHandler>(ref this._InterpreterIterator, value, changeEventHandler2);
			}
			while (changeEventHandler != changeEventHandler2);
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00092B5C File Offset: 0x00090D5C
		[CompilerGenerated]
		public void ViewTests(MyRadioButton.ChangeEventHandler obj)
		{
			MyRadioButton.ChangeEventHandler changeEventHandler = this._InterpreterIterator;
			MyRadioButton.ChangeEventHandler changeEventHandler2;
			do
			{
				changeEventHandler2 = changeEventHandler;
				MyRadioButton.ChangeEventHandler value = (MyRadioButton.ChangeEventHandler)Delegate.Remove(changeEventHandler2, obj);
				changeEventHandler = Interlocked.CompareExchange<MyRadioButton.ChangeEventHandler>(ref this._InterpreterIterator, value, changeEventHandler2);
			}
			while (changeEventHandler != changeEventHandler2);
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x00092B94 File Offset: 0x00090D94
		public void RaiseChange()
		{
			MyRadioButton.ChangeEventHandler interpreterIterator = this._InterpreterIterator;
			if (interpreterIterator != null)
			{
				interpreterIterator(this, false);
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x0000C89A File Offset: 0x0000AA9A
		// (set) Token: 0x0600165E RID: 5726 RVA: 0x0000C8AC File Offset: 0x0000AAAC
		public string Logo
		{
			get
			{
				return this.ShapeLogo.Data.ToString();
			}
			set
			{
				this.ShapeLogo.Data = (Geometry)new GeometryConverter().ConvertFromString(value);
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x0000C8C9 File Offset: 0x0000AAC9
		// (set) Token: 0x06001660 RID: 5728 RVA: 0x0000C8D1 File Offset: 0x0000AAD1
		public double LogoScale
		{
			get
			{
				return this.serializerIterator;
			}
			set
			{
				this.serializerIterator = value;
				if (!Information.IsNothing(this.ShapeLogo))
				{
					this.ShapeLogo.RenderTransform = new ScaleTransform
					{
						ScaleX = this.LogoScale,
						ScaleY = this.LogoScale
					};
				}
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x0000C90F File Offset: 0x0000AB0F
		// (set) Token: 0x06001662 RID: 5730 RVA: 0x0000C917 File Offset: 0x0000AB17
		public bool Checked
		{
			get
			{
				return this.watcherIterator;
			}
			set
			{
				this.SetChecked(value, false, true);
			}
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x00092BB4 File Offset: 0x00090DB4
		public void SetChecked(bool value, bool user, bool anime)
		{
			checked
			{
				try
				{
					bool flag = false;
					if (base.IsLoaded && value != this.watcherIterator)
					{
						MyRadioButton.ChangeEventHandler interpreterIterator = this._InterpreterIterator;
						if (interpreterIterator != null)
						{
							interpreterIterator(this, user);
						}
					}
					if (value != this.watcherIterator)
					{
						this.watcherIterator = value;
						flag = true;
					}
					if (!Information.IsNothing(base.Parent))
					{
						List<MyRadioButton> list = new List<MyRadioButton>();
						int num = 0;
						try
						{
							foreach (object obj in ((IEnumerable)NewLateBinding.LateGet(base.Parent, null, "Children", new object[0], null, null, null)))
							{
								object objectValue = RuntimeHelpers.GetObjectValue(obj);
								if (objectValue is MyRadioButton)
								{
									list.Add((MyRadioButton)objectValue);
									if (Conversions.ToBoolean(NewLateBinding.LateGet(objectValue, null, "Checked", new object[0], null, null, null)))
									{
										num++;
									}
								}
							}
						}
						finally
						{
							IEnumerator enumerator;
							if (enumerator is IDisposable)
							{
								(enumerator as IDisposable).Dispose();
							}
						}
						int num2 = num;
						if (num2 == 0)
						{
							list[0].Checked = true;
						}
						else if (num2 > 1)
						{
							if (this.Checked)
							{
								try
								{
									foreach (MyRadioButton myRadioButton in list)
									{
										if (myRadioButton.Checked && !myRadioButton.Equals(this))
										{
											myRadioButton.Checked = false;
										}
									}
									goto IL_198;
								}
								finally
								{
									List<MyRadioButton>.Enumerator enumerator2;
									((IDisposable)enumerator2).Dispose();
								}
							}
							bool flag2 = false;
							try
							{
								foreach (MyRadioButton myRadioButton2 in list)
								{
									if (myRadioButton2.Checked)
									{
										if (flag2)
										{
											myRadioButton2.Checked = false;
										}
										else
										{
											flag2 = true;
										}
									}
								}
							}
							finally
							{
								List<MyRadioButton>.Enumerator enumerator3;
								((IDisposable)enumerator3).Dispose();
							}
						}
						IL_198:
						if (flag)
						{
							this.RefreshColor(null, anime);
							if (this.Checked)
							{
								MyRadioButton.CheckEventHandler indexerIterator = this._IndexerIterator;
								if (indexerIterator != null)
								{
									indexerIterator(this, user);
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "单选按钮勾选改变错误", ModBase.LogLevel.Hint, "出现错误");
				}
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x0000C922 File Offset: 0x0000AB22
		// (set) Token: 0x06001665 RID: 5733 RVA: 0x0000C934 File Offset: 0x0000AB34
		public string Text
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyRadioButton._IdentifierIterator));
			}
			set
			{
				base.SetValue(MyRadioButton._IdentifierIterator, value);
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x0000C942 File Offset: 0x0000AB42
		// (set) Token: 0x06001667 RID: 5735 RVA: 0x0000C94A File Offset: 0x0000AB4A
		public MyRadioButton.ColorState ColorType
		{
			get
			{
				return this.m_SystemIterator;
			}
			set
			{
				this.m_SystemIterator = value;
				this.RefreshColor(null, null);
			}
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00092E10 File Offset: 0x00091010
		[CompilerGenerated]
		public void ResetTests(MyRadioButton.PreviewClickEventHandler obj)
		{
			MyRadioButton.PreviewClickEventHandler previewClickEventHandler = this.m_ParamIterator;
			MyRadioButton.PreviewClickEventHandler previewClickEventHandler2;
			do
			{
				previewClickEventHandler2 = previewClickEventHandler;
				MyRadioButton.PreviewClickEventHandler value = (MyRadioButton.PreviewClickEventHandler)Delegate.Combine(previewClickEventHandler2, obj);
				previewClickEventHandler = Interlocked.CompareExchange<MyRadioButton.PreviewClickEventHandler>(ref this.m_ParamIterator, value, previewClickEventHandler2);
			}
			while (previewClickEventHandler != previewClickEventHandler2);
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00092E48 File Offset: 0x00091048
		[CompilerGenerated]
		public void InsertTests(MyRadioButton.PreviewClickEventHandler obj)
		{
			MyRadioButton.PreviewClickEventHandler previewClickEventHandler = this.m_ParamIterator;
			MyRadioButton.PreviewClickEventHandler previewClickEventHandler2;
			do
			{
				previewClickEventHandler2 = previewClickEventHandler;
				MyRadioButton.PreviewClickEventHandler value = (MyRadioButton.PreviewClickEventHandler)Delegate.Remove(previewClickEventHandler2, obj);
				previewClickEventHandler = Interlocked.CompareExchange<MyRadioButton.PreviewClickEventHandler>(ref this.m_ParamIterator, value, previewClickEventHandler2);
			}
			while (previewClickEventHandler != previewClickEventHandler2);
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00092E80 File Offset: 0x00091080
		private void Radiobox_MouseUp()
		{
			if (!this.Checked && this.m_TagIterator)
			{
				ModBase.Log("[Control] 按下单选按钮：" + this.Text, ModBase.LogLevel.Normal, "出现错误");
				this.m_TagIterator = false;
				ModBase.RouteEventArgs routeEventArgs = new ModBase.RouteEventArgs(true);
				MyRadioButton.PreviewClickEventHandler paramIterator = this.m_ParamIterator;
				if (paramIterator != null)
				{
					paramIterator(this, routeEventArgs);
				}
				if (!routeEventArgs.m_SerializerError)
				{
					this.SetChecked(true, true, true);
				}
			}
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x0000C95B File Offset: 0x0000AB5B
		private void Radiobox_MouseDown()
		{
			if (!this.Checked)
			{
				this.m_TagIterator = true;
				this.RefreshColor(null, null);
			}
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x0000C974 File Offset: 0x0000AB74
		private void Radiobox_MouseLeave()
		{
			this.m_TagIterator = false;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00092EEC File Offset: 0x000910EC
		private void RefreshColor(object obj = null, object e = null)
		{
			try
			{
				if (base.IsLoaded && ModAnimation.CalcParser() == 0 && !false.Equals(RuntimeHelpers.GetObjectValue(e)))
				{
					MyRadioButton.ColorState colorType = this.ColorType;
					if (colorType != MyRadioButton.ColorState.White)
					{
						if (colorType == MyRadioButton.ColorState.Highlight)
						{
							if (this.Checked)
							{
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaColor(this.ShapeLogo, Shape.FillProperty, new ModBase.MyColor(255.0, 255.0, 255.0) - this.ShapeLogo.Fill, 120, 0, null, false),
									ModAnimation.AaColor(this.LabText, TextBlock.ForegroundProperty, new ModBase.MyColor(255.0, 255.0, 255.0) - this.LabText.Foreground, 120, 0, null, false)
								}, "MyRadioButton Checked " + Conversions.ToString(this.issuerIterator), false);
								ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, "ColorBrush3", 120, 0, null, false), "MyRadioButton Color " + Conversions.ToString(this.issuerIterator), false);
							}
							else if (this.m_TagIterator)
							{
								ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, "ColorBrush6", 90, 0, null, false), "MyRadioButton Color " + Conversions.ToString(this.issuerIterator), false);
							}
							else if (base.IsMouseOver)
							{
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaColor(this.ShapeLogo, Shape.FillProperty, "ColorBrush3", 90, 0, null, false),
									ModAnimation.AaColor(this.LabText, TextBlock.ForegroundProperty, "ColorBrush3", 90, 0, null, false)
								}, "MyRadioButton Checked " + Conversions.ToString(this.issuerIterator), false);
								ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, "ColorBrush7", 90, 0, null, false), "MyRadioButton Color " + Conversions.ToString(this.issuerIterator), false);
							}
							else
							{
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaColor(this.ShapeLogo, Shape.FillProperty, "ColorBrush3", 150, 0, null, false),
									ModAnimation.AaColor(this.LabText, TextBlock.ForegroundProperty, "ColorBrush3", 150, 0, null, false)
								}, "MyRadioButton Checked " + Conversions.ToString(this.issuerIterator), false);
								ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, ModSecret._ConsumerField - base.Background, 150, 0, null, false), "MyRadioButton Color " + Conversions.ToString(this.issuerIterator), false);
							}
						}
					}
					else if (this.Checked)
					{
						ModAnimation.AniStart(new ModAnimation.AniData[]
						{
							ModAnimation.AaColor(this.ShapeLogo, Shape.FillProperty, "ColorBrush3", 120, 0, null, false),
							ModAnimation.AaColor(this.LabText, TextBlock.ForegroundProperty, "ColorBrush3", 120, 0, null, false)
						}, "MyRadioButton Checked " + Conversions.ToString(this.issuerIterator), false);
						ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, new ModBase.MyColor(255.0, 255.0, 255.0) - base.Background, 120, 0, null, false), "MyRadioButton Color " + Conversions.ToString(this.issuerIterator), false);
					}
					else if (this.m_TagIterator)
					{
						ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, new ModBase.MyColor(120.0, ModSecret.m_ObserverField) - base.Background, 60, 0, null, false), "MyRadioButton Color " + Conversions.ToString(this.issuerIterator), false);
					}
					else if (base.IsMouseOver)
					{
						ModAnimation.AniStart(new ModAnimation.AniData[]
						{
							ModAnimation.AaColor(this.ShapeLogo, Shape.FillProperty, new ModBase.MyColor(255.0, 255.0, 255.0) - this.ShapeLogo.Fill, 90, 0, null, false),
							ModAnimation.AaColor(this.LabText, TextBlock.ForegroundProperty, new ModBase.MyColor(255.0, 255.0, 255.0) - this.LabText.Foreground, 90, 0, null, false)
						}, "MyRadioButton Checked " + Conversions.ToString(this.issuerIterator), false);
						ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, new ModBase.MyColor(50.0, ModSecret.m_ObserverField) - base.Background, 90, 0, null, false), "MyRadioButton Color " + Conversions.ToString(this.issuerIterator), false);
					}
					else
					{
						ModAnimation.AniStart(new ModAnimation.AniData[]
						{
							ModAnimation.AaColor(this.ShapeLogo, Shape.FillProperty, new ModBase.MyColor(255.0, 255.0, 255.0) - this.ShapeLogo.Fill, 150, 0, null, false),
							ModAnimation.AaColor(this.LabText, TextBlock.ForegroundProperty, new ModBase.MyColor(255.0, 255.0, 255.0) - this.LabText.Foreground, 150, 0, null, false)
						}, "MyRadioButton Checked " + Conversions.ToString(this.issuerIterator), false);
						ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, ModSecret._ConsumerField - base.Background, 150, 0, null, false), "MyRadioButton Color " + Conversions.ToString(this.issuerIterator), false);
					}
				}
				else
				{
					ModAnimation.AniStop("MyRadioButton Checked " + Conversions.ToString(this.issuerIterator));
					ModAnimation.AniStop("MyRadioButton Color " + Conversions.ToString(this.issuerIterator));
					MyRadioButton.ColorState colorType2 = this.ColorType;
					if (colorType2 != MyRadioButton.ColorState.White)
					{
						if (colorType2 == MyRadioButton.ColorState.Highlight)
						{
							if (this.Checked)
							{
								base.SetResourceReference(Border.BackgroundProperty, "ColorBrush3");
								this.ShapeLogo.Fill = new ModBase.MyColor(255.0, 255.0, 255.0);
								this.LabText.Foreground = new ModBase.MyColor(255.0, 255.0, 255.0);
							}
							else
							{
								base.Background = ModSecret._ConsumerField;
								this.ShapeLogo.SetResourceReference(Shape.FillProperty, "ColorBrush3");
								this.LabText.SetResourceReference(TextBlock.ForegroundProperty, "ColorBrush3");
							}
						}
					}
					else if (this.Checked)
					{
						base.Background = new ModBase.MyColor(255.0, 255.0, 255.0);
						this.ShapeLogo.SetResourceReference(Shape.FillProperty, "ColorBrush3");
						this.LabText.SetResourceReference(TextBlock.ForegroundProperty, "ColorBrush3");
					}
					else
					{
						base.Background = ModSecret._ConsumerField;
						this.ShapeLogo.Fill = new ModBase.MyColor(255.0, 255.0, 255.0);
						this.LabText.Foreground = new ModBase.MyColor(255.0, 255.0, 255.0);
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "刷新单选按钮颜色出错", ModBase.LogLevel.Debug, "出现错误");
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x0000C97D File Offset: 0x0000AB7D
		// (set) Token: 0x0600166F RID: 5743 RVA: 0x0000C985 File Offset: 0x0000AB85
		internal virtual MyRadioButton PanBack { get; set; }

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x0000C98E File Offset: 0x0000AB8E
		// (set) Token: 0x06001671 RID: 5745 RVA: 0x0000C996 File Offset: 0x0000AB96
		internal virtual Path ShapeLogo { get; set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x0000C99F File Offset: 0x0000AB9F
		// (set) Token: 0x06001673 RID: 5747 RVA: 0x0000C9A7 File Offset: 0x0000ABA7
		internal virtual TextBlock LabText { get; set; }

		// Token: 0x06001674 RID: 5748 RVA: 0x0009373C File Offset: 0x0009193C
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._RefIterator)
			{
				this._RefIterator = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/myradiobutton.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (MyRadioButton)target;
				return;
			}
			if (connectionId == 2)
			{
				this.ShapeLogo = (Path)target;
				return;
			}
			if (connectionId == 3)
			{
				this.LabText = (TextBlock)target;
				return;
			}
			this._RefIterator = true;
		}

		// Token: 0x04000B55 RID: 2901
		public int issuerIterator;

		// Token: 0x04000B56 RID: 2902
		[CompilerGenerated]
		private MyRadioButton.CheckEventHandler _IndexerIterator;

		// Token: 0x04000B57 RID: 2903
		[CompilerGenerated]
		private MyRadioButton.ChangeEventHandler _InterpreterIterator;

		// Token: 0x04000B58 RID: 2904
		private double serializerIterator;

		// Token: 0x04000B59 RID: 2905
		private bool watcherIterator;

		// Token: 0x04000B5A RID: 2906
		public static readonly DependencyProperty _IdentifierIterator = DependencyProperty.Register("Text", typeof(string), typeof(MyRadioButton), new PropertyMetadata(delegate(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			if (sender != null)
			{
				((MyRadioButton)sender).LabText.Text = Conversions.ToString(e.NewValue);
			}
		}));

		// Token: 0x04000B5B RID: 2907
		private MyRadioButton.ColorState m_SystemIterator;

		// Token: 0x04000B5C RID: 2908
		[CompilerGenerated]
		private MyRadioButton.PreviewClickEventHandler m_ParamIterator;

		// Token: 0x04000B5D RID: 2909
		private bool m_TagIterator;

		// Token: 0x04000B5E RID: 2910
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyRadioButton _ObserverIterator;

		// Token: 0x04000B5F RID: 2911
		[AccessedThroughProperty("ShapeLogo")]
		[CompilerGenerated]
		private Path stubIterator;

		// Token: 0x04000B60 RID: 2912
		[CompilerGenerated]
		[AccessedThroughProperty("LabText")]
		private TextBlock _RulesIterator;

		// Token: 0x04000B61 RID: 2913
		private bool _RefIterator;

		// Token: 0x020001E1 RID: 481
		// (Invoke) Token: 0x0600167C RID: 5756
		public delegate void CheckEventHandler(object sender, bool raiseByMouse);

		// Token: 0x020001E2 RID: 482
		// (Invoke) Token: 0x06001681 RID: 5761
		public delegate void ChangeEventHandler(object sender, bool raiseByMouse);

		// Token: 0x020001E3 RID: 483
		public enum ColorState
		{
			// Token: 0x04000B63 RID: 2915
			White,
			// Token: 0x04000B64 RID: 2916
			Highlight
		}

		// Token: 0x020001E4 RID: 484
		// (Invoke) Token: 0x06001686 RID: 5766
		public delegate void PreviewClickEventHandler(object sender, ModBase.RouteEventArgs e);
	}
}
