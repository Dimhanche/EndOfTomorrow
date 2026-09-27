using System.Collections.Generic;

namespace GOAP
{
    public class GOAP_Goal
    {
        public string Name { get; set; }
        public Dictionary<string, bool> DesiredState { get; set; }
        public int Priority { get; set; } = 1;

        public GOAP_Goal(string name, Dictionary<string, bool> desiredState, int priority = 1)
        {
            Name = name;
            DesiredState = desiredState;
            Priority = priority;
        }
    }
}