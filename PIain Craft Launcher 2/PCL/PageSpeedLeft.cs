using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020001BA RID: 442
	[DesignerGenerated]
	public class PageSpeedLeft : MyPageLeft, IComponentConnector
	{
		// Token: 0x06001489 RID: 5257 RVA: 0x0000BA2E File Offset: 0x00009C2E
		public PageSpeedLeft()
		{
			base.Loaded += this.Page_Loaded;
			this._FieldComposer = false;
			this.m_ReaderComposer = new Dictionary<string, MyCard>();
			this.InitializeComponent();
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x00089ADC File Offset: 0x00087CDC
		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			this.Watcher();
			this.TryReturnToHome();
			if (!this._FieldComposer)
			{
				this._FieldComposer = true;
				DispatcherTimer dispatcherTimer = new DispatcherTimer();
				dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 300);
				dispatcherTimer.Tick += delegate(object sender, EventArgs e)
				{
					this.Watcher();
				};
				dispatcherTimer.Start();
				if (!ModBase._TokenRepository)
				{
					base.RowDefinitions[12].Height = new GridLength(0.0);
					base.RowDefinitions[13].Height = new GridLength(0.0);
					base.RowDefinitions[14].Height = new GridLength(0.0);
					base.RowDefinitions[15].Height = new GridLength(0.0);
				}
			}
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x00089BC4 File Offset: 0x00087DC4
		private void Watcher()
		{
			if (ModMain._ProcessIterator._MethodIterator == FormMain.PageType.DownloadManager)
			{
				try
				{
					if (!Enumerable.Any<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar))
					{
						this.LabProgress.Text = "100 %";
						this.LabSpeed.Text = "0 B/s";
						this.LabFile.Text = "0";
						this.LabThread.Text = "0 / " + Conversions.ToString(ModNet.m_StateTests);
					}
					else
					{
						double loaderTaskbarProgress = ModLoader.LoaderTaskbarProgress;
						string text = Conversions.ToString(Math.Floor(loaderTaskbarProgress * 100.0)) + "." + ModBase.StrFill(Conversions.ToString(Math.Floor((loaderTaskbarProgress * 100.0 - Math.Floor(loaderTaskbarProgress * 100.0)) * 100.0)), "0", 2) + " %";
						this.LabProgress.Text = ((loaderTaskbarProgress > 0.999999) ? "100 %" : text);
						this.LabSpeed.Text = ModBase.GetString(ModNet.tokenTests.m_BridgeTest) + "/s";
						this.LabFile.Text = Conversions.ToString((ModNet.tokenTests._CollectionTest < 0) ? "0*" : ModNet.tokenTests._CollectionTest);
						this.LabThread.Text = Conversions.ToString(ModNet._ConfigurationTests) + " / " + Conversions.ToString(ModNet.m_StateTests);
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "下载管理左栏监视出错", ModBase.LogLevel.Feedback, "出现错误");
				}
				if (ModMain.m_CreatorIterator != null && ModMain.m_CreatorIterator.PanMain != null)
				{
					try
					{
						try
						{
							foreach (ModLoader.LoaderBase loader in Enumerable.ToList<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar))
							{
								this.TaskRefresh(loader);
							}
						}
						finally
						{
							List<ModLoader.LoaderBase>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
					catch (Exception ex2)
					{
						ModBase.Log(ex2, "下载管理右栏监视出错", ModBase.LogLevel.Feedback, "出现错误");
					}
				}
			}
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x00089E34 File Offset: 0x00088034
		public void TaskRefresh(ModLoader.LoaderBase Loader)
		{
			PageSpeedLeft._Closure$__6-1 CS$<>8__locals1 = new PageSpeedLeft._Closure$__6-1(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Me = this;
			CS$<>8__locals1.$VB$Local_Loader = Loader;
			if (CS$<>8__locals1.$VB$Local_Loader != null && CS$<>8__locals1.$VB$Local_Loader.Show)
			{
				try
				{
					List<ModLoader.LoaderBase> list = (List<ModLoader.LoaderBase>)NewLateBinding.LateGet(CS$<>8__locals1.$VB$Local_Loader, null, "GetLoaderList", new object[0], null, null, null);
					if (this.m_ReaderComposer.ContainsKey(CS$<>8__locals1.$VB$Local_Loader.Name))
					{
						Grid grid = this.m_ReaderComposer[CS$<>8__locals1.$VB$Local_Loader.Name];
						double num = CS$<>8__locals1.$VB$Local_Loader.Progress + (double)CS$<>8__locals1.$VB$Local_Loader.State;
						if (ModBase.Val(RuntimeHelpers.GetObjectValue(grid.Tag)) == num)
						{
							return;
						}
						grid.Tag = num;
						if (grid.Children.Count <= 3)
						{
							ModBase.Log("[Watcher] 元素不足的卡片：" + CS$<>8__locals1.$VB$Local_Loader.Name, ModBase.LogLevel.Debug, "出现错误");
							return;
						}
						grid = (Grid)grid.Children[3];
						checked
						{
							try
							{
								switch (CS$<>8__locals1.$VB$Local_Loader.State)
								{
								case ModBase.LoadState.Waiting:
								case ModBase.LoadState.Loading:
									try
									{
										if (grid.Children.Count < list.Count * 2)
										{
											ModBase.Log(string.Format("[Watcher] 刷新下载管理卡片 {0} 失败：卡片中仅有 {1} 个子项，要求至少有 {2} 个子项", CS$<>8__locals1.$VB$Local_Loader.Name, grid.Children.Count, list.Count * 2), ModBase.LogLevel.Debug, "出现错误");
											goto IL_480;
										}
										int num2 = 0;
										try
										{
											foreach (ModLoader.LoaderBase loaderBase in list)
											{
												switch (loaderBase.State)
												{
												case ModBase.LoadState.Waiting:
													if (Operators.ConditionalCompareObjectNotEqual(((FrameworkElement)grid.Children[num2 * 2]).Tag, "Waiting", false))
													{
														grid.Children.RemoveAt(num2 * 2);
														grid.Children.Insert(num2 * 2, (UIElement)ModBase.GetObjectFromXML("<Path xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:local=\"clr-namespace:PCL;assembly=Plain Craft Launcher 2\" Stretch=\"Uniform\" Tag=\"Waiting\" Data=\"F1 M5,0 a5,5 360 1 0 0,0.0001 m15,0 a5,5 360 1 0 0,0.0001 m15,0 a5,5 360 1 0 0,0.0001 Z\" Width=\"18\" HorizontalAlignment=\"Center\" Grid.Column=\"0\" Grid.Row=\"" + Conversions.ToString(num2) + "\" Fill=\"{DynamicResource ColorBrush3}\" Margin=\"0,7,0,0\" VerticalAlignment=\"Top\" Height=\"6\"/>"));
													}
													break;
												case ModBase.LoadState.Loading:
													if (Operators.ConditionalCompareObjectNotEqual(((FrameworkElement)grid.Children[num2 * 2]).Tag, "Loading", false))
													{
														grid.Children.RemoveAt(num2 * 2);
														grid.Children.Insert(num2 * 2, (UIElement)ModBase.GetObjectFromXML(string.Concat(new string[]
														{
															"<TextBlock xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:local=\"clr-namespace:PCL;assembly=Plain Craft Launcher 2\" Text=\"",
															Conversions.ToString(Math.Floor(unchecked(loaderBase.Progress * 100.0))),
															"%\" Tag=\"Loading\" HorizontalAlignment=\"Center\" Grid.Column=\"0\" Grid.Row=\"",
															Conversions.ToString(num2),
															"\" Foreground=\"{DynamicResource ColorBrush3}\"/>"
														})));
													}
													else
													{
														((TextBlock)grid.Children[num2 * 2]).Text = Conversions.ToString(Math.Floor(unchecked(loaderBase.Progress * 100.0))) + "%";
													}
													break;
												case ModBase.LoadState.Finished:
													if (Operators.ConditionalCompareObjectNotEqual(((FrameworkElement)grid.Children[num2 * 2]).Tag, "Finished", false))
													{
														grid.Children.RemoveAt(num2 * 2);
														grid.Children.Insert(num2 * 2, (UIElement)ModBase.GetObjectFromXML("<Path xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:local=\"clr-namespace:PCL;assembly=Plain Craft Launcher 2\" Stretch=\"Uniform\" Tag=\"Finished\" Data=\"F1 M 23.7501,33.25L 34.8334,44.3333L 52.2499,22.1668L 56.9999,26.9168L 34.8334,53.8333L 19.0001,38L 23.7501,33.25 Z\" Height=\"16\" Width=\"15\" HorizontalAlignment=\"Center\" Grid.Column=\"0\" Grid.Row=\"" + Conversions.ToString(num2) + "\" Fill=\"{DynamicResource ColorBrush3}\" Margin=\"0,3,0,0\" VerticalAlignment=\"Top\"/>"));
													}
													break;
												}
												num2++;
											}
										}
										finally
										{
											List<ModLoader.LoaderBase>.Enumerator enumerator;
											((IDisposable)enumerator).Dispose();
										}
										goto IL_480;
									}
									catch (Exception ex)
									{
										ModBase.Log(ex, string.Format("刷新下载管理卡片 {0} 失败", CS$<>8__locals1.$VB$Local_Loader.Name), ModBase.LogLevel.Feedback, "出现错误");
										goto IL_480;
									}
									break;
								case ModBase.LoadState.Finished:
									break;
								case ModBase.LoadState.Failed:
								{
									grid.RowDefinitions.Clear();
									grid.Children.Clear();
									grid.Children.Add((UIElement)ModBase.GetObjectFromXML("<Path xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" Stretch=\"Uniform\" Tag=\"Failed\" Data=\"F1 M2.5,0 L0,2.5 7.5,10 0,17.5 2.5,20 10,12.5 17.5,20 20,17.5 12.5,10 20,2.5 17.5,0 10,7.5 2.5,0Z\" Height=\"15\" Width=\"15\" HorizontalAlignment=\"Center\" Grid.Column=\"0\" Grid.Row=\"0\" Fill=\"{DynamicResource ColorBrush3}\" Margin=\"0,1,0,0\" VerticalAlignment=\"Top\"/>"));
									TextBlock textBlock = (TextBlock)ModBase.GetObjectFromXML("<TextBlock xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" TextWrapping=\"Wrap\" HorizontalAlignment=\"Left\" ToolTip=\"单击复制错误详情\" Grid.Column=\"1\" Grid.Row=\"0\" Margin=\"0,0,0,5\" />");
									textBlock.Text = ModBase.GetExceptionDetail(CS$<>8__locals1.$VB$Local_Loader.Error, false);
									textBlock.MouseLeftButtonDown += ((PageSpeedLeft._Closure$__.$IR6-2 == null) ? (PageSpeedLeft._Closure$__.$IR6-2 = delegate(object sender, MouseButtonEventArgs e)
									{
										((PageSpeedLeft._Closure$__.$I6-0 == null) ? (PageSpeedLeft._Closure$__.$I6-0 = delegate(TextBlock sender, EventArgs e)
										{
											ModBase.ClipboardSet(sender.Text, false);
											ModMain.Hint("已复制错误详情！", ModMain.HintType.Finish, true);
										}) : PageSpeedLeft._Closure$__.$I6-0)((TextBlock)sender, e);
									}) : PageSpeedLeft._Closure$__.$IR6-2);
									grid.Children.Add(textBlock);
									goto IL_480;
								}
								default:
									goto IL_480;
								}
								ModAnimation.AniDispose((MyCard)grid.Parent, true, delegate(object a0)
								{
									this.TryReturnToHome();
								});
								IL_480:
								goto IL_8A5;
							}
							catch (Exception ex2)
							{
								ModBase.Log(ex2, "更新下载管理显示失败（" + CS$<>8__locals1.$VB$Local_Loader.State.ToString() + "）", ModBase.LogLevel.Feedback, "出现错误");
								goto IL_8A5;
							}
						}
					}
					if (CS$<>8__locals1.$VB$Local_Loader.State != ModBase.LoadState.Aborted && CS$<>8__locals1.$VB$Local_Loader.State != ModBase.LoadState.Finished)
					{
						try
						{
							PageSpeedLeft._Closure$__6-0 CS$<>8__locals2 = new PageSpeedLeft._Closure$__6-0(CS$<>8__locals2);
							CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2 = CS$<>8__locals1;
							string text = string.Concat(new string[]
							{
								"\r\n                        <local:MyCard xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:local=\"clr-namespace:PCL;assembly=Plain Craft Launcher 2\"\r\n                            Tag=\"",
								Conversions.ToString(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Loader.Progress + (double)CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Loader.State),
								"\" Title=\"",
								ModBase.smethod_3(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Loader.Name),
								"\" Margin=\"0,0,0,15\">\r\n                            <Grid Margin=\"14,40,15,10\">\r\n                                <Grid.ColumnDefinitions>\r\n                                    <ColumnDefinition Width=\"50\"/>\r\n                                    <ColumnDefinition/>\r\n                                </Grid.ColumnDefinitions>\r\n                                <Grid.RowDefinitions>"
							});
							try
							{
								foreach (ModLoader.LoaderBase loaderBase2 in list)
								{
									text += "<RowDefinition Height=\"26\"/>";
								}
							}
							finally
							{
								List<ModLoader.LoaderBase>.Enumerator enumerator2;
								((IDisposable)enumerator2).Dispose();
							}
							text += "</Grid.RowDefinitions>";
							int num3 = 0;
							try
							{
								foreach (ModLoader.LoaderBase loaderBase3 in list)
								{
									switch (loaderBase3.State)
									{
									case ModBase.LoadState.Waiting:
										text = text + "<Path Stretch=\"Uniform\" Tag=\"Waiting\" Data=\"F1 M5,0 a5,5 360 1 0 0,0.0001 m15,0 a5,5 360 1 0 0,0.0001 m15,0 a5,5 360 1 0 0,0.0001 Z\" Width=\"18\" HorizontalAlignment=\"Center\" Grid.Column=\"0\" Grid.Row=\"" + Conversions.ToString(num3) + "\" Fill=\"{DynamicResource ColorBrush3}\" Margin=\"0,7,0,0\" VerticalAlignment=\"Top\" Height=\"6\"/>";
										break;
									case ModBase.LoadState.Loading:
										text = string.Concat(new string[]
										{
											text,
											"<TextBlock Text=\"",
											Conversions.ToString(Math.Floor(loaderBase3.Progress * 100.0)),
											"%\" Tag=\"Loading\" HorizontalAlignment=\"Center\" Grid.Column=\"0\" Grid.Row=\"",
											Conversions.ToString(num3),
											"\" Foreground=\"{DynamicResource ColorBrush3}\" />"
										});
										break;
									case ModBase.LoadState.Finished:
										text = text + "<Path Stretch=\"Uniform\" Tag=\"Finished\" Data=\"F1 M 23.7501,33.25L 34.8334,44.3333L 52.2499,22.1668L 56.9999,26.9168L 34.8334,53.8333L 19.0001,38L 23.7501,33.25 Z\" Height=\"16\" Width=\"15\" HorizontalAlignment=\"Center\" Grid.Column=\"0\" Grid.Row=\"" + Conversions.ToString(num3) + "\" Fill=\"{DynamicResource ColorBrush3}\" Margin=\"0,3,0,0\" VerticalAlignment=\"Top\"/>";
										break;
									default:
										text = text + "<Path Stretch=\"Uniform\" Tag=\"Failed\" Data=\"F1 M2.5,0 L0,2.5 7.5,10 0,17.5 2.5,20 10,12.5 17.5,20 20,17.5 12.5,10 20,2.5 17.5,0 10,7.5 2.5,0Z\" Height=\"15\" Width=\"15\" HorizontalAlignment=\"Center\" Grid.Column=\"0\" Grid.Row=\"" + Conversions.ToString(num3) + "\" Fill=\"{DynamicResource ColorBrush3}\" Margin=\"0,1,0,0\" VerticalAlignment=\"Top\"/>";
										break;
									}
									text = string.Concat(new string[]
									{
										text,
										"<TextBlock Text=\"",
										ModBase.smethod_3(loaderBase3.Name),
										"\" HorizontalAlignment=\"Left\" Grid.Column=\"1\" Grid.Row=\"",
										Conversions.ToString(num3),
										"\"/>"
									});
									checked
									{
										num3++;
									}
								}
							}
							finally
							{
								List<ModLoader.LoaderBase>.Enumerator enumerator3;
								((IDisposable)enumerator3).Dispose();
							}
							text += "</Grid></local:MyCard>";
							try
							{
								CS$<>8__locals2.$VB$Local_Card = (MyCard)ModBase.GetObjectFromXML(text);
							}
							catch (Exception ex3)
							{
								ModBase.Log(ex3, "新建下载管理卡片失败", ModBase.LogLevel.Debug, "出现错误");
								ModBase.Log("出错的卡片内容：\r\n" + text, ModBase.LogLevel.Normal, "出现错误");
								throw;
							}
							ModMain.m_CreatorIterator.PanMain.Children.Insert(0, CS$<>8__locals2.$VB$Local_Card);
							this.m_ReaderComposer.Add(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Loader.Name, CS$<>8__locals2.$VB$Local_Card);
							ModBase.Log(string.Format("[Watcher] 新建下载管理卡片：{0}", CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Loader.Name), ModBase.LogLevel.Normal, "出现错误");
							MyIconButton myIconButton = new MyIconButton
							{
								Name = "BtnCancel",
								Logo = "F1 M2,0 L0,2 8,10 0,18 2,20 10,12 18,20 20,18 12,10 20,2 18,0 10,8 2,0Z",
								Height = 20.0,
								Margin = new Thickness(0.0, 10.0, 10.0, 0.0),
								LogoScale = 1.1,
								HorizontalAlignment = HorizontalAlignment.Right,
								VerticalAlignment = VerticalAlignment.Top
							};
							CS$<>8__locals2.$VB$Local_Card.Children.Add(myIconButton);
							myIconButton.Click += delegate(object sender, EventArgs e)
							{
								base._Lambda$__1((MyIconButton)sender, e);
							};
							if (CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Loader.State == ModBase.LoadState.Failed)
							{
								CS$<>8__locals2.$VB$Local_Card.Tag = null;
								this.TaskRefresh(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Loader);
							}
						}
						catch (Exception ex4)
						{
							ModBase.Log(ex4, "添加下载管理卡片失败", ModBase.LogLevel.Feedback, "出现错误");
						}
					}
					IL_8A5:;
				}
				catch (Exception ex5)
				{
					ModBase.Log(ex5, "刷新下载管理显示失败", ModBase.LogLevel.Feedback, "出现错误");
				}
			}
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0008A7D0 File Offset: 0x000889D0
		public void TaskRemove(object Loader)
		{
			if (this.m_ReaderComposer.ContainsKey(Conversions.ToString(NewLateBinding.LateGet(Loader, null, "Name", new object[0], null, null, null))))
			{
				ModBase.RunInUiWait(delegate()
				{
					Grid element = this.m_ReaderComposer[Conversions.ToString(NewLateBinding.LateGet(Loader, null, "Name", new object[0], null, null, null))];
					ModMain.m_CreatorIterator.PanMain.Children.Remove(element);
					this.m_ReaderComposer.Remove(Conversions.ToString(NewLateBinding.LateGet(Loader, null, "Name", new object[0], null, null, null)));
					ModBase.Log(string.Format("[Watcher] 移除下载管理卡片：{0}", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(Loader, null, "Name", new object[0], null, null, null))), ModBase.LogLevel.Normal, "出现错误");
				});
			}
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0000BA61 File Offset: 0x00009C61
		private void TryReturnToHome()
		{
			if (ModMain.m_CreatorIterator.PanMain.Children.Count == 0 && ModMain._ProcessIterator._MethodIterator == FormMain.PageType.DownloadManager)
			{
				ModMain._ProcessIterator.PageBack();
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x0000BA9A File Offset: 0x00009C9A
		// (set) Token: 0x06001490 RID: 5264 RVA: 0x0000BAA2 File Offset: 0x00009CA2
		internal virtual TextBlock LabProgress { get; set; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0000BAAB File Offset: 0x00009CAB
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x0000BAB3 File Offset: 0x00009CB3
		internal virtual TextBlock LabSpeed { get; set; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x0000BABC File Offset: 0x00009CBC
		// (set) Token: 0x06001494 RID: 5268 RVA: 0x0000BAC4 File Offset: 0x00009CC4
		internal virtual TextBlock LabFile { get; set; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x0000BACD File Offset: 0x00009CCD
		// (set) Token: 0x06001496 RID: 5270 RVA: 0x0000BAD5 File Offset: 0x00009CD5
		internal virtual TextBlock LabThread { get; set; }

		// Token: 0x06001497 RID: 5271 RVA: 0x0008A830 File Offset: 0x00088A30
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.threadComposer)
			{
				this.threadComposer = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagespeedleft.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0008A860 File Offset: 0x00088A60
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.LabProgress = (TextBlock)target;
				return;
			}
			if (connectionId == 2)
			{
				this.LabSpeed = (TextBlock)target;
				return;
			}
			if (connectionId == 3)
			{
				this.LabFile = (TextBlock)target;
				return;
			}
			if (connectionId == 4)
			{
				this.LabThread = (TextBlock)target;
				return;
			}
			this.threadComposer = true;
		}

		// Token: 0x04000A8A RID: 2698
		private bool _FieldComposer;

		// Token: 0x04000A8B RID: 2699
		private readonly Dictionary<string, MyCard> m_ReaderComposer;

		// Token: 0x04000A8C RID: 2700
		[AccessedThroughProperty("LabProgress")]
		[CompilerGenerated]
		private TextBlock m_ClientComposer;

		// Token: 0x04000A8D RID: 2701
		[CompilerGenerated]
		[AccessedThroughProperty("LabSpeed")]
		private TextBlock configComposer;

		// Token: 0x04000A8E RID: 2702
		[AccessedThroughProperty("LabFile")]
		[CompilerGenerated]
		private TextBlock testsComposer;

		// Token: 0x04000A8F RID: 2703
		[AccessedThroughProperty("LabThread")]
		[CompilerGenerated]
		private TextBlock m_MapperComposer;

		// Token: 0x04000A90 RID: 2704
		private bool threadComposer;
	}
}
