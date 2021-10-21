using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pixelplacement
{
    public class UIStateMachine : StateMachine
    {
       
        [SerializeField]RampPanelLogic rampState;
        [SerializeField]FailPanelLogic failState;

        private void OnEnable()
        {
            ObjectPit.OnLevelFailed += FailState;
            Basket.OnLevelCompleted += WinState;
            Basket.OnRampEnter += RampState;

        }
        private void OnDestroy()
        {
            ObjectPit.OnLevelFailed -= FailState;
            Basket.OnLevelCompleted -= WinState;
            Basket.OnRampEnter -= RampState;
        }

        

        public void StartLevel()
        {
            
            ChangeState(0);
        }

        public void StartGameplayUI()
        {
           
            ChangeState(1);
        }

        public void WinState()
        {
           
            ChangeState(4);
        }
        public void FailState(ObjectPit objectPit)
        {

            failState.SetPitToContinue(objectPit);
            ChangeState(3);

        }
        public void RampState(RampController rampController)
        {
            rampState.SetFillController(rampController);
            ChangeState(2);

        }
        
    }

}

