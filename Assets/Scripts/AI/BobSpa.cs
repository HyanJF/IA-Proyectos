using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class BobSpa : MonoBehaviour
{
    private int health = 50;

    public Transform player;

    private float distanceToPlayer;
    public float fleeDistance = 4f;
    private bool lineOfSight = false;

    private Dictionary<string, float> actionScores;

    public Transform[] patrolPoints = new Transform[4];
    private int patrolIndex = 0;
    public float distanceCheck = 1;

    private NavMeshAgent agentSmith;

    private void Start()
    {
        actionScores = new Dictionary<string, float>()
        {
            {"Flee", 0f },
            {"Chase", 0f },
            {"Patrol" ,0f }
        };

        gameObject.TryGetComponent(out agentSmith);
    }

    private void Update()
    {
        //SENSE
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Ray ray = new Ray(transform.position + (Vector3.up * 0.5f), player.position - transform.position);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            lineOfSight = hit.collider.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement _);
        }
        if (Vector3.Distance(patrolPoints[patrolIndex].position, transform.position) < distanceCheck)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }

        //PLAN 
        actionScores["Flee"] = ((distanceToPlayer < fleeDistance ? 10 : 0) + (health < (health * 0.5) ? 5f : 0)) * (lineOfSight == true ? 1 : 0);
        actionScores["Chase"] = ((distanceCheck >= fleeDistance ? 8f : 0f) + (health > (health * 0.5) ? 5f : 0)) * (lineOfSight == true ? 1 : 0);
        actionScores["Patrol"] = 3f;

        string chosenAction = actionScores.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        switch (chosenAction)
        {
            //ACT
            case "Flee":
                Flee();
                break;
            case "Chase":
                Chase();
                break;
            case "Patrol":
                Patrol();
                break;
            default:
                break;
        }
    }

    private void Flee()
    {
        Vector3 fleeDir = (transform.position - player.position).normalized * 2;
        agentSmith.SetDestination(fleeDir);
    }

    private void Chase()
    {
        agentSmith.SetDestination(player.position);
    }

    private void Patrol()
    {
        agentSmith.SetDestination(patrolPoints[patrolIndex].position);
    }
    
}
