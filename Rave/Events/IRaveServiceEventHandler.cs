using RaveDotNet.Models.Events;

namespace RaveDotNet.Events
{
    public interface IRaveServiceEventHandler
    {
        void OnInit(InitEventArgs e);
        void OnSuccess(SuccessEventArgs e);
        void OnFailed(FailedEventArgs e);
        void OnRequery(RequeryEventArgs e);
        void OnRequeryError(RequeryErrorEventArgs e);
        void OnCancelled(CancelledEventArgs e);
        void OnTimeout(TimeoutEventArgs e);
    }
}
