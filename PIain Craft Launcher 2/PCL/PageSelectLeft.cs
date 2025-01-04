﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x0200018F RID: 399
	[DesignerGenerated]
	public class PageSelectLeft : MyPageLeft, IRefreshable, IComponentConnector
	{
		// Token: 0x0600101F RID: 4127 RVA: 0x00009D98 File Offset: 0x00007F98
		public PageSelectLeft()
		{
			base.Initialized += this.PageSelectLeft_Initialized;
			base.Loaded += this.PageSelectLeft_Loaded;
			this.managerMapper = true;
			this.InitializeComponent();
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00009DD2 File Offset: 0x00007FD2
		private void PageSelectLeft_Initialized(object sender, EventArgs e)
		{
			ModMinecraft.m_CreatorTests.PreviewFinish += delegate(ModLoader.LoaderBase a0)
			{
				this._Lambda$__1-0();
			};
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x00009DEA File Offset: 0x00007FEA
		private void PageSelectLeft_Loaded(object sender, RoutedEventArgs e)
		{
			if (this.managerMapper)
			{
				this.McFolderListUI();
			}
			this.managerMapper = false;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00075A98 File Offset: 0x00073C98
		private void McFolderListUI()
		{
			checked
			{
				try
				{
					if (this.m_ModelMapper != null && Enumerable.SequenceEqual<ModMinecraft.McFolder>(this.m_ModelMapper, ModMinecraft.messageTests))
					{
						bool flag = true;
						int num = this.m_ModelMapper.Count - 1;
						int i = 0;
						while (i <= num)
						{
							if (this.m_ModelMapper[i].Equals(ModMinecraft.messageTests[i]))
							{
								i++;
							}
							else
							{
								flag = false;
								IL_5F:
								if (flag)
								{
									return;
								}
								goto IL_67;
							}
						}
						goto IL_5F;
					}
					IL_67:
					this.m_ModelMapper = ModMinecraft.messageTests;
					ModMain._InvocationIterator.PanList.Children.Clear();
					ModMain._InvocationIterator.PanList.Children.Add(new TextBlock
					{
						Text = "文件夹列表",
						Margin = new Thickness(13.0, 18.0, 5.0, 4.0),
						Opacity = 0.6,
						FontSize = 12.0
					});
					foreach (ModMinecraft.McFolder mcFolder in ModMinecraft.messageTests.ToArray())
					{
						PageSelectLeft._Closure$__4-0 CS$<>8__locals1 = new PageSelectLeft._Closure$__4-0(CS$<>8__locals1);
						CS$<>8__locals1.$VB$Local_ContMenu = null;
						switch (mcFolder.Type)
						{
						case ModMinecraft.McFolderType.Original:
						{
							PageSelectLeft._Closure$__4-0 CS$<>8__locals2 = CS$<>8__locals1;
							XElement xelement = new XElement(XName.Get("ContextMenu", "http://schemas.microsoft.com/winfx/2006/xaml/presentation"));
							xelement.Add(new XAttribute(XName.Get("xmlns", ""), "http://schemas.microsoft.com/winfx/2006/xaml/presentation"));
							xelement.Add(new XAttribute(XName.Get("x", "http://www.w3.org/2000/xmlns/"), "http://schemas.microsoft.com/winfx/2006/xaml"));
							xelement.Add(new XAttribute(XName.Get("local", "http://www.w3.org/2000/xmlns/"), "clr-namespace:PCL;assembly=Plain Craft Launcher 2"));
							XElement xelement2 = new XElement(XName.Get("MyMenuItem", "clr-namespace:PCL;assembly=Plain Craft Launcher 2"));
							xelement2.Add(new XAttribute(XName.Get("Name", "http://schemas.microsoft.com/winfx/2006/xaml"), "Rename"));
							xelement2.Add(new XAttribute(XName.Get("Header", ""), "重命名"));
							xelement2.Add(new XAttribute(XName.Get("Padding", ""), "0,2,0,0"));
							xelement2.Add(new XAttribute(XName.Get("Icon", ""), "F1 M 53.2929,21.2929L 54.7071,22.7071C 56.4645,24.4645 56.4645,27.3137 54.7071,29.0711L 52.2323,31.5459L 44.4541,23.7677L 46.9289,21.2929C 48.6863,19.5355 51.5355,19.5355 53.2929,21.2929 Z M 31.7262,52.052L 23.948,44.2738L 43.0399,25.182L 50.818,32.9601L 31.7262,52.052 Z M 23.2409,47.1023L 28.8977,52.7591L 21.0463,54.9537L 23.2409,47.1023 Z"));
							xelement.Add(xelement2);
							XElement xelement3 = new XElement(XName.Get("MyMenuItem", "clr-namespace:PCL;assembly=Plain Craft Launcher 2"));
							xelement3.Add(new XAttribute(XName.Get("Name", "http://schemas.microsoft.com/winfx/2006/xaml"), "Open"));
							xelement3.Add(new XAttribute(XName.Get("Header", ""), "打开"));
							xelement3.Add(new XAttribute(XName.Get("Icon", ""), "F1 M 19,50L 28,34L 63,34L 54,50L 19,50 Z M 19,28.0001L 35,28C 36,25 37.4999,24.0001 37.4999,24.0001L 48.75,24C 49.3023,24 50,24.6977 50,25.25L 50,28L 54,28.0001L 54,32L 27,32L 19,46.4L 19,28.0001 Z"));
							xelement.Add(xelement3);
							XElement xelement4 = new XElement(XName.Get("MyMenuItem", "clr-namespace:PCL;assembly=Plain Craft Launcher 2"));
							xelement4.Add(new XAttribute(XName.Get("Name", "http://schemas.microsoft.com/winfx/2006/xaml"), "Refresh"));
							xelement4.Add(new XAttribute(XName.Get("Header", ""), "刷新"));
							xelement4.Add(new XAttribute(XName.Get("Icon", ""), "F1 M 38,20.5833C 42.9908,20.5833 47.4912,22.6825 50.6667,26.046L 50.6667,17.4167L 55.4166,22.1667L 55.4167,34.8333L 42.75,34.8333L 38,30.0833L 46.8512,30.0833C 44.6768,27.6539 41.517,26.125 38,26.125C 31.9785,26.125 27.0037,30.6068 26.2296,36.4167L 20.6543,36.4167C 21.4543,27.5397 28.9148,20.5833 38,20.5833 Z M 38,49.875C 44.0215,49.875 48.9963,45.3932 49.7703,39.5833L 55.3457,39.5833C 54.5457,48.4603 47.0852,55.4167 38,55.4167C 33.0092,55.4167 28.5088,53.3175 25.3333,49.954L 25.3333,58.5833L 20.5833,53.8333L 20.5833,41.1667L 33.25,41.1667L 38,45.9167L 29.1487,45.9167C 31.3231,48.3461 34.483,49.875 38,49.875 Z"));
							xelement.Add(xelement4);
							XElement xelement5 = new XElement(XName.Get("MyMenuItem", "clr-namespace:PCL;assembly=Plain Craft Launcher 2"));
							xelement5.Add(new XAttribute(XName.Get("Name", "http://schemas.microsoft.com/winfx/2006/xaml"), "Delete"));
							xelement5.Add(new XAttribute(XName.Get("Header", ""), "删除"));
							xelement5.Add(new XAttribute(XName.Get("Padding", ""), "0,0,0,2"));
							xelement5.Add(new XAttribute(XName.Get("Icon", ""), "F1 M 26.9166,22.1667L 37.9999,33.25L 49.0832,22.1668L 53.8332,26.9168L 42.7499,38L 53.8332,49.0834L 49.0833,53.8334L 37.9999,42.75L 26.9166,53.8334L 22.1666,49.0833L 33.25,38L 22.1667,26.9167L 26.9166,22.1667 Z "));
							xelement.Add(xelement5);
							CS$<>8__locals2.$VB$Local_ContMenu = (ContextMenu)ModBase.GetObjectFromXML(xelement);
							break;
						}
						case ModMinecraft.McFolderType.RenamedOriginal:
						{
							PageSelectLeft._Closure$__4-0 CS$<>8__locals3 = CS$<>8__locals1;
							XElement xelement6 = new XElement(XName.Get("ContextMenu", "http://schemas.microsoft.com/winfx/2006/xaml/presentation"));
							xelement6.Add(new XAttribute(XName.Get("xmlns", ""), "http://schemas.microsoft.com/winfx/2006/xaml/presentation"));
							xelement6.Add(new XAttribute(XName.Get("x", "http://www.w3.org/2000/xmlns/"), "http://schemas.microsoft.com/winfx/2006/xaml"));
							xelement6.Add(new XAttribute(XName.Get("local", "http://www.w3.org/2000/xmlns/"), "clr-namespace:PCL;assembly=Plain Craft Launcher 2"));
							XElement xelement7 = new XElement(XName.Get("MyMenuItem", "clr-namespace:PCL;assembly=Plain Craft Launcher 2"));
							xelement7.Add(new XAttribute(XName.Get("Name", "http://schemas.microsoft.com/winfx/2006/xaml"), "Remove"));
							xelement7.Add(new XAttribute(XName.Get("Header", ""), "复原名称"));
							xelement7.Add(new XAttribute(XName.Get("Padding", ""), "0,2,0,0"));
							xelement7.Add(new XAttribute(XName.Get("Icon", ""), "F1 M 53.2929,21.2929L 54.7071,22.7071C 56.4645,24.4645 56.4645,27.3137 54.7071,29.0711L 52.2323,31.5459L 44.4541,23.7677L 46.9289,21.2929C 48.6863,19.5355 51.5355,19.5355 53.2929,21.2929 Z M 31.7262,52.052L 23.948,44.2738L 43.0399,25.182L 50.818,32.9601L 31.7262,52.052 Z M 23.2409,47.1023L 28.8977,52.7591L 21.0463,54.9537L 23.2409,47.1023 Z"));
							xelement6.Add(xelement7);
							XElement xelement8 = new XElement(XName.Get("MyMenuItem", "clr-namespace:PCL;assembly=Plain Craft Launcher 2"));
							xelement8.Add(new XAttribute(XName.Get("Name", "http://schemas.microsoft.com/winfx/2006/xaml"), "Rename"));
							xelement8.Add(new XAttribute(XName.Get("Header", ""), "重命名"));
							xelement8.Add(new XAttribute(XName.Get("Icon", ""), "F1 M 53.2929,21.2929L 54.7071,22.7071C 56.4645,24.4645 56.4645,27.3137 54.7071,29.0711L 52.2323,31.5459L 44.4541,23.7677L 46.9289,21.2929C 48.6863,19.5355 51.5355,19.5355 53.2929,21.2929 Z M 31.7262,52.052L 23.948,44.2738L 43.0399,25.182L 50.818,32.9601L 31.7262,52.052 Z M 23.2409,47.1023L 28.8977,52.7591L 21.0463,54.9537L 23.2409,47.1023 Z"));
							xelement6.Add(xelement8);
							XElement xelement9 = new XElement(XName.Get("MyMenuItem", "clr-namespace:PCL;assembly=Plain Craft Launcher 2"));
							xelement9.Add(new XAttribute(XName.Get("Name", "http://schemas.microsoft.com/winfx/2006/xaml"), "Open"));
							xelement9.Add(new XAttribute(XName.Get("Header", ""), "打开"));
							xelement9.Add(new XAttribute(XName.Get("Icon", ""), "F1 M 19,50L 28,34L 63,34L 54,50L 19,50 Z M 19,28.0001L 35,28C 36,25 37.4999,24.0001 37.4999,24.0001L 48.75,24C 49.3023,24 50,24.6977 50,25.25L 50,28L 54,28.0001L 54,32L 27,32L 19,46.4L 19,28.0001 Z"));
							xelement6.Add(xelement9);
							XElement xelement10 = new XElement(XName.Get("MyMenuItem", "clr-namespace:PCL;assembly=Plain Craft Launcher 2"));
							xelement10.Add(new XAttribute(XName.Get("Name", "http://schemas.microsoft.com/winfx/2006/xaml"), "Refresh"));
							xelement10.Add(new XAttribute(XName.Get("Header", ""), "刷新"));
							xelement10.Add(new XAttribute(XName.Get("Icon", ""), "F1 M 38,20.5833C 42.9908,20.5833 47.4912,22.6825 50.6667,26.046L 50.6667,17.4167L 55.4166,22.1667L 55.4167,34.8333L 42.75,34.8333L 38,30.0833L 46.8512,30.0833C 44.6768,27.6539 41.517,26.125 38,26.125C 31.9785,26.125 27.0037,30.6068 26.2296,36.4167L 20.6543,36.4167C 21.4543,27.5397 28.9148,20.5833 38,20.5833 Z M 38,49.875C 44.0215,49.875 48.9963,45.3932 49.7703,39.5833L 55.3457,39.5833C 54.5457,48.4603 47.0852,55.4167 38,55.4167C 33.0092,55.4167 28.5088,53.3175 25.3333,49.954L 25.3333,58.5833L 20.5833,53.8333L 20.5833,41.1667L 33.25,41.1667L 38,45.9167L 29.1487,45.9167C 31.3231,48.3461 34.483,49.875 38,49.875 Z"));
							xelement6.Add(xelement10);
							XElement xelement11 = new XElement(XName.Get("MyMenuItem", "clr-namespace:PCL;assembly=Plain Craft Launcher 2"));
							xelement11.Add(new XAttribute(XName.Get("Name", "http://schemas.microsoft.com/winfx/2006/xaml"), "Delete"));
							xelement11.Add(new XAttribute(XName.Get("Header", ""), "删除"));
							xelement11.Add(new XAttribute(XName.Get("Padding", ""), "0,0,0,2"));
							xelement11.Add(new XAttribute(XName.Get("Icon", ""), "F1 M 26.9166,22.1667L 37.9999,33.25L 49.0832,22.1668L 53.8332,26.9168L 42.7499,38L 53.8332,49.0834L 49.0833,53.8334L 37.9999,42.75L 26.9166,53.8334L 22.1666,49.0833L 33.25,38L 22.1667,26.9167L 26.9166,22.1667 Z "));
							xelement6.Add(xelement11);
							CS$<>8__locals3.$VB$Local_ContMenu = (ContextMenu)ModBase.GetObjectFromXML(xelement6);
							break;
						}
						case ModMinecraft.McFolderType.Custom:
						{
							PageSelectLeft._Closure$__4-0 CS$<>8__locals4 = CS$<>8__locals1;
							XElement xelement12 = new XElement(XName.Get("ContextMenu", "http://schemas.microsoft.com/winfx/2006/xaml/presentation"));
							xelement12.Add(new XAttribute(XName.Get("xmlns", ""), "http://schemas.microsoft.com/winfx/2006/xaml/presentation"));
							xelement12.Add(new XAttribute(XName.Get("x", "http://www.w3.org/2000/xmlns/"), "http://schemas.microsoft.com/winfx/2006/xaml"));
							xelement12.Add(new XAttribute(XName.Get("local", "http://www.w3.org/2000/xmlns/"), "clr-namespace:PCL;assembly=Plain Craft Launcher 2"));
							XElement xelement13 = new XElement(XName.Get("MyMenuItem", "clr-namespace:PCL;assembly=Plain Craft Launcher 2"));
							xelement13.Add(new XAttribute(XName.Get("Name", "http://schemas.microsoft.com/winfx/2006/xaml"), "Rename"));
							xelement13.Add(new XAttribute(XName.Get("Header", ""), "重命名"));
							xelement13.Add(new XAttribute(XName.Get("Padding", ""), "0,2,0,0"));
							xelement13.Add(new XAttribute(XName.Get("Icon", ""), "F1 M 53.2929,21.2929L 54.7071,22.7071C 56.4645,24.4645 56.4645,27.3137 54.7071,29.0711L 52.2323,31.5459L 44.4541,23.7677L 46.9289,21.2929C 48.6863,19.5355 51.5355,19.5355 53.2929,21.2929 Z M 31.7262,52.052L 23.948,44.2738L 43.0399,25.182L 50.818,32.9601L 31.7262,52.052 Z M 23.2409,47.1023L 28.8977,52.7591L 21.0463,54.9537L 23.2409,47.1023 Z"));
							xelement12.Add(xelement13);
							XElement xelement14 = new XElement(XName.Get("MyMenuItem", "clr-namespace:PCL;assembly=Plain Craft Launcher 2"));
							xelement14.Add(new XAttribute(XName.Get("Name", "http://schemas.microsoft.com/winfx/2006/xaml"), "Open"));
							xelement14.Add(new XAttribute(XName.Get("Header", ""), "打开"));
							xelement14.Add(new XAttribute(XName.Get("Icon", ""), "F1 M 19,50L 28,34L 63,34L 54,50L 19,50 Z M 19,28.0001L 35,28C 36,25 37.4999,24.0001 37.4999,24.0001L 48.75,24C 49.3023,24 50,24.6977 50,25.25L 50,28L 54,28.0001L 54,32L 27,32L 19,46.4L 19,28.0001 Z"));
							xelement12.Add(xelement14);
							XElement xelement15 = new XElement(XName.Get("MyMenuItem", "clr-namespace:PCL;assembly=Plain Craft Launcher 2"));
							xelement15.Add(new XAttribute(XName.Get("Name", "http://schemas.microsoft.com/winfx/2006/xaml"), "Refresh"));
							xelement15.Add(new XAttribute(XName.Get("Header", ""), "刷新"));
							xelement15.Add(new XAttribute(XName.Get("Icon", ""), "F1 M 38,20.5833C 42.9908,20.5833 47.4912,22.6825 50.6667,26.046L 50.6667,17.4167L 55.4166,22.1667L 55.4167,34.8333L 42.75,34.8333L 38,30.0833L 46.8512,30.0833C 44.6768,27.6539 41.517,26.125 38,26.125C 31.9785,26.125 27.0037,30.6068 26.2296,36.4167L 20.6543,36.4167C 21.4543,27.5397 28.9148,20.5833 38,20.5833 Z M 38,49.875C 44.0215,49.875 48.9963,45.3932 49.7703,39.5833L 55.3457,39.5833C 54.5457,48.4603 47.0852,55.4167 38,55.4167C 33.0092,55.4167 28.5088,53.3175 25.3333,49.954L 25.3333,58.5833L 20.5833,53.8333L 20.5833,41.1667L 33.25,41.1667L 38,45.9167L 29.1487,45.9167C 31.3231,48.3461 34.483,49.875 38,49.875 Z"));
							xelement12.Add(xelement15);
							XElement xelement16 = new XElement(XName.Get("MyMenuItem", "clr-namespace:PCL;assembly=Plain Craft Launcher 2"));
							xelement16.Add(new XAttribute(XName.Get("Name", "http://schemas.microsoft.com/winfx/2006/xaml"), "Remove"));
							xelement16.Add(new XAttribute(XName.Get("Header", ""), "移出列表"));
							xelement16.Add(new XAttribute(XName.Get("Icon", ""), "F1 M 23.3428,25.205L 23.3805,25.4461C 23.9229,27.177 30.261,29.0992 38,29.0992C 45.7386,29.0992 52.0765,27.1771 52.6194,25.4463L 52.6571,25.205C 52.6571,23.3616 46.0949,21.3109 38,21.3109C 29.9051,21.3109 23.3428,23.3616 23.3428,25.205 Z M 23.3428,53.0204L 19.1571,26.2111C 19.0534,25.8817 19,25.5459 19,25.205C 19,20.9036 27.5066,17.4167 38,17.4167C 48.4934,17.4167 57,20.9036 57,25.205C 57,25.5459 56.9466,25.8818 56.8429,26.2112L 52.6571,53.0204L 52.5974,53.0204C 51.9241,56.1393 45.6457,58.5833 38,58.5833C 30.3543,58.5833 24.076,56.1393 23.4026,53.0204L 23.3428,53.0204 Z M 51.8228,30.5485C 48.3585,32.0537 43.4469,32.9933 38,32.9933C 32.5531,32.9933 27.6415,32.0537 24.1771,30.5484L 27.5988,52.464L 27.6857,52.464C 27.6857,53.3857 32.3036,54.6892 38,54.6892C 43.6964,54.6892 48.3143,53.3857 48.3143,52.464L 48.4011,52.464L 51.8228,30.5485 Z "));
							xelement12.Add(xelement16);
							XElement xelement17 = new XElement(XName.Get("MyMenuItem", "clr-namespace:PCL;assembly=Plain Craft Launcher 2"));
							xelement17.Add(new XAttribute(XName.Get("Name", "http://schemas.microsoft.com/winfx/2006/xaml"), "Delete"));
							xelement17.Add(new XAttribute(XName.Get("Header", ""), "删除"));
							xelement17.Add(new XAttribute(XName.Get("Padding", ""), "0,0,0,2"));
							xelement17.Add(new XAttribute(XName.Get("Icon", ""), "F1 M 26.9166,22.1667L 37.9999,33.25L 49.0832,22.1668L 53.8332,26.9168L 42.7499,38L 53.8332,49.0834L 49.0833,53.8334L 37.9999,42.75L 26.9166,53.8334L 22.1666,49.0833L 33.25,38L 22.1667,26.9167L 26.9166,22.1667 Z "));
							xelement12.Add(xelement17);
							CS$<>8__locals4.$VB$Local_ContMenu = (ContextMenu)ModBase.GetObjectFromXML(xelement12);
							break;
						}
						}
						if ((mcFolder.Type == ModMinecraft.McFolderType.Original || mcFolder.Type == ModMinecraft.McFolderType.RenamedOriginal) && Operators.CompareString(mcFolder.Path, ModBase.Path + ".minecraft\\", false) == 0 && ModMinecraft.messageTests.Count == 1)
						{
							((MyMenuItem)CS$<>8__locals1.$VB$Local_ContMenu.FindName("Delete")).Header = "清空";
						}
						if (mcFolder.Type != ModMinecraft.McFolderType.Original)
						{
							((MyMenuItem)CS$<>8__locals1.$VB$Local_ContMenu.FindName("Remove")).AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(ModMain._InvocationIterator.Remove_Click));
						}
						((MyMenuItem)CS$<>8__locals1.$VB$Local_ContMenu.FindName("Open")).AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(ModMain._InvocationIterator.Open_Click));
						((MyMenuItem)CS$<>8__locals1.$VB$Local_ContMenu.FindName("Delete")).AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(ModMain._InvocationIterator.Delete_Click));
						((MyMenuItem)CS$<>8__locals1.$VB$Local_ContMenu.FindName("Rename")).AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(ModMain._InvocationIterator.Rename_Click));
						((MyMenuItem)CS$<>8__locals1.$VB$Local_ContMenu.FindName("Refresh")).AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(ModMain._InvocationIterator.Refresh_Click));
						CS$<>8__locals1.$VB$Local_NewItem = new MyListItem
						{
							IsScaleAnimationEnabled = false,
							Type = MyListItem.CheckType.RadioBox,
							MinPaddingRight = 30,
							Title = mcFolder.Name,
							Info = mcFolder.Path,
							Height = 40.0,
							ContextMenu = CS$<>8__locals1.$VB$Local_ContMenu,
							Tag = mcFolder
						};
						MyListItem $VB$Local_NewItem = CS$<>8__locals1.$VB$Local_NewItem;
						PageSelectLeft._Closure$__R4-1 CS$<>8__locals5 = new PageSelectLeft._Closure$__R4-1(CS$<>8__locals5);
						CS$<>8__locals5.$VB$NonLocal_2 = ModMain._InvocationIterator;
						$VB$Local_NewItem.Changed += delegate(object sender, ModBase.RouteEventArgs e)
						{
							CS$<>8__locals5.$VB$NonLocal_2.Folder_Change((MyListItem)sender, e);
						};
						MyIconButton myIconButton = new MyIconButton
						{
							Logo = "M651.946667 1001.813333c-22.186667 0-42.666667-10.24-61.44-27.306666-23.893333-23.893333-49.493333-35.84-75.093334-35.84-29.013333 0-56.32 11.946667-73.386666 30.72v3.413333c-17.066667 17.066667-42.666667 27.306667-66.56 27.306667h-6.826667c-6.826667 0-11.946667-1.706667-15.36-1.706667l-6.826667-1.706667c-64.853333-20.48-121.173333-54.613333-168.96-98.986666-29.013333-23.893333-37.546667-63.146667-25.6-95.573334 8.533333-23.893333 5.12-51.2-10.24-75.093333-15.36-27.306667-34.133333-40.96-59.733333-47.786667h-1.706667l-5.12-1.706666c-35.84-8.533333-61.44-34.133333-66.56-69.973334C1.706667 575.146667 0 537.6 0 512c0-32.426667 3.413333-63.146667 8.533333-93.866667v-6.826666l3.413334-8.533334c10.24-23.893333 23.893333-40.96 44.373333-51.2 5.12-3.413333 11.946667-6.826667 20.48-8.533333 27.306667-8.533333 51.2-25.6 63.146667-44.373333 13.653333-23.893333 17.066667-52.906667 10.24-81.92-11.946667-34.133333 0-71.68 30.72-93.866667 44.373333-37.546667 97.28-68.266667 158.72-93.866667l3.413333-1.706666c44.373333-13.653333 75.093333 3.413333 92.16 20.48 23.893333 23.893333 49.493333 35.84 75.093333 35.84 30.72 0 56.32-10.24 71.68-30.72l3.413334-3.413334c27.306667-27.306667 63.146667-35.84 93.866666-22.186666 63.146667 22.186667 117.76 54.613333 165.546667 97.28 29.013333 23.893333 37.546667 63.146667 25.6 95.573333-8.533333 23.893333-5.12 51.2 10.24 75.093333 15.36 27.306667 34.133333 40.96 59.733333 47.786667h1.706667l5.12 1.706667c35.84 8.533333 61.44 34.133333 66.56 71.68 6.826667 30.72 10.24 63.146667 11.946667 93.866666v3.413334c0 32.426667-3.413333 63.146667-8.533334 93.866666v6.826667l-3.413333 8.533333c-10.24 23.893333-23.893333 40.96-44.373333 51.2-5.12 3.413333-11.946667 6.826667-20.48 8.533334-27.306667 8.533333-51.2 25.6-63.146667 46.08-13.653333 23.893333-17.066667 52.906667-10.24 81.92 11.946667 35.84-1.706667 75.093333-30.72 95.573333-44.373333 35.84-95.573333 66.56-157.013333 92.16-15.36 3.413333-27.306667 3.413333-35.84 3.413333z m3.413333-83.626666z m1.706667 0zM517.12 853.333333c47.786667 0 93.866667 20.48 134.826667 59.733334 1.706667 1.706667 3.413333 1.706667 3.413333 3.413333 52.906667-22.186667 97.28-49.493333 136.533333-80.213333l1.706667-1.706667v-3.413333c-13.653333-52.906667-8.533333-104.106667 17.066667-148.48 23.893333-39.253333 64.853333-69.973333 114.346666-85.333334 1.706667 0 3.413333-1.706667 6.826667-6.826666 5.12-25.6 8.533333-51.2 8.533333-78.506667-1.706667-29.013333-3.413333-56.32-10.24-81.92v-5.12h-1.706666c-51.2-11.946667-90.453333-39.253333-119.466667-87.04-27.306667-44.373333-34.133333-100.693333-17.066667-148.48l-1.706666-1.706667h-3.413334c-39.253333-35.84-85.333333-63.146667-136.533333-80.213333H648.533333s-1.706667 1.706667-3.413333 1.706667c-32.426667 39.253333-80.213333 59.733333-136.533333 59.733333-47.786667 0-93.866667-20.48-134.826667-59.733333l-1.706667-1.706667h-1.706666c-54.613333 22.186667-98.986667 49.493333-136.533334 80.213333l-1.706666 1.706667v3.413333c13.653333 52.906667 8.533333 104.106667-17.066667 148.48-23.893333 39.253333-64.853333 69.973333-114.346667 85.333334-1.706667 0-3.413333 1.706667-6.826666 6.826666-6.826667 25.6-8.533333 51.2-8.533334 78.506667 0 30.72 3.413333 58.026667 6.826667 76.8l1.706667 5.12h1.706666c51.2 11.946667 90.453333 39.253333 119.466667 87.04 27.306667 44.373333 34.133333 100.693333 17.066667 148.48l1.706666 1.706667 1.706667 1.706666c37.546667 35.84 83.626667 63.146667 134.826667 80.213334 1.706667 0 3.413333 0 3.413333 1.706666h1.706667s1.706667 0 5.12-1.706666c34.133333-37.546667 81.92-59.733333 136.533333-59.733334z m-6.826667-146.773333c-110.933333 0-199.68-85.333333-199.68-196.266667 0-109.226667 87.04-196.266667 199.68-196.266666s199.68 85.333333 199.68 196.266666c-1.706667 109.226667-88.746667 196.266667-199.68 196.266667z m0-307.2c-63.146667 0-114.346667 49.493333-114.346666 110.933333 0 63.146667 49.493333 110.933333 114.346666 110.933334 30.72 0 59.733333-11.946667 80.213334-32.426667 20.48-20.48 32.426667-49.493333 32.426666-78.506667 0-63.146667-49.493333-110.933333-112.64-110.933333z",
							LogoScale = 1.1
						};
						myIconButton.Click += delegate(object sender, EventArgs e)
						{
							CS$<>8__locals1.$VB$Local_ContMenu.PlacementTarget = CS$<>8__locals1.$VB$Local_NewItem;
							CS$<>8__locals1.$VB$Local_ContMenu.IsOpen = true;
						};
						CS$<>8__locals1.$VB$Local_NewItem.Buttons = new MyIconButton[]
						{
							myIconButton
						};
						ModMain._InvocationIterator.PanList.Children.Add(CS$<>8__locals1.$VB$Local_NewItem);
						ModBase.Log("[Minecraft] 有效的 Minecraft 文件夹：" + mcFolder.Name + " > " + mcFolder.Path, ModBase.LogLevel.Normal, "出现错误");
					}
					ModMain._InvocationIterator.PanList.Children.Add(new TextBlock
					{
						Text = "添加或导入",
						Margin = new Thickness(13.0, 18.0, 5.0, 4.0),
						Opacity = 0.6,
						FontSize = 12.0
					});
					if (!Directory.Exists(ModBase.Path + ".minecraft\\"))
					{
						MyListItem myListItem = new MyListItem
						{
							IsScaleAnimationEnabled = false,
							Type = MyListItem.CheckType.Clickable,
							Title = "新建 .minecraft 文件夹",
							Height = 34.0,
							ToolTip = "在 PCL 当前所在文件夹下创建新的 .minecraft 文件夹",
							LogoScale = 0.9,
							Logo = "M103.331925 384.978025l25.805736 0L129.137661 161.847132c0-18.313088 14.905478-33.718963 33.718963-33.718963l0.969071 0 253.006318 0c10.82044 0 20.218484 4.797259 26.500561 12.257162l117.579929 126.753869 297.819966 0c18.297738 0 33.736359 15.179724 33.736359 33.977859l0 0.952698 0 82.909292 25.547863 0c18.538215 0 34.187637 15.179724 34.187637 33.977859 0 2.163269-0.469698 3.617387-0.469698 5.539156l-54.437843 432.971086c-1.210571 10.382465-7.007601 19.056008-14.968923 24.352641-6.249331 5.765307-14.680351 9.624195-23.595394 9.624195l-0.969071 0-694.906773 0c-9.155521 0-17.344017-3.858888-23.626094-9.155521-8.67252-5.765307-14.453177-14.939247-15.389502-25.758664L69.597613 423.040922c-2.165316-18.313088 10.868535-35.414581 29.665647-38.062897L103.331925 384.978025 103.331925 384.978025zM196.576609 384.978025 196.576609 384.978025l627.938546 0 0-49.625234L546.461371 335.352791l0 0c-9.400091 0-18.329461-4.117784-25.048489-11.110035L402.363486 196.067514 196.576609 196.067514 196.576609 384.978025 196.576609 384.978025zM879.469767 452.916347 879.469767 452.916347l-20.267603 0-0.469698 0-0.969071 0-694.906773 0-0.984421 0-20.218484 0 45.781696 366.728382 646.218888 0L879.469767 452.916347 879.469767 452.916347z"
						};
						ToolTipService.SetPlacement(myListItem, PlacementMode.Right);
						ToolTipService.SetHorizontalOffset(myListItem, -50.0);
						ToolTipService.SetVerticalOffset(myListItem, 2.5);
						ModMain._InvocationIterator.PanList.Children.Add(myListItem);
						MyListItem myListItem2 = myListItem;
						PageSelectLeft._Closure$__R4-2 CS$<>8__locals6 = new PageSelectLeft._Closure$__R4-2(CS$<>8__locals6);
						CS$<>8__locals6.$VB$NonLocal_3 = ModMain._InvocationIterator;
						myListItem2.Click += delegate(object sender, MouseButtonEventArgs e)
						{
							CS$<>8__locals6.$VB$NonLocal_3.Create_Click();
						};
					}
					MyListItem myListItem3 = new MyListItem
					{
						IsScaleAnimationEnabled = false,
						Type = MyListItem.CheckType.Clickable,
						Title = "添加已有文件夹",
						Height = 34.0,
						ToolTip = "将一个已有的 Minecraft 文件夹添加到列表",
						Logo = "M512.277 954.412c-118.89 0-230.659-46.078-314.73-129.73S67.12 629.666 67.12 511.222s46.327-229.744 130.398-313.427 195.82-129.73 314.73-129.73 230.659 46.078 314.72 129.73S957.397 392.81 957.397 511.183 911.078 740.96 826.97 824.642s-195.8 129.77-314.692 129.77z m0-822.784c-101.972 0-197.809 39.494-269.865 111.222s-111.7 166.997-111.7 268.373 39.653 196.695 111.67 268.335S410.246 890.78 512.248 890.78s197.809-39.484 269.865-111.222 111.7-166.998 111.67-268.374c-0.03-101.375-39.654-196.665-111.67-268.303S614.22 131.628 512.277 131.628z m222.585 347.8H544.073V288.64c-0.76-17.561-15.613-31.18-33.173-30.419-16.495 0.714-29.704 13.924-30.419 30.419v190.787H289.703c-17.56 0.761-31.179 15.614-30.419 33.174 0.715 16.494 13.924 29.703 30.42 30.418H480.48v190.788c0.761 17.56 15.614 31.179 33.174 30.419 16.494-0.715 29.703-13.925 30.418-30.42V543.02h190.788c17.56 0.762 32.413-12.857 33.173-30.418 0.762-17.561-12.858-32.414-30.419-33.174a31.683 31.683 0 0 0-2.753 0z"
					};
					ToolTipService.SetPlacement(myListItem3, PlacementMode.Right);
					ToolTipService.SetHorizontalOffset(myListItem3, -50.0);
					ToolTipService.SetVerticalOffset(myListItem3, 2.5);
					ModMain._InvocationIterator.PanList.Children.Add(myListItem3);
					MyListItem myListItem4 = myListItem3;
					PageSelectLeft._Closure$__R4-3 CS$<>8__locals7 = new PageSelectLeft._Closure$__R4-3(CS$<>8__locals7);
					CS$<>8__locals7.$VB$NonLocal_4 = ModMain._InvocationIterator;
					myListItem4.Click += delegate(object sender, MouseButtonEventArgs e)
					{
						CS$<>8__locals7.$VB$NonLocal_4.Add_Click();
					};
					MyListItem myListItem5 = new MyListItem
					{
						IsScaleAnimationEnabled = false,
						Type = MyListItem.CheckType.Clickable,
						Title = "导入整合包",
						Height = 34.0,
						ToolTip = "在当前选择的 Minecraft 文件夹下安装整合包",
						Logo = "M512 40.96C249.344 40.96 35.84 252.416 35.84 512s213.504 471.04 476.16 471.04c103.424 0 202.752-33.28 286.72-96.256l1.536-1.536c5.12-5.632 7.68-12.8 7.68-19.968 0-16.896-13.824-30.208-30.72-30.208-7.68 0-15.36 2.56-20.992 7.68h-0.512c-71.68 52.224-155.648 79.36-243.712 79.36-227.328 0-412.16-182.784-412.16-407.552 0-224.768 184.832-407.552 412.16-407.552s412.16 182.784 412.16 407.552c0 68.608-15.872 132.608-46.592 190.464-0.512 1.024-1.024 2.048-1.024 3.072-0.512 2.048-1.536 4.608-1.536 8.192 0 16.896 13.824 30.208 30.72 30.208 12.288 0 23.04-7.168 28.16-18.432 35.84-68.608 53.76-141.312 53.76-216.064 0.512-259.584-212.992-471.04-475.648-471.04z M812.032 483.328c-31.744-20.992-71.68 1.536-78.848 6.144-1.024 0.512-104.448 61.44-128 74.752-8.192 4.608-22.528-0.512-27.136-4.096-31.232-36.352-54.272-70.656-68.608-102.4-13.312-29.184 0.512-41.472 3.072-43.52 7.168-4.608 114.688-68.608 143.36-83.456 24.064-12.288 40.96-25.088 46.08-45.056 3.072-13.312 0-27.136-9.216-39.936-22.016-31.744-172.544-84.992-311.296-3.584-157.184 91.648-152.064 242.688-150.528 292.352v9.216c0 18.944-12.8 37.376-14.848 40.448l-20.992 21.504c-6.144 6.144-9.216 13.824-9.216 22.528 0 8.704 3.584 16.384 9.728 22.528 12.8 12.288 32.768 11.776 45.056-0.512l22.528-23.552 0.512-0.512c3.072-3.584 30.208-38.4 30.208-81.92l-0.512-11.264c-1.536-44.544-5.632-162.816 119.296-235.52 88.064-51.2 173.056-32.256 208.896-19.968-36.864 19.456-143.36 83.456-144.896 84.48-22.016 14.336-55.808 58.88-26.112 122.88 17.408 37.376 43.52 76.8 80.896 120.32 14.336 17.408 62.976 37.376 103.424 15.36 24.576-13.312 125.44-73.216 130.048-75.776 2.048-1.024 4.608-2.56 7.68-3.584 0 2.56-0.512 6.144-1.024 10.752-5.632 35.84-35.328 155.136-191.488 181.76-49.664 8.704-89.6 3.584-121.856-0.512h-0.512c-37.888-4.608-73.216-9.216-101.888 14.336-31.232 26.112-40.96 34.304-35.84 54.272 3.584 14.336 16.384 24.064 30.72 24.064 2.56 0 5.12-0.512 7.68-1.024 6.656-1.536 12.8-5.632 16.896-10.752 2.048-2.048 7.68-6.656 20.992-18.432 6.656-5.632 25.088-3.584 52.736 0 34.816 4.608 81.92 10.24 141.312 0.512 157.184-26.624 228.864-138.752 243.2-234.496 7.68-38.912 0-64.512-21.504-78.336z"
					};
					ToolTipService.SetPlacement(myListItem5, PlacementMode.Right);
					ToolTipService.SetHorizontalOffset(myListItem5, -50.0);
					ToolTipService.SetVerticalOffset(myListItem5, 2.5);
					ModMain._InvocationIterator.PanList.Children.Add(myListItem5);
					myListItem5.Click += ((PageSelectLeft._Closure$__.$IR4-8 == null) ? (PageSelectLeft._Closure$__.$IR4-8 = delegate(object sender, MouseButtonEventArgs e)
					{
						ModModpack.ModpackInstall();
					}) : PageSelectLeft._Closure$__.$IR4-8);
					ModMain._InvocationIterator.PanList.Children.Add(new FrameworkElement
					{
						Height = 10.0,
						IsHitTestVisible = false
					});
					int num2 = ModMinecraft.messageTests.Count - 1;
					for (int k = 0; k <= num2; k++)
					{
						if (Operators.CompareString(ModMinecraft.messageTests[k].Path, ModMinecraft.m_ProxyTests, false) == 0)
						{
							((MyListItem)ModMain._InvocationIterator.PanList.Children[k + 1]).Checked = true;
							return;
						}
					}
					if (!Enumerable.Any<ModMinecraft.McFolder>(ModMinecraft.messageTests))
					{
						throw new ArgumentNullException("没有可用的 Minecraft 文件夹");
					}
					ModBase.m_IdentifierRepository.Set("LaunchFolderSelect", ModMinecraft.messageTests[0].Path.Replace(ModBase.Path, "$"), false, null);
					((MyListItem)ModMain._InvocationIterator.PanList.Children[1]).Checked = true;
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "构建 Minecraft 文件夹列表 UI 出错", ModBase.LogLevel.Feedback, "出现错误");
				}
				finally
				{
					ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.RunOnUpdated, 1, "versions\\", false);
				}
			}
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00076B40 File Offset: 0x00074D40
		public void Add_Click()
		{
			string text = "";
			if (ModNet.HasDownloadingTask(false))
			{
				ModMain.Hint("在下载任务进行时，无法添加游戏文件夹！", ModMain.HintType.Critical, true);
				return;
			}
			try
			{
				text = ModBase.SelectFolder("选择文件夹");
				if (Operators.CompareString(text, "", false) != 0)
				{
					if (!text.Contains("!") && !text.Contains(";"))
					{
						string[] array = text.TrimEnd(new char[]
						{
							'\\'
						}).Split("\\");
						string text2 = (Operators.CompareString(Enumerable.Last<string>(array), ".minecraft", false) == 0) ? ((Enumerable.Count<string>(array) >= 3) ? array[checked(Enumerable.Count<string>(array) - 2)] : "") : Enumerable.Last<string>(array);
						if (text2.Length > 40)
						{
							text2 = text2.Substring(0, 39);
						}
						string text3 = ModMain.MyMsgBoxInput("输入显示名称", "输入该文件夹在左边栏列表中显示的名称。", text2, new Collection<Validate>
						{
							new ValidateNullOrWhiteSpace(),
							new ValidateLength(1, 30),
							new ValidateExcept(new string[]
							{
								">",
								"|"
							}, "输入内容不能包含 %！")
						}, "", "确定", "取消", false);
						if (!string.IsNullOrWhiteSpace(text3))
						{
							PageSelectLeft.AddFolder(text, text3, true);
						}
					}
					else
					{
						ModMain.Hint("Minecraft 文件夹路径中不能含有感叹号或分号！", ModMain.HintType.Critical, true);
					}
				}
			}
			catch (Exception ex)
			{
				ModBase.Log(ex, "添加文件夹失败（" + text + "）", ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00076CE4 File Offset: 0x00074EE4
		public static void AddFolder(string FolderPath, string DisplayName, bool ShowHint)
		{
			PageSelectLeft._Closure$__7-0 CS$<>8__locals1 = new PageSelectLeft._Closure$__7-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_FolderPath = FolderPath;
			CS$<>8__locals1.$VB$Local_DisplayName = DisplayName;
			CS$<>8__locals1.$VB$Local_ShowHint = ShowHint;
			ModBase.RunInThread(checked(delegate
			{
				try
				{
					if (!CS$<>8__locals1.$VB$Local_FolderPath.EndsWith("\\"))
					{
						CS$<>8__locals1.$VB$Local_FolderPath += "\\";
					}
					if (ModBase.CheckPermission(CS$<>8__locals1.$VB$Local_FolderPath))
					{
						if (!ModBase.CheckPermission(CS$<>8__locals1.$VB$Local_FolderPath + "versions\\"))
						{
							foreach (DirectoryInfo directoryInfo in new DirectoryInfo(CS$<>8__locals1.$VB$Local_FolderPath).GetDirectories())
							{
								if (ModBase.CheckPermission(directoryInfo.FullName + "\\versions\\"))
								{
									CS$<>8__locals1.$VB$Local_FolderPath = directoryInfo.FullName + "\\";
									break;
								}
							}
						}
						List<string> list = new List<string>(ModBase.m_IdentifierRepository.Get("LaunchFolders", null).ToString().Split("|"));
						bool flag = false;
						bool flag2 = false;
						int num = list.Count - 1;
						int j = 0;
						while (j <= num)
						{
							string text = list[j];
							if (Operators.CompareString(text, "", false) == 0 || Operators.CompareString(text.Split(">")[1], CS$<>8__locals1.$VB$Local_FolderPath, false) != 0)
							{
								j++;
							}
							else
							{
								flag = true;
								if (Operators.CompareString(text.Split(">")[0], CS$<>8__locals1.$VB$Local_DisplayName, false) == 0)
								{
									if (CS$<>8__locals1.$VB$Local_ShowHint)
									{
										ModMain.Hint("此文件夹已在列表中！", ModMain.HintType.Info, true);
									}
									return;
								}
								list[j] = CS$<>8__locals1.$VB$Local_DisplayName + ">" + CS$<>8__locals1.$VB$Local_FolderPath;
								flag2 = true;
								if (CS$<>8__locals1.$VB$Local_ShowHint)
								{
									ModMain.Hint("文件夹名称已更新为 " + CS$<>8__locals1.$VB$Local_DisplayName + " ！", ModMain.HintType.Finish, true);
								}
								IL_1D0:
								if (!flag)
								{
									list.Add(CS$<>8__locals1.$VB$Local_DisplayName + ">" + CS$<>8__locals1.$VB$Local_FolderPath);
								}
								ModBase.m_IdentifierRepository.Set("LaunchFolders", list.ToArray().Join("|"), false, null);
								ModBase.m_IdentifierRepository.Set("LaunchFolderSelect", CS$<>8__locals1.$VB$Local_FolderPath.Replace(ModBase.Path, "$"), false, null);
								ModMinecraft.m_CreatorTests.Start(null, true);
								if (flag2)
								{
									return;
								}
								if (CS$<>8__locals1.$VB$Local_ShowHint)
								{
									ModMain.Hint("文件夹 " + CS$<>8__locals1.$VB$Local_DisplayName + " 已添加！", ModMain.HintType.Finish, true);
								}
								DirectoryInfo directoryInfo2 = new DirectoryInfo(CS$<>8__locals1.$VB$Local_FolderPath + "mods\\");
								if (!directoryInfo2.Exists || Enumerable.Count<FileInfo>(directoryInfo2.EnumerateFiles()) < 3)
								{
									return;
								}
								DirectoryInfo directoryInfo3 = new DirectoryInfo(CS$<>8__locals1.$VB$Local_FolderPath + "versions\\");
								if (directoryInfo3.Exists && Enumerable.Count<DirectoryInfo>(directoryInfo3.EnumerateDirectories()) <= 3)
								{
									try
									{
										foreach (DirectoryInfo directoryInfo4 in directoryInfo3.EnumerateDirectories())
										{
											ModMinecraft.McVersion mcVersion = new ModMinecraft.McVersion(directoryInfo4.FullName);
											mcVersion.Load();
											if (mcVersion.RunThread())
											{
												DirectoryInfo directoryInfo5 = new DirectoryInfo(mcVersion.Path + "mods\\");
												if (directoryInfo5.Exists && Enumerable.Any<FileInfo>(directoryInfo5.EnumerateFiles()))
												{
													break;
												}
												ModBase.m_IdentifierRepository.Set("VersionArgumentIndie", 2, false, mcVersion);
												ModBase.Log("[Setup] 已自动关闭单版本隔离：" + mcVersion.Name, ModBase.LogLevel.Debug, "出现错误");
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
									return;
								}
								return;
							}
						}
						goto IL_1D0;
					}
					if (!CS$<>8__locals1.$VB$Local_ShowHint)
					{
						throw new Exception("PCL 没有访问文件夹的权限：" + CS$<>8__locals1.$VB$Local_FolderPath);
					}
					ModMain.Hint("添加文件夹失败：PCL 没有访问该文件夹的权限！", ModMain.HintType.Critical, true);
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "向文件夹列表中添加新文件夹失败", ModBase.LogLevel.Feedback, "出现错误");
				}
			}));
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00076D20 File Offset: 0x00074F20
		public void Create_Click()
		{
			if (ModNet.HasDownloadingTask(false))
			{
				ModMain.Hint("在下载任务进行时，无法创建游戏文件夹！", ModMain.HintType.Critical, true);
				return;
			}
			if (!Directory.Exists(ModBase.Path + ".minecraft\\"))
			{
				Directory.CreateDirectory(ModBase.Path + ".minecraft\\");
				Directory.CreateDirectory(ModBase.Path + ".minecraft\\versions\\");
				ModBase.m_IdentifierRepository.Set("LaunchFolderSelect", "$.minecraft\\", false, null);
				ModMinecraft.McFolderLauncherProfilesJsonCreate(ModBase.Path + ".minecraft\\");
				ModMain.Hint("新建 .minecraft 文件夹成功！", ModMain.HintType.Finish, true);
			}
			ModMinecraft.m_CreatorTests.Start(null, true);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00076DC4 File Offset: 0x00074FC4
		public void Remove_Click(object sender, RoutedEventArgs e)
		{
			checked
			{
				try
				{
					ModMinecraft.McFolder mcFolder = (ModMinecraft.McFolder)((MyListItem)((Popup)((ContextMenu)NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null)).Parent).PlacementTarget).Tag;
					if (mcFolder.Type == ModMinecraft.McFolderType.Custom)
					{
						switch (ModMain.MyMsgBox("是否需要清理 PCL 在该文件夹中的配置文件？\r\n这包括各个版本的独立设置（如自定义图标、第三方登录配置）等，对游戏本身没有影响。", "配置文件清理", "删除", "保留", "取消", false, true, false, null, null, null))
						{
						case 1:
							if (File.Exists(mcFolder.Path + "PCL.ini"))
							{
								File.Delete(mcFolder.Path + "PCL.ini");
							}
							if (!Directory.Exists(mcFolder.Path + "versions\\"))
							{
								goto IL_13E;
							}
							try
							{
								foreach (DirectoryInfo directoryInfo in new DirectoryInfo(mcFolder.Path + "versions\\").EnumerateDirectories())
								{
									if (Directory.Exists(directoryInfo.FullName + "\\PCL\\"))
									{
										Directory.Delete(directoryInfo.FullName + "\\PCL\\", true);
									}
								}
								goto IL_13E;
							}
							finally
							{
								IEnumerator<DirectoryInfo> enumerator;
								if (enumerator != null)
								{
									enumerator.Dispose();
								}
							}
							break;
						case 2:
							goto IL_13E;
						case 3:
							break;
						default:
							goto IL_13E;
						}
						return;
					}
					IL_13E:
					List<string> list = new List<string>(ModBase.m_IdentifierRepository.Get("LaunchFolders", null).ToString().Split("|"));
					string str = "";
					int num = list.Count - 1;
					int i = 0;
					while (i <= num)
					{
						if (Operators.CompareString(list[i], "", false) != 0)
						{
							if (!list[i].ToString().EndsWith(mcFolder.Path))
							{
								i++;
								continue;
							}
							str = list[i].ToString().BeforeFirst(">", false);
							list.RemoveAt(i);
						}
						IL_1D7:
						ModBase.m_IdentifierRepository.Set("LaunchFolders", (!Enumerable.Any<string>(list)) ? "" : list.ToArray().Join("|"), false, null);
						ModMain.Hint((mcFolder.Type == ModMinecraft.McFolderType.Custom) ? ("文件夹 " + str + " 已从列表中移除！") : "文件夹名称已复原！", ModMain.HintType.Finish, true);
						ModMinecraft.m_CreatorTests.Start(null, true);
						return;
					}
					goto IL_1D7;
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "从列表中移除游戏文件夹失败", ModBase.LogLevel.Feedback, "出现错误");
				}
			}
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00077064 File Offset: 0x00075264
		public void Delete_Click(object sender, RoutedEventArgs e)
		{
			PageSelectLeft._Closure$__10-0 CS$<>8__locals1 = new PageSelectLeft._Closure$__10-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_Folder = (ModMinecraft.McFolder)((MyListItem)((Popup)((ContextMenu)NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null)).Parent).PlacementTarget).Tag;
			PageSelectLeft._Closure$__10-0 CS$<>8__locals2 = CS$<>8__locals1;
			string $VB$Local_DeleteText;
			if ((CS$<>8__locals1.$VB$Local_Folder.Type == ModMinecraft.McFolderType.Original || CS$<>8__locals1.$VB$Local_Folder.Type == ModMinecraft.McFolderType.RenamedOriginal) && Operators.CompareString(CS$<>8__locals1.$VB$Local_Folder.Path, ModBase.Path + ".minecraft\\", false) == 0)
			{
				if (ModMinecraft.messageTests.Count == 1)
				{
					$VB$Local_DeleteText = "清空";
					goto IL_9F;
				}
			}
			$VB$Local_DeleteText = "删除";
			IL_9F:
			CS$<>8__locals2.$VB$Local_DeleteText = $VB$Local_DeleteText;
			checked
			{
				if (ModMain.MyMsgBox(string.Concat(new string[]
				{
					"你确定要",
					CS$<>8__locals1.$VB$Local_DeleteText,
					"这个文件夹吗？\r\n目标文件夹：",
					CS$<>8__locals1.$VB$Local_Folder.Path,
					"\r\n\r\n这会导致该文件夹中的所有存档与其他文件永久丢失，且不可恢复！"
				}), "删除警告", "取消", "确认", "取消", false, true, false, null, null, null) == 2 && ModMain.MyMsgBox("如果你在该文件夹中存放了除 MC 以外的其他文件，这些文件也会被一同删除！\r\n继续删除会导致该文件夹中的所有文件永久丢失，请在仔细确认后再继续！\r\n目标文件夹：" + CS$<>8__locals1.$VB$Local_Folder.Path + "\r\n\r\n这是最后一次警告！", "删除警告", "确认" + CS$<>8__locals1.$VB$Local_DeleteText, "取消", "", true, true, false, null, null, null) == 1)
				{
					if (CS$<>8__locals1.$VB$Local_Folder.Type == ModMinecraft.McFolderType.Custom)
					{
						List<string> list = new List<string>(ModBase.m_IdentifierRepository.Get("LaunchFolders", null).ToString().Split("|"));
						int num = list.Count - 1;
						int i = 0;
						while (i <= num)
						{
							if (Operators.CompareString(list[i], "", false) != 0)
							{
								if (!list[i].ToString().EndsWith(CS$<>8__locals1.$VB$Local_Folder.Path))
								{
									i++;
									continue;
								}
								list.RemoveAt(i);
							}
							IL_1D4:
							ModBase.m_IdentifierRepository.Set("LaunchFolders", (!Enumerable.Any<string>(list)) ? "" : list.ToArray().Join("|"), false, null);
							goto IL_204;
						}
						goto IL_1D4;
					}
					IL_204:
					ModBase.RunInNewThread(delegate
					{
						try
						{
							ModMain.Hint(string.Concat(new string[]
							{
								"正在",
								CS$<>8__locals1.$VB$Local_DeleteText,
								"文件夹 ",
								CS$<>8__locals1.$VB$Local_Folder.Name,
								"！"
							}), ModMain.HintType.Info, true);
							ModBase.DeleteDirectory(CS$<>8__locals1.$VB$Local_Folder.Path, false);
							if (Operators.CompareString(CS$<>8__locals1.$VB$Local_DeleteText, "清空", false) == 0)
							{
								Directory.CreateDirectory(CS$<>8__locals1.$VB$Local_Folder.Path);
							}
							ModMain.Hint(string.Concat(new string[]
							{
								"已",
								CS$<>8__locals1.$VB$Local_DeleteText,
								"文件夹 ",
								CS$<>8__locals1.$VB$Local_Folder.Name,
								"！"
							}), ModMain.HintType.Finish, true);
						}
						catch (Exception ex)
						{
							ModBase.Log(ex, CS$<>8__locals1.$VB$Local_DeleteText + "文件夹 " + CS$<>8__locals1.$VB$Local_Folder.Name + " 失败", ModBase.LogLevel.Hint, "出现错误");
						}
						finally
						{
							ModMinecraft.m_CreatorTests.Start(null, true);
						}
					}, "Folder Delete " + Conversions.ToString(ModBase.GetUuid()), ThreadPriority.BelowNormal);
				}
			}
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0007729C File Offset: 0x0007549C
		public void Open_Click(object sender, RoutedEventArgs e)
		{
			ModBase.OpenExplorer("\"" + ((MyListItem)((Popup)((ContextMenu)NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null)).Parent).PlacementTarget).Info + "\"");
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x000772F0 File Offset: 0x000754F0
		public void Refresh_Click(object sender, RoutedEventArgs e)
		{
			PageSelectLeft.RefreshCurrent(((ModMinecraft.McFolder)((MyListItem)((Popup)((ContextMenu)NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null)).Parent).PlacementTarget).Tag).Path);
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x00009E01 File Offset: 0x00008001
		public void RefreshCurrent()
		{
			PageSelectLeft.RefreshCurrent(ModMinecraft.m_ProxyTests);
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00077340 File Offset: 0x00075540
		public static void RefreshCurrent(string Folder)
		{
			ModBase.WriteIni(Folder + "PCL.ini", "VersionCache", "");
			if (Operators.CompareString(Folder, ModMinecraft.m_ProxyTests, false) == 0)
			{
				ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", false);
			}
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00077390 File Offset: 0x00075590
		public void Rename_Click(object sender, RoutedEventArgs e)
		{
			ModMinecraft.McFolder mcFolder = (ModMinecraft.McFolder)((MyListItem)((Popup)((ContextMenu)NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null)).Parent).PlacementTarget).Tag;
			checked
			{
				try
				{
					string text = ModMain.MyMsgBoxInput("输入新名称", "", mcFolder.Name, new Collection<Validate>
					{
						new ValidateNullOrWhiteSpace(),
						new ValidateLength(1, 30),
						new ValidateExcept(new string[]
						{
							">",
							"|"
						}, "输入内容不能包含 %！")
					}, "", "确定", "取消", false);
					if (!string.IsNullOrWhiteSpace(text))
					{
						List<string> list = new List<string>(ModBase.m_IdentifierRepository.Get("LaunchFolders", null).ToString().Split("|"));
						bool flag = false;
						int num = list.Count - 1;
						int i = 0;
						while (i <= num)
						{
							string text2 = list[i];
							if (Operators.CompareString(text2, "", false) == 0 || Operators.CompareString(text2.Split(">")[1], mcFolder.Path, false) != 0)
							{
								i++;
							}
							else
							{
								flag = true;
								if (Operators.CompareString(text2.Split(">")[0], text, false) == 0)
								{
									return;
								}
								list[i] = text + ">" + mcFolder.Path;
								IL_168:
								if (!flag)
								{
									list.Add(text + ">" + mcFolder.Path);
								}
								ModMain.Hint("文件夹名称已更新为 " + text + " ！", ModMain.HintType.Finish, true);
								ModBase.m_IdentifierRepository.Set("LaunchFolders", list.ToArray().Join("|"), false, null);
								ModMinecraft.m_CreatorTests.Start(null, true);
								return;
							}
						}
						goto IL_168;
					}
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "重命名文件夹失败", ModBase.LogLevel.Feedback, "出现错误");
				}
			}
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x000775A4 File Offset: 0x000757A4
		public void Folder_Change(MyListItem sender, ModBase.RouteEventArgs e)
		{
			if (e.interpreterError && sender.Checked)
			{
				if (ModNet.HasDownloadingTask(true))
				{
					ModMain.Hint("在下载任务进行时，无法切换游戏文件夹！", ModMain.HintType.Critical, true);
					e.m_SerializerError = true;
					return;
				}
				ModBase.m_IdentifierRepository.Set("LaunchFolderSelect", ((ModMinecraft.McFolder)sender.Tag).Path.Replace(ModBase.Path, "$"), false, null);
				ModMinecraft.m_CreatorTests.Start(null, true);
				ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.RunOnUpdated, 1, "versions\\", false);
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x00009E0D File Offset: 0x0000800D
		// (set) Token: 0x0600102F RID: 4143 RVA: 0x00009E15 File Offset: 0x00008015
		internal virtual StackPanel PanList { get; set; }

		// Token: 0x06001030 RID: 4144 RVA: 0x00077634 File Offset: 0x00075834
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._BaseMapper)
			{
				this._BaseMapper = true;
				Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageselectleft.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0000414E File Offset: 0x0000234E
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00009E1E File Offset: 0x0000801E
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.PanList = (StackPanel)target;
				return;
			}
			this._BaseMapper = true;
		}

		// Token: 0x04000887 RID: 2183
		private bool managerMapper;

		// Token: 0x04000888 RID: 2184
		private List<ModMinecraft.McFolder> m_ModelMapper;

		// Token: 0x04000889 RID: 2185
		[CompilerGenerated]
		[AccessedThroughProperty("PanList")]
		private StackPanel m_WrapperMapper;

		// Token: 0x0400088A RID: 2186
		private bool _BaseMapper;
	}
}
