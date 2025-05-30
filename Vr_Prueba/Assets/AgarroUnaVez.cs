using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgarroUnaVez : MonoBehaviour
{
    public bool Agarro = false;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ManoIzquierda") || other.CompareTag("ManoDerecha"))
        {
            Agarro = true;



        }
    }
}
