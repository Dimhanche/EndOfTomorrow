namespace GOAP
{
    public class GOAP_Node
    {
        public GOAP_Node Parent { get; }
        public int RunningCost { get; }
        public GOAP_WorldState State { get; }
        public GOAP_Action Action { get; }

        public GOAP_Node(GOAP_Node parent, int runningCost, GOAP_WorldState state, GOAP_Action action)
        {
            Parent = parent;
            RunningCost = runningCost;
            State = state;
            Action = action;
        }
    }
}

