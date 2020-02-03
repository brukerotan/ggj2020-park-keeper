using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    Player player;
    [SerializeField] ParticleSystem walkDust;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Mathf.Abs(player.rb.velocity.x) > 0.01f || Mathf.Abs(player.rb.velocity.y) > 0.01f)
        {
            //print("play");
            if (!walkDust.isPlaying)
            {
                walkDust.Play();
            }
        }
        else
        {
            //print("stop"); 
            walkDust.Stop();
        }
        
    }
}
