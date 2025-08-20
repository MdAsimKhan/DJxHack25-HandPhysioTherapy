using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject flyPrefab;          // Assign your fly prefab here
    public float constantSeconds = 10f;   // Fly lifetime timeout in seconds
    public FlyWander flyscript;
    public GameObject currentFly;
    public TMP_Text score;
    public float totalTime = 30f;        // Total countdown time in seconds
    private float timeRemaining;
    private bool timerRunning = false;
    private bool isGameOver = false;
    public GameObject wellDonePanel;
    public TMP_Text time;
    public TMP_Text finalScore;

    private void Start()
    {
        timeRemaining = totalTime;
        timerRunning = true;
        UpdateTimeUI();
        //SpawnFly();
        //SpawnFly();
    }

    private void Update()
    {
        if (!timerRunning)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            timerRunning = false;
            OnGameOver();
        }

        UpdateTimeUI();

        // If no fly exists or fly is inactive, spawn a new one immediately
        if ( !isGameOver &&(currentFly == null || !currentFly.activeInHierarchy))
        {
            SpawnFly();
        }
    }
    private void UpdateTimeUI()
    {
        // Display time as seconds with 1 decimal place (change format as needed)
        time.text = timeRemaining.ToString("F0");
    }

    private void OnGameOver()
    {
        Debug.Log("Game Over!");
        isGameOver = true;
        finalScore.text = score.text;
        // Show Game Over UI panel
        if (wellDonePanel != null)
        {
            wellDonePanel.SetActive(true);
        }
        FlyWander[] flies = Object.FindObjectsByType<FlyWander>(FindObjectsSortMode.None);

        foreach (FlyWander fly in flies)
        {
            // Call Die() to handle proper shutdown (buzz stop, disable collider, deactivate)
            fly.transform.parent.gameObject.SetActive(false);
        }

        // Optionally: stop game actions, disable player input, etc.

        // Freeze time scale if you want to pause the entire game (optional)
        // Time.timeScale = 0f;
    }


    private void SpawnFly()
    {
        Vector3 center = flyscript.playerHead.position + flyscript.offsetFromPlayerHead + flyscript.playerHead.forward * (flyscript.wanderRadius * 0.6f);
        center.y = flyscript.playerHead.position.y + flyscript.offsetFromPlayerHead.y + flyscript.flyHeight - 1.6f;

        Vector3 randomPosInSphere = center + Random.insideUnitSphere * flyscript.wanderRadius;

        currentFly = Instantiate(flyPrefab, randomPosInSphere, Quaternion.identity);    


    }
}
