using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ManajerFase2 : MonoBehaviour
{
    [Header("Referencias")]
    public Animator doctorAnimator; // Animaciones: Idle, Writing
    public List<GameObject> socketsHerramientas; // Sockets en orden
    public List<string> ordenHerramientas; // Tags correctos por orden
    public int puntosFase2 = 50;

    private int herramientaActualIndex = 0;
    private bool fase2Completada = false;
    private bool animacionEnCurso = false;

    public GameObject PanelResumen;
    public GameObject PanelFase2;
    public GameObject PanelHistorialMedico;

    [Header("Dependencia de Fase 1")]
    public ManagerFase1 managerFase1;

    public TextMeshProUGUI PointsFase2;

    void Start()
    {
        doctorAnimator.Play("Breathing Idle");
        DesactivarTodosLosSockets();
    }

    void Update()
    {
        if (managerFase1.faseEvaluada && !fase2Completada)
        {
            ActivarSocketActual();
        }
    }

    void DesactivarTodosLosSockets()
    {
        foreach (var socket in socketsHerramientas)
            socket.SetActive(false);
    }

    void ActivarSocketActual()
    {
        if (herramientaActualIndex < socketsHerramientas.Count)
            socketsHerramientas[herramientaActualIndex].SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (fase2Completada || animacionEnCurso) return;

        string herramientaTag = other.tag;
        string herramientaEsperada = ordenHerramientas[herramientaActualIndex];

        if (herramientaTag == herramientaEsperada)
        {
            Debug.Log("✅ Herramienta correcta detectada: " + herramientaTag);
            StartCoroutine(ProcesoHerramientaCorrecta(other.gameObject));
        }
        else
        {
            puntosFase2 -= 5;
            Debug.Log("❌ Herramienta incorrecta. Puntos actuales: " + puntosFase2);
        }
    }

    IEnumerator ProcesoHerramientaCorrecta(GameObject herramienta)
    {
        animacionEnCurso = true;

        doctorAnimator.Play("Writing"); // Animación de operación
        yield return new WaitForSeconds(4f);

        // Destruir la herramienta entregada
        Destroy(herramienta);

        // Desactivar socket actual
        socketsHerramientas[herramientaActualIndex].SetActive(false);
        herramientaActualIndex++;

        if (herramientaActualIndex >= ordenHerramientas.Count)
        {
            fase2Completada = true;
            doctorAnimator.Play("Breathing Idle");
            Debug.Log("🎉 Fase 2 completada. Puntos finales: " + puntosFase2);
            PointsFase2.text = "" + puntosFase2 + "de 50";
            PanelResumen.SetActive(true);
            PanelFase2.SetActive(false);
            PanelHistorialMedico.SetActive(false); 
        }
        else
        {
            doctorAnimator.Play("Breathing Idle");
            ActivarSocketActual();
        }

        animacionEnCurso = false;
    }
}
