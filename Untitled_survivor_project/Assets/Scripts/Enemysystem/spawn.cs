using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class spawn : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;
    public Transform character; // Reference to the character's transform
    public float spawningDistance = 5f; // The distance at which the object spawns the character

    private bool alreadySpawned = false;

    public GameObject villager;
    public GameObject fireEffect;
    public GameObject poop;


    public float delayTime = 2.0f;
    float delayPoopTime = 2f;



    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (character == null)
        {
            Debug.LogWarning("Character reference not set in FollowCharacterNavMesh script!");
        }

        poop.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (character == null)
        {
            return;
        }

        // Calculate the distance between the object and the character along the XZ plane (ignoring the height difference)
        float distanceToCharacter = Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z),
                                                     new Vector3(character.position.x, 0f, character.position.z));
        
        if (!alreadySpawned && distanceToCharacter <= spawningDistance)
        {
            alreadySpawned = true;

            
            poop.SetActive(true);
            Invoke("FunctionToInvoke", delayPoopTime);
            




        }
      




    }


    private void FunctionToInvoke()
    {
        Instantiate(villager, transform.position, Quaternion.identity);
        Instantiate(fireEffect, transform.position, Quaternion.identity);

        Destroy(gameObject, delayTime);
    }



    
}
