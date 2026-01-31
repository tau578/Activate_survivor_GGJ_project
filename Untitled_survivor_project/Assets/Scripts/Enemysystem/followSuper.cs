using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class followSuper : MonoBehaviour
{

    private bool following = true;
    private bool close = false;

    public Transform character; // Reference to the character's transform
    public float followDistance = 10f; // The distance at which the object starts following the character
    public float stoppingDistance = 2.3f; // The distance at which the object stops following the character

    private NavMeshAgent navMeshAgent;
    private bool isFollowingCharacter = false;

    public bool isAnimationPlaying = true;
    private Animator animator;


    private float newVariable;
    private float b;



    //enemy
    public Transform enemy;
    public float followDistanceenemy = 10f;
    public float stoppingDistanceenemy = 0.69f;
    private bool isFollowingEnemy = false;

    public string batataTag = "batata";

    



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (character == null)
        {
            Debug.LogWarning("Character reference not set in FollowCharacterNavMesh script!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (character == null)
        {
            return;
        }

        //attack wale follow bel bools
        if (Input.GetKeyDown(KeyCode.F))
        {
            following = true;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            following = false;
        }




        if (following)
        {

            // Calculate the distance between the object and the character along the XZ plane (ignoring the height difference)
            float distanceToCharacter = Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z),
                                                         new Vector3(character.position.x, 0f, character.position.z));

            // Check if the object has not started following the character yet and if it's within the follow distance
            if (!isFollowingCharacter && distanceToCharacter <= followDistance)
            {
                isFollowingCharacter = true;

                isAnimationPlaying = isFollowingCharacter;
                //true
                animator.SetBool("attacki", false);
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
                    animator.SetBool("attacki", false);
                    animator.SetBool("yejri", isAnimationPlaying);

                }
                else
                {
                    // Stop the agent from moving if it's within the stopping distance
                    navMeshAgent.isStopped = true;

                    isAnimationPlaying = !isFollowingCharacter;
                    //false
                    animator.SetBool("attacki", false);
                    animator.SetBool("yejri", isAnimationPlaying);
                }
            }
        }

        if (!following)
        {
            if (!close)
            {
                FindClosestBatata();
            

                // Calculate the distance between the object and the character along the XZ plane (ignoring the height difference)
                float distanceToEnemy = Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z),
                                                         new Vector3(enemy.position.x, 0f, enemy.position.z));

                // Check if the object has not started following the character yet and if it's within the follow distance
                if (!isFollowingEnemy) //&& distanceToEnemy <= followDistanceenemy)
                {
                    isFollowingEnemy = true;

                    isAnimationPlaying = isFollowingEnemy;
                    //true
                    animator.SetBool("yejri", isAnimationPlaying);

                
            
                }

                if (isFollowingEnemy)
                {
                    if (distanceToEnemy > stoppingDistanceenemy)
                    {
                        // Follow the character along the ground plane
                        navMeshAgent.isStopped = false;
                        navMeshAgent.SetDestination(enemy.position);

                        isAnimationPlaying = isFollowingCharacter;
                        //true
                        animator.SetBool("yejri", true);
                        animator.SetBool("attacki", false);

                    }
                    else
                    {
                        // Stop the agent from moving if it's within the stopping distance
                        navMeshAgent.isStopped = true;

                        isAnimationPlaying = !isFollowingEnemy;
                        //false
                        animator.SetBool("attacki", true);
                        //8azran
                        Vector3 directionToEnemyyy = enemy.position - transform.position;
                        directionToEnemyyy.y = 0f;
                        // Look at the enemy only in the horizontal direction
                        if (directionToEnemyyy != Vector3.zero)
                        {
                            transform.rotation = Quaternion.LookRotation(directionToEnemyyy);
                        }

                    }

                }
            }





        }

    }

    void FindClosestBatata()
    {
        GameObject[] batatas = GameObject.FindGameObjectsWithTag(batataTag);
        float closestDistance = Mathf.Infinity;
        enemy = null;

        foreach (GameObject batata in batatas)
        {
            float distance = Vector3.Distance(transform.position, batata.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                enemy = batata.transform;
            }
        }
    }




}
