using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarParticulas : MonoBehaviour
{
    public ParticleSystem particulas;

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el que entró es el Player (por tag)
        if (other.CompareTag("Untagged"))
        {
            if (particulas != null && particulas.isPlaying)
            {
                particulas.Stop(); // Detiene la emisión de partículas
                Debug.Log("Partículas desactivadas porque el Player entró al área.");
            }
        }
    }
}
