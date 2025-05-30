using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MascaraPuesta : MonoBehaviour
{

    public bool mascaraPuesta = false;
    public AudioSource audiosource;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mascara"))
        {
            if(!mascaraPuesta)
            {
                mascaraPuesta = true;
                Debug.Log("mascara Puesta");

                if(audiosource != null  && !audiosource.isPlaying)
                {
                    audiosource.Play();
                }
            }
        
        }
    }


}
