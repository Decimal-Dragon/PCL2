using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020001D0 RID: 464
	[DesignerGenerated]
	public class MyMsgText : Grid, IComponentConnector
	{
		// Token: 0x0600159E RID: 5534 RVA: 0x0008FBD8 File Offset: 0x0008DDD8
		public MyMsgText(ModMain.MyMsgBoxConverter Converter)
		{
			base.Loaded += new RoutedEventHandler(this.Load);
			this.m_InterceptorComposer = ModBase.GetUuid();
			try
			{
				this.InitializeComponent();
				this.Btn1.Name = this.Btn1.Name + Conversions.ToString(ModBase.GetUuid());
				this.Btn2.Name = this.Btn2.Name + Conversions.ToString(ModBase.GetUuid());
				this.Btn3.Name = this.Btn3.Name + Conversions.ToString(ModBase.GetUuid());
				this.m_PageComposer = Converter;
				this.LabTitle.Text = Converter.Title;
				this.LabCaption.Text = Converter.listenerMap;
				this.Btn1.Text = Converter.m_BridgeMap;
				if (Converter._ClassMap)
				{
					this.Btn1.ColorType = MyButton.ColorState.Red;
					this.LabTitle.SetResourceReference(TextBlock.ForegroundProperty, "ColorBrushRedLight");
				}
				this.Btn2.Text = Converter.itemMap;
				this.Btn3.Text = Converter.reponseMap;
				this.Btn2.Visibility = ((Operators.CompareString(Converter.itemMap, "", false) == 0) ? Visibility.Collapsed : Visibility.Visible);
				this.Btn3.Visibility = ((Operators.CompareString(Converter.reponseMap, "", false) == 0) ? Visibility.Collapsed : Visibility.Visible);
				this.ShapeLine.StrokeThickness = ModBase.smethod_4(1.0);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "普通弹窗初始化失败", ModBase.LogLevel.Hint, "出现错误");
			}
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0008FDA0 File Offset: 0x0008DFA0
		private void Load(object sender, EventArgs e)
		{
			try
			{
				if (this.Btn2.IsVisible && this.Btn1.ColorType != MyButton.ColorState.Red)
				{
					this.Btn1.ColorType = MyButton.ColorState.Highlight;
				}
				this.Btn1.Focus();
				base.Opacity = 0.0;
				ModAnimation.AniStart(ModAnimation.AaColor(ModMain._ProcessIterator.PanMsg, Panel.BackgroundProperty, (this.m_PageComposer._ClassMap ? new ModBase.MyColor(140.0, 80.0, 0.0, 0.0) : new ModBase.MyColor(90.0, 0.0, 0.0, 0.0)) - ModMain._ProcessIterator.PanMsg.Background, 200, 0, null, false), "PanMsg Background", false);
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaOpacity(this, 1.0, 120, 60, null, false),
					ModAnimation.AaDouble(delegate(object i)
					{
						TranslateTransform transformPos;
						(transformPos = this.TransformPos).Y = Conversions.ToDouble(Operators.AddObject(transformPos.Y, i));
					}, -this.TransformPos.Y, 300, 60, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false),
					ModAnimation.AaDouble(delegate(object i)
					{
						RotateTransform transformRotate;
						(transformRotate = this.TransformRotate).Angle = Conversions.ToDouble(Operators.AddObject(transformRotate.Angle, i));
					}, -this.TransformRotate.Angle, 300, 60, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false)
				}, "MyMsgBox " + Conversions.ToString(this.m_InterceptorComposer), false);
				ModBase.Log("[Control] 普通弹窗：" + this.LabTitle.Text + "\r\n" + this.LabCaption.Text, ModBase.LogLevel.Normal, "出现错误");
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "普通弹窗加载失败", ModBase.LogLevel.Hint, "出现错误");
			}
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0008FFA0 File Offset: 0x0008E1A0
		private void Close()
		{
			if (this.m_PageComposer.m_PolicyMap || Operators.CompareString(this.m_PageComposer.itemMap, "", false) != 0)
			{
				this.m_PageComposer._OrderMap.Continue = false;
			}
			ComponentDispatcher.PopModal();
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaCode((MyMsgText._Closure$__.$I4-0 == null) ? (MyMsgText._Closure$__.$I4-0 = delegate()
				{
					if (!Enumerable.Any<ModMain.MyMsgBoxConverter>(ModMain.m_DispatcherIterator))
					{
						ModAnimation.AniStart(ModAnimation.AaColor(ModMain._ProcessIterator.PanMsg, Panel.BackgroundProperty, new ModBase.MyColor(0.0, 0.0, 0.0, 0.0) - ModMain._ProcessIterator.PanMsg.Background, 200, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false), "", false);
					}
				}) : MyMsgText._Closure$__.$I4-0, 30, false),
				ModAnimation.AaOpacity(this, -base.Opacity, 80, 20, null, false),
				ModAnimation.AaDouble(delegate(object i)
				{
					TranslateTransform transformPos;
					(transformPos = this.TransformPos).Y = Conversions.ToDouble(Operators.AddObject(transformPos.Y, i));
				}, 20.0 - this.TransformPos.Y, 150, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
				ModAnimation.AaDouble(delegate(object i)
				{
					RotateTransform transformRotate;
					(transformRotate = this.TransformRotate).Angle = Conversions.ToDouble(Operators.AddObject(transformRotate.Angle, i));
				}, 6.0 - this.TransformRotate.Angle, 150, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak), false),
				ModAnimation.AaCode(delegate
				{
					((Grid)base.Parent).Children.Remove(this);
				}, 0, true)
			}, "MyMsgBox " + Conversions.ToString(this.m_InterceptorComposer), false);
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x000900E4 File Offset: 0x0008E2E4
		public void Btn1_Click()
		{
			if (!this.m_PageComposer.m_ProducerMap)
			{
				if (this.m_PageComposer.m_GlobalMap != null)
				{
					this.m_PageComposer.m_GlobalMap();
					return;
				}
				this.m_PageComposer.m_ProducerMap = true;
				this.m_PageComposer.schemaMap = 1;
				this.Close();
			}
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00090140 File Offset: 0x0008E340
		public void Btn2_Click()
		{
			if (!this.m_PageComposer.m_ProducerMap)
			{
				if (this.m_PageComposer.m_ExceptionMap != null)
				{
					this.m_PageComposer.m_ExceptionMap();
					return;
				}
				this.m_PageComposer.m_ProducerMap = true;
				this.m_PageComposer.schemaMap = 2;
				this.Close();
			}
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0009019C File Offset: 0x0008E39C
		public void Btn3_Click()
		{
			if (!this.m_PageComposer.m_ProducerMap)
			{
				if (this.m_PageComposer.utilsMap != null)
				{
					this.m_PageComposer.utilsMap();
					return;
				}
				this.m_PageComposer.m_ProducerMap = true;
				this.m_PageComposer.schemaMap = 3;
				this.Close();
			}
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x000901F8 File Offset: 0x0008E3F8
		private void Drag(object sender, MouseButtonEventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				if (e.GetPosition(this.ShapeLine).Y > 2.0)
				{
					goto IL_34;
				}
				IL_28:
				num2 = 3;
				ModMain._ProcessIterator.DragMove();
				IL_34:
				goto IL_93;
				IL_36:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_54:
				goto IL_88;
				IL_56:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_66:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_56;
			}
			IL_88:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_93:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x0000C338 File Offset: 0x0000A538
		// (set) Token: 0x060015A6 RID: 5542 RVA: 0x0000C340 File Offset: 0x0000A540
		internal virtual RotateTransform TransformRotate { get; set; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x0000C349 File Offset: 0x0000A549
		// (set) Token: 0x060015A8 RID: 5544 RVA: 0x0000C351 File Offset: 0x0000A551
		internal virtual TranslateTransform TransformPos { get; set; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x0000C35A File Offset: 0x0000A55A
		// (set) Token: 0x060015AA RID: 5546 RVA: 0x000902B0 File Offset: 0x0008E4B0
		internal virtual Border PanBorder
		{
			[CompilerGenerated]
			get
			{
				return this.m_DispatcherComposer;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.Drag);
				Border dispatcherComposer = this.m_DispatcherComposer;
				if (dispatcherComposer != null)
				{
					dispatcherComposer.MouseLeftButtonDown -= value2;
				}
				this.m_DispatcherComposer = value;
				dispatcherComposer = this.m_DispatcherComposer;
				if (dispatcherComposer != null)
				{
					dispatcherComposer.MouseLeftButtonDown += value2;
				}
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x0000C362 File Offset: 0x0000A562
		// (set) Token: 0x060015AC RID: 5548 RVA: 0x0000C36A File Offset: 0x0000A56A
		internal virtual DropShadowEffect EffectShadow { get; set; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x0000C373 File Offset: 0x0000A573
		// (set) Token: 0x060015AE RID: 5550 RVA: 0x0000C37B File Offset: 0x0000A57B
		internal virtual Grid PanMain { get; set; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x0000C384 File Offset: 0x0000A584
		// (set) Token: 0x060015B0 RID: 5552 RVA: 0x000902F4 File Offset: 0x0008E4F4
		internal virtual TextBlock LabTitle
		{
			[CompilerGenerated]
			get
			{
				return this._RecordComposer;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.Drag);
				TextBlock recordComposer = this._RecordComposer;
				if (recordComposer != null)
				{
					recordComposer.MouseLeftButtonDown -= value2;
				}
				this._RecordComposer = value;
				recordComposer = this._RecordComposer;
				if (recordComposer != null)
				{
					recordComposer.MouseLeftButtonDown += value2;
				}
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x0000C38C File Offset: 0x0000A58C
		// (set) Token: 0x060015B2 RID: 5554 RVA: 0x0000C394 File Offset: 0x0000A594
		internal virtual Rectangle ShapeLine { get; set; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x0000C39D File Offset: 0x0000A59D
		// (set) Token: 0x060015B4 RID: 5556 RVA: 0x0000C3A5 File Offset: 0x0000A5A5
		internal virtual MyScrollViewer PanCaption { get; set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x0000C3AE File Offset: 0x0000A5AE
		// (set) Token: 0x060015B6 RID: 5558 RVA: 0x0000C3B6 File Offset: 0x0000A5B6
		internal virtual TextBlock LabCaption { get; set; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x0000C3BF File Offset: 0x0000A5BF
		// (set) Token: 0x060015B8 RID: 5560 RVA: 0x0000C3C7 File Offset: 0x0000A5C7
		internal virtual StackPanel PanBtn { get; set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x0000C3D0 File Offset: 0x0000A5D0
		// (set) Token: 0x060015BA RID: 5562 RVA: 0x00090338 File Offset: 0x0008E538
		public virtual MyButton Btn1
		{
			[CompilerGenerated]
			get
			{
				return this.m_CreatorComposer;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.Btn1_Click();
				};
				MyButton creatorComposer = this.m_CreatorComposer;
				if (creatorComposer != null)
				{
					creatorComposer.Click -= value2;
				}
				this.m_CreatorComposer = value;
				creatorComposer = this.m_CreatorComposer;
				if (creatorComposer != null)
				{
					creatorComposer.Click += value2;
				}
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x0000C3D8 File Offset: 0x0000A5D8
		// (set) Token: 0x060015BC RID: 5564 RVA: 0x0009037C File Offset: 0x0008E57C
		public virtual MyButton Btn2
		{
			[CompilerGenerated]
			get
			{
				return this.initializerComposer;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.Btn2_Click();
				};
				MyButton myButton = this.initializerComposer;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.initializerComposer = value;
				myButton = this.initializerComposer;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		// (set) Token: 0x060015BE RID: 5566 RVA: 0x000903C0 File Offset: 0x0008E5C0
		public virtual MyButton Btn3
		{
			[CompilerGenerated]
			get
			{
				return this.m_SingletonComposer;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.Btn3_Click();
				};
				MyButton singletonComposer = this.m_SingletonComposer;
				if (singletonComposer != null)
				{
					singletonComposer.Click -= value2;
				}
				this.m_SingletonComposer = value;
				singletonComposer = this.m_SingletonComposer;
				if (singletonComposer != null)
				{
					singletonComposer.Click += value2;
				}
			}
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x00090404 File Offset: 0x0008E604
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.regComposer)
			{
				this.regComposer = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/mymsg/mymsgtext.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x00090434 File Offset: 0x0008E634
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.TransformRotate = (RotateTransform)target;
				return;
			}
			if (connectionId == 2)
			{
				this.TransformPos = (TranslateTransform)target;
				return;
			}
			if (connectionId == 3)
			{
				this.PanBorder = (Border)target;
				return;
			}
			if (connectionId == 4)
			{
				this.EffectShadow = (DropShadowEffect)target;
				return;
			}
			if (connectionId == 5)
			{
				this.PanMain = (Grid)target;
				return;
			}
			if (connectionId == 6)
			{
				this.LabTitle = (TextBlock)target;
				return;
			}
			if (connectionId == 7)
			{
				this.ShapeLine = (Rectangle)target;
				return;
			}
			if (connectionId == 8)
			{
				this.PanCaption = (MyScrollViewer)target;
				return;
			}
			if (connectionId == 9)
			{
				this.LabCaption = (TextBlock)target;
				return;
			}
			if (connectionId == 10)
			{
				this.PanBtn = (StackPanel)target;
				return;
			}
			if (connectionId == 11)
			{
				this.Btn1 = (MyButton)target;
				return;
			}
			if (connectionId == 12)
			{
				this.Btn2 = (MyButton)target;
				return;
			}
			if (connectionId == 13)
			{
				this.Btn3 = (MyButton)target;
				return;
			}
			this.regComposer = true;
		}

		// Token: 0x04000B04 RID: 2820
		private readonly ModMain.MyMsgBoxConverter m_PageComposer;

		// Token: 0x04000B05 RID: 2821
		private readonly int m_InterceptorComposer;

		// Token: 0x04000B06 RID: 2822
		[CompilerGenerated]
		[AccessedThroughProperty("TransformRotate")]
		private RotateTransform m_ContainerComposer;

		// Token: 0x04000B07 RID: 2823
		[AccessedThroughProperty("TransformPos")]
		[CompilerGenerated]
		private TranslateTransform paramsComposer;

		// Token: 0x04000B08 RID: 2824
		[CompilerGenerated]
		[AccessedThroughProperty("PanBorder")]
		private Border m_DispatcherComposer;

		// Token: 0x04000B09 RID: 2825
		[CompilerGenerated]
		[AccessedThroughProperty("EffectShadow")]
		private DropShadowEffect _ProcessComposer;

		// Token: 0x04000B0A RID: 2826
		[CompilerGenerated]
		[AccessedThroughProperty("PanMain")]
		private Grid parameterComposer;

		// Token: 0x04000B0B RID: 2827
		[CompilerGenerated]
		[AccessedThroughProperty("LabTitle")]
		private TextBlock _RecordComposer;

		// Token: 0x04000B0C RID: 2828
		[AccessedThroughProperty("ShapeLine")]
		[CompilerGenerated]
		private Rectangle m_ServiceComposer;

		// Token: 0x04000B0D RID: 2829
		[AccessedThroughProperty("PanCaption")]
		[CompilerGenerated]
		private MyScrollViewer invocationComposer;

		// Token: 0x04000B0E RID: 2830
		[CompilerGenerated]
		[AccessedThroughProperty("LabCaption")]
		private TextBlock _ProxyComposer;

		// Token: 0x04000B0F RID: 2831
		[AccessedThroughProperty("PanBtn")]
		[CompilerGenerated]
		private StackPanel _MessageComposer;

		// Token: 0x04000B10 RID: 2832
		[CompilerGenerated]
		[AccessedThroughProperty("Btn1")]
		private MyButton m_CreatorComposer;

		// Token: 0x04000B11 RID: 2833
		[AccessedThroughProperty("Btn2")]
		[CompilerGenerated]
		private MyButton initializerComposer;

		// Token: 0x04000B12 RID: 2834
		[CompilerGenerated]
		[AccessedThroughProperty("Btn3")]
		private MyButton m_SingletonComposer;

		// Token: 0x04000B13 RID: 2835
		private bool regComposer;
	}
}
