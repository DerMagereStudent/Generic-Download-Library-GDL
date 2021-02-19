using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using GDL.Data;

namespace GDL.Protocols {
	/// <summary>
	/// Provider that supports FTPS.
	/// </summary>
	public class FtpsProtocolProvider : FtpProtocolProvider {
		/// <summary>
		/// Class used by the WebRequest.Create factory to create FTP requests
		/// </summary>
		public class FtpsWebRequestCreator : IWebRequestCreate {
			public FtpsWebRequestCreator() {
			}
			public WebRequest Create(Uri uri) => WebRequest.Create(uri.AbsoluteUri.Replace("ftps://", "ftp://"));
		}

		static FtpsProtocolProvider() {
			WebRequest.RegisterPrefix("ftps", new FtpsWebRequestCreator());
		}

		/// <summary>
		/// Checks if the given url is a supported url of this provider. Format: [ftp|ftps]://domain/path?query
		/// </summary>
		/// <param name="url">The url to ckeck.</param>
		/// <returns>True if the url is supported.</returns>
		public override bool IsValidUrl(string url) => UrlUtils.IsWellFormedFtp(url) || UrlUtils.IsWellFormedFtps(url);

		/// <summary>
		/// Creates a <see cref="FtpWebRequest"/> for a specified <see cref="ContentLocation"/>, enables SSL, and overrides the current certificate check with the <see cref="FtpsProtocolProvider.CheckServerCertificate"/>.
		/// </summary>
		/// <param name="location">The location of the content.</param>
		/// <returns>The created <see cref="FtpWebRequest"/> as a <see cref="WebRequest"/>.</returns>
		public override WebRequest GetRequest(ContentLocation location) {
			FtpWebRequest request = base.GetRequest(location) as FtpWebRequest;
			request.EnableSsl = true;
			ServicePointManager.ServerCertificateValidationCallback = this.CheckServerCertificate;
			return request;
		}

		/// <summary>
		/// Checks if the server certificate is valid. Accepts every certificate. Override to specify.
		/// </summary>
		/// <param name="sender">An object that contains state information for this validation.</param>
		/// <param name="certificate">The certificate used to authenticate the remote party.</param>
		/// <param name="chain">The chain of certificate authorities associated with the remote certificate.</param>
		/// <param name="sslPolicyErrors">One or more errors associated with the remote certificate.</param>
		/// <returns>True if the certificate was accpeted.</returns>
		public virtual bool CheckServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
	}
}