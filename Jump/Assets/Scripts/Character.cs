using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
   [SerializeField] private float moveSpeed;
   private Rigidbody2D rb;
   [SerializeField] private float jumpForce;
   [SerializeField] private GameObject firePrefab;
   [SerializeField] private Transform fireSpawnPoint;
   [SerializeField] private GameObject gameOverScreen;
   public float fallThreshold = -10f;
   private Animator _animator;
   
   private bool isFlying = false;
   private float flyTime = 0f;
   private float flyForce = 0f;
   
   public bool isRapidAscent = false;
   public float rapidAscentThreshold = 7f;
   private void Start()
   {
      rb = gameObject.GetComponent<Rigidbody2D>();
      _animator = GetComponent<Animator>();

   }

   private void Update()
   {
      float horizontal = Input.GetAxis("Horizontal");
      gameObject.transform.Translate(horizontal*moveSpeed*Time.deltaTime,0,0);
      
      if (Input.GetKeyDown(KeyCode.Space))
      {
         _animator.SetTrigger("Shoot");
         _animator.SetBool("Idle", false);
         ShootFire();
         StartCoroutine(ReturnToIdle());
      }

      IEnumerator ReturnToIdle()
      {
         // wait for anim finish
         yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
      
         _animator.SetBool("Idle", true);
      }
      
      
      if (transform.position.y < fallThreshold)
      {
         Die();
      }
      if (isFlying)
      {
         Fly();
      }
      
      isRapidAscent = rb.velocity.y > rapidAscentThreshold;
   }
   public void StartFlying(float duration, float force)
   {
      isFlying = true;
      flyTime = duration;
      flyForce = force;
   }

   private void Fly()
   {
      if (flyTime > 0)
      {
         rb.velocity = new Vector2(rb.velocity.x, flyForce);
         flyTime -= Time.deltaTime;
      }
      else
      {
         isFlying = false;
      }
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
      if (collision.gameObject.CompareTag("Enemy"))
      {
         Die();
      }
   }

   private void Die()
   {
      ShowGameOverScreen();
      Destroy(gameObject);
   }

   private void ShootFire()
   {
      Instantiate(firePrefab, fireSpawnPoint.position, fireSpawnPoint.rotation);
   }

   public void Jump()
   {
      rb.AddForce(Vector2.up*jumpForce);
   }
   
   private void ShowGameOverScreen()
   {
      gameOverScreen.SetActive(true);
   }
   public void DisableCollider()
   {
      GetComponent<Collider2D>().enabled = false;
   }

   public void EnableCollider()
   {
      GetComponent<Collider2D>().enabled = true;
   }
}
