using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerFase1 : MonoBehaviour
{
    [Header("Referencias a scripts")]
    public AgarroUnaVez agarraScript;
    public LavadoManos lavadoManosScript;
    public MascaraPuesta mascaraPuestaScript;
    public NoActionMascarilla accionMascarillaScript;
    public ManagerPreparacion managerPreparacion;

    [Header("Puntaje de la Fase 1")]
    public int puntosFase1 = 50;
    private bool faseEvaluada = false;

    void Update()
    {
        if (!faseEvaluada && managerPreparacion.fasePreparacionCompleta)
        {
            EvaluarFase1();
            faseEvaluada = true;

            // Aquí puedes activar la Fase 2 o enviar evento, etc.
            Debug.Log("✅ Fase 1 finalizada con " + puntosFase1 + " puntos.");
        }
    }

    void EvaluarFase1()
    {
        if (!agarraScript.Agarro)
        {
            puntosFase1 -= 10;
            Debug.Log("❌ No agarró: -10 puntos");
        }

        if (!lavadoManosScript.lavadoManos)
        {
            puntosFase1 -= 10;
            Debug.Log("❌ No se lavó las manos: -10 puntos");
        }

        if (!mascaraPuestaScript.mascaraPuesta)
        {
            puntosFase1 -= 10;
            Debug.Log("❌ No se puso la mascarilla: -10 puntos");
        }

        if (!accionMascarillaScript.accionRealizada)
        {
            puntosFase1 -= 10;
            Debug.Log("❌ No realizó la acción de mascarilla: -10 puntos");
        }

        int penalizacionIntentos = managerPreparacion.intentosMalos * 5;
        puntosFase1 -= penalizacionIntentos;

        if (penalizacionIntentos > 0)
        {
            Debug.Log("❌ Intentos malos: -" + penalizacionIntentos + " puntos (" + managerPreparacion.intentosMalos + " intentos)");
        }

        puntosFase1 = Mathf.Max(0, puntosFase1); // Asegurarse de no tener puntos negativos
    }
}
