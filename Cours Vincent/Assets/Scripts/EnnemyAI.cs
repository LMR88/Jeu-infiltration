using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints; // Points de patrouille
    [SerializeField] private NavMeshAgent agent;
    public float detectionRange = 10f; // Portée de détection
    public float fieldOfView = 60f; // Champ de vision
    public float chaseTime = 5f; // Temps avant d’abandonner la traque
    private int currentPatrolIndex = 0;
    private Transform player;
    private bool isChasing = false;
    private Vector3 lastKnownPosition;
    private float chaseTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GoToNextPatrolPoint();

    }

    void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
            DetectPlayer();
        }
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void DetectPlayer()
    {
        Vector3 startRaycast = transform.position + Vector3.up * 1.5f; // 🔺 Lève le Raycast à hauteur du buste
        float distance = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (distance < detectionRange && angle < fieldOfView / 2f)
        {
            RaycastHit hit;
            if (Physics.Raycast(startRaycast, directionToPlayer, out hit, detectionRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    isChasing = true;
                    lastKnownPosition = player.position;
                    chaseTimer = chaseTime;
                    Debug.Log("🚨 Joueur détecté ! L'ennemi commence à poursuivre.");
                }
            }
        }
    }

    void ChasePlayer()
    {
        agent.destination = lastKnownPosition;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < detectionRange)
        {
            lastKnownPosition = player.position;
            chaseTimer = chaseTime;
        }

        chaseTimer -= Time.deltaTime;
        if (chaseTimer <= 0)
        {
            isChasing = false;
            GoToNextPatrolPoint();
        }
    }
    
}
