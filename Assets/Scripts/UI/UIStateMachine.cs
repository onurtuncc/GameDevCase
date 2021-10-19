using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pixelplacement
{
    public class UIStateMachine : MonoBehaviour
    {
        StateMachine stateMachine;
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

        private void Awake()
        {
            stateMachine = GetComponent<StateMachine>();
           
        }

        public void StartLevel()
        {
            stateMachine.ChangeState(0);
        }

        public void StartGameplayUI()
        {
            stateMachine.ChangeState(1);
        }

        public void WinState()
        {
            stateMachine.ChangeState(4);
        }
        public void FailState(ObjectPit objectPit)
        {

            failState.SetPitToContinue(objectPit);
            stateMachine.ChangeState(3);
            
        }
        public void RampState(RampController rampController)
        {
            rampState.SetFillController(rampController);
            stateMachine.ChangeState(2);
            
        }
        
    }

}

