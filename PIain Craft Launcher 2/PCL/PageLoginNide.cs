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
	// Token: 0x0200018A RID: 394
	[DesignerGenerated]
	public class PageLoginNide : Grid, IComponentConnector
	{
		// Token: 0x06000FBC RID: 4028 RVA: 0x00009A63 File Offset: 0x00007C63
		public PageLoginNide()
		{
			this._RegistryMapper = true;
			this.InitializeComponent();
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0007434C File Offset: 0x0007254C
		public void Reload(bool KeepInput)
		{
			this.CheckRemember.Checked = Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LoginRemember", null));
			if (KeepInput && !this._RegistryMapper)
			{
				string text = this.ComboName.Text;
				this.ComboName.ItemsSource = (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LoginNideEmail", null), "", false) ? null : ModBase.m_IdentifierRepository.Get("LoginNideEmail", null).ToString().Split("¨"));
				this.ComboName.Text = text;
			}
			else if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LoginNideEmail", null), "", false))
			{
				this.ComboName.ItemsSource = null;
			}
			else
			{
				this.ComboName.ItemsSource = ModBase.m_IdentifierRepository.Get("LoginNideEmail", null).ToString().Split("¨");
				this.ComboName.Text = ModBase.m_IdentifierRepository.Get("LoginNideEmail", null).ToString().BeforeFirst("¨", false);
				if (Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("LoginRemember", null)))
				{
					this.TextPass.Password = ModBase.m_IdentifierRepository.Get("LoginNidePass", null).ToString().BeforeFirst("¨", false).Trim();
				}
			}
			this._RegistryMapper = false;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x000744BC File Offset: 0x000726BC
		public static ModLaunch.McLoginServer GetLoginData()
		{
			string str = Conversions.ToString(Information.IsNothing(ModMinecraft.AddClient()) ? ModBase.m_IdentifierRepository.Get("CacheNideServer", null) : ModBase.m_IdentifierRepository.Get("VersionServerNide", ModMinecraft.AddClient()));
			ModLaunch.McLoginServer result;
			if (ModMain.parserRepository == null)
			{
				result = new ModLaunch.McLoginServer(ModLaunch.McLoginType.Nide)
				{
					m_SerializerMap = "Nide",
					m_IssuerMap = "",
					_IndexerMap = "",
					m_WatcherMap = "统一通行证",
					Type = ModLaunch.McLoginType.Nide,
					interpreterMap = "https://auth.mc-user.com:233/" + str + "/authserver"
				};
			}
			else
			{
				result = new ModLaunch.McLoginServer(ModLaunch.McLoginType.Nide)
				{
					m_SerializerMap = "Nide",
					m_IssuerMap = ModMain.parserRepository.ComboName.Text.Replace("¨", "").Trim(),
					_IndexerMap = ModMain.parserRepository.TextPass.Password.Replace("¨", "").Trim(),
					m_WatcherMap = "统一通行证",
					Type = ModLaunch.McLoginType.Nide,
					interpreterMap = "https://auth.mc-user.com:233/" + str + "/authserver"
				};
			}
			return result;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00047A00 File Offset: 0x00045C00
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

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00009A79 File Offset: 0x00007C79
		public string IsVaild()
		{
			return PageLoginNide.IsVaild(PageLoginNide.GetLoginData());
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x000745EC File Offset: 0x000727EC
		private void ComboName_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(sender, null, "Text", new object[0], null, null, null), "", false))
			{
				this.TextPass.Password = "";
			}
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set("CacheNideAccess", "", false, null);
			}
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00009A85 File Offset: 0x00007C85
		private void TextPass_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set("CacheNideAccess", "", false, null);
			}
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x00074648 File Offset: 0x00072848
		private void ComboName_SelectionChanged(MyComboBox sender, SelectionChangedEventArgs e)
		{
			if (Conversions.ToBoolean(sender.SelectedIndex == -1 || Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("LoginRemember", null)))))
			{
				this.TextPass.Password = "";
				return;
			}
			this.TextPass.Password = ModBase.m_IdentifierRepository.Get("LoginNidePass", null).ToString().Split("¨")[sender.SelectedIndex].Trim();
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00006A7A File Offset: 0x00004C7A
		private void CheckBoxChange(MyCheckBox sender, object e)
		{
			if (ModAnimation.CalcParser() == 0)
			{
				ModBase.m_IdentifierRepository.Set(Conversions.ToString(sender.Tag), sender.Checked, false, null);
			}
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00009AA4 File Offset: 0x00007CA4
		private void ComboName_TextChanged()
		{
			this.BtnLink.Content = ((Operators.CompareString(this.ComboName.Text, "", false) == 0) ? "注册账号" : "找回密码");
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x000746D0 File Offset: 0x000728D0
		private void Btn_Click(object sender, EventArgs e)
		{
			if (Operators.ConditionalCompareObjectEqual(this.BtnLink.Content, "注册账号", false))
			{
				ModBase.OpenWebsite(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("https://login.mc-user.com:233/", ModBase.m_IdentifierRepository.Get("VersionServerNide", ModMinecraft.AddClient())), "/register")));
				return;
			}
			ModBase.OpenWebsite("https://login.mc-user.com:233/account/login");
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00009AD5 File Offset: 0x00007CD5
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x00074734 File Offset: 0x00072934
		internal virtual MyComboBox ComboName
		{
			[CompilerGenerated]
			get
			{
				return this.ruleMapper;
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
				MyComboBox myComboBox = this.ruleMapper;
				if (myComboBox != null)
				{
					myComboBox.ValidateParser(obj);
					myComboBox.SelectionChanged -= value2;
					myComboBox.ValidateParser(obj2);
				}
				this.ruleMapper = value;
				myComboBox = this.ruleMapper;
				if (myComboBox != null)
				{
					myComboBox.DeleteParser(obj);
					myComboBox.SelectionChanged += value2;
					myComboBox.DeleteParser(obj2);
				}
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x00009ADD File Offset: 0x00007CDD
		// (set) Token: 0x06000FCA RID: 4042 RVA: 0x000747B0 File Offset: 0x000729B0
		internal virtual PasswordBox TextPass
		{
			[CompilerGenerated]
			get
			{
				return this._ProccesorMapper;
			}
			[CompilerGenerated]
			set
			{
				RoutedEventHandler value2 = new RoutedEventHandler(this.TextPass_PasswordChanged);
				PasswordBox proccesorMapper = this._ProccesorMapper;
				if (proccesorMapper != null)
				{
					proccesorMapper.PasswordChanged -= value2;
				}
				this._ProccesorMapper = value;
				proccesorMapper = this._ProccesorMapper;
				if (proccesorMapper != null)
				{
					proccesorMapper.PasswordChanged += value2;
				}
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00009AE5 File Offset: 0x00007CE5
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x000747F4 File Offset: 0x000729F4
		internal virtual MyCheckBox CheckRemember
		{
			[CompilerGenerated]
			get
			{
				return this.m_SetterMapper;
			}
			[CompilerGenerated]
			set
			{
				MyCheckBox.ChangeEventHandler obj = delegate(object a0, bool a1)
				{
					this.CheckBoxChange((MyCheckBox)a0, a1);
				};
				MyCheckBox setterMapper = this.m_SetterMapper;
				if (setterMapper != null)
				{
					setterMapper.PublishConfig(obj);
				}
				this.m_SetterMapper = value;
				setterMapper = this.m_SetterMapper;
				if (setterMapper != null)
				{
					setterMapper.InstantiateConfig(obj);
				}
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00009AED File Offset: 0x00007CED
		// (set) Token: 0x06000FCE RID: 4046 RVA: 0x00074838 File Offset: 0x00072A38
		internal virtual MyTextButton BtnLink
		{
			[CompilerGenerated]
			get
			{
				return this.factoryMapper;
			}
			[CompilerGenerated]
			set
			{
				MyTextButton.ClickEventHandler value2 = new MyTextButton.ClickEventHandler(this.Btn_Click);
				MyTextButton myTextButton = this.factoryMapper;
				if (myTextButton != null)
				{
					myTextButton.Click -= value2;
				}
				this.factoryMapper = value;
				myTextButton = this.factoryMapper;
				if (myTextButton != null)
				{
					myTextButton.Click += value2;
				}
			}
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0007487C File Offset: 0x00072A7C
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_ExporterMapper)
			{
				this.m_ExporterMapper = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/pageloginnide.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x000748AC File Offset: 0x00072AAC
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
			this.m_ExporterMapper = true;
		}

		// Token: 0x0400086D RID: 2157
		private bool _RegistryMapper;

		// Token: 0x0400086E RID: 2158
		[AccessedThroughProperty("ComboName")]
		[CompilerGenerated]
		private MyComboBox ruleMapper;

		// Token: 0x0400086F RID: 2159
		[AccessedThroughProperty("TextPass")]
		[CompilerGenerated]
		private PasswordBox _ProccesorMapper;

		// Token: 0x04000870 RID: 2160
		[AccessedThroughProperty("CheckRemember")]
		[CompilerGenerated]
		private MyCheckBox m_SetterMapper;

		// Token: 0x04000871 RID: 2161
		[AccessedThroughProperty("BtnLink")]
		[CompilerGenerated]
		private MyTextButton factoryMapper;

		// Token: 0x04000872 RID: 2162
		private bool m_ExporterMapper;
	}
}
