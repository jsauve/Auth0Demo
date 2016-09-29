using Auth0.LoginClient;
using Auth0Demo.Abstractions;
using Auth0Demo.Config;
using System;
using System.Threading.Tasks;

namespace Auth0Demo.UWP
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
            await PresentAuth0LoginUi();
        }

        async Task PresentAuth0LoginUi()
        {
            try
            {
                var user = await _Client.LoginAsync(withRefreshToken: true);

                Settings.SetUser(user.ConvertLocalType());
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
