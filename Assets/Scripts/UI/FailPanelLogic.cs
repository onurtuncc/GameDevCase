using System;
using UnityEngine;


namespace Pixelplacement
{
    public class FailPanelLogic: State
    {
        private ObjectPit pitToContinue;

        public void SetPitToContinue(ObjectPit objectPit)
        {
            pitToContinue = objectPit;
        }

        public void Continue()
        {
            if (pitToContinue != null)
            {
                pitToContinue.PassThePit();
                ChangeState(1);
            }
                
        }
    }
}
    

