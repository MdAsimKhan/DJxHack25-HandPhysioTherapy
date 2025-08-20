using UnityEngine;
using UnityEngine.UI;

public class colliderCheck : MonoBehaviour
{
    public bool rightHandin = false;
    public bool leftHandin = false;
    public bool dieFly = false;
    public GameObject fly;
    private GameManager manager;
    public AudioSource splat;

    private void Start()
    {
        manager = Object.FindAnyObjectByType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.tag);
        if (other.gameObject.tag == "leftHand")
        {
            leftHandin = true;
        }
        if (other.gameObject.tag == "rightHand")
        {
            rightHandin = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "leftHand")
        {
            leftHandin = false;
        }
        if (other.gameObject.tag == "rightHand")
        {
            rightHandin = false;
        }
    }

    private void Update()
    {
        if (rightHandin && leftHandin)
        {
            dieFly = true;
            splat.Play();
            //if (Score != null)
            //{
            //    Score.text += 1;
            //}
            int currentScore = int.Parse(manager.score.text);

            // Add 1 to the score
            currentScore += 10;

            // Update the text with the new score
            manager.score.text = currentScore.ToString();
            fly.SetActive(false);
        }
    }

}