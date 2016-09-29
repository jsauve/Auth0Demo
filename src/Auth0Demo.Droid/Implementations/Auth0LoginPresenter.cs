using System;
using System.Threading.Tasks;
using Android.App;
using Auth0.SDK;
using Auth0Demo.Abstractions;
using Auth0Demo.Config;
using Plugin.CurrentActivity;

namespace Auth0Demo.Droid
{
	public class Auth0LoginPresenter : ILoginUiPresenter
	{
		Auth0Client _Client;

		Activity _PresentingActivity;

		public Auth0LoginPresenter()
		{
			_Client = new Auth0Client(PrivateKeys.Auth0Domain, PrivateKeys.Auth0ClientId);
		}

		public async Task PresentLoginUi(object page)
		{
			// pqge is not used in this implementation because we're taking advantage of the CrossCurrentActivity plugin for Xamarin.Android. Thx @motz!

			_PresentingActivity = CrossCurrentActivity.Current.Activity;

			await PresentAuth0LoginUi();
		}

		async Task PresentAuth0LoginUi()
		{
			try
			{
				var user = await _Client.LoginAsync(_PresentingActivity, withRefreshToken: true);

				Settings.SetUser(user.ConvertToLocalType());
			}
			catch (Exception ex)
			{
				// A TaskCanceledException likely indiactes that the user has tapped the Cancel button in the Auth0 login UI,
				// so we recursively call PresentAuth0LoginUi() until they have successfully logged in.
				if (ex is TaskCanceledException)
				{
					await PresentAuth0LoginUi();
				}
			}

			return;
		}
	}
}

