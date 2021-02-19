using System;

namespace GDL.Protocols {
	public static class UrlUtils {
		/// <summary>
		/// Checks if a given url is a valid url for HTTP or HTTPS.
		/// </summary>
		/// <param name="url">The url to check.</param>
		/// <returns>True if the url is valid.</returns>
		public static bool IsWellFormedHttpHttps(string url) => Uri.TryCreate(url, UriKind.Absolute, out Uri result) && (result.Scheme.Equals(Uri.UriSchemeHttp) || result.Scheme.Equals(Uri.UriSchemeHttps));

		/// <summary>
		/// Checks if a given url is a valid url for FTP.
		/// </summary>
		/// <param name="url">The url to check.</param>
		/// <returns>True if the url is valid.</returns>
		public static bool IsWellFormedFtp(string url) => Uri.TryCreate(url, UriKind.Absolute, out Uri result) && result.Scheme.Equals(Uri.UriSchemeFtp);

		/// <summary>
		/// Checks if a given url is a valid url for FTP. Format ftps://domain/path
		/// </summary>
		/// <param name="url">The url to check.</param>
		/// <returns>True if the url is valid.</returns>
		public static bool IsWellFormedFtps(string url) {
			if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
				return false;

			int index = url.IndexOf("://");
			return index > 0 && url.Substring(0, index).Equals("ftps");
		}
	}
}