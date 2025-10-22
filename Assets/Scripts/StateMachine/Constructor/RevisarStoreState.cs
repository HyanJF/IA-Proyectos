using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class RevisarStoreState : State
{
    private float waitTime = 3f;

    public State irAStoreState;
    public State llevarABaseState;

    public override void Enter()
    {
        if (RevisarRecursos())
        {
            // Tomar un recurso
            int recursosDisponibles = blackboard.GetValue<int>("recursosEnStore");
            recursosDisponibles--;
            blackboard.SetValue("recursosEnStore", recursosDisponibles);

            blackboard.SetValue("carryingResource", 1);
            stateMachine.ChangeState(llevarABaseState);
        }
        else
        {
            stateMachine.StartCoroutine(EsperarYVolver());
        }
    }

    private bool RevisarRecursos()
    {
        int recursosDisponibles = blackboard.GetValue<int>("recursosEnStore");
        return recursosDisponibles > 0;
    }

    private IEnumerator EsperarYVolver()
    {
        yield return new WaitForSeconds(waitTime);
        stateMachine.ChangeState(irAStoreState);
    }

    public override void Execute() { }
    public override void Exit() { }
}
