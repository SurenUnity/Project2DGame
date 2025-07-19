using Cysharp.Threading.Tasks;
using MVVM.Core.View;
using MVVM.Core.ViewModel;

namespace Views.World.Core
{
    public abstract class ViewBehaviour<TViewModel> : BaseViewBehaviour<TViewModel> where TViewModel : IViewModel
    {
        public override bool IsShown { get; }

        public override UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public override UniTask Hide()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }

        public override async UniTask Destroy()
        {
            await Hide();
        }

        public override void DestroyInstantly()
        {
            Destroy(gameObject);
        }
    }
}