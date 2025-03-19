using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine;
using UnityEngine.AI;

public class EnemyDetection : MonoBehaviour
{
    public Transform player;
    public float fieldOfView = 60f;
    public float detectionRange = 10f;
    public LayerMask layerMask;
    private NavMeshAgent agent;
    private Vector3 lastSeenPosition;
    private bool isChasing = false;
    private float chaseTime = 5f;
    private float lostTime = 0f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isChasing)
        {
            agent.SetDestination(lastSeenPosition);

            if (Vector3.Distance(transform.position, lastSeenPosition) < 1f)
            {
                lostTime += Time.deltaTime;
                if (lostTime > chaseTime)
                {
                   isChasing = false;
                   lostTime = 0f;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 direction = other.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, direction);


        if (angle < fieldOfView / 2 && direction.magnitude < detectionRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, detectionRange, layerMask))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    lastSeenPosition = other.transform.position;
                    isChasing = true;
                    lostTime = 0f;
                }
            }
        }
    }
}
