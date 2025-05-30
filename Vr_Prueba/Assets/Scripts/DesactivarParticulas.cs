using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarParticulas : MonoBehaviour
{
    public ParticleSystem particulas;

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el que entr� es el Player (por tag)
        if (other.CompareTag("Untagged"))
        {
            if (particulas != null && particulas.isPlaying)
            {
                particulas.Stop(); // Detiene la emisi�n de part�culas
                Debug.Log("Part�culas desactivadas porque el Player entr� al �rea.");
            }
        }
    }
}
