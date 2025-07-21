using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.UI.MainMenu
{
    public class SelectLevelItemView : MonoBehaviour
    {
        public event Action<int> Click; 
        
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Button _selectLevelButton;
        private int _level;

        public void Awake()
        {
            _selectLevelButton.onClick.AddListener(SelectLevelClickHandler);
        }

        public void Init(int level)
        {
            _level = level;
            _levelText.text = level.ToString();
        }

        private void SelectLevelClickHandler()
        {
            Click?.Invoke(_level);
        }
    }
}