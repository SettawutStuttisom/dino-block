using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    private float startTime;
    private float finalTime;
    private bool isTiming = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isTiming)
        {
            finalTime = Time.time - startTime;
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        finalTime = 0;
        isTiming = true;

        Debug.Log("Timer Started");
    }

    public void StopTimer()
    {
        finalTime = Time.time - startTime;
        isTiming = false;

        Debug.Log("Final Time: " + finalTime);
    }

    public float GetTime()
    {
        return finalTime;
    }
}