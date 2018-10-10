using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Apache.OpenWhisk.Runtime.Common
{
    public class Startup
    {
        public static void WriteLogMarkers()
        {
            Console.WriteLine("XXX_THE_END_OF_A_WHISK_ACTIVATION_XXX");
            Console.Error.WriteLine("XXX_THE_END_OF_A_WHISK_ACTIVATION_XXX");
            //await Console.Error.FlushAsync();
        }
        
        public void Configure(IApplicationBuilder app)
        {
            PathString initPath = new PathString("/init");
            PathString runPath = new PathString("/run");
            Init init = new Init();
            Run run = null;
            app.Run(async (httpContext) =>
                {
                    if (httpContext.Request.Path.Equals(initPath))
                    {
                        run = await init.HandleRequest(httpContext);
                        return;
                    }

                    if (httpContext.Request.Path.Equals(runPath))
                    {
                        if (!init.Initialized)
                        {
                            await httpContext.Response.WriteError("Cannot invoke an uninitialized action.");
                            return;
                        }

                        if (run == null)
                        {
                            await httpContext.Response.WriteError("Cannot invoke an uninitialized action.");
                            return;
                        }

                        await run.HandleRequest(httpContext);
                    }
                }
            );
        }
    }
}
