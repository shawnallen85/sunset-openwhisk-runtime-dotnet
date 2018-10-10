using Newtonsoft.Json.Linq;

namespace Apache.OpenWhisk.Tests.Dotnet
{
    public class Exception
    {
        public JObject Main(JObject args)
        {
            throw (new System.Exception("noooooooo"));
        }
    }
}