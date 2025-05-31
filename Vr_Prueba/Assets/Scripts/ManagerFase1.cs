using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public bool faseEvaluada = false;

    public GameObject PanelAparecer;
    public GameObject PanelOcultar;

    public TextMeshProUGUI PointsFase1;
    public TextMeshProUGUI mal1;
    public TextMeshProUGUI mal2;
    public TextMeshProUGUI mal3;
    public TextMeshProUGUI mal4;
    public TextMeshProUGUI mal5;

    void Update()
    {
        if (!faseEvaluada && managerPreparacion.fasePreparacionCompleta)
        {
            EvaluarFase1();
            faseEvaluada = true;
            PanelAparecer.SetActive(true);
            PanelOcultar.SetActive(false);

            // Aquí puedes activar la Fase 2 o enviar evento, etc.
            Debug.Log("✅ Fase 1 finalizada con " + puntosFase1 + " puntos.");
            PointsFase1.text = "" + puntosFase1 + "de 50";
        }

        void EvaluarFase1()
        {
            if (!agarraScript.Agarro)
            {
                puntosFase1 -= 10;
                Debug.Log("❌ No agarró: -10 puntos");
                mal1.text = "❌ No agarró tabla: -10 puntos";
            }

            if (!lavadoManosScript.lavadoManos)
            {
                puntosFase1 -= 10;
                Debug.Log("❌ No se lavó las manos: -10 puntos");
                mal2.text = "❌ No se lavó las manos: -10 puntos";
            }

            if (!mascaraPuestaScript.mascaraPuesta)
            {
                puntosFase1 -= 10;
                mal3.text=("❌ No se puso la mascarilla: -10 puntos");

            }

            if (!accionMascarillaScript.accionRealizada)
            {
                puntosFase1 -= 10;
                 mal4.text = ("❌ No se puso guantes: -10 puntos");
            }

            int penalizacionIntentos = managerPreparacion.intentosMalos * 5;
            puntosFase1 -= penalizacionIntentos;

            if (penalizacionIntentos > 0)
            {
                Debug.Log("❌ Intentos malos: -" + penalizacionIntentos + " puntos (" + managerPreparacion.intentosMalos + " intentos)");
                mal5.text = "❌ Herramientas malas:" + managerPreparacion.intentosMalos + "\n" + penalizacionIntentos + "puntos";
            }

            puntosFase1 = Mathf.Max(0, puntosFase1); // Asegurarse de no tener puntos negativos
        }
    }
}