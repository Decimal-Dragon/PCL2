using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;
using PCL.My;

namespace PCL
{
	// Token: 0x0200010F RID: 271
	[DesignerGenerated]
	public class PageVersionMod : MyPageRight, IRefreshable, IComponentConnector
	{
		// Token: 0x06000AF6 RID: 2806 RVA: 0x0004C274 File Offset: 0x0004A474
		public PageVersionMod()
		{
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.PageOther_Loaded();
			};
			base.Initialized += delegate(object sender, EventArgs e)
			{
				this.LoaderInit();
			};
			base.MapBroadcaster(new MyPageRight.PageExitEventHandler(this.UnselectedAllWithAnimation));
			base.KeyDown += this.PageVersionMod_KeyDown;
			this.getterConfig = false;
			this.tokenConfig = new Dictionary<string, MyLocalModItem>();
			this.expressionConfig = 0;
			this.m_WriterConfig = new List<string>();
			this.m_RegistryConfig = PageVersionMod.FilterType.All;
			this.InitializeComponent();
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0004C304 File Offset: 0x0004A504
		public void PageOther_Loaded()
		{
			if (ModMain._ProcessIterator.taskIterator.initializerMap != FormMain.PageType.CompDetail)
			{
				this.PanBack.ScrollToHome();
			}
			checked
			{
				ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
				this.m_WriterConfig.Clear();
				this.ReloadModList(false);
				this.ChangeAllSelected(false);
				ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
				if (!this.getterConfig)
				{
					this.getterConfig = true;
					try
					{
						foreach (object obj in this.PanFilter.Children)
						{
							((MyRadioButton)obj).LabText.Margin = new Thickness(-2.0, 0.0, 8.0, 0.0);
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
			}
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0004C3F0 File Offset: 0x0004A5F0
		public void ReloadModList(bool ForceReload = false)
		{
			if (this.LoaderRun(ForceReload ? ModLoader.LoaderFolderRunType.ForceRun : ModLoader.LoaderFolderRunType.RunOnUpdated))
			{
				ModBase.Log("[System] 已刷新 Mod 列表", ModBase.LogLevel.Normal, "出现错误");
				this.QueryReader(PageVersionMod.FilterType.All);
				this.PanBack.ScrollToHome();
				this.SearchBox.Text = "";
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00007932 File Offset: 0x00005B32
		void IRefreshable.RefreshSelf()
		{
			PageVersionMod.Refresh();
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0004C440 File Offset: 0x0004A640
		public static void Refresh()
		{
			try
			{
				ModComp.pool.Clear();
				ModComp.customer.Clear();
				File.Delete(ModBase.m_DecoratorRepository + "Cache\\LocalMod.json");
				ModBase.Log("[Mod] 由于点击刷新按钮，清理本地 Mod 信息缓存", ModBase.LogLevel.Normal, "出现错误");
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "强制刷新时清理本地 Mod 信息缓存失败", ModBase.LogLevel.Debug, "出现错误");
			}
			if (ModMain._ThreadRepository != null)
			{
				ModMain._ThreadRepository.ReloadModList(true);
			}
			ModMain.m_TestsRepository.ItemMod.Checked = true;
			ModMain.Hint("正在刷新……", ModMain.HintType.Info, false);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0004C4E4 File Offset: 0x0004A6E4
		private void LoaderInit()
		{
			base.PageLoaderInit(this.Load, this.PanLoad, this.PanAllBack, null, ModMod.m_Record, delegate(ModLoader.LoaderBase a0)
			{
				this.LoadUIFromLoaderOutput();
			}, null, false);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0000799F File Offset: 0x00005B9F
		private void Load_Click(object sender, MouseButtonEventArgs e)
		{
			if (ModMod.m_Record.State == ModBase.LoadState.Failed)
			{
				this.LoaderRun(ModLoader.LoaderFolderRunType.ForceRun);
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x000079B6 File Offset: 0x00005BB6
		public bool LoaderRun(ModLoader.LoaderFolderRunType Type)
		{
			return ModLoader.LoaderFolderRun(ModMod.m_Record, PageVersionLeft._InstanceConfig.ChangeMapper() + "mods\\", Type, 0, "", false);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0004C520 File Offset: 0x0004A720
		private void LoadUIFromLoaderOutput()
		{
			try
			{
				if (Enumerable.Any<ModMod.McMod>(ModMod.m_Record.Output))
				{
					this.PanBack.Visibility = Visibility.Visible;
					this.PanEmpty.Visibility = Visibility.Collapsed;
					this.tokenConfig.Clear();
					try
					{
						foreach (ModMod.McMod mcMod in ModMod.m_Record.Output)
						{
							this.tokenConfig[mcMod.ConcatTests()] = this.McModListItem(mcMod);
						}
					}
					finally
					{
						List<ModMod.McMod>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					this.QueryReader(PageVersionMod.FilterType.All);
					this.SearchBox.Text = "";
					this.RefreshUI();
				}
				else
				{
					this.PanEmpty.Visibility = Visibility.Visible;
					this.PanBack.Visibility = Visibility.Collapsed;
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "加载 Mod 列表 UI 失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0004C624 File Offset: 0x0004A824
		private MyLocalModItem McModListItem(ModMod.McMod Entry)
		{
			checked
			{
				ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
				MyLocalModItem myLocalModItem = new MyLocalModItem();
				myLocalModItem.SnapsToDevicePixels = true;
				myLocalModItem.PopBroadcaster(Entry);
				myLocalModItem._Policy = new Action<MyLocalModItem, EventArgs>(this.McModContent);
				myLocalModItem.Checked = this.m_WriterConfig.Contains(Entry.ConcatTests());
				MyLocalModItem myLocalModItem2 = myLocalModItem;
				MyLocalModItem $VB$NonLocal_2 = myLocalModItem2;
				Entry.ViewMapper(delegate(ModMod.McMod a0)
				{
					$VB$NonLocal_2.Refresh();
				});
				myLocalModItem2.Refresh();
				ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
				return myLocalModItem2;
			}
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0004C6AC File Offset: 0x0004A8AC
		private void McModContent(MyLocalModItem sender, EventArgs e)
		{
			sender2.RateBroadcaster(delegate(object sender, ModBase.RouteEventArgs e)
			{
				this.CheckChanged((MyLocalModItem)sender, e);
			});
			sender2.Click += ((PageVersionMod._Closure$__.$IR13-7 == null) ? (PageVersionMod._Closure$__.$IR13-7 = delegate(object sender, MouseButtonEventArgs e)
			{
				((PageVersionMod._Closure$__.$I13-0 == null) ? (PageVersionMod._Closure$__.$I13-0 = delegate(MyLocalModItem ss, EventArgs ee)
				{
					ss.Checked = !ss.Checked;
				}) : PageVersionMod._Closure$__.$I13-0)((MyLocalModItem)sender, e);
			}) : PageVersionMod._Closure$__.$IR13-7);
			MyIconButton myIconButton = new MyIconButton
			{
				LogoScale = 1.05,
				Logo = "M889.018182 418.909091H884.363636V316.509091a93.090909 93.090909 0 0 0-99.607272-89.832727h-302.545455l-93.090909-76.334546A46.545455 46.545455 0 0 0 358.865455 139.636364H146.152727A93.090909 93.090909 0 0 0 46.545455 229.469091V837.818182a46.545455 46.545455 0 0 0 46.545454 46.545454 46.545455 46.545455 0 0 0 16.756364-3.258181 109.381818 109.381818 0 0 0 25.134545 3.258181h586.472727a85.178182 85.178182 0 0 0 87.04-63.301818l163.374546-302.545454a46.545455 46.545455 0 0 0 5.585454-21.876364A82.385455 82.385455 0 0 0 889.018182 418.909091z m-744.727273-186.181818h198.283636l93.09091 76.334545a46.545455 46.545455 0 0 0 29.323636 10.705455h319.301818a12.101818 12.101818 0 0 1 6.516364 0V418.909091H302.545455a85.178182 85.178182 0 0 0-87.04 63.301818L139.636364 622.778182V232.727273a19.549091 19.549091 0 0 1 6.516363 0z m578.094546 552.029091a27.461818 27.461818 0 0 0-2.792728 6.516363H154.530909l147.083636-272.290909a27.461818 27.461818 0 0 0 2.792728-6.981818h565.061818z",
				Tag = sender2
			};
			myIconButton.ToolTip = "打开文件位置";
			ToolTipService.SetPlacement(myIconButton, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton, 2.0);
			myIconButton.Click += delegate(object sender, EventArgs e)
			{
				this.Open_Click((MyIconButton)sender, e);
			};
			MyIconButton myIconButton2 = new MyIconButton
			{
				LogoScale = 1.0,
				Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
				Tag = sender2
			};
			myIconButton2.ToolTip = "详情";
			ToolTipService.SetPlacement(myIconButton2, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton2, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton2, 2.0);
			myIconButton2.Click += this.Info_Click;
			sender2.MouseRightButtonUp += new MouseButtonEventHandler(this.Info_Click);
			MyIconButton myIconButton3 = new MyIconButton
			{
				LogoScale = 1.0,
				Logo = "M520.192 0C408.43 0 317.44 82.87 313.563 186.734H52.736c-29.038 0-52.663 21.943-52.663 49.079s23.625 49.152 52.663 49.152h58.075v550.473c0 103.35 75.118 187.757 167.717 187.757h472.43c92.599 0 167.716-83.894 167.716-187.757V285.477h52.59c29.038 0 52.59-21.943 52.663-49.08-0.073-27.135-23.625-49.151-52.663-49.151H726.235C723.237 83.017 631.955 0 520.192 0zM404.846 177.957c3.803-50.03 50.176-89.015 107.447-89.015 57.197 0 103.57 38.985 106.788 89.015H404.92zM284.379 933.669c-33.353 0-69.997-39.351-69.997-95.525v-549.01H833.39v549.522c0 56.247-36.645 95.525-69.998 95.525H284.379v-0.512z M357.23 800.695a48.274 48.274 0 0 0 47.616-49.006V471.7a48.274 48.274 0 0 0-47.543-49.08 48.274 48.274 0 0 0-47.69 49.006V751.69c0 27.282 20.846 49.006 47.617 49.006z m166.62 0a48.274 48.274 0 0 0 47.688-49.006V471.7a48.274 48.274 0 0 0-47.689-49.08 48.274 48.274 0 0 0-47.543 49.006V751.69c0 27.282 21.431 49.006 47.543 49.006z m142.92 0a48.274 48.274 0 0 0 47.543-49.006V471.7a48.274 48.274 0 0 0-47.543-49.08 48.274 48.274 0 0 0-47.616 49.006V751.69c0 27.282 20.773 49.006 47.543 49.006z",
				Tag = sender2
			};
			myIconButton3.ToolTip = "删除";
			ToolTipService.SetPlacement(myIconButton3, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton3, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton3, 2.0);
			myIconButton3.Click += delegate(object sender, EventArgs e)
			{
				this.Delete_Click((MyIconButton)sender, e);
			};
			MyIconButton myIconButton4 = new MyIconButton
			{
				LogoScale = 1.0,
				Logo = ((sender2.ExcludeBroadcaster().State == ModMod.McMod.McModState.Fine) ? "M508 990.4c-261.6 0-474.4-212-474.4-474.4S246.4 41.6 508 41.6s474.4 212 474.4 474.4S769.6 990.4 508 990.4zM508 136.8c-209.6 0-379.2 169.6-379.2 379.2 0 209.6 169.6 379.2 379.2 379.2s379.2-169.6 379.2-379.2C887.2 306.4 717.6 136.8 508 136.8zM697.6 563.2 318.4 563.2c-26.4 0-47.2-21.6-47.2-47.2 0-26.4 21.6-47.2 47.2-47.2l379.2 0c26.4 0 47.2 21.6 47.2 47.2C744.8 542.4 724 563.2 697.6 563.2z" : "M512 0a512 512 0 1 0 512 512A512 512 0 0 0 512 0z m0 921.6a409.6 409.6 0 1 1 409.6-409.6 409.6 409.6 0 0 1-409.6 409.6z M716.8 339.968l-256 253.44L328.192 460.8A51.2 51.2 0 0 0 256 532.992l168.448 168.96a51.2 51.2 0 0 0 72.704 0l289.28-289.792A51.2 51.2 0 0 0 716.8 339.968z"),
				Tag = sender2
			};
			myIconButton4.ToolTip = ((sender2.ExcludeBroadcaster().State == ModMod.McMod.McModState.Fine) ? "禁用" : "启用");
			ToolTipService.SetPlacement(myIconButton4, PlacementMode.Center);
			ToolTipService.SetVerticalOffset(myIconButton4, 30.0);
			ToolTipService.SetHorizontalOffset(myIconButton4, 2.0);
			myIconButton4.Click += delegate(object sender, EventArgs e)
			{
				this.ED_Click((MyIconButton)sender, e);
			};
			if (sender2.ExcludeBroadcaster().State == ModMod.McMod.McModState.Unavailable)
			{
				sender2.Buttons = new MyIconButton[]
				{
					myIconButton2,
					myIconButton,
					myIconButton3
				};
				return;
			}
			sender2.Buttons = new MyIconButton[]
			{
				myIconButton2,
				myIconButton,
				myIconButton4,
				myIconButton3
			};
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0004C918 File Offset: 0x0004AB18
		public void RefreshUI()
		{
			PageVersionMod._Closure$__14-0 CS$<>8__locals1 = new PageVersionMod._Closure$__14-0(CS$<>8__locals1);
			checked
			{
				if (this.PanList != null)
				{
					CS$<>8__locals1.$VB$Local_ShowingMods = Enumerable.ToList<ModMod.McMod>(Enumerable.Where<ModMod.McMod>(this.InterruptReader() ? this._ProccesorConfig : (ModMod.m_Record.Output ?? new List<ModMod.McMod>()), (ModMod.McMod m) => this.CanPassFilter(m)));
					ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
					if (Enumerable.Any<ModMod.McMod>(CS$<>8__locals1.$VB$Local_ShowingMods))
					{
						this.PanList.Visibility = Visibility.Visible;
						this.PanList.Children.Clear();
						try
						{
							foreach (ModMod.McMod mcMod in CS$<>8__locals1.$VB$Local_ShowingMods)
							{
								MyLocalModItem myLocalModItem = this.tokenConfig[mcMod.ConcatTests()];
								myLocalModItem.Checked = this.m_WriterConfig.Contains(mcMod.ConcatTests());
								this.PanList.Children.Add(myLocalModItem);
							}
							goto IL_FF;
						}
						finally
						{
							List<ModMod.McMod>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
					this.PanList.Visibility = Visibility.Collapsed;
					IL_FF:
					ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
					this.m_WriterConfig = Enumerable.ToList<string>(Enumerable.Where<string>(this.m_WriterConfig, delegate(string m)
					{
						PageVersionMod._Closure$__14-1 CS$<>8__locals2 = new PageVersionMod._Closure$__14-1(CS$<>8__locals2);
						CS$<>8__locals2.$VB$Local_m = m;
						return Enumerable.Any<ModMod.McMod>(CS$<>8__locals1.$VB$Local_ShowingMods, (ModMod.McMod s) => Operators.CompareString(s.ConcatTests(), CS$<>8__locals2.$VB$Local_m, false) == 0);
					}));
					this.RefreshBars();
				}
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0004CA68 File Offset: 0x0004AC68
		public void RefreshBars()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int count;
			bool flag;
			checked
			{
				try
				{
					foreach (ModMod.McMod mcMod in (this.InterruptReader() ? this._ProccesorConfig : (ModMod.m_Record.Output ?? new List<ModMod.McMod>())))
					{
						num++;
						if (mcMod.SelectMapper())
						{
							num4++;
						}
						if (mcMod.State.Equals(ModMod.McMod.McModState.Fine))
						{
							num2++;
						}
						if (mcMod.State.Equals(ModMod.McMod.McModState.Disabled))
						{
							num3++;
						}
						if (mcMod.State.Equals(ModMod.McMod.McModState.Unavailable))
						{
							num5++;
						}
					}
				}
				finally
				{
					List<ModMod.McMod>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				this.BtnFilterAll.Text = (this.InterruptReader() ? "搜索结果" : "全部") + string.Format(" ({0})", num);
				this.BtnFilterCanUpdate.Text = string.Format("可更新 ({0})", num4);
				this.BtnFilterCanUpdate.Visibility = ((this.StartReader() == PageVersionMod.FilterType.CanUpdate || num4 > 0) ? Visibility.Visible : Visibility.Collapsed);
				this.BtnFilterEnabled.Text = string.Format("启用 ({0})", num2);
				this.BtnFilterEnabled.Visibility = ((this.StartReader() == PageVersionMod.FilterType.Enabled || (num2 > 0 && num2 < num)) ? Visibility.Visible : Visibility.Collapsed);
				this.BtnFilterDisabled.Text = string.Format("禁用 ({0})", num3);
				this.BtnFilterDisabled.Visibility = ((this.StartReader() == PageVersionMod.FilterType.Disabled || num3 > 0) ? Visibility.Visible : Visibility.Collapsed);
				this.BtnFilterError.Text = string.Format("错误 ({0})", num5);
				this.BtnFilterError.Visibility = ((this.StartReader() == PageVersionMod.FilterType.Unavailable || num5 > 0) ? Visibility.Visible : Visibility.Collapsed);
				count = this.m_WriterConfig.Count;
				if (flag = (count > 0))
				{
					this.LabSelect.Text = string.Format("已选择 {0} 个文件", count);
				}
				if (flag)
				{
					bool isEnabled = false;
					bool isEnabled2 = false;
					bool isEnabled3 = false;
					try
					{
						foreach (ModMod.McMod mcMod2 in ModMod.m_Record.Output)
						{
							if (this.m_WriterConfig.Contains(mcMod2.ConcatTests()))
							{
								if (mcMod2.SelectMapper())
								{
									isEnabled = true;
								}
								if (mcMod2.State == ModMod.McMod.McModState.Fine)
								{
									isEnabled2 = true;
								}
								else if (mcMod2.State == ModMod.McMod.McModState.Disabled)
								{
									isEnabled3 = true;
								}
							}
						}
					}
					finally
					{
						List<ModMod.McMod>.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
					this.BtnSelectDisable.IsEnabled = isEnabled2;
					this.BtnSelectEnable.IsEnabled = isEnabled3;
					this.BtnSelectUpdate.IsEnabled = isEnabled;
				}
			}
			if (ModAnimation.CalcParser() == 0)
			{
				this.PanListBack.Margin = new Thickness(0.0, 0.0, 0.0, (double)(flag ? 95 : 15));
				if (flag)
				{
					if (this.expressionConfig >= count)
					{
						this.expressionConfig = count;
						return;
					}
					this.expressionConfig = count;
					this.CardSelect.Visibility = Visibility.Visible;
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaOpacity(this.CardSelect, 1.0 - this.CardSelect.Opacity, 60, 0, null, false),
						ModAnimation.AaTranslateY(this.CardSelect, -27.0 - this.TransSelect.Y, 120, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false),
						ModAnimation.AaTranslateY(this.CardSelect, 3.0, 150, 120, new ModAnimation.AniEaseInoutFluent(ModAnimation.AniEasePower.Weak, 0.5), false),
						ModAnimation.AaTranslateY(this.CardSelect, -1.0, 90, 270, new ModAnimation.AniEaseInoutFluent(ModAnimation.AniEasePower.Weak, 0.5), false)
					}, "Mod Sidebar", false);
					return;
				}
				else if (this.expressionConfig != 0)
				{
					this.expressionConfig = 0;
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaOpacity(this.CardSelect, -this.CardSelect.Opacity, 90, 0, null, false),
						ModAnimation.AaTranslateY(this.CardSelect, -10.0 - this.TransSelect.Y, 90, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak), false),
						ModAnimation.AaCode(delegate
						{
							this.CardSelect.Visibility = Visibility.Collapsed;
						}, 0, true)
					}, "Mod Sidebar", false);
					return;
				}
			}
			else
			{
				ModAnimation.AniStop("Mod Sidebar");
				this.expressionConfig = count;
				if (flag)
				{
					this.CardSelect.Visibility = Visibility.Visible;
					this.CardSelect.Opacity = 1.0;
					this.TransSelect.Y = -25.0;
					return;
				}
				this.CardSelect.Visibility = Visibility.Collapsed;
				this.CardSelect.Opacity = 0.0;
				this.TransSelect.Y = -10.0;
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0004CFA4 File Offset: 0x0004B1A4
		private void BtnManageOpen_Click(object sender, EventArgs e)
		{
			try
			{
				Directory.CreateDirectory(PageVersionLeft._InstanceConfig.ChangeMapper() + "mods\\");
				ModBase.OpenExplorer("\"" + PageVersionLeft._InstanceConfig.ChangeMapper() + "mods\\\"");
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "打开 Mods 文件夹失败", ModBase.LogLevel.Msgbox, "出现错误");
			}
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000079DE File Offset: 0x00005BDE
		private void BtnManageSelectAll_Click(object sender, MouseButtonEventArgs e)
		{
			this.ChangeAllSelected(this.m_WriterConfig.Count < this.PanList.Children.Count);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0004D01C File Offset: 0x0004B21C
		private void BtnManageInstall_Click(object sender, MouseButtonEventArgs e)
		{
			string[] array = ModBase.SelectFiles("Mod 文件(*.jar;*.litemod;*.disabled;*.old)|*.jar;*.litemod;*.disabled;*.old", "选择要安装的 Mod");
			if (Enumerable.Any<string>(array))
			{
				PageVersionMod.InstallMods(array);
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0004D048 File Offset: 0x0004B248
		public static bool InstallMods(IEnumerable<string> FilePathList)
		{
			PageVersionMod._Closure$__20-0 CS$<>8__locals1 = new PageVersionMod._Closure$__20-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_Extension = Enumerable.First<string>(FilePathList).AfterLast(".", false).ToLower();
			bool result;
			if (!Enumerable.Any<string>(new string[]
			{
				"jar",
				"litemod",
				"disabled",
				"old"
			}, (string t) => Operators.CompareString(t, CS$<>8__locals1.$VB$Local_Extension, false) == 0))
			{
				result = false;
			}
			else
			{
				ModBase.Log("[System] 文件为 jar/litemod 格式，尝试作为 Mod 安装", ModBase.LogLevel.Normal, "出现错误");
				if (Enumerable.First<string>(FilePathList).Contains(":\\$RECYCLE.BIN\\"))
				{
					ModMain.Hint("请先将文件从回收站还原，再尝试安装！", ModMain.HintType.Critical, true);
					result = true;
				}
				else
				{
					ModMinecraft.McVersion mcVersion = ModMinecraft.AddClient();
					if (ModMain._ProcessIterator._MethodIterator == FormMain.PageType.VersionSetup)
					{
						mcVersion = PageVersionLeft._InstanceConfig;
					}
					if (!(ModMain._ProcessIterator._MethodIterator == FormMain.PageType.VersionSelect) && mcVersion != null && mcVersion.RunThread())
					{
						if ((!(ModMain._ProcessIterator._MethodIterator == FormMain.PageType.VersionSetup) || ModMain._ProcessIterator.GetTests() != FormMain.PageSubType.SetupSystem) && ModMain.MyMsgBox(string.Format("是否要将这{0}文件作为 Mod 安装到 {1}？", (Enumerable.Count<string>(FilePathList) == 1) ? "个" : "些", mcVersion.Name), "Mod 安装确认", "确定", "取消", "", false, true, false, null, null, null) != 1)
						{
							goto IL_28A;
						}
						try
						{
							try
							{
								foreach (string text in FilePathList)
								{
									string text2 = ModBase.GetFileNameFromPath(text).Replace(".disabled", "");
									if (!text2.Contains("."))
									{
										text2 += ".jar";
									}
									ModBase.CopyFile(text, mcVersion.ChangeMapper() + "mods\\" + text2);
								}
							}
							finally
							{
								IEnumerator<string> enumerator;
								if (enumerator != null)
								{
									enumerator.Dispose();
								}
							}
							if (Enumerable.Count<string>(FilePathList) == 1)
							{
								ModMain.Hint(string.Format("已安装 {0}！", ModBase.GetFileNameFromPath(Enumerable.First<string>(FilePathList)).Replace(".disabled", "")), ModMain.HintType.Finish, true);
							}
							else
							{
								ModMain.Hint(string.Format("已安装 {0} 个 Mod！", Enumerable.Count<string>(FilePathList)), ModMain.HintType.Finish, true);
							}
							if (ModMain._ProcessIterator._MethodIterator == FormMain.PageType.VersionSetup && ModMain._ProcessIterator.GetTests() == FormMain.PageSubType.SetupSystem)
							{
								ModLoader.LoaderFolderRun(ModMod.m_Record, mcVersion.ChangeMapper() + "mods\\", ModLoader.LoaderFolderRunType.ForceRun, 0, "", false);
							}
							goto IL_28A;
						}
						catch (Exception ex)
						{
							ModBase.Log(ex, "复制 Mod 文件失败", ModBase.LogLevel.Msgbox, "出现错误");
							goto IL_28A;
						}
					}
					ModMain.Hint("若要安装 Mod，请先选择一个可以安装 Mod 的版本！", ModMain.HintType.Info, true);
					IL_28A:
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00007A03 File Offset: 0x00005C03
		private void BtnManageDownload_Click(object sender, MouseButtonEventArgs e)
		{
			PageDownloadMod.m_PageField = PageVersionLeft._InstanceConfig;
			ModMain._ProcessIterator.PageChange(FormMain.PageType.Download, FormMain.PageSubType.DownloadMod);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0004D318 File Offset: 0x0004B518
		public void CheckChanged(MyLocalModItem sender, ModBase.RouteEventArgs e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				string item = sender.ExcludeBroadcaster().ConcatTests();
				if (sender.Checked)
				{
					if (!this.m_WriterConfig.Contains(item))
					{
						this.m_WriterConfig.Add(item);
					}
				}
				else
				{
					this.m_WriterConfig.Remove(item);
				}
				this.RefreshBars();
			}
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0004D370 File Offset: 0x0004B570
		private void ChangeAllSelected(bool Value)
		{
			checked
			{
				ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
				this.m_WriterConfig.Clear();
				try
				{
					foreach (MyLocalModItem myLocalModItem in this.tokenConfig.Values)
					{
						bool flag = Value && this.PanList.Children.Contains(myLocalModItem);
						myLocalModItem.Checked = flag;
						if (flag)
						{
							this.m_WriterConfig.Add(myLocalModItem.ExcludeBroadcaster().ConcatTests());
						}
					}
				}
				finally
				{
					Dictionary<string, MyLocalModItem>.ValueCollection.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
				this.RefreshBars();
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0004D424 File Offset: 0x0004B624
		private void UnselectedAllWithAnimation()
		{
			int num = ModAnimation.CalcParser();
			ModAnimation.AssetParser(0);
			this.ChangeAllSelected(false);
			ModAnimation.AssetParser(checked(ModAnimation.CalcParser() + num));
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00007A21 File Offset: 0x00005C21
		private void PageVersionMod_KeyDown(object sender, KeyEventArgs e)
		{
			if (MyWpfExtension.ManageParser().Keyboard.CtrlKeyDown && e.Key == Key.A)
			{
				this.ChangeAllSelected(true);
			}
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00007A45 File Offset: 0x00005C45
		private PageVersionMod.FilterType StartReader()
		{
			return this.m_RegistryConfig;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0004D450 File Offset: 0x0004B650
		private void QueryReader(PageVersionMod.FilterType value)
		{
			if (this.m_RegistryConfig != value)
			{
				this.m_RegistryConfig = value;
				switch (value)
				{
				case PageVersionMod.FilterType.All:
					this.BtnFilterAll.Checked = true;
					break;
				case PageVersionMod.FilterType.Enabled:
					this.BtnFilterEnabled.Checked = true;
					break;
				case PageVersionMod.FilterType.Disabled:
					this.BtnFilterDisabled.Checked = true;
					break;
				case PageVersionMod.FilterType.CanUpdate:
					this.BtnFilterCanUpdate.Checked = true;
					break;
				default:
					this.BtnFilterError.Checked = true;
					break;
				}
				this.RefreshUI();
			}
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0004D4D0 File Offset: 0x0004B6D0
		private bool CanPassFilter(ModMod.McMod CheckingMod)
		{
			bool result;
			switch (this.StartReader())
			{
			case PageVersionMod.FilterType.All:
				result = true;
				break;
			case PageVersionMod.FilterType.Enabled:
				result = (CheckingMod.State == ModMod.McMod.McModState.Fine);
				break;
			case PageVersionMod.FilterType.Disabled:
				result = (CheckingMod.State == ModMod.McMod.McModState.Disabled);
				break;
			case PageVersionMod.FilterType.CanUpdate:
				result = CheckingMod.SelectMapper();
				break;
			case PageVersionMod.FilterType.Unavailable:
				result = (CheckingMod.State == ModMod.McMod.McModState.Unavailable);
				break;
			default:
				result = false;
				break;
			}
			return result;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00007A4D File Offset: 0x00005C4D
		private void ChangeFilter(MyRadioButton sender, bool raiseByMouse)
		{
			this.QueryReader((PageVersionMod.FilterType)Conversions.ToInteger(sender.Tag));
			this.RefreshUI();
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00007A66 File Offset: 0x00005C66
		private void BtnSelectED_Click(MyIconTextButton sender, ModBase.RouteEventArgs e)
		{
			this.EDMods(Enumerable.Where<ModMod.McMod>(ModMod.m_Record.Output, (ModMod.McMod m) => this.m_WriterConfig.Contains(m.ConcatTests())), !sender.Equals(this.BtnSelectDisable));
			this.ChangeAllSelected(false);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0004D534 File Offset: 0x0004B734
		private void EDMods(IEnumerable<ModMod.McMod> ModList, bool IsEnable)
		{
			bool flag = true;
			try
			{
				foreach (ModMod.McMod $VB$Local_ModEntity in Enumerable.ToList<ModMod.McMod>(ModList))
				{
					PageVersionMod._Closure$__35-0 CS$<>8__locals1 = new PageVersionMod._Closure$__35-0(CS$<>8__locals1);
					CS$<>8__locals1.$VB$Local_ModEntity = $VB$Local_ModEntity;
					string text = null;
					if (CS$<>8__locals1.$VB$Local_ModEntity.State == ModMod.McMod.McModState.Fine && !IsEnable)
					{
						text = CS$<>8__locals1.$VB$Local_ModEntity.Path + (File.Exists(CS$<>8__locals1.$VB$Local_ModEntity.Path + ".old") ? ".old" : ".disabled");
					}
					else
					{
						if (CS$<>8__locals1.$VB$Local_ModEntity.State != ModMod.McMod.McModState.Disabled || !IsEnable)
						{
							continue;
						}
						text = CS$<>8__locals1.$VB$Local_ModEntity.CustomizeTests();
					}
					try
					{
						if (File.Exists(text))
						{
							if (!File.Exists(CS$<>8__locals1.$VB$Local_ModEntity.Path))
							{
								ModBase.Log("[Mod] Mod 的状态已被切换", ModBase.LogLevel.Debug, "出现错误");
								continue;
							}
							if (Operators.CompareString(ModBase.smethod_0(CS$<>8__locals1.$VB$Local_ModEntity.Path), ModBase.smethod_0(text), false) != 0)
							{
								ModMain.MyMsgBox(string.Format("目前同时存在启用和禁用的两个 Mod 文件：{0} - {1}{2} - {3}{4}{5}注意，这两个文件的内容并不相同。{6}在手动删除或重命名其中一个文件后，才能继续操作。", new object[]
								{
									"\r\n",
									text,
									"\r\n",
									CS$<>8__locals1.$VB$Local_ModEntity.Path,
									"\r\n",
									"\r\n",
									"\r\n"
								}), "存在文件冲突", "确定", "", "", false, true, false, null, null, null);
								continue;
							}
						}
						File.Delete(text);
						Microsoft.VisualBasic.FileSystem.Rename(CS$<>8__locals1.$VB$Local_ModEntity.Path, text);
					}
					catch (FileNotFoundException ex)
					{
						ModBase.Log(ex, string.Format("未找到需要重命名的 Mod（{0}）", CS$<>8__locals1.$VB$Local_ModEntity.Path ?? "null"), ModBase.LogLevel.Feedback, "出现错误");
						this.ReloadModList(true);
						return;
					}
					catch (Exception ex2)
					{
						ModBase.Log(ex2, string.Format("重命名 Mod 失败（{0}）", CS$<>8__locals1.$VB$Local_ModEntity.Path ?? "null"), ModBase.LogLevel.Debug, "出现错误");
						flag = false;
					}
					ModMod.McMod mcMod = new ModMod.McMod(text);
					mcMod.FromJson(CS$<>8__locals1.$VB$Local_ModEntity.ToJson());
					if (ModMod.m_Record.Output.Contains(CS$<>8__locals1.$VB$Local_ModEntity))
					{
						int index = ModMod.m_Record.Output.IndexOf(CS$<>8__locals1.$VB$Local_ModEntity);
						ModMod.m_Record.Output.RemoveAt(index);
						ModMod.m_Record.Output.Insert(index, mcMod);
					}
					if (this._ProccesorConfig != null && this._ProccesorConfig.Contains(CS$<>8__locals1.$VB$Local_ModEntity))
					{
						int index2 = this._ProccesorConfig.IndexOf(CS$<>8__locals1.$VB$Local_ModEntity);
						this._ProccesorConfig.Remove(CS$<>8__locals1.$VB$Local_ModEntity);
						this._ProccesorConfig.Insert(index2, mcMod);
					}
					MyLocalModItem myLocalModItem = this.McModListItem(mcMod);
					this.tokenConfig[CS$<>8__locals1.$VB$Local_ModEntity.ConcatTests()] = myLocalModItem;
					int num = this.PanList.Children.IndexOf(Enumerable.FirstOrDefault<MyLocalModItem>(Enumerable.OfType<MyLocalModItem>(this.PanList.Children), (MyLocalModItem i) => i.ExcludeBroadcaster() == CS$<>8__locals1.$VB$Local_ModEntity));
					if (num != -1)
					{
						this.PanList.Children.RemoveAt(num);
						this.PanList.Children.Insert(num, myLocalModItem);
					}
				}
			}
			finally
			{
				List<ModMod.McMod>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			if (flag)
			{
				this.RefreshBars();
			}
			else
			{
				ModMain.Hint("由于文件被占用，Mod 的状态切换失败，请尝试关闭正在运行的游戏后再试！", ModMain.HintType.Critical, true);
				this.ReloadModList(true);
			}
			this.LoaderRun(ModLoader.LoaderFolderRunType.UpdateOnly);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0004D910 File Offset: 0x0004BB10
		private void BtnSelectUpdate_Click()
		{
			List<ModMod.McMod> list = Enumerable.ToList<ModMod.McMod>(Enumerable.Where<ModMod.McMod>(ModMod.m_Record.Output, (ModMod.McMod m) => this.m_WriterConfig.Contains(m.ConcatTests()) && m.SelectMapper()));
			if (Enumerable.Any<ModMod.McMod>(list))
			{
				this.UpdateMods(list);
				this.ChangeAllSelected(false);
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0004D954 File Offset: 0x0004BB54
		public void UpdateMods(IEnumerable<ModMod.McMod> ModList)
		{
			PageVersionMod._Closure$__38-0 CS$<>8__locals1 = new PageVersionMod._Closure$__38-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_ModList = ModList;
			if (Conversions.ToBoolean(Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("HintUpdateMod", null))) || Enumerable.Count<ModMod.McMod>(CS$<>8__locals1.$VB$Local_ModList) >= 15))
			{
				if (ModMain.MyMsgBox(string.Format("新版本 Mod 可能不兼容旧存档或者其他 Mod，这可能导致游戏崩溃，甚至永久损坏存档！{0}如果你在游玩整合包，请千万不要自行更新 Mod！{1}{2}在更新前，请先备份存档，并检查 Mod 的更新日志。{3}如果更新后出现问题，你也可以在回收站找回更新前的 Mod。", new object[]
				{
					"\r\n",
					"\r\n",
					"\r\n",
					"\r\n"
				}), "Mod 更新警告", "我已了解风险，继续更新", "取消", "", true, true, false, null, null, null) != 1)
				{
					return;
				}
				ModBase.m_IdentifierRepository.Set("HintUpdateMod", true, false, null);
			}
			try
			{
				PageVersionMod._Closure$__38-1 CS$<>8__locals2 = new PageVersionMod._Closure$__38-1(CS$<>8__locals2);
				CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2 = CS$<>8__locals1;
				CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_ModList = Enumerable.ToList<ModMod.McMod>(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_ModList);
				List<ModNet.NetFile> list = new List<ModNet.NetFile>();
				CS$<>8__locals2.$VB$Local_FileCopyList = new Dictionary<string, string>();
				try
				{
					foreach (ModMod.McMod mcMod in CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_ModList)
					{
						ModComp.CompFile updateFile = mcMod.UpdateFile;
						if (updateFile.ExcludeTests())
						{
							string text = mcMod._PropertyTest._BridgeRepository.Replace(".jar", "").Replace(".old", "").Replace(".disabled", "");
							string text2 = mcMod.UpdateFile._BridgeRepository.Replace(".jar", "").Replace(".old", "").Replace(".disabled", "");
							List<string> list2 = Enumerable.ToList<string>(text.Split(new char[]
							{
								'-'
							}));
							List<string> list3 = Enumerable.ToList<string>(text2.Split(new char[]
							{
								'-'
							}));
							bool flag = false;
							try
							{
								foreach (string item in Enumerable.ToList<string>(list2))
								{
									if (list3.Contains(item))
									{
										list2.Remove(item);
										list3.Remove(item);
										flag = true;
									}
								}
							}
							finally
							{
								List<string>.Enumerator enumerator2;
								((IDisposable)enumerator2).Dispose();
							}
							if (flag && Enumerable.Any<string>(list2) && Enumerable.Any<string>(list3))
							{
								text = list2.Join("-");
								text2 = list3.Join("-");
							}
							string text3 = ModBase.m_DecoratorRepository + "DownloadedMods\\" + mcMod.FileName.Replace(text, text2);
							string value = ModBase.GetPathFromFullPath(mcMod.Path) + mcMod.FileName.Replace(text, text2);
							list.Add(updateFile.ToNetFile(text3));
							CS$<>8__locals2.$VB$Local_FileCopyList[text3] = value;
						}
					}
				}
				finally
				{
					IEnumerator<ModMod.McMod> enumerator;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
				List<ModLoader.LoaderBase> list4 = new List<ModLoader.LoaderBase>();
				CS$<>8__locals2.$VB$Local_FinishedFileNames = new List<string>();
				list4.Add(new ModNet.LoaderDownload("下载新版 Mod 文件", list)
				{
					ProgressWeight = (double)Enumerable.Count<ModMod.McMod>(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_ModList) * 1.5
				});
				list4.Add(new ModLoader.LoaderTask<int, int>("替换旧版 Mod 文件", delegate(ModLoader.LoaderTask<int, int> a0)
				{
					base._Lambda$__0();
				}, null, ThreadPriority.Normal));
				CS$<>8__locals2.$VB$Local_Loader = new ModLoader.LoaderCombo<IEnumerable<ModMod.McMod>>("Mod 更新：" + PageVersionLeft._InstanceConfig.Name, list4);
				CS$<>8__locals2.$VB$Local_PathMods = PageVersionLeft._InstanceConfig.ChangeMapper() + "mods\\";
				CS$<>8__locals2.$VB$Local_Loader.OnStateChanged = delegate(ModLoader.LoaderBase a0)
				{
					base._Lambda$__1();
				};
				ModBase.Log(string.Format("[Mod] 开始更新 {0} 个 Mod：{1}", Enumerable.Count<ModMod.McMod>(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_ModList), CS$<>8__locals2.$VB$Local_PathMods), ModBase.LogLevel.Normal, "出现错误");
				PageVersionMod._RuleConfig.Add(CS$<>8__locals2.$VB$Local_PathMods);
				CS$<>8__locals2.$VB$Local_Loader.Start(null, false);
				ModLoader.LoaderTaskbarAdd<IEnumerable<ModMod.McMod>>(CS$<>8__locals2.$VB$Local_Loader);
				ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
				ModMain._ProcessIterator.BtnExtraDownload.Ribble();
				this.ReloadModList(true);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "初始化 Mod 更新失败", ModBase.LogLevel.Debug, "出现错误");
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00007A9F File Offset: 0x00005C9F
		private void BtnSelectDelete_Click()
		{
			this.DeleteMods(Enumerable.Where<ModMod.McMod>(ModMod.m_Record.Output, (ModMod.McMod m) => this.m_WriterConfig.Contains(m.ConcatTests())));
			this.ChangeAllSelected(false);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0004DDD4 File Offset: 0x0004BFD4
		private void DeleteMods(IEnumerable<ModMod.McMod> ModList)
		{
			try
			{
				bool flag = true;
				bool shiftKeyDown = MyWpfExtension.ManageParser().Keyboard.ShiftKeyDown;
				ModList = Enumerable.ToList<ModMod.McMod>(Enumerable.Select<string, ModMod.McMod>(Enumerable.Where<string>(Enumerable.Distinct<string>(Enumerable.SelectMany<ModMod.McMod, string>(ModList, (PageVersionMod._Closure$__.$I40-0 == null) ? (PageVersionMod._Closure$__.$I40-0 = delegate(ModMod.McMod Target)
				{
					IEnumerable<string> result;
					if (Target.State == ModMod.McMod.McModState.Fine)
					{
						result = new string[]
						{
							Target.Path,
							Target.Path + (File.Exists(Target.Path + ".old") ? ".old" : ".disabled")
						};
					}
					else
					{
						result = new string[]
						{
							Target.Path,
							Target.CustomizeTests()
						};
					}
					return result;
				}) : PageVersionMod._Closure$__.$I40-0)), (PageVersionMod._Closure$__.$I40-1 == null) ? (PageVersionMod._Closure$__.$I40-1 = ((string m) => File.Exists(m))) : PageVersionMod._Closure$__.$I40-1), (PageVersionMod._Closure$__.$I40-2 == null) ? (PageVersionMod._Closure$__.$I40-2 = ((string m) => new ModMod.McMod(m))) : PageVersionMod._Closure$__.$I40-2));
				try
				{
					IEnumerator<ModMod.McMod> enumerator = ModList.GetEnumerator();
					while (enumerator.MoveNext())
					{
						PageVersionMod._Closure$__40-0 CS$<>8__locals1 = new PageVersionMod._Closure$__40-0(CS$<>8__locals1);
						CS$<>8__locals1.$VB$Local_ModEntity = enumerator.Current;
						try
						{
							if (shiftKeyDown)
							{
								File.Delete(CS$<>8__locals1.$VB$Local_ModEntity.Path);
							}
							else
							{
								MyWpfExtension.ManageParser().FileSystem.DeleteFile(CS$<>8__locals1.$VB$Local_ModEntity.Path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
							}
						}
						catch (OperationCanceledException ex)
						{
							ModBase.Log(ex, "删除 Mod 被主动取消", ModBase.LogLevel.Debug, "出现错误");
							this.ReloadModList(true);
							return;
						}
						catch (Exception ex2)
						{
							ModBase.Log(ex2, string.Format("删除 Mod 失败（{0}）", CS$<>8__locals1.$VB$Local_ModEntity.Path), ModBase.LogLevel.Msgbox, "出现错误");
							flag = false;
						}
						this.m_WriterConfig.Remove(CS$<>8__locals1.$VB$Local_ModEntity.ConcatTests());
						ModMod.m_Record.Output.Remove(CS$<>8__locals1.$VB$Local_ModEntity);
						List<ModMod.McMod> proccesorConfig = this._ProccesorConfig;
						if (proccesorConfig != null)
						{
							proccesorConfig.Remove(CS$<>8__locals1.$VB$Local_ModEntity);
						}
						this.tokenConfig.Remove(CS$<>8__locals1.$VB$Local_ModEntity.ConcatTests());
						int num = this.PanList.Children.IndexOf(Enumerable.FirstOrDefault<MyLocalModItem>(Enumerable.OfType<MyLocalModItem>(this.PanList.Children), (MyLocalModItem i) => i.ExcludeBroadcaster().Equals(CS$<>8__locals1.$VB$Local_ModEntity)));
						if (num >= 0)
						{
							this.PanList.Children.RemoveAt(num);
						}
					}
				}
				finally
				{
					IEnumerator<ModMod.McMod> enumerator;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
				this.RefreshBars();
				if (!flag)
				{
					ModMain.Hint("由于文件被占用，Mod 删除失败，请尝试关闭正在运行的游戏后再试！", ModMain.HintType.Critical, true);
					this.ReloadModList(true);
				}
				else if (this.PanList.Children.Count == 0)
				{
					this.ReloadModList(true);
				}
				else
				{
					this.RefreshBars();
				}
				if (!flag)
				{
					return;
				}
				if (shiftKeyDown)
				{
					if (Enumerable.Count<ModMod.McMod>(ModList) == 1)
					{
						ModMain.Hint(string.Format("已彻底删除 {0}！", Enumerable.Single<ModMod.McMod>(ModList).FileName), ModMain.HintType.Finish, true);
					}
					else
					{
						ModMain.Hint(string.Format("已彻底删除 {0} 个文件！", Enumerable.Count<ModMod.McMod>(ModList)), ModMain.HintType.Finish, true);
					}
				}
				else if (Enumerable.Count<ModMod.McMod>(ModList) == 1)
				{
					ModMain.Hint(string.Format("已将 {0} 删除到回收站！", Enumerable.Single<ModMod.McMod>(ModList).FileName), ModMain.HintType.Finish, true);
				}
				else
				{
					ModMain.Hint(string.Format("已将 {0} 个文件删除到回收站！", Enumerable.Count<ModMod.McMod>(ModList)), ModMain.HintType.Finish, true);
				}
			}
			catch (OperationCanceledException ex3)
			{
				ModBase.Log(ex3, "删除 Mod 被主动取消", ModBase.LogLevel.Debug, "出现错误");
				this.ReloadModList(true);
			}
			catch (Exception ex4)
			{
				ModBase.Log(ex4, "删除 Mod 出现未知错误", ModBase.LogLevel.Feedback, "出现错误");
				this.ReloadModList(true);
			}
			this.LoaderRun(ModLoader.LoaderFolderRunType.UpdateOnly);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00007AC9 File Offset: 0x00005CC9
		private void BtnSelectCancel_Click()
		{
			this.ChangeAllSelected(false);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0004E194 File Offset: 0x0004C394
		public void Info_Click(object sender, EventArgs e)
		{
			checked
			{
				try
				{
					ModMod.McMod mcMod = ((MyLocalModItem)((sender is MyIconButton) ? NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) : sender)).ExcludeBroadcaster();
					if (mcMod.State == ModMod.McMod.McModState.Unavailable)
					{
						ModMain.MyMsgBox("无法读取此 Mod 的信息。\r\n\r\n详细的错误信息：" + ModBase.GetExceptionDetail(mcMod.SetupMapper(), false), "Mod 读取失败", "确定", "", "", false, true, false, null, null, null);
					}
					else if (mcMod.Comp != null)
					{
						ModMain._ProcessIterator.PageChange(new FormMain.PageStackData
						{
							initializerMap = FormMain.PageType.CompDetail,
							m_SingletonMap = new object[]
							{
								mcMod.Comp,
								new List<string>(),
								PageVersionLeft._InstanceConfig.Version._ConnectionMap,
								PageVersionLeft._InstanceConfig.Version.m_StructMap ? ModComp.CompModLoaderType.Forge : (PageVersionLeft._InstanceConfig.Version._ValMap ? ModComp.CompModLoaderType.NeoForge : (PageVersionLeft._InstanceConfig.Version._CandidateMap ? ModComp.CompModLoaderType.Fabric : ModComp.CompModLoaderType.Any))
							}
						}, FormMain.PageSubType.Default);
					}
					else
					{
						List<string> list = new List<string>();
						if (mcMod.Description != null)
						{
							list.Add(mcMod.Description + "\r\n");
						}
						if (mcMod.NewMapper() != null)
						{
							list.Add("作者：" + mcMod.NewMapper());
						}
						list.Add(string.Concat(new string[]
						{
							"文件：",
							mcMod.FileName,
							"（",
							ModBase.GetString(new FileInfo(mcMod.Path).Length),
							"）"
						}));
						if (mcMod.Version != null)
						{
							list.Add("版本：" + mcMod.Version);
						}
						List<string> list2 = new List<string>();
						if (mcMod.LoginMapper() != null)
						{
							list2.Add("Mod ID：" + mcMod.LoginMapper());
						}
						if (Enumerable.Any<KeyValuePair<string, string>>(mcMod.Dependencies))
						{
							list2.Add("依赖于：");
							try
							{
								foreach (KeyValuePair<string, string> keyValuePair in mcMod.Dependencies)
								{
									list2.Add(" - " + keyValuePair.Key + ((keyValuePair.Value == null) ? "" : ("，版本：" + keyValuePair.Value)));
								}
							}
							finally
							{
								Dictionary<string, string>.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
						}
						if (Enumerable.Any<string>(list2))
						{
							list.Add("");
							list.AddRange(list2);
						}
						string text = mcMod.Name.Replace(" ", "+");
						string text2 = text.Substring(0, 1);
						int num = Enumerable.Count<char>(text) - 1;
						for (int i = 1; i <= num; i++)
						{
							bool flag = text[i - 1].ToString().ToLower().Equals(text[i - 1].ToString());
							bool flag2 = text[i].ToString().ToLower().Equals(text[i].ToString());
							if (flag && !flag2)
							{
								text2 += "+";
							}
							text2 += Conversions.ToString(text[i]);
						}
						text2 = text2.Replace("++", "+").Replace("pti+Fine", "ptiFine");
						if (mcMod.AwakeMapper() == null)
						{
							if (ModMain.MyMsgBox(list.Join("\r\n"), mcMod.Name, "百科搜索", "返回", "", false, true, false, null, null, null) == 1)
							{
								ModBase.OpenWebsite("https://www.mcmod.cn/s?key=" + text2 + "&site=all&filter=0");
							}
						}
						else
						{
							int num2 = ModMain.MyMsgBox(list.Join("\r\n"), mcMod.Name, "打开官网", "百科搜索", "返回", false, true, false, null, null, null);
							if (num2 != 1)
							{
								if (num2 == 2)
								{
									ModBase.OpenWebsite("https://www.mcmod.cn/s?key=" + text2 + "&site=all&filter=0");
								}
							}
							else
							{
								ModBase.OpenWebsite(mcMod.AwakeMapper());
							}
						}
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "获取 Mod 详情失败", ModBase.LogLevel.Feedback, "出现错误");
				}
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0004E60C File Offset: 0x0004C80C
		public void Open_Click(MyIconButton sender, EventArgs e)
		{
			try
			{
				MyLocalModItem myLocalModItem = (MyLocalModItem)sender.Tag;
				ModBase.OpenExplorer("/select,\"" + myLocalModItem.ExcludeBroadcaster().Path + "\"");
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "打开 Mod 文件位置失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0004E678 File Offset: 0x0004C878
		public void Delete_Click(MyIconButton sender, EventArgs e)
		{
			MyLocalModItem myLocalModItem = (MyLocalModItem)sender.Tag;
			this.DeleteMods(new ModMod.McMod[]
			{
				myLocalModItem.ExcludeBroadcaster()
			});
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0004E6A8 File Offset: 0x0004C8A8
		public void ED_Click(MyIconButton sender, EventArgs e)
		{
			MyLocalModItem myLocalModItem = (MyLocalModItem)sender.Tag;
			this.EDMods(new ModMod.McMod[]
			{
				myLocalModItem.ExcludeBroadcaster()
			}, myLocalModItem.ExcludeBroadcaster().State == ModMod.McMod.McModState.Disabled);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00007AD2 File Offset: 0x00005CD2
		public bool InterruptReader()
		{
			return !string.IsNullOrWhiteSpace(this.SearchBox.Text);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0004E6E4 File Offset: 0x0004C8E4
		public void SearchRun()
		{
			if (this.InterruptReader())
			{
				List<ModBase.SearchEntry<ModMod.McMod>> list = new List<ModBase.SearchEntry<ModMod.McMod>>();
				try
				{
					foreach (ModMod.McMod mcMod in ModMod.m_Record.Output)
					{
						List<KeyValuePair<string, double>> list2 = new List<KeyValuePair<string, double>>();
						list2.Add(new KeyValuePair<string, double>(mcMod.Name, 1.0));
						list2.Add(new KeyValuePair<string, double>(mcMod.FileName, 1.0));
						if (mcMod.Version != null)
						{
							list2.Add(new KeyValuePair<string, double>(mcMod.Version, 0.2));
						}
						if (mcMod.Description != null && Operators.CompareString(mcMod.Description, "", false) != 0)
						{
							list2.Add(new KeyValuePair<string, double>(mcMod.Description, 0.4));
						}
						if (mcMod.Comp != null)
						{
							if (Operators.CompareString(mcMod.Comp.comparatorRepository, mcMod.Name, false) != 0)
							{
								list2.Add(new KeyValuePair<string, double>(mcMod.Comp.comparatorRepository, 1.0));
							}
							if (Operators.CompareString(mcMod.Comp.RemoveTests(), mcMod.Comp.comparatorRepository, false) != 0)
							{
								list2.Add(new KeyValuePair<string, double>(mcMod.Comp.RemoveTests(), 1.0));
							}
							if (Operators.CompareString(mcMod.Comp.mappingRepository, mcMod.Description, false) != 0)
							{
								list2.Add(new KeyValuePair<string, double>(mcMod.Comp.mappingRepository, 0.4));
							}
							list2.Add(new KeyValuePair<string, double>(string.Join("", mcMod.Comp.m_PoolRepository), 0.2));
						}
						list.Add(new ModBase.SearchEntry<ModMod.McMod>
						{
							m_DicError = mcMod,
							helperError = list2
						});
					}
				}
				finally
				{
					List<ModMod.McMod>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				this._ProccesorConfig = Enumerable.ToList<ModMod.McMod>(Enumerable.Select<ModBase.SearchEntry<ModMod.McMod>, ModMod.McMod>(ModBase.Search<ModMod.McMod>(list, this.SearchBox.Text, 6, 0.35), (PageVersionMod._Closure$__.$I49-0 == null) ? (PageVersionMod._Closure$__.$I49-0 = ((ModBase.SearchEntry<ModMod.McMod> r) => r.m_DicError)) : PageVersionMod._Closure$__.$I49-0));
			}
			this.RefreshUI();
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00007AE7 File Offset: 0x00005CE7
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x00007AEF File Offset: 0x00005CEF
		internal virtual Grid PanAllBack { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00007AF8 File Offset: 0x00005CF8
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x00007B00 File Offset: 0x00005D00
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00007B09 File Offset: 0x00005D09
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x00007B11 File Offset: 0x00005D11
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00007B1A File Offset: 0x00005D1A
		// (set) Token: 0x06000B24 RID: 2852 RVA: 0x0004E938 File Offset: 0x0004CB38
		internal virtual MySearchBox SearchBox
		{
			[CompilerGenerated]
			get
			{
				return this.m_ImporterConfig;
			}
			[CompilerGenerated]
			set
			{
				MySearchBox.TextChangedEventHandler obj = delegate(object sender, EventArgs e)
				{
					this.SearchRun();
				};
				MySearchBox importerConfig = this.m_ImporterConfig;
				if (importerConfig != null)
				{
					importerConfig.RegisterField(obj);
				}
				this.m_ImporterConfig = value;
				importerConfig = this.m_ImporterConfig;
				if (importerConfig != null)
				{
					importerConfig.ChangeField(obj);
				}
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00007B22 File Offset: 0x00005D22
		// (set) Token: 0x06000B26 RID: 2854 RVA: 0x00007B2A File Offset: 0x00005D2A
		internal virtual MyCard PanManage { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00007B33 File Offset: 0x00005D33
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x0004E97C File Offset: 0x0004CB7C
		internal virtual MyButton BtnManageOpen
		{
			[CompilerGenerated]
			get
			{
				return this.connectionConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnManageOpen_Click);
				MyButton myButton = this.connectionConfig;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.connectionConfig = value;
				myButton = this.connectionConfig;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00007B3B File Offset: 0x00005D3B
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x0004E9C0 File Offset: 0x0004CBC0
		internal virtual MyButton BtnManageInstall
		{
			[CompilerGenerated]
			get
			{
				return this.m_ServerConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnManageInstall_Click);
				MyButton serverConfig = this.m_ServerConfig;
				if (serverConfig != null)
				{
					serverConfig.Click -= value2;
				}
				this.m_ServerConfig = value;
				serverConfig = this.m_ServerConfig;
				if (serverConfig != null)
				{
					serverConfig.Click += value2;
				}
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x00007B43 File Offset: 0x00005D43
		// (set) Token: 0x06000B2C RID: 2860 RVA: 0x0004EA04 File Offset: 0x0004CC04
		internal virtual MyButton BtnManageDownload
		{
			[CompilerGenerated]
			get
			{
				return this.m_ResolverConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnManageDownload_Click);
				MyButton resolverConfig = this.m_ResolverConfig;
				if (resolverConfig != null)
				{
					resolverConfig.Click -= value2;
				}
				this.m_ResolverConfig = value;
				resolverConfig = this.m_ResolverConfig;
				if (resolverConfig != null)
				{
					resolverConfig.Click += value2;
				}
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00007B4B File Offset: 0x00005D4B
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x0004EA48 File Offset: 0x0004CC48
		internal virtual MyButton BtnManageSelectAll
		{
			[CompilerGenerated]
			get
			{
				return this.statusConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnManageSelectAll_Click);
				MyButton myButton = this.statusConfig;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.statusConfig = value;
				myButton = this.statusConfig;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00007B53 File Offset: 0x00005D53
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x00007B5B File Offset: 0x00005D5B
		internal virtual MyButton BtnManageCheck { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x00007B64 File Offset: 0x00005D64
		// (set) Token: 0x06000B32 RID: 2866 RVA: 0x00007B6C File Offset: 0x00005D6C
		internal virtual MyCard PanListBack { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00007B75 File Offset: 0x00005D75
		// (set) Token: 0x06000B34 RID: 2868 RVA: 0x00007B7D File Offset: 0x00005D7D
		internal virtual StackPanel PanFilter { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x00007B86 File Offset: 0x00005D86
		// (set) Token: 0x06000B36 RID: 2870 RVA: 0x0004EA8C File Offset: 0x0004CC8C
		internal virtual MyRadioButton BtnFilterAll
		{
			[CompilerGenerated]
			get
			{
				return this.m_ValConfig;
			}
			[CompilerGenerated]
			set
			{
				MyRadioButton.CheckEventHandler obj = delegate(object a0, bool a1)
				{
					this.ChangeFilter((MyRadioButton)a0, a1);
				};
				MyRadioButton valConfig = this.m_ValConfig;
				if (valConfig != null)
				{
					valConfig.ResolveTests(obj);
				}
				this.m_ValConfig = value;
				valConfig = this.m_ValConfig;
				if (valConfig != null)
				{
					valConfig.LogoutTests(obj);
				}
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00007B8E File Offset: 0x00005D8E
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x0004EAD0 File Offset: 0x0004CCD0
		internal virtual MyRadioButton BtnFilterEnabled
		{
			[CompilerGenerated]
			get
			{
				return this.m_AttrConfig;
			}
			[CompilerGenerated]
			set
			{
				MyRadioButton.CheckEventHandler obj = delegate(object a0, bool a1)
				{
					this.ChangeFilter((MyRadioButton)a0, a1);
				};
				MyRadioButton attrConfig = this.m_AttrConfig;
				if (attrConfig != null)
				{
					attrConfig.ResolveTests(obj);
				}
				this.m_AttrConfig = value;
				attrConfig = this.m_AttrConfig;
				if (attrConfig != null)
				{
					attrConfig.LogoutTests(obj);
				}
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00007B96 File Offset: 0x00005D96
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x0004EB14 File Offset: 0x0004CD14
		internal virtual MyRadioButton BtnFilterDisabled
		{
			[CompilerGenerated]
			get
			{
				return this.candidateConfig;
			}
			[CompilerGenerated]
			set
			{
				MyRadioButton.CheckEventHandler obj = delegate(object a0, bool a1)
				{
					this.ChangeFilter((MyRadioButton)a0, a1);
				};
				MyRadioButton myRadioButton = this.candidateConfig;
				if (myRadioButton != null)
				{
					myRadioButton.ResolveTests(obj);
				}
				this.candidateConfig = value;
				myRadioButton = this.candidateConfig;
				if (myRadioButton != null)
				{
					myRadioButton.LogoutTests(obj);
				}
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00007B9E File Offset: 0x00005D9E
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x0004EB58 File Offset: 0x0004CD58
		internal virtual MyRadioButton BtnFilterCanUpdate
		{
			[CompilerGenerated]
			get
			{
				return this.advisorConfig;
			}
			[CompilerGenerated]
			set
			{
				MyRadioButton.CheckEventHandler obj = delegate(object a0, bool a1)
				{
					this.ChangeFilter((MyRadioButton)a0, a1);
				};
				MyRadioButton myRadioButton = this.advisorConfig;
				if (myRadioButton != null)
				{
					myRadioButton.ResolveTests(obj);
				}
				this.advisorConfig = value;
				myRadioButton = this.advisorConfig;
				if (myRadioButton != null)
				{
					myRadioButton.LogoutTests(obj);
				}
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00007BA6 File Offset: 0x00005DA6
		// (set) Token: 0x06000B3E RID: 2878 RVA: 0x0004EB9C File Offset: 0x0004CD9C
		internal virtual MyRadioButton BtnFilterError
		{
			[CompilerGenerated]
			get
			{
				return this.accountConfig;
			}
			[CompilerGenerated]
			set
			{
				MyRadioButton.CheckEventHandler obj = delegate(object a0, bool a1)
				{
					this.ChangeFilter((MyRadioButton)a0, a1);
				};
				MyRadioButton myRadioButton = this.accountConfig;
				if (myRadioButton != null)
				{
					myRadioButton.ResolveTests(obj);
				}
				this.accountConfig = value;
				myRadioButton = this.accountConfig;
				if (myRadioButton != null)
				{
					myRadioButton.LogoutTests(obj);
				}
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00007BAE File Offset: 0x00005DAE
		// (set) Token: 0x06000B40 RID: 2880 RVA: 0x00007BB6 File Offset: 0x00005DB6
		internal virtual StackPanel PanList { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00007BBF File Offset: 0x00005DBF
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x00007BC7 File Offset: 0x00005DC7
		internal virtual MyCard PanEmpty { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00007BD0 File Offset: 0x00005DD0
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x0004EBE0 File Offset: 0x0004CDE0
		internal virtual MyButton BtnHintDownload
		{
			[CompilerGenerated]
			get
			{
				return this._ManagerConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnManageDownload_Click);
				MyButton managerConfig = this._ManagerConfig;
				if (managerConfig != null)
				{
					managerConfig.Click -= value2;
				}
				this._ManagerConfig = value;
				managerConfig = this._ManagerConfig;
				if (managerConfig != null)
				{
					managerConfig.Click += value2;
				}
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00007BD8 File Offset: 0x00005DD8
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x0004EC24 File Offset: 0x0004CE24
		internal virtual MyButton BtnHintInstall
		{
			[CompilerGenerated]
			get
			{
				return this._ModelConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnManageInstall_Click);
				MyButton modelConfig = this._ModelConfig;
				if (modelConfig != null)
				{
					modelConfig.Click -= value2;
				}
				this._ModelConfig = value;
				modelConfig = this._ModelConfig;
				if (modelConfig != null)
				{
					modelConfig.Click += value2;
				}
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x00007BE0 File Offset: 0x00005DE0
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x0004EC68 File Offset: 0x0004CE68
		internal virtual MyButton BtnHintOpen
		{
			[CompilerGenerated]
			get
			{
				return this.wrapperConfig;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnManageOpen_Click);
				MyButton myButton = this.wrapperConfig;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.wrapperConfig = value;
				myButton = this.wrapperConfig;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00007BE8 File Offset: 0x00005DE8
		// (set) Token: 0x06000B4A RID: 2890 RVA: 0x00007BF0 File Offset: 0x00005DF0
		internal virtual MyCard PanLoad { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x00007BF9 File Offset: 0x00005DF9
		// (set) Token: 0x06000B4C RID: 2892 RVA: 0x0004ECAC File Offset: 0x0004CEAC
		internal virtual MyLoading Load
		{
			[CompilerGenerated]
			get
			{
				return this.m_AttributeConfig;
			}
			[CompilerGenerated]
			set
			{
				MyLoading.ClickEventHandler value2 = new MyLoading.ClickEventHandler(this.Load_Click);
				MyLoading.StateChangedEventHandler obj = delegate(object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2)
				{
					this.UnselectedAllWithAnimation();
				};
				MyLoading attributeConfig = this.m_AttributeConfig;
				if (attributeConfig != null)
				{
					attributeConfig.Click -= value2;
					attributeConfig.InterruptField(obj);
				}
				this.m_AttributeConfig = value;
				attributeConfig = this.m_AttributeConfig;
				if (attributeConfig != null)
				{
					attributeConfig.Click += value2;
					attributeConfig.PrintField(obj);
				}
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00007C01 File Offset: 0x00005E01
		// (set) Token: 0x06000B4E RID: 2894 RVA: 0x00007C09 File Offset: 0x00005E09
		internal virtual MyCard CardSelect { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x00007C12 File Offset: 0x00005E12
		// (set) Token: 0x06000B50 RID: 2896 RVA: 0x00007C1A File Offset: 0x00005E1A
		internal virtual TranslateTransform TransSelect { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00007C23 File Offset: 0x00005E23
		// (set) Token: 0x06000B52 RID: 2898 RVA: 0x00007C2B File Offset: 0x00005E2B
		internal virtual TextBlock LabSelect { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x00007C34 File Offset: 0x00005E34
		// (set) Token: 0x06000B54 RID: 2900 RVA: 0x0004ED0C File Offset: 0x0004CF0C
		internal virtual MyIconTextButton BtnSelectUpdate
		{
			[CompilerGenerated]
			get
			{
				return this._InfoConfig;
			}
			[CompilerGenerated]
			set
			{
				MyIconTextButton.ClickEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.BtnSelectUpdate_Click();
				};
				MyIconTextButton infoConfig = this._InfoConfig;
				if (infoConfig != null)
				{
					infoConfig.Click -= value2;
				}
				this._InfoConfig = value;
				infoConfig = this._InfoConfig;
				if (infoConfig != null)
				{
					infoConfig.Click += value2;
				}
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x00007C3C File Offset: 0x00005E3C
		// (set) Token: 0x06000B56 RID: 2902 RVA: 0x0004ED50 File Offset: 0x0004CF50
		internal virtual MyIconTextButton BtnSelectEnable
		{
			[CompilerGenerated]
			get
			{
				return this.adapterConfig;
			}
			[CompilerGenerated]
			set
			{
				MyIconTextButton.ClickEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.BtnSelectED_Click((MyIconTextButton)sender, e);
				};
				MyIconTextButton myIconTextButton = this.adapterConfig;
				if (myIconTextButton != null)
				{
					myIconTextButton.Click -= value2;
				}
				this.adapterConfig = value;
				myIconTextButton = this.adapterConfig;
				if (myIconTextButton != null)
				{
					myIconTextButton.Click += value2;
				}
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00007C44 File Offset: 0x00005E44
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x0004ED94 File Offset: 0x0004CF94
		internal virtual MyIconTextButton BtnSelectDisable
		{
			[CompilerGenerated]
			get
			{
				return this.m_FacadeConfig;
			}
			[CompilerGenerated]
			set
			{
				MyIconTextButton.ClickEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.BtnSelectED_Click((MyIconTextButton)sender, e);
				};
				MyIconTextButton facadeConfig = this.m_FacadeConfig;
				if (facadeConfig != null)
				{
					facadeConfig.Click -= value2;
				}
				this.m_FacadeConfig = value;
				facadeConfig = this.m_FacadeConfig;
				if (facadeConfig != null)
				{
					facadeConfig.Click += value2;
				}
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x00007C4C File Offset: 0x00005E4C
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x0004EDD8 File Offset: 0x0004CFD8
		internal virtual MyIconTextButton BtnSelectDelete
		{
			[CompilerGenerated]
			get
			{
				return this.m_ListConfig;
			}
			[CompilerGenerated]
			set
			{
				MyIconTextButton.ClickEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.BtnSelectDelete_Click();
				};
				MyIconTextButton listConfig = this.m_ListConfig;
				if (listConfig != null)
				{
					listConfig.Click -= value2;
				}
				this.m_ListConfig = value;
				listConfig = this.m_ListConfig;
				if (listConfig != null)
				{
					listConfig.Click += value2;
				}
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00007C54 File Offset: 0x00005E54
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x0004EE1C File Offset: 0x0004D01C
		internal virtual MyIconTextButton BtnSelectCancel
		{
			[CompilerGenerated]
			get
			{
				return this._MerchantConfig;
			}
			[CompilerGenerated]
			set
			{
				MyIconTextButton.ClickEventHandler value2 = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.BtnSelectCancel_Click();
				};
				MyIconTextButton merchantConfig = this._MerchantConfig;
				if (merchantConfig != null)
				{
					merchantConfig.Click -= value2;
				}
				this._MerchantConfig = value;
				merchantConfig = this._MerchantConfig;
				if (merchantConfig != null)
				{
					merchantConfig.Click += value2;
				}
			}
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0004EE60 File Offset: 0x0004D060
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_AuthenticationConfig)
			{
				this.m_AuthenticationConfig = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageversion/pageversionmod.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0004EE90 File Offset: 0x0004D090
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
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
				this.SearchBox = (MySearchBox)target;
				return;
			}
			if (connectionId == 5)
			{
				this.PanManage = (MyCard)target;
				return;
			}
			if (connectionId == 6)
			{
				this.BtnManageOpen = (MyButton)target;
				return;
			}
			if (connectionId == 7)
			{
				this.BtnManageInstall = (MyButton)target;
				return;
			}
			if (connectionId == 8)
			{
				this.BtnManageDownload = (MyButton)target;
				return;
			}
			if (connectionId == 9)
			{
				this.BtnManageSelectAll = (MyButton)target;
				return;
			}
			if (connectionId == 10)
			{
				this.BtnManageCheck = (MyButton)target;
				return;
			}
			if (connectionId == 11)
			{
				this.PanListBack = (MyCard)target;
				return;
			}
			if (connectionId == 12)
			{
				this.PanFilter = (StackPanel)target;
				return;
			}
			if (connectionId == 13)
			{
				this.BtnFilterAll = (MyRadioButton)target;
				return;
			}
			if (connectionId == 14)
			{
				this.BtnFilterEnabled = (MyRadioButton)target;
				return;
			}
			if (connectionId == 15)
			{
				this.BtnFilterDisabled = (MyRadioButton)target;
				return;
			}
			if (connectionId == 16)
			{
				this.BtnFilterCanUpdate = (MyRadioButton)target;
				return;
			}
			if (connectionId == 17)
			{
				this.BtnFilterError = (MyRadioButton)target;
				return;
			}
			if (connectionId == 18)
			{
				this.PanList = (StackPanel)target;
				return;
			}
			if (connectionId == 19)
			{
				this.PanEmpty = (MyCard)target;
				return;
			}
			if (connectionId == 20)
			{
				this.BtnHintDownload = (MyButton)target;
				return;
			}
			if (connectionId == 21)
			{
				this.BtnHintInstall = (MyButton)target;
				return;
			}
			if (connectionId == 22)
			{
				this.BtnHintOpen = (MyButton)target;
				return;
			}
			if (connectionId == 23)
			{
				this.PanLoad = (MyCard)target;
				return;
			}
			if (connectionId == 24)
			{
				this.Load = (MyLoading)target;
				return;
			}
			if (connectionId == 25)
			{
				this.CardSelect = (MyCard)target;
				return;
			}
			if (connectionId == 26)
			{
				this.TransSelect = (TranslateTransform)target;
				return;
			}
			if (connectionId == 27)
			{
				this.LabSelect = (TextBlock)target;
				return;
			}
			if (connectionId == 28)
			{
				this.BtnSelectUpdate = (MyIconTextButton)target;
				return;
			}
			if (connectionId == 29)
			{
				this.BtnSelectEnable = (MyIconTextButton)target;
				return;
			}
			if (connectionId == 30)
			{
				this.BtnSelectDisable = (MyIconTextButton)target;
				return;
			}
			if (connectionId == 31)
			{
				this.BtnSelectDelete = (MyIconTextButton)target;
				return;
			}
			if (connectionId == 32)
			{
				this.BtnSelectCancel = (MyIconTextButton)target;
				return;
			}
			this.m_AuthenticationConfig = true;
		}

		// Token: 0x040005A1 RID: 1441
		private bool getterConfig;

		// Token: 0x040005A2 RID: 1442
		public Dictionary<string, MyLocalModItem> tokenConfig;

		// Token: 0x040005A3 RID: 1443
		private int expressionConfig;

		// Token: 0x040005A4 RID: 1444
		public List<string> m_WriterConfig;

		// Token: 0x040005A5 RID: 1445
		private PageVersionMod.FilterType m_RegistryConfig;

		// Token: 0x040005A6 RID: 1446
		public static List<string> _RuleConfig = new List<string>();

		// Token: 0x040005A7 RID: 1447
		private List<ModMod.McMod> _ProccesorConfig;

		// Token: 0x040005A8 RID: 1448
		[AccessedThroughProperty("PanAllBack")]
		[CompilerGenerated]
		private Grid setterConfig;

		// Token: 0x040005A9 RID: 1449
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyScrollViewer factoryConfig;

		// Token: 0x040005AA RID: 1450
		[AccessedThroughProperty("PanMain")]
		[CompilerGenerated]
		private StackPanel exporterConfig;

		// Token: 0x040005AB RID: 1451
		[AccessedThroughProperty("SearchBox")]
		[CompilerGenerated]
		private MySearchBox m_ImporterConfig;

		// Token: 0x040005AC RID: 1452
		[AccessedThroughProperty("PanManage")]
		[CompilerGenerated]
		private MyCard m_WorkerConfig;

		// Token: 0x040005AD RID: 1453
		[AccessedThroughProperty("BtnManageOpen")]
		[CompilerGenerated]
		private MyButton connectionConfig;

		// Token: 0x040005AE RID: 1454
		[AccessedThroughProperty("BtnManageInstall")]
		[CompilerGenerated]
		private MyButton m_ServerConfig;

		// Token: 0x040005AF RID: 1455
		[AccessedThroughProperty("BtnManageDownload")]
		[CompilerGenerated]
		private MyButton m_ResolverConfig;

		// Token: 0x040005B0 RID: 1456
		[AccessedThroughProperty("BtnManageSelectAll")]
		[CompilerGenerated]
		private MyButton statusConfig;

		// Token: 0x040005B1 RID: 1457
		[AccessedThroughProperty("BtnManageCheck")]
		[CompilerGenerated]
		private MyButton _RoleConfig;

		// Token: 0x040005B2 RID: 1458
		[AccessedThroughProperty("PanListBack")]
		[CompilerGenerated]
		private MyCard structConfig;

		// Token: 0x040005B3 RID: 1459
		[AccessedThroughProperty("PanFilter")]
		[CompilerGenerated]
		private StackPanel _PrinterConfig;

		// Token: 0x040005B4 RID: 1460
		[AccessedThroughProperty("BtnFilterAll")]
		[CompilerGenerated]
		private MyRadioButton m_ValConfig;

		// Token: 0x040005B5 RID: 1461
		[AccessedThroughProperty("BtnFilterEnabled")]
		[CompilerGenerated]
		private MyRadioButton m_AttrConfig;

		// Token: 0x040005B6 RID: 1462
		[AccessedThroughProperty("BtnFilterDisabled")]
		[CompilerGenerated]
		private MyRadioButton candidateConfig;

		// Token: 0x040005B7 RID: 1463
		[AccessedThroughProperty("BtnFilterCanUpdate")]
		[CompilerGenerated]
		private MyRadioButton advisorConfig;

		// Token: 0x040005B8 RID: 1464
		[CompilerGenerated]
		[AccessedThroughProperty("BtnFilterError")]
		private MyRadioButton accountConfig;

		// Token: 0x040005B9 RID: 1465
		[CompilerGenerated]
		[AccessedThroughProperty("PanList")]
		private StackPanel queueConfig;

		// Token: 0x040005BA RID: 1466
		[AccessedThroughProperty("PanEmpty")]
		[CompilerGenerated]
		private MyCard m_EventConfig;

		// Token: 0x040005BB RID: 1467
		[CompilerGenerated]
		[AccessedThroughProperty("BtnHintDownload")]
		private MyButton _ManagerConfig;

		// Token: 0x040005BC RID: 1468
		[CompilerGenerated]
		[AccessedThroughProperty("BtnHintInstall")]
		private MyButton _ModelConfig;

		// Token: 0x040005BD RID: 1469
		[AccessedThroughProperty("BtnHintOpen")]
		[CompilerGenerated]
		private MyButton wrapperConfig;

		// Token: 0x040005BE RID: 1470
		[CompilerGenerated]
		[AccessedThroughProperty("PanLoad")]
		private MyCard _BaseConfig;

		// Token: 0x040005BF RID: 1471
		[CompilerGenerated]
		[AccessedThroughProperty("Load")]
		private MyLoading m_AttributeConfig;

		// Token: 0x040005C0 RID: 1472
		[CompilerGenerated]
		[AccessedThroughProperty("CardSelect")]
		private MyCard _CodeConfig;

		// Token: 0x040005C1 RID: 1473
		[CompilerGenerated]
		[AccessedThroughProperty("TransSelect")]
		private TranslateTransform prototypeConfig;

		// Token: 0x040005C2 RID: 1474
		[AccessedThroughProperty("LabSelect")]
		[CompilerGenerated]
		private TextBlock _AnnotationConfig;

		// Token: 0x040005C3 RID: 1475
		[AccessedThroughProperty("BtnSelectUpdate")]
		[CompilerGenerated]
		private MyIconTextButton _InfoConfig;

		// Token: 0x040005C4 RID: 1476
		[CompilerGenerated]
		[AccessedThroughProperty("BtnSelectEnable")]
		private MyIconTextButton adapterConfig;

		// Token: 0x040005C5 RID: 1477
		[AccessedThroughProperty("BtnSelectDisable")]
		[CompilerGenerated]
		private MyIconTextButton m_FacadeConfig;

		// Token: 0x040005C6 RID: 1478
		[AccessedThroughProperty("BtnSelectDelete")]
		[CompilerGenerated]
		private MyIconTextButton m_ListConfig;

		// Token: 0x040005C7 RID: 1479
		[AccessedThroughProperty("BtnSelectCancel")]
		[CompilerGenerated]
		private MyIconTextButton _MerchantConfig;

		// Token: 0x040005C8 RID: 1480
		private bool m_AuthenticationConfig;

		// Token: 0x02000110 RID: 272
		private enum FilterType
		{
			// Token: 0x040005CA RID: 1482
			All,
			// Token: 0x040005CB RID: 1483
			Enabled,
			// Token: 0x040005CC RID: 1484
			Disabled,
			// Token: 0x040005CD RID: 1485
			CanUpdate,
			// Token: 0x040005CE RID: 1486
			Unavailable
		}
	}
}
