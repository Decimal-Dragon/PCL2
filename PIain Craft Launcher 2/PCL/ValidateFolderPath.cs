using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;
using PCL.My;

namespace PCL
{
	// Token: 0x0200014D RID: 333
	public class ValidateFolderPath : Validate
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x00008C44 File Offset: 0x00006E44
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x00008C4C File Offset: 0x00006E4C
		public bool UseMinecraftCharCheck { get; set; }

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00008C55 File Offset: 0x00006E55
		public ValidateFolderPath()
		{
			this.UseMinecraftCharCheck = true;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00008C65 File Offset: 0x00006E65
		public ValidateFolderPath(bool UseMinecraftCharCheck)
		{
			this.UseMinecraftCharCheck = true;
			this.UseMinecraftCharCheck = UseMinecraftCharCheck;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0005C2F4 File Offset: 0x0005A4F4
		public override string Validate(string Str)
		{
			Str = Str.Replace("/", "\\");
			if (!Str.TrimEnd(new char[]
			{
				'\\'
			}).EndsWithF(":", false))
			{
				Str = Str.TrimEnd(new char[]
				{
					'\\'
				});
			}
			string text = new ValidateNullOrWhiteSpace().Validate(Str);
			checked
			{
				string result;
				if (Operators.CompareString(text, "", false) != 0)
				{
					result = text;
				}
				else
				{
					text = new ValidateLength(1, 254).Validate(Str);
					if (Operators.CompareString(text, "", false) != 0)
					{
						result = text;
					}
					else
					{
						if (!Str.StartsWithF("\\\\Mac\\", false))
						{
							try
							{
								foreach (DriveInfo driveInfo in MyWpfExtension.ManageParser().FileSystem.Drives)
								{
									if (Operators.CompareString(Str.ToUpper(), driveInfo.Name, false) == 0)
									{
										return "";
									}
									if (Str.StartsWithF(driveInfo.Name, true))
									{
										goto IL_107;
									}
								}
							}
							finally
							{
								IEnumerator<DriveInfo> enumerator;
								if (enumerator != null)
								{
									enumerator.Dispose();
								}
							}
							return "文件夹路径头存在错误！";
						}
						IL_107:
						int num = Str.StartsWithF("\\\\Mac\\", false) ? 2 : 1;
						int num2 = Enumerable.Count<string>(Str.Split("\\")) - 1;
						for (int i = num; i <= num2; i++)
						{
							string str = Str.Split("\\")[i];
							if (Operators.CompareString(new ValidateNullOrWhiteSpace().Validate(str), "", false) != 0)
							{
								return "文件夹路径存在错误！";
							}
							string text2 = new ValidateExcept(new string(Path.GetInvalidFileNameChars()) + (this.UseMinecraftCharCheck ? "!;" : ""), "路径中存在无效字符！").Validate(str);
							if (Operators.CompareString(text2, "", false) != 0)
							{
								return text2;
							}
							if (str.StartsWithF(" ", false))
							{
								return "文件夹名不能以空格开头！";
							}
							if (str.EndsWithF(" ", false))
							{
								return "文件夹名不能以空格结尾！";
							}
							if (str.EndsWithF(".", false))
							{
								return "文件夹名不能以小数点结尾！";
							}
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
							}, "文件夹名不可为 %！", false).Validate(str);
							if (Operators.CompareString(text3, "", false) != 0)
							{
								return text3;
							}
							if (ModBase.RegexCheck(Str, ".{2,}~\\d", 0))
							{
								return "文件夹名不能包含这一特殊格式！";
							}
						}
						result = "";
					}
				}
				return result;
			}
		}

		// Token: 0x0400071B RID: 1819
		[CompilerGenerated]
		private bool m_AdvisorTests;
	}
}
