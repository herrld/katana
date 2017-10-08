using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace katana
{
    using System.IO;
    using System.Web.Http;
    using Appfunc = Func<IDictionary<string, object>, Task>;

    class Program
    {
        static void Main(string[] args)
        {
            string uri = "http://localhost:8888";
            using (WebApp.Start<Startup>(uri))
            {
                Console.WriteLine("started");
                Console.ReadKey();
            }
        }

        public class Startup
        {

            public void Configuration(IAppBuilder app)
            {

                app.Use(async (env, next) =>
                {
                    Console.WriteLine("requesting:" + env.Request.Path);
                    await next();
                    Console.WriteLine("Response: " + env.Response.StatusCode);
                });
                ConfigureWebApi(app);
                //app.UseHelloWorld();
                app.UseWelcomePage();

                //app.Run(ctx => {
                //    return ctx.Response.WriteAsync("Hello world");
                //});
            }

            private void ConfigureWebApi(IAppBuilder app)
            {
                var config = new HttpConfiguration();
                config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}",
                    new { id = RouteParameter.Optional });

                app.UseWebApi(config);
            }
        }


    }

    public static class AppBuilderExtentions
    {
        public static void UseHelloWorld(this IAppBuilder app)
        {
            app.Use<HelloWorldComponent>();
        }
    }


    public class HelloWorldComponent
    {
        Appfunc next;
        public HelloWorldComponent(Appfunc next)
        {
            this.next = next;
        }

        public Task Invoke(IDictionary<string, object> environment)
        {
            var response = environment["owin.ResponseBody"] as Stream;
            using (var writer = new StreamWriter(response))
            {
                return writer.WriteAsync("Hello from stream");
            }
            //await next(environment);
        }
    }
}
