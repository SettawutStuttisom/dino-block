using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Dino Prefabs")]
    public GameObject[] dinoPrefabs;

    private Transform spawnPoint;
    private ThoughtBubbleSpawner bubbleSpawner;
    private GameObject currentDino;

    [Header("Game Round Settings")]
    public int maxDinoCount = 10;
    private int dinoClearedCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // =========================
    // Scene Loaded
    // =========================
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StartGame")
        {
            spawnPoint = GameObject.Find("SpawnPoint")?.transform;
            bubbleSpawner = FindObjectOfType<ThoughtBubbleSpawner>();

            ResetGame();

            SpawnNextDino();
        }
    }

    // =========================
    // Spawn Dino
    // =========================
    void SpawnNextDino()
    {
        // ⭐ เริ่มจับเวลาเมื่อ Dino ตัวแรกเกิด
        if (dinoClearedCount == 0 && TimerManager.Instance != null)
        {
            TimerManager.Instance.StartTimer();
        }

        if (spawnPoint == null)
        {
            Debug.LogError("SpawnPoint not found!");
            return;
        }

        if (dinoPrefabs == null || dinoPrefabs.Length == 0)
        {
            Debug.LogError("No Dino Prefabs assigned!");
            return;
        }

        if (currentDino != null)
        {
            Destroy(currentDino);
        }

        int index = Random.Range(0, dinoPrefabs.Length);
        GameObject prefab = dinoPrefabs[index];

        currentDino = Instantiate(
            prefab,
            spawnPoint.position,
            Quaternion.identity
        );

        Dino dinoScript = currentDino.GetComponent<Dino>();

        if (dinoScript != null && bubbleSpawner != null)
        {
            bubbleSpawner.SpawnBubble(dinoScript.correctShapeID);
        }
    }

    // =========================
    // ตอบถูก
    // =========================
    public void RoundComplete()
    {
        dinoClearedCount++;

        if (currentDino != null)
        {
            Destroy(currentDino);
        }

        if (bubbleSpawner != null)
        {
            bubbleSpawner.ClearBubble();
        }

        if (dinoClearedCount >= maxDinoCount)
        {
            // ⭐ หยุดเวลา
            if (TimerManager.Instance != null)
            {
                TimerManager.Instance.StopTimer();
            }

            SceneManager.LoadScene("Finish");
            return;
        }

        SpawnNextDino();
    }

    // =========================
    // Reset Game
    // =========================
    public void ResetGame()
    {
        dinoClearedCount = 0;
    }
}