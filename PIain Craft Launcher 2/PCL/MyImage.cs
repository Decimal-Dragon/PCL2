using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x0200003E RID: 62
	public class MyImage : Image
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00012840 File Offset: 0x00010A40
		public MyImage()
		{
			base.Initialized += delegate(object sender, EventArgs e)
			{
				this.Load();
			};
			this.exporter = new TimeSpan(7, 0, 0, 0);
			this._Worker = "";
			this.m_Server = null;
			this._Resolver = "pack://application:,,,/images/Icons/NoIcon.png";
			this._Status = null;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00002CD8 File Offset: 0x00000ED8
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00002CEA File Offset: 0x00000EEA
		public bool EnableCache
		{
			get
			{
				return Conversions.ToBoolean(base.GetValue(MyImage._Importer));
			}
			set
			{
				base.SetValue(MyImage._Importer, value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00002CFD File Offset: 0x00000EFD
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00002D05 File Offset: 0x00000F05
		public new string Source
		{
			get
			{
				return this._Worker;
			}
			set
			{
				if (Operators.CompareString(value, "", false) == 0)
				{
					value = null;
				}
				if (Operators.CompareString(this._Worker, value, false) != 0)
				{
					this._Worker = value;
					if (base.IsInitialized)
					{
						this.Load();
					}
				}
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00002D3C File Offset: 0x00000F3C
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00002D44 File Offset: 0x00000F44
		public string FallbackSource
		{
			get
			{
				return this.m_Server;
			}
			set
			{
				this.m_Server = value;
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00002D4D File Offset: 0x00000F4D
		public string RevertParser()
		{
			return this._Resolver;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00002D55 File Offset: 0x00000F55
		public void RunBroadcaster(string value)
		{
			this._Resolver = value;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00002D5E File Offset: 0x00000F5E
		public string ManageBroadcaster()
		{
			return this._Status;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0001289C File Offset: 0x00010A9C
		public void SetBroadcaster(string value)
		{
			if (Operators.CompareString(value, "", false) == 0)
			{
				value = null;
			}
			if (Operators.CompareString(this._Status, value, false) != 0)
			{
				this._Status = value;
				try
				{
					MyBitmap Bitmap = (value == null) ? null : new MyBitmap(value);
					ModBase.RunInUiWait(delegate()
					{
						this.Source = Bitmap;
					});
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, string.Format("加载图片失败（{0}）", value), ModBase.LogLevel.Debug, "出现错误");
					try
					{
						if (value.StartsWithF(ModBase.m_DecoratorRepository, false) && File.Exists(value))
						{
							File.Delete(value);
						}
					}
					catch (Exception ex2)
					{
					}
				}
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00012970 File Offset: 0x00010B70
		private void Load()
		{
			MyImage._Closure$__24-0 CS$<>8__locals1 = new MyImage._Closure$__24-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Me = this;
			if (this.Source == null)
			{
				this.SetBroadcaster(null);
				return;
			}
			if (!this.Source.StartsWithF("http", false))
			{
				this.SetBroadcaster(this.Source);
				return;
			}
			CS$<>8__locals1.$VB$Local_Url = this.Source;
			CS$<>8__locals1.$VB$Local_Retried = false;
			CS$<>8__locals1.$VB$Local_TempPath = MyImage.GetTempPath(CS$<>8__locals1.$VB$Local_Url);
			CS$<>8__locals1.$VB$Local_TempFile = new FileInfo(CS$<>8__locals1.$VB$Local_TempPath);
			CS$<>8__locals1.$VB$Local_EnableCache = this.EnableCache;
			if (CS$<>8__locals1.$VB$Local_EnableCache && CS$<>8__locals1.$VB$Local_TempFile.Exists)
			{
				this.SetBroadcaster(CS$<>8__locals1.$VB$Local_TempPath);
				if (DateTime.Now - CS$<>8__locals1.$VB$Local_TempFile.LastWriteTime < this.exporter)
				{
					return;
				}
			}
			ModBase.RunInNewThread(delegate
			{
				MyImage._Closure$__24-1 CS$<>8__locals2 = new MyImage._Closure$__24-1(CS$<>8__locals2);
				CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2 = CS$<>8__locals1;
				CS$<>8__locals2.$VB$Local_TempDownloadingPath = null;
				try
				{
					IL_15:
					CS$<>8__locals1.$VB$Me.SetBroadcaster(CS$<>8__locals1.$VB$Me.RevertParser());
					CS$<>8__locals2.$VB$Local_TempDownloadingPath = CS$<>8__locals1.$VB$Local_TempPath + Conversions.ToString(ModBase.RandomInteger(0, 10000000));
					Directory.CreateDirectory(ModBase.GetPathFromFullPath(CS$<>8__locals1.$VB$Local_TempPath));
					using (WebClient webClient = new WebClient())
					{
						webClient.DownloadFile(CS$<>8__locals1.$VB$Local_Url, CS$<>8__locals2.$VB$Local_TempDownloadingPath);
					}
					if (Operators.CompareString(CS$<>8__locals1.$VB$Local_Url, CS$<>8__locals1.$VB$Me.Source, false) != 0 && Operators.CompareString(CS$<>8__locals1.$VB$Local_Url, CS$<>8__locals1.$VB$Me.FallbackSource, false) != 0)
					{
						File.Delete(CS$<>8__locals2.$VB$Local_TempDownloadingPath);
					}
					else if (CS$<>8__locals1.$VB$Local_EnableCache)
					{
						if (File.Exists(CS$<>8__locals1.$VB$Local_TempPath))
						{
							File.Delete(CS$<>8__locals1.$VB$Local_TempPath);
						}
						FileSystem.Rename(CS$<>8__locals2.$VB$Local_TempDownloadingPath, CS$<>8__locals1.$VB$Local_TempPath);
						ModBase.RunInUi((CS$<>8__locals1.$I1 == null) ? (CS$<>8__locals1.$I1 = delegate()
						{
							CS$<>8__locals1.$VB$Me.SetBroadcaster(CS$<>8__locals1.$VB$Local_TempPath);
						}) : CS$<>8__locals1.$I1, false);
					}
					else
					{
						ModBase.RunInUiWait(delegate()
						{
							CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Me.SetBroadcaster(CS$<>8__locals2.$VB$Local_TempDownloadingPath);
						});
						File.Delete(CS$<>8__locals2.$VB$Local_TempDownloadingPath);
					}
				}
				catch (Exception ex)
				{
					try
					{
						if (CS$<>8__locals1.$VB$Local_TempPath != null)
						{
							File.Delete(CS$<>8__locals1.$VB$Local_TempPath);
						}
						if (CS$<>8__locals2.$VB$Local_TempDownloadingPath != null)
						{
							File.Delete(CS$<>8__locals2.$VB$Local_TempDownloadingPath);
						}
					}
					catch (Exception ex2)
					{
					}
					if (!CS$<>8__locals1.$VB$Local_Retried)
					{
						ModBase.Log(ex, string.Format("下载图片可重试地失败（{0}）", CS$<>8__locals1.$VB$Local_Url), ModBase.LogLevel.Developer, "出现错误");
						CS$<>8__locals1.$VB$Local_Retried = true;
						CS$<>8__locals1.$VB$Local_Url = (CS$<>8__locals1.$VB$Me.FallbackSource ?? CS$<>8__locals1.$VB$Me.Source);
						if (CS$<>8__locals1.$VB$Local_Url == null)
						{
							CS$<>8__locals1.$VB$Me.SetBroadcaster(null);
						}
						else
						{
							if (CS$<>8__locals1.$VB$Local_Url.StartsWithF("http", false))
							{
								CS$<>8__locals1.$VB$Local_TempPath = MyImage.GetTempPath(CS$<>8__locals1.$VB$Local_Url);
								CS$<>8__locals1.$VB$Local_TempFile = new FileInfo(CS$<>8__locals1.$VB$Local_TempPath);
								if (CS$<>8__locals1.$VB$Local_EnableCache && CS$<>8__locals1.$VB$Local_TempFile.Exists)
								{
									CS$<>8__locals1.$VB$Me.SetBroadcaster(CS$<>8__locals1.$VB$Local_TempPath);
									if (DateTime.Now - CS$<>8__locals1.$VB$Local_TempFile.CreationTime < CS$<>8__locals1.$VB$Me.exporter)
									{
										return;
									}
								}
								if (Operators.CompareString(CS$<>8__locals1.$VB$Me.Source, CS$<>8__locals1.$VB$Local_Url, false) == 0)
								{
									Thread.Sleep(1000);
								}
								goto IL_15;
							}
							CS$<>8__locals1.$VB$Me.SetBroadcaster(CS$<>8__locals1.$VB$Local_Url);
						}
					}
					else
					{
						ModBase.Log(ex, string.Format("下载图片失败（{0}）", CS$<>8__locals1.$VB$Local_Url), ModBase.LogLevel.Hint, "出现错误");
					}
				}
			}, "MyImage PicLoader " + Conversions.ToString(ModBase.GetUuid()) + "#", ThreadPriority.BelowNormal);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00002D66 File Offset: 0x00000F66
		public static string GetTempPath(string Url)
		{
			return string.Format("{0}MyImage\\{1}.png", ModBase.m_DecoratorRepository, ModBase.GetHash(Url));
		}

		// Token: 0x04000084 RID: 132
		public TimeSpan exporter;

		// Token: 0x04000085 RID: 133
		public static readonly DependencyProperty _Importer = DependencyProperty.Register("EnableCache", typeof(bool), typeof(MyImage), new PropertyMetadata(true));

		// Token: 0x04000086 RID: 134
		private string _Worker;

		// Token: 0x04000087 RID: 135
		public static readonly DependencyProperty connection = DependencyProperty.Register("Source", typeof(string), typeof(MyImage), new PropertyMetadata(delegate(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			if (sender != null)
			{
				((MyImage)sender).Source = e.NewValue.ToString();
			}
		}));

		// Token: 0x04000088 RID: 136
		private string m_Server;

		// Token: 0x04000089 RID: 137
		private string _Resolver;

		// Token: 0x0400008A RID: 138
		private string _Status;
	}
}
