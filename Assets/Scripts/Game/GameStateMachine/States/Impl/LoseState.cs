using UnityEngine;

namespace Game.GameStateMachine.States.Impl
{
    public class LoseState : AState
    {
        protected override void HandleEnter()
        {
            Debug.Log("EnterLoseState");
        }

        protected override void HandleExit()
        {
            Debug.Log("ExitLoseState");
        }
    }
}