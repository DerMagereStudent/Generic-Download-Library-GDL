using System;
using System.IO;
using System.Net;

using GDL.Data;

namespace GDL.Protocols {
	/// <summary>
	/// Provider that supports HTTP and HTTPS.
	/// </summary>
	public class HttpProtocolProvider : ProtocolProvider {
		/// <summary>
		/// Creates a stream to the content, specified by its <see cref="ContentLocation"/>.
		/// </summary>
		/// <param name="location">The location of the content.</param>
		/// <param name="initialPosition">The offset of the stream.</param>
		/// <param name="endPosition">The end position of the stream.</param>
		/// <returns>The created stream. Null if invalid location.</returns>
		public override Stream CreateStream(ContentLocation location, long initialPosition, long endPosition) {
			if (!UrlUtils.IsWellFormedHttpHttps(location.Url))
				return null;

			HttpWebRequest request = this.GetRequest(location) as HttpWebRequest;
			this.FillCredential(request, location.Credential);

			if (initialPosition > 0) {
				if (endPosition < 0)
					request.AddRange(initialPosition);
				else
					request.AddRange(initialPosition, endPosition);
			}

			return request.GetResponse().GetResponseStream();
		}

		/// <summary>
		/// Gets the <see cref="ContentInfo"/> of a content, specified by its <see cref="ContentLocation"/>.
		/// </summary>
		/// <param name="location">The location of the content.</param>
		/// <returns>The content information. Null if invalid location.</returns>
		public override ContentInfo GetContentInfo(ContentLocation location) {
			if (!UrlUtils.IsWellFormedHttpHttps(location.Url))
				return null;

			HttpWebRequest request = this.GetRequest(location) as HttpWebRequest;
			this.FillCredential(request, location.Credential);

			HttpWebResponse response = request.GetResponse() as HttpWebResponse;

			string contentType = response.ContentType;
			DateTime lastModified = response.LastModified;
			ContentSize contentSize = response.ContentLength;
			bool acceptRanges = string.Compare(response.Headers["Accept-Ranges"], "bytes", true) == 0;

			return new ContentInfo(contentType, acceptRanges, contentSize, lastModified);
		}
	}
}