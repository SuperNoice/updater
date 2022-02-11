using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Updater.Utils
{
    public class WebHeaderHelper
    {
        public static HttpContentHeaders GetHeader(string url)
        {
            HttpClient client = new HttpClient();
            var task = client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url)).GetAwaiter().GetResult();

            if (task.Content.Headers.LastModified == null)
            {
                throw new Exception("Некоректный ответ");
            }

            return task.Content.Headers;
        }


        public static DateTime GetFileCreateDate(string url)
        {
            var header = GetHeader(url);

            DateTime.TryParse(header.LastModified.ToString(), out DateTime result);

            return result;
        }
    }
}
