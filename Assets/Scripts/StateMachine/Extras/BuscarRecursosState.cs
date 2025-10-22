using UnityEngine;
using UnityEngine.AI;

public class BuscarRecursosState : State
{
    private NavMeshAgent agent;
    private float searchRadius;
    private float wanderRadius;
    private bool destinoAsignado = false;

    public State recogerRecursosState;

    public override void Enter()
    {
        agent = blackboard.GetValue<NavMeshAgent>("navAgent");
        searchRadius = blackboard.GetValue<float>("searchRadius");
        wanderRadius = blackboard.GetValue<float>("wanderRadius");

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent no encontrado en Blackboard.");
            return;
        }

        destinoAsignado = false;
        agent.isStopped = false; // asegurar que el agente puede moverse
        AsignarNuevoDestino();
    }

    public override void Execute()
    {
        if (agent == null) return;

        // Si todavía no tiene destino, asignar uno
        if (!destinoAsignado)
        {
            AsignarNuevoDestino();
        }

        // Si llegó al destino y no hay path pendiente
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            destinoAsignado = false; // reiniciar para poder asignar nuevo destino después
            BuscarObjetosCercanos();
        }
    }

    public override void Exit()
    {
        destinoAsignado = false;
    }

    private void AsignarNuevoDestino()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += agent.transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            destinoAsignado = true;
        }
    }

    private void BuscarObjetosCercanos()
    {
        Collider[] hits = Physics.OverlapSphere(agent.transform.position, searchRadius);
        GameObject objetoMasCercano = null;
        float minDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Object"))
            {
                float dist = Vector3.Distance(agent.transform.position, hit.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    objetoMasCercano = hit.gameObject;
                }
            }
        }

        if (objetoMasCercano != null)
        {
            blackboard.SetValue("targetObject", objetoMasCercano);
            stateMachine.ChangeState(recogerRecursosState);
        }
        else
        {
            destinoAsignado = false; // marcar que necesitamos un nuevo destino
            AsignarNuevoDestino();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (agent != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(agent.transform.position, searchRadius);
        }
    }
}
