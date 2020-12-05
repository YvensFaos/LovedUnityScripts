using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

/**
This script changes the Animator Controler of one object's Animator.
*/
public class ChangeAnimator : MonoBehaviour
{
    [SerializeField] private Animator objectWithAnimator;
    [SerializeField] private AnimatorController replaceWithAnimatorController;

    public void ReplaceAnimator()
    {
        objectWithAnimator.runtimeAnimatorController = replaceWithAnimatorController;
    }
}
