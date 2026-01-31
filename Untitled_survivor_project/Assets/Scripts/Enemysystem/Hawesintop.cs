using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hawesintop : MonoBehaviour
{
    public float walkRadius = 10f; // The maximum distance the bot can walk from its starting position
    public float minWalkDistance = 3f; // The minimum distance the bot must walk before changing direction
    private NavMeshAgent agent;
    private Vector3 randomDestination;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
    }

    void Update()
    {
        // If the bot has reached its destination or is stuck, set a new random destination
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            SetRandomDestination();
        }
    }

    void SetRandomDestination()
    {
        // Calculate a random direction within the walk radius
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;

        // Check if the random direction is within the NavMesh bounds
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, walkRadius, NavMesh.AllAreas);

        // Set the new random destination
        randomDestination = navHit.position;
        agent.SetDestination(randomDestination);
    }
}
