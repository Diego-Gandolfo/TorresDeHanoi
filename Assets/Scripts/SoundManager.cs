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

    public void PlaySound(string clip, float volumen)
    {
        switch (clip)
        {
            case "Click":
                audioSourceSFX.clip = click;
                audioSourceSFX.volume = volumen;
                audioSourceSFX.Play();
                break;
            case "Endgame":
                audioSourceSFX.clip = endgame;
                audioSourceSFX.volume = volumen;
                audioSourceSFX.Play();
                break;
        }
    }
}
