using System.Diagnostics;
using Microsoft.Extensions.Hosting;

namespace Ubiquitous.Metrics.Diagnostics;

class DiagnosticsService(IEnumerable<DiagnosticSubscriberBase> diagnosticObservers) : IHostedService {
    public Task StartAsync(CancellationToken cancellationToken) {
        foreach (var diagnosticObserver in diagnosticObservers)
            diagnosticObserver.Subscribe(DiagnosticListener.AllListeners);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
