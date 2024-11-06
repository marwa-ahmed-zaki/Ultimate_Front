using System.Net.Http.Headers;

namespace Ultimate_Front
{
  
        public static class HttpClientServiceExtensions
        {
            public static void AddHttpClientFor<TInterface, TImplementation>(this IServiceCollection services, string baseUrl)
                where TInterface : class
                where TImplementation : class, TInterface
            {
                services.AddHttpClient<TInterface, TImplementation>(client =>
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                });
            }
        }
}
