using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private GameObject playerBall;

    private void Start()
    {
        _score.text = "Score: ";

        Instantiate(playerBall);
    }


}
