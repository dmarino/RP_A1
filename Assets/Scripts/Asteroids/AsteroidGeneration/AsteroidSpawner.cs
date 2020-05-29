//Copyright (C) 2020, Nicolas Morales Escobar. All rights reserved.

using System;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class AsteroidSpawner
{
    [Header("Spawn")] 
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private AsteroidPool asteroidPool;
    [SerializeField] private Vector2 xMinMax;

    [Header("Procedural Generation")]
    [SerializeField] private float xFactor = 1.2f;
    [SerializeField] private float yFactor = 1.2f;
    
    
    private float timer;

    public void Initialize()
    {
        asteroidPool.Initialize();
    }

    public void Execute()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenSpawns)
        {
            SpawnAsteroid();
            timer = 0f;
        }
    }

    private void SpawnAsteroid()
    {
        GameObject asteroidGO = asteroidPool.GetInstance();

        Asteroid asteroid = asteroidGO.GetComponent<Asteroid>();

        asteroid.onRequireDestroy += RemoveAsteroid;

        Transform asteroidT = asteroidGO.transform;
        asteroidT.position = spawnPosition;
        
        SetAsteroidPosition(asteroidT);
        
        asteroidGO.SetActive(true);
    }

    private void RemoveAsteroid(Asteroid asteroid)
    {
        asteroid.onRequireDestroy -= RemoveAsteroid;

        asteroidPool.Restore(asteroid.gameObject);
    }

    private void SetAsteroidPosition(Transform asteroidT)
    {
        float positionX = Random.Range(xMinMax.x, xMinMax.y);

        Vector3 position = asteroidT.position;
        position.x = positionX;
        asteroidT.position = position;
    }
}
