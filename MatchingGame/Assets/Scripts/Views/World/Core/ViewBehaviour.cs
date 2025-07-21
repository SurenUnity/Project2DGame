using Cysharp.Threading.Tasks;
using MVVM.Core.View;
using MVVM.Core.ViewModel;

namespace Views.World.Core
{
    public abstract class ViewBehaviour<TViewModel> : BaseViewBehaviour<TViewModel> where TViewModel : IViewModel
    {
        private bool _isShown;
        public override bool IsShown => _isShown;

        public override UniTask Show()
        {
            gameObject.SetActive(true);
            _isShown = true;
            ViewModel.ViewShowed();
            return UniTask.CompletedTask;
        }

        public override UniTask Hide()
        {
            gameObject.SetActive(false);
            _isShown = false;
            ViewModel.ViewHided();
            return UniTask.CompletedTask;
        }

        public override async UniTask Destroy()
        {
            await Hide();
            Dispose();
            ViewModel.ViewDestroyed();
            Destroy(gameObject);
        }

        public override void DestroyInstantly()
        {
            Dispose();
            ViewModel.ViewDestroyed();
            _isShown = false;
            Destroy(gameObject);
        }
    }
}