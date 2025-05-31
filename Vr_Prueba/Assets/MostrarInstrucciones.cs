using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarInstrucciones : MonoBehaviour
{
    public GameObject lavaryponer;
    


    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el que entró es el Player (por tag)
        if (other.CompareTag("ManoDerecha"))
        {

            lavaryponer.SetActive(true); // Detiene la emisión de partículas
           

     

        }
    }
}
