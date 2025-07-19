using System;
using Services;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class GameStartup : MonoBehaviour
    {
        private IGameStartupService _gameStartupService;

        [Inject]
        private void Inject(IGameStartupService gameStartupService)
        {
            _gameStartupService = gameStartupService;
        }

        private void Start()
        {
            _gameStartupService.Startup().Forget();
        }
    }
}