using UnityEngine;


public class Collided : MonoBehaviour
{
    public Transform startPoint;
    public GameObject parent;
    public TMPro.TMP_Text msg;
    public AudioClip fail;
    public GameObject gameOverUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wire"))
        {
            msg.color = Color.red;
            msg.text = "RETRY";
            gameOverUI.SetActive(true);
            parent.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = false;
            parent.transform.position = startPoint.position;
            parent.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = true;
            var audioSrc = GameObject.FindFirstObjectByType<AudioSource>();
            audioSrc.volume = 0.5f;
            audioSrc.PlayOneShot(fail);
        }
    }
}
