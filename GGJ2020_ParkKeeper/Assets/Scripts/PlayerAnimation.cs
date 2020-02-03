using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Player player;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("absVelocityX", Mathf.Abs(player.rb.velocity.x));
        anim.SetFloat("absVelocityY", Mathf.Abs(player.rb.velocity.y));
    }
}
