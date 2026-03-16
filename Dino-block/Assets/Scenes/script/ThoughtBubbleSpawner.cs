using UnityEngine;

public class ThoughtBubbleSpawner : MonoBehaviour
{
    public GameObject thoughtBubblePrefab;

    public GameObject circleBlack;
    public GameObject squareBlack;
    public GameObject triangleBlack;

    // 🔵 เพิ่มตัวปรับตำแหน่ง
    public Vector3 shapeOffset = new Vector3(0f, 0f, 0f);

    // 🔵 เพิ่มตัวปรับขนาด
    public Vector3 shapeScale = new Vector3(1f, 1f, 1f);

    private GameObject currentBubble;

    public void SpawnBubble(int shapeID)
    {
        ClearBubble();

        currentBubble = Instantiate(
            thoughtBubblePrefab,
            transform.position,
            Quaternion.identity
        );

        GameObject shapeToSpawn = null;

        if (shapeID == 1)
            shapeToSpawn = circleBlack;
        else if (shapeID == 2)
            shapeToSpawn = triangleBlack;
        else if (shapeID == 3)
            shapeToSpawn = squareBlack;

        if (shapeToSpawn != null)
        {
            GameObject shape = Instantiate(
                shapeToSpawn,
                currentBubble.transform
            );

            // ✅ ปรับตำแหน่งภายในลูกโป่ง
            shape.transform.localPosition = shapeOffset;

            // ✅ ปรับขนาด
            shape.transform.localScale = shapeScale;
        }
    }

    public void ClearBubble()
    {
        if (currentBubble != null)
            Destroy(currentBubble);
    }
}