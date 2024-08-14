using UnityEngine;

public class AudioManager : BaseManager
{
    AudioSource bgAudioSource;
    AudioSource normalAudioSource;

    public AudioManager(GameFacade gameFacade) : base(gameFacade)
    {

    }

    public override void OnInit()
    {
        base.OnInit();
        GameObject audioSource = new GameObject("AudioSource(GameObject)");
        bgAudioSource = audioSource.AddComponent<AudioSource>();
        normalAudioSource = audioSource.AddComponent<AudioSource>();

        PlayBgSound("Sounds/Bg(moderate)");
    }

    /// <summary>
    /// Play audio.
    /// </summary>
    /// <param name="audioSource">AudioSource.</param>
    /// <param name="audioClip">AudioClip.</param>
    /// <param name="volumn">Volumn</param>
    /// <param name="loop">Loop or not.</param>
    void PlaySound(AudioSource audioSource, AudioClip audioClip, float volumn, bool loop=false)
    {
        audioSource.volume = volumn;
        audioSource.clip = audioClip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    /// <summary>
    /// Function with light background music.
    /// </summary>
    /// <param name="soundName">The pathname of sound.</param>
    public void PlayBgSound(string soundName)
    {
        PlaySound(bgAudioSource, LoadSound(soundName), 0.5f, true);
    }

    /// <summary>
    /// Function with normal background music.
    /// </summary>
    /// <param name="soundName">The pathname of sound.</param>
    public void PlayNormalSound(string soundName)
    {
        PlaySound(normalAudioSource, LoadSound(soundName), 1f);
    }

    AudioClip LoadSound(string path)
    {
        return Resources.Load<AudioClip>(path);
    }
}
