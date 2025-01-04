using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL.My.Resources
{
	// Token: 0x02000014 RID: 20
	[CompilerGenerated]
	[HideModuleName]
	[StandardModule]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	internal sealed class Resources
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000024F5 File Offset: 0x000006F5
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager CloneParser
		{
			get
			{
				if (object.ReferenceEquals(Resources._Client, null))
				{
					Resources._Client = new ResourceManager("PCL.Resources", typeof(Resources).Assembly);
				}
				return Resources._Client;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002528 File Offset: 0x00000728
		// (set) Token: 0x0600005E RID: 94 RVA: 0x0000252F File Offset: 0x0000072F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo InvokeParser
		{
			get
			{
				return Resources.m_Config;
			}
			set
			{
				Resources.m_Config = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002537 File Offset: 0x00000737
		internal static byte[] Custom
		{
			get
			{
				return (byte[])RuntimeHelpers.GetObjectValue(Resources.CloneParser.GetObject("Custom", Resources.m_Config));
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002557 File Offset: 0x00000757
		internal static byte[] Dialogs
		{
			get
			{
				return (byte[])RuntimeHelpers.GetObjectValue(Resources.CloneParser.GetObject("Dialogs", Resources.m_Config));
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002577 File Offset: 0x00000777
		internal static byte[] ForgeInstaller
		{
			get
			{
				return (byte[])RuntimeHelpers.GetObjectValue(Resources.CloneParser.GetObject("ForgeInstaller", Resources.m_Config));
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002597 File Offset: 0x00000797
		internal static byte[] Help
		{
			get
			{
				return (byte[])RuntimeHelpers.GetObjectValue(Resources.CloneParser.GetObject("Help", Resources.m_Config));
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000025B7 File Offset: 0x000007B7
		internal static byte[] Imazen_WebP
		{
			get
			{
				return (byte[])RuntimeHelpers.GetObjectValue(Resources.CloneParser.GetObject("Imazen_WebP", Resources.m_Config));
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000025D7 File Offset: 0x000007D7
		internal static byte[] JavaWrapper
		{
			get
			{
				return (byte[])RuntimeHelpers.GetObjectValue(Resources.CloneParser.GetObject("JavaWrapper", Resources.m_Config));
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000025F7 File Offset: 0x000007F7
		internal static byte[] Json
		{
			get
			{
				return (byte[])RuntimeHelpers.GetObjectValue(Resources.CloneParser.GetObject("Json", Resources.m_Config));
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002617 File Offset: 0x00000817
		internal static byte[] Byte_0
		{
			get
			{
				return (byte[])RuntimeHelpers.GetObjectValue(Resources.CloneParser.GetObject("libwebp64", Resources.m_Config));
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002637 File Offset: 0x00000837
		internal static byte[] ModData
		{
			get
			{
				return (byte[])RuntimeHelpers.GetObjectValue(Resources.CloneParser.GetObject("ModData", Resources.m_Config));
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002657 File Offset: 0x00000857
		internal static byte[] NAudio
		{
			get
			{
				return (byte[])RuntimeHelpers.GetObjectValue(Resources.CloneParser.GetObject("NAudio", Resources.m_Config));
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002677 File Offset: 0x00000877
		internal static byte[] Transformer
		{
			get
			{
				return (byte[])RuntimeHelpers.GetObjectValue(Resources.CloneParser.GetObject("Transformer", Resources.m_Config));
			}
		}

		// Token: 0x04000008 RID: 8
		private static ResourceManager _Client;

		// Token: 0x04000009 RID: 9
		private static CultureInfo m_Config;
	}
}
