using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Controls;
using Microsoft.VisualBasic.CompilerServices;
using NAudio;
using NAudio.Wave;

namespace PCL
{
	// Token: 0x020000B4 RID: 180
	[StandardModule]
	public sealed class ModMusic
	{
		// Token: 0x06000573 RID: 1395 RVA: 0x0002EDFC File Offset: 0x0002CFFC
		private static void MusicListInit(bool ForceReload, string PreventFirst = null)
		{
			if (ForceReload)
			{
				ModMusic.m_SpecificationField = null;
			}
			try
			{
				if (ModMusic.m_SpecificationField == null)
				{
					ModMusic.m_SpecificationField = new List<string>();
					Directory.CreateDirectory(ModBase.Path + "PCL\\Musics\\");
					try
					{
						foreach (FileInfo fileInfo in ModBase.EnumerateFiles(ModBase.Path + "PCL\\Musics\\"))
						{
							string text = fileInfo.Extension.ToLower();
							if (!Enumerable.Contains<string>(new string[]
							{
								".ini",
								".jpg",
								".txt",
								".cfg",
								".lrc",
								".db",
								".png"
							}, text))
							{
								ModMusic.m_SpecificationField.Add(fileInfo.FullName);
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
				ModMusic._ContextField = (List<string>)(Conversions.ToBoolean(ModBase.m_IdentifierRepository.Get("UiMusicRandom", null)) ? ModBase.Shuffle<string>(new List<string>(ModMusic.m_SpecificationField)) : new List<string>(ModMusic.m_SpecificationField));
				if (PreventFirst != null && Operators.CompareString(Enumerable.FirstOrDefault<string>(ModMusic._ContextField), PreventFirst, false) == 0)
				{
					ModMusic._ContextField.RemoveAt(0);
					ModMusic._ContextField.Add(PreventFirst);
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "初始化音乐列表失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0002EF94 File Offset: 0x0002D194
		private static string DequeueNextMusicAddress()
		{
			if (ModMusic.m_SpecificationField == null || !Enumerable.Any<string>(ModMusic.m_SpecificationField) || !Enumerable.Any<string>(ModMusic._ContextField))
			{
				ModMusic.MusicListInit(false, null);
			}
			string text;
			if (Enumerable.Any<string>(ModMusic._ContextField))
			{
				text = ModMusic._ContextField[0];
				ModMusic._ContextField.RemoveAt(0);
			}
			else
			{
				text = null;
			}
			if (!Enumerable.Any<string>(ModMusic._ContextField))
			{
				ModMusic.MusicListInit(false, text);
			}
			return text;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00004E9D File Offset: 0x0000309D
		private static void MusicRefreshUI()
		{
			ModBase.RunInUi((ModMusic._Closure$__.$I5-0 == null) ? (ModMusic._Closure$__.$I5-0 = delegate()
			{
				try
				{
					if (!Enumerable.Any<string>(ModMusic.m_SpecificationField))
					{
						ModMain._ProcessIterator.BtnExtraMusic.Show = false;
					}
					else
					{
						ModMain._ProcessIterator.BtnExtraMusic.Show = true;
						string text;
						if (ModMusic.ReflectReader() == ModMusic.MusicStates.Pause)
						{
							ModMain._ProcessIterator.BtnExtraMusic.Logo = "M803.904 463.936a55.168 55.168 0 0 1 0 96.128l-463.616 264.448C302.848 845.888 256 819.136 256 776.448V247.616c0-42.752 46.848-69.44 84.288-48.064l463.616 264.384z";
							ModMain._ProcessIterator.BtnExtraMusic.ManageField(0.8);
							text = "已暂停：" + ModBase.GetFileNameWithoutExtentionFromPath(ModMusic.m_RequestField);
							if (ModMusic.m_SpecificationField.Count > 1)
							{
								text += "\r\n左键恢复播放，右键播放下一曲。";
							}
							else
							{
								text += "\r\n左键恢复播放，右键重新从头播放。";
							}
						}
						else
						{
							ModMain._ProcessIterator.BtnExtraMusic.Logo = "M348.293565 716.53287V254.797913c0-41.672348 28.004174-78.358261 68.919652-90.37913L815.994435 40.826435c62.775652-18.610087 125.907478 26.579478 125.907478 89.933913v539.158261c8.013913 42.25113-8.94887 89.177043-47.014956 127.109565a232.848696 232.848696 0 0 1-170.785392 65.758609c-61.885217-2.938435-111.081739-33.435826-129.113043-80.050087-18.031304-46.614261-2.137043-102.177391 41.672348-145.853218a232.848696 232.848696 0 0 1 170.785391-65.80313c21.014261 1.024 40.514783 5.164522 57.878261 12.065391V233.338435c0-12.109913-10.551652-20.034783-20.569044-20.034783a24.620522 24.620522 0 0 0-5.787826 0.934957L439.785739 338.18713a19.545043 19.545043 0 0 0-14.825739 19.144348v438.984348H423.846957c11.53113 43.987478-5.164522 94.208-45.412174 134.322087a232.848696 232.848696 0 0 1-170.785392 65.758609c-61.885217-2.938435-111.081739-33.435826-129.113043-80.050087-18.031304-46.614261-2.137043-102.177391 41.672348-145.853218a232.848696 232.848696 0 0 1 170.785391-65.80313c20.791652 1.024 40.069565 5.075478 57.299478 11.842783z";
							ModMain._ProcessIterator.BtnExtraMusic.ManageField(1.0);
							text = "正在播放：" + ModBase.GetFileNameWithoutExtentionFromPath(ModMusic.m_RequestField);
							if (ModMusic.m_SpecificationField.Count > 1)
							{
								text += "\r\n左键暂停，右键播放下一曲。";
							}
							else
							{
								text += "\r\n左键暂停，右键重新从头播放。";
							}
						}
						ModMain._ProcessIterator.BtnExtraMusic.ToolTip = text;
						ToolTipService.SetVerticalOffset(ModMain._ProcessIterator.BtnExtraMusic, (double)(text.Contains("\n") ? 10 : 16));
					}
					if (ModMain.m_OrderIterator != null)
					{
						ModMain.m_OrderIterator.MusicRefreshUI();
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "刷新背景音乐 UI 失败", ModBase.LogLevel.Feedback, "出现错误");
				}
			}) : ModMusic._Closure$__.$I5-0, false);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0002F004 File Offset: 0x0002D204
		public static void MusicControlPause()
		{
			if (ModMusic.mockField == null)
			{
				ModMain.Hint("音乐播放尚未开始！", ModMain.HintType.Critical, true);
				return;
			}
			ModMusic.MusicStates musicStates = ModMusic.ReflectReader();
			if (musicStates == ModMusic.MusicStates.Play)
			{
				ModMusic.MusicPause();
				return;
			}
			if (musicStates == ModMusic.MusicStates.Pause)
			{
				ModMusic.MusicResume();
				return;
			}
			ModBase.Log("[Music] 音乐目前为停止状态，已强制尝试开始播放", ModBase.LogLevel.Debug, "出现错误");
			ModMusic.MusicRefreshPlay(false, false);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0002F058 File Offset: 0x0002D258
		public static void MusicControlNext()
		{
			if (ModMusic.m_SpecificationField.Count == 1)
			{
				ModMusic.MusicStartPlay(ModMusic.m_RequestField, false);
				ModMain.Hint("重新播放：" + ModBase.GetFileNameFromPath(ModMusic.m_RequestField), ModMain.HintType.Finish, true);
			}
			else
			{
				string text = ModMusic.DequeueNextMusicAddress();
				if (text == null)
				{
					ModMain.Hint("没有可以播放的音乐！", ModMain.HintType.Critical, true);
				}
				else
				{
					ModMusic.MusicStartPlay(text, false);
					ModMain.Hint("正在播放：" + ModBase.GetFileNameFromPath(text), ModMain.HintType.Finish, true);
				}
			}
			ModMusic.MusicRefreshUI();
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0002F0D4 File Offset: 0x0002D2D4
		public static ModMusic.MusicStates ReflectReader()
		{
			ModMusic.MusicStates result;
			if (ModMusic.mockField == null)
			{
				result = ModMusic.MusicStates.Stop;
			}
			else
			{
				object left = NewLateBinding.LateGet(ModMusic.mockField, null, "PlaybackState", new object[0], null, null, null);
				if (Operators.ConditionalCompareObjectEqual(left, 0, false))
				{
					result = ModMusic.MusicStates.Stop;
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 2, false))
				{
					result = ModMusic.MusicStates.Pause;
				}
				else
				{
					result = ModMusic.MusicStates.Play;
				}
			}
			return result;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0002F130 File Offset: 0x0002D330
		public static void MusicRefreshPlay(bool ShowHint, bool IsFirstLoad = false)
		{
			try
			{
				ModMusic.MusicListInit(true, null);
				if (!Enumerable.Any<string>(ModMusic.m_SpecificationField))
				{
					if (ModMusic.mockField == null)
					{
						if (ShowHint)
						{
							ModMain.Hint("未检测到可用的背景音乐！", ModMain.HintType.Critical, true);
						}
					}
					else
					{
						ModMusic.mockField = null;
						if (ShowHint)
						{
							ModMain.Hint("背景音乐已清除！", ModMain.HintType.Finish, true);
						}
					}
				}
				else
				{
					string text = ModMusic.DequeueNextMusicAddress();
					if (text == null)
					{
						if (ShowHint)
						{
							ModMain.Hint("没有可以播放的音乐！", ModMain.HintType.Critical, true);
						}
					}
					else
					{
						try
						{
							ModMusic.MusicStartPlay(text, IsFirstLoad);
							if (ShowHint)
							{
								ModMain.Hint("背景音乐已刷新：" + ModBase.GetFileNameFromPath(text), ModMain.HintType.Finish, false);
							}
						}
						catch (Exception ex)
						{
						}
					}
				}
				ModMusic.MusicRefreshUI();
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, "刷新背景音乐播放失败", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0002F20C File Offset: 0x0002D40C
		private static void MusicStartPlay(string Address, bool IsFirstLoad = false)
		{
			if (Address != null)
			{
				ModBase.Log("[Music] 播放开始：" + Address, ModBase.LogLevel.Normal, "出现错误");
				ModMusic.m_RequestField = Address;
				ModBase.RunInNewThread(delegate
				{
					ModMusic.MusicLoop(IsFirstLoad);
				}, "Music", ThreadPriority.BelowNormal);
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0002F260 File Offset: 0x0002D460
		public static bool MusicPause()
		{
			bool result;
			if (ModMusic.ReflectReader() == ModMusic.MusicStates.Play)
			{
				ModBase.RunInThread((ModMusic._Closure$__.$I13-0 == null) ? (ModMusic._Closure$__.$I13-0 = delegate()
				{
					ModBase.Log("[Music] 已暂停播放", ModBase.LogLevel.Normal, "出现错误");
					WaveOut waveOut = ModMusic.mockField;
					if (waveOut != null)
					{
						NewLateBinding.LateCall(waveOut, null, "Pause", new object[0], null, null, null, true);
					}
					ModMusic.MusicRefreshUI();
				}) : ModMusic._Closure$__.$I13-0);
				result = true;
			}
			else
			{
				ModBase.Log(string.Format("[Music] 无需暂停播放，当前状态为 {0}", ModMusic.ReflectReader()), ModBase.LogLevel.Normal, "出现错误");
				result = false;
			}
			return result;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0002F2C4 File Offset: 0x0002D4C4
		public static bool MusicResume()
		{
			bool result;
			if (ModMusic.ReflectReader() != ModMusic.MusicStates.Play && Enumerable.Any<string>(ModMusic.m_SpecificationField))
			{
				ModBase.RunInThread((ModMusic._Closure$__.$I14-0 == null) ? (ModMusic._Closure$__.$I14-0 = delegate()
				{
					ModBase.Log("[Music] 已恢复播放", ModBase.LogLevel.Normal, "出现错误");
					WaveOut waveOut = ModMusic.mockField;
					if (waveOut != null)
					{
						NewLateBinding.LateCall(waveOut, null, "Play", new object[0], null, null, null, true);
					}
					ModMusic.MusicRefreshUI();
				}) : ModMusic._Closure$__.$I14-0);
				result = true;
			}
			else
			{
				ModBase.Log(string.Format("[Music] 无需继续播放，当前状态为 {0}", ModMusic.ReflectReader()), ModBase.LogLevel.Normal, "出现错误");
				result = false;
			}
			return result;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0002F334 File Offset: 0x0002D534
		private static void MusicLoop(bool IsFirstLoad = false)
		{
			WaveOut waveOut = null;
			WaveStream waveStream = null;
			try
			{
				waveOut = new WaveOut();
				ModMusic.mockField = waveOut;
				waveStream = new AudioFileReader(ModMusic.m_RequestField);
				waveOut.Init(waveStream);
				waveOut.Play();
				if (Conversions.ToBoolean(IsFirstLoad && Conversions.ToBoolean(Operators.NotObject(ModBase.m_IdentifierRepository.Get("UiMusicAuto", null)))))
				{
					waveOut.Pause();
				}
				ModMusic.MusicRefreshUI();
				int num = 0;
				while (waveOut.Equals(RuntimeHelpers.GetObjectValue(ModMusic.mockField)) && waveOut.PlaybackState != null)
				{
					ModMusic._Closure$__17-0 CS$<>8__locals1 = new ModMusic._Closure$__17-0(CS$<>8__locals1);
					if (Operators.ConditionalCompareObjectNotEqual(ModBase.m_IdentifierRepository.Get("UiMusicVolume", null), num, false))
					{
						num = Conversions.ToInteger(ModBase.m_IdentifierRepository.Get("UiMusicVolume", null));
						waveOut.Volume = (float)((double)num / 1000.0);
					}
					CS$<>8__locals1.$VB$Local_Percent = waveStream.CurrentTime.TotalMilliseconds / waveStream.TotalTime.TotalMilliseconds;
					ModBase.RunInUi(delegate()
					{
						ModMain._ProcessIterator.BtnExtraMusic.Progress = CS$<>8__locals1.$VB$Local_Percent;
					}, false);
					Thread.Sleep(100);
				}
				if (waveOut.PlaybackState == null && Enumerable.Any<string>(ModMusic.m_SpecificationField))
				{
					ModMusic.MusicStartPlay(ModMusic.DequeueNextMusicAddress(), false);
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "播放音乐出现内部错误（" + ModMusic.m_RequestField + "）", ModBase.LogLevel.Developer, "出现错误");
				if (ex is MmException && (ex.Message.Contains("NoDriver") || ex.Message.Contains("BadDeviceId")))
				{
					ModMain.Hint("由于音频设备变更，音乐播放功能在重启 PCL 后才能恢复！", ModMain.HintType.Critical, true);
					Thread.Sleep(1000000000);
				}
				if (!ex.Message.Contains("Got a frame at sample rate") && !ex.Message.Contains("does not support changes to"))
				{
					if ((!ModMusic.m_RequestField.EndsWithF(".wav", true) && !ModMusic.m_RequestField.EndsWithF(".mp3", true) && !ModMusic.m_RequestField.EndsWithF(".flac", true)) || ex.Message.Contains("0xC00D36C4"))
					{
						ModMain.Hint("播放音乐失败（" + ModBase.GetFileNameFromPath(ModMusic.m_RequestField) + "）：PCL 可能不支持此音乐格式，请将格式转换为 .wav、.mp3 或 .flac 后再试", ModMain.HintType.Critical, true);
					}
					else
					{
						ModBase.Log(ex, "播放音乐失败（" + ModBase.GetFileNameFromPath(ModMusic.m_RequestField) + "）", ModBase.LogLevel.Hint, "出现错误");
					}
				}
				else
				{
					ModMain.Hint("播放音乐失败（" + ModBase.GetFileNameFromPath(ModMusic.m_RequestField) + "）：PCL 不支持播放音频属性在中途发生变化的音乐", ModMain.HintType.Critical, true);
				}
				ModMusic.m_SpecificationField.Remove(ModMusic.m_RequestField);
				ModMusic._ContextField.Remove(ModMusic.m_RequestField);
				ModMusic.MusicRefreshUI();
				Thread.Sleep(2000);
				if (ex is FileNotFoundException)
				{
					ModMusic.MusicRefreshPlay(true, IsFirstLoad);
				}
				else
				{
					ModMusic.MusicStartPlay(ModMusic.DequeueNextMusicAddress(), IsFirstLoad);
				}
			}
			finally
			{
				if (waveOut != null)
				{
					waveOut.Dispose();
				}
				if (waveStream != null)
				{
					waveStream.Dispose();
				}
				ModMusic.MusicRefreshUI();
			}
		}

		// Token: 0x040002FC RID: 764
		public static List<string> _ContextField = null;

		// Token: 0x040002FD RID: 765
		public static List<string> m_SpecificationField = null;

		// Token: 0x040002FE RID: 766
		public static WaveOut mockField = null;

		// Token: 0x040002FF RID: 767
		private static string m_RequestField = "";

		// Token: 0x020000B5 RID: 181
		public enum MusicStates
		{
			// Token: 0x04000301 RID: 769
			Stop,
			// Token: 0x04000302 RID: 770
			Play,
			// Token: 0x04000303 RID: 771
			Pause
		}
	}
}
