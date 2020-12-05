using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/**
This script connects an exposed variable from an Audio Mixer to a UI slider.
*/
public class MixerAudioSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string exposedVariable;
    [SerializeField] private Slider slider;

    private void Start()
    {
        if (slider != null)
        {
            if (mixer.GetFloat(exposedVariable, out var volume))
            {
                slider.value = volume;
            }
        }
    }

    public void ChangeMixerVolume(Slider volume){
        mixer.SetFloat(exposedVariable, volume.value);
    }
}
