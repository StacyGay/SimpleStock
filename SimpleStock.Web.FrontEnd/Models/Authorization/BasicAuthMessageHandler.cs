using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Hosting;
using System.Web.Security;
using SimpleStock.Core.Logging;
using SimpleStock.Core.Utilities;

namespace SimpleStock.Web.FrontEnd.Models.Authorization
{
	public class BasicAuthenticationMessageHandler : DelegatingHandler
	{
		private readonly ILogger _logger;

		private class Credentials
		{
			public string Username { get; set; }
			public string Password { get; set; }
		}

		public BasicAuthenticationMessageHandler(ILogger logger)
		{
			_logger = logger;
		}

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
		{
			if(request.Headers.Authorization == null)
				return base.SendAsync(request, cancellationToken);

			
			var credentials = ExtractCredentials(request.Headers.Authorization);
			if(credentials == null || !ValidateUser(credentials))
				return base.SendAsync(request, cancellationToken);

			var identity = new GenericIdentity(credentials.Username, "Basic");
			request.Properties.Add(HttpPropertyKeys.ClientCertificateKey, new GenericPrincipal(identity, new string[0]));
			
			
			return base.SendAsync(request, cancellationToken);
		}

		private bool ValidateUser(Credentials credentials)
		{
			return Membership.ValidateUser(credentials.Username, credentials.Password);
		}

		private Credentials ExtractCredentials(AuthenticationHeaderValue authHeader)
		{
			try
			{
				if (authHeader == null)
					return null;
				

				if (authHeader.Scheme != "Basic")
					return null;

				var encodedUserPass = authHeader.Parameter.Trim();
				var encoding = Encoding.GetEncoding("iso-8859-1");
				var userPass = encoding.GetString(Convert.FromBase64String(encodedUserPass));
				var parts = userPass.Split(":".ToCharArray());
				return new Credentials { Username = parts[0], Password = parts[1] };
			}
			catch (Exception ex)
			{
				_logger.Log(notes: "Authorization: error getting credentials from header", ex: ex, extraInfo: authHeader.ToJson());
				return null;
			}
		}
	}
}