using System.Diagnostics;
using Microsoft.Extensions.DiagnosticAdapter;

namespace Ubiquitous.Metrics.Diagnostics;

public abstract class DiagnosticSubscriberBase {
    readonly IObserver<DiagnosticListener> _observer;

    protected DiagnosticSubscriberBase() => _observer = new DiagnosticObserver(IsMatch, this);

    protected abstract bool IsMatch(string name);

    public void Subscribe(IObservable<DiagnosticListener> allListeners) => allListeners.Subscribe(_observer);

    protected virtual Predicate<string>? IsEnabled => null;

    class DiagnosticObserver(Func<string, bool> isMatch, DiagnosticSubscriberBase subscriber)
        : IObserver<DiagnosticListener> {
        readonly List<IDisposable> _subscriptions = [];

        void IObserver<DiagnosticListener>.OnNext(DiagnosticListener value) {
            if (!isMatch(value.Name)) return;

            var subscription = subscriber.IsEnabled == null
                ? value.SubscribeWithAdapter(subscriber)
                : value.SubscribeWithAdapter(subscriber, subscriber.IsEnabled);

            _subscriptions.Add(subscription);
        }

        void IObserver<DiagnosticListener>.OnError(Exception error) {
            // It is safe to ignore, errors are not happening here
        }

        void IObserver<DiagnosticListener>.OnCompleted() {
            _subscriptions.ForEach(x => x.Dispose());
            _subscriptions.Clear();
        }
    }
}

public static class DiagnosticListenerExtensions {
    public static IDisposable SubscribeWithAdapter(
        this DiagnosticListener diagnostic, object target, Predicate<string> isEnabled
    ) {
        var adapter = new DiagnosticSourceAdapter(target);
        return diagnostic.Subscribe(adapter, isEnabled);
    }
}
