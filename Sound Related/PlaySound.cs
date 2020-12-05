using UnityEngine;

/**
This script uses this object's Audio Source to playOneShot one audioClip.
*/
[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}
