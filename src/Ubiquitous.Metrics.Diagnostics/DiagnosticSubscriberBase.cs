using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.DiagnosticAdapter;

namespace Ubiquitous.Metrics.Diagnostics {
    public abstract class DiagnosticSubscriberBase {
        readonly IObserver<DiagnosticListener> _observer;

        protected DiagnosticSubscriberBase() => _observer = new DiagnosticObserver(IsMatch, this);

        protected abstract bool IsMatch(string name);

        public void Subscribe(IObservable<DiagnosticListener> allListeners) => allListeners.Subscribe(_observer);

        protected virtual Predicate<string>? IsEnabled { get; } = null;

        class DiagnosticObserver : IObserver<DiagnosticListener> {
            readonly Func<string, bool>       _isMatch;
            readonly DiagnosticSubscriberBase _subscriber;
            readonly List<IDisposable>        _subscriptions = new();

            public DiagnosticObserver(Func<string, bool> isMatch, DiagnosticSubscriberBase subscriber) {
                _isMatch    = isMatch;
                _subscriber = subscriber;
            }

            void IObserver<DiagnosticListener>.OnNext(DiagnosticListener value) {
                if (!_isMatch(value.Name)) return;

                var subscription = _subscriber.IsEnabled == null
                    ? value.SubscribeWithAdapter(_subscriber)
                    : value.SubscribeWithAdapter(_subscriber, _subscriber.IsEnabled);

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
        public static IDisposable SubscribeWithAdapter(this DiagnosticListener diagnostic, object target, Predicate<string> isEnabled) {
            var adapter = new DiagnosticSourceAdapter(target);
            return diagnostic.Subscribe(adapter, isEnabled);
        }
    }
}
