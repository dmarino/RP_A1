using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour
{

    [SerializeField]
    private float _moveOffset = 10f;

    private Vector2 screenBounds;


    //reference to the rigidBody
    private Rigidbody2D rb;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    }

    private void Update()
    {
        float xVel = _moveOffset * Input.GetAxis("Horizontal");
        float yVel = _moveOffset * Input.GetAxis("Vertical");

        rb.velocity = new Vector2(xVel,yVel);
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -screenBounds.x, screenBounds.x);
        pos.y = Mathf.Clamp(pos.y, -screenBounds.y, screenBounds.y);

        transform.position = pos;
        
    }
}
