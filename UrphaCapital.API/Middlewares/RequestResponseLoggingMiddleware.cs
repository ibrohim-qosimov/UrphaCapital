using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
namespace UrphaCapital.API.Middlewares;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();
        var stopwatch = Stopwatch.StartNew();
        // Read and log request details
        var requestBody = await ReadRequestBodyAsync(context.Request);
        var requestHeaders = GetFormattedHeaders(context.Request.Headers);
        // Copy the original response body stream
        var originalBodyStream = context.Response.Body;
        using (var newBodyStream = new MemoryStream())
        {
            context.Response.Body = newBodyStream;
            await _next(context);
            // Log response details
            stopwatch.Stop();
            var responseTime = stopwatch.ElapsedMilliseconds;
            context.Response.Body = originalBodyStream;
            newBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(newBodyStream).ReadToEndAsync();
            var responseHeaders = GetFormattedHeaders(context.Response.Headers);
            var model = new
            {
                Request = new
                {
                    context.Request.Method,
                    context.Request.Path,
                    QueryString = context.Request.QueryString.ToString(),
                    Headers = requestHeaders,
                    Body = requestBody
                },
                Response = new
                {
                    context.Response.StatusCode,
                    Body = responseBody,
                    ResponseTime = responseTime,
                    Headers = responseHeaders
                }
            };
            _logger.LogInformation("Request and Response: {@LogDetails}", model);
            var message = $"{model.Request.Method} {model.Request.Body} {model.Request.Path} " +
                          $"{model.Request.Headers} {model.Request.QueryString} " +
                          $"{model.Response.Body} {model.Response.Headers} " +
                          $"{model.Response.ResponseTime} {model.Response.StatusCode}";
            // Copy the response body back to the original stream
            newBodyStream.Seek(0, SeekOrigin.Begin);
            await newBodyStream.CopyToAsync(originalBodyStream);
        }
    }
    private async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        request.EnableBuffering(); // Allow rewinding the request stream
        using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
        {
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0; // Rewind the stream
            return body;
        }
    }
    private string GetFormattedHeaders(IHeaderDictionary headers)
    {
        var headerDict = new Dictionary<string, string>();
        foreach (var header in headers)
        {
            headerDict[header.Key] = string.Join(", ", header.Value);
        }
        return JsonConvert.SerializeObject(headerDict, Formatting.Indented);
    }
}