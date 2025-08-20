using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaintingGame : MonoBehaviour
{
    [Header("References")]
    public GameObject[] paintingPrefabs;
    public GameObject wall;
    public GameObject ComepletedUI;
    public GameObject TimeUpUI;
    public TMP_Text[] scoreText; // Assign in Inspector
    public TMP_Text timerText; // Assign in Inspector

    [Header("Settings")]
    public int paintingCount = 5;
    public float paintingWidth = 1f;
    public float paintingHeight = 1f;
    public int maxPlacementAttempts = 50;
    public float gameTime = 60f; // seconds

    private int score = 0;
    private float timer;
    private int paintingsTouched = 0;
    private bool gameActive = true;

    void Start()
    {
        timer = gameTime;
        score = 0;
        paintingsTouched = 0;
        gameActive = true;
        UpdateScoreUI();
        UpdateTimerUI();
        PlacePaintingsOnWall();
    }

    void Update()
    {
        if (!gameActive) return;

        timer -= Time.deltaTime;
        UpdateTimerUI();

        if (timer <= 0f)
        {
            timer = 0f;
            GameOver(false);
        }
    }

    void PlacePaintingsOnWall()
    {
        Vector3 wallCenter = wall.transform.position;
        Vector3 wallScale = wall.transform.localScale;
        float width = 10f * wallScale.x;
        float height = 10f * wallScale.z;

        float minX = wallCenter.x - width / 2f;
        float maxX = wallCenter.x + width / 2f;
        float minY = wallCenter.y - height / 4f;
        float maxY = wallCenter.y + height / 4f;

        Vector3 wallNormal = wall.transform.forward;
        float offset = 0.01f;

        List<Rect> placedRects = new List<Rect>();

        for (int i = 0; i < paintingCount; i++)
        {
            bool placed = false;
            for (int attempt = 0; attempt < maxPlacementAttempts && !placed; attempt++)
            {
                float x = Random.Range(minX + paintingWidth / 2f, maxX - paintingWidth / 2f);
                float y = Random.Range(minY + paintingHeight / 2f, maxY - paintingHeight / 2f);

                Rect newRect = new Rect(
                    x - paintingWidth / 2f,
                    y - paintingHeight / 2f,
                    paintingWidth,
                    paintingHeight
                );

                bool overlaps = false;
                foreach (var rect in placedRects)
                {
                    if (rect.Overlaps(newRect))
                    {
                        overlaps = true;
                        break;
                    }
                }

                if (!overlaps)
                {
                    Vector3 position = new Vector3(x, y, wallCenter.z) + wallNormal * offset;
                    Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(-50f, 50f));
                    int prefabIndex = Random.Range(0, paintingPrefabs.Length);
                    GameObject prefab = paintingPrefabs[prefabIndex];

                    GameObject painting = Instantiate(prefab, position, rotation);
                    painting.tag = "Painting"; // Ensure tag is set
                    placedRects.Add(newRect);
                    placed = true;
                }
            }
        }
    }

    public void PaintingTouched()
    {
        if (!gameActive) return;

        score += 10;
        paintingsTouched++;
        UpdateScoreUI();

        if (paintingsTouched >= paintingCount)
        {
            GameOver(true);
        }
    }

    void GameOver(bool completed)
    {
        gameActive = false;
        if (completed)
        {
            ComepletedUI.SetActive(true);
        }
        else
        {
            TimeUpUI.SetActive(true);
        }
    }

    void UpdateScoreUI()
    {
        //if (scoreText != null)
            //scoreText.text = $"Score: {score}";
        foreach (var text in scoreText)
            {
            if (text != null)
                text.text = $"{score}";
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = $"{Mathf.CeilToInt(timer)}";
    }
}