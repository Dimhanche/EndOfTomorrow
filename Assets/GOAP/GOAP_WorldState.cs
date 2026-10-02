using System.Collections.Generic;
using System.Linq;

namespace GOAP
{
    public class GOAP_WorldState : Dictionary<string, bool>
    {
        public GOAP_WorldState() : base()
        {
        }

        public GOAP_WorldState(Dictionary<string, bool> state) : base(state)
        {
        }

        public override string ToString()
        {
            return string.Join(", ", this.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
        }
    }
    public enum EGOAP_State
    {
        Success,
        Failure,
        Running
    }
}