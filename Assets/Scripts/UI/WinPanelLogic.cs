using System;
using UnityEngine;
using TMPro;

namespace Pixelplacement
{
    public class WinPanelLogic : State
    {
        [SerializeField] private TMP_Text goldText;
        [SerializeField] private TMP_Text totalGoldText;

        private void OnEnable()
        {
            goldText.text = ScoreManager.ScoreManagerInstance.Score.ToString();
            totalGoldText.text = "Total Gold " + PlayerPrefController.Instance.GetTotalGold().ToString();
        }
    }
}
