using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; 
    [SerializeField] private float moveDistance = 3f; 

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingRight = true;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(moveDistance, 0, 0);

    }

    private void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (transform.position == targetPosition)
        {
            movingRight = !movingRight;
            targetPosition = movingRight ? startPosition + new Vector3(moveDistance, 0, 0) : startPosition - new Vector3(moveDistance, 0, 0);
        }
    }
}
