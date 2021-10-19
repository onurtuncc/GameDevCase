using System;
using UnityEngine;
using TMPro;

namespace Pixelplacement
{
    public class StartPanelLogic : State
    {
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text nextLevelText;
        [SerializeField] private TMP_Text gameplaylevelText;
        [SerializeField] private TMP_Text gameplaynextLevelText;

        private int currentLevel;
        private void OnEnable()
        {
            SetLevelText();
        }
        private void OnDisable()
        {
            
        }
        public void OnTapButtonClick()
        {
            ChangeState("GamePlayPanel");
            
        }
        public void SetLevelText()
        {
            currentLevel = PlayerPrefController.Instance.GetCurrentLevelData();
            levelText.text = currentLevel.ToString();
            nextLevelText.text = (currentLevel + 1).ToString();
            gameplaylevelText.text = currentLevel.ToString();
            gameplaynextLevelText.text = (currentLevel + 1).ToString();
        }
    }
}
