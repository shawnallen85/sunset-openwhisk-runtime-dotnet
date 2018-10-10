using Newtonsoft.Json.Linq;

namespace Apache.OpenWhisk.Tests.Dotnet
{
    public class NonEmptyConstructor
    {
        public NonEmptyConstructor(string value)
        {
            System.Console.WriteLine(value);
        }
        public JObject Main(JObject args)
        {
            return (args);
        }
    }
}