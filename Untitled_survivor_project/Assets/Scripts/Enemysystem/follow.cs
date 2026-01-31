using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class follow : MonoBehaviour
{
    public Transform character; // Reference to the character's transform
    public float followDistance = 10f; // The distance at which the object starts following the character
    public float stoppingDistance = 2.3f; // The distance at which the object stops following the character

    private NavMeshAgent navMeshAgent;
    private bool isFollowingCharacter = false;

    public bool isAnimationPlaying = true;
    private Animator animator;


    private float newVariable;
    private float b;



    private void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (character == null)
        {
            Debug.LogWarning("Character reference not set in FollowCharacterNavMesh script!");
        }
    }

    private void Update()
    {
        if (character == null)
        {
            return;
        }

        // Calculate the distance between the object and the character along the XZ plane (ignoring the height difference)
        float distanceToCharacter = Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z),
                                                     new Vector3(character.position.x, 0f, character.position.z));

        // Check if the object has not started following the character yet and if it's within the follow distance
        if (!isFollowingCharacter && distanceToCharacter <= followDistance)
        {
            isFollowingCharacter = true;

            isAnimationPlaying = isFollowingCharacter;
            //true
            animator.SetBool("yejri", isAnimationPlaying);

            VariableManager.Instance.AddToA(0.7f); //add 1 to a
            newVariable = VariableManager.Instance.GetA(); 

            stoppingDistance = newVariable + stoppingDistance;
            Debug.Log("Value of stopping distance: " + stoppingDistance);
            
        }

        if (isFollowingCharacter)
        {
            if (distanceToCharacter > stoppingDistance)
            {
                // Follow the character along the ground plane
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(character.position);

                isAnimationPlaying = isFollowingCharacter;
                //true
                animator.SetBool("yejri", isAnimationPlaying);

            }
            else
            {
                // Stop the agent from moving if it's within the stopping distance
                navMeshAgent.isStopped = true;

                isAnimationPlaying = !isFollowingCharacter;
                //false
                animator.SetBool("yejri", isAnimationPlaying);
            }
        }
    }
}

