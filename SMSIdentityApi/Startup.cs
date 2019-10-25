using Autofac;
using Autofac.Integration.Owin;
using Autofac.Integration.WebApi;
using Data;
using InfraStructure.Repository;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(SMSIdentityApi.Startup))]

namespace SMSIdentityApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            HttpConfiguration config = new HttpConfiguration();
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.SuppressDefaultHostAuthentication();

            // DI using Autofac
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            //var config = GlobalConfiguration.Configuration;

            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CableIdentityDbContext>().InstancePerRequest();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterWebApiModelBinderProvider();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            IUserRepository userRepository = new UserRepository(new CableIdentityDbContext());
            //using (var scope = container.sc())
            //{
            //    userRepository = scope.Resolve<IUserRepository>();
            //}
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                 routeTemplate: "api/{controller}/{action}/{id}", //"api/{controller}/{id}"
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            //var userRepository = (IUserRepository)DependencyResolver.Current.GetService(typeof(IUserRepository));
            ConfigureOAuth(app, userRepository);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app, IUserRepository userRepository)
        {

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new CustomOAuthProvider(userRepository),
                AccessTokenFormat = new CustomJwtFormat("http://jwtauthzsrv.azurewebsites.net"),
                AuthenticationMode = AuthenticationMode.Active
            };

            // OAuth 2.0 Bearer Access Token Generation
            //app.UseOAuthBearerAuthentication(OAuthServerOptions);
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());



        }
    }
}
