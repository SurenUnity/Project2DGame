using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Views.World.Core;

namespace Views.World.Cards
{
    public class CardsView : ViewBehaviour<ICardsViewModel>
    {
        [SerializeField] private Transform _cardContainer;
        [SerializeField] private CardItemView _cardItemPrefab;

        private List<CardItemView> _cardItemViews = new();

        private CompositeDisposable _compositeDisposable = new();
        
        protected override void OnViewModelBind()
        {
            CreateCardItems(ViewModel.CardViewModels);
            
            ViewModel.CardViewModels.ObserveAdd().Subscribe(v => CreateCardItem(v.Value))
                .AddTo(_compositeDisposable);
        }

        private void CreateCardItems(IEnumerable<ICardItemViewModel> cardItemViewModels)
        {
            foreach (var cardItemViewModel in cardItemViewModels)
            {
                CreateCardItem(cardItemViewModel);
            }
        }
        
        private void CreateCardItem(ICardItemViewModel cardItemViewModel)
        {
            var cardItemView = Instantiate(_cardItemPrefab, _cardContainer);
            cardItemView.Init(cardItemViewModel);
            _cardItemViews.Add(cardItemView);
        }

        public override UniTask Destroy()
        {
            foreach (var cardItemView in _cardItemViews)
            {
                cardItemView.Dispose();
            }
            
            return base.Destroy();
        }
    }
}