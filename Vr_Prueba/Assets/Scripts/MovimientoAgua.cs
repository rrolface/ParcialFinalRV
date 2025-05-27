using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class MovimientoAgua : MonoBehaviour
{

    public float velY, velX;
    Renderer render;

    private void Start()
    {
        render = GetComponent<Renderer>();
    }

    private void Update()
    {
        render.material.SetTextureOffset("_MainTex", new Vector2(velX, velY) * Time.time);
    }


}
