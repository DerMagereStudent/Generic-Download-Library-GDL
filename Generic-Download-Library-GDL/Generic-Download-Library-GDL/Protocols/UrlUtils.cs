using System;

namespace GDL.Protocols {
	public static class UrlUtils {
		/// <summary>
		/// Checks if a given url is a valid url for HTTP or HTTPS.
		/// </summary>
		/// <param name="url">The url to check.</param>
		/// <returns>True if the url is valid.</returns>
		public static bool IsWellFormedHttpHttps(string url) => Uri.TryCreate(url, UriKind.Absolute, out Uri result) && (result.Scheme.Equals(Uri.UriSchemeHttp) || result.Scheme.Equals(Uri.UriSchemeHttps));
	}
}