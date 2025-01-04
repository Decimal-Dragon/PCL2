using System;
using Microsoft.VisualBasic;

namespace PCL
{
	// Token: 0x02000142 RID: 322
	public class ValidateNullOrEmpty : Validate
	{
		// Token: 0x06000D5F RID: 3423 RVA: 0x0005B7B0 File Offset: 0x000599B0
		public override string Validate(string Str)
		{
			string result;
			if (!Information.IsNothing(Str) && !string.IsNullOrEmpty(Str))
			{
				result = "";
			}
			else
			{
				result = "输入内容不能为空！";
			}
			return result;
		}
	}
}
