using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    Vector3[] paths;

    void Start()
    {
        
    }

    int currentIndex = 0;

    public void Move(int steps)
    {
        int newIndex = currentIndex + steps;
        if (newIndex > paths.Length)
        {
            currentIndex = newIndex - paths.Length;
        }
        transform.position = paths[currentIndex];
    }
}
