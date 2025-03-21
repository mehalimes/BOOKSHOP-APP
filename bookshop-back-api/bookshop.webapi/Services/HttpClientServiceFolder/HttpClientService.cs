namespace bookshop.webapi.Services.HttpClientServiceFolder
{
    public class HttpClientService
    {
        public readonly HttpClient client;

        public HttpClientService(IHttpClientFactory factory)
        {
            client = factory.CreateClient();
        }
    }
}
