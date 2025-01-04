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
using System.Windows.Shapes;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020001D2 RID: 466
	[DesignerGenerated]
	public class MyRadioBox : Grid, IMyRadio, IComponentConnector
	{
		// Token: 0x060015CF RID: 5583 RVA: 0x0009058C File Offset: 0x0008E78C
		public MyRadioBox()
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
			base.IsEnabledChanged += delegate(object sender, DependencyPropertyChangedEventArgs e)
			{
				this.Radiobox_IsEnabledChanged();
			};
			base.MouseEnter += delegate(object sender, MouseEventArgs e)
			{
				this.Radiobox_MouseEnterAnimation();
			};
			base.MouseLeave += delegate(object sender, MouseEventArgs e)
			{
				this.Radiobox_MouseLeaveAnimation();
			};
			this.m_ProductComposer = ModBase.GetUuid();
			this.m_ObjectComposer = false;
			this._ItemComposer = false;
			this.reponseComposer = true;
			this.InitializeComponent();
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x00090634 File Offset: 0x0008E834
		[CompilerGenerated]
		public void UpdateConfig(MyRadioBox.PreviewCheckEventHandler obj)
		{
			MyRadioBox.PreviewCheckEventHandler previewCheckEventHandler = this.m_ListenerComposer;
			MyRadioBox.PreviewCheckEventHandler previewCheckEventHandler2;
			do
			{
				previewCheckEventHandler2 = previewCheckEventHandler;
				MyRadioBox.PreviewCheckEventHandler value = (MyRadioBox.PreviewCheckEventHandler)Delegate.Combine(previewCheckEventHandler2, obj);
				previewCheckEventHandler = Interlocked.CompareExchange<MyRadioBox.PreviewCheckEventHandler>(ref this.m_ListenerComposer, value, previewCheckEventHandler2);
			}
			while (previewCheckEventHandler != previewCheckEventHandler2);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x0009066C File Offset: 0x0008E86C
		[CompilerGenerated]
		public void RateConfig(MyRadioBox.PreviewCheckEventHandler obj)
		{
			MyRadioBox.PreviewCheckEventHandler previewCheckEventHandler = this.m_ListenerComposer;
			MyRadioBox.PreviewCheckEventHandler previewCheckEventHandler2;
			do
			{
				previewCheckEventHandler2 = previewCheckEventHandler;
				MyRadioBox.PreviewCheckEventHandler value = (MyRadioBox.PreviewCheckEventHandler)Delegate.Remove(previewCheckEventHandler2, obj);
				previewCheckEventHandler = Interlocked.CompareExchange<MyRadioBox.PreviewCheckEventHandler>(ref this.m_ListenerComposer, value, previewCheckEventHandler2);
			}
			while (previewCheckEventHandler != previewCheckEventHandler2);
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x000906A4 File Offset: 0x0008E8A4
		[CompilerGenerated]
		public void VerifyConfig(MyRadioBox.PreviewChangeEventHandler obj)
		{
			MyRadioBox.PreviewChangeEventHandler previewChangeEventHandler = this.collectionComposer;
			MyRadioBox.PreviewChangeEventHandler previewChangeEventHandler2;
			do
			{
				previewChangeEventHandler2 = previewChangeEventHandler;
				MyRadioBox.PreviewChangeEventHandler value = (MyRadioBox.PreviewChangeEventHandler)Delegate.Combine(previewChangeEventHandler2, obj);
				previewChangeEventHandler = Interlocked.CompareExchange<MyRadioBox.PreviewChangeEventHandler>(ref this.collectionComposer, value, previewChangeEventHandler2);
			}
			while (previewChangeEventHandler != previewChangeEventHandler2);
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x000906DC File Offset: 0x0008E8DC
		[CompilerGenerated]
		public void ChangeConfig(MyRadioBox.PreviewChangeEventHandler obj)
		{
			MyRadioBox.PreviewChangeEventHandler previewChangeEventHandler = this.collectionComposer;
			MyRadioBox.PreviewChangeEventHandler previewChangeEventHandler2;
			do
			{
				previewChangeEventHandler2 = previewChangeEventHandler;
				MyRadioBox.PreviewChangeEventHandler value = (MyRadioBox.PreviewChangeEventHandler)Delegate.Remove(previewChangeEventHandler2, obj);
				previewChangeEventHandler = Interlocked.CompareExchange<MyRadioBox.PreviewChangeEventHandler>(ref this.collectionComposer, value, previewChangeEventHandler2);
			}
			while (previewChangeEventHandler != previewChangeEventHandler2);
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060015D4 RID: 5588 RVA: 0x00090714 File Offset: 0x0008E914
		// (remove) Token: 0x060015D5 RID: 5589 RVA: 0x0009074C File Offset: 0x0008E94C
		public event IMyRadio.CheckEventHandler Check
		{
			[CompilerGenerated]
			add
			{
				IMyRadio.CheckEventHandler checkEventHandler = this._VisitorComposer;
				IMyRadio.CheckEventHandler checkEventHandler2;
				do
				{
					checkEventHandler2 = checkEventHandler;
					IMyRadio.CheckEventHandler value2 = (IMyRadio.CheckEventHandler)Delegate.Combine(checkEventHandler2, value);
					checkEventHandler = Interlocked.CompareExchange<IMyRadio.CheckEventHandler>(ref this._VisitorComposer, value2, checkEventHandler2);
				}
				while (checkEventHandler != checkEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				IMyRadio.CheckEventHandler checkEventHandler = this._VisitorComposer;
				IMyRadio.CheckEventHandler checkEventHandler2;
				do
				{
					checkEventHandler2 = checkEventHandler;
					IMyRadio.CheckEventHandler value2 = (IMyRadio.CheckEventHandler)Delegate.Remove(checkEventHandler2, value);
					checkEventHandler = Interlocked.CompareExchange<IMyRadio.CheckEventHandler>(ref this._VisitorComposer, value2, checkEventHandler2);
				}
				while (checkEventHandler != checkEventHandler2);
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060015D6 RID: 5590 RVA: 0x00090784 File Offset: 0x0008E984
		// (remove) Token: 0x060015D7 RID: 5591 RVA: 0x000907BC File Offset: 0x0008E9BC
		public event IMyRadio.ChangedEventHandler Changed
		{
			[CompilerGenerated]
			add
			{
				IMyRadio.ChangedEventHandler changedEventHandler = this._ValueComposer;
				IMyRadio.ChangedEventHandler changedEventHandler2;
				do
				{
					changedEventHandler2 = changedEventHandler;
					IMyRadio.ChangedEventHandler value2 = (IMyRadio.ChangedEventHandler)Delegate.Combine(changedEventHandler2, value);
					changedEventHandler = Interlocked.CompareExchange<IMyRadio.ChangedEventHandler>(ref this._ValueComposer, value2, changedEventHandler2);
				}
				while (changedEventHandler != changedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				IMyRadio.ChangedEventHandler changedEventHandler = this._ValueComposer;
				IMyRadio.ChangedEventHandler changedEventHandler2;
				do
				{
					changedEventHandler2 = changedEventHandler;
					IMyRadio.ChangedEventHandler value2 = (IMyRadio.ChangedEventHandler)Delegate.Remove(changedEventHandler2, value);
					changedEventHandler = Interlocked.CompareExchange<IMyRadio.ChangedEventHandler>(ref this._ValueComposer, value2, changedEventHandler2);
				}
				while (changedEventHandler != changedEventHandler2);
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x0000C44E File Offset: 0x0000A64E
		// (set) Token: 0x060015D9 RID: 5593 RVA: 0x0000C456 File Offset: 0x0000A656
		public bool Checked
		{
			get
			{
				return this.m_ObjectComposer;
			}
			set
			{
				this.SetChecked(value, false, true);
			}
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x000907F4 File Offset: 0x0008E9F4
		public void SetChecked(bool value, bool user, bool anime)
		{
			try
			{
				if (value && user)
				{
					ModBase.RouteEventArgs routeEventArgs = new ModBase.RouteEventArgs(user);
					MyRadioBox.PreviewCheckEventHandler listenerComposer = this.m_ListenerComposer;
					if (listenerComposer != null)
					{
						listenerComposer(this, routeEventArgs);
					}
					if (routeEventArgs.m_SerializerError)
					{
						this.Radiobox_MouseLeave();
						return;
					}
				}
				bool flag = false;
				if (base.IsLoaded && value != this.m_ObjectComposer)
				{
					MyRadioBox.PreviewChangeEventHandler previewChangeEventHandler = this.collectionComposer;
					if (previewChangeEventHandler != null)
					{
						previewChangeEventHandler(this, new ModBase.RouteEventArgs(user));
					}
				}
				if (value != this.m_ObjectComposer)
				{
					this.m_ObjectComposer = value;
					flag = true;
				}
				if (base.Parent != null)
				{
					List<MyRadioBox> list = new List<MyRadioBox>();
					int num = 0;
					checked
					{
						try
						{
							foreach (object obj in ((IEnumerable)NewLateBinding.LateGet(base.Parent, null, "Children", new object[0], null, null, null)))
							{
								object objectValue = RuntimeHelpers.GetObjectValue(obj);
								if (objectValue is MyRadioBox)
								{
									list.Add((MyRadioBox)objectValue);
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
									foreach (MyRadioBox myRadioBox in list)
									{
										if (myRadioBox.Checked && !myRadioBox.Equals(this))
										{
											myRadioBox.Checked = false;
										}
									}
									goto IL_1D0;
								}
								finally
								{
									List<MyRadioBox>.Enumerator enumerator2;
									((IDisposable)enumerator2).Dispose();
								}
							}
							bool flag2 = false;
							try
							{
								foreach (MyRadioBox myRadioBox2 in list)
								{
									if (myRadioBox2.Checked)
									{
										if (flag2)
										{
											myRadioBox2.Checked = false;
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
								List<MyRadioBox>.Enumerator enumerator3;
								((IDisposable)enumerator3).Dispose();
							}
						}
						IL_1D0:;
					}
					if (flag)
					{
						if (base.IsLoaded && ModAnimation.CalcParser() == 0 && anime)
						{
							if (this.Checked)
							{
								if (this.ShapeDot.Opacity < 0.01)
								{
									this.ShapeDot.Opacity = 1.0;
								}
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaScale(this.ShapeBorder, 10.0 - this.ShapeBorder.Width, 150, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false, true),
									ModAnimation.AaScale(this.ShapeBorder, 8.0, 300, 90, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Middle), false, true)
								}, "MyRadioBox Border " + Conversions.ToString(this.m_ProductComposer), false);
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaScale(this.ShapeDot, 9.0 - this.ShapeDot.Width, 390, 0, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false, true),
									ModAnimation.AaOpacity(this.ShapeDot, 1.0 - this.ShapeDot.Opacity, 75, 90, null, false)
								}, "MyRadioBox Dot " + Conversions.ToString(this.m_ProductComposer), false);
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaColor(this.ShapeBorder, Shape.StrokeProperty, base.IsMouseOver ? "ColorBrush3" : (base.IsEnabled ? "ColorBrush2" : "ColorBrushGray4"), 150, 0, null, false)
								}, "MyRadioBox BorderColor " + Conversions.ToString(this.m_ProductComposer), false);
							}
							else
							{
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaScale(this.ShapeBorder, 18.0 - this.ShapeBorder.Width, 150, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false, true)
								}, "MyRadioBox Border " + Conversions.ToString(this.m_ProductComposer), false);
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaScale(this.ShapeDot, -this.ShapeDot.Width, 150, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false, true),
									ModAnimation.AaOpacity(this.ShapeDot, -this.ShapeDot.Opacity, 75, 30, null, false)
								}, "MyRadioBox Dot " + Conversions.ToString(this.m_ProductComposer), false);
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaColor(this.ShapeBorder, Shape.StrokeProperty, base.IsMouseOver ? "ColorBrush3" : (base.IsEnabled ? "ColorBrush1" : "ColorBrushGray4"), 150, 0, null, false)
								}, "MyRadioBox BorderColor " + Conversions.ToString(this.m_ProductComposer), false);
							}
						}
						else
						{
							ModAnimation.AniStop("MyRadioBox Border " + Conversions.ToString(this.m_ProductComposer));
							ModAnimation.AniStop("MyRadioBox Dot " + Conversions.ToString(this.m_ProductComposer));
							ModAnimation.AniStop("MyRadioBox BorderColor " + Conversions.ToString(this.m_ProductComposer));
							if (this.Checked)
							{
								this.ShapeDot.Width = 9.0;
								this.ShapeDot.Height = 9.0;
								this.ShapeDot.Opacity = 1.0;
								this.ShapeDot.Margin = new Thickness(5.5, 0.0, 0.0, 0.0);
								this.ShapeBorder.SetResourceReference(Shape.StrokeProperty, base.IsEnabled ? "ColorBrush2" : "ColorBrushGray4");
							}
							else
							{
								this.ShapeDot.Width = 0.0;
								this.ShapeDot.Height = 0.0;
								this.ShapeDot.Opacity = 0.0;
								this.ShapeDot.Margin = new Thickness(10.0, 0.0, 0.0, 0.0);
								this.ShapeBorder.SetResourceReference(Shape.StrokeProperty, base.IsEnabled ? "ColorBrush1" : "ColorBrushGray4");
							}
						}
						if (this.Checked)
						{
							IMyRadio.CheckEventHandler visitorComposer = this._VisitorComposer;
							if (visitorComposer != null)
							{
								visitorComposer(this, new ModBase.RouteEventArgs(user));
							}
						}
						IMyRadio.ChangedEventHandler valueComposer = this._ValueComposer;
						if (valueComposer != null)
						{
							valueComposer(this, new ModBase.RouteEventArgs(user));
						}
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "单选框勾选改变错误", ModBase.LogLevel.Hint, "出现错误");
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x0000C461 File Offset: 0x0000A661
		// (set) Token: 0x060015DC RID: 5596 RVA: 0x0000C473 File Offset: 0x0000A673
		public string Text
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyRadioBox.bridgeComposer));
			}
			set
			{
				base.SetValue(MyRadioBox.bridgeComposer, value);
			}
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00090F0C File Offset: 0x0008F10C
		private void Radiobox_MouseUp()
		{
			if (this._ItemComposer)
			{
				this._ItemComposer = false;
				ModBase.Log("[Control] 按下单选框：" + this.Text, ModBase.LogLevel.Normal, "出现错误");
				this.SetChecked(true, true, true);
				ModAnimation.AniStart(ModAnimation.AaColor(this.ShapeBorder, Shape.FillProperty, "ColorBrushHalfWhite", 100, 0, null, false), "MyRadioBox Background " + Conversions.ToString(this.m_ProductComposer), false);
			}
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00090F84 File Offset: 0x0008F184
		private void Radiobox_MouseDown()
		{
			this._ItemComposer = true;
			base.Focus();
			ModAnimation.AniStart(ModAnimation.AaColor(this.ShapeBorder, Shape.FillProperty, "ColorBrushBg1", 100, 0, null, false), "MyRadioBox Background " + Conversions.ToString(this.m_ProductComposer), false);
			if (!this.Checked)
			{
				ModAnimation.AniStart(ModAnimation.AaScale(this.ShapeBorder, 16.5 - this.ShapeBorder.Width, 1000, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false, true), "MyRadioBox Border " + Conversions.ToString(this.m_ProductComposer), false);
			}
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00091028 File Offset: 0x0008F228
		private void Radiobox_MouseLeave()
		{
			if (this._ItemComposer)
			{
				this._ItemComposer = false;
				ModAnimation.AniStart(ModAnimation.AaColor(this.ShapeBorder, Shape.FillProperty, "ColorBrushHalfWhite", 100, 0, null, false), "MyRadioBox Background " + Conversions.ToString(this.m_ProductComposer), false);
				if (!this.Checked)
				{
					ModAnimation.AniStart(ModAnimation.AaScale(this.ShapeBorder, 18.0 - this.ShapeBorder.Width, 400, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false, true), "MyRadioBox Border " + Conversions.ToString(this.m_ProductComposer), false);
				}
			}
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x000910D0 File Offset: 0x0008F2D0
		private void Radiobox_IsEnabledChanged()
		{
			if (!base.IsLoaded || ModAnimation.CalcParser() != 0)
			{
				ModAnimation.AniStop("MyRadioBox BorderColor " + Conversions.ToString(this.m_ProductComposer));
				ModAnimation.AniStop("MyRadioBox TextColor " + Conversions.ToString(this.m_ProductComposer));
				this.LabText.SetResourceReference(TextBlock.ForegroundProperty, base.IsEnabled ? "ColorBrush1" : "ColorBrushGray4");
				this.ShapeBorder.SetResourceReference(Shape.StrokeProperty, base.IsEnabled ? (this.Checked ? "ColorBrush2" : "ColorBrush1") : "ColorBrushGray4");
				return;
			}
			if (base.IsEnabled)
			{
				this.Radiobox_MouseLeaveAnimation();
				return;
			}
			ModAnimation.AniStart(ModAnimation.AaColor(this.ShapeBorder, Shape.StrokeProperty, ModSecret.m_StateField - this.ShapeBorder.Stroke, 200, 0, null, false), "MyRadioBox BorderColor " + Conversions.ToString(this.m_ProductComposer), false);
			ModAnimation.AniStart(ModAnimation.AaColor(this.LabText, TextBlock.ForegroundProperty, ModSecret.m_StateField - this.LabText.Foreground, 200, 0, null, false), "MyRadioBox TextColor " + Conversions.ToString(this.m_ProductComposer), false);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00091228 File Offset: 0x0008F428
		private void Radiobox_MouseEnterAnimation()
		{
			ModAnimation.AniStart(ModAnimation.AaColor(this.ShapeBorder, Shape.StrokeProperty, "ColorBrush3", 100, 0, null, false), "MyRadioBox BorderColor " + Conversions.ToString(this.m_ProductComposer), false);
			ModAnimation.AniStart(ModAnimation.AaColor(this.LabText, TextBlock.ForegroundProperty, "ColorBrush3", 100, 0, null, false), "MyRadioBox TextColor " + Conversions.ToString(this.m_ProductComposer), false);
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x000912A0 File Offset: 0x0008F4A0
		private void Radiobox_MouseLeaveAnimation()
		{
			if (base.IsEnabled)
			{
				if (base.IsLoaded && ModAnimation.CalcParser() == 0)
				{
					ModAnimation.AniStart(ModAnimation.AaColor(this.ShapeBorder, Shape.StrokeProperty, base.IsEnabled ? (this.Checked ? "ColorBrush2" : "ColorBrush1") : "ColorBrushGray4", 200, 0, null, false), "MyRadioBox BorderColor " + Conversions.ToString(this.m_ProductComposer), false);
					ModAnimation.AniStart(ModAnimation.AaColor(this.LabText, TextBlock.ForegroundProperty, base.IsEnabled ? "ColorBrush1" : "ColorBrushGray4", 200, 0, null, false), "MyRadioBox TextColor " + Conversions.ToString(this.m_ProductComposer), false);
					return;
				}
				ModAnimation.AniStop("MyRadioBox BorderColor " + Conversions.ToString(this.m_ProductComposer));
				ModAnimation.AniStop("MyRadioBox TextColor " + Conversions.ToString(this.m_ProductComposer));
				this.ShapeBorder.SetResourceReference(Shape.StrokeProperty, base.IsEnabled ? (this.Checked ? "ColorBrush2" : "ColorBrush1") : "ColorBrushGray4");
				this.LabText.SetResourceReference(TextBlock.ForegroundProperty, base.IsEnabled ? "ColorBrush1" : "ColorBrushGray4");
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x0000C481 File Offset: 0x0000A681
		// (set) Token: 0x060015E4 RID: 5604 RVA: 0x0000C489 File Offset: 0x0000A689
		internal virtual MyRadioBox PanBack { get; set; }

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x0000C492 File Offset: 0x0000A692
		// (set) Token: 0x060015E6 RID: 5606 RVA: 0x0000C49A File Offset: 0x0000A69A
		internal virtual TextBlock LabText { get; set; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x0000C4A3 File Offset: 0x0000A6A3
		// (set) Token: 0x060015E8 RID: 5608 RVA: 0x0000C4AB File Offset: 0x0000A6AB
		internal virtual Ellipse ShapeBorder { get; set; }

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x0000C4B4 File Offset: 0x0000A6B4
		// (set) Token: 0x060015EA RID: 5610 RVA: 0x0000C4BC File Offset: 0x0000A6BC
		internal virtual Ellipse ShapeDot { get; set; }

		// Token: 0x060015EB RID: 5611 RVA: 0x000913F8 File Offset: 0x0008F5F8
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._PolicyComposer)
			{
				this._PolicyComposer = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/myradiobox.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00091428 File Offset: 0x0008F628
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (MyRadioBox)target;
				return;
			}
			if (connectionId == 2)
			{
				this.LabText = (TextBlock)target;
				return;
			}
			if (connectionId == 3)
			{
				this.ShapeBorder = (Ellipse)target;
				return;
			}
			if (connectionId == 4)
			{
				this.ShapeDot = (Ellipse)target;
				return;
			}
			this._PolicyComposer = true;
		}

		// Token: 0x04000B16 RID: 2838
		public int m_ProductComposer;

		// Token: 0x04000B17 RID: 2839
		[CompilerGenerated]
		private MyRadioBox.PreviewCheckEventHandler m_ListenerComposer;

		// Token: 0x04000B18 RID: 2840
		[CompilerGenerated]
		private MyRadioBox.PreviewChangeEventHandler collectionComposer;

		// Token: 0x04000B19 RID: 2841
		[CompilerGenerated]
		private IMyRadio.CheckEventHandler _VisitorComposer;

		// Token: 0x04000B1A RID: 2842
		[CompilerGenerated]
		private IMyRadio.ChangedEventHandler _ValueComposer;

		// Token: 0x04000B1B RID: 2843
		private bool m_ObjectComposer;

		// Token: 0x04000B1C RID: 2844
		public static readonly DependencyProperty bridgeComposer = DependencyProperty.Register("Text", typeof(string), typeof(MyRadioBox), new PropertyMetadata(delegate(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			if (sender != null)
			{
				((MyRadioBox)sender).LabText.Text = Conversions.ToString(e.NewValue);
			}
		}));

		// Token: 0x04000B1D RID: 2845
		private bool _ItemComposer;

		// Token: 0x04000B1E RID: 2846
		private bool reponseComposer;

		// Token: 0x04000B1F RID: 2847
		[CompilerGenerated]
		[AccessedThroughProperty("PanBack")]
		private MyRadioBox _GlobalComposer;

		// Token: 0x04000B20 RID: 2848
		[CompilerGenerated]
		[AccessedThroughProperty("LabText")]
		private TextBlock _ExceptionComposer;

		// Token: 0x04000B21 RID: 2849
		[CompilerGenerated]
		[AccessedThroughProperty("ShapeBorder")]
		private Ellipse m_UtilsComposer;

		// Token: 0x04000B22 RID: 2850
		[AccessedThroughProperty("ShapeDot")]
		[CompilerGenerated]
		private Ellipse classComposer;

		// Token: 0x04000B23 RID: 2851
		private bool _PolicyComposer;

		// Token: 0x020001D3 RID: 467
		// (Invoke) Token: 0x060015F6 RID: 5622
		public delegate void PreviewCheckEventHandler(object sender, ModBase.RouteEventArgs e);

		// Token: 0x020001D4 RID: 468
		// (Invoke) Token: 0x060015FB RID: 5627
		public delegate void PreviewChangeEventHandler(object sender, ModBase.RouteEventArgs e);
	}
}
