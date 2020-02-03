using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desastre : MonoBehaviour
{
    [SerializeField] int vidaMaxima = 10;
    [SerializeField] int vida;
    [SerializeField] Vector2 spawnOffsetMin;
    [SerializeField] Vector2 spawnOffsetMax;
    [SerializeField] GameObject instantiateOnDamage;

    void Start()
    {
        vida = vidaMaxima;
        transform.position = (Vector2)transform.position + Vector2.MoveTowards(spawnOffsetMin, spawnOffsetMax, Random.Range(0, Vector2.Distance(spawnOffsetMin, spawnOffsetMax)));
    }

    public void TomarDano()
    {
        vida--;
        if (instantiateOnDamage != null)
        {
            Instantiate(instantiateOnDamage, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)), Quaternion.identity);
        }
        if (vida <= 0)
        {
            Atracao temp = GetComponentInParent<Atracao>();
            if (transform.parent.gameObject == Parque.GetParque().gameObject)
                GameManager.GetGameManager().SomarScore(300);
            print(transform.parent);
            if (temp != null)
                temp.DesastreEliminado(gameObject);
        }
    }
}
