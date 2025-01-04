using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace PCL
{
	// Token: 0x020000E2 RID: 226
	[DesignerGenerated]
	public class PageDownloadInstall : MyPageRight, IComponentConnector
	{
		// Token: 0x0600073C RID: 1852 RVA: 0x0003F2B8 File Offset: 0x0003D4B8
		public PageDownloadInstall()
		{
			base.Initialized += delegate(object sender, EventArgs e)
			{
				this.LoaderInit();
			};
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.Init();
			};
			this.m_DefinitionField = false;
			this.strategyField = false;
			this.m_ProcField = false;
			this.readerReader = null;
			this.m_ClientReader = null;
			this._ConfigReader = null;
			this.m_TestsReader = null;
			this.m_MapperReader = null;
			this.m_ThreadReader = null;
			this._PropertyReader = null;
			this.composerReader = false;
			this.iteratorReader = false;
			this.m_RepositoryReader = false;
			this._ContextReader = false;
			this._SpecificationReader = false;
			this.InitializeComponent();
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0003F360 File Offset: 0x0003D560
		private void LoaderInit()
		{
			base.PageLoaderInit(this.LoadMinecraft, this.PanLoad, this.PanBack, null, ModDownload.accountTests, delegate(ModLoader.LoaderBase a0)
			{
				this.LoadMinecraft_OnFinish();
			}, null, true);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0003F39C File Offset: 0x0003D59C
		private void Init()
		{
			this.PanBack.ScrollToHome();
			ModDownload.m_ModelTests.Start(null, false);
			ModDownload._FacadeTests.Start(null, false);
			ModDownload.authenticationTests.Start(null, false);
			ModDownload.m_AnnotationTests.Start(null, false);
			this.TextSelectName.ValidateRules = new Collection<Validate>
			{
				new ValidateFolderName(ModMinecraft.m_ProxyTests + "versions", true, true)
			};
			this.TextSelectName.Validate();
			this.SelectReload();
			if (!this.m_DefinitionField)
			{
				this.m_DefinitionField = true;
				ModDownloadLib.McDownloadForgeRecommendedRefresh();
				this.LoadOptiFine.State = ModDownload.m_ModelTests;
				this.LoadLiteLoader.State = ModDownload._FacadeTests;
				this.LoadFabric.State = ModDownload.authenticationTests;
				this.LoadFabricApi.State = ModDownload.mappingTests;
				this.LoadNeoForge.State = ModDownload.m_AnnotationTests;
				this.LoadOptiFabric.State = ModDownload.tokenizerTests;
			}
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0003F498 File Offset: 0x0003D698
		private void EnterSelectPage()
		{
			if (!this.strategyField)
			{
				this.strategyField = true;
				this._ContextReader = false;
				this._SpecificationReader = false;
				this.iteratorReader = false;
				this.PanSelect.Visibility = Visibility.Visible;
				this.PanSelect.IsHitTestVisible = true;
				this.PanMinecraft.IsHitTestVisible = false;
				this.PanBack.IsHitTestVisible = false;
				this.PanBack.ScrollToHome();
				this.CardMinecraft.IsSwaped = true;
				this.CardOptiFine.IsSwaped = true;
				this.CardLiteLoader.IsSwaped = true;
				this.CardForge.IsSwaped = true;
				this.CardNeoForge.IsSwaped = true;
				this.CardFabric.IsSwaped = true;
				this.CardFabricApi.IsSwaped = true;
				this.CardOptiFabric.IsSwaped = true;
				if (Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("HintInstallBack", null))))
				{
					ModBase.m_IdentifierRepository.Set("HintInstallBack", true, false, null);
					ModMain.Hint("点击 Minecraft 项即可返回游戏主版本选择页面！", ModMain.HintType.Info, true);
				}
				try
				{
					foreach (FrameworkElement frameworkElement in base.GetAllAnimControls(this.PanSelect, false))
					{
						frameworkElement.Opacity = 1.0;
						frameworkElement.RenderTransform = new TranslateTransform();
					}
				}
				finally
				{
					List<FrameworkElement>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				if (this.parserReader.StartsWith("1."))
				{
					ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> loaderTask = new ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>("DlForgeVersion " + this.parserReader, new Action<ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>>(ModDownload.DlForgeVersionMain), null, ThreadPriority.Normal);
					this.LoadForge.State = loaderTask;
					loaderTask.Start(this.parserReader, false);
				}
				ModDownload.mappingTests.Start(null, false);
				ModDownload.tokenizerTests.Start(null, false);
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaOpacity(this.PanMinecraft, -this.PanMinecraft.Opacity, 100, 10, null, false),
					ModAnimation.AaTranslateX(this.PanMinecraft, -50.0 - ((TranslateTransform)this.PanMinecraft.RenderTransform).X, 110, 10, null, false),
					ModAnimation.AaCode(delegate
					{
						this.PanBack.ScrollToHome();
						this.TextSelectName.Validate();
						this.OptiFine_Loaded();
						this.LiteLoader_Loaded();
						this.Forge_Loaded();
						this.NeoForge_Loaded();
						this.Fabric_Loaded();
						this.FabricApi_Loaded();
						this.OptiFabric_Loaded();
						this.SelectReload();
					}, 0, true),
					ModAnimation.AaOpacity(this.PanSelect, 1.0 - this.PanSelect.Opacity, 250, 150, null, false),
					ModAnimation.AaTranslateX(this.PanSelect, -((TranslateTransform)this.PanSelect.RenderTransform).X, 500, 150, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false),
					ModAnimation.AaCode(delegate
					{
						this.PanMinecraft.Visibility = Visibility.Collapsed;
						this.PanBack.IsHitTestVisible = true;
						if (!this.m_ProcField)
						{
							this.m_ProcField = true;
							this.BtnOptiFineClearInner.SetBinding(Shape.FillProperty, new Binding("Foreground")
							{
								Source = this.CardOptiFine.RestartParser(),
								Mode = BindingMode.OneWay
							});
							this.BtnLiteLoaderClearInner.SetBinding(Shape.FillProperty, new Binding("Foreground")
							{
								Source = this.CardLiteLoader.RestartParser(),
								Mode = BindingMode.OneWay
							});
							this.BtnForgeClearInner.SetBinding(Shape.FillProperty, new Binding("Foreground")
							{
								Source = this.CardForge.RestartParser(),
								Mode = BindingMode.OneWay
							});
							this.BtnNeoForgeClearInner.SetBinding(Shape.FillProperty, new Binding("Foreground")
							{
								Source = this.CardNeoForge.RestartParser(),
								Mode = BindingMode.OneWay
							});
							this.BtnFabricClearInner.SetBinding(Shape.FillProperty, new Binding("Foreground")
							{
								Source = this.CardFabric.RestartParser(),
								Mode = BindingMode.OneWay
							});
							this.BtnFabricApiClearInner.SetBinding(Shape.FillProperty, new Binding("Foreground")
							{
								Source = this.CardFabricApi.RestartParser(),
								Mode = BindingMode.OneWay
							});
							this.BtnOptiFabricClearInner.SetBinding(Shape.FillProperty, new Binding("Foreground")
							{
								Source = this.CardOptiFabric.RestartParser(),
								Mode = BindingMode.OneWay
							});
						}
					}, 0, true)
				}, "FrmDownloadInstall SelectPageSwitch", true);
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0003F778 File Offset: 0x0003D978
		public void ExitSelectPage()
		{
			if (this.strategyField)
			{
				this.strategyField = false;
				this.SelectClear();
				this.PanMinecraft.Visibility = Visibility.Visible;
				this.PanSelect.IsHitTestVisible = false;
				this.PanMinecraft.IsHitTestVisible = true;
				this.PanBack.IsHitTestVisible = false;
				this.PanBack.ScrollToHome();
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaOpacity(this.PanSelect, -this.PanSelect.Opacity, 90, 10, null, false),
					ModAnimation.AaTranslateX(this.PanSelect, 50.0 - ((TranslateTransform)this.PanSelect.RenderTransform).X, 100, 10, null, false),
					ModAnimation.AaCode(delegate
					{
						this.PanBack.ScrollToHome();
					}, 0, true),
					ModAnimation.AaOpacity(this.PanMinecraft, 1.0 - this.PanMinecraft.Opacity, 150, 100, null, false),
					ModAnimation.AaTranslateX(this.PanMinecraft, -((TranslateTransform)this.PanMinecraft.RenderTransform).X, 400, 100, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), false),
					ModAnimation.AaCode(delegate
					{
						this.PanSelect.Visibility = Visibility.Collapsed;
						this.PanBack.IsHitTestVisible = true;
					}, 0, true)
				}, "FrmDownloadInstall SelectPageSwitch", false);
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0003F8E0 File Offset: 0x0003DAE0
		public void MinecraftSelected(MyListItem sender, MouseButtonEventArgs e)
		{
			this.parserReader = sender.Title;
			this.m_BroadcasterReader = NewLateBinding.LateIndexGet(sender.Tag, new object[]
			{
				"url"
			}, null).ToString();
			this._FieldReader = sender.Logo;
			this.EnterSelectPage();
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00005B8E File Offset: 0x00003D8E
		private void CardMinecraft_PreviewSwap(object sender, ModBase.RouteEventArgs e)
		{
			this.ExitSelectPage();
			e.m_SerializerError = true;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0003F930 File Offset: 0x0003DB30
		private void SetOptiFineInfoShow(string IsShow)
		{
			if (!Operators.ConditionalCompareObjectEqual(this.PanOptiFineInfo.Tag, IsShow, false))
			{
				this.PanOptiFineInfo.Tag = IsShow;
				if (Operators.CompareString(IsShow, "True", false) == 0)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaTranslateY(this.PanOptiFineInfo, -((TranslateTransform)this.PanOptiFineInfo.RenderTransform).Y, 270, 100, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Middle), false),
						ModAnimation.AaOpacity(this.PanOptiFineInfo, 1.0 - this.PanOptiFineInfo.Opacity, 100, 90, null, false)
					}, "SetOptiFineInfoShow", false);
					return;
				}
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaTranslateY(this.PanOptiFineInfo, 6.0 - ((TranslateTransform)this.PanOptiFineInfo.RenderTransform).Y, 200, 0, null, false),
					ModAnimation.AaOpacity(this.PanOptiFineInfo, -this.PanOptiFineInfo.Opacity, 100, 0, null, false)
				}, "SetOptiFineInfoShow", false);
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0003FA50 File Offset: 0x0003DC50
		private void SetLiteLoaderInfoShow(string IsShow)
		{
			if (!Operators.ConditionalCompareObjectEqual(this.PanLiteLoaderInfo.Tag, IsShow, false))
			{
				this.PanLiteLoaderInfo.Tag = IsShow;
				if (Operators.CompareString(IsShow, "True", false) == 0)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaTranslateY(this.PanLiteLoaderInfo, -((TranslateTransform)this.PanLiteLoaderInfo.RenderTransform).Y, 270, 100, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Middle), false),
						ModAnimation.AaOpacity(this.PanLiteLoaderInfo, 1.0 - this.PanLiteLoaderInfo.Opacity, 100, 90, null, false)
					}, "SetLiteLoaderInfoShow", false);
					return;
				}
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaTranslateY(this.PanLiteLoaderInfo, 6.0 - ((TranslateTransform)this.PanLiteLoaderInfo.RenderTransform).Y, 200, 0, null, false),
					ModAnimation.AaOpacity(this.PanLiteLoaderInfo, -this.PanLiteLoaderInfo.Opacity, 100, 0, null, false)
				}, "SetLiteLoaderInfoShow", false);
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0003FB70 File Offset: 0x0003DD70
		private void SetForgeInfoShow(string IsShow)
		{
			if (!Operators.ConditionalCompareObjectEqual(this.PanForgeInfo.Tag, IsShow, false))
			{
				this.PanForgeInfo.Tag = IsShow;
				if (Operators.CompareString(IsShow, "True", false) == 0)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaTranslateY(this.PanForgeInfo, -((TranslateTransform)this.PanForgeInfo.RenderTransform).Y, 270, 100, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Middle), false),
						ModAnimation.AaOpacity(this.PanForgeInfo, 1.0 - this.PanForgeInfo.Opacity, 100, 90, null, false)
					}, "SetForgeInfoShow", false);
					return;
				}
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaTranslateY(this.PanForgeInfo, 6.0 - ((TranslateTransform)this.PanForgeInfo.RenderTransform).Y, 200, 0, null, false),
					ModAnimation.AaOpacity(this.PanForgeInfo, -this.PanForgeInfo.Opacity, 100, 0, null, false)
				}, "SetForgeInfoShow", false);
			}
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0003FC90 File Offset: 0x0003DE90
		private void SetNeoForgeInfoShow(string IsShow)
		{
			if (!Operators.ConditionalCompareObjectEqual(this.PanNeoForgeInfo.Tag, IsShow, false))
			{
				this.PanNeoForgeInfo.Tag = IsShow;
				if (Operators.CompareString(IsShow, "True", false) == 0)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaTranslateY(this.PanNeoForgeInfo, -((TranslateTransform)this.PanNeoForgeInfo.RenderTransform).Y, 270, 100, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Middle), false),
						ModAnimation.AaOpacity(this.PanNeoForgeInfo, 1.0 - this.PanNeoForgeInfo.Opacity, 100, 90, null, false)
					}, "SetNeoForgeInfoShow", false);
					return;
				}
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaTranslateY(this.PanNeoForgeInfo, 6.0 - ((TranslateTransform)this.PanNeoForgeInfo.RenderTransform).Y, 200, 0, null, false),
					ModAnimation.AaOpacity(this.PanNeoForgeInfo, -this.PanNeoForgeInfo.Opacity, 100, 0, null, false)
				}, "SetNeoForgeInfoShow", false);
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0003FDB0 File Offset: 0x0003DFB0
		private void SetFabricInfoShow(string IsShow)
		{
			if (!Operators.ConditionalCompareObjectEqual(this.PanFabricInfo.Tag, IsShow, false))
			{
				this.PanFabricInfo.Tag = IsShow;
				if (Operators.CompareString(IsShow, "True", false) == 0)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaTranslateY(this.PanFabricInfo, -((TranslateTransform)this.PanFabricInfo.RenderTransform).Y, 270, 100, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Middle), false),
						ModAnimation.AaOpacity(this.PanFabricInfo, 1.0 - this.PanFabricInfo.Opacity, 100, 90, null, false)
					}, "SetFabricInfoShow", false);
					return;
				}
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaTranslateY(this.PanFabricInfo, 6.0 - ((TranslateTransform)this.PanFabricInfo.RenderTransform).Y, 200, 0, null, false),
					ModAnimation.AaOpacity(this.PanFabricInfo, -this.PanFabricInfo.Opacity, 100, 0, null, false)
				}, "SetFabricInfoShow", false);
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0003FED0 File Offset: 0x0003E0D0
		private void SetFabricApiInfoShow(string IsShow)
		{
			if (!Operators.ConditionalCompareObjectEqual(this.PanFabricApiInfo.Tag, IsShow, false))
			{
				this.PanFabricApiInfo.Tag = IsShow;
				if (Operators.CompareString(IsShow, "True", false) == 0)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaTranslateY(this.PanFabricApiInfo, -((TranslateTransform)this.PanFabricApiInfo.RenderTransform).Y, 270, 100, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Middle), false),
						ModAnimation.AaOpacity(this.PanFabricApiInfo, 1.0 - this.PanFabricApiInfo.Opacity, 100, 90, null, false)
					}, "SetFabricApiInfoShow", false);
					return;
				}
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaTranslateY(this.PanFabricApiInfo, 6.0 - ((TranslateTransform)this.PanFabricApiInfo.RenderTransform).Y, 200, 0, null, false),
					ModAnimation.AaOpacity(this.PanFabricApiInfo, -this.PanFabricApiInfo.Opacity, 100, 0, null, false)
				}, "SetFabricApiInfoShow", false);
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0003FFF0 File Offset: 0x0003E1F0
		private void SetOptiFabricInfoShow(string IsShow)
		{
			if (!Operators.ConditionalCompareObjectEqual(this.PanOptiFabricInfo.Tag, IsShow, false))
			{
				this.PanOptiFabricInfo.Tag = IsShow;
				if (Operators.CompareString(IsShow, "True", false) == 0)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaTranslateY(this.PanOptiFabricInfo, -((TranslateTransform)this.PanOptiFabricInfo.RenderTransform).Y, 270, 100, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Middle), false),
						ModAnimation.AaOpacity(this.PanOptiFabricInfo, 1.0 - this.PanOptiFabricInfo.Opacity, 100, 90, null, false)
					}, "SetOptiFabricInfoShow", false);
					return;
				}
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaTranslateY(this.PanOptiFabricInfo, 6.0 - ((TranslateTransform)this.PanOptiFabricInfo.RenderTransform).Y, 200, 0, null, false),
					ModAnimation.AaOpacity(this.PanOptiFabricInfo, -this.PanOptiFabricInfo.Opacity, 100, 0, null, false)
				}, "SetOptiFabricInfoShow", false);
			}
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00040110 File Offset: 0x0003E310
		private void SelectReload()
		{
			if (this.parserReader != null && !this.composerReader)
			{
				this.composerReader = true;
				this.SelectNameUpdate();
				this.ItemSelect.Title = this.TextSelectName.Text;
				this.ItemSelect.Info = this.GetSelectInfo();
				this.ItemSelect.Logo = this.GetSelectLogo();
				this.LabMinecraft.Text = this.parserReader;
				this.ImgMinecraft.Source = new MyBitmap(this._FieldReader);
				string text = this.LoadOptiFineGetError();
				this.CardOptiFine.SearchParser().Visibility = ((text == null) ? Visibility.Visible : Visibility.Collapsed);
				if (text != null)
				{
					this.CardOptiFine.IsSwaped = true;
				}
				this.SetOptiFineInfoShow(Conversions.ToString(this.CardOptiFine.IsSwaped));
				if (this.readerReader == null)
				{
					this.BtnOptiFineClear.Visibility = Visibility.Collapsed;
					this.ImgOptiFine.Visibility = Visibility.Collapsed;
					this.LabOptiFine.Text = (text ?? "点击选择");
					this.LabOptiFine.Foreground = ModSecret.m_StateField;
				}
				else
				{
					this.BtnOptiFineClear.Visibility = Visibility.Visible;
					this.ImgOptiFine.Visibility = Visibility.Visible;
					this.LabOptiFine.Text = this.readerReader.m_SchemaTest.Replace(this.parserReader + " ", "");
					this.LabOptiFine.Foreground = ModSecret.m_RefField;
				}
				if (this.parserReader.Contains("1.") && ModBase.Val(this.parserReader.Split(".")[1]) <= 12.0)
				{
					this.CardLiteLoader.Visibility = Visibility.Visible;
					string text2 = this.LoadLiteLoaderGetError();
					this.CardLiteLoader.SearchParser().Visibility = ((text2 == null) ? Visibility.Visible : Visibility.Collapsed);
					if (text2 != null)
					{
						this.CardLiteLoader.IsSwaped = true;
					}
					this.SetLiteLoaderInfoShow(Conversions.ToString(this.CardLiteLoader.IsSwaped));
					if (this.m_ClientReader == null)
					{
						this.BtnLiteLoaderClear.Visibility = Visibility.Collapsed;
						this.ImgLiteLoader.Visibility = Visibility.Collapsed;
						this.LabLiteLoader.Text = (text2 ?? "点击选择");
						this.LabLiteLoader.Foreground = ModSecret.m_StateField;
					}
					else
					{
						this.BtnLiteLoaderClear.Visibility = Visibility.Visible;
						this.ImgLiteLoader.Visibility = Visibility.Visible;
						this.LabLiteLoader.Text = this.m_ClientReader.Inherit;
						this.LabLiteLoader.Foreground = ModSecret.m_RefField;
					}
				}
				else
				{
					this.CardLiteLoader.Visibility = Visibility.Collapsed;
				}
				string text3 = this.LoadForgeGetError();
				this.CardForge.SearchParser().Visibility = ((text3 == null) ? Visibility.Visible : Visibility.Collapsed);
				if (text3 != null)
				{
					this.CardForge.IsSwaped = true;
				}
				this.SetForgeInfoShow(Conversions.ToString(this.CardForge.IsSwaped));
				if (this._ConfigReader == null)
				{
					this.BtnForgeClear.Visibility = Visibility.Collapsed;
					this.ImgForge.Visibility = Visibility.Collapsed;
					this.LabForge.Text = (text3 ?? "点击选择");
					this.LabForge.Foreground = ModSecret.m_StateField;
				}
				else
				{
					this.BtnForgeClear.Visibility = Visibility.Visible;
					this.ImgForge.Visibility = Visibility.Visible;
					this.LabForge.Text = this._ConfigReader.m_ConfigMap;
					this.LabForge.Foreground = ModSecret.m_RefField;
				}
				if (this.parserReader.Contains("1.") && ModBase.Val(this.parserReader.Split(".")[1]) > 19.0)
				{
					this.CardNeoForge.Visibility = Visibility.Visible;
					string text4 = this.LoadNeoForgeGetError();
					this.CardNeoForge.SearchParser().Visibility = ((text4 == null) ? Visibility.Visible : Visibility.Collapsed);
					if (text4 != null)
					{
						this.CardNeoForge.IsSwaped = true;
					}
					this.SetNeoForgeInfoShow(Conversions.ToString(this.CardNeoForge.IsSwaped));
					if (this.m_TestsReader == null)
					{
						this.BtnNeoForgeClear.Visibility = Visibility.Collapsed;
						this.ImgNeoForge.Visibility = Visibility.Collapsed;
						this.LabNeoForge.Text = (text4 ?? "点击选择");
						this.LabNeoForge.Foreground = ModSecret.m_StateField;
					}
					else
					{
						this.BtnNeoForgeClear.Visibility = Visibility.Visible;
						this.ImgNeoForge.Visibility = Visibility.Visible;
						this.LabNeoForge.Text = this.m_TestsReader.m_ConfigMap;
						this.LabNeoForge.Foreground = ModSecret.m_RefField;
					}
				}
				else
				{
					this.CardNeoForge.Visibility = Visibility.Collapsed;
				}
				if (this.parserReader.Contains("1.") && ModBase.Val(this.parserReader.Split(".")[1]) <= 13.0)
				{
					this.CardFabric.Visibility = Visibility.Collapsed;
				}
				else
				{
					this.CardFabric.Visibility = Visibility.Visible;
					string text5 = this.LoadFabricGetError();
					this.CardFabric.SearchParser().Visibility = ((text5 == null) ? Visibility.Visible : Visibility.Collapsed);
					if (text5 != null)
					{
						this.CardFabric.IsSwaped = true;
					}
					this.SetFabricInfoShow(Conversions.ToString(this.CardFabric.IsSwaped));
					if (this.m_MapperReader == null)
					{
						this.BtnFabricClear.Visibility = Visibility.Collapsed;
						this.ImgFabric.Visibility = Visibility.Collapsed;
						this.LabFabric.Text = (text5 ?? "点击选择");
						this.LabFabric.Foreground = ModSecret.m_StateField;
					}
					else
					{
						this.BtnFabricClear.Visibility = Visibility.Visible;
						this.ImgFabric.Visibility = Visibility.Visible;
						this.LabFabric.Text = this.m_MapperReader.Replace("+build", "");
						this.LabFabric.Foreground = ModSecret.m_RefField;
					}
				}
				if (this.m_MapperReader == null)
				{
					this.CardFabricApi.Visibility = Visibility.Collapsed;
				}
				else
				{
					this.CardFabricApi.Visibility = Visibility.Visible;
					string text6 = this.LoadFabricApiGetError();
					this.CardFabricApi.SearchParser().Visibility = ((text6 == null) ? Visibility.Visible : Visibility.Collapsed);
					if (text6 != null || this.m_MapperReader == null)
					{
						this.CardFabricApi.IsSwaped = true;
					}
					this.SetFabricApiInfoShow(Conversions.ToString(this.CardFabricApi.IsSwaped));
					if (this.m_ThreadReader == null)
					{
						this.BtnFabricApiClear.Visibility = Visibility.Collapsed;
						this.ImgFabricApi.Visibility = Visibility.Collapsed;
						this.LabFabricApi.Text = (text6 ?? "点击选择");
						this.LabFabricApi.Foreground = ModSecret.m_StateField;
					}
					else
					{
						this.BtnFabricApiClear.Visibility = Visibility.Visible;
						this.ImgFabricApi.Visibility = Visibility.Visible;
						this.LabFabricApi.Text = Enumerable.First<string>(this.m_ThreadReader._ProductRepository.Split("]")[1].Replace("Fabric API ", "").Replace(" build ", ".").Split("+")).Trim();
						this.LabFabricApi.Foreground = ModSecret.m_RefField;
					}
				}
				if (this.m_MapperReader != null && this.readerReader != null)
				{
					this.CardOptiFabric.Visibility = Visibility.Visible;
					string text7 = this.LoadOptiFabricGetError();
					this.CardOptiFabric.SearchParser().Visibility = ((text7 == null) ? Visibility.Visible : Visibility.Collapsed);
					if (text7 != null || this.m_MapperReader == null)
					{
						this.CardOptiFabric.IsSwaped = true;
					}
					this.SetOptiFabricInfoShow(Conversions.ToString(this.CardOptiFabric.IsSwaped));
					if (this._PropertyReader == null)
					{
						this.BtnOptiFabricClear.Visibility = Visibility.Collapsed;
						this.ImgOptiFabric.Visibility = Visibility.Collapsed;
						this.LabOptiFabric.Text = (text7 ?? "点击选择");
						this.LabOptiFabric.Foreground = ModSecret.m_StateField;
					}
					else
					{
						this.BtnOptiFabricClear.Visibility = Visibility.Visible;
						this.ImgOptiFabric.Visibility = Visibility.Visible;
						this.LabOptiFabric.Text = this._PropertyReader._ProductRepository.ToLower().Replace("optifabric-", "").Replace(".jar", "").Trim().TrimStart(new char[]
						{
							'v'
						});
						this.LabOptiFabric.Foreground = ModSecret.m_RefField;
					}
				}
				else
				{
					this.CardOptiFabric.Visibility = Visibility.Collapsed;
				}
				if (this.m_MapperReader != null && this.m_ThreadReader == null)
				{
					this.HintFabricAPI.Visibility = Visibility.Visible;
				}
				else
				{
					this.HintFabricAPI.Visibility = Visibility.Collapsed;
				}
				if (this.m_MapperReader != null && this.readerReader != null && this._PropertyReader == null)
				{
					if (!this.parserReader.StartsWith("1.14") && !this.parserReader.StartsWith("1.15"))
					{
						this.HintOptiFabric.Visibility = Visibility.Visible;
						this.HintOptiFabricOld.Visibility = Visibility.Collapsed;
					}
					else
					{
						this.HintOptiFabric.Visibility = Visibility.Collapsed;
						this.HintOptiFabricOld.Visibility = Visibility.Visible;
					}
				}
				else
				{
					this.HintOptiFabric.Visibility = Visibility.Collapsed;
					this.HintOptiFabricOld.Visibility = Visibility.Collapsed;
				}
				if (this.parserReader.Contains("1.") && ModBase.Val(this.parserReader.Split(".")[1]) >= 16.0 && this.readerReader != null && (this._ConfigReader != null || this.m_MapperReader != null))
				{
					this.HintModOptiFine.Visibility = Visibility.Visible;
				}
				else
				{
					this.HintModOptiFine.Visibility = Visibility.Collapsed;
				}
				this.composerReader = false;
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00040A8C File Offset: 0x0003EC8C
		private void SelectClear()
		{
			this.parserReader = null;
			this.m_BroadcasterReader = null;
			this._FieldReader = null;
			this.readerReader = null;
			this.m_ClientReader = null;
			this._ConfigReader = null;
			this.m_TestsReader = null;
			this.m_MapperReader = null;
			this.m_ThreadReader = null;
			this._PropertyReader = null;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00040AE0 File Offset: 0x0003ECE0
		private string GetSelectName()
		{
			string text = this.parserReader;
			if (this.m_MapperReader != null)
			{
				text = text + "-Fabric " + this.m_MapperReader.Replace("+build", "");
			}
			if (this._ConfigReader != null)
			{
				text = text + "-Forge_" + this._ConfigReader.m_ConfigMap;
			}
			if (this.m_TestsReader != null)
			{
				text = text + "-NeoForge_" + this.m_TestsReader.m_ConfigMap;
			}
			if (this.m_ClientReader != null)
			{
				text += "-LiteLoader";
			}
			if (this.readerReader != null)
			{
				text = text + "-OptiFine_" + this.readerReader.m_SchemaTest.Replace(this.parserReader + " ", "").Replace(" ", "_");
			}
			return text;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00040BB8 File Offset: 0x0003EDB8
		private string GetSelectInfo()
		{
			string text = "";
			if (this.m_MapperReader != null)
			{
				text = text + ", Fabric " + this.m_MapperReader.Replace("+build", "");
			}
			if (this._ConfigReader != null)
			{
				text = text + ", Forge " + this._ConfigReader.m_ConfigMap;
			}
			if (this.m_TestsReader != null)
			{
				text = text + ", NeoForge " + this.m_TestsReader.m_ConfigMap;
			}
			if (this.m_ClientReader != null)
			{
				text += ", LiteLoader";
			}
			if (this.readerReader != null)
			{
				text = text + ", OptiFine " + this.readerReader.m_SchemaTest.Replace(this.parserReader + " ", "");
			}
			if (Operators.CompareString(text, "", false) == 0)
			{
				text = ", 无附加安装";
			}
			return text.TrimStart(", ".ToCharArray());
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00040CA4 File Offset: 0x0003EEA4
		private string GetSelectLogo()
		{
			string result;
			if (this.m_MapperReader != null)
			{
				result = "pack://application:,,,/images/Blocks/Fabric.png";
			}
			else if (this._ConfigReader != null)
			{
				result = "pack://application:,,,/images/Blocks/Anvil.png";
			}
			else if (this.m_TestsReader != null)
			{
				result = "pack://application:,,,/images/Blocks/NeoForge.png";
			}
			else if (this.m_ClientReader != null)
			{
				result = "pack://application:,,,/images/Blocks/Egg.png";
			}
			else if (this.readerReader != null)
			{
				result = "pack://application:,,,/images/Blocks/GrassPath.png";
			}
			else
			{
				result = this._FieldReader;
			}
			return result;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00005B9D File Offset: 0x00003D9D
		private void SelectNameUpdate()
		{
			if (!this.iteratorReader && !this.m_RepositoryReader)
			{
				this.m_RepositoryReader = true;
				this.TextSelectName.Text = this.GetSelectName();
				this.m_RepositoryReader = false;
			}
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00005BCE File Offset: 0x00003DCE
		private void TextSelectName_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!this.m_RepositoryReader)
			{
				this.iteratorReader = true;
				this.SelectReload();
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00005BE5 File Offset: 0x00003DE5
		private void TextSelectName_ValidateChanged(object sender, EventArgs e)
		{
			this.BtnSelectStart.IsEnabled = (Operators.CompareString(this.TextSelectName.ValidateResult, "", false) == 0);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00040D0C File Offset: 0x0003EF0C
		private void LoadMinecraft_OnFinish()
		{
			this.ExitSelectPage();
			checked
			{
				try
				{
					Dictionary<string, List<JObject>> dictionary = new Dictionary<string, List<JObject>>
					{
						{
							"正式版",
							new List<JObject>()
						},
						{
							"预览版",
							new List<JObject>()
						},
						{
							"远古版",
							new List<JObject>()
						},
						{
							"愚人节版",
							new List<JObject>()
						}
					};
					JArray jarray = (JArray)ModDownload.accountTests.Output.Value["versions"];
					try
					{
						foreach (JToken jtoken in jarray)
						{
							JObject jobject = (JObject)jtoken;
							string text = (string)jobject["type"];
							if (Operators.CompareString(text, "release", false) != 0)
							{
								if (Operators.CompareString(text, "snapshot", false) != 0)
								{
									if (Operators.CompareString(text, "special", false) != 0)
									{
										text = "远古版";
									}
									else
									{
										text = "愚人节版";
									}
								}
								else
								{
									text = "预览版";
									if (jobject["id"].ToString().StartsWith("1.") && !jobject["id"].ToString().ToLower().Contains("combat") && !jobject["id"].ToString().ToLower().Contains("rc") && !jobject["id"].ToString().ToLower().Contains("experimental") && !jobject["id"].ToString().ToLower().Contains("pre"))
									{
										text = "正式版";
										jobject["type"] = "release";
									}
									string text2 = jobject["id"].ToString().ToLower();
									uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
									if (num <= 2819146544U)
									{
										if (num <= 2783260049U)
										{
											if (num != 673058499U)
											{
												if (num == 2783260049U)
												{
													if (Operators.CompareString(text2, "1.rv-pre1", false) == 0)
													{
														goto IL_3D5;
													}
												}
											}
											else if (Operators.CompareString(text2, "2.0", false) == 0)
											{
												goto IL_3D5;
											}
										}
										else if (num != 2812350463U)
										{
											if (num == 2819146544U)
											{
												if (Operators.CompareString(text2, "15w14a", false) == 0)
												{
													goto IL_3D5;
												}
											}
										}
										else if (Operators.CompareString(text2, "23w13a_or_b", false) == 0)
										{
											goto IL_3D5;
										}
									}
									else
									{
										if (num <= 3741345573U)
										{
											if (num != 3653809737U)
											{
												if (num != 3741345573U)
												{
													goto IL_374;
												}
												if (Operators.CompareString(text2, "20w14infinite", false) != 0)
												{
													goto IL_374;
												}
											}
											else if (Operators.CompareString(text2, "20w14∞", false) != 0)
											{
												goto IL_374;
											}
											text = "愚人节版";
											jobject["id"] = "20w14∞";
											jobject["type"] = "special";
											jobject.Add("lore", ModMinecraft.GetMcFoolName((string)jobject["id"]));
											goto IL_422;
										}
										if (num != 3766348906U)
										{
											if (num != 3830073160U)
											{
												if (num == 4285466550U)
												{
													if (Operators.CompareString(text2, "24w14potato", false) == 0)
													{
														goto IL_3D5;
													}
												}
											}
											else if (Operators.CompareString(text2, "22w13oneblockatatime", false) == 0)
											{
												goto IL_3D5;
											}
										}
										else if (Operators.CompareString(text2, "3d shareware v1.34", false) == 0)
										{
											goto IL_3D5;
										}
									}
									IL_374:
									DateTime dateTime = Extensions.Value<DateTime>(jobject["releaseTime"]).ToUniversalTime().AddHours(2.0);
									if (dateTime.Month == 4 && dateTime.Day == 1)
									{
										text = "愚人节版";
										jobject["type"] = "special";
										goto IL_422;
									}
									goto IL_422;
									IL_3D5:
									text = "愚人节版";
									jobject["type"] = "special";
									jobject.Add("lore", ModMinecraft.GetMcFoolName((string)jobject["id"]));
								}
							}
							else
							{
								text = "正式版";
							}
							IL_422:
							dictionary[text].Add(jobject);
						}
					}
					finally
					{
						IEnumerator<JToken> enumerator;
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
					int num2 = dictionary.Keys.Count - 1;
					for (int i = 0; i <= num2; i++)
					{
						dictionary[Enumerable.ElementAtOrDefault<string>(dictionary.Keys, i)] = Enumerable.ElementAtOrDefault<List<JObject>>(dictionary.Values, i).Sort((PageDownloadInstall._Closure$__.$I39-0 == null) ? (PageDownloadInstall._Closure$__.$I39-0 = ((JObject Left, JObject Right) => DateTime.Compare(Extensions.Value<DateTime>(Left["releaseTime"]), Extensions.Value<DateTime>(Right["releaseTime"])) > 0)) : PageDownloadInstall._Closure$__.$I39-0);
					}
					this.PanMinecraft.Children.Clear();
					MyCard myCard = new MyCard();
					myCard.Title = "最新版本";
					myCard.Margin = new Thickness(0.0, 15.0, 0.0, 15.0);
					myCard.CreateParser(2);
					MyCard myCard2 = myCard;
					List<JObject> list = new List<JObject>();
					JObject jobject2 = (JObject)dictionary["正式版"][0].DeepClone();
					jobject2["lore"] = "最新正式版，发布于 " + Extensions.Value<DateTime>(jobject2["releaseTime"]).ToString("yyyy'/'MM'/'dd HH':'mm");
					list.Add(jobject2);
					if (DateTime.Compare(Extensions.Value<DateTime>(dictionary["正式版"][0]["releaseTime"]), Extensions.Value<DateTime>(dictionary["预览版"][0]["releaseTime"])) < 0)
					{
						JObject jobject3 = (JObject)dictionary["预览版"][0].DeepClone();
						jobject3["lore"] = "最新预览版，发布于 " + Extensions.Value<DateTime>(jobject3["releaseTime"]).ToString("yyyy'/'MM'/'dd HH':'mm");
						list.Add(jobject3);
					}
					StackPanel element = new StackPanel
					{
						Margin = new Thickness(20.0, 40.0, 18.0, 0.0),
						VerticalAlignment = VerticalAlignment.Top,
						RenderTransform = new TranslateTransform(0.0, 0.0),
						Tag = list
					};
					MyCard.StackInstall(ref element, 7, "");
					myCard2.Children.Add(element);
					this.PanMinecraft.Children.Insert(0, myCard2);
					try
					{
						foreach (KeyValuePair<string, List<JObject>> keyValuePair in dictionary)
						{
							if (Enumerable.Any<JObject>(keyValuePair.Value))
							{
								MyCard myCard3 = new MyCard();
								myCard3.Title = keyValuePair.Key + " (" + Conversions.ToString(keyValuePair.Value.Count) + ")";
								myCard3.Margin = new Thickness(0.0, 0.0, 0.0, 15.0);
								myCard3.CreateParser(7);
								MyCard myCard4 = myCard3;
								StackPanel stackPanel = new StackPanel
								{
									Margin = new Thickness(20.0, 40.0, 18.0, 0.0),
									VerticalAlignment = VerticalAlignment.Top,
									RenderTransform = new TranslateTransform(0.0, 0.0),
									Tag = keyValuePair.Value
								};
								myCard4.Children.Add(stackPanel);
								myCard4._Stub = stackPanel;
								myCard4.IsSwaped = true;
								this.PanMinecraft.Children.Add(myCard4);
							}
						}
					}
					finally
					{
						Dictionary<string, List<JObject>>.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
					if (PageDownloadInstall._ErrorReader != null)
					{
						ModBase.Log("[Download] 自动选择 MC 版本：" + PageDownloadInstall._ErrorReader, ModBase.LogLevel.Normal, "出现错误");
						try
						{
							foreach (JToken jtoken2 in jarray)
							{
								JObject jobject4 = (JObject)jtoken2;
								if (Operators.CompareString(jobject4["id"].ToString(), PageDownloadInstall._ErrorReader, false) == 0)
								{
									MyListItem sender2 = ModDownloadLib.McDownloadListItem(jobject4, (PageDownloadInstall._Closure$__.$IR39-4 == null) ? (PageDownloadInstall._Closure$__.$IR39-4 = delegate(object sender, MouseButtonEventArgs e)
									{
										((PageDownloadInstall._Closure$__.$I39-1 == null) ? (PageDownloadInstall._Closure$__.$I39-1 = delegate()
										{
										}) : PageDownloadInstall._Closure$__.$I39-1)();
									}) : PageDownloadInstall._Closure$__.$IR39-4, false);
									this.MinecraftSelected(sender2, null);
								}
							}
						}
						finally
						{
							IEnumerator<JToken> enumerator3;
							if (enumerator3 != null)
							{
								enumerator3.Dispose();
							}
						}
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "可视化安装版本列表出错", ModBase.LogLevel.Feedback, "出现错误");
				}
			}
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00041638 File Offset: 0x0003F838
		private string LoadOptiFineGetError()
		{
			string result;
			if (this.m_TestsReader != null)
			{
				result = "与 NeoForge 不兼容";
			}
			else if (this.LoadOptiFine != null && this.LoadOptiFine.State.LoadingState != MyLoading.MyLoadingState.Run)
			{
				if (this.LoadOptiFine.State.LoadingState == MyLoading.MyLoadingState.Error)
				{
					result = Conversions.ToString(Operators.ConcatenateObject("获取版本列表失败：", NewLateBinding.LateGet(NewLateBinding.LateGet(this.LoadOptiFine.State, null, "Error", new object[0], null, null, null), null, "Message", new object[0], null, null, null)));
				}
				else if (this._ConfigReader != null && ModMinecraft.VersionSortInteger(this.parserReader, "1.13") >= 0 && ModMinecraft.VersionSortInteger("1.14.3", this.parserReader) >= 0)
				{
					result = "与 Forge 不兼容";
				}
				else
				{
					string text = "9999.9999";
					bool flag = false;
					try
					{
						foreach (ModDownload.DlOptiFineListEntry dlOptiFineListEntry in ModDownload.m_ModelTests.Output.Value)
						{
							if (dlOptiFineListEntry.m_SchemaTest.StartsWith(this.parserReader + " "))
							{
								if (this._ConfigReader == null)
								{
									return null;
								}
								if (Conversions.ToBoolean(this.IsOptiFineSuitForForge(dlOptiFineListEntry, this._ConfigReader)))
								{
									return null;
								}
								flag = true;
								if (dlOptiFineListEntry.parserMap != null)
								{
									text = (ModMinecraft.VersionSortBoolean(text, dlOptiFineListEntry.parserMap) ? dlOptiFineListEntry.parserMap : text);
								}
							}
						}
					}
					finally
					{
						List<ModDownload.DlOptiFineListEntry>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					if (Operators.CompareString(text, "9999.9999", false) == 0)
					{
						result = (flag ? "与 Forge 不兼容" : "没有可用版本");
					}
					else
					{
						result = "需要 Forge " + (text.Contains(".") ? "" : "#") + text + " 或更高版本";
					}
				}
			}
			else
			{
				result = "正在获取版本列表……";
			}
			return result;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00041818 File Offset: 0x0003FA18
		private object IsOptiFineSuitForForge(ModDownload.DlOptiFineListEntry OptiFine, ModDownload.DlForgeVersionEntry Forge)
		{
			object result;
			if (Operators.CompareString(Forge._TestsMap, OptiFine.CheckMapper(), false) != 0)
			{
				result = false;
			}
			else if (OptiFine.parserMap == null)
			{
				result = false;
			}
			else if (string.IsNullOrWhiteSpace(OptiFine.parserMap))
			{
				result = true;
			}
			else if (OptiFine.parserMap.Contains("."))
			{
				result = (ModMinecraft.VersionSortInteger(Forge.clientMap.ToString(), OptiFine.parserMap) >= 0);
			}
			else
			{
				result = ((double)Forge.clientMap.Revision >= Conversions.ToDouble(OptiFine.parserMap));
			}
			return result;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00005C0B File Offset: 0x00003E0B
		private void CardOptiFine_PreviewSwap(object sender, ModBase.RouteEventArgs e)
		{
			if (this.LoadOptiFineGetError() != null)
			{
				e.m_SerializerError = true;
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000418C4 File Offset: 0x0003FAC4
		private void OptiFine_Loaded()
		{
			try
			{
				if (ModDownload.m_ModelTests.State == ModBase.LoadState.Finished)
				{
					List<ModDownload.DlOptiFineListEntry> list = new List<ModDownload.DlOptiFineListEntry>();
					try
					{
						foreach (ModDownload.DlOptiFineListEntry dlOptiFineListEntry in ModDownload.m_ModelTests.Output.Value)
						{
							if (!Conversions.ToBoolean(this._ConfigReader != null && Conversions.ToBoolean(Operators.NotObject(this.IsOptiFineSuitForForge(dlOptiFineListEntry, this._ConfigReader)))) && dlOptiFineListEntry.m_SchemaTest.StartsWith(this.parserReader + " "))
							{
								list.Add(dlOptiFineListEntry);
							}
						}
					}
					finally
					{
						List<ModDownload.DlOptiFineListEntry>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					if (Enumerable.Any<ModDownload.DlOptiFineListEntry>(list))
					{
						list = list.Sort((PageDownloadInstall._Closure$__.$I44-0 == null) ? (PageDownloadInstall._Closure$__.$I44-0 = ((ModDownload.DlOptiFineListEntry Left, ModDownload.DlOptiFineListEntry Right) => (!Left._DefinitionTest && Right._DefinitionTest) || ((!Left._DefinitionTest || Right._DefinitionTest) && ModMinecraft.VersionSortBoolean(Left.m_SchemaTest, Right.m_SchemaTest)))) : PageDownloadInstall._Closure$__.$I44-0);
						this.PanOptiFine.Children.Clear();
						try
						{
							foreach (ModDownload.DlOptiFineListEntry entry in list)
							{
								this.PanOptiFine.Children.Add(ModDownloadLib.OptiFineDownloadListItem(entry, delegate(object sender, MouseButtonEventArgs e)
								{
									this.OptiFine_Selected((MyListItem)sender, e);
								}, false));
							}
						}
						finally
						{
							List<ModDownload.DlOptiFineListEntry>.Enumerator enumerator2;
							((IDisposable)enumerator2).Dispose();
						}
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "可视化 OptiFine 安装版本列表出错", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00041A7C File Offset: 0x0003FC7C
		private void OptiFine_Selected(MyListItem sender, EventArgs e)
		{
			this.readerReader = (ModDownload.DlOptiFineListEntry)sender.Tag;
			if (Conversions.ToBoolean(this._ConfigReader != null && Conversions.ToBoolean(Operators.NotObject(this.IsOptiFineSuitForForge(this.readerReader, this._ConfigReader)))))
			{
				this._ConfigReader = null;
			}
			this.OptiFabric_Loaded();
			this.Forge_Loaded();
			this.NeoForge_Loaded();
			this.CardOptiFine.IsSwaped = true;
			this.SelectReload();
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00005C1C File Offset: 0x00003E1C
		private void OptiFine_Clear(object sender, MouseButtonEventArgs e)
		{
			this.readerReader = null;
			this._PropertyReader = null;
			this.CardOptiFine.IsSwaped = true;
			e.Handled = true;
			this.Forge_Loaded();
			this.NeoForge_Loaded();
			this.SelectReload();
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00041AF8 File Offset: 0x0003FCF8
		private string LoadLiteLoaderGetError()
		{
			string result;
			if (this.parserReader.Contains("1.") && ModBase.Val(this.parserReader.Split(".")[1]) <= 12.0)
			{
				if (this.LoadLiteLoader != null && this.LoadLiteLoader.State.LoadingState != MyLoading.MyLoadingState.Run)
				{
					if (this.LoadLiteLoader.State.LoadingState == MyLoading.MyLoadingState.Error)
					{
						result = Conversions.ToString(Operators.ConcatenateObject("获取版本列表失败：", NewLateBinding.LateGet(NewLateBinding.LateGet(this.LoadLiteLoader.State, null, "Error", new object[0], null, null, null), null, "Message", new object[0], null, null, null)));
					}
					else
					{
						try
						{
							List<ModDownload.DlLiteLoaderListEntry>.Enumerator enumerator = ModDownload._FacadeTests.Output.Value.GetEnumerator();
							while (enumerator.MoveNext())
							{
								if (Operators.CompareString(enumerator.Current.Inherit, this.parserReader, false) == 0)
								{
									return null;
								}
							}
						}
						finally
						{
							List<ModDownload.DlLiteLoaderListEntry>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
						result = "没有可用版本";
					}
				}
				else
				{
					result = "正在获取版本列表……";
				}
			}
			else
			{
				result = "没有可用版本";
			}
			return result;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00005C51 File Offset: 0x00003E51
		private void CardLiteLoader_PreviewSwap(object sender, ModBase.RouteEventArgs e)
		{
			if (this.LoadLiteLoaderGetError() != null)
			{
				e.m_SerializerError = true;
			}
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00041C30 File Offset: 0x0003FE30
		private void LiteLoader_Loaded()
		{
			try
			{
				if (ModDownload._FacadeTests.State == ModBase.LoadState.Finished)
				{
					List<ModDownload.DlLiteLoaderListEntry> list = new List<ModDownload.DlLiteLoaderListEntry>();
					try
					{
						foreach (ModDownload.DlLiteLoaderListEntry dlLiteLoaderListEntry in ModDownload._FacadeTests.Output.Value)
						{
							if (Operators.CompareString(dlLiteLoaderListEntry.Inherit, this.parserReader, false) == 0)
							{
								list.Add(dlLiteLoaderListEntry);
							}
						}
					}
					finally
					{
						List<ModDownload.DlLiteLoaderListEntry>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					if (Enumerable.Any<ModDownload.DlLiteLoaderListEntry>(list))
					{
						this.PanLiteLoader.Children.Clear();
						try
						{
							foreach (ModDownload.DlLiteLoaderListEntry entry in list)
							{
								this.PanLiteLoader.Children.Add(ModDownloadLib.LiteLoaderDownloadListItem(entry, delegate(object sender, MouseButtonEventArgs e)
								{
									this.LiteLoader_Selected((MyListItem)sender, e);
								}, false));
							}
						}
						finally
						{
							List<ModDownload.DlLiteLoaderListEntry>.Enumerator enumerator2;
							((IDisposable)enumerator2).Dispose();
						}
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "可视化 LiteLoader 安装版本列表出错", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00005C62 File Offset: 0x00003E62
		private void LiteLoader_Selected(MyListItem sender, EventArgs e)
		{
			this.m_ClientReader = (ModDownload.DlLiteLoaderListEntry)sender.Tag;
			this.CardLiteLoader.IsSwaped = true;
			this.SelectReload();
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00005C87 File Offset: 0x00003E87
		private void LiteLoader_Clear(object sender, MouseButtonEventArgs e)
		{
			this.m_ClientReader = null;
			this.CardLiteLoader.IsSwaped = true;
			e.Handled = true;
			this.SelectReload();
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00041D60 File Offset: 0x0003FF60
		private string LoadForgeGetError()
		{
			string result;
			if (!this.parserReader.StartsWith("1."))
			{
				result = "没有可用版本";
			}
			else if (!this.LoadForge.State.IsLoader)
			{
				result = "正在获取版本列表……";
			}
			else
			{
				ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> loaderTask = (ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>)this.LoadForge.State;
				if (Operators.CompareString(this.parserReader, loaderTask.Input, false) != 0)
				{
					result = "正在获取版本列表……";
				}
				else if (loaderTask.State == ModBase.LoadState.Loading)
				{
					result = "正在获取版本列表……";
				}
				else if (loaderTask.State == ModBase.LoadState.Failed)
				{
					string message = loaderTask.Error.Message;
					if (message.Contains("没有可用版本"))
					{
						result = "没有可用版本";
					}
					else
					{
						result = "获取版本列表失败：" + message;
					}
				}
				else if (loaderTask.State != ModBase.LoadState.Finished)
				{
					result = "获取版本列表失败：未知错误，状态为 " + ModBase.GetStringFromEnum(loaderTask.State);
				}
				else
				{
					bool flag = false;
					try
					{
						foreach (ModDownload.DlForgeVersionEntry dlForgeVersionEntry in loaderTask.Output)
						{
							if (Operators.CompareString(dlForgeVersionEntry._ComposerMap, "universal", false) != 0 && Operators.CompareString(dlForgeVersionEntry._ComposerMap, "client", false) != 0)
							{
								if (this.m_TestsReader != null)
								{
									return "与 NeoForge 不兼容";
								}
								if (this.m_MapperReader != null)
								{
									return "与 Fabric 不兼容";
								}
								if (this.readerReader != null && ModMinecraft.VersionSortInteger(this.parserReader, "1.13") >= 0 && ModMinecraft.VersionSortInteger("1.14.3", this.parserReader) >= 0)
								{
									return "与 OptiFine 不兼容";
								}
								if (!Conversions.ToBoolean(this.readerReader != null && Conversions.ToBoolean(Operators.NotObject(this.IsOptiFineSuitForForge(this.readerReader, dlForgeVersionEntry)))))
								{
									return null;
								}
								flag = true;
							}
						}
					}
					finally
					{
						List<ModDownload.DlForgeVersionEntry>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					result = (flag ? "与 OptiFine 不兼容" : "该版本不支持自动安装");
				}
			}
			return result;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00005CA9 File Offset: 0x00003EA9
		private void CardForge_PreviewSwap(object sender, ModBase.RouteEventArgs e)
		{
			if (this.LoadForgeGetError() != null)
			{
				e.m_SerializerError = true;
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00041F68 File Offset: 0x00040168
		private void Forge_Loaded()
		{
			try
			{
				if (this.LoadForge.State.IsLoader)
				{
					ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> loaderTask = (ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>)this.LoadForge.State;
					if (Operators.CompareString(this.parserReader, loaderTask.Input, false) == 0)
					{
						if (loaderTask.State == ModBase.LoadState.Finished)
						{
							List<ModDownload.DlForgeVersionEntry> list = Enumerable.ToList<ModDownload.DlForgeVersionEntry>(loaderTask.Output);
							if (Enumerable.Any<ModDownload.DlForgeVersionEntry>(loaderTask.Output))
							{
								this.PanForge.Children.Clear();
								list = Enumerable.ToList<ModDownload.DlForgeVersionEntry>(Enumerable.Where<ModDownload.DlForgeVersionEntry>(list.Sort((PageDownloadInstall._Closure$__.$I54-0 == null) ? (PageDownloadInstall._Closure$__.$I54-0 = ((ModDownload.DlForgeVersionEntry a, ModDownload.DlForgeVersionEntry b) => a.clientMap > b.clientMap)) : PageDownloadInstall._Closure$__.$I54-0), (ModDownload.DlForgeVersionEntry v) => Operators.CompareString(v._ComposerMap, "universal", false) != 0 && Operators.CompareString(v._ComposerMap, "client", false) != 0 && !Conversions.ToBoolean(this.readerReader != null && Conversions.ToBoolean(Operators.NotObject(this.IsOptiFineSuitForForge(this.readerReader, v))))));
								ModDownloadLib.ForgeDownloadListItemPreload(this.PanForge, list, delegate(object sender, MouseButtonEventArgs e)
								{
									this.Forge_Selected((MyListItem)sender, e);
								}, false);
								try
								{
									foreach (ModDownload.DlForgeVersionEntry entry in list)
									{
										this.PanForge.Children.Add(ModDownloadLib.ForgeDownloadListItem(entry, delegate(object sender, MouseButtonEventArgs e)
										{
											this.Forge_Selected((MyListItem)sender, e);
										}, false));
									}
								}
								finally
								{
									List<ModDownload.DlForgeVersionEntry>.Enumerator enumerator;
									((IDisposable)enumerator).Dispose();
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "可视化 Forge 安装版本列表出错", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000420F4 File Offset: 0x000402F4
		private void Forge_Selected(MyListItem sender, EventArgs e)
		{
			this._ConfigReader = (ModDownload.DlForgeVersionEntry)sender.Tag;
			this.CardForge.IsSwaped = true;
			if (Conversions.ToBoolean(this.readerReader != null && Conversions.ToBoolean(Operators.NotObject(this.IsOptiFineSuitForForge(this.readerReader, this._ConfigReader)))))
			{
				this.readerReader = null;
			}
			this.OptiFine_Loaded();
			this.SelectReload();
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00005CBA File Offset: 0x00003EBA
		private void Forge_Clear(object sender, MouseButtonEventArgs e)
		{
			this._ConfigReader = null;
			this.CardForge.IsSwaped = true;
			e.Handled = true;
			this.OptiFine_Loaded();
			this.SelectReload();
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00042164 File Offset: 0x00040364
		private string LoadNeoForgeGetError()
		{
			string result;
			if (!this.parserReader.StartsWith("1."))
			{
				result = "没有可用版本";
			}
			else if (this.readerReader != null)
			{
				result = "与 OptiFine 不兼容";
			}
			else if (this._ConfigReader != null)
			{
				result = "与 Forge 不兼容";
			}
			else if (this.m_MapperReader != null)
			{
				result = "与 Fabric 不兼容";
			}
			else if (this.LoadNeoForge != null && this.LoadNeoForge.State.LoadingState != MyLoading.MyLoadingState.Run)
			{
				if (this.LoadNeoForge.State.LoadingState == MyLoading.MyLoadingState.Error)
				{
					result = Conversions.ToString(Operators.ConcatenateObject("获取版本列表失败：", NewLateBinding.LateGet(NewLateBinding.LateGet(this.LoadNeoForge.State, null, "Error", new object[0], null, null, null), null, "Message", new object[0], null, null, null)));
				}
				else if (Enumerable.Any<ModDownload.DlNeoForgeListEntry>(ModDownload.m_AnnotationTests.Output.Value, (ModDownload.DlNeoForgeListEntry v) => Operators.CompareString(v._TestsMap, this.parserReader, false) == 0))
				{
					result = null;
				}
				else
				{
					result = "没有可用版本";
				}
			}
			else
			{
				result = "正在获取版本列表……";
			}
			return result;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00005CE2 File Offset: 0x00003EE2
		private void CardNeoForge_PreviewSwap(object sender, ModBase.RouteEventArgs e)
		{
			if (this.LoadNeoForgeGetError() != null)
			{
				e.m_SerializerError = true;
			}
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00042274 File Offset: 0x00040474
		private void NeoForge_Loaded()
		{
			try
			{
				if (ModDownload.m_AnnotationTests.State == ModBase.LoadState.Finished)
				{
					List<ModDownload.DlNeoForgeListEntry> list = Enumerable.ToList<ModDownload.DlNeoForgeListEntry>(Enumerable.Where<ModDownload.DlNeoForgeListEntry>(ModDownload.m_AnnotationTests.Output.Value, (ModDownload.DlNeoForgeListEntry v) => Operators.CompareString(v._TestsMap, this.parserReader, false) == 0));
					if (Enumerable.Any<ModDownload.DlNeoForgeListEntry>(list))
					{
						this.PanNeoForge.Children.Clear();
						ModDownloadLib.NeoForgeDownloadListItemPreload(this.PanNeoForge, list, delegate(object sender, MouseButtonEventArgs e)
						{
							this.NeoForge_Selected((MyListItem)sender, e);
						}, false);
						try
						{
							foreach (ModDownload.DlNeoForgeListEntry info in list)
							{
								this.PanNeoForge.Children.Add(ModDownloadLib.NeoForgeDownloadListItem(info, delegate(object sender, MouseButtonEventArgs e)
								{
									this.NeoForge_Selected((MyListItem)sender, e);
								}, false));
							}
						}
						finally
						{
							List<ModDownload.DlNeoForgeListEntry>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "可视化 NeoForge 安装版本列表出错", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00005CF3 File Offset: 0x00003EF3
		private void NeoForge_Selected(MyListItem sender, EventArgs e)
		{
			this.m_TestsReader = (ModDownload.DlNeoForgeListEntry)sender.Tag;
			this.CardNeoForge.IsSwaped = true;
			this.OptiFine_Loaded();
			this.SelectReload();
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00005D1E File Offset: 0x00003F1E
		private void NeoForge_Clear(object sender, MouseButtonEventArgs e)
		{
			this.m_TestsReader = null;
			this.CardNeoForge.IsSwaped = true;
			e.Handled = true;
			this.OptiFine_Loaded();
			this.SelectReload();
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0004237C File Offset: 0x0004057C
		private string LoadFabricGetError()
		{
			string result;
			if (this.LoadFabric != null && this.LoadFabric.State.LoadingState != MyLoading.MyLoadingState.Run)
			{
				if (this.LoadFabric.State.LoadingState == MyLoading.MyLoadingState.Error)
				{
					result = Conversions.ToString(Operators.ConcatenateObject("获取版本列表失败：", NewLateBinding.LateGet(NewLateBinding.LateGet(this.LoadFabric.State, null, "Error", new object[0], null, null, null), null, "Message", new object[0], null, null, null)));
				}
				else
				{
					try
					{
						IEnumerator<JToken> enumerator = ModDownload.authenticationTests.Output.Value["game"].GetEnumerator();
						while (enumerator.MoveNext())
						{
							if (Operators.CompareString(((JObject)enumerator.Current)["version"].ToString(), this.parserReader.Replace("∞", "infinite").Replace("Combat Test 7c", "1.16_combat-3"), false) == 0)
							{
								if (this._ConfigReader != null)
								{
									return "与 Forge 不兼容";
								}
								if (this.m_TestsReader != null)
								{
									return "与 NeoForge 不兼容";
								}
								return null;
							}
						}
					}
					finally
					{
						IEnumerator<JToken> enumerator;
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
					result = "没有可用版本";
				}
			}
			else
			{
				result = "正在获取版本列表……";
			}
			return result;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00005D46 File Offset: 0x00003F46
		private void CardFabric_PreviewSwap(object sender, ModBase.RouteEventArgs e)
		{
			if (this.LoadFabricGetError() != null)
			{
				e.m_SerializerError = true;
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x000424C4 File Offset: 0x000406C4
		private void Fabric_Loaded()
		{
			try
			{
				if (ModDownload.authenticationTests.State == ModBase.LoadState.Finished)
				{
					JArray jarray = (JArray)ModDownload.authenticationTests.Output.Value["loader"];
					if (Enumerable.Any<JToken>(jarray))
					{
						this.PanFabric.Children.Clear();
						this.PanFabric.Tag = jarray;
						this.CardFabric._Stub = this.PanFabric;
						this.CardFabric.CreateParser(12);
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "可视化 Fabric 安装版本列表出错", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00042578 File Offset: 0x00040778
		public void Fabric_Selected(MyListItem sender, EventArgs e)
		{
			this.m_MapperReader = NewLateBinding.LateIndexGet(sender.Tag, new object[]
			{
				"version"
			}, null).ToString();
			this.FabricApi_Loaded();
			this.OptiFabric_Loaded();
			this.CardFabric.IsSwaped = true;
			this.SelectReload();
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00005D57 File Offset: 0x00003F57
		private void Fabric_Clear(object sender, MouseButtonEventArgs e)
		{
			this.m_MapperReader = null;
			this.m_ThreadReader = null;
			this._PropertyReader = null;
			this.CardFabric.IsSwaped = true;
			e.Handled = true;
			this.SelectReload();
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000425C8 File Offset: 0x000407C8
		public static bool IsSuitableFabricApi(string DisplayName, string MinecraftVersion)
		{
			checked
			{
				bool result;
				try
				{
					if (DisplayName != null && MinecraftVersion != null)
					{
						DisplayName = DisplayName.ToLower();
						MinecraftVersion = MinecraftVersion.Replace("∞", "infinite").Replace("Combat Test 7c", "1.16_combat-3").ToLower();
						if (DisplayName.StartsWith("[" + MinecraftVersion + "]"))
						{
							result = true;
						}
						else if (DisplayName.Contains("/") && DisplayName.Contains("]"))
						{
							string[] array = DisplayName.BeforeFirst("]", false).TrimStart(new char[]
							{
								'['
							}).Split("/");
							for (int i = 0; i < array.Length; i++)
							{
								if (Operators.CompareString(array[i], MinecraftVersion, false) == 0)
								{
									return true;
								}
							}
							List<string> list = ModBase.RegexSearch(DisplayName.BeforeFirst("]", false), "[a-z/]+|[0-9/]+", 0);
							List<string> list2 = ModBase.RegexSearch(MinecraftVersion.BeforeFirst("]", false), "[a-z/]+|[0-9/]+", 0);
							int num = 0;
							while (list.Count - 1 >= num || list2.Count - 1 >= num)
							{
								string text = (list.Count - 1 < num) ? "-1" : list[num];
								string text2 = (list2.Count - 1 < num) ? "-1" : list2[num];
								if (!text.Contains("/"))
								{
									if (Operators.CompareString(text, text2, false) != 0)
									{
										return false;
									}
								}
								else if (!text.Contains(text2))
								{
									return false;
								}
								num++;
							}
							result = true;
						}
						else
						{
							result = false;
						}
					}
					else
					{
						result = false;
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, string.Concat(new string[]
					{
						"判断 Fabric API 版本适配性出错（",
						DisplayName,
						", ",
						MinecraftVersion,
						"）"
					}), ModBase.LogLevel.Debug, "出现错误");
					result = false;
				}
				return result;
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000427CC File Offset: 0x000409CC
		private string LoadFabricApiGetError()
		{
			string result;
			if (this.LoadFabricApi != null && this.LoadFabricApi.State.LoadingState != MyLoading.MyLoadingState.Run)
			{
				if (this.LoadFabricApi.State.LoadingState == MyLoading.MyLoadingState.Error)
				{
					result = Conversions.ToString(Operators.ConcatenateObject("获取版本列表失败：", NewLateBinding.LateGet(NewLateBinding.LateGet(this.LoadFabricApi.State, null, "Error", new object[0], null, null, null), null, "Message", new object[0], null, null, null)));
				}
				else if (ModDownload.mappingTests.Output == null)
				{
					if (this.m_MapperReader == null)
					{
						result = "需要安装 Fabric";
					}
					else
					{
						result = "正在获取版本列表……";
					}
				}
				else
				{
					try
					{
						List<ModComp.CompFile>.Enumerator enumerator = ModDownload.mappingTests.Output.GetEnumerator();
						while (enumerator.MoveNext())
						{
							if (PageDownloadInstall.IsSuitableFabricApi(enumerator.Current._ProductRepository, this.parserReader))
							{
								if (this.m_MapperReader == null)
								{
									return "需要安装 Fabric";
								}
								return null;
							}
						}
					}
					finally
					{
						List<ModComp.CompFile>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					result = "没有可用版本";
				}
			}
			else
			{
				result = "正在获取版本列表……";
			}
			return result;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00005D87 File Offset: 0x00003F87
		private void CardFabricApi_PreviewSwap(object sender, ModBase.RouteEventArgs e)
		{
			if (this.LoadFabricApiGetError() != null)
			{
				e.m_SerializerError = true;
			}
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000428F0 File Offset: 0x00040AF0
		private void FabricApi_Loaded()
		{
			try
			{
				if (ModDownload.mappingTests.State == ModBase.LoadState.Finished)
				{
					if (this.parserReader != null && this.m_MapperReader != null)
					{
						List<ModComp.CompFile> list = new List<ModComp.CompFile>();
						try
						{
							foreach (ModComp.CompFile compFile in ModDownload.mappingTests.Output)
							{
								if (PageDownloadInstall.IsSuitableFabricApi(compFile._ProductRepository, this.parserReader))
								{
									if (!compFile._ProductRepository.StartsWith("["))
									{
										ModBase.Log("[Download] 已特判修改 Fabric API 显示名：" + compFile._ProductRepository, ModBase.LogLevel.Debug, "出现错误");
										compFile._ProductRepository = "[" + this.parserReader + "] " + compFile._ProductRepository;
									}
									list.Add(compFile);
								}
							}
						}
						finally
						{
							List<ModComp.CompFile>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
						if (Enumerable.Any<ModComp.CompFile>(list))
						{
							list = list.Sort((PageDownloadInstall._Closure$__.$I71-0 == null) ? (PageDownloadInstall._Closure$__.$I71-0 = ((ModComp.CompFile a, ModComp.CompFile b) => DateTime.Compare(a._ListenerRepository, b._ListenerRepository) > 0)) : PageDownloadInstall._Closure$__.$I71-0);
							this.PanFabricApi.Children.Clear();
							try
							{
								foreach (ModComp.CompFile compFile2 in list)
								{
									if (PageDownloadInstall.IsSuitableFabricApi(compFile2._ProductRepository, this.parserReader))
									{
										this.PanFabricApi.Children.Add(ModDownloadLib.FabricApiDownloadListItem(compFile2, delegate(object sender, MouseButtonEventArgs e)
										{
											this.FabricApi_Selected((MyListItem)sender, e);
										}));
									}
								}
							}
							finally
							{
								List<ModComp.CompFile>.Enumerator enumerator2;
								((IDisposable)enumerator2).Dispose();
							}
							if (!this._ContextReader)
							{
								this._ContextReader = true;
								ModBase.Log(string.Format("[Download] 已自动选择 Fabric API：{0}", ((MyListItem)this.PanFabricApi.Children[0]).Title), ModBase.LogLevel.Normal, "出现错误");
								this.FabricApi_Selected((MyListItem)this.PanFabricApi.Children[0], null);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "可视化 Fabric API 安装版本列表出错", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00005D98 File Offset: 0x00003F98
		private void FabricApi_Selected(MyListItem sender, EventArgs e)
		{
			this.m_ThreadReader = (ModComp.CompFile)sender.Tag;
			this.CardFabricApi.IsSwaped = true;
			this.SelectReload();
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00005DBD File Offset: 0x00003FBD
		private void FabricApi_Clear(object sender, MouseButtonEventArgs e)
		{
			this.m_ThreadReader = null;
			this.CardFabricApi.IsSwaped = true;
			e.Handled = true;
			this.SelectReload();
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00042B40 File Offset: 0x00040D40
		private bool IsSuitableOptiFabric(ModComp.CompFile ModFile, string MinecraftVersion)
		{
			bool result;
			try
			{
				if (MinecraftVersion == null)
				{
					result = false;
				}
				else
				{
					result = ModFile.m_ValueRepository.Contains(MinecraftVersion);
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "判断 OptiFabric 版本适配性出错（" + MinecraftVersion + "）", ModBase.LogLevel.Debug, "出现错误");
				result = false;
			}
			return result;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00042BA0 File Offset: 0x00040DA0
		private string LoadOptiFabricGetError()
		{
			string result;
			if (!this.parserReader.StartsWith("1.14") && !this.parserReader.StartsWith("1.15"))
			{
				if (this.LoadOptiFabric != null && this.LoadOptiFabric.State.LoadingState != MyLoading.MyLoadingState.Run)
				{
					if (this.LoadOptiFabric.State.LoadingState == MyLoading.MyLoadingState.Error)
					{
						result = Conversions.ToString(Operators.ConcatenateObject("获取版本列表失败：", NewLateBinding.LateGet(NewLateBinding.LateGet(this.LoadOptiFabric.State, null, "Error", new object[0], null, null, null), null, "Message", new object[0], null, null, null)));
					}
					else if (ModDownload.tokenizerTests.Output == null)
					{
						if (this.m_MapperReader == null && this.readerReader == null)
						{
							result = "需要安装 OptiFine 与 Fabric";
						}
						else if (this.m_MapperReader == null)
						{
							result = "需要安装 Fabric";
						}
						else if (this.readerReader == null)
						{
							result = "需要安装 OptiFine";
						}
						else
						{
							result = "正在获取版本列表……";
						}
					}
					else
					{
						try
						{
							foreach (ModComp.CompFile modFile in ModDownload.tokenizerTests.Output)
							{
								if (this.IsSuitableOptiFabric(modFile, this.parserReader))
								{
									if (this.m_MapperReader == null && this.readerReader == null)
									{
										return "需要安装 OptiFine 与 Fabric";
									}
									if (this.m_MapperReader == null)
									{
										return "需要安装 Fabric";
									}
									if (this.readerReader == null)
									{
										return "需要安装 OptiFine";
									}
									return null;
								}
							}
						}
						finally
						{
							List<ModComp.CompFile>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
						result = "没有可用版本";
					}
				}
				else
				{
					result = "正在获取版本列表……";
				}
			}
			else
			{
				result = "不兼容老版本 Fabric，请手动下载 OptiFabric Origins";
			}
			return result;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00005DDF File Offset: 0x00003FDF
		private void CardOptiFabric_PreviewSwap(object sender, ModBase.RouteEventArgs e)
		{
			if (this.LoadOptiFabricGetError() != null)
			{
				e.m_SerializerError = true;
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00042D50 File Offset: 0x00040F50
		private void OptiFabric_Loaded()
		{
			try
			{
				if (ModDownload.tokenizerTests.State == ModBase.LoadState.Finished)
				{
					if (this.parserReader != null && this.m_MapperReader != null && this.readerReader != null)
					{
						List<ModComp.CompFile> list = new List<ModComp.CompFile>();
						try
						{
							foreach (ModComp.CompFile compFile in ModDownload.tokenizerTests.Output)
							{
								if (this.IsSuitableOptiFabric(compFile, this.parserReader))
								{
									list.Add(compFile);
								}
							}
						}
						finally
						{
							List<ModComp.CompFile>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
						if (Enumerable.Any<ModComp.CompFile>(list))
						{
							list = list.Sort((PageDownloadInstall._Closure$__.$I78-0 == null) ? (PageDownloadInstall._Closure$__.$I78-0 = ((ModComp.CompFile a, ModComp.CompFile b) => DateTime.Compare(a._ListenerRepository, b._ListenerRepository) > 0)) : PageDownloadInstall._Closure$__.$I78-0);
							this.PanOptiFabric.Children.Clear();
							try
							{
								foreach (ModComp.CompFile compFile2 in list)
								{
									if (this.IsSuitableOptiFabric(compFile2, this.parserReader))
									{
										this.PanOptiFabric.Children.Add(ModDownloadLib.OptiFabricDownloadListItem(compFile2, delegate(object sender, MouseButtonEventArgs e)
										{
											this.OptiFabric_Selected((MyListItem)sender, e);
										}));
									}
								}
							}
							finally
							{
								List<ModComp.CompFile>.Enumerator enumerator2;
								((IDisposable)enumerator2).Dispose();
							}
							if (!this._SpecificationReader && !this.parserReader.StartsWith("1.14") && !this.parserReader.StartsWith("1.15"))
							{
								this._SpecificationReader = true;
								ModBase.Log(string.Format("[Download] 已自动选择 OptiFabric：{0}", ((MyListItem)this.PanOptiFabric.Children[0]).Title), ModBase.LogLevel.Normal, "出现错误");
								this.OptiFabric_Selected((MyListItem)this.PanOptiFabric.Children[0], null);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "可视化 OptiFabric 安装版本列表出错", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00005DF0 File Offset: 0x00003FF0
		private void OptiFabric_Selected(MyListItem sender, EventArgs e)
		{
			this._PropertyReader = (ModComp.CompFile)sender.Tag;
			this.CardOptiFabric.IsSwaped = true;
			this.SelectReload();
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00005E15 File Offset: 0x00004015
		private void OptiFabric_Clear(object sender, MouseButtonEventArgs e)
		{
			this._PropertyReader = null;
			this.CardOptiFabric.IsSwaped = true;
			e.Handled = true;
			this.SelectReload();
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00005E37 File Offset: 0x00004037
		private void TextSelectName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && this.BtnSelectStart.IsEnabled)
			{
				this.BtnSelectStart_Click();
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00042F78 File Offset: 0x00041178
		private void BtnSelectStart_Click()
		{
			if (((this._ConfigReader == null && this.m_TestsReader == null && this.m_MapperReader == null) || (!Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LaunchArgumentIndie", null), 0, false) && !Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LaunchArgumentIndie", null), 2, false)) || ModMain.MyMsgBox("你尚未开启版本隔离，这会导致多个 MC 共用同一个 Mod 文件夹。\r\n因此在切换 MC 版本时，MC 会因为读取到与当前版本不符的 Mod 而崩溃。\r\nPCL 推荐你在开始下载前，在 设置 → 版本隔离 中开启版本隔离选项！", "版本隔离提示", "取消下载", "继续", "", false, true, false, null, null, null) != 1) && ModDownloadLib.McInstall(new ModDownloadLib.McInstallRequest
			{
				m_SystemTest = this.TextSelectName.Text,
				_ParamTest = string.Format("{0}versions\\{1}\\", ModMinecraft.m_ProxyTests, this.TextSelectName.Text),
				m_ObserverTest = this.m_BroadcasterReader,
				_TagTest = this.parserReader,
				rulesTest = this.readerReader,
				decoratorTest = this._ConfigReader,
				stateTest = this.m_TestsReader,
				m_CallbackTest = this.m_MapperReader,
				_TemplateTest = this.m_ThreadReader,
				OptiFabric = this._PropertyReader,
				methodTest = this.m_ClientReader
			}))
			{
				this.ExitSelectPage();
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00005E55 File Offset: 0x00004055
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x00005E5D File Offset: 0x0000405D
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00005E66 File Offset: 0x00004066
		// (set) Token: 0x0600077E RID: 1918 RVA: 0x00005E6E File Offset: 0x0000406E
		internal virtual StackPanel PanMinecraft { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x00005E77 File Offset: 0x00004077
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x00005E7F File Offset: 0x0000407F
		internal virtual StackPanel PanSelect { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00005E88 File Offset: 0x00004088
		// (set) Token: 0x06000782 RID: 1922 RVA: 0x00005E90 File Offset: 0x00004090
		internal virtual MyHint HintFabricAPI { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x00005E99 File Offset: 0x00004099
		// (set) Token: 0x06000784 RID: 1924 RVA: 0x00005EA1 File Offset: 0x000040A1
		internal virtual MyHint HintOptiFabric { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x00005EAA File Offset: 0x000040AA
		// (set) Token: 0x06000786 RID: 1926 RVA: 0x00005EB2 File Offset: 0x000040B2
		internal virtual MyHint HintOptiFabricOld { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x00005EBB File Offset: 0x000040BB
		// (set) Token: 0x06000788 RID: 1928 RVA: 0x00005EC3 File Offset: 0x000040C3
		internal virtual MyHint HintModOptiFine { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00005ECC File Offset: 0x000040CC
		// (set) Token: 0x0600078A RID: 1930 RVA: 0x00005ED4 File Offset: 0x000040D4
		internal virtual MyListItem ItemSelect { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x00005EDD File Offset: 0x000040DD
		// (set) Token: 0x0600078C RID: 1932 RVA: 0x000430B4 File Offset: 0x000412B4
		internal virtual MyButton BtnSelectStart
		{
			[CompilerGenerated]
			get
			{
				return this._WatcherReader;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.BtnSelectStart_Click();
				};
				MyButton watcherReader = this._WatcherReader;
				if (watcherReader != null)
				{
					watcherReader.Click -= value2;
				}
				this._WatcherReader = value;
				watcherReader = this._WatcherReader;
				if (watcherReader != null)
				{
					watcherReader.Click += value2;
				}
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x00005EE5 File Offset: 0x000040E5
		// (set) Token: 0x0600078E RID: 1934 RVA: 0x000430F8 File Offset: 0x000412F8
		internal virtual MyTextBox TextSelectName
		{
			[CompilerGenerated]
			get
			{
				return this.identifierReader;
			}
			[CompilerGenerated]
			set
			{
				TextChangedEventHandler value2 = new TextChangedEventHandler(this.TextSelectName_TextChanged);
				MyTextBox.ValidateChangedEventHandler obj = new MyTextBox.ValidateChangedEventHandler(this.TextSelectName_ValidateChanged);
				KeyEventHandler value3 = new KeyEventHandler(this.TextSelectName_KeyDown);
				MyTextBox myTextBox = this.identifierReader;
				if (myTextBox != null)
				{
					myTextBox.TextChanged -= value2;
					MyTextBox.MapReader(obj);
					myTextBox.KeyDown -= value3;
				}
				this.identifierReader = value;
				myTextBox = this.identifierReader;
				if (myTextBox != null)
				{
					myTextBox.TextChanged += value2;
					MyTextBox.SortReader(obj);
					myTextBox.KeyDown += value3;
				}
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x00005EED File Offset: 0x000040ED
		// (set) Token: 0x06000790 RID: 1936 RVA: 0x00043170 File Offset: 0x00041370
		internal virtual MyCard CardMinecraft
		{
			[CompilerGenerated]
			get
			{
				return this._SystemReader;
			}
			[CompilerGenerated]
			set
			{
				MyCard.PreviewSwapEventHandler obj = new MyCard.PreviewSwapEventHandler(this.CardMinecraft_PreviewSwap);
				MyCard systemReader = this._SystemReader;
				if (systemReader != null)
				{
					systemReader.PostParser(obj);
				}
				this._SystemReader = value;
				systemReader = this._SystemReader;
				if (systemReader != null)
				{
					systemReader.InterruptParser(obj);
				}
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x00005EF5 File Offset: 0x000040F5
		// (set) Token: 0x06000792 RID: 1938 RVA: 0x00005EFD File Offset: 0x000040FD
		internal virtual Grid PanMinecraftInfo { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x00005F06 File Offset: 0x00004106
		// (set) Token: 0x06000794 RID: 1940 RVA: 0x00005F0E File Offset: 0x0000410E
		internal virtual Image ImgMinecraft { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00005F17 File Offset: 0x00004117
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x00005F1F File Offset: 0x0000411F
		internal virtual TextBlock LabMinecraft { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x00005F28 File Offset: 0x00004128
		// (set) Token: 0x06000798 RID: 1944 RVA: 0x000431B4 File Offset: 0x000413B4
		internal virtual MyCard CardForge
		{
			[CompilerGenerated]
			get
			{
				return this.m_StubReader;
			}
			[CompilerGenerated]
			set
			{
				MyCard.SwapEventHandler obj = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.SelectReload();
				};
				MyCard.PreviewSwapEventHandler obj2 = new MyCard.PreviewSwapEventHandler(this.CardForge_PreviewSwap);
				MyCard stubReader = this.m_StubReader;
				if (stubReader != null)
				{
					stubReader.MoveParser(obj);
					stubReader.PostParser(obj2);
				}
				this.m_StubReader = value;
				stubReader = this.m_StubReader;
				if (stubReader != null)
				{
					stubReader.CompareParser(obj);
					stubReader.InterruptParser(obj2);
				}
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x00005F30 File Offset: 0x00004130
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x00005F38 File Offset: 0x00004138
		internal virtual StackPanel PanForge { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x00005F41 File Offset: 0x00004141
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x00005F49 File Offset: 0x00004149
		internal virtual Grid PanForgeInfo { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x00005F52 File Offset: 0x00004152
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x00005F5A File Offset: 0x0000415A
		internal virtual Image ImgForge { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00005F63 File Offset: 0x00004163
		// (set) Token: 0x060007A0 RID: 1952 RVA: 0x00005F6B File Offset: 0x0000416B
		internal virtual TextBlock LabForge { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00005F74 File Offset: 0x00004174
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x00043214 File Offset: 0x00041414
		internal virtual Grid BtnForgeClear
		{
			[CompilerGenerated]
			get
			{
				return this.stateReader;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.Forge_Clear);
				Grid grid = this.stateReader;
				if (grid != null)
				{
					grid.MouseLeftButtonUp -= value2;
				}
				this.stateReader = value;
				grid = this.stateReader;
				if (grid != null)
				{
					grid.MouseLeftButtonUp += value2;
				}
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00005F7C File Offset: 0x0000417C
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x00005F84 File Offset: 0x00004184
		internal virtual Path BtnForgeClearInner { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00005F8D File Offset: 0x0000418D
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x00043258 File Offset: 0x00041458
		internal virtual MyCard CardNeoForge
		{
			[CompilerGenerated]
			get
			{
				return this._TemplateReader;
			}
			[CompilerGenerated]
			set
			{
				MyCard.SwapEventHandler obj = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.SelectReload();
				};
				MyCard.PreviewSwapEventHandler obj2 = new MyCard.PreviewSwapEventHandler(this.CardNeoForge_PreviewSwap);
				MyCard templateReader = this._TemplateReader;
				if (templateReader != null)
				{
					templateReader.MoveParser(obj);
					templateReader.PostParser(obj2);
				}
				this._TemplateReader = value;
				templateReader = this._TemplateReader;
				if (templateReader != null)
				{
					templateReader.CompareParser(obj);
					templateReader.InterruptParser(obj2);
				}
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00005F95 File Offset: 0x00004195
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x00005F9D File Offset: 0x0000419D
		internal virtual StackPanel PanNeoForge { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00005FA6 File Offset: 0x000041A6
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x00005FAE File Offset: 0x000041AE
		internal virtual Grid PanNeoForgeInfo { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00005FB7 File Offset: 0x000041B7
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x00005FBF File Offset: 0x000041BF
		internal virtual Image ImgNeoForge { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x00005FC8 File Offset: 0x000041C8
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x00005FD0 File Offset: 0x000041D0
		internal virtual TextBlock LabNeoForge { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x00005FD9 File Offset: 0x000041D9
		// (set) Token: 0x060007B0 RID: 1968 RVA: 0x000432B8 File Offset: 0x000414B8
		internal virtual Grid BtnNeoForgeClear
		{
			[CompilerGenerated]
			get
			{
				return this._GetterReader;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.NeoForge_Clear);
				Grid getterReader = this._GetterReader;
				if (getterReader != null)
				{
					getterReader.MouseLeftButtonUp -= value2;
				}
				this._GetterReader = value;
				getterReader = this._GetterReader;
				if (getterReader != null)
				{
					getterReader.MouseLeftButtonUp += value2;
				}
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00005FE1 File Offset: 0x000041E1
		// (set) Token: 0x060007B2 RID: 1970 RVA: 0x00005FE9 File Offset: 0x000041E9
		internal virtual Path BtnNeoForgeClearInner { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00005FF2 File Offset: 0x000041F2
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x000432FC File Offset: 0x000414FC
		internal virtual MyCard CardFabric
		{
			[CompilerGenerated]
			get
			{
				return this.m_ExpressionReader;
			}
			[CompilerGenerated]
			set
			{
				MyCard.SwapEventHandler obj = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.SelectReload();
				};
				MyCard.PreviewSwapEventHandler obj2 = new MyCard.PreviewSwapEventHandler(this.CardFabric_PreviewSwap);
				MyCard expressionReader = this.m_ExpressionReader;
				if (expressionReader != null)
				{
					expressionReader.MoveParser(obj);
					expressionReader.PostParser(obj2);
				}
				this.m_ExpressionReader = value;
				expressionReader = this.m_ExpressionReader;
				if (expressionReader != null)
				{
					expressionReader.CompareParser(obj);
					expressionReader.InterruptParser(obj2);
				}
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00005FFA File Offset: 0x000041FA
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x00006002 File Offset: 0x00004202
		internal virtual StackPanel PanFabric { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0000600B File Offset: 0x0000420B
		// (set) Token: 0x060007B8 RID: 1976 RVA: 0x00006013 File Offset: 0x00004213
		internal virtual Grid PanFabricInfo { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0000601C File Offset: 0x0000421C
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x00006024 File Offset: 0x00004224
		internal virtual Image ImgFabric { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0000602D File Offset: 0x0000422D
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x00006035 File Offset: 0x00004235
		internal virtual TextBlock LabFabric { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0000603E File Offset: 0x0000423E
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x0004335C File Offset: 0x0004155C
		internal virtual Grid BtnFabricClear
		{
			[CompilerGenerated]
			get
			{
				return this.m_SetterReader;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.Fabric_Clear);
				Grid setterReader = this.m_SetterReader;
				if (setterReader != null)
				{
					setterReader.MouseLeftButtonUp -= value2;
				}
				this.m_SetterReader = value;
				setterReader = this.m_SetterReader;
				if (setterReader != null)
				{
					setterReader.MouseLeftButtonUp += value2;
				}
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x00006046 File Offset: 0x00004246
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x0000604E File Offset: 0x0000424E
		internal virtual Path BtnFabricClearInner { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00006057 File Offset: 0x00004257
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x000433A0 File Offset: 0x000415A0
		internal virtual MyCard CardFabricApi
		{
			[CompilerGenerated]
			get
			{
				return this.exporterReader;
			}
			[CompilerGenerated]
			set
			{
				MyCard.SwapEventHandler obj = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.SelectReload();
				};
				MyCard.PreviewSwapEventHandler obj2 = new MyCard.PreviewSwapEventHandler(this.CardFabricApi_PreviewSwap);
				MyCard myCard = this.exporterReader;
				if (myCard != null)
				{
					myCard.MoveParser(obj);
					myCard.PostParser(obj2);
				}
				this.exporterReader = value;
				myCard = this.exporterReader;
				if (myCard != null)
				{
					myCard.CompareParser(obj);
					myCard.InterruptParser(obj2);
				}
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0000605F File Offset: 0x0000425F
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x00006067 File Offset: 0x00004267
		internal virtual StackPanel PanFabricApi { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00006070 File Offset: 0x00004270
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x00006078 File Offset: 0x00004278
		internal virtual Grid PanFabricApiInfo { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00006081 File Offset: 0x00004281
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x00006089 File Offset: 0x00004289
		internal virtual Image ImgFabricApi { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00006092 File Offset: 0x00004292
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x0000609A File Offset: 0x0000429A
		internal virtual TextBlock LabFabricApi { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x000060A3 File Offset: 0x000042A3
		// (set) Token: 0x060007CC RID: 1996 RVA: 0x00043400 File Offset: 0x00041600
		internal virtual Grid BtnFabricApiClear
		{
			[CompilerGenerated]
			get
			{
				return this.m_ResolverReader;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.FabricApi_Clear);
				Grid resolverReader = this.m_ResolverReader;
				if (resolverReader != null)
				{
					resolverReader.MouseLeftButtonUp -= value2;
				}
				this.m_ResolverReader = value;
				resolverReader = this.m_ResolverReader;
				if (resolverReader != null)
				{
					resolverReader.MouseLeftButtonUp += value2;
				}
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x000060AB File Offset: 0x000042AB
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x000060B3 File Offset: 0x000042B3
		internal virtual Path BtnFabricApiClearInner { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x000060BC File Offset: 0x000042BC
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x00043444 File Offset: 0x00041644
		internal virtual MyCard CardOptiFine
		{
			[CompilerGenerated]
			get
			{
				return this.roleReader;
			}
			[CompilerGenerated]
			set
			{
				MyCard.SwapEventHandler obj = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.SelectReload();
				};
				MyCard.PreviewSwapEventHandler obj2 = new MyCard.PreviewSwapEventHandler(this.CardOptiFine_PreviewSwap);
				MyCard myCard = this.roleReader;
				if (myCard != null)
				{
					myCard.MoveParser(obj);
					myCard.PostParser(obj2);
				}
				this.roleReader = value;
				myCard = this.roleReader;
				if (myCard != null)
				{
					myCard.CompareParser(obj);
					myCard.InterruptParser(obj2);
				}
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x000060C4 File Offset: 0x000042C4
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x000060CC File Offset: 0x000042CC
		internal virtual StackPanel PanOptiFine { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x000060D5 File Offset: 0x000042D5
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x000060DD File Offset: 0x000042DD
		internal virtual Grid PanOptiFineInfo { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x000060E6 File Offset: 0x000042E6
		// (set) Token: 0x060007D6 RID: 2006 RVA: 0x000060EE File Offset: 0x000042EE
		internal virtual Image ImgOptiFine { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x000060F7 File Offset: 0x000042F7
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x000060FF File Offset: 0x000042FF
		internal virtual TextBlock LabOptiFine { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00006108 File Offset: 0x00004308
		// (set) Token: 0x060007DA RID: 2010 RVA: 0x000434A4 File Offset: 0x000416A4
		internal virtual Grid BtnOptiFineClear
		{
			[CompilerGenerated]
			get
			{
				return this.m_CandidateReader;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.OptiFine_Clear);
				Grid candidateReader = this.m_CandidateReader;
				if (candidateReader != null)
				{
					candidateReader.MouseLeftButtonUp -= value2;
				}
				this.m_CandidateReader = value;
				candidateReader = this.m_CandidateReader;
				if (candidateReader != null)
				{
					candidateReader.MouseLeftButtonUp += value2;
				}
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x00006110 File Offset: 0x00004310
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x00006118 File Offset: 0x00004318
		internal virtual Path BtnOptiFineClearInner { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00006121 File Offset: 0x00004321
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x000434E8 File Offset: 0x000416E8
		internal virtual MyCard CardOptiFabric
		{
			[CompilerGenerated]
			get
			{
				return this.accountReader;
			}
			[CompilerGenerated]
			set
			{
				MyCard.SwapEventHandler obj = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.SelectReload();
				};
				MyCard.PreviewSwapEventHandler obj2 = new MyCard.PreviewSwapEventHandler(this.CardOptiFabric_PreviewSwap);
				MyCard myCard = this.accountReader;
				if (myCard != null)
				{
					myCard.MoveParser(obj);
					myCard.PostParser(obj2);
				}
				this.accountReader = value;
				myCard = this.accountReader;
				if (myCard != null)
				{
					myCard.CompareParser(obj);
					myCard.InterruptParser(obj2);
				}
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x00006129 File Offset: 0x00004329
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x00006131 File Offset: 0x00004331
		internal virtual StackPanel PanOptiFabric { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0000613A File Offset: 0x0000433A
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x00006142 File Offset: 0x00004342
		internal virtual Grid PanOptiFabricInfo { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0000614B File Offset: 0x0000434B
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x00006153 File Offset: 0x00004353
		internal virtual Image ImgOptiFabric { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0000615C File Offset: 0x0000435C
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual TextBlock LabOptiFabric { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0000616D File Offset: 0x0000436D
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x00043548 File Offset: 0x00041748
		internal virtual Grid BtnOptiFabricClear
		{
			[CompilerGenerated]
			get
			{
				return this._WrapperReader;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.OptiFabric_Clear);
				Grid wrapperReader = this._WrapperReader;
				if (wrapperReader != null)
				{
					wrapperReader.MouseLeftButtonUp -= value2;
				}
				this._WrapperReader = value;
				wrapperReader = this._WrapperReader;
				if (wrapperReader != null)
				{
					wrapperReader.MouseLeftButtonUp += value2;
				}
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00006175 File Offset: 0x00004375
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x0000617D File Offset: 0x0000437D
		internal virtual Path BtnOptiFabricClearInner { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x00006186 File Offset: 0x00004386
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x0004358C File Offset: 0x0004178C
		internal virtual MyCard CardLiteLoader
		{
			[CompilerGenerated]
			get
			{
				return this.m_AttributeReader;
			}
			[CompilerGenerated]
			set
			{
				MyCard.SwapEventHandler obj = delegate(object sender, ModBase.RouteEventArgs e)
				{
					this.SelectReload();
				};
				MyCard.PreviewSwapEventHandler obj2 = new MyCard.PreviewSwapEventHandler(this.CardLiteLoader_PreviewSwap);
				MyCard attributeReader = this.m_AttributeReader;
				if (attributeReader != null)
				{
					attributeReader.MoveParser(obj);
					attributeReader.PostParser(obj2);
				}
				this.m_AttributeReader = value;
				attributeReader = this.m_AttributeReader;
				if (attributeReader != null)
				{
					attributeReader.CompareParser(obj);
					attributeReader.InterruptParser(obj2);
				}
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0000618E File Offset: 0x0000438E
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x00006196 File Offset: 0x00004396
		internal virtual StackPanel PanLiteLoader { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0000619F File Offset: 0x0000439F
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x000061A7 File Offset: 0x000043A7
		internal virtual Grid PanLiteLoaderInfo { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x000061B0 File Offset: 0x000043B0
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x000061B8 File Offset: 0x000043B8
		internal virtual Image ImgLiteLoader { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x000061C1 File Offset: 0x000043C1
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x000061C9 File Offset: 0x000043C9
		internal virtual TextBlock LabLiteLoader { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x000061D2 File Offset: 0x000043D2
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x000435EC File Offset: 0x000417EC
		internal virtual Grid BtnLiteLoaderClear
		{
			[CompilerGenerated]
			get
			{
				return this._AdapterReader;
			}
			[CompilerGenerated]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.LiteLoader_Clear);
				Grid adapterReader = this._AdapterReader;
				if (adapterReader != null)
				{
					adapterReader.MouseLeftButtonUp -= value2;
				}
				this._AdapterReader = value;
				adapterReader = this._AdapterReader;
				if (adapterReader != null)
				{
					adapterReader.MouseLeftButtonUp += value2;
				}
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x000061DA File Offset: 0x000043DA
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x000061E2 File Offset: 0x000043E2
		internal virtual Path BtnLiteLoaderClearInner { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x000061EB File Offset: 0x000043EB
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x000061F3 File Offset: 0x000043F3
		internal virtual MyCard PanLoad { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x000061FC File Offset: 0x000043FC
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x00006204 File Offset: 0x00004404
		internal virtual MyLoading LoadMinecraft { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x0000620D File Offset: 0x0000440D
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x00043630 File Offset: 0x00041830
		internal virtual MyLoading LoadOptiFine
		{
			[CompilerGenerated]
			get
			{
				return this.m_AuthenticationReader;
			}
			[CompilerGenerated]
			set
			{
				MyLoading.StateChangedEventHandler obj = delegate(object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2)
				{
					this.SelectReload();
				};
				MyLoading.StateChangedEventHandler obj2 = delegate(object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2)
				{
					this.OptiFine_Loaded();
				};
				MyLoading authenticationReader = this.m_AuthenticationReader;
				if (authenticationReader != null)
				{
					authenticationReader.InterruptField(obj);
					authenticationReader.InterruptField(obj2);
				}
				this.m_AuthenticationReader = value;
				authenticationReader = this.m_AuthenticationReader;
				if (authenticationReader != null)
				{
					authenticationReader.PrintField(obj);
					authenticationReader.PrintField(obj2);
				}
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x00006215 File Offset: 0x00004415
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x00043690 File Offset: 0x00041890
		internal virtual MyLoading LoadForge
		{
			[CompilerGenerated]
			get
			{
				return this._AlgoReader;
			}
			[CompilerGenerated]
			set
			{
				MyLoading.StateChangedEventHandler obj = delegate(object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2)
				{
					this.SelectReload();
				};
				MyLoading.StateChangedEventHandler obj2 = delegate(object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2)
				{
					this.Forge_Loaded();
				};
				MyLoading algoReader = this._AlgoReader;
				if (algoReader != null)
				{
					algoReader.InterruptField(obj);
					algoReader.InterruptField(obj2);
				}
				this._AlgoReader = value;
				algoReader = this._AlgoReader;
				if (algoReader != null)
				{
					algoReader.PrintField(obj);
					algoReader.PrintField(obj2);
				}
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0000621D File Offset: 0x0000441D
		// (set) Token: 0x06000802 RID: 2050 RVA: 0x000436F0 File Offset: 0x000418F0
		internal virtual MyLoading LoadNeoForge
		{
			[CompilerGenerated]
			get
			{
				return this.m_ComparatorReader;
			}
			[CompilerGenerated]
			set
			{
				MyLoading.StateChangedEventHandler obj = delegate(object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2)
				{
					this.SelectReload();
				};
				MyLoading.StateChangedEventHandler obj2 = delegate(object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2)
				{
					this.NeoForge_Loaded();
				};
				MyLoading comparatorReader = this.m_ComparatorReader;
				if (comparatorReader != null)
				{
					comparatorReader.InterruptField(obj);
					comparatorReader.InterruptField(obj2);
				}
				this.m_ComparatorReader = value;
				comparatorReader = this.m_ComparatorReader;
				if (comparatorReader != null)
				{
					comparatorReader.PrintField(obj);
					comparatorReader.PrintField(obj2);
				}
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x00006225 File Offset: 0x00004425
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x00043750 File Offset: 0x00041950
		internal virtual MyLoading LoadLiteLoader
		{
			[CompilerGenerated]
			get
			{
				return this.m_MappingReader;
			}
			[CompilerGenerated]
			set
			{
				MyLoading.StateChangedEventHandler obj = delegate(object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2)
				{
					this.SelectReload();
				};
				MyLoading.StateChangedEventHandler obj2 = delegate(object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2)
				{
					this.LiteLoader_Loaded();
				};
				MyLoading mappingReader = this.m_MappingReader;
				if (mappingReader != null)
				{
					mappingReader.InterruptField(obj);
					mappingReader.InterruptField(obj2);
				}
				this.m_MappingReader = value;
				mappingReader = this.m_MappingReader;
				if (mappingReader != null)
				{
					mappingReader.PrintField(obj);
					mappingReader.PrintField(obj2);
				}
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x0000622D File Offset: 0x0000442D
		// (set) Token: 0x06000806 RID: 2054 RVA: 0x000437B0 File Offset: 0x000419B0
		internal virtual MyLoading LoadFabric
		{
			[CompilerGenerated]
			get
			{
				return this.m_TokenizerReader;
			}
			[CompilerGenerated]
			set
			{
				MyLoading.StateChangedEventHandler obj = delegate(object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2)
				{
					this.SelectReload();
				};
				MyLoading.StateChangedEventHandler obj2 = delegate(object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2)
				{
					this.Fabric_Loaded();
				};
				MyLoading tokenizerReader = this.m_TokenizerReader;
				if (tokenizerReader != null)
				{
					tokenizerReader.InterruptField(obj);
					tokenizerReader.InterruptField(obj2);
				}
				this.m_TokenizerReader = value;
				tokenizerReader = this.m_TokenizerReader;
				if (tokenizerReader != null)
				{
					tokenizerReader.PrintField(obj);
					tokenizerReader.PrintField(obj2);
				}
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00006235 File Offset: 0x00004435
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x00043810 File Offset: 0x00041A10
		internal virtual MyLoading LoadFabricApi
		{
			[CompilerGenerated]
			get
			{
				return this._FilterReader;
			}
			[CompilerGenerated]
			set
			{
				MyLoading.StateChangedEventHandler obj = delegate(object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2)
				{
					this.SelectReload();
				};
				MyLoading.StateChangedEventHandler obj2 = delegate(object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2)
				{
					this.FabricApi_Loaded();
				};
				MyLoading filterReader = this._FilterReader;
				if (filterReader != null)
				{
					filterReader.InterruptField(obj);
					filterReader.InterruptField(obj2);
				}
				this._FilterReader = value;
				filterReader = this._FilterReader;
				if (filterReader != null)
				{
					filterReader.PrintField(obj);
					filterReader.PrintField(obj2);
				}
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0000623D File Offset: 0x0000443D
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x00043870 File Offset: 0x00041A70
		internal virtual MyLoading LoadOptiFabric
		{
			[CompilerGenerated]
			get
			{
				return this.m_DatabaseReader;
			}
			[CompilerGenerated]
			set
			{
				MyLoading.StateChangedEventHandler obj = delegate(object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2)
				{
					this.SelectReload();
				};
				MyLoading.StateChangedEventHandler obj2 = delegate(object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2)
				{
					this.OptiFabric_Loaded();
				};
				MyLoading databaseReader = this.m_DatabaseReader;
				if (databaseReader != null)
				{
					databaseReader.InterruptField(obj);
					databaseReader.InterruptField(obj2);
				}
				this.m_DatabaseReader = value;
				databaseReader = this.m_DatabaseReader;
				if (databaseReader != null)
				{
					databaseReader.PrintField(obj);
					databaseReader.PrintField(obj2);
				}
			}
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x000438D0 File Offset: 0x00041AD0
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.predicateReader)
			{
				this.predicateReader = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagedownload/pagedownloadinstall.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00043900 File Offset: 0x00041B00
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanBack = (MyScrollViewer)target;
				return;
			}
			if (connectionId == 2)
			{
				this.PanMinecraft = (StackPanel)target;
				return;
			}
			if (connectionId == 3)
			{
				this.PanSelect = (StackPanel)target;
				return;
			}
			if (connectionId == 4)
			{
				this.HintFabricAPI = (MyHint)target;
				return;
			}
			if (connectionId == 5)
			{
				this.HintOptiFabric = (MyHint)target;
				return;
			}
			if (connectionId == 6)
			{
				this.HintOptiFabricOld = (MyHint)target;
				return;
			}
			if (connectionId == 7)
			{
				this.HintModOptiFine = (MyHint)target;
				return;
			}
			if (connectionId == 8)
			{
				this.ItemSelect = (MyListItem)target;
				return;
			}
			if (connectionId == 9)
			{
				this.BtnSelectStart = (MyButton)target;
				return;
			}
			if (connectionId == 10)
			{
				this.TextSelectName = (MyTextBox)target;
				return;
			}
			if (connectionId == 11)
			{
				this.CardMinecraft = (MyCard)target;
				return;
			}
			if (connectionId == 12)
			{
				this.PanMinecraftInfo = (Grid)target;
				return;
			}
			if (connectionId == 13)
			{
				this.ImgMinecraft = (Image)target;
				return;
			}
			if (connectionId == 14)
			{
				this.LabMinecraft = (TextBlock)target;
				return;
			}
			if (connectionId == 15)
			{
				this.CardForge = (MyCard)target;
				return;
			}
			if (connectionId == 16)
			{
				this.PanForge = (StackPanel)target;
				return;
			}
			if (connectionId == 17)
			{
				this.PanForgeInfo = (Grid)target;
				return;
			}
			if (connectionId == 18)
			{
				this.ImgForge = (Image)target;
				return;
			}
			if (connectionId == 19)
			{
				this.LabForge = (TextBlock)target;
				return;
			}
			if (connectionId == 20)
			{
				this.BtnForgeClear = (Grid)target;
				return;
			}
			if (connectionId == 21)
			{
				this.BtnForgeClearInner = (Path)target;
				return;
			}
			if (connectionId == 22)
			{
				this.CardNeoForge = (MyCard)target;
				return;
			}
			if (connectionId == 23)
			{
				this.PanNeoForge = (StackPanel)target;
				return;
			}
			if (connectionId == 24)
			{
				this.PanNeoForgeInfo = (Grid)target;
				return;
			}
			if (connectionId == 25)
			{
				this.ImgNeoForge = (Image)target;
				return;
			}
			if (connectionId == 26)
			{
				this.LabNeoForge = (TextBlock)target;
				return;
			}
			if (connectionId == 27)
			{
				this.BtnNeoForgeClear = (Grid)target;
				return;
			}
			if (connectionId == 28)
			{
				this.BtnNeoForgeClearInner = (Path)target;
				return;
			}
			if (connectionId == 29)
			{
				this.CardFabric = (MyCard)target;
				return;
			}
			if (connectionId == 30)
			{
				this.PanFabric = (StackPanel)target;
				return;
			}
			if (connectionId == 31)
			{
				this.PanFabricInfo = (Grid)target;
				return;
			}
			if (connectionId == 32)
			{
				this.ImgFabric = (Image)target;
				return;
			}
			if (connectionId == 33)
			{
				this.LabFabric = (TextBlock)target;
				return;
			}
			if (connectionId == 34)
			{
				this.BtnFabricClear = (Grid)target;
				return;
			}
			if (connectionId == 35)
			{
				this.BtnFabricClearInner = (Path)target;
				return;
			}
			if (connectionId == 36)
			{
				this.CardFabricApi = (MyCard)target;
				return;
			}
			if (connectionId == 37)
			{
				this.PanFabricApi = (StackPanel)target;
				return;
			}
			if (connectionId == 38)
			{
				this.PanFabricApiInfo = (Grid)target;
				return;
			}
			if (connectionId == 39)
			{
				this.ImgFabricApi = (Image)target;
				return;
			}
			if (connectionId == 40)
			{
				this.LabFabricApi = (TextBlock)target;
				return;
			}
			if (connectionId == 41)
			{
				this.BtnFabricApiClear = (Grid)target;
				return;
			}
			if (connectionId == 42)
			{
				this.BtnFabricApiClearInner = (Path)target;
				return;
			}
			if (connectionId == 43)
			{
				this.CardOptiFine = (MyCard)target;
				return;
			}
			if (connectionId == 44)
			{
				this.PanOptiFine = (StackPanel)target;
				return;
			}
			if (connectionId == 45)
			{
				this.PanOptiFineInfo = (Grid)target;
				return;
			}
			if (connectionId == 46)
			{
				this.ImgOptiFine = (Image)target;
				return;
			}
			if (connectionId == 47)
			{
				this.LabOptiFine = (TextBlock)target;
				return;
			}
			if (connectionId == 48)
			{
				this.BtnOptiFineClear = (Grid)target;
				return;
			}
			if (connectionId == 49)
			{
				this.BtnOptiFineClearInner = (Path)target;
				return;
			}
			if (connectionId == 50)
			{
				this.CardOptiFabric = (MyCard)target;
				return;
			}
			if (connectionId == 51)
			{
				this.PanOptiFabric = (StackPanel)target;
				return;
			}
			if (connectionId == 52)
			{
				this.PanOptiFabricInfo = (Grid)target;
				return;
			}
			if (connectionId == 53)
			{
				this.ImgOptiFabric = (Image)target;
				return;
			}
			if (connectionId == 54)
			{
				this.LabOptiFabric = (TextBlock)target;
				return;
			}
			if (connectionId == 55)
			{
				this.BtnOptiFabricClear = (Grid)target;
				return;
			}
			if (connectionId == 56)
			{
				this.BtnOptiFabricClearInner = (Path)target;
				return;
			}
			if (connectionId == 57)
			{
				this.CardLiteLoader = (MyCard)target;
				return;
			}
			if (connectionId == 58)
			{
				this.PanLiteLoader = (StackPanel)target;
				return;
			}
			if (connectionId == 59)
			{
				this.PanLiteLoaderInfo = (Grid)target;
				return;
			}
			if (connectionId == 60)
			{
				this.ImgLiteLoader = (Image)target;
				return;
			}
			if (connectionId == 61)
			{
				this.LabLiteLoader = (TextBlock)target;
				return;
			}
			if (connectionId == 62)
			{
				this.BtnLiteLoaderClear = (Grid)target;
				return;
			}
			if (connectionId == 63)
			{
				this.BtnLiteLoaderClearInner = (Path)target;
				return;
			}
			if (connectionId == 64)
			{
				this.PanLoad = (MyCard)target;
				return;
			}
			if (connectionId == 65)
			{
				this.LoadMinecraft = (MyLoading)target;
				return;
			}
			if (connectionId == 66)
			{
				this.LoadOptiFine = (MyLoading)target;
				return;
			}
			if (connectionId == 67)
			{
				this.LoadForge = (MyLoading)target;
				return;
			}
			if (connectionId == 68)
			{
				this.LoadNeoForge = (MyLoading)target;
				return;
			}
			if (connectionId == 69)
			{
				this.LoadLiteLoader = (MyLoading)target;
				return;
			}
			if (connectionId == 70)
			{
				this.LoadFabric = (MyLoading)target;
				return;
			}
			if (connectionId == 71)
			{
				this.LoadFabricApi = (MyLoading)target;
				return;
			}
			if (connectionId == 72)
			{
				this.LoadOptiFabric = (MyLoading)target;
				return;
			}
			this.predicateReader = true;
		}

		// Token: 0x04000411 RID: 1041
		private bool m_DefinitionField;

		// Token: 0x04000412 RID: 1042
		public bool strategyField;

		// Token: 0x04000413 RID: 1043
		private bool m_ProcField;

		// Token: 0x04000414 RID: 1044
		private string parserReader;

		// Token: 0x04000415 RID: 1045
		private string m_BroadcasterReader;

		// Token: 0x04000416 RID: 1046
		private string _FieldReader;

		// Token: 0x04000417 RID: 1047
		private ModDownload.DlOptiFineListEntry readerReader;

		// Token: 0x04000418 RID: 1048
		private ModDownload.DlLiteLoaderListEntry m_ClientReader;

		// Token: 0x04000419 RID: 1049
		private ModDownload.DlForgeVersionEntry _ConfigReader;

		// Token: 0x0400041A RID: 1050
		private ModDownload.DlNeoForgeListEntry m_TestsReader;

		// Token: 0x0400041B RID: 1051
		private string m_MapperReader;

		// Token: 0x0400041C RID: 1052
		private ModComp.CompFile m_ThreadReader;

		// Token: 0x0400041D RID: 1053
		private ModComp.CompFile _PropertyReader;

		// Token: 0x0400041E RID: 1054
		private bool composerReader;

		// Token: 0x0400041F RID: 1055
		private bool iteratorReader;

		// Token: 0x04000420 RID: 1056
		private bool m_RepositoryReader;

		// Token: 0x04000421 RID: 1057
		public static string _ErrorReader = null;

		// Token: 0x04000422 RID: 1058
		private bool _ContextReader;

		// Token: 0x04000423 RID: 1059
		private bool _SpecificationReader;

		// Token: 0x04000424 RID: 1060
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyScrollViewer _MockReader;

		// Token: 0x04000425 RID: 1061
		[AccessedThroughProperty("PanMinecraft")]
		[CompilerGenerated]
		private StackPanel m_RequestReader;

		// Token: 0x04000426 RID: 1062
		[CompilerGenerated]
		[AccessedThroughProperty("PanSelect")]
		private StackPanel dicReader;

		// Token: 0x04000427 RID: 1063
		[CompilerGenerated]
		[AccessedThroughProperty("HintFabricAPI")]
		private MyHint _HelperReader;

		// Token: 0x04000428 RID: 1064
		[AccessedThroughProperty("HintOptiFabric")]
		[CompilerGenerated]
		private MyHint _IssuerReader;

		// Token: 0x04000429 RID: 1065
		[CompilerGenerated]
		[AccessedThroughProperty("HintOptiFabricOld")]
		private MyHint indexerReader;

		// Token: 0x0400042A RID: 1066
		[AccessedThroughProperty("HintModOptiFine")]
		[CompilerGenerated]
		private MyHint interpreterReader;

		// Token: 0x0400042B RID: 1067
		[AccessedThroughProperty("ItemSelect")]
		[CompilerGenerated]
		private MyListItem m_SerializerReader;

		// Token: 0x0400042C RID: 1068
		[AccessedThroughProperty("BtnSelectStart")]
		[CompilerGenerated]
		private MyButton _WatcherReader;

		// Token: 0x0400042D RID: 1069
		[AccessedThroughProperty("TextSelectName")]
		[CompilerGenerated]
		private MyTextBox identifierReader;

		// Token: 0x0400042E RID: 1070
		[AccessedThroughProperty("CardMinecraft")]
		[CompilerGenerated]
		private MyCard _SystemReader;

		// Token: 0x0400042F RID: 1071
		[AccessedThroughProperty("PanMinecraftInfo")]
		[CompilerGenerated]
		private Grid m_ParamReader;

		// Token: 0x04000430 RID: 1072
		[AccessedThroughProperty("ImgMinecraft")]
		[CompilerGenerated]
		private Image m_TagReader;

		// Token: 0x04000431 RID: 1073
		[CompilerGenerated]
		[AccessedThroughProperty("LabMinecraft")]
		private TextBlock observerReader;

		// Token: 0x04000432 RID: 1074
		[AccessedThroughProperty("CardForge")]
		[CompilerGenerated]
		private MyCard m_StubReader;

		// Token: 0x04000433 RID: 1075
		[AccessedThroughProperty("PanForge")]
		[CompilerGenerated]
		private StackPanel m_RulesReader;

		// Token: 0x04000434 RID: 1076
		[AccessedThroughProperty("PanForgeInfo")]
		[CompilerGenerated]
		private Grid _RefReader;

		// Token: 0x04000435 RID: 1077
		[AccessedThroughProperty("ImgForge")]
		[CompilerGenerated]
		private Image m_DecoratorReader;

		// Token: 0x04000436 RID: 1078
		[CompilerGenerated]
		[AccessedThroughProperty("LabForge")]
		private TextBlock instanceReader;

		// Token: 0x04000437 RID: 1079
		[AccessedThroughProperty("BtnForgeClear")]
		[CompilerGenerated]
		private Grid stateReader;

		// Token: 0x04000438 RID: 1080
		[AccessedThroughProperty("BtnForgeClearInner")]
		[CompilerGenerated]
		private Path callbackReader;

		// Token: 0x04000439 RID: 1081
		[CompilerGenerated]
		[AccessedThroughProperty("CardNeoForge")]
		private MyCard _TemplateReader;

		// Token: 0x0400043A RID: 1082
		[CompilerGenerated]
		[AccessedThroughProperty("PanNeoForge")]
		private StackPanel methodReader;

		// Token: 0x0400043B RID: 1083
		[AccessedThroughProperty("PanNeoForgeInfo")]
		[CompilerGenerated]
		private Grid taskReader;

		// Token: 0x0400043C RID: 1084
		[AccessedThroughProperty("ImgNeoForge")]
		[CompilerGenerated]
		private Image m_ConsumerReader;

		// Token: 0x0400043D RID: 1085
		[AccessedThroughProperty("LabNeoForge")]
		[CompilerGenerated]
		private TextBlock m_ConfigurationReader;

		// Token: 0x0400043E RID: 1086
		[AccessedThroughProperty("BtnNeoForgeClear")]
		[CompilerGenerated]
		private Grid _GetterReader;

		// Token: 0x0400043F RID: 1087
		[CompilerGenerated]
		[AccessedThroughProperty("BtnNeoForgeClearInner")]
		private Path tokenReader;

		// Token: 0x04000440 RID: 1088
		[AccessedThroughProperty("CardFabric")]
		[CompilerGenerated]
		private MyCard m_ExpressionReader;

		// Token: 0x04000441 RID: 1089
		[CompilerGenerated]
		[AccessedThroughProperty("PanFabric")]
		private StackPanel writerReader;

		// Token: 0x04000442 RID: 1090
		[CompilerGenerated]
		[AccessedThroughProperty("PanFabricInfo")]
		private Grid registryReader;

		// Token: 0x04000443 RID: 1091
		[AccessedThroughProperty("ImgFabric")]
		[CompilerGenerated]
		private Image m_RuleReader;

		// Token: 0x04000444 RID: 1092
		[CompilerGenerated]
		[AccessedThroughProperty("LabFabric")]
		private TextBlock _ProccesorReader;

		// Token: 0x04000445 RID: 1093
		[CompilerGenerated]
		[AccessedThroughProperty("BtnFabricClear")]
		private Grid m_SetterReader;

		// Token: 0x04000446 RID: 1094
		[AccessedThroughProperty("BtnFabricClearInner")]
		[CompilerGenerated]
		private Path factoryReader;

		// Token: 0x04000447 RID: 1095
		[CompilerGenerated]
		[AccessedThroughProperty("CardFabricApi")]
		private MyCard exporterReader;

		// Token: 0x04000448 RID: 1096
		[AccessedThroughProperty("PanFabricApi")]
		[CompilerGenerated]
		private StackPanel m_ImporterReader;

		// Token: 0x04000449 RID: 1097
		[AccessedThroughProperty("PanFabricApiInfo")]
		[CompilerGenerated]
		private Grid m_WorkerReader;

		// Token: 0x0400044A RID: 1098
		[CompilerGenerated]
		[AccessedThroughProperty("ImgFabricApi")]
		private Image _ConnectionReader;

		// Token: 0x0400044B RID: 1099
		[AccessedThroughProperty("LabFabricApi")]
		[CompilerGenerated]
		private TextBlock _ServerReader;

		// Token: 0x0400044C RID: 1100
		[AccessedThroughProperty("BtnFabricApiClear")]
		[CompilerGenerated]
		private Grid m_ResolverReader;

		// Token: 0x0400044D RID: 1101
		[AccessedThroughProperty("BtnFabricApiClearInner")]
		[CompilerGenerated]
		private Path statusReader;

		// Token: 0x0400044E RID: 1102
		[AccessedThroughProperty("CardOptiFine")]
		[CompilerGenerated]
		private MyCard roleReader;

		// Token: 0x0400044F RID: 1103
		[AccessedThroughProperty("PanOptiFine")]
		[CompilerGenerated]
		private StackPanel m_StructReader;

		// Token: 0x04000450 RID: 1104
		[AccessedThroughProperty("PanOptiFineInfo")]
		[CompilerGenerated]
		private Grid m_PrinterReader;

		// Token: 0x04000451 RID: 1105
		[AccessedThroughProperty("ImgOptiFine")]
		[CompilerGenerated]
		private Image _ValReader;

		// Token: 0x04000452 RID: 1106
		[AccessedThroughProperty("LabOptiFine")]
		[CompilerGenerated]
		private TextBlock _AttrReader;

		// Token: 0x04000453 RID: 1107
		[AccessedThroughProperty("BtnOptiFineClear")]
		[CompilerGenerated]
		private Grid m_CandidateReader;

		// Token: 0x04000454 RID: 1108
		[CompilerGenerated]
		[AccessedThroughProperty("BtnOptiFineClearInner")]
		private Path advisorReader;

		// Token: 0x04000455 RID: 1109
		[CompilerGenerated]
		[AccessedThroughProperty("CardOptiFabric")]
		private MyCard accountReader;

		// Token: 0x04000456 RID: 1110
		[CompilerGenerated]
		[AccessedThroughProperty("PanOptiFabric")]
		private StackPanel m_QueueReader;

		// Token: 0x04000457 RID: 1111
		[AccessedThroughProperty("PanOptiFabricInfo")]
		[CompilerGenerated]
		private Grid _EventReader;

		// Token: 0x04000458 RID: 1112
		[AccessedThroughProperty("ImgOptiFabric")]
		[CompilerGenerated]
		private Image _ManagerReader;

		// Token: 0x04000459 RID: 1113
		[AccessedThroughProperty("LabOptiFabric")]
		[CompilerGenerated]
		private TextBlock modelReader;

		// Token: 0x0400045A RID: 1114
		[AccessedThroughProperty("BtnOptiFabricClear")]
		[CompilerGenerated]
		private Grid _WrapperReader;

		// Token: 0x0400045B RID: 1115
		[AccessedThroughProperty("BtnOptiFabricClearInner")]
		[CompilerGenerated]
		private Path _BaseReader;

		// Token: 0x0400045C RID: 1116
		[AccessedThroughProperty("CardLiteLoader")]
		[CompilerGenerated]
		private MyCard m_AttributeReader;

		// Token: 0x0400045D RID: 1117
		[AccessedThroughProperty("PanLiteLoader")]
		[CompilerGenerated]
		private StackPanel _CodeReader;

		// Token: 0x0400045E RID: 1118
		[AccessedThroughProperty("PanLiteLoaderInfo")]
		[CompilerGenerated]
		private Grid prototypeReader;

		// Token: 0x0400045F RID: 1119
		[CompilerGenerated]
		[AccessedThroughProperty("ImgLiteLoader")]
		private Image _AnnotationReader;

		// Token: 0x04000460 RID: 1120
		[AccessedThroughProperty("LabLiteLoader")]
		[CompilerGenerated]
		private TextBlock m_InfoReader;

		// Token: 0x04000461 RID: 1121
		[AccessedThroughProperty("BtnLiteLoaderClear")]
		[CompilerGenerated]
		private Grid _AdapterReader;

		// Token: 0x04000462 RID: 1122
		[CompilerGenerated]
		[AccessedThroughProperty("BtnLiteLoaderClearInner")]
		private Path facadeReader;

		// Token: 0x04000463 RID: 1123
		[AccessedThroughProperty("PanLoad")]
		[CompilerGenerated]
		private MyCard listReader;

		// Token: 0x04000464 RID: 1124
		[AccessedThroughProperty("LoadMinecraft")]
		[CompilerGenerated]
		private MyLoading merchantReader;

		// Token: 0x04000465 RID: 1125
		[AccessedThroughProperty("LoadOptiFine")]
		[CompilerGenerated]
		private MyLoading m_AuthenticationReader;

		// Token: 0x04000466 RID: 1126
		[AccessedThroughProperty("LoadForge")]
		[CompilerGenerated]
		private MyLoading _AlgoReader;

		// Token: 0x04000467 RID: 1127
		[AccessedThroughProperty("LoadNeoForge")]
		[CompilerGenerated]
		private MyLoading m_ComparatorReader;

		// Token: 0x04000468 RID: 1128
		[CompilerGenerated]
		[AccessedThroughProperty("LoadLiteLoader")]
		private MyLoading m_MappingReader;

		// Token: 0x04000469 RID: 1129
		[CompilerGenerated]
		[AccessedThroughProperty("LoadFabric")]
		private MyLoading m_TokenizerReader;

		// Token: 0x0400046A RID: 1130
		[CompilerGenerated]
		[AccessedThroughProperty("LoadFabricApi")]
		private MyLoading _FilterReader;

		// Token: 0x0400046B RID: 1131
		[AccessedThroughProperty("LoadOptiFabric")]
		[CompilerGenerated]
		private MyLoading m_DatabaseReader;

		// Token: 0x0400046C RID: 1132
		private bool predicateReader;
	}
}
