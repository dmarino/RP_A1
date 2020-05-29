using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour
{

    [SerializeField]
    private float _moveOffset = 10f;



    [Header("Fire")]

    [SerializeField]
    private float _fireRate = 1f;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float bulletSpeed =2f;

    [SerializeField]
    private Transform shootingPoint;




    //this is to chect that the player wont go out of bouds
    private Vector2 screenBounds;


    //reference to the rigidBody
    private Rigidbody2D rb;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        InvokeRepeating("Fire", 0.33f, _fireRate);

    }


    //here i check for the  player input
    private void Update()
    {
        float xVel = _moveOffset * Input.GetAxis("Horizontal");
        float yVel = _moveOffset * Input.GetAxis("Vertical");

        rb.velocity = new Vector2(xVel,yVel);
    }


    //here i check the player doesnt go out of bounds
    private void LateUpdate()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -screenBounds.x, screenBounds.x);
        pos.y = Mathf.Clamp(pos.y, -screenBounds.y, screenBounds.y);

        transform.position = pos;
        
    }


    //fire
    private void Fire()
    {

        GameObject b = Instantiate(bullet, shootingPoint.position, Quaternion.identity);
        b.GetComponent<Rigidbody2D>().velocity = Vector3.up * bulletSpeed;
    }

}
