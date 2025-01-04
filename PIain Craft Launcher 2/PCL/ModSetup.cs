using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL
{
	// Token: 0x020001AD RID: 429
	public class ModSetup
	{
		// Token: 0x060011C4 RID: 4548 RVA: 0x0007E55C File Offset: 0x0007C75C
		public ModSetup()
		{
			this.m_ValThread = new Dictionary<string, ModSetup.SetupEntry>
			{
				{
					"Identify",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, false)
				},
				{
					"WindowHeight",
					new ModSetup.SetupEntry(550, 0, false)
				},
				{
					"WindowWidth",
					new ModSetup.SetupEntry(900, 0, false)
				},
				{
					"HintDownloadThread",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Registry, false)
				},
				{
					"HintNotice",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Registry, false)
				},
				{
					"HintDownload",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Registry, false)
				},
				{
					"HintInstallBack",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Registry, false)
				},
				{
					"HintHide",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Registry, false)
				},
				{
					"HintHandInstall",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Registry, false)
				},
				{
					"HintBuy",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Registry, false)
				},
				{
					"HintClearRubbish",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Registry, false)
				},
				{
					"HintUpdateMod",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Registry, false)
				},
				{
					"TestSetupReader",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"SystemEula",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Registry, false)
				},
				{
					"SystemCount",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Registry, true)
				},
				{
					"SystemLaunchCount",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Registry, true)
				},
				{
					"SystemLastVersionReg",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Registry, true)
				},
				{
					"SystemHighestBetaVersionReg",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Registry, true)
				},
				{
					"SystemHighestAlphaVersionReg",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Registry, true)
				},
				{
					"SystemSetupVersionReg",
					new ModSetup.SetupEntry(1, ModSetup.SetupSource.Registry, false)
				},
				{
					"SystemSetupVersionIni",
					new ModSetup.SetupEntry(1, 0, false)
				},
				{
					"SystemHelpVersion",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Registry, false)
				},
				{
					"SystemDebugMode",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Registry, false)
				},
				{
					"SystemDebugAnim",
					new ModSetup.SetupEntry(9, ModSetup.SetupSource.Registry, false)
				},
				{
					"SystemDebugDelay",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Registry, false)
				},
				{
					"SystemDebugSkipCopy",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Registry, false)
				},
				{
					"SystemSystemCache",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, false)
				},
				{
					"SystemSystemUpdate",
					new ModSetup.SetupEntry(0, 0, false)
				},
				{
					"SystemSystemActivity",
					new ModSetup.SetupEntry(0, 0, false)
				},
				{
					"CacheSavedPageUrl",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, false)
				},
				{
					"CacheSavedPageVersion",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, false)
				},
				{
					"CacheMsOAuthRefresh",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheMsAccess",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheMsProfileJson",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheMsUuid",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheMsName",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheMsV2Migrated",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Registry, false)
				},
				{
					"CacheMsV2OAuthRefresh",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheMsV2Access",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheMsV2ProfileJson",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheMsV2Uuid",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheMsV2Name",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheNideAccess",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheNideClient",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheNideUuid",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheNideName",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheNideUsername",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheNidePass",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheNideServer",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheAuthAccess",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheAuthClient",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheAuthUuid",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheAuthName",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheAuthUsername",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheAuthPass",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheAuthServerServer",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheAuthServerName",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheAuthServerRegister",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"CacheDownloadFolder",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, false)
				},
				{
					"CacheJavaListVersion",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Registry, false)
				},
				{
					"LoginRemember",
					new ModSetup.SetupEntry(true, ModSetup.SetupSource.Registry, true)
				},
				{
					"LoginLegacyName",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"LoginMsJson",
					new ModSetup.SetupEntry("{}", ModSetup.SetupSource.Registry, true)
				},
				{
					"LoginNideEmail",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"LoginNidePass",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"LoginAuthEmail",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"LoginAuthPass",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"LoginType",
					new ModSetup.SetupEntry(ModLaunch.McLoginType.Legacy, ModSetup.SetupSource.Registry, false)
				},
				{
					"LoginPageType",
					new ModSetup.SetupEntry(0, 0, false)
				},
				{
					"LaunchSkinID",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, false)
				},
				{
					"LaunchSkinType",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Registry, false)
				},
				{
					"LaunchSkinSlim",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Registry, false)
				},
				{
					"LaunchVersionSelect",
					new ModSetup.SetupEntry("", 0, false)
				},
				{
					"LaunchFolderSelect",
					new ModSetup.SetupEntry("", 0, false)
				},
				{
					"LaunchFolders",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, false)
				},
				{
					"LaunchArgumentTitle",
					new ModSetup.SetupEntry("", 0, false)
				},
				{
					"LaunchArgumentInfo",
					new ModSetup.SetupEntry("PCL", 0, false)
				},
				{
					"LaunchArgumentJavaSelect",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, false)
				},
				{
					"LaunchArgumentJavaAll",
					new ModSetup.SetupEntry("[]", ModSetup.SetupSource.Registry, false)
				},
				{
					"LaunchArgumentIndie",
					new ModSetup.SetupEntry(0, 0, false)
				},
				{
					"LaunchArgumentVisible",
					new ModSetup.SetupEntry(5, ModSetup.SetupSource.Registry, false)
				},
				{
					"LaunchArgumentPriority",
					new ModSetup.SetupEntry(1, ModSetup.SetupSource.Registry, false)
				},
				{
					"LaunchArgumentWindowWidth",
					new ModSetup.SetupEntry(854, 0, false)
				},
				{
					"LaunchArgumentWindowHeight",
					new ModSetup.SetupEntry(480, 0, false)
				},
				{
					"LaunchArgumentWindowType",
					new ModSetup.SetupEntry(1, 0, false)
				},
				{
					"LaunchArgumentRam",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Registry, false)
				},
				{
					"LaunchAdvanceJvm",
					new ModSetup.SetupEntry("-XX:+UseG1GC -XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow -Djdk.lang.Process.allowAmbiguousCommands=true -Dfml.ignoreInvalidMinecraftCertificates=True -Dfml.ignorePatchDiscrepancies=True -Dlog4j2.formatMsgNoLookups=true", 0, false)
				},
				{
					"LaunchAdvanceGame",
					new ModSetup.SetupEntry("", 0, false)
				},
				{
					"LaunchAdvanceRun",
					new ModSetup.SetupEntry("", 0, false)
				},
				{
					"LaunchAdvanceRunWait",
					new ModSetup.SetupEntry(true, 0, false)
				},
				{
					"LaunchAdvanceAssets",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"LaunchAdvanceJava",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"LaunchRamType",
					new ModSetup.SetupEntry(0, 0, false)
				},
				{
					"LaunchRamCustom",
					new ModSetup.SetupEntry(15, 0, false)
				},
				{
					"LinkEula",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Registry, false)
				},
				{
					"LinkName",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, false)
				},
				{
					"LinkHiperCertLast",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, false)
				},
				{
					"LinkHiperCertTime",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, false)
				},
				{
					"LinkHiperCertWarn",
					new ModSetup.SetupEntry(true, ModSetup.SetupSource.Registry, false)
				},
				{
					"LinkIoiVersion",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Registry, true)
				},
				{
					"ToolHelpChinese",
					new ModSetup.SetupEntry(true, ModSetup.SetupSource.Registry, false)
				},
				{
					"ToolDownloadThread",
					new ModSetup.SetupEntry(63, ModSetup.SetupSource.Registry, false)
				},
				{
					"ToolDownloadSpeed",
					new ModSetup.SetupEntry(42, ModSetup.SetupSource.Registry, false)
				},
				{
					"ToolDownloadVersion",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Registry, false)
				},
				{
					"ToolDownloadTranslate",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Registry, false)
				},
				{
					"ToolDownloadIgnoreQuilt",
					new ModSetup.SetupEntry(true, ModSetup.SetupSource.Registry, false)
				},
				{
					"ToolDownloadCert",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Registry, false)
				},
				{
					"ToolDownloadMod",
					new ModSetup.SetupEntry(1, ModSetup.SetupSource.Registry, false)
				},
				{
					"ToolModLocalNameStyle",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Registry, false)
				},
				{
					"ToolUpdateAlpha",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Registry, true)
				},
				{
					"ToolUpdateRelease",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Registry, false)
				},
				{
					"ToolUpdateSnapshot",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Registry, false)
				},
				{
					"ToolUpdateReleaseLast",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, false)
				},
				{
					"ToolUpdateSnapshotLast",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, false)
				},
				{
					"UiLauncherTransparent",
					new ModSetup.SetupEntry(600, 0, false)
				},
				{
					"UiLauncherHue",
					new ModSetup.SetupEntry(180, 0, false)
				},
				{
					"UiLauncherSat",
					new ModSetup.SetupEntry(80, 0, false)
				},
				{
					"UiLauncherDelta",
					new ModSetup.SetupEntry(90, 0, false)
				},
				{
					"UiLauncherLight",
					new ModSetup.SetupEntry(20, 0, false)
				},
				{
					"UiLauncherTheme",
					new ModSetup.SetupEntry(0, 0, false)
				},
				{
					"UiLauncherThemeGold",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Registry, true)
				},
				{
					"UiLauncherThemeHide",
					new ModSetup.SetupEntry("0|1|2|3|4", ModSetup.SetupSource.Registry, true)
				},
				{
					"UiLauncherThemeHide2",
					new ModSetup.SetupEntry("0|1|2|3|4", ModSetup.SetupSource.Registry, true)
				},
				{
					"UiLauncherLogo",
					new ModSetup.SetupEntry(true, 0, false)
				},
				{
					"UiLauncherEmail",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiBackgroundColorful",
					new ModSetup.SetupEntry(true, 0, false)
				},
				{
					"UiBackgroundOpacity",
					new ModSetup.SetupEntry(1000, 0, false)
				},
				{
					"UiBackgroundBlur",
					new ModSetup.SetupEntry(0, 0, false)
				},
				{
					"UiBackgroundSuit",
					new ModSetup.SetupEntry(0, 0, false)
				},
				{
					"UiCustomType",
					new ModSetup.SetupEntry(0, 0, false)
				},
				{
					"UiCustomPreset",
					new ModSetup.SetupEntry(0, 0, false)
				},
				{
					"UiCustomNet",
					new ModSetup.SetupEntry("", 0, false)
				},
				{
					"UiLogoType",
					new ModSetup.SetupEntry(1, 0, false)
				},
				{
					"UiLogoText",
					new ModSetup.SetupEntry("", 0, false)
				},
				{
					"UiLogoLeft",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiMusicVolume",
					new ModSetup.SetupEntry(500, 0, false)
				},
				{
					"UiMusicStop",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiMusicStart",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiMusicRandom",
					new ModSetup.SetupEntry(true, 0, false)
				},
				{
					"UiMusicAuto",
					new ModSetup.SetupEntry(true, 0, false)
				},
				{
					"UiHiddenPageDownload",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiHiddenPageLink",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiHiddenPageSetup",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiHiddenPageOther",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiHiddenFunctionSelect",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiHiddenFunctionModUpdate",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiHiddenFunctionHidden",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiHiddenSetupLaunch",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiHiddenSetupUi",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiHiddenSetupLink",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiHiddenSetupSystem",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiHiddenOtherHelp",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiHiddenOtherFeedback",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiHiddenOtherVote",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiHiddenOtherAbout",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"UiHiddenOtherTest",
					new ModSetup.SetupEntry(false, 0, false)
				},
				{
					"VersionAdvanceJvm",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Version, false)
				},
				{
					"VersionAdvanceGame",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Version, false)
				},
				{
					"VersionAdvanceAssets",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Version, false)
				},
				{
					"VersionAdvanceAssetsV2",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Version, false)
				},
				{
					"VersionAdvanceJava",
					new ModSetup.SetupEntry(false, ModSetup.SetupSource.Version, false)
				},
				{
					"VersionAdvanceRun",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Version, false)
				},
				{
					"VersionAdvanceRunWait",
					new ModSetup.SetupEntry(true, ModSetup.SetupSource.Version, false)
				},
				{
					"VersionRamType",
					new ModSetup.SetupEntry(2, ModSetup.SetupSource.Version, false)
				},
				{
					"VersionRamCustom",
					new ModSetup.SetupEntry(15, ModSetup.SetupSource.Version, false)
				},
				{
					"VersionRamOptimize",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Version, false)
				},
				{
					"VersionArgumentTitle",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Version, false)
				},
				{
					"VersionArgumentInfo",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Version, false)
				},
				{
					"VersionArgumentIndie",
					new ModSetup.SetupEntry(-1, ModSetup.SetupSource.Version, false)
				},
				{
					"VersionArgumentJavaSelect",
					new ModSetup.SetupEntry("使用全局设置", ModSetup.SetupSource.Version, false)
				},
				{
					"VersionServerEnter",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Version, false)
				},
				{
					"VersionServerLogin",
					new ModSetup.SetupEntry(0, ModSetup.SetupSource.Version, false)
				},
				{
					"VersionServerNide",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Version, false)
				},
				{
					"VersionServerAuthRegister",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Version, false)
				},
				{
					"VersionServerAuthName",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Version, false)
				},
				{
					"VersionServerAuthServer",
					new ModSetup.SetupEntry("", ModSetup.SetupSource.Version, false)
				}
			};
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0000AB01 File Offset: 0x00008D01
		public void Set(string Key, object Value, bool ForceReload = false, ModMinecraft.McVersion Version = null)
		{
			this.Set(Key, RuntimeHelpers.GetObjectValue(Value), this.m_ValThread[Key], ForceReload, Version);
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0007FCC0 File Offset: 0x0007DEC0
		private void Set(string Key, object Value, ModSetup.SetupEntry E, bool ForceReload, ModMinecraft.McVersion Version)
		{
			try
			{
				Value = RuntimeHelpers.GetObjectValue(Conversion.CTypeDynamic(RuntimeHelpers.GetObjectValue(Value), E.Type));
				if (E.databaseMap == 2)
				{
					if (Operators.ConditionalCompareObjectEqual(E.Value, Value, false) && !ForceReload)
					{
						return;
					}
				}
				else if (E.Source != ModSetup.SetupSource.Version)
				{
					E.databaseMap = 2;
				}
				E.Value = RuntimeHelpers.GetObjectValue(Value);
				if (E._MappingMap)
				{
					try
					{
						if (Value == null)
						{
							Value = "";
						}
						Value = ModSecret.SecretEncrypt(Conversions.ToString(Value), "PCL" + ModBase._TagRepository);
					}
					catch (Exception ex)
					{
						ModBase.Log(ex, "加密设置失败：" + Key, ModBase.LogLevel.Developer, "出现错误");
					}
				}
				switch (E.Source)
				{
				case ModSetup.SetupSource.Normal:
					ModBase.WriteIni("Setup", Key, Conversions.ToString(Value));
					break;
				case ModSetup.SetupSource.Registry:
					ModBase.WriteReg(Key, Conversions.ToString(Value), false);
					break;
				case ModSetup.SetupSource.Version:
					if (Version == null)
					{
						throw new Exception("更改版本设置 " + Key + " 时未提供目标版本");
					}
					ModBase.WriteIni(Version.Path + "PCL\\Setup.ini", Key, Conversions.ToString(Value));
					break;
				}
				MethodInfo method = typeof(ModSetup).GetMethod(Key);
				if (method != null)
				{
					method.Invoke(this, new object[]
					{
						Value
					});
				}
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("设置设置项时出错（" + Key + ", ", Value), "）")), ModBase.LogLevel.Feedback, "出现错误");
			}
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0000AB1F File Offset: 0x00008D1F
		public object Load(string Key, bool ForceReload = false, ModMinecraft.McVersion Version = null)
		{
			return this.Load(Key, this.m_ValThread[Key], ForceReload, Version);
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0007FE88 File Offset: 0x0007E088
		private object Load(string Key, ModSetup.SetupEntry E, bool ForceReload, ModMinecraft.McVersion Version)
		{
			object value;
			if (E.databaseMap == 2 && !ForceReload)
			{
				value = E.Value;
			}
			else
			{
				this.Read(Key, ref E, Version);
				if (E.Source != ModSetup.SetupSource.Version)
				{
					E.databaseMap = 2;
				}
				MethodInfo method = typeof(ModSetup).GetMethod(Key);
				if (method != null)
				{
					method.Invoke(this, new object[]
					{
						E.Value
					});
				}
				value = E.Value;
			}
			return value;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0000AB36 File Offset: 0x00008D36
		public object Get(string Key, ModMinecraft.McVersion Version = null)
		{
			if (!this.m_ValThread.ContainsKey(Key))
			{
				throw new KeyNotFoundException("未找到设置项：" + Key)
				{
					Source = Key
				};
			}
			return this.Get(Key, this.m_ValThread[Key], Version);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0007FEFC File Offset: 0x0007E0FC
		private object Get(string Key, ModSetup.SetupEntry E, ModMinecraft.McVersion Version)
		{
			string text = this.ForceValue(Key);
			if (text != null)
			{
				E.Value = RuntimeHelpers.GetObjectValue(Conversion.CTypeDynamic(text, E.Type));
				E.databaseMap = 1;
			}
			if (E.databaseMap == 0)
			{
				this.Read(Key, ref E, Version);
				if (E.Source != ModSetup.SetupSource.Version)
				{
					E.databaseMap = 1;
				}
			}
			return E.Value;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0007FF5C File Offset: 0x0007E15C
		public void Reset(string Key, bool ForceReload = false, ModMinecraft.McVersion Version = null)
		{
			ModSetup.SetupEntry setupEntry = this.m_ValThread[Key];
			this.Set(Key, RuntimeHelpers.GetObjectValue(setupEntry.m_TokenizerMap), setupEntry, ForceReload, Version);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0000AB72 File Offset: 0x00008D72
		public string GetDefault(string Key)
		{
			return Conversions.ToString(this.m_ValThread[Key].m_TokenizerMap);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0007FF8C File Offset: 0x0007E18C
		private void Read(string Key, ref ModSetup.SetupEntry E, ModMinecraft.McVersion Version)
		{
			try
			{
				if (E.databaseMap == 0)
				{
					ModSetup.SetupSource source = E.Source;
					string text;
					if (source != ModSetup.SetupSource.Normal)
					{
						if (source != ModSetup.SetupSource.Registry)
						{
							if (Version == null)
							{
								throw new Exception("读取版本设置 " + Key + " 时未提供目标版本");
							}
							text = ModBase.ReadIni(Version.Path + "PCL\\Setup.ini", Key, Conversions.ToString(E.filterMap));
						}
						else
						{
							text = ModBase.ReadReg(Key, Conversions.ToString(E.filterMap));
						}
					}
					else
					{
						text = ModBase.ReadIni("Setup", Key, Conversions.ToString(E.filterMap));
					}
					if (E._MappingMap)
					{
						if (text.Equals(RuntimeHelpers.GetObjectValue(E.filterMap)))
						{
							text = Conversions.ToString(E.m_TokenizerMap);
						}
						else
						{
							try
							{
								text = ModSecret.SecretDecrypt(text, "PCL" + ModBase._TagRepository);
							}
							catch (Exception ex)
							{
								ModBase.Log(ex, "解密设置失败：" + Key, ModBase.LogLevel.Developer, "出现错误");
								text = Conversions.ToString(E.m_TokenizerMap);
								ModBase.m_IdentifierRepository.Set(Key, RuntimeHelpers.GetObjectValue(E.m_TokenizerMap), true, null);
							}
						}
					}
					E.Value = RuntimeHelpers.GetObjectValue(Conversion.CTypeDynamic(text, E.Type));
				}
			}
			catch (Exception ex2)
			{
				ModBase.Log(ex2, "读取设置失败：" + Key, ModBase.LogLevel.Hint, "出现错误");
				E.Value = RuntimeHelpers.GetObjectValue(Conversion.CTypeDynamic(RuntimeHelpers.GetObjectValue(E.m_TokenizerMap), E.Type));
			}
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0008014C File Offset: 0x0007E34C
		private string ForceValue(string Key)
		{
			string result;
			if (Operators.CompareString(Key, "UiLauncherTheme", false) == 0)
			{
				result = "0";
			}
			else if (Operators.CompareString(Key, "UiHiddenPageLink", false) == 0)
			{
				result = Conversions.ToString(true);
			}
			else if (Operators.CompareString(Key, "UiHiddenSetupLink", false) == 0)
			{
				result = Conversions.ToString(true);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x000801A0 File Offset: 0x0007E3A0
		public void LaunchVersionSelect(string Value)
		{
			ModBase.Log("[Setup] 当前选择的 Minecraft 版本：" + Value, ModBase.LogLevel.Normal, "出现错误");
			ModBase.WriteIni(ModMinecraft.m_ProxyTests + "PCL.ini", "Version", Information.IsNothing(ModMinecraft.AddClient()) ? "" : ModMinecraft.AddClient().Name);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x000801FC File Offset: 0x0007E3FC
		public void LaunchFolderSelect(string Value)
		{
			ModBase.Log("[Setup] 当前选择的 Minecraft 文件夹：" + Value.ToString().Replace("$", ModBase.Path), ModBase.LogLevel.Normal, "出现错误");
			ModMinecraft.m_ProxyTests = Value.ToString().Replace("$", ModBase.Path);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0000AB8A File Offset: 0x00008D8A
		public void LaunchRamType(int Type)
		{
			if (ModMain._PolicyIterator != null)
			{
				ModMain._PolicyIterator.RamType(Type);
			}
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0000AB9E File Offset: 0x00008D9E
		public void LaunchSkinType(int Value)
		{
			ModBase.RunInUi(delegate()
			{
				if (!Information.IsNothing(ModMain._PolicyIterator))
				{
					switch (Value)
					{
					case 0:
					case 1:
					case 2:
						ModMain._PolicyIterator.PanSkinID.Visibility = Visibility.Collapsed;
						ModMain._PolicyIterator.PanSkinChange.Visibility = Visibility.Collapsed;
						break;
					case 3:
						ModMain._PolicyIterator.PanSkinID.Visibility = Visibility.Visible;
						ModMain._PolicyIterator.PanSkinChange.Visibility = Visibility.Collapsed;
						break;
					case 4:
						ModMain._PolicyIterator.PanSkinID.Visibility = Visibility.Collapsed;
						ModMain._PolicyIterator.PanSkinChange.Visibility = Visibility.Visible;
						break;
					}
					ModMain._PolicyIterator.CardSkin.TriggerForceResize();
				}
				PageLaunchLeft.infoMapper.Start(null, false);
			}, false);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0000ABBD File Offset: 0x00008DBD
		public void LaunchSkinID(string Value)
		{
			PageLaunchLeft.infoMapper.Start(null, false);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0000ABCB File Offset: 0x00008DCB
		public void ToolDownloadThread(int Value)
		{
			ModNet.m_StateTests = checked(Value + 1);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0000ABD5 File Offset: 0x00008DD5
		public void ToolDownloadCert(bool Value)
		{
			ServicePointManager.ServerCertificateValidationCallback = (Value ? null : ((ModSetup._Closure$__.$IR21-1 == null) ? (ModSetup._Closure$__.$IR21-1 = ((object a0, X509Certificate a1, X509Chain a2, SslPolicyErrors a3) => ((ModSetup._Closure$__.$I21-0 == null) ? (ModSetup._Closure$__.$I21-0 = (() => true)) : ModSetup._Closure$__.$I21-0)())) : ModSetup._Closure$__.$IR21-1));
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00080250 File Offset: 0x0007E450
		public void ToolDownloadSpeed(int Value)
		{
			checked
			{
				if (Value <= 14)
				{
					ModNet.m_TemplateTests = (long)Math.Round(unchecked((double)(checked(Value + 1)) * 0.1 * 1024.0 * 1024.0));
					return;
				}
				if (Value <= 31)
				{
					ModNet.m_TemplateTests = (long)Math.Round(unchecked((double)(checked(Value - 11)) * 0.5 * 1024.0 * 1024.0));
					return;
				}
				if (Value <= 41)
				{
					ModNet.m_TemplateTests = unchecked((long)(checked((Value - 21) * 1024))) * 1024L;
					return;
				}
				ModNet.m_TemplateTests = -1L;
			}
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0000AC06 File Offset: 0x00008E06
		public void UiLauncherTransparent(int Value)
		{
			ModMain._ProcessIterator.Opacity = (double)Value / 1000.0 + 0.4;
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0000AC28 File Offset: 0x00008E28
		public void UiBackgroundColorful(bool Value)
		{
			ModSecret.ViewReader(-1);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0000AC30 File Offset: 0x00008E30
		public void UiBackgroundOpacity(int Value)
		{
			ModMain._ProcessIterator.ImgBack.Opacity = (double)Value / 1000.0;
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x000802F4 File Offset: 0x0007E4F4
		public void UiBackgroundBlur(int Value)
		{
			checked
			{
				if (Value == 0)
				{
					ModMain._ProcessIterator.ImgBack.Effect = null;
				}
				else
				{
					ModMain._ProcessIterator.ImgBack.Effect = new BlurEffect
					{
						Radius = (double)(Value + 1)
					};
				}
				ModMain._ProcessIterator.ImgBack.Margin = new Thickness((double)(0 - (Value + 1)) / 1.8);
			}
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00080358 File Offset: 0x0007E558
		public void UiBackgroundSuit(int Value)
		{
			if (!Information.IsNothing(ModMain._ProcessIterator.ImgBack.Background))
			{
				double width = ((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).ImageSource.Width;
				double height = ((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).ImageSource.Height;
				if (Value == 0)
				{
					if (width < ModMain._ProcessIterator.PanMain.ActualWidth / 2.0 && height < ModMain._ProcessIterator.PanMain.ActualHeight / 2.0)
					{
						Value = 4;
					}
					else
					{
						Value = 2;
					}
				}
				((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).TileMode = TileMode.None;
				((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).Viewport = new Rect(0.0, 0.0, 1.0, 1.0);
				((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
				switch (Value)
				{
				case 1:
					ModMain._ProcessIterator.ImgBack.HorizontalAlignment = HorizontalAlignment.Center;
					ModMain._ProcessIterator.ImgBack.VerticalAlignment = VerticalAlignment.Center;
					((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).Stretch = Stretch.None;
					ModMain._ProcessIterator.ImgBack.Width = ((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).ImageSource.Width;
					ModMain._ProcessIterator.ImgBack.Height = ((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).ImageSource.Height;
					return;
				case 2:
					ModMain._ProcessIterator.ImgBack.HorizontalAlignment = HorizontalAlignment.Stretch;
					ModMain._ProcessIterator.ImgBack.VerticalAlignment = VerticalAlignment.Stretch;
					((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).Stretch = Stretch.UniformToFill;
					ModMain._ProcessIterator.ImgBack.Width = double.NaN;
					ModMain._ProcessIterator.ImgBack.Height = double.NaN;
					return;
				case 3:
					ModMain._ProcessIterator.ImgBack.HorizontalAlignment = HorizontalAlignment.Stretch;
					ModMain._ProcessIterator.ImgBack.VerticalAlignment = VerticalAlignment.Stretch;
					((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).Stretch = Stretch.Fill;
					ModMain._ProcessIterator.ImgBack.Width = double.NaN;
					ModMain._ProcessIterator.ImgBack.Height = double.NaN;
					return;
				case 4:
					ModMain._ProcessIterator.ImgBack.HorizontalAlignment = HorizontalAlignment.Stretch;
					ModMain._ProcessIterator.ImgBack.VerticalAlignment = VerticalAlignment.Stretch;
					((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).Stretch = Stretch.None;
					((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).TileMode = TileMode.Tile;
					((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).Viewport = new Rect(0.0, 0.0, ((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).ImageSource.Width, ((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).ImageSource.Height);
					((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).ViewportUnits = BrushMappingMode.Absolute;
					ModMain._ProcessIterator.ImgBack.Width = double.NaN;
					ModMain._ProcessIterator.ImgBack.Height = double.NaN;
					return;
				case 5:
					ModMain._ProcessIterator.ImgBack.HorizontalAlignment = HorizontalAlignment.Left;
					ModMain._ProcessIterator.ImgBack.VerticalAlignment = VerticalAlignment.Top;
					((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).Stretch = Stretch.None;
					ModMain._ProcessIterator.ImgBack.Width = ((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).ImageSource.Width;
					ModMain._ProcessIterator.ImgBack.Height = ((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).ImageSource.Height;
					return;
				case 6:
					ModMain._ProcessIterator.ImgBack.HorizontalAlignment = HorizontalAlignment.Right;
					ModMain._ProcessIterator.ImgBack.VerticalAlignment = VerticalAlignment.Top;
					((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).Stretch = Stretch.None;
					ModMain._ProcessIterator.ImgBack.Width = ((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).ImageSource.Width;
					ModMain._ProcessIterator.ImgBack.Height = ((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).ImageSource.Height;
					return;
				case 7:
					ModMain._ProcessIterator.ImgBack.HorizontalAlignment = HorizontalAlignment.Left;
					ModMain._ProcessIterator.ImgBack.VerticalAlignment = VerticalAlignment.Bottom;
					((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).Stretch = Stretch.None;
					ModMain._ProcessIterator.ImgBack.Width = ((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).ImageSource.Width;
					ModMain._ProcessIterator.ImgBack.Height = ((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).ImageSource.Height;
					return;
				case 8:
					ModMain._ProcessIterator.ImgBack.HorizontalAlignment = HorizontalAlignment.Right;
					ModMain._ProcessIterator.ImgBack.VerticalAlignment = VerticalAlignment.Bottom;
					((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).Stretch = Stretch.None;
					ModMain._ProcessIterator.ImgBack.Width = ((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).ImageSource.Width;
					ModMain._ProcessIterator.ImgBack.Height = ((ImageBrush)ModMain._ProcessIterator.ImgBack.Background).ImageSource.Height;
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0008096C File Offset: 0x0007EB6C
		public void UiCustomType(int Value)
		{
			if (ModMain.m_OrderIterator != null)
			{
				switch (Value)
				{
				case 0:
					ModMain.m_OrderIterator.PanCustomPreset.Visibility = Visibility.Collapsed;
					ModMain.m_OrderIterator.PanCustomLocal.Visibility = Visibility.Collapsed;
					ModMain.m_OrderIterator.PanCustomNet.Visibility = Visibility.Collapsed;
					ModMain.m_OrderIterator.HintCustom.Visibility = Visibility.Collapsed;
					break;
				case 1:
					ModMain.m_OrderIterator.PanCustomPreset.Visibility = Visibility.Collapsed;
					ModMain.m_OrderIterator.PanCustomLocal.Visibility = Visibility.Visible;
					ModMain.m_OrderIterator.PanCustomNet.Visibility = Visibility.Collapsed;
					ModMain.m_OrderIterator.HintCustom.Visibility = Visibility.Visible;
					ModMain.m_OrderIterator.HintCustom.Text = string.Format("从 PCL 文件夹下的 Custom.xaml 读取主页内容。{0}你可以手动编辑该文件，向主页添加文本、图片、常用网站、快捷启动等功能。", "\r\n");
					ModMain.m_OrderIterator.HintCustom.EventType = "";
					ModMain.m_OrderIterator.HintCustom.EventData = "";
					break;
				case 2:
					ModMain.m_OrderIterator.PanCustomPreset.Visibility = Visibility.Collapsed;
					ModMain.m_OrderIterator.PanCustomLocal.Visibility = Visibility.Collapsed;
					ModMain.m_OrderIterator.PanCustomNet.Visibility = Visibility.Visible;
					ModMain.m_OrderIterator.HintCustom.Visibility = Visibility.Visible;
					ModMain.m_OrderIterator.HintCustom.Text = string.Format("从指定网址联网获取主页内容。服主也可以用于动态更新服务器公告。{0}如果你制作了稳定运行的联网主页，可以点击这条提示投稿，若合格即可加入预设！", "\r\n");
					ModMain.m_OrderIterator.HintCustom.EventType = "打开网页";
					ModMain.m_OrderIterator.HintCustom.EventData = "https://github.com/Hex-Dragon/PCL2/discussions/2528";
					break;
				case 3:
					ModMain.m_OrderIterator.PanCustomPreset.Visibility = Visibility.Visible;
					ModMain.m_OrderIterator.PanCustomLocal.Visibility = Visibility.Collapsed;
					ModMain.m_OrderIterator.PanCustomNet.Visibility = Visibility.Collapsed;
					ModMain.m_OrderIterator.HintCustom.Visibility = Visibility.Collapsed;
					break;
				}
				ModMain.m_OrderIterator.CardCustom.TriggerForceResize();
			}
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00080B48 File Offset: 0x0007ED48
		public void UiLogoType(int Value)
		{
			switch (Value)
			{
			case 0:
				ModMain._ProcessIterator.ShapeTitleLogo.Visibility = Visibility.Collapsed;
				ModMain._ProcessIterator.LabTitleLogo.Visibility = Visibility.Collapsed;
				ModMain._ProcessIterator.ImageTitleLogo.Visibility = Visibility.Collapsed;
				if (!Information.IsNothing(ModMain.m_OrderIterator))
				{
					ModMain.m_OrderIterator.CheckLogoLeft.Visibility = Visibility.Visible;
					ModMain.m_OrderIterator.PanLogoText.Visibility = Visibility.Collapsed;
					ModMain.m_OrderIterator.PanLogoChange.Visibility = Visibility.Collapsed;
				}
				break;
			case 1:
				ModMain._ProcessIterator.ShapeTitleLogo.Visibility = Visibility.Visible;
				ModMain._ProcessIterator.LabTitleLogo.Visibility = Visibility.Collapsed;
				ModMain._ProcessIterator.ImageTitleLogo.Visibility = Visibility.Collapsed;
				if (!Information.IsNothing(ModMain.m_OrderIterator))
				{
					ModMain.m_OrderIterator.CheckLogoLeft.Visibility = Visibility.Collapsed;
					ModMain.m_OrderIterator.PanLogoText.Visibility = Visibility.Collapsed;
					ModMain.m_OrderIterator.PanLogoChange.Visibility = Visibility.Collapsed;
				}
				break;
			case 2:
				ModMain._ProcessIterator.ShapeTitleLogo.Visibility = Visibility.Collapsed;
				ModMain._ProcessIterator.LabTitleLogo.Visibility = Visibility.Visible;
				ModMain._ProcessIterator.ImageTitleLogo.Visibility = Visibility.Collapsed;
				if (!Information.IsNothing(ModMain.m_OrderIterator))
				{
					ModMain.m_OrderIterator.CheckLogoLeft.Visibility = Visibility.Collapsed;
					ModMain.m_OrderIterator.PanLogoText.Visibility = Visibility.Visible;
					ModMain.m_OrderIterator.PanLogoChange.Visibility = Visibility.Collapsed;
				}
				ModBase.m_IdentifierRepository.Load("UiLogoText", true, null);
				break;
			case 3:
				ModMain._ProcessIterator.ShapeTitleLogo.Visibility = Visibility.Collapsed;
				ModMain._ProcessIterator.LabTitleLogo.Visibility = Visibility.Collapsed;
				ModMain._ProcessIterator.ImageTitleLogo.Visibility = Visibility.Visible;
				if (!Information.IsNothing(ModMain.m_OrderIterator))
				{
					ModMain.m_OrderIterator.CheckLogoLeft.Visibility = Visibility.Collapsed;
					ModMain.m_OrderIterator.PanLogoText.Visibility = Visibility.Collapsed;
					ModMain.m_OrderIterator.PanLogoChange.Visibility = Visibility.Visible;
				}
				try
				{
					ModMain._ProcessIterator.ImageTitleLogo.Source = ModBase.Path + "PCL\\Logo.png";
				}
				catch (Exception ex)
				{
					ModMain._ProcessIterator.ImageTitleLogo.Source = null;
					ModBase.Log(ex, "显示标题栏图片失败", ModBase.LogLevel.Msgbox, "出现错误");
				}
				break;
			}
			ModBase.m_IdentifierRepository.Load("UiLogoLeft", true, null);
			if (!Information.IsNothing(ModMain.m_OrderIterator))
			{
				ModMain.m_OrderIterator.CardLogo.TriggerForceResize();
			}
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0000AC4D File Offset: 0x00008E4D
		public void UiLogoText(string Value)
		{
			ModMain._ProcessIterator.LabTitleLogo.Text = Value;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00080DD8 File Offset: 0x0007EFD8
		public void UiLogoLeft(bool Value)
		{
			ModMain._ProcessIterator.PanTitleMain.ColumnDefinitions[0].Width = new GridLength((double)((!Value || !Operators.ConditionalCompareObjectEqual(ModBase.m_IdentifierRepository.Get("UiLogoType", null), 0, false)) ? 1 : 0), GridUnitType.Star);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0000AC5F File Offset: 0x00008E5F
		public void UiHiddenPageLink(bool Value)
		{
			PageSetupUI.HiddenRefresh();
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0000AC5F File Offset: 0x00008E5F
		public void UiHiddenPageDownload(bool Value)
		{
			PageSetupUI.HiddenRefresh();
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0000AC5F File Offset: 0x00008E5F
		public void UiHiddenPageSetup(bool Value)
		{
			PageSetupUI.HiddenRefresh();
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0000AC5F File Offset: 0x00008E5F
		public void UiHiddenPageOther(bool Value)
		{
			PageSetupUI.HiddenRefresh();
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0000AC5F File Offset: 0x00008E5F
		public void UiHiddenFunctionSelect(bool Value)
		{
			PageSetupUI.HiddenRefresh();
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0000AC5F File Offset: 0x00008E5F
		public void UiHiddenFunctionModUpdate(bool Value)
		{
			PageSetupUI.HiddenRefresh();
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0000AC5F File Offset: 0x00008E5F
		public void UiHiddenFunctionHidden(bool Value)
		{
			PageSetupUI.HiddenRefresh();
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0000AC5F File Offset: 0x00008E5F
		public void UiHiddenSetupLaunch(bool Value)
		{
			PageSetupUI.HiddenRefresh();
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0000AC5F File Offset: 0x00008E5F
		public void UiHiddenSetupUi(bool Value)
		{
			PageSetupUI.HiddenRefresh();
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0000AC5F File Offset: 0x00008E5F
		public void UiHiddenSetupLink(bool Value)
		{
			PageSetupUI.HiddenRefresh();
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0000AC5F File Offset: 0x00008E5F
		public void UiHiddenSetupSystem(bool Value)
		{
			PageSetupUI.HiddenRefresh();
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0000AC5F File Offset: 0x00008E5F
		public void UiHiddenOtherHelp(bool Value)
		{
			PageSetupUI.HiddenRefresh();
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0000AC5F File Offset: 0x00008E5F
		public void UiHiddenOtherFeedback(bool Value)
		{
			PageSetupUI.HiddenRefresh();
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0000AC5F File Offset: 0x00008E5F
		public void UiHiddenOtherVote(bool Value)
		{
			PageSetupUI.HiddenRefresh();
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0000AC5F File Offset: 0x00008E5F
		public void UiHiddenOtherAbout(bool Value)
		{
			PageSetupUI.HiddenRefresh();
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0000AC5F File Offset: 0x00008E5F
		public void UiHiddenOtherTest(bool Value)
		{
			PageSetupUI.HiddenRefresh();
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0000AC66 File Offset: 0x00008E66
		public void SystemDebugMode(bool Value)
		{
			ModBase._TokenRepository = Value;
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00080E2C File Offset: 0x0007F02C
		public void SystemDebugAnim(int Value)
		{
			ModAnimation.m_Task = ((Value >= 30) ? 200.0 : ModBase.MathClamp((double)Value * 0.1 + 0.1, 0.1, 3.0));
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0000AC6E File Offset: 0x00008E6E
		public void VersionRamType(int Type)
		{
			if (ModMain.composerRepository != null)
			{
				ModMain.composerRepository.RamType(Type);
			}
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00080E7C File Offset: 0x0007F07C
		public void VersionServerLogin(int Type)
		{
			if (ModMain.composerRepository != null)
			{
				ModBase.WriteIni(ModMinecraft.m_ProxyTests + "PCL.ini", "VersionCache", "");
				if (PageVersionLeft._InstanceConfig != null)
				{
					PageVersionLeft._InstanceConfig = new ModMinecraft.McVersion(PageVersionLeft._InstanceConfig.Name).Load();
					ModLoader.LoaderFolderRun(ModMinecraft._ListenerTests, ModMinecraft.m_ProxyTests, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", false);
				}
			}
		}

		// Token: 0x04000937 RID: 2359
		private readonly Dictionary<string, ModSetup.SetupEntry> m_ValThread;

		// Token: 0x020001AE RID: 430
		private enum SetupSource
		{
			// Token: 0x04000939 RID: 2361
			Normal,
			// Token: 0x0400093A RID: 2362
			Registry,
			// Token: 0x0400093B RID: 2363
			Version
		}

		// Token: 0x020001AF RID: 431
		private class SetupEntry
		{
			// Token: 0x060011F5 RID: 4597 RVA: 0x00080EE8 File Offset: 0x0007F0E8
			public SetupEntry(object Value, object Source = 0, object Encoded = false)
			{
				this.databaseMap = 0;
				try
				{
					this.m_TokenizerMap = RuntimeHelpers.GetObjectValue(Value);
					this._MappingMap = Conversions.ToBoolean(Encoded);
					this.Value = RuntimeHelpers.GetObjectValue(Value);
					this.Source = (ModSetup.SetupSource)Conversions.ToInteger(Source);
					this.Type = (Value ?? new object()).GetType();
					this.filterMap = RuntimeHelpers.GetObjectValue(Conversions.ToBoolean(Encoded) ? ModSecret.SecretEncrypt(Conversions.ToString(Value), "PCL" + ModBase._TagRepository) : Value);
				}
				catch (Exception ex)
				{
					ModBase.Log(ex, "初始化 SetupEntry 失败", ModBase.LogLevel.Feedback, "出现错误");
				}
			}

			// Token: 0x0400093C RID: 2364
			public bool _MappingMap;

			// Token: 0x0400093D RID: 2365
			public object m_TokenizerMap;

			// Token: 0x0400093E RID: 2366
			public object filterMap;

			// Token: 0x0400093F RID: 2367
			public object Value;

			// Token: 0x04000940 RID: 2368
			public ModSetup.SetupSource Source;

			// Token: 0x04000941 RID: 2369
			public byte databaseMap;

			// Token: 0x04000942 RID: 2370
			public Type Type;
		}
	}
}
