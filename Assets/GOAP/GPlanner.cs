using System.Collections.Generic;
using UnityEngine;

public class NodeNOVO {

    public NodeNOVO parent;
    public float cost;
    public Dictionary<string, int> state;
    public GAction action;

    // Constructor
    public NodeNOVO(NodeNOVO parent, float cost, Dictionary<string, int> allStates, GAction action) {

        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allStates);
        this.action = action;
    }

    // Constructor
    public NodeNOVO(NodeNOVO parent, float cost, Dictionary<string, int> allStates, Dictionary<string, int> beliefStates,GAction action) {

        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allStates);

        foreach(KeyValuePair<string,int> b in beliefStates)
        {
            if(!this.state.ContainsKey(b.Key))
            {
                this.state.Add(b.Key, b.Value);
            }
        }
        this.action = action;
        
    }
}

public class GPlanner {

    public Queue<GAction> plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates beliefStates) {

        List<GAction> usableActions = new List<GAction>();

        foreach (GAction a in actions) {

            if (a.IsAchievable()) {

                usableActions.Add(a);
            }
        }

        List<NodeNOVO> leaves = new List<NodeNOVO>();
        NodeNOVO start = new NodeNOVO(null, 0.0f, GWorld.Instance.GetWorld().GetStates(), beliefStates.GetStates(), null);

        bool success = BuildGraph(start, leaves, usableActions, goal);

        if (!success) {

            Debug.Log("NO PLAN");
            return null;
        }

        NodeNOVO cheapest = null;
        foreach (NodeNOVO leaf in leaves) {

            if (cheapest == null) {

                cheapest = leaf;
            } else if (leaf.cost < cheapest.cost) {

                cheapest = leaf;
            }
        }
        List<GAction> result = new List<GAction>();
        NodeNOVO n = cheapest;

        while (n != null) {

            if (n.action != null) {

                result.Insert(0, n.action);
            }

            n = n.parent;
        }

        Queue<GAction> queue = new Queue<GAction>();

        foreach (GAction a in result) {

            queue.Enqueue(a);
        }

        Debug.Log("The Plan is: ");
        foreach (GAction a in queue) {

            Debug.Log("Q: " + a.actionName);
        }

        return queue;
    }

    private bool BuildGraph(NodeNOVO parent, List<NodeNOVO> leaves, List<GAction> usableActions, Dictionary<string, int> goal) {

        bool foundPath = false;
        foreach (GAction action in usableActions) {

            if (action.IsAhievableGiven(parent.state)) {

                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);

                foreach (KeyValuePair<string, int> eff in action.effects) {

                    if (!currentState.ContainsKey(eff.Key)) {

                        currentState.Add(eff.Key, eff.Value);
                    }
                }

                NodeNOVO nodeNOVO = new NodeNOVO(parent, parent.cost + action.cost, currentState, action);

                if (GoalAchieved(goal, currentState)) {

                    leaves.Add(nodeNOVO);
                    foundPath = true;
                } else {

                    List<GAction> subset = ActionSubset(usableActions, action);
                    bool found = BuildGraph(nodeNOVO, leaves, subset, goal);

                    if (found) {

                        foundPath = true;
                    }
                }
            }
        }
        return foundPath;
    }

    private List<GAction> ActionSubset(List<GAction> actions, GAction removeMe) {

        List<GAction> subset = new List<GAction>();

        foreach (GAction a in actions) {

            if (!a.Equals(removeMe)) {

                subset.Add(a);
            }
        }
        return subset;
    }

    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state) {

        foreach (KeyValuePair<string, int> g in goal) {

            if (!state.ContainsKey(g.Key)) {

                return false;
            }
        }
        return true;
    }
}
