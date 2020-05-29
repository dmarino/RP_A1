using System;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public event Action<Asteroid> onRequireDestroy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onRequireDestroy?.Invoke(this);
        }
    }
    
    private void OnBecameInvisible()
    {
        onRequireDestroy?.Invoke(this);
    }
}