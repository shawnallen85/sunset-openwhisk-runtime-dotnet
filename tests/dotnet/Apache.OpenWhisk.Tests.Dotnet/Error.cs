using Newtonsoft.Json.Linq;

namespace Apache.OpenWhisk.Tests.Dotnet
{
    public class Error
    {
        public JObject Main(JObject args)
        {
            JObject message = new JObject();
            message.Add("error", new JValue("This action is unhappy."));
            return (message);
        }
    }
}