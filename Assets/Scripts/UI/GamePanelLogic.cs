using System;
using UnityEngine;
using TMPro;

namespace Pixelplacement
{
    public class GamePanelLogic : State
    {
        [SerializeField] private TMP_Text totalGoldText;
        private void OnEnable()
        {
            totalGoldText.text = PlayerPrefController.Instance.GetTotalGold().ToString();

        }

    }
}
