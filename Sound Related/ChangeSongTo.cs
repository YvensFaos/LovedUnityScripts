using UnityEngine;

/**
This script changes the music from this object's Audio Source to another.
*/
[RequireComponent(typeof(AudioSource))]
public class ChangeSongTo : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private AudioClip baseSong;
    [SerializeField] private AudioClip combatSong;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ChangeToCombatSong()
    {
        PlaySong(combatSong);
    }
    
    public void ChangeToBaseSong()
    {
        PlaySong(baseSong);
    }

    private void PlaySong(AudioClip song)
    {
        _audioSource.clip = song;
        _audioSource.Stop();
        _audioSource.Play();
    }
}
