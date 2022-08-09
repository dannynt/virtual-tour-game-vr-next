using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    public void Approve()
    {
        animator.SetTrigger("Approve");
    }

    public void Disapprove()
    {
        animator.SetTrigger("Disapprove");
    }

    public void Touch()
    {
        animator.SetTrigger("Touch");
    }

    public void GoInDialogue(bool value)
    {
        animator.SetBool("dialogue", value);
    }
}
