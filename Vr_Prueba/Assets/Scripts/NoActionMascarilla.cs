using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NoActionMascarilla : XRSocketInteractor
{
    public string targetTag;
    [SerializeField] private Material newMaterial;  // Nuevo material a aplicar
    [SerializeField] private GameObject objectToChangeMaterial;  // Objeto cuyo material se cambia

    [Header("Variables para Manager")]
    public bool accionRealizada = false;  // Variable que se actualizará cuando se inserte correctamente

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(OnObjectInserted);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        selectEntered.RemoveListener(OnObjectInserted);
    }

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && interactable.transform.tag == targetTag;
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && interactable.transform.tag == targetTag;
    }

    private void OnObjectInserted(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag(targetTag))
        {
            args.interactableObject.transform.gameObject.SetActive(false);

            if (objectToChangeMaterial != null && newMaterial != null)
            {
                var renderer = objectToChangeMaterial.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = newMaterial;
                }
                else
                {
                    Debug.Log("No se encontró Renderer en objectToChangeMaterial");
                }
            }

            // Se realizó correctamente la acción
            accionRealizada = true;
            Debug.Log("guantes Puestos");
        }
    }

}
