using Domain.Framework.Logging;
using Polly;
using Polly.CircuitBreaker;

namespace Pumpkin.Infrastructure.ExternalServices.Helpers.Polly.CircuitBreaker;

public static class PollyHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="failureThreshold">Break on >=X% actions result in handled exceptions...</param>
    /// <param name="samplingDuration">over any X second period</param>
    /// <param name="minimumThroughput">provided at least 8 actions in the 10 second period.</param>
    /// <param name="durationOfBreak">Break for X seconds.</param>
    /// <returns></returns>
    public static AsyncCircuitBreakerPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(
        double? failureThreshold = null,
        int? samplingDuration = null,
        int? minimumThroughput = null,
        int? durationOfBreak = null)
    {
        return
            Policy<HttpResponseMessage>.Handle<Exception>()
                .Or<InvalidOperationException>()
                .Or<HttpRequestException>()
                .Or<TimeoutException>()
                .Or<TaskCanceledException>()
                .AdvancedCircuitBreakerAsync(
                    failureThreshold: failureThreshold.Value, // Break on >=90% actions result in handled exceptions...
                    samplingDuration: TimeSpan.FromSeconds(samplingDuration.Value), // ... over any x second period
                    minimumThroughput: minimumThroughput.Value, // ... provided at least y actions in the x second period.
                    durationOfBreak: TimeSpan.FromSeconds(durationOfBreak.Value), // Break for x seconds.
                    onBreak: async (ex, ts) =>
                    {
                        LogManager.GetLogger("CircuitBreaker").Info($"Break Occured at {DateTime.UtcNow}");

                        await Task.CompletedTask;
                    },
                    onHalfOpen: async () =>
                    {
                        LogManager.GetLogger("CircuitBreaker").Info($"HalfOpen Occured at {DateTime.UtcNow}");

                        await Task.CompletedTask;
                    },
                    onReset: async () =>
                    {
                        LogManager.GetLogger("CircuitBreaker").Info($"Reset Occured at {DateTime.UtcNow}");

                        await Task.CompletedTask;
                    }
                );
    }
}