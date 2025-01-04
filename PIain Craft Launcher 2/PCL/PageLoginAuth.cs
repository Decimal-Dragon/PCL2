using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020000F1 RID: 241
	[DesignerGenerated]
	public class PageLoginAuth : Grid, IComponentConnector
	{
		// Token: 0x0600091B RID: 2331 RVA: 0x00006A27 File Offset: 0x00004C27
		public PageLoginAuth()
		{
			base.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				this.ReloadRegisterButton();
			};
			this.m_RefClient = true;
			this.InitializeComponent();
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00047778 File Offset: 0x00045978
		public void Reload(bool KeepInput)
		{
			this.CheckRemember.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LoginRemember", null));
			if (KeepInput && !this.m_RefClient)
			{
				string text = this.ComboName.Text;
				this.ComboName.ItemsSource = (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LoginAuthEmail", null), "", false) ? null : ModBase.m_IdentifierRepository.Get("LoginAuthEmail", null).ToString().Split("¨"));
				this.ComboName.Text = text;
			}
			else if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LoginAuthEmail", null), "", false))
			{
				this.ComboName.ItemsSource = null;
			}
			else
			{
				this.ComboName.ItemsSource = ModBase.m_IdentifierRepository.Get("LoginAuthEmail", null).ToString().Split("¨");
				this.ComboName.Text = ModBase.m_IdentifierRepository.Get("LoginAuthEmail", null).ToString().BeforeFirst("¨", false);
				if (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LoginRemember", null)))
				{
					this.TextPass.Password = ModBase.m_IdentifierRepository.Get("LoginAuthPass", null).ToString().BeforeFirst("¨", false).Trim();
				}
			}
			this.m_RefClient = false;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x000478E8 File Offset: 0x00045AE8
		public static ModLaunch.McLoginServer GetLoginData()
		{
			string interpreterMap = Conversions.ToString(Operators.ConcatenateObject(Information.IsNothing(ModMinecraft.AddClient()) ? ModBase.m_IdentifierRepository.Get("CacheAuthServerServer", null) : ModBase.m_IdentifierRepository.Get("VersionServerAuthServer", ModMinecraft.AddClient()), "/authserver"));
			ModLaunch.McLoginServer result;
			if (ModMain.fieldRepository == null)
			{
				result = new ModLaunch.McLoginServer(ModLaunch.McLoginType.Auth)
				{
					m_SerializerMap = "Auth",
					interpreterMap = interpreterMap,
					m_IssuerMap = "",
					_IndexerMap = "",
					m_WatcherMap = "Authlib-Injector",
					Type = ModLaunch.McLoginType.Auth
				};
			}
			else
			{
				result = new ModLaunch.McLoginServer(ModLaunch.McLoginType.Auth)
				{
					m_SerializerMap = "Auth",
					interpreterMap = interpreterMap,
					m_IssuerMap = ModMain.fieldRepository.ComboName.Text.Replace("¨", "").Trim(),
					_IndexerMap = ModMain.fieldRepository.TextPass.Password.Replace("¨", "").Trim(),
					m_WatcherMap = "Authlib-Injector",
					Type = ModLaunch.McLoginType.Auth
				};
			}
			return result;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00047A00 File Offset: 0x00045C00
		public static string IsVaild(ModLaunch.McLoginServer LoginData)
		{
			string result;
			if (Operators.CompareString(LoginData.m_IssuerMap, "", false) == 0)
			{
				result = "账号不能为空！";
			}
			else if (Operators.CompareString(LoginData._IndexerMap, "", false) == 0)
			{
				result = "密码不能为空！";
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00006A4F File Offset: 0x00004C4F
		public string IsVaild()
		{
			return PageLoginAuth.IsVaild(PageLoginAuth.GetLoginData());
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00047A4C File Offset: 0x00045C4C
		private void ComboName_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(sender, null, "Text", new object[0], null, null, null), "", false))
			{
				this.TextPass.Password = "";
			}
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set("CacheAuthAccess", "", false, null);
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00006A5B File Offset: 0x00004C5B
		private void TextPass_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set("CacheAuthAccess", "", false, null);
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00047AA8 File Offset: 0x00045CA8
		private void ComboName_SelectionChanged(MyComboBox sender, SelectionChangedEventArgs e)
		{
			if (Conversions.ToBoolean(sender.SelectedIndex == -1 || Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("LoginRemember", null)))))
			{
				this.TextPass.Password = "";
				return;
			}
			this.TextPass.Password = ModBase.m_IdentifierRepository.Get("LoginAuthPass", null).ToString().Split("¨")[sender.SelectedIndex].Trim();
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00006A7A File Offset: 0x00004C7A
		private void CheckBoxChange(MyCheckBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.Checked, false, null);
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00006AA5 File Offset: 0x00004CA5
		private void ComboName_TextChanged()
		{
			this.BtnLink.Content = ((Operators.CompareString(this.ComboName.Text, "", false) == 0) ? "注册账号" : "找回密码");
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00047B30 File Offset: 0x00045D30
		private void Btn_Click(object sender, EventArgs e)
		{
			if (Operators.ConditionalCompareObjectEqual(this.BtnLink.Content, "注册账号", false))
			{
				ModBase.OpenWebsite(Conversions.ToString((ModMinecraft.AddClient() != null) ? ModBase.m_IdentifierRepository.Get("VersionServerAuthRegister", ModMinecraft.AddClient()) : ModBase.m_IdentifierRepository.Get("CacheAuthServerRegister", null)));
				return;
			}
			ModBase.OpenWebsite(Conversions.ToString((ModMinecraft.AddClient() != null) ? ModBase.m_IdentifierRepository.Get("VersionServerAuthRegister", ModMinecraft.AddClient()) : ModBase.m_IdentifierRepository.Get("CacheAuthServerRegister", null)).Replace("/auth/register", "/auth/forgot"));
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00047BD4 File Offset: 0x00045DD4
		private void ReloadRegisterButton()
		{
			string str = Conversions.ToString((ModMinecraft.AddClient() != null) ? ModBase.m_IdentifierRepository.Get("VersionServerAuthRegister", ModMinecraft.AddClient()) : ModBase.m_IdentifierRepository.Get("CacheAuthServerRegister", null));
			this.BtnLink.Visibility = (string.IsNullOrEmpty(new ValidateHttp().Validate(str)) ? Visibility.Visible : Visibility.Collapsed);
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x00006AD6 File Offset: 0x00004CD6
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x00047C38 File Offset: 0x00045E38
		internal virtual MyComboBox ComboName
		{
			[CompilerGenerated]
			get
			{
				return this.decoratorClient;
			}
			[CompilerGenerated]
			set
			{
				MyComboBox.TextChangedEventHandler obj = new MyComboBox.TextChangedEventHandler(this.ComboName_TextChanged);
				SelectionChangedEventHandler value2 = delegate(object sender, SelectionChangedEventArgs e)
				{
					this.ComboName_SelectionChanged((MyComboBox)sender, e);
				};
				MyComboBox.TextChangedEventHandler obj2 = delegate(object sender, TextChangedEventArgs e)
				{
					this.ComboName_TextChanged();
				};
				MyComboBox myComboBox = this.decoratorClient;
				if (myComboBox != null)
				{
					myComboBox.ValidateParser(obj);
					myComboBox.SelectionChanged -= value2;
					myComboBox.ValidateParser(obj2);
				}
				this.decoratorClient = value;
				myComboBox = this.decoratorClient;
				if (myComboBox != null)
				{
					myComboBox.DeleteParser(obj);
					myComboBox.SelectionChanged += value2;
					myComboBox.DeleteParser(obj2);
				}
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x00006ADE File Offset: 0x00004CDE
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x00047CB4 File Offset: 0x00045EB4
		internal virtual PasswordBox TextPass
		{
			[CompilerGenerated]
			get
			{
				return this.m_InstanceClient;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = new RoutedEventHandler(this.TextPass_PasswordChanged);
				PasswordBox instanceClient = this.m_InstanceClient;
				if (instanceClient != null)
				{
					instanceClient.PasswordChanged -= value2;
				}
				this.m_InstanceClient = value;
				instanceClient = this.m_InstanceClient;
				if (instanceClient != null)
				{
					instanceClient.PasswordChanged += value2;
				}
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00006AE6 File Offset: 0x00004CE6
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x00047CF8 File Offset: 0x00045EF8
		internal virtual MyCheckBox CheckRemember
		{
			[CompilerGenerated]
			get
			{
				return this.stateClient;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = delegate(object a0, bool a1)
				{
					this.CheckBoxChange((MyCheckBox)a0, a1);
				};
				MyCheckBox myCheckBox = this.stateClient;
				if (myCheckBox != null)
				{
					myCheckBox.PublishConfig(obj);
				}
				this.stateClient = value;
				myCheckBox = this.stateClient;
				if (myCheckBox != null)
				{
					myCheckBox.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00006AEE File Offset: 0x00004CEE
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x00047D3C File Offset: 0x00045F3C
		internal virtual MyTextButton BtnLink
		{
			[CompilerGenerated]
			get
			{
				return this.m_CallbackClient;
			}
			[CompilerGenerated]
			set
			{
				MyTextButton.ClickEventHandler value2 = new MyTextButton.ClickEventHandler(this.Btn_Click);
				MyTextButton callbackClient = this.m_CallbackClient;
				if (callbackClient != null)
				{
					callbackClient.Click -= value2;
				}
				this.m_CallbackClient = value;
				callbackClient = this.m_CallbackClient;
				if (callbackClient != null)
				{
					callbackClient.Click += value2;
				}
			}
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00047D80 File Offset: 0x00045F80
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_TemplateClient)
			{
				this.m_TemplateClient = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/pageloginauth.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00047DB0 File Offset: 0x00045FB0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.ComboName = (MyComboBox)target;
				return;
			}
			if (connectionId == 2)
			{
				this.TextPass = (PasswordBox)target;
				return;
			}
			if (connectionId == 3)
			{
				this.CheckRemember = (MyCheckBox)target;
				return;
			}
			if (connectionId == 4)
			{
				this.BtnLink = (MyTextButton)target;
				return;
			}
			this.m_TemplateClient = true;
		}

		// Token: 0x040004DC RID: 1244
		private bool m_RefClient;

		// Token: 0x040004DD RID: 1245
		[AccessedThroughProperty("ComboName")]
		[CompilerGenerated]
		private MyComboBox decoratorClient;

		// Token: 0x040004DE RID: 1246
		[AccessedThroughProperty("TextPass")]
		[CompilerGenerated]
		private PasswordBox m_InstanceClient;

		// Token: 0x040004DF RID: 1247
		[AccessedThroughProperty("CheckRemember")]
		[CompilerGenerated]
		private MyCheckBox stateClient;

		// Token: 0x040004E0 RID: 1248
		[AccessedThroughProperty("BtnLink")]
		[CompilerGenerated]
		private MyTextButton m_CallbackClient;

		// Token: 0x040004E1 RID: 1249
		private bool m_TemplateClient;
	}
}
