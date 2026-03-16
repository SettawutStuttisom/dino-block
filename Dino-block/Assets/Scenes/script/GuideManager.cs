using UnityEngine;
using UnityEngine.SceneManagement;

public class GuideManager : MonoBehaviour
{
    public float waitTime = 10f;   // เวลาที่รอก่อนข้ามฉาก
    private bool isSkipped = false;

    void Start()
    {
        Invoke("GoToStartGame", waitTime);
    }

    public void SkipGuide()
    {
        if (!isSkipped)
        {
            isSkipped = true;
            CancelInvoke("GoToStartGame");
            GoToStartGame();
        }
    }

    void GoToStartGame()
    {
        SceneManager.LoadScene("StartGame");
    }
}