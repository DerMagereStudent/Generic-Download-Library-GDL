using System;

namespace GDL.Data {
	/// <summary>
	/// A structure containing information about a content.
	/// </summary>
	public class ContentInfo {
		/// <summary>
		/// The type of the content.
		/// </summary>
		public string ContentType { get; }

		/// <summary>
		/// True, if the content supports ranges (has the header [accept-ranges: bytes])
		/// </summary>
		public bool AcceptRanges { get; }

		/// <summary>
		/// The size of the content as a convertible size structure.
		/// </summary>
		public ContentSize Size { get; }

		/// <summary>
		/// The date time the content was last modified.
		/// </summary>
		public DateTime LastModified { get; }

		/// <summary>
		/// Creates a new instance of the struct <see cref="ContentInfo"/>
		/// </summary>
		/// <param name="contentType">The type of the content.</param>
		/// <param name="acceptRanges">True, if the content supports ranges (has the header [accept-ranges: bytes])</param>
		/// <param name="size">The size of the content as a convertible size structure.</param>
		/// <param name="lastModified">The date time the content was last modified.</param>
		public ContentInfo(string contentType, bool acceptRanges, ContentSize size, DateTime lastModified) {
			this.ContentType = contentType;
			this.AcceptRanges = acceptRanges;
			this.Size = size;
			this.LastModified = lastModified;
		}
	}
}