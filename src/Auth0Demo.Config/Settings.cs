using System;
using Auth0Demo.MappedTypes;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Auth0Demo.Config
{
    /// <summary>
    /// This class uses the Xamarin settings plugin (Plugins.Settings) to implement cross-platform storing of settings.
    /// </summary> 

	/*  DO NOT USE THIS SETTINGS MECHANISM TO STORE TOKENS IN A LIVE PRODUCTION ENVIRONMENT!!! YOUR USERS WILL BE PUT AT RISK!!!       */
	/*  Instead store them in some kind of secure storage mechanism, such as the iOS keychain or encrypted SQLite.                     */
	/*  We're just storing them using Xam.Plugins.Settings for convenience for this demo. But it IS NOT secure. You have been warned.  */

    public class Settings
    {
		private static ISettings AppSettings => CrossSettings.Current;

        public static bool LoggedIn => !string.IsNullOrWhiteSpace(UserIdToken);
		public static bool LoggedOut => !LoggedIn;

        public static void SetUser(MappedTypes.Auth0User user)
        {
            UserIdToken = user.IdToken;
            UserAuth0AccessToken = user.Auth0AccessToken;
            JObjectProfileString = user.JObjectProfileString;
            RefreshToken = user.RefreshToken;
        }

        public static MappedTypes.Auth0User GetUser()
        {
            return new Auth0User()
            {
                IdToken = UserIdToken,
                Auth0AccessToken = UserAuth0AccessToken,
                JObjectProfileString = JObjectProfileString,
                RefreshToken = UserRefreshTokenKey
            };
        }

        private const string UserIdTokenKey = "UserIdToken_key";
        private static readonly string UserIdTokenDefault = "";
        private static string UserIdToken
        {
            get { return AppSettings.GetValueOrDefault<string>(UserIdTokenKey, UserIdTokenDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserIdTokenKey, value); }
        }

        private const string UserAuth0AccessTokenKey = "UserAuth0AccessToken_key";
        private static readonly string UserAuth0AccessTokenDefault = "";
        private static string UserAuth0AccessToken
        {
            get { return AppSettings.GetValueOrDefault<string>(UserAuth0AccessTokenKey, UserAuth0AccessTokenDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserAuth0AccessTokenKey, value); }
        }

        private const string UserJObjectProfileStringKey = "UserJObjectProfileString_key";
        private static readonly string UserJObjectProfileStringDefault = "";
        private static string JObjectProfileString
        {
            get { return AppSettings.GetValueOrDefault<string>(UserJObjectProfileStringKey, UserJObjectProfileStringDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserJObjectProfileStringKey, value); }
        }

        private const string UserRefreshTokenKey = "UserRefreshToken_key";
        private static readonly string UserRefreshTokenDefault = "";
        private static string RefreshToken
        {
            get { return AppSettings.GetValueOrDefault<string>(UserRefreshTokenKey, UserRefreshTokenDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserRefreshTokenKey, value); }
        }


    }
}

