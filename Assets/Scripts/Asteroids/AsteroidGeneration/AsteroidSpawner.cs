//Copyright (C) 2020, Nicolas Morales Escobar. All rights reserved.

using System.Collections.Generic;
using System.Collections;
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
    [SerializeField] private Vector2 spawnDelayMinMax;
    [SerializeField] private Transform bottomT;
    [SerializeField] private float baseVelocity = 3f;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float baseMultiplier;
    [SerializeField] private float increaseSpeed = .5f;

    private MonoBehaviour monoBehaviour;

    private float multiplier;
    private int asteroidCount;
    private int asteroidsToSpawn = 1;

    private float timer;

    public void Initialize(MonoBehaviour monoBehaviour)
    {
        this.monoBehaviour = monoBehaviour;
        asteroidPool.Initialize();
        multiplier = baseMultiplier;
    }

    public void Execute()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenSpawns)
        {
            for (int i = 0; i < asteroidsToSpawn; i++)
            {
                monoBehaviour.StartCoroutine(SpawnDelay());
            }
            timer = 0f;
        }
    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(Random.Range(spawnDelayMinMax.x, spawnDelayMinMax.y));
        SpawnAsteroid();
    }

    private void SpawnAsteroid()
    {
        GameObject asteroidGO = asteroidPool.GetInstance();

        Asteroid asteroid = asteroidGO.GetComponent<Asteroid>();

        asteroid.onRequireDestroy += RemoveAsteroid;

        AsteroidMovement asteroidMovement = asteroid.GetComponent<AsteroidMovement>();

        if (asteroidCount % 3 == 0)
        {
            multiplier += Time.time * Time.deltaTime * increaseSpeed;
        
            asteroidMovement.velocity = baseVelocity + baseVelocity * multiplier;
        }
        asteroidMovement.velocity = Mathf.Clamp(asteroidMovement.velocity, 0, maxSpeed);

        Transform asteroidT = asteroidGO.transform;
        asteroidT.position = spawnPosition;
        
        SetAsteroidPosition(asteroidT);
        Vector3 target = bottomT.position;
        target.x = Random.Range(xMinMax.x, xMinMax.y);
        
        asteroidMovement.direction = (target - asteroidT.position).normalized;
        
        asteroidGO.SetActive(true);

        asteroidCount++;
        if (asteroidCount % 15 == 0)
        {
            asteroidsToSpawn++;
        }
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
