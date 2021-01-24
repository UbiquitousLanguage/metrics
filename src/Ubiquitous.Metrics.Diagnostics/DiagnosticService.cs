using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Ubiquitous.Metrics.Diagnostics {
    class DiagnosticsService : IHostedService {
        readonly IEnumerable<DiagnosticSubscriberBase> _diagnosticObservers;

        public DiagnosticsService(IEnumerable<DiagnosticSubscriberBase> diagnosticObservers) => _diagnosticObservers = diagnosticObservers;

        public Task StartAsync(CancellationToken cancellationToken) {
            foreach (var diagnosticObserver in _diagnosticObservers) diagnosticObserver.Subscribe(DiagnosticListener.AllListeners);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
