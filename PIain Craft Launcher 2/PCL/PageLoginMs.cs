using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace PCL
{
	// Token: 0x020000F4 RID: 244
	[DesignerGenerated]
	public class PageLoginMs : StackPanel, IComponentConnector
	{
		// Token: 0x06000956 RID: 2390 RVA: 0x00006BD7 File Offset: 0x00004DD7
		public PageLoginMs()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00048504 File Offset: 0x00046704
		public void Reload(bool KeepInput)
		{
			int selectedIndex = this.ComboAccounts.SelectedIndex;
			this.ComboAccounts.Items.Clear();
			this.ComboAccounts.Items.Add(new MyComboBoxItem
			{
				Content = "添加新账号"
			});
			try
			{
				JObject jobject = (JObject)ModBase.GetJson(Conversions.ToString(ModBase.m_IdentifierRepository.Get("LoginMsJson", null)));
				try
				{
					foreach (KeyValuePair<string, JToken> keyValuePair in jobject)
					{
						MyListItem myListItem = (MyListItem)((DataTemplate)base.FindResource("ComboBoxItemTemplateWithDelete")).LoadContent();
						myListItem.Tag = keyValuePair.Value.ToString();
						myListItem.Title = keyValuePair.Key;
						Enumerable.ElementAtOrDefault<MyIconButton>(myListItem.Buttons, 0).Tag = keyValuePair.Key;
						this.ComboAccounts.Items.Add(myListItem);
					}
				}
				finally
				{
					IEnumerator<KeyValuePair<string, JToken>> enumerator;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, string.Format("微软登录信息出错，登录信息已被重置（{0}）", RuntimeHelpers.GetObjectValue(ModBase.m_IdentifierRepository.Get("LoginMsJson", null))), ModBase.LogLevel.Hint, "出现错误");
				ModBase.m_IdentifierRepository.Set("LoginMsJson", "{}", false, null);
			}
			this.ComboAccounts.SelectedIndex = (KeepInput ? Math.Max(0, selectedIndex) : 0);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00048684 File Offset: 0x00046884
		private void ComboAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ModAnimation.CalcParser() == 0 && this.ComboAccounts.SelectedItem != null && this.ComboAccounts.ResetParser() != null)
			{
				if (this.ComboAccounts.SelectedItem is MyListItem)
				{
					this.ComboAccounts.ResetParser().Content = ((MyListItem)this.ComboAccounts.SelectedItem).Title;
					return;
				}
				if (this.ComboAccounts.SelectedItem is MyComboBoxItem)
				{
					this.ComboAccounts.ResetParser().Content = RuntimeHelpers.GetObjectValue(((MyComboBoxItem)this.ComboAccounts.SelectedItem).Content);
				}
			}
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0004872C File Offset: 0x0004692C
		public static ModLaunch.McLoginMs GetLoginData()
		{
			ModLaunch.McLoginMs result;
			if (ModMain.m_ClientRepository == null)
			{
				result = new ModLaunch.McLoginMs
				{
					m_SystemMap = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheMsV2OAuthRefresh", null)),
					observerMap = Conversions.ToString(ModBase.m_IdentifierRepository.Get("CacheMsV2Name", null))
				};
			}
			else
			{
				ModLaunch.McLoginMs Result = null;
				ModBase.RunInUiWait(delegate()
				{
					if (ModMain.m_ClientRepository.ComboAccounts.SelectedIndex == 0)
					{
						Result = new ModLaunch.McLoginMs();
						return;
					}
					MyListItem myListItem = (MyListItem)ModMain.m_ClientRepository.ComboAccounts.SelectedItem;
					Result = new ModLaunch.McLoginMs
					{
						m_SystemMap = Conversions.ToString(myListItem.Tag),
						observerMap = myListItem.Title
					};
				});
				result = Result;
			}
			return result;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x000487A4 File Offset: 0x000469A4
		public static string IsVaild(ModLaunch.McLoginMs LoginData)
		{
			string result;
			if (Operators.CompareString(LoginData.m_SystemMap, "", false) == 0)
			{
				result = "请在登录账号后再启动游戏！";
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00006BE6 File Offset: 0x00004DE6
		public string IsVaild()
		{
			return PageLoginMs.IsVaild(PageLoginMs.GetLoginData());
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x000487D4 File Offset: 0x000469D4
		private void BtnLogin_Click(object sender, EventArgs e)
		{
			this.ComboAccounts.IsEnabled = false;
			this.BtnLogin.IsEnabled = false;
			this.BtnLogin.Text = "0%";
			ModBase.RunInNewThread(delegate
			{
				try
				{
					ModLaunch.m_ContainerTests.Start(PageLoginMs.GetLoginData(), true);
					while (ModLaunch.m_ContainerTests.State == ModBase.LoadState.Loading)
					{
						ModBase.RunInUi(delegate()
						{
							this.BtnLogin.Text = Conversions.ToString(Math.Round(ModLaunch.m_ContainerTests.Progress * 100.0)) + "%";
						}, false);
						Thread.Sleep(50);
					}
					if (ModLaunch.m_ContainerTests.State == ModBase.LoadState.Finished)
					{
						ModBase.RunInUi((PageLoginMs._Closure$__.$I6-2 == null) ? (PageLoginMs._Closure$__.$I6-2 = delegate()
						{
							ModMain.recordIterator.RefreshPage(false, true);
						}) : PageLoginMs._Closure$__.$I6-2, false);
					}
					else
					{
						if (ModLaunch.m_ContainerTests.State == ModBase.LoadState.Aborted)
						{
							throw new ThreadInterruptedException();
						}
						if (ModLaunch.m_ContainerTests.Error == null)
						{
							throw new Exception("未知错误！");
						}
						throw new Exception(ModLaunch.m_ContainerTests.Error.Message, ModLaunch.m_ContainerTests.Error);
					}
				}
				catch (ThreadInterruptedException ex)
				{
					ModMain.Hint("已取消登录！", ModMain.HintType.Info, true);
				}
				catch (Exception ex2)
				{
					if (Operators.CompareString(ex2.Message, "$$", false) != 0)
					{
						if (ex2.Message.StartsWith("$"))
						{
							ModMain.Hint(ex2.Message.TrimStart(new char[]
							{
								'$'
							}), ModMain.HintType.Critical, true);
						}
						else if (ex2 is AuthenticationException && ex2.Message.ContainsF("SSL/TLS", false))
						{
							ModBase.Log(ex2, "正版登录验证失败，请尝试在 [设置 → 启动器] 中关闭 [验证 SSL 证书] 然后再试。\r\n\r\n原始错误信息：", ModBase.LogLevel.Msgbox, "出现错误");
						}
						else
						{
							ModBase.Log(ex2, "正版登录尝试失败", ModBase.LogLevel.Msgbox, "出现错误");
						}
					}
				}
				finally
				{
					ModBase.RunInUi(delegate()
					{
						this.ComboAccounts.IsEnabled = true;
						this.BtnLogin.IsEnabled = true;
						this.BtnLogin.Text = "登录";
					}, false);
				}
			}, "Ms Login", ThreadPriority.Normal);
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00006BF2 File Offset: 0x00004DF2
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x00006BFA File Offset: 0x00004DFA
		internal virtual Grid PanEmpty { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00006C03 File Offset: 0x00004E03
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x00048824 File Offset: 0x00046A24
		internal virtual MyComboBox ComboAccounts
		{
			[CompilerGenerated]
			get
			{
				return this.m_RegistryClient;
			}
			[CompilerGenerated]
			set
			{
				SelectionChangedEventHandler value2 = new SelectionChangedEventHandler(this.ComboAccounts_SelectionChanged);
				MyComboBox registryClient = this.m_RegistryClient;
				if (registryClient != null)
				{
					registryClient.SelectionChanged -= value2;
				}
				this.m_RegistryClient = value;
				registryClient = this.m_RegistryClient;
				if (registryClient != null)
				{
					registryClient.SelectionChanged += value2;
				}
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x00006C0B File Offset: 0x00004E0B
		// (set) Token: 0x06000962 RID: 2402 RVA: 0x00048868 File Offset: 0x00046A68
		internal virtual MyButton BtnLogin
		{
			[CompilerGenerated]
			get
			{
				return this.m_RuleClient;
			}
			[CompilerGenerated]
			set
			{
				MyButton.ClickEventHandler value2 = new MyButton.ClickEventHandler(this.BtnLogin_Click);
				MyButton ruleClient = this.m_RuleClient;
				if (ruleClient != null)
				{
					ruleClient.Click -= value2;
				}
				this.m_RuleClient = value;
				ruleClient = this.m_RuleClient;
				if (ruleClient != null)
				{
					ruleClient.Click += value2;
				}
			}
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x000488AC File Offset: 0x00046AAC
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._ProccesorClient)
			{
				this._ProccesorClient = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/pageloginms.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00006C13 File Offset: 0x00004E13
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanEmpty = (Grid)target;
				return;
			}
			if (connectionId == 2)
			{
				this.ComboAccounts = (MyComboBox)target;
				return;
			}
			if (connectionId == 3)
			{
				this.BtnLogin = (MyButton)target;
				return;
			}
			this._ProccesorClient = true;
		}

		// Token: 0x040004EB RID: 1259
		[AccessedThroughProperty("PanEmpty")]
		[CompilerGenerated]
		private Grid m_WriterClient;

		// Token: 0x040004EC RID: 1260
		[CompilerGenerated]
		[AccessedThroughProperty("ComboAccounts")]
		private MyComboBox m_RegistryClient;

		// Token: 0x040004ED RID: 1261
		[AccessedThroughProperty("BtnLogin")]
		[CompilerGenerated]
		private MyButton m_RuleClient;

		// Token: 0x040004EE RID: 1262
		private bool _ProccesorClient;
	}
}
