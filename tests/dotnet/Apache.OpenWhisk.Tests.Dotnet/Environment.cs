using Newtonsoft.Json.Linq;

namespace Apache.OpenWhisk.Tests.Dotnet
{
    public class Environment
    {
        public JObject Main(JObject args)
        {
            JObject message = new JObject();
            message.Add("api_host", new JValue(System.Environment.GetEnvironmentVariable("__OW_API_HOST")));
            message.Add("api_key", new JValue(System.Environment.GetEnvironmentVariable("__OW_API_KEY")));
            message.Add("namespace", new JValue(System.Environment.GetEnvironmentVariable("__OW_NAMESPACE")));
            message.Add("action_name", new JValue(System.Environment.GetEnvironmentVariable("__OW_ACTION_NAME")));
            message.Add("activation_id", new JValue(System.Environment.GetEnvironmentVariable("__OW_ACTIVATION_ID")));
            message.Add("deadline", new JValue(System.Environment.GetEnvironmentVariable("__OW_DEADLINE")));
            return (message);
        }
    }
}