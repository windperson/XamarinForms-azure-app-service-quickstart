using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace XamarinForms_client_quickstart
{
    public class LoggingHandler : DelegatingHandler
    {
        private readonly bool _logRequestResponseBody;

        public LoggingHandler(bool logRequestResponseBody = false)
        {
            this._logRequestResponseBody = logRequestResponseBody;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            Debug.WriteLine("Request: {0} {1}", request.Method, request.RequestUri.ToString());

            if (_logRequestResponseBody && request.Content != null)
            {
                var requestContent = await request.Content.ReadAsStringAsync();
                Debug.WriteLine(requestContent);
            }

            Debug.WriteLine("HEADERS");

            foreach (var header in request.Headers)
            {
                Debug.WriteLine(string.Format("{0}:{1}", header.Key, string.Join(",", header.Value)));
            }

            var response = await base.SendAsync(request, cancellationToken);

            Debug.WriteLine("Response: {0}", response.StatusCode);

            if (_logRequestResponseBody)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(responseContent);
            }

            return response;
        }
    }
}