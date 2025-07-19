using MVVM.Core.ViewModel;
using UniRx;

namespace Views.World.Cards
{
    public interface ICardsViewModel : IViewModel
    {
        IReadOnlyReactiveCollection<ICardItemViewModel> CardViewModels { get; }
    }
}