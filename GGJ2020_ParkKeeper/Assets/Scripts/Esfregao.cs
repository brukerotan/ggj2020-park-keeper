using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esfregao : Item
{
    private Transform transformPivot;
    private float lastRotation;
    private float currentRotation;
    private float cd;
    private float cd2;
    [SerializeField] private Sprite spriteNormal;
    [SerializeField] private Sprite spriteTorto;

    private void Update()
    {
        if (onHand)
        {
            cd += Time.deltaTime;
            if (cd > 0.15f)
            {
                cd = 0;
                lastRotation = currentRotation;
                currentRotation = transformPivot.rotation.eulerAngles.z;
                if (lastRotation - currentRotation > 15)
                {
                    spriteRenderer.sprite = spriteTorto;
                    if (spriteRenderer.flipY)
                        spriteRenderer.flipX = false;
                    else
                        spriteRenderer.flipX = true;
                }
                else if (lastRotation - currentRotation < -15)
                {
                    spriteRenderer.sprite = spriteTorto;
                    if (spriteRenderer.flipY)
                        spriteRenderer.flipX = true;
                    else
                        spriteRenderer.flipX = false;
                }
                else
                {
                    spriteRenderer.sprite = spriteNormal;
                }
            }
        }
    }

    public override void Pegar()
    {
        base.Pegar();
        transformPivot = GetComponentInParent<Mao>().GetPivot();
    }

    public override void Largar()
    {
        base.Largar();
        transformPivot = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        cd2 += Time.deltaTime;
        if (onHand && cd2 > 0.5f && collision.gameObject.CompareTag("vomito") && Mathf.Abs(currentRotation - lastRotation) > 4f)
        {
            cd2 = 0;
            collision.gameObject.GetComponent<Desastre>().TomarDano();
            audioS.pitch = Random.Range(0.9f, 1.1f);
            audioS.Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onHand && collision.gameObject.CompareTag("vomito") && Mathf.Abs(currentRotation - lastRotation) > 4f)
        {
            collision.gameObject.GetComponent<Desastre>().TomarDano();
            audioS.pitch = Random.Range(0.9f, 1.1f);
            audioS.Play();
        }
    }
}
