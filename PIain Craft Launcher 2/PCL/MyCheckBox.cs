using System;
using System.CodeDom.Compiler;
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
	// Token: 0x020001C9 RID: 457
	[DesignerGenerated]
	public class MyCheckBox : Grid, IComponentConnector
	{
		// Token: 0x06001549 RID: 5449 RVA: 0x0008E368 File Offset: 0x0008C568
		public MyCheckBox()
		{
			base.MouseLeftButtonUp += delegate(object sender, MouseButtonEventArgs e)
			{
				this.Checkbox_MouseUp();
			};
			base.MouseLeftButtonDown += delegate(object sender, MouseButtonEventArgs e)
			{
				this.Checkbox_MouseDown();
			};
			base.MouseLeave += delegate(object sender, MouseEventArgs e)
			{
				this.Checkbox_MouseLeave();
			};
			base.IsEnabledChanged += delegate(object sender, DependencyPropertyChangedEventArgs e)
			{
				this.Checkbox_IsEnabledChanged();
			};
			base.MouseEnter += delegate(object sender, MouseEventArgs e)
			{
				this.Checkbox_MouseEnterAnimation();
			};
			base.MouseLeave += delegate(object sender, MouseEventArgs e)
			{
				this.Checkbox_MouseLeaveAnimation();
			};
			this._ManagerComposer = ModBase.GetUuid();
			this.m_BaseComposer = false;
			this.codeComposer = false;
			this._PrototypeComposer = true;
			this.InitializeComponent();
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0008E410 File Offset: 0x0008C610
		[CompilerGenerated]
		public void InstantiateConfig(MyCheckBox.ChangeEventHandler obj)
		{
			MyCheckBox.ChangeEventHandler changeEventHandler = this.m_ModelComposer;
			MyCheckBox.ChangeEventHandler changeEventHandler2;
			do
			{
				changeEventHandler2 = changeEventHandler;
				MyCheckBox.ChangeEventHandler value = (MyCheckBox.ChangeEventHandler)Delegate.Combine(changeEventHandler2, obj);
				changeEventHandler = Interlocked.CompareExchange<MyCheckBox.ChangeEventHandler>(ref this.m_ModelComposer, value, changeEventHandler2);
			}
			while (changeEventHandler != changeEventHandler2);
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0008E448 File Offset: 0x0008C648
		[CompilerGenerated]
		public void PublishConfig(MyCheckBox.ChangeEventHandler obj)
		{
			MyCheckBox.ChangeEventHandler changeEventHandler = this.m_ModelComposer;
			MyCheckBox.ChangeEventHandler changeEventHandler2;
			do
			{
				changeEventHandler2 = changeEventHandler;
				MyCheckBox.ChangeEventHandler value = (MyCheckBox.ChangeEventHandler)Delegate.Remove(changeEventHandler2, obj);
				changeEventHandler = Interlocked.CompareExchange<MyCheckBox.ChangeEventHandler>(ref this.m_ModelComposer, value, changeEventHandler2);
			}
			while (changeEventHandler != changeEventHandler2);
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0008E480 File Offset: 0x0008C680
		[CompilerGenerated]
		public void PatchConfig(MyCheckBox.PreviewChangeEventHandler obj)
		{
			MyCheckBox.PreviewChangeEventHandler previewChangeEventHandler = this._WrapperComposer;
			MyCheckBox.PreviewChangeEventHandler previewChangeEventHandler2;
			do
			{
				previewChangeEventHandler2 = previewChangeEventHandler;
				MyCheckBox.PreviewChangeEventHandler value = (MyCheckBox.PreviewChangeEventHandler)Delegate.Combine(previewChangeEventHandler2, obj);
				previewChangeEventHandler = Interlocked.CompareExchange<MyCheckBox.PreviewChangeEventHandler>(ref this._WrapperComposer, value, previewChangeEventHandler2);
			}
			while (previewChangeEventHandler != previewChangeEventHandler2);
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0008E4B8 File Offset: 0x0008C6B8
		[CompilerGenerated]
		public void RemoveConfig(MyCheckBox.PreviewChangeEventHandler obj)
		{
			MyCheckBox.PreviewChangeEventHandler previewChangeEventHandler = this._WrapperComposer;
			MyCheckBox.PreviewChangeEventHandler previewChangeEventHandler2;
			do
			{
				previewChangeEventHandler2 = previewChangeEventHandler;
				MyCheckBox.PreviewChangeEventHandler value = (MyCheckBox.PreviewChangeEventHandler)Delegate.Remove(previewChangeEventHandler2, obj);
				previewChangeEventHandler = Interlocked.CompareExchange<MyCheckBox.PreviewChangeEventHandler>(ref this._WrapperComposer, value, previewChangeEventHandler2);
			}
			while (previewChangeEventHandler != previewChangeEventHandler2);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0008E4F0 File Offset: 0x0008C6F0
		public void RaiseChange()
		{
			MyCheckBox.ChangeEventHandler modelComposer = this.m_ModelComposer;
			if (modelComposer != null)
			{
				modelComposer(this, false);
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x0000C0F9 File Offset: 0x0000A2F9
		// (set) Token: 0x06001550 RID: 5456 RVA: 0x0000C101 File Offset: 0x0000A301
		public bool Checked
		{
			get
			{
				return this.m_BaseComposer;
			}
			set
			{
				this.SetChecked(value, false, true);
			}
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0008E510 File Offset: 0x0008C710
		public void SetChecked(bool value, bool user, bool anime)
		{
			try
			{
				if (value != this.m_BaseComposer)
				{
					if (value && user)
					{
						ModBase.RouteEventArgs routeEventArgs = new ModBase.RouteEventArgs(user);
						MyCheckBox.PreviewChangeEventHandler wrapperComposer = this._WrapperComposer;
						if (wrapperComposer != null)
						{
							wrapperComposer(this, routeEventArgs);
						}
						if (routeEventArgs.m_SerializerError)
						{
							this.codeComposer = true;
							this.Checkbox_MouseLeave();
							this.codeComposer = false;
							return;
						}
					}
					this.m_BaseComposer = value;
					if (base.IsLoaded)
					{
						MyCheckBox.ChangeEventHandler modelComposer = this.m_ModelComposer;
						if (modelComposer != null)
						{
							modelComposer(this, user);
						}
					}
					if (base.IsLoaded && ModAnimation.CalcParser() == 0 && anime)
					{
						this._PrototypeComposer = false;
						if (this.Checked)
						{
							ModAnimation.AniStart(new ModAnimation.AniData[]
							{
								ModAnimation.AaScale(this.ShapeBorder, 12.0 - this.ShapeBorder.Width, 150, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false, true),
								ModAnimation.AaScaleTransform(this.ShapeCheck, 1.0 - ((ScaleTransform)this.ShapeCheck.RenderTransform).ScaleX, 300, 105, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false),
								ModAnimation.AaScale(this.ShapeBorder, 6.0, 300, 105, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Middle), false, true)
							}, "MyCheckBox Scale " + Conversions.ToString(this._ManagerComposer), false);
							ModAnimation.AniStart(new ModAnimation.AniData[]
							{
								ModAnimation.AaColor(this.ShapeBorder, Border.BorderBrushProperty, base.IsEnabled ? (base.IsMouseOver ? "ColorBrush3" : "ColorBrush2") : "ColorBrushGray4", 150, 0, null, false)
							}, "MyCheckBox BorderColor " + Conversions.ToString(this._ManagerComposer), false);
							ModAnimation.AniStart(new ModAnimation.AniData[]
							{
								ModAnimation.AaCode(delegate
								{
									this._PrototypeComposer = true;
								}, 300, false)
							}, "MyCheckBox AllowMouseDown " + Conversions.ToString(this._ManagerComposer), false);
						}
						else
						{
							ModAnimation.AniStart(new ModAnimation.AniData[]
							{
								ModAnimation.AaScale(this.ShapeBorder, 12.0 - this.ShapeBorder.Width, 150, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false, true),
								ModAnimation.AaScaleTransform(this.ShapeCheck, -((ScaleTransform)this.ShapeCheck.RenderTransform).ScaleX, 135, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak), false),
								ModAnimation.AaScale(this.ShapeBorder, 6.0, 300, 105, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Middle), false, true)
							}, "MyCheckBox Scale " + Conversions.ToString(this._ManagerComposer), false);
							ModAnimation.AniStart(new ModAnimation.AniData[]
							{
								ModAnimation.AaColor(this.ShapeBorder, Border.BorderBrushProperty, base.IsEnabled ? (base.IsMouseOver ? "ColorBrush3" : "ColorBrush1") : "ColorBrushGray4", 150, 0, null, false)
							}, "MyCheckBox BorderColor " + Conversions.ToString(this._ManagerComposer), false);
							ModAnimation.AniStart(new ModAnimation.AniData[]
							{
								ModAnimation.AaCode(delegate
								{
									this._PrototypeComposer = true;
								}, 300, false)
							}, "MyCheckBox AllowMouseDown " + Conversions.ToString(this._ManagerComposer), false);
						}
					}
					else
					{
						ModAnimation.AniStop("MyCheckBox Scale " + Conversions.ToString(this._ManagerComposer));
						ModAnimation.AniStop("MyCheckBox BorderColor " + Conversions.ToString(this._ManagerComposer));
						ModAnimation.AniStop("MyCheckBox AllowMouseDown " + Conversions.ToString(this._ManagerComposer));
						if (this.Checked)
						{
							((ScaleTransform)this.ShapeCheck.RenderTransform).ScaleX = 1.0;
							((ScaleTransform)this.ShapeCheck.RenderTransform).ScaleY = 1.0;
							this.ShapeBorder.SetResourceReference(Border.BorderBrushProperty, base.IsEnabled ? "ColorBrush2" : "ColorBrushGray4");
						}
						else
						{
							((ScaleTransform)this.ShapeCheck.RenderTransform).ScaleX = 0.0;
							((ScaleTransform)this.ShapeCheck.RenderTransform).ScaleY = 0.0;
							this.ShapeBorder.SetResourceReference(Border.BorderBrushProperty, base.IsEnabled ? "ColorBrush1" : "ColorBrushGray4");
						}
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "设置 Checked 失败", ModBase.LogLevel.Debug, "出现错误");
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x0000C10C File Offset: 0x0000A30C
		// (set) Token: 0x06001553 RID: 5459 RVA: 0x0000C11E File Offset: 0x0000A31E
		public string Text
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyCheckBox.m_AttributeComposer));
			}
			set
			{
				base.SetValue(MyCheckBox.m_AttributeComposer, value);
			}
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0008E9D0 File Offset: 0x0008CBD0
		private void Checkbox_MouseUp()
		{
			if (this.codeComposer)
			{
				ModBase.Log("[Control] 按下复选框（" + (!this.Checked).ToString() + "）：" + this.Text, ModBase.LogLevel.Normal, "出现错误");
				this.codeComposer = false;
				this.SetChecked(!this.Checked, true, true);
				ModAnimation.AniStart(ModAnimation.AaColor(this.ShapeBorder, Border.BackgroundProperty, "ColorBrushHalfWhite", 100, 0, null, false), "MyCheckBox Background " + Conversions.ToString(this._ManagerComposer), false);
			}
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0008EA64 File Offset: 0x0008CC64
		private void Checkbox_MouseDown()
		{
			if (this._PrototypeComposer)
			{
				this.codeComposer = true;
				base.Focus();
				ModAnimation.AniStart(ModAnimation.AaColor(this.ShapeBorder, Border.BackgroundProperty, "ColorBrushBg1", 100, 0, null, false), "MyCheckBox Background " + Conversions.ToString(this._ManagerComposer), false);
				if (this.Checked)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaScale(this.ShapeBorder, 16.5 - this.ShapeBorder.Width, 1000, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false, true),
						ModAnimation.AaScaleTransform(this.ShapeCheck, 0.9 - ((ScaleTransform)this.ShapeCheck.RenderTransform).ScaleX, 1000, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false)
					}, "MyCheckBox Scale " + Conversions.ToString(this._ManagerComposer), false);
					return;
				}
				ModAnimation.AniStart(ModAnimation.AaScale(this.ShapeBorder, 16.5 - this.ShapeBorder.Width, 1000, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false, true), "MyCheckBox Scale " + Conversions.ToString(this._ManagerComposer), false);
			}
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0008EBA8 File Offset: 0x0008CDA8
		private void Checkbox_MouseLeave()
		{
			if (this.codeComposer)
			{
				this.codeComposer = false;
				ModAnimation.AniStart(ModAnimation.AaColor(this.ShapeBorder, Border.BackgroundProperty, "ColorBrushHalfWhite", 100, 0, null, false), "MyCheckBox Background " + Conversions.ToString(this._ManagerComposer), false);
				if (this.Checked)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaScale(this.ShapeBorder, 18.0 - this.ShapeBorder.Width, 400, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false, true),
						ModAnimation.AaScaleTransform(this.ShapeCheck, 1.0 - ((ScaleTransform)this.ShapeCheck.RenderTransform).ScaleX, 500, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false)
					}, "MyCheckBox Scale " + Conversions.ToString(this._ManagerComposer), false);
					return;
				}
				ModAnimation.AniStart(ModAnimation.AaScale(this.ShapeBorder, 18.0 - this.ShapeBorder.Width, 400, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false, true), "MyCheckBox Scale " + Conversions.ToString(this._ManagerComposer), false);
			}
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x0008ECE8 File Offset: 0x0008CEE8
		private void Checkbox_IsEnabledChanged()
		{
			if (!base.IsLoaded || ModAnimation.CalcParser() != 0)
			{
				ModAnimation.AniStop("MyCheckBox TextColor " + Conversions.ToString(this._ManagerComposer));
				ModAnimation.AniStop("MyCheckBox BorderColor " + Conversions.ToString(this._ManagerComposer));
				this.LabText.SetResourceReference(TextBlock.ForegroundProperty, base.IsEnabled ? "ColorBrush1" : "ColorBrushGray4");
				this.ShapeBorder.SetResourceReference(Border.BorderBrushProperty, base.IsEnabled ? (this.Checked ? "ColorBrush2" : "ColorBrush1") : "ColorBrushGray4");
				return;
			}
			if (base.IsEnabled)
			{
				this.Checkbox_MouseLeaveAnimation();
				return;
			}
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaColor(this.ShapeBorder, Border.BorderBrushProperty, ModSecret.m_StateField - this.ShapeBorder.BorderBrush, 200, 0, null, false)
			}, "MyCheckBox BorderColor " + Conversions.ToString(this._ManagerComposer), false);
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaColor(this.LabText, TextBlock.ForegroundProperty, ModSecret.m_StateField - this.LabText.Foreground, 200, 0, null, false)
			}, "MyCheckBox TextColor " + Conversions.ToString(this._ManagerComposer), false);
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0008EE5C File Offset: 0x0008D05C
		private void Checkbox_MouseEnterAnimation()
		{
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaColor(this.LabText, TextBlock.ForegroundProperty, "ColorBrush3", 100, 0, null, false)
			}, "MyCheckBox TextColor " + Conversions.ToString(this._ManagerComposer), false);
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaColor(this.ShapeBorder, Border.BorderBrushProperty, "ColorBrush3", 100, 0, null, false)
			}, "MyCheckBox BorderColor " + Conversions.ToString(this._ManagerComposer), false);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0008EEF0 File Offset: 0x0008D0F0
		private void Checkbox_MouseLeaveAnimation()
		{
			if (base.IsEnabled)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaColor(this.LabText, TextBlock.ForegroundProperty, base.IsEnabled ? "ColorBrush1" : "ColorBrushGray4", 200, 0, null, false)
				}, "MyCheckBox TextColor " + Conversions.ToString(this._ManagerComposer), false);
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaColor(this.ShapeBorder, Border.BorderBrushProperty, base.IsEnabled ? (this.Checked ? "ColorBrush2" : "ColorBrush1") : "ColorBrushGray4", 200, 0, null, false)
				}, "MyCheckBox BorderColor " + Conversions.ToString(this._ManagerComposer), false);
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x0000C12C File Offset: 0x0000A32C
		// (set) Token: 0x0600155B RID: 5467 RVA: 0x0000C134 File Offset: 0x0000A334
		internal virtual MyCheckBox PanBack { get; set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x0000C13D File Offset: 0x0000A33D
		// (set) Token: 0x0600155D RID: 5469 RVA: 0x0000C145 File Offset: 0x0000A345
		internal virtual TextBlock LabText { get; set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x0000C14E File Offset: 0x0000A34E
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x0000C156 File Offset: 0x0000A356
		internal virtual Border ShapeBorder { get; set; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0000C15F File Offset: 0x0000A35F
		// (set) Token: 0x06001561 RID: 5473 RVA: 0x0000C167 File Offset: 0x0000A367
		internal virtual Path ShapeCheck { get; set; }

		// Token: 0x06001562 RID: 5474 RVA: 0x0008EFC0 File Offset: 0x0008D1C0
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.listComposer)
			{
				this.listComposer = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/mycheckbox.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0008EFF0 File Offset: 0x0008D1F0
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (MyCheckBox)target;
				return;
			}
			if (connectionId == 2)
			{
				this.LabText = (TextBlock)target;
				return;
			}
			if (connectionId == 3)
			{
				this.ShapeBorder = (Border)target;
				return;
			}
			if (connectionId == 4)
			{
				this.ShapeCheck = (Path)target;
				return;
			}
			this.listComposer = true;
		}

		// Token: 0x04000AE6 RID: 2790
		public int _ManagerComposer;

		// Token: 0x04000AE7 RID: 2791
		[CompilerGenerated]
		private MyCheckBox.ChangeEventHandler m_ModelComposer;

		// Token: 0x04000AE8 RID: 2792
		[CompilerGenerated]
		private MyCheckBox.PreviewChangeEventHandler _WrapperComposer;

		// Token: 0x04000AE9 RID: 2793
		private bool m_BaseComposer;

		// Token: 0x04000AEA RID: 2794
		public static readonly DependencyProperty m_AttributeComposer = DependencyProperty.Register("Text", typeof(string), typeof(MyCheckBox), new PropertyMetadata(delegate(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			if (!Information.IsNothing(sender))
			{
				((MyCheckBox)sender).LabText.Text = Conversions.ToString(e.NewValue);
			}
		}));

		// Token: 0x04000AEB RID: 2795
		private bool codeComposer;

		// Token: 0x04000AEC RID: 2796
		private bool _PrototypeComposer;

		// Token: 0x04000AED RID: 2797
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyCheckBox annotationComposer;

		// Token: 0x04000AEE RID: 2798
		[CompilerGenerated]
		[AccessedThroughProperty("LabText")]
		private TextBlock _InfoComposer;

		// Token: 0x04000AEF RID: 2799
		[CompilerGenerated]
		[AccessedThroughProperty("ShapeBorder")]
		private Border _AdapterComposer;

		// Token: 0x04000AF0 RID: 2800
		[CompilerGenerated]
		[AccessedThroughProperty("ShapeCheck")]
		private Path facadeComposer;

		// Token: 0x04000AF1 RID: 2801
		private bool listComposer;

		// Token: 0x020001CA RID: 458
		// (Invoke) Token: 0x0600156F RID: 5487
		public delegate void ChangeEventHandler(object sender, bool user);

		// Token: 0x020001CB RID: 459
		// (Invoke) Token: 0x06001574 RID: 5492
		public delegate void PreviewChangeEventHandler(object sender, ModBase.RouteEventArgs e);
	}
}
