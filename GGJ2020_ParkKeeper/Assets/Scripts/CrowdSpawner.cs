using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSpawner : MonoBehaviour
{
    [SerializeField] GameObject crowdPrefab;
    [SerializeField] Vector2 minRange = new Vector2();
    [SerializeField] Vector2 maxRange = new Vector2();
    Vector2 spawnPosition = new Vector2();

    [SerializeField] float minimumTimeToSpawn = 5;
    [SerializeField] float maximumTimeToSpawn = 30;
    float timeToSpawn = 5;
    float spawnTimer = 0;

    [SerializeField] float chanceVomito = 5f;

    private void Start()
    {
        timeToSpawn = Random.Range(minimumTimeToSpawn, maximumTimeToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > timeToSpawn)
        {
            spawnTimer = 0;
            timeToSpawn = Random.Range(minimumTimeToSpawn, maximumTimeToSpawn);
            TryToSpawn();
        }
        
    }

    void TryToSpawn()
    {
        spawnPosition = new Vector2(Random.Range(minRange.x, maxRange.x), Random.Range(minRange.y, maxRange.y));
        RaycastHit2D hit = Physics2D.BoxCast(spawnPosition, new Vector2(5, 2), 0, Vector3.forward);
        if (hit.collider != null)
        {
            return;
        }
        //print("vai spawnar s");
        GameObject crowd = Instantiate(crowdPrefab, spawnPosition, Quaternion.identity);
        crowd.GetComponent<Crowd>().vomitSpawnChance = chanceVomito;
    }
}
