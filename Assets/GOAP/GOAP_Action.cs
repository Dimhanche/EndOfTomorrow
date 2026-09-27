using System.Collections.Generic;

namespace GOAP
{
    public abstract class GOAP_Action
    {
        public string Name { get; protected set; }
        public int Cost { get; protected set; } = 1;
        public Dictionary<string, bool> Preconditions { get; protected set; }
        public Dictionary<string, bool> Effects { get; protected set; }

        public GOAP_Action()
        {
            Preconditions = new Dictionary<string, bool>();
            Effects = new Dictionary<string, bool>();
        }

        public abstract GOAP_State Perform(EntityInfo entity);
        public abstract bool CheckProceduralPrecondition(EntityInfo entity);
    }
}