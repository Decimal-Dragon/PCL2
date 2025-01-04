using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x02000197 RID: 407
	[DesignerGenerated]
	public class PageLaunchLeft : MyPageLeft, IComponentConnector
	{
		// Token: 0x0600104C RID: 4172 RVA: 0x00077C70 File Offset: 0x00075E70
		public PageLaunchLeft()
		{
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.PageLaunchLeft_Loaded();
			};
			this.attributeMapper = false;
			this.m_CodeMapper = false;
			this.m_PrototypeMapper = PageLaunchLeft.PageType.None;
			this.m_MerchantMapper = 0;
			this._AuthenticationMapper = null;
			this.algoMapper = 0.0;
			this.m_ComparatorMapper = false;
			this.m_TokenizerMapper = false;
			this.InitializeComponent();
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00077CDC File Offset: 0x00075EDC
		public void PageLaunchLeft_Loaded()
		{
			if (this.attributeMapper)
			{
				this.RefreshPage(true, false);
			}
			this.AprilPosTrans.X = 0.0;
			this.AprilPosTrans.Y = 0.0;
			checked
			{
				if (!this.attributeMapper)
				{
					this.attributeMapper = true;
					ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
					ModMinecraft._ListenerTests.LoadingStateChanged += delegate(MyLoading.MyLoadingState a0, MyLoading.MyLoadingState a1)
					{
						this.RefreshButtonsUI();
					};
					ModMinecraft.m_CreatorTests.LoadingStateChanged += delegate(MyLoading.MyLoadingState a0, MyLoading.MyLoadingState a1)
					{
						this.RefreshButtonsUI();
					};
					this.RefreshButtonsUI();
					ModBase.RunInNewThread(delegate
					{
						string text = null;
						if (File.Exists(ModBase.Path + "modpack.zip"))
						{
							text = ModBase.Path + "modpack.zip";
						}
						if (File.Exists(ModBase.Path + "modpack.mrpack"))
						{
							text = ModBase.Path + "modpack.mrpack";
						}
						if (text != null)
						{
							ModBase.Log("[Launch] 需自动安装整合包：" + text, ModBase.LogLevel.Debug, "出现错误");
							ModBase.m_IdentifierRepository.Set("LaunchFolderSelect", "$.minecraft\\", false, null);
							if (!Directory.Exists(ModBase.Path + ".minecraft\\"))
							{
								Directory.CreateDirectory(ModBase.Path + ".minecraft\\");
								Directory.CreateDirectory(ModBase.Path + ".minecraft\\versions\\");
								ModMinecraft.McFolderLauncherProfilesJsonCreate(ModBase.Path + ".minecraft\\");
							}
							PageSelectLeft.AddFolder(ModBase.Path + ".minecraft\\", ModBase.GetFolderNameFromPath(ModBase.Path), false);
							ModMinecraft.m_CreatorTests.WaitForExit(null, null, false);
						}
						ModMinecraft.m_ProxyTests = ModBase.m_IdentifierRepository.Get("LaunchFolderSelect", null).ToString().Replace("$", ModBase.Path);
						if (Operators.CompareString(ModMinecraft.m_ProxyTests, "", false) == 0 || !Directory.Exists(ModMinecraft.m_ProxyTests))
						{
							if (Operators.CompareString(ModMinecraft.m_ProxyTests, "", false) == 0)
							{
								ModBase.Log("[Launch] 没有已储存的 Minecraft 文件夹", ModBase.LogLevel.Normal, "出现错误");
							}
							else
							{
								ModBase.Log("[Launch] Minecraft 文件夹无效，该文件夹已不存在：" + ModMinecraft.m_ProxyTests, ModBase.LogLevel.Debug, "出现错误");
							}
							ModMinecraft.m_CreatorTests.WaitForExit(null, null, true);
							ModBase.m_IdentifierRepository.Set("LaunchFolderSelect", ModMinecraft.messageTests[0].Path.Replace(ModBase.Path, "$"), false, null);
						}
						ModBase.Log("[Launch] Minecraft 文件夹：" + ModMinecraft.m_ProxyTests, ModBase.LogLevel.Normal, "出现错误");
						if (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("SystemDebugDelay", null)))
						{
							Thread.Sleep(ModBase.RandomInteger(500, 3000));
						}
						if (text != null)
						{
							try
							{
								ModLoader.LoaderCombo<string> loaderCombo = ModModpack.ModpackInstall(text, ModBase.GetFolderNameFromPath(ModBase.Path), null);
								ModBase.Log("[Launch] 自动安装整合包已开始：" + text, ModBase.LogLevel.Normal, "出现错误");
								loaderCombo.WaitForExit(null, null, false);
								if (loaderCombo.State == ModBase.LoadState.Finished)
								{
									ModBase.Log("[Launch] 自动安装整合包成功，删除安装包：" + text, ModBase.LogLevel.Normal, "出现错误");
									File.Delete(text);
								}
							}
							catch (ModBase.CancelledException ex)
							{
								ModBase.Log(ex, "自动安装整合包被用户取消：" + text, ModBase.LogLevel.Debug, "出现错误");
							}
							catch (Exception ex2)
							{
								ModBase.Log(ex2, "自动安装整合包失败：" + text, ModBase.LogLevel.Msgbox, "出现错误");
							}
						}
						string text2 = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchVersionSelect", null));
						ModMinecraft.McVersion Version = (Operators.CompareString(text2, "", false) == 0) ? null : new ModMinecraft.McVersion(text2);
						if (Version == null || !Version.Path.StartsWithF(ModMinecraft.m_ProxyTests, false) || !Version.Check())
						{
							ModBase.Log("[Launch] 当前选择的 Minecraft 版本无效：" + ((Version == null) ? "null" : Version.Path), Information.IsNothing(Version) ? ModBase.LogLevel.Normal : ModBase.LogLevel.Debug, "出现错误");
							if (ModMinecraft._ListenerTests.State != ModBase.LoadState.Finished)
							{
								ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", true);
							}
							if (Enumerable.Any<KeyValuePair<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>>>(ModMinecraft._RegTests) && !Enumerable.First<KeyValuePair<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>>>(ModMinecraft._RegTests).Value[0].getterMap.Contains("RedstoneBlock"))
							{
								Version = Enumerable.First<KeyValuePair<ModMinecraft.McVersionCardType, List<ModMinecraft.McVersion>>>(ModMinecraft._RegTests).Value[0];
								ModBase.m_IdentifierRepository.Set("LaunchVersionSelect", Version.Name, false, null);
								ModBase.Log("[Launch] 自动选择 Minecraft 版本：" + Version.Path, ModBase.LogLevel.Normal, "出现错误");
							}
							else
							{
								Version = null;
								ModBase.m_IdentifierRepository.Set("LaunchVersionSelect", "", false, null);
								ModBase.Log("[Launch] 无可用 Minecraft 版本", ModBase.LogLevel.Normal, "出现错误");
							}
						}
						ModBase.RunInUi(delegate()
						{
							ModMinecraft.InstantiateClient(Version);
							this.m_CodeMapper = true;
							this.RefreshButtonsUI();
							this.RefreshPage(false, false);
							if (Operators.CompareString(ModLaunch.McLoginAble(), "", false) == 0)
							{
								ModLaunch.interceptorTests.Start(null, false);
							}
						}, false);
					}, "Version Check", ThreadPriority.AboveNormal);
					ModLaunch.McLoginType mcLoginType = (ModLaunch.McLoginType)Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("LoginType", null));
					if (mcLoginType == ModLaunch.McLoginType.Legacy || mcLoginType == ModLaunch.McLoginType.Ms)
					{
						((MyRadioButton)base.FindName("RadioLoginType" + Conversions.ToString((int)mcLoginType))).Checked = true;
					}
					this.RefreshPage(false, false);
					ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
				}
			}
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00077DDC File Offset: 0x00075FDC
		public void PageChangeToLaunching()
		{
			object left = ModBase.m_IdentifierRepository.Get("LoginType", null);
			if (Operators.ConditionalCompareObjectEqual(left, ModLaunch.McLoginType.Legacy, false))
			{
				if (PageLinkHiper.EnableReader() == ModBase.LoadState.Finished)
				{
					this.LabLaunchingMethod.Text = "联机离线登录";
				}
				else
				{
					this.LabLaunchingMethod.Text = "离线登录";
				}
			}
			else if (Operators.ConditionalCompareObjectEqual(left, ModLaunch.McLoginType.Ms, false))
			{
				this.LabLaunchingMethod.Text = "正版登录";
			}
			else if (Operators.ConditionalCompareObjectEqual(left, ModLaunch.McLoginType.Nide, false))
			{
				this.LabLaunchingMethod.Text = "统一通行证";
			}
			else if (Operators.ConditionalCompareObjectEqual(left, ModLaunch.McLoginType.Auth, false))
			{
				this.LabLaunchingMethod.Text = "Authlib-Injector";
			}
			this.LabLaunchingName.Text = ModMinecraft.AddClient().Name;
			this.LabLaunchingStage.Text = "初始化";
			TextBlock labLaunchingTitle = this.LabLaunchingTitle;
			ModLaunch.McLaunchOptions filterTests = ModLaunch.filterTests;
			labLaunchingTitle.Text = ((((filterTests != null) ? filterTests.requestMap : null) == null) ? "正在启动游戏" : "正在导出启动脚本");
			this.LabLaunchingProgress.Text = "0.00 %";
			this.LabLaunchingProgress.Opacity = 1.0;
			this.LabLaunchingDownload.Visibility = Visibility.Visible;
			this.LabLaunchingProgressLeft.Opacity = 0.6;
			this.LabLaunchingDownload.Visibility = Visibility.Visible;
			this.LabLaunchingDownload.Text = "0 B/s";
			this.LabLaunchingDownload.Opacity = 0.0;
			this.LabLaunchingDownload.Visibility = Visibility.Collapsed;
			this.LabLaunchingDownloadLeft.Opacity = 0.0;
			this.LabLaunchingDownloadLeft.Visibility = Visibility.Collapsed;
			this.ProgressLaunchingFinished.Width = new GridLength(0.0, GridUnitType.Star);
			this.ProgressLaunchingUnfinished.Width = new GridLength(1.0, GridUnitType.Star);
			this.PanLaunchingHint.Opacity = 0.0;
			this.PanLaunchingHint.Visibility = Visibility.Collapsed;
			this.PanLaunchingInfo.Width = double.NaN;
			ModLaunch._PoolTests = null;
			ModLaunch.customerTests = null;
			this.LabLaunchingHint.Text = PageOtherTest.GetRandomHint();
			this.PanInput.IsHitTestVisible = false;
			this.PanLaunching.IsHitTestVisible = false;
			this.LoadLaunching.State.LoadingState = MyLoading.MyLoadingState.Run;
			this.PanLaunching.Visibility = Visibility.Visible;
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaOpacity(this.PanInput, 0.0, 50, 0, null, false),
				ModAnimation.AaOpacity(this.PanInput, -this.PanInput.Opacity, 110, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), true),
				ModAnimation.AaScaleTransform(this.PanInput, 1.2 - ((ScaleTransform)this.PanInput.RenderTransform).ScaleX, 160, 0, null, false),
				ModAnimation.AaOpacity(this.PanLaunching, 1.0 - this.PanLaunching.Opacity, 150, 100, null, false),
				ModAnimation.AaScaleTransform(this.PanLaunching, 1.0 - ((ScaleTransform)this.PanLaunching.RenderTransform).ScaleX, 500, 100, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false),
				ModAnimation.AaCode(delegate
				{
					this.PanLaunching.IsHitTestVisible = true;
				}, 150, false)
			}, "Launch State Page", false);
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00078160 File Offset: 0x00076360
		public void PageChangeToLogin()
		{
			NewLateBinding.LateCall(this.PageGet(this.m_PrototypeMapper), null, "Reload", new object[]
			{
				false
			}, new string[]
			{
				"KeepInput"
			}, null, null, true);
			this.PanInput.IsHitTestVisible = false;
			this.PanLaunching.IsHitTestVisible = false;
			this.LoadLaunching.State.LoadingState = MyLoading.MyLoadingState.Stop;
			this.PanInput.Visibility = Visibility.Visible;
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaOpacity(this.PanLaunching, -this.PanLaunching.Opacity, 150, 0, null, false),
				ModAnimation.AaScaleTransform(this.PanLaunching, 0.8 - ((ScaleTransform)this.PanLaunching.RenderTransform).ScaleX, 150, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false),
				ModAnimation.AaOpacity(this.PanInput, 1.0 - this.PanInput.Opacity, 250, 50, null, false),
				ModAnimation.AaScaleTransform(this.PanInput, 1.0 - ((ScaleTransform)this.PanInput.RenderTransform).ScaleX, 300, 50, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false),
				ModAnimation.AaCode(delegate
				{
					this.PanInput.IsHitTestVisible = true;
				}, 200, false)
			}, "Launch State Page", true);
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000782DC File Offset: 0x000764DC
		private object PageGet(PageLaunchLeft.PageType Type)
		{
			object result;
			switch (Type)
			{
			case PageLaunchLeft.PageType.Legacy:
				if (Information.IsNothing(ModMain.m_ProcIterator))
				{
					ModMain.m_ProcIterator = new PageLoginLegacy();
				}
				result = ModMain.m_ProcIterator;
				break;
			case PageLaunchLeft.PageType.Nide:
				if (Information.IsNothing(ModMain.parserRepository))
				{
					ModMain.parserRepository = new PageLoginNide();
				}
				result = ModMain.parserRepository;
				break;
			case PageLaunchLeft.PageType.NideSkin:
				if (Information.IsNothing(ModMain.broadcasterRepository))
				{
					ModMain.broadcasterRepository = new PageLoginNideSkin();
				}
				result = ModMain.broadcasterRepository;
				break;
			case PageLaunchLeft.PageType.Auth:
				if (Information.IsNothing(ModMain.fieldRepository))
				{
					ModMain.fieldRepository = new PageLoginAuth();
				}
				result = ModMain.fieldRepository;
				break;
			case PageLaunchLeft.PageType.AuthSkin:
				if (Information.IsNothing(ModMain.readerRepository))
				{
					ModMain.readerRepository = new PageLoginAuthSkin();
				}
				result = ModMain.readerRepository;
				break;
			case PageLaunchLeft.PageType.Ms:
				if (Information.IsNothing(ModMain.m_ClientRepository))
				{
					ModMain.m_ClientRepository = new PageLoginMs();
				}
				result = ModMain.m_ClientRepository;
				break;
			case PageLaunchLeft.PageType.MsSkin:
				if (Information.IsNothing(ModMain.m_ConfigRepository))
				{
					ModMain.m_ConfigRepository = new PageLoginMsSkin();
				}
				result = ModMain.m_ConfigRepository;
				break;
			default:
				throw new ArgumentOutOfRangeException("Type", "即将切换的登录分页编号越界");
			}
			return result;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x000783F8 File Offset: 0x000765F8
		private object PageChange(PageLaunchLeft.PageType Type, bool Anim)
		{
			PageLoginMs PageNew = ModMain.m_ClientRepository;
			checked
			{
				object $VB$Local_PageNew;
				try
				{
					if (this.m_PrototypeMapper == Type)
					{
						$VB$Local_PageNew = PageNew;
					}
					else
					{
						PageNew = RuntimeHelpers.GetObjectValue(this.PageGet(Type));
						ModAnimation.AniStop("FrmLogin PageChange");
						if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(PageNew)) && !Information.IsNothing(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(PageNew, null, "Parent", new object[0], null, null, null))))
						{
							object $VB$Local_PageNew2 = PageNew;
							Type type = null;
							string memberName = "SetValue";
							object[] array = new object[2];
							array[0] = ContentPresenter.ContentProperty;
							NewLateBinding.LateCall($VB$Local_PageNew2, type, memberName, array, null, null, null, true);
						}
						if (Anim)
						{
							ThreadStart $I1;
							base.Dispatcher.Invoke(delegate()
							{
								ModAnimation.AniStart(new ModAnimation.AniData[]
								{
									ModAnimation.AaOpacity(this.PanLogin, unchecked(-this.PanLogin.Opacity), 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
									ModAnimation.AaCode(($I1 == null) ? ($I1 = delegate()
									{
										ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
										this.PanLogin.Children.Clear();
										this.PanLogin.Children.Add((UIElement)PageNew);
										ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
									}) : $I1, 100, false),
									ModAnimation.AaOpacity(this.PanLogin, 1.0, 100, 120, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false)
								}, "FrmLogin PageChange", false);
							}, DispatcherPriority.Render);
						}
						else
						{
							ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
							this.PanLogin.Children.Clear();
							this.PanLogin.Children.Add((UIElement)PageNew);
							ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
						}
						this.m_PrototypeMapper = Type;
						$VB$Local_PageNew = PageNew;
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "切换登录分页失败（" + ModBase.GetStringFromEnum(Type) + "）", ModBase.LogLevel.Feedback, "出现错误");
					$VB$Local_PageNew = PageNew;
				}
				return $VB$Local_PageNew;
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00078568 File Offset: 0x00076768
		public void RefreshPage(bool KeepInput, bool Anim)
		{
			int num;
			if (ModMinecraft.AddClient() != null)
			{
				num = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("VersionServerLogin", ModMinecraft.AddClient()));
				ModBase.m_IdentifierRepository.Set("LoginPageType", num, false, null);
			}
			else
			{
				num = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("LoginPageType", null));
			}
			PageLaunchLeft.PageType pageType;
			switch (num)
			{
			case 0:
				break;
			case 1:
				if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("CacheMsV2Access", null), "", false))
				{
					pageType = PageLaunchLeft.PageType.Ms;
				}
				else
				{
					pageType = PageLaunchLeft.PageType.MsSkin;
				}
				ModBase.m_IdentifierRepository.Set("LoginType", ModLaunch.McLoginType.Ms, false, null);
				this.PanType.Visibility = Visibility.Collapsed;
				this.PanTypeOne.Visibility = Visibility.Visible;
				this.PathTypeOne.Data = (Geometry)new GeometryConverter().ConvertFromString("M511.488256 95.184408c35.310345 22.516742 95.184408 55.78011 167.34033 84.437781 75.738131 29.681159 148.405797 40.93953 191.392304 45.033483v353.615193c0 73.691154-50.662669 164.781609-136.123938 244.101949C649.65917 901.181409 558.568716 942.12094 512 942.12094c-46.568716 0-137.65917-40.93953-222.096952-119.748126C204.441779 742.54073 153.77911 651.450275 153.77911 577.247376v-353.103448c42.474763-4.093953 116.165917-15.352324 191.904048-45.545227 75.226387-30.192904 133.565217-63.456272 165.805098-83.414293M512 0c-4.093953 0-8.187906 1.535232-11.258371 3.582209l-14.84058 10.234882c-1.023488 0.511744-67.550225 47.592204-170.410794 88.531735-100.813593 39.916042-198.556722 41.963018-199.58021 41.963018l-25.075462 0.511744c-10.746627 0.511744-18.934533 8.187906-18.934533 18.422789v414.000999c0 216.97951 286.064968 446.24088 440.09995 446.24088s440.09995-229.261369 440.09995-445.729136V163.758121c0-10.234883-8.69965-18.422789-18.934533-18.422789l-24.563718-0.511744c-1.023488 0-98.766617-2.046977-199.58021-41.963018-103.372314-40.93953-170.410795-88.01999-170.922538-88.531734L523.258371 3.582209c-3.070465-2.558721-7.164418-3.582209-11.258371-3.582209z M743.308346 410.930535l-260.477761 260.477761c-15.864068 15.864068-41.963018 15.864068-57.827087 0l-144.823588-144.823588c-15.864068-15.864068-15.864068-41.963018 0-57.827087 8.187906-8.187906 18.422789-11.770115 29.169415-11.770115 10.234883 0 20.981509 4.093953 29.169416 11.770115l115.654173 115.654173L685.993003 352.591704c15.864068-15.864068 41.963018-15.864068 57.827087 0 15.352324 16.375812 15.352324 42.474763-0.511744 58.338831z");
				this.LabTypeOne.Text = "正版登录";
				this.RadioLoginType5.Visibility = Visibility.Visible;
				this.RadioLoginType0.Visibility = Visibility.Collapsed;
				goto IL_37B;
			case 2:
				pageType = PageLaunchLeft.PageType.Legacy;
				ModBase.m_IdentifierRepository.Set("LoginType", ModLaunch.McLoginType.Legacy, false, null);
				this.PanType.Visibility = Visibility.Collapsed;
				this.PanTypeOne.Visibility = Visibility.Visible;
				this.PathTypeOne.Data = (Geometry)new GeometryConverter().ConvertFromString("M533.293176 788.841412a60.235294 60.235294 0 1 1 85.202824 85.202823l-42.616471 42.586353c-129.355294 129.385412-339.124706 129.385412-468.510117 0-129.385412-129.385412-129.385412-339.124706 0-468.510117l42.586353-42.616471a60.235294 60.235294 0 1 1 85.202823 85.202824l-42.61647 42.586352a210.823529 210.823529 0 1 0 298.164706 298.164706l42.586352-42.61647z m255.548236-255.548236l42.61647-42.586352a210.823529 210.823529 0 1 0-298.164706-298.164706l-42.586352 42.61647a60.235294 60.235294 0 1 1-85.202824-85.202823l42.616471-42.586353c129.355294-129.385412 339.124706-129.385412 468.510117 0 129.385412 129.385412 129.385412 339.124706 0 468.510117l-42.586353 42.616471a60.235294 60.235294 0 1 1-85.202823-85.202824zM192.542118 192.542118a60.235294 60.235294 0 0 1 85.202823 0l553.712941 553.712941a60.235294 60.235294 0 0 1-85.202823 85.202823L192.542118 277.744941a60.235294 60.235294 0 0 1 0-85.202823z");
				this.LabTypeOne.Text = "离线登录";
				goto IL_37B;
			case 3:
				if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("CacheNideAccess", null), "", false))
				{
					pageType = PageLaunchLeft.PageType.Nide;
				}
				else
				{
					pageType = PageLaunchLeft.PageType.NideSkin;
				}
				ModBase.m_IdentifierRepository.Set("LoginType", ModLaunch.McLoginType.Nide, false, null);
				this.PanType.Visibility = Visibility.Collapsed;
				this.PanTypeOne.Visibility = Visibility.Visible;
				this.PathTypeOne.Data = (Geometry)new GeometryConverter().ConvertFromString("M834.5 684.1c-31.2-70.4-98.9-120.9-179.1-127.3 63.5-8.5 112.6-63 112.6-128.8 0-71.8-58.2-130-130-130s-130 58.2-130 130c0 65.9 49 120.3 112.6 128.8-80.2 6.4-148 57-179.1 127.3-8.7 19.7 6 42 27.6 42 12.1 0 22.7-7.5 27.7-18.5 24.3-53.9 78.5-91.5 141.3-91.5s117 37.6 141.3 91.5c5 11.1 15.6 18.5 27.7 18.5 21.4 0 36.1-22.3 27.4-42zM567.9 427.9c0-38.6 31.4-70 70-70s70 31.4 70 70-31.4 70-70 70-70-31.4-70-70zM460.3 347.9H216.9c-16.6 0-30 13.4-30 30s13.4 30 30 30h243.3c16.6 0 30-13.4 30-30 0.1-16.5-13.4-30-29.9-30zM367.4 459.6H216.9c-16.6 0-30 13.4-30 30s13.4 30 30 30h150.4c16.6 0 30-13.4 30-30 0.1-16.6-13.4-30-29.9-30zM297.4 571.2H217c-16.6 0-30 13.4-30 30s13.4 30 30 30h80.4c16.6 0 30-13.4 30-30 0-16.5-13.5-30-30-30zM900 236v552H124V236h776m0-60H124c-33.1 0-60 26.9-60 60v552c0 33.1 26.9 60 60 60h776c33.1 0 60-26.9 60-60V236c0-33.1-26.9-60-60-60z");
				this.LabTypeOne.Text = "统一通行证登录";
				goto IL_37B;
			case 4:
				if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("CacheAuthAccess", null), "", false))
				{
					pageType = PageLaunchLeft.PageType.Auth;
				}
				else
				{
					pageType = PageLaunchLeft.PageType.AuthSkin;
				}
				ModBase.m_IdentifierRepository.Set("LoginType", ModLaunch.McLoginType.Auth, false, null);
				this.PanType.Visibility = Visibility.Collapsed;
				this.PanTypeOne.Visibility = Visibility.Visible;
				this.PathTypeOne.Data = (Geometry)new GeometryConverter().ConvertFromString("M834.5 684.1c-31.2-70.4-98.9-120.9-179.1-127.3 63.5-8.5 112.6-63 112.6-128.8 0-71.8-58.2-130-130-130s-130 58.2-130 130c0 65.9 49 120.3 112.6 128.8-80.2 6.4-148 57-179.1 127.3-8.7 19.7 6 42 27.6 42 12.1 0 22.7-7.5 27.7-18.5 24.3-53.9 78.5-91.5 141.3-91.5s117 37.6 141.3 91.5c5 11.1 15.6 18.5 27.7 18.5 21.4 0 36.1-22.3 27.4-42zM567.9 427.9c0-38.6 31.4-70 70-70s70 31.4 70 70-31.4 70-70 70-70-31.4-70-70zM460.3 347.9H216.9c-16.6 0-30 13.4-30 30s13.4 30 30 30h243.3c16.6 0 30-13.4 30-30 0.1-16.5-13.4-30-29.9-30zM367.4 459.6H216.9c-16.6 0-30 13.4-30 30s13.4 30 30 30h150.4c16.6 0 30-13.4 30-30 0.1-16.6-13.4-30-29.9-30zM297.4 571.2H217c-16.6 0-30 13.4-30 30s13.4 30 30 30h80.4c16.6 0 30-13.4 30-30 0-16.5-13.5-30-30-30zM900 236v552H124V236h776m0-60H124c-33.1 0-60 26.9-60 60v552c0 33.1 26.9 60 60 60h776c33.1 0 60-26.9 60-60V236c0-33.1-26.9-60-60-60z");
				this.LabTypeOne.Text = Conversions.ToString((ModMinecraft.AddClient() == null) ? ModBase.m_IdentifierRepository.Get("CacheAuthServerName", null) : ModBase.m_IdentifierRepository.Get("VersionServerAuthName", ModMinecraft.AddClient()));
				if (Operators.CompareString(this.LabTypeOne.Text, "", false) == 0)
				{
					this.LabTypeOne.Text = "第三方登录";
					goto IL_37B;
				}
				goto IL_37B;
			default:
				ModBase.Log("[Control] 未知的登录页面：" + Conversions.ToString(num), ModBase.LogLevel.Hint, "出现错误");
				break;
			}
			if (this.RadioLoginType5.Checked)
			{
				if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("CacheMsV2Access", null), "", false))
				{
					pageType = PageLaunchLeft.PageType.Ms;
				}
				else
				{
					pageType = PageLaunchLeft.PageType.MsSkin;
				}
				ModBase.m_IdentifierRepository.Set("LoginType", ModLaunch.McLoginType.Ms, false, null);
			}
			else
			{
				pageType = PageLaunchLeft.PageType.Legacy;
				ModBase.m_IdentifierRepository.Set("LoginType", ModLaunch.McLoginType.Legacy, false, null);
			}
			this.PanType.Visibility = Visibility.Visible;
			this.PanTypeOne.Visibility = Visibility.Collapsed;
			this.RadioLoginType5.Visibility = Visibility.Visible;
			this.RadioLoginType0.Visibility = Visibility.Visible;
			IL_37B:
			if (this.m_PrototypeMapper != pageType)
			{
				object[] array;
				bool[] array2;
				NewLateBinding.LateCall(this.PageChange(pageType, Anim), null, "Reload", array = new object[]
				{
					KeepInput
				}, null, null, array2 = new bool[]
				{
					true
				}, true);
				if (array2[0])
				{
					KeepInput = (bool)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[0]), typeof(bool));
				}
				MyRadioButton myRadioButton = (MyRadioButton)base.FindName(Conversions.ToString(Operators.ConcatenateObject("RadioLoginType", ModBase.m_IdentifierRepository.Get("LoginType", null))));
				if (myRadioButton != null)
				{
					myRadioButton.Checked = true;
				}
			}
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00009F76 File Offset: 0x00008176
		private void RadioLoginType_Change(object sender, bool raiseByMouse)
		{
			if (raiseByMouse)
			{
				this.RefreshPage(true, true);
			}
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00009F83 File Offset: 0x00008183
		private static ModBase.EqualableList<string> SkinMsInput()
		{
			return new ModBase.EqualableList<string>
			{
				Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheMsV2Name", null)),
				Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheMsV2Uuid", null))
			};
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0007898C File Offset: 0x00076B8C
		private static void SkinMsLoad(ModLoader.LoaderTask<ModBase.EqualableList<string>, string> Data)
		{
			ModBase.RunInUi((PageLaunchLeft._Closure$__.$I15-0 == null) ? (PageLaunchLeft._Closure$__.$I15-0 = delegate()
			{
				if (ModMain.m_ConfigRepository != null && ModMain.m_ConfigRepository.Skin != null)
				{
					ModMain.m_ConfigRepository.Skin.Clear();
				}
			}) : PageLaunchLeft._Closure$__.$I15-0, false);
			string text = Data.Input[0];
			string uuid = Data.Input[1];
			if (Operators.CompareString(text, "", false) == 0)
			{
				Data.Output = ModBase.m_SerializerRepository + "Skins/" + ModMinecraft.McSkinSex(Conversions.ToString(ModLaunch.McLoginLegacyUuid(text))) + ".png";
				ModBase.Log("[Minecraft] 获取微软正版皮肤失败，ID 为空", ModBase.LogLevel.Normal, "出现错误");
			}
			else
			{
				try
				{
					string text2 = ModMinecraft.McSkinGetAddress(uuid, "Ms");
					if (Data.IsAborted)
					{
						throw new ThreadInterruptedException("当前任务已取消：" + text);
					}
					text2 = ModMinecraft.McSkinDownload(text2);
					if (Data.IsAborted)
					{
						throw new ThreadInterruptedException("当前任务已取消：" + text);
					}
					Data.Output = text2;
				}
				catch (Exception ex)
				{
					if (Operators.CompareString(ex.GetType().Name, "ThreadInterruptedException", false) == 0)
					{
						Data.Output = "";
						return;
					}
					if (ModBase.GetExceptionSummary(ex).Contains("429"))
					{
						Data.Output = ModBase.m_SerializerRepository + "Skins/" + ModMinecraft.McSkinSex(Conversions.ToString(ModLaunch.McLoginLegacyUuid(text))) + ".png";
						ModBase.Log("[Minecraft] 获取正版皮肤失败（" + text + "）：获取皮肤太过频繁，请 5 分钟后再试！", ModBase.LogLevel.Hint, "出现错误");
					}
					else if (ModBase.GetExceptionSummary(ex).Contains("未设置自定义皮肤"))
					{
						Data.Output = ModBase.m_SerializerRepository + "Skins/" + ModMinecraft.McSkinSex(Conversions.ToString(ModLaunch.McLoginLegacyUuid(text))) + ".png";
						ModBase.Log("[Minecraft] 用户未设置自定义皮肤，跳过皮肤加载", ModBase.LogLevel.Normal, "出现错误");
					}
					else
					{
						Data.Output = ModBase.m_SerializerRepository + "Skins/" + ModMinecraft.McSkinSex(Conversions.ToString(ModLaunch.McLoginLegacyUuid(text))) + ".png";
						ModBase.Log(ex, "获取微软正版皮肤失败（" + text + "）", ModBase.LogLevel.Hint, "出现错误");
					}
				}
			}
			if (ModMain.m_ConfigRepository != null)
			{
				ModBase.RunInUi(new Action(ModMain.m_ConfigRepository.Skin.Load), false);
				return;
			}
			if (!Data.IsAborted)
			{
				Data.Input = null;
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00078BF8 File Offset: 0x00076DF8
		private static ModBase.EqualableList<string> SkinLegacyInput()
		{
			int num = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("LaunchSkinType", null));
			ModBase.EqualableList<string> result;
			if (num != 0)
			{
				if (num != 3)
				{
					result = new ModBase.EqualableList<string>
					{
						Conversions.ToString(num)
					};
				}
				else
				{
					result = new ModBase.EqualableList<string>
					{
						Conversions.ToString(3),
						Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchSkinID", null))
					};
				}
			}
			else if (ModMain.m_ProcIterator != null && ModMain.m_ProcIterator.m_ImporterMapper)
			{
				result = new ModBase.EqualableList<string>
				{
					Conversions.ToString(0),
					ModMain.m_ProcIterator.ComboName.Text.Trim() ?? ""
				};
			}
			else if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LoginLegacyName", null), "", false))
			{
				result = new ModBase.EqualableList<string>
				{
					Conversions.ToString(0),
					""
				};
			}
			else
			{
				result = new ModBase.EqualableList<string>
				{
					Conversions.ToString(0),
					ModBase.m_IdentifierRepository.Get("LoginLegacyName", null).ToString().BeforeFirst("¨", false) ?? ""
				};
			}
			return result;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00078D34 File Offset: 0x00076F34
		private static void SkinLegacyLoad(ModLoader.LoaderTask<ModBase.EqualableList<string>, string> Data)
		{
			ModBase.RunInUi((PageLaunchLeft._Closure$__.$I18-0 == null) ? (PageLaunchLeft._Closure$__.$I18-0 = delegate()
			{
				if (ModMain.m_ProcIterator != null && ModMain.m_ProcIterator.Skin != null)
				{
					ModMain.m_ProcIterator.Skin.Clear();
				}
			}) : PageLaunchLeft._Closure$__.$I18-0, false);
			string left = Data.Input[0];
			if (Operators.CompareString(left, Conversions.ToString(0), false) == 0)
			{
				Data.Output = ModBase.m_SerializerRepository + "Skins/" + ModMinecraft.McSkinSex(Conversions.ToString(ModLaunch.McLoginLegacyUuid(Data.Input[1]))) + ".png";
			}
			else
			{
				if (Operators.CompareString(left, Conversions.ToString(1), false) != 0)
				{
					if (Operators.CompareString(left, Conversions.ToString(2), false) == 0)
					{
						Data.Output = ModBase.m_SerializerRepository + "Skins/Alex.png";
						goto IL_2C1;
					}
					if (Operators.CompareString(left, Conversions.ToString(3), false) == 0)
					{
						string text = Data.Input[1];
						try
						{
							if (Enumerable.Count<char>(text) < 2)
							{
								Data.Output = ModBase.m_SerializerRepository + "Skins/Steve.png";
							}
							else
							{
								string text2 = Conversions.ToString(ModLaunch.McLoginMojangUuid(text, true));
								if (Data.IsAborted)
								{
									throw new ThreadInterruptedException("当前任务已取消：" + text);
								}
								text2 = ModMinecraft.McSkinGetAddress(text2, "Mojang");
								if (Data.IsAborted)
								{
									throw new ThreadInterruptedException("当前任务已取消：" + text);
								}
								text2 = ModMinecraft.McSkinDownload(text2);
								if (Data.IsAborted)
								{
									throw new ThreadInterruptedException("当前任务已取消：" + text);
								}
								Data.Output = text2;
							}
							goto IL_2C1;
						}
						catch (Exception ex)
						{
							if (Operators.CompareString(ex.GetType().Name, "ThreadInterruptedException", false) == 0)
							{
								Data.Output = "";
								return;
							}
							if (ModBase.GetExceptionSummary(ex).Contains("429"))
							{
								Data.Output = ModBase.m_SerializerRepository + "Skins/" + ModMinecraft.McSkinSex(Conversions.ToString(ModLaunch.McLoginLegacyUuid(text))) + ".png";
								ModBase.Log("获取离线登录使用的正版皮肤失败（" + text + "）：获取皮肤太过频繁，请 5 分钟后再试！", ModBase.LogLevel.Normal, "出现错误");
							}
							else
							{
								Data.Output = ModBase.m_SerializerRepository + "Skins/" + ModMinecraft.McSkinSex(Conversions.ToString(ModLaunch.McLoginLegacyUuid(text))) + ".png";
								ModBase.Log(ex, "获取离线登录使用的正版皮肤失败（" + text + "）", ModBase.LogLevel.Debug, "出现错误");
							}
							goto IL_2C1;
						}
					}
					if (Operators.CompareString(left, Conversions.ToString(4), false) != 0)
					{
						goto IL_2C1;
					}
					if (File.Exists(ModBase.m_InstanceRepository + "CustomSkin.png"))
					{
						Data.Output = ModBase.m_InstanceRepository + "CustomSkin.png";
						goto IL_2C1;
					}
					ModMain.Hint("未找到离线皮肤自定义文件，可能它已被删除。PCL 将使用默认的 Steve 皮肤！", ModMain.HintType.Info, true);
					ModBase.m_IdentifierRepository.Set("LaunchSkinType", 1, false, null);
				}
				Data.Output = ModBase.m_SerializerRepository + "Skins/Steve.png";
			}
			IL_2C1:
			if (ModMain.m_ProcIterator != null)
			{
				ModBase.RunInUi(new Action(ModMain.m_ProcIterator.Skin.Load), false);
				return;
			}
			if (!Data.IsAborted)
			{
				Data.Input = null;
			}
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00009FC0 File Offset: 0x000081C0
		private static ModBase.EqualableList<string> SkinNideInput()
		{
			return new ModBase.EqualableList<string>
			{
				Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheNideName", null)),
				Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheNideUuid", null))
			};
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00079044 File Offset: 0x00077244
		private static void SkinNideLoad(ModLoader.LoaderTask<ModBase.EqualableList<string>, string> Data)
		{
			ModBase.RunInUi((PageLaunchLeft._Closure$__.$I21-0 == null) ? (PageLaunchLeft._Closure$__.$I21-0 = delegate()
			{
				if (ModMain.broadcasterRepository != null && ModMain.broadcasterRepository.Skin != null)
				{
					ModMain.broadcasterRepository.Skin.Clear();
				}
			}) : PageLaunchLeft._Closure$__.$I21-0, false);
			string text = Data.Input[0];
			string uuid = Data.Input[1];
			if (Operators.CompareString(text, "", false) == 0)
			{
				Data.Output = ModBase.m_SerializerRepository + "Skins/" + ModMinecraft.McSkinSex(Conversions.ToString(ModLaunch.McLoginLegacyUuid(text))) + ".png";
				ModBase.Log("[Minecraft] 获取统一通行证皮肤失败，ID 为空", ModBase.LogLevel.Normal, "出现错误");
			}
			else
			{
				try
				{
					string text2 = ModMinecraft.McSkinGetAddress(uuid, "Nide");
					if (Data.IsAborted)
					{
						throw new ThreadInterruptedException("当前任务已取消：" + text);
					}
					text2 = ModMinecraft.McSkinDownload(text2);
					if (Data.IsAborted)
					{
						throw new ThreadInterruptedException("当前任务已取消：" + text);
					}
					Data.Output = text2;
				}
				catch (Exception ex)
				{
					if (Operators.CompareString(ex.GetType().Name, "ThreadInterruptedException", false) == 0)
					{
						Data.Output = "";
						return;
					}
					if (ModBase.GetExceptionSummary(ex).Contains("429"))
					{
						Data.Output = ModBase.m_SerializerRepository + "Skins/Steve.png";
						ModBase.Log("[Minecraft] 获取统一通行证皮肤失败（" + text + "）：获取皮肤太过频繁，请 5 分钟后再试！", ModBase.LogLevel.Hint, "出现错误");
					}
					else if (ModBase.GetExceptionSummary(ex).Contains("未设置自定义皮肤"))
					{
						Data.Output = ModBase.m_SerializerRepository + "Skins/Steve.png";
						ModBase.Log("[Minecraft] 用户未设置自定义皮肤，跳过皮肤加载", ModBase.LogLevel.Normal, "出现错误");
					}
					else
					{
						Data.Output = ModBase.m_SerializerRepository + "Skins/Steve.png";
						ModBase.Log(ex, "获取统一通行证皮肤失败（" + text + "）", ModBase.LogLevel.Hint, "出现错误");
					}
				}
			}
			if (ModMain.broadcasterRepository != null)
			{
				ModBase.RunInUi(new Action(ModMain.broadcasterRepository.Skin.Load), false);
				return;
			}
			if (!Data.IsAborted)
			{
				Data.Input = null;
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00009FFD File Offset: 0x000081FD
		private static ModBase.EqualableList<string> SkinAuthInput()
		{
			return new ModBase.EqualableList<string>
			{
				Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheAuthName", null)),
				Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheAuthUuid", null))
			};
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00079260 File Offset: 0x00077460
		private static void SkinAuthLoad(ModLoader.LoaderTask<ModBase.EqualableList<string>, string> Data)
		{
			ModBase.RunInUi((PageLaunchLeft._Closure$__.$I24-0 == null) ? (PageLaunchLeft._Closure$__.$I24-0 = delegate()
			{
				if (ModMain.readerRepository != null && ModMain.readerRepository.Skin != null)
				{
					ModMain.readerRepository.Skin.Clear();
				}
			}) : PageLaunchLeft._Closure$__.$I24-0, false);
			string text = Data.Input[0];
			string uuid = Data.Input[1];
			if (Operators.CompareString(text, "", false) == 0)
			{
				Data.Output = ModBase.m_SerializerRepository + "Skins/Steve.png";
				ModBase.Log("[Minecraft] 获取 Authlib-Injector 皮肤失败，ID 为空", ModBase.LogLevel.Normal, "出现错误");
			}
			else
			{
				try
				{
					string text2 = ModMinecraft.McSkinGetAddress(uuid, "Auth");
					if (Data.IsAborted)
					{
						throw new ThreadInterruptedException("当前任务已取消：" + text);
					}
					text2 = ModMinecraft.McSkinDownload(text2);
					if (Data.IsAborted)
					{
						throw new ThreadInterruptedException("当前任务已取消：" + text);
					}
					Data.Output = text2;
				}
				catch (Exception ex)
				{
					if (Operators.CompareString(ex.GetType().Name, "ThreadInterruptedException", false) == 0)
					{
						Data.Output = "";
						return;
					}
					if (ModBase.GetExceptionSummary(ex).Contains("429"))
					{
						Data.Output = ModBase.m_SerializerRepository + "Skins/Steve.png";
						ModBase.Log("[Minecraft] 获取 Authlib-Injector 皮肤失败（" + text + "）：获取皮肤太过频繁，请 5 分钟后再试！", ModBase.LogLevel.Hint, "出现错误");
					}
					else if (ModBase.GetExceptionSummary(ex).Contains("未设置自定义皮肤"))
					{
						Data.Output = ModBase.m_SerializerRepository + "Skins/Steve.png";
						ModBase.Log("[Minecraft] 用户未设置自定义皮肤，跳过皮肤加载", ModBase.LogLevel.Normal, "出现错误");
					}
					else
					{
						Data.Output = ModBase.m_SerializerRepository + "Skins/Steve.png";
						ModBase.Log(ex, "获取 Authlib-Injector 皮肤失败（" + text + "）", ModBase.LogLevel.Hint, "出现错误");
					}
				}
			}
			if (ModMain.readerRepository != null)
			{
				ModBase.RunInUi(new Action(ModMain.readerRepository.Skin.Load), false);
				return;
			}
			if (!Data.IsAborted)
			{
				Data.Input = null;
			}
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0000A03A File Offset: 0x0000823A
		private void BtnVersion_Click(object sender, EventArgs e)
		{
			if (ModLaunch.databaseTests.State != ModBase.LoadState.Loading)
			{
				ModMain._ProcessIterator.PageChange(FormMain.PageType.VersionSelect, FormMain.PageSubType.Default);
			}
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00079468 File Offset: 0x00077668
		public void LaunchButtonClick()
		{
			if (ModLaunch.databaseTests.State != ModBase.LoadState.Loading && this.BtnLaunch.IsEnabled && (ModMain._ProcessIterator._GetterIterator == null || ModMain._ProcessIterator._GetterIterator.NewBroadcaster() == MyPageRight.PageStates.ContentStay || ModMain._ProcessIterator._GetterIterator.NewBroadcaster() == MyPageRight.PageStates.ContentEnter))
			{
				if (ModMain.mapRepository && !ModMain.m_ErrorRepository)
				{
					ModSecret.ThemeUnlock(12, false, "隐藏主题 滑稽彩 已解锁！");
					ModMain.m_ErrorRepository = true;
					ModMain.recordIterator.AprilScaleTrans.ScaleX = 1.0;
					ModMain.recordIterator.AprilScaleTrans.ScaleY = 1.0;
					ModMain.recordIterator.AprilPosTrans.X = 0.0;
					ModMain.recordIterator.AprilPosTrans.Y = 0.0;
					ModMain._ProcessIterator.BtnExtraApril.ShowRefresh();
				}
				if (Operators.CompareString(this.BtnLaunch.Text, "启动游戏", false) == 0)
				{
					ModLaunch.McLaunchStart(null);
					return;
				}
				if (Operators.CompareString(this.BtnLaunch.Text, "下载游戏", false) == 0)
				{
					ModMain._ProcessIterator.PageChange(FormMain.PageType.Download, FormMain.PageSubType.DownloadInstall);
				}
			}
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x000795A8 File Offset: 0x000777A8
		public void RefreshButtonsUI()
		{
			if (this.BtnLaunch.IsLoaded)
			{
				int num;
				if (this.m_CodeMapper && ModMinecraft._ListenerTests.State != ModBase.LoadState.Loading)
				{
					if (ModMinecraft.m_CreatorTests.State != ModBase.LoadState.Loading)
					{
						if (ModMinecraft.AddClient() != null)
						{
							num = 3;
							goto IL_78;
						}
						if (Conversions.ToBoolean(Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenPageDownload", null)) && !PageSetupUI.CreateClient()))
						{
							num = 1;
							goto IL_78;
						}
						num = 2;
						goto IL_78;
					}
				}
				num = 0;
				IL_78:
				if (num != this.m_MerchantMapper || Operators.CompareString((ModMinecraft.AddClient() == null) ? "" : ModMinecraft.AddClient().Path, (this._AuthenticationMapper == null) ? "" : this._AuthenticationMapper.Path, false) != 0)
				{
					this._AuthenticationMapper = ModMinecraft.AddClient();
					this.m_MerchantMapper = num;
					switch (num)
					{
					case 0:
						ModBase.Log("[Minecraft] 启动按钮：正在加载 Minecraft 版本", ModBase.LogLevel.Normal, "出现错误");
						ModMain.recordIterator.BtnLaunch.Text = "正在加载";
						ModMain.recordIterator.BtnLaunch.IsEnabled = false;
						ModMain.recordIterator.LabVersion.Text = "正在加载中，请稍候";
						ModMain.recordIterator.BtnVersion.IsEnabled = false;
						ModMain.recordIterator.BtnMore.Visibility = Visibility.Collapsed;
						break;
					case 1:
						ModBase.Log("[Minecraft] 启动按钮：无 Minecraft 版本，下载已禁用", ModBase.LogLevel.Normal, "出现错误");
						ModMain.recordIterator.BtnLaunch.Text = "启动游戏";
						ModMain.recordIterator.BtnLaunch.IsEnabled = false;
						ModMain.recordIterator.LabVersion.Text = "未找到可用的游戏版本";
						ModMain.recordIterator.BtnVersion.IsEnabled = true;
						ModMain.recordIterator.BtnMore.Visibility = Visibility.Collapsed;
						break;
					case 2:
						ModBase.Log("[Minecraft] 启动按钮：无 Minecraft 版本，要求下载", ModBase.LogLevel.Normal, "出现错误");
						ModMain.recordIterator.BtnLaunch.Text = "下载游戏";
						ModMain.recordIterator.BtnLaunch.IsEnabled = true;
						ModMain.recordIterator.LabVersion.Text = "未找到可用的游戏版本";
						ModMain.recordIterator.BtnVersion.IsEnabled = true;
						ModMain.recordIterator.BtnMore.Visibility = Visibility.Collapsed;
						break;
					case 3:
						ModBase.Log("[Minecraft] 启动按钮：Minecraft 版本：" + ModMinecraft.AddClient().Path, ModBase.LogLevel.Normal, "出现错误");
						ModMain.recordIterator.BtnLaunch.Text = "启动游戏";
						ModMain.recordIterator.BtnVersion.IsEnabled = true;
						ModMain.recordIterator.BtnLaunch.IsEnabled = true;
						ModMain.recordIterator.LabVersion.Text = ModMinecraft.AddClient().Name;
						break;
					}
				}
				ModMain.recordIterator.BtnVersion.Visibility = (Conversions.ToBoolean(!PageSetupUI.CreateClient() && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenFunctionSelect", null))) ? Visibility.Collapsed : Visibility.Visible);
				if (num == 3)
				{
					ModMain.recordIterator.BtnMore.Visibility = ModMain.recordIterator.BtnVersion.Visibility;
				}
			}
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x000798B4 File Offset: 0x00077AB4
		private void BtnCancel_Click()
		{
			if (ModLaunch.m_PredicateTests != null)
			{
				ModLaunch.m_PredicateTests.Abort();
				ModLaunch.McLaunchLog("已取消启动");
				try
				{
					if (ModLaunch.customerTests != null)
					{
						ModLaunch.customerTests.Kill();
					}
					else if (ModLaunch._PoolTests != null && !ModLaunch._PoolTests.HasExited)
					{
						ModLaunch._PoolTests.Kill();
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "取消启动结束进程失败", ModBase.LogLevel.Hint, "出现错误");
				}
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0000A05A File Offset: 0x0000825A
		private void BtnMore_Click(object sender, EventArgs e)
		{
			if (ModLaunch.databaseTests.State != ModBase.LoadState.Loading)
			{
				ModMinecraft.AddClient().Load();
				PageVersionLeft._InstanceConfig = ModMinecraft.AddClient();
				ModMain._ProcessIterator.PageChange(FormMain.PageType.VersionSetup, FormMain.PageSubType.Default);
			}
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00079940 File Offset: 0x00077B40
		public void LaunchingRefresh()
		{
			try
			{
				PageLaunchLeft._Closure$__33-0 CS$<>8__locals1 = new PageLaunchLeft._Closure$__33-0(CS$<>8__locals1);
				CS$<>8__locals1.$VB$Me = this;
				if (ModLaunch.m_PredicateTests.State != ModBase.LoadState.Aborted)
				{
					bool flag = false;
					try
					{
						try
						{
							foreach (ModLoader.LoaderBase loaderBase in ModLaunch.m_PredicateTests.GetLoaderList(false))
							{
								if (loaderBase.State == ModBase.LoadState.Loading || loaderBase.State == ModBase.LoadState.Waiting)
								{
									this.LabLaunchingStage.Text = loaderBase.Name;
									flag = (Operators.CompareString(loaderBase.Name, "等待游戏窗口出现", false) == 0 || Operators.CompareString(loaderBase.Name, "结束处理", false) == 0);
									goto IL_E2;
								}
							}
						}
						finally
						{
							List<ModLoader.LoaderBase>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
						this.LabLaunchingStage.Text = "已完成";
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "获取是否启动完成失败，可能是由于启动状态改变导致集合已修改", ModBase.LogLevel.Debug, "出现错误");
						return;
					}
					IL_E2:
					if (ModAnimation.AniIsRun("Launch State Page"))
					{
						flag = false;
					}
					double progress = ModLaunch.m_PredicateTests.Progress;
					if (progress >= this.algoMapper)
					{
						ref double ptr = ref this.algoMapper;
						this.algoMapper = ptr + ((progress - this.algoMapper) * 0.2 + 0.005);
					}
					if (progress <= this.algoMapper)
					{
						this.algoMapper = progress;
					}
					if (flag)
					{
						this.algoMapper = 1.0;
					}
					this.LabLaunchingTitle.Text = (flag ? "已启动游戏" : ((ModLaunch.filterTests.requestMap == null) ? "正在启动游戏" : "正在导出启动脚本"));
					this.LabLaunchingProgress.Text = ModBase.StrFillNum(this.algoMapper * 100.0, 2) + " %";
					CS$<>8__locals1.$VB$Local_HasLaunchDownloader = false;
					try
					{
						try
						{
							foreach (ModNet.LoaderDownload loaderDownload in ModNet.tokenTests.regTest)
							{
								if (loaderDownload.RealParent != null && Operators.CompareString(loaderDownload.RealParent.Name, "Minecraft 启动", false) == 0 && loaderDownload.State == ModBase.LoadState.Loading)
								{
									CS$<>8__locals1.$VB$Local_HasLaunchDownloader = true;
								}
							}
						}
						finally
						{
							IEnumerator<ModNet.LoaderDownload> enumerator2;
							if (enumerator2 != null)
							{
								enumerator2.Dispose();
							}
						}
					}
					catch (Exception ex2)
					{
						ModBase.Log(ex2, "获取 Minecraft 启动下载器失败，可能是因为启动被取消", ModBase.LogLevel.Debug, "出现错误");
						CS$<>8__locals1.$VB$Local_HasLaunchDownloader = false;
					}
					this.LabLaunchingDownload.Text = ModBase.GetString(ModNet.tokenTests.m_BridgeTest) + "/s";
					List<ModAnimation.AniData> list = new List<ModAnimation.AniData>
					{
						ModAnimation.AaGridLengthWidth(this.ProgressLaunchingFinished, this.algoMapper - this.ProgressLaunchingFinished.Width.Value, 260, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
						ModAnimation.AaGridLengthWidth(this.ProgressLaunchingUnfinished, 1.0 - this.algoMapper - this.ProgressLaunchingUnfinished.Width.Value, 260, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false)
					};
					if (CS$<>8__locals1.$VB$Local_HasLaunchDownloader == (this.LabLaunchingDownload.Visibility == Visibility.Collapsed))
					{
						this.LabLaunchingDownload.Visibility = Visibility.Visible;
						this.LabLaunchingDownloadLeft.Visibility = Visibility.Visible;
						list.AddRange(new ModAnimation.AniData[]
						{
							ModAnimation.AaOpacity(this.LabLaunchingDownload, (CS$<>8__locals1.$VB$Local_HasLaunchDownloader > false) - this.LabLaunchingDownload.Opacity, 100, 0, null, false),
							ModAnimation.AaOpacity(this.LabLaunchingDownloadLeft, (CS$<>8__locals1.$VB$Local_HasLaunchDownloader ? 0.5 : 0.0) - this.LabLaunchingDownloadLeft.Opacity, 100, 0, null, false),
							ModAnimation.AaCode(delegate
							{
								if (!CS$<>8__locals1.$VB$Local_HasLaunchDownloader)
								{
									CS$<>8__locals1.$VB$Me.LabLaunchingDownload.Visibility = Visibility.Collapsed;
									CS$<>8__locals1.$VB$Me.LabLaunchingDownloadLeft.Visibility = Visibility.Collapsed;
								}
							}, 110, false)
						});
					}
					if (!flag == (this.LabLaunchingProgress.Visibility == Visibility.Collapsed))
					{
						this.LabLaunchingProgress.Visibility = Visibility.Visible;
						this.LabLaunchingProgressLeft.Visibility = Visibility.Visible;
						if (flag)
						{
							this.PanLaunchingHint.Visibility = Visibility.Visible;
						}
						list.AddRange(new ModAnimation.AniData[]
						{
							ModAnimation.AaOpacity(this.LabLaunchingProgress, !flag - this.LabLaunchingProgress.Opacity, 100, 0, null, false),
							ModAnimation.AaOpacity(this.LabLaunchingProgressLeft, ((!flag) ? 0.5 : 0.0) - this.LabLaunchingProgressLeft.Opacity, 100, 0, null, false),
							ModAnimation.AaOpacity(this.PanLaunchingHint, (flag > false) - this.PanLaunchingHint.Opacity, 100, 0, null, false)
						});
					}
					ModAnimation.AniStart(list, "Launching Progress", false);
				}
			}
			catch (Exception ex3)
			{
				ModBase.Log(ex3, "刷新启动信息失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00079E7C File Offset: 0x0007807C
		private void PanLaunchingInfo_SizeChangedW(object sender, SizeChangedEventArgs e)
		{
			double value = e.NewSize.Width - e.PreviousSize.Width;
			if (e.PreviousSize.Width != 0.0 && !this.m_ComparatorMapper && Math.Abs(value) >= 1.0 && this.PanLaunchingInfo.ActualWidth != 0.0)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaWidth(this.PanLaunchingInfo, value, 180, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
					ModAnimation.AaCode(delegate
					{
						this.m_ComparatorMapper = false;
						this.PanLaunchingInfo.Width = this.mappingMapper;
					}, 0, true)
				}, "Launching Info Width", false);
				this.m_ComparatorMapper = true;
				this.mappingMapper = this.PanLaunchingInfo.Width;
				this.PanLaunchingInfo.Width = e.PreviousSize.Width;
			}
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00079F74 File Offset: 0x00078174
		private void PanLaunchingInfo_SizeChangedH(object sender, SizeChangedEventArgs e)
		{
			double value = e.NewSize.Height - e.PreviousSize.Height;
			if (e.PreviousSize.Height != 0.0 && !this.m_TokenizerMapper && Math.Abs(value) >= 1.0 && this.PanLaunchingInfo.ActualHeight != 0.0)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaHeight(this.PanLaunchingInfo, value, 180, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
					ModAnimation.AaCode(delegate
					{
						this.m_TokenizerMapper = false;
						this.PanLaunchingInfo.Height = this.filterMapper;
					}, 0, true)
				}, "Launching Info Height", false);
				this.m_TokenizerMapper = true;
				this.filterMapper = this.PanLaunchingInfo.Height;
				this.PanLaunchingInfo.Height = e.PreviousSize.Height;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0000A08F File Offset: 0x0000828F
		// (set) Token: 0x06001065 RID: 4197 RVA: 0x0000A097 File Offset: 0x00008297
		internal virtual PageLaunchLeft PanBack { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0000A0A0 File Offset: 0x000082A0
		// (set) Token: 0x06001067 RID: 4199 RVA: 0x0000A0A8 File Offset: 0x000082A8
		internal virtual Grid PanInput { get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x0000A0B1 File Offset: 0x000082B1
		// (set) Token: 0x06001069 RID: 4201 RVA: 0x0007A06C File Offset: 0x0007826C
		internal virtual MyButton BtnVersion
		{
			[CompilerGenerated]
			get
			{
				return this._PoolMapper;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnVersion_Click);
				MyButton poolMapper = this._PoolMapper;
				if (poolMapper != null)
				{
					poolMapper.Click -= value2;
				}
				this._PoolMapper = value;
				poolMapper = this._PoolMapper;
				if (poolMapper != null)
				{
					poolMapper.Click += value2;
				}
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x0000A0B9 File Offset: 0x000082B9
		// (set) Token: 0x0600106B RID: 4203 RVA: 0x0007A0B0 File Offset: 0x000782B0
		internal virtual MyButton BtnMore
		{
			[CompilerGenerated]
			get
			{
				return this.m_CustomerMapper;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnMore_Click);
				MyButton customerMapper = this.m_CustomerMapper;
				if (customerMapper != null)
				{
					customerMapper.Click -= value2;
				}
				this.m_CustomerMapper = value;
				customerMapper = this.m_CustomerMapper;
				if (customerMapper != null)
				{
					customerMapper.Click += value2;
				}
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x0000A0C1 File Offset: 0x000082C1
		// (set) Token: 0x0600106D RID: 4205 RVA: 0x0000A0C9 File Offset: 0x000082C9
		internal virtual Grid PanLogin { get; set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x0000A0D2 File Offset: 0x000082D2
		// (set) Token: 0x0600106F RID: 4207 RVA: 0x0000A0DA File Offset: 0x000082DA
		internal virtual Grid PanTypeOne { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0000A0E3 File Offset: 0x000082E3
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x0000A0EB File Offset: 0x000082EB
		internal virtual Path PathTypeOne { get; set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0000A0F4 File Offset: 0x000082F4
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x0000A0FC File Offset: 0x000082FC
		internal virtual TextBlock LabTypeOne { get; set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x0000A105 File Offset: 0x00008305
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x0000A10D File Offset: 0x0000830D
		internal virtual Grid PanType { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0000A116 File Offset: 0x00008316
		// (set) Token: 0x06001077 RID: 4215 RVA: 0x0007A0F4 File Offset: 0x000782F4
		internal virtual MyRadioButton RadioLoginType5
		{
			[CompilerGenerated]
			get
			{
				return this.m_ProcessMapper;
			}
			[CompilerGenerated]
			set
			{
				MyRadioButton.CheckEventHandler obj = new MyRadioButton.CheckEventHandler(this.RadioLoginType_Change);
				MyRadioButton processMapper = this.m_ProcessMapper;
				if (processMapper != null)
				{
					processMapper.ResolveTests(obj);
				}
				this.m_ProcessMapper = value;
				processMapper = this.m_ProcessMapper;
				if (processMapper != null)
				{
					processMapper.LogoutTests(obj);
				}
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x0000A11E File Offset: 0x0000831E
		// (set) Token: 0x06001079 RID: 4217 RVA: 0x0007A138 File Offset: 0x00078338
		internal virtual MyRadioButton RadioLoginType0
		{
			[CompilerGenerated]
			get
			{
				return this._ParameterMapper;
			}
			[CompilerGenerated]
			set
			{
				MyRadioButton.CheckEventHandler obj = new MyRadioButton.CheckEventHandler(this.RadioLoginType_Change);
				MyRadioButton parameterMapper = this._ParameterMapper;
				if (parameterMapper != null)
				{
					parameterMapper.ResolveTests(obj);
				}
				this._ParameterMapper = value;
				parameterMapper = this._ParameterMapper;
				if (parameterMapper != null)
				{
					parameterMapper.LogoutTests(obj);
				}
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x0000A126 File Offset: 0x00008326
		// (set) Token: 0x0600107B RID: 4219 RVA: 0x0000A12E File Offset: 0x0000832E
		internal virtual ScaleTransform AprilScaleTrans { get; set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x0000A137 File Offset: 0x00008337
		// (set) Token: 0x0600107D RID: 4221 RVA: 0x0000A13F File Offset: 0x0000833F
		internal virtual TranslateTransform AprilPosTrans { get; set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x0000A148 File Offset: 0x00008348
		// (set) Token: 0x0600107F RID: 4223 RVA: 0x0007A17C File Offset: 0x0007837C
		internal virtual MyButton BtnLaunch
		{
			[CompilerGenerated]
			get
			{
				return this.invocationMapper;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.LaunchButtonClick();
				};
				RoutedEventHandler value3 = delegate(object sender, RoutedEventArgs e)
				{
					this.RefreshButtonsUI();
				};
				MyButton myButton = this.invocationMapper;
				if (myButton != null)
				{
					myButton.Click -= value2;
					myButton.Loaded -= value3;
				}
				this.invocationMapper = value;
				myButton = this.invocationMapper;
				if (myButton != null)
				{
					myButton.Click += value2;
					myButton.Loaded += value3;
				}
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x0000A150 File Offset: 0x00008350
		// (set) Token: 0x06001081 RID: 4225 RVA: 0x0000A158 File Offset: 0x00008358
		internal virtual TextBlock LabVersion { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x0000A161 File Offset: 0x00008361
		// (set) Token: 0x06001083 RID: 4227 RVA: 0x0000A169 File Offset: 0x00008369
		internal virtual Grid PanLaunching { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x0000A172 File Offset: 0x00008372
		// (set) Token: 0x06001085 RID: 4229 RVA: 0x0000A17A File Offset: 0x0000837A
		internal virtual MyLoading LoadLaunching { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x0000A183 File Offset: 0x00008383
		// (set) Token: 0x06001087 RID: 4231 RVA: 0x0000A18B File Offset: 0x0000838B
		internal virtual TextBlock LabLaunchingTitle { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x0000A194 File Offset: 0x00008394
		// (set) Token: 0x06001089 RID: 4233 RVA: 0x0000A19C File Offset: 0x0000839C
		internal virtual TextBlock LabLaunchingName { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x0000A1A5 File Offset: 0x000083A5
		// (set) Token: 0x0600108B RID: 4235 RVA: 0x0000A1AD File Offset: 0x000083AD
		internal virtual ColumnDefinition ProgressLaunchingFinished { get; set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x0000A1B6 File Offset: 0x000083B6
		// (set) Token: 0x0600108D RID: 4237 RVA: 0x0000A1BE File Offset: 0x000083BE
		internal virtual ColumnDefinition ProgressLaunchingUnfinished { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x0000A1C7 File Offset: 0x000083C7
		// (set) Token: 0x0600108F RID: 4239 RVA: 0x0007A1DC File Offset: 0x000783DC
		internal virtual Grid PanLaunchingInfo
		{
			[CompilerGenerated]
			get
			{
				return this.m_ListenerMapper;
			}
			[CompilerGenerated]
			set
			{
				SizeChangedEventHandler value2 = new SizeChangedEventHandler(this.PanLaunchingInfo_SizeChangedW);
				SizeChangedEventHandler value3 = new SizeChangedEventHandler(this.PanLaunchingInfo_SizeChangedH);
				Grid listenerMapper = this.m_ListenerMapper;
				if (listenerMapper != null)
				{
					listenerMapper.SizeChanged -= value2;
					listenerMapper.SizeChanged -= value3;
				}
				this.m_ListenerMapper = value;
				listenerMapper = this.m_ListenerMapper;
				if (listenerMapper != null)
				{
					listenerMapper.SizeChanged += value2;
					listenerMapper.SizeChanged += value3;
				}
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x0000A1CF File Offset: 0x000083CF
		// (set) Token: 0x06001091 RID: 4241 RVA: 0x0000A1D7 File Offset: 0x000083D7
		internal virtual TextBlock LabLaunchingStage { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x0000A1E0 File Offset: 0x000083E0
		// (set) Token: 0x06001093 RID: 4243 RVA: 0x0000A1E8 File Offset: 0x000083E8
		internal virtual TextBlock LabLaunchingMethod { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x0000A1F1 File Offset: 0x000083F1
		// (set) Token: 0x06001095 RID: 4245 RVA: 0x0000A1F9 File Offset: 0x000083F9
		internal virtual TextBlock LabLaunchingProgressLeft { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x0000A202 File Offset: 0x00008402
		// (set) Token: 0x06001097 RID: 4247 RVA: 0x0000A20A File Offset: 0x0000840A
		internal virtual TextBlock LabLaunchingProgress { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x0000A213 File Offset: 0x00008413
		// (set) Token: 0x06001099 RID: 4249 RVA: 0x0000A21B File Offset: 0x0000841B
		internal virtual TextBlock LabLaunchingDownloadLeft { get; set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x0000A224 File Offset: 0x00008424
		// (set) Token: 0x0600109B RID: 4251 RVA: 0x0000A22C File Offset: 0x0000842C
		internal virtual TextBlock LabLaunchingDownload { get; set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x0000A235 File Offset: 0x00008435
		// (set) Token: 0x0600109D RID: 4253 RVA: 0x0000A23D File Offset: 0x0000843D
		internal virtual Grid PanLaunchingHint { get; set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x0000A246 File Offset: 0x00008446
		// (set) Token: 0x0600109F RID: 4255 RVA: 0x0000A24E File Offset: 0x0000844E
		internal virtual TextBlock LabLaunchingHint { get; set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x0000A257 File Offset: 0x00008457
		// (set) Token: 0x060010A1 RID: 4257 RVA: 0x0007A23C File Offset: 0x0007843C
		internal virtual MyButton BtnCancel
		{
			[CompilerGenerated]
			get
			{
				return this._ExceptionMapper;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.BtnCancel_Click();
				};
				MyButton exceptionMapper = this._ExceptionMapper;
				if (exceptionMapper != null)
				{
					exceptionMapper.Click -= value2;
				}
				this._ExceptionMapper = value;
				exceptionMapper = this._ExceptionMapper;
				if (exceptionMapper != null)
				{
					exceptionMapper.Click += value2;
				}
			}
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0007A280 File Offset: 0x00078480
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._UtilsMapper)
			{
				this._UtilsMapper = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/pagelaunchleft.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0007A2B0 File Offset: 0x000784B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (PageLaunchLeft)target;
				return;
			}
			if (connectionId == 2)
			{
				this.PanInput = (Grid)target;
				return;
			}
			if (connectionId == 3)
			{
				this.BtnVersion = (MyButton)target;
				return;
			}
			if (connectionId == 4)
			{
				this.BtnMore = (MyButton)target;
				return;
			}
			if (connectionId == 5)
			{
				this.PanLogin = (Grid)target;
				return;
			}
			if (connectionId == 6)
			{
				this.PanTypeOne = (Grid)target;
				return;
			}
			if (connectionId == 7)
			{
				this.PathTypeOne = (Path)target;
				return;
			}
			if (connectionId == 8)
			{
				this.LabTypeOne = (TextBlock)target;
				return;
			}
			if (connectionId == 9)
			{
				this.PanType = (Grid)target;
				return;
			}
			if (connectionId == 10)
			{
				this.RadioLoginType5 = (MyRadioButton)target;
				return;
			}
			if (connectionId == 11)
			{
				this.RadioLoginType0 = (MyRadioButton)target;
				return;
			}
			if (connectionId == 12)
			{
				this.AprilScaleTrans = (ScaleTransform)target;
				return;
			}
			if (connectionId == 13)
			{
				this.AprilPosTrans = (TranslateTransform)target;
				return;
			}
			if (connectionId == 14)
			{
				this.BtnLaunch = (MyButton)target;
				return;
			}
			if (connectionId == 15)
			{
				this.LabVersion = (TextBlock)target;
				return;
			}
			if (connectionId == 16)
			{
				this.PanLaunching = (Grid)target;
				return;
			}
			if (connectionId == 17)
			{
				this.LoadLaunching = (MyLoading)target;
				return;
			}
			if (connectionId == 18)
			{
				this.LabLaunchingTitle = (TextBlock)target;
				return;
			}
			if (connectionId == 19)
			{
				this.LabLaunchingName = (TextBlock)target;
				return;
			}
			if (connectionId == 20)
			{
				this.ProgressLaunchingFinished = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 21)
			{
				this.ProgressLaunchingUnfinished = (ColumnDefinition)target;
				return;
			}
			if (connectionId == 22)
			{
				this.PanLaunchingInfo = (Grid)target;
				return;
			}
			if (connectionId == 23)
			{
				this.LabLaunchingStage = (TextBlock)target;
				return;
			}
			if (connectionId == 24)
			{
				this.LabLaunchingMethod = (TextBlock)target;
				return;
			}
			if (connectionId == 25)
			{
				this.LabLaunchingProgressLeft = (TextBlock)target;
				return;
			}
			if (connectionId == 26)
			{
				this.LabLaunchingProgress = (TextBlock)target;
				return;
			}
			if (connectionId == 27)
			{
				this.LabLaunchingDownloadLeft = (TextBlock)target;
				return;
			}
			if (connectionId == 28)
			{
				this.LabLaunchingDownload = (TextBlock)target;
				return;
			}
			if (connectionId == 29)
			{
				this.PanLaunchingHint = (Grid)target;
				return;
			}
			if (connectionId == 30)
			{
				this.LabLaunchingHint = (TextBlock)target;
				return;
			}
			if (connectionId == 31)
			{
				this.BtnCancel = (MyButton)target;
				return;
			}
			this._UtilsMapper = true;
		}

		// Token: 0x04000897 RID: 2199
		private bool attributeMapper;

		// Token: 0x04000898 RID: 2200
		private bool m_CodeMapper;

		// Token: 0x04000899 RID: 2201
		private PageLaunchLeft.PageType m_PrototypeMapper;

		// Token: 0x0400089A RID: 2202
		public static ModLoader.LoaderTask<ModBase.EqualableList<string>, string> annotationMapper = new ModLoader.LoaderTask<ModBase.EqualableList<string>, string>("Loader Skin Ms", new Action<ModLoader.LoaderTask<ModBase.EqualableList<string>, string>>(PageLaunchLeft.SkinMsLoad), new Func<ModBase.EqualableList<string>>(PageLaunchLeft.SkinMsInput), ThreadPriority.AboveNormal);

		// Token: 0x0400089B RID: 2203
		public static ModLoader.LoaderTask<ModBase.EqualableList<string>, string> infoMapper = new ModLoader.LoaderTask<ModBase.EqualableList<string>, string>("Loader Skin Legacy", new Action<ModLoader.LoaderTask<ModBase.EqualableList<string>, string>>(PageLaunchLeft.SkinLegacyLoad), new Func<ModBase.EqualableList<string>>(PageLaunchLeft.SkinLegacyInput), ThreadPriority.AboveNormal);

		// Token: 0x0400089C RID: 2204
		public static ModLoader.LoaderTask<ModBase.EqualableList<string>, string> m_AdapterMapper = new ModLoader.LoaderTask<ModBase.EqualableList<string>, string>("Loader Skin Nide", new Action<ModLoader.LoaderTask<ModBase.EqualableList<string>, string>>(PageLaunchLeft.SkinNideLoad), new Func<ModBase.EqualableList<string>>(PageLaunchLeft.SkinNideInput), ThreadPriority.AboveNormal);

		// Token: 0x0400089D RID: 2205
		public static ModLoader.LoaderTask<ModBase.EqualableList<string>, string> m_FacadeMapper = new ModLoader.LoaderTask<ModBase.EqualableList<string>, string>("Loader Skin Auth", new Action<ModLoader.LoaderTask<ModBase.EqualableList<string>, string>>(PageLaunchLeft.SkinAuthLoad), new Func<ModBase.EqualableList<string>>(PageLaunchLeft.SkinAuthInput), ThreadPriority.AboveNormal);

		// Token: 0x0400089E RID: 2206
		public static List<ModLoader.LoaderTask<ModBase.EqualableList<string>, string>> m_ListMapper = new List<ModLoader.LoaderTask<ModBase.EqualableList<string>, string>>
		{
			PageLaunchLeft.annotationMapper,
			PageLaunchLeft.infoMapper,
			PageLaunchLeft.m_AdapterMapper,
			PageLaunchLeft.m_FacadeMapper
		};

		// Token: 0x0400089F RID: 2207
		private int m_MerchantMapper;

		// Token: 0x040008A0 RID: 2208
		private ModMinecraft.McVersion _AuthenticationMapper;

		// Token: 0x040008A1 RID: 2209
		private double algoMapper;

		// Token: 0x040008A2 RID: 2210
		private bool m_ComparatorMapper;

		// Token: 0x040008A3 RID: 2211
		private double mappingMapper;

		// Token: 0x040008A4 RID: 2212
		private bool m_TokenizerMapper;

		// Token: 0x040008A5 RID: 2213
		private double filterMapper;

		// Token: 0x040008A6 RID: 2214
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private PageLaunchLeft _DatabaseMapper;

		// Token: 0x040008A7 RID: 2215
		[AccessedThroughProperty("PanInput")]
		[CompilerGenerated]
		private Grid predicateMapper;

		// Token: 0x040008A8 RID: 2216
		[CompilerGenerated]
		[AccessedThroughProperty("BtnVersion")]
		private MyButton _PoolMapper;

		// Token: 0x040008A9 RID: 2217
		[CompilerGenerated]
		[AccessedThroughProperty("BtnMore")]
		private MyButton m_CustomerMapper;

		// Token: 0x040008AA RID: 2218
		[AccessedThroughProperty("PanLogin")]
		[CompilerGenerated]
		private Grid pageMapper;

		// Token: 0x040008AB RID: 2219
		[AccessedThroughProperty("PanTypeOne")]
		[CompilerGenerated]
		private Grid interceptorMapper;

		// Token: 0x040008AC RID: 2220
		[CompilerGenerated]
		[AccessedThroughProperty("PathTypeOne")]
		private Path _ContainerMapper;

		// Token: 0x040008AD RID: 2221
		[AccessedThroughProperty("LabTypeOne")]
		[CompilerGenerated]
		private TextBlock m_ParamsMapper;

		// Token: 0x040008AE RID: 2222
		[AccessedThroughProperty("PanType")]
		[CompilerGenerated]
		private Grid m_DispatcherMapper;

		// Token: 0x040008AF RID: 2223
		[AccessedThroughProperty("RadioLoginType5")]
		[CompilerGenerated]
		private MyRadioButton m_ProcessMapper;

		// Token: 0x040008B0 RID: 2224
		[CompilerGenerated]
		[AccessedThroughProperty("RadioLoginType0")]
		private MyRadioButton _ParameterMapper;

		// Token: 0x040008B1 RID: 2225
		[CompilerGenerated]
		[AccessedThroughProperty("AprilScaleTrans")]
		private ScaleTransform m_RecordMapper;

		// Token: 0x040008B2 RID: 2226
		[AccessedThroughProperty("AprilPosTrans")]
		[CompilerGenerated]
		private TranslateTransform m_ServiceMapper;

		// Token: 0x040008B3 RID: 2227
		[CompilerGenerated]
		[AccessedThroughProperty("BtnLaunch")]
		private MyButton invocationMapper;

		// Token: 0x040008B4 RID: 2228
		[AccessedThroughProperty("LabVersion")]
		[CompilerGenerated]
		private TextBlock m_ProxyMapper;

		// Token: 0x040008B5 RID: 2229
		[CompilerGenerated]
		[AccessedThroughProperty("PanLaunching")]
		private Grid _MessageMapper;

		// Token: 0x040008B6 RID: 2230
		[CompilerGenerated]
		[AccessedThroughProperty("LoadLaunching")]
		private MyLoading creatorMapper;

		// Token: 0x040008B7 RID: 2231
		[AccessedThroughProperty("LabLaunchingTitle")]
		[CompilerGenerated]
		private TextBlock _InitializerMapper;

		// Token: 0x040008B8 RID: 2232
		[CompilerGenerated]
		[AccessedThroughProperty("LabLaunchingName")]
		private TextBlock singletonMapper;

		// Token: 0x040008B9 RID: 2233
		[AccessedThroughProperty("ProgressLaunchingFinished")]
		[CompilerGenerated]
		private ColumnDefinition m_RegMapper;

		// Token: 0x040008BA RID: 2234
		[CompilerGenerated]
		[AccessedThroughProperty("ProgressLaunchingUnfinished")]
		private ColumnDefinition m_ProductMapper;

		// Token: 0x040008BB RID: 2235
		[CompilerGenerated]
		[AccessedThroughProperty("PanLaunchingInfo")]
		private Grid m_ListenerMapper;

		// Token: 0x040008BC RID: 2236
		[CompilerGenerated]
		[AccessedThroughProperty("LabLaunchingStage")]
		private TextBlock collectionMapper;

		// Token: 0x040008BD RID: 2237
		[CompilerGenerated]
		[AccessedThroughProperty("LabLaunchingMethod")]
		private TextBlock visitorMapper;

		// Token: 0x040008BE RID: 2238
		[AccessedThroughProperty("LabLaunchingProgressLeft")]
		[CompilerGenerated]
		private TextBlock _ValueMapper;

		// Token: 0x040008BF RID: 2239
		[CompilerGenerated]
		[AccessedThroughProperty("LabLaunchingProgress")]
		private TextBlock objectMapper;

		// Token: 0x040008C0 RID: 2240
		[AccessedThroughProperty("LabLaunchingDownloadLeft")]
		[CompilerGenerated]
		private TextBlock _BridgeMapper;

		// Token: 0x040008C1 RID: 2241
		[AccessedThroughProperty("LabLaunchingDownload")]
		[CompilerGenerated]
		private TextBlock _ItemMapper;

		// Token: 0x040008C2 RID: 2242
		[CompilerGenerated]
		[AccessedThroughProperty("PanLaunchingHint")]
		private Grid m_ReponseMapper;

		// Token: 0x040008C3 RID: 2243
		[CompilerGenerated]
		[AccessedThroughProperty("LabLaunchingHint")]
		private TextBlock globalMapper;

		// Token: 0x040008C4 RID: 2244
		[AccessedThroughProperty("BtnCancel")]
		[CompilerGenerated]
		private MyButton _ExceptionMapper;

		// Token: 0x040008C5 RID: 2245
		private bool _UtilsMapper;

		// Token: 0x02000198 RID: 408
		private enum PageType
		{
			// Token: 0x040008C7 RID: 2247
			None,
			// Token: 0x040008C8 RID: 2248
			Legacy,
			// Token: 0x040008C9 RID: 2249
			Nide,
			// Token: 0x040008CA RID: 2250
			NideSkin,
			// Token: 0x040008CB RID: 2251
			Auth,
			// Token: 0x040008CC RID: 2252
			AuthSkin,
			// Token: 0x040008CD RID: 2253
			Ms,
			// Token: 0x040008CE RID: 2254
			MsSkin
		}
	}
}
