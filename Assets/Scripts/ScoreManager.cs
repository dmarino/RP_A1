using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    public Text scoreText;
    [SerializeField] private int asteroidDestroyedScore=10;

    private int score=0;
    private bool isAlive;


    private void Start()
    {
        instance = this;
        isAlive = true;
        
        StartCoroutine(TimeScoring());
    }


    private void Update()
    {
        scoreText.text = score.ToString("D12");
    }
    public void AsteroidDestroyed(){
        score += asteroidDestroyedScore;
    }


    public void PlayerDied(){
        isAlive = false;
    }

    IEnumerator TimeScoring(){

        while(isAlive){
            score++;
            yield return new WaitForSeconds (1f);
        }
    }
}
