using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartAnimationController : MonoBehaviour
{
    Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void JumpAnimation(int jumpDirection)
    {
        if(jumpDirection == 0)
        {
            animator.SetTrigger("JumpCenter");
        }
        else if(jumpDirection == 1)
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
