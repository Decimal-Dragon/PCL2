using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x02000149 RID: 329
	public class ValidateExceptSame : Validate
	{
		// Token: 0x06000D88 RID: 3464 RVA: 0x00008AA2 File Offset: 0x00006CA2
		[CompilerGenerated]
		public Collection<string> ComputeClient()
		{
			return this._ImporterTests;
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00008AAA File Offset: 0x00006CAA
		[CompilerGenerated]
		public void IncludeClient(Collection<string> AutoPropertyValue)
		{
			this._ImporterTests = AutoPropertyValue;
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00008AB3 File Offset: 0x00006CB3
		[CompilerGenerated]
		public string SortClient()
		{
			return this.m_WorkerTests;
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00008ABB File Offset: 0x00006CBB
		[CompilerGenerated]
		public void InvokeClient(string AutoPropertyValue)
		{
			this.m_WorkerTests = AutoPropertyValue;
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00008AC4 File Offset: 0x00006CC4
		[CompilerGenerated]
		public bool DestroyClient()
		{
			return this.connectionTests;
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00008ACC File Offset: 0x00006CCC
		[CompilerGenerated]
		public void PrepareClient(bool AutoPropertyValue)
		{
			this.connectionTests = AutoPropertyValue;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00008AD5 File Offset: 0x00006CD5
		public ValidateExceptSame()
		{
			this.IncludeClient(new Collection<string>());
			this.PrepareClient(false);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00008AF0 File Offset: 0x00006CF0
		public ValidateExceptSame(Collection<string> Excepts, string ErrorMessage = "输入内容不能为 %！", bool IgnoreCase = false)
		{
			this.IncludeClient(new Collection<string>());
			this.PrepareClient(false);
			this.IncludeClient(Excepts);
			this.InvokeClient(ErrorMessage);
			this.PrepareClient(IgnoreCase);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0005BAD0 File Offset: 0x00059CD0
		public ValidateExceptSame(IEnumerable Excepts, string ErrorMessage = "输入内容不能为 %！", bool IgnoreCase = false)
		{
			this.IncludeClient(new Collection<string>());
			this.PrepareClient(false);
			this.IncludeClient(new Collection<string>());
			try
			{
				foreach (object value in Excepts)
				{
					string item = Conversions.ToString(value);
					this.ComputeClient().Add(item);
				}
			}
			finally
			{
				IEnumerator enumerator;
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
			this.InvokeClient(ErrorMessage);
			this.PrepareClient(IgnoreCase);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0005BB60 File Offset: 0x00059D60
		public override string Validate(string Str)
		{
			string result;
			if (Str == null)
			{
				result = this.SortClient().Replace("%", "null");
			}
			else
			{
				try
				{
					foreach (string text in this.ComputeClient())
					{
						if (this.DestroyClient())
						{
							if (Operators.CompareString(Str.ToLower(), text.ToLower(), false) == 0)
							{
								return this.SortClient().Replace("%", text);
							}
						}
						else if (Str.Equals(text))
						{
							return this.SortClient().Replace("%", text);
						}
					}
				}
				finally
				{
					IEnumerator<string> enumerator;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
				result = "";
			}
			return result;
		}

		// Token: 0x0400070D RID: 1805
		[CompilerGenerated]
		private Collection<string> _ImporterTests;

		// Token: 0x0400070E RID: 1806
		[CompilerGenerated]
		private string m_WorkerTests;

		// Token: 0x0400070F RID: 1807
		[CompilerGenerated]
		private bool connectionTests;
	}
}
