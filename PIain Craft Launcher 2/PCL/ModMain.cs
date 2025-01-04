using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using PCL.My;

namespace PCL
{
	// Token: 0x020001F2 RID: 498
	[StandardModule]
	public sealed class ModMain
	{
		// Token: 0x06001775 RID: 6005 RVA: 0x00097CA8 File Offset: 0x00095EA8
		public static void Hint(string Text, ModMain.HintType Type = ModMain.HintType.Info, bool Log = true)
		{
			if (ModMain.paramsIterator == null)
			{
				ModMain.paramsIterator = new List<ModMain.HintMessage>();
			}
			ModMain.paramsIterator.Add(new ModMain.HintMessage
			{
				regMap = (Text ?? ""),
				Type = Type,
				m_ProductMap = Log
			});
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00097CFC File Offset: 0x00095EFC
		private static void HintTick()
		{
			try
			{
				if (Enumerable.Any<ModMain.HintMessage>(ModMain.paramsIterator))
				{
					while (Enumerable.Any<ModMain.HintMessage>(ModMain.paramsIterator))
					{
						ModMain._Closure$__5-0 CS$<>8__locals1 = new ModMain._Closure$__5-0(CS$<>8__locals1);
						ModMain.HintMessage hintMessage = ModMain.paramsIterator[0];
						hintMessage.regMap = hintMessage.regMap.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
						if (ModMain._ProcessIterator.PanHint.Children.Count < 20)
						{
							CS$<>8__locals1.$VB$Local_DoubleStack = null;
							try
							{
								foreach (object obj in ModMain._ProcessIterator.PanHint.Children)
								{
									Border border = (Border)obj;
									if (Conversions.ToBoolean(Conversions.ToBoolean(NewLateBinding.LateIndexGet(border.Tag, new object[]
									{
										0
									}, null)) && Operators.CompareString(((TextBlock)border.Child).Text, hintMessage.regMap, false) == 0))
									{
										CS$<>8__locals1.$VB$Local_DoubleStack = border;
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
							CS$<>8__locals1.$VB$Local_Percent = 0.3;
							ModMain.HintType type = hintMessage.Type;
							if (type != ModMain.HintType.Info)
							{
								if (type != ModMain.HintType.Finish)
								{
									CS$<>8__locals1.$VB$Local_TargetColor0 = new ModBase.MyColor(215.0, 255.0, 53.0, 11.0);
									CS$<>8__locals1.$VB$Local_TargetColor1 = new ModBase.MyColor(215.0, 255.0, 43.0, 0.0);
								}
								else
								{
									CS$<>8__locals1.$VB$Local_TargetColor0 = new ModBase.MyColor(215.0, 33.0, 177.0, 33.0);
									CS$<>8__locals1.$VB$Local_TargetColor1 = new ModBase.MyColor(215.0, 29.0, 160.0, 29.0);
								}
							}
							else
							{
								CS$<>8__locals1.$VB$Local_TargetColor0 = new ModBase.MyColor(215.0, 37.0, 155.0, 252.0);
								CS$<>8__locals1.$VB$Local_TargetColor1 = new ModBase.MyColor(215.0, 10.0, 142.0, 252.0);
							}
							if (!Information.IsNothing(CS$<>8__locals1.$VB$Local_DoubleStack))
							{
								if (!ModAnimation.AniIsRun(Conversions.ToString(Operators.ConcatenateObject("Hint Show ", NewLateBinding.LateIndexGet(CS$<>8__locals1.$VB$Local_DoubleStack.Tag, new object[]
								{
									1
								}, null)))))
								{
									ModAnimation.AniStop(Conversions.ToString(Operators.ConcatenateObject("Hint Hide ", NewLateBinding.LateIndexGet(CS$<>8__locals1.$VB$Local_DoubleStack.Tag, new object[]
									{
										1
									}, null))));
									double a = (800.0 + ModBase.MathClamp((double)hintMessage.regMap.Length, 5.0, 23.0) * 180.0) * ModAnimation.m_Task;
									ModAnimation.AniStart(new ModAnimation.AniData[]
									{
										ModAnimation.AaX(CS$<>8__locals1.$VB$Local_DoubleStack, -12.0 - CS$<>8__locals1.$VB$Local_DoubleStack.Margin.Left, 50, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
										ModAnimation.AaX(CS$<>8__locals1.$VB$Local_DoubleStack, -8.0, 50, 50, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false),
										ModAnimation.AaX(CS$<>8__locals1.$VB$Local_DoubleStack, 8.0, 50, 100, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
										ModAnimation.AaX(CS$<>8__locals1.$VB$Local_DoubleStack, -8.0, 50, 150, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false),
										ModAnimation.AaDouble(delegate(object i)
										{
											CS$<>8__locals1.$VB$Local_Percent = Conversions.ToDouble(Operators.AddObject(CS$<>8__locals1.$VB$Local_Percent, i));
											LinearGradientBrush linearGradientBrush = (LinearGradientBrush)CS$<>8__locals1.$VB$Local_DoubleStack.Background;
											linearGradientBrush.GradientStops[0].Color = CS$<>8__locals1.$VB$Local_TargetColor0 * CS$<>8__locals1.$VB$Local_Percent + new ModBase.MyColor(255.0, 255.0, 255.0) * (1.0 - CS$<>8__locals1.$VB$Local_Percent);
											linearGradientBrush.GradientStops[1].Color = CS$<>8__locals1.$VB$Local_TargetColor1 * CS$<>8__locals1.$VB$Local_Percent + new ModBase.MyColor(255.0, 255.0, 255.0) * (1.0 - CS$<>8__locals1.$VB$Local_Percent);
										}, 0.7, 250, 0, null, false),
										ModAnimation.AaX(CS$<>8__locals1.$VB$Local_DoubleStack, -50.0, 200, checked((int)Math.Round(a)), new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false),
										ModAnimation.AaOpacity(CS$<>8__locals1.$VB$Local_DoubleStack, -1.0, 150, checked((int)Math.Round(a)), null, false),
										ModAnimation.AaCode(delegate
										{
											NewLateBinding.LateIndexSetComplex(CS$<>8__locals1.$VB$Local_DoubleStack.Tag, new object[]
											{
												0,
												false
											}, null, false, true);
										}, checked((int)Math.Round(a)), false),
										ModAnimation.AaHeight(CS$<>8__locals1.$VB$Local_DoubleStack, -26.0, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), true),
										ModAnimation.AaCode(delegate
										{
											ModMain._ProcessIterator.PanHint.Children.Remove(CS$<>8__locals1.$VB$Local_DoubleStack);
										}, 0, true)
									}, Conversions.ToString(Operators.ConcatenateObject("Hint Hide ", NewLateBinding.LateIndexGet(CS$<>8__locals1.$VB$Local_DoubleStack.Tag, new object[]
									{
										1
									}, null))), false);
								}
							}
							else
							{
								ModMain._Closure$__5-1 CS$<>8__locals2 = new ModMain._Closure$__5-1(CS$<>8__locals2);
								CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2 = CS$<>8__locals1;
								CS$<>8__locals2.$VB$Local_NewHintControl = new Border
								{
									Tag = new object[]
									{
										true,
										ModBase.GetUuid()
									},
									Margin = new Thickness(-70.0, 0.0, 20.0, 0.0),
									Opacity = 0.0,
									Height = 0.0,
									HorizontalAlignment = HorizontalAlignment.Left,
									CornerRadius = new CornerRadius(0.0, 6.0, 6.0, 0.0)
								};
								CS$<>8__locals2.$VB$Local_NewHintControl.Background = new LinearGradientBrush(new GradientStopCollection(new List<GradientStop>
								{
									new GradientStop(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_TargetColor0 * CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Percent + new ModBase.MyColor(255.0, 255.0, 255.0) * (1.0 - CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Percent), 0.0),
									new GradientStop(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_TargetColor1 * CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Percent + new ModBase.MyColor(255.0, 255.0, 255.0) * (1.0 - CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Percent), 1.0)
								}), 90.0);
								CS$<>8__locals2.$VB$Local_NewHintControl.Child = new TextBlock
								{
									TextTrimming = TextTrimming.CharacterEllipsis,
									FontSize = 13.0,
									Text = hintMessage.regMap,
									Foreground = new ModBase.MyColor(255.0, 255.0, 255.0),
									Margin = new Thickness(33.0, 5.0, 8.0, 5.0)
								};
								ModMain._ProcessIterator.PanHint.Children.Add(CS$<>8__locals2.$VB$Local_NewHintControl);
								List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
								if (ModMain._ProcessIterator.PanHint.Children.Count > 1)
								{
									list.Add(ModAnimation.AaHeight(CS$<>8__locals2.$VB$Local_NewHintControl, 26.0, 150, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false));
								}
								else
								{
									CS$<>8__locals2.$VB$Local_NewHintControl.Height = 26.0;
								}
								list.AddRange(new ModAnimation.AniData[]
								{
									ModAnimation.AaX(CS$<>8__locals2.$VB$Local_NewHintControl, 30.0, 400, 0, new ModAnimation.AniEaseOutElastic(ModAnimation.AniEasePower.Weak), false),
									ModAnimation.AaX(CS$<>8__locals2.$VB$Local_NewHintControl, 20.0, 200, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), false),
									ModAnimation.AaOpacity(CS$<>8__locals2.$VB$Local_NewHintControl, 1.0, 100, 0, null, false),
									ModAnimation.AaDouble(delegate(object i)
									{
										CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Percent = Conversions.ToDouble(Operators.AddObject(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Percent, i));
										LinearGradientBrush linearGradientBrush = (LinearGradientBrush)CS$<>8__locals2.$VB$Local_NewHintControl.Background;
										linearGradientBrush.GradientStops[0].Color = CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_TargetColor0 * CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Percent + new ModBase.MyColor(255.0, 255.0, 255.0) * (1.0 - CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Percent);
										linearGradientBrush.GradientStops[1].Color = CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_TargetColor1 * CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Percent + new ModBase.MyColor(255.0, 255.0, 255.0) * (1.0 - CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_Percent);
									}, 0.7, 250, 100, null, false)
								});
								ModAnimation.AniStart(list, Conversions.ToString(Operators.ConcatenateObject("Hint Show ", NewLateBinding.LateIndexGet(CS$<>8__locals2.$VB$Local_NewHintControl.Tag, new object[]
								{
									1
								}, null))), false);
								double a2 = (800.0 + ModBase.MathClamp((double)hintMessage.regMap.Length, 5.0, 23.0) * 180.0) * ModAnimation.m_Task;
								ModAnimation.AniStart(checked(new ModAnimation.AniData[]
								{
									ModAnimation.AaX(CS$<>8__locals2.$VB$Local_NewHintControl, -50.0, 200, (int)Math.Round(a2), new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false),
									ModAnimation.AaOpacity(CS$<>8__locals2.$VB$Local_NewHintControl, -1.0, 150, (int)Math.Round(a2), null, false),
									ModAnimation.AaCode(delegate
									{
										NewLateBinding.LateIndexSetComplex(CS$<>8__locals2.$VB$Local_NewHintControl.Tag, new object[]
										{
											0,
											false
										}, null, false, true);
									}, (int)Math.Round(a2), false),
									ModAnimation.AaHeight(CS$<>8__locals2.$VB$Local_NewHintControl, -26.0, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), true),
									ModAnimation.AaCode(delegate
									{
										ModMain._ProcessIterator.PanHint.Children.Remove(CS$<>8__locals2.$VB$Local_NewHintControl);
									}, 0, true)
								}), Conversions.ToString(Operators.ConcatenateObject("Hint Hide ", NewLateBinding.LateIndexGet(CS$<>8__locals2.$VB$Local_NewHintControl.Tag, new object[]
								{
									1
								}, null))), false);
							}
						}
						if (hintMessage.m_ProductMap)
						{
							ModBase.Log("[UI] 弹出提示：" + hintMessage.regMap, ModBase.LogLevel.Normal, "出现错误");
						}
						ModMain.paramsIterator.RemoveAt(0);
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "显示弹出提示失败", ModBase.LogLevel.Normal, "出现错误");
			}
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x0009878C File Offset: 0x0009698C
		private static void HideAllHint()
		{
			try
			{
				IEnumerator enumerator = ModMain._ProcessIterator.PanHint.Children.GetEnumerator();
				while (enumerator.MoveNext())
				{
					ModMain._Closure$__6-0 CS$<>8__locals1 = new ModMain._Closure$__6-0(CS$<>8__locals1);
					CS$<>8__locals1.$VB$Local_Control = (Border)enumerator.Current;
					CS$<>8__locals1.$VB$Local_Control.IsHitTestVisible = false;
					ModAnimation.AniStart(new ModAnimation.AniData[]
					{
						ModAnimation.AaX(CS$<>8__locals1.$VB$Local_Control, -50.0, 200, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false),
						ModAnimation.AaOpacity(CS$<>8__locals1.$VB$Local_Control, -1.0, 150, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Middle), false),
						ModAnimation.AaCode(delegate
						{
							NewLateBinding.LateIndexSetComplex(CS$<>8__locals1.$VB$Local_Control.Tag, new object[]
							{
								0,
								false
							}, null, false, true);
						}, 0, false),
						ModAnimation.AaHeight(CS$<>8__locals1.$VB$Local_Control, -26.0, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Middle), true),
						ModAnimation.AaCode(delegate
						{
							ModMain._ProcessIterator.PanHint.Children.Remove(CS$<>8__locals1.$VB$Local_Control);
						}, 0, true)
					}, Conversions.ToString(Operators.ConcatenateObject("Hint Hide ", NewLateBinding.LateIndexGet(CS$<>8__locals1.$VB$Local_Control.Tag, new object[]
					{
						1
					}, null))), false);
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

		// Token: 0x06001778 RID: 6008 RVA: 0x000988F8 File Offset: 0x00096AF8
		public static int MyMsgBox(string Caption, string Title = "提示", string Button1 = "确定", string Button2 = "", string Button3 = "", bool IsWarn = false, bool HighLight = true, bool ForceWait = false, Action Button1Action = null, Action Button2Action = null, Action Button3Action = null)
		{
			ModMain.MyMsgBoxConverter myMsgBoxConverter = new ModMain.MyMsgBoxConverter
			{
				Type = ModMain.MyMsgBoxType.Text,
				m_BridgeMap = Button1,
				itemMap = Button2,
				reponseMap = Button3,
				listenerMap = Caption,
				_ClassMap = IsWarn,
				Title = Title,
				m_ObjectMap = HighLight,
				m_PolicyMap = true,
				m_GlobalMap = Button1Action,
				m_ExceptionMap = Button2Action,
				utilsMap = Button3Action
			};
			ModMain.m_DispatcherIterator.Add(myMsgBoxConverter);
			if (ModBase.RunInUi())
			{
				ModMain.MyMsgBoxTick();
			}
			checked
			{
				int result;
				if (Button2.Length <= 0 && !ForceWait)
				{
					result = 1;
				}
				else
				{
					if (ModMain._ProcessIterator != null && (ModMain._ProcessIterator.PanMsg != null || !ModBase.RunInUi()))
					{
						try
						{
							ModMain._ProcessIterator.DragStop();
							ComponentDispatcher.PushModal();
							Dispatcher.PushFrame(myMsgBoxConverter._OrderMap);
							goto IL_195;
						}
						finally
						{
							ComponentDispatcher.PopModal();
						}
					}
					ModMain.m_DispatcherIterator.Remove(myMsgBoxConverter);
					if (Button2.Length > 0)
					{
						MsgBoxResult msgBoxResult = Interaction.MsgBox(Caption, ((Button3.Length > 0) ? MsgBoxStyle.YesNoCancel : MsgBoxStyle.YesNo) + (IsWarn ? 16 : 32), Title);
						if (msgBoxResult != MsgBoxResult.Cancel)
						{
							if (msgBoxResult != MsgBoxResult.Yes)
							{
								if (msgBoxResult == MsgBoxResult.No)
								{
									myMsgBoxConverter.schemaMap = 2;
								}
							}
							else
							{
								myMsgBoxConverter.schemaMap = 1;
							}
						}
						else
						{
							myMsgBoxConverter.schemaMap = 3;
						}
					}
					else
					{
						Interaction.MsgBox(Caption, MsgBoxStyle.OkOnly + (IsWarn ? 16 : 32), Title);
						myMsgBoxConverter.schemaMap = 1;
					}
					ModBase.Log(string.Concat(new string[]
					{
						"[Control] 主窗体加载完成前出现意料外的等待弹窗：",
						Button1,
						",",
						Button2,
						",",
						Button3
					}), ModBase.LogLevel.Debug, "出现错误");
					IL_195:
					ModBase.Log(Conversions.ToString(Operators.ConcatenateObject("[Control] 普通弹框返回：", myMsgBoxConverter.schemaMap ?? "null")), ModBase.LogLevel.Normal, "出现错误");
					result = Conversions.ToInteger(myMsgBoxConverter.schemaMap);
				}
				return result;
			}
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00098AE0 File Offset: 0x00096CE0
		public static string MyMsgBoxInput(string Title, string Text = "", string DefaultInput = "", Collection<Validate> ValidateRules = null, string HintText = "", string Button1 = "确定", string Button2 = "取消", bool IsWarn = false)
		{
			ModMain.MyMsgBoxConverter myMsgBoxConverter = new ModMain.MyMsgBoxConverter
			{
				listenerMap = Text,
				m_ValueMap = HintText,
				Type = ModMain.MyMsgBoxType.Input,
				m_VisitorMap = (ValidateRules ?? new Collection<Validate>()),
				m_BridgeMap = Button1,
				itemMap = Button2,
				m_CollectionMap = DefaultInput,
				_ClassMap = IsWarn,
				Title = Title
			};
			ModMain.m_DispatcherIterator.Add(myMsgBoxConverter);
			try
			{
				if (ModMain._ProcessIterator != null)
				{
					ModMain._ProcessIterator.DragStop();
				}
				ComponentDispatcher.PushModal();
				Dispatcher.PushFrame(myMsgBoxConverter._OrderMap);
			}
			finally
			{
				ComponentDispatcher.PopModal();
			}
			ModBase.Log(Conversions.ToString(Operators.ConcatenateObject("[Control] 输入弹框返回：", myMsgBoxConverter.schemaMap ?? "null")), ModBase.LogLevel.Normal, "出现错误");
			return Conversions.ToString(myMsgBoxConverter.schemaMap);
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00098BB8 File Offset: 0x00096DB8
		public static int? MyMsgBoxSelect(List<IMyRadio> Selections, string Title = "提示", string Button1 = "确定", string Button2 = "", bool IsWarn = false)
		{
			ModMain.MyMsgBoxConverter myMsgBoxConverter = new ModMain.MyMsgBoxConverter
			{
				Type = ModMain.MyMsgBoxType.Select,
				m_BridgeMap = Button1,
				itemMap = Button2,
				m_CollectionMap = Selections,
				_ClassMap = IsWarn,
				Title = Title
			};
			ModMain.m_DispatcherIterator.Add(myMsgBoxConverter);
			try
			{
				if (ModMain._ProcessIterator != null)
				{
					ModMain._ProcessIterator.DragStop();
				}
				ComponentDispatcher.PushModal();
				Dispatcher.PushFrame(myMsgBoxConverter._OrderMap);
			}
			finally
			{
				ComponentDispatcher.PopModal();
			}
			ModBase.Log(Conversions.ToString(Operators.ConcatenateObject("[Control] 选择弹框返回：", myMsgBoxConverter.schemaMap ?? "null")), ModBase.LogLevel.Normal, "出现错误");
			return (int?)myMsgBoxConverter.schemaMap;
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00098C70 File Offset: 0x00096E70
		public static void MyMsgBoxTick()
		{
			try
			{
				if (ModMain._ProcessIterator != null && ModMain._ProcessIterator.PanMsg != null)
				{
					if (ModMain._ProcessIterator.WindowState != WindowState.Minimized)
					{
						if (ModMain._ProcessIterator.PanMsg.Children.Count > 0)
						{
							ModMain._ProcessIterator.PanMsg.Visibility = Visibility.Visible;
						}
						else if (Enumerable.Any<ModMain.MyMsgBoxConverter>(ModMain.m_DispatcherIterator))
						{
							ModMain._ProcessIterator.PanMsg.Visibility = Visibility.Visible;
							switch (ModMain.m_DispatcherIterator[0].Type)
							{
							case ModMain.MyMsgBoxType.Text:
								ModMain._ProcessIterator.PanMsg.Children.Add(new MyMsgText(ModMain.m_DispatcherIterator[0]));
								break;
							case ModMain.MyMsgBoxType.Select:
								ModMain._ProcessIterator.PanMsg.Children.Add(new MyMsgSelect(ModMain.m_DispatcherIterator[0]));
								break;
							case ModMain.MyMsgBoxType.Input:
								ModMain._ProcessIterator.PanMsg.Children.Add(new MyMsgInput(ModMain.m_DispatcherIterator[0]));
								break;
							case ModMain.MyMsgBoxType.Login:
								ModMain._ProcessIterator.PanMsg.Children.Add(new MyMsgLogin(ModMain.m_DispatcherIterator[0]));
								break;
							}
							ModMain.m_DispatcherIterator.RemoveAt(0);
						}
						else if (ModMain._ProcessIterator.PanMsg.Visibility != Visibility.Collapsed)
						{
							ModMain._ProcessIterator.PanMsg.Visibility = Visibility.Collapsed;
						}
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "处理等待中的弹窗失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00098E2C File Offset: 0x0009702C
		private static void HelpLoad(ModLoader.LoaderTask<int, List<ModMain.HelpEntry>> Loader)
		{
			object obj = ModMain.testRepository;
			ObjectFlowControl.CheckForSyncLockOnValueType(obj);
			checked
			{
				lock (obj)
				{
					try
					{
						ModMain.HelpTryExtract();
						List<string> list = new List<string>();
						try
						{
							List<string> list2 = new List<string>();
							if (Directory.Exists(ModBase.Path + "PCL\\Help\\"))
							{
								try
								{
									foreach (FileInfo fileInfo in ModBase.EnumerateFiles(ModBase.Path + "PCL\\Help\\"))
									{
										string left = fileInfo.Extension.ToLower();
										if (Operators.CompareString(left, ".helpignore", false) != 0)
										{
											if (Operators.CompareString(left, ".json", false) == 0)
											{
												list.Add(fileInfo.FullName);
											}
										}
										else
										{
											ModBase.Log("[Help] 发现 .helpignore 文件：" + fileInfo.FullName, ModBase.LogLevel.Normal, "出现错误");
											string[] array = ModBase.ReadFile(fileInfo.FullName, null).Split("\r\n".ToCharArray());
											for (int i = 0; i < array.Length; i++)
											{
												string text = array[i].BeforeFirst("#", false).Trim();
												if (!string.IsNullOrWhiteSpace(text))
												{
													list2.Add(text);
													if (ModBase._TokenRepository)
													{
														ModBase.Log("[Help]  > " + text, ModBase.LogLevel.Normal, "出现错误");
													}
												}
											}
										}
									}
								}
								finally
								{
									IEnumerator<FileInfo> enumerator;
									if (enumerator != null)
									{
										enumerator.Dispose();
									}
								}
							}
							ModBase.Log("[Help] 已扫描 PCL 文件夹下的帮助文件，目前总计 " + Conversions.ToString(list.Count) + " 条", ModBase.LogLevel.Normal, "出现错误");
							try
							{
								IEnumerator<FileInfo> enumerator2 = ModBase.EnumerateFiles(ModBase.m_DecoratorRepository + "Help").GetEnumerator();
								IL_286:
								while (enumerator2.MoveNext())
								{
									FileInfo fileInfo2 = enumerator2.Current;
									if (Operators.CompareString(fileInfo2.Extension.ToLower(), ".json", false) == 0 && !fileInfo2.Directory.FullName.Replace(ModBase.m_DecoratorRepository + "Help", "").Contains("\\."))
									{
										string text2 = fileInfo2.FullName.Replace(ModBase.m_DecoratorRepository + "Help\\", "");
										try
										{
											foreach (string text3 in list2)
											{
												if (ModBase.RegexCheck(text2, text3, 0))
												{
													if (ModBase._TokenRepository)
													{
														ModBase.Log("[Help] 已忽略 " + text2 + "：" + text3, ModBase.LogLevel.Normal, "出现错误");
													}
													goto IL_286;
												}
											}
										}
										finally
										{
											List<string>.Enumerator enumerator3;
											((IDisposable)enumerator3).Dispose();
										}
										list.Add(fileInfo2.FullName);
									}
								}
							}
							finally
							{
								IEnumerator<FileInfo> enumerator2;
								if (enumerator2 != null)
								{
									enumerator2.Dispose();
								}
							}
							ModBase.Log("[Help] 已扫描缓存文件夹下的帮助文件，目前总计 " + Conversions.ToString(list.Count) + " 条", ModBase.LogLevel.Normal, "出现错误");
						}
						catch (Exception ex)
						{
							ModBase.Log(ex, "检查帮助文件夹失败", ModBase.LogLevel.Msgbox, "出现错误");
						}
						if (!Loader.IsAborted)
						{
							List<ModMain.HelpEntry> list3 = new List<ModMain.HelpEntry>();
							try
							{
								foreach (string text4 in list)
								{
									try
									{
										ModMain.HelpEntry helpEntry = new ModMain.HelpEntry(text4);
										list3.Add(helpEntry);
										if (ModBase._TokenRepository)
										{
											ModBase.Log("[Help] 已加载的帮助条目：" + helpEntry.Title + " ← " + text4, ModBase.LogLevel.Normal, "出现错误");
										}
									}
									catch (Exception ex2)
									{
										ModBase.Log(ex2, "初始化帮助条目失败（" + text4 + "）", ModBase.LogLevel.Msgbox, "出现错误");
									}
								}
							}
							finally
							{
								List<string>.Enumerator enumerator4;
								((IDisposable)enumerator4).Dispose();
							}
							if (!Enumerable.Any<ModMain.HelpEntry>(list3))
							{
								throw new Exception("未找到可用的帮助；若不需要帮助页面，可以在 设置 → 个性化 → 功能隐藏 中将其隐藏");
							}
							if (!Loader.IsAborted)
							{
								Loader.Output = list3;
							}
						}
					}
					catch (Exception ex3)
					{
						ModBase.Log(ex3, "帮助列表初始化失败", ModBase.LogLevel.Debug, "出现错误");
						throw;
					}
				}
			}
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x000992DC File Offset: 0x000974DC
		public static void HelpTryExtract()
		{
			if (Operators.ConditionalCompareObjectNotEqual(ModBase.m_IdentifierRepository.Get("SystemHelpVersion", null), 347, false) || !File.Exists(ModBase.m_DecoratorRepository + "Help\\启动器\\备份设置.xaml"))
			{
				ModBase.DeleteDirectory(ModBase.m_DecoratorRepository + "Help", false);
				Directory.CreateDirectory(ModBase.m_DecoratorRepository + "Help");
				ModBase.WriteFile(ModBase.m_DecoratorRepository + "Cache\\Help.zip", ModBase.GetResources("Help"), false);
				ModBase.ExtractFile(ModBase.m_DecoratorRepository + "Cache\\Help.zip", ModBase.m_DecoratorRepository + "Help", Encoding.UTF8, null);
				ModBase.m_IdentifierRepository.Set("SystemHelpVersion", 347, false, null);
				ModBase.Log("[Help] 已解压内置帮助文件，目前状态：" + Conversions.ToString(File.Exists(ModBase.m_DecoratorRepository + "Help\\启动器\\备份设置.xaml")), ModBase.LogLevel.Debug, "出现错误");
			}
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x000993E0 File Offset: 0x000975E0
		public static string HelpArgumentReplace(string Xaml)
		{
			return ModBase.RegexReplaceEach(ModBase.RegexReplaceEach(Xaml.Replace("{path}", ModBase.smethod_3(ModBase.Path)), (ModMain._Closure$__.$IR64-1 == null) ? (ModMain._Closure$__.$IR64-1 = ((Match a0) => ((ModMain._Closure$__.$I64-0 == null) ? (ModMain._Closure$__.$I64-0 = (() => ModBase.smethod_3(PageOtherTest.GetRandomHint()))) : ModMain._Closure$__.$I64-0)())) : ModMain._Closure$__.$IR64-1, "\\{hint\\}", 0), (ModMain._Closure$__.$IR64-2 == null) ? (ModMain._Closure$__.$IR64-2 = ((Match a0) => ((ModMain._Closure$__.$I64-1 == null) ? (ModMain._Closure$__.$I64-1 = (() => ModBase.smethod_3(PageOtherTest.GetRandomCave()))) : ModMain._Closure$__.$I64-1)())) : ModMain._Closure$__.$IR64-2, "\\{cave\\}", 0);
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00099460 File Offset: 0x00097660
		private static void TimerFool()
		{
			try
			{
				if (ModMain.recordIterator != null && ModMain.recordIterator.AprilPosTrans != null && ModMain._ProcessIterator.expressionIterator != null)
				{
					if (!ModMain.m_ErrorRepository && !(ModMain._ProcessIterator._MethodIterator != FormMain.PageType.Launch) && ModAnimation.CalcParser() == 0 && ModMain.recordIterator.BtnLaunch.IsLoaded)
					{
						Point position = ModMain._ProcessIterator.expressionIterator.GetPosition(ModMain._ProcessIterator);
						double num;
						double num2;
						Vector vector;
						Vector vector3;
						checked
						{
							if (position == ModMain.m_MockRepository)
							{
								ModMain._SpecificationRepository++;
							}
							else
							{
								ModMain.m_MockRepository = position;
								ModMain._SpecificationRepository = 0;
							}
							num = ModMain.recordIterator.BtnLaunch.ActualWidth / 2.0;
							num2 = ModMain.recordIterator.BtnLaunch.ActualHeight / 2.0;
							vector = (Vector)(ModMain._ProcessIterator.expressionIterator.GetPosition(ModMain.recordIterator.BtnLaunch) - new Vector(num, num2));
							Vector vector2 = new Vector(vector.X, vector.Y);
							vector2.Normalize();
							vector3 = -vector2;
						}
						double length = new Vector(Math.Max(0.0, Math.Abs(vector.X) - num), Math.Max(0.0, Math.Abs(vector.Y) - num2)).Length;
						double num3 = Math.Sin((double)ModMain.m_IssuerRepository / 37.5 * 3.141592653589793);
						Vector vector4 = Math.Max(0.0, num3 * 0.25 - 0.65 - Math.Log((length + 0.4) / 200.0)) * vector3;
						if (ModMain._SpecificationRepository >= 320)
						{
							Vector vector5 = (Vector)(ModMain._ProcessIterator.expressionIterator.GetPosition(ModMain._ProcessIterator.PanMain) - new Vector(num, ModMain._ProcessIterator.PanMain.ActualHeight - num2 * 3.0));
							Vector vector6 = new Vector(ModMain.recordIterator.AprilPosTrans.X, ModMain.recordIterator.AprilPosTrans.Y);
							if (vector5.Length > 250.0 && vector6.Length > 0.4)
							{
								vector4 -= vector6 * 0.0005;
								vector6.Normalize();
								vector4 -= vector6 * 0.15;
							}
						}
						Point point = ModMain.recordIterator.BtnLaunch.TranslatePoint(new Point(0.0, 0.0), ModMain._ProcessIterator.PanForm);
						if (point.X < -num * 2.0)
						{
							TranslateTransform aprilPosTrans;
							(aprilPosTrans = ModMain.recordIterator.AprilPosTrans).X = aprilPosTrans.X + (ModMain._ProcessIterator.PanForm.ActualWidth + num * 2.0);
							ModMain.m_ContextRepository.X = ModMain.m_ContextRepository.X - 80.0;
							if (point.Y < 0.0)
							{
								(aprilPosTrans = ModMain.recordIterator.AprilPosTrans).Y = aprilPosTrans.Y + num2 * 2.5;
							}
							else if (point.Y > ModMain._ProcessIterator.PanForm.ActualHeight - num2 * 2.0)
							{
								(aprilPosTrans = ModMain.recordIterator.AprilPosTrans).Y = aprilPosTrans.Y - num2 * 2.5;
							}
						}
						else if (point.X > ModMain._ProcessIterator.PanForm.ActualWidth)
						{
							TranslateTransform aprilPosTrans;
							(aprilPosTrans = ModMain.recordIterator.AprilPosTrans).X = aprilPosTrans.X - (ModMain._ProcessIterator.PanForm.ActualWidth + num * 2.0);
							ModMain.m_ContextRepository.X = ModMain.m_ContextRepository.X + 80.0;
							if (point.Y < 0.0)
							{
								(aprilPosTrans = ModMain.recordIterator.AprilPosTrans).Y = aprilPosTrans.Y + num2 * 2.5;
							}
							else if (point.Y > ModMain._ProcessIterator.PanForm.ActualHeight - num2 * 2.0)
							{
								(aprilPosTrans = ModMain.recordIterator.AprilPosTrans).Y = aprilPosTrans.Y - num2 * 2.5;
							}
						}
						else if (point.Y < -num2 * 2.0)
						{
							TranslateTransform aprilPosTrans;
							(aprilPosTrans = ModMain.recordIterator.AprilPosTrans).Y = aprilPosTrans.Y + (ModMain._ProcessIterator.PanForm.ActualHeight + num2 * 2.0);
							ModMain.m_ContextRepository.Y = ModMain.m_ContextRepository.Y - 25.0;
							if (point.X < 0.0)
							{
								(aprilPosTrans = ModMain.recordIterator.AprilPosTrans).X = aprilPosTrans.X + num * 2.0;
							}
							else if (point.X > ModMain._ProcessIterator.PanForm.ActualWidth - num * 2.0)
							{
								(aprilPosTrans = ModMain.recordIterator.AprilPosTrans).X = aprilPosTrans.X - num * 2.0;
							}
						}
						else if (point.Y > ModMain._ProcessIterator.PanForm.ActualHeight)
						{
							TranslateTransform aprilPosTrans;
							(aprilPosTrans = ModMain.recordIterator.AprilPosTrans).Y = aprilPosTrans.Y - (ModMain._ProcessIterator.PanForm.ActualHeight + num2 * 2.0);
							ModMain.m_ContextRepository.Y = ModMain.m_ContextRepository.Y + 25.0;
							if (point.X < 0.0)
							{
								(aprilPosTrans = ModMain.recordIterator.AprilPosTrans).X = aprilPosTrans.X + num * 2.0;
							}
							else if (point.X > ModMain._ProcessIterator.PanForm.ActualWidth - num * 2.0)
							{
								(aprilPosTrans = ModMain.recordIterator.AprilPosTrans).X = aprilPosTrans.X - num * 2.0;
							}
						}
						ModMain.m_ContextRepository = ModMain.m_ContextRepository * 0.8 + vector4;
						double num4 = Math.Min(60.0, ModMain.m_ContextRepository.Length);
						if (num4 >= 0.01)
						{
							ModMain.m_ContextRepository.Normalize();
							ModMain.m_ContextRepository *= num4;
							ModMain.requestRepository = checked((int)Math.Round(unchecked((double)ModMain.requestRepository + num4)));
							TranslateTransform aprilPosTrans;
							(aprilPosTrans = ModMain.recordIterator.AprilPosTrans).X = aprilPosTrans.X + ModMain.m_ContextRepository.X;
							(aprilPosTrans = ModMain.recordIterator.AprilPosTrans).Y = aprilPosTrans.Y + ModMain.m_ContextRepository.Y;
							ModMain.recordIterator.AprilScaleTrans.ScaleX = ModBase.MathClamp(1.0 - (Math.Abs(vector3.X) - Math.Abs(vector3.Y)) * (num4 / 160.0), 0.2, 1.8);
							ModMain.recordIterator.AprilScaleTrans.ScaleY = ModBase.MathClamp(1.0 - (Math.Abs(vector3.Y) - Math.Abs(vector3.X)) * (num4 / 100.0), 0.2, 1.8);
							if (ModMain.requestRepository > 4000)
							{
								ModMain.requestRepository = -4000;
								switch (ModBase.RandomInteger(0, 3))
								{
								case 0:
									ModMain.Hint("放弃吧！只需要点一下右下角的小白旗……", ModMain.HintType.Info, true);
									break;
								case 1:
									ModMain.Hint("看到右下角的那面小白旗了吗？", ModMain.HintType.Info, true);
									break;
								case 2:
									ModMain.Hint("这里建议点一下右下角的小白旗投降呢.jpg", ModMain.HintType.Info, true);
									break;
								case 3:
									ModMain.Hint("右下角的小白旗永远等着你……", ModMain.HintType.Info, true);
									break;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "愚人节移动出错", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00099D58 File Offset: 0x00097F58
		public static void ShowWindowToTop(IntPtr Handle)
		{
			try
			{
				ModMain.PostMessageA(Handle, 6402U, 0L, 0L);
				ModMain.SetForegroundWindow(Handle);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "设置窗口置顶失败", ModBase.LogLevel.Hint, "出现错误");
			}
		}

		// Token: 0x06001781 RID: 6017
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr FindWindowA([MarshalAs(UnmanagedType.VBByRefStr)] ref string ClassName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string WindowName);

		// Token: 0x06001782 RID: 6018
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int SetForegroundWindow(IntPtr hWnd);

		// Token: 0x06001783 RID: 6019
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern bool PostMessageA(IntPtr hWnd, uint msg, long wParam, long lParam);

		// Token: 0x06001784 RID: 6020 RVA: 0x00099DBC File Offset: 0x00097FBC
		private static void TimerMain()
		{
			try
			{
				ModMain.HintTick();
				ModMain.MyMsgBoxTick();
				ModMain._ProcessIterator.DragTick();
				ModLoader.LoaderTaskbarProgressRefresh();
				if (ModSecret.registryField == 2)
				{
					ModSecret.ViewReader(-1);
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "短程主时钟执行异常", ModBase.LogLevel.Assert, "出现错误");
			}
			checked
			{
				ModMain.m_HelperRepository++;
				if (ModMain.m_HelperRepository == 4)
				{
					ModMain.m_HelperRepository = 0;
					try
					{
						if (ModSecret.configurationField == 12)
						{
							ModSecret.ViewReader(-1);
						}
					}
					catch (Exception ex2)
					{
						ModBase.Log(ex2, "中程主时钟执行异常", ModBase.LogLevel.Debug, "出现错误");
					}
				}
				ModMain.m_IssuerRepository++;
				if (ModMain.m_IssuerRepository == 150)
				{
					ModMain.m_IssuerRepository = 0;
					try
					{
						if (ModMain._ProcessIterator.BtnExtraApril_ShowCheck() && ModMain.requestRepository != 0)
						{
							ModMain._ProcessIterator.BtnExtraApril.Ribble();
						}
						ModBase.RunInUi((ModMain._Closure$__.$I79-0 == null) ? (ModMain._Closure$__.$I79-0 = delegate()
						{
							if (!ModMain._ProcessIterator.Hidden)
							{
								if (ModMain._ProcessIterator.Top < -9000.0)
								{
									ModMain._ProcessIterator.Top = 100.0;
								}
								if (ModMain._ProcessIterator.Left < -9000.0)
								{
									ModMain._ProcessIterator.Left = 100.0;
								}
							}
						}) : ModMain._Closure$__.$I79-0, false);
					}
					catch (Exception ex3)
					{
						ModBase.Log(ex3, "长程主时钟执行异常", ModBase.LogLevel.Assert, "出现错误");
					}
				}
			}
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00099F10 File Offset: 0x00098110
		public static void TimerMainStart()
		{
			ModBase.RunInNewThread((ModMain._Closure$__.$I80-0 == null) ? (ModMain._Closure$__.$I80-0 = delegate()
			{
				try
				{
					for (;;)
					{
						ModBase.RunInUiWait(new Action(ModMain.TimerMain));
						Thread.Sleep(49);
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "程序主时钟出错", ModBase.LogLevel.Feedback, "出现错误");
				}
			}) : ModMain._Closure$__.$I80-0, "Timer Main", ThreadPriority.Normal);
			if (ModMain.mapRepository)
			{
				ModBase.RunInNewThread((ModMain._Closure$__.$I80-1 == null) ? (ModMain._Closure$__.$I80-1 = delegate()
				{
					try
					{
						int tickCount = MyWpfExtension.ManageParser().Clock.TickCount;
						for (;;)
						{
							if (tickCount != MyWpfExtension.ManageParser().Clock.TickCount)
							{
								tickCount = MyWpfExtension.ManageParser().Clock.TickCount;
								ModBase.RunInUiWait(new Action(ModMain.TimerFool));
							}
							Thread.Sleep(1);
						}
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "愚人节主时钟出错", ModBase.LogLevel.Feedback, "出现错误");
					}
				}) : ModMain._Closure$__.$I80-1, "Timer Main Fool", ThreadPriority.Normal);
			}
		}

		// Token: 0x04000BDB RID: 3035
		private static List<ModMain.HintMessage> paramsIterator = ModMain.paramsIterator ?? new List<ModMain.HintMessage>();

		// Token: 0x04000BDC RID: 3036
		public static List<ModMain.MyMsgBoxConverter> m_DispatcherIterator = ModMain.m_DispatcherIterator ?? new List<ModMain.MyMsgBoxConverter>();

		// Token: 0x04000BDD RID: 3037
		public static FormMain _ProcessIterator;

		// Token: 0x04000BDE RID: 3038
		public static SplashScreen m_ParameterIterator;

		// Token: 0x04000BDF RID: 3039
		public static PageLaunchLeft recordIterator;

		// Token: 0x04000BE0 RID: 3040
		public static PageLaunchRight m_ServiceIterator;

		// Token: 0x04000BE1 RID: 3041
		public static PageSelectLeft _InvocationIterator;

		// Token: 0x04000BE2 RID: 3042
		public static PageSelectRight proxyIterator;

		// Token: 0x04000BE3 RID: 3043
		public static PageSpeedLeft _MessageIterator;

		// Token: 0x04000BE4 RID: 3044
		public static PageSpeedRight m_CreatorIterator;

		// Token: 0x04000BE5 RID: 3045
		public static PageLinkLeft m_InitializerIterator;

		// Token: 0x04000BE6 RID: 3046
		public static PageLinkIoi m_SingletonIterator;

		// Token: 0x04000BE7 RID: 3047
		public static PageLinkHiper m_RegIterator;

		// Token: 0x04000BE8 RID: 3048
		public static PageOtherHelpDetail _ProductIterator;

		// Token: 0x04000BE9 RID: 3049
		public static PageLinkFeedback m_ListenerIterator;

		// Token: 0x04000BEA RID: 3050
		public static PageDownloadLeft m_CollectionIterator;

		// Token: 0x04000BEB RID: 3051
		public static PageDownloadInstall m_VisitorIterator;

		// Token: 0x04000BEC RID: 3052
		public static PageDownloadClient _ValueIterator;

		// Token: 0x04000BED RID: 3053
		public static PageDownloadOptiFine objectIterator;

		// Token: 0x04000BEE RID: 3054
		public static PageDownloadLiteLoader bridgeIterator;

		// Token: 0x04000BEF RID: 3055
		public static PageDownloadForge m_ItemIterator;

		// Token: 0x04000BF0 RID: 3056
		public static PageDownloadNeoForge _ReponseIterator;

		// Token: 0x04000BF1 RID: 3057
		public static PageDownloadFabric _GlobalIterator;

		// Token: 0x04000BF2 RID: 3058
		public static PageDownloadMod m_ExceptionIterator;

		// Token: 0x04000BF3 RID: 3059
		public static PageDownloadPack m_UtilsIterator;

		// Token: 0x04000BF4 RID: 3060
		public static PageSetupLeft _ClassIterator;

		// Token: 0x04000BF5 RID: 3061
		public static PageSetupLaunch _PolicyIterator;

		// Token: 0x04000BF6 RID: 3062
		public static PageSetupUI m_OrderIterator;

		// Token: 0x04000BF7 RID: 3063
		public static PageSetupSystem producerIterator;

		// Token: 0x04000BF8 RID: 3064
		public static PageSetupLink m_SchemaIterator;

		// Token: 0x04000BF9 RID: 3065
		public static PageOtherLeft descriptorIterator;

		// Token: 0x04000BFA RID: 3066
		public static PageOtherHelp m_PublisherIterator;

		// Token: 0x04000BFB RID: 3067
		public static PageOtherAbout _DefinitionIterator;

		// Token: 0x04000BFC RID: 3068
		public static PageOtherTest _StrategyIterator;

		// Token: 0x04000BFD RID: 3069
		public static PageLoginLegacy m_ProcIterator;

		// Token: 0x04000BFE RID: 3070
		public static PageLoginNide parserRepository;

		// Token: 0x04000BFF RID: 3071
		public static PageLoginNideSkin broadcasterRepository;

		// Token: 0x04000C00 RID: 3072
		public static PageLoginAuth fieldRepository;

		// Token: 0x04000C01 RID: 3073
		public static PageLoginAuthSkin readerRepository;

		// Token: 0x04000C02 RID: 3074
		public static PageLoginMs m_ClientRepository;

		// Token: 0x04000C03 RID: 3075
		public static PageLoginMsSkin m_ConfigRepository;

		// Token: 0x04000C04 RID: 3076
		public static PageVersionLeft m_TestsRepository;

		// Token: 0x04000C05 RID: 3077
		public static PageVersionOverall m_MapperRepository;

		// Token: 0x04000C06 RID: 3078
		public static PageVersionMod _ThreadRepository;

		// Token: 0x04000C07 RID: 3079
		public static PageVersionModDisabled _PropertyRepository;

		// Token: 0x04000C08 RID: 3080
		public static PageVersionSetup composerRepository;

		// Token: 0x04000C09 RID: 3081
		public static PageDownloadCompDetail iteratorRepository;

		// Token: 0x04000C0A RID: 3082
		public static ModLoader.LoaderTask<int, List<ModMain.HelpEntry>> _RepositoryRepository = new ModLoader.LoaderTask<int, List<ModMain.HelpEntry>>("Help Page", new Action<ModLoader.LoaderTask<int, List<ModMain.HelpEntry>>>(ModMain.HelpLoad), null, ThreadPriority.BelowNormal);

		// Token: 0x04000C0B RID: 3083
		private static readonly object testRepository = RuntimeHelpers.GetObjectValue(new object());

		// Token: 0x04000C0C RID: 3084
		public static bool mapRepository = DateTime.Now.Month == 4 && DateTime.Now.Day == 1;

		// Token: 0x04000C0D RID: 3085
		public static bool m_ErrorRepository = false;

		// Token: 0x04000C0E RID: 3086
		private static Vector m_ContextRepository = new Vector(0.0, 0.0);

		// Token: 0x04000C0F RID: 3087
		private static int _SpecificationRepository = 0;

		// Token: 0x04000C10 RID: 3088
		private static Point m_MockRepository = new Point(0.0, 0.0);

		// Token: 0x04000C11 RID: 3089
		private static int requestRepository = 0;

		// Token: 0x04000C12 RID: 3090
		public static MySlider _DicRepository = null;

		// Token: 0x04000C13 RID: 3091
		private static int m_HelperRepository = 0;

		// Token: 0x04000C14 RID: 3092
		private static int m_IssuerRepository = 0;

		// Token: 0x020001F3 RID: 499
		public enum HintType
		{
			// Token: 0x04000C16 RID: 3094
			Info,
			// Token: 0x04000C17 RID: 3095
			Finish,
			// Token: 0x04000C18 RID: 3096
			Critical
		}

		// Token: 0x020001F4 RID: 500
		private struct HintMessage
		{
			// Token: 0x04000C19 RID: 3097
			public string regMap;

			// Token: 0x04000C1A RID: 3098
			public ModMain.HintType Type;

			// Token: 0x04000C1B RID: 3099
			public bool m_ProductMap;
		}

		// Token: 0x020001F5 RID: 501
		public class MyMsgBoxConverter
		{
			// Token: 0x06001786 RID: 6022 RVA: 0x00099F84 File Offset: 0x00098184
			public MyMsgBoxConverter()
			{
				this.m_ValueMap = "";
				this.m_BridgeMap = "确定";
				this.itemMap = "";
				this.reponseMap = "";
				this.m_GlobalMap = null;
				this.m_ExceptionMap = null;
				this.utilsMap = null;
				this._ClassMap = false;
				this.m_PolicyMap = false;
				this._OrderMap = new DispatcherFrame(true);
				this.m_ProducerMap = false;
			}

			// Token: 0x04000C1C RID: 3100
			public ModMain.MyMsgBoxType Type;

			// Token: 0x04000C1D RID: 3101
			public string Title;

			// Token: 0x04000C1E RID: 3102
			public string listenerMap;

			// Token: 0x04000C1F RID: 3103
			public object m_CollectionMap;

			// Token: 0x04000C20 RID: 3104
			public Collection<Validate> m_VisitorMap;

			// Token: 0x04000C21 RID: 3105
			public string m_ValueMap;

			// Token: 0x04000C22 RID: 3106
			public bool m_ObjectMap;

			// Token: 0x04000C23 RID: 3107
			public string m_BridgeMap;

			// Token: 0x04000C24 RID: 3108
			public string itemMap;

			// Token: 0x04000C25 RID: 3109
			public string reponseMap;

			// Token: 0x04000C26 RID: 3110
			public Action m_GlobalMap;

			// Token: 0x04000C27 RID: 3111
			public Action m_ExceptionMap;

			// Token: 0x04000C28 RID: 3112
			public Action utilsMap;

			// Token: 0x04000C29 RID: 3113
			public bool _ClassMap;

			// Token: 0x04000C2A RID: 3114
			public bool m_PolicyMap;

			// Token: 0x04000C2B RID: 3115
			public DispatcherFrame _OrderMap;

			// Token: 0x04000C2C RID: 3116
			public bool m_ProducerMap;

			// Token: 0x04000C2D RID: 3117
			public string schemaMap;
		}

		// Token: 0x020001F6 RID: 502
		public enum MyMsgBoxType
		{
			// Token: 0x04000C2F RID: 3119
			Text,
			// Token: 0x04000C30 RID: 3120
			Select,
			// Token: 0x04000C31 RID: 3121
			Input,
			// Token: 0x04000C32 RID: 3122
			Login
		}

		// Token: 0x020001F7 RID: 503
		public class HelpEntry
		{
			// Token: 0x06001788 RID: 6024 RVA: 0x00099FFC File Offset: 0x000981FC
			public HelpEntry(string FilePath)
			{
				this._ProcMap = null;
				this._ParserError = true;
				this.m_BroadcasterError = true;
				this.fieldError = true;
				this._DescriptorMap = FilePath;
				JObject jobject = (JObject)ModBase.GetJson(ModMain.HelpArgumentReplace(ModBase.ReadFile(FilePath, null)));
				if (jobject == null)
				{
					throw new FileNotFoundException("未找到帮助文件：" + FilePath, FilePath);
				}
				if (jobject["Title"] == null)
				{
					throw new ArgumentException("未找到 Title 项");
				}
				this.Title = (string)jobject["Title"];
				this._PublisherMap = (string)(jobject["Description"] ?? "");
				this._DefinitionMap = (string)(jobject["Keywords"] ?? "");
				this._ProcMap = (string)jobject["Logo"];
				this._ParserError = (bool)(jobject["ShowInSearch"] ?? this._ParserError);
				this.m_BroadcasterError = (bool)(jobject["ShowInPublic"] ?? this.m_BroadcasterError);
				this.fieldError = (bool)(jobject["ShowInSnapshot"] ?? this.fieldError);
				this._StrategyMap = new List<string>();
				try
				{
					foreach (object obj in ((IEnumerable)(jobject["Types"] ?? ModBase.GetJson("[]"))))
					{
						object objectValue = RuntimeHelpers.GetObjectValue(obj);
						this._StrategyMap.Add(Conversions.ToString(objectValue));
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
				if (((bool?)(jobject["IsEvent"] ?? false)).GetValueOrDefault())
				{
					this._ClientError = (string)jobject["EventType"];
					if (this._ClientError == null)
					{
						throw new ArgumentException("未找到 EventType 项");
					}
					this.m_ConfigError = (string)(jobject["EventData"] ?? "");
					this._ReaderError = true;
					return;
				}
				else
				{
					string text = FilePath.ToLower().Replace(".json", ".xaml");
					if (!File.Exists(text))
					{
						throw new FileNotFoundException("未找到帮助条目 .json 对应的 .xaml 文件（" + text + "）");
					}
					this.testsError = ModBase.ReadFile(text, null);
					this._ReaderError = false;
					return;
				}
			}

			// Token: 0x06001789 RID: 6025 RVA: 0x0000D1C4 File Offset: 0x0000B3C4
			public MyListItem ToListItem()
			{
				return this.SetToListItem(new MyListItem());
			}

			// Token: 0x0600178A RID: 6026 RVA: 0x0009A2A0 File Offset: 0x000984A0
			public MyListItem SetToListItem(MyListItem Item)
			{
				string text;
				if (this._ReaderError)
				{
					if (Operators.CompareString(this._ClientError, "弹出窗口", false) == 0)
					{
						text = ModBase.m_SerializerRepository + "Blocks/GrassPath.png";
					}
					else
					{
						text = ModBase.m_SerializerRepository + "Blocks/CommandBlock.png";
					}
				}
				else
				{
					text = ModBase.m_SerializerRepository + "Blocks/Grass.png";
				}
				Item.SnapsToDevicePixels = true;
				Item.Title = this.Title;
				Item.Info = this._PublisherMap;
				Item.Logo = (this._ProcMap ?? text);
				Item.Height = 42.0;
				Item.Type = MyListItem.CheckType.Clickable;
				Item.Tag = this;
				Item.EventType = null;
				Item.EventData = null;
				Item.Click += ((ModMain.HelpEntry._Closure$__.$I15-0 == null) ? (ModMain.HelpEntry._Closure$__.$I15-0 = delegate(object sender, MouseButtonEventArgs e)
				{
					PageOtherHelp.OnItemClick((ModMain.HelpEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null));
				}) : ModMain.HelpEntry._Closure$__.$I15-0);
				return Item;
			}

			// Token: 0x04000C33 RID: 3123
			public string _DescriptorMap;

			// Token: 0x04000C34 RID: 3124
			public string Title;

			// Token: 0x04000C35 RID: 3125
			public string _PublisherMap;

			// Token: 0x04000C36 RID: 3126
			public string _DefinitionMap;

			// Token: 0x04000C37 RID: 3127
			public List<string> _StrategyMap;

			// Token: 0x04000C38 RID: 3128
			public string _ProcMap;

			// Token: 0x04000C39 RID: 3129
			public bool _ParserError;

			// Token: 0x04000C3A RID: 3130
			public bool m_BroadcasterError;

			// Token: 0x04000C3B RID: 3131
			public bool fieldError;

			// Token: 0x04000C3C RID: 3132
			public bool _ReaderError;

			// Token: 0x04000C3D RID: 3133
			public string _ClientError;

			// Token: 0x04000C3E RID: 3134
			public string m_ConfigError;

			// Token: 0x04000C3F RID: 3135
			public string testsError;
		}
	}
}
