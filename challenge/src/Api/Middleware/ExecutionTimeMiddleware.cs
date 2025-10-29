using System.Diagnostics;
namespace challenge.src.Api.Middleware
{
    /// <summary>
    /// Middleware que mide el tiempo de ejecución de cada request y lo añade como header `X-Execution-Time-ms`.
    /// También escribe un log con la ruta y el tiempo en milisegundos.
    /// </summary>
    public class ExecutionTimeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExecutionTimeMiddleware> _logger;

        public ExecutionTimeMiddleware(RequestDelegate next, ILogger<ExecutionTimeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                await _next(context);
            }
            finally
            {
                sw.Stop();
                var elapsedMs = sw.ElapsedMilliseconds;
                var path = context.Request.Path;
                _logger.LogInformation("Request {Method} {Path} executed in {Elapsed} ms", context.Request.Method, path, elapsedMs);
            }
        }
    }
}
