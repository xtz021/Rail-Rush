using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private const string ANIMATOR_TRIGGER_JUMPCENTER = "JumpCenter";
    private const string ANIMATOR_TRIGGER_JUMPRIGHT = "JumpRight";
    private const string ANIMATOR_TRIGGER_JUMPLEFT = "JumpLeft";
    private const string ANIMATOR_TRIGGER_CROUCH = "CrouchCenter";

    Animation characterAnimation;
    Animator animator;
    private float crouchDuration = 1.0f;
    private float jumpDuration = 1.0f;

    private void Start()
    {
        characterAnimation = transform.GetChild(0).GetComponent<Animation>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }
    public void JumpCenter()
    {
        //characterAnimation.Play("JumpCenter");
        animator.SetTrigger(ANIMATOR_TRIGGER_JUMPCENTER);
    }

    public void JumpRight()
    {
        //characterAnimation.Play("JumpRight");
        animator.SetTrigger(ANIMATOR_TRIGGER_JUMPRIGHT);
    }
    public void JumpLeft()
    {
        //characterAnimation.Play("JumpLeft");
        animator.SetTrigger(ANIMATOR_TRIGGER_JUMPLEFT);
    }

    public void Crouch()
    {
        //StartCoroutine(PlayCrouchAnimationWithDuration(crouchDuration));
        animator.SetTrigger(ANIMATOR_TRIGGER_CROUCH);
    }


    private IEnumerator PlayCrouchAnimationWithDuration(float duration)
    {
        characterAnimation.Play("CrouchCenterEnter");
        yield return new WaitForSeconds(duration);
        characterAnimation.Play("CrouchCenterExit");
        yield break;
    }
}
