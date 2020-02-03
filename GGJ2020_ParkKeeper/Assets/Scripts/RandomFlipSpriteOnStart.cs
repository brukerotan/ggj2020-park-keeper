using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFlipSpriteOnStart : MonoBehaviour
{
    void Start()
    {
        if(Random.Range(0, 100) < 50)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
