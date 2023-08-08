using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private GameObject playerBall;

    private int playerScore;

    //public enum GameState
    //{ Inbounds, Outbounds }

    //public GameState State
    //{
    //    get;
    //    set;
    //}
    //private void ChangeState(GameState newState)
    //{
    //    if (State != newState)
    //    {
    //        State = newState;
    //    }
    //}

    private void Start()
    {
        InitGame();
    }


    public void InitGame()
    {
        playerScore = 0;
        UpdateScore();
        Instantiate(playerBall);
    }

    public void PlusScore()
    {
        playerScore++;
        UpdateScore();
    }

    public void MinusScore()
    {
        if (playerScore > 0) playerScore--;
        UpdateScore();
    }

    private void UpdateScore()
    {
        _score.text = "Score: " + playerScore.ToString();
    }
}
