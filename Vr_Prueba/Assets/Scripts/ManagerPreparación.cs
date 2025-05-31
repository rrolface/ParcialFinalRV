using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class ManagerPreparacion : MonoBehaviour
{
    [Header("Sockets")]
    public GameObject socketBandeja;
    public List<GameObject> socketsHerramientas;

    [Header("Herramientas válidas")]
    public List<string> tagsHerramientasValidas;
    public int totalHerramientasEsperadas;

    [Header("Objetos de referencia")]
    public GameObject bandejaGO;

    [Header("UI: Chulitos de progreso")]
    public List<Image> chulitos; // Asigna los 8 chulitos en el Inspector

    [Header("Variables de Manager")]
    public int intentosMalos = 0;
    public int herramientasCorrectasColocadas = 0;
    public bool fasePreparacionCompleta = false;

    private bool bandejaColocada = false;

    void Start()
    {
        // Activar socket bandeja y desactivar sockets herramientas
        socketBandeja.SetActive(true);
        foreach (var socket in socketsHerramientas)
            socket.SetActive(false);

        // Ocultar chulitos al inicio
        foreach (var ch in chulitos)
            ch.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        Debug.Log("Objeto ingresado: " + other.name + " (Tag: " + tag + ")");

        if (other.CompareTag("ObjetoMalo"))
        {
            intentosMalos++;
            Debug.Log("⚠ Objeto incorrecto colocado. Total intentos malos: " + intentosMalos);
            return;
        }

        // Paso: Mascarilla
        if (other.CompareTag("Mascara"))
        {
            MarcarChulito(0);
            return;
        }

        // Paso: Lavado de manos
        if (other.CompareTag("LavadoManos"))
        {
            MarcarChulito(1);
            return;
        }

        // Bandeja colocada: habilitar sockets
        if (other.CompareTag("Bandeja") && !bandejaColocada)
        {
            bandejaColocada = true;
            foreach (var s in socketsHerramientas)
                s.SetActive(true);
            Debug.Log("✅ Bandeja colocada. Sockets de herramientas habilitados.");
            return;
        }

        // Herramientas válidas
        if (tagsHerramientasValidas.Contains(tag))
        {
            herramientasCorrectasColocadas++;
            Debug.Log("✅ Herramienta colocada: " + tag);

            // Mostrar chulito correspondiente (suma 2 por Mascarilla y Lavado)
            int idx = tagsHerramientasValidas.IndexOf(tag) + 2;
            MarcarChulito(idx);
        }

        // Finalizar fase
        if (herramientasCorrectasColocadas >= totalHerramientasEsperadas)
        {
            fasePreparacionCompleta = true;
            Debug.Log("🎉 Fase de preparación completa.");
            DesactivarInteraccionesFinales();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string tag = other.tag;

        if (tag == "Bandeja" && bandejaColocada)
        {
            bandejaColocada = false;
            foreach (var s in socketsHerramientas)
                s.SetActive(false);
            Debug.Log("🔄 Bandeja retirada.");
        }

        if (tagsHerramientasValidas.Contains(tag) && herramientasCorrectasColocadas > 0)
        {
            herramientasCorrectasColocadas--;
            Debug.Log("🔄 Herramienta retirada: " + tag);

            int idx = tagsHerramientasValidas.IndexOf(tag) + 2;
            DesmarcarChulito(idx);
        }
    }

    private void MarcarChulito(int index)
    {
        if (index >= 0 && index < chulitos.Count)
        {
            chulitos[index].enabled = true;
        }
        else
        {
            Debug.LogWarning("Índice de chulito fuera de rango: " + index);
        }
    }

    private void DesmarcarChulito(int index)
    {
        if (index >= 0 && index < chulitos.Count)
        {
            chulitos[index].enabled = false;
        }
    }

    private void DesactivarInteraccionesFinales()
    {
        var grab = bandejaGO.GetComponent<XRGrabInteractable>();
        if (grab != null) grab.enabled = false;

        var rb = bandejaGO.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
}
