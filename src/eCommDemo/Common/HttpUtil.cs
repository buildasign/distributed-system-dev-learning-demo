using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace eCommDemo.Common
{
    public static class HttpUtil
    {
        private static string baseUrl = "http://localhost:59828/";

        public static HttpRequestMessage CreateRequest(string url, HttpMethod method)
        {
            var uri = new Uri(baseUrl + url);

            var request = new HttpRequestMessage
            {
                RequestUri = uri
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Method = method;

            return request;
        }

        public static HttpRequestMessage CreateRequest<T>(string url, HttpMethod method, T content) where T : class
        {
            var request = CreateRequest(url, method);


            request.Content = new ObjectContent<T>(content, new JsonMediaTypeFormatter());

            return request;
        }

        public static T Send<T>(HttpRequestMessage message)
        {
            T parsed;
            using (var client = new HttpClient())
            {
                var result = client.SendAsync(message).Result;


                parsed = result.Content.ReadAsAsync<T>().Result;
            }

            return parsed;
        }
    }
}