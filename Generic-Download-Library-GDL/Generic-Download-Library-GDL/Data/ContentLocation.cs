using System.Net;

namespace GDL.Data {
	public struct ContentLocation {
		/// <summary>
		/// The url of the content.
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// The credential used for protocol authentication.
		/// </summary>
		public NetworkCredential Credential { get; set; }

		/// <summary>
		/// Creates a new instance of the struct <see cref="ContentLocation"/>
		/// </summary>
		/// <param name="url">The url of the resource.</param>
		/// <param name="credentials">The credential used for protocol authentication. Pass null if no authentication is necessary.</param>
		public ContentLocation(string url, NetworkCredential credential) {
			this.Credential = credential;
			this.Url = url;
		}
	}
}