using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020000E4 RID: 228
	[DesignerGenerated]
	public class PageDownloadCompDetail : MyPageRight, IComponentConnector
	{
		// Token: 0x06000840 RID: 2112 RVA: 0x000440C0 File Offset: 0x000422C0
		public PageDownloadCompDetail()
		{
			base.Initialized += this.PageDownloadCompDetail_Inited;
			base.Loaded += new RoutedEventHandler(this.PageDownloadCompDetail_Loaded);
			base.IncludeBroadcaster(new MyPageRight.PageEnterEventHandler(this.Init));
			this._PoolReader = null;
			this._CustomerReader = new ModLoader.LoaderTask<int, List<ModComp.CompFile>>("Comp File", delegate(ModLoader.LoaderTask<int, List<ModComp.CompFile>> Task)
			{
				Task.Output = ModComp.CompFilesGet(this._PageReader._AuthenticationRepository, this._PageReader.listRepository);
			}, null, ThreadPriority.Normal);
			this._ProcessReader = true;
			this.InitializeComponent();
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0004413C File Offset: 0x0004233C
		private void PageDownloadCompDetail_Inited(object sender, EventArgs e)
		{
			this._PageReader = (ModComp.CompProject)NewLateBinding.LateIndexGet(ModMain._ProcessIterator._MethodIterator.m_SingletonMap, new object[]
			{
				0
			}, null);
			this.m_InterceptorReader = Conversions.ToString(NewLateBinding.LateIndexGet(ModMain._ProcessIterator._MethodIterator.m_SingletonMap, new object[]
			{
				2
			}, null));
			this.containerReader = (ModComp.CompModLoaderType)Conversions.ToInteger(NewLateBinding.LateIndexGet(ModMain._ProcessIterator._MethodIterator.m_SingletonMap, new object[]
			{
				3
			}, null));
			base.PageLoaderInit(this.Load, this.PanLoad, this.PanMain, this.CardIntro, this._CustomerReader, delegate(ModLoader.LoaderBase a0)
			{
				this.Load_OnFinish();
			}, null, true);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00044208 File Offset: 0x00042408
		private void PageDownloadCompDetail_Loaded(object sender, EventArgs e)
		{
			this._PageReader = (ModComp.CompProject)NewLateBinding.LateIndexGet(ModMain._ProcessIterator._MethodIterator.m_SingletonMap, new object[]
			{
				0
			}, null);
			this.m_InterceptorReader = Conversions.ToString(NewLateBinding.LateIndexGet(ModMain._ProcessIterator._MethodIterator.m_SingletonMap, new object[]
			{
				2
			}, null));
			this.containerReader = (ModComp.CompModLoaderType)Conversions.ToInteger(NewLateBinding.LateIndexGet(ModMain._ProcessIterator._MethodIterator.m_SingletonMap, new object[]
			{
				3
			}, null));
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000442A4 File Offset: 0x000424A4
		private void Load_State(object sender, MyLoading.MyLoadingState state, MyLoading.MyLoadingState oldState)
		{
			ModBase.LoadState state2 = this._CustomerReader.State;
			if (state2 == ModBase.LoadState.Failed)
			{
				string text = "";
				if (this._CustomerReader.Error != null)
				{
					text = this._CustomerReader.Error.Message;
				}
				if (text.Contains("不是有效的 json 文件"))
				{
					ModBase.Log("[Comp] 下载的文件 json 列表损坏，已自动重试", ModBase.LogLevel.Debug, "出现错误");
					base.PageLoaderRestart(null, true);
				}
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0004430C File Offset: 0x0004250C
		private void Load_OnFinish()
		{
			this.dispatcherReader = false;
			List<string> list = Enumerable.ToList<string>(Enumerable.OrderByDescending<string, string>(Enumerable.Distinct<string>(Enumerable.Select<string, string>(Enumerable.SelectMany<ModComp.CompFile, string>(this._CustomerReader.Output, (PageDownloadCompDetail._Closure$__.$I13-0 == null) ? (PageDownloadCompDetail._Closure$__.$I13-0 = ((ModComp.CompFile v) => v.m_ValueRepository)) : PageDownloadCompDetail._Closure$__.$I13-0), (string v) => this.GetGroupedVersionName(v, this.dispatcherReader, true))), (PageDownloadCompDetail._Closure$__.$I13-2 == null) ? (PageDownloadCompDetail._Closure$__.$I13-2 = ((string s) => s)) : PageDownloadCompDetail._Closure$__.$I13-2, new ModMinecraft.VersionComparer()));
			if (list.Count >= 9)
			{
				this.dispatcherReader = true;
				list = Enumerable.ToList<string>(Enumerable.OrderByDescending<string, string>(Enumerable.Distinct<string>(Enumerable.Select<string, string>(Enumerable.SelectMany<ModComp.CompFile, string>(this._CustomerReader.Output, (PageDownloadCompDetail._Closure$__.$I13-3 == null) ? (PageDownloadCompDetail._Closure$__.$I13-3 = ((ModComp.CompFile v) => v.m_ValueRepository)) : PageDownloadCompDetail._Closure$__.$I13-3), (string v) => this.GetGroupedVersionName(v, this.dispatcherReader, true))), (PageDownloadCompDetail._Closure$__.$I13-5 == null) ? (PageDownloadCompDetail._Closure$__.$I13-5 = ((string s) => s)) : PageDownloadCompDetail._Closure$__.$I13-5, new ModMinecraft.VersionComparer()));
			}
			this.PanFilter.Children.Clear();
			if (list.Count <= 2)
			{
				this.CardFilter.Visibility = Visibility.Collapsed;
				this.paramsReader = null;
			}
			else
			{
				this.CardFilter.Visibility = Visibility.Visible;
				list.Insert(0, "全部");
				try
				{
					foreach (string text in list)
					{
						MyRadioButton myRadioButton = new MyRadioButton
						{
							Text = text,
							Margin = new Thickness(2.0, 0.0, 2.0, 0.0),
							ColorType = MyRadioButton.ColorState.Highlight
						};
						myRadioButton.LabText.Margin = new Thickness(-2.0, 0.0, 8.0, 0.0);
						myRadioButton.LogoutTests(delegate(object a0, bool a1)
						{
							this._Lambda$__13-6((MyRadioButton)a0, a1);
						});
						this.PanFilter.Children.Add(myRadioButton);
					}
				}
				finally
				{
					List<string>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				MyRadioButton myRadioButton2 = null;
				if (Operators.CompareString(this.m_InterceptorReader, "", false) != 0 && Enumerable.FirstOrDefault<ModComp.CompFile>(this._CustomerReader.Output, (ModComp.CompFile v) => v.m_ValueRepository.Contains(this.m_InterceptorReader)) != null)
				{
					string groupedVersionName = this.GetGroupedVersionName(this.m_InterceptorReader, this.dispatcherReader, true);
					try
					{
						foreach (object obj in this.PanFilter.Children)
						{
							MyRadioButton myRadioButton3 = (MyRadioButton)obj;
							if (Operators.CompareString(myRadioButton3.Text, groupedVersionName, false) == 0)
							{
								myRadioButton2 = myRadioButton3;
								break;
							}
						}
					}
					finally
					{
						IEnumerator enumerator2;
						if (enumerator2 is IDisposable)
						{
							(enumerator2 as IDisposable).Dispose();
						}
					}
				}
				if (myRadioButton2 == null)
				{
					myRadioButton2 = (MyRadioButton)this.PanFilter.Children[0];
				}
				myRadioButton2.Checked = true;
			}
			this.UpdateFilterResult();
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00044630 File Offset: 0x00042830
		private void UpdateFilterResult()
		{
			string text = (Operators.CompareString(this.m_InterceptorReader, "", false) != 0 || this.containerReader != ModComp.CompModLoaderType.Any) ? string.Format("所选版本：{0}{1}", (this.containerReader != ModComp.CompModLoaderType.Any) ? (this.containerReader.ToString() + " ") : "", this.m_InterceptorReader) : "";
			SortedDictionary<string, List<ModComp.CompFile>> sortedDictionary = new SortedDictionary<string, List<ModComp.CompFile>>(new PageDownloadCompDetail.CardSorter(text));
			sortedDictionary.Add("其他版本", new List<ModComp.CompFile>());
			List<int> list = new List<int>((IEnumerable<int>)Enum.GetValues(typeof(ModComp.CompModLoaderType)));
			try
			{
				foreach (ModComp.CompFile compFile in this._CustomerReader.Output)
				{
					try
					{
						foreach (string name in compFile.m_ValueRepository)
						{
							if (this.paramsReader == null || Operators.CompareString(this.GetGroupedVersionName(name, this.dispatcherReader, true), this.paramsReader, false) == 0)
							{
								string groupedVersionName = this.GetGroupedVersionName(name, false, false);
								List<string> list2 = new List<string>();
								if (this._PageReader.predicateRepository.Count > 1 && this._PageReader.Type == ModComp.CompType.Mod && groupedVersionName.StartsWith("1."))
								{
									try
									{
										foreach (ModComp.CompModLoaderType compModLoaderType in compFile._VisitorRepository)
										{
											if (!Conversions.ToBoolean(compModLoaderType == ModComp.CompModLoaderType.Quilt && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("ToolDownloadIgnoreQuilt", null))) && list.Contains((int)compModLoaderType))
											{
												list2.Add(compModLoaderType.ToString() + " ");
											}
										}
									}
									finally
									{
										List<ModComp.CompModLoaderType>.Enumerator enumerator3;
										((IDisposable)enumerator3).Dispose();
									}
								}
								if (!Enumerable.Any<string>(list2))
								{
									list2.Add("");
								}
								try
								{
									foreach (string str in list2)
									{
										string text2 = str + groupedVersionName;
										if (!sortedDictionary.ContainsKey(text2))
										{
											sortedDictionary.Add(text2, new List<ModComp.CompFile>());
										}
										if (!sortedDictionary[text2].Contains(compFile))
										{
											sortedDictionary[text2].Add(compFile);
										}
									}
								}
								finally
								{
									List<string>.Enumerator enumerator4;
									((IDisposable)enumerator4).Dispose();
								}
							}
						}
					}
					finally
					{
						List<string>.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
				}
			}
			finally
			{
				List<ModComp.CompFile>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			if (Operators.CompareString(text, "", false) != 0)
			{
				sortedDictionary.Add(text, new List<ModComp.CompFile>());
				try
				{
					foreach (ModComp.CompFile compFile2 in this._CustomerReader.Output)
					{
						if (compFile2.m_ValueRepository.Contains(this.m_InterceptorReader) && (this.containerReader == ModComp.CompModLoaderType.Any || compFile2._VisitorRepository.Contains(this.containerReader)) && (this.paramsReader == null || Enumerable.Any<string>(compFile2.m_ValueRepository, (string v) => Operators.CompareString(this.GetGroupedVersionName(v, this.dispatcherReader, true), this.paramsReader, false) == 0)) && !sortedDictionary[text].Contains(compFile2))
						{
							sortedDictionary[text].Add(compFile2);
						}
					}
				}
				finally
				{
					List<ModComp.CompFile>.Enumerator enumerator5;
					((IDisposable)enumerator5).Dispose();
				}
			}
			try
			{
				this.PanResults.Children.Clear();
				try
				{
					foreach (KeyValuePair<string, List<ModComp.CompFile>> keyValuePair in sortedDictionary)
					{
						if (Enumerable.Any<ModComp.CompFile>(keyValuePair.Value))
						{
							MyCard myCard = new MyCard();
							myCard.Title = keyValuePair.Key;
							myCard.Margin = new Thickness(0.0, 0.0, 0.0, 15.0);
							myCard.CreateParser((this._PageReader.Type == ModComp.CompType.ModPack) ? 9 : 8);
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
							this.PanResults.Children.Add(myCard2);
							if (Operators.CompareString(keyValuePair.Key, text, false) != 0 && (ModMain._ProcessIterator._MethodIterator.m_SingletonMap == null || !((List<string>)NewLateBinding.LateIndexGet(ModMain._ProcessIterator._MethodIterator.m_SingletonMap, new object[]
							{
								1
							}, null)).Contains(myCard2.Title)))
							{
								myCard2.IsSwaped = true;
							}
							else
							{
								MyCard.StackInstall(ref stackPanel, (this._PageReader.Type == ModComp.CompType.ModPack) ? 9 : 8, keyValuePair.Key);
							}
							if (Operators.CompareString(keyValuePair.Key, "其他版本", false) == 0)
							{
								stackPanel.Children.Add(new MyHint
								{
									Text = "由于版本信息更新缓慢，可能无法识别刚更新的 MC 版本，只需等待几天即可自动恢复正常。",
									IsWarn = false,
									Margin = new Thickness(0.0, 0.0, 0.0, 7.0)
								});
							}
						}
					}
				}
				finally
				{
					SortedDictionary<string, List<ModComp.CompFile>>.Enumerator enumerator6;
					enumerator6.Dispose();
				}
				if (this.PanResults.Children.Count == 1)
				{
					((MyCard)this.PanResults.Children[0]).IsSwaped = false;
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "可视化工程下载列表出错", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00044CAC File Offset: 0x00042EAC
		private string GetGroupedVersionName(string Name, bool MajorOnly, bool FoldOldRelease)
		{
			string result;
			if (Name == null)
			{
				result = "其他版本";
			}
			else if (Name.Contains("w"))
			{
				result = "快照版本";
			}
			else if (!Name.StartsWith("1.0") && Name.StartsWith("1.") && (!FoldOldRelease || ModBase.Val(Name.Split(".")[1]) >= 10.0))
			{
				result = (MajorOnly ? ("1." + Name.Split(".")[1].BeforeFirst(" ", false)) : Name);
			}
			else
			{
				result = "远古版本";
			}
			return result;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00044D48 File Offset: 0x00042F48
		public void Init()
		{
			checked
			{
				ModAnimation.AssetParser(ModAnimation.CalcParser() + 1);
				this._PageReader = (ModComp.CompProject)NewLateBinding.LateIndexGet(ModMain._ProcessIterator._MethodIterator.m_SingletonMap, new object[]
				{
					0
				}, null);
				this.PanBack.ScrollToHome();
				if (this._ProcessReader)
				{
					this._ProcessReader = false;
				}
				else
				{
					base.PageLoaderRestart(null, true);
				}
				if (this._PoolReader != null)
				{
					this.PanIntro.Children.Remove(this._PoolReader);
				}
				this._PoolReader = this._PageReader.ToCompItem(true, true);
				this._PoolReader._EventField = false;
				this._PoolReader.Margin = new Thickness(-7.0, -7.0, 0.0, 8.0);
				this.PanIntro.Children.Insert(0, this._PoolReader);
				this.BtnIntroWeb.Text = (this._PageReader.listRepository ? "转到 CurseForge" : "转到 Modrinth");
				this.BtnIntroWiki.Visibility = ((this._PageReader.FlushTests() == 0) ? Visibility.Collapsed : Visibility.Visible);
				ModAnimation.AssetParser(ModAnimation.CalcParser() - 1);
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00044E88 File Offset: 0x00043088
		public void Install_Click(MyListItem sender, EventArgs e)
		{
			try
			{
				ModComp.CompFile compFile = (ModComp.CompFile)sender.Tag;
				string name = string.Format("{0} 整合包下载：{1} ", this._PageReader.listRepository ? "CurseForge" : "Modrinth", this._PageReader.RemoveTests());
				string text = this._PageReader.RemoveTests().Replace(".zip", "").Replace(".rar", "").Replace(".mrpack", "").Replace("\\", "＼").Replace("/", "／").Replace("|", "｜").Replace(":", "：").Replace("<", "＜").Replace(">", "＞").Replace("*", "＊").Replace("?", "？").Replace("\"", "").Replace("： ", "：");
				ValidateFolderName validateFolderName = new ValidateFolderName(ModMinecraft.m_ProxyTests + "versions", true, true);
				if (Operators.CompareString(validateFolderName.Validate(text), "", false) != 0)
				{
					text = "";
				}
				string VersionName = ModMain.MyMsgBoxInput("输入版本名称", "", text, new Collection<Validate>
				{
					validateFolderName
				}, "", "确定", "取消", false);
				if (!string.IsNullOrEmpty(VersionName))
				{
					List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
					string Target = string.Format("{0}versions\\{1}\\原始整合包.{2}", ModMinecraft.m_ProxyTests, VersionName, this._PageReader.listRepository ? "zip" : "mrpack");
					string LogoFileAddress = MyImage.GetTempPath(this._PoolReader.Logo);
					list.Add(new ModNet.LoaderDownload("下载整合包文件", new List<ModNet.NetFile>
					{
						compFile.ToNetFile(Target)
					})
					{
						ProgressWeight = 10.0,
						Block = true
					});
					list.Add(new ModLoader.LoaderTask<int, int>("准备安装整合包", delegate(ModLoader.LoaderTask<int, int> a0)
					{
						base._Lambda$__0();
					}, null, ThreadPriority.Normal)
					{
						ProgressWeight = 0.1
					});
					ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(name, list);
					loaderCombo.OnStateChanged = ((PageDownloadCompDetail._Closure$__.$I18-1 == null) ? (PageDownloadCompDetail._Closure$__.$I18-1 = delegate(ModLoader.LoaderBase MyLoader)
					{
						switch (MyLoader.State)
						{
						case ModBase.LoadState.Loading:
							return;
						case ModBase.LoadState.Failed:
							ModMain.Hint(MyLoader.Name + "失败：" + ModBase.GetExceptionSummary(MyLoader.Error), ModMain.HintType.Critical, true);
							break;
						case ModBase.LoadState.Aborted:
							ModMain.Hint(MyLoader.Name + "已取消！", ModMain.HintType.Info, true);
							break;
						}
						ModDownloadLib.McInstallFailedClearFolder(MyLoader);
					}) : PageDownloadCompDetail._Closure$__.$I18-1);
					loaderCombo.Start(ModMinecraft.m_ProxyTests + "versions\\" + VersionName + "\\", false);
					ModLoader.LoaderTaskbarAdd<string>(loaderCombo);
					ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
					ModMain._ProcessIterator.BtnExtraDownload.Ribble();
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "下载资源整合包失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00045190 File Offset: 0x00043390
		public void Save_Click(object sender, EventArgs e)
		{
			PageDownloadCompDetail._Closure$__20-0 CS$<>8__locals1 = new PageDownloadCompDetail._Closure$__20-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Me = this;
			CS$<>8__locals1.$VB$Local_File = (ModComp.CompFile)NewLateBinding.LateGet((sender is MyListItem) ? sender : NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
			ModBase.RunInNewThread(delegate
			{
				try
				{
					PageDownloadCompDetail._Closure$__20-3 CS$<>8__locals2 = new PageDownloadCompDetail._Closure$__20-3(CS$<>8__locals2);
					CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2 = CS$<>8__locals1;
					CS$<>8__locals2.$VB$Local_Desc = ((CS$<>8__locals1.$VB$Me._PageReader.Type == ModComp.CompType.ModPack) ? "整合包" : ((CS$<>8__locals1.$VB$Me._PageReader.Type == ModComp.CompType.Mod) ? "Mod " : "资源包"));
					CS$<>8__locals2.$VB$Local_DefaultFolder = null;
					if (CS$<>8__locals1.$VB$Me._PageReader.Type == ModComp.CompType.Mod)
					{
						PageDownloadCompDetail._Closure$__20-2 CS$<>8__locals3 = new PageDownloadCompDetail._Closure$__20-2(CS$<>8__locals3);
						CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3 = CS$<>8__locals2;
						CS$<>8__locals3.$VB$Local_AllowForge = null;
						CS$<>8__locals3.$VB$Local_AllowFabric = null;
						if (Enumerable.Any<ModComp.CompModLoaderType>(CS$<>8__locals1.$VB$Local_File._VisitorRepository))
						{
							CS$<>8__locals3.$VB$Local_AllowForge = new bool?(CS$<>8__locals1.$VB$Local_File._VisitorRepository.Contains(ModComp.CompModLoaderType.Forge) || CS$<>8__locals1.$VB$Local_File._VisitorRepository.Contains(ModComp.CompModLoaderType.NeoForge));
							CS$<>8__locals3.$VB$Local_AllowFabric = new bool?(CS$<>8__locals1.$VB$Local_File._VisitorRepository.Contains(ModComp.CompModLoaderType.Fabric));
						}
						else if (Enumerable.Any<ModComp.CompModLoaderType>(CS$<>8__locals1.$VB$Me._PageReader.predicateRepository))
						{
							CS$<>8__locals3.$VB$Local_AllowForge = new bool?(CS$<>8__locals1.$VB$Me._PageReader.predicateRepository.Contains(ModComp.CompModLoaderType.Forge) || CS$<>8__locals1.$VB$Local_File._VisitorRepository.Contains(ModComp.CompModLoaderType.NeoForge));
							CS$<>8__locals3.$VB$Local_AllowFabric = new bool?(CS$<>8__locals1.$VB$Me._PageReader.predicateRepository.Contains(ModComp.CompModLoaderType.Fabric));
						}
						bool? flag2;
						bool? flag = flag2 = ((CS$<>8__locals3.$VB$Local_AllowForge != null) ? ((CS$<>8__locals3.$VB$Local_AllowForge != null) ? new bool?(!CS$<>8__locals3.$VB$Local_AllowForge.GetValueOrDefault()) : CS$<>8__locals3.$VB$Local_AllowForge) : new bool?(false));
						bool? flag3 = (flag2 == null || flag.GetValueOrDefault()) ? (flag & CS$<>8__locals3.$VB$Local_AllowFabric != null) : new bool?(false);
						if ((flag3 == null || flag3.GetValueOrDefault()) && ((CS$<>8__locals3.$VB$Local_AllowFabric != null) ? new bool?(!CS$<>8__locals3.$VB$Local_AllowFabric.GetValueOrDefault()) : CS$<>8__locals3.$VB$Local_AllowFabric).GetValueOrDefault() && flag3 != null)
						{
							CS$<>8__locals3.$VB$Local_AllowForge = null;
							CS$<>8__locals3.$VB$Local_AllowFabric = null;
						}
						ModBase.Log(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("[Comp] 允许 Forge：", (CS$<>8__locals3.$VB$Local_AllowForge != null) ? CS$<>8__locals3.$VB$Local_AllowForge.GetValueOrDefault() : "未知"), "，允许 Fabric："), (CS$<>8__locals3.$VB$Local_AllowFabric != null) ? CS$<>8__locals3.$VB$Local_AllowFabric.GetValueOrDefault() : "未知")), ModBase.LogLevel.Normal, "出现错误");
						Func<ModMinecraft.McVersion, bool> func = delegate(ModMinecraft.McVersion Version)
						{
							PageDownloadCompDetail._Closure$__20-1 CS$<>8__locals4 = new PageDownloadCompDetail._Closure$__20-1(CS$<>8__locals4);
							CS$<>8__locals4.$VB$Local_Version = Version;
							if (!CS$<>8__locals4.$VB$Local_Version.importerMap)
							{
								CS$<>8__locals4.$VB$Local_Version.Load();
							}
							bool result;
							if (!CS$<>8__locals4.$VB$Local_Version.RunThread())
							{
								result = false;
							}
							else if (Enumerable.Any<string>(CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3.$VB$NonLocal_$VB$Closure_2.$VB$Local_File.m_ValueRepository, (PageDownloadCompDetail._Closure$__.$I20-2 == null) ? (PageDownloadCompDetail._Closure$__.$I20-2 = ((string v) => v.Contains("."))) : PageDownloadCompDetail._Closure$__.$I20-2) && !Enumerable.Any<string>(CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3.$VB$NonLocal_$VB$Closure_2.$VB$Local_File.m_ValueRepository, (string v) => v.Contains(".") && Operators.CompareString(v, CS$<>8__locals4.$VB$Local_Version.Version._ConnectionMap, false) == 0))
							{
								result = false;
							}
							else if (CS$<>8__locals3.$VB$Local_AllowForge != null && CS$<>8__locals3.$VB$Local_AllowFabric != null)
							{
								bool? flag5 = CS$<>8__locals3.$VB$Local_AllowForge;
								if ((flag5 == null || flag5.GetValueOrDefault()) && (CS$<>8__locals4.$VB$Local_Version.Version.m_StructMap || CS$<>8__locals4.$VB$Local_Version.Version._ValMap) && flag5 != null)
								{
									result = true;
								}
								else
								{
									flag5 = CS$<>8__locals3.$VB$Local_AllowFabric;
									result = ((flag5 == null || flag5.GetValueOrDefault()) && CS$<>8__locals4.$VB$Local_Version.Version._CandidateMap && flag5 != null);
								}
							}
							else
							{
								result = true;
							}
							return result;
						};
						if (PageDownloadCompDetail.parameterReader != null)
						{
							CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3.$VB$Local_DefaultFolder = PageDownloadCompDetail.parameterReader;
							ModBase.Log("[Comp] 使用上次下载时的文件夹作为默认下载位置", ModBase.LogLevel.Normal, "出现错误");
						}
						else if (ModMinecraft.AddClient() != null && func(ModMinecraft.AddClient()))
						{
							CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3.$VB$Local_DefaultFolder = ModMinecraft.AddClient().ChangeMapper() + "mods\\";
							Directory.CreateDirectory(CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3.$VB$Local_DefaultFolder);
							ModBase.Log("[Comp] 使用当前版本的 mods 文件夹作为默认下载位置（" + ModMinecraft.AddClient().Name + "）", ModBase.LogLevel.Normal, "出现错误");
						}
						else
						{
							bool flag4;
							if (flag4 = (ModMinecraft._ListenerTests.State != ModBase.LoadState.Finished))
							{
								ModMain.Hint("正在查找适合的游戏版本……", ModMain.HintType.Info, true);
								ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", true);
							}
							List<ModMinecraft.McVersion> list = new List<ModMinecraft.McVersion>();
							try
							{
								foreach (ModMinecraft.McVersion mcVersion in Enumerable.SelectMany<List<ModMinecraft.McVersion>, ModMinecraft.McVersion>(ModMinecraft._RegTests.Values, (PageDownloadCompDetail._Closure$__.$I20-4 == null) ? (PageDownloadCompDetail._Closure$__.$I20-4 = ((List<ModMinecraft.McVersion> l) => l)) : PageDownloadCompDetail._Closure$__.$I20-4))
								{
									if (func(mcVersion))
									{
										list.Add(mcVersion);
									}
								}
							}
							finally
							{
								IEnumerator<ModMinecraft.McVersion> enumerator;
								if (enumerator != null)
								{
									enumerator.Dispose();
								}
							}
							if (!Enumerable.Any<ModMinecraft.McVersion>(list))
							{
								CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3.$VB$Local_DefaultFolder = ModMinecraft.m_ProxyTests;
								if (flag4)
								{
									ModMain.Hint("当前 MC 文件夹中没有找到适合这个 Mod 的版本！", ModMain.HintType.Info, true);
								}
								else
								{
									ModBase.Log("[Comp] 由于当前版本不兼容，使用当前的 MC 文件夹作为默认下载位置", ModBase.LogLevel.Normal, "出现错误");
								}
							}
							else
							{
								ModMinecraft.McVersion mcVersion2 = Enumerable.LastOrDefault<ModMinecraft.McVersion>(Enumerable.OrderBy<ModMinecraft.McVersion, int>(list, (PageDownloadCompDetail._Closure$__.$I20-5 == null) ? (PageDownloadCompDetail._Closure$__.$I20-5 = delegate(ModMinecraft.McVersion v)
								{
									DirectoryInfo directoryInfo = new DirectoryInfo(v.ChangeMapper() + "mods\\");
									if (!directoryInfo.Exists)
									{
										return -1;
									}
									return directoryInfo.GetFiles().Length;
								}) : PageDownloadCompDetail._Closure$__.$I20-5));
								CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3.$VB$Local_DefaultFolder = mcVersion2.ChangeMapper() + "mods\\";
								Directory.CreateDirectory(CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3.$VB$Local_DefaultFolder);
								ModBase.Log("[Comp] 使用适合的游戏版本作为默认下载位置（" + mcVersion2.Name + "）", ModBase.LogLevel.Normal, "出现错误");
							}
						}
					}
					if (Operators.CompareString(CS$<>8__locals1.$VB$Me._PageReader.RemoveTests(), CS$<>8__locals1.$VB$Me._PageReader.comparatorRepository, false) == 0)
					{
						CS$<>8__locals2.$VB$Local_FileName = CS$<>8__locals1.$VB$Local_File._BridgeRepository;
					}
					else
					{
						string text = CS$<>8__locals1.$VB$Me._PageReader.RemoveTests().BeforeFirst(" (", false).BeforeFirst(" - ", false).Replace("\\", "＼").Replace("/", "／").Replace("|", "｜").Replace(":", "：").Replace("<", "＜").Replace(">", "＞").Replace("*", "＊").Replace("?", "？").Replace("\"", "").Replace("： ", "：");
						object left = ModBase.m_IdentifierRepository.Get("ToolDownloadTranslate", null);
						if (Operators.ConditionalCompareObjectEqual(left, 0, false))
						{
							CS$<>8__locals2.$VB$Local_FileName = string.Format("[{0}] {1}", text, CS$<>8__locals1.$VB$Local_File._BridgeRepository);
						}
						else if (Operators.ConditionalCompareObjectEqual(left, 1, false))
						{
							CS$<>8__locals2.$VB$Local_FileName = string.Format("{0}-{1}", text, CS$<>8__locals1.$VB$Local_File._BridgeRepository);
						}
						else if (Operators.ConditionalCompareObjectEqual(left, 2, false))
						{
							CS$<>8__locals2.$VB$Local_FileName = string.Format("{0}-{1}", CS$<>8__locals1.$VB$Local_File._BridgeRepository, text);
						}
						else
						{
							CS$<>8__locals2.$VB$Local_FileName = CS$<>8__locals1.$VB$Local_File._BridgeRepository;
						}
					}
					ModBase.RunInUi(delegate()
					{
						string text2 = ModBase.SelectAs("选择保存位置", CS$<>8__locals2.$VB$Local_FileName, CS$<>8__locals2.$VB$Local_Desc + "文件|" + ((CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Me._PageReader.Type == ModComp.CompType.Mod) ? (CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_File._BridgeRepository.EndsWith(".litemod") ? "*.litemod" : "*.jar") : (CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_File._BridgeRepository.EndsWith(".mrpack") ? "*.mrpack" : "*.zip")), CS$<>8__locals2.$VB$Local_DefaultFolder);
						if (text2.Contains("\\"))
						{
							string name = CS$<>8__locals2.$VB$Local_Desc + "下载：" + ModBase.GetFileNameWithoutExtentionFromPath(text2) + " ";
							if (Operators.CompareString(text2, CS$<>8__locals2.$VB$Local_DefaultFolder, false) != 0 && CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Me._PageReader.Type == ModComp.CompType.Mod)
							{
								PageDownloadCompDetail.parameterReader = ModBase.GetPathFromFullPath(text2);
							}
							ModLoader.LoaderCombo<int> loaderCombo = new ModLoader.LoaderCombo<int>(name, new List<ModLoader.LoaderBase>
							{
								new ModNet.LoaderDownload("下载文件", new List<ModNet.NetFile>
								{
									CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_File.ToNetFile(text2)
								})
								{
									ProgressWeight = 6.0,
									Block = true
								}
							});
							loaderCombo.OnStateChanged = new Action<ModLoader.LoaderBase>(ModDownloadLib.DownloadStateSave);
							loaderCombo.Start(1, false);
							ModLoader.LoaderTaskbarAdd<int>(loaderCombo);
							ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
							ModMain._ProcessIterator.BtnExtraDownload.Ribble();
						}
					}, false);
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "保存资源文件失败", ModBase.LogLevel.Feedback, "出现错误");
				}
			}, "Download CompDetail Save", ThreadPriority.Normal);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000063AE File Offset: 0x000045AE
		private void BtnIntroWeb_Click(object sender, EventArgs e)
		{
			ModBase.OpenWebsite(this._PageReader.m_TokenizerRepository);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000063C0 File Offset: 0x000045C0
		private void BtnIntroWiki_Click(object sender, EventArgs e)
		{
			ModBase.OpenWebsite("https://www.mcmod.cn/class/" + Conversions.ToString(this._PageReader.FlushTests()) + ".html");
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000063E6 File Offset: 0x000045E6
		private void BtnIntroCopy_Click(object sender, EventArgs e)
		{
			ModBase.ClipboardSet(this._PoolReader.LabTitle.Text, true);
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x000063FE File Offset: 0x000045FE
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x00006406 File Offset: 0x00004606
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x0000640F File Offset: 0x0000460F
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x00006417 File Offset: 0x00004617
		internal virtual MyCard CardIntro { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00006420 File Offset: 0x00004620
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x00006428 File Offset: 0x00004628
		internal virtual StackPanel PanIntro { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x00006431 File Offset: 0x00004631
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x00045204 File Offset: 0x00043404
		internal virtual MyButton BtnIntroWeb
		{
			[CompilerGenerated]
			get
			{
				return this.m_ProxyReader;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnIntroWeb_Click);
				MyButton proxyReader = this.m_ProxyReader;
				if (proxyReader != null)
				{
					proxyReader.Click -= value2;
				}
				this.m_ProxyReader = value;
				proxyReader = this.m_ProxyReader;
				if (proxyReader != null)
				{
					proxyReader.Click += value2;
				}
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00006439 File Offset: 0x00004639
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x00045248 File Offset: 0x00043448
		internal virtual MyButton BtnIntroWiki
		{
			[CompilerGenerated]
			get
			{
				return this.messageReader;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnIntroWiki_Click);
				MyButton myButton = this.messageReader;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.messageReader = value;
				myButton = this.messageReader;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x00006441 File Offset: 0x00004641
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x0004528C File Offset: 0x0004348C
		internal virtual MyButton BtnIntroCopy
		{
			[CompilerGenerated]
			get
			{
				return this.creatorReader;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnIntroCopy_Click);
				MyButton myButton = this.creatorReader;
				if (myButton != null)
				{
					myButton.Click -= value2;
				}
				this.creatorReader = value;
				myButton = this.creatorReader;
				if (myButton != null)
				{
					myButton.Click += value2;
				}
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00006449 File Offset: 0x00004649
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x00006451 File Offset: 0x00004651
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0000645A File Offset: 0x0000465A
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x00006462 File Offset: 0x00004662
		internal virtual MyCard CardFilter { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0000646B File Offset: 0x0000466B
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x00006473 File Offset: 0x00004673
		internal virtual StackPanel PanFilter { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x0000647C File Offset: 0x0000467C
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x00006484 File Offset: 0x00004684
		internal virtual StackPanel PanResults { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x0000648D File Offset: 0x0000468D
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x00006495 File Offset: 0x00004695
		internal virtual MyCard PanLoad { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0000649E File Offset: 0x0000469E
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x000452D0 File Offset: 0x000434D0
		internal virtual MyLoading Load
		{
			[CompilerGenerated]
			get
			{
				return this.m_CollectionReader;
			}
			[CompilerGenerated]
			set
			{
				MyLoading.StateChangedEventHandler obj = new MyLoading.StateChangedEventHandler(this.Load_State);
				MyLoading collectionReader = this.m_CollectionReader;
				if (collectionReader != null)
				{
					collectionReader.InterruptField(obj);
				}
				this.m_CollectionReader = value;
				collectionReader = this.m_CollectionReader;
				if (collectionReader != null)
				{
					collectionReader.PrintField(obj);
				}
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00045314 File Offset: 0x00043514
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_VisitorReader)
			{
				this.m_VisitorReader = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagedownload/pagedownloadcompdetail.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00045344 File Offset: 0x00043544
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
				this.CardIntro = (MyCard)target;
				return;
			}
			if (connectionId == 3)
			{
				this.PanIntro = (StackPanel)target;
				return;
			}
			if (connectionId == 4)
			{
				this.BtnIntroWeb = (MyButton)target;
				return;
			}
			if (connectionId == 5)
			{
				this.BtnIntroWiki = (MyButton)target;
				return;
			}
			if (connectionId == 6)
			{
				this.BtnIntroCopy = (MyButton)target;
				return;
			}
			if (connectionId == 7)
			{
				this.PanMain = (StackPanel)target;
				return;
			}
			if (connectionId == 8)
			{
				this.CardFilter = (MyCard)target;
				return;
			}
			if (connectionId == 9)
			{
				this.PanFilter = (StackPanel)target;
				return;
			}
			if (connectionId == 10)
			{
				this.PanResults = (StackPanel)target;
				return;
			}
			if (connectionId == 11)
			{
				this.PanLoad = (MyCard)target;
				return;
			}
			if (connectionId == 12)
			{
				this.Load = (MyLoading)target;
				return;
			}
			this.m_VisitorReader = true;
		}

		// Token: 0x04000475 RID: 1141
		private MyCompItem _PoolReader;

		// Token: 0x04000476 RID: 1142
		private ModLoader.LoaderTask<int, List<ModComp.CompFile>> _CustomerReader;

		// Token: 0x04000477 RID: 1143
		private ModComp.CompProject _PageReader;

		// Token: 0x04000478 RID: 1144
		private string m_InterceptorReader;

		// Token: 0x04000479 RID: 1145
		private ModComp.CompModLoaderType containerReader;

		// Token: 0x0400047A RID: 1146
		private string paramsReader;

		// Token: 0x0400047B RID: 1147
		private bool dispatcherReader;

		// Token: 0x0400047C RID: 1148
		private bool _ProcessReader;

		// Token: 0x0400047D RID: 1149
		public static string parameterReader = null;

		// Token: 0x0400047E RID: 1150
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyScrollViewer m_RecordReader;

		// Token: 0x0400047F RID: 1151
		[CompilerGenerated]
		[AccessedThroughProperty("CardIntro")]
		private MyCard _ServiceReader;

		// Token: 0x04000480 RID: 1152
		[CompilerGenerated]
		[AccessedThroughProperty("PanIntro")]
		private StackPanel _InvocationReader;

		// Token: 0x04000481 RID: 1153
		[AccessedThroughProperty("BtnIntroWeb")]
		[CompilerGenerated]
		private MyButton m_ProxyReader;

		// Token: 0x04000482 RID: 1154
		[AccessedThroughProperty("BtnIntroWiki")]
		[CompilerGenerated]
		private MyButton messageReader;

		// Token: 0x04000483 RID: 1155
		[AccessedThroughProperty("BtnIntroCopy")]
		[CompilerGenerated]
		private MyButton creatorReader;

		// Token: 0x04000484 RID: 1156
		[CompilerGenerated]
		[AccessedThroughProperty("PanMain")]
		private StackPanel _InitializerReader;

		// Token: 0x04000485 RID: 1157
		[CompilerGenerated]
		[AccessedThroughProperty("CardFilter")]
		private MyCard singletonReader;

		// Token: 0x04000486 RID: 1158
		[AccessedThroughProperty("PanFilter")]
		[CompilerGenerated]
		private StackPanel m_RegReader;

		// Token: 0x04000487 RID: 1159
		[CompilerGenerated]
		[AccessedThroughProperty("PanResults")]
		private StackPanel m_ProductReader;

		// Token: 0x04000488 RID: 1160
		[CompilerGenerated]
		[AccessedThroughProperty("PanLoad")]
		private MyCard m_ListenerReader;

		// Token: 0x04000489 RID: 1161
		[AccessedThroughProperty("Load")]
		[CompilerGenerated]
		private MyLoading m_CollectionReader;

		// Token: 0x0400048A RID: 1162
		private bool m_VisitorReader;

		// Token: 0x020000E5 RID: 229
		private class CardSorter : IComparer<string>
		{
			// Token: 0x06000870 RID: 2160 RVA: 0x00045428 File Offset: 0x00043628
			public int Compare(string x, string y)
			{
				checked
				{
					int result;
					if (Operators.CompareString(x, y, false) == 0)
					{
						result = 0;
					}
					else if (Operators.CompareString(x, this.Topmost, false) == 0)
					{
						result = -1;
					}
					else if (Operators.CompareString(y, this.Topmost, false) == 0)
					{
						result = 1;
					}
					else
					{
						bool flag = x.EndsWithF("版本", false);
						bool flag2 = y.EndsWithF("版本", false);
						if (flag && flag2)
						{
							result = x.CompareTo(y);
						}
						else if (flag)
						{
							result = 1;
						}
						else if (flag2)
						{
							result = -1;
						}
						else
						{
							int num = 0 - ModMinecraft.VersionSortInteger(x.Replace(x.BeforeFirst(" ", false) + " ", ""), y.Replace(y.BeforeFirst(" ", false) + " ", ""));
							if (num != 0)
							{
								result = num;
							}
							else
							{
								result = 0 - ModMinecraft.VersionSortInteger(x, y);
							}
						}
					}
					return result;
				}
			}

			// Token: 0x06000871 RID: 2161 RVA: 0x00006557 File Offset: 0x00004757
			public CardSorter(string Topmost = "")
			{
				this.Topmost = "";
				this.Topmost = (Topmost ?? "");
			}

			// Token: 0x0400048B RID: 1163
			public string Topmost;
		}
	}
}
