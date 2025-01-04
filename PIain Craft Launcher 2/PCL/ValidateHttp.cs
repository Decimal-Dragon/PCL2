using System;

namespace PCL
{
	// Token: 0x02000145 RID: 325
	public class ValidateHttp : Validate
	{
		// Token: 0x06000D6D RID: 3437 RVA: 0x0005B834 File Offset: 0x00059A34
		public override string Validate(string Str)
		{
			if (Str.EndsWithF("/", false))
			{
				Str = Str.Substring(0, checked(Str.Length - 1));
			}
			string result;
			if (!ModBase.RegexCheck(Str, "^(http[s]?)\\://", 0))
			{
				result = "输入的网址无效！";
			}
			else
			{
				result = "";
			}
			return result;
		}
	}
}
