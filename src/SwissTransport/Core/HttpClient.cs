namespace SwissTransport.Core
{
    using System;
    using System.Net;

    public class HttpClient : IHttpClient
    {
        private readonly WebClient client;

        public HttpClient(ICredentials credentials, IWebProxy proxy)
        {
            this.client = new WebClient
            {
                Credentials = credentials ?? throw new ArgumentNullException(nameof(credentials)),
                Proxy = proxy ?? throw new ArgumentNullException(nameof(proxy))
            };
        }

        public void Dispose()
        {
            this.client?.Dispose();
        }

        public string GetString(Uri uri)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            return this.client.DownloadString(uri);
        }

        public TObject GetObject<TObject>(Uri uri, Func<string, TObject> converter)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            return converter.Invoke(GetString(uri));
        }
    }
}