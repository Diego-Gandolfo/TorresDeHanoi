using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;

    [Header("Sound FX")]
    [SerializeField] private AudioClip click = null;
    [SerializeField] private AudioClip endgame = null;

    private AudioSource audioSourceSFX = null;

    public void Awake()
    {
        soundManager = this;
        audioSourceSFX = gameObject.GetComponent<AudioSource>();
    }

    public void PlayClick()
    {
        audioSourceSFX.clip = click;
        audioSourceSFX.volume = 1f;
        audioSourceSFX.Play();
    }

    public void PlayEndgame()
    {
        audioSourceSFX.clip = endgame;
        audioSourceSFX.volume = 1f;
        audioSourceSFX.Play();
    }
}
