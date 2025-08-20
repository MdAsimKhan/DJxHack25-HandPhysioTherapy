using UnityEngine;

public class PaintingTouchHandler : MonoBehaviour
{
    private PaintingGame paintingGame;

    void Start()
    {
        paintingGame = FindFirstObjectByType<PaintingGame>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Painting"))
        {
            other.transform.rotation = Quaternion.identity;
            other.enabled = false;
            var audio = other.gameObject.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Play();
            }
            if (paintingGame != null)
            {
                paintingGame.PaintingTouched();
            }
        }
    }
}