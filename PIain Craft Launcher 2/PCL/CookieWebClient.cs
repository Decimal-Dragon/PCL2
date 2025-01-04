using System;
using System.Collections.Generic;
using System.Net;

namespace PCL
{
	// Token: 0x020000C7 RID: 199
	public class CookieWebClient : WebClient
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x00034258 File Offset: 0x00032458
		public CookieWebClient(CookieContainer container, Dictionary<string, string> Headers) : this(container)
		{
			try
			{
				foreach (KeyValuePair<string, string> keyValuePair in Headers)
				{
					base.Headers[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			finally
			{
				Dictionary<string, string>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00005128 File Offset: 0x00003328
		public CookieWebClient() : this(new CookieContainer())
		{
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00005136 File Offset: 0x00003336
		public CookieWebClient(CookieContainer container)
		{
			this.m_ServerField = new CookieContainer();
			this.resolverField = 600000;
			this.m_ServerField = container;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000342C0 File Offset: 0x000324C0
		protected override WebRequest GetWebRequest(Uri address)
		{
			WebRequest webRequest = base.GetWebRequest(address);
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest != null)
			{
				httpWebRequest.CookieContainer = this.m_ServerField;
				httpWebRequest.Timeout = this.resolverField;
			}
			return webRequest;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000342F8 File Offset: 0x000324F8
		protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
		{
			WebResponse webResponse = base.GetWebResponse(request, result);
			this.ReadCookies(webResponse);
			return webResponse;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00034318 File Offset: 0x00032518
		protected override WebResponse GetWebResponse(WebRequest request)
		{
			WebResponse webResponse = base.GetWebResponse(request);
			this.ReadCookies(webResponse);
			return webResponse;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00034338 File Offset: 0x00032538
		private void ReadCookies(WebResponse r)
		{
			HttpWebResponse httpWebResponse = r as HttpWebResponse;
			if (httpWebResponse != null)
			{
				CookieCollection cookies = httpWebResponse.Cookies;
				this.m_ServerField.Add(cookies);
			}
		}

		// Token: 0x0400035A RID: 858
		private readonly CookieContainer m_ServerField;

		// Token: 0x0400035B RID: 859
		public int resolverField;
	}
}
