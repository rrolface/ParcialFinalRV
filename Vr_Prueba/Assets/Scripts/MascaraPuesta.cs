using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MascaraPuesta : MonoBehaviour
{

    public bool mascaraPuesta = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mascara"))
        {
            mascaraPuesta=true;
            Debug.Log("mascara Puesta");
        }
    }


}
