using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Extensions;
using Services.Cards;
using UniRx;
using UnityEngine;
using Views.World.Core;

namespace Views.World.Cards
{
    public class CardsView : ViewBehaviour<ICardsViewModel>
    {
        [SerializeField] private Transform _cardContainer;
        [SerializeField] private CardItemView _cardItemPrefab;
        [SerializeField] private CardGridComponent _gridComponent;
        [SerializeField] private float _previewDelay;

        private List<CardItemView> _cardItemViews = new();

        private CancellationTokenSource _previewCancellationTokenSource;
        private CompositeDisposable _compositeDisposable = new();
        
        protected override void OnViewModelBind()
        {
            CreateCardItems(ViewModel.CardViewModels);
            
            ViewModel.GameStared += OnGameStarted;
            ViewModel.CardViewModels.ObserveAdd().Subscribe(v => CreateCardItem(v.Value))
                .AddTo(_compositeDisposable);
        }

        private void OnGameStarted()
        {
            PrepareCards().Forget();
        }

        private async UniTaskVoid PrepareCards()
        {
            foreach (var cardItemView in _cardItemViews)
            {
                cardItemView.Hide();
            }

            var enableCards = new List<CardItemView>();
            
            foreach (var cardItemViewModel in ViewModel.CardsForLevel)
            {
                enableCards.Add(_cardItemViews.FirstOrDefault(v => v.StaticId == cardItemViewModel.StaticId));
            }
            
            enableCards.Shuffle();
            _gridComponent.Grid(ViewModel.Columns, ViewModel.Rows, enableCards.ToArray());
            foreach (var cardItemView in enableCards)
            {
                cardItemView.Show();
                cardItemView.ResetAnimation();
                cardItemView.PreviewOn();
            }
            
            _previewCancellationTokenSource = new CancellationTokenSource();
            
            await UniTask.Delay(TimeSpan.FromSeconds(_previewDelay), cancellationToken: _previewCancellationTokenSource.Token).
                SuppressCancellationThrow();

            if (_previewCancellationTokenSource.IsCancellationRequested)
            {
                return;
            }

            foreach (var cardItemView in enableCards)
            {
                cardItemView.PreviewOff();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                _gridComponent.Grid(ViewModel.Columns, ViewModel.Rows, _cardItemViews.ToArray());
            }
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

        public override void Dispose()
        {
            _previewCancellationTokenSource?.Cancel();
            
            foreach (var cardItemView in _cardItemViews)
            {
                cardItemView.Dispose();
            }
            
            ViewModel.GameStared -= OnGameStarted;
        }
    }
}