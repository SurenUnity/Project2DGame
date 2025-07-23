using System;
using System.Collections.Generic;
using MVVM.Core.ViewModel;
using UniRx;

namespace Views.World.Cards
{
    public interface ICardsViewModel : IViewModel
    {
        event Action GameStared;
        int Columns { get; }
        int Rows { get; }
        IReadOnlyReactiveCollection<ICardItemViewModel> CardViewModels { get; }
        IReadOnlyList<ICardItemViewModel> CardsForLevel { get; }
    }
}