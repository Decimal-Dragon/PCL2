using System;
using Microsoft.VisualBasic;

namespace PCL
{
	// Token: 0x02000141 RID: 321
	public class ValidateNullable : Validate
	{
		// Token: 0x06000D5C RID: 3420 RVA: 0x0005B788 File Offset: 0x00059988
		public override string Validate(string Str)
		{
			string result;
			if (!Information.IsNothing(Str) && !string.IsNullOrEmpty(Str))
			{
				result = "";
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
