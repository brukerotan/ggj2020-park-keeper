using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public float lifeSpan = 0.6f;
    private float currentLifeSpan = 0;
    public float speed = 10f;
    private bool dying = false;
    private SpriteRenderer sprRenderer;

    ProjectileManager projectileManager;
    private void Start()
    {
        projectileManager = ProjectileManager.GetProjectileManager();
        sprRenderer = GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        dying = false;
        sprRenderer.color = new Color(1, 1, 1, 1f);
        currentLifeSpan = 0;
        projectileManager.ReturnSmoke(gameObject);
    }

    void FixedUpdate()
    {
        if (dying == true)
            return;

        float deltaTime = Time.fixedDeltaTime;
        transform.position += transform.right * speed * deltaTime;
        currentLifeSpan += deltaTime;
        if (currentLifeSpan >= lifeSpan)
        {
            dying = true;
            StartCoroutine(DeathSequence());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("fogo"))
        {
            collision.gameObject.GetComponent<Desastre>().TomarDano();
            dying = true;
            StartCoroutine(DeathSequence());
        }
    }

    private IEnumerator DeathSequence()
    {
        float deltaTime = Time.fixedDeltaTime;
        sprRenderer.color = new Color(1, 1, 1, 0.9f);
        transform.localScale = transform.localScale / 1.3f;
        transform.position += transform.right * speed * deltaTime / 2;
        yield return new WaitForSeconds(0.02f);
        sprRenderer.color = new Color(1, 1, 1, 0.8f);
        transform.localScale = transform.localScale / 1.2f;
        transform.position += transform.right * speed * deltaTime / 2;
        yield return new WaitForSeconds(0.02f);
        sprRenderer.color = new Color(1, 1, 1, 0.7f);
        transform.localScale = transform.localScale / 1.2f;
        transform.position += transform.right * speed * deltaTime / 2;
        yield return new WaitForSeconds(0.02f);
        sprRenderer.color = new Color(1, 1, 1, 0.6f);
        transform.localScale = transform.localScale / 1.2f;
        transform.position += transform.right * speed * deltaTime / 2;
        yield return new WaitForSeconds(0.02f);
        sprRenderer.color = new Color(1, 1, 1, 0.5f);
        transform.localScale = transform.localScale / 1.2f;
        transform.position += transform.right * speed * deltaTime / 2;
        yield return new WaitForSeconds(0.02f);
        sprRenderer.color = new Color(1, 1, 1, 0.4f);
        transform.localScale = transform.localScale / 1.2f;
        transform.position += transform.right * speed * deltaTime / 2;
        yield return new WaitForSeconds(0.02f);
        sprRenderer.color = new Color(1, 1, 1, 0.3f);
        transform.localScale = transform.localScale / 1.2f;
        transform.position += transform.right * speed * deltaTime / 2;
        yield return new WaitForSeconds(0.02f);
        sprRenderer.color = new Color(1, 1, 1, 0.2f);
        transform.localScale = transform.localScale / 1.2f;
        transform.position += transform.right * speed * deltaTime / 2;
        yield return new WaitForSeconds(0.02f);
        sprRenderer.color = new Color(1, 1, 1, 0.1f);
        transform.localScale = transform.localScale / 1.2f;
        transform.position += transform.right * speed * deltaTime / 2;
        yield return new WaitForSeconds(0.02f);
        gameObject.SetActive(false);
    }
}
