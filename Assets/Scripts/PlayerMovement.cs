using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private MaiActions action;
    public float speed = 2f;

    private void Start()
    {
        gameObject.TryGetComponent(out controller);
        action = new MaiActions();

        action.Gamaplay.Enable();
    }

    private void Update()
    {
        Vector2 input = action.Gamaplay.Move.ReadValue<Vector2>();

        controller.Move(new Vector3(input.x,0,input.y) * Time.deltaTime * speed);
    }
}
