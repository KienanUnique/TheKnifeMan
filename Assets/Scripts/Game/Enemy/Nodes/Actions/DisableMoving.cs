using System;

namespace Game.Enemy.Nodes.Actions
{
    [Serializable]
    public class DisableMoving : AAiActionNode
    {
        protected override ENodeState OnUpdate()
        {
            Enemy.DisableMoving();
            return ENodeState.Success;
        }
    }
}