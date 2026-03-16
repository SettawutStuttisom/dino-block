using UnityEngine;

public class BlockDrag : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 offset;

    public int shapeID;

    [Header("Audio Objects")]
    public AudioSource correctAudioSource;
    public AudioSource incorrectAudioSource;

    [Header("Feedback Prefabs")]
    public GameObject correctPopup;
    public GameObject wrongPopup;

    public float popupTime = 1f;

    void Start()
    {
        startPos = transform.position;
    }

    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    void OnMouseUp()
    {
        CheckDino();
    }

    void CheckDino()
    {
        Vector3 dropPos = transform.position;

        Collider2D hit = Physics2D.OverlapPoint(dropPos);

        if (hit != null)
        {
            Dino dino = hit.GetComponent<Dino>();

            if (dino != null && dino.correctShapeID == shapeID)
            {
                Debug.Log("Correct block!");

                if (correctAudioSource != null)
                    correctAudioSource.Play();

                ShowPopup(correctPopup, dropPos);

                // Spawn ไข่
                EggSpawnPoint[] spawns = FindObjectsOfType<EggSpawnPoint>(true);

                foreach (EggSpawnPoint spawn in spawns)
                {
                    if (spawn.shapeID == shapeID)
                    {
                        spawn.SpawnEgg();
                        break;
                    }
                }

                // แจ้ง GameManager ไปด่านถัดไป
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.RoundComplete();
                }

                Destroy(gameObject);
                return;
            }
        }

        if (incorrectAudioSource != null)
            incorrectAudioSource.Play();

        ShowPopup(wrongPopup, dropPos);

        transform.position = startPos;
    }

    void ShowPopup(GameObject popup, Vector3 pos)
    {
        if (popup == null) return;

        GameObject obj = Instantiate(popup, pos, Quaternion.identity);
        Destroy(obj, popupTime);
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}