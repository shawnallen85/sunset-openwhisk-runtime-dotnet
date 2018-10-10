using Newtonsoft.Json.Linq;

namespace Apache.OpenWhisk.Tests.Dotnet
{
    public class Nuller
    {
        public JObject Main(JObject args)
        {
            return (null);
        }
    }
}