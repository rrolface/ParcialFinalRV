using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarParticulas : MonoBehaviour
{
    public GameObject particulas;
    public GameObject panelrevisa;
  

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el que entr� es el Player (por tag)
        if (other.CompareTag("ManoDerecha"))
        {
          
                particulas.SetActive(false); // Detiene la emisi�n de part�culas
            panelrevisa.SetActive(false);
            
                Debug.Log("Part�culas desactivadas porque el Player entr� al �rea.");

            
        }
    }
}
