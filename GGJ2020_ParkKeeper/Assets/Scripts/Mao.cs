using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mao : MonoBehaviour
{
    [SerializeField]
    private Transform handPivot;
    [SerializeField]
    private SpriteRenderer playerRenderer;
    private SpriteRenderer itemRenderer;
    private bool pego = false;
    private Item item;
    [SerializeField]
    private float speed;

    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - handPivot.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        handPivot.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

        if (handPivot.rotation.eulerAngles.z > 90 && handPivot.rotation.eulerAngles.z < 270)
        {
            if (item != null)
                itemRenderer.flipY = true;
            playerRenderer.flipX = true;
        }
        else
        {
            if (item != null)
                itemRenderer.flipY = false;
            playerRenderer.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (pego && item != null)
            {
                StartCoroutine(DelayPego());
                item.transform.SetParent(null);
                item.Largar();
                item = null;
                itemRenderer = null;
            }
            else if (!pego && item == null)
            {
                RaycastHit2D raycastHit;
                raycastHit = Physics2D.CircleCast(transform.position, 0.55f, Vector3.forward, 0, 256); //esse 256 representa a layer 8 da unity (objetos pegáveis tem q estar nela)
                if (raycastHit.collider != null)
                {
                    StopAllCoroutines();
                    StartCoroutine(DelayPego());
                    raycastHit.collider.transform.SetParent(this.transform);
                    raycastHit.collider.transform.localPosition = new Vector3(0, 0, 0);
                    item = raycastHit.collider.gameObject.GetComponent<Item>();
                    item.transform.localRotation = Quaternion.identity;
                    itemRenderer = item.transform.gameObject.GetComponent<SpriteRenderer>();
                    item.Pegar();
                }
            }

        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Objeto")
    //    {
    //        if (Input.GetKey(KeyCode.E) && !pego && item == null)
    //        {
    //            StopAllCoroutines();
    //            StartCoroutine(DelayPego());
    //            collision.transform.SetParent(this.transform);
    //            collision.transform.localPosition = new Vector3(0, 0, 0);
    //            item = collision.gameObject.GetComponent<Item>();
    //            item.transform.localRotation = Quaternion.identity;
    //            item.Pegar();
    //        }
    //    }
    //}

    public Transform GetPivot()
    {
        return handPivot;
    }

    private IEnumerator DelayPego()
    {
        yield return new WaitForSeconds(0.3f);
        pego = !pego;
    }
}