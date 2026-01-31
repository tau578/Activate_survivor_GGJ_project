using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    // The shared variable "a"
    private float a = 0f;

    // Singleton instance
    private static VariableManager instance;

    // Get the singleton instance
    public static VariableManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<VariableManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(VariableManager).Name);
                    instance = singletonObject.AddComponent<VariableManager>();
                }
            }
            return instance;
        }
    }

    // Getter for variable "a"
    public float GetA()
    {
        return a;
    }

    // Setter for variable "a"
    public void SetA(float newValue)
    {
        if (a != newValue)
        {
            a = newValue;
            Debug.Log("Variable 'a' changed to: " + a);
        }
    }

    // Method to add a value to variable "a"
    public void AddToA(float valueToAdd)
    {
        a += valueToAdd;
        
    }
}
