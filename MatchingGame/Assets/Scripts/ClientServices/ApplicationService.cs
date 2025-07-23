using System;

namespace ClientServices
{
    public class ApplicationService : IApplicationService
    {
        public event Action<bool> ApplicationFocusChanged;
        public event Action ApplicationQuited;
        
        public ApplicationService()
        {
            UnityEngine.Application.focusChanged += OnApplicationFocusChanged;
            UnityEngine.Application.quitting += OnApplicationQuited;
        }

        private void OnApplicationQuited()
        {
            ApplicationQuited?.Invoke();
        }

        private void OnApplicationFocusChanged(bool isFocus)
        {
            ApplicationFocusChanged?.Invoke(isFocus);
        }

        public void Dispose()
        {
            UnityEngine.Application.focusChanged -= OnApplicationFocusChanged;
            UnityEngine.Application.quitting -= OnApplicationQuited;
        }
    }
}