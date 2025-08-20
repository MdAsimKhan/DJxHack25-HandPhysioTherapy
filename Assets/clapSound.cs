using UnityEngine;

public class clapSound : MonoBehaviour
{
    public AudioClip clap1;
    public AudioClip clap2;
    public AudioSource AudioSource;
    public int claptrigger = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "leftHand")
        {
            if (claptrigger == 0)
            {
                AudioSource.clip = clap1;
                AudioSource.Play();
                claptrigger = 1;
                Debug.Log("Playing clap1");
            }
            else 
            {
                claptrigger = 0;
                AudioSource.clip = clap2;
                AudioSource.Play();
                Debug.Log("Playing clap2");
            }
        }
    }


}
