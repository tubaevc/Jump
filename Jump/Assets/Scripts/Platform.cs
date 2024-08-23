using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private ScoreTMP _scoreTMP;
    private Transform platformTransform;

    private void Start()
    {
        _scoreTMP = FindObjectOfType<ScoreTMP>();
        platformTransform = GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Char"))
        {
            if (collision.relativeVelocity.y<=0f)
            {
            collision.gameObject.GetComponent<Character>().Jump();
            _scoreTMP.IncreaseScore(platformTransform);
            }
        }
    }
}
