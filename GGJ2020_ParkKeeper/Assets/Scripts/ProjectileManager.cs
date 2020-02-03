using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager projectileManager;
    public static ProjectileManager GetProjectileManager() { return projectileManager; }

    public GameObject smokePrefab;

    private Queue<GameObject> smokeQueue;

    private void Awake() //singleton
    {
        if (projectileManager == null)
            projectileManager = this;
        else
            Destroy(gameObject);
    }
    private void OnDisable()
    {
        if (projectileManager == this)
            projectileManager = null;
    }

    private void Start()
    {
        smokeQueue = new Queue<GameObject>();
        GameObject temp;
        for (int i = 0; i < 25; i++)
            temp = Instantiate(smokePrefab);
    }

    public GameObject GetSmoke()
    {
        if(smokeQueue.Count == 0)
        {
            GameObject temp;
            for (int i = 0; i < 5; i++)
                temp = Instantiate(smokePrefab);
        }
        return smokeQueue.Dequeue();
    }

    public void ReturnSmoke(GameObject smokeGo)
    {
        smokeQueue.Enqueue(smokeGo);
    }
}
