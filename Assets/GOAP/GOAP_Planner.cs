using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GOAP
{
    public class GOAP_Planner
    {
        public Queue<GOAP_Action> Plan(EntityInfo entity,GOAP_WorldState currentState, GOAP_Goal goal, List<GOAP_Action> availableActions)
        {
            // Filtrer les actions pertinentes
            var usableActions = availableActions
                .Where(a => a.CheckProceduralPrecondition(entity))
                .ToList();

            // Créer une liste de noeuds pour la recherche
            var leaves = new List<GOAP_Node>();
            var start = new GOAP_Node(null, 0, currentState, null);

            bool success = BuildGraph(start, leaves, usableActions, goal);

            if (!success)
            {
                Debug.Log("Aucun plan trouvé !");
                return null;
            }

            // Trouver le chemin le moins coûteux
            GOAP_Node cheapest = null;
            foreach (var leaf in leaves)
            {
                if (cheapest == null || leaf.RunningCost < cheapest.RunningCost)
                    cheapest = leaf;
            }

            // Construire le plan
            var plan = new Queue<GOAP_Action>();
            GOAP_Node n = cheapest;
            while (n != null)
            {
                if (n.Action != null)
                    plan.Enqueue(n.Action);
                n = n.Parent;
            }

            // Inverser pour avoir l'ordre correct
            var finalPlan = new Queue<GOAP_Action>(plan.Reverse());
            return finalPlan;
        }

        private bool BuildGraph(GOAP_Node parent, List<GOAP_Node> leaves, List<GOAP_Action> usableActions, GOAP_Goal goal)
        {
            bool foundPath = false;

            foreach (var action in usableActions)
            {
                if (StateMatches(action.Preconditions, parent.State))
                {
                    var currentState = ApplyState(parent.State, action.Effects);
                    var node = new GOAP_Node(parent, parent.RunningCost + action.Cost, currentState, action);

                    if (StateMatches(goal.DesiredState, currentState))
                    {
                        leaves.Add(node);
                        foundPath = true;
                    }
                    else
                    {
                        var newUsableActions = ActionSubset(usableActions, action);
                        bool found = BuildGraph(node, leaves, newUsableActions, goal);
                        if (found)
                            foundPath = true;
                    }
                }
            }

            return foundPath;
        }

        private List<GOAP_Action> ActionSubset(List<GOAP_Action> actions, GOAP_Action removeAction)
        {
            var subset = new List<GOAP_Action>();
            foreach (var a in actions)
            {
                if (!a.Equals(removeAction))
                    subset.Add(a);
            }

            return subset;
        }

        private bool StateMatches(Dictionary<string, bool> test, GOAP_WorldState state)
        {
            foreach (var t in test)
            {
                if (!state.ContainsKey(t.Key) || state[t.Key] != t.Value)
                    return false;
            }

            return true;
        }

        private GOAP_WorldState ApplyState(GOAP_WorldState currentState, Dictionary<string, bool> stateChange)
        {
            var newState = new GOAP_WorldState(currentState);
            foreach (var change in stateChange)
            {
                newState[change.Key] = change.Value;
            }

            return newState;
        }
    }
}