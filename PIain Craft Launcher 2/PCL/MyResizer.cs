using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.VisualBasic;

namespace PCL
{
	// Token: 0x020001D6 RID: 470
	public class MyResizer
	{
		// Token: 0x06001601 RID: 5633 RVA: 0x00091480 File Offset: 0x0008F680
		public MyResizer(Window target)
		{
			this.orderComposer = null;
			this.m_ProducerComposer = false;
			this.schemaComposer = false;
			this.descriptorComposer = false;
			this._PublisherComposer = false;
			this._DefinitionComposer = new Dictionary<UIElement, short>();
			this._StrategyComposer = new Dictionary<UIElement, short>();
			this.procComposer = new Dictionary<UIElement, short>();
			this.parserIterator = new Dictionary<UIElement, short>();
			this._BroadcasterIterator = default(MyResizer.PointAPI);
			this.fieldIterator = default(Size);
			this.m_ReaderIterator = default(MyResizer.POINT);
			this.orderComposer = target;
			if (Information.IsNothing(target))
			{
				throw new Exception("Invalid Window handle");
			}
			target.SourceInitialized += this.MyMacClass_SourceInitialized;
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0000C530 File Offset: 0x0000A730
		private void MyMacClass_SourceInitialized(object sender, EventArgs e)
		{
			this.configIterator = (PresentationSource.FromVisual((Visual)sender) as HwndSource);
			this.configIterator.AddHook(new HwndSourceHook(this.WndProc));
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0000C55F File Offset: 0x0000A75F
		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg == 36)
			{
				MyResizer.WmGetMinMaxInfo(hwnd, lParam);
				handled = true;
			}
			return (IntPtr)0;
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00091534 File Offset: 0x0008F734
		private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
		{
			object obj = Marshal.PtrToStructure(lParam, typeof(MyResizer.MINMAXINFO));
			MyResizer.MINMAXINFO minmaxinfo = (obj != null) ? ((MyResizer.MINMAXINFO)obj) : default(MyResizer.MINMAXINFO);
			IntPtr intPtr = MyResizer.MonitorFromWindow(hwnd, 2);
			checked
			{
				if (intPtr != IntPtr.Zero)
				{
					MyResizer.MONITORINFO monitorinfo = new MyResizer.MONITORINFO();
					MyResizer.GetMonitorInfo(intPtr, monitorinfo);
					MyResizer.RECT messageMap = monitorinfo.messageMap;
					MyResizer.RECT proxyMap = monitorinfo._ProxyMap;
					minmaxinfo.m_InterceptorMap.predicateMap = Math.Abs(messageMap.m_DispatcherMap - proxyMap.m_DispatcherMap);
					minmaxinfo.m_InterceptorMap.m_PoolMap = Math.Abs(messageMap._ProcessMap - proxyMap._ProcessMap);
					minmaxinfo._PageMap.m_PoolMap = Math.Abs(messageMap.m_RecordMap - messageMap._ProcessMap);
					MyResizer._ClientIterator = minmaxinfo._PageMap.m_PoolMap;
					if (messageMap.Height == proxyMap.Height)
					{
						ref int ptr = ref minmaxinfo._PageMap.m_PoolMap;
						minmaxinfo._PageMap.m_PoolMap = ptr - 2;
					}
				}
				Marshal.StructureToPtr<MyResizer.MINMAXINFO>(minmaxinfo, lParam, true);
			}
		}

		// Token: 0x06001605 RID: 5637
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern bool GetMonitorInfo(IntPtr hMonitor, MyResizer.MONITORINFO lpmi);

		// Token: 0x06001606 RID: 5638
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

		// Token: 0x06001607 RID: 5639 RVA: 0x0000C578 File Offset: 0x0000A778
		private void connectMouseHandlers(UIElement element)
		{
			element.MouseLeftButtonDown += this.element_MouseLeftButtonDown;
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x0000C58C File Offset: 0x0000A78C
		public void addResizerRight(UIElement element)
		{
			this.connectMouseHandlers(element);
			this._StrategyComposer.Add(element, 0);
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x0000C5A2 File Offset: 0x0000A7A2
		public void addResizerLeft(UIElement element)
		{
			this.connectMouseHandlers(element);
			this._DefinitionComposer.Add(element, 0);
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x0000C5B8 File Offset: 0x0000A7B8
		public void addResizerUp(UIElement element)
		{
			this.connectMouseHandlers(element);
			this.procComposer.Add(element, 0);
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x0000C5CE File Offset: 0x0000A7CE
		public void addResizerDown(UIElement element)
		{
			this.connectMouseHandlers(element);
			this.parserIterator.Add(element, 0);
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x0000C5E4 File Offset: 0x0000A7E4
		public void addResizerRightDown(UIElement element)
		{
			this.connectMouseHandlers(element);
			this._StrategyComposer.Add(element, 0);
			this.parserIterator.Add(element, 0);
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x0000C607 File Offset: 0x0000A807
		public void addResizerLeftDown(UIElement element)
		{
			this.connectMouseHandlers(element);
			this._DefinitionComposer.Add(element, 0);
			this.parserIterator.Add(element, 0);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x0000C62A File Offset: 0x0000A82A
		public void addResizerRightUp(UIElement element)
		{
			this.connectMouseHandlers(element);
			this._StrategyComposer.Add(element, 0);
			this.procComposer.Add(element, 0);
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x0000C64D File Offset: 0x0000A84D
		public void addResizerLeftUp(UIElement element)
		{
			this.connectMouseHandlers(element);
			this._DefinitionComposer.Add(element, 0);
			this.procComposer.Add(element, 0);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00091640 File Offset: 0x0008F840
		private void element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			MyResizer.GetCursorPos(out this._BroadcasterIterator);
			checked
			{
				this._BroadcasterIterator.X = (int)Math.Round(ModBase.smethod_4((double)this._BroadcasterIterator.X));
				this._BroadcasterIterator.Y = (int)Math.Round(ModBase.smethod_4((double)this._BroadcasterIterator.Y));
				this.fieldIterator = new Size(this.orderComposer.Width, this.orderComposer.Height);
				this.m_ReaderIterator = new MyResizer.POINT((int)Math.Round(this.orderComposer.Left), (int)Math.Round(this.orderComposer.Top));
				UIElement key = (UIElement)sender;
				if (this._DefinitionComposer.ContainsKey(key))
				{
					this.schemaComposer = true;
				}
				if (this._StrategyComposer.ContainsKey(key))
				{
					this.m_ProducerComposer = true;
				}
				if (this.procComposer.ContainsKey(key))
				{
					this.descriptorComposer = true;
				}
				if (this.parserIterator.ContainsKey(key))
				{
					this._PublisherComposer = true;
				}
				ModBase.RunInNewThread(new Action(this.updateSizeLoop), "窗口大小调整检测", ThreadPriority.Normal);
			}
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00091760 File Offset: 0x0008F960
		private void updateSizeLoop()
		{
			try
			{
				while (this._PublisherComposer || this.schemaComposer || this.m_ProducerComposer || this.descriptorComposer)
				{
					this.orderComposer.Dispatcher.Invoke(new Action(this.updateSize), DispatcherPriority.Render);
					this.orderComposer.Dispatcher.Invoke(new Action(this.updateMouseDown), DispatcherPriority.Render);
					Thread.Sleep(0);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x000917F8 File Offset: 0x0008F9F8
		private void updateSize()
		{
			MyResizer.PointAPI pointAPI = default(MyResizer.PointAPI);
			MyResizer.GetCursorPos(out pointAPI);
			checked
			{
				pointAPI.X = (int)Math.Round(ModBase.smethod_4((double)pointAPI.X));
				pointAPI.Y = (int)Math.Round(ModBase.smethod_4((double)pointAPI.Y));
			}
			try
			{
				double num = -1.0;
				double num2 = -1.0;
				double num3 = -10000.0;
				double num4 = -10000.0;
				if (this.m_ProducerComposer)
				{
					if (this.orderComposer.Width == this.orderComposer.MinWidth)
					{
						if (this._BroadcasterIterator.X < pointAPI.X)
						{
							num = this.fieldIterator.Width - (double)(checked(this._BroadcasterIterator.X - pointAPI.X));
						}
					}
					else if (this.fieldIterator.Width - (double)(checked(this._BroadcasterIterator.X - pointAPI.X)) >= this.orderComposer.MinWidth)
					{
						num = this.fieldIterator.Width - (double)(checked(this._BroadcasterIterator.X - pointAPI.X));
					}
					else
					{
						num = this.orderComposer.MinWidth;
					}
				}
				if (this._PublisherComposer)
				{
					if (this.orderComposer.Height == this.orderComposer.MinHeight)
					{
						if (this._BroadcasterIterator.Y < pointAPI.Y)
						{
							if (MyResizer._ClientIterator > 0)
							{
								num2 = ((this.fieldIterator.Height - (double)(checked(this._BroadcasterIterator.Y - pointAPI.Y)) + this.orderComposer.Top <= (double)MyResizer._ClientIterator) ? (this.fieldIterator.Height - (double)(checked(this._BroadcasterIterator.Y - pointAPI.Y))) : ((double)MyResizer._ClientIterator - this.orderComposer.Top));
							}
							else
							{
								num2 = this.fieldIterator.Height - (double)(checked(this._BroadcasterIterator.Y - pointAPI.Y));
							}
						}
					}
					else if (this.fieldIterator.Height - (double)(checked(this._BroadcasterIterator.Y - pointAPI.Y)) >= this.orderComposer.MinHeight)
					{
						if (MyResizer._ClientIterator > 0)
						{
							num2 = ((this.fieldIterator.Height - (double)(checked(this._BroadcasterIterator.Y - pointAPI.Y)) + this.orderComposer.Top <= (double)MyResizer._ClientIterator) ? (this.fieldIterator.Height - (double)(checked(this._BroadcasterIterator.Y - pointAPI.Y))) : ((double)MyResizer._ClientIterator - this.orderComposer.Top));
						}
						else
						{
							num2 = this.fieldIterator.Height - (double)(checked(this._BroadcasterIterator.Y - pointAPI.Y));
						}
					}
					else
					{
						num2 = this.orderComposer.MinHeight;
					}
				}
				if (this.schemaComposer)
				{
					if (this.orderComposer.Width == this.orderComposer.MinWidth)
					{
						if (this._BroadcasterIterator.X > pointAPI.X)
						{
							num = this.fieldIterator.Width + (double)this._BroadcasterIterator.X - (double)pointAPI.X;
							num3 = (double)(checked(this.m_ReaderIterator.predicateMap - (this._BroadcasterIterator.X - pointAPI.X)));
						}
						else
						{
							num = this.orderComposer.MinWidth;
							num3 = (double)this.m_ReaderIterator.predicateMap + this.fieldIterator.Width - this.orderComposer.Width;
						}
					}
					else if (this.fieldIterator.Width + (double)(checked(this._BroadcasterIterator.X - pointAPI.X)) >= this.orderComposer.MinWidth)
					{
						num = this.fieldIterator.Width + (double)(checked(this._BroadcasterIterator.X - pointAPI.X));
						num3 = (double)(checked(this.m_ReaderIterator.predicateMap - (this._BroadcasterIterator.X - pointAPI.X)));
					}
					else
					{
						num = this.orderComposer.MinWidth;
						num3 = (double)this.m_ReaderIterator.predicateMap + this.fieldIterator.Width - this.orderComposer.Width;
					}
				}
				if (this.descriptorComposer)
				{
					if (this.orderComposer.Height == this.orderComposer.MinHeight)
					{
						if (this._BroadcasterIterator.Y > pointAPI.Y)
						{
							num2 = this.fieldIterator.Height + (double)this._BroadcasterIterator.Y - (double)pointAPI.Y;
							num4 = (double)(checked(this.m_ReaderIterator.m_PoolMap - (this._BroadcasterIterator.Y - pointAPI.Y)));
						}
						else
						{
							num2 = this.orderComposer.MinHeight;
							num4 = (double)this.m_ReaderIterator.m_PoolMap + this.fieldIterator.Height - this.orderComposer.Height;
						}
					}
					else if (this.fieldIterator.Height + (double)(checked(this._BroadcasterIterator.Y - pointAPI.Y)) >= this.orderComposer.MinHeight)
					{
						num2 = this.fieldIterator.Height + (double)this._BroadcasterIterator.Y - (double)pointAPI.Y;
						num4 = (double)(checked(this.m_ReaderIterator.m_PoolMap - (this._BroadcasterIterator.Y - pointAPI.Y)));
					}
					else
					{
						num2 = this.orderComposer.MinHeight;
						num4 = (double)this.m_ReaderIterator.m_PoolMap + this.fieldIterator.Height - this.orderComposer.Height;
					}
				}
				if (num > 10.0 && Math.Abs(num - this.orderComposer.Width) > 0.7)
				{
					this.orderComposer.Width = num;
				}
				if (num2 > 10.0 && Math.Abs(num2 - this.orderComposer.Height) > 0.7)
				{
					this.orderComposer.Height = num2;
				}
				if (num3 > -9999.0 && Math.Abs(num3 - this.orderComposer.Left) > 0.7)
				{
					this.orderComposer.Left = num3;
				}
				if (num4 > -9999.0 && Math.Abs(num4 - this.orderComposer.Top) > 0.7)
				{
					this.orderComposer.Top = num4;
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0000C670 File Offset: 0x0000A870
		private void updateMouseDown()
		{
			if (Mouse.LeftButton == MouseButtonState.Released)
			{
				this.m_ProducerComposer = false;
				this.schemaComposer = false;
				this.descriptorComposer = false;
				this._PublisherComposer = false;
			}
		}

		// Token: 0x06001614 RID: 5652
		[DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern bool GetCursorPos(out MyResizer.PointAPI lpPoint);

		// Token: 0x04000B25 RID: 2853
		private readonly Window orderComposer;

		// Token: 0x04000B26 RID: 2854
		private bool m_ProducerComposer;

		// Token: 0x04000B27 RID: 2855
		private bool schemaComposer;

		// Token: 0x04000B28 RID: 2856
		private bool descriptorComposer;

		// Token: 0x04000B29 RID: 2857
		private bool _PublisherComposer;

		// Token: 0x04000B2A RID: 2858
		private readonly Dictionary<UIElement, short> _DefinitionComposer;

		// Token: 0x04000B2B RID: 2859
		private readonly Dictionary<UIElement, short> _StrategyComposer;

		// Token: 0x04000B2C RID: 2860
		private readonly Dictionary<UIElement, short> procComposer;

		// Token: 0x04000B2D RID: 2861
		private readonly Dictionary<UIElement, short> parserIterator;

		// Token: 0x04000B2E RID: 2862
		private MyResizer.PointAPI _BroadcasterIterator;

		// Token: 0x04000B2F RID: 2863
		private Size fieldIterator;

		// Token: 0x04000B30 RID: 2864
		private MyResizer.POINT m_ReaderIterator;

		// Token: 0x04000B31 RID: 2865
		private static int _ClientIterator = -1;

		// Token: 0x04000B32 RID: 2866
		private HwndSource configIterator;

		// Token: 0x020001D7 RID: 471
		// (Invoke) Token: 0x06001618 RID: 5656
		private delegate void RefreshDelegate();

		// Token: 0x020001D8 RID: 472
		private struct POINT
		{
			// Token: 0x0600161A RID: 5658 RVA: 0x0000C698 File Offset: 0x0000A898
			public POINT(int x, int y)
			{
				this = default(MyResizer.POINT);
				this.predicateMap = x;
				this.m_PoolMap = y;
			}

			// Token: 0x04000B33 RID: 2867
			public int predicateMap;

			// Token: 0x04000B34 RID: 2868
			public int m_PoolMap;
		}

		// Token: 0x020001D9 RID: 473
		private struct MINMAXINFO
		{
			// Token: 0x04000B35 RID: 2869
			public MyResizer.POINT m_CustomerMap;

			// Token: 0x04000B36 RID: 2870
			public MyResizer.POINT _PageMap;

			// Token: 0x04000B37 RID: 2871
			public MyResizer.POINT m_InterceptorMap;

			// Token: 0x04000B38 RID: 2872
			public MyResizer.POINT m_ContainerMap;

			// Token: 0x04000B39 RID: 2873
			public MyResizer.POINT _ParamsMap;
		}

		// Token: 0x020001DA RID: 474
		private struct RECT
		{
			// Token: 0x170003C4 RID: 964
			// (get) Token: 0x0600161D RID: 5661 RVA: 0x0000C6C0 File Offset: 0x0000A8C0
			public int Width
			{
				get
				{
					return Math.Abs(checked(this.parameterMap - this.m_DispatcherMap));
				}
			}

			// Token: 0x170003C5 RID: 965
			// (get) Token: 0x0600161E RID: 5662 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
			public int Height
			{
				get
				{
					return checked(this.m_RecordMap - this._ProcessMap);
				}
			}

			// Token: 0x0600161F RID: 5663 RVA: 0x0000C6E3 File Offset: 0x0000A8E3
			public RECT(int left, int top, int right, int bottom)
			{
				this = default(MyResizer.RECT);
				this.m_DispatcherMap = left;
				this._ProcessMap = top;
				this.parameterMap = right;
				this.m_RecordMap = bottom;
			}

			// Token: 0x06001620 RID: 5664 RVA: 0x0000C70A File Offset: 0x0000A90A
			public RECT(MyResizer.RECT rcSrc)
			{
				this = default(MyResizer.RECT);
				this.m_DispatcherMap = rcSrc.m_DispatcherMap;
				this._ProcessMap = rcSrc._ProcessMap;
				this.parameterMap = rcSrc.parameterMap;
				this.m_RecordMap = rcSrc.m_RecordMap;
			}

			// Token: 0x04000B3A RID: 2874
			public int m_DispatcherMap;

			// Token: 0x04000B3B RID: 2875
			public int _ProcessMap;

			// Token: 0x04000B3C RID: 2876
			public int parameterMap;

			// Token: 0x04000B3D RID: 2877
			public int m_RecordMap;

			// Token: 0x04000B3E RID: 2878
			public static MyResizer.RECT m_ServiceMap = default(MyResizer.RECT);
		}

		// Token: 0x020001DB RID: 475
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private class MONITORINFO
		{
			// Token: 0x06001621 RID: 5665 RVA: 0x0000C744 File Offset: 0x0000A944
			public MONITORINFO()
			{
				this._InvocationMap = Marshal.SizeOf(typeof(MyResizer.MONITORINFO));
				this._ProxyMap = default(MyResizer.RECT);
				this.messageMap = default(MyResizer.RECT);
				this._CreatorMap = 0;
			}

			// Token: 0x04000B3F RID: 2879
			public int _InvocationMap;

			// Token: 0x04000B40 RID: 2880
			public MyResizer.RECT _ProxyMap;

			// Token: 0x04000B41 RID: 2881
			public MyResizer.RECT messageMap;

			// Token: 0x04000B42 RID: 2882
			public int _CreatorMap;
		}

		// Token: 0x020001DC RID: 476
		private struct PointAPI
		{
			// Token: 0x04000B43 RID: 2883
			public int X;

			// Token: 0x04000B44 RID: 2884
			public int Y;
		}
	}
}
