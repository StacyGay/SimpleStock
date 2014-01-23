using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SimpleStock.Web.FrontEnd.Models.Authorization
{
	public interface IBasicAuthMethod
	{
		bool Authorize(string username, string password);
	}

	public class BasicAuthAttribute : ActionFilterAttribute
	{
		private const string AuthenticationHeaderName = "Authorization";
		private readonly Type _authType;

		public BasicAuthAttribute(Type authType)
		{
			_authType = authType;
		}

		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			if (Authenticate(actionContext))
				return;
			
			var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
			actionContext.Response = response;
		}

		private static string GetHttpRequestHeader(HttpHeaders headers, string headerName)
		{
			if (!headers.Contains(headerName))
				return string.Empty;

			return headers.GetValues(headerName)
							.SingleOrDefault();
		}

		private bool Authenticate(HttpActionContext actionContext)
		{
			var authMethod = (IBasicAuthMethod)Activator.CreateInstance(_authType);
			var headers = actionContext.Request.Headers;

			var authenticationString = GetHttpRequestHeader(headers, AuthenticationHeaderName);
			if (string.IsNullOrEmpty(authenticationString))
				return false;

			var authenticationStringParts = authenticationString.Split(' ');

			if (authenticationStringParts.Length != 2)
				return false;

			authenticationString = authenticationStringParts[1];

			var encoding = Encoding.GetEncoding("iso-8859-1");
			var authDecoded = encoding.GetString(Convert.FromBase64String(authenticationString));

			var authenticationParts = authDecoded.Split(new[] { ":" },
					StringSplitOptions.RemoveEmptyEntries);

			if (authenticationParts.Length != 2)
				return false;

			var username = authenticationParts[0].Trim();
			var password = authenticationParts[1].Trim();

			return authMethod.Authorize(username, password);
		}
	}
}