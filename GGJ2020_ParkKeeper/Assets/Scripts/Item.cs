using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected bool onHand = false;
    protected SpriteRenderer spriteRenderer;
    protected AudioSource audioS;

    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioS = GetComponent<AudioSource>();
    }

    public virtual void Pegar()
    {
        onHand = true;
        spriteRenderer.sortingOrder = 20;
    }

    public virtual void Largar()
    {
        transform.position -= new Vector3(0, 0.4f);
        if (spriteRenderer.flipY)
            transform.rotation = Quaternion.Euler(0, 0, 215f);
        else
            transform.rotation = Quaternion.Euler(0, 0, -35f);
        spriteRenderer.sortingOrder = 5;
        onHand = false;
    }

    public virtual void ForcarAudio()
    {
        audioS.pitch = Random.Range(0.9f, 1.1f);
        audioS.Play();
    }
}
