using UnityEngine;

public class audiomanager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip buttonClick;
    public AudioClip jumpSound;
    public AudioClip collectSound; 
    public AudioClip loseSound;    

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
