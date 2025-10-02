using UnityEngine;

public class NoiseObject : MonoBehaviour
{
    public float noiseRadius = 50f; 
    public float lifeTime = 5f;    

    private void Start()
    {
        Destroy(gameObject, lifeTime);

        Collider[] colliders = Physics.OverlapSphere(transform.position, noiseRadius);
        foreach (Collider col in colliders)
        {
            Loquito enemy = col.GetComponent<Loquito>();
            if (enemy != null)
            {
                Debug.Log("Enemigo atraido");
                enemy.HearNoise(transform.position);
            }
            else
            {
                Debug.Log("Enemigo no encontrado");
            }
        }
    }
}
