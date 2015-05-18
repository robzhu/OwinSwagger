using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http;
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

            //Tell swagger to generate documentation based on the XML doc file output from msbuild
            config.EnableSwagger( c =>
            {
                c.IncludeXmlComments( "docs.xml" );
                c.SingleApiVersion( "1.0", "Owin Swashbuckle Demo" );
            } ).EnableSwaggerUi();

            app.UseWebApi( config );
        }
    }
}
