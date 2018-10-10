using System;
using Newtonsoft.Json.Linq;

namespace Apache.OpenWhisk.Tests.Dotnet
{
    public class Echo
    {
        public JObject Main(JObject args)
        {
            return (args);
        }
    }
}