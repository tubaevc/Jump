using System.Collections;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    public float flyDuration = 7f;
    public float flyForce = 5.0f;
    private bool isAttached = false;
    public Vector3 propellerOffset = new Vector3(0, 0.5f, 0);
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
            transform.position = playerTransform.position + propellerOffset;
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
                AttachPropellerToPlayer(player);
                isAttached = true;
                StartCoroutine(DetachPropellerAfterDuration());
                StartPropellerAnimation();
            }
        }
    }

    private void AttachPropellerToPlayer(Character player)
    {
        playerTransform = player.transform;
        transform.position = playerTransform.position + propellerOffset;
        GetComponent<Collider2D>().enabled = false;
        player.DisableCollider();
    }

    private void StartPropellerAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("Fly", true);
        }
    }

    private void StopPropellerAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("Fly", false);
        }
    }

    private IEnumerator DetachPropellerAfterDuration()
    {
        yield return new WaitForSeconds(flyDuration);
        DetachPropeller();
    }

    private void DetachPropeller()
    {
        if (isAttached)
        {
            StopPropellerAnimation();
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