using UnityEngine;
using UnityEngine.AI;

public class LlevarRecursosState : State
{
    private NavMeshAgent agent;
    private GameObject carryingObject;
    private int carryingAmount;
    private GameObject storeObject;
    private float interactDistance = 2f;
    private bool enDestino = false;

    public State guardarRecursosState;

    public override void Enter()
    {
        agent = blackboard.GetValue<NavMeshAgent>("navAgent");
        carryingObject = blackboard.GetValue<GameObject>("carryingObject");
        carryingAmount = blackboard.GetValue<int>("carryingAmount");
        storeObject = blackboard.GetValue<GameObject>("storeObject");

        // Mover hacia el almacén
        agent.isStopped = false;
        agent.SetDestination(storeObject.transform.position);
        enDestino = false;
    }

    public override void Execute()
    {
        if (agent == null || storeObject == null || carryingObject == null) return;

        float dist = Vector3.Distance(agent.transform.position, storeObject.transform.position);

        if (!enDestino && dist <= interactDistance)
        {
            enDestino = true;
            agent.isStopped = true;

            blackboard.Remove("carryingObject");
            blackboard.Remove("carryingAmount");
            blackboard.Remove("targetObject");

            stateMachine.ChangeState(guardarRecursosState);
        }
    }

    public override void Exit()
    {
        enDestino = false;
    }
}
