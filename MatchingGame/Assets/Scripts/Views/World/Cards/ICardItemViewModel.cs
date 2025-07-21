using System;
using Services.Cards;
using UniRx;
using UnityEngine;

namespace Views.World.Cards
{
    public interface ICardItemViewModel : IDisposable
    {
        event Action<ICardItemViewModel> OnClick;
        event Action OnDeselected;
        event Action OnMatch;
        int StaticId { get; }
        IReadOnlyReactiveProperty<string> Id { get; }
        IReadOnlyReactiveProperty<Sprite> Icon { get; }
        IReadOnlyReactiveProperty<CardStateType> StateType { get; }
        void Click();
        void Match();
        void Deselect();
    }
}