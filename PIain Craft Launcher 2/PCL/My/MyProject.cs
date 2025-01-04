using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL.My
{
	// Token: 0x02000010 RID: 16
	[HideModuleName]
	[StandardModule]
	[GeneratedCode("MyTemplate", "11.0.0.0")]
	internal sealed class MyProject
	{
		// Token: 0x02000011 RID: 17
		[ComVisible(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal sealed class ThreadSafeObjectProvider<T> where T : new()
		{
			// Token: 0x06000049 RID: 73 RVA: 0x000023F4 File Offset: 0x000005F4
			internal T StopTests()
			{
				if (MyProject.ThreadSafeObjectProvider<T>.m_SetterRepository == null)
				{
					MyProject.ThreadSafeObjectProvider<T>.m_SetterRepository = Activator.CreateInstance<T>();
				}
				return MyProject.ThreadSafeObjectProvider<T>.m_SetterRepository;
			}

			// Token: 0x0600004A RID: 74 RVA: 0x00002411 File Offset: 0x00000611
			[EditorBrowsable(EditorBrowsableState.Never)]
			public ThreadSafeObjectProvider()
			{
			}

			// Token: 0x04000001 RID: 1
			[ThreadStatic]
			[CompilerGenerated]
			private static T m_SetterRepository;
		}
	}
}
