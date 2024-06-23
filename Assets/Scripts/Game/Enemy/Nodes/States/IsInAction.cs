using System;

namespace Game.Enemy.Nodes.States
{
    [Serializable]
    public class IsInAction : AAiActionNode
    {
        protected override ENodeState OnUpdate()
        {
            return Enemy.IsInAction ? ENodeState.Success : ENodeState.Failure;
        }
    }
}