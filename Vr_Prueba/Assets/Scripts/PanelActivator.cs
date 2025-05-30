using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelActivator : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public float tiempoParaActivar = 5f; // Tiempo antes de activar paneles y partículas
    public float tiempoActivo = 5f;      // Tiempo que los paneles estarán activos
    public ParticleSystem particulasObjetivo; // Sistema de partículas
    public GameObject dirigete;

    void Start()
    {
        StartCoroutine(ActivarYDesactivarPanelesYParticulas());
    }

    private System.Collections.IEnumerator ActivarYDesactivarPanelesYParticulas()
    {
        // Esperar el tiempo antes de activar
        yield return new WaitForSeconds(tiempoParaActivar);

        // Activar paneles
        if (panel1 != null) panel1.SetActive(true);
        if (panel2 != null) panel2.SetActive(true);

        // Activar partículas (se mantienen)
        if (particulasObjetivo != null && !particulasObjetivo.isPlaying)
            particulasObjetivo.Play();

        // Esperar para desactivar los paneles
        yield return new WaitForSeconds(tiempoActivo);

        // Desactivar paneles
        if (panel1 != null) panel1.SetActive(false);
        if (panel2 != null) panel2.SetActive(false);
        dirigete.SetActive(true);
    }
}
