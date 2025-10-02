using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private MaiActions action;
    public float speed = 2f;

    [Header("Throw Settings")]
    public GameObject noisePrefab;    // Prefab del objeto que hace ruido
    public Transform throwPoint;      // Punto de salida (por ej. mano del player)
    public float throwForce = 10f;    // Fuerza del lanzamiento

    private void Start()
    {
        gameObject.TryGetComponent(out controller);
        action = new MaiActions();

        action.Gamaplay.Enable();
    }

    private void Update()
    {
        // Movimiento
        Vector2 input = action.Gamaplay.Move.ReadValue<Vector2>();
        controller.Move(new Vector3(input.x, 0, input.y) * Time.deltaTime * speed);

        // Lanzar objeto (ejemplo con tecla G)
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            ThrowNoise();
        }
    }

    private void ThrowNoise()
    {
        if (noisePrefab != null && throwPoint != null)
        {
            GameObject obj = Instantiate(noisePrefab, throwPoint.position, Quaternion.identity);
            Rigidbody rb = obj.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
            }
        }
    }
}
