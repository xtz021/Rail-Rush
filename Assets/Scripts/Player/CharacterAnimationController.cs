using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    public static CharacterAnimationController Instance {  get; private set; }

    private const string ANIMATOR_TRIGGER_JUMPCENTER = "JumpCenter";
    private const string ANIMATOR_TRIGGER_JUMPRIGHT = "JumpRight";
    private const string ANIMATOR_TRIGGER_JUMPLEFT = "JumpLeft";
    private const string ANIMATOR_TRIGGER_CROUCH = "CrouchCenter";

    Animation characterAnimation;
    Animator animator;
    private float crouchDuration = 1.0f;
    private float jumpDuration = 1.0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple instances of CharacterAnimationController detected. Destroying duplicate.");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

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

    public void Dead(string causeOfDeath)
    {
        animator.SetTrigger("Dead_" + causeOfDeath);
    }

    public void LeanLeft()
    {
        animator.SetBool("LeanLeft", true);
        animator.SetBool("LeanRight", false);
    }

    public void LeanRight()
    {
        animator.SetBool("LeanLeft", false);
        animator.SetBool("LeanRight", true);
    }

    public void StopLeaning()
    {
        animator.SetBool("LeanLeft", false);
        animator.SetBool("LeanRight", false);
    }

    public void ResetCharacterAnimator()
    {
        animator.SetTrigger("Revive");
    }


    private IEnumerator PlayCrouchAnimationWithDuration(float duration)
    {
        characterAnimation.Play("CrouchCenterEnter");
        yield return new WaitForSeconds(duration);
        characterAnimation.Play("CrouchCenterExit");
        yield break;
    }
}
