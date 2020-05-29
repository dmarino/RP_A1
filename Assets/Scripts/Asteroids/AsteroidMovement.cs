using System;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public Vector3 direction;
    public float velocity;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * velocity;
    }
}