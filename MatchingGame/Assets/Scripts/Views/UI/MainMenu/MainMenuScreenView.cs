using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Views.World.Core;

namespace Views.UI.MainMenu
{
    public class MainMenuScreenView : ViewBehaviour<IMainMenuScreenViewModel>
    {
        [SerializeField] private TMP_Text _totalScoreText;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _selecLevelButton;
        
        [Space]
        [Header("Select Level Panel")]
        [SerializeField] private GameObject _selectLevelPanel;
        [SerializeField] private Transform _selectLevelItemsContainer;
        [SerializeField] private Button _selectLevelBackButton;
        [SerializeField] private SelectLevelItemView _selectLevelItemViewPrefab;

        private List<SelectLevelItemView> _selectLevelItems = new();

        private CompositeDisposable _compositeDisposable = new();

        protected override void OnViewModelBind()
        {
            _selecLevelButton.onClick.AddListener(OpenSelectLevelPanel);
            _selectLevelBackButton.onClick.AddListener(HideSelectLevelPanel);
            _continueButton.onClick.AddListener(ContinueGame);
            _newGameButton.onClick.AddListener(NewGameClickHandler);
            ViewModel.TotalScore.Subscribe(SetTotalScore).AddTo(_compositeDisposable);
            CreateLevelItems();
        }

        private void SetTotalScore(int totalScore)
        {
            _totalScoreText.text = totalScore.ToString();
        }
        
        private void CreateLevelItems()
        {
            for (int i = 0; i < ViewModel.LevelsCount; i++)
            {
                var levelItemView = Instantiate(_selectLevelItemViewPrefab, _selectLevelItemsContainer);
                levelItemView.Init(i + 1);
                levelItemView.Click += SelectLevelClickHandler;
                _selectLevelItems.Add(levelItemView);
            }
        }

        private void ContinueGame()
        {
            //TODO save/load system
        }

        private void NewGameClickHandler()
        {
            NewGame().Forget();
        }
        
        private async UniTaskVoid NewGame()
        {
            ViewModel.NewGame();
            await Hide();
        }
        
        private void SelectLevelClickHandler(int level)
        {
            SelectLevel(level).Forget();
        }

        private void OpenSelectLevelPanel()
        {
            _selectLevelPanel.SetActive(true);
        }

        private void HideSelectLevelPanel()
        {
            _selectLevelPanel.SetActive(false);
        }

        private async UniTaskVoid SelectLevel(int level)
        {
            ViewModel.SelectLevel(level).Forget();
            await Hide();
        }

        public override void Dispose()
        {
            foreach (var selectLevelItemView in _selectLevelItems)
            {
                selectLevelItemView.Click -= SelectLevelClickHandler;
            }
            
            _selecLevelButton.onClick.RemoveListener(OpenSelectLevelPanel);
            _selectLevelBackButton.onClick.RemoveListener(HideSelectLevelPanel);
            _continueButton.onClick.RemoveListener(ContinueGame);
            _newGameButton.onClick.RemoveListener(NewGameClickHandler);
            
            _compositeDisposable.Dispose();
        }
    }
}