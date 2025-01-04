using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

namespace PCL.XamlGeneratedNamespace
{
	// Token: 0x02000215 RID: 533
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DebuggerNonUserCode]
	public sealed class GeneratedInternalTypeHelper : InternalTypeHelper
	{
		// Token: 0x06001895 RID: 6293 RVA: 0x0000DB07 File Offset: 0x0000BD07
		protected override object CreateInstance(Type type, CultureInfo culture)
		{
			return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, culture);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0000DB17 File Offset: 0x0000BD17
		protected override object GetPropertyValue(PropertyInfo propertyInfo, object target, CultureInfo culture)
		{
			return propertyInfo.GetValue(RuntimeHelpers.GetObjectValue(target), BindingFlags.Default, null, null, culture);
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0000DB29 File Offset: 0x0000BD29
		protected override void SetPropertyValue(PropertyInfo propertyInfo, object target, object value, CultureInfo culture)
		{
			propertyInfo.SetValue(RuntimeHelpers.GetObjectValue(target), RuntimeHelpers.GetObjectValue(value), BindingFlags.Default, null, null, culture);
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x000A04D0 File Offset: 0x0009E6D0
		protected override Delegate CreateDelegate(Type delegateType, object target, string handler)
		{
			return (Delegate)target.GetType().InvokeMember("_CreateDelegate", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, RuntimeHelpers.GetObjectValue(target), new object[]
			{
				delegateType,
				handler
			}, null);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0000DB42 File Offset: 0x0000BD42
		protected override void AddEventHandler(EventInfo eventInfo, object target, Delegate handler)
		{
			eventInfo.AddEventHandler(RuntimeHelpers.GetObjectValue(target), handler);
		}
	}
}
