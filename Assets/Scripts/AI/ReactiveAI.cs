using UnityEngine;

public class ReactiveAI : MonoBehaviour
{
    public Transform player;
    public float speed = 4f;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
    }
}
