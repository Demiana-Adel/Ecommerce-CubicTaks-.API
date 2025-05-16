using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class LoggingHandler : DelegatingHandler
{
    private static readonly string logFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\ApiLog.txt";

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var requestBody = request.Content != null ? await request.Content.ReadAsStringAsync() : string.Empty;
        var requestInfo = new StringBuilder();
        requestInfo.AppendLine($"[{DateTime.Now}] REQUEST {request.Method} {request.RequestUri}");
        requestInfo.AppendLine($"Headers: {request.Headers}");
        requestInfo.AppendLine($"Body: {requestBody}");

        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        var responseBody = response.Content != null ? await response.Content.ReadAsStringAsync() : string.Empty;
        requestInfo.AppendLine($"RESPONSE {(int)response.StatusCode} {response.ReasonPhrase}");
        requestInfo.AppendLine($"Response Body: {responseBody}");
        requestInfo.AppendLine("----------------------------------------------------");

        WriteLogToFile(requestInfo.ToString());

        return response;
    }

    private void WriteLogToFile(string log)
    {
        try
        {
            string dir = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.AppendAllText(logFilePath, log);
        }
        catch (Exception ex)
        {
            // Optional: Handle logging errors (e.g., file locked)
        }
    }
}
