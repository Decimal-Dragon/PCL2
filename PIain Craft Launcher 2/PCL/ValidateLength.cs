using System;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x02000147 RID: 327
	public class ValidateLength : Validate
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x000089D9 File Offset: 0x00006BD9
		// (set) Token: 0x06000D78 RID: 3448 RVA: 0x000089E1 File Offset: 0x00006BE1
		public int Min { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x000089EA File Offset: 0x00006BEA
		// (set) Token: 0x06000D7A RID: 3450 RVA: 0x000089F2 File Offset: 0x00006BF2
		public int Max { get; set; }

		// Token: 0x06000D7B RID: 3451 RVA: 0x000089FB File Offset: 0x00006BFB
		public ValidateLength()
		{
			this.Min = 0;
			this.Max = int.MaxValue;
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00008A16 File Offset: 0x00006C16
		public ValidateLength(int Min, int Max = 2147483647)
		{
			this.Min = 0;
			this.Max = int.MaxValue;
			this.Min = Min;
			this.Max = Max;
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0005B928 File Offset: 0x00059B28
		public override string Validate(string Str)
		{
			string result;
			if (Strings.Len(Str) != this.Max && this.Max == this.Min)
			{
				result = "长度必须为 " + Conversions.ToString(this.Max) + " 个字符！";
			}
			else if (Strings.Len(Str) > this.Max)
			{
				result = "长度最长为 " + Conversions.ToString(this.Max) + " 个字符！";
			}
			else if (Strings.Len(Str) < this.Min)
			{
				result = "长度至少需 " + Conversions.ToString(this.Min) + " 个字符！";
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x04000709 RID: 1801
		[CompilerGenerated]
		private int _ProccesorTests;

		// Token: 0x0400070A RID: 1802
		[CompilerGenerated]
		private int m_SetterTests;
	}
}
