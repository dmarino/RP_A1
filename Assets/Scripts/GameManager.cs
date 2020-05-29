
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AsteroidSpawner asteroidSpawner;
        
    private void Awake()
    {
        asteroidSpawner.Initialize();
    }
    private void Update()
    {
        asteroidSpawner.Execute();
    }
}