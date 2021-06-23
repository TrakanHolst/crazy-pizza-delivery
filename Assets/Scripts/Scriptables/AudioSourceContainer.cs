using UnityEngine;

[CreateAssetMenu(fileName = "AudioSourceContainer", menuName = "ScriptableObjects/AudioSourceContainer", order = 1)]
public class AudioSourceContainer : ScriptableObject
{
    [SerializeField]
    public AudioSource Source {
        get { return source; }
        set { source = value; }
    }
    public AudioSource source;

    public void Play(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void PlayLoop(AudioClip clip)
    {
        source.clip = clip;
        source.loop = true;
        source.Play();
    }
}
