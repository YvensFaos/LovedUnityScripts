using DG.Tweening;
using UnityEngine;

/**
REQUIRES: DOTWEEN

This script animated one Material property using DOTWeen.
*/
public class AnimateMaterial : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private Material material;
    [SerializeField] private string materialProperty;
    [SerializeField] private float timeToAnimate;

    private void Awake()
    {
        material = renderer.material;
    }

    public void PerformMaterialAnimation(float valueToAchieve)
    {
        Tween tween = DOTween.To(() => material.GetFloat(materialProperty), val => { material.SetFloat(materialProperty, val); }, valueToAchieve, timeToAnimate);
        tween.Play();
    }

}
