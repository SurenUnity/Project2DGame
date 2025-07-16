using Cysharp.Threading.Tasks;

namespace MVVM.Core.View
{
    public interface IView
    {
        string Name { get; }
        bool IsShown { get; }
        UniTask Show();
        UniTask Hide();
        UniTask Destroy();
        void DestroyInstantly();
    }
}