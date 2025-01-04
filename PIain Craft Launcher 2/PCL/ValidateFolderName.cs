using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x0200014A RID: 330
	public class ValidateFolderName : Validate
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x00008B20 File Offset: 0x00006D20
		// (set) Token: 0x06000D94 RID: 3476 RVA: 0x00008B28 File Offset: 0x00006D28
		public string Path { get; set; }

		// Token: 0x06000D95 RID: 3477 RVA: 0x00008B31 File Offset: 0x00006D31
		[CompilerGenerated]
		public bool SelectClient()
		{
			return this.m_ResolverTests;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00008B39 File Offset: 0x00006D39
		[CompilerGenerated]
		public void ReadClient(bool AutoPropertyValue)
		{
			this.m_ResolverTests = AutoPropertyValue;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00008B42 File Offset: 0x00006D42
		[CompilerGenerated]
		public bool ResolveClient()
		{
			return this._StatusTests;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00008B4A File Offset: 0x00006D4A
		[CompilerGenerated]
		public void ReflectClient(bool AutoPropertyValue)
		{
			this._StatusTests = AutoPropertyValue;
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00008B53 File Offset: 0x00006D53
		public ValidateFolderName()
		{
			this.ReadClient(true);
			this.ReflectClient(true);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0005BC1C File Offset: 0x00059E1C
		public ValidateFolderName(string Path, bool UseMinecraftCharCheck = true, bool IgnoreCase = true)
		{
			this.ReadClient(true);
			this.ReflectClient(true);
			int num2;
			int num4;
			object obj;
			try
			{
				IL_15:
				int num = 1;
				this.Path = Path;
				IL_1E:
				num = 2;
				this.ReflectClient(IgnoreCase);
				IL_27:
				num = 3;
				this.ReadClient(UseMinecraftCharCheck);
				IL_30:
				ProjectData.ClearProjectError();
				num2 = 1;
				IL_37:
				num = 5;
				this.m_RoleTests = new DirectoryInfo(Path).EnumerateDirectories();
				IL_4A:
				goto IL_B1;
				IL_4C:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_72:
				goto IL_A6;
				IL_74:
				num4 = num;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num2);
				IL_84:;
			}
			catch when (endfilter(obj is Exception & num2 != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_74;
			}
			IL_A6:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_B1:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0005BCF4 File Offset: 0x00059EF4
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
					result = "文件夹名不能以空格开头！";
				}
				else if (Str.EndsWithF(" ", false))
				{
					result = "文件夹名不能以空格结尾！";
				}
				else
				{
					text = new ValidateLength(1, 100).Validate(Str);
					if (Operators.CompareString(text, "", false) != 0)
					{
						result = text;
					}
					else if (Str.EndsWithF(".", false))
					{
						result = "文件夹名不能以小数点结尾！";
					}
					else
					{
						string text2 = new ValidateExcept(new string(System.IO.Path.GetInvalidFileNameChars()) + (this.SelectClient() ? "!;" : ""), "文件夹名不可包含 % 字符！").Validate(Str);
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
							}, "文件夹名不可为 %！", true).Validate(Str);
							if (Operators.CompareString(text3, "", false) != 0)
							{
								result = text3;
							}
							else if (ModBase.RegexCheck(Str, ".{2,}~\\d", 0))
							{
								result = "文件夹名不能包含这一特殊格式！";
							}
							else
							{
								List<string> list = new List<string>();
								if (this.m_RoleTests != null)
								{
									try
									{
										foreach (DirectoryInfo directoryInfo in this.m_RoleTests)
										{
											list.Add(directoryInfo.Name);
										}
									}
									finally
									{
										IEnumerator<DirectoryInfo> enumerator;
										if (enumerator != null)
										{
											enumerator.Dispose();
										}
									}
								}
								string text4 = new ValidateExceptSame(list, "不可与现有文件夹重名！", this.ResolveClient()).Validate(Str);
								if (Operators.CompareString(text4, "", false) != 0)
								{
									result = text4;
								}
								else
								{
									result = "";
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "检查文件夹名出错", ModBase.LogLevel.Debug, "出现错误");
				result = "错误：" + ex.Message;
			}
			return result;
		}

		// Token: 0x04000710 RID: 1808
		[CompilerGenerated]
		private string m_ServerTests;

		// Token: 0x04000711 RID: 1809
		[CompilerGenerated]
		private bool m_ResolverTests;

		// Token: 0x04000712 RID: 1810
		[CompilerGenerated]
		private bool _StatusTests;

		// Token: 0x04000713 RID: 1811
		private readonly IEnumerable<DirectoryInfo> m_RoleTests;
	}
}
