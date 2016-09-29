using System;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Auth0Demo.iOS
{
	// Since We're unable to store complex types using Xam.Plugins.Settings, there's a type that closely mirrors Auth0.SDK.Auth0User called MappedTypes.Auth0User.
	// This extension class allows us to easily convert between the two types as needed.
	public static class Auth0UserExtensions
	{
		public static Auth0.SDK.Auth0User ConvertToSdkType(this MappedTypes.Auth0User localType)
		{
			var result = Mapper.Map<Auth0.SDK.Auth0User>(localType);

			result.Profile = JsonConvert.DeserializeObject<JObject>(localType.JObjectProfileString);

			return result;
		}

		public static MappedTypes.Auth0User ConvertLocalType(this Auth0.SDK.Auth0User sdkType)
		{
			var result = Mapper.Map<MappedTypes.Auth0User>(sdkType);

			result.JObjectProfileString = JsonConvert.SerializeObject(sdkType.Profile);

			return result;
		}
	}
}

