using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothTime = .4f;
    Vector3 velocity = new Vector3();
    Vector3 resultVector = new Vector3();
    Vector3 shakePosition = new Vector3();

    void Start()
    {
        foreach (Atracao atr in FindObjectsOfType<Atracao>())
        {
            atr.OnHeal += Shake;
        }
    }

    void LateUpdate()
    {
        resultVector.x = target.position.x * 0.5f;
        resultVector.y = target.position.y * 0.5f;
        resultVector.z = transform.position.z;

        //transform.position = Vector3.SmoothDamp(transform.position, resultVector, ref velocity, smoothTime) + shakePosition;
        transform.position = new Vector3(0, 0, -10) + shakePosition; //camera parada, só pelo mapa atual da cena do bruno
        shakePosition = Vector3.zero;
    }

    void Shake()
    {
        shakePosition.x += 0.2f * Mathf.Sign(UnityEngine.Random.Range(-1f, 1.0f));
        shakePosition.y += 0.2f * Mathf.Sign(UnityEngine.Random.Range(-1.0f, 1.0f));
    }
}
