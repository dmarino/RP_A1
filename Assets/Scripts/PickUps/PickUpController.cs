using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public static PickUpController instance;
    [SerializeField] int posibilityOfSpawn = 50;


    [SerializeField] GameObject[] pickups;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;        
    }

    public void SpawnPickUp(Transform t){

        int posibility = Random.Range(0,100);

        if(posibility < posibilityOfSpawn){

            int pickUp = Random.Range(0,pickups.Length);
            Instantiate(pickups[pickUp], t.position, Quaternion.identity);

        }
    }
}
