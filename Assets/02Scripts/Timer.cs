using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    float curTime = 0;
    int seconds = 0;
    int minutes = 0;
    public bool isRunning = true;

    void Update()
    {
        if (isRunning)
        {
            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        curTime += Time.deltaTime;
        seconds = (int)(curTime % 60);
        minutes = (int)(curTime / 60) % 60;
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
