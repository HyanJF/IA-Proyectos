using UnityEngine;

public interface IActionsStrategy
{
    bool CanPerform { get; }
    bool Complete { get; }

    void Start()
    {
        //Aqui Nada
    }

    void Update(float deltaTime) 
    {
        //Aqui Nada Tampoco
    }

    void Stop()
    {
        //Aqui Menos
    }


}
