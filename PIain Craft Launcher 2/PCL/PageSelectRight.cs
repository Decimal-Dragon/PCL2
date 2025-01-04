using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;
using PCL.My;

namespace PCL
{
	// Token: 0x0200018C RID: 396
	[DesignerGenerated]
	public class PageSelectRight : MyPageRight, IComponentConnector
	{
		// Token: 0x06000FEA RID: 4074 RVA: 0x00009BE2 File Offset: 0x00007DE2
		public PageSelectRight()
		{
			base.Loaded += this.PageSelectRight_Loaded;
			base.Initialized += delegate(object sender, EventArgs e)
			{
				this.LoaderInit();
			};
			this._StatusMapper = false;
			this.InitializeComponent();
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00009C1C File Offset: 0x00007E1C
		private void PageSelectRight_Loaded(object sender, RoutedEventArgs e)
		{
			ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.RunOnUpdated, 1, "versions\\", false);
			this.PanBack.ScrollToHome();
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00074CE8 File Offset: 0x00072EE8
		private void LoaderInit()
		{
			base.PageLoaderInit(this.Load, this.PanLoad, this.PanAllBack, null, ModMinecraft._ListenerTests, delegate(ModLoader.LoaderBase a0)
			{
				this.McVersionListUI((ModLoader.LoaderTask<string, int>)a0);
			}, null, false);
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00009C41 File Offset: 0x00007E41
		private void Load_Click(object sender, MouseButtonEventArgs e)
		{
			if (ModMinecraft._ListenerTests.State == ModBase.LoadState.Failed)
			{
				ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", false);
			}
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00074D24 File Offset: 0x00072F24
		private void McVersionListUI(ModLoader.LoaderTask<string, int> Loader)
		{
			try
			{
				this.PanMain.Children.Clear();
				foreach (KeyValuePair<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>> keyValuePair in Enumerable.ToArray<KeyValuePair<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>>>(ModMinecraft._RegTests))
				{
					if (!(keyValuePair.Key == ModMinecraft.McVersionCardType.Hidden ^ this._StatusMapper))
					{
						string text;
						switch (keyValuePair.Key)
						{
						case ModMinecraft.McVersionCardType.Star:
							text = "收藏夹";
							break;
						case ModMinecraft.McVersionCardType.Auto:
							goto IL_3FC;
						case ModMinecraft.McVersionCardType.Hidden:
							text = "隐藏的版本";
							break;
						case ModMinecraft.McVersionCardType.API:
						{
							bool flag = false;
							bool flag2 = false;
							bool flag3 = false;
							bool flag4 = false;
							try
							{
								foreach (ModMinecraft.McVersion mcVersion in keyValuePair.Value)
								{
									if (mcVersion.Version._CandidateMap)
									{
										flag3 = true;
									}
									if (mcVersion.Version._AccountMap)
									{
										flag4 = true;
									}
									if (mcVersion.Version.m_StructMap)
									{
										flag = true;
									}
									if (mcVersion.Version._ValMap)
									{
										flag2 = true;
									}
								}
							}
							finally
							{
								List<ModMinecraft.McVersion>.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
							if (checked((flag4 > false) + (flag > false) + (flag3 > false) + (flag2 > false)) > true)
							{
								text = "可安装 Mod";
							}
							else if (flag)
							{
								text = "Forge 版本";
							}
							else if (flag2)
							{
								text = "NeoForge 版本";
							}
							else if (flag4)
							{
								text = "LiteLoader 版本";
							}
							else
							{
								text = "Fabric 版本";
							}
							break;
						}
						case ModMinecraft.McVersionCardType.OriginalLike:
							text = "常规版本";
							break;
						case ModMinecraft.McVersionCardType.Rubbish:
							text = "不常用版本";
							break;
						case ModMinecraft.McVersionCardType.Fool:
							text = "愚人节版本";
							break;
						case ModMinecraft.McVersionCardType.Error:
							text = "错误的版本";
							break;
						default:
							goto IL_3FC;
						}
						string text2 = text + ((Operators.CompareString(text, "收藏夹", false) == 0) ? "" : (" (" + Conversions.ToString(keyValuePair.Value.Count) + ")"));
						MyCard myCard = new MyCard();
						myCard.Title = text2;
						myCard.Margin = new Thickness(0.0, 0.0, 0.0, 15.0);
						myCard.CreateParser(0);
						MyCard myCard2 = myCard;
						StackPanel stackPanel = new StackPanel
						{
							Margin = new Thickness(20.0, 40.0, 18.0, 0.0),
							VerticalAlignment = VerticalAlignment.Top,
							RenderTransform = new TranslateTransform(0.0, 0.0),
							Tag = keyValuePair.Value
						};
						myCard2.Children.Add(stackPanel);
						myCard2._Stub = stackPanel;
						this.PanMain.Children.Add(myCard2);
						if (keyValuePair.Key != ModMinecraft.McVersionCardType.Rubbish && keyValuePair.Key != ModMinecraft.McVersionCardType.Error)
						{
							if (keyValuePair.Key != ModMinecraft.McVersionCardType.Fool)
							{
								MyCard.StackInstall(ref stackPanel, 0, text2);
								goto IL_2C4;
							}
						}
						myCard2.IsSwaped = true;
						goto IL_2C4;
						IL_3FC:
						throw new ArgumentException("未知的卡片种类（" + Conversions.ToString((int)keyValuePair.Key) + "）");
					}
					IL_2C4:;
				}
				if (this.PanMain.Children.Count == 1 && ((MyCard)this.PanMain.Children[0]).IsSwaped)
				{
					((MyCard)this.PanMain.Children[0]).IsSwaped = false;
				}
				if (this.PanMain.Children.Count == 0)
				{
					this.PanEmpty.Visibility = Visibility.Visible;
					this.PanBack.Visibility = Visibility.Collapsed;
					if (this._StatusMapper)
					{
						this.LabEmptyTitle.Text = "无隐藏版本";
						this.LabEmptyContent.Text = "没有版本被隐藏，你可以在版本设置的版本分类选项中隐藏版本。\r\n再次按下 F11 即可退出隐藏版本查看模式。";
						this.BtnEmptyDownload.Visibility = Visibility.Collapsed;
					}
					else
					{
						this.LabEmptyTitle.Text = "无可用版本";
						this.LabEmptyContent.Text = "未找到任何版本的游戏，请先下载任意版本的游戏。\r\n若有已存在的游戏，请在左边的列表中选择添加文件夹，选择 .minecraft 文件夹将其导入。";
						this.BtnEmptyDownload.Visibility = (Conversions.ToBoolean(Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenPageDownload", null)) && !PageSetupUI.CreateClient()) ? Visibility.Collapsed : Visibility.Visible);
					}
				}
				else
				{
					this.PanBack.Visibility = Visibility.Visible;
					this.PanEmpty.Visibility = Visibility.Collapsed;
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "将版本列表转换显示时失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x000751A8 File Offset: 0x000733A8
		public static MyListItem McVersionListItem(ModMinecraft.McVersion Version)
		{
			MyListItem myListItem = new MyListItem
			{
				Title = Version.Name,
				Info = Version._ConsumerMap,
				Height = 42.0,
				Tag = Version,
				SnapsToDevicePixels = true,
				Type = MyListItem.CheckType.Clickable
			};
			try
			{
				if (Version.getterMap.EndsWith("PCL\\Logo.png"))
				{
					myListItem.Logo = Version.Path + "PCL\\Logo.png";
				}
				else
				{
					myListItem.Logo = Version.getterMap;
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "加载版本图标失败", ModBase.LogLevel.Hint, "出现错误");
				myListItem.Logo = "pack://application:,,,/images/Blocks/RedstoneBlock.png";
			}
			myListItem.tokenComposer = new Action<MyListItem, EventArgs>(PageSelectRight.McVersionListContent);
			return myListItem;
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0007527C File Offset: 0x0007347C
		private static void McVersionListContent(MyListItem sender, EventArgs e)
		{
			ModMinecraft.McVersion Version = (ModMinecraft.McVersion)sender2.Tag;
			sender2.Click += ((PageSelectRight._Closure$__.$IR7-3 == null) ? (PageSelectRight._Closure$__.$IR7-3 = delegate(object sender, MouseButtonEventArgs e)
			{
				PageSelectRight.Item_Click((MyListItem)sender, e);
			}) : PageSelectRight._Closure$__.$IR7-3);
			MyIconButton myIconButton = new MyIconButton();
			if (Version._TokenMap)
			{
				myIconButton.ToolTip = "取消收藏";
				ToolTipService.SetPlacement(myIconButton, PlacementMode.Center);
				ToolTipService.SetVerticalOffset(myIconButton, 30.0);
				ToolTipService.SetHorizontalOffset(myIconButton, 2.0);
				myIconButton.LogoScale = 1.1;
				myIconButton.Logo = "M700.856 155.543c-74.769 0-144.295 72.696-190.046 127.26-45.737-54.576-115.247-127.26-190.056-127.26-134.79 0-244.443 105.78-244.443 235.799 0 77.57 39.278 131.988 70.845 175.713C238.908 694.053 469.62 852.094 479.39 858.757c9.41 6.414 20.424 9.629 31.401 9.629 11.006 0 21.998-3.215 31.398-9.63 9.782-6.662 240.514-164.703 332.238-291.701 31.587-43.724 70.874-98.143 70.874-175.713-0.001-130.02-109.656-235.8-244.445-235.8z m0 0";
			}
			else
			{
				myIconButton.ToolTip = "收藏";
				ToolTipService.SetPlacement(myIconButton, PlacementMode.Center);
				ToolTipService.SetVerticalOffset(myIconButton, 30.0);
				ToolTipService.SetHorizontalOffset(myIconButton, 2.0);
				myIconButton.LogoScale = 1.1;
				myIconButton.Logo = "M512 896a42.666667 42.666667 0 0 1-30.293333-12.373333l-331.52-331.946667a224.426667 224.426667 0 0 1 0-315.733333 223.573333 223.573333 0 0 1 315.733333 0L512 282.026667l46.08-46.08a223.573333 223.573333 0 0 1 315.733333 0 224.426667 224.426667 0 0 1 0 315.733333l-331.52 331.946667A42.666667 42.666667 0 0 1 512 896zM308.053333 256a136.533333 136.533333 0 0 0-97.28 40.106667 138.24 138.24 0 0 0 0 194.986666L512 792.746667l301.226667-301.653334a138.24 138.24 0 0 0 0-194.986666 141.653333 141.653333 0 0 0-194.56 0l-76.373334 76.8a42.666667 42.666667 0 0 1-60.586666 0L405.333333 296.106667A136.533333 136.533333 0 0 0 308.053333 256z";
			}
			myIconButton.Click += delegate(object sender, EventArgs e)
			{
				base._Lambda$__0();
			};
			MyIconButton myIconButton2 = new MyIconButton
			{
				LogoScale = 1.1,
				Logo = "M520.192 0C408.43 0 317.44 82.87 313.563 186.734H52.736c-29.038 0-52.663 21.943-52.663 49.079s23.625 49.152 52.663 49.152h58.075v550.473c0 103.35 75.118 187.757 167.717 187.757h472.43c92.599 0 167.716-83.894 167.716-187.757V285.477h52.59c29.038 0 52.59-21.943 52.663-49.08-0.073-27.135-23.625-49.151-52.663-49.151H726.235C723.237 83.017 631.955 0 520.192 0zM404.846 177.957c3.803-50.03 50.176-89.015 107.447-89.015 57.197 0 103.57 38.985 106.788 89.015H404.92zM284.379 933.669c-33.353 0-69.997-39.351-69.997-95.525v-549.01H833.39v549.522c0 56.247-36.645 95.525-69.998 95.525H284.379v-0.512z M357.23 800.695a48.274 48.274 0 0 0 47.616-49.006V471.7a48.274 48.274 0 0 0-47.543-49.08 48.274 48.274 0 0 0-47.69 49.006V751.69c0 27.282 20.846 49.006 47.617 49.006z m166.62 0a48.274 48.274 0 0 0 47.688-49.006V471.7a48.274 48.274 0 0 0-47.689-49.08 48.274 48.274 0 0 0-47.543 49.006V751.69c0 27.282 21.431 49.006 47.543 49.006z m142.92 0a48.274 48.274 0 0 0 47.543-49.006V471.7a48.274 48.274 0 0 0-47.543-49.08 48.274 48.274 0 0 0-47.616 49.006V751.69c0 27.282 20.773 49.006 47.543 49.006z"
			};
			myIconButton2.ToolTip = "删除";
			ToolTipService.SetPlacement(myIconButton2, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton2, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton2, 2.0);
			myIconButton2.Click += delegate(object sender, EventArgs e)
			{
				base._Lambda$__1();
			};
			if (Version._ConfigurationMap != ModMinecraft.McVersionState.Error)
			{
				MyIconButton myIconButton3 = new MyIconButton
				{
					LogoScale = 1.1,
					Logo = "M651.946667 1001.813333c-22.186667 0-42.666667-10.24-61.44-27.306666-23.893333-23.893333-49.493333-35.84-75.093334-35.84-29.013333 0-56.32 11.946667-73.386666 30.72v3.413333c-17.066667 17.066667-42.666667 27.306667-66.56 27.306667h-6.826667c-6.826667 0-11.946667-1.706667-15.36-1.706667l-6.826667-1.706667c-64.853333-20.48-121.173333-54.613333-168.96-98.986666-29.013333-23.893333-37.546667-63.146667-25.6-95.573334 8.533333-23.893333 5.12-51.2-10.24-75.093333-15.36-27.306667-34.133333-40.96-59.733333-47.786667h-1.706667l-5.12-1.706666c-35.84-8.533333-61.44-34.133333-66.56-69.973334C1.706667 575.146667 0 537.6 0 512c0-32.426667 3.413333-63.146667 8.533333-93.866667v-6.826666l3.413334-8.533334c10.24-23.893333 23.893333-40.96 44.373333-51.2 5.12-3.413333 11.946667-6.826667 20.48-8.533333 27.306667-8.533333 51.2-25.6 63.146667-44.373333 13.653333-23.893333 17.066667-52.906667 10.24-81.92-11.946667-34.133333 0-71.68 30.72-93.866667 44.373333-37.546667 97.28-68.266667 158.72-93.866667l3.413333-1.706666c44.373333-13.653333 75.093333 3.413333 92.16 20.48 23.893333 23.893333 49.493333 35.84 75.093333 35.84 30.72 0 56.32-10.24 71.68-30.72l3.413334-3.413334c27.306667-27.306667 63.146667-35.84 93.866666-22.186666 63.146667 22.186667 117.76 54.613333 165.546667 97.28 29.013333 23.893333 37.546667 63.146667 25.6 95.573333-8.533333 23.893333-5.12 51.2 10.24 75.093333 15.36 27.306667 34.133333 40.96 59.733333 47.786667h1.706667l5.12 1.706667c35.84 8.533333 61.44 34.133333 66.56 71.68 6.826667 30.72 10.24 63.146667 11.946667 93.866666v3.413334c0 32.426667-3.413333 63.146667-8.533334 93.866666v6.826667l-3.413333 8.533333c-10.24 23.893333-23.893333 40.96-44.373333 51.2-5.12 3.413333-11.946667 6.826667-20.48 8.533334-27.306667 8.533333-51.2 25.6-63.146667 46.08-13.653333 23.893333-17.066667 52.906667-10.24 81.92 11.946667 35.84-1.706667 75.093333-30.72 95.573333-44.373333 35.84-95.573333 66.56-157.013333 92.16-15.36 3.413333-27.306667 3.413333-35.84 3.413333z m3.413333-83.626666z m1.706667 0zM517.12 853.333333c47.786667 0 93.866667 20.48 134.826667 59.733334 1.706667 1.706667 3.413333 1.706667 3.413333 3.413333 52.906667-22.186667 97.28-49.493333 136.533333-80.213333l1.706667-1.706667v-3.413333c-13.653333-52.906667-8.533333-104.106667 17.066667-148.48 23.893333-39.253333 64.853333-69.973333 114.346666-85.333334 1.706667 0 3.413333-1.706667 6.826667-6.826666 5.12-25.6 8.533333-51.2 8.533333-78.506667-1.706667-29.013333-3.413333-56.32-10.24-81.92v-5.12h-1.706666c-51.2-11.946667-90.453333-39.253333-119.466667-87.04-27.306667-44.373333-34.133333-100.693333-17.066667-148.48l-1.706666-1.706667h-3.413334c-39.253333-35.84-85.333333-63.146667-136.533333-80.213333H648.533333s-1.706667 1.706667-3.413333 1.706667c-32.426667 39.253333-80.213333 59.733333-136.533333 59.733333-47.786667 0-93.866667-20.48-134.826667-59.733333l-1.706667-1.706667h-1.706666c-54.613333 22.186667-98.986667 49.493333-136.533334 80.213333l-1.706666 1.706667v3.413333c13.653333 52.906667 8.533333 104.106667-17.066667 148.48-23.893333 39.253333-64.853333 69.973333-114.346667 85.333334-1.706667 0-3.413333 1.706667-6.826666 6.826666-6.826667 25.6-8.533333 51.2-8.533334 78.506667 0 30.72 3.413333 58.026667 6.826667 76.8l1.706667 5.12h1.706666c51.2 11.946667 90.453333 39.253333 119.466667 87.04 27.306667 44.373333 34.133333 100.693333 17.066667 148.48l1.706666 1.706667 1.706667 1.706666c37.546667 35.84 83.626667 63.146667 134.826667 80.213334 1.706667 0 3.413333 0 3.413333 1.706666h1.706667s1.706667 0 5.12-1.706666c34.133333-37.546667 81.92-59.733333 136.533333-59.733334z m-6.826667-146.773333c-110.933333 0-199.68-85.333333-199.68-196.266667 0-109.226667 87.04-196.266667 199.68-196.266666s199.68 85.333333 199.68 196.266666c-1.706667 109.226667-88.746667 196.266667-199.68 196.266667z m0-307.2c-63.146667 0-114.346667 49.493333-114.346666 110.933333 0 63.146667 49.493333 110.933333 114.346666 110.933334 30.72 0 59.733333-11.946667 80.213334-32.426667 20.48-20.48 32.426667-49.493333 32.426666-78.506667 0-63.146667-49.493333-110.933333-112.64-110.933333z"
				};
				myIconButton3.ToolTip = "设置";
				ToolTipService.SetPlacement(myIconButton3, PlacementMode.Center);
				ToolTipService.SetVerticalOffset(myIconButton3, 30.0);
				ToolTipService.SetHorizontalOffset(myIconButton3, 2.0);
				myIconButton3.Click += delegate(object sender, EventArgs e)
				{
					base._Lambda$__2();
				};
				sender2.MouseRightButtonUp += delegate(object sender, MouseButtonEventArgs e)
				{
					base._Lambda$__3();
				};
				sender2.Buttons = new MyIconButton[]
				{
					myIconButton,
					myIconButton2,
					myIconButton3
				};
				return;
			}
			MyIconButton myIconButton4 = new MyIconButton
			{
				LogoScale = 1.15,
				Logo = "M889.018182 418.909091H884.363636V316.509091a93.090909 93.090909 0 0 0-99.607272-89.832727h-302.545455l-93.090909-76.334546A46.545455 46.545455 0 0 0 358.865455 139.636364H146.152727A93.090909 93.090909 0 0 0 46.545455 229.469091V837.818182a46.545455 46.545455 0 0 0 46.545454 46.545454 46.545455 46.545455 0 0 0 16.756364-3.258181 109.381818 109.381818 0 0 0 25.134545 3.258181h586.472727a85.178182 85.178182 0 0 0 87.04-63.301818l163.374546-302.545454a46.545455 46.545455 0 0 0 5.585454-21.876364A82.385455 82.385455 0 0 0 889.018182 418.909091z m-744.727273-186.181818h198.283636l93.09091 76.334545a46.545455 46.545455 0 0 0 29.323636 10.705455h319.301818a12.101818 12.101818 0 0 1 6.516364 0V418.909091H302.545455a85.178182 85.178182 0 0 0-87.04 63.301818L139.636364 622.778182V232.727273a19.549091 19.549091 0 0 1 6.516363 0z m578.094546 552.029091a27.461818 27.461818 0 0 0-2.792728 6.516363H154.530909l147.083636-272.290909a27.461818 27.461818 0 0 0 2.792728-6.981818h565.061818z"
			};
			myIconButton4.ToolTip = "打开文件夹";
			ToolTipService.SetPlacement(myIconButton4, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton4, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton4, 2.0);
			myIconButton4.Click += delegate(object sender, EventArgs e)
			{
				base._Lambda$__4();
			};
			sender2.MouseRightButtonUp += delegate(object sender, MouseButtonEventArgs e)
			{
				base._Lambda$__5();
			};
			sender2.Buttons = new MyIconButton[]
			{
				myIconButton,
				myIconButton2,
				myIconButton4
			};
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0007553C File Offset: 0x0007373C
		public static void Item_Click(MyListItem sender, EventArgs e)
		{
			ModMinecraft.McVersion mcVersion = (ModMinecraft.McVersion)sender.Tag;
			if (new ModMinecraft.McVersion(mcVersion.Path).Check())
			{
				ModMinecraft.InstantiateClient(mcVersion);
				ModBase.m_IdentifierRepository.Set("LaunchVersionSelect", ModMinecraft.AddClient().Name, false, null);
				ModMain._ProcessIterator.PageBack();
				return;
			}
			PageVersionOverall.OpenVersionFolder(mcVersion);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00007EEB File Offset: 0x000060EB
		private void BtnDownload_Click(object sender, EventArgs e)
		{
			ModMain._ProcessIterator.PageChange(FormMain.PageType.Download, FormMain.PageSubType.DownloadInstall);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0007559C File Offset: 0x0007379C
		public static void DeleteVersion(MyListItem Item, ModMinecraft.McVersion Version)
		{
			try
			{
				bool shiftKeyDown = MyWpfExtension.ManageParser().Keyboard.ShiftKeyDown;
				bool flag = Version._ConfigurationMap != ModMinecraft.McVersionState.Error && Operators.CompareString(Version.ChangeMapper(), ModMinecraft.m_ProxyTests, false) != 0;
				int num = ModMain.MyMsgBox(string.Format("你确定要{0}删除版本 {1} 吗？", shiftKeyDown ? "永久" : "", Version.Name) + (flag ? "\r\n由于该版本开启了版本隔离，删除版本时该版本对应的存档、资源包、Mod 等文件也将被一并删除！" : ""), "版本删除确认", "确定", "取消", "", true, true, false, null, null, null);
				if (num != 1)
				{
					if (num == 2)
					{
						return;
					}
				}
				else
				{
					ModBase.IniClearCache(Version.Path + "PCL\\Setup.ini");
					if (shiftKeyDown)
					{
						ModBase.DeleteDirectory(Version.Path, false);
						ModMain.Hint("版本 " + Version.Name + " 已永久删除！", ModMain.HintType.Finish, true);
					}
					else
					{
						FileSystem.DeleteDirectory(Version.Path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
						ModMain.Hint("版本 " + Version.Name + " 已删除到回收站！", ModMain.HintType.Finish, true);
					}
				}
				if (Version.expressionMap != ModMinecraft.McVersionCardType.Hidden && Version._TokenMap)
				{
					ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", false);
				}
				else
				{
					StackPanel stackPanel = (StackPanel)Item.Parent;
					if (stackPanel.Children.Count > 2)
					{
						MyCard myCard = (MyCard)stackPanel.Parent;
						myCard.Title = checked(myCard.Title.Replace(Conversions.ToString(stackPanel.Children.Count - 1), Conversions.ToString(stackPanel.Children.Count - 2)));
						stackPanel.Children.Remove(Item);
						if (ModMinecraft.AddClient() != null && Operators.CompareString(Version.Path, ModMinecraft.AddClient().Path, false) == 0)
						{
							ModMinecraft.InstantiateClient((ModMinecraft.McVersion)((MyListItem)stackPanel.Children[0]).Tag);
						}
						ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.UpdateOnly, 1, "versions\\", false);
					}
					else
					{
						ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", false);
					}
				}
			}
			catch (OperationCanceledException ex)
			{
				ModBase.Log(ex, "删除版本 " + Version.Name + " 被主动取消", ModBase.LogLevel.Debug, "出现错误");
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, "删除版本 " + Version.Name + " 失败", ModBase.LogLevel.Msgbox, "出现错误");
			}
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00075848 File Offset: 0x00073A48
		public void BtnEmptyDownload_Loaded()
		{
			Visibility visibility = Conversions.ToBoolean((Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenPageDownload", null)) && !PageSetupUI.CreateClient()) || this._StatusMapper) ? Visibility.Collapsed : Visibility.Visible;
			if (this.BtnEmptyDownload.Visibility != visibility)
			{
				this.BtnEmptyDownload.Visibility = visibility;
				this.PanLoad.TriggerForceResize();
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x00009C68 File Offset: 0x00007E68
		// (set) Token: 0x06000FF6 RID: 4086 RVA: 0x00009C70 File Offset: 0x00007E70
		internal virtual Grid PanAllBack { get; set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x00009C79 File Offset: 0x00007E79
		// (set) Token: 0x06000FF8 RID: 4088 RVA: 0x00009C81 File Offset: 0x00007E81
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x00009C8A File Offset: 0x00007E8A
		// (set) Token: 0x06000FFA RID: 4090 RVA: 0x00009C92 File Offset: 0x00007E92
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x00009C9B File Offset: 0x00007E9B
		// (set) Token: 0x06000FFC RID: 4092 RVA: 0x00009CA3 File Offset: 0x00007EA3
		internal virtual MyCard PanEmpty { get; set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x00009CAC File Offset: 0x00007EAC
		// (set) Token: 0x06000FFE RID: 4094 RVA: 0x00009CB4 File Offset: 0x00007EB4
		internal virtual TextBlock LabEmptyTitle { get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x00009CBD File Offset: 0x00007EBD
		// (set) Token: 0x06001000 RID: 4096 RVA: 0x00009CC5 File Offset: 0x00007EC5
		internal virtual TextBlock LabEmptyContent { get; set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x00009CCE File Offset: 0x00007ECE
		// (set) Token: 0x06001002 RID: 4098 RVA: 0x000758B4 File Offset: 0x00073AB4
		internal virtual MyButton BtnEmptyDownload
		{
			[CompilerGenerated]
			get
			{
				return this.advisorMapper;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnDownload_Click);
				RoutedEventHandler value3 = delegate(object sender, RoutedEventArgs e)
				{
					this.BtnEmptyDownload_Loaded();
				};
				MyButton myButton = this.advisorMapper;
				if (myButton != null)
				{
					myButton.Click -= value2;
					myButton.Loaded -= value3;
				}
				this.advisorMapper = value;
				myButton = this.advisorMapper;
				if (myButton != null)
				{
					myButton.Click += value2;
					myButton.Loaded += value3;
				}
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x00009CD6 File Offset: 0x00007ED6
		// (set) Token: 0x06001004 RID: 4100 RVA: 0x00009CDE File Offset: 0x00007EDE
		internal virtual MyCard PanLoad { get; set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x00009CE7 File Offset: 0x00007EE7
		// (set) Token: 0x06001006 RID: 4102 RVA: 0x00075914 File Offset: 0x00073B14
		internal virtual MyLoading Load
		{
			[CompilerGenerated]
			get
			{
				return this.m_QueueMapper;
			}
			[CompilerGenerated]
			set
			{
				MyLoading.ClickEventHandler value2 = new MyLoading.ClickEventHandler(this.Load_Click);
				MyLoading queueMapper = this.m_QueueMapper;
				if (queueMapper != null)
				{
					queueMapper.Click -= value2;
				}
				this.m_QueueMapper = value;
				queueMapper = this.m_QueueMapper;
				if (queueMapper != null)
				{
					queueMapper.Click += value2;
				}
			}
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00075958 File Offset: 0x00073B58
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.eventMapper)
			{
				this.eventMapper = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageselectright.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00075988 File Offset: 0x00073B88
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanAllBack = (Grid)target;
				return;
			}
			if (connectionId == 2)
			{
				this.PanBack = (MyScrollViewer)target;
				return;
			}
			if (connectionId == 3)
			{
				this.PanMain = (StackPanel)target;
				return;
			}
			if (connectionId == 4)
			{
				this.PanEmpty = (MyCard)target;
				return;
			}
			if (connectionId == 5)
			{
				this.LabEmptyTitle = (TextBlock)target;
				return;
			}
			if (connectionId == 6)
			{
				this.LabEmptyContent = (TextBlock)target;
				return;
			}
			if (connectionId == 7)
			{
				this.BtnEmptyDownload = (MyButton)target;
				return;
			}
			if (connectionId == 8)
			{
				this.PanLoad = (MyCard)target;
				return;
			}
			if (connectionId == 9)
			{
				this.Load = (MyLoading)target;
				return;
			}
			this.eventMapper = true;
		}

		// Token: 0x04000878 RID: 2168
		public bool _StatusMapper;

		// Token: 0x04000879 RID: 2169
		[AccessedThroughProperty("PanAllBack")]
		[CompilerGenerated]
		private Grid _RoleMapper;

		// Token: 0x0400087A RID: 2170
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyScrollViewer m_StructMapper;

		// Token: 0x0400087B RID: 2171
		[AccessedThroughProperty("PanMain")]
		[CompilerGenerated]
		private StackPanel printerMapper;

		// Token: 0x0400087C RID: 2172
		[AccessedThroughProperty("PanEmpty")]
		[CompilerGenerated]
		private MyCard _ValMapper;

		// Token: 0x0400087D RID: 2173
		[AccessedThroughProperty("LabEmptyTitle")]
		[CompilerGenerated]
		private TextBlock attrMapper;

		// Token: 0x0400087E RID: 2174
		[AccessedThroughProperty("LabEmptyContent")]
		[CompilerGenerated]
		private TextBlock _CandidateMapper;

		// Token: 0x0400087F RID: 2175
		[CompilerGenerated]
		[AccessedThroughProperty("BtnEmptyDownload")]
		private MyButton advisorMapper;

		// Token: 0x04000880 RID: 2176
		[CompilerGenerated]
		[AccessedThroughProperty("PanLoad")]
		private MyCard _AccountMapper;

		// Token: 0x04000881 RID: 2177
		[AccessedThroughProperty("Load")]
		[CompilerGenerated]
		private MyLoading m_QueueMapper;

		// Token: 0x04000882 RID: 2178
		private bool eventMapper;
	}
}
