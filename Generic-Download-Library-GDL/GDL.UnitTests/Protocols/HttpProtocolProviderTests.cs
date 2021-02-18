using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using GDL.Data;
using GDL.Protocols;

namespace GDL.UnitTests.Protocols {
	[TestClass]
	public class HttpProtocolProviderTests {
		#region CreateStream
		[TestMethod]
		public void CreateStream__LocationUrlIsNull__ShouldReturnNull() {
			// Arrange
			HttpProtocolProvider provider = new HttpProtocolProvider();
			provider.Initialize();

			// Act + Assert
			Assert.IsNull(provider.CreateStream(new ContentLocation(null, null), -1, -1));
		}

		[TestMethod]
		public void CreateStream__LocationUrlIsNotWellFormed__ShouldReturnNull() {
			// Arrange
			HttpProtocolProvider provider = new HttpProtocolProvider();
			provider.Initialize();

			// Act + Assert
			Assert.IsNull(provider.CreateStream(new ContentLocation("https://?", null), -1, -1));
		}

		[TestMethod]
		public void CreateStream__LocationUrlDefinesWrongProtocol__ShouldReturnNull() {
			// Arrange
			HttpProtocolProvider provider = new HttpProtocolProvider();
			provider.Initialize();

			// Act + Assert
			Assert.IsNull(provider.CreateStream(new ContentLocation("ftp://der-magere-student.com", null), -1, -1));
		}
		#endregion

		#region GetContentInfo
		public void GetContentInfo__LocationUrlIsNull__ShouldReturnNull() {
			// Arrange
			HttpProtocolProvider provider = new HttpProtocolProvider();
			provider.Initialize();

			// Act + Assert
			Assert.IsNull(provider.GetContentInfo(new ContentLocation(null, null)));
		}

		[TestMethod]
		public void GetContentInfo__LocationUrlIsNotWellFormed__ShouldReturnNull() {
			// Arrange
			HttpProtocolProvider provider = new HttpProtocolProvider();
			provider.Initialize();

			// Act + Assert
			Assert.IsNull(provider.GetContentInfo(new ContentLocation("https://?", null)));
		}

		[TestMethod]
		public void GetContentInfo__LocationUrlDefinesWrongProtocol__ShouldReturnNull() {
			// Arrange
			HttpProtocolProvider provider = new HttpProtocolProvider();
			provider.Initialize();

			// Act + Assert
			Assert.IsNull(provider.GetContentInfo(new ContentLocation("ftp://der-magere-student.com", null)));
		}
		#endregion
	}
}