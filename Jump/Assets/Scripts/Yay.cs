using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yay : MonoBehaviour
{
   private float bounceForce = 20f;
   private void OnCollisionEnter2D(Collision2D collision)
   {
      if (collision.gameObject.CompareTag("Char"))
      { 
         Character player = collision.gameObject.GetComponent<Character>();
         Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

         if (player != null && rb != null && !player.isRapidAscent)
         {
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
            GetComponent<Collider2D>().enabled = false;
         }
      }
   }
}
