using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Apache.OpenWhisk.Runtime.Common
{
    public class Run
    {
        private readonly Type _type;
        private readonly MethodInfo _method;
        private readonly ConstructorInfo _constructor;

        public Run(Type type, MethodInfo method, ConstructorInfo constructor)
        {
            _type = type;
            _method = method;
            _constructor = constructor;
        }

        public async Task HandleRequest(HttpContext httpContext)
        {
            if (_type == null || _method == null || _constructor == null)
            {
                await httpContext.Response.WriteError("Cannot invoke an uninitialized action.");
                return;
            }

            try
            {
                string body = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();

                JObject inputObject = string.IsNullOrEmpty(body) ? null : JObject.Parse(body);

                JObject valObject = null;

                if (inputObject != null)
                {
                    valObject = inputObject["value"] as JObject;
                    foreach (JToken token in inputObject.Children())
                    {
                        try
                        {
                            if (token.Path.Equals("value", StringComparison.InvariantCultureIgnoreCase))
                                continue;
                            string envKey = $"__OW_{token.Path.ToUpperInvariant()}";
                            string envVal = token.First.ToString();
                            Environment.SetEnvironmentVariable(envKey, envVal);
                            //Console.WriteLine($"Set environment variable \"{envKey}\" to \"{envVal}\".");
                        }
                        catch (Exception)
                        {
                            await Console.Error.WriteLineAsync(
                                $"Unable to set environment variable for the \"{token.Path}\" token.");
                        }
                    }
                }

                object owObject = _constructor.Invoke(new object[] { });

                try
                {
                    JObject output = (JObject) _method.Invoke(owObject, new object[] {valObject});

                    if (output == null)
                    {
                        await httpContext.Response.WriteError("The action returned null");
                        Console.Error.WriteLine("The action returned null");
                        return;
                    }

                    await httpContext.Response.WriteResponse(200, output.ToString());
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.StackTrace);
                    await httpContext.Response.WriteError(ex.Message
#if DEBUG
                                                          + ", " + ex.StackTrace
#endif
                    );
                }
            }
            finally
            {
                Startup.WriteLogMarkers();
            }
        }
    }
}