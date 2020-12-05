using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/**
REQUIRES: DOTWEEN

This script allows changing the Depth of Field post-processing effect properties using DOTween.
*/
public class FocalLensController : MonoBehaviour
{
    [SerializeField] private Volume postProcessVolume;
    [SerializeField] private float startFocalLength = 115.0f;
    [SerializeField] private float changeDepthOfFieldTimer = 1.0f;
    
    private VolumeProfile _volumeProfile;
    private DepthOfField _depthOfField;

    private void Awake()
    {
        _volumeProfile = postProcessVolume.profile;
        _volumeProfile.TryGet(out _depthOfField);
    }

    private void Start()
    {
        _depthOfField.focalLength.value = startFocalLength;
    }

    public void ChangeDepthOfField(float distance)
    {
        DOTween.To(() => _depthOfField.focalLength.value, value => _depthOfField.focalLength.value = value,
            distance, changeDepthOfFieldTimer);
    }
}
