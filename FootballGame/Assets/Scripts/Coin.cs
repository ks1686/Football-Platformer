using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 100; // coin value; change this depending on how many coins/time limit
    public AudioClip coinSound; // sound to play on collect

    // called when collider enters this trigger
    private void OnTriggerEnter(Collider other)
    {
        // check if object has player tag
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    void Collect(GameObject player)
    {
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