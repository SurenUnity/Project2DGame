using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Services.Cards;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Views.World.Components;
using static Services.Cards.CardStateType;
using Logger = Utils.Logger;

namespace Views.World.Cards
{
    public class CardItemView : MonoBehaviour, IClickable, IDisposable
    {
        [SerializeField] private SpriteRenderer _iconSpriteRenderer;
        [SerializeField] private CardAnimationSettings _animationSettings;
        
        private ICardItemViewModel _itemViewModel;
        private CompositeDisposable _disposable = new();
        private Vector3 _defaultScale;
        private Sequence _selectedSequence;
        private Sequence _deselectedSequence;
        private Sequence _matchSequence;

        private bool _isInteractable;
        
        public void Init(ICardItemViewModel cardItemViewModel)
        {
            _itemViewModel = cardItemViewModel;

            _iconSpriteRenderer.gameObject.SetActive(_itemViewModel.StateType.Value is Selected);
            
            _itemViewModel.Icon.Subscribe(SetIcon).AddTo(_disposable);
            _itemViewModel.OnMatch += OnMatch;
            _itemViewModel.OnDeselected += OnDeselected;
            
            SetIcon(_itemViewModel.Icon.Value);
        }

        public void PreviewOn()
        {
            _iconSpriteRenderer.gameObject.SetActive(true);
            _isInteractable = false;
        }

        public void PreviewOff()
        {
            _iconSpriteRenderer.gameObject.SetActive(false);
            _isInteractable = true;
        }
        
        private void OnDeselected()
        {
            PlayDeselectAnimation();
        }

        private void OnMatch()
        {
            PlayMatchAnimation();
        }

        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
            _defaultScale = scale;
        }
        
        private void PlaySelectAnimation()
        {
            var targetScale = new Vector3(_defaultScale.x + _animationSettings.selectScaleValue,
                _defaultScale.y + _animationSettings.selectScaleValue, 1);

            _deselectedSequence?.Kill();
            _selectedSequence?.Kill();
            
            _selectedSequence = DOTween.Sequence();
            
            _selectedSequence.Append(transform.DOScale(targetScale, _animationSettings.selectDuration));
            _selectedSequence.Append(transform.DOScale(_defaultScale, _animationSettings.selectDuration));
            _selectedSequence.OnComplete(() =>
                _iconSpriteRenderer.gameObject.SetActive(true));

        }

        private void PlayDeselectAnimation()
        {
            _selectedSequence.Kill();
            _deselectedSequence?.Kill();
            _deselectedSequence = DOTween.Sequence();
            
            var targetScale = new Vector3(_defaultScale.x + _animationSettings.deselectScaleValue,
                _defaultScale.y + _animationSettings.deselectScaleValue, 1);
            
            _deselectedSequence.Append(transform.DOScale(targetScale, _animationSettings.selectDuration));
            _deselectedSequence.Append(transform.DOScale(_defaultScale, _animationSettings.selectDuration));
            _deselectedSequence.OnComplete(() => _iconSpriteRenderer.gameObject.SetActive(false));
        }

        private void PlayMatchAnimation()
        {
            _selectedSequence.Kill();
            _deselectedSequence?.Kill();
            _matchSequence = DOTween.Sequence();
            
            var targetScale = new Vector3(_defaultScale.x + _animationSettings.matchScaleValue,
                _defaultScale.y + _animationSettings.matchScaleValue, 1);
            
            _matchSequence.Append(transform.DOScale(targetScale, _animationSettings.matchDuration));
            _matchSequence.Append(transform.DOScale(_defaultScale, _animationSettings.matchDuration));
            _matchSequence.OnComplete(() =>
                _iconSpriteRenderer.gameObject.SetActive(true));
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
            _selectedSequence?.Kill();
            _deselectedSequence?.Kill();
            _matchSequence?.Kill();
            _itemViewModel.OnMatch -= OnMatch;
            _itemViewModel.OnDeselected -= OnDeselected;
            _disposable.Dispose();
        }

        public void Click()
        {
            if (!_isInteractable)
            {
                return;
            }
            
            if (_itemViewModel.StateType.Value is Deselected or Enable)
            {
                _itemViewModel.Click();
                PlaySelectAnimation();
            }
        }
    }
}