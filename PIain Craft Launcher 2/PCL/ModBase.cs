using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using Newtonsoft.Json;
using Ookii.Dialogs.Wpf;
using PCL.My;
using PCL.My.Resources;

namespace PCL
{
	// Token: 0x020001FD RID: 509
	[StandardModule]
	public sealed class ModBase
	{
		// Token: 0x060017A7 RID: 6055 RVA: 0x0009A900 File Offset: 0x00098B00
		public static string RadixConvert(string Input, int FromRadix, int ToRadix)
		{
			checked
			{
				string result;
				if (string.IsNullOrEmpty(Input))
				{
					result = "0";
				}
				else
				{
					bool flag;
					if (flag = Input.StartsWithF("-", false))
					{
						Input = Input.TrimStart(new char[]
						{
							'-'
						});
					}
					long num = 0L;
					long num2 = 1L;
					try
					{
						foreach (int num3 in Enumerable.Select<char, int>(Enumerable.Reverse<char>(Input), (ModBase._Closure$__.$I29-0 == null) ? (ModBase._Closure$__.$I29-0 = ((char l) => "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz/+=".IndexOfF(Conversions.ToString(l), false))) : ModBase._Closure$__.$I29-0))
						{
							num += unchecked((long)num3) * num2;
							num2 *= unchecked((long)FromRadix);
						}
					}
					finally
					{
						IEnumerator<int> enumerator;
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
					string str = "";
					while (num > 0L)
					{
						int num4 = (int)(num % unchecked((long)ToRadix));
						num = (long)Math.Round((double)(num - unchecked((long)num4)) / (double)ToRadix);
						str = Conversions.ToString("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz/+="[num4]) + str;
					}
					result = (flag ? "-" : "") + str;
				}
				return result;
			}
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0009AA2C File Offset: 0x00098C2C
		public static double MathBezier(double x, double x1, double y1, double x2, double y2, double acc = 0.01)
		{
			double result;
			if (x > 0.0 && !double.IsNaN(x))
			{
				if (x >= 1.0)
				{
					result = 1.0;
				}
				else
				{
					object obj = x;
					object obj2;
					do
					{
						obj2 = Operators.MultiplyObject(Operators.MultiplyObject(3, obj), Operators.AddObject(Operators.AddObject(Operators.MultiplyObject(Operators.MultiplyObject(0.33333333 + x1 - x2, obj), obj), Operators.MultiplyObject(x2 - 2.0 * x1, obj)), x1));
						obj = Operators.AddObject(obj, Operators.MultiplyObject(Operators.SubtractObject(x, obj2), 0.5));
					}
					while (!Operators.ConditionalCompareObjectLess(NewLateBinding.LateGet(null, typeof(Math), "Abs", new object[]
					{
						Operators.SubtractObject(obj2, x)
					}, null, null, null), acc, false));
					result = Conversions.ToDouble(Operators.MultiplyObject(Operators.MultiplyObject(3, obj), Operators.AddObject(Operators.AddObject(Operators.MultiplyObject(Operators.MultiplyObject(0.33333333 + y1 - y2, obj), obj), Operators.MultiplyObject(y2 - 2.0 * y1, obj)), y1)));
				}
			}
			else
			{
				result = 0.0;
			}
			return result;
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0000D3C5 File Offset: 0x0000B5C5
		public static byte MathByte(double d)
		{
			if (d < 0.0)
			{
				d = 0.0;
			}
			if (d > 255.0)
			{
				d = 255.0;
			}
			return checked((byte)Math.Round(d));
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0009ABA0 File Offset: 0x00098DA0
		public static ModBase.MyColor MathRound(ModBase.MyColor col, int w = 0)
		{
			return new ModBase.MyColor
			{
				mapperError = Math.Round(col.mapperError, w),
				m_ThreadError = Math.Round(col.m_ThreadError, w),
				_PropertyError = Math.Round(col._PropertyError, w),
				composerError = Math.Round(col.composerError, w)
			};
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0000D3FC File Offset: 0x0000B5FC
		public static double MathPercent(double ValueA, double ValueB, double Percent)
		{
			return Math.Round(ValueA * (1.0 - Percent) + ValueB * Percent, 6);
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0000D415 File Offset: 0x0000B615
		public static ModBase.MyColor MathPercent(ModBase.MyColor ValueA, ModBase.MyColor ValueB, double Percent)
		{
			return ModBase.MathRound(ValueA * (1.0 - Percent) + ValueB * Percent, 6);
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x0000D43A File Offset: 0x0000B63A
		public static double MathClamp(double value, double min, double max)
		{
			return Math.Max(min, Math.Min(max, value));
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0009ABFC File Offset: 0x00098DFC
		public static int MathSgn(double Value)
		{
			int result;
			if (Value == 0.0)
			{
				result = 0;
			}
			else if (Value > 0.0)
			{
				result = 1;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0009AC2C File Offset: 0x00098E2C
		public static void RenameReg(RegistryKey parentKey, string subKeyName, string newSubKeyName)
		{
			if (Enumerable.Contains<string>(parentKey.GetSubKeyNames(), newSubKeyName))
			{
				parentKey.DeleteSubKeyTree(newSubKeyName, false);
			}
			RegistryKey registryKey = parentKey.OpenSubKey(subKeyName);
			if (!Information.IsNothing(registryKey))
			{
				RegistryKey registryKey2 = parentKey.CreateSubKey(newSubKeyName);
				if (registryKey.GetSubKeyNames().Length > 0)
				{
					throw new NotSupportedException("不支持对包含子键的子键进行重命名：" + registryKey.GetSubKeyNames()[0] + "。");
				}
				foreach (string name in registryKey.GetValueNames())
				{
					object objectValue = RuntimeHelpers.GetObjectValue(registryKey.GetValue(name));
					RegistryValueKind valueKind = registryKey.GetValueKind(name);
					registryKey2.SetValue(name, RuntimeHelpers.GetObjectValue(objectValue), valueKind);
				}
				parentKey.DeleteSubKeyTree(subKeyName, false);
			}
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0009ACDC File Offset: 0x00098EDC
		public static string ReadReg(string Key, string DefaultValue = "")
		{
			string result;
			try
			{
				RegistryKey registryKey = MyWpfExtension.ManageParser().Registry.CurrentUser.OpenSubKey("Software\\PCL", true);
				if (registryKey == null)
				{
					result = DefaultValue;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine(Conversions.ToString(registryKey.GetValue(Key)));
					string text = stringBuilder.ToString().Replace("\r\n", "");
					result = ((Operators.CompareString(text, "", false) == 0) ? DefaultValue : text);
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "读取注册表出错：" + Key, ModBase.LogLevel.Hint, "出现错误");
				result = DefaultValue;
			}
			return result;
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0009AD88 File Offset: 0x00098F88
		public static void WriteReg(string Key, string Value, bool ShowException = false)
		{
			try
			{
				RegistryKey currentUser = MyWpfExtension.ManageParser().Registry.CurrentUser;
				RegistryKey registryKey = currentUser.OpenSubKey("Software\\PCL", true);
				if (registryKey == null)
				{
					registryKey = currentUser.CreateSubKey("Software\\PCL");
				}
				registryKey.SetValue(Key, Value);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "写入注册表出错：" + Key, ShowException ? ModBase.LogLevel.Hint : ModBase.LogLevel.Developer, "出现错误");
				if (ShowException)
				{
					throw;
				}
			}
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0000D449 File Offset: 0x0000B649
		public static void IniClearCache(string FileName)
		{
			if (!FileName.Contains(":\\"))
			{
				FileName = string.Format("{0}PCL\\{1}.ini", ModBase.Path, FileName);
			}
			if (ModBase.stateRepository.ContainsKey(FileName))
			{
				ModBase.stateRepository.Remove(FileName);
			}
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0009AE0C File Offset: 0x0009900C
		private static Dictionary<string, string> IniGetContent(string FileName)
		{
			Dictionary<string, string> result;
			try
			{
				if (!FileName.Contains(":\\"))
				{
					FileName = string.Format("{0}PCL\\{1}.ini", ModBase.Path, FileName);
				}
				if (ModBase.stateRepository.ContainsKey(FileName))
				{
					result = ModBase.stateRepository[FileName];
				}
				else if (!File.Exists(FileName))
				{
					result = null;
				}
				else
				{
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					foreach (string text in ModBase.ReadFile(FileName, null).Split(Enumerable.ToArray<char>("\r\n"), StringSplitOptions.RemoveEmptyEntries))
					{
						int num = text.IndexOfF(":", false);
						if (num > 0)
						{
							dictionary[text.Substring(0, num)] = text.Substring(checked(num + 1));
						}
					}
					ModBase.stateRepository[FileName] = dictionary;
					result = dictionary;
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, string.Format("生成 ini 文件缓存失败（{0}）", FileName), ModBase.LogLevel.Hint, "出现错误");
				result = null;
			}
			return result;
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0009AF10 File Offset: 0x00099110
		public static string ReadIni(string FileName, string Key, string DefaultValue = "")
		{
			Dictionary<string, string> dictionary = ModBase.IniGetContent(FileName);
			string result;
			if (dictionary != null && dictionary.ContainsKey(Key))
			{
				result = dictionary[Key];
			}
			else
			{
				result = DefaultValue;
			}
			return result;
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0009AF40 File Offset: 0x00099140
		public static void WriteIni(string FileName, string Key, string Value)
		{
			try
			{
				Key = Key.Replace("\r", "").Replace("\n", "").Replace(":", "");
				Value = Value.Replace("\r", "").Replace("\n", "");
				Dictionary<string, string> dictionary = ModBase.IniGetContent(FileName);
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, string>();
				}
				if (!dictionary.ContainsKey(Key) || Operators.CompareString(dictionary[Key], Value, false) != 0)
				{
					dictionary[Key] = Value;
					StringBuilder stringBuilder = new StringBuilder();
					try
					{
						foreach (KeyValuePair<string, string> keyValuePair in dictionary)
						{
							stringBuilder.Append(keyValuePair.Key);
							stringBuilder.Append(":");
							stringBuilder.Append(keyValuePair.Value);
							stringBuilder.Append("\r\n");
						}
					}
					finally
					{
						Dictionary<string, string>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					if (!FileName.Contains(":\\"))
					{
						FileName = string.Format("{0}PCL\\{1}.ini", ModBase.Path, FileName);
					}
					ModBase.WriteFile(FileName, stringBuilder.ToString(), false, null);
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, string.Format("写入文件失败（{0} → {1}:{2}）", FileName, Key, Value), ModBase.LogLevel.Hint, "出现错误");
			}
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0009B0C4 File Offset: 0x000992C4
		public static string GetPathFromFullPath(string FilePath)
		{
			if (!FilePath.Contains("\\") && !FilePath.Contains("/"))
			{
				throw new Exception("不包含路径：" + FilePath);
			}
			checked
			{
				string text;
				if (!FilePath.EndsWithF("\\", false) && !FilePath.EndsWithF("/", false))
				{
					text = Strings.Left(FilePath, FilePath.LastIndexOfAny(new char[]
					{
						'\\',
						'/'
					}) + 1);
					if (Operators.CompareString(text, "", false) == 0)
					{
						throw new Exception("不包含路径：" + FilePath);
					}
				}
				else
				{
					bool flag = FilePath.EndsWithF("\\", false);
					FilePath = Strings.Left(FilePath, Strings.Len(FilePath) - 1);
					text = Strings.Left(FilePath, FilePath.LastIndexOfAny(new char[]
					{
						'\\',
						'/'
					})) + (flag ? "\\" : "/");
					if (Operators.CompareString(text, "", false) == 0)
					{
						throw new Exception("不包含路径：" + FilePath);
					}
				}
				return text;
			}
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0009B1C4 File Offset: 0x000993C4
		public static string GetFileNameFromPath(string FilePath)
		{
			FilePath = FilePath.Replace("/", "\\");
			if (FilePath.EndsWithF("\\", false))
			{
				throw new Exception("不包含文件名：" + FilePath);
			}
			if (FilePath.Contains("?"))
			{
				FilePath = FilePath.Substring(0, FilePath.IndexOfF("?", false));
			}
			if (FilePath.Contains("\\"))
			{
				FilePath = FilePath.Substring(checked(FilePath.LastIndexOfF("\\", false) + 1));
			}
			int length = FilePath.Length;
			if (length == 0)
			{
				throw new Exception("不包含文件名：" + FilePath);
			}
			if (length > 250)
			{
				throw new PathTooLongException("文件名过长：" + FilePath);
			}
			return FilePath;
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0009B27C File Offset: 0x0009947C
		public static string GetFileNameWithoutExtentionFromPath(string FilePath)
		{
			string fileNameFromPath = ModBase.GetFileNameFromPath(FilePath);
			string result;
			if (fileNameFromPath.Contains("."))
			{
				result = fileNameFromPath.Substring(0, fileNameFromPath.LastIndexOfF(".", false));
			}
			else
			{
				result = fileNameFromPath;
			}
			return result;
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0009B2B8 File Offset: 0x000994B8
		public static string GetFolderNameFromPath(string FolderPath)
		{
			string result;
			if (!FolderPath.EndsWithF(":\\", false) && !FolderPath.EndsWithF(":\\\\", false))
			{
				if (FolderPath.EndsWithF("\\", false) || FolderPath.EndsWithF("/", false))
				{
					FolderPath = Strings.Left(FolderPath, checked(FolderPath.Length - 1));
				}
				result = ModBase.GetFileNameFromPath(FolderPath);
			}
			else
			{
				result = FolderPath.Substring(0, 1);
			}
			return result;
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0009B320 File Offset: 0x00099520
		public static void CopyFile(string FromPath, string ToPath)
		{
			try
			{
				if (!FromPath.Contains(":\\"))
				{
					FromPath = ModBase.Path + FromPath;
				}
				if (!ToPath.Contains(":\\"))
				{
					ToPath = ModBase.Path + ToPath;
				}
				if (Operators.CompareString(FromPath, ToPath, false) != 0)
				{
					Directory.CreateDirectory(ModBase.GetPathFromFullPath(ToPath));
					File.Copy(FromPath, ToPath, true);
				}
			}
			catch (Exception innerException)
			{
				throw new Exception("复制文件出错：" + FromPath + " → " + ToPath, innerException);
			}
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0009B3B4 File Offset: 0x000995B4
		public static byte[] ReadFileBytes(string FilePath, Encoding Encoding = null)
		{
			checked
			{
				byte[] result;
				try
				{
					if (!FilePath.Contains(":\\"))
					{
						FilePath = ModBase.Path + FilePath;
					}
					if (File.Exists(FilePath))
					{
						byte[] array;
						using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
						{
							array = new byte[(int)(fileStream.Length - 1L) + 1];
							fileStream.Read(array, 0, (int)fileStream.Length);
						}
						result = array;
					}
					else
					{
						ModBase.Log("[System] 欲读取的文件不存在，已返回空内容：" + FilePath, ModBase.LogLevel.Normal, "出现错误");
						result = new byte[0];
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "读取文件出错：" + FilePath, ModBase.LogLevel.Debug, "出现错误");
					result = new byte[0];
				}
				return result;
			}
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0009B490 File Offset: 0x00099690
		public static string ReadFile(string FilePath, Encoding Encoding = null)
		{
			byte[] bytes = ModBase.ReadFileBytes(FilePath, null);
			return (Encoding == null) ? ModBase.DecodeBytes(bytes) : Encoding.GetString(bytes);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0009B4BC File Offset: 0x000996BC
		public static string ReadFile(Stream Stream, Encoding Encoding = null)
		{
			string result;
			try
			{
				byte[] array = new byte[16385];
				int i = Stream.Read(array, 0, 16384);
				List<byte> list = new List<byte>();
				while (i > 0)
				{
					if (i > 0)
					{
						list.AddRange(Enumerable.ToList<byte>(array).GetRange(0, i));
					}
					i = Stream.Read(array, 0, 16384);
				}
				byte[] bytes = list.ToArray();
				result = (Encoding ?? ModBase.GetEncoding(bytes)).GetString(bytes);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "读取流出错", ModBase.LogLevel.Debug, "出现错误");
				result = "";
			}
			return result;
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0009B56C File Offset: 0x0009976C
		public static void WriteFile(string FilePath, string Text, bool Append = false, Encoding Encoding = null)
		{
			if (!FilePath.Contains(":\\"))
			{
				FilePath = ModBase.Path + FilePath;
			}
			Directory.CreateDirectory(ModBase.GetPathFromFullPath(FilePath));
			if (File.Exists(FilePath))
			{
				using (StreamWriter streamWriter = new StreamWriter(FilePath, Append, Encoding ?? ModBase.GetEncoding(ModBase.ReadFileBytes(FilePath, null))))
				{
					streamWriter.Write(Text);
					streamWriter.Flush();
					streamWriter.Close();
					return;
				}
			}
			File.WriteAllText(FilePath, Text, Encoding ?? new UTF8Encoding(false));
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0000D483 File Offset: 0x0000B683
		public static void WriteFile(string FilePath, byte[] Content, bool Append = false)
		{
			if (!FilePath.Contains(":\\"))
			{
				FilePath = ModBase.Path + FilePath;
			}
			Directory.CreateDirectory(ModBase.GetPathFromFullPath(FilePath));
			File.WriteAllBytes(FilePath, Content);
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0009B604 File Offset: 0x00099804
		public static bool WriteFile(string FilePath, Stream Stream)
		{
			bool result;
			try
			{
				if (!FilePath.Contains(":\\"))
				{
					FilePath = ModBase.Path + FilePath;
				}
				Directory.CreateDirectory(ModBase.GetPathFromFullPath(FilePath));
				using (FileStream fileStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
				{
					byte[] array = new byte[16385];
					for (int i = Stream.Read(array, 0, 16384); i > 0; i = Stream.Read(array, 0, 16384))
					{
						if (i > 0)
						{
							fileStream.Write(array, 0, i);
						}
					}
					fileStream.Close();
				}
				result = true;
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "保存流出错", ModBase.LogLevel.Debug, "出现错误");
				result = false;
			}
			return result;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0009B6D0 File Offset: 0x000998D0
		public static Encoding GetEncoding(byte[] Bytes)
		{
			Encoding result;
			if (Enumerable.Count<byte>(Bytes) < 3)
			{
				result = new UTF8Encoding(false);
			}
			else if (Bytes[0] >= 239)
			{
				if (Bytes[0] == 239 && Bytes[1] == 187)
				{
					result = new UTF8Encoding(true);
				}
				else if (Bytes[0] == 254 && Bytes[1] == 255)
				{
					result = Encoding.BigEndianUnicode;
				}
				else if (Bytes[0] == 255 && Bytes[1] == 254)
				{
					result = Encoding.Unicode;
				}
				else
				{
					result = Encoding.GetEncoding("GB18030");
				}
			}
			else
			{
				string @string = Encoding.UTF8.GetString(Bytes);
				char value = Encoding.UTF8.GetString(new byte[]
				{
					239,
					191,
					189
				}).ToCharArray()[0];
				if (@string.Contains(Conversions.ToString(value)))
				{
					result = Encoding.GetEncoding("GB18030");
				}
				else
				{
					result = new UTF8Encoding(false);
				}
			}
			return result;
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0009B7B4 File Offset: 0x000999B4
		public static string DecodeBytes(byte[] Bytes)
		{
			int num = Bytes.Length;
			checked
			{
				string result;
				if (num < 3)
				{
					result = Encoding.UTF8.GetString(Bytes);
				}
				else if (Bytes[0] >= 239)
				{
					if (Bytes[0] == 239 && Bytes[1] == 187)
					{
						result = Encoding.UTF8.GetString(Bytes, 3, num - 3);
					}
					else if (Bytes[0] == 254 && Bytes[1] == 255)
					{
						result = Encoding.BigEndianUnicode.GetString(Bytes, 3, num - 3);
					}
					else if (Bytes[0] == 255 && Bytes[1] == 254)
					{
						result = Encoding.Unicode.GetString(Bytes, 3, num - 3);
					}
					else
					{
						result = Encoding.GetEncoding("GB18030").GetString(Bytes, 3, num - 3);
					}
				}
				else
				{
					string @string = Encoding.UTF8.GetString(Bytes);
					char value = Encoding.UTF8.GetString(new byte[]
					{
						239,
						191,
						189
					}).ToCharArray()[0];
					if (@string.Contains(Conversions.ToString(value)))
					{
						result = Encoding.GetEncoding("GB18030").GetString(Bytes);
					}
					else
					{
						result = @string;
					}
				}
				return result;
			}
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0009B8CC File Offset: 0x00099ACC
		public static string SelectAs(string Title, string FileName, string FileFilter = null, string DefaultDir = null)
		{
			string text;
			using (System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog())
			{
				saveFileDialog.AddExtension = true;
				saveFileDialog.AutoUpgradeEnabled = true;
				saveFileDialog.Title = Title;
				saveFileDialog.FileName = FileName;
				if (FileFilter != null)
				{
					saveFileDialog.Filter = FileFilter;
				}
				if (DefaultDir != null)
				{
					saveFileDialog.InitialDirectory = DefaultDir;
				}
				saveFileDialog.ShowDialog();
				text = (saveFileDialog.FileName.Contains(":\\") ? saveFileDialog.FileName : "");
				ModBase.Log("[UI] 选择文件返回：" + text, ModBase.LogLevel.Normal, "出现错误");
			}
			return text;
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0009B96C File Offset: 0x00099B6C
		public static string SelectFile(string FileFilter, string Title)
		{
			string fileName;
			using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
			{
				openFileDialog.AddExtension = true;
				openFileDialog.AutoUpgradeEnabled = true;
				openFileDialog.CheckFileExists = true;
				openFileDialog.Filter = FileFilter;
				openFileDialog.Multiselect = false;
				openFileDialog.Title = Title;
				openFileDialog.ValidateNames = true;
				openFileDialog.ShowDialog();
				ModBase.Log("[UI] 选择单个文件返回：" + openFileDialog.FileName, ModBase.LogLevel.Normal, "出现错误");
				fileName = openFileDialog.FileName;
			}
			return fileName;
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0009B9F8 File Offset: 0x00099BF8
		public static string[] SelectFiles(string FileFilter, string Title)
		{
			string[] fileNames;
			using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
			{
				openFileDialog.AddExtension = true;
				openFileDialog.AutoUpgradeEnabled = true;
				openFileDialog.CheckFileExists = true;
				openFileDialog.Filter = FileFilter;
				openFileDialog.Multiselect = true;
				openFileDialog.Title = Title;
				openFileDialog.ValidateNames = true;
				openFileDialog.ShowDialog();
				ModBase.Log("[UI] 选择多个文件返回：" + openFileDialog.FileNames.Join(","), ModBase.LogLevel.Normal, "出现错误");
				fileNames = openFileDialog.FileNames;
			}
			return fileNames;
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x0009BA8C File Offset: 0x00099C8C
		public static string SelectFolder(string Title = "选择文件夹")
		{
			VistaFolderBrowserDialog vistaFolderBrowserDialog = new VistaFolderBrowserDialog
			{
				ShowNewFolderButton = true,
				RootFolder = Environment.SpecialFolder.Desktop,
				Description = Title,
				UseDescriptionForTitle = true
			};
			vistaFolderBrowserDialog.ShowDialog();
			string text = string.IsNullOrEmpty(vistaFolderBrowserDialog.SelectedPath) ? "" : (vistaFolderBrowserDialog.SelectedPath + (vistaFolderBrowserDialog.SelectedPath.EndsWithF("\\", false) ? "" : "\\"));
			ModBase.Log("[UI] 选择文件夹返回：" + text, ModBase.LogLevel.Normal, "出现错误");
			return text;
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0009BB18 File Offset: 0x00099D18
		public static bool CheckPermission(string Path)
		{
			bool result;
			try
			{
				if (string.IsNullOrEmpty(Path))
				{
					result = false;
				}
				else
				{
					if (!Path.EndsWithF("\\", false))
					{
						Path += "\\";
					}
					if (!Path.EndsWithF(":\\System Volume Information\\", false) && !Path.EndsWithF(":\\$RECYCLE.BIN\\", false))
					{
						if (!Directory.Exists(Path))
						{
							result = false;
						}
						else
						{
							string str = "CheckPermission" + Conversions.ToString(ModBase.GetUuid());
							if (File.Exists(Path + str))
							{
								File.Delete(Path + str);
							}
							File.Create(Path + str).Dispose();
							File.Delete(Path + str);
							result = true;
						}
					}
					else
					{
						result = false;
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "没有对文件夹 " + Path + " 的权限，请尝试以管理员权限运行 PCL", ModBase.LogLevel.Debug, "出现错误");
				result = false;
			}
			return result;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0009BC0C File Offset: 0x00099E0C
		public static void CheckPermissionWithException(string Path)
		{
			if (string.IsNullOrWhiteSpace(Path))
			{
				throw new ArgumentNullException("文件夹名不能为空！");
			}
			if (!Path.EndsWithF("\\", false))
			{
				Path += "\\";
			}
			if (!Directory.Exists(Path))
			{
				throw new DirectoryNotFoundException("文件夹不存在！");
			}
			if (File.Exists(Path + "CheckPermission"))
			{
				File.Delete(Path + "CheckPermission");
			}
			File.Create(Path + "CheckPermission").Dispose();
			File.Delete(Path + "CheckPermission");
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x0009BCA4 File Offset: 0x00099EA4
		public static string smethod_0(string FilePath)
		{
			bool flag = false;
			checked
			{
				string result;
				for (;;)
				{
					try
					{
						StringBuilder stringBuilder = new StringBuilder();
						FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
						byte[] array = new MD5CryptoServiceProvider().ComputeHash(fileStream);
						fileStream.Close();
						int num = array.Length - 1;
						for (int i = 0; i <= num; i++)
						{
							stringBuilder.Append(array[i].ToString("x2"));
						}
						result = stringBuilder.ToString();
						break;
					}
					catch (Exception ex)
					{
						if (flag || ex is FileNotFoundException)
						{
							ModBase.Log(ex, "获取文件 MD5 失败：" + FilePath, ModBase.LogLevel.Debug, "出现错误");
							result = "";
							break;
						}
						flag = true;
						ModBase.Log(ex, "获取文件 MD5 可重试失败：" + FilePath, ModBase.LogLevel.Normal, "出现错误");
						Thread.Sleep(ModBase.RandomInteger(200, 500));
					}
				}
				return result;
			}
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0009BD9C File Offset: 0x00099F9C
		public static string GetFileSHA256(string FilePath)
		{
			bool flag = false;
			checked
			{
				string result;
				for (;;)
				{
					try
					{
						FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
						byte[] array = new SHA256CryptoServiceProvider().ComputeHash(fileStream);
						fileStream.Close();
						StringBuilder stringBuilder = new StringBuilder();
						int num = array.Length - 1;
						for (int i = 0; i <= num; i++)
						{
							stringBuilder.Append(array[i].ToString("x2"));
						}
						result = stringBuilder.ToString();
						break;
					}
					catch (Exception ex)
					{
						if (flag || ex is FileNotFoundException)
						{
							ModBase.Log(ex, "获取文件 SHA256 失败：" + FilePath, ModBase.LogLevel.Debug, "出现错误");
							result = "";
							break;
						}
						flag = true;
						ModBase.Log(ex, "获取文件 SHA256 可重试失败：" + FilePath, ModBase.LogLevel.Normal, "出现错误");
						Thread.Sleep(ModBase.RandomInteger(200, 500));
					}
				}
				return result;
			}
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0009BE94 File Offset: 0x0009A094
		public static string smethod_1(string FilePath)
		{
			bool flag = false;
			checked
			{
				string result;
				for (;;)
				{
					try
					{
						FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
						byte[] array = new SHA1CryptoServiceProvider().ComputeHash(fileStream);
						fileStream.Close();
						StringBuilder stringBuilder = new StringBuilder();
						int num = array.Length - 1;
						for (int i = 0; i <= num; i++)
						{
							stringBuilder.Append(array[i].ToString("x2"));
						}
						result = stringBuilder.ToString();
						break;
					}
					catch (Exception ex)
					{
						if (flag || ex is FileNotFoundException)
						{
							ModBase.Log(ex, "获取文件 SHA1 失败：" + FilePath, ModBase.LogLevel.Debug, "出现错误");
							result = "";
							break;
						}
						flag = true;
						ModBase.Log(ex, "获取文件 SHA1 可重试失败：" + FilePath, ModBase.LogLevel.Normal, "出现错误");
						Thread.Sleep(ModBase.RandomInteger(200, 500));
					}
				}
				return result;
			}
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x0009BF8C File Offset: 0x0009A18C
		public static string smethod_2(Stream Stream)
		{
			checked
			{
				string result;
				try
				{
					byte[] array = new SHA1CryptoServiceProvider().ComputeHash(Stream);
					StringBuilder stringBuilder = new StringBuilder();
					int num = array.Length - 1;
					for (int i = 0; i <= num; i++)
					{
						stringBuilder.Append(array[i].ToString("x2"));
					}
					result = stringBuilder.ToString();
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "获取流 SHA1 失败", ModBase.LogLevel.Debug, "出现错误");
					result = "";
				}
				return result;
			}
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x0009C01C File Offset: 0x0009A21C
		public static void ExtractFile(string CompressFilePath, string DestDirectory, Encoding Encode = null, Action<double> ProgressIncrementHandler = null)
		{
			Directory.CreateDirectory(DestDirectory);
			if (CompressFilePath.EndsWithF(".gz", true))
			{
				GZipStream gzipStream = new GZipStream(new FileStream(CompressFilePath, FileMode.Open, FileAccess.ReadWrite), 0);
				FileStream fileStream = new FileStream(DestDirectory + ModBase.GetFileNameFromPath(CompressFilePath).ToLower().Replace(".tar", "").Replace(".gz", ""), FileMode.OpenOrCreate, FileAccess.Write);
				for (int num = gzipStream.ReadByte(); num != -1; num = gzipStream.ReadByte())
				{
					fileStream.WriteByte(checked((byte)num));
				}
				fileStream.Close();
				gzipStream.Close();
				return;
			}
			using (ZipArchive zipArchive = ZipFile.Open(CompressFilePath, ZipArchiveMode.Read, Encode ?? Encoding.GetEncoding("GB18030")))
			{
				int count = zipArchive.Entries.Count;
				try
				{
					foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
					{
						if (ProgressIncrementHandler != null)
						{
							ProgressIncrementHandler(1.0 / (double)count);
						}
						string text = System.IO.Path.Combine(DestDirectory, zipArchiveEntry.FullName);
						if (!text.EndsWithF("\\", false) && !text.EndsWithF("/", false))
						{
							Directory.CreateDirectory(ModBase.GetPathFromFullPath(text));
							zipArchiveEntry.ExtractToFile(text, true);
						}
					}
				}
				finally
				{
					IEnumerator<ZipArchiveEntry> enumerator;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
			}
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x0009C180 File Offset: 0x0009A380
		public static int DeleteDirectory(string Path, bool IgnoreIssue = false)
		{
			checked
			{
				int result;
				if (!Directory.Exists(Path))
				{
					result = 0;
				}
				else
				{
					int num = 0;
					string[] files;
					try
					{
						files = Directory.GetFiles(Path);
					}
					catch (DirectoryNotFoundException ex)
					{
						ModBase.Log(ex, string.Format("疑似为孤立符号链接，尝试直接删除（{0}）", Path), ModBase.LogLevel.Developer, "出现错误");
						Directory.Delete(Path);
						return 0;
					}
					foreach (string text in files)
					{
						bool flag = false;
						for (;;)
						{
							try
							{
								File.Delete(text);
								num++;
								break;
							}
							catch (Exception ex2)
							{
								if (!flag)
								{
									flag = true;
									ModBase.Log(ex2, string.Format("删除文件失败，将在 0.3s 后重试（{0}）", text), ModBase.LogLevel.Debug, "出现错误");
									Thread.Sleep(300);
								}
								else
								{
									if (!IgnoreIssue)
									{
										throw;
									}
									ModBase.Log(ex2, "删除单个文件可忽略地失败", ModBase.LogLevel.Debug, "出现错误");
									break;
								}
							}
						}
					}
					string[] directories = Directory.GetDirectories(Path);
					for (int j = 0; j < directories.Length; j++)
					{
						ModBase.DeleteDirectory(directories[j], IgnoreIssue);
					}
					bool flag2 = false;
					for (;;)
					{
						try
						{
							Directory.Delete(Path, true);
							break;
						}
						catch (Exception ex3)
						{
							if (!flag2)
							{
								flag2 = true;
								ModBase.Log(ex3, string.Format("删除文件夹失败，将在 0.3s 后重试（{0}）", Path), ModBase.LogLevel.Debug, "出现错误");
								Thread.Sleep(300);
							}
							else
							{
								if (!IgnoreIssue)
								{
									throw;
								}
								ModBase.Log(ex3, "删除单个文件夹可忽略地失败", ModBase.LogLevel.Debug, "出现错误");
								break;
							}
						}
					}
					result = num;
				}
				return result;
			}
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0009C31C File Offset: 0x0009A51C
		public static void CopyDirectory(string FromPath, string ToPath, Action<double> ProgressIncrementHandler = null)
		{
			FromPath = FromPath.Replace("/", "\\");
			if (!FromPath.EndsWithF("\\", false))
			{
				FromPath += "\\";
			}
			ToPath = ToPath.Replace("/", "\\");
			if (!ToPath.EndsWithF("\\", false))
			{
				ToPath += "\\";
			}
			List<FileInfo> list = Enumerable.ToList<FileInfo>(ModBase.EnumerateFiles(FromPath));
			int count = list.Count;
			try
			{
				foreach (FileInfo fileInfo in list)
				{
					ModBase.CopyFile(fileInfo.FullName, fileInfo.FullName.Replace(FromPath, ToPath));
					if (ProgressIncrementHandler != null)
					{
						ProgressIncrementHandler(1.0 / (double)count);
					}
				}
			}
			finally
			{
				List<FileInfo>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0009C400 File Offset: 0x0009A600
		public static IEnumerable<FileInfo> EnumerateFiles(string Directory)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Directory);
			IEnumerable<FileInfo> result;
			if (!directoryInfo.Exists)
			{
				result = new List<FileInfo>();
			}
			else
			{
				result = directoryInfo.EnumerateFiles("*", SearchOption.AllDirectories);
			}
			return result;
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0009C434 File Offset: 0x0009A634
		public static string GetExceptionDetail(Exception Ex, bool ShowAllTrace = false)
		{
			ModBase._Closure$__75-0 CS$<>8__locals1 = new ModBase._Closure$__75-0(CS$<>8__locals1);
			string result;
			if (Ex == null)
			{
				result = "无可用错误信息！";
			}
			else
			{
				Exception ex = Ex;
				while (ex.InnerException != null)
				{
					ex = ex.InnerException;
				}
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				while (Ex != null)
				{
					list.Add(Ex.Message.Replace("\r\n", "\r").Replace("\n", "\r").Replace("\r\r", "\r").Replace("\r", "\r\n"));
					if (Ex.StackTrace != null)
					{
						foreach (string text in Ex.StackTrace.Split("\r\n"))
						{
							if (ShowAllTrace || text.ToLower().Contains("pcl"))
							{
								list2.Add(text.Replace("\r", string.Empty).Replace("\n", string.Empty));
							}
						}
					}
					Ex = Ex.InnerException;
				}
				list = Enumerable.ToList<string>(Enumerable.Distinct<string>(list));
				CS$<>8__locals1.$VB$Local_Desc = list.Join("\r\n→ ");
				string text2 = Enumerable.Any<string>(list2) ? ("\r\n" + list2.Join("\r\n")) : "";
				string text3 = null;
				if (!(ex is TypeLoadException) && !(ex is BadImageFormatException) && !(ex is MissingMethodException) && !(ex is NotImplementedException) && !(ex is TypeInitializationException))
				{
					if (ex is UnauthorizedAccessException)
					{
						text3 = "PCL 的权限不足。请尝试右键 PCL，选择以管理员身份运行。";
					}
					else if (ex is OutOfMemoryException)
					{
						text3 = "你的电脑运行内存不足，导致 PCL 无法继续运行。请在关闭一部分不需要的程序后再试。";
					}
					else if (ex is COMException)
					{
						text3 = "由于操作系统或显卡存在问题，导致出现错误。请尝试重启 PCL。";
					}
					else if (Enumerable.Any<string>(new string[]
					{
						"远程主机强迫关闭了",
						"远程方已关闭传输流",
						"未能解析此远程名称",
						"由于目标计算机积极拒绝",
						"操作已超时",
						"操作超时",
						"服务器超时",
						"连接超时"
					}, (string s) => CS$<>8__locals1.$VB$Local_Desc.Contains(s)))
					{
						text3 = "你的网络环境不佳，导致难以连接到服务器。请检查网络，多重试几次，或尝试使用 VPN。";
					}
				}
				else
				{
					text3 = "PCL 的运行环境存在问题。请尝试重新安装 .NET Framework 4.6.2 然后再试。若无法安装，请先卸载较新版本的 .NET Framework，然后再尝试安装。";
				}
				string text4 = (Operators.CompareString(ex.GetType().FullName, "System.Exception", false) == 0) ? "" : ("\r\n错误类型：" + ex.GetType().FullName);
				if (text3 == null)
				{
					result = CS$<>8__locals1.$VB$Local_Desc + text2 + text4;
				}
				else
				{
					string str = text3 + "\r\n" + Enumerable.First<string>(list) + "\r\n————————————\r\n";
					list[0] = "详细错误信息：";
					result = str + list.Join("\r\n→ ") + text2 + text4;
				}
			}
			return result;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0009C6E8 File Offset: 0x0009A8E8
		public static string GetExceptionSummary(Exception Ex)
		{
			ModBase._Closure$__76-0 CS$<>8__locals1 = new ModBase._Closure$__76-0(CS$<>8__locals1);
			string result;
			if (Ex == null)
			{
				result = "无可用错误信息！";
			}
			else
			{
				Exception ex = Ex;
				while (ex.InnerException != null)
				{
					ex = ex.InnerException;
				}
				List<string> list = new List<string>();
				while (Ex != null)
				{
					list.Add(Ex.Message.Replace("\r\n", "\r").Replace("\n", "\r").Replace("\r\r", "\r").Replace("\r", " "));
					Ex = Ex.InnerException;
				}
				list = Enumerable.ToList<string>(Enumerable.Distinct<string>(list));
				CS$<>8__locals1.$VB$Local_Desc = list.Join("\r\n→ ");
				string text = null;
				if (!(ex is TypeLoadException) && !(ex is BadImageFormatException) && !(ex is MissingMethodException) && !(ex is NotImplementedException) && !(ex is TypeInitializationException))
				{
					if (ex is UnauthorizedAccessException)
					{
						text = "PCL 的权限不足。请尝试右键 PCL，选择以管理员身份运行。";
					}
					else if (ex is OutOfMemoryException)
					{
						text = "你的电脑运行内存不足，导致 PCL 无法继续运行。请在关闭一部分不需要的程序后再试。";
					}
					else if (ex is COMException)
					{
						text = "由于操作系统或显卡存在问题，导致出现错误。请尝试重启 PCL。";
					}
					else if (Enumerable.Any<string>(new string[]
					{
						"远程主机强迫关闭了",
						"远程方已关闭传输流",
						"未能解析此远程名称",
						"由于目标计算机积极拒绝",
						"操作已超时",
						"操作超时",
						"服务器超时",
						"连接超时"
					}, (string s) => CS$<>8__locals1.$VB$Local_Desc.Contains(s)))
					{
						text = "你的网络环境不佳，导致难以连接到服务器。请检查网络，多重试几次，或尝试使用 VPN。";
					}
				}
				else
				{
					text = "PCL 的运行环境存在问题。请尝试重新安装 .NET Framework 4.6.2 然后再试。若无法安装，请先卸载较新版本的 .NET Framework，然后再尝试安装。";
				}
				if (text != null)
				{
					result = text + "详细错误：" + Enumerable.First<string>(list);
				}
				else
				{
					list.Reverse();
					result = list.Join(" → ");
				}
			}
			return result;
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0000D4B2 File Offset: 0x0000B6B2
		public static string GetStringFromEnum(Enum EnumData)
		{
			return Enum.GetName(EnumData.GetType(), EnumData);
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0009C89C File Offset: 0x0009AA9C
		public static string GetString(long FileSize)
		{
			checked
			{
				bool flag;
				if (flag = (FileSize < 0L))
				{
					FileSize *= -1L;
				}
				string result;
				if (FileSize < 1000L)
				{
					result = (flag ? "-" : "") + Conversions.ToString(FileSize) + " B";
				}
				else if (FileSize < 1024000L)
				{
					string text = Conversions.ToString(Math.Round((double)FileSize / 1024.0));
					result = (flag ? "-" : "") + Conversions.ToString(Math.Round((double)FileSize / 1024.0, (int)Math.Round(ModBase.MathClamp((double)(3 - text.Length), 0.0, 2.0)))) + " K";
				}
				else if (FileSize < 1048576000L)
				{
					string text2 = Conversions.ToString(Math.Round((double)FileSize / 1024.0 / 1024.0));
					result = (flag ? "-" : "") + Conversions.ToString(Math.Round((double)FileSize / 1024.0 / 1024.0, (int)Math.Round(ModBase.MathClamp((double)(3 - text2.Length), 0.0, 2.0)))) + " M";
				}
				else
				{
					string text3 = Conversions.ToString(Math.Round((double)FileSize / 1024.0 / 1024.0 / 1024.0));
					result = (flag ? "-" : "") + Conversions.ToString(Math.Round((double)FileSize / 1024.0 / 1024.0 / 1024.0, (int)Math.Round(ModBase.MathClamp((double)(3 - text3.Length), 0.0, 2.0)))) + " G";
				}
				return result;
			}
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x0009CAA4 File Offset: 0x0009ACA4
		public static object GetJson(string Data)
		{
			object result;
			try
			{
				result = JsonConvert.DeserializeObject(Data, new JsonSerializerSettings
				{
					DateTimeZoneHandling = 0
				});
			}
			catch (Exception ex)
			{
				int length = (Data ?? "").Length;
				throw new Exception("格式化 JSON 失败：" + ((length > 2000) ? (Data.Substring(0, 500) + string.Format("...(全长 {0} 个字符)...", length) + Strings.Right(Data, 500)) : Data));
			}
			return result;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x0009CB38 File Offset: 0x0009AD38
		public static string Capitalize(this string word)
		{
			string result;
			if (string.IsNullOrEmpty(word))
			{
				result = word;
			}
			else
			{
				result = word.Substring(0, 1).ToUpperInvariant() + word.Substring(1).ToLowerInvariant();
			}
			return result;
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x0009CB74 File Offset: 0x0009AD74
		public static string StrFill(string Str, string Code, byte Length)
		{
			string result;
			if (Str.Length > (int)Length)
			{
				result = Strings.Mid(Str, 1, (int)Length);
			}
			else
			{
				result = Strings.Mid(Str.PadRight((int)Length, Conversions.ToChar(Code)), checked(Str.Length + 1)) + Str;
			}
			return result;
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x0009CBB8 File Offset: 0x0009ADB8
		public static string StrFillNum(double Num, int Length)
		{
			Num = Math.Round(Num, Length, MidpointRounding.AwayFromZero);
			string text = Conversions.ToString(Num);
			checked
			{
				if (!text.Contains("."))
				{
					text = (text + ".").PadRight(text.Length + 1 + Length, '0');
				}
				else
				{
					text = text.PadRight(text.Split(".")[0].Length + 1 + Length, '0');
				}
				return text;
			}
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x0009CC24 File Offset: 0x0009AE24
		public static object StrTrim(string Str, bool RemoveQuote = true)
		{
			if (RemoveQuote)
			{
				Str = Str.Split("（")[0].Split("：")[0].Split("(")[0].Split(":")[0];
			}
			return Str.Trim(new char[]
			{
				'.',
				'。',
				'！',
				' ',
				'!',
				'?',
				'？',
				'\r',
				'\n'
			});
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0009CC80 File Offset: 0x0009AE80
		public static string Join(this IEnumerable List, string Split)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			try
			{
				foreach (object obj in List)
				{
					object objectValue = RuntimeHelpers.GetObjectValue(obj);
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(Split);
					}
					if (objectValue != null)
					{
						stringBuilder.Append(RuntimeHelpers.GetObjectValue(objectValue));
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
			return stringBuilder.ToString();
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x0009CD00 File Offset: 0x0009AF00
		public static string[] Split(this string FullStr, string SplitStr)
		{
			string[] result;
			if (SplitStr.Length == 1)
			{
				result = FullStr.Split(new char[]
				{
					SplitStr[0]
				});
			}
			else
			{
				result = FullStr.Split(new string[]
				{
					SplitStr
				}, StringSplitOptions.None);
			}
			return result;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x0009CD44 File Offset: 0x0009AF44
		public static ulong GetHash(string Str)
		{
			ulong num = 5381UL;
			checked
			{
				int num2 = Str.Length - 1;
				for (int i = 0; i <= num2; i++)
				{
					num = (num << 5 ^ num ^ (ulong)Str[i]);
				}
				return num ^ 12218072394304324399UL;
			}
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0009CD8C File Offset: 0x0009AF8C
		public static string GetStringMD5(string Str)
		{
			byte[] array = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(Str));
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0000D4C0 File Offset: 0x0000B6C0
		public static bool IsASCII(this string Input)
		{
			return Enumerable.All<char>(Input, (ModBase._Closure$__.$I88-0 == null) ? (ModBase._Closure$__.$I88-0 = ((char c) => c < '\u0080')) : ModBase._Closure$__.$I88-0);
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x0009CDE8 File Offset: 0x0009AFE8
		public static string BeforeFirst(this string Str, string Text, bool IgnoreCase = false)
		{
			int num = string.IsNullOrEmpty(Text) ? -1 : Str.IndexOfF(Text, IgnoreCase);
			string result;
			if (num >= 0)
			{
				result = Str.Substring(0, num);
			}
			else
			{
				result = Str;
			}
			return result;
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0009CE1C File Offset: 0x0009B01C
		public static string BeforeLast(this string Str, string Text, bool IgnoreCase = false)
		{
			int num = string.IsNullOrEmpty(Text) ? -1 : Str.LastIndexOfF(Text, IgnoreCase);
			string result;
			if (num >= 0)
			{
				result = Str.Substring(0, num);
			}
			else
			{
				result = Str;
			}
			return result;
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x0009CE50 File Offset: 0x0009B050
		public static string AfterFirst(this string Str, string Text, bool IgnoreCase = false)
		{
			int num = string.IsNullOrEmpty(Text) ? -1 : Str.IndexOfF(Text, IgnoreCase);
			string result;
			if (num >= 0)
			{
				result = Str.Substring(checked(num + Text.Length));
			}
			else
			{
				result = Str;
			}
			return result;
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x0009CE8C File Offset: 0x0009B08C
		public static string AfterLast(this string Str, string Text, bool IgnoreCase = false)
		{
			int num = string.IsNullOrEmpty(Text) ? -1 : Str.LastIndexOfF(Text, IgnoreCase);
			string result;
			if (num >= 0)
			{
				result = Str.Substring(checked(num + Text.Length));
			}
			else
			{
				result = Str;
			}
			return result;
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0009CEC8 File Offset: 0x0009B0C8
		public static string Between(this string Str, string After, string Before, bool IgnoreCase = false)
		{
			int num = string.IsNullOrEmpty(After) ? -1 : Str.LastIndexOfF(After, IgnoreCase);
			checked
			{
				if (num >= 0)
				{
					num += After.Length;
				}
				else
				{
					num = 0;
				}
				int num2 = string.IsNullOrEmpty(Before) ? -1 : Str.IndexOfF(Before, num, IgnoreCase);
				string result;
				if (num2 >= 0)
				{
					result = Str.Substring(num, num2 - num);
				}
				else if (num > 0)
				{
					result = Str.Substring(num);
				}
				else
				{
					result = Str;
				}
				return result;
			}
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0000D4EC File Offset: 0x0000B6EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool StartsWithF(this string Str, string Prefix, bool IgnoreCase = false)
		{
			return Str.StartsWith(Prefix, IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x0000D4FC File Offset: 0x0000B6FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EndsWithF(this string Str, string Suffix, bool IgnoreCase = false)
		{
			return Str.EndsWith(Suffix, IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x0000D50C File Offset: 0x0000B70C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool ContainsF(this string Str, string SubStr, bool IgnoreCase = false)
		{
			return Str.IndexOf(SubStr, IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) >= 0;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0000D522 File Offset: 0x0000B722
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOfF(this string Str, string SubStr, bool IgnoreCase = false)
		{
			return Str.IndexOf(SubStr, IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0000D532 File Offset: 0x0000B732
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOfF(this string Str, string SubStr, int StartIndex, bool IgnoreCase = false)
		{
			return Str.IndexOf(SubStr, StartIndex, IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x0000D543 File Offset: 0x0000B743
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LastIndexOfF(this string Str, string SubStr, bool IgnoreCase = false)
		{
			return Str.LastIndexOf(SubStr, IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x0000D553 File Offset: 0x0000B753
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LastIndexOfF(this string Str, string SubStr, int StartIndex, bool IgnoreCase = false)
		{
			return Str.LastIndexOf(SubStr, StartIndex, IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0009CF34 File Offset: 0x0009B134
		public static double Val(object Str)
		{
			double result;
			try
			{
				result = ((!(Str is string) || !Operators.ConditionalCompareObjectEqual(Str, "&", false)) ? Conversion.Val(RuntimeHelpers.GetObjectValue(Str)) : 0.0);
			}
			catch (Exception ex)
			{
				result = 0.0;
			}
			return result;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0009CF98 File Offset: 0x0009B198
		public static string smethod_3(string Str)
		{
			if (Str.StartsWithF("{", false))
			{
				Str = "{}" + Str;
			}
			return Str.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&apos;").Replace("\"", "&quot;").Replace("\r\n", "&#xa;");
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0009D01C File Offset: 0x0009B21C
		public static List<string> RegexSearch(string str, string regex, RegexOptions options = 0)
		{
			List<string> list;
			try
			{
				list = new List<string>();
				MatchCollection matchCollection = new Regex(regex, options).Matches(str);
				if (matchCollection == null)
				{
					list = list;
				}
				else
				{
					try
					{
						foreach (object obj in matchCollection)
						{
							Match match = (Match)obj;
							list.Add(match.Value);
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
			catch (Exception ex)
			{
				ModBase.Log(ex, "正则匹配全部项出错", ModBase.LogLevel.Debug, "出现错误");
				list = new List<string>();
			}
			return list;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0009D0CC File Offset: 0x0009B2CC
		public static string RegexSeek(string str, string regex, RegexOptions options = 0)
		{
			string result;
			try
			{
				string value = Regex.Match(str, regex, options).Value;
				result = ((Operators.CompareString(value, "", false) == 0) ? null : value);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "正则匹配第一项出错", ModBase.LogLevel.Debug, "出现错误");
				result = null;
			}
			return result;
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x0009D130 File Offset: 0x0009B330
		public static bool RegexCheck(string str, string regex, RegexOptions options = 0)
		{
			bool result;
			try
			{
				result = Regex.IsMatch(str, regex, options);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "正则检查出错", ModBase.LogLevel.Debug, "出现错误");
				result = false;
			}
			return result;
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x0000D564 File Offset: 0x0000B764
		public static string RegexReplace(string Input, string Replacement, string Regex, RegexOptions options = 0)
		{
			return Regex.Replace(Input, Regex, Replacement, options);
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x0000D56F File Offset: 0x0000B76F
		public static string RegexReplaceEach(string Input, MatchEvaluator Replacement, string Regex, RegexOptions options = 0)
		{
			return Regex.Replace(Input, Regex, Replacement, options);
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x0009D17C File Offset: 0x0009B37C
		private static double SearchSimilarity(string Source, string Query)
		{
			int i = 0;
			double num = 0.0;
			Source = Source.ToLower().Replace(" ", "");
			Query = Query.ToLower().Replace(" ", "");
			int length = Source.Length;
			int length2 = Query.Length;
			checked
			{
				while (i < length2)
				{
					int j = 0;
					int num2 = 0;
					int num3 = 0;
					while (j < Source.Length)
					{
						int num4 = 0;
						while (i + num4 < length2 && j + num4 < Source.Length)
						{
							if (Source[j + num4] != Query[i + num4])
							{
								break;
							}
							num4++;
						}
						if (num4 > num2)
						{
							num2 = num4;
							num3 = j;
						}
						j += Math.Max(1, num4);
					}
					if (num2 > 0)
					{
						Source = Source.Substring(0, num3) + ((Enumerable.Count<char>(Source) > num3 + num2) ? Source.Substring(num3 + num2) : string.Empty);
						unchecked
						{
							double num5 = Math.Pow(1.4, (double)(checked(3 + num2))) - 3.6;
							num5 *= 1.0 + 0.3 * (double)Math.Max(0, checked(3 - Math.Abs(i - num3)));
							num += num5;
						}
					}
					i += Math.Max(1, num2);
				}
			}
			return num / (double)length2 * (3.0 / Math.Pow((double)(checked(length + 15)), 0.5)) * (double)((length2 <= 2) ? checked(3 - length2) : 1);
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x0009D304 File Offset: 0x0009B504
		private static double SearchSimilarityWeighted(List<KeyValuePair<string, double>> Source, string Query)
		{
			double num = 0.0;
			double num2 = 0.0;
			try
			{
				foreach (KeyValuePair<string, double> keyValuePair in Source)
				{
					num2 += ModBase.SearchSimilarity(keyValuePair.Key, Query) * keyValuePair.Value;
					num += keyValuePair.Value;
				}
			}
			finally
			{
				List<KeyValuePair<string, double>>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			return num2 / num;
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x0009D384 File Offset: 0x0009B584
		public static List<ModBase.SearchEntry<T>> Search<T>(List<ModBase.SearchEntry<T>> Entries, string Query, int MaxBlurCount = 5, double MinBlurSimilarity = 0.1)
		{
			List<ModBase.SearchEntry<T>> list = new List<ModBase.SearchEntry<!!0>>();
			checked
			{
				List<ModBase.SearchEntry<T>> result;
				if (!Enumerable.Any<ModBase.SearchEntry<T>>(Entries))
				{
					result = list;
				}
				else
				{
					try
					{
						List<ModBase.SearchEntry<T>>.Enumerator enumerator = Entries.GetEnumerator();
						while (enumerator.MoveNext())
						{
							ModBase._Closure$__111-0<T> CS$<>8__locals1 = new ModBase._Closure$__111-0<!!0>(CS$<>8__locals1);
							CS$<>8__locals1.$VB$Local_Entry = enumerator.Current;
							CS$<>8__locals1.$VB$Local_Entry.m_IssuerError = ModBase.SearchSimilarityWeighted(CS$<>8__locals1.$VB$Local_Entry.helperError, Query);
							CS$<>8__locals1.$VB$Local_Entry.indexerError = Enumerable.All<string>(Query.Split(" "), delegate(string QueryPart)
							{
								ModBase._Closure$__111-1<$CLS0> CS$<>8__locals2 = new ModBase._Closure$__111-1<!0>(CS$<>8__locals2);
								CS$<>8__locals2.$VB$Local_QueryPart = QueryPart;
								return Enumerable.Any<KeyValuePair<string, double>>(CS$<>8__locals1.$VB$Local_Entry.helperError, (KeyValuePair<string, double> Source) => Source.Key.Replace(" ", "").ContainsF(CS$<>8__locals2.$VB$Local_QueryPart, true));
							});
						}
					}
					finally
					{
						List<ModBase.SearchEntry<T>>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					Entries = Entries.Sort((ModBase._Closure$__111<!!0>.$I111-2 == null) ? (ModBase._Closure$__111<!!0>.$I111-2 = delegate(ModBase.SearchEntry<$CLS0> Left, ModBase.SearchEntry<$CLS0> Right)
					{
						bool result2;
						if (Left.indexerError ^ Right.indexerError)
						{
							result2 = Left.indexerError;
						}
						else
						{
							result2 = (Left.m_IssuerError > Right.m_IssuerError);
						}
						return result2;
					}) : ModBase._Closure$__111<!!0>.$I111-2);
					int num = 0;
					try
					{
						foreach (ModBase.SearchEntry<T> searchEntry in Entries)
						{
							if (searchEntry.indexerError)
							{
								list.Add(searchEntry);
							}
							else
							{
								if (searchEntry.m_IssuerError < MinBlurSimilarity)
								{
									break;
								}
								if (num == MaxBlurCount)
								{
									break;
								}
								list.Add(searchEntry);
								num++;
							}
						}
					}
					finally
					{
						List<ModBase.SearchEntry<T>>.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
					result = list;
				}
				return result;
			}
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x0009D4D8 File Offset: 0x0009B6D8
		private static string GetPureASCIIDir()
		{
			string result;
			if ((ModBase.Path + "PCL").IsASCII())
			{
				result = ModBase.Path + "PCL\\";
			}
			else if (ModBase.m_InstanceRepository.IsASCII())
			{
				result = ModBase.m_InstanceRepository;
			}
			else if (ModBase.m_DecoratorRepository.IsASCII())
			{
				result = ModBase.m_DecoratorRepository;
			}
			else
			{
				result = ModBase._RefRepository + "ProgramData\\PCL\\";
			}
			return result;
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x0009D548 File Offset: 0x0009B748
		public static bool IsAdmin()
		{
			WindowsIdentity current = WindowsIdentity.GetCurrent();
			return new WindowsPrincipal(current).IsInRole(WindowsBuiltInRole.Administrator);
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x0000D57A File Offset: 0x0000B77A
		public static int RunAsAdmin(string Argument)
		{
			Process process = Process.Start(new ProcessStartInfo(ModBase.interpreterRepository)
			{
				Verb = "runas",
				Arguments = Argument
			});
			process.WaitForExit();
			return process.ExitCode;
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		public static bool IsSystemLanguageChinese()
		{
			return Operators.CompareString(CultureInfo.CurrentCulture.TwoLetterISOLanguageName, "zh", false) == 0 || Operators.CompareString(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "zh", false) == 0;
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x0009D56C File Offset: 0x0009B76C
		public static int GetUuid()
		{
			if (ModBase.m_ConsumerRepository == null)
			{
				ModBase.m_ConsumerRepository = RuntimeHelpers.GetObjectValue(new object());
			}
			object consumerRepository = ModBase.m_ConsumerRepository;
			ObjectFlowControl.CheckForSyncLockOnValueType(consumerRepository);
			checked
			{
				int result;
				lock (consumerRepository)
				{
					ModBase.taskRepository++;
					result = ModBase.taskRepository;
				}
				return result;
			}
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0009D5D4 File Offset: 0x0009B7D4
		public static List<T> GetFullList<T>(IList data)
		{
			List<T> list = new List<!!0>();
			checked
			{
				int num = data.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					if (data[i] is ICollection)
					{
						list.AddRange((IEnumerable<!!0>)data[i]);
					}
					else
					{
						list.Add(Conversions.ToGenericParameter<T>(data[i]));
					}
				}
				return list;
			}
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0009D634 File Offset: 0x0009B834
		public static List<T> Distinct<T>(this ICollection<T> Arr, ModBase.CompareThreadStart<T> IsEqual)
		{
			List<T> list = new List<!!0>();
			checked
			{
				int num = Arr.Count - 1;
				int i = 0;
				IL_56:
				while (i <= num)
				{
					int num2 = i + 1;
					int num3 = Arr.Count - 1;
					for (int j = num2; j <= num3; j++)
					{
						if (IsEqual(Enumerable.ElementAtOrDefault<T>(Arr, i), Enumerable.ElementAtOrDefault<T>(Arr, j)))
						{
							IL_52:
							i++;
							goto IL_56;
						}
					}
					list.Add(Enumerable.ElementAtOrDefault<T>(Arr, i));
					goto IL_52;
				}
				return list;
			}
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x0009D69C File Offset: 0x0009B89C
		public static string GetTimeNow()
		{
			return DateTime.Now.ToString("HH':'mm':'ss'.'fff");
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x0000D5DB File Offset: 0x0000B7DB
		public static long GetTimeTick()
		{
			return checked(unchecked((long)MyWpfExtension.ManageParser().Clock.TickCount) + 2147483651L);
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x0009D6BC File Offset: 0x0009B8BC
		public static string GetTimeSpanString(TimeSpan Span, bool IsShortForm)
		{
			string str = (Span.TotalMilliseconds > 0.0) ? "后" : "前";
			if (Span.TotalMilliseconds < 0.0)
			{
				Span = -Span;
			}
			double num = Math.Floor((double)Span.Days / 30.0);
			string str2;
			if (IsShortForm)
			{
				if (num >= 12.0)
				{
					str2 = Conversions.ToString(Math.Floor(num / 12.0)) + " 年";
				}
				else if (num >= 2.0)
				{
					str2 = Conversions.ToString(num) + " 个月";
				}
				else if (Span.TotalDays >= 2.0)
				{
					str2 = Conversions.ToString(Span.Days) + " 天";
				}
				else if (Span.TotalHours >= 1.0)
				{
					str2 = Conversions.ToString(Span.Hours) + " 小时";
				}
				else if (Span.TotalMinutes >= 1.0)
				{
					str2 = Conversions.ToString(Span.Minutes) + " 分钟";
				}
				else if (Span.TotalSeconds >= 1.0)
				{
					str2 = Conversions.ToString(Span.Seconds) + " 秒";
				}
				else
				{
					str2 = "1 秒";
				}
			}
			else if (num >= 61.0)
			{
				str2 = Conversions.ToString(Math.Floor(num / 12.0)) + " 年";
			}
			else if (num >= 12.0)
			{
				str2 = Conversions.ToString(Math.Floor(num / 12.0)) + " 年" + ((num % 12.0 > 0.0) ? (" " + Conversions.ToString(num % 12.0) + " 个月") : "");
			}
			else if (num >= 4.0)
			{
				str2 = Conversions.ToString(num) + " 个月";
			}
			else if (num >= 1.0)
			{
				str2 = Conversions.ToString(num) + " 月" + ((Span.Days % 30 > 0) ? (" " + Conversions.ToString(Span.Days % 30) + " 天") : "");
			}
			else if (Span.TotalDays >= 4.0)
			{
				str2 = Conversions.ToString(Span.Days) + " 天";
			}
			else if (Span.TotalDays >= 1.0)
			{
				str2 = Conversions.ToString(Span.Days) + " 天" + ((Span.Hours > 0) ? (" " + Conversions.ToString(Span.Hours) + " 小时") : "");
			}
			else if (Span.TotalHours >= 10.0)
			{
				str2 = Conversions.ToString(Span.Hours) + " 小时";
			}
			else if (Span.TotalHours >= 1.0)
			{
				str2 = Conversions.ToString(Span.Hours) + " 小时" + ((Span.Minutes > 0) ? (" " + Conversions.ToString(Span.Minutes) + " 分钟") : "");
			}
			else if (Span.TotalMinutes >= 10.0)
			{
				str2 = Conversions.ToString(Span.Minutes) + " 分钟";
			}
			else if (Span.TotalMinutes >= 1.0)
			{
				str2 = Conversions.ToString(Span.Minutes) + " 分" + ((Span.Seconds > 0) ? (" " + Conversions.ToString(Span.Seconds) + " 秒") : "");
			}
			else if (Span.TotalSeconds >= 1.0)
			{
				str2 = Conversions.ToString(Span.Seconds) + " 秒";
			}
			else
			{
				str2 = "1 秒";
			}
			return str2 + str;
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x0009DB1C File Offset: 0x0009BD1C
		public static long GetUnixTimestamp()
		{
			return checked((long)Math.Round((double)(DateAndTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000000.0));
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0009DB5C File Offset: 0x0009BD5C
		public static void ShellOnly(string FileName, string Arguments = "")
		{
			using (Process process = new Process())
			{
				process.StartInfo.Arguments = Arguments;
				process.StartInfo.FileName = FileName;
				ModBase.Log("[System] 执行外部命令：" + FileName + " " + Arguments, ModBase.LogLevel.Normal, "出现错误");
				process.Start();
			}
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x0009DBC8 File Offset: 0x0009BDC8
		public static ModBase.Result ShellAndGetExitCode(string FileName, string Arguments = "", int Timeout = 1000000)
		{
			ModBase.Result result;
			try
			{
				using (Process process = new Process())
				{
					process.StartInfo.Arguments = Arguments;
					process.StartInfo.FileName = FileName;
					ModBase.Log("[System] 执行外部命令并等待返回码：" + FileName + " " + Arguments, ModBase.LogLevel.Normal, "出现错误");
					process.Start();
					if (process.WaitForExit(Timeout))
					{
						result = (ModBase.Result)process.ExitCode;
					}
					else
					{
						result = ModBase.Result.Timeout;
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "执行命令失败：" + FileName, ModBase.LogLevel.Msgbox, "出现错误");
				result = ModBase.Result.Fail;
			}
			return result;
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0009DC7C File Offset: 0x0009BE7C
		public static string ShellAndGetOutput(string FileName, string Arguments = "", int Timeout = 1000000, string WorkingDirectory = null)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo
			{
				Arguments = Arguments,
				FileName = FileName,
				UseShellExecute = false,
				CreateNoWindow = true,
				RedirectStandardError = true,
				RedirectStandardOutput = true,
				WorkingDirectory = (WorkingDirectory ?? ModBase.Path.TrimEnd(new char[]
				{
					'\\'
				}))
			};
			if (WorkingDirectory != null)
			{
				if (processStartInfo.EnvironmentVariables.ContainsKey("appdata"))
				{
					processStartInfo.EnvironmentVariables["appdata"] = WorkingDirectory;
				}
				else
				{
					processStartInfo.EnvironmentVariables.Add("appdata", WorkingDirectory);
				}
			}
			ModBase.Log("[System] 执行外部命令并等待返回结果：" + FileName + " " + Arguments, ModBase.LogLevel.Normal, "出现错误");
			string result;
			using (Process process = new Process
			{
				StartInfo = processStartInfo
			})
			{
				process.Start();
				string text = process.StandardOutput.ReadToEnd() + process.StandardError.ReadToEnd();
				process.WaitForExit(Timeout);
				if (!process.HasExited)
				{
					process.Kill();
				}
				result = text;
			}
			return result;
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x0009DD94 File Offset: 0x0009BF94
		public static Thread RunInNewThread(Action Action, string Name = null, ThreadPriority Priority = ThreadPriority.Normal)
		{
			Thread thread = new Thread(delegate()
			{
				try
				{
					Action();
				}
				catch (ThreadInterruptedException ex)
				{
					ModBase.Log(Name + "：线程已中止", ModBase.LogLevel.Normal, "出现错误");
				}
				catch (Exception ex2)
				{
					ModBase.Log(ex2, Name + "：线程执行失败", ModBase.LogLevel.Feedback, "出现错误");
				}
			});
			thread.Name = (Name ?? ("Runtime New Invoke " + Conversions.ToString(ModBase.GetUuid()) + "#"));
			thread.Priority = Priority;
			thread.Start();
			return thread;
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x0009DDFC File Offset: 0x0009BFFC
		public static Output RunInUiWait<Output>(Func<Output> Action)
		{
			Output result;
			if (ModBase.RunInUi())
			{
				result = Action();
			}
			else
			{
				result = System.Windows.Application.Current.Dispatcher.Invoke<Output>(Action);
			}
			return result;
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x0000D5F7 File Offset: 0x0000B7F7
		public static void RunInUiWait(Action Action)
		{
			if (ModBase.RunInUi())
			{
				Action();
				return;
			}
			System.Windows.Application.Current.Dispatcher.Invoke(Action);
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x0000D617 File Offset: 0x0000B817
		public static void RunInUi(Action Action, bool ForceWaitUntilLoaded = false)
		{
			if (ForceWaitUntilLoaded)
			{
				System.Windows.Application.Current.Dispatcher.InvokeAsync(Action, DispatcherPriority.Loaded);
				return;
			}
			if (ModBase.RunInUi())
			{
				Action();
				return;
			}
			System.Windows.Application.Current.Dispatcher.InvokeAsync(Action);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x0000D64E File Offset: 0x0000B84E
		public static void RunInThread(Action Action)
		{
			if (ModBase.RunInUi())
			{
				ModBase.RunInNewThread(Action, "Runtime Invoke " + Conversions.ToString(ModBase.GetUuid()) + "#", ThreadPriority.Normal);
				return;
			}
			Action();
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x0009DE2C File Offset: 0x0009C02C
		public static List<T> Sort<T>(this IList<T> List, ModBase.CompareThreadStart<T> SortRule)
		{
			List<T> list = new List<!!0>();
			checked
			{
				while (Enumerable.Any<T>(List))
				{
					T t = List[0];
					int num = List.Count - 1;
					for (int i = 1; i <= num; i++)
					{
						if (SortRule(List[i], t))
						{
							t = List[i];
						}
					}
					List.Remove(t);
					list.Add(t);
				}
				return list;
			}
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0000D67F File Offset: 0x0000B87F
		public static IList<T> Clone<T>(this IList<T> list)
		{
			return new List<!!0>(list);
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x0009DE90 File Offset: 0x0009C090
		public static object GetProgramArgument(string Name, object DefaultValue = "")
		{
			string[] array = Interaction.Command().Split(" ");
			checked
			{
				int num = array.Length - 1;
				for (int i = 0; i <= num; i++)
				{
					if (Operators.CompareString(array[i], "-" + Name, false) == 0)
					{
						object result;
						if (array.Length != i + 1 && !array[i + 1].StartsWithF("-", false))
						{
							result = array[i + 1];
						}
						else
						{
							result = true;
						}
						return result;
					}
				}
				return DefaultValue;
			}
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0009DF04 File Offset: 0x0009C104
		public static DateTime GetDate(int timeStamp)
		{
			DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
			long ticks = checked(unchecked((long)timeStamp) * 10000000L);
			return dateTime.Add(new TimeSpan(ticks));
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0009DF48 File Offset: 0x0009C148
		public static DateTime GetLocalTime(DateTime UtcDate)
		{
			return DateTime.SpecifyKind(UtcDate, DateTimeKind.Utc).ToLocalTime();
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x0009DF64 File Offset: 0x0009C164
		public static void OpenWebsite(string Url)
		{
			try
			{
				if (!Url.StartsWithF("http", true) && !Url.StartsWithF("minecraft://", true))
				{
					throw new Exception(Url + " 不是一个有效的网址，它必须以 http 开头！");
				}
				ModBase.Log("[System] 正在打开网页：" + Url, ModBase.LogLevel.Normal, "出现错误");
				Process.Start(Url);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "无法打开网页（" + Url + "）", ModBase.LogLevel.Debug, "出现错误");
				ModBase.ClipboardSet(Url, false);
				ModMain.MyMsgBox("可能由于浏览器未正确配置，PCL 无法为你打开网页。\r\n网址已经复制到剪贴板，若有需要可以手动粘贴访问。\r\n" + string.Format("网址：{0}", Url), "无法打开网页", "确定", "", "", false, true, false, null, null, null);
			}
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x0009E034 File Offset: 0x0009C234
		public static void OpenExplorer(string Argument)
		{
			try
			{
				ModBase.ShellOnly("explorer.exe", Argument);
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "打开资源管理器失败，请尝试关闭安全软件（如 360 安全卫士）", ModBase.LogLevel.Msgbox, "出现错误");
			}
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x0009E080 File Offset: 0x0009C280
		public static void ClipboardSet(string Text, bool ShowSuccessHint = true)
		{
			ModBase._Closure$__146-0 CS$<>8__locals1 = new ModBase._Closure$__146-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_Text = Text;
			CS$<>8__locals1.$VB$Local_ShowSuccessHint = ShowSuccessHint;
			ModBase.RunInThread(checked(delegate
			{
				int num = 0;
				for (;;)
				{
					try
					{
						ModBase.RunInUi((CS$<>8__locals1.$I1 == null) ? (CS$<>8__locals1.$I1 = delegate()
						{
							MyWpfExtension.ManageParser().Clipboard.Clear();
							MyWpfExtension.ManageParser().Clipboard.SetText(CS$<>8__locals1.$VB$Local_Text);
						}) : CS$<>8__locals1.$I1, false);
						break;
					}
					catch (Exception ex)
					{
						num++;
						if (num > 5)
						{
							ModBase.Log(ex, "可能由于剪贴板被其他程序占用，文本复制失败", ModBase.LogLevel.Hint, "出现错误");
							break;
						}
						Thread.Sleep(20);
					}
				}
				if (CS$<>8__locals1.$VB$Local_ShowSuccessHint)
				{
					ModMain.Hint("已成功复制！", ModMain.HintType.Finish, true);
				}
			}));
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0000D687 File Offset: 0x0000B887
		public static byte[] GetResources(string ResourceName)
		{
			ModBase.Log("[System] 获取资源：" + ResourceName, ModBase.LogLevel.Normal, "出现错误");
			return (byte[])Resources.CloneParser.GetObject(ResourceName);
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0009E0B4 File Offset: 0x0009C2B4
		public static void DeltaLeft(FrameworkElement control, double newValue)
		{
			ModBase.DebugAssert(!double.IsNaN(newValue));
			ModBase.DebugAssert(!double.IsInfinity(newValue));
			if (control is Window)
			{
				Window window;
				(window = (Window)control).Left = window.Left + newValue;
				return;
			}
			switch (control.HorizontalAlignment)
			{
			case System.Windows.HorizontalAlignment.Left:
			case System.Windows.HorizontalAlignment.Stretch:
				control.Margin = new Thickness(control.Margin.Left + newValue, control.Margin.Top, control.Margin.Right, control.Margin.Bottom);
				return;
			default:
				ModBase.DebugAssert(false);
				return;
			case System.Windows.HorizontalAlignment.Right:
				control.Margin = new Thickness(control.Margin.Left, control.Margin.Top, control.Margin.Right - newValue, control.Margin.Bottom);
				return;
			}
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0009E1AC File Offset: 0x0009C3AC
		public static void SetLeft(FrameworkElement control, double newValue)
		{
			ModBase.DebugAssert(control.HorizontalAlignment == System.Windows.HorizontalAlignment.Left);
			control.Margin = new Thickness(newValue, control.Margin.Top, control.Margin.Right, control.Margin.Bottom);
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0009E200 File Offset: 0x0009C400
		public static void DeltaTop(FrameworkElement control, double newValue)
		{
			ModBase.DebugAssert(!double.IsNaN(newValue));
			ModBase.DebugAssert(!double.IsInfinity(newValue));
			if (control is Window)
			{
				Window window;
				(window = (Window)control).Top = window.Top + newValue;
				return;
			}
			VerticalAlignment verticalAlignment = control.VerticalAlignment;
			if (verticalAlignment == VerticalAlignment.Top)
			{
				control.Margin = new Thickness(control.Margin.Left, control.Margin.Top + newValue, control.Margin.Right, control.Margin.Bottom);
				return;
			}
			if (verticalAlignment != VerticalAlignment.Bottom)
			{
				ModBase.DebugAssert(false);
				return;
			}
			control.Margin = new Thickness(control.Margin.Left, control.Margin.Top, control.Margin.Right, control.Margin.Bottom - newValue);
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0009E2E8 File Offset: 0x0009C4E8
		public static void SetTop(FrameworkElement control, double newValue)
		{
			ModBase.DebugAssert(control.VerticalAlignment == VerticalAlignment.Top);
			control.Margin = new Thickness(control.Margin.Left, newValue, control.Margin.Right, control.Margin.Bottom);
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x0000D6AF File Offset: 0x0000B8AF
		public static double GetPixelSize(double WPFSize)
		{
			return WPFSize / 96.0 * (double)ModBase._ConfigurationRepository;
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0000D6C3 File Offset: 0x0000B8C3
		public static double smethod_4(double PixelSize)
		{
			return PixelSize * 96.0 / (double)ModBase._ConfigurationRepository;
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x0009E33C File Offset: 0x0009C53C
		public static ImageBrush ControlBrush(FrameworkElement UI)
		{
			double actualWidth = UI.ActualWidth;
			double actualHeight = UI.ActualHeight;
			ImageBrush result;
			if (actualWidth >= 1.0 && actualHeight >= 1.0)
			{
				RenderTargetBitmap renderTargetBitmap = checked(new RenderTargetBitmap((int)Math.Round(ModBase.GetPixelSize(actualWidth)), (int)Math.Round(ModBase.GetPixelSize(actualHeight)), (double)ModBase._ConfigurationRepository, (double)ModBase._ConfigurationRepository, PixelFormats.Pbgra32));
				renderTargetBitmap.Render(UI);
				result = new ImageBrush(renderTargetBitmap);
			}
			else
			{
				result = new ImageBrush();
			}
			return result;
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x0009E3B8 File Offset: 0x0009C5B8
		public static ImageBrush ControlBrush(FrameworkElement UI, double Width, double Height, double Left = 0.0, double Top = 0.0)
		{
			UI.Measure(new System.Windows.Size(Width, Height));
			UI.Arrange(new Rect(0.0, 0.0, Width, Height));
			RenderTargetBitmap renderTargetBitmap = checked(new RenderTargetBitmap((int)Math.Round(ModBase.GetPixelSize(Width)), (int)Math.Round(ModBase.GetPixelSize(Height)), (double)ModBase._ConfigurationRepository, (double)ModBase._ConfigurationRepository, PixelFormats.Default));
			renderTargetBitmap.Render(UI);
			if (Left != 0.0 || Top != 0.0)
			{
				UI.Arrange(new Rect(Left, Top, Width, Height));
			}
			return new ImageBrush(renderTargetBitmap);
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0000D6D7 File Offset: 0x0000B8D7
		public static void ControlFreeze(System.Windows.Controls.Panel UI)
		{
			UI.Background = ModBase.ControlBrush(UI);
			UI.Children.Clear();
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x0000D6F0 File Offset: 0x0000B8F0
		public static void ControlFreeze(Border UI)
		{
			UI.Background = ModBase.ControlBrush(UI);
			UI.Child = null;
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0000D705 File Offset: 0x0000B905
		public static object GetObjectFromXML(XElement Str)
		{
			return ModBase.GetObjectFromXML(Str.ToString());
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x0009E458 File Offset: 0x0009C658
		public static object GetObjectFromXML(string Str)
		{
			object result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (StreamWriter streamWriter = new StreamWriter(memoryStream))
				{
					streamWriter.Write(Str);
					streamWriter.Flush();
					memoryStream.Position = 0L;
					result = XamlReader.Load(memoryStream);
				}
			}
			return result;
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0000D712 File Offset: 0x0000B912
		public static bool RunInUi()
		{
			return Thread.CurrentThread.ManagedThreadId == ModBase._GetterRepository;
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0009E4C8 File Offset: 0x0009C6C8
		public static bool IsVisibleInForm(this FrameworkElement element)
		{
			bool result;
			if (!element.IsVisible)
			{
				result = false;
			}
			else
			{
				Rect rect = element.TransformToAncestor(ModMain._ProcessIterator).TransformBounds(new Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight));
				Rect rect2 = new Rect(0.0, 0.0, ModMain._ProcessIterator.ActualWidth, ModMain._ProcessIterator.ActualHeight);
				result = (rect2.Contains(rect.TopLeft) || rect2.Contains(rect.BottomRight));
			}
			return result;
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x0009E568 File Offset: 0x0009C768
		public static bool IsTextTrimmed(this TextBlock Control)
		{
			Control.Measure(new System.Windows.Size(double.MaxValue, double.MaxValue));
			return Control.DesiredSize.Width > Control.ActualWidth;
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0000D725 File Offset: 0x0000B925
		public static void LogStart()
		{
			ModBase.RunInNewThread((ModBase._Closure$__.$I169-0 == null) ? (ModBase._Closure$__.$I169-0 = checked(delegate()
			{
				bool flag = true;
				try
				{
					int num = 4;
					do
					{
						if (File.Exists(ModBase.Path + "PCL\\Log" + Conversions.ToString(num) + ".txt"))
						{
							if (File.Exists(ModBase.Path + "PCL\\Log" + Conversions.ToString(num + 1) + ".txt"))
							{
								File.Delete(ModBase.Path + "PCL\\Log" + Conversions.ToString(num + 1) + ".txt");
							}
							ModBase.CopyFile(ModBase.Path + "PCL\\Log" + Conversions.ToString(num) + ".txt", ModBase.Path + "PCL\\Log" + Conversions.ToString(num + 1) + ".txt");
						}
						num += -1;
					}
					while (num >= 1);
					File.Create(ModBase.Path + "PCL\\Log1.txt").Dispose();
				}
				catch (IOException ex)
				{
					flag = false;
					ModMain.Hint("可能同时开启了多个 PCL，程序可能会出现未知问题！", ModMain.HintType.Critical, true);
					ModBase.Log(ex, "日志初始化失败（疑似文件占用问题）", ModBase.LogLevel.Debug, "出现错误");
				}
				catch (Exception ex2)
				{
					flag = false;
					ModBase.Log(ex2, "日志初始化失败", ModBase.LogLevel.Hint, "出现错误");
				}
				try
				{
					ModBase.m_WriterRepository = new StreamWriter(ModBase.Path + "PCL\\Log1.txt", true)
					{
						AutoFlush = true
					};
					goto IL_196;
				}
				catch (Exception ex3)
				{
					ModBase.m_WriterRepository = null;
					ModBase.Log(ex3, "日志写入失败", ModBase.LogLevel.Hint, "出现错误");
					goto IL_196;
				}
				IL_17C:
				ModBase.expressionRepository = new StringBuilder();
				IL_186:
				Thread.Sleep(50);
				IL_196:
				if (flag)
				{
					ModBase.LogFlush();
					goto IL_186;
				}
				goto IL_17C;
			})) : ModBase._Closure$__.$I169-0, "Log Writer", ThreadPriority.Lowest);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0009E5A8 File Offset: 0x0009C7A8
		public static void LogFlush()
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				if (ModBase.m_WriterRepository == null)
				{
					goto IL_72;
				}
				IL_10:
				num2 = 4;
				string text = null;
				IL_14:
				num2 = 5;
				object registryRepository = ModBase._RegistryRepository;
				ObjectFlowControl.CheckForSyncLockOnValueType(registryRepository);
				lock (registryRepository)
				{
					if (ModBase.expressionRepository.Length > 0)
					{
						StringBuilder stringBuilder = ModBase.expressionRepository;
						ModBase.expressionRepository = new StringBuilder();
						text = stringBuilder.ToString();
					}
				}
				IL_60:
				num2 = 6;
				if (text == null)
				{
					goto IL_72;
				}
				IL_65:
				num2 = 7;
				ModBase.m_WriterRepository.Write(text);
				IL_72:
				goto IL_E1;
				IL_74:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_A2:
				goto IL_D6;
				IL_A4:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_B4:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_A4;
			}
			IL_D6:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_E1:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0009E6BC File Offset: 0x0009C8BC
		public static void Log(string Text, ModBase.LogLevel Level = ModBase.LogLevel.Normal, string Title = "出现错误")
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				string value = string.Concat(new string[]
				{
					"[",
					ModBase.GetTimeNow(),
					"] ",
					Text,
					"\r\n"
				});
				IL_39:
				num2 = 3;
				if (!ModBase._TokenRepository)
				{
					goto IL_78;
				}
				IL_42:
				num2 = 4;
				object ruleRepository = ModBase._RuleRepository;
				ObjectFlowControl.CheckForSyncLockOnValueType(ruleRepository);
				lock (ruleRepository)
				{
					ModBase.expressionRepository.Append(value);
					goto IL_86;
				}
				IL_78:
				num2 = 6;
				ModBase.expressionRepository.Append(value);
				IL_86:
				num2 = 7;
				if (ModBase._ObserverRepository || Level == ModBase.LogLevel.Normal)
				{
					goto IL_243;
				}
				IL_98:
				num2 = 9;
				Text = ModBase.RegexReplace(Text, "", "\\[[^\\]]+?\\] ", 0);
				IL_AE:
				num2 = 10;
				switch (Level)
				{
				case ModBase.LogLevel.Debug:
					IL_DB:
					num2 = 13;
					if (!ModBase._TokenRepository)
					{
						break;
					}
					IL_E8:
					num2 = 14;
					ModMain.Hint("[调试模式] " + Text, ModMain.HintType.Info, false);
					break;
				case ModBase.LogLevel.Hint:
					IL_102:
					num2 = 16;
					ModMain.Hint(Text, ModMain.HintType.Critical, false);
					break;
				case ModBase.LogLevel.Msgbox:
					IL_112:
					num2 = 18;
					ModMain.MyMsgBox(Text, Title, "确定", "", "", true, true, false, null, null, null);
					break;
				case ModBase.LogLevel.Feedback:
					IL_137:
					num2 = 20;
					if (!ModBase.CanFeedback(false))
					{
						goto IL_180;
					}
					IL_142:
					num2 = 21;
					if (ModMain.MyMsgBox(Text + "\r\n\r\n是否反馈此问题？如果不反馈，这个问题可能永远无法得到解决！", Title, "反馈", "取消", "", true, true, false, null, null, null) != 1)
					{
						break;
					}
					IL_171:
					num2 = 22;
					ModBase.Feedback(false, true);
					break;
					IL_180:
					num2 = 24;
					ModMain.MyMsgBox(Text + "\r\n\r\n将 PCL 更新至最新版或许可以解决这个问题……", Title, "确定", "", "", true, true, false, null, null, null);
					break;
				case ModBase.LogLevel.Assert:
				{
					IL_1AF:
					num2 = 26;
					long timeTick = ModBase.GetTimeTick();
					IL_1B9:
					num2 = 27;
					if (!ModBase.CanFeedback(false))
					{
						goto IL_1E9;
					}
					IL_1C4:
					num2 = 28;
					if (Interaction.MsgBox(Text + "\r\n\r\n是否反馈此问题？如果不反馈，这个问题可能永远无法得到解决！", MsgBoxStyle.YesNo | MsgBoxStyle.Critical, Title) != MsgBoxResult.Yes)
					{
						goto IL_200;
					}
					IL_1DD:
					num2 = 29;
					ModBase.Feedback(false, true);
					goto IL_200;
					IL_1E9:
					num2 = 31;
					Interaction.MsgBox(Text + "\r\n\r\n将 PCL 更新至最新版或许可以解决这个问题……", MsgBoxStyle.Critical, Title);
					IL_200:
					num2 = 32;
					if (checked(ModBase.GetTimeTick() - timeTick) >= 1500L)
					{
						goto IL_23A;
					}
					IL_216:
					num2 = 33;
					ModBase.Log("[System] PCL 已崩溃：\r\n" + Text, ModBase.LogLevel.Normal, "出现错误");
					IL_22F:
					num2 = 34;
					FormMain.EndProgramForce(ModBase.Result.Exception);
					break;
					IL_23A:
					num2 = 36;
					FormMain.EndProgramForce(ModBase.Result.Fail);
					break;
				}
				}
				IL_243:
				goto IL_32D;
				IL_248:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_2EE:
				goto IL_322;
				IL_2F0:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_300:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_2F0;
			}
			IL_322:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_32D:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0009EA34 File Offset: 0x0009CC34
		public static void Log(Exception Ex, string Desc, ModBase.LogLevel Level = ModBase.LogLevel.Debug, string Title = "出现错误")
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				if (Ex is ThreadInterruptedException)
				{
					goto IL_290;
				}
				IL_14:
				num2 = 4;
				string text = Desc + "：" + ModBase.GetExceptionDetail(Ex, false);
				IL_29:
				num2 = 5;
				string value = string.Concat(new string[]
				{
					"[",
					ModBase.GetTimeNow(),
					"] ",
					Desc,
					"：",
					ModBase.GetExceptionDetail(Ex, true),
					"\r\n"
				});
				IL_6E:
				num2 = 6;
				if (!ModBase._TokenRepository)
				{
					goto IL_AE;
				}
				IL_77:
				num2 = 7;
				object ruleRepository = ModBase._RuleRepository;
				ObjectFlowControl.CheckForSyncLockOnValueType(ruleRepository);
				lock (ruleRepository)
				{
					ModBase.expressionRepository.Append(value);
					goto IL_BE;
				}
				IL_AE:
				num2 = 9;
				ModBase.expressionRepository.Append(value);
				IL_BE:
				num2 = 10;
				if (ModBase._ObserverRepository)
				{
					goto IL_290;
				}
				IL_CB:
				num2 = 12;
				switch (Level)
				{
				case ModBase.LogLevel.Debug:
				{
					IL_FA:
					num2 = 16;
					string str = Desc + "：" + ModBase.GetExceptionSummary(Ex);
					IL_110:
					num2 = 17;
					if (!ModBase._TokenRepository)
					{
						break;
					}
					IL_11D:
					num2 = 18;
					ModMain.Hint("[调试模式] " + str, ModMain.HintType.Info, false);
					break;
				}
				case ModBase.LogLevel.Hint:
				{
					IL_138:
					num2 = 20;
					string text2 = Desc + "：" + ModBase.GetExceptionSummary(Ex);
					IL_14E:
					num2 = 21;
					ModMain.Hint(text2, ModMain.HintType.Critical, false);
					break;
				}
				case ModBase.LogLevel.Msgbox:
					IL_15F:
					num2 = 23;
					ModMain.MyMsgBox(text, Title, "确定", "", "", true, true, false, null, null, null);
					break;
				case ModBase.LogLevel.Feedback:
					IL_184:
					num2 = 25;
					if (!ModBase.CanFeedback(false))
					{
						goto IL_1CD;
					}
					IL_18F:
					num2 = 26;
					if (ModMain.MyMsgBox(text + "\r\n\r\n是否反馈此问题？如果不反馈，这个问题可能永远无法得到解决！", Title, "反馈", "取消", "", true, true, false, null, null, null) != 1)
					{
						break;
					}
					IL_1BE:
					num2 = 27;
					ModBase.Feedback(false, true);
					break;
					IL_1CD:
					num2 = 29;
					ModMain.MyMsgBox(text + "\r\n\r\n将 PCL 更新至最新版或许可以解决这个问题……", Title, "确定", "", "", true, true, false, null, null, null);
					break;
				case ModBase.LogLevel.Assert:
				{
					IL_1FC:
					num2 = 31;
					long timeTick = ModBase.GetTimeTick();
					IL_206:
					num2 = 32;
					if (!ModBase.CanFeedback(false))
					{
						goto IL_236;
					}
					IL_211:
					num2 = 33;
					if (Interaction.MsgBox(text + "\r\n\r\n是否反馈此问题？如果不反馈，这个问题可能永远无法得到解决！", MsgBoxStyle.YesNo | MsgBoxStyle.Critical, Title) != MsgBoxResult.Yes)
					{
						goto IL_24D;
					}
					IL_22A:
					num2 = 34;
					ModBase.Feedback(false, true);
					goto IL_24D;
					IL_236:
					num2 = 36;
					Interaction.MsgBox(text + "\r\n\r\n将 PCL 更新至最新版或许可以解决这个问题……", MsgBoxStyle.Critical, Title);
					IL_24D:
					num2 = 37;
					if (checked(ModBase.GetTimeTick() - timeTick) >= 1500L)
					{
						goto IL_287;
					}
					IL_263:
					num2 = 38;
					ModBase.Log("[System] PCL 已崩溃：\r\n" + text, ModBase.LogLevel.Normal, "出现错误");
					IL_27C:
					num2 = 39;
					FormMain.EndProgramForce(ModBase.Result.Exception);
					break;
					IL_287:
					num2 = 41;
					FormMain.EndProgramForce(ModBase.Result.Fail);
					break;
				}
				}
				IL_290:
				goto IL_38E;
				IL_295:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_34F:
				goto IL_383;
				IL_351:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_361:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_351;
			}
			IL_383:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_38E:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0009EE0C File Offset: 0x0009D00C
		public static void Feedback(bool ShowMsgbox = true, bool ForceOpenLog = false)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				ModBase.FeedbackInfo();
				IL_0E:
				num2 = 3;
				if (!ForceOpenLog && (!ShowMsgbox || ModMain.MyMsgBox("若你在汇报一个 Bug，请点击 打开文件夹 按钮，并上传 Log(1~5).txt 中包含错误信息的文件。\r\n游戏崩溃一般与启动器无关，请不要因为游戏崩溃而提交反馈。", "反馈提交提醒", "打开文件夹", "不需要", "", false, true, false, null, null, null) != 1))
				{
					goto IL_58;
				}
				IL_3D:
				num2 = 4;
				ModBase.OpenExplorer("\"" + ModBase.Path + "PCL\\\"");
				IL_58:
				num2 = 5;
				ModBase.OpenWebsite("https://github.com/Hex-Dragon/PCL2/issues/");
				IL_64:
				goto IL_CB;
				IL_66:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_8C:
				goto IL_C0;
				IL_8E:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_9E:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_8E;
			}
			IL_C0:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_CB:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0009EEFC File Offset: 0x0009D0FC
		public static bool CanFeedback(bool ShowHint)
		{
			bool result;
			if (false.Equals(PageSetupSystem.IsLauncherNewest()))
			{
				if (ShowHint && ModMain.MyMsgBox(string.Format("你的 PCL 不是最新版，因此无法提交反馈。{0}请在更新后，确认该问题在最新版中依然存在，然后再提交反馈。", "\r\n"), "无法提交反馈", "更新", "取消", "", false, true, false, null, null, null) == 1)
				{
					ModSecret.UpdateCheckByButton();
				}
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x0009EF60 File Offset: 0x0009D160
		public static void FeedbackInfo()
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				ModBase.Log(string.Concat(new string[]
				{
					"[System] 诊断信息：\r\n操作系统：",
					MyWpfExtension.ManageParser().Info.OSFullName,
					"（32 位：",
					Conversions.ToString(ModBase.m_StubRepository),
					"）\r\n剩余内存：",
					Conversions.ToString(Conversion.Int(MyWpfExtension.ManageParser().Info.AvailablePhysicalMemory / 1024.0 / 1024.0)),
					" M / ",
					Conversions.ToString(Conversion.Int(MyWpfExtension.ManageParser().Info.TotalPhysicalMemory / 1024.0 / 1024.0)),
					" M\r\nDPI：",
					Conversions.ToString(ModBase._ConfigurationRepository),
					"（",
					Conversions.ToString(Math.Round((double)ModBase._ConfigurationRepository / 96.0, 2) * 100.0),
					"%）\r\nMC 文件夹：",
					ModMinecraft.m_ProxyTests ?? "Nothing",
					"\r\n文件位置：",
					ModBase.Path
				}), ModBase.LogLevel.Normal, "出现错误");
				IL_138:
				goto IL_193;
				IL_13A:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_154:
				goto IL_188;
				IL_156:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_166:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_156;
			}
			IL_188:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_193:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0000D757 File Offset: 0x0000B957
		public static void DebugAssert(bool Exp)
		{
			if (!Exp)
			{
				throw new Exception("断言命中");
			}
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0009F124 File Offset: 0x0009D324
		public static string GetStackTrace()
		{
			return Enumerable.ToList<string>(Enumerable.Select<MethodBase, string>(Enumerable.Select<StackFrame, MethodBase>(Enumerable.Skip<StackFrame>(new StackTrace().GetFrames(), 1), (ModBase._Closure$__.$I179-0 == null) ? (ModBase._Closure$__.$I179-0 = ((StackFrame f) => f.GetMethod())) : ModBase._Closure$__.$I179-0), (ModBase._Closure$__.$I179-1 == null) ? (ModBase._Closure$__.$I179-1 = ((MethodBase f) => string.Concat(new string[]
			{
				f.Name,
				"(",
				Enumerable.ToList<string>(Enumerable.Select<ParameterInfo, string>(f.GetParameters(), (ModBase._Closure$__.$I179-2 == null) ? (ModBase._Closure$__.$I179-2 = ((ParameterInfo p) => p.ToString())) : ModBase._Closure$__.$I179-2)).Join(", "),
				") - ",
				f.Module.ToString()
			}))) : ModBase._Closure$__.$I179-1)).Join("\r\n").Replace("\r\n\r\n", "\r\n");
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x0000D767 File Offset: 0x0000B967
		public static T RandomOne<T>(ICollection<T> objects)
		{
			return Enumerable.ElementAtOrDefault<T>(objects, ModBase.RandomInteger(0, checked(objects.Count - 1)));
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x0000D77D File Offset: 0x0000B97D
		public static int RandomInteger(int min, int max)
		{
			return checked((int)Math.Round(unchecked(Math.Floor((double)(checked(max - min + 1)) * ModBase._ProccesorRepository.NextDouble()) + (double)min)));
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0009F1B4 File Offset: 0x0009D3B4
		public static IList<T> Shuffle<T>(IList<T> array)
		{
			IList<T> list = new List<!!0>();
			while (Enumerable.Any<T>(array))
			{
				int index = ModBase.RandomInteger(0, checked(array.Count - 1));
				list.Add(array[index]);
				array.RemoveAt(index);
			}
			return list;
		}

		// Token: 0x04000C51 RID: 3153
		public static IntPtr m_IndexerRepository;

		// Token: 0x04000C52 RID: 3154
		public static string Path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

		// Token: 0x04000C53 RID: 3155
		public static string interpreterRepository = ModBase.Path + AppDomain.CurrentDomain.SetupInformation.ApplicationName;

		// Token: 0x04000C54 RID: 3156
		public static string m_SerializerRepository = "pack://application:,,,/Plain Craft Launcher 2;component/Images/";

		// Token: 0x04000C55 RID: 3157
		public static string watcherRepository = "zh_CN";

		// Token: 0x04000C56 RID: 3158
		public static ModSetup m_IdentifierRepository = new ModSetup();

		// Token: 0x04000C57 RID: 3159
		public static long _SystemRepository = ModBase.GetTimeTick();

		// Token: 0x04000C58 RID: 3160
		public static DateTime paramRepository = DateTime.Now;

		// Token: 0x04000C59 RID: 3161
		public static string _TagRepository = ModSecret.SecretGetUniqueAddress();

		// Token: 0x04000C5A RID: 3162
		public static bool _ObserverRepository = false;

		// Token: 0x04000C5B RID: 3163
		public static bool m_StubRepository = !Environment.Is64BitOperatingSystem;

		// Token: 0x04000C5C RID: 3164
		public static bool rulesRepository = Encoding.Default.CodePage == 936;

		// Token: 0x04000C5D RID: 3165
		public static string _RefRepository = Conversions.ToString(Enumerable.First<char>(Enumerable.First<string>(Enumerable.Where<string>(Environment.GetLogicalDrives(), (string p) => Directory.Exists(p))).ToUpper())) + ":\\";

		// Token: 0x04000C5E RID: 3166
		public static string m_DecoratorRepository = (Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("SystemSystemCache", null), "", false) ? (System.IO.Path.GetTempPath() + "PCL\\") : ModBase.m_IdentifierRepository.Get("SystemSystemCache", null)).ToString().Replace("/", "\\").TrimEnd(new char[]
		{
			'\\'
		}) + "\\";

		// Token: 0x04000C5F RID: 3167
		public static string m_InstanceRepository = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PCL\\";

		// Token: 0x04000C60 RID: 3168
		private static readonly Dictionary<string, Dictionary<string, string>> stateRepository = new Dictionary<string, Dictionary<string, string>>();

		// Token: 0x04000C61 RID: 3169
		public static char callbackRepository = Convert.ToChar(8220);

		// Token: 0x04000C62 RID: 3170
		public static char m_TemplateRepository = Convert.ToChar(8221);

		// Token: 0x04000C63 RID: 3171
		public static string _MethodRepository = ModBase.GetPureASCIIDir();

		// Token: 0x04000C64 RID: 3172
		private static int taskRepository = 1;

		// Token: 0x04000C65 RID: 3173
		private static object m_ConsumerRepository;

		// Token: 0x04000C66 RID: 3174
		public static readonly int _ConfigurationRepository = checked((int)Math.Round((double)Graphics.FromHwnd(IntPtr.Zero).DpiX));

		// Token: 0x04000C67 RID: 3175
		private static readonly int _GetterRepository = Thread.CurrentThread.ManagedThreadId;

		// Token: 0x04000C68 RID: 3176
		public static bool _TokenRepository = false;

		// Token: 0x04000C69 RID: 3177
		private static StringBuilder expressionRepository = new StringBuilder();

		// Token: 0x04000C6A RID: 3178
		private static StreamWriter m_WriterRepository;

		// Token: 0x04000C6B RID: 3179
		private static readonly object _RegistryRepository = RuntimeHelpers.GetObjectValue(new object());

		// Token: 0x04000C6C RID: 3180
		private static readonly object _RuleRepository = RuntimeHelpers.GetObjectValue(new object());

		// Token: 0x04000C6D RID: 3181
		private static readonly Random _ProccesorRepository = new Random();

		// Token: 0x020001FE RID: 510
		public class Logo
		{
		}

		// Token: 0x020001FF RID: 511
		public class MyColor
		{
			// Token: 0x0600182E RID: 6190 RVA: 0x0000D79E File Offset: 0x0000B99E
			public static implicit operator ModBase.MyColor(string str)
			{
				return new ModBase.MyColor(str);
			}

			// Token: 0x0600182F RID: 6191 RVA: 0x0000D7A6 File Offset: 0x0000B9A6
			public static implicit operator ModBase.MyColor(System.Windows.Media.Color col)
			{
				return new ModBase.MyColor(col);
			}

			// Token: 0x06001830 RID: 6192 RVA: 0x0000D7AE File Offset: 0x0000B9AE
			public static implicit operator System.Windows.Media.Color(ModBase.MyColor conv)
			{
				return System.Windows.Media.Color.FromArgb(ModBase.MathByte(conv.mapperError), ModBase.MathByte(conv.m_ThreadError), ModBase.MathByte(conv._PropertyError), ModBase.MathByte(conv.composerError));
			}

			// Token: 0x06001831 RID: 6193 RVA: 0x0000D7E1 File Offset: 0x0000B9E1
			public static implicit operator System.Drawing.Color(ModBase.MyColor conv)
			{
				return System.Drawing.Color.FromArgb((int)ModBase.MathByte(conv.mapperError), (int)ModBase.MathByte(conv.m_ThreadError), (int)ModBase.MathByte(conv._PropertyError), (int)ModBase.MathByte(conv.composerError));
			}

			// Token: 0x06001832 RID: 6194 RVA: 0x0000D814 File Offset: 0x0000BA14
			public static implicit operator ModBase.MyColor(SolidColorBrush bru)
			{
				return new ModBase.MyColor(bru.Color);
			}

			// Token: 0x06001833 RID: 6195 RVA: 0x0000D821 File Offset: 0x0000BA21
			public static implicit operator SolidColorBrush(ModBase.MyColor conv)
			{
				return new SolidColorBrush(System.Windows.Media.Color.FromArgb(ModBase.MathByte(conv.mapperError), ModBase.MathByte(conv.m_ThreadError), ModBase.MathByte(conv._PropertyError), ModBase.MathByte(conv.composerError)));
			}

			// Token: 0x06001834 RID: 6196 RVA: 0x0000D859 File Offset: 0x0000BA59
			public static implicit operator ModBase.MyColor(System.Windows.Media.Brush bru)
			{
				return new ModBase.MyColor(bru);
			}

			// Token: 0x06001835 RID: 6197 RVA: 0x0000D821 File Offset: 0x0000BA21
			public static implicit operator System.Windows.Media.Brush(ModBase.MyColor conv)
			{
				return new SolidColorBrush(System.Windows.Media.Color.FromArgb(ModBase.MathByte(conv.mapperError), ModBase.MathByte(conv.m_ThreadError), ModBase.MathByte(conv._PropertyError), ModBase.MathByte(conv.composerError)));
			}

			// Token: 0x06001836 RID: 6198 RVA: 0x0009F1F8 File Offset: 0x0009D3F8
			public static ModBase.MyColor operator +(ModBase.MyColor a, ModBase.MyColor b)
			{
				return new ModBase.MyColor
				{
					mapperError = a.mapperError + b.mapperError,
					composerError = a.composerError + b.composerError,
					_PropertyError = a._PropertyError + b._PropertyError,
					m_ThreadError = a.m_ThreadError + b.m_ThreadError
				};
			}

			// Token: 0x06001837 RID: 6199 RVA: 0x0009F258 File Offset: 0x0009D458
			public static ModBase.MyColor operator -(ModBase.MyColor a, ModBase.MyColor b)
			{
				return new ModBase.MyColor
				{
					mapperError = a.mapperError - b.mapperError,
					composerError = a.composerError - b.composerError,
					_PropertyError = a._PropertyError - b._PropertyError,
					m_ThreadError = a.m_ThreadError - b.m_ThreadError
				};
			}

			// Token: 0x06001838 RID: 6200 RVA: 0x0000D861 File Offset: 0x0000BA61
			public static ModBase.MyColor operator *(ModBase.MyColor a, double b)
			{
				return new ModBase.MyColor
				{
					mapperError = a.mapperError * b,
					composerError = a.composerError * b,
					_PropertyError = a._PropertyError * b,
					m_ThreadError = a.m_ThreadError * b
				};
			}

			// Token: 0x06001839 RID: 6201 RVA: 0x0000D8A0 File Offset: 0x0000BAA0
			public static ModBase.MyColor operator /(ModBase.MyColor a, double b)
			{
				return new ModBase.MyColor
				{
					mapperError = a.mapperError / b,
					composerError = a.composerError / b,
					_PropertyError = a._PropertyError / b,
					m_ThreadError = a.m_ThreadError / b
				};
			}

			// Token: 0x0600183A RID: 6202 RVA: 0x0009F2B8 File Offset: 0x0009D4B8
			public static bool operator ==(ModBase.MyColor a, ModBase.MyColor b)
			{
				return (Information.IsNothing(a) && Information.IsNothing(b)) || (!Information.IsNothing(a) && !Information.IsNothing(b) && (a.mapperError == b.mapperError && a.m_ThreadError == b.m_ThreadError && a._PropertyError == b._PropertyError) && a.composerError == b.composerError);
			}

			// Token: 0x0600183B RID: 6203 RVA: 0x0009F32C File Offset: 0x0009D52C
			public static bool operator !=(ModBase.MyColor a, ModBase.MyColor b)
			{
				return (!Information.IsNothing(a) || !Information.IsNothing(b)) && (Information.IsNothing(a) || Information.IsNothing(b) || a.mapperError != b.mapperError || a.m_ThreadError != b.m_ThreadError || a._PropertyError != b._PropertyError || a.composerError != b.composerError);
			}

			// Token: 0x0600183C RID: 6204 RVA: 0x0009F3A4 File Offset: 0x0009D5A4
			public MyColor()
			{
				this.mapperError = 255.0;
				this.m_ThreadError = 0.0;
				this._PropertyError = 0.0;
				this.composerError = 0.0;
			}

			// Token: 0x0600183D RID: 6205 RVA: 0x0009F3F4 File Offset: 0x0009D5F4
			public MyColor(System.Windows.Media.Color col)
			{
				this.mapperError = 255.0;
				this.m_ThreadError = 0.0;
				this._PropertyError = 0.0;
				this.composerError = 0.0;
				this.mapperError = (double)col.A;
				this.m_ThreadError = (double)col.R;
				this._PropertyError = (double)col.G;
				this.composerError = (double)col.B;
			}

			// Token: 0x0600183E RID: 6206 RVA: 0x0009F47C File Offset: 0x0009D67C
			public MyColor(string HexString)
			{
				this.mapperError = 255.0;
				this.m_ThreadError = 0.0;
				this._PropertyError = 0.0;
				this.composerError = 0.0;
				object obj = System.Windows.Media.ColorConverter.ConvertFromString(HexString);
				System.Windows.Media.Color color = (obj != null) ? ((System.Windows.Media.Color)obj) : default(System.Windows.Media.Color);
				this.mapperError = (double)color.A;
				this.m_ThreadError = (double)color.R;
				this._PropertyError = (double)color.G;
				this.composerError = (double)color.B;
			}

			// Token: 0x0600183F RID: 6207 RVA: 0x0009F520 File Offset: 0x0009D720
			public MyColor(double newA, ModBase.MyColor col)
			{
				this.mapperError = 255.0;
				this.m_ThreadError = 0.0;
				this._PropertyError = 0.0;
				this.composerError = 0.0;
				this.mapperError = newA;
				this.m_ThreadError = col.m_ThreadError;
				this._PropertyError = col._PropertyError;
				this.composerError = col.composerError;
			}

			// Token: 0x06001840 RID: 6208 RVA: 0x0009F59C File Offset: 0x0009D79C
			public MyColor(double newR, double newG, double newB)
			{
				this.mapperError = 255.0;
				this.m_ThreadError = 0.0;
				this._PropertyError = 0.0;
				this.composerError = 0.0;
				this.mapperError = 255.0;
				this.m_ThreadError = newR;
				this._PropertyError = newG;
				this.composerError = newB;
			}

			// Token: 0x06001841 RID: 6209 RVA: 0x0009F610 File Offset: 0x0009D810
			public MyColor(double newA, double newR, double newG, double newB)
			{
				this.mapperError = 255.0;
				this.m_ThreadError = 0.0;
				this._PropertyError = 0.0;
				this.composerError = 0.0;
				this.mapperError = newA;
				this.m_ThreadError = newR;
				this._PropertyError = newG;
				this.composerError = newB;
			}

			// Token: 0x06001842 RID: 6210 RVA: 0x0009F680 File Offset: 0x0009D880
			public MyColor(System.Windows.Media.Brush brush)
			{
				this.mapperError = 255.0;
				this.m_ThreadError = 0.0;
				this._PropertyError = 0.0;
				this.composerError = 0.0;
				System.Windows.Media.Color color = ((SolidColorBrush)brush).Color;
				this.mapperError = (double)color.A;
				this.m_ThreadError = (double)color.R;
				this._PropertyError = (double)color.G;
				this.composerError = (double)color.B;
			}

			// Token: 0x06001843 RID: 6211 RVA: 0x0009F714 File Offset: 0x0009D914
			public MyColor(SolidColorBrush brush)
			{
				this.mapperError = 255.0;
				this.m_ThreadError = 0.0;
				this._PropertyError = 0.0;
				this.composerError = 0.0;
				System.Windows.Media.Color color = brush.Color;
				this.mapperError = (double)color.A;
				this.m_ThreadError = (double)color.R;
				this._PropertyError = (double)color.G;
				this.composerError = (double)color.B;
			}

			// Token: 0x06001844 RID: 6212 RVA: 0x0009F7A4 File Offset: 0x0009D9A4
			public MyColor(object obj)
			{
				this.mapperError = 255.0;
				this.m_ThreadError = 0.0;
				this._PropertyError = 0.0;
				this.composerError = 0.0;
				if (obj == null)
				{
					this.mapperError = 255.0;
					this.m_ThreadError = 255.0;
					this._PropertyError = 255.0;
					this.composerError = 255.0;
					return;
				}
				if (obj is SolidColorBrush)
				{
					System.Windows.Media.Color color = ((SolidColorBrush)obj).Color;
					this.mapperError = (double)color.A;
					this.m_ThreadError = (double)color.R;
					this._PropertyError = (double)color.G;
					this.composerError = (double)color.B;
					return;
				}
				this.mapperError = Conversions.ToDouble(NewLateBinding.LateGet(obj, null, "A", new object[0], null, null, null));
				this.m_ThreadError = Conversions.ToDouble(NewLateBinding.LateGet(obj, null, "R", new object[0], null, null, null));
				this._PropertyError = Conversions.ToDouble(NewLateBinding.LateGet(obj, null, "G", new object[0], null, null, null));
				this.composerError = Conversions.ToDouble(NewLateBinding.LateGet(obj, null, "B", new object[0], null, null, null));
			}

			// Token: 0x06001845 RID: 6213 RVA: 0x0009F904 File Offset: 0x0009DB04
			public double Hue(double v1, double v2, double vH)
			{
				if (vH < 0.0)
				{
					vH += 1.0;
				}
				if (vH > 1.0)
				{
					vH -= 1.0;
				}
				double result;
				if (vH < 0.16667)
				{
					result = v1 + (v2 - v1) * 6.0 * vH;
				}
				else if (vH < 0.5)
				{
					result = v2;
				}
				else if (vH < 0.66667)
				{
					result = v1 + (v2 - v1) * (4.0 - vH * 6.0);
				}
				else
				{
					result = v1;
				}
				return result;
			}

			// Token: 0x06001846 RID: 6214 RVA: 0x0009F9A0 File Offset: 0x0009DBA0
			public ModBase.MyColor FromHSL(double sH, double sS, double sL)
			{
				if (sS == 0.0)
				{
					this.m_ThreadError = sL * 2.55;
					this._PropertyError = this.m_ThreadError;
					this.composerError = this.m_ThreadError;
				}
				else
				{
					double num = sH / 360.0;
					double num2 = sS / 100.0;
					double num3 = sL / 100.0;
					num2 = ((num3 < 0.5) ? (num2 * num3 + num3) : (num2 * (1.0 - num3) + num3));
					num3 = 2.0 * num3 - num2;
					this.m_ThreadError = 255.0 * this.Hue(num3, num2, num + 0.3333333333333333);
					this._PropertyError = 255.0 * this.Hue(num3, num2, num);
					this.composerError = 255.0 * this.Hue(num3, num2, num - 0.3333333333333333);
				}
				this.mapperError = 255.0;
				return this;
			}

			// Token: 0x06001847 RID: 6215 RVA: 0x0009FAAC File Offset: 0x0009DCAC
			public ModBase.MyColor FromHSL2(double sH, double sS, double sL)
			{
				if (sS == 0.0)
				{
					this.m_ThreadError = sL * 2.55;
					this._PropertyError = this.m_ThreadError;
					this.composerError = this.m_ThreadError;
				}
				else
				{
					sH = (sH + 3600000.0) % 360.0;
					double[] array = new double[]
					{
						0.1,
						-0.06,
						-0.3,
						-0.19,
						-0.15,
						-0.24,
						-0.32,
						-0.09,
						0.18,
						0.05,
						-0.12,
						-0.02,
						0.1,
						-0.06
					};
					double num = sH / 30.0;
					int num2 = checked((int)Math.Floor(num));
					num = 50.0 - ((1.0 - num + (double)num2) * array[num2] + (num - (double)num2) * array[checked(num2 + 1)]) * sS;
					sL = ((sL < num) ? (sL / num) : (1.0 + (sL - num) / (100.0 - num))) * 50.0;
					this.FromHSL(sH, sS, sL);
				}
				this.mapperError = 255.0;
				return this;
			}

			// Token: 0x06001848 RID: 6216 RVA: 0x0009FBA4 File Offset: 0x0009DDA4
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"(",
					Conversions.ToString(this.mapperError),
					",",
					Conversions.ToString(this.m_ThreadError),
					",",
					Conversions.ToString(this._PropertyError),
					",",
					Conversions.ToString(this.composerError),
					")"
				});
			}

			// Token: 0x06001849 RID: 6217 RVA: 0x0000D8DF File Offset: 0x0000BADF
			public override bool Equals(object obj)
			{
				return this == (ModBase.MyColor)obj;
			}

			// Token: 0x04000C6E RID: 3182
			public double mapperError;

			// Token: 0x04000C6F RID: 3183
			public double m_ThreadError;

			// Token: 0x04000C70 RID: 3184
			public double _PropertyError;

			// Token: 0x04000C71 RID: 3185
			public double composerError;
		}

		// Token: 0x02000200 RID: 512
		public class MyRect
		{
			// Token: 0x17000407 RID: 1031
			// (get) Token: 0x0600184B RID: 6219 RVA: 0x0000D8ED File Offset: 0x0000BAED
			// (set) Token: 0x0600184C RID: 6220 RVA: 0x0000D8F5 File Offset: 0x0000BAF5
			public double Width { get; set; }

			// Token: 0x17000408 RID: 1032
			// (get) Token: 0x0600184D RID: 6221 RVA: 0x0000D8FE File Offset: 0x0000BAFE
			// (set) Token: 0x0600184E RID: 6222 RVA: 0x0000D906 File Offset: 0x0000BB06
			public double Height { get; set; }

			// Token: 0x17000409 RID: 1033
			// (get) Token: 0x0600184F RID: 6223 RVA: 0x0000D90F File Offset: 0x0000BB0F
			// (set) Token: 0x06001850 RID: 6224 RVA: 0x0000D917 File Offset: 0x0000BB17
			public double Left { get; set; }

			// Token: 0x1700040A RID: 1034
			// (get) Token: 0x06001851 RID: 6225 RVA: 0x0000D920 File Offset: 0x0000BB20
			// (set) Token: 0x06001852 RID: 6226 RVA: 0x0000D928 File Offset: 0x0000BB28
			public double Top { get; set; }

			// Token: 0x06001853 RID: 6227 RVA: 0x0009FC20 File Offset: 0x0009DE20
			public MyRect()
			{
				this.Width = 0.0;
				this.Height = 0.0;
				this.Left = 0.0;
				this.Top = 0.0;
			}

			// Token: 0x06001854 RID: 6228 RVA: 0x0009FC70 File Offset: 0x0009DE70
			public MyRect(double left, double top, double width, double height)
			{
				this.Width = 0.0;
				this.Height = 0.0;
				this.Left = 0.0;
				this.Top = 0.0;
				this.Left = left;
				this.Top = top;
				this.Width = width;
				this.Height = height;
			}

			// Token: 0x04000C72 RID: 3186
			[CompilerGenerated]
			private double m_IteratorError;

			// Token: 0x04000C73 RID: 3187
			[CompilerGenerated]
			private double _RepositoryError;

			// Token: 0x04000C74 RID: 3188
			[CompilerGenerated]
			private double m_TestError;

			// Token: 0x04000C75 RID: 3189
			[CompilerGenerated]
			private double m_MapError;
		}

		// Token: 0x02000201 RID: 513
		public enum LoadState
		{
			// Token: 0x04000C77 RID: 3191
			Waiting,
			// Token: 0x04000C78 RID: 3192
			Loading,
			// Token: 0x04000C79 RID: 3193
			Finished,
			// Token: 0x04000C7A RID: 3194
			Failed,
			// Token: 0x04000C7B RID: 3195
			Aborted
		}

		// Token: 0x02000202 RID: 514
		public enum Result
		{
			// Token: 0x04000C7D RID: 3197
			Aborted = -1,
			// Token: 0x04000C7E RID: 3198
			Success,
			// Token: 0x04000C7F RID: 3199
			Fail,
			// Token: 0x04000C80 RID: 3200
			Exception,
			// Token: 0x04000C81 RID: 3201
			Timeout,
			// Token: 0x04000C82 RID: 3202
			Cancel
		}

		// Token: 0x02000203 RID: 515
		public class EqualableList<T> : List<!0>
		{
			// Token: 0x06001857 RID: 6231 RVA: 0x0009FCE0 File Offset: 0x0009DEE0
			public override bool Equals(object obj)
			{
				checked
				{
					bool result;
					if (!(obj is List<!0>))
					{
						result = false;
					}
					else
					{
						List<T> list = (List<!0>)obj;
						if (list.Count != base.Count)
						{
							result = false;
						}
						else
						{
							int num = list.Count - 1;
							for (int i = 0; i <= num; i++)
							{
								T t = list[i];
								if (!t.Equals(base[i]))
								{
									return false;
								}
							}
							result = true;
						}
					}
					return result;
				}
			}

			// Token: 0x06001858 RID: 6232 RVA: 0x0000D93A File Offset: 0x0000BB3A
			public static bool operator ==(ModBase.EqualableList<T> left, ModBase.EqualableList<T> right)
			{
				return EqualityComparer<ModBase.EqualableList<!0>>.Default.Equals(left, right);
			}

			// Token: 0x06001859 RID: 6233 RVA: 0x0000D948 File Offset: 0x0000BB48
			public static bool operator !=(ModBase.EqualableList<T> left, ModBase.EqualableList<T> right)
			{
				return !(left == right);
			}
		}

		// Token: 0x02000204 RID: 516
		public class FileChecker
		{
			// Token: 0x0600185B RID: 6235 RVA: 0x0009FD54 File Offset: 0x0009DF54
			public FileChecker(long MinSize = -1L, long ActualSize = -1L, string Hash = null, bool CanUseExistsFile = true, bool IsJson = false)
			{
				this._ErrorError = -1L;
				this.m_ContextError = -1L;
				this.specificationError = null;
				this.m_MockError = true;
				this.m_RequestError = false;
				this._ErrorError = ActualSize;
				this.m_ContextError = MinSize;
				this.specificationError = Hash;
				this.m_MockError = CanUseExistsFile;
				this.m_RequestError = IsJson;
			}

			// Token: 0x0600185C RID: 6236 RVA: 0x0009FDC0 File Offset: 0x0009DFC0
			public string Check(string LocalPath)
			{
				string result;
				try
				{
					FileInfo fileInfo = new FileInfo(LocalPath);
					if (!fileInfo.Exists)
					{
						result = "文件不存在：" + LocalPath;
					}
					else
					{
						long length = fileInfo.Length;
						if (this._ErrorError >= 0L && this._ErrorError != length)
						{
							result = string.Concat(new string[]
							{
								"文件大小应为 ",
								Conversions.ToString(this._ErrorError),
								" B，实际为 ",
								Conversions.ToString(length),
								" B"
							});
						}
						else if (this.m_ContextError >= 0L && this.m_ContextError > length)
						{
							result = string.Concat(new string[]
							{
								"文件大小应大于 ",
								Conversions.ToString(this.m_ContextError),
								" B，实际为 ",
								Conversions.ToString(length),
								" B"
							});
						}
						else
						{
							if (!string.IsNullOrEmpty(this.specificationError))
							{
								if (this.specificationError.Length < 35)
								{
									if (Operators.CompareString(this.specificationError.ToLowerInvariant(), ModBase.smethod_0(LocalPath), false) != 0)
									{
										return "文件 MD5 应为 " + this.specificationError + "，实际为 " + ModBase.smethod_0(LocalPath);
									}
								}
								else if (this.specificationError.Length == 64)
								{
									if (Operators.CompareString(this.specificationError.ToLowerInvariant(), ModBase.GetFileSHA256(LocalPath), false) != 0)
									{
										return "文件 SHA256 应为 " + this.specificationError + "，实际为 " + ModBase.GetFileSHA256(LocalPath);
									}
								}
								else if (Operators.CompareString(this.specificationError.ToLowerInvariant(), ModBase.smethod_1(LocalPath), false) != 0)
								{
									return "文件 SHA1 应为 " + this.specificationError + "，实际为 " + ModBase.smethod_1(LocalPath);
								}
							}
							if (this.m_RequestError)
							{
								string text = ModBase.ReadFile(LocalPath, null);
								if (Operators.CompareString(text, "", false) == 0)
								{
									throw new Exception("读取到的文件为空");
								}
								try
								{
									ModBase.GetJson(text);
								}
								catch (Exception innerException)
								{
									throw new Exception("不是有效的 json 文件", innerException);
								}
							}
							result = null;
						}
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "检查文件出错", ModBase.LogLevel.Debug, "出现错误");
					result = ModBase.GetExceptionSummary(ex);
				}
				return result;
			}

			// Token: 0x04000C83 RID: 3203
			public long _ErrorError;

			// Token: 0x04000C84 RID: 3204
			public long m_ContextError;

			// Token: 0x04000C85 RID: 3205
			public string specificationError;

			// Token: 0x04000C86 RID: 3206
			public bool m_MockError;

			// Token: 0x04000C87 RID: 3207
			public bool m_RequestError;
		}

		// Token: 0x02000205 RID: 517
		public class SearchEntry<T>
		{
			// Token: 0x04000C88 RID: 3208
			public T m_DicError;

			// Token: 0x04000C89 RID: 3209
			public List<KeyValuePair<string, double>> helperError;

			// Token: 0x04000C8A RID: 3210
			public double m_IssuerError;

			// Token: 0x04000C8B RID: 3211
			public bool indexerError;
		}

		// Token: 0x02000206 RID: 518
		public class SafeList<T> : SynchronizedCollection<T>, IEnumerable, IEnumerable<!0>
		{
			// Token: 0x06001860 RID: 6240 RVA: 0x0000D954 File Offset: 0x0000BB54
			public SafeList()
			{
			}

			// Token: 0x06001861 RID: 6241 RVA: 0x0000D95D File Offset: 0x0000BB5D
			public SafeList(IEnumerable<T> Data) : base(RuntimeHelpers.GetObjectValue(new object()), Data)
			{
			}

			// Token: 0x06001862 RID: 6242 RVA: 0x0000D971 File Offset: 0x0000BB71
			public static implicit operator ModBase.SafeList<T>(List<T> Data)
			{
				return new ModBase.SafeList<!0>(Data);
			}

			// Token: 0x06001863 RID: 6243 RVA: 0x0000D979 File Offset: 0x0000BB79
			public static implicit operator List<T>(ModBase.SafeList<T> Data)
			{
				return new List<!0>(Data);
			}

			// Token: 0x06001864 RID: 6244 RVA: 0x000A0030 File Offset: 0x0009E230
			public new IEnumerator<T> GetEnumerator()
			{
				object syncRoot = base.SyncRoot;
				ObjectFlowControl.CheckForSyncLockOnValueType(syncRoot);
				IEnumerator<T> result;
				lock (syncRoot)
				{
					result = (IEnumerator<!0>)Enumerable.ToList<T>(base.Items).GetEnumerator();
				}
				return result;
			}

			// Token: 0x06001865 RID: 6245 RVA: 0x000A008C File Offset: 0x0009E28C
			IEnumerator IEnumerable.GetEnumeratorGeneral()
			{
				object syncRoot = base.SyncRoot;
				ObjectFlowControl.CheckForSyncLockOnValueType(syncRoot);
				IEnumerator result;
				lock (syncRoot)
				{
					result = (IEnumerator)Enumerable.ToList<T>(base.Items).GetEnumerator();
				}
				return result;
			}
		}

		// Token: 0x02000207 RID: 519
		public class RestartException : Exception
		{
		}

		// Token: 0x02000208 RID: 520
		public class CancelledException : Exception
		{
		}

		// Token: 0x02000209 RID: 521
		public sealed class RouteEventArgs : EventArgs
		{
			// Token: 0x0600186B RID: 6251 RVA: 0x0000D981 File Offset: 0x0000BB81
			public RouteEventArgs(bool RaiseByMouse = false)
			{
				this.m_SerializerError = false;
				this.interpreterError = RaiseByMouse;
			}

			// Token: 0x04000C8C RID: 3212
			public bool interpreterError;

			// Token: 0x04000C8D RID: 3213
			public bool m_SerializerError;
		}

		// Token: 0x0200020A RID: 522
		// (Invoke) Token: 0x06001870 RID: 6256
		public delegate bool CompareThreadStart<T>(T Left, T Right);

		// Token: 0x0200020B RID: 523
		public enum LogLevel
		{
			// Token: 0x04000C8F RID: 3215
			Normal,
			// Token: 0x04000C90 RID: 3216
			Developer,
			// Token: 0x04000C91 RID: 3217
			Debug,
			// Token: 0x04000C92 RID: 3218
			Hint,
			// Token: 0x04000C93 RID: 3219
			Msgbox,
			// Token: 0x04000C94 RID: 3220
			Feedback,
			// Token: 0x04000C95 RID: 3221
			Assert
		}
	}
}
