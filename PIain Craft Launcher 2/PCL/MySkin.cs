using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PCL
{
	// Token: 0x02000185 RID: 389
	[DesignerGenerated]
	public class MySkin : Grid, IComponentConnector
	{
		// Token: 0x06000F85 RID: 3973 RVA: 0x00073338 File Offset: 0x00071538
		public MySkin()
		{
			base.MouseEnter += this.PanSkin_MouseEnter;
			base.MouseLeave += this.PanSkin_MouseLeave;
			base.MouseLeftButtonDown += this.PanSkin_MouseLeftButtonDown;
			base.MouseLeftButtonUp += this.PanSkin_MouseLeftButtonUp;
			this.m_TemplateMapper = false;
			this._MethodMapper = false;
			this.InitializeComponent();
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000F86 RID: 3974 RVA: 0x000733A8 File Offset: 0x000715A8
		// (remove) Token: 0x06000F87 RID: 3975 RVA: 0x000733E0 File Offset: 0x000715E0
		public event MySkin.ClickEventHandler Click
		{
			[CompilerGenerated]
			add
			{
				MySkin.ClickEventHandler clickEventHandler = this.m_InstanceMapper;
				MySkin.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MySkin.ClickEventHandler value2 = (MySkin.ClickEventHandler)Delegate.Combine(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MySkin.ClickEventHandler>(ref this.m_InstanceMapper, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				MySkin.ClickEventHandler clickEventHandler = this.m_InstanceMapper;
				MySkin.ClickEventHandler clickEventHandler2;
				do
				{
					clickEventHandler2 = clickEventHandler;
					MySkin.ClickEventHandler value2 = (MySkin.ClickEventHandler)Delegate.Remove(clickEventHandler2, value);
					clickEventHandler = Interlocked.CompareExchange<MySkin.ClickEventHandler>(ref this.m_InstanceMapper, value2, clickEventHandler2);
				}
				while (clickEventHandler != clickEventHandler2);
			}
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0000986A File Offset: 0x00007A6A
		public string FlushClient()
		{
			return this.stateMapper;
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x00009872 File Offset: 0x00007A72
		public void PatchClient(string value)
		{
			this.stateMapper = value;
			base.ToolTip = ((Operators.CompareString(this.stateMapper, "", false) == 0) ? "加载中" : "点击更换皮肤（右键查看更多选项）");
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x000098A0 File Offset: 0x00007AA0
		private void PanSkin_MouseEnter(object sender, MouseEventArgs e)
		{
			ModAnimation.AniStart(ModAnimation.AaOpacity(this.ShadowSkin, 0.8 - this.ShadowSkin.Opacity, 200, 100, null, false), "Skin Shadow", false);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x00073418 File Offset: 0x00071618
		private void PanSkin_MouseLeave(object sender, MouseEventArgs e)
		{
			ModAnimation.AniStart(ModAnimation.AaOpacity(this.ShadowSkin, 0.2 - this.ShadowSkin.Opacity, 200, 0, null, false), "Skin Shadow", false);
			this.m_TemplateMapper = false;
			ModAnimation.AniStart(ModAnimation.AaScaleTransform(this, 1.0 - ((ScaleTransform)base.RenderTransform).ScaleX, 60, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false), "Skin Scale", false);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x000098D6 File Offset: 0x00007AD6
		private void PanSkin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.m_TemplateMapper = true;
			ModAnimation.AniStart(ModAnimation.AaScaleTransform(this, 0.9 - ((ScaleTransform)base.RenderTransform).ScaleX, 60, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false), "Skin Scale", false);
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00073494 File Offset: 0x00071694
		private void PanSkin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			ModAnimation.AniStart(ModAnimation.AaScaleTransform(this, 1.0 - ((ScaleTransform)base.RenderTransform).ScaleX, 60, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false), "Skin Scale", false);
			if (this.m_TemplateMapper)
			{
				this.m_TemplateMapper = false;
				MySkin.ClickEventHandler instanceMapper = this.m_InstanceMapper;
				if (instanceMapper != null)
				{
					instanceMapper(RuntimeHelpers.GetObjectValue(sender), e);
				}
			}
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00009914 File Offset: 0x00007B14
		public void BtnSkinSave_Click()
		{
			MySkin.Save(this.callbackMapper);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x000734FC File Offset: 0x000716FC
		public static void Save(ModLoader.LoaderTask<ModBase.EqualableList<string>, string> Loader)
		{
			string output = Loader.Output;
			if (Loader.State != ModBase.LoadState.Finished)
			{
				ModMain.Hint("皮肤正在获取中，请稍候！", ModMain.HintType.Critical, true);
				if (Loader.State != ModBase.LoadState.Loading)
				{
					Loader.Start(null, false);
					return;
				}
			}
			else
			{
				try
				{
					string text = ModBase.SelectAs("选取保存皮肤的位置", ModBase.GetFileNameFromPath(output), "皮肤图片文件(*.png)|*.png", null);
					if (text.Contains("\\"))
					{
						File.Delete(text);
						if (output.StartsWith(ModBase.m_SerializerRepository))
						{
							new MyBitmap(output).Save(text);
						}
						else
						{
							ModBase.CopyFile(output, text);
						}
						ModMain.Hint("皮肤保存成功！", ModMain.HintType.Finish, true);
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "保存皮肤失败", ModBase.LogLevel.Hint, "出现错误");
				}
			}
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00009921 File Offset: 0x00007B21
		private void BtnSkinSave_Checked(MyMenuItem sender, RoutedEventArgs e)
		{
			sender.IsEnabled = (Operators.CompareString(this.FlushClient(), "", false) == 0);
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x000735C4 File Offset: 0x000717C4
		public void Load()
		{
			checked
			{
				try
				{
					this.PatchClient(this.callbackMapper.Output);
					if (string.IsNullOrEmpty(this.FlushClient()))
					{
						throw new Exception("皮肤加载器 " + this.callbackMapper.Name + " 没有输出");
					}
					if (!this.FlushClient().StartsWith(ModBase.m_SerializerRepository) && !File.Exists(this.FlushClient()))
					{
						throw new FileNotFoundException("皮肤文件未找到", this.FlushClient());
					}
					MyBitmap myBitmap;
					try
					{
						myBitmap = new MyBitmap(this.FlushClient());
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, string.Format("皮肤文件已损坏：{0}", this.FlushClient()), ModBase.LogLevel.Hint, "出现错误");
						File.Delete(this.FlushClient());
						return;
					}
					this.ImgBack.Tag = this.FlushClient();
					int num = (int)Math.Round((double)myBitmap._ContainerIterator.Width / 64.0);
					if (myBitmap._ContainerIterator.Width < 32 || myBitmap._ContainerIterator.Height < 32)
					{
						this.ImgFore.Source = null;
						this.ImgBack.Source = null;
						throw new Exception("图片大小不足，长为 " + Conversions.ToString(myBitmap._ContainerIterator.Height) + "，宽为 " + Conversions.ToString(myBitmap._ContainerIterator.Width));
					}
					if (myBitmap._ContainerIterator.Width >= 64 && myBitmap._ContainerIterator.Height >= 32)
					{
						if (myBitmap._ContainerIterator.GetPixel(1, 1).A != 0 && myBitmap._ContainerIterator.GetPixel(myBitmap._ContainerIterator.Width - 1, myBitmap._ContainerIterator.Height - 1).A != 0 && myBitmap._ContainerIterator.GetPixel(myBitmap._ContainerIterator.Width - 2, (int)Math.Round(unchecked((double)myBitmap._ContainerIterator.Height / 2.0 - 2.0))).A != 0 && (!(myBitmap._ContainerIterator.GetPixel(1, 1) != myBitmap._ContainerIterator.GetPixel(num * 41, num * 9)) || !(myBitmap._ContainerIterator.GetPixel(myBitmap._ContainerIterator.Width - 1, myBitmap._ContainerIterator.Height - 1) != myBitmap._ContainerIterator.GetPixel(num * 41, num * 9)) || !(myBitmap._ContainerIterator.GetPixel(myBitmap._ContainerIterator.Width - 2, (int)Math.Round(unchecked((double)myBitmap._ContainerIterator.Height / 2.0 - 2.0))) != myBitmap._ContainerIterator.GetPixel(num * 41, num * 9))))
						{
							this.ImgFore.Source = null;
						}
						else
						{
							this.ImgFore.Source = myBitmap.Clip(num * 40, num * 8, num * 8, num * 8);
						}
					}
					else
					{
						this.ImgFore.Source = null;
					}
					this.ImgBack.Source = myBitmap.Clip(num * 8, num * 8, num * 8, num * 8);
					ModBase.Log("[Skin] 载入头像成功：" + this.callbackMapper.Name, ModBase.LogLevel.Normal, "出现错误");
				}
				catch (Exception ex2)
				{
					ModBase.Log(ex2, string.Concat(new string[]
					{
						"载入头像失败（",
						this.FlushClient() ?? "null",
						",",
						this.callbackMapper.Name,
						"）"
					}), ModBase.LogLevel.Hint, "出现错误");
				}
			}
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0000993D File Offset: 0x00007B3D
		public void Clear()
		{
			this.PatchClient("");
			this.ImgFore.Source = null;
			this.ImgBack.Source = null;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00009962 File Offset: 0x00007B62
		public void RefreshClick()
		{
			MySkin.RefreshCache(this.callbackMapper);
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000739BC File Offset: 0x00071BBC
		public static void RefreshCache(ModLoader.LoaderTask<ModBase.EqualableList<string>, string> sender = null)
		{
			MySkin._Closure$__22-0 CS$<>8__locals1 = new MySkin._Closure$__22-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_sender = sender;
			bool flag = false;
			try
			{
				List<ModLoader.LoaderTask<ModBase.EqualableList<string>, string>>.Enumerator enumerator = PageLaunchLeft.m_ListMapper.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.State == ModBase.LoadState.Loading)
					{
						flag = true;
						break;
					}
				}
			}
			finally
			{
				List<ModLoader.LoaderTask<ModBase.EqualableList<string>, string>>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			if (ModMain.recordIterator != null && flag)
			{
				ModMain.Hint("有正在获取中的皮肤，请稍后再试！", ModMain.HintType.Info, true);
				return;
			}
			ModBase.RunInThread(checked(delegate
			{
				try
				{
					ModMain.Hint("正在刷新头像……", ModMain.HintType.Info, true);
					ModBase.Log("[Skin] 正在清空皮肤缓存", ModBase.LogLevel.Normal, "出现错误");
					if (Directory.Exists(ModBase.m_DecoratorRepository + "Cache\\Skin"))
					{
						ModBase.DeleteDirectory(ModBase.m_DecoratorRepository + "Cache\\Skin", false);
					}
					if (Directory.Exists(ModBase.m_DecoratorRepository + "Cache\\Uuid"))
					{
						ModBase.DeleteDirectory(ModBase.m_DecoratorRepository + "Cache\\Uuid", false);
					}
					ModBase.IniClearCache(ModBase.m_DecoratorRepository + "Cache\\Skin\\IndexMs.ini");
					ModBase.IniClearCache(ModBase.m_DecoratorRepository + "Cache\\Skin\\IndexNide.ini");
					ModBase.IniClearCache(ModBase.m_DecoratorRepository + "Cache\\Skin\\IndexAuth.ini");
					ModBase.IniClearCache(ModBase.m_DecoratorRepository + "Cache\\Uuid\\Mojang.ini");
					ModLoader.LoaderTask<ModBase.EqualableList<string>, string>[] array2;
					if (CS$<>8__locals1.$VB$Local_sender == null)
					{
						ModLoader.LoaderTask<ModBase.EqualableList<string>, string>[] array = new ModLoader.LoaderTask<ModBase.EqualableList<string>, string>[2];
						array[0] = PageLaunchLeft.infoMapper;
						array2 = array;
						array[1] = PageLaunchLeft.annotationMapper;
					}
					else
					{
						(array2 = new ModLoader.LoaderTask<ModBase.EqualableList<string>, string>[1])[0] = CS$<>8__locals1.$VB$Local_sender;
					}
					ModLoader.LoaderTask<ModBase.EqualableList<string>, string>[] array3 = array2;
					for (int i = 0; i < array3.Length; i++)
					{
						array3[i].WaitForExit(null, null, true);
					}
					ModMain.Hint("已刷新头像！", ModMain.HintType.Finish, true);
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "刷新皮肤缓存失败", ModBase.LogLevel.Msgbox, "出现错误");
				}
			}));
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00073A50 File Offset: 0x00071C50
		public static void ReloadCache(string SkinAddress)
		{
			MySkin._Closure$__23-0 CS$<>8__locals1 = new MySkin._Closure$__23-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_SkinAddress = SkinAddress;
			ModBase.RunInThread(checked(delegate
			{
				try
				{
					ModBase.WriteIni(ModBase.m_DecoratorRepository + "Cache\\Skin\\IndexMs.ini", Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheMsV2Uuid", null)), CS$<>8__locals1.$VB$Local_SkinAddress);
					ModBase.Log(string.Format("[Skin] 已写入皮肤地址缓存 {0} -> {1}", RuntimeHelpers.GetObjectValue(ModBase.m_IdentifierRepository.Get("CacheMsV2Uuid", null)), CS$<>8__locals1.$VB$Local_SkinAddress), ModBase.LogLevel.Normal, "出现错误");
					ModLoader.LoaderTask<ModBase.EqualableList<string>, string>[] array = new ModLoader.LoaderTask<ModBase.EqualableList<string>, string>[]
					{
						PageLaunchLeft.annotationMapper,
						PageLaunchLeft.infoMapper
					};
					for (int i = 0; i < array.Length; i++)
					{
						array[i].WaitForExit(null, null, true);
					}
					ModMain.Hint("更改皮肤成功！", ModMain.HintType.Finish, true);
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "更改正版皮肤后刷新皮肤失败", ModBase.LogLevel.Feedback, "出现错误");
				}
			}));
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0000996F File Offset: 0x00007B6F
		// (set) Token: 0x06000F97 RID: 3991 RVA: 0x0000997F File Offset: 0x00007B7F
		public bool HasCape
		{
			get
			{
				return this.BtnSkinCape.Visibility == Visibility.Collapsed;
			}
			set
			{
				if (value)
				{
					this.BtnSkinCape.Visibility = Visibility.Visible;
					return;
				}
				this.BtnSkinCape.Visibility = Visibility.Collapsed;
			}
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00073A7C File Offset: 0x00071C7C
		public void BtnSkinCape_Click()
		{
			if (this._MethodMapper)
			{
				ModMain.Hint("正在更改披风中，请稍候！", ModMain.HintType.Info, true);
				return;
			}
			if (ModLaunch.m_ContainerTests.State == ModBase.LoadState.Failed)
			{
				ModMain.Hint("登录失败，无法更改披风！", ModMain.HintType.Critical, true);
				return;
			}
			ModMain.Hint("正在获取披风列表，请稍候……", ModMain.HintType.Info, true);
			this._MethodMapper = true;
			ModBase.RunInNewThread(delegate
			{
				try
				{
					MySkin._Closure$__28-0 CS$<>8__locals1 = new MySkin._Closure$__28-0(CS$<>8__locals1);
					if (ModLaunch.m_ContainerTests.State != ModBase.LoadState.Finished)
					{
						ModLaunch.m_ContainerTests.WaitForExit(PageLoginMsSkin.GetLoginData(), null, false);
					}
					if (ModLaunch.m_ContainerTests.State != ModBase.LoadState.Finished)
					{
						ModMain.Hint("登录失败，无法更改披风！", ModMain.HintType.Critical, true);
					}
					else
					{
						string stateMap = ModLaunch.m_ContainerTests.Output._StateMap;
						CS$<>8__locals1.$VB$Local_SkinData = (JObject)ModBase.GetJson(ModLaunch.m_ContainerTests.Output._TemplateMap);
						CS$<>8__locals1.$VB$Local_SelId = null;
						ModBase.RunInUiWait(delegate()
						{
							try
							{
								Dictionary<string, string> dictionary = new Dictionary<string, string>
								{
									{
										"Migrator",
										"迁移者披风"
									},
									{
										"MapMaker",
										"Realms 地图制作者披风"
									},
									{
										"Moderator",
										"Mojira 管理员披风"
									},
									{
										"Translator-Chinese",
										"Crowdin 中文翻译者披风"
									},
									{
										"Translator",
										"Crowdin 翻译者披风"
									},
									{
										"Cobalt",
										"Cobalt 披风"
									},
									{
										"Vanilla",
										"原版披风"
									},
									{
										"Minecon2011",
										"Minecon 2011 参与者披风"
									},
									{
										"Minecon2012",
										"Minecon 2012 参与者披风"
									},
									{
										"Minecon2013",
										"Minecon 2013 参与者披风"
									},
									{
										"Minecon2015",
										"Minecon 2015 参与者披风"
									},
									{
										"Minecon2016",
										"Minecon 2016 参与者披风"
									},
									{
										"Cherry Blossom",
										"樱花披风"
									},
									{
										"15th Anniversary",
										"15 周年纪念披风"
									},
									{
										"Purple Heart",
										"紫色心形披风"
									},
									{
										"Follower's",
										"追随者披风"
									},
									{
										"MCC 15th Year",
										"MCC 15 周年披风"
									},
									{
										"Minecraft Experience",
										"村民救援披风"
									}
								};
								List<IMyRadio> list = new List<IMyRadio>
								{
									new MyRadioBox
									{
										Text = "无披风"
									}
								};
								try
								{
									foreach (JToken jtoken in CS$<>8__locals1.$VB$Local_SkinData["capes"])
									{
										string text2 = jtoken["alias"].ToString();
										if (dictionary.ContainsKey(text2))
										{
											text2 = dictionary[text2];
										}
										list.Add(new MyRadioBox
										{
											Text = text2
										});
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
								CS$<>8__locals1.$VB$Local_SelId = ModMain.MyMsgBoxSelect(list, "选择披风", "确定", "取消", false);
							}
							catch (Exception ex2)
							{
								ModBase.Log(ex2, "获取玩家皮肤列表失败", ModBase.LogLevel.Feedback, "出现错误");
							}
						});
						if (CS$<>8__locals1.$VB$Local_SelId != null)
						{
							string url = "https://api.minecraftservices.com/minecraft/profile/capes/active";
							int? $VB$Local_SelId = CS$<>8__locals1.$VB$Local_SelId;
							string method = (($VB$Local_SelId != null) ? new bool?($VB$Local_SelId.GetValueOrDefault() == 0) : null).GetValueOrDefault() ? "DELETE" : "PUT";
							$VB$Local_SelId = CS$<>8__locals1.$VB$Local_SelId;
							string text = ModNet.NetRequestRetry(url, method, (($VB$Local_SelId != null) ? new bool?($VB$Local_SelId.GetValueOrDefault() == 0) : null).GetValueOrDefault() ? "" : new JObject(new JProperty("capeId", CS$<>8__locals1.$VB$Local_SkinData["capes"][checked(CS$<>8__locals1.$VB$Local_SelId - 1)]["id"])).ToString(0, new JsonConverter[0]), "application/json", true, new Dictionary<string, string>
							{
								{
									"Authorization",
									"Bearer " + stateMap
								}
							});
							if (text.Contains("\"errorMessage\""))
							{
								ModMain.Hint(Conversions.ToString(Operators.ConcatenateObject("更改披风失败：", NewLateBinding.LateIndexGet(ModBase.GetJson(text), new object[]
								{
									"errorMessage"
								}, null))), ModMain.HintType.Critical, true);
							}
							else
							{
								ModMain.Hint("更改披风成功！", ModMain.HintType.Finish, true);
							}
						}
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "更改披风失败", ModBase.LogLevel.Hint, "出现错误");
				}
				finally
				{
					this._MethodMapper = false;
				}
			}, "Cape Change", ThreadPriority.Normal);
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0000999D File Offset: 0x00007B9D
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x000099A5 File Offset: 0x00007BA5
		internal virtual DropShadowEffect ShadowSkin { get; set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x000099AE File Offset: 0x00007BAE
		// (set) Token: 0x06000F9C RID: 3996 RVA: 0x00073AE4 File Offset: 0x00071CE4
		internal virtual MyMenuItem BtnSkinSave
		{
			[CompilerGenerated]
			get
			{
				return this._ConsumerMapper;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = delegate(object sender, RoutedEventArgs e)
				{
					this.BtnSkinSave_Click();
				};
				RoutedEventHandler value3 = delegate(object sender, RoutedEventArgs e)
				{
					this.BtnSkinSave_Checked((MyMenuItem)sender, e);
				};
				MyMenuItem consumerMapper = this._ConsumerMapper;
				if (consumerMapper != null)
				{
					consumerMapper.Click -= value2;
					consumerMapper.Checked -= value3;
				}
				this._ConsumerMapper = value;
				consumerMapper = this._ConsumerMapper;
				if (consumerMapper != null)
				{
					consumerMapper.Click += value2;
					consumerMapper.Checked += value3;
				}
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x000099B6 File Offset: 0x00007BB6
		// (set) Token: 0x06000F9E RID: 3998 RVA: 0x00073B44 File Offset: 0x00071D44
		internal virtual MyMenuItem BtnSkinRefresh
		{
			[CompilerGenerated]
			get
			{
				return this.m_ConfigurationMapper;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = delegate(object sender, RoutedEventArgs e)
				{
					this.RefreshClick();
				};
				MyMenuItem configurationMapper = this.m_ConfigurationMapper;
				if (configurationMapper != null)
				{
					configurationMapper.Click -= value2;
				}
				this.m_ConfigurationMapper = value;
				configurationMapper = this.m_ConfigurationMapper;
				if (configurationMapper != null)
				{
					configurationMapper.Click += value2;
				}
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x000099BE File Offset: 0x00007BBE
		// (set) Token: 0x06000FA0 RID: 4000 RVA: 0x00073B88 File Offset: 0x00071D88
		internal virtual MyMenuItem BtnSkinCape
		{
			[CompilerGenerated]
			get
			{
				return this.m_GetterMapper;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = delegate(object sender, RoutedEventArgs e)
				{
					this.BtnSkinCape_Click();
				};
				MyMenuItem getterMapper = this.m_GetterMapper;
				if (getterMapper != null)
				{
					getterMapper.Click -= value2;
				}
				this.m_GetterMapper = value;
				getterMapper = this.m_GetterMapper;
				if (getterMapper != null)
				{
					getterMapper.Click += value2;
				}
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x000099C6 File Offset: 0x00007BC6
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x000099CE File Offset: 0x00007BCE
		internal virtual Image ImgBack { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x000099D7 File Offset: 0x00007BD7
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x000099DF File Offset: 0x00007BDF
		internal virtual Image ImgFore { get; set; }

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00073BCC File Offset: 0x00071DCC
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.writerMapper)
			{
				this.writerMapper = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/myskin.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00073BFC File Offset: 0x00071DFC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.ShadowSkin = (DropShadowEffect)target;
				return;
			}
			if (connectionId == 2)
			{
				this.BtnSkinSave = (MyMenuItem)target;
				return;
			}
			if (connectionId == 3)
			{
				this.BtnSkinRefresh = (MyMenuItem)target;
				return;
			}
			if (connectionId == 4)
			{
				this.BtnSkinCape = (MyMenuItem)target;
				return;
			}
			if (connectionId == 5)
			{
				this.ImgBack = (Image)target;
				return;
			}
			if (connectionId == 6)
			{
				this.ImgFore = (Image)target;
				return;
			}
			this.writerMapper = true;
		}

		// Token: 0x0400085D RID: 2141
		[CompilerGenerated]
		private MySkin.ClickEventHandler m_InstanceMapper;

		// Token: 0x0400085E RID: 2142
		private string stateMapper;

		// Token: 0x0400085F RID: 2143
		public ModLoader.LoaderTask<ModBase.EqualableList<string>, string> callbackMapper;

		// Token: 0x04000860 RID: 2144
		private bool m_TemplateMapper;

		// Token: 0x04000861 RID: 2145
		private bool _MethodMapper;

		// Token: 0x04000862 RID: 2146
		[AccessedThroughProperty("ShadowSkin")]
		[CompilerGenerated]
		private DropShadowEffect m_TaskMapper;

		// Token: 0x04000863 RID: 2147
		[CompilerGenerated]
		[AccessedThroughProperty("BtnSkinSave")]
		private MyMenuItem _ConsumerMapper;

		// Token: 0x04000864 RID: 2148
		[AccessedThroughProperty("BtnSkinRefresh")]
		[CompilerGenerated]
		private MyMenuItem m_ConfigurationMapper;

		// Token: 0x04000865 RID: 2149
		[CompilerGenerated]
		[AccessedThroughProperty("BtnSkinCape")]
		private MyMenuItem m_GetterMapper;

		// Token: 0x04000866 RID: 2150
		[CompilerGenerated]
		[AccessedThroughProperty("ImgBack")]
		private Image tokenMapper;

		// Token: 0x04000867 RID: 2151
		[CompilerGenerated]
		[AccessedThroughProperty("ImgFore")]
		private Image m_ExpressionMapper;

		// Token: 0x04000868 RID: 2152
		private bool writerMapper;

		// Token: 0x02000186 RID: 390
		// (Invoke) Token: 0x06000FB1 RID: 4017
		public delegate void ClickEventHandler(object sender, MouseButtonEventArgs e);
	}
}
