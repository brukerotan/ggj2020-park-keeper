using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Atracao : MonoBehaviour
{

    [SerializeField] int vidaMaxima;
    int vidaAtual = 0;
    [SerializeField] float intervaloDeDano = 1f;
    float timerDano = 0;
    [SerializeField] Color corDoDano = Color.red;
    [SerializeField] Color corDoConserto = Color.green;
    [SerializeField] Color corQuebrado = Color.gray;
    Color corAlvo = Color.white;
    [SerializeField] string tagConsertar = "consertar";

    [SerializeField] bool estaQuebrado = false;

    [SerializeField] private SpriteRenderer avisoUI;
    [SerializeField] private Sprite perigoUI;
    [SerializeField] private Sprite quebrouUI;
    private bool avisoLigado = false;

    public bool estaDandoPonto { get; private set; }
    public bool ehParque;

    [SerializeField] List<GameObject> desastresPossiveis = new List<GameObject>();
    public List<GameObject> desastresAtuais = new List<GameObject>();

    [SerializeField] int desastresMaximos = 4;
    [SerializeField] float offsetMultiplier = 1;
    //[SerializeField] float tempoMinimioParaTentarDesastre = 5;
    //[SerializeField] float tempoMaximoParaTentarDesastre = 10;
    //[SerializeField] float chanceBaseDeDesastre = 20f;

    public Action OnHeal;

    //bool estaSobAtaque = false;

    [SerializeField] GameObject consertoParticula;

    [SerializeField] List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    void Start()
    {
        vidaAtual = vidaMaxima;
        if (!ehParque)
            avisoUI.enabled = false;
    }

    private void Update()
    {
        //SpawnDeAtracoes();
        CicloDeDano();

        if (desastresAtuais.Count > 0 || (estaQuebrado && !ehParque))
        {
            estaDandoPonto = false;
            if (!ehParque)
            {
                if (!avisoLigado)
                    StartCoroutine(PiscarAviso());
                if (estaQuebrado && desastresAtuais.Count == 0)
                    avisoUI.sprite = quebrouUI;
                else
                    avisoUI.sprite = perigoUI;
            }
        }
        else
        {
            if (!ehParque)
            {
                StopAllCoroutines();
                avisoUI.enabled = false;
                avisoLigado = false;
            }
            estaDandoPonto = true;
        }



        if (!estaQuebrado)
        {
            corAlvo = Color.white;
        }
        else
        {
            corAlvo = Color.grey;
        }

        foreach (SpriteRenderer sprRend in spriteRenderers)
        {
            sprRend.color = Vector4.MoveTowards(sprRend.color, corAlvo, 5.0f * Time.deltaTime);
        }

    }

    public void SpawnExterno(GameObject spawnling)
    {
        desastresAtuais.Add(spawnling);
    }

    public bool SpawnDeAtracoes()
    {
        if (estaQuebrado || desastresAtuais.Count > 0)
        {
            return false;
        }
        else
        {
            //spawna
            int indexAleatorio = Random.Range(0, desastresPossiveis.Count);
            GameObject desastreEscolhido = desastresPossiveis[indexAleatorio];
            GameObject desastre;
            for (int i = 0; i < Random.Range(1, desastresMaximos + 1); i++)
            {
                desastre = Instantiate(desastreEscolhido,
                                     (Vector2)transform.position,// + new Vector2(Random.Range(-2, 2), Random.Range(-2, 2)),
                                     Quaternion.identity, transform);
                desastresAtuais.Add(desastre);
            }
            return true;
        }

    }

    void CicloDeDano()
    {
        if (desastresAtuais.Count > 0 && !estaQuebrado)
        {
            timerDano += Time.deltaTime;
            if (timerDano > intervaloDeDano)
            {
                timerDano = 0;
                vidaAtual--;
                foreach (SpriteRenderer sprRend in spriteRenderers)
                {
                    sprRend.color = corDoDano;
                }

                if (vidaAtual <= 0)
                {
                    vidaAtual = 0;
                    estaQuebrado = true;
                }
            }
        }
        else
        {
            timerDano = 0f;
        }
    }

    public void DesastreEliminado(GameObject x)
    {
        desastresAtuais.Remove(x);
        Destroy(x);
        //print(desastresAtuais.Count);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (estaQuebrado && collision.CompareTag(tagConsertar) && desastresAtuais.Count == 0)
        {
            collision.gameObject.GetComponentInParent<Item>().ForcarAudio();
            Instantiate(consertoParticula, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)), Quaternion.identity);
            OnHeal();
            vidaAtual += 3;
            foreach (SpriteRenderer sprRend in spriteRenderers)
            {
                sprRend.color = corDoConserto;
            }
            if (vidaAtual >= vidaMaxima)
            {
                vidaAtual = vidaMaxima;
                estaQuebrado = false;
            }
        }
    }

    private IEnumerator PiscarAviso()
    {
        avisoLigado = true;
        while (avisoLigado)
        {
            yield return new WaitForSeconds(0.2f);
            avisoUI.enabled = !avisoUI.enabled;
        }
    }
}
