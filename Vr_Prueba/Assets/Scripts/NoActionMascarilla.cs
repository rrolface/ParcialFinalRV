using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NoActionMascarilla : XRSocketInteractor
{
    public string targetTag;

    protected override void OnEnable()
    {
        base.OnEnable();
        // Suscribirse al evento cuando un objeto entra al socket
        selectEntered.AddListener(OnObjectInserted);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        // Evitar fugas de memoria
        selectEntered.RemoveListener(OnObjectInserted);
    }

    // Validar si puede hacer hover (pasar por encima)
    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && interactable.transform.tag == targetTag;
    }

    // Validar si puede ser seleccionado por el socket
    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && interactable.transform.tag == targetTag;
    }

    // Método que se llama cuando se inserta el objeto
    private void OnObjectInserted(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag(targetTag))
        {
            // Desactiva el objeto al insertarse
            args.interactableObject.transform.gameObject.SetActive(false);
        }
    }

}
