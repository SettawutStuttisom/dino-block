using UnityEngine;

public class GlobalPauseManager : MonoBehaviour
{
    public GameObject pausePanel;

    public GameObject sign1; // มีหรือไม่มีก็ได้
    public GameObject sign2; // มีหรือไม่มีก็ได้

    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    public void PauseGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);

        // ถ้ามี sign ก็ซ่อน
        if (sign1 != null)
            sign1.SetActive(false);

        if (sign2 != null)
            sign2.SetActive(false);

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        // ถ้ามี sign ก็แสดง
        if (sign1 != null)
            sign1.SetActive(true);

        if (sign2 != null)
            sign2.SetActive(true);

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}