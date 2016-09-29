using Auth0Demo.Abstractions;
using Auth0Demo.Config;
using Microsoft.Practices.ServiceLocation;

using Xamarin.Forms;

namespace Auth0Demo
{
	public partial class MainPage : ContentPage
	{
		ILoginUiPresenter _LoginUiPresenter;

		public MainPage()
		{
			InitializeComponent();

			_LoginUiPresenter = ServiceLocator.Current.GetInstance<ILoginUiPresenter>();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			if (!Settings.LoggedIn)
				await _LoginUiPresenter.PresentLoginUi(this);
		}
	}
}
