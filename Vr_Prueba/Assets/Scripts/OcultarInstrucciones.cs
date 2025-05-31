using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcultarInstrucciones : MonoBehaviour
{
    public GameObject lavaryponer;



    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el que entró es el Player (por tag)
        if (other.CompareTag("ManoDerecha"))
        {

            lavaryponer.SetActive(false); // Detiene la emisión de partículas




        }
    }
}
