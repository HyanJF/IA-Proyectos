using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using TMPro;

public class Loquito : MonoBehaviour
{
    private float health = 100;
    public Transform player;

    private float distanceToPlayer;
    private bool lineOfSight = false;

    private Dictionary<string, float> actionScores;

    public List<Transform> patrolPoints;
    private int patrolIndex = 0;
    public float distanceCheck = 1;

    private NavMeshAgent agentSmith;

    public TextMeshProUGUI investigateText;
    public TextMeshProUGUI chaseText;

    public float viewDistance = 10f;
    public float viewAngle = 45f;

    // --- INVESTIVAR ---
    private Vector3 noisePosition;
    private bool heardNoise = false;
    public float investigateRadius = 2f;
    public int maxPatrolPoints = 4;

    private void Start()
    {
        actionScores = new Dictionary<string, float>()
        {
            {"Chase", 0f },
            {"Patrol", 0f },
            {"Investigate", 0f }
        };

        health = 100;

        gameObject.TryGetComponent(out agentSmith);
    }

    private void Update()
    {
        //SENSE
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        lineOfSight = PlayerInFOV();

        if (Vector3.Distance(patrolPoints[patrolIndex].position, transform.position) < distanceCheck)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Count;
        }

        //PLAN (scores)
        actionScores["Chase"] = lineOfSight ? 10f : 0f;
        actionScores["Investigate"] = heardNoise ? 5f : 0f;
        actionScores["Patrol"] = 3f;

        investigateText.text = "INVESTIGATE = " + actionScores["Investigate"];
        chaseText.text = "CHASE = " + actionScores["Chase"];

        // choose action
        string chosenAction = actionScores.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        switch (chosenAction)
        {
            case "Chase":
                Chase();
                break;
            case "Investigate":
                Investigate();
                break;
            case "Patrol":
                Patrol();
                break;
        }
    }

    private bool PlayerInFOV()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;

        if (distanceToPlayer > viewDistance) return false;

        float angletoPlayer = Vector3.Angle(transform.forward, dirToPlayer);
        if (angletoPlayer > viewAngle / 2) return false;

        if (Physics.Raycast(transform.position, dirToPlayer, out RaycastHit hit, distanceToPlayer))
        {
            return hit.collider.gameObject.CompareTag("Player");
        }
        return false;
    }

    private void Chase()
    {
        agentSmith.SetDestination(player.position);
    }

    private void Patrol()
    {
        agentSmith.SetDestination(patrolPoints[patrolIndex].position);
    }

    private void Investigate()
    {
        agentSmith.SetDestination(noisePosition);

        // si llega al punto del ruido
        if (Vector3.Distance(transform.position, noisePosition) < investigateRadius)
        {
            if (lineOfSight)
            {
                // si encontró al jugador
                heardNoise = false;
            }
            else
            {
                // agregar el punto
                patrolPoints.Add(CreateTransform(noisePosition));

                // Elimina un punto si se excede el limite.
                if (patrolPoints.Count > maxPatrolPoints)
                {
                    Transform farthest = patrolPoints[0];
                    float maxDist = Vector3.Distance(transform.position, farthest.position);

                    foreach (var p in patrolPoints)
                    {
                        float d = Vector3.Distance(transform.position, p.position);
                        if (d > maxDist)
                        {
                            maxDist = d;
                            farthest = p;
                        }
                    }
                    patrolPoints.Remove(farthest);
                }
                heardNoise = false;
            }
        }
    }

    // Método público para enviarle un ruido
    public void HearNoise(Vector3 noisePos)
    {
        noisePosition = noisePos;
        heardNoise = true;
    }

    // Helper para convertir Vector3 en Transform "falso" para patrulla
    private Transform CreateTransform(Vector3 pos)
    {
        GameObject go = new GameObject("NoisePoint");
        go.transform.position = pos;
        return go.transform;
    }
}
