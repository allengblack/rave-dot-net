using System;
using Rave.Models.Events;

namespace Rave.Events
{
    public class RaveServiceEventHandler: IRaveServiceEventHandler
    {
        public event EventHandler InitEventHandler;
        public event EventHandler SuccessEventHandler;
        public event EventHandler FailedEventHandler;
        public event EventHandler RequeryEventHandler;
        public event EventHandler RequeryErrorEventHandler;
        public event EventHandler CancelledEventHandler;
        public event EventHandler TimeoutEventHandler;

        public void OnInit(InitEventArgs e) {
            EventHandler handler = InitEventHandler;
            if (InitEventHandler != null) {
                handler(this, e);
            }
        }

        public void OnSuccess(SuccessEventArgs e) {
            EventHandler handler = SuccessEventHandler;
            if (SuccessEventHandler != null) {
                handler(this, e);
            }
        }

        public void OnFailed(FailedEventArgs e) {
            EventHandler handler = FailedEventHandler;
            if (FailedEventHandler != null) {
                handler(this, e);
            }
        }

        public void OnRequery(RequeryEventArgs e) {
            EventHandler handler = RequeryEventHandler;
            if (RequeryEventHandler != null) {
                handler(this, e);
            }
        }

        public void OnRequeryError(RequeryErrorEventArgs e) {
            EventHandler handler = RequeryErrorEventHandler;
            if (RequeryErrorEventHandler != null) {
                handler(this, e);
            }
        }

        public void OnCancelled(CancelledEventArgs e) {
            EventHandler handler = CancelledEventHandler;
            if (CancelledEventHandler != null) {
                handler(this, e);
            }
        }

        public void OnTimeout(TimeoutEventArgs e) {
            EventHandler handler = TimeoutEventHandler;
            if (TimeoutEventHandler != null) {
                handler(this, e);
            }
        }
    }
}
