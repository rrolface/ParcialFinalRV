using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcultarInstrucciones : MonoBehaviour
{
    public GameObject lavaryponer;



    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el que entr� es el Player (por tag)
        if (other.CompareTag("ManoDerecha"))
        {

            lavaryponer.SetActive(false); // Detiene la emisi�n de part�culas




        }
    }
}
