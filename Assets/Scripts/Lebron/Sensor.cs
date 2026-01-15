using System;
using UnityEngine;

[RequireComponent (typeof(SphereCollider))]

public class Sensor : MonoBehaviour
{
    public float detectionRadius = 5f;
    public float timeInterval = 1f;

    SphereCollider detectionRange;

    public event Action OnTargetChanged;

    private GameObject target;

    public Vector3 TargetPosition => target != null ? target.transform.position : Vector3.zero;
    public bool IsTargetInRange => TargetPosition != Vector3.zero;

    private Vector3 lastKnownPosition;
    private float t = 0;

    private void Awake()
    {
        detectionRange = GetComponent<SphereCollider>();
        detectionRange.isTrigger = true;
        detectionRange.radius = detectionRadius;
    }

    private void Start()
    {
        t = timeInterval;
        if (t <= 0)
        {
            UpdateTargetPosition(target);
            t = timeInterval;
        }
    }

    private void UpdateTargetPosition(GameObject target = null)
    {
        this.target = target;
        if (IsTargetInRange && (lastKnownPosition != TargetPosition || lastKnownPosition != Vector3.zero))
        {
            lastKnownPosition = TargetPosition;
            if (OnTargetChanged != null)
            {
                OnTargetChanged.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
        { 
            return; 
        }
        UpdateTargetPosition(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        UpdateTargetPosition();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsTargetInRange ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
