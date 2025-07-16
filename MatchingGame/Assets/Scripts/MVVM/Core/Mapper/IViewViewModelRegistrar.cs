using MVVM.Core.View;
using MVVM.Core.ViewModel;

namespace MVVM.Core.Mapper
{
    public interface IViewViewModelRegistrar
    {
        void Register<TView, TViewModel>()
            where TViewModel : IBaseViewModel
            where TView : IViewInitializer;
    }
}