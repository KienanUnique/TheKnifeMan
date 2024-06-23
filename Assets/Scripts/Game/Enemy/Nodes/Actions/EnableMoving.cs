using System;

namespace Game.Enemy.Nodes.Actions
{
    [Serializable]
    public class EnableMoving : AAiActionNode
    {
        protected override ENodeState OnUpdate()
        {
            Enemy.EnableMoving();
            return ENodeState.Success;
        }
    }
}