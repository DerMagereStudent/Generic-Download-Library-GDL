using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using GDL.Data;
using GDL.Protocols;

namespace GDL.IntegrationTests.Protocols {
	[TestClass]
	public class FtpProtocolProviderTests {
		#region CreateStream
		[TestMethod]
		public void CreateStream__LocationUrlIsValidNoBounds__ShouldReturnStreamWithWholeContent() {
			// Arrange
			FtpProtocolProvider provider = new FtpProtocolProvider();
			provider.Initialize();

			// Act + Assert - 1
			Stream stream = provider.CreateStream(new ContentLocation(ProtocolTestData.TxtTestFileUrlFtp, new FtpCredential()), -1, -1);
			Assert.IsNotNull(stream);

			// Act + Assert - 2
			string content = new StreamReader(stream).ReadToEnd();
			Assert.AreEqual(content, ProtocolTestData.TxtTestFileContent);
		}

		[TestMethod]
		public void CreateStream__LocationUrlIsValidInitialPositionIsSet__ShouldReturnStreamWithContentStartingAt10() {
			// Arrange
			FtpProtocolProvider provider = new FtpProtocolProvider();
			provider.Initialize();

			// Act + Assert - 1
			Stream stream = provider.CreateStream(new ContentLocation(ProtocolTestData.TxtTestFileUrlFtp, new FtpCredential()), 10, -1);
			Assert.IsNotNull(stream);

			// Act + Assert - 2
			string content = new StreamReader(stream).ReadToEnd();
			Assert.AreEqual(content, ProtocolTestData.TxtTestFileContent.Substring(10));
		}

		[TestMethod]
		public void CreateStream__LocationUrlIsValidInitialPositionOutOfRange__ShouldReturnStreamWithEmptyString() {
			// Arrange
			int start = ProtocolTestData.TxtTestFileContent.Length;
			FtpProtocolProvider provider = new FtpProtocolProvider();
			provider.Initialize();

			// Act + Assert - 1
			Stream stream = provider.CreateStream(new ContentLocation(ProtocolTestData.TxtTestFileUrlFtp, new FtpCredential()), start, -1);
			Assert.IsNotNull(stream);

			// Act + Assert - 2
			string content = new StreamReader(stream).ReadToEnd();
			Assert.AreEqual(content, "");
		}
		#endregion

		#region GetContentInfo
		[TestMethod]
		public void GetContentInfo__LocationUrlIsValid__TestWithTxtFile() {
			// Arrange
			FtpProtocolProvider provider = new FtpProtocolProvider();
			provider.Initialize();

			// Act + Assert - 1
			ContentInfo info = provider.GetContentInfo(new ContentLocation(ProtocolTestData.TxtTestFileUrlFtp, new FtpCredential()));
			Assert.IsNotNull(info);

			// Assert - 2
			Assert.AreEqual(info.Size, ProtocolTestData.TxtTestFileContent.Length);
		}

		[TestMethod]
		public void GetContentInfo__LocationUrlIsValid__TestWithBinFile() {
			// Arrange
			FtpProtocolProvider provider = new FtpProtocolProvider();
			provider.Initialize();

			// Act + Assert - 1
			ContentInfo info = provider.GetContentInfo(new ContentLocation(ProtocolTestData.BinTestFile100MBUrlFtp, new FtpCredential()));
			Assert.IsNotNull(info);

			// Assert - 2
			Assert.AreEqual(info.Size, 104857600); // 104857600 = 100 * 2^20 = 100MB
		}
		#endregion
	}
}