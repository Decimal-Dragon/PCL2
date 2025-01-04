using System;
using System.Runtime.CompilerServices;

namespace PCL
{
	// Token: 0x02000144 RID: 324
	public class ValidateRegex : Validate
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x00008929 File Offset: 0x00006B29
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x00008931 File Offset: 0x00006B31
		public string Regex { get; set; }

		// Token: 0x06000D66 RID: 3430 RVA: 0x0000893A File Offset: 0x00006B3A
		[CompilerGenerated]
		public string ConcatReader()
		{
			return this.m_WriterTests;
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00008942 File Offset: 0x00006B42
		[CompilerGenerated]
		public void UpdateReader(string AutoPropertyValue)
		{
			this.m_WriterTests = AutoPropertyValue;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0000894B File Offset: 0x00006B4B
		public ValidateRegex()
		{
			this.UpdateReader("正则检查失败！");
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0000895F File Offset: 0x00006B5F
		public ValidateRegex(string Regex, string ErrorDescription = "正则检查失败！")
		{
			this.UpdateReader("正则检查失败！");
			this.Regex = Regex;
			this.UpdateReader(ErrorDescription);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0005B808 File Offset: 0x00059A08
		public override string Validate(string Str)
		{
			string result;
			if (!ModBase.RegexCheck(Str, this.Regex, 0))
			{
				result = this.ConcatReader();
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x04000705 RID: 1797
		[CompilerGenerated]
		private string expressionTests;

		// Token: 0x04000706 RID: 1798
		[CompilerGenerated]
		private string m_WriterTests;
	}
}
