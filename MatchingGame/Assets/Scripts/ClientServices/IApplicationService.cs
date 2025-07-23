using System;

namespace ClientServices
{
    public interface IApplicationService : IDisposable
    {
        event Action<bool> ApplicationFocusChanged;
        event Action ApplicationQuited;
    }
}