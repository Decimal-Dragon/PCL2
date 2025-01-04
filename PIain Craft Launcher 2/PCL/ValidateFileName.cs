using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x0200014B RID: 331
	public class ValidateFileName : Validate
	{
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00008B6A File Offset: 0x00006D6A
		// (set) Token: 0x06000D9E RID: 3486 RVA: 0x00008B72 File Offset: 0x00006D72
		public string Name { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00008B7B File Offset: 0x00006D7B
		// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x00008B83 File Offset: 0x00006D83
		public bool UseMinecraftCharCheck { get; set; }

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00008B8C File Offset: 0x00006D8C
		[CompilerGenerated]
		public bool DeleteClient()
		{
			return this.m_ValTests;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00008B94 File Offset: 0x00006D94
		[CompilerGenerated]
		public void ValidateClient(bool AutoPropertyValue)
		{
			this.m_ValTests = AutoPropertyValue;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00008B9D File Offset: 0x00006D9D
		[CompilerGenerated]
		public string GetClient()
		{
			return this._AttrTests;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00008BA5 File Offset: 0x00006DA5
		[CompilerGenerated]
		public void FindClient(string AutoPropertyValue)
		{
			this._AttrTests = AutoPropertyValue;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00008BAE File Offset: 0x00006DAE
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x00008BB6 File Offset: 0x00006DB6
		public object RequireParentFolderExists
		{
			[CompilerGenerated]
			get
			{
				return this._CandidateTests;
			}
			[CompilerGenerated]
			set
			{
				this._CandidateTests = RuntimeHelpers.GetObjectValue(value);
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00008BC4 File Offset: 0x00006DC4
		public ValidateFileName()
		{
			this.UseMinecraftCharCheck = true;
			this.ValidateClient(true);
			this.FindClient(null);
			this.RequireParentFolderExists = true;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00008BEE File Offset: 0x00006DEE
		public ValidateFileName(string Name, bool UseMinecraftCharCheck = true, bool IgnoreCase = true)
		{
			this.UseMinecraftCharCheck = true;
			this.ValidateClient(true);
			this.FindClient(null);
			this.RequireParentFolderExists = true;
			this.Name = Name;
			this.ValidateClient(IgnoreCase);
			this.UseMinecraftCharCheck = UseMinecraftCharCheck;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0005BFE4 File Offset: 0x0005A1E4
		public override string Validate(string Str)
		{
			string result;
			try
			{
				string text = new ValidateNullOrWhiteSpace().Validate(Str);
				if (Operators.CompareString(text, "", false) != 0)
				{
					result = text;
				}
				else if (Str.StartsWithF(" ", false))
				{
					result = "文件名不能以空格开头！";
				}
				else if (Str.EndsWithF(" ", false))
				{
					result = "文件名不能以空格结尾！";
				}
				else
				{
					text = new ValidateLength(1, 253).Validate(Str + this.GetClient());
					if (Operators.CompareString(text, "", false) != 0)
					{
						result = text;
					}
					else if (Str.EndsWithF(".", false))
					{
						result = "文件名不能以小数点结尾！";
					}
					else
					{
						string text2 = new ValidateExcept(new string(Path.GetInvalidFileNameChars()) + (this.UseMinecraftCharCheck ? "!;" : ""), "文件名不可包含 % 字符！").Validate(Str);
						if (Operators.CompareString(text2, "", false) != 0)
						{
							result = text2;
						}
						else
						{
							string text3 = new ValidateExceptSame(new string[]
							{
								"CON",
								"PRN",
								"AUX",
								"CLOCK$",
								"NUL",
								"COM0",
								"COM1",
								"COM2",
								"COM3",
								"COM4",
								"COM5",
								"COM6",
								"COM7",
								"COM8",
								"COM9",
								"LPT0",
								"LPT1",
								"LPT2",
								"LPT3",
								"LPT4",
								"LPT5",
								"LPT6",
								"LPT7",
								"LPT8",
								"LPT9"
							}, "文件名不可为 %！", true).Validate(Str);
							if (Operators.CompareString(text3, "", false) != 0)
							{
								result = text3;
							}
							else if (ModBase.RegexCheck(Str, ".{2,}~\\d", 0))
							{
								result = "文件名不能包含这一特殊格式！";
							}
							else
							{
								if (this.GetClient() != null)
								{
									DirectoryInfo directoryInfo = new DirectoryInfo(this.GetClient());
									if (directoryInfo.Exists)
									{
										string text4 = new ValidateExceptSame(Enumerable.Select<FileInfo, string>(directoryInfo.EnumerateFiles("*"), (ValidateFileName._Closure$__.$I22-0 == null) ? (ValidateFileName._Closure$__.$I22-0 = ((FileInfo f) => f.Name)) : ValidateFileName._Closure$__.$I22-0), "不可与现有文件重名！", this.DeleteClient()).Validate(Str);
										if (Operators.CompareString(text4, "", false) != 0)
										{
											return text4;
										}
									}
									else if (Conversions.ToBoolean(this.RequireParentFolderExists))
									{
										return string.Format("父文件夹不存在：{0}", this.GetClient());
									}
								}
								result = "";
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "检查文件名出错", ModBase.LogLevel.Debug, "出现错误");
				result = "错误：" + ex.Message;
			}
			return result;
		}

		// Token: 0x04000714 RID: 1812
		[CompilerGenerated]
		private string m_StructTests;

		// Token: 0x04000715 RID: 1813
		[CompilerGenerated]
		private bool _PrinterTests;

		// Token: 0x04000716 RID: 1814
		[CompilerGenerated]
		private bool m_ValTests;

		// Token: 0x04000717 RID: 1815
		[CompilerGenerated]
		private string _AttrTests;

		// Token: 0x04000718 RID: 1816
		[CompilerGenerated]
		private object _CandidateTests;
	}
}
