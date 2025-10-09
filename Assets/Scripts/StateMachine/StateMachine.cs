using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State initialState;
    public State currentState;
    public FSMContext context = new FSMContext();

    private void Start()
    {
        Changestate(initialState);
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
            currentState.CheckTransitions(this);
        }
    }
    public void Changestate(State state)
    {
        if (currentState == state || state == null)
        {
            return;
        }
        if (currentState == null)
        {
            currentState.ExitState(this);
        }

        currentState = state;
        currentState.EnterState(this);
    }
}

[SerializeField]
public class FSMContext
{
    public GameObject playab;
    public LayerMask layerInecesaria;
}
