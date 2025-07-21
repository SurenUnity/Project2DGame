using Cysharp.Threading.Tasks;
using MVVM.Core.View;
using MVVM.Core.ViewModel;

namespace Views.UI.Core
{
    public abstract class ScreenViewBehaviour<TViewModel> : BaseViewBehaviour<TViewModel> where TViewModel : IViewModel
    {
        private bool _isShown;
        public override bool IsShown => _isShown;

        public override UniTask Show()
        {
            gameObject.SetActive(true);
            _isShown = true;
            return UniTask.CompletedTask;
        }

        public override UniTask Hide()
        {
            gameObject.SetActive(false);
            _isShown = false;
            return UniTask.CompletedTask;
        }

        public override async UniTask Destroy()
        {
            await Hide();
            Dispose();
            Destroy(gameObject);
        }

        public override void DestroyInstantly()
        {
            Dispose();
            Destroy(gameObject);
        }
    }
}