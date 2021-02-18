namespace GDL.Data {
	/// <summary>
	/// A structure representing a convertible amount of bytes.
	/// </summary>
	public struct ContentSize {
		/// <summary>
		/// The number of bytes, this object represents.
		/// </summary>
		public long Bytes { get; set; }

		/// <summary>
		/// The number of kilo bytes, this object represents. Bytes / 2^10.
		/// </summary>
		public float KB => this.Bytes / 1024.0f;

		/// <summary>
		/// The number of mega bytes, this object represents. Bytes / 2^20.
		/// </summary>
		public float MB => this.KB / 1024.0f;

		/// <summary>
		/// The number of giga bytes, this object represents. Bytes / 2^30.
		/// </summary>
		public float GB => this.MB / 1024.0f;

		/// <summary>
		/// The number of tera bytes, this object represents. Bytes / 2^40.
		/// </summary>
		public float TB => this.GB / 1024.0f;

		/// <summary>
		/// The number of peta bytes, this object represents. Bytes / 2^50.
		/// </summary>
		public float PB => this.TB / 1024.0f;

		/// <summary>
		/// The number of exa bytes, this object represents. Bytes / 2^60.
		/// </summary>
		public float EB => this.PB / 1024.0f;

		/// <summary>
		/// Creates a new instance of the struct <see cref="ContentSize"/>.
		/// </summary>
		/// <param name="bytes">The number of bytes, this object should represent.</param>
		public ContentSize(long bytes) {
			this.Bytes = bytes;
		}

		/// <summary>
		/// Implicitly converts from <see cref="ContentSize"/> to <see cref="long"/>.
		/// </summary>
		/// <param name="size">The <see cref="ContentSize"/> object to convert.</param>
		public static implicit operator long(ContentSize size) => size.Bytes;

		/// <summary>
		/// Implicitly converts from <see cref="long"/> to <see cref="ContentSize"/>.
		/// </summary>
		/// <param name="size">The number of bytes as </param>
		public static implicit operator ContentSize(long bytes) => new ContentSize(bytes);
	}
}