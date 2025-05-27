using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomSocket : XRSocketInteractor
{
    public string targetTag;

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && interactable.transform.CompareTag(targetTag);
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        string nombre = interactable.transform.name;

        // Filtrado por tag (como tenías antes)
        if (!interactable.transform.CompareTag(targetTag))
            return false;

        // Filtrado por lógica central (bandeja -> trapo -> resto)
        return base.CanSelect(interactable) && ImplementosManager.instance.PuedeInsertar(nombre, this);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(ObjetoInsertado);
        selectExited.AddListener(ObjetoRemovido);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        selectEntered.RemoveListener(ObjetoInsertado);
        selectExited.RemoveListener(ObjetoRemovido);
    }

    private void ObjetoInsertado(SelectEnterEventArgs args)
    {
        string nombre = args.interactableObject.transform.name;
        ImplementosManager.instance.ReportarObjetoInsertado(nombre, this);
    }

    private void ObjetoRemovido(SelectExitEventArgs args)
    {
        string nombre = args.interactableObject.transform.name;
        ImplementosManager.instance.ReportarObjetoRemovido(nombre);
    }
}
