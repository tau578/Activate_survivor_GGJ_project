using UnityEngine;

public class enemyDetector : MonoBehaviour
{

    



    public bool hasEnemyInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            hasEnemyInside = true;
            Debug.Log("true");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            hasEnemyInside = false;
            Debug.Log("false");
        }
    }
}