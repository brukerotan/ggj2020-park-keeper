using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class thx : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(tchau());
    }
    private IEnumerator tchau()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}
