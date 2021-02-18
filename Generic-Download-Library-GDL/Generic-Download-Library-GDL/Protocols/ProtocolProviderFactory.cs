using System;
using System.Collections.Generic;
using System.Linq;

namespace GDL.Protocols {
	/// <summary>
	/// Static class that controls and simplfies the protocol provider creation.
	/// </summary>
	public static class ProtocolProviderFactory {
		private static Dictionary<Type, Predicate<string>> registeredProtocolProviders;

		static ProtocolProviderFactory() {
			ProtocolProviderFactory.registeredProtocolProviders = new Dictionary<Type, Predicate<string>>();
		}

		/// <summary>
		/// Registers a <see cref="ProtocolProvider"/> for a specific url case defined by condition.
		/// </summary>
		/// <param name="providerType">The type of the provider which should be instantiated when the condition is fulfilled.</param>
		/// <param name="condition">The condition the url has to fulfill to allow the specified provider to be instantiated.</param>
		/// <param name="overrideCurrent">If and only if true, the current condition of the specified provider will be overridden, if defined.</param>
		/// <returns>True if there where changes made to the registration. False if wrong paramters or not allowed.</returns>
		public static bool RegisterProtocolProvider(Type providerType, Predicate<string> condition, bool overrideCurrent = false) {
			if (providerType is null || condition is null || !typeof(ProtocolProvider).IsAssignableFrom(providerType) || providerType.IsAbstract)
				return false;

			if (ProtocolProviderFactory.registeredProtocolProviders.ContainsKey(providerType) && !overrideCurrent)
				return false;

			ProtocolProviderFactory.registeredProtocolProviders[providerType] = condition;
			return true;
		}

		/// <summary>
		/// Deregisters a <see cref="ProtocolProvider"/>.
		/// </summary>
		/// <param name="providerType">The type of the provider which shoudl be removed.</param>
		/// <returns>True if there where changes made to the registration. False if wrong paramters or not allowed.</returns>
		public static bool DeregisterProtocolProvider(Type providerType) {
			if (providerType is null || !typeof(ProtocolProvider).IsAssignableFrom(providerType) || !ProtocolProviderFactory.registeredProtocolProviders.ContainsKey(providerType))
				return false;

			ProtocolProviderFactory.registeredProtocolProviders.Remove(providerType);
			return true;
		}

		/// <summary>
		/// Creates the corresponding provider for the given url.
		/// </summary>
		/// <param name="url">The url which should be handled.</param>
		/// <returns>The created provider. Null if no valid provider was found.</returns>
		public static ProtocolProvider CreateProvider(string url) {
			Type providerType = ProtocolProviderFactory.GetProviderType(url);

			if (providerType is null)
				return null;

			return CreateFromType(providerType);
		}

		/// <summary>
		/// Returns the type of the protocol provider which should be used to handle the given url.
		/// </summary>
		/// <param name="url">The url the provider should handle.</param>
		/// <returns>The type of the protocol provider. Null if no valid provider was found.</returns>
		public static Type GetProviderType(string url) {
			return ProtocolProviderFactory.registeredProtocolProviders
				.Where(kvp => kvp.Value.Invoke(url))
				.Select(kvp => kvp.Key)
				.FirstOrDefault();
		}

		/// <summary>
		/// Creates a provider from a given type and calls <see cref="ProtocolProvider.Initialize"/>.
		/// </summary>
		/// <param name="type">The type of the provider.</param>
		/// <returns>The created provider. Null if the type does not represent a <see cref="ProtocolProvider"/>.</returns>
		public static ProtocolProvider CreateFromType(Type type) {
			if (!typeof(ProtocolProvider).IsAssignableFrom(type))
				return null;

			ProtocolProvider provider = Activator.CreateInstance(type) as ProtocolProvider;
			provider.Initialize();

			return provider;
		}
	}
}