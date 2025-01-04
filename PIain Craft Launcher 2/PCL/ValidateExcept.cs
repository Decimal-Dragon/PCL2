using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x02000148 RID: 328
	public class ValidateExcept : Validate
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x00008A3F File Offset: 0x00006C3F
		// (set) Token: 0x06000D80 RID: 3456 RVA: 0x00008A47 File Offset: 0x00006C47
		public Collection<string> Excepts { get; set; }

		// Token: 0x06000D81 RID: 3457 RVA: 0x00008A50 File Offset: 0x00006C50
		[CompilerGenerated]
		public string FillClient()
		{
			return this._ExporterTests;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00008A58 File Offset: 0x00006C58
		[CompilerGenerated]
		public void WriteClient(string AutoPropertyValue)
		{
			this._ExporterTests = AutoPropertyValue;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00008A61 File Offset: 0x00006C61
		public ValidateExcept()
		{
			this.Excepts = new Collection<string>();
			this.WriteClient("输入内容不能包含 %！");
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00008A80 File Offset: 0x00006C80
		public ValidateExcept(Collection<string> Excepts, string ErrorMessage = "输入内容不能包含 %！")
		{
			this.Excepts = new Collection<string>();
			this.Excepts = Excepts;
			this.WriteClient(ErrorMessage);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0005B9CC File Offset: 0x00059BCC
		public ValidateExcept(IEnumerable Excepts, string ErrorMessage = "输入内容不能包含 %！")
		{
			this.Excepts = new Collection<string>();
			this.Excepts = new Collection<string>();
			this.WriteClient(ErrorMessage);
			try
			{
				foreach (object value in Excepts)
				{
					string item = Conversions.ToString(value);
					this.Excepts.Add(item);
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
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0005BA4C File Offset: 0x00059C4C
		public override string Validate(string Str)
		{
			try
			{
				foreach (string text in this.Excepts)
				{
					if (Str.IndexOfF(text, true) >= 0)
					{
						if (Information.IsNothing(this.FillClient()))
						{
							this.WriteClient("");
						}
						return this.FillClient().Replace("%", text);
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
			return "";
		}

		// Token: 0x0400070B RID: 1803
		[CompilerGenerated]
		private Collection<string> factoryTests;

		// Token: 0x0400070C RID: 1804
		[CompilerGenerated]
		private string _ExporterTests;
	}
}
