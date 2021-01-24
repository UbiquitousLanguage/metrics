using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Ubiquitous.Metrics.Diagnostics {
    public static class RegistrationExtensions {
        /// <summary>
        /// Registers a diagnostic observer in the container. When called for the first time, also registers a hosted service.
        /// <code>Configure</code> methods of your <code>Startup</code>.
        /// </summary>
        /// <param name="services"></param>
        /// <typeparam name="TDiagnosticObserver"></typeparam>
        /// <returns></returns>
        public static IServiceCollection AddDiagnosticObserver<TDiagnosticObserver>(this IServiceCollection services)
            where TDiagnosticObserver : DiagnosticSubscriberBase {
            services.TryAddEnumerable(ServiceDescriptor.Transient<DiagnosticSubscriberBase, TDiagnosticObserver>());

            if (services.All(d => d.ImplementationType != typeof(DiagnosticsService)))
                services.AddSingleton<IHostedService, DiagnosticsService>();

            return services;
        }
    }
}
