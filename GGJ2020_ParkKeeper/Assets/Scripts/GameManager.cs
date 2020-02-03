using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public static GameManager GetGameManager() { return gameManager; }

    private int score;
    private float timer;
    private int scoreFinal;
    [SerializeField] float score1Estrela = 10000;
    [SerializeField] float score2Estrela = 15000;
    [SerializeField] float score3Estrela = 30000;
    private int segundos, minutos;
    public float duracaoPartida = 2;
    private DesastreManager desastreManager;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI tempoUI;
    [SerializeField] GameObject levelTerminadoUI; 
    public TextMeshProUGUI scoreUITerminado;
    public TextMeshProUGUI notaUI;
    [SerializeField] string proximoLevel;

    // Start is called before the first frame update

    private void Awake()
    {
        if (gameManager == null)
            gameManager = this;
        else
            Destroy(gameObject);
    }

    private void OnDisable()
    {
        if (gameManager == this)
            gameManager = null;
    }

    void Start()
    {
        levelTerminadoUI.gameObject.SetActive(false);
        Time.timeScale = 1;
        scoreFinal = 1;
        score = 0;
        segundos = Mathf.RoundToInt((duracaoPartida - Mathf.FloorToInt(duracaoPartida)) * 60);
        //print(segundos + " segundos");
        minutos = Mathf.FloorToInt(duracaoPartida);
        //print(minutos + " minutos");
        desastreManager = DesastreManager.GetDesastreManager();
    }

    // Update is called once per frame
    void Update()
    {
        if(minutos <= 0)
        {
            if(segundos <= 0)
            {
                Time.timeScale = 0;
                levelTerminadoUI.SetActive(true);
                scoreUITerminado.text = "Score: " + scoreFinal.ToString();
                if (Input.GetKeyDown(KeyCode.T) && scoreFinal >= score1Estrela)
                {
                    SceneManager.LoadScene(proximoLevel);
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    SceneManager.LoadScene("Main menu");
                }

                if (scoreFinal >= score3Estrela)
                {
                    notaUI.text = "PERFECT!!\n [Press [T] to next level] or [Press [R] to retry]\n [Press [ESC] to quit to menu]";
                }
                else if (scoreFinal >= score2Estrela)
                {
                    notaUI.text = "That was a good job!\n [Press [T] to next level] or [Press [R] to retry]\n [Press [ESC] to quit to menu]";
                }
                else if (scoreFinal >= score1Estrela)
                {
                    notaUI.text = "nice :)\n [Press [T] to next level] or [Press [R] to retry]\n [Press [ESC] to quit to menu]";
                }
                else
                {
                    notaUI.text = "Not good enough... but don't give up!\n[Press [R] to retry]\n [Press [ESC] to quit to menu]";
                }
            }
        }

        if (segundos < 0)
        {
            minutos--;
            segundos = 59;
            if (minutos <= 0)
                return;
        }
        if (timer < Time.time)
        {
            score += 10 * desastreManager.GetPontuacao() * Mathf.RoundToInt(100 - GetPercentualDuracao() * 100) / 3;
            timer = Time.time + 1;
            segundos--;
        }

        scoreFinal = score;
        

        scoreUI.text = scoreFinal.ToString();
        tempoUI.text = minutos + ":" + segundos.ToString("00");
    }

    public void SomarScore(int x)
    {
        score += x;
        scoreUI.text = scoreFinal.ToString();
    }

    public float GetPercentualDuracao()
    {
        float resultadoFloat = (minutos + ((float)segundos / 60)) / duracaoPartida;
        //print(resultadoFloat);
        return resultadoFloat;
    }
}
