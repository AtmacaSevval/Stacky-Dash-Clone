using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text bestScoreText;

    void Update()
    {
        if (scoreText != null && bestScoreText != null)
        {

            scoreText.text = "Score: " + GameManager.singleton.score;
            bestScoreText.text = "Best Score: " + GameManager.singleton.best;
        }

    }

}
