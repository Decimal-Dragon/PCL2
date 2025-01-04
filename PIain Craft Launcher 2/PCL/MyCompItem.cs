using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020000DE RID: 222
	[DesignerGenerated]
	public class MyCompItem : Grid, IComponentConnector
	{
		// Token: 0x06000692 RID: 1682 RVA: 0x0003D938 File Offset: 0x0003BB38
		public MyCompItem()
		{
			base.PreviewMouseLeftButtonUp += this.Button_MouseUp;
			this.Click += delegate(object sender, MouseButtonEventArgs e)
			{
				this.ProjectClick((MyCompItem)sender, e);
			};
			base.PreviewMouseLeftButtonDown += this.Button_MouseDown;
			base.MouseLeave += new MouseEventHandler(this.Button_MouseLeave);
			base.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(this.Button_MouseLeave);
			base.MouseEnter += new MouseEventHandler(this.RefreshColor);
			base.MouseLeave += new MouseEventHandler(this.RefreshColor);
			base.MouseLeftButtonDown += new MouseButtonEventHandler(this.RefreshColor);
			base.MouseLeftButtonUp += new MouseButtonEventHandler(this.RefreshColor);
			this.m_AttrField = ModBase.GetUuid();
			this._AdvisorField = false;
			this._AccountField = null;
			this._EventField = true;
			this.InitializeComponent();
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00005614 File Offset: 0x00003814
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x00005621 File Offset: 0x00003821
		public string Logo
		{
			get
			{
				return this.PathLogo.Source;
			}
			set
			{
				this.PathLogo.Source = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0000562F File Offset: 0x0000382F
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x0000563C File Offset: 0x0000383C
		public string Title
		{
			get
			{
				return this.LabTitle.Text;
			}
			set
			{
				if (Operators.CompareString(this.LabTitle.Text, value, false) != 0)
				{
					this.LabTitle.Text = value;
				}
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0000565E File Offset: 0x0000385E
		public string StopReader()
		{
			TextBlock labTitleRaw = this.LabTitleRaw;
			string result;
			if (labTitleRaw != null)
			{
				if ((result = labTitleRaw.Text) != null)
				{
					return result;
				}
			}
			result = "";
			return result;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0000567B File Offset: 0x0000387B
		public void RestartReader(string value)
		{
			if (Operators.CompareString(this.LabTitleRaw.Text, value, false) != 0)
			{
				this.LabTitleRaw.Text = value;
				this.LabTitleRaw.Visibility = ((Operators.CompareString(value, "", false) == 0) ? Visibility.Collapsed : Visibility.Visible);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x000056BA File Offset: 0x000038BA
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x000056C7 File Offset: 0x000038C7
		public string Description
		{
			get
			{
				return this.LabInfo.Text;
			}
			set
			{
				if (Operators.CompareString(this.LabInfo.Text, value, false) != 0)
				{
					this.LabInfo.Text = value;
				}
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0003DA14 File Offset: 0x0003BC14
		private void LabInfo_MouseEnter(object sender, MouseEventArgs e)
		{
			if (this.IsTextTrimmed(this.LabInfo))
			{
				this.ToolTipInfo.Content = this.LabInfo.Text;
				this.ToolTipInfo.Width = this.LabInfo.ActualWidth + 25.0;
				this.LabInfo.ToolTip = this.ToolTipInfo;
				return;
			}
			this.LabInfo.ToolTip = null;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0003DA84 File Offset: 0x0003BC84
		private bool IsTextTrimmed(TextBlock textBlock)
		{
			Typeface typeface = new Typeface(textBlock.FontFamily, textBlock.FontStyle, textBlock.FontWeight, textBlock.FontStretch);
			return new FormattedText(textBlock.Text, Thread.CurrentThread.CurrentCulture, textBlock.FlowDirection, typeface, textBlock.FontSize, textBlock.Foreground, (double)ModBase._ConfigurationRepository).Width > textBlock.ActualWidth;
		}

		// Token: 0x17000094 RID: 148
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x0003DAEC File Offset: 0x0003BCEC
		public List<string> Tags
		{
			set
			{
				this.PanTags.Children.Clear();
				this.PanTags.Visibility = (Enumerable.Any<string>(value) ? Visibility.Visible : Visibility.Collapsed);
				try
				{
					foreach (string str in value)
					{
						object objectValue = RuntimeHelpers.GetObjectValue(ModBase.GetObjectFromXML("<Border xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\r\n                         Background=\"#11000000\" Padding=\"3,1\" CornerRadius=\"3\" Margin=\"0,0,3,0\" \r\n                         SnapsToDevicePixels=\"True\" UseLayoutRounding=\"False\">\r\n                   <TextBlock Text=\"" + str + "\" Foreground=\"#868686\" FontSize=\"11\" />\r\n                </Border>"));
						this.PanTags.Children.Add((UIElement)objectValue);
					}
				}
				finally
				{
					List<string>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600069E RID: 1694 RVA: 0x0003DB8C File Offset: 0x0003BD8C
		// (remove) Token: 0x0600069F RID: 1695 RVA: 0x0003DBC4 File Offset: 0x0003BDC4
		public event MyCompItem.ClickEventHandler Click
		{
			[CompilerGenerated]
			add
			{
				MyCompItem.ClickEventHandler clickEventHandler = this._CandidateField;
				MyCompItem.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MyCompItem.ClickEventHandler value2 = (MyCompItem.ClickEventHandler)Delegate.Combine(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MyCompItem.ClickEventHandler>(ref this._CandidateField, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				MyCompItem.ClickEventHandler clickEventHandler = this._CandidateField;
				MyCompItem.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MyCompItem.ClickEventHandler value2 = (MyCompItem.ClickEventHandler)Delegate.Remove(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MyCompItem.ClickEventHandler>(ref this._CandidateField, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0003DBFC File Offset: 0x0003BDFC
		private void Button_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (this._AdvisorField)
			{
				MyCompItem.ClickEventHandler candidateField = this._CandidateField;
				if (candidateField != null)
				{
					candidateField(RuntimeHelpers.GetObjectValue(sender), e);
				}
				if (!e.Handled)
				{
					ModBase.Log("[Control] 按下资源工程列表项：" + this.LabTitle.Text, ModBase.LogLevel.Normal, "出现错误");
				}
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0003DC50 File Offset: 0x0003BE50
		private void ProjectClick(MyCompItem sender, EventArgs e)
		{
			List<string> list = new List<string>();
			if (ModMain._ProcessIterator._MethodIterator.initializerMap == FormMain.PageType.CompDetail)
			{
				try
				{
					foreach (object obj in ModMain.iteratorRepository.PanResults.Children)
					{
						MyCard myCard = (MyCard)obj;
						if (Operators.CompareString(myCard.Title, "", false) != 0 && !myCard.IsSwaped)
						{
							list.Add(myCard.Title);
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
				ModBase.Log("[Comp] 记录当前已展开的卡片：" + string.Join("、", list), ModBase.LogLevel.Normal, "出现错误");
				NewLateBinding.LateIndexSet(ModMain._ProcessIterator._MethodIterator.m_SingletonMap, new object[]
				{
					1,
					list
				}, null);
			}
			string text;
			ModComp.CompModLoaderType compModLoaderType;
			if (ModMain._ProcessIterator._MethodIterator.initializerMap == FormMain.PageType.CompDetail)
			{
				text = Conversions.ToString(NewLateBinding.LateIndexGet(ModMain._ProcessIterator._MethodIterator.m_SingletonMap, new object[]
				{
					2
				}, null));
				compModLoaderType = (ModComp.CompModLoaderType)Conversions.ToInteger(NewLateBinding.LateIndexGet(ModMain._ProcessIterator._MethodIterator.m_SingletonMap, new object[]
				{
					3
				}, null));
			}
			else
			{
				ModComp.CompType type = ((ModComp.CompProject)sender.Tag).Type;
				if (type != ModComp.CompType.Mod)
				{
					if (type != ModComp.CompType.ModPack)
					{
						text = "";
					}
					else
					{
						text = (PageDownloadPack._UtilsReader.Input.m_ParameterRepository ?? "");
					}
				}
				else
				{
					text = (PageDownloadMod.interceptorField.Input.m_ParameterRepository ?? "");
					compModLoaderType = PageDownloadMod.interceptorField.Input.processRepository;
				}
			}
			if (((ModComp.CompProject)sender.Tag).Type != ModComp.CompType.Mod)
			{
				compModLoaderType = ModComp.CompModLoaderType.Any;
			}
			ModMain._ProcessIterator.PageChange(new FormMain.PageStackData
			{
				initializerMap = FormMain.PageType.CompDetail,
				m_SingletonMap = new object[]
				{
					sender.Tag,
					new List<string>(),
					text,
					compModLoaderType
				}
			}, FormMain.PageSubType.Default);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x000056E9 File Offset: 0x000038E9
		private void Button_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (base.IsMouseOver && this._EventField)
			{
				this._AdvisorField = true;
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00005702 File Offset: 0x00003902
		private void Button_MouseLeave(object sender, object e)
		{
			this._AdvisorField = false;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0003DE68 File Offset: 0x0003C068
		public Border RectBack
		{
			get
			{
				if (this._AccountField == null)
				{
					Border border = new Border
					{
						Name = "RectBack",
						CornerRadius = new CornerRadius(3.0),
						RenderTransform = new ScaleTransform(0.8, 0.8),
						RenderTransformOrigin = new Point(0.5, 0.5),
						BorderThickness = new Thickness(ModBase.smethod_4(1.0)),
						SnapsToDevicePixels = true,
						IsHitTestVisible = false,
						Opacity = 0.0
					};
					border.SetResourceReference(Border.BackgroundProperty, "ColorBrush7");
					border.SetResourceReference(Border.BorderBrushProperty, "ColorBrush6");
					Grid.SetColumnSpan(border, 999);
					Grid.SetRowSpan(border, 999);
					base.Children.Insert(0, border);
					this._AccountField = border;
				}
				return this._AccountField;
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0003DF68 File Offset: 0x0003C168
		public void RefreshColor(object sender, EventArgs e)
		{
			if (this._EventField)
			{
				string right;
				int num;
				if (base.IsMouseOver)
				{
					if (this._AdvisorField)
					{
						right = "MouseDown";
						num = 120;
					}
					else
					{
						right = "MouseOver";
						num = 120;
					}
				}
				else
				{
					right = "Idle";
					num = 180;
				}
				if (Operators.CompareString(this.queueField, right, false) != 0)
				{
					this.queueField = right;
					if (base.IsLoaded && ModAnimation.CalcParser() == 0)
					{
						List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
						if (base.IsMouseOver)
						{
							list.AddRange(new ModAnimation.AniData[]
							{
								ModAnimation.AaColor(this.RectBack, Border.BackgroundProperty, this._AdvisorField ? "ColorBrush6" : "ColorBrushBg1", num, 0, null, false),
								ModAnimation.AaOpacity(this.RectBack, 1.0 - this.RectBack.Opacity, num, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false)
							});
							if (this._AdvisorField)
							{
								list.Add(ModAnimation.AaScaleTransform(this.RectBack, 0.996 - ((ScaleTransform)this.RectBack.RenderTransform).ScaleX, checked((int)Math.Round(unchecked((double)num * 1.2))), 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false));
							}
							else
							{
								list.Add(ModAnimation.AaScaleTransform(this.RectBack, 1.0 - ((ScaleTransform)this.RectBack.RenderTransform).ScaleX, checked((int)Math.Round(unchecked((double)num * 1.2))), 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false));
							}
						}
						else
						{
							list.AddRange(new ModAnimation.AniData[]
							{
								ModAnimation.AaOpacity(this.RectBack, -this.RectBack.Opacity, num, 0, null, false),
								ModAnimation.AaColor(this.RectBack, Border.BackgroundProperty, this._AdvisorField ? "ColorBrush6" : "ColorBrush7", num, 0, null, false),
								ModAnimation.AaScaleTransform(this.RectBack, 0.996 - ((ScaleTransform)this.RectBack.RenderTransform).ScaleX, num, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
								ModAnimation.AaScaleTransform(this.RectBack, -0.196, 1, 0, null, true)
							});
						}
						ModAnimation.AniStart(list, "CompItem Color " + Conversions.ToString(this.m_AttrField), false);
						return;
					}
					ModAnimation.AniStop("CompItem Color " + Conversions.ToString(this.m_AttrField));
					if (this._AccountField != null)
					{
						this.RectBack.Opacity = 0.0;
					}
				}
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0000570B File Offset: 0x0000390B
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x00005713 File Offset: 0x00003913
		internal virtual MyCompItem PanBack { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0000571C File Offset: 0x0000391C
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x00005724 File Offset: 0x00003924
		internal virtual MyImage PathLogo { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0000572D File Offset: 0x0000392D
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x00005735 File Offset: 0x00003935
		internal virtual TextBlock LabTitle { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0000573E File Offset: 0x0000393E
		// (set) Token: 0x060006AD RID: 1709 RVA: 0x00005746 File Offset: 0x00003946
		internal virtual TextBlock LabTitleRaw { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0000574F File Offset: 0x0000394F
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x00005757 File Offset: 0x00003957
		internal virtual StackPanel PanTags { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00005760 File Offset: 0x00003960
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x0003E20C File Offset: 0x0003C40C
		internal virtual TextBlock LabInfo
		{
			[CompilerGenerated]
			get
			{
				return this._CodeField;
			}
			[CompilerGenerated]
			set
			{
				MouseEventHandler value2 = new MouseEventHandler(this.LabInfo_MouseEnter);
				TextBlock codeField = this._CodeField;
				if (codeField != null)
				{
					codeField.MouseEnter -= value2;
				}
				this._CodeField = value;
				codeField = this._CodeField;
				if (codeField != null)
				{
					codeField.MouseEnter += value2;
				}
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00005768 File Offset: 0x00003968
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x00005770 File Offset: 0x00003970
		internal virtual ToolTip ToolTipInfo { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00005779 File Offset: 0x00003979
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x00005781 File Offset: 0x00003981
		internal virtual ColumnDefinition ColumnVersion1 { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0000578A File Offset: 0x0000398A
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x00005792 File Offset: 0x00003992
		internal virtual ColumnDefinition ColumnVersion2 { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0000579B File Offset: 0x0000399B
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x000057A3 File Offset: 0x000039A3
		internal virtual ColumnDefinition ColumnVersion3 { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x000057AC File Offset: 0x000039AC
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x000057B4 File Offset: 0x000039B4
		internal virtual ColumnDefinition ColumnTime1 { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x000057BD File Offset: 0x000039BD
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x000057C5 File Offset: 0x000039C5
		internal virtual ColumnDefinition ColumnTime2 { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x000057CE File Offset: 0x000039CE
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x000057D6 File Offset: 0x000039D6
		internal virtual ColumnDefinition ColumnTime3 { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x000057DF File Offset: 0x000039DF
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x000057E7 File Offset: 0x000039E7
		internal virtual Path PathVersion { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x000057F0 File Offset: 0x000039F0
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x000057F8 File Offset: 0x000039F8
		internal virtual TextBlock LabVersion { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00005801 File Offset: 0x00003A01
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x00005809 File Offset: 0x00003A09
		internal virtual Path PathDownload { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00005812 File Offset: 0x00003A12
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x0000581A File Offset: 0x00003A1A
		internal virtual TextBlock LabDownload { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00005823 File Offset: 0x00003A23
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x0000582B File Offset: 0x00003A2B
		internal virtual Path PathTime { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00005834 File Offset: 0x00003A34
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x0000583C File Offset: 0x00003A3C
		internal virtual TextBlock LabTime { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00005845 File Offset: 0x00003A45
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x0000584D File Offset: 0x00003A4D
		internal virtual Path PathSource { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00005856 File Offset: 0x00003A56
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x0000585E File Offset: 0x00003A5E
		internal virtual TextBlock LabSource { get; set; }

		// Token: 0x060006D0 RID: 1744 RVA: 0x0003E250 File Offset: 0x0003C450
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._CustomerField)
			{
				this._CustomerField = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/modules/minecraft/mycompitem.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0003E280 File Offset: 0x0003C480
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (MyCompItem)target;
				return;
			}
			if (connectionId == 2)
			{
				this.PathLogo = (MyImage)target;
				return;
			}
			if (connectionId == 3)
			{
				this.LabTitle = (TextBlock)target;
				return;
			}
			if (connectionId == 4)
			{
				this.LabTitleRaw = (TextBlock)target;
				return;
			}
			if (connectionId == 5)
			{
				this.PanTags = (StackPanel)target;
				return;
			}
			if (connectionId == 6)
			{
				this.LabInfo = (TextBlock)target;
				return;
			}
			if (connectionId == 7)
			{
				this.ToolTipInfo = (ToolTip)target;
				return;
			}
			if (connectionId == 8)
			{
				this.ColumnVersion1 = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 9)
			{
				this.ColumnVersion2 = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 10)
			{
				this.ColumnVersion3 = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 11)
			{
				this.ColumnTime1 = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 12)
			{
				this.ColumnTime2 = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 13)
			{
				this.ColumnTime3 = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 14)
			{
				this.PathVersion = (Path)target;
				return;
			}
			if (connectionId == 15)
			{
				this.LabVersion = (TextBlock)target;
				return;
			}
			if (connectionId == 16)
			{
				this.PathDownload = (Path)target;
				return;
			}
			if (connectionId == 17)
			{
				this.LabDownload = (TextBlock)target;
				return;
			}
			if (connectionId == 18)
			{
				this.PathTime = (Path)target;
				return;
			}
			if (connectionId == 19)
			{
				this.LabTime = (TextBlock)target;
				return;
			}
			if (connectionId == 20)
			{
				this.PathSource = (Path)target;
				return;
			}
			if (connectionId == 21)
			{
				this.LabSource = (TextBlock)target;
				return;
			}
			this._CustomerField = true;
		}

		// Token: 0x040003D3 RID: 979
		public int m_AttrField;

		// Token: 0x040003D4 RID: 980
		[CompilerGenerated]
		private MyCompItem.ClickEventHandler _CandidateField;

		// Token: 0x040003D5 RID: 981
		private bool _AdvisorField;

		// Token: 0x040003D6 RID: 982
		private Border _AccountField;

		// Token: 0x040003D7 RID: 983
		private string queueField;

		// Token: 0x040003D8 RID: 984
		public bool _EventField;

		// Token: 0x040003D9 RID: 985
		[CompilerGenerated]
		[AccessedThroughProperty("PanBack")]
		private MyCompItem managerField;

		// Token: 0x040003DA RID: 986
		[CompilerGenerated]
		[AccessedThroughProperty("PathLogo")]
		private MyImage m_ModelField;

		// Token: 0x040003DB RID: 987
		[AccessedThroughProperty("LabTitle")]
		[CompilerGenerated]
		private TextBlock wrapperField;

		// Token: 0x040003DC RID: 988
		[CompilerGenerated]
		[AccessedThroughProperty("LabTitleRaw")]
		private TextBlock _BaseField;

		// Token: 0x040003DD RID: 989
		[CompilerGenerated]
		[AccessedThroughProperty("PanTags")]
		private StackPanel m_AttributeField;

		// Token: 0x040003DE RID: 990
		[AccessedThroughProperty("LabInfo")]
		[CompilerGenerated]
		private TextBlock _CodeField;

		// Token: 0x040003DF RID: 991
		[AccessedThroughProperty("ToolTipInfo")]
		[CompilerGenerated]
		private ToolTip _PrototypeField;

		// Token: 0x040003E0 RID: 992
		[AccessedThroughProperty("ColumnVersion1")]
		[CompilerGenerated]
		private ColumnDefinition annotationField;

		// Token: 0x040003E1 RID: 993
		[AccessedThroughProperty("ColumnVersion2")]
		[CompilerGenerated]
		private ColumnDefinition m_InfoField;

		// Token: 0x040003E2 RID: 994
		[AccessedThroughProperty("ColumnVersion3")]
		[CompilerGenerated]
		private ColumnDefinition adapterField;

		// Token: 0x040003E3 RID: 995
		[CompilerGenerated]
		[AccessedThroughProperty("ColumnTime1")]
		private ColumnDefinition facadeField;

		// Token: 0x040003E4 RID: 996
		[AccessedThroughProperty("ColumnTime2")]
		[CompilerGenerated]
		private ColumnDefinition _MerchantField;

		// Token: 0x040003E5 RID: 997
		[CompilerGenerated]
		[AccessedThroughProperty("ColumnTime3")]
		private ColumnDefinition _AuthenticationField;

		// Token: 0x040003E6 RID: 998
		[CompilerGenerated]
		[AccessedThroughProperty("PathVersion")]
		private Path _AlgoField;

		// Token: 0x040003E7 RID: 999
		[CompilerGenerated]
		[AccessedThroughProperty("LabVersion")]
		private TextBlock comparatorField;

		// Token: 0x040003E8 RID: 1000
		[AccessedThroughProperty("PathDownload")]
		[CompilerGenerated]
		private Path _MappingField;

		// Token: 0x040003E9 RID: 1001
		[AccessedThroughProperty("LabDownload")]
		[CompilerGenerated]
		private TextBlock tokenizerField;

		// Token: 0x040003EA RID: 1002
		[CompilerGenerated]
		[AccessedThroughProperty("PathTime")]
		private Path _FilterField;

		// Token: 0x040003EB RID: 1003
		[AccessedThroughProperty("LabTime")]
		[CompilerGenerated]
		private TextBlock databaseField;

		// Token: 0x040003EC RID: 1004
		[CompilerGenerated]
		[AccessedThroughProperty("PathSource")]
		private Path _PredicateField;

		// Token: 0x040003ED RID: 1005
		[CompilerGenerated]
		[AccessedThroughProperty("LabSource")]
		private TextBlock m_PoolField;

		// Token: 0x040003EE RID: 1006
		private bool _CustomerField;

		// Token: 0x020000DF RID: 223
		// (Invoke) Token: 0x060006D8 RID: 1752
		public delegate void ClickEventHandler(object sender, MouseButtonEventArgs e);
	}
}
