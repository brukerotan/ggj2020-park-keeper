using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator animator;

    private int levelToLoad;

    void Start()
    {
        Time.timeScale = 1;
    }

    public void PlayGame()
    {
        FadeToLevel(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("Fadeout");
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
