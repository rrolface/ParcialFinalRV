using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarParticulas : MonoBehaviour
{
    public GameObject particulas;
    public GameObject panelrevisa;
  

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el que entró es el Player (por tag)
        if (other.CompareTag("ManoDerecha"))
        {
          
                particulas.SetActive(false); // Detiene la emisión de partículas
            panelrevisa.SetActive(false);
            
                Debug.Log("Partículas desactivadas porque el Player entró al área.");

            
        }
    }
}
