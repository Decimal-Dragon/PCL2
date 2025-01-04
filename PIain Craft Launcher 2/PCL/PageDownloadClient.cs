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
using Newtonsoft.Json.Linq;

namespace PCL
{
	// Token: 0x020001A7 RID: 423
	[DesignerGenerated]
	public class PageDownloadClient : MyPageRight, IComponentConnector
	{
		// Token: 0x0600116B RID: 4459 RVA: 0x0000A8B5 File Offset: 0x00008AB5
		public PageDownloadClient()
		{
			base.Initialized += delegate(object sender, EventArgs e)
			{
				this.LoaderInit();
			};
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.Init();
			};
			this.InitializeComponent();
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0007CCB8 File Offset: 0x0007AEB8
		private void LoaderInit()
		{
			base.PageLoaderInit(this.Load, this.PanLoad, this.PanBack, null, ModDownload.accountTests, delegate(ModLoader.LoaderBase a0)
			{
				this.Load_OnFinish();
			}, null, true);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0000A8E8 File Offset: 0x00008AE8
		private void Init()
		{
			this.PanBack.ScrollToHome();
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0007CCF4 File Offset: 0x0007AEF4
		private void Load_OnFinish()
		{
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
														goto IL_3CF;
													}
												}
											}
											else if (Operators.CompareString(text2, "2.0", false) == 0)
											{
												goto IL_3CF;
											}
										}
										else if (num != 2812350463U)
										{
											if (num == 2819146544U)
											{
												if (Operators.CompareString(text2, "15w14a", false) == 0)
												{
													goto IL_3CF;
												}
											}
										}
										else if (Operators.CompareString(text2, "23w13a_or_b", false) == 0)
										{
											goto IL_3CF;
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
													goto IL_36E;
												}
												if (Operators.CompareString(text2, "20w14infinite", false) != 0)
												{
													goto IL_36E;
												}
											}
											else if (Operators.CompareString(text2, "20w14∞", false) != 0)
											{
												goto IL_36E;
											}
											text = "愚人节版";
											jobject["id"] = "20w14∞";
											jobject["type"] = "special";
											jobject.Add("lore", ModMinecraft.GetMcFoolName((string)jobject["id"]));
											goto IL_41C;
										}
										if (num != 3766348906U)
										{
											if (num != 3830073160U)
											{
												if (num == 4285466550U)
												{
													if (Operators.CompareString(text2, "24w14potato", false) == 0)
													{
														goto IL_3CF;
													}
												}
											}
											else if (Operators.CompareString(text2, "22w13oneblockatatime", false) == 0)
											{
												goto IL_3CF;
											}
										}
										else if (Operators.CompareString(text2, "3d shareware v1.34", false) == 0)
										{
											goto IL_3CF;
										}
									}
									IL_36E:
									DateTime dateTime = Extensions.Value<DateTime>(jobject["releaseTime"]).ToUniversalTime().AddHours(2.0);
									if (dateTime.Month == 4 && dateTime.Day == 1)
									{
										text = "愚人节版";
										jobject["type"] = "special";
										goto IL_41C;
									}
									goto IL_41C;
									IL_3CF:
									text = "愚人节版";
									jobject["type"] = "special";
									jobject.Add("lore", ModMinecraft.GetMcFoolName((string)jobject["id"]));
								}
							}
							else
							{
								text = "正式版";
							}
							IL_41C:
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
						dictionary[Enumerable.ElementAtOrDefault<string>(dictionary.Keys, i)] = Enumerable.ElementAtOrDefault<List<JObject>>(dictionary.Values, i).Sort((PageDownloadClient._Closure$__.$I3-0 == null) ? (PageDownloadClient._Closure$__.$I3-0 = ((JObject a, JObject b) => DateTime.Compare(Extensions.Value<DateTime>(a["releaseTime"]), Extensions.Value<DateTime>(b["releaseTime"])) > 0)) : PageDownloadClient._Closure$__.$I3-0);
					}
					this.PanMain.Children.Clear();
					MyCard myCard = new MyCard();
					myCard.Title = "最新版本";
					myCard.Margin = new Thickness(0.0, 0.0, 0.0, 15.0);
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
					MyCard.StackInstall(ref element, 2, "");
					myCard2.Children.Add(element);
					this.PanMain.Children.Add(myCard2);
					try
					{
						foreach (KeyValuePair<string, List<JObject>> keyValuePair in dictionary)
						{
							if (Enumerable.Any<JObject>(keyValuePair.Value))
							{
								MyCard myCard3 = new MyCard();
								myCard3.Title = keyValuePair.Key + " (" + Conversions.ToString(keyValuePair.Value.Count) + ")";
								myCard3.Margin = new Thickness(0.0, 0.0, 0.0, 15.0);
								myCard3.CreateParser(2);
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
								this.PanMain.Children.Add(myCard4);
							}
						}
					}
					finally
					{
						Dictionary<string, List<JObject>>.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "可视化 MC 版本列表出错", ModBase.LogLevel.Feedback, "出现错误");
				}
			}
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0000A8F5 File Offset: 0x00008AF5
		public void DownloadStart(MyListItem sender, object e)
		{
			ModDownloadLib.McDownloadClient(ModNet.NetPreDownloadBehaviour.HintWhileExists, sender.Title, NewLateBinding.LateIndexGet(sender.Tag, new object[]
			{
				"url"
			}, null).ToString());
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x0000A923 File Offset: 0x00008B23
		// (set) Token: 0x06001171 RID: 4465 RVA: 0x0000A92B File Offset: 0x00008B2B
		internal virtual MyScrollViewer PanBack { get; set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x0000A934 File Offset: 0x00008B34
		// (set) Token: 0x06001173 RID: 4467 RVA: 0x0000A93C File Offset: 0x00008B3C
		internal virtual StackPanel PanMain { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x0000A945 File Offset: 0x00008B45
		// (set) Token: 0x06001175 RID: 4469 RVA: 0x0000A94D File Offset: 0x00008B4D
		internal virtual MyCard PanLoad { get; set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x0000A956 File Offset: 0x00008B56
		// (set) Token: 0x06001177 RID: 4471 RVA: 0x0000A95E File Offset: 0x00008B5E
		internal virtual MyLoading Load { get; set; }

		// Token: 0x06001178 RID: 4472 RVA: 0x0007D558 File Offset: 0x0007B758
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._ConfigurationThread)
			{
				this._ConfigurationThread = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagedownload/pagedownloadclient.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x0007D588 File Offset: 0x0007B788
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
				this.PanLoad = (MyCard)target;
				return;
			}
			if (connectionId == 4)
			{
				this.Load = (MyLoading)target;
				return;
			}
			this._ConfigurationThread = true;
		}

		// Token: 0x04000917 RID: 2327
		[AccessedThroughProperty("PanBack")]
		[CompilerGenerated]
		private MyScrollViewer templateThread;

		// Token: 0x04000918 RID: 2328
		[AccessedThroughProperty("PanMain")]
		[CompilerGenerated]
		private StackPanel methodThread;

		// Token: 0x04000919 RID: 2329
		[AccessedThroughProperty("PanLoad")]
		[CompilerGenerated]
		private MyCard _TaskThread;

		// Token: 0x0400091A RID: 2330
		[AccessedThroughProperty("Load")]
		[CompilerGenerated]
		private MyLoading m_ConsumerThread;

		// Token: 0x0400091B RID: 2331
		private bool _ConfigurationThread;
	}
}
