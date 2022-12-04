using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SubGoal
{
    public Dictionary<string, int> sgoals;
    public bool remove;

    public SubGoal(string s, int i, bool r)
    {
        sgoals = new Dictionary<string,int>();
        sgoals.Add(s, i);
        remove= r;
    } 
}

public class GAgent : MonoBehaviour
{
    public List<GAction> action = new List<GAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();

    GPlanner planner;
    Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;

    void Start()
    {
        GAction[] acts = this.GetComponent<GAction>();
        foreach (GAction a in acts)
        {
            actions.Add(a);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }

}
