using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTMP : MonoBehaviour
{
    private int score;
    private Transform lastPlatform;
    [SerializeField] private TMP_Text ScoreText;
    private void Start()
    {
        score = 0;
        lastPlatform = null;
    }

    
    public void IncreaseScore(Transform currentPlatform)
    {
        if (currentPlatform != lastPlatform)
        {
            int scoreIncrease = Random.Range(100, 201);
            score += scoreIncrease;
            ScoreText.text = "Score: "+score.ToString();
            lastPlatform = currentPlatform;
        }
       
    }
}
