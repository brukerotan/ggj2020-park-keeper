using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    [SerializeField, Range(0, 120)] float minimumCrowdDuration = 20f;
    [SerializeField, Range(0, 120)] float maximumCrowdDuration = 40f;
    [SerializeField] GameObject vomitPrefab;
    public float vomitSpawnChance;
    private Parque parque;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Random.Range(minimumCrowdDuration, maximumCrowdDuration));
        parque = Parque.GetParque();
    }

    private void OnDestroy()
    {
        if(Random.Range(0, 100) <= vomitSpawnChance)
        {
            parque.atracaoParque.SpawnExterno(Instantiate(vomitPrefab, transform.position, Quaternion.identity, parque.gameObject.transform));
        }
    }
}
