using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishManager : MonoBehaviour
{
    [Header("Canvas")]
    public Canvas resultCanvas;

    [Header("Video")]
    public VideoPlayer videoPlayer;

    [Header("Overlay")]
    public Image darkOverlay;
    public float fadeDuration = 1.5f;

    [Header("UI")]
    public GameObject finishPanel;
    public Text timeText;
    public Text levelText;
    public Image levelImage;

    [Header("Scene Names")]
    public string restartScene;
    public string menuScene;

    [Header("Level Sprites")]
    public Sprite level1;
    public Sprite level2;
    public Sprite level3;
    public Sprite level4;
    public Sprite level5;

    [Header("Time Threshold (Seconds)")]
    public float level5Time = 10f;
    public float level4Time = 15f;
    public float level3Time = 20f;
    public float level2Time = 30f;
    public float level1Time = 30f;

    void Start()
    {
        // ซ่อน Canvas ก่อน
        if (resultCanvas != null)
            resultCanvas.enabled = false;

        if (finishPanel != null)
            finishPanel.SetActive(false);

        if (darkOverlay != null)
            darkOverlay.color = new Color(0,0,0,0);

        if (videoPlayer != null)
            videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        StartCoroutine(FinishSequence());
    }

    IEnumerator FinishSequence()
    {
        yield return new WaitForSeconds(0.5f);

        float t = 0;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            float alpha = Mathf.Lerp(0, 0.6f, t / fadeDuration);

            if (darkOverlay != null)
                darkOverlay.color = new Color(0,0,0,alpha);

            yield return null;
        }

        if (resultCanvas != null)
            resultCanvas.enabled = true;

        ShowResult();
    }

    void ShowResult()
    {
        float timeUsed = 0f;

        // ⭐ อ่านเวลาจาก TimerManager
        if (TimerManager.Instance != null)
            timeUsed = TimerManager.Instance.GetTime();

        if (finishPanel != null)
            finishPanel.SetActive(true);

        if (timeText != null)
            timeText.text = "Time : " + timeUsed.ToString("F2") + " sec";

        SetLevel(timeUsed);
    }

    void SetLevel(float time)
    {
        if (time <= level5Time)
        {
            levelText.text = "คุณมีความเร็วเหมือนเสือชีต้าห์!";
            levelImage.sprite = level5;
        }
        else if (time <= level4Time)
        {
            levelText.text = "คุณมีความเร็วเหมือนกระต่าย!";
            levelImage.sprite = level4;
        }
        else if (time <= level3Time)
        {
            levelText.text = "คุณมีความเร็วเหมือนคน!";
            levelImage.sprite = level3;
        }
        else if (time <= level2Time)
        {
            levelText.text = "คุณมีความเร็วเหมือนเต่า!";
            levelImage.sprite = level2;
        }
        else
        {
            levelText.text = "คุณมีความเร็วเหมือนหอยทาก!";
            levelImage.sprite = level1;
        }

        ResizeImageToFit();
    }

    void ResizeImageToFit()
    {
        if (levelImage == null || levelImage.sprite == null) return;

        levelImage.SetNativeSize();

        float maxWidth = 400f;
        float maxHeight = 400f;

        RectTransform rt = levelImage.GetComponent<RectTransform>();

        float widthRatio = maxWidth / rt.sizeDelta.x;
        float heightRatio = maxHeight / rt.sizeDelta.y;

        float scale = Mathf.Min(widthRatio, heightRatio, 1f);

        rt.sizeDelta = new Vector2(
            rt.sizeDelta.x * scale,
            rt.sizeDelta.y * scale
        );
    }

    // ปุ่มเล่นใหม่
    public void RestartGame()
    {
        if (!string.IsNullOrEmpty(restartScene))
            SceneManager.LoadScene(restartScene);
    }

    // ปุ่มกลับเมนู
    public void GoToMenu()
    {
        if (!string.IsNullOrEmpty(menuScene))
            SceneManager.LoadScene(menuScene);
    }
}