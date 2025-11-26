using UnityEngine;
using BJs;

[CreateAssetMenu(fileName = "Condition", menuName = "FSM/Conditions/Condition")]
public class IsOnGroundCondition : Condition
{
    public float checkDistance = 1.5f;
    public LayerMask floorMask;
    public override bool Check(StateMachine stateMachine)
    {
        Ray ray = new Ray(stateMachine.transform.position, Vector3.down);

        return Physics.Raycast(ray, checkDistance, floorMask);
        
    }
}
