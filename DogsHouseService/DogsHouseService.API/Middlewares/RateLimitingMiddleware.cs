using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace DogsHouseService.API.Middlewares
{
    public class RateLimitingMiddleware : ControllerBase
    {
        private readonly RequestDelegate _next;
        private readonly int _maxRequests;
        private readonly TimeSpan _timeWindow;
        private readonly ConcurrentDictionary<string, (DateTime Timestamp, int Count)> _requestCounts = new();

        public RateLimitingMiddleware(RequestDelegate next, int maxRequests, TimeSpan timeWindow)
        {
            _next = next;
            _maxRequests = maxRequests;
            _timeWindow = timeWindow;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientIp = context.Connection.RemoteIpAddress.ToString();
            var currentTime = DateTime.UtcNow;
            var entry = _requestCounts.GetOrAdd(clientIp, _ => (currentTime, 0));

            if (currentTime - entry.Timestamp > _timeWindow)
            {
                entry = (currentTime, 0);
                _requestCounts[clientIp] = entry;
            }

            entry.Count++;

            if (entry.Count > _maxRequests)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too many requests. Please try again later.");
                return;
            }

            _requestCounts[clientIp] = entry;
            await _next(context);
        }
    }
}
