using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ManagerPreparación : MonoBehaviour
{
    [Header("Sockets")]
    public GameObject socketBandeja;
    public GameObject socketTrapo;
    public GameObject socketTijeraQuirirjica;
    public GameObject socketPinzaAnatomica;
    public GameObject SocketPinzaHemostáticacurva;
    public GameObject SocketPinzaHemostáticaRecta;
    public GameObject SocketJeringa;
    public GameObject SocketBisturi;

    [Header("Herramientas válidas")]
    public List<string> tagsHerramientasValidas;
    public int totalHerramientasEsperadas;

    [Header("Objetos de referencia")]
    public GameObject bandejaGO;
    public GameObject trapoGO;

    [Header("Variables para el Manager")]
    public int intentosMalos = 0;
    public int herramientasCorrectasColocadas = 0;
    public bool fasePreparacionCompleta = false;

    private bool bandejaColocada = false;
    private bool trapoColocado = false;

    void Start()
    {
        socketBandeja.SetActive(true);
        socketTrapo.SetActive(false);
        socketTijeraQuirirjica.SetActive(false);
        socketPinzaAnatomica.SetActive(false);
        SocketPinzaHemostáticacurva.SetActive(false);
        SocketPinzaHemostáticaRecta.SetActive(false);
        SocketJeringa.SetActive(false);
        SocketBisturi.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        Debug.Log("Objeto ingresado: " + other.name + " (Tag: " + tag + ")");

        if (tag == "ObjetoMalo")
        {
            intentosMalos++;
            Debug.Log("⚠️ Objeto incorrecto colocado. Total intentos malos: " + intentosMalos);
            return;
        }

        if (tag == "Bandeja")
        {
            if (!bandejaColocada)
            {
                bandejaColocada = true;
                socketTrapo.SetActive(true);
                Debug.Log("✅ Bandeja colocada. Socket del trapo habilitado.");
            }
            return;
        }

        if (tag == "Trapo")
        {
            if (!trapoColocado && bandejaColocada)
            {
                trapoColocado = true;
                Debug.Log("✅ Trapo colocado. Sockets de herramientas habilitados.");

                // Habilitar sockets de herramientas
                socketTijeraQuirirjica.SetActive(true);
                socketPinzaAnatomica.SetActive(true);
                SocketPinzaHemostáticacurva.SetActive(true);
                SocketPinzaHemostáticaRecta.SetActive(true);
                SocketJeringa.SetActive(true);
                SocketBisturi.SetActive(true);

                DesactivarInteraccionesFinales(); // Bloquear bandeja y trapo
            }
            else if (!bandejaColocada)
            {
                Debug.Log("⚠️ Trapo colocado antes de la bandeja. Acción ignorada.");
            }
            return;
        }

        // Si es una herramienta válida
        if (tagsHerramientasValidas.Contains(tag))
        {
            herramientasCorrectasColocadas++;
            Debug.Log("✅ Herramienta colocada: " + tag);
        }

        // Verificar si ya están todas las herramientas colocadas
        if (herramientasCorrectasColocadas >= totalHerramientasEsperadas)
        {
            fasePreparacionCompleta = true;
            Debug.Log("🎉 Fase de preparación completa.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string tag = other.tag;

        if (tag == "Bandeja" && bandejaColocada)
        {
            if (!bandejaColocada)
            {
                bandejaColocada = true;

                if (socketTrapo != null)
                {
                    socketTrapo.SetActive(true);
                    Debug.Log("✅ Bandeja colocada correctamente. Socket del trapo habilitado.");
                }
                else
                {
                    Debug.LogWarning("⚠️ socketTrapo no está asignado en el inspector.");
                }
            }
            return;
        }

        if (tag == "Trapo" && trapoColocado)
        {
            trapoColocado = false;
            Debug.Log("🔄 Trapo retirado.");
            return;
        }

        if (tagsHerramientasValidas.Contains(tag))
        {
            if (herramientasCorrectasColocadas > 0)
                herramientasCorrectasColocadas--;

            Debug.Log("🔄 Herramienta retirada: " + tag);
        }
    }

    private void DesactivarInteraccionesFinales()
    {
        var grab1 = bandejaGO.GetComponent<XRGrabInteractable>();
        var grab2 = trapoGO.GetComponent<XRGrabInteractable>();

        if (grab1 != null) grab1.enabled = false;
        if (grab2 != null) grab2.enabled = false;

        Debug.Log("🔒 Bandeja y trapo bloqueados para evitar su manipulación.");
    }

}
