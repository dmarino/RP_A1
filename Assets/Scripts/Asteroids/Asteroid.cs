﻿using System;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public event Action<Asteroid> onRequireDestroy;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bullet"))
        {
            onRequireDestroy?.Invoke(this);
        }
    }
    
    private void OnBecameInvisible()
    {
        onRequireDestroy?.Invoke(this);
    }
}