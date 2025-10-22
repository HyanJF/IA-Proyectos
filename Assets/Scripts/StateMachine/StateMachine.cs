using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState;
    public Blackboard blackboard;

    void Start()
    {
        blackboard.SetValue("recursosEnStore", 1);

        var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
            blackboard.SetValue("navAgent", agent);

        GameObject store = GameObject.FindGameObjectWithTag("Store");
        if (store != null)
            blackboard.SetValue("storeObject", store);

        GameObject baseObj = GameObject.FindGameObjectWithTag("Base");
        if (baseObj != null)
            blackboard.SetValue("baseObject", baseObj);

        if (!blackboard.HasValue("searchRadius"))
            blackboard.SetValue("searchRadius", 10f);
        if (!blackboard.HasValue("wanderRadius"))
            blackboard.SetValue("wanderRadius", 20f);

        if (currentState != null)
        {
            currentState.Initialize(this, blackboard);
            currentState.Enter();
        }
    }

    void Update()
    {
        currentState?.Execute();
    }

    public void ChangeState(State newState)
    {
        if (newState == null) return;

        currentState?.Exit();
        currentState = newState;
        currentState.Initialize(this, blackboard);
        currentState.Enter();
    }
}
