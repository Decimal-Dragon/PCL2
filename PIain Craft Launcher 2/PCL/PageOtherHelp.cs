using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020001A1 RID: 417
	[DesignerGenerated]
	public class PageOtherHelp : MyPageRight, IRefreshable, IComponentConnector
	{
		// Token: 0x06001105 RID: 4357 RVA: 0x0000A5DF File Offset: 0x000087DF
		public PageOtherHelp()
		{
			base.Loaded += this.PageOther_Loaded;
			base.Initialized += this.PageOther_Inited;
			this.InitializeComponent();
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0000A612 File Offset: 0x00008812
		private void PageOther_Loaded(object sender, RoutedEventArgs e)
		{
			this.PanBack.ScrollToHome();
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0007B78C File Offset: 0x0007998C
		private void PageOther_Inited(object sender, EventArgs e)
		{
			base.PageLoaderInit(this.Load, this.PanLoad, this.PanBack, null, ModMain._RepositoryRepository, delegate(ModLoader.LoaderBase a0)
			{
				this.HelpListLoad((ModLoader.LoaderTask<int, List<ModMain.HelpEntry>>)a0);
			}, null, true);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0007B7C8 File Offset: 0x000799C8
		private void HelpListLoad(ModLoader.LoaderTask<int, List<ModMain.HelpEntry>> Loader)
		{
			try
			{
				this.PanList.Children.Clear();
				this.PanBack.ScrollToHome();
				List<ModMain.HelpEntry> output = Loader.Output;
				List<string> list = new List<string>();
				try
				{
					foreach (ModMain.HelpEntry helpEntry in output)
					{
						if ((ModBase.Val("50") != 50.0 || helpEntry.m_BroadcasterError) && (ModBase.Val("50") == 50.0 || helpEntry.fieldError))
						{
							try
							{
								foreach (string item in helpEntry._StrategyMap)
								{
									if (!list.Contains(item))
									{
										list.Add(item);
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
				}
				finally
				{
					List<ModMain.HelpEntry>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				if (list.Contains("指南"))
				{
					list.Remove("指南");
					list.Insert(0, "指南");
				}
				try
				{
					foreach (string text in list)
					{
						List<ModMain.HelpEntry> list2 = new List<ModMain.HelpEntry>();
						try
						{
							foreach (ModMain.HelpEntry helpEntry2 in output)
							{
								if ((ModBase.Val("50") != 50.0 || helpEntry2.m_BroadcasterError) && (ModBase.Val("50") == 50.0 || helpEntry2.fieldError) && helpEntry2._StrategyMap.Contains(text))
								{
									list2.Add(helpEntry2);
								}
							}
						}
						finally
						{
							List<ModMain.HelpEntry>.Enumerator enumerator4;
							((IDisposable)enumerator4).Dispose();
						}
						MyCard myCard = new MyCard();
						myCard.Title = text;
						myCard.Margin = new Thickness(0.0, 0.0, 0.0, 15.0);
						myCard.CreateParser(11);
						MyCard myCard2 = myCard;
						StackPanel stackPanel = new StackPanel
						{
							Margin = new Thickness(20.0, 40.0, 18.0, 0.0),
							VerticalAlignment = VerticalAlignment.Top,
							RenderTransform = new TranslateTransform(0.0, 0.0),
							Tag = list2
						};
						myCard2.Children.Add(stackPanel);
						myCard2._Stub = stackPanel;
						if (Operators.CompareString(text, "指南", false) == 0)
						{
							MyCard.StackInstall(ref stackPanel, 11, "指南");
						}
						else
						{
							myCard2.IsSwaped = true;
						}
						this.PanList.Children.Add(myCard2);
					}
				}
				finally
				{
					List<string>.Enumerator enumerator3;
					((IDisposable)enumerator3).Dispose();
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "加载帮助列表 UI 失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0007BB24 File Offset: 0x00079D24
		public static void OnItemClick(ModMain.HelpEntry Entry)
		{
			try
			{
				if (Entry._ReaderError)
				{
					ModEvent.TryStartEvent(Entry._ClientError, Entry.m_ConfigError);
				}
				else
				{
					PageOtherHelp.EnterHelpPage(Entry);
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "处理帮助项目点击时发生意外错误", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0000A61F File Offset: 0x0000881F
		public static void EnterHelpPage(string Location)
		{
			ModBase.RunInThread(delegate
			{
				if (ModMain._RepositoryRepository.State != ModBase.LoadState.Finished)
				{
					ModMain._RepositoryRepository.WaitForExit(ModBase.GetUuid(), null, false);
				}
				ModMain.HelpEntry Entry = new ModMain.HelpEntry(Location);
				ModBase.RunInUi(delegate()
				{
					PageOtherHelpDetail pageOtherHelpDetail = new PageOtherHelpDetail();
					if (pageOtherHelpDetail.Init(Entry))
					{
						ModMain._ProcessIterator.PageChange(new FormMain.PageStackData
						{
							initializerMap = FormMain.PageType.HelpDetail,
							m_SingletonMap = new object[]
							{
								Entry,
								pageOtherHelpDetail
							}
						}, FormMain.PageSubType.Default);
						return;
					}
					ModBase.Log("[Help] 已取消进入帮助项目，这一般是由于 xaml 初始化失败，且用户在弹窗中手动放弃", ModBase.LogLevel.Debug, "出现错误");
				}, false);
			});
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0000A63D File Offset: 0x0000883D
		public static void EnterHelpPage(ModMain.HelpEntry Entry)
		{
			Action $I1;
			ModBase.RunInThread(delegate
			{
				if (ModMain._RepositoryRepository.State != ModBase.LoadState.Finished)
				{
					ModMain._RepositoryRepository.WaitForExit(ModBase.GetUuid(), null, false);
				}
				ModBase.RunInUi(($I1 == null) ? ($I1 = delegate()
				{
					PageOtherHelpDetail pageOtherHelpDetail = new PageOtherHelpDetail();
					if (pageOtherHelpDetail.Init(Entry))
					{
						ModMain._ProcessIterator.PageChange(new FormMain.PageStackData
						{
							initializerMap = FormMain.PageType.HelpDetail,
							m_SingletonMap = new object[]
							{
								Entry,
								pageOtherHelpDetail
							}
						}, FormMain.PageSubType.Default);
						return;
					}
					ModBase.Log("[Help] 已取消进入帮助项目，这一般是由于 xaml 初始化失败，且用户在弹窗中手动放弃", ModBase.LogLevel.Debug, "出现错误");
				}) : $I1, false);
			});
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0007BB84 File Offset: 0x00079D84
		public static PageOtherHelpDetail GetHelpPage(string Location)
		{
			if (ModMain._RepositoryRepository.State != ModBase.LoadState.Finished)
			{
				ModMain._RepositoryRepository.WaitForExit(ModBase.GetUuid(), null, false);
			}
			PageOtherHelpDetail pageOtherHelpDetail = new PageOtherHelpDetail();
			if (!pageOtherHelpDetail.Init(new ModMain.HelpEntry(Location)))
			{
				throw new Exception("已取消进入帮助项目，这一般是由于 xaml 初始化失败，且用户在弹窗中手动放弃");
			}
			return pageOtherHelpDetail;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0007BBD8 File Offset: 0x00079DD8
		public void SearchRun()
		{
			if (string.IsNullOrWhiteSpace(this.SearchBox.Text))
			{
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaOpacity(this.PanSearch, -this.PanSearch.Opacity, 100, 0, null, false),
					ModAnimation.AaCode(delegate
					{
						this.PanSearch.Height = 0.0;
						this.PanSearch.Visibility = Visibility.Collapsed;
						this.PanList.Visibility = Visibility.Visible;
					}, 0, true),
					ModAnimation.AaOpacity(this.PanList, 1.0 - this.PanList.Opacity, 150, 30, null, false)
				}, "FrmOtherHelp Search Switch", false);
				return;
			}
			List<ModBase.SearchEntry<ModMain.HelpEntry>> list = new List<ModBase.SearchEntry<ModMain.HelpEntry>>();
			try
			{
				foreach (ModMain.HelpEntry helpEntry in ModMain._RepositoryRepository.Output)
				{
					if (helpEntry._ParserError && (ModBase.Val("50") != 50.0 || helpEntry.m_BroadcasterError) && helpEntry._ParserError && (ModBase.Val("50") == 50.0 || helpEntry.fieldError))
					{
						list.Add(new ModBase.SearchEntry<ModMain.HelpEntry>
						{
							m_DicError = helpEntry,
							helperError = new List<KeyValuePair<string, double>>
							{
								new KeyValuePair<string, double>(helpEntry.Title, 1.0),
								new KeyValuePair<string, double>(helpEntry._PublisherMap, 0.5),
								new KeyValuePair<string, double>(helpEntry._DefinitionMap, 1.5)
							}
						});
					}
				}
			}
			finally
			{
				List<ModMain.HelpEntry>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			List<ModBase.SearchEntry<ModMain.HelpEntry>> list2 = ModBase.Search<ModMain.HelpEntry>(list, this.SearchBox.Text, 5, 0.08);
			this.PanSearchList.Children.Clear();
			if (!Enumerable.Any<ModBase.SearchEntry<ModMain.HelpEntry>>(list2))
			{
				this.PanSearch.Title = "无搜索结果";
				this.PanSearchList.Visibility = Visibility.Collapsed;
			}
			else
			{
				this.PanSearch.Title = "搜索结果";
				try
				{
					foreach (ModBase.SearchEntry<ModMain.HelpEntry> searchEntry in list2)
					{
						MyListItem myListItem = searchEntry.m_DicError.ToListItem();
						if (ModBase._TokenRepository)
						{
							myListItem.Info = string.Concat(new string[]
							{
								searchEntry.indexerError ? "完全匹配，" : "",
								"相似度：",
								Conversions.ToString(Math.Round(searchEntry.m_IssuerError, 3)),
								"，",
								myListItem.Info
							});
						}
						this.PanSearchList.Children.Add(myListItem);
					}
				}
				finally
				{
					List<ModBase.SearchEntry<ModMain.HelpEntry>>.Enumerator enumerator2;
					((IDisposable)enumerator2).Dispose();
				}
				this.PanSearchList.Visibility = Visibility.Visible;
			}
			ModAnimation.AniStart(new ModAnimation.AniData[]
			{
				ModAnimation.AaOpacity(this.PanList, -this.PanList.Opacity, 100, 0, null, false),
				ModAnimation.AaCode(delegate
				{
					this.PanList.Visibility = Visibility.Collapsed;
					this.PanSearch.Visibility = Visibility.Visible;
					this.PanSearch.TriggerForceResize();
				}, 0, true),
				ModAnimation.AaOpacity(this.PanSearch, 1.0 - this.PanSearch.Opacity, 150, 30, null, false)
			}, "FrmOtherHelp Search Switch", false);
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0000A65B File Offset: 0x0000885B
		public void Refresh()
		{
			PageOtherLeft.RefreshHelp();
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x0000A662 File Offset: 0x00008862
		// (set) Token: 0x06001110 RID: 4368 RVA: 0x0000A66A File Offset: 0x0000886A
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x0000A673 File Offset: 0x00008873
		// (set) Token: 0x06001112 RID: 4370 RVA: 0x0000A67B File Offset: 0x0000887B
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x0000A684 File Offset: 0x00008884
		// (set) Token: 0x06001114 RID: 4372 RVA: 0x0007BF34 File Offset: 0x0007A134
		internal virtual MySearchBox SearchBox
		{
			[CompilerGenerated]
			get
			{
				return this.m_SpecificationThread;
			}
			[CompilerGenerated]
			set
			{
				MySearchBox.TextChangedEventHandler obj = delegate(object sender, EventArgs e)
				{
					this.SearchRun();
				};
				MySearchBox specificationThread = this.m_SpecificationThread;
				if (specificationThread != null)
				{
					specificationThread.RegisterField(obj);
				}
				this.m_SpecificationThread = value;
				specificationThread = this.m_SpecificationThread;
				if (specificationThread != null)
				{
					specificationThread.ChangeField(obj);
				}
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x0000A68C File Offset: 0x0000888C
		// (set) Token: 0x06001116 RID: 4374 RVA: 0x0000A694 File Offset: 0x00008894
		internal virtual MyCard PanSearch { get; set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x0000A69D File Offset: 0x0000889D
		// (set) Token: 0x06001118 RID: 4376 RVA: 0x0000A6A5 File Offset: 0x000088A5
		internal virtual StackPanel PanSearchList { get; set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x0000A6AE File Offset: 0x000088AE
		// (set) Token: 0x0600111A RID: 4378 RVA: 0x0000A6B6 File Offset: 0x000088B6
		internal virtual StackPanel PanList { get; set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x0000A6BF File Offset: 0x000088BF
		// (set) Token: 0x0600111C RID: 4380 RVA: 0x0000A6C7 File Offset: 0x000088C7
		internal virtual MyCard PanLoad { get; set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x0000A6D0 File Offset: 0x000088D0
		// (set) Token: 0x0600111E RID: 4382 RVA: 0x0000A6D8 File Offset: 0x000088D8
		internal virtual MyLoading Load { get; set; }

		// Token: 0x0600111F RID: 4383 RVA: 0x0007BF78 File Offset: 0x0007A178
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._IndexerThread)
			{
				this._IndexerThread = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageother/pageotherhelp.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0007BFA8 File Offset: 0x0007A1A8
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
				this.PanMain = (StackPanel)target;
				return;
			}
			if (connectionId == 3)
			{
				this.SearchBox = (MySearchBox)target;
				return;
			}
			if (connectionId == 4)
			{
				this.PanSearch = (MyCard)target;
				return;
			}
			if (connectionId == 5)
			{
				this.PanSearchList = (StackPanel)target;
				return;
			}
			if (connectionId == 6)
			{
				this.PanList = (StackPanel)target;
				return;
			}
			if (connectionId == 7)
			{
				this.PanLoad = (MyCard)target;
				return;
			}
			if (connectionId == 8)
			{
				this.Load = (MyLoading)target;
				return;
			}
			this._IndexerThread = true;
		}

		// Token: 0x040008F8 RID: 2296
		[CompilerGenerated]
		[AccessedThroughProperty("PanBack")]
		private MyScrollViewer _ErrorThread;

		// Token: 0x040008F9 RID: 2297
		[CompilerGenerated]
		[AccessedThroughProperty("PanMain")]
		private StackPanel _ContextThread;

		// Token: 0x040008FA RID: 2298
		[AccessedThroughProperty("SearchBox")]
		[CompilerGenerated]
		private MySearchBox m_SpecificationThread;

		// Token: 0x040008FB RID: 2299
		[AccessedThroughProperty("PanSearch")]
		[CompilerGenerated]
		private MyCard _MockThread;

		// Token: 0x040008FC RID: 2300
		[AccessedThroughProperty("PanSearchList")]
		[CompilerGenerated]
		private StackPanel _RequestThread;

		// Token: 0x040008FD RID: 2301
		[CompilerGenerated]
		[AccessedThroughProperty("PanList")]
		private StackPanel dicThread;

		// Token: 0x040008FE RID: 2302
		[AccessedThroughProperty("PanLoad")]
		[CompilerGenerated]
		private MyCard helperThread;

		// Token: 0x040008FF RID: 2303
		[AccessedThroughProperty("Load")]
		[CompilerGenerated]
		private MyLoading _IssuerThread;

		// Token: 0x04000900 RID: 2304
		private bool _IndexerThread;
	}
}
