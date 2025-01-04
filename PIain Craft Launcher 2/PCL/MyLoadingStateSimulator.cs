using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace PCL
{
	// Token: 0x0200009C RID: 156
	public class MyLoadingStateSimulator : ILoadingTrigger
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x000046E5 File Offset: 0x000028E5
		public MyLoadingStateSimulator()
		{
			this.CustomizeField(MyLoading.MyLoadingState.Unloaded);
			this.IsLoader = 0;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000046FC File Offset: 0x000028FC
		[CompilerGenerated]
		private MyLoading.MyLoadingState MoveField()
		{
			return this.advisorBroadcaster;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00004704 File Offset: 0x00002904
		[CompilerGenerated]
		private void CustomizeField(MyLoading.MyLoadingState AutoPropertyValue)
		{
			this.advisorBroadcaster = AutoPropertyValue;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000470D File Offset: 0x0000290D
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x00028610 File Offset: 0x00026810
		public MyLoading.MyLoadingState LoadingState
		{
			get
			{
				return this.MoveField();
			}
			set
			{
				if (this.MoveField() != value)
				{
					MyLoading.MyLoadingState oldState = this.MoveField();
					this.CustomizeField(value);
					ILoadingTrigger.LoadingStateChangedEventHandler queueBroadcaster = this._QueueBroadcaster;
					if (queueBroadcaster != null)
					{
						queueBroadcaster(value, oldState);
					}
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00004715 File Offset: 0x00002915
		public bool IsLoader { get; }

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600046F RID: 1135 RVA: 0x00028648 File Offset: 0x00026848
		// (remove) Token: 0x06000470 RID: 1136 RVA: 0x00028680 File Offset: 0x00026880
		public event ILoadingTrigger.LoadingStateChangedEventHandler LoadingStateChanged
		{
			[CompilerGenerated]
			add
			{
				ILoadingTrigger.LoadingStateChangedEventHandler loadingStateChangedEventHandler = this._QueueBroadcaster;
				ILoadingTrigger.LoadingStateChangedEventHandler loadingStateChangedEventHandler2;
				do
				{
					loadingStateChangedEventHandler2 = loadingStateChangedEventHandler;
					ILoadingTrigger.LoadingStateChangedEventHandler value2 = (ILoadingTrigger.LoadingStateChangedEventHandler)Delegate.Combine(loadingStateChangedEventHandler2, value);
					loadingStateChangedEventHandler = Interlocked.CompareExchange<ILoadingTrigger.LoadingStateChangedEventHandler>(ref this._QueueBroadcaster, value2, loadingStateChangedEventHandler2);
				}
				while (loadingStateChangedEventHandler != loadingStateChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ILoadingTrigger.LoadingStateChangedEventHandler loadingStateChangedEventHandler = this._QueueBroadcaster;
				ILoadingTrigger.LoadingStateChangedEventHandler loadingStateChangedEventHandler2;
				do
				{
					loadingStateChangedEventHandler2 = loadingStateChangedEventHandler;
					ILoadingTrigger.LoadingStateChangedEventHandler value2 = (ILoadingTrigger.LoadingStateChangedEventHandler)Delegate.Remove(loadingStateChangedEventHandler2, value);
					loadingStateChangedEventHandler = Interlocked.CompareExchange<ILoadingTrigger.LoadingStateChangedEventHandler>(ref this._QueueBroadcaster, value2, loadingStateChangedEventHandler2);
				}
				while (loadingStateChangedEventHandler != loadingStateChangedEventHandler2);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000471 RID: 1137 RVA: 0x000286B8 File Offset: 0x000268B8
		// (remove) Token: 0x06000472 RID: 1138 RVA: 0x000286F0 File Offset: 0x000268F0
		public event ILoadingTrigger.ProgressChangedEventHandler ProgressChanged
		{
			[CompilerGenerated]
			add
			{
				ILoadingTrigger.ProgressChangedEventHandler progressChangedEventHandler = this._EventBroadcaster;
				ILoadingTrigger.ProgressChangedEventHandler progressChangedEventHandler2;
				do
				{
					progressChangedEventHandler2 = progressChangedEventHandler;
					ILoadingTrigger.ProgressChangedEventHandler value2 = (ILoadingTrigger.ProgressChangedEventHandler)Delegate.Combine(progressChangedEventHandler2, value);
					progressChangedEventHandler = Interlocked.CompareExchange<ILoadingTrigger.ProgressChangedEventHandler>(ref this._EventBroadcaster, value2, progressChangedEventHandler2);
				}
				while (progressChangedEventHandler != progressChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ILoadingTrigger.ProgressChangedEventHandler progressChangedEventHandler = this._EventBroadcaster;
				ILoadingTrigger.ProgressChangedEventHandler progressChangedEventHandler2;
				do
				{
					progressChangedEventHandler2 = progressChangedEventHandler;
					ILoadingTrigger.ProgressChangedEventHandler value2 = (ILoadingTrigger.ProgressChangedEventHandler)Delegate.Remove(progressChangedEventHandler2, value);
					progressChangedEventHandler = Interlocked.CompareExchange<ILoadingTrigger.ProgressChangedEventHandler>(ref this._EventBroadcaster, value2, progressChangedEventHandler2);
				}
				while (progressChangedEventHandler != progressChangedEventHandler2);
			}
		}

		// Token: 0x04000262 RID: 610
		[CompilerGenerated]
		private MyLoading.MyLoadingState advisorBroadcaster;

		// Token: 0x04000263 RID: 611
		[CompilerGenerated]
		private bool _AccountBroadcaster;

		// Token: 0x04000264 RID: 612
		[CompilerGenerated]
		private ILoadingTrigger.LoadingStateChangedEventHandler _QueueBroadcaster;

		// Token: 0x04000265 RID: 613
		[CompilerGenerated]
		private ILoadingTrigger.ProgressChangedEventHandler _EventBroadcaster;
	}
}
