using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto : MonoBehaviour
{

    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            print("tiro");
        }
    }
    
}
