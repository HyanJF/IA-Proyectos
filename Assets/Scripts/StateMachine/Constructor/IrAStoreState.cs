using UnityEngine;
using UnityEngine.AI;

public class IrAStoreState : State
{
    private NavMeshAgent agent;
    private GameObject storeObject;

    public State revisarStoreState;

    private bool destinoAsignado = false;

    public override void Enter()
    {
        agent = blackboard.GetValue<NavMeshAgent>("navAgent");
        storeObject = blackboard.GetValue<GameObject>("storeObject");

        if (agent == null || storeObject == null) return;

        agent.isStopped = false;

        Vector3 targetPos = storeObject.transform.position;
        if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            targetPos = hit.position;
            Vector3 offset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            agent.SetDestination(targetPos + offset);
            destinoAsignado = true;
        }
        else
        {
            Debug.LogWarning("StoreObject no tiene un punto válido en el NavMesh.");
        }
    }

    public override void Execute()
    {
        if (!destinoAsignado || agent == null) return;

        if (agent.pathPending) return;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            stateMachine.ChangeState(revisarStoreState);
            destinoAsignado = false;
        }
    }

    public override void Exit()
    {
        if (agent != null)
            agent.isStopped = true;
    }
}
