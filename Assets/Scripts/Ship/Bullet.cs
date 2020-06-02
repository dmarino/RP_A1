using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5.0f;
    [SerializeField] float lifeTime = 5.0f;

    Rigidbody2D rb;


    void Start()
    {
        
        //gets the reference to the rigidbody
        rb = GetComponent<Rigidbody2D>();

        //sets the lifetime of the bullet
        Destroy(gameObject, lifeTime);

    }

    private void FixedUpdate()
    {
        //sets the velocity of the bullet
        rb.velocity = Vector3.up * bulletSpeed;
    }

}
