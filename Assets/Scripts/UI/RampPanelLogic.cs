using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Pixelplacement
{
    public class RampPanelLogic: State
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private TMP_Text percentageText;
        public Text goldText;
        private RampController rc;


        private void OnDisable()
        {
            goldText.text = "";
        }

        public void SetFillController(RampController rampController)
        {
            rc = rampController;
            rampController.popUpGoldText = goldText;
        }
        private void Update()
        {
            if (rc != null)
            {
                fillImage.fillAmount = rc.FillAmount;
                percentageText.text = "%" + (rc.FillAmount * 100).ToString();
            }
                

        }
    }
}
