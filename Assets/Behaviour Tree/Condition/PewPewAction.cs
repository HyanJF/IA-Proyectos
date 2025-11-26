using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PewPew", story: "[Agent] Shoots at [Target] a [Bullet]", category: "Action", id: "158a078a14356fca8a06a796f7e28b1d")]
public partial class PewPewAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<GameObject> Bullet;

    public BlackboardVariable<float> bulletSpeed = new BlackboardVariable<float>(10f);


    protected override Status OnStart()
    {
        GameObject bulletInstance = GameObject.Instantiate(Bullet.Value, Agent.Value.transform.position, Quaternion.identity);
        if (!bulletInstance.TryGetComponent(out Rigidbody rb))
        {
            rb = bulletInstance.AddComponent<Rigidbody>();
        }
        rb.AddForce((Target.Value.transform.position - Agent.Value.transform.position).normalized * bulletSpeed, ForceMode.Impulse);
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

