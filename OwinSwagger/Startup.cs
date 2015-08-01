using System.Collections.Generic;
using System.IO;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;

namespace OwinSwagger
{
    public class Startup
    {
        public void Configuration( IAppBuilder app )
        {
            var config = new HttpConfiguration();

            ConfigureIoC( app, config );

            //Use JSON friendly default settings
            var defaultSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter>{ new StringEnumConverter{ CamelCaseText = true }, }
            };
            JsonConvert.DefaultSettings = () => { return defaultSettings; };

            //Specify JSON as the default media type
            config.Formatters.Clear();
            config.Formatters.Add( new JsonMediaTypeFormatter() );
            config.Formatters.JsonFormatter.SerializerSettings = defaultSettings;

            //Route all requests to the RootController by default
            config.Routes.MapHttpRoute( "api", "{controller}/{id}", defaults: new { id = RouteParameter.Optional } );
            config.MapHttpAttributeRoutes();

            var execPath = Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location );
            //Tell swagger to generate documentation based on the XML doc file output from msbuild
            config.EnableSwagger( c =>
            {
                c.IncludeXmlComments( execPath + "\\docs.xml" );
                c.SingleApiVersion( "1.0", "Owin Swashbuckle Demo" );
            } ).EnableSwaggerUi();

            app.UseWebApi( config );
        }

        private void ConfigureIoC( IAppBuilder app, HttpConfiguration config )
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers( Assembly.GetExecutingAssembly() ).InstancePerRequest();
            builder.RegisterWebApiFilterProvider( config );

            //register type specific shit here
            RegisterRequestTypes( builder );

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver( container );

            app.UseAutofacMiddleware( container );
            app.UseAutofacWebApi( config );
            app.UseWebApi( config );
        }

        private void RegisterRequestTypes( ContainerBuilder builder )
        {
            builder.RegisterType<Library>().As<ILibrary>();
        }
    }
}
