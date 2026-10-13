using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 100; // coin value; change this depending on how many coins/time limit
    public AudioClip coinSound; // sound to play on collect

    // Destroy() is deferred until after the current Update loop, so a second
    // trigger contact in the same frame would otherwise award the coin twice.
    private bool collected;

    // called when collider enters this trigger
    private void OnTriggerEnter(Collider other)
    {
        // check if object has player tag
        if (!collected && other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    void Collect(GameObject player)
    {
        collected = true;
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(coinValue);
        }
        else
        {
            Debug.LogError("ScoreManager instance not found");
        }

        // play sound
        if (coinSound != null)
        {
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
        }

        // destroy object
        Destroy(gameObject);

    }
}