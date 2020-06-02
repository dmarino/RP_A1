using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    public Text scoreText;

    public int nearMissScore=5;
    public int normalAvoidScore=1;
    public int asteroidDestroyedScore=2;

    private int score=0;


    void NearMiss(){
        score+= nearMissScore;
        //scoreText.text = score;
    }

    void NormalAvoid(){
        score += normalAvoidScore;
    }

    void AsteroidDestroyed(){
        score += asteroidDestroyedScore;
    }
}
