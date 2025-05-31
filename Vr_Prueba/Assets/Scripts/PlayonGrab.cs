using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(AudioSource))]
public class PlayOnGrab : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private AudioSource audioSource;

    [Tooltip("Clip que se reproducirá al coger el objeto")]
    public AudioClip grabClip;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        audioSource = GetComponent<AudioSource>();

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (grabClip != null)
        {
            audioSource.clip = grabClip;
            audioSource.loop = false; // Asegúrate que no haga loop
            audioSource.Play();
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop(); // Detener el sonido inmediatamente
        }
    }
}
