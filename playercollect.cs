using UnityEngine;

public class PlayerController : MonoBehaviour 
{

    private AudioSource playerAudioSource;

    public AudioClip fruitCollectSound;



    private void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();

        if (playerAudioSource == null)
        {
            playerAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("fruit"))
        {
            if (playerAudioSource != null && fruitCollectSound != null)
            {
                playerAudioSource.PlayOneShot(fruitCollectSound);
            }

            

            Destroy(collision.gameObject);
        }
    }
}