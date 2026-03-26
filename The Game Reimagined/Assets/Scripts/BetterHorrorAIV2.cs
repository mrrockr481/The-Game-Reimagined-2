using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BetterHorrorAIV2 : MonoBehaviour
{
    [Header("Made by Moonz")]

    [Header("Monster Settings ahh")]
    public float WanderSpeed = 5f;
    public float ChasingSpeed = 7.5f;
    public NavMeshAgent agent;

    [Header("Tag || The Tag That The Monster Will Chase")]
    public string playerTag = "Body";

    [Header("WayPoints that the opmsnter folow")]
    public List<Transform> waypoints;

    [Header("Raycast/vision Settings")]
    public float viewDistance = 20f;
    public float fovAngle = 60f;
    public int rayCount = 10;

    [Header("Chase Duration || For How Long Will The Monster Still Chase Player If Out Of Sight")]
    public float chaseDuration = 5f;

    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip roarSound;

    [Header("Raycast Colors")]
    public Color whenHitColor = Color.green;
    public Color whenNotHitColor = Color.red;

    [Header("Debugging")]
    public Transform currentTarget;
    public float chaseTimer;
    public int currentWaypointIndex = -1;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = WanderSpeed;
        Wander();
    }

    void Update()
    {
        // FIXED — Added condition
        if (agent != null)
        {
            agent.enabled = true;

            if (DetectTarget())
            {
                agent.speed = ChasingSpeed;
                chaseTimer = chaseDuration;
            }
            else if (currentTarget != null)
            {
                chaseTimer -= Time.deltaTime;

                if (chaseTimer > 0)
                {
                    Chase(currentTarget);
                }
                else
                {
                    currentTarget = null;
                    agent.speed = WanderSpeed;
                    Wander();
                }
            }
            else if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                agent.speed = WanderSpeed;
                Wander();
            }
        }
        else
        {
            agent.enabled = false;
        }
    }

    bool DetectTarget()
    {
        bool targetDetected = false;

        for (int i = 0; i < rayCount; i++)
        {
            float step = (float)i / (rayCount - 1);
            float angle = Mathf.Lerp(-fovAngle / 2, fovAngle / 2, step);
            Vector3 rayDirection = Quaternion.Euler(0, angle, 0) * transform.forward;

            if (Physics.Raycast(transform.position, rayDirection, out RaycastHit hit, viewDistance, ~0, QueryTriggerInteraction.Collide))
            {
                if (hit.collider.CompareTag(playerTag))
                {
                    if (currentTarget != hit.collider.transform)
                    {
                        audioSource.PlayOneShot(roarSound);
                    }

                    currentTarget = hit.collider.transform;
                    Chase(currentTarget);
                    targetDetected = true;
                    break;
                }
            }
        }

        return targetDetected;
    }

    void Chase(Transform target)
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    void Wander()
    {
        if (waypoints.Count == 0) return;

        int newWaypointIndex;
        do
        {
            newWaypointIndex = Random.Range(0, waypoints.Count);
        } while (newWaypointIndex == currentWaypointIndex && waypoints.Count > 1);

        currentWaypointIndex = newWaypointIndex;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Vector3 leftBoundary = Quaternion.Euler(0, -fovAngle / 2, 0) * transform.forward * viewDistance;
        Vector3 rightBoundary = Quaternion.Euler(0, fovAngle / 2, 0) * transform.forward * viewDistance;

        Gizmos.DrawRay(transform.position, leftBoundary);
        Gizmos.DrawRay(transform.position, rightBoundary);

        if (rayCount > 1)
        {
            for (int i = 0; i < rayCount; i++)
            {
                float step = (float)i / (rayCount - 1);
                float angle = Mathf.Lerp(-fovAngle / 2, fovAngle / 2, step);
                Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

                bool hitPlayer = Physics.Raycast(transform.position, direction, out RaycastHit hit, viewDistance, ~0, QueryTriggerInteraction.Collide)
                                 && hit.collider.CompareTag(playerTag);

                Gizmos.color = hitPlayer ? whenHitColor : whenNotHitColor;
                Gizmos.DrawRay(transform.position, direction * viewDistance);
            }
        }
    }
}