using System;
using System.IO;
using System.Net;

using GDL.Data;

namespace GDL.Protocols {
	/// <summary>
	/// Provider that supports FTP.
	/// </summary>
	public class FtpProtocolProvider : ProtocolProvider {
		/// <summary>
		/// Checks if the given url is a supported url of this provider. Format: ftp://domain/path
		/// </summary>
		/// <param name="url">The url to ckeck.</param>
		/// <returns>True if the url is supported.</returns>
		public override bool IsValidUrl(string url) => UrlUtils.IsWellFormedFtp(url);

		/// <summary>
		/// Creates a stream to the content, specified by its <see cref="ContentLocation"/>.
		/// </summary>
		/// <param name="location">The location of the content.</param>
		/// <param name="initialPosition">The offset of the stream.</param>
		/// <param name="endPosition">Not supported.</param>
		/// <returns>The created stream. Null if invalid location.</returns>
		public override Stream CreateStream(ContentLocation location, long initialPosition, long endPosition) {
			if (!this.IsValidUrl(location.Url))
				return null;

			FtpWebRequest request = this.GetRequest(location) as FtpWebRequest;
			this.FillCredential(request, location.Credential);

			request.Method = WebRequestMethods.Ftp.DownloadFile;

			if (initialPosition > 0)
				request.ContentOffset = initialPosition;

			return request.GetResponse().GetResponseStream();
		}

		/// <summary>
		/// Gets the <see cref="ContentInfo"/> of a content, specified by its <see cref="ContentLocation"/>.
		/// </summary>
		/// <param name="location">The location of the content.</param>
		/// <returns>The content information. Null if invalid location.</returns>
		public override ContentInfo GetContentInfo(ContentLocation location) {
			if (!this.IsValidUrl(location.Url))
				return null;

			FtpWebRequest request;

			string contentType = "";
			DateTime lastModified;
			ContentSize size;

			request = this.GetRequest(location) as FtpWebRequest;
			request.Method = WebRequestMethods.Ftp.GetFileSize;
			this.FillCredential(request, location.Credential);

			using (FtpWebResponse response = request.GetResponse() as FtpWebResponse)
				size = response.ContentLength;

			request = this.GetRequest(location) as FtpWebRequest;
			request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
			this.FillCredential(request, location.Credential);

			using (FtpWebResponse response = request.GetResponse() as FtpWebResponse)
				lastModified = response.LastModified;

			return new ContentInfo(contentType, true, size, lastModified);
		}
	}
}