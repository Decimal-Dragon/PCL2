using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x0200018B RID: 395
	[DesignerGenerated]
	public class PageLoginLegacy : Grid, IComponentConnector
	{
		// Token: 0x06000FD6 RID: 4054 RVA: 0x00009B20 File Offset: 0x00007D20
		public PageLoginLegacy()
		{
			base.Loaded += this.PageLoginLegacy_Loaded;
			this.m_ImporterMapper = false;
			this.InitializeComponent();
			this.Skin.callbackMapper = PageLaunchLeft.infoMapper;
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00009B58 File Offset: 0x00007D58
		private void PageLoginLegacy_Loaded(object sender, RoutedEventArgs e)
		{
			this.Skin.callbackMapper.Start(null, false);
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00074904 File Offset: 0x00072B04
		public void Reload(bool KeepInput)
		{
			if (KeepInput && this.m_ImporterMapper)
			{
				string text = this.ComboName.Text.Trim();
				this.ComboName.ItemsSource = (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LoginLegacyName", null), "", false) ? null : ModBase.m_IdentifierRepository.Get("LoginLegacyName", null).ToString().Split("¨"));
				this.ComboName.Text = text;
			}
			else if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LoginLegacyName", null), "", false))
			{
				this.ComboName.ItemsSource = null;
			}
			else
			{
				this.ComboName.ItemsSource = ModBase.m_IdentifierRepository.Get("LoginLegacyName", null).ToString().Split("¨");
				this.ComboName.Text = ModBase.m_IdentifierRepository.Get("LoginLegacyName", null).ToString().BeforeFirst("¨", false).Trim();
			}
			this.m_ImporterMapper = true;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00074A14 File Offset: 0x00072C14
		public static ModLaunch.McLoginData GetLoginData()
		{
			string rulesMap = (ModMain.m_ProcIterator == null) ? "" : ModMain.m_ProcIterator.ComboName.Text.Replace("¨", "").Trim();
			return new ModLaunch.McLoginLegacy
			{
				m_RulesMap = rulesMap,
				m_RefMap = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("LaunchSkinType", null)),
				_DecoratorMap = Conversions.ToString(ModBase.m_IdentifierRepository.Get("LaunchSkinID", null))
			};
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00074A98 File Offset: 0x00072C98
		public static string IsVaild(ModLaunch.McLoginLegacy LoginData)
		{
			string result;
			if (Operators.CompareString(LoginData.m_RulesMap.Trim(), "", false) == 0)
			{
				result = "玩家名不能为空！";
			}
			else if (LoginData.m_RulesMap.Contains("\""))
			{
				result = "玩家名不能包含英文引号！";
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x00009B6C File Offset: 0x00007D6C
		public string IsVaild()
		{
			return PageLoginLegacy.IsVaild((ModLaunch.McLoginLegacy)PageLoginLegacy.GetLoginData());
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00074AE8 File Offset: 0x00072CE8
		private void ComboName_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Space)
			{
				int caretIndex = ((MyTextBox)this.ComboName.Template.FindName("PART_EditableTextBox", this.ComboName)).CaretIndex;
				if (caretIndex == this.ComboName.Text.Length || caretIndex == 0)
				{
					e.Handled = true;
				}
			}
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00074B44 File Offset: 0x00072D44
		private void ComboLegacy_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("LaunchSkinType", null), 0, false))
			{
				PageLaunchLeft.infoMapper.Start(null, true);
			}
			this.HintChinese.Visibility = (ModBase.RegexCheck(this.ComboName.Text, "^[0-9A-Za-z_]*$", 0) ? Visibility.Collapsed : Visibility.Visible);
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00074BA4 File Offset: 0x00072DA4
		private void Skin_Click()
		{
			if (Conversions.ToBoolean((Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenPageSetup", null)) || Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiHiddenSetupLaunch", null))) && !PageSetupUI.CreateClient()))
			{
				ModMain.Hint("启动设置已被禁用！", ModMain.HintType.Critical, true);
				return;
			}
			ModMain._ProcessIterator.PageChange(FormMain.PageType.Setup, FormMain.PageSubType.Default);
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x00009B7D File Offset: 0x00007D7D
		// (set) Token: 0x06000FE0 RID: 4064 RVA: 0x00009B85 File Offset: 0x00007D85
		internal virtual MyHint HintChinese { get; set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00009B8E File Offset: 0x00007D8E
		// (set) Token: 0x06000FE2 RID: 4066 RVA: 0x00074C14 File Offset: 0x00072E14
		internal virtual MySkin Skin
		{
			[CompilerGenerated]
			get
			{
				return this._ConnectionMapper;
			}
			[CompilerGenerated]
			set
			{
				MySkin.ClickEventHandler value2 = delegate(object sender, MouseButtonEventArgs e)
				{
					this.Skin_Click();
				};
				MySkin connectionMapper = this._ConnectionMapper;
				if (connectionMapper != null)
				{
					connectionMapper.Click -= value2;
				}
				this._ConnectionMapper = value;
				connectionMapper = this._ConnectionMapper;
				if (connectionMapper != null)
				{
					connectionMapper.Click += value2;
				}
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x00009B96 File Offset: 0x00007D96
		// (set) Token: 0x06000FE4 RID: 4068 RVA: 0x00074C58 File Offset: 0x00072E58
		internal virtual MyComboBox ComboName
		{
			[CompilerGenerated]
			get
			{
				return this.m_ServerMapper;
			}
			[CompilerGenerated]
			set
			{
				KeyEventHandler value2 = new KeyEventHandler(this.ComboName_PreviewKeyDown);
				MyComboBox.TextChangedEventHandler obj = new MyComboBox.TextChangedEventHandler(this.ComboLegacy_TextChanged);
				MyComboBox serverMapper = this.m_ServerMapper;
				if (serverMapper != null)
				{
					serverMapper.PreviewKeyDown -= value2;
					serverMapper.ValidateParser(obj);
				}
				this.m_ServerMapper = value;
				serverMapper = this.m_ServerMapper;
				if (serverMapper != null)
				{
					serverMapper.PreviewKeyDown += value2;
					serverMapper.DeleteParser(obj);
				}
			}
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00074CB8 File Offset: 0x00072EB8
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this.m_ResolverMapper)
			{
				this.m_ResolverMapper = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/pageloginlegacy.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00009B9E File Offset: 0x00007D9E
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.HintChinese = (MyHint)target;
				return;
			}
			if (connectionId == 2)
			{
				this.Skin = (MySkin)target;
				return;
			}
			if (connectionId == 3)
			{
				this.ComboName = (MyComboBox)target;
				return;
			}
			this.m_ResolverMapper = true;
		}

		// Token: 0x04000873 RID: 2163
		public bool m_ImporterMapper;

		// Token: 0x04000874 RID: 2164
		[AccessedThroughProperty("HintChinese")]
		[CompilerGenerated]
		private MyHint m_WorkerMapper;

		// Token: 0x04000875 RID: 2165
		[AccessedThroughProperty("Skin")]
		[CompilerGenerated]
		private MySkin _ConnectionMapper;

		// Token: 0x04000876 RID: 2166
		[AccessedThroughProperty("ComboName")]
		[CompilerGenerated]
		private MyComboBox m_ServerMapper;

		// Token: 0x04000877 RID: 2167
		private bool m_ResolverMapper;
	}
}
