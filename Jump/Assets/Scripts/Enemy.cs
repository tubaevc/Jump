using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   [SerializeField] private Animator _animator;
   [SerializeField] private float moveSpeed = 2f; 
   [SerializeField] private float moveDistance = 3f; 
   [SerializeField] private LayerMask platformLayer;
   
   private Vector3 startPosition;
   private Vector3 targetPosition;
   private bool movingRight = true;
   private void Start()
   {
      startPosition = transform.position;
      targetPosition = startPosition + new Vector3(moveDistance, 0, 0);

      PlatformBoundsCheck();
   }

   private void Update()
   {
      MoveEnemy();
   }

   private void MoveEnemy()
   {
      float step = moveSpeed * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

      if (transform.position == targetPosition)
      {
         movingRight = !movingRight;
         targetPosition = movingRight ? startPosition + new Vector3(moveDistance, 0, 0) : startPosition - new Vector3(moveDistance, 0, 0);
      }
   }

   private void PlatformBoundsCheck()
   {
      Collider2D[] platforms = Physics2D.OverlapBoxAll(startPosition, new Vector2(moveDistance * 2, 1), 0, platformLayer);

      if (platforms.Length > 0)
      {
         Collider2D platform = platforms[0];
         Vector3 platformSize = platform.bounds.size;

         moveDistance = platformSize.x / 2;
      }
   }
}
