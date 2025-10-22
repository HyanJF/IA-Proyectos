using UnityEngine;
using UnityEngine.AI;

public class LlevarABaseState : State
{
    private NavMeshAgent agent;
    private GameObject baseObject;

    public State revisarStoreState;

    private bool destinoAsignado = false;

    public override void Enter()
    {
        agent = blackboard.GetValue<NavMeshAgent>("navAgent");
        baseObject = blackboard.GetValue<GameObject>("baseObject");

        if (agent == null || baseObject == null) return;

        agent.isStopped = false;

        Vector3 targetPos = baseObject.transform.position;
        if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            targetPos = hit.position;

            Vector3 offset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            agent.SetDestination(targetPos + offset);

            destinoAsignado = true;
        }
        else
        {
            Debug.LogWarning("BaseObject no tiene un punto válido en el NavMesh.");
        }
    }

    public override void Execute()
    {
        if (!destinoAsignado || agent == null) return;

        // Esperar a que el path sea calculado
        if (agent.pathPending) return;

        // Todavía en camino
        if (agent.remainingDistance > agent.stoppingDistance) return;

        // Llegó al destino
        int cantidad = blackboard.GetValue<int>("carryingResource");
        Debug.Log($"Constructor dejó {cantidad} recursos en Base");

        int recursosEnBase = blackboard.GetValue<int>("recursosEnBase");
        recursosEnBase += cantidad;
        blackboard.SetValue("recursosEnBase", recursosEnBase);

        blackboard.Remove("carryingResource");
        stateMachine.ChangeState(blackboard.GetValue<State>("revisarStoreState"));

        destinoAsignado = false;
    }

    public override void Exit()
    {
        if (agent != null)
            agent.isStopped = true;
    }
}
