using System;
using System.Threading.Tasks;
using Auth0.SDK;
using Auth0Demo.Abstractions;
using Auth0Demo.Config;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Auth0Demo.iOS
{
	public class Auth0LoginPresenter : ILoginUiPresenter
	{
		Auth0Client _Client;

		public Auth0LoginPresenter()
		{
			_Client = new Auth0Client(PrivateKeys.Auth0Domain, PrivateKeys.Auth0ClientId);
		}

		public async Task PresentLoginUi(object page)
		{
			var renderer = Platform.GetRenderer(page as Page);
			if (renderer == null)
			{
				renderer = Platform.CreateRenderer(page as Page);
				Platform.SetRenderer(page as Page, renderer);
			}

			await PresentAuth0LoginUi(renderer.ViewController);
		}

		async Task PresentAuth0LoginUi(UIViewController viewController)
		{
			try
			{
				var user = await _Client.LoginAsync(viewController, withRefreshToken: true);

				Settings.SetUser(user.ConvertLocalType());
			}
			catch (Exception ex)
			{
				// A TaskCanceledException likely indiactes that the user has tapped the Cancel button in the Auth0 login UI,
				// so we recursively call PresentAuth0LoginUi() until they have successfully logged in.
				if (ex is TaskCanceledException)
				{
					await PresentAuth0LoginUi(viewController);
				}
			}

			return;
		}
	}
}

