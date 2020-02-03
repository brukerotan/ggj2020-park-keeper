using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesastreManager : MonoBehaviour
{
    public static DesastreManager desastreManager; // singleton
    public static DesastreManager GetDesastreManager() { return desastreManager; } // singleton (para encontrarem ele: DesastreManager.GetDes(...))

    private List<Atracao> listaAtracoes;
    private List<int> indexListaAtracoes = new List<int>();
    private float timerAtual = 0;
    private float timerThreshold;
    [SerializeField] private float intervaloSpawnInicial = 10;
    [SerializeField] private float intervaloSpawnMinimo = 4f;

    private GameManager gameManager;

    private void Awake() //singleton
    {
        if (desastreManager == null)
            desastreManager = this;
        else
            Destroy(gameObject);
    }
    private void OnDisable() //follow up do singleton
    {
        if (desastreManager == this)
            desastreManager = null;
    }

    private void Start()
    {
        listaAtracoes = new List<Atracao>(FindObjectsOfType<Atracao>());
        //for (int i = 0; i < listaAtracoes.Count; i++)
        //{
        //    if (listaAtracoes[i].ehParque)
        //    {
        //        listaAtracoes.RemoveAt(i);
        //        break;
        //    }
        //}
        //print(listaAtracoes.Count + " atracoes no parque nesse momento maravilhoso");
        gameManager = GameManager.GetGameManager();
        timerThreshold = intervaloSpawnInicial;
        timerAtual = intervaloSpawnInicial - 1; // para brotar o primeiro desastre com 1 segundo de jogo.
    }

    private void RefreshIndexList()
    {
        indexListaAtracoes.Clear();
        for (int i = 0; i < listaAtracoes.Count; i++)
            indexListaAtracoes.Add(i);
    }

    private void Update()
    {
        timerThreshold = intervaloSpawnMinimo + ((intervaloSpawnInicial - intervaloSpawnMinimo) * gameManager.GetPercentualDuracao());
        //print(timerThreshold);

        timerAtual += Time.deltaTime;
        if (timerAtual >= timerThreshold)
        {
            RefreshIndexList();
            bool continuaLoop = true;
            while (indexListaAtracoes.Count > 0 && continuaLoop)
            {
                int randomInt = Random.Range(0, indexListaAtracoes.Count);
                int indexSorteado = indexListaAtracoes[randomInt];
                indexListaAtracoes.RemoveAt(randomInt);
                if (!listaAtracoes[indexSorteado].ehParque && listaAtracoes[indexSorteado].SpawnDeAtracoes())
                    continuaLoop = false;
                //print(listaAtracoes.Count - indexListaAtracoes.Count + " " + continuaLoop);
            }
            timerAtual = 0;
        }
    }

    public int GetPontuacao()
    {
        int tempPontuacao = 0;
        for (int i = 0; i < listaAtracoes.Count; i++)
            if (listaAtracoes[i].estaDandoPonto)
                tempPontuacao++;
        return tempPontuacao;
    }
}
