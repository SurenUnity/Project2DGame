using System;
using ClientServices;
using Cysharp.Threading.Tasks;
using ModestTree.Util;
using MVVM.Core.ViewModel;
using Services.Cards;
using UniRx;
using UnityEngine;

namespace Views.World.Cards
{
    public class CardItemItemViewModel : ICardItemViewModel
    {
        public event Action<ICardItemViewModel> OnClick;
        public event Action OnDeselected;
        public event Action OnMatch;

        private readonly ICardItem _cardItem;
        private readonly IAssetsProvider _assetsProvider;
        
        private IReactiveProperty<Sprite> _icon = new ReactiveProperty<Sprite>();

        private CompositeDisposable _compositeDisposable = new();

        public int StaticId => _cardItem.StaticId;
        public IReadOnlyReactiveProperty<string> Id => _cardItem.Id;
        public IReadOnlyReactiveProperty<Sprite> Icon => _icon;
        public IReadOnlyReactiveProperty<CardStateType> StateType => _cardItem.StateType;

        public CardItemItemViewModel(ICardItem cardItem, 
            IAssetsProvider assetsProvider)
        {
            _cardItem = cardItem;
            _assetsProvider = assetsProvider;

            _cardItem.Id.Subscribe(v=>LoadSprite(v).Forget()).AddTo(_compositeDisposable);
        }
        
        public void Click()
        {
            
            _cardItem.Select();
            OnClick?.Invoke(this);
        }

        public void Match()
        {
            OnMatch?.Invoke();
        }

        public void Deselect()
        {
            OnDeselected?.Invoke();
        }

        private async UniTaskVoid LoadSprite(string id)
        {
            _icon.Value = await _assetsProvider.GetSprite(id);
        }
        
        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}