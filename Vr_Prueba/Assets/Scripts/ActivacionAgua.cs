using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class ActivacionAgua : MonoBehaviour
{
    public Animator aguaAnimator;

    public void AbrirAgua()
    {
        if (aguaAnimator != null)
        {
            aguaAnimator.Play("Agua_caer");
            Debug.Log("Agua abierta");
        }
    }

    public void CerrarAgua()
    {
        if (aguaAnimator != null)
        {
            aguaAnimator.Play("Agua_cerrar");
            Debug.Log("Agua cerrada");
        }
    }
}
