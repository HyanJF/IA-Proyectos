using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected Blackboard blackboard;
    protected StateMachine stateMachine;

    public virtual void Initialize(StateMachine sm, Blackboard bb)
    {
        stateMachine = sm;
        blackboard = bb;
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
