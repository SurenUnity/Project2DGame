namespace MVVM.Core.ViewModel
{
    public interface IBaseViewModel : IViewModel
    {
        void SetViewManager(IViewManager viewManager);
    }
}