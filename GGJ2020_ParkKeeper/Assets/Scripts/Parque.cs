using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parque : MonoBehaviour
{
    public static Parque parque;
    public static Parque GetParque() { return parque; }
    public Atracao atracaoParque;
    private void Awake() { if (parque == null) parque = this; else { Destroy(gameObject); return; } atracaoParque = GetComponent<Atracao>(); }
    private void OnDisable() { if (parque == this) parque = null; }
}
