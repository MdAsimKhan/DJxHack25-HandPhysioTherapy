using UnityEngine;

public class DidIt : MonoBehaviour
{
    public Timer timer;
    public TMPro.TMP_Text msg;
    public AudioClip success;
    public GameObject CompletedUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShowCompletionOptions();
        }
    }

    public void ShowCompletionOptions()
    {
        msg.color = Color.green;
        msg.text = "YOU DID IT!";
        timer.isRunning = false;
        var audioSrc = GameObject.FindFirstObjectByType<AudioSource>();
        audioSrc.volume = 0.5f;
        audioSrc.PlayOneShot(success);
        CompletedUI.SetActive(true);
    }
}
