using System;
using Microsoft.VisualBasic;

namespace PCL
{
	// Token: 0x02000143 RID: 323
	public class ValidateNullOrWhiteSpace : Validate
	{
		// Token: 0x06000D62 RID: 3426 RVA: 0x0005B7DC File Offset: 0x000599DC
		public override string Validate(string Str)
		{
			string result;
			if (!Information.IsNothing(Str) && !string.IsNullOrWhiteSpace(Str))
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
