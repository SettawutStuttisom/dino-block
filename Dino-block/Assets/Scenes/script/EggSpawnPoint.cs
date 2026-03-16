using UnityEngine;

public class EggSpawnPoint : MonoBehaviour
{
    public int shapeID;          // 🔥 ระบุว่า spawn สำหรับบล็อกอะไร
    public GameObject eggPrefab;

    public void SpawnEgg()
    {
        if (eggPrefab == null) return;

        Instantiate(
            eggPrefab,
            transform.position,
            Quaternion.identity
        );
    }
}