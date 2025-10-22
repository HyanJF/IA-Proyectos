using UnityEngine;

public class GuardarRecursosState : State
{
    private float guardarTiempo = 1f;
    private float timer;

    public State buscarRecursosState;

    public override void Enter()
    {
        timer = guardarTiempo;

        GameObject target = blackboard.GetValue<GameObject>("targetObject");
        if (target != null)
        { 
            target.SetActive(false);
            blackboard.Remove("targetObject");

            int recursosDisponibles = blackboard.GetValue<int>("recursosEnStore");
            recursosDisponibles++;
            blackboard.SetValue("recursosEnStore", recursosDisponibles);
        }

        Debug.Log("NPC empieza a guardar los recursos...");
    }

    public override void Execute()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Debug.Log("NPC terminó de guardar los recursos.");

            // Cambiar al estado de búsqueda
            stateMachine.ChangeState(buscarRecursosState);
        }
    }

    public override void Exit()
    {
    }
}
