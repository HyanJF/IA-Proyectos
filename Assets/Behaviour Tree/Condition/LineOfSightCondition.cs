using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Line of Sight", story: "[Agent] has Line of Sight of [Object]", category: "El Papu", id: "818deba522164e1f1ffb904ed7f1c37e")]
public partial class LineOfSightCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Object;

    public override bool IsTrue()
    {
        Ray ray = new Ray(Agent.Value.transform.position, Object.Value.transform.position - Agent.Value.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == Object.Value)
            {
                return true; 
            }
        }
        return false;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
