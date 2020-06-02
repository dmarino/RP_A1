using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpParent : MonoBehaviour
{
    [SerializeField] float lifeTime = 5.0f;
    
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PickedUp(other);
            Destroy(gameObject);
        }
        
    }
    
    protected virtual void PickedUp(Collider2D other)
    {

    }
}
