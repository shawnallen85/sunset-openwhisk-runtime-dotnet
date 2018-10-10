using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Apache.OpenWhisk.Runtime.Common
{
    public static class HttpResponseExtension
    {
        public static async Task WriteResponse(this HttpResponse response, int code, string content)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            response.ContentLength = bytes.Length;
            response.StatusCode = code;
            await response.WriteAsync(content);
        }

        public static async Task WriteError(this HttpResponse response, string errorMessage)
        {
            JObject message = new JObject {{"error", new JValue(errorMessage)}};
            await WriteResponse(response, 502, JsonConvert.SerializeObject(message));
        }

    }
}