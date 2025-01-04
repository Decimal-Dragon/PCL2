using System;
using System.CodeDom.Compiler;
using System.Collections;
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
	// Token: 0x020000A0 RID: 160
	[DesignerGenerated]
	public class MyMsgSelect : Grid, IComponentConnector
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x00029288 File Offset: 0x00027488
		public MyMsgSelect(ModMain.MyMsgBoxConverter Converter)
		{
			base.Loaded += new RoutedEventHandler(this.Load);
			this.m_PredicateBroadcaster = ModBase.GetUuid();
			this._PoolBroadcaster = -1;
			try
			{
				this.InitializeComponent();
				this.Btn1.Name = this.Btn1.Name + Conversions.ToString(ModBase.GetUuid());
				this.Btn2.Name = this.Btn2.Name + Conversions.ToString(ModBase.GetUuid());
				this.databaseBroadcaster = Converter;
				this.LabTitle.Text = Converter.Title;
				this.Btn1.Text = Converter.m_BridgeMap;
				if (Converter._ClassMap)
				{
					this.Btn1.ColorType = MyButton.ColorState.Red;
					this.LabTitle.SetResourceReference(TextBlock.ForegroundProperty, "ColorBrushRedLight");
				}
				this.Btn2.Text = Converter.itemMap;
				this.Btn2.Visibility = ((Operators.CompareString(Converter.itemMap, "", false) == 0) ? Visibility.Collapsed : Visibility.Visible);
				this.ShapeLine.StrokeThickness = ModBase.smethod_4(1.0);
				this.Btn1.IsEnabled = false;
				try
				{
					foreach (object obj in ((IEnumerable)Converter.m_CollectionMap))
					{
						IMyRadio myRadio = (IMyRadio)obj;
						this.PanSelection.Children.Add((UIElement)myRadio);
						myRadio.Check += delegate(object sender, ModBase.RouteEventArgs e)
						{
							this.OnChecked((IMyRadio)sender, e);
						};
						if (myRadio is MyListItem)
						{
							((MyListItem)myRadio).Type = MyListItem.CheckType.RadioBox;
							((MyListItem)myRadio).MinHeight = 24.0;
						}
						else
						{
							((MyRadioBox)myRadio).MinHeight = 24.0;
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
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "选择弹窗初始化失败", ModBase.LogLevel.Hint, "出现错误");
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000294B4 File Offset: 0x000276B4
		private void Load(object sender, EventArgs e)
		{
			try
			{
				if (this.Btn2.IsVisible && this.Btn1.ColorType != MyButton.ColorState.Red)
				{
					this.Btn1.ColorType = MyButton.ColorState.Highlight;
				}
				base.Opacity = 0.0;
				ModAnimation.AniStart(ModAnimation.AaColor(ModMain._ProcessIterator.PanMsg, Panel.BackgroundProperty, (this.databaseBroadcaster._ClassMap ? new ModBase.MyColor(140.0, 80.0, 0.0, 0.0) : new ModBase.MyColor(90.0, 0.0, 0.0, 0.0)) - ModMain._ProcessIterator.PanMsg.Background, 200, 0, null, false), "PanMsg Background", false);
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
				}, "MyMsgBox " + Conversions.ToString(this.m_PredicateBroadcaster), false);
				ModBase.Log("[Control] 选择弹窗：" + this.LabTitle.Text, ModBase.LogLevel.Normal, "出现错误");
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "选择弹窗加载失败", ModBase.LogLevel.Hint, "出现错误");
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00029698 File Offset: 0x00027898
		private void Close()
		{
			this.databaseBroadcaster._OrderMap.Continue = false;
			ComponentDispatcher.PopModal();
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaCode((MyMsgSelect._Closure$__.$I4-0 == null) ? (MyMsgSelect._Closure$__.$I4-0 = delegate()
				{
					if (!Enumerable.Any<ModMain.MyMsgBoxConverter>(ModMain.m_DispatcherIterator))
					{
						ModAnimation.AniStart(ModAnimation.AaColor(ModMain._ProcessIterator.PanMsg, Panel.BackgroundProperty, new ModBase.MyColor(0.0, 0.0, 0.0, 0.0) - ModMain._ProcessIterator.PanMsg.Background, 200, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false), "", false);
					}
				}) : MyMsgSelect._Closure$__.$I4-0, 30, false),
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
			}, "MyMsgBox " + Conversions.ToString(this.m_PredicateBroadcaster), false);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000485F File Offset: 0x00002A5F
		public void Btn1_Click()
		{
			if (!this.databaseBroadcaster.m_ProducerMap && this._PoolBroadcaster != -1)
			{
				this.databaseBroadcaster.m_ProducerMap = true;
				this.databaseBroadcaster.schemaMap = this._PoolBroadcaster;
				this.Close();
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000489F File Offset: 0x00002A9F
		public void Btn2_Click()
		{
			if (!this.databaseBroadcaster.m_ProducerMap)
			{
				this.databaseBroadcaster.m_ProducerMap = true;
				this.databaseBroadcaster.schemaMap = null;
				this.Close();
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000048CC File Offset: 0x00002ACC
		private void OnChecked(IMyRadio sender, EventArgs e)
		{
			this.Btn1.IsEnabled = true;
			this._PoolBroadcaster = this.PanSelection.Children.IndexOf((UIElement)sender);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000297B8 File Offset: 0x000279B8
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

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x000048F6 File Offset: 0x00002AF6
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x000048FE File Offset: 0x00002AFE
		internal virtual RotateTransform TransformRotate { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00004907 File Offset: 0x00002B07
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x0000490F File Offset: 0x00002B0F
		internal virtual TranslateTransform TransformPos { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00004918 File Offset: 0x00002B18
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x00029870 File Offset: 0x00027A70
		internal virtual Border PanBorder
		{
			[CompilerGenerated]
			get
			{
				return this.interceptorBroadcaster;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.Drag);
				Border border = this.interceptorBroadcaster;
				if (border != null)
				{
					border.MouseLeftButtonDown -= value2;
				}
				this.interceptorBroadcaster = value;
				border = this.interceptorBroadcaster;
				if (border != null)
				{
					border.MouseLeftButtonDown += value2;
				}
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00004920 File Offset: 0x00002B20
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x00004928 File Offset: 0x00002B28
		internal virtual DropShadowEffect EffectShadow { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00004931 File Offset: 0x00002B31
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x00004939 File Offset: 0x00002B39
		internal virtual Grid PanMain { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00004942 File Offset: 0x00002B42
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x000298B4 File Offset: 0x00027AB4
		internal virtual TextBlock LabTitle
		{
			[CompilerGenerated]
			get
			{
				return this.m_DispatcherBroadcaster;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.Drag);
				TextBlock dispatcherBroadcaster = this.m_DispatcherBroadcaster;
				if (dispatcherBroadcaster != null)
				{
					dispatcherBroadcaster.MouseLeftButtonDown -= value2;
				}
				this.m_DispatcherBroadcaster = value;
				dispatcherBroadcaster = this.m_DispatcherBroadcaster;
				if (dispatcherBroadcaster != null)
				{
					dispatcherBroadcaster.MouseLeftButtonDown += value2;
				}
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000494A File Offset: 0x00002B4A
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x00004952 File Offset: 0x00002B52
		internal virtual Rectangle ShapeLine { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000495B File Offset: 0x00002B5B
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x00004963 File Offset: 0x00002B63
		internal virtual MyScrollViewer PanCaption { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000496C File Offset: 0x00002B6C
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x00004974 File Offset: 0x00002B74
		internal virtual StackPanel PanSelection { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000497D File Offset: 0x00002B7D
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00004985 File Offset: 0x00002B85
		internal virtual StackPanel PanBtn { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000498E File Offset: 0x00002B8E
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x000298F8 File Offset: 0x00027AF8
		public virtual MyButton Btn1
		{
			[CompilerGenerated]
			get
			{
				return this.invocationBroadcaster;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.Btn1_Click();
				};
				MyButton myButton = this.invocationBroadcaster;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.invocationBroadcaster = value;
				myButton = this.invocationBroadcaster;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00004996 File Offset: 0x00002B96
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x0002993C File Offset: 0x00027B3C
		public virtual MyButton Btn2
		{
			[CompilerGenerated]
			get
			{
				return this._ProxyBroadcaster;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.Btn2_Click();
				};
				MyButton proxyBroadcaster = this._ProxyBroadcaster;
				if (proxyBroadcaster != null)
				{
					proxyBroadcaster.Click -= value2;
				}
				this._ProxyBroadcaster = value;
				proxyBroadcaster = this._ProxyBroadcaster;
				if (proxyBroadcaster != null)
				{
					proxyBroadcaster.Click += value2;
				}
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00029980 File Offset: 0x00027B80
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_MessageBroadcaster)
			{
				this.m_MessageBroadcaster = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/mymsg/mymsgselect.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000299B0 File Offset: 0x00027BB0
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
				this.PanCaption = (MyScrollViewer)target;
				return;
			}
			if (connectionId == 9)
			{
				this.PanSelection = (StackPanel)target;
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
			this.m_MessageBroadcaster = true;
		}

		// Token: 0x0400027A RID: 634
		private readonly ModMain.MyMsgBoxConverter databaseBroadcaster;

		// Token: 0x0400027B RID: 635
		private readonly int m_PredicateBroadcaster;

		// Token: 0x0400027C RID: 636
		private int _PoolBroadcaster;

		// Token: 0x0400027D RID: 637
		[AccessedThroughProperty("TransformRotate")]
		[CompilerGenerated]
		private RotateTransform customerBroadcaster;

		// Token: 0x0400027E RID: 638
		[CompilerGenerated]
		[AccessedThroughProperty("TransformPos")]
		private TranslateTransform m_PageBroadcaster;

		// Token: 0x0400027F RID: 639
		[AccessedThroughProperty("PanBorder")]
		[CompilerGenerated]
		private Border interceptorBroadcaster;

		// Token: 0x04000280 RID: 640
		[CompilerGenerated]
		[AccessedThroughProperty("EffectShadow")]
		private DropShadowEffect m_ContainerBroadcaster;

		// Token: 0x04000281 RID: 641
		[AccessedThroughProperty("PanMain")]
		[CompilerGenerated]
		private Grid m_ParamsBroadcaster;

		// Token: 0x04000282 RID: 642
		[AccessedThroughProperty("LabTitle")]
		[CompilerGenerated]
		private TextBlock m_DispatcherBroadcaster;

		// Token: 0x04000283 RID: 643
		[CompilerGenerated]
		[AccessedThroughProperty("ShapeLine")]
		private Rectangle processBroadcaster;

		// Token: 0x04000284 RID: 644
		[AccessedThroughProperty("PanCaption")]
		[CompilerGenerated]
		private MyScrollViewer m_ParameterBroadcaster;

		// Token: 0x04000285 RID: 645
		[CompilerGenerated]
		[AccessedThroughProperty("PanSelection")]
		private StackPanel m_RecordBroadcaster;

		// Token: 0x04000286 RID: 646
		[CompilerGenerated]
		[AccessedThroughProperty("PanBtn")]
		private StackPanel _ServiceBroadcaster;

		// Token: 0x04000287 RID: 647
		[AccessedThroughProperty("Btn1")]
		[CompilerGenerated]
		private MyButton invocationBroadcaster;

		// Token: 0x04000288 RID: 648
		[CompilerGenerated]
		[AccessedThroughProperty("Btn2")]
		private MyButton _ProxyBroadcaster;

		// Token: 0x04000289 RID: 649
		private bool m_MessageBroadcaster;
	}
}
