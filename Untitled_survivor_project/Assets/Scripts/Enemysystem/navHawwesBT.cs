using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navHawwesBT : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public Animator animator; // Reference to the Animator component

    private NavMeshAgent navMeshAgent;
    private float timer;
    private bool isWalking;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        isWalking = false;

        // Set the initial animation state to idle
        animator.SetBool("IsWalking", false);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (isWalking && timer <= 0f)
        {
            
            StopWalking();
        }

        if (!isWalking)
        {
            
            PickRandomDestination();
            
            StartWalking();
            
        }

        // Update the animator parameter based on the AI's movement status

        //animator.SetBool("IsWalking", isWalking);
    }

    void PickRandomDestination()
    {
        // Get a random point within the specified wanderRadius
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        // Find the nearest valid NavMesh position to the random point
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randomDirection, out navHit, wanderRadius, -1))
        {
            navMeshAgent.SetDestination(navHit.position);
        }

        
    }

    void StartWalking()
    {
        isWalking = true;
        navMeshAgent.isStopped = false;
        animator.SetBool("IsWalking", true);
        timer = wanderTimer;

        
    }

    void StopWalking()
    {
        isWalking = false;
        
        navMeshAgent.isStopped = true;
        animator.SetBool("IsWalking", false);
        timer = wanderTimer;

        // Set the animation state to idle when the AI stops walking
        
    }
}

