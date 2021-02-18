using System;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using GDL.Protocols;
using GDL.Data;

namespace GDL.UnitTests.Protocols {
	[TestClass]
	public class ProtocolProviderFactoryTests {
		#region Mocks
		private class ProtocolProviderMock : ProtocolProvider {
			public override Stream CreateStream(ContentLocation location, long initialPosition, long endPosition) => null;
			public override ContentInfo GetContentInfo(ContentLocation location) => null;
		}
		#endregion

		#region RegisterProtocolProvider
		[TestMethod]
		public void RegisterProtocolProvider__ProviderTypeIsNull__ShouldReturnFalse() {
			// Act + Assert
			Assert.IsFalse(ProtocolProviderFactory.RegisterProtocolProvider(null, url => true, false));
		}

		[TestMethod]
		public void RegisterProtocolProvider__ProviderTypeIsNotAProvider__ShouldReturnFalse() {
			// Act + Assert
			Assert.IsFalse(ProtocolProviderFactory.RegisterProtocolProvider(typeof(Type), url => true, false));
		}

		[TestMethod]
		public void RegisterProtocolProvider__ProviderTypeIsAbstractProvider__ShouldReturnFalse() {
			// Act + Assert
			Assert.IsFalse(ProtocolProviderFactory.RegisterProtocolProvider(typeof(ProtocolProvider), url => true, false));
		}

		[TestMethod]
		public void RegisterProtocolProvider__ConditionIsNull__ShouldReturnFalse() {
			// Act + Assert
			Assert.IsFalse(ProtocolProviderFactory.RegisterProtocolProvider(typeof(ProtocolProviderMock), null, false));
		}

		[TestMethod]
		public void RegisterProtocolProvider__ProviderAndConditionAreValid__ShouldReturnTrue() {
			// Act + Assert
			Assert.IsTrue(ProtocolProviderFactory.RegisterProtocolProvider(typeof(ProtocolProviderMock), url => true, false));

			// Clean up
			ProtocolProviderFactory.SetInitialValues();
		}

		[TestMethod]
		public void RegisterProtocolProvider__ProviderAlreadyDefinedShouldNotBeOverridden__ShouldReturnFalse() {
			// Arrange
			ProtocolProviderFactory.RegisterProtocolProvider(typeof(ProtocolProviderMock), url => true, false);

			// Act + Assert
			Assert.IsFalse(ProtocolProviderFactory.RegisterProtocolProvider(typeof(ProtocolProviderMock), url => false, false));

			// Clean up
			ProtocolProviderFactory.SetInitialValues();
		}

		[TestMethod]
		public void RegisterProtocolProvider__ProviderAlreadyDefinedAndShouldBeOverridden__ShouldReturnFalse() {
			// Arrange
			ProtocolProviderFactory.RegisterProtocolProvider(typeof(ProtocolProviderMock), url => true, false);

			// Act + Assert
			Assert.IsTrue(ProtocolProviderFactory.RegisterProtocolProvider(typeof(ProtocolProviderMock), url => false, true));

			// Clean up
			ProtocolProviderFactory.SetInitialValues();
		}
		#endregion

		#region DeregisterProtocolProvider
		[TestMethod]
		public void DeregisterProtocolProvider__ProviderIsNull__ShouldReturnFalse() {
			// Act + Assert
			Assert.IsFalse(ProtocolProviderFactory.DeregisterProtocolProvider(null));
		}

		[TestMethod]
		public void DeregisterProtocolProvider__TypeIsNoProvider__ShouldReturnFalse() {
			// Act + Assert
			Assert.IsFalse(ProtocolProviderFactory.DeregisterProtocolProvider(typeof(Type)));
		}

		[TestMethod]
		public void DeregisterProtocolProvider__RegistrationDoesNotContainType__ShouldReturnFalse() {
			// Act + Assert
			Assert.IsFalse(ProtocolProviderFactory.DeregisterProtocolProvider(typeof(ProtocolProviderMock)));
		}

		[TestMethod]
		public void DeregisterProtocolProvider__ValidParameter__ShouldReturnFalse() {
			// Arrange
			ProtocolProviderFactory.RegisterProtocolProvider(typeof(ProtocolProviderMock), url => true, false);

			// Act + Assert
			Assert.IsFalse(ProtocolProviderFactory.DeregisterProtocolProvider(typeof(Type)));

			// Clean up
			ProtocolProviderFactory.SetInitialValues();
		}
		#endregion

		#region CreateProvider
		[TestMethod]
		public void CreateProvider__NoTypeDefined__ShouldReturnNull() {
			// Act + Assert
			Assert.IsNull(ProtocolProviderFactory.CreateProvider("invalid-url"));
		}

		[TestMethod]
		public void CreateProvider__TypeDefined__ShouldReturnInstanceOfSpecifiedType() {
			// Arrange
			string testUrl = "mock-url";
			ProtocolProviderFactory.RegisterProtocolProvider(typeof(ProtocolProviderMock), url => url.Equals(testUrl));

			// Act + Assert
			Assert.IsInstanceOfType(ProtocolProviderFactory.CreateProvider(testUrl), typeof(ProtocolProviderMock));

			// Clean up
			ProtocolProviderFactory.SetInitialValues();
		}

		// Any other Test for this method is done inside the methods this method uses.
		#endregion

		#region GetProviderType
		[TestMethod]
		public void GetProviderType__RegistrationDoesNotContainType__ShouldReturnNull() {
			// Act + Assert
			Assert.IsNull(ProtocolProviderFactory.GetProviderType("invalid-url"));
		}

		[TestMethod]
		public void GetProviderType__TypeDefined__ShouldReturnInstanceOfSpecifiedType() {
			// Arrange
			string testUrl = "mock-url";
			ProtocolProviderFactory.RegisterProtocolProvider(typeof(ProtocolProviderMock), url => url.Equals(testUrl));

			// Act + Assert
			Assert.IsInstanceOfType(ProtocolProviderFactory.CreateProvider(testUrl), typeof(ProtocolProviderMock));
			
			// Clean up
			ProtocolProviderFactory.SetInitialValues();
		}

		[TestMethod]
		public void GetProviderType__MultipleFulfilledConditions__ShouldReturnFirstTypeFullfillsCondition__HttpProtocolProvider() {
			// Arrange
			string testUrl = "https://mock-url";
			ProtocolProviderFactory.RegisterProtocolProvider(typeof(ProtocolProviderMock), url => url.Equals(testUrl));

			// Act + Assert
			Assert.IsInstanceOfType(ProtocolProviderFactory.CreateProvider(testUrl), typeof(HttpProtocolProvider));

			// Clean up
			ProtocolProviderFactory.SetInitialValues();
		}
		#endregion

		#region CreateFromType
		[TestMethod]
		public void CreateFromType__TypeIsNull__ShouldReturnNull() {
			// Act + Assert
			Assert.IsNull(ProtocolProviderFactory.CreateFromType(null));
		}

		[TestMethod]
		public void CreateFromType__TypeIsNoProvider__ShouldReturnNull() {
			// Act + Assert
			Assert.IsNull(ProtocolProviderFactory.CreateFromType(typeof(Type)));
		}

		[TestMethod]
		public void CreateFromType__TypeIsValid__ShouldReturnInstanceOfSpecifiedType() {
			// Arrange
			Type type = typeof(ProtocolProviderMock);

			// Act + Assert
			Assert.IsInstanceOfType(ProtocolProviderFactory.CreateFromType(type), type);
		}
		#endregion
	}
}