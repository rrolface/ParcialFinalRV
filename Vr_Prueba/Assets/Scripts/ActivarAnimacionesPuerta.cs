using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarAnimacionesPuerta : MonoBehaviour
{
    public Animator animador1;
    public Animator animador2;
    public string triggerName = "Activar"; // Nombre del trigger en los Animators

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (animador1 != null) animador1.SetTrigger(triggerName);
            if (animador2 != null) animador2.SetTrigger(triggerName);
        }
    }
}
