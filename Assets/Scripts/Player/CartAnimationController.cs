using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CartAnimationController : MonoBehaviour
{
    public static CartAnimationController Instance { get; private set; }

    Animator animator;
    bool animationTriggered = false;

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

    public void DeadAnimation(string causeOfDeath)
    {
        animator.SetTrigger("Dead_" + causeOfDeath);
    }

}
