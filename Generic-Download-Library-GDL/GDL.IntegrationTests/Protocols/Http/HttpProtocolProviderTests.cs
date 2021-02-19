using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using GDL.Data;
using GDL.Protocols;

namespace GDL.IntegrationTests.Protocols {
	[TestClass]
	public class HttpProtocolProviderTests {
		#region CreateStream
		[TestMethod]
		public void CreateStream__LocationUrlIsValidNoBounds__ShouldReturnStreamWithWholeContent() {
			// Arrange
			HttpProtocolProvider provider = new HttpProtocolProvider();
			provider.Initialize();

			// Act + Assert - 1
			Stream stream = provider.CreateStream(new ContentLocation(ProtocolTestData.TxtTestFileUrlHttp, null), -1, -1);
			Assert.IsNotNull(stream);

			// Act + Assert - 2
			string content = new StreamReader(stream).ReadToEnd();
			Assert.AreEqual(content, ProtocolTestData.TxtTestFileContent);
		}

		[TestMethod]
		public void CreateStream__LocationUrlIsValidInitialPositionIsSet__ShouldReturnStreamWithContentStartingAt10() {
			// Arrange
			HttpProtocolProvider provider = new HttpProtocolProvider();
			provider.Initialize();

			// Act + Assert - 1
			Stream stream = provider.CreateStream(new ContentLocation(ProtocolTestData.TxtTestFileUrlHttp, null), 10, -1);
			Assert.IsNotNull(stream);

			// Act + Assert - 2
			string content = new StreamReader(stream).ReadToEnd();
			Assert.AreEqual(content, ProtocolTestData.TxtTestFileContent.Substring(10));
		}

		[TestMethod]
		public void CreateStream__LocationUrlIsValidBothBoundsSet__ShouldReturnStreamWithContentStartingAt10EndingAt15() {
			// Arrange
			int start = 10;
			int end = 15;
			HttpProtocolProvider provider = new HttpProtocolProvider();
			provider.Initialize();

			// Act + Assert - 1
			Stream stream = provider.CreateStream(new ContentLocation(ProtocolTestData.TxtTestFileUrlHttp, null), start, end);
			Assert.IsNotNull(stream);

			// Act + Assert - 2
			string content = new StreamReader(stream).ReadToEnd();
			Assert.AreEqual(content, ProtocolTestData.TxtTestFileContent.Substring(start, end - start + 1)); // [10, 15] has length 6. Its not [10, 15[
		}

		[TestMethod]
		public void CreateStream__LocationUrlIsValidBoundsDefineSize1__ShouldReturnStreamWithEmptyString() {
			// Arrange
			int start = 9;
			int end = 9;
			HttpProtocolProvider provider = new HttpProtocolProvider();
			provider.Initialize();

			// Act + Assert - 1
			Stream stream = provider.CreateStream(new ContentLocation(ProtocolTestData.TxtTestFileUrlHttp, null), start, end);
			Assert.IsNotNull(stream);

			// Act + Assert - 2
			string content = new StreamReader(stream).ReadToEnd();
			Assert.AreEqual(content, ProtocolTestData.TxtTestFileContent.Substring(start, 1));
		}

		[TestMethod]
		public void CreateStream__LocationUrlIsValidBoundsAreOutOfRange__ShouldReturnStreamWithWholeContent() {
			// Arrange
			int start = -1;
			int end = ProtocolTestData.TxtTestFileContent.Length;
			HttpProtocolProvider provider = new HttpProtocolProvider();
			provider.Initialize();

			// Act + Assert - 1
			Stream stream = provider.CreateStream(new ContentLocation(ProtocolTestData.TxtTestFileUrlHttp, null), start, end);
			Assert.IsNotNull(stream);

			// Act + Assert - 2
			string content = new StreamReader(stream).ReadToEnd();
			Assert.AreEqual(content, ProtocolTestData.TxtTestFileContent);
		}
		#endregion

		#region GetContentInfo
		[TestMethod]
		public void GetContentInfo__LocationUrlIsValid__TestWithTxtFile() {
			// Arrange
			HttpProtocolProvider provider = new HttpProtocolProvider();
			provider.Initialize();

			// Act + Assert - 1
			ContentInfo info = provider.GetContentInfo(new ContentLocation(ProtocolTestData.TxtTestFileUrlHttp, null));
			Assert.IsNotNull(info);

			// Assert - 2
			Assert.AreEqual(info.Size, ProtocolTestData.TxtTestFileContent.Length);
		}

		[TestMethod]
		public void GetContentInfo__LocationUrlIsValid__TestWithBinFile() {
			// Arrange
			HttpProtocolProvider provider = new HttpProtocolProvider();
			provider.Initialize();

			// Act + Assert - 1
			ContentInfo info = provider.GetContentInfo(new ContentLocation(ProtocolTestData.BinTestFile100MBUrlHttp, null));
			Assert.IsNotNull(info);

			// Assert - 2
			Assert.AreEqual(info.Size, 104857600); // 104857600 = 100 * 2^20 = 100MB
		}
		#endregion
	}
}