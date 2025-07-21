using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Views.UI.Components
{
    public class PopupAnimationsComponent : MonoBehaviour, IDisposable
    {
        [SerializeField] private Transform _animatedTransform;
        [SerializeField] private float _showAnimationDuration;
        [SerializeField] private float _hideAnimationDuration;
        [SerializeField] private Ease _showAnimationEase;
        [SerializeField] private Ease _hideAnimationEase;

        private CancellationTokenSource _animationTokenSource = new();
         
        public async UniTask ShowAnimation()
        {
            await _animatedTransform.DOScale(1, _showAnimationDuration).From(0).SetEase(_showAnimationEase)
                .AsyncWaitForCompletion().AsUniTask().AttachExternalCancellation(_animationTokenSource.Token).SuppressCancellationThrow();
        }

        public async UniTask HideAnimation()
        {
            await _animatedTransform.DOScale(1, _hideAnimationDuration).From(0).SetEase(_hideAnimationEase)
                .AsyncWaitForCompletion().AsUniTask().AttachExternalCancellation(_animationTokenSource.Token).SuppressCancellationThrow();
        }

        public void Dispose()
        {
            _animationTokenSource?.Dispose();
        }
    }
}