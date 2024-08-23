using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float flyDuration = 15f;
    public float flyForce = 7.0f;
    private bool isAttached = false;
    public Vector3 jetpackOffset = new Vector3(0, 0.5f, 0);
    private Transform playerTransform;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAttached && playerTransform != null)
        {
            transform.position = playerTransform.position + jetpackOffset;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAttached && collision.gameObject.CompareTag("Char"))
        {
            Character player = collision.gameObject.GetComponent<Character>();
            if (player != null && !player.isRapidAscent)
            {
                player.StartFlying(flyDuration, flyForce);
                AttachJetpackToPlayer(player);
                isAttached = true;
                StartCoroutine(DetachJetpackAfterDuration());
                StartJetpackAnimation();
            }
        }
    }

    private void AttachJetpackToPlayer(Character player)
    {
        playerTransform = player.transform;
        transform.position = playerTransform.position + jetpackOffset;
        GetComponent<Collider2D>().enabled = false;
        player.DisableCollider();
    }

    private void StartJetpackAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Fire");
        }
    }

    private void StopJetpackAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("Idle", true);
        }
    }

    private IEnumerator DetachJetpackAfterDuration()
    {
        yield return new WaitForSeconds(flyDuration);
        DetachJetpack();
    }

    private void DetachJetpack()
    {
        if (isAttached)
        {
            StopJetpackAnimation();
            isAttached = false;

            if (playerTransform != null)
            {
                playerTransform.GetComponent<Character>().EnableCollider();
            }

            playerTransform = null;
            GetComponent<Collider2D>().enabled = true;
            Destroy(gameObject, 1f);
        }
    }
}