using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavadoManos : MonoBehaviour
{
    public bool lavadoManos = false;

    public bool lavadoDerecha=false;
    public bool lavadoIzquierda = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

      
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ManoDerecha"))
        {
            lavadoDerecha = true;
            Debug.Log("se lavo la derecha");

        }

        if (other.CompareTag("ManoIzquierda"))
        {
            lavadoIzquierda = true;
            Debug.Log("izquierda");

        }



        if (lavadoIzquierda && lavadoDerecha)
        {
            lavadoManos = true;
            Debug.Log("lavado de manos");
        }

    }

}
