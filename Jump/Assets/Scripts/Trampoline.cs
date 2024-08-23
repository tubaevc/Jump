using System.Collections;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float bounceForce = 5f; 
    public float minDownwardVelocity = -2f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Char"))
        {
            Character player = collision.gameObject.GetComponent<Character>();
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (player != null && playerRb != null && playerRb.velocity.y <= minDownwardVelocity)
            {
                animator.SetTrigger("Jump");
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
                playerRb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
                StartCoroutine(RotatePlayer(player.gameObject, bounceForce));
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    private IEnumerator RotatePlayer(GameObject player, float force)
    {
        float elapsedTime = 0f;
        float duration = 2f;
        Quaternion startRotation = player.transform.rotation;
        Vector3 startEulerAngles = startRotation.eulerAngles;

        while (elapsedTime < duration)
        {
            float rotationProgress = elapsedTime / duration;
            float currentAngle = Mathf.Lerp(0, 360, rotationProgress);

            player.transform.rotation = Quaternion.Euler(startEulerAngles.x, startEulerAngles.y, currentAngle);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        player.transform.rotation = startRotation;
    }
}