using System;
using Newtonsoft.Json.Linq;

namespace Apache.OpenWhisk.Tests.Dotnet
{
    public class AltEcho
    {
        public JObject Main(JObject args)
        {
            Console.WriteLine("hello stdout");
            Console.Error.WriteLine("hello stderr");
            return (args);
        }
    }
}