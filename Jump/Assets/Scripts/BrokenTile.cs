using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenTile : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private bool isBreaking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isBreaking && collision.gameObject.CompareTag("Char"))
        {
            isBreaking = true;
            animator.SetTrigger("Brake");
            StartCoroutine(FallAfterAnimation());
        }
    }

    IEnumerator FallAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        rb.gravityScale = 1; 
    }
}
