using System;
using RaveDotNet.Models.Events;

namespace RaveDotNet.Events
{
    public class RaveServiceEventHandler: IRaveServiceEventHandler
    {
        public delegate void InitEventHandler(object sender, InitEventArgs e);
        public delegate void SuccessEventHandler(object sender, SuccessEventArgs e);
        public delegate void FailedEventHandler(object sender, FailedEventArgs e);
        public delegate void RequeryEventHandler(object sender, RequeryEventArgs e);
        public delegate void RequeryErrorEventHandler(object sender, RequeryErrorEventArgs e);
        public delegate void CancelledEventHandler(object sender, CancelledEventArgs e);
        public delegate void TimeoutEventHandler(object sender, TimeoutEventArgs e);
        public event InitEventHandler InitEvent;
        public event SuccessEventHandler SuccessEvent;
        public event FailedEventHandler FailedEvent;
        public event RequeryEventHandler RequeryEvent;
        public event RequeryErrorEventHandler RequeryErrorEvent;
        public event CancelledEventHandler CancelledEvent;
        public event TimeoutEventHandler TimeoutEvent;

        public void OnInit(InitEventArgs e) {
            InitEventHandler handler = InitEvent;
            if (InitEvent != null) {
                handler(this, e);
            }
        }

        public void OnSuccess(SuccessEventArgs e) {
            SuccessEventHandler handler = SuccessEvent;
            if (SuccessEvent != null) {
                handler(this, e);
            }
        }

        public void OnFailed(FailedEventArgs e) {
            FailedEventHandler handler = FailedEvent;
            if (FailedEvent != null) {
                handler(this, e);
            }
        }

        public void OnRequery(RequeryEventArgs e) {
            RequeryEventHandler handler = RequeryEvent;
            if (RequeryEvent != null) {
                handler(this, e);
            }
        }

        public void OnRequeryError(RequeryErrorEventArgs e) {
            RequeryErrorEventHandler handler = RequeryErrorEvent;
            if (RequeryErrorEvent != null) {
                handler(this, e);
            }
        }

        public void OnCancelled(CancelledEventArgs e) {
            CancelledEventHandler handler = CancelledEvent;
            if (CancelledEvent != null) {
                handler(this, e);
            }
        }

        public void OnTimeout(TimeoutEventArgs e) {
            TimeoutEventHandler handler = TimeoutEvent;
            if (TimeoutEvent != null) {
                handler(this, e);
            }
        }
    }
}
