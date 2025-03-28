using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override ENodeState Evaluate()
        {
            bool anyChildRunning = false;
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case ENodeState.FAILURE:
                        state = ENodeState.FAILURE;
                        return state;
                    case ENodeState.SUCCESS:
                        continue;
                    case ENodeState.RUNNING:
                        anyChildRunning = true;
                        continue;
                    default:
                        state = ENodeState.SUCCESS;
                        return state;
                }
            }
            state = anyChildRunning ? ENodeState.RUNNING : ENodeState.SUCCESS;
            return state;
        }
    }
}
