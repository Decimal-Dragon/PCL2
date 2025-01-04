using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.Logging;

namespace PCL.My
{
	// Token: 0x02000012 RID: 18
	[HideModuleName]
	[StandardModule]
	internal sealed class MyWpfExtension
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002447 File Offset: 0x00000647
		internal static Application RunParser()
		{
			return (Application)Application.Current;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002453 File Offset: 0x00000653
		internal static Computer ManageParser()
		{
			return MyWpfExtension._Parser.StopTests();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000245F File Offset: 0x0000065F
		internal static User AwakeParser()
		{
			return MyWpfExtension._Broadcaster.StopTests();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000246B File Offset: 0x0000066B
		internal static Log WriteParser()
		{
			return MyWpfExtension.reader.StopTests();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002477 File Offset: 0x00000677
		internal static MyWpfExtension.MyWindows ComputeParser()
		{
			return MyWpfExtension.m_Field.StopTests();
		}

		// Token: 0x04000002 RID: 2
		private static MyProject.ThreadSafeObjectProvider<Computer> _Parser = new MyProject.ThreadSafeObjectProvider<Computer>();

		// Token: 0x04000003 RID: 3
		private static MyProject.ThreadSafeObjectProvider<User> _Broadcaster = new MyProject.ThreadSafeObjectProvider<User>();

		// Token: 0x04000004 RID: 4
		private static MyProject.ThreadSafeObjectProvider<MyWpfExtension.MyWindows> m_Field = new MyProject.ThreadSafeObjectProvider<MyWpfExtension.MyWindows>();

		// Token: 0x04000005 RID: 5
		private static MyProject.ThreadSafeObjectProvider<Log> reader = new MyProject.ThreadSafeObjectProvider<Log>();

		// Token: 0x02000013 RID: 19
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MyGroupCollection("System.Windows.Window", "Create__Instance__", "Dispose__Instance__", "My.MyWpfExtenstionModule.Windows")]
		internal sealed class MyWindows
		{
			// Token: 0x06000052 RID: 82 RVA: 0x0000E39C File Offset: 0x0000C59C
			private static T Create__Instance__<T>(T Instance) where T : Window, new()
			{
				T result;
				if (Instance == null)
				{
					if (MyWpfExtension.MyWindows.factoryRepository != null)
					{
						if (MyWpfExtension.MyWindows.factoryRepository.ContainsKey(typeof(!!0)))
						{
							throw new InvalidOperationException("The window cannot be accessed via My.Windows from the Window constructor.");
						}
					}
					else
					{
						MyWpfExtension.MyWindows.factoryRepository = new Hashtable();
					}
					MyWpfExtension.MyWindows.factoryRepository.Add(typeof(!!0), null);
					result = Activator.CreateInstance<T>();
				}
				else
				{
					result = Instance;
				}
				return result;
			}

			// Token: 0x06000053 RID: 83 RVA: 0x00002483 File Offset: 0x00000683
			private void Dispose__Instance__<T>(ref T instance) where T : Window
			{
				instance = default(!!0);
			}

			// Token: 0x06000054 RID: 84 RVA: 0x00002411 File Offset: 0x00000611
			[EditorBrowsable(EditorBrowsableState.Never)]
			public MyWindows()
			{
			}

			// Token: 0x06000055 RID: 85 RVA: 0x0000248C File Offset: 0x0000068C
			[EditorBrowsable(EditorBrowsableState.Never)]
			public override bool Equals(object o)
			{
				return base.Equals(RuntimeHelpers.GetObjectValue(o));
			}

			// Token: 0x06000056 RID: 86 RVA: 0x0000249A File Offset: 0x0000069A
			[EditorBrowsable(EditorBrowsableState.Never)]
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x06000057 RID: 87 RVA: 0x000024A2 File Offset: 0x000006A2
			[EditorBrowsable(EditorBrowsableState.Never)]
			internal new Type GetType()
			{
				return typeof(MyWpfExtension.MyWindows);
			}

			// Token: 0x06000058 RID: 88 RVA: 0x000024AF File Offset: 0x000006AF
			[EditorBrowsable(EditorBrowsableState.Never)]
			public override string ToString()
			{
				return base.ToString();
			}

			// Token: 0x06000059 RID: 89 RVA: 0x000024B7 File Offset: 0x000006B7
			public FormMain DisableTests()
			{
				this._ExporterRepository = MyWpfExtension.MyWindows.Create__Instance__<FormMain>(this._ExporterRepository);
				return this._ExporterRepository;
			}

			// Token: 0x0600005A RID: 90 RVA: 0x000024D0 File Offset: 0x000006D0
			public void InitTests(FormMain Value)
			{
				if (Value != this._ExporterRepository)
				{
					if (Value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					this.Dispose__Instance__<FormMain>(ref this._ExporterRepository);
				}
			}

			// Token: 0x04000006 RID: 6
			[ThreadStatic]
			private static Hashtable factoryRepository;

			// Token: 0x04000007 RID: 7
			[EditorBrowsable(EditorBrowsableState.Never)]
			public FormMain _ExporterRepository;
		}
	}
}
