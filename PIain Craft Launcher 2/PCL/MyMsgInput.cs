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
	// Token: 0x0200009E RID: 158
	[DesignerGenerated]
	public class MyMsgInput : Grid, IComponentConnector
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x00028920 File Offset: 0x00026B20
		public MyMsgInput(ModMain.MyMsgBoxConverter Converter)
		{
			base.Loaded += new RoutedEventHandler(this.Load);
			this.m_BaseBroadcaster = ModBase.GetUuid();
			try
			{
				this.InitializeComponent();
				this.Btn1.Name = this.Btn1.Name + Conversions.ToString(ModBase.GetUuid());
				this.Btn2.Name = this.Btn2.Name + Conversions.ToString(ModBase.GetUuid());
				this.m_WrapperBroadcaster = Converter;
				this.LabTitle.Text = Converter.Title;
				this.LabText.Text = Converter.listenerMap;
				this.PanText.Visibility = ((Operators.CompareString(Converter.listenerMap, "", false) == 0) ? Visibility.Collapsed : Visibility.Visible);
				this.TextArea.Text = Conversions.ToString(Converter.m_CollectionMap);
				this.TextArea.HintText = Converter.m_ValueMap;
				this.TextArea.ValidateRules = Converter.m_VisitorMap;
				this.Btn1.Text = Converter.m_BridgeMap;
				if (Converter._ClassMap)
				{
					this.Btn1.ColorType = MyButton.ColorState.Red;
					this.LabTitle.SetResourceReference(TextBlock.ForegroundProperty, "ColorBrushRedLight");
				}
				this.Btn2.Text = Converter.itemMap;
				this.Btn2.Visibility = ((Operators.CompareString(Converter.itemMap, "", false) == 0) ? Visibility.Collapsed : Visibility.Visible);
				this.ShapeLine.StrokeThickness = ModBase.smethod_4(1.0);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "输入弹窗初始化失败", ModBase.LogLevel.Hint, "出现错误");
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00028AE8 File Offset: 0x00026CE8
		private void Load(object sender, EventArgs e)
		{
			try
			{
				if (this.Btn2.IsVisible && this.Btn1.ColorType != MyButton.ColorState.Red)
				{
					this.Btn1.ColorType = MyButton.ColorState.Highlight;
				}
				this.TextArea.Focus();
				this.TextArea.SelectionStart = this.TextArea.Text.Length;
				base.Opacity = 0.0;
				ModAnimation.AniStart(ModAnimation.AaColor(ModMain._ProcessIterator.PanMsg, Panel.BackgroundProperty, (this.m_WrapperBroadcaster._ClassMap ? new ModBase.MyColor(140.0, 80.0, 0.0, 0.0) : new ModBase.MyColor(90.0, 0.0, 0.0, 0.0)) - ModMain._ProcessIterator.PanMsg.Background, 200, 0, null, false), "PanMsg Background", false);
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
				}, "MyMsgBox " + Conversions.ToString(this.m_BaseBroadcaster), false);
				ModBase.Log("[Control] 输入弹窗：" + this.LabTitle.Text, ModBase.LogLevel.Normal, "出现错误");
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "输入弹窗加载失败", ModBase.LogLevel.Hint, "出现错误");
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00028CF4 File Offset: 0x00026EF4
		private void Close()
		{
			this.m_WrapperBroadcaster._OrderMap.Continue = false;
			ComponentDispatcher.PopModal();
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaCode((MyMsgInput._Closure$__.$I4-0 == null) ? (MyMsgInput._Closure$__.$I4-0 = delegate()
				{
					if (!Enumerable.Any<ModMain.MyMsgBoxConverter>(ModMain.m_DispatcherIterator))
					{
						ModAnimation.AniStart(ModAnimation.AaColor(ModMain._ProcessIterator.PanMsg, Panel.BackgroundProperty, new ModBase.MyColor(0.0, 0.0, 0.0, 0.0) - ModMain._ProcessIterator.PanMsg.Background, 200, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false), "", false);
					}
				}) : MyMsgInput._Closure$__.$I4-0, 30, false),
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
			}, "MyMsgBox " + Conversions.ToString(this.m_BaseBroadcaster), false);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00028E14 File Offset: 0x00027014
		public void Btn1_Click()
		{
			if (!this.m_WrapperBroadcaster.m_ProducerMap && Operators.CompareString(this.TextArea.ValidateResult, "", false) == 0)
			{
				this.m_WrapperBroadcaster.m_ProducerMap = true;
				this.m_WrapperBroadcaster.schemaMap = this.TextArea.Text;
				this.Close();
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00004725 File Offset: 0x00002925
		public void Btn2_Click()
		{
			if (!this.m_WrapperBroadcaster.m_ProducerMap)
			{
				this.m_WrapperBroadcaster.m_ProducerMap = true;
				this.m_WrapperBroadcaster.schemaMap = null;
				this.Close();
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00004752 File Offset: 0x00002952
		private void TextCaption_ValidateChanged(object sender, EventArgs e)
		{
			this.Btn1.IsEnabled = (Operators.CompareString(this.TextArea.ValidateResult, "", false) == 0);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00028E70 File Offset: 0x00027070
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

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00004778 File Offset: 0x00002978
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x00004780 File Offset: 0x00002980
		internal virtual RotateTransform TransformRotate { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00004789 File Offset: 0x00002989
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x00004791 File Offset: 0x00002991
		internal virtual TranslateTransform TransformPos { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000479A File Offset: 0x0000299A
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x00028F28 File Offset: 0x00027128
		internal virtual Border PanBorder
		{
			[CompilerGenerated]
			get
			{
				return this.m_PrototypeBroadcaster;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.Drag);
				Border prototypeBroadcaster = this.m_PrototypeBroadcaster;
				if (prototypeBroadcaster != null)
				{
					prototypeBroadcaster.MouseLeftButtonDown -= value2;
				}
				this.m_PrototypeBroadcaster = value;
				prototypeBroadcaster = this.m_PrototypeBroadcaster;
				if (prototypeBroadcaster != null)
				{
					prototypeBroadcaster.MouseLeftButtonDown += value2;
				}
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x000047A2 File Offset: 0x000029A2
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x000047AA File Offset: 0x000029AA
		internal virtual DropShadowEffect EffectShadow { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x000047B3 File Offset: 0x000029B3
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x000047BB File Offset: 0x000029BB
		internal virtual Grid PanMain { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x000047C4 File Offset: 0x000029C4
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x00028F6C File Offset: 0x0002716C
		internal virtual TextBlock LabTitle
		{
			[CompilerGenerated]
			get
			{
				return this._AdapterBroadcaster;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.Drag);
				TextBlock adapterBroadcaster = this._AdapterBroadcaster;
				if (adapterBroadcaster != null)
				{
					adapterBroadcaster.MouseLeftButtonDown -= value2;
				}
				this._AdapterBroadcaster = value;
				adapterBroadcaster = this._AdapterBroadcaster;
				if (adapterBroadcaster != null)
				{
					adapterBroadcaster.MouseLeftButtonDown += value2;
				}
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x000047CC File Offset: 0x000029CC
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x000047D4 File Offset: 0x000029D4
		internal virtual Rectangle ShapeLine { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x000047DD File Offset: 0x000029DD
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x000047E5 File Offset: 0x000029E5
		internal virtual MyScrollViewer PanText { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x000047EE File Offset: 0x000029EE
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x000047F6 File Offset: 0x000029F6
		internal virtual TextBlock LabText { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x000047FF File Offset: 0x000029FF
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x00028FB0 File Offset: 0x000271B0
		internal virtual MyTextBox TextArea
		{
			[CompilerGenerated]
			get
			{
				return this.algoBroadcaster;
			}
			[CompilerGenerated]
			set
			{
				MyTextBox.ValidateChangedEventHandler obj = new MyTextBox.ValidateChangedEventHandler(this.TextCaption_ValidateChanged);
				if (this.algoBroadcaster != null)
				{
					MyTextBox.MapReader(obj);
				}
				this.algoBroadcaster = value;
				if (this.algoBroadcaster != null)
				{
					MyTextBox.SortReader(obj);
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00004807 File Offset: 0x00002A07
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x0000480F File Offset: 0x00002A0F
		internal virtual StackPanel PanBtn { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00004818 File Offset: 0x00002A18
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x00028FF0 File Offset: 0x000271F0
		public virtual MyButton Btn1
		{
			[CompilerGenerated]
			get
			{
				return this.mappingBroadcaster;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.Btn1_Click();
				};
				MyButton myButton = this.mappingBroadcaster;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.mappingBroadcaster = value;
				myButton = this.mappingBroadcaster;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00004820 File Offset: 0x00002A20
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x00029034 File Offset: 0x00027234
		public virtual MyButton Btn2
		{
			[CompilerGenerated]
			get
			{
				return this._TokenizerBroadcaster;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.Btn2_Click();
				};
				MyButton tokenizerBroadcaster = this._TokenizerBroadcaster;
				if (tokenizerBroadcaster != null)
				{
					tokenizerBroadcaster.Click -= value2;
				}
				this._TokenizerBroadcaster = value;
				tokenizerBroadcaster = this._TokenizerBroadcaster;
				if (tokenizerBroadcaster != null)
				{
					tokenizerBroadcaster.Click += value2;
				}
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00029078 File Offset: 0x00027278
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.filterBroadcaster)
			{
				this.filterBroadcaster = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/mymsg/mymsginput.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000290A8 File Offset: 0x000272A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
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
				this.PanText = (MyScrollViewer)target;
				return;
			}
			if (connectionId == 9)
			{
				this.LabText = (TextBlock)target;
				return;
			}
			if (connectionId == 10)
			{
				this.TextArea = (MyTextBox)target;
				return;
			}
			if (connectionId == 11)
			{
				this.PanBtn = (StackPanel)target;
				return;
			}
			if (connectionId == 12)
			{
				this.Btn1 = (MyButton)target;
				return;
			}
			if (connectionId == 13)
			{
				this.Btn2 = (MyButton)target;
				return;
			}
			this.filterBroadcaster = true;
		}

		// Token: 0x04000268 RID: 616
		private readonly ModMain.MyMsgBoxConverter m_WrapperBroadcaster;

		// Token: 0x04000269 RID: 617
		private readonly int m_BaseBroadcaster;

		// Token: 0x0400026A RID: 618
		[AccessedThroughProperty("TransformRotate")]
		[CompilerGenerated]
		private RotateTransform attributeBroadcaster;

		// Token: 0x0400026B RID: 619
		[CompilerGenerated]
		[AccessedThroughProperty("TransformPos")]
		private TranslateTransform _CodeBroadcaster;

		// Token: 0x0400026C RID: 620
		[CompilerGenerated]
		[AccessedThroughProperty("PanBorder")]
		private Border m_PrototypeBroadcaster;

		// Token: 0x0400026D RID: 621
		[CompilerGenerated]
		[AccessedThroughProperty("EffectShadow")]
		private DropShadowEffect annotationBroadcaster;

		// Token: 0x0400026E RID: 622
		[CompilerGenerated]
		[AccessedThroughProperty("PanMain")]
		private Grid infoBroadcaster;

		// Token: 0x0400026F RID: 623
		[AccessedThroughProperty("LabTitle")]
		[CompilerGenerated]
		private TextBlock _AdapterBroadcaster;

		// Token: 0x04000270 RID: 624
		[CompilerGenerated]
		[AccessedThroughProperty("ShapeLine")]
		private Rectangle m_FacadeBroadcaster;

		// Token: 0x04000271 RID: 625
		[AccessedThroughProperty("PanText")]
		[CompilerGenerated]
		private MyScrollViewer _MerchantBroadcaster;

		// Token: 0x04000272 RID: 626
		[AccessedThroughProperty("LabText")]
		[CompilerGenerated]
		private TextBlock authenticationBroadcaster;

		// Token: 0x04000273 RID: 627
		[AccessedThroughProperty("TextArea")]
		[CompilerGenerated]
		private MyTextBox algoBroadcaster;

		// Token: 0x04000274 RID: 628
		[AccessedThroughProperty("PanBtn")]
		[CompilerGenerated]
		private StackPanel _ComparatorBroadcaster;

		// Token: 0x04000275 RID: 629
		[CompilerGenerated]
		[AccessedThroughProperty("Btn1")]
		private MyButton mappingBroadcaster;

		// Token: 0x04000276 RID: 630
		[AccessedThroughProperty("Btn2")]
		[CompilerGenerated]
		private MyButton _TokenizerBroadcaster;

		// Token: 0x04000277 RID: 631
		private bool filterBroadcaster;
	}
}
