using System.IO;
using System.Net;

using GDL.Data;

namespace GDL.Protocols {
	/// <summary>
	/// The base class of each protocol provider.
	/// </summary>
	public abstract class ProtocolProvider {
		/// <summary>
		/// Creates a <see cref="WebRequest"/> for a specified <see cref="ContentLocation"/>
		/// </summary>
		/// <param name="location">The location of the content.</param>
		/// <returns>The created <see cref="WebRequest"/>.</returns>
		public virtual WebRequest GetRequest(ContentLocation location) {
			WebRequest request = WebRequest.Create(location.Url);
			request.Timeout = 30000;
			return request;
		}

		/// <summary>
		/// Fills in the credential
		/// </summary>
		/// <param name="request">The request to apply the credential to.</param>
		/// <param name="credentials"></param>
		public virtual void FillCredential<T>(T request, NetworkCredential credential) where T : WebRequest {
			if (credential is null)
				return;

			request.Credentials = credential;
		}

		/// <summary>
		/// Checks if the given url is a supported url of this provider.
		/// </summary>
		/// <param name="url">The url to ckeck.</param>
		/// <returns>True if the url is supported.</returns>
		public abstract bool IsValidUrl(string url);

		/// <summary>
		/// Can be used for initializing the provider. This method is called when this object was created by the <see cref="ProtocolProviderFactory"/>
		/// </summary>
		public virtual void Initialize() { }

		/// <summary>
		/// Gets the <see cref="ContentInfo"/> of a content, specified by its <see cref="ContentLocation"/>.
		/// </summary>
		/// <param name="location">The location of the content.</param>
		/// <returns>The content inforamtion.</returns>
		public abstract ContentInfo GetContentInfo(ContentLocation location);

		/// <summary>
		/// Creates a stream to the content, specified by its <see cref="ContentLocation"/>.
		/// </summary>
		/// <param name="location">The location of the content.</param>
		/// <param name="initialPosition">The offset of the stream.</param>
		/// <param name="endPosition">The end position of the stream.</param>
		/// <returns>The created stream.</returns>
		public abstract Stream CreateStream(ContentLocation location, long initialPosition, long endPosition);
	}
}