namespace SwissTransport.Core
{
    using System;

    public interface IHttpClient : IDisposable
    {
        string GetString(Uri uri);

        TObject GetObject<TObject>(Uri uri, Func<string, TObject> converter);
    }
}