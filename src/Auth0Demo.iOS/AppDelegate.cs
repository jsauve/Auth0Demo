using Auth0Demo.Abstractions;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using AutoMapper;
using Foundation;
using Microsoft.Practices.ServiceLocation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Auth0Demo.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        // an IoC Container
        IContainer _IoCContainer;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            InitializeAutoMapper();

            RegisterDependencies();

            Forms.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
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
