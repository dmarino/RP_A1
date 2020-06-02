using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [Header("Game Managment")]
    [SerializeField] GameObject gameOverScreen;

    //this is to chect that the player wont go out of bouds
    private Vector2 screenBounds;


    [Header("PowerUp")]

    [SerializeField] int timeOfPowerUp=10;

    private int currentPowerUpTime=0;

    [SerializeField] Slider powerUpSlider;

    //reference to the rigidBody
    private Rigidbody2D rb;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        InvokeRepeating("Fire", 0.33f, _fireRate);

        powerUpSlider.maxValue = timeOfPowerUp;

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

        Instantiate(bullet, shootingPoint.position, Quaternion.identity);
    }


    public void PickUpPowerUp(){

        if(currentPowerUpTime>0){
            currentPowerUpTime= timeOfPowerUp;
        }
        else{
            currentPowerUpTime= timeOfPowerUp;
            changeFire(false);
            StartCoroutine(PowerUp());
        }
    }

    private void changeFire(bool normal){

        float rate = _fireRate;
        if(!normal){ rate = rate/10; }

        Debug.Log(rate);

        CancelInvoke("Fire");
        InvokeRepeating("Fire", 0.33f, rate);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag.Equals("Asteroid")){

            ScoreManager.instance.PlayerDied();
            CancelInvoke("Fire");

            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver(){

        gameOverScreen.SetActive(true);
        yield return new WaitForSeconds (5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    IEnumerator PowerUp(){

        //shows slider
        powerUpSlider.gameObject.SetActive(true);

        while(currentPowerUpTime >0){

            powerUpSlider.value = currentPowerUpTime;
            currentPowerUpTime--;
            yield return new WaitForSeconds (1f);
        }

        changeFire(true);
        powerUpSlider.gameObject.SetActive(false);

    }

}
