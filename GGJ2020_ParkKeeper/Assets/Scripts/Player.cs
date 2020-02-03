using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    Vector2 moveDirection = new Vector2();
    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ClampMagnitude pro jogador não andar mais rápido 
        rb.velocity = Vector2.ClampMagnitude(moveDirection, 1.0f) * speed;
    }
}
