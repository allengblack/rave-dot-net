using System;
using Rave.Models.Events;

namespace Rave.Events
{
    public class RaveServiceEventHandler: IRaveServiceEventHandler
    {
        public event EventHandler InitEvent;
        public event EventHandler SuccessEvent;
        public event EventHandler FailedEvent;
        public event EventHandler RequeryEvent;
        public event EventHandler RequeryErrorEvent;
        public event EventHandler CancelledEvent;
        public event EventHandler TimeoutEvent;

        public void OnInit(InitEventArgs e) {
            EventHandler handler = InitEvent;
            if (InitEvent != null) {
                handler(this, e);
            }
        }

        public void OnSuccess(SuccessEventArgs e) {
            EventHandler handler = SuccessEvent;
            if (SuccessEvent != null) {
                handler(this, e);
            }
        }

        public void OnFailed(FailedEventArgs e) {
            EventHandler handler = FailedEvent;
            if (FailedEvent != null) {
                handler(this, e);
            }
        }

        public void OnRequery(RequeryEventArgs e) {
            EventHandler handler = RequeryEvent;
            if (RequeryEvent != null) {
                handler(this, e);
            }
        }

        public void OnRequeryError(RequeryErrorEventArgs e) {
            EventHandler handler = RequeryErrorEvent;
            if (RequeryErrorEvent != null) {
                handler(this, e);
            }
        }

        public void OnCancelled(CancelledEventArgs e) {
            EventHandler handler = CancelledEvent;
            if (CancelledEvent != null) {
                handler(this, e);
            }
        }

        public void OnTimeout(TimeoutEventArgs e) {
            EventHandler handler = TimeoutEvent;
            if (TimeoutEvent != null) {
                handler(this, e);
            }
        }
    }
}
