using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    Animation characterAnimation;
    private float crouchDuration = 1.0f;

    private void Start()
    {
        characterAnimation = transform.GetChild(0).GetComponent<Animation>();
    }

    public void Crouch()
    {
        StartCoroutine(PlayCrouchAnimationWithDuration(crouchDuration));
    }

    private IEnumerator PlayCrouchAnimationWithDuration(float duration)
    {
        characterAnimation.Play("CrouchCenterEnter");
        yield return new WaitForSeconds(duration);
        characterAnimation.Play("CrouchCenterExit");
        yield break;
    }
}
