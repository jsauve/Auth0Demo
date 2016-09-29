using Android.App;
using Android.Content.PM;
using Android.OS;
using Auth0Demo.Abstractions;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Auth0Demo.Droid
{
	[Activity(Label = "Auth0Demo", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
		// an IoC Container
		IContainer _IoCContainer;

        protected override void OnCreate(Bundle bundle)
        {
			InitializeAutoMapper();

			RegisterDependencies();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);

            LoadApplication(new App());
        }

		// We're using AutoMapper to simplify copying between some types
		void InitializeAutoMapper()
		{
			Mapper.Initialize(cfg => {
				cfg.CreateMap<Auth0.SDK.Auth0User, MappedTypes.Auth0User>();
				cfg.CreateMap<MappedTypes.Auth0User, Auth0.SDK.Auth0User>();
			});
		}

		// We're using AutoFac as an IoC container for executing platform-specific behaviors from within the cross-platform Xamarin.Forms project (Auth0Demo).
		void RegisterDependencies()
		{
			var builder = new ContainerBuilder();

			builder.RegisterInstance(new Auth0LoginPresenter()).As<ILoginUiPresenter>();

			_IoCContainer = builder.Build();

			var csl = new AutofacServiceLocator(_IoCContainer);
			ServiceLocator.SetLocatorProvider(() => csl);
		}
    }
}

