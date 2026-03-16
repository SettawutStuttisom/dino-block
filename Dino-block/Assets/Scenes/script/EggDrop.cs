using UnityEngine;

public class EggDrop : MonoBehaviour
{
    public GameObject[] blockPrefabs;

    [Header("Audio Settings")]
    public AudioClip hitSound;

    private bool hasBroken = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasBroken) return;

        if (collision.gameObject.CompareTag("Nest"))
        {
            hasBroken = true;

            if (hitSound != null)
            {
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
            }

            if (blockPrefabs.Length > 0)
            {
                int randomIndex = Random.Range(0, blockPrefabs.Length);

                Instantiate(
                    blockPrefabs[randomIndex],
                    transform.position,
                    Quaternion.identity
                );
            }

            Destroy(gameObject);
        }
    }
}