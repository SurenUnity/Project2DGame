using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Views.World.Cards
{
    public class CardItemView : MonoBehaviour, IPointerClickHandler, IDisposable
    {
        [SerializeField] private SpriteRenderer _iconSpriteRenderer;
        
        private ICardItemViewModel _itemViewModel;
        private CompositeDisposable _disposable = new();

        public void Init(ICardItemViewModel cardItemViewModel)
        {
            _itemViewModel = cardItemViewModel;
            
            _itemViewModel.Icon.Subscribe(SetIcon).AddTo(_disposable);
            
            SetIcon(_itemViewModel.Icon.Value);
        }

        private void SetIcon(Sprite icon)
        {
            if (icon == null)
            {
                return;
            }
            _iconSpriteRenderer.sprite = icon;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _itemViewModel.Click();
        }
    }
}