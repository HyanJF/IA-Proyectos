using System.Collections.Generic;
using UnityEngine;

public class AgentAction
{
    public string Name;
    public float Cost;

    public HashSet<AgentBelief> Preconditions = new();
    public HashSet<AgentBelief> Effects = new();

    IActionsStrategy strategy;
    public bool Complete => strategy.Complete;

    AgentAction(string name)
    {
        Name = name;
    }

    public void Start() => strategy.Start();

    public void Update(float deltaTime)
    {
        if (strategy.CanPerform)
        {
            strategy.Update(deltaTime);
        }
        if (!strategy.Complete)
        {
            return;
        }

        foreach (var effect in Effects)
        {
            effect.Evaluate();

        }
    }

    public void Stop() => strategy.Stop();

    public class Builder
    {
        readonly AgentAction action;
        /*
        public Builder(string name)
        {
            action = new AgentAction(name)
            {
                action.Cost = 1f
            }
        }

        public Builder WithCost(float cost)
        {
            action.Cost = cost;
        }

        public Builder WithStrategy(IActionsStrategy strategy)
        {

        }*/
    }
}
