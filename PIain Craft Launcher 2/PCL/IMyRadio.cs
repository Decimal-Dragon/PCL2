using System;

namespace PCL
{
	// Token: 0x02000016 RID: 22
	public interface IMyRadio
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600006D RID: 109
		// (remove) Token: 0x0600006E RID: 110
		event IMyRadio.CheckEventHandler Check;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600006F RID: 111
		// (remove) Token: 0x06000070 RID: 112
		event IMyRadio.ChangedEventHandler Changed;

		// Token: 0x02000017 RID: 23
		// (Invoke) Token: 0x06000074 RID: 116
		public delegate void CheckEventHandler(object sender, ModBase.RouteEventArgs e);

		// Token: 0x02000018 RID: 24
		// (Invoke) Token: 0x06000079 RID: 121
		public delegate void ChangedEventHandler(object sender, ModBase.RouteEventArgs e);
	}
}
