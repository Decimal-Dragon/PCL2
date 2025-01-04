using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x02000043 RID: 67
	public class MyPageRight : AdornerDecorator
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00002E40 File Offset: 0x00001040
		public MyPageRight()
		{
			this.m_Role = ModBase.GetUuid();
			this.m_Printer = MyPageRight.PageStates.Empty;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00002E5B File Offset: 0x0000105B
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00012DF4 File Offset: 0x00010FF4
		public MyScrollViewer PanScroll
		{
			get
			{
				return (MyScrollViewer)base.GetValue((DependencyProperty)MyPageRight._Struct);
			}
			set
			{
				object[] array;
				bool[] array2;
				NewLateBinding.LateCall(this, null, "SetValue", array = new object[]
				{
					MyPageRight._Struct,
					value
				}, null, null, array2 = new bool[]
				{
					default(bool),
					true
				}, true);
				if (array2[1])
				{
					value = (MyScrollViewer)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[1]), typeof(MyScrollViewer));
				}
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00002E72 File Offset: 0x00001072
		public MyPageRight.PageStates NewBroadcaster()
		{
			return this.m_Printer;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00002E7A File Offset: 0x0000107A
		public void ComputeBroadcaster(MyPageRight.PageStates value)
		{
			if (this.m_Printer != value)
			{
				this.m_Printer = value;
				if (ModBase._TokenRepository)
				{
					ModBase.Log("[UI] 页面状态切换为 " + ModBase.GetStringFromEnum(value), ModBase.LogLevel.Normal, "出现错误");
				}
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00012E58 File Offset: 0x00011058
		public void PageLoaderInit(MyLoading LoaderUi, FrameworkElement PanLoader, FrameworkElement PanContent, FrameworkElement PanAlways, ModLoader.LoaderBase RealLoader, Action<ModLoader.LoaderBase> FinishedInvoke = null, Func<object> InputInvoke = null, bool AutoRun = true)
		{
			MyPageRight._Closure$__19-0 CS$<>8__locals1 = new MyPageRight._Closure$__19-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Me = this;
			CS$<>8__locals1.$VB$Local_RealLoader = RealLoader;
			CS$<>8__locals1.$VB$Local_FinishedInvoke = FinishedInvoke;
			this.m_Advisor = PanLoader;
			this.PanContent = PanContent;
			this.PanAlways = PanAlways;
			this._Val = CS$<>8__locals1.$VB$Local_RealLoader;
			this.candidate = LoaderUi;
			this.m_Attr = InputInvoke;
			this._Account = AutoRun;
			if (CS$<>8__locals1.$VB$Local_FinishedInvoke != null)
			{
				CS$<>8__locals1.$VB$Local_RealLoader.PreviewFinish += delegate(ModLoader.LoaderBase a0)
				{
					base._Lambda$__0();
				};
			}
			CS$<>8__locals1.$VB$Local_RealLoader.OnStateChangedUi += delegate(ModLoader.LoaderBase Loader, ModBase.LoadState NewState, ModBase.LoadState OldState)
			{
				MyPageRight._Closure$__19-1 CS$<>8__locals2 = new MyPageRight._Closure$__19-1(CS$<>8__locals2);
				CS$<>8__locals2.$VB$Me = this;
				CS$<>8__locals2.$VB$Local_Loader = Loader;
				CS$<>8__locals2.$VB$Local_NewState = NewState;
				CS$<>8__locals2.$VB$Local_OldState = OldState;
				ModBase.RunInUi(delegate()
				{
					CS$<>8__locals2.$VB$Me.PageLoaderState(CS$<>8__locals2.$VB$Local_Loader, CS$<>8__locals2.$VB$Local_NewState, CS$<>8__locals2.$VB$Local_OldState);
				}, false);
			};
			PanLoader.Visibility = Visibility.Collapsed;
			PanContent.Visibility = Visibility.Collapsed;
			if (PanAlways != null)
			{
				PanAlways.Visibility = Visibility.Collapsed;
			}
			if (this._Account)
			{
				if (this._Val.GetType().Name.StartsWithF("LoaderTask", false))
				{
					ModLoader.LoaderBase val = this._Val;
					object val2 = this._Val;
					Type type = null;
					string memberName = "StartGetInput";
					object[] array = new object[2];
					int num = 1;
					ref object ptr = ref this.m_Attr;
					array[num] = this.m_Attr;
					object[] array2 = array;
					bool[] array3;
					object obj = NewLateBinding.LateGet(val2, type, memberName, array, null, null, array3 = new bool[]
					{
						default(bool),
						true
					});
					if (array3[1])
					{
						ptr = RuntimeHelpers.GetObjectValue(array2[1]);
					}
					val.Start(RuntimeHelpers.GetObjectValue(obj), false);
				}
				else
				{
					object obj2 = null;
					if (this.m_Attr != null)
					{
						obj2 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(this.m_Attr, new object[0], null));
					}
					this._Val.Start(RuntimeHelpers.GetObjectValue(obj2), false);
				}
			}
			if (this._Val.State == ModBase.LoadState.Finished && CS$<>8__locals1.$VB$Local_FinishedInvoke != null)
			{
				ModBase.RunInUiWait(delegate()
				{
					CS$<>8__locals1.$VB$Local_FinishedInvoke(CS$<>8__locals1.$VB$Local_RealLoader);
				});
			}
			this.candidate.State = CS$<>8__locals1.$VB$Local_RealLoader;
			this.candidate.Click += delegate(object sender, MouseButtonEventArgs e)
			{
				base._Lambda$__5();
			};
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00013010 File Offset: 0x00011210
		public void PageLoaderRestart(object Input = null, bool IsForceRestart = true)
		{
			if (this._Account)
			{
				if (this._Val.GetType().Name.StartsWithF("LoaderTask", false))
				{
					ModLoader.LoaderBase val = this._Val;
					object val2 = this._Val;
					Type type = null;
					string memberName = "StartGetInput";
					object[] array = new object[2];
					array[0] = Input;
					int num = 1;
					ref object ptr = ref this.m_Attr;
					array[num] = this.m_Attr;
					object[] array2 = array;
					bool[] array3;
					object obj = NewLateBinding.LateGet(val2, type, memberName, array, null, null, array3 = new bool[]
					{
						true,
						true
					});
					if (array3[0])
					{
						Input = RuntimeHelpers.GetObjectValue(array2[0]);
					}
					if (array3[1])
					{
						ptr = RuntimeHelpers.GetObjectValue(array2[1]);
					}
					val.Start(RuntimeHelpers.GetObjectValue(obj), IsForceRestart);
					return;
				}
				if (Input == null && this.m_Attr != null)
				{
					Input = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(this.m_Attr, new object[0], null));
				}
				this._Val.Start(RuntimeHelpers.GetObjectValue(Input), IsForceRestart);
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000130E8 File Offset: 0x000112E8
		public void PageOnEnter()
		{
			if (ModBase._TokenRepository)
			{
				ModBase.Log("[UI] 已触发 PageOnEnter", ModBase.LogLevel.Normal, "出现错误");
			}
			MyPageRight.PageEnterEventHandler queue = this._Queue;
			if (queue != null)
			{
				queue();
			}
			MyPageRight.PageStates pageStates = this.NewBroadcaster();
			if (pageStates == MyPageRight.PageStates.Empty)
			{
				if (this._Val != null && this._Val.State != ModBase.LoadState.Finished && this._Val.State != ModBase.LoadState.Waiting)
				{
					if (this._Val.State != ModBase.LoadState.Aborted)
					{
						if (this._Val.State == ModBase.LoadState.Loading)
						{
							this.ComputeBroadcaster(MyPageRight.PageStates.LoaderWait);
							ModAnimation.AniStart(ModAnimation.AaCode(new ThreadStart(this.PageOnLoaderWaitFinished), 200, false), "PageRight PageChange " + Conversions.ToString(this.m_Role), false);
							return;
						}
						this.ComputeBroadcaster(MyPageRight.PageStates.LoaderEnter);
						this.TriggerEnterAnimation(new FrameworkElement[]
						{
							this.PanAlways,
							this.m_Advisor
						});
						return;
					}
				}
				this.ComputeBroadcaster(MyPageRight.PageStates.ContentEnter);
				this.TriggerEnterAnimation(new FrameworkElement[]
				{
					this.PanAlways,
					(FrameworkElement)(this.PanContent ?? this.Child)
				});
				return;
			}
			if (pageStates == MyPageRight.PageStates.ContentEnter)
			{
				return;
			}
			if (pageStates != MyPageRight.PageStates.ContentExit)
			{
				throw new Exception("在状态为 " + ModBase.GetStringFromEnum(this.NewBroadcaster()) + " 时触发了 PageOnEnter 事件。");
			}
			if (this._Val != null && this._Val.State != ModBase.LoadState.Finished && this._Val.State != ModBase.LoadState.Waiting)
			{
				if (this._Val.State != ModBase.LoadState.Aborted)
				{
					if (this._Val.State == ModBase.LoadState.Loading)
					{
						this.ComputeBroadcaster(MyPageRight.PageStates.LoaderWait);
						ModAnimation.AniStart(ModAnimation.AaCode(new ThreadStart(this.PageOnLoaderWaitFinished), 200, false), "PageRight PageChange " + Conversions.ToString(this.m_Role), false);
						return;
					}
					this.ComputeBroadcaster(MyPageRight.PageStates.LoaderEnter);
					this.TriggerEnterAnimation(new FrameworkElement[]
					{
						this.m_Advisor
					});
					return;
				}
			}
			this.ComputeBroadcaster(MyPageRight.PageStates.ContentEnter);
			this.TriggerEnterAnimation(new FrameworkElement[]
			{
				(FrameworkElement)(this.PanContent ?? this.Child)
			});
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00013304 File Offset: 0x00011504
		[CompilerGenerated]
		public void IncludeBroadcaster(MyPageRight.PageEnterEventHandler obj)
		{
			MyPageRight.PageEnterEventHandler pageEnterEventHandler = this._Queue;
			MyPageRight.PageEnterEventHandler pageEnterEventHandler2;
			do
			{
				pageEnterEventHandler2 = pageEnterEventHandler;
				MyPageRight.PageEnterEventHandler value = (MyPageRight.PageEnterEventHandler)Delegate.Combine(pageEnterEventHandler2, obj);
				pageEnterEventHandler = Interlocked.CompareExchange<MyPageRight.PageEnterEventHandler>(ref this._Queue, value, pageEnterEventHandler2);
			}
			while (pageEnterEventHandler != pageEnterEventHandler2);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0001333C File Offset: 0x0001153C
		[CompilerGenerated]
		public void CloneBroadcaster(MyPageRight.PageEnterEventHandler obj)
		{
			MyPageRight.PageEnterEventHandler pageEnterEventHandler = this._Queue;
			MyPageRight.PageEnterEventHandler pageEnterEventHandler2;
			do
			{
				pageEnterEventHandler2 = pageEnterEventHandler;
				MyPageRight.PageEnterEventHandler value = (MyPageRight.PageEnterEventHandler)Delegate.Remove(pageEnterEventHandler2, obj);
				pageEnterEventHandler = Interlocked.CompareExchange<MyPageRight.PageEnterEventHandler>(ref this._Queue, value, pageEnterEventHandler2);
			}
			while (pageEnterEventHandler != pageEnterEventHandler2);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00013374 File Offset: 0x00011574
		public void PageOnExit()
		{
			if (ModBase._TokenRepository)
			{
				ModBase.Log("[UI] 已触发 PageOnExit", ModBase.LogLevel.Normal, "出现错误");
			}
			MyPageRight.PageExitEventHandler @event = this._Event;
			if (@event != null)
			{
				@event();
			}
			switch (this.NewBroadcaster())
			{
			case MyPageRight.PageStates.Empty:
			case MyPageRight.PageStates.PageExit:
				break;
			case MyPageRight.PageStates.LoaderWait:
				this.ComputeBroadcaster(MyPageRight.PageStates.PageExit);
				this.TriggerExitAnimation(new FrameworkElement[]
				{
					this.PanAlways
				});
				return;
			case MyPageRight.PageStates.LoaderEnter:
			case MyPageRight.PageStates.LoaderStayForce:
			case MyPageRight.PageStates.LoaderStay:
				this.ComputeBroadcaster(MyPageRight.PageStates.PageExit);
				this.TriggerExitAnimation(new FrameworkElement[]
				{
					this.PanAlways,
					this.m_Advisor
				});
				return;
			case MyPageRight.PageStates.LoaderExit:
			case MyPageRight.PageStates.ContentExit:
				this.ComputeBroadcaster(MyPageRight.PageStates.PageExit);
				if (this.PanAlways != null)
				{
					this.TriggerExitAnimation(new FrameworkElement[]
					{
						this.PanAlways,
						(FrameworkElement)(this.PanContent ?? this.Child)
					});
				}
				break;
			case MyPageRight.PageStates.ContentEnter:
			case MyPageRight.PageStates.ContentStay:
				this.ComputeBroadcaster(MyPageRight.PageStates.PageExit);
				this.TriggerExitAnimation(new FrameworkElement[]
				{
					this.PanAlways,
					(FrameworkElement)(this.PanContent ?? this.Child)
				});
				return;
			default:
				return;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00013498 File Offset: 0x00011698
		[CompilerGenerated]
		public void MapBroadcaster(MyPageRight.PageExitEventHandler obj)
		{
			MyPageRight.PageExitEventHandler pageExitEventHandler = this._Event;
			MyPageRight.PageExitEventHandler pageExitEventHandler2;
			do
			{
				pageExitEventHandler2 = pageExitEventHandler;
				MyPageRight.PageExitEventHandler value = (MyPageRight.PageExitEventHandler)Delegate.Combine(pageExitEventHandler2, obj);
				pageExitEventHandler = Interlocked.CompareExchange<MyPageRight.PageExitEventHandler>(ref this._Event, value, pageExitEventHandler2);
			}
			while (pageExitEventHandler != pageExitEventHandler2);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000134D0 File Offset: 0x000116D0
		[CompilerGenerated]
		public void InvokeBroadcaster(MyPageRight.PageExitEventHandler obj)
		{
			MyPageRight.PageExitEventHandler pageExitEventHandler = this._Event;
			MyPageRight.PageExitEventHandler pageExitEventHandler2;
			do
			{
				pageExitEventHandler2 = pageExitEventHandler;
				MyPageRight.PageExitEventHandler value = (MyPageRight.PageExitEventHandler)Delegate.Remove(pageExitEventHandler2, obj);
				pageExitEventHandler = Interlocked.CompareExchange<MyPageRight.PageExitEventHandler>(ref this._Event, value, pageExitEventHandler2);
			}
			while (pageExitEventHandler != pageExitEventHandler2);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00013508 File Offset: 0x00011708
		public void PageOnForceExit()
		{
			if (this.NewBroadcaster() != MyPageRight.PageStates.Empty)
			{
				if (ModBase._TokenRepository)
				{
					ModBase.Log("[UI] 已触发 PageOnForceExit", ModBase.LogLevel.Normal, "出现错误");
				}
				this.ComputeBroadcaster(MyPageRight.PageStates.Empty);
				ModAnimation.AniStop("PageRight PageChange " + Conversions.ToString(this.m_Role));
				if (this._Val == null)
				{
					this.Child.Visibility = Visibility.Collapsed;
					return;
				}
				this.PanContent.Visibility = Visibility.Collapsed;
				this.m_Advisor.Visibility = Visibility.Collapsed;
				if (this.PanAlways != null)
				{
					this.PanAlways.Visibility = Visibility.Collapsed;
				}
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00013598 File Offset: 0x00011798
		public void PageOnContentExit()
		{
			if (ModBase._TokenRepository)
			{
				ModBase.Log("[UI] 已触发 PageOnContentExit", ModBase.LogLevel.Normal, "出现错误");
			}
			if (this._Val != null && this._Val.State == ModBase.LoadState.Loading)
			{
				throw new Exception("在调用 PageOnContentExit 时，加载器不能为 Loading 状态");
			}
			switch (this.NewBroadcaster())
			{
			case MyPageRight.PageStates.Empty:
			case MyPageRight.PageStates.LoaderWait:
				this.PageOnEnter();
				return;
			case MyPageRight.PageStates.LoaderEnter:
			case MyPageRight.PageStates.LoaderStayForce:
			case MyPageRight.PageStates.LoaderStay:
				this.ComputeBroadcaster(MyPageRight.PageStates.ContentExit);
				this.TriggerExitAnimation(new FrameworkElement[]
				{
					this.m_Advisor
				});
				return;
			case MyPageRight.PageStates.LoaderExit:
				this.ComputeBroadcaster(MyPageRight.PageStates.ContentExit);
				return;
			case MyPageRight.PageStates.ContentEnter:
			case MyPageRight.PageStates.ContentStay:
				this.ComputeBroadcaster(MyPageRight.PageStates.ContentExit);
				this.TriggerExitAnimation(new FrameworkElement[]
				{
					this.PanContent
				});
				return;
			default:
				return;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00013654 File Offset: 0x00011854
		private void PageOnEnterAnimationFinished()
		{
			if (ModBase._TokenRepository)
			{
				ModBase.Log("[UI] 已触发 PageOnEnterAnimationFinished", ModBase.LogLevel.Normal, "出现错误");
			}
			MyPageRight.PageStates pageStates = this.NewBroadcaster();
			if (pageStates == MyPageRight.PageStates.LoaderEnter)
			{
				this.ComputeBroadcaster(MyPageRight.PageStates.LoaderStayForce);
				ModAnimation.AniStart(ModAnimation.AaCode(new ThreadStart(this.PageOnLoaderStayFinished), 400, false), "PageRight PageChange " + Conversions.ToString(this.m_Role), false);
				return;
			}
			if (pageStates == MyPageRight.PageStates.ContentEnter)
			{
				this.ComputeBroadcaster(MyPageRight.PageStates.ContentStay);
				return;
			}
			throw new Exception("在状态为 " + ModBase.GetStringFromEnum(this.NewBroadcaster()) + " 时触发了 PageOnEnterAnimationFinished 事件。");
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000136F0 File Offset: 0x000118F0
		private void PageOnExitAnimationFinished()
		{
			if (ModBase._TokenRepository)
			{
				ModBase.Log("[UI] 已触发 PageOnExitAnimationFinished", ModBase.LogLevel.Normal, "出现错误");
			}
			switch (this.NewBroadcaster())
			{
			case MyPageRight.PageStates.LoaderExit:
				this.ComputeBroadcaster(MyPageRight.PageStates.ContentEnter);
				this.TriggerEnterAnimation(new FrameworkElement[]
				{
					this.PanContent
				});
				return;
			case MyPageRight.PageStates.ContentExit:
				this.PageOnEnter();
				return;
			case MyPageRight.PageStates.PageExit:
				this.ComputeBroadcaster(MyPageRight.PageStates.Empty);
				return;
			}
			throw new Exception("在状态为 " + ModBase.GetStringFromEnum(this.NewBroadcaster()) + " 时触发了 PageOnExitAnimationFinished 事件。");
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0001378C File Offset: 0x0001198C
		private void PageOnLoaderWaitFinished()
		{
			if (ModBase._TokenRepository)
			{
				ModBase.Log("[UI] 已触发 PageOnLoaderWaitFinished", ModBase.LogLevel.Normal, "出现错误");
			}
			MyPageRight.PageStates pageStates = this.NewBroadcaster();
			if (pageStates != MyPageRight.PageStates.LoaderWait)
			{
				throw new Exception("在状态为 " + ModBase.GetStringFromEnum(this.NewBroadcaster()) + " 时触发了 PageOnLoaderWaitFinished 事件。");
			}
			this.ComputeBroadcaster(MyPageRight.PageStates.LoaderEnter);
			if (this.PanAlways != null && this.PanAlways.Visibility == Visibility.Collapsed)
			{
				this.TriggerEnterAnimation(new FrameworkElement[]
				{
					this.PanAlways,
					this.m_Advisor
				});
				return;
			}
			this.TriggerEnterAnimation(new FrameworkElement[]
			{
				this.m_Advisor
			});
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00013834 File Offset: 0x00011A34
		private void PageOnLoaderStayFinished()
		{
			if (ModBase._TokenRepository)
			{
				ModBase.Log("[UI] 已触发 PageOnLoaderStayFinished", ModBase.LogLevel.Normal, "出现错误");
			}
			MyPageRight.PageStates pageStates = this.NewBroadcaster();
			if (pageStates != MyPageRight.PageStates.LoaderStayForce)
			{
				throw new Exception("在状态为 " + ModBase.GetStringFromEnum(this.NewBroadcaster()) + " 时触发了 PageOnLoaderWaitFinished 事件。");
			}
			if (this._Val.State == ModBase.LoadState.Finished)
			{
				this.ComputeBroadcaster(MyPageRight.PageStates.LoaderExit);
				this.TriggerExitAnimation(new FrameworkElement[]
				{
					this.m_Advisor
				});
				return;
			}
			this.ComputeBroadcaster(MyPageRight.PageStates.LoaderStay);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000138BC File Offset: 0x00011ABC
		private void PageLoaderState(object sender, ModBase.LoadState NewState, ModBase.LoadState OldState)
		{
			switch (NewState)
			{
			case ModBase.LoadState.Waiting:
			case ModBase.LoadState.Finished:
			case ModBase.LoadState.Aborted:
				if (OldState == ModBase.LoadState.Failed || OldState == ModBase.LoadState.Loading)
				{
					if (ModBase._TokenRepository)
					{
						ModBase.Log("[UI] 已触发 PageLoaderState (Stop/Abort)", ModBase.LogLevel.Normal, "出现错误");
					}
					MyPageRight.PageStates pageStates = this.NewBroadcaster();
					if (pageStates != MyPageRight.PageStates.LoaderWait)
					{
						if (pageStates != MyPageRight.PageStates.LoaderStay)
						{
							return;
						}
						this.ComputeBroadcaster(MyPageRight.PageStates.LoaderExit);
						this.TriggerExitAnimation(new FrameworkElement[]
						{
							this.m_Advisor
						});
					}
					else
					{
						this.ComputeBroadcaster(MyPageRight.PageStates.ContentEnter);
						if (this.PanAlways != null && this.PanAlways.Visibility == Visibility.Collapsed)
						{
							this.TriggerEnterAnimation(new FrameworkElement[]
							{
								this.PanAlways,
								this.PanContent
							});
							return;
						}
						this.TriggerEnterAnimation(new FrameworkElement[]
						{
							this.PanContent
						});
						return;
					}
				}
				break;
			case ModBase.LoadState.Loading:
			case ModBase.LoadState.Failed:
				if (OldState != ModBase.LoadState.Failed && OldState != ModBase.LoadState.Loading)
				{
					if (ModBase._TokenRepository)
					{
						ModBase.Log("[UI] 已触发 PageLoaderState (Start/Refresh)", ModBase.LogLevel.Normal, "出现错误");
					}
					MyPageRight.PageStates pageStates2 = this.NewBroadcaster();
					if (pageStates2 == MyPageRight.PageStates.LoaderExit)
					{
						this.ComputeBroadcaster(MyPageRight.PageStates.ContentExit);
						return;
					}
					if (pageStates2 - MyPageRight.PageStates.ContentEnter <= 1)
					{
						this.ComputeBroadcaster(MyPageRight.PageStates.ContentExit);
						this.TriggerExitAnimation(new FrameworkElement[]
						{
							this.PanContent
						});
						return;
					}
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000139E0 File Offset: 0x00011BE0
		public void TriggerEnterAnimation(params FrameworkElement[] Elements)
		{
			IEnumerable<FrameworkElement> enumerable = Enumerable.Where<FrameworkElement>(Elements, (MyPageRight._Closure$__.$I40-0 == null) ? (MyPageRight._Closure$__.$I40-0 = ((FrameworkElement e) => e != null)) : MyPageRight._Closure$__.$I40-0);
			try
			{
				foreach (FrameworkElement frameworkElement in enumerable)
				{
					frameworkElement.Visibility = Visibility.Visible;
				}
			}
			finally
			{
				IEnumerator<FrameworkElement> enumerator;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
			List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
			int num = 0;
			MyScrollBar firstScrollViewer;
			checked
			{
				try
				{
					foreach (FrameworkElement element in enumerable)
					{
						try
						{
							foreach (FrameworkElement frameworkElement2 in this.GetAllAnimControls(element, true))
							{
								frameworkElement2.IsHitTestVisible = true;
								if (frameworkElement2.RenderTransform != null && frameworkElement2.RenderTransform is TranslateTransform)
								{
									frameworkElement2.RenderTransform = null;
								}
							}
						}
						finally
						{
							List<FrameworkElement>.Enumerator enumerator3;
							((IDisposable)enumerator3).Dispose();
						}
						try
						{
							foreach (FrameworkElement frameworkElement3 in this.GetAllAnimControls(element, false))
							{
								frameworkElement3.Opacity = 0.0;
								frameworkElement3.RenderTransform = new TranslateTransform(0.0, -16.0);
								list.Add(ModAnimation.AaOpacity(frameworkElement3, 1.0, 150, num, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), false));
								list.Add(ModAnimation.AaTranslateY(frameworkElement3, 5.0, 250, num, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false));
								list.Add(ModAnimation.AaTranslateY(frameworkElement3, 11.0, 350, num, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Middle), false));
								num += 40;
							}
						}
						finally
						{
							List<FrameworkElement>.Enumerator enumerator4;
							((IDisposable)enumerator4).Dispose();
						}
					}
				}
				finally
				{
					IEnumerator<FrameworkElement> enumerator2;
					if (enumerator2 != null)
					{
						enumerator2.Dispose();
					}
				}
				firstScrollViewer = this.GetFirstScrollViewer(enumerable);
			}
			if (firstScrollViewer != null)
			{
				if (!(firstScrollViewer.RenderTransform is TranslateTransform))
				{
					firstScrollViewer.RenderTransform = new TranslateTransform(10.0, 0.0);
				}
				list.Add(ModAnimation.AaTranslateX(firstScrollViewer, -((TranslateTransform)firstScrollViewer.RenderTransform).X, 350, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false));
			}
			list.Add(ModAnimation.AaCode(delegate
			{
				this.PageOnEnterAnimationFinished();
			}, 0, true));
			ModAnimation.AniStart(list, "PageRight PageChange " + Conversions.ToString(this.m_Role), false);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00013CA8 File Offset: 0x00011EA8
		public void TriggerExitAnimation(params FrameworkElement[] Elements)
		{
			MyPageRight._Closure$__41-0 CS$<>8__locals1 = new MyPageRight._Closure$__41-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Me = this;
			CS$<>8__locals1.$VB$Local_RealElements = Enumerable.Where<FrameworkElement>(Elements, (MyPageRight._Closure$__.$I41-0 == null) ? (MyPageRight._Closure$__.$I41-0 = ((FrameworkElement e) => e != null)) : MyPageRight._Closure$__.$I41-0);
			List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
			int num = 0;
			MyScrollBar firstScrollViewer;
			checked
			{
				try
				{
					foreach (FrameworkElement element in CS$<>8__locals1.$VB$Local_RealElements)
					{
						try
						{
							foreach (FrameworkElement frameworkElement in this.GetAllAnimControls(element, false))
							{
								frameworkElement.IsHitTestVisible = false;
								list.Add(ModAnimation.AaOpacity(frameworkElement, -1.0, 90, num, null, false));
								list.Add(ModAnimation.AaTranslateY(frameworkElement, -6.0, 90, num, null, false));
								num += 20;
							}
						}
						finally
						{
							List<FrameworkElement>.Enumerator enumerator2;
							((IDisposable)enumerator2).Dispose();
						}
					}
				}
				finally
				{
					IEnumerator<FrameworkElement> enumerator;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
				firstScrollViewer = this.GetFirstScrollViewer(CS$<>8__locals1.$VB$Local_RealElements);
			}
			if (firstScrollViewer != null)
			{
				if (!(firstScrollViewer.RenderTransform is TranslateTransform))
				{
					firstScrollViewer.RenderTransform = new TranslateTransform();
				}
				list.Add(ModAnimation.AaTranslateX(firstScrollViewer, 10.0 - ((TranslateTransform)firstScrollViewer.RenderTransform).X, 90, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false));
			}
			list.Add(ModAnimation.AaCode(delegate
			{
				try
				{
					foreach (FrameworkElement frameworkElement2 in CS$<>8__locals1.$VB$Local_RealElements)
					{
						frameworkElement2.Visibility = Visibility.Collapsed;
					}
				}
				finally
				{
					IEnumerator<FrameworkElement> enumerator3;
					if (enumerator3 != null)
					{
						enumerator3.Dispose();
					}
				}
				CS$<>8__locals1.$VB$Me.PageOnExitAnimationFinished();
			}, 0, true));
			ModAnimation.AniStart(list, "PageRight PageChange " + Conversions.ToString(this.m_Role), false);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00013E50 File Offset: 0x00012050
		internal List<FrameworkElement> GetAllAnimControls(FrameworkElement Element, bool IgnoreInvisibility = false)
		{
			List<FrameworkElement> result = new List<FrameworkElement>();
			this.GetAllAnimControls(Element, ref result, IgnoreInvisibility);
			return result;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00013E70 File Offset: 0x00012070
		private void GetAllAnimControls(FrameworkElement Element, ref List<FrameworkElement> AllControls, bool IgnoreInvisibility)
		{
			if (IgnoreInvisibility || Element.Visibility != Visibility.Collapsed)
			{
				if (Element is MyCard || Element is MyHint || Element is TextBlock || Element is MyTextButton)
				{
					AllControls.Add(Element);
					return;
				}
				if (Element is ContentControl)
				{
					object objectValue = RuntimeHelpers.GetObjectValue(((ContentControl)Element).Content);
					if (objectValue != null && objectValue is FrameworkElement)
					{
						this.GetAllAnimControls((FrameworkElement)objectValue, ref AllControls, IgnoreInvisibility);
						return;
					}
				}
				else if (Element is Panel)
				{
					try
					{
						foreach (object obj in ((Panel)Element).Children)
						{
							object objectValue2 = RuntimeHelpers.GetObjectValue(obj);
							if (objectValue2 is FrameworkElement)
							{
								this.GetAllAnimControls((FrameworkElement)objectValue2, ref AllControls, IgnoreInvisibility);
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
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00013F60 File Offset: 0x00012160
		private MyScrollBar GetFirstScrollViewer(IEnumerable<FrameworkElement> Elements)
		{
			MyScrollViewer myScrollViewer = null;
			try
			{
				foreach (FrameworkElement frameworkElement in Elements)
				{
					if (frameworkElement is MyScrollViewer)
					{
						myScrollViewer = (MyScrollViewer)frameworkElement;
						goto IL_8D;
					}
					try
					{
						foreach (object obj in LogicalTreeHelper.GetChildren(frameworkElement))
						{
							object objectValue = RuntimeHelpers.GetObjectValue(obj);
							if (objectValue is MyScrollViewer)
							{
								myScrollViewer = (MyScrollViewer)objectValue;
								goto IL_8D;
							}
						}
					}
					finally
					{
						IEnumerator enumerator2;
						if (enumerator2 is IDisposable)
						{
							(enumerator2 as IDisposable).Dispose();
						}
					}
				}
			}
			finally
			{
				IEnumerator<FrameworkElement> enumerator;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
			return null;
			IL_8D:
			MyScrollBar result;
			if (myScrollViewer.ComputedVerticalScrollBarVisibility != Visibility.Visible)
			{
				result = null;
			}
			else
			{
				result = myScrollViewer.m_RegBroadcaster;
			}
			return result;
		}

		// Token: 0x04000097 RID: 151
		public int m_Role;

		// Token: 0x04000098 RID: 152
		private static readonly DependencyProperty _Struct = DependencyProperty.Register("PanScroll", typeof(MyScrollViewer), typeof(MyPageRight), new PropertyMetadata(null));

		// Token: 0x04000099 RID: 153
		private MyPageRight.PageStates m_Printer;

		// Token: 0x0400009A RID: 154
		private ModLoader.LoaderBase _Val;

		// Token: 0x0400009B RID: 155
		private object m_Attr;

		// Token: 0x0400009C RID: 156
		private MyLoading candidate;

		// Token: 0x0400009D RID: 157
		private FrameworkElement m_Advisor;

		// Token: 0x0400009E RID: 158
		private FrameworkElement PanContent;

		// Token: 0x0400009F RID: 159
		private FrameworkElement PanAlways;

		// Token: 0x040000A0 RID: 160
		private bool _Account;

		// Token: 0x040000A1 RID: 161
		[CompilerGenerated]
		private MyPageRight.PageEnterEventHandler _Queue;

		// Token: 0x040000A2 RID: 162
		[CompilerGenerated]
		private MyPageRight.PageExitEventHandler _Event;

		// Token: 0x02000044 RID: 68
		public enum PageStates
		{
			// Token: 0x040000A4 RID: 164
			Empty,
			// Token: 0x040000A5 RID: 165
			LoaderWait,
			// Token: 0x040000A6 RID: 166
			LoaderEnter,
			// Token: 0x040000A7 RID: 167
			LoaderStayForce,
			// Token: 0x040000A8 RID: 168
			LoaderStay,
			// Token: 0x040000A9 RID: 169
			LoaderExit,
			// Token: 0x040000AA RID: 170
			ContentEnter,
			// Token: 0x040000AB RID: 171
			ContentStay,
			// Token: 0x040000AC RID: 172
			ContentExit,
			// Token: 0x040000AD RID: 173
			PageExit
		}

		// Token: 0x02000045 RID: 69
		// (Invoke) Token: 0x06000189 RID: 393
		public delegate void PageEnterEventHandler();

		// Token: 0x02000046 RID: 70
		// (Invoke) Token: 0x0600018E RID: 398
		public delegate void PageExitEventHandler();
	}
}
