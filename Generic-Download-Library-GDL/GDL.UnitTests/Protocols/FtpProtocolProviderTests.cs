using Microsoft.VisualStudio.TestTools.UnitTesting;

using GDL.Data;
using GDL.Protocols;

namespace GDL.UnitTests.Protocols {
	[TestClass]
	public class FtpProtocolProviderTests {
		#region CreateStream
		[TestMethod]
		public void CreateStream__LocationUrlIsNull__ShouldReturnNull() {
			// Arrange
			FtpProtocolProvider provider = new FtpProtocolProvider();
			provider.Initialize();

			// Act + Assert
			Assert.IsNull(provider.CreateStream(new ContentLocation(null, null), -1, -1));
		}

		[TestMethod]
		public void CreateStream__LocationUrlIsNotWellFormed__ShouldReturnNull() {
			// Arrange
			FtpProtocolProvider provider = new FtpProtocolProvider();
			provider.Initialize();

			// Act + Assert
			Assert.IsNull(provider.CreateStream(new ContentLocation("ftp://?", null), -1, -1));
		}

		[TestMethod]
		public void CreateStream__LocationUrlDefinesWrongProtocol__ShouldReturnNull() {
			// Arrange
			FtpProtocolProvider provider = new FtpProtocolProvider();
			provider.Initialize();

			// Act + Assert
			Assert.IsNull(provider.CreateStream(new ContentLocation("http://der-magere-student.com", null), -1, -1));
		}
		#endregion

		#region GetContentInfo
		[TestMethod]
		public void GetContentInfo__LocationUrlIsNull__ShouldReturnNull() {
			// Arrange
			FtpProtocolProvider provider = new FtpProtocolProvider();
			provider.Initialize();

			// Act + Assert
			Assert.IsNull(provider.GetContentInfo(new ContentLocation(null, null)));
		}

		[TestMethod]
		public void GetContentInfo__LocationUrlIsNotWellFormed__ShouldReturnNull() {
			// Arrange
			FtpProtocolProvider provider = new FtpProtocolProvider();
			provider.Initialize();

			// Act + Assert
			Assert.IsNull(provider.GetContentInfo(new ContentLocation("ftp://?", null)));
		}

		[TestMethod]
		public void GetContentInfo__LocationUrlDefinesWrongProtocol__ShouldReturnNull() {
			// Arrange
			FtpProtocolProvider provider = new FtpProtocolProvider();
			provider.Initialize();

			// Act + Assert
			Assert.IsNull(provider.GetContentInfo(new ContentLocation("http://der-magere-student.com", null)));
		}
		#endregion
	}
}