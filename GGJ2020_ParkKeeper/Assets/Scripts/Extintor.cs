using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extintor : Item
{
    public float fireCooldown = 0.4f;
    private float cooldown = 0;
    public Transform emitterPos;
    ProjectileManager projectileManager;
    private float initialAudioVolume = 0;

    new protected void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.GetProjectileManager();
        initialAudioVolume = audioS.volume;
    }
    private void Update()
    {
        if (!onHand)
            return;

        if (cooldown > 0)
            cooldown -= Time.deltaTime;
        else if (Input.GetKey(KeyCode.Mouse0)) { Fire(); }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StopAllCoroutines();
            audioS.volume = initialAudioVolume;
            audioS.Play();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
            StartCoroutine(AudioFadeOut());
    }

    private void Fire()
    {
        //print(emitterPos);
        cooldown = fireCooldown;
        GameObject temp = projectileManager.GetSmoke();
        temp.SetActive(true);
        float tempRandom = Random.Range(1.1f, 2.8f);
        temp.transform.localScale = new Vector3(tempRandom, tempRandom, 1);
        temp.transform.position = emitterPos.position;
        temp.transform.rotation = emitterPos.rotation;
    }

    private IEnumerator AudioFadeOut()
    {
        //float x = 0;
        while (audioS.volume > 0.1f)
        {
            yield return new WaitForEndOfFrame();
            audioS.volume -= 0.04f;
            //x += Time.deltaTime;
        }
        //print(x);
        audioS.volume = 0;
        yield return new WaitForEndOfFrame();
        audioS.Stop();
    }
}
