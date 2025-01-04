using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Imazen.WebP;
using PCL.My;

namespace PCL
{
	// Token: 0x020001F0 RID: 496
	public class MyBitmap
	{
		// Token: 0x06001760 RID: 5984 RVA: 0x000976CC File Offset: 0x000958CC
		public static implicit operator MyBitmap(Image Image)
		{
			MyBitmap result;
			if (Image == null)
			{
				result = null;
			}
			else
			{
				result = new MyBitmap(Image);
			}
			return result;
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x000976E8 File Offset: 0x000958E8
		public static implicit operator Image(MyBitmap Image)
		{
			Image result;
			if (Image == null)
			{
				result = null;
			}
			else
			{
				result = Image._ContainerIterator;
			}
			return result;
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00097704 File Offset: 0x00095904
		public static implicit operator MyBitmap(ImageSource Image)
		{
			MyBitmap result;
			if (Image == null)
			{
				result = null;
			}
			else
			{
				result = new MyBitmap(Image);
			}
			return result;
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00097720 File Offset: 0x00095920
		public static implicit operator ImageSource(MyBitmap Image)
		{
			ImageSource result;
			if (Image == null)
			{
				result = null;
			}
			else
			{
				Bitmap containerIterator = Image._ContainerIterator;
				Rectangle rect = new Rectangle(0, 0, containerIterator.Width, containerIterator.Height);
				BitmapData bitmapData = containerIterator.LockBits(rect, ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				try
				{
					int bufferSize = checked(rect.Width * rect.Height * 4);
					result = BitmapSource.Create(containerIterator.Width, containerIterator.Height, (double)containerIterator.HorizontalResolution, (double)containerIterator.VerticalResolution, PixelFormats.Bgra32, null, bitmapData.Scan0, bufferSize, bitmapData.Stride);
				}
				finally
				{
					containerIterator.UnlockBits(bitmapData);
				}
			}
			return result;
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x000977C0 File Offset: 0x000959C0
		public static implicit operator MyBitmap(Bitmap Image)
		{
			MyBitmap result;
			if (Image == null)
			{
				result = null;
			}
			else
			{
				result = new MyBitmap(Image);
			}
			return result;
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x000977DC File Offset: 0x000959DC
		public static implicit operator Bitmap(MyBitmap Image)
		{
			Bitmap result;
			if (Image == null)
			{
				result = null;
			}
			else
			{
				result = Image._ContainerIterator;
			}
			return result;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x000977F8 File Offset: 0x000959F8
		public static implicit operator MyBitmap(ImageBrush Image)
		{
			MyBitmap result;
			if (Image == null)
			{
				result = null;
			}
			else
			{
				result = new MyBitmap(Image);
			}
			return result;
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x00097814 File Offset: 0x00095A14
		public static implicit operator ImageBrush(MyBitmap Image)
		{
			ImageBrush result;
			if (Image == null)
			{
				result = null;
			}
			else
			{
				result = new ImageBrush(new MyBitmap(Image._ContainerIterator));
			}
			return result;
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00002411 File Offset: 0x00000611
		public MyBitmap()
		{
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00097840 File Offset: 0x00095A40
		public MyBitmap(string FilePathOrResourceName)
		{
			try
			{
				FilePathOrResourceName = FilePathOrResourceName.Replace("pack://application:,,,/images/", ModBase.m_SerializerRepository);
				if (FilePathOrResourceName.StartsWithF(ModBase.m_SerializerRepository, false))
				{
					if (MyBitmap.m_InterceptorIterator.ContainsKey(FilePathOrResourceName))
					{
						this._ContainerIterator = MyBitmap.m_InterceptorIterator[FilePathOrResourceName]._ContainerIterator;
					}
					else
					{
						this._ContainerIterator = new MyBitmap((ImageSource)new ImageSourceConverter().ConvertFromString(FilePathOrResourceName));
						MyBitmap.m_InterceptorIterator.TryAdd(FilePathOrResourceName, this._ContainerIterator);
					}
				}
				else
				{
					using (FileStream fileStream = new FileStream(FilePathOrResourceName, FileMode.Open))
					{
						byte[] array = new byte[2];
						fileStream.Read(array, 0, 2);
						fileStream.Seek(0L, SeekOrigin.Begin);
						if (array[0] == 82 && array[1] == 73)
						{
							byte[] array2 = new byte[checked((int)(fileStream.Length - 1L) + 1)];
							fileStream.Read(array2, 0, array2.Length);
							this._ContainerIterator = MyBitmap.WebPDecoder.DecodeFromBytes(array2);
						}
						else
						{
							this._ContainerIterator = new Bitmap(fileStream);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this._ContainerIterator = (Bitmap)MyWpfExtension.RunParser().TryFindResource(FilePathOrResourceName);
				if (this._ContainerIterator == null)
				{
					this._ContainerIterator = new Bitmap(1, 1);
					throw new Exception(string.Format("加载 MyBitmap 失败（{0}）", FilePathOrResourceName), ex);
				}
				ModBase.Log(ex, string.Format("指定类型有误的 MyBitmap 加载（{0}）", FilePathOrResourceName), ModBase.LogLevel.Developer, "出现错误");
			}
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x000979F4 File Offset: 0x00095BF4
		public MyBitmap(ImageSource Image)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new PngBitmapEncoder
				{
					Frames = 
					{
						BitmapFrame.Create((BitmapSource)Image)
					}
				}.Save(memoryStream);
				this._ContainerIterator = new Bitmap(memoryStream);
			}
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0000D147 File Offset: 0x0000B347
		public MyBitmap(Image Image)
		{
			this._ContainerIterator = (Bitmap)Image;
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0000D15C File Offset: 0x0000B35C
		public MyBitmap(Bitmap Image)
		{
			this._ContainerIterator = Image;
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00097A58 File Offset: 0x00095C58
		public MyBitmap(ImageBrush Image)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new BmpBitmapEncoder
				{
					Frames = 
					{
						BitmapFrame.Create((BitmapSource)Image.ImageSource)
					}
				}.Save(memoryStream);
				this._ContainerIterator = new Bitmap(memoryStream);
			}
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00097AC0 File Offset: 0x00095CC0
		public MyBitmap Clip(int X, int Y, int Width, int Height)
		{
			Bitmap bitmap = new Bitmap(Width, Height, this._ContainerIterator.PixelFormat);
			bitmap.SetResolution(this._ContainerIterator.HorizontalResolution, this._ContainerIterator.VerticalResolution);
			checked
			{
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
					graphics.TranslateTransform((float)(0 - X), (float)(0 - Y));
					graphics.DrawImage(this._ContainerIterator, new Rectangle(0, 0, this._ContainerIterator.Width, this._ContainerIterator.Height));
				}
				return bitmap;
			}
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0000D16C File Offset: 0x0000B36C
		public MyBitmap RotateFlip(RotateFlipType Type)
		{
			Bitmap bitmap = new Bitmap(this._ContainerIterator);
			bitmap.SetResolution(this._ContainerIterator.HorizontalResolution, this._ContainerIterator.VerticalResolution);
			bitmap.RotateFlip(Type);
			return bitmap;
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00097B64 File Offset: 0x00095D64
		public void Save(string FilePath)
		{
			BitmapEncoder bitmapEncoder = new PngBitmapEncoder();
			bitmapEncoder.Frames.Add(BitmapFrame.Create((BitmapSource)this));
			using (FileStream fileStream = new FileStream(FilePath, FileMode.Create))
			{
				bitmapEncoder.Save(fileStream);
			}
		}

		// Token: 0x04000BD9 RID: 3033
		public static ConcurrentDictionary<string, MyBitmap> m_InterceptorIterator = new ConcurrentDictionary<string, MyBitmap>();

		// Token: 0x04000BDA RID: 3034
		public Bitmap _ContainerIterator;

		// Token: 0x020001F1 RID: 497
		private class WebPDecoder
		{
			// Token: 0x06001772 RID: 6002 RVA: 0x0000D1A1 File Offset: 0x0000B3A1
			public static Bitmap DecodeFromBytes(byte[] Bytes)
			{
				if (ModBase.m_StubRepository)
				{
					throw new Exception("不支持在 32 位系统下加载 WebP 图片。");
				}
				return new SimpleDecoder().DecodeFromBytes(Bytes, (long)Bytes.Length);
			}
		}
	}
}
