using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x02000027 RID: 39
	[StandardModule]
	public sealed class ModAnimation
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
		public static void AniDispose(MyCard Control, bool RemoveFromChildren, ParameterizedThreadStart CallBack = null)
		{
			if (Control.IsHitTestVisible)
			{
				Control.IsHitTestVisible = false;
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaScaleTransform(Control, -0.08, 200, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false),
					ModAnimation.AaOpacity(Control, -1.0, 200, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
					ModAnimation.AaHeight(Control, -Control.ActualHeight, 150, 100, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
					ModAnimation.AaCode(delegate
					{
						if (RemoveFromChildren)
						{
							if (Control.Parent == null)
							{
								return;
							}
							object[] array3;
							bool[] array4;
							NewLateBinding.LateCall(NewLateBinding.LateGet(Control.Parent, null, "Children", new object[0], null, null, null), null, "Remove", array3 = new object[]
							{
								Control
							}, null, null, array4 = new bool[]
							{
								true
							}, true);
							if (array4[0])
							{
								Control = (MyCard)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[0]), typeof(MyCard));
							}
						}
						else
						{
							Control.Visibility = Visibility.Collapsed;
						}
						if (CallBack != null)
						{
							CallBack(Control);
						}
					}, 0, true)
				}, "MyCard Dispose " + Conversions.ToString(Control.indexer), false);
				return;
			}
			if (RemoveFromChildren)
			{
				if (Control.Parent == null)
				{
					return;
				}
				object[] array;
				bool[] array2;
				NewLateBinding.LateCall(NewLateBinding.LateGet(Control.Parent, null, "Children", new object[0], null, null, null), null, "Remove", array = new object[]
				{
					Control
				}, null, null, array2 = new bool[]
				{
					true
				}, true);
				if (array2[0])
				{
					Control = (MyCard)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[0]), typeof(MyCard));
				}
			}
			else
			{
				Control.Visibility = Visibility.Collapsed;
			}
			if (CallBack != null)
			{
				CallBack(Control);
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0001017C File Offset: 0x0000E37C
		public static void AniDispose(MyHint Control, bool RemoveFromChildren, ParameterizedThreadStart CallBack = null)
		{
			if (Control.IsHitTestVisible)
			{
				Control.IsHitTestVisible = false;
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaScaleTransform(Control, -0.08, 200, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false),
					ModAnimation.AaOpacity(Control, -1.0, 200, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
					ModAnimation.AaHeight(Control, -Control.ActualHeight, 150, 100, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
					ModAnimation.AaCode(delegate
					{
						if (RemoveFromChildren)
						{
							object[] array;
							bool[] array2;
							NewLateBinding.LateCall(NewLateBinding.LateGet(Control.Parent, null, "Children", new object[0], null, null, null), null, "Remove", array = new object[]
							{
								Control
							}, null, null, array2 = new bool[]
							{
								true
							}, true);
							if (array2[0])
							{
								Control = (MyHint)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[0]), typeof(MyHint));
							}
						}
						else
						{
							Control.Visibility = Visibility.Collapsed;
						}
						if (CallBack != null)
						{
							CallBack(Control);
						}
					}, 0, true)
				}, "MyCard Dispose " + Conversions.ToString(Control.m_ParamBroadcaster), false);
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0001027C File Offset: 0x0000E47C
		public static void AniDispose(MyIconButton Control, bool RemoveFromChildren, ParameterizedThreadStart CallBack = null)
		{
			if (Control.IsHitTestVisible)
			{
				Control.IsHitTestVisible = false;
				ModAnimation.AniStart(new ModAnimation.AniData[]
				{
					ModAnimation.AaScaleTransform(Control, -1.5, 200, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false),
					ModAnimation.AaCode(delegate
					{
						if (RemoveFromChildren)
						{
							object[] array;
							bool[] array2;
							NewLateBinding.LateCall(NewLateBinding.LateGet(Control.Parent, null, "Children", new object[0], null, null, null), null, "Remove", array = new object[]
							{
								Control
							}, null, null, array2 = new bool[]
							{
								true
							}, true);
							if (array2[0])
							{
								Control = (MyIconButton)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[0]), typeof(MyIconButton));
							}
						}
						else
						{
							Control.Visibility = Visibility.Collapsed;
						}
						if (CallBack != null)
						{
							CallBack(Control);
						}
					}, 0, true)
				}, "MyIconButton Dispose " + Conversions.ToString(Control.authenticationComposer), false);
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00002A66 File Offset: 0x00000C66
		public static int CalcParser()
		{
			return ModAnimation._Token;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00010328 File Offset: 0x0000E528
		public static void AssetParser(int value)
		{
			object writer = ModAnimation._Writer;
			ObjectFlowControl.CheckForSyncLockOnValueType(writer);
			lock (writer)
			{
				ModAnimation._Token = value;
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00010370 File Offset: 0x0000E570
		public static ModAnimation.AniData AaX(object Obj, double Value, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.Number,
				m_ResolverRepository = ModAnimation.AniTypeSub.X,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				printerRepository = After,
				roleRepository = checked(0 - Delay)
			};
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000103E0 File Offset: 0x0000E5E0
		public static ModAnimation.AniData AaY(object Obj, double Value, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.Number,
				m_ResolverRepository = ModAnimation.AniTypeSub.Y,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				printerRepository = After,
				roleRepository = checked(0 - Delay)
			};
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00010450 File Offset: 0x0000E650
		public static ModAnimation.AniData AaWidth(object Obj, double Value, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.Number,
				m_ResolverRepository = ModAnimation.AniTypeSub.Width,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				printerRepository = After,
				roleRepository = checked(0 - Delay)
			};
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000104C0 File Offset: 0x0000E6C0
		public static ModAnimation.AniData AaHeight(object Obj, double Value, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.Number,
				m_ResolverRepository = ModAnimation.AniTypeSub.Height,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				printerRepository = After,
				roleRepository = checked(0 - Delay)
			};
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00010530 File Offset: 0x0000E730
		public static ModAnimation.AniData AaOpacity(object Obj, double Value, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.Number,
				m_ResolverRepository = ModAnimation.AniTypeSub.Opacity,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				printerRepository = After,
				roleRepository = checked(0 - Delay)
			};
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000105A0 File Offset: 0x0000E7A0
		public static ModAnimation.AniData AaValue(object Obj, double Value, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.Number,
				m_ResolverRepository = ModAnimation.AniTypeSub.Value,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				printerRepository = After,
				roleRepository = checked(0 - Delay)
			};
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00010610 File Offset: 0x0000E810
		public static ModAnimation.AniData AaRadius(object Obj, double Value, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.Number,
				m_ResolverRepository = ModAnimation.AniTypeSub.Radius,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				printerRepository = After,
				roleRepository = checked(0 - Delay)
			};
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00010680 File Offset: 0x0000E880
		public static ModAnimation.AniData AaBorderThickness(object Obj, double Value, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.Number,
				m_ResolverRepository = ModAnimation.AniTypeSub.BorderThickness,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				printerRepository = After,
				roleRepository = checked(0 - Delay)
			};
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000106F0 File Offset: 0x0000E8F0
		public static ModAnimation.AniData AaStrokeThickness(object Obj, double Value, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.Number,
				m_ResolverRepository = ModAnimation.AniTypeSub.StrokeThickness,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				printerRepository = After,
				roleRepository = checked(0 - Delay)
			};
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00010760 File Offset: 0x0000E960
		public static ModAnimation.AniData AaGridLengthWidth(object Obj, double Value, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.Number,
				m_ResolverRepository = ModAnimation.AniTypeSub.GridLengthWidth,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				printerRepository = After,
				roleRepository = checked(0 - Delay)
			};
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000107D0 File Offset: 0x0000E9D0
		public static ModAnimation.AniData AaDouble(object Obj, DependencyProperty Prop, double Value, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.Number,
				m_ResolverRepository = ModAnimation.AniTypeSub.Double,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = new object[]
				{
					Obj,
					Prop,
					""
				},
				Value = Value,
				printerRepository = After,
				roleRepository = checked(0 - Delay)
			};
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00010850 File Offset: 0x0000EA50
		public static ModAnimation.AniData AaDouble(ParameterizedThreadStart Lambda, double Value, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.Number,
				m_ResolverRepository = ModAnimation.AniTypeSub.DoubleParam,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = Lambda,
				Value = Value,
				printerRepository = After,
				roleRepository = checked(0 - Delay)
			};
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000108BC File Offset: 0x0000EABC
		public static ModAnimation.AniData AaColor(FrameworkElement Obj, DependencyProperty Prop, ModBase.MyColor Value, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.Color,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = new object[]
				{
					Obj,
					Prop,
					""
				},
				Value = Value,
				printerRepository = After,
				roleRepository = checked(0 - Delay),
				_CandidateRepository = new ModBase.MyColor(0.0, 0.0, 0.0, 0.0)
			};
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00010960 File Offset: 0x0000EB60
		public static ModAnimation.AniData AaColor(FrameworkElement Obj, DependencyProperty Prop, string Res, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.Color,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = new object[]
				{
					Obj,
					Prop,
					Res
				},
				Value = new ModBase.MyColor(RuntimeHelpers.GetObjectValue(Application.Current.FindResource(Res))) - new ModBase.MyColor(RuntimeHelpers.GetObjectValue(Obj.GetValue(Prop))),
				printerRepository = After,
				roleRepository = checked(0 - Delay),
				_CandidateRepository = new ModBase.MyColor(0.0, 0.0, 0.0, 0.0)
			};
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00010A28 File Offset: 0x0000EC28
		public static ModAnimation.AniData AaScale(object Obj, double Value, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false, bool Absolute = false)
		{
			ModBase.MyRect value;
			if (Absolute)
			{
				value = new ModBase.MyRect(-0.5 * Value, -0.5 * Value, Value, Value);
			}
			else
			{
				value = new ModBase.MyRect(Conversions.ToDouble(Operators.MultiplyObject(Operators.MultiplyObject(-0.5, NewLateBinding.LateGet(Obj, null, "ActualWidth", new object[0], null, null, null)), Value)), Conversions.ToDouble(Operators.MultiplyObject(Operators.MultiplyObject(-0.5, NewLateBinding.LateGet(Obj, null, "ActualHeight", new object[0], null, null, null)), Value)), Conversions.ToDouble(Operators.MultiplyObject(NewLateBinding.LateGet(Obj, null, "ActualWidth", new object[0], null, null, null), Value)), Conversions.ToDouble(Operators.MultiplyObject(NewLateBinding.LateGet(Obj, null, "ActualHeight", new object[0], null, null, null), Value)));
			}
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.Scale,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = RuntimeHelpers.GetObjectValue(Obj),
				Value = value,
				printerRepository = After,
				roleRepository = checked(0 - Delay)
			};
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00010B70 File Offset: 0x0000ED70
		public static ModAnimation.AniData AaTextAppear(object Obj, bool Hide = false, bool TimePerText = true, int Time = 70, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return checked(new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.TextAppear,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				statusRepository = (TimePerText ? (Time * ((Obj is TextBlock) ? NewLateBinding.LateGet(Obj, null, "Text", new object[0], null, null, null) : NewLateBinding.LateGet(Obj, null, "Context", new object[0], null, null, null).ToString()).ToString().Length) : Time),
				_AttrRepository = RuntimeHelpers.GetObjectValue(Obj),
				Value = new object[]
				{
					(Obj is TextBlock) ? NewLateBinding.LateGet(Obj, null, "Text", new object[0], null, null, null) : NewLateBinding.LateGet(Obj, null, "Context", new object[0], null, null, null).ToString(),
					Hide
				},
				printerRepository = After,
				roleRepository = 0 - Delay
			});
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00010C68 File Offset: 0x0000EE68
		public static ModAnimation.AniData AaCode(ThreadStart Code, int Delay = 0, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.Code,
				statusRepository = 1,
				Value = Code,
				printerRepository = After,
				roleRepository = checked(0 - Delay)
			};
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00010CA8 File Offset: 0x0000EEA8
		public static ModAnimation.AniData AaScaleTransform(object Obj, double Value, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.ScaleTransform,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				printerRepository = After,
				roleRepository = checked(0 - Delay)
			};
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00010D10 File Offset: 0x0000EF10
		public static ModAnimation.AniData AaRotateTransform(object Obj, double Value, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.RotateTransform,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				printerRepository = After,
				roleRepository = checked(0 - Delay)
			};
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00010D78 File Offset: 0x0000EF78
		public static ModAnimation.AniData AaTranslateX(object Obj, double Value, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.Number,
				m_ResolverRepository = ModAnimation.AniTypeSub.TranslateX,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				printerRepository = After,
				roleRepository = checked(0 - Delay)
			};
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00010DE8 File Offset: 0x0000EFE8
		public static ModAnimation.AniData AaTranslateY(object Obj, double Value, int Time = 400, int Delay = 0, ModAnimation.AniEase Ease = null, bool After = false)
		{
			return new ModAnimation.AniData
			{
				serverRepository = ModAnimation.AniType.Number,
				m_ResolverRepository = ModAnimation.AniTypeSub.TranslateY,
				statusRepository = Time,
				_ValRepository = (Ease ?? new ModAnimation.AniEaseLinear()),
				_AttrRepository = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				printerRepository = After,
				roleRepository = checked(0 - Delay)
			};
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00010E58 File Offset: 0x0000F058
		public static List<ModAnimation.AniData> AaStack(StackPanel Stack, int Time = 100, int Delay = 25)
		{
			List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
			int num = 0;
			checked
			{
				try
				{
					foreach (object obj in Stack.Children)
					{
						object objectValue = RuntimeHelpers.GetObjectValue(obj);
						NewLateBinding.LateSet(objectValue, null, "Opacity", new object[]
						{
							0
						}, null, null);
						list.Add(ModAnimation.AaOpacity(RuntimeHelpers.GetObjectValue(objectValue), 1.0, Time, num, null, false));
						num += Delay;
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
				return list;
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00010EF8 File Offset: 0x0000F0F8
		public static void AniStart(IList AniGroup, string Name = "", bool RefreshTime = false)
		{
			if (RefreshTime)
			{
				ModAnimation.configuration = ModBase.GetTimeTick();
			}
			ModAnimation.AniGroupEntry aniGroupEntry = new ModAnimation.AniGroupEntry
			{
				importerRepository = ModBase.GetFullList<ModAnimation.AniData>(AniGroup),
				m_WorkerRepository = ModBase.GetTimeTick()
			};
			if (Operators.CompareString(Name, "", false) == 0)
			{
				Name = Conversions.ToString(aniGroupEntry.m_ConnectionRepository);
			}
			else
			{
				ModAnimation.AniStop(Name);
			}
			ModAnimation._Consumer.Add(Name, aniGroupEntry);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00002A6D File Offset: 0x00000C6D
		public static void AniStart(ModAnimation.AniData AniGroup, string Name = "", bool RefreshTime = false)
		{
			ModAnimation.AniStart(new List<ModAnimation.AniData>
			{
				AniGroup
			}, Name, RefreshTime);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00002A82 File Offset: 0x00000C82
		public static void AniStop(string Name)
		{
			ModAnimation._Consumer.Remove(Name);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00002A90 File Offset: 0x00000C90
		public static bool AniIsRun(string Name)
		{
			return ModAnimation._Consumer.ContainsKey(Name);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00010F60 File Offset: 0x0000F160
		public static void AniStart()
		{
			ModAnimation.configuration = ModBase.GetTimeTick();
			ModAnimation._Setter = ModAnimation.configuration;
			ModAnimation._Getter = true;
			ModBase.RunInNewThread((ModAnimation._Closure$__.$I60-0 == null) ? (ModAnimation._Closure$__.$I60-0 = checked(delegate()
			{
				try
				{
					ModBase.Log("[Animation] 动画线程开始", ModBase.LogLevel.Normal, "出现错误");
					for (;;)
					{
						ModAnimation._Closure$__60-0 CS$<>8__locals1 = new ModAnimation._Closure$__60-0(CS$<>8__locals1);
						CS$<>8__locals1.$VB$Local_DeltaTime = (long)Math.Round(ModBase.MathClamp((double)(ModBase.GetTimeTick() - ModAnimation.configuration), 0.0, 100000.0));
						if (CS$<>8__locals1.$VB$Local_DeltaTime >= 3L)
						{
							ModAnimation.configuration = ModBase.GetTimeTick();
							if (ModBase._TokenRepository)
							{
								if (ModBase.MathClamp((double)(ModAnimation.configuration - ModAnimation._Setter), 0.0, 100000.0) >= 500.0)
								{
									ModAnimation.factory = ModAnimation._Proccesor;
									ModAnimation._Proccesor = 0;
									ModAnimation._Setter = ModAnimation.configuration;
								}
								ModAnimation._Proccesor += 2;
							}
							ModBase.RunInUiWait(delegate()
							{
								ModAnimation.m_Registry = 0;
								ModAnimation.AniTimer((int)Math.Round(unchecked((double)CS$<>8__locals1.$VB$Local_DeltaTime * ModAnimation.m_Task)));
								if (ModBase.RandomInteger(0, 64 * (ModBase._TokenRepository ? 5 : 30)) == 0 && ((ModAnimation.factory < 62 && ModAnimation.factory > 0) || ModAnimation.m_Registry > 4 || ModNet.tokenTests._CollectionTest != 0))
								{
									ModBase.Log(string.Concat(new string[]
									{
										"[Report] FPS ",
										Conversions.ToString(ModAnimation.factory),
										", 动画 ",
										Conversions.ToString(ModAnimation.m_Registry),
										", 下载中 ",
										Conversions.ToString(ModNet.tokenTests._CollectionTest),
										"（",
										ModBase.GetString(ModNet.tokenTests.m_BridgeTest),
										"/s）"
									}), ModBase.LogLevel.Normal, "出现错误");
								}
							});
						}
						Thread.Sleep(1);
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "动画帧执行失败", ModBase.LogLevel.Assert, "出现错误");
				}
			})) : ModAnimation._Closure$__.$I60-0, "Animation", ThreadPriority.AboveNormal);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00010FB8 File Offset: 0x0000F1B8
		public static void AniTimer(int DeltaTick)
		{
			checked
			{
				try
				{
					if ((double)DeltaTick / ModAnimation.m_Task > 200.0)
					{
						ModBase.Log("[Animation] 两个动画帧间隔 " + Conversions.ToString(DeltaTick) + " ms", ModBase.LogLevel.Developer, "出现错误");
					}
					int num = -1;
					IL_2AE:
					while (num + 1 < ModAnimation._Consumer.Count)
					{
						num++;
						ModAnimation.AniGroupEntry aniGroupEntry = Enumerable.ElementAtOrDefault<ModAnimation.AniGroupEntry>(ModAnimation._Consumer.Values, num);
						if (aniGroupEntry.m_WorkerRepository <= ModAnimation.configuration)
						{
							bool flag = true;
							int i = 0;
							while (i < aniGroupEntry.importerRepository.Count)
							{
								ModAnimation.AniData aniData = aniGroupEntry.importerRepository[i];
								if (!aniData.printerRepository)
								{
									flag = false;
									ref int ptr = ref aniData.roleRepository;
									aniData.roleRepository = ptr + DeltaTick;
									if (aniData.roleRepository > 0)
									{
										aniData = ModAnimation.AniRun(aniData);
										ModAnimation.m_Registry++;
									}
									if (aniData.roleRepository >= aniData.statusRepository)
									{
										if (Conversions.ToBoolean(aniData.serverRepository == ModAnimation.AniType.Color && Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(NewLateBinding.LateIndexGet(aniData._AttrRepository, new object[]
										{
											2
										}, null), "", false)))))
										{
											object instance = NewLateBinding.LateIndexGet(aniData._AttrRepository, new object[]
											{
												0
											}, null);
											Type type = null;
											string memberName = "SetResourceReference";
											object[] array = new object[2];
											int num2 = 0;
											object attrRepository = aniData._AttrRepository;
											object instance2 = attrRepository;
											object[] array2 = new object[1];
											object obj = array2[0] = 1;
											array[num2] = NewLateBinding.LateIndexGet(instance2, array2, null);
											int num3 = 1;
											object attrRepository2 = aniData._AttrRepository;
											object instance3 = attrRepository2;
											object[] array3 = new object[1];
											object obj2 = array3[0] = 2;
											array[num3] = NewLateBinding.LateIndexGet(instance3, array3, null);
											object[] array4 = array;
											bool[] array5;
											NewLateBinding.LateCall(instance, type, memberName, array, null, null, array5 = new bool[]
											{
												true,
												true
											}, true);
											if (array5[0])
											{
												NewLateBinding.LateIndexSetComplex(attrRepository, new object[]
												{
													obj,
													array4[0]
												}, null, true, false);
											}
											if (array5[1])
											{
												NewLateBinding.LateIndexSetComplex(attrRepository2, new object[]
												{
													obj2,
													array4[1]
												}, null, true, false);
											}
										}
										aniGroupEntry.importerRepository.RemoveAt(i);
									}
									else
									{
										aniGroupEntry.importerRepository[i] = aniData;
										i++;
									}
								}
								else
								{
									if (!flag)
									{
										break;
									}
									flag = false;
									aniData.printerRepository = false;
									aniGroupEntry.importerRepository[i] = aniData;
								}
							}
							if (!Enumerable.Any<ModAnimation.AniData>(aniGroupEntry.importerRepository))
							{
								int num4 = ModAnimation._Consumer.Count - 1;
								for (int j = 0; j <= num4; j++)
								{
									if (Enumerable.ElementAt<KeyValuePair<string, ModAnimation.AniGroupEntry>>(ModAnimation._Consumer, j).Value.m_ConnectionRepository == aniGroupEntry.m_ConnectionRepository)
									{
										ModAnimation._Consumer.Remove(Enumerable.ElementAt<KeyValuePair<string, ModAnimation.AniGroupEntry>>(ModAnimation._Consumer, j).Key);
										IL_2AA:
										num--;
										goto IL_2AE;
									}
								}
								goto IL_2AA;
							}
						}
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "动画刻执行失败", ModBase.LogLevel.Hint, "出现错误");
				}
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000112C4 File Offset: 0x0000F4C4
		private static ModAnimation.AniData AniRun(ModAnimation.AniData Ani)
		{
			try
			{
				switch (Ani.serverRepository)
				{
				case ModAnimation.AniType.Number:
				{
					double num = ModBase.MathPercent(0.0, Conversions.ToDouble(Ani.Value), Ani._ValRepository.GetDelta((double)Ani.roleRepository / (double)Ani.statusRepository, Ani.m_StructRepository));
					if (num != 0.0)
					{
						switch (Ani.m_ResolverRepository)
						{
						case ModAnimation.AniTypeSub.X:
							ModBase.DeltaLeft((FrameworkElement)Ani._AttrRepository, num);
							break;
						case ModAnimation.AniTypeSub.Y:
							ModBase.DeltaTop((FrameworkElement)Ani._AttrRepository, num);
							break;
						case ModAnimation.AniTypeSub.Width:
						{
							FrameworkElement frameworkElement = (FrameworkElement)Ani._AttrRepository;
							frameworkElement.Width = Math.Max((double.IsNaN(frameworkElement.Width) ? frameworkElement.ActualWidth : frameworkElement.Width) + num, 0.0);
							break;
						}
						case ModAnimation.AniTypeSub.Height:
						{
							FrameworkElement frameworkElement2 = (FrameworkElement)Ani._AttrRepository;
							frameworkElement2.Height = Math.Max((double.IsNaN(frameworkElement2.Height) ? frameworkElement2.ActualHeight : frameworkElement2.Height) + num, 0.0);
							break;
						}
						case ModAnimation.AniTypeSub.Opacity:
							NewLateBinding.LateSet(Ani._AttrRepository, null, "Opacity", new object[]
							{
								ModBase.MathClamp(Conversions.ToDouble(Operators.AddObject(NewLateBinding.LateGet(Ani._AttrRepository, null, "Opacity", new object[0], null, null, null), num)), 0.0, 1.0)
							}, null, null);
							break;
						case ModAnimation.AniTypeSub.Value:
						{
							object attrRepository = Ani._AttrRepository;
							NewLateBinding.LateSet(attrRepository, null, "Value", new object[]
							{
								Operators.AddObject(NewLateBinding.LateGet(attrRepository, null, "Value", new object[0], null, null, null), num)
							}, null, null);
							break;
						}
						case ModAnimation.AniTypeSub.Radius:
						{
							object attrRepository = Ani._AttrRepository;
							NewLateBinding.LateSet(attrRepository, null, "Radius", new object[]
							{
								Operators.AddObject(NewLateBinding.LateGet(attrRepository, null, "Radius", new object[0], null, null, null), num)
							}, null, null);
							break;
						}
						case ModAnimation.AniTypeSub.BorderThickness:
						{
							object attrRepository2 = Ani._AttrRepository;
							Type type = null;
							string memberName = "BorderThickness";
							object[] array = new object[1];
							int num2 = 0;
							object obj = NewLateBinding.LateGet(Ani._AttrRepository, null, "BorderThickness", new object[0], null, null, null);
							array[num2] = new Thickness(((obj != null) ? ((Thickness)obj) : default(Thickness)).Bottom + num);
							NewLateBinding.LateSet(attrRepository2, type, memberName, array, null, null);
							break;
						}
						case ModAnimation.AniTypeSub.StrokeThickness:
							NewLateBinding.LateSet(Ani._AttrRepository, null, "StrokeThickness", new object[]
							{
								NewLateBinding.LateGet(null, typeof(Math), "Max", new object[]
								{
									Operators.AddObject(NewLateBinding.LateGet(Ani._AttrRepository, null, "StrokeThickness", new object[0], null, null, null), num),
									0
								}, null, null, null)
							}, null, null);
							break;
						case ModAnimation.AniTypeSub.TranslateX:
						{
							if (Information.IsNothing(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(Ani._AttrRepository, null, "RenderTransform", new object[0], null, null, null))) || !(NewLateBinding.LateGet(Ani._AttrRepository, null, "RenderTransform", new object[0], null, null, null) is TranslateTransform))
							{
								NewLateBinding.LateSet(Ani._AttrRepository, null, "RenderTransform", new object[]
								{
									new TranslateTransform(0.0, 0.0)
								}, null, null);
							}
							TranslateTransform translateTransform;
							(translateTransform = (TranslateTransform)NewLateBinding.LateGet(Ani._AttrRepository, null, "RenderTransform", new object[0], null, null, null)).X = translateTransform.X + num;
							break;
						}
						case ModAnimation.AniTypeSub.TranslateY:
						{
							if (Information.IsNothing(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(Ani._AttrRepository, null, "RenderTransform", new object[0], null, null, null))) || !(NewLateBinding.LateGet(Ani._AttrRepository, null, "RenderTransform", new object[0], null, null, null) is TranslateTransform))
							{
								NewLateBinding.LateSet(Ani._AttrRepository, null, "RenderTransform", new object[]
								{
									new TranslateTransform(0.0, 0.0)
								}, null, null);
							}
							TranslateTransform translateTransform;
							(translateTransform = (TranslateTransform)NewLateBinding.LateGet(Ani._AttrRepository, null, "RenderTransform", new object[0], null, null, null)).Y = translateTransform.Y + num;
							break;
						}
						case ModAnimation.AniTypeSub.Double:
						{
							object instance = NewLateBinding.LateIndexGet(Ani._AttrRepository, new object[]
							{
								0
							}, null);
							Type type2 = null;
							string memberName2 = "SetValue";
							object[] array2 = new object[2];
							int num3 = 0;
							object attrRepository = Ani._AttrRepository;
							object instance2 = attrRepository;
							object[] array3 = new object[1];
							object obj2 = array3[0] = 1;
							array2[num3] = NewLateBinding.LateIndexGet(instance2, array3, null);
							int num4 = 1;
							object instance3 = NewLateBinding.LateIndexGet(Ani._AttrRepository, new object[]
							{
								0
							}, null);
							Type type3 = null;
							string memberName3 = "GetValue";
							object[] array4 = new object[1];
							int num5 = 0;
							object attrRepository3 = Ani._AttrRepository;
							object instance4 = attrRepository3;
							object[] array5 = new object[1];
							object obj3 = array5[0] = 1;
							array4[num5] = NewLateBinding.LateIndexGet(instance4, array5, null);
							object[] array6 = array4;
							bool[] array7;
							object left = NewLateBinding.LateGet(instance3, type3, memberName3, array4, null, null, array7 = new bool[]
							{
								true
							});
							if (array7[0])
							{
								NewLateBinding.LateIndexSetComplex(attrRepository3, new object[]
								{
									obj3,
									array6[0]
								}, null, true, false);
							}
							array2[num4] = Operators.AddObject(left, num);
							object[] array8 = array2;
							string[] argumentNames = null;
							Type[] typeArguments = null;
							bool[] array9 = new bool[2];
							array9[0] = true;
							bool[] array10 = array9;
							NewLateBinding.LateCall(instance, type2, memberName2, array2, argumentNames, typeArguments, array9, true);
							if (array10[0])
							{
								NewLateBinding.LateIndexSetComplex(attrRepository, new object[]
								{
									obj2,
									array8[0]
								}, null, true, false);
							}
							break;
						}
						case ModAnimation.AniTypeSub.DoubleParam:
							((ParameterizedThreadStart)Ani._AttrRepository)(num);
							break;
						case ModAnimation.AniTypeSub.GridLengthWidth:
							NewLateBinding.LateSet(Ani._AttrRepository, null, "Width", new object[]
							{
								new GridLength(Conversions.ToDouble(NewLateBinding.LateGet(null, typeof(Math), "Max", new object[]
								{
									Operators.AddObject(NewLateBinding.LateGet(NewLateBinding.LateGet(Ani._AttrRepository, null, "Width", new object[0], null, null, null), null, "Value", new object[0], null, null, null), num),
									0
								}, null, null, null)), GridUnitType.Star)
							}, null, null);
							break;
						}
					}
					break;
				}
				case ModAnimation.AniType.Color:
				{
					ModBase.MyColor b = ModBase.MathPercent(new ModBase.MyColor(0.0, 0.0, 0.0, 0.0), (ModBase.MyColor)Ani.Value, Ani._ValRepository.GetDelta((double)Ani.roleRepository / (double)Ani.statusRepository, Ani.m_StructRepository)) + (ModBase.MyColor)Ani._CandidateRepository;
					FrameworkElement frameworkElement3 = (FrameworkElement)NewLateBinding.LateIndexGet(Ani._AttrRepository, new object[]
					{
						0
					}, null);
					DependencyProperty dependencyProperty = (DependencyProperty)NewLateBinding.LateIndexGet(Ani._AttrRepository, new object[]
					{
						1
					}, null);
					ModBase.MyColor myColor = new ModBase.MyColor(RuntimeHelpers.GetObjectValue(frameworkElement3.GetValue(dependencyProperty))) + b;
					frameworkElement3.SetValue(dependencyProperty, RuntimeHelpers.GetObjectValue((Operators.CompareString(dependencyProperty.PropertyType.Name, "Color", false) == 0) ? myColor : myColor));
					Ani._CandidateRepository = myColor - new ModBase.MyColor(RuntimeHelpers.GetObjectValue(frameworkElement3.GetValue(dependencyProperty)));
					break;
				}
				case ModAnimation.AniType.Scale:
				{
					FrameworkElement frameworkElement4 = (FrameworkElement)Ani._AttrRepository;
					double delta = Ani._ValRepository.GetDelta((double)Ani.roleRepository / (double)Ani.statusRepository, Ani.m_StructRepository);
					frameworkElement4.Margin = new Thickness(frameworkElement4.Margin.Left + ModBase.MathPercent(0.0, Conversions.ToDouble(NewLateBinding.LateGet(Ani.Value, null, "Left", new object[0], null, null, null)), delta), frameworkElement4.Margin.Top + ModBase.MathPercent(0.0, Conversions.ToDouble(NewLateBinding.LateGet(Ani.Value, null, "Top", new object[0], null, null, null)), delta), frameworkElement4.Margin.Right + ModBase.MathPercent(0.0, Conversions.ToDouble(NewLateBinding.LateGet(Ani.Value, null, "Left", new object[0], null, null, null)), delta), frameworkElement4.Margin.Bottom + ModBase.MathPercent(0.0, Conversions.ToDouble(NewLateBinding.LateGet(Ani.Value, null, "Top", new object[0], null, null, null)), delta));
					frameworkElement4.Width = Math.Max(frameworkElement4.Width + ModBase.MathPercent(0.0, Conversions.ToDouble(NewLateBinding.LateGet(Ani.Value, null, "Width", new object[0], null, null, null)), delta), 0.0);
					frameworkElement4.Height = Math.Max(frameworkElement4.Height + ModBase.MathPercent(0.0, Conversions.ToDouble(NewLateBinding.LateGet(Ani.Value, null, "Height", new object[0], null, null, null)), delta), 0.0);
					break;
				}
				case ModAnimation.AniType.TextAppear:
					checked
					{
						int num6 = (int)Math.Round(unchecked((double)(Conversions.ToBoolean(NewLateBinding.LateIndexGet(Ani.Value, new object[]
						{
							1
						}, null)) ? NewLateBinding.LateIndexGet(Ani.Value, new object[]
						{
							0
						}, null).ToString().Length : 0) + Math.Round((double)(checked(NewLateBinding.LateIndexGet(Ani.Value, new object[]
						{
							0
						}, null).ToString().Length * (Conversions.ToBoolean(NewLateBinding.LateIndexGet(Ani.Value, new object[]
						{
							1
						}, null)) ? -1 : 1))) * Ani._ValRepository.GetDelta((double)Ani.roleRepository / (double)Ani.statusRepository, 0.0))));
						string text = Strings.Mid(Conversions.ToString(NewLateBinding.LateIndexGet(Ani.Value, new object[]
						{
							0
						}, null)), 1, num6);
						if (num6 < NewLateBinding.LateIndexGet(Ani.Value, new object[]
						{
							0
						}, null).ToString().Length)
						{
							if (Convert.ToInt32(Convert.ToChar(Strings.Mid(Conversions.ToString(NewLateBinding.LateIndexGet(Ani.Value, new object[]
							{
								0
							}, null)), num6 + 1, 1))) >= Convert.ToInt32(Convert.ToChar(128)))
							{
								text += Encoding.GetEncoding("GB18030").GetString(new byte[]
								{
									(byte)ModBase.RandomInteger(176, 247),
									(byte)ModBase.RandomInteger(161, 249)
								});
							}
							else
							{
								text += Conversions.ToString(ModBase.RandomOne<char>("0123456789./*-+\\[]{};':/?,!@#$%^&*()_+-=qwwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray()));
							}
						}
						if (Ani._AttrRepository is TextBlock)
						{
							NewLateBinding.LateSet(Ani._AttrRepository, null, "Text", new object[]
							{
								text
							}, null, null);
						}
						else
						{
							NewLateBinding.LateSet(Ani._AttrRepository, null, "Context", new object[]
							{
								text
							}, null, null);
						}
						break;
					}
				case ModAnimation.AniType.Code:
					((ThreadStart)Ani.Value)();
					break;
				case ModAnimation.AniType.ScaleTransform:
				{
					FrameworkElement frameworkElement5 = (FrameworkElement)Ani._AttrRepository;
					if (!(frameworkElement5.RenderTransform is ScaleTransform))
					{
						frameworkElement5.RenderTransformOrigin = new Point(0.5, 0.5);
						frameworkElement5.RenderTransform = new ScaleTransform(1.0, 1.0);
					}
					double num7 = ModBase.MathPercent(0.0, Conversions.ToDouble(Ani.Value), Ani._ValRepository.GetDelta((double)Ani.roleRepository / (double)Ani.statusRepository, Ani.m_StructRepository));
					((ScaleTransform)frameworkElement5.RenderTransform).ScaleX = Math.Max(((ScaleTransform)frameworkElement5.RenderTransform).ScaleX + num7, 0.0);
					((ScaleTransform)frameworkElement5.RenderTransform).ScaleY = Math.Max(((ScaleTransform)frameworkElement5.RenderTransform).ScaleY + num7, 0.0);
					break;
				}
				case ModAnimation.AniType.RotateTransform:
				{
					FrameworkElement frameworkElement6 = (FrameworkElement)Ani._AttrRepository;
					if (!(frameworkElement6.RenderTransform is RotateTransform))
					{
						frameworkElement6.RenderTransformOrigin = new Point(0.5, 0.5);
						frameworkElement6.RenderTransform = new RotateTransform(0.0);
					}
					double num8 = ModBase.MathPercent(0.0, Conversions.ToDouble(Ani.Value), Ani._ValRepository.GetDelta((double)Ani.roleRepository / (double)Ani.statusRepository, Ani.m_StructRepository));
					((RotateTransform)frameworkElement6.RenderTransform).Angle = ((RotateTransform)frameworkElement6.RenderTransform).Angle + num8;
					break;
				}
				}
				Ani.m_StructRepository = (double)Ani.roleRepository / (double)Ani.statusRepository;
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "执行动画失败：" + Ani.ToString(), ModBase.LogLevel.Hint, "出现错误");
			}
			return Ani;
		}

		// Token: 0x04000039 RID: 57
		public static double m_Task = 1.0;

		// Token: 0x0400003A RID: 58
		public static Dictionary<string, ModAnimation.AniGroupEntry> _Consumer = new Dictionary<string, ModAnimation.AniGroupEntry>();

		// Token: 0x0400003B RID: 59
		private static long configuration;

		// Token: 0x0400003C RID: 60
		public static bool _Getter = false;

		// Token: 0x0400003D RID: 61
		private static int _Token = 0;

		// Token: 0x0400003E RID: 62
		private static readonly object _Writer = RuntimeHelpers.GetObjectValue(new object());

		// Token: 0x0400003F RID: 63
		private static int m_Registry = 0;

		// Token: 0x04000040 RID: 64
		private static int _Proccesor = 0;

		// Token: 0x04000041 RID: 65
		private static long _Setter = 0L;

		// Token: 0x04000042 RID: 66
		public static int factory = 0;

		// Token: 0x02000028 RID: 40
		public class AniGroupEntry
		{
			// Token: 0x06000115 RID: 277 RVA: 0x00002A9D File Offset: 0x00000C9D
			public AniGroupEntry()
			{
				this.m_ConnectionRepository = ModBase.GetUuid();
			}

			// Token: 0x04000043 RID: 67
			public List<ModAnimation.AniData> importerRepository;

			// Token: 0x04000044 RID: 68
			public long m_WorkerRepository;

			// Token: 0x04000045 RID: 69
			public int m_ConnectionRepository;
		}

		// Token: 0x02000029 RID: 41
		public struct AniData
		{
			// Token: 0x06000117 RID: 279 RVA: 0x00012090 File Offset: 0x00010290
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					ModBase.GetStringFromEnum(this.serverRepository),
					" | ",
					Conversions.ToString(this.roleRepository),
					"/",
					Conversions.ToString(this.statusRepository),
					"(",
					Conversions.ToString(Math.Round(this.m_StructRepository * 100.0)),
					"%)",
					(this._AttrRepository == null) ? "" : string.Concat(new string[]
					{
						" | ",
						this._AttrRepository.ToString(),
						"(",
						this._AttrRepository.GetType().Name,
						")"
					})
				});
			}

			// Token: 0x04000046 RID: 70
			public ModAnimation.AniType serverRepository;

			// Token: 0x04000047 RID: 71
			public ModAnimation.AniTypeSub m_ResolverRepository;

			// Token: 0x04000048 RID: 72
			public int statusRepository;

			// Token: 0x04000049 RID: 73
			public int roleRepository;

			// Token: 0x0400004A RID: 74
			public double m_StructRepository;

			// Token: 0x0400004B RID: 75
			public bool printerRepository;

			// Token: 0x0400004C RID: 76
			public ModAnimation.AniEase _ValRepository;

			// Token: 0x0400004D RID: 77
			public object[] _AttrRepository;

			// Token: 0x0400004E RID: 78
			public object[] Value;

			// Token: 0x0400004F RID: 79
			public ModBase.MyColor _CandidateRepository;
		}

		// Token: 0x0200002A RID: 42
		public enum AniType
		{
			// Token: 0x04000051 RID: 81
			Number,
			// Token: 0x04000052 RID: 82
			Color,
			// Token: 0x04000053 RID: 83
			Scale,
			// Token: 0x04000054 RID: 84
			TextAppear,
			// Token: 0x04000055 RID: 85
			Code,
			// Token: 0x04000056 RID: 86
			ScaleTransform,
			// Token: 0x04000057 RID: 87
			RotateTransform
		}

		// Token: 0x0200002B RID: 43
		public enum AniTypeSub
		{
			// Token: 0x04000059 RID: 89
			X,
			// Token: 0x0400005A RID: 90
			Y,
			// Token: 0x0400005B RID: 91
			Width,
			// Token: 0x0400005C RID: 92
			Height,
			// Token: 0x0400005D RID: 93
			Opacity,
			// Token: 0x0400005E RID: 94
			Value,
			// Token: 0x0400005F RID: 95
			Radius,
			// Token: 0x04000060 RID: 96
			BorderThickness,
			// Token: 0x04000061 RID: 97
			StrokeThickness,
			// Token: 0x04000062 RID: 98
			TranslateX,
			// Token: 0x04000063 RID: 99
			TranslateY,
			// Token: 0x04000064 RID: 100
			Double,
			// Token: 0x04000065 RID: 101
			DoubleParam,
			// Token: 0x04000066 RID: 102
			GridLengthWidth
		}

		// Token: 0x0200002C RID: 44
		public enum AniEasePower
		{
			// Token: 0x04000068 RID: 104
			Weak = 2,
			// Token: 0x04000069 RID: 105
			Middle,
			// Token: 0x0400006A RID: 106
			Strong,
			// Token: 0x0400006B RID: 107
			ExtraStrong
		}

		// Token: 0x0200002D RID: 45
		public abstract class AniEase
		{
			// Token: 0x0600011A RID: 282
			public abstract double GetValue(double t);

			// Token: 0x0600011B RID: 283 RVA: 0x00002AB1 File Offset: 0x00000CB1
			public virtual double GetDelta(double t1, double t0)
			{
				return this.GetValue(t1) - this.GetValue(t0);
			}
		}

		// Token: 0x0200002E RID: 46
		public class AniEaseInout : ModAnimation.AniEase
		{
			// Token: 0x0600011D RID: 285 RVA: 0x00002AC2 File Offset: 0x00000CC2
			public AniEaseInout(ModAnimation.AniEase EaseIn, ModAnimation.AniEase EaseOut, double EaseInPercent = 0.5)
			{
				this.advisorRepository = EaseIn;
				this.EaseOut = EaseOut;
				this.accountRepository = EaseInPercent;
			}

			// Token: 0x0600011E RID: 286 RVA: 0x0001216C File Offset: 0x0001036C
			public override double GetValue(double t)
			{
				double result;
				if (t < this.accountRepository)
				{
					result = this.accountRepository * this.advisorRepository.GetValue(t / this.accountRepository);
				}
				else
				{
					result = (1.0 - this.accountRepository) * this.EaseOut.GetValue((t - this.accountRepository) / (1.0 - this.accountRepository)) + this.accountRepository;
				}
				return result;
			}

			// Token: 0x0400006C RID: 108
			private readonly ModAnimation.AniEase advisorRepository;

			// Token: 0x0400006D RID: 109
			private readonly ModAnimation.AniEase EaseOut;

			// Token: 0x0400006E RID: 110
			private readonly double accountRepository;
		}

		// Token: 0x0200002F RID: 47
		public class AniEaseLinear : ModAnimation.AniEase
		{
			// Token: 0x06000121 RID: 289 RVA: 0x00002AE9 File Offset: 0x00000CE9
			public override double GetValue(double t)
			{
				return ModBase.MathClamp(t, 0.0, 1.0);
			}

			// Token: 0x06000122 RID: 290 RVA: 0x00002B03 File Offset: 0x00000D03
			public override double GetDelta(double t1, double t0)
			{
				return ModBase.MathClamp(t1, 0.0, 1.0) - ModBase.MathClamp(t0, 0.0, 1.0);
			}
		}

		// Token: 0x02000030 RID: 48
		public class AniEaseInFluent : ModAnimation.AniEase
		{
			// Token: 0x06000124 RID: 292 RVA: 0x00002B36 File Offset: 0x00000D36
			public AniEaseInFluent(ModAnimation.AniEasePower Power = ModAnimation.AniEasePower.Middle)
			{
				this.queueRepository = Power;
			}

			// Token: 0x06000125 RID: 293 RVA: 0x00002B46 File Offset: 0x00000D46
			public override double GetValue(double t)
			{
				return Math.Pow(ModBase.MathClamp(t, 0.0, 1.0), (double)this.queueRepository);
			}

			// Token: 0x0400006F RID: 111
			private readonly ModAnimation.AniEasePower queueRepository;
		}

		// Token: 0x02000031 RID: 49
		public class AniEaseOutFluent : ModAnimation.AniEase
		{
			// Token: 0x06000127 RID: 295 RVA: 0x00002B6C File Offset: 0x00000D6C
			public AniEaseOutFluent(ModAnimation.AniEasePower Power = ModAnimation.AniEasePower.Middle)
			{
				this.eventRepository = Power;
			}

			// Token: 0x06000128 RID: 296 RVA: 0x00002B7C File Offset: 0x00000D7C
			public override double GetValue(double t)
			{
				return 1.0 - Math.Pow(ModBase.MathClamp(1.0 - t, 0.0, 1.0), (double)this.eventRepository);
			}

			// Token: 0x04000070 RID: 112
			private readonly ModAnimation.AniEasePower eventRepository;
		}

		// Token: 0x02000032 RID: 50
		public class AniEaseInoutFluent : ModAnimation.AniEase
		{
			// Token: 0x0600012A RID: 298 RVA: 0x00002BB6 File Offset: 0x00000DB6
			public AniEaseInoutFluent(ModAnimation.AniEasePower Power = ModAnimation.AniEasePower.Middle, double Middle = 0.5)
			{
				this.m_ManagerRepository = new ModAnimation.AniEaseInout(new ModAnimation.AniEaseInFluent(Power), new ModAnimation.AniEaseOutFluent(Power), Middle);
			}

			// Token: 0x0600012B RID: 299 RVA: 0x00002BD7 File Offset: 0x00000DD7
			public override double GetValue(double t)
			{
				return this.m_ManagerRepository.GetValue(t);
			}

			// Token: 0x04000071 RID: 113
			private ModAnimation.AniEaseInout m_ManagerRepository;
		}

		// Token: 0x02000033 RID: 51
		public class AniEaseInBack : ModAnimation.AniEase
		{
			// Token: 0x0600012D RID: 301 RVA: 0x00002BE5 File Offset: 0x00000DE5
			public AniEaseInBack(ModAnimation.AniEasePower Power = ModAnimation.AniEasePower.Middle)
			{
				this._ModelRepository = 3.0 - (double)Power * 0.5;
			}

			// Token: 0x0600012E RID: 302 RVA: 0x000121E0 File Offset: 0x000103E0
			public override double GetValue(double t)
			{
				t = ModBase.MathClamp(t, 0.0, 1.0);
				return Math.Pow(t, this._ModelRepository) * Math.Cos(4.71238898038469 * (1.0 - t));
			}

			// Token: 0x04000072 RID: 114
			private readonly double _ModelRepository;
		}

		// Token: 0x02000034 RID: 52
		public class AniEaseOutBack : ModAnimation.AniEase
		{
			// Token: 0x06000130 RID: 304 RVA: 0x00002C0A File Offset: 0x00000E0A
			public AniEaseOutBack(ModAnimation.AniEasePower Power = ModAnimation.AniEasePower.Middle)
			{
				this.wrapperRepository = 3.0 - (double)Power * 0.5;
			}

			// Token: 0x06000131 RID: 305 RVA: 0x00012230 File Offset: 0x00010430
			public override double GetValue(double t)
			{
				t = ModBase.MathClamp(t, 0.0, 1.0);
				return 1.0 - Math.Pow(1.0 - t, this.wrapperRepository) * Math.Cos(4.71238898038469 * t);
			}

			// Token: 0x04000073 RID: 115
			private readonly double wrapperRepository;
		}

		// Token: 0x02000035 RID: 53
		public class AniEaseInCar : ModAnimation.AniEase
		{
			// Token: 0x06000133 RID: 307 RVA: 0x00002C2F File Offset: 0x00000E2F
			public AniEaseInCar(double Middle = 0.7, ModAnimation.AniEasePower Power = ModAnimation.AniEasePower.Middle)
			{
				this.baseRepository = new ModAnimation.AniEaseInout(new ModAnimation.AniEaseInBack(Power), new ModAnimation.AniEaseOutFluent(Power), Middle);
			}

			// Token: 0x06000134 RID: 308 RVA: 0x00002C50 File Offset: 0x00000E50
			public override double GetValue(double t)
			{
				return this.baseRepository.GetValue(t);
			}

			// Token: 0x04000074 RID: 116
			private ModAnimation.AniEaseInout baseRepository;
		}

		// Token: 0x02000036 RID: 54
		public class AniEaseOutCar : ModAnimation.AniEase
		{
			// Token: 0x06000136 RID: 310 RVA: 0x00002C5E File Offset: 0x00000E5E
			public AniEaseOutCar(double Middle = 0.3, ModAnimation.AniEasePower Power = ModAnimation.AniEasePower.Middle)
			{
				this.attributeRepository = new ModAnimation.AniEaseInout(new ModAnimation.AniEaseInFluent(Power), new ModAnimation.AniEaseOutBack(Power), Middle);
			}

			// Token: 0x06000137 RID: 311 RVA: 0x00002C7F File Offset: 0x00000E7F
			public override double GetValue(double t)
			{
				return this.attributeRepository.GetValue(t);
			}

			// Token: 0x04000075 RID: 117
			private ModAnimation.AniEaseInout attributeRepository;
		}

		// Token: 0x02000037 RID: 55
		public class AniEaseInElastic : ModAnimation.AniEase
		{
			// Token: 0x06000139 RID: 313 RVA: 0x00002C8D File Offset: 0x00000E8D
			public AniEaseInElastic(ModAnimation.AniEasePower Power = ModAnimation.AniEasePower.Middle)
			{
				this.codeRepository = (int)(checked(Power + 4));
			}

			// Token: 0x0600013A RID: 314 RVA: 0x00012288 File Offset: 0x00010488
			public override double GetValue(double t)
			{
				t = ModBase.MathClamp(t, 0.0, 1.0);
				return Math.Pow(t, (double)(checked(this.codeRepository - 1)) * 0.25) * Math.Cos(((double)this.codeRepository - 3.5) * 3.141592653589793 * Math.Pow(1.0 - t, 1.5));
			}

			// Token: 0x04000076 RID: 118
			private readonly int codeRepository;
		}

		// Token: 0x02000038 RID: 56
		public class AniEaseOutElastic : ModAnimation.AniEase
		{
			// Token: 0x0600013C RID: 316 RVA: 0x00002C9F File Offset: 0x00000E9F
			public AniEaseOutElastic(ModAnimation.AniEasePower Power = ModAnimation.AniEasePower.Middle)
			{
				this.prototypeRepository = (int)(checked(Power + 4));
			}

			// Token: 0x0600013D RID: 317 RVA: 0x00012304 File Offset: 0x00010504
			public override double GetValue(double t)
			{
				t = 1.0 - ModBase.MathClamp(t, 0.0, 1.0);
				return 1.0 - Math.Pow(t, (double)(checked(this.prototypeRepository - 1)) * 0.25) * Math.Cos(((double)this.prototypeRepository - 3.5) * 3.141592653589793 * Math.Pow(1.0 - t, 1.5));
			}

			// Token: 0x04000077 RID: 119
			private readonly int prototypeRepository;
		}
	}
}
