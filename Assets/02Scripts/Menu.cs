using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public InputAction menuBtn;
    public GameObject pauseUI;

    private void Awake()
    {
        menuBtn.Enable();
    }

    private void OnEnable()
    {
        menuBtn.performed += ctx => ShowPauseUI();
    }

    public void ShowPauseUI()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel(int ind)
    {
        SceneManager.LoadScene(ind);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
