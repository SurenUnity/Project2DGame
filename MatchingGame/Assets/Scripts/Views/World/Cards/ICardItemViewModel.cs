using System;
using UniRx;
using UnityEngine;

namespace Views.World.Cards
{
    public interface ICardItemViewModel : IDisposable
    {
        event Action<ICardItemViewModel> OnClick;
        IReadOnlyReactiveProperty<string> Id { get; }
        IReadOnlyReactiveProperty<Sprite> Icon { get; }
        void Click();
    }
}