using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace PCL
{
	// Token: 0x0200001C RID: 28
	public class MyCard : Grid
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00002798 File Offset: 0x00000998
		public UIElement GetParser()
		{
			return this.dic.Child;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000027A5 File Offset: 0x000009A5
		public void FindParser(UIElement value)
		{
			this.dic.Child = value;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000027B3 File Offset: 0x000009B3
		public TextBlock RestartParser()
		{
			this.Init();
			return this._Helper;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000027C1 File Offset: 0x000009C1
		public void DisableParser(TextBlock value)
		{
			this._Helper = value;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000027CA File Offset: 0x000009CA
		public Path SearchParser()
		{
			this.Init();
			return this.m_Issuer;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000027D8 File Offset: 0x000009D8
		public void AddParser(Path value)
		{
			this.m_Issuer = value;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000027E1 File Offset: 0x000009E1
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000027F3 File Offset: 0x000009F3
		public string Title
		{
			get
			{
				return Conversions.ToString(base.GetValue(MyCard.interpreter));
			}
			set
			{
				base.SetValue(MyCard.interpreter, value);
				if (this._Helper != null)
				{
					this.RestartParser().Text = value;
				}
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000EB74 File Offset: 0x0000CD74
		public MyCard()
		{
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.Init();
			};
			base.MouseEnter += this.MyCard_MouseEnter;
			base.MouseLeave += this.MyCard_MouseLeave;
			base.SizeChanged += this.MySizeChanged;
			base.MouseLeftButtonDown += this.MyCard_MouseLeftButtonDown;
			base.MouseLeftButtonUp += this.MyCard_MouseLeftButtonUp;
			base.MouseLeave += this.MyCard_MouseLeave_Swap;
			this.indexer = ModBase.GetUuid();
			this.serializer = false;
			this.HasMouseAnimation = true;
			this.UseAnimation = true;
			this._Tag = false;
			this.CanSwap = false;
			this.decorator = false;
			this.SwapLogoRight = false;
			this.callback = false;
			this.request = new SystemDropShadowChrome
			{
				Margin = new Thickness(-9.5, -9.0, 0.5, -0.5),
				Opacity = 0.1,
				CornerRadius = new CornerRadius(6.0)
			};
			this.request.SetResourceReference(SystemDropShadowChrome._ObserverTests, "ColorObject1");
			base.Children.Insert(0, this.request);
			this.dic = new Border
			{
				Background = new SolidColorBrush(Color.FromArgb(205, byte.MaxValue, byte.MaxValue, byte.MaxValue)),
				CornerRadius = new CornerRadius(6.0),
				IsHitTestVisible = false
			};
			base.Children.Insert(1, this.dic);
			this.mock = new Grid();
			base.Children.Add(this.mock);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000ED48 File Offset: 0x0000CF48
		private void Init()
		{
			if (!this.serializer)
			{
				this.serializer = true;
				if (Operators.CompareString(this.Title, "", false) != 0 && this.RestartParser() == null)
				{
					this.DisableParser(new TextBlock
					{
						HorizontalAlignment = HorizontalAlignment.Left,
						VerticalAlignment = VerticalAlignment.Top,
						Margin = new Thickness(15.0, 12.0, 0.0, 0.0),
						FontWeight = FontWeights.Bold,
						FontSize = 13.0,
						IsHitTestVisible = false
					});
					this.RestartParser().SetResourceReference(TextBlock.ForegroundProperty, "ColorBrush1");
					this.RestartParser().SetBinding(TextBlock.TextProperty, new Binding("Title")
					{
						Source = this,
						Mode = BindingMode.OneWay
					});
					this.mock.Children.Add(this.RestartParser());
				}
				if (this.CanSwap || this._Stub != null)
				{
					if (this._Stub == null && base.Children.Count > 3)
					{
						this._Stub = base.Children[3];
					}
					this.AddParser(new Path
					{
						HorizontalAlignment = HorizontalAlignment.Right,
						Stretch = Stretch.Uniform,
						Height = 6.0,
						Width = 10.0,
						VerticalAlignment = VerticalAlignment.Top,
						Margin = new Thickness(0.0, 17.0, 16.0, 0.0),
						Data = (Geometry)new GeometryConverter().ConvertFromString("M2,4 l-2,2 10,10 10,-10 -2,-2 -8,8 -8,-8 z"),
						RenderTransform = new RotateTransform(180.0),
						RenderTransformOrigin = new Point(0.5, 0.5)
					});
					this.SearchParser().SetResourceReference(Shape.FillProperty, "ColorBrush1");
					this.mock.Children.Add(this.SearchParser());
				}
				if (this.IsSwaped && this._Stub != null)
				{
					this.SearchParser().RenderTransform = new RotateTransform((double)(this.SwapLogoRight ? 270 : 0));
					bool RawUseAnimation = this.UseAnimation;
					this.UseAnimation = false;
					base.Height = 40.0;
					ModAnimation.AniStop("MyCard Height " + Conversions.ToString(this.indexer));
					this._Tag = false;
					ModBase.RunInUi(delegate()
					{
						this.UseAnimation = RawUseAnimation;
					}, true);
				}
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000EFFC File Offset: 0x0000D1FC
		public void StackInstall()
		{
			StackPanel stackPanel = (StackPanel)this._Stub;
			MyCard.StackInstall(ref stackPanel, this.CountParser(), this.Title);
			this._Stub = stackPanel;
			this.TriggerForceResize();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000F034 File Offset: 0x0000D234
		public static void StackInstall(ref StackPanel Stack, int Type, string CardTitle = "")
		{
			if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(Stack.Tag)))
			{
				switch (Type)
				{
				case 3:
					Stack.Tag = ((List<ModDownload.DlOptiFineListEntry>)Stack.Tag).Sort((MyCard._Closure$__.$I24-0 == null) ? (MyCard._Closure$__.$I24-0 = ((ModDownload.DlOptiFineListEntry a, ModDownload.DlOptiFineListEntry b) => ModMinecraft.VersionSortBoolean(a.m_SchemaTest, b.m_SchemaTest))) : MyCard._Closure$__.$I24-0);
					break;
				case 4:
				case 10:
					Stack.Tag = ((List<ModDownload.DlLiteLoaderListEntry>)Stack.Tag).Sort((MyCard._Closure$__.$I24-1 == null) ? (MyCard._Closure$__.$I24-1 = ((ModDownload.DlLiteLoaderListEntry a, ModDownload.DlLiteLoaderListEntry b) => ModMinecraft.VersionSortBoolean(a.Inherit, b.Inherit))) : MyCard._Closure$__.$I24-1);
					break;
				case 6:
					Stack.Tag = ((List<ModDownload.DlForgeVersionEntry>)Stack.Tag).Sort((MyCard._Closure$__.$I24-2 == null) ? (MyCard._Closure$__.$I24-2 = ((ModDownload.DlForgeVersionEntry a, ModDownload.DlForgeVersionEntry b) => a.clientMap > b.clientMap)) : MyCard._Closure$__.$I24-2);
					break;
				case 8:
				case 9:
					Stack.Tag = ((List<ModComp.CompFile>)Stack.Tag).Sort((MyCard._Closure$__.$I24-3 == null) ? (MyCard._Closure$__.$I24-3 = ((ModComp.CompFile a, ModComp.CompFile b) => DateTime.Compare(a._ListenerRepository, b._ListenerRepository) > 0)) : MyCard._Closure$__.$I24-3);
					break;
				}
				switch (Type)
				{
				case 5:
				{
					MyLoading myLoading = new MyLoading
					{
						Text = "正在获取版本列表",
						Margin = new Thickness(5.0)
					};
					ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> loaderTask = new ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>("DlForgeVersion Main", new Action<ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>>(ModDownload.DlForgeVersionMain), null, ThreadPriority.Normal);
					myLoading.State = loaderTask;
					loaderTask.Start(RuntimeHelpers.GetObjectValue(Stack.Tag), false);
					MyLoading myLoading2 = myLoading;
					MyCard._Closure$__R24-1 CS$<>8__locals1 = new MyCard._Closure$__R24-1(CS$<>8__locals1);
					CS$<>8__locals1.$VB$NonLocal_2 = ModMain.m_ItemIterator;
					myLoading2.PrintField(delegate(object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2)
					{
						CS$<>8__locals1.$VB$NonLocal_2.Forge_StateChanged((MyLoading)a0, a1, a2);
					});
					MyLoading myLoading3 = myLoading;
					MyCard._Closure$__R24-2 CS$<>8__locals2 = new MyCard._Closure$__R24-2(CS$<>8__locals2);
					CS$<>8__locals2.$VB$NonLocal_3 = ModMain.m_ItemIterator;
					myLoading3.Click += delegate(object sender, MouseButtonEventArgs e)
					{
						CS$<>8__locals2.$VB$NonLocal_3.Forge_Click((MyLoading)sender, e);
					};
					Stack.Children.Add(myLoading);
					break;
				}
				case 6:
					ModDownloadLib.ForgeDownloadListItemPreload(Stack, (List<ModDownload.DlForgeVersionEntry>)Stack.Tag, new MyListItem.ClickEventHandler(ModDownloadLib.ForgeSave_Click), true);
					break;
				case 8:
					ModComp.CompFilesCardPreload(Stack, (List<ModComp.CompFile>)Stack.Tag);
					break;
				}
				try
				{
					foreach (object obj in ((IEnumerable)Stack.Tag))
					{
						object objectValue = RuntimeHelpers.GetObjectValue(obj);
						switch (Type)
						{
						case 0:
							Stack.Children.Add(PageSelectRight.McVersionListItem((ModMinecraft.McVersion)objectValue));
							continue;
						case 2:
							Stack.Children.Add(ModDownloadLib.McDownloadListItem((JObject)objectValue, new MyListItem.ClickEventHandler(ModDownloadLib.McDownloadMenuSave), true));
							continue;
						case 3:
							Stack.Children.Add(ModDownloadLib.OptiFineDownloadListItem((ModDownload.DlOptiFineListEntry)objectValue, new MyListItem.ClickEventHandler(ModDownloadLib.OptiFineSave_Click), true));
							continue;
						case 4:
						{
							UIElementCollection children = Stack.Children;
							ModDownload.DlLiteLoaderListEntry entry = (ModDownload.DlLiteLoaderListEntry)objectValue;
							MyCard._Closure$__R24-3 CS$<>8__locals3 = new MyCard._Closure$__R24-3(CS$<>8__locals3);
							CS$<>8__locals3.$VB$NonLocal_4 = ModMain.bridgeIterator;
							children.Add(ModDownloadLib.LiteLoaderDownloadListItem(entry, delegate(object sender, MouseButtonEventArgs e)
							{
								CS$<>8__locals3.$VB$NonLocal_4.DownloadStart((MyListItem)sender, e);
							}, false));
							continue;
						}
						case 5:
							continue;
						case 6:
							Stack.Children.Add(ModDownloadLib.ForgeDownloadListItem((ModDownload.DlForgeVersionEntry)objectValue, new MyListItem.ClickEventHandler(ModDownloadLib.ForgeSave_Click), true));
							continue;
						case 7:
							Stack.Children.Add(ModDownloadLib.McDownloadListItem((JObject)objectValue, (MyCard._Closure$__.$I24-4 == null) ? (MyCard._Closure$__.$I24-4 = delegate(object sender, MouseButtonEventArgs e)
							{
								ModMain.m_VisitorIterator.MinecraftSelected((MyListItem)sender, e);
							}) : MyCard._Closure$__.$I24-4, false));
							continue;
						case 8:
							if (((List<ModComp.CompFile>)Stack.Tag).Distinct((MyCard._Closure$__.$I24-5 == null) ? (MyCard._Closure$__.$I24-5 = ((ModComp.CompFile a, ModComp.CompFile b) => Operators.CompareString(a._ProductRepository, b._ProductRepository, false) == 0)) : MyCard._Closure$__.$I24-5).Count != ((List<ModComp.CompFile>)Stack.Tag).Count)
							{
								Stack.Children.Add(((ModComp.CompFile)objectValue).ToListItem(new MyListItem.ClickEventHandler(ModMain.iteratorRepository.Save_Click), null, true));
								continue;
							}
							Stack.Children.Add(((ModComp.CompFile)objectValue).ToListItem(new MyListItem.ClickEventHandler(ModMain.iteratorRepository.Save_Click), null, false));
							continue;
						case 9:
						{
							if (((List<ModComp.CompFile>)Stack.Tag).Distinct((MyCard._Closure$__.$I24-6 == null) ? (MyCard._Closure$__.$I24-6 = ((ModComp.CompFile a, ModComp.CompFile b) => Operators.CompareString(a._ProductRepository, b._ProductRepository, false) == 0)) : MyCard._Closure$__.$I24-6).Count != ((List<ModComp.CompFile>)Stack.Tag).Count)
							{
								UIElementCollection children2 = Stack.Children;
								ModComp.CompFile compFile = (ModComp.CompFile)objectValue;
								MyCard._Closure$__R24-4 CS$<>8__locals4 = new MyCard._Closure$__R24-4(CS$<>8__locals4);
								CS$<>8__locals4.$VB$NonLocal_5 = ModMain.iteratorRepository;
								children2.Add(compFile.ToListItem(delegate(object sender, MouseButtonEventArgs e)
								{
									CS$<>8__locals4.$VB$NonLocal_5.Install_Click((MyListItem)sender, e);
								}, new MyIconButton.ClickEventHandler(ModMain.iteratorRepository.Save_Click), true));
								continue;
							}
							UIElementCollection children3 = Stack.Children;
							ModComp.CompFile compFile2 = (ModComp.CompFile)objectValue;
							MyCard._Closure$__R24-5 CS$<>8__locals5 = new MyCard._Closure$__R24-5(CS$<>8__locals5);
							CS$<>8__locals5.$VB$NonLocal_6 = ModMain.iteratorRepository;
							children3.Add(compFile2.ToListItem(delegate(object sender, MouseButtonEventArgs e)
							{
								CS$<>8__locals5.$VB$NonLocal_6.Install_Click((MyListItem)sender, e);
							}, new MyIconButton.ClickEventHandler(ModMain.iteratorRepository.Save_Click), false));
							continue;
						}
						case 10:
							Stack.Children.Add(ModDownloadLib.LiteLoaderDownloadListItem((ModDownload.DlLiteLoaderListEntry)objectValue, new MyListItem.ClickEventHandler(ModDownloadLib.LiteLoaderSave_Click), true));
							continue;
						case 11:
							Stack.Children.Add(((ModMain.HelpEntry)objectValue).ToListItem());
							continue;
						case 12:
						{
							UIElementCollection children4 = Stack.Children;
							JObject entry2 = (JObject)objectValue;
							MyCard._Closure$__R24-6 CS$<>8__locals6 = new MyCard._Closure$__R24-6(CS$<>8__locals6);
							CS$<>8__locals6.$VB$NonLocal_7 = ModMain.m_VisitorIterator;
							children4.Add(ModDownloadLib.FabricDownloadListItem(entry2, delegate(object sender, MouseButtonEventArgs e)
							{
								CS$<>8__locals6.$VB$NonLocal_7.Fabric_Selected((MyListItem)sender, e);
							}));
							continue;
						}
						case 13:
							Stack.Children.Add(ModDownloadLib.NeoForgeDownloadListItem((ModDownload.DlNeoForgeListEntry)objectValue, new MyListItem.ClickEventHandler(ModDownloadLib.NeoForgeSave_Click), true));
							continue;
						}
						ModBase.Log("未知的虚拟化种类：" + Conversions.ToString(Type), ModBase.LogLevel.Feedback, "出现错误");
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
				Stack.Children.Add(new FrameworkElement
				{
					Height = 18.0
				});
				Stack.Tag = null;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00002815 File Offset: 0x00000A15
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x0000281D File Offset: 0x00000A1D
		public bool HasMouseAnimation { get; set; }

		// Token: 0x060000B2 RID: 178 RVA: 0x0000F6E8 File Offset: 0x0000D8E8
		private void MyCard_MouseEnter(object sender, MouseEventArgs e)
		{
			if (this.HasMouseAnimation)
			{
				List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
				if (!Information.IsNothing(this.RestartParser()))
				{
					list.Add(ModAnimation.AaColor(this.RestartParser(), TextBlock.ForegroundProperty, "ColorBrush2", 150, 0, null, false));
				}
				if (!Information.IsNothing(this.SearchParser()))
				{
					list.Add(ModAnimation.AaColor(this.SearchParser(), Shape.FillProperty, "ColorBrush2", 150, 0, null, false));
				}
				list.AddRange(new ModAnimation.AniData[]
				{
					ModAnimation.AaColor(this.request, SystemDropShadowChrome._ObserverTests, "ColorObject2", 180, 0, null, false),
					ModAnimation.AaColor(this.dic, Border.BackgroundProperty, new ModBase.MyColor(230.0, 255.0, 255.0, 255.0) - this.dic.Background, 180, 0, null, false),
					ModAnimation.AaOpacity(this.request, 0.3 - this.request.Opacity, 180, 0, null, false)
				});
				ModAnimation.AniStart(list, "MyCard Mouse " + Conversions.ToString(this.indexer), false);
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000F840 File Offset: 0x0000DA40
		private void MyCard_MouseLeave(object sender, MouseEventArgs e)
		{
			if (this.HasMouseAnimation)
			{
				List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
				if (!Information.IsNothing(this.RestartParser()))
				{
					list.Add(ModAnimation.AaColor(this.RestartParser(), TextBlock.ForegroundProperty, "ColorBrush1", 250, 0, null, false));
				}
				if (!Information.IsNothing(this.SearchParser()))
				{
					list.Add(ModAnimation.AaColor(this.SearchParser(), Shape.FillProperty, "ColorBrush1", 250, 0, null, false));
				}
				list.AddRange(new ModAnimation.AniData[]
				{
					ModAnimation.AaColor(this.request, SystemDropShadowChrome._ObserverTests, "ColorObject1", 300, 0, null, false),
					ModAnimation.AaColor(this.dic, Border.BackgroundProperty, new ModBase.MyColor(205.0, 255.0, 255.0, 255.0) - this.dic.Background, 300, 0, null, false),
					ModAnimation.AaOpacity(this.request, 0.1 - this.request.Opacity, 300, 0, null, false)
				});
				ModAnimation.AniStart(list, "MyCard Mouse " + Conversions.ToString(this.indexer), false);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002826 File Offset: 0x00000A26
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000282E File Offset: 0x00000A2E
		public bool UseAnimation { get; set; }

		// Token: 0x060000B6 RID: 182 RVA: 0x0000F998 File Offset: 0x0000DB98
		private void MySizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (this.UseAnimation)
			{
				double num = (this.IsSwaped ? 40.0 : e.NewSize.Height) - e.PreviousSize.Height;
				if (e.PreviousSize.Height != 0.0 && !this._Tag && Math.Abs(num) >= 1.0 && base.ActualHeight != 0.0)
				{
					this.StartHeightAnimation(num, e.PreviousSize.Height, false);
				}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000FA3C File Offset: 0x0000DC3C
		private void StartHeightAnimation(double DeltaHeight, double PreviousHeight, bool IsLoadAnimation)
		{
			if (!this._Tag && ModMain._ProcessIterator != null)
			{
				List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
				if (DeltaHeight <= 10.0 && (DeltaHeight >= -10.0 || Information.IsNothing(RuntimeHelpers.GetObjectValue(this._Stub))))
				{
					list.AddRange(new ModAnimation.AniData[]
					{
						ModAnimation.AaHeight(this, DeltaHeight, checked((int)Math.Round(ModBase.MathClamp(unchecked(Math.Abs(DeltaHeight) * 4.0), 150.0, 250.0))), 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false)
					});
				}
				else
				{
					double num = ModBase.MathClamp(Math.Abs(DeltaHeight) * 0.05, 3.0, 10.0) * (double)Math.Sign(DeltaHeight);
					list.AddRange(new ModAnimation.AniData[]
					{
						ModAnimation.AaHeight(this, DeltaHeight + num, 300, IsLoadAnimation ? 30 : 0, (ModAnimation.AniEase)((DeltaHeight > ModMain._ProcessIterator.Height) ? new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.ExtraStrong) : new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.ExtraStrong)), false),
						ModAnimation.AaHeight(this, -num, 150, 260, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), false)
					});
				}
				list.Add(ModAnimation.AaCode(delegate
				{
					this._Tag = false;
					base.Height = this._Observer;
					if (this.IsSwaped)
					{
						NewLateBinding.LateSet(this._Stub, null, "Visibility", new object[]
						{
							Visibility.Collapsed
						}, null, null);
					}
				}, 0, true));
				ModAnimation.AniStart(list, "MyCard Height " + Conversions.ToString(this.indexer), false);
				this._Tag = true;
				this._Observer = (this.IsSwaped ? 40.0 : base.Height);
				base.Height = PreviousHeight;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000FBE0 File Offset: 0x0000DDE0
		public void TriggerForceResize()
		{
			base.Height = (this.IsSwaped ? 40.0 : double.NaN);
			ModAnimation.AniStop("MyCard Height " + Conversions.ToString(this.indexer));
			this._Tag = false;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002837 File Offset: 0x00000A37
		// (set) Token: 0x060000BA RID: 186 RVA: 0x0000283F File Offset: 0x00000A3F
		public bool CanSwap { get; set; }

		// Token: 0x060000BB RID: 187 RVA: 0x00002848 File Offset: 0x00000A48
		[CompilerGenerated]
		public int CountParser()
		{
			return this.@ref;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00002850 File Offset: 0x00000A50
		[CompilerGenerated]
		public void CreateParser(int AutoPropertyValue)
		{
			this.@ref = AutoPropertyValue;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00002859 File Offset: 0x00000A59
		// (set) Token: 0x060000BE RID: 190 RVA: 0x0000FC30 File Offset: 0x0000DE30
		public bool IsSwaped
		{
			get
			{
				return this.decorator;
			}
			set
			{
				if (this.decorator != value)
				{
					this.decorator = value;
					if (this._Stub != null)
					{
						if (!this.IsSwaped && this._Stub is StackPanel)
						{
							StackPanel stackPanel = (StackPanel)this._Stub;
							MyCard.StackInstall(ref stackPanel, this.CountParser(), this.Title);
							this._Stub = stackPanel;
						}
						if (base.IsLoaded)
						{
							NewLateBinding.LateSet(this._Stub, null, "Visibility", new object[]
							{
								Visibility.Visible
							}, null, null);
							this.TriggerForceResize();
							ModAnimation.AniStart(ModAnimation.AaRotateTransform(this.SearchParser(), (double)(this.decorator ? (this.SwapLogoRight ? 270 : 0) : 180) - ((RotateTransform)this.SearchParser().RenderTransform).Angle, 400, 0, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false), "MyCard Swap " + Conversions.ToString(this.indexer), true);
						}
					}
				}
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00002861 File Offset: 0x00000A61
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00002869 File Offset: 0x00000A69
		public bool SwapLogoRight { get; set; }

		// Token: 0x060000C1 RID: 193 RVA: 0x0000FD30 File Offset: 0x0000DF30
		[CompilerGenerated]
		public void InterruptParser(MyCard.PreviewSwapEventHandler obj)
		{
			MyCard.PreviewSwapEventHandler previewSwapEventHandler = this.template;
			MyCard.PreviewSwapEventHandler previewSwapEventHandler2;
			do
			{
				previewSwapEventHandler2 = previewSwapEventHandler;
				MyCard.PreviewSwapEventHandler value = (MyCard.PreviewSwapEventHandler)Delegate.Combine(previewSwapEventHandler2, obj);
				previewSwapEventHandler = Interlocked.CompareExchange<MyCard.PreviewSwapEventHandler>(ref this.template, value, previewSwapEventHandler2);
			}
			while (previewSwapEventHandler != previewSwapEventHandler2);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000FD68 File Offset: 0x0000DF68
		[CompilerGenerated]
		public void PostParser(MyCard.PreviewSwapEventHandler obj)
		{
			MyCard.PreviewSwapEventHandler previewSwapEventHandler = this.template;
			MyCard.PreviewSwapEventHandler previewSwapEventHandler2;
			do
			{
				previewSwapEventHandler2 = previewSwapEventHandler;
				MyCard.PreviewSwapEventHandler value = (MyCard.PreviewSwapEventHandler)Delegate.Remove(previewSwapEventHandler2, obj);
				previewSwapEventHandler = Interlocked.CompareExchange<MyCard.PreviewSwapEventHandler>(ref this.template, value, previewSwapEventHandler2);
			}
			while (previewSwapEventHandler != previewSwapEventHandler2);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000FDA0 File Offset: 0x0000DFA0
		[CompilerGenerated]
		public void CompareParser(MyCard.SwapEventHandler obj)
		{
			MyCard.SwapEventHandler swapEventHandler = this.method;
			MyCard.SwapEventHandler swapEventHandler2;
			do
			{
				swapEventHandler2 = swapEventHandler;
				MyCard.SwapEventHandler value = (MyCard.SwapEventHandler)Delegate.Combine(swapEventHandler2, obj);
				swapEventHandler = Interlocked.CompareExchange<MyCard.SwapEventHandler>(ref this.method, value, swapEventHandler2);
			}
			while (swapEventHandler != swapEventHandler2);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000FDD8 File Offset: 0x0000DFD8
		[CompilerGenerated]
		public void MoveParser(MyCard.SwapEventHandler obj)
		{
			MyCard.SwapEventHandler swapEventHandler = this.method;
			MyCard.SwapEventHandler swapEventHandler2;
			do
			{
				swapEventHandler2 = swapEventHandler;
				MyCard.SwapEventHandler value = (MyCard.SwapEventHandler)Delegate.Remove(swapEventHandler2, obj);
				swapEventHandler = Interlocked.CompareExchange<MyCard.SwapEventHandler>(ref this.method, value, swapEventHandler2);
			}
			while (swapEventHandler != swapEventHandler2);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000FE10 File Offset: 0x0000E010
		private void MyCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			double y = Mouse.GetPosition(this).Y;
			if (this.IsSwaped || (this._Stub != null && y <= 40.0 && (y != 0.0 || base.IsMouseDirectlyOver)))
			{
				this.callback = true;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000FE64 File Offset: 0x0000E064
		private void MyCard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (this.callback)
			{
				this.callback = false;
				double y = Mouse.GetPosition(this).Y;
				if (this.IsSwaped || (this._Stub != null && y <= 40.0 && (y != 0.0 || base.IsMouseDirectlyOver)))
				{
					ModBase.RouteEventArgs routeEventArgs = new ModBase.RouteEventArgs(true);
					MyCard.PreviewSwapEventHandler previewSwapEventHandler = this.template;
					if (previewSwapEventHandler != null)
					{
						previewSwapEventHandler(this, routeEventArgs);
					}
					if (routeEventArgs.m_SerializerError)
					{
						this.callback = false;
						return;
					}
					this.IsSwaped = !this.IsSwaped;
					ModBase.Log("[Control] " + (this.IsSwaped ? "折叠卡片" : "展开卡片") + ((this.Title == null) ? "" : ("：" + this.Title)), ModBase.LogLevel.Normal, "出现错误");
					MyCard.SwapEventHandler swapEventHandler = this.method;
					if (swapEventHandler != null)
					{
						swapEventHandler(this, routeEventArgs);
					}
				}
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00002872 File Offset: 0x00000A72
		private void MyCard_MouseLeave_Swap(object sender, MouseEventArgs e)
		{
			this.callback = false;
		}

		// Token: 0x04000015 RID: 21
		private readonly Grid mock;

		// Token: 0x04000016 RID: 22
		private readonly SystemDropShadowChrome request;

		// Token: 0x04000017 RID: 23
		private readonly Border dic;

		// Token: 0x04000018 RID: 24
		private TextBlock _Helper;

		// Token: 0x04000019 RID: 25
		private Path m_Issuer;

		// Token: 0x0400001A RID: 26
		public int indexer;

		// Token: 0x0400001B RID: 27
		public static readonly DependencyProperty interpreter = DependencyProperty.Register("Title", typeof(string), typeof(MyCard), new PropertyMetadata(""));

		// Token: 0x0400001C RID: 28
		private bool serializer;

		// Token: 0x0400001D RID: 29
		[CompilerGenerated]
		private bool watcher;

		// Token: 0x0400001E RID: 30
		[CompilerGenerated]
		private bool system;

		// Token: 0x0400001F RID: 31
		private bool _Tag;

		// Token: 0x04000020 RID: 32
		private double _Observer;

		// Token: 0x04000021 RID: 33
		public object _Stub;

		// Token: 0x04000022 RID: 34
		[CompilerGenerated]
		private bool m_Rules;

		// Token: 0x04000023 RID: 35
		[CompilerGenerated]
		private int @ref;

		// Token: 0x04000024 RID: 36
		private bool decorator;

		// Token: 0x04000025 RID: 37
		[CompilerGenerated]
		private bool instance;

		// Token: 0x04000026 RID: 38
		private bool callback;

		// Token: 0x04000027 RID: 39
		[CompilerGenerated]
		private MyCard.PreviewSwapEventHandler template;

		// Token: 0x04000028 RID: 40
		[CompilerGenerated]
		private MyCard.SwapEventHandler method;

		// Token: 0x0200001D RID: 29
		// (Invoke) Token: 0x060000CD RID: 205
		public delegate void PreviewSwapEventHandler(object sender, ModBase.RouteEventArgs e);

		// Token: 0x0200001E RID: 30
		// (Invoke) Token: 0x060000D2 RID: 210
		public delegate void SwapEventHandler(object sender, ModBase.RouteEventArgs e);
	}
}
