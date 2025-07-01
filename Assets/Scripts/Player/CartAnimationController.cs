using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CartAnimationController : MonoBehaviour
{
    public static CartAnimationController Instance { get; private set; }

    Animator animator;
    bool animationTriggered = false;
    private int currentChacterIndex = 0; // Index of the current character

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple instances of CartAnimationController detected. Destroying duplicate.");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void JumpAnimation(int jumpDirection)
    {
        if (jumpDirection == 0)
        {
            animator.SetTrigger("JumpCenter");
        }
        else if (jumpDirection == 1)
        {
            animator.SetTrigger("JumpRight");
        }
        else if (jumpDirection == -1)
        {
            animator.SetTrigger("JumpLeft");
        }
    }

    public void BackToIdle()
    {
        animator.SetTrigger("BackToRail");
    }

    public void DeadAnimation(string causeOfDeath)
    {
        animator.SetTrigger("Dead_" + causeOfDeath);
    }

    public void ResetCartAnimator()
    {
        animator.SetTrigger("Revive");
    }

    public void PlayCartMovingAudio()
    {
        AudioManager.Instance.Play("CartMoving");
        AudioManager.Instance.Stop("CartJump");
    }

    public void PlayCartJumpAudio()
    {
        AudioManager.Instance.Play("CartJump");
        AudioManager.Instance.Stop("CartMoving");
    }

    public void SetCurrentCharacterIndex(int index)
    {
        currentChacterIndex = index;
    }

    public void PlayDeadAudioByCharacter()
    {
        if(currentChacterIndex != 0)
        {
            AudioManager.Instance.Play($"Char{currentChacterIndex}_fall");
            Debug.Log($"Playing dead audio for character index: {currentChacterIndex}");
            AudioManager.Instance.Stop("CartMoving");
            AudioManager.Instance.Stop("CartJump");
        }
        else
        {
            Debug.LogWarning("Current character index is 0, no specific dead audio for this character.");
        }
    }
}
