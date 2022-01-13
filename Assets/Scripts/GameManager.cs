using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public bool GameEnded { get; private set; }
    public bool GameStarted { get; private set; }
    [SerializeField] TMP_Text TextStart;

    public int score;
    public int best;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }else if(singleton != this)
        {
            Destroy(gameObject);
        }

        best = PlayerPrefs.GetInt("Highscore");
    }

    public void StartGame()
    {
        GameStarted = true;
        TextStart.enabled = false;

    }
    public void EndGame(bool win)
    {
        GameEnded = true;
        Debug.Log("Game Ended");
        if (Input.GetMouseButton(0))
        {
            NextLevel();
        }
        //Application.Quit();

    }
    private void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt("Highscore", best);
        }
    }

}
