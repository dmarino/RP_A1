//Copyright (C) 2020, Nicolas Morales Escobar. All rights reserved.

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DynamicGameObjectPool
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int startingInstances;
    
    private List<GameObject> gameObjects;
    
    public void Initialize()
    {
        gameObjects = new List<GameObject>();
    
        for (int i = 0; i < startingInstances; i++)
        {
            gameObjects.Add(SpawnInstance());
        }
    }
    
    protected GameObject SpawnInstance()
    {
        GameObject gameObject = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        gameObject.SetActive(false);
        return gameObject;
    }
    
    protected virtual void Reset(GameObject gameObject)
    {
        gameObject.SetActive(false);
            
        Transform transform = gameObject.transform;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
    
    public virtual GameObject GetInstance()
    {
        GameObject gameObject = gameObjects.Count > 0 ? DeallocateGameObject() : SpawnInstance();
        Reset(gameObject);
        return gameObject;
    }
    
    protected GameObject DeallocateGameObject()
    {
        GameObject gameObject = gameObjects[0];
        gameObjects.RemoveAt(0);
        return gameObject;
    }
    
    public void Restore(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObjects.Add(gameObject);
    }
}