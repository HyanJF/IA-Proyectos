using UnityEngine;

[CreateAssetMenu(fileName = "StatePatito", menuName = "FSM/States/StateForward")]
public class MoveState : State
{
    public float speed = 2f;
    public Vector3 direction = Vector3.forward;

    public override void UpdateState(StateMachine stateMachine)
    {
        stateMachine.transform.Translate(direction * speed * Time.deltaTime, Space.Self);
    }
}
