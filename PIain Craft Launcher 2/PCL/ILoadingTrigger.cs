using System;

namespace PCL
{
	// Token: 0x02000099 RID: 153
	public interface ILoadingTrigger
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000458 RID: 1112
		bool IsLoader { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000459 RID: 1113
		// (set) Token: 0x0600045A RID: 1114
		MyLoading.MyLoadingState LoadingState { get; set; }

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600045B RID: 1115
		// (remove) Token: 0x0600045C RID: 1116
		event ILoadingTrigger.LoadingStateChangedEventHandler LoadingStateChanged;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600045D RID: 1117
		// (remove) Token: 0x0600045E RID: 1118
		event ILoadingTrigger.ProgressChangedEventHandler ProgressChanged;

		// Token: 0x0200009A RID: 154
		// (Invoke) Token: 0x06000462 RID: 1122
		public delegate void LoadingStateChangedEventHandler(MyLoading.MyLoadingState NewState, MyLoading.MyLoadingState OldState);

		// Token: 0x0200009B RID: 155
		// (Invoke) Token: 0x06000467 RID: 1127
		public delegate void ProgressChangedEventHandler(double NewProgress, double OldProgress);
	}
}
