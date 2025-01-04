using System;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x02000146 RID: 326
	public class ValidateInteger : Validate
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x00008981 File Offset: 0x00006B81
		// (set) Token: 0x06000D70 RID: 3440 RVA: 0x00008989 File Offset: 0x00006B89
		public int Min { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x00008992 File Offset: 0x00006B92
		// (set) Token: 0x06000D72 RID: 3442 RVA: 0x0000899A File Offset: 0x00006B9A
		public int Max { get; set; }

		// Token: 0x06000D73 RID: 3443 RVA: 0x000089A3 File Offset: 0x00006BA3
		public ValidateInteger()
		{
			this.Max = int.MaxValue;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x000089B7 File Offset: 0x00006BB7
		public ValidateInteger(int Min, int Max)
		{
			this.Max = int.MaxValue;
			this.Min = Min;
			this.Max = Max;
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0005B880 File Offset: 0x00059A80
		public override string Validate(string Str)
		{
			string result;
			if (Str.Length > 9)
			{
				result = "请输入一个大小合理的数字！";
			}
			else if (Operators.CompareString((checked((int)Math.Round(ModBase.Val(Str)))).ToString(), Str, false) != 0)
			{
				result = "请输入一个整数！";
			}
			else if (ModBase.Val(Str) > (double)this.Max)
			{
				result = "不可超过 " + Conversions.ToString(this.Max) + "！";
			}
			else if (ModBase.Val(Str) < (double)this.Min)
			{
				result = "不可低于 " + Conversions.ToString(this.Min) + "！";
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x04000707 RID: 1799
		[CompilerGenerated]
		private int registryTests;

		// Token: 0x04000708 RID: 1800
		[CompilerGenerated]
		private int m_RuleTests;
	}
}
