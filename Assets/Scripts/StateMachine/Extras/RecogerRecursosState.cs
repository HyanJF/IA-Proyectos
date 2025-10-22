using UnityEngine;
using UnityEngine.AI;

public class RecogerRecursosState : State
{
    private NavMeshAgent agent;
    private GameObject targetObject;
    private float interactDistance = 2f;
    private bool recogiendo = false;

    public State buscarRecursosState;
    public State llevarRecursosState;

    public override void Enter()
    {
        agent = blackboard.GetValue<NavMeshAgent>("navAgent");
        targetObject = blackboard.GetValue<GameObject>("targetObject");

        // Ir hacia el recurso
        agent.isStopped = false;
        agent.SetDestination(targetObject.transform.position);
    }

    public override void Execute()
    {
        if (agent == null || targetObject == null)
        {
            stateMachine.ChangeState(buscarRecursosState);
            return;
        }

        float dist = Vector3.Distance(agent.transform.position, targetObject.transform.position);

        if (!recogiendo && dist <= interactDistance)
        {
            recogiendo = true;
            agent.isStopped = true;
            ResourceNode recurso = targetObject.GetComponent<ResourceNode>();
            if (recurso != null && recurso.TieneRecursos())
            {
                int cantidadRecogida = recurso.Extraer(5);
                blackboard.SetValue("carryingObject", targetObject);
                blackboard.SetValue("carryingAmount", cantidadRecogida);

                Debug.Log($"NPC recogió {cantidadRecogida} unidades de {targetObject.name}");

                stateMachine.ChangeState(llevarRecursosState);
            }
            else
            {
                Debug.Log("Recurso vacío o inválido. Volviendo a buscar...");
                stateMachine.ChangeState(buscarRecursosState);
            }
        }
    }

    public override void Exit()
    {
        recogiendo = false;
    }
}
