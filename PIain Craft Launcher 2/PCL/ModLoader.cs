using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Shell;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x02000121 RID: 289
	[StandardModule]
	public sealed class ModLoader
	{
		// Token: 0x06000C92 RID: 3218 RVA: 0x0000859C File Offset: 0x0000679C
		public static void LoaderTaskbarAdd<T>(ModLoader.LoaderCombo<T> Loader)
		{
			if (ModMain._MessageIterator != null)
			{
				ModMain._MessageIterator.TaskRemove(Loader);
			}
			ModLoader.LoaderTaskbar.Add(Loader);
			ModBase.Log(string.Format("[Taskbar] {0} 已加入任务列表", Loader.Name), ModBase.LogLevel.Normal, "出现错误");
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00054480 File Offset: 0x00052680
		public static void LoaderTaskbarProgressRefresh()
		{
			try
			{
				double num = ModLoader.LoaderTaskbarProgressGet();
				bool flag = true;
				try
				{
					List<ModLoader.LoaderBase>.Enumerator enumerator = Enumerable.ToList<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar).GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.State == ModBase.LoadState.Loading)
						{
							flag = false;
						}
					}
				}
				finally
				{
					List<ModLoader.LoaderBase>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				try
				{
					foreach (ModLoader.LoaderBase loaderBase in Enumerable.ToList<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar))
					{
						if (flag || loaderBase.State == ModBase.LoadState.Aborted || loaderBase.State == ModBase.LoadState.Waiting)
						{
							if (ModMain._MessageIterator != null)
							{
								ModMain._MessageIterator.TaskRefresh(loaderBase);
							}
							ModLoader.LoaderTaskbar.Remove(loaderBase);
							ModBase.Log(string.Format("[Taskbar] {0} 已移出任务列表", loaderBase.Name), ModBase.LogLevel.Normal, "出现错误");
						}
					}
				}
				finally
				{
					List<ModLoader.LoaderBase>.Enumerator enumerator2;
					((IDisposable)enumerator2).Dispose();
				}
				if (num > 0.0 && num < 1.0 && ModLoader.LoaderTaskbarProgress <= num)
				{
					ModLoader.LoaderTaskbarProgress = ModLoader.LoaderTaskbarProgress * 0.9 + num * 0.1;
				}
				else
				{
					ModLoader.LoaderTaskbarProgress = num;
				}
				ModBase.RunInUi((ModLoader._Closure$__.$I8-0 == null) ? (ModLoader._Closure$__.$I8-0 = delegate()
				{
					ModMain._ProcessIterator.BtnExtraDownload.Progress = ModLoader.LoaderTaskbarProgress;
				}) : ModLoader._Closure$__.$I8-0, false);
				TaskbarItemProgressState taskbarItemProgressState;
				if (Enumerable.Any<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar))
				{
					if (ModLoader.LoaderTaskbarProgress != 1.0)
					{
						if (ModLoader.LoaderTaskbarProgress < 0.015)
						{
							taskbarItemProgressState = TaskbarItemProgressState.Indeterminate;
							goto IL_18B;
						}
						taskbarItemProgressState = TaskbarItemProgressState.Normal;
						ModMain._ProcessIterator.TaskbarItemInfo.ProgressValue = ModLoader.LoaderTaskbarProgress;
						goto IL_18B;
					}
				}
				taskbarItemProgressState = TaskbarItemProgressState.None;
				IL_18B:
				if (ModLoader.LoaderTaskbarProgressLast != taskbarItemProgressState)
				{
					ModLoader.LoaderTaskbarProgressLast = taskbarItemProgressState;
					ModMain._ProcessIterator.TaskbarItemInfo.ProgressState = taskbarItemProgressState;
					ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "刷新任务栏进度显示失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x000546B4 File Offset: 0x000528B4
		public static double LoaderTaskbarProgressGet()
		{
			double result;
			try
			{
				if (!Enumerable.Any<ModLoader.LoaderBase>(ModLoader.LoaderTaskbar))
				{
					result = 1.0;
				}
				else
				{
					result = ModBase.MathClamp(Enumerable.Average(Enumerable.Select<ModLoader.LoaderBase, double>(ModLoader.LoaderTaskbar, (ModLoader._Closure$__.$I9-0 == null) ? (ModLoader._Closure$__.$I9-0 = ((ModLoader.LoaderBase e) => e.Progress)) : ModLoader._Closure$__.$I9-0)), 0.0, 1.0);
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "获取任务栏进度出错", ModBase.LogLevel.Feedback, "出现错误");
				result = 0.5;
			}
			return result;
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00054760 File Offset: 0x00052960
		public static bool LoaderFolderRun(ModLoader.LoaderBase Loader, string FolderPath, ModLoader.LoaderFolderRunType Type, int MaxDepth = 0, string ExtraPath = "", bool WaitForExit = false)
		{
			ModLoader.LoaderFolderDictionaryEntry value = default(ModLoader.LoaderFolderDictionaryEntry);
			value.FolderPath = FolderPath + ExtraPath;
			value.LastCheckTime = null;
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(FolderPath + ExtraPath);
				value.LastCheckTime = (directoryInfo.Exists ? new DateTime?(ModLoader.GetActualLastWriteTimeUtc(directoryInfo, MaxDepth)) : ((DateTime?)null));
				if (Type == ModLoader.LoaderFolderRunType.RunOnUpdated && ModLoader.LoaderFolderDictionary.ContainsKey(Loader))
				{
					if (directoryInfo.Exists)
					{
						ModLoader.LoaderFolderDictionaryEntry loaderFolderDictionaryEntry = ModLoader.LoaderFolderDictionary[Loader];
						if (loaderFolderDictionaryEntry.LastCheckTime != null && value.Equals(ModLoader.LoaderFolderDictionary[Loader]))
						{
							return false;
						}
					}
					else
					{
						ModLoader.LoaderFolderDictionaryEntry loaderFolderDictionaryEntry = ModLoader.LoaderFolderDictionary[Loader];
						if (loaderFolderDictionaryEntry.LastCheckTime == null)
						{
							return false;
						}
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "文件夹加载器启动检测出错", ModBase.LogLevel.Debug, "出现错误");
			}
			ModLoader.LoaderFolderDictionary[Loader] = value;
			bool result;
			if (Type == ModLoader.LoaderFolderRunType.UpdateOnly)
			{
				result = false;
			}
			else
			{
				if (WaitForExit)
				{
					Loader.WaitForExit(FolderPath, null, true);
				}
				else
				{
					Loader.Start(FolderPath, true);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00054898 File Offset: 0x00052A98
		private static DateTime GetActualLastWriteTimeUtc(DirectoryInfo FolderInfo, int MaxDepth)
		{
			DateTime dateTime = FolderInfo.LastWriteTimeUtc;
			if (MaxDepth > 0)
			{
				try
				{
					foreach (DirectoryInfo folderInfo in FolderInfo.EnumerateDirectories())
					{
						DateTime actualLastWriteTimeUtc = ModLoader.GetActualLastWriteTimeUtc(folderInfo, checked(MaxDepth - 1));
						if (DateTime.Compare(actualLastWriteTimeUtc, dateTime) > 0)
						{
							dateTime = actualLastWriteTimeUtc;
						}
					}
				}
				finally
				{
					IEnumerator<DirectoryInfo> enumerator;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
			}
			return dateTime;
		}

		// Token: 0x04000658 RID: 1624
		public static ModBase.SafeList<ModLoader.LoaderBase> LoaderTaskbar = new ModBase.SafeList<ModLoader.LoaderBase>();

		// Token: 0x04000659 RID: 1625
		public static double LoaderTaskbarProgress = 0.0;

		// Token: 0x0400065A RID: 1626
		private static TaskbarItemProgressState LoaderTaskbarProgressLast = TaskbarItemProgressState.None;

		// Token: 0x0400065B RID: 1627
		private static Dictionary<ModLoader.LoaderBase, ModLoader.LoaderFolderDictionaryEntry> LoaderFolderDictionary = new Dictionary<ModLoader.LoaderBase, ModLoader.LoaderFolderDictionaryEntry>();

		// Token: 0x02000122 RID: 290
		public abstract class LoaderBase : ILoadingTrigger
		{
			// Token: 0x06000C97 RID: 3223 RVA: 0x00054900 File Offset: 0x00052B00
			protected LoaderBase()
			{
				this.IsLoader = 1;
				this.Uuid = ModBase.GetUuid();
				this.Name = "未命名任务 " + Conversions.ToString(this.Uuid) + "#";
				this.LockState = RuntimeHelpers.GetObjectValue(new object());
				this.Parent = null;
				this.HasOnStateChangedThread = false;
				this._State = ModBase.LoadState.Waiting;
				this._LoadingState = MyLoading.MyLoadingState.Stop;
				this.Error = null;
				this.Block = true;
				this.Show = true;
				this.IsForceRestarting = false;
				this._Progress = -1.0;
				this.ProgressWeight = 1.0;
			}

			// Token: 0x170001FE RID: 510
			// (get) Token: 0x06000C98 RID: 3224 RVA: 0x000085D6 File Offset: 0x000067D6
			public bool IsLoader { get; }

			// Token: 0x170001FF RID: 511
			// (get) Token: 0x06000C99 RID: 3225 RVA: 0x000549AC File Offset: 0x00052BAC
			public ModLoader.LoaderBase RealParent
			{
				get
				{
					ModLoader.LoaderBase loaderBase;
					try
					{
						loaderBase = this.Parent;
						while (loaderBase != null && loaderBase.Parent != null)
						{
							loaderBase = loaderBase.Parent;
						}
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "获取父加载器失败（" + this.Name + "）", ModBase.LogLevel.Feedback, "出现错误");
						loaderBase = null;
					}
					return loaderBase;
				}
			}

			// Token: 0x06000C9A RID: 3226 RVA: 0x000085DE File Offset: 0x000067DE
			public virtual void InitParent(ModLoader.LoaderBase Parent)
			{
				this.Parent = Parent;
			}

			// Token: 0x1400000D RID: 13
			// (add) Token: 0x06000C9B RID: 3227 RVA: 0x00054A18 File Offset: 0x00052C18
			// (remove) Token: 0x06000C9C RID: 3228 RVA: 0x00054A50 File Offset: 0x00052C50
			public event ModLoader.LoaderBase.OnStateChangedThreadEventHandler OnStateChangedThread;

			// Token: 0x1400000E RID: 14
			// (add) Token: 0x06000C9D RID: 3229 RVA: 0x00054A88 File Offset: 0x00052C88
			// (remove) Token: 0x06000C9E RID: 3230 RVA: 0x00054AC0 File Offset: 0x00052CC0
			public event ModLoader.LoaderBase.OnStateChangedUiEventHandler OnStateChangedUi;

			// Token: 0x17000200 RID: 512
			// (set) Token: 0x06000C9F RID: 3231 RVA: 0x00054AF8 File Offset: 0x00052CF8
			public Action<ModLoader.LoaderBase> OnStateChanged
			{
				set
				{
					this.OnStateChangedUi += delegate(ModLoader.LoaderBase Loader, ModBase.LoadState NewState, ModBase.LoadState OldState)
					{
						value(Loader);
					};
				}
			}

			// Token: 0x1400000F RID: 15
			// (add) Token: 0x06000CA0 RID: 3232 RVA: 0x00054B24 File Offset: 0x00052D24
			// (remove) Token: 0x06000CA1 RID: 3233 RVA: 0x00054B5C File Offset: 0x00052D5C
			public event ModLoader.LoaderBase.PreviewFinishEventHandler PreviewFinish;

			// Token: 0x06000CA2 RID: 3234 RVA: 0x00054B94 File Offset: 0x00052D94
			protected void RaisePreviewFinish()
			{
				ModLoader.LoaderBase.PreviewFinishEventHandler previewFinishEvent = this.PreviewFinishEvent;
				if (previewFinishEvent != null)
				{
					previewFinishEvent(this);
				}
			}

			// Token: 0x17000201 RID: 513
			// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x000085E7 File Offset: 0x000067E7
			// (set) Token: 0x06000CA4 RID: 3236 RVA: 0x00054BB4 File Offset: 0x00052DB4
			public ModBase.LoadState State
			{
				get
				{
					return this._State;
				}
				set
				{
					if (this._State != value)
					{
						ModBase.LoadState OldState = this._State;
						if (Conversions.ToBoolean(value == ModBase.LoadState.Finished && Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("SystemDebugDelay", null))))
						{
							Thread.Sleep(ModBase.RandomInteger(100, 2000));
						}
						this._State = value;
						ModBase.Log("[Loader] 加载器 " + this.Name + " 状态改变：" + ModBase.GetStringFromEnum(value), ModBase.LogLevel.Normal, "出现错误");
						ModBase.RunInUi(delegate()
						{
							ModBase.LoadState $VB$Local_value = value;
							if ($VB$Local_value != ModBase.LoadState.Loading)
							{
								if ($VB$Local_value != ModBase.LoadState.Failed)
								{
									this.LoadingState = MyLoading.MyLoadingState.Stop;
								}
								else
								{
									this.LoadingState = MyLoading.MyLoadingState.Error;
								}
							}
							else
							{
								this.LoadingState = MyLoading.MyLoadingState.Run;
							}
							ModLoader.LoaderBase.OnStateChangedUiEventHandler onStateChangedUiEvent = this.OnStateChangedUiEvent;
							if (onStateChangedUiEvent != null)
							{
								onStateChangedUiEvent(this, value, OldState);
							}
						}, false);
						if (this.HasOnStateChangedThread)
						{
							ModBase.RunInThread(delegate
							{
								ModLoader.LoaderBase.OnStateChangedThreadEventHandler onStateChangedThreadEvent = this.OnStateChangedThreadEvent;
								if (onStateChangedThreadEvent != null)
								{
									onStateChangedThreadEvent(this, value, OldState);
								}
							});
						}
					}
				}
			}

			// Token: 0x17000202 RID: 514
			// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x000085EF File Offset: 0x000067EF
			// (set) Token: 0x06000CA6 RID: 3238 RVA: 0x00054C98 File Offset: 0x00052E98
			public MyLoading.MyLoadingState LoadingState
			{
				get
				{
					return this._LoadingState;
				}
				set
				{
					if (this._LoadingState != value)
					{
						MyLoading.MyLoadingState loadingState = this._LoadingState;
						this._LoadingState = value;
						ILoadingTrigger.LoadingStateChangedEventHandler loadingStateChangedEvent = this.LoadingStateChangedEvent;
						if (loadingStateChangedEvent != null)
						{
							loadingStateChangedEvent(value, loadingState);
						}
					}
				}
			}

			// Token: 0x14000010 RID: 16
			// (add) Token: 0x06000CA7 RID: 3239 RVA: 0x00054CD0 File Offset: 0x00052ED0
			// (remove) Token: 0x06000CA8 RID: 3240 RVA: 0x00054D08 File Offset: 0x00052F08
			public event ILoadingTrigger.LoadingStateChangedEventHandler LoadingStateChanged;

			// Token: 0x17000203 RID: 515
			// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x000085F7 File Offset: 0x000067F7
			// (set) Token: 0x06000CAA RID: 3242 RVA: 0x000085FF File Offset: 0x000067FF
			public Exception Error { get; set; }

			// Token: 0x17000204 RID: 516
			// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00054D40 File Offset: 0x00052F40
			// (set) Token: 0x06000CAC RID: 3244 RVA: 0x00054D98 File Offset: 0x00052F98
			public virtual double Progress
			{
				get
				{
					ModBase.LoadState state = this.State;
					double result;
					if (state != ModBase.LoadState.Waiting)
					{
						if (state != ModBase.LoadState.Loading)
						{
							result = 1.0;
						}
						else
						{
							result = ((this._Progress == -1.0) ? 0.02 : this._Progress);
						}
					}
					else
					{
						result = 0.0;
					}
					return result;
				}
				set
				{
					if (this._Progress != value)
					{
						double progress = this._Progress;
						this._Progress = value;
						ILoadingTrigger.ProgressChangedEventHandler progressChangedEvent = this.ProgressChangedEvent;
						if (progressChangedEvent != null)
						{
							progressChangedEvent(value, progress);
						}
					}
				}
			}

			// Token: 0x14000011 RID: 17
			// (add) Token: 0x06000CAD RID: 3245 RVA: 0x00054DD0 File Offset: 0x00052FD0
			// (remove) Token: 0x06000CAE RID: 3246 RVA: 0x00054E08 File Offset: 0x00053008
			public event ILoadingTrigger.ProgressChangedEventHandler ProgressChanged;

			// Token: 0x17000205 RID: 517
			// (get) Token: 0x06000CAF RID: 3247 RVA: 0x00008608 File Offset: 0x00006808
			// (set) Token: 0x06000CB0 RID: 3248 RVA: 0x00008610 File Offset: 0x00006810
			public double ProgressWeight { get; set; }

			// Token: 0x06000CB1 RID: 3249
			public abstract void Start(object Input = null, bool IsForceRestart = false);

			// Token: 0x06000CB2 RID: 3250
			public abstract void Abort();

			// Token: 0x06000CB3 RID: 3251 RVA: 0x00054E40 File Offset: 0x00053040
			public void WaitForExit(object Input = null, ModLoader.LoaderBase LoaderToSyncProgress = null, bool IsForceRestart = false)
			{
				this.Start(RuntimeHelpers.GetObjectValue(Input), IsForceRestart);
				while (this.State == ModBase.LoadState.Loading)
				{
					if (LoaderToSyncProgress != null)
					{
						LoaderToSyncProgress.Progress = this.Progress;
					}
					Thread.Sleep(10);
				}
				if (this.State == ModBase.LoadState.Finished)
				{
					return;
				}
				if (this.State == ModBase.LoadState.Aborted)
				{
					throw new ThreadInterruptedException("加载器执行已中断。");
				}
				if (Information.IsNothing(this.Error))
				{
					throw new Exception("未知错误！");
				}
				throw new Exception(this.Error.Message, this.Error);
			}

			// Token: 0x06000CB4 RID: 3252 RVA: 0x00054EC8 File Offset: 0x000530C8
			public void WaitForExitTime(int Timeout, object Input = null, string TimeoutMessage = "等待加载器执行超时。", object LoaderToSyncProgress = null, bool IsForceRestart = false)
			{
				this.Start(RuntimeHelpers.GetObjectValue(Input), IsForceRestart);
				checked
				{
					while (this.State == ModBase.LoadState.Loading)
					{
						if (LoaderToSyncProgress != null)
						{
							NewLateBinding.LateSet(LoaderToSyncProgress, null, "Progress", new object[]
							{
								this.Progress
							}, null, null);
						}
						Thread.Sleep(10);
						Timeout -= 10;
						if (Timeout < 0)
						{
							throw new TimeoutException(TimeoutMessage);
						}
					}
					if (this.State == ModBase.LoadState.Finished)
					{
						return;
					}
					if (this.State == ModBase.LoadState.Aborted)
					{
						throw new ThreadInterruptedException("加载器执行已中断。");
					}
					if (Information.IsNothing(this.Error))
					{
						throw new Exception("未知错误！");
					}
					throw this.Error;
				}
			}

			// Token: 0x06000CB5 RID: 3253 RVA: 0x00054F6C File Offset: 0x0005316C
			public override bool Equals(object obj)
			{
				ModLoader.LoaderBase loaderBase = obj as ModLoader.LoaderBase;
				return loaderBase != null && this.Uuid == loaderBase.Uuid;
			}

			// Token: 0x0400065D RID: 1629
			public int Uuid;

			// Token: 0x0400065E RID: 1630
			public string Name;

			// Token: 0x0400065F RID: 1631
			public readonly object LockState;

			// Token: 0x04000660 RID: 1632
			public ModLoader.LoaderBase Parent;

			// Token: 0x04000662 RID: 1634
			public bool HasOnStateChangedThread;

			// Token: 0x04000665 RID: 1637
			private ModBase.LoadState _State;

			// Token: 0x04000666 RID: 1638
			private MyLoading.MyLoadingState _LoadingState;

			// Token: 0x04000669 RID: 1641
			public bool Block;

			// Token: 0x0400066A RID: 1642
			public bool Show;

			// Token: 0x0400066B RID: 1643
			public bool IsForceRestarting;

			// Token: 0x0400066C RID: 1644
			private double _Progress;

			// Token: 0x02000123 RID: 291
			// (Invoke) Token: 0x06000CBA RID: 3258
			public delegate void OnStateChangedThreadEventHandler(ModLoader.LoaderBase Loader, ModBase.LoadState NewState, ModBase.LoadState OldState);

			// Token: 0x02000124 RID: 292
			// (Invoke) Token: 0x06000CBF RID: 3263
			public delegate void OnStateChangedUiEventHandler(ModLoader.LoaderBase Loader, ModBase.LoadState NewState, ModBase.LoadState OldState);

			// Token: 0x02000125 RID: 293
			// (Invoke) Token: 0x06000CC4 RID: 3268
			public delegate void PreviewFinishEventHandler(ModLoader.LoaderBase Loader);
		}

		// Token: 0x02000128 RID: 296
		public class LoaderTask<InputType, OutputType> : ModLoader.LoaderBase
		{
			// Token: 0x17000206 RID: 518
			// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00008627 File Offset: 0x00006827
			public bool IsAborted
			{
				get
				{
					return this.IsAbortedWithThread(Thread.CurrentThread);
				}
			}

			// Token: 0x06000CCE RID: 3278 RVA: 0x00008634 File Offset: 0x00006834
			public bool IsAbortedWithThread(Thread Thread)
			{
				return this.LastRunningThread == null || !object.ReferenceEquals(Thread, this.LastRunningThread) || base.State == ModBase.LoadState.Aborted;
			}

			// Token: 0x06000CCF RID: 3279 RVA: 0x00055034 File Offset: 0x00053234
			public InputType StartGetInput(InputType Input = default(InputType), Func<object> InputDelegate = null)
			{
				ModLoader.LoaderTask<InputType, OutputType>._Closure$__11-0 CS$<>8__locals1 = new ModLoader.LoaderTask<!0, !1>._Closure$__11-0();
				CS$<>8__locals1.$VB$Local_Input = Input;
				CS$<>8__locals1.$VB$Local_InputDelegate = InputDelegate;
				if (CS$<>8__locals1.$VB$Local_InputDelegate == null)
				{
					CS$<>8__locals1.$VB$Local_InputDelegate = this.InputDelegate;
				}
				InputType inputType = default(!0);
				if (CS$<>8__locals1.$VB$Local_Input != null)
				{
					if (inputType == null)
					{
						goto IL_8F;
					}
					ModLoader.LoaderTask<InputType, OutputType>._Closure$__11-0 CS$<>8__locals2 = CS$<>8__locals1;
					ref !0 ptr = ref CS$<>8__locals2.$VB$Local_Input;
					if (default(!0) == null)
					{
						InputType $VB$Local_Input = CS$<>8__locals2.$VB$Local_Input;
						ptr = ref $VB$Local_Input;
					}
					if (!ptr.Equals(inputType))
					{
						goto IL_8F;
					}
				}
				if (CS$<>8__locals1.$VB$Local_InputDelegate != null)
				{
					ModBase.RunInUiWait(delegate()
					{
						CS$<>8__locals1.$VB$Local_Input = Conversions.ToGenericParameter<InputType>(CS$<>8__locals1.$VB$Local_InputDelegate());
					});
				}
				IL_8F:
				return CS$<>8__locals1.$VB$Local_Input;
			}

			// Token: 0x06000CD0 RID: 3280 RVA: 0x000550D8 File Offset: 0x000532D8
			public bool ShouldStart(ref object Input, bool IsForceRestart = false, bool IgnoreReloadTimeout = false)
			{
				try
				{
					Input = this.StartGetInput(Conversions.ToGenericParameter<InputType>(Input), null);
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "加载输入获取失败（" + this.Name + "）", ModBase.LogLevel.Hint, "出现错误");
					base.Error = ex;
					object lockState = this.LockState;
					ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
					lock (lockState)
					{
						base.State = ModBase.LoadState.Failed;
					}
				}
				return IsForceRestart || Input == null != (this.Input == null) || (Input != null && !Input.Equals(this.Input)) || ((base.State != ModBase.LoadState.Loading && base.State != ModBase.LoadState.Finished) || (!IgnoreReloadTimeout && this.ReloadTimeout != -1 && this.LastFinishedTime != 0L && checked(ModBase.GetTimeTick() - this.LastFinishedTime) >= (long)this.ReloadTimeout));
			}

			// Token: 0x06000CD1 RID: 3281 RVA: 0x000551F4 File Offset: 0x000533F4
			public override void Start(object Input = null, bool IsForceRestart = false)
			{
				if (this.ShouldStart(ref Input, IsForceRestart, false))
				{
					if (base.State == ModBase.LoadState.Loading)
					{
						this.TriggerThreadAbort();
					}
					this.Input = Conversions.ToGenericParameter<InputType>(Input);
					object lockState = this.LockState;
					ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
					lock (lockState)
					{
						base.State = ModBase.LoadState.Loading;
						this.Progress = -1.0;
					}
					this.LastRunningThread = new Thread(delegate()
					{
						try
						{
							this.IsForceRestarting = IsForceRestart;
							if (ModBase._TokenRepository)
							{
								ModBase.Log(string.Concat(new string[]
								{
									"[Loader] 加载线程 ",
									this.Name,
									" (",
									Conversions.ToString(Thread.CurrentThread.ManagedThreadId),
									") 已",
									this.IsForceRestarting ? "强制" : "",
									"启动"
								}), ModBase.LogLevel.Normal, "出现错误");
							}
							this.LoadDelegate(this);
							if (this.IsAborted)
							{
								ModBase.Log(string.Concat(new string[]
								{
									"[Loader] 加载线程 ",
									this.Name,
									" (",
									Conversions.ToString(Thread.CurrentThread.ManagedThreadId),
									") 已中断但线程正常运行至结束，输出被弃用（最新线程：",
									Conversions.ToString((this.LastRunningThread == null) ? -1 : this.LastRunningThread.ManagedThreadId),
									"）"
								}), ModBase.LogLevel.Developer, "出现错误");
							}
							else
							{
								this.RaisePreviewFinish();
								this.State = ModBase.LoadState.Finished;
								this.LastFinishedTime = ModBase.GetTimeTick();
							}
						}
						catch (ThreadInterruptedException ex)
						{
							if (ModBase._TokenRepository)
							{
								ModBase.Log(ex, string.Concat(new string[]
								{
									"加载线程 ",
									this.Name,
									" (",
									Conversions.ToString(Thread.CurrentThread.ManagedThreadId),
									") 已触发线程中断"
								}), ModBase.LogLevel.Debug, "出现错误");
							}
							if (!this.IsAborted)
							{
								this.State = ModBase.LoadState.Aborted;
							}
						}
						catch (Exception ex2)
						{
							if (!this.IsAborted)
							{
								ModBase.Log(ex2, string.Concat(new string[]
								{
									"加载线程 ",
									this.Name,
									" (",
									Conversions.ToString(Thread.CurrentThread.ManagedThreadId),
									") 发生运行时错误"
								}), ModBase.LogLevel.Developer, "出现错误");
								this.Error = ex2;
								this.State = ModBase.LoadState.Failed;
							}
						}
					})
					{
						Name = this.Name,
						Priority = this.ThreadPriority
					};
					this.LastRunningThread.Start();
				}
			}

			// Token: 0x06000CD2 RID: 3282 RVA: 0x000552C8 File Offset: 0x000534C8
			public override void Abort()
			{
				if (base.State == ModBase.LoadState.Loading)
				{
					object lockState = this.LockState;
					ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
					lock (lockState)
					{
						base.State = ModBase.LoadState.Aborted;
					}
					this.TriggerThreadAbort();
				}
			}

			// Token: 0x06000CD3 RID: 3283 RVA: 0x00055320 File Offset: 0x00053520
			private void TriggerThreadAbort()
			{
				if (this.LastRunningThread != null)
				{
					if (ModBase._TokenRepository)
					{
						ModBase.Log(string.Concat(new string[]
						{
							"[Loader] 加载线程 ",
							this.Name,
							" (",
							Conversions.ToString(this.LastRunningThread.ManagedThreadId),
							") 已中断"
						}), ModBase.LogLevel.Normal, "出现错误");
					}
					if (this.LastRunningThread.IsAlive)
					{
						this.LastRunningThread.Interrupt();
					}
					this.LastRunningThread = null;
				}
			}

			// Token: 0x06000CD4 RID: 3284 RVA: 0x00008657 File Offset: 0x00006857
			public LoaderTask()
			{
				this.ReloadTimeout = -1;
				this.LastFinishedTime = 0L;
				this.LastRunningThread = null;
				this.Input = default(!0);
				this.Output = default(!1);
			}

			// Token: 0x06000CD5 RID: 3285 RVA: 0x000553A8 File Offset: 0x000535A8
			public LoaderTask(string Name, Action<ModLoader.LoaderTask<InputType, OutputType>> LoadDelegate, Func<InputType> InputDelegate = null, ThreadPriority Priority = ThreadPriority.Normal)
			{
				this.ReloadTimeout = -1;
				this.LastFinishedTime = 0L;
				this.LastRunningThread = null;
				this.Input = default(!0);
				this.Output = default(!1);
				this.Name = Name;
				this.LoadDelegate = LoadDelegate;
				this.InputDelegate = InputDelegate;
				this.ThreadPriority = Priority;
			}

			// Token: 0x04000673 RID: 1651
			protected internal ThreadPriority ThreadPriority;

			// Token: 0x04000674 RID: 1652
			protected internal Action<ModLoader.LoaderTask<InputType, OutputType>> LoadDelegate;

			// Token: 0x04000675 RID: 1653
			protected internal Func<object> InputDelegate;

			// Token: 0x04000676 RID: 1654
			public int ReloadTimeout;

			// Token: 0x04000677 RID: 1655
			public long LastFinishedTime;

			// Token: 0x04000678 RID: 1656
			public Thread LastRunningThread;

			// Token: 0x04000679 RID: 1657
			public InputType Input;

			// Token: 0x0400067A RID: 1658
			public OutputType Output;
		}

		// Token: 0x0200012B RID: 299
		public class LoaderCombo<InputType> : ModLoader.LoaderBase
		{
			// Token: 0x17000207 RID: 519
			// (get) Token: 0x06000CDD RID: 3293 RVA: 0x000556B8 File Offset: 0x000538B8
			// (set) Token: 0x06000CDE RID: 3294 RVA: 0x000086AD File Offset: 0x000068AD
			public override double Progress
			{
				get
				{
					ModBase.LoadState state = base.State;
					double result;
					if (state != ModBase.LoadState.Waiting)
					{
						if (state != ModBase.LoadState.Loading)
						{
							result = 1.0;
						}
						else
						{
							double num = 0.0;
							double num2 = 0.0;
							try
							{
								foreach (ModLoader.LoaderBase loaderBase in this.Loaders)
								{
									num += loaderBase.ProgressWeight;
									num2 += loaderBase.ProgressWeight * loaderBase.Progress;
								}
							}
							finally
							{
								List<ModLoader.LoaderBase>.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
							if (num == 0.0)
							{
								result = 0.0;
							}
							else
							{
								result = num2 / num;
							}
						}
					}
					else
					{
						result = 0.0;
					}
					return result;
				}
				set
				{
					throw new Exception("多重加载器不支持设置进度");
				}
			}

			// Token: 0x06000CDF RID: 3295 RVA: 0x00055780 File Offset: 0x00053980
			public LoaderCombo(string Name, IEnumerable<ModLoader.LoaderBase> Loaders)
			{
				this.Loaders = new List<ModLoader.LoaderBase>();
				this.Loaders.Clear();
				try
				{
					foreach (ModLoader.LoaderBase loaderBase in Loaders)
					{
						if (loaderBase != null)
						{
							this.Loaders.Add(loaderBase);
							loaderBase.OnStateChangedThread += this.SubTaskStateChanged;
							loaderBase.HasOnStateChangedThread = true;
						}
					}
				}
				finally
				{
					IEnumerator<ModLoader.LoaderBase> enumerator;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
				this.InitParent(null);
				this.Name = Name;
			}

			// Token: 0x06000CE0 RID: 3296 RVA: 0x00055814 File Offset: 0x00053A14
			public override void InitParent(ModLoader.LoaderBase Parent)
			{
				this.Parent = Parent;
				try
				{
					foreach (ModLoader.LoaderBase loaderBase in this.Loaders)
					{
						loaderBase.InitParent(this);
					}
				}
				finally
				{
					List<ModLoader.LoaderBase>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}

			// Token: 0x06000CE1 RID: 3297 RVA: 0x0005586C File Offset: 0x00053A6C
			public override void Start(object Input = null, bool IsForceRestart = false)
			{
				this.IsForceRestarting = IsForceRestart;
				object lockState = this.LockState;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
				lock (lockState)
				{
					if (base.State == ModBase.LoadState.Loading)
					{
						return;
					}
					base.State = ModBase.LoadState.Loading;
				}
				this.Input = Conversions.ToGenericParameter<InputType>(Input);
				if (IsForceRestart)
				{
					try
					{
						foreach (ModLoader.LoaderBase loaderBase in this.Loaders)
						{
							loaderBase.State = ModBase.LoadState.Waiting;
						}
					}
					finally
					{
						List<ModLoader.LoaderBase>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
				ModBase.RunInThread(new Action(this.Update));
			}

			// Token: 0x06000CE2 RID: 3298 RVA: 0x00055928 File Offset: 0x00053B28
			public override void Abort()
			{
				object lockState = this.LockState;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
				lock (lockState)
				{
					if (base.State != ModBase.LoadState.Loading && base.State != ModBase.LoadState.Waiting)
					{
						return;
					}
					base.State = ModBase.LoadState.Aborted;
				}
				ModBase.RunInThread(delegate
				{
					try
					{
						foreach (ModLoader.LoaderBase loaderBase in this.Loaders)
						{
							loaderBase.Abort();
						}
					}
					finally
					{
						List<ModLoader.LoaderBase>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				});
			}

			// Token: 0x06000CE3 RID: 3299 RVA: 0x00055994 File Offset: 0x00053B94
			private void SubTaskStateChanged(ModLoader.LoaderBase Loader, ModBase.LoadState NewState, ModBase.LoadState OldState)
			{
				switch (NewState)
				{
				case ModBase.LoadState.Waiting:
				case ModBase.LoadState.Loading:
					return;
				case ModBase.LoadState.Finished:
					this.Update();
					return;
				case ModBase.LoadState.Aborted:
					this.Abort();
					return;
				}
				object lockState = this.LockState;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
				lock (lockState)
				{
					if (base.State >= ModBase.LoadState.Finished)
					{
						return;
					}
					base.Error = new Exception(Loader.Name + "失败", Loader.Error);
					base.State = Loader.State;
				}
				try
				{
					List<ModLoader.LoaderBase>.Enumerator enumerator = this.Loaders.GetEnumerator();
					while (enumerator.MoveNext())
					{
						Loader = enumerator.Current;
						Loader.Abort();
					}
				}
				finally
				{
					List<ModLoader.LoaderBase>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
			}

			// Token: 0x06000CE4 RID: 3300 RVA: 0x00055A8C File Offset: 0x00053C8C
			private void Update()
			{
				if (base.State != ModBase.LoadState.Finished && base.State != ModBase.LoadState.Failed && base.State != ModBase.LoadState.Aborted)
				{
					bool flag = true;
					bool flag2 = false;
					object obj = this.Input;
					try
					{
						foreach (ModLoader.LoaderBase loaderBase in this.Loaders)
						{
							ModBase.LoadState state = loaderBase.State;
							if (state == ModBase.LoadState.Loading)
							{
								if (loaderBase.GetType().Name.StartsWithF("LoaderTask", false))
								{
									object instance = loaderBase;
									Type type = null;
									string memberName = "ShouldStart";
									object[] array = new object[2];
									int num = 1;
									if (obj == null)
									{
										goto IL_180;
									}
									if (Enumerable.First<Type>(loaderBase.GetType().GenericTypeArguments) != obj.GetType())
									{
										goto IL_180;
									}
									object obj2 = obj;
									IL_181:
									array[num] = obj2;
									array[0] = true;
									if (Conversions.ToBoolean(NewLateBinding.LateGet(instance, type, memberName, array, new string[]
									{
										"IgnoreReloadTimeout"
									}, null, null)))
									{
										ModBase.Log("[Loader] 由于输入条件变更，重启进行中的加载器 " + loaderBase.Name, ModBase.LogLevel.Developer, "出现错误");
										goto IL_1C6;
									}
									goto IL_27B;
									IL_180:
									obj2 = null;
									goto IL_181;
								}
								IL_27B:
								flag = false;
								flag2 = true;
								continue;
							}
							if (state == ModBase.LoadState.Finished)
							{
								if (loaderBase.GetType().Name.StartsWithF("LoaderTask", false))
								{
									object instance2 = loaderBase;
									Type type2 = null;
									string memberName2 = "ShouldStart";
									object[] array2 = new object[2];
									int num2 = 1;
									if (obj == null)
									{
										goto IL_B5;
									}
									if (Enumerable.First<Type>(loaderBase.GetType().GenericTypeArguments) != obj.GetType())
									{
										goto IL_B5;
									}
									object obj3 = obj;
									IL_B6:
									array2[num2] = obj3;
									array2[0] = true;
									if (Conversions.ToBoolean(NewLateBinding.LateGet(instance2, type2, memberName2, array2, new string[]
									{
										"IgnoreReloadTimeout"
									}, null, null)))
									{
										ModBase.Log("[Loader] 由于输入条件变更，重启已完成的加载器 " + loaderBase.Name, ModBase.LogLevel.Normal, "出现错误");
										goto IL_1C6;
									}
									obj = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(loaderBase, null, "Output", new object[0], null, null, null));
									goto IL_119;
									IL_B5:
									obj3 = null;
									goto IL_B6;
								}
								IL_119:
								if (loaderBase.Block && !flag)
								{
									flag2 = true;
									continue;
								}
								continue;
							}
							IL_1C6:
							flag = false;
							if (!flag2)
							{
								if (obj != null)
								{
									string name = loaderBase.GetType().Name;
									if (!name.StartsWithF("LoaderTask", false) && !name.StartsWithF("LoaderCombo", false))
									{
										if (!name.StartsWithF("LoaderDownload", false))
										{
											throw new Exception("未知的加载器类型（" + name + "）");
										}
										loaderBase.Start(RuntimeHelpers.GetObjectValue((obj is List<ModNet.NetFile>) ? obj : null), this.IsForceRestarting);
									}
									else
									{
										loaderBase.Start(RuntimeHelpers.GetObjectValue((Enumerable.First<Type>(loaderBase.GetType().GenericTypeArguments) == obj.GetType()) ? obj : null), this.IsForceRestarting);
									}
								}
								else
								{
									loaderBase.Start(null, this.IsForceRestarting);
								}
								if (loaderBase.Block)
								{
									flag2 = true;
								}
							}
						}
					}
					finally
					{
						List<ModLoader.LoaderBase>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					if (flag)
					{
						base.RaisePreviewFinish();
						base.State = ModBase.LoadState.Finished;
						ModMain._ProcessIterator.BtnExtraDownload.ShowRefresh();
					}
				}
			}

			// Token: 0x06000CE5 RID: 3301 RVA: 0x00055D8C File Offset: 0x00053F8C
			public static void GetLoaderList(object Loader, ref List<ModLoader.LoaderBase> List, bool RequireShow = true)
			{
				try
				{
					foreach (object obj in ((IEnumerable)NewLateBinding.LateGet(Loader, null, "Loaders", new object[0], null, null, null)))
					{
						object objectValue = RuntimeHelpers.GetObjectValue(obj);
						if (Conversions.ToBoolean(Conversions.ToBoolean(NewLateBinding.LateGet(objectValue, null, "Show", new object[0], null, null, null)) || !RequireShow))
						{
							List.Add((ModLoader.LoaderBase)objectValue);
						}
						if (objectValue.GetType().Name.StartsWithF("LoaderCombo", false))
						{
							ModLoader.LoaderCombo<!0>.GetLoaderList(RuntimeHelpers.GetObjectValue(objectValue), ref List, true);
						}
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

			// Token: 0x06000CE6 RID: 3302 RVA: 0x000086B9 File Offset: 0x000068B9
			public void GetLoaderList(ref List<ModLoader.LoaderBase> List, bool RequireShow = true)
			{
				ModLoader.LoaderCombo<!0>.GetLoaderList(this, ref List, RequireShow);
			}

			// Token: 0x06000CE7 RID: 3303 RVA: 0x00055E58 File Offset: 0x00054058
			public List<ModLoader.LoaderBase> GetLoaderList(bool RequireShow = true)
			{
				List<ModLoader.LoaderBase> result = new List<ModLoader.LoaderBase>();
				this.GetLoaderList(ref result, RequireShow);
				return result;
			}

			// Token: 0x0400067F RID: 1663
			public List<ModLoader.LoaderBase> Loaders;

			// Token: 0x04000680 RID: 1664
			public InputType Input;
		}

		// Token: 0x0200012C RID: 300
		private struct LoaderFolderDictionaryEntry
		{
			// Token: 0x06000CEA RID: 3306 RVA: 0x00055EC8 File Offset: 0x000540C8
			public override bool Equals(object obj)
			{
				bool result;
				if (!(obj is ModLoader.LoaderFolderDictionaryEntry))
				{
					result = false;
				}
				else
				{
					ModLoader.LoaderFolderDictionaryEntry loaderFolderDictionaryEntry = (ModLoader.LoaderFolderDictionaryEntry)obj;
					result = (EqualityComparer<DateTime?>.Default.Equals(this.LastCheckTime, loaderFolderDictionaryEntry.LastCheckTime) && Operators.CompareString(this.FolderPath, loaderFolderDictionaryEntry.FolderPath, false) == 0);
				}
				return result;
			}

			// Token: 0x04000681 RID: 1665
			public DateTime? LastCheckTime;

			// Token: 0x04000682 RID: 1666
			public string FolderPath;
		}

		// Token: 0x0200012D RID: 301
		public enum LoaderFolderRunType
		{
			// Token: 0x04000684 RID: 1668
			RunOnUpdated,
			// Token: 0x04000685 RID: 1669
			ForceRun,
			// Token: 0x04000686 RID: 1670
			UpdateOnly
		}
	}
}
