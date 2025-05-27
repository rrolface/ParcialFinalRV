using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ImplementosManager : MonoBehaviour
{
    public static ImplementosManager instance;

    [Header("Referencias")]
    public GameObject bandejaSocket;
    public GameObject trapoSocket;
    public List<CustomSocket> otrosSockets;

    [Header("Configuración de objetos")]
    public List<string> objetosBuenos;
    public List<string> objetosMalos;

    private bool bandejaColocada = false;
    private bool trapoColocado = false;
    private HashSet<string> objetosCorrectosColocados = new();
    private int totalObjetosBuenos = 10;
    private bool objetoMaloFueColocado = false;

    private void Awake()
    {
        instance = this;
    }

    public bool PuedeInsertar(string nombreObjeto, CustomSocket socket)
    {
        if (!bandejaColocada)
        {
            return socket == bandejaSocket.GetComponent<CustomSocket>() && nombreObjeto == "Bandeja";
        }

        if (!trapoColocado)
        {
            return socket == trapoSocket.GetComponent<CustomSocket>() && nombreObjeto == "Trapo";
        }

        // Solo se permiten los demás después del trapo
        return otrosSockets.Contains(socket);
    }

    public void ReportarObjetoInsertado(string nombreObjeto, CustomSocket socket)
    {
        if (nombreObjeto == "Bandeja")
        {
            bandejaColocada = true;
            return;
        }

        if (nombreObjeto == "Trapo")
        {
            trapoColocado = true;
            return;
        }

        if (objetosBuenos.Contains(nombreObjeto))
        {
            objetosCorrectosColocados.Add(nombreObjeto);
        }
        else if (objetosMalos.Contains(nombreObjeto))
        {
            objetoMaloFueColocado = true;
        }

        VerificarFinalizacion();
    }

    public void ReportarObjetoRemovido(string nombreObjeto)
    {
        objetosCorrectosColocados.Remove(nombreObjeto);
    }

    private void VerificarFinalizacion()
    {
        if (objetosCorrectosColocados.Count >= totalObjetosBuenos)
        {
            int puntaje = 20;
            if (objetoMaloFueColocado)
                puntaje -= 2;

            Debug.Log("¡Tarea completada! Puntaje: " + puntaje);
            // Desactivar la bandeja y el trapo para que no puedan agarrarse
            bandejaSocket.GetComponent<XRSocketInteractor>().enabled = false;
            trapoSocket.GetComponent<XRSocketInteractor>().enabled = false;
        }
    }
}
